using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static DVLD_DataAccess.Global_Data;
using static DVLD_DataAccess.UserData;

namespace DVLD_DataAccess
{
    public static class Local_DL_Data
    {

        

        public class clsAP
        {
            public string Local_DL_ID { get; set; }
            public string Local_DL_Class { get; set; }
            public string Local_DL_PassedTests { get; set; }
            public string ApplicationID { get; set; }
            public string ApplicantPersonID { get; set; }
            public DateTime ApplicationDate { get; set; }
            public string ApplicationTypeID { get; set; }
            public string ApplicationStatus { get; set; }
            public DateTime LastStatusDate { get; set; }
            public string PaidFees { get; set; }
            public string CreatedByUserID { get; set; }
        }

        public class clsApInfo
        {
            public string LDL_ID { get; set; }
            public string ApplicationID { get; set; }
            public string ApplicationsPaidFees { get; set; }
            public DateTime ApplicationDate { get; set; }
            public string ApplicationTypeTitle { get; set; }
            public string Applicant { get; set; }
            public string LicenseClasseName { get; set; }
            public string ApplicationStatusText { get; set; }

            public string ApplicationStatusNum { get; set; }
        }


        public static string AddNewLocalAP(clsAP AP, string licenseClassID)
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

            string query2 = @"
                INSERT INTO [dbo].[LocalDrivingLicenseApplications]
                           ([ApplicationID]
                           ,[LicenseClassID])
                     VALUES
                           (@ApplicationID, @LicenseClassID);
                SELECT SCOPE_IDENTITY();";

            int newApplicationID;
            string newLocalDrivingLicenseApplicationsID;

            SqlCommand command1 = new SqlCommand(query1, DataAccessSettinegs.Connection);


            command1.Parameters.AddWithValue("@ApplicantPersonID", AP.ApplicantPersonID);
            command1.Parameters.AddWithValue("@ApplicationDate", AP.ApplicationDate);
            command1.Parameters.AddWithValue("@ApplicationTypeID", AP.ApplicationTypeID);
            command1.Parameters.AddWithValue("@ApplicationStatus", AP.ApplicationStatus);
            command1.Parameters.AddWithValue("@LastStatusDate", AP.LastStatusDate);
            command1.Parameters.AddWithValue("@PaidFees", AP.PaidFees);
            command1.Parameters.AddWithValue("@CreatedByUserID", AP.CreatedByUserID);






            SqlCommand command2 = new SqlCommand(query2, DataAccessSettinegs.Connection);





            try
            {
                DataAccessSettinegs.Connection.Open();

                newApplicationID = Convert.ToInt32(command1.ExecuteScalar());

                command2.Parameters.AddWithValue("@ApplicationID", newApplicationID);
                command2.Parameters.AddWithValue("@LicenseClassID", licenseClassID);

                newLocalDrivingLicenseApplicationsID = Convert.ToString(command2.ExecuteScalar());
                return newLocalDrivingLicenseApplicationsID;

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

        public static string HasOpenLicenseApplication(string PeronID,string ClassID,int Status)
        {


            string Query = @"SELECT        dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
FROM            dbo.LocalDrivingLicenseApplications INNER JOIN
                         dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID
						 where ApplicantPersonID = @ApplicantPersonID 
						 and  LicenseClassID = @LicenseClassID
						 and ApplicationStatus =  @ApplicationStatus";


            object Result = null;


            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PeronID);
            command.Parameters.AddWithValue("@LicenseClassID", ClassID);
            command.Parameters.AddWithValue("@ApplicationStatus", Status);

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


            if (Result != null) 
            {
                return Convert.ToString(Result);
            }
            else
            {
                return null;
            }

            

        }

