using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AwesomeMvc;
using DataKioskStacks.DataContract.ContractHelpers;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace EnrollmentDataKiosk.Models.PortalModel
{
    
    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = @"Password and Confirm Password must match.")]
    public class AuthPortalUser
    {

            [DisplayName(@"Last Name")]
            [Required(ErrorMessage = @"* Required")]
            [StringLength(50)]
            public string LastName { get; set; }

            [DisplayName(@"First Name")]
            [Required(ErrorMessage = @"* Required")]
            [StringLength(50)]
            public string FirstName { get; set; }


            //[DisplayName(@"Middle Name")]
            //[StringLength(50)]
            //public string MiddleName { get; set; }

            [Required(ErrorMessage = @"Mobile Number is required", AllowEmptyStrings = false)]
            [StringLength(15, MinimumLength = 7, ErrorMessage = @"Mobile number must be between 7 amd 15 digits")]
            //[CheckMobileNumber(ErrorMessage = "Invalid Mobile Number")]
            public string MobileNo { get; set; }

            [DataType(DataType.EmailAddress)]
            [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = @"This email address is not valid.")]
            [Required(ErrorMessage = @"* Required")]
            [StringLength(50)]
            public string Email { get; set; }

            public int FailedPasswordCount { get; set; }
           
            public bool IsApproved { get; set; }
            public bool IsDeleted { get; set; }
            public bool IsFirstTimeLogin { get; set; }
            public bool IsLockedOut { get; set; }

            [StringLength(35)]
            public string LastLockedOutTimeStamp { get; set; }

            [StringLength(35)]
            public string LastLoginTimeStamp { get; set; }
            
            public string LastPasswordChangeTimeStamp { get; set; }

            [UIHint("MultiLookup")]
            public IEnumerable<int> Roles { get; set; }

            public List<NameAndValueObject> AllRoles { get; set; }

            public int[] MyRoleIds { get; set; }

            public string[] MyRoles { get; set; }


            [DataType(DataType.Password)]
            [Required(ErrorMessage = @"* Required")]
            [StringLength(150)]
            [ValidatePasswordLength(ErrorMessage = @"Password length must be at least 8 character long")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = @"* Required")]
            [StringLength(150)]
            [ValidatePasswordLength(ErrorMessage = @"Password length must be at least 8 character long")]
            public string ConfirmPassword { get; set; }

            public string SelectedRoles { get; set; }

            public string Sex
            {
                get
                {
                    if (SexId < 1)
                    {
                        return "";
                    }
                    return SexId == 1 ? "Male" : "Female";
                }
            }

            [DisplayName(@"Gender")]
            [Required(AllowEmptyStrings = false, ErrorMessage = @"Gender Info is required")]
            [CheckNumber(0, ErrorMessage = @"Invalid Gender Information")]
            //[UIHint("Odropdown")]
            //[AweUrl(Action = "GetSexes", Controller = "ODropDownData")]
            public int SexId { get; set; }

            [StringLength(35)]
            public string TimeStampRegistered { get; set; }

            public long UserId { get; set; }



            [DisplayName(@"Organizations")]
            [Required(AllowEmptyStrings = false, ErrorMessage = @"Organization Info is required")]
            [CheckNumber(0, ErrorMessage = @"Invalid Organization Information")]
            public long OrganizationId { get; set; }

            [DisplayName(@"User Types")]
            [Required(AllowEmptyStrings = false, ErrorMessage = @"User Type Info is required")]
            [CheckNumber(0, ErrorMessage = @"Invalid User Type Information")]
            public int UserTypeId { get; set; }

            [Required(AllowEmptyStrings = false, ErrorMessage = @"Role Info is required")]
            [CheckNumber(0, ErrorMessage = @"Invalid Role Information")]
            public int RoleId { get; set; }




            //[Required(ErrorMessage = @"* Required")]
            [StringLength(50)]
            [Display(Name = @"Username")]
            public string UserName { get; set; }

            public string Action { get; set; }

    }

}