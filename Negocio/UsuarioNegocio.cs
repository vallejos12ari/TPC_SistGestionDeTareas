using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> Listar()
        {
            var lista = new List<Usuario>();

            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM usuarios WHERE eliminado = 0");

                using (var lector = datos.EjecutarLectura())
                {
                    while (lector.Read())
                    {
                        var aux = new Usuario
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Email = lector["email"].ToString(),
                            Password = lector["password"].ToString(),
                            Rol = lector["rol"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            Verificado = (byte)lector["verificado"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };

                        lista.Add(aux);
                    }
                }
            }

            return lista;
        }

        public void Agregar(Usuario u)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"INSERT INTO usuarios 
                        (nombre,email,password,rol,eliminado,verificado) 
                        VALUES (@nombre,@mail,@pass,@rol,@eliminado,@verificado)");

                datos.AgregarParametro("@nombre", u.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@mail", u.Email, SqlDbType.VarChar);
                datos.AgregarParametro("@pass", u.Password, SqlDbType.VarChar);
                datos.AgregarParametro("@rol", u.Rol, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", u.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@verificado", u.Verificado, SqlDbType.TinyInt);

                datos.EjecutarAccion();
            }
        }
        
        public Usuario BuscarPorId(int idUsuario)
        {
            Usuario usuarioEncontrado = null;

            using (AccesoDatos datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM usuarios WHERE id = @idUsuario");
                datos.AgregarParametro("@idUsuario", idUsuario, SqlDbType.Int);

                using (SqlDataReader lector = datos.EjecutarLectura())
                {
                    if (lector.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = (int)lector["id"];
                        usuario.Nombre = lector["nombre"].ToString();
                        usuario.Email = lector["email"].ToString();
                        usuario.Password = lector["password"].ToString();
                        usuario.Rol = lector["rol"].ToString();
                        usuario.Eliminado = (byte)lector["eliminado"];
                        usuario.Verificado = (byte)lector["verificado"];
                        usuario.FechaCreacion = (DateTime)lector["fecha_creacion"];

                        usuarioEncontrado = usuario;
                    }
                }
            }
            return usuarioEncontrado;
        }


        public void Modificar(Usuario u)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"UPDATE usuarios SET 
                        nombre=@nombre, 
                        email=@mail, 
                        rol=@rol,
                        password=@password,
                        eliminado=@eliminado,
                        verificado=@verificado
                        WHERE id=@id");

                datos.AgregarParametro("@nombre", u.Nombre, SqlDbType.VarChar);
                datos.AgregarParametro("@mail", u.Email, SqlDbType.VarChar);
                datos.AgregarParametro("@rol", u.Rol, SqlDbType.VarChar);
                datos.AgregarParametro("@password", u.Password, SqlDbType.VarChar);
                datos.AgregarParametro("@eliminado", u.Eliminado, SqlDbType.TinyInt);
                datos.AgregarParametro("@verificado", u.Verificado, SqlDbType.TinyInt);
                datos.AgregarParametro("@id", u.Id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }
        
        public void CambiarContrasenia(int usuarioId, string password)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta(@"UPDATE usuarios SET password=@password, verificado=1 WHERE id=@id");

                datos.AgregarParametro("@password", password, SqlDbType.VarChar);
                datos.AgregarParametro("@id", usuarioId, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public void Eliminar(int id)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("UPDATE usuarios SET eliminado = 1 WHERE id=@id");
                datos.AgregarParametro("@id", id, SqlDbType.Int);

                datos.EjecutarAccion();
            }
        }

        public Usuario Login(string email, string password)
        {
            using (var datos = new AccesoDatos())
            {
                datos.DefinirConsulta("SELECT * FROM usuarios WHERE email = @email AND password = @pass AND eliminado = 0");

                datos.AgregarParametro("@email", email, SqlDbType.VarChar);
                datos.AgregarParametro("@pass", password, SqlDbType.VarChar);

                using (var lector = datos.EjecutarLectura())
                {
                    if (lector.Read())
                    {
                        return new Usuario
                        {
                            Id = (int)lector["id"],
                            Nombre = lector["nombre"].ToString(),
                            Email = lector["email"].ToString(),
                            Password = lector["password"].ToString(),
                            Rol = lector["rol"].ToString(),
                            Eliminado = (byte)lector["eliminado"],
                            Verificado = (byte)lector["verificado"],
                            FechaCreacion = (DateTime)lector["fecha_creacion"]
                        };
                    }
                }
            }

            return null;
        }
    }
}