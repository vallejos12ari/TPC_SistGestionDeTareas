using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace Web.Pages.Usuarios
{
    public partial class Listar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarUsuarios();
            }
        }

        private void CargarUsuarios()
        {
            UsuarioNegocio negocioUsuarios = new UsuarioNegocio();
            List<Usuario> listaUsuarios = negocioUsuarios.Listar();
            TablaUsuarios.DataSource = listaUsuarios;
            TablaUsuarios.DataBind();
        }

        protected void ClickBotonConfirmarEliminar(object sender, EventArgs e)
        {
            int id = int.Parse(IdUsuarioAEliminar.Value);
            UsuarioNegocio negocio = new UsuarioNegocio();
            negocio.Eliminar(id);
            CargarUsuarios();
        }
    }
}