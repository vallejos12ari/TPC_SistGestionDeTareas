using System;
using System.Collections.Generic;
using System.Web.UI;
using Dominio;
using Negocio;

namespace SistemaDeGestionDeTareas
{
    public partial class Usuarios : Page
    {
        private List<Usuario> usuarios = new List<Usuario>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            if (usuario.Rol != "Admin")
            {
                Response.Redirect("MisTareas.aspx");
            }
            
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarios = usuarioNegocio.Listar();

            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();
        }

        protected void gvUsuarios_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string idUsuario = e.CommandArgument.ToString();

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarUsuario.aspx?id={idUsuario}");
            }
            else if (e.CommandName == "Borrar")
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                usuarioNegocio.Eliminar(Convert.ToInt32(idUsuario));
                LoadUsers();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearUsuario.aspx");
        }
    }
}