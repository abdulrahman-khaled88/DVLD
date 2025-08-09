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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD.Pepole
{
    public partial class FrmAddNewPerson : Form
    {
       private static PepoleBuisness.ClsPerson _Person = new PepoleBuisness.ClsPerson();
        public FrmAddNewPerson()
        {
            InitializeComponent();
        }

        private void _SetDefultValueForControls()
        {
            comboBoxCountry.SelectedItem = "Qatar";
            _Person.DateOfBirth = dateTimePicker1.Value;
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty( textBoxFirstName.Text ))
            {
                _Person.FirstName = textBoxFirstName.Text;

                _ClearErrorProvider(textBoxFirstName);
            }
            
        }

        private void textBoxNationalNO_TextChanged(object sender, EventArgs e)
        {
            if (PepoleBuisness.IsNatonalNoExists(textBoxNationalNO.Text))
            {
                errorProvider1.SetError(textBoxNationalNO, "Natonal No. Exists");
            }
            else
            {
                errorProvider1.SetError(textBoxNationalNO, "");
                _Person.NationalNo = textBoxNationalNO.Text;
            }
        }

        private void textBoxSecondName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSecondName.Text))
            {
                _Person.SecondName = textBoxSecondName.Text;

                _ClearErrorProvider(textBoxSecondName);
            }
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                _Person.ThirdName = textBox4.Text;

                _ClearErrorProvider(textBox4);
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                _Person.LastName = textBox3.Text;

                _ClearErrorProvider(textBox3);
            }
            
        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPhone.Text))
            {
                _Person.Phone = textBoxPhone.Text;

                _ClearErrorProvider(textBoxPhone);
            }
            
        }



        private void richTextBoxAddress_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBoxAddress.Text))
            {
                _Person.Address = richTextBoxAddress.Text;

                _ClearErrorProvider(richTextBoxAddress);
            }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            _Person.DateOfBirth = dateTimePicker1.Value;
        }



        private void FrmAddNewPerson_Load(object sender, EventArgs e)
        {
            _SetDefultValueForControls();
        }

        private void radioButtonMale_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_Person.ImagePath))
            {
                if (radioButtonMale.Checked)
                {
                    pictureBox1.Image = Image.FromFile(@"D:\Desktop\Icons\Male 512.png");
                }
            }
            _Person.GendorText = "Male";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_Person.ImagePath))
            {
                if (radioButtonFemale.Checked)
                {
                    pictureBox1.Image = Image.FromFile(@"D:\Desktop\Icons\Female 512.png");
                }
            }
            _Person.GendorText = "Female";
        }

        private void linkLabelImagePath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            openFileDialog1.InitialDirectory = @"C:\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _Person.ImagePath = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(_Person.ImagePath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '-')
            {
                errorProvider1.SetError(textBoxPhone, "");
                return;
            }
            else if (char.IsLetter(e.KeyChar))
            {
                errorProvider1.SetError(textBoxPhone, "Please Enter Nubmers Only");
            }




            e.Handled = true;
        }

        private void comboBoxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Person.CountryName = comboBoxCountry.Text;
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

        private void _SetErrorProvider(Control Control)
        {
            errorProvider1.SetError(Control, "Can't Be Empty!");
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            string NewID = null;

            if(_AreTextBoxesEmpty())
            {
                MessageBox.Show("Please Complete The Information!", "Warning", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else  
            {
                NewID = PepoleBuisness.AddNewPerson(_Person);

                if (NewID != null) 
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    labelPersonID.Text = NewID;
                }
                else
                {
                    MessageBox.Show("Erorr404", "Not Saved",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                _Person.Email = textBox6.Text;

                _ClearErrorProvider(textBox6);
            }
        }


    }
}
