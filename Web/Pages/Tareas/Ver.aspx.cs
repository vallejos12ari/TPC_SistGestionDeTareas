using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Web.Pages.Tareas
{
    public partial class Ver : System.Web.UI.Page
    {
        private readonly TareaNegocio tareaNegocio = new TareaNegocio();
        private readonly ComentarioNegocio comentarioNegocio = new ComentarioNegocio();
        private Tarea tarea = new Tarea();
        private readonly HoraNegocio horaNegocio = new HoraNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("Listar.aspx");
                return;
            }

            RepeaterComentarios.ItemCommand += OnItemCommandComentario;
            RepeaterComentarios.ItemDataBound += OnItemDataBoundComentario;

            if (!IsPostBack)
            {
                CargarTarea();
                CargarComentarios();
                CargarHistorial();
            }
        }

        private string RolUsuario()
        {
            Usuario u = (Usuario)Session["UsuarioActual"];
            return u?.Rol ?? "";
        }

        private int? UsuarioIdActual()
        {
            Usuario u = (Usuario)Session["UsuarioActual"];
            return u?.Id;
        }

        private void CargarTarea()
        {
            int id = int.Parse(Request.QueryString["id"]);
            Tarea t = tareaNegocio.BuscarPorId(id);
            this.tarea = t;
            CargarEstadosPosibles(t.EstadoId);
            EstadoNegocio estadoNegocio = new EstadoNegocio();
            PrioridadNegocio prioridadNegocio = new PrioridadNegocio();
            TagNegocio tagNegocio = new TagNegocio();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            TipoRelacionNegocio tipoRelacionNegocio = new TipoRelacionNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            List<Tag> tags = tagNegocio.BuscarPorTarea(t.Id);
            Estado estado = estadoNegocio.BuscarPorId(t.EstadoId);
            Prioridad prioridad = prioridadNegocio.BuscarPorId(t.PrioridadId);
            Usuario usuario = usuarioNegocio.BuscarPorId(t.UsuarioId);
            List<Imagen> imagenes = imagenNegocio.BuscarPorTarea(t.Id);

            PanelCambiarEstado.Visible = t.UsuarioId == UsuarioIdActual() || t.CreadoPor == UsuarioIdActual();
            
            if (estado.EsFinal == 1)
            {
                PanelCambiarEstado.Visible = false;
            }
            
            if (t.TipoRelacionId.HasValue)
            {
                TipoRelacion tipoRelacion = tipoRelacionNegocio.BuscarPorId(t.TipoRelacionId.Value);
                Tarea tareaRelacionada = tareaNegocio.BuscarPorId(t.RelacionadoId ?? 0);

                LblTipoRelacion.Text = tipoRelacion.Nombre;

                if (tareaRelacionada != null)
                {
                    linkTareaRelacionada.InnerText = tareaRelacionada.Titulo;
                    linkTareaRelacionada.HRef = "/Pages/Tareas/Ver?id=" + tareaRelacionada.Id;
                }
                else
                {
                    linkTareaRelacionada.InnerText = "No encontrada";
                    linkTareaRelacionada.Attributes["class"] = "text-muted";
                }
            }
            else
            {
                LblTipoRelacion.Text = "Ninguna";
                linkTareaRelacionada.InnerText = "Sin relación";
                linkTareaRelacionada.Attributes["class"] = "text-muted";
            }

            LblTitulo.InnerText = t.Titulo;
            LblDescripcion.Text = t.Descripcion;

            LblUsuarioCreador.Text = usuarioNegocio.BuscarPorId(t.CreadoPor).Nombre;

            LblEstado.InnerHtml = "<span style='color:" + estado.Color + "'>" + estado.Nombre + "</span>";
            LblPrioridad.InnerHtml = "<span style='color:" + prioridad.Color + "'>" + prioridad.Nombre + "</span>";

            LblUsuarioAsignado.Text = usuario.Nombre;
            LblVencimiento.Text = t.FechaVencimiento.ToString("dd/MM/yyyy");

            if (t.HsEstimadas != null)
            {
                LblHoras.Text = t.HsEstimadas.ToString("0.##");
            }
            else
            {
                LblHoras.Text = "-";
            }

            RepeaterTags.DataSource = tags;
            RepeaterTags.DataBind();

            RepeaterImagenes.DataSource = imagenes;
            RepeaterImagenes.DataBind();

            string rol = RolUsuario();
            bool esAdmin = rol == "ADMIN" || rol == "SUPERVISOR";
            bool esSupervisorYCreador = rol == "SUPERVISOR" && t.CreadoPor != UsuarioIdActual();

            PanelAccionesAdmin.Visible = esAdmin && estado.EsFinal == 0;

            if (esAdmin || esSupervisorYCreador)
            {
                BotonEditar.HRef = "Editar.aspx?id=" + t.Id;
            }

            int? usuarioIdActual = UsuarioIdActual();
            bool esAsignado = usuarioIdActual.HasValue && usuarioIdActual.Value == usuario.Id;

            PanelAgregarHoras.Visible = esAsignado && estado.EsFinal == 0;

            HoraNegocio horaNegocio = new HoraNegocio();
            List<Hora> horas = horaNegocio.ListarPorTarea(t.Id);

            decimal totalHoras = 0;
            foreach (Hora h in horas)
            {
                totalHoras += h.Horas;
            }

            if (totalHoras > 0)
            {
                LblHorasCargadas.InnerText = totalHoras.ToString("0.##") + " hs";
                LblHorasCargadas.Visible = true;
                LblHorasCargadas.Attributes["onclick"] = "AbrirModalHorasCargadas()";
                LblHorasCargadas.Attributes["class"] = "fw-bold text-decoration-none";
            }
            else
            {
                LblHorasCargadas.InnerText = "0 hs";
                LblHorasCargadas.Attributes["class"] = "text-muted";
                LblHorasCargadas.Attributes["onclick"] = "";
            }

            List<object> listaHoras = new List<object>();

            foreach (Hora h in horas)
            {
                Usuario u = usuarioNegocio.BuscarPorId(h.UsuarioId);

                listaHoras.Add(new
                {
                    UsuarioNombre = u != null ? u.Nombre : "Usuario eliminado",
                    Horas = h.Horas,
                    Dia = h.Dia
                });
            }

            RepeaterHorasCargadas.DataSource = listaHoras;
            RepeaterHorasCargadas.DataBind();
        }

        private void CargarEstadosPosibles(int estadoActualId)
        {
            EstadoNegocio estadoNegocio = new EstadoNegocio();

            var siguientes = estadoNegocio.ObtenerSiguienteEstado(estadoActualId);

            SelectNuevoEstado.Items.Clear();


            if (siguientes.Count == 0)
            {
                SelectNuevoEstado.Enabled = false;
                SelectNuevoEstado.Items.Add(new System.Web.UI.WebControls.ListItem("No hay estados disponibles", ""));
                BotonConfirmarCambioEstado.Enabled = false;
            }
            else
            {
                foreach (var est in siguientes)
                    SelectNuevoEstado.Items.Add(new ListItem(est.Nombre, est.Id.ToString()));
            }
        }

        protected void ClickCambiarEstado(object sender, EventArgs e)
        {
            ErrorCambioEstado.Text = "";

            if (string.IsNullOrEmpty(SelectNuevoEstado.SelectedValue))
            {
                ErrorCambioEstado.Text = "Debe seleccionar un estado.";
                return;
            }

            int idTarea = int.Parse(Request.QueryString["id"]);
            int nuevoEstadoId = int.Parse(SelectNuevoEstado.SelectedValue);

            EstadoNegocio estadoNegocio = new EstadoNegocio();
            
            Tarea t = tareaNegocio.BuscarPorId(idTarea);


            HistorialNegocio historialNegocio = new HistorialNegocio();
            Historial historial = new Historial();
            string estadoAnterior = "Estado " + (estadoNegocio.BuscarPorId(t.EstadoId)).Nombre;
            string estadoNuevo = "Estado " + (estadoNegocio.BuscarPorId(nuevoEstadoId)).Nombre;
            t.EstadoId = nuevoEstadoId;
            tareaNegocio.Modificar(t);

            if (estadoAnterior != estadoNuevo)
            {
                historial.UsuarioId = UsuarioIdActual() ?? 0;
                historial.TareaId = t.Id;
                historial.ValorAnterior = estadoAnterior;
                historial.ValorNuevo = estadoNuevo;
                historialNegocio.Agregar(historial);
                t.EstadoId = nuevoEstadoId;
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void ClickAgregarHoras(object sender, EventArgs e)
        {
            string texto = InputHoras.Text.Trim();

            if (texto == "")
            {
                ErrorHoras.Text = "Debés ingresar un valor numérico.";
                return;
            }

            decimal valor;
            bool esNumero = decimal.TryParse(texto, out valor);

            if (!esNumero)
            {
                ErrorHoras.Text = "El valor ingresado no es un número válido.";
                return;
            }

            if (valor <= 0)
            {
                ErrorHoras.Text = "Las horas deben ser mayores a cero.";
                return;
            }

            int? usuarioId = UsuarioIdActual();

            if (usuarioId == null)
            {
                ErrorHoras.Text = "No se pudo identificar el usuario.";
                return;
            }

            Hora h = new Hora();
            h.TareaId = int.Parse(Request.QueryString["id"]);
            h.UsuarioId = usuarioId.Value;
            h.Horas = valor;
            h.Dia = DateTime.Now.Date;
            h.Eliminado = 0;
            h.FechaCreacion = DateTime.Now;

            horaNegocio.Agregar(h);

            InputHoras.Text = "";
            ErrorHoras.Text = "";
            CargarTarea();
        }

        private void CargarComentarios()
        {
            int id = int.Parse(Request.QueryString["id"]);
            List<Comentario> comentarios = comentarioNegocio.BuscarPorTarea(id);
            List<object> lista = new List<object>();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            foreach (Comentario c in comentarios)
            {
                Usuario u = usuarioNegocio.BuscarPorId(c.UsuarioId);

                lista.Add(new
                {
                    Id = c.Id,
                    Texto = c.Texto,
                    FechaCreacion = c.FechaCreacion,
                    UsuarioNombre = u != null ? u.Nombre : "Usuario eliminado"
                });
            }

            RepeaterComentarios.DataSource = lista;
            RepeaterComentarios.DataBind();
        }

        protected void ClickAgregarComentario(object sender, EventArgs e)
        {
            int? usuarioId = UsuarioIdActual();

            if (usuarioId == null)
            {
                ErrorComentarios.Text = "No se pudo identificar el usuario.";
                return;
            }

            string texto = TextoComentario.Text.Trim();

            if (texto == "")
            {
                ErrorComentarios.Text = "El comentario no puede estar vacío.";
                return;
            }

            Comentario c = new Comentario();
            c.TareaId = int.Parse(Request.QueryString["id"]);
            c.UsuarioId = usuarioId.Value;
            c.Texto = texto;
            c.Eliminado = 0;
            c.FechaCreacion = DateTime.Now;

            comentarioNegocio.Agregar(c);

            TextoComentario.Text = "";

            CargarComentarios();
        }

        protected void OnItemCommandComentario(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "EliminarComentario")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                comentarioNegocio.Eliminar(id);
                CargarComentarios();
            }
        }

        protected void OnItemDataBoundComentario(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item &&
                e.Item.ItemType != ListItemType.AlternatingItem)
                return;

            int? usuarioActualId = UsuarioIdActual();
            if (usuarioActualId == null)
                return;

            object data = e.Item.DataItem;
            int idComentario = (int)data.GetType().GetProperty("Id").GetValue(data);

            Comentario comentario = comentarioNegocio.BuscarPorId(idComentario);

            LinkButton boton = (LinkButton)e.Item.FindControl("BotonEliminarComentario");

            if (boton != null)
            {
                if (comentario == null || comentario.UsuarioId != usuarioActualId)
                    boton.Visible = false;
            }
        }

        protected void ClickEliminarComentarioConfirmado(object sender, EventArgs e)
        {
            string valor = HiddenComentarioId.Value;
            if (string.IsNullOrEmpty(valor))
                return;

            int id = int.Parse(valor);

            Comentario comentario = comentarioNegocio.BuscarPorId(id);
            int? usuarioActualId = UsuarioIdActual();

            if (comentario != null && usuarioActualId != null)
            {
                if (comentario.UsuarioId == usuarioActualId.Value)
                {
                    comentarioNegocio.Eliminar(id);
                }
            }

            CargarComentarios();
        }

        private void CargarHistorial()
        {
            int id = int.Parse(Request.QueryString["id"]);
            HistorialNegocio historialNegocio = new HistorialNegocio();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            var lista = new List<object>();

            foreach (Historial h in historialNegocio.ListarPorTarea(id))
            {
                Usuario u = usuarioNegocio.BuscarPorId(h.UsuarioId);

                lista.Add(new
                {
                    UsuarioNombre = u != null ? u.Nombre : "Usuario eliminado",
                    ValorAnterior = h.ValorAnterior,
                    ValorNuevo = h.ValorNuevo,
                    Fecha = h.Fecha
                });
            }

            RepeaterHistorial.DataSource = lista;
            RepeaterHistorial.DataBind();
        }


        protected void ClickEliminar(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            tareaNegocio.Eliminar(id);
            Response.Redirect("Listar.aspx");
        }
    }
}