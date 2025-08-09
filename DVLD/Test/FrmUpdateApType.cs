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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Test
{
    public partial class FrmUpdateApType : Form
    {
        private TestTypeBuisness.ClsTestTy _Test = new TestTypeBuisness.ClsTestTy();
        public FrmUpdateApType(TestTypeBuisness.ClsTestTy Test)
        {
            InitializeComponent();
            _Test = Test;
        }

        private void _SetTestInfo()
        {
            labelID.Text = _Test.ID;
            textBoxTitle.Text = _Test.Title;
            textBoxFees.Text = _Test.Fees;
            richTextBoxDescription.Text = _Test.Description;
        }

        private void _GetTestInfo()
        {
            
            
                _Test.Title = textBoxTitle.Text;
                _Test.Fees = textBoxFees.Text;
                _Test.Description = richTextBoxDescription.Text;
                
            
            

        }


        private bool _CheckIfEmpty()
        {
            if (string.IsNullOrEmpty(textBoxFees.Text) ||
                string.IsNullOrEmpty(textBoxTitle.Text)||
                string.IsNullOrEmpty(richTextBoxDescription.Text))
            {
                MessageBox.Show("Complete Info");
                return true;
            }
            return false;

        }

        private bool _Update()
        {

            _GetTestInfo();
            return TestTypeBuisness.UpdateTestTypes(_Test.ID, _Test);

           
        }

        private void FrmUpdateTest_Load(object sender, EventArgs e)
        {
            _SetTestInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(!_CheckIfEmpty())
            {
                

                if (_Update())
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
