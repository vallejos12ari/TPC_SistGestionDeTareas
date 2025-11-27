using Dominio;
using Negocio;
using System;
using System.Text.RegularExpressions;

namespace Web.Pages.Usuarios
{
    public partial class Crear : System.Web.UI.Page
    {
        private UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
        }

        private void CargarRoles()
        {
            SelectRol.Items.Clear();
            SelectRol.Items.Add("ADMIN");
            SelectRol.Items.Add("SUPERVISOR");
            SelectRol.Items.Add("USER");
        }

        private bool ValidarCampos()
        {
            if (TextoNombre.Text.Trim().Length < 3)
            {
                ErrorCrear.Text = "El nombre debe tener al menos 3 caracteres.";
                return false;
            }

            if (TextoNombre.Text.Trim().Length > 100)
            {
                ErrorCrear.Text = "El nombre no puede superar los 100 caracteres.";
                return false;
            }

            if (TextoNombre.Text.Contains("<") || TextoNombre.Text.Contains(">"))
            {
                ErrorCrear.Text = "El nombre contiene caracteres inválidos.";
                return false;
            }

            if (TextoEmail.Text.Trim().Length == 0)
            {
                ErrorCrear.Text = "El email es obligatorio.";
                return false;
            }

            if (!Regex.IsMatch(TextoEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ErrorCrear.Text = "El email no tiene un formato válido.";
                return false;
            }

            bool yaExiste = usuarioNegocio.EmailExiste(TextoEmail.Text.Trim());
            if (yaExiste)
            {
                ErrorCrear.Text = "Ya existe un usuario con ese email.";
                return false;
            }

            if (string.IsNullOrEmpty(SelectRol.SelectedValue))
            {
                ErrorCrear.Text = "Debe seleccionar un rol.";
                return false;
            }

            return true;
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            ErrorCrear.Text = "";

            if (!ValidarCampos())
            {
                return;
            }

            Usuario usuario = new Usuario();

            usuario.Nombre = TextoNombre.Text.Trim();
            usuario.Email = TextoEmail.Text.Trim();
            usuario.Rol = SelectRol.SelectedValue;
            usuario.Verificado = 0;
            usuario.Password = "gestiondetareas";

            usuarioNegocio.Agregar(usuario);

            Response.Redirect("Listar.aspx");
        }
    }
}