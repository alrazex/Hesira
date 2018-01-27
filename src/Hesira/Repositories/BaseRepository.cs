using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hesira.Entities;
using Hesira.Helpers.General;

namespace Hesira.Repositories
{
    public class BaseRepository
    {
        public HesiraEntities db;

        public BaseRepository(HesiraEntities _db)
        {
            this.db = _db;
        }

    }
}