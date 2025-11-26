using System;
using System.Collections.Generic;
using System.Data;
using Dominio;

namespace Negocio
{
    public class HistorialNegocio
    {
        public List<Historial> Listar()
        {
            var lista = new List<Historial>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM historial");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var h = new Historial
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            UsuarioId = (int)lector["usuario_id"],
                            ValorAnterior = lector["valor_anterior"] != DBNull.Value ? lector["valor_anterior"].ToString() : null,
                            ValorNuevo = lector["valor_nuevo"] != DBNull.Value ? lector["valor_nuevo"].ToString() : null,
                            Fecha = (DateTime)lector["fecha"]
                        };

                        lista.Add(h);
                    }
                }
            }

            return lista;
        }

        public void Agregar(Historial h)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
                    INSERT INTO historial (tarea_id, usuario_id, valor_anterior, valor_nuevo)
                    VALUES (@tarea, @usuario, @anterior, @nuevo)");

                datos.AgregarParametro("@tarea", h.TareaId, SqlDbType.Int);
                datos.AgregarParametro("@usuario", h.UsuarioId, SqlDbType.Int);
                datos.AgregarParametro("@anterior", (object)h.ValorAnterior ?? DBNull.Value, SqlDbType.Text);
                datos.AgregarParametro("@nuevo", (object)h.ValorNuevo ?? DBNull.Value, SqlDbType.Text);

                datos.EjecutarAccion();
            }
        }
    }
}