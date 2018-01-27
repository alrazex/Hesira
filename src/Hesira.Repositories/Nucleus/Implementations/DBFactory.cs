using System;
using Hesira.Entities;
using Hesira.Repositories.Nucleus.Interfaces;

namespace Hesira.Repositories.Nucleus.Implementations
{
    public class DBFactory : IDBFactory, IDisposable
    {
        private HesiraEntities db;

        // implement IDisposable 
        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }

        // current context or create another
        public HesiraEntities Create()
        {
            return db ?? (db = new HesiraEntities());
        }

    }
}

