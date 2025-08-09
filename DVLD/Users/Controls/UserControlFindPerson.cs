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

namespace DVLD.Users.Controls
{
    public partial class UserControlFindPerson : UserControl
    {
        public UserControlFindPerson()
        {
            InitializeComponent();
        }

        private void _SetPersonInfo()
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

        private PepoleBuisness.ClsPerson _GetPersonInfo()
        {
            PepoleBuisness.ClsPerson person = comboBoxFilter.SelectedIndex == 0 ?
            PepoleBuisness.FindPersonByID(textBoxFilter.Text) :
            PepoleBuisness.FindPersonByNationalNo(textBoxFilter.Text);

            return person;
        }
        private void buttonFind_Click(object sender, EventArgs e)
        {
            _SetPersonInfo();
        }

        public string PersonID
        {
            get { return labelPersonID.Text; }
        }

        public bool IsPersonEmpty()
        {
            return string.IsNullOrEmpty(labelPersonID.Text) || labelPersonID.Text == "[???]";
        }


        private void buttonAddNewPerson_Click(object sender, EventArgs e)
        {
            FrmAddNewPerson frm = new FrmAddNewPerson();
            frm.ShowDialog();
        }

        private void UserControlFindPerson_Load(object sender, EventArgs e)
        {
            comboBoxFilter.SelectedIndex = 0;
        }
    }
}
