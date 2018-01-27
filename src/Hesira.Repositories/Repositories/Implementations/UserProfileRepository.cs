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
    public class UserProfileRepository : BaseContextRepository, IUserProfileRepository
    {
        #region Constructor
        public UserProfileRepository(IDBFactory dbFactory)
            : base(dbFactory)
        {
        }
        #endregion

        public UserProfileModel GetProfileForUser(long userId)
        {
            throw new NotImplementedException();
        }

        public long InsertOrUpdate(UserProfileModel model)
        {
            var userProfile = new UsersProfile();

            if (model.Id > 0)
            {
                userProfile = db.UsersProfiles.SingleOrDefault(x => x.Id == model.Id);

                model.ToDatabaseModel(userProfile);
            }
            else
            {
                userProfile.Timestamp = DateTime.Now;
                model.ToDatabaseModel(userProfile);
                db.UsersProfiles.Add(userProfile);
            }

            db.SaveChanges();

            return userProfile.Id;
        }
    }
}
