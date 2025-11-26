using System;

namespace Dominio
{
    public class Comentario
    {
        public int Id { get; set; }
        public int TareaId { get; set; }
        public int UsuarioId { get; set; }

        public string Texto { get; set; }

        public byte Eliminado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}