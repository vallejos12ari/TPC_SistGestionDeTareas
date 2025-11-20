using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class EstadoNegocio
    {
        // Métodos para la entidad ESTADO
        public List<Estado> Listar()
        {
            List<Estado> lista = new List<Estado>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdEstado, NombreEstado FROM ESTADO");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Estado aux = new Estado();
                    aux.IdEstado = (int)datos.Lector["IdEstado"];
                    aux.NombreEstado = (string)datos.Lector["NombreEstado"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar estados", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Estado nuevoEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ESTADO (NombreEstado) VALUES (@NombreEstado)");
                datos.setearParametro("@NombreEstado", nuevoEstado.NombreEstado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar estado", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Estado estado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ESTADO SET NombreEstado = @NombreEstado WHERE IdEstado = @IdEstado");
                datos.setearParametro("@NombreEstado", estado.NombreEstado);
                datos.setearParametro("@IdEstado", estado.IdEstado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar estado", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int idEstado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Primero, eliminar las transiciones de estado relacionadas
                datos.setearConsulta("DELETE FROM ORDEN_ESTADOS WHERE IdEstadoActual = @IdEstado OR IdEstadoDestino = @IdEstado");
                datos.setearParametro("@IdEstado", idEstado);
                datos.ejecutarAccion();
                datos.cerrarConexion(); // Cerrar y reabrir para la siguiente consulta

                datos = new AccesoDatos(); // Nueva instancia para la segunda consulta
                datos.setearConsulta("DELETE FROM ESTADO WHERE IdEstado = @IdEstado");
                datos.setearParametro("@IdEstado", idEstado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar estado. Asegúrese de que no haya tareas asociadas.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // Métodos para la entidad ORDEN_ESTADOS
        public List<OrdenEstado> ListarOrdenEstados()
        {
            List<OrdenEstado> lista = new List<OrdenEstado>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT OE.IdOrden,
                           EA.IdEstado AS IdEstadoActual, EA.NombreEstado AS NombreEstadoActual,
                           ED.IdEstado AS IdEstadoDestino, ED.NombreEstado AS NombreEstadoDestino
                    FROM ORDEN_ESTADOS OE
                    INNER JOIN ESTADO EA ON OE.IdEstadoActual = EA.IdEstado
                    INNER JOIN ESTADO ED ON OE.IdEstadoDestino = ED.IdEstado
                ");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    OrdenEstado aux = new OrdenEstado();
                    aux.IdOrden = (int)datos.Lector["IdOrden"];
                    aux.EstadoActual = new Estado
                    {
                        IdEstado = (int)datos.Lector["IdEstadoActual"],
                        NombreEstado = (string)datos.Lector["NombreEstadoActual"]
                    };
                    aux.EstadoDestino = new Estado
                    {
                        IdEstado = (int)datos.Lector["IdEstadoDestino"],
                        NombreEstado = (string)datos.Lector["NombreEstadoDestino"]
                    };
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar órdenes de estado", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarOrdenEstado(OrdenEstado nuevaOrden)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ORDEN_ESTADOS (IdEstadoActual, IdEstadoDestino) VALUES (@IdEstadoActual, @IdEstadoDestino)");
                datos.setearParametro("@IdEstadoActual", nuevaOrden.EstadoActual.IdEstado);
                datos.setearParametro("@IdEstadoDestino", nuevaOrden.EstadoDestino.IdEstado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar orden de estado. Verifique que la transición no exista ya.", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void EliminarOrdenEstado(int idOrden)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ORDEN_ESTADOS WHERE IdOrden = @IdOrden");
                datos.setearParametro("@IdOrden", idOrden);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar orden de estado", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Estado> ObtenerEstadosDestino(int idEstadoActual)
        {
            List<Estado> lista = new List<Estado>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"
                    SELECT ED.IdEstado, ED.NombreEstado
                    FROM ORDEN_ESTADOS OE
                    INNER JOIN ESTADO ED ON OE.IdEstadoDestino = ED.IdEstado
                    WHERE OE.IdEstadoActual = @IdEstadoActual
                ");
                datos.setearParametro("@IdEstadoActual", idEstadoActual);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Estado aux = new Estado();
                    aux.IdEstado = (int)datos.Lector["IdEstado"];
                    aux.NombreEstado = (string)datos.Lector["NombreEstado"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estados destino", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
