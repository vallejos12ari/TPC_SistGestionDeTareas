using Dominio;
using Negocio;
using System;

namespace Web.Pages.Estados
{
    public partial class Crear : System.Web.UI.Page
    {
        EstadoNegocio estadoNegocio = new EstadoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextoColor.Text = "#FFE066";
                string script = "document.getElementById('SelectColor').value = '#FFE066';";
                ClientScript.RegisterStartupScript(GetType(), "SetColorCrear", script, true);
            }
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
                ErrorCrear.Text = "El color seleccionado no es válido.";
                return;
            }

            Estado estado = new Estado();
            estado.Nombre = nombre;
            estado.Color = color;
            estado.EsFinal = CheckEsFinal.Checked ? (byte)1 : (byte)0;

            estadoNegocio.Agregar(estado);

            Response.Redirect("Listar.aspx");
        }
    }
}