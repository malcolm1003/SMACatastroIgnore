using AccesoBase;
using System;
using System.Windows.Forms;
using Utilerias;

namespace SMACatastro.catastroRevision
{
    public partial class frmActualizar3en1 : Form
    {
        public int Folio_Catastro { get; set; }
        public string Serie_Catastro { get; set; }
        //clave catastral
        public string Estado { get; set; }
        public int Municipio { get; set; }
        public int Zona { get; set; }
        public int Manzana { get; set; }
        public int Lote { get; set; }
        public string Edifico { get; set; }
        public string Depto { get; set; }
        public string SERIE_TEU { get; private set; }
        public int FOLIO_TEU { get; private set; }
        public int AÑO_TEU { get; private set; }
        public int MES_TEU { get; private set; }
        public int MODIFICO { get; private set; }


        public frmActualizar3en1()
        {
            InitializeComponent();
        }
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();      //conexion 
        Util util = new Util();
        //METODO PARA ARRASTRAR EL FORMULARIO-----------------------------------------------------------------------------------------------
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //METODOS PARA CERRAR,MAXIMIZAR, MINIMIZAR FORMULARIO-------------------------------------------------------------------------------
        int lx, ly;
        int sw, sh;
        private void frmActualizar3en1_Load(object sender, EventArgs e)
        {
            label7.Text = "Usuario: " + Program.nombre_usuario;
            pago_predio_ant();
        }
        private void pago_predio_ant()
        {
            try
            {

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////// OBTENEMOS DATOS DEL FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT seriePago, folioPago, mes_predial, año_predial  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM TRES_EN_UNO_2025 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE Municipio = " + Municipio;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND zona = " + Zona;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND manzana = " + Manzana;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND lote = " + Lote;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND edificio = " + util.scm(Edifico);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND depto = " + util.scm(Depto);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND serie = " + util.scm(Serie_Catastro);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND FOLIO = " + Folio_Catastro;

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {

                        lblSeriePredioA.Text = con.leer_interno[0].ToString().Trim();
                        lblFolioPredioA.Text = con.leer_interno[1].ToString().Trim();
                        lblMesPredioA.Text = con.leer_interno[2].ToString().Trim();
                        lblAñoPredioA.Text = con.leer_interno[3].ToString().Trim();


                    }

                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return;
            }
        }

        private void cmdAplica_Click(object sender, EventArgs e)
        {
            try
            {   // OBTENEMOS LOS NUEVOS DATOS DEL PAGO DE PREDIO

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT TOP 1 P.UltAnioPag, P.UltMesPag, R.Serie, R.Folio  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM RECIBOS R, PROPIEDADES P ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE R.Municipio = " + Municipio;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.zona = " + Zona;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.manzana = " + Manzana;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.lote = " + Lote;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.edificio = " + util.scm(Edifico);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.depto = " + util.scm(Depto);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Status in ('A', 'E')";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Municipio = P.Municipio";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Zona = P.Zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Manzana = P.Manzana";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Lote = P.Lote";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Edificio = P.Edificio";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Depto = P.Depto";
                con.cadena_sql_interno = con.cadena_sql_interno + "   ORDER BY R.nNoRecib DESC";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        lblSeriePredioN.Text = con.leer_interno[2].ToString().Trim();
                        lblFolioPredioN.Text = con.leer_interno[3].ToString().Trim();
                        lblMesPredioN.Text = con.leer_interno[1].ToString().Trim();
                        lblAñoPredioN.Text = con.leer_interno[0].ToString().Trim();
                    }

                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return;
            }
            btnActualizar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if(lblSeriePredioA.Text == "" || lblFolioPredioA.Text == "" || lblMesPredioA.Text == "" || lblAñoPredioA.Text == "")
            {
                lblSeriePredioA.Text = "0";
                lblFolioPredioA.Text = "0";
                lblMesPredioA.Text = "0";
                lblAñoPredioA.Text = "0";
            }
            try
            {
                //REALIZAMOS INSERT EN LA TABLA DE HISTORIAL

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " INSERT INTO SONG_3_EN_1_ACT_P  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "           ( FOLIO, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SERIE, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             MUNICIPIO, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ZONA, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             MANZANA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             LOTE, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             EDIFICIO, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             DEPTO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             mes_predial_old, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             año_predial_old, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             seriePago_old, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             folioPago_old, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             mes_predial_new, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             AÑO_PREDIAL_NEW, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             seriePago_new, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             folioPago_new, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             USUARIO )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VALUES ( ";
                con.cadena_sql_interno = con.cadena_sql_interno + Folio_Catastro + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + util.scm(Serie_Catastro) + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + Municipio + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + Zona + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + Manzana + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + Lote + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + util.scm(Edifico) + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + util.scm(Depto) + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + lblMesPredioA.Text + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + lblAñoPredioA.Text + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + util.scm(lblSeriePredioA.Text) + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + lblFolioPredioA.Text + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + lblMesPredioN.Text + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + lblAñoPredioN.Text + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + util.scm(lblSeriePredioN.Text) + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + lblFolioPredioN.Text + ", ";
                con.cadena_sql_interno = con.cadena_sql_interno + util.scm(Program.nombre_usuario) + ")";

                con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";

                //hacemos update a la tres en uno 2025
                con.cadena_sql_interno = con.cadena_sql_interno + "   UPDATE TRES_EN_UNO_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "      SET SERIEPAGO = " + util.scm(lblSeriePredioN.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "        , FOLIOPAGO = " + lblFolioPredioN.Text;
                con.cadena_sql_interno = con.cadena_sql_interno + "        , MES_PREDIAL = " + lblMesPredioN.Text;
                con.cadena_sql_interno = con.cadena_sql_interno + "        , AÑO_PREDIAL = " + lblAñoPredioN.Text;
                con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE FOLIO = " + Folio_Catastro;
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND SERIE =  " + util.scm(Serie_Catastro);
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND Municipio = " + Municipio;
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND zona = " + Zona;
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND manzana = " + Manzana;
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND lote = " + Lote;
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND edificio = " + util.scm(Edifico);
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND depto = " + util.scm(Depto);

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

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

            // frmTresenUno TRES = new frmTresenUno();
            this.SERIE_TEU = lblSeriePredioN.Text;
            this.FOLIO_TEU = Convert.ToInt32(lblFolioPredioN.Text);
            this.MES_TEU = Convert.ToInt32(lblMesPredioN.Text);
            this.AÑO_TEU = Convert.ToInt32(lblAñoPredioN.Text);
            this.MODIFICO = 1;
            this.Close(); // Cerrar el formulario actual
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.MODIFICO = 0;
            this.Close();
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
        }

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
