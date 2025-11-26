using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public byte Eliminado { get; set; }
        public byte Verificado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<Usuario> UsuariosAsignados { get; set; } = new List<Usuario>();
    }
}