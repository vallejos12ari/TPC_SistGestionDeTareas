using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ReporteRendimiento
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public int TareasCreadas { get; set; }
        public int TareasAsignadas { get; set; }
        public int TareasCompletadas { get; set; }
        public int TareasPendientes { get; set; }
        public int TareasVencidas { get; set; }
    }
}