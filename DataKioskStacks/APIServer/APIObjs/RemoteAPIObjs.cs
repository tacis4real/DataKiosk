using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataKioskStacks.DataContract;
using DataKioskStacks.DataContract.ContractHelpers;

namespace DataKioskStacks.APIServer.APIObjs
{

    public class ApiObj
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string TechErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public UploadClientStation StationInfo { get; set; }
    }

    public class UploadClientStation
    {
        public long ClientStationId;
        public long OrganizationId;
        public string EnrollerRegId;
        public long EnrollerId;
        public string OrganizationName;
        public string StationName;
        public string StationId;
        public string APIAccessKey;
        public int StateId;
        public int LocalAreaId;
        public long RegisteredByUserId;
        public int Status;
    }

    public class RespMessage
    {
        public string ErrorMessage;
        public bool IsSuccessful;
        public string TechErrorMessage;
    }

    public class AuthParameter
    {
        public string StationKey;
        public string AuthKey;
    }

    public class AccessParameter
    {
        //public string StationKey;
        //public string DeviceIdentity;
        //public string DeviceIP;
        //public string StationName;

        public string UserName;
        public string Surname;
        public string FirstName;
        public string MobileNumber;
        public string Email;
        public string StationName;
        public string StationId;
        public string DeviceId;
        public string DeviceIP;
    }

    public class AccessRespObj : RespMessage
    {
        public string StationKey;
        public string StationName;
        public string ApiUser;
        public string AuthKey;
        public string ApiVersion;
        public string ApiKey;
    }
}
