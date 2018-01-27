using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BForms.Models;
using BForms.Mvc;
using Hesira.Models;
using Hesira.Resources;

namespace Hesira.Areas.Common.Models
{
    public class NewAppoitmentModel
    {

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Doctor", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> DoctorsDropdown { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Date", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DatePicker)]
        public BsDateTime DatePicker { get; set; }


    }

    public class AppointmentsPageModel
    {
        public NewAppoitmentModel NewAppoitmentModel { get; set; }
        public AppointmentsListModel AppointmentsListModel { get; set; }
    }

    public class AppointmentsListModel
    {
        public LightUserViewModel UserLightModel { get; set; }
        public List<AppointmentLightModel> Appointments { get; set; }
    }

    public class AppointmentModel
    {
        public LightUserViewModel PatientOrDoctorModel { get; set; }
        public long Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MedicalTrace { get; set; }
        public AppointmentState State { get; set; }
        public PrescriptionModel Prescription { get; set; }
    }

    public class AppointmentLightModel
    {
        public LightUserViewModel PatientOrDoctorModel { get; set; }
        public long Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class EventModel
    {
        public string title { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public bool allDay { get; set; }
        public string id { get; set; }
    }

    public enum AppointmentState
    {
        Pending = 1,
        Finished = 2,
    }

}