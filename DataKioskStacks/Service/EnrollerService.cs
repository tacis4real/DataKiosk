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
    public class EnrollerService
    {

        private readonly EnrollerRepository _enrollerRepository;

        public EnrollerService()
        {
            _enrollerRepository = new EnrollerRepository();
        }

        public long AddEnroller(Enroller enroller, out string msg)
        {
            try
            {
                return _enrollerRepository.AddEnroller(enroller, out msg);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return 0;
            }
        }

        public Enroller GetEnrollerByRegId(string regId)
        {
            try
            {
                return _enrollerRepository.GetEnrollerByRegId(regId);
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new Enroller();
            }
        }

        public StationRespObj RemoteLogin(RemoteStationLoginObj loginObj)
        {
            try
            {
                return _enrollerRepository.RemoteLogin(loginObj);
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return new StationRespObj
                {
                    ResponseStatus = new ResponseStatus
                    {
                        IsSuccessful = false,
                        Message = new ResponseMessage
                        {
                            FriendlyMessage = "Unable to complete your request! Please try again later",
                            TechnicalMessage = "Unable to complete your request! Please try again later"
                        },
                    }
                };
            }
        }
    }
}
