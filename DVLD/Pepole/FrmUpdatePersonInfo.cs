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

namespace DVLD.Pepole
{
    public partial class FrmUpdatePersonInfo : Form
    {
        private static PepoleBuisness.ClsPerson person = new PepoleBuisness.ClsPerson();
        private static PepoleBuisness.ClsPerson NewPerson = new PepoleBuisness.ClsPerson();

        private void _SetDefultValues()
        {
            labelPersonID.Text = Convert.ToString( person.ID );
            textBoxFirstName.Text = person.FirstName;
            textBoxSecondName.Text = person.SecondName;
            textBoxThirdName.Text = person.ThirdName;
            textBoxLastName.Text = person.LastName;
            textBoxNationalNO.Text = person.NationalNo;
            textBoxPhone.Text = person.Phone;
            textBoxEmail.Text = person.Email;
            richTextBoxAddress.Text = person.Address;
            dateTimePicker1.Value = person.DateOfBirth;
            comboBoxCountry.Text = person.CountryName;



        }

        private void _SetDefultValuesForGendor()
        {


            if (!string.IsNullOrEmpty(person.ImagePath))
            {
                pictureBox1.Image = Image.FromFile($@"{person.ImagePath}");
            }
            else
            {
                if (person.GendorText == "Male")
                {
                    radioButtonMale.Checked = true;
                    pictureBox1.Image = Image.FromFile(@"D:\Desktop\Icons\Male 512.png");
                }
                else
                {
                    radioButtonFemale.Checked = true;
                    pictureBox1.Image = Image.FromFile(@"D:\Desktop\Icons\Female 512.png");
                }
            }
        }

        private void _SetValueForNewPerson()
        {
            NewPerson.FirstName = textBoxFirstName.Text;
            NewPerson.SecondName = textBoxSecondName.Text;
            NewPerson.ThirdName = textBoxThirdName.Text;
            NewPerson.LastName = textBoxLastName.Text;
            NewPerson.NationalNo = textBoxNationalNO.Text;
            NewPerson.Address = richTextBoxAddress.Text;
            NewPerson.Phone = textBoxPhone.Text;
            NewPerson.CountryName = comboBoxCountry.Text;
            NewPerson.GendorText = radioButtonMale.Checked ? "Male" : "Female";
            NewPerson.DateOfBirth = dateTimePicker1.Value;
            NewPerson.Email = textBoxEmail.Text;


        }
        public FrmUpdatePersonInfo(PepoleBuisness.ClsPerson _person)
        {
            person = _person;
            InitializeComponent();
        }

        private void FrmUpdatePersonInfo_Load(object sender, EventArgs e)
        {
            _SetDefultValues();
            _SetDefultValuesForGendor();
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            _ClearErrorProvider(textBoxFirstName);
            _SetValueForNewPerson();
        }

        private void textBoxSecondName_TextChanged(object sender, EventArgs e)
        {
            _SetValueForNewPerson();
            _ClearErrorProvider(textBoxSecondName);
        }

        private void textBoxThirdName_TextChanged(object sender, EventArgs e)
        {
            _ClearErrorProvider(textBoxThirdName);
            _SetValueForNewPerson();
        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {
            _ClearErrorProvider(textBoxLastName);
            _SetValueForNewPerson();
        }

        private void textBoxNationalNO_TextChanged(object sender, EventArgs e)
        {
            _ClearErrorProvider(textBoxNationalNO);
            _SetValueForNewPerson();
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            _ClearErrorProvider(textBoxEmail);
            _SetValueForNewPerson();
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            _ClearErrorProvider(textBoxPhone);
            _SetValueForNewPerson();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            _SetValueForNewPerson();
        }

        private void richTextBoxAddress_TextChanged(object sender, EventArgs e)
        {
            _ClearErrorProvider(richTextBoxAddress);
            _SetValueForNewPerson();
        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SetValueForNewPerson();
        }

        private void linkLabelImagePath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            openFileDialog1.InitialDirectory = @"C:\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NewPerson.ImagePath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(NewPerson.ImagePath);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _AreTextBoxesEmpty()
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

        private void _ClearErrorProvider(Control Control)
        {
            errorProvider1.SetError(Control, "");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            

            if (_AreTextBoxesEmpty())
            {
                MessageBox.Show("Please Complete The Information!", "Warning"
                    , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                 

                if (PepoleBuisness.UpdatePerson(person.ID, NewPerson))
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show("Erorr404", "Not Saved"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
