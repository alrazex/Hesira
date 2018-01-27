using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesira
{
    public class Enums
    {
        #region User Enums


        public enum UserRoles : byte
        {
            All = 1,
            Patient = 2,
            Doctor = 3,
            Admin = 4
        }

        public enum GenderEnum
        {
            Both = 1,
            Female = 2,
            Male = 3
        }
       

        #endregion

    }
}
