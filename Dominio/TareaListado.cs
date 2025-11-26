using System;
using System.Collections.Generic;

namespace Dominio
{
    public class TareaListado
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        public string EstadoNombre { get; set; }
        public string EstadoColor { get; set; }

        public string PrioridadNombre { get; set; }
        public string PrioridadColor { get; set; }

        public string UsuarioAsignadoNombre { get; set; }

        public DateTime FechaVencimiento { get; set; }
        
        public List<Tag> Tags { get; set; }
        
        public string FechaVencimientoFormateada
        {
            get { return FechaVencimiento.ToString("dd/MM/yyyy"); }
        }
    }
}