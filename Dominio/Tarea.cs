using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Tarea
    {
        public int IdTarea { get; set; }
        public int IdEstado { get; set; }
        public int IdPrioridad { get; set; }
        public int IdUsuarioCreador { get; set; }
        public int IdUsuarioAsignado { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaActualizacion { get; set;}


        public Estado Estado { get; set; }
        public Prioridad Prioridad { get; set; }
        public Usuario UsuarioCreador { get; set; }
        public Usuario UsuarioAsignado { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Imagen> Imagen { get; set; } = new List<Imagen>();
    }
}
