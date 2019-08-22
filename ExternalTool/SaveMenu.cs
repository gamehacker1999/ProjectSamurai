using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExternalTool
{
    public partial class SaveMenu : Form
    {
        //Field for the form
        private bool save;

        //Properties
        public bool Save
        {
            get { return save; }
        }
       


        public SaveMenu()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            save = true;
            Close();

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            save = false;
            Close();
        }

      
    }
}
