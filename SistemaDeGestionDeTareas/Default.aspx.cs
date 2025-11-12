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
            if (usuario.Rol != "Admin")
            {
                Response.Redirect("MisTareas.aspx");
            }
        }
    }
}