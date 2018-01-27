using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hesira.Areas.Common.Models;

namespace Hesira.Areas.Patient.Models
{
    public class MedicalTraceRow
    {
        public string MedicalTrace { get; set; }

        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }

        public long DoctorId { get; set; }
        public string DoctorName { get; set; }

        public string PrescriptionSeries { get; set; }
        public long PrescriptionNumber { get; set; }
        public long PrescriptionId { get; set; }

        public string Disease { get; set; }

        public List<DrugDiseaseModel> AssociatedDrugDisease { get; set; } 
    }

    public class TracePageModel
    {
        public List<MedicalTraceRow> AppointmentsTrace { get; set; } 
    }
        
}