using System;
using Negocio;
using Dominio;

namespace Web
{
    public partial class Default : System.Web.UI.Page
    {
        private ReporteNegocio reporteNegocio = new ReporteNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];

            if (usuario == null || usuario.Rol == "USER")
            {
                Response.Redirect("~/Pages/Tareas/Listar.aspx");
                return;
            }

            if (!IsPostBack)
            {
                TextoDesde.Text = DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd");
                TextoHasta.Text = DateTime.Today.ToString("yyyy-MM-dd");
                EjecutarReportes();
            }
        }

        protected void ClickBotonFiltrar(object sender, EventArgs e)
        {
            EjecutarReportes();
        }

        private void EjecutarReportes()
        {
            DateTime desde;
            DateTime hasta;

            if (!DateTime.TryParse(TextoDesde.Text, out desde))
            {
                desde = DateTime.Today.AddDays(-30);
                TextoDesde.Text = desde.ToString("yyyy-MM-dd");
            }

            if (!DateTime.TryParse(TextoHasta.Text, out hasta))
            {
                hasta = DateTime.Today;
                TextoHasta.Text = hasta.ToString("yyyy-MM-dd");
            }

            var tareasPorEstado = reporteNegocio.TareasPorEstado(desde, hasta);
            TablaTareasPorEstado.DataSource = tareasPorEstado;
            TablaTareasPorEstado.DataBind();

            var vencidas = reporteNegocio.TareasVencidas(desde, hasta);
            TablaVencidas.DataSource = new[] { vencidas };
            TablaVencidas.DataBind();

            var horas = reporteNegocio.HorasPorUsuario(desde, hasta);
            TablaHorasPorUsuario.DataSource = horas;
            TablaHorasPorUsuario.DataBind();
        }
    }
}