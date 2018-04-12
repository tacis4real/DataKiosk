using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataKioskStacks.DataContract.Admin
{



    [Table("EnrollKiosk.Role")]
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }

        [Column(TypeName = "varchar")]
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Description { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
