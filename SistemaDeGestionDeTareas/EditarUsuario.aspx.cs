using System;
using Dominio;
using Negocio;

namespace SistemaDeGestionDeTareas
{
    public partial class EditarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            if (usuario.Rol != "Admin")
            {
                Response.Redirect("MisTareas.aspx");
            }
            
            if (!IsPostBack)
            {
                var idStr = Request.QueryString["id"];
                if (!int.TryParse(idStr, out int id) || id <= 0)
                {
                    Response.Redirect("Usuarios.aspx");
                    return;
                }

                CargarUsuario(id);
            }
        }

        private void CargarUsuario(int id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            var usuario = negocio.ObtenerPorId(id);

            if (usuario == null)
            {
                // No encontrado → volvemos al listado
                Response.Redirect("Usuarios.aspx");
                return;
            }

            hfIdUsuario.Value = usuario.IdUsuario.ToString();
            txtNombreUsuario.Text = usuario.NombreUsuario;
            txtEmail.Text = usuario.Email;
            ddlRol.SelectedValue = usuario.Rol ?? "";
            chkActivo.Checked = usuario.Activo;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            if (!int.TryParse(hfIdUsuario.Value, out int id) || id <= 0)
            {
                Response.Redirect("Usuarios.aspx");
                return;
            }

            try
            {
                var usuario = new Usuario
                {
                    IdUsuario = id,
                    NombreUsuario = txtNombreUsuario.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Rol = ddlRol.SelectedValue,
                    Activo = chkActivo.Checked,
                    // Si el campo viene vacío, no actualizamos la contraseña
                    Password = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text.Trim()
                };

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.Actualizar(usuario);

                Response.Redirect("Usuarios.aspx");
            }
            catch (Exception ex)
            {
                vsErrores.HeaderText = "Error al actualizar el usuario:";
                vsErrores.Controls.Add(new System.Web.UI.LiteralControl(ex.Message));
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }
    }
}