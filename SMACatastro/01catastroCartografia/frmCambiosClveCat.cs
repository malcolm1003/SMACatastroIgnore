using AccesoBase;
using SMACatastro.catastroCartografia;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Utilerias;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
//using Form = System.Windows.Forms.Form;




namespace SMACatastro.catastroSistemas
{
    public partial class frmCambiosClveCat : Form
    {
        int nivelDeUsuario = 1; // cambiar por variable de nivel de usuario
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();
        double valor_terreno_m, valor_terreno_comun_m, valor_construccion_m, valor_COMUN_m, INDIVISO_CAMBIO;
        int ESTADO_M, MUNICIPIO_M, ZONA_M, MANZANA_M, LOTE_M;
        string EDIFICIO_M, DEPTO_M;
        private bool focoEstablecido = false;
        //METODO PARA ARRASTRAR EL FORMULARIO-----------------------------------------------------------------------------------------------
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public frmCambiosClveCat()
        {
            InitializeComponent();
        }

        private void pnlDatosPredio_Paint(object sender, PaintEventArgs e)
        {

        }

        private void inicio()
        {
            limpiarTodo();
            ENABLEDGRAL();
            txtZona.Focus();

        }

        private void limpiarTodo()   /// limpiamos toda la pantalla
        {
            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";

            txtZonaD.Text = "";
            txtMzaD.Text = "";
            txtLoteD.Text = "";
            txtedifD.Text = "";
            txtDeptoD.Text = "";

            lblZonaO.Text = "";
            lblMzaO.Text = "";
            lblLoteO.Text = "";
            lblEdifO.Text = "";
            lblDeptoO.Text = "";

            lblInterior.Text = "";

            DGVRESULTADO.DataSource = null;
            DGVRESULTADO.Rows.Clear();

            //cboTipoPredio.Items.Clear();
            //cboEstadoPredio.Items.Clear();

            lblDomicilioPredio.Text = "";
            lblZonaOrigen.Text = "";
            lblCodigoCalle.Text = "";

            picDerechaUno.Visible = false;
            picDerechaDos.Visible = false;
            picDerechaTres.Visible = false;

            lblTipoPredio.Text = "";
            lblEstadoPredio.Text = "";
            lblCalle.Text = "";
            lblRegimenProp.Text = "";
            lblUbicacion.Text = "";
            lblSupConstruccion.Text = "";
            lblSupConsComun.Text = "";
            lblDesnivel.Text = "";
            llArea.Text = "";
            lblUsoSuelo.Text = "";
            lblDestino.Text = "";


            lblNoExterior.Text = "";
            lblEnCalle.Text = "";
            lblYcalle.Text = "";
            txtCodigoPostal.Text = "";
            lblColonia.Text = "";

            // cboRegimenPropiedad.Items.Clear();
            lblSupTerreno.Text = "";
            lblSupTerrenoComun.Text = "";
            lblFrente.Text = "";
            lblFondo.Text = "";
            lblObservaciones.Text = "";

            lblPropietario.Text = "";
            lblDomicilioPropietario.Text = "";
            lblDomicilioFiscal.Text = "";

            lblSupTerrenoPro.Text = "";
            lblSupTerrenoComunPro.Text = "";
            lblSupConstruccionPro.Text = "";
            lblSupConstruccionComunPro.Text = "";
            lblIndiviso.Text = "";

            //cboDestino.Items.Clear();
            lblValorTerrenoPropio.Text = "";
            lblValorTerrenoComun.Text = "";
            lblValorConstPropia.Text = "";
            lblValorConstComun.Text = "";
            lblValorCatastral.Text = "";
            lblObservacionPro.Text = "";

            MUNICIPIO_M = 0;
            ZONA_M = 0;
            MANZANA_M = 0;
            LOTE_M = 0;
            EDIFICIO_M = "";
            DEPTO_M = "";
            valor_terreno_m = 0;
            valor_terreno_comun_m = 0;
            valor_construccion_m = 0;
            valor_COMUN_m = 0;
            lblLatitud.Text = "";
            lblLonguitud.Text = "";

            txtZona.Focus();
        }

        private void ENABLEDGRAL()
        {
            btnMinimizar.Enabled = true;
            btnConsulta.Enabled = true;
            btnBuscar.Enabled = true;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;

            txtZona.Enabled = true;
            txtMzna.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;

            txtZonaD.Enabled = false;
            txtMzaD.Enabled = false;
            txtLoteD.Enabled = false;
            txtedifD.Enabled = false;
            txtDeptoD.Enabled = false;
            btnCambio.Enabled = false;
            btnMaps.Enabled = false;
            btnValidar.Enabled = false;

        }

        private void generales()
        {
            ENABLEDGRAL();
            limpiarTodo();

            txtZona.Enabled = true;
            txtMzna.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;

            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";

            btnConsulta.Enabled = true;
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;
        }

        private void frmMovimientosSistemas_Load(object sender, EventArgs e)
        {
            inicio();
            cajasColor();
            lblUsuario.Text = "Usuario: " + Program.nombre_usuario;
            nivelDeUsuario = 1;// cambiar por variable de nivel de usuario
        }

