using Dominio;
using Negocio;
using System;

namespace Web.Pages.Usuarios
{
    public partial class Crear : System.Web.UI.Page
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarRoles();
        }

        private void CargarRoles()
        {
            SelectRol.Items.Clear();
            SelectRol.Items.Add("ADMIN");
            SelectRol.Items.Add("SUPERVISOR");
            SelectRol.Items.Add("USER");
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            usuario.Nombre = TextoNombre.Text;
            usuario.Email = TextoEmail.Text;
            usuario.Rol = SelectRol.SelectedValue;
            usuario.Verificado = 0;
            usuario.Password = "gestiondeturnos";

            usuarioNegocio.Agregar(usuario);

            Response.Redirect("Listar.aspx");
        }
    }
}