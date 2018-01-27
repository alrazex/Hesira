using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using BForms.Grid;
using BForms.Models;
using BForms.Utilities;
using Hesira.Areas.Common.Models;
using Hesira.Entities;
using Hesira.Helpers.General;
using Hesira.Models;
using Hesira.Repositories;
using WebGrease.Css.Extensions;

namespace Hesira.Areas.Common.Repositories
{

    #region Settings

    public class UsersSettings : BsGridRepositorySettings<UserSearchModel>
    {
        public UserData UserData { get; set; }

    }

    #endregion

    public class UsersRepository : BsBaseGridRepository<User, UserRowModel>
    {

        #region Constructor & Properties

        private HesiraEntities db;

        public UsersSettings Settings
        {
            get
            {
                return settings as UsersSettings;
            }

        }

        public UsersRepository(HesiraEntities db)
        {

            this.db = db;

        }

        #endregion

        #region Mappers

        public Expression<Func<User, UserRowModel>> Map_User_UserRowModel = x => new UserRowModel
        {
            Id = x.Id,
            Enabled = x.Enabled,
            Name = x.Firstname + " " + x.Lastname,
            Email = x.Email,
            CNP = x.CNP,
            Role = x.IsAdmin ? UserRoles.Admin : (x.IsDoctor ? UserRoles.Doctor : UserRoles.Patient)

        };

        public Expression<Func<User, UserDetailsModel>> Map_User_UserDetailsModel = x => new UserDetailsModel
        {



        };


        #endregion

        #region Map / Filter / Query

        #region Query

        public override IQueryable<User> Query()
        {
            var query = db.Users.AsQueryable();
            return Filter(query);
        }

        #endregion

        #region Filter

        public IQueryable<User> Filter(IQueryable<User> query)
        {
            query = FilterByRole(query);
            query = FilterBySearchModel(query);
            return query;
        }

        public IQueryable<User> FilterByRole(IQueryable<User> query)
        {

            var userData = Settings.UserData;

            if (userData.IsDoctor)
            {
                query = query.Where(x => (x.IsDoctor || x.IsPatient) && !x.IsAdmin);
            }
            else if (userData.IsPatient)
            {
                query = query.Where(x => x.Id == userData.Id);
            }


            return query;
        }

        public IQueryable<User> FilterBySearchModel(IQueryable<User> query)
 {

    var searchSettings = Settings.Search;

    if (!string.IsNullOrEmpty(Settings.QuickSearch))
    {

        var quickSearch = Settings.QuickSearch.ToLower();

        query =
            query.Where(
                x =>
                    x.Email.ToLower().Contains(quickSearch) || x.CNP.ToLower().Contains(quickSearch) ||
                    x.Firstname.ToLower().Contains(quickSearch) || x.Lastname.ToLower().Contains(quickSearch) ||
                    x.UsersProfiles.FirstOrDefault() != null &&
                    x.UsersProfiles.FirstOrDefault().PhoneNumber.ToLower().Contains(quickSearch) ||
                    x.UsersProfiles.FirstOrDefault() != null &&
                    x.UsersProfiles.FirstOrDefault()
                        .Country.CountryNameLowerCase.ToLower()
                        .Contains(quickSearch) ||
                    x.UsersProfiles.FirstOrDefault() != null &&
                    x.UsersProfiles.FirstOrDefault().Profession.ToLower().Contains(quickSearch) ||
                    x.UsersProfiles.FirstOrDefault() != null &&
                    x.UsersProfiles.FirstOrDefault().UsersState.State.ToLower().Contains(quickSearch)
                );


    }
            else
                if (searchSettings != null)
                {

                    if (!string.IsNullOrEmpty(searchSettings.Email))
                    {
                        query = query.Where(x => x.Email.Contains(searchSettings.Email));
                    }

                    if (!string.IsNullOrEmpty(searchSettings.Name))
                    {
                        query = query.Where(x => (x.Firstname + " " + x.Lastname).Contains(searchSettings.Name));
                    }

                    if (!string.IsNullOrEmpty(searchSettings.CNP))
                    {
                        query = query.Where(x => x.CNP.Contains(searchSettings.CNP));
                    }

                    if (searchSettings.EnableBtnList != null && searchSettings.EnableBtnList.SelectedValues != null)
                    {
                        var isEnable = searchSettings.EnableBtnList.SelectedValues.Value;
                        if (isEnable != YesNoEnum.All)
                        {
                            query = query.Where(x => isEnable == YesNoEnum.Yes ? x.Enabled : !x.Enabled);
                        }
                    }

                    if (searchSettings.GenderBtnList != null && searchSettings.GenderBtnList.SelectedValues != null)
                    {
                        var gender = searchSettings.GenderBtnList.SelectedValues.Value;
                        if (gender != GenderEnum.Both)
                        {
                            query =
                                query.Where(
                                    x =>
                                        gender == GenderEnum.Male
                                            ? x.UsersProfiles.FirstOrDefault() != null &&
                                              x.UsersProfiles.FirstOrDefault().Gender == (int)GenderEnum.Male
                                            : x.UsersProfiles.FirstOrDefault() != null &&
                                              x.UsersProfiles.FirstOrDefault().Gender == (int)GenderEnum.Female);
                        }

                    }


                    if (searchSettings.RoleBtnList != null && searchSettings.RoleBtnList.SelectedValues != null)
                    {
                        var role = searchSettings.RoleBtnList.SelectedValues.Value;
                        if (role != UserRoles.All)
                        {
                            query =
                                query.Where(
                                    x =>
                                        role == UserRoles.Admin
                                            ? x.IsAdmin
                                            : (role == UserRoles.Doctor
                                                ? x.IsDoctor
                                                : (role == UserRoles.Patient ? x.IsPatient : true)));

                        }

                    }

                    if (searchSettings.CitizenshipDropdown != null &&
                        searchSettings.CitizenshipDropdown.SelectedValues != null)
                    {


                        query =
                            query.Where(
                                x =>
                                    x.UsersProfiles.FirstOrDefault() != null &&
                                    x.UsersProfiles.FirstOrDefault().Id_Citizenship ==
                                    searchSettings.CitizenshipDropdown.SelectedValues);

                    }

                }

            return query;

        }

