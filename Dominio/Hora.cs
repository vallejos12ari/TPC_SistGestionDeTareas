using System;

namespace Dominio
{
    public class Hora
    {
        public int Id { get; set; }
        public int TareaId { get; set; }
        public int UsuarioId { get; set; }

        public decimal Horas { get; set; }
        public DateTime Dia { get; set; }

        public byte Eliminado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}