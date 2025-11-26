using System;
using System.Collections.Generic;

namespace Dominio
{
    public class FiltroTareaBusqueda
    {
        public int? EstadoId { get; set; }
        public int? PrioridadId { get; set; }
        public int? UsuarioAsignadoId { get; set; }
        public string Texto { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int? TagId { get; set; }
    }
}