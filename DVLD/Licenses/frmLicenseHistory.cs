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
    public partial class frmLicenseHistory : Form
    {
        private string _PersonID;
        public frmLicenseHistory( string PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }


        private void _UpdateRecord()
        {
            userControlRecord1.RecordsText = dataGridView1.RowCount.ToString();
            userControlRecord2.RecordsText = dataGridView2.RowCount.ToString();
        }

        private void _UpdateDataGrid()
        {
            dataGridView1.DataSource = Local_Dl_Business.GetAllLocalLiceses(_PersonID);
            dataGridView2.DataSource = Local_Dl_Business.GetAllGlobalLiceses(_PersonID);
        }



        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            _UpdateDataGrid();
            _UpdateRecord();
            
        }
    }
}
