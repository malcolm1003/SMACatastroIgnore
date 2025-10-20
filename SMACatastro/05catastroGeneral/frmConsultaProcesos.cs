using AccesoBase;
using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Math.Field;
using SMACatastro.catastroCartografia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilerias;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;

namespace SMACatastro.catastroRevision
{
    public partial class frmConsultaProcesos : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2(); //Comenzamos con declarar las variables que se deben de utilizar      
        Util util = new Util();
        public int ValidacionD = 0;
        int folioTemp, OperacionTramite, validacionProcedimiento, folio, serie, numRows = 0;
        string serieTemp, usuario, fecha_iniL, fecha_finL, observaReviso = "";
        public int SistemasFaltante, ventanillaFaltante = 0;
        public int FolioConsulta = 0;
        public string SerieConsulta = "";
        public int EdoCarto, EdoVenta, EdoRevision, EdoSistemas, EdoEliminado = 0;
        public string ComentarioObservaSis, ComentarioObservaRev = "";
        //METODO PARA ARRASTRAR EL FORMULARIO---
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        //*** semaforo **//
        private enum SemaforoEstado { Rojo, Naranja, Amarillo, Verde }
        private SemaforoEstado _estadoActual;

        // Controla el acceso a la lógica del semáforo (opcional, para demostración de SemaphoreSlim)
        private readonly SemaphoreSlim _semaforoLogic = new SemaphoreSlim(1, 1);
        private CancellationTokenSource _cts;

