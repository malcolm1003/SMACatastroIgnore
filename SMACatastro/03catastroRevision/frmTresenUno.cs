using AccesoBase;
using SMACatastro.formaReporte;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Utilerias;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using TextBox = System.Windows.Forms.TextBox;

namespace SMACatastro.catastroRevision
{
    public partial class frmTresenUno : Form
    {
        public frmTresenUno()
        {
            InitializeComponent();
        }
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();      //conexion 
        Util util = new Util();
        frmRCertificaciones Rcertificado = new frmRCertificaciones();
        frmCertificado3en1 CETTIFICADO3EN1INGRESOS = new frmCertificado3en1();
        private bool focoEstablecido = false;

        //METODO PARA ARRASTRAR EL FORMULARIO-----------------------------------------------------------------------------------------------
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //METODOS PARA CERRAR,MAXIMIZAR, MINIMIZAR FORMULARIO-------------------------------------------------------------------------------
        int lx, ly;
        int sw, sh;
        int MaxValor;
        int FOLIO;
        int tipo_ceti_2 = 0, val_predio, MES_ACTUAL, val_aportacion;
        double CLAVE_3_1;
        string soloFecha;
        int ESTADO_M, MUNICIPIO_M, ZONA_M, MANZANA_M, LOTE_M;
        string EDIFICIO_M, DEPTO_M, SERIE, FOLIO_CERTI;
        string estado2 = string.Empty;
        string MUNICIPIO2 = string.Empty;
        string ZONA2 = string.Empty;
        string MANZANA2 = string.Empty;
        string LOTE2 = string.Empty;
        string EDIFICIO2 = string.Empty;
        string DEPTO2 = string.Empty;
        string DOM_PREDIO = string.Empty;
        string DOM_PROPIEDAD = string.Empty;
        string CP_PREDIO = string.Empty;
        string NUM_INT_PROPIEDADES = string.Empty;
        string NUM_EXT_PREDIO = string.Empty;
        string ClaveCat1 = string.Empty;
        int IMPRIMIO_SI_NO;
        int REVISO;
        private void ACTUALIZAR_CDV()
        {
            //REVISAMOS SI SE ACTUALIZO EN LA TABLA CAT DONDE VA 2025 Y SI NO SE ACTUALIZA
            if (REVISO == 0)
            {
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = " ";

                    con.cadena_sql_interno = con.cadena_sql_interno + "     UPDATE CAT_DONDE_VA_2025";
                    con.cadena_sql_interno = con.cadena_sql_interno + "        SET SISTEMAS = 1";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , FECHA_REV = GETDATE() ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , HORA_REV = GETDATE() ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , USU_REVISO = " + util.scm(Program.acceso_usuario);
                    con.cadena_sql_interno = con.cadena_sql_interno + "      , OBSERVA_SISTEMA = 'IMPRESION DE CERTIFICADOS' "; 
                    con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE SERIE =  " + util.scm(SERIE);
                    con.cadena_sql_interno = con.cadena_sql_interno + "        AND FOLIO_ORIGEN =  " + FOLIO;

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

            }

        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //VALIDAMOS SI ESTAN PAGADAS LAS ORDENES DE PAGO Y EL PREDIO
            //P = PAGADO
            //O= NO PAGADO
            if (lblOrdenPago.Text == "O")
            {
                MessageBox.Show("ERROR, NO SE ENCUENTRA PAGADO EL CERTIFICADO ", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lblPredio.Text == "O")
            {
                MessageBox.Show("ERROR, NO SE ENCUENTRA ACTUALIZADO EL PAGO DEL PREDIO", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (IMPRIMIO_SI_NO == 2)//si es dos ya se imprimio el certificado por segunda ocacion
            {
                MessageBox.Show("EL CERTIFICADO YA FUE IMPRESO EN DOS OCACIONES, NO SE PUEDE VOLVER A IMPRIMIR ", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////imprimimos el certificado/////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            // Declaración de variables
            string folio2 = string.Empty;
            DateTime fecha2;
            double i = 0.0;
            string var = string.Empty;
            int valor = 0;
            int A = 0;
            int B = 0;
            double tp = 0.0;
            double tc = 0.0;
            double cp = 0.0;
            double cc = 0.0;
            double valor_terreno = 0.0;
            double valor_terreno_comun = 0.0;
            double valor_construccion = 0.0;
            double valor_comun = 0.0;
            int año_a = 0;
            string estado_c = string.Empty;
            string municipio_c = string.Empty;
            string zona_c = string.Empty;
            string manzana_c = string.Empty;
            string lote_c = string.Empty;
            string edificio_c = string.Empty;
            string depto_c = string.Empty;
            object fecha_entrega = null;
            double FOLIO_3_1 = 0.0;
            string folio2_3_1 = string.Empty;
            string folio3_3_1 = string.Empty;
            string DGH_CONCA = string.Empty;
            int reviso2 = 0;
            int FOLIOS_DONDE = 0;
            int ayuda_memo = 0;
            double valor_clave_catastral = 0.0;
            double valor_terreno_total = 0.0;
            double valor_construccion_total = 0.0;
            string FECHA;
            // FECHA = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            // Variables para verificar si está bloqueada
            int bloqueo_si_o_no = 0;
            string NOMBRE_IMPORTANTE = "";
            int verificar = 0;
            estado_c = "15";
            municipio_c = MUNICIPIO_M.ToString().PadLeft(3, '0');
            zona_c = ZONA_M.ToString().PadLeft(2, '0');
            manzana_c = MANZANA_M.ToString().PadLeft(3, '0');
            lote_c = LOTE_M.ToString().PadLeft(2, '0');
            edificio_c = EDIFICIO_M.PadLeft(4, '0');
            depto_c = DEPTO_M.PadLeft(4, '0');
            int id;
            id = IMPRIMIO_SI_NO + 1;

            // Obtener la fecha actual en formato "YYYYMMDD"
            FECHA = DateTime.Now.ToString("yyyyMMdd");
            estado2 = "15";
            MUNICIPIO2 = MUNICIPIO2.PadLeft(3, '0');
            ZONA2 = ZONA2.PadLeft(2, '0');
            MANZANA2 = MANZANA2.PadLeft(3, '0');
            LOTE2 = LOTE2.PadLeft(2, '0');
            EDIFICIO2 = EDIFICIO2.PadLeft(2, '0');
            DEPTO2 = DEPTO2.PadLeft(4, '0');
            ///
            DateTime fechaActual = DateTime.Now;
            string dia = fechaActual.ToString("dd");
            string mes = fechaActual.ToString("MMMM", new CultureInfo("es-ES")); // Mes en español
            string año = fechaActual.ToString("yyyy");
            // Convertir los valores de los TextBox a double (si es necesario)
            tp = Convert.ToDouble(lblSupTerrPriv.Text);
            tc = Convert.ToDouble(lblSupTerrComun.Text);
            cp = Convert.ToDouble(lblConstruccionPrivada.Text);
            cc = Convert.ToDouble(lblConstruccionComun.Text);
            valor_terreno = Convert.ToDouble(lblTerrenoPrivadoV.Text);
            valor_terreno_comun = Convert.ToDouble(lblTerrenoComunV.Text);
            valor_construccion = Convert.ToDouble(lblValorConsPriv.Text);
            valor_comun = Convert.ToDouble(lblConsComunV.Text);
            valor_terreno_total = Convert.ToDouble(lblValTotCons.Text);
            valor_construccion_total = Convert.ToDouble(lblValTotCons.Text);
            valor_clave_catastral = Convert.ToDouble(lblValor.Text.Trim());
            // Asignar un valor fijo
            año_a = Program.añoActual;

            //        '1  nada ninguno
            //        '2  DESARROLLO
            //        '3  PREDIO
            //        '4  PREDIO Y DESARROLLO
            //        '5  CATASTRO
            //        '6  CATASTRO Y DESARROLLO
            //        '7  CATASTRO Y PREDIO
            //        '8  CATASTRO Y DESARROLLO Y PREDIO

            //// Usar PadLeft para agregar ceros a la izquierda
            folio3_3_1 = FOLIO_CERTI.PadLeft(7, '0');
            switch (tipo_ceti_2)
            {
                case 2: //Certificacion de desarrollo (APORTACION A MEJORAS)
                    try
                    {
                        //SE INSERTA EN LA TABLA TRES_EN_UNO_1 ( HISTORIAL DE IMPRESIONES DE CERTIFICADOS)
                        con.conectar_base_interno();
                        con.cadena_sql_interno = " ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO TRES_EN_UNO_1";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    FECHAALTA,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    NombreContri, DomicilioFis, DescripcionColonia,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado2, Municipio2, Zona2, Manzana2, Lote2, Edificio2, Depto2,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Tp,Tc, Cp, Cc,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ValorTerreno, ValorTerreno_comun, ValorConstruccion, ValorComun, valor_clave_catastral,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    año_predial, mes_predial,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    serie_factura_predio, folio_fatura_predio,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertValor, folioCertValor,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertAportacion, folioCertAportacion,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertPredio , folioCertPredio";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(FECHA) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ," + MUNICIPIO_M + " ," + ZONA_M + " ," + MANZANA_M + " ," + LOTE_M + " ," + util.scm(EDIFICIO_M) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(DEPTO_M) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblTitular.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblCalle.Text.Trim()) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblColonia.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ," + MUNICIPIO2 + " ," + ZONA2 + " ," + MANZANA2 + " ," + LOTE2 + " ," + util.scm(EDIFICIO2) + " ," + util.scm(DEPTO2) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + tp + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + tc + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + cp + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + cc + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno_comun + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_construccion + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_comun + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_clave_catastral + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + lblAñoPredio.Text.Trim() + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + lblMesPredio.Text.Trim() + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSeriePredio.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioPredio.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ,"; // Serie Certificado valor
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ,";// folio Certificado valor
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSerieCertificado.Text.Trim()) + " ,";// sere certificado aportacion
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioCertificado.Text.Trim()) + " ,";// folio Certificado aportacion
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ,";// sere certificado predio
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ";// folio Certificado predio
                        con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";
                        //hacemos update a la tabla TRES EN 1 ( SI ID ES 1 YA SE IMPRIMIO EL CERTIFICADO)
                        con.cadena_sql_interno = con.cadena_sql_interno + "   UPDATE TRES_EN_UNO_2025";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET ID = " + id;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE FOLIO = " + FOLIO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND SERIE =  " + util.scm(SERIE);
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND estado = 15";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MUNICIPIO = " + MUNICIPIO_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND ZONA = " + ZONA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MANZANA = " + MANZANA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND LOTE = " + LOTE_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO = " + util.scm(EDIFICIO_M);
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO = " + util.scm(DEPTO_M);


                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        con.cerrar_interno();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error al ejecutar la consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }
                    //OBTENEMOS EÑ NOMBRE DEL TITULAR QUE FIRMA EL CERTIFICADO
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + " SELECT NOMBRE ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   From PERSONAS_IMPORTANTES WHERE ID = 2 ";

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            NOMBRE_IMPORTANTE = con.leer_interno[0].ToString();

                        }
                        con.cerrar_interno();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error al executar la consulta de personas importantes" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        // Retornar false si ocurre un error
                    }
                    //CREAMOS EL CERTIFICADO DE APORTACION A MEJORAS
                    ACTUALIZAR_CDV();
                    Rcertificado.FOLIO_CER = folio3_3_1;
                    Rcertificado.CLAVE_CAT = municipio_c + "-" + zona_c + "-" + manzana_c + "-" + lote_c + "-" + edificio_c + "-" + depto_c;
                    Rcertificado.CLAVE_ANT = MUNICIPIO2 + "-" + ZONA2 + "-" + MANZANA2 + "-" + LOTE2 + "-" + EDIFICIO2 + "-" + DEPTO2;
                    Rcertificado.NOMBRE = lblTitular.Text.Trim();
                    Rcertificado.DIRECCION = lblCalle.Text.Trim();
                    Rcertificado.DIRECCION_2 = lblColonia.Text.Trim() + ", SAN MATEO ATENCO; ESTADO DE MEXICO.";
                    Rcertificado.SUP_TERRENO_PRIV = Convert.ToString(tp).Trim() + " m²";
                    Rcertificado.SUP_TERRENO_COM = Convert.ToString(tc).Trim() + " m²";
                    Rcertificado.VAL_TERRENO = (valor_terreno + valor_terreno_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.SUP_CONST_PRIV = Convert.ToString(cp).Trim() + " m²";
                    Rcertificado.SUP_CONST_COM = Convert.ToString(cc).Trim() + " m²";
                    Rcertificado.VAL_CONSTRUCCION = (valor_construccion + valor_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.VAL_CATASTRAL = (valor_terreno + valor_terreno_comun + valor_construccion + valor_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.FOLIO2 = lblSerieCertificado.Text + " - " + lblFolioCertificado.Text;
                    Rcertificado.PERSONA_FIRMA = NOMBRE_IMPORTANTE;

                    // Rcertificado.CERTIFICACION_01 = "\"C E R T I F I C A C I O N   D |E  C L A V E   Y   V A L O R   C A T A S T R A L\" ";
                    Rcertificado.CERTIFICACION_01 = " ";
                    //Rcertificado.CERTIFICACION_02 = "\"C E R T I F I C A D O    D E    N O    A D E U D O    P R E D I A L\"";
                    Rcertificado.CERTIFICACION_02 = " ";
                    Rcertificado.CERTIFICACION_03 = "\"C E R T I F I C A D O    D E    A P O R T A C I O N    D E    M E J O R A S\"";
                    //Rcertificado.CERTIFICACION_03 = " ";

                    Rcertificado.FECHA_DIA = dia;
                    Rcertificado.FECHA_MES = mes;
                    Rcertificado.FECHA_A = año;

                    Rcertificado.ShowDialog();

                    break;

                case 5: //5 CERTIFICADO CLAVE Y VALOR CATASTRAL
                    Program.tipoReporte = 5; // Asignar el tipo de reporte para el certificado de clave y valor catastral
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = " ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO TRES_EN_UNO_1";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    FECHAALTA,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    NombreContri, DomicilioFis, DescripcionColonia,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado2, Municipio2, Zona2, Manzana2, Lote2, Edificio2, Depto2,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Tp,Tc, Cp, Cc,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ValorTerreno, ValorTerreno_comun, ValorConstruccion, ValorComun, valor_clave_catastral,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    año_predial, mes_predial,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    serie_factura_predio, folio_fatura_predio,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertValor, folioCertValor,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertAportacion, folioCertAportacion,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertPredio , folioCertPredio";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(FECHA) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ," + MUNICIPIO_M + " ," + ZONA_M + " ," + MANZANA_M + " ," + LOTE_M + " ," + util.scm(EDIFICIO_M) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(DEPTO_M) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblTitular.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblCalle.Text.Trim()) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblColonia.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ," + MUNICIPIO2 + " ," + ZONA2 + " ," + MANZANA2 + " ," + LOTE2 + " ," + util.scm(EDIFICIO2) + " ," + util.scm(DEPTO2) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + tp + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + tc + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + cp + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + cc + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno_comun + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_construccion + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_comun + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_clave_catastral + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + lblAñoPredio.Text.Trim() + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + lblMesPredio.Text.Trim() + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSeriePredio.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioPredio.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSerieCertificado.Text.Trim()) + " ,"; // Serie Certificado calve y valor
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioCertificado.Text.Trim()) + " ,";// folio Certificado calve y valor
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ,";// sere certificado aportacion
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ,";// folio Certificado aportacion
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ,";// sere certificado predio
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ";// folio Certificado predio
                        con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";
                        //hacemos update a la tabla TRES EN 1(SI ID ES 1 YA SE IMPRIMIO EL CERTIFICADO)
                        con.cadena_sql_interno = con.cadena_sql_interno + "   UPDATE TRES_EN_UNO_2025";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET ID = " + id;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE FOLIO = " + FOLIO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND SERIE =  " + util.scm(SERIE);
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND estado = 15";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MUNICIPIO = " + MUNICIPIO_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND ZONA = " + ZONA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MANZANA = " + MANZANA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND LOTE = " + LOTE_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO = " + util.scm(EDIFICIO_M);
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO = " + util.scm(DEPTO_M);


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
                    //try aquí falló 
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";

                        con.cadena_sql_interno = " SELECT NOMBRE ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  From PERSONAS_IMPORTANTES WHERE ID = 1 ";

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            NOMBRE_IMPORTANTE = con.leer_interno[0].ToString();

                        }
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
                  

                    ACTUALIZAR_CDV();
                    //CREAMOS EL CERTIFICADO DE CLAVE Y VALOR CATASTRAL
                    Rcertificado.FOLIO_CER = folio3_3_1;
                    Rcertificado.CLAVE_CAT = municipio_c + "-" + zona_c + "-" + manzana_c + "-" + lote_c + "-" + edificio_c + "-" + depto_c;
                    Rcertificado.CLAVE_ANT = MUNICIPIO2 + "-" + ZONA2 + "-" + MANZANA2 + "-" + LOTE2 + "-" + EDIFICIO2 + "-" + DEPTO2;
                    Rcertificado.NOMBRE = lblTitular.Text.Trim();
                    Rcertificado.DIRECCION = lblCalle.Text.Trim();
                    Rcertificado.DIRECCION_2 = lblColonia.Text.Trim() + ", SAN MATEO ATENCO; ESTADO DE MEXICO.";
                    Rcertificado.SUP_TERRENO_PRIV = Convert.ToString(tp).Trim() + " m²";
                    Rcertificado.SUP_TERRENO_COM = Convert.ToString(tc).Trim() + " m²";
                    Rcertificado.VAL_TERRENO = (valor_terreno + valor_terreno_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.SUP_CONST_PRIV = Convert.ToString(cp).Trim() + " m²";
                    Rcertificado.SUP_CONST_COM = Convert.ToString(cc).Trim() + " m²";
                    Rcertificado.VAL_CONSTRUCCION = (valor_construccion + valor_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.VAL_CATASTRAL = (valor_terreno + valor_terreno_comun + valor_construccion + valor_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.FOLIO2 = lblSerieCertificado.Text + " - " + lblFolioCertificado.Text;
                    Rcertificado.PERSONA_FIRMA = NOMBRE_IMPORTANTE;

                    Rcertificado.CERTIFICACION_01 = "\"C E R T I F I C A C I O N   D E  C L A V E   Y   V A L O R   C A T A S T R A L\" ";
                    //Rcertificado.CERTIFICACION_01 = " ";
                    //Rcertificado.CERTIFICACION_02 = "\"C E R T I F I C A D O    D E    N O    A D E U D O    P R E D I A L\"";
                    Rcertificado.CERTIFICACION_02 = " ";
                    //Rcertificado.CERTIFICACION_03 = "\"C E R T I F I C A D O    D E    A P O R T A C I O N    D E    M E J O R A S\"";
                    Rcertificado.CERTIFICACION_03 = " ";

                    Rcertificado.FECHA_DIA = dia;
                    Rcertificado.FECHA_MES = mes;
                    Rcertificado.FECHA_A = año;

                    Rcertificado.ShowDialog();

                    Program.tipoReporte = 0; // Resetear el tipo de reporte después de imprimir
                    break;

                case 6: //6 CERTIFICADO CLAVE Y VALOR CATASTRAL / APORTACION A MEJORAS


                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = " ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO TRES_EN_UNO_1";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    FECHAALTA,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    NombreContri, DomicilioFis, DescripcionColonia,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado2, Municipio2, Zona2, Manzana2, Lote2, Edificio2, Depto2,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Tp,Tc, Cp, Cc,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ValorTerreno, ValorTerreno_comun, ValorConstruccion, ValorComun, valor_clave_catastral,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    año_predial, mes_predial,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    serie_factura_predio, folio_fatura_predio,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertValor, folioCertValor,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertAportacion, folioCertAportacion,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertPredio , folioCertPredio";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(FECHA) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ," + MUNICIPIO_M + " ," + ZONA_M + " ," + MANZANA_M + " ," + LOTE_M + " ," + util.scm(EDIFICIO_M) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(DEPTO_M) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblTitular.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblCalle.Text.Trim()) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblColonia.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ," + MUNICIPIO2 + " ," + ZONA2 + " ," + MANZANA2 + " ," + LOTE2 + " ," + util.scm(EDIFICIO2) + " ," + util.scm(DEPTO2) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + tp + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + tc + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + cp + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + cc + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno_comun + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_construccion + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_comun + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_clave_catastral + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + lblAñoPredio.Text.Trim() + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + lblMesPredio.Text.Trim() + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSeriePredio.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioPredio.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSerieCertificado.Text.Trim()) + " ,"; // Serie Certificado calve y valor
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioCertificado.Text.Trim()) + " ,";// folio Certificado calve y valor
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSerieCertificado.Text.Trim()) + " ,";// sere certificado aportacion
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioCertificado.Text.Trim()) + " ,";// folio Certificado aportacion
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ,";// sere certificado predio
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm("-") + " ";// folio Certificado predio
                        con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";
                        //hacemos update a la tabla TRES EN 1 ( SI ID ES 1 YA SE IMPRIMIO EL CERTIFICADO)
                        con.cadena_sql_interno = con.cadena_sql_interno + "   UPDATE TRES_EN_UNO_2025";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET ID =" + id;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE FOLIO = " + FOLIO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND SERIE =  " + util.scm(SERIE);
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND estado = 15";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MUNICIPIO = " + MUNICIPIO_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND ZONA = " + ZONA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MANZANA = " + MANZANA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND LOTE = " + LOTE_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO = " + util.scm(EDIFICIO_M);
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO = " + util.scm(DEPTO_M);


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

                    //try 
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + " SELECT NOMBRE ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   From PERSONAS_IMPORTANTES WHERE ID = 2 ";

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            NOMBRE_IMPORTANTE = con.leer_interno[0].ToString();

                        }
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

