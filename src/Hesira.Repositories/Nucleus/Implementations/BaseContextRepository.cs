using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Entities;
using Hesira.Repositories.Nucleus.Interfaces;

namespace Hesira.Repositories.Nucleus.Implementations
{
    public abstract class BaseContextRepository
    {
        protected internal HesiraEntities db;

        protected BaseContextRepository(IDBFactory dbFactory)
        {
            db = dbFactory.Create();
        }
    }
}
