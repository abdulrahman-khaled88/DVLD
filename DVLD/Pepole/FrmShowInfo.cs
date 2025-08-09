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
    public partial class FrmShowInfo : Form
    {
        private static PepoleBuisness.ClsPerson person = new PepoleBuisness.ClsPerson();
        public FrmShowInfo(PepoleBuisness.ClsPerson Person)
        {
            person = Person;
            InitializeComponent();
        }

        private void _SetPersonInfo()
        {
            labelPersonID.Text = Convert.ToString( person.ID);
            labelName.Text = $"{person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}";
            labelNationalNo.Text = person.NationalNo;
            labelGendor.Text = person.GendorText;
            labelEmail.Text = person.Email;
            labelPhone.Text = person.Phone;
            labelAddress.Text = person.Address;
            labelCountry.Text = person.CountryName;
            labelDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            if ( !string.IsNullOrEmpty( person.ImagePath))
            {
                pictureBox1.Image = Image.FromFile($@"{person.ImagePath}");
            }
            else
            {
                if(person.GendorText == "Male")
                {
                    pictureBox1.Image = Image.FromFile(@"D:\Desktop\Icons\Male 512.png");
                }
                else
                {
                    pictureBox1.Image = Image.FromFile(@"D:\Desktop\Icons\Female 512.png");
                }
            }
            
        }

        private void FrmShowInfo_Load(object sender, EventArgs e)
        {
            _SetPersonInfo();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
