using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages.Tareas
{
    public partial class Editar : Page
    {
        private readonly UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        private readonly EstadoNegocio estadoNegocio = new EstadoNegocio();
        private readonly PrioridadNegocio prioridadNegocio = new PrioridadNegocio();
        private readonly TagNegocio tagNegocio = new TagNegocio();
        private readonly TipoRelacionNegocio tipoRelacionNegocio = new TipoRelacionNegocio();
        private readonly TareaNegocio tareaNegocio = new TareaNegocio();

        private int UsuarioActualId()
        {
            Usuario u = (Usuario)Session["UsuarioActual"];
            return u.Id;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Listar.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarSelects();
                CargarTarea();
            }
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

        private void CargarTarea()
        {
            int id = int.Parse(Request.QueryString["id"]);
            Tarea t = tareaNegocio.BuscarPorId(id);

            TextoTitulo.Text = t.Titulo;
            TextoDescripcion.Text = t.Descripcion;
            SelectUsuarioAsignado.SelectedValue = t.UsuarioId.ToString();
            SelectPrioridad.SelectedValue = t.PrioridadId.ToString();
            TextoVencimiento.Text = t.FechaVencimiento.ToString("yyyy-MM-dd");

            if (t.HsEstimadas != null)
                TextoHorasEstimadas.Text = t.HsEstimadas.ToString("0.##");

            if (t.TipoRelacionId.HasValue)
                SelectTipoRelacion.SelectedValue = t.TipoRelacionId.Value.ToString();

            if (t.RelacionadoId.HasValue)
                SelectTareaRelacionada.SelectedValue = t.RelacionadoId.Value.ToString();

            List<Tag> tags = tagNegocio.BuscarPorTarea(t.Id);
            foreach (System.Web.UI.WebControls.ListItem item in SelectTags.Items)
                if (tags.Exists(x => x.Id.ToString() == item.Value))
                    item.Selected = true;
            
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            List<Imagen> imagenes = imagenNegocio.BuscarPorTarea(t.Id);

            RepeaterImagenesExistentes.DataSource = imagenes;
            RepeaterImagenesExistentes.DataBind();
        }
        
        protected void RepeaterImagenesExistentes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarImagen")
            {
                int idImagen = int.Parse(e.CommandArgument.ToString());

                ImagenNegocio imagenNegocio = new ImagenNegocio();
                Imagen img = imagenNegocio.BuscarPorId(idImagen);

                if (img != null)
                {
                    string rutaFisica = Server.MapPath(img.Path);
                    if (File.Exists(rutaFisica))
                        File.Delete(rutaFisica);

                    imagenNegocio.Eliminar(idImagen);
                }

                CargarTarea();
            }
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            ErrorEditar.Text = "";

            if (TextoTitulo.Text.Trim().Length == 0)
            {
                ErrorEditar.Text = "El título es obligatorio.";
                return;
            }

            if (TextoDescripcion.Text.Trim().Length == 0)
            {
                ErrorEditar.Text = "La descripción es obligatoria.";
                return;
            }

            if (string.IsNullOrEmpty(SelectUsuarioAsignado.SelectedValue))
            {
                ErrorEditar.Text = "Debe seleccionar un usuario asignado.";
                return;
            }


            if (string.IsNullOrEmpty(SelectPrioridad.SelectedValue))
            {
                ErrorEditar.Text = "Debe seleccionar una prioridad.";
                return;
            }

            if (string.IsNullOrEmpty(TextoVencimiento.Text))
            {
                ErrorEditar.Text = "Debe ingresar una fecha de vencimiento.";
                return;
            }

            decimal horasEstimadasDecimal = 0;
            decimal.TryParse(TextoHorasEstimadas.Text, out horasEstimadasDecimal);

            int id = int.Parse(Request.QueryString["id"]);
            Tarea t = tareaNegocio.BuscarPorId(id);

            t.Titulo = TextoTitulo.Text.Trim();
            t.Descripcion = TextoDescripcion.Text.Trim();
            t.UsuarioId = int.Parse(SelectUsuarioAsignado.SelectedValue);
            t.EstadoId = t.EstadoId;
            t.PrioridadId = int.Parse(SelectPrioridad.SelectedValue);
            t.HsEstimadas = horasEstimadasDecimal;
            t.FechaVencimiento = DateTime.Parse(TextoVencimiento.Text);

            if (!string.IsNullOrEmpty(SelectTipoRelacion.SelectedValue))
                t.TipoRelacionId = int.Parse(SelectTipoRelacion.SelectedValue);
            else
                t.TipoRelacionId = null;

            if (!string.IsNullOrEmpty(SelectTareaRelacionada.SelectedValue))
                t.RelacionadoId = int.Parse(SelectTareaRelacionada.SelectedValue);
            else
                t.RelacionadoId = null;

            tareaNegocio.Modificar(t);

            List<Tag> tagsActuales = tagNegocio.BuscarPorTarea(t.Id);
            List<int> tagsActualesIds = new List<int>();

            foreach (Tag tg in tagsActuales)
                tagsActualesIds.Add(tg.Id);

            List<int> tagsSeleccionados = new List<int>();
            foreach (System.Web.UI.WebControls.ListItem item in SelectTags.Items)
                if (item.Selected)
                    tagsSeleccionados.Add(int.Parse(item.Value));

            List<int> tagsAcrear = new List<int>();
            List<int> tagsAeliminar = new List<int>();

            foreach (int idTag in tagsSeleccionados)
                if (!tagsActualesIds.Contains(idTag))
                    tagsAcrear.Add(idTag);

            foreach (int idTag in tagsActualesIds)
                if (!tagsSeleccionados.Contains(idTag))
                    tagsAeliminar.Add(idTag);

            TareaTagNegocio tareaTagNegocio = new TareaTagNegocio();

            foreach (int idTag in tagsAeliminar)
                tareaTagNegocio.Eliminar(t.Id, idTag);

            foreach (int idTag in tagsAcrear)
            {
                TareaTag tt = new TareaTag();
                tt.TareaId = t.Id;
                tt.TagId = idTag;
                tareaTagNegocio.Agregar(tt);
            }

            ImagenNegocio imagenNegocio = new ImagenNegocio();

            if (InputImagenes.PostedFiles != null && InputImagenes.PostedFiles.Count > 0)
            {
                string carpeta = Server.MapPath("~/Uploads/Tareas/" + t.Id + "/");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                for (int i = 0; i < InputImagenes.PostedFiles.Count; i++)
                {
                    System.Web.HttpPostedFile file = InputImagenes.PostedFiles[i];
                    if (InputImagenes.PostedFiles[i].ContentType == "application/octet-stream") continue;

                    string extension = Path.GetExtension(file.FileName);
                    string nombre = Guid.NewGuid().ToString("N") + extension;
                    string rutaFisica = Path.Combine(carpeta, nombre);
                    string rutaPublica = "/Uploads/Tareas/" + t.Id + "/" + nombre;

                    file.SaveAs(rutaFisica);

                    Imagen img = new Imagen();
                    img.TareaId = t.Id;
                    img.Path = rutaPublica;
                    img.Nombre = nombre;
                    img.Size = file.ContentLength;
                    img.Mime = file.ContentType;

                    imagenNegocio.Agregar(img);
                }
            }

            Response.Redirect("Listar.aspx");
        }
    }
}
