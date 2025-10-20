//using SMACatastro.catastroRevision;
using SMACatastro.catastroCartografia;
using SMACatastro.catastroSistemas;
using System;
using System.Windows.Forms;

namespace SMACatastro.formaInicio
{
    public partial class frm_06_MenuSistemas : Form
    {
        public frm_06_MenuSistemas()
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
            frmMovimientosSistemas fs = new frmMovimientosSistemas();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCobroManual_Click(object sender, EventArgs e)
        {
            //frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            //frmTresenUno fs = new frmTresenUno();
            //fs.ShowDialog();
            ////fs.Show();

            //frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCobroOrdenes_Click(object sender, EventArgs e)
        {
            //frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            //frmBloqueoDesbloqueo fs = new frmBloqueoDesbloqueo();
            //fs.ShowDialog();
            ////fs.Show();
            //frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCortes_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmColonias fs = new frmColonias();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void btnCalles_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmCalles fs = new frmCalles();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void btnManzanas_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmManzanas fs = new frmManzanas();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void btnLocalidades_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmLocalidades fs = new frmLocalidades();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void btnAreasH_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmAreasHomogeneas fs = new frmAreasHomogeneas();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void btnImportacion_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmImportacion fs = new frmImportacion();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }
    }
}
