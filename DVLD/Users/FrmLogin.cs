using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD.Users
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private static int _LoginResult = -1;
        private enum _LoginStatus
        {
            RongUsername_Password = -1,
            AccountDeactivated = 0,
            Success = 1,
        }

        private void _SetCurrentLoginIfo()
        {


            UserBuisness.LoggedUser =
                UserBuisness.FindUserByID
                (Convert.ToString 
                (Convert.ToInt32
                (UserBuisness.FindUserIdByLoginInfo
                (textBoxUserName.Text, textBoxPassword.Text))));
        }

        private void _Login()
        {
            
            _LoginResult = UserBuisness.IsValidLogin(textBoxUserName.Text, textBoxPassword.Text);

            _LoginStatus Status = (_LoginStatus)_LoginResult;



            if (Status == _LoginStatus.Success)
            {


                _SetCurrentLoginIfo();


                frmMain frm = new frmMain();

                frm.ShowDialog();
                
            }
            else if (Status == _LoginStatus.RongUsername_Password)
            {
                MessageBox.Show
                ("Invalid Username/Password.", "Wrong Credintials"
                , MessageBoxButtons.OK
                , MessageBoxIcon.Error);
            }
            else if (Status == _LoginStatus.AccountDeactivated)
            {
                MessageBox.Show
                ("Your account is deactivated. Please contact support for assistance.",
                "Account Deactivated"
               , MessageBoxButtons.OK
               , MessageBoxIcon.Error);
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            _Login();
        }
            
            

            
            
        
    }
}
