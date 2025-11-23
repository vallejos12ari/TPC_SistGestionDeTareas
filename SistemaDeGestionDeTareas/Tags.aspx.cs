using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaDeGestionDeTareas
{
    public partial class Tags : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrilla();
        }

        private void CargarGrilla()
        {
            TagNegocio negocio = new TagNegocio();
            gvTags.DataSource = negocio.Listar();
            gvTags.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("CrearTag.aspx");
        }

        protected void gvTags_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Editar")
            {
                Response.Redirect("EditarTag.aspx?id=" + id);

            }
            else if (e.CommandName == "Eliminar")
            {
                TagNegocio negocio = new TagNegocio();
                negocio.Eliminar(id);
                CargarGrilla();
            }
        }
    }
}