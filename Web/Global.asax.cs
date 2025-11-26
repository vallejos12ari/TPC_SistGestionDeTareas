using System;
using System.Web;
using System.Web.Routing;
using Dominio;
using Microsoft.AspNet.FriendlyUrls;

namespace Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            FriendlyUrlSettings configuracion = new FriendlyUrlSettings();
            configuracion.AutoRedirectMode = RedirectMode.Permanent;
            RouteTable.Routes.EnableFriendlyUrls(configuracion);
        }
        
        protected void Session_Start(object sender, EventArgs e)
        {
            // Session["UsuarioActual"] = new Usuario
            // {
            //     Id = 2,
            //     Nombre = "Dev",
            //     Email = "dev@local",
            //     Rol = "SUPERVISOR"
            // };
        }
    }
}