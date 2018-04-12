using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract;
using DataKioskStacks.Service.Contract;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace EnrollmentDataKiosk.Areas.Terminal.Controllers
{
    public class TerminalAPIController : Controller
    {


        private ClientStation authenticateRequest(out string msg)
        {
            try
            {

                IEnumerable<string> stationKeys, apiAccessKeys, stationNames;

                stationKeys = Request.Headers.GetValues("StationKey");
                apiAccessKeys = Request.Headers.GetValues("APIAccessKey");
                stationNames = Request.Headers.GetValues("StationName");


                if (stationKeys == null || apiAccessKeys == null || stationNames == null)
                {
                    msg = "Empty / Invalid Authentication Parameter Was Encountered";
                    return null;
                }
                var stationKeyList = stationKeys as IList<string> ?? stationKeys.ToList();
                var apiKeyList = apiAccessKeys as IList<string> ?? apiAccessKeys.ToList();
                var stationNameList = stationNames as IList<string> ?? stationNames.ToList();
                if (!apiKeyList.Any() || !stationKeyList.Any() || !stationNameList.Any())
                {
                    msg = "Empty / Invalid Authentication Parameter Was Encountered";
                    return null;
                }

                var apikey = apiKeyList.ElementAt(0);
                var stationKey = stationKeyList.ElementAt(0);
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


        //[HttpPost]
        //[Route("bioEnroll/DataKiosk/AuthorizeStationAccess")]
        //public StationRespObj AuthorizeStationAccess(StationRegObj appRegObj)
        //{
        //    try
        //    {

        //        if (appRegObj == null)
        //        {
        //            return new StationRespObj
        //            {
        //                ResponseStatus = new ResponseStatus
        //                {
        //                    IsSuccessful = false,
        //                    Message = new ResponseMessage
        //                    {
        //                        FriendlyMessage = "Unable to process your registration. Please try later",
        //                        TechnicalMessage = "Invalid / Empty Registration Object"
        //                    }
        //                }
        //            };
        //        }
        //        return ServiceProvider.Instance().GetClientStationServices().RegisterStation(appRegObj);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new StationRespObj
        //        {
        //            ResponseStatus = new ResponseStatus
        //            {
        //                IsSuccessful = false,
        //                Message = new ResponseMessage
        //                {
        //                    FriendlyMessage = "Unable to process your registration. Please try later",
        //                    TechnicalMessage = "Error: " + ex.Message
        //                }
        //            }
        //        };
        //    }
        //}

        //[HttpPost]
        //[Route("bioEnroll/DataKiosk/AuthorizeStationAccess")]
        //public JsonResult AuthorizeStationAccess(StationRegObj appRegObj)
        //{

        //    var resp = new StationRespObj
        //    {
        //        ResponseStatus = new ResponseStatus
        //        {
        //            IsSuccessful = false,
        //            Message = new ResponseMessage
        //            {
        //                FriendlyMessage = "Unable to process your registration. Please try later",
        //                TechnicalMessage = "Invalid / Empty Registration Object"
        //            }
        //        }
        //    };

        //    try
        //    {

        //        if (appRegObj == null)
        //        {
        //            return Json(resp, JsonRequestBehavior.AllowGet);
        //        }

        //        resp = ServiceProvider.Instance().GetClientStationServices().RegisterStation(appRegObj);
        //        return Json(resp, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        resp.ResponseStatus.Message.FriendlyMessage = "Unable to process your registration. Please try later";
        //        resp.ResponseStatus.Message.TechnicalMessage = "Error: " + ex.Message;
        //        return Json(resp, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[HttpPost]
        //[Route("bioEnroll/DataKiosk/RemoteLogin")]
        //public JsonResult RemoteLogin(RemoteStationLoginObj loginObj)
        //{

        //    var resp = new StationRespObj
        //    {
        //        ResponseStatus = new ResponseStatus
        //        {
        //            IsSuccessful = false,
        //            Message = new ResponseMessage
        //            {
        //                FriendlyMessage = "Unable to process your login. Please try later",
        //                TechnicalMessage = "Invalid / Empty Login Object"
        //            }
        //        }
        //    };

        //    try
        //    {

        //        if (loginObj == null)
        //        {
        //            return Json(resp, JsonRequestBehavior.AllowGet);
        //        }

        //        resp = ServiceProvider.Instance().GetEnrollerServices().RemoteLogin(loginObj);
        //        return Json(resp, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        resp.ResponseStatus.Message.FriendlyMessage = "Unable to process your registration. Please try later";
        //        resp.ResponseStatus.Message.TechnicalMessage = "Error: " + ex.Message;
        //        return Json(resp, JsonRequestBehavior.AllowGet);
        //    }
        //}

	}
}