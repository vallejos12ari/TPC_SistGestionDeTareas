using Negocio;
using Dominio;
using System;
using System.Collections.Generic;

namespace Web.Pages.Tags
{
    public partial class Listar : System.Web.UI.Page
    {
        TagNegocio negocio = new TagNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarTags();
        }

        private void CargarTags()
        {
            List<Tag> lista = negocio.Listar();
            TablaTags.DataSource = lista;
            TablaTags.DataBind();
        }

        protected void ClickBotonConfirmarEliminar(object sender, EventArgs e)
        {
            int id = int.Parse(IdTagAEliminar.Value);
            negocio.Eliminar(id);
            CargarTags();
        }
    }
}