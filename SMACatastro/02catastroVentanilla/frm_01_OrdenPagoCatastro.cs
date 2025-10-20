using AccesoBase;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Mysqlx.Crud;
using QRCoder;
using SMACatastro;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using USLibV4;
//using USLibV4.Addendas.HEB;
//using USLibV4.RetencionPagoV2.Complementos.PlanesRetiro11;
using Utilerias;
using ToolTip = System.Windows.Forms.ToolTip;

namespace SMAIngresos.Catastro
{
    public partial class frm_01_OrdenPagoCatastro : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();
        int zona, manzana, lote, folio, existe, folio_catastro, existeCart, existeAmbas, existePago, existeBloqueo = 0;
        string edificio, depto, serie = "";
        int validacion, validador, existe3en1 = 0;
        int mesActual = 0;
        int ValorCadena = 0;
        int predio, catastro, desarrollo, tipo_certi = 0;
        int valorCadena = 0;    
        public int NUM_ITEMS = 0;
        string fechaTexto = "";
        string fechaDD = "";
        string fechaMM = "";
        string fechaAA = "";
        string fechaTextof = "";
        string fechaDDf = "";
        string fechaMMf = "";
        string fechaAAf = "";
        string serieCatastro = "";
        string SERIEFACTURA = "";
        DateTime fechaff = DateTime.Today;
        double TERRENO1, TERRENO2, TERRENO3, TERRENO4, TERRENO5 = 0;
        double CONSTRUCCION1, CONSTRUCCION2, CONSTRUCCION3, CONSTRUCCION4, CONSTRUCCION5 = 0;
        int SELECCION_2, validacionPago = 0;
        int PAGO_PREDIAL,  FOLIO_FACTURA, AÑO_PAGO = 0;
        string idArea = "03";
        double valorCons = 0;
        double COSTO, COSTO2, COSTO3, COSTOTOTAL = 0;
        double UMA = 283;
        //revisar eso sub / total         
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();


        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public frm_01_OrdenPagoCatastro()
        {
            InitializeComponent();
        }
        private void frm_01_OrdenPagoCatastro_Activated(object sender, EventArgs e)
        {
            cmdNuevo.Focus();
        }
        private void frm_01_OrdenPagoCatastro_Load(object sender, EventArgs e)
        {
            limpiartodo();
            inhabilitarRfc();
            label27.Text = Program.nombre_usuario;
            cmdNuevo.Focus();
            cmdSalida.Enabled = true;
        }
        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void pnlDatosPredio_MouseDown(object sender, MouseEventArgs e)
        {

        }
        ////////////////////////////////////////////////
        ///////--------------TOOLTIP PARA LOS BOTONES 
        ////////////////////////////////////////////////
        private void cmdBuscar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(cmdNuevo, "NUEVO");
        }
        private void cmdCancela_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(cmdCancela, "CANCELAR");
        }

        private void cmdSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(cmdSalida, "SALIR");
        }
        /////////////////////////////////////////////////
        /////---SWITCH PARA LAS CAJAS AMARILLAS Y BLANCAS
        /////////////////////////////////////////////////
        void cajasamarillas(int ca)
        {
            switch (ca)
            {
                //cambiar a color amarillo las cajas de texto 
                case 1: txtFolioCatastro.BackColor = Color.Yellow; break;
                case 2: txtRfc.BackColor = Color.Yellow; break;
                case 3: txtPersonaMoral.BackColor = Color.Yellow; break;
                case 4: cboRegimenFiscal1.BackColor = Color.Yellow; break;
                case 5: cboUsoFiscal1.BackColor = Color.Yellow; break;
                case 6: txtInfComp.BackColor = Color.Yellow; break;
                case 7: txtZona.BackColor = Color.Yellow; break;
                case 8: txtManzana.BackColor = Color.Yellow; break;
                case 9: txtLote.BackColor = Color.Yellow; break;
                case 10: txtEdificio.BackColor = Color.Yellow; break;
                case 11: txtDepto.BackColor = Color.Yellow; break;
            }

        }
        void cajasblancas(int cb)
        {
            switch (cb)
            {
                //cambiar a color blanco las cajas de texto
                case 1: txtFolioCatastro.BackColor = Color.White; break;
                case 2: txtRfc.BackColor = Color.White; break;
                case 3: txtPersonaMoral.BackColor = Color.White; break;
                case 4: cboRegimenFiscal1.BackColor = Color.White; break;
                case 5: cboUsoFiscal1.BackColor = Color.White; break;
                case 6: txtInfComp.BackColor = Color.White; break;
                case 7: txtZona.BackColor = Color.White; break;
                case 8: txtManzana.BackColor = Color.White; break;
                case 9: txtLote.BackColor = Color.White; break;
                case 10: txtEdificio.BackColor = Color.White; break;
                case 11: txtDepto.BackColor = Color.White; break;

            }
        }
        ////////////////////////////////////////////////////////
        ///--ASIGNAR EL CASE DEL SWITCH PARA LOS COLORES EN LA CAJA 
        ////////////////////////////////////////////////////////
        private void txtFolioCatastro_Enter(object sender, EventArgs e)
        {
            cajasamarillas(1);
        }
        private void txtFolioCatastro_Leave(object sender, EventArgs e)
        {
            cajasblancas(1);
            cmdConsulta.Focus();
        }
        private void txtRfc_Enter(object sender, EventArgs e)
        {
            cajasamarillas(2);
        }
        private void txtRfc_Leave(object sender, EventArgs e)
        {
            cajasblancas(2);
        }
        private void txtPersonaMoral_Enter(object sender, EventArgs e)
        {
            cajasamarillas(3);
        }
        private void txtPersonaMoral_Leave(object sender, EventArgs e)
        {
            cajasblancas(3);
        }
        private void cboRegimenFiscal1_Enter(object sender, EventArgs e)
        {
            cajasamarillas(4);
        }
        private void cboRegimenFiscal1_Leave(object sender, EventArgs e)
        {
            cajasblancas(4);
        }
        private void cboUsoFiscal1_Enter(object sender, EventArgs e)
        {
            cajasamarillas(5);
        }
        private void cboUsoFiscal1_Leave(object sender, EventArgs e)
        {
            cajasblancas(5);
        }
        private void txtInfComp_Enter(object sender, EventArgs e)
        {
            cajasamarillas(6);
        }
        private void txtInfComp_Leave(object sender, EventArgs e)
        {
            cajasblancas(6);
        }
        private void txtZona_Enter(object sender, EventArgs e)
        {
            cajasamarillas(7);
        }
        private void txtZona_Leave(object sender, EventArgs e)
        {
            cajasblancas(7);
        }
        private void txtManzana_Enter(object sender, EventArgs e)
        {
            cajasamarillas(8);
        }
        private void txtManzana_Leave(object sender, EventArgs e)
        {
            cajasblancas(8);
        }

        private void txtLote_Enter(object sender, EventArgs e)
        {
            cajasamarillas(9);
        }

        private void txtLote_Leave(object sender, EventArgs e)
        {
            cajasblancas(9);
        }
        private void txtEdificio_Enter(object sender, EventArgs e)
        {
            cajasamarillas(10);
        }
        private void txtEdificio_Leave(object sender, EventArgs e)
        {
            cajasblancas(10);
        }
        private void txtDepto_Enter(object sender, EventArgs e)
        {
            cajasamarillas(11);
        }
        private void txtDepto_Leave(object sender, EventArgs e)
        {
            cajasblancas(11);
        }
        ///////
        //MÉTODO PARA QUE CUANDO SE CUMPLA LA CANTIDAD DE DIGITOS EN LA CAJA DE TEXTO, PASE A LA SIGUIENTE CAJA 
        ///////
        private void txtZona_TextChanged(object sender, EventArgs e)
        {
            if (txtZona.Text.Length == 2)
            {
                txtManzana.Focus();
            }
        }
        private void txtManzana_TextChanged(object sender, EventArgs e)
        {
            if (txtManzana.Text.Length == 3)
            {
                txtLote.Focus();
            }
        }
        private void txtLote_TextChanged(object sender, EventArgs e)
        {
            if (txtLote.Text.Length == 2)
            {
                txtEdificio.Focus();
            }
        }
        private void txtEdificio_TextChanged(object sender, EventArgs e)
        {
            if (txtEdificio.Text.Length == 2)
            {
                txtDepto.Focus();
            }
        }
        private void txtDepto_TextChanged(object sender, EventArgs e)
        {
            if (txtDepto.Text.Length == 4)
            {
                txtFolioCatastro.Focus();
            }
        }
        ///////////////////////////
        //-------------------ADMITIR SOLO LOS NÚMEROS DE LA CAJA DE TEXTO 
        //////////////////////////

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

        private void pnlDatosPredio_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtFolioCatastro_KeyPress(object sender, KeyPressEventArgs e)
        {
            /////////////////////Solo habilitar números al entrar y con el botón enter poder consultar
            util.soloNumero(e);
            if (e.KeyChar == (char)13)
            {
                Consulta();
            }

        }
        ////////////////////////////////////////
        //////----BOTONES MANDADOS A LOS MÉTODOS 
        ////////////////////////////////////////
        private void cmdIngresarLista_Click(object sender, EventArgs e)
        {
            costos();
        }

        private void cmdMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void cmdSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmdOrden_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE REALIZAR ESTA ÓRDEN DE PAGO?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo);
            if (resp == DialogResult.Yes)
            {
                //insertar y revisar el commit 
                Insertar();
            }
        }

        private void cmdCancela_Click(object sender, EventArgs e)
        {
            limpiartodo();
        }

        private void cmdConsulta_Click(object sender, EventArgs e)
        {
            Consulta();
        }
        private void cmdModificaLista_Click(object sender, EventArgs e)
        {

            double costoImporte = 0;
            double costoIva = 0;

            string sinIvaConIvaS2 = "0";
            int sinIvaConIvaI = 1;

            Program.totalDesglosado = 0;

            if (sinIvaConIvaI == 1)  // no tiene iva
            {
                if (lblConcepto.Items.Count > 1) //esta madre no funciona  
                {
                    lblConcepto.Items.Remove(NUM_ITEMS);


                    lblCosto.Items.RemoveAt(NUM_ITEMS);


                    lblsubTotal.Text = "0.0";
                    lblivatota.Text = "0.0";
                    lblTotal.Text = "0.0";
                }
            }

            if (sinIvaConIvaI == 2)  // si tiene iva
            {
                costoImporte = Convert.ToDouble(lblPrecioCosto.Text.Trim());
                costoIva = Convert.ToDouble(lblCosto.Items[NUM_ITEMS + 1].ToString());

                lblConcepto.Items.RemoveAt(NUM_ITEMS);
                lblConcepto.Items.RemoveAt(NUM_ITEMS);

                lblCosto.Items.RemoveAt(NUM_ITEMS);
                lblCosto.Items.RemoveAt(NUM_ITEMS);


                Program.subTotalDesglosado = 0;
                Program.ivaDesglosado = 0;

            }

            cmdIngresarLista.Enabled = false;
            //costoImporte = Convert.ToDouble(lblPrecioCosto.Text.Trim());
            costoIva = 0;
            lblConcepto.Items.Clear();
            lblCosto.Items.Clear();
            //revisr los valores de abajo 


            //Program.subTotalDesglosado = Program.subTotalDesglosado - costoImporte;
            //Program.ivaDesglosado = Program.ivaDesglosado - costoIva;

            Program.subTotalDesglosado = 0;
            Program.ivaDesglosado = 0;



            lblConceptoCobro.Text = "";
            lblPrecioCosto.Text = "";

            lblsubTotal.Text = string.Format("{0:#,0.00}", Program.subTotalDesglosado);
            lblivatota.Text = string.Format("{0:#,0.00}", Program.ivaDesglosado);
            lblTotal.Text = string.Format("{0:#,0.00}", (Program.subTotalDesglosado + Program.ivaDesglosado));

            lbxTipoServicio.Focus();

            cboCantidad.Enabled = false;
            lblPrecioCosto.Enabled = false;

            lbxTipoServicio.Enabled = true;
            lblConceptoCobro.Enabled = true;
            cboCantidad.Enabled = true;
            lblPrecioCosto.Enabled = true;
            cmdIngresarLista.Enabled = true;
            cmdModificaLista.Enabled = false;
            cmdCancelarLista.Enabled = false;
            lblConcepto.Enabled = true;
            lblCosto.Enabled = true;
            cmdOrden.Enabled = true;

            cmdModificaLista.Enabled = false;
            cmdCancelarLista.Enabled = false;


            cboCantidad.SelectedIndex = 0;
            Program.subTotalDesglosado = 0;
            Program.ivaDesglosado = 0;
        }

        private void lblConcepto_DoubleClick(object sender, EventArgs e)
        {
            //
            if (lblConcepto.Text == "")
            {
                MessageBox.Show("NECESITAS SELECCIONAR UN CONCEPTO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                /*
                if (lblConcepto.SelectedItem.ToString().Substring(14, 3) != "IVA")
                {
                    NUM_ITEMS = 0;
                    NUM_ITEMS = Convert.ToInt32(lblConcepto.SelectedIndex.ToString());
                    lblConceptoCobro.Text = lblConcepto.SelectedItem.ToString();
                    lblPrecioCosto.Text = lblCosto.Items[NUM_ITEMS].ToString();

                    lbxTipoServicio.Enabled = false;
                    lblConceptoCobro.Enabled = false;
                    cboCantidad.Enabled = false;
                    lblPrecioCosto.Enabled = false;
                    cmdIngresarLista.Enabled = false;
                    cmdModificaLista.Enabled = true;
                    cmdCancelarLista.Enabled = true;
                    lblConcepto.Enabled = false;
                    lblCosto.Enabled = false;
                    cmdOrden.Enabled = false;
                */
                }
            }
        


        private void cmdCancelarLista_Click(object sender, EventArgs e)
        {
            NUM_ITEMS = 0;
            lblConceptoCobro.Text = "";
            lblPrecioCosto.Text = "";

            lblPrecioCosto.Enabled = false;
            cboCantidad.Enabled = false;

            lbxTipoServicio.Enabled = true;
            lblConceptoCobro.Enabled = true;
            cboCantidad.Enabled = true;
            lblPrecioCosto.Enabled = true;
            cmdIngresarLista.Enabled = true;
            cmdModificaLista.Enabled = false;
            cmdCancelarLista.Enabled = false;
            lblConcepto.Enabled = true;
            lblCosto.Enabled = true;
            cmdOrden.Enabled = true;

            cmdModificaLista.Enabled = false;
            cmdCancelarLista.Enabled = false;
        }
        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss");
        }

        private void cmdBuscar_Click(object sender, EventArgs e)
        {
            inicio();
        }
        void inicio ()
        {
            cmdNuevo.Enabled = false;
            txtZona.Enabled = true;
            txtZona.Focus();
            txtManzana.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;
            txtFolioCatastro.Enabled = true;
            cmdConsulta.Enabled = true;
            cmdSalida.Enabled = false;
            llenarSerie();
        }

        void habilitarRfc()
        {
            txtRfc.Enabled = true;
            txtPersonaMoral.Enabled = true;
            cboRegimenFiscal1.Enabled = true;
            cboUsoFiscal1.Enabled = true;
        }
        void inhabilitarRfc()
        {
            txtRfc.Enabled = false;
            txtPersonaMoral.Enabled = false;
            cboRegimenFiscal1.Enabled = false;
            cboUsoFiscal1.Enabled = false;
        }
        void llenarCombosRFC()
        {
            cboRegimenFiscal1.Items.Clear();
            cboRegimenFiscal1.Items.Add("601 General de Ley Personas Morales");
            cboRegimenFiscal1.Items.Add("603 Personas Morales con Fines no Lucrativos");
            cboRegimenFiscal1.Items.Add("605 Sueldos y Salarios e Ingresos Asimilados a Salarios");
            cboRegimenFiscal1.Items.Add("606 Arrendamiento");
            cboRegimenFiscal1.Items.Add("607 Régimen de Enajenación o Adquisición de Bienes");
            cboRegimenFiscal1.Items.Add("608 Demás ingresos");
            cboRegimenFiscal1.Items.Add("610 Residentes en el Extranjero sin Establecimiento Permanente en México");
            cboRegimenFiscal1.Items.Add("611 Ingresos por Dividendos (socios y accionistas)");
            cboRegimenFiscal1.Items.Add("612 Personas Físicas con Actividades Empresariales y Profesionales");
            cboRegimenFiscal1.Items.Add("614 Ingresos por intereses");
            cboRegimenFiscal1.Items.Add("615 Régimen de los ingresos por obtención de premios");
            cboRegimenFiscal1.Items.Add("616 Sin obligaciones fiscales");
            cboRegimenFiscal1.Items.Add("620 Sociedades Cooperativas de Producción que optan por diferir sus ingresos");
            cboRegimenFiscal1.Items.Add("621 Incorporación Fiscal");
            cboRegimenFiscal1.Items.Add("622 Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras");
            cboRegimenFiscal1.Items.Add("623 Opcional para Grupos de Sociedades");
            cboRegimenFiscal1.Items.Add("624 Coordinados");
            cboRegimenFiscal1.Items.Add("625 Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas");
            cboRegimenFiscal1.Items.Add("626 Régimen Simplificado de Confianza");
            cboRegimenFiscal1.SelectedIndex = 0;

            cboUsoFiscal1.Items.Clear();
            cboUsoFiscal1.Items.Add("G01 Adquisición de mercancías.");
            cboUsoFiscal1.Items.Add("G02 Devoluciones, descuentos o bonificaciones.");
            cboUsoFiscal1.Items.Add("G03 Gastos en general.");
            cboUsoFiscal1.Items.Add("I01 Construcciones.");
            cboUsoFiscal1.Items.Add("I02 Mobiliario y equipo de oficina por inversiones.");
            cboUsoFiscal1.Items.Add("I03 Equipo de transporte.");
            cboUsoFiscal1.Items.Add("I04 Equipo de computo y accesorios.");
            cboUsoFiscal1.Items.Add("I05 Dados, troqueles, moldes, matrices y herramental.");
            cboUsoFiscal1.Items.Add("I06 Comunicaciones telefónicas.");
            cboUsoFiscal1.Items.Add("I07 Comunicaciones satelitales.");
            cboUsoFiscal1.Items.Add("I08 Otra maquinaria y equipo.");
            cboUsoFiscal1.Items.Add("D01 Honorarios médicos, dentales y gastos hospitalarios.");
            cboUsoFiscal1.Items.Add("D02 Gastos médicos por incapacidad o discapacidad.");
            cboUsoFiscal1.Items.Add("D03 Gastos funerales.");
            cboUsoFiscal1.Items.Add("D04 Donativos.");
            cboUsoFiscal1.Items.Add("D05 Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación).");
            cboUsoFiscal1.Items.Add("D06 Aportaciones voluntarias al SAR.");
            cboUsoFiscal1.Items.Add("D07 Primas por seguros de gastos médicos.");
            cboUsoFiscal1.Items.Add("D08 Gastos de transportación escolar obligatoria.");
            cboUsoFiscal1.Items.Add("D09 Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones.");
            cboUsoFiscal1.Items.Add("D10 Pagos por servicios educativos (colegiaturas).");
            cboUsoFiscal1.Items.Add("S01 Sin efectos fiscales.");
            cboUsoFiscal1.Items.Add("CP01 Pagos");
            cboUsoFiscal1.Items.Add("CN01 Nómina");
            cboUsoFiscal1.SelectedIndex = 2;


        }
        void limpiartodo()
        {
            //limpiar los textbox de arriba

            cmdNuevo.Enabled = true;
            cmdSalida.Enabled = true;
            cbo_serie.Text = string.Empty;
            txtZona.Text = string.Empty;
            txtManzana.Text = string.Empty;
            txtLote.Text = string.Empty;
            txtEdificio.Text = string.Empty;
            txtDepto.Text = string.Empty;
            txtZona.Focus();


            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio .Enabled = false;
            txtDepto .Enabled = false;
            cmdConsulta.Enabled = false;

            txtFolioCatastro.Text = string.Empty;

            //limpiamos todos los label 


            lblTitular.Text = string.Empty;
            lblColonia.Text = string.Empty;
            lblCalle.Text = string.Empty;
            lblSupTerrPriv.Text = string.Empty;
            lblSupConsPriv.Text = string.Empty;
            lblSupTerrComun.Text = string.Empty;
            lblSupConsCom.Text = string.Empty;
            lblValTerrPriv.Text = string.Empty;
            lblValTerrCom.Text = string.Empty;
            lblValorConsPriv.Text = string.Empty;
            lblValConsCom.Text = string.Empty;
            lblValTotTerr.Text = string.Empty;
            lblValTotCons.Text = string.Empty;
            lblValor.Text = string.Empty;
            lblFechaCaduca.Text = string.Empty;
            lblObservaciones.Text = string.Empty;
            //lblFechaActual.Text = string.Empty; ---- tal vez este no se deba limpiar
            lblConcepto.Text = string.Empty;
            lblCosto.Text = string.Empty;
            txtInfComp.Text = "";
            txtInfComp.Enabled = false;



            //panel de datos fiscales
            chkSioNoRfc.Checked = false;
            txtRfc.Text = string.Empty;
            txtRfc.Enabled = false;
            txtPersonaMoral.Text = string.Empty;
            txtPersonaMoral.Enabled = false;
            cboRegimenFiscal1.Items.Clear();
            cboRegimenFiscal1.Enabled = false;
            cboUsoFiscal1.Items.Clear();
            cboUsoFiscal1.Enabled = false;
            cbo_serie.SelectedIndex = -1;

            cmdIngresarLista.Enabled = false;
            cmdModificaLista.Enabled = false;
            cmdCancelarLista.Enabled = false;
            //limpiar los label list
            lbxTipoServicio.Enabled = false;
            lbxTipoServicio.Text = "";
            lblConceptoCobro.Text = "";
            cboCantidad.Items.Clear();

            cboCantidad.Items.Clear();
            lblPrecioCosto.Text = "";

            lblConcepto.Items.Clear();
            lblConcepto.Enabled = false;
            lblCosto.Items.Clear();

            lbxTipoServicio.Items.Clear();
            lblConcepto.Items.Clear();
            COSTO = 0;
            COSTO2 = 0;
            COSTO3  = 0;

            lblsubTotal.Text = "";
            lblivatota.Text = "";
            lblTotal.Text = "";
            SELECCION_2 = 0;
        }
        void llenarCombos()
        {

            DateTime fecha = DateTime.Today;

            string fechaTexto = "";
            string fechaDD = "";
            string fechaMM = "";
            string fechaAA = "";
            fechaTexto = fecha.ToString();
            fechaDD = fechaTexto.Trim().Substring(0, 2);
            fechaMM = fechaTexto.Trim().Substring(3, 2);
            fechaAA = fechaTexto.Trim().Substring(6, 4);

            mesActual = System.DateTime.Today.Month;

            DateTime date = DateTime.Now;                                                   //Primero obtenemos el día actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);             //Asi obtenemos el primer dia del mes actual
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);    //Y de la siguiente forma obtenemos el ultimo dia del mes

            fechaTextof = oUltimoDiaDelMes.ToString();
            fechaDD = fechaTextof.Trim().Substring(0, 2);
            fechaMM = fechaTextof.Trim().Substring(3, 2);
            fechaAA = fechaTextof.Trim().Substring(6, 4);
            fechaTextof = fechaDD + "/" + fechaMM + "/" + fechaAA;

            lblFechaCaduca.Text = fechaTextof;

            cboCantidad.Items.Clear();
            cboCantidad.Items.Add("1");
            cboCantidad.Items.Add("2");
            cboCantidad.Items.Add("3");
            cboCantidad.Items.Add("4");
            cboCantidad.Items.Add("5");
            cboCantidad.Items.Add("6");
            cboCantidad.Items.Add("7");
            cboCantidad.Items.Add("8");
            cboCantidad.Items.Add("9");
            cboCantidad.Items.Add("10");
            cboCantidad.Items.Add("11");
            cboCantidad.Items.Add("12");
            cboCantidad.Items.Add("13");
            cboCantidad.Items.Add("14");
            cboCantidad.Items.Add("15");
            cboCantidad.Items.Add("16");
            cboCantidad.Items.Add("17");
            cboCantidad.Items.Add("18");
            cboCantidad.Items.Add("19");
            cboCantidad.Items.Add("20");
            cboCantidad.SelectedIndex = 0;

        }
        void borrarRfc()
        {
            txtRfc.Text = "";
            txtPersonaMoral.Text = "";
            cboRegimenFiscal1.Items.Clear();
            cboUsoFiscal1.Items.Clear();
        }
        void llenarcboConcepto()
        {
            lbxTipoServicio.Enabled = true;
            lbxTipoServicio.Items.Clear();
            lbxTipoServicio.Items.Add("800101 -- CERTIFICADO CLAVE Y VALOR CATASTRAL -- CATASTRO");
            lbxTipoServicio.Items.Add("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            lbxTipoServicio.Items.Add("800102 -- CERTIFICADO DE APORTACION A MEJORAS -- CATASTRO");
            lbxTipoServicio.Items.Add("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            lbxTipoServicio.Items.Add("800104 -- CERTIFICADO CLAVE Y VALOR CATASTRAL / APORTACION A MEJORAS -- CATASTRO");
            lbxTipoServicio.Items.Add("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            lbxTipoServicio.Items.Add("800105 -- CERTIFICADO CLAVE Y VALOR CATASTRAL / APORTACION A MEJORAS / NO ADEUDO PREDIAL -- CATASTRO");
            lbxTipoServicio.Items.Add("-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        void llenarSerie()
        {
            cbo_serie.Items.Clear();
            con.conectar_base_interno();
            con.open_c_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT SERIE ";
            con.cadena_sql_interno = con.cadena_sql_interno + "     FROM RECIBOS ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE FECCOB > '20250101'  ";
            con.cadena_sql_interno = con.cadena_sql_interno + " GROUP BY SERIE ";
            con.cadena_sql_cmd_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                cbo_serie.Items.Add(con.leer_interno[0].ToString().Trim());
            }
            cbo_serie.SelectedIndex = 0;

            //cerrar la conexión
            con.cerrar_interno();
        }

        void Consulta()
        {

            if (txtZona.Text == "")          { MessageBox.Show("NO SE TIENE LA ZONA", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZona.Focus(); return; }
            if (txtManzana.Text == "")       { MessageBox.Show("NO SE TIENE LA MANZANA", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); txtManzana.Focus(); return; }
            if (txtLote.Text == "")          { MessageBox.Show("NO SE TIENE EL LOTE", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLote.Focus(); return; }
            if (txtEdificio.Text == "")      { MessageBox.Show("NO SE TIENE EL EDIFICIO", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); txtEdificio.Focus(); return; }
            if (txtDepto.Text == "")         { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDepto.Focus(); return; }
            if (cbo_serie.Text == "")        { MessageBox.Show("NO SE TIENE LA SERIE", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); cbo_serie.Focus(); return; }
            if (txtFolioCatastro.Text == "") { MessageBox.Show("NO SE TIENE EL FOLIO", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); txtFolioCatastro.Focus(); return; }

            zona = Convert.ToInt32(txtZona.Text.Trim());
            manzana = Convert.ToInt32(txtManzana.Text.Trim());
            lote = Convert.ToInt32(txtLote.Text.Trim());
            edificio = txtEdificio.Text.Trim();
            depto = txtDepto.Text.Trim();
            folio_catastro = Convert.ToInt32(txtFolioCatastro.Text.Trim());

            
            //////////////////////////////////
            //----VALIDAR SI ESTÁ BLOQUEADO
            //////////////////////////////////
            con.conectar_base_interno();
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS ";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ( ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT  ZONA";
            con.cadena_sql_interno = con.cadena_sql_interno + "    FROM BLOQCVE_2 ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE ESTADO = " + Program.PEstado;
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND MUNICIPIO = " + Program.municipioN;
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND ZONA      = " + zona;
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND MANZANA   = " + manzana;
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND LOTE      = " + lote;
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND EDIFICIO  = '" + edificio + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND DEPTO     = '" + depto + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    )";
            con.cadena_sql_interno = con.cadena_sql_interno + "    BEGIN ";
            con.cadena_sql_interno = con.cadena_sql_interno + "     SELECT EXISTE = 1 ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    END  ";
            con.cadena_sql_interno = con.cadena_sql_interno + "     ELSE  ";
            con.cadena_sql_interno = con.cadena_sql_interno + "       BEGIN ";
            con.cadena_sql_interno = con.cadena_sql_interno + "          SELECT EXISTE = 0 ";
            con.cadena_sql_interno = con.cadena_sql_interno + "     END  ";
            con.open_c_interno();
            con.cadena_sql_cmd_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                existeBloqueo = Convert.ToInt32(con.leer_interno[0].ToString());
            }
            //cerrar la conexión
            con.cerrar_interno();

            //////////////////////
            //----VALIDAR SI EXISTE EN CARTOGRAFÍA 
            ///////////////////


            if (existeBloqueo == 0)
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    IF EXISTS (SELECT ZONA ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM CAT_NEW_CARTOGRAFIA_2025 ";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE ESTADO = " + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND MUNICIPIO = " + Program.municipioN;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND ZONA      = " + zona;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND MANZANA   = " + manzana;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND LOTE      = " + lote;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO  = '" + edificio + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO     = '" + depto + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND FOLIO_ORIGEN = " + folio_catastro;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND TAMAÑO = 'U'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND SERIE     = '" + Program.serie + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND UBICACION IN (3, 5) ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   )";
                con.cadena_sql_interno = con.cadena_sql_interno + "   BEGIN ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SELECT EXISTE = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   END  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ELSE  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "      BEGIN ";
                con.cadena_sql_interno = con.cadena_sql_interno + "          SELECT EXISTE = 2 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    END  ";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    existeCart = Convert.ToInt32(con.leer_interno[0].ToString());
                }

                //cerrar la conexión
                con.cerrar_interno();
            }
            else
            {
                MessageBox.Show("LA CLAVE CATASTRAL SE ENCUENTRA BLOQUEADA POR EL DEPARTAMENTO DE CATASTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            //////////////////////
            //----VALIDAR SI EXISTE EN CARTOGRAFÍA Y EN VENTANILLA
            ///////////////////
            if (existeCart == 1)
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    IF EXISTS ( ";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT ZONA ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM CAT_NEW_CARTOGRAFIA_2025 CN, CAT_DONDE_VA_2025 CD ";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE cn.ESTADO = " + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND cn.MUNICIPIO = " + Program.municipioN;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.ZONA      = " + zona;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.MANZANA   = " + manzana;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.LOTE      = " + lote;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.EDIFICIO  = '" + edificio + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.DEPTO     = '" + depto + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.FOLIO_ORIGEN = " + folio_catastro;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.TAMAÑO = 'U'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.SERIE     = '" + Program.serie + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.UBICACION IN (3, 5) ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CD.VENTANILLA = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.SERIE = CD.SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND CN.FOLIO_ORIGEN = CD.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "   )";
                con.cadena_sql_interno = con.cadena_sql_interno + "   BEGIN ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SELECT EXISTE = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   END  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ELSE  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN ";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT EXISTE = 2 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    END  ";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    existeAmbas = Convert.ToInt32(con.leer_interno[0].ToString());
                }
                //cerrar
                con.cerrar_interno();
            }
            else
            {
                MessageBox.Show("NO EXISTE EL FOLIO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                limpiartodo();
                inicio(); //DUDA SI DEJAMOS ESTA CAJA AQUÍ PARA VER 
                return;
            }
            /////////////////////////////////////////////////////////////////
            //---------------------------------VALIDAR SI YA EXISTE EL PAGOS
            /////////////////////////////////////////////////////////////////
            if (existeAmbas == 1)
            {

                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS ";
                con.cadena_sql_interno = con.cadena_sql_interno + " ( ";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT ZONA";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM TRES_EN_UNO_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE ESTADO = " + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND MUNICIPIO = " +Program.municipioN;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND ZONA      = " + zona;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND MANZANA   = " + manzana;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND LOTE      = " + lote;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND EDIFICIO  = '" + edificio + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND DEPTO     = '" + depto + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FOLIO = " + folio_catastro;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND STATUS_CATASTRO_PAGO = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "  )";
                con.cadena_sql_interno = con.cadena_sql_interno + "  BEGIN ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT PAGO = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "  END ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   ELSE ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    BEGIN ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     SELECT PAGO = 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "    END";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    existePago = Convert.ToInt32(con.leer_interno[0].ToString());
                }

                //cerrar
                con.cerrar_interno();
            }
            else
            {
                MessageBox.Show("EL FOLIO NO ESTÁ AUTORIZADO POR VENTANILLA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiartodo();
                inicio();
                return;
            }
            //////////////////////////////////////////////////////////////////////////////////////
            //////----EN CASO DE NO TENER EL PAGO, GENERAR LA CONSULTA AL PROCEDIMIENTO ALMACENADO 
            //////////////////////////////////////////////////////////////////////////////////////
                        
            if (existePago == 0) //no existe el pago  
            {
                con.conectar_base_interno();
                con.open_c_interno();
                ////cambiar por el otro procedimiento almacenado 
                ///
                SqlCommand cmd = new SqlCommand("N19_CONSULTA_PREDIO", con.cnn_interno); //nombre del procedimiento almacenado que vamos a utilizar
                cmd.CommandType = CommandType.StoredProcedure; //Se le indica que es un procedimiento almacenado
 
                cmd.Parameters.Add("@estado2", SqlDbType.VarChar, 2).Value = 15;
                cmd.Parameters.Add("@municipio2", SqlDbType.Int, 3).Value = 41;
                cmd.Parameters.Add("@zona2", SqlDbType.Int, 2).Value = zona;
                cmd.Parameters.Add("@manzana2", SqlDbType.Int, 3).Value = manzana;
                cmd.Parameters.Add("@lote2", SqlDbType.Int, 2).Value = lote;
                cmd.Parameters.Add("@edificio2", SqlDbType.Char, 2).Value = edificio;
                cmd.Parameters.Add("@depto2", SqlDbType.Char, 4).Value = depto;
                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    lblTitular.Text = rdr["PMM_PROP"].ToString().Trim();
                    lblCalle.Text = rdr["Domicilio"].ToString().Trim() + "   N° " + rdr["NUM_INTP"].ToString().Trim() + "   N° Ext " + rdr["NUM_EXT"].ToString().Trim();
                    lblColonia.Text = rdr["Colonia"].ToString().Trim();
                    //
                    if (lblValTerrPriv.Text.Trim() == "") { lblValTerrPriv.Text = "0.0"; }
                    if (lblValorConsPriv.Text.Trim() == "") { lblValorConsPriv.Text = "0.0"; }

                    if (lblValTerrCom.Text.Trim() == "") { lblValTerrCom.Text = "0.0"; }
                    if (lblValConsCom.Text.Trim() == "") { lblValConsCom.Text = "0.0"; }

                    if (lblValTotTerr.Text.Trim() == "") { lblValTotTerr.Text = "0.0"; }
                    if (lblValTotCons.Text.Trim() == "") { lblValTotCons.Text = "0.0"; }

                    if (lblValTotTerr.Text.Trim() == "") { lblValTotTerr.Text = "0.0"; }
                    if (lblValTotCons.Text.Trim() == "") { lblValTotCons.Text = "0.0"; }

                    TERRENO1 = Convert.ToDouble(rdr["SUP_TERR_TOT"].ToString().Trim());
                    TERRENO2 = Convert.ToDouble(rdr["SUP_TERR_COM"].ToString().Trim());

                    TERRENO3 = Convert.ToDouble(rdr["VALOR_TERRENO_P"].ToString().Trim());
                    TERRENO4 = Convert.ToDouble(rdr["VALOR_TERRENO_C"].ToString().Trim());

                    TERRENO5 = TERRENO3 + TERRENO4;
                    CONSTRUCCION1 = Convert.ToDouble(rdr["SUP_CONS"].ToString().Trim()); //SUP CONS 
                    CONSTRUCCION2 = Convert.ToDouble(rdr["SUP_CONS_COM"].ToString().Trim()); //SUP CONS COM
                    CONSTRUCCION3 = Convert.ToDouble(rdr["VALOR_CONSTRUCCION_P"].ToString().Trim()); //VALOR CONS P
                    CONSTRUCCION4 = Convert.ToDouble(rdr["VALOR_CONSTRUCCION_C"].ToString().Trim()); //VALOR CONS C
                    CONSTRUCCION5 = CONSTRUCCION3 + CONSTRUCCION4;

                    lblSupTerrPriv.Text = String.Format("{0:#,##0.00}", TERRENO1);
                    lblSupTerrComun.Text = String.Format("{0:#,##0.00}", TERRENO2);
                    lblValTerrPriv.Text = String.Format("{0:#,##0.00}", TERRENO3);
                    lblValTerrCom.Text = String.Format("{0:#,##0.00}", TERRENO4);
                    lblValTotTerr.Text = String.Format("{0:#,##0.00}", TERRENO5);


                    lblValTotCons.Text = String.Format("{0:#,##0.00}", CONSTRUCCION5);


                    lblSupConsPriv.Text = rdr["SUP_CONS"].ToString().Trim();
                    lblSupConsCom.Text = rdr["SUP_CONS_COM"].ToString().Trim();


                    lblValorConsPriv.Text = rdr["VALOR_CONSTRUCCION_P"].ToString().Trim();
                    lblValConsCom.Text = rdr["VALOR_CONSTRUCCION_C"].ToString().Trim();

                    lblSupConsPriv.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblSupConsPriv.Text.Trim()));
                    lblSupConsCom.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblSupConsCom.Text.Trim()));


                    lblValorConsPriv.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValorConsPriv.Text.Trim()));
                    lblValConsCom.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValConsCom.Text.Trim()));
                    lblValor.Text = rdr["VALOR_CATASTRAL"].ToString().Trim();
                    lblValor.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValor.Text.Trim()));

                    lblObservaciones.Text = rdr["C_OBS_PROP"].ToString().Trim().ToUpper();
                    lblCalle.Text = rdr["DOM_FIS"].ToString().Trim().ToUpper();
                    txtInfComp.Focus();
                    
                }
                //cerrar la conexión
                
                con.cerrar_interno();

                double valor_terreno_m;
                double valor_terreno_comun_m;
                double valor_construccion_m;
                double valor_COMUN_m;

                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd2= new SqlCommand("songCalculoValorCat", con.cnn_interno);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = 15;
                cmd2.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 3).Value = Convert.ToInt32(lblMunicipio.Text.Trim());
                cmd2.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = Convert.ToInt32(txtZona.Text.Trim());
                cmd2.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = Convert.ToInt32(txtManzana.Text.Trim());
                cmd2.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = Convert.ToInt32(txtLote.Text.Trim());
                cmd2.Parameters.Add("@EDIFICIO", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
                cmd2.Parameters.Add("@DEPTO", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();
                cmd2.Parameters.Add("@AÑO", SqlDbType.Int, 4).Value = Program.añoActual;

                cmd2.Parameters.Add("@valorTerrenoPropio", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd2.Parameters.Add("@valorTerrenoComun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd2.Parameters.Add("@valorConstruccion", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd2.Parameters.Add("@valorComun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd2.Parameters.Add("@valorCatastral", SqlDbType.Float, 9).Direction = ParameterDirection.Output;

                cmd2.Connection = con.cnn_interno;
                cmd2.ExecuteNonQuery();

                valor_terreno_m = Convert.ToDouble(cmd2.Parameters["@valorTerrenoPropio"].Value);
                valor_terreno_comun_m = Convert.ToDouble(cmd2.Parameters["@valorTerrenoComun"].Value);
                valor_construccion_m = Convert.ToDouble(cmd2.Parameters["@valorConstruccion"].Value);
                valor_COMUN_m = Convert.ToDouble(cmd2.Parameters["@valorComun"].Value);

                con.cerrar_interno();

                lblValTerrPriv.Text = valor_terreno_m.ToString("N2");
                lblValTerrCom.Text = valor_terreno_comun_m.ToString("N2");
                lblValorConsPriv.Text = valor_construccion_m.ToString("N2");
                lblValConsCom.Text = valor_COMUN_m.ToString("N2");


            }
            else
            {
                //CERRAR CONEXIÓON
                con.cerrar_interno();
                MessageBox.Show("YA ESTÁ GENERADO PREVIAMENTE EL PROCESO", "¡INFORMACIÓN!");
                limpiartodo();
                inicio();
                return;
            }
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            //txtFolioCatastro.Enabled = true;
            txtFolioCatastro.Enabled = false;

            pnlDatosPredio.Enabled = true;
            txtInfComp.Enabled = true;
            txtInfComp.Focus();

            pnlCobro.Enabled = true;
            pnlCobroTotal.Enabled = true;
            cmdConsulta.Enabled = false;

            llenarCombos();
            llenarcboConcepto();

            //con.conectar_base_interno();
            //con.cadena_sql_interno = "";
            //con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT P.UltAnioPag, P.UltMesPag, R.Serie, R.Folio  ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "    FROM RECIBOS R, PROPIEDADES P ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE R.Municipio = " + Program.municipioN;
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.zona = " + zona;
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.manzana = " + manzana;
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.lote = " + lote;
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.edificio = " + util.scm(edificio);
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.depto = " + util.scm(depto);
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Status In ('A', 'E')  ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Municipio = P.Municipio ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Zona = P.Zona ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Manzana = P.Manzana ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Lote = P.Lote ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Edificio = P.Edificio ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "     AND R.Depto = P.Depto ";
            //con.cadena_sql_interno = con.cadena_sql_interno + "ORDER BY R.nNoRecib DESC";


            //con.cadena_sql_cmd_interno();
            //con.open_c_interno();
            //con.leer_interno = con.cmd_interno.ExecuteReader();
            //if (!con.leer_interno.HasRows)
            //{
            //    MessageBox.Show("NO SE ENCONTRO REGISTRO DE PAGO CON EL NUMERO DE ORDEN INGRESADO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    con.cerrar_interno();
            //    return; // Retornar si no hay resultados
            //}
            //while (con.leer_interno.Read())
            //{
            //    AÑO_PAGO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
            //    PAGO_PREDIAL = Convert.ToInt32(con.leer_interno[1].ToString().Trim()); //variable de hugo 
            //    SERIEFACTURA = con.leer_interno[2].ToString().Trim();
            //    FOLIO_FACTURA = Convert.ToInt32(con.leer_interno[3].ToString().Trim());
            //}

            ////cerrar la conexión
            //con.cerrar_interno();


            ////aquí es lo de charly metodo de pagar 
            //double pago_predial = 0.0;
            //int mes = DateTime.Now.Month;
            //int bimestre = 0;
            //int mes_bimestre = 0;
            //int mes_base = 0;

            //if (mes > 1 && mes <= 2) bimestre = 1;
            //if (mes > 2 && mes <= 4) bimestre = 2;
            //if (mes > 4 && mes <= 6) bimestre = 3;
            //if (mes > 6 && mes <= 8) bimestre = 4;
            //if (mes > 8 && mes <= 10) bimestre = 5;
            //if (mes > 10 && mes <= 12) bimestre = 6;

            //mes_base = PAGO_PREDIAL;// Convertir el mes a un número entero

            //if (mes_base > 1 && mes_base <= 2) mes_bimestre = 1;
            //if (mes_base > 2 && mes_base <= 4) mes_bimestre = 2;
            //if (mes_base > 4 && mes_base <= 6) mes_bimestre = 3;
            //if (mes_base > 6 && mes_base <= 8) mes_bimestre = 4;
            //if (mes_base > 8 && mes_base <= 10) mes_bimestre = 5;
            //if (mes_base > 10 && mes_base <= 12) mes_bimestre = 6;

            //if (AÑO_PAGO == Program.añoActual) // REVISAR AÑO, SE CORRIGIO POR EL LABEL 
            //{
            //    if (mes_bimestre >= bimestre)
            //    {
            //        validacionPago = 1; //se puede imprimir 
            //    }
            //    else
            //    {
            //        validacionPago = 0;
            //    }
            //}
            //else
            //{
            //    validacionPago = 0;
            //}
        }

            //limpiartodo(); return;
        
        
        //////////////////////
        //----MÉTODO PARA GUARDAR // GENERAR LOS INSERTS 
        ///////////////////
        void Insertar()
        {

            /*
            validar lo de la fecha del pago del 
             */

            int varaa = 0;
            int varbb = 0;
            int folioInicial = 0;
            int folioFinal = 0;

            int ultimoFolio = 0;
            int vNoOrden = 0;
            int vFolioInicial = 0;
            int vFolioFinal = 0;
            int vUltimoFolio = 0;


            String RFC1 = "";
            String PFM = "";
            String RF = "";
            String UF = "";
            int folioFinalOrden = 0;
            int ultimoFolioOcupado = 0;
            ////QUITAR ESTO 
            if (lblTitular.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL CIUDADANO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (lblCalle.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DOMICILIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (lblColonia.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR LA COLONIA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (lblFechaCaduca.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR LA FECHA DE CADUCIDAD", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (lblTotal.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL TOTAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            if (chkSioNoRfc.Checked == true)
            {
                if (txtRfc.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL RFC", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtRfc.Focus(); return; }
                if (txtRfc.Text.Trim().Length < 12) { MessageBox.Show("EL RFC DEBE DE TENER 12 CARACTERES", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtRfc.Focus(); return; }
                if (txtPersonaMoral.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR PERSONA FISICA O MORAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtPersonaMoral.Focus(); return; }
                if (cboRegimenFiscal1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL REGIMEN FISCAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); cboRegimenFiscal1.Focus(); return; }
                if (cboUsoFiscal1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL USO FISCAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); cboUsoFiscal1.Focus(); return; }

                RFC1 = txtRfc.Text.Trim();
                PFM = txtPersonaMoral.Text.Trim();
                RF = cboRegimenFiscal1.Text.Trim();
                UF = cboUsoFiscal1.Text.Trim();
            }
            else
            {
                RFC1 = "XAXX010101000";
                PFM = "PÚBLICO EN GENERAL";
                RF = "616 Sin obligaciones fiscales";
                UF = "S01 Sin efectos fiscales.";
            }

            if (txtInfComp.Text == "") { MessageBox.Show("NO SE TIENE LA INFORMACIÓN COMPLEMENTARIA", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); txtInfComp.Focus(); return; }
            if (txtInfComp.Text.Length < 5) { MessageBox.Show("DEBES DE TENER MÁS INFORMACIÓN PARA LA INFORMACIÓN COMPLEMENTARIA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information); txtInfComp.Focus(); return; }
            if (lblTotal.Text == "") { MessageBox.Show("NO SE PUEDE GENERAR UNA ÓRDEN DE PAGO SIN CONCEPTOS", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error); lbxTipoServicio.Focus(); return; }
            if (lblConcepto.Items.Count == 0) { MessageBox.Show("NO SE PUEDE GENERAR UNA ÓRDEN DE PAGO SIN CONCEPTOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); lbxTipoServicio.Focus(); return; } //VALIDACIÓN



            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ OBTENERMOS LA CLAVE CATASTRAL
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            int ESTADO = 15;
            int MUNICIPIO = 041;
            string clave_catastral = ESTADO + "-" + MUNICIPIO + "-" + txtZona.Text.Trim() + "-" + txtManzana.Text.Trim() + "-" + txtLote.Text.Trim() + "-" + txtEdificio.Text.Trim() + "-" + txtDepto.Text.Trim();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ OBTENERMOS EL TIPO DE CERTIFICADO QUE SE VA A REALIZAR SEGÚN LA OPCIÓN SELECCIONADA 
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            int tipo_certi = 0; //INICIO
            if (lbxTipoServicio.SelectedIndex == 0)
            {
                tipo_certi = 5; //CLAVE VALOR
            }
            else if (lbxTipoServicio.SelectedIndex == 2)
            {
                tipo_certi = 2; ///APORTACION A MEJORAS
            }
            else if (lbxTipoServicio.SelectedIndex == 4)
            {
                tipo_certi = 6; //CLAVE VALOR / APORTACION A MEJORAS
            }
            else if (lbxTipoServicio.SelectedIndex == 6)
            {
                tipo_certi = 8; //3 en 1
                //if (validacionPago != 1)
                //{
                //    MessageBox.Show("NO SE PUEDE IMPRIMIR LA ÓRDEN DE PAGO DEBIDO A QUE EL PAGO PREDIAL NO SE ENCUENTRA AL CORRIENTE ", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
            }
            if( tipo_certi == 8)
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT UltAnioPag, UltMesPag ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM PROPIEDADES P";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE Municipio = " + Program.municipioN;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND zona = " + zona;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND manzana = " + manzana;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND lote = " + lote;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND edificio = " + util.scm(edificio);
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND depto = " + util.scm(depto);

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                
                while (con.leer_interno.Read())
                {
                    AÑO_PAGO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    PAGO_PREDIAL = Convert.ToInt32(con.leer_interno[1].ToString().Trim()); //variable de hugo 
                   
                }

                //cerrar la conexión
                con.cerrar_interno();


                //aquí es lo de charly metodo de pagar 
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

                mes_base = PAGO_PREDIAL;// Convertir el mes a un número entero

                if (mes_base > 1 && mes_base <= 2) mes_bimestre = 1;
                if (mes_base > 2 && mes_base <= 4) mes_bimestre = 2;
                if (mes_base > 4 && mes_base <= 6) mes_bimestre = 3;
                if (mes_base > 6 && mes_base <= 8) mes_bimestre = 4;
                if (mes_base > 8 && mes_base <= 10) mes_bimestre = 5;
                if (mes_base > 10 && mes_base <= 12) mes_bimestre = 6;

                if (AÑO_PAGO == Program.añoActual) // REVISAR AÑO, SE CORRIGIO POR EL LABEL 
                {
                    if (mes_bimestre >= bimestre)
                    {
                        validacionPago = 1; //se puede imprimir 
                    }
                    else
                    {
                        validacionPago = 0;
                    }
                }
                else
                {
                    validacionPago = 0;
                }
                if(validacionPago != 1)
                {
                    MessageBox.Show("NO SE PUEDE IMPRIMIR LA ÓRDEN DE PAGO DEL CERTIFICADO 3 EN 1, DEBIDO A QUE EL PAGO PREDIAL NO SE ENCUENTRA AL CORRIENTE ", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT serie, folio ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    FROM RECIBOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE Municipio = " + Program.municipioN;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND zona = " + zona;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND manzana = " + manzana;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND lote = " + lote;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND edificio = " + util.scm(edificio);
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND depto = " + util.scm(depto);
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND Serie = " + util.scm(Program.serie);
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND UltAnioPag = " + AÑO_PAGO;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND UltMesPag = " + PAGO_PREDIAL;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND Suerte IN('impuesto predial.', 'IMPUESTO PREDIAL')" ;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND Status IN('A', 'E')";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     ORDER BY nNoRecib DESC " ;

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        SERIEFACTURA = con.leer_interno[0].ToString().Trim();
                        FOLIO_FACTURA = Convert.ToInt32(con.leer_interno[1].ToString().Trim()); //variable de hugo 

                    }

                    //cerrar la conexión
                    con.cerrar_interno();
                    
                }
            }
            //VALIDAMOS QUE NO SE MANDEN DATOS VACÍOS

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ OBTENER LA FECHA 
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string fechaIngreso = "";
            fechaIngreso = DateTime.Now.ToString("O");
            string fechaIngresos = fechaIngreso.Trim().Substring(0, 10);
            string fechasHora = fechaIngreso.Trim().Substring(11, 8);
            string fechaSql = fechaIngreso.Trim().Substring(0, 4) + fechaIngreso.Trim().Substring(5, 2) + fechaIngreso.Trim().Substring(8, 2);
            string fechaHoraSql = fechaSql + " " + fechasHora;


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ obtenemos el ultimo folio ingresado
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT max(folioOrd) ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM SONG_ordenesPago";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE cIdArea  = '" + idArea + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND serieOrd = '" + Program.serieOrdenPago + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                if (con.leer_interno[0].ToString().Trim() == "")
                {
                    varbb = 0;
                    ultimoFolio = folioInicial;
                }
                else
                {
                    varbb = 1;
                    ultimoFolio = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                }
            }

            //CERRAR CADENA
            con.cerrar_interno();

            //------------------------------------------ comparacion de folios
            varaa = 0;
            if (varbb != 0)
            {
                ultimoFolio = ultimoFolio + 1;
            }
            //----------------------------------------------------------------------------------------------------------------------//
            // VALIDAMOS LOS FOLIOS DE LAS ORDENES DE PAGO                                                                          //
            //----------------------------------------------------------------------------------------------------------------------//
            double total = Convert.ToDouble(lblTotal.Text.Trim());
            if (total < 0) { MessageBox.Show("EL TOTAL NO PUEDE SER MENOR O IGUAL A  0", "ERROR", MessageBoxButtons.OK); return; }
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT folioIni, folioFin";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_areasResponsableOrdenes ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE cIdArea  = '" + idArea + "'";
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                vFolioInicial = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                vFolioFinal = Convert.ToInt32(con.leer_interno[1].ToString().Trim());
            }

            //CERRAR CONEXIÓN 
            con.cerrar_interno();

            if (vFolioInicial >= vFolioFinal) { MessageBox.Show("NO SE CUENTAN CON FOLIOS", "ERROR", MessageBoxButtons.OK); return; }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ obtenemos el id, foilio inicial y final del area
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT folioIni, folioFin, cIdArea ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM SONG_areasResponsableOrdenes";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE cIdArea  = '" + idArea + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                if (con.leer_interno[2].ToString().Trim() == "")
                {
                    varaa = 0;
                }
                else
                {
                    varaa = 1;
                    folioInicial = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    folioFinal = Convert.ToInt32(con.leer_interno[1].ToString().Trim());
                    idArea = con.leer_interno[2].ToString().Trim();
                }
            }

            //cerrar la conexión
            con.cerrar_interno();




            if (varaa == 0) { MessageBox.Show("EL AREA NO CUENTA CON FOLIOS", "ERROR", MessageBoxButtons.OK); return; }
            ////////////////////////////////////////////////////////////////////////////////////////
            //---------------------------------------------------------------
            //MÉTODO PARA AGREGAR A LA ORDEN DE PAGO DEL SONG 2025 
            //----------------------------------------------------------------
            ///////////////////////////////////////////////////////////////////////////////////////
            ///------------------------------------------------------------------------

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ INSERTAMOS EN LA TABLA ORDEN DE PAGO
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            string fechaCaducidad = lblFechaCaduca.Text.Trim();

            fechaCaducidad = fechaCaducidad.Trim().Substring(6, 4) + fechaCaducidad.Trim().Substring(3, 2) + fechaCaducidad.Trim().Substring(0, 2);

            con.cadena_sql_interno = "";
            con.cadena_sql_interno = "                          INSERT INTO SONG_ordenesPago (serieOrd,  folioOrd,  cIdArea, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                               fecElaba,  ciudadan,  domicili,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                               infoComp,  recOfici,  fecCaduc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                               totalOrd,  nombreUs,  rfc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                               fisicaMor, regFiscal, usoCfdi)";
            con.cadena_sql_interno = con.cadena_sql_interno + " VALUES ('" + Program.serieOrdenPago + "', " + ultimoFolio + ", '" + idArea + "',";
            con.cadena_sql_interno = con.cadena_sql_interno + " '" + fechaSql + "', '" + lblTitular.Text.Trim() + "', '" + lblCalle.Text.Trim() + "',";
            con.cadena_sql_interno = con.cadena_sql_interno + " '" + txtInfComp.Text.Trim() + "', " + " 0 " + ", '" + fechaCaducidad + "',"; //el recibo es en 0, se pasa así 
            con.cadena_sql_interno = con.cadena_sql_interno + " " + Convert.ToDouble(lblTotal.Text.Trim()) + ", '" + Program.nombre_usuario.Trim() + "', '" + RFC1 + "', ";
            con.cadena_sql_interno = con.cadena_sql_interno + " '" + PFM + "', '" + RF + "', '" + UF + "')";

            con.conectar_base_interno();
            con.open_c_interno();
            con.cadena_sql_cmd_interno();
            con.cmd_interno.ExecuteReader();

            //CERRAR CONEXIÓN 
            con.cerrar_interno();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ GENERAMOS EL REGISTRO PARA SONG_ORDENESPAGOAUTORIZA PERO CON EL VALOR EN 1 PUES YA VA AUTORIADO 
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  INSERT INTO SONG_ordenesPagoAutoriza(serieOrd, folioOrd, autoriza, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                                       fechaAutorisa, pagadaOrd, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                                       fechaCaduca, cancelaOrd, observaciones, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                                       nombreUsuario, seriePag, folioPag) ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  VALUES('" + Program.serieOrdenPago + "', " + ultimoFolio + ", 1, ";
            con.cadena_sql_interno = con.cadena_sql_interno + " ' " + fechaHoraSql + " '"; //aqui va fecha
            con.cadena_sql_interno = con.cadena_sql_interno + "      , 0, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    '" + fechaCaducidad + "', 0, 'INICIO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "    '" + Program.nombre_usuario.Trim() + "', '" + "." + "', " + "0" + ")";

            con.conectar_base_interno();
            con.open_c_interno();
            con.cadena_sql_cmd_interno();
            con.cmd_interno.ExecuteReader();

            //cerrar la conexión
            con.cerrar_interno();
            ////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ OBTENEMOS EL NUMERO DE ÓRDEN
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///-----------------------------------------
            ///////////////////////////////////////////////////////////////////////////////////////////////////////

            varbb = 0;
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT nNoOrden ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM SONG_ordenesPago";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE serieOrd = '" + Program.serieOrdenPago + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND folioOrd =  " + ultimoFolio;

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                if (con.leer_interno[0].ToString().Trim() == "")
                {
                    varbb = 0;
                }
                else
                {
                    varbb = 1;
                    vNoOrden = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                }
            }

            //CERRAR CONEXIÓN
            con.cerrar_interno();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //------------------------------------------ INSERTAMOS EN LA TABLA SONG_ORDEN_DESG
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            string dircD = "";
            string areaD = "";
            string concD = "";
            string conceptoCompleto = "";
            decimal impoD = 0;
            string CuntaContD = "0";
            string descCuentaD = "";

            for (int i = 0; i < lblConcepto.Items.Count; i++)
            {
                dircD = lblConcepto.Items[i].ToString().Substring(0, 2);
                areaD = lblConcepto.Items[i].ToString().Substring(2, 2);
                concD = lblConcepto.Items[i].ToString().Substring(4, 2);
                conceptoCompleto = lblConcepto.Items[i].ToString();
                impoD = Convert.ToDecimal(lblCosto.Items[i].ToString());
                varaa = 0;

                con.cadena_sql_interno = "";
                con.cadena_sql_interno = "                           INSERT INTO SONG_ORDEN_DESG (nNoOrden, cCveDirec, cCveArea, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "                               cCveCpto, Importe)";
                con.cadena_sql_interno = con.cadena_sql_interno + "  VALUES (" + vNoOrden + ", '" + dircD + "', '" + areaD + "', ";
                con.cadena_sql_interno = con.cadena_sql_interno + "         '" + concD + "', " + impoD + ")";

                con.conectar_base_interno();
                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.cmd_interno.ExecuteReader();

                //CERRAR CONEXIÓN
                con.cerrar_interno();
            }
           
                
                    //CONVERTIMOS LOS VALORES A DOUBLE PARA FORMATEARLOS
                    double supterr = Convert.ToDouble(lblSupTerrPriv.Text);
                    lblSupTerrPriv.Text = supterr.ToString("###,###,###,##0.##");

                    double supterrcom = Convert.ToDouble(lblSupTerrComun.Text);
                    lblSupTerrComun.Text = supterrcom.ToString("###,###,###,##0.##"); //bien 

                    double supconspriv = Convert.ToDouble(lblSupConsPriv.Text);
                    lblSupConsPriv.Text = supconspriv.ToString("###,###,###,##0.##");

                    double supconscom = Convert.ToDouble(lblSupConsCom.Text);
                    lblSupConsCom.Text = supconscom.ToString("###,###,###,##0.##"); //bien 

                    double valtotterr = Convert.ToDouble(lblValTotTerr.Text);
                    lblValTotTerr.Text = valtotterr.ToString("###,###,###,##0.##");

                    double valterrcom = Convert.ToDouble(lblValTerrCom.Text);
                    lblValTerrCom.Text = valterrcom.ToString("###,###,###,##0.##");

                    double valcoscom = Convert.ToDouble(lblValConsCom.Text);
                    lblValConsCom.Text = valcoscom.ToString("###,###,###,##0.##"); //bien

                    double valtotcons = Convert.ToDouble(lblValTotCons.Text);
                    lblValTotCons.Text = valtotcons.ToString("###,###,###,##0.##"); //valor catastral 

                    double valorcatastral = Convert.ToDouble(lblValor.Text);
                    lblValor.Text = valorcatastral.ToString("###,###,###,##0.##"); //valor catastral


                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //------------------------------------- GENERAMOS LA CONSULTA PARA OBTENER EL ÚLTIMO MES Y AÑO DEL PAGO 
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //------------------------------------- GENERAMOS EL INSERT EN LA TABLA DE 3 EN 1 2025, SERIE, FOLIO DE LA ÓRDEN
                    ///////////////////////////////////////////////////////////////////////////////////////////


                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno += "    INSERT INTO TRES_EN_UNO_2025";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Fecha_alta,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    estado,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    municipio,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    zona,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    manzana,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    lote,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    edificio,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    depto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    año,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    nombre_contri,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    domicilio_fis,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    colonia,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    descripcion_colonia,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    estado2,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    municipio2,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    zona2,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    manzana2,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    lote2,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    edificio2,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    depto2,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    tp,"; //terreno 1 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    tc,"; //terreno 2
                    con.cadena_sql_interno = con.cadena_sql_interno + "    cp,"; //txt 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    cc,"; //txt
                    con.cadena_sql_interno = con.cadena_sql_interno + "    valor_terreno,"; //terreno 3 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    valor_terreno_comun,"; //terreno 4
                    con.cadena_sql_interno = con.cadena_sql_interno + "    valor_construccion,"; //txt_cons 4
                    con.cadena_sql_interno = con.cadena_sql_interno + "    valor_comun,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    observaciones,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    status_catastro,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    status_catastro_pago,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    año_predial,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    pago_predial,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    status_predio,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    status_aportacion,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    tipo_certificacion,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    clave_catastral,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    valor_clave_catastral,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    ID,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE, ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Serie_Orden,"; //DATOS QUE HUGO AGREGÓ 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Folio_Orden,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    mes_predial,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    seriePago,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    folioPago";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Values";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + fechaHoraSql + "' ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + txtFolioCatastro.Text.ToString() + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    15,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    41,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(txtZona.Text.ToString().Trim()) + " ,"; //
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(txtManzana.Text.ToString().Trim()) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(txtLote.Text.ToString().Trim()) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + txtEdificio.Text.Trim() + "' ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + txtDepto.Text.Trim() + "' ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    2025,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + lblTitular.Text.Trim() + "' ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + lblCalle.Text.Trim() + "' ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + lblColonia.Text.Trim() + "' ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    15,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    41,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(txtZona.Text.ToString().Trim()) + " ,"; //
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(txtManzana.Text.ToString().Trim()) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(txtLote.Text.ToString().Trim()) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + txtEdificio.Text.Trim() + "' ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + txtDepto.Text.Trim() + "' ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + supterr + " ,"; //tp                                                                            
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + supterrcom + " ,"; //tc
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + supconspriv + " ,";//cp                     
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + supconscom + " ,"; //cc
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + valtotterr + " ,"; //valor terreno h
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + valterrcom + " ,"; //valor terreno comun i
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + valtotcons + " ,"; //valor cons j 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + valcoscom + " ,";  //valor cons ,M
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + txtInfComp.Text.ToString() + "' ,"; //observaciones
                    con.cadena_sql_interno = con.cadena_sql_interno + "    0,"; //status catastro
                    con.cadena_sql_interno = con.cadena_sql_interno + "    1,"; //status catastro pago
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + AÑO_PAGO + ","; //AÑO PREDIAL 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    0,"; //pago predial 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    0,"; //status predio
                    con.cadena_sql_interno = con.cadena_sql_interno + "    0, "; //status aportación
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + tipo_certi + " ,"; //tipo del traslado (5, 2, 6, 8) según sea el caso 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + clave_catastral + "' ,"; //observaciones
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + valorcatastral + "' ,"; //observaciones
                    con.cadena_sql_interno = con.cadena_sql_interno + "    0,"; //ID
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + cbo_serie.Text.Trim() + "', ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + Program.serieOrdenPago.ToString().Trim() + "', ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + ultimoFolio + "', "; //observaciones
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + PAGO_PREDIAL + ",";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    '" + SERIEFACTURA + "', "; // SERIE FACTURA 
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + FOLIO_FACTURA + ""; //observaciones
                    con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                    //falta agregar lo que dice hugo    
                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    //cerrar la conexión
                    con.cerrar_interno();

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //--------------------------------------------------- GENERAMOS EL REPORTE DE LA ÓRDEN DE PAGO 
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        formaReporte.MostrarOrden mostrarReporteV = new formaReporte.MostrarOrden(); //mostrar orden es el que se utiliza, no reporte 
                        mostrarReporteV.folioOrden = ultimoFolio;
                        mostrarReporteV.serieOrden = Program.serieOrdenPago;
                        //PARAMETROS QUE VOY A MANDAR DESDE AQUÍ PARA LA PANTALLA 
                        mostrarReporteV.claveCat = clave_catastral;
                        mostrarReporteV.serieCatastro = cbo_serie.Text.Trim();
                        mostrarReporteV.folioCat = Convert.ToInt32(txtFolioCatastro.Text.Trim());

                        MessageBox.Show("PROCEDE A IMPRIMIR LA ORDEN DE PAGO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mostrarReporteV.ShowDialog();
                        limpiartodo();
                
            
        }
        private void chkSioNoRfc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSioNoRfc.Checked) // Realiza acciones cuando la casilla está marcada
            {
                borrarRfc(); //método para limpiar los campos de RFC
                habilitarRfc(); //método para habilitar los campos de RFC
                llenarCombosRFC(); //método para llenar los combos de RFC
                txtRfc.Text = "";  //colocar el color amarillo al entrar y blanco al salir 
                txtPersonaMoral.Text = "";
                txtRfc.Focus();
            }
            else // Realiza acciones cuando la casilla no está marcada
            {
                borrarRfc();
                inhabilitarRfc();
                lbxTipoServicio.Focus();
            }
        }
        private void lbxTipoServicio_DoubleClick(object sender, EventArgs e)
        {
            String seleccionST1 = lbxTipoServicio.SelectedItem.ToString();
            String seleccionST = lbxTipoServicio.SelectedItem.ToString();
            int seleccionInt = 0;
            SELECCION_2 = lbxTipoServicio.SelectedIndex;

            if (seleccionST1.Substring(0, 1) != "-")
            {
                if (SELECCION_2 == 0)
                {
                    seleccionInt = Convert.ToInt32(seleccionST.Substring(0, 1));
                    lblConceptoCobro.Text = seleccionST;
                    cboCantidad.SelectedIndex = 0;
                    //lblPrecioCosto.Text = "283";
                    lblPrecioCosto.Text = Convert.ToString(UMA);

                    lblPrecioCosto.Enabled = true;
                    cboCantidad.Enabled = true;

                    cmdModificaLista.Enabled = false;
                    cmdCancelarLista.Enabled = false;

                    lblPrecioCosto.Focus();
                }
                else if (SELECCION_2 == 2)
                {
                    seleccionInt = Convert.ToInt32(seleccionST.Substring(0, 1));
                    lblConceptoCobro.Text = seleccionST;
                    cboCantidad.SelectedIndex = 0;
                    //lblPrecioCosto.Text = "283";
                    lblPrecioCosto.Text = Convert.ToString(UMA);

                    lblPrecioCosto.Enabled = true;
                    cboCantidad.Enabled = true;

                    cmdModificaLista.Enabled = false;
                    cmdCancelarLista.Enabled = false;

                    lblPrecioCosto.Focus();
                }
                else if (SELECCION_2 == 4)
                {
                    seleccionInt = Convert.ToInt32(seleccionST.Substring(0, 1));
                    lblConceptoCobro.Text = seleccionST;
                    cboCantidad.SelectedIndex = 0;
                    lblPrecioCosto.Text = Convert.ToString(UMA * 2);
                    //lblPrecioCosto.Text = "566";

                    lblPrecioCosto.Enabled = true;
                    cboCantidad.Enabled = true;

                    cmdModificaLista.Enabled = false;
                    cmdCancelarLista.Enabled = false;

                    lblPrecioCosto.Focus();
                }
                else if (SELECCION_2 == 6)
                {
                    seleccionInt = Convert.ToInt32(seleccionST.Substring(0, 1));
                    lblConceptoCobro.Text = seleccionST;
                    cboCantidad.SelectedIndex = 0;
                    lblPrecioCosto.Text = Convert.ToString(UMA * 3);
                    //lblPrecioCosto.Text = "849";

                    lblPrecioCosto.Enabled = true;
                    cboCantidad.Enabled = true;

                    cmdModificaLista.Enabled = false;
                    cmdCancelarLista.Enabled = false;

                    lblPrecioCosto.Focus();
                }
                //seleccionST = lbxTipoServicio.SelectedIndex.VALUE;
                cboCantidad.Enabled = true;
                cmdIngresarLista.Enabled = true;
                cmdIngresarLista.Focus();
            }
        }


        private void costos() ///modificar 
        {
            if (lblConceptoCobro.Text.Trim() == "") { MessageBox.Show("SE DEBE DE ELEGIR UN CONCEPTO", "ERROR", MessageBoxButtons.OK); return; }
            if (lblPrecioCosto.Text.Trim() == "") { MessageBox.Show("SE DEBE INGRESAR UNA CANTIDAD", "ERROR", MessageBoxButtons.OK); return; }
            if (Convert.ToDouble(lblPrecioCosto.Text.Trim()) <= 0) { MessageBox.Show("SE DEBE INGRESAR UNA CANTIDAD MAYOR A  0", "ERROR", MessageBoxButtons.OK); return; }

            string VarcCveDirec = "";
            string VarcCveArea = "";
            string VarcCveCpto = "";
            string VdomOnoDom = "";
            int VdomOnoDomI = 1;
            int VarOcupados = 1;
            double costo = 0;
            double ivaT = 0;
            string conceptoCompleto = "";
            string conceptoCompleto2 = "";
            string conceptoCompleto3 = "";
            string conceptoCompleto4 = "";
            string conceptoCompleto5 = "";
            string conceptoCompleto6 = "";
            string conceptoCompleto7 = "";
            string conceptoCompleto8 = "";
            int contador = 0;
            //

            pnlCobroTotal.Enabled = true;
            costo = Convert.ToDouble(lblPrecioCosto.Text);
            ////////SE AGREGO PARA QUE MULTIPLIQUE CANTIDAD POR COSTO
            costo = costo * Convert.ToDouble(cboCantidad.Text);
            //----------------------------------------------------------------------------------------------------------------------//
            //--------------- REVIZAMOS QUE NO SEA 0  ------------------------------------------------------------------------------//
            lblConcepto.Enabled = true;

            if (costo != 0)
            {

                ////////////CATALOGO DE CONCEPTOS
                ///SECCION_2
                ///0 = CERTIFICACION CLAVE Y VALOR CATASTRAL
                ///2 = APORTACION A MEJORAS
                ///4 CERTIFICACION CLAVE Y VALOR CATASTRAL Y APORTACION A MEJORAS
                ///6 = CERTIFICACION CLAVE Y VALOR CATASTRAL Y APORTACION A MEJORAS Y CONSTANCIA DE NO ADEUDO ( 3 EN 1)


                if (SELECCION_2 == 0) //UNO , EL PRIMERO 
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    //llenar certificado clave y valor
                    con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT cCveDirec,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         cCveArea,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         cCveCpto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         Comentario,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         cDescCpto";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    FROM SONG_CONCEPTOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE ID_OFICINA = " + "3";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND Activo = 0";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND cCveDirec = 80 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND cCveArea  = 01";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND cCveCpto  = 02";
                    con.cadena_sql_interno = con.cadena_sql_interno + "ORDER BY cDescCpto ";
                    //modificar 

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    //lbxTipoServicio.Items.Clear();
                    while (con.leer_interno.Read())
                    {
                        conceptoCompleto = con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  --  " + con.leer_interno[3].ToString().Trim() + "  --  " + con.leer_interno[4].ToString().Trim();
                    }

                    //CERRAR CONEXIÓN
                    con.cerrar_interno();

                    cmdIngresarLista.Enabled = false;
                    lbxTipoServicio.Enabled = false;

                    contador = 1;
                }
                if (SELECCION_2 == 2) //UNO, EL SEGUNDO 
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    //llenar aportación a mejoras
                    con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cCveDirec,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveArea,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveCpto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          Comentario,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cDescCpto";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM SONG_CONCEPTOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE ID_OFICINA = " + "3";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND Activo = 0";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveDirec = 80 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveArea  = 01";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveCpto  = 03";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY cDescCpto ";
                    //modificar 

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    //lbxTipoServicio.Items.Clear();
                    while (con.leer_interno.Read())
                    {
                        conceptoCompleto2 = con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  --  " + con.leer_interno[3].ToString().Trim() + "  --  " + con.leer_interno[4].ToString().Trim();
                    }
                    //CERRAR CONEXIÓN
                    con.cerrar_interno();
                    cmdIngresarLista.Enabled = false;
                    lbxTipoServicio.Enabled = false;
                    contador = 2;
                    //seleccion  del select 
                }
                //////////////////////////////////////////////////
                if (SELECCION_2 == 4) //DOS 
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    //llenar certificado clave y valor y aportación a mejoras
                    con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cCveDirec,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveArea,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveCpto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          Comentario,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cDescCpto";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM SONG_CONCEPTOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE ID_OFICINA = " + "3";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND Activo = 0";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveDirec = 80 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveArea  = 01";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveCpto  = 02";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY cDescCpto ";
                    //modificar 

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    //lbxTipoServicio.Items.Clear();
                    while (con.leer_interno.Read())
                    {
                        conceptoCompleto3 = con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  --  " + con.leer_interno[3].ToString().Trim() + "  --  " + con.leer_interno[4].ToString().Trim();
                    }
                    //CERRAR CONEXIÓN
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    //llenar certificado clave y valor
                    con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cCveDirec,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveArea,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveCpto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          Comentario,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cDescCpto";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM SONG_CONCEPTOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE ID_OFICINA = " + "3";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND Activo = 0";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveDirec = 80 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveArea  = 01";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveCpto  = 03";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY cDescCpto ";
                    //modificar 

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    //lbxTipoServicio.Items.Clear();
                    while (con.leer_interno.Read())
                    {
                        conceptoCompleto4 = con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  --  " + con.leer_interno[3].ToString().Trim() + "  --  " + con.leer_interno[4].ToString().Trim();
                    }
                    //CERRAR CONEXIÓN


                    con.cerrar_interno();
                    lbxTipoServicio.Enabled = false;
                    cmdIngresarLista.Enabled = false;
                    contador = 4;
                }
                if (SELECCION_2 == 6) ///TRES EN 1 
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    //llenar certificado 3 en 1 
                    con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cCveDirec,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveArea,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveCpto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          Comentario,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cDescCpto";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM SONG_CONCEPTOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE ID_OFICINA = " + "9"; //ANTES ERA 3, SE CAMBIO POR TESORERÍA 
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND Activo = 0";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveDirec = 80 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveArea  = 01";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveCpto  = 01"; //NO ADEUDO PREDIAL
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY cDescCpto ";
                    //modificar 

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        conceptoCompleto5 = con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  --  " + con.leer_interno[3].ToString().Trim() + "  --  " + con.leer_interno[4].ToString().Trim();
                    }

                    //CERRAR CONEXIÓN
                    con.cerrar_interno();

                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    //llenar certificado clave y valor
                    con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cCveDirec,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveArea,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveCpto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          Comentario,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cDescCpto";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM SONG_CONCEPTOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE ID_OFICINA = " + "3";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND Activo = 0";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveDirec = 80 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveArea  = 01";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveCpto  = 02";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY cDescCpto ";
                    //modificar 

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        conceptoCompleto6 = con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  --  " + con.leer_interno[3].ToString().Trim() + "  --  " + con.leer_interno[4].ToString().Trim();
                    }

                    //CERRAR CONEXIÓN
                    con.cerrar_interno();

                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    //llenar certificado clave y valor
                    con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT cCveDirec,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveArea,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cCveCpto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          Comentario,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          cDescCpto";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM SONG_CONCEPTOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE ID_OFICINA = " + "3";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND Activo = 0";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveDirec = 80 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveArea  = 01";
                    con.cadena_sql_interno = con.cadena_sql_interno + "      AND cCveCpto  = 03";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY cDescCpto ";
                    //modificar 

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    //lbxTipoServicio.Items.Clear();
                    while (con.leer_interno.Read())
                    {
                        conceptoCompleto7 = con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  --  " + con.leer_interno[3].ToString().Trim() + "  --  " + con.leer_interno[4].ToString().Trim();
                    }

                    //CERRAR CONEXIÓN
                    con.cerrar_interno();

                    cmdIngresarLista.Enabled = false;
                    lbxTipoServicio.Enabled = false;

                    contador = 6;
                }
                VdomOnoDom = "1";
                //---------------------------- REVISAMOS QUE NO SE REPITA EL CONCEPTO DE COBRO ------------------------------------//

                for (int i = 0; i < lblConcepto.Items.Count; i++) //segun yo ya no va
                {
                    if (lblConceptoCobro.Text.Trim() == lblConcepto.Items[i].ToString())
                    {
                        MessageBox.Show("NO PUEDE REPETIR EL CONCEPTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblPrecioCosto.Text = "";
                        lblConceptoCobro.Text = "";
                        lbxTipoServicio.Focus();
                        return;
                    }
                }

                if (VdomOnoDomI == 2)
                {
                    lblConcepto.Items.Add(lblConceptoCobro.Text);
                    lblConcepto.Items.Add(lblConceptoCobro.Text.Substring(0, 8) + "  --  IVA");

                    lblCosto.Items.Add(costo);
                    lblCosto.Items.Add(costo * 0.16);

                    Program.subTotalDesglosado = Program.subTotalDesglosado + costo;
                    Program.ivaDesglosado = Program.ivaDesglosado + (costo * 0.16);
                }

                if (VdomOnoDomI == 1)
                {
                    if (contador == 1)
                    {
                        lblConcepto.Items.Add(conceptoCompleto);
                        lblCosto.Items.Add(costo);

                        //Program.subTotalDesglosado = Convert.ToDouble(lblPrecioCosto.Text.ToString * Convert.ToDouble(cboCantidad.Text.ToString());
                        Program.subTotalDesglosado = Convert.ToDouble(lblPrecioCosto.Text) * Convert.ToInt32(cboCantidad.Text);
                    }
                    else if (contador == 2)
                    {
                        lblCosto.Items.Clear();
                        lblConcepto.Items.Add(conceptoCompleto2);
                        lblCosto.Items.Add(costo);
                        Program.subTotalDesglosado = Convert.ToDouble(lblPrecioCosto.Text) * Convert.ToInt32(cboCantidad.Text);

                    }
                    else if (contador == 4)
                    {
                        lblConcepto.Items.Add(conceptoCompleto3);
                        lblConcepto.Items.Add(conceptoCompleto4);
                        lblCosto.Items.Clear();
                        lblCosto.Items.Add(costo / 2);
                        lblCosto.Items.Add(costo / 2);
                        Program.subTotalDesglosado = Convert.ToDouble(lblPrecioCosto.Text) * Convert.ToInt32(cboCantidad.Text);
                    }
                    else if (contador == 6)
                    {
                        lblConcepto.Items.Add(conceptoCompleto5);
                        lblConcepto.Items.Add(conceptoCompleto6);
                        lblConcepto.Items.Add(conceptoCompleto7);
                        lblCosto.Items.Clear();
                        lblCosto.Items.Add(costo / 3);
                        lblCosto.Items.Add(costo / 3);
                        lblCosto.Items.Add(costo / 3);
                        Program.subTotalDesglosado = Convert.ToDouble(lblPrecioCosto.Text) * Convert.ToInt32(cboCantidad.Text);
                    }
                    lblConceptoCobro.Text = "";
                    lblPrecioCosto.Text = "";

                    lblsubTotal.Text = string.Format("{0:#,0.00}", Program.subTotalDesglosado);
                    lblivatota.Text = string.Format("{0:#,0.00}", Program.ivaDesglosado);
                    lblTotal.Text = string.Format("{0:#,0.00}", Program.subTotalDesglosado);

                    lbxTipoServicio.Focus();

                    cboCantidad.Enabled = false;
                    lblPrecioCosto.Enabled = false;
                    cmdModificaLista.Enabled = true;
                    cmdCancelarLista.Enabled = false;
                    cmdOrden.Enabled = true;

                }
                else
                {
                    MessageBox.Show("NO PUEDE SER  0.00", "ERROR", MessageBoxButtons.OK);
                    lblPrecioCosto.Focus();
                    return;
                }

            }
            void alado()
            {
                double costoImporte = 0;
                double costoIva = 0;

                string sinIvaConIvaS2 = "0";
                int sinIvaConIvaI = 1;

                if (sinIvaConIvaI == 1)  // no tiene iva
                {
                    costoImporte = Convert.ToDouble(lblPrecioCosto.Text.Trim());
                    costoIva = 0;

                    lblConcepto.Items.RemoveAt(NUM_ITEMS);
                    lblCosto.Items.RemoveAt(NUM_ITEMS);
                }

                if (sinIvaConIvaI == 2)  // si tiene iva
                {
                    costoImporte = Convert.ToDouble(lblPrecioCosto.Text.Trim());
                    costoIva = Convert.ToDouble(lblCosto.Items[NUM_ITEMS + 1].ToString());

                    lblConcepto.Items.RemoveAt(NUM_ITEMS);
                    lblConcepto.Items.RemoveAt(NUM_ITEMS);

                    lblCosto.Items.RemoveAt(NUM_ITEMS);
                    lblCosto.Items.RemoveAt(NUM_ITEMS);

                }

                Program.subTotalDesglosado = Program.subTotalDesglosado - costoImporte;
                Program.ivaDesglosado = Program.ivaDesglosado - costoIva;

                lblConceptoCobro.Text = "";
                lblPrecioCosto.Text = "";

                lblsubTotal.Text = string.Format("{0:#,0.00}", Program.subTotalDesglosado);
                lblivatota.Text = string.Format("{0:#,0.00}", Program.ivaDesglosado);
                lblTotal.Text = string.Format("{0:#,0.00}", (Program.subTotalDesglosado + Program.ivaDesglosado));

                lbxTipoServicio.Focus();

                cboCantidad.Enabled = false;
                lblPrecioCosto.Enabled = false;

                lbxTipoServicio.Enabled = true;
                lblConceptoCobro.Enabled = true;
                cboCantidad.Enabled = true;
                lblPrecioCosto.Enabled = true;
                cmdIngresarLista.Enabled = true;
                cmdModificaLista.Enabled = false;
                cmdCancelarLista.Enabled = false;
                lblConcepto.Enabled = true;
                lblCosto.Enabled = true;
                cmdOrden.Enabled = true;

                cmdModificaLista.Enabled = false;
                cmdCancelarLista.Enabled = false;

            }


        }
    }
}
