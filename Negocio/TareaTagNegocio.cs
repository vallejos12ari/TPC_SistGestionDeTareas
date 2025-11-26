using System;
using System.Collections.Generic;
using System.Data;
using Dominio;

namespace Negocio
{
    public class TareaTagNegocio
    {
        public List<TareaTag> Listar()
        {
            var lista = new List<TareaTag>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM tareas_tags WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var tt = new TareaTag
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            TagId = (int)lector["tag_id"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(tt);
                    }
                }
            }

            return lista;
        }

        public void Agregar(TareaTag tt)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"INSERT INTO tareas_tags (tarea_id, tag_id)
                                        VALUES (@tarea, @tag)");

                datos.AgregarParametro("@tarea", tt.TareaId, SqlDbType.Int);
                datos.AgregarParametro("@tag", tt.TagId, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int tareaId, int tagId)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("DELETE FROM tareas_tags WHERE tarea_id = @tarea AND tag_id = @tag");
                datos.AgregarParametro("@tarea", tareaId, SqlDbType.Int);
                datos.AgregarParametro("@tag", tagId, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }

        public void EliminarPorTarea(int idTarea)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE tareas_tags SET eliminado = 1 WHERE tarea_id=@id");
                datos.AgregarParametro("@id", idTarea, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }
    }
}
