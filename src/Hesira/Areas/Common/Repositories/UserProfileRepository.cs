using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using BForms.Models;
using Hesira.Areas.Common.Models;
using Hesira.Entities;
using Hesira.Models;
using Hesira.Repositories;
using Hesira.Resources;

namespace Hesira.Areas.Common.Repositories
{
    public class UserProfileRepository : BaseRepository
    {
        #region Constructor

        public UserProfileRepository(HesiraEntities db)
            : base(db)
        {

        }

        #endregion

        #region CRUD

        #region Read
        public UserProfileModel GetUserProfile(long userId)
        {

            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                var profile = user.UsersProfiles.FirstOrDefault();

                if (profile != null)
                {

                    var address = profile.Address;

                    return new UserProfileModel
                    {
                        Basic = new UserProfileBasicModel
                        {
                            Citizenship = ReturnCitizenshipForUser(profile.Id_Citizenship),
                            CNP = user.CNP,
                            Gender = profile.Gender == (int)GenderEnum.Female ? GenderEnum.Female : GenderEnum.Male,
                            Role = user.IsAdmin ? Resource.Admin : (user.IsDoctor ? Resource.Doctor : Resource.Patient),
                            Id = user.Id,
                            Country = address != null && address.Country != null ? address.Country.CountryNameLowerCase : string.Empty
                        },
                        UserInfo = new UserProfileInfoModel
                        {
                            BirthDay = profile.Birthday,
                            Firstname = user.Firstname,
                            Lastname = user.Lastname,
                            Profession = profile.Profession,
                            State = profile.UsersState != null ? profile.UsersState.State : string.Empty
                        },
                        Contact = new UserProfileContactModel
                        {
                            Email = user.Email,
                            PhoneNumber = profile.PhoneNumber,
                            PhysicalAddress = address != null ? address.PhysicalAddress : string.Empty
                        }
                    };


                }




            }
            return new UserProfileModel();






        }

        public UserProfileEditableModel GetEditableUserProfile(long objId, PanelComponentsEnum componentId)
        {

            var model = db.Users.FirstOrDefault(x => x.Id == objId);
            var result = new UserProfileEditableModel
            {
                Id = model.Id,
                Contact = new UserProfileContactEditableModel
                {
                    Email = model.Email
                }
            };

            if (componentId == PanelComponentsEnum.Contact)
            {
                var profile = model.UsersProfiles.FirstOrDefault();
                if (profile != null && profile.Address != null)
                {
                    result.Contact.PhoneNumber = profile.PhoneNumber;
                    result.Contact.PhysicalAddress = profile.Address.PhysicalAddress;

                }


            }
            else if (componentId == PanelComponentsEnum.UserInfo)
            {
                var profile = model.UsersProfiles.FirstOrDefault();
                if (profile != null)
                {


                    var states = new BsSelectList<int?>
                    {
                        Items = db.UsersStates.Select(x => new BsSelectListItem
                        {
                            Text = x.Code,
                            Value = x.Id.ToString()
                        }).ToList(),
                        SelectedValues = profile.Id_State
                    };

                    result.UserInfo = new UserProfileInfoEditableModel
                    {
                        Firstname = model.Firstname,
                        Lastname = model.Lastname,
                        BirthDay = new BsDateTime
                        {
                            DateValue = profile.Birthday
                        },
                        Profession = profile.Profession,
                        StateDropdown = states

                    };


                }

            }

            return result;


        }
        #endregion

        #region Update

        public UserProfileModel Update(UserProfileEditableModel model, long objId)
        {
            var userProfile = db.UsersProfiles.FirstOrDefault(x => x.Id_User == objId);

            if (userProfile != null)
            {
                if (model.UserInfo != null)
                {
                    userProfile.User.Firstname = model.UserInfo.Firstname;
                    userProfile.User.Lastname = model.UserInfo.Lastname;
                    userProfile.Profession = model.UserInfo.Profession;
                    if (model.UserInfo.BirthDay.DateValue != null)
                    {
                        userProfile.Birthday = model.UserInfo.BirthDay.DateValue.Value;
                    }
                    userProfile.Id_State = model.UserInfo.StateDropdown != null &&
                                           model.UserInfo.StateDropdown.SelectedValues != null
                        ? model.UserInfo.StateDropdown.SelectedValues
                        : null;

                }

                if (model.Contact != null)
                {
                    userProfile.PhoneNumber = model.Contact.PhoneNumber;
                    userProfile.Address.PhysicalAddress = model.Contact.PhysicalAddress;
                    userProfile.User.Email = model.Contact.Email;
                }

                userProfile.Timestamp = DateTime.Now;
                db.SaveChanges();

                return GetUserProfile(objId);
            }

            return null;
        }

        #endregion

        #endregion

        #region Helpers
        public string ReturnCitizenshipForUser(int countryId)
        {
            return db.Countries.FirstOrDefault(x => x.Id == countryId).ISO_ALPHA3;
        }

        #endregion

    }
}