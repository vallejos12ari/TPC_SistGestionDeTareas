using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaDeGestionDeTareas
{
    public partial class ReporteRendimiento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarReporte();
        }

        private void CargarReporte()
        {
            ReporteRendimientoNegocio negocio = new ReporteRendimientoNegocio();
            gvReporte.DataSource = negocio.Listar();
            gvReporte.DataBind();
        }
    }
}