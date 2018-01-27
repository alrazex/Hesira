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
    
    public partial class Prescription
    {
        public Prescription()
        {
            this.Appointments = new HashSet<Appointment>();
            this.PrescriptionsDrugs = new HashSet<PrescriptionsDrug>();
        }
    
        public long Id { get; set; }
        public string Series { get; set; }
        public long Number { get; set; }
        public long Id_Patient { get; set; }
        public System.DateTime PrescriptionDate { get; set; }
        public long Id_Doctor { get; set; }
    
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual User PrescriptionsDoctor { get; set; }
        public virtual User PrescriptionsPatient { get; set; }
        public virtual ICollection<PrescriptionsDrug> PrescriptionsDrugs { get; set; }
    }
}