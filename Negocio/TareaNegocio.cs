using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using Dominio;

namespace Negocio
{
    public class TareaNegocio
    {
        public List<Tarea> Listar()
        {
            var lista = new List<Tarea>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM tareas WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var aux = new Tarea
                        {
                            Id = (int)lector["id"],
                            Titulo = lector["titulo"].ToString(),
                            Descripcion = lector["descripcion"] != DBNull.Value ? lector["descripcion"].ToString() : null,
                            UsuarioId = (int)lector["usuario_id"],
                            CreadoPor = (int)lector["creado_por"],
                            HsEstimadas = (decimal)lector["hs_estimadas"],
                            EstadoId = (int)lector["estado_id"],
                            PrioridadId = (int)lector["prioridad_id"],
                            TipoRelacionId = lector["tipo_relacion_id"] != DBNull.Value ? (int?)lector["tipo_relacion_id"] : null,
                            RelacionadoId = lector["relacionado_id"] != DBNull.Value ? (int?)lector["relacionado_id"] : null,
                            FechaVencimiento = (DateTime)lector["fecha_vencimiento"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(aux);
                    }
                }
            }

            return lista;
        }
        
        public List<Tarea> ListarAsignables()
        {
            var lista = new List<Tarea>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM tareas t JOIN estados e ON e.id = t.estado_id AND e.es_final = 0 WHERE t.eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var aux = new Tarea
                        {
                            Id = (int)lector["id"],
                            Titulo = lector["titulo"].ToString(),
                            Descripcion = lector["descripcion"] != DBNull.Value ? lector["descripcion"].ToString() : null,
                            UsuarioId = (int)lector["usuario_id"],
                            CreadoPor = (int)lector["creado_por"],
                            HsEstimadas = (decimal)lector["hs_estimadas"],
                            EstadoId = (int)lector["estado_id"],
                            PrioridadId = (int)lector["prioridad_id"],
                            TipoRelacionId = lector["tipo_relacion_id"] != DBNull.Value ? (int?)lector["tipo_relacion_id"] : null,
                            RelacionadoId = lector["relacionado_id"] != DBNull.Value ? (int?)lector["relacionado_id"] : null,
                            FechaVencimiento = (DateTime)lector["fecha_vencimiento"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(aux);
                    }
                }
            }

