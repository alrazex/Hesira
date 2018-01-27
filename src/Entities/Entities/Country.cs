//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hesira.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Country
    {
        public Country()
        {
            this.Addresses = new HashSet<Address>();
            this.UsersProfiles = new HashSet<UsersProfile>();
        }
    
        public int Id { get; set; }
        public string NumericalCode { get; set; }
        public string CountryNameLowerCase { get; set; }
        public string CountryNameUpperCase { get; set; }
        public string ISO_ALPHA3 { get; set; }
        public string ISO_ALPHA2 { get; set; }
    
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<UsersProfile> UsersProfiles { get; set; }
    }
}