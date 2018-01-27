using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Models.Address;
using Hesira.Models.User;

namespace Hesira.Services.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAll();
        long Create(UserModel userModel, UserProfileModel userProfileModel, AddressModel userAddressModel);
    }
}
