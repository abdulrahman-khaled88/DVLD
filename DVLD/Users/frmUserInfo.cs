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
    public partial class frmUserInfo : Form
    {
        private UserBuisness.ClsUser _User = new UserBuisness.ClsUser();

        public frmUserInfo(UserBuisness.ClsUser User)
        {
            InitializeComponent();
            _User = User;
        }


        private void _SetUserInfo()
        {
            PepoleBuisness.ClsPerson person = 
                PepoleBuisness.FindPersonByID( Convert.ToString(_User.PersonID) );

            labelPersonID.Text = Convert.ToString(person.ID);
            labelName.Text = $"{person.FirstName} {person.SecondName} {person.ThirdName} {person.LastName}";
            labelNationalNo.Text = person.NationalNo;
            labelGendor.Text = person.GendorText;
            labelEmail.Text = person.Email;
            labelPhone.Text = person.Phone;
            labelAddress.Text = person.Address;
            labelCountry.Text = person.CountryName;
            labelDateOfBirth.Text = person.DateOfBirth.ToShortDateString();
            labelUserID.Text = Convert.ToString( _User.UserID );
            labelUserName.Text =  _User.UserName ;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            _SetUserInfo();
        }
    }
}
