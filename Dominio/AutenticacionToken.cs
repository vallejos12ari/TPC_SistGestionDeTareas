using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class AutenticacionToken
    {
        public int IdToken { get; set; }
        public int IdUsuario { get; set; }
        public string Token { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }

        public Usuario Usuario { get; set; }
    }
}
