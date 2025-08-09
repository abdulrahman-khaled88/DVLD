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

namespace DVLD.Applications
{
    public partial class frmApTypes : Form
    {
        public frmApTypes()
        {
            InitializeComponent();
        }

        private void _UpdateRecords()
        {
            userControlRecord1.RecordsText = dataGridView1.RowCount.ToString();
        }

        private void _UpdateDataGrid()
        {
            dataGridView1.DataSource = ApTypeBuisness.ListAllApTypesInfo();
        }

        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
            _UpdateDataGrid();
            _UpdateRecords();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;


                ApTypeBuisness.SelectIdInDGV = 
                    dataGridView1.Rows[e.RowIndex].Cells["ApplicationTypeID"].Value.ToString();



            }
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FrmEdit frm = new FrmEdit(
                ApTypeBuisness.DtToClsConvert
                (ApTypeBuisness.FindApTypeByID(ApTypeBuisness.SelectIdInDGV)));

            frm.ShowDialog();
            _UpdateRecords();
            _UpdateDataGrid();
        }
    }
}
