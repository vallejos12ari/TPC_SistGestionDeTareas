using System;
using System.Collections.Generic;
using System.Data;
using Dominio;

namespace Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> Listar()
        {
            var lista = new List<Imagen>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM imagenes WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var img = new Imagen
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            Nombre = lector["nombre"].ToString(),
                            Mime = lector["mime"]?.ToString(),
                            Size = lector["size"] != DBNull.Value ? (int?)lector["size"] : null,
                            Path = lector["path"].ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(img);
                    }
                }
            }

            return lista;
        }
        
        public Imagen BuscarPorId(int id)
        {
            Imagen img = null;

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM imagenes WHERE id = @id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                using (System.Data.SqlClient.SqlDataReader lector = datos.EjecutarLectura())
                {
                    if (lector.Read())
                    {
                        img = new Imagen
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            Nombre = lector["nombre"].ToString(),
                            Mime = lector["mime"] != DBNull.Value ? lector["mime"].ToString() : null,
                            Size = lector["size"] != DBNull.Value ? (int?)lector["size"] : null,
                            Path = lector["path"].ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };
                    }
                }
            }

            return img;
        }
        
        public List<Imagen> BuscarPorTarea(int tareaId)
        {
            var lista = new List<Imagen>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
                    SELECT *
                    FROM imagenes
                    WHERE eliminado = 0
                      AND tarea_id = @tarea
                ");

                datos.AgregarParametro("@tarea", tareaId, SqlDbType.Int);

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var img = new Imagen
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            Nombre = lector["nombre"].ToString(),
                            Mime = lector["mime"]?.ToString(),
                            Size = lector["size"] != DBNull.Value ? (int?)lector["size"] : null,
                            Path = lector["path"].ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(img);
                    }
                }
            }

            return lista;
        }

        public void Agregar(Imagen img)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
                    INSERT INTO imagenes (tarea_id, nombre, mime, size, path)
                    VALUES (@tarea, @nombre, @mime, @size, @path)");

                datos.AgregarParametro("@tarea", img.TareaId, SqlDbType.Int);
                datos.AgregarParametro("@nombre", img.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@mime", (object)img.Mime ?? DBNull.Value, SqlDbType.VarChar);
                datos.AgregarParametro("@size", (object)img.Size ?? DBNull.Value, SqlDbType.Int);
                datos.AgregarParametro("@path", img.Path, SqlDbType.VarChar);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("DELETE FROM imagenes WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }
    }
}
