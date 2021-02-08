using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SaudeEmCasa.Backend
{
    class Util
    {
        protected static string CodUsu;
        public static string CodUsusuario
        {
            get
            {
                return CodUsu;
            }
            set
            {
                CodUsu = value;
            }
        }

        protected static string CodSis = "SFC";
        public static string CodSistema
        {
            get
            {
                return CodSis;
            }
        }

        public static string ObterConnectionString()
        {
            string conn = string.Empty;

            try
            {
                conn = ConfigurationManager.ConnectionStrings["sgf_bd"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return conn;
        }
    }
}
