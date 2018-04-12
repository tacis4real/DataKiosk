﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using DataKioskStacks.DataContract.ContractHelpers;
using DataKioskStacks.ValResource;

namespace DataKioskStacks.APIServer.APIObjs
{

    #region Portal User

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Password and Confirm Password must match.")]
    public class UserRegistrationObj
    {
        public long UserId { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Surname is required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = @"Surname must be between 2 and 100 characters")]
        public string Surname { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"First Name must are required", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 2, ErrorMessage = @"First Name must be between 2 and 100 characters")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = @"Mobile Number must be between 7 and 15 digits")]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Mobile Number is required")]
        //[CheckMobileNumber(ErrorMessage = "Invalid Mobile Number")]
        public string MobileNumber { get; set; }

        [Range(1, 2)]
        public int Sex { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = @"Invalid Email Address")]
        public string Email { get; set; }

        //[Required(ErrorMessage = @"Username is required")]
        [StringLength(20, ErrorMessage = @"Username cannot be longer than 20 character length")]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.Password)]
        [ValidatePasswordLength(ErrorMessage = @"Password length must be at least 8 character long")]
        public string Password { get; set; }

        [Required]
        [StringLength(150)]
        [ValidatePasswordLength(ErrorMessage = @"Password length must be at least 8 character long")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public int RegisteredByUserId { get; set; }

        [StringLength(50)]
        public string DeviceSerialNumber { get; set; }

        [StringLength(50)]
        public string DeviceName { get; set; }

        public string SelectedRoles { get; set; }
        public int[] MyRoleIds { get; set; }

        public string[] MyRoles { get; set; }
        public bool IsActive { get; set; }



        [Required(AllowEmptyStrings = false, ErrorMessage = @"Organization Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid Organization Information")]
        public long OrganizationId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"User Type Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid User Type Information")]
        public int UserTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"Role Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid Role Information")]
        public int RoleId { get; set; }


    }






    public class UserEditObj
    {

        [Required(ErrorMessage = @"User Id is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid User Information")]
        public int UserId { get; set; }




        [Required(AllowEmptyStrings = false, ErrorMessage = @"Organization Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid Organization Information")]
        public long OrganizationId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"User Type Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid User Type Information")]
        public int UserTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"Role Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid Role Information")]
        public int RoleId { get; set; }




        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Surname is required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = @"Surname must be between 2 and 100 characters")]
        public string Surname { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"First Name must are required", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 2, ErrorMessage = @"First Name must be between 2 and 100 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = @"Sex is required")]
        [Range(1, 2, ErrorMessage = @"Selected Sex is invalid")]
        public int Sex { get; set; }


        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = @"Supplied email address is not valid.")]
        public string Email { get; set; }


        [Required(ErrorMessage = @"Mobile Number is required", AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 7, ErrorMessage = @"Mobile number must be between 7 amd 15 digits")]
        [CheckMobileNumber(ErrorMessage = @"Invalid Mobile Number")]
        public string MobileNumber { get; set; }

        public bool IsApproved { get; set; }


        [Required(ErrorMessage = @"Invalid Request Information")]
        [CheckNumber(0, ErrorMessage = @"Invalid Registrar Information")]
        public int RegisteredByUserId { get; set; }

        [Required(ErrorMessage = @"Device Id is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = @"Invalid Device Id")]
        public string DeviceId { get; set; }

        [StringLength(50)]
        public string DeviceName { get; set; }

        [Required(ErrorMessage = @"Invalid Request Session! Please re-login")]
        [StringLength(32, MinimumLength = 32, ErrorMessage = @"Invalid Request Session! Please re-login")]
        public string RegistrarToken { get; set; }


    }

    public class UserDeviceRegObj
    {

        [Required(ErrorMessage = "User is required")]
        [CheckNumber(0, ErrorMessage = "Invalid User Information")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "Device Serial Number is required")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Invalid Serial Number")]
        public string DeviceSerialNumber { get; set; }

        [StringLength(50)]
        public string DeviceName { get; set; }


        [Required(ErrorMessage = "Mobile Number is required", AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Mobile Number must be between 7 and 15 digits")]
        [CheckMobileNumber(ErrorMessage = "Invalid Mobile Number")]
        public string MobileNumber { get; set; }



        [StringLength(600)]
        public string NotificationToken { get; set; }

    }

    public class UserNotificationTokenObj
    {

        [Required(ErrorMessage = "User Id is required")]
        [CheckNumber(0, ErrorMessage = "Invalid User Information")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Invalid User Device Id")]
        [CheckNumber(0, ErrorMessage = "Invalid User Device Id")]
        public int UserDeviceId { get; set; }

        [Required(ErrorMessage = "Device Id is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Invalid Device Id")]
        public string DeviceSerialNumber { get; set; }

        [StringLength(800)]
        [Required(ErrorMessage = "Notification Token is required")]
        public string NotificationToken { get; set; }

    }

    public class UserAuthorizeCodeObj
    {

        [Required(ErrorMessage = "User Id is required")]
        [CheckNumber(0, ErrorMessage = "Invalid User Information")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Invalid User Device Id")]
        [CheckNumber(0, ErrorMessage = "Invalid User Device Id")]
        public int UserDeviceId { get; set; }

        [Required(ErrorMessage = "Device Id is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Invalid Device Id")]
        public string DeviceSerialNumber { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Authorization Code is required")]
        public string AuthorizationCode { get; set; }

    }


    #endregion

    public class OperatorIdGenerationObj
    {
        public long OrganizationId { get; set; }
        public long ClientStationId { get; set; }
    }

    public class OperatorStationRegObj
    {

        [StringLength(20, ErrorMessage = @"Username is too short or too long", MinimumLength = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = @"Device ID is required", AllowEmptyStrings = false)]
        [StringLength(50)]
        public string DeviceId { get; set; }

        [StringLength(20)]
        public string DeviceIP { get; set; }
    }

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Password and Confirm Password must match.")]
    public class EnrollerRegObj
    {

        public long EnrollerId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"Organization Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid Organization Information")]
        public long OrganizationId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"Station Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid Station Information")]
        public long ClientStationId { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Unable to generate Operator Reg ID", AllowEmptyStrings = false)]
        //[StringLength(10, ErrorMessage = @"Invalid Operator Reg ID")]
        [Index("IX_OperatorReg_Id", IsUnique = true)]
        [Display(Name = @"Operator ID")]
        public string EnrollerRegId { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Surname is required", AllowEmptyStrings = false)]
        //StringLength(150, MinimumLength = 8, ErrorMessage = @"Surname must be between 8 and 150 characters")]
        [StringLength(150, ErrorMessage = @"Surname must not more than 150 characters")]
        [Display(Name = @"Surname")]
        public string Surname { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"First name is required", AllowEmptyStrings = false)]
        [StringLength(150, ErrorMessage = @"First name must not more than 150 characters")]
        [Display(Name = @"First Name")]
        public string FirstName { get; set; }

        public string OtherNames { get; set; }

        [Required(ErrorMessage = @"Sex is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid sex")]
        public int Sex { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Address is required")]
        [DisplayName("Operator Address")]
        public string Address { get; set; }


        [StringLength(20, ErrorMessage = @"Username is too short or too long", MinimumLength = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Username is required")]
        public string UserName { get; set; }

        [DisplayName(@"Password")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = @"* Required")]
        public string Password { get; set; }

        [Required]
        [StringLength(150)]
        //[ValidatePasswordLength(ErrorMessage = "Password length must be at least 8 character long")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }

        [ScaffoldColumn(false)]
        public string UserCode { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Enroller's mobile number is required", AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 7, ErrorMessage = @"Enroller's mobile must be between 7 and 15 digits")]
        [Display(Name = @"Mobile No")]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = @"Invalid Email Address")]
        public string Email { get; set; }
    }


    public class OrganizationRegObj
    {

        public long OrganizationId { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Organization name is required", AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 2, ErrorMessage = @"Organization name must be between 2 and 500 characters")]
        [Display(Name = @"Organization Name")]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Organization Location Address is required")]
        [DisplayName(@"Organization Address")]
        public string Address { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = @"Invalid Email Address")]
        public string Email { get; set; }

        [Display(Name = @"Phone Number")]
        public string PhoneNumber { get; set; }
        public string Action { get; set; }
        public DateTime TimeStampRegistered { get; set; }

        public int Status { get; set; }

        //[CheckNumber(0, ErrorMessage = @"Admin User Information is required")]
        public long RegisteredByUserId { get; set; }

    }


    public class StationRegObj
    {

        [StringLength(20, ErrorMessage = @"Username is too short or too long", MinimumLength = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Username is required")]
        public string UserName { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Surname is required", AllowEmptyStrings = false)]
        [StringLength(150, ErrorMessage = @"Surname must not more than 150 characters")]
        [Display(Name = @"Surname")]
        public string Surname { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"First name is required", AllowEmptyStrings = false)]
        [StringLength(150, ErrorMessage = @"First name must not more than characters")]
        [Display(Name = @"First Name")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Enroller's mobile number is required", AllowEmptyStrings = false)]
        [StringLength(15, MinimumLength = 7, ErrorMessage = @"Enroller's mobile must be between 7 and 15 digits")]
        [Display(Name = @"Mobile No")]
        public string MobileNumber { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = @"Invalid Email Address")]
        public string Email { get; set; }

        //[Required(ErrorMessage = @"Station name is required")]
        [StringLength(150, ErrorMessage = @"Station name must not more than 150 characters")]
        public string StationName { get; set; }

        [Required(ErrorMessage = @"Station ID is required", AllowEmptyStrings = false)]
        //[StringLength(15, MinimumLength = 15, ErrorMessage = @"Invalid Station ID")]
        public string StationId { get; set; }

        [Required(ErrorMessage = @"Device ID is required", AllowEmptyStrings = false)]
        [StringLength(50)]
        public string DeviceId { get; set; }

        [StringLength(20)]
        public string DeviceIP { get; set; }
    }

    public class RemoteStationLoginObj
    {
        
        [StringLength(20, ErrorMessage = @"Username is too short or too long", MinimumLength = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Username is required")]
        public string UserName { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = @"* Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = @"Device ID is required", AllowEmptyStrings = false)]
        [StringLength(50)]
        public string DeviceId { get; set; }

        [StringLength(20)]
        public string DeviceIP { get; set; }
    }

    public class BiometricData
    {
        public string ImageFileName { get; set; }
        public string ImagePath { get; set; }
        public List<byte[]> FingerPrintTemplates { get; set; }
        public BiometricData()
        {
            FingerPrintTemplates = new List<byte[]>();
        }
    }


    #region REPORT Listing

    public class RegisteredOrganizationReportObj
    {
        public long OrganizationId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime TimeStampRegistered { get; set; }
        public int Status { get; set; }
        public string StatusType
        {
            get
            {
                //if (Status < 0)
                //{
                //    return "";
                //}
                return Status == 1 ? "Active" : Status == -1 ? "InActive" : "Unknown";
            }
        }
        public long RegisteredByUserId { get; set; }
    }


    #endregion



    




    #region Upload From Remote

    public class BeneficiaryRegMinObj
    {
        public long BeneficiaryId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Surname_is_required", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Surname_is_too_short_or_too_long")]
        public string Surname { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Firstname_is_required", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 2, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"First_name_is_too_short_or_too_long")]
        public string FirstName { get; set; }
        public string Othernames { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Date_of_Birth_is_required", AllowEmptyStrings = false)]
        [StringLength(10, MinimumLength = 10, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Invalid_Date_of_Birth")]
        public string DateOfBirth { get; set; }
        public string MobileNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Residential_Address_is_required")]
        [StringLength(200)]
        public string ResidentialAddress { get; set; }

        [StringLength(200)]
        public string OfficeAddress { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"State_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_State_Information")]
        public int StateId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Local_Government_Area_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Local_Government_Area_Information")]
        public int LocalAreaId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Sex_Info_is_required", AllowEmptyStrings = false)]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Sex")]
        public int Sex { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Marital_Status_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Marital_Status_Information")]
        public int MaritalStatus { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Occupation_is_required")]
        //[CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Occupation_Information")]
        public int OccupationId { get; set; }
        public int Status { get; set; }

        #region Biometrics

        public List<byte[]> FingerPrintTemplate { get; set; }
        public Image Image { get; set; }
        public byte[] ImageByteArray { get; set; }
        public string ImageByteString { get; set; }
        public string ImagePath { get; set; }
        public string ImageFileName { get; set; }

        #endregion

    }

    public class BulkBeneficiaryRegObj
    {
        //[Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Local_Government_Area_is_required")]
        //[CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Invalid_Local_Government_Area_Information")]
        public int LocalAreaId { get; set; }
        public List<BeneficiaryRegMinObj> BeneficiaryRegObjs { get; set; }
    }

    #endregion


    
}
