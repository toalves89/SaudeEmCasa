using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaudeEmCasa
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Chama tela de login antes do Menu
//            frmLogin frm = new frmLogin();
//            frm.ShowDialog();

//            if (frm.LoginComSucesso)
//            {
                Application.Run(new frmPrincipal());
//            }

        }
    }
}
