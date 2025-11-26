using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class TipoRelacionNegocio
    {
        public List<TipoRelacion> Listar()
        {
            var lista = new List<TipoRelacion>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM tipo_relaciones WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var tr = new TipoRelacion
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(tr);
                    }
                }
            }

            return lista;
        }

        public void Agregar(TipoRelacion tr)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"INSERT INTO tipo_relaciones (nombre, eliminado)
                                        VALUES (@nombre, @eliminado)");

                datos.AgregarParametro("@nombre", tr.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", tr.Eliminado, SqlDbType.TinyInt);

                datos.EjecutarAccion();
            }
        }

        public void Modificar(TipoRelacion tr)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"UPDATE tipo_relaciones SET 
                                            nombre=@nombre,
                                            eliminado=@eliminado
                                        WHERE id=@id");

                datos.AgregarParametro("@nombre", tr.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", tr.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@id", tr.Id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE tipo_relaciones SET eliminado = 1 WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public TipoRelacion BuscarPorId(int id)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT id, nombre FROM tipo_relaciones WHERE id = @id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                SqlDataReader lector = datos.EjecutarLectura();

                if (lector.Read())
                {
                    TipoRelacion tipoRelacion = new TipoRelacion();
                    tipoRelacion.Id = (int)lector["id"];
                    tipoRelacion.Nombre = (string)lector["nombre"];

                    return tipoRelacion;
                }

                return null;
            }
        }
    }
}