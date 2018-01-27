using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hesira.Entities;
using Hesira.Helpers.General;

namespace Hesira.Repositories
{
    // repository for common ajax actions - whole application 
    public class AjaxRepository : BaseRepository
    {

        #region Properties & Constructor

        public AjaxRepository(HesiraEntities db)
            : base(db)
        {

        }

        #endregion

        #region Sidebar

        public bool? ToggleSidebarMode(UserData userData)
        {

            bool? sidebarMode = null;

            if (userData != null)
            {
                var userId = userData.Id;
                var currentUser = db.Users.FirstOrDefault(x => x.Id == userId);
                if (currentUser != null)
                {
                    currentUser.SidebarMode = !currentUser.SidebarMode;                   
                    db.SaveChanges();
                    sidebarMode = currentUser.SidebarMode;
                }

            }

            return sidebarMode;

        }
        #endregion

    }
}