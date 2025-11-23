using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace SistemaDeGestionDeTareas
{
    public partial class _Default : Page
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

            //if (usuario.Rol != "Admin")
            //{
            //  Response.Redirect("MisTareas.aspx");
            //}
        }

        protected void btnPruebaPrioridades_Click(object sender, EventArgs e)
        {
            Response.Redirect("Prioridades.aspx");
        }

        protected void btnPruebaTags_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tags.aspx");
        }
    }
}