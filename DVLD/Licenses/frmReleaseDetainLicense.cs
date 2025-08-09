using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD.Licenses
{
    public partial class frmReleaseDetainLicense : Form
    {
        public frmReleaseDetainLicense()
        {
            InitializeComponent();
        }

        private void buttonReleaseDetainLicense_Click(object sender, EventArgs e)
        {
            _ReleaseDetainLicense();
            _SetValues();
        }

        private void _Find()
        {
            if (string.IsNullOrEmpty(textBoxFilter.Text))
            {
                MessageBox.Show("Please Enter The ID !", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!LicensesBuisness.IsLicenseDetan(textBoxFilter.Text))
            {
                MessageBox.Show("The selected license is not Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                _SetValues();

            }
        }

        private void _ReleaseDetainLicense()
        {



            if (!LicensesBuisness.IsLicenseDetan(textBoxFilter.Text))
            {
                MessageBox.Show("The selected license is Not Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                bool True = LicensesBuisness.ReleaseLicence(1,DateTime.Now,UserBuisness.LoggedUser.UserID,
                    textBoxFilter.Text);

                    


                if (!True)
                {
                    MessageBox.Show(
"License Faild To Detained !",
"Error",
MessageBoxButtons.OK,
MessageBoxIcon.Error
);

                }
                else
                {
                    LicensesBuisness.ActiveateLicence(labelLicenseID.Text);

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
    }
}
