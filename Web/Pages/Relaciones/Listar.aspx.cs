using Negocio;
using Dominio;
using System;
using System.Collections.Generic;

namespace Web.Pages.Relaciones
{
    public partial class Listar : System.Web.UI.Page
    {
        TipoRelacionNegocio negocio = new TipoRelacionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarRelaciones();
        }

        private void CargarRelaciones()
        {
            List<TipoRelacion> lista = negocio.Listar();
            TablaRelaciones.DataSource = lista;
            TablaRelaciones.DataBind();
        }

        protected void ClickBotonConfirmarEliminar(object sender, EventArgs e)
        {
            int id = int.Parse(IdRelacionAEliminar.Value);
            negocio.Eliminar(id);
            CargarRelaciones();
        }
    }
}