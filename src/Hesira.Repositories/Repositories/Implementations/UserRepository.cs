using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Entities;
using Hesira.Models.User;
using Hesira.Repositories.Extensions;
using Hesira.Repositories.Nucleus.Implementations;
using Hesira.Repositories.Nucleus.Interfaces;
using Hesira.Repositories.Repositories.Interfaces;

namespace Hesira.Repositories.Repositories.Implementations
{
    public class UserRepository : BaseContextRepository, IUserRepository
    {
        #region Constructor
        public UserRepository(IDBFactory dbFactory)
            : base(dbFactory)
        {

        }
        #endregion

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public long InsertOrUpdate(UserModel model)
        {
            var user = new User();

            if (model.Id > 0)
            {

                user = db.Users.SingleOrDefault(x => x.Id == model.Id);
                model.ToDatabaseModel(user);

            }
            else
            {
                user.Timestamp = DateTime.Now;
                model.ToDatabaseModel(user);
                db.Users.Add(user);
            }

            db.SaveChanges();

            return user.Id;
        }
    }
}
