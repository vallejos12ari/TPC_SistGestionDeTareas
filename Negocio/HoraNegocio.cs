using System;
using System.Collections.Generic;
using System.Data;
using Dominio;

namespace Negocio
{
    public class HoraNegocio
    {
        public List<Hora> Listar()
        {
            var lista = new List<Hora>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM horas WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var h = new Hora
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            UsuarioId = (int)lector["usuario_id"],
                            Horas = (decimal)lector["horas"],
                            Dia = (DateTime)lector["dia"],
                            Eliminado = (byte)lector["eliminado"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(h);
                    }
                }
            }

            return lista;
        }
        
        public List<Hora> ListarPorTarea(int tareaId)
        {
            var lista = new List<Hora>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM horas WHERE eliminado = 0 AND tarea_id = @id");
                datos.AgregarParametro("@id", tareaId, SqlDbType.Int);
                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var h = new Hora
                        {
                            Id = (int)lector["id"],
                            TareaId = (int)lector["tarea_id"],
                            UsuarioId = (int)lector["usuario_id"],
                            Horas = (decimal)lector["horas"],
                            Dia = (DateTime)lector["dia"],
                            Eliminado = (byte)lector["eliminado"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(h);
                    }
                }
            }

            return lista;
        }
        
        

        public void Agregar(Hora h)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"
                    INSERT INTO horas (tarea_id, usuario_id, horas, dia, eliminado)
                    VALUES (@tarea, @usuario, @horas, @dia, @eliminado)");

                datos.AgregarParametro("@tarea", h.TareaId, SqlDbType.Int);
                datos.AgregarParametro("@usuario", h.UsuarioId, SqlDbType.Int);
                datos.AgregarParametro("@horas", h.Horas, SqlDbType.Decimal);
                datos.AgregarParametro("@dia", h.Dia, SqlDbType.Date);
                datos.AgregarParametro("@eliminado", h.Eliminado, SqlDbType.TinyInt);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE horas SET eliminado = 1 WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }
    }
}
