using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public static class UserBuisness
    {
        public class ClsUser
        {
            public string UserID = string.Empty;
            public string PersonID = string.Empty;
            public string UserName = string.Empty;
            public string Password = string.Empty;
            public int IsActive = 0;
        }

        public static ClsUser LoggedUser = null;
        

        public static string _SelectedUserIdGrid = string.Empty;

        private static UserData.ClsUser _BuisnessToDataClassConvert(UserBuisness.ClsUser User)
        {
            UserData.ClsUser User2 = new UserData.ClsUser();
            User2.UserID = User.UserID;
            User2.PersonID = User.PersonID;
            User2.UserName = User.UserName;
            User2.Password = User.Password;
            User2.IsActive = User.IsActive;

            return User2;
        }

        public static DataTable ListAllUsersInfo()
        {
            return UserData.ListAllUsersInfo();
        }

        public static DataTable ListAllUsersInfoByFilter(string FilterBy, string Value)
        {
            return UserData.ListAllUsersInfoByFilter( FilterBy,  Value);
        }

        public static int IsValidLogin(string UserName, string Password)
        {
            return UserData.IsValidLogin(UserName, Password);
        }

        public static ClsUser FindUserByID(string UserID)
        {
            DataTable dt = UserData.FindUserByID(UserID);
            DataRow row = dt.Rows[0];
            ClsUser User = new ClsUser();

            User.UserID = Convert.ToString(row["UserID"]);
            User.UserName = Convert.ToString(row["UserName"]);
            User.IsActive = Convert.ToInt32(row["IsActive"]);
            User.PersonID = Convert.ToString(row["PersonID"]);
            User.Password = Convert.ToString(row["Password"]);


            return User;

        }

        public static string FindUserIdByLoginInfo(string UserName, string Password)
        {
            return UserData.FindUserIdByLoginInfo(UserName, Password);
        }

        public static bool ChangeUserPassword(string UserID, string Password)
        {
           return UserData.ChangeUserPassword(UserID, Password); 
        }

        public static bool IsPersonUser(string PersonID)
        {
            return UserData.IsPersonUser(PersonID);
        }

        public static string AddNewUser(UserBuisness.ClsUser user)
        {
            return UserData.AddNewUser(_BuisnessToDataClassConvert(user));
        }

        public static bool DeleteUser(string UserID)
        {
            return UserData.DeleteUser(UserID);
        }

        public static bool UpdateUser(int UserID, ClsUser NewUser)
        {
            return UserData.UpdateUser(UserID, _BuisnessToDataClassConvert( NewUser ));
        }
    }
}
