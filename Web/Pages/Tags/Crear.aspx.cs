using Negocio;
using Dominio;
using System;

namespace Web.Pages.Tags
{
    public partial class Crear : System.Web.UI.Page
    {
        TagNegocio negocio = new TagNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            string nombre = TextoNombre.Text.Trim();
            string color = TextoColor.Text.Trim();

            if (nombre.Length == 0)
            {
                ErrorCrear.Text = "El nombre no puede estar vacío.";
                return;
            }

            if (!color.StartsWith("#") || color.Length != 7)
            {
                ErrorCrear.Text = "El color no es válido.";
                return;
            }

            Tag t = new Tag();
            t.Nombre = TextoNombre.Text.Trim();
            t.Color = TextoColor.Text.Trim();

            negocio.Agregar(t);

            Response.Redirect("Listar.aspx");
        }
    }
}