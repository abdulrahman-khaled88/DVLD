using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static DVLD_DataAccess.Local_DL_Data;

namespace DVLD_DataAccess
{
    public static class LicensesData
    {

        public class clsLicenseInfo
        {
            public string Class {  get; set; }
            public string Name { get; set; }
            public string LicenseID { get; set; }
            public string NationalNo { get; set; }
            public string Gendor { get; set; }
            public DateTime IssueDate { get; set; }
            public string IsActive { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string DriverID { get; set; }
            public DateTime ExpirationDate { get; set; }

            public string ImagePath { get; set; }
        }
        
        public static string AddNewLicense(string ApplicationID, string DriverID ,
            string LicenseClass,DateTime IssueDate, DateTime ExpirationDate,
           string notes, string paidFees, string IsActive,
           string IssueReason,string CreatedByUserID)
        {
            string LicenseID = null;


            string query = @"INSERT INTO [dbo].[Licenses]
                     ([ApplicationID],
                      [DriverID],
                      [LicenseClass],
                      [IssueDate],
                      [ExpirationDate],
                      [Notes],
                      [PaidFees],
                      [IsActive],
                      [IssueReason],
                      [CreatedByUserID])
                     VALUES
                     (@ApplicationID,
                      @DriverID,
                      @LicenseClass,
                      @IssueDate,
                      @ExpirationDate,
                      @Notes,
                      @PaidFees,
                      @IsActive,
                      @IssueReason,
                      @CreatedByUserID);
                        SELECT SCOPE_IDENTITY();";





            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", notes);
            command.Parameters.AddWithValue("@PaidFees", paidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                DataAccessSettinegs.Connection.Open();

                LicenseID = Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }



            return LicenseID;
        }



        public static clsLicenseInfo GetLicenseInfo(string LicenseID) 
        {

            string query = @"SELECT 
    dbo.LicenseClasses.ClassName,
    CONCAT(dbo.People.FirstName, ' ', dbo.People.SecondName, ' ', dbo.People.ThirdName, ' ', dbo.People.LastName) AS [Full Name],
    dbo.Licenses.IssueDate,
    dbo.Licenses.ExpirationDate,
    CASE 
        WHEN dbo.Licenses.IsActive = 0 THEN 'No'
        WHEN dbo.Licenses.IsActive = 1 THEN 'Yes'
    END AS [Is Active Status],
    dbo.Drivers.DriverID,
    dbo.People.ImagePath,
	dbo.People.NationalNo,
    dbo.People.DateOfBirth,
    CASE 
        WHEN dbo.People.Gendor = 0 THEN 'Male'
        WHEN dbo.People.Gendor = 1 THEN 'Female'
    END AS [Gender]
FROM 
    dbo.Licenses
INNER JOIN 
    dbo.LicenseClasses ON dbo.Licenses.LicenseClass = dbo.LicenseClasses.LicenseClassID
INNER JOIN 
    dbo.Drivers ON dbo.Licenses.DriverID = dbo.Drivers.DriverID
INNER JOIN 
    dbo.People ON dbo.Drivers.PersonID = dbo.People.PersonID
where dbo.Licenses.LicenseID = @LicenseID;";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            clsLicenseInfo licenseInfo = new clsLicenseInfo();

            try
            {
                DataAccessSettinegs.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // Populate the clsLicenseInfo object from the SqlDataReader
                    licenseInfo.Class = reader["ClassName"].ToString();
                    licenseInfo.Name = reader["Full Name"].ToString();
                    licenseInfo.NationalNo = reader["NationalNo"]?.ToString(); // Assuming NationalNo is part of your query
                    licenseInfo.Gendor = reader["Gender"].ToString();
                    licenseInfo.IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    licenseInfo.IsActive = reader["Is Active Status"].ToString();
                    licenseInfo.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    licenseInfo.DriverID = reader["DriverID"].ToString();
                    licenseInfo.ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    licenseInfo.ImagePath = reader["ImagePath"].ToString();

                }
                else
                {
                    return null;
                }



                reader.Close();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();

            }

