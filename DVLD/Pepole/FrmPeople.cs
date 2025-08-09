using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;

namespace DVLD.Pepole
{
    public partial class FrmPeople : Form
    {
        public FrmPeople()
        {
            InitializeComponent();
        }

        private static string _SelectedPersonIdFromGridView = null;
        private void _UpdateDataGridView()
        {
            if (comboBoxFilter.SelectedIndex == 0)
            {
                dataGridView1.DataSource = PepoleBuisness.GetAllPersonsInfo();
            }
            else if (!string.IsNullOrWhiteSpace(textBoxFilter.Text))
            {
                if (comboBoxFilter.SelectedIndex == 1)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("PersonID", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 2)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("NationalNo", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 3)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("FirstName", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 4)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("SecondName", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 5)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("ThirdName", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 6)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("LastName", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 7)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("Country", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 8)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter
                        ("Gendor", 
                        Convert.ToString(PepoleBuisness.ConvertGendorTextToNumber(textBoxFilter.Text)) );
                }
                else if (comboBoxFilter.SelectedIndex == 9)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("Phone", textBoxFilter.Text);
                }
                else if (comboBoxFilter.SelectedIndex == 10)
                {
                    dataGridView1.DataSource = PepoleBuisness.
                        GetAllPersonsInfoByFilter("Email", textBoxFilter.Text);
                }

            }
            else
            {
                dataGridView1.DataSource = PepoleBuisness.GetAllPersonsInfo();
            }







        }

        private void _UpdateRecords()
        {
            labelRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void _DeletePerson()
        {
            if (MessageBox.Show($"Are you sure you want to delete Peson {_SelectedPersonIdFromGridView}"
              , "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (PepoleBuisness.DeletePerson(Convert.ToInt32(_SelectedPersonIdFromGridView)))
                {
                    MessageBox.Show("Person Deleted Successfuly.", "Successful", MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Person Was not deleted because it has data linked to it."
                        , "Error", MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                }
            }
        }

        private void _SetControlsDefultValues()
        {
            comboBoxFilter.SelectedIndex = 0;
        }

        private void _UpdateTextBoxFilter()
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

        private void FrmPeople_Load(object sender, EventArgs e)
        {
            _UpdateDataGridView();
            _UpdateRecords();
            _SetControlsDefultValues();
        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdateTextBoxFilter();
            _UpdateDataGridView();
            _UpdateRecords();
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            _UpdateDataGridView();
            _UpdateRecords();
        }

        private void buttonAddNewPerson_Click(object sender, EventArgs e)
        {
            FrmAddNewPerson frm = new FrmAddNewPerson();
            frm.ShowDialog();
            _UpdateDataGridView();
            _UpdateRecords();
        }

        private void AddNewPersontoolStripMenuI_Click(object sender, EventArgs e)
        {
            FrmAddNewPerson frm = new FrmAddNewPerson();
            frm.ShowDialog();
            _UpdateDataGridView();
            _UpdateRecords();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DeletePerson();
            _UpdateDataGridView();
            _UpdateRecords();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;


                _SelectedPersonIdFromGridView = dataGridView1.Rows[e.RowIndex].Cells["PersonID"].Value.ToString();

                

            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowInfo frm = new FrmShowInfo(PepoleBuisness.FindPersonByID(_SelectedPersonIdFromGridView));
            frm.ShowDialog();
        }

        private void _NotRedy()
        {
            MessageBox.Show("We Working On It!","Not Ready",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void PhoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _NotRedy();
        }

        private void SendEmailtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            _NotRedy();
        }

        private void EdittoolStripMenuI_Click(object sender, EventArgs e)
        {
            FrmUpdatePersonInfo frm = new FrmUpdatePersonInfo(PepoleBuisness.FindPersonByID(_SelectedPersonIdFromGridView));
            frm.ShowDialog();
            _UpdateDataGridView();
            _UpdateRecords();
        }
    }
}
