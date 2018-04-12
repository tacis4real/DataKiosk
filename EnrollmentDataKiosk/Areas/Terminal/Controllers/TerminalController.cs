using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract;
using DataKioskStacks.Service.Contract;
using EnrollmentDataKiosk.Filters;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace EnrollmentDataKiosk.Areas.Terminal.Controllers
{
    public class TerminalController : ApiController
    {


        [HttpPost]
        [Route("bioEnroll/DataKiosk/BulkBeneficiaryRegistration")]
        [APIAuthenticate]
        public JsonResult<BulkBeneficiaryRegResponseObj> BulkBeneficiaryRegistration(BulkBeneficiaryRegObj appRegObj)
        {
            var response = new BulkBeneficiaryRegResponseObj
            {
                MainStatus = new ResponseStatus()
            };

            try
            {
                object authResponse;
                if (!Request.Properties.TryGetValue("AuthResponse", out authResponse))
                {
                    response = new BulkBeneficiaryRegResponseObj
                    {
                        MainStatus = new ResponseStatus
                        {
                            IsSuccessful = false,
                            Message = new ResponseMessage
                            {
                                FriendlyMessage = "Process Error Occurred! Check parameters and try again later",
                                TechnicalMessage = "Unable to perform authentication on the server"
                            }
                        }
                    };
                    return Json(response);
                }
                var auth = authResponse as ApiObj;
                if (auth == null)
                {
                    response = new BulkBeneficiaryRegResponseObj
                    {
                        MainStatus = new ResponseStatus
                        {
                            IsSuccessful = false,
                            Message = new ResponseMessage
                            {
                                FriendlyMessage = "Process Error Occurred! Check parameters and try again later",
                                TechnicalMessage = "Unable to perform authentication on the server"
                            }
                        }
                    };
                    return Json(response);
                }

                if (auth.IsSuccessful)
                {
                    response = ServiceProvider.Instance().GetBeneficiaryServices().AddBulkBeneficiary(appRegObj, auth.StationInfo);
                    return Json(response);
                }

                response = new BulkBeneficiaryRegResponseObj
                {
                    MainStatus = new ResponseStatus
                    {
                        IsSuccessful = auth.IsSuccessful,
                        Message = new ResponseMessage
                        {
                            FriendlyMessage = auth.ErrorMessage,
                            TechnicalMessage = auth.TechErrorMessage
                        }
                    }
                };
                return Json(response);
            }
            catch (Exception ex)
            {
                response.MainStatus.IsSuccessful = false;
                response.MainStatus.Message.FriendlyMessage = "An unexpected error occurred";
                response.MainStatus.Message.TechnicalMessage = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return Json(response);
            }
        }


        private ClientStation authenticateRequest(out string msg)
        {
            try
            {

                IEnumerable<string> stationIds, apiAccessKeys, stationNames;

                Request.Headers.TryGetValues("StationId", out stationIds);
                Request.Headers.TryGetValues("APIAccessKey", out apiAccessKeys);
                Request.Headers.TryGetValues("StationName", out stationNames);

                if (stationIds == null || apiAccessKeys == null || stationNames == null)
                {
                    msg = "Empty / Invalid Authentication Parameter Was Encountered";
                    return null;
                }
                var stationIdList = stationIds as IList<string> ?? stationIds.ToList();
                var apiKeyList = apiAccessKeys as IList<string> ?? apiAccessKeys.ToList();
                var stationNameList = stationNames as IList<string> ?? stationNames.ToList();
                if (!apiKeyList.Any() || !stationIdList.Any() || !stationNameList.Any())
                {
                    msg = "Empty / Invalid Authentication Parameter Was Encountered";
                    return null;
                }

                var apikey = apiKeyList.ElementAt(0);
                var stationKey = stationIdList.ElementAt(0);
                var stationName = stationNameList.ElementAt(0);

                if (string.IsNullOrEmpty(apikey) || string.IsNullOrEmpty(stationKey) ||
                    string.IsNullOrEmpty(stationName))
                {
                    msg = "Empty / Invalid Authentication Parameter Was Encountered";
                    return null;
                }

                var stationInfo = ServiceProvider.Instance()
                    .GetClientStationServices()
                    .ValidateClientAccess(stationKey, stationName, apikey, out msg);

                return stationInfo;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                msg = "Unable to authenticate request access";
                return null;
            }
        }

        [HttpPost]
        [Route("bioEnroll/DataKiosk/AuthorizeStationAccess")]
        public JsonResult<StationRespObj> AuthorizeStationAccess(StationRegObj appRegObj)
        {

            var resp = new StationRespObj
            {
                ResponseStatus = new ResponseStatus
                {
                    IsSuccessful = false,
                    Message = new ResponseMessage
                    {
                        FriendlyMessage = "Unable to process your registration. Please try later",
                        TechnicalMessage = "Invalid / Empty Registration Object"
                    }
                }
            };

            try
            {

                if (appRegObj == null)
                {
                    return Json(resp);
                }

                resp = ServiceProvider.Instance().GetClientStationServices().RegisterStation(appRegObj);
                return Json(resp);
            }
            catch (Exception ex)
            {
                resp.ResponseStatus.Message.FriendlyMessage = "Unable to process your registration. Please try later";
                resp.ResponseStatus.Message.TechnicalMessage = "Error: " + ex.Message;
                return Json(resp);
            }
        }

        [HttpPost]
        [Route("bioEnroll/DataKiosk/RemoteLogin")]
        public JsonResult<StationRespObj> RemoteLogin(RemoteStationLoginObj loginObj)
        {

            var resp = new StationRespObj
            {
                ResponseStatus = new ResponseStatus
                {
                    IsSuccessful = false,
                    Message = new ResponseMessage
                    {
                        FriendlyMessage = "Unable to process your login. Please try later",
                        TechnicalMessage = "Invalid / Empty Login Object"
                    }
                }
            };

            try
            {

                if (loginObj == null)
                {
                    return Json(resp);
                }

                resp = ServiceProvider.Instance().GetEnrollerServices().RemoteLogin(loginObj);
                return Json(resp);
            }
            catch (Exception ex)
            {
                resp.ResponseStatus.Message.FriendlyMessage = "Unable to process your registration. Please try later";
                resp.ResponseStatus.Message.TechnicalMessage = "Error: " + ex.Message;
                return Json(resp);
            }
        }

        
        

    }
}