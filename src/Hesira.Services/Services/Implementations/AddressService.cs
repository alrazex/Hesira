using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Models.Address;
using Hesira.Repositories.Nucleus.Interfaces;
using Hesira.Repositories.Repositories.Interfaces;
using Hesira.Services.Nucleus.Implementations;
using Hesira.Services.Services.Interfaces;

namespace Hesira.Services.Services.Implementations
{
    public class AddressService : BaseService, IAddressService
    {

        #region Constructor & Properties

        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public AddressService(IUserRepository userRepository, IAddressRepository addressRepository,
            IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            this._addressRepository = addressRepository;
            this._userProfileRepository = userProfileRepository;
            this._userRepository = userRepository;
        }

        #endregion

        public AddressModel GetAddressForUser(long userId)
        {
            return _addressRepository.GetAddressForUser(userId);
        }
    }
}

