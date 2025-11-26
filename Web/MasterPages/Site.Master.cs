using Dominio;
using System;

namespace Web.MasterPages
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioActual"] != null)
            {
                Usuario usuarioLogeado = (Usuario)Session["UsuarioActual"];
                TextoUsuario.Text = usuarioLogeado?.Nombre;

                if (((Usuario)Session["UsuarioActual"]).Rol != "ADMIN")
                {
                    PanelSidebar.Visible = false;
                }
            }
            else
            {
                    Response.Redirect("../Auth/Login");
            }
        }

        protected void ClickBotonLogout(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("/Pages/Auth/Login");
        }
    }
}