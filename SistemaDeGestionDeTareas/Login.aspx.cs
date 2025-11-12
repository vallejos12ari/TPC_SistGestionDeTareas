using System;
using System.Web.Security;
using System.Web.UI;
using Negocio;

namespace SistemaDeGestionDeTareas
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User?.Identity?.IsAuthenticated == true && !IsPostBack)
            {
                if ( Session["UsuarioActual"] != null)
                {
                    
                }
                Response.Redirect("Default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            var negocio = new UsuarioNegocio();
            var usuario = negocio.Validar(email, password);

            if (usuario != null)
            {
                Session["UsuarioActual"] = usuario;

                bool recordarme = chkRecordarme.Checked;

                var returnUrl = Request.QueryString["ReturnUrl"];
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, recordarme);
                    Response.Redirect(returnUrl, endResponse: true);
                    return;
                }

                FormsAuthentication.RedirectFromLoginPage(usuario.NombreUsuario, recordarme);
            }
            else
            {
                lblError.Text = "Usuario o contraseña inválidos.";
            }
        }
    }
}