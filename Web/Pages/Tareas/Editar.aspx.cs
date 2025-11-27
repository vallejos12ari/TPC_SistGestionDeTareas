using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
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
        private readonly HistorialNegocio historialNegocio = new HistorialNegocio();

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
            CargarSelectUsuarioAsignado();
            CargarSelectPrioridades();
            CargarSelectTags();
            CargarSelectTipoRelacion();
            CargarSelectTareaRelacionada();
        }

        private void CargarSelectUsuarioAsignado()
        {
            Usuario usuarioActual = (Usuario)Session["UsuarioActual"];
            UsuariosRelacionadosNegocio rel = new UsuariosRelacionadosNegocio();

            SelectUsuarioAsignado.Items.Clear();

            foreach (Usuario u in rel.ListarAsignados(UsuarioActualId()))
            {
                SelectUsuarioAsignado.Items.Add(new ListItem(u.Nombre, u.Id.ToString()));
            }

            SelectUsuarioAsignado.Items.Add(new ListItem(usuarioActual.Nombre, usuarioActual.Id.ToString()));
        }

        private void CargarSelectPrioridades()
        {
            SelectPrioridad.Items.Clear();

            foreach (Prioridad p in prioridadNegocio.Listar())
            {
                SelectPrioridad.Items.Add(new ListItem(p.Nombre, p.Id.ToString()));
            }
        }

        private void CargarSelectTags()
        {
            SelectTags.Items.Clear();

            foreach (Tag t in tagNegocio.Listar())
            {
                SelectTags.Items.Add(new ListItem(t.Nombre, t.Id.ToString()));
            }
        }

        private void CargarSelectTipoRelacion()
        {
            SelectTipoRelacion.Items.Clear();
            SelectTipoRelacion.Items.Add(new ListItem("Ninguna", ""));

            foreach (TipoRelacion tr in tipoRelacionNegocio.Listar())
            {
                SelectTipoRelacion.Items.Add(new ListItem(tr.Nombre, tr.Id.ToString()));
            }
        }

        private void CargarSelectTareaRelacionada()
        {
            SelectTareaRelacionada.Items.Clear();
            SelectTareaRelacionada.Items.Add(new ListItem("Ninguna", ""));

            int idActual = int.Parse(Request.QueryString["id"]);

            foreach (Tarea t in tareaNegocio.ListarAsignables())
            {
                if (t.Id != idActual)
                {
                    SelectTareaRelacionada.Items.Add(new ListItem(t.Titulo, t.Id.ToString()));
                }
            }
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
            {
                TextoHorasEstimadas.Text = t.HsEstimadas.ToString("0.##");
            }

            if (t.TipoRelacionId.HasValue)
            {
                SelectTipoRelacion.SelectedValue = t.TipoRelacionId.ToString();
            }

            if (t.RelacionadoId.HasValue)
            {
                SelectTareaRelacionada.SelectedValue = t.RelacionadoId.ToString();
            }

            List<Tag> tags = tagNegocio.BuscarPorTarea(t.Id);

            foreach (ListItem item in SelectTags.Items)
            {
                if (tags.Exists(x => x.Id.ToString() == item.Value))
                {
                    item.Selected = true;
                }
            }

            ImagenNegocio imgNeg = new ImagenNegocio();
            RepeaterImagenesExistentes.DataSource = imgNeg.BuscarPorTarea(t.Id);
            RepeaterImagenesExistentes.DataBind();
            
            Estado estadoActual = estadoNegocio.BuscarPorId(t.EstadoId);

            if (estadoActual.EsFinal == 1)
            {
                TextoTitulo.Enabled = false;
                TextoDescripcion.Enabled = false;
                SelectUsuarioAsignado.Enabled = false;
                SelectPrioridad.Enabled = false;
                SelectTipoRelacion.Enabled = false;
                SelectTareaRelacionada.Enabled = false;
                TextoVencimiento.Enabled = false;
                TextoHorasEstimadas.Enabled = false;
            }
        }

        private bool ValidarCampos()
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

            if (TextoTitulo.Text.Contains("<") || TextoTitulo.Text.Contains(">"))
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

            HashSet<string> nombres = new HashSet<string>();

            foreach (var file in files)
            {
                if (file.ContentType == "application/octet-stream")
                {
                    continue;
                }

                string nombre = file.FileName.ToLower();

                if (!nombres.Add(nombre))
                {
                    return MostrarError("Hay imágenes duplicadas en la selección.");
                }

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
                    return MostrarError("Uno de los archivos no es una imagen válida.");
                }
            }

            return true;
        }

        private bool MostrarError(string mensaje)
        {
            ErrorEditar.Text = mensaje;
            return false;
        }

        private decimal ObtenerHorasEstimadas()
        {
            decimal.TryParse(TextoHorasEstimadas.Text, out decimal horas);
            return horas;
        }

        private List<int> ObtenerTagsSeleccionados()
        {
            List<int> tags = new List<int>();

            foreach (ListItem item in SelectTags.Items)
            {
                if (item.Selected)
                {
                    tags.Add(int.Parse(item.Value));
                }
            }

            return tags;
        }

        private void ActualizarTags(Tarea t)
        {
            List<Tag> actuales = tagNegocio.BuscarPorTarea(t.Id);
            List<int> idsActuales = new List<int>();

            foreach (Tag tg in actuales)
            {
                idsActuales.Add(tg.Id);
            }

            List<int> seleccionados = ObtenerTagsSeleccionados();

            List<int> crear = new List<int>();
            List<int> eliminar = new List<int>();

            foreach (int id in seleccionados)
            {
                if (!idsActuales.Contains(id))
                {
                    crear.Add(id);
                }
            }

            foreach (int id in idsActuales)
            {
                if (!seleccionados.Contains(id))
                {
                    eliminar.Add(id);
                }
            }

            TareaTagNegocio n = new TareaTagNegocio();

            foreach (int id in eliminar)
            {
                n.Eliminar(t.Id, id);
            }

            foreach (int id in crear)
            {
                TareaTag tt = new TareaTag();
                tt.TareaId = t.Id;
                tt.TagId = id;
                n.Agregar(tt);
            }
        }

        private void GuardarImagenes(Tarea t)
        {
            if (InputImagenes.PostedFiles == null || InputImagenes.PostedFiles.Count == 0)
            {
                return;
            }

            ImagenNegocio imgNeg = new ImagenNegocio();
            string carpeta = Server.MapPath("~/Uploads/Tareas/" + t.Id + "/");

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            foreach (var file in InputImagenes.PostedFiles)
            {
                if (file.ContentType == "application/octet-stream")
                {
                    continue;
                }

                string ext = Path.GetExtension(file.FileName);
                string nombre = Guid.NewGuid().ToString("N") + ext;
                string rutaFisica = Path.Combine(carpeta, nombre);
                string rutaPublica = "/Uploads/Tareas/" + t.Id + "/" + nombre;

                file.SaveAs(rutaFisica);

                Imagen img = new Imagen();
                img.TareaId = t.Id;
                img.Path = rutaPublica;
                img.Nombre = nombre;
                img.Size = file.ContentLength;
                img.Mime = file.ContentType;

                imgNeg.Agregar(img);
            }
        }

        protected void RepeaterImagenesExistentes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarImagen")
            {
                int idImagen = int.Parse(e.CommandArgument.ToString());

                ImagenNegocio imgNeg = new ImagenNegocio();
                Imagen img = imgNeg.BuscarPorId(idImagen);

                if (img != null)
                {
                    string rutaFisica = Server.MapPath(img.Path);

                    if (File.Exists(rutaFisica))
                    {
                        File.Delete(rutaFisica);
                    }

                    imgNeg.Eliminar(idImagen);
                }

                CargarTarea();
            }
        }

        protected void ClickBotonGuardar(object sender, EventArgs e)
        {
            ErrorEditar.Text = "";

            if (!ValidarCampos())
            {
                return;
            }

            int id = int.Parse(Request.QueryString["id"]);
            Tarea t = tareaNegocio.BuscarPorId(id);

            bool conHistorial = false;
            Historial historial = new Historial();

            int nuevoAsignado = int.Parse(SelectUsuarioAsignado.SelectedValue);

            if (t.UsuarioId != nuevoAsignado)
            {
                historial.UsuarioId = UsuarioActualId();
                historial.ValorAnterior = "Asignado a " + usuarioNegocio.BuscarPorId(t.UsuarioId).Nombre;
                historial.ValorNuevo = "Asignado a " + usuarioNegocio.BuscarPorId(nuevoAsignado).Nombre;
                conHistorial = true;
            }

            t.Titulo = TextoTitulo.Text.Trim();
            t.Descripcion = TextoDescripcion.Text.Trim();
            t.UsuarioId = nuevoAsignado;
            t.PrioridadId = int.Parse(SelectPrioridad.SelectedValue);
            t.HsEstimadas = ObtenerHorasEstimadas();
            t.FechaVencimiento = DateTime.Parse(TextoVencimiento.Text);

            if (!string.IsNullOrEmpty(SelectTipoRelacion.SelectedValue))
            {
                t.TipoRelacionId = int.Parse(SelectTipoRelacion.SelectedValue);
            }
            else
            {
                t.TipoRelacionId = null;
            }

            if (!string.IsNullOrEmpty(SelectTareaRelacionada.SelectedValue))
            {
                t.RelacionadoId = int.Parse(SelectTareaRelacionada.SelectedValue);
            }
            else
            {
                t.RelacionadoId = null;
            }

            tareaNegocio.Modificar(t);

            if (conHistorial)
            {
                historialNegocio.Agregar(historial);
            }

            ActualizarTags(t);
            GuardarImagenes(t);

            Response.Redirect("Listar.aspx");
        }
    }
}
