using System.ComponentModel.DataAnnotations.Schema;

namespace DataKioskStacks.DataContract.Admin
{

    [Table("EnrollKiosk.UserRole")]
    public  class UserRole
    {
        public int UserRoleId { get; set; }
        public long UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
