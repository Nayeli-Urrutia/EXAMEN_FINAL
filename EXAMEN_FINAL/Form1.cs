using EXAMEN_FINAL.DATA.DataAcces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace EXAMEN_FINAL
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.ErrorProvider errorProvider;

        private string[] gruposOnePiece = {
            "Pirata",
            "Marina",
            "Revolucionario",
            "Shichibukai",
            "Yonkou",
            "CP9",
            "CP0",
            "Cazarrecompensas",
            "Mink",
            "Samurai",
            "Hombre-Pez",
            "Gigante",
            "Gyojin",
            "Tontatta",
            "Cyborg",
            "Civiles"
        };

        private PERSONAJEOP personaje;

        private void CargarPersonajes()
        {
            dataGridViewOP.DataSource = personaje.LeerPersonajes();
      
        }

        public Form1()
        {
            InitializeComponent();
            personaje = new PERSONAJEOP("localhost", "root", "");
            this.Load += new System.EventHandler(this.Form1_Load);
        }

        // mostrar listado
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxGrupo.Items.AddRange(gruposOnePiece);
            

        }




        //MOSTRAR PERSONAJES
        private void button1Cargar_Click(object sender, EventArgs e)
        {
            dataGridViewOP.DataSource = personaje.LeerPersonajes();
        }


        private void comboBoxGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxGrupo.Items.AddRange(gruposOnePiece);
        }


        //AGREGAR PERSONAJE
        private void buttonInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener valores de los controles de la interfaz de usuario
                string nombre = textBoxNombre.Text;
                string grupo = comboBoxGrupo.Text;
                string cargo = textBoxCargo.Text;
                int nivel_poder = (int)numericUpDownNivle_poder.Value;
                string raza = textBoxRaza.Text;
                int recompensa = (int)numericUpDownRecompensa.Value;
                string fruta_del_diablo = textBoxFruta_del_diablo.Text;

                // Llamar al método CrearPersonaje y obtener la respuesta
                int respuesta = personaje.CrearPersonaje(nombre, grupo, cargo, nivel_poder, raza, recompensa, fruta_del_diablo);

                // Verificar la respuesta e informar al usuario
                if (respuesta > 0)
                {
                    MessageBox.Show("Personaje creado correctamente");
                    dataGridViewOP.DataSource = personaje.LeerPersonajes();
                }
                else
                {
                    MessageBox.Show("Error al crear el personaje");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y mostrar un mensaje de error al usuario
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar que haya un registro seleccionado en el DataGridView
                if (dataGridViewOP.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un personaje para actualizar.");
                    return;
                }

                // Obtener la fila seleccionada
                DataGridViewRow row = dataGridViewOP.SelectedRows[0];

                // Obtener el ID del personaje (asumiendo que hay una columna "id")
                int id = Convert.ToInt32(row.Cells["id"].Value);

                // Obtener los valores de los controles de la interfaz de usuario
                string nombre = textBoxNombre.Text.Trim();
                string grupo = comboBoxGrupo.Text.Trim();
                string cargo = textBoxCargo.Text.Trim();
                int nivel_poder = (int)numericUpDownNivle_poder.Value;
                string raza = textBoxRaza.Text.Trim();
                int recompensa = (int)numericUpDownRecompensa.Value;
                string fruta_del_diablo = textBoxFruta_del_diablo.Text.Trim();

                // Validar los datos antes de la actualización
                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(grupo) ||
                    string.IsNullOrWhiteSpace(cargo) || string.IsNullOrWhiteSpace(raza) ||
                    nivel_poder < 0 || recompensa < 0)
                {
                    MessageBox.Show("Uno o más parámetros son inválidos.");
                    return;
                }

                // Confirmar con el usuario si desea realizar la actualización
                DialogResult result = MessageBox.Show("¿Está seguro de que desea actualizar este personaje?", "Confirmar Actualización", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Llamar al método para actualizar el personaje
                    int respuesta = personaje.ActualizarPersonaje(id, nombre, grupo, cargo, nivel_poder, raza, recompensa, fruta_del_diablo);

                    // Verificar la respuesta del método de actualización
                    if (respuesta > 0)
                    {
                        MessageBox.Show("Personaje actualizado correctamente.");
                        // Actualizar el DataGridView para reflejar los cambios
                        dataGridViewOP.DataSource = personaje.LeerPersonajes();
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el personaje.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y mostrar un mensaje de error al usuario
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }


        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewOP.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione un personaje para eliminar.");
                    return;
                }

                DataGridViewRow row = dataGridViewOP.SelectedRows[0];
                int id = Convert.ToInt32(row.Cells["id"].Value);

                var confirmResult = MessageBox.Show("¿Está seguro de que desea eliminar este personaje?",
                                                     "Confirmar Eliminación",
                                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    int respuesta = personaje.EliminarPersonaje(id);

                    if (respuesta > 0)
                    {
                        MessageBox.Show("Personaje eliminado correctamente");
                        CargarPersonajes();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el personaje");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        }
    
}
    
    
    

        
            


          

