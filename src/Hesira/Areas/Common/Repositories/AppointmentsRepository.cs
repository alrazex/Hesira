
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BForms.Models;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Hesira.Areas.Common.Models;
using Hesira.Entities;
using Hesira.Helpers.General;
using Hesira.Models;
using Hesira.Repositories;

namespace Hesira.Areas.Common.Repositories
{
    public class AppointmentsRepository : BaseRepository
    {
        #region Constructor

        public AppointmentsRepository(HesiraEntities db)
            : base(db)
        {

        }

        #endregion.

        public NewAppoitmentModel GetNewAppoitmentModel()
        {
            return new NewAppoitmentModel
            {
                DoctorsDropdown = new BsSelectList<int?>
                {
                    Items = db.Users.Where(x => x.IsDoctor).Select(x => new BsSelectListItem
                    {
                        Value = SqlFunctions.StringConvert((double)x.Id),
                        Text = x.Firstname + " " + x.Lastname
                    }).OrderBy(x => x.Text).ToList()
                }
            };
        }

        public AppointmentsListModel GetAppointments(UserData userData)
        {
            return new AppointmentsListModel
            {
                UserLightModel = db.Users.Where(x => x.Id == userData.Id).Select(x => new LightUserViewModel
                {
                    Id = x.Id,
                    FirstName = x.Firstname,
                    LastName = x.Lastname,
                }).FirstOrDefault(),
                Appointments =
                    db.Appointments.Where(x => x.State == (int)AppointmentState.Pending && userData.IsPatient ? x.Id_Patient == userData.Id : x.Id_Doctor == userData.Id)
                        .Select(x => new AppointmentLightModel
                        {
                            Id = x.Id,
                            EndDate = x.EndDate,
                            StartDate = x.StartDate,
                            PatientOrDoctorModel = userData.IsPatient
                                ? new LightUserViewModel
                                {
                                    Id = x.AppointmentDoctor.Id,
                                    FirstName = x.AppointmentDoctor.Firstname,
                                    LastName = x.AppointmentDoctor.Lastname
                                }
                                : new LightUserViewModel
                                {
                                    Id = x.AppointmentPatient.Id,
                                    FirstName = x.AppointmentPatient.Firstname,
                                    LastName = x.AppointmentPatient.Lastname
                                }
                        }).ToList().Where(x => x.EndDate.Value.Date == DateTime.Now.Date).ToList()

            };
        }

        public bool Create(NewAppoitmentModel newModel, UserData userData)
        {
            var dbAppointment = new Appointment();

            if (newModel.DoctorsDropdown.SelectedValues != null && newModel.DatePicker.DateValue != null)
            {
                var doctorId = newModel.DoctorsDropdown.SelectedValues.Value;
                var currentDate = newModel.DatePicker.DateValue.Value.Date;

                dbAppointment.Id_Doctor = doctorId;
                var currentAppointments =
                    db.Appointments.Where(
                        x => x.Id_Doctor == doctorId).ToList().Where(x => x.StartDate.Date == currentDate).ToList();
                if (currentAppointments.Any())
                {

                    var maxEndDate =
                        currentAppointments.OrderByDescending(x => x.Id).Select(x => x.EndDate).FirstOrDefault();

                    if (maxEndDate.Hour >= 18 && maxEndDate.Minute >= 30)
                    {
                        return false;
                    }
                    else
                    {
                        dbAppointment.StartDate = maxEndDate;
                        dbAppointment.EndDate = maxEndDate.AddMinutes(30);
                    }


                }
                else
                {

                    dbAppointment.StartDate = currentDate.AddHours(8);
                    dbAppointment.EndDate = currentDate.AddHours(8).AddMinutes(30);

                }
            }
            else
            {
                return false;
            }
            dbAppointment.Id_Patient = userData.Id;
            dbAppointment.State = (int)AppointmentState.Pending;
            db.Appointments.Add(dbAppointment);
            db.SaveChanges();
            return true;

        }


        public List<EventModel> ConvertAppointmentsToEvents(List<AppointmentLightModel> currentAppointments)
        {

            return currentAppointments.Select(x => new EventModel
            {
                end = x.EndDate.Value.ToString("s"),
                start = x.StartDate.Value.ToString("s"),
                title = x.PatientOrDoctorModel.FirstName + " " + x.PatientOrDoctorModel.LastName,
                id = x.Id.ToString(),
                allDay = false
            }).ToList();

        }
    }
}