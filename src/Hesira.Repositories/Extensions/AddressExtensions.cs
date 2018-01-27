using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Entities;
using Hesira.Models.Address;

namespace Hesira.Repositories.Extensions
{
    public static class AddressExtensions
    {


        public static void ToDomainModel(this Address dbModel, AddressModel domainModel)
        {
            domainModel.Id = dbModel.Id_Address;
            domainModel.CountryId = dbModel.Id_Country;
            domainModel.PhysicalAddress = dbModel.PhysicalAddress;
        }

        public static void ToDatabaseModel(this AddressModel domainModel, Address dbModel)
        {
            dbModel.Id_Country = domainModel.CountryId;
            dbModel.PhysicalAddress = domainModel.PhysicalAddress;
        }

    }
}
