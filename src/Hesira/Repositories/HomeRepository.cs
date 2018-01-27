using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hesira.Areas.Common.Models;
using Hesira.Entities;
using Hesira.Helpers.General;
using Hesira.Models;

namespace Hesira.Repositories
{
    public class HomeRepository : BaseRepository
    {
        public HomeRepository(HesiraEntities _db)
            : base(_db)
        {
        }

        public DashBoardPageModel GetDashBoardModel(UserData userData)
        {
            var users = db.Users.AsQueryable();
            var appointments = db.Appointments.AsQueryable();

            return new DashBoardPageModel
            {
                NoOfUsers = userData.IsAdmin ? users.Count() : userData.IsDoctor ? users.Count(x => (x.IsDoctor || x.IsPatient) && !x.IsAdmin) : (int?)null,
                NoOfAppointments = appointments.Count(x => x.State == (int)AppointmentState.Pending && (x.Id_Doctor == userData.Id || x.Id_Patient == userData.Id)),
                NoOfTraces = appointments.Count(x => x.State == (int)AppointmentState.Finished && (x.Id_Doctor == userData.Id || x.Id_Patient == userData.Id))
            };

        }
    }
}