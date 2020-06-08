using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ETL_CAT
{

    public partial class formExportacion : Form
    {
        DialogResult result;
        string linea, Servidor, NombreBD, UsuarioBD, PassBD;
        #region //Carga el directorio de archivos NoSQL
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ListaArchivos.Text = "";
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string file in files)
                    {
                        ListaArchivos.Text = ListaArchivos.Text + file + "\n";
                    }
                }
                MessageBox.Show("Archivos cargados correctamente.", "Axolotl ETL");
            }
        }
        private void pictureBox2_DragDrop(object sender, DragEventArgs e)
        {
            ListaArchivos.Text = "";
            string[] folders = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string folder in folders)
            {
                string[] files = Directory.GetFiles(folder);
                foreach (string file in files)
                {
                    ListaArchivos.Text = ListaArchivos.Text + file + "\n";
                }
                MessageBox.Show("Archivos cargados correctamente.", "Axolotl ETL");
            }
        }
        #endregion
        #region //carga el archivo de reglas
        private void pictureBox3_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                ListaReglas.Text = File.ReadAllText(file);
            }
            MessageBox.Show("Archivo cargado correctamente.", "Axolotl ETL");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ListaReglas.Text = "";
            result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                ListaReglas.Text = File.ReadAllText(openFileDialog1.FileName);
            }
            MessageBox.Show("Archivo cargado correctamente.", "Axolotl ETL");
        }
        #endregion
       
        //Carga el archivo de conexión
        public formExportacion()
        {
            InitializeComponent();
            //fichero de configuracion de servidor y base de datos
            string path = "Conect.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            if (File.Exists(path))
            {
                linea = file.ReadLine();
                string[] tokens = linea.Split(';');
                Servidor = tokens[0];
                NombreBD = tokens[1];
                UsuarioBD = tokens[2];
                PassBD = tokens[3];
            }
        }

        //Permite el drag&drop en el formulario
        private void formExportacion_Load(object sender, EventArgs e)
        {
            pictureBox2.AllowDrop = true;
            pictureBox3.AllowDrop = true;
        }
        private void pictureBox2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        private void pictureBox3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        //Selecciona el repositorio de destino de la información
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            result = MessageBox.Show("¿Desea exportar la información a un servidor SQL?", "Axolotl ETL",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            bool flag =false;
            System.Data.DataTable Tabla = new System.Data.DataTable("Tabla1");
            DataRow row = Tabla.NewRow();
            for (int i = 0; i <= ListaArchivos.Lines.Length - 2; i++)//for archivos
            {
                CodigoFuente.Text = File.ReadAllText(ListaArchivos.Lines[i]);
                string[] lineaCodigoFuente = CodigoFuente.Lines;
                for (int k = 0; k <= ListaReglas.Lines.Length - 1; k++)//for reglas
                {
                    int j = 0;
                    string[] Regla = ListaReglas.Lines[k].Split(';');
                    string DatoExtraer = Regla[0];
                    string ApartirDe = Regla[1];
                    string Columna = Regla[2];
                    string HastaEl = Regla[3];
                    foreach (string line in lineaCodigoFuente)//foreach de cada una de las lineas
                    {
                        if (line.Contains(DatoExtraer))
                        {
                            if (lineaCodigoFuente[j + 1].IndexOf(HastaEl) > 0)
                            {
                                if (i == 0)
                                {
                                    Tabla.Columns.Add(DatoExtraer);
                                    if (flag == false) { 
                                        for (int m = 0; m <= ListaArchivos.Lines.Length -2 ; m++) //for para la generación de filas
                                            Tabla.Rows.Add(i.ToString());
                                        flag = true;
                                        }
                                }
                                int final = lineaCodigoFuente[j + 1].IndexOf(HastaEl);
                                int comienzo = Int32.Parse(ApartirDe);
                                Tabla.Rows[i][DatoExtraer] = lineaCodigoFuente[j + 1].Substring(comienzo, final - comienzo);
                            }
                        }
                        j++;
                    }
                }
            }
            DataSet ds = new DataSet("Base de Datos");
            ds.Tables.Add(Tabla);
            if (result == DialogResult.Yes)
            {
                ExportDataSetToSQL(ds);
            }
            else if (result == DialogResult.No)
            {
                ExportDataSetToExcel(ds);
            }
            MessageBox.Show("Proceso terminado correctamente.", "Axolotl ETL");
        }
        //función para exportar el dataset a Excel
        private void ExportDataSetToExcel(DataSet ds)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
            foreach (System.Data.DataTable table in ds.Tables)
            {
                Worksheet excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet) excelWorkBook.Sheets.Add();
                excelWorkSheet.Name = table.TableName;

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }
            excelApp.Visible = true;
            excelWorkBook.Close();
            excelApp.Quit();
        }
        //función para exportar el dataset a Excel
        private void ExportDataSetToSQL(DataSet ds) {
            string connetionString = "Data Source=" + Servidor + "; Initial Catalog =" + NombreBD + ";User ID =" + UsuarioBD + "; Password =" + PassBD;
            SqlConnection cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlBulkCopy Exportar = default(SqlBulkCopy);
            Exportar = new SqlBulkCopy(cnn);
            Exportar.DestinationTableName = "DatosExportados";
            Exportar.WriteToServer(ds.Tables[0]);
            cnn.Close();
        }
    }
}