        public static DataTable ListAllLocal_Dl()
        {
            DataTable dt = new DataTable();

            string Query = @"


SELECT dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS [L.D.LAppID],
       dbo.LicenseClasses.ClassName AS [Driving Class],
       dbo.People.NationalNo,
       CONCAT(dbo.People.FirstName, ' ', dbo.People.SecondName, ' ', dbo.People.ThirdName, ' ', dbo.People.LastName) AS [Full Name],
       dbo.Applications.ApplicationDate,
       CASE
           WHEN dbo.Applications.ApplicationStatus = 1 THEN 'New'
           WHEN dbo.Applications.ApplicationStatus = 2 THEN 'Cancelled'
           WHEN dbo.Applications.ApplicationStatus = 3 THEN 'Completed'
       END AS [Application Status]
FROM dbo.LocalDrivingLicenseApplications
INNER JOIN dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID
INNER JOIN dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID
INNER JOIN dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID
WHERE dbo.Applications.ApplicationStatus IN (1, 3);




        
           



";

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

        private static string _UsersQueryFilterGenerator(string FilterBy)
        {
            string Query;

            Query = $@"SELECT dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID AS [L.D.LAppID], dbo.LicenseClasses.ClassName AS [Driving Class], dbo.People.NationalNo, CONCAT(dbo.People.FirstName, ' ', dbo.People.SecondName, ' ', dbo.People.ThirdName, ' ', dbo.People.LastName) AS [Full Name], dbo.Applications.ApplicationDate,
case
when dbo.Applications.ApplicationStatus = 1 then 'New'
when dbo.Applications.ApplicationStatus = 2 then 'Cancelled'
when dbo.Applications.ApplicationStatus = 3 then 'Completed'
end as [Application Status]



FROM dbo.LocalDrivingLicenseApplications INNER JOIN dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID INNER JOIN dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID INNER JOIN dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID
WHERE 
    {FilterBy} = @{FilterBy}
AND 
dbo.Applications.ApplicationStatus IN (1, 3);";





            return Query;
        }

        public static DataTable ListAllLocal_DlByFilter(string FilterBy, string Value)
        {
            DataTable dt = new DataTable();

            string Query = _UsersQueryFilterGenerator(FilterBy);

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

        public static bool UpdateLDL_ApStatus(string LDL_ID, string NewStatus)
        {
            string Query = @"UPDATE dbo.Applications
SET ApplicationStatus = @ApplicationStatus
FROM dbo.LocalDrivingLicenseApplications
INNER JOIN dbo.Applications
ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID
WHERE dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;
";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDL_ID);



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

        public static string GetApStatus(string ApID)
        {
            string Query = @"SELECT         dbo.Applications.ApplicationStatus
FROM            dbo.Applications INNER JOIN
                         dbo.LocalDrivingLicenseApplications ON dbo.Applications.ApplicationID = dbo.LocalDrivingLicenseApplications.ApplicationID
						 where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";


            object Result = null;


            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", ApID);


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


            if (Result != null)
            {
                return Convert.ToString(Result);
            }
            else
            {
                return null;
            }
        }

        public static bool DeleteLDLAp(string LDL_ID)
        {

            string Query = @"DELETE FROM [dbo].[LocalDrivingLicenseApplications]
      WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDL_ID);

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

        public static clsApInfo GetAPInfoByID(string AP_ID)
        {
            string query = @"SELECT 
    dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID as LDL_ID, 
    dbo.Applications.ApplicationID, 
    dbo.Applications.PaidFees , 
    dbo.Applications.ApplicationDate, 
    dbo.ApplicationTypes.ApplicationTypeTitle ,  
    dbo.LicenseClasses.ClassName,
    -- Case for ApplicationStatus
    CASE 
        WHEN dbo.Applications.ApplicationStatus = 1 THEN 'New'
        WHEN dbo.Applications.ApplicationStatus = 2 THEN 'Cancelled'
        WHEN dbo.Applications.ApplicationStatus = 3 THEN 'Completed'
        ELSE 'Unknown'
    END AS ApplicationStatus,
    -- Combine name columns into FullName
    CONCAT(dbo.People.FirstName, ' ', dbo.People.SecondName, ' ', dbo.People.ThirdName, ' ', dbo.People.LastName) AS Applicant
FROM 
    dbo.Applications
INNER JOIN 
    dbo.LocalDrivingLicenseApplications 
    ON dbo.Applications.ApplicationID = dbo.LocalDrivingLicenseApplications.ApplicationID
INNER JOIN 
    dbo.People 
    ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID
INNER JOIN 
    dbo.ApplicationTypes 
    ON dbo.Applications.ApplicationTypeID = dbo.ApplicationTypes.ApplicationTypeID
INNER JOIN 
    dbo.LicenseClasses 
    ON dbo.LocalDrivingLicenseApplications.LicenseClassID = dbo.LicenseClasses.LicenseClassID
	where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", AP_ID);

            clsApInfo AP = new clsApInfo();

            try
            {
                DataAccessSettinegs.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) 
                {
                    AP.LDL_ID = Convert.ToString(reader["LDL_ID"]);
                    AP.ApplicationID = Convert.ToString(reader["ApplicationID"]);
                    AP.ApplicationsPaidFees = Convert.ToString ( Convert.ToInt32(reader["PaidFees"]) );
                    AP.ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    AP.ApplicationTypeTitle = Convert.ToString(reader["ApplicationTypeTitle"]);
                    AP.LicenseClasseName = Convert.ToString(reader["ClassName"]);
                    AP.ApplicationStatusText = Convert.ToString(reader["ApplicationStatus"]);
                    AP.Applicant = Convert.ToString(reader["Applicant"]);
                }
                else
                {
                    throw new Exception("No data found for the given ID.");
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

            return AP;
        }

        public static string GetTestTypeFees(string TestID)
        {
            string query = @"select TestTypeFees from TestTypes
where TestTypeID = @TestID";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestID", TestID);

            

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

        public static bool CheckIfHaveAppointment(string LDL_ID, string TestID)
        {
            string query = @"select bool=1 from TestAppointments
where TestTypeID = @TestTypeID
and LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
and IsLocked = '0' ;";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDL_ID);

            clsApInfo AP = new clsApInfo();

            try
            {
                DataAccessSettinegs.Connection.Open();

                object result = command.ExecuteScalar();

                return result != null;






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

        public static DataTable GetAllTestAppointments(string LDL_ID, string TestID)
        {
            string Query = @"SELECT        dbo.TestAppointments.TestAppointmentID, dbo.TestAppointments.AppointmentDate, dbo.TestTypes.TestTypeFees, dbo.TestAppointments.IsLocked
FROM            dbo.TestAppointments INNER JOIN
                         dbo.TestTypes ON dbo.TestAppointments.TestTypeID = dbo.TestTypes.TestTypeID
where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
and dbo.TestAppointments.TestTypeID = @TestTypeID
  ;";

            DataTable dt = new DataTable();

            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDL_ID);
            command.Parameters.AddWithValue("@TestTypeID", TestID);

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

        public static bool AddNewTestAppointment(string LDL_ID, string TestID, string PaidFees,
             DateTime AppointmentDate , string CreatedByUserID , string RetakeTestApplicationID)
        {
            


            string query = @"INSERT INTO [dbo].[TestAppointments]
           ([TestTypeID]
           ,[LocalDrivingLicenseApplicationID]
           ,[AppointmentDate]
           ,[PaidFees]
           ,[CreatedByUserID]
           ,[IsLocked]
           ,[RetakeTestApplicationID])
     VALUES
           (@TestTypeID
           ,@LocalDrivingLicenseApplicationID
           ,@AppointmentDate
           ,@PaidFees
           ,@CreatedByUserID
           ,@IsLocked
           ,@RetakeTestApplicationID);
";

            int rowEffected = 0;

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDL_ID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@RetakeTestApplicationID",
    string.IsNullOrEmpty(RetakeTestApplicationID) ? (object)DBNull.Value : RetakeTestApplicationID);
            command.Parameters.AddWithValue("@IsLocked", 0);
            


            try
            {
                DataAccessSettinegs.Connection.Open();

               rowEffected=  command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }


            return rowEffected > 0;
            
        }

        public static bool UpdateTestAppointment(string TestAppointmentID , DateTime NewDate)
        {
            string Query = @"UPDATE dbo.TestAppointments
SET AppointmentDate = @NewAppointmentDate
WHERE TestAppointmentID = @TestAppointmentID; 
  ";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            
            command.Parameters.AddWithValue("@NewAppointmentDate", NewDate);



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

        public static DateTime GetTestAppointmentDate(string TestAppointmentID)
        {
            string Query = @"select AppointmentDate from TestAppointments
where TestAppointmentID = @TestAppointmentID";


            object Result = null;


            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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


            if (Result != null)
            {
                return Convert.ToDateTime(Result);
            }
            else
            {
                throw new Exception("No Date Time");
            }
        }

        public static bool IsTestAppointmentLocked(string TestAppointmentID)
        {
            string Query = @"select bool=1 from TestAppointments
where TestAppointmentID = @TestAppointmentID
and IsLocked = 1";


            


            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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

        public static string TakeTest(string TestAppointmentID,string TestResult , string Notes , string CreatedByUserID)
        {
            string query = @"INSERT INTO [dbo].[Tests]
           ([TestAppointmentID]
           ,[TestResult]
           ,[Notes]
           ,[CreatedByUserID])
     VALUES
           (@TestAppointmentID
           ,@TestResult
           ,@Notes
           ,@CreatedByUserID);

           SELECT SCOPE_IDENTITY();
";

            string TestID = "";

            

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);




            try
            {
                DataAccessSettinegs.Connection.Open();

                TestID = Convert.ToString ( command.ExecuteScalar() );


            }


            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }

            return TestID;
            
        }


        public static bool MakeTestAppointmentLocked(string TestAppointmentID)
        {
            string Query = @"UPDATE [dbo].[TestAppointments]
   SET 
     [IsLocked] = 1

 WHERE TestAppointmentID = @TestAppointmentID; 
  ";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            



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

        public static bool IsPassedTest(string LDL_ID, string TestTypeID)
        {
            string Query = @"
SELECT        bool=1
FROM            dbo.LocalDrivingLicenseApplications INNER JOIN
                         dbo.TestAppointments ON dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = dbo.TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                         dbo.Tests ON dbo.TestAppointments.TestAppointmentID = dbo.Tests.TestAppointmentID INNER JOIN
                         dbo.TestTypes ON dbo.TestAppointments.TestTypeID = dbo.TestTypes.TestTypeID
						 where dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
						 and dbo.Tests.TestResult = 1
						 and dbo.TestTypes.TestTypeID = @TestTypeID";





            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LDL_ID);

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

        public  static DataTable GetAllLocalLiceses(string PersonID)
        {
            string Query = @"SELECT        dbo.Licenses.LicenseID, dbo.Licenses.DriverID, dbo.LicenseClasses.ClassName, dbo.Licenses.IssueDate, dbo.Licenses.ExpirationDate, dbo.Licenses.IsActive 
FROM            dbo.LicenseClasses INNER JOIN
                         dbo.Licenses ON dbo.LicenseClasses.LicenseClassID = dbo.Licenses.LicenseClass INNER JOIN
                         dbo.Drivers ON dbo.Licenses.DriverID = dbo.Drivers.DriverID
						 where dbo.Drivers.PersonID = @PersonID
  ;";

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

        public static DataTable GetAllGlobalLiceses(string PersonID)
        {
            string Query = @"SELECT        dbo.InternationalLicenses.InternationalLicenseID,dbo.Licenses.LicenseID, dbo.Licenses.DriverID, dbo.LicenseClasses.ClassName, dbo.Licenses.IssueDate, dbo.Licenses.ExpirationDate, dbo.Licenses.IsActive 
                         
FROM            dbo.LicenseClasses INNER JOIN
                         dbo.Licenses ON dbo.LicenseClasses.LicenseClassID = dbo.Licenses.LicenseClass INNER JOIN
                         dbo.InternationalLicenses ON dbo.Licenses.LicenseID = dbo.InternationalLicenses.IssuedUsingLocalLicenseID INNER JOIN
                         dbo.Drivers ON dbo.Licenses.DriverID = dbo.Drivers.DriverID AND dbo.InternationalLicenses.DriverID = dbo.Drivers.DriverID
						 where dbo.Drivers.PersonID = @PersonID
  ;";

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

        public static string GetPersonIdByLDL(string LDL_ID)
        {
            string query = @"SELECT        dbo.People.PersonID
FROM            dbo.LocalDrivingLicenseApplications INNER JOIN
                         dbo.Applications ON dbo.LocalDrivingLicenseApplications.ApplicationID = dbo.Applications.ApplicationID INNER JOIN
                         dbo.People ON dbo.Applications.ApplicantPersonID = dbo.People.PersonID
						 where LocalDrivingLicenseApplicationID = @LDL_ID";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LDL_ID", LDL_ID);



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

        public static bool IsDriver(string PersonID)
        {
            string Query = @"
                select bool=1 from Drivers
                where PersonID = @PersonID
";





            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            

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

        public static bool IsHaveTheLicense(string PersonID, string LicenseClassID)
        {
            string Query = @"SELECT        bool  = 1
                            FROM            dbo.Licenses INNER JOIN
                         dbo.Drivers ON dbo.Licenses.DriverID = dbo.Drivers.DriverID
						 where dbo.Drivers.PersonID = @PersonID
						 and LicenseClass = @LicenseClassID;
";





            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static string GetLicenseClassID(string LDL_ID)
        {
            string query = @"select LicenseClassID from LocalDrivingLicenseApplications
where LocalDrivingLicenseApplicationID = @LDL_ID ";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LDL_ID", LDL_ID);



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

        public static string GetApplicationID(string LDL_ID)
        {
            string query = @"select ApplicationID from LocalDrivingLicenseApplications		
where LocalDrivingLicenseApplicationID = @LDL_ID ";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@LDL_ID", LDL_ID);



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

        public static string GetApplicationFess(string AP_ID)
        {
            string query = @"		
select ApplicationFees from ApplicationTypes
where ApplicationTypeID = @AP_ID ";

            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@AP_ID", AP_ID);



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
