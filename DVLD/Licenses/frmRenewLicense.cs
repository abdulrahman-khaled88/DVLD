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
    public partial class frmRenewLicense : Form
    {
        public frmRenewLicense()
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

        private string _GetClassID()
        {
            if(labelClass.Text == "Class 1 - Small Motorcycle")
            {
                return "1";
            }
            else if (labelClass.Text == "Class 2 - Heavy Motorcycle License")
            {
                return "2";
            }
            else if (labelClass.Text == "Class 3 - Ordinary driving license")
            {
                return "3";
            }
            else if (labelClass.Text == "Class 4 - Commercial")
            {
                return "4";
            }
            else if (labelClass.Text == "Class 5 - Agricultural")
            {
                return "5";
            }
            else if (labelClass.Text == "Class 6 - Small and medium bus")
            {
                return "6";
            }
            else if (labelClass.Text == "Class 7 - Truck and heavy vehicle")
            {
                return "7";
            }
            else
            {
                return null;
            }

        }

        private void _RenewLicense()
        {
            

                Local_Dl_Business.clsAP AP = new Local_Dl_Business.clsAP();


                PepoleBuisness.ClsPerson person = PepoleBuisness.FindPersonByNationalNo(labelNationalNo.Text);

                AP.ApplicantPersonID = Convert.ToString(person.ID);
                AP.ApplicationDate = DateTime.Now;
                AP.ApplicationTypeID = "2";
                AP.ApplicationStatus = "3";
                AP.LastStatusDate = DateTime.Now;
                AP.PaidFees = Local_Dl_Business.GetApplicationFess("2");
                AP.CreatedByUserID = UserBuisness.LoggedUser.UserID;

                string NewApllicationID = LicensesBuisness.AddNewApplication(AP);

                string RenwedLicenseID = 
                LicensesBuisness.AddNewLicense(NewApllicationID,labelDriverID.Text,_GetClassID(),
                Convert.ToDateTime (label.Text), Convert.ToDateTime(label16.Text),""
                ,Local_Dl_Business.GetApplicationFess("2"),"1","1",UserBuisness.LoggedUser.UserID);





            if (string.IsNullOrEmpty(RenwedLicenseID))
            {
                MessageBox.Show(
    "Failed to renew the license.",
    "Error",
    MessageBoxButtons.OK,
    MessageBoxIcon.Error
);

            }
            else
            {
                LicensesBuisness.DeActiveLicence(labelLicenseID.Text);
                labelRenewed.Text = RenwedLicenseID;
                MessageBox.Show(
    "License renewed successfully!",
    "Success",
    MessageBoxButtons.OK,
    MessageBoxIcon.Information
);

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

                if (info.ExpirationDate > DateTime.Now)
                {
                    MessageBox.Show("The selected license is not yet expired.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    if (labelIsDetained.Text == "No" && labelIsActive.Text == "Yes")
                    {
                        label21.Text = DateTime.Now.ToShortDateString();
                        label.Text = DateTime.Now.ToShortDateString();
                        label16.Text = DateTime.Now.AddYears(10).ToShortDateString();
                        labelTotalFees.Text = Local_Dl_Business.GetApplicationFess("2");
                        labelOldLicence.Text = labelLicenseID.Text;
                        labelUserName.Text = UserBuisness.LoggedUser.UserName;
                        buttonRenew.Enabled = true;
                    }
                    else
                    {
                        label21.Text = "[???]";
                        label.Text = "[???]";
                        label16.Text = "[???]";
                        labelTotalFees.Text = "[???]";
                        labelOldLicence.Text = "[???]";
                        labelUserName.Text = "[???]";
                        buttonRenew.Enabled = false;
                    }
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

        private void buttonRenew_Click(object sender, EventArgs e)
        {
            _RenewLicense();
        }
    }
}
