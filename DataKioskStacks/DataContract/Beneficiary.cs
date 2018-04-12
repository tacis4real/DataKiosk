using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract.ContractHelpers;
using DataKioskStacks.ValResource;
using Newtonsoft.Json;

namespace DataKioskStacks.DataContract
{


    [Table("EnrollKiosk.Beneficiary")]
    public class Beneficiary
    {

        public long BeneficiaryId { get; set; }
        public long BeneficiaryRemoteId { get; set; }
        public int RecordId { get; set; }
        public long ClientStationId { get; set; }
        public long EnrollerId { get; set; }

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

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Occupation_is_required")]
        [CheckNumber(0, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = @"Invalid_Occupation_Information")]
        public int OccupationId { get; set; }
        public string RemoteImageFileName { get; set; }
        public string ImageFileName { get; set; }
        public string ImagePath { get; set; }
        public byte[] FingerPrintTemplates { get; set; }
        //public List<byte[]> FingerPrintTemplates { get; set; }

        #region Old Bio Data
        //internal string _Biometric { get; set; }

        //[NotMapped]
        //public BiometricData BeneficiaryBiometricDetail
        //{
        //    get { return _Biometric == null ? null : JsonConvert.DeserializeObject<BiometricData>(_Biometric); }
        //    set { _Biometric = JsonConvert.SerializeObject(value); }
        //}

        #endregion

        public DateTime TimeStampRegistered { get; set; }
        public RegStatus Status { get; set; }


        public int SurnameHashed { get; set; }
        public int FirstNameHashed { get; set; }
        public int OtherNameHashed { get; set; }
        public int DateOfBirthHashed { get; set; }
        public int MobileNoHashed { get; set; }



        #region Navigation Properties
        public virtual ClientStation ClientStation { get; set; }
        public virtual Enroller Enroller { get; set; }
        #endregion


    }
}
