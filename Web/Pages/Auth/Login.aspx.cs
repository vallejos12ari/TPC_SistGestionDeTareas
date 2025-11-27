using Dominio;
using Negocio;
using System;

namespace Web.Pages.Auth
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioActual"] != null)
            {
                Response.Redirect("../Default.aspx");
            }
        }

        protected void BotonLoginClick(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblError.CssClass = "alert alert-danger d-none";

            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MostrarError("Debe completar todos los campos.");
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = negocio.Login(email, password);

            if (usuario == null)
            {
                MostrarError("Credenciales incorrectas o usuario inhabilitado.");
                return;
            }

            if (usuario.Verificado == 0)
            {
                Session["UsuarioPendiente"] = usuario;

                ClientScript.RegisterStartupScript(this.GetType(),
                    "MostrarModal", "AbrirModalPassword();", true);

                return;
            }

            Session["UsuarioActual"] = usuario;

            if (usuario.Rol == "ADMIN")
            {
                Response.Redirect("../Default.aspx");
                return;
            }

            Response.Redirect("../Tareas/Listar");
            
        }

        protected void BotonGuardarPassword(object sender, EventArgs e)
        {
            lblErrorPassword.Text = "";
            lblErrorPassword.CssClass = "alert alert-danger d-none";

            string pass1 = txtNuevaPassword.Text.Trim();
            string pass2 = txtRepetirPassword.Text.Trim();

            if (pass1.Length < 4)
            {
                MostrarErrorPassword("La contraseña debe tener al menos 4 caracteres.");
                return;
            }

            if (pass1 != pass2)
            {
                MostrarErrorPassword("Las contraseñas no coinciden.");
                return;
            }

            Usuario usuario = (Usuario)Session["UsuarioPendiente"];
            if (usuario == null)
            {
                MostrarError("Error interno. Intentá nuevamente.");
                return;
            }

            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.CambiarContrasenia(usuario.Id, pass1);

            Session["UsuarioActual"] = negocio.BuscarPorId(usuario.Id);
            Session.Remove("UsuarioPendiente");

            Response.Redirect("../Tareas/Listar.aspx");
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.CssClass = "alert alert-danger d-block";
        }

        private void MostrarErrorPassword(string mensaje)
        {
            lblErrorPassword.Text = mensaje;
            lblErrorPassword.CssClass = "alert alert-danger d-block";

            ClientScript.RegisterStartupScript(this.GetType(),
                "ReabrirModal", "AbrirModalPassword();", true);
        }
    }
}
