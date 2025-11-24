using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ReporteRendimientoNegocio
    {
        public List<ReporteRendimiento> Listar()
        {
            List<ReporteRendimiento> lista = new List<ReporteRendimiento>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT * FROM vw_ReporteRendimiento");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ReporteRendimiento rep = new ReporteRendimiento
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"],
                        TareasCreadas = (int)datos.Lector["TareasCreadas"],
                        TareasAsignadas = (int)datos.Lector["TareasAsignadas"],
                        TareasCompletadas = (int)datos.Lector["TareasCompletadas"],
                        TareasPendientes = (int)datos.Lector["TareasPendientes"],
                        TareasVencidas = (int)datos.Lector["TareasVencidas"]
                    };

                    lista.Add(rep);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el reporte de rendimiento", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}