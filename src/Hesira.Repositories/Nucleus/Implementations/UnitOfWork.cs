using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Entities;
using Hesira.Repositories.Nucleus.Interfaces;

namespace Hesira.Repositories.Nucleus.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IDBFactory _dbFactory;
        private readonly IsolationLevel _isolationLevel = IsolationLevel.Serializable;
        private DbContext _dbContext;
        private DbContextTransaction _transaction;
        private bool _disposed;

        protected DbContext DBContext
        {
            get
            {
                return _dbContext ?? (_dbContext = _dbFactory.Create());
            }
        }

        public UnitOfWork(IDBFactory dbFactory)
        {
            _dbFactory = dbFactory;
            _isolationLevel = IsolationLevel.ReadCommitted;
        }

        public void BeginTransaction()
        {
            _transaction = DBContext.Database.BeginTransaction(_isolationLevel);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            _transaction = DBContext.Database.BeginTransaction(_isolationLevel);

        }

        public void Rollback()
        {
            if (_transaction != null)
            {

                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
            else
            {
                 throw new Exception("You are not in a transaction");
            }

          
        }

        public void Commit()
        {
            if (_transaction != null)
            {

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;

            }
            else
            {
                
                throw new Exception("You are not in a transaction");
            }


        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }

                    DBContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
