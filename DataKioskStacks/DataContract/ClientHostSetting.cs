using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.DataContract.ContractHelpers;

namespace DataKioskStacks.DataContract
{

    [Table("EnrollKiosk.ClientStation")]
    public class ClientStation
    {
        public ClientStation()
        {
            Beneficiaries = new HashSet<Beneficiary>();
            //Enrollers = new HashSet<Enroller>();
        }
        public long ClientStationId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = @"Organization Info is required")]
        [CheckNumber(0, ErrorMessage = @"Invalid Organization Information")]
        public long OrganizationId { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Station name is required")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = @"Station name must be between 5 and 150 characters")]
        [Display(Name=@"Station Name")]
        public string StationName { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Unable to generate Station ID", AllowEmptyStrings = false)]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = @"Invalid Station ID")]
        [Index("IX_Station_Id", IsUnique = true)]
        [Display(Name = @"Station ID")]
        public string StationId { get; set; }

        //[Column(TypeName = "varchar")]
        //[Required(ErrorMessage = @"Unable to generate Station Key", AllowEmptyStrings = false)]
        //[StringLength(15, MinimumLength = 15, ErrorMessage = @"Invalid Station Key")]
        //[Index("IX_Station_Key", IsUnique = true)]
        //[Display(Name = @"Station Key")]
        //public string StationKey { get; set; }

        [Column(TypeName = "varchar")]
        [Required(ErrorMessage = @"Unable to generate Access Key", AllowEmptyStrings = false)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = @"Invalid Access Key")]
        [Index("IX_APIAccessKey_Key", IsUnique = true)]
        [Display(Name = @"API Access Key")]
        public string APIAccessKey { get; set; }

        //[Column(TypeName = "varchar")]
        //[StringLength(50)]
        //[Display(Name = @"Device Id")]
        //public string DeviceId { get; set; }

        //[Column(TypeName = "varchar")]
        //[Required(ErrorMessage = @"Assignee's full name is required", AllowEmptyStrings = false)]
        //[StringLength(150, MinimumLength = 8, ErrorMessage = @"Assignee's full name must be between 8 and 150 characters")]
        //[Display(Name = @"Assignee  Name")]
        //public string AssigneeFullName { get; set; }

        [Required(ErrorMessage = @"Station Location State is required", AllowEmptyStrings = false)]
        [CheckNumber(0, ErrorMessage = @"Invalid Station Location State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = @"Station Location Local Government Area is required", AllowEmptyStrings = false)]
        [CheckNumber(0, ErrorMessage = @"Invalid Station Local Government Area")]
        public int LocalAreaId { get; set; }

        //[Column(TypeName = "varchar")]
        //[Required(ErrorMessage = @"Assignee's mobile number is required", AllowEmptyStrings = false)]
        //[StringLength(15, MinimumLength = 7, ErrorMessage = @"Assignee's mobile must be between 7 and 15 digits")]
        //[Display(Name = @"Assignee Mobile No")]
        //public string AssigneeMobileNumber { get; set; }

        //[Column(TypeName = "varchar")]
        //[StringLength(15)]
        //[Display(Name = @"Device IP")]
        //public string DeviceIP { get; set; }

        public DateTime TimeStampRegistered { get; set; }

        //[CheckNumber(0, ErrorMessage = @"Admin User Information is required")]
        public long RegisteredByUserId { get; set; }
        public int Status { get; set; }



        public virtual Organization Organization { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
        //public virtual ICollection<Enroller> Enrollers { get; set; }
        public virtual LocalArea LocalArea { get; set; }
    }
}
