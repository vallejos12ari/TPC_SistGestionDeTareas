using Negocio;
using Dominio;
using System;
using System.Collections.Generic;

namespace Web.Pages.Prioridades
{
    public partial class Listar : System.Web.UI.Page
    {
        PrioridadNegocio negocio = new PrioridadNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarPrioridades();
        }

        private void CargarPrioridades()
        {
            List<Prioridad> lista = negocio.Listar();
            TablaPrioridades.DataSource = lista;
            TablaPrioridades.DataBind();
        }

        protected void ClickBotonConfirmarEliminar(object sender, EventArgs e)
        {
            int id = int.Parse(IdPrioridadAEliminar.Value);
            negocio.Eliminar(id);
            CargarPrioridades();
        }
    }
}