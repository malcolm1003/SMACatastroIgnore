using System;
using System.Windows.Forms;

namespace SMACatastro.formaInicio
{
    public partial class frm_00_Inicio : Form
    {
        public frm_00_Inicio()
        {
            InitializeComponent();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            formaInicio.frm_01_Usuarios fs = new formaInicio.frm_01_Usuarios();
            fs.ShowDialog();
        }

        private void frm_00_Inicio_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            formaInicio.frm_01_Usuarios fs = new formaInicio.frm_01_Usuarios();
            fs.ShowDialog();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            formaInicio.frm_01_Usuarios fs = new formaInicio.frm_01_Usuarios();
            fs.ShowDialog();
        }

        private void label2_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            formaInicio.frm_01_Usuarios fs = new formaInicio.frm_01_Usuarios();
            fs.ShowDialog();
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            formaInicio.frm_01_Usuarios fs = new formaInicio.frm_01_Usuarios();
            fs.ShowDialog();
        }

        private void frm_00_Inicio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Hide();
                formaInicio.frm_01_Usuarios fs = new formaInicio.frm_01_Usuarios();
                fs.ShowDialog();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
