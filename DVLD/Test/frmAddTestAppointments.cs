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
    public partial class frmAddTestAppointments : Form
    {
        Local_Dl_Business.clsApInfo _AP = new Local_Dl_Business.clsApInfo();
        string _TestID;





        public frmAddTestAppointments(Local_Dl_Business.clsApInfo AP,string TestID)
        {
            InitializeComponent();
            _AP = AP;
            _TestID = TestID;


        }

        private void frmAddVtestAppointments_Load(object sender, EventArgs e)
        {

            _SetValues();


        }

        private void _SetValues()
        {


            labelD_L_ApID.Text = _AP.LDL_ID;
            labelLicenseClass.Text = _AP.LicenseClasseName;
            labelPersonName.Text = _AP.Applicant;
            labelFees.Text = Local_Dl_Business.GetTestTypeFees(_TestID);




        }

        private void _Save()
        {
            buttonSave.Enabled = false;
            if (Local_Dl_Business.AddNewTestAppointment(_AP.LDL_ID, _TestID, labelFees.Text, dateTimePicker1.Value
                 , UserBuisness.LoggedUser.UserID))
            {
                MessageBox.Show("The data has been saved successfully.",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("The data  not be saved. Please try again or contact support.",
    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }



        }


        private void buttonSave_Click(object sender, EventArgs e)
        {


          _Save();


        }






    }
}