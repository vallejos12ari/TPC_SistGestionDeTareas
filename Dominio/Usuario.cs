using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string ContraseniaHash { get; set; }
        public string Rol {  get; set; }
        public bool Activo {  get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        //public Estado Estado { get; set; }
        //public Prioridades Prioridades { get; set; }
        //public Tarea Tarea { get; set; }
        //public Tags Tags { get; set; }
        //public TareasTags TareasTags { get; set; }
        //faltan img
        //falta autenticacionTokens

        public List<Tarea> TareasCreadas { get; set; } = new List<Tarea>();
        public List<Tarea> TareasAsignadas { get; set; } = new List<Tarea>();
        public List<AutenticacionToken> Tokens { get; set; } = new List<AutenticacionToken>();



    }
}
