using Negocio;
using Dominio;
using System;

namespace Web.Pages.Relaciones
{
    public partial class Crear : System.Web.UI.Page
    {
        TipoRelacionNegocio negocio = new TipoRelacionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            string nombre = TextoNombre.Text.Trim();

            if (nombre.Length == 0)
            {
                ErrorCrear.Text = "El nombre no puede estar vacío.";
                return;
            }

            TipoRelacion r = new TipoRelacion();
            r.Nombre = nombre;

            negocio.Agregar(r);

            Response.Redirect("Listar.aspx");
        }
    }
}