using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.Infrastructure;
using DataKioskStacks.Infrastructure.Contract;
using DataKioskStacks.Repository.Helpers;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Repository
{
    internal class OrganizationRepository
    {

        private readonly IDataKioskRepository<Organization> _repository;
        private readonly DataKioskUoWork _uoWork;

        public OrganizationRepository()
        {
            _uoWork = new DataKioskUoWork();
            _repository = new DataKioskRepository<Organization>(_uoWork);
        }


        internal long AddOrganization(Organization organization, out string msg)
        {
            try
            {

                #region Null Validation
                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(organization, out valResults))
                {
                    var errorDetail = new StringBuilder();
                    if (!valResults.IsNullOrEmpty())
                    {
                        errorDetail.AppendLine("Following error occurred:");
                        valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                    }
                    else
                    {
                        errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                    }

                    msg = errorDetail.ToString();
                    return -1;
                }
                #endregion

                #region Check Duplication

                if (IsDuplicate2(organization, out msg))
                {
                    return 0;
                }

                #endregion

                
                organization.TimeStampRegistered = DateTime.Now;
                //organization.Status = 1;
                var processedOrganization = _repository.Add(organization);
                _uoWork.SaveChanges();
                msg = "";
                return processedOrganization.OrganizationId;
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later " + ex.GetBaseException().Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return 0;
            }
        }

        internal ResponseStatus UpdateOrganization(Organization organizationRegObj)
        {
            var response = new ResponseStatus
            {
                IsSuccessful = false,
                Message = new ResponseMessage(),
                ReturnedId = 0
            };

            try
            {
                #region Null Validation

                if (organizationRegObj.Equals(null))
                {
                    response.Message.FriendlyMessage = response.Message.TechnicalMessage = "Error Occurred! Unable to proceed with your registration";
                    return response;
                }

                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(organizationRegObj, out valResults))
                {
                    var errorDetail = new StringBuilder();
                    if (!valResults.IsNullOrEmpty())
                    {
                        errorDetail.AppendLine("Following error occurred:");
                        valResults.ForEachx(m => errorDetail.AppendLine(m.ErrorMessage));
                    }
                    else
                    {
                        errorDetail.AppendLine("Validation error occurred! Please check all supplied parameters and try again");
                    }
                    response.Message.FriendlyMessage = response.Message.TechnicalMessage = errorDetail.ToString();
                    return response;
                }

                #endregion
                
                #region Get This Organization
                var thisOrganization = GetOrganization(organizationRegObj.OrganizationId);
                if (thisOrganization == null || thisOrganization.OrganizationId < 1)
                {
                    response.Message.FriendlyMessage = response.Message.TechnicalMessage = "Invalid Organization Information";
                    return response;
                }
                #endregion

                string msg;
                if (IsDuplicate2(organizationRegObj, out msg, 1))
                {
                    response.Message.FriendlyMessage = response.Message.TechnicalMessage = msg;
                    return response;
                }

                thisOrganization.Name = organizationRegObj.Name;
                thisOrganization.Email = organizationRegObj.Email;
                thisOrganization.Address = organizationRegObj.Address;
                thisOrganization.PhoneNumber = organizationRegObj.PhoneNumber;
                thisOrganization.Status = organizationRegObj.Status;

                var processedOrganization = _repository.Update(thisOrganization);
                _uoWork.SaveChanges();
                if (processedOrganization.OrganizationId < 1)
                {
                    response.Message.FriendlyMessage = response.Message.TechnicalMessage = "Process Failed! Please try again later";
                    return response;
                }

                response.IsSuccessful = true;
                return response;

            }
            catch (Exception ex)
            {
                response.Message.FriendlyMessage =
                             "Unable to complete your request due to error! Please try again later";
                response.Message.TechnicalMessage = "Error" + ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return response;
            }
        }



        internal Organization GetOrganization(long organizationId)
        {
            try
            {
                var myItem = _repository.GetById(organizationId);
                if (myItem == null || myItem.OrganizationId < 1) { return null; }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        internal List<Organization> GetOrganizations()
        {
            try
            {
                var myItems = _repository.GetAll().ToList();
                if (!myItems.Any()) { return new List<Organization>(); }
                return myItems;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new List<Organization>(); ;
            }
        }

        internal List<RegisteredOrganizationReportObj> GetOrganizationListObjs()
        {
            try
            {
                var myItemList = _repository.GetAll().ToList();
                if (!myItemList.Any() || myItemList.Count < 1) return new List<RegisteredOrganizationReportObj>();

                var retList = new List<RegisteredOrganizationReportObj>();
                myItemList.ForEachx(m =>
                {
                    retList.Add(new RegisteredOrganizationReportObj
                    {
                        OrganizationId = m.OrganizationId,
                        Name = m.Name,
                        Address = m.Address,
                        Email = m.Email,
                        PhoneNumber = m.PhoneNumber,
                        RegisteredByUserId = m.RegisteredByUserId,
                        TimeStampRegistered = m.TimeStampRegistered,
                        Status = m.Status
                    });
                });

                return retList;

            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new List<RegisteredOrganizationReportObj>();
            }
        }




        #region Duplicate Check

        private bool IsDuplicate2(Organization org, out string msg, int status = 0)
        {
            try
            {

                #region Latest Duplicate

                var check = IsPhoneNumberExist2(org, status);
                var check1 = IsEmailExist2(org, status);

                if ((!check.Any() || check.Count == 0) && (!check1.Any() || check1.Count == 0))
                {
                    msg = "";
                    return false;
                }

                if (check != null)
                {
                    if (check.Count > 0)
                    {
                        msg = "Duplicate Error! Phone Number already been used by another organization";
                        return true;
                    }
                }

                if (check1 != null && check1.Count > 0)
                {
                    msg = "Duplicate Error! Email already been used by another organization";
                    return true;
                }

                #endregion

                msg = "";
                return false;

            }
            catch (Exception ex)
            {
                msg = "Error: " + ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return true;
            }
        }

        internal List<Organization> IsPhoneNumberExist2(Organization org, int status = 0)
        {
            try
            {

                var myItem = new List<Organization>();
                switch (status)
                {
                    
                    case 0:
                        myItem = GetOrganizations().FindAll(x => x.PhoneNumber == org.PhoneNumber);
                        break;

                    case 1:
                        myItem = GetOrganizations().FindAll(x => x.PhoneNumber == org.PhoneNumber && x.OrganizationId != org.OrganizationId);
                        break;
                }
                
                if (!myItem.Any()) { return new List<Organization>(); }
                return myItem;

            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        internal List<Organization> IsEmailExist2(Organization org, int status = 0)
        {
            try
            {

                var myItem = new List<Organization>();
                switch (status)
                {

                    case 0:
                        myItem = GetOrganizations().FindAll(x => x.Email == org.Email);
                        break;

                    case 1:
                        myItem = GetOrganizations().FindAll(x => x.Email == org.Email && x.OrganizationId != org.OrganizationId);
                        break;
                }

                if (!myItem.Any()) { return new List<Organization>(); }
                return myItem;

            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }




        private bool IsDuplicate(string email, string phoneNo, out string msg)
        {
            try
            {

                #region Latest Duplicate

                var check = IsPhoneNumberExist(phoneNo);
                var check1 = IsEmailExist(email);

                if ((!check.Any() || check.Count == 0) && (!check1.Any() || check1.Count == 0) )
                {
                    msg = "";
                    return false;
                }

                if (check != null)
                {
                    if (check.Count > 0)
                    {
                        msg = "Duplicate Error! Phone Number already been used by another operator";
                        return true;
                    }
                }

                if (check1 != null && check1.Count > 0)
                {
                    msg = "Duplicate Error! Email already been used by another operator";
                    return true;
                }
                
                #endregion

                msg = "";
                return false;

            }
            catch (Exception ex)
            {
                msg = "Error: " + ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return true;
            }
        }
        internal List<Organization> IsPhoneNumberExist(string phoneNo)
        {
            try
            {
                var myItem = GetOrganizations().FindAll(x => x.PhoneNumber == phoneNo);
                if (!myItem.Any()) { return new List<Organization>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        internal List<Organization> IsEmailExist(string email)
        {
            try
            {
                var myItem = GetOrganizations().FindAll(x => x.Email == email);
                if (!myItem.Any()) { return new List<Organization>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        #endregion


        


    }
}