        public frmConsultaProcesos()
        {
            InitializeComponent();
            ResetPantlla();
        }
        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ssss");
        }
        private void btnCancela_Click(object sender, EventArgs e)
        {
            ResetPantlla();
            _cts?.Cancel();
            pcARRIBA.Visible = false;
            pcizquierda.Visible = false;
            pcDerecho.Visible = false;
            pnLOcultaSemaforo.Visible = true;
            pnlTabla.Visible = false;
            //_cancellationTokenSource.Cancel();
        }

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
                //CapturarPantalla();
                con.cerrar_interno();
                return;
            }
            //** llenamos mas combos**//

        }
        void ResetPantlla()
        {
            PcFlechaDerecha1.Visible = false;
            PcFlechaDerecha2.Visible = false;
            PcFlechaDerecha3.Visible = false;
            PcFlechaDerecha4.Visible = false;
            pnlInformativo.Visible = false;
            pnlTabla.Visible = false;
            pcARRIBA.Visible = false;
            pcDerecho.Visible = false;
            pcizquierda.Visible = false;
            pnlTAPADERA.Visible = false;
            RtxtBoxObservaCarto.Clear();
            RtxtBoxObservaVentnilla.Clear();
            RtxtBoxObservaRevision.Clear();
            RtxtBoxObservaSistemas.Clear();
            //** BOTONES PARTE SUPERIOR**//
            btnNuevo.Enabled = true;
            btnBUscar.Enabled = false;
            btnCancela.Enabled = false;
            btnSalida.Enabled = true;
            pnlTAPADERA.Visible = false;
            //** ETIQUETAS**//
            lblZona.Text = string.Empty;
            lblManzana.Text = string.Empty;
            lblLote.Text = string.Empty;
            lblEdificio.Text = string.Empty;
            lblDepto.Text = string.Empty;
            lblserie.Text = string.Empty;
            lblFolio.Text = string.Empty;
            //** FILTRO DE BUSUQEDA**//
            pnlFiltro.Enabled = false;
            rbFechaIni.Checked = false;
            btmLimpiar2.Enabled = false;

            //** TIPO TRAMITE **//
            rbTipoTramite.Checked = false;
            btmLimpiar7.Enabled = false;
            cbbTrammite.SelectedIndex = -1;
            //** USUARIO CARTOGRAFIA**//
            rbIdenticiudadano.Checked = false;
            btmLimpiar4.Enabled = false;
            cbbUsuarioCarto.SelectedIndex = -1;
            //** USUARIO VENTANILLA**/
            rbElaboroVenta.Checked = false;
            btmLimpiar5.Enabled = false;
            cbbUsuarioVenta.SelectedIndex = -1;
            //** CALVE CATASTRAL**/
            rbClave.Checked = false;
            btmLimpiar8.Enabled = false;
            txtZona.Text = string.Empty;
            txtManzana.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtEdificio.Text = string.Empty;
            txtDepto.Text = string.Empty;
            //** SERIE Y FOLIO**//
            rbSerieFolio.Checked = false;
            cboSerie.SelectedIndex = -1;
            rdSerFol.Checked = false;
            cbbSerieFOL.SelectedIndex = -1;
            txtFolio.Text = string.Empty;
            rbRangosFolios.Checked = false;
            cbbRanSerie.SelectedIndex = -1;
            TXTrango1Fol.Text = string.Empty;
            TXTrango2Fol.Text = string.Empty;
            //** BOTONES DE PENDIENTES**//
            //** botones consulta*//
            btnLimpiarAbajo.Enabled = false;
            btnConsulta.Enabled = false;
            //** REJILLA DE RESULTADOS**//
            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            lblConteo.Text = "0";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pnlFiltro.Enabled = true;
            btnBUscar.Enabled = true;
            btnCancela.Enabled = true;
            btnNuevo.Enabled = false;
            btnConsulta.Enabled = true;
            btnLimpiarAbajo.Enabled = true;
            rbFechaIni.Checked = false;
            rbTipoTramite.Checked = false;
            rbIdenticiudadano.Checked = false;
            rbElaboroVenta.Checked = false;
            rbClave.Checked = false;
            rbSerieFolio.Checked = false;
            rdSerFol.Checked = false;
            rbRangosFolios.Checked = false;
            //** LLENAMOS COMBOS**/
            llenadoCombos();
        }

        private void cboDia1_Enter(object sender, EventArgs e)
        {
            cboDia1.BackColor = Color.Yellow;
        }

        private void cboDia1_Leave(object sender, EventArgs e)
        {
            cboDia1.BackColor = Color.White;
        }

        private void cboMes1_Leave(object sender, EventArgs e)
        {
            cboMes1.BackColor = Color.White;
        }

        private void cboMes1_Enter(object sender, EventArgs e)
        {
            cboMes1.BackColor = Color.Yellow;
        }

        private void cboAño1_Enter(object sender, EventArgs e)
        {
            cboAño1.BackColor = Color.Yellow;
        }

        private void cboAño1_Leave(object sender, EventArgs e)
        {
            cboAño1.BackColor = Color.White;
        }

        private void cboDia2_Leave(object sender, EventArgs e)
        {
            cboDia2.BackColor = Color.White;
        }

        private void cboDia2_Enter(object sender, EventArgs e)
        {
            cboDia2.BackColor = Color.Yellow;
        }

        private void cboMes2_Enter(object sender, EventArgs e)
        {
            cboMes2.BackColor = Color.Yellow;
        }

        private void cboMes2_Leave(object sender, EventArgs e)
        {
            cboMes2.BackColor = Color.White;
        }

        private void cbbTrammite_Enter(object sender, EventArgs e)
        {
            cbbTrammite.BackColor = Color.Yellow;
        }

        private void cbbTrammite_Leave(object sender, EventArgs e)
        {
            cbbTrammite.BackColor = Color.White;
        }

        private void cbbUsuarioCarto_Leave(object sender, EventArgs e)
        {
            cbbUsuarioCarto.BackColor = Color.White;
        }

        private void cbbUsuarioCarto_Enter(object sender, EventArgs e)
        {
            cbbUsuarioCarto.BackColor = Color.Yellow;
        }

        private void cbbUsuarioVenta_Enter(object sender, EventArgs e)
        {
            cbbUsuarioVenta.BackColor = Color.Yellow;
        }

        private void cbbUsuarioVenta_Leave(object sender, EventArgs e)
        {
            cbbUsuarioVenta.BackColor = Color.White;
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
            txtDepto.BackColor = Color.White;
        }

        private void cboSerie_Leave(object sender, EventArgs e)
        {
            cboSerie.BackColor = Color.White;
        }

        private void cboSerie_Enter(object sender, EventArgs e)
        {
            cboSerie.BackColor = Color.Yellow;
        }

        private void cbbSerieFOL_Leave(object sender, EventArgs e)
        {
            cbbSerieFOL.BackColor = Color.White;
        }

        private void cbbSerieFOL_Enter(object sender, EventArgs e)
        {
            cbbSerieFOL.BackColor = Color.Yellow;
        }

        private void txtFolio_Leave(object sender, EventArgs e)
        {
            txtFolio.BackColor = Color.White;
        }

        private void txtFolio_Enter(object sender, EventArgs e)
        {
            txtFolio.BackColor = Color.Yellow;
        }

        private void cbbRanSerie_Leave(object sender, EventArgs e)
        {
            cbbRanSerie.BackColor = Color.White;
        }

        private void cbbRanSerie_Enter(object sender, EventArgs e)
        {
            cbbRanSerie.BackColor = Color.Yellow;
        }

        private void TXTrango1Fol_Leave(object sender, EventArgs e)
        {
            TXTrango1Fol.BackColor = Color.White;
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

        private void rbFechaIni_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFechaIni.Checked == false)
            {
                cboDia1.SelectedIndex = -1;
                cboDia2.SelectedIndex = -1;
                cboMes1.SelectedIndex = -1;
                cboMes2.SelectedIndex = -1;
                cboAño1.SelectedIndex = -1;
                cboAño2.SelectedIndex = -1;
                cboDia1.Enabled = false;
                cboMes1.Enabled = false;
                cboAño1.Enabled = false;
                cboDia2.Enabled = false;
                cboMes2.Enabled = false;
                cboAño2.Enabled = false;
                btmLimpiar2.Enabled = false;
            }
            else
            {
                cboDia1.Enabled = true;
                cboMes1.Enabled = true;
                cboAño1.Enabled = true;
                cboDia2.Enabled = true;
                cboMes2.Enabled = true;
                cboAño2.Enabled = true;
                btmLimpiar2.Enabled = true;
            }
        }

        private void rbTipoTramite_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTipoTramite.Checked == false)
            {
                cbbTrammite.SelectedIndex = -1;
                cbbTrammite.Enabled = false;
                btmLimpiar7.Enabled = false;
            }
            else
            {
                cbbTrammite.SelectedIndex = -1;
                cbbTrammite.Enabled = true;
                btmLimpiar7.Enabled = true;
            }
        }

        private void rbIdenticiudadano_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdenticiudadano.Checked == false)
            {
                cbbUsuarioCarto.SelectedIndex = -1;
                cbbUsuarioCarto.Enabled = false;
                btmLimpiar4.Enabled = false;
            }
            else
            {
                cbbUsuarioCarto.SelectedIndex = -1;
                cbbUsuarioCarto.Enabled = true;
                btmLimpiar4.Enabled = true;
            }
        }

        private void rbElaboroVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbElaboroVenta.Checked == false)
            {
                cbbUsuarioVenta.SelectedIndex = -1;
                cbbUsuarioVenta.Enabled = false;
                btmLimpiar5.Enabled = false;
            }
            else
            {
                cbbUsuarioVenta.SelectedIndex = -1;
                cbbUsuarioVenta.Enabled = true;
                btmLimpiar5.Enabled = true;
            }
        }

        private void rbClave_CheckedChanged(object sender, EventArgs e)
        {
            if (rbClave.Checked == false)
            {
                txtZona.Text = string.Empty;
                txtManzana.Text = string.Empty;
                txtLote.Text = string.Empty;
                txtEdificio.Text = string.Empty;
                txtDepto.Text = string.Empty;
                txtZona.Enabled = false;
                txtManzana.Enabled = false;
                txtLote.Enabled = false;
                txtEdificio.Enabled = false;
                txtDepto.Enabled = false;
                btmLimpiar8.Enabled = false;
            }
            else
            {
                txtZona.Text = string.Empty;
                txtManzana.Text = string.Empty;
                txtLote.Text = string.Empty;
                txtEdificio.Text = string.Empty;
                txtDepto.Text = string.Empty;
                txtZona.Enabled = true;
                txtManzana.Enabled = true;
                txtLote.Enabled = true;
                txtEdificio.Enabled = true;
                txtDepto.Enabled = true;
                btmLimpiar8.Enabled = true;
                txtZona.Focus();
            }
        }

        private void rbSerieFolio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSerieFolio.Checked == false)
            {
                cboSerie.SelectedIndex = -1;
                cboSerie.Enabled = false;
            }
            else
            {
                cboSerie.SelectedIndex = -1;
                cboSerie.Enabled = true;
            }
        }

        private void rdSerFol_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSerFol.Checked == false)
            {
                cbbSerieFOL.SelectedIndex = -1;
                txtFolio.Text = string.Empty;
                cbbSerieFOL.Enabled = false;
                txtFolio.Enabled = false;
            }
            else
            {
                cbbSerieFOL.SelectedIndex = 0;
                txtFolio.Text = string.Empty;
                cbbSerieFOL.Enabled = true;
                txtFolio.Enabled = true;
                txtFolio.Focus();
            }
        }

        private void rbRangosFolios_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRangosFolios.Checked == false)
            {
                cbbRanSerie.SelectedIndex = -1;
                TXTrango1Fol.Text = string.Empty;
                TXTrango2Fol.Text = string.Empty;
                cbbRanSerie.Enabled = false;
                TXTrango1Fol.Enabled = false;
                TXTrango2Fol.Enabled = false;
            }
            else
            {
                cbbRanSerie.SelectedIndex = 0;
                TXTrango1Fol.Text = string.Empty;
                TXTrango2Fol.Text = string.Empty;
                cbbRanSerie.Enabled = true;
                TXTrango1Fol.Enabled = true;
                TXTrango2Fol.Enabled = true;
                TXTrango1Fol.Focus();
            }
        }
        void RefrescarFiltros()
        {
            rbFechaIni.Checked = true;
            rbTipoTramite.Checked = false;
            rbIdenticiudadano.Checked = false;
            rbElaboroVenta.Checked = false;
            rbClave.Checked = false;
            rbSerieFolio.Checked = false;
            rdSerFol.Checked = false;
            rbRangosFolios.Checked = false;
            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            lblConteo.Text = "0";
            dgResultado.Enabled = false;
        }
        private void btnLimpiarAbajo_Click(object sender, EventArgs e)
        {
            RefrescarFiltros();
            pnlTAPADERA.Visible = false;
            btnCancela.Enabled = true;
            pnlTabla.Visible = false;
            pnLOcultaSemaforo.Visible = true;
        }



        private void btmLimpiar2_Click(object sender, EventArgs e)
        {
            rbFechaIni.Checked = false;
        }

        private void btmLimpiar7_Click(object sender, EventArgs e)
        {
            rbTipoTramite.Checked = false;
        }

        private void btmLimpiar4_Click(object sender, EventArgs e)
        {
            rbIdenticiudadano.Checked = false;
        }

        private void frmConsultaProcesos_Load(object sender, EventArgs e)
        {
            ResetPantlla();
            lblUsuario.Text = "USUARIO: " + Program.nombre_usuario;

        }

        private void btmLimpiar5_Click(object sender, EventArgs e)
        {
            rbElaboroVenta.Checked = false;
        }

        private async void dgResultado_DoubleClick(object sender, EventArgs e)
        {
            //** sacamos valor elegido ** //
            var cellValue = dgResultado.CurrentRow.Cells[1].Value.ToString();
            if (string.IsNullOrWhiteSpace(cellValue?.ToString())) //ver esto 
            {
                MessageBox.Show("SE GENERÓ UN ERROR, NO HAY INFORMACIÓN", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            pnLOcultaSemaforo.Visible = false;

            //solo guardarlo en variable para temporal 
            SerieConsulta = dgResultado.CurrentRow.Cells[0].Value.ToString().Trim(); //Sacar la serie de la celda del datagrid para convertirlo a string / cadena de texto 
            FolioConsulta = (Convert.ToInt32(dgResultado.CurrentRow.Cells[1].Value.ToString().Trim())); //Sacar el folio de la celda del datagrid para convertirlo a entero
            PcFlechaDerecha1.Visible = true;
            PcFlechaDerecha2.Visible = true;
            PcFlechaDerecha3.Visible = true;
            PcFlechaDerecha4.Visible = true;
            pnlInformativo.Visible = true;
            lblEdoCartografia.Text = string.Empty;
            lblEdoVentanilla.Text = string.Empty;
            lblEdoRevision.Text = string.Empty;
            lblEdoSistemas.Text = string.Empty;

            //** cargamos arriba** clave catastral//
            pnlTAPADERA.Visible = true;
            lblserie.Text = string.Empty;
            lblFolio.Text = string.Empty;
            lblZona.Text = string.Empty;
            lblManzana.Text = string.Empty;
            lblLote.Text = string.Empty;
            lblEdificio.Text = string.Empty;
            lblDepto.Text = string.Empty;

            lblserie.Text = dgResultado.CurrentRow.Cells[0].Value.ToString().Trim();
            lblFolio.Text = dgResultado.CurrentRow.Cells[1].Value.ToString().Trim();
            lblZona.Text = dgResultado.CurrentRow.Cells[3].Value.ToString().Trim();
            lblManzana.Text = dgResultado.CurrentRow.Cells[4].Value.ToString().Trim();
            lblLote.Text = dgResultado.CurrentRow.Cells[5].Value.ToString().Trim();
            lblEdificio.Text = dgResultado.CurrentRow.Cells[6].Value.ToString().Trim();
            lblDepto.Text = dgResultado.CurrentRow.Cells[7].Value.ToString().Trim();

            TimeSpan intervalo = TimeSpan.FromSeconds(30);
            var cancellationTokenSource = new CancellationTokenSource();
            // 2. Obtener el token
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            await Task.WhenAll(IniciarConsultaPeriodicaAsync(intervalo, cancellationToken),
                                dgResultado_DoubleClickAsync(this, EventArgs.Empty));
        }

        private void TXTrango1Fol_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void TXTrango2Fol_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void cboAño2_Enter(object sender, EventArgs e)
        {
            cboAño2.BackColor = Color.Yellow;
        }

        private void cboAño2_Leave(object sender, EventArgs e)
        {
            cboAño2.BackColor = Color.White;
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

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void PanelBarraTitulo_MouseHover(object sender, EventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pnlFiltro_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btmLimpiar8_Click(object sender, EventArgs e)
        {
            rbClave.Checked = false;
        }

        private void btmLimpiar1_Click(object sender, EventArgs e)
        {
            rbSerieFolio.Checked = false;
            rdSerFol.Checked = false;
            rbRangosFolios.Checked = false;
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
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            BusquedaGeneral();// metod de busqueda
        }
        void BusquedaGeneral()//** aqui se toman las opciones de busqueda**//
        {

            pnLOcultaSemaforo.Visible = false;
            _cts?.Cancel();
            pcARRIBA.Visible = false;
            pcizquierda.Visible = false;
            pcDerecho.Visible = false;
            pnLOcultaSemaforo.Visible = true;
            pnlTabla.Visible = false;
            if (rbFechaIni.Checked == false && rbTipoTramite.Checked == false && rbIdenticiudadano.Checked == false && rbElaboroVenta.Checked == false && rbClave.Checked == false && rbSerieFolio.Checked == false && rdSerFol.Checked == false && rbRangosFolios.Checked == false)
            {
                MessageBox.Show("SE DEBE DE SELECCIONAR ALGÚN TIPO DE FILTRO PARA REALIZAR LA BÚSQUEDA", "ERROR", MessageBoxButtons.OK); return;
            }

            if (rbFechaIni.Checked == true) { if (cboDia1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DÍA INICIAL", "ERROR", MessageBoxButtons.OK); cboDia1.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboMes1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL MES INICIAL", "ERROR", MessageBoxButtons.OK); cboMes1.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboAño1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL AÑO INICIAL", "ERROR", MessageBoxButtons.OK); cboAño1.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboDia2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DÍA FINAL", "ERROR", MessageBoxButtons.OK); cboDia2.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboMes2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL MES FINAL", "ERROR", MessageBoxButtons.OK); cboMes2.Focus(); return; } }
            if (rbFechaIni.Checked == true) { if (cboAño2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL AÑO FINAL", "ERROR", MessageBoxButtons.OK); cboAño2.Focus(); return; } }

            if (rbTipoTramite.Checked == true) { if (cbbTrammite.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL TRÁMITE", "ERROR", MessageBoxButtons.OK); cbbTrammite.Focus(); return; } }
            if (rbIdenticiudadano.Checked == true) { if (cbbUsuarioCarto.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL USUARIO DE CARTOGRAFÍA", "ERROR", MessageBoxButtons.OK); cbbUsuarioCarto.Focus(); return; } }
            if (rbElaboroVenta.Checked == true) { if (cbbUsuarioVenta.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL USUARIO DE VENTANILLA", "ERROR", MessageBoxButtons.OK); cbbUsuarioVenta.Focus(); return; } }
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
            }
            if (rbSerieFolio.Checked == true)
            {
                if (cboSerie.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR LA SERIE", "ERROR", MessageBoxButtons.OK); cboSerie.Focus(); return; }
            }
            if (rdSerFol.Checked == true)//** folio solito**//
            {
                if (cbbSerieFOL.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR LA SERIE", "ERROR", MessageBoxButtons.OK); cbbSerieFOL.Focus(); return; }
                if (txtFolio.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL FOLIO", "ERROR", MessageBoxButtons.OK); txtFolio.Focus(); return; }
            }
            if (rbRangosFolios.Checked == true)//** rango de folios**// 
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

            //** se arma el query de busqueda**//
            con.conectar_base_interno();
            con.cadena_sql_interno = ""; //Limpiamos la cadena de conexión
            con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cnc.SERIE, cnc.FOLIO, cnc.Municipio ' Mpio', cnc.Zona 'ZONA', cnc.MANZANA ' MZA', cnc.Lote 'LOTE', ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.Edificio 'EDIF', cnc.Depto 'DEPTO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "           CAST(cnc.FECHA AS DATETIME)" +
                " + ' ' + CAST(cnc.HORA AS DATETIME) AS 'FECHA_ORIGEN' ";
            con.cadena_sql_interno = con.cadena_sql_interno + "          ,CNC.DESCRIPCION  ";
            //*** validamos si concatenar datos de la tabla de ventanilla**//
            if (rbElaboroVenta.Checked == true)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "       ,CNC.OBSERVACIONES ";
            }
            //*** validamos si concatenar datos de la tabla de ventanilla**//
            con.cadena_sql_interno = con.cadena_sql_interno + "      FROM CAT_DONDE_VA_2025 cdv, CAT_NEW_CARTOGRAFIA_2025 cnc";
            if (rbElaboroVenta.Checked == true)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + ", CAT_NEW_VENTANILLA_2025 cnv ";
            }
            con.cadena_sql_interno = con.cadena_sql_interno + "      WHERE cdv.CARTOGRAFIA = 1";


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
            if (rbElaboroVenta.Checked == true)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.FOLIO_ORIGEN = cnv.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.SERIE = cnv.SERIE";
            }
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

            //** agramos los demas opciones de bsuqueda en dado caso **// 
            con.cadena_sql_interno = con.cadena_sql_interno + "      ORDER BY cnc.FOLIO_ORIGEN ASC"; //ordenar con el folio de manera ascendente 
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
                dgResultado.Columns[0].Width = 50; //SERIE
                dgResultado.Columns[1].Width = 50; //FOLIO
                dgResultado.Columns[2].Width = 50; //MUNICIPIO 
                dgResultado.Columns[3].Width = 50; //ZONA 
                dgResultado.Columns[4].Width = 50; //MANZANA
                dgResultado.Columns[5].Width = 50; //LOTE 
                dgResultado.Columns[6].Width = 50; //EDIFICIO
                dgResultado.Columns[7].Width = 50; //DEPTO
                dgResultado.Columns[8].Width = 120; //FECHA 
                if (rbElaboroVenta.Checked == true)
                {
                    dgResultado.Columns[9].Width = 150; //DESCRIPCION 
                    dgResultado.Columns[10].Width = 150; //OBSERVACIONES 
                }
                else
                {
                    dgResultado.Columns[9].Width = 150; //DESCRIPCION 
                    /// dgResultado.Columns[10].Width = 150; //USUARIO CARTOGRAFIA
                }

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
                pnlTabla.Visible = true;
                dgResultado.Enabled = true;
                ///** validamos los botones***//

            }

        }

        private async Task dgResultado_DoubleClickAsync(object sender, EventArgs e)
        {
            //**semaforo**//
            _estadoActual = SemaforoEstado.Rojo; // Estado inicial
            this.Paint += new PaintEventHandler(DibujarSemaforo);
            // btnIniciar.Enabled = false;
            _cts = new CancellationTokenSource();

            try
            {
                // Bucle principal que se ejecuta hasta que se cancele
                while (!_cts.Token.IsCancellationRequested)
                {
                    await _semaforoLogic.WaitAsync(_cts.Token); // Espera asincrónica para entrar al semáforo
                    try
                    {
                        await CambiarEstadoSemaforo(); // Cambia el estado y espera
                    }
                    finally
                    {
                        _semaforoLogic.Release(); // Libera el semáforo
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // La cancelación es esperada, se ignora la excepción
            }
        }

        // Método para detener el semáforo
        private void btnDetener_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            //btnIniciar.Enabled = true;
        }
        // Dibuja el semáforo en la pantalla
        private void DibujarSemaforo(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dibuja la caja del semáforo
            g.FillRectangle(Brushes.Black, 1044, 380, 87, 295);
            pcizquierda.Visible = true;
            //pcizquierda.BackColor = Color.Transparent;
            pcARRIBA.Visible = true;
            pcizquierda.Visible = true;
            pcDerecho.Visible = true;
            //** aqui emepzamos a validar**/ los estatus
            if ((EdoCarto == 1) && (EdoVenta == 1) && (EdoRevision == 1) && (EdoSistemas == 0) && (EdoEliminado == 1))//** validamos si esta eliminado**//
            {
                lblEdoCartografia.Text = string.Empty;
                lblEdoVentanilla.Text = string.Empty;
                lblEdoRevision.Text = string.Empty;
                lblEdoSistemas.Text = string.Empty;
                //** dibujamos el semaforo**//
                g.FillEllipse(_estadoActual == SemaforoEstado.Rojo ? Brushes.Red : Brushes.DarkRed, 1055, 390, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Naranja ? Brushes.Red : Brushes.DarkRed, 1055, 460, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Amarillo ? Brushes.Red : Brushes.DarkRed, 1055, 530, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Verde ? Brushes.Red : Brushes.DarkRed, 1055, 600, 60, 60);
                //** fin de dibujar semaforo **//
                lblEdoCartografia.ForeColor = Color.Red;
                lblEdoCartografia.Text = "O";
                lblEdoVentanilla.ForeColor = Color.Red;
                lblEdoVentanilla.Text = "O";
                lblEdoRevision.ForeColor = Color.Red;
                lblEdoRevision.Text = "O";
                lblEdoSistemas.ForeColor = Color.Red;
                lblEdoSistemas.Text = "O";

            }
            //** cartografia **/
            if ((EdoCarto == 1) && (EdoVenta == 0) && (EdoRevision == 0) && (EdoSistemas == 0) && (EdoEliminado == 0))//** validamos si esta eliminado**//
            {
                lblEdoCartografia.Text = string.Empty;
                lblEdoVentanilla.Text = string.Empty;
                lblEdoRevision.Text = string.Empty;
                lblEdoSistemas.Text = string.Empty;

                g.FillEllipse(_estadoActual == SemaforoEstado.Rojo ? Brushes.DarkOrange : Brushes.DarkOrange, 1060, 390, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Naranja ? Brushes.DarkOrange : Brushes.DarkOrange, 1060, 460, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Amarillo ? Brushes.Yellow : Brushes.DarkOrange, 1060, 530, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Verde ? Brushes.Green : Brushes.DarkGreen, 1060, 600, 60, 60);

                lblEdoCartografia.ForeColor = Color.Green;
                lblEdoCartografia.Text = "P";
                lblEdoVentanilla.ForeColor = Color.Red;
                lblEdoVentanilla.Text = "O";
                lblEdoRevision.ForeColor = Color.Red;
                lblEdoRevision.Text = "O";
                lblEdoSistemas.ForeColor = Color.Red;
                lblEdoSistemas.Text = "O";

            }
            if ((EdoCarto == 1) && (EdoVenta == 1) && (EdoRevision == 0) && (EdoSistemas == 0) && (EdoEliminado == 0))//** validamos si esta eliminado**//
            {
                lblEdoCartografia.Text = string.Empty;
                lblEdoVentanilla.Text = string.Empty;
                lblEdoRevision.Text = string.Empty;
                lblEdoSistemas.Text = string.Empty;

                g.FillEllipse(_estadoActual == SemaforoEstado.Rojo ? Brushes.DarkOrange : Brushes.DarkOrange, 1060, 390, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Naranja ? Brushes.Yellow : Brushes.DarkOrange, 1060, 460, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Amarillo ? Brushes.Green : Brushes.DarkGreen, 1060, 530, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Verde ? Brushes.Green : Brushes.DarkGreen, 1060, 600, 60, 60);

                lblEdoCartografia.ForeColor = Color.Green;
                lblEdoCartografia.Text = "P";
                lblEdoVentanilla.ForeColor = Color.Green;
                lblEdoVentanilla.Text = "P";
                lblEdoRevision.ForeColor = Color.Red;
                lblEdoRevision.Text = "O";
                lblEdoSistemas.ForeColor = Color.Red;
                lblEdoSistemas.Text = "O";
            }
            if ((EdoCarto == 1) && (EdoVenta == 1) && (EdoRevision == 1) && (EdoSistemas == 0) && (EdoEliminado == 0))//** validamos si esta eliminado**//
            {
                lblEdoCartografia.Text = string.Empty;
                lblEdoVentanilla.Text = string.Empty;
                lblEdoRevision.Text = string.Empty;
                lblEdoSistemas.Text = string.Empty;

                g.FillEllipse(_estadoActual == SemaforoEstado.Rojo ? Brushes.Yellow : Brushes.DarkOrange, 1060, 390, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Naranja ? Brushes.Green : Brushes.DarkGreen, 1060, 460, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Amarillo ? Brushes.Green : Brushes.DarkGreen, 1060, 530, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Verde ? Brushes.Green : Brushes.DarkGreen, 1060, 600, 60, 60);

                lblEdoCartografia.ForeColor = Color.Green;
                lblEdoCartografia.Text = "P";
                lblEdoVentanilla.ForeColor = Color.Green;
                lblEdoVentanilla.Text = "P";
                lblEdoRevision.ForeColor = Color.Green;
                lblEdoRevision.Text = "P";
                lblEdoSistemas.ForeColor = Color.Red;
                lblEdoSistemas.Text = "O";
            }
            if ((EdoCarto == 1) && (EdoVenta == 1) && (EdoRevision == 1) && (EdoSistemas == 1) && (EdoEliminado == 0))//** validamos si esta eliminado**//
            {
                lblEdoCartografia.Text = string.Empty;
                lblEdoVentanilla.Text = string.Empty;
                lblEdoRevision.Text = string.Empty;
                lblEdoSistemas.Text = string.Empty;

                g.FillEllipse(_estadoActual == SemaforoEstado.Rojo ? Brushes.Green : Brushes.DarkGreen, 1060, 390, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Naranja ? Brushes.Green : Brushes.DarkGreen, 1060, 460, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Amarillo ? Brushes.Green : Brushes.DarkGreen, 1060, 530, 60, 60);
                g.FillEllipse(_estadoActual == SemaforoEstado.Verde ? Brushes.Green : Brushes.DarkGreen, 1060, 600, 60, 60);

                lblEdoCartografia.ForeColor = Color.Green;
                lblEdoCartografia.Text = "P";
                lblEdoVentanilla.ForeColor = Color.Green;
                lblEdoVentanilla.Text = "P";
                lblEdoRevision.ForeColor = Color.Green;
                lblEdoRevision.Text = "P";
                lblEdoSistemas.ForeColor = Color.Green;
                lblEdoSistemas.Text = "P";
            }
            //// Dibuja las tres luces, la activa se dibuja con color brillante
            //g.FillEllipse(_estadoActual == SemaforoEstado.Rojo ? Brushes.Red : Brushes.DarkRed, 1060, 390, 60, 60);
            //g.FillEllipse(_estadoActual == SemaforoEstado.Naranja ? Brushes.Orange : Brushes.DarkOrange, 1060, 460, 60, 60);
            //g.FillEllipse(_estadoActual == SemaforoEstado.Amarillo ? Brushes.Yellow : Brushes.DarkOliveGreen, 1060, 530, 60, 60);
            //g.FillEllipse(_estadoActual == SemaforoEstado.Verde ? Brushes.Green : Brushes.DarkGreen, 1060, 600, 60, 60);
        }

        // Controla la transición entre estados y los tiempos de espera
        private async Task CambiarEstadoSemaforo()
        {
            switch (_estadoActual)
            {
                case SemaforoEstado.Rojo:
                    _estadoActual = SemaforoEstado.Verde;
                    await Task.Delay(800, _cts.Token); // Espera 5 segundos en verde

                    break;
                case SemaforoEstado.Naranja:
                    _estadoActual = SemaforoEstado.Rojo;
                    await Task.Delay(800, _cts.Token); // Espera 5 segundos en rojo

                    break;

                case SemaforoEstado.Amarillo:
                    _estadoActual = SemaforoEstado.Naranja;
                    await Task.Delay(800, _cts.Token); // Espera 5 segundos en rojo

                    break;
                case SemaforoEstado.Verde:
                    _estadoActual = SemaforoEstado.Amarillo;
                    await Task.Delay(800, _cts.Token); // Espera 2 segundos en amarillo
                    break;

            }
            this.Invalidate(); // Fuerza un redibujado del formulario
        }

        public async Task IniciarConsultaPeriodicaAsync(TimeSpan intervalo, CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    // Ejecutar la consulta
                    EjecutarConsulta();
                    RtxtBoxObservaCarto.Enabled = false;
                    EjecutarConsultaObservaciones(); // Llama a la función para obtener observaciones
                    if (EdoVenta == 1)
                    {
                        //*** ejecutamos la busueda de observaciones de ventanilla**//
                        EjecutarConsultaObservacionesVentanilla();
                        RtxtBoxObservaVentnilla.Enabled = false;
                    }
                    else
                    {
                        RtxtBoxObservaVentnilla.Text = string.Empty;
                        RtxtBoxObservaVentnilla.Enabled = false;
                    }
                    //** revision**//
                    if (EdoRevision == 1)
                    {
                        RtxtBoxObservaRevision.Text = ComentarioObservaRev;
                        RtxtBoxObservaRevision.Enabled = false;
                    }
                    else
                    {
                        RtxtBoxObservaRevision.Text = string.Empty;
                        RtxtBoxObservaRevision.Enabled = false;
                    }
                    if (EdoSistemas == 1)
                    {
                        RtxtBoxObservaSistemas.Text = ComentarioObservaSis;
                        RtxtBoxObservaSistemas.Enabled = false;
                    }
                    else
                    {
                        RtxtBoxObservaSistemas.Text = string.Empty;
                        RtxtBoxObservaSistemas.Enabled = false;
                    }

                    // Esperar el intervalo especificado
                    await Task.Delay(intervalo, cancellationToken);
                }
                catch (TaskCanceledException)
                {
                    // La tarea fue cancelada, salir limpiamente
                    break;
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Error en consulta periódica: {ex.Message}");
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    

                    // Esperar antes de reintentar en caso de error
                    await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
                }
            }
        }
        private void EjecutarConsultaRevision()
        {

        }
        private void EjecutarConsultaObservacionesVentanilla()
        {
            try
            {
                con.conectar_base_interno();
                con.open_c_interno();
                con.cadena_sql_interno = ""; //Limpiamos la cadena de conexión
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT OBSERVACIONES,USUARIO ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM CAT_NEW_VENTANILLA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "     WHERE FOLIO_ORIGEN =" + FolioConsulta;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND SERIE =" + util.scm(SerieConsulta);
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    RtxtBoxObservaVentnilla.Text = con.leer_interno[0].ToString().Trim();
                    RtxtBoxObservaVentnilla.Enabled = true;
                    lblNameVenta.Text = con.leer_interno[1].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}");
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
            }
            finally
            {
                con.leer_interno.Close();
                con.cerrar_interno();
            }
        }
        private void EjecutarConsultaObservaciones()
        {
            try
            {
                con.conectar_base_interno();
                con.open_c_interno();
                con.cadena_sql_interno = ""; //Limpiamos la cadena de conexión
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT OBSERVACIONES,USUARIO ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "     WHERE FOLIO_ORIGEN =" + FolioConsulta;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND SERIE =" + util.scm(SerieConsulta);
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    RtxtBoxObservaCarto.Text = con.leer_interno[0].ToString().Trim();
                    lblNameCarto.Text = con.leer_interno[1].ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}");
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
            }
            finally
            {
                con.leer_interno.Close();
                con.cerrar_interno();
            }
        }
        private void EjecutarConsulta()
        {
            try
            {
                con.conectar_base_interno();
                con.open_c_interno();
                con.cadena_sql_interno = ""; //Limpiamos la cadena de conexión
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT CARTOGRAFIA , VENTANILLA , REVISO , SISTEMAS , ELIMINADO,OBSERVA_SISTEMA,OBSERVA_REVISO ";
                con.cadena_sql_interno = con.cadena_sql_interno + "          ,USU_REVISO, USU_SISTEMAS";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM CAT_DONDE_VA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "     WHERE FOLIO_ORIGEN =" + FolioConsulta;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND SERIE =" + util.scm(SerieConsulta);
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    EdoCarto = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    EdoVenta = Convert.ToInt32(con.leer_interno[1].ToString().Trim());
                    EdoRevision = Convert.ToInt32(con.leer_interno[2].ToString().Trim());
                    EdoSistemas = Convert.ToInt32(con.leer_interno[3].ToString().Trim());
                    EdoEliminado = Convert.ToInt32(con.leer_interno[4].ToString().Trim());
                    ComentarioObservaSis = con.leer_interno[5].ToString().Trim();
                    ComentarioObservaRev = con.leer_interno[6].ToString().Trim();
                    if (EdoRevision == 1) { lblNameReviso.Text = con.leer_interno[7].ToString().Trim(); } else { lblNameReviso.Text = string.Empty; }
                    if (EdoSistemas == 1) { lblNameSsitemas.Text = con.leer_interno[8].ToString().Trim(); } else { lblNameSsitemas.Text = string.Empty; }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                // MessageBox.Show($"Error al ejecutar la consulta: {ex.Message}");
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
            }
            finally
            {
                con.leer_interno.Close();
                con.cerrar_interno();
            }
        }
    }
}
