using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TareaNegocio
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        // 🔹 Método real que usaremos en el sistema
        public void NotificarAsignacion(int idUsuarioAsignado, Tarea tarea)
        {
            try
            {
                // 1) Obtener usuario asignado
                Usuario usuarioAsignado = usuarioNegocio.ObtenerPorId(idUsuarioAsignado);

                if (usuarioAsignado == null || string.IsNullOrEmpty(usuarioAsignado.Email))
                    throw new Exception("El usuario asignado no existe o no tiene email configurado.");

                // 2) Obtener usuario que asignó
                Usuario usuarioCreador = usuarioNegocio.ObtenerPorId(tarea.IdUsuarioCreador);

                string nombreAsignador = usuarioCreador != null
                    ? usuarioCreador.NombreUsuario
                    : "Administrador";

                // 3) Armar correo
                EmailService mail = new EmailService();
                mail.ArmarCorreoAsignacion(
                    usuarioAsignado.Email,
                    tarea.Titulo,
                    nombreAsignador
                );

                // 4) Enviar
                mail.EnviarEmail();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar la notificación de asignación", ex);
            }
        }

        public void NotificarAsignacion(string emailDestino, string tituloTarea, string asignador)
        {
            try
            {
                EmailService mail = new EmailService();
                mail.ArmarCorreoAsignacion(emailDestino, tituloTarea, asignador);
                mail.EnviarEmail();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar la notificación de tarea.", ex);
            }
        }

    }
}