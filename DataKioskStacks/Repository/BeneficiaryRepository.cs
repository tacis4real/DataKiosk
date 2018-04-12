using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract;
using DataKioskStacks.DataContract.ContractHelpers;
using DataKioskStacks.Infrastructure;
using DataKioskStacks.Infrastructure.Contract;
using DataKioskStacks.Repository.Helpers;
using DataKioskStacks.Service.Contract;
using Newtonsoft.Json;
using WebCribs.TechCracker.WebCribs.TechCracker;
using WebCribs.TechCracker.WebCribs.TechCracker.GateKeeper;

namespace DataKioskStacks.Repository
{
    internal class BeneficiaryRepository
    {

        private readonly IDataKioskRepository<Beneficiary> _repository;
        private readonly DataKioskUoWork _uoWork;

        public BeneficiaryRepository()
        {
            _uoWork = new DataKioskUoWork();
            _repository = new DataKioskRepository<Beneficiary>(_uoWork);
        }


        internal Beneficiary GetBeneficiary(long id)
        {
            try
            {
                var myItem = _repository.GetById(id);
                if (myItem == null || myItem.BeneficiaryId < 1)
                {
                    return new Beneficiary();
                }

                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }


        #region Upload From Remote

        public BulkBeneficiaryRegResponseObj AddBulkBeneficiary(BulkBeneficiaryRegObj beneficiaryRegObjs, UploadClientStation station)
        {
            var response = new BulkBeneficiaryRegResponseObj
            {
                MainStatus = new ResponseStatus
                {
                    IsSuccessful = false,
                    Message = new ResponseMessage
                    {
                        FriendlyMessage = "",
                        TechnicalMessage = ""
                    }
                },
                BeneficiaryRegResponses = new List<BeneficiaryRegResponseObj>()
            };

            try
            {
                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(beneficiaryRegObjs, out valResults))
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

                    response.MainStatus.Message.FriendlyMessage = response.MainStatus.Message.TechnicalMessage = errorDetail.ToString();
                    return response;
                }

                if (beneficiaryRegObjs.BeneficiaryRegObjs.IsNullOrEmpty())
                {
                    response.MainStatus.Message.FriendlyMessage = "Data List is empty!";
                    response.MainStatus.Message.TechnicalMessage = "Data List is empty!";
                    return response;
                }

                //var valSettings = new DataValidationSettingRepository().GetDataValidationSettings();
                //if (valSettings.IsNullOrEmpty())
                //{
                //    response.MainStatus.Message.FriendlyMessage = "Invalid Data Validation Setting";
                //    response.MainStatus.Message.TechnicalMessage = "Unable to retrieve Data Validation Parameters";
                //    return null;
                //}

                foreach (var item in beneficiaryRegObjs.BeneficiaryRegObjs)
                {
                    var thisResponse = new BeneficiaryRegResponseObj
                    {
                        Status = new ResponseStatus
                        {
                            IsSuccessful = false,
                            Message = new ResponseMessage
                            {
                                FriendlyMessage = "",
                                TechnicalMessage = ""
                            }
                        },
                        BeneficiaryId = item.BeneficiaryId,
                        MobileNumber = item.MobileNumber,
                    };

                    try
                    {
                        valResults = new List<ValidationResult>();
                        if (!EntityValidatorHelper.Validate(item, out valResults))
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

                            thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = errorDetail.ToString();
                            response.BeneficiaryRegResponses.Add(thisResponse);
                            continue;
                        }
                        
                        var regStatus = (RegStatus)Enum.Parse(typeof(RegStatus), item.Status.ToString(CultureInfo.InvariantCulture));

                        #region Delete

                        //try
                        //{
                        //    if (regStatus == RegStatus.Deleted)
                        //    {

                        //        if (!valSettings.Find(m => (string.Compare(m.Name.Trim(), "Can_Delete_Record", StringComparison.CurrentCultureIgnoreCase) == 0)).ValidationValue)
                        //        {
                        //            thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = "Data Deleting Feature is currently turned off! Unable to delete this record on the remote server";
                        //            response.BusinessRegResponses.Add(thisResponse);
                        //            continue;
                        //        }

                        //        if (item.BusinessInfoRemoteId < 1)
                        //        {
                        //            thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = "Invalid Remote Record Id! Unable to complete operation on this record";
                        //            response.BusinessRegResponses.Add(thisResponse);
                        //            continue;
                        //        }

                        //        var buzInfo = GetBusinessInfoItem(item.BusinessInfoRemoteId);
                        //        if (buzInfo == null)
                        //        {
                        //            thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = "No Remote Record Found! The specified Remote Id did not return any record";
                        //            response.BusinessRegResponses.Add(thisResponse);
                        //            continue;
                        //        }
                        //        buzInfo.Status = RegStatus.Deleted;
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    thisResponse.Status.Message.FriendlyMessage = "Unable to process this record due to error";
                        //    thisResponse.Status.Message.TechnicalMessage = "Error: " + ex.Message;
                        //    BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                        //    response.BeneficiaryRegResponses.Add(thisResponse);
                        //    continue;
                        //}

                        #endregion
                        #region Edit
                        //try
                        //{
                        //    if (regStatus == RegStatus.Edited)
                        //    {



                        //        if (!valSettings.Find(m => (string.Compare(m.Name.Trim(), "Can_Edit_Record", StringComparison.CurrentCultureIgnoreCase) == 0)).ValidationValue)
                        //        {
                        //            thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = "Data Editing Feature is currently turned off! Unable to update this record on the remote server";
                        //            response.BusinessRegResponses.Add(thisResponse);
                        //            continue;
                        //        }

                        //        if (item.BusinessInfoRemoteId < 1)
                        //        {
                        //            thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = "Invalid Remote Record Id! Unable to complete operation on this record";
                        //            response.BusinessRegResponses.Add(thisResponse);
                        //            continue;
                        //        }

                        //        var bizInfo = loadBusinessInfo(item, businessRegObjs.LocalAreaId, valSettings, ref thisResponse.Status);
                        //        if (bizInfo == null)
                        //        {
                        //            response.BusinessRegResponses.Add(thisResponse);
                        //            continue;
                        //        }

                        //        if (!doValidation(bizInfo, 2, valSettings, ref thisResponse.Status))
                        //        {
                        //            response.BusinessRegResponses.Add(thisResponse);
                        //            continue;
                        //        }

                        //        using (var db = _uoWork.BeginTransaction())
                        //        {
                        //            var processedBusinessInfo = _repository.Update(bizInfo);
                        //            _uoWork.SaveChanges();
                        //            if (processedBusinessInfo.BusinessInfoId < 1)
                        //            {
                        //                db.Rollback();
                        //                thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = "Update process failed!";
                        //                response.BusinessRegResponses.Add(thisResponse);
                        //                continue;
                        //            }

                        //            var ownerInfo = bizInfo.BusinessOwnerInfo;
                        //            var ownerRet = _ownerRepository.Update(ownerInfo);
                        //            _uoWork.SaveChanges();

                        //            if (ownerRet.BusinessOwnerInfoId < 1)
                        //            {
                        //                db.Rollback();
                        //                thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = "Processing Failed! Please try again later";
                        //                response.BusinessRegResponses.Add(thisResponse);
                        //                continue;
                        //            }

                        //            db.Commit();
                        //            thisResponse.Status.IsSuccessful = true;
                        //            thisResponse.BusinessInfoRemoteId = bizInfo.BusinessInfoId;
                        //            thisResponse.BusinessOwnerInfoRemoteId = ownerInfo.BusinessOwnerInfoId;
                        //            response.BusinessRegResponses.Add(thisResponse);
                        //        }
                        //        continue;
                        //    }
                        //}
                        //catch (Exception ex)
                        //{
                        //    thisResponse.Status.Message.FriendlyMessage = "Unable to process this record due to error";
                        //    thisResponse.Status.Message.TechnicalMessage = "Error: " + ex.Message;
                        //    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                        //    response.BusinessRegResponses.Add(thisResponse);
                        //    continue;
                        //}

                        #endregion
                        #region New Record


                        #region Saving Image

                        string msg;
                        var org = station.OrganizationName;
                        var stationId = station.StationId;
                        var enrollerRegId = station.EnrollerRegId;
                        //var image = Utils.BeneficiaryRegObj.Image;

                        var imgResPath = EnrollHelper.PrepareImagePath(org, stationId, enrollerRegId, out msg);
                        if (imgResPath.IsNullOrEmpty() || !msg.IsNullOrEmpty())
                        {
                            thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = msg;
                            response.BeneficiaryRegResponses.Add(thisResponse);
                            continue;
                        }
                        
                        var bytes = Convert.FromBase64String(item.ImageByteString);
                        File.WriteAllBytes(@imgResPath, bytes);
                        var imgFileName = Path.GetFileName(imgResPath);

                        #region Biometric

                        //var listOfArrays = new List<byte[]>();
                        //var array = listOfArrays
                        //                .SelectMany(a => a)
                        //                .ToArray();


                        var fingerPrintByteArray = item.FingerPrintTemplate
                                        .SelectMany(a => a)
                                        .ToArray();

                        //var bio = new BiometricData
                        //{
                        //    FingerPrintTemplates = item.FingerPrintTemplate,
                        //    ImageFileName = Path.GetFileName(imgResPath),
                        //    ImagePath = "~/" + imgResPath
                        //};
                        //string _bio = JsonConvert.SerializeObject(bio);


                        #endregion

                        #endregion

                        var ben = new Beneficiary
                        {
                            Surname = item.Surname,
                            FirstName = item.FirstName,
                            Othernames = item.Othernames,
                            Sex = item.Sex,
                            MaritalStatus = item.MaritalStatus,
                            OccupationId = item.OccupationId,
                            DateOfBirth = item.DateOfBirth,
                            ResidentialAddress = item.ResidentialAddress,
                            OfficeAddress = item.OfficeAddress,
                            LocalAreaId = item.LocalAreaId,
                            StateId = item.StateId,
                            ImageFileName = imgFileName,
                            ImagePath = "~/" + imgResPath,
                            RemoteImageFileName = item.ImageFileName,
                            BeneficiaryRemoteId = item.BeneficiaryId,
                            FingerPrintTemplates = fingerPrintByteArray,
                            //Biometric = _bio,

                            SurnameHashed = UniqueHashing.GetStandardHash(item.Surname.Trim().Replace(" ", "").ToLower()),
                            FirstNameHashed = UniqueHashing.GetStandardHash(item.FirstName.Trim().Replace(" ", "").ToLower()),
                            OtherNameHashed = UniqueHashing.GetStandardHash(item.Othernames.Trim().Replace(" ", "").ToLower()),
                            MobileNoHashed = UniqueHashing.GetStandardHash(item.MobileNumber.Trim().Replace(" ", "").ToLower()),
                            DateOfBirthHashed = UniqueHashing.GetStandardHash(item.DateOfBirth.Trim().Replace(" ", "").ToLower()),
                            
                            Status = regStatus,
                            EnrollerId = station.EnrollerId,
                            ClientStationId = station.ClientStationId,
                            TimeStampRegistered = DateTime.Now,
                        };

                        if (!doValidation(ben, 1, ref thisResponse.Status))
                        {
                            response.BeneficiaryRegResponses.Add(thisResponse);
                            continue;
                        }
                        
                        var retVal = _repository.Add(ben);
                        _uoWork.SaveChanges();
                        if (retVal.BeneficiaryId < 1)
                        {
                            thisResponse.Status.Message.FriendlyMessage = thisResponse.Status.Message.TechnicalMessage = "Processing Failed! Please try again later";
                            response.BeneficiaryRegResponses.Add(thisResponse);
                            continue;
                        }
                        
                        thisResponse.Status.IsSuccessful = true;
                        thisResponse.BeneficiaryRemoteId = retVal.BeneficiaryId;
                        response.BeneficiaryRegResponses.Add(thisResponse);
                        #endregion

                    }
                    catch (Exception ex)
                    {
                        thisResponse.Status.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                        thisResponse.Status.Message.TechnicalMessage = "Error: " + ex.Message;
                        BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                        response.BeneficiaryRegResponses.Add(thisResponse);
                    }

                }


