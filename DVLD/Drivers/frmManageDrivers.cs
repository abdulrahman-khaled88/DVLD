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
using DVLD_Buisness;

namespace DVLD.Drivers
{
    public partial class frmManageDrivers : Form
    {
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private string _SelectedPersonID = string.Empty;

        private void _UpdateRecords()
        {
            userControlRecord1.RecordsText = dataGridView1.RowCount.ToString();
        }

        private void _UpdateDataGrid()
        {
            if(!string.IsNullOrEmpty(textBoxFilter.Text))
            {
                if (comboBoxFilter.SelectedIndex == 1)
                {
                    dataGridView1.DataSource = Drivers_Buisness.GetAllDirversByDriverID(textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 2)
                {
                    dataGridView1.DataSource = Drivers_Buisness.GetAllDirversByPersonID(textBoxFilter.Text);
                }
            }
            else
            {
                dataGridView1.DataSource = Drivers_Buisness.GetAllDirvers();
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

        private void _SetDefultValue()
        {
            comboBoxFilter.SelectedIndex = 0;
        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            _SetDefultValue();
            _UpdateDataGrid();
            _UpdateRecords();
            _UpdateTextBox();

        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdateTextBox();
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            _UpdateDataGrid();
            _UpdateRecords();

        }

        private void getLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseHistory frm = new frmLicenseHistory(_SelectedPersonID);
            frm.ShowDialog();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;


                _SelectedPersonID = dataGridView1.Rows[e.RowIndex].Cells["PersonID"].Value.ToString();



            }
        }
    }
}
