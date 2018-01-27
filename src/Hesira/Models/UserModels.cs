using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Hesira.Resources;

namespace Hesira.Models
{

    public class LightUserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRoles Role { get; set; }
        public GenderEnum? Gender { get; set; }
    }

    #region Enums

    public enum UserRoles : byte
    {
        [Display(Name = "All", ResourceType = typeof(Resource))]
        All = 1,
        [Display(Name = "Patient", ResourceType = typeof(Resource))]
        Patient = 2,
        [Display(Name = "Doctor", ResourceType = typeof(Resource))]
        Doctor = 3,
        [Display(Name = "Admin", ResourceType = typeof(Resource))]
        Admin = 4
    }

    public enum GenderEnum
    {
        [Display(Name = "Both", ResourceType = typeof(Resource))]
        Both = 1,
        [Display(Name = "Female", ResourceType = typeof(Resource))]
        Female = 2,
        [Display(Name = "Male", ResourceType = typeof(Resource))]
        Male = 3
    }
    #endregion

}