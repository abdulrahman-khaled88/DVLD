using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Licenses;
using DVLD.Test;
using DVLD_Buisness;

namespace DVLD.Local_DL
{
    public partial class frmMangeLocal_DL : Form
    {
        public frmMangeLocal_DL()
        {
            InitializeComponent();
        }

        private string _SelectedApIdGrid = null;
        private void _UpdateRecords()
        {
            userControlRecord1.RecordsText = dataGridView1.RowCount.ToString();
        }

        private void _UpdateDataGrid()
        {
            if (!string.IsNullOrEmpty(textBoxFilter.Text))
            {
                if (comboBoxFilter.SelectedIndex == 1)
                {
                    dataGridView1.DataSource =
                        Local_Dl_Business.ListAllLocal_DlByFilter("LocalDrivingLicenseApplicationID", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 2)
                {
                    dataGridView1.DataSource =
                        Local_Dl_Business.ListAllLocal_DlByFilter("NationalNo", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 3)
                {
                    dataGridView1.DataSource =
                        Local_Dl_Business.ListAllLocal_DlByFilter("ApplicationStatus", _GetStatusNumber(textBoxFilter.Text));
                }
            }
            else
            {
                dataGridView1.DataSource = Local_Dl_Business.ListAllLocal_Dl();
            }

            
            
                
            


            
        }

        private void _UpdateTextBox()
        {
            if (comboBoxFilter.SelectedIndex == 0)
            {
                textBoxFilter.Visible = false;
            }
            else
            {
                textBoxFilter.Visible = true;
            }
        }


        private string _GetStatusNumber(string Status)
        {
            if (Status == "New")
            {
                return "1";
            }
            else if (Status == "Cancelled")
            {
                return "2";
            }
            else if (Status == "Completed")
            {
                return "3";
            }
            else
            {
                return "";
            }

        }

        private void _Cancle()
        {
            if (MessageBox.Show($"Are you sure you want to cancle this application?"
                    , "Confirm cancle", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string Status = Local_Dl_Business.GetApStatus(_SelectedApIdGrid);

                if (Status == "1")
                {
                    if (Local_Dl_Business.UpdateLDL_ApStatus(_SelectedApIdGrid, _GetStatusNumber("Cancelled")))
                    {
                        MessageBox.Show("The application has been successfully canceled.", "Success", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("The application could not be canceled. Please try again or contact support.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (Status == "2")
                {
                    MessageBox.Show("The application has already been canceled and cannot be canceled again.", "Notice",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (Status == "3")
                {
                    MessageBox.Show("The application has already been completed and cannot be modified.", "Completed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }





            }
        }
        

        private void frmMangeLocal_DL_Load(object sender, EventArgs e)
        {
            _UpdateDataGrid();
            _UpdateTextBox();
            _UpdateRecords();
        }

        private void buttonAddNewPerson_Click(object sender, EventArgs e)
        {
            FrmAddNewLocal_DL frm = new FrmAddNewLocal_DL();
            frm.ShowDialog();
            _UpdateDataGrid();
            _UpdateRecords() ;
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            _UpdateDataGrid();
            _UpdateRecords();
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdateTextBox();
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Cancle();
            _UpdateDataGrid() ;
            _UpdateRecords() ;
        }


        private string _GetSelectedRow(DataGridView dgv,DataGridViewCellMouseEventArgs e, string CellName)
        {
            if (e.RowIndex >= 0)
            {

                dgv.ClearSelection();
                dgv.Rows[e.RowIndex].Selected = true;


                return dgv.Rows[e.RowIndex].Cells[$"{CellName}"].Value.ToString();



            }

            return null;
        }

        private void _UpdateSechduleTests()
        {
            if (Local_Dl_Business.GetApStatus(_SelectedApIdGrid) != "1")
            {
                sechduleTestsToolStripMenuItem.Enabled = false;
            }
            else
            {
                sechduleTestsToolStripMenuItem.Enabled = true;
            }
        }

        private void _UpdateIssueDrivingLicense()
        {
            issueDrivingLicenseToolStripMenuItem.Enabled=false;

            if (Local_Dl_Business.GetApStatus(_SelectedApIdGrid) == "3")
            {
                issueDrivingLicenseToolStripMenuItem.Enabled = true;
            }
        }
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            _SelectedApIdGrid = _GetSelectedRow(dataGridView1, e, "L.D.LAppID");

            _UpdateSechduleTests();
            _UpdateIssueDrivingLicense();


        }

        private Local_Dl_Business.clsApInfo _GetInfoFortest()
        {
            Local_Dl_Business.clsApInfo AP = Local_Dl_Business.GetAPInfoByID(_SelectedApIdGrid);

            return AP;
        }


        private void sechduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVisionTest frm = new frmVisionTest(_GetInfoFortest());
            frm.ShowDialog();
            _UpdateDataGrid();
            _UpdateRecords();

        }


        private void sechduleTestsToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            sechduleVisionTestToolStripMenuItem.Enabled = false;
            sechduleWriteTestToolStripMenuItem.Enabled = false;
            sechduleStreetTestToolStripMenuItem.Enabled = false;

            if (!Local_Dl_Business.IsPassedTest(_SelectedApIdGrid, "1"))
            {
                sechduleVisionTestToolStripMenuItem.Enabled = true;
            }
            else if (!Local_Dl_Business.IsPassedTest(_SelectedApIdGrid, "2"))
            {
                sechduleVisionTestToolStripMenuItem.Enabled = false;
                sechduleWriteTestToolStripMenuItem.Enabled = true;
                
            }
            else if (!Local_Dl_Business.IsPassedTest(_SelectedApIdGrid, "3"))
            {
                sechduleWriteTestToolStripMenuItem.Enabled = false;
                sechduleStreetTestToolStripMenuItem.Enabled = true;
            }

        }

        private void sechduleWriteTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWrittenTest frm = new frmWrittenTest(_GetInfoFortest());
            frm.ShowDialog();
            _UpdateDataGrid();
            _UpdateRecords();
        }

        private void sechduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Street_Test frm = new frm_Street_Test (_GetInfoFortest());
            frm.ShowDialog();
            if (Local_Dl_Business.IsPassedTest(_SelectedApIdGrid,"3"))
            {
                Local_Dl_Business.UpdateLDL_ApStatus(_SelectedApIdGrid, "3");
            }
            _UpdateDataGrid();
            _UpdateRecords();
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Local_Dl_Business.GetApStatus(_SelectedApIdGrid)=="1")
            {
               if( Local_Dl_Business.DeleteLDLAp(_SelectedApIdGrid))
                {
                    MessageBox.Show("Deletion successful!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
               else
                {
                    MessageBox.Show("Deletion failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("This item cannot be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            _UpdateDataGrid() ;
            _UpdateTextBox();
        }

        private void LicenseApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(Local_Dl_Business.GetPersonIdByLDL(_SelectedApIdGrid));
            frm.ShowDialog();
        }

        private void _AddNewLicense()
        {
            string PersonID = Local_Dl_Business.GetPersonIdByLDL(_SelectedApIdGrid);
            string LicenseClassID = Local_Dl_Business.GetLicenseClassID(_SelectedApIdGrid);


            if (Local_Dl_Business.IsHaveTheLicense(PersonID, LicenseClassID))
            {
                MessageBox.Show("This person already has this license.", "License Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(Local_Dl_Business.IsDriver(PersonID))
                {
                    string LicensesID = LicensesBuisness.AddNewLicense(Local_Dl_Business.GetApplicationID(_SelectedApIdGrid),
      Drivers_Buisness.GetDriverID(PersonID), LicenseClassID, DateTime.Now, DateTime.Now.AddYears(10),
      " ", Local_Dl_Business.GetApplicationFess("1"),
      "1", "1", UserBuisness.LoggedUser.UserID);



                    if (!string.IsNullOrEmpty(LicensesID))
                    {
                        MessageBox.Show("License added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add the license. No rows were affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                   string DriverID = Drivers_Buisness.AddNewDriver
                        (PersonID, UserBuisness.LoggedUser.UserID,DateTime.Now);

                  string LicensesID =  LicensesBuisness.AddNewLicense(Local_Dl_Business.GetApplicationID(_SelectedApIdGrid),
                        DriverID, LicenseClassID, DateTime.Now, DateTime.Now.AddYears(10),
                        " ", Local_Dl_Business.GetApplicationFess("1"),
                        "1", "1", UserBuisness.LoggedUser.UserID);

                    if (!string.IsNullOrEmpty(LicensesID))
                    {
                        MessageBox.Show("License added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add the license. No rows were affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }




                }
            }




        }


        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _AddNewLicense();
        }
    }
}
