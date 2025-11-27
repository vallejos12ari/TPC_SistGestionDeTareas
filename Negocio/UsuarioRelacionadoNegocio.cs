using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class UsuariosRelacionadosNegocio
    {
        public void Asignar(int idSupervisor, int idUsuario)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("INSERT INTO usuarios_relacionados (id_supervisor, id_usuario) VALUES (@s, @u)");
                datos.AgregarParametro("@s", idSupervisor, SqlDbType.Int);
                datos.AgregarParametro("@u", idUsuario, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }

        public void Desasignar(int idSupervisor, int idUsuario)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("DELETE FROM usuarios_relacionados WHERE id_supervisor = @s AND id_usuario = @u");
                datos.AgregarParametro("@s", idSupervisor, SqlDbType.Int);
                datos.AgregarParametro("@u", idUsuario, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }

        public List<Usuario> ListarAsignados(int idSupervisor)
        {
            List<Usuario> lista = new List<Usuario>();

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(
                    "SELECT u.id, u.nombre, u.email, u.rol, u.verificado " +
                    "FROM usuarios_relacionados r " +
                    "INNER JOIN usuarios u ON u.id = r.id_usuario AND u.eliminado = 0" +
                    "WHERE r.id_supervisor = @s"
                );

                datos.AgregarParametro("@s", idSupervisor, SqlDbType.Int);

                using (SqlDataReader lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = (int)lector["id"];
                        usuario.Nombre = lector["nombre"].ToString();
                        usuario.Email = lector["email"].ToString();
                        usuario.Rol = lector["rol"].ToString();
                        usuario.Verificado = (byte)lector["verificado"];
                        lista.Add(usuario);
                    }
                }
            }

            return lista;
        }

        public List<Usuario> ListarSupervisoresDeUsuario(int idUsuario)
        {
            List<Usuario> lista = new List<Usuario>();

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta(
                    "SELECT u.id, u.nombre, u.email, u.rol, u.verificado " +
                    "FROM usuarios_relacionados r " +
                    "INNER JOIN usuarios u ON u.id = r.id_supervisor " +
                    "WHERE r.id_usuario = @u"
                );

                datos.AgregarParametro("@u", idUsuario, SqlDbType.Int);

                using (SqlDataReader lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = (int)lector["id"];
                        usuario.Nombre = lector["nombre"].ToString();
                        usuario.Email = lector["email"].ToString();
                        usuario.Rol = lector["rol"].ToString();
                        usuario.Verificado = (byte)lector["verificado"];
                        lista.Add(usuario);
                    }
                }
            }

            return lista;
        }

        public void BorrarAsignados(int idSupervisor)
        {
            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("DELETE FROM usuarios_relacionados WHERE id_supervisor = @s");
                datos.AgregarParametro("@s", idSupervisor, SqlDbType.Int);
                datos.EjecutarAccion();
            }
        }
    }
}