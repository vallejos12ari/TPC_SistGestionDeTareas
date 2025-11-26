using Dominio;
using Negocio;
using System;

namespace Web.Pages.Prioridades
{
    public partial class Editar : System.Web.UI.Page
    {
        PrioridadNegocio negocio = new PrioridadNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Listar.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int id = int.Parse(Request.QueryString["id"]);
                IdPrioridadHidden.Value = id.ToString();

                Prioridad p = negocio.BuscarPorId(id);

                TextoNombre.Text = p.Nombre;
                TextoColor.Text = p.Color;

                string script = "document.getElementById('SelectColor').value = '" + p.Color + "';";
                ClientScript.RegisterStartupScript(GetType(), "SetColor", script, true);
            }
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            string nombre = TextoNombre.Text.Trim();
            string color = TextoColor.Text.Trim();

            if (nombre.Length == 0)
            {
                ErrorEditar.Text = "El nombre no puede estar vacío.";
                return;
            }

            if (!color.StartsWith("#") || color.Length != 7)
            {
                ErrorEditar.Text = "El color no es válido.";
                return;
            }

            Prioridad p = new Prioridad();
            p.Id = int.Parse(IdPrioridadHidden.Value);
            p.Nombre = nombre;
            p.Color = color;

            negocio.Modificar(p);

            Response.Redirect("Listar.aspx");
        }
    }
}