            return licenseInfo;



        }


        public static bool IsLicenseDetan(string LicenseID)
        {
            string Query = @"
select bool=1 from DetainedLicenses
where LicenseID = @LicenseID
and IsReleased = 0
";





            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                DataAccessSettinegs.Connection.Open();

                object Result = command.ExecuteScalar();

                return Result != null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }


        }


        public static bool IsHaveInternationalLicense(string LicenseID)
        {
            string Query = @"
SELECT        bool=1
FROM            dbo.Licenses INNER JOIN
                         dbo.InternationalLicenses ON dbo.Licenses.LicenseID = dbo.InternationalLicenses.IssuedUsingLocalLicenseID
						 where dbo.Licenses.LicenseID = @LicenseID;
";





            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                DataAccessSettinegs.Connection.Open();

                object Result = command.ExecuteScalar();

                return Result != null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }


        }


        public static string AddNewInternationalLicense(Local_DL_Data.clsAP AP,
             string driverID, string issuedUsingLocalLicenseID,
            DateTime issueDate, DateTime expirationDate,int isActive,string createdByUserID)
        {




            string query1 = @"
                INSERT INTO [dbo].[Applications]
                           ([ApplicantPersonID]
                           ,[ApplicationDate]
                           ,[ApplicationTypeID]
                           ,[ApplicationStatus]
                           ,[LastStatusDate]
                           ,[PaidFees]
                           ,[CreatedByUserID])
                     VALUES
                           (@ApplicantPersonID
                           ,@ApplicationDate
                           ,@ApplicationTypeID
                           ,@ApplicationStatus
                           ,@LastStatusDate
                           ,@PaidFees
                           ,@CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

            string query2 = @"INSERT INTO [dbo].[InternationalLicenses]
           ([ApplicationID]
           ,[DriverID]
           ,[IssuedUsingLocalLicenseID]
           ,[IssueDate]
           ,[ExpirationDate]
           ,[IsActive]
           ,[CreatedByUserID])
     VALUES
           (@ApplicationID
           ,@DriverID
           ,@IssuedUsingLocalLicenseID
           ,@IssueDate
           ,@ExpirationDate
           ,@IsActive
           ,@CreatedByUserID);
            SELECT SCOPE_IDENTITY();";

            string newApplicationID;
            string newInternationalLicenseID;

            //Add New Application
            SqlCommand command1 = new SqlCommand(query1, DataAccessSettinegs.Connection);
            command1.Parameters.AddWithValue("@ApplicantPersonID", AP.ApplicantPersonID);
            command1.Parameters.AddWithValue("@ApplicationDate", AP.ApplicationDate);
            command1.Parameters.AddWithValue("@ApplicationTypeID", AP.ApplicationTypeID);
            command1.Parameters.AddWithValue("@ApplicationStatus", AP.ApplicationStatus);
            command1.Parameters.AddWithValue("@LastStatusDate", AP.LastStatusDate);
            command1.Parameters.AddWithValue("@PaidFees", AP.PaidFees);
            command1.Parameters.AddWithValue("@CreatedByUserID", AP.CreatedByUserID);




            







            try
            {
                DataAccessSettinegs.Connection.Open();


                newApplicationID = Convert.ToString(command1.ExecuteScalar());


                //Add New International License
                SqlCommand command2 = new SqlCommand(query2, DataAccessSettinegs.Connection);
                command2.Parameters.AddWithValue("@ApplicationID", newApplicationID);
                command2.Parameters.AddWithValue("@DriverID", driverID);
                command2.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", issuedUsingLocalLicenseID);
                command2.Parameters.AddWithValue("@IssueDate", issueDate);
                command2.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                command2.Parameters.AddWithValue("@IsActive", isActive);
                command2.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                       
                newInternationalLicenseID = Convert.ToString(command2.ExecuteScalar());
                



            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }



            return newInternationalLicenseID;

        }


        public static string AddNewApplication(Local_DL_Data.clsAP AP)
        {



            string query = @"
                INSERT INTO [dbo].[Applications]
                           ([ApplicantPersonID]
                           ,[ApplicationDate]
                           ,[ApplicationTypeID]
                           ,[ApplicationStatus]
                           ,[LastStatusDate]
                           ,[PaidFees]
                           ,[CreatedByUserID])
                     VALUES
                           (@ApplicantPersonID
                           ,@ApplicationDate
                           ,@ApplicationTypeID
                           ,@ApplicationStatus
                           ,@LastStatusDate
                           ,@PaidFees
                           ,@CreatedByUserID);
                SELECT SCOPE_IDENTITY();";

            string newApplicationID ;

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            //Add New Application
            SqlCommand command1 = new SqlCommand(query, DataAccessSettinegs.Connection);
            command1.Parameters.AddWithValue("@ApplicantPersonID", AP.ApplicantPersonID);
            command1.Parameters.AddWithValue("@ApplicationDate", AP.ApplicationDate);
            command1.Parameters.AddWithValue("@ApplicationTypeID", AP.ApplicationTypeID);
            command1.Parameters.AddWithValue("@ApplicationStatus", AP.ApplicationStatus);
            command1.Parameters.AddWithValue("@LastStatusDate", AP.LastStatusDate);
            command1.Parameters.AddWithValue("@PaidFees", AP.PaidFees);
            command1.Parameters.AddWithValue("@CreatedByUserID", AP.CreatedByUserID);


            try
            {
                DataAccessSettinegs.Connection.Open();

                newApplicationID = Convert.ToString(command1.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }


            return newApplicationID;

        }


        public static bool DeActiveLicence(string LicenseID)
        {
            string Query = @"UPDATE [dbo].[Licenses]
   SET 
      [IsActive] = 0

 WHERE LicenseID = @LicenseID;

";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);




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

        public static string DetanLicence
            (string licenseID, DateTime detainDate, string fineFees, string createdByUserID, int isReleased)
        {



            string query = @"
    INSERT INTO [dbo].[DetainedLicenses]
               ([LicenseID]
               ,[DetainDate]
               ,[FineFees]
               ,[CreatedByUserID]
               ,[IsReleased])
         VALUES
               (@LicenseID
               ,@DetainDate
               ,@FineFees
               ,@CreatedByUserID
               ,@IsReleased);
                SELECT SCOPE_IDENTITY();";

            string DetanID;



            //Add New Detan
            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);
            command.Parameters.AddWithValue("@LicenseID", licenseID); 
            command.Parameters.AddWithValue("@DetainDate", detainDate); 
            command.Parameters.AddWithValue("@FineFees", fineFees); 
            command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID); 
            command.Parameters.AddWithValue("@IsReleased", isReleased); 


            try
            {
                DataAccessSettinegs.Connection.Open();

                DetanID = Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }


            return DetanID;

        }


        public static bool ReleaseLicence(int isReleased,DateTime releaseDate,string releasedByUserID,string licenseID)
        {
            string Query = @"UPDATE [dbo].[DetainedLicenses]
   SET [IsReleased] = @IsReleased,
       [ReleaseDate] = @ReleaseDate,
       [ReleasedByUserID] = @ReleasedByUserID
        where LicenseID = @LicenseID;
";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);
            command.Parameters.AddWithValue("@IsReleased", isReleased); 
            command.Parameters.AddWithValue("@ReleaseDate", releaseDate); 
            command.Parameters.AddWithValue("@ReleasedByUserID", releasedByUserID); 
            command.Parameters.AddWithValue("@LicenseID", licenseID); 



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

        public static bool ActiveateLicence(string LicenseID)
        {
            string Query = @"UPDATE [dbo].[Licenses]
   SET 
      [IsActive] = 1

 WHERE LicenseID = @LicenseID;

";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);




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
    }
}
