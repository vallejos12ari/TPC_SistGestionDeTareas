using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TagNegocio
    {
        public List<Tag> Listar()
        {
            List<Tag> lista = new List<Tag>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdTag, Nombre, Color  FROM TAG");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Tag aux = new Tag();
                    aux.IdTag = (int)datos.Lector["IdTag"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Color = datos.Lector["Color"] == DBNull.Value ? null : (string)datos.Lector["Color"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar Tags", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // aca se busca tag por id
        public Tag ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Tag tag = null;

            try
            {
                datos.setearConsulta("SELECT IdTag, Nombre, Color FROM TAG WHERE IdTag = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    tag = new Tag
                    {
                        IdTag = (int)datos.Lector["IdTag"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Color = datos.Lector["Color"] == DBNull.Value ? null : (string)datos.Lector["Color"]
                    };
                }

                return tag;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el Tag por ID", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        
        public void Agregar(Tag tag)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO TAG (Nombre, Color) VALUES (@nombre, @color)");
                datos.setearParametro("@nombre", tag.Nombre);
                datos.setearParametro("@color", tag.Color);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar Tag", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Tag tag)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE TAG SET Nombre = @nombre, Color = @color WHERE IdTag = @id");
                datos.setearParametro("@nombre", tag.Nombre);
                datos.setearParametro("@color", tag.Color);
                datos.setearParametro("@id", tag.IdTag);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar Tag", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM TAG WHERE IdTag = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar Tag", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}