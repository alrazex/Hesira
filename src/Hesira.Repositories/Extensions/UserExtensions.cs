using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hesira.Entities;
using Hesira.Models.Address;
using Hesira.Models.User;

namespace Hesira.Repositories.Extensions
{
    public static class UserExtensions
    {


        public static void ToDomainModel(this User dbModel, UserModel domainModel)
        {
            domainModel.Id = dbModel.Id;

            domainModel.CNP = dbModel.CNP;
            domainModel.Email = dbModel.Email;
            domainModel.Enabled = dbModel.Enabled;
            domainModel.Firstname = dbModel.Firstname;
            domainModel.IsAdmin = dbModel.IsAdmin;
            domainModel.IsDoctor = dbModel.IsDoctor;
            domainModel.IsPatient = dbModel.IsPatient;

            domainModel.Lastname = dbModel.Lastname;
            domainModel.Password = dbModel.Password;

            var userProfile = dbModel.UsersProfiles.FirstOrDefault();

            if (userProfile != null)
            {
                dbModel.UsersProfiles.FirstOrDefault().ToDomainModel(domainModel.Profile);
                domainModel.Id_Profile = userProfile.Id;
            }

            domainModel.SidebarMode = dbModel.SidebarMode;
        }

        public static void ToDatabaseModel(this UserModel domainModel, User dbModel)
        {
            dbModel.CNP = domainModel.CNP;
            dbModel.Email = domainModel.Email;
            dbModel.Enabled = domainModel.Enabled;
            dbModel.Firstname = domainModel.Firstname;
            dbModel.IsAdmin = domainModel.IsAdmin;
            dbModel.IsDoctor = domainModel.IsDoctor;
            dbModel.IsPatient = domainModel.IsPatient;
            dbModel.Lastname = domainModel.Lastname;
            dbModel.SidebarMode = domainModel.SidebarMode;
            dbModel.Timestamp = DateTime.Now;
            dbModel.Password = domainModel.Password;

        }

        public static void ToDomainModel(this UsersProfile dbModel, UserProfileModel domainModel)
        {

            domainModel.Id = dbModel.Id;
            domainModel.BirthDay = dbModel.Birthday;
            domainModel.Gender = dbModel.Gender;
            domainModel.Id_Address = dbModel.Id_Address;
            domainModel.Id_Citizenship = dbModel.Id_Citizenship;
            if (dbModel.Id_State.HasValue)
            {
                domainModel.Id_State = dbModel.Id_State.Value;
                
            }
            domainModel.Id_User = dbModel.Id_User;
            domainModel.PhoneNumber = dbModel.PhoneNumber;
            domainModel.Profession = dbModel.Profession;
            if (dbModel.Id_Address.HasValue)
            {
               dbModel.Address.ToDomainModel(domainModel.Address);
            }

        }

        public static void ToDatabaseModel(this UserProfileModel domainModel, UsersProfile dbModel)
        {

            dbModel.Birthday = domainModel.BirthDay;
            dbModel.Gender = domainModel.Gender;
            dbModel.Id_Address = domainModel.Id_Address;
            dbModel.Id_Citizenship = domainModel.Id_Citizenship;
            dbModel.Id_State = domainModel.Id_State;
            dbModel.Id_User = domainModel.Id_User;
            dbModel.PhoneNumber = domainModel.PhoneNumber;
            dbModel.Profession = domainModel.Profession;

        }

    }
}
