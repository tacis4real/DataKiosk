using System;
using System.Collections.Generic;
using DataKioskStacks.DataContract.ContractHelpers;

namespace DataKioskStacks.APIServer.APIObjs
{

    #region Portal User

    public class UserLoginResponseObj
    {

        public long UserId { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string NewPassword { get; set; }

        public ResponseStatus Status;
        public string AuthToken { get; set; }
        public string Othernames { get; set; }
        public string Surname { get; set; }
        public int RoleId { get; set; }
        public bool IsFirstTimeAccess { get; set; }
        public string Username { get; set; }

    }


    public class UserRegResponse
    {
        public long UserId { get; set; }
        public string MobileNumber { get; set; }
        public string Surname { get; set; }
        public string Othernames { get; set; }
        public string Email { get; set; }

        public ResponseStatus Status;
    }

    public class UserResponseObj
    {

        public long UserId { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string NewPassword { get; set; }
        public ResponseStatus Status;
        public string AuthToken { get; set; }

    }


    public class UserDeviceResponseObj
    {
        public long UserId { get; set; }
        public long UserDeviceId { get; set; }
        public string MobileNumber { get; set; }
        public string AuthorizationCode { get; set; }
        public ResponseStatus Status;
    }

    public class AdminTaskResponseObj
    {
        public long AdminUserId { get; set; }
        public long BeneficiaryUserId { get; set; }
        public string NewPassword { get; set; }
        public ResponseStatus Status;
    }

    public class RegisteredUserReportObj
    {
        public long UserId { get; set; }
        public long OrganizationId { get; set; }
        public int UserTypeId { get; set; }
        public int RoleId { get; set; }

        public string MyRole { get; set; }
        public string UserType { get; set; }
        public string Organization { get; set; }

        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsApproved { get; set; }
        public int SexId { get; set; }
        public string SelectedRoles { get; set; }
        public string[] MyRoles { get; set; }
        public int[] MyRoleIds { get; set; }
        public string PasswordChangeTimeStamp { get; set; }
        public string LastLoginTimeStamp { get; set; }
        public string LastLockedOutTimeStamp { get; set; }
        public int FailedPasswordCount { get; set; }
        public string TimeStampRegistered { get; set; }
        public bool IsPasswordChangeRequired { get; set; }

    }

    #endregion


    public class ReportParamObj
    {
        public long CustomerId;
        public string StartDate;
        public string StopDate;
        public string OrderNumber;
        public string TransactionId;
        public int Status;
    }

    public class ResponseStatus
    {
        public bool IsSuccessful;
        public ResponseMessage Message;
        public long ReturnedId;
        public string ReturnedValue;
    }

    public class ResponseMessage
    {
        public string FriendlyMessage;
        public string TechnicalMessage;
        public string MessageId;
    }

    public class BusinessRegResponseObj
    {
        public long BusinessInfoRemoteId;
        public long BusinessInfoId;
        public long BusinessOwnerInfoRemoteId;
        public long BusinessOwnerInfoId;
        public string TaxReferenceNumber;
        public string BusinessName;
        public string MobileNumber;
        public ResponseStatus Status;
    }

    public class BulkBusinessRegResponseObj
    {
        public List<BusinessRegResponseObj> BusinessRegResponses;
        public ResponseStatus MainStatus;
    }
    

    public class StationRespObj
    {
        public string APIAccessKey;
        public string DeviceId;
        public string StationId;
        public string StationName;
        //public string StationAddress;
        public string ResidentialAddress;
        public long ClientStationId;
        public long EnrollerId;
        //public long OrganizationId;
        public string EnrollerRegId;
        public string Surname;
        public string FirstName;
        public string Othernames;
        public int Sex;
        public string UserName;
        public string MobileNumber;
        public string Email;
        public int EnrollerStatus;
        public int StationStatus;


        public ResponseStatus ResponseStatus;
    }


    #region Upload Response

    public class BeneficiaryRegResponseObj
    {
        public long BeneficiaryRemoteId;
        public long BeneficiaryId;
        public string MobileNumber;
        public int RecordId;
        public ResponseStatus Status;
    }

    public class BulkBeneficiaryRegResponseObj
    {
        public List<BeneficiaryRegResponseObj> BeneficiaryRegResponses;
        public ResponseStatus MainStatus;
    }


    #endregion

}
