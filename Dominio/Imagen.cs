using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Imagen
    {
        public int IdImagen { get; set; }
        public int IdTarea { get; set; }
        public string UrlImagen { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Tarea Tarea { get; set; }
    }
}
