using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hesira.Areas.Common.Models
{
    public class DrugDiseaseModel
    {
        public LightDrugModel DrugModel { get; set; }
        public int Quantity { get; set; }
        public int Days { get; set; }
        public LightDiseaseModel DiseaseModel { get; set; }
    }

    public class LightDrugModel
    {
        public long Id { get; set; }
        public string CommercialName { get; set; }
        public string Vendor { get; set; }
        public string ReductionClass { get; set; }
    }
}