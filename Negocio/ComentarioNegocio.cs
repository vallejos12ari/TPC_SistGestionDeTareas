using System;
using System.Collections.Generic;
using System.Data;
using Dominio;

namespace Negocio
{
    public class ComentarioNegocio
    {
        public List<Comentario> Listar()
        {
            var lista = new List<Comentario>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM comentarios WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var c = new Comentario
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            UsuarioId = (int)lector["usuario_id"],
                            Texto = lector["texto"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(c);
                    }
                }
            }

            return lista;
        }

        public List<Comentario> BuscarPorTarea(int tareaId)
        {
            var lista = new List<Comentario>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM comentarios WHERE eliminado = 0 AND tarea_id = @tareaId");
                datos.AgregarParametro("@tareaId", tareaId, SqlDbType.Int);

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var c = new Comentario
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            UsuarioId = (int)lector["usuario_id"],
                            Texto = lector["texto"].ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(c);
                    }
                }
            }

            return lista;
        }

        public Comentario BuscarPorId(int id)
        {
            Comentario comentario = null;

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM comentarios WHERE id = @id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                using (System.Data.SqlClient.SqlDataReader lector = datos.EjecutarLectura())
                {
                    if (lector.Read())
                    {
                        Comentario c = new Comentario();
                        c.Id = (int)lector["id"];
                        c.TareaId = (int)lector["tarea_id"];
                        c.UsuarioId = (int)lector["usuario_id"];
                        c.Texto = lector["texto"].ToString();
                        c.Eliminado = (byte)lector["eliminado"];
                        c.FechaCreacion = (DateTime)lector["fecha_creacion"];
                        comentario = c;
                    }
                }
            }

            return comentario;
        }

        public void Agregar(Comentario c)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
                    INSERT INTO comentarios (tarea_id, usuario_id, texto, eliminado)
                    VALUES (@tarea, @usuario, @texto, @eliminado)");

                datos.AgregarParametro("@tarea", c.TareaId, SqlDbType.Int);
                datos.AgregarParametro("@usuario", c.UsuarioId, SqlDbType.Int);
                datos.AgregarParametro("@texto", c.Texto, SqlDbType.Text);
                datos.AgregarParametro("@eliminado", c.Eliminado, SqlDbType.TinyInt);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE comentarios SET eliminado = 1 WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }
    }
}