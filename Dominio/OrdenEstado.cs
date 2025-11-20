using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class OrdenEstado
    {
        public int IdOrden { get; set; }
        public Estado EstadoActual { get; set; }
        public Estado EstadoDestino { get; set; }
    }
}
