using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace ETL_CAT
{
    public partial class formConfiguracion : Form
    {
        //Lectura de archivo de configuración de servidor 
        string linea;
        public formConfiguracion()
        {
            InitializeComponent();
            string path = "Conect.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            if (File.Exists(path))
            {
                linea = file.ReadLine();
                string[] tokens = linea.Split(';');
                Servidor.Text = tokens[0];
                NombreBD.Text = tokens[1];
                UsuarioBD.Text = tokens[2];
                PassBD.Text = tokens[3];
            }
        }
        //Prueba la conexión con el servidor
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string connetionString = "Data Source="+Servidor.Text+"; Initial Catalog =" +NombreBD.Text+";User ID ="+ UsuarioBD.Text+"; Password =" +PassBD.Text;
            SqlConnection cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Conexión creada exitosamente.", "Axolotl ETL");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error de conexión, revise la configuración y después inténtelo nuevamente.", "Axolotl ETL");
            }
        }
        private void formConfiguracion_Load(object sender, EventArgs e)
        {

        }
    }
}
