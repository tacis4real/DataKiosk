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
    public class ClientStationService
    {

        private readonly ClientStationRepository _clientStationRepository;

        public ClientStationService()
        {
            _clientStationRepository = new ClientStationRepository();
        }

        public long AddClientStation(ClientStation station, out string msg)
        {
            try
            {
                return _clientStationRepository.AddClientStation(station, out msg);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return 0;
            }
        }

        public string GenerateOperatorId(long orgId, long stationId, out string msg)
        {
            try
            {
                return _clientStationRepository.GenerateOperatorId(orgId, stationId, out msg);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return "";
            }
        }

        public string GenerateSIN(out string msg, string stationId, int orgId)
        {
            try
            {
                return _clientStationRepository.GenerateSIN(out msg, stationId, orgId);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return "";
            }
        }
        public string AddClientStation2(out string msg, string stationId, int orgId)
        {
            try
            {
                return _clientStationRepository.GenerateSIN(out msg, stationId, orgId);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return "";
            }
        }

        public StationRespObj RegisterStation(StationRegObj corporateInfo)
        {
            try
            {
                return _clientStationRepository.RegisterStation(corporateInfo);
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

        public ClientStation ValidateClientAccess(string stationId, string stationName, string accessKey, out string msg)
        {
            try
            {
                return _clientStationRepository.ValidateClientAccess(stationId, stationName, accessKey, out msg);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

        public UploadClientStation ValidateUploadClientAccess(string stationId, string stationName, string accessKey, out string msg)
        {
            try
            {
                return _clientStationRepository.ValidateUploadClientAccess(stationId, stationName, accessKey, out msg);
            }
            catch (Exception ex)
            {
                msg = "Processing Error Occurred! Please try again later";
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

    }
}
