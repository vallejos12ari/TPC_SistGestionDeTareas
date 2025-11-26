using System;

namespace Dominio
{
    public class TipoRelacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte Eliminado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}