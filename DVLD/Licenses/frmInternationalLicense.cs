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
    public partial class frmInternationalLicense : Form
    {
        public frmInternationalLicense()
        {
            InitializeComponent();
        }

        private void _Find()
        {
            if (string.IsNullOrEmpty(textBoxFilter.Text))
            {
                MessageBox.Show("Please Enter The ID !", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {


                _SetValues();



            }
        }

        private void _IssueLicense()
        {
            if (!LicensesBuisness.IsHaveInternationalLicense(textBoxFilter.Text))
            {
                Local_Dl_Business.clsAP AP = new Local_Dl_Business.clsAP();


                PepoleBuisness.ClsPerson person = PepoleBuisness.FindPersonByNationalNo(labelNationalNo.Text);

                AP.ApplicantPersonID = Convert.ToString( person.ID );
                AP.ApplicationDate = DateTime.Now;
                AP.ApplicationTypeID = "6";
                AP.ApplicationStatus = "3";
                AP.LastStatusDate = DateTime.Now;
                AP.PaidFees = Local_Dl_Business.GetApplicationFess("6");
                AP.CreatedByUserID = UserBuisness.LoggedUser.UserID;




                string NewInternationalLicenseID = LicensesBuisness.AddNewInternationalLicense
                    (AP, labelDriverID.Text, labelLicenseID.Text,
                    Convert.ToDateTime (label.Text), Convert.ToDateTime(label16.Text),
                    labelIsActive.Text == "Yes" ? 1 : 0, UserBuisness.LoggedUser.UserID);

                if (string.IsNullOrEmpty(NewInternationalLicenseID))
                {
                    MessageBox.Show("Failed to create the International License.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    label_iL_ID.Text = NewInternationalLicenseID;
                    MessageBox.Show("International License created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                MessageBox.Show("This driver already has an International License.", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                labelDateOfBirth.Text = info.DateOfBirth.ToShortDateString() ;
                labelDriverID.Text = info.DriverID;
                labelExpirationDate.Text = info.ExpirationDate.ToShortDateString();
                labelIsDetained.Text = LicensesBuisness.IsLicenseDetan(labelLicenseID.Text) ? "Yes" : "No" ;
                if (!string.IsNullOrEmpty(info.ImagePath))
                {
                    pictureBox23.Image = Image.FromFile(info.ImagePath);
                }

                if(labelIsDetained.Text == "No" && labelIsActive.Text == "Yes" 
                    && info.Class == "Class 3 - Ordinary driving license")
                {
                    label21.Text = DateTime.Now.ToShortDateString();
                    label.Text = DateTime.Now.ToShortDateString();
                    label16.Text = DateTime.Now.AddYears(1).ToShortDateString();
                    labelFees.Text = Local_Dl_Business.GetApplicationFess("6");
                    labelLocalLicence.Text = labelLicenseID.Text;
                    labelUserName.Text = UserBuisness.LoggedUser.UserName;
                    buttonIssue.Enabled = true;
                }
                else
                {
                    label21.Text = "[???]";
                    label.Text = "[???]";
                    label16.Text = "[???]";
                    labelFees.Text = "[???]";
                    labelLocalLicence.Text = "[???]";
                    labelUserName.Text = "[???]";
                    buttonIssue.Enabled = false;
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

        private void buttonIssue_Click(object sender, EventArgs e)
        {
            _IssueLicense();
        }
    }
}
