using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SMACatastro.formaInicio
{
    public partial class frm_02_MenuGeneral : Form
    {
        public frm_02_MenuGeneral()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO  TIEMPO DE EJECUCION
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private int tolerance = 15;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelContenedorPrincipal.Region = region;
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(55, 61, 69));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // METODO PARA ARRASTRAR EL FORMULARIO
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // METODOS PARA CERRAR,MAXIMIZAR, MINIMIZAR FORMULARIO
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        int lx, ly;
        int sw, sh;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // METODO PARA ABRIR FORM DENTRO DE PANEL
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void AbrirFormEnPanel(object formHijo)
        {
            if (this.panelContenedorForm.Controls.Count > 0)
                this.panelContenedorForm.Controls.RemoveAt(0);
            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.panelContenedorForm.Controls.Add(fh);
            this.panelContenedorForm.Tag = fh;
            fh.Show();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // METODO PARA ABRIR EL FORMULARIO DONDE SE ENCUENTRA EL LOGO 2
        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MostrarFormLogo()
        {
            AbrirFormEnPanel(new SMAIngresos.formaInicio.frm_00_Logo());
            //label3.Text = "Nombre Usuario: " + Program.acceso_usuario;
            //label5.Text = "Cargo: Caja N° " + Program.acceso_cargo;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // METODO PARA BLOQUEAR BOTONES
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void bloquear_boton()
        {
            btnCartografia.Enabled = false;
            btnVentanilla.Enabled = false;
            btnRevision.Enabled = false;
            btnSistemas.Enabled = false;
            btnGenerales.Enabled = false;
            btnSoporte.Enabled = false;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void btnMenu_Click(object sender, EventArgs e)
        {
            //-------CON EFECTO SLIDING
            if (panelMenu.Width == 230)
            {
                this.tmContraerMenu.Start();
            }
            else if (panelMenu.Width == 55)
            {
                this.tmExpandirMenu.Start();
            }
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");

            if (Program.menuBotonBloqueo == 1)
            {
                if (Program.acceso_nivel_acceso == 1)   //CARTOGRAFIA
                {
                    label1.Text = "- CATASTRO - CARTOGRAFIA   " + Program.VercionS;
                    lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                    btnCartografia.Enabled = true;
                    btnVentanilla.Enabled = false;
                    btnRevision.Enabled = false;
                    btnGenerales.Enabled = true;
                    btnSistemas.Enabled = false;
                    btnSoporte.Enabled = false;

                    btnSalir.Enabled = true;
                    btnSalir.BackColor = Color.FromArgb(237, 181, 17);
                }
                else if (Program.acceso_nivel_acceso == 2)   //VENTANILLA
                {
                    label1.Text = "- CATASTRO - VENTANILLA   " + Program.VercionS;
                    lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                    btnCartografia.Enabled = false;
                    btnVentanilla.Enabled = true;
                    btnRevision.Enabled = false;
                    btnGenerales.Enabled = true;
                    btnSistemas.Enabled = false;
                    btnSoporte.Enabled = false;

                    btnSalir.Enabled = true;
                    btnSalir.BackColor = Color.FromArgb(237, 181, 17);
                }
                else if (Program.acceso_nivel_acceso == 3)   //REVISION
                {
                    label1.Text = "- CATASTRO - REVISION   " + Program.VercionS;
                    lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                    btnCartografia.Enabled = false;
                    btnVentanilla.Enabled = false;
                    btnRevision.Enabled = true;
                    btnGenerales.Enabled = true;
                    btnSistemas.Enabled = false;
                    btnSoporte.Enabled = false;

                    btnSalir.Enabled = true;
                    btnSalir.BackColor = Color.FromArgb(237, 181, 17);
                }
                else if (Program.acceso_nivel_acceso == 4)   //SISTEMAS
                {
                    label1.Text = "- CATASTRO - SISTEMAS   " + Program.VercionS;
                    lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                    btnCartografia.Enabled = false;
                    btnVentanilla.Enabled = false;
                    btnRevision.Enabled = false;
                    btnGenerales.Enabled = true;
                    btnSistemas.Enabled = true;
                    btnSoporte.Enabled = false;

                    btnSalir.Enabled = true;
                    btnSalir.BackColor = Color.FromArgb(237, 181, 17);
                }
                else if (Program.acceso_nivel_acceso == 9)
                {
                    
                    label1.Text = "- CATASTRO - GENERAL   " + Program.VercionS;
                    lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                    btnCartografia.Enabled = false;
                    btnVentanilla.Enabled = false;
                    btnRevision.Enabled = false;
                    btnGenerales.Enabled = true;
                    btnSistemas.Enabled = false;
                    btnSoporte.Enabled = false;
                }
                else
                {

                    label1.Text = "- CATASTRO - SUPERVISOR   " ;
                    lblCargoUsuarioMenu.Text = " PROYECTO ";
                    btnCartografia.Enabled = true;
                    btnVentanilla.Enabled = true;
                    btnRevision.Enabled = true;
                    btnGenerales.Enabled = true;
                    btnSistemas.Enabled = true;
                    btnSoporte.Enabled = true;

                    btnSalir.Enabled = true;
                    btnSalir.BackColor = Color.FromArgb(237, 181, 17);
                }
                //Font fuente = new Font(label1.Font.FontFamily, 12);
                //label1.Font = fuente;

                //-------CON EFECTO SLIDING
                if (panelMenu.Width == 55)
                {
                    this.tmExpandirMenu.Start();
                }
            }

        }

        private void tmContraerMenu_Tick(object sender, EventArgs e)
        {
            if (panelMenu.Width <= 55)
                this.tmContraerMenu.Stop();
            else
                panelMenu.Width = panelMenu.Width - 5;
        }

        private void frm_02_MenuGeneral_Load(object sender, EventArgs e)
        {
            lblVercionSudsos.Text = Program.VercionS;

            MostrarFormLogo();
            lblNombreUsuarioMenu.Text = Program.acceso_nombre_usuario;
            lblCargoUsuarioMenu.Text = Program.acceso_cargo;

            //----------------------------------------------------------------------------------------------------------//
            // empesamos a revisar los niveles de acceso                                                                //
            //----------------------------------------------------------------------------------------------------------//

            if (Program.acceso_nivel_acceso == 1)       //  CARTOGRAFIA
            {
                label1.Text = "- CATASTRO - CARTOGRAFIA   " + Program.VercionS;
                lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                btnCartografia.Enabled = true;
                btnVentanilla.Enabled = false;
                btnRevision.Enabled = false;
                btnSistemas.Enabled = false;
                btnGenerales.Enabled = true;
                btnSoporte.Enabled = false;

                btnSalir.Enabled = true;
                btnSalir.BackColor = Color.FromArgb(237, 181, 17);
            }
            else if (Program.acceso_nivel_acceso == 2)   //VENTANILLA
            {
                label1.Text = "- CATASTRO - VENTANILLA   " + Program.VercionS;
                lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                btnCartografia.Enabled = false;
                btnVentanilla.Enabled = true;
                btnRevision.Enabled = false;
                btnGenerales.Enabled = true;
                btnSistemas.Enabled = false;
                btnSoporte.Enabled = false;

                btnSalir.Enabled = true;
                btnSalir.BackColor = Color.FromArgb(237, 181, 17);
            }
            else if (Program.acceso_nivel_acceso == 3)   //REVISION
            {
                label1.Text = "- CATASTRO - REVISION   " + Program.VercionS;
                lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                btnCartografia.Enabled = false;
                btnVentanilla.Enabled = false;
                btnRevision.Enabled = true;
                btnGenerales.Enabled = true;
                btnSistemas.Enabled = false;
                btnSoporte.Enabled = false;

                btnSalir.Enabled = true;
                btnSalir.BackColor = Color.FromArgb(237, 181, 17);
            }
            else if (Program.acceso_nivel_acceso == 4)   //SISTEMAS
            {
                label1.Text = "- CATASTRO - SISTEMAS   " + Program.VercionS;
                lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                btnCartografia.Enabled = false;
                btnVentanilla.Enabled = false;
                btnRevision.Enabled = false;
                btnGenerales.Enabled = true;
                btnSistemas.Enabled = true;
                btnSoporte.Enabled = false;

                btnSalir.Enabled = true;
                btnSalir.BackColor = Color.FromArgb(237, 181, 17);
            }
            else if (Program.acceso_nivel_acceso == 9)   //general
            {
                label1.Text = "- CATASTRO - BÚSQUEDAS   " + Program.VercionS;
                lblCargoUsuarioMenu.Text = " " + Program.acceso_cargo;
                btnCartografia.Enabled = false;
                btnVentanilla.Enabled = false;
                btnRevision.Enabled = false;
                btnGenerales.Enabled = true;
                btnSistemas.Enabled = false;
                btnSoporte.Enabled = false;

                btnSalir.Enabled = true;
                btnSalir.BackColor = Color.FromArgb(237, 181, 17);
            }
            else
            {

                label1.Text = "- CATASTRO - SUPERVISOR   ";
                lblCargoUsuarioMenu.Text = " PROYECTO ";
                btnCartografia.Enabled = true;
                btnVentanilla.Enabled = true;
                btnRevision.Enabled = true;
                btnGenerales.Enabled = true;
                btnSistemas.Enabled = true;
                btnSoporte.Enabled = true;

                btnSalir.Enabled = true;
                btnSalir.BackColor = Color.FromArgb(237, 181, 17);
            }
            Program.menuBotonBloqueo = 0;

        }

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar?", "Alerta¡¡", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnContrato_Click(object sender, EventArgs e)
        {
            Program.menuBotonBloqueo = 0;
            if (panelMenu.Width == 230)
            {
                this.tmContraerMenu.Start();
            }
            else if (panelMenu.Width == 55)
            {
                //this.tmExpandirMenu.Start();
            }
            label1.Text = "- CATASTRO - CARTOGRAFIA";
            //Font fuente = new Font(label1.Font.FontFamily, 20);
            //label1.Font = fuente;

            bloquear_boton();

            SMACatastro.formaInicio.frm_03_MenuCartografia frm = new SMACatastro.formaInicio.frm_03_MenuCartografia();
            frm.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            AbrirFormEnPanel(frm);
        }

        private void tmExpandirMenu_Tick(object sender, EventArgs e)
        {
            if (panelMenu.Width >= 230)
                this.tmExpandirMenu.Stop();
            else
                panelMenu.Width = panelMenu.Width + 5;
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelContenedorForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            Program.menuBotonBloqueo = 0;
            if (panelMenu.Width == 230)
            {
                this.tmContraerMenu.Start();
            }
            else if (panelMenu.Width == 55)
            {
                //this.tmExpandirMenu.Start();
            }
            label1.Text = "- CATASTRO - REVISION";
            //Font fuente = new Font(label1.Font.FontFamily, 20);
            //label1.Font = fuente;

            bloquear_boton();

            SMACatastro.formaInicio.frm_05_MenuRevision frm = new SMACatastro.formaInicio.frm_05_MenuRevision();
            frm.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            AbrirFormEnPanel(frm);


        }

        private void btnCalculos_Click(object sender, EventArgs e)
        {
            Program.menuBotonBloqueo = 0;
            if (panelMenu.Width == 230)
            {
                this.tmContraerMenu.Start();
            }
            else if (panelMenu.Width == 55)
            {
                //this.tmExpandirMenu.Start();
            }
            label1.Text = "- CATASTRO - VENTANILLA";
            //Font fuente = new Font(label1.Font.FontFamily, 20);
            //label1.Font = fuente;

            bloquear_boton();

            SMACatastro.formaInicio.frm_04_MenuVentanilla frm = new SMACatastro.formaInicio.frm_04_MenuVentanilla();
            frm.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            AbrirFormEnPanel(frm);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            Program.menuBotonBloqueo = 0;
            if (panelMenu.Width == 230)
            {
                this.tmContraerMenu.Start();
            }
            else if (panelMenu.Width == 55)
            {
                //this.tmExpandirMenu.Start();
            }
            label1.Text = "- CATASTRO - SISTEMAS";
            //Font fuente = new Font(label1.Font.FontFamily, 20);
            //label1.Font = fuente;

            bloquear_boton();

            SMACatastro.formaInicio.frm_06_MenuSistemas frm = new SMACatastro.formaInicio.frm_06_MenuSistemas();
            frm.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            AbrirFormEnPanel(frm);
        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            Program.menuBotonBloqueo = 0;
            if (panelMenu.Width == 230)
            {
                this.tmContraerMenu.Start();
            }
            else if (panelMenu.Width == 55)
            {
                //this.tmExpandirMenu.Start();
            }
            label1.Text = "- CATASTRO - GENERAL";
            //Font fuente = new Font(label1.Font.FontFamily, 20);
            //label1.Font = fuente;

            bloquear_boton();

            frm_07_MenuGeneral frm = new frm_07_MenuGeneral();
            frm.FormClosed += new FormClosedEventHandler(MostrarFormLogoAlCerrarForms);
            AbrirFormEnPanel(frm);
        }

        private void btnSoporte_Click(object sender, EventArgs e)
        {

        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // METODO PARA ABRIR EL FORMULARIO DONDE SE ENCUENTRA EL LOGO 1
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void MostrarFormLogoAlCerrarForms(object sender, FormClosedEventArgs e)
        {
            MostrarFormLogo();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
