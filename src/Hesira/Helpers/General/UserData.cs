using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hesira.Entities;
using Hesira.Models;

namespace Hesira.Helpers.General
{
    [Serializable]
    public class UserData
    {

        public long Id { get; set; }
        public string CNP { get; set; }
        public string Email { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsDoctor { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPatient { get; set; }
        public bool SideBarOpen { get; set; }
        public GenderEnum Gender { get; set; }
        public string Profession { get; set; }
        public UserRoles Role { get; set; }

        public UserData(HesiraEntities db, string cnp)
        {
            LoadData(db, cnp);
        }

        public void LoadData(HesiraEntities db, string cnp)
        {

            var user = db.Users.FirstOrDefault(x => x.CNP == cnp);

            if (user != null)
            {
                user.LastAuthDate = DateTime.Now;
                db.SaveChanges();
                Id = user.Id;
                Email = user.Email;
                Name = user.Firstname + " " + user.Lastname;
                Firstname = user.Firstname;
                Lastname = user.Lastname;
                IsDoctor = user.IsDoctor;
                IsAdmin = user.IsAdmin;
                IsPatient = user.IsPatient;
                Enabled = user.Enabled;
                SideBarOpen = user.SidebarMode;
                Role = user.IsAdmin ? UserRoles.Admin : (user.IsDoctor ? UserRoles.Doctor : UserRoles.Patient);
                CNP = user.CNP;

                var userProfile = user.UsersProfiles.FirstOrDefault();
                if (userProfile != null)
                {
                    Gender = userProfile.Gender == (int)GenderEnum.Female ? GenderEnum.Female : GenderEnum.Male;
                    Profession = userProfile.Profession;
                }
                 
                
            }

        }


    }

}
