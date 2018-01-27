using System;
using System.Collections.Generic;
using System.Linq;
using Hesira.Models.User;
using Hesira.Repositories.Nucleus.Interfaces;

namespace Hesira.Repositories.Repositories.Interfaces
{
    public interface IUserRepository : IEntityRepository<UserModel>
    {
       IEnumerable<UserModel> GetAll();
       long InsertOrUpdate(UserModel model);
    }
}
