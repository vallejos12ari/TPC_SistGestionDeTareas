using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using Tarea = Dominio.Tarea;

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
            {
                CargarSelects();
                SetearFechaVencimientoHoy();
            }
        }

        private int UsuarioActualId()
        {
            Usuario u = (Usuario)Session["UsuarioActual"];
            return u.Id;
        }

        private void SetearFechaVencimientoHoy()
        {
            TextoVencimiento.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void CargarSelects()
        {
            CargarSelectUsuarioAsignado();
            CargarSelectPrioridades();
            CargarSelectTags();
            CargarSelectTipoRelacion();
            CargarSelectTareaRelacionada();
        }

        private void CargarSelectUsuarioAsignado()
        {
            Usuario usuarioActual = (Usuario)Session["UsuarioActual"];
            UsuariosRelacionadosNegocio usuariosRelacionadosNegocio = new UsuariosRelacionadosNegocio();

            SelectUsuarioAsignado.Items.Clear();

            foreach (Usuario u in usuariosRelacionadosNegocio.ListarAsignados(UsuarioActualId()))
            {
                SelectUsuarioAsignado.Items.Add(new System.Web.UI.WebControls.ListItem(u.Nombre, u.Id.ToString()));
            }

            SelectUsuarioAsignado.Items.Add(new System.Web.UI.WebControls.ListItem(usuarioActual.Nombre, usuarioActual.Id.ToString()));
        }

        private void CargarSelectPrioridades()
        {
            SelectPrioridad.Items.Clear();

            foreach (Prioridad p in prioridadNegocio.Listar())
            {
                SelectPrioridad.Items.Add(new System.Web.UI.WebControls.ListItem(p.Nombre, p.Id.ToString()));
            }
        }

        private void CargarSelectTags()
        {
            SelectTags.Items.Clear();

            foreach (Tag t in tagNegocio.Listar())
            {
                SelectTags.Items.Add(new System.Web.UI.WebControls.ListItem(t.Nombre, t.Id.ToString()));
            }
        }

        private void CargarSelectTipoRelacion()
        {
            SelectTipoRelacion.Items.Clear();
            SelectTipoRelacion.Items.Add(new System.Web.UI.WebControls.ListItem("Ninguna", ""));

            foreach (TipoRelacion tr in tipoRelacionNegocio.Listar())
            {
                SelectTipoRelacion.Items.Add(new System.Web.UI.WebControls.ListItem(tr.Nombre, tr.Id.ToString()));
            }
        }

        private void CargarSelectTareaRelacionada()
        {
            SelectTareaRelacionada.Items.Clear();
            SelectTareaRelacionada.Items.Add(new System.Web.UI.WebControls.ListItem("Ninguna", ""));

            foreach (Tarea t in tareaNegocio.ListarAsignables())
            {
                SelectTareaRelacionada.Items.Add(new System.Web.UI.WebControls.ListItem(t.Titulo, t.Id.ToString()));
            }
        }

        private bool ValidarCamposObligatorios()
        {
            Estado estadoInicial = estadoNegocio.BuscarInicial();
            if (estadoInicial == null)
            {
                return MostrarError("No hay estado inicial configurado.");
            }
            
            if (TextoTitulo.Text.Trim().Length < 3)
            {
                return MostrarError("El título debe tener al menos 3 caracteres.");
            }

            if (TextoTitulo.Text.Trim().Length > 100)
            {
                return MostrarError("El título no puede superar los 100 caracteres.");
            }
            
            if (TextoTitulo.Text.Trim().Contains("<") || TextoTitulo.Text.Trim().Contains(">"))
            {
                return MostrarError("El título contiene caracteres inválidos.");
            }

            if (TextoDescripcion.Text.Contains("<") || TextoDescripcion.Text.Contains(">"))
            {
                return MostrarError("La descripción contiene caracteres inválidos.");
            }

            if (TextoDescripcion.Text.Trim().Length < 5)
            {
                return MostrarError("La descripción debe tener al menos 5 caracteres.");
            }

            if (string.IsNullOrEmpty(SelectUsuarioAsignado.SelectedValue))
            {
                return MostrarError("Debe seleccionar un usuario asignado.");
            }

            if (string.IsNullOrEmpty(SelectPrioridad.SelectedValue))
            {
                return MostrarError("Debe seleccionar una prioridad.");
            }

            if (string.IsNullOrEmpty(TextoVencimiento.Text))
            {
                return MostrarError("Debe ingresar una fecha de vencimiento.");
            }

            DateTime fecha;
            if (!DateTime.TryParse(TextoVencimiento.Text, out fecha))
            {
                return MostrarError("La fecha de vencimiento no es válida.");
            }

            if (fecha.Date < DateTime.Today)
            {
                return MostrarError("La fecha de vencimiento no puede ser anterior a hoy.");
            }

            if (!string.IsNullOrEmpty(SelectTipoRelacion.SelectedValue) &&
                string.IsNullOrEmpty(SelectTareaRelacionada.SelectedValue))
            {
                return MostrarError("Si la tarea está relacionada con otra, debe indicar cuál.");
            }

            if (!ValidarHorasEstimadas())
            {
                return false;
            }

            if (!ValidarImagenes())
            {
                return false;
            }

            return true;
        }

        private bool MostrarError(string mensaje)
        {
            ErrorCrear.Text = mensaje;
            return false;
        }

        private bool ValidarHorasEstimadas()
        {
            if (string.IsNullOrEmpty(TextoHorasEstimadas.Text))
            {
                return true;
            }

            decimal horas;
            if (!decimal.TryParse(TextoHorasEstimadas.Text, out horas))
            {
                return MostrarError("Las horas estimadas deben ser un número válido.");
            }

            if (horas < 0)
            {
                return MostrarError("Las horas estimadas no pueden ser negativas.");
            }

            if (horas > 24)
            {
                return MostrarError("Las horas estimadas no pueden superar las 24 horas.");
            }

            return true;
        }

        private bool ValidarImagenes()
        {
            var files = InputImagenes.PostedFiles;

            if (files.Count == 0)
            {
                return true;
            }

            if (files.Count > 10)
            {
                return MostrarError("No se pueden subir más de 10 imágenes.");
            }

            foreach (var file in files)
            {
                if (file.ContentType == "application/octet-stream") continue;
                string ext = Path.GetExtension(file.FileName).ToLower();

                if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".webp")
                {
                    return MostrarError("Solo se permiten imágenes JPG, JPEG, PNG o WEBP.");
                }

                if (file.ContentLength > 5 * 1024 * 1024)
                {
                    return MostrarError("Cada imagen debe pesar menos de 5 MB.");
                }

                if (!file.ContentType.StartsWith("image/"))
                {
                    return MostrarError("Uno de los archivos seleccionados no es una imagen válida.");
                }
            }

            return true;
        }

        private decimal ObtenerHorasEstimadas()
        {
            decimal.TryParse(TextoHorasEstimadas.Text, out decimal horas);
            return horas;
        }

        private List<int> ObtenerTagsSeleccionados()
        {
            List<int> tags = new List<int>();

            foreach (System.Web.UI.WebControls.ListItem item in SelectTags.Items)
            {
                if (item.Selected)
                {
                    tags.Add(int.Parse(item.Value));
                }
            }

            return tags;
        }

        private int? ObtenerIdTipoRelacion()
        {
            if (string.IsNullOrEmpty(SelectTipoRelacion.SelectedValue))
            {
                return null;
            }

            return int.Parse(SelectTipoRelacion.SelectedValue);
        }

        private int? ObtenerIdTareaRelacionada()
        {
            if (string.IsNullOrEmpty(SelectTareaRelacionada.SelectedValue))
            {
                return null;
            }

            return int.Parse(SelectTareaRelacionada.SelectedValue);
        }

        private Tarea ConstruirTarea(decimal horasEstimadas)
        {
            Tarea t = new Tarea();
            t.Titulo = TextoTitulo.Text.Trim();
            t.Descripcion = TextoDescripcion.Text.Trim();
            t.UsuarioId = int.Parse(SelectUsuarioAsignado.SelectedValue);
            t.CreadoPor = UsuarioActualId();
            t.HsEstimadas = horasEstimadas;
            t.EstadoId = estadoNegocio.BuscarInicial().Id;
            t.PrioridadId = int.Parse(SelectPrioridad.SelectedValue);
            t.TipoRelacionId = ObtenerIdTipoRelacion();
            t.RelacionadoId = ObtenerIdTareaRelacionada();
            t.FechaVencimiento = DateTime.Parse(TextoVencimiento.Text);
            return t;
        }

        protected void ClickBotonCrear(object sender, EventArgs e)
        {
            ErrorCrear.Text = "";

            if (!ValidarCamposObligatorios())
            {
                return;
            }

            decimal horasEstimadas = ObtenerHorasEstimadas();
            List<int> tags = ObtenerTagsSeleccionados();
            Tarea nuevaTarea = ConstruirTarea(horasEstimadas);

            tareaNegocio.Agregar(nuevaTarea, tags, InputImagenes.PostedFiles);

            Response.Redirect("Listar.aspx");
        }
    }
}