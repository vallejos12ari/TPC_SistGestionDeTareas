using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Prioridad
    {
        public int IdPrioridad {  get; set; }
        public string NombrePrioridad { get; set; }
        public override string ToString() => NombrePrioridad;
    }
}
