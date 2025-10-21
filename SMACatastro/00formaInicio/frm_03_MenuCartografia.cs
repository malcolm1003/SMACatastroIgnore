using SMACatastro.catastroCartografia;
using System;
using System.Windows.Forms;

namespace SMACatastro.formaInicio
{
    public partial class frm_03_MenuCartografia : Form
    {
        public frm_03_MenuCartografia()
        {
            InitializeComponent();
        }


        private void cmdSalida_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.menuBotonBloqueo = 1;
        }
        private void cmdAltaCerCam_Click(object sender, EventArgs e)
        {
            Form formularioActual = Form.ActiveForm;
            formularioActual.Opacity = 0.70;
            formularioActual.Hide();
            catastroCartografia.frmCatastro01UbicacionAlta fs = new catastroCartografia.frmCatastro01UbicacionAlta();
            fs.ShowDialog();
            formularioActual.Show();
            formularioActual.Opacity = 1.0;
        }
        private void cmdCobroManual_Click(object sender, EventArgs e)
        {
            Form formularioActual = Form.ActiveForm;
            formularioActual.Opacity = 0.70;
            formularioActual.Hide();
            catastroCartografia.frmCatastro03BusquedaCatastro fs = new catastroCartografia.frmCatastro03BusquedaCatastro();
            fs.ShowDialog();
            formularioActual.Show();
            formularioActual.Opacity = 1.0;
        }

        private void cmdCobroOrdenes_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmCatastro04ReporteCartografia fs = new frmCatastro04ReporteCartografia();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }
    }
}
