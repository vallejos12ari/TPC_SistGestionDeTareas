using System;
using System.Collections.Generic;
using Dominio;
using Negocio;

namespace SistemaDeGestionDeTareas
{
    public partial class Prioridades : System.Web.UI.Page
    {
        private List<Prioridad> prioridades = new List<Prioridad>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            if (usuario.Rol != "Admin")
            {
                Response.Redirect("MisTareas.aspx");
            }

            if (!IsPostBack)
            {
                LoadPrioridades();
            }
        }

        private void LoadPrioridades()
        {
            PrioridadNegocio negocio = new PrioridadNegocio();
            prioridades = negocio.Listar();
            gvPrioridades.DataSource = prioridades;
            gvPrioridades.DataBind();
        }

        protected void gvPrioridades_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
          
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnCrear_Click1(object sender, EventArgs e)
        {
            Response.Redirect("CrearPrioridad.aspx");
        }

        protected void gvPrioridades_RowCommand1(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();

            if (e.CommandName == "Editar")
            {
                Response.Redirect($"EditarPrioridad.aspx?id={id}");
            }
            else if (e.CommandName == "Borrar")
            {
                PrioridadNegocio negocio = new PrioridadNegocio();
                negocio.Eliminar(Convert.ToInt32(id));

                LoadPrioridades();
            }

        }
    }
}
