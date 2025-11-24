using Dominio;
using Negocio;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace SistemaDeGestionDeTareas
{
    public partial class EditarUsuario : System.Web.UI.Page
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

                Response.Redirect("Usuarios.aspx");
                return;
            }

            hfIdUsuario.Value = usuario.IdUsuario.ToString();
            txtNombreUsuario.Text = usuario.NombreUsuario;
            txtEmail.Text = usuario.Email;
            ddlRol.SelectedValue = usuario.Rol ?? "";
            chkActivo.Checked = usuario.Activo;
            CargarRelacionesUsuario(usuario.IdUsuario);
            CargarUsuariosDisponibles(usuario.IdUsuario);
        }

        private void CargarRelacionesUsuario(int idUsuarioActual)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            var usuario = negocio.ObtenerPorId(idUsuarioActual);
            if (usuario != null)
            {
                gvUsuariosRelacionados.DataSource = usuario.UsuariosRelacionados;
                gvUsuariosRelacionados.DataBind();
            }
        }

        private void CargarUsuariosDisponibles(int idUsuarioActual)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            var todosLosUsuarios = negocio.Listar();
            var usuarioActual = negocio.ObtenerPorId(idUsuarioActual);
            if (usuarioActual != null)
            {

                var usuariosFiltrados = todosLosUsuarios.Where(u => u.IdUsuario != idUsuarioActual && !usuarioActual.UsuariosRelacionados.Any(rel => rel.IdUsuario == u.IdUsuario)).ToList();

                ddlUsuariosDisponibles.DataSource = usuariosFiltrados;
                ddlUsuariosDisponibles.DataValueField = "IdUsuario";
                ddlUsuariosDisponibles.DataTextField = "NombreUsuario";
                ddlUsuariosDisponibles.DataBind();

                if (ddlUsuariosDisponibles.Items.Count == 0)
                    if (ddlUsuariosDisponibles.Items.Count == 0)
                    {
                        ddlUsuariosDisponibles.Items.Insert(0, new ListItem("No hay usuarios disponibles", ""));
                    }
                    else
                    {
                        ddlUsuariosDisponibles.Items.Insert(0, new ListItem("Seleccionar usuario a relacionar", ""));
                    }
            }
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


        protected void btnAgregarRelacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(ddlUsuariosDisponibles.SelectedValue))
                {
               
                    return;
                }

                int idUsuarioActual = int.Parse(hfIdUsuario.Value);
                int idUsuarioARelacionar = int.Parse(ddlUsuariosDisponibles.SelectedValue);

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.AgregarRelacion(idUsuarioActual, idUsuarioARelacionar);

                CargarRelacionesUsuario(idUsuarioActual);
                CargarUsuariosDisponibles(idUsuarioActual);
            }
            catch (Exception ex)
            {
              
                vsErrores.HeaderText = "Error al agregar la relación:";
                vsErrores.Controls.Add(new System.Web.UI.LiteralControl(ex.Message));
            }
        }

        protected void gvUsuariosRelacionados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                try
                {
                    int idUsuarioActual = int.Parse(hfIdUsuario.Value);
                    int idUsuarioARelacionar = int.Parse(e.CommandArgument.ToString());

                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.EliminarRelacion(idUsuarioActual, idUsuarioARelacionar);

                    
                    CargarRelacionesUsuario(idUsuarioActual);
                    CargarUsuariosDisponibles(idUsuarioActual);
                }
                catch (Exception ex)
                {
                 
                    vsErrores.HeaderText = "Error al eliminar la relación:";
                    vsErrores.Controls.Add(new System.Web.UI.LiteralControl(ex.Message));
                }
            }
        }

    }
}