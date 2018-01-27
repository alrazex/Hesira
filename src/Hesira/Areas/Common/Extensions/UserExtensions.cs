using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Web;
using DocumentFormat.OpenXml.Wordprocessing;
using Hesira.Areas.Common.Models;
using Hesira.Helpers.General;
using Hesira.Models;
using Hesira.Models.Address;
using Hesira.Models.User;
using UserProfileModel = Hesira.Models.User.UserProfileModel;

namespace Hesira.Areas.Common.Extensions
{
    public static class UserExtensions
    {

        public static void ToUserProfileDomainModel(this UserNewModel viewModel, UserProfileModel domainModel)
        {
            domainModel.Id_Citizenship = viewModel.Citizenship != null && viewModel.Citizenship.SelectedValues != null
                ? viewModel.Citizenship.SelectedValues.Value
                : 0;
            domainModel.Id_State = viewModel.State != null && viewModel.State.SelectedValues != null
                ? (int?)viewModel.State.SelectedValues.Value
                : null;
            domainModel.PhoneNumber = viewModel.PhoneNumber;
            domainModel.Profession = viewModel.Profession;
            domainModel.Gender = viewModel.GenderBtnList != null && viewModel.GenderBtnList.SelectedValues != null
                ? (int)viewModel.GenderBtnList.SelectedValues
                : 0;
            domainModel.BirthDay = viewModel.BirthDayPicker.DateValue.HasValue
                ? viewModel.BirthDayPicker.DateValue.Value
                : DateTime.MinValue;

        }

        public static void ToUserDomainModel(this UserNewModel viewModel, UserModel domainModel)
        {
            domainModel.Firstname = viewModel.Firstname;
            domainModel.Lastname = viewModel.Lastname;
            domainModel.CNP = viewModel.CNP;

            var currentSalt = ConfigurationManager.AppSettings["authSalt"];
            var encryptedPassword = viewModel.Password.Encrypt(currentSalt);
            domainModel.Password = encryptedPassword;

            domainModel.Enabled = true;
            if (viewModel.RoleBtnList != null && viewModel.RoleBtnList.SelectedValues != null)
            {
                var selected = viewModel.RoleBtnList.SelectedValues;
                domainModel.IsAdmin = selected == UserRoles.Admin;
                domainModel.IsDoctor = selected == UserRoles.Doctor;
                domainModel.IsPatient = selected == UserRoles.Patient;
            }
            domainModel.SidebarMode = true;
            domainModel.Email = viewModel.Email;
        }

        public static void ToAddressDomainModel(this UserNewModel viewModel, AddressModel domainModel)
        {
            domainModel.CountryId = viewModel.Countries != null && viewModel.Countries.SelectedValues != null
                ? viewModel.Countries.SelectedValues.Value
                : 0;
            domainModel.PhysicalAddress = viewModel.PhysicalAddress;
        }

    }
}