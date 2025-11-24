using System;
using System.Net;
using System.Net.Mail;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("it.vallejos.ari@gmail.com", "imju tghb bkvi mtei");
            server.EnableSsl = true;
            server.Port = 587;
            server.Host = "smtp.gmail.com";
        }

        public void ArmarCorreoAsignacion(string emailDestino, string tituloTarea, string asignador)
        {
            email = new MailMessage();
            email.From = new MailAddress("it.vallejos.ari@gmail.com");

            email.To.Add(emailDestino);

            email.Subject = "Nueva tarea asignada";

            email.IsBodyHtml = true;

            email.Body =
                $"<h2>Se te ha asignado una nueva tarea</h2>" +
                $"<p><strong>Título:</strong> {tituloTarea}</p>" +
                $"<p><strong>Asignado por:</strong> {asignador}</p>" +
                "<br/>" +
                "<p>Por favor revisa tu panel de tareas.</p>";
        }

        public void EnviarEmail()
        {
            server.Send(email);
        }
    }
}
