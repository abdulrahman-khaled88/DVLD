using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public static class TestTypeBuisness
    {
        public class ClsTestTy
        {
            public string ID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Fees { get; set; }

        }

        public static string SelectedIdDGV = null;
        public static DataTable ListAllTestTypes()
        {
            return TestTypeData.ListAllTestTypes();
        }

        public static bool UpdateTestTypes(string TestID, ClsTestTy NewTest)
        {
            return TestTypeData.UpdateTestTypes(TestID, _BuisnessToDataConvert(NewTest));
        }

        public static DataTable FindTestTypeByID(string TestID)
        {
            return TestTypeData.FindTestTypeByID(TestID);
        }

        private static TestTypeData.ClsTestTy _BuisnessToDataConvert(ClsTestTy Test)
        {
            TestTypeData.ClsTestTy Test2 = new TestTypeData.ClsTestTy();

            Test2.ID = Test.ID;
            Test2.Title = Test.Title;
            Test2.Description = Test.Description;
            Test2.Fees = Test.Fees;

                

            return Test2;
        }

        public static TestTypeBuisness.ClsTestTy DtToClsConvert(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }


            TestTypeBuisness.ClsTestTy Test = new TestTypeBuisness.ClsTestTy();


            DataRow row = dt.Rows[0];

            Test.ID = Convert.ToString(row["TestTypeID"]);
            Test.Title = Convert.ToString(row["TestTypeTitle"]);
            Test.Description = Convert.ToString(row["TestTypeDescription"]);
            Test.Fees = Convert.ToString(row["TestTypeFees"]);






            return Test;
        }
    }
}
