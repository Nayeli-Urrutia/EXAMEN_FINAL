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



       

        public int CrearPersonaje(string nombre, string grupo, string cargo, int nivel_poder, string raza, int recompensa, string fruta_del_diablo)
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

                    string sql = "INSERT INTO personajes_one_piece (nombre, grupo, cargo, nivel_poder, raza, recompensa, fruta_del_diablo) VALUES (@nombre, @grupo, @cargo, @nivel_poder, @raza, @recompensa, @fruta_del_diablo)";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", nombre);
                        command.Parameters.AddWithValue("@grupo", grupo);
                        command.Parameters.AddWithValue("@cargo", cargo);
                        command.Parameters.AddWithValue("@nivel_poder", nivel_poder);
                        command.Parameters.AddWithValue("@raza", raza);
                        command.Parameters.AddWithValue("@recompensa", recompensa);
                        command.Parameters.AddWithValue("@fruta_del_diablo", fruta_del_diablo);

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

            public int ActualizarPersonaje(int id, string nombre, string grupo, string cargo, int nivel_poder, string raza, int recompensa, string fruta_del_diablo)
            {
                // Validar los datos antes de la actualización
                if (id <= 0 || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(grupo) ||
                    string.IsNullOrWhiteSpace(cargo) || string.IsNullOrWhiteSpace(raza) ||
                    nivel_poder < 0 || recompensa < 0)
                {
                    throw new ArgumentException("Uno o más parámetros son inválidos.");
                }

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string sql = "UPDATE personajes_one_piece SET nombre = @nombre, grupo = @grupo, cargo = @cargo, nivel_poder = @nivel_poder, raza = @raza, recompensa = @recompensa, fruta_del_diablo = @fruta_del_diablo WHERE id = @id";
                        using (MySqlCommand command = new MySqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@nombre", nombre);
                            command.Parameters.AddWithValue("@grupo", grupo);
                            command.Parameters.AddWithValue("@cargo", cargo);
                            command.Parameters.AddWithValue("@nivel_poder", nivel_poder);
                            command.Parameters.AddWithValue("@raza", raza);
                            command.Parameters.AddWithValue("@recompensa", recompensa);
                            command.Parameters.AddWithValue("@fruta_del_diablo", fruta_del_diablo);

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
    }
    }






    

