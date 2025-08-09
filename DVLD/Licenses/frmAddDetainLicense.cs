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

namespace DVLD.Licenses
{
    public partial class frmAddDetainLicense : Form
    {
        public frmAddDetainLicense()
        {
            InitializeComponent();
        }

        private void _Find()
        {
            if (string.IsNullOrEmpty(textBoxFilter.Text))
            {
                MessageBox.Show("Please Enter The ID !", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (LicensesBuisness.IsLicenseDetan(textBoxFilter.Text))
            {
                    MessageBox.Show("The selected license is Orady Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {

                _SetValues();

            }
        }

        

        private void _DetainLicense()
        {



            if (LicensesBuisness.IsLicenseDetan(textBoxFilter.Text))
            {
                MessageBox.Show("The selected license is Orady Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {




                string DetainID = LicensesBuisness.DetanLicence(labelLicenceID.Text, DateTime.Now
                    , textBoxFineFees.Text, UserBuisness.LoggedUser.UserID, 0);


                if (string.IsNullOrEmpty(DetainID))
                {
                    MessageBox.Show(
        "Failed to Detain the license.",
        "Error",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
    );

                }
                else
                {
                    labelDetainID.Text = DetainID;
                    LicensesBuisness.DeActiveLicence(labelLicenseID.Text);

                    MessageBox.Show(
        "License Detained successfully!",
        "Success",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    );

                }
            }
















        }

        private void _SetValues()
        {
            LicensesBuisness.clsLicenseInfo info = LicensesBuisness.GetLicenseInfo(textBoxFilter.Text);

            if (info != null)
            {
                labelClass.Text = info.Class;
                labelName.Text = info.Name;
                labelLicenseID.Text = textBoxFilter.Text;
                labelNationalNo.Text = info.NationalNo;
                labelGendor.Text = info.Gendor;
                labelIssueDate.Text = info.IssueDate.ToShortDateString();
                labelIsActive.Text = info.IsActive;
                labelDateOfBirth.Text = info.DateOfBirth.ToShortDateString();
                labelDriverID.Text = info.DriverID;
                labelExpirationDate.Text = info.ExpirationDate.ToShortDateString();
                labelIsDetained.Text = LicensesBuisness.IsLicenseDetan(labelLicenseID.Text) ? "Yes" : "No";
                if (!string.IsNullOrEmpty(info.ImagePath))
                {
                    pictureBox23.Image = Image.FromFile(info.ImagePath);
                }
                
                

                    if (labelIsActive.Text == "Yes")
                    {
                        label21.Text = DateTime.Now.ToShortDateString();
                        label.Text = DateTime.Now.ToShortDateString();
                        label16.Text = DateTime.Now.AddYears(10).ToShortDateString();
                        labelLicenceID.Text = labelLicenseID.Text;
                        labelUserName.Text = UserBuisness.LoggedUser.UserName;
                        buttonDetainLicense.Enabled = true;
                        textBoxFineFees.Enabled = true;
                    }
                    else
                    {
                    MessageBox.Show(
"The selected license is not active.",
"Error",
MessageBoxButtons.OK,
MessageBoxIcon.Error
);
                    label21.Text = "[???]";
                        label.Text = "[???]";
                        label16.Text = "[???]";
                        labelLicenceID.Text = "[???]";
                        labelUserName.Text = "[???]";
                        buttonDetainLicense.Enabled = false;
                        textBoxFineFees.Enabled = false;
                    }





            }
            else
            {
                MessageBox.Show("License not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            _Find();
        }

        private void buttonDetainLicense_Click(object sender, EventArgs e)
        {

            _DetainLicense();

        }
    }
}
