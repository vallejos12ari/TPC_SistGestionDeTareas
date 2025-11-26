using Dominio;
using Negocio;
using System;

namespace Web.Pages.Tags
{
    public partial class Editar : System.Web.UI.Page
    {
        TagNegocio negocio = new TagNegocio();

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
                IdTagHidden.Value = id.ToString();

                Tag t = negocio.BuscarPorId(id);

                TextoNombre.Text = t.Nombre;
                TextoColor.Text = t.Color;

                string script = "document.getElementById('SelectColor').value = '" + t.Color + "';";
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

            Tag t = new Tag();
            t.Id = int.Parse(IdTagHidden.Value);
            t.Nombre = nombre;
            t.Color = color;

            negocio.Modificar(t);

            Response.Redirect("Listar.aspx");
        }
    }
}