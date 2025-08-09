using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Pepole;
using DVLD_Buisness;

namespace DVLD.Users
{
    public partial class AddNewUser : Form
    {
        public enum Mode
        {
            Update = 1,
            AddNew = 2,
        }

        private Mode _currentMode;

        

        public AddNewUser(Mode mode)
        {
            InitializeComponent();
            _currentMode = mode;
        }

        private PepoleBuisness.ClsPerson  _GetPersonInfo()
        {
            PepoleBuisness.ClsPerson person = comboBoxFilter.SelectedIndex == 0 ?
            PepoleBuisness.FindPersonByID(textBoxFilter.Text) :
            PepoleBuisness.FindPersonByNationalNo(textBoxFilter.Text);

            return person;
        }

        private void _SetUserInfo()
        {
            PepoleBuisness.ClsPerson person = _GetPersonInfo();

            if (PepoleBuisness.IsPersoFound(person))
            {
                labelPersonID.Text = Convert.ToString(person.ID);
                labelName.Text = $"{person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}";
                labelNationalNo.Text = person.NationalNo;
                labelGendor.Text = person.GendorText;
                labelEmail.Text = person.Email;
                labelPhone.Text = person.Phone;
                labelAddress.Text = person.Address;
                labelCountry.Text = person.CountryName;
                labelDateOfBirth.Text = person.DateOfBirth.ToShortDateString();


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
            else
            {
                MessageBox.Show("The person  not be found.", "Person Not Found"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }






        }

        private void buttonAddNewPerson_Click(object sender, EventArgs e)
        {
            FrmAddNewPerson frm = new FrmAddNewPerson();
            frm.ShowDialog();
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            _SetUserInfo();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _CheckPersonID()
        {
            if (labelPersonID.Text != "[???]")
            {
                if (!UserBuisness.IsPersonUser(labelPersonID.Text))
                {
                    tabControlPage1.SelectedIndex = 1;
                    textBoxConfirmPassword.Enabled = true;
                    textBoxUserName.Enabled = true;
                    textBoxPassword.Enabled = true;
                    buttonSave.Enabled = true;
                    checkBoxIsActive.Enabled = true;

                }
                else
                {
                    MessageBox.Show("This person already has a user account.", "Select another Person",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("The person  not be found.", "Person Not Found"
                                , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if( _currentMode ==Mode.AddNew)
            {
                _CheckPersonID();
            }
            else if (_currentMode == Mode.Update)
            {
                tabControlPage1.SelectedIndex = 1;
                buttonSave.Text = "Update";
            }


        }

        private void _ClearErrorProvider(Control Control)
        {
            errorProvider1.SetError(Control, "");
        }

        private void _SetErrorProvider(Control Control)
        {
            errorProvider1.SetError(Control, "Can't Be Empty!");
        }

        private void _CheckEmptyTextBox(Control Control)
        {
            if (string.IsNullOrEmpty(Control.Text))
            {
                _SetErrorProvider(Control);
            }
            else
            {
                _ClearErrorProvider(Control);
            }
        }

        private bool _AreTextBoxesEmpty()
        {
            bool IsTrue = false;

            foreach (Control control in groupBox1.Controls)
            {
                if (control is TextBoxBase textBoxBase && string.IsNullOrEmpty(textBoxBase.Text))
                {
                    IsTrue = true;
                }

            }

            return IsTrue;
        }

        private bool _IsInfoRight()
        {
            if(!_AreTextBoxesEmpty() && CheckConfirmPassword() )
            {
                return true;
            }
            else
            {
                return false;
            }





        }

        private void _AddNewUser()
        {
            if (_IsInfoRight())
            {
                UserBuisness.ClsUser User = new UserBuisness.ClsUser();
                User.UserName = textBoxUserName.Text;
                User.Password = textBoxPassword.Text;
                User.PersonID = labelPersonID.Text;
                User.IsActive = checkBoxIsActive.Checked ? 1 : 0;
               string UserID = UserBuisness.AddNewUser(User);

                if (UserID != null)
                {
                    MessageBox.Show("Saved successfully!", "Success"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);

                    labelUserID.Text = UserID;
                    buttonSave.Enabled = false;
                }
                else
                {
                    MessageBox.Show("The data not saved. Please try again.",
                        "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private void _UpdateUser()
        {
            if (_IsInfoRight())
            {
                UserBuisness.ClsUser User = new UserBuisness.ClsUser();
                User.UserName = textBoxUserName.Text;
                User.Password = textBoxPassword.Text;
                User.IsActive = checkBoxIsActive.Checked ? 1 : 0;
                User.UserID = labelUserID.Text;
                

                if ( UserBuisness.UpdateUser(Convert.ToInt32( User.UserID ),User) )
                {
                    MessageBox.Show("Updated successfully!", "Success"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    
                }
                else
                {
                    MessageBox.Show("The data not Updated. Please try again.",
                        "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_currentMode == Mode.AddNew)
            {
                _AddNewUser();
            }
            else if(_currentMode == Mode.Update)
            {
                _UpdateUser();
                
            }


        }

        private bool CheckConfirmPassword()
        {
            if (textBoxConfirmPassword.Text != textBoxPassword.Text)
            {
                errorProvider1.SetError(textBoxConfirmPassword, "Confirm Password Not = Password");
                return false ;
            }
            else
            {
                _ClearErrorProvider(textBoxConfirmPassword);
                return true;
            }

        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            _CheckEmptyTextBox(textBoxUserName);
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            _CheckEmptyTextBox(textBoxPassword);
        }

        private void textBoxConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            _CheckEmptyTextBox(textBoxConfirmPassword);
            CheckConfirmPassword();
        }

        private void _UpdateMode()
        {
            _LoadInfoForUpdateMode();
            _SetUiForUpdateUser();
            
        }

        private void _LoadInfoForUpdateMode()
        {
            UserBuisness.ClsUser user = UserBuisness.FindUserByID(UserBuisness._SelectedUserIdGrid);

            PepoleBuisness.ClsPerson person = PepoleBuisness.FindPersonByID(Convert.ToString(user.PersonID));

            

            labelPersonID.Text = Convert.ToString(person.ID);
            labelName.Text = $"{person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}";
            labelNationalNo.Text = person.NationalNo;
            labelGendor.Text = person.GendorText;
            labelEmail.Text = person.Email;
            labelPhone.Text = person.Phone;
            labelAddress.Text = person.Address;
            labelCountry.Text = person.CountryName;
            labelDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            label1.Text = "Update User";
            labelUserID.Text = Convert.ToString(user.UserID);
            textBoxUserName.Text = user.UserName;
            textBoxPassword.Text = user.Password;
            textBoxConfirmPassword.Text = textBoxPassword.Text;
            checkBoxIsActive.Checked = user.IsActive == 1 ? true : false;

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

        private void _SetUiForUpdateUser()
        {
            comboBoxFilter.Enabled = false;
            textBoxFilter.Enabled = false ;
            buttonAddNewPerson.Enabled = false ;
            buttonFind.Enabled = false ;
        }

        private void _SetUiForAddNewUser()
        {
            textBoxConfirmPassword.Enabled = false ;
            textBoxPassword.Enabled = false ;
            textBoxUserName.Enabled = false ;
            checkBoxIsActive.Enabled = false ;
            buttonSave.Enabled = false ;
            comboBoxFilter.SelectedIndex = 0;
        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            if (_currentMode == Mode.AddNew)
            {
                _SetUiForAddNewUser();
            }
            else if (_currentMode == Mode.Update)
            {
                _UpdateMode();
            }
            
        }
    }
}
