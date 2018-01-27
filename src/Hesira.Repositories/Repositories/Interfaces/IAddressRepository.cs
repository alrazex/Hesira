using System;
using System.Collections.Generic;
using System.Linq;
using Hesira.Models.Address;
using Hesira.Repositories.Nucleus.Interfaces;

namespace Hesira.Repositories.Repositories.Interfaces
{
    public interface IAddressRepository : IEntityRepository<AddressModel>
    {
        AddressModel GetAddressForUser(long userId);
        long InsertOrUpdate(AddressModel model);
    }
}
