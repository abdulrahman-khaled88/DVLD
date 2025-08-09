using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Pepole;
using DVLD_Buisness;

namespace DVLD.Users
{
    public partial class FrmManageUsers : Form
    {
        public FrmManageUsers()
        {
            InitializeComponent();
        }

        

        private void _UpdateRecord()
        {
            labelRecords.Text = dataGridView1.RowCount.ToString();
        }

        private void _UpdateTextBoxFilter()
        {
            if (comboBoxFilter.SelectedIndex == 0 || comboBoxFilter.SelectedIndex == 5)
            {
                textBoxFilter.Visible = false;
            }
            else
            {
                textBoxFilter.Visible = true;
            }
        }

        private void _UpdatecomboBoxIsActiveFilter()
        {
            if (comboBoxFilter.SelectedIndex == 5)
            {
                comboBoxIsActiveFilter.Visible = true;
            }
            else
            {
                comboBoxIsActiveFilter.Visible = false;
            }
            if (comboBoxIsActiveFilter.Text != "All")
            {
                dataGridView1.DataSource = UserBuisness.ListAllUsersInfoByFilter("IsActive"
                    , comboBoxIsActiveFilter.Text == "Yes" ? "1" : "0");
                _UpdateRecord();
            }
            else
            {
                dataGridView1.DataSource = UserBuisness.ListAllUsersInfo();
                _UpdateRecord();
            }

        }


        private void _SetDefultValus()
        {
            comboBoxFilter.SelectedIndex = 0;
            comboBoxIsActiveFilter.SelectedIndex = 0;
            textBoxFilter.Visible = false;
            comboBoxIsActiveFilter.Visible = false;
        }

        private void _UpdateDataGrid()
        {
            if (comboBoxFilter.SelectedIndex == 0)
            {
                dataGridView1.DataSource = UserBuisness.ListAllUsersInfo();
            }
            else if (comboBoxFilter.SelectedIndex == 1)
            {
                if (!string.IsNullOrEmpty(textBoxFilter.Text))
                {
                    dataGridView1.DataSource = UserBuisness.ListAllUsersInfoByFilter("PersonID", textBoxFilter.Text);
                }
                else
                {
                    dataGridView1.DataSource = UserBuisness.ListAllUsersInfo();
                }
            }
            else if (comboBoxFilter.SelectedIndex == 2)
            {
                if (!string.IsNullOrEmpty(textBoxFilter.Text))
                {
                    dataGridView1.DataSource = UserBuisness.ListAllUsersInfoByFilter("UserID", textBoxFilter.Text);
                }
                else
                {
                    dataGridView1.DataSource = UserBuisness.ListAllUsersInfo();
                }
                
            }
            else if (comboBoxFilter.SelectedIndex == 3)
            {

                if (!string.IsNullOrEmpty(textBoxFilter.Text))
                {
                    dataGridView1.DataSource = UserBuisness.ListAllUsersInfoByFilter("UserName", textBoxFilter.Text);
                }
                else
                {
                    dataGridView1.DataSource = UserBuisness.ListAllUsersInfo();
                }
                
            }
            else if (comboBoxFilter.SelectedIndex == 4)
            {

                if (!string.IsNullOrEmpty(textBoxFilter.Text))
                {
                    dataGridView1.DataSource = UserBuisness.ListAllUsersInfoByFilter("FullName", textBoxFilter.Text);
                }
                else
                {
                    dataGridView1.DataSource = UserBuisness.ListAllUsersInfo();
                }

            }
            else if (comboBoxFilter.SelectedIndex == 5)
            {
                _UpdatecomboBoxIsActiveFilter();
            }



        }

        private void FrmManageUsers_Load(object sender, EventArgs e)
        {
            
            _SetDefultValus();
            _UpdateDataGrid();
            _UpdateRecord();
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {


            _UpdateDataGrid();
            _UpdateRecord();


        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdateTextBoxFilter();
            _UpdatecomboBoxIsActiveFilter();
            _UpdateDataGrid();
        }

        private void comboBoxIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdatecomboBoxIsActiveFilter();
        }

        private void buttonAddNewUser_Click(object sender, EventArgs e)
        {
            AddNewUser frm = new AddNewUser(AddNewUser.Mode.AddNew);
            frm.ShowDialog();
            _UpdateDataGrid();
            _UpdateRecord();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(UserBuisness.FindUserByID(UserBuisness._SelectedUserIdGrid));
            frm.ShowDialog();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;


                UserBuisness._SelectedUserIdGrid = dataGridView1.Rows[e.RowIndex].Cells["UserID"].Value.ToString();



            }
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser frm = new AddNewUser(AddNewUser.Mode.AddNew);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frm = new frmChangeUserPassword
                (UserBuisness.FindUserByID(UserBuisness._SelectedUserIdGrid));
            frm.ShowDialog();
        }

        private void _DeletePerson()
        {
            if (MessageBox.Show($"Are you sure you want to delete User [{UserBuisness._SelectedUserIdGrid}]"
              , "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (UserBuisness.DeleteUser(UserBuisness._SelectedUserIdGrid))
                {
                    MessageBox.Show("User Deleted Successfuly.", "Successful", MessageBoxButtons.OK
                        , MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("User Was not deleted."
                        , "Error", MessageBoxButtons.OK
                        , MessageBoxIcon.Error);
                }
            }
        }

        private void _NotRedy()
        {
            MessageBox.Show("We Working On It!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _DeletePerson();
            _UpdateDataGrid();
            _UpdateRecord();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _NotRedy();
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _NotRedy();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewUser frm = new AddNewUser(AddNewUser.Mode.Update);
            frm.ShowDialog();
            _UpdateDataGrid();
            _UpdateRecord();
        }
    }
}
