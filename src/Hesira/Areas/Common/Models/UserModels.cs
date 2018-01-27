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
    public class UsersPageModel
    {
        [BsGrid(HasDetails = true)]
        public BsGridModel<UserRowModel> Grid { get; set; }

        [BsToolbar(Theme = BsTheme.Purple)]
        [Display(Name = "Users", ResourceType = typeof(Resource))]
        public BsToolbarModel<UserSearchModel, UserNewModel> Toolbar { get; set; }
    }

    public class UserRowModel : BsGridRowModel<UserDetailsModel>
    {
        public long Id { get; set; }
        public bool Enabled { get; set; }

        [BsGridColumn(Width = 3)]
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string Name { get; set; }

        [BsGridColumn(Width = 3)]
        [Display(Name = "Role", ResourceType = typeof(Resource))]
        public UserRoles Role { get; set; }

        [BsGridColumn(Width = 3)]
        [Display(Name = "CNP", ResourceType = typeof(Resource))]
        public string CNP { get; set; }

        [BsGridColumn(Width = 3)]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        public override object GetUniqueID()
        {
            return Id;
        }
    }

    public class UserDetailsModel
    {


    }

    public class UserNewModel
    {

        public UserNewModel()
        {

            Countries = new BsSelectList<int?>();
            Citizenship = new BsSelectList<int?>();

        }

        #region Basic

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Firstname", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Firstname { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Lastname", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Lastname { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Email { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [BsControl(BsControlType.RadioButtonList)]
        [Display(Name = "Role", ResourceType = typeof(Resource))]
        public BsSelectList<UserRoles?> RoleBtnList { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "CNP", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string CNP { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.Password)]
        public string Password { get; set; }


        #endregion

        #region Profile

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [BsControl(BsControlType.RadioButtonList)]
        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        public BsSelectList<GenderEnum?> GenderBtnList { get; set; }


        [Display(Name = "Profession", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Profession { get; set; }


        #region Address & Contact

        [Display(Name = "TelephoneNumber", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Country", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> Countries { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "PhysicalAddress", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string PhysicalAddress { get; set; }

        #endregion

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "BirthDay", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DatePicker)]
        public BsDateTime BirthDayPicker { get; set; }


        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "State", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> State { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Citizenship", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> Citizenship { get; set; }

        #endregion


    }

    public class UserSearchModel
    {

        public UserSearchModel()
        {

            AgeRange = new BsRange<int?>
            {
                From = new BsRangeItem<int?>
                {
                    ItemValue = 1,
                    MinValue = 1,
                    TextValue = "1"
                },
                To = new BsRangeItem<int?>
                {
                    ItemValue = 2,
                    MaxValue = 120,
                    TextValue = "2",
                },
                TextValue = Resource.Choose
            };
            CitizenshipDropdown = new BsSelectList<int?>();


        }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Name { get; set; }

        [Display(Name = "CNP", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string CNP { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.TextBox)]
        public string Email { get; set; }

        [BsControl(BsControlType.RadioButtonList)]
        [Display(Name = "IsEnabled", ResourceType = typeof(Resource))]
        public BsSelectList<YesNoEnum?> EnableBtnList { get; set; }

        [BsControl(BsControlType.RadioButtonList)]
        [Display(Name = "Role", ResourceType = typeof(Resource))]
        public BsSelectList<UserRoles?> RoleBtnList { get; set; }

        [Display(Name = "AgeInterval", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.NumberRange)]
        public BsRange<int?> AgeRange { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.RadioButtonList)]
        public BsSelectList<GenderEnum?> GenderBtnList { get; set; }

        [Display(Name = "Citizenship", Prompt = "Choose", ResourceType = typeof(Resource))]
        [BsControl(BsControlType.DropDownList)]
        public BsSelectList<int?> CitizenshipDropdown { get; set; }

    }

    public class UserComponentListModel
    {

        public UserComponentListModel()
        {
            UserComponentListBox = new BsSelectList<List<string>>();
            UserComponentDropdown = new BsSelectList<int?>();

        }

        public BsSelectList<List<string>> UserComponentListBox { get; set; }
        public BsSelectList<int?> UserComponentDropdown { get; set; }
        public RegComponentHtmlType HtmlType { get; set; }
        public int Total { get; set; }

    }

    public class UserComponentListUpdateModel
    {

        public string QueryString { get; set; }
        public int PageLimit { get; set; }
        public int CurrentPage { get; set; }
        public UserAutocompleteComponents? ComponentId { get; set; }

    }

    public enum UserAutocompleteComponents
    {
        Citizenship = 1,
        Countries = 2
    }

    public enum RegComponentHtmlType
    {

        Dropdown = 1,
        ListBox = 2

    }


}