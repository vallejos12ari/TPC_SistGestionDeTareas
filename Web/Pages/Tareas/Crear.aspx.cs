using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;

namespace Web.Pages.Tareas
{
    public partial class Crear : Page
    {
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private readonly EstadoNegocio estadoNegocio = new EstadoNegocio();
        private readonly PrioridadNegocio prioridadNegocio = new PrioridadNegocio();
        private readonly TagNegocio tagNegocio = new TagNegocio();
        private readonly TipoRelacionNegocio tipoRelacionNegocio = new TipoRelacionNegocio();
        private readonly TareaNegocio tareaNegocio = new TareaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarSelects();
        }

        private int UsuarioActualId()
        {
            Usuario u = (Usuario)Session["UsuarioActual"];
            return u.Id;
        }

        private void CargarSelects()
        {
            Usuario usuarioActual = (Usuario)Session["UsuarioActual"];
            UsuariosRelacionadosNegocio usuariosRelacionadosNegocio = new UsuariosRelacionadosNegocio();
            SelectUsuarioAsignado.Items.Clear();
            foreach (Usuario u in usuariosRelacionadosNegocio.ListarAsignados(UsuarioActualId()))
                SelectUsuarioAsignado.Items.Add(new System.Web.UI.WebControls.ListItem(u.Nombre, u.Id.ToString()));
            SelectUsuarioAsignado.Items.Add(new System.Web.UI.WebControls.ListItem(usuarioActual.Nombre, usuarioActual.Id.ToString()));
            
            SelectPrioridad.Items.Clear();
            foreach (Prioridad p in prioridadNegocio.Listar())
                SelectPrioridad.Items.Add(new System.Web.UI.WebControls.ListItem(p.Nombre, p.Id.ToString()));

            SelectTags.Items.Clear();
            foreach (Tag t in tagNegocio.Listar())
                SelectTags.Items.Add(new System.Web.UI.WebControls.ListItem(t.Nombre, t.Id.ToString()));

            SelectTipoRelacion.Items.Clear();
            SelectTipoRelacion.Items.Add(new System.Web.UI.WebControls.ListItem("Ninguna", ""));
            foreach (TipoRelacion tr in tipoRelacionNegocio.Listar())
                SelectTipoRelacion.Items.Add(new System.Web.UI.WebControls.ListItem(tr.Nombre, tr.Id.ToString()));

            SelectTareaRelacionada.Items.Clear();
            SelectTareaRelacionada.Items.Add(new System.Web.UI.WebControls.ListItem("Ninguna", ""));
            foreach (TareaListado t in tareaNegocio.ListarFiltrado(new FiltroTareaBusqueda()))
                SelectTareaRelacionada.Items.Add(new System.Web.UI.WebControls.ListItem(t.Titulo, t.Id.ToString()));
        }

        protected void ClickBotonCrear(object sender, EventArgs e)
        {
            ErrorCrear.Text = "";

            if (TextoTitulo.Text.Trim().Length == 0)
            {
                ErrorCrear.Text = "El título es obligatorio.";
                return;
            }

            if (TextoDescripcion.Text.Trim().Length == 0)
            {
                ErrorCrear.Text = "La descripción es obligatoria.";
                return;
            }

            if (string.IsNullOrEmpty(SelectUsuarioAsignado.SelectedValue))
            {
                ErrorCrear.Text = "Debe seleccionar un usuario asignado.";
                return;
            }

            if (string.IsNullOrEmpty(SelectPrioridad.SelectedValue))
            {
                ErrorCrear.Text = "Debe seleccionar una prioridad.";
                return;
            }

            if (string.IsNullOrEmpty(TextoVencimiento.Text))
            {
                ErrorCrear.Text = "Debe ingresar una fecha de vencimiento.";
                return;
            }

            decimal.TryParse(TextoHorasEstimadas.Text, out decimal horasEstimadas);

            List<int> tags = new List<int>();
            foreach (System.Web.UI.WebControls.ListItem item in SelectTags.Items)
            {
                if (item.Selected)
                    tags.Add(int.Parse(item.Value));
            }

            int usuarioAsignadoId = int.Parse(SelectUsuarioAsignado.SelectedValue);
            int estadoId = estadoNegocio.BuscarInicial().Id;
            int prioridadId = int.Parse(SelectPrioridad.SelectedValue);

            int? tipoRelacionId = null;
            if (!string.IsNullOrEmpty(SelectTipoRelacion.SelectedValue))
                tipoRelacionId = int.Parse(SelectTipoRelacion.SelectedValue);

            int? tareaRelacionadaId = null;
            if (!string.IsNullOrEmpty(SelectTareaRelacionada.SelectedValue))
                tareaRelacionadaId = int.Parse(SelectTareaRelacionada.SelectedValue);

            DateTime vencimiento = DateTime.Parse(TextoVencimiento.Text);

            Tarea t = new Tarea();
            t.Titulo = TextoTitulo.Text.Trim();
            t.Descripcion = TextoDescripcion.Text.Trim();
            t.UsuarioId = usuarioAsignadoId;
            t.CreadoPor = UsuarioActualId();
            t.HsEstimadas = horasEstimadas;
            t.EstadoId = estadoId;
            t.PrioridadId = prioridadId;
            t.TipoRelacionId = tipoRelacionId;
            t.RelacionadoId = tareaRelacionadaId;
            t.FechaVencimiento = vencimiento;

            tareaNegocio.Agregar(t, tags, InputImagenes.PostedFiles);
            Response.Redirect("Listar.aspx");
        }
    }
}