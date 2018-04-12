using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.DataContract;
using DataKioskStacks.Infrastructure;
using DataKioskStacks.Infrastructure.Contract;
using WebCribs.TechCracker.WebCribs.TechCracker;

namespace DataKioskStacks.Repository
{
    internal class SerialGeneratorRepository
    {

        private static IDataKioskRepository<SerialNumberKeeper> _repository;
        private static DataKioskUoWork _uoWork;

        public SerialGeneratorRepository()
        {
            _uoWork = new DataKioskUoWork();
            _repository = new DataKioskRepository<SerialNumberKeeper>(_uoWork);
        }


        public static long Generate()
        {
            try
            {
                _uoWork = new DataKioskUoWork(/*You can specify you custom context here*/);
                _repository = new DataKioskRepository<SerialNumberKeeper>(_uoWork);

                Purge();

                var processedScratchPin = _repository.Add(new SerialNumberKeeper());
                _uoWork.SaveChanges();
                return processedScratchPin.SerialNumberKeeperId;
            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
                return 0;
            }
        }


        private static void Purge()
        {
            try
            {
                var sql1 = "Select coalesce(Count(\"SerialNumberKeeperId\")) FROM \"EnrollKiosk\".\"SerialNumberKeeper\";";

                var check = _repository.RepositoryContext().Database.SqlQuery<int>(sql1).ToList();
                if (check.IsNullOrEmpty()) { return; }
                if (check[0] > 100)
                {
                    sql1 = "Delete FROM \"EnrollKiosk\".\"SerialNumberKeeper\";";
                    _repository.RepositoryContext().Database.ExecuteSqlCommand(sql1);
                }

            }
            catch (Exception ex)
            {
                BugManager.LogApplicationBug(ex.StackTrace, ex.Source, ex.Message);
            }
        }
    }
}
