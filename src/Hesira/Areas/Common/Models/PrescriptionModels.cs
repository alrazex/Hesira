using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hesira.Models;

namespace Hesira.Areas.Common.Models
{
    public class PrescriptionModel
    {
        public long Id { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public LightUserViewModel Patient { get; set; }
        public LightUserViewModel Doctor { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public List<DrugDiseaseModel> AssociatedDrugDisease { get; set; } 
    }
}