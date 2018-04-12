using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataKioskStacks.Infrastructure.Contract;

namespace DataKioskStacks.Infrastructure
{
    public class DataKioskUoWork : IDataKioskUoWork, IDisposable
    {

        private readonly DataKioskContext _dbContext;

        public DataKioskUoWork(DataKioskContext context)
        {
            _dbContext = context;
        }

        public DataKioskUoWork()
        {
            _dbContext = new DataKioskContext();
        }

        public DbContextTransaction BeginTransaction()
        {
            return _dbContext.DataKioskDbContext.Database.BeginTransaction();
        }


        public void SaveChanges()
        {
            _dbContext.DataKioskDbContext.SaveChanges();
        }

        public DataKioskContext Context { get { return _dbContext; } }


        #region Implementation of IDispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_disposed) return;
            _dbContext.Dispose();
            _disposed = true;
        }

        private bool _disposed;

        ~DataKioskUoWork()
        {
            Dispose(false);
        }

        #endregion


        
    }
}
