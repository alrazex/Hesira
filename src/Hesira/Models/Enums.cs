using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Hesira.Resources;

namespace Hesira.Models
{

    public enum YesNoEnum
    {
        [Display(Name = "All", ResourceType = typeof(Resource))]
        All = 1,
        [Display(Name = "Yes", ResourceType = typeof(Resource))]
        Yes = 2,
        [Display(Name = "No", ResourceType = typeof(Resource))]
        No = 3,
    }

}