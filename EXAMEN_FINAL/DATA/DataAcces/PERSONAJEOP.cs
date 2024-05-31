using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXAMEN_FINAL.DATA.DataAcces
{
    internal class PERSONAJEOP
    {

        // Información de conexión a la base de datos
        private String connectionString = "Server=localhost;Database= examen_final; Uid=root;Pwd=Dios1234";

        //constructor
        public PERSONAJEOP(string servidor, string usur, string pwd)
        {
            connectionString = "Server=" + servidor + ";Database=examen_final;Uid=" + usur + ";Pwd=" + pwd + "Dios1234";
        }

      

        // Método para leer todos los personajes
        public DataTable LeerPersonajes()
        {
            DataTable personajes = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT * FROM personajes_one_piece";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(personajes);
                    }
                }
            }

            return personajes;
        }


        //METODO PARA CREAR NUEVO PERSONAJE
       

        public int CrearPersonaje(string nombre, string grupo, string cargo, int nivel_poder, string raza, int recompensa, string fruta_del_diablo, DateTime Fecha_creacion)
        {
            // Validar los datos antes de la inserción
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(grupo) ||
                string.IsNullOrWhiteSpace(cargo) || string.IsNullOrWhiteSpace(raza) ||
                nivel_poder < 0 || recompensa < 0)
            {
                throw new ArgumentException("Uno o más parámetros son inválidos.");
            }

            // Intentar la inserción y manejar posibles errores
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO personajes_one_piece (nombre, grupo, cargo, nivel_poder, raza, recompensa, fruta_del_diablo, Fecha_creacion) VALUES (@nombre, @grupo, @cargo, @nivel_poder, @raza, @recompensa, @fruta_del_diablo, @Fecha_creacion)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@grupo", grupo);
                        command.Parameters.AddWithValue("@cargo", cargo);
                        command.Parameters.AddWithValue("@nivel_poder", nivel_poder);
                        command.Parameters.AddWithValue("@raza", raza);
                        command.Parameters.AddWithValue("@recompensa", recompensa);
                        command.Parameters.AddWithValue("@fruta_del_diablo", fruta_del_diablo);
                        command.Parameters.AddWithValue("@Fecha_creacion", Fecha_creacion);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Manejar errores específicos de MySQL
                Console.WriteLine("Error de MySQL: " + ex.Message);
                return -1; // O cualquier otro valor que indique un error
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine("Error: " + ex.Message);
                return -1; // O cualquier otro valor que indique un error
            }


        }

        //METODO PARA ACTUALIZAR PERSONAJE

        public int ActualizarPersonaje(int id, string nombre, string grupo, string cargo, int nivel_poder, string raza, int recompensa, string fruta_del_diablo, DateTime Fecha_creacion)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Construir la consulta SQL dinámicamente basada en los valores no vacíos
                    StringBuilder sql = new StringBuilder("UPDATE personajes_one_piece SET ");
                    MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;

                    if (!string.IsNullOrWhiteSpace(nombre))
                    {
                        sql.Append("nombre = @nombre, ");
                        command.Parameters.AddWithValue("@nombre", nombre);
                    }
                    if (!string.IsNullOrWhiteSpace(grupo))
                    {
                        sql.Append("grupo = @grupo, ");
                        command.Parameters.AddWithValue("@grupo", grupo);
                    }
                    if (!string.IsNullOrWhiteSpace(cargo))
                    {
                        sql.Append("cargo = @cargo, ");
                        command.Parameters.AddWithValue("@cargo", cargo);
                    }
                    if (nivel_poder > 0)
                    {
                        sql.Append("nivel_poder = @nivel_poder, ");
                        command.Parameters.AddWithValue("@nivel_poder", nivel_poder);
                    }
                    if (!string.IsNullOrWhiteSpace(raza))
                    {
                        sql.Append("raza = @raza, ");
                        command.Parameters.AddWithValue("@raza", raza);
                    }
                    if (recompensa > 0)
                    {
                        sql.Append("recompensa = @recompensa, ");
                        command.Parameters.AddWithValue("@recompensa", recompensa);
                    }
                    if (!string.IsNullOrWhiteSpace(fruta_del_diablo))
                    {
                        sql.Append("fruta_del_diablo = @fruta_del_diablo, ");
                        command.Parameters.AddWithValue("@fruta_del_diablo", fruta_del_diablo);
                    }

                    sql.Append("Fecha_creacion = @Fecha_creacion ");
                    command.Parameters.AddWithValue("@Fecha_creacion", Fecha_creacion);

                    sql.Append("WHERE id = @id");
                    command.Parameters.AddWithValue("@id", id);

                    command.CommandText = sql.ToString();

                    return command.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                // Manejar errores específicos de MySQL
                Console.WriteLine("Error de MySQL: " + ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                // Manejar otros errores
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        //METODO PARA ELIMINAR PERSONAJE

        public int EliminarPersonaje(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID inválido.");
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "DELETE FROM personajes_one_piece WHERE id = @id";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error de MySQL: " + ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        //ordenar un personaje por fecha_creacion 
        public DataTable OrdenarPersonajeFecha(DateTime Fecha_creacion)
        {
            DataTable ordenar = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM examen_final.personajes_one_piece ORDER BY Fecha_creacion ASC =@Fecha_creacion ";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Fecha_creacion", Fecha_creacion);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(ordenar);
                    }
                }
            }

            return ordenar;
        }

        //Ordenar Reciente 
        public DataTable ObtenerPersonajesRecientes(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable personajesRecientes = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM examen_final.personajes_one_piece WHERE Fecha_creacion BETWEEN @fechaInicio AND @fechaFin ORDER BY Fecha_creacion ASC";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@fechaFin", fechaFin);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(personajesRecientes);
                    }
                }
            }

            return personajesRecientes;
        }
    }
    }






    