        #endregion

        #region Order

        public override IOrderedQueryable<User> OrderQuery(IQueryable<User> query)
        {

            orderedQueryBuilder.OrderFor(x => x.Name, y => y.Firstname + " " + y.Lastname);

            orderedQueryBuilder.OrderFor(x => x.CNP, y => y.CNP);

            orderedQueryBuilder.OrderFor(x => x.Role, y => y.IsAdmin ? 3 : y.IsDoctor ? 2 : 1);

            orderedQueryBuilder.OrderFor(x => x.Email, y => y.Email);

            var orderedQuery = orderedQueryBuilder.Order(query, x => x.OrderByDescending(y => y.Timestamp));

            return orderedQuery;
        }

        #endregion

        #region Map

        public override IEnumerable<UserRowModel> MapQuery(IQueryable<User> query)
        {

            var model = query.Select(Map_User_UserRowModel).ToList();
            return model;
        }

        #endregion

        #endregion

        #region CRUD

        #region Read


        #region Readonly

        public UserRowModel ReadRow(long? objId)
        {
            return db.Users.Where(x => x.Id == objId).Select(Map_User_UserRowModel).FirstOrDefault();
        }

        public List<UserRowModel> ReadRows(List<long> objIds)
        {
            return db.Users.Where(x => objIds.Contains(x.Id)).Select(Map_User_UserRowModel).ToList();
        }


        #endregion

        #region Editable



        #endregion

  

        #endregion


        #region Delete & Enable / Disable

        public void EnableDisable(long objId, bool? enable)
        {
            var entity = db.Users.FirstOrDefault(x => x.Id == objId);

            if (entity != null)
            {
                entity.Enabled = enable.HasValue ? enable.Value : !entity.Enabled;
                db.SaveChanges();
            }
        }

        public void Delete(long objId)
        {
            var entity = db.Users.FirstOrDefault(x => x.Id == objId);

            if (entity != null)
            {

                

                foreach (var prescription in db.Prescriptions.Where(x => x.Id_Doctor == objId || x.Id_Patient == objId).ToList())
                {

                    foreach (var pdrugs in db.PrescriptionsDrugs.Where(x=>x.Id_Prescription == prescription.Id))
                    {
                        db.PrescriptionsDrugs.Remove(pdrugs);
                    }

                    db.Prescriptions.Remove(prescription);
                }

                foreach (var appointment in db.Appointments.Where(x => x.Id_Doctor == objId || x.Id_Patient == objId).ToList())
                {
                    db.Appointments.Remove(appointment);
                }

                foreach (var userProfile in db.UsersProfiles.Where(x => x.Id_User == objId))
                {
                    db.UsersProfiles.Remove(userProfile);
                }

                db.Users.Remove(entity);
                db.SaveChanges();
            }
        }



