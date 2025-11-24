using System;
using Dominio;
using Negocio;

namespace SistemaDeGestionDeTareas
{
    public partial class CrearUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            if (usuario == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (usuario.Rol != "Admin")
            {
                Response.Redirect("MisTareas.aspx");
            }

            if (!IsPostBack)
            {
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario nuevo = new Usuario
                {
                    NombreUsuario = txtNombreUsuario.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Rol = ddlRol.SelectedValue,
                    Activo = chkActivo.Checked,
                };

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Agregar(nuevo);

                Response.Redirect("Usuarios.aspx");
            }
            catch (Exception ex)
            {
                vsErrores.HeaderText = "Error al crear usuario:";
                vsErrores.Controls.Add(new System.Web.UI.LiteralControl(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }
    }
}