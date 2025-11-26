using System;

namespace Dominio
{
    public class Historial
    {
        public int Id { get; set; }
        public int TareaId { get; set; }
        public int UsuarioId { get; set; }

        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }

        public DateTime Fecha { get; set; }
    }
}