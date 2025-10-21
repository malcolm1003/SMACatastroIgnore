using AccesoBase;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using Utilerias;

namespace SMACatastro.catastroCartografia
{
    public partial class frmCatastro03BusquedaCatastro : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();

        ////////////////////////////////////////////////////////////
        ///////////////// -------PARA ARRASTRAR EL PANEL 
        ////////////////////////////////////////////////////////////
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public frmCatastro03BusquedaCatastro()
        {
            InitializeComponent();
        }

        private void cajasColor()
        {
            txtZona.Enter += util.TextBox_Enter;
            txtMzna.Enter += util.TextBox_Enter;
            txtLote.Enter += util.TextBox_Enter;
            txtEdificio.Enter += util.TextBox_Enter;
            txtDepto.Enter += util.TextBox_Enter;
            txtZonaDos.Enter += util.TextBox_Enter;
            txtMznaDos.Enter += util.TextBox_Enter;
            txtLoteDos.Enter += util.TextBox_Enter;
            txtEdificioDos.Enter += util.TextBox_Enter;
            txtDeptoDos.Enter += util.TextBox_Enter;

            txtpersona.Enter += util.TextBox_Enter;
            txtCalle.Enter += util.TextBox_Enter;
            txtColonia.Enter += util.TextBox_Enter;
            txtLocalidad.Enter += util.TextBox_Enter;
            txtNoInterior.Enter += util.TextBox_Enter;
            cboUbicacion.Enter += util.Cbo_Box_Enter;
            supTerreno0.Enter += util.TextBox_Enter;
            txtSupCont0.Enter += util.TextBox_Enter;
            txtSupComun0.Enter += util.TextBox_Enter;
            txtSupContComun0.Enter += util.TextBox_Enter;
            txtValoresCatastrales0.Enter += util.TextBox_Enter;

            supTerreno.Enter += util.TextBox_Enter;
            txtSupCont.Enter += util.TextBox_Enter;
            txtSupComun.Enter += util.TextBox_Enter;
            txtSupContComun.Enter += util.TextBox_Enter;
            txtValoresCatastrales.Enter += util.TextBox_Enter;

            rboIdenticaClaCatastral.Enter += util.RadioButon_Enter;
            rbIdenticiudadano.Enter += util.RadioButon_Enter;
            rboIdenticaCalle.Enter += util.RadioButon_Enter;
            rboIdenticaColonia.Enter += util.RadioButon_Enter;
            rboIdenticaLocalidad.Enter += util.RadioButon_Enter;
            rboIdenticaNoInterior.Enter += util.RadioButon_Enter;
            rboIdenticaUbicacion.Enter += util.RadioButon_Enter;
            rboIdenticaSupTerreno.Enter += util.RadioButon_Enter;
            rboIdenticaSupConstruccion.Enter += util.RadioButon_Enter;
            rboIdenticaSupTerrenoComun.Enter += util.RadioButon_Enter;
            rboIdenticaSupConstComun.Enter += util.RadioButon_Enter;
            rboIdenticaValorCatastral.Enter += util.RadioButon_Enter;
            rboSimilarClaCatastral.Enter += util.RadioButon_Enter;
            rbSimilarciudadano.Enter += util.RadioButon_Enter;
            rboSimilarCalle.Enter += util.RadioButon_Enter;
            rboSimilarColonia.Enter += util.RadioButon_Enter;
            rboSimilarLocalidad.Enter += util.RadioButon_Enter;
            rboSimilarNoInterior.Enter += util.RadioButon_Enter;
        }

        private void frmCatastro03BusquedaCatastro_Load(object sender, EventArgs e)
        {
            inicio();
        }
        private void inicio()
        {
            inabilitarBotones();
            cajasColor();
            inicios();
            label27.Text = "USUARIO: " + Program.nombre_usuario.ToString();
            txtZona.Focus();
        }
        private void inicios()
        {
            // limpiamos la clave catastral
            limpiarClave();

            // limpiamos panel de filtro
            limpiarFiltro();
            rboFiltroQuitar();
            pnlDatosPredio.Enabled = false;

            // limpiamos el data gridview
            dataGridView1.DataSource = null; // Si estaba enlazado a un DataSource
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // limpiamos datos del predio y propiedades
            limpiarPredios();
            limpiarPropiedades();

            txtZona.Enabled = true;
            txtMzna.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;

            btnConsulta.Enabled = true;
            btnFiltro.Enabled = true;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;

            txtZona.Focus();

        }

