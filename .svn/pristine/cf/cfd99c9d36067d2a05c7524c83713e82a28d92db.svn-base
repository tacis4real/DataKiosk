using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract;
using DataKioskStacks.Repository;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Service
{
    public class BeneficiaryService
    {

        private readonly BeneficiaryRepository _beneficiaryRepository;
        public BeneficiaryService()
        {
            _beneficiaryRepository = new BeneficiaryRepository();
        }


        public Beneficiary GetBeneficiary(long id)
        {
            try
            {
                return _beneficiaryRepository.GetBeneficiary(id);
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new Beneficiary();
            }
        }

        public BulkBeneficiaryRegResponseObj AddBulkBeneficiary(BulkBeneficiaryRegObj beneficiaryRegObjs, UploadClientStation station)
        {
            var response = new BulkBeneficiaryRegResponseObj
            {
                MainStatus = new ResponseStatus
                {
                    IsSuccessful = false,
                    Message = new ResponseMessage()
                },
                BeneficiaryRegResponses = new List<BeneficiaryRegResponseObj>()

            };

            try
            {
                return _beneficiaryRepository.AddBulkBeneficiary(beneficiaryRegObjs, station);
            }
            catch (Exception ex)
            {
                response.MainStatus.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                response.MainStatus.Message.TechnicalMessage = "Error: " + ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return response;
            }
        }

    }
}
