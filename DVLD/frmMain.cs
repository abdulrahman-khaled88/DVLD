using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Applications;
using DVLD.Drivers;
using DVLD.Licenses;
using DVLD.Local_DL;
using DVLD.Pepole;
using DVLD.Test;
using DVLD.Users;
using DVLD_Buisness;

namespace DVLD
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmPeople frm = new FrmPeople();
            frm.MdiParent = this;
            frm.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void applicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManageUsers frm = new FrmManageUsers();
            frm.MdiParent = this;
            frm.Show();
        }

        private void CurrentUserInfotoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo
                (UserBuisness.FindUserByID(UserBuisness.LoggedUser.UserID));

            frm.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void ChangePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangeUserPassword frm = new frmChangeUserPassword
                (UserBuisness.FindUserByID(UserBuisness.LoggedUser.UserID));

            frm.ShowDialog();
        }

        private void manageApplicatioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApTypes frm = new frmApTypes();
            frm.MdiParent = this;
            frm.Show();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTestType frm = new FrmTestType();
            frm.MdiParent = this;
            frm.Show();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddNewLocal_DL frm = new FrmAddNewLocal_DL();
            frm.MdiParent = this;
            frm.Show();
        }



        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmManageDrivers frm = new frmManageDrivers();
            frm.MdiParent = this;
            frm.Show();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMangeLocal_DL frm = new frmMangeLocal_DL();
            frm.MdiParent = this;
            frm.Show();
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicense frm = new frmInternationalLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicense frm = new frmRenewLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void rePlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLicense frm = new frmReplaceLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddDetainLicense frm = new frmAddDetainLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense();
            frm.MdiParent = this;
            frm.Show();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
