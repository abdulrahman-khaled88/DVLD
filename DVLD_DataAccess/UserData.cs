using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static DVLD_DataAccess.PepoleData;


namespace DVLD_DataAccess
{
    public static class UserData
    {
        public class ClsUser
        {
            public string UserID = string.Empty;
            public string PersonID = string.Empty;
            public string UserName = string.Empty;
            public string Password = string.Empty;
            public int IsActive = 0;
        }

        

        public static int IsValidLogin(string UserName , string Password)
        {
            string Qurey = @"select IsActive from Users
                             where UserName = @UserName and Password = @Password ";

            int IsActive = -1;



            SqlCommand command = new SqlCommand(Qurey,DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                DataAccessSettinegs.Connection.Open();

                object Result = command.ExecuteScalar();


                if (Result != null)
                {
                    IsActive = Convert.ToInt32(Result);

                }


                DataAccessSettinegs.Connection.Close();
                return IsActive ;

            }
            catch(Exception ex) 
            {
                return IsActive;

                throw new Exception("An error occurred in the Data Access Layer.", ex);
            }

        }

        public static string FindUserIdByLoginInfo(string UserName, string Password)
        {
            string Qurey = @"select UserID from Users
                             where UserName = @UserName and Password = @Password ";

            string UserID  = string.Empty;



            SqlCommand command = new SqlCommand(Qurey, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                DataAccessSettinegs.Connection.Open();

                object Result = command.ExecuteScalar();


                if (Result != null)
                {
                    UserID = Convert.ToString(Result);
                }


                DataAccessSettinegs.Connection.Close();
                return UserID;

            }
            catch (Exception ex)
            {
                return UserID;

                throw new Exception("An error occurred in the Data Access Layer.", ex);
            }

        }

       
        public static DataTable ListAllUsersInfo()
        {
            DataTable dt = new DataTable();

            string Query = @"SELECT dbo.Users.UserID, 
       dbo.Users.PersonID, 
       dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + dbo.People.ThirdName + ' ' + dbo.People.LastName AS FullName, 
       dbo.Users.UserName, 
       dbo.Users.IsActive
FROM dbo.Users 
INNER JOIN dbo.People 
ON dbo.Users.PersonID = dbo.People.PersonID;";

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

            if (FilterBy == "FullName")
            {
                
                Query = $@"SELECT dbo.Users.UserID, 
       dbo.Users.PersonID, 
       dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + dbo.People.ThirdName + ' ' + dbo.People.LastName AS FullName, 
       dbo.Users.UserName, 
       dbo.Users.IsActive
FROM dbo.Users 
INNER JOIN dbo.People 
ON dbo.Users.PersonID = dbo.People.PersonID
WHERE dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + dbo.People.ThirdName + ' ' + dbo.People.LastName = @{FilterBy};
";
            }
            else
            {
                Query = $@"SELECT dbo.Users.UserID, 
       dbo.Users.PersonID, 
       dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + dbo.People.ThirdName + ' ' + dbo.People.LastName AS FullName, 
       dbo.Users.UserName, 
       dbo.Users.IsActive
FROM dbo.Users 
INNER JOIN dbo.People 
ON dbo.Users.PersonID = dbo.People.PersonID
        WHERE 
            dbo.Users.{FilterBy} = @{FilterBy};";
            }
             

            return Query;
        }

        public static DataTable ListAllUsersInfoByFilter(string FilterBy, string Value)
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

        public static DataTable FindUserByID( string UserID)
        {
            DataTable dt = new DataTable();

            string Query = "select * from Users where UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool ChangeUserPassword(string UserID, string Password)
        {
            string Query = @"UPDATE [dbo].[Users]
                        SET
                        [Password] = @Password
      
                        WHERE UserID = @UserID";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                DataAccessSettinegs.Connection.Open();

                RowsEffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }


            return RowsEffected > 0;
        }

        public static bool IsPersonUser(string PersonID)
        {
            string Qurey = "select bool=1 from Users where PersonID = @PersonID;";

            



            SqlCommand command = new SqlCommand(Qurey, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            

            try
            {
                DataAccessSettinegs.Connection.Open();

                object Result = command.ExecuteScalar();

                DataAccessSettinegs.Connection.Close();

                return Result != null;
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred in the Data Access Layer.", ex);
                
            }
        }

        public static string AddNewUser(UserData.ClsUser user)
        {
            string UserID = null;


            string query = @"INSERT INTO [dbo].[Users]
           ([PersonID]
           ,[UserName]
           ,[Password]
           ,[IsActive])
     VALUES
           (@PersonID, 
           @UserName, 
           @Password, 
           @IsActive);
    SELECT SCOPE_IDENTITY();";

            
            


            SqlCommand command = new SqlCommand(query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@PersonID",user.PersonID);
            command.Parameters.AddWithValue("@UserName", user.UserName);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@IsActive", user.IsActive);

            try
            {
                DataAccessSettinegs.Connection.Open();

                UserID = Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            finally
            {
                DataAccessSettinegs.Connection.Close();
            }



            return UserID;
        }

        public static bool DeleteUser(string UserID)
        {
            string Query = @"DELETE FROM [dbo].[Users]
                                WHERE UserID = @UserID;";
            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool UpdateUser(int UserID, ClsUser NewUser)
        {
            string Query = @"UPDATE [dbo].[Users]
   SET 
      [UserName] = @UserName,
      [Password] = @Password,
      [IsActive] = @IsActive
 WHERE UserID = @UserID;
";



            int RowsEffected = 0;
            SqlCommand command = new SqlCommand(Query, DataAccessSettinegs.Connection);

            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", NewUser.Password);
            command.Parameters.AddWithValue("@UserName", NewUser.UserName);
            command.Parameters.AddWithValue("@IsActive", NewUser.IsActive);



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
