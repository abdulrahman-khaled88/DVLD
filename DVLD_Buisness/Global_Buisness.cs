using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public static class Global_Buisness
    {
        
        public static string GetApFeesByID(string ID)
        {
          return  Global_Data.GetApFeesByID(ID);
        }

    }
}
