using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class Drivers_Data
    {


        public static DataTable GetAllDirvers()
        {
            string Query = @"SELECT 
    d.DriverID,
	p.PersonID,
    CONCAT(p.FirstName, ' ', p.SecondName, ' ', p.ThirdName, ' ', p.LastName) AS FullName,
    d.CreatedDate,
    COUNT(l.IsActive) AS ActiveLicenses
FROM 
    dbo.Drivers d
INNER JOIN 
    dbo.Licenses l ON d.DriverID = l.DriverID
INNER JOIN 
    dbo.People p ON d.PersonID = p.PersonID
GROUP BY 
    d.DriverID,
	p.PersonID,
    p.FirstName, 
    p.SecondName, 
    p.ThirdName, 
    p.LastName,
    d.CreatedDate";




            DataTable dt = new DataTable();
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

        public static DataTable GetAllDirversByPersonID(string PersonID)
        {
            string Query = @"SELECT 
    dbo.Drivers.DriverID,
    dbo.People.PersonID,
    CONCAT(FirstName, ' ', SecondName, ' ', ThirdName, ' ', LastName) AS FullName,
    CreatedDate,
    COUNT(IsActive) AS ActiveLicenses
FROM 
    dbo.Drivers
INNER JOIN 
    dbo.Licenses ON dbo.Drivers.DriverID = dbo.Licenses.DriverID
INNER JOIN 
    dbo.People ON dbo.Drivers.PersonID = dbo.People.PersonID
GROUP BY 
    dbo.Drivers.DriverID,
    dbo.People.PersonID,
    FirstName, 
    SecondName, 
    ThirdName, 
    LastName,
    CreatedDate
HAVING  dbo.People.PersonID = @PersonID;";





            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetAllDirversByDriverID(string DriverID)
        {
            string Query = @"SELECT 
    dbo.Drivers.DriverID,
    dbo.People.PersonID,
    CONCAT(FirstName, ' ', SecondName, ' ', ThirdName, ' ', LastName) AS FullName,
    CreatedDate,
    COUNT(IsActive) AS ActiveLicenses
FROM 
    dbo.Drivers
INNER JOIN 
    dbo.Licenses ON dbo.Drivers.DriverID = dbo.Licenses.DriverID
INNER JOIN 
    dbo.People ON dbo.Drivers.PersonID = dbo.People.PersonID
GROUP BY 
    dbo.Drivers.DriverID,
    dbo.People.PersonID,
    FirstName, 
    SecondName, 
    ThirdName, 
    LastName,
    CreatedDate
HAVING  dbo.Drivers.DriverID = @DriverID;";





            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static string AddNewDriver(string PersonID, string CreatedByUserID, DateTime CreatedDate)
        {
            string DriverID = null;


            string query = @"INSERT INTO [dbo].[Drivers]
           ([PersonID],
            [CreatedByUserID],
            [CreatedDate])
     VALUES
           (@PersonID,
            @CreatedByUserID,
            @CreatedDate);
    SELECT SCOPE_IDENTITY();";





            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            

            try
            {
                DataAccessSettinegs.Connection.Open();

                DriverID = Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }



            return DriverID;
        }

        public static string GetDriverID(string PersonID)
        {
            string query = @"select DriverID from Drivers
where 	PersonID = @PersonID	 ";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);



            try
            {
                DataAccessSettinegs.Connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToString(result);
                }
                else
                {
                    return null;
                }




            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();

            }
        }


    }
}
