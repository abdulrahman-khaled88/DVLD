using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public static class ApTypeBuisness
    {
        public static string SelectIdInDGV = null;
        public  class ClsApType
        {
            public  string ID = string.Empty;
            public  string Title = string.Empty;
            public  string Fees = string.Empty;
        }

        public static DataTable ListAllApTypesInfo()
        {
            return ApTypeData.ListAllApTypesInfo();
        }

        private static ApTypeData.ClsApType _BuisnessToDataConvert(ClsApType AP)
        {
            ApTypeData.ClsApType AP2 = new ApTypeData.ClsApType();

            AP2.ID = AP.ID;
            AP2.Title = AP.Title;
            AP2.Fees = AP.Fees;
            return AP2;
        }

        public static bool UpdateApTypes(string ApplicationID, ClsApType NewApplication)
        {
            return ApTypeData.UpdateApTypes(ApplicationID,
                _BuisnessToDataConvert(NewApplication));
        }

        public static DataTable FindApTypeByID(string ApplicationTypesID)
        {
            return ApTypeData.FindApTypeByID(ApplicationTypesID);
        }

        public static ApTypeBuisness.ClsApType DtToClsConvert(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null; 
            }

            
            ApTypeBuisness.ClsApType AP = new ApTypeBuisness.ClsApType();

            
            DataRow row = dt.Rows[0];

            
            AP.ID =    Convert.ToString( row["ApplicationTypeID"] ); 
            AP.Title = Convert.ToString(row["ApplicationTypeTitle"]);
            AP.Fees = Convert.ToString(row["ApplicationFees"]);



            return AP;
        }
    }
}
