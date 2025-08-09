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
    public partial class frmUpdateTestAppointments : Form
    {
        Local_Dl_Business.clsApInfo _AP = new Local_Dl_Business.clsApInfo();
        string _TestID;
        public frmUpdateTestAppointments(Local_Dl_Business.clsApInfo AP,string TestID)
        {
            _AP = AP;
            InitializeComponent();
            _TestID = TestID;
        }

        private void _Update()
        {
            if (Local_Dl_Business.UpdateTestAppointment
                (Local_Dl_Business.SelectedTestAppointmentID,dateTimePicker1.Value))
            {
                MessageBox.Show("The data has been Updated successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("The data  not  Updated. Please try again or contact support.",
    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        

        private void _SetValues()
        {


            labelD_L_ApID.Text = _AP.LDL_ID;
            labelLicenseClass.Text = _AP.LicenseClasseName;
            labelPersonName.Text = _AP.Applicant;
            labelFees.Text = Local_Dl_Business.GetTestTypeFees(_TestID);
            dateTimePicker1.Value = Local_Dl_Business.GetTestAppointmentDate(Local_Dl_Business.SelectedTestAppointmentID);
            




        }
        private void UpdateTestAppointments_Load(object sender, EventArgs e)
        {
            _SetValues();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            _Update();
        }
    }
}
