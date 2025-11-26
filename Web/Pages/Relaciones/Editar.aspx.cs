using Negocio;
using Dominio;
using System;

namespace Web.Pages.Relaciones
{
    public partial class Editar : System.Web.UI.Page
    {
        TipoRelacionNegocio negocio = new TipoRelacionNegocio();

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
                IdRelacionHidden.Value = id.ToString();

                TipoRelacion r = negocio.BuscarPorId(id);
                TextoNombre.Text = r.Nombre;
            }
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            string nombre = TextoNombre.Text.Trim();

            if (nombre.Length == 0)
            {
                ErrorEditar.Text = "El nombre no puede estar vacío.";
                return;
            }

            TipoRelacion r = new TipoRelacion();
            r.Id = int.Parse(IdRelacionHidden.Value);
            r.Nombre = nombre;

            negocio.Modificar(r);

            Response.Redirect("Listar.aspx");
        }
    }
}