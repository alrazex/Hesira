using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Models.User;
using Hesira.Repositories.Nucleus.Interfaces;
using Hesira.Repositories.Repositories.Interfaces;
using Hesira.Services.Nucleus.Implementations;
using Hesira.Services.Services.Interfaces;

namespace Hesira.Services.Services.Implementations
{
    public class UserProfileService : BaseService, IUserProfileService
    {

        #region Constructor & Properties 

        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserRepository userRepository, IAddressRepository addressRepository,
            IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._addressRepository = addressRepository;
            this._userProfileRepository = userProfileRepository;
            this._userRepository = userRepository;
        }

        #endregion

        public UserProfileModel GetProfileForUser(long userId)
        {
            return _userProfileRepository.GetProfileForUser(userId);
        }
    }
}
