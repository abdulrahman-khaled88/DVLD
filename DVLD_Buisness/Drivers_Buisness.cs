using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public static class Drivers_Buisness
    {
        public static DataTable GetAllDirvers()
        {
            return Drivers_Data.GetAllDirvers();
        }

        public static DataTable GetAllDirversByDriverID(string DriverID)
        {
            return Drivers_Data.GetAllDirversByDriverID(DriverID);
        }

        public static DataTable GetAllDirversByPersonID(string PersonID)
        {
            return Drivers_Data.GetAllDirversByPersonID(PersonID);
        }

        public static string AddNewDriver(string PersonID, string CreatedByUserID, DateTime CreatedDate)
        {
            return Drivers_Data.AddNewDriver(PersonID, CreatedByUserID, CreatedDate);
        }

        public static string GetDriverID(string PersonID)
        {
            return Drivers_Data.GetDriverID(PersonID);
        }
    }
}
