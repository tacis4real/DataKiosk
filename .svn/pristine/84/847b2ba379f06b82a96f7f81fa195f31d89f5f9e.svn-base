using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract;
using DataKioskStacks.Service.Contract;

namespace EnrollmentDataKiosk.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class APIAuthenticateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {

            ApiObj response;
            try
            {
                //StationId
                var stationIdValue = filterContext.Request.Headers.GetValues("StationId");
                //EnrollerRegId
                var enrollerRegIdValue = filterContext.Request.Headers.GetValues("EnrollerRegId");
                //ApiAccessKey
                var apiKeyValue = filterContext.Request.Headers.GetValues("APIAccessKey");
                //StationName
                var stationNameValue = filterContext.Request.Headers.GetValues("StationName");
                if (stationIdValue == null || apiKeyValue == null || enrollerRegIdValue == null || stationNameValue == null)
                {
                    response = new ApiObj
                    {
                        IsSuccessful = false,
                        ErrorMessage = "Empty / Invalid Authentication Parameter Was Encountered",
                        StationInfo = new UploadClientStation()
                    };
                    filterContext.Request.Properties.Add("AuthResponse", response);
                    return;
                }

                var apiStationId = stationIdValue.FirstOrDefault();
                var stationKey = apiKeyValue.FirstOrDefault();
                var enrollerRegId = enrollerRegIdValue.FirstOrDefault();
                var stationName = stationNameValue.FirstOrDefault();
                if (string.IsNullOrEmpty(apiStationId) || string.IsNullOrEmpty(stationKey) ||
                    string.IsNullOrEmpty(stationName))
                {
                    response = new ApiObj
                    {
                        IsSuccessful = false,
                        ErrorMessage = "Empty / Invalid Authentication Parameter Was Encountered",
                        StationInfo = new UploadClientStation()
                    };
                    filterContext.Request.Properties.Add("AuthResponse", response);
                    return;
                }

                string msg;
                //var stationInfo = ServiceProvider.Instance()
                //    .GetClientStationServices()
                //    .ValidateClientAccess(apiStationId, stationName, stationKey, out msg);
                var stationInfo = ServiceProvider.Instance()
                    .GetClientStationServices()
                    .ValidateUploadClientAccess(apiStationId, stationName, stationKey, out msg);
                if (stationInfo == null || stationInfo.ClientStationId < 1)
                {
                    response = new ApiObj
                    {
                        IsSuccessful = false,
                        ErrorMessage = "Unable to authenticate request",
                        StationInfo = new UploadClientStation()
                    };
                    filterContext.Request.Properties.Add("AuthResponse", response);
                    return;
                }

                var enrollerInfo = ServiceProvider.Instance()
                    .GetEnrollerServices()
                    .GetEnrollerByRegId(enrollerRegId);
                if (enrollerInfo == null || enrollerInfo.EnrollerId < 1)
                {
                    response = new ApiObj
                    {
                        IsSuccessful = false,
                        ErrorMessage = "Unable to authenticate request! Operator does not exist",
                        StationInfo = new UploadClientStation()
                    };
                    filterContext.Request.Properties.Add("AuthResponse", response);
                    return;
                }

                stationInfo.EnrollerRegId = enrollerInfo.EnrollerRegId;
                stationInfo.EnrollerId = enrollerInfo.EnrollerId;
                response = new ApiObj
                {
                    IsSuccessful = true,
                    StationInfo = stationInfo
                };
                filterContext.Request.Properties.Add("AuthResponse", response);

            }
            catch (Exception ex)
            {
                response = new ApiObj
                {
                    IsSuccessful = false,
                    ErrorMessage = "Authentication Failed!",
                    TechErrorMessage = ex.Message,
                    StationInfo = new UploadClientStation()
                };
                filterContext.Request.Properties.Add("AuthResponse", response);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    
}