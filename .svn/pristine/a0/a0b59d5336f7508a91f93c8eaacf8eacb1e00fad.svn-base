using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.APIServer.APIObjs;
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.Repository;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Service
{
    public class OrganizationService
    {

        private readonly OrganizationRepository _organizationRepository;

        public OrganizationService()
        {
            _organizationRepository = new OrganizationRepository();
        }


        public long AddOrganization(Organization organization, out string msg)
        {
            try
            {
                return _organizationRepository.AddOrganization(organization, out msg);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return 0;
            }
        }

        public ResponseStatus UpdateOrganization(Organization organizationRegObj)
        {
            var response = new ResponseStatus
            {
                IsSuccessful = false,
                Message = new ResponseMessage(),
                ReturnedId = 0
            };

            try
            {
                return _organizationRepository.UpdateOrganization(organizationRegObj);
            }
            catch (Exception ex)
            {
                response.Message.FriendlyMessage = "Processing Error Occurred! Please try again later";
                response.Message.TechnicalMessage = "Error: " + ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return response;
            }
        }



        public List<RegisteredOrganizationReportObj> GetOrganizationListObjs()
        {
            try
            {
                return _organizationRepository.GetOrganizationListObjs();
            }
            catch (Exception ex)
            {
                //msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new List<RegisteredOrganizationReportObj>();
            }
        }



    }
}
