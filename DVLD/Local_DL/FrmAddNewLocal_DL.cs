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

namespace DVLD.Local_DL
{
    public partial class FrmAddNewLocal_DL : Form
    {
        public FrmAddNewLocal_DL()
        {
            InitializeComponent();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            _EnableSaveBtn();
        }

        private void _SetDefultVlue()
        {
            comboBoxClass.SelectedIndex = 0;
            labelApDate.Text = DateTime.Now.ToShortDateString();
            labelCurrentUserName.Text = UserBuisness.LoggedUser.UserName;
            labelApFees.Text = Global_Buisness.GetApFeesByID("1");
        }

        private void _EnableSaveBtn()
        {
            if (!userControlFindPerson1.IsPersonEmpty())
            {
                buttonSave.Enabled = true;
            }
        }

        private string _GetClassID()
        {
            return Convert.ToString ( comboBoxClass.SelectedIndex + 1 );
        }

        private string _CheckIfAPExisits()
        {
           return Local_Dl_Business.HasOpenLicenseApplication
                (userControlFindPerson1.PersonID, _GetClassID(), 1);
        }

        private Local_Dl_Business.clsAP _SetNewAP()
        {
            Local_Dl_Business.clsAP ab = new Local_Dl_Business.clsAP();

            ab.CreatedByUserID = UserBuisness.LoggedUser.UserID;
            ab.ApplicationDate = Convert.ToDateTime ( labelApDate.Text );
            ab.ApplicantPersonID = userControlFindPerson1.PersonID;
            ab.ApplicationTypeID = "1";
            ab.ApplicationStatus = "1";
            ab.LastStatusDate = Convert.ToDateTime(labelApDate.Text);
            ab.PaidFees = Global_Buisness.GetApFeesByID ("1");

            return ab;
        }

        private void _Save()
        {
            string AP = _CheckIfAPExisits();

            


            if (AP != null) 
            {
                MessageBox.Show
                    ($@"Choose another License Class,the selected Person Already have an active 
                        application for the selected class with id={AP}", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else if (Local_Dl_Business.IsHaveTheLicense
                (userControlFindPerson1.PersonID,_GetClassID()))
            {
                MessageBox.Show("This person already has this license.", "License Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
              string NewAP = Local_Dl_Business.AddNewLocalAP
                    (_SetNewAP(), _GetClassID());

                if (NewAP != null)
                {
                    labelID.Text = NewAP;
                    MessageBox.Show("Data saved successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private void FrmLocal_DL_Load(object sender, EventArgs e)
        {
            _SetDefultVlue();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _Save();
        }
    }
}
