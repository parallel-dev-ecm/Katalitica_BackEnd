using System.Data;
using System.Diagnostics;
using Katalitica_API.Controllers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Katalitica_API.Resources
{
    public class DBDatos: DbContext
    {
       //public static string cadenaConexion = "server=10.20.30.3;database=KataliticaTMS_Test;User ID=sa;Password=S0p0rt3+;TrustServerCertificate=True";

      public static string cadenaConexion = "server=201.131.21.32;database=KataliticaTMS_Test;User ID=sa;Password=S0p0rt3+;TrustServerCertificate=True";
        public static DataSet ListarTablas(string nombreProcedimiento, List<ParameterResource> parametros = null)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Name, parametro.Value);
                    }
                }
                DataSet tabla = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);


                return tabla;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

        public static DataTable Listar(string nombreProcedimiento, List<ParameterResource> parametros = null)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Name, parametro.Value);
                    }
                    
                }
                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);


                return tabla;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

        public static bool ExecuteStoredProcedure(string nombreProcedimiento, List<ParameterResource> parametros = null)
        {
            using SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                using SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Name, parametro.Value);
                    }
                }

                object result = cmd.ExecuteScalar();
                return (result != null && Convert.ToInt32(result) == 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // This will print the error message to the console
                return false;
            }
        }


        public static bool Ejecutar(string nombreProcedimiento, List<ParameterResource> parametros = null)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (parametros != null)
                {
                    foreach (var parametro in parametros)
                    {
                        cmd.Parameters.AddWithValue(parametro.Name, parametro.Value);
                    }
                }

                int i = cmd.ExecuteNonQuery();
                Trace.WriteLine("result" + i);
                //return (i > 0);
                return true;
            }
            catch (SqlException ex)
            {
                Trace.WriteLine(ex.Message); // This will print the error message to the console
                Trace.WriteLine(ex.StackTrace); // This will print the stack trace to the console
                if (ex.InnerException != null)
                {
                    Trace.WriteLine(ex.InnerException.Message); // Print inner exception message, if any
                }
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

    }
}

