using System;

namespace Dominio
{
    public class TareaTag
    {
        public int Id { get; set; }
        public int TareaId { get; set; }
        public int TagId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}