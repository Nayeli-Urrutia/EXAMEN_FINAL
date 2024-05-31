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
        public Form1()
        {
            InitializeComponent();
            errorProvider = new System.Windows.Forms.ErrorProvider();
            personaje = new PERSONAJEOP("localhost", "root", "");
            this.Load += new System.EventHandler(this.Form1_Load);
          

            // Suscribir eventos de validación
            textBoxNombre.Validating += new CancelEventHandler(textBoxNombre_Validating);
            textBoxCargo.Validating += new CancelEventHandler(textBoxCargo_Validating);
            textBoxRaza.Validating += new CancelEventHandler(textBoxRaza_Validating);
            comboBoxGrupo.Validating += new CancelEventHandler(comboBoxGrupo_Validating);

            // Suscribir eventos KeyPress
            textBoxNombre.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            textBoxCargo.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            textBoxRaza.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            comboBoxGrupo.KeyPress += new KeyPressEventHandler(comboBox_KeyPress);
        }
       




        private void textBoxNombre_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
            {
                errorProvider.SetError(textBoxNombre, "El nombre es requerido");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textBoxNombre, "");
            }
        }

        private void textBoxCargo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCargo.Text))
            {
                errorProvider.SetError(textBoxCargo, "El cargo es requerido");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textBoxCargo, "");
            }
        }

        private void textBoxRaza_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxRaza.Text))
            {
                errorProvider.SetError(textBoxRaza, "La raza es requerida");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(textBoxRaza, "");
            }
        }

        private void comboBoxGrupo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxGrupo.Text))
            {
                errorProvider.SetError(comboBoxGrupo, "El grupo es requerido");
                e.Cancel = true;
            }
            else
            {
                errorProvider.SetError(comboBoxGrupo, "");
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Evita el beep estándar de Windows al presionar Enter
                this.Validate();
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // Evita el beep estándar de Windows al presionar Enter
                this.Validate();
            }
        }



        private PERSONAJEOP personaje;

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


        //MOSTRAR TABLA
        private void CargarPersonajes()
        {
            dataGridViewOP.DataSource = personaje.LeerPersonajes();
      
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


        //Mostrar los Grupos
        private void comboBoxGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxGrupo.Items.AddRange(gruposOnePiece);
        }


        //AGREGAR PERSONAJE
        private void buttonInsertar_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
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
                    DateTime Fecha_creacion = DateTime.Now;


                    // Llamar al método CrearPersonaje y obtener la respuesta
                    int respuesta = personaje.CrearPersonaje(nombre, grupo, cargo, nivel_poder, raza, recompensa, fruta_del_diablo, Fecha_creacion);

                    // Verificar la respuesta e informar al usuario
                    if (respuesta > 0)
                    {
                        MessageBox.Show("Personaje creado correctamente");
                        dataGridViewOP.DataSource = personaje.LeerPersonajes();
                        LimpiarControles();
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
        }
        private void LimpiarControles()
        {
            textBoxNombre.Text = "";
            textBoxCargo.Text = "";
            textBoxRaza.Text = "";
            textBoxFruta_del_diablo.Text = "";
            numericUpDownNivle_poder.Value = 0;
            numericUpDownRecompensa.Value = 0;
            // Limpiar la selección en el ComboBox
            comboBoxGrupo.SelectedIndex = -1;
        }
        //ACTUALIZAR PERSONAJE

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
                DateTime Fecha_creacion = DateTime.Now; // Obtener la fecha actual

                // Confirmar con el usuario si desea realizar la actualización
                DialogResult result = MessageBox.Show("¿Está seguro de que desea actualizar este personaje?", "Confirmar Actualización", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Llamar al método para actualizar el personaje
                    int respuesta = personaje.ActualizarPersonaje(id, nombre, grupo, cargo, nivel_poder, raza, recompensa, fruta_del_diablo, Fecha_creacion);

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

        //ELIMINAR PERSONAJE

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

        private PERSONAJEOP ordenar;



        //Ordenar fecha descendente
        private void buscarPorFecha()
        {

            DataTable todosLosPersonajes = personaje.LeerPersonajes();

            DateTime Fecha_creacion = dateTimePickerfecha.Value;


            DataView dv = todosLosPersonajes.DefaultView;
            dv.Sort = "Fecha_creacion DESC"; // Cambia ASC por DESC si deseas ordenar en orden descendente
            DataTable personajesOrdenados = dv.ToTable();

            dataGridViewOP.DataSource = personajesOrdenados;


        }

        private void button1_Click(object sender, EventArgs e)
        {

            buscarPorFecha();
        }


        //Fecha reciente


        private void buscarPorFechareciente()
        {
            DateTime fechaFin = DateTime.Now;
            DateTime fechaInicio = fechaFin.AddDays(-5); // Cambia el número de días según queramos

            DataTable personajesRecientesOrdenados = personaje.ObtenerPersonajesRecientes(fechaInicio, fechaFin);

            dataGridViewOP.DataSource = personajesRecientesOrdenados;
        }


      

        private void button2_Click(object sender, EventArgs e)
        {
            buscarPorFechareciente();
        }
    }
    
}
    
    
    

        
            


          

