using System;

namespace Dominio
{
    public class Imagen
    {
        public int Id { get; set; }
        public int TareaId { get; set; }

        public string Nombre { get; set; }
        public string Mime { get; set; }
        public int? Size { get; set; }
        public string Path { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}