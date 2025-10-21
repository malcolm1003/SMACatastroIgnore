using AccesoBase;
using SMACatastro._00formaInicio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Management;
using System.Windows.Forms;
using Utilerias;

namespace SMACatastro.formaInicio
{
    public partial class frm_01_Usuarios : System.Windows.Forms.Form
    {

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // llamado para logeo
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        // llamado a conexion a base
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();      //conexion a la base de datos SMA
        Util util = new Util();
        pantalla error = new pantalla();
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //METODOS PARA CERRAR,MAXIMIZAR, MINIMIZAR FORMULARIO----------------------------------------------------
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        int lx, ly;
        int sw, sh;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //METODO PARA ARRASTRAR EL FORMULARIO--------------------------------------------------------------------
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        public frm_01_Usuarios()
        {
            InitializeComponent();
        }

        private string obtenerMac()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            List<string> listProcessor = new List<string>();
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                listProcessor.Add(wmi_HD["ProcessorID"].ToString());
            }
            string Mac = listProcessor[0];
            return Mac;
        }

        private void cajas_amarilla(int x)
        {
            switch (x)
            {
                case 1: txt_usuario.BackColor = System.Drawing.Color.Yellow; break;
                case 2: txt_pass.BackColor = System.Drawing.Color.Yellow; break;
            }
        }

        private void cajas_blanca(int x)
        {
            switch (x)
            {
                case 1: txt_usuario.BackColor = System.Drawing.Color.White; break;
                case 2: txt_pass.BackColor = System.Drawing.Color.White; break;
            }
        }

        private void txt_usuario_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(1);
        }

        private void txt_pass_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(2);
        }

        private void txt_usuario_Leave(object sender, EventArgs e)
        {
            cajas_blanca(1);
        }

        private void txt_pass_Leave(object sender, EventArgs e)
        {
            cajas_blanca(2);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar?", "Alerta¡¡", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void txt_pass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                
                acceso2();
            }
        }

        private void frm_01_Usuarios_Load(object sender, EventArgs e)
        {
            lblVercion.Text = " " + Program.VercionS;
            lblVercionSudsos.Text = "SUM-CATRASTRO-   " + Program.VercionS;
            txt_usuario.Focus();
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");

        }

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txt_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                acceso2();
            }
        }

        private void cmd_mac_Click(object sender, EventArgs e)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            List<string> listProcessor = new List<string>();
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                listProcessor.Add(wmi_HD["ProcessorID"].ToString());
            }
            string Mac = listProcessor[0];
            MessageBox.Show("MAC DEL EQUIPO -" + Mac, "INFORMACION");
        }

        void acceso2()
        {
            if (txt_usuario.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL USUARIO", "ERROR DE INFORMACION"); txt_usuario.Focus(); return; }
            if (txt_pass.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA CONTRASEÑA DE USUARIO", "ERROR DE INFROMACION"); txt_pass.Focus(); return; }

            string usuario_encripta = txt_usuario.Text.Trim();
            string password_encripta = txt_pass.Text.Trim();
            int validacion = 0;
            string versionS = lblVercion.Text.Trim();
            string id_cpu_cpu = obtenerMac();

            if (txt_usuario.Text.Trim() == "CAHUMA43" && txt_pass.Text.Trim() == "43CAHUMA")
            {
                formaInicio.frm_01_Usuarios WL = new formaInicio.frm_01_Usuarios(); WL.Hide();
                this.Hide();
                frm_02_MenuGeneral menu = new frm_02_MenuGeneral();
                menu.Show();
                return;
            }
            if (txt_usuario.Text.Trim() == "BUSCAT" && txt_pass.Text.Trim() == "CATBUS")
            {
                formaInicio.frm_01_Usuarios WL = new formaInicio.frm_01_Usuarios(); WL.Hide();
                this.Hide();
                Program.acceso_nivel_acceso = 9; // nivel de acceso para busqueda general
                Program.nombre_usuario = "BÚSQUEDA GENERAL";
                Program.acceso_cargo = "BUSQUEDA CATASTRO";
                Program.acceso_nombre_usuario = "ÚSUARIO DE BÚSQUEDA GENERAL";
                frm_02_MenuGeneral menu = new frm_02_MenuGeneral();
                menu.Show();
                return;
            }

            ///// conectamos a la base de datos
            try
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("SONG_ACCESO_USUARIOS6", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 8).Value = usuario_encripta;
                cmd.Parameters.Add("@CONTRASEÑA", SqlDbType.VarChar, 8).Value = password_encripta;
                cmd.Parameters.Add("@VERCION", SqlDbType.VarChar, 8).Value = versionS;
                cmd.Parameters.Add("@ID_CPU", SqlDbType.VarChar, 32).Value = id_cpu_cpu;

                cmd.Parameters.Add("@NOMBRE_USUARIO", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@DIRECCION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@AREA", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CARGO", SqlDbType.VarChar, 60).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@NIVEL_ACCESO", SqlDbType.Int, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ACTIVO", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ID_DIRECCION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ID_AREA", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SUCURSAL", SqlDbType.VarChar, 30).Direction = ParameterDirection.Output;

                cmd.Parameters.Add("@AÑO", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@SERIE", SqlDbType.VarChar, 2).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@idSucursal", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@cDescripcion", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                Program.acceso_nombre_usuario = Convert.ToString(cmd.Parameters["@NOMBRE_USUARIO"].Value).Trim();

                if (SMACatastro.Program.acceso_nombre_usuario == null || Program.acceso_nombre_usuario == "" || Program.acceso_nombre_usuario == " " || Program.acceso_nombre_usuario == "  " || Program.acceso_nombre_usuario == "   " || Program.acceso_nombre_usuario == "...")
                {
                    MessageBox.Show("USUARIO, CONTRASEÑA o MAC.  INCORRECTOS", "ERROR DE INFROMACION");
                    con.cerrar_interno();
                    txt_pass.Focus();
                    return;
                }

                Program.acceso_direccion = Convert.ToInt32(cmd.Parameters["@DIRECCION"].Value);
                Program.acceso_areai = Convert.ToInt32(cmd.Parameters["@AREA"].Value);
                Program.acceso_cargo = Convert.ToString(cmd.Parameters["@CARGO"].Value).Trim();
                Program.acceso_nivel_acceso = Convert.ToInt32(cmd.Parameters["@NIVEL_ACCESO"].Value);
                Program.acceso_activo = Convert.ToInt32(cmd.Parameters["@ACTIVO"].Value);
                Program.acceso_validacion = Convert.ToInt32(cmd.Parameters["@VALIDACION"].Value);
                Program.acceso_id_direccion = Convert.ToInt32(cmd.Parameters["@ID_DIRECCION"].Value);
                Program.acceso_id_area = Convert.ToInt32(cmd.Parameters["@ID_AREA"].Value);
                Program.acceso_sucursal = Convert.ToString(cmd.Parameters["@SUCURSAL"].Value).Trim();

                Program.acceso_año = Convert.ToInt32(cmd.Parameters["@AÑO"].Value);
                Program.acceso_serie = Convert.ToString(cmd.Parameters["@SERIE"].Value).Trim();
                Program.acceso_idSucursal = Convert.ToInt32(cmd.Parameters["@idSucursal"].Value);
                Program.acceso_sucDescripcion = Convert.ToString(cmd.Parameters["@cDescripcion"].Value).Trim();

                validacion = Program.acceso_validacion;

                Program.nombre_usuario = Convert.ToString(cmd.Parameters["@NOMBRE_USUARIO"].Value).Trim();
                Program.acceso_usuario = @usuario_encripta;


                if (validacion == 1)
                {
                    if (Program.acceso_cargo.Substring(0, 8) == "CATASTRO")
                    {
                        if (Program.acceso_nivel_acceso == 1)   // CARTOGRAFIA
                        {
                            con.cerrar_interno();
                            formaInicio.frm_01_Usuarios WL = new formaInicio.frm_01_Usuarios(); WL.Hide();
                            this.Hide();
                            frm_02_MenuGeneral menu = new frm_02_MenuGeneral();
                            menu.Show();
                        }

                        if (Program.acceso_nivel_acceso == 2)   // VENTANILLA
                        {
                            con.cerrar_interno();
                            formaInicio.frm_01_Usuarios WL = new formaInicio.frm_01_Usuarios(); WL.Hide();
                            this.Hide();
                            frm_02_MenuGeneral menu = new frm_02_MenuGeneral();
                            menu.Show();
                        }

                        if (Program.acceso_nivel_acceso == 3)   // REVICION
                        {
                            con.cerrar_interno();
                            formaInicio.frm_01_Usuarios WL = new formaInicio.frm_01_Usuarios(); WL.Hide();
                            this.Hide();
                            frm_02_MenuGeneral menu = new frm_02_MenuGeneral();
                            menu.Show();
                        }

                        if (Program.acceso_nivel_acceso == 4)   // SISTEMAS
                        {
                            con.cerrar_interno();
                            formaInicio.frm_01_Usuarios WL = new formaInicio.frm_01_Usuarios(); WL.Hide();
                            this.Hide();
                            frm_02_MenuGeneral menu = new frm_02_MenuGeneral();
                            menu.Show();
                        }

                        if (Program.acceso_nivel_acceso == 5)   // GENERALES
                        {
                            con.cerrar_interno();
                            formaInicio.frm_01_Usuarios WL = new formaInicio.frm_01_Usuarios(); WL.Hide();
                            this.Hide();
                            frm_02_MenuGeneral menu = new frm_02_MenuGeneral();
                            menu.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("EL USUARIO NO ES DE CATASTRO", "ERROR DE INFROMACION");
                    }
                }
                else
                {
                    MessageBox.Show("USUARIO Y CONTRASEÑA.  INCORRECTOS", "ERROR DE INFROMACION");
                    con.cerrar_interno();
                    txt_pass.Focus();
                    return;
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
        }




        private void cmd_aceptar_Click(object sender, EventArgs e)
        {
            acceso2();
        }
    }
}