                    ACTUALIZAR_CDV();

                    Rcertificado.FOLIO_CER = folio3_3_1;
                    Rcertificado.CLAVE_CAT = municipio_c + "-" + zona_c + "-" + manzana_c + "-" + lote_c + "-" + edificio_c + "-" + depto_c;
                    Rcertificado.CLAVE_ANT = MUNICIPIO2 + "-" + ZONA2 + "-" + MANZANA2 + "-" + LOTE2 + "-" + EDIFICIO2 + "-" + DEPTO2;
                    Rcertificado.NOMBRE = lblTitular.Text.Trim();
                    Rcertificado.DIRECCION = lblCalle.Text.Trim();
                    Rcertificado.DIRECCION_2 = lblColonia.Text.Trim() + ", SAN MATEO ATENCO; ESTADO DE MEXICO.";
                    Rcertificado.SUP_TERRENO_PRIV = Convert.ToString(tp).Trim() + " m²";
                    Rcertificado.SUP_TERRENO_COM = Convert.ToString(tc).Trim() + " m²";
                    Rcertificado.VAL_TERRENO = (valor_terreno + valor_terreno_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.SUP_CONST_PRIV = Convert.ToString(cp).Trim() + " m²";
                    Rcertificado.SUP_CONST_COM = Convert.ToString(cc).Trim() + " m²";
                    Rcertificado.VAL_CONSTRUCCION = (valor_construccion + valor_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.VAL_CATASTRAL = (valor_terreno + valor_terreno_comun + valor_construccion + valor_comun).ToString("###,###,###,###,###,##0.00");
                    Rcertificado.FOLIO2 = lblSerieCertificado.Text + " - " + lblFolioCertificado.Text;
                    Rcertificado.PERSONA_FIRMA = NOMBRE_IMPORTANTE;

                    Rcertificado.CERTIFICACION_01 = "\"C E R T I F I C A C I O N   D E  C L A V E   Y   V A L O R   C A T A S T R A L\" ";
                    //Rcertificado.CERTIFICACION_01 = " ";
                    //Rcertificado.CERTIFICACION_02 = "\"C E R T I F I C A D O    D E    N O    A D E U D O    P R E D I A L\"";
                    Rcertificado.CERTIFICACION_02 = " ";
                    Rcertificado.CERTIFICACION_03 = "\"C E R T I F I C A D O    D E    A P O R T A C I O N    D E    M E J O R A S\"";
                    //Rcertificado.CERTIFICACION_03 = " ";

                    Rcertificado.FECHA_DIA = dia;
                    Rcertificado.FECHA_MES = mes;
                    Rcertificado.FECHA_A = año;

                    Rcertificado.ShowDialog();

                    break;

                case 8: //8 CERTIFICADO CLAVE Y VALOR CATASTRAL / APORTACION A MEJORAS / NO ADEUDO PREDIAL
                    string fecha_cob_v = string.Empty;
                    string fecha_cob_c = string.Empty;

                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = " ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO TRES_EN_UNO_1";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    FECHAALTA,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    NombreContri, DomicilioFis, DescripcionColonia,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado2, Municipio2, Zona2, Manzana2, Lote2, Edificio2, Depto2,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Tp,Tc, Cp, Cc,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ValorTerreno, ValorTerreno_comun, ValorConstruccion, ValorComun, valor_clave_catastral,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    año_predial, mes_predial,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    serie_factura_predio, folio_fatura_predio,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertValor, folioCertValor,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertAportacion, folioCertAportacion,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    SerieCertPredio , folioCertPredio";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(FECHA) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ," + MUNICIPIO_M + " ," + ZONA_M + " ," + MANZANA_M + " ," + LOTE_M + " ," + util.scm(EDIFICIO_M) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(DEPTO_M) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblTitular.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblCalle.Text.Trim()) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblColonia.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ," + MUNICIPIO2 + " ," + ZONA2 + " ," + MANZANA2 + " ," + LOTE2 + " ," + util.scm(EDIFICIO2) + " ," + util.scm(DEPTO2) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + tp + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + tc + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + cp + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + cc + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_terreno_comun + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_construccion + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_comun + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + valor_clave_catastral + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + lblAñoPredio.Text.Trim() + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + lblMesPredio.Text.Trim() + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSeriePredio.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioPredio.Text.Trim()) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSerieCertificado.Text.Trim()) + " ,"; // Serie Certificado calve y valor
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioCertificado.Text.Trim()) + " ,";// folio Certificado calve y valor
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSerieCertificado.Text.Trim()) + " ,";// sere certificado aportacion
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioCertificado.Text.Trim()) + " ,";// folio Certificado aportacion
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblSerieCertificado.Text.Trim()) + " ,";// sere certificado predio
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblFolioCertificado.Text.Trim()) + " ";// folio Certificado predio
                        con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";
                        //hacemos update a la tabla TRES EN 1 ( SI ID ES 1 YA SE IMPRIMIO EL CERTIFICADO)
                        con.cadena_sql_interno = con.cadena_sql_interno + "   UPDATE TRES_EN_UNO_2025";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET ID = " + id;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE FOLIO = " + FOLIO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND SERIE =  " + util.scm(SERIE);
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND estado = 15";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MUNICIPIO = " + MUNICIPIO_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND ZONA = " + ZONA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MANZANA = " + MANZANA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND LOTE = " + LOTE_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO = " + util.scm(EDIFICIO_M);
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO = " + util.scm(DEPTO_M);

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

                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";

                    con.cadena_sql_interno = " SELECT NOMBRE ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  From PERSONAS_IMPORTANTES WHERE ID = 2 ";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        NOMBRE_IMPORTANTE = con.leer_interno[0].ToString();

                    }
                    con.cerrar_interno();

                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "SELECT FECCOB";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  FROM RECIBOS";
                        con.cadena_sql_interno = con.cadena_sql_interno + " WHERE SERIE = " + util.scm(lblSeriePredio.Text.Trim());
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND FOLIO = " + util.scm(lblFolioPredio.Text.Trim());
                        con.open_c_interno();
                        con.cadena_sql_cmd_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();


                        while (con.leer_interno.Read())
                        {
                            fecha_cob_v = con.leer_interno[0].ToString().Trim();
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
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "SELECT FECCOB";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  FROM RECIBOS";
                        con.cadena_sql_interno = con.cadena_sql_interno + " WHERE SERIE = " + util.scm(lblSerieCertificado.Text.Trim());
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND FOLIO = " + util.scm(lblFolioCertificado.Text.Trim());

                        con.open_c_interno();
                        con.cadena_sql_cmd_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {

                            fecha_cob_c = con.leer_interno[0].ToString().Trim();
                            soloFecha = fecha_cob_c.Substring(0, 10); // Toma los primeros 10 caracteres

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
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "SELECT FOLIO = MAX(FOLIOCERTI)";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  FROM TRES_EN_UNO_1";

                        con.open_c_interno();
                        con.cadena_sql_cmd_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            MaxValor = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
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

                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "            SELECT P.Domicilio, P.NumExt, P.CodPost, PP.DomFis, PP.NumIntP  ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS P, PROPIEDADES PP";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE PP.estado = 15";
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND PP.Municipio = " + MUNICIPIO_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND PP.Zona = " + ZONA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND PP.Manzana = " + MANZANA_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND PP.Lote = " + LOTE_M;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND PP.Edificio = " + util.scm(EDIFICIO_M);
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND PP.Depto = " + util.scm(DEPTO_M);
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND P.Municipio = PP.Municipio ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND P.Zona = PP.Zona ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND P.Manzana = PP.Manzana ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND P.Lote = PP.Lote ";

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        if (!con.leer_interno.HasRows)
                        {
                            MessageBox.Show("CLAVE CATASTRAL NO ENCONTRADA", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Retornar si no hay resultados
                        }

                        while (con.leer_interno.Read())
                        {
                            if (con.leer_interno[0].ToString().Trim() != "")
                            {
                                DOM_PREDIO = con.leer_interno[0].ToString().Trim();
                                NUM_EXT_PREDIO = con.leer_interno[1].ToString().Trim();
                                CP_PREDIO = con.leer_interno[2].ToString().Trim();
                                DOM_PROPIEDAD = con.leer_interno[3].ToString().Trim();
                                NUM_INT_PROPIEDADES = con.leer_interno[4].ToString().Trim();
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

                    ACTUALIZAR_CDV();

                    CETTIFICADO3EN1INGRESOS.FOLIO_3EN1 = MaxValor;
                    CETTIFICADO3EN1INGRESOS.FOLIO_CERTIFICADO = folio3_3_1;
                    CETTIFICADO3EN1INGRESOS.nombre_contri = lblTitular.Text.Trim();
                    CETTIFICADO3EN1INGRESOS.calle = lblCalle.Text.Trim();
                    CETTIFICADO3EN1INGRESOS.manzana = MANZANA_M.ToString();
                    CETTIFICADO3EN1INGRESOS.lote = LOTE_M.ToString();
                    CETTIFICADO3EN1INGRESOS.num_ext = NUM_EXT_PREDIO.Trim();
                    CETTIFICADO3EN1INGRESOS.num_int = NUM_INT_PROPIEDADES.Trim();
                    CETTIFICADO3EN1INGRESOS.colonia = lblColonia.Text.Trim();
                    if (EDIFICIO_M == "00")
                    {
                        CETTIFICADO3EN1INGRESOS.domicilio = DOM_PREDIO.Trim();
                    }
                    else
                    {
                        CETTIFICADO3EN1INGRESOS.domicilio = DOM_PROPIEDAD.Trim();
                    }
                    CETTIFICADO3EN1INGRESOS.CP = CP_PREDIO.Trim();
                    ClaveCat1 = lblMun.Text.Trim() + " - " + ZONA_M.ToString().PadLeft(2, '0') + " - "+ MANZANA_M.ToString().PadLeft(3, '0') + " - " + LOTE_M.ToString().PadLeft(2, '0') + " - " + EDIFICIO_M.ToString().PadLeft(2, '0') + " - " + DEPTO_M.ToString().PadLeft(4, '0');
                    CETTIFICADO3EN1INGRESOS.clave_catastral = ClaveCat1;
                    CETTIFICADO3EN1INGRESOS.año_act = año;
                    CETTIFICADO3EN1INGRESOS.fecha_factura = soloFecha;
                    CETTIFICADO3EN1INGRESOS.tp = tp.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.tc = tc.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.cp1 = cp.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.cc = cc.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.vtp = valor_terreno.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.vtc = valor_terreno_comun.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.vcp = valor_construccion.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.vcc = valor_comun.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.vttp = valor_terreno_total.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.vttc = valor_construccion_total.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.vc = valor_clave_catastral.ToString("###,###,###,###,###,##0.00");
                    CETTIFICADO3EN1INGRESOS.serie_orden = lblSerieCertificado.Text.Trim();
                    CETTIFICADO3EN1INGRESOS.folio_orden = lblFolioCertificado.Text.Trim();
                    CETTIFICADO3EN1INGRESOS.ShowDialog();
                    break;
            }
            limpiar_inicio();
        }

        private void cmdCancela_Click(object sender, EventArgs e)
        {
            limpiar_inicio();

        }
        private void limpiar_inicio()
        {
            limpiarCampos();
            HABILITAR();
            limpiarDataGridView();
            quitarRbo();
            btnActualizar.Enabled = false;
            PNLFBUSCAR.Enabled = false;
            btnBuscar.Enabled = true;
            MUNICIPIO_M = 0;
            ZONA_M = 0;
            MANZANA_M = 0;
            LOTE_M = 0;
            EDIFICIO_M = "";
            DEPTO_M = "";
            btnImprimir.Enabled = false;
            REVISO = 0;
            txtZona.Focus();

        }

        private void btnConsulta_bus_Click(object sender, EventArgs e)
        {
            if (Cbo_Certificado.Text == "")
            {
                if (txtpersona.Text == "")
                {
                    if (txtDomicilio.Text == "")
                    {
                        if (txtDomicilio.Text == "")
                        {
                            if (txtZonab.Text == "")
                            {
                                if (cboAño.Text == "")
                                {
                                    if (cboMes.Text == "")
                                    {
                                        if (cboDia.Text == "")
                                        {
                                            if (cboañoF.Text == "")
                                            {
                                                if (cbomesf.Text == "")
                                                {
                                                    if (cbodiaf.Text == "")
                                                    {
                                                        MessageBox.Show("NO SE TIENE OPCIONES DE BUSQUEDA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            DGVRESULTADO.Enabled = true;
            //ORDENAR LAS BUSQUEDAS EN EL MISMO ORDEN QUE APAREZCA 
            //AREA EMISORA
            if (rbCertificado.Checked == true) { if (Cbo_Certificado.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR EL CERTIFICADO CORRECTO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); Cbo_Certificado.Focus(); return; } }
            //CIUDADANO 
            if (rbIdenticiudadano.Checked == true) { if (txtpersona.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DEL CIUDADANO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtpersona.Focus(); return; } }
            if (rbSimilarciudadano.Checked == true) { if (txtpersona.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DEL CIUDADANO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtpersona.Focus(); return; } }

            //DOMICILIOS
            if (rbIdentiDomicilio.Checked == true) { if (txtDomicilio.Text.Trim() == "") { MessageBox.Show("FAVOR DE COLOCAR UN RFC", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDomicilio.Focus(); return; } }
            if (rbSimiliarDomicilio.Checked == true) { if (txtDomicilio.Text.Trim() == "") { MessageBox.Show("FAVOR DE COLOCAR UN RFC", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDomicilio.Focus(); return; } }

            //clave catastral
            if (rbCveCatastral.Checked == true) { if (txtZonab.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR LA ZONA", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtZonab.Focus(); return; } }

            //FECHAS INICIIO 
            if (rbFecha.Checked == true) { if (cboAño.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR AÑO DE INICIO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); cboAño.Focus(); return; } }
            if (rbFecha.Checked == true) { if (cboMes.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR MES DE INICIO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); cboMes.Focus(); return; } }
            if (rbFecha.Checked == true) { if (cboDia.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR DÍA DE INICIO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); cboDia.Focus(); return; } }
            //FECHAS FINALES
            if (rbFecharango.Checked == true) { if (cboañoF.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR AÑO DE FINAL DE RANGO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); cboañoF.Focus(); return; } }
            if (rbFecharango.Checked == true) { if (cbomesf.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR UN MES PARA EL FINAL DEL RANGO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); cbomesf.Focus(); return; } }
            if (rbFecharango.Checked == true) { if (cbodiaf.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR UN AÑO PARA EL FINAL DEL RANGO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); cbodiaf.Focus(); return; } }

            //aquí debe ir el try y catch 
            // SE ARMA EL query DE BUSQUEDA CONSULTA SQL 
            try
            {
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT   tu.SERIE, tu.FOLIO, tu.clave_catastral, RTRIM(tu.nombre_contri), RTRIM(tu.domicilio_fis),  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "           tu.valor_clave_catastral, sc.Certificado ,RTRIM(tu.observaciones), tu.municipio, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "           tu.zona, tu.manzana,tu.lote, tu.edificio,tu.depto ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM   TRES_EN_UNO_2025 tu, song_certificaciones sc";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE  tu.tipo_certificacion = sc.id_certificado";

                // tipo de certificado
                if (rbCertificado.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.tipo_certificacion = " + Cbo_Certificado.Text.Trim().Substring(0, 2); }
                // ciudadano
                if (rbIdenticiudadano.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.nombre_contri = " + util.scm(txtpersona.Text.Trim()); }
                if (rbSimilarciudadano.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.nombre_contri LIKE '%" + txtpersona.Text.Trim() + "%'"; }
                //domicilio
                if (rbIdentiDomicilio.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.domicilio_fis = " + util.scm(txtDomicilio.Text.Trim()); }
                if (rbSimiliarDomicilio.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.domicilio_fis LIKE '%" + txtDomicilio.Text.Trim() + "%'"; }
                //clave catastral
                if (rbCveCatastral.Checked == true)
                {
                    if (txtZonab.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND r.zona = " + txtZonab.Text.Trim();
                    }
                    if (txtMznab.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND r.manzana = " + txtMznab.Text.Trim();
                    }
                    if (txtLoteb.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND r.lote = " + txtLoteb.Text.Trim();
                    }
                    if (txtEdificiob.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND r.edificio = " + util.scm(txtEdificiob.Text.Trim());
                    }
                    if (txtEdificiob.Text.Trim() != "")
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND r.depto = " + util.scm(txtDeptob.Text.Trim());
                    }
                }
                //fechas
                if (rbFecha.Checked == true)
                {
                    if (rbFecharango.Checked == true)
                    {
                        DateTime F1 = Convert.ToDateTime(cboAño.Text + "-" + cboMes.Text + "-" + cboDia.Text);
                        DateTime F2 = Convert.ToDateTime(cboañoF.Text + "-" + cbomesf.Text + "-" + cbodiaf.Text);

                        if (F1 > F2)
                        {
                            MessageBox.Show("FECHA FINAL NO PUEDE SER MENOR QUE LA FECHA INICIAL", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                        }
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.Fecha_alta >= '" + cboAño.Text.Trim() + cboMes.Text.Substring(0, 2) + cboDia.Text.Trim() + " 00:00:00'";
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.Fecha_alta <= '" + cboañoF.Text.Trim() + cbomesf.Text.Substring(0, 2) + cbodiaf.Text.Trim() + " 23:59:59'";
                    }
                    else
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.Fecha_alta  >= '" + cboAño.Text.Trim() + cboMes.Text.Substring(0, 2) + cboDia.Text.Trim() + " 00:00:00'";
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND tu.Fecha_alta  <= '" + cboAño.Text.Trim() + cboMes.Text.Substring(0, 2) + cboDia.Text.Trim() + " 23:59:59'";
                    }
                }
                con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY tu.SERIE, tu.FOLIO DESC";

                DataTable LLENAR_GRID_1 = new DataTable();
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                //da.Fill(LLENAR_GRID_1);

                if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
                {
                    MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    DGVRESULTADO.Columns[2].HeaderText = "CLAVE CATASTRAL";       // 
                    DGVRESULTADO.Columns[3].HeaderText = "NOMBRE DEL PROPÍETARIO";                        // 
                    DGVRESULTADO.Columns[4].HeaderText = "DOMICILIO";               //
                    DGVRESULTADO.Columns[5].HeaderText = "VALOR CATASTRAL";                  //
                    DGVRESULTADO.Columns[6].HeaderText = "CERTIFICADO";             //
                    DGVRESULTADO.Columns[7].HeaderText = "OBSERVACIONES";                      //



                    DGVRESULTADO.Columns[8].Visible = false; // Ocultar columna de municipio
                    DGVRESULTADO.Columns[9].Visible = false; // Ocultar columna de zona
                    DGVRESULTADO.Columns[10].Visible = false; // Ocultar columna de manzana
                    DGVRESULTADO.Columns[11].Visible = false; // Ocultar columna de lote
                    DGVRESULTADO.Columns[12].Visible = false; // Ocultar columna de edificio
                    DGVRESULTADO.Columns[13].Visible = false; // Ocultar columna de departamento

                    DGVRESULTADO.Columns[0].Width = 50; // Ajusta el ancho de la columna SERIE
                    DGVRESULTADO.Columns[1].Width = 50; // Ajusta el ancho de la columna FOLIO
                    DGVRESULTADO.Columns[2].Width = 180; // Ajusta el ancho de la columna CLAVE CATASTRAL
                    DGVRESULTADO.Columns[3].Width = 250; // Ajusta el ancho de la columna NOMBRE DEL PROPÍETARIO
                    DGVRESULTADO.Columns[4].Width = 410; // Ajusta el ancho de la columna DOMICILIO
                    DGVRESULTADO.Columns[5].Width = 190; // Ajusta el ancho de la columna VALOR CATASTRAL
                    DGVRESULTADO.Columns[6].Width = 550; // Ajusta el ancho de la columna CERTIFICADO
                    DGVRESULTADO.Columns[7].Width = 400; // Ajusta el ancho de la columna OBSERVACIONES




                    int CONTEO;
                    CONTEO = DGVRESULTADO.Rows.Count - 1;
                    lblNumRegistro.Text = CONTEO.ToString(); //Se limpia el label de conteo de registros
                    DGVRESULTADO.Enabled = true;



                    con.cerrar_interno(); //Cerramos la conexión después de llenar el DataTable
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

        private void rbCertificado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCertificado.Checked == true)
            {
                Cbo_Certificado.Enabled = true;
                Cbo_Certificado.SelectedIndex = 0;
                Cbo_Certificado.Focus();
            }
        }

        private void rbIdenticiudadano_CheckedChanged(object sender, EventArgs e)
        {
            txtpersona.Text = string.Empty;
            txtpersona.Enabled = true;
            txtpersona.Focus();
        }

        private void rbSimilarciudadano_CheckedChanged(object sender, EventArgs e)
        {
            txtpersona.Text = string.Empty;
            txtpersona.Enabled = true;
            txtpersona.Focus();
        }

        private void rbIdentiDomicilio_CheckedChanged(object sender, EventArgs e)
        {
            txtDomicilio.Text = string.Empty;
            txtDomicilio.Enabled = true;
            txtDomicilio.Focus();
        }

        private void rbSimiliarDomicilio_CheckedChanged(object sender, EventArgs e)
        {
            txtDomicilio.Text = string.Empty;
            txtDomicilio.Enabled = true;
            txtDomicilio.Focus();
        }

        private void rbCveCatastral_CheckedChanged(object sender, EventArgs e)
        {
            txtZonab.Text = string.Empty;
            txtZonab.Enabled = true;
            txtMznab.Text = string.Empty;
            txtMznab.Enabled = true;
            txtLoteb.Text = string.Empty;
            txtLoteb.Enabled = true;
            txtEdificiob.Text = string.Empty;
            txtEdificiob.Enabled = true;
            txtDeptob.Text = string.Empty;
            txtDeptob.Enabled = true;
            txtZonab.Focus();
        }

        private void rbFecha_CheckedChanged(object sender, EventArgs e)
        {
            cboDia.Enabled = true;
            cboMes.Enabled = true;
            cboAño.Enabled = true;
            cboDia.SelectedIndex = -1;
            cboMes.SelectedIndex = -1;
            cboAño.SelectedIndex = -1;

            PNLRANGO.Enabled = true;
        }

        private void rbFecharango_CheckedChanged(object sender, EventArgs e)
        {
            cbodiaf.Enabled = true;
            cbomesf.Enabled = true;
            cboañoF.Enabled = true;
            cbodiaf.SelectedIndex = -1;
            cbomesf.SelectedIndex = -1;
            cboañoF.SelectedIndex = -1;
        }

        private void cmdBorrar7_Click(object sender, EventArgs e)
        {
            rbCertificado.Checked = false;
            Cbo_Certificado.SelectedIndex = -1;
            Cbo_Certificado.Enabled = false;
        }

        private void cmdLimpiaCiudadano_Click(object sender, EventArgs e)
        {
            rbSimilarciudadano.Checked = false;
            rbIdenticiudadano.Checked = false;
            txtpersona.Text = "";
            txtpersona.Enabled = false;
        }

        private void cmdLimpiarRFC_Click(object sender, EventArgs e)
        {
            rbIdentiDomicilio.Checked = false;
            rbSimiliarDomicilio.Checked = false;
            txtDomicilio.Text = "";
            txtDomicilio.Enabled = false;
        }

        private void cmdLimpiaCve_Click(object sender, EventArgs e)
        {
            rbCveCatastral.Checked = false;
            txtZonab.Text = string.Empty;
            txtZonab.Enabled = false;
            txtMznab.Text = string.Empty;
            txtMznab.Enabled = false;
            txtLoteb.Text = string.Empty;
            txtLoteb.Enabled = false;
            txtEdificiob.Text = string.Empty;
            txtEdificiob.Enabled = false;
            txtDeptob.Text = string.Empty;
            txtDeptob.Enabled = false;
        }

        private void cmdLimpiarFechaInicio_Click(object sender, EventArgs e)
        {
            rbFecha.Checked = false;
            cboDia.SelectedIndex = -1;
            cboMes.SelectedIndex = -1;
            cboAño.SelectedIndex = -1;
            cboDia.Enabled = false;
            cboMes.Enabled = false;
            cboAño.Enabled = false;
            PNLRANGO.Enabled = false;
            rbFecharango.Checked = false;
            cbodiaf.SelectedIndex = -1;
            cbomesf.SelectedIndex = -1;
            cboañoF.SelectedIndex = -1;
        }

        private void cmdLimpiarFechaFin_Click(object sender, EventArgs e)
        {
            rbFecharango.Checked = false;
            cbodiaf.SelectedIndex = -1;
            cbomesf.SelectedIndex = -1;
            cboañoF.SelectedIndex = -1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            TXT_FOLIO.Text = "";
            TXT_FOLIO.Enabled = false;
            txtZona.Text = string.Empty;
            txtMzna.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtEdificio.Text = string.Empty;
            txtDepto.Text = string.Empty;
            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            btnConsulta.Enabled = false;
            //llenarcboStatus();
            HabilitarPanelBusqueda();
            PNLFBUSCAR.Enabled = true;
            btnConsulta_bus.Enabled = true;
            btnCancelar.Enabled = true;
            btnBuscar.Enabled = false;
            btnConsulta.Focus(); // Establecer el foco en el botón de consulta
        }
        void HabilitarPanelBusqueda()
        {
            quitarRbo();
            inhabilitarTxtycboDeBusqueda();
            habilitarRbo();
            habilitarBotonesdeLimpieza();
            llenarCboFechas();
        }
        void llenarCboFechas()
        {
            cboMes.Items.Clear();
            cboMes.Items.Add("01 Enero");
            cboMes.Items.Add("02 Febrero");
            cboMes.Items.Add("03 Marzo");
            cboMes.Items.Add("04 Abril");
            cboMes.Items.Add("05 Mayo");
            cboMes.Items.Add("06 Junio");
            cboMes.Items.Add("07 Julio");
            cboMes.Items.Add("08 Agosto");
            cboMes.Items.Add("09 Septiembre");
            cboMes.Items.Add("10 Octubre");
            cboMes.Items.Add("11 Noviembre");
            cboMes.Items.Add("12 Diciembre");
            cboMes.SelectedIndex = -1;
            cboMes.Enabled = false;

            cboAño.Items.Clear();
            cboAño.Items.Add("2025");
            cboAño.Items.Add("2026");
            cboAño.Items.Add("2027");
            cboAño.Items.Add("2028");
            cboAño.Items.Add("2029");
            cboAño.SelectedIndex = -1;
            cboAño.Enabled = false;

            cbomesf.Items.Clear();
            cbomesf.Items.Add("01 Enero");
            cbomesf.Items.Add("02 Febrero");
            cbomesf.Items.Add("03 Marzo");
            cbomesf.Items.Add("04 Abril");
            cbomesf.Items.Add("05 Mayo");
            cbomesf.Items.Add("06 Junio");
            cbomesf.Items.Add("07 Julio");
            cbomesf.Items.Add("08 Agosto");
            cbomesf.Items.Add("09 Septiembre");
            cbomesf.Items.Add("10 Octubre");
            cbomesf.Items.Add("11 Noviembre");
            cbomesf.Items.Add("12 Diciembre");
            cbomesf.SelectedIndex = -1;
            cbomesf.Enabled = false;

            cboañoF.Items.Clear();
            cboañoF.Items.Add("2025");
            cboañoF.Items.Add("2026");
            cboañoF.Items.Add("2027");
            cboañoF.Items.Add("2028");
            cboañoF.Items.Add("2029");
            cboañoF.SelectedIndex = -1;
            cboañoF.Enabled = false;
        }

        void quitarRbo()
        {
            //Método para quitar los radio buttons al iniciar el formulario
            rbCertificado.Checked = false;
            rbIdentiDomicilio.Checked = false;
            rbFecha.Checked = false;
            rbFecharango.Checked = false;
            rbIdenticiudadano.Checked = false;
            rbSimilarciudadano.Checked = false;
            rbSimiliarDomicilio.Checked = false;
            rbCveCatastral.Checked = false;

        }
        void habilitarBotonesdeLimpieza()
        {
            //Método para habilitar los botones de limpieza al iniciar el formulario
            cmdBorrar7.Enabled = true;
            cmdLimpiaCiudadano.Enabled = true;
            cmdLimpiarRFC.Enabled = true;
            cmdLimpiaCve.Enabled = true;
            cmdLimpiarFechaInicio.Enabled = true;
            cmdLimpiarFechaFin.Enabled = true;
        }
        void habilitarRbo()
        {
            //Método para habilitar los radio buttons al iniciar el formulario
            rbCertificado.Enabled = true;
            rbIdentiDomicilio.Enabled = true;
            rbFecha.Enabled = true;
            rbFecharango.Enabled = true;
            rbIdenticiudadano.Enabled = true;
            rbSimilarciudadano.Enabled = true;
            rbSimiliarDomicilio.Enabled = true;
            rbCveCatastral.Enabled = true;
        }
        void inhabilitarTxtycboDeBusqueda()
        {
            //Método para inhabilitar los textBox y comboBox al iniciar el formulario
            Cbo_Certificado.Enabled = false;
            txtpersona.Enabled = false;
            txtDomicilio.Enabled = false;
            txtZonab.Enabled = false;
            txtMznab.Enabled = false;
            txtLoteb.Enabled = false;
            txtEdificiob.Enabled = false;
            txtDeptob.Enabled = false;
            cboDia.Enabled = false;
            cboMes.Enabled = false;
            cboAño.Enabled = false;
            PNLRANGO.Enabled = false;
            cbodiaf.Enabled = false;
            cbomesf.Enabled = false;
            cboañoF.Enabled = false;
            DeshabilitarControlesEnPanel(PNLFBUSCAR);
            //cmdCancela.Enabled = false;
        }
        private void DeshabilitarControlesEnPanel(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                // Si el control es TextBox o ComboBox → Deshabilitar
                if (control is TextBox || control is ComboBox)
                {
                    control.Enabled = false;
                }
                // Si el control es un Panel o contenedor → Llamada recursiva
                else if (control is Panel || control is TabControl)
                {
                    DeshabilitarControlesEnPanel(control);
                }
            }
        }

        private void cboMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAño.Text == "" && cboMes.Text != "")
            {
                MessageBox.Show("FAVOR DE SELECCIONAR AÑO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboAño.Focus();
                cboMes.SelectedIndex = -1;
                return;

            }
            if (cboMes.Text != "")
            {

                cboDia.Enabled = true;
                string mestempo = "";
                mestempo = cboMes.Text.Substring(0, 2);
                // 30 dias
                if (mestempo == "04" || mestempo == "06" || mestempo == "09" || mestempo == "11")
                {
                    cboDia.Items.Clear();
                    cboDia.Items.Add("01");
                    cboDia.Items.Add("02");
                    cboDia.Items.Add("03");
                    cboDia.Items.Add("04");
                    cboDia.Items.Add("05");
                    cboDia.Items.Add("06");
                    cboDia.Items.Add("07");
                    cboDia.Items.Add("08");
                    cboDia.Items.Add("09");
                    cboDia.Items.Add("10");
                    cboDia.Items.Add("11");
                    cboDia.Items.Add("12");
                    cboDia.Items.Add("13");
                    cboDia.Items.Add("14");
                    cboDia.Items.Add("15");
                    cboDia.Items.Add("16");
                    cboDia.Items.Add("17");
                    cboDia.Items.Add("18");
                    cboDia.Items.Add("19");
                    cboDia.Items.Add("20");
                    cboDia.Items.Add("21");
                    cboDia.Items.Add("22");
                    cboDia.Items.Add("23");
                    cboDia.Items.Add("24");
                    cboDia.Items.Add("25");
                    cboDia.Items.Add("26");
                    cboDia.Items.Add("27");
                    cboDia.Items.Add("28");
                    cboDia.Items.Add("29");
                    cboDia.Items.Add("30");
                    cboDia.SelectedIndex = -1;
                    cboDia.Enabled = true;
                }
                //31 dias
                if (mestempo == "01" || mestempo == "03" || mestempo == "05" || mestempo == "07" || mestempo == "08" || mestempo == "10" || mestempo == "12")
                {
                    cboDia.Items.Clear();
                    cboDia.Items.Add("01");
                    cboDia.Items.Add("02");
                    cboDia.Items.Add("03");
                    cboDia.Items.Add("04");
                    cboDia.Items.Add("05");
                    cboDia.Items.Add("06");
                    cboDia.Items.Add("07");
                    cboDia.Items.Add("08");
                    cboDia.Items.Add("09");
                    cboDia.Items.Add("10");
                    cboDia.Items.Add("11");
                    cboDia.Items.Add("12");
                    cboDia.Items.Add("13");
                    cboDia.Items.Add("14");
                    cboDia.Items.Add("15");
                    cboDia.Items.Add("16");
                    cboDia.Items.Add("17");
                    cboDia.Items.Add("18");
                    cboDia.Items.Add("19");
                    cboDia.Items.Add("20");
                    cboDia.Items.Add("21");
                    cboDia.Items.Add("22");
                    cboDia.Items.Add("23");
                    cboDia.Items.Add("24");
                    cboDia.Items.Add("25");
                    cboDia.Items.Add("26");
                    cboDia.Items.Add("27");
                    cboDia.Items.Add("28");
                    cboDia.Items.Add("29");
                    cboDia.Items.Add("30");
                    cboDia.Items.Add("31");
                    cboDia.SelectedIndex = -1;
                    cboDia.Enabled = true;
                }
                //28 dias
                if (mestempo == "02")
                {

                    cboDia.Items.Clear();
                    cboDia.Items.Add("01");
                    cboDia.Items.Add("02");
                    cboDia.Items.Add("03");
                    cboDia.Items.Add("04");
                    cboDia.Items.Add("05");
                    cboDia.Items.Add("06");
                    cboDia.Items.Add("07");
                    cboDia.Items.Add("08");
                    cboDia.Items.Add("09");
                    cboDia.Items.Add("10");
                    cboDia.Items.Add("11");
                    cboDia.Items.Add("12");
                    cboDia.Items.Add("13");
                    cboDia.Items.Add("14");
                    cboDia.Items.Add("15");
                    cboDia.Items.Add("16");
                    cboDia.Items.Add("17");
                    cboDia.Items.Add("18");
                    cboDia.Items.Add("19");
                    cboDia.Items.Add("20");
                    cboDia.Items.Add("21");
                    cboDia.Items.Add("22");
                    cboDia.Items.Add("23");
                    cboDia.Items.Add("24");
                    cboDia.Items.Add("25");
                    cboDia.Items.Add("26");
                    cboDia.Items.Add("27");

                    if (cboAño.Text == "2020" || cboAño.Text == "2024" || cboAño.Text == "2028" || cboAño.Text == "2032" || cboAño.Text == "2036")
                    {
                        cboDia.Items.Add("28");
                        cboDia.Items.Add("29");
                    }
                    else
                    {
                        cboDia.Items.Add("28");

                    }
                    cboDia.SelectedIndex = -1;
                    // cboDia1.Enabled = false;
                }
            }
        }

        private void cbomesf_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboañoF.Text == "" && cbomesf.Text != "") //SI SON VACIOS, ERROR
            {
                MessageBox.Show("FAVOR DE SELECCIONAR UN AÑO A BUSCAR", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboañoF.Focus();
                cbomesf.SelectedIndex = -1;
                return;

            }
            if (cbomesf.Text != "")
            {

                cbodiaf.Enabled = true;
                string mestempo = "";
                mestempo = cbomesf.Text.Substring(0, 2);
                // 30 dias
                if (mestempo == "04" || mestempo == "06" || mestempo == "09" || mestempo == "11")
                {
                    cbodiaf.Items.Clear();
                    cbodiaf.Items.Add("01");
                    cbodiaf.Items.Add("02");
                    cbodiaf.Items.Add("03");
                    cbodiaf.Items.Add("04");
                    cbodiaf.Items.Add("05");
                    cbodiaf.Items.Add("06");
                    cbodiaf.Items.Add("07");
                    cbodiaf.Items.Add("08");
                    cbodiaf.Items.Add("09");
                    cbodiaf.Items.Add("10");
                    cbodiaf.Items.Add("11");
                    cbodiaf.Items.Add("12");
                    cbodiaf.Items.Add("13");
                    cbodiaf.Items.Add("14");
                    cbodiaf.Items.Add("15");
                    cbodiaf.Items.Add("16");
                    cbodiaf.Items.Add("17");
                    cbodiaf.Items.Add("18");
                    cbodiaf.Items.Add("19");
                    cbodiaf.Items.Add("20");
                    cbodiaf.Items.Add("21");
                    cbodiaf.Items.Add("22");
                    cbodiaf.Items.Add("23");
                    cbodiaf.Items.Add("24");
                    cbodiaf.Items.Add("25");
                    cbodiaf.Items.Add("26");
                    cbodiaf.Items.Add("27");
                    cbodiaf.Items.Add("28");
                    cbodiaf.Items.Add("29");
                    cbodiaf.Items.Add("30");
                    cbodiaf.SelectedIndex = -1;
                    cbodiaf.Enabled = true;
                }
                //31 dias
                if (mestempo == "01" || mestempo == "03" || mestempo == "05" || mestempo == "07" || mestempo == "08" || mestempo == "10" || mestempo == "12")
                {
                    cbodiaf.Items.Clear();
                    cbodiaf.Items.Add("01");
                    cbodiaf.Items.Add("02");
                    cbodiaf.Items.Add("03");
                    cbodiaf.Items.Add("04");
                    cbodiaf.Items.Add("05");
                    cbodiaf.Items.Add("06");
                    cbodiaf.Items.Add("07");
                    cbodiaf.Items.Add("08");
                    cbodiaf.Items.Add("09");
                    cbodiaf.Items.Add("10");
                    cbodiaf.Items.Add("11");
                    cbodiaf.Items.Add("12");
                    cbodiaf.Items.Add("13");
                    cbodiaf.Items.Add("14");
                    cbodiaf.Items.Add("15");
                    cbodiaf.Items.Add("16");
                    cbodiaf.Items.Add("17");
                    cbodiaf.Items.Add("18");
                    cbodiaf.Items.Add("19");
                    cbodiaf.Items.Add("20");
                    cbodiaf.Items.Add("21");
                    cbodiaf.Items.Add("22");
                    cbodiaf.Items.Add("23");
                    cbodiaf.Items.Add("24");
                    cbodiaf.Items.Add("25");
                    cbodiaf.Items.Add("26");
                    cbodiaf.Items.Add("27");
                    cbodiaf.Items.Add("28");
                    cbodiaf.Items.Add("29");
                    cbodiaf.Items.Add("30");
                    cbodiaf.Items.Add("31");
                    cbodiaf.SelectedIndex = -1;
                    cbodiaf.Enabled = true;
                }
                //28 dias
                if (mestempo == "02")
                {

                    cbodiaf.Items.Clear();
                    cbodiaf.Items.Add("01");
                    cbodiaf.Items.Add("02");
                    cbodiaf.Items.Add("03");
                    cbodiaf.Items.Add("04");
                    cbodiaf.Items.Add("05");
                    cbodiaf.Items.Add("06");
                    cbodiaf.Items.Add("07");
                    cbodiaf.Items.Add("08");
                    cbodiaf.Items.Add("09");
                    cbodiaf.Items.Add("10");
                    cbodiaf.Items.Add("11");
                    cbodiaf.Items.Add("12");
                    cbodiaf.Items.Add("13");
                    cbodiaf.Items.Add("14");
                    cbodiaf.Items.Add("15");
                    cbodiaf.Items.Add("16");
                    cbodiaf.Items.Add("17");
                    cbodiaf.Items.Add("18");
                    cbodiaf.Items.Add("19");
                    cbodiaf.Items.Add("20");
                    cbodiaf.Items.Add("21");
                    cbodiaf.Items.Add("22");
                    cbodiaf.Items.Add("23");
                    cbodiaf.Items.Add("24");
                    cbodiaf.Items.Add("25");
                    cbodiaf.Items.Add("26");
                    cbodiaf.Items.Add("27");
                    /// años biciestos contemplando hasta el 2032 si es que aun vivimos jajaja
                    if (cboañoF.Text == "2020" || cboañoF.Text == "2024" || cboañoF.Text == "2028" || cboañoF.Text == "2032" || cboañoF.Text == "2036")
                    {
                        cbodiaf.Items.Add("28");
                        cbodiaf.Items.Add("29");
                    }
                    else
                    {
                        cbodiaf.Items.Add("28");

                    }
                    cbodiaf.SelectedIndex = -1;
                    // cboDia1.Enabled = false;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarDataGridView();
            limpiar_2();
            lblNumRegistro.Text = "0";

        }
        void limpiarDataGridView()
        {
            // Método para limpiar los DataGridView
            DGVRESULTADO.DataSource = null; // Si estaba enlazado a un DataSource
            DGVRESULTADO.Rows.Clear();
            DGVRESULTADO.Columns.Clear();
        }

        private void DGVRESULTADO_DoubleClick(object sender, EventArgs e)
        {
            // string SERIED = string.Empty;
            double FOLIOD = 0.0;
            double TERRENO1;
            double TERRENO2;
            double TERRENO3;
            double TERRENO4;
            double TERRENO5;
            double construccion1;
            double construccion2;
            double construccion3;
            double CONSTRUCCION4;
            double CONSTRUCCION5;
            double VALOR_CAT;
            double terreno6;
            double construccion6;

            if (DGVRESULTADO.CurrentRow.Cells[0].Value.ToString() == "")
            {
                MessageBox.Show("SELECCIONE UN DATO CORRECTO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // Sale del método o procedimiento
            }

            SERIE = Convert.ToString(DGVRESULTADO.CurrentRow.Cells[0].Value).Trim();
            FOLIO = Convert.ToInt32(DGVRESULTADO.CurrentRow.Cells[1].Value);
            MUNICIPIO_M = Convert.ToInt32(DGVRESULTADO.CurrentRow.Cells[8].Value);
            ZONA_M = Convert.ToInt32(DGVRESULTADO.CurrentRow.Cells[9].Value);
            MANZANA_M = Convert.ToInt32(DGVRESULTADO.CurrentRow.Cells[10].Value);
            LOTE_M = Convert.ToInt32(DGVRESULTADO.CurrentRow.Cells[11].Value);
            EDIFICIO_M = DGVRESULTADO.CurrentRow.Cells[12].Value.ToString().Trim();
            DEPTO_M = DGVRESULTADO.CurrentRow.Cells[13].Value.ToString().Trim();

            if (DGVRESULTADO.CurrentRow.Index != -1)
            {
                try
                {

                    //VERIFICA QUE EL FOLIO ESTA AUTORIZADO POR REVISION EN LA TABLA CAT_DONDE_VA_2025
                    int verificar = 0;
                    con.conectar_base_interno();
                    con.cadena_sql_interno = " ";
                    con.cadena_sql_interno = "                          IF EXISTS (SELECT VENTANILLA";
                    con.cadena_sql_interno = con.cadena_sql_interno + "              FROM CAT_DONDE_VA_2025  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             Where SERIE =" + util.scm(SERIE);
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND FOLIO_ORIGEN = " + FOLIO;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND CARTOGRAFIA = 1 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND VENTANILLA = 1 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND REVISO = 1 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             )";
                    con.cadena_sql_interno = con.cadena_sql_interno + "                 BEGIN";
                    con.cadena_sql_interno = con.cadena_sql_interno + "                     SELECT existe = 1";
                    con.cadena_sql_interno = con.cadena_sql_interno + "                  End";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             Else";
                    con.cadena_sql_interno = con.cadena_sql_interno + "                 BEGIN";
                    con.cadena_sql_interno = con.cadena_sql_interno + "                     SELECT existe = 2";
                    con.cadena_sql_interno = con.cadena_sql_interno + "                 End";

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
                        MessageBox.Show("NO SE PUEDE REALIZAR EL PROCESO HASTA QUE SEA AUTORIZADO POR REVISION", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        limpiar_2();
                        //txtZona.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    return; // Retornar false si ocurre un error
                }

                try
                {
                    //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE
                    int verificar = 0;
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                    con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_M;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_M;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA_M;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE_M;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO_M);
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO_M) + ")";
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
                        MessageBox.Show("ESTA CLAVE CATASTRAL ESTA BLOQUEADA POR CATASTRO ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE, SEGUNDA CONSULTA
                    int verificar = 0;
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                    con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_M;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_M;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA_M;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE_M;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO_M);
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO_M) + ")";
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
                        MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA POR TESORERIA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                {   // Aquí iría la lógica para realizar la consulta a la base de datos
                    // Por ejemplo, podrías llamar a un método que realice la consulta y muestre los resultados en un DataGridView o similar.

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////// OBTENEMOS DATOS DEL FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT TRE.tipo_certificacion, TRE.folio_CERTI, TRE.status_predio, TRE.status_aportacion, TRE.nombre_contri, ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         TRE.domicilio_fis, TRE.descripcion_colonia, TRE.tp, TRE.tc, TRE.valor_terreno, TRE.valor_terreno_comun ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         TRE.cp,TRE.cc, TRE.valor_construccion, TRE.valor_comun, TRE.OBSERVACIONES, SGC.Certificado,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         SOA.seriePag, SOA.folioPag,  TRE.Serie_Orden, TRE.Folio_Orden, SOA.nombreUsuario, TRE.FOLIO_CERTI,      ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         TRE.estado2, TRE.municipio2, TRE.zona2, TRE.manzana2, TRE.lote2, TRE.edificio2, TRE.depto2,  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         TRE.AÑO_PREDIAL, TRE.MES_PREDIAL, TRE.SERIEPAGO, TRE.FOLIOPAGO, TRE.ID, CDV.REVISO  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    FROM TRES_EN_UNO_2025 TRE, song_certificaciones SGC, SONG_ordenesPagoAutoriza SOA , CAT_DONDE_VA_2025 CDV ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE TRE.serie = " + util.scm(SERIE);
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.FOLIO = " + FOLIO;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.serie = CDV.SERIE ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.FOLIO = CDV.FOLIO_ORIGEN ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.tipo_certificacion = SGC.id_certificado ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.Serie_Orden = SOA.serieOrd ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.Folio_Orden = SOA.folioOrd ";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();


                    // Verificar si el resultado está vacío
                    if (!con.leer_interno.HasRows)
                    {
                        MessageBox.Show("FOLIO Y CLAVE NO ENCONTRADO EN LA ACTUALIZACION DEL SISTEMA SUM TREE 2.0 ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiar_2();
                        return; // Retornar si no hay resultados
                    }


                    while (con.leer_interno.Read())
                    {
                        if (con.leer_interno[0].ToString().Trim() != "")
                        {
                            tipo_ceti_2 = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                            CLAVE_3_1 = Convert.ToDouble(con.leer_interno[1].ToString().Trim());
                            val_predio = Convert.ToInt32(con.leer_interno[2].ToString().Trim());
                            val_aportacion = Convert.ToInt32(con.leer_interno[3].ToString().Trim());
                            lblTitular.Text = con.leer_interno[4].ToString().Trim();
                            lblCalle.Text = con.leer_interno[5].ToString().Trim();
                            lblColonia.Text = con.leer_interno[6].ToString().Trim();
                            TERRENO1 = Convert.ToDouble(con.leer_interno[7].ToString().Trim()); //SUPERFICIE TERRENO PROPIO
                            TERRENO2 = Convert.ToDouble(con.leer_interno[8].ToString().Trim()); //SUPERFICIE TERRENO COMUN
                            TERRENO3 = Convert.ToDouble(con.leer_interno[9].ToString().Trim()); //VALOR TERRENO PROPIO
                            TERRENO4 = Convert.ToDouble(con.leer_interno[10].ToString().Trim()); //VALOR TERRENO COMUN
                            TERRENO5 = TERRENO4 + TERRENO3; //TOTAL VALOR TERRENO PROPIO + COMUN
                            terreno6 = TERRENO1 + TERRENO2; //TOTAL SUPERFICIE TERRENO PROPIO + COMUN
                            lblTerrenoTot.Text = terreno6.ToString("N2");
                            lblSupTerrPriv.Text = TERRENO1.ToString("N2");
                            lblSupTerrComun.Text = TERRENO2.ToString("N2");
                            lblTerrenoPrivadoV.Text = TERRENO3.ToString("N2");
                            lblTerrenoComunV.Text = TERRENO4.ToString("N2");
                            lblValTotTerr.Text = TERRENO5.ToString("N2");
                            construccion1 = Convert.ToDouble(con.leer_interno[11].ToString().Trim()); //SUPERFICIE CONSTRUCCION PROPIA
                            construccion2 = Convert.ToDouble(con.leer_interno[12].ToString().Trim()); //SUPERFICIE CONSTRUCCION COMUN
                            construccion3 = Convert.ToDouble(con.leer_interno[13].ToString().Trim()); //VALOR CONSTRUCCION PROPIA
                            CONSTRUCCION4 = Convert.ToDouble(con.leer_interno[14].ToString().Trim()); //VALOR CONSTRUCCION COMUN
                            CONSTRUCCION5 = construccion3 + CONSTRUCCION4; //TOTAL VALOR CONSTRUCCION PROPIA + COMUN
                            construccion6 = construccion1 + construccion2; //TOTAL SUPERFICIE CONSTRUCCION PROPIA + COMUN
                            lblConstTot.Text = construccion6.ToString("N2");
                            lblConstruccionPrivada.Text = construccion1.ToString("N2");
                            lblConstruccionComun.Text = construccion2.ToString("N2");
                            lblValorConsPriv.Text = construccion3.ToString("N2");
                            lblConsComunV.Text = CONSTRUCCION4.ToString("N4");
                            lblValTotCons.Text = CONSTRUCCION5.ToString("N2");
                            VALOR_CAT = TERRENO5 + CONSTRUCCION5;
                            lblValor.Text = VALOR_CAT.ToString("N2");
                            lblObservaciones.Text = con.leer_interno[15].ToString().Trim();
                            lblCertificado.Text = con.leer_interno[16].ToString().Trim();
                            lblSerieCertificado.Text = con.leer_interno[17].ToString().Trim();
                            lblFolioCertificado.Text = con.leer_interno[18].ToString().Trim();
                            lblSerieOrd.Text = con.leer_interno[19].ToString().Trim();
                            lblFolioOrd.Text = con.leer_interno[20].ToString().Trim();
                            lblGenerada.Text = con.leer_interno[21].ToString().Trim(); //nombre del usuario que genero la orden de pago
                            FOLIO_CERTI = con.leer_interno[22].ToString().Trim(); //FOLIO DEL CERTIFICADO
                            estado2 = con.leer_interno[23].ToString().Trim(); //ESTADO
                            MUNICIPIO2 = con.leer_interno[24].ToString().Trim(); //MUNICIPIO
                            ZONA2 = con.leer_interno[25].ToString().Trim(); //ZONA
                            MANZANA2 = con.leer_interno[26].ToString().Trim(); //MANZANA
                            LOTE2 = con.leer_interno[27].ToString().Trim(); //LOTE
                            EDIFICIO2 = con.leer_interno[28].ToString().Trim(); //EDIFICIO
                            DEPTO2 = con.leer_interno[29].ToString().Trim(); //DEPARTAMENTO
                            lblAñoPredio.Text = con.leer_interno[30].ToString().Trim();
                            lblMesPredio.Text = con.leer_interno[31].ToString().Trim();
                            lblSeriePredio.Text = con.leer_interno[32].ToString().Trim();
                            lblFolioPredio.Text = con.leer_interno[33].ToString().Trim();
                            IMPRIMIO_SI_NO = Convert.ToInt32(con.leer_interno[34].ToString().Trim()); //SI SE IMPRIMIO O NO 
                            REVISO = Convert.ToInt32(con.leer_interno[35].ToString().Trim());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    return; // Retornar false si ocurre un error
                }


                //REVISAMOS SI ESTA PAGADO EL CERTITIICADO
                try
                {
                    int verificar = 0; // Variable para almacenar el resultado de la consulta
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                    con.cadena_sql_interno = con.cadena_sql_interno + "              FROM RECIBOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE SERIE =" + util.scm(lblSerieCertificado.Text);
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND folio = " + lblFolioCertificado.Text;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND STATUS = 'A' ";

                    con.cadena_sql_interno = con.cadena_sql_interno + "            )";
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
                        lblOrdenPago.Text = "P";
                    }
                    else if (verificar == 2)
                    {
                        lblOrdenPago.Text = "O";
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



                predio();
                btnImprimir.Enabled = true;
                btnActualizar.Enabled = true;
                //if (lblCertificado.Text.Trim() == "CERTIFICADO CLAVE Y VALOR CATASTRAL / APORTACION A MEJORAS / NO ADEUDO PREDIAL")
                //{
                //    btnActualizar.Enabled = true;

                //}
                //else
                //{
                //    btnActualizar.Enabled = false;
                //}

            }
        }

        private void PNLFBUSCAR_Paint(object sender, PaintEventArgs e)
        {

        }

        private void HABILITAR()
        {
            txtZona.Enabled = true;
            txtMzna.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;
            //CBO_SERIE.Enabled = true;
            TXT_FOLIO.Enabled = true;
            btnConsulta.Enabled = true;

        }
        private void frmTresenUno_Load(object sender, EventArgs e)
        {
            label7.Text = "Usuario: " + Program.nombre_usuario;
            llenarSerie();
            limpiar_inicio();
        }


        private void llenarSerie()
        {
            CBO_SERIE.Items.Clear();
            con.conectar_base_interno();
            con.open_c_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "     SELECT SERIE ";
            con.cadena_sql_interno = con.cadena_sql_interno + "       FROM RECIBOS ";
            con.cadena_sql_interno = con.cadena_sql_interno + "      WHERE FECCOB > '20250101'  ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   GROUP BY SERIE ";
            con.cadena_sql_cmd_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                CBO_SERIE.Items.Add(con.leer_interno[0].ToString().Trim());
            }
            //CERRAR CONEXIÓN 
            CBO_SERIE.SelectedIndex = 0;

            //para el status de la factura  
            con.cerrar_interno();
        }

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void txtZonab_TextChanged(object sender, EventArgs e)
        {
            if (txtZonab.Text.Length == 2) { txtMznab.Focus(); }
        }

        private void txtMznab_TextChanged(object sender, EventArgs e)
        {
            if (txtMznab.Text.Length == 3) { txtLoteb.Focus(); }
        }

        private void txtLoteb_TextChanged(object sender, EventArgs e)
        {
            if (txtLoteb.Text.Length == 2) { txtEdificiob.Focus(); }
        }

        private void txtEdificiob_TextChanged(object sender, EventArgs e)
        {
            if (txtEdificiob.Text.Length == 2) { txtDeptob.Focus(); }
        }

        private void txtDeptob_TextChanged(object sender, EventArgs e)
        {
            if (txtDeptob.Text.Length == 4) { btnConsulta_bus.Focus(); }
        }

        private void frmTresenUno_Activated(object sender, EventArgs e)
        {
            if (!focoEstablecido)
            {
                txtZona.Focus();
                focoEstablecido = true;
            }
        }

        private void lblConstruccionPrivada_Click(object sender, EventArgs e)
        {

        }

        private void panel53_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel62_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TXT_FOLIO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consulta();
            }
        }
        private void limpiar_2()
        {
            lblTitular.Text = string.Empty;
            lblCalle.Text = string.Empty;
            lblColonia.Text = string.Empty;
            lblSupTerrPriv.Text = string.Empty;
            lblSupTerrComun.Text = string.Empty;
            lblTerrenoPrivadoV.Text = string.Empty;
            lblTerrenoComunV.Text = string.Empty;
            lblValTotTerr.Text = string.Empty;
            lblConstruccionPrivada.Text = string.Empty;
            lblConstruccionComun.Text = string.Empty;
            lblValorConsPriv.Text = string.Empty;
            lblConsComunV.Text = string.Empty;
            lblValTotCons.Text = string.Empty;
            lblValor.Text = string.Empty;
            lblObservaciones.Text = string.Empty;
            lblSerieOrd.Text = string.Empty;
            lblFolioOrd.Text = string.Empty;
            lblSerieCertificado.Text = string.Empty;
            lblFolioCertificado.Text = string.Empty;
            lblCertificado.Text = string.Empty;
            lblOrdenPago.Text = "O";
            lblPredio.Text = "O";
            lblSeriePredio.Text = string.Empty;
            lblFolioPredio.Text = string.Empty;
            lblAñoPredio.Text = string.Empty;
            lblMesPredio.Text = string.Empty;
            lblGenerada.Text = string.Empty;
            lblTerrenoTot.Text = string.Empty;
            lblConstTot.Text = string.Empty;
        }
        private void limpiarCampos()
        {
            txtZona.Text = string.Empty;
            txtMzna.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtEdificio.Text = string.Empty;
            txtDepto.Text = string.Empty;
            lblTitular.Text = string.Empty;
            lblCalle.Text = string.Empty;
            lblColonia.Text = string.Empty;
            lblSupTerrPriv.Text = string.Empty;
            lblSupTerrComun.Text = string.Empty;
            lblTerrenoPrivadoV.Text = string.Empty;
            lblTerrenoComunV.Text = string.Empty;
            lblValTotTerr.Text = string.Empty;
            lblConstruccionPrivada.Text = string.Empty;
            lblConstruccionComun.Text = string.Empty;
            lblValorConsPriv.Text = string.Empty;
            lblConsComunV.Text = string.Empty;
            lblValTotCons.Text = string.Empty;
            lblValor.Text = string.Empty;
            lblObservaciones.Text = string.Empty;
            TXT_FOLIO.Text = "";
            lblSerieOrd.Text = string.Empty;
            lblFolioOrd.Text = string.Empty;
            lblSerieCertificado.Text = string.Empty;
            lblFolioCertificado.Text = string.Empty;
            lblCertificado.Text = string.Empty;
            lblOrdenPago.Text = "O";
            lblPredio.Text = "O";
            lblNumRegistro.Text = "0"; // Reiniciar el conteo de registros
            lblSeriePredio.Text = string.Empty;
            lblFolioPredio.Text = string.Empty;
            lblAñoPredio.Text = string.Empty;
            lblMesPredio.Text = string.Empty;
            lblGenerada.Text = string.Empty;
            Cbo_Certificado.SelectedIndex = -1;
            lblTerrenoTot.Text = string.Empty;
            lblConstTot.Text = string.Empty;

        }

        private void TXT_FOLIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
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

        private void txtZonab_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtLoteb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
        }

        private void btnBuscar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBuscar, "FILTRO DE BUSQUEDA");
        }

        private void btnCancela_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCancela, "CANCELAR");
        }

        private void btnSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnSalida, "SALIDA");
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close(); // Cerrar el formulario actual
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pnlDatosPredio_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnConsulta_bus_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnConsulta_bus, "CONSULTAR BUSQUEDA");
        }

        private void btnCancelar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCancelar, "CANCELAR BUSQUEDA");
        }

        private void btnImprimir_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnImprimir, "IMPRIMIR CERTIFICADO");
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            string latitud="";
            string longitud="";
            try
            {
                ///OBTENER LA GEOLOCALIZACIÓN
                con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT TOP 1 LATITUD, LONGITUD";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_GEOLOCALIZACION";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana   = " + Convert.ToInt32(txtMzna.Text.Trim());  //Se cocatena la manzana que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Lote      = " + Convert.ToInt32(txtLote.Text.Trim());  //Se cocatena el lote que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO  = '" + txtEdificio.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO     = '" + txtDepto.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY id DESC";


            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            while (con.leer_interno.Read())
            {
                if (con.leer_interno[0].ToString().Trim() != "")
                {
                    latitud = con.leer_interno[0].ToString().Trim();
                    longitud = con.leer_interno[1].ToString().Trim();
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
    Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            frmActualizar3en1 actualizar = new frmActualizar3en1();
            actualizar.Municipio = MUNICIPIO_M;
            actualizar.Manzana = MANZANA_M;
            actualizar.Zona = ZONA_M;
            actualizar.Lote = LOTE_M;
            actualizar.Edifico = EDIFICIO_M;
            actualizar.Depto = DEPTO_M;
            actualizar.Folio_Catastro = FOLIO;
            actualizar.Serie_Catastro = SERIE;
            actualizar.ShowDialog();
            if (actualizar.MODIFICO == 1)
            {
                lblSeriePredio.Text = actualizar.SERIE_TEU;
                lblFolioPredio.Text = actualizar.FOLIO_TEU.ToString();
                lblMesPredio.Text = actualizar.MES_TEU.ToString();
                lblAñoPredio.Text = actualizar.AÑO_TEU.ToString();
                predio();
                btnActualizar.Enabled = false;
            }

        }

        private void PanelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMaps_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnMaps, "MOSTRAR UBICACION");
        }

        private void panel54_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            consulta();
            btnActualizar.Enabled = true;
        }
        private void consulta()
        {
            double TERRENO1;
            double TERRENO2;
            double TERRENO3;
            double TERRENO4;
            double TERRENO5;
            double construccion1;
            double construccion2;
            double construccion3;
            double CONSTRUCCION4;
            double CONSTRUCCION5;
            double VALOR_CAT;
            double terreno6;
            double construccion6;

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
            if (string.IsNullOrEmpty(TXT_FOLIO.Text))
            {
                MessageBox.Show("NO SE TIENE EL FOLIO", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // Sale del método o procedimiento
            }
            if (string.IsNullOrEmpty(CBO_SERIE.Text))
            {
                MessageBox.Show("NO SE TIENE LA SERIE", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // Sale del método o procedimiento
            }

            MUNICIPIO_M = Convert.ToInt32(lblMun.Text.Trim());
            ZONA_M = Convert.ToInt32(txtZona.Text.Trim());
            MANZANA_M = Convert.ToInt32(txtMzna.Text.Trim());
            LOTE_M = Convert.ToInt32(txtLote.Text.Trim());
            EDIFICIO_M = txtEdificio.Text.Trim();
            DEPTO_M = txtDepto.Text.Trim();
            FOLIO = int.Parse(TXT_FOLIO.Text);
            SERIE = CBO_SERIE.Text.Trim();

            try
            {

                //VERIFICA QUE EL FOLIO ESTA AUTORIZADO POR REVISION EN LA TABLA CAT_DONDE_VA_2025
                int verificar = 0;
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = "                          IF EXISTS (SELECT VENTANILLA";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM CAT_DONDE_VA_2025  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where SERIE =" + util.scm(SERIE);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND FOLIO_ORIGEN = " + FOLIO;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND CARTOGRAFIA = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND VENTANILLA = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND REVISO = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";
                con.cadena_sql_interno = con.cadena_sql_interno + "                 BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "                     SELECT existe = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "                  End";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Else";
                con.cadena_sql_interno = con.cadena_sql_interno + "                 BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "                     SELECT existe = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "                 End";

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
                    MessageBox.Show("NO SE PUEDE REALIZAR EL PROCESO HASTA QUE SEA AUTORIZADO POR REVISION", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //txtZona.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
          


            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE
                int verificar = 0;
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO_M);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO_M) + ")";
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
                    MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZona.Focus();
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
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE, SEGUNDA CONSULTA
                int verificar = 0;
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO_M);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO_M) + ")";
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
                    MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZona.Focus();
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
            {   // Aquí iría la lógica para realizar la consulta a la base de datos
                // Por ejemplo, podrías llamar a un método que realice la consulta y muestre los resultados en un DataGridView o similar.

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                /////// OBTENEMOS DATOS DEL FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT TRE.tipo_certificacion, TRE.folio_CERTI, TRE.status_predio, TRE.status_aportacion, TRE.nombre_contri, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "         TRE.domicilio_fis, TRE.descripcion_colonia, TRE.tp, TRE.tc, TRE.valor_terreno, TRE.valor_terreno_comun ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "         TRE.cp,TRE.cc, TRE.valor_construccion, TRE.valor_comun, TRE.OBSERVACIONES, SGC.Certificado,";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SOA.seriePag, SOA.folioPag,  TRE.Serie_Orden, TRE.Folio_Orden, SOA.nombreUsuario, TRE.FOLIO_CERTI,      ";
                con.cadena_sql_interno = con.cadena_sql_interno + "         TRE.estado2, TRE.municipio2, TRE.zona2, TRE.manzana2, TRE.lote2, TRE.edificio2, TRE.depto2,     ";
                con.cadena_sql_interno = con.cadena_sql_interno + "         TRE.AÑO_PREDIAL, TRE.MES_PREDIAL, TRE.SERIEPAGO, TRE.FOLIOPAGO, TRE.ID";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM TRES_EN_UNO_2025 TRE, song_certificaciones SGC, SONG_ordenesPagoAutoriza SOA ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE TRE.serie =" + util.scm(SERIE);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.FOLIO = " + FOLIO;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.Municipio =   " + MUNICIPIO_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.zona = " + ZONA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.manzana = " + MANZANA_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.lote = " + LOTE_M;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.edificio = " + util.scm(EDIFICIO_M);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.depto = " + util.scm(DEPTO_M);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.tipo_certificacion = SGC.id_certificado ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.Serie_Orden = SOA.serieOrd ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND TRE.Folio_Orden = SOA.folioOrd ";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();


                // Verificar si el resultado está vacío
                if (!con.leer_interno.HasRows)
                {

                    MessageBox.Show("FOLIO Y CLAVE NO ENCONTRADO", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnConsulta.Enabled = true;
                    btnBuscar.Enabled = true;
                    CBO_SERIE.Enabled = true;
                    TXT_FOLIO.Enabled = true;
                    limpiarCampos();
                    txtZona.Focus();
                    return; // Retornar si no hay resultados
                }


                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {

                        tipo_ceti_2 = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                        CLAVE_3_1 = Convert.ToDouble(con.leer_interno[1].ToString().Trim());
                        val_predio = Convert.ToInt32(con.leer_interno[2].ToString().Trim());
                        val_aportacion = Convert.ToInt32(con.leer_interno[3].ToString().Trim());

                        lblTitular.Text = con.leer_interno[4].ToString().Trim();
                        lblCalle.Text = con.leer_interno[5].ToString().Trim();
                        lblColonia.Text = con.leer_interno[6].ToString().Trim();
                        TERRENO1 = Convert.ToDouble(con.leer_interno[7].ToString().Trim()); //SUPERFICIE TERRENO PROPIO
                        TERRENO2 = Convert.ToDouble(con.leer_interno[8].ToString().Trim()); //SUPERFICIE TERRENO COMUN
                        TERRENO3 = Convert.ToDouble(con.leer_interno[9].ToString().Trim()); //VALOR TERRENO PROPIO
                        TERRENO4 = Convert.ToDouble(con.leer_interno[10].ToString().Trim()); //VALOR TERRENO COMUN
                        TERRENO5 = TERRENO4 + TERRENO3; //TOTAL VALOR TERRENO PROPIO + COMUN
                        terreno6 = TERRENO1 + TERRENO2; //TOTAL SUPERFICIE TERRENO PROPIO + COMUN
                        lblTerrenoTot.Text = terreno6.ToString("N2");
                        lblSupTerrPriv.Text = TERRENO1.ToString("N2");
                        lblSupTerrComun.Text = TERRENO2.ToString("N2");
                        lblTerrenoPrivadoV.Text = TERRENO3.ToString("N2");
                        lblTerrenoComunV.Text = TERRENO4.ToString("N2");
                        lblValTotTerr.Text = TERRENO5.ToString("N2");
                        construccion1 = Convert.ToDouble(con.leer_interno[11].ToString().Trim()); //SUPERFICIE CONSTRUCCION PROPIA
                        construccion2 = Convert.ToDouble(con.leer_interno[12].ToString().Trim()); //SUPERFICIE CONSTRUCCION COMUN
                        construccion3 = Convert.ToDouble(con.leer_interno[13].ToString().Trim()); //VALOR CONSTRUCCION PROPIA
                        CONSTRUCCION4 = Convert.ToDouble(con.leer_interno[14].ToString().Trim()); //VALOR CONSTRUCCION COMUN
                        CONSTRUCCION5 = construccion3 + CONSTRUCCION4; //TOTAL VALOR CONSTRUCCION PROPIA + COMUN
                        construccion6 = construccion1 + construccion2; //TOTAL SUPERFICIE CONSTRUCCION PROPIA + COMUN
                        lblConstTot.Text = construccion6.ToString("N2");
                        lblConstruccionPrivada.Text = construccion1.ToString("N2");
                        lblConstruccionComun.Text = construccion2.ToString("N2");
                        lblValorConsPriv.Text = construccion3.ToString("N2");
                        lblConsComunV.Text = CONSTRUCCION4.ToString("N4");
                        lblValTotCons.Text = CONSTRUCCION5.ToString("N2");

                        VALOR_CAT = TERRENO5 + CONSTRUCCION5;
                        lblValor.Text = VALOR_CAT.ToString("N2");
                        lblObservaciones.Text = con.leer_interno[15].ToString().Trim();
                        lblCertificado.Text = con.leer_interno[16].ToString().Trim();
                        lblSerieCertificado.Text = con.leer_interno[17].ToString().Trim();
                        lblFolioCertificado.Text = con.leer_interno[18].ToString().Trim();
                        lblSerieOrd.Text = con.leer_interno[19].ToString().Trim();
                        lblFolioOrd.Text = con.leer_interno[20].ToString().Trim();
                        lblGenerada.Text = con.leer_interno[21].ToString().Trim(); //nombre del usuario que genero la orden de pago
                        FOLIO_CERTI = con.leer_interno[22].ToString().Trim(); //FOLIO DEL CERTIFICADO
                        estado2 = con.leer_interno[23].ToString().Trim(); //ESTADO
                        MUNICIPIO2 = con.leer_interno[24].ToString().Trim(); //MUNICIPIO
                        ZONA2 = con.leer_interno[25].ToString().Trim(); //ZONA
                        MANZANA2 = con.leer_interno[26].ToString().Trim(); //MANZANA
                        LOTE2 = con.leer_interno[27].ToString().Trim(); //LOTE
                        EDIFICIO2 = con.leer_interno[28].ToString().Trim(); //EDIFICIO
                        DEPTO2 = con.leer_interno[29].ToString().Trim(); //DEPARTAMENTO
                        lblAñoPredio.Text = con.leer_interno[30].ToString().Trim();
                        lblMesPredio.Text = con.leer_interno[31].ToString().Trim();
                        lblSeriePredio.Text = con.leer_interno[32].ToString().Trim();
                        lblFolioPredio.Text = con.leer_interno[33].ToString().Trim();
                        IMPRIMIO_SI_NO = Convert.ToInt32(con.leer_interno[34].ToString().Trim()); //SI SE IMPRIMIO O NO 
                       
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

            ////LLENAREMOS LOS DATOS DEL PAGO DE PREDIO
            //try
            //{   // Aquí iría la lógica para realizar la consulta a la base de datos
            //    // Por ejemplo, podrías llamar a un método que realice la consulta y muestre los resultados en un DataGridView o similar.
            //    FOLIO = int.Parse(TXT_FOLIO.Text);
            //    SERIE = CBO_SERIE.Text.Trim();
            //    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //    /////// OBTENEMOS DATOS DEL FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
            //    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //    con.conectar_base_interno();
            //    con.cadena_sql_interno = "";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT TOP 1 P.UltAnioPag, P.UltMesPag, R.Serie, R.Folio  ";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "    FROM RECIBOS R, PROPIEDADES P ";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE R.Municipio = " + MUNICIPIO_M;
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.zona = " + ZONA_M;
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.manzana = " + MANZANA_M;
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.lote = " + LOTE_M;
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.edificio = " + util.scm(EDIFICIO_M);
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.depto = " + util.scm(DEPTO_M);
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Status = 'A'";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Municipio = P.Municipio";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Zona = P.Zona";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Manzana = P.Manzana";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Lote = P.Lote";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Edificio = P.Edificio";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Depto = P.Depto";
            //    con.cadena_sql_interno = con.cadena_sql_interno + "   ORDER BY R.nNoRecib DESC";


            //    //con.conectar_base_interno();
            //    //con.cadena_sql_interno = "";
            //    //con.cadena_sql_interno = "                 SELECT  UltAnioPag, UltMesPag, serie, folio  FROM recibos ";
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "Where  Municipio = " + MUNICIPIO_M;
            //    //con.cadena_sql_interno = con.cadena_sql_interno + " AND ZONA = " + ZONA_M;
            //    //con.cadena_sql_interno = con.cadena_sql_interno + " AND MANZANA = " + MANZANA_M;
            //    //con.cadena_sql_interno = con.cadena_sql_interno + " AND LOTE = " + LOTE_M;
            //    //con.cadena_sql_interno = con.cadena_sql_interno + " AND EDIFICIO = " + util.scm(EDIFICIO_M);
            //    //con.cadena_sql_interno = con.cadena_sql_interno + " AND DEPTO =  " + util.scm(DEPTO_M);
            //    //con.cadena_sql_interno = con.cadena_sql_interno + " AND SUERTE IN ( 'IMPUESTO PREDIAL', 'impuesto predial.', 'IMPUESTO PREDIAL REZAGO')";
            //    //con.cadena_sql_interno = con.cadena_sql_interno + " AND nNoRecib = (SELECT MAX(nNoRecib)";
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "                   FROM RECIBOS";
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "                  Where Municipio = " + MUNICIPIO_M;
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "                    AND ZONA = " + ZONA_M;
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "                    AND MANZANA = " + MANZANA_M;
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "                    AND LOTE = " + LOTE_M;
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "                    AND EDIFICIO = " + util.scm(EDIFICIO_M);
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "                    AND DEPTO =  " + util.scm(DEPTO_M);
            //    //con.cadena_sql_interno = con.cadena_sql_interno + "                    AND SUERTE IN ( 'IMPUESTO PREDIAL', 'impuesto predial.', 'IMPUESTO PREDIAL REZAGO'))";

            //    con.cadena_sql_cmd_interno();
            //    con.open_c_interno();
            //    con.leer_interno = con.cmd_interno.ExecuteReader();


            //    // Verificar si el resultado está vacío
            //    if (!con.leer_interno.HasRows)
            //    {

            //        MessageBox.Show("PAGO DE PREDIO NO ENCONTRADO", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        btnConsulta.Enabled = true;
            //        btnBuscar.Enabled = true;
            //        CBO_SERIE.Enabled = true;
            //        TXT_FOLIO.Enabled = true;
            //        limpiarCampos();
            //        return; // Retornar si no hay resultados
            //    }


            //    while (con.leer_interno.Read())
            //    {
            //        if (con.leer_interno[0].ToString().Trim() != "")
            //        {
            //            lblAñoPredio.Text = con.leer_interno[0].ToString().Trim();
            //            lblMesPredio.Text = con.leer_interno[1].ToString().Trim();
            //            lblSeriePredio.Text = con.leer_interno[2].ToString().Trim();
            //            lblFolioPredio.Text = con.leer_interno[3].ToString().Trim();
            //        }

            //    }
            //    con.cerrar_interno();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error al realizar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            //REVISAMOS SI ESTA PAGADO EL CERTITIICADO
            try
            {
                int verificar = 0; // Variable para almacenar el resultado de la consulta
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM RECIBOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE SERIE =" + util.scm(lblSerieCertificado.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND folio = " + lblFolioCertificado.Text;
                con.cadena_sql_interno = con.cadena_sql_interno + "            )";
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
                    lblOrdenPago.Text = "P";
                    lblOrdenPago.ForeColor = Color.GreenYellow;
                }
                else if (verificar == 2)
                {
                    lblOrdenPago.Text = "O";
                    lblOrdenPago.ForeColor = Color.Red;
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
            //comprobamos si el pago de predio esta actualizado
            predio();

            btnConsulta.Enabled = false;
            btnBuscar.Enabled = false;
            CBO_SERIE.Enabled = false;
            TXT_FOLIO.Enabled = false;
            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            btnImprimir.Enabled = true;
            btnActualizar.Enabled = true;
        }
        private void predio()
        {
            double pago_predial = 0.0;
            int mes = DateTime.Now.Month;
            int bimestre = 0;
            int mes_bimestre = 0;
            int mes_base = 0;

            if (mes > 1 && mes <= 2) bimestre = 1;
            if (mes > 2 && mes <= 4) bimestre = 2;
            if (mes > 4 && mes <= 6) bimestre = 3;
            if (mes > 6 && mes <= 8) bimestre = 4;
            if (mes > 8 && mes <= 10) bimestre = 5;
            if (mes > 10 && mes <= 12) bimestre = 6;
            if (lblMesPredio.Text.Trim() == "")
            {
                lblPredio.Text = "O";//NO PASA
                lblPredio.ForeColor = Color.Red;
            }
            else
            {
                mes_base = Convert.ToInt32(lblMesPredio.Text.Trim()); // Convertir el mes a un número entero

                if (mes_base > 1 && mes_base <= 2) mes_bimestre = 1;
                if (mes_base > 2 && mes_base <= 4) mes_bimestre = 2;
                if (mes_base > 4 && mes_base <= 6) mes_bimestre = 3;
                if (mes_base > 6 && mes_base <= 8) mes_bimestre = 4;
                if (mes_base > 8 && mes_base <= 10) mes_bimestre = 5;
                if (mes_base > 10 && mes_base <= 12) mes_bimestre = 6;

                if (lblAñoPredio.Text == Program.añoActual.ToString()) // Comparar el texto con "2025"
                {
                    if (mes_bimestre >= bimestre)
                    {
                        lblPredio.Text = "P";//SI PASA
                        lblPredio.ForeColor = Color.GreenYellow;
                    }
                    else
                    {
                        lblPredio.Text = "O";//NO PASA
                        lblPredio.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblPredio.Text = "O";//NO PASA
                    lblPredio.ForeColor = Color.Red;
                }
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
            if (txtDepto.Text.Length == 4) { TXT_FOLIO.Focus(); }
        }

        private void LOSTFOCUS(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = System.Drawing.Color.White;
            txt.Select(txt.Text.Length, 0);
        }

        private void GOTFOCUS(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = System.Drawing.Color.Yellow;
            txt.Select(txt.Text.Length, 0);
        }
        private void GOTFOCUS_CBO(object sender, EventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            txt.BackColor = System.Drawing.Color.Yellow;
            txt.Select(txt.Text.Length, 0);
        }

        private void LOSTFOCUS_CBO(object sender, EventArgs e)
        {
            ComboBox txt = (ComboBox)sender;
            txt.BackColor = System.Drawing.Color.White;
            txt.Select(txt.Text.Length, 0);
        }
    }
}
