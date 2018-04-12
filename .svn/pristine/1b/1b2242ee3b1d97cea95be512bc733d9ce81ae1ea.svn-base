using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.DataContract.Admin;
using DataKioskStacks.Infrastructure;
using DataKioskStacks.Infrastructure.Contract;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Repository
{
    internal class UserTypeRepository
    {

        private readonly IDataKioskRepository<UserType> _repository;
        private readonly DataKioskUoWork _uoWork;

        public UserTypeRepository()
        {
            _uoWork = new DataKioskUoWork();
            _repository = new DataKioskRepository<UserType>(_uoWork);
        }


        public UserType GetUserType(int userTypeId, out string msg)
        {
            try
            {
                msg = "";
                var myItem = _repository.GetById(userTypeId);
                if (myItem == null || myItem.UserTypeId < 1) { return null; }
                return myItem;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return null;
            }
        }

    }
}
