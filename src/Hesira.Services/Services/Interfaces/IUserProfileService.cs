using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Models.User;

namespace Hesira.Services.Services.Interfaces
{
    public interface IUserProfileService
    {

        UserProfileModel GetProfileForUser(long userId);
    }
}