        private void consulta()
        {
            if (txtZona.Text.Trim() == "")     { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2)       { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim() == "")     { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length < 3)       { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim() == "")     { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2)       { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2)   { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() == "")    { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4)      { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            Program.municipioV = Program.Vmunicipio;
            Program.zonaV = txtZona.Text.Trim();
            Program.manzanaV = txtMzna.Text.Trim();
            Program.loteV = txtLote.Text.Trim();
            Program.edificioV = txtEdificio.Text.Trim();
            Program.deptoV = txtDepto.Text.Trim();

            //------------------------------- OBTENEMOS LA INFROMACION DE LA CLAVE CATASTRAL.
            
            con.conectar_base_interno();
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT p.PmnProp, pr.Domicilio, pr.Zona, c.NomCalle, pr.NumExt, pr.CodPost,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.Ubicacion, pr.RegProp, co.NomCol,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.SupTerrTot, pr.SupTerrCom, pr.SupCons, pr.SupConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.Frente, pr.Fondo, pr.Desnivel, pr.AreaInscr,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       p.PtjeCondom, pr.Domicilio, p.NumIntP,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       p.STerrProp, p.STerrCom, p.SConsProp, p.SConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       svc.VALOR_TERRENO_P, svc.VALOR_TERRENO_C, svc.VALOR_CONSTRUCCION_P,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       svc.VALOR_CONSTRUCCION_C, svc.VALOR_CATASTRAL, p.cObsProp, pr.CodCalle";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM PROPIEDADES p, SONG_valoresCatastralesGenerales svc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       PREDIOS pr, CALLES c, COLONIAS co";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE p.Estado      = " + Program.Vestado;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Municipio   = " + Program.municipioV;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Zona        = " + Program.zonaV;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Manzana     = " + Program.manzanaV;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Lote        = " + Program.loteV;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Edificio    = '" + Program.edificioV + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Depto       = '" + Program.deptoV + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Estado      = svc.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Municipio   = svc.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Zona        = svc.Zona";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Manzana     = svc.Manzana";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Lote        = svc.Lote";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Edificio    = svc.Edificio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Depto       = svc.Depto";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND svc.Estado    = pr.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND svc.Municipio = pr.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND svc.Zona      = pr.Zona";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND svc.Manzana   = pr.Manzana";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND svc.Lote      = pr.Lote";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado     = c.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio  = c.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.CodCalle   = c.CodCalle";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Zona       = c.ZonaOrig";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado     = co.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio  = co.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Colonia    = co.Colonia";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            if (!con.leer_interno.HasRows)
            {
                MessageBox.Show("NO SE ENCONTRO NINGUNA CLAVE CATASTRAL", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.cerrar_interno(); 
                inicio(); 
                return; // Retornar si no hay resultados ( REGRESO )
            }

            while (con.leer_interno.Read())
            {
                lblPredioPropietario.Text = con.leer_interno[0].ToString().Trim();
                lblPredioDomicilio.Text = con.leer_interno[1].ToString().Trim();
                lblPredioZonaOrigen.Text = con.leer_interno[2].ToString().Trim();
                lblPredioCalle.Text = con.leer_interno[3].ToString().Trim();
                lblPredioNoExterior.Text = con.leer_interno[4].ToString().Trim();
                lblPredioCodigoPostal.Text = con.leer_interno[5].ToString().Trim();
                lblPredioUbicacion.Text = con.leer_interno[6].ToString().Trim();
                lblPredioRegPropiedad.Text = con.leer_interno[7].ToString().Trim();
                lblPredioColonia.Text = con.leer_interno[8].ToString().Trim();

                lblPredioSupTerreno.Text = con.leer_interno[9].ToString().Trim();
                lblPredioTerrenoComun.Text = con.leer_interno[10].ToString().Trim();
                lblPredioConstruccion.Text = con.leer_interno[11].ToString().Trim();
                lblPredioConstruccionComun.Text = con.leer_interno[12].ToString().Trim();
                lblPredioFrente.Text = con.leer_interno[13].ToString().Trim();
                lblPredioFondo.Text = con.leer_interno[14].ToString().Trim();
                lblPredioDesnivel.Text = con.leer_interno[15].ToString().Trim();
                lblPredioAreaInscripcion.Text = con.leer_interno[16].ToString().Trim();


                if (lblPredioSupTerreno.Text.Trim() == "") { lblPredioSupTerreno.Text = "0.00"; }
                else { lblPredioSupTerreno.Text = string.Format("{0:#,##0.00}", double.Parse(lblPredioSupTerreno.Text)); }

                if (lblPredioTerrenoComun.Text.Trim() == "") { lblPredioTerrenoComun.Text = "0.00"; }
                else { lblPredioTerrenoComun.Text = string.Format("{0:#,##0.00}", double.Parse(lblPredioTerrenoComun.Text)); }

                if (lblPredioConstruccion.Text.Trim() == "") { lblPredioConstruccion.Text = "0.00"; }
                else { lblPredioConstruccion.Text = string.Format("{0:#,##0.00}", double.Parse(lblPredioConstruccion.Text)); }

                if (lblPredioConstruccionComun.Text.Trim() == "") { lblPredioConstruccionComun.Text = "0.00"; }
                else { lblPredioConstruccionComun.Text = string.Format("{0:#,##0.00}", double.Parse(lblPredioConstruccionComun.Text)); }

                if (lblPredioFrente.Text.Trim() == "") { lblPredioFrente.Text = "0.00"; }
                else { lblPredioFrente.Text = string.Format("{0:#,##0.00}", double.Parse(lblPredioFrente.Text)); }

                if (lblPredioFondo.Text.Trim() == "") { lblPredioFondo.Text = "0.00"; }
                else { lblPredioFondo.Text = string.Format("{0:#,##0.00}", double.Parse(lblPredioFondo.Text)); }

                if (lblPredioDesnivel.Text.Trim() == "") { lblPredioDesnivel.Text = "0.00"; }
                else { lblPredioDesnivel.Text = string.Format("{0:#,##0.00}", double.Parse(lblPredioDesnivel.Text)); }

                if (lblPredioAreaInscripcion.Text.Trim() == "") { lblPredioAreaInscripcion.Text = "0.00"; }
                else { lblPredioAreaInscripcion.Text = string.Format("{0:#,##0.00}", double.Parse(lblPredioAreaInscripcion.Text)); }


                lblPropiedadesIndiviso.Text = con.leer_interno[17].ToString().Trim();
                lblPropiedadesDomFiscal.Text = con.leer_interno[18].ToString().Trim();
                lblNoInterior.Text = con.leer_interno[19].ToString().Trim();
                lblPropiedadesSupTerrenoProp.Text = con.leer_interno[20].ToString().Trim();
                lblPropiedadesTerrComunProp.Text = con.leer_interno[21].ToString().Trim();
                lblPropiedadesConstruccionProp.Text = con.leer_interno[22].ToString().Trim();
                lblPropiedadesConstruccionComunProp.Text = con.leer_interno[23].ToString().Trim();
                lblPropiedadesValorTerrenoP.Text = con.leer_interno[24].ToString().Trim();
                lblPropiedadesValorTerrenoC.Text = con.leer_interno[25].ToString().Trim();
                lblPropiedadesValorConstP.Text = con.leer_interno[26].ToString().Trim();
                lblPropiedadesValorConstC.Text = con.leer_interno[27].ToString().Trim();
                lblPropiedadesValorCatastral.Text = con.leer_interno[28].ToString().Trim();
                lblPropiedadesObservacion.Text = con.leer_interno[29].ToString().Trim();

                if (lblPropiedadesSupTerrenoProp.Text.Trim() == "") { lblPropiedadesSupTerrenoProp.Text = "0.00"; }
                else { lblPropiedadesSupTerrenoProp.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesSupTerrenoProp.Text)); }

                if (lblPropiedadesTerrComunProp.Text.Trim() == "") { lblPropiedadesTerrComunProp.Text = "0.00"; }
                else { lblPropiedadesTerrComunProp.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesTerrComunProp.Text)); }

