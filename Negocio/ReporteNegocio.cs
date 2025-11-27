using System;
using System.Collections.Generic;
using System.Data;
using Dominio;

namespace Negocio
{
    public class ReporteNegocio
    {
        public List<ReporteTareasPorEstado> TareasPorEstado(DateTime desde, DateTime hasta)
        {
            List<ReporteTareasPorEstado> lista = new List<ReporteTareasPorEstado>();

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
                    SELECT 
                        e.nombre AS Estado,
                        COUNT(*) AS Cantidad
                    FROM tareas t
                    INNER JOIN estados e ON t.estado_id = e.id
                    WHERE t.eliminado = 0
                      AND e.es_final = 0
                      AND t.fecha_creacion BETWEEN @desde AND @hasta
                    GROUP BY e.nombre
                    ORDER BY Cantidad DESC;
                ");

                datos.AgregarParametro("@desde", desde, SqlDbType.DateTime);
                datos.AgregarParametro("@hasta", hasta, SqlDbType.DateTime);

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        ReporteTareasPorEstado item = new ReporteTareasPorEstado
                        {
                            Estado = lector["Estado"].ToString(),
                            Cantidad = Convert.ToInt32(lector["Cantidad"])
                        };
                        lista.Add(item);
                    }
                }
            }

            return lista;
        }

        public ReporteTareasVencidas TareasVencidas(DateTime desde, DateTime hasta)
        {
            ReporteTareasVencidas resultado = new ReporteTareasVencidas();

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
                    SELECT
                        SUM(CASE WHEN t.fecha_vencimiento < GETDATE() THEN 1 ELSE 0 END) AS Vencidas,
                        SUM(CASE WHEN t.fecha_vencimiento >= GETDATE() THEN 1 ELSE 0 END) AS EnFecha
                    FROM tareas t
                    WHERE t.eliminado = 0
                      AND t.fecha_creacion BETWEEN @desde AND @hasta;
                ");

                datos.AgregarParametro("@desde", desde, SqlDbType.DateTime);
                datos.AgregarParametro("@hasta", hasta, SqlDbType.DateTime);

                using (var lector = datos.EjecutarLectura())
                {
                    if (lector.Read())
                    {
                        resultado.Vencidas = lector["Vencidas"] != DBNull.Value ? Convert.ToInt32(lector["Vencidas"]) : 0;
                        resultado.EnFecha = lector["EnFecha"] != DBNull.Value ? Convert.ToInt32(lector["EnFecha"]) : 0;
                    }
                }
            }

            return resultado;
        }

        public List<ReporteHorasPorUsuario> HorasPorUsuario(DateTime desde, DateTime hasta)
        {
            List<ReporteHorasPorUsuario> lista = new List<ReporteHorasPorUsuario>();

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
                    SELECT 
                        u.nombre AS Usuario,
                        SUM(h.horas) AS TotalHoras
                    FROM horas h
                    INNER JOIN usuarios u ON h.usuario_id = u.id
                    WHERE h.eliminado = 0
                      AND h.dia BETWEEN @desde AND @hasta
                    GROUP BY u.nombre
                    ORDER BY TotalHoras DESC;
                ");

                datos.AgregarParametro("@desde", desde, SqlDbType.Date);
                datos.AgregarParametro("@hasta", hasta, SqlDbType.Date);

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        ReporteHorasPorUsuario item = new ReporteHorasPorUsuario
                        {
                            Usuario = lector["Usuario"].ToString(),
                            TotalHoras = Convert.ToDecimal(lector["TotalHoras"])
                        };

                        lista.Add(item);
                    }
                }
            }

            return lista;
        }
    }
}