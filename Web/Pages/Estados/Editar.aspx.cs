using Dominio;
using Negocio;
using System;

namespace Web.Pages.Estados
{
    public partial class Editar : System.Web.UI.Page
    {
        EstadoNegocio estadoNegocio = new EstadoNegocio();

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
                IdEstadoHidden.Value = id.ToString();

                Estado estado = estadoNegocio.BuscarPorId(id);

                TextoNombre.Text = estado.Nombre;
                TextoColor.Text = estado.Color;

                string script = "document.getElementById('SelectColor').value = '" + estado.Color + "';";
                ClientScript.RegisterStartupScript(GetType(), "SetColorEditar", script, true);
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
                ErrorEditar.Text = "El color seleccionado no es válido.";
                return;
            }

            Estado estado = new Estado();
            estado.Id = int.Parse(IdEstadoHidden.Value);
            estado.Nombre = nombre;
            estado.Color = color;

            estadoNegocio.Modificar(estado);

            Response.Redirect("Listar.aspx");
        }
    }
}