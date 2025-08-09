using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Local_DL
{
    public partial class UserControlApInfo : UserControl
    {
        public UserControlApInfo()
        {
            InitializeComponent();
        }

        public string LDL_ID
        {
            get { return labelD_L_ApID.Text; }
            set { labelD_L_ApID.Text = value; }
        }

        public string ApplicationID
        {
            get { return labelID.Text; }
            set { labelID.Text = value; }
        }

        public string ApplicationsPaidFees
        {
            get { return labelFees.Text; }
            set { labelFees.Text = value; }
        }

        public string ApplicationDate
        {
            get { return labelDate.Text; }
            set { labelDate.Text = value; }
        }

        public string ApplicationTypeTitle
        {
            get { return labelType.Text; }
            set { labelType.Text = value; }
        }

        public string Applicant
        {
            get { return labelApplicant.Text; }
            set { labelApplicant.Text = value; }
        }

        public string LicenseClasseName
        {
            get { return labelLicenseClass.Text; }
            set { labelLicenseClass.Text = value; }
        }

        public string ApplicationStatusText
        {
            get { return labelStatus.Text; }
            set { labelStatus.Text = value; }
        }

        public string CreatedBy
        {
            get { return labelCreatedBy.Text; }
            set { labelCreatedBy.Text = value; }
        }


    }
}
