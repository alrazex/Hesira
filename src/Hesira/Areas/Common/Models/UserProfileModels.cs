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

    #region ReadOnly

    public class UserProfileModel
    {
        public UserProfileModel()
        {
            Basic = new UserProfileBasicModel();
            UserInfo = new UserProfileInfoModel();
            Contact = new UserProfileContactModel();
        }
    
        [BsPanel(Id = PanelComponentsEnum.Basic, Expandable = false, Editable = false)]
        [Display(Name = "BasicInfo", ResourceType = typeof(Resource))]
        public UserProfileBasicModel Basic { get; set; }

        [BsPanel(Id = PanelComponentsEnum.UserInfo)]
        [Display(Name = "OtherInfo", ResourceType = typeof(Resource))]
        public UserProfileInfoModel UserInfo { get; set; }

        [BsPanel(Id = PanelComponentsEnum.Contact)]
        [Display(Name = "Contact", ResourceType = typeof(Resource))]
        public UserProfileContactModel Contact { get; set; }
    }

    public class UserProfileBasicModel
    {
        public long Id { get; set; }

        public string CNP { get; set; }

        public GenderEnum Gender { get; set; }

        public string Citizenship { get; set; }

        public string Role { get; set; }

        public string Country { get; set; }


    }

    public class UserProfileContactModel
    {

        public string Email { get; set; }

        public string PhysicalAddress { get; set; }

        public string PhoneNumber { get; set; }


    }

    public class UserProfileInfoModel
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string State { get; set; }

        public DateTime BirthDay { get; set; }

        public string Profession { get; set; }

    }


    #endregion

    #region Editable

    public class UserProfileEditableModel
    {
        public UserProfileInfoEditableModel UserInfo { get; set; }

        public UserProfileContactEditableModel Contact { get; set; }

        public long Id { get; set; }
    }

    public class UserProfileInfoEditableModel
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Firstname", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Firstname { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Lastname", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Lastname { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "BirthDay", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DatePicker)]
        public BsDateTime BirthDay { get; set; }

        [Display(Name = "Profession", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Profession { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "State", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> StateDropdown { get; set; }


    }

    public class UserProfileContactEditableModel
    {

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.Email)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "PhysicalAddress", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string PhysicalAddress { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "PhysicalAddress", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string PhoneNumber { get; set; }

    }
    #endregion

    public enum PanelComponentsEnum
    {
        Basic = 1,
        UserInfo = 2,
        Contact = 3
    }

}