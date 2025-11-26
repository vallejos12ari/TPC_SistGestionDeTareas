using System;

namespace Dominio
{
    public class Tag
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte Eliminado { get; set; }
        public string Color { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}