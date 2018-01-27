using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Models.Address;

namespace Hesira.Services.Services.Interfaces
{
    public interface IAddressService
    {

        AddressModel GetAddressForUser(long userId);

    }
}
