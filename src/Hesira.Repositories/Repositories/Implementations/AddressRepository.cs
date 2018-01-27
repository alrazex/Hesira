using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Entities;
using Hesira.Models.Address;
using Hesira.Repositories.Extensions;
using Hesira.Repositories.Nucleus.Implementations;
using Hesira.Repositories.Nucleus.Interfaces;
using Hesira.Repositories.Repositories.Interfaces;

namespace Hesira.Repositories.Repositories.Implementations
{
    public class AddressRepository : BaseContextRepository, IAddressRepository
    {
        #region Constructor

        public AddressRepository(IDBFactory dbFactory) : base(dbFactory)
        {
            
        }

        #endregion

        public AddressModel GetAddressForUser(long userId)
        {
            var addressDomainModel = new AddressModel();

            var firstOrDefault = db.UsersProfiles.FirstOrDefault(x => x.Id_User == userId);
            if (firstOrDefault != null)
            {
                firstOrDefault
                      .Address.ToDomainModel(addressDomainModel);
                
            }

            return addressDomainModel;

        }

        public long InsertOrUpdate(AddressModel model)
        {
            var address = new Address();

            if (model.Id > 0)
            {
                address = db.Addresses.SingleOrDefault(x => x.Id_Address == model.Id);

                model.ToDatabaseModel(address);
            }
            else
            {
                model.ToDatabaseModel(address);
                db.Addresses.Add(address);
            }

            db.SaveChanges();

            return address.Id_Address;
        }
    }
}
