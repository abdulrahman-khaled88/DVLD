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

namespace DVLD.Test
{
    public partial class FrmTakeTest : Form
    {
        Local_Dl_Business.clsApInfo _AP = new Local_Dl_Business.clsApInfo();
        public FrmTakeTest(Local_Dl_Business.clsApInfo AP, string testID)
        {
            InitializeComponent();
            _AP = AP;
            _TestID = testID;
        }
        string _TestID;
        private void _SetValues()
        {


            labelD_L_ApID.Text = _AP.LDL_ID;
            labelLicenseClass.Text = _AP.LicenseClasseName;
            labelPersonName.Text = _AP.Applicant;
            labelFees.Text = Local_Dl_Business.GetTestTypeFees(_TestID);
            labelDate.Text =  Local_Dl_Business.
                GetTestAppointmentDate(Local_Dl_Business.SelectedTestAppointmentID).ToShortDateString();





        }

        private void FrmTakeTest_Load(object sender, EventArgs e)
        {
            _SetValues();
        }

        private string _GetResultNum()
        {
            if (radioButtonPass.Checked)
            {
                return "1";
            }
            else if (radioButtonPass.Checked)
            {
                return "0";
            }

            return "";
        }

        private void _Save()
        {
           string TestID = Local_Dl_Business.TakeTest
        (Local_Dl_Business.SelectedTestAppointmentID, _GetResultNum(), richTextBox1.Text, UserBuisness.LoggedUser.UserID);

            if(!string.IsNullOrEmpty(TestID))
            {
               Local_Dl_Business.MakeTestAppointmentLocked(Local_Dl_Business.SelectedTestAppointmentID);
               buttonSave.Enabled = false;
               labelTestID.Text = TestID;
                MessageBox.Show("Save Successful!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Save failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            _Save();
        }
    }
}
