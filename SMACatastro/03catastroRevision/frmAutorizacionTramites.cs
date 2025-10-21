using AccesoBase;
using GMap.NET.MapProviders;
using Microsoft.Office.Interop.Excel;
using Microsoft.Practices.CompositeUI.Utility;
using Mysqlx.Cursor;
using Org.BouncyCastle.Math.Field;
using SMACatastro.catastroCartografia;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Policy;
using System.Web.Configuration;
using System.Windows.Forms;
using Telerik.WinControls;
using USLibV4.Utilerias;
using Utilerias;
using static log4net.Appender.RollingFileAppender;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace SMACatastro.catastroRevision
{
    public partial class frmAutorizacionTramites : Form
    {
        //te falta el tooltip , revisa un poco el diseño, etc; valida bien que hace con franky 
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2(); //Comenzamos con declarar las variables que se deben de utilizar
        int folioTemp, OperacionTramite, validacionProcedimiento, folio, serie, numRows = 0;
        public int ValidacionD = 0;
        string serieTemp, usuario, fecha_iniL, fecha_finL, observaReviso = "";
        public int SistemasFaltante, ventanillaFaltante = 0;
        Util util = new Util();
        string CadenaComplemento = "";
        //METODO PARA ARRASTRAR EL FORMULARIO---
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        public frmAutorizacionTramites()
        {
            InitializeComponent();
            btnNuevo.Focus();
        }
        private void frmAutorizacionTramites_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "USUARIO: " + Program.nombre_usuario;
            limpiarTodo();
            limpiarArriba();
            limpiarPanelAbajo();
            btnNuevo.Focus();
            //llenadoCombos();
        }

        private void frmAutorizacionTramites_Activated(object sender, EventArgs e)
        {
            btnNuevo.Focus();
        }
        /////////////////////////////////////////////////////////////////////////////
        ///MÉTOODOS DE LAS PANTALLAS, LIMPIAR
        /////////////////////////////////////////////////////////////////////////////
        void limpiarTodo() //limpiar todo
        {
            pnlFiltro.Enabled = false;
            pnlTAPADERA.Visible = false;
            SistemasFaltante = 0;
            ventanillaFaltante = 0;
            rbRangosFolios.Checked = false;
            rdSerFol.Checked = false;
            rbSerieFolio.Checked = false;
            btnCancela.Enabled = false;
            btnBUscar.Enabled = false;
            rbSerieFolio.Checked = false;
            rbFechaIni.Checked = false;
            rbIdenticiudadano.Checked = false;
            rbElaboroVenta.Checked = false;
            btnMasAutoriza.Enabled = false;
            btnMasAutoriza.Enabled = false;
            btnMasCancel.Enabled = false;
            pnlDatosAlta.Visible = false;
            pnlTAPAR.Visible = true;
            lblLatitud.Text = "";
            lblLonguitud.Text = "";
            lblLonguitud.Enabled = false;
            lblLatitud.Enabled = false;
            btnMaps.Enabled = false;
            CadenaComplemento = "";

            rbTipoTramite.Checked = false;
            rbClave.Checked = false;
            txtZona.Text = string.Empty;
            txtManzana.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtEdificio.Text = string.Empty;
            txtDepto.Text = string.Empty;

            txtFolio.Text = string.Empty;
            TXTrango1Fol.Text = string.Empty;
            TXTrango2Fol.Text = string.Empty;
            //cboSerie.SelectedIndex = -1;
            limpiarArriba();
            btnNuevo.Enabled = true;
            btnSalida.Enabled = true;
            //DATAGRID LIMPIAR 
            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            //limpiar el conteo 
            lblConteo.Text = "0";
            //ventanilla limpiar lo de abajo
            cbbUsuarioVenta.SelectedIndex = -1;
            lblDescripcionVentanilla.Text = "";
            lblFechaHoraVentanilla.Text = "";
            lblObservacionesVentanilla.Text = "";
            lblUsuarioVentanillla.Text = "";

            //cartografía limpiar lo de abajo 
            lblDescripcionCartografia.Text = "";
            lblFechaHoraCartografia.Text = "";
            lblObservacionesCartografia.Text = "";
            lblUsuarioCartografia.Text = "";
            //btnConsultaGeneral.Enabled = false;
            //
            // btnPendienteSistemas.Enabled = false;
            cmdAutorizarProcesoIndividual.Enabled = false;
            // btnRevisadosVentanilla.Enabled = false;
            btnCancelarProceso.Enabled = false;
            btnLimpiarAbajo.Enabled = false;
            //
            txtObservacionTramite.Enabled = false;
            txtObservacionTramite.Text = "";
        }
        void limpiarArriba() //método para solo limpiar lo de arriba , cajas de texto ; checkbox; etc.
        {

            cboSerie.Enabled = false;

            txtFolio.Text = "";
            txtFolio.Enabled = false;
            txtFolio.BackColor = Color.White;
            cboSerie.SelectedIndex = -1;
            cboDia1.SelectedIndex = -1;
            cboDia1.Enabled = false;
            cboMes1.SelectedIndex = -1;
            cboMes1.Enabled = false;
            cboAño1.SelectedIndex = -1;
            cboAño1.Enabled = false;
            cboDia2.SelectedIndex = -1;
            cboDia2.Enabled = false;
            cboMes2.SelectedIndex = -1;
            cboMes2.Enabled = false;
            cboAño2.SelectedIndex = -1;
            cboAño2.Enabled = false;
        }
        void limpiarPanelAbajo()
        {
            lblZonaOrigen.Text = "";
            lblCalle.Text = "";
            lblSupTerr.Text = "";
            lblSupTerrCom.Text = "";
            lblSupCons.Text = "";
            lblSupConsCom.Text = "";
            lblSupConsCom.Text = "";
            lblSupConsCom.Text = "";
            lblSupConsCom.Text = "";
            lblFrente.Text = "";
            lblFondo.Text = "";
            lblRegimen.Text = "";
            pnlDatosAlta.Visible = false;
        }
        void limpiardatagrid()
        {
            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            lblConteo.Text = "0";
        }
        //void habilitarArriba() //al dar botón nuevo , habilita los checkbox
        //{
        //    ckbFechas.Enabled = true;
        //    ckbFolio.Enabled = true;
        //    ckbSerie.Enabled = true;
        //}
        void cajas_amarillas(int ca) //Método para colocar las cajas amarillas 
        {
            switch (ca)
            {
                case 1: txtFolio.BackColor = System.Drawing.Color.Yellow; break; //Solo hay un caso de cajas amarillas
                case 2: cboSerie.BackColor = System.Drawing.Color.Yellow; break;
                case 3: cboDia1.BackColor = System.Drawing.Color.Yellow; break;
                case 4: cboMes1.BackColor = System.Drawing.Color.Yellow; break;
                case 5: cboAño1.BackColor = System.Drawing.Color.Yellow; break;
                case 6: cboDia2.BackColor = System.Drawing.Color.Yellow; break;
                case 7: cboMes2.BackColor = System.Drawing.Color.Yellow; break;
                case 8: cboAño2.BackColor = System.Drawing.Color.Yellow; break;
                case 9: txtObservacionTramite.BackColor = System.Drawing.Color.Yellow; break;
            }
        }
        void cajas_blancas(int cb) //Método para colocar las cajas blancas
        {
            switch (cb)
            {
                case 1: txtFolio.BackColor = System.Drawing.Color.White; break; //Solo hay un caso de cajas blancas
                case 2: cboSerie.BackColor = System.Drawing.Color.White; break;
                case 3: cboDia1.BackColor = System.Drawing.Color.White; break;
                case 4: cboMes1.BackColor = System.Drawing.Color.White; break;
                case 5: cboAño1.BackColor = System.Drawing.Color.White; break;
                case 6: cboDia2.BackColor = System.Drawing.Color.White; break;
                case 7: cboMes2.BackColor = System.Drawing.Color.White; break;
                case 8: cboAño2.BackColor = System.Drawing.Color.White; break;
                case 9: txtObservacionTramite.BackColor = System.Drawing.Color.White; break;
            }
        }
        private void validaCajas() //validar que no esté vacio ninguna caja de los combobox
        {
            if (cboDia1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DIA DE INICIO", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return; }
            if (cboMes1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL MES DE INICIO", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return; }
            if (cboAño1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL AÑO DE INICIO", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return; }
            if (cboDia2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DIA FINAL", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return; }
            if (cboMes2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL MES FINAL", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return; }
            if (cboAño2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL AÑO FINAL", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return; }
        }
        private void validaFecha() //método para validar si pasa la fecha, o es bisiesto 
        {
            int validado = 0;
            string dateString = cboDia1.Text + "/" + cboMes1.Text + "/" + cboAño1.Text + " 00:00:00";
            CultureInfo enUS = new CultureInfo("en-US");
            DateTime dateValue;
            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy hh:mm:ss", enUS, DateTimeStyles.None, out dateValue))
            {
                validado = 1;
            }
            else
            {
                MessageBox.Show("LA FECHA DE INICIO, NO TIENE EL FORMATO ADECUADO", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return;
            }
            string dateStrings = cboDia2.Text + "/" + cboMes2.Text + "/" + cboAño2.Text + " 00:00:00";
            CultureInfo enUSs = new CultureInfo("en-US");
            DateTime dateValues;
            if (DateTime.TryParseExact(dateStrings, "dd/MM/yyyy hh:mm:ss", enUSs, DateTimeStyles.None, out dateValues))
            {
                validado = 1;
            }
            else
            {
                MessageBox.Show("LA FECHA FINAL, NO TIENE EL FORMATO ADECUADO", "ERROR", MessageBoxButtons.OK); return;
            }
            DateTime spFECHA_INIS = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text + "-" + cboDia1.Text + "T00:00:00");
            DateTime spFECHA_FINS = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text + "-" + cboDia2.Text + "T23:59:59");
            if (spFECHA_INIS > spFECHA_FINS)
            {
                MessageBox.Show("LA FECHA INICIAL NO PUEDE SER MAYOR A LA FECHA FINAL", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return;
            }
            if (spFECHA_FINS < spFECHA_INIS)
            {
                MessageBox.Show("LA FECHA FINAL NO PUEDE SER MENOR A LA FECHA INICIAL", "ERROR", MessageBoxButtons.OK); ValidacionD = 1; return;
            }
        }
        ///////////////////////////////////////////////////////////////
        ///llenado combos dias y series
        ///////////////////////////////////////////////////////////////
        void llenadoCombos() //llenar los días 
        {
            cboDia1.Items.Clear();
            cboDia1.Items.Add("01");
            cboDia1.Items.Add("02");
            cboDia1.Items.Add("03");
            cboDia1.Items.Add("04");
            cboDia1.Items.Add("05");
            cboDia1.Items.Add("06");
            cboDia1.Items.Add("07");
            cboDia1.Items.Add("08");
            cboDia1.Items.Add("09");
            cboDia1.Items.Add("10");
            cboDia1.Items.Add("11");
            cboDia1.Items.Add("12");
            cboDia1.Items.Add("13");
            cboDia1.Items.Add("14");
            cboDia1.Items.Add("15");
            cboDia1.Items.Add("16");
            cboDia1.Items.Add("17");
            cboDia1.Items.Add("18");
            cboDia1.Items.Add("19");
            cboDia1.Items.Add("20");
            cboDia1.Items.Add("21");
            cboDia1.Items.Add("22");
            cboDia1.Items.Add("23");
            cboDia1.Items.Add("24");
            cboDia1.Items.Add("25");
            cboDia1.Items.Add("26");
            cboDia1.Items.Add("27");
            cboDia1.Items.Add("28");
            cboDia1.Items.Add("29");
            cboDia1.Items.Add("30");
            cboDia1.Items.Add("31");
            cboDia1.SelectedIndex = -1;

            cboMes1.Items.Clear();
            cboMes1.Items.Add("01");
            cboMes1.Items.Add("02");
            cboMes1.Items.Add("03");
            cboMes1.Items.Add("04");
            cboMes1.Items.Add("05");
            cboMes1.Items.Add("06");
            cboMes1.Items.Add("07");
            cboMes1.Items.Add("08");
            cboMes1.Items.Add("09");
            cboMes1.Items.Add("10");
            cboMes1.Items.Add("11");
            cboMes1.Items.Add("12");
            cboMes1.SelectedIndex = -1;

            cboAño1.Items.Clear();
            cboAño1.Items.Add("2023");
            cboAño1.Items.Add("2024");
            cboAño1.Items.Add("2025");
            cboAño1.Items.Add("2026");
            cboAño1.Items.Add("2027");
            cboAño1.Items.Add("2028");
            cboAño1.Items.Add("2029");
            cboAño1.Items.Add("2030");
            cboAño1.Items.Add("2031");
            cboAño1.Items.Add("2032");
            cboAño1.Items.Add("2033");
            cboAño1.Items.Add("2034");
            cboAño1.SelectedIndex = -1;

            cboDia2.Items.Clear();
            cboDia2.Items.Add("01");
            cboDia2.Items.Add("02");
            cboDia2.Items.Add("03");
            cboDia2.Items.Add("04");
            cboDia2.Items.Add("05");
            cboDia2.Items.Add("06");
            cboDia2.Items.Add("07");
            cboDia2.Items.Add("08");
            cboDia2.Items.Add("09");
            cboDia2.Items.Add("10");
            cboDia2.Items.Add("11");
            cboDia2.Items.Add("12");
            cboDia2.Items.Add("13");
            cboDia2.Items.Add("14");
            cboDia2.Items.Add("15");
            cboDia2.Items.Add("16");
            cboDia2.Items.Add("17");
            cboDia2.Items.Add("18");
            cboDia2.Items.Add("19");
            cboDia2.Items.Add("20");
            cboDia2.Items.Add("21");
            cboDia2.Items.Add("22");
            cboDia2.Items.Add("23");
            cboDia2.Items.Add("24");
            cboDia2.Items.Add("25");
            cboDia2.Items.Add("26");
            cboDia2.Items.Add("27");
            cboDia2.Items.Add("28");
            cboDia2.Items.Add("29");
            cboDia2.Items.Add("30");
            cboDia2.Items.Add("31");
            cboDia2.SelectedIndex = -1;

            cboMes2.Items.Clear();
            cboMes2.Items.Add("01");
            cboMes2.Items.Add("02");
            cboMes2.Items.Add("03");
            cboMes2.Items.Add("04");
            cboMes2.Items.Add("05");
            cboMes2.Items.Add("06");
            cboMes2.Items.Add("07");
            cboMes2.Items.Add("08");
            cboMes2.Items.Add("09");
            cboMes2.Items.Add("10");
            cboMes2.Items.Add("11");
            cboMes2.Items.Add("12");
            cboMes2.SelectedIndex = -1;

            cboAño2.Items.Clear();
            cboAño2.Items.Add("2023");
            cboAño2.Items.Add("2024");
            cboAño2.Items.Add("2025");
            cboAño2.Items.Add("2026");
            cboAño2.Items.Add("2027");
            cboAño2.Items.Add("2028");
            cboAño2.Items.Add("2029");
            cboAño2.Items.Add("2030");
            cboAño2.Items.Add("2031");
            cboAño2.Items.Add("2032");
            cboAño2.Items.Add("2033");
            cboAño2.Items.Add("2034");
            cboAño2.SelectedIndex = -1;

            try
            {
                cboSerie.Items.Clear();
                con.conectar_base_interno();
                con.open_c_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT SERIE ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM CAT_DONDE_VA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + " GROUP BY SERIE ";
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    cboSerie.Items.Add(con.leer_interno[0].ToString().Trim());
                    cbbRanSerie.Items.Add(con.leer_interno[0].ToString().Trim());
                    cbbSerieFOL.Items.Add(con.leer_interno[0].ToString().Trim());
                }
                cboSerie.SelectedIndex = -1;

                //CERRAR CONEXIÓN 
                con.cerrar_interno();
                //** LLENAMOS COMBOS DE USUARIOS
                cbbUsuarioCarto.Items.Clear();
                con.conectar_base_interno();
                con.open_c_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT USUARIO ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE SERIE =" + util.scm(Program.serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "   GROUP BY USUARIO";

                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    cbbUsuarioCarto.Items.Add(con.leer_interno[0].ToString().Trim());
                }
                cbbUsuarioCarto.SelectedIndex = -1;
                //CERRAR CONEXIÓN 
                con.cerrar_interno();
                //** LLENAMOS COMBOS DE USUARIOS VENTANILLA**//
                cbbUsuarioVenta.Items.Clear();
                con.conectar_base_interno();
                con.open_c_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT USUARIO";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_VENTANILLA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE SERIE =" + util.scm(Program.serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "   GROUP BY USUARIO";

                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    cbbUsuarioVenta.Items.Add(con.leer_interno[0].ToString().Trim());
                }
                cbbUsuarioVenta.SelectedIndex = -1;
                //CERRAR CONEXIÓN 
                con.cerrar_interno();
                cbbTrammite.Items.Clear();

                con.conectar_base_interno();
                con.open_c_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT cnv.UBICACION,  cnv.DESCRIPCION ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025 cnc, CAT_NEW_VENTANILLA_2025 cnv";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE cnc.FOLIO_ORIGEN = cnv.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "   GROUP BY cnv.UBICACION, cnv.DESCRIPCION";
                con.cadena_sql_interno = con.cadena_sql_interno + "   ORDER BY cnv.UBICACION";

                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    cbbTrammite.Items.Add(con.leer_interno[0].ToString().Trim() + " - " + con.leer_interno[1].ToString().Trim());
                }
                cbbTrammite.SelectedIndex = -1;
                //CERRAR CONEXIÓN 
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL GENERAR LA CONSULTA" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return;
            }
            //** llenamos mas combos**//


        }
        public void CapturarPantalla()
        {
            // Definir la ruta de la carpeta
            string carpetaCapturas = @"C:\SONGUI\CAPTURAS";

            // Crear la carpeta si no existe
            if (!Directory.Exists(carpetaCapturas))
            {
                Directory.CreateDirectory(carpetaCapturas);
                Console.WriteLine($"Carpeta creada: {carpetaCapturas}");
            }

            // Obtener el tamaño de la pantalla principal
            Rectangle bounds = Screen.PrimaryScreen.Bounds;

            // Crear un bitmap con las dimensiones de la pantalla
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    // Capturar la pantalla
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                // Generar nombre de archivo con timestamp
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string nombreArchivo = $"captura_{timestamp}.png";
                string filePath = Path.Combine(carpetaCapturas, nombreArchivo);

                // Guardar la imagen
                bitmap.Save(filePath, ImageFormat.Png);

                Console.WriteLine($"Captura guardada en: {filePath}");
            }
        }
        private void btnCancela_Click(object sender, EventArgs e)
        {
            limpiarTodo();
            limpiarArriba();
            limpiarPanelAbajo();
        }
        /////////////////////////////////////////////////////////////////
        //BOTÓN PARA CERRAR EL FORMULARIO
        ////////////////////////////////////////////////////////////////
        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //////////////////////////////////////////////
        //Botón para minimizar el formulario 
        ////////////////////////////////////////////
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        ///////////////////////////////////////////////////////////////
        //PARA COLOCARLE LA FECHA Y HORA EN EL FORMULARIO
        ///////////////////////////////////////////////////////////////
        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ssss");
        }
        //////////////////////////////////////////////////////////////
        //BOTÓN PARA INICIAR UN NUEVO PROCESO 
        //////////////////////////////////////////////////////////////
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //habilitarArriba();
            pnlFiltro.Enabled = true;
            cbbSerieFOL.Enabled = false;
            txtFolio.Enabled = false;
            cbbRanSerie.Enabled = false;
            TXTrango1Fol.Enabled = false;
            TXTrango2Fol.Enabled = false;
            llenadoCombos();
            btnNuevo.Enabled = false;
            btnSalida.Enabled = false;
            btnCancela.Enabled = true;
            btnBUscar.Enabled = true;
            btnCancela.Enabled = true;
            rbSerieFolio.Enabled = true;
            rbSerieFolio.Checked = false;
            rdSerFol.Enabled = true;
            rdSerFol.Checked = false;
            rbRangosFolios.Enabled = true;
            rbRangosFolios.Checked = false;
            rbClave.Enabled = true;
            rbClave.Checked = false;
            rbFechaIni.Enabled = true;
            rbFechaIni.Checked = false;
            rbIdenticiudadano.Enabled = true;
            rbIdenticiudadano.Checked = false;
            rbElaboroVenta.Enabled = true;
            rbElaboroVenta.Checked = false;
            rbTipoTramite.Enabled = true;
            rbTipoTramite.Checked = false;

            //** deshabiltamos cajas **//
            cboSerie.Enabled = false;
            txtFolio.Enabled = false;
            cboDia1.Enabled = false;
            cboMes1.Enabled = false;
            cboAño1.Enabled = false;
            cboDia2.Enabled = false;
            cboMes2.Enabled = false;
            cboAño2.Enabled = false;
            cbbSerieFOL.Enabled = false;
            txtFolio.Enabled = false;
            cbbUsuarioCarto.Enabled = false;
            cbbUsuarioVenta.Enabled = false;
            rbAutorizado.Enabled = true;
            rbNoAutorizado.Enabled = true;
            btnConsulta.Enabled = true;
            btnLimpiarAbajo.Enabled = true;
            rbNoAutorizado.Checked = true;


        }
        ////////////////////////////////////////////////////////
        //////CAJAS AMARILLAS
        ////////////////////////////////////////////////////////
        private void txtFolio_Enter(object sender, EventArgs e)
        {
            cajas_amarillas(1);
        }
        private void cboSerie_Enter(object sender, EventArgs e)
        {
            cboSerie.BackColor = Color.Yellow;
        }
        private void cboDia1_Enter(object sender, EventArgs e)
        {
            cajas_amarillas(3);
        }

        private void cboMes1_Enter(object sender, EventArgs e)
        {
            cajas_amarillas(4);
        }

        private void cboAño1_Enter(object sender, EventArgs e)
        {
            cajas_amarillas(5);
        }

        private void cboDia2_Enter(object sender, EventArgs e)
        {
            cajas_amarillas(6);
        }
        private void cboMes2_Enter(object sender, EventArgs e)
        {
            cajas_amarillas(7);
        }
        private void cboAño2_Enter(object sender, EventArgs e)
        {
            cajas_amarillas(8);
        }
        private void txtObservacionTramite_Enter(object sender, EventArgs e)
        {
            cajas_amarillas(9);
        }
        /////////////////////////////////////////////////////////////////////////////
        //////CAJA BLANCA AL SALIR DE LA CAJA DE TEXTO   
        /////////////////////////////////////////////////////////////////////////////
        private void txtFolio_Leave(object sender, EventArgs e)
        {
            cajas_blancas(1);
        }
        private void txtObservacionTramite_Leave(object sender, EventArgs e)
        {
            cajas_blancas(9);
        }
        /////////////////////////////////////////////////////////////////////////////
        //////ACEPTAR SOLO NÚMERO 
        /////////////////////////////////////////////////////////////////////////////
        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void btnNuevo_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnNuevo, "NUEVO PROCESO");
        }
        private void btnCancela_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCancela, "LIMPIAR TODO");
        }
        private void btnSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnSalida, "SALIR DE LA PANTALLA");
        }

        //////////////////////////////////////////////////////////////////////////////
        //////////  CONSULTA INICIO, 0 Y 0 EN REVISO Y ELIMINADO 
        /////////////////////////////////////////////////////////////////////////////
        void consultaGeneral()
        {
            // panel de busquedas

            ///****// 
            limpiardatagrid();
            limpiarPanelAbajo();
            lblDescripcionVentanilla.Text = "";
            lblFechaHoraVentanilla.Text = "";
            lblObservacionesVentanilla.Text = "";
            lblUsuarioVentanillla.Text = "";
            //cartografía limpiar lo de abajo 
            lblDescripcionCartografia.Text = "";
            lblFechaHoraCartografia.Text = "";
            lblObservacionesCartografia.Text = "";
            lblUsuarioCartografia.Text = "";

            //botones
            cmdAutorizarProcesoIndividual.Enabled = false;
            //btnPendienteSistemas.Enabled = true;
            //btnRevisadosVentanilla.Enabled = true;
            btnCancelarProceso.Enabled = false;
            //
            txtObservacionTramite.Focus();
            txtObservacionTramite.Text = "";
            SistemasFaltante = 0;

            //* se debe validar cada caja minimo contenga algun dato***/
            if (cboSerie.Text == "")
            {
                if (txtFolio.Text == "")
                {
                    if (cboDia1.Text == "")
                    {
                        if (cboMes1.Text == "")
                        {
                            if (cboAño1.Text == "")
                            {
                                if (cboDia2.Text == "")
                                {
                                    if (cboMes2.Text == "")
                                    {
                                        if (cboAño2.Text == "")
                                        {
                                            if (cbbUsuarioCarto.Text == "")
                                            {
                                                if (cbbUsuarioVenta.Text == "")
                                                {
                                                    if (rbAutorizado.Checked == false)
                                                    {
                                                        if (rbNoAutorizado.Checked == false)
                                                        {
                                                            if (rbAutorizado.Checked == false)
                                                            {
                                                                if (cbbTrammite.Text == "")
                                                                {
                                                                    if (txtZona.Text == "")
                                                                    {
                                                                        if (txtManzana.Text == "")
                                                                        {
                                                                            if (txtLote.Text == "")
                                                                            {
                                                                                if (txtEdificio.Text == "")
                                                                                {
                                                                                    if (txtDepto.Text == "")
                                                                                    {
                                                                                        MessageBox.Show("SE DEBE DE INGRESAR ALGÚN CRITERIO DE BÚSQUEDA", "ERROR", MessageBoxButtons.OK);
                                                                                        return;
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
            /// validamos cajas obligatorias
            if (rbSerieFolio.Checked == true) { if (cboSerie.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR LA SERIE", "ERROR", MessageBoxButtons.OK); cboSerie.Focus(); return; } }
            // if (rbSerieFolio.Checked == true) { if (txtFolio.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL FOLIO", "ERROR", MessageBoxButtons.OK); txtFolio.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboDia1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DÍA INICIAL", "ERROR", MessageBoxButtons.OK); cboDia1.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboMes1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL MES INICIAL", "ERROR", MessageBoxButtons.OK); cboMes1.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboAño1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL AÑO INICIAL", "ERROR", MessageBoxButtons.OK); cboAño1.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboDia2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DÍA FINAL", "ERROR", MessageBoxButtons.OK); cboDia2.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboMes2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL MES FINAL", "ERROR", MessageBoxButtons.OK); cboMes2.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboAño2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL AÑO FINAL", "ERROR", MessageBoxButtons.OK); cboAño2.Focus(); return; } }
            if (rbTipoTramite.Checked == true) { if (cbbTrammite.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL TRÁMITE", "ERROR", MessageBoxButtons.OK); cbbTrammite.Focus(); return; } }
            if (rdSerFol.Checked == true)
            {
                if (cbbSerieFOL.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR LA SERIE", "ERROR", MessageBoxButtons.OK); cbbSerieFOL.Focus(); return; }
                if (txtFolio.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL FOLIO", "ERROR", MessageBoxButtons.OK); txtFolio.Focus(); return; }
            }
            if (rbRangosFolios.Checked == true)
            {
                if (cbbRanSerie.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR LA SERIE", "ERROR", MessageBoxButtons.OK); cbbRanSerie.Focus(); return; }
                if (TXTrango1Fol.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL FOLIO INICIAL", "ERROR", MessageBoxButtons.OK); TXTrango1Fol.Focus(); return; }
                if (TXTrango2Fol.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL FOLIO FINAL", "ERROR", MessageBoxButtons.OK); TXTrango2Fol.Focus(); return; }
                if (Convert.ToInt32(TXTrango1Fol.Text.Trim()) > Convert.ToInt32(TXTrango2Fol.Text.Trim())) { MessageBox.Show("EL FOLIO INICIAL NO PUEDE SER MAYOR AL FOLIO FINAL", "ERROR", MessageBoxButtons.OK); TXTrango1Fol.Focus(); return; }
            }

            if (rbFechaIni.Checked == true)
            {
                validaCajas();
                ValidacionD = 0; //sirve para validar fecha  esta variable 
                validaFecha();
                if (ValidacionD == 1) { return; } //al ser uno, no hace las siguientes acciones 
                fecha_iniL = cboAño1.Text + cboMes1.Text + cboDia1.Text + " 00:00:00"; //fecha inicial con la hr, primeros momentos del día 
                fecha_finL = cboAño2.Text + cboMes2.Text + cboDia2.Text + " 23:59:59"; //fecha final con la hr , ultimo momento del día 
            }
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = ""; //Limpiamos la cadena de conexión
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cnc.SERIE, cnc.FOLIO, cnv.Municipio 'MUNICIPIO', cnc.Zona 'ZONA', cnc.MANZANA, cnc.Lote 'LOTE', ";
                con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.Edificio 'EDIFICIO', cnc.Depto 'DEPTO',";
                con.cadena_sql_interno = con.cadena_sql_interno + "           CAST(cnc.FECHA AS DATETIME) + ' ' + CAST(cnc.HORA AS DATETIME) AS 'FECHA_Y_HORA_CARTOGRAFIA', ";
                con.cadena_sql_interno = con.cadena_sql_interno + "           cnv.DESCRIPCION 'CONCEPTO VENTANILLA', CAST(cnv.FECHA AS DATETIME)+ ' ' + CAST(cnv.HORA AS DATETIME) AS 'FECHA_Y_HORA_VENTANILLA', ";
                con.cadena_sql_interno = con.cadena_sql_interno + "           CASE cdv.REVISO";
                con.cadena_sql_interno = con.cadena_sql_interno + "               WHEN 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "               THEN 'PENDIENTE'";
                con.cadena_sql_interno = con.cadena_sql_interno + "               WHEN 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "               THEN 'APROBADO'";
                con.cadena_sql_interno = con.cadena_sql_interno + "           END 'ESTATUS'";
                con.cadena_sql_interno = con.cadena_sql_interno + "      FROM CAT_DONDE_VA_2025 cdv, CAT_NEW_CARTOGRAFIA_2025 cnc, CAT_NEW_VENTANILLA_2025 cnv";
                con.cadena_sql_interno = con.cadena_sql_interno + "      WHERE cdv.CARTOGRAFIA = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.VENTANILLA = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.ELIMINADO = 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.SISTEMAS = 0";
                if (rbNoAutorizado.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.REVISO = 0";  //pendientes en reviso
                }
                if (rbAutorizado.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.REVISO = 1";  //autorizados reviso
                }
                if (rbSerieFolio.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.SERIE =" + util.scm(cboSerie.Text.Trim().ToString());  //serie oara jugar
                }
                if (rdSerFol.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.SERIE =" + util.scm(cbbSerieFOL.Text.Trim().ToString());  //serie oara jugar
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.FOLIO_ORIGEN =" + Convert.ToInt32(txtFolio.Text.Trim());  //folio para jugar
                }
                if (rbRangosFolios.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.SERIE =" + util.scm(cbbRanSerie.Text.Trim().ToString());  //serie oara jugar
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.FOLIO_ORIGEN >=" + Convert.ToInt32(TXTrango1Fol.Text.Trim());  //folio 1  para jugar
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.FOLIO_ORIGEN <=" + Convert.ToInt32(TXTrango2Fol.Text.Trim());  //folio 2  para jugar
                }
                //*** seccion fija **//
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND cdv.FOLIO_ORIGEN = cnc.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND cdv.SERIE = cnc.SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.ESTADO =" + Program.PEstado; //estado fijo
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.MUNICIPIO = " + Program.municipioN; //municipio fijo
                                                                                                                      //**  final de fijo **/

                if (rbClave.Checked == true)/// tomar valores para clave catastral
                {
                    if (txtZona.Text.Trim() == "")
                    {
                        if (txtManzana.Text.Trim() == "")
                        {
                            if (txtLote.Text.Trim() == "")
                            {
                                if (txtEdificio.Text.Trim() == "")
                                {
                                    if (txtDepto.Text.Trim() == "")
                                    {
                                        MessageBox.Show("SE DEBE DE INGRESAR ALGÚN DATO EN CLAVE CATASTRAL", "ERROR", MessageBoxButtons.OK);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (txtZona.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.ZONA =" + txtZona.Text.ToString().Trim(); //concatenar zona
                    }
                    if (txtManzana.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.MANZANA =" + txtManzana.Text.ToString().Trim();
                    }
                    if (txtLote.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.LOTE =" + txtLote.Text.ToString().Trim();
                    }
                    if (txtEdificio.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.EDIFICIO ='" + txtEdificio.Text.ToString().Trim() + "'";
                    }
                    if (txtDepto.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.DEPTO ='" + txtDepto.Text.ToString().Trim() + "'";
                    }
                }
                if (rbFechaIni.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "  AND cnc.FECHA  >= ' " + fecha_iniL + " ' "; //agregarle la fecha inicial a la consulta; mayor a
                    con.cadena_sql_interno = con.cadena_sql_interno + "  AND cnc.FECHA  <= ' " + fecha_finL + " ' "; //agregarle a la fecha final a la consulta ; menor a 
                }
                if (rbIdenticiudadano.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "   AND cnc.USUARIO = '" + cbbUsuarioCarto.Text.ToString().Trim() + "'"; //concatenar usuario cartografía
                }
                //** seccion fija **//
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.FOLIO_ORIGEN = cnv.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.SERIE = cnv.SERIE";
                //**  final de fijo **/
                if (cbbTrammite.Text != "")
                {
                    string[] parts = cbbTrammite.Text.Split('-');
                    string part1 = parts[0].Trim(); // UBICACION
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.UBICACION = '" + part1 + "'"; //concatenar el trámite 
                }
                if (rbElaboroVenta.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnv.USUARIO= '" + cbbUsuarioVenta.Text.ToString().Trim() + "'"; //concatenar usuario ventanilla
                }
                CadenaComplemento = con.cadena_sql_interno;

                //** agramos los demas opciones de bsuqueda en dado caso **// 
                con.cadena_sql_interno = con.cadena_sql_interno + "      ORDER BY cnc.FOLIO_ORIGEN DESC"; //ordenar con el folio de manera ascendente 
                DataTable LLENAR_GRID_1 = new DataTable();
                con.conectar_base_interno();
                con.open_c_interno();
                SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ///
                ///** va contener la cadena de busqueda **//

                //** contiene la cadena de busaueda**//
                if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
                {
                    MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN REFERENTE A LA BÚSQUEDA", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error); //por si no hay nada 
                    con.cerrar_interno();
                    txtObservacionTramite.Text = string.Empty;
                    txtObservacionTramite.Enabled = false;
                    return;
                }
                else //en caso de encontrar un dato, se realiza toda la acción de abajo 
                {
                    dgResultado.DataSource = LLENAR_GRID_1; //FORMA PARA LLENAR EL DATAGRIDVIEW CON LA CONSULTA 
                    con.cerrar_interno();
                    dgResultado.EnableHeadersVisualStyles = false; // Desactiva estilos predeterminados
                    dgResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 54, 151); //COLOR DEL ENCABEZADO CON RGB 
                    dgResultado.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    dgResultado.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8); //FUENTE PARA LAS CELDAS 
                    dgResultado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; //COLOR DE LETRA DEL ENCABEZADO EN BLANCO 
                    foreach (DataGridViewColumn columna in dgResultado.Columns)
                    {
                        columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    foreach (DataGridViewColumn columna in dgResultado.Columns)
                    {
                        columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    dgResultado.Columns[0].Width = 80; //SERIE
                    dgResultado.Columns[1].Width = 80; //FOLIO
                    dgResultado.Columns[2].Width = 80; //MUNICIPIO 
                    dgResultado.Columns[3].Width = 80; //ZONA 
                    dgResultado.Columns[4].Width = 80; //MANZANA
                    dgResultado.Columns[5].Width = 80; //LOTE 
                    dgResultado.Columns[6].Width = 100; //EDIFICIO
                    dgResultado.Columns[7].Width = 100; //DEPTO
                    dgResultado.Columns[8].Width = 250; //FECHA 
                    dgResultado.Columns[9].Width = 250; //CONCEPTO 
                    dgResultado.Columns[10].Width = 250; //FECHA Y HORA 
                    dgResultado.Columns[11].Width = 250; //ESTATUS
                    dgResultado.Columns[1].DefaultCellStyle.Format = "N0"; //darle formato de miles a la celda 1
                    dgResultado.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //SELECCIONAR TODA LA FILA 
                    dgResultado.MultiSelect = false; // Solo permitir selección de una fila a la vez

                    // Deshabilitar edición
                    dgResultado.ReadOnly = true;
                    // Estilos visuales
                    dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                    dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 
                    dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                    lblConteo.Text = (dgResultado.Rows.Count - 1).ToString(); //contamos menos 1 para que sea sin el espacio en blanco 
                    txtObservacionTramite.Enabled = false;
                    btnMasAutoriza.Enabled = false;
                    btnMasCancel.Enabled = false;
                    // txtObservacionTramite.Focus();

                    ///** validamos los botones***//
                    if (dgResultado.Rows.Count == 2)
                    {
                        btnMasCancel.Enabled = false;
                        btnMasAutoriza.Enabled = false;
                        btnCancelarProceso.Enabled = true;
                        cmdAutorizarProcesoIndividual.Enabled = true;
                        txtObservacionTramite.Enabled = true;
                        txtObservacionTramite.Focus();
                    }
                    else
                    {
                        btnMasCancel.Enabled = true;
                        btnMasAutoriza.Enabled = true;
                        btnCancelarProceso.Enabled = false;
                        cmdAutorizarProcesoIndividual.Enabled = false;
                        txtObservacionTramite.Enabled = true;
                        txtObservacionTramite.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar la consulta" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }
           
        }
        private void panel30_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btmLimpiar1_Click(object sender, EventArgs e)
        {

            rbSerieFolio.Checked = false;
            rdSerFol.Checked = false;
            rbRangosFolios.Checked = false;
        }

        private void btmLimpiar2_Click(object sender, EventArgs e)
        {
            rbFechaIni.Checked = false;
        }

        private void btmLimpiar4_Click(object sender, EventArgs e)
        {
            rbIdenticiudadano.Checked = false;
        }

        private void btmLimpiar5_Click(object sender, EventArgs e)
        {
            rbElaboroVenta.Checked = false;
        }
        private void rbSerieFolio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSerieFolio.Checked == true)
            {
                cboSerie.Enabled = true;
                cboSerie.SelectedIndex = 0;

            }
            else
            {
                cboSerie.Enabled = false;
                cboSerie.SelectedIndex = -1;

            }
        }
        private void rbFechaIni_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFechaIni.Checked == true)
            {
                cboDia1.Enabled = true;
                cboMes1.Enabled = true;
                cboAño1.Enabled = true;
                cboDia1.SelectedIndex = -1;
                cboMes1.SelectedIndex = -1;
                cboAño1.SelectedIndex = -1;
                cboDia2.Enabled = true;
                cboMes2.Enabled = true;
                cboAño2.Enabled = true;
                cboDia2.SelectedIndex = -1;
                cboMes2.SelectedIndex = -1;
                cboAño2.SelectedIndex = -1;
            }
            else
            {
                cboDia1.Enabled = false;
                cboMes1.Enabled = false;
                cboAño1.Enabled = false;
                cboDia1.SelectedIndex = -1;
                cboMes1.SelectedIndex = -1;
                cboAño1.SelectedIndex = -1;
                cboDia2.Enabled = false;
                cboMes2.Enabled = false;
                cboAño2.Enabled = false;
                cboDia2.SelectedIndex = -1;
                cboMes2.SelectedIndex = -1;
                cboAño2.SelectedIndex = -1;
            }

        }
        private void rbIdenticiudadano_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdenticiudadano.Checked == true)
            {
                cbbUsuarioCarto.Enabled = true;
                cbbUsuarioCarto.SelectedIndex = -1;
            }
            else
            {
                cbbUsuarioCarto.Enabled = false;
                cbbUsuarioCarto.SelectedIndex = -1;
            }
        }

        private void btmLimpiar7_Click(object sender, EventArgs e)
        {
            rbTipoTramite.Checked = false;
        }

        private void rbTipoTramite_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTipoTramite.Checked == true)
            {
                cbbTrammite.Enabled = true;
                cbbTrammite.SelectedIndex = -1;
            }
            else
            {
                cbbTrammite.Enabled = false;
                cbbTrammite.SelectedIndex = -1;
            }
        }

        private void rbClave_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClave.Checked == true)
            {
                txtZona.Text = string.Empty;
                txtZona.Enabled = true;
                txtManzana.Text = string.Empty;
                txtManzana.Enabled = false;
                txtLote.Text = string.Empty;
                txtLote.Enabled = false;
                txtEdificio.Text = string.Empty;
                txtEdificio.Enabled = false;
                txtDepto.Text = string.Empty;
                txtDepto.Enabled = false;
                txtZona.Focus();
            }
            else
            {
                txtZona.Text = string.Empty;
                txtZona.Enabled = false;
                txtManzana.Text = string.Empty;
                txtManzana.Enabled = false;
                txtLote.Text = string.Empty;
                txtLote.Enabled = false;
                txtEdificio.Text = string.Empty;
                txtEdificio.Enabled = false;
                txtDepto.Text = string.Empty;
                txtDepto.Enabled = false;
            }
        }

        private void txtZona_Leave(object sender, EventArgs e)
        {
            txtZona.BackColor = Color.White;
        }

        private void txtZona_Enter(object sender, EventArgs e)
        {
            txtZona.BackColor = Color.Yellow;
        }

        private void txtManzana_Leave(object sender, EventArgs e)
        {
            txtManzana.BackColor = Color.White;
        }

        private void txtManzana_Enter(object sender, EventArgs e)
        {
            txtManzana.BackColor = Color.Yellow;
        }

        private void txtLote_Leave(object sender, EventArgs e)
        {
            txtLote.BackColor = Color.White;
        }

        private void txtLote_Enter(object sender, EventArgs e)
        {
            txtLote.BackColor = Color.Yellow;
        }

        private void txtEdificio_Leave(object sender, EventArgs e)
        {
            txtEdificio.BackColor = Color.White;
        }

        private void txtEdificio_Enter(object sender, EventArgs e)
        {
            txtEdificio.BackColor = Color.Yellow;
        }

        private void txtDepto_Leave(object sender, EventArgs e)
        {
            txtDepto.BackColor = Color.White;
        }

        private void txtDepto_Enter(object sender, EventArgs e)
        {
            txtDepto.BackColor = Color.Yellow;
        }

        private void txtZona_TextChanged(object sender, EventArgs e)
        {
            if (txtZona.Text.Length == 2)
            {
                txtManzana.Text = string.Empty;
                txtManzana.Enabled = true;
                txtManzana.Focus();
            }
        }

        private void txtManzana_TextChanged(object sender, EventArgs e)
        {
            if (txtManzana.Text.Length == 3)
            {
                txtLote.Text = string.Empty;
                txtLote.Enabled = true;
                txtLote.Focus();
            }
        }

        private void txtLote_TextChanged(object sender, EventArgs e)
        {
            if (txtLote.Text.Length == 2)
            {
                txtEdificio.Text = string.Empty;
                txtEdificio.Enabled = true;
                txtEdificio.Focus();
            }
        }

        private void txtEdificio_TextChanged(object sender, EventArgs e)
        {
            if (txtEdificio.Text.Length == 2)
            {
                txtDepto.Text = string.Empty;
                txtDepto.Enabled = true;
                txtDepto.Focus();
            }
        }

        private void btnBUscar_Click(object sender, EventArgs e)
        {
            frmCatastro03BusquedaCatastro buscar = new frmCatastro03BusquedaCatastro();
            buscar.ShowDialog();
            rbClave.Checked = true;
            txtZona.Text = Program.zonaV;
            txtManzana.Text = Program.manzanaV;
            txtLote.Text = Program.loteV;
            txtEdificio.Text = Program.edificioV;
            txtDepto.Text = Program.deptoV;
        }

        private void btmLimpiar8_Click(object sender, EventArgs e)
        {
            rbClave.Checked = false;
        }

        private void rdSerFol_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSerFol.Checked == true)
            {
                cbbSerieFOL.Enabled = true;
                cbbSerieFOL.SelectedIndex = 0;
                txtFolio.Text = string.Empty;
                txtFolio.Enabled = true;
                txtFolio.Focus();
            }
            else
            {
                cbbSerieFOL.Enabled = false;
                cbbSerieFOL.SelectedIndex = -1;
                txtFolio.Text = string.Empty;
                txtFolio.Enabled = false;
            }
        }

        private void rbRangosFolios_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRangosFolios.Checked == true)
            {
                cbbRanSerie.Enabled = true;
                cbbRanSerie.SelectedIndex = 0;
                TXTrango1Fol.Enabled = true;
                TXTrango1Fol.Text = string.Empty;
                TXTrango2Fol.Enabled = true;
                TXTrango2Fol.Text = string.Empty;
                TXTrango1Fol.Focus();
            }
            else
            {
                cbbRanSerie.Enabled = false;
                cbbRanSerie.SelectedIndex = -1;
                TXTrango1Fol.Text = string.Empty;
                TXTrango1Fol.Enabled = false;
                TXTrango2Fol.Enabled = false;
                TXTrango2Fol.Text = string.Empty;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SistemasFaltante = 1;
            con.conectar_base_interno();
            con.cadena_sql_interno = ""; //Limpiamos la cadena de conexión

            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT CNC.SERIE, CNC.FOLIO_ORIGEN, CNC.MUNICIPIO, CNC.ZONA, CNC.MANZANA, CNC.LOTE,";
            con.cadena_sql_interno = con.cadena_sql_interno + "        CNC.EDIFICIO, CNC.DEPTO, CNC.DESCRIPCION, FORMAT(CAST(cnc.FECHA AS DATETIME), 'dd-MM-yyyy') + ' ' + FORMAT(CAST(cnc.HORA AS DATETIME), 'HH:mm:ss') AS 'FECHA_Y_HORA_CARTOGRAFIA' , CNC.OBSERVACIONES, CNC.USUARIO  ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_DONDE_VA_2025 CDV, CAT_NEW_CARTOGRAFIA_2025 CNC ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CDV.VENTANILLA = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.SERIE = CNC.SERIE";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.FOLIO_ORIGEN = CNC.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY CNC.SERIE, CNC.FOLIO_ORIGEN";
            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN REFERENTE A LA BÚSQUEDA", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error); //por si no hay nada 
                con.cerrar_interno();
                txtObservacionTramite.Text = string.Empty;
                txtObservacionTramite.Enabled = false;
                pnlFiltro.Enabled = true;
                return;
            }
            else //en caso de encontrar un dato, se realiza toda la acción de abajo 
            {
                dgResultado.DataSource = LLENAR_GRID_1; //FORMA PARA LLENAR EL DATAGRIDVIEW CON LA CONSULTA 
                con.cerrar_interno();
                dgResultado.EnableHeadersVisualStyles = false; // Desactiva estilos predeterminados
                dgResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 54, 151); //COLOR DEL ENCABEZADO CON RGB 
                dgResultado.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                dgResultado.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8); //FUENTE PARA LAS CELDAS 
                dgResultado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; //COLOR DE LETRA DEL ENCABEZADO EN BLANCO 
                foreach (DataGridViewColumn columna in dgResultado.Columns)
                {
                    columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                foreach (DataGridViewColumn columna in dgResultado.Columns)
                {
                    columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dgResultado.Columns[0].Width = 80; //SERIE
                dgResultado.Columns[1].Width = 80; //FOLIO
                dgResultado.Columns[2].Width = 80; //MUNICIPIO 
                dgResultado.Columns[3].Width = 80; //ZONA 
                dgResultado.Columns[4].Width = 80; //MANZANA
                dgResultado.Columns[5].Width = 80; //LOTE 
                dgResultado.Columns[6].Width = 100; //EDIFICIO
                dgResultado.Columns[7].Width = 100; //DEPTO
                dgResultado.Columns[8].Width = 250; //CONCEPTO
                dgResultado.Columns[9].Width = 250; //FECHA 
                dgResultado.Columns[10].Width = 250; //CONCEPTO 
                dgResultado.Columns[11].Width = 250; //FECHA Y HORA 
                //dgResultado.Columns[12].Width = 250; //ESTATUS
                dgResultado.Columns[1].DefaultCellStyle.Format = "N0"; //darle formato de miles a la celda 1
                dgResultado.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //SELECCIONAR TODA LA FILA 
                dgResultado.MultiSelect = false; // Solo permitir selección de una fila a la vez
                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 
                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString(); //contamos menos 1 para que sea sin el espacio en blanco 
                txtObservacionTramite.Enabled = false;
                pnlFiltro.Enabled = true;
                pnlTAPAR.Visible = true;
                pnlDatosAlta.Visible = false;
                btnCancelarProceso.Enabled = false;
                cmdAutorizarProcesoIndividual.Enabled = false;
                btnMasCancel.Enabled = false;
                btnMasAutoriza.Enabled = false;
                // txtObservacionTramite.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SistemasFaltante = 1;
            con.conectar_base_interno();
            con.cadena_sql_interno = ""; //Limpiamos la cadena de conexión
            con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cnc.SERIE, cnc.FOLIO, cnv.Municipio 'MUNICIPIO', cnc.Zona 'ZONA', cnc.MANZANA, cnc.Lote 'LOTE', ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.Edificio 'EDIFICIO', cnc.Depto 'DEPTO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.DESCRIPCION 'CONCEPTO CARTOGRAFIA', cnc.FECHA + ' ' + cnc.HORA 'FECHA Y HORA CARTOGRAFIA', ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnv.DESCRIPCION 'CONCEPTO VENTANILLA',  cnv.FECHA + ' ' + cnv.HORA 'FECHA Y HORA VENTANILLA', ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           CASE cdv.REVISO";
            con.cadena_sql_interno = con.cadena_sql_interno + "               WHEN 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "               THEN 'PENDIENTE'";
            con.cadena_sql_interno = con.cadena_sql_interno + "               WHEN 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "               THEN 'APROBADO'";
            con.cadena_sql_interno = con.cadena_sql_interno + "           END 'ESTATUS'";
            con.cadena_sql_interno = con.cadena_sql_interno + "      FROM CAT_DONDE_VA_2025 cdv, CAT_NEW_CARTOGRAFIA_2025 cnc, CAT_NEW_VENTANILLA_2025 cnv";
            con.cadena_sql_interno = con.cadena_sql_interno + "      WHERE cdv.CARTOGRAFIA = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.SERIE =" + util.scm(Program.serie);
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.VENTANILLA = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.ELIMINADO = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.REVISO = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND cdv.SISTEMAS = 0";
            //*** seccion fija **//
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cdv.FOLIO_ORIGEN = cnc.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cdv.SERIE = cnc.SERIE";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.ESTADO =" + Program.PEstado; //estado fijo
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.MUNICIPIO = " + Program.municipioN; //municipio fijo
            //** seccion fija **//
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.FOLIO_ORIGEN = cnv.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.SERIE = cnv.SERIE";
            //**  final de fijo **/
            con.cadena_sql_interno = con.cadena_sql_interno + "      ORDER BY cnc.FOLIO_ORIGEN ASC"; //ordenar con el folio de manera ascendente 
            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN REFERENTE A LA BÚSQUEDA", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error); //por si no hay nada 
                con.cerrar_interno();
                txtObservacionTramite.Text = string.Empty;
                txtObservacionTramite.Enabled = false;
                pnlFiltro.Enabled = true;
                return;
            }
            else //en caso de encontrar un dato, se realiza toda la acción de abajo 
            {
                dgResultado.DataSource = LLENAR_GRID_1; //FORMA PARA LLENAR EL DATAGRIDVIEW CON LA CONSULTA 
                con.cerrar_interno();
                dgResultado.EnableHeadersVisualStyles = false; // Desactiva estilos predeterminados
                dgResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 54, 151); //COLOR DEL ENCABEZADO CON RGB 
                dgResultado.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                dgResultado.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8); //FUENTE PARA LAS CELDAS 
                dgResultado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; //COLOR DE LETRA DEL ENCABEZADO EN BLANCO 
                foreach (DataGridViewColumn columna in dgResultado.Columns)
                {
                    columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                foreach (DataGridViewColumn columna in dgResultado.Columns)
                {
                    columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dgResultado.Columns[0].Width = 80; //SERIE
                dgResultado.Columns[1].Width = 80; //FOLIO
                dgResultado.Columns[2].Width = 80; //MUNICIPIO 
                dgResultado.Columns[3].Width = 80; //ZONA 
                dgResultado.Columns[4].Width = 80; //MANZANA
                dgResultado.Columns[5].Width = 80; //LOTE 
                dgResultado.Columns[6].Width = 100; //EDIFICIO
                dgResultado.Columns[7].Width = 100; //DEPTO
                dgResultado.Columns[8].Width = 250; //CONCEPTO
                dgResultado.Columns[9].Width = 250; //FECHA 
                dgResultado.Columns[10].Width = 250; //CONCEPTO 
                dgResultado.Columns[11].Width = 250; //FECHA Y HORA 
                dgResultado.Columns[12].Width = 250; //ESTATUS
                dgResultado.Columns[1].DefaultCellStyle.Format = "N0"; //darle formato de miles a la celda 1
                dgResultado.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //SELECCIONAR TODA LA FILA 
                dgResultado.MultiSelect = false; // Solo permitir selección de una fila a la vez

                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 
                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString(); //contamos menos 1 para que sea sin el espacio en blanco 
                txtObservacionTramite.Enabled = false;
                pnlFiltro.Enabled = true;
                pnlTAPAR.Visible = true;
                pnlDatosAlta.Visible = false;
                // pnlFiltro.Enabled = false;
                // txtObservacionTramite.Focus();
                btnCancelarProceso.Enabled = false;
                cmdAutorizarProcesoIndividual.Enabled = false;
            }
        }

        private void btnMasAutoriza_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿SE TOMARÁN TODOS LOS FOLIOS EN LA REHILLA DE INFORMACION PARA AUTORIZAR CON UN TOTAL DE: " + lblConteo.Text.Trim() + " PROCESOS", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) //Se le indica que hay un error con el folio tal de la fila
            {
                if (txtObservacionTramite.Text == "") { MessageBox.Show("NECESITAS COLOCAR UNA OBSERVACION", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error); txtObservacionTramite.Focus(); return; }
                if (txtObservacionTramite.Text.Length < 10) { MessageBox.Show("LA OBSERVACION DEBE DE SER MAYOR A 10 CARACTERES", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtObservacionTramite.Focus(); return; }

                observaReviso = txtObservacionTramite.Text.Trim();
                OperacionTramite = 1; //es para poner 0 en 0 en ambos lados , REVISO Y ELIMINADO 
                //int validaProceso = 0;
                usuario = Program.nombre_usuario; //valor que se va a pasar al procedimiento almacenado 
                string FechaDia = DateTime.Now.ToString("yyyyMMdd");
                string HoraDia = DateTime.Now.ToString("HH:mm:ss");


                string buscarCadena = "FROM CAT_DONDE_VA_2025 cdv"; //cadena que se va a buscar
                int indice = CadenaComplemento.IndexOf(buscarCadena);
                if (indice >= 0)
                {
                    string resultado = CadenaComplemento.Substring(indice);
                    CadenaComplemento = resultado; // Actualiza CadenaComplemento con la subcadena encontrada
                }
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    UPDATE CAT_DONDE_VA_2025";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    SET REVISO =" + OperacionTramite;  //2 es para cancelar
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , FECHA_REV =" + util.scm(FechaDia);//fecha de hoy
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , HORA_REV = " + util.scm(HoraDia);//hora de hoy
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , OBSERVA_REVISO =" + util.scm(txtObservacionTramite.Text.Trim());//observacion
                    con.cadena_sql_interno = con.cadena_sql_interno + "     , USU_REVISO =" + util.scm(usuario);//usuario que lo reviso
                    con.cadena_sql_interno = con.cadena_sql_interno + "  " + CadenaComplemento; //cadena que se genero en la busqueda
                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.cmd_interno.ExecuteReader();
                    con.cerrar_interno();

                    MessageBox.Show("SE AUTORIZO CON EXITO LOS FOLIOS", "¡INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //** LIMPIAR TODO Y VOLVER A INICIAR
                    limpiarTodo();
                    limpiarArriba();
                    limpiarPanelAbajo();
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR AL CANCELAR LOS FOLIOS " + e, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMasCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿SE TOMARÁN TODOS LOS FOLIOS EN LA REJILLA DE INFORMACION PARA NO AUTORIZR UN TOTAL DE: " + lblConteo.Text.Trim() + " PROCESOS", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) //Se le indica que hay un error con el folio tal de la fila
            {
                if (txtObservacionTramite.Text == "") { MessageBox.Show("NECESITAS COLOCAR UNA OBSERVACION", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error); txtObservacionTramite.Focus(); return; }
                if (txtObservacionTramite.Text.Length < 10) { MessageBox.Show("LA OBSERVACION DEBE DE SER MAYOR A 10 CARACTERES", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtObservacionTramite.Focus(); return; }

                observaReviso = txtObservacionTramite.Text.Trim();
                OperacionTramite = 1; //es para poner 0 en 0 en ambos lados , REVISO Y ELIMINADO 
                //int validaProceso = 0;
                usuario = Program.nombre_usuario; //valor que se va a pasar al procedimiento almacenado 
                string FechaDia = DateTime.Now.ToString("yyyyMMdd");
                string HoraDia = DateTime.Now.ToString("HH:mm:ss");


                string buscarCadena = "FROM CAT_DONDE_VA_2025 cdv"; //cadena que se va a buscar
                int indice = CadenaComplemento.IndexOf(buscarCadena);
                if (indice >= 0)
                {
                    string resultado = CadenaComplemento.Substring(indice);
                    CadenaComplemento = resultado; // Actualiza CadenaComplemento con la subcadena encontrada
                }
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    UPDATE CAT_DONDE_VA_2025";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    SET ELIMINADO =" + OperacionTramite;  //2 es para cancelar
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , REVISO =" + OperacionTramite;  //3 es para cancelar / revisado por la jefa
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , FECHA_REV =" + util.scm(FechaDia);//fecha de hoy
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , HORA_REV = " + util.scm(HoraDia);//hora de hoy
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , OBSERVA_REVISO =" + util.scm(txtObservacionTramite.Text.Trim());//observacion
                    con.cadena_sql_interno = con.cadena_sql_interno + "     , USU_REVISO =" + util.scm(usuario);//usuario que lo reviso
                    con.cadena_sql_interno = con.cadena_sql_interno + "  " + CadenaComplemento; //cadena que se genero en la busqueda
                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.cmd_interno.ExecuteReader();
                    con.cerrar_interno();

                    MessageBox.Show("SE CANCELO CON EXITO LOS FOLIOS", "¡INFORMACION!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //** LIMPIAR TODO Y VOLVER A INICIAR
                    limpiarTodo();
                    limpiarArriba();
                    limpiarPanelAbajo();
                }
                catch (Exception)
                {
                    MessageBox.Show("ERROR AL CANCELAR LOS FOLIOS " + e, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void TXTrango1Fol_Enter(object sender, EventArgs e)
        {
            TXTrango1Fol.BackColor = Color.Yellow;
        }

        private void TXTrango2Fol_Leave(object sender, EventArgs e)
        {
            TXTrango2Fol.BackColor = Color.White;
        }

        private void TXTrango2Fol_Enter(object sender, EventArgs e)
        {
            TXTrango2Fol.BackColor = Color.Yellow;
        }

        private void TXTrango1Fol_Leave(object sender, EventArgs e)
        {
            TXTrango1Fol.BackColor = Color.White;
        }

        private void TXTrango1Fol_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void TXTrango2Fol_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void txtZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void txtManzana_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void cboSerie_Leave(object sender, EventArgs e)
        {
            cboSerie.BackColor = Color.White;
        }

        private void cbbSerieFOL_Leave(object sender, EventArgs e)
        {
            cbbSerieFOL.BackColor = Color.White;
        }

        private void cbbSerieFOL_Enter(object sender, EventArgs e)
        {
            cbbSerieFOL.BackColor = Color.Yellow;
        }

        private void cbbRanSerie_Leave(object sender, EventArgs e)
        {
            cbbRanSerie.BackColor = Color.White;
        }

        private void cbbRanSerie_Enter(object sender, EventArgs e)
        {
            cbbRanSerie.BackColor = Color.Yellow;
        }

        private void pnlFiltro_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlDatosAlta_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbbTrammite_Enter(object sender, EventArgs e)
        {
            cbbTrammite.BackColor = Color.Yellow;
        }

        private void cbbTrammite_Leave(object sender, EventArgs e)
        {
            cbbTrammite.BackColor = Color.White;
        }

        private void cbbUsuarioCarto_Enter(object sender, EventArgs e)
        {
            cbbUsuarioCarto.BackColor = Color.Yellow;
        }

        private void cbbUsuarioCarto_Leave(object sender, EventArgs e)
        {
            cbbUsuarioCarto.BackColor = Color.White;
        }

        private void cbbUsuarioVenta_Leave(object sender, EventArgs e)
        {
            cbbUsuarioVenta.BackColor = Color.White;
        }

        private void cbbUsuarioVenta_Enter(object sender, EventArgs e)
        {
            cbbUsuarioVenta.BackColor = Color.Yellow;
        }

        private void cboDia1_Leave(object sender, EventArgs e)
        {
            cboDia1.BackColor = Color.White;
        }

        private void cboMes1_Leave(object sender, EventArgs e)
        {
            cboMes1.BackColor = Color.White;
        }

        private void cboAño1_Leave(object sender, EventArgs e)
        {
            cboAño1.BackColor = Color.White;
        }

        private void cboDia2_Enter_1(object sender, EventArgs e)
        {
            cboDia2.BackColor = Color.Yellow;
        }

        private void cboDia2_Leave(object sender, EventArgs e)
        {
            cboDia2.BackColor = Color.White;
        }

        private void cboMes2_Leave(object sender, EventArgs e)
        {
            cboMes2.BackColor = Color.White;
        }

        private void cboMes2_Enter_1(object sender, EventArgs e)
        {
            cboMes2.BackColor = Color.Yellow;
        }

        private void cboAño2_Leave(object sender, EventArgs e)
        {
            cboAño2.BackColor = Color.White;
        }

        private void cboAño2_Enter_1(object sender, EventArgs e)
        {
            cboAño2.BackColor = Color.Yellow;
        }

        private void btnBUscar_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnBUscar, "BUSCAR");
        }

        private void btnLimpiarAbajo_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnLimpiarAbajo, "LIMPIAR FILTRO");
        }

        private void dgResultado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            string latitud = lblLatitud.Text.Trim();
            string longitud = lblLonguitud.Text.Trim();

            //return $"https://www.google.com/maps?q={latitud},{longitud}";
            Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
        }

        private void rbElaboroVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbElaboroVenta.Checked == true)
            {
                cbbUsuarioVenta.Enabled = true;
                cbbUsuarioVenta.SelectedIndex = -1;
            }
            else
            {
                cbbUsuarioVenta.Enabled = false;
                cbbUsuarioVenta.SelectedIndex = -1;
            }
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            consultaGeneral();
        }

        private void dgResultado_DoubleClick(object sender, EventArgs e)
        {

            if (SistemasFaltante == 0) // si es normal habilita botones  individuales
            {
                btnMasCancel.Enabled = false;
                btnMasAutoriza.Enabled = false;
                btnCancelarProceso.Enabled = false;
                cmdAutorizarProcesoIndividual.Enabled = false;
                CargaRejilla();
                Coordenadas();
            }
            else// si es faltante de sistema y desahbilita botones
            {
                CargaRejilla();
                Coordenadas();
                btnMasCancel.Enabled = false;
                btnMasAutoriza.Enabled = false;
                btnCancelarProceso.Enabled = false;
                cmdAutorizarProcesoIndividual.Enabled = false;
                txtObservacionTramite.Enabled = false;
            }


            //if (ventanillaFaltante == 0)// si es faltante de ventanilla
            //{
            //    CargaRejilla();
            //    Coordenadas();
            //    btnMasCancel.Enabled = false;
            //    btnMasAutoriza.Enabled = false;
            //    btnCancelarProceso.Enabled = false;
            //    cmdAutorizarProcesoIndividual.Enabled = false;
            //    txtObservacionTramite.Enabled = false;
            //}
            //else// si  es de ventanilla entra aca deshabilita botones debe autoriza ventailla
            //{
            //    CargaRejilla();
            //    Coordenadas();
            //    btnMasCancel.Enabled = false;
            //    btnMasAutoriza.Enabled = false;
            //    btnCancelarProceso.Enabled = true;
            //    cmdAutorizarProcesoIndividual.Enabled = true;
            //    txtObservacionTramite.Enabled = true;
            //    txtObservacionTramite.Focus();
            //}
        }
        void Coordenadas()
        {
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT Latitud, Longitud FROM SONG_GEOLOCALIZACION";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE Zona =" + Convert.ToInt32(dgResultado.CurrentRow.Cells[3].Value.ToString());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana =" + Convert.ToInt32(dgResultado.CurrentRow.Cells[4].Value.ToString());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote =" + Convert.ToInt32(dgResultado.CurrentRow.Cells[5].Value.ToString());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio =" + util.scm(dgResultado.CurrentRow.Cells[6].Value.ToString());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto =" + util.scm(dgResultado.CurrentRow.Cells[7].Value.ToString());
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                if (!con.leer_interno.HasRows)
                {
                    lblLatitud.Text = string.Empty;
                    lblLatitud.Enabled = false;
                    lblLonguitud.Text = string.Empty;
                    lblLonguitud.Enabled = false;
                    btnMaps.Enabled = false;
                }
                else
                {
                    while (con.leer_interno.Read())
                    {
                        lblLatitud.Text = con.leer_interno["Latitud"].ToString().Trim();
                        lblLonguitud.Text = con.leer_interno["Longitud"].ToString().Trim();
                    }
                    btnMaps.Enabled = true;
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar el proceso N19_CALCULO_CATASTRO" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }

        }
        void CargaRejilla()
        {
            limpiarPanelAbajo();
            var cellValue = dgResultado.CurrentRow.Cells[1].Value.ToString();
            if (string.IsNullOrWhiteSpace(cellValue?.ToString())) //ver esto 
            {
                MessageBox.Show("SE GENERÓ UN ERROR, NO HAY INFORMACIÓN", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //solo guardarlo en variable para temporal 
            string serieP = dgResultado.CurrentRow.Cells[0].Value.ToString().Trim(); //Sacar la serie de la celda del datagrid para convertirlo a string / cadena de texto 
            int folioP = (Convert.ToInt32(dgResultado.CurrentRow.Cells[1].Value.ToString().Trim())); //Sacar el folio de la celda para pasarlo a entero
                                                                                                     //colocar en texto 
                                                                                                     //txtFolio.Text = folioP.ToString(); //colocar el folio en la caja de texto
            lblDescripcionCartografia.Text = (dgResultado.CurrentRow.Cells[9].Value.ToString().Trim());
            lblFechaHoraCartografia.Text = (dgResultado.CurrentRow.Cells[8].Value.ToString().Trim());
            lblDescripcionVentanilla.Text = (dgResultado.CurrentRow.Cells[9].Value.ToString().Trim()); //Para colocar los valores en cada txt de la parte de abajo
            lblFechaHoraVentanilla.Text = (dgResultado.CurrentRow.Cells[10].Value.ToString().Trim());

            ///*** se llena arriba la informacion ***///
            lblserie.Text = string.Empty;
            lblFolio.Text = string.Empty;
            lblZona.Text = string.Empty;
            lblManzana.Text = string.Empty;
            lblLote.Text = string.Empty;
            lblEdificio.Text = string.Empty;
            lblDepto.Text = string.Empty;

            lblserie.Text = dgResultado.CurrentRow.Cells[0].Value.ToString().Trim();
            lblFolio.Text = folioP.ToString();
            lblZona.Text = dgResultado.CurrentRow.Cells[3].Value.ToString().Trim();
            lblManzana.Text = dgResultado.CurrentRow.Cells[4].Value.ToString().Trim();
            lblLote.Text = dgResultado.CurrentRow.Cells[5].Value.ToString().Trim();
            lblEdificio.Text = dgResultado.CurrentRow.Cells[6].Value.ToString().Trim();
            lblDepto.Text = dgResultado.CurrentRow.Cells[7].Value.ToString().Trim();

            pnlTAPADERA.Visible = true;
            //** se llena abajo la informacion **//
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT CV.USUARIO, CC.USUARIO, CV.FOLIO_ORIGEN, CV.OBSERVACIONES, CC.OBSERVACIONES ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025 CC,  CAT_NEW_VENTANILLA_2025 CV  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CC.FOLIO_ORIGEN = CV.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CC.SERIE = CV.SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CV.FOLIO_ORIGEN = " + folioP;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CC.SERIE = " + util.scm(serieP) + "";
                con.conectar_base_interno();
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "") //Colocar en cada caja de texto cada resultado
                    {
                        lblUsuarioVentanillla.Text = con.leer_interno[0].ToString().Trim(); //Colocar en cada caja de texto cada resultado
                        lblUsuarioCartografia.Text = con.leer_interno[1].ToString().Trim();
                        folioTemp = Convert.ToInt32(con.leer_interno[2].ToString().Trim());
                        lblObservacionesVentanilla.Text = con.leer_interno[3].ToString().Trim();
                        lblObservacionesCartografia.Text = con.leer_interno[4].ToString().Trim();
                    }
                }
                //cerrar la conexión 
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }

            //habilitar los botones , etc 
            cmdAutorizarProcesoIndividual.Enabled = true;
            btnCancelarProceso.Enabled = true;
            btnLimpiarAbajo.Enabled = true;
            txtObservacionTramite.Text = string.Empty;
            txtObservacionTramite.Enabled = true;
            btnMasCancel.Enabled = true;
            btnMasAutoriza.Enabled = true;
            txtObservacionTramite.Focus();
            pnlDatosAlta.Visible = false;
            pnlTAPAR.Visible = true;

            btnMasCancel.Enabled = false;
            btnMasAutoriza.Enabled = false;
            btnCancelarProceso.Enabled = true;
            cmdAutorizarProcesoIndividual.Enabled = true;


            if (lblDescripcionCartografia.Text == "ALTA DE CLAVE")
            {
                pnlDatosAlta.Visible = true;
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " SELECT ZON_ORIGEN, TERR_PROPIO, TERR_COMUN, SUP_CON, SUP_CON_COM, FRENTE,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "        FONDO, REGIMEN, COD_CALLE, IRREGULARIDAD, TOPOGRAFiA, ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "        VAL_TERRENO , VAL_TERRENO_COMUN , VAL_CONST, VAL_CONST_COMUN ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE FOLIO_ORIGEN = " + folioTemp;
                    con.conectar_base_interno();
                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();
                    while (con.leer_interno.Read())
                    {
                        if (con.leer_interno[0].ToString().Trim() != "") //Colocar en cada caja de texto cada resultado
                        {
                            lblZonaOrigen.Text = con.leer_interno[0].ToString().Trim(); //Colocar en cada caja de texto cada resultado
                            lblSupTerr.Text = Convert.ToDouble(con.leer_interno[1].ToString(), CultureInfo.InvariantCulture).ToString("N2"); //Colocar en cada caja de texto cada resultado
                            lblSupTerrCom.Text = Convert.ToDouble(con.leer_interno[2].ToString(), CultureInfo.InvariantCulture).ToString("N2"); //Colocar en cada caja de texto cada resultado
                            lblSupCons.Text = Convert.ToDouble(con.leer_interno[3].ToString(), CultureInfo.InvariantCulture).ToString("N2"); //Colocar en cada caja de texto cada resultado
                            lblSupConsCom.Text = Convert.ToDouble(con.leer_interno[4].ToString(), CultureInfo.InvariantCulture).ToString("N2"); //Colocar en cada caja de texto cada resultado
                            lblFrente.Text = Convert.ToDouble(con.leer_interno[5].ToString(), CultureInfo.InvariantCulture).ToString("N2"); //Colocar en cada caja de texto cada resultado
                            lblFondo.Text = Convert.ToDouble(con.leer_interno[6].ToString(), CultureInfo.InvariantCulture).ToString("N2"); //Colocar en cada caja de texto cada resultado
                            lblRegimen.Text = con.leer_interno[7].ToString().Trim(); //Colocar en cada caja de texto cada resultado
                            lblCalle.Text = con.leer_interno[8].ToString().Trim(); //Colocar en cada caja de texto cada resultado
                            lblDesnivel.Text = con.leer_interno[9].ToString().Trim(); //Colocar en cada caja de texto cada resultado
                            lblAreaT.Text = con.leer_interno[10].ToString().Trim(); //Colocar en cada caja de texto cada resultado

                            double resultado =
                            (string.IsNullOrEmpty(con.leer_interno[11].ToString().Trim()) ? 0 : double.Parse(con.leer_interno[11].ToString().Trim()) +
                            (string.IsNullOrEmpty(con.leer_interno[12].ToString().Trim()) ? 0 : double.Parse(con.leer_interno[12].ToString().Trim()) +
                            (string.IsNullOrEmpty(con.leer_interno[13].ToString().Trim()) ? 0 : double.Parse(con.leer_interno[13].ToString().Trim()) +
                            (string.IsNullOrEmpty(con.leer_interno[14].ToString().Trim()) ? 0 : double.Parse(con.leer_interno[14].ToString().Trim())))));
                            // Asigna el resultado convertido a texto al Label
                            lblValorCat.Text = resultado.ToString("N2");

                        }
                    }
                    txtObservacionTramite.Text = string.Empty;
                    txtObservacionTramite.Enabled = true;
                    txtObservacionTramite.Focus();
                    pnlDatosAlta.Enabled = true;
                    pnlTAPAR.Enabled = false;
                    con.cerrar_interno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al executar " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    // Retornar false si ocurre un error
                }

                pnlTAPAR.Visible = false;
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " SELECT NOMCALLE ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CALLES";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CODCALLE = " + util.scm(lblCalle.Text.ToString());
                    con.conectar_base_interno();
                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();
                    while (con.leer_interno.Read())
                    {
                        if (con.leer_interno[0].ToString().Trim() != "") //Colocar en cada caja de texto cada resultado
                        {
                            lblCalle.Text = con.leer_interno[0].ToString().Trim(); //Colocar en cada caja de texto cada resultado
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al executar " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    // Retornar false si ocurre un error
                }

                btnMasCancel.Enabled = false;
                btnMasAutoriza.Enabled = false;
                btnCancelarProceso.Enabled = true;
                cmdAutorizarProcesoIndividual.Enabled = true;
            }
        }
        private void cmdAutorizarProcesoIndividual_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿QUIERES APROBAR EL TRAMITE DEL FOLIO? : " + lblFolio.Text.Trim(), "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) //Se le indica que hay un error con el folio tal de la fila
            {
                if (txtObservacionTramite.Text == "") { MessageBox.Show("NECESITAS COLOCAR UNA OBSERVACION", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error); txtObservacionTramite.Focus(); return; }
                if (txtObservacionTramite.Text.Length < 10) { MessageBox.Show("LA OBSERVACION DEBE DE SER MAYOR A 10 CARACTERES", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtObservacionTramite.Focus(); return; }
                observaReviso = txtObservacionTramite.Text.Trim();
                OperacionTramite = 1; //es para poner 0 en 0 en ambos lados , REVISO Y ELIMINADO 
                //validacionBloqueo = 0;
                usuario = Program.nombre_usuario; //valor que se va a pasar al procedimiento almacenado 
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.open_c_interno();
                    SqlCommand cmd = new SqlCommand("SONGSP_AUTORIZACIONTRAMITES_2025", con.cnn_interno);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OPERACION", SqlDbType.VarChar, 2).Value = OperacionTramite;
                    cmd.Parameters.Add("@SERIE", SqlDbType.VarChar, 2).Value = Program.serie;
                    cmd.Parameters.Add("@FOLIO", SqlDbType.Int, 5).Value = lblFolio.Text.Trim();
                    cmd.Parameters.Add("@USUREVISO", SqlDbType.Char, 50).Value = usuario;
                    cmd.Parameters.Add("@OBSERVAREVISO", SqlDbType.NChar, 50).Value = observaReviso;
                    cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                    cmd.Connection = con.cnn_interno;
                    cmd.ExecuteNonQuery();
                    validacionProcedimiento = Convert.ToInt32(cmd.Parameters["@VALIDACION"].Value);
                    con.cerrar_interno();
                    if (validacionProcedimiento == 1) //Al generarse de manera correcta lo reestablece bien 
                    {
                        MessageBox.Show("SE APROBÓ CON ÉXITO EL PROCESO DEL FOLIO: " + lblFolio.Text.Trim(), "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information); //Se le indica que hay un error con el folio tal de la fila
                        //limpiarTodo();
                        limpiarPanelAbajo();
                        consultaGeneral();
                        deshabilitarbotonesabajo();
                        lblLatitud.Text = string.Empty;
                        lblLonguitud.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("OCURRIÓ UN ERROR", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarTodo();
                        // se bueno regresarlo a como estaba?????

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OCURRIÓ UN ERROR, COMUNICATE CON EL ADMINISTRADOR" + ex, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    limpiarTodo();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        /// PARA CANCELAR LOS PROCESOS DE UN FOLIO, REVISO 0 , ELIMINO 0 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancelarProceso_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿QUIERES CANCELAR EL TRAMITE DEL FOLIO? : " + lblserie.Text.ToString() + " - " + lblFolio.Text.Trim(), "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) //Se le indica que hay un error con el folio tal de la fila
            {
                if (txtObservacionTramite.Text == "") { MessageBox.Show("NECESITAS COLOCAR UNA OBSERVACION", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error); txtObservacionTramite.Focus(); return; }
                if (txtObservacionTramite.Text.Length < 10) { MessageBox.Show("LA OBSERVACION DEBE DE SER MAYOR A 10 CARACTERES", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtObservacionTramite.Focus(); return; }
                observaReviso = txtObservacionTramite.Text.Trim();
                OperacionTramite = 2; //es 1 y 1 reviso y eliminado 
                //validacionBloqueo = 0;
                usuario = Program.nombre_usuario; //valor que se va a pasar al procedimiento almacenado 
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.open_c_interno();
                    SqlCommand cmd = new SqlCommand("SONGSP_AUTORIZACIONTRAMITES_2025", con.cnn_interno);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OPERACION", SqlDbType.VarChar, 2).Value = OperacionTramite;
                    cmd.Parameters.Add("@SERIE", SqlDbType.VarChar, 2).Value = Program.serie;
                    cmd.Parameters.Add("@FOLIO", SqlDbType.Int, 5).Value = lblFolio.Text.Trim();
                    cmd.Parameters.Add("@USUREVISO", SqlDbType.Char, 50).Value = usuario;
                    cmd.Parameters.Add("@OBSERVAREVISO", SqlDbType.NChar, 50).Value = observaReviso;
                    cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                    cmd.Connection = con.cnn_interno;
                    cmd.ExecuteNonQuery();
                    validacionProcedimiento = Convert.ToInt32(cmd.Parameters["@VALIDACION"].Value);
                    con.cerrar_interno();
                    if (validacionProcedimiento == 1) //Al generarse de manera correcta lo reestablece bien 
                    {
                        MessageBox.Show("SE CANCELÓ CON ÉXITO EL PROCESO DEL FOLIO: " + folioTemp, "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information); //Se le indica que hay un error con el folio tal de la fila
                        //limpiarTodo();
                        limpiarPanelAbajo();
                        consultaGeneral();
                        deshabilitarbotonesabajo();
                    }
                    else
                    {
                        MessageBox.Show("OCURRIÓ UN ERROR, COMUNICATE CON EL ADMINISTRADOR", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiarTodo();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("OCURRIÓ UN ERROR, COMUNICATE CON EL ADMINISTRADOR" + ex, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    // limpiarTodo();
                }
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        /// PARA CANCELAR LOS PROCESOS DE UN FOLIO, REVISO 0 , ELIMINO 0 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancelarProcesoMasivo_Click(object sender, EventArgs e)
        {
            // autorcar();
        }
        private void btnLimpiarAbajo_Click(object sender, EventArgs e)
        {
            //**  limpair paneka de filtros **//
            rbSerieFolio.Checked = false;
            rbFechaIni.Checked = false;
            rbRangosFolios.Checked = false;
            rdSerFol.Checked = false;
            rbSerieFolio.Checked = false;
            rbIdenticiudadano.Checked = false;
            rbElaboroVenta.Checked = false;
            rbTipoTramite.Checked = false;
            rbClave.Checked = false;
            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            btnMasAutoriza.Enabled = false;
            btnMasCancel.Enabled = false;
            txtFolio.Text = string.Empty;
            lblLonguitud.Text = string.Empty;
            lblLatitud.Text = string.Empty;
            btnMaps.Enabled = false;
            SistemasFaltante = 0;
            ventanillaFaltante = 0;
            pnlTAPADERA.Visible = false;

            //ventanilla limpiar lo de abajo
            lblDescripcionVentanilla.Text = "";
            lblFechaHoraVentanilla.Text = "";
            lblObservacionesVentanilla.Text = "";
            lblUsuarioVentanillla.Text = "";
            //cartografía limpiar lo de abajo 
            lblDescripcionCartografia.Text = "";
            lblFechaHoraCartografia.Text = "";
            lblObservacionesCartografia.Text = "";
            lblUsuarioCartografia.Text = "";

            //botones
            cmdAutorizarProcesoIndividual.Enabled = false;
            btnCancelarProceso.Enabled = false;
            //
            //txtObservacionTramite.Focus();
            txtObservacionTramite.Text = "";
            txtObservacionTramite.Enabled = false;
            txtObservacionTramite.BackColor = Color.White;
            limpiarPanelAbajo();
            rbFechaIni.Focus();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        /// CONSULTA GENERAL, REVISO 0, ELIMINADO 0 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        void deshabilitarbotonesabajo()
        {
            btnCancelarProceso.Enabled = false;
            cmdAutorizarProcesoIndividual.Enabled = false;
            txtObservacionTramite.Enabled = false;
        }
    }
}

