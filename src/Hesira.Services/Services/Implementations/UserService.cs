using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Models.Address;
using Hesira.Models.User;
using Hesira.Repositories.Nucleus.Interfaces;
using Hesira.Repositories.Repositories.Interfaces;
using Hesira.Services.Nucleus.Implementations;
using Hesira.Services.Services.Interfaces;

namespace Hesira.Services.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {

        #region Constructor & Properties

        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserService(IUserRepository userRepository, IAddressRepository addressRepository,
            IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._addressRepository = addressRepository;
            this._userProfileRepository = userProfileRepository;
            this._userRepository = userRepository;
        }

        #endregion

        public IEnumerable<UserModel> GetAll()
        {
            return _userRepository.GetAll();
        }

        public long Create(UserModel userModel, UserProfileModel userProfileModel, AddressModel userAddressModel)
        {

            long? userId = null;

            unitOfWork.BeginTransaction();


            if (userAddressModel != null && userProfileModel != null)
            {
                var addressId = _addressRepository.InsertOrUpdate(userAddressModel);
                userProfileModel.Id_Address = addressId;
            }

            userId = _userRepository.InsertOrUpdate(userModel);

            if (userProfileModel != null)
            {
                userProfileModel.Id_User = userId.Value;
                _userProfileRepository.InsertOrUpdate(userProfileModel);

            }

            unitOfWork.Commit();

            return userId.Value;
        }
    }
}
