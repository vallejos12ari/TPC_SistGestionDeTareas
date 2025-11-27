using Dominio;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class EstadoFlujoNegocio
    {
        public List<EstadoFlujo> Listar()
        {
            List<EstadoFlujo> lista = new List<EstadoFlujo>();

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(
                    "SELECT f.id, o.id AS origenId, o.nombre AS origenNombre, o.color AS origenColor, " +
                    "d.id AS destinoId, d.nombre AS destinoNombre, d.color AS destinoColor " +
                    "FROM estados_flujo f " +
                    "INNER JOIN estados o ON o.id = f.estado_origen_id " +
                    "INNER JOIN estados d ON d.id = f.estado_destino_id WHERE o.eliminado = 0 AND d.eliminado = 0");

                SqlDataReader lector = datos.EjecutarLectura();

                while (lector.Read())
                {
                    EstadoFlujo flujo = new EstadoFlujo();

                    flujo.Id = (int)lector["id"];

                    Estado origen = new Estado();
                    origen.Id = (int)lector["origenId"];
                    origen.Nombre = (string)lector["origenNombre"];
                    origen.Color = (string)lector["origenColor"];
                    flujo.Origen = origen;

                    Estado destino = new Estado();
                    destino.Id = (int)lector["destinoId"];
                    destino.Nombre = (string)lector["destinoNombre"];
                    destino.Color = (string)lector["destinoColor"];
                    flujo.Destino = destino;

                    lista.Add(flujo);
                }
            }

            return lista;
        }

        public void Agregar(int idOrigen, int idDestino)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("INSERT INTO estados_flujo (estado_origen_id, estado_destino_id) VALUES (@o, @d)");
                datos.AgregarParametro("@o", idOrigen, SqlDbType.Int);
                datos.AgregarParametro("@d", idDestino, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("DELETE FROM estados_flujo WHERE id = @id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }
        
        public bool ExisteFlujo(int idOrigen, int idDestino)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(
                    "SELECT COUNT(*) FROM estados_flujo " +
                    "WHERE estado_origen_id = @o AND estado_destino_id = @d");
                datos.AgregarParametro("@o", idOrigen, SqlDbType.Int);
                datos.AgregarParametro("@d", idDestino, SqlDbType.Int);

                SqlDataReader lector = datos.EjecutarLectura();

                if (lector.Read())
                {
                    int cantidad = (int)lector[0];
                    return cantidad > 0;
                }

                return false;
            }
        }
        
    }
}