        private void GEOLOCALIZACION()
        {
            //OBTENEMOS LA GEOLOCALIZACION DEL PREDIO
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
                        lblLatitud.Text = con.leer_interno[0].ToString().Trim();
                        lblLonguitud.Text = con.leer_interno[1].ToString().Trim();
                        btnMaps.Enabled = true;
                    }
                }
                con.cerrar_interno();
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


        }
        private void cajasColor()
        {
            txtZona.Enter += util.TextBox_Enter;
            txtMzna.Enter += util.TextBox_Enter;
            txtLote.Enter += util.TextBox_Enter;
            txtEdificio.Enter += util.TextBox_Enter;
            txtDepto.Enter += util.TextBox_Enter;
            txtZonaD.Enter += util.TextBox_Enter;
            txtMzaD.Enter += util.TextBox_Enter;
            txtLoteD.Enter += util.TextBox_Enter;
            txtedifD.Enter += util.TextBox_Enter;
            txtDeptoD.Enter += util.TextBox_Enter;
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

        private void cmdSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
        }

        private void frmCambiosClveCat_Activated(object sender, EventArgs e)
        {
            // Establecer el foco en el TextBox solo la primera vez que se activa el formulario
            if (!focoEstablecido)
            {
                txtZona.Focus();
                focoEstablecido = true;
            }
        }

        private void txtEdificio_TextChanged(object sender, EventArgs e)
        {
            if (txtEdificio.Text.Length == 2) { txtDepto.Focus(); }
        }

        private void btnCambio_Click(object sender, EventArgs e)
        {
            if (txtZonaD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZonaD.Focus(); return; }
            if (txtZonaD.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZonaD.Focus(); return; }
            if (txtMzaD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtMzaD.Focus(); return; }
            if (txtMzaD.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtMzaD.Focus(); return; }
            if (txtLoteD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLoteD.Focus(); return; }
            if (txtLoteD.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLoteD.Focus(); return; }
            if (txtedifD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtedifD.Focus(); return; }
            if (txtedifD.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtedifD.Focus(); return; }
            if (txtDeptoD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDeptoD.Focus(); return; }
            if (txtDeptoD.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDeptoD.Focus(); return; }

            int MUNICIPIO_D = Convert.ToInt32(lblMunD.Text.Trim());
            int ZONA_D = Convert.ToInt32(txtZonaD.Text.Trim());
            int MANZANA_D = Convert.ToInt32(txtMzaD.Text.Trim());
            int LOTE_D = Convert.ToInt32(txtLoteD.Text.Trim());
            string EDIFICIO_D = txtedifD.Text.Trim();
            string DEPTO_D = txtDeptoD.Text.Trim();
            double indivisoClaveOrigen = 0;
            double indivisoSumatoria = 0;
            int FRACCIONAMIENTO = 0; //BANDERA PARA SABER SI ES UN FRACCIONAMIENTO O NO, 0 NO ES, 1 SI ES

            if (EDIFICIO_M != "00")
            //if (CONTADOR_FRACIONAMIENTO > 1)
            {
                if (DEPTO_M != "0000")
                {
                    try
                    {
                        //////////////VERIRFICAMOS SI EXISTE REGISTRO EN PREDIOS EN CLAVE DESTINO
                        int verificar = 0;
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT zona";
                        con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_D;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_D;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA_D;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE_D;
                        con.cadena_sql_interno = con.cadena_sql_interno + "           )";
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

                        if (verificar == 2)
                        {
                            MessageBox.Show("CLAVE PERTENECE A UN CONDOMINIO, NO EXISTE LOTE DESTINO, FAVOR DE REVISAR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtZonaD.Focus();
                            return;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }

                    try
                    {
                        //////////////VERIRFICAMOS SI EL INDIVISO EN CLAVE DESTINO NO EXCEDE EL 100%

                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + " SELECT SUM(PtjeCondom)";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   FROM PROPIEDADES";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE estado = " + Program.PEstado;
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND MUNICIPIO = " + MUNICIPIO_D;
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND ZONA = " + ZONA_D;
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND MANZANA = " + MANZANA_D;
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND LOTE = " + LOTE_D;
                        //con.cadena_sql_interno = con.cadena_sql_interno + "    AND EDIFICIO = " + EDIFICIO_D;
                        //con.cadena_sql_interno = con.cadena_sql_interno + "    AND DEPTO = " + DEPTO_D;


                        con.open_c_interno();
                        con.cadena_sql_cmd_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            if (con.leer_interno[0].ToString() != "")
                            {
                                indivisoSumatoria = Convert.ToDouble(con.leer_interno[0].ToString());
                            }
                            else
                            {
                                indivisoSumatoria = 0;
                            }

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

                    if (indivisoSumatoria < 100)
                    {
                        indivisoClaveOrigen = Convert.ToDouble(lblIndiviso.Text.ToString());
                        double SUMA_INDIVISO = indivisoSumatoria + indivisoClaveOrigen;
                        if (SUMA_INDIVISO > 100)
                        {
                            MessageBox.Show("NO SE PUEDE REALIZAR EL CAMBIO DE CLAVE, DADO QUE EL PORCENTAJE CONDOMINAL EXCEDE EL 100%", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtZonaD.Focus();
                            return;
                        }
                        FRACCIONAMIENTO = 1; //BANDERA PARA SABER QUE ES UN FRACCIONAMIENTO Y SI CUMPLE CON LOS REQUISITOS DE INDIVISOS, PARA EL PROCEDIMIENTO ALMACENADO

                    }
                    else
                    {
                        MessageBox.Show("NO SE PUEDE REALIZAR EL CAMBIO DE CLAVE, DADO QUE EL PORCENTAJE CONDOMINAL YA ESTA AL 100%", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtZonaD.Focus();
                        return;
                    }
                }

            }
            else // NO ES UN FRACCIONAMIENTO, ES UN LOTE
            {
                try
                {
                    //////////////VERIRFICAMOS SI EXISTE REGISTRO EN PREDIOS EN CLAVE DESTINO
                    int verificar = 0;
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT zona";
                    con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_D;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_D;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA_D;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE_D;
                    con.cadena_sql_interno = con.cadena_sql_interno + "           )";
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

                    if (verificar == 1)
                    {
                        MessageBox.Show("YA EXISTE LA CLAVE CATASTRAL DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtZonaD.Focus();
                        return;
                    }

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

            try
            {
                //////////////VERIRFICAMOS SI NO EXISTE REGISTRO EN UNID_CONST EN CLAVE DESTINO
                int verificar = 0;
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO_D);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO_D) + ")";
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
                    var existe = con.leer_interno[0].ToString();
                    verificar = Convert.ToInt32(existe);
                }
                con.cerrar_interno();

                if (verificar == 1)
                {
                    MessageBox.Show("EXISTE UN REGISTRO EN UNIDADES DE CONSTRUCCION DE LA CLAVE DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZonaD.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
            try
            {
                int validacion = 0;

                con.conectar_base_interno();
                con.open_c_interno();

                // SE MANDA A LLAMAR EL PROCEDIMIENTO ALMACENADO PARA REALIZAR EL CAMBIO DE CLAVE CATASTRAL
                SqlCommand cmd = new SqlCommand("SONG_CAMBIO_CLAVE", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@estadoOrigen", SqlDbType.Int, 2).Value = Program.PEstado;
                cmd.Parameters.Add("@municipioOrigen", SqlDbType.Int, 3).Value = MUNICIPIO_M;
                cmd.Parameters.Add("@zonaOrigen", SqlDbType.Int, 2).Value = Convert.ToInt32(ZONA_M);
                cmd.Parameters.Add("@manzanaOrigen", SqlDbType.Int, 3).Value = Convert.ToInt32(MANZANA_M);
                cmd.Parameters.Add("@loteOrigen", SqlDbType.Int, 2).Value = Convert.ToInt32(LOTE_M);
                cmd.Parameters.Add("@edificioOrigen", SqlDbType.VarChar, 2).Value = EDIFICIO_M;
                cmd.Parameters.Add("@deptoOrigen", SqlDbType.VarChar, 4).Value = DEPTO_M;
                cmd.Parameters.Add("@estadoDestino", SqlDbType.Int, 2).Value = Program.PEstado;
                cmd.Parameters.Add("@municipioDestino", SqlDbType.Int, 3).Value = MUNICIPIO_D;
                cmd.Parameters.Add("@zonaDestino", SqlDbType.Int, 2).Value = Convert.ToInt32(ZONA_D);
                cmd.Parameters.Add("@manzanaDestino", SqlDbType.Int, 3).Value = Convert.ToInt32(MANZANA_D);
                cmd.Parameters.Add("@loteDestino", SqlDbType.Int, 2).Value = Convert.ToInt32(LOTE_D);
                cmd.Parameters.Add("@edificioDestino", SqlDbType.VarChar, 2).Value = EDIFICIO_D;
                cmd.Parameters.Add("@deptoDestino", SqlDbType.VarChar, 4).Value = DEPTO_D;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 10).Value = Program.acceso_usuario;
                cmd.Parameters.Add("@fraccionamiento", SqlDbType.Int, 10).Value = FRACCIONAMIENTO;
                cmd.Parameters.Add("@validacion", SqlDbType.Int, 1).Direction = ParameterDirection.Output;

                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                validacion = Convert.ToInt32(cmd.Parameters["@validacion"].Value);
                con.cerrar_interno();

                if (validacion == 1)
                {
                    MessageBox.Show("LA CLAVE CATASTRAL SE CAMBIO CON EXITO", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    inicio();

                }
                else
                {
                    MessageBox.Show("NO SE REALIZO EL CAMBIO DE LA CLAVE CATASTRAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Threading.Thread.Sleep(500);
                CapturarPantallaConInformacion(ex);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }



        }


        //public void CapturarPantallaCompletaConEscala(Exception ex = null)
        //{
        //    Bitmap bitmapOriginal = null;
        //    Bitmap bitmapFinal = null;

        //    try
        //    {
        //        string carpetaCapturas = @"C:\SONGUI\CAPTURAS";
        //        Directory.CreateDirectory(carpetaCapturas);

        //        Rectangle virtualScreen = SystemInformation.VirtualScreen;
        //        float dpiScale = ObtenerFactorEscalaDPI();

        //        // 1. Captura original
        //        bitmapOriginal = new Bitmap(virtualScreen.Width, virtualScreen.Height);
        //        using (Graphics g = Graphics.FromImage(bitmapOriginal))
        //        {
        //            g.CopyFromScreen(virtualScreen.X, virtualScreen.Y, 0, 0, virtualScreen.Size);
        //        }

        //        // 2. Aplicar escala si es necesario
        //        if (dpiScale > 1.0f)
        //        {
        //            bitmapFinal = RedimensionarImagenDPI(bitmapOriginal, dpiScale);
        //            bitmapOriginal.Dispose(); // Liberar el original ya que usamos el redimensionado
        //            bitmapOriginal = null;
        //        }
        //        else
        //        {
        //            bitmapFinal = bitmapOriginal; // Usar el original
        //            bitmapOriginal = null; // Para evitar doble liberación
        //        }

        //        // 3. Agregar texto de error
        //        if (ex != null)
        //        {
        //            AgregarTextoError(bitmapFinal, ex);
        //        }

        //        // 4. Guardar
        //        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //        string filePath = Path.Combine(carpetaCapturas, $"captura_escala_{timestamp}.png");
        //        bitmapFinal.Save(filePath, ImageFormat.Png);
        //        Console.WriteLine($"Captura exitosa: {filePath}");
        //    }
        //    catch (Exception captureEx)
        //    {
        //        Console.WriteLine($"Error: {captureEx.Message}");
        //        CapturarPantallaSimple();
        //    }
        //    finally
        //    {
        //        // Liberar recursos en finally para garantizar que se ejecute
        //        bitmapOriginal?.Dispose();
        //        bitmapFinal?.Dispose();
        //    }
        //}


        //public void CapturarPantallaCompletaConEscala(Exception ex = null)
        //{
        //    try
        //    {
        //        string carpetaCapturas = @"C:\SONGUI\CAPTURAS";

        //        if (!Directory.Exists(carpetaCapturas))
        //        {
        //            Directory.CreateDirectory(carpetaCapturas);
        //        }

        //        Rectangle virtualScreen = SystemInformation.VirtualScreen;
        //        float dpiScale = ObtenerFactorEscalaDPI();

        //        // Crear bitmap fuera del using para poder redimensionarlo
        //        Bitmap bitmapFinal;

        //        // Captura inicial
        //        using (Bitmap bitmapOriginal = new Bitmap(virtualScreen.Width, virtualScreen.Height))
        //        {
        //            using (Graphics g = Graphics.FromImage(bitmapOriginal))
        //            {
        //                g.CopyFromScreen(virtualScreen.X, virtualScreen.Y, 0, 0,
        //                               virtualScreen.Size, CopyPixelOperation.SourceCopy);
        //            }

        //            // Aplicar redimensionamiento si es necesario
        //            if (dpiScale > 1.0f)
        //            {
        //                bitmapFinal = RedimensionarImagenDPI(bitmapOriginal, dpiScale);
        //            }
        //            else
        //            {
        //                bitmapFinal = new Bitmap(bitmapOriginal); // Clonar
        //            }
        //        }

        //        // Agregar texto de error si existe
        //        if (ex != null)
        //        {
        //            AgregarTextoError(bitmapFinal, ex);
        //        }

        //        // Guardar imagen
        //        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        //        string nombreArchivo = $"captura_escala_{timestamp}.png";
        //        string filePath = Path.Combine(carpetaCapturas, nombreArchivo);

        //        bitmapFinal.Save(filePath, ImageFormat.Png);
        //        bitmapFinal.Dispose(); // Liberar memoria

        //        Console.WriteLine($"Captura guardada: {filePath}");
        //    }
        //    catch (Exception captureEx)
        //    {
        //        Console.WriteLine($"Error: {captureEx.Message}");
        //        CapturarPantallaSimple();
        //    }
        //}
        public void CapturarPantallaCompletaConEscala(Exception ex = null)
        {
            Bitmap bitmapFinal = null; // Variable separada para el bitmap final

            try
            {
                string carpetaCapturas = @"C:\SONGUI\CAPTURAS";

                if (!Directory.Exists(carpetaCapturas))
                {
                    Directory.CreateDirectory(carpetaCapturas);
                }

                // 1. OBTENER INFORMACIÓN DE LA PANTALLA VIRTUAL
                Rectangle virtualScreen = SystemInformation.VirtualScreen;

                // 2. OBTENER FACTOR DE ESCALA DPI
                float dpiScale = ObtenerFactorEscalaDPI();

                Console.WriteLine($"DPI Scale: {dpiScale}");
                Console.WriteLine($"Virtual Screen: {virtualScreen}");

                // 3. CREAR BITMAP TEMPORAL PARA CAPTURA
                using (Bitmap bitmapTemp = new Bitmap(virtualScreen.Width, virtualScreen.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmapTemp))
                    {
                        // 4. CAPTURAR PANTALLA COMPLETA
                        g.CopyFromScreen(virtualScreen.X, virtualScreen.Y, 0, 0,
                                       virtualScreen.Size, CopyPixelOperation.SourceCopy);
                    }

                    // 5. DETERMINAR QUÉ BITMAP USAR (redimensionado o original)
                    if (dpiScale > 1.0f)
                    {
                        // Crear nuevo bitmap redimensionado
                        bitmapFinal = RedimensionarImagenDPI(bitmapTemp, dpiScale);
                    }
                    else
                    {
                        // Clonar el bitmap original
                        bitmapFinal = new Bitmap(bitmapTemp);
                    }
                }

                // 6. AGREGAR INFORMACIÓN DEL ERROR (si se proporciona)
                if (ex != null)
                {
                    AgregarTextoError(bitmapFinal, ex);
                }

                // 7. GUARDAR CAPTURA
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string nombreArchivo = $"captura_escala_{timestamp}.png";
                string filePath = Path.Combine(carpetaCapturas, nombreArchivo);

                bitmapFinal.Save(filePath, ImageFormat.Png);
                Console.WriteLine($"Captura con escala guardada: {filePath}");
            }
            catch (Exception captureEx)
            {
                Console.WriteLine($"Error al capturar pantalla: {captureEx.Message}");
                // Fallback: intentar método simple
                CapturarPantallaSimple();
            }
            finally
            {
                // Asegurar que el bitmap final se libere
                bitmapFinal?.Dispose();
            }
        }



        // Obtener factor de escala DPI del sistema
        private float ObtenerFactorEscalaDPI()
        {
            try
            {
                using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    float dpiX = g.DpiX;
                    return dpiX / 96.0f; // 96 DPI = 100% escala
                }
            }
            catch
            {
                return 1.0f; // Valor por defecto si hay error
            }
        }

        // Redimensionar imagen considerando DPI
        private Bitmap RedimensionarImagenDPI(Bitmap original, float escalaDPI)
        {
            try
            {
                // Calcular nuevo tamaño (reducir según la escala)
                int nuevoAncho = (int)(original.Width / escalaDPI);
                int nuevoAlto = (int)(original.Height / escalaDPI);

                Bitmap redimensionada = new Bitmap(nuevoAncho, nuevoAlto);

                using (Graphics g = Graphics.FromImage(redimensionada))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                    g.DrawImage(original, 0, 0, nuevoAncho, nuevoAlto);
                }

                return redimensionada;
            }
            catch
            {
                return original; // Devolver original si hay error
            }
        }

        // Agregar texto de error a la imagen
        private void AgregarTextoError(Bitmap bitmap, Exception ex)
        {
            try
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                using (Font font = new Font("Arial", 14, FontStyle.Bold))
                using (Brush textoBrush = new SolidBrush(Color.Red))
                using (Brush fondoBrush = new SolidBrush(Color.FromArgb(220, Color.White)))
                {
                    string infoError = $"ERROR: {ex.Message}\n" +
                                     $"Fecha: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                                     $"Usuario: {Program.acceso_usuario}\n" +
                                     $"Tipo: {ex.GetType().Name}";

                    // Medir texto
                    SizeF tamañoTexto = g.MeasureString(infoError, font);

                    // Crear fondo para texto
                    RectangleF rectFondo = new RectangleF(20, 20, tamañoTexto.Width + 20, tamañoTexto.Height + 20);
                    g.FillRectangle(fondoBrush, rectFondo);

                    // Borde del fondo
                    g.DrawRectangle(Pens.Black, 20, 20, tamañoTexto.Width + 20, tamañoTexto.Height + 20);

                    // Dibujar texto
                    g.DrawString(infoError, font, textoBrush, 30, 30);
                }
            }
            catch (Exception textEx)
            {
                Console.WriteLine($"Error al agregar texto: {textEx.Message}");
            }
        }

        // Método de respaldo simple
        private void CapturarPantallaSimple()
        {
            try
            {
                string carpetaCapturas = @"C:\SONGUI\CAPTURAS";

                if (!Directory.Exists(carpetaCapturas))
                    Directory.CreateDirectory(carpetaCapturas);

                Rectangle bounds = Screen.PrimaryScreen.Bounds;

                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
                    }

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string filePath = Path.Combine(carpetaCapturas, $"captura_simple_{timestamp}.png");
                    bitmap.Save(filePath, ImageFormat.Png);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en captura simple: {ex.Message}");
            }
        }











        // Método especializado para capturar pantalla con información de la excepción
        public void CapturarPantallaConInformacion(Exception ex)
        {
            try
            {
                string carpetaCapturas = @"C:\SONGUI\CAPTURAS";

                if (!Directory.Exists(carpetaCapturas))
                {
                    Directory.CreateDirectory(carpetaCapturas);
                }

                Rectangle bounds = Screen.PrimaryScreen.Bounds;

                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);

                        // Agregar información detallada del error en la imagen
                        using (Font font = new Font("Arial", 12))
                        using (Brush brush = new SolidBrush(Color.Red))
                        {
                            string infoError = $"EXCEPCIÓN CAPTURADA - {DateTime.Now}\n" +
                                             $"Mensaje: {ex.Message}\n" +
                                             $"Tipo: {ex.GetType().Name}\n" +
                                             $"Usuario: {Program.acceso_usuario}\n" +
                                             $"Proceso: Cambio Clave Catastral\n" +
                                             $"Origen: {ex.Source}\n" +
                                             $"Stack Trace: {ex.StackTrace?.Substring(0, Math.Min(200, ex.StackTrace.Length))}...";

                            // Fondo semitransparente para el texto
                            using (Brush backgroundBrush = new SolidBrush(Color.FromArgb(200, Color.White)))
                            {
                                g.FillRectangle(backgroundBrush, 10, 10, 600, 150);
                            }

                            g.DrawString(infoError, font, brush, new RectangleF(15, 15, 590, 140));
                        }
                    }

                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string nombreArchivo = $"error_excepcion_{timestamp}.png";
                    string filePath = Path.Combine(carpetaCapturas, nombreArchivo);

                    bitmap.Save(filePath, ImageFormat.Png);

                    Console.WriteLine($"Captura de excepción guardada: {filePath}");
                }
            }
            catch (Exception captureEx)
            {
                Console.WriteLine($"Error al capturar pantalla de excepción: {captureEx.Message}");
            }
        }




        private void txtZonaD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtMzaD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtLoteD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtZonaD_TextChanged(object sender, EventArgs e)
        {
            if (txtZonaD.Text.Length == 2) { txtMzaD.Focus(); }
        }

        private void label77_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMzaD_TextChanged(object sender, EventArgs e)
        {
            if (txtMzaD.Text.Length == 3) { txtLoteD.Focus(); }
        }

        private void txtLoteD_TextChanged(object sender, EventArgs e)
        {
            if (txtLoteD.Text.Length == 2) { txtedifD.Focus(); }
        }

        private void txtedifD_TextChanged(object sender, EventArgs e)
        {
            if (txtedifD.Text.Length == 2) { txtDeptoD.Focus(); }
        }

        private void txtDeptoD_TextChanged(object sender, EventArgs e)
        {
            if (txtDeptoD.Text.Length == 4) { btnValidar.Focus(); }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblLatitud.Text) || string.IsNullOrWhiteSpace(lblLonguitud.Text))
            {
                MessageBox.Show("Por favor, ingrese la latitud y longitud antes de abrir Google Maps.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string latitud = lblLatitud.Text.Trim();
            string longitud = lblLonguitud.Text.Trim();

            //return $"https://www.google.com/maps?q={latitud},{longitud}";
            Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
        }

        private void btnConsulta_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnConsulta, "CONSULTAR CLAVE CATASTRAL");
        }

        private void btnBuscar_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnBuscar, "BUSCAR CLAVE CATASTRAL");
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (txtZonaD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZonaD.Focus(); return; }
            if (txtZonaD.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZonaD.Focus(); return; }
            if (txtMzaD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtMzaD.Focus(); return; }
            if (txtMzaD.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtMzaD.Focus(); return; }
            if (txtLoteD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLoteD.Focus(); return; }
            if (txtLoteD.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLoteD.Focus(); return; }
            if (txtedifD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtedifD.Focus(); return; }
            if (txtedifD.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtedifD.Focus(); return; }
            if (txtDeptoD.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDeptoD.Focus(); return; }
            if (txtDeptoD.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDeptoD.Focus(); return; }

            int MUNICIPIO_D = Convert.ToInt32(lblMunD.Text.Trim());
            int ZONA_D = Convert.ToInt32(txtZonaD.Text.Trim());
            int MANZANA_D = Convert.ToInt32(txtMzaD.Text.Trim());
            int LOTE_D = Convert.ToInt32(txtLoteD.Text.Trim());
            string EDIFICIO_D = txtedifD.Text.Trim();
            string DEPTO_D = txtDeptoD.Text.Trim();

            try
            {
                //////////////VERIRFICAMOS SI NO EXISTE REGISTRO EN PROPIEDADES EN CLAVE DESTINO
                int verificar = 0;
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO_D);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO_D) + ")";
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
                    var existe = con.leer_interno[0].ToString();
                    verificar = Convert.ToInt32(existe);
                }
                con.cerrar_interno();

                if (verificar == 1)
                {
                    MessageBox.Show("EXISTE UN REGISTRO EN PROPIEDADES LA CLAVE DESTINO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZonaD.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }

            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA SI NO EXISTE REGISTRO EN PREDIOS EN CLAVE DESTINO
                int verificar = 0;
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM MANZANAS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_D;
                con.cadena_sql_interno = con.cadena_sql_interno + "           )";
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
                    var existe = con.leer_interno[0].ToString();
                    verificar = Convert.ToInt32(existe);
                }
                con.cerrar_interno();

                if (verificar == 2)
                {
                    MessageBox.Show("NO EXISTE LA MANZANA DESTINO, FAVOR DE CREARLA ANTES DE EL CAMBIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZonaD.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }

            MessageBox.Show("NO EXISTE LA CLAVE CATASTRAL DESTINO, PUEDE PROCEDER CON EL CAMBIO", "INFORME", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnCambio.Enabled = true;
            btnValidar.Enabled = false;


        }

        private void btnCancelar_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnCancelar, "CANCELAR");
        }

        private void cmdSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(cmdSalida, "SALIR");
        }

        private void btnValidar_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnValidar, "VALIDAR CLAVE CATASTRAL DESTINO");
        }

        private void btnCambio_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnCambio, "REALIZAR CAMBIO DE CLAVE CATASTRAL");
        }

        private void btnMinimizar_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnMinimizar, "MINIMIZAR");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Crear y mostrar formulario
            frmCatastro03BusquedaCatastro bsuqueda = new frmCatastro03BusquedaCatastro();
            bsuqueda.ShowDialog(); // No modal, no bloquea
            txtZona.Text = Program.zonaV;
            txtMzna.Text = Program.manzanaV;
            txtLote.Text = Program.loteV;
            txtEdificio.Text = Program.edificioV;
            txtDepto.Text = Program.deptoV;
            // O mostrar sin crear variable explícita
            //new Form2().Show();
            consulta();
            btnConsulta.Enabled = false;
        }

        private void txtDepto_TextChanged(object sender, EventArgs e)
        {
            if (txtDepto.Text.Length == 4) { btnConsulta.Focus(); }
        }

        private void txtZona_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            inicio();

        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            consulta();
        }

        private void consulta()
        {

            int tipo_predio = 0;
            double superficie = 0;
            string domicilio1 = string.Empty;
            int ZONA_ORI1 = 0;
            int COD_CALLE1 = 0;
            string CALLES1 = string.Empty;
            string NUM_EXT1 = string.Empty;
            string CALLE_11 = string.Empty;
            string CALLE_21 = string.Empty;
            int cp1 = 0;
            int COLONIA1 = 0;
            string REGIMEN1 = "";
            string UBICACION1 = "";
            double SUP_TERRENO1 = 0.0;
            double SUP_CONST1 = 0.0;
            double SUP_TERRENO_C1 = 0.0;
            double SUP_CONST_C1 = 0.0;
            double frente1 = 0.0;
            double fondo1 = 0.0;
            double DESNIVEL1 = 0.0;
            double AREA_INS1 = 0.0;
            double OBSERVA1 = 0.0;
            string NO_INT2 = string.Empty;
            string PROPIETARIO1 = string.Empty;
            string TELEFONO1 = string.Empty;
            string DOM_FIS1 = string.Empty;
            string USO1 = string.Empty;
            string DESTINO1 = string.Empty;
            double TERR_PRO1 = 0.0;

            double TERR_COM1 = 0.0;
            double CONS_PRO1 = 0.0;
            double CONS_COMUN1 = 0.0;
            double INDIVISO1 = 0.0;
            string OBSERVA21 = string.Empty;
            double VALOR_CATASTRAL1 = 0.0;
            string DOMICILIO2 = string.Empty;

            if (txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDepto.Focus(); return; }


            MUNICIPIO_M = Convert.ToInt32(lblMun.Text.Trim());
            ZONA_M = Convert.ToInt32(txtZona.Text.Trim());
            MANZANA_M = Convert.ToInt32(txtMzna.Text.Trim());
            LOTE_M = Convert.ToInt32(txtLote.Text.Trim());
            EDIFICIO_M = txtEdificio.Text.Trim();
            DEPTO_M = txtDepto.Text.Trim();

            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE
                con.conectar_base_interno();
                con.open_c_interno();
                SqlCommand cmd = new SqlCommand("SONG_CLAVE_BLOQUE_1_2", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                //**** PARAMETROS DE ENTRADA ****//
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
                cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 3).Value = MUNICIPIO_M;
                cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = ZONA_M;
                cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = MANZANA_M;
                cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = LOTE_M;
                cmd.Parameters.Add("@EDIFICIO", SqlDbType.VarChar, 2).Value = EDIFICIO_M;
                cmd.Parameters.Add("@DEPTO", SqlDbType.VarChar, 4).Value = DEPTO_M;
                cmd.Parameters.Add("@VALIDADOR", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();
                con.cerrar_interno();
                if (Convert.ToInt32(cmd.Parameters["@VALIDADOR"].Value) == 1)
                {
                    MessageBox.Show("LA CLAVE CATASTRAL CUENTA CON UN BLOQUEO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);

                return; // Retornar false si ocurre un error
            }

            try
            {
                //**** procedemos a buscar la informacion para llenar las etiquetas de la pantalla ***//
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "     SELECT ";// predios
                con.cadena_sql_interno = con.cadena_sql_interno + "           pr.TipoPredio , pr.Domicilio , pr.ZonaOrig , pr.CodCalle , c.NomCalle,";
                con.cadena_sql_interno = con.cadena_sql_interno + "           pr.NumExt , pr.EntCalle ,pr.YCalle, pr.CodPost ,co.Colonia, co.NomCol, pr.RegProp ,re.Descr ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "           pr.Ubicacion,f.DescFact,pr.SupTerrTot,pr.SupCons ,pr.SupTerrCom ,pr.SupConsCom ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "           pr.Frente , pr.Fondo , pr.Desnivel , pr.AreaInscr , pr.cObsPred ,";
                ///propiedades
                con.cadena_sql_interno = con.cadena_sql_interno + "           p.NumIntP, p.PmnProp , p.DomFis,p.Uso, d.UsoEsp ,d.Descrip, p.STerrProp ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "           p.STerrCom ,p.SConsProp ,p.SConsCom ,p.PtjeCondom , p.cObsProp";
                con.cadena_sql_interno = con.cadena_sql_interno + "       FROM PROPIEDADES p, PREDIOS pr, CALLES c, COLONIAS co, REGIMEN re, FACTORES f, DESTINO d";
                con.cadena_sql_interno = con.cadena_sql_interno + "       WHERE p.Estado =" + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Municipio =" + Program.municipioN;
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Zona =" + ZONA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Manzana =" + MANZANA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Lote =" + LOTE_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Edificio =" + util.scm(EDIFICIO_M);
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Depto =" + util.scm(DEPTO_M);
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Estado = pr.Estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Municipio = pr.Municipio";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Zona = pr.Zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Manzana = pr.Manzana";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Lote = pr.Lote";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.Estado = c.Estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.Municipio = c.Municipio";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.ZonaOrig = c.ZonaOrig";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.CodCalle = c.CodCalle";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.Estado = co.Estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.Municipio = co.Municipio";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.Colonia = co.Colonia";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.RegProp = re.RegProp";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND pr.Ubicacion = f.NumFactor";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND f.AnioVigMD = " + Program.añoActual;
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND f.TipoMerDem = 6";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.Uso = d.Uso";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND p.UsoEsp = d.UsoEsp";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                if (!con.leer_interno.HasRows) { MessageBox.Show("NO SE ENCONTRO REGISTRO DE CLAVE CATASTRAL", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error); con.cerrar_interno(); txtZona.Focus(); return; }
                while (con.leer_interno.Read())
                {
                    ///*** etiquetas predios ***//
                    tipo_predio = Convert.ToInt32(con.leer_interno["TipoPredio"].ToString().Trim());
                    // lblEstadoPredio.Text = con.leer_interno["DescFact"].ToString().Trim();
                    lblDomicilioPredio.Text = con.leer_interno["Domicilio"].ToString().Trim();
                    lblZonaOrigen.Text = con.leer_interno["ZonaOrig"].ToString().Trim();
                    lblCodigoCalle.Text = con.leer_interno["CodCalle"].ToString().Trim();
                    lblCalle.Text = con.leer_interno["NomCalle"].ToString().Trim();
                    lblNoExterior.Text = con.leer_interno["NumExt"].ToString().Trim();
                    lblEnCalle.Text = con.leer_interno["EntCalle"].ToString().Trim();
                    lblYcalle.Text = con.leer_interno["YCalle"].ToString().Trim();
                    txtCodigoPostal.Text = con.leer_interno["CodPost"].ToString().Trim();
                    lblColonia.Text = con.leer_interno["NomCol"].ToString().Trim();
                    lblRegimenProp.Text = con.leer_interno["RegProp"].ToString().Trim() + " " + con.leer_interno["Descr"].ToString().Trim();
                    lblUbicacion.Text = con.leer_interno["Ubicacion"].ToString().Trim() + " " + con.leer_interno["DescFact"].ToString().Trim();
                    lblSupTerreno.Text = Convert.ToDouble(con.leer_interno["SupTerrTot"].ToString().Trim()).ToString("N2");
                    lblSupTerrenoComun.Text = Convert.ToDouble(con.leer_interno["SupTerrCom"].ToString().Trim()).ToString("N2");
                    lblFrente.Text = Convert.ToDouble(con.leer_interno["Frente"].ToString().Trim()).ToString("N2");
                    lblFondo.Text = Convert.ToDouble(con.leer_interno["Fondo"].ToString().Trim()).ToString("N2");
                    lblSupConstruccion.Text = Convert.ToDouble(con.leer_interno["SupCons"].ToString().Trim()).ToString("N2");
                    superficie = Convert.ToDouble(con.leer_interno["SupCons"].ToString().Trim());
                    lblSupConsComun.Text = Convert.ToDouble(con.leer_interno["SupConsCom"].ToString().Trim()).ToString("N2");
                    lblDesnivel.Text = con.leer_interno["Desnivel"].ToString().Trim();
                    llArea.Text = con.leer_interno["AreaInscr"].ToString().Trim();
                    lblObservaciones.Text = con.leer_interno["cObsPred"].ToString().Trim();
                    ///*** etiquetas propiedades ***//
                    lblInterior.Text = con.leer_interno["NumIntP"].ToString().Trim();
                    lblPropietario.Text = con.leer_interno["PmnProp"].ToString().Trim();
                    lblDomicilioPropietario.Text = con.leer_interno["DomFis"].ToString().Trim();
                    lblDomicilioFiscal.Text = con.leer_interno["DomFis"].ToString().Trim();
                    lblUsoSuelo.Text = con.leer_interno["Uso"].ToString().Trim() + " " + con.leer_interno["Descrip"].ToString().Trim();
                    lblSupTerrenoPro.Text = Convert.ToDouble(con.leer_interno["STerrProp"].ToString().Trim()).ToString("N2");
                    lblSupTerrenoComunPro.Text = Convert.ToDouble(con.leer_interno["STerrCom"].ToString().Trim()).ToString("N2");
                    lblSupConstruccionPro.Text = Convert.ToDouble(con.leer_interno["SConsProp"].ToString().Trim()).ToString("N2");
                    lblSupConstruccionComunPro.Text = Convert.ToDouble(con.leer_interno["SConsCom"].ToString().Trim()).ToString("N2");
                    lblIndiviso.Text = con.leer_interno["PtjeCondom"].ToString().Trim();
                    INDIVISO_CAMBIO = Convert.ToDouble(con.leer_interno["PtjeCondom"].ToString().Trim());
                    lblDestino.Text = con.leer_interno["UsoEsp"].ToString().Trim() + " " + con.leer_interno["Descrip"].ToString().Trim();
                    lblObservacionPro.Text = con.leer_interno["cObsProp"].ToString().Trim();
                }
                con.cerrar_interno();/// cerramos conexion
            }
            catch (Exception ex)
            {
                CapturarPantallaConInformacion(ex);
                MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // Retornar false si ocurre un error
            }

            if (tipo_predio == 1)
            {
                lblTipoPredio.Text = "1 URBANO";
            }
            else if (tipo_predio == 2)
            {
                lblTipoPredio.Text = "2 RUSTICO";
            }

            if (superficie > 0)
            {
                lblEstadoPredio.Text = "1 CONSTUIDO";
            }
            else
            {
                lblEstadoPredio.Text = "0 BALDIO";
            }

            try
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("N19_CONSULTA_PREDIO", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ESTADO2", SqlDbType.Int, 2).Value = 15;
                cmd.Parameters.Add("@MUNICIPIO2", SqlDbType.Int, 3).Value = 41;
                cmd.Parameters.Add("@ZONA2", SqlDbType.Int, 2).Value = Convert.ToInt32(ZONA_M);
                cmd.Parameters.Add("@MANZANA2", SqlDbType.Int, 3).Value = Convert.ToInt32(MANZANA_M);
                cmd.Parameters.Add("@LOTE2", SqlDbType.Int, 2).Value = Convert.ToInt32(LOTE_M);
                cmd.Parameters.Add("@EDIFICIO2", SqlDbType.VarChar, 2).Value = EDIFICIO_M;
                cmd.Parameters.Add("@DEPTO2", SqlDbType.VarChar, 4).Value = DEPTO_M;


                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    valor_terreno_m = Convert.ToDouble(rdr["VALOR_TERRENO_P"].ToString().Trim());
                    valor_terreno_comun_m = Convert.ToDouble(rdr["valor_terreno_c"].ToString().Trim());
                    valor_construccion_m = Convert.ToDouble(rdr["valor_construccion_p"].ToString().Trim());
                    valor_COMUN_m = Convert.ToDouble(rdr["valor_construccion_c"].ToString().Trim());
                    VALOR_CATASTRAL1 = Convert.ToDouble(rdr["VALOR_CATASTRAL"].ToString().Trim());

                    lblValorTerrenoPropio.Text = valor_terreno_m.ToString("N2");
                    lblValorTerrenoComun.Text = valor_terreno_comun_m.ToString("N2");
                    lblValorConstPropia.Text = valor_construccion_m.ToString("N2");
                    lblValorConstComun.Text = valor_COMUN_m.ToString("N2");
                    lblValorCatastral.Text = VALOR_CATASTRAL1.ToString("N2");

                }


                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }

            lblMunO.Text = MUNICIPIO_M.ToString().PadLeft(3, '0');
            lblZonaO.Text = ZONA_M.ToString().PadLeft(2, '0');
            lblMzaO.Text = MANZANA_M.ToString().PadLeft(3, '0');
            lblLoteO.Text = LOTE_M.ToString().PadLeft(2, '0');
            lblEdifO.Text = EDIFICIO_M.ToString().PadLeft(2, '0');
            lblDeptoO.Text = DEPTO_M.ToString().PadLeft(4, '0');

            GEOLOCALIZACION();
            PREDIALPAGO();
            btnConsulta.Enabled = false;
            btnBuscar.Enabled = false;

            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;

            picDerechaUno.Visible = true;
            picDerechaDos.Visible = true;
            picDerechaTres.Visible = true;

            txtZonaD.Enabled = true;
            txtMzaD.Enabled = true;
            txtLoteD.Enabled = true;
            txtedifD.Enabled = true;
            txtDeptoD.Enabled = true;
            btnCambio.Enabled = false;
            btnValidar.Enabled = true;
            txtZonaD.Focus();
        }
        private void PREDIALPAGO()
        {
            try
            {
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT R.Serie, R.Folio, R.FecCob, RTRIM(R.Suerte), R.UltMesPag, R.UltAnioPag, R.Total";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM RECIBOS R";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE R.MUNICIPIO = " + MUNICIPIO_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.ZONA = " + ZONA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.MANZANA = " + MANZANA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.LOTE = " + LOTE_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.EDIFICIO = " + util.scm(EDIFICIO_M);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.DEPTO = " + util.scm(DEPTO_M);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.STATUS IN  ('A', 'E')";
                con.cadena_sql_interno = con.cadena_sql_interno + "ORDER BY R.Serie DESC";

                DataTable LLENAR_GRID_1 = new DataTable();
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
                {
                    lblNumRegistro.Text = "NO SE ENCONTRARON PAGOS DE LA CLAVE CATASTRAL"; //Se limpia el label de conteo de registros
                }
                else //en caso de encontrar un dato, se realiza toda la acción de abajo 
                {
                    // da.Fill(dt);
                    DGVRESULTADO.DataSource = LLENAR_GRID_1;

                    DGVRESULTADO.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    DGVRESULTADO.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    //dgResultado.EnableHeadersVisualStyles = false; // Desactiva estilos predeterminados
                    DGVRESULTADO.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                    DGVRESULTADO.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    foreach (DataGridViewColumn columna in DGVRESULTADO.Columns)
                    {
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    foreach (DataGridViewColumn columna in DGVRESULTADO.Columns)
                    {
                        columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Configuración de selección
                    DGVRESULTADO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    // dgResultado.MultiSelect = false; // Solo permitir selección de una fila a la vez

                    // Deshabilitar edición
                    DGVRESULTADO.ReadOnly = true;
                    // Estilos visuales
                    DGVRESULTADO.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    DGVRESULTADO.DefaultCellStyle.SelectionForeColor = Color.Black;
                    //Para los encabezados del datagridview
                    DGVRESULTADO.Columns[0].HeaderText = "SERIE";                      //          
                    DGVRESULTADO.Columns[1].HeaderText = "FOLIO";                      // 
                    DGVRESULTADO.Columns[2].HeaderText = "FECHA DE COBRO";
                    DGVRESULTADO.Columns[3].HeaderText = "CONCEPTO";       // 
                    DGVRESULTADO.Columns[4].HeaderText = "MES";                        // 
                    DGVRESULTADO.Columns[5].HeaderText = "AÑO";               //
                    DGVRESULTADO.Columns[6].HeaderText = "TOTAL";                  //

                    //DGVRESULTADO.Columns[0].Width = 50; // Ajusta el ancho de la columna SERIE
                    //DGVRESULTADO.Columns[1].Width = 50; // Ajusta el ancho de la columna FOLIO
                    //DGVRESULTADO.Columns[2].Width = 180; // Ajusta el ancho de la columna CLAVE CATASTRAL
                    //DGVRESULTADO.Columns[3].Width = 50; // Ajusta el ancho de la columna NOMBRE DEL PROPÍETARIO
                    //DGVRESULTADO.Columns[4].Width = 50; // Ajusta el ancho de la columna DOMICILIO
                    //DGVRESULTADO.Columns[5].Width = 100; // Ajusta el ancho de la columna VALOR CATASTRAL
                    int CONTEO;
                    CONTEO = DGVRESULTADO.Rows.Count - 1;
                    lblNumRegistro.Text = CONTEO.ToString(); //Se limpia el label de conteo de registros
                    DGVRESULTADO.Enabled = true;

                    con.cerrar_interno(); //Cerramos la conexión después de llenar el DataTable
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }
        }

    }
}
