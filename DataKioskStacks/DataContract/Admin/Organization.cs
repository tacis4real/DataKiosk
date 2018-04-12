using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataKioskStacks.DataContract.ContractHelpers;

namespace DataKioskStacks.DataContract.Admin
{

    [Table("EnrollKiosk.Organization")]
    public class Organization
    {

        public Organization()
        {
            Users = new HashSet<User>();
            ClientStations = new HashSet<ClientStation>();
            //Enrollers = new HashSet<Enroller>();
        }


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
        public DateTime TimeStampRegistered { get; set; }
        public int Status { get; set; }

        [CheckNumber(0, ErrorMessage = @"Admin User Information is required")]
        public long RegisteredByUserId { get; set; }


        //public virtual ICollection<Enroller> Enrollers { get; set; }
        public virtual ICollection<ClientStation> ClientStations { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
