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
    public partial class frmVisionTest : Form
    {
        private Local_Dl_Business.clsApInfo _AP = new Local_Dl_Business.clsApInfo();

        
        public frmVisionTest(Local_Dl_Business.clsApInfo AP)
        {
            InitializeComponent();
            _AP = AP;
        }

        private void buttonAddNewAppointments_Click(object sender, EventArgs e)
        {
            if (!_CheckIfHaveAppointments())
            {
                if(Local_Dl_Business.IsPassedTest(_AP.LDL_ID,"1"))
                {
                    MessageBox.Show("You have already passed this test!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    frmAddTestAppointments frm = new frmAddTestAppointments(_AP, "1");
                    frm.ShowDialog();
                    _UpdateDataGrid();
                    _UpdateRecords();
                }

            }
            else
            {
                MessageBox.Show("You already have a  test appointments scheduled. Please check your schedule.",
                            "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private bool _CheckIfHaveAppointments()
        {
            return Local_Dl_Business.CheckIfHaveAppointment(_AP.LDL_ID, "1");
        }

        private void _SetControlValue()
        {
            userControlApInfo1.LDL_ID = _AP.LDL_ID;
            userControlApInfo1.ApplicationDate = _AP.ApplicationDate.ToShortDateString();
            userControlApInfo1.ApplicationID = _AP.ApplicationID;
            userControlApInfo1.Applicant = _AP.Applicant;
            userControlApInfo1.ApplicationsPaidFees = _AP.ApplicationsPaidFees;
            userControlApInfo1.ApplicationStatusText = _AP.ApplicationStatusText;
            userControlApInfo1.ApplicationTypeTitle = _AP.ApplicationTypeTitle;
            userControlApInfo1.LicenseClasseName = _AP.LicenseClasseName;
            userControlApInfo1.CreatedBy = UserBuisness.LoggedUser.UserName;
        }

        private void _UpdateRecords()
        {
            userControlRecord1.RecordsText = dataGridView1.RowCount.ToString();

        }

        private void _UpdateDataGrid()
        {
            dataGridView1.DataSource = Local_Dl_Business.GetAllTestAppointments(_AP.LDL_ID, "1");
        }

        private void frmAddVtest_Load(object sender, EventArgs e)
        {
            _SetControlValue();
            _UpdateDataGrid();
            _UpdateRecords();
        }

        private bool _IsLocked()
        {
            return Local_Dl_Business.IsTestAppointmentLocked(Local_Dl_Business.SelectedTestAppointmentID);
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!_IsLocked())
            {
                 frmUpdateTestAppointments frm = new frmUpdateTestAppointments(_AP,"1");
                frm.ShowDialog();
                _UpdateDataGrid();
                _UpdateRecords();
            }
            else
            {
                MessageBox.Show("You cannot edit this", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;


                Local_Dl_Business.SelectedTestAppointmentID = dataGridView1.Rows[e.RowIndex].Cells["TestAppointmentID"].Value.ToString();
            }
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_IsLocked())
            {
                FrmTakeTest frm = new FrmTakeTest(_AP, "1");
                frm.ShowDialog();
                _UpdateDataGrid();
                _UpdateRecords();
            }
            else
            {
                MessageBox.Show("You cannot Take Test For This", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
