using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdUsuario, NombreUsuario, Email, Rol, Activo, FechaCreacion FROM USUARIO");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Rol = (string)datos.Lector["Rol"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM USUARIO WHERE idUsuario = @idUsuario");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el usuario", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@"INSERT INTO USUARIO 
            (NombreUsuario, Email, ContraseniaHash, Rol, Activo)
            VALUES (@NombreUsuario, @Email, @Password, @Rol, @Activo)");

                datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Password", usuario.Password);
                datos.setearParametro("@Rol", usuario.Rol);
                datos.setearParametro("@Activo", usuario.Activo);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el usuario", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario ObtenerPorId(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT IdUsuario, NombreUsuario, Email, Rol, Activo
            FROM USUARIO
            WHERE IdUsuario = @IdUsuario");
                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    var u = new Usuario
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"],
                        Email = (string)datos.Lector["Email"],
                        Rol = datos.Lector["Rol"] as string,
                        Activo = Convert.ToBoolean(datos.Lector["Activo"]),
                    };

                    u.UsuariosRelacionados = ListarRelacionados(u.IdUsuario);

                    return u;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por ID", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Actualizar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string sql = @"
            UPDATE USUARIO
               SET NombreUsuario = @NombreUsuario,
                   Email         = @Email,
                   Rol           = @Rol,
                   Activo        = @Activo";

                if (!string.IsNullOrEmpty(usuario.Password))
                    sql += ", ContraseniaHash = @Password";

                sql += " WHERE IdUsuario = @IdUsuario;";

                datos.setearConsulta(sql);

                datos.setearParametro("@NombreUsuario", usuario.NombreUsuario);
                datos.setearParametro("@Email", usuario.Email);
                datos.setearParametro("@Rol", (object)usuario.Rol ?? DBNull.Value);
                datos.setearParametro("@Activo", usuario.Activo);
                datos.setearParametro("@IdUsuario", usuario.IdUsuario);

                if (!string.IsNullOrEmpty(usuario.Password))
                    datos.setearParametro("@Password", usuario.Password);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el usuario", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Usuario Validar(string email, string password)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT IdUsuario, NombreUsuario, Email, Rol, Activo, FechaCreacion
                    FROM USUARIO
                    WHERE Email = @Email AND ContraseniaHash = @Password AND Activo = 1
                ");

                datos.setearParametro("@Email", email);
                datos.setearParametro("@Password", password);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        NombreUsuario = (string)datos.Lector["NombreUsuario"],
                        Email = (string)datos.Lector["Email"],
                        Rol = datos.Lector["Rol"] as string,
                    };

                    return usuario;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el usuario", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }




        public List<Usuario> ListarRelacionados(int idUsuario)
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(@" SELECT U.IdUsuario, U.NombreUsuario, U.Email, U.Rol
                         FROM USUARIO U
                      INNER JOIN USUARIO_RELACION UR ON U.IdUsuario = UR.IdUsuarioRelacionado
                        WHERE UR.IdUsuario = @idUsuario");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IdUsuario = (int)datos.Lector["IdUsuario"];
                    aux.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Rol = (string)datos.Lector["Rol"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarRelacion(int idUsuario, int idUsuarioRelacionado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {

                if (idUsuario == idUsuarioRelacionado)
                    return;

                datos.setearConsulta("INSERT INTO USUARIO_RELACION (IdUsuario, IdUsuarioRelacionado) VALUES (@idUsuario, @idUsuarioRelacionado)");

                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idUsuarioRelacionado", idUsuarioRelacionado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void EliminarRelacion(int idUsuario, int idUsuarioRelacionado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM USUARIO_RELACION WHERE IdUsuario = @idUsuario AND IdUsuarioRelacionado = @idUsuarioRelacionado");
                datos.setearParametro("@idUsuario", idUsuario);
                datos.setearParametro("@idUsuarioRelacionado", idUsuarioRelacionado);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }

}