            return lista;
        }

        public List<TareaListado> ListarFiltrado(FiltroTareaBusqueda f)
        {
            List<TareaListado> lista = new List<TareaListado>();

            using (AccesoDatos datos = new AccesoDatos())
            {
                string sql = @"
        SELECT  t.id,
                t.titulo,
                e.nombre AS estado_nombre,
                e.color AS estado_color,
                p.nombre AS prioridad_nombre,
                p.color AS prioridad_color,
                u.nombre AS usuario_asignado,
                t.fecha_vencimiento
        FROM tareas t
        INNER JOIN estados e ON e.id = t.estado_id
        INNER JOIN prioridades p ON p.id = t.prioridad_id
        LEFT JOIN usuarios u ON u.id = t.usuario_id
        WHERE t.eliminado = 0
        ";

                if (f.EstadoId.HasValue)
                    sql += " AND t.estado_id = @estado ";

                if (f.PrioridadId.HasValue)
                    sql += " AND t.prioridad_id = @prioridad ";

                if (f.UsuarioAsignadoId.HasValue)
                    sql += " AND t.usuario_id = @usuario ";

                if (!string.IsNullOrWhiteSpace(f.Texto))
                    sql += " AND (t.titulo LIKE '%' + @texto + '%' OR t.descripcion LIKE '%' + @texto + '%') ";

                if (f.FechaDesde.HasValue)
                    sql += " AND t.fecha_vencimiento >= @desde ";

                if (f.FechaHasta.HasValue)
                    sql += " AND t.fecha_vencimiento <= @hasta ";

                if (f.TagId.HasValue)
                {
                    sql += @" AND t.id IN (
                        SELECT tarea_id 
                        FROM tareas_tags
                        WHERE tag_id = @tag
                    )";
                }

                sql += " ORDER BY t.fecha_vencimiento ASC ";

                datos.DefinirConsulta(sql);

                if (f.EstadoId.HasValue)
                    datos.AgregarParametro("@estado", f.EstadoId.Value, SqlDbType.Int);

                if (f.PrioridadId.HasValue)
                    datos.AgregarParametro("@prioridad", f.PrioridadId.Value, SqlDbType.Int);

                if (f.UsuarioAsignadoId.HasValue)
                    datos.AgregarParametro("@usuario", f.UsuarioAsignadoId.Value, SqlDbType.Int);

                if (!string.IsNullOrWhiteSpace(f.Texto))
                    datos.AgregarParametro("@texto", f.Texto, SqlDbType.VarChar);

                if (f.FechaDesde.HasValue)
                    datos.AgregarParametro("@desde", f.FechaDesde.Value, SqlDbType.Date);

                if (f.FechaHasta.HasValue)
                    datos.AgregarParametro("@hasta", f.FechaHasta.Value, SqlDbType.Date);

                if (f.TagId.HasValue)
                    datos.AgregarParametro("@tag", f.TagId.Value, SqlDbType.Int);

                SqlDataReader lector = datos.EjecutarLectura();

                while (lector.Read())
                {
                    TareaListado t = new TareaListado();
                    t.Id = (int)lector["id"];
                    t.Titulo = lector["titulo"].ToString();
                    t.EstadoNombre = lector["estado_nombre"].ToString();
                    t.EstadoColor = lector["estado_color"].ToString();
                    t.PrioridadNombre = lector["prioridad_nombre"].ToString();
                    t.PrioridadColor = lector["prioridad_color"].ToString();
                    t.UsuarioAsignadoNombre = lector["usuario_asignado"] != DBNull.Value
                        ? lector["usuario_asignado"].ToString()
                        : "";

                    t.FechaVencimiento = (DateTime)lector["fecha_vencimiento"];

                    TagNegocio tagNegocio = new TagNegocio();
                    t.Tags = tagNegocio.BuscarPorTarea(t.Id);
                    lista.Add(t);
                }
            }

            return lista;
        }

        public Tarea BuscarPorId(int id)
        {
            Tarea tarea = null;

            using (AccesoDatos datos = new AccesoDatos())
            {
                string sql = @"
                   SELECT  id,
                           titulo,
                           descripcion,
                           usuario_id,
                           creado_por,
                           hs_estimadas,
                           estado_id,
                           prioridad_id,
                           tipo_relacion_id,
                           relacionado_id,
                           fecha_vencimiento,
                           eliminado
                   FROM tareas
                   WHERE id = @id
                   AND eliminado = 0
                ";

                datos.DefinirConsulta(sql);
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                SqlDataReader lector = datos.EjecutarLectura();

                if (lector.Read())
                {
                    tarea = new Tarea();

                    tarea.Id = (int)lector["id"];
                    tarea.Titulo = lector["titulo"].ToString();
                    tarea.Descripcion = lector["descripcion"] != DBNull.Value ? lector["descripcion"].ToString() : null;

                    tarea.UsuarioId = lector["usuario_id"] != DBNull.Value
                        ? Convert.ToInt32(lector["usuario_id"])
                        : 0;

                    tarea.CreadoPor = lector["creado_por"] != DBNull.Value ? (int)lector["creado_por"] : 0;

                    tarea.HsEstimadas = lector["hs_estimadas"] != DBNull.Value
                        ? Convert.ToDecimal(lector["hs_estimadas"])
                        : 0;

                    tarea.EstadoId = (int)lector["estado_id"];
                    tarea.PrioridadId = (int)lector["prioridad_id"];

                    tarea.TipoRelacionId = lector["tipo_relacion_id"] != DBNull.Value
                        ? (int?)lector["tipo_relacion_id"]
                        : null;

                    tarea.RelacionadoId = lector["relacionado_id"] != DBNull.Value
                        ? (int?)lector["relacionado_id"]
                        : null;
                    tarea.FechaVencimiento = (DateTime)lector["fecha_vencimiento"];
                }
            }

            return tarea;
        }

        public void Agregar(Tarea t, List<int> tagIds, IList<HttpPostedFile> imagenes)
        {
            int idTareaNueva;

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
            INSERT INTO tareas
                (titulo, descripcion, usuario_id, creado_por, hs_estimadas, estado_id, prioridad_id, tipo_relacion_id, relacionado_id, fecha_vencimiento)
            VALUES
                (@titulo, @desc, @uid, @creado, @hs, @estado, @prio, @tipo, @rel, @venci);

            SELECT SCOPE_IDENTITY();
        ");

                datos.AgregarParametro("@titulo", t.Titulo, SqlDbType.VarChar);
                datos.AgregarParametro("@desc", (object)t.Descripcion ?? DBNull.Value, SqlDbType.Text);
                datos.AgregarParametro("@uid", t.UsuarioId, SqlDbType.Int);
                datos.AgregarParametro("@creado", t.CreadoPor, SqlDbType.Int);
                datos.AgregarParametro("@hs", t.HsEstimadas, SqlDbType.Decimal);
                datos.AgregarParametro("@estado", t.EstadoId, SqlDbType.Int);
                datos.AgregarParametro("@prio", t.PrioridadId, SqlDbType.Int);
                datos.AgregarParametro("@tipo", (object)t.TipoRelacionId ?? DBNull.Value, SqlDbType.Int);
                datos.AgregarParametro("@rel", (object)t.RelacionadoId ?? DBNull.Value, SqlDbType.Int);
                datos.AgregarParametro("@venci", (object)t.FechaVencimiento ?? DBNull.Value, SqlDbType.DateTime);

                object resultado = datos.EjecutarScalar();
                idTareaNueva = Convert.ToInt32(resultado);
                t.Id = idTareaNueva;
            }

            TareaTagNegocio tareaTagNegocio = new TareaTagNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            foreach (int idTag in tagIds)
            {
                TareaTag tag = new TareaTag();
                tag.TagId = idTag;
                tag.TareaId = idTareaNueva;

                tareaTagNegocio.Agregar(tag);
            }
            string carpeta = HttpContext.Current.Server.MapPath("~/Uploads/Tareas/" + idTareaNueva + "/");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            for (int i = 0; i < imagenes.Count; i++)
            {
                var file = imagenes[i];

                string extension = Path.GetExtension(file.FileName);

                if (extension == "application/octet-stream" || extension == "") continue;

                string nombre = Guid.NewGuid().ToString("N") + extension;

                string rutaFisica = Path.Combine(carpeta, nombre);

                string rutaPublica = "/Uploads/Tareas/" + idTareaNueva + "/" + nombre;

                file.SaveAs(rutaFisica);

                Imagen imagen = new Imagen();
                imagen.TareaId = idTareaNueva;
                imagen.Path = rutaPublica; 
                imagen.Nombre = nombre;
                imagen.Size = file.ContentLength;
                imagen.Mime = file.ContentType;

                imagenNegocio.Agregar(imagen);
            }
            
            NotificarAsignacion(t.UsuarioId, t);

        }
        
        public void Modificar(Tarea t)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
            UPDATE tareas SET
                titulo = @titulo,
                descripcion = @desc,
                usuario_id = @uid,
                hs_estimadas = @hs,
                estado_id = @estado,
                prioridad_id = @prio,
                tipo_relacion_id = @tipo,
                relacionado_id = @rel,
                fecha_vencimiento = @venci
            WHERE id = @id
        ");

                datos.AgregarParametro("@titulo", t.Titulo, SqlDbType.VarChar);
                datos.AgregarParametro("@desc", (object)t.Descripcion ?? DBNull.Value, SqlDbType.Text);
                datos.AgregarParametro("@uid", t.UsuarioId, SqlDbType.Int);
                datos.AgregarParametro("@hs", t.HsEstimadas, SqlDbType.Decimal);
                datos.AgregarParametro("@estado", t.EstadoId, SqlDbType.Int);
                datos.AgregarParametro("@prio", t.PrioridadId, SqlDbType.Int);
                datos.AgregarParametro("@tipo", (object)t.TipoRelacionId ?? DBNull.Value, SqlDbType.Int);
                datos.AgregarParametro("@rel", (object)t.RelacionadoId ?? DBNull.Value, SqlDbType.Int);
                datos.AgregarParametro("@venci", (object)t.FechaVencimiento ?? DBNull.Value, SqlDbType.DateTime);
                datos.AgregarParametro("@id", t.Id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE tareas SET eliminado = 1 WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }

        public void NotificarAsignacion(int idUsuarioAsignado, Tarea tarea)
        {
            try
            {
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                Usuario usuarioAsignado = usuarioNegocio.BuscarPorId(idUsuarioAsignado);

                if (usuarioAsignado == null || string.IsNullOrEmpty(usuarioAsignado.Email))
                    throw new Exception("El usuario asignado no existe o no tiene email configurado.");

                Usuario usuarioCreador = usuarioNegocio.BuscarPorId(tarea.UsuarioId);

                string nombreAsignador = usuarioCreador != null
                    ? usuarioCreador.Nombre
                    : "ADMIN";

                EmailService mail = new EmailService();
                mail.ArmarCorreoAsignacion(
                    usuarioAsignado.Email,
                    tarea.Titulo,
                    nombreAsignador
                );

                mail.EnviarEmail();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar la notificación de asignación", ex);
            }
        }

        public void NotificarAsignacion(string emailDestino, string tituloTarea, string asignador)
        {
            try
            {
                EmailService mail = new EmailService();
                mail.ArmarCorreoAsignacion(emailDestino, tituloTarea, asignador);
                mail.EnviarEmail();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar la notificación de tarea.", ex);
            }
        }
    }
}