using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_DataAccess.UserData;

namespace DVLD_DataAccess
{
    public static class ApTypeData
    {

        public  class ClsApType
        {
            public  string ID = string.Empty;
            public  string Title = string.Empty;
            public  string Fees = string.Empty;
        }

        public static DataTable ListAllApTypesInfo()
        {
            DataTable dt = new DataTable();

            string Query = @"select * from ApplicationTypes;";

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

        public static bool UpdateApTypes(string ApID, ClsApType NewAp)
        {
            string Query = @"UPDATE [dbo].[ApplicationTypes]
   SET [ApplicationTypeTitle] = @Title,
       [ApplicationFees] = @ApplicationFees
 WHERE ApplicationTypeID = @ApID";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@ApID", ApID);
            command.Parameters.AddWithValue("@Title", NewAp.Title);
            command.Parameters.AddWithValue("@ApplicationFees", NewAp.Fees);
            



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

        public static DataTable FindApTypeByID(string ApplicationTypesID)
        {
            DataTable dt = new DataTable();

            string Query = "select * from ApplicationTypes where ApplicationTypeID = @ApplicationTypesID;";

            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@ApplicationTypesID", ApplicationTypesID);

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
