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
    public partial class frmChangeUserPassword : Form
    {
        private UserBuisness.ClsUser _User = new UserBuisness.ClsUser();
        public frmChangeUserPassword(UserBuisness.ClsUser User)
        {
            InitializeComponent();
            _User = User;
        }

        private void _SetUserInfo()
        {
            PepoleBuisness.ClsPerson person =
                PepoleBuisness.FindPersonByID(Convert.ToString(_User.PersonID));

            labelPersonID.Text = Convert.ToString(person.ID);
            labelName.Text = $"{person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}";
            labelNationalNo.Text = person.NationalNo;
            labelGendor.Text = person.GendorText;
            labelEmail.Text = person.Email;
            labelPhone.Text = person.Phone;
            labelAddress.Text = person.Address;
            labelCountry.Text = person.CountryName;
            labelDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            labelUserID.Text = Convert.ToString(_User.UserID);
            labelUserName.Text = _User.UserName;
            labelIsActive.Text = _User.IsActive == 1 ? "Yes" : "No";

            if (!string.IsNullOrEmpty(person.ImagePath))
            {
                pictureBox1.Image = Image.FromFile($@"{person.ImagePath}");
            }
            else
            {
                if (person.GendorText == "Male")
                {
                    pictureBox1.Image = Image.FromFile(@"D:\Desktop\Icons\Male 512.png");
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(@"D:\Desktop\Icons\Female 512.png");
                }
            }




        }

        private bool _IsPasswordRight()
        {
            return textBoxCurrentPassword.Text == _User.Password;
        }

        private bool _CheckEmptyBoxes()
        {
            
        
            bool IsTrue = false;

            foreach (Control control in groupBox1.Controls)
            {
                if (control is TextBoxBase textBoxBase && string.IsNullOrEmpty(textBoxBase.Text))
                {
                    errorProvider1.SetError(control, "Can't Be Empty!");
                    IsTrue = true;
                }

            }

            return IsTrue;
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChangeUserPassword_Load(object sender, EventArgs e)
        {
            _SetUserInfo();
        }

        private void textBoxCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            if (!_IsPasswordRight())
            {
                errorProvider1.SetError(textBoxCurrentPassword, "Current password is wrong!");
            }
            else if (!string.IsNullOrEmpty(textBoxCurrentPassword.Text))
            {
                _ClearErrorProvider(textBoxCurrentPassword);
            }
        }

        private void _ClearErrorProvider(Control Control)
        {
            errorProvider1.SetError(Control, "");
        }

        private void textBoxNewPassword_TextChanged(object sender, EventArgs e)
        {
             if (!string.IsNullOrEmpty(textBoxNewPassword.Text))
            {
                _ClearErrorProvider(textBoxNewPassword);
            }
        }

        private void textBoxConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxNewPassword.Text))
            {
                _ClearErrorProvider(textBoxNewPassword);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!_CheckEmptyBoxes() && _IsPasswordRight()) 
            {
                if (UserBuisness.ChangeUserPassword
                    ( Convert.ToString(_User.UserID), textBoxNewPassword.Text ))
                {
                    MessageBox.Show
                        ("Password changed successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show
                        ("Failed to change password. Please try again.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
