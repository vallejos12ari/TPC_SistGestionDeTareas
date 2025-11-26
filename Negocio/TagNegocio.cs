using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class TagNegocio
    {
        public List<Tag> Listar()
        {
            var lista = new List<Tag>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM tags WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var t = new Tag
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            Color = lector["color"].ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(t);
                    }
                }
            }

            return lista;
        }

        public void Agregar(Tag t)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"INSERT INTO tags (nombre, eliminado, color)
                                           VALUES (@nombre, @eliminado, @color)");

                datos.AgregarParametro("@nombre", t.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", t.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@color", t.Color, SqlDbType.VarChar);

                datos.EjecutarAccion();
            }
        }

        public void Modificar(Tag t)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"UPDATE tags SET 
                                            nombre=@nombre, 
                                            eliminado=@eliminado, 
                                            color=@color 
                                        WHERE id=@id");

                datos.AgregarParametro("@nombre", t.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", t.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@color", t.Color, SqlDbType.VarChar);
                datos.AgregarParametro("@id", t.Id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE tags SET eliminado = 1 WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public Tag BuscarPorId(int id)
        {
            Tag tag = null;

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT id, nombre, color FROM tags WHERE id = @id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                SqlDataReader lector = datos.EjecutarLectura();

                if (lector.Read())
                {
                    tag = new Tag();
                    tag.Id = (int)lector["id"];
                    tag.Nombre = (string)lector["nombre"];
                    tag.Color = (string)lector["color"];
                }
            }

            return tag;
        }

        public List<Tag> BuscarPorTarea(int tareaId)
        {
            List<Tag> tags = new List<Tag>();
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT id, nombre, color FROM tags WHERE eliminado = 0 AND id IN (SELECT tag_id FROM tareas_tags WHERE tarea_id = @id); ");
                datos.AgregarParametro("@id", tareaId, SqlDbType.Int);
                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        Tag t = new Tag
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Color = lector["color"].ToString(),
                        };

                        tags.Add(t);
                    }
                }
            }

            return tags;
        }
    }
}