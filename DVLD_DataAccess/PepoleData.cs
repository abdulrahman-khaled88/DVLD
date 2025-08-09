using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class PepoleData
    {
        public class ClsPerson
        {
            public int ID { get; set; } = 0;
            public string FirstName { get; set; } = string.Empty;
            public string SecondName { get; set; } = string.Empty;
            public string ThirdName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string NationalNo { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public int NationalityCountryID { get; set; } = 0;
            public string Address { get; set; } = string.Empty;
            public DateTime DateOfBirth { get; set; } = new DateTime(2006, 1, 1);
            public string ImagePath { get; set; } = string.Empty;
            public int Gendor { get; set; } = 0;


        }


        public static DataTable ListAllPersonsInfo()
        {
            DataTable dt = new DataTable();

            string Query = @"SELECT        dbo.People.PersonID, dbo.People.NationalNo, dbo.People.FirstName, dbo.People.SecondName, dbo.People.ThirdName, dbo.People.LastName, dbo.People.DateOfBirth, dbo.People.Gendor, dbo.People.Address, 
                         dbo.People.Phone, dbo.People.Email, dbo.Countries.CountryName AS Country, dbo.People.ImagePath
FROM            dbo.People INNER JOIN
                         dbo.Countries ON dbo.People.NationalityCountryID = dbo.Countries.CountryID";

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
                throw new Exception("Error",ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }




            return dt;
        }

        public static DataTable ListAllPersonsInfoByFilter(string Query, string FilterBy, string Value)
        {
            DataTable dt = new DataTable();



            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue($"@{FilterBy}", Value);

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

        public static string AddNewPerson(ClsPerson Person)
        {

            string query = @"
INSERT INTO [dbo].[People]
   ([NationalNo]
   ,[FirstName]
   ,[SecondName]
   ,[ThirdName]
   ,[LastName]
   ,[DateOfBirth]
   ,[Gendor]
   ,[Address]
   ,[Phone]
   ,[Email]
   ,[NationalityCountryID]
   ,[ImagePath])
VALUES
   (@NationalNo
   ,@FirstName
   ,@SecondName
   ,@ThirdName
   ,@LastName
   ,@DateOfBirth
   ,@Gendor
   ,@Address
   ,@Phone
   ,@Email
   ,@NationalityCountryID
   ,@ImagePath);
    SELECT SCOPE_IDENTITY();";
            string ID = null;
            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@NationalNo", Person.NationalNo);
            command.Parameters.AddWithValue("@FirstName", Person.FirstName);
            command.Parameters.AddWithValue("@SecondName", Person.SecondName);
            command.Parameters.AddWithValue("@ThirdName", Person.ThirdName);
            command.Parameters.AddWithValue("@LastName", Person.LastName);
            command.Parameters.AddWithValue("@DateOfBirth", Person.DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Person.Gendor);
            command.Parameters.AddWithValue("@Address", Person.Address);
            command.Parameters.AddWithValue("@Phone", Person.Phone);
            command.Parameters.AddWithValue("@Email", Person.Email);
            command.Parameters.AddWithValue("@NationalityCountryID", Person.NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", Person.ImagePath);

            try
            {
                DataAccessSettinegs.Connection.Open();
                ID = Convert.ToString( command.ExecuteScalar() );
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }



            return ID ;
        }

        public static bool DeletePerson(int PersonID)
        {
            string Query = @"DELETE FROM [dbo].[People]
      WHERE PersonID = @PersonID";
            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static int GetCountryIdByName(string CountryName)
        {


            string Query = @"select CountryID from Countries
where CountryName = @CountryName ;";

            int countryId = 0;

            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);


            try
            {
                DataAccessSettinegs.Connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    countryId = Convert.ToInt32(result);
                }



            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }



            return countryId;

        }

        public static string GetCountryNameByID(int CountryID)
        {


            string Query = @"select CountryName from Countries
where  CountryID = @CountryID ;";

            string CountryName = "";

            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);


            try
            {
                DataAccessSettinegs.Connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    CountryName = Convert.ToString(result);
                }



            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }



            return CountryName;

        }

        public static bool UpdatePerson(int PersonID, ClsPerson NewPerson)
        {
            string Query = @"
UPDATE [dbo].[People]
SET 
      [NationalNo] = @NationalNo,
      [FirstName] = @FirstName,
      [SecondName] = @SecondName,
      [ThirdName] = @ThirdName,
      [LastName] = @LastName,
      [DateOfBirth] = @DateOfBirth,
      [Gendor] = @Gendor,
      [Address] = @Address,
      [Phone] = @Phone,
      [Email] = @Email,
      [NationalityCountryID] = @NationalityCountryID,
      [ImagePath] = @ImagePath
WHERE 
      [PersonID] = @PersonID";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NewPerson.NationalNo);
            command.Parameters.AddWithValue("@FirstName", NewPerson.FirstName);
            command.Parameters.AddWithValue("@SecondName", NewPerson.SecondName);
            command.Parameters.AddWithValue("@ThirdName", NewPerson.ThirdName);
            command.Parameters.AddWithValue("@LastName", NewPerson.LastName);
            command.Parameters.AddWithValue("@DateOfBirth", NewPerson.DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", NewPerson.Gendor);
            command.Parameters.AddWithValue("@Address", NewPerson.Address);
            command.Parameters.AddWithValue("@Phone", NewPerson.Phone);
            command.Parameters.AddWithValue("@Email", NewPerson.Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NewPerson.NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", NewPerson.ImagePath);
            

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

        public static bool IsNatonalNoExists(string NationalNo)
        {

            string Query = "select Bool=1 from People where NationalNo = @NationalNo";
            object Result = new object();
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                DataAccessSettinegs.Connection.Open();

                Result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }




            return Result != null;








        }
    }
}
