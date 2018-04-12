using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract;
using DataKioskStacks.Infrastructure;
using DataKioskStacks.Infrastructure.Contract;
using DataKioskStacks.Repository.Helpers;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Repository
{
    internal class EnrollerRepository
    {

        private readonly IDataKioskRepository<Enroller> _repository;
        private readonly DataKioskUoWork _uoWork;

        public EnrollerRepository()
        {
            _uoWork = new DataKioskUoWork();
            _repository = new DataKioskRepository<Enroller>(_uoWork);
        }
        
        internal long AddEnroller(Enroller enroller, out string msg)
        {
            try
            {

                #region Null Validation
                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(enroller, out valResults))
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

                //var station = GetStation(corporateInfo.StationId);
                //if (station == null || string.IsNullOrEmpty(station.StationName) || station.ClientStationId < 1)
                //{
                //    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Invalid Station Key";
                //    return response;
                //}

                if (IsDuplicate(enroller.ClientStationId, enroller.UserName, enroller.Email, enroller.EnrollerRegId, enroller.MobileNumber, out msg))
                {
                    return 0;
                }

                #endregion



                enroller.UserCode = Crypto.HashPassword(enroller.Password);
                enroller.Salt = EncryptionHelper.GenerateSalt(30, 50);
                enroller.Password = Crypto.GenerateSalt(16);

                enroller.TimeStampRegistered = DateTime.Now;
                enroller.Status = 1;
                var processedClientStation = _repository.Add(enroller);
                _uoWork.SaveChanges();
                msg = "";
                return processedClientStation.EnrollerId;
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later " + ex.GetBaseException().Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return 0;
            }
        }
        internal List<Enroller> GetEnrollers()
        {
            try
            {
                var myItems = _repository.GetAll().ToList();
                if (!myItems.Any()) { return new List<Enroller>(); }
                return myItems;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new List<Enroller>(); ;
            }
        }
        public Enroller GetEnroller(string username)
        {
            try
            {
                var enrollerLists = _repository.GetAll(x => string.Compare(x.UserName, username, StringComparison.CurrentCultureIgnoreCase) == 0).ToList();
                if (!enrollerLists.Any() || enrollerLists.Count != 1)
                {
                    return new Enroller();
                }

                return enrollerLists[0].EnrollerId < 1 ? new Enroller() : enrollerLists[0];
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        public Enroller GetEnrollerByRegId(string regId)
        {
            try
            {
                var enrollerLists = _repository.GetAll(x => string.Compare(x.EnrollerRegId, regId, StringComparison.CurrentCultureIgnoreCase) == 0).ToList();
                if (!enrollerLists.Any() || enrollerLists.Count != 1)
                {
                    return new Enroller();
                }

                return enrollerLists[0].EnrollerId < 1 ? new Enroller() : enrollerLists[0];
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }


        #region Duplicate Check

        


        private bool IsDuplicate(long clientStationId, string username, string email, string sin, string phoneNo, out string msg)
        {
            try
            {

                #region Latest Duplicate

                // Check Duplicate Phone Number & SIN
                var check = IsUsernameExist(username);
                var check1 = IsPhoneNumberExist(clientStationId, phoneNo);
                var check2 = IsSINExist(clientStationId, sin);
                var check3 = IsEmailExist(clientStationId, email);

                if ((!check.Any() || check.Count == 0) && (!check1.Any() || check1.Count == 0) && (!check2.Any() || check2.Count == 0) && (!check3.Any() || check3.Count == 0))
                {
                    msg = "";
                    return false;
                }

                if (check != null)
                {
                    if (check.Count > 0)
                    {
                        msg = "Duplicate Error! Username already exist under";
                        return true;
                    }
                }

                if (check1 != null && check1.Count > 0)
                {
                    msg = "Duplicate Error! Phone Number already been used by another operator";
                    return true;
                }

                if (check2 != null)
                {
                    if (check2.Count > 0)
                    {
                        msg = "Duplicate Error! Operator Reg. No already exist under this station's organization";
                        return true;
                    }
                }

                if (check3 != null && check3.Count > 0)
                {
                    msg = "Duplicate Error! Email already been used by another operator";
                    return true;
                }

                #endregion

                msg = "";
                return false;

                //msg = "Unable to check duplicate! Please try again later";
                //return true;
            }
            catch (Exception ex)
            {
                msg = "Error: " + ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return true;
            }
        }
        internal List<Enroller> IsUsernameExist(string username)
        {
            try
            {
                var myItem = new EnrollerRepository().GetEnrollers().FindAll(x => x.UserName == username);
                if (!myItem.Any()) { return new List<Enroller>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        internal List<Enroller> IsPhoneNumberExist(long clientStationId, string phoneNo)
        {
            try
            {
                //var myItem =
                //    new EnrollerRepository().GetEnrollers()
                //        .FindAll(x => x.OrganizationId == organizationId && x.MobileNumber == phoneNo);
                var myItem =
                    new EnrollerRepository().GetEnrollers()
                        .FindAll(x => x.ClientStationId == clientStationId && x.MobileNumber == phoneNo);
                if (!myItem.Any()) { return new List<Enroller>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        internal List<Enroller> IsEmailExist(long clientStationId, string email)
        {
            try
            {
                //var myItem =
                //    new EnrollerRepository().GetEnrollers()
                //        .FindAll(x => x.OrganizationId == organizationId && x.Email == email);
                var myItem =
                    new EnrollerRepository().GetEnrollers()
                        .FindAll(x => x.ClientStationId == clientStationId && x.Email == email);
                if (!myItem.Any()) { return new List<Enroller>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        internal List<Enroller> IsSINExist(long clientStationId, string sin)
        {
            try
            {
                //var myItem =
                //    new EnrollerRepository().GetEnrollers()
                //        .FindAll(x => x.OrganizationId == organizationId && x.EnrollerRegId == sin);
                var myItem =
                    new EnrollerRepository().GetEnrollers()
                        .FindAll(x => x.ClientStationId == clientStationId && x.EnrollerRegId == sin);
                if (!myItem.Any()) { return new List<Enroller>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new List<Enroller>();
            }
        }

        #endregion

        #region Remote Login

        public StationRespObj RemoteLogin(RemoteStationLoginObj loginObj)
        {
            var response = new StationRespObj
            {
                ResponseStatus = new ResponseStatus
                {
                    Message = new ResponseMessage(),
                    IsSuccessful = false,
                }
            };
            try
            {
                #region Null Validation

                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(loginObj, out valResults))
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

                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = errorDetail.ToString();
                    return response;
                }

                #endregion
                
                string msg;
                var enroller = LoginEnroller(loginObj.UserName, loginObj.Password, out msg);
                if (enroller == null || string.IsNullOrEmpty(enroller.EnrollerRegId) || enroller.EnrollerId < 1)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Invalid Station Operator Info";
                    return response;
                }

                var station = new ClientStationRepository().GetStation(enroller.ClientStationId);
                if (station == null || station.ClientStationId < 1)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Invalid Station Info";
                    return response;
                }

                enroller.DeviceId = loginObj.DeviceId;
                enroller.DeviceIP = loginObj.DeviceIP;
                enroller.Status = 2;
                enroller.TimeStampAuthorized = DateTime.Now;
                var processedClientStationEnroller = _repository.Update(enroller);
                _uoWork.SaveChanges();

                if (processedClientStationEnroller.EnrollerId < 1)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Process Failed! Unable to process operator authorize information";
                    return response;
                }

                response.ResponseStatus.IsSuccessful = true;
                response.APIAccessKey = station.APIAccessKey;
                response.ClientStationId = enroller.ClientStationId;
                response.ResidentialAddress = enroller.Address;
                response.Surname = enroller.Surname;
                response.FirstName = enroller.FirstName;
                response.Othernames = enroller.OtherNames;
                response.Sex = enroller.Sex;
                response.Email = enroller.Email;
                response.MobileNumber = enroller.MobileNumber;
                //response.OrganizationId = enroller.OrganizationId;
                response.EnrollerRegId = enroller.EnrollerRegId;
                response.EnrollerId = enroller.EnrollerId;
                response.StationId = station.StationId;
                response.StationName = station.StationName;
                //response.UserName = corporateInfo.UserName;
                response.ResponseStatus.ReturnedId = enroller.EnrollerId;
                response.ResponseStatus.IsSuccessful = true;
                response.StationStatus = station.Status;
                response.EnrollerStatus = enroller.Status;
                //response.ResponseStatus.ReturnedValue = retId;
                //response.DeviceId = station.DeviceId;
                return response;
            }
            catch (Exception ex)
            {
                response.ResponseStatus.Message.FriendlyMessage = "Processing Error Occurred! Please try again later " + ex.Message;
                response.ResponseStatus.Message.TechnicalMessage = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return response;
            }
        }
        public Enroller LoginEnroller(string username, string password, out string msg)
        {
            try
            {
                var enroller = GetEnroller(username);
                if (enroller == null || enroller.EnrollerId < 1)
                {
                    msg = "Login Failed! Reason: Incorrect Username or Password";
                    return new Enroller();
                }

                //string msg;
                if (!ValidateEnroller(enroller, password, out msg))
                {
                    msg = string.IsNullOrEmpty(msg) ? "Unable to login" : msg;
                    return new Enroller();
                }

                return enroller;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                msg = "Login Failed! Reason: " + ex.Message;
                return new Enroller();
            }
        }
        public bool ValidateEnroller(Enroller thisUser, string password, out string msg)
        {
            try
            {
                if (thisUser == null)
                {
                    msg = "Invalid / Empty Enroller Information";
                    return false;
                }

                var flag = Crypto.VerifyHashedPassword(thisUser.UserCode, password);
                //RecordLoginEvent(thisUser.UserProfileId, flag && thisUser.IsApproved);
                if (!flag)
                {
                    msg = "Invalid Username, Password or both";
                    return false;
                }

                //if (!thisUser.IsApproved)
                //{
                //    if (thisUser.IsLockedOut)
                //    {
                //        msg = "This user is currently locked out due to several wrong passwords";
                //        return false;
                //    }
                //    msg = "This user is currently disabled by the admin";
                //    return false;
                //}
                msg = "";
                return true;

            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                msg = "Unable to validate user";
                return false;
            }
        }
        
        #endregion

    }
}
