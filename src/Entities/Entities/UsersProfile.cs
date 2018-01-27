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
    
    public partial class UsersProfile
    {
        public long Id { get; set; }
        public long Id_User { get; set; }
        public System.DateTime Birthday { get; set; }
        public Nullable<long> Id_Address { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<System.DateTime> LastVisitDate { get; set; }
        public int Gender { get; set; }
        public string Profession { get; set; }
        public int Id_Citizenship { get; set; }
        public Nullable<int> Id_State { get; set; }
        public System.DateTime Timestamp { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual Country Country { get; set; }
        public virtual User User { get; set; }
        public virtual UsersState UsersState { get; set; }
    }
}