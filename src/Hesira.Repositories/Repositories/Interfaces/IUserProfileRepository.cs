using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Models.User;
using Hesira.Repositories.Nucleus.Interfaces;

namespace Hesira.Repositories.Repositories.Interfaces
{
    public interface IUserProfileRepository : IEntityRepository<UserProfileModel>
    {
        UserProfileModel GetProfileForUser(long userId);
        long InsertOrUpdate(UserProfileModel model);
    }
}
