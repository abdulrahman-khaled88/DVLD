using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.ApTypeData;

namespace DVLD_DataAccess
{
    public static class TestTypeData
    {

        public class ClsTestTy
        {
           public string ID { get; set; }
           public string Title { get; set; }
           public string Description { get; set; }
           public string Fees { get; set; }

        }

        public static DataTable ListAllTestTypes()
        {
            DataTable dt = new DataTable();

            string Query = @"select * from TestTypes;";

            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);


            try
            {
                DataAccessSettinegs.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }




            return dt;
        }

        public static bool UpdateTestTypes(string TestID, ClsTestTy NewTest)
        {
            string Query = @"UPDATE [dbo].[TestTypes]
   SET [TestTypeTitle] = @TestTypeTitle,
       [TestTypeDescription] = @TestTypeDescription,
       [TestTypeFees] = @TestTypeFees
 WHERE TestTypeID = @TestID ;";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestTypeTitle", NewTest.Title);
            command.Parameters.AddWithValue("@TestTypeFees", NewTest.Fees);
            command.Parameters.AddWithValue("@TestTypeDescription", NewTest.Description);




            try
            {
                DataAccessSettinegs.Connection.Open();

                RowsEffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }


            return RowsEffected > 0;
        }

        public static DataTable FindTestTypeByID(string TestID)
        {
            DataTable dt = new DataTable();

            string Query = "select * from TestTypes where TestTypeID = @TestID;";

            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                DataAccessSettinegs.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
                }


                reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }




            return dt;
        }

        




    }
}
