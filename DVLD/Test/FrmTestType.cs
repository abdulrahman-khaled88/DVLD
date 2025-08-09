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
    public partial class FrmTestType : Form
    {
        public FrmTestType()
        {
            InitializeComponent();
        }

        private void _UpdateRecords()
        {
            userControlRecord1.RecordsText = dataGridView1.RowCount.ToString();
        }

        private void _UpdateDataGrid()
        {
            dataGridView1.DataSource = TestTypeBuisness.ListAllTestTypes();
        }

        private void FrmTestType_Load(object sender, EventArgs e)
        {
            _UpdateDataGrid();
            _UpdateRecords();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUpdateApType frm = new FrmUpdateApType
                (TestTypeBuisness.DtToClsConvert
                (TestTypeBuisness.FindTestTypeByID(TestTypeBuisness.SelectedIdDGV)));

            frm.ShowDialog();
            _UpdateDataGrid ();
            _UpdateRecords ();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.Rows[e.RowIndex].Selected = true;


            TestTypeBuisness.SelectedIdDGV =
                dataGridView1.Rows[e.RowIndex].Cells["TestTypeID"].Value.ToString();
        }
    }
}
