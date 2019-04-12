using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EasyAIO
{
    internal partial class NewNameFrm : Form
    {
        public NewNameFrm()
        {
            InitializeComponent();
        }
        public string ToolName
        {
            get { return txtbox_name.Text; }
            set { txtbox_name.Text = value; }
        }
            
    }
}
