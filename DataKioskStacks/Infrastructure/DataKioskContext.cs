using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.DataManager;
using DataKioskStacks.Infrastructure.Contract;

namespace DataKioskStacks.Infrastructure
{
    public class DataKioskContext : IDataKioskContext
    {

        public DataKioskContext()
        {
            DataKioskDbContext = new DataKioskEntities();
            DataKioskDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public void Dispose()
        {
            DataKioskDbContext.Dispose();
        }

        public DbContext DataKioskDbContext { get; private set; }
    }
}
