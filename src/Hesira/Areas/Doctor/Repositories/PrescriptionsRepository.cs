using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using BForms.Models;
using Hesira.Areas.Common.Models;
using Hesira.Areas.Doctor.Models;
using Hesira.Entities;
using Hesira.Helpers.General;
using Hesira.Repositories;

namespace Hesira.Areas.Doctor.Repositories
{
    public class PrescriptionsRepository : BaseRepository
    {

        #region Constructor

        private readonly PrescriptionGroupEditorRepository _builderRepository;
        public PrescriptionsRepository(HesiraEntities _db)
            : base(_db)
        {
            _builderRepository = new PrescriptionGroupEditorRepository(_db);
        }
        #endregion

        public bool Create(PrescriptionNewModel newModel, UserData userData)
        {
            if (newModel.Appointments != null && newModel.Appointments.SelectedValues != null &&
                newModel.Diseases != null && newModel.Diseases.SelectedValues != null)
            {
                var dbAppointment =
                    db.Appointments.FirstOrDefault(x => x.Id == newModel.Appointments.SelectedValues.Value);

                long? prescriptionNumber = null;
                var lastPrescription = db.Prescriptions.OrderByDescending(x => x.Number).FirstOrDefault();
                prescriptionNumber = lastPrescription != null ? lastPrescription.Number : 0;

                if (dbAppointment != null)
                {
                    var prescription = new Prescription();
                    prescription.Id_Doctor = userData.Id;
                    prescription.Id_Patient = dbAppointment.Id_Patient;
                    prescription.PrescriptionDate = DateTime.Now;
                    prescription.Number = prescriptionNumber.Value + 1;
                    prescription.Series = "RO/HESIRA/2014";
                    db.Prescriptions.Add(prescription);
                    db.SaveChanges();

                    foreach (var drug in newModel.PrescriptionBuilderModel.PrescriptionBuiler.Items)
                    {
                        var prescriptionDrug = new PrescriptionsDrug();
                        prescriptionDrug.Id_Prescription = prescription.Id;
                        prescriptionDrug.Id_Drug = drug.Id;
                        prescriptionDrug.Id_Disease = newModel.Diseases.SelectedValues.Value;
                        prescriptionDrug.Quantity = drug.Form.Quantity.ItemValue != null
                            ? drug.Form.Quantity.ItemValue.Value
                            : 0;
                        prescriptionDrug.Days = drug.Form.NumberOfDays.ItemValue != null
                            ? drug.Form.NumberOfDays.ItemValue.Value
                            : 0;
                        db.PrescriptionsDrugs.Add(prescriptionDrug);
                    }
                    db.SaveChanges();

                    dbAppointment.Trace = newModel.MedicalTrace;
                    dbAppointment.Id_Prescription = prescription.Id;
                    dbAppointment.State = (int)AppointmentState.Finished;
                    db.SaveChanges();
                    return true;

                }
                return false;
            }
            return false;
        }

        public PrescriptionNewModel GetNewForm(UserData userData)
        {

            var currentAppointments =
                db.Appointments.Where(x => x.State == (int)AppointmentState.Pending && x.Id_Doctor == userData.Id)
                    .ToList();
            return new PrescriptionNewModel
            {
                PrescriptionBuilderModel = new PrescriptionBuilderModel
                {
                    DrugsTab = new PrescriptionBuilderTabModel
                    {
                        Grid = _builderRepository.ToBsGridViewModel(new PrescriptionGroupEditorRepositorySettings
                        {
                            Page = 1,
                            PageSize = 5,

                        }),
                    }
                },
                Appointments = new BsSelectList<int?>
                {
                    Items = currentAppointments.Select(x => new BsSelectListItem
                    {
                        Text = x.AppointmentPatient.Firstname + " " + x.AppointmentPatient.Lastname + " - " + x.StartDate.ToShortDateString() + " " + x.StartDate.ToShortTimeString(),
                        Value = x.Id.ToString()
                    }).ToList(),
                },

                Diseases = new BsSelectList<int?>
                {
                    Items = db.Diseases.Select(x => new BsSelectListItem
                    {
                        Text = x.Name,
                        Value = SqlFunctions.StringConvert((double)x.Id)
                    }).ToList()
                }



            };
        }
    }
}