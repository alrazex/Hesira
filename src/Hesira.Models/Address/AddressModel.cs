using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesira.Models.Address
{
    public class AddressModel
    {


        public long Id { get; set; }

        public int CountryId { get; set; }

        public string PhysicalAddress { get; set; }


    }
}
