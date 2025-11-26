using Dominio;
using System;

namespace Web.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioActual"] == null)
            {
                Response.Redirect("Auth/Login");
            }
            
            if (((Usuario)Session["UsuarioActual"])?.Rol != "ADMIN")
            {
                Response.Redirect("Pages/Tareas/Listar");
            }
        }
    }
}