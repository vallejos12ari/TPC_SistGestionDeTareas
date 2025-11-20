using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaDeGestionDeTareas
{
    public partial class Prioridades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrilla();

        }

        private void CargarGrilla()
        {
            PrioridadNegocio negocio = new PrioridadNegocio();
            gvPrioridades.DataSource = negocio.Listar();
            gvPrioridades.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            //Response.Redirect("FormularioPrioridad.aspx");
        }

        protected void gvPrioridades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect("FormularioPrioridad.aspx?id=" + id);
            }
            else if (e.CommandName == "Eliminar")
            {
                PrioridadNegocio negocio = new PrioridadNegocio();
                negocio.Eliminar(id);
                CargarGrilla();
            }
        }
    }
}