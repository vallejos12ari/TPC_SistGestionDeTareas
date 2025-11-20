using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PrioridadNegocio
    {
        public List<Prioridad> Listar()
        {
            List<Prioridad> lista = new List<Prioridad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdPrioridad, NombrePrioridad, Nivel FROM PRIORIDAD");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Prioridad aux = new Prioridad();
                    aux.IdPrioridad = (int)datos.Lector["IdPrioridad"];
                    aux.NombrePrioridad = (string)datos.Lector["NombrePrioridad"];
                    aux.Nivel = (int)datos.Lector["Nivel"];

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Agregar(Prioridad prioridad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO PRIORIDAD (NombrePrioridad, Nivel) VALUES (@nombre, @nivel)");
                datos.setearParametro("@nombre", prioridad.NombrePrioridad);
                datos.setearParametro("@nivel", prioridad.Nivel);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void Modificar(Prioridad prioridad)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PRIORIDAD SET NombrePrioridad=@nombre, Nivel=@nivel WHERE IdPrioridad=@id");
                datos.setearParametro("@nombre", prioridad.NombrePrioridad);
                datos.setearParametro("@nivel", prioridad.Nivel);
                datos.setearParametro("@id", prioridad.IdPrioridad);
                datos.ejecutarAccion();
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
                datos.setearConsulta("DELETE FROM PRIORIDAD WHERE IdPrioridad=@id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}