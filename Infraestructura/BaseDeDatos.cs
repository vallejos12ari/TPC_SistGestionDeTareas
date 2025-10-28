using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Infraestructura
{
    public class BaseDeDatos
    {

        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DATABASE"].ConnectionString;

        public static SqlConnection ObtenerConexion()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
