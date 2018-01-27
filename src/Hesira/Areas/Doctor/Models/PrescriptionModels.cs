using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BForms.Models;
using BForms.Mvc;
using Hesira.Resources;

namespace Hesira.Areas.Doctor.Models
{
    public class PrescriptionsPageModel
    {
        public PrescriptionNewModel NewPrescriptionModel { get; set; }
    }

    public class PrescriptionNewModel
    {
        public PrescriptionBuilderModel PrescriptionBuilderModel { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Appointments", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> Appointments { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Disease", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> Diseases { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "MedicalTrace", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextArea)]
        public string MedicalTrace { get; set; }

    }
}