                response.MainStatus.IsSuccessful = true;
                return response;
            }
            catch (Exception ex)
            {
                response.MainStatus.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                response.MainStatus.Message.TechnicalMessage = "Error: " + ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return response;
            }
        }




        #region For Adding
        private bool doValidation(Beneficiary beneficiary, int calType, ref ResponseStatus status)
        {
            try
            {
                string msg;

                var surnameHashed = beneficiary.SurnameHashed;
                var dobHashed = beneficiary.DateOfBirthHashed;
                var mobileHashed = beneficiary.MobileNoHashed;
                var firstnameHashed = beneficiary.FirstNameHashed;
                var othernameHashed = beneficiary.OtherNameHashed;
                var sex = beneficiary.Sex;

                #region Settings Validation
                //if (valSettings.Find(m => (string.Compare(m.Name.Trim(), "Validate_Surname", StringComparison.CurrentCultureIgnoreCase) == 0)).ValidationValue)
                //{
                //    surnameHashed = businessInfo.BusinessOwnerInfo.SurnameHashed;
                //}

                //if (valSettings.Find(m => (string.Compare(m.Name.Trim(), "Validate_Date_Of_Birth", StringComparison.CurrentCultureIgnoreCase) == 0)).ValidationValue)
                //{
                //    dobHashed = businessInfo.BusinessOwnerInfo.DateOfBirthHashed;
                //}

                //if (valSettings.Find(m => (string.Compare(m.Name.Trim(), "Validate_Mobile_Number", StringComparison.CurrentCultureIgnoreCase) == 0)).ValidationValue)
                //{
                //    mobileHashed = businessInfo.BusinessOwnerInfo.MobileNoHashed;
                //}

                //if (valSettings.Find(m => (string.Compare(m.Name.Trim(), "Validate_Othernames", StringComparison.CurrentCultureIgnoreCase) == 0)).ValidationValue)
                //{
                //    othernameHashed = businessInfo.BusinessOwnerInfo.OthernamesHashed;
                //}

                //if (valSettings.Find(m => (string.Compare(m.Name.Trim(), "Validate_Business_Name", StringComparison.CurrentCultureIgnoreCase) == 0)).ValidationValue)
                //{
                //    busNameHashed = businessInfo.BusinessNameHashed;
                //}

                //if (valSettings.Find(m => (string.Compare(m.Name.Trim(), "Validate_Sex", StringComparison.CurrentCultureIgnoreCase) == 0)).ValidationValue)
                //{
                //    sex = businessInfo.BusinessOwnerInfo.Sex;
                //}
                #endregion
                
                if (surnameHashed == 0 && dobHashed == 0 && mobileHashed == 0 && othernameHashed == 0 && firstnameHashed == 0 &&
                    sex == 0)
                {
                    //No Validation Required
                    return true;
                }

                if (doesBeneficiaryInfoExist(surnameHashed, dobHashed, mobileHashed, othernameHashed, firstnameHashed, sex, calType,
                    out msg))
                {
                    status.Message.FriendlyMessage = string.IsNullOrEmpty(msg) ? "Duplicate Error! This Beneciary Information Already Exist" : "Unable to validate Beneficiary Information";
                    status.Message.TechnicalMessage = string.IsNullOrEmpty(msg) ? "Duplicate Error" : msg;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                status.Message.FriendlyMessage = "Unable to validate data";
                status.Message.TechnicalMessage = "Error: " + ex.Message;
                return false;
            }
        }

        private bool doesBeneficiaryInfoExist(int surnameHashed, int dobHashed, int mobileHashed, int othernameHashed, int firstnameHashed, int sex, int calType, out  string msg)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append(string.Format("SELECT * FROM \"EnrollKiosk\".\"Beneficiary\" WHERE 1 = 1"));

                if (Math.Abs(surnameHashed) > 0)
                {
                    sql.Append(string.Format(" AND \"SurnameHashed\" = {0}", surnameHashed));
                }

                if (Math.Abs(dobHashed) > 0)
                {
                    sql.Append(string.Format(" AND \"DateOfBirthHashed\" = {0}", dobHashed));
                }

                if (Math.Abs(mobileHashed) > 0)
                {
                    sql.Append(string.Format(" AND \"MobileNoHashed\" = {0}", mobileHashed));
                }

                if (Math.Abs(firstnameHashed) > 0)
                {
                    sql.Append(string.Format(" AND \"FirstNameHashed\" = {0}", firstnameHashed));
                }

                if (Math.Abs(othernameHashed) > 0)
                {
                    sql.Append(string.Format(" AND \"OtherNameHashed\" = {0}", othernameHashed));
                }

                if (sex > 0 && sex < 3)
                {
                    sql.Append(string.Format(" AND \"Sex\" = {0}", sex));
                }

                var retVal = _repository.RepositoryContext()
                     .Database.SqlQuery<Beneficiary>(sql.ToString()).ToList();
                if (retVal.IsNullOrEmpty())
                {
                    msg = "";
                    return false;
                }
                if (calType == 2)
                {
                    if (retVal.Count == 1)
                    {
                        msg = "";
                        return false;
                    }
                }
                msg = "";
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return true;
            }
        }
        #endregion

        #endregion

    }
}
