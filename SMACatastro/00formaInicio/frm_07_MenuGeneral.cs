//using SMACatastro.catastroRevision;
using SMACatastro.catastroCartografia;
using SMACatastro.catastroRevision;
using System;
using System.Windows.Forms;

namespace SMACatastro.formaInicio
{
    public partial class frm_07_MenuGeneral : Form
    {
        public frm_07_MenuGeneral()
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
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmConsultaProcesos fs = new frmConsultaProcesos();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCobroManual_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmCatastro04ReporteCartografia fs = new frmCatastro04ReporteCartografia();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCobroOrdenes_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmCatastro03BusquedaCatastro fs = new frmCatastro03BusquedaCatastro();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCortes_Click(object sender, EventArgs e)
        {
            //frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            //frmColonias fs = new frmColonias();
            //fs.ShowDialog();
            ////fs.Show();
            //frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void btnCalles_Click(object sender, EventArgs e)
        {
            //frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            //frmCalles fs = new frmCalles();
            //fs.ShowDialog();
            ////fs.Show();
            //frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

      
    }
}
