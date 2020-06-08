using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace ETL_CAT
{
    public partial class formPrincipal : Form
    {
        public formPrincipal()
        {
            InitializeComponent();
        }
        #region //Manejo de Forms con herencia en panel
        //Permite mostrar forms dentro de paneles
        private void AddFormInPanel(Form fh)
        {
            if (this.panelContenedor.Controls.Count > 0)
                this.panelContenedor.Controls.RemoveAt(0);
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.panelContenedor.Controls.Add(fh);
            this.panelContenedor.Tag = fh;
            fh.Show();
        }
        //Carga el form de inicio
        private void Form1_Load(object sender, EventArgs e)
        {

            var form = Application.OpenForms.OfType<formInicio>().FirstOrDefault();
            formInicio hijo = form ?? new formInicio();
            AddFormInPanel(hijo);
        }
        //Muestra el form de inicio en el panel
        private void IconInicio_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<formInicio>().FirstOrDefault();
            formInicio hijo = form ?? new formInicio();
            AddFormInPanel(hijo);
        }
        //Muestra el form de inicio en el panel
        private void LabelInicio_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<formInicio>().FirstOrDefault();
            formInicio hijo = form ?? new formInicio();
            AddFormInPanel(hijo);
        }
        //Muestra el form de las reglas en el panel
        private void IconReglas_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<formReglas>().FirstOrDefault();
            formReglas hijo = form ?? new formReglas();
            AddFormInPanel(hijo);
        }
        //Muestra el form de las reglas en el panel
        private void LabelReglas_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<formReglas>().FirstOrDefault();
            formReglas hijo = form ?? new formReglas();
            AddFormInPanel(hijo);
        }
        //Muestra el form del flujo de información en el panel
        private void IconFlujo_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<formExportacion>().FirstOrDefault();
            formExportacion hijo = form ?? new formExportacion();
            AddFormInPanel(hijo);
        }
        //Muestra el form de conexiones en el panel
        private void IconConexiones_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<formConfiguracion>().FirstOrDefault();
            formConfiguracion hijo = form ?? new formConfiguracion();
            AddFormInPanel(hijo);
        }
        //Muestra el form de conexiones en el panel
        private void LabelConexiones_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<formConfiguracion>().FirstOrDefault();
            formConfiguracion hijo = form ?? new formConfiguracion();
            AddFormInPanel(hijo);
        }
        //Muestra el form del flujo de información en el panel
        private void LabelFlujo_Click(object sender, EventArgs e)
        {
            var form = Application.OpenForms.OfType<formExportacion>().FirstOrDefault();
            formExportacion hijo = form ?? new formExportacion();
            AddFormInPanel(hijo);
        }
        #endregion
        #region //Animación del menu
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            if (PanelI.Width == 50)
            {

                PanelI.Visible = false;
                PanelI.Width = 177;
                panel1.Width = 177;
                bunifuTransition2.ShowSync(label5);
                bunifuTransition1.ShowSync(panel1);
                bunifuTransition1.ShowSync(PanelI);
            }
            else
            {

                PanelI.Visible = false;
                PanelI.Width = 50;
                panel1.Width = 50;
                bunifuTransition2.Hide(label5);
                bunifuTransition1.ShowSync(panel1);
                bunifuTransition1.ShowSync(PanelI);
            }
        }
        #endregion
        #region //permite el cambio de colores de label al pasar el mouse
        private void LabelInicio_MouseHover(object sender, EventArgs e)
        {
            LabelInicio.ForeColor = System.Drawing.Color.DeepSkyBlue;
        }

        private void LabelInicio_MouseLeave(object sender, EventArgs e)
        {
            LabelInicio.ForeColor = System.Drawing.Color.White;
            LabelConexiones.ForeColor = Color.White;
            LabelReglas.ForeColor = Color.White;
            LabelFlujo.ForeColor = Color.White;
        }

        private void LabelReglas_MouseHover(object sender, EventArgs e)
        {
            LabelReglas.ForeColor = System.Drawing.Color.DeepSkyBlue;
        }

        private void LabelReglas_MouseLeave(object sender, EventArgs e)
        {
            LabelInicio.ForeColor = System.Drawing.Color.White;
            LabelConexiones.ForeColor = Color.White;
            LabelReglas.ForeColor = Color.White;
            LabelFlujo.ForeColor = Color.White;
        }

        private void LabelConexiones_MouseHover(object sender, EventArgs e)
        {
            LabelConexiones.ForeColor = System.Drawing.Color.DeepSkyBlue;
        }

        private void LabelConexiones_MouseLeave(object sender, EventArgs e)
        {
            LabelInicio.ForeColor = System.Drawing.Color.White;
            LabelConexiones.ForeColor = Color.White;
            LabelReglas.ForeColor = Color.White;
            LabelFlujo.ForeColor = Color.White;
        }

        private void LabelFlujo_MouseHover(object sender, EventArgs e)
        {
            LabelFlujo.ForeColor = System.Drawing.Color.DeepSkyBlue;
        }

        private void LabelFlujo_MouseLeave(object sender, EventArgs e)
        {
            LabelInicio.ForeColor = System.Drawing.Color.White;
            LabelConexiones.ForeColor = Color.White;
            LabelReglas.ForeColor = Color.White;
            LabelFlujo.ForeColor = Color.White;
        }
        #endregion

        //Cierra la aplicación
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        //Minimiza la aplicación
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
