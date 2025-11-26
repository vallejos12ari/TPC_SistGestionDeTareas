using System;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos : IDisposable
    {
        private readonly SqlConnection _conexion;
        private readonly SqlCommand _comando;

        public AccesoDatos()
        {
            _conexion = new SqlConnection("server=.\\SQLEXPRESS;database=gestion_de_tareas; integrated security=true");
            _comando = new SqlCommand
            {
                Connection = _conexion
            };
        }

        /// define la consulta que se va a ejecutar.
        public void DefinirConsulta(string consulta)
        {
            _comando.CommandType = CommandType.Text;
            _comando.CommandText = consulta;
            _comando.Parameters.Clear();
        }

        /// agrega un parametro
        public void AgregarParametro(string nombre, object valor, SqlDbType tipo)
        {
            _comando.Parameters.Add(nombre, tipo).Value = valor ?? DBNull.Value;
        }

        /// ejecuta una consulta que devuelve filas
        public SqlDataReader EjecutarLectura()
        {
            try
            {
                if (_conexion.State != ConnectionState.Open)
                    _conexion.Open();

                // CloseConnection permite que al cerrar el reader se cierre también la conexión
                return _comando.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {
                throw;
            }
        }

        /// ejecuta una accion
        public int EjecutarAccion()
        {
            try
            {
                if (_conexion.State != ConnectionState.Open)
                    _conexion.Open();

                return _comando.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _conexion.Close();
            }
        }
        
        public object EjecutarScalar()
        {
            try
            {
                if (_conexion.State != ConnectionState.Open)
                    _conexion.Open();

                return _comando.ExecuteScalar();
            }
            catch
            {
                throw;
            }
            finally
            {
                _conexion.Close();
            }
        }

        /// libero todos los recursos con el dispose
        public void Dispose()
        {
            if (_conexion.State != ConnectionState.Closed)
                _conexion.Close();

            _conexion.Dispose();
            _comando.Dispose();
        }
    }
}