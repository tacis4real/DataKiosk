using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataKioskStacks.DataContract.ContractHelpers;

namespace DataKioskStacks.DataContract
{


    [Table("EnrollKiosk.LocalArea")]
    public class LocalArea
    {

        public LocalArea()
        {
            ClientStations = new HashSet<ClientStation>();
        }

        public long LocalAreaId { get; set; }

        //[Required(ErrorMessage = "State Information is required")]
        //[CheckNumber(0, ErrorMessage = "Invalid State Information")]
        public int StateId { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }


        public ICollection<ClientStation> ClientStations { get; set; }
        //public virtual State State { get; set; }
    }
}
