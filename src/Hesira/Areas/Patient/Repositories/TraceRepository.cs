using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hesira.Areas.Common.Models;
using Hesira.Areas.Patient.Models;
using Hesira.Entities;
using Hesira.Repositories;

namespace Hesira.Areas.Patient.Repositories
{
    public class TraceRepository : BaseRepository
    {
        #region Constructor

        public TraceRepository(HesiraEntities db)
            : base(db)
        {

        }

        #endregion

        public TracePageModel GetTrace(long userId)
        {
            var appointmentsTraceList = (from appointment in db.Appointments
                join prescription in db.Prescriptions on appointment.Id_Prescription equals prescription.Id
                where appointment.Id_Patient == userId && appointment.State == (int) AppointmentState.Finished
                select new MedicalTraceRow
                {
                    DoctorId = appointment.Id_Doctor,
                    DoctorName = appointment.AppointmentDoctor.Firstname + " " + appointment.AppointmentDoctor.Lastname,
                    EndDate = appointment.EndDate,
                    StartDate = appointment.StartDate,
                    PrescriptionNumber = prescription.Number,
                    PrescriptionSeries = prescription.Series,
                    PrescriptionId = prescription.Id,
                    MedicalTrace = appointment.Trace,
                    Disease =
                        prescription.PrescriptionsDrugs.FirstOrDefault() != null
                            ? prescription.PrescriptionsDrugs.FirstOrDefault().Disease.Name
                            : string.Empty

                }).OrderByDescending(x => x.StartDate).ToList();


            foreach (var medicalTraceRow in appointmentsTraceList)
            {
                medicalTraceRow.StartDateString = medicalTraceRow.StartDate.ToShortDateString() + " " +
                                                  medicalTraceRow.StartDate.ToShortTimeString();

                medicalTraceRow.EndDateString = medicalTraceRow.EndDate.ToShortDateString() + " " +
                                                medicalTraceRow.EndDate.ToShortTimeString();

                medicalTraceRow.AssociatedDrugDisease = (from pd in db.PrescriptionsDrugs
                                                         join drug in db.Drugs on pd.Id_Drug equals drug.Id
                                                         join disease in db.Diseases on pd.Id_Disease equals disease.Id
                                                         where medicalTraceRow.PrescriptionId == pd.Id_Prescription
                                                         select new DrugDiseaseModel
                                                         {

                                                             Days = pd.Days,
                                                             Quantity = pd.Quantity,
                                                             DiseaseModel = new LightDiseaseModel
                                                             {
                                                                 Id = disease.Id,
                                                                 Name = disease.Name
                                                             },
                                                             DrugModel = new LightDrugModel
                                                             {
                                                                 Id = drug.Id,
                                                                 Vendor = drug.Vendor,
                                                                 CommercialName = drug.ComercialName,
                                                                 ReductionClass = drug.ReductionClass
                                                             }

                                                         }).ToList();

            }


            return new TracePageModel
            {
                AppointmentsTrace = appointmentsTraceList
            };
        }
    }
}