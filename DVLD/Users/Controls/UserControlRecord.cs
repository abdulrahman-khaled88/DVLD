using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users.Controls
{
    public partial class UserControlRecord : UserControl
    {

        public UserControlRecord()
        {
            InitializeComponent();
        }

        
        public string RecordsText
        {
         
            get { return labelRecords.Text; }
            set { labelRecords.Text = value; }
        }


    }
}
