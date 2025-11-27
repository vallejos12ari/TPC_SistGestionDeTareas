using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class EstadoNegocio
    {
        public List<Estado> Listar()
        {
            var lista = new List<Estado>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM estados WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var e = new Estado
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            Color = lector["color"].ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"],
                            EsFinal = (byte)lector["es_final"],
                        };

                        lista.Add(e);
                    }
                }
            }

            return lista;
        }

        public void Agregar(Estado e)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"INSERT INTO estados (nombre, eliminado, color, es_final)
                                           VALUES (@nombre, @eliminado, @color, @esFinal)");

                datos.AgregarParametro("@nombre", e.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", e.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@color", e.Color, SqlDbType.VarChar);
                datos.AgregarParametro("@esFinal", e.EsFinal, SqlDbType.TinyInt);

                datos.EjecutarAccion();
            }
        }

        public void Modificar(Estado e)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"UPDATE estados SET 
                                            nombre=@nombre, 
                                            eliminado=@eliminado,
                                            color=@color,
                                            es_final=@esFinal
                                        WHERE id=@id");

                datos.AgregarParametro("@nombre", e.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", e.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@color", e.Color, SqlDbType.VarChar);
                datos.AgregarParametro("@id", e.Id, SqlDbType.Int);
                datos.AgregarParametro("@esFinal", e.EsFinal, SqlDbType.TinyInt);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE estados SET eliminado = 1 WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public Estado BuscarPorId(int id)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT id, nombre, color, es_final FROM estados WHERE id = @id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                SqlDataReader lector = datos.EjecutarLectura();

                if (lector.Read())
                {
                    Estado estado = new Estado();
                    estado.Id = (int)lector["id"];
                    estado.Nombre = (string)lector["nombre"];
                    estado.Color = (string)lector["color"];
                    estado.EsFinal = (byte)lector["es_final"];

                    return estado;
                }

                return null;
            }
        }

        public void AsignarInicial(int estadoId)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                    datos.DefinirConsulta(@"
                UPDATE estados 
                SET es_inicial = 0 
                WHERE es_inicial = 1;

                UPDATE estados 
                SET es_inicial = 1 
                WHERE id = @id;
            ");

                datos.AgregarParametro("@id", estadoId, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }
        
        public Estado BuscarInicial()
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT TOP 1 * FROM estados WHERE eliminado = 0 AND es_inicial = 1");

                using (var lector = datos.EjecutarLectura())
                {
                    if (lector.Read())
                    {
                        return new Estado
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            Color = lector["color"]?.ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };
                    }
                }
            }

            return null;
        }
        
        public List<Estado> ObtenerSiguienteEstado(int estadoActualId)
        {
            var lista = new List<Estado>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
            SELECT e.*
            FROM estados_flujo f
            INNER JOIN estados e ON e.id = f.estado_destino_id
            WHERE f.estado_origen_id = @origen
              AND e.eliminado = 0
        ");

                datos.AgregarParametro("@origen", estadoActualId, SqlDbType.Int);

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var e = new Estado
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            Color = lector["color"]?.ToString(),
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(e);
                    }
                }
            }

            return lista;
        }

    }
}