                if (lblPropiedadesConstruccionProp.Text.Trim() == "") { lblPropiedadesConstruccionProp.Text = "0.00"; }
                else { lblPropiedadesConstruccionProp.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesConstruccionProp.Text)); }

                if (lblPropiedadesConstruccionComunProp.Text.Trim() == "") { lblPropiedadesConstruccionComunProp.Text = "0.00"; }
                else { lblPropiedadesConstruccionComunProp.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesConstruccionComunProp.Text)); }

                if (lblPropiedadesValorTerrenoP.Text.Trim() == "") { lblPropiedadesValorTerrenoP.Text = "0.00"; }
                else { lblPropiedadesValorTerrenoP.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesValorTerrenoP.Text)); }

                if (lblPropiedadesValorTerrenoC.Text.Trim() == "") { lblPropiedadesValorTerrenoC.Text = "0.00"; }
                else { lblPropiedadesValorTerrenoC.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesValorTerrenoC.Text)); }

                if (lblPropiedadesValorConstP.Text.Trim() == "") { lblPropiedadesValorConstP.Text = "0.00"; }
                else { lblPropiedadesValorConstP.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesValorConstP.Text)); }

                if (lblPropiedadesValorConstC.Text.Trim() == "") { lblPropiedadesValorConstC.Text = "0.00"; }
                else { lblPropiedadesValorConstC.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesValorConstC.Text)); }

                if (lblPropiedadesValorCatastral.Text.Trim() == "") { lblPropiedadesValorCatastral.Text = "0.00"; }
                else { lblPropiedadesValorCatastral.Text = string.Format("{0:#,##0.00}", double.Parse(lblPropiedadesValorCatastral.Text)); }
                //código que se agregó para el código de la calle solicitado por giovanna
                lblCodCalle.Text = con.leer_interno[30].ToString().Trim();
            }
            con.cerrar_interno();

            //------------------------------- OBTENEMOS LA ULTIMA LATITUD Y LONGITUD.

            con.conectar_base_interno();
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT TOP 1 ID, Latitud, Longitud";
            con.cadena_sql_interno = con.cadena_sql_interno + "     FROM SONG_GEOLOCALIZACION";
            con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE Estado      =  " + Program.Vestado;
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND Municipio   =  " + Program.municipioV;
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND Zona        =  " + Program.zonaV;
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND Manzana     =  " + Program.manzanaV;
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND Lote        =  " + Program.loteV;
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND Edificio    = '" + Program.edificioV + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND Depto       = '" + Program.deptoV + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY ID DESC     ";
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            if (!con.leer_interno.HasRows)
            {
                txtLatitud.Text = "";
                txtLongitud.Text = "";
            }
            while (con.leer_interno.Read())
            {
                txtLatitud.Text = con.leer_interno[1].ToString().Trim();
                txtLongitud.Text = con.leer_interno[2].ToString().Trim();
            }
            con.cerrar_interno();

            //------------------------------- OBTENEMOS EL TIPO DE PREDIO.

            int ubicacionEntero = Convert.ToInt32(lblPredioUbicacion.Text.Trim());
            switch (ubicacionEntero)
            {
                case 1:
                    cboUbicacion.Text = "URBANO";
                    break;
                case 2:
                    cboUbicacion.Text = "RURAL";
                    break;
                case 3:
                    cboUbicacion.Text = "EXTRATERRITORIAL";
                    break;
                default:
                    cboUbicacion.Text = "NO DEFINIDO";
                    break;
            }
            switch (ubicacionEntero) //así va 
            {
                case 1:
                    lblPredioUbicacion.Text = "1 tINTERMEDIO";
                    break;
                case 2:
                    lblPredioUbicacion.Text = "2\tESQUINERO\r\n";
                    break;
                case 3:
                    lblPredioUbicacion.Text = "3\tCABECERO\r\n";
                    break;
                case 4:
                    lblPredioUbicacion.Text = "4\tMANZANERO\r\n";
                    break;
                case 5:
                    cboUbicacion.Text = "5\tFRENTES NO CONTIGUOS\r\n";
                    break;
                case 6:
                    cboUbicacion.Text = "6\tINTERIOR\r\n";
                    break;
                case 7:
                    cboUbicacion.Text = "7\tSIN DESCRIPCION\r\n";
                    break;
                default:
                    cboUbicacion.Text = "NO DEFINIDO";
                    break;
            }
            ///////////////////////////rwvisar bloqueos
            ///
            int verificar = 0;
            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE
               
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + Program.municipioV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + Program.zonaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + Program.manzanaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + Program.loteV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(Program.edificioV);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(Program.deptoV) + ")";
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
                MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                try
                {
                    //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE

                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "            SELECT COMENTARIO";
                    con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + Program.municipioV;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + Program.zonaV;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + Program.manzanaV;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + Program.loteV;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(Program.edificioV);
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(Program.deptoV) ;


                    con.open_c_interno();
                    con.cadena_sql_cmd_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {

                        lblBloqueo.Text = con.leer_interno[0].ToString();
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
            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;

            btnConsulta.Enabled = false;
            btnFiltro.Enabled = false;

            btnCancelar.Enabled = true;
            btnMinimizar.Enabled = true;
            btnMaps.Enabled = true;

            picDerechaUno.Enabled = true;
            picDerechaDos.Enabled = true;
            picDerechaTres.Enabled = true;

            //aquí debe ser el if con los niveles //confirmar 
            if (Program.acceso_nivel_acceso == 9)
            {
                btnConstComun.Enabled = false;
                btnConstLote.Enabled = false;
            }
            else
            {
                btnConstComun.Enabled = true;
                btnConstLote.Enabled = true;
            }
            btnManifestacion.Enabled = true;
            cmdSalida.Enabled = false;
        }

        private void limpiarClave()
        {
            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";
        }

        private void limpiarPredios()
        {
            
            lblPredioPropietario.Text = "";
            lblPredioDomicilio.Text = "";
            lblCodCalle.Text = "";
            lblPredioZonaOrigen.Text = "";
            lblPredioCalle.Text = "";
            lblPredioNoExterior.Text = "";
            lblPredioCodigoPostal.Text = "";
            lblPredioUbicacion.Text = "";
            lblPredioRegPropiedad.Text = "";
            lblPredioColonia.Text = "";
            lblPredioSupTerreno.Text = "";
            lblPredioTerrenoComun.Text = "";
            lblPredioConstruccion.Text = "";
            lblPredioConstruccionComun.Text = "";
            lblPredioFrente.Text = "";
            lblPredioFondo.Text = "";
            lblPredioDesnivel.Text = "";
            lblPredioAreaInscripcion.Text = "";
        }

        private void limpiarPropiedades()
        {
            lblPropiedadesIndiviso.Text = "";
            lblPropiedadesDomFiscal.Text = "";
            lblNoInterior.Text = "";
            lblPropiedadesSupTerrenoProp.Text = "";
            lblPropiedadesTerrComunProp.Text = "";
            lblPropiedadesConstruccionProp.Text = "";
            lblPropiedadesConstruccionComunProp.Text = "";
            lblPropiedadesValorTerrenoP.Text = "";
            lblPropiedadesValorTerrenoC.Text = "";
            lblPropiedadesValorConstP.Text = "";
            lblPropiedadesValorConstC.Text = "";
            lblPropiedadesValorCatastral.Text = "";
            lblPropiedadesObservacion.Text = "";
            lblBloqueo.Text = "";
            txtLatitud.Text = "";
            txtLongitud.Text = "";
        }

        private void limpiarFiltro()
        {
            txtZonaDos.Text = "";
            txtMznaDos.Text = "";
            txtLoteDos.Text = "";
            txtEdificioDos.Text = "";
            txtDeptoDos.Text = "";
            txtpersona.Text = "";
            txtCalle.Text = "";
            txtColonia.Text = "";
            txtLocalidad.Text = "";
            txtNoInterior.Text = "";

            cboUbicacion.Items.Clear();
            //llenar el combobox de la ubicacion
            cboUbicacion.Items.Add("1 INTERMEDIO");
            cboUbicacion.Items.Add("2 ESQUINERO");
            cboUbicacion.Items.Add("3 CABECERO");
            cboUbicacion.Items.Add("4 MANZANERO");
            cboUbicacion.Items.Add("5 FRENTES NO CONTIGUOS");
            cboUbicacion.Items.Add("6 INTERIOR");
            cboUbicacion.Items.Add("7 SIN DESCRIPCION");

            supTerreno0.Text = "";
            txtSupCont0.Text = "";
            txtSupComun0.Text = "";
            txtSupContComun0.Text = "";
            txtValoresCatastrales0.Text = "";

            supTerreno.Text = "";
            txtSupCont.Text = "";
            txtSupComun.Text = "";
            txtSupContComun.Text = "";
            txtValoresCatastrales.Text = "";
        }

        private void rboFiltroQuitar()
        {
            rboIdenticaClaCatastral.Checked = false;
            rbIdenticiudadano.Checked = false;
            rboIdenticaCalle.Checked = false;
            rboIdenticaColonia.Checked = false;
            rboIdenticaLocalidad.Checked = false;

            rboIdenticaNoInterior.Checked = false;

            rboIdenticaUbicacion.Checked = false;

            rboIdenticaSupTerreno.Checked = false;
            rboIdenticaSupConstruccion.Checked = false;
            rboIdenticaSupTerrenoComun.Checked = false;
            rboIdenticaSupConstComun.Checked = false;
            rboIdenticaValorCatastral.Checked = false;

            rboSimilarClaCatastral.Checked = false;
            rbSimilarciudadano.Checked = false;
            rboSimilarCalle.Checked = false;
            rboSimilarColonia.Checked = false;
            rboSimilarLocalidad.Checked = false;
            rboSimilarNoInterior.Checked = false;
        }

        private void inabilitarBotones()
        {
            btnConsulta.Enabled = false;
            btnFiltro.Enabled = false;
            btnCancelar.Enabled = false;
            cmdSalida.Enabled = false;
            btnConsultaFilt.Enabled = false;
            btnCancelarFilt.Enabled = false;

            picDerechaUno.Enabled = false;
            picDerechaDos.Enabled = false;
            picDerechaTres.Enabled = false;

            btnConstComun.Enabled = false;
            btnConstLote.Enabled = false;
        }
        private void frmCatastro03BusquedaCatastro_Activated(object sender, EventArgs e)
        {
            txtZona.Focus();
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

        private void txtZonaDos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtMznaDos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtLoteDos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void supTerreno0_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, coma y teclas de control
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Verificar que solo haya un separador decimal
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as System.Windows.Forms.TextBox).Text.Contains(".") ||
                 (sender as System.Windows.Forms.TextBox).Text.Contains(",")))
            {
                e.Handled = true;
            }
        }
        private void supTerreno_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, coma y teclas de control
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Verificar que solo haya un separador decimal
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as System.Windows.Forms.TextBox).Text.Contains(".") ||
                 (sender as System.Windows.Forms.TextBox).Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void txtSupComun0_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, coma y teclas de control
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Verificar que solo haya un separador decimal
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as System.Windows.Forms.TextBox).Text.Contains(".") ||
                 (sender as System.Windows.Forms.TextBox).Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void txtSupComun_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, coma y teclas de control
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Verificar que solo haya un separador decimal
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as System.Windows.Forms.TextBox).Text.Contains(".") ||
                 (sender as System.Windows.Forms.TextBox).Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void txtSupContComun0_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, coma y teclas de control
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Verificar que solo haya un separador decimal
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as System.Windows.Forms.TextBox).Text.Contains(".") ||
                 (sender as System.Windows.Forms.TextBox).Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void txtSupContComun_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, coma y teclas de control
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Verificar que solo haya un separador decimal
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as System.Windows.Forms.TextBox).Text.Contains(".") ||
                 (sender as System.Windows.Forms.TextBox).Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void txtValoresCatastrales0_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, coma y teclas de control
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Verificar que solo haya un separador decimal
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as System.Windows.Forms.TextBox).Text.Contains(".") ||
                 (sender as System.Windows.Forms.TextBox).Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void txtValoresCatastrales_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal, coma y teclas de control
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            // Verificar que solo haya un separador decimal
            if ((e.KeyChar == '.' || e.KeyChar == ',') &&
                ((sender as System.Windows.Forms.TextBox).Text.Contains(".") ||
                 (sender as System.Windows.Forms.TextBox).Text.Contains(",")))
            {
                e.Handled = true;
            }
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

        private void txtEdificio_TextChanged(object sender, EventArgs e)
        {
            if (txtEdificio.Text.Length == 2) { txtDepto.Focus(); }
        }

        private void txtDepto_TextChanged(object sender, EventArgs e)
        {
            if (txtDepto.Text.Length == 4) { btnConsulta.Focus(); }
        }

        private void txtZonaDos_TextChanged(object sender, EventArgs e)
        {
            if (txtZonaDos.Text.Length == 2) { txtMznaDos.Focus(); }
        }

        private void txtMznaDos_TextChanged(object sender, EventArgs e)
        {
            if (txtMznaDos.Text.Length == 3) { txtLoteDos.Focus(); }
        }

        private void txtLoteDos_TextChanged(object sender, EventArgs e)
        {
            if (txtLoteDos.Text.Length == 2) { txtEdificioDos.Focus(); }
        }

        private void txtEdificioDos_TextChanged(object sender, EventArgs e)
        {
            if (txtEdificioDos.Text.Length == 2) { txtDeptoDos.Focus(); }
        }
        private void txtDeptoDos_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            inicio();
        }
        private void btnMaps_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLatitud.Text) || string.IsNullOrWhiteSpace(txtLongitud.Text))
            {
                MessageBox.Show("No hay latitud y longitud, antes de abrir Google Maps.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string latitud = txtLatitud.Text.Trim();
            string longitud = txtLongitud.Text.Trim();

            //return $"https://www.google.com/maps?q={latitud},{longitud}";
            Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
        }
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            consulta();
        }
        private void inicioFiltro()
        {
            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;

            btnConsulta.Enabled = false;

            btnFiltro.Enabled = false;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = false;
            btnMinimizar.Enabled = true;


            btnMaps.Enabled = false;

            picDerechaUno.Enabled = false;
            picDerechaDos.Enabled = false;
            picDerechaTres.Enabled = false;

            pnlDatosPredio.Enabled = true;

            limpiarFiltro();
            rboFiltroQuitar();

            txtZonaDos.Enabled = false;
            txtMznaDos.Enabled = false;
            txtLoteDos.Enabled = false;
            txtEdificioDos.Enabled = false;
            txtDeptoDos.Enabled = false;
            txtpersona.Enabled = false;
            txtCalle.Enabled = false;
            txtColonia.Enabled = false;
            txtLocalidad.Enabled = false;
            txtNoInterior.Enabled = false;

            cboUbicacion.Enabled = false;

            supTerreno0.Enabled = false;
            txtSupCont0.Enabled = false;
            txtSupComun0.Enabled = false;
            txtSupContComun0.Enabled = false;
            txtValoresCatastrales0.Enabled = false;

            supTerreno.Enabled = false;
            txtSupCont.Enabled = false;
            txtSupComun.Enabled = false;
            txtSupContComun.Enabled = false;
            txtValoresCatastrales.Enabled = false;

            btnConsultaFilt.Enabled = true;
            btnCancelarFilt.Enabled = true;

        } //quitar esto 
        private void btnFiltro_Click(object sender, EventArgs e)
        {
            inicioFiltro();

            rbIdenticiudadano.Checked = true;
            txtpersona.Enabled = true;
            txtpersona.Text = "";
            txtpersona.Focus();

        }

        private void btnBorrar1_Click(object sender, EventArgs e)
        {
            rboIdenticaClaCatastral.Checked = false;
            rboSimilarClaCatastral.Checked = false;

            txtZonaDos.Text = "";
            txtMznaDos.Text = "";
            txtLoteDos.Text = "";
            txtEdificioDos.Text = "";
            txtDeptoDos.Text = "";

            txtZonaDos.Enabled = false;
            txtMznaDos.Enabled = false;
            txtLoteDos.Enabled = false;
            txtEdificioDos.Enabled = false;
            txtDeptoDos.Enabled = false;

            btnBorrar1.Enabled = true;
        }

        private void btnBorrar2_Click(object sender, EventArgs e)
        {
            btnBorrar2.Enabled = true;
            rbIdenticiudadano.Checked = false;
            rbSimilarciudadano.Checked = false;
            txtpersona.Text = "";
            txtpersona.Enabled = false;
        }
        private void btnBorrar3_Click(object sender, EventArgs e)
        {
            btnBorrar3.Enabled = true;
            rboIdenticaCalle.Checked = false;
            rboSimilarCalle.Checked = false;
            txtCalle.Text = "";
            txtCalle.Enabled = false;
        }

        private void btnBorrar4_Click(object sender, EventArgs e)
        {
            btnBorrar4.Enabled = true;
            rboIdenticaColonia.Checked = false;
            rboSimilarColonia.Checked = false;
            txtColonia.Text = "";
            txtColonia.Enabled = false;
        }

        private void btnBorrar5_Click(object sender, EventArgs e)
        {
            btnBorrar5.Enabled = true;
            rboIdenticaLocalidad.Checked = false;
            rboSimilarLocalidad.Checked = false;
            txtLocalidad.Text = "";
            txtLocalidad.Enabled = false;
        }

        private void btnBorrar6_Click(object sender, EventArgs e)
        {
            btnBorrar6.Enabled = true;
            rboIdenticaNoInterior.Checked = false;
            rboSimilarNoInterior.Checked = false;
            txtNoInterior.Text = "";
            txtNoInterior.Enabled = false;
        }
        //borrar las ubicaciones 
        private void btnBorrar7_Click(object sender, EventArgs e)
        {
            btnBorrar7.Enabled = true;
            rboIdenticaUbicacion.Checked = false;
            cboUbicacion.SelectedIndex = -1;
            cboUbicacion.Enabled = false;
        }
        //superficie de terreno
        private void btnBorrar8_Click(object sender, EventArgs e)
        {
            btnBorrar8.Enabled = true;
            rboIdenticaSupTerreno.Checked = false;
            supTerreno0.Text = "";
            supTerreno.Text = "";
            supTerreno0.Enabled = false;
            supTerreno.Enabled = false;
        }
        //borrar superficie de construcción
        private void btnBorrar9_Click(object sender, EventArgs e)
        {
            btnBorrar9.Enabled = true;
            rboIdenticaSupConstruccion.Checked = false;
            txtSupCont0.Text = "";
            txtSupCont.Text = "";
            txtSupCont0.Enabled = false;
            txtSupCont.Enabled = false;
        }
        //borrar valores superficies terreno comun
        private void btnBorrar10_Click(object sender, EventArgs e)
        {
            btnBorrar10.Enabled = true;
            rboIdenticaSupTerrenoComun.Checked = false;
            txtSupComun0.Text = "";
            txtSupComun.Text = "";
            txtSupComun0.Enabled = false;
            txtSupComun.Enabled = false;
        }
        //valores superficies construccion comun
        private void btnBorrar11_Click(object sender, EventArgs e)
        {
            btnBorrar11.Enabled = true;
            rboIdenticaSupConstComun.Checked = false;
            txtSupContComun0.Text = "";
            txtSupContComun.Text = "";
            txtSupContComun0.Enabled = false;
            txtSupContComun.Enabled = false;
        }
        //valores cat
        private void btnBorrar12_Click(object sender, EventArgs e)
        {
            btnBorrar12.Enabled = true;
            rboIdenticaValorCatastral.Checked = false;
            txtValoresCatastrales0.Text = "";
            txtValoresCatastrales.Text = "";

            txtValoresCatastrales0.Enabled = false;
            txtValoresCatastrales.Enabled = false;
        }

        private void consultarFiltros()
        {
            //----------------------------------------------------------------------------------------------------------------------------------------------------//
            //--------------------------------------------  validamos que tengamos por lo menos un txt lleno  ----------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------------------------------------//

            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";

            if (txtZonaDos.Text.Trim() == "")
            {
                if (txtMznaDos.Text.Trim() == "")
                {
                    if (txtLoteDos.Text.Trim() == "")
                    {
                        if (txtEdificioDos.Text.Trim() == "")
                        {
                            if (txtDeptoDos.Text.Trim() == "")
                            {
                                if (txtpersona.Text.Trim() == "")
                                {
                                    if (txtCalle.Text.Trim() == "")
                                    {
                                        if (txtColonia.Text.Trim() == "")
                                        {
                                            if (txtLocalidad.Text.Trim() == "")
                                            {
                                                if (txtNoInterior.Text.Trim() == "")
                                                {
                                                    if (cboUbicacion.Text.Trim() == "")
                                                    {
                                                        if (supTerreno0.Text.Trim() == "")
                                                        {
                                                            if (supTerreno.Text.Trim() == "")
                                                            {
                                                                if (txtSupCont0.Text.Trim() == "")
                                                                {
                                                                    if (txtSupCont.Text.Trim() == "")
                                                                    {
                                                                        if (txtSupComun0.Text.Trim() == "")
                                                                        {
                                                                            if (txtSupComun.Text.Trim() == "")
                                                                            {
                                                                                if (txtSupContComun0.Text.Trim() == "")
                                                                                {
                                                                                    if (txtSupContComun.Text.Trim() == "")
                                                                                    {
                                                                                        if (txtValoresCatastrales0.Text.Trim() == "")
                                                                                        {
                                                                                            if (txtValoresCatastrales.Text.Trim() == "")
                                                                                            {
                                                                                                MessageBox.Show("NO SE TIENE INFORMACION DE BUSQUEDA", "ERROR", MessageBoxButtons.OK); cancelarFiltroInicio(); return;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (rboIdenticaClaCatastral.Checked == true)
            {
                if (txtZonaDos.Text.Trim() == "")
                {
                    if (txtMznaDos.Text.Trim() == "")
                    {
                        if (txtLoteDos.Text.Trim() == "")
                        {
                            if (txtEdificioDos.Text.Trim() == "")
                            {
                                if (txtDeptoDos.Text.Trim() == "")
                                {
                                    MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR UN DATO DE CLAVE CATASTRAL", "ERROR", MessageBoxButtons.OK); return;
                                }
                            }
                        }
                    }
                }
            }

            if (rboSimilarClaCatastral.Checked == true)
            {
                if (txtZonaDos.Text.Trim() == "")
                {
                    if (txtMznaDos.Text.Trim() == "")
                    {
                        if (txtLoteDos.Text.Trim() == "")
                        {
                            if (txtEdificioDos.Text.Trim() == "")
                            {
                                if (txtDeptoDos.Text.Trim() == "")
                                {
                                    MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR UN DATO DE CLAVE CATASTRAL", "ERROR", MessageBoxButtons.OK); return;
                                }
                            }
                        }
                    }
                }
            }

            if (rbIdenticiudadano.Checked == true) { if (txtpersona.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE PERSONA", "ERROR", MessageBoxButtons.OK); return; } }
            if (rbSimilarciudadano.Checked == true) { if (txtpersona.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE PERSONA", "ERROR", MessageBoxButtons.OK); return; } }

            if (rboIdenticaCalle.Checked == true) { if (txtCalle.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE CALLE", "ERROR", MessageBoxButtons.OK); return; } }
            if (rboSimilarCalle.Checked == true) { if (txtCalle.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE CALLE", "ERROR", MessageBoxButtons.OK); return; } }

            if (rboIdenticaColonia.Checked == true) { if (txtColonia.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE COLONIA", "ERROR", MessageBoxButtons.OK); return; } }
            if (rboSimilarColonia.Checked == true) { if (txtColonia.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE COLONIA", "ERROR", MessageBoxButtons.OK); return; } }

            if (rboIdenticaLocalidad.Checked == true) { if (txtLocalidad.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE LOCALIDAD", "ERROR", MessageBoxButtons.OK); return; } }
            if (rboSimilarLocalidad.Checked == true) { if (txtLocalidad.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE LOCALIDAD", "ERROR", MessageBoxButtons.OK); return; } }

            if (rboIdenticaNoInterior.Checked == true) { if (txtNoInterior.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE N° INTERIOR", "ERROR", MessageBoxButtons.OK); return; } }
            if (rboSimilarNoInterior.Checked == true) { if (txtNoInterior.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE N° INTERIOR", "ERROR", MessageBoxButtons.OK); return; } }

            if (rboIdenticaUbicacion.Checked == true) { if (cboUbicacion.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE UBICACION", "ERROR", MessageBoxButtons.OK); return; } }
            if (rboIdenticaUbicacion.Checked == true) { if (cboUbicacion.Text.Trim() == "") { MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR INFORMACION DE UBICACION", "ERROR", MessageBoxButtons.OK); return; } }

            if (rboIdenticaSupTerreno.Checked == true)
            {
                if (supTerreno0.Text.Trim() == "")
                {
                    if (supTerreno.Text.Trim() == "")
                    {
                        MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR SUPERFICIES DE TERRENO", "ERROR", MessageBoxButtons.OK); return;
                    }
                }
            }

            if (rboIdenticaSupConstruccion.Checked == true)
            {
                if (txtSupCont0.Text.Trim() == "")
                {
                    if (txtSupCont.Text.Trim() == "")
                    {
                        MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR SUPERFICIE DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); return;
                    }
                }
            }

            if (rboIdenticaSupTerrenoComun.Checked == true)
            {
                if (txtSupComun0.Text.Trim() == "")
                {
                    if (txtSupComun.Text.Trim() == "")
                    {
                        MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR SUPERFICIE DE TERRENO COMUN", "ERROR", MessageBoxButtons.OK); return;
                    }
                }
            }

            if (rboIdenticaSupConstComun.Checked == true)
            {
                if (txtSupContComun0.Text.Trim() == "")
                {
                    if (txtSupContComun.Text.Trim() == "")
                    {
                        MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR SUPERFICIE DE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); return;
                    }
                }
            }

            if (rboIdenticaValorCatastral.Checked == true)
            {
                if (txtValoresCatastrales0.Text.Trim() == "")
                {
                    if (txtValoresCatastrales.Text.Trim() == "")
                    {
                        MessageBox.Show("NO SE PUEBE BUSCAR SIN INGRESAR LOS VALORES CATASTRALES", "ERROR", MessageBoxButtons.OK); return;
                    }
                }
            }

            //----------------------------------------------------------------------------------------------------------------------------------------------------//
            //--------------------------------------------  consulta con filtros de consulta  --------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------------------------------------//

            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT p.Municipio,   p.Zona,       p.Manzana,    p.Lote,        p.Edificio,    p.Depto, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "       p.PmnProp,    pr.Domicilio, pr.Zona,       c.NomCalle,   pr.NumExt,     pr.CodPost,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.Ubicacion,  pr.RegProp,   co.NomCol,    pr.SupTerrTot, pr.SupTerrCom, pr.SupCons,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.SupConsCom, pr.Frente,    pr.Fondo,     pr.Desnivel,   pr.AreaInscr,   p.PtjeCondom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.Domicilio,   p.NumIntP,    p.STerrProp,  p.STerrCom,    p.SConsProp,   p.SConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       svc.VALOR_TERRENO_P,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       svc.VALOR_TERRENO_C,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       svc.VALOR_CONSTRUCCION_P,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       svc.VALOR_CONSTRUCCION_C,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       svc.VALOR_CATASTRAL,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       p.cObsProp";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM PREDIOS pr, PROPIEDADES p, SONG_valoresCatastralesGenerales svc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       CALLES c, COLONIAS co, MANZANAS m, LOCALIDADES l";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE pr.Estado = 15";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio = 041";

            if (rboIdenticaClaCatastral.Checked == true)
            {
                if (txtZonaDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Zona = " + txtZonaDos.Text.Trim(); }
                if (txtMznaDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Manzana = " + txtMznaDos.Text.Trim(); }
                if (txtLoteDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Lote = " + txtLoteDos.Text.Trim(); }
            }

            if (rboSimilarClaCatastral.Checked == true)
            {
                if (txtZonaDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Zona = " + txtZonaDos.Text.Trim(); }
                if (txtMznaDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Manzana = " + txtMznaDos.Text.Trim(); }
                if (txtLoteDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Lote = " + txtLoteDos.Text.Trim(); }
            }

            if (rboIdenticaUbicacion.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Ubicacion = " + Convert.ToInt32(cboUbicacion.Text.Trim().Substring(1, 1)); }

            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado = p.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio = p.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Zona = p.Zona";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Manzana = p.Manzana";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Lote = p.Lote";

            if (rboIdenticaClaCatastral.Checked == true)
            {
                if (txtZonaDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Edificio = '" + txtEdificioDos.Text.Trim() + "'"; }
                if (txtMznaDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Depto = '" + txtDeptoDos.Text.Trim() + "'"; }
            }
            if (rboSimilarClaCatastral.Checked == true)
            {
                if (txtEdificioDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Edificio = '" + txtEdificioDos.Text.Trim() + "'"; }
                if (txtDeptoDos.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Depto = '" + txtDeptoDos.Text.Trim() + "'"; }
            }

            if (rbIdenticiudadano.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.PmnProp = '" + txtpersona.Text.Trim() + "'"; }
            if (rbSimilarciudadano.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.PmnProp like '%" + txtpersona.Text.Trim() + "%'"; }

            if (rboIdenticaNoInterior.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.NumIntP = '" + txtNoInterior.Text.Trim() + "'"; }
            if (rboSimilarNoInterior.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.NumIntP like '%" + txtNoInterior.Text.Trim() + "%'"; }


            if (rboIdenticaSupTerreno.Checked == true)
            {
                if (supTerreno0.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.STerrProp >= " + Convert.ToInt32(supTerreno0.Text.Trim()); }
                if (supTerreno.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.STerrProp <= " + Convert.ToInt32(supTerreno.Text.Trim()); }
            }

            if (rboIdenticaSupConstruccion.Checked == true)
            {
                if (txtSupCont0.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.SConsProp >= " + Convert.ToInt32(txtSupCont0.Text.Trim()); }
                if (txtSupCont.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.SConsProp <= " + Convert.ToInt32(txtSupCont.Text.Trim()); }
            }

            if (rboIdenticaSupTerrenoComun.Checked == true)
            {
                if (txtSupComun0.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.STerrCom >= " + Convert.ToInt32(txtSupComun0.Text.Trim()); }
                if (txtSupComun.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.STerrCom <= " + Convert.ToInt32(txtSupComun.Text.Trim()); }
            }

            if (rboIdenticaSupConstComun.Checked == true)
            {
                if (txtSupContComun0.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.SConsCom >= " + Convert.ToInt32(txtSupContComun0.Text.Trim()); }
                if (txtSupContComun.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.SConsCom <= " + Convert.ToInt32(txtSupContComun.Text.Trim()); }
            }

            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Estado = svc.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Municipio = svc.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Zona = svc.Zona";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Manzana = svc.Manzana";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Lote = svc.Lote";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Edificio = svc.Edificio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Depto = svc.Depto";

            if (rboIdenticaValorCatastral.Checked == true)
            {
                if (txtValoresCatastrales0.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND svc.VALOR_CATASTRAL >= " + Convert.ToInt32(txtValoresCatastrales0.Text.Trim()); }
                if (txtValoresCatastrales.Text.Trim() != "") { con.cadena_sql_interno = con.cadena_sql_interno + "   AND svc.VALOR_CATASTRAL <= " + Convert.ToInt32(txtValoresCatastrales.Text.Trim()); }
            }

            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado = m.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio = m.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Zona = m.Zona";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Manzana = m.Manzana";

            con.cadena_sql_interno = con.cadena_sql_interno + "   AND m.Estado = co.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND m.Municipio = co.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND m.Colonia = co.Colonia";

            if (rboIdenticaColonia.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND co.NomCol = '" + txtColonia.Text.Trim() + "'"; }
            if (rboSimilarColonia.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND co.NomCol like '%" + txtColonia.Text.Trim() + "%'"; }

            con.cadena_sql_interno = con.cadena_sql_interno + "   AND m.Estado = l.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND m.Municipio = l.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND m.Localidad = l.Localidad";

            if (rboIdenticaLocalidad.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND l.NomLoc = '" + txtLocalidad.Text.Trim() + "'"; }
            if (rboSimilarLocalidad.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND l.NomLoc like '%" + txtLocalidad.Text.Trim() + "%'"; }

            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado = c.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio = c.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.CodCalle = c.CodCalle";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Zona = c.ZonaOrig";

            if (rboIdenticaCalle.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND c.NomCalle = '" + txtCalle.Text.Trim() + "'"; }
            if (rboSimilarCalle.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND c.NomCalle like '%" + txtCalle.Text.Trim() + "%'"; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////

            con.conectar_base_interno();
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            int contador = 0;

            while (con.leer_interno.Read())
            {
                contador = 1;
            }
            con.cerrar_interno();

            ///////////////////////////////////////////////////////////////////////////////////////////////////

            if (contador == 0)
            {
                MessageBox.Show("NO SE OBTUVIERON REGISTROS DE LA CONSULTA", "ERROR", MessageBoxButtons.OK); return;
            }
            else
            {
                con.conectar_base_interno();
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                SqlDataAdapter da = new SqlDataAdapter(con.cmd_interno);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.cerrar_interno();
                dataGridView1.Visible = true;

                double numRegistro = Convert.ToDouble(dataGridView1.Rows.Count - 1);
                lblNumRegistro.Text = string.Format("{0:#,0}", numRegistro);
            }
            //----------------------------------------------------------------------------------------------------------------------------------------------------//
            //--------------------------------------------  bloqueamos los botones  ------------------------------------------------------------------------------//
            //----------------------------------------------------------------------------------------------------------------------------------------------------//
        }
        private void btnConsultaFilt_Click(object sender, EventArgs e)
        {
            consultarFiltros(); //consultar filtros 
        }
        private void btnCancelarFilt_Click(object sender, EventArgs e)
        {
            cancelarFiltroInicio();
        }
        private void cancelarFiltroInicio()
        {
            cancelarFiltro();
            limpiarPredios();
            limpiarPropiedades();
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            lblNumRegistro.Text = "0";

            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";

            rbIdenticiudadano.Focus();
        }
        private void cancelarFiltro()
        {
            btnBorrar1.Enabled = true;
            btnBorrar2.Enabled = true;
            btnBorrar3.Enabled = true;
            btnBorrar4.Enabled = true;
            btnBorrar5.Enabled = true;
            btnBorrar6.Enabled = true;
            btnBorrar7.Enabled = true;
            btnBorrar8.Enabled = true;
            btnBorrar9.Enabled = true;
            btnBorrar10.Enabled = true;
            btnBorrar11.Enabled = true;
            btnBorrar12.Enabled = true;

            rboIdenticaClaCatastral.Checked = false;
            rboSimilarClaCatastral.Checked = false;
            rbIdenticiudadano.Checked = false;
            rbSimilarciudadano.Checked = false;
            rboIdenticaCalle.Checked = false;
            rboSimilarCalle.Checked = false;
            rboIdenticaColonia.Checked = false;
            rboSimilarColonia.Checked = false;
            rboIdenticaLocalidad.Checked = false;
            rboSimilarLocalidad.Checked = false;
            rboIdenticaNoInterior.Checked = false;
            rboSimilarNoInterior.Checked = false;
            rboIdenticaUbicacion.Checked = false;
            rboIdenticaSupTerreno.Checked = false;
            rboIdenticaSupConstruccion.Checked = false;
            rboIdenticaSupTerrenoComun.Checked = false;
            rboIdenticaSupConstComun.Checked = false;
            rboIdenticaValorCatastral.Checked = false;

            txtZonaDos.Text = "";
            txtMznaDos.Text = "";
            txtLoteDos.Text = "";
            txtEdificioDos.Text = "";
            txtDeptoDos.Text = "";
            txtpersona.Text = "";
            txtCalle.Text = "";
            txtColonia.Text = "";
            txtLocalidad.Text = "";
            txtNoInterior.Text = "";
            supTerreno0.Text = "";
            supTerreno.Text = "";
            txtSupCont0.Text = "";
            txtSupCont.Text = "";
            txtSupComun0.Text = "";
            txtSupComun.Text = "";
            txtSupContComun0.Text = "";
            txtSupContComun.Text = "";
            txtValoresCatastrales0.Text = "";
            txtValoresCatastrales.Text = "";

            cboUbicacion.SelectedIndex = -1;
        }
        private void rboIdenticaClaCatastral_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaClaCatastral.Checked == true)
            {
                txtZonaDos.Enabled = true;
                txtMznaDos.Enabled = true;
                txtLoteDos.Enabled = true;
                txtEdificioDos.Enabled = true;
                txtDeptoDos.Enabled = true;

                txtZonaDos.Text = "";
                txtMznaDos.Text = "";
                txtLoteDos.Text = "";
                txtEdificioDos.Text = "";
                txtDeptoDos.Text = "";

                txtZonaDos.Focus();
            }
        }
        private void rboSimilarClaCatastral_CheckedChanged(object sender, EventArgs e)
        {
            if (rboSimilarClaCatastral.Checked == true)
            {
                txtZonaDos.Enabled = true;
                txtMznaDos.Enabled = true;
                txtLoteDos.Enabled = true;
                txtEdificioDos.Enabled = true;
                txtDeptoDos.Enabled = true;

                txtZonaDos.Text = "";
                txtMznaDos.Text = "";
                txtLoteDos.Text = "";
                txtEdificioDos.Text = "";
                txtDeptoDos.Text = "";

                txtZonaDos.Focus();
            }
        }
        private void rbIdenticiudadano_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdenticiudadano.Checked == true)
            {
                txtpersona.Enabled = true;
                txtpersona.Text = "";
                txtpersona.Focus();
            }
        }
        private void rbSimilarciudadano_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSimilarciudadano.Checked == true)
            {
                txtpersona.Enabled = true;
                txtpersona.Text = "";
                txtpersona.Focus();
            }
        }
        private void rboIdenticaCalle_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaCalle.Checked == true)
            {
                txtCalle.Enabled = true;
                txtCalle.Text = "";
                txtCalle.Focus();
            }
        }
        private void rboSimilarCalle_CheckedChanged(object sender, EventArgs e)
        {
            if (rboSimilarCalle.Checked == true)
            {
                txtCalle.Enabled = true;
                txtCalle.Text = "";
                txtCalle.Focus();
            }
        }

        private void rboIdenticaColonia_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaColonia.Checked == true)
            {
                txtColonia.Enabled = true;
                txtColonia.Text = "";
                txtColonia.Focus();
            }
        }
        private void rboSimilarColonia_CheckedChanged(object sender, EventArgs e)
        {
            if (rboSimilarColonia.Checked == true)
            {
                txtColonia.Enabled = true;
                txtColonia.Text = "";
                txtColonia.Focus();
            }
        }
        private void rboIdenticaLocalidad_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaLocalidad.Checked == true)
            {
                txtLocalidad.Enabled = true;
                txtLocalidad.Text = "";
                txtLocalidad.Focus();
            }
        }

        private void rboSimilarLocalidad_CheckedChanged(object sender, EventArgs e)
        {
            if (rboSimilarLocalidad.Checked == true)
            {
                txtLocalidad.Enabled = true;
                txtLocalidad.Text = "";
                txtLocalidad.Focus();
            }
        }

        private void rboIdenticaNoInterior_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaNoInterior.Checked == true)
            {
                txtNoInterior.Enabled = true;
                txtNoInterior.Text = "";
                txtNoInterior.Focus();
            }
        }
        private void rboSimilarNoInterior_CheckedChanged(object sender, EventArgs e)
        {
            if (rboSimilarNoInterior.Checked == true)
            {
                txtNoInterior.Enabled = true;
                txtNoInterior.Text = "";
                txtNoInterior.Focus();
            }
        }
        private void rboIdenticaUbicacion_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaUbicacion.Checked == true)
            {
                cboUbicacion.Enabled = true;
                cboUbicacion.SelectedIndex = -1;
                cboUbicacion.Focus();
            }
        }
        private void rboIdenticaSupTerreno_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaSupTerreno.Checked == true)
            {
                supTerreno0.Enabled = true;
                supTerreno0.Text = "";

                supTerreno.Enabled = true;
                supTerreno.Text = "";
                supTerreno0.Focus();
            }
        }
        private void rboIdenticaSupConstruccion_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaSupConstruccion.Checked == true)
            {
                txtSupCont0.Enabled = true;
                txtSupCont0.Text = "";

                txtSupCont.Enabled = true;
                txtSupCont.Text = "";
                txtSupCont0.Focus();
            }
        }

        private void rboIdenticaSupTerrenoComun_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaSupTerrenoComun.Checked == true)
            {
                txtSupComun0.Enabled = true;
                txtSupComun0.Text = "";

                txtSupComun.Enabled = true;
                txtSupComun.Text = "";
                txtSupComun0.Focus();
            }
        }

        private void rboIdenticaSupConstComun_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaSupConstComun.Checked == true)
            {
                txtSupContComun0.Enabled = true;
                txtSupContComun0.Text = "";

                txtSupContComun.Enabled = true;
                txtSupContComun.Text = "";
                txtSupContComun0.Focus();
            }
        }

        private void rboIdenticaValorCatastral_CheckedChanged(object sender, EventArgs e)
        {
            if (rboIdenticaValorCatastral.Checked == true)
            {
                txtValoresCatastrales0.Enabled = true;
                txtValoresCatastrales0.Text = "";

                txtValoresCatastrales.Enabled = true;
                txtValoresCatastrales.Text = "";
                txtValoresCatastrales0.Focus();
            }
        }
        private void txtZonaDos_Leave(object sender, EventArgs e)
        {

        }
        private void supTerreno0_Leave(object sender, EventArgs e)
        {
            if (supTerreno0.Text.Trim() == "") { supTerreno0.Text = "0.00"; }
            else { supTerreno0.Text = string.Format("{0:#,##0.00}", double.Parse(supTerreno0.Text)); }
        }

        private void supTerreno_Leave(object sender, EventArgs e)
        {
            if   (supTerreno.Text.Trim() == "") { supTerreno.Text = "0.00"; }
            else { supTerreno.Text = string.Format("{0:#,##0.00}", double.Parse(supTerreno.Text)); }
        }

        private void txtSupCont0_Leave(object sender, EventArgs e)
        {
            if   (txtSupCont0.Text.Trim() == "") { txtSupCont0.Text = "0.00"; }
            else { txtSupCont0.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupCont0.Text)); }
        }

        private void txtSupCont_Leave(object sender, EventArgs e)
        {
            if (txtSupCont.Text.Trim() == "") { txtSupCont.Text = "0.00"; }
            else { txtSupCont.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupCont.Text)); }
        }

        private void txtSupComun0_Leave(object sender, EventArgs e)
        {
            if (txtSupComun0.Text.Trim() == "") { txtSupComun0.Text = "0.00"; }
            else { txtSupComun0.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupComun0.Text)); }
        }

        private void txtSupComun_Leave(object sender, EventArgs e)
        {
            if (txtSupComun.Text.Trim() == "") { txtSupComun.Text = "0.00"; }
            else { txtSupComun.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupComun.Text)); }
        }

        private void txtSupContComun0_Leave(object sender, EventArgs e)
        {
            if (txtSupContComun0.Text.Trim() == "") { txtSupContComun0.Text = "0.00"; }
            else { txtSupContComun0.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupContComun0.Text)); }
        }

        private void txtSupContComun_Leave(object sender, EventArgs e)
        {
            if (txtSupContComun.Text.Trim() == "") { txtSupContComun.Text = "0.00"; }
            else { txtSupContComun.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupContComun.Text)); }
        }

        private void txtValoresCatastrales0_Leave(object sender, EventArgs e)
        {
            if (txtValoresCatastrales0.Text.Trim() == "") { txtValoresCatastrales0.Text = "0.00"; }
            else { txtValoresCatastrales0.Text = string.Format("{0:#,##0.00}", double.Parse(txtValoresCatastrales0.Text)); }
        }
        private void txtValoresCatastrales_Leave(object sender, EventArgs e)
        {
            if (txtValoresCatastrales.Text.Trim() == "") { txtValoresCatastrales.Text = "0.00"; }
            else { txtValoresCatastrales.Text = string.Format("{0:#,##0.00}", double.Parse(txtValoresCatastrales.Text)); }
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string municipios = "0";
            string zonas = "0";
            string manzanas = "0";
            string lotes = "0";
            string edificios = "0";
            string deptos = "0";

            municipios = "041";
            zonas = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();
            manzanas = dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim();
            lotes = dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim();
            edificios = dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim();
            deptos = dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim();

            if (zonas.Length == 1) { zonas = "0" + zonas; }
            if (manzanas.Length == 1) { manzanas = "00" + manzanas; }
            else if (manzanas.Length == 2) { manzanas = "0" + manzanas; }

            if (lotes.Length == 1) { lotes = "0" + lotes; }
            if (edificios.Length == 1) { edificios = "0" + edificios; }

            if (deptos.Length == 1) { deptos = "000" + deptos; }
            else if (deptos.Length == 2) { deptos = "00" + deptos; }
            else if (deptos.Length == 3) { deptos = "0" + deptos; }

            txtZona.Text = zonas;
            txtMzna.Text = manzanas;
            txtLote.Text = lotes;
            txtEdificio.Text = edificios;
            txtDepto.Text = deptos;

            consulta();

        }

        private void txtpersona_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtCalle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtColonia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtLocalidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtNoInterior_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void supTerreno_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtSupCont_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtSupComun_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtSupContComun_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtValoresCatastrales_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void cboUbicacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtZonaDos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtMznaDos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtLoteDos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtEdificioDos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void txtDeptoDos_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consultarFiltros();
            }
        }

        private void picDerechaUno_Click(object sender, EventArgs e)
        {
            Program.municipioV = "041";
            Program.zonaV = txtZona.Text.Trim();
            Program.manzanaV = txtMzna.Text.Trim();
            Program.loteV = txtLote.Text.Trim();
            Program.edificioV = txtEdificio.Text.Trim();
            Program.deptoV = txtDepto.Text.Trim();
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void picDerechaDos_Click(object sender, EventArgs e)
        {
            Program.municipioV = "041";
            Program.zonaV = txtZona.Text.Trim();
            Program.manzanaV = txtMzna.Text.Trim();
            Program.loteV = txtLote.Text.Trim();
            Program.edificioV = txtEdificio.Text.Trim();
            Program.deptoV = txtDepto.Text.Trim();
            this.Close();
        }

        private void picDerechaTres_Click(object sender, EventArgs e)
        {
            Program.municipioV = "041";
            Program.zonaV = txtZona.Text.Trim();
            Program.manzanaV = txtMzna.Text.Trim();
            Program.loteV = txtLote.Text.Trim();
            Program.edificioV = txtEdificio.Text.Trim();
            Program.deptoV = txtDepto.Text.Trim();
            this.Close();
        }
        private void cmdSalida_Click(object sender, EventArgs e)
        {
            Program.municipioV = "";
            Program.zonaV = "";
            Program.manzanaV = "";
            Program.loteV = "";
            Program.edificioV = "";
            Program.deptoV = "";
            this.Close();
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }

        private void PanelBarraTitulo_MouseHover(object sender, EventArgs e)
        {

        }
        //mover formulario de la parte superior 
        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnConstLote_Click(object sender, EventArgs e)
        {
            double constC = 0;
            if (lblPredioConstruccion.Text.Trim() == "") { lblPredioConstruccion.Text = "0"; }
            if (lblPredioConstruccionComun.Text.Trim() == "") { lblPredioConstruccionComun.Text = "0"; }

            if (lblPredioConstruccionComun.Text.Trim() != "0")
            {
                constC = Convert.ToDouble(lblPredioConstruccionComun.Text.Trim());
                if (constC > 0)
                {
                    MessageBox.Show("NO SE PUEDE INGRESAR CONSTRUCCION PROPIA. PORQUE SE TIENE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK);
                    return;
                }
            }

            if (lblPredioConstruccionComun.Text.Trim() == "") { lblPredioConstruccionComun.Text = "0"; }
            if (lblPredioConstruccionComun.Text.Trim() == "0" || lblPredioConstruccionComun.Text.Trim() == "0.00")
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
               // txtSupCont.Text = Program.constuccion.ToString();
                frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;

                //frmCatastro01UbicacionAlta.ActiveForm.Opacity = 0.50;
                //frmCatastro02UnidadesConstruccion fs = new frmCatastro02UnidadesConstruccion();
                //fs.ShowDialog();
                ////txtSupCont.Text = Program.construccion();
                ////fs.Show();
                //frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;
            }
        }

        private void btnConstComun_Click(object sender, EventArgs e)
        {
            double constP = 0;

            if (lblPredioConstruccion.Text.Trim() == "") { lblPredioConstruccion.Text = "0"; }
            if (lblPredioConstruccionComun.Text.Trim() == "") { lblPredioConstruccionComun.Text = "0"; }

            constP = Convert.ToDouble(lblPredioConstruccion.Text.Trim());
            if (constP > 0)
            {
                MessageBox.Show("NO SE PUEDE INGRESAR CONSTRUCCION COMUN. PORQUE SE TIENE CONSTRUCCION PRIVADA", "ERROR", MessageBoxButtons.OK);
                return;
            }


            if (lblPredioConstruccion.Text.Trim() == "0")
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
        //botón para abrir la manifestación catastral
        private void btnManifestacion_Click(object sender, EventArgs e)
        {

            int ZONA = Convert.ToInt32(txtZona.Text.ToString().Trim());
            int MANZANA = Convert.ToInt32(txtMzna.Text.ToString().Trim());
            int LOTE = Convert.ToInt32(txtLote.Text.ToString().Trim());
            string EDIFICIO = txtEdificio.Text.ToString().Trim();
            string DEPTO = txtDepto.Text.ToString().Trim();

            /*
            //spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + "T00:00:00");
            //spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + "T23:59:59");
            //ABRIMOS EL REPORTE 
            formaReporte.mostrarManifestacion mostrarReporteV = new formaReporte.mostrarManifestacion();
            ////////// LOS PARAMETROS QUE VOY A MANDAR DEL FORMULARIO DE AQUÍ 
            mostrarReporteV.ZONA = ZONA; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
            mostrarReporteV.MANZANA = MANZANA; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
            mostrarReporteV.LOTE = LOTE ; //DATETIME PARA LA FECHA INICIAL //DATETIME PARA LA FECHA FINAL 
            mostrarReporteV.EDIFICIO= EDIFICIO ; //Ubicación cortada del combobox para el procedimiento almacenado
            mostrarReporteV.DEPTO= DEPTO ; //Ubicación cortada del combobox para el procedimiento almacenado
            
            MessageBox.Show("PROCEDE A IMPRIMIR LA MANIFESTACIÓN", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mostrarReporteV.ShowDialog();
            */
            
        }
    }
}
