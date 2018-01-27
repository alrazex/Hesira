using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BForms.Models;
using BForms.Mvc;
using Hesira.Resources;

namespace Hesira.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "CNP", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string CNP { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.Password)]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.CheckBox)]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}