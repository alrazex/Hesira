using System;
using System.Data;

namespace Hesira.Repositories.Nucleus.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        void BeginTransaction();

        void BeginTransaction(IsolationLevel _isolationLevel);

        void Commit();

        void Rollback();
        

    }
}
