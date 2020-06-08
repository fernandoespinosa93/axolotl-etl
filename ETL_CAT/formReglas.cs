using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETL_CAT
{
    public partial class formReglas : Form
    {
        public formReglas()
        {
            InitializeComponent();
        }
        #region //Muestra el la posición del cursor en pantalla
        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int line = textBox1.GetLineFromCharIndex(textBox1.SelectionStart);
            int column = textBox1.SelectionStart - textBox1.GetFirstCharIndexFromLine(line);
            label10.Text = line.ToString() + ",";
            label11.Text = column.ToString();
        }
        #endregion
        #region //oculta o muestra iconos segun la opción señalada
        public void MostrarIconos() {
            label1.Visible = false;
            label2.Visible = false;
            pictureBox6.Visible = false;
            pictureBox5.Visible = false;
            textBox1.Visible = true;
            label12.Visible = true;
            label10.Visible = true;
            label13.Visible = true;
            label11.Visible = true;
            vCodigo.Visible = true;
            vReglas.Visible = true;
            dataGridView1.Visible = true;
            IconHTML.Visible = true;
            IconLimpiar.Visible = true;
            IconRegresar.Visible = true;
            IconGuardar.Visible = true;
            labelHTML.Visible = true;
            labelLimpiar.Visible = true;
            LabelRegresar.Visible = true;
            labelGuardar.Visible = true;
        }
        public void OcultarIconos() {
            label1.Visible = true;
            label2.Visible = true;
            pictureBox6.Visible = true;
            pictureBox5.Visible = true;
            textBox1.Visible = false;
            label12.Visible = false;
            label10.Visible = false;
            label13.Visible = false;
            label11.Visible = false;
            vCodigo.Visible = false;
            vReglas.Visible = false;
            dataGridView1.Visible = false;
            IconHTML.Visible = false;
            IconLimpiar.Visible = false;
            IconRegresar.Visible = false;
            IconGuardar.Visible = false;
            labelHTML.Visible = false;
            labelLimpiar.Visible = false;
            LabelRegresar.Visible = false;
            labelGuardar.Visible = false;
        }
        #endregion
        
        //Boton de regreso
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            MostrarIconos();
        }
        private void IconRegresar_Click(object sender, EventArgs e)
        {
            OcultarIconos();
        }
        //Apertura de archivo de reglas
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Abrir un archivo de reglas";
            theDialog.Filter = "Archivos REG|*.reg";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(theDialog.FileName))
                {
                    Int32 columns = 0;
                    Int32 rows = 0;
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        dataGridView1.Rows.Add();
                        String[] data = line.Split(';');
                        for (Int32 i = 0; i < data.Length; i++)
                        {
                            dataGridView1.Rows[rows].Cells[columns].Value = data[i];
                            columns++;
                        }
                        rows++;
                        columns = 0;
                    }
                }
                MostrarIconos();
            }
        }
        //Limpieza de los controles
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            this.dataGridView1.Rows.Clear();
            textBox1.Text = "";
        }
        //Apertura de archivo HTML
        private void IconHTML_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Abrir un archivo HTML";
            theDialog.Filter = "Archivos HTML|*.html";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string filename = theDialog.FileName;
                            textBox1.Text = File.ReadAllText(filename);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error all abrir archivo " + ex.Message);
                }
            }
        }
        //Guarda archivo de reglas
        private void IconGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Archivos REG|*.reg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                TextWriter sw = new StreamWriter(sfd.FileName);
                int rowcount = dataGridView1.Rows.Count;
                for (int i = 0; i < rowcount-1 ; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Columna4"].Value is null)
                    {
                        sw.WriteLine(dataGridView1.Rows[i].Cells["Columna1"].Value.ToString() + ";" + dataGridView1.Rows[i].Cells["Columna2"].Value.ToString() + ";" + dataGridView1.Rows[i].Cells["Columna3"].Value.ToString() + ";"+"NULL");
                    }
                    else {
                        sw.WriteLine(dataGridView1.Rows[i].Cells["Columna1"].Value.ToString() + ";" + dataGridView1.Rows[i].Cells["Columna2"].Value.ToString() + ";" + dataGridView1.Rows[i].Cells["Columna3"].Value.ToString() + ";" + dataGridView1.Rows[i].Cells["Columna4"].Value.ToString());
                    }
                }
                sw.Close();
                MessageBox.Show("Archivo creado","Axolotl ETL");
            }
        }
        private void formReglas_Load(object sender, EventArgs e)
        {

        }
    }
}
