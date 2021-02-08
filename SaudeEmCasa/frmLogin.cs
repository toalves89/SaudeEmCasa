using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaudeEmCasa.Backend;

namespace SaudeEmCasa
{
    public partial class frmLogin : Form
    {
        //Variável que controla o login
        public bool LoginComSucesso = false;
            
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            LoginComSucesso = false;
            this.Close();
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            //Verifica se foi preenchido campos usuário
            if (edtUsu.Text == "")
            {
                MessageBox.Show("Campo obrigatório.", "Login - Saúde em Casa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                edtUsu.Focus();
                return;
            }
            else
            {

                if (edtSenha.Text == "")
                {
                    MessageBox.Show("Campo obrigatório.", "Login - Saúde em Casa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    edtSenha.Focus();
                    return;
                }
            }


            using (SqlConnection conn = new SqlConnection(Util.ObterConnectionString()))
            {
                //Busca sistema
                using (SqlCommand comm = new SqlCommand("select cod_sis from sis WHERE cod_sis = '" + Util.CodSistema + "'", conn))
                {
                    comm.CommandTimeout = 0;
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Sistem não cadastrado.", "Login - Saúde em Casa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        edtUsu.Focus();
                        return;
                    }
                }
                conn.Close();

                //Busca usuário
                using (SqlCommand comm = new SqlCommand("select cod_usu, descr from usu WHERE cod_usu = '" + edtUsu.Text + "'", conn))
                {
                    comm.CommandTimeout = 0;
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Usuário não localizado.", "Login - Saúde em Casa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        edtUsu.Focus();
                        return;
                    }
                }
                conn.Close();

                //Busca ligação entre usuário e sistema
                using (SqlCommand comm = new SqlCommand("select cod_sis, cod_usu from sis_usu WHERE cod_usu = '" + edtUsu.Text + "' and cod_sis = '" + Util.CodSistema + "'", conn))
                {
                    comm.CommandTimeout = 0;
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        MessageBox.Show("Usuário não tem permissão para acessar este sistema.", "Login - Saúde em Casa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        edtUsu.Focus();
                        return;
                    }
                }
                conn.Close();

                //Valida login
                String StrSQL;

                StrSQL = "";
                StrSQL += " declare @name varchar(30), @pwd3 varchar(20), @pwd4 varbinary(100) ";
                StrSQL += " set @name = '" + edtUsu.Text + "' ";
                StrSQL += " set @pwd3 = '" + edtSenha.Text + "' ";
                StrSQL += " select @pwd4 = senha from usu ";
                StrSQL += " where cod_usu = @name ; ";
                StrSQL += " select PwdCompare(@pwd3, @pwd4,0) resultado ";

                using (SqlCommand comm = new SqlCommand(StrSQL, conn))
                {
                    comm.CommandTimeout = 0;
                    conn.Open();

                    SqlDataReader reader = comm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == 0)
                            {
                                MessageBox.Show("Senha inválida.", "Login - Saúde em Casa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                edtSenha.Focus();
                                return;
                            }
                            else
                            {
                                Util.CodUsusuario = edtUsu.Text;
                                LoginComSucesso = true;
                                this.Close();
                            }
                        }
                    }
                }
            }
        }

        private void frmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar.CompareTo((char)Keys.Return)) == 0)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}
