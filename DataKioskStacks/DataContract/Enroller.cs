using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.DataContract.ContractHelpers;

namespace DataKioskStacks.DataContract
{

    [Table("EnrollKiosk.Enroller")]
    public class Enroller
    {

        public Enroller()
        {
            Beneficiaries = new HashSet<Beneficiary>();
        }
        

        public long EnrollerId { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = @"Organization Info is required")]
        //[CheckNumber(0, ErrorMessage = @"Invalid Organization Information")]
        //public long OrganizationId { get; set; }

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

        [StringLength(20, ErrorMessage = @"Username is too short or too long", MinimumLength = 8)]
        [Required(AllowEmptyStrings = false, ErrorMessage = @"Username is required")]
        public string UserName { get; set; }

        [DisplayName(@"Password")]
        [StringLength(50)]
        [Required(ErrorMessage = @"* Required")]
        public string Password { get; set; }
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

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Address is required")]
        [DisplayName("Operator Address")]
        public string Address { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [Display(Name = @"Device Id")]
        public string DeviceId { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(15)]
        [Display(Name = @"Device IP")]
        public string DeviceIP { get; set; }
        public DateTime TimeStampRegistered { get; set; }

        [CheckNumber(0, ErrorMessage = @"Admin User Information is required")]
        public long RegisteredByUserId { get; set; }
        public int Status { get; set; }
        public DateTime? TimeStampAuthorized { get; set; }


        //public virtual ClientStation ClientStation { get; set; }
        //public virtual Organization Organization { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }

    }
}
