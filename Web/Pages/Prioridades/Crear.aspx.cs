using Dominio;
using Negocio;
using System;

namespace Web.Pages.Prioridades
{
    public partial class Crear : System.Web.UI.Page
    {
        PrioridadNegocio negocio = new PrioridadNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                TextoColor.Text = "#FFE066";
        }

        protected void ClickBotonCrear(object sender, EventArgs e)
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

            Prioridad p = new Prioridad();
            p.Nombre = nombre;
            p.Color = color;

            negocio.Agregar(p);

            Response.Redirect("Listar.aspx");
        }
    }
}