using SMACatastro.catastroRevision;
using SMACatastro.catastroSistemas;
using System;
using System.Windows.Forms;

namespace SMACatastro.formaInicio
{
    public partial class frm_05_MenuRevision : Form
    {
        public frm_05_MenuRevision()
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
            frmAutorizacionTramites fs = new frmAutorizacionTramites();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCobroManual_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmTresenUno fs = new frmTresenUno();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCobroOrdenes_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmBloqueoDesbloqueo fs = new frmBloqueoDesbloqueo();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdCortes_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmCatastroCambioNombre fs = new frmCatastroCambioNombre();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void btnCambioCveCat_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmCambiosClveCat fs = new frmCambiosClveCat();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void btnCambioColonia_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frmColoniaPorClave fs = new frmColoniaPorClave();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }
    }
}
