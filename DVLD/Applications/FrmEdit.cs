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
    public partial class FrmEdit : Form
    {
        private ApTypeBuisness.ClsApType _Application = 
            new ApTypeBuisness.ClsApType();
        public FrmEdit(ApTypeBuisness.ClsApType Application)
        {
            InitializeComponent();
            _Application = Application;
        }

        private void _SetApplicationIbfo()
        {
            labelID.Text = _Application.ID;
            textBoxTitle.Text = _Application.Title;
            textBoxFees.Text = _Application.Fees;
        }

        private void FrmEdit_Load(object sender, EventArgs e)
        {
            _SetApplicationIbfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _CheckIfEmpty()
        {
            if(string.IsNullOrEmpty(textBoxFees.Text) ||
                string.IsNullOrEmpty(textBoxTitle.Text))
            {
                MessageBox.Show("Complete Info");
                return true;
            }
            return false;

        }

        private bool _Update()
        {
            _Application.Title = textBoxTitle.Text;
            _Application.Fees = textBoxFees.Text;
           return ApTypeBuisness.UpdateApTypes(_Application.ID, _Application);
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!_CheckIfEmpty())
            {
                if(_Update())
                {
                    MessageBox.Show("Updated successfully!", "Update Status",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Update failed. Please try again.", "Update Status",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void textBoxFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
    }
}
