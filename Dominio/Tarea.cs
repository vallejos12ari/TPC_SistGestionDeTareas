using System;

namespace Dominio
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }

        public int UsuarioId { get; set; }
        public int CreadoPor { get; set; }

        public decimal HsEstimadas { get; set; }

        public int EstadoId { get; set; }
        public int PrioridadId { get; set; }
        public int? TipoRelacionId { get; set; }
        public int? RelacionadoId { get; set; }

        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}