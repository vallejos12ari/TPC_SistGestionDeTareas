using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace Web.Pages.Estados
{
    public partial class Listar : System.Web.UI.Page
    {
        private EstadoNegocio estadoNegocio = new EstadoNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarEstados();
        }

        private void CargarEstados()
        {
            List<Estado> lista = estadoNegocio.Listar();
            TablaEstados.DataSource = lista;
            TablaEstados.DataBind();
        }

        protected void ClickBotonConfirmarEliminar(object sender, EventArgs e)
        {
            int id = int.Parse(IdEstadoAEliminar.Value);
            estadoNegocio.Eliminar(id);
            CargarEstados();
        }
    }
}