        #endregion

        #endregion

        #region Details


        public override void FillDetails(UserRowModel row)
        {
            row.Details = db.Users.Where(x => x.Id == row.Id).Select(Map_User_UserDetailsModel).FirstOrDefault();
        }


        #endregion

        #region New / Search Form

        public UserSearchModel GetSearchForm(UsersSettings settings)
        {
            var model = new UserSearchModel();

            var userData = settings.UserData;

            model.EnableBtnList = new BsSelectList<YesNoEnum?>();
            model.EnableBtnList.ItemsFromEnum(typeof(YesNoEnum));
            model.EnableBtnList.SelectedValues = YesNoEnum.All;


            model.RoleBtnList = new BsSelectList<UserRoles?>();

            if (userData.IsDoctor)
            {
                model.RoleBtnList.ItemsFromEnum(typeof(UserRoles), UserRoles.Admin);

            }
            else
            {
                model.RoleBtnList.ItemsFromEnum(typeof(UserRoles));

            }

            model.RoleBtnList.SelectedValues = UserRoles.All;

            model.GenderBtnList = new BsSelectList<GenderEnum?>();
            model.GenderBtnList.ItemsFromEnum(typeof(GenderEnum));
            model.GenderBtnList.SelectedValues = GenderEnum.Both;

           
            return model;
        }

        public UserNewModel GetNewForm(UsersSettings settings)
        {

            var model = new UserNewModel();

            var userData = settings.UserData;

            model.RoleBtnList = new BsSelectList<UserRoles?>();

            if (userData.IsDoctor)
            {
                model.RoleBtnList.ItemsFromEnum(typeof(UserRoles), UserRoles.Admin, UserRoles.All);

            }
            else
            {
                model.RoleBtnList.ItemsFromEnum(typeof(UserRoles), UserRoles.All);

            }
            model.RoleBtnList.SelectedValues = UserRoles.Patient;

            model.GenderBtnList = new BsSelectList<GenderEnum?>();
            model.GenderBtnList.ItemsFromEnum(typeof(GenderEnum), GenderEnum.Both);
            model.GenderBtnList.SelectedValues = GenderEnum.Male;


            model.State = new BsSelectList<int?>();
            model.State.Items = db.UsersStates.Select(x => new BsSelectListItem
            {
                Text = x.Code,
                Value = x.Id.ToString()
            }).ToList();

            return model;
        }

        #endregion

        #region Autocomplete Widget

        public UserComponentListModel UpdateUserComponentList(UsersSettings settings, UserComponentListUpdateModel updateModel)
        {

            var componentReturnModel = new UserComponentListModel();

            switch (updateModel.ComponentId)
            {
                #region Countries & Citizenship
                case UserAutocompleteComponents.Citizenship:
                case UserAutocompleteComponents.Countries:
                    {
                        componentReturnModel.HtmlType = RegComponentHtmlType.Dropdown;


                        var countries = db.Countries.AsQueryable();

                        if (!string.IsNullOrEmpty(updateModel.QueryString))
                        {
                            countries =
                                countries.Where(
                                    x => x.CountryNameLowerCase.Contains(updateModel.QueryString.ToLower()));
                        }

                        componentReturnModel.Total = countries.Count();

                        var countriesIQ = countries.OrderBy(x => x.CountryNameLowerCase)
                            .Skip((updateModel.CurrentPage - 1) * updateModel.PageLimit)
                            .Take(updateModel.PageLimit);

                        componentReturnModel.UserComponentDropdown.Items =
                                countriesIQ.Select(item => new BsSelectListItem
                                {
                                    Text = item.CountryNameUpperCase.Trim(),
                                    Value = SqlFunctions.StringConvert((double)item.Id),

                                }).ToList();


                        break;
                    }


                #endregion
            }

            return componentReturnModel;

        }

        #endregion

        #region Helpers 

        public bool NotUniqueCNP(string CNP)
        {
            return db.Users.Any(x => x.CNP == CNP);
        }

        #endregion

    }
}