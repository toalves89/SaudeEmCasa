using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaudeEmCasa
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void vendaToolStripMenuItem_Click(object sender, EventArgs e)
        {

   
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {


            pnlEscTab.Location = new Point(3, this.Size.Height - 60);
        }
    }
}
