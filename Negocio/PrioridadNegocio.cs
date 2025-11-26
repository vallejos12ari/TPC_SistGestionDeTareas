using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class PrioridadNegocio
    {
        public List<Prioridad> Listar()
        {
            var lista = new List<Prioridad>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM prioridades WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var p = new Prioridad
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            Color = lector["color"].ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(p);
                    }
                }
            }

            return lista;
        }

        public void Agregar(Prioridad p)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"INSERT INTO prioridades (nombre, eliminado, color)
                                           VALUES (@nombre, @eliminado, @color)");

                datos.AgregarParametro("@nombre", p.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", p.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@color", p.Color, SqlDbType.VarChar);

                datos.EjecutarAccion();
            }
        }

        public void Modificar(Prioridad p)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"UPDATE prioridades SET 
                                            nombre=@nombre, 
                                            eliminado=@eliminado, 
                                            color=@color 
                                        WHERE id=@id");

                datos.AgregarParametro("@nombre", p.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", p.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@color", p.Color, SqlDbType.VarChar);
                datos.AgregarParametro("@id", p.Id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE prioridades SET eliminado = 1 WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }
        
        public Prioridad BuscarPorId(int id)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT id, nombre, color FROM prioridades WHERE id = @id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                SqlDataReader lector = datos.EjecutarLectura();

                if (lector.Read())
                {
                    Prioridad p = new Prioridad();
                    p.Id = (int)lector["id"];
                    p.Nombre = (string)lector["nombre"];
                    p.Color = (string)lector["color"];
                    return p;
                }

                return null;
            }
        }
    }
}