using SMACatastro.catastroRevision;
using SMAIngresos.Catastro;
using System;
using System.Windows.Forms;

namespace SMACatastro.formaInicio
{
    public partial class frm_04_MenuVentanilla : Form
    {
        public frm_04_MenuVentanilla()
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
            frmVentanilla fs = new frmVentanilla();
            fs.ShowDialog();
            //fs.Show();

            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }

        private void cmdOrdenPago_Click(object sender, EventArgs e)
        {
            frm_03_MenuCartografia.ActiveForm.Opacity = 0.70;
            frm_01_OrdenPagoCatastro fs = new frm_01_OrdenPagoCatastro();
            fs.ShowDialog();
            //fs.Show();
            frm_02_MenuGeneral.ActiveForm.Opacity = 1.0;
        }
    }
}
