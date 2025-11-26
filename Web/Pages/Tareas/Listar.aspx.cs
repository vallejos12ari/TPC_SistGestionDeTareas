using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Tareas
{
    public partial class Listar : Page
    {
        private readonly TareaNegocio tareaNegocio = new TareaNegocio();
        private readonly EstadoNegocio estadoNegocio = new EstadoNegocio();
        private readonly PrioridadNegocio prioridadNegocio = new PrioridadNegocio();
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private readonly TagNegocio tagNegocio = new TagNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarTareas();
            }
        }

        private string RolUsuario()
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            return usuario?.Rol ?? "";
        }

        private int IdUsuarioActual()
        {
            Usuario usuario = (Usuario)Session["UsuarioActual"];
            return usuario.Id;
        }

        private void CargarFiltros()
        {
            string rol = RolUsuario();

            FiltroEstado.Items.Clear();
            FiltroEstado.Items.Add(new ListItem("Todos", ""));
            foreach (Estado e in estadoNegocio.Listar())
                FiltroEstado.Items.Add(new ListItem(e.Nombre, e.Id.ToString()));
            FiltroEstado.SelectedIndex = 0;
            FiltroPrioridad.Items.Clear();
            FiltroPrioridad.Items.Add(new ListItem("Todos", ""));
            foreach (Prioridad p in prioridadNegocio.Listar())
                FiltroPrioridad.Items.Add(new ListItem(p.Nombre, p.Id.ToString()));
            FiltroPrioridad.SelectedIndex = 0;

            if (rol == "ADMIN" || rol == "SUPERVISOR")
            {
                FiltroUsuarioContainer.Visible = true;
                UsuariosRelacionadosNegocio usuariosRelacionadosNegocio = new UsuariosRelacionadosNegocio();

                FiltroUsuarioAsignado.Items.Clear();
                FiltroUsuarioAsignado.Items.Add(new ListItem("Todos", ""));
                foreach (Usuario u in usuariosRelacionadosNegocio.ListarAsignados(IdUsuarioActual()))
                    FiltroUsuarioAsignado.Items.Add(new ListItem(u.Nombre, u.Id.ToString()));
                FiltroUsuarioAsignado.SelectedIndex = 0;
                PanelBotonCrear.Visible = true;
            }
            else
            {
                FiltroUsuarioContainer.Visible = false;
            }

            FiltroTags.Items.Clear();
            FiltroTags.Items.Add(new ListItem("Todos", ""));
            foreach (Tag t in tagNegocio.Listar())
                FiltroTags.Items.Add(new ListItem(t.Nombre, t.Id.ToString()));
            FiltroTags.SelectedIndex = 0;
        }

        private void CargarTareas()
        {
            string rol = RolUsuario();
            int? idUsuario = IdUsuarioActual();

            int? idEstado = string.IsNullOrEmpty(FiltroEstado.SelectedValue) ? (int?)null : int.Parse(FiltroEstado.SelectedValue);
            int? idPrioridad = string.IsNullOrEmpty(FiltroPrioridad.SelectedValue) ? (int?)null : int.Parse(FiltroPrioridad.SelectedValue);

            int? idAsignado = null;

            if (rol == "ADMIN" || rol == "SUPERVISOR")
            {
                if (string.IsNullOrEmpty(FiltroUsuarioAsignado.SelectedValue))
                    idAsignado = idUsuario;
                else
                    idAsignado = int.Parse(FiltroUsuarioAsignado.SelectedValue);
            }
            else
            {
                if (idUsuario != null)
                    idAsignado = idUsuario.Value;
            }

            int? idTag = string.IsNullOrEmpty(FiltroTags.SelectedValue) ? (int?)null : int.Parse(FiltroTags.SelectedValue);

            DateTime? desde = string.IsNullOrEmpty(FiltroDesde.Text) ? (DateTime?)null : DateTime.Parse(FiltroDesde.Text);
            DateTime? hasta = string.IsNullOrEmpty(FiltroHasta.Text) ? (DateTime?)null : DateTime.Parse(FiltroHasta.Text);

            string texto = FiltroTexto.Text.Trim();

            FiltroTareaBusqueda filtroTareaBusqueda = new FiltroTareaBusqueda();
            filtroTareaBusqueda.EstadoId = idEstado;
            filtroTareaBusqueda.PrioridadId = idPrioridad;
            filtroTareaBusqueda.UsuarioAsignadoId = idAsignado;
            filtroTareaBusqueda.TagId = idTag;
            filtroTareaBusqueda.FechaDesde = desde;
            filtroTareaBusqueda.FechaHasta = hasta;
            filtroTareaBusqueda.Texto = texto;

            List<TareaListado> lista = tareaNegocio.ListarFiltrado(filtroTareaBusqueda);

            TablaTareas.DataSource = lista;
            TablaTareas.DataBind();

            MostrarMensajeSiNoHayTareas(lista);

            bool mostrarColumnaAsignado = rol == "USER";
            TablaTareas.Columns[3].Visible = mostrarColumnaAsignado;

            string rolAct = RolUsuario();
            bool esAdmin = rolAct == "ADMIN" || rolAct == "SUPERVISOR";

            foreach (GridViewRow row in TablaTareas.Rows)
            {
                Panel panelAdmin =
                    (Panel)row.FindControl("PanelAdmin");

                if (panelAdmin != null)
                    panelAdmin.Visible = esAdmin;
            }
        }

        private void MostrarMensajeSiNoHayTareas(List<TareaListado> tareas)
        {
            if (tareas == null || tareas.Count == 0)
            {
                TablaTareas.Visible = false;
                PanelSinTareas.Visible = true;
            }
            else
            {
                TablaTareas.Visible = true;
                PanelSinTareas.Visible = false;
            }
        }

        protected void ClickBotonFiltrar(object sender, EventArgs e)
        {
            CargarTareas();
        }

        protected void ClickBotonConfirmarEliminar(object sender, EventArgs e)
        {
            if (int.TryParse(IdTareaAEliminar.Value, out int id))
            {
                tareaNegocio.Eliminar(id);
                CargarTareas();
            }
        }
    }
}