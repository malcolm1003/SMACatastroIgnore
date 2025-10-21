using AccesoBase;
using GMap.NET.MapProviders;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Utilerias;
using Font = System.Drawing.Font;





namespace SMACatastro.catastroCartografia
{
    public partial class frmCatastro01UbicacionAlta : Form
    {

        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();      //conexion a la base de sapase
        Util util = new Util();
        String serie = Program.SerieC;
        String CONSTRUCCION;
        int variablePorSiCopiaLatitud = 0;


        public frmCatastro01UbicacionAlta()
        {
            InitializeComponent();
        }

        private void frmCatastro01UbicacionAlta_Load(object sender, EventArgs e)
        {
            inicio2();

            //inicio();
        }

        private void inicio2()   /// para iniciar el formulario desde el menu de inicio del tab index
        {
            limpiarTodoAlta();
            inabilitarBot();
            mtcInformacion.SelectedIndex = 0; // inicio de todo.
            inabilitarCajasInicio();


            txtMun.Text = Program.Vmunicipio;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;
            lblSerie.Text = Program.SerieC.ToString().Trim();
            label27.Text = "Usuario: " + Program.nombre_usuario.Trim();
            txtMun.Text = Program.Vmunicipio;
            variablePorSiCopiaLatitud = 0;
        }


        private void inicio()
        {
            cancelartodo();
            limpiarTodoAlta();
            llenarCombos1erVercion();

            lblSerie.Text = "";
            label27.Text = "Usuario: " + Program.nombre_usuario.Trim();
            txtZona.Focus();
        }



        private void cancelartodo()
        {
            inabilitarBot();
            inabilitarCajasInicio();

            pnlDatosPredio.Visible = true;
            pnlDatosPredio.Enabled = true;

            btnConsulta.Enabled = true;
            cmdSalida.Enabled = true;
            btnCancelar.Enabled = true;

            txtMun.Enabled = true;
            txtZona.Enabled = true;
            txtMzna.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;
        }


        private void inabilitarBot()
        {
            btnConsulta.Enabled = false;
            btnGuardar.Enabled = false;
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = false;
            cmdSalida.Enabled = false;
            btnMinimizar.Enabled = false;

            btnZonaOrigen.Enabled = false;
            btnConstLote.Enabled = false;
            btnConstComun.Enabled = false;
            btnRefresh.Enabled = false;
            btnMaps.Enabled = false;
        }

        private void inabilitarCajasInicio()
        {
            txtMun.Enabled = false;
            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;

            txtZonaOrigen.Enabled = false;
            txtCalle.Enabled = false;
            cboCalles.Enabled = false;

            cboRegimenPropiedad.Enabled = false;
            cboUbicacion.Enabled = false;

            txtSupTerreno.Enabled = false;
            txtSupComun.Enabled = false;
            txtSupCont.Enabled = false;
            txtSupContComn.Enabled = false;
            txtDesnivel.Enabled = false;
            txtAreaInscripta.Enabled = false;
            txtFrente.Enabled = false;
            txtFondo.Enabled = false;
            txtLatitud.Enabled = true;
            txtLongitud.Enabled = true;
            txtObservaciones.Enabled = false;
        }


        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void cajas_amarilla(int x)
        {
            switch (x)
            {
                case 1: txtZonaOrigen.BackColor = System.Drawing.Color.Yellow; break;
                case 2: txtCalle.BackColor = System.Drawing.Color.Yellow; break;
                case 3: cboCalles.BackColor = System.Drawing.Color.Yellow; break;
                case 4: cboRegimenPropiedad.BackColor = System.Drawing.Color.Yellow; break;
                case 5: cboUbicacion.BackColor = System.Drawing.Color.Yellow; break;
                case 6: txtSupTerreno.BackColor = System.Drawing.Color.Yellow; break;
                case 7: txtSupCont.BackColor = System.Drawing.Color.Yellow; break;
                case 8: txtSupComun.BackColor = System.Drawing.Color.Yellow; break;
                case 9: txtSupContComn.BackColor = System.Drawing.Color.Yellow; break;
                case 10: btnConstComun.BackColor = System.Drawing.Color.Yellow; break;
                case 11: txtFrente.BackColor = System.Drawing.Color.Yellow; break;
                case 12: txtFondo.BackColor = System.Drawing.Color.Yellow; break;
                case 13: txtDesnivel.BackColor = System.Drawing.Color.Yellow; break;
                case 14: txtAreaInscripta.BackColor = System.Drawing.Color.Yellow; break;

                case 15: txtZona.BackColor = System.Drawing.Color.Yellow; break;
                case 16: txtMzna.BackColor = System.Drawing.Color.Yellow; break;
                case 17: txtLote.BackColor = System.Drawing.Color.Yellow; break;
                case 18: txtEdificio.BackColor = System.Drawing.Color.Yellow; break;
                case 19: txtDepto.BackColor = System.Drawing.Color.Yellow; break;

                case 20: txtObservaciones.BackColor = System.Drawing.Color.Yellow; break;
            }
        }

        private void cajas_blancas(int x)
        {
            switch (x)
            {
                case 1: txtZonaOrigen.BackColor = System.Drawing.Color.White; break;
                case 2: txtCalle.BackColor = System.Drawing.Color.White; break;
                case 3: cboCalles.BackColor = System.Drawing.Color.White; break;
                case 4: cboRegimenPropiedad.BackColor = System.Drawing.Color.White; break;
                case 5: cboUbicacion.BackColor = System.Drawing.Color.White; break;
                case 6: txtSupTerreno.BackColor = System.Drawing.Color.White; break;
                case 7: txtSupCont.BackColor = System.Drawing.Color.White; break;
                case 8: txtSupComun.BackColor = System.Drawing.Color.White; break;
                case 9: txtSupContComn.BackColor = System.Drawing.Color.White; break;
                case 10: btnConstComun.BackColor = System.Drawing.Color.White; break;
                case 11: txtFrente.BackColor = System.Drawing.Color.White; break;
                case 12: txtFondo.BackColor = System.Drawing.Color.White; break;
                case 13: txtDesnivel.BackColor = System.Drawing.Color.White; break;
                case 14: txtAreaInscripta.BackColor = System.Drawing.Color.White; break;

                case 15: txtZona.BackColor = System.Drawing.Color.White; break;
                case 16: txtMzna.BackColor = System.Drawing.Color.White; break;
                case 17: txtLote.BackColor = System.Drawing.Color.White; break;
                case 18: txtEdificio.BackColor = System.Drawing.Color.White; break;
                case 19: txtDepto.BackColor = System.Drawing.Color.White; break;

                case 20: txtObservaciones.BackColor = System.Drawing.Color.White; break;
            }
        }


        //METODO PARA ARRASTRAR EL FORMULARIO-----------------------------------------------------------------------------------------------
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //METODOS PARA CERRAR,MAXIMIZAR, MINIMIZAR FORMULARIO-------------------------------------------------------------------------------
        int lx, ly;
        int sw, sh;

        private void txtMzna_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(16);
        }

        private void txtMzna_Leave(object sender, EventArgs e)
        {
            cajas_blancas(16);
        }

        private void txtLote_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(17);
        }

        private void txtLote_Leave(object sender, EventArgs e)
        {
            cajas_blancas(17);
        }

        private void txtEdificio_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(18);
        }

        private void txtEdificio_Leave(object sender, EventArgs e)
        {
            cajas_blancas(18);
        }

        private void txtDepto_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(19);
        }

        private void txtDepto_Leave(object sender, EventArgs e)
        {
            cajas_blancas(19);
        }

        private void txtZonaOrigen_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(1);
            txtCalle.Text = "";
            cboCalles.Items.Clear();
        }

        private void txtZonaOrigen_Leave(object sender, EventArgs e)
        {
            cajas_blancas(1);
        }

        private void txtCalle_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(2);
        }

        private void txtCalle_Leave(object sender, EventArgs e)
        {
            cajas_blancas(2);
        }

        private void cboCalles_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(3);
        }

        private void cboCalles_Leave(object sender, EventArgs e)
        {
            cajas_blancas(3);
        }

        private void cboRegimenPropiedad_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(4);
        }

        private void cboRegimenPropiedad_Leave(object sender, EventArgs e)
        {
            cajas_blancas(4);
        }

        private void cboUbicacion_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(5);
        }

        private void cboUbicacion_Leave(object sender, EventArgs e)
        {
            cajas_blancas(5);
        }

        private void txtSupTerreno_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(6);
        }

        private void txtSupTerreno_Leave(object sender, EventArgs e)
        {
            cajas_blancas(6);
            double d = 0.00;

            if (txtSupTerreno.Text.Trim() != "")
            {
                d = Convert.ToDouble(txtSupTerreno.Text);
            }

            txtSupTerreno.Text = d.ToString("###,###,###,##0.##");
        }

        private void txtSupCont_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(7);
        }

        private void txtSupCont_Leave(object sender, EventArgs e)
        {
            cajas_blancas(7);

            double d = Convert.ToDouble(txtSupCont.Text);
            txtSupCont.Text = d.ToString("###,###,###,##0.##");
        }

        private void txtSupComun_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(8);
        }

        private void txtSupComun_Leave(object sender, EventArgs e)
        {
            cajas_blancas(8);
            if (txtSupComun.Text.Trim() != "")
            {
                double d = Convert.ToDouble(txtSupComun.Text);
                txtSupComun.Text = d.ToString("###,###,###,##0.##");
            }
        }

        private void txtSupContComn_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(9);
        }

        private void txtSupContComn_Leave(object sender, EventArgs e)
        {
            cajas_blancas(9);
            if (txtSupContComn.Text.Trim() != "")
            {
                double d = Convert.ToDouble(txtSupContComn.Text);
                txtSupContComn.Text = d.ToString("###,###,###,##0.##");
            }
        }

        private void txtFrente_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(11);
        }

        private void txtFrente_Leave(object sender, EventArgs e)
        {
            cajas_blancas(11);

            if (txtFrente.Text.Trim() != "")
            {
                double d = Convert.ToDouble(txtFrente.Text);
                txtFrente.Text = d.ToString("###,###,###,##0.##");
            }
        }

        private void txtFondo_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(12);
        }

        private void txtFondo_Leave(object sender, EventArgs e)
        {
            cajas_blancas(12);
            if (txtFondo.Text.Trim() != "")
            {
                double d = Convert.ToDouble(txtFondo.Text);
                txtFondo.Text = d.ToString("###,###,###,##0.##");
            }
        }

        private void txtDesnivel_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(13);
        }

        private void txtDesnivel_Leave(object sender, EventArgs e)
        {
            cajas_blancas(13);

            if (txtDesnivel.Text.Trim() != "")
            {
                double d = Convert.ToDouble(txtDesnivel.Text);
                txtDesnivel.Text = d.ToString("###,###,###,##0.##");
            }
        }

        private void txtAreaInscripta_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(14);
        }
        private void txtAreaInscripta_Leave(object sender, EventArgs e)
        {
            cajas_blancas(14);

            if (txtAreaInscripta.Text.Trim() != "")
            {
                double d = Convert.ToDouble(txtAreaInscripta.Text);
                txtAreaInscripta.Text = d.ToString("###,###,###,##0.##");
            }
        }

        private void txtObservaciones_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(20);
        }

        private void txtObservaciones_Leave(object sender, EventArgs e)
        {
            cajas_blancas(20);
        }

        private void cmdSalida_Click(object sender, EventArgs e)
        {
            this.Close();
            //Program.menuBotonBloqueo = 1;
        }

        private void txtZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            // solo se permiten numeros		
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        private void txtMzna_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
        private void txtEdificio_TextChanged(object sender, EventArgs e)
        {
            if (txtEdificio.Text.Length == 2) { txtDepto.Focus(); }
        }
        private void txtZona_TextChanged(object sender, EventArgs e)
        {
            if (txtZona.Text.Length == 2) { txtMzna.Focus(); }
        }
        private void txtMzna_TextChanged(object sender, EventArgs e)
        {
            if (txtMzna.Text.Length == 3) { txtLote.Focus(); }
        }
        private void txtLote_TextChanged(object sender, EventArgs e)
        {
            if (txtLote.Text.Length == 2) { txtEdificio.Focus(); }
        }
        private void txtDepto_TextChanged(object sender, EventArgs e)
        {
            if (txtDepto.Text.Length == 4) { btnConsulta.Focus(); }
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        
        {
            lblSerie.Text = "";
            lblSerie.Text = Program.SerieC.ToString();

            if (Program.tipoUbicacionCartografia == 1)      // Alta de clave catastral
            {
                VALIDACION();
                txtZonaOrigen.Focus();
            }

            if (Program.tipoUbicacionCartografia == 2)      // Cambios en clave catastral
            {
                VALIDACION2();
                txtObservaciones.Focus();
            }

            if (Program.tipoUbicacionCartografia == 3)      // Certificados de clave y valor catastral
            {
                VALIDACION3();
                txtObservaciones.Focus();
            }
        }

        private void VALIDACION()
        {
            if (txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            if (txtEdificio.Text.Trim() != "00") { MessageBox.Show("ESTA CLAVE CATASTRAL NO ES DE PREDIOS., ES DE FRACCIONAMIENTO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() != "0000") { MessageBox.Show("ESTA CLAVE CATASTRAL NO ES DE PREDIOS., ES DE FRACCIONAMIENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();

            int EXISTE_PRO = 0;
            int EXISTE_PRE = 0;
            int proceso = 0;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAMOS SI EXISTE LA MANZANA
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            


                try
                {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT Zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM MANZANAS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE Zona = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana = " + mznaVar + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        EXISTE_PRO = 2;
                    }
                    else
                    {
                        EXISTE_PRO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            if (EXISTE_PRO == 2)
            {
                MessageBox.Show("NO EXISTE ESTA MANZANA", "ERROR", MessageBoxButtons.OK);
                txtMzna.Focus();
                return;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAR SI EXISTE EN POPIEDADES
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            EXISTE_PRO = 0;
            EXISTE_PRE = 0;
            proceso = 0;

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO  = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO     = " + util.scm(deptoVar) + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        EXISTE_PRO = 2;
                    }
                    else
                    {
                        EXISTE_PRO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAR SI EXISTE EN PREDIOS
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "") { EXISTE_PRE = 2; }
                    else
                    {
                        EXISTE_PRE = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VALIDAMOS LA INFORMACION DE EXISTENCIA
            /////////////////////////////////////////////////////////////  EN PROPIEDADES Y PREDIOS
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (edificioVar != "00")
            {
                if (deptoVar != "0000")
                {
                    MessageBox.Show("NO SE PUEDE DAR UNA ALTA DE FRACCIONAMIENTO POR ESTE MEDIO", "ERROR", MessageBoxButtons.OK);
                    limpiarTodoAlta();
                    txtZona.Focus();
                    return;
                }
            }

            if (EXISTE_PRO == 1)
            {
                MessageBox.Show("ESTA CLAVE CATASTRAL EXISTE EN PROPIEDADES", "ERROR", MessageBoxButtons.OK);
                limpiarTodoAlta();
                txtZona.Focus();
                return;
            }
            if (EXISTE_PRE == 1)
            {
                MessageBox.Show("ESTA CLAVE CATASTRAL EXISTE EN PREDIOS", "ERROR", MessageBoxButtons.OK);
                limpiarTodoAlta();
                txtZona.Focus();
                return;
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  SI NO EXISTE CONTINUAMOS CON EL LLENADO DE LOS COMBOS
            /////////////////////////////////////////////////////////////  REVISAMOS SI EXISTE FOLIO DE ALTA CON ESA CLAVE CATASTRAL
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "              From CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where DESCRIPCION = 'ALTA DE CLAVE'";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO  = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO     = " + util.scm(deptoVar) + " )";
                con.cadena_sql_interno = con.cadena_sql_interno + "      BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "          SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "      End";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Else";
                con.cadena_sql_interno = con.cadena_sql_interno + "      BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "          SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "      End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        EXISTE_PRE = 2;
                    }
                    else
                    {
                        proceso = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al obtener el folio de origen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  INGRESAMOS LA CONSTRUCCION SI ES QUE TIENE PRIVADA O COMUN
            /////////////////////////////////////////////////////////////  SOLO REVASAMOS SI HAY CONSTRUCCION PRIVADA
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where Zona     = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana  = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote     = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio = '" + edificioVar + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto    = '" + deptoVar + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "") { txtSupCont.Text = "0"; }
                    else { txtSupCont.Text = con.leer_interno[0].ToString().Trim(); }
                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al sumar la construccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  INGRESAMOS LA CONSTRUCCION SI ES QUE TIENE PRIVADA O COMUN
            /////////////////////////////////////////////////////////////  SOLO REVASAMOS SI HAY CONSTRUCCION COMUN
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where Zona     = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana  = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Lote     = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Edificio = ''";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Depto    = ''";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        txtSupContComn.Text = "0";
                    }
                    else
                    {
                        txtSupContComn.Text = con.leer_interno[0].ToString().Trim();
                    }
                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en seleccionar la construaccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            if (proceso == 1)
            {
                MessageBox.Show("EXISTE PROCESO YA CON ESTA CLAVE CATASTRAL", "ERROR", MessageBoxButtons.OK);
                inicio();
                txtZona.Focus();
                return;
            }
            if (proceso == 2)
            {
                pnlDatosPredio.Visible = true;
                pnlDatosPredio.Enabled = true;

                habilitarCajasInicio();
                llenarCombos1erVercion();

                btnConsulta.Enabled = false;
                btnGuardar.Enabled = true;
                btnBuscar.Enabled = false;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = false;

                txtObservaciones.Text = "";
                txtCalle.Text = "";
                txtSupTerreno.Text = "";
                txtSupComun.Text = "";
                txtDesnivel.Text = "";
                txtAreaInscripta.Text = "";
                txtFrente.Text = "";
                txtFondo.Text = "";
                txtLatitud.Text = "";
                txtLongitud.Text = "";

                txtSupCont.Enabled = true;
                txtSupContComn.Enabled = true;

                btnZonaOrigen.Enabled = true;
                btnConstLote.Enabled = true;
                btnConstComun.Enabled = true;
                btnRefresh.Enabled = true;
                btnMaps.Enabled = true;

                txtZona.Enabled = false;
                txtMzna.Enabled = false;
                txtLote.Enabled = false;
                txtEdificio.Enabled = false;
                txtDepto.Enabled = false;

                txtCalle.Enabled = false;
                GEOLOCALIZACION();
                txtZonaOrigen.Focus();

            }
        }//validacion para las altas
        private void ConsultaDatosPredio()
        {
            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();
            string regProp = "";
            string ubicacion = "";
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT P.ZonaOrig, C.CodCalle, C.NomCalle, R.RegProp, P.SupTerrTot,";
                con.cadena_sql_interno = con.cadena_sql_interno + "        P.SupTerrCom, P.SupCons, P.SupConsCom, P.Frente, P.Fondo, P.Desnivel, P.AreaInscr, P.Ubicacion";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM PREDIOS P, CALLES C, REGIMEN R";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE P.Municipio =  " + Convert.ToInt32(Program.municipioN);
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND P.Zona = " + Convert.ToInt32(txtZona.Text.ToString());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND P.Manzana =  " + Convert.ToInt32(txtMzna.Text.ToString());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND P.Lote = " + Convert.ToInt32(txtLote.Text.ToString());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND p.Estado = C.Estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND P.Municipio = C.Municipio";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND P.ZonaOrig = c.ZonaOrig";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND P.CodCalle = C.CodCalle";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND P.RegProp = r.RegProp";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        txtZonaOrigen.Text = con.leer_interno[0].ToString().Trim();
                       
                        cboCalles.Items.Add(con.leer_interno[1].ToString().Trim() + "  "  + con.leer_interno[2].ToString().Trim());
                        //cboRegimenPropiedad.Items.Add(con.leer_interno[3].ToString().Trim());
                        regProp = con.leer_interno[3].ToString().Trim();
                        txtSupTerreno.Text = con.leer_interno[4].ToString().Trim();
                        txtSupComun.Text = con.leer_interno[5].ToString().Trim();
                        txtSupCont.Text = con.leer_interno[6].ToString().Trim();
                        txtSupContComn.Text = con.leer_interno[7].ToString().Trim();
                        txtFrente.Text = con.leer_interno[8].ToString().Trim();
                        txtFondo.Text = con.leer_interno[9].ToString().Trim();
                        txtDesnivel.Text = con.leer_interno[10].ToString().Trim();
                        txtAreaInscripta.Text = con.leer_interno[11].ToString().Trim();
                        cboUbicacion.Items.Add(con.leer_interno[12].ToString().Trim());
                        cboCalles.SelectedIndex = 0;
                        //cboRegimenPropiedad.SelectedIndex = 0;
                        //cboUbicacion.SelectedIndex = 0;
                    }
                }
                con.cerrar_interno();

                foreach (var item in cboRegimenPropiedad.Items)
                {
                    string itemStr = item.ToString();
                    if (itemStr.StartsWith(regProp))
                    {
                        // Mostrar el valor completo del ComboBox
                        cboRegimenPropiedad.SelectedItem = item;
                        break; // Salir del bucle al encontrar la primera coincidencia
                    }
                }
                foreach (var item in cboUbicacion.Items)
                {
                    string itemStr = item.ToString();
                    if (itemStr.StartsWith(ubicacion))
                    {
                        // Mostrar el valor completo del ComboBox
                        cboUbicacion.SelectedItem = item;
                        break; // Salir del bucle al encontrar la primera coincidencia
                    }
                }
            }
            catch (Exception ex)
            {
            
            }

            }
        private void GEOLOCALIZACION()
        {
            double latitud, longitud;
            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();
            try
            {
                ///OBTENER LA GEOLOCALIZACIÓN
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT TOP 1  LATITUD, LONGITUD";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_GEOLOCALIZACION";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona      = " + zonaVar;  //Se cocatena la zona que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana   = " + mznaVar;  //Se cocatena la manzana que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Lote      = " + loteVar;  //Se cocatena el lote que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO   = '" + edificioVar + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO  = '" + deptoVar + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY id DESC";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        txtLatitud.Text = con.leer_interno[0].ToString().Trim();
                        txtLongitud.Text = con.leer_interno[1].ToString().Trim();
                    }
                }
                con.cerrar_interno();
                if (txtLatitud.Text == "" && txtLongitud.Text == "")
                {
                    txtLatitud.Visible = false;
                    txtLongitud.Visible = false;
                    txtLatitudG.Visible = true;
                    txtLatitudG.Enabled = true;
                    txtLongitudG.Visible = true;
                    txtLongitudG.Enabled = true;
                    variablePorSiCopiaLatitud = 1;

                }
                else
                {
                    txtLatitud.Visible = true;
                    txtLongitud.Visible = true;
                    txtLatitudG.Visible = false;
                    txtLongitudG.Visible = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en seleccionar la geolocalizacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
            //SI LA LATITUD Y LONGITUD NO ESTAN VACIAS, SE MUESTRA EL MAPA DE GOOGLE CON LA UBICACION DEL PREDIO
            if (txtLatitud.Text != "" || txtLongitud.Text != "")
            {
                latitud = Convert.ToDouble(txtLatitud.Text.Trim());
                longitud = Convert.ToDouble(txtLongitud.Text.Trim());
                gMapControl1.Visible = true;
                gMapControl1.DragButton = MouseButtons.Left;
                gMapControl1.CanDragMap = true;
                gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
                gMapControl1.Position = new GMap.NET.PointLatLng(latitud, longitud);
                gMapControl1.MinZoom = 1;
                gMapControl1.MaxZoom = 24;
                gMapControl1.Zoom = 19;
                gMapControl1.AutoScroll = true;
                gMapControl1.Enabled = true;
                btnMaps.Enabled = true;
            }
            else
            {
                gMapControl1.DragButton = MouseButtons.Left;
                gMapControl1.CanDragMap = true;
                gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
                gMapControl1.Position = new GMap.NET.PointLatLng(19.262174, -99.5330638);
                gMapControl1.MinZoom = 1;
                gMapControl1.MaxZoom = 24;
                gMapControl1.Zoom = 15;
                gMapControl1.AutoScroll = true;
                gMapControl1.Visible = true;
            }

        }
        private void VALIDACION2()
        {
            if (txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            //if (txtEdificio.Text.Trim() != "00") { MessageBox.Show("ESTA CLAVE CATASTRAL NO ES DE PREDIOS., ES DE FRACCIONAMIENTO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            //if (txtDepto.Text.Trim() != "0000") { MessageBox.Show("ESTA CLAVE CATASTRAL NO ES DE PREDIOS., ES DE FRACCIONAMIENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();

            int EXISTE_PRO = 0;
            int EXISTE_PRE = 0;
            int proceso = 0;


            int verificar = 0;
            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(deptoVar) + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {

                    verificar = Convert.ToInt32(con.leer_interno[0].ToString());
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

            if (verificar == 1)
            {
                MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA, NO SE PUEDE REALIZAR EL PROCESO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtZona.Focus();
                return;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAMOS SI EXISTE LA MANZANA
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT Zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM MANZANAS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE Zona = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana = " + mznaVar + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        EXISTE_PRO = 2;
                    }
                    else
                    {
                        EXISTE_PRO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            if (EXISTE_PRO == 2)
            {
                MessageBox.Show("NO EXISTE ESTA MANZANA", "ERROR", MessageBoxButtons.OK);
                txtMzna.Focus();
                return;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAR SI EXISTE EN POPIEDADES
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            EXISTE_PRO = 0;
            EXISTE_PRE = 0;
            proceso = 0;

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO  = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO     = " + util.scm(deptoVar) + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")                                // no existe
                    {
                        EXISTE_PRO = 2;
                    }
                    else
                    {
                        EXISTE_PRO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar la tabla de propiedades", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAR SI EXISTE EN PREDIOS
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        EXISTE_PRE = 2;                                                         // no existe
                    }
                    else
                    {
                        EXISTE_PRE = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar la tabla de predios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////

            if (EXISTE_PRO == 2)
            {
                MessageBox.Show("ESTA CLAVE CATASTRAL NO EXISTE EN PROPIEDADES", "ERROR", MessageBoxButtons.OK);
                limpiarTodoAlta();
                txtZona.Focus();
                return;
            }
            if (EXISTE_PRE == 2)
            {
                MessageBox.Show("ESTA CLAVE CATASTRAL NO EXISTE EN PREDIOS", "ERROR", MessageBoxButtons.OK);
                limpiarTodoAlta();
                txtZona.Focus();
                return;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  INGRESAMOS LA CONSTRUCCION SI ES QUE TIENE PRIVADA O COMUN
            /////////////////////////////////////////////////////////////  SOLO REVASAMOS SI HAY CONSTRUCCION PRIVADA
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where Zona     = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana  = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote     = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio = '" + edificioVar + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto    = '" + deptoVar + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        txtSupCont.Text = "0";
                    }
                    else
                    {
                        txtSupCont.Text = con.leer_interno[0].ToString().Trim();
                    }
                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al sumar la construccion de la tabla unidades de construccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  INGRESAMOS LA CONSTRUCCION SI ES QUE TIENE PRIVADA O COMUN
            /////////////////////////////////////////////////////////////  SOLO REVASAMOS SI HAY CONSTRUCCION COMUN
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where Zona     = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana  = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Lote     = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Edificio = ''";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Depto    = ''";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        txtSupContComn.Text = "0";
                    }
                    else
                    {
                        txtSupContComn.Text = con.leer_interno[0].ToString().Trim();
                    }
                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en seleccionar la construaccion en la tabla de construccion comun", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  limpiamos las cajas de texto de datos del predio
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            pnlDatosPredio.Visible = true;
            pnlDatosPredio.Enabled = true;

            limpiarDatosPredio();
            llenarCombos1erVercion();
            inabilitarDatosPredio();

            btnZonaOrigen.Enabled = false;
            btnConstLote.Enabled = true;
            btnConstComun.Enabled = true;
            btnRefresh.Enabled = true;
            btnMaps.Enabled = true;

            limpiarHabilitarChechBox();

            txtObservaciones.Enabled = true;
            txtObservaciones.Text = "";

            inabilitarBotones();
            inabilitarCajasTextoCatastro();

            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  llenamos los datos del predio
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            lblSerie.Text = Program.SerieC.ToString();

            GEOLOCALIZACION();
            ConsultaDatosPredio();
            txtObservaciones.Focus();

        }//validacion para busquedas

        private void VALIDACION3()
        {
            if (txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            //if (txtEdificio.Text.Trim() != "00") { MessageBox.Show("ESTA CLAVE CATASTRAL NO ES DE PREDIOS., ES DE FRACCIONAMIENTO", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            //if (txtDepto.Text.Trim() != "0000") { MessageBox.Show("ESTA CLAVE CATASTRAL NO ES DE PREDIOS., ES DE FRACCIONAMIENTO", "ERROR EN CERTIFICADOS", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();

            int EXISTE_PRO = 0;
            int EXISTE_PRE = 0;
            int proceso = 0;


            int verificar = 0;
            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(deptoVar) + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {

                    verificar = Convert.ToInt32(con.leer_interno[0].ToString());
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

            if (verificar == 1)
            {
                MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA, NO SE PUEDE REALIZAR EL PROCESO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtZona.Focus();
                return;
            }


            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAMOS SI EXISTE LA MANZANA
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT Zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM MANZANAS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE Zona = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana = " + mznaVar + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        EXISTE_PRO = 2;
                    }
                    else
                    {
                        EXISTE_PRO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            if (EXISTE_PRO == 2)
            {
                MessageBox.Show("NO EXISTE ESTA MANZANA", "ERROR", MessageBoxButtons.OK);
                txtMzna.Focus();
                return;
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAR SI EXISTE EN POPIEDADES
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            EXISTE_PRO = 0;
            EXISTE_PRE = 0;
            proceso = 0;

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO  = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO     = " + util.scm(deptoVar) + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")                                // no existe
                    {
                        EXISTE_PRO = 2;
                    }
                    else
                    {
                        EXISTE_PRO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar la tabla de propiedades", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  VERIFICAR SI EXISTE EN PREDIOS
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        EXISTE_PRE = 2;                                                         // no existe
                    }
                    else
                    {
                        EXISTE_PRE = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar la tabla de predios", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////

            if (EXISTE_PRO == 2)
            {
                MessageBox.Show("ESTA CLAVE CATASTRAL NO EXISTE EN PROPIEDADES", "ERROR", MessageBoxButtons.OK);
                limpiarTodoAlta();
                txtZona.Focus();
                return;
            }
            if (EXISTE_PRE == 2)
            {
                MessageBox.Show("ESTA CLAVE CATASTRAL NO EXISTE EN PREDIOS", "ERROR", MessageBoxButtons.OK);
                limpiarTodoAlta();
                txtZona.Focus();
                return;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  INGRESAMOS LA CONSTRUCCION SI ES QUE TIENE PRIVADA O COMUN
            /////////////////////////////////////////////////////////////  SOLO REVASAMOS SI HAY CONSTRUCCION PRIVADA
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where Zona     = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana  = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote     = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio = '" + edificioVar + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto    = '" + deptoVar + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        txtSupCont.Text = "0";
                    }
                    else
                    {
                        txtSupCont.Text = con.leer_interno[0].ToString().Trim();
                    }
                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al sumar la construccion de la tabla unidades de construccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  INGRESAMOS LA CONSTRUCCION SI ES QUE TIENE PRIVADA O COMUN
            /////////////////////////////////////////////////////////////  SOLO REVASAMOS SI HAY CONSTRUCCION COMUN
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where Zona     = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana  = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Lote     = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Edificio = ''";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Depto    = ''";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        txtSupContComn.Text = "0";
                    }
                    else
                    {
                        txtSupContComn.Text = con.leer_interno[0].ToString().Trim();
                    }
                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en seleccionar la construaccion en la tabla de construccion comun", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  limpiamos las cajas de texto de datos del predio
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            pnlDatosPredio.Visible = true;
            pnlDatosPredio.Enabled = true;

            limpiarDatosPredio();
            llenarCombos1erVercion();
            inabilitarDatosPredio();

            btnZonaOrigen.Enabled = false;
            btnConstLote.Enabled = true;
            btnConstComun.Enabled = true;
            btnRefresh.Enabled = true;
            btnMaps.Enabled = true;

            txtObservaciones.Enabled = true;
            txtObservaciones.Text = "";

            inabilitarBotones();
            inabilitarCajasTextoCatastro();

            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;

            ///////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  llenamos los datos del predio
            ///////////////////////////////////////////////////////////////////////////////////////////////////

            lblSerie.Text = Program.SerieC.ToString();
            ConsultaDatosPredio();
            GEOLOCALIZACION();

        }//validacion para certificados

        private void limpiarDatosPredio()
        {
            lblSerie.Text = "";
            txtZonaOrigen.Text = "";
            txtCalle.Text = "";

            cboCalles.Items.Clear();
            cboRegimenPropiedad.Items.Clear();
            cboUbicacion.Items.Clear();

            txtSupTerreno.Text = "";
            txtSupCont.Text = "";
            txtSupComun.Text = "";
            txtSupContComn.Text = "";
            txtFrente.Text = "";
            txtFondo.Text = "";
            txtDesnivel.Text = "";
            txtAreaInscripta.Text = "";
            txtLatitud.Text = "";
            txtLongitud.Text = "";
            txtObservaciones.Text = "";
        }

        private void inabilitarDatosPredio()
        {
            lblSerie.Enabled = true;

            txtZonaOrigen.Enabled = false;
            txtCalle.Enabled = false;
            txtSupTerreno.Enabled = false;
            txtSupCont.Enabled = false;
            txtSupComun.Enabled = false;
            txtSupContComn.Enabled = false;
            txtFrente.Enabled = false;
            txtFondo.Enabled = false;
            txtDesnivel.Enabled = false;
            txtAreaInscripta.Enabled = false;

            txtLatitud.Enabled = true;
            txtLongitud.Enabled = true;

            txtObservaciones.Enabled = false;
            cboCalles.Enabled = false;
            cboRegimenPropiedad.Enabled = false;
            cboUbicacion.Enabled = false;
        }

        private void limpiarHabilitarChechBox()
        {
            ckbCambioNombre.Enabled = true;
            ckbCambioConstruccion.Enabled = true;
            ckbCambioSuperficie.Enabled = true;
            ckbCambioFactoresCons.Enabled = true;
            ckbCambioFactoresTerr.Enabled = true;

            ckbCambioNombre.Checked = false;
            ckbCambioConstruccion.Checked = false;
            ckbCambioSuperficie.Checked = false;
            ckbCambioFactoresCons.Checked = false;
            ckbCambioFactoresTerr.Checked = false;
        }

        private void inabilitarBotones()
        {
            btnConsulta.Enabled = false;
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = false;
            cmdSalida.Enabled = false;
            btnMinimizar.Enabled = true;
            btnGuardar.Enabled = false;
        }

        private void inabilitarCajasTextoCatastro()
        {
            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
        }

        private void habilitarCajasInicio()
        {
            txtZonaOrigen.Enabled = true;
            txtCalle.Enabled = false;
            txtSupTerreno.Enabled = true;
            txtSupCont.Enabled = true;
            txtSupComun.Enabled = true;
            txtSupContComn.Enabled = true;
            txtFrente.Enabled = true;
            txtFondo.Enabled = true;
            txtDesnivel.Enabled = true;
            txtAreaInscripta.Enabled = true;

            txtLatitud.Enabled = true;
            txtLongitud.Enabled = true;
            txtObservaciones.Enabled = true;

            cboCalles.Enabled = false;
            cboRegimenPropiedad.Enabled = true;
            cboUbicacion.Enabled = true;

            btnZonaOrigen.Enabled = true;
            btnConstLote.Enabled = true;
            btnConstComun.Enabled = true;
            btnRefresh.Enabled = true;

            //cboCalles.SelectedIndex = 1;

            cboRegimenPropiedad.SelectedIndex = -1;
            cboUbicacion.SelectedIndex = -1;

            txtZonaOrigen.Focus();
        }

        private void limpiarTodoAlta()
        {
            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";

            txtZonaOrigen.Text = "";
            txtCalle.Text = "";

            cboCalles.Items.Clear();
            cboRegimenPropiedad.Items.Clear();
            cboUbicacion.Items.Clear();

            txtSupTerreno.Text = "";
            txtSupCont.Text = "";
            txtSupComun.Text = "";
            txtSupContComn.Text = "";
            txtFrente.Text = "";
            txtFondo.Text = "";
            txtDesnivel.Text = "";
            txtAreaInscripta.Text = "";

            txtLatitud.Text = "";
            txtLongitud.Text = "";
            txtLatitudG.Text = "";
            txtLongitudG.Text = "";

            txtObservaciones.Text = "";
            gMapControl1.Visible = false;

            lblSerie.Text = "";

            ckbCambioNombre.Checked = false;
            ckbCambioConstruccion.Checked = false;
            ckbCambioSuperficie.Checked = false;
            ckbCambioFactoresCons.Checked = false;
            ckbCambioFactoresTerr.Checked = false;

        }

        private void llenarCombos1erVercion()
        {
            cboCalles.Items.Clear();
            cboRegimenPropiedad.Items.Clear();
            cboUbicacion.Items.Clear();

            cboRegimenPropiedad.Items.Add("0 SIN DESCRIPCION");
            cboRegimenPropiedad.Items.Add("1 PRIVADA INDIVIDUAL");
            cboRegimenPropiedad.Items.Add("2 PRIVADA CONDOMINIO");
            cboRegimenPropiedad.Items.Add("3 EJIDAL");
            cboRegimenPropiedad.Items.Add("4 COMUNAL");
            cboRegimenPropiedad.Items.Add("5 COMUN REPARTIMIENTO");
            cboRegimenPropiedad.Items.Add("6 FEDERAL");
            cboRegimenPropiedad.Items.Add("7 ESTATAL");
            cboRegimenPropiedad.Items.Add("8 MUNICIPAL");

            cboUbicacion.Items.Add("0 SIN DESCRIPCION");
            cboUbicacion.Items.Add("1 INTERMEDIO");
            cboUbicacion.Items.Add("2 ESQUINERO");
            cboUbicacion.Items.Add("3 CABECERO");
            cboUbicacion.Items.Add("4 MANZANERO");
            cboUbicacion.Items.Add("5 FRENTES NO CONTIGUOS");
            cboUbicacion.Items.Add("6 INTERIOR");
        }

        private void zonaOrigen()
        {
            txtCalle.Text = "";
            cboCalles.Items.Clear();

            if (txtZonaOrigen.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA DE ORIGEN", "ERROR", MessageBoxButtons.OK); return; }

            int zonaOrigenP = Convert.ToInt32(txtZonaOrigen.Text.Trim());
            int contador = 0;
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT CodCalle, NomCalle";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CALLES";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE ZonaOrig = " + zonaOrigenP;
                con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY CodCalle";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    contador = contador + 1;
                    if (con.leer_interno[0].ToString().Trim().Length == 1)
                    {
                        cboCalles.Items.Add("00" + con.leer_interno[0].ToString().Trim() + "  " + con.leer_interno[1].ToString().Trim());
                    }
                    if (con.leer_interno[0].ToString().Trim().Length == 2)
                    {
                        cboCalles.Items.Add("0" + con.leer_interno[0].ToString().Trim() + "  " + con.leer_interno[1].ToString().Trim());
                    }
                    if (con.leer_interno[0].ToString().Trim().Length == 3)
                    {
                        cboCalles.Items.Add(con.leer_interno[0].ToString().Trim() + "  " + con.leer_interno[1].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }

            if (contador == 0)
            {
                MessageBox.Show("NO SE TIENEN CALLES PARA ESTA ZONA DE ORIGEN", "ERROR", MessageBoxButtons.OK); return;
            }



            cboCalles.Enabled = true;
            txtCalle.Enabled = false;
            cboCalles.Focus();

            cboCalles.SelectedIndex = 0;
            txtCalle.Text = cboCalles.SelectedItem.ToString().Trim().Substring(0, 3);

        }

        private void inabilitarBotonesAlta()
        {
            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
        }

        private void btnZonaOrigen_Click(object sender, EventArgs e)
        {
            zonaOrigen();
        }

        private void txtZonaOrigen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtSupTerreno_KeyPress(object sender, KeyPressEventArgs e)
        {
            int valida = 0;

            if (e.KeyChar >= 48 && e.KeyChar <= 57)  //solo numero
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 46)   // punto decimal
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 8)   // delete
            {
                e.Handled = false;
                valida = 1;
            }

            if (valida == 0)
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtSupComun_KeyPress(object sender, KeyPressEventArgs e)
        {
            int valida = 0;

            if (e.KeyChar >= 48 && e.KeyChar <= 57)  //solo numero
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 46)   // punto decimal
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 8)   // delete
            {
                e.Handled = false;
                valida = 1;
            }

            if (valida == 0)
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtFrente_KeyPress(object sender, KeyPressEventArgs e)
        {
            int valida = 0;

            if (e.KeyChar >= 48 && e.KeyChar <= 57)  //solo numero
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 46)   // punto decimal
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 8)   // delete
            {
                e.Handled = false;
                valida = 1;
            }

            if (valida == 0)
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtFondo_KeyPress(object sender, KeyPressEventArgs e)
        {
            int valida = 0;

            if (e.KeyChar >= 48 && e.KeyChar <= 57)  //solo numero
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 46)   // punto decimal
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 8)   // delete
            {
                e.Handled = false;
                valida = 1;
            }

            if (valida == 0)
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtDesnivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            int valida = 0;

            if (e.KeyChar >= 48 && e.KeyChar <= 57)  //solo numero
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 46)   // punto decimal
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 8)   // delete
            {
                e.Handled = false;
                valida = 1;
            }

            if (valida == 0)
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtAreaInscripta_KeyPress(object sender, KeyPressEventArgs e)
        {
            int valida = 0;

            if (e.KeyChar >= 48 && e.KeyChar <= 57)  //solo numero
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 46)   // punto decimal
            {
                e.Handled = false;
                valida = 1;
            }

            if (e.KeyChar == 8)   // delete
            {
                e.Handled = false;
                valida = 1;
            }

            if (valida == 0)
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void cboCalles_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCalle.Text = cboCalles.Text.Substring(0, 3);
        }

        private void txtDepto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Program.tipoUbicacionCartografia == 1)      //alta de calve catastral
                {
                    VALIDACION();
                }
            }
        }

        private void txtZonaOrigen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Program.tipoUbicacionCartografia == 1)      //alta de calve catastral
                {
                    zonaOrigen();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            inicio2();
            //inicio();
        }

        private void btnConstLote_Click(object sender, EventArgs e)
        {

            double constC = 0;
            if (txtSupCont.Text.Trim() == "") { txtSupCont.Text = "0"; }
            if (txtSupContComn.Text.Trim() == "") { txtSupContComn.Text = "0"; }

            if (txtSupContComn.Text.Trim() != "0")
            {
                constC = Convert.ToDouble(txtSupContComn.Text.Trim());
                if (constC > 0)
                {
                    MessageBox.Show("NO SE PUEDE INGRESAR CONSTRUCCION PROPIA. PORQUE SE TIENE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK);
                    return;
                }
            }

            if (txtSupContComn.Text.Trim() == "") { txtSupContComn.Text = "0"; }
            if (txtSupContComn.Text.Trim() == "0")
            {
                Program.tipoContruccion = 0;                        // O PRIVADA, 1 COMUN
                Program.municipioV = Program.municipioT.Trim();
                Program.zonaV = txtZona.Text.Trim();
                Program.manzanaV = txtMzna.Text.Trim();
                Program.loteV = txtLote.Text.Trim();
                Program.edificioV = txtEdificio.Text.Trim();
                Program.deptoV = txtDepto.Text.Trim();

                Program.tipoContruccion = 0;                        //construccion PRIVADA         

                frmCatastro01UbicacionAlta.ActiveForm.Opacity = 0.70;
                frmCatastro02UnidadesConstruccion fs = new frmCatastro02UnidadesConstruccion();
                fs.ShowDialog();
                //fs.Show();
                txtSupCont.Text = Program.constuccion.ToString();
                frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;

                //frmCatastro01UbicacionAlta.ActiveForm.Opacity = 0.50;
                //frmCatastro02UnidadesConstruccion fs = new frmCatastro02UnidadesConstruccion();
                //fs.ShowDialog();
                ////txtSupCont.Text = Program.construccion();
                ////fs.Show();
                //frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;
            }

        }

        private void limpiarVariablesClaveCatProgram()
        {
            Program.municipioV = txtMun.Text.Trim();

        }

        private void btnConstComun_Click(object sender, EventArgs e)
        {
            double constP = 0;

            if (txtSupCont.Text.Trim() == "") { txtSupCont.Text = "0"; }
            if (txtSupContComn.Text.Trim() == "") { txtSupContComn.Text = "0"; }

            constP = Convert.ToDouble(txtSupCont.Text.Trim());
            if (constP > 0)
            {
                MessageBox.Show("NO SE PUEDE INGRESAR CONSTRUCCION COMUN. PORQUE SE TIENE CONSTRUCCION PRIVADA", "ERROR", MessageBoxButtons.OK);
                return;
            }


            if (txtSupCont.Text.Trim() == "0")
            {
                Program.tipoContruccion = 0;
                Program.municipioV = Program.municipioT.Trim();
                Program.zonaV = txtZona.Text.Trim();
                Program.manzanaV = txtMzna.Text.Trim();
                Program.loteV = txtLote.Text.Trim();
                Program.edificioV = txtEdificio.Text.Trim();
                Program.deptoV = txtDepto.Text.Trim();

                Program.tipoContruccion = 1;                        //construccion comun         

                frmCatastro01UbicacionAlta.ActiveForm.Opacity = 0.50;
                frmCatastro02UnidadesConstruccion fs = new frmCatastro02UnidadesConstruccion();
                fs.ShowDialog();
                //fs.Show();
                frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;
            }
        }

        private int AltaLote()
        {
            if (txtZonaOrigen.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA DE ORIGEN", "ERROR", MessageBoxButtons.OK); txtZonaOrigen.Focus(); return 0; }
            if (txtCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA CALLE", "ERROR", MessageBoxButtons.OK); txtCalle.Focus(); return 0; }
            if (cboRegimenPropiedad.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL REGIMEN DE LA PROPIEDAD", "ERROR", MessageBoxButtons.OK); cboRegimenPropiedad.Focus(); return 0; }
            if (txtSupTerreno.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUPERFICIE DE TERRENO", "ERROR", MessageBoxButtons.OK); txtSupTerreno.Focus(); return 0; }
            if (txtSupComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUPERFICIE DE TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtSupComun.Focus(); return 0; }
            if (txtFrente.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FRENTE DE LA PROPIEDAD", "ERROR", MessageBoxButtons.OK); txtFrente.Focus(); return 0; }
            if (txtFondo.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FONDE DE LA PROPIEDAD", "ERROR", MessageBoxButtons.OK); txtFondo.Focus(); return 0; }
            if (cboCalles.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA CALLE", "ERROR", MessageBoxButtons.OK); cboCalles.Focus(); return 0; }
            if (cboUbicacion.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA UBICACION", "ERROR", MessageBoxButtons.OK); cboUbicacion.Focus(); return 0; }
            if (txtSupCont.Text.Trim() == "") { MessageBox.Show("NO SE TIENE SUPERFICIE DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtSupCont.Focus(); return 0; }
            if (txtSupContComn.Text.Trim() == "") { MessageBox.Show("NO SE TIENE SUPERFICIE DE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtSupContComn.Focus(); return 0; }
            if (txtDesnivel.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DESNIVEL", "ERROR", MessageBoxButtons.OK); txtDesnivel.Focus(); return 0; }
            if (txtAreaInscripta.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA AREA INSCRIPTA", "ERROR", MessageBoxButtons.OK); txtAreaInscripta.Focus(); return 0; }

            if (variablePorSiCopiaLatitud == 1)
            {
                if (txtLatitudG.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA LATITUD", "ERROR", MessageBoxButtons.OK); gMapControl1.Focus(); return 0; }
                if (txtLongitud.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA LONGITUD", "ERROR", MessageBoxButtons.OK); gMapControl1.Focus(); return 0; }
                String latitudGmail = txtLatitudG.Text.Trim();
                String longitudGmail = txtLongitudG.Text.Trim();
            }
            if (txtLatitud.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA LATITUD", "ERROR", MessageBoxButtons.OK); gMapControl1.Focus(); return 0; }
            if (txtLongitud.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA LONGITUD", "ERROR", MessageBoxButtons.OK); gMapControl1.Focus(); return 0; }



            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();
            string serie = lblSerie.Text.Trim();

            double valor_terreno_m;
            double valor_terreno_comun_m;
            double valor_construccion_m;
            double valor_COMUN_m;
            double factor_frente_m;
            double factor_fondo_m;
            double factor_irregularidad_m;
            double factor_area;
            double factor_topografia_m;
            double factor_posicion_m;

            string fechaIngreso = "";

            fechaIngreso = DateTime.Now.ToString("O");
            string fechaIngresos = fechaIngreso.Trim().Substring(0, 10);
            string fechasHora = fechaIngreso.Trim().Substring(11, 8);
            string fechaSql = fechaIngreso.Trim().Substring(0, 4) + fechaIngreso.Trim().Substring(5, 2) + fechaIngreso.Trim().Substring(8, 2);
            string fechaHoraSql = fechaSql + " " + fechasHora;
            int A = 0;
            int B = 0;
            int maxFolio = 0;

            if (txtEdificio.Text != "00")
            {
                if (txtDepto.Text != "0000") { MessageBox.Show("NO SE PUEDE DAR DE ALTA ESTA CLAVE, CLAVE ERRONEA", "ERROR", MessageBoxButtons.OK); return 0; }
                else { MessageBox.Show("NO SE PUEDE DAR DE ALTA ESTA CLAVE, CLAVE ERRONEA", "ERROR", MessageBoxButtons.OK); return 0; }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// Vamos ver si existe o no en la base de datos  //////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = " IF EXISTS (SELECT ZONA";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE ESTADO    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         IF EXISTS (SELECT ZONA";
                con.cadena_sql_interno = con.cadena_sql_interno + "                      FROM PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "                     WHERE ESTADO    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND LOTE      = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND EDIFICIO  = '" + edificioVar + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND DEPTO     = '" + deptoVar + "')";
                con.cadena_sql_interno = con.cadena_sql_interno + "             BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT MEMO = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "             END";
                con.cadena_sql_interno = con.cadena_sql_interno + "         Else";
                con.cadena_sql_interno = con.cadena_sql_interno + "             BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT MEMO = 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             End";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " Else";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT MEMO = 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " SET NOCOUNT ON";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        A = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }

                con.cerrar_interno();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error saber si existe ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }
            if (A == 1)
            {
                MessageBox.Show("ESTA CLAVE CATASTRAL - EXISTE EN LA BASE DE DATOS, CLAVE ERRÓNEA", "ERROR", MessageBoxButtons.OK);
                cancelartodo();
                limpiarTodoAlta();
                llenarCombos1erVercion();

                tabAlta();
                return 0;
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// Vamos a ver si se encuentra en las tablas temporales  //////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = " IF EXISTS (SELECT ZONA";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM tem_PREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE ESTADO    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE      = " + loteVar + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         IF EXISTS (SELECT ZONA";
                con.cadena_sql_interno = con.cadena_sql_interno + "                      FROM tem_PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "                     WHERE ESTADO    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND ZONA      = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND MANZANA   = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND LOTE      = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND EDIFICIO  = '" + edificioVar + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "                       AND DEPTO     = '" + deptoVar + "')";
                con.cadena_sql_interno = con.cadena_sql_interno + "             BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT MEMO = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "             END";
                con.cadena_sql_interno = con.cadena_sql_interno + "         Else";
                con.cadena_sql_interno = con.cadena_sql_interno + "             BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT MEMO = 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             End";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " Else";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT MEMO = 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " SET NOCOUNT ON";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "") { B = Convert.ToInt32(con.leer_interno[0].ToString().Trim()); }
                }

                con.cerrar_interno();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error"  + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }
            if (B == 1)
            {
                MessageBox.Show("ESTA CLAVE YA TIENE UN REGISTRO PREVIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                cancelartodo();
                limpiarTodoAlta();
                llenarCombos1erVercion();

                tabAlta();
                return 0;

            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// OBTENEMOS EL MAXIMO FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = " SELECT MAX(FOLIO_ORIGEN) FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE SERIE = " + util.scm(serie);

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        maxFolio = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }

                if (maxFolio == 0)
                {
                    maxFolio = 1;
                }
                else
                {
                    maxFolio = maxFolio + 1;
                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar el maximo folio" + ex.Message , MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// OBTENEMOS LOS VALORES DE CONSTRUCCION, CON SUS FACTORES /////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("N19_CALCULO_CATASTRO", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = 15;
                cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 3).Value = 41;
                cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = Convert.ToInt32(zonaVar);
                cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = Convert.ToInt32(mznaVar);
                cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = Convert.ToInt32(loteVar);
                cmd.Parameters.Add("@EDIFICIO", SqlDbType.VarChar, 2).Value = edificioVar;
                cmd.Parameters.Add("@DEPTO", SqlDbType.VarChar, 4).Value = deptoVar;

                if (txtSupTerreno.Text == "") { txtSupTerreno.Text = "0"; }
                if (txtSupCont.Text == "") { txtSupCont.Text = "0"; }
                if (txtSupComun.Text == "") { txtSupComun.Text = "0"; }
                if (txtSupContComn.Text == "") { txtSupContComn.Text = "0"; }
                if (txtFrente.Text == "") { txtFrente.Text = "0"; }
                if (txtFondo.Text == "") { txtFondo.Text = "0"; }
                if (txtDesnivel.Text == "") { txtDesnivel.Text = "0"; }
                if (txtAreaInscripta.Text == "") { txtAreaInscripta.Text = "0"; }

                cmd.Parameters.Add("@TP", SqlDbType.Float, 2).Value = txtSupTerreno.Text.Trim();
                cmd.Parameters.Add("@TP2", SqlDbType.Float, 3).Value = 0;
                cmd.Parameters.Add("@tc", SqlDbType.Float, 2).Value = txtSupComun.Text.Trim();
                cmd.Parameters.Add("@año", SqlDbType.Int, 3).Value = Program.añoActual;
                cmd.Parameters.Add("@frente", SqlDbType.Float, 2).Value = txtFrente.Text.Trim();
                cmd.Parameters.Add("@fondo", SqlDbType.Float, 2).Value = txtFondo.Text.Trim();
                cmd.Parameters.Add("@irregularidad", SqlDbType.Float, 4).Value = txtAreaInscripta.Text.Trim();

                cmd.Parameters.Add("@topografia", SqlDbType.Float, 2).Value = txtDesnivel.Text.Trim();
                cmd.Parameters.Add("@posicion", SqlDbType.Int, 3).Value = Convert.ToInt32(cboUbicacion.Text.Trim().Substring(0, 1));
                cmd.Parameters.Add("@indiviso", SqlDbType.Float, 2).Value = 1;
                cmd.Parameters.Add("@cc1", SqlDbType.Float, 3).Value = txtSupContComn.Text.Trim();
                cmd.Parameters.Add("@regimen", SqlDbType.Int, 2).Value = Convert.ToInt32(cboRegimenPropiedad.Text.Trim().Substring(0, 1));
                cmd.Parameters.Add("@COD_CALLE", SqlDbType.Int, 2).Value = Convert.ToInt32(txtCalle.Text.Trim());

                cmd.Parameters.Add("@valor_terreno", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@valor_terreno_comun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@valor_construccion", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@valor_COMUN", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@factor_frente", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@factor_fondo", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@factor_irregularidad", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@factor_area", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@factor_topografia", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@factor_posicion", SqlDbType.Float, 9).Direction = ParameterDirection.Output;

                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                valor_terreno_m = Convert.ToDouble(cmd.Parameters["@valor_terreno"].Value);
                valor_terreno_comun_m = Convert.ToDouble(cmd.Parameters["@valor_terreno_comun"].Value);
                valor_construccion_m = Convert.ToDouble(cmd.Parameters["@valor_construccion"].Value);
                valor_COMUN_m = Convert.ToDouble(cmd.Parameters["@valor_COMUN"].Value);
                factor_frente_m = Convert.ToDouble(cmd.Parameters["@factor_frente"].Value);
                factor_fondo_m = Convert.ToDouble(cmd.Parameters["@factor_fondo"].Value);
                factor_irregularidad_m = Convert.ToDouble(cmd.Parameters["@factor_irregularidad"].Value);
                factor_area = Convert.ToDouble(cmd.Parameters["@factor_area"].Value);
                factor_topografia_m = Convert.ToDouble(cmd.Parameters["@factor_topografia"].Value);
                factor_posicion_m = Convert.ToDouble(cmd.Parameters["@factor_posicion"].Value);

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar el proceso N19_CALCULO_CATASTRO" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////  INSERTAR EN CAT_NEW_CARTOGRAFIA  ///////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                con.conectar_base_interno();

                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ESTADO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    MUNICIPIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ZONA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    MANZANA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    LOTE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    EDIFICIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    DEPTO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    UBICACION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    DESCRIPCION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FECHA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    HORA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    USUARIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    OBSERVACIONES,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TERR_PROPIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TERR_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AÑO_CALCULO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    frente,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    fondo,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    IRREGULARIDAD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TOPOGRAFIA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    POSICION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    INDIVISO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SUP_CON_COM,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SUP_CON,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    REGIMEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    COD_CALLE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ZON_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_TERRENO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_TERRENO_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_CONST,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_CONST_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_FRENTE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_FONDO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_IRREG,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_AREA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_TOPO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_POSICION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "   )";
                con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "   (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(muniVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(zonaVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(mznaVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(loteVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "   '" + edificioVar + "' ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "   '" + deptoVar + "' ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 1 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "        'ALTA CLAVE' ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "   '" + fechaSql + "' ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "   '" + fechaHoraSql + "' ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "   '" + Program.nombre_usuario + "' ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "   '" + txtObservaciones.Text.Trim() + "' ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToDouble(txtSupTerreno.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToDouble(txtSupComun.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.añoActual + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToDouble(txtFrente.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToDouble(txtFondo.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToDouble(txtDesnivel.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToDouble(txtAreaInscripta.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(cboUbicacion.Text.Trim().Substring(0, 1)) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "  0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + txtSupContComn.Text.Trim() + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + txtSupCont.Text.Trim() + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(cboRegimenPropiedad.Text.Trim().Substring(0, 1)) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + txtCalle.Text.Trim() + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + txtZonaOrigen.Text.Trim() + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno_comun_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_construccion_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_COMUN_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + factor_frente_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + factor_fondo_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + factor_irregularidad_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + factor_area + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + factor_topografia_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + factor_posicion_m + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "   )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_interno = con.cadena_sql_interno + "    INSERT INTO CAT_DONDE_VA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    CARTOGRAFIA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VENTANILLA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    REVISO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SISTEMAS,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ELIMINADO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    1,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_interno = con.cadena_sql_interno + "    INSERT INTO SONG_GEOLOCALIZACION";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Estado,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Municipio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Zona,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Manzana,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Lote,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Edificio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Depto,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    NombreUsuario,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Latitud,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Longitud,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    COMENTARIO";
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VALUES";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + 15 + " ,";                                  // Estado
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(muniVar) + " ,"; // Municipio
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(zonaVar) + " ,"; // Zona
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(mznaVar) + " ,"; // Manzana
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(loteVar) + " ,"; // Lote
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + edificioVar + "',"; // Edificio
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + deptoVar + "',"; // Depto 
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + Program.nombre_usuario + "',"; // NombreUsuario
                if (variablePorSiCopiaLatitud == 1)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLatitudG.Text.Trim() + "',"; // Latitud
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLongitudG.Text.Trim() + "',"; // Longitud
                }
                else
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLatitud.Text.Trim() + "',"; // Latitud
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLongitud.Text.Trim() + "',"; // Longitud
                }
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtObservaciones.Text.Trim() + "'";  // Comentario
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al insertar en la tabla CAT_NEW_CARTOGRAFIA_2025, CAT_DONDE_VA_2025, SONG_GEOLOCALIZACION ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }

            MessageBox.Show("FUE INGRESADO EL TRAMITE ,CON EXITO", "INFORMACION", MessageBoxButtons.OK);

            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (s, ev) =>
            {
                // Configurar la fuente
                Font font = new Font("Arial", 14, FontStyle.Bold);

                // Nombre del Archivo
                printDoc.PrinterSettings.PrinterName = "MEMO";

                // Crear el texto a imprimir
                string texto0 = $"                                       Fecha de Creación. {DateTime.Now}";
                string texto1 = $"Folio: {serie}-{maxFolio}, Alta de Clave -> 041-{zonaVar}-{mznaVar}-{loteVar}-{edificioVar}-{deptoVar}";
                string texto2 = $"Usuario que dio la Alta -> {Program.nombre_usuario}";
                string texto3 = $"Zona de Origen -> {txtZonaOrigen.Text.Trim()}  Calle -> {cboCalles.Text.Trim()}";
                string texto4 = $"Regimen Propiedad -> {cboRegimenPropiedad.Text.Trim()}  Ubicacion -> {cboUbicacion.Text.Trim()}";
                string texto5 = $"Sup. Terreno -> {txtSupTerreno.Text.Trim()}  Sup. Terr. Comun -> {txtSupComun.Text.Trim()}";
                string texto6 = $"Sup. Construccion -> {txtSupCont.Text.Trim()}  Sup. Const. Comun -> {txtSupContComn.Text.Trim()}";
                string texto7 = $"Desnivel -> {txtDesnivel.Text.Trim()}  Area Inscripta -> {txtAreaInscripta.Text.Trim()}";
                string texto8 = $"Frente -> {txtFrente.Text.Trim()}  Fondo -> {txtFondo.Text.Trim()}";
                string texto9 = $"Latitud -> {txtLatitud.Text.Trim()}  Longitud -> {txtLongitud.Text.Trim()}";
                string texto10 = $"Observaciones -> {txtObservaciones.Text.Trim()}";


                // Dibujar el texto en la página
                ev.Graphics.DrawString(texto0, font, Brushes.Black, new PointF(15, 20));
                ev.Graphics.DrawString(texto1, font, Brushes.Black, new PointF(15, 50));
                ev.Graphics.DrawString(texto2, font, Brushes.Black, new PointF(15, 80));
                ev.Graphics.DrawString(texto3, font, Brushes.Black, new PointF(15, 110));
                ev.Graphics.DrawString(texto4, font, Brushes.Black, new PointF(15, 140));
                ev.Graphics.DrawString(texto5, font, Brushes.Black, new PointF(15, 170));
                ev.Graphics.DrawString(texto6, font, Brushes.Black, new PointF(15, 200));
                ev.Graphics.DrawString(texto7, font, Brushes.Black, new PointF(15, 230));
                ev.Graphics.DrawString(texto8, font, Brushes.Black, new PointF(15, 260));
                ev.Graphics.DrawString(texto9, font, Brushes.Black, new PointF(15, 290));
                ev.Graphics.DrawString(texto10, font, Brushes.Black, new PointF(15, 320));
            };

            // Iniciar la impresió
            printDoc.Print();
            return 1; // Retornar true si todo salió bien
        }

        private int cambiosLote()
        {
            String AAA = "";
            string fechaIngreso = "";
            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();
            String serie = Program.SerieC;

            fechaIngreso = DateTime.Now.ToString("O");
            string fechaIngresos = fechaIngreso.Trim().Substring(0, 10);
            string fechasHora = fechaIngreso.Trim().Substring(11, 8);
            string fechaSql = fechaIngreso.Trim().Substring(0, 4) + fechaIngreso.Trim().Substring(5, 2) + fechaIngreso.Trim().Substring(8, 2);
            string fechaHoraSql = fechaSql + " " + fechasHora;
            int maxFolio = 0;

            //revisar las fechas como obtenerlas

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// COMPROBACIONES //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (!ckbCambioNombre.Checked && !ckbCambioConstruccion.Checked && !ckbCambioFactoresCons.Checked && !ckbCambioSuperficie.Checked && !ckbCambioFactoresTerr.Checked)
            {
                MessageBox.Show("NO SE TIENE NINGUNA OPCION DE CAMBIOS", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Sale de la función
            }

            if (ckbCambioNombre.Checked == true) { AAA = "1"; } else { AAA = "0"; }
            if (ckbCambioSuperficie.Checked == true) { AAA = AAA + "1"; } else { AAA = AAA + "0"; }
            if (ckbCambioConstruccion.Checked == true) { AAA = AAA + "1"; } else { AAA = AAA + "0"; }
            if (ckbCambioFactoresCons.Checked == true) { AAA = AAA + "1"; } else { AAA = AAA + "0"; }
            if (ckbCambioFactoresTerr.Checked == true) { AAA = AAA + "1"; } else { AAA = AAA + "0"; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// OBTENEMOS EL MAXIMO FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();

                con.cadena_sql_interno = " SELECT MAX(FOLIO_ORIGEN)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE serie = " + util.scm(serie);

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        maxFolio = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                if (maxFolio == 0) { maxFolio = 1; } else { maxFolio = maxFolio + 1; }

                con.cerrar_interno();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al buscar el maximo folio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////  INSERTAR EN CAT_NEW_CARTOGRAFIA  ///////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();

                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ESTADO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    MUNICIPIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ZONA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    MANZANA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    LOTE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    EDIFICIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    DEPTO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    UBICACION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    DESCRIPCION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FECHA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    HORA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    USUARIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    OBSERVACIONES,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TERR_PROPIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TERR_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AÑO_CALCULO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    frente,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    fondo,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    IRREGULARIDAD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TOPOGRAFIA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    POSICION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    INDIVISO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SUP_CON_COM,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SUP_CON,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    REGIMEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    COD_CALLE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ZON_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_TERRENO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_TERRENO_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_CONST,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_CONST_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_FRENTE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_FONDO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_IRREG,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_AREA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_TOPO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_POSICION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(muniVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(zonaVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(mznaVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(loteVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(edificioVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(deptoVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.tipoUbicacionCartografia + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    '" + AAA + "CAMBIO DE CLAVE" + "',";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(fechaSql) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(fechaHoraSql) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(Program.nombre_usuario) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(txtObservaciones.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.añoActual + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_interno = con.cadena_sql_interno + "    INSERT INTO CAT_DONDE_VA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    CARTOGRAFIA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VENTANILLA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    REVISO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SISTEMAS,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ELIMINADO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    1,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_interno = con.cadena_sql_interno + "    INSERT INTO SONG_GEOLOCALIZACION";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Estado,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Municipio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Zona,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Manzana,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Lote,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Edificio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Depto,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    NombreUsuario,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Latitud,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Longitud,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    COMENTARIO";
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VALUES";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + 15 + " ,";                                 // Estado
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(muniVar) + " ,";           // Municipio
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(zonaVar) + " ,";           // Zona
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(mznaVar) + " ,";           // Manzana
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(loteVar) + " ,";           // Lote
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + edificioVar + "',";                  // Edificio
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + deptoVar + "',";                     // Depto 
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + Program.nombre_usuario + "',";       // NombreUsuario
                if (variablePorSiCopiaLatitud == 1)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLatitudG.Text.Trim() + "',";       // Latitud
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLongitudG.Text.Trim() + "',";      // Longitud
                }
                else
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLatitud.Text.Trim() + "',";       // Latitud
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLongitud.Text.Trim() + "',";      // Longitud
                }
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtObservaciones.Text.Trim() + "'";  // Comentario
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                con.cerrar_interno();

                MessageBox.Show("FUE INGRESADO EL TRÁMITE, CON ÉXITO, PREPARE LA IMPRESORA. YA QUE SE PROCEDE A IMPRIMIR", "INFORMACION", MessageBoxButtons.OK);

                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += (s, ev) =>
                {

                    Font font = new Font("Arial", 14, FontStyle.Bold);      // Configurar la fuente
                    string texto = $"FOLIO: {serie}-  {maxFolio} CAMBIO DE CLAVE  041-{zonaVar}-{mznaVar}-{loteVar}-{edificioVar}-{deptoVar}    {DateTime.Now}";        // Crear el texto a imprimir
                    ev.Graphics.DrawString(texto, font, Brushes.Black, new PointF(15, 20));     // Dibujar el texto en la página
                };

                // Iniciar la impresió
                printDoc.Print();
                return 1; // Retornar true si todo salió bien
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al insertar en CAT_NEW_CARTOGRAFIA_2025, CAT_DONDE_VA_2025, SONG_GEOLOCALIZACION ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }
        }

        private int certificadoLote()
        {
            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();
            String serie = Program.SerieC;
            string fechaIngreso = "";
            int maxFolio = 0;
            fechaIngreso = DateTime.Now.ToString("O");
            string fechaIngresos = fechaIngreso.Trim().Substring(0, 10);
            string fechasHora = fechaIngreso.Trim().Substring(11, 8);
            string fechaSql = fechaIngreso.Trim().Substring(0, 4) + fechaIngreso.Trim().Substring(5, 2) + fechaIngreso.Trim().Substring(8, 2);
            string fechaHoraSql = fechaSql + " " + fechasHora;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// OBTENEMOS EL MAXIMO FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = " SELECT MAX(FOLIO_ORIGEN) FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE serie = " + util.scm(serie);

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        maxFolio = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }

                if (maxFolio == 0) { maxFolio = 1; } else { maxFolio = maxFolio + 1; }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////  INSERTAR EN CAT_NEW_CARTOGRAFIA  ///////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            try
            {
                con.conectar_base_interno();

                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ESTADO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    MUNICIPIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ZONA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    MANZANA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    LOTE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    EDIFICIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    DEPTO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    UBICACION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    DESCRIPCION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FECHA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    HORA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    USUARIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    OBSERVACIONES,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TERR_PROPIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TERR_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AÑO_CALCULO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    frente,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    fondo,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    IRREGULARIDAD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TOPOGRAFIA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    POSICION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    INDIVISO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SUP_CON_COM,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SUP_CON,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    REGIMEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    COD_CALLE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ZON_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_TERRENO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_TERRENO_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_CONST,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_CONST_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_FRENTE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_FONDO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_IRREG,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_AREA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_TOPO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_POSICION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(muniVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(zonaVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(mznaVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(loteVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(edificioVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(deptoVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.tipoUbicacionCartografia + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("CERTIFICADO CLAVE") + ",";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(fechaSql) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(fechaHoraSql) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(Program.nombre_usuario) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(txtObservaciones.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.añoActual + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_interno = con.cadena_sql_interno + "    INSERT INTO CAT_DONDE_VA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    CARTOGRAFIA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VENTANILLA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    REVISO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SISTEMAS,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ELIMINADO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    1,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_interno = con.cadena_sql_interno + "    INSERT INTO SONG_GEOLOCALIZACION";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Estado,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Municipio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Zona,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Manzana,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Lote,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Edificio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Depto,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    NombreUsuario,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Latitud,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Longitud,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    COMENTARIO";
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VALUES";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + 15 + " ,";                                 // Estado
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(muniVar) + " ,";           // Municipio
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(zonaVar) + " ,";           // Zona
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(mznaVar) + " ,";           // Manzana
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + Convert.ToInt32(loteVar) + " ,";           // Lote
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + edificioVar + "',";                  // Edificio
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + deptoVar + "',";                     // Depto 
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + Program.nombre_usuario + "',";       // NombreUsuario
                if (variablePorSiCopiaLatitud == 1)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLatitudG.Text.Trim() + "',";       // Latitud
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLongitudG.Text.Trim() + "',";      // Longitud
                }
                else
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLatitud.Text.Trim() + "',";       // Latitud
                    con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtLongitud.Text.Trim() + "',";      // Longitud
                }
                con.cadena_sql_interno = con.cadena_sql_interno + "     " + "'" + txtObservaciones.Text.Trim() + "'";  // Comentario
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                con.cerrar_interno();

                MessageBox.Show("FUE INGRESADO EL TRÁMITE, CON ÉXITO, PREPARE LA IMPRESORA. YA QUE SE PROCEDE A IMPRIMIR", "INFORMACION", MessageBoxButtons.OK);

                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += (s, ev) =>
                {
                    Font font = new Font("Arial", 14, FontStyle.Bold);                                                                                              // Configurar la fuente
                    string texto = $"FOLIO: {serie}-  {maxFolio} CERTIFICADOS  041-{zonaVar}-{mznaVar}-{loteVar}-{edificioVar}-{deptoVar}    {DateTime.Now}";       // Crear el texto a imprimir
                    ev.Graphics.DrawString(texto, font, Brushes.Black, new PointF(15, 20));                                                                         // Dibujar el texto en la página
                };

                // Iniciar la impresió
                printDoc.Print();
                return 1; // Retornar true si todo salió bien

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 0; // Retornar false si ocurre un error
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int resultado = 0;

            if (txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            //esta variable se generó debido a que se solicitó que se pudieran copiar las coordenadas, 
            //por lo que, si no cuenta con coordenadas; se habilita la caja de texto para copiar 
            if (variablePorSiCopiaLatitud == 1)
            {
                if (txtLatitudG.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA LATITUD", "ERROR", MessageBoxButtons.OK); gMapControl1.Focus(); return; }
                if (txtLongitudG.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA LONGITUD", "ERROR", MessageBoxButtons.OK); gMapControl1.Focus(); return; }
            }
            else
            {
                if (txtLatitud.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA LATITUD", "ERROR", MessageBoxButtons.OK); gMapControl1.Focus(); return; }
                if (txtLongitud.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA LONGITUD", "ERROR", MessageBoxButtons.OK); gMapControl1.Focus(); return; }
            }
            if (txtObservaciones.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA OBSERVACION", "ERROR", MessageBoxButtons.OK); txtObservaciones.Focus(); return; }


            if (mtcInformacion.SelectedIndex == 0)
            {
                Program.tipoUbicacionCartografia = 0;
            }// 0 = inicio

            if (mtcInformacion.SelectedIndex == 1)
            {
                Program.tipoUbicacionCartografia = 1;
                resultado = AltaLote();
            }// 1 = alta de lote

            if (mtcInformacion.SelectedIndex == 2)
            {
                Program.tipoUbicacionCartografia = 2;
                resultado = cambiosLote();
            }// 2 = cambios de lote

            if (mtcInformacion.SelectedIndex == 3)
            {
                Program.tipoUbicacionCartografia = 3; // 3 = certificados
                resultado = certificadoLote();
            }// 3 = certificados


            if (resultado == 0) { return; }             // Si hubo un error en el alta, no continuar
            if (resultado == 1)                         // todo bien, se imprimió el documento y se retornó 
            {
                inicio2();
                mtcInformacion.SelectedIndex = 0;
                Program.tipoUbicacionCartografia = 0;   // 0 = inicio
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmCatastro01UbicacionAlta.ActiveForm.Opacity = 0.50;
            frmCatastro03BusquedaCatastro fs = new frmCatastro03BusquedaCatastro();
            fs.ShowDialog();
            //fs.Show();
            frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;

            if (Program.zonaV != "") { txtZona.Text = Program.zonaV.Trim(); }
            if (Program.manzanaV != "") { txtMzna.Text = Program.manzanaV.Trim(); }
            if (Program.loteV != "") { txtLote.Text = Program.loteV.Trim(); }
            if (Program.edificioV != "") { txtEdificio.Text = Program.edificioV.Trim(); }
            if (Program.deptoV != "") { txtDepto.Text = Program.deptoV.Trim(); }



        }
        private void txtZona_Enter_1(object sender, EventArgs e)
        {
            cajas_amarilla(15);
        }

        private void txtObservaciones_Leave_1(object sender, EventArgs e)
        {
            cajas_blancas(20);
        }

        private void txtObservaciones_Enter_1(object sender, EventArgs e)
        {
            cajas_amarilla(20);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
            gMapControl1.Position = new GMap.NET.PointLatLng(19.262174, -99.5330638);
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 14;
            gMapControl1.AutoScroll = true;
            gMapControl1.Visible = true;

            txtLatitud.Text = "";
            txtLongitud.Text = "";
            txtLatitudG.Text = "";
            txtLongitudG.Text = "";
        }

        private void frmCatastro01UbicacionAlta_Activated(object sender, EventArgs e)
        {
            mtcInformacion.Focus();
        }

        private void tabAlta()
        {
            inicio2();

            mtcInformacion.SelectedIndex = 1;
            txtZona.Enabled = true;
            txtMzna.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;

            btnConsulta.Enabled = true;
            btnBuscar.Enabled = true;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;

            Program.tipoUbicacionCartografia = 1; // 1 = alta de lote
            txtZona.Focus();
        }




        private void mtcInformacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mtcInformacion.SelectedIndex == 0)
            {
                inicio2();
                mtcInformacion.SelectedIndex = 0;
                Program.tipoUbicacionCartografia = 0; // 0 = inicio
            }// inicio

            if (mtcInformacion.SelectedIndex == 1)
            {
                tabAlta();
                txtZona.Focus();

            }// alta de lote

            if (mtcInformacion.SelectedIndex == 2)
            {
                inicio2();

                mtcInformacion.SelectedIndex = 2;
                txtZona.Enabled = true;
                txtMzna.Enabled = true;
                txtLote.Enabled = true;
                txtEdificio.Enabled = true;
                txtDepto.Enabled = true;

                btnConsulta.Enabled = true;
                btnBuscar.Enabled = true;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                ckbCambioNombre.Enabled = false;
                ckbCambioConstruccion.Enabled = false;
                ckbCambioSuperficie.Enabled = false;
                ckbCambioFactoresCons.Enabled = false;
                ckbCambioFactoresTerr.Enabled = false;

                Program.tipoUbicacionCartografia = 2; // 2 = cambios de lote
                txtZona.Focus();
            }// cambios de lote

            if (mtcInformacion.SelectedIndex == 3)
            {
                mtcInformacion.SelectedIndex = 3;
                txtZona.Enabled = true;
                txtMzna.Enabled = true;
                txtLote.Enabled = true;
                txtEdificio.Enabled = true;
                txtDepto.Enabled = true;

                btnConsulta.Enabled = true;
                btnBuscar.Enabled = true;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                Program.tipoUbicacionCartografia = 3; // 3 = certificados
                txtZona.Focus();
            }// certificados
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLatitud.Text) || string.IsNullOrWhiteSpace(txtLongitud.Text))
            {
                MessageBox.Show("Por favor, ingrese la latitud y longitud antes de abrir Google Maps.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string latitud2 = txtLatitud.Text.Trim();
            string longitud2 = txtLongitud.Text.Trim();

            //return $"https://www.google.com/maps?q={latitud},{longitud}";
            Process.Start($"https://www.google.com/maps?q={latitud2},{longitud2}");
            if (txtLatitudG.Text.Length == 16 && txtLongitudG.Text.Length == 16 )
            {
                string latitud = txtLatitud.Text.Trim();
                string longitud = txtLongitud.Text.Trim();

                //return $"https://www.google.com/maps?q={latitud},{longitud}";
                Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
            }
            else
            {
                MessageBox.Show("LAS COORDENADAS NO CUENTAN CON EL FORMATO CORRECTO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;

            //se posicionan en el txt de la latitud y longitud
            txtLatitud.Text = lat.ToString();
            txtLongitud.Text = lng.ToString();
            txtLatitudG.Text = lat.ToString();
            txtLongitudG.Text = lng.ToString();

        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
        }

        private void txtObservaciones_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtObservaciones_Leave_2(object sender, EventArgs e)
        {
            cajas_blancas(20);
        }

        private void txtObservaciones_Enter_2(object sender, EventArgs e)
        {
            cajas_amarilla(20);
        }

        private void cmdSalida_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdSalida, "SALIDA");
        }

        private void btnCancelar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnCancelar, "CANCELAR");
        }

        private void btnBuscar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnBuscar, "BUSCAR CLAVE CATASTRAL");
        }

        private void btnConsulta_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnConsulta, "CONSULTAR");
        }

        private void btnConstLote_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnConstLote, "CONTRUCCION PRIVADA");
        }

        private void btnConstComun_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnConstComun, "CONTRUCCION COMUN");
        }

        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnRefresh, "REFRESCAR MAPA");
        }

        private void btnMaps_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnMaps, "GOOGLE MAPS");
        }

        private void btnGuardar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnGuardar, "GUARDAR");
        }

        private void btnMinimizar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnMinimizar, "MINIMIZAR");
        }

        private void txtLatitudG_Enter(object sender, EventArgs e)
        {
            txtLatitudG.BackColor = Color.Yellow;
        }

        private void txtLongitudG_Enter(object sender, EventArgs e)
        {
            txtLongitud.BackColor = Color.Yellow;
        }

        private void txtLatitudG_Leave(object sender, EventArgs e)
        {
            txtLatitudG.BackColor = Color.White;
        }

        private void txtLongitudG_Leave(object sender, EventArgs e)
        {
            txtLongitudG.BackColor = Color.White;
        }

        private void txtZona_Leave_1(object sender, EventArgs e)
        {
            cajas_blancas(15);
            txtZonaOrigen.Text = txtZona.Text;
            txtMzna.Focus();
        }
        public void SetTextBoxValue(string value)
        {
            CONSTRUCCION = value;
            txtSupCont.Text = CONSTRUCCION;
        }



    }
}
