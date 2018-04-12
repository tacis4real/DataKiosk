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
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.Infrastructure;
using DataKioskStacks.Infrastructure.Contract;
using DataKioskStacks.Repository.Helpers;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Repository
{
    internal class ClientStationRepository
    {

        private readonly IDataKioskRepository<ClientStation> _repository;
        private readonly IDataKioskRepository<Enroller> _enrollerRepository; 
        private readonly IDataKioskRepository<LocalArea> _localRepository;
        private readonly IDataKioskRepository<Organization> _organizationRepository; 
        private readonly DataKioskUoWork _uoWork;

        public ClientStationRepository()
        {
            _uoWork = new DataKioskUoWork();
            _repository = new DataKioskRepository<ClientStation>(_uoWork);
            _enrollerRepository = new DataKioskRepository<Enroller>(_uoWork);
            _localRepository = new DataKioskRepository<LocalArea>(_uoWork);
            _organizationRepository = new DataKioskRepository<Organization>(_uoWork);
        }


        internal long AddClientStation(ClientStation clientStation, out string msg)
        {
            try
            {
                clientStation.APIAccessKey = "3924156302";
                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(clientStation, out valResults))
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

                #region Get Organization Info
                //var clientOrg = new OrganizationRepository().GetOrganization(clientStation.OrganizationId);
                #endregion
                //var key = GenerateStationKey(clientStation.StationName, clientOrg.PhoneNumber, clientStation.StationId);
                var key = GenerateStationKey(clientStation.StationName, "08036975694", clientStation.StationId);
                if (string.IsNullOrEmpty(key) || key.Length != 10)
                {
                    msg = "Unable to generate Station Key";
                    return -1;
                }
                clientStation.APIAccessKey = key;
                clientStation.TimeStampRegistered = DateTime.Now;
                clientStation.Status = 1;
                var processedClientStation = _repository.Add(clientStation);
                _uoWork.SaveChanges();
                msg = "";
                return processedClientStation.ClientStationId;
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later " + ex.GetBaseException().Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return 0;
            }
        }
        public StationRespObj RegisterStation(StationRegObj corporateInfo)
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
                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(corporateInfo, out valResults))
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

                var station = GetStation(corporateInfo.StationId);
                if (station == null || string.IsNullOrEmpty(station.StationName) || station.ClientStationId < 1)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Invalid Station Key";
                    return response;
                }

                //if (string.Compare(station.StationName.Trim(), corporateInfo.StationName.Trim(), StringComparison.CurrentCultureIgnoreCase) != 0)
                //{
                //    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Incorrect Station Name";
                //    return response;
                //}

                if (string.Compare(station.StationId.Trim(), corporateInfo.StationId.Trim(), StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Incorrect Station ID";
                    return response;
                }

                string msg;
                var opRegId = GenerateSIN(out msg, station.StationId, station.OrganizationId);
                if (string.IsNullOrEmpty(opRegId))
                {
                    response.ResponseStatus.Message.FriendlyMessage = "Unable to generate Operator ID";
                    response.ResponseStatus.Message.TechnicalMessage = "Unable to generate Operator ID";
                    return response;
                }


                if (IsDuplicate(station.OrganizationId, corporateInfo.UserName, corporateInfo.Email, opRegId, corporateInfo.MobileNumber, out msg))
                {
                    response.ResponseStatus.Message.FriendlyMessage = msg;
                    response.ResponseStatus.Message.TechnicalMessage = msg;
                    response.ResponseStatus.IsSuccessful = false;
                    return response;
                }

                var helper = new Enroller
                {
                    EnrollerRegId = opRegId,
                    //OrganizationId = station.OrganizationId,
                    ClientStationId = station.ClientStationId,
                    DeviceIP = corporateInfo.DeviceIP,
                    DeviceId = corporateInfo.DeviceId,
                    FirstName = corporateInfo.FirstName,
                    Surname = corporateInfo.Surname,
                    UserName = corporateInfo.UserName,
                    Email = corporateInfo.Email,
                    MobileNumber = corporateInfo.MobileNumber,
                    Status = 1,
                    RegisteredByUserId = 1,
                    TimeStampRegistered = DateTime.Now
                };

                //if (station.Status == 2)
                //{
                //    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "This Station is already configured and used";
                //    return response;
                //}

                //station.DeviceId = corporateInfo.DeviceId;
                //station.DeviceIP = corporateInfo.DeviceIP;
                //station.Status = 2;

                //var processedClientEnrollerStation = _enrollerRepository.Add(helper);
                var retId = new EnrollerRepository().AddEnroller(helper, out msg);
                _uoWork.SaveChanges();

                if (retId < 1)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Process Failed! Unable to process hosting information";
                    return response;
                }

                
                response.APIAccessKey = station.APIAccessKey;
                response.ClientStationId = station.ClientStationId;
                response.DeviceId = corporateInfo.DeviceId;
                response.Surname = corporateInfo.Surname;
                response.FirstName = corporateInfo.FirstName;
                response.Email = corporateInfo.Email;
                response.MobileNumber = corporateInfo.MobileNumber;
                //response.OrganizationId = station.OrganizationId;
                response.EnrollerRegId = opRegId;
                response.EnrollerId = retId;
                response.StationId = station.StationId;
                response.StationName = station.StationName;
                response.UserName = corporateInfo.UserName;
                response.ResponseStatus.IsSuccessful = true;
                response.ResponseStatus.ReturnedId = retId;
                return response;


                //public string APIAccessKey;
                //public string DeviceId;
                //public string StationId;
                //public long ClientStationId;
                //public long EnrollerId;
                //public long OrganizationId;
                //public string EnrollerRegId;
                //public string Surname;
                //public string FirstName;
                //public string UserName;
                //public string MobileNumber;
                //public string Email;



            }
            catch (Exception ex)
            {
                response.ResponseStatus.Message.FriendlyMessage = "Processing Error Occurred! Please try again later " + ex.Message;
                response.ResponseStatus.Message.TechnicalMessage = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return response;
            }
        }




        #region Online Setup of Enroller
        public StationRespObj RegisterStation(OperatorStationRegObj corporateInfo)
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
                List<ValidationResult> valResults;
                if (!EntityValidatorHelper.Validate(corporateInfo, out valResults))
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

                //var station = GetStation(corporateInfo.StationId);

                var enroller = new EnrollerRepository().GetEnroller(corporateInfo.UserName);
                if (enroller == null || string.IsNullOrEmpty(enroller.EnrollerRegId) || enroller.EnrollerId < 1)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Invalid Station Operator Info";
                    return response;
                }

                var station = GetStation(enroller.ClientStationId);
                if (station == null || station.ClientStationId < 1)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Invalid Station Info";
                    return response;
                }

                enroller.DeviceId = corporateInfo.DeviceId;
                enroller.DeviceIP = corporateInfo.DeviceIP;
                var processedClientStationEnroller = _enrollerRepository.Update(enroller);
                _uoWork.SaveChanges();

                if (processedClientStationEnroller.EnrollerId < 1)
                {
                    response.ResponseStatus.Message.FriendlyMessage = response.ResponseStatus.Message.TechnicalMessage = "Process Failed! Unable to process operator authorize information";
                    return response;
                }

                response.ResponseStatus.IsSuccessful = true;
                response.APIAccessKey = station.APIAccessKey;
                response.ClientStationId = enroller.ClientStationId;
                response.DeviceId = corporateInfo.DeviceId;
                response.Surname = enroller.Surname;
                response.FirstName = enroller.FirstName;
                response.Email = enroller.Email;
                response.MobileNumber = enroller.MobileNumber;
                //response.OrganizationId = enroller.OrganizationId;
                response.EnrollerRegId = enroller.EnrollerRegId;
                response.EnrollerId = enroller.EnrollerId;
                response.StationId = station.StationId;
                response.UserName = corporateInfo.UserName;
                response.ResponseStatus.ReturnedId = enroller.EnrollerId;
                response.ResponseStatus.IsSuccessful = true;
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

        #endregion

        internal ClientStation GetStation(long orgId, long clientStationId)
        {
            try
            {
                var retVal = _repository.GetAll(x => x.OrganizationId == orgId && x.ClientStationId == clientStationId).ToList();
                if (!retVal.Any())
                {
                    return null;
                }
                return retVal[0];
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }

        }
        internal ClientStation GetStation(long clientStationId)
        {
            try
            {
                var retVal = _repository.GetById(clientStationId);
                if (retVal == null || retVal.ClientStationId < 1)
                {
                    return null;
                }
                return retVal;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }

        }
        private ClientStation GetStation(string stationId)
        {
            try
            {
                var sql = new StringBuilder();
                sql.Append(string.Format("SELECT * FROM \"EnrollKiosk\".\"ClientStation\" WHERE \"StationId\" = '{0}'", stationId));

                var retVal = _repository.RepositoryContext()
                      .Database.SqlQuery<ClientStation>(sql.ToString()).ToList();
                if (retVal.IsNullOrEmpty())
                {
                    return null;
                }
                return retVal[0];
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }

        }

        private string GenerateStationKey(string fullname, string mobileNo, string stationKey)
        {
            try
            {
                var deviceCode = Math.Abs(stationKey.GetHashCode()) + "" + Math.Abs((fullname.GetHashCode() * mobileNo.GetHashCode()));
                if (deviceCode.StartsWith("0"))
                {
                    deviceCode = deviceCode.Substring(1);
                }
                if (deviceCode.Length < 18)
                {
                    deviceCode = deviceCode + "" + Math.Abs(DateTime.Now.ToString("yyyy/MMMM/dd").GetHashCode());
                }
                deviceCode = deviceCode.Substring(11, 2) + "" + deviceCode.Substring(0, 3) + "" + deviceCode.Substring(7, 3) + "" + deviceCode.Substring(5, 2);
                var serialNo = SerialGeneratorRepository.Generate();
                var regCode = "";
                if (serialNo > 0)
                {
                    regCode = serialNo + "" + deviceCode.Substring(0, deviceCode.Length - serialNo.ToString(CultureInfo.InvariantCulture).Length);
                }
                else
                {
                    regCode = deviceCode;
                }
                return regCode;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }


        #region Terminal

        public ClientStation ValidateClientAccess(string stationId, string stationName, string accessKey, out string msg)
        {
            try
            {
                var stationInfo = GetStation(stationId);
                if (stationInfo == null || stationInfo.ClientStationId < 1)
                {
                    msg = "Unregistered / Invalid Authorization Access";
                    return null;
                }
                if (
                    string.Compare(stationInfo.APIAccessKey.Trim(), accessKey.Trim(),
                        StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(stationInfo.StationId.Trim(), stationId.Trim(),
                        StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(stationInfo.StationName.Trim(), stationName.Trim(),
                        StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    msg = "Unregistered / Invalid Authorization Access";
                    return null;
                }
                if (stationInfo.Status != 2)
                {
                    msg = "Unauthorized Access!";
                    return null;
                }
                
                msg = "";
                return stationInfo;

            }
            catch (Exception ex)
            {
                msg = "Unable to validate Station Access";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        public UploadClientStation ValidateUploadClientAccess(string stationId, string stationName, string accessKey, out string msg)
        {
            try
            {
                var stationInfo = GetStation(stationId);
                if (stationInfo == null || stationInfo.ClientStationId < 1)
                {
                    msg = "Unregistered / Invalid Authorization Access";
                    return null;
                }
                if (
                    string.Compare(stationInfo.APIAccessKey.Trim(), accessKey.Trim(),
                        StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(stationInfo.StationId.Trim(), stationId.Trim(),
                        StringComparison.CurrentCultureIgnoreCase) != 0 ||
                    string.Compare(stationInfo.StationName.Trim(), stationName.Trim(),
                        StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    msg = "Unregistered / Invalid Authorization Access";
                    return null;
                }
                //if (stationInfo.Status != 2)
                //{
                //    msg = "Unauthorized Access!";
                //    return null;
                //}

                var orgName = _organizationRepository.GetById(stationInfo.OrganizationId).Name;
                var retInfo = new UploadClientStation
                {
                    ClientStationId = stationInfo.ClientStationId,
                    StationId = stationInfo.StationId,
                    StationName = stationInfo.StationName,
                    APIAccessKey = stationInfo.APIAccessKey,
                    StateId = stationInfo.StateId,
                    LocalAreaId = stationInfo.LocalAreaId,
                    OrganizationId = stationInfo.OrganizationId,
                    OrganizationName = orgName,
                    RegisteredByUserId = stationInfo.RegisteredByUserId
                };
                
                msg = "";
                return retInfo;

            }
            catch (Exception ex)
            {
                msg = "Unable to validate Station Access";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        #endregion



        #region SIN Generation

        public string GenerateOperatorId(long orgId, long clientStationId, out string msg)
        {
            try
            {

                var station = GetStation(orgId, clientStationId);
                if (station == null || string.IsNullOrEmpty(station.StationName) || station.ClientStationId < 1)
                {
                    msg = "No Station Info found! Try again";
                    return "";
                }

                // IDP-001
                var stationId = station.StationId + "-";
                var lastEnrollerInfo = GetMaxEnrollerInfo(out msg, orgId, clientStationId);
                if (lastEnrollerInfo == null || lastEnrollerInfo.EnrollerId < 1)
                {
                    if (!string.IsNullOrEmpty(msg))
                    {
                        return "";
                    }
                    return stationId + "001";
                }

                var retOpId = lastEnrollerInfo.EnrollerRegId.Replace(stationId, "");
                if (string.IsNullOrEmpty(retOpId))
                {
                    msg = "Unable to retreive generate OSN(Operator Station Number)";
                    return "";
                }

                var newOpId = stationId + (long.Parse(retOpId) + 1).ToString("D03");
                return newOpId;
            }
            catch (Exception ex)
            {
                msg = "Unable to retreive generate OSN(Operator Station Number)";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return "";
            }

        }
        
        #region Old Method
        public string GenerateSIN(out string msg, string stationId, long orgId)
        {
            try
            {
                // IDP-001
                stationId = stationId + "-";
                var lastEnrollerInfo = GetMaxEnrollerInfo(out msg, orgId);
                if (lastEnrollerInfo == null || lastEnrollerInfo.EnrollerId < 1)
                {
                    if (!string.IsNullOrEmpty(msg))
                    {
                        return "";
                    }
                    return stationId + "001";
                }

                var retOpId = lastEnrollerInfo.EnrollerRegId.Replace(stationId, "");
                if (string.IsNullOrEmpty(retOpId))
                {
                    msg = "Unable to retreive generate SIN";
                    return "";
                }

                var newOpId = stationId + (long.Parse(retOpId) + 1).ToString("D03");
                return newOpId;
            }
            catch (Exception ex)
            {
                msg = "Unable to retreive generate CRN";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return "";
            }

        }
        private Enroller GetMaxEnrollerInfo(out string msg, long organizationId)
        {

            try
            {
                var activity = GetLastEnrollerRegisteredByOrganizationId(organizationId);
                if (activity == null || activity.EnrollerId < 1)
                {
                    msg = "";
                    return null;
                }

                msg = "";
                return activity;
            }
            catch (Exception ex)
            {
                msg = "Unable to  generate CRN";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        internal Enroller GetLastEnrollerRegisteredByOrganizationId(long organizationId)
        {
            try
            {
                //var myItems = _enrollerRepository.GetAll(x => x.OrganizationId == organizationId).OrderByDescending(o => o.EnrollerId).ToList();
                //if (!myItems.Any()) { return new Enroller(); }
                //return myItems[0];
                return null;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new Enroller();
            }
        }

        #endregion

        private Enroller GetMaxEnrollerInfo(out string msg, long organizationId, long clientStationId)
        {
            try
            {
                var activity = GetLastEnrollerRegistered(clientStationId);
                if (activity == null || activity.EnrollerId < 1)
                {
                    msg = "";
                    return null;
                }
                
                msg = "";
                return activity;
            }
            catch (Exception ex)
            {
                msg = "Unable to  generate CRN";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        internal Enroller GetLastEnrollerRegistered(long clientStationId)
        {
            try
            {
                //var myItems = _enrollerRepository.GetAll(x => x.OrganizationId == organizationId && x.ClientStationId == clientStationId).OrderByDescending(o => o.EnrollerId).ToList();
                var myItems =
                    _enrollerRepository.GetAll(
                        x => x.ClientStationId == clientStationId)
                        .OrderByDescending(o => o.EnrollerId)
                        .ToList();
                if (!myItems.Any()) { return new Enroller(); }
                return myItems[0];
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new Enroller();
            }
        }

        internal Enroller GetLastEnrollerRegisteredByOrganizationId(long organizationId, long clientStationId)
        {
            try
            {
                //var myItems = _enrollerRepository.GetAll(x => x.OrganizationId == organizationId && x.ClientStationId == clientStationId).OrderByDescending(o => o.EnrollerId).ToList();
                var myItems =
                    _enrollerRepository.GetAll(
                        x => x.ClientStationId == clientStationId)
                        .OrderByDescending(o => o.EnrollerId)
                        .ToList();
                if (!myItems.Any()) { return new Enroller(); }
                return myItems[0];
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new Enroller();
            }
        }



        internal Enroller GetEnrollerByOrganizationId(long organizationId)
        {
            try
            {
                //var myItems = _enrollerRepository.GetAll(x => x.OrganizationId == organizationId).OrderByDescending(o => o.EnrollerId).ToList();
                //if (!myItems.Any()) { return new Enroller(); }
                //return myItems[0];
                return null;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new Enroller();
            }
        }

        
        #region Duplication 

        private bool IsDuplicate(long organizationId, string username, string email, string sin, string phoneNo, out string msg)
        {
            try
            {

                #region Latest Duplicate

                // Check Duplicate Phone Number & SIN
                var check = IsUsernameExist(username);
                var check1 = IsPhoneNumberExist(organizationId, phoneNo);
                var check2 = IsSINExist(organizationId, sin);
                var check3 = IsEmailExist(organizationId, email);

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
        internal List<Enroller> IsPhoneNumberExist(long organizationId, string phoneNo)
        {
            try
            {
                //var myItem =
                //    new EnrollerRepository().GetEnrollers()
                //        .FindAll(x => x.OrganizationId == organizationId && x.MobileNumber == phoneNo);
                var myItem =
                    new EnrollerRepository().GetEnrollers()
                        .FindAll(x => x.MobileNumber == phoneNo);
                if (!myItem.Any()) { return new List<Enroller>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        internal List<Enroller> IsEmailExist(long organizationId, string email)
        {
            try
            {
                //var myItem =
                //    new EnrollerRepository().GetEnrollers()
                //        .FindAll(x => x.OrganizationId == organizationId && x.Email == email);
                var myItem =
                    new EnrollerRepository().GetEnrollers()
                        .FindAll(x => x.Email == email);
                if (!myItem.Any()) { return new List<Enroller>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }
        internal List<Enroller> IsSINExist(long organizationId, string sin)
        {
            try
            {
                //var myItem =
                //    new EnrollerRepository().GetEnrollers()
                //        .FindAll(x => x.OrganizationId == organizationId && x.EnrollerRegId == sin);
                var myItem =
                    new EnrollerRepository().GetEnrollers()
                        .FindAll(x => x.EnrollerRegId == sin);
                if (!myItem.Any()) { return new List<Enroller>(); }
                return myItem;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new List<Enroller>();
            }
        }
        private bool IsDuplicate2(long organizationId, string username, string email, string sin, string phoneNo, out string msg, int status = 0)
        {
            try
            {

                #region Latest Duplicate

                // Check Duplicate Phone Number & SIN
                var check = IsUsernameExist(username);
                var check1 = IsPhoneNumberExist(organizationId, phoneNo);
                var check2 = IsSINExist(organizationId, sin);
                var check3 = IsEmailExist(organizationId, email);

                if ((!check.Any() || check.Count == 0) && (!check1.Any() || check1.Count == 0) && (!check2.Any() || check2.Count == 0) && (!check3.Any() || check3.Count == 0))
                {
                    msg = "";
                    return false;
                }

                switch (status)
                {
                    case 0:

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

                        break;

                    case 1:
                        if (check2 != null)
                        {
                            if (check2.Count > 1)
                            {
                                msg = "Duplicate Error! Church/Client already exist under the selected parent church";
                                return true;
                            }
                        }

                        if (check != null)
                        {
                            if (check.Count > 1)
                            {
                                msg = "Duplicate Error! Phone Number already been used by another Church";
                                return true;
                            }
                        }

                        break;
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

        #endregion

        #endregion


        #region Resources

        public static string GetBasePath()
        {
            var currentDomain = AppDomain.CurrentDomain;

            string dirPath;
            if (currentDomain.BaseDirectory.Contains("\\bin\\Debug"))
            {
                dirPath = currentDomain.BaseDirectory.Replace("\\bin\\Debug\\", "");
            }
            else if (currentDomain.BaseDirectory.Contains("\\bin\\Release"))
            {
                dirPath = currentDomain.BaseDirectory.Replace("\\bin\\Release\\", "");
            }
            else
            {
                dirPath = currentDomain.BaseDirectory;
            }
            return dirPath;
        }

        internal string GetFromResources(string resourceName)
        {
            try
            {
                using (var reader = new StreamReader(resourceName))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion
        

    }
}
