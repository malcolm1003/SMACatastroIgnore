using AccesoBase;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using MaterialSkin.Controls;
using Microsoft.Office.Interop.Excel;
using Mysqlx.Crud;
using Org.BouncyCastle.Ocsp;
using SMACatastro;
using SMACatastro.catastroCartografia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Telerik.WinControls;
using USLibV4.Utilerias;
using Utilerias;
using ZstdSharp.Unsafe;
using static log4net.Appender.FileAppender;
using static QRCoder.PayloadGenerator;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;


namespace SMACatastro.catastroSistemas
{
    public partial class frmMovimientosSistemas : Form
    {
        int nivelDeUsuario = 1;    // cambiar por variable de nivel de usuario
        int tipoDeMovimiento = 0;  // 1 altas, 2 bajas, 3 cambios, 4 generales
        string descripcionConsulta = "";  // saber que es lo que van a hacer en los cambios o en las altas

        decimal facFrente = 0;
        decimal facFondo = 0;
        decimal facIrreg = 0;
        decimal facArea = 0;
        decimal facTopo = 0;
        decimal facPosicion = 0;
        string usoSueloV = "";
        string destinoV = "";



        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();


        public frmMovimientosSistemas()
        {
            InitializeComponent();
        }

        private void mtcInformacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mtcInformacion.SelectedIndex == 0) // inicio
            {
                cajasBlanco();
                pnlOculta.Visible = true;
                pnlOculta.Size = new Size(1339, 531);
                pnlOculta.Left = 12;                    // Distancia desde el borde izquierdo
                pnlOculta.Top = 153;                    // Distancia desde el borde superior
                
                tipoDeMovimiento = 0;
                invisibleSerieFolioVerdadero();
                abilitarSerieYfolio();
                btnBuscar.Enabled = false;

                inicio();

            }// inicio

            if (mtcInformacion.SelectedIndex == 1) // altas
            {
                
                if (nivelDeUsuario != 4 )
                {
                    MessageBox.Show("NO SE TIENE EL NIVEL DE USUARIO", "ERROR", MessageBoxButtons.OK);
                    mtcInformacion.SelectedIndex = 1;
                    return;
                }
                else
                {
                    tipoDeMovimiento = 1;       //alta
                    Program.tipoDeMovimientoProgram = 1; // alta
                    cajasBlanco();

                    invisibleSerieFolioVerdadero();
                    abilitarSerieYfolio();
                    generales();
                    panelMuestra();

                    btnConsulta.Enabled = true;
                    btnBuscar.Enabled = false;
                    btnCancelar.Enabled = true;
                    cmdSalida.Enabled = true;
                    btnMinimizar.Enabled = true;

                    txtSerie.Text = Program.SerieC.Trim();
                    txtSerie.Enabled = true;
                    txtFolio.Enabled = true;
                    txtZona.Focus();
                }
                    
            }// altas

            if (mtcInformacion.SelectedIndex == 2) // bajas
            {
                if (nivelDeUsuario != 4)
                {
                    MessageBox.Show("NO SE TIENE EL NIVEL DE USUARIO", "ERROR", MessageBoxButtons.OK);
                    mtcInformacion.SelectedIndex = 2;
                    return;
                }
                else
                {
                    tipoDeMovimiento = 2;
                    Program.tipoDeMovimientoProgram = 2; // bajas 
                    cajasBlanco();

                    panelOculta();
                    invisibleSerieFoliofalse();
                    inabilitarSerieYfolio();
                    generales();

                    txtPropietario.Enabled = false;
                    btnConsulta.Enabled = true;
                    btnBuscar.Enabled = true;
                    btnCancelar.Enabled = true;
                    cmdSalida.Enabled = true;
                    btnMinimizar.Enabled = true;

                    txtZona.Focus();
                }
            }// bajas

            if (mtcInformacion.SelectedIndex == 3) // cambios
            {
                if (nivelDeUsuario != 4)
                {
                    MessageBox.Show("NO SE TIENE EL NIVEL DE USUARIO", "ERROR", MessageBoxButtons.OK);
                    mtcInformacion.SelectedIndex = 3;
                    return;
                }
                else
                {
                    tipoDeMovimiento = 3;

                    Program.tipoDeMovimientoProgram = 3; // cambios
                    cajasBlanco();

                    invisibleSerieFolioVerdadero();
                    abilitarSerieYfolio();
                    generales();
                    panelMuestra();

                    btnConsulta.Enabled = true;
                    btnBuscar.Enabled = false;
                    btnCancelar.Enabled = true;
                    cmdSalida.Enabled = true;
                    btnMinimizar.Enabled = true;

                    txtPropietario.Enabled = false;
                    txtSerie.Text = Program.SerieC.Trim();
                    txtSerie.Enabled = true;
                    txtFolio.Enabled = true;
                    txtZona.Focus();
                }
            }// cambios

            if (mtcInformacion.SelectedIndex == 4)   // consultas generales
            {
                tipoDeMovimiento = 4;
                Program.tipoDeMovimientoProgram = 4; // consulta generales
                cajasBlanco();

                panelOculta();
                invisibleSerieFoliofalse();
                inabilitarSerieYfolio();
                generales();

                txtPropietario.Enabled = false;
                btnConsulta.Enabled = true;
                btnBuscar.Enabled = true;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtZona.Focus();

            }// generales
            
            if (mtcInformacion.SelectedIndex == 5)   // Cambios generales
            {
                tipoDeMovimiento = 5;
                Program.tipoDeMovimientoProgram = 5; // cambios generales
                cajasBlanco();


                panelOculta();
                invisibleSerieFoliofalse();
                inabilitarSerieYfolio();
                generales();

                txtPropietario.Enabled = false;
                btnConsulta.Enabled = true;
                btnBuscar.Enabled = true;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtZona.Focus();
            }// generales
        }

        private void panelOculta()
        {
            pnlOculta.Visible = true;
            pnlOculta.Size = new Size(1339, 161);
            pnlOculta.Left = 12;                    // Distancia desde el borde izquierdo
            pnlOculta.Top = 153;                    // Distancia desde el borde superior
        }

        private void panelMuestra()
        {
            pnlOculta.Visible = false;
            pnlOculta.Size = new Size(1339, 171);
            pnlOculta.Left = 12;                    // Distancia desde el borde izquierdo
            pnlOculta.Top = 153;                    // Distancia desde el borde superior
            DGVmovimiento.Enabled = true;
            cmdAlDia.Enabled = true;
            cmdDiasAnteriores.Enabled = true;
            cmdRefresh.Enabled = true;
        }

        private void invisibleSerieFolioVerdadero()
        {
            lblDiagonal.Visible = true;
            txtSerie.Visible    = true;
            lblGuion.Visible    = true;
            txtFolio.Visible    = true;
            lblSeries.Visible   = true;
            lblFolios.Visible   = true;
        }

        private void invisibleSerieFoliofalse()
        {
            lblDiagonal.Visible = false;
            txtSerie.Visible    = false;
            lblGuion.Visible    = false;
            txtFolio.Visible    = false;
            lblSeries.Visible   = false;
            lblFolios.Visible   = false;
        }

        private void inabilitarSerieYfolio()
        {
            txtSerie.Text = "";
            txtSerie.Enabled = false;
            txtSerie.BackColor = System.Drawing.Color.Black;

            txtFolio.Text = "";
            txtFolio.Enabled = false;
            txtFolio.BackColor = System.Drawing.Color.Black;
        }

        private void abilitarSerieYfolio()
        {
            txtSerie.Text = "";
            txtSerie.Enabled = false;
            txtSerie.BackColor = System.Drawing.Color.White;

            txtFolio.Text = "";
            txtFolio.Enabled = false;
            txtFolio.BackColor = System.Drawing.Color.White;
        }

        private void abilitarSerieyFolioyClaveCatastro()
        {
            txtSerie.Text = "";
            txtSerie.Enabled = true;
            txtSerie.BackColor = System.Drawing.Color.White;
            txtFolio.Text = "";
            txtFolio.Enabled = true;
            txtFolio.BackColor = System.Drawing.Color.White;
            txtZona.Text = "";
            txtZona.Enabled = true;
            txtMzna.Text = "";
            txtMzna.Enabled = true;
            txtLote.Text = "";
            txtLote.Enabled = true;
            txtEdificio.Text = "";
            txtEdificio.Enabled = true;
            txtDepto.Text = "";
            txtDepto.Enabled = true;
        }

        private void inicio()
        {
            limpiarTodo();
            llenarCombosNormales();
            inabilitarTodos();
            pnlOculta.Enabled = true;
            pnlOculta.Visible = true;
            pnlOculta.BringToFront();

            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;
            mtcInformacion.SelectedIndex = 0;
        }

        private void limpiarTodo()   /// limpiamos toda la pantalla
        {
            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";
            txtSerie.Text = "";
            txtFolio.Text = "";

            DGVmovimiento.DataSource = null;
            DGVmovimiento.Rows.Clear();

            cboTipoPredio.Items.Clear();
            lblEdoPredio.Text = "";

            txtLatitud.Text = "";// txtLatitud0
            txtLongitud.Text = "";// txtLongitud0  

            txtDomicilioPredio.Text = "";
            txtZonaOrigen.Text = "";
            txtCodigoCalle.Text = "";

            cboCalle.Items.Clear();

            txtNoExterior.Text = "";
            txtEnCalle.Text = "";
            txtYcalle.Text = "";
            txtCodigoPostal.Text = "";
            txtColonia.Text = "";

            cboRegimenPropiedad.Items.Clear();
            txtSupTerreno.Text = "";
            txtSupTerrenoComun.Text = "";
            txtFrente.Text = "";
            txtFondo.Text  = "";
            txtObservaciones.Text = "";

            cboUbicacion.Items.Clear();
            txtSupConstruccion.Text = "";
            txtSupConstruccion.Text = "";
            txtSupConstruccionComun.Text = "";
            txtDesnivel.Text = "";
            txtArea.Text = "";

            txtNoIntrior.Text = ""; 
            txtPropietario.Text = "";
            txtDomicilioPropietario.Text = "";
            txtDomicilioFiscal.Text = "";
            cboUsoSuelo.Text = "";
            txtSupTerrenoPro.Text = "";
            txtSupTerrenoComunPro.Text = "";
            txtSupConstruccionPro.Text = "";
            txtSupConstruccionComunPro.Text = "";
            txtIndiviso.Text = "";

            cboDestino.Items.Clear();
            txtValorTerrenoPropio.Text = "";
            txtValorTerrenoComun.Text = "";
            txtValorConstPropia.Text = "";
            txtValorConstComun.Text = "";
            txtValorCatastral.Text = "";
            txtObservacionPro.Text = "";
        }

        private void limpiarTodoNoClaveYfolio()   /// limpiamos toda la pantalla
        {

            DGVmovimiento.DataSource = null; // Si estaba enlazado a un DataSource
            DGVmovimiento.Rows.Clear();

            cboTipoPredio.Items.Clear();
            lblEdoPredio.Text = "";

            txtLatitud.Text = "";// txtLatitud0
            txtLongitud.Text = "";// txtLongitud0  

            txtDomicilioPredio.Text = "";
            txtZonaOrigen.Text = "";
            txtCodigoCalle.Text = "";

            cboCalle.Items.Clear();

            txtNoExterior.Text = "";
            txtEnCalle.Text = "";
            txtYcalle.Text = "";
            txtCodigoPostal.Text = "";
            txtColonia.Text = "";

            cboRegimenPropiedad.Items.Clear();
            txtSupTerreno.Text = "";
            txtSupTerrenoComun.Text = "";
            txtSupConstruccion.Text = "";
            txtSupConstruccionComun.Text = "";
            txtDesnivel.Text = "";
            txtArea.Text = "";


            txtFrente.Text = "";
            txtFondo.Text = "";
            txtObservaciones.Text = "";

            txtNoIntrior.Text = "";
            txtPropietario.Text = "";
            txtDomicilioPropietario.Text = "";
            txtDomicilioFiscal.Text = "";
            cboUsoSuelo.Text = "";
            txtSupTerrenoPro.Text = "";
            txtSupTerrenoComunPro.Text = "";
            txtSupConstruccionPro.Text = "";
            txtSupConstruccionComunPro.Text = "";
            txtIndiviso.Text = "";

            cboDestino.Items.Clear();
            txtValorTerrenoPropio.Text = "";
            txtValorTerrenoComun.Text = "";
            txtValorConstPropia.Text = "";
            txtValorConstComun.Text = "";
            txtValorCatastral.Text = "";
            txtObservacionPro.Text = "";
        }

        private void limpiarTodoNoClaveYfolioNoDGVmovimiento()   /// limpiamos toda la pantalla
        {
            cboTipoPredio.Items.Clear();
            lblEdoPredio.Text = "";

            txtLatitud.Text = "";// txtLatitud0
            txtLongitud.Text = "";// txtLongitud0  

            txtDomicilioPredio.Text = "";
            txtZonaOrigen.Text = "";
            txtCodigoCalle.Text = "";

            cboCalle.Items.Clear();

            txtNoExterior.Text = "";
            txtEnCalle.Text = "";
            txtYcalle.Text = "";
            txtCodigoPostal.Text = "";
            txtColonia.Text = "";

            cboRegimenPropiedad.Items.Clear();
            txtSupTerreno.Text = "";
            txtSupTerrenoComun.Text = "";
            txtFrente.Text = "";
            txtFondo.Text = "";
            txtObservaciones.Text = "";

            txtSupConstruccion.Text = "";
            txtSupConstruccionComun.Text = "";
            txtDesnivel.Text = "";
            txtArea.Text = "";

            txtNoIntrior.Text = "";
            txtPropietario.Text = "";
            txtDomicilioPropietario.Text = "";
            txtDomicilioFiscal.Text = "";
            cboUsoSuelo.Text = "";
            txtSupTerrenoPro.Text = "";
            txtSupTerrenoComunPro.Text = "";
            txtSupConstruccionPro.Text = "";
            txtSupConstruccionComunPro.Text = "";
            txtIndiviso.Text = "";

            cboDestino.Items.Clear();
            txtValorTerrenoPropio.Text = "";
            txtValorTerrenoComun.Text = "";
            txtValorConstPropia.Text = "";
            txtValorConstComun.Text = "";
            txtValorCatastral.Text = "";
            txtObservacionPro.Text = "";
        }

        private void inabilitarTodos()
        {
            btnMinimizar.Enabled = true;
            btnConsulta.Enabled = false;
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = false;
            cmdSalida.Enabled = false;

            btnAceptar.Enabled = false;
            btnCalculo.Enabled = false;

            btnConstLote.Enabled = false;
            btnConstComun.Enabled = false;

            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            txtSerie.Enabled = false;
            txtFolio.Enabled = false;
            txtNoIntrior.Enabled = false;

            pnlDatosPredio.Enabled = false;
            cboTipoPredio.Enabled = false;
            lblEdoPredio.Enabled = false;
            txtDomicilioPredio.Enabled = false;
            txtZonaOrigen.Enabled = false;
            txtCodigoCalle.Enabled = false;
            cboCalle.Enabled = false;
            txtNoExterior.Enabled = false;
            txtEnCalle.Enabled = false;
            txtYcalle.Enabled = false;
            txtCodigoPostal.Enabled = false;
            txtColonia.Enabled = false;

            cboRegimenPropiedad.Enabled = false;
            txtSupTerreno.Enabled = false;
            txtSupTerrenoComun.Enabled = false;
            txtFrente.Enabled = false;
            txtFondo.Enabled = false;
            txtObservaciones.Enabled = false;

            cboUbicacion.Enabled = false;
            txtSupConstruccion.Enabled = false;
            txtSupConstruccionComun.Enabled = false;
            txtDesnivel.Enabled = false;
            txtArea.Enabled = false;

            pnlDatosPropiedades.Enabled = false;
            txtPropietario.Enabled = false;
            txtDomicilioPropietario.Enabled = false;
            txtDomicilioFiscal.Enabled = false;
            cboUsoSuelo.Enabled = false;
            txtSupTerrenoPro.Enabled = false;
            txtSupTerrenoComunPro.Enabled = false;
            txtSupConstruccionPro.Enabled = false;
            txtSupConstruccionComunPro.Enabled = false;
            txtIndiviso.Enabled = false;

            cboDestino.Enabled = false;
            txtValorTerrenoPropio.Enabled = false;
            txtValorTerrenoComun.Enabled = false;
            txtValorConstPropia.Enabled = false;
            txtValorConstComun.Enabled = false;
            txtValorCatastral.Enabled = false;
            txtObservacionPro.Enabled = false;

        }

        private void generales()
        {
            inabilitarTodos();
            limpiarTodo();
            llenarCombosNormales();
            pnlOculta.Visible = true;

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
            txtSerie.Text = "";
            txtFolio.Text = "";

            btnConsulta.Enabled = true;
            btnBuscar.Enabled = true;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;
        }

        private void frmMovimientosSistemas_Load(object sender, EventArgs e)
        {
            pnlOculta.Visible = true;
            pnlOculta.Size = new Size(1339, 531);

            pnlOculta.Left = 12;                    // Distancia desde el borde izquierdo
            pnlOculta.Top = 153;                    // Distancia desde el borde superior

            inicio();
            cajasColor();
            nivelDeUsuario = Program.acceso_nivel_acceso;
            lblUsuario.Text = Program.nombre_usuario;

        }

        private void cajasBlanco()
        {
            txtZona.BackColor = System.Drawing.Color.White;
            txtMzna.BackColor = System.Drawing.Color.White;
            txtLote.BackColor = System.Drawing.Color.White;
            txtEdificio.BackColor = System.Drawing.Color.White;
            txtDepto.BackColor = System.Drawing.Color.White;
            txtSerie.BackColor = System.Drawing.Color.White;
            txtFolio.BackColor = System.Drawing.Color.White;

            txtDomicilioPredio.BackColor = System.Drawing.Color.White;
            txtZonaOrigen.BackColor = System.Drawing.Color.White;
            txtCodigoCalle.BackColor = System.Drawing.Color.White;
            txtNoExterior.BackColor = System.Drawing.Color.White;
            txtEnCalle.BackColor = System.Drawing.Color.White;
            txtYcalle.BackColor = System.Drawing.Color.White;
            txtCodigoPostal.BackColor = System.Drawing.Color.White;
            txtColonia.BackColor = System.Drawing.Color.White;
            txtSupTerreno.BackColor = System.Drawing.Color.White;
            txtSupTerrenoComun.BackColor = System.Drawing.Color.White;
            txtFrente.BackColor = System.Drawing.Color.White;
            txtFondo.BackColor = System.Drawing.Color.White;
            txtDesnivel.BackColor = System.Drawing.Color.White;
            txtArea.BackColor = System.Drawing.Color.White;
            txtObservaciones.BackColor = System.Drawing.Color.White;
            txtPropietario.BackColor = System.Drawing.Color.White;
            txtDomicilioPropietario.BackColor = System.Drawing.Color.White;
            txtDomicilioFiscal.BackColor = System.Drawing.Color.White;
            txtSupTerrenoPro.BackColor = System.Drawing.Color.White;
            txtSupTerrenoComunPro.BackColor = System.Drawing.Color.White;
            txtSupConstruccionPro.BackColor = System.Drawing.Color.White;
            txtSupConstruccionComunPro.BackColor = System.Drawing.Color.White;
            txtIndiviso.BackColor = System.Drawing.Color.White;
            txtValorTerrenoPropio.BackColor = System.Drawing.Color.White;
            txtValorTerrenoComun.BackColor = System.Drawing.Color.White;
            txtValorConstPropia.BackColor = System.Drawing.Color.White;
            txtValorConstComun.BackColor = System.Drawing.Color.White;
            txtValorCatastral.BackColor = System.Drawing.Color.White;
            txtObservacionPro.BackColor = System.Drawing.Color.White;
            txtNoIntrior.BackColor = System.Drawing.Color.White;

            //lblEdoPredio.Enter += util.TextBox_Enter;

            cboTipoPredio.BackColor = System.Drawing.Color.White;
            cboCalle.BackColor = System.Drawing.Color.White;
            cboRegimenPropiedad.BackColor = System.Drawing.Color.White;
            cboUsoSuelo.BackColor = System.Drawing.Color.White;
            cboDestino.BackColor = System.Drawing.Color.White;
            cboUbicacion.BackColor = System.Drawing.Color.White;

        }

        private void cajasColor()
        {
            txtZona.Enter += util.TextBox_Enter;
            txtMzna.Enter += util.TextBox_Enter;
            txtLote.Enter += util.TextBox_Enter;
            txtEdificio.Enter += util.TextBox_Enter;
            txtDepto.Enter += util.TextBox_Enter;
            txtSerie.Enter += util.TextBox_Enter;
            txtFolio.Enter += util.TextBox_Enter;

            txtDomicilioPredio.Enter += util.TextBox_Enter;
            txtZonaOrigen.Enter += util.TextBox_Enter;
            txtCodigoCalle.Enter += util.TextBox_Enter;
            txtNoExterior.Enter += util.TextBox_Enter;
            txtEnCalle.Enter += util.TextBox_Enter;
            txtYcalle.Enter += util.TextBox_Enter;
            txtCodigoPostal.Enter += util.TextBox_Enter;
            txtColonia.Enter += util.TextBox_Enter;
            txtSupTerreno.Enter += util.TextBox_Enter;
            txtSupTerrenoComun.Enter += util.TextBox_Enter;
            txtFrente.Enter += util.TextBox_Enter;
            txtFondo.Enter += util.TextBox_Enter;
            txtDesnivel.Enter += util.TextBox_Enter;
            txtArea.Enter += util.TextBox_Enter;
            txtObservaciones.Enter += util.TextBox_Enter;
            txtPropietario.Enter += util.TextBox_Enter;
            txtDomicilioPropietario.Enter += util.TextBox_Enter;
            txtDomicilioFiscal.Enter += util.TextBox_Enter;
            txtSupTerrenoPro.Enter += util.TextBox_Enter;
            txtSupTerrenoComunPro.Enter += util.TextBox_Enter;
            txtSupConstruccionPro.Enter += util.TextBox_Enter;
            txtSupConstruccionComunPro.Enter += util.TextBox_Enter;
            txtIndiviso.Enter += util.TextBox_Enter;
            txtValorTerrenoPropio.Enter += util.TextBox_Enter;
            txtValorTerrenoComun.Enter += util.TextBox_Enter;
            txtValorConstPropia.Enter += util.TextBox_Enter;
            txtValorConstComun.Enter += util.TextBox_Enter;
            txtValorCatastral.Enter += util.TextBox_Enter;
            txtObservacionPro.Enter += util.TextBox_Enter;
            txtNoIntrior.Enter += util.TextBox_Enter;

            //lblEdoPredio.Enter += util.TextBox_Enter;

            cboTipoPredio.Enter += util.Cbo_Box_Enter;
            cboCalle.Enter += util.Cbo_Box_Enter;
            cboRegimenPropiedad.Enter += util.Cbo_Box_Enter;
            cboUsoSuelo.Enter += util.Cbo_Box_Enter;
            cboDestino.Enter += util.Cbo_Box_Enter;
            cboUbicacion.Enter += util.Cbo_Box_Enter;
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
            if (txtDepto.Text.Length == 4) 
            { 
                if (txtFolio.Visible == true)
                { 
                    txtFolio.Focus(); 
                }
                else
                { 
                    btnConsulta.Focus(); 
                }
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            inicio();
        }

        private void PanelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //METODO PARA ARRASTRAR EL FORMULARIO-----------------------------------------------------------------------------------------------
        
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //----------------------------------------------------------------------------------------------------------------------------------

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLatitud.Text) || string.IsNullOrWhiteSpace(txtLongitud.Text))
            {
                MessageBox.Show("Por favor, ingrese la latitud y longitud antes de abrir Google Maps.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string latitud = txtLatitud.Text.Trim();
            string longitud = txtLongitud.Text.Trim();

            //return $"https://www.google.com/maps?q={latitud},{longitud}";
            Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
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

            btnConsulta.Enabled = true;
            btnBuscar.Enabled = true;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;

            if (txtZona.Text.Trim() != "" && txtMzna.Text.Trim() != "" && txtLote.Text.Trim() != "" && txtEdificio.Text.Trim() != "" && txtDepto.Text.Trim() != "")
            {
                consultaGeneral();
            }
            else
            {
                txtZona.Focus(); 
            }



                

            //frmCatastro01UbicacionAlta.ActiveForm.Opacity = 0.50;
            //frmCatastro03BusquedaCatastro fs = new frmCatastro03BusquedaCatastro();
            //fs.ShowDialog();
            ////fs.Show();
            //frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;

            //if (Program.zonaV != "") { txtZona.Text = Program.zonaV.Trim(); }
            //if (Program.manzanaV != "") { txtMzna.Text = Program.manzanaV.Trim(); }
            //if (Program.loteV != "") { txtLote.Text = Program.loteV.Trim(); }
            //if (Program.edificioV != "") { txtEdificio.Text = Program.edificioV.Trim(); }
            //if (Program.deptoV != "") { txtDepto.Text = Program.deptoV.Trim(); }
        }

        private void llenarCombosNormales()
        {
            cboTipoPredio.Items.Clear();
            cboTipoPredio.Items.Add("1 URBANO");
            cboTipoPredio.Items.Add("0 RUSTICO");

            cboRegimenPropiedad.Items.Clear();
            cboRegimenPropiedad.Items.Add("0 SIN DESCRIPCION");
            cboRegimenPropiedad.Items.Add("1 PRIVADA INDIVIDUAL");
            cboRegimenPropiedad.Items.Add("2 PRIVADA CONDOMINIO");
            cboRegimenPropiedad.Items.Add("3 EJIDAL");
            cboRegimenPropiedad.Items.Add("4 COMUNAL");
            cboRegimenPropiedad.Items.Add("5 COMUN REPARTIMIENTO");
            cboRegimenPropiedad.Items.Add("6 FEDERAL");
            cboRegimenPropiedad.Items.Add("7 ESTATAL");
            cboRegimenPropiedad.Items.Add("8 MUNICIPAL");

            cboUbicacion.Items.Clear();
            cboUbicacion.Items.Add("0 SIN DESCRIPCION");
            cboUbicacion.Items.Add("1 INTERMEDIO");
            cboUbicacion.Items.Add("2 ESQUINERO");
            cboUbicacion.Items.Add("3 CABECERO");
            cboUbicacion.Items.Add("4 MANZANERO");
            cboUbicacion.Items.Add("5 FRENTES NO CONTIGUOS");
            cboUbicacion.Items.Add("6 INTERIOR");

            cboUsoSuelo.Items.Clear();
            cboUsoSuelo.Items.Add("- SIN DESCRIPCION");
            cboUsoSuelo.Items.Add("A AGRICOLA");
            cboUsoSuelo.Items.Add("B AGOSTADERO");
            cboUsoSuelo.Items.Add("C COMERCIAL");
            cboUsoSuelo.Items.Add("E EQUIPAMIENTO");
            cboUsoSuelo.Items.Add("F FORESTAL");
            cboUsoSuelo.Items.Add("G ERIAZO");
            cboUsoSuelo.Items.Add("H HABITACIONAL");
            cboUsoSuelo.Items.Add("I INDUSTRIAL");
            cboUsoSuelo.Items.Add("L ESPECIAL");
            cboUsoSuelo.Items.Add("Q EQUIPAMIENTO");

            cboDestino.Items.Clear();
            //cboDestino.Items.Add("0 SIN DESCRIPCION");
            //cboDestino.Items.Add("1 PRIVADA INDIVIDUAL");
            //cboDestino.Items.Add("2 PRIVADA CONDOMINIO");
            //cboDestino.Items.Add("3 EJIDAL");
            //cboDestino.Items.Add("4 COMUNAL");
            //cboDestino.Items.Add("5 COMUN REPARTIMIENTO");
            //cboDestino.Items.Add("6 FEDERAL");
            //cboDestino.Items.Add("7 ESTATAL");
            //cboDestino.Items.Add("8 MUNICIPAL");

        }

        private int llenarCombosBaseDatos(int Y)
        {
            ///////////////////////////////////////Llenamos el combo de calles
            cboCalle.Items.Clear();
            con.conectar_base_interno();
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT c.CodCalle, c.NomCalle";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM CALLES c";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Estado    = " + Program.Vestado;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Municipio = " + Program.Vmunicipio;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND ZonaOrig = " +  Convert.ToInt32(txtZonaOrigen.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY c.CodCalle";
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            while (con.leer_interno.Read())
            {
                cboCalle.Items.Add(con.leer_interno[0].ToString().Trim().PadLeft(3, '0') + " " + con.leer_interno[1].ToString().Trim());
            }
            con.cerrar_interno();

            if (cboCalle.Items.Count <= 0) { Y = 0; }
            else { Y = 1; }

            if (tipoDeMovimiento != 1)
            {
                /////////////////////////////////////Llenamos el combo de destino
                cboDestino.Items.Clear();
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT UsoEsp, Descrip ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM DESTINO ";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Uso  = '" + cboUsoSuelo.Text.Trim().Substring(0, 1) + "'";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    cboDestino.Items.Add(con.leer_interno[0].ToString().Trim() + " " + con.leer_interno[1].ToString().Trim());
                }
                con.cerrar_interno();

                if (cboDestino.Items.Count <= 0) { Y = 0; }
                else { Y = 1; }
            }
            return Y;
        }

        private void limparCombos()
        {
            cboTipoPredio.Items.Clear();
            cboCalle.Items.Clear();
            cboRegimenPropiedad.Items.Clear();
            cboUbicacion.Items.Clear();

            cboUsoSuelo.Items.Clear();
            cboDestino.Items.Clear();
        }

        private void consultaGeneral()
        {

            limpiarTodoNoClaveYfolioNoDGVmovimiento();
            llenarCombosNormales();

            // ----------------------------------------------------------------------------------------------------------------------- //
            //-------------------------------------------- consulta para ver si la clave catastral se encuentra bloqueada
            int resultado = consultaBloqueo(0);         // El resultado es 0 no esta bloqueada, 2 si esta bloqueada
            if (resultado == 1) { inicio(); return; }
            if (resultado == 2) { MessageBox.Show("LA CLAVE CATASTRAL SE ENCUENTRA BLOQUEADA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }
            // ----------------------------------------------------------------------------------------------------------------------- //

            if (tipoDeMovimiento == 1 || tipoDeMovimiento == 3)      //****************************** Altas y Cambios **********
            {
                // ----------------------------------------------------------------------------------------------------------------------- //
                //-------------------------------------------- consulta para ver SI el movimiento esta autorizado
                int resultado1 = consultaSiEstaAutorizado(0);         // El resultado es 0 SI esta autorizado, 2 NO esta autorizado
                if (resultado1 == 1) { MessageBox.Show("NO SE TIENE SERIE Y FOLIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }
                if (resultado1 == 2) { inicio(); return; }
                if (resultado1 == 3) { MessageBox.Show("NO SE TIENE INFORMACION CON ESTE SERIE Y FOLIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }
                if (resultado1 == 4) { MessageBox.Show("YA SE HISO ESTE PROCESO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }
                if (resultado1 == 5) { MessageBox.Show("SE ELIMINO EL PROCESO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }
                if (resultado1 == 6) { MessageBox.Show("NO SE HA AUTORIZADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }
                if (resultado1 == 7) { MessageBox.Show("ERROR EN LA INFORMACION DE LA BASE DE DATOS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }


                // ----------------------------------------------------------------------------------------------------------------------- //

                if (tipoDeMovimiento == 1)      //****************************** Altas **********
                {
                    // ----------------------------------------------------------------------------------------------------------------------- //
                    //-------------------------------------------- 
                    int resultado1_1 = llenamosLasCajasDeTextoDeAltas(0);
                    if (resultado1_1 == 1) { inicio(); return; }

                    btnCalculo.Enabled = false;
                    btnAceptar.Enabled = true;
                    btnRefresh.Enabled = true;
                }

                if (tipoDeMovimiento == 3)      //****************************** Cambios **********
                {
                    int resultado2 = ConsultaGeneral(0);
                    if (resultado2 == 1) { inicio(); return; }
                    if (resultado2 == 2) { MessageBox.Show("NO SE ENCONTRO NINGUNA CLAVE CATASTRAL", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }
                    if (resultado2 == 3) { MessageBox.Show("NO SE TIENE NINGUNA CALLE o DESTINO ASIGNADO", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }

                    // ----------------------------------------------------------------------------------------------------------------------- //
                    //-------------------------------------------- revisar que si sea de cambio de datos en la clave catastral

                    int resultado3 = consultaTipoDeMovimiento(0);
                    if (resultado3 == 1) { inicio(); return; }
                    if (resultado3 == 2) { MessageBox.Show("NO SE TIENE NINGUNA DESCRIPCION DE LA CONSULTA", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }

                    if (descripcionConsulta.Substring(5, 15).Trim() != "CAMBIO DE CLAVE") { MessageBox.Show("LA CLAVE CATASTRAL Y EL FOLIO NO CORRESPONDE A UN CAMBIO EN LA CLAVE CATASTRAL", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }

                    string tipoCambioArealizar = "";
                    tipoCambioArealizar = descripcionConsulta.Substring(0, 5).Trim();

                    int resultado4 = inabilitarCajasPorMovimiento(tipoCambioArealizar);

                    if (resultado4 == 1)
                    {
                        btnCancelar.Enabled = true;
                        cmdSalida.Enabled = true;
                        btnMinimizar.Enabled = true;
                        pnlDatosPredio.Enabled = true;
                        pnlDatosPropiedades.Enabled = true;
                        btnCalculo.Enabled = true;
                        btnAceptar.Enabled = true;

                        txtPropietario.Enabled = false;
                        txtValorTerrenoPropio.Enabled = true;
                        txtValorTerrenoComun.Enabled = true;
                        txtValorConstPropia.Enabled = true;
                        txtValorConstComun.Enabled = true;
                        txtValorCatastral.Enabled = true;

                        txtObservacionPro.Enabled = true;
                        txtObservacionPro.BackColor = System.Drawing.Color.GreenYellow;


                    }

                    btnCalculo.Enabled = true;
                    btnAceptar.Enabled = true;
                    btnRefresh.Enabled = true;

                }
            }   //****************************** Altas y Cambios **********

            // ----------------------------------------------------------------------------------------------------------------------- //
            //-------------------------------------------- Consulta general para las opciones de generales y  bajas

            if (tipoDeMovimiento == 2 || tipoDeMovimiento == 4 || tipoDeMovimiento == 5)     //**************** Bajas, Consultas y Cambios Generales **********
            {
                int resultado3 = ConsultaGeneral(0);
                if (resultado3 == 1) { inicio(); return; }
                if (resultado3 == 2) { MessageBox.Show("NO SE ENCONTRO NINGUNA CLAVE CATASTRAL", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }
                if (resultado3 == 3) { MessageBox.Show("NO SE TIENE NINGUNA CALLE o DESTINO ASIGNADO", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error); inicio(); return; }

                /********************************************************************************/
                /***** habilitamos los paneles de informacion   *********************************/

                inabilitarClaveYfolio();
                pnlDatosPredio.Enabled = true;
                pnlDatosPropiedades.Enabled = true;

                abilitarEtiquetas();

                btnConsulta.Enabled = false;
                btnBuscar.Enabled = false;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                btnMaps.Enabled = true;
                btnConstLote.Enabled = true;
                btnConstComun.Enabled = true;


                if (tipoDeMovimiento == 2)      /****************************** Bajas **********/
                {
                    txtPropietario.Enabled = false;
                    btnCalculo.Enabled = false;
                    btnAceptar.Enabled = true;
                    btnRefresh.Enabled = true;
                }

                if (tipoDeMovimiento == 4)      /****************************** CONSULTA **********/
                {
                    txtPropietario.Enabled = false;
                    btnCalculo.Enabled = false;
                    btnAceptar.Enabled = false;
                    btnRefresh.Enabled = true;
                }

                if (tipoDeMovimiento == 5)      /****************************** CAMBIOS GENERALES **********/
                {
                    cboTipoPredio.Enabled = true;
                    txtDomicilioPredio.Enabled = true;
                    txtZonaOrigen.Enabled = true;
                    cboCalle.Enabled = true;
                    txtNoExterior.Enabled = true;
                    txtEnCalle.Enabled = true;
                    txtYcalle.Enabled = true;
                    txtCodigoPostal.Enabled = true;
                    txtNoIntrior.Enabled = true;
                    txtDomicilioPropietario.Enabled = true;
                    txtDomicilioFiscal.Enabled = true;

                    cboTipoPredio.BackColor = System.Drawing.Color.GreenYellow;
                    txtDomicilioPredio.BackColor = System.Drawing.Color.GreenYellow;
                    txtZonaOrigen.BackColor = System.Drawing.Color.GreenYellow;
                    cboCalle.BackColor = System.Drawing.Color.GreenYellow;
                    txtNoExterior.BackColor = System.Drawing.Color.GreenYellow;
                    txtEnCalle.BackColor = System.Drawing.Color.GreenYellow;
                    txtYcalle.BackColor = System.Drawing.Color.GreenYellow;
                    txtCodigoPostal.BackColor = System.Drawing.Color.GreenYellow;
                    txtNoIntrior.BackColor = System.Drawing.Color.GreenYellow;
                    txtDomicilioPropietario.BackColor = System.Drawing.Color.GreenYellow;
                    txtDomicilioFiscal.BackColor = System.Drawing.Color.GreenYellow;

                    txtPropietario.Enabled = false; 
                    btnCalculo.Enabled = false;
                    btnAceptar.Enabled = true ;
                    btnRefresh.Enabled = true;
                    txtDomicilioPredio.Focus();
                    txtObservaciones.Enabled = true;
                    txtObservaciones.BackColor = System.Drawing.Color.GreenYellow;

                }
            }   //****************************** Bajas y Generales **********
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            if (txtMun.Text.Trim()      == "") { MessageBox.Show("NO SE TIENE EL MUNICIPIO", "ERROR", MessageBoxButtons.OK); txtMun.Focus(); return; }
            if (txtZona.Text.Trim()     == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtZona.Text.Length       < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim()     == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length       < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim()     == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtLote.Text.Length       < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length   < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim()    == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length      < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            if (mtcInformacion.SelectedIndex == 1) // altas
            {
                if (txtSerie.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SERIE", "ERROR", MessageBoxButtons.OK); txtMun.Focus(); return; }
                if (txtFolio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FOLIO", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            }// Altas

            if (mtcInformacion.SelectedIndex == 3) // cambios
            {
                if (txtSerie.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SERIE", "ERROR", MessageBoxButtons.OK); txtMun.Focus(); return; }
                if (txtFolio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FOLIO", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            }// Cambios

            consultaGeneral();
        }

        private int llenamosLasCajasDeTextoDeAltas(int Y) 
        {
            string MUNICIPIO_C2 = "";
            string ZONA_C2      = "";
            string MANZANA_C2   = "";
            string LOTE_C2      = "";
            string EDIFICIO_C2  = "";
            string DEPTO_C2     = "";
            string FOLIO_C2     = "";
            string TIPO_CAT     = "";
            string REGIMEN_CC   = "";
            string UBICACION_CC = "";
            string SERIE_C2     = "";

            int area_homo = 0;
            int Autorizado = 0;
            int regPropiedad = 0;
            int posicionUbi = 0;

            facFrente   = 0;
            facFondo    = 0;
            facIrreg    = 0;
            facArea     = 0;
            facTopo     = 0;
            facPosicion = 0; 

            MUNICIPIO_C2 = txtMun.Text.Trim();
            ZONA_C2      = txtZona.Text.Trim();
            MANZANA_C2   = txtMzna.Text.Trim();
            LOTE_C2      = txtLote.Text.Trim();
            EDIFICIO_C2  = txtEdificio.Text.Trim();
            DEPTO_C2     = txtDepto.Text.Trim();
            FOLIO_C2     = txtFolio.Text.Trim();
            SERIE_C2     = txtSerie.Text.Trim();

            try  //------------------------------- consulta para ver si la clave catastral se encuentra bloqueada
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT ZON_ORIGEN, COD_CALLE, REGIMEN, POSICION, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "        TERR_PROPIO, TERR_COMUN, SUP_CON_COM, SUP_CON,";
                con.cadena_sql_interno = con.cadena_sql_interno + "        FRENTE, FONDO, IRREGULARIDAD, TOPOGRAFIA, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "        VAL_TERRENO, VAL_TERRENO_COMUN, VAL_CONST, VAL_CONST_COMUN, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "        FAC_FRENTE, FAC_FONDO, FAC_IRREG, FAC_AREA, FAC_TOPO, FAC_POSICION";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where ESTADO       = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND MUNICIPIO    = "  + MUNICIPIO_C2;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND ZONA         = "  + ZONA_C2;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND MANZANA      = "  + MANZANA_C2;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND LOTE         = "  + LOTE_C2;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND EDIFICIO     = '" + EDIFICIO_C2 + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND DEPTO        = '" + DEPTO_C2    + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FOLIO_ORIGEN = "  + FOLIO_C2;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND SERIE        = '" + SERIE_C2    + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "") { Autorizado = 0; }                // NO EXISTEN DATOS
                    else
                    {
                        Autorizado = 1;                                                                 // EXISTEN DATOS
                        txtZonaOrigen.Text           = con.leer_interno[0].ToString().Trim();
                        txtCodigoCalle.Text          = con.leer_interno[1].ToString().Trim().PadLeft(3, '0');         //padleft
                        regPropiedad                 = Convert.ToInt32(con.leer_interno[2].ToString().Trim());
                        posicionUbi                  = Convert.ToInt32(con.leer_interno[3].ToString().Trim());

                        txtSupTerreno.Text           = con.leer_interno[4].ToString().Trim();
                        txtSupTerrenoComun.Text      = con.leer_interno[5].ToString().Trim();
                        txtSupConstruccionComun.Text = con.leer_interno[6].ToString().Trim();
                        txtSupConstruccion.Text      = con.leer_interno[7].ToString().Trim();

                        txtFrente.Text               = con.leer_interno[8].ToString().Trim();
                        txtFondo.Text                = con.leer_interno[9].ToString().Trim();
                        txtDesnivel.Text             = con.leer_interno[10].ToString().Trim();
                        txtArea.Text                 = con.leer_interno[11].ToString().Trim();

                        txtValorTerrenoPropio.Text   = con.leer_interno[12].ToString().Trim();
                        txtValorTerrenoComun.Text    = con.leer_interno[13].ToString().Trim();
                        txtValorConstPropia.Text     = con.leer_interno[14].ToString().Trim();
                        txtValorConstComun.Text      = con.leer_interno[15].ToString().Trim();

                        txtIndiviso.Text = "0";
                        txtValorCatastral.Text = (Convert.ToDouble(txtValorTerrenoPropio.Text.Trim()) + Convert.ToDouble(txtValorTerrenoComun.Text.Trim()) + Convert.ToDouble(txtValorConstPropia.Text.Trim()) + Convert.ToDouble(txtValorConstComun.Text.Trim())).ToString();

                        facFrente   = Convert.ToDecimal(con.leer_interno[16].ToString().Trim());
                        facFondo    = Convert.ToDecimal(con.leer_interno[17].ToString().Trim());
                        facIrreg    = Convert.ToDecimal(con.leer_interno[18].ToString().Trim());
                        facArea     = Convert.ToDecimal(con.leer_interno[19].ToString().Trim());
                        facTopo     = Convert.ToDecimal(con.leer_interno[20].ToString().Trim());
                        facPosicion = Convert.ToDecimal(con.leer_interno[21].ToString().Trim());
                    }
                }
                con.cerrar_interno();

                int resultadoCboCalles3 = llenarCombosBaseDatos(0);
                if (resultadoCboCalles3 == 0) { return 1; }

                string codigoCalleV2 = txtCodigoCalle.Text.Trim();
                if (codigoCalleV2.Length == 1) { codigoCalleV2 = codigoCalleV2 + " "; }

                for (int i = 0; i < cboCalle.Items.Count; i++)
                {
                    // Obtener el texto del ítem actual
                    string itemTexto = cboCalle.Items[i].ToString();

                    // Verificar si los dos primeros carácter coincide con el codigo de calle
                    if (itemTexto.Length > 0 && itemTexto.Substring(0, 3) == codigoCalleV2)
                    {
                        // Seleccionar el ítem correspondiente
                        cboCalle.SelectedIndex = i;
                        break; // Salir del bucle una vez encontrado
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1; // Retornar false si ocurre un error
            }

            //---------------------------------------------------------------------------------------------------------//
            //------------------------------- ingresamos la calle con el codigo de calle

            int resultadoCboCalles = llenarCombosBaseDatos(0);
            if (resultadoCboCalles == 0) { return 1; }

            int resultadoCboCalles2 = llenarCombosBaseDatos(0);
            if (resultadoCboCalles2 == 0) { return 1; }

            string codigoCalleV = txtCodigoCalle.Text.Trim();
            if (codigoCalleV.Length == 1) { codigoCalleV = codigoCalleV + " "; }

            for (int i = 0; i < cboCalle.Items.Count; i++)
            {
                // Obtener el texto del ítem actual
                string itemTexto = cboCalle.Items[i].ToString();

                // Verificar si los dos primeros carácter coincide con el codigo de calle
                if (itemTexto.Length > 0 && itemTexto.Substring(0, 3) == codigoCalleV)
                {
                    // Seleccionar el ítem correspondiente
                    cboCalle.SelectedIndex = i;
                    break; // Salir del bucle una vez encontrado
                }
            }

            //---------------------------------------------------------------------------------------------------------//
            //------------------------------- ingresamos ubicacion

            cboUbicacion.SelectedIndex = posicionUbi;

            //---------------------------------------------------------------------------------------------------------//
            //------------------------------- ingresamos las superficies de terreno y constr. de predios a propiedades

            txtSupTerrenoPro.Text = txtSupTerreno.Text.Trim();
            txtSupTerrenoComunPro.Text = txtSupTerrenoComun.Text.Trim();
            txtSupConstruccionPro.Text = txtSupConstruccion.Text.Trim();
            txtSupConstruccionComunPro.Text = txtSupConstruccionComun.Text.Trim();

            //---------------------------------------------------------------------------------------------------------//
            //------------------------------- colocamos la colonia

            int colonia_gV = 0;
            int area_homoV = 0;

            try  
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT Colonia, AreaHom";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM MANZANAS";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where ZONA       = " + ZONA_C2;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana    = " + MANZANA_C2;

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")        // NO EXISTEN DATOS 
                    {
                        colonia_gV = 0;
                        area_homoV = 1;
                    }                
                    else
                    {
                        colonia_gV = Convert.ToInt32 (con.leer_interno[0].ToString().Trim());
                        area_homoV = Convert.ToInt32 (con.leer_interno[1].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1; // Retornar false si ocurre un error
            }

            //---------------------------------------------------------------------------------------------------------//
            //------------------------------- ingresamos la colonia en la caja de texto

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT NomCol, cCPCol";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM COLONIAS";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where Estado       = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Municipio    = " + txtMun.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND colonia      = " + colonia_gV;

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")        // NO EXISTEN DATOS 
                    {
                        txtColonia.Text = "SIN NOMBRE";
                        txtCodigoPostal.Text = "00000";
                    }
                    else
                    {
                        txtColonia.Text = con.leer_interno[0].ToString().Trim();
                        txtCodigoPostal.Text = con.leer_interno[1].ToString().Trim();
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 1; // Retornar false si ocurre un error
            }

            txtObservacionPro.Visible = true;
            txtObservacionPro.Enabled = true;
            txtObservacionPro.Focus();

            if (txtSupTerreno.Text.Trim()               == "") { txtSupTerreno.Text = "0"; }
            if (txtSupTerrenoComun.Text.Trim()          == "") { txtSupTerrenoComun.Text = "0"; }
            if (txtSupConstruccion.Text.Trim()          == "") { txtSupConstruccion.Text = "0"; }
            if (txtSupConstruccionComun.Text.Trim()     == "") { txtSupConstruccionComun.Text = "0"; }
            if (txtSupTerrenoPro.Text.Trim()            == "") { txtSupTerrenoPro.Text = "0"; }
            if (txtSupTerrenoComunPro.Text.Trim()       == "") { txtSupTerrenoComunPro.Text = "0"; }
            if (txtSupConstruccionPro.Text.Trim()       == "") { txtSupConstruccionPro.Text = "0"; }
            if (txtSupConstruccionComunPro.Text.Trim()  == "") { txtSupConstruccionComunPro.Text = "0"; }

            if ((Convert.ToDecimal(txtSupConstruccionPro.Text.Trim()) + Convert.ToDecimal(txtSupConstruccionComunPro.Text.Trim())) > 0)
            {
                lblEdoPredio.Text = "1 CONSTRUIDO";
            }
            else 
            { 
                lblEdoPredio.Text = "0 BALDIO"; 
            }

            inabilitarPrediosYpropiedades();

            cboTipoPredio.Enabled = true;
            txtDomicilioPredio.Enabled = true;
            txtNoExterior.Enabled = true;
            txtEnCalle.Enabled = true;
            txtYcalle.Enabled = true;
            txtPropietario.Enabled = true;
            txtNoIntrior.Enabled = true;
            txtDomicilioPropietario.Enabled = true;
            txtDomicilioFiscal.Enabled = true;
            cboUsoSuelo.Enabled = true;
            cboDestino.Enabled = true;
            txtObservacionPro.Enabled = true;
            cboRegimenPropiedad.Enabled = true;

            btnCalculo.Enabled = true;
            btnAceptar.Enabled = true;

            btnConsulta.Enabled = false;

            btnCancelar.Enabled = true;
            btnBuscar.Enabled = false;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;

            pnlDatosPredio.Enabled = true;
            pnlDatosPropiedades.Enabled = true;

            btnConstLote.Enabled = true;
            btnConstComun.Enabled = true;

            cboTipoPredio.Focus();

            return 2;
        }

        private int inabilitarCajasPorMovimiento(string y)
        {
            inabilitarTodos();
            string variableTipoCambio = y;
            if (variableTipoCambio.Substring(0,1) == "1")               
            {
                txtPropietario.Enabled = false;
            }        //CAMBIO DE NOMBRE

            if (variableTipoCambio.Substring(1, 1) == "1")              
            {
                if (txtEdificio.Text.Trim() == "00" && txtDepto.Text.Trim() == "0000")  // es lote
                {
                    txtSupTerreno.Enabled = true;
                    txtSupTerrenoComun.Enabled = false;

                    txtSupTerrenoPro.Enabled = true;
                    txtSupTerrenoComunPro.Enabled = true;

                    txtSupTerreno.BackColor = System.Drawing.Color.White;
                    txtSupTerrenoComun.BackColor = System.Drawing.Color.White;
                    txtSupTerrenoPro.BackColor = System.Drawing.Color.GreenYellow;
                    txtSupTerrenoComunPro.BackColor = System.Drawing.Color.GreenYellow;
                }

                else  // es edificio o departamento
                {
                    txtSupTerrenoPro.Enabled = true;
                    txtSupTerrenoPro.BackColor = System.Drawing.Color.GreenYellow;
                }
            }       //CAMBIO DE SUPERFICIE

            if (variableTipoCambio.Substring(2, 1) == "1")         
            {
                if (txtEdificio.Text.Trim() == "00" && txtDepto.Text.Trim() == "0000")  // es lote
                {
                    btnConstLote.Enabled = true;
                    btnConstComun.Enabled = true;
                    Program.tipoUbicacionCartografia = 1; //vairable se ve el mnuevo en pantalla de cosntruccion
                }
                else  
                {
                    btnConstLote.Enabled = true;
                }
            }       //CAMBIO EN CONSTRUCCION

            if (variableTipoCambio.Substring(3, 1) == "1")
            {
                if (txtEdificio.Text.Trim() == "00" && txtDepto.Text.Trim() == "0000")  // es lote
                {
                    btnConstLote.Enabled = true;
                    btnConstComun.Enabled = true;
                }
                else
                {
                    btnConstLote.Enabled = true;
                }
            }       //CAMBIO EN FACTORES DE CONSTRUCCION

            if (variableTipoCambio.Substring(4, 1) == "1")
            {
                if (txtEdificio.Text.Trim() == "00" && txtDepto.Text.Trim() == "0000")  // es lote
                {
                    cboUbicacion.Enabled    = true;
                    txtFrente.Enabled       = true;
                    txtFondo.Enabled        = true;
                    txtDesnivel.Enabled     = true;
                    txtArea.Enabled         = true;
                    cboUsoSuelo.Enabled     = true;
                    cboDestino.Enabled      = true;

                    cboUbicacion.BackColor  = System.Drawing.Color.GreenYellow;
                    txtFrente.BackColor     = System.Drawing.Color.GreenYellow;
                    txtFondo.BackColor      = System.Drawing.Color.GreenYellow;
                    txtDesnivel.BackColor   = System.Drawing.Color.GreenYellow;
                    txtArea.BackColor       = System.Drawing.Color.GreenYellow;
                    cboUsoSuelo.BackColor   = System.Drawing.Color.GreenYellow;
                    cboDestino.BackColor    = System.Drawing.Color.GreenYellow;
                }
            }       //CAMBIO EN FACTORES DE TERRENO


            txtObservacionPro.BackColor = System.Drawing.Color.GreenYellow;
            return 1;
        } 

        private int consultaBloqueo(int Y)
        {
            //-------------------------------------------- consulta para ver si la clave catastral se encuentra bloqueada

            int bloqueoCat = 0;

            Program.municipioV = Program.Vmunicipio;
            Program.zonaV = txtZona.Text.Trim();
            Program.manzanaV = txtMzna.Text.Trim();
            Program.loteV = txtLote.Text.Trim();
            Program.edificioV = txtEdificio.Text.Trim();
            Program.deptoV = txtDepto.Text.Trim();

            try  //------------------------------- consulta para ver si la clave catastral se encuentra bloqueada
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT Zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE Estado = 15 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Municipio = " + Program.municipioV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Zona =      " + Program.zonaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana =   " + Program.manzanaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Lote =      " + Program.loteV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Edificio =  " + Program.edificioV;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Depto =     " + Program.deptoV + " )";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "") { bloqueoCat = 0; }
                    else { bloqueoCat = Convert.ToInt32(con.leer_interno[0].ToString().Trim()); }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 1;
                // Retornar false si ocurre un error
            }

            if (bloqueoCat == 1) { return 2; }      // la clave catastral se encuentra bloqueada
            else                 { return 0; }      // la clave catastral no se encuentra bloqueada
            //---------------------------------------------------------------------------------------------------------//

        }

        private int consultaSiEstaAutorizado(int Y)
        {
            //-------------------------------------------- consulta para saber si esta autorizado el movimiento con serie y folio

            int Autorizado = 0;
            int CAR = 0;
            int VEN = 0;
            int REV = 0;
            int SIS = 0;
            int ELI = 0;

            if (   txtSerie.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SERIE",    "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (   txtFolio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FOLIO",    "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (    txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA",     "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (    txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA",  "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (    txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE",     "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (   txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPTO",    "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }

            Program.SerieC = txtSerie.Text.Trim();
            Program.FolioC = Convert.ToInt32(txtFolio.Text.Trim());

            try  //--------------------------------------- consulta para saber si esta autorizado el movimiento con serie y folio
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT cdv.CARTOGRAFIA, cdv.VENTANILLA, cdv.REVISO, cdv.SISTEMAS, cdv.ELIMINADO";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_DONDE_VA_2025 cdv, CAT_NEW_CARTOGRAFIA_2025 cnc";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE cdv.FOLIO_ORIGEN  =   " + Program.FolioC;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.SERIE         =  '" + Program.SerieC + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.FOLIO_ORIGEN  = cnc.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.SERIE         = cnc.SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.MUNICIPIO     =  " + Convert.ToInt32( txtMun.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.ZONA          =  " + Convert.ToInt32(txtZona.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.MANZANA       =  " + Convert.ToInt32(txtMzna.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.LOTE          =  " + Convert.ToInt32(txtLote.Text.Trim());   
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.EDIFICIO      = '" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.DEPTO         = '" + txtDepto.Text.Trim()    + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "") { Autorizado = 0; }                // NO EXISTEN DATOS
                    else
                    {
                        Autorizado = 1;                                                                 // EXISTEN DATOS

                        CAR = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                        VEN = Convert.ToInt32(con.leer_interno[1].ToString().Trim());
                        REV = Convert.ToInt32(con.leer_interno[2].ToString().Trim());
                        SIS = Convert.ToInt32(con.leer_interno[3].ToString().Trim());
                        ELI = Convert.ToInt32(con.leer_interno[4].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 2;
                // Retornar false si ocurre un error
            }

            if (Autorizado == 0) { return 3; }
            else
            {
                if (CAR == 1 && VEN == 1 && REV == 1 && SIS == 1 && ELI == 0) { return 4; }            //YA SE REALISO ESTE PROCESO
                if (CAR == 1 && VEN == 1 && REV == 1 && SIS == 0 && ELI == 1) { return 5; }            //SE ELIMINO EL PROCESO
                if (CAR == 1 && VEN == 1 && REV == 0 && SIS == 0 && ELI == 0) { return 6; }            //NO SE A AUTORIZADO
                if (CAR == 1 && VEN == 1 && REV == 0 && SIS == 1 && ELI == 0) { return 7; }            //ERROR DE INFORMACION EN LA BASE DE DATOS
                if (CAR == 1 && VEN == 1 && REV == 1 && SIS == 0 && ELI == 0) { return 8; }            //ESTA AUTORIZADO
            }

            return 2;
            //---------------------------------------------------------------------------------------------------------//

        }

        private int consultaTipoDeMovimiento(int Y)
        {
            //-------------------------------------------- consulta para saber si esta autorizado el movimiento con serie y folio

            string descripcion = "";

            if (txtSerie.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SERIE", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (txtFolio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FOLIO", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            Program.SerieC = txtSerie.Text.Trim();
            Program.FolioC = Convert.ToInt32(txtFolio.Text.Trim());

            descripcion = "";

            try  //--------------------------------------- consulta para saber si esta autorizado el movimiento con serie y folio
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT DESCRIPCION";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE FOLIO_ORIGEN =  " + Program.FolioC;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND        SERIE = '" + Program.SerieC  + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND       ESTADO = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND    MUNICIPIO = "  + txtMun.Text.Trim()   ;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND         ZONA = "  + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND      MANZANA = "  + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND         LOTE = "  + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND     EDIFICIO = '" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND        DEPTO = '" + txtDepto.Text.Trim()    + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "") { descripcion = ""; }               // 3 no existe en la tabla
                    else { descripcion = con.leer_interno[0].ToString().Trim(); }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 1;
                // Retornar false si ocurre un error
            }


            if (descripcion == "") 
            {
                descripcionConsulta = "";
                return 2; 
            }                           // no hay descripcion del movimiento
            else 
            {
                descripcionConsulta = descripcion;
                return 0; 
            }                         // si hay descripcion del movimiento
            //---------------------------------------------------------------------------------------------------------//

        }

        private void inabilitar_notones()
        {
            btnMinimizar.Enabled = true;
            btnConsulta.Enabled = false;
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = false;
            cmdSalida.Enabled = false;

            btnAceptar.Enabled = false;
            btnCalculo.Enabled = false;
        }

        private void inabilitarPrediosYpropiedades()
        {
            inabilitar_notones();

            btnConstLote.Enabled = false;
            btnConstComun.Enabled = false;

            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            txtSerie.Enabled = false;
            txtFolio.Enabled = false;
            txtNoIntrior.Enabled = false;

            pnlDatosPredio.Enabled = false;
            cboTipoPredio.Enabled = false;
            lblEdoPredio.Enabled = false;
            txtDomicilioPredio.Enabled = false;
            txtZonaOrigen.Enabled = false;
            txtCodigoCalle.Enabled = false;
            cboCalle.Enabled = false;
            txtNoExterior.Enabled = false;
            txtEnCalle.Enabled = false;
            txtYcalle.Enabled = false;
            txtCodigoPostal.Enabled = false;
            txtColonia.Enabled = false;

            cboRegimenPropiedad.Enabled = false;
            txtSupTerreno.Enabled = false;
            txtSupTerrenoComun.Enabled = false;
            txtFrente.Enabled = false;
            txtFondo.Enabled = false;
            txtObservaciones.Enabled = false;

            cboUbicacion.Enabled = false;
            txtSupConstruccion.Enabled = false;
            txtSupConstruccionComun.Enabled = false;
            txtDesnivel.Enabled = false;
            txtArea.Enabled = false;

            pnlDatosPropiedades.Enabled = false;
            txtPropietario.Enabled = false;
            txtDomicilioPropietario.Enabled = false;
            txtDomicilioFiscal.Enabled = false;
            cboUsoSuelo.Enabled = false;
            txtSupTerrenoPro.Enabled = false;
            txtSupTerrenoComunPro.Enabled = false;
            txtSupConstruccionPro.Enabled = false;
            txtSupConstruccionComunPro.Enabled = false;
            txtIndiviso.Enabled = false;

            cboDestino.Enabled = false;
            txtValorTerrenoPropio.Enabled = false;
            txtValorTerrenoComun.Enabled = false;
            txtValorConstPropia.Enabled = false;
            txtValorConstComun.Enabled = false;
            txtValorCatastral.Enabled = false;
            txtObservacionPro.Enabled = false;

        }

        private int ConsultaGeneral(int Y)
        {
            if (txtZona.Text.Trim()     == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (txtZona.Text.Length       < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (txtMzna.Text.Trim()     == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return 1; }
            if (txtMzna.Text.Length       < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return 1; }
            if (txtLote.Text.Trim()     == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return 1; }
            if (txtLote.Text.Length       < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return 1; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return 1; }
            if (txtEdificio.Text.Length   < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return 1; }
            if (txtDepto.Text.Trim()    == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return 1; }
            if (txtDepto.Text.Length      < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return 1; }

            Program.municipioV = Program.Vmunicipio;
            Program.zonaV = txtZona.Text.Trim();
            Program.manzanaV = txtMzna.Text.Trim();
            Program.loteV = txtLote.Text.Trim();
            Program.edificioV = txtEdificio.Text.Trim();
            Program.deptoV = txtDepto.Text.Trim();

            //------------------------------- consulta general para llenar los datos del predio y propiedades
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT pr.TipoPredio, pr.Domicilio, pr.ZonaOrig, pr.CodCalle, c.NomCalle,";
                con.cadena_sql_interno = con.cadena_sql_interno + "       pr.NumExt, pr.EntCalle, pr.YCalle, pr.CodPost, co.Colonia, co.NomCol, re.RegProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "       pr.Ubicacion, f.DescFact, pr.SupTerrTot, pr.SupCons, pr.SupTerrCom, pr.SupConsCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "       pr.Frente, pr.Fondo, pr.Desnivel, pr.AreaInscr, pr.cObsPred,";
                con.cadena_sql_interno = con.cadena_sql_interno + "       p.NumIntP, p.PmnProp, p.DomFis, p.Uso, d.UsoEsp, d.Descrip, p.STerrProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "       p.STerrCom, p.SConsProp, p.SConsCom, p.PtjeCondom, p.cObsProp";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM PROPIEDADES p, PREDIOS pr, CALLES c, COLONIAS co, REGIMEN re, FACTORES f, DESTINO d";

                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE p.Estado      =  " + Program.Vestado;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Municipio   =  " + Program.municipioV;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Zona        =  " + Program.zonaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Manzana     =  " + Program.manzanaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Lote        =  " + Program.loteV;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Edificio    = '" + Program.edificioV + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Depto       = '" + Program.deptoV + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Estado      = pr.Estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Municipio   = pr.Municipio";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Zona        = pr.Zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Manzana     = pr.Manzana";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Lote        = pr.Lote";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado     = c.Estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio  = c.Municipio";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.ZonaOrig   = c.ZonaOrig";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.CodCalle   = c.CodCalle";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado     = co.Estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio  = co.Municipio";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Colonia    = co.Colonia";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.RegProp    = re.RegProp";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Ubicacion  = f.NumFactor";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND f.AnioVigMD   = " + Program.añoActual;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND f.TipoMerDem  = 6";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Uso         = d.Uso";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.UsoEsp      = d.UsoEsp";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                if (!con.leer_interno.HasRows)
                {
                    MessageBox.Show("NO SE ENCONTRO NINGUNA CLAVE CATASTRAL", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.cerrar_interno();
                    inicio();
                    return 2; // Retornar si no hay resultados
                }

                int tipoPredioV = 0;
                double edoPredioV = 0;
                int regPropV = 0;
                int ubicacionV = 0;


                while (con.leer_interno.Read())
                {
                    tipoPredioV = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    if (tipoPredioV == 0) { cboTipoPredio.SelectedIndex = 1; }
                    if (tipoPredioV == 1) { cboTipoPredio.SelectedIndex = 0; }

                    edoPredioV = Convert.ToInt32(con.leer_interno[31].ToString().Trim()) + Convert.ToInt32(con.leer_interno[32].ToString().Trim());

                    if (edoPredioV > 0) { lblEdoPredio.Text = "1 CONSTRUIDO"; }
                    if (edoPredioV <= 0) { lblEdoPredio.Text = "0 BALDIO"; }

                    txtDomicilioPredio.Text = con.leer_interno[1].ToString().Trim();
                    txtZonaOrigen.Text = con.leer_interno[2].ToString().Trim();
                    txtCodigoCalle.Text = con.leer_interno[3].ToString().Trim().PadLeft(3, '0'); //este comentario

                    txtNoExterior.Text = con.leer_interno[5].ToString().Trim();
                    txtEnCalle.Text = con.leer_interno[6].ToString().Trim();
                    txtYcalle.Text = con.leer_interno[7].ToString().Trim();
                    txtCodigoPostal.Text = con.leer_interno[8].ToString().Trim();
                    txtColonia.Text = con.leer_interno[10].ToString().Trim();

                    regPropV = Convert.ToInt32(con.leer_interno[11].ToString().Trim());
                    cboRegimenPropiedad.SelectedIndex = regPropV;

                    ubicacionV = Convert.ToInt32(con.leer_interno[12].ToString().Trim());
                    cboUbicacion.SelectedIndex = ubicacionV;

                    /* estos debemos de pasar a formato de 2 decimales  */
                    /* --------------------------------------------------------------------------------------- */

                    txtSupTerreno.Text = con.leer_interno[14].ToString().Trim();
                    txtSupConstruccion.Text = con.leer_interno[15].ToString().Trim();
                    txtSupTerrenoComun.Text = con.leer_interno[16].ToString().Trim();
                    txtSupConstruccionComun.Text = con.leer_interno[17].ToString().Trim();
                    txtFrente.Text = con.leer_interno[18].ToString().Trim();
                    txtFondo.Text = con.leer_interno[19].ToString().Trim();
                    txtDesnivel.Text = con.leer_interno[20].ToString().Trim();
                    txtArea.Text = con.leer_interno[21].ToString().Trim();

                    if (txtSupTerreno.Text.Trim() == "") { txtSupTerreno.Text = "0.00"; } else { txtSupTerreno.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupTerreno.Text)); }
                    if (txtSupConstruccion.Text.Trim() == "") { txtSupConstruccion.Text = "0.00"; } else { txtSupConstruccion.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupConstruccion.Text)); }
                    if (txtSupTerrenoComun.Text.Trim() == "") { txtSupTerrenoComun.Text = "0.00"; } else { txtSupTerrenoComun.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupTerrenoComun.Text)); }
                    if (txtSupConstruccionComun.Text.Trim() == "") { txtSupConstruccionComun.Text = "0.00"; } else { txtSupConstruccionComun.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupConstruccionComun.Text)); }
                    if (txtFrente.Text.Trim() == "") { txtFrente.Text = "0.00"; } else { txtFrente.Text = string.Format("{0:#,##0.00}", double.Parse(txtFrente.Text)); }
                    if (txtFondo.Text.Trim() == "") { txtFondo.Text = "0.00"; } else { txtFondo.Text = string.Format("{0:#,##0.00}", double.Parse(txtFondo.Text)); }
                    if (txtDesnivel.Text.Trim() == "") { txtDesnivel.Text = "0.00"; } else { txtDesnivel.Text = string.Format("{0:#,##0.00}", double.Parse(txtDesnivel.Text)); }
                    if (txtArea.Text.Trim() == "") { txtArea.Text = "0.00"; } else { txtArea.Text = string.Format("{0:#,##0.00}", double.Parse(txtArea.Text)); }

                    /* --------------------------------------------------------------------------------------- */

                    txtObservaciones.Text = con.leer_interno[22].ToString().Trim();
                    txtNoIntrior.Text = con.leer_interno[23].ToString().Trim();
                    txtPropietario.Text = con.leer_interno[24].ToString().Trim();
                    txtDomicilioPropietario.Text = con.leer_interno[25].ToString().Trim();
                    txtDomicilioFiscal.Text = con.leer_interno[25].ToString().Trim();

                    usoSueloV = con.leer_interno[26].ToString().Trim();
                    destinoV = con.leer_interno[27].ToString().Trim();

                    /* estos debemos de pasar a formato de 2 decimales  */
                    /* --------------------------------------------------------------------------------------- */

                    txtSupTerrenoPro.Text = con.leer_interno[29].ToString().Trim();
                    txtSupTerrenoComunPro.Text = con.leer_interno[30].ToString().Trim();
                    txtSupConstruccionPro.Text = con.leer_interno[31].ToString().Trim();
                    txtSupConstruccionComunPro.Text = con.leer_interno[32].ToString().Trim();
                    txtIndiviso.Text = con.leer_interno[33].ToString().Trim();

                    if (txtSupTerrenoPro.Text.Trim() == "") { txtSupTerrenoPro.Text = "0.00"; } else { txtSupTerrenoPro.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupTerrenoPro.Text)); }
                    if (txtSupTerrenoComunPro.Text.Trim() == "") { txtSupTerrenoComunPro.Text = "0.00"; } else { txtSupTerrenoComunPro.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupTerrenoComunPro.Text)); }
                    if (txtSupConstruccionPro.Text.Trim() == "") { txtSupConstruccionPro.Text = "0.00"; } else { txtSupConstruccionPro.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupConstruccionPro.Text)); }
                    if (txtSupConstruccionComunPro.Text.Trim() == "") { txtSupConstruccionComunPro.Text = "0.00"; } else { txtSupConstruccionComunPro.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupConstruccionComunPro.Text)); }
                    if (txtIndiviso.Text.Trim() == "") { txtIndiviso.Text = "0.00"; } else { txtIndiviso.Text = string.Format("{0:#,##0.00}", double.Parse(txtIndiviso.Text)); }

                    /* --------------------------------------------------------------------------------------- */

                    txtObservacionPro.Text = con.leer_interno[34].ToString().Trim();
                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar consulta" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }


            for (int i = 0; i < cboUsoSuelo.Items.Count; i++)
            {
                // Obtener el texto del ítem actual
                string itemTexto = cboUsoSuelo.Items[i].ToString();

                // Verificar si el primer carácter coincide con REGIMEN1
                if (itemTexto.Length > 0 && itemTexto.Substring(0, 1) == usoSueloV)
                {
                    // Seleccionar el ítem correspondiente
                    cboUsoSuelo.SelectedIndex = i;
                    break; // Salir del bucle una vez encontrado
                }
            }


            /********************************************************************************/
            /***** llenamos los combos de calles y destinos  ********************************/

            int resultadoCboCalles = llenarCombosBaseDatos(0);
            if (resultadoCboCalles == 0) { return 3; }

            /********************************************************************************/

            string codigoCalleV = txtCodigoCalle.Text.Trim();
            if (codigoCalleV.Length == 1) { codigoCalleV = codigoCalleV + " "; }

            for (int i = 0; i < cboCalle.Items.Count; i++)
            {
                // Obtener el texto del ítem actual
                string itemTexto = cboCalle.Items[i].ToString();

                // Verificar si los dos primeros carácter coincide con el codigo de calle
                if (itemTexto.Length > 0 && itemTexto.Substring(0, 2) == codigoCalleV)
                {
                    // Seleccionar el ítem correspondiente
                    cboCalle.SelectedIndex = i;
                    break; // Salir del bucle una vez encontrado
                }
            }

            for (int i = 0; i < cboDestino.Items.Count; i++)
            {
                // Obtener el texto del ítem actual
                string itemTexto = cboDestino.Items[i].ToString();

                // Verificar si los dos primeros carácter coincide con el codigo de calle
                if (itemTexto.Length > 0 && itemTexto.Substring(0, 2) == destinoV)
                {
                    // Seleccionar el ítem correspondiente
                    cboDestino.SelectedIndex = i;
                    break; // Salir del bucle una vez encontrado
                }
            }

            /********************************************************************************/
            /***** obtenemos los valores catastrales ****************************************/

            try
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("N19_CONSULTA_PREDIO", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ESTADO2", SqlDbType.Int, 2).Value = 15;
                cmd.Parameters.Add("@MUNICIPIO2", SqlDbType.Int, 3).Value = 41;
                cmd.Parameters.Add("@ZONA2", SqlDbType.Int, 2).Value = Convert.ToInt32(Program.zonaV);
                cmd.Parameters.Add("@MANZANA2", SqlDbType.Int, 3).Value = Convert.ToInt32(Program.manzanaV);
                cmd.Parameters.Add("@LOTE2", SqlDbType.Int, 2).Value = Convert.ToInt32(Program.loteV);
                cmd.Parameters.Add("@EDIFICIO2", SqlDbType.VarChar, 2).Value = Program.edificioV;
                cmd.Parameters.Add("@DEPTO2", SqlDbType.VarChar, 4).Value = Program.deptoV;

                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtValorTerrenoPropio.Text = (Convert.ToDouble(rdr["VALOR_TERRENO_P"].ToString().Trim())).ToString("N2");
                    txtValorTerrenoComun.Text = (Convert.ToDouble(rdr["valor_terreno_c"].ToString().Trim())).ToString("N2");
                    txtValorConstPropia.Text = (Convert.ToDouble(rdr["valor_construccion_p"].ToString().Trim())).ToString("N2");
                    txtValorConstComun.Text = (Convert.ToDouble(rdr["valor_construccion_c"].ToString().Trim())).ToString("N2");
                    txtValorCatastral.Text = (Convert.ToDouble(rdr["VALOR_CATASTRAL"].ToString().Trim())).ToString("N2");
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar el proceso N19_CONSULTA_PREDIO" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 1;
                // Retornar false si ocurre un error
            }

            /********************************************************************************/
            /***** obtenemos LATITUD Y LONGITUD      ****************************************/

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT TOP 1  LATITUD, LONGITUD";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_GEOLOCALIZACION";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona      =  " + Program.zonaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana   =  " + Program.manzanaV;   
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Lote      =  " + Program.loteV;   
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO  = '" + Program.edificioV + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO     = '" + Program.deptoV + "'";
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
                    else
                    {
                        txtLatitud.Text  = "";
                        txtLongitud.Text = "";
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al consultar coordenadas" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return 1;
                // Retornar false si ocurre un error
            }

            return 0;
        }

        private int ConsultaGeneralDos(int Y)
        {
            if (txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
            if (txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return 1; }
            if (txtMzna.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return 1; }
            if (txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return 1; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return 1; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return 1; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return 1; }
            if (txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return 1; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return 1; }

            Program.municipioV = Program.Vmunicipio;
            Program.zonaV = txtZona.Text.Trim();
            Program.manzanaV = txtMzna.Text.Trim();
            Program.loteV = txtLote.Text.Trim();
            Program.edificioV = txtEdificio.Text.Trim();
            Program.deptoV = txtDepto.Text.Trim();

            if (tipoDeMovimiento != 4)
            {
                if (txtSerie.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SERIE", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
                if (txtFolio.Text.Length < 2) { MessageBox.Show("NO SE TIENE EL FOLIO", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return 1; }
                Program.SerieC = txtSerie.Text.Trim();
                Program.FolioC = Convert.ToInt32(txtFolio.Text.Trim());
            }



            //------------------------------- consulta general para llenar los datos del predio y propiedades

            con.conectar_base_interno();
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT pr.TipoPredio, pr.Domicilio, pr.ZonaOrig, pr.CodCalle, c.NomCalle,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.NumExt, pr.EntCalle, pr.YCalle, pr.CodPost, co.Colonia, co.NomCol, re.RegProp,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.Ubicacion, f.DescFact, pr.SupTerrTot, pr.SupCons, pr.SupTerrCom, pr.SupConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       pr.Frente, pr.Fondo, pr.Desnivel, pr.AreaInscr, pr.cObsPred,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       p.NumIntP, p.PmnProp, p.DomFis, p.Uso, d.UsoEsp, d.Descrip, p.STerrProp,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       p.STerrCom, p.SConsProp, p.SConsCom, p.PtjeCondom, p.cObsProp";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM PROPIEDADES p, PREDIOS pr, CALLES c, COLONIAS co, REGIMEN re, FACTORES f, DESTINO d";

            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE p.Estado      =  " + Program.Vestado;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Municipio   =  " + Program.municipioV;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Zona        =  " + Program.zonaV;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Manzana     =  " + Program.manzanaV;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Lote        =  " + Program.loteV;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Edificio    = '" + Program.edificioV + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Depto       = '" + Program.deptoV + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Estado      = pr.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Municipio   = pr.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Zona        = pr.Zona";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Manzana     = pr.Manzana";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Lote        = pr.Lote";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado     = c.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio  = c.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.ZonaOrig   = c.ZonaOrig";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.CodCalle   = c.CodCalle";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Estado     = co.Estado";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Municipio  = co.Municipio";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Colonia    = co.Colonia";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.RegProp    = re.RegProp";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND pr.Ubicacion  = f.NumFactor";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND f.AnioVigMD   = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND f.TipoMerDem  = 6";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.Uso         = d.Uso";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND p.UsoEsp      = d.UsoEsp";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            if (!con.leer_interno.HasRows)
            {
                MessageBox.Show("NO SE ENCONTRO NINGUNA CLAVE CATASTRAL", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.cerrar_interno();
                inicio();
                return 2; // Retornar si no hay resultados
            }

            int tipoPredioV = 0;
            double edoPredioV = 0;
            string codCallesV = "";
            string zonaOrigenV = "";

            int regPropV = 0;
            int ubicacionV = 0;
            string usoSueloV = "";
            string destinoV = "";

            while (con.leer_interno.Read())
            {
                tipoPredioV = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                if (tipoPredioV == 0) { cboTipoPredio.SelectedIndex = 1; }
                if (tipoPredioV == 1) { cboTipoPredio.SelectedIndex = 0; }

                edoPredioV = Convert.ToInt32(con.leer_interno[31].ToString().Trim()) + Convert.ToInt32(con.leer_interno[32].ToString().Trim());

                if (edoPredioV > 0) { lblEdoPredio.Text = "1 CONSTRUIDO"; }
                if (edoPredioV <= 0) { lblEdoPredio.Text = "0 BALDIO"; }

                txtDomicilioPredio.Text = con.leer_interno[1].ToString().Trim();
                txtZonaOrigen.Text = con.leer_interno[2].ToString().Trim();
                txtCodigoCalle.Text = con.leer_interno[3].ToString().Trim();

                txtNoExterior.Text = con.leer_interno[5].ToString().Trim();
                txtEnCalle.Text = con.leer_interno[6].ToString().Trim();
                txtYcalle.Text = con.leer_interno[7].ToString().Trim();
                txtCodigoPostal.Text = con.leer_interno[8].ToString().Trim();
                txtColonia.Text = con.leer_interno[10].ToString().Trim();

                regPropV = Convert.ToInt32(con.leer_interno[11].ToString().Trim());
                cboRegimenPropiedad.SelectedIndex = regPropV;

                ubicacionV = Convert.ToInt32(con.leer_interno[12].ToString().Trim());
                cboUbicacion.SelectedIndex = ubicacionV;

                /* estos debemos de pasar a formato de 2 decimales  */
                /* --------------------------------------------------------------------------------------- */

                txtSupTerreno.Text = con.leer_interno[14].ToString().Trim();
                txtSupConstruccion.Text = con.leer_interno[15].ToString().Trim();
                txtSupTerrenoComun.Text = con.leer_interno[16].ToString().Trim();
                txtSupConstruccionComun.Text = con.leer_interno[17].ToString().Trim();
                txtFrente.Text = con.leer_interno[18].ToString().Trim();
                txtFondo.Text = con.leer_interno[19].ToString().Trim();
                txtDesnivel.Text = con.leer_interno[20].ToString().Trim();
                txtArea.Text = con.leer_interno[21].ToString().Trim();

                if (txtSupTerreno.Text.Trim() == "") { txtSupTerreno.Text = "0.00"; } else { txtSupTerreno.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupTerreno.Text)); }
                if (txtSupConstruccion.Text.Trim() == "") { txtSupConstruccion.Text = "0.00"; } else { txtSupConstruccion.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupConstruccion.Text)); }
                if (txtSupTerrenoComun.Text.Trim() == "") { txtSupTerrenoComun.Text = "0.00"; } else { txtSupTerrenoComun.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupTerrenoComun.Text)); }
                if (txtSupConstruccionComun.Text.Trim() == "") { txtSupConstruccionComun.Text = "0.00"; } else { txtSupConstruccionComun.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupConstruccionComun.Text)); }
                if (txtFrente.Text.Trim() == "") { txtFrente.Text = "0.00"; } else { txtFrente.Text = string.Format("{0:#,##0.00}", double.Parse(txtFrente.Text)); }
                if (txtFondo.Text.Trim() == "") { txtFondo.Text = "0.00"; } else { txtFondo.Text = string.Format("{0:#,##0.00}", double.Parse(txtFondo.Text)); }
                if (txtDesnivel.Text.Trim() == "") { txtDesnivel.Text = "0.00"; } else { txtDesnivel.Text = string.Format("{0:#,##0.00}", double.Parse(txtDesnivel.Text)); }
                if (txtArea.Text.Trim() == "") { txtArea.Text = "0.00"; } else { txtArea.Text = string.Format("{0:#,##0.00}", double.Parse(txtArea.Text)); }

                /* --------------------------------------------------------------------------------------- */

                txtObservaciones.Text = con.leer_interno[22].ToString().Trim();
                txtNoIntrior.Text = con.leer_interno[23].ToString().Trim();
                txtPropietario.Text = con.leer_interno[24].ToString().Trim();
                txtDomicilioPropietario.Text = con.leer_interno[25].ToString().Trim();
                txtDomicilioFiscal.Text = con.leer_interno[25].ToString().Trim();

                usoSueloV = con.leer_interno[26].ToString().Trim();

                for (int i = 0; i < cboUsoSuelo.Items.Count; i++)
                {
                    // Obtener el texto del ítem actual
                    string itemTexto = cboUsoSuelo.Items[i].ToString();

                    // Verificar si el primer carácter coincide con REGIMEN1
                    if (itemTexto.Length > 0 && itemTexto.Substring(0, 1) == usoSueloV)
                    {
                        // Seleccionar el ítem correspondiente
                        cboUsoSuelo.SelectedIndex = i;
                        break; // Salir del bucle una vez encontrado
                    }
                }

                destinoV = con.leer_interno[27].ToString().Trim();

                /* estos debemos de pasar a formato de 2 decimales  */
                /* --------------------------------------------------------------------------------------- */

                txtSupTerrenoPro.Text = con.leer_interno[29].ToString().Trim();
                txtSupTerrenoComunPro.Text = con.leer_interno[30].ToString().Trim();
                txtSupConstruccionPro.Text = con.leer_interno[31].ToString().Trim();
                txtSupConstruccionComunPro.Text = con.leer_interno[32].ToString().Trim();
                txtIndiviso.Text = con.leer_interno[33].ToString().Trim();

                if (txtSupTerrenoPro.Text.Trim() == "") { txtSupTerrenoPro.Text = "0.00"; } else { txtSupTerrenoPro.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupTerrenoPro.Text)); }
                if (txtSupTerrenoComunPro.Text.Trim() == "") { txtSupTerrenoComunPro.Text = "0.00"; } else { txtSupTerrenoComunPro.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupTerrenoComunPro.Text)); }
                if (txtSupConstruccionPro.Text.Trim() == "") { txtSupConstruccionPro.Text = "0.00"; } else { txtSupConstruccionPro.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupConstruccionPro.Text)); }
                if (txtSupConstruccionComunPro.Text.Trim() == "") { txtSupConstruccionComunPro.Text = "0.00"; } else { txtSupConstruccionComunPro.Text = string.Format("{0:#,##0.00}", double.Parse(txtSupConstruccionComunPro.Text)); }
                if (txtIndiviso.Text.Trim() == "") { txtIndiviso.Text = "0.00"; } else { txtIndiviso.Text = string.Format("{0:#,##0.00}", double.Parse(txtIndiviso.Text)); }

                /* --------------------------------------------------------------------------------------- */

                txtObservacionPro.Text = con.leer_interno[34].ToString().Trim();
            }

            con.cerrar_interno();

            int resultadoCboCalles = llenarCombosBaseDatos(0);
            if (resultadoCboCalles == 0) { return 3; }

            string codigoCalleV = txtCodigoCalle.Text.Trim();
            if (codigoCalleV.Length == 1) { codigoCalleV = codigoCalleV + " "; }

            for (int i = 0; i < cboCalle.Items.Count; i++)
            {
                // Obtener el texto del ítem actual
                string itemTexto = cboCalle.Items[i].ToString();

                // Verificar si los dos primeros carácter coincide con el codigo de calle
                if (itemTexto.Length > 0 && itemTexto.Substring(0, 2) == codigoCalleV)
                {
                    // Seleccionar el ítem correspondiente
                    cboCalle.SelectedIndex = i;
                    break; // Salir del bucle una vez encontrado
                }
            }

            for (int i = 0; i < cboDestino.Items.Count; i++)
            {
                // Obtener el texto del ítem actual
                string itemTexto = cboDestino.Items[i].ToString();

                // Verificar si los dos primeros carácter coincide con el codigo de calle
                if (itemTexto.Length > 0 && itemTexto.Substring(0, 2) == destinoV)
                {
                    // Seleccionar el ítem correspondiente
                    cboDestino.SelectedIndex = i;
                    break; // Salir del bucle una vez encontrado
                }
            }

            /********************************************************************************/
            /***** obtenemos los valores catastrales ****************************************/

            try
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("N19_CONSULTA_PREDIO", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ESTADO2", SqlDbType.Int, 2).Value = 15;
                cmd.Parameters.Add("@MUNICIPIO2", SqlDbType.Int, 3).Value = 41;
                cmd.Parameters.Add("@ZONA2", SqlDbType.Int, 2).Value = Convert.ToInt32(Program.zonaV);
                cmd.Parameters.Add("@MANZANA2", SqlDbType.Int, 3).Value = Convert.ToInt32(Program.manzanaV);
                cmd.Parameters.Add("@LOTE2", SqlDbType.Int, 2).Value = Convert.ToInt32(Program.loteV);
                cmd.Parameters.Add("@EDIFICIO2", SqlDbType.VarChar, 2).Value = Program.edificioV;
                cmd.Parameters.Add("@DEPTO2", SqlDbType.VarChar, 4).Value = Program.deptoV;

                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    txtValorTerrenoPropio.Text = (Convert.ToDouble(rdr["VALOR_TERRENO_P"].ToString().Trim())).ToString("N2");
                    txtValorTerrenoComun.Text = (Convert.ToDouble(rdr["valor_terreno_c"].ToString().Trim())).ToString("N2");
                    txtValorConstPropia.Text = (Convert.ToDouble(rdr["valor_construccion_p"].ToString().Trim())).ToString("N2");
                    txtValorConstComun.Text = (Convert.ToDouble(rdr["valor_construccion_c"].ToString().Trim())).ToString("N2");
                    txtValorCatastral.Text = (Convert.ToDouble(rdr["VALOR_CATASTRAL"].ToString().Trim())).ToString("N2");
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Retornar false si ocurre un error
            }

            /********************************************************************************/
            /***** obtenemos LATITUD Y LONGITUD      ****************************************/

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT TOP 1  LATITUD, LONGITUD";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_GEOLOCALIZACION";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona      =  " + Program.zonaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana   =  " + Program.manzanaV;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Lote      =  " + Program.loteV;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO  = '" + Program.edificioV + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO     = '" + Program.deptoV + "'";
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en seleccionar la geolocalizacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0; // Retornar false si ocurre un error
            }

            /********************************************************************************/
            /***** habilitamos los paneles de informacion   *********************************/

            pnlDatosPredio.Enabled = true;
            pnlDatosPropiedades.Enabled = true;
            btnCalculo.Enabled = true;
            btnAceptar.Enabled = true;
            btnMaps.Enabled = true;
            btnConstLote.Enabled = true;
            btnConstComun.Enabled = true;

            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            txtSerie.Enabled = false;
            txtFolio.Enabled = false;

            btnConsulta.Enabled = false;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;

            abilitarEtiquetas();

            return 4;
        }

        private void abilitarEtiquetas()
        {
            txtLatitud.Enabled = true;
            txtLongitud.Enabled = true;
            lblEdoPredio.Enabled = true;
            txtColonia.Enabled = true;
            txtSupConstruccion.Enabled = true;
            txtSupConstruccionComun.Enabled = true;
            txtPropietario.Enabled = true;
            txtSupConstruccionPro.Enabled = true;
            txtSupConstruccionComunPro.Enabled = true;
            txtValorTerrenoPropio.Enabled = true;
            txtValorTerrenoComun.Enabled = true;
            txtValorConstPropia.Enabled = true;
            txtValorConstComun.Enabled = true;
            txtValorCatastral.Enabled = true;
        }

        private void inabilitarClaveYfolio()
        {
            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            txtSerie.Enabled = false;
            txtFolio.Enabled = false;
        }

        private void GEOLOCALIZACION()
        {
            double latitud, longitud;
            String muniVar = Program.municipioT;
            String zonaVar = Program.zonaV;
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
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona      = "  + zonaVar;  //Se cocatena la zona que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana   = "  + mznaVar;  //Se cocatena la manzana que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Lote      = "  + loteVar;  //Se cocatena el lote que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO  = '" + edificioVar + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO     = '" + deptoVar    + "'";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error en seleccionar la geolocalizacion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }
        }

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void cmdAlDia_Click(object sender, EventArgs e)
        {
            limpiarTodoNoClaveYfolio();
            llenarCombosNormales();

            DateTime fechaActual = DateTime.Now;
              string fechaActualFormatoAño = fechaActual.ToString("yyyyMMdd");
              string fechaActualFormatoAñoUno = fechaActualFormatoAño + " 00:00:00";
              string fechaActualFormatoAñoDos = fechaActualFormatoAño + " 23:59:59";

            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT cnc.SERIE,    cnc.FOLIO_ORIGEN,";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ltrim(RIGHT('0' + convert(VARCHAR, cnc.MUNICIPIO), 3)) AS 'MUNICIPIO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ltrim(RIGHT('0' + convert(VARCHAR, cnc.ZONA),      2)) AS 'ZONA',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        CASE";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.MANZANA)) = 1 THEN '00' + ltrim(convert(VARCHAR, cnc.MANZANA))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.MANZANA)) = 2 THEN  '0' + ltrim(convert(VARCHAR, cnc.MANZANA))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             ELSE ltrim(convert(VARCHAR, cnc.MANZANA))";
            con.cadena_sql_interno = con.cadena_sql_interno + "        END AS 'MANZANA',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        ltrim(RIGHT('0' + convert(VARCHAR, cnc.LOTE),      2)) AS 'LOTE',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        ltrim(RIGHT('0' + convert(VARCHAR, cnc.EDIFICIO),  2)) AS 'EDIFICIO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        CASE";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.DEPTO)) = 1 THEN '000' + ltrim(convert(VARCHAR, cnc.DEPTO))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.DEPTO)) = 2 THEN '00'  + ltrim(convert(VARCHAR, cnc.DEPTO))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.DEPTO)) = 3 THEN '0'   + ltrim(convert(VARCHAR, cnc.DEPTO))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             ELSE ltrim(convert(VARCHAR, cnc.DEPTO))";
            con.cadena_sql_interno = con.cadena_sql_interno + "        END AS 'DEPTO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        REPLACE(CONVERT(VARCHAR(10), cdv.HORA_REV, 103), '.', '/') + ' ' +      RIGHT(CONVERT(VARCHAR(26), cdv.HORA_REV, 109), 13) AS 'FECHA',";

            con.cadena_sql_interno = con.cadena_sql_interno + "        ltrim(cnv.DESCRIPCION) AS 'TRAMITE',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        REPLACE(CONVERT(VARCHAR(10), cnc.FECHA, 103), '.', '/') + ' ' +    RIGHT(CONVERT(VARCHAR(26), cnc.HORA, 109), 13) AS 'FECHA_CREACION',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        ltrim(cnc.OBSERVACIONES) ";

            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025 cnc, CAT_NEW_VENTANILLA_2025 cnv, CAT_DONDE_VA_2025 cdv";

            if (tipoDeMovimiento == 1)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE cnc.UBICACION = 1";
            }   //Alta
            if (tipoDeMovimiento == 3)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE cnc.UBICACION = 2";
            }   //CAMBIOS

            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.FOLIO_ORIGEN = cnv.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.FOLIO_ORIGEN = cdv.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.CARTOGRAFIA  = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.VENTANILLA   = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.REVISO       = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.SISTEMAS     = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.ELIMINADO    = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.FECHA_REV   >=  '" + fechaActualFormatoAñoUno + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.FECHA_REV   <=  '" + fechaActualFormatoAñoDos + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY cdv.HORA_REV DESC";
            // todo bien
            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            if (da.Fill(LLENAR_GRID_1) == 0)     
            {
                con.cerrar_interno();
                MessageBox.Show("NO SE ENCONTRO DATOS DE LA BUSQUEDA", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inabilitarPrediosYpropiedades();
                limpiarTodo();

                abilitarSerieyFolioyClaveCatastro();

                btnConsulta.Enabled = true;   //btnConsultar
                btnBuscar.Enabled = false;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtZona.Focus();
                return;
            }
            else
            {
                DGVmovimiento.DataSource = LLENAR_GRID_1;
                con.cerrar_interno();
                DGVmovimiento.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                DGVmovimiento.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                DGVmovimiento.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(159, 24, 151);
                DGVmovimiento.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

                foreach (DataGridViewColumn columna in DGVmovimiento.Columns)
                {
                    columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                // Configuración de selección
                DGVmovimiento.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Deshabilitar edición
                DGVmovimiento.ReadOnly = true;
                // Estilos visuales
                DGVmovimiento.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
                DGVmovimiento.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

                // Configurar todas las columnas para que no se puedan redimensionar
                DGVmovimiento.AllowUserToResizeColumns = false;

                DGVmovimiento.Columns[0].Width = 50;                        // SERIE         
                DGVmovimiento.Columns[1].Width = 50;                        // FOLIO_ORIGEN
                DGVmovimiento.Columns[2].Width = 70;                        // MUNICIPIO
                DGVmovimiento.Columns[3].Width = 70;                        // ZONA
                DGVmovimiento.Columns[4].Width = 70;                        // MANZANA
                DGVmovimiento.Columns[5].Width = 70;                        // LOTE
                DGVmovimiento.Columns[6].Width = 70;                        // EDIFICIO
                DGVmovimiento.Columns[7].Width = 70;                        // DEPTO
                DGVmovimiento.Columns[8].Width = 140;                       // FECHA AUTORIZACION
                DGVmovimiento.Columns[9].Width = 100;                       // TRAMITE
                DGVmovimiento.Columns[10].Width = 140;                      // FECHA DE CREACION
                DGVmovimiento.Columns[11].Width = 415;                      // OBSERVACIONES CATASTRO

                DGVmovimiento.Columns[0].Name = "SERIE";                    // SERIE   
                DGVmovimiento.Columns[1].Name = "FOLIO";                    // FOLIO_ORIGEN 
                DGVmovimiento.Columns[2].Name = "MUNICIPIO";                // MUNICIPIO 
                DGVmovimiento.Columns[3].Name = "ZONA";                     // ZONA 
                DGVmovimiento.Columns[4].Name = "MANZANA";                  // MANZANA 
                DGVmovimiento.Columns[5].Name = "LOTE";                     // LOTE 
                DGVmovimiento.Columns[6].Name = "EDIFICIO";                 // EDIFICIO 
                DGVmovimiento.Columns[7].Name = "DEPTO";                    // DEPTO 
                DGVmovimiento.Columns[8].Name = "FECHA_AUTO";               // FECHA AUTORIZACION 
                DGVmovimiento.Columns[9].Name = "TRAMITE";                  // TRAMITE 
                DGVmovimiento.Columns[10].Name = "FECHA_CREA";              // FECHA DE CREACION 
                DGVmovimiento.Columns[11].Name = "OBSERVA";                 // OBSERVACION DE CATASTRO 

                DGVmovimiento.Columns[0].HeaderText = "SERIE";              // SERIE   
                DGVmovimiento.Columns[1].HeaderText = "FOLIO";              // FOLIO_ORIGEN 
                DGVmovimiento.Columns[2].HeaderText = "MUNICIPIO";          // MUNICIPIO 
                DGVmovimiento.Columns[3].HeaderText = "ZONA";               // ZONA 
                DGVmovimiento.Columns[4].HeaderText = "MANZANA";            // MANZANA 
                DGVmovimiento.Columns[5].HeaderText = "LOTE";               // LOTE 
                DGVmovimiento.Columns[6].HeaderText = "EDIFICIO";           // EDIFICIO 
                DGVmovimiento.Columns[7].HeaderText = "DEPTO";              // DEPTO 
                DGVmovimiento.Columns[8].HeaderText = "FECHA AUTORIZA";     // FECHA AUTORIZA 
                DGVmovimiento.Columns[9].HeaderText = "TRAMITE";            // TRAMITE 
                DGVmovimiento.Columns[10].HeaderText = "FECHA CREACION";     // FECHA CREACION 
                DGVmovimiento.Columns[11].HeaderText = "OBSERVACION CATASTRO";     // FECHA CREACION 

                DGVmovimiento.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                DGVmovimiento.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                int CONTEO;
                CONTEO = DGVmovimiento.Rows.Count - 1;
                //lblNumRegistro.Text = CONTEO.ToString();
                DGVmovimiento.Enabled = true; // Habilitar la grilla de resultados
                con.cerrar_interno();
            }

            inabilitarPrediosYpropiedades();
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;

            cmdAlDia.Enabled = true;
            cmdDiasAnteriores.Enabled = true;
            cmdRefresh.Enabled = true;
        }

        private void cmdDiasAnteriores_Click(object sender, EventArgs e)
        {
            limpiarTodoNoClaveYfolio();
            llenarCombosNormales();

            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT cnc.SERIE,    cnc.FOLIO_ORIGEN,";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ltrim(RIGHT('0' + convert(VARCHAR, cnc.MUNICIPIO), 3)) AS 'MUNICIPIO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ltrim(RIGHT('0' + convert(VARCHAR, cnc.ZONA),      2)) AS 'ZONA',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        CASE";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.MANZANA)) = 1 THEN '00' + ltrim(convert(VARCHAR, cnc.MANZANA))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.MANZANA)) = 2 THEN  '0' + ltrim(convert(VARCHAR, cnc.MANZANA))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             ELSE ltrim(convert(VARCHAR, cnc.MANZANA))";
            con.cadena_sql_interno = con.cadena_sql_interno + "        END AS 'MANZANA',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        ltrim(RIGHT('0' + convert(VARCHAR, cnc.LOTE),      2)) AS 'LOTE',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        ltrim(RIGHT('0' + convert(VARCHAR, cnc.EDIFICIO),  2)) AS 'EDIFICIO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        CASE";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.DEPTO)) = 1 THEN '000' + ltrim(convert(VARCHAR, cnc.DEPTO))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.DEPTO)) = 2 THEN '00'  + ltrim(convert(VARCHAR, cnc.DEPTO))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHEN LEN(convert(VARCHAR, cnc.DEPTO)) = 3 THEN '0'   + ltrim(convert(VARCHAR, cnc.DEPTO))";
            con.cadena_sql_interno = con.cadena_sql_interno + "             ELSE ltrim(convert(VARCHAR, cnc.DEPTO))";
            con.cadena_sql_interno = con.cadena_sql_interno + "        END AS 'DEPTO',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        REPLACE(CONVERT(VARCHAR(10), cdv.HORA_REV, 103), '.', '/') + ' ' +      RIGHT(CONVERT(VARCHAR(26), cdv.HORA_REV, 109), 13) AS 'FECHA',";

            con.cadena_sql_interno = con.cadena_sql_interno + "        ltrim(cnv.DESCRIPCION) AS 'TRAMITE',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        REPLACE(CONVERT(VARCHAR(10), cnc.FECHA, 103), '.', '/') + ' ' +    RIGHT(CONVERT(VARCHAR(26), cnc.HORA, 109), 13) AS 'FECHA_CREACION',";
            con.cadena_sql_interno = con.cadena_sql_interno + "        ltrim(cnc.OBSERVACIONES) ";

            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025 cnc, CAT_NEW_VENTANILLA_2025 cnv, CAT_DONDE_VA_2025 cdv";

            if (tipoDeMovimiento == 1)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE cnc.UBICACION = 1";
            }   //Alta
            if (tipoDeMovimiento == 3)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE cnc.UBICACION = 2";
            }   //CAMBIOS

            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.FOLIO_ORIGEN = cnv.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cnc.FOLIO_ORIGEN = cdv.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.CARTOGRAFIA  = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.VENTANILLA   = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.REVISO       = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.SISTEMAS     = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND cdv.ELIMINADO    = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY cdv.HORA_REV DESC";

            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO
            {
                con.cerrar_interno();   
                MessageBox.Show("NO SE ENCONTRO DATOS DE LA BUSQUEDA", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                inabilitarPrediosYpropiedades();
                limpiarTodo();
                abilitarSerieyFolioyClaveCatastro();

                btnConsulta.Enabled = true;   //btnConsultar
                btnBuscar.Enabled = false;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtZona.Focus();
                return;
            }
            else
            {
                DGVmovimiento.DataSource = LLENAR_GRID_1;
                con.cerrar_interno();
                DGVmovimiento.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                DGVmovimiento.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                DGVmovimiento.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(159, 24, 151);
                DGVmovimiento.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;

                foreach (DataGridViewColumn columna in DGVmovimiento.Columns)
                {
                    columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                // Configuración de selección
                DGVmovimiento.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Deshabilitar edición
                DGVmovimiento.ReadOnly = true;
                // Estilos visuales
                DGVmovimiento.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Yellow;
                DGVmovimiento.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;

                // Configurar todas las columnas para que no se puedan redimensionar
                DGVmovimiento.AllowUserToResizeColumns = false;

                DGVmovimiento.Columns[0].Width = 50;                        // SERIE         
                DGVmovimiento.Columns[1].Width = 50;                        // FOLIO_ORIGEN
                DGVmovimiento.Columns[2].Width = 70;                        // MUNICIPIO
                DGVmovimiento.Columns[3].Width = 70;                        // ZONA
                DGVmovimiento.Columns[4].Width = 70;                        // MANZANA
                DGVmovimiento.Columns[5].Width = 70;                        // LOTE
                DGVmovimiento.Columns[6].Width = 70;                        // EDIFICIO
                DGVmovimiento.Columns[7].Width = 70;                        // DEPTO
                DGVmovimiento.Columns[8].Width = 140;                       // FECHA AUTORIZACION
                DGVmovimiento.Columns[9].Width = 100;                       // TRAMITE
                DGVmovimiento.Columns[10].Width = 140;                      // FECHA DE CREACION
                DGVmovimiento.Columns[11].Width = 415;                      // OBSERVACIONES CATASTRO

                DGVmovimiento.Columns[0].Name = "SERIE";                    // SERIE   
                DGVmovimiento.Columns[1].Name = "FOLIO";                    // FOLIO_ORIGEN 
                DGVmovimiento.Columns[2].Name = "MUNICIPIO";                // MUNICIPIO 
                DGVmovimiento.Columns[3].Name = "ZONA";                     // ZONA 
                DGVmovimiento.Columns[4].Name = "MANZANA";                  // MANZANA 
                DGVmovimiento.Columns[5].Name = "LOTE";                     // LOTE 
                DGVmovimiento.Columns[6].Name = "EDIFICIO";                 // EDIFICIO 
                DGVmovimiento.Columns[7].Name = "DEPTO";                    // DEPTO 
                DGVmovimiento.Columns[8].Name = "FECHA_AUTO";               // FECHA AUTORIZACION 
                DGVmovimiento.Columns[9].Name = "TRAMITE";                  // TRAMITE 
                DGVmovimiento.Columns[10].Name = "FECHA_CREA";              // FECHA DE CREACION 
                DGVmovimiento.Columns[11].Name = "OBSERVA";                 // OBSERVACION DE CATASTRO 

                DGVmovimiento.Columns[0].HeaderText = "SERIE";              // SERIE   
                DGVmovimiento.Columns[1].HeaderText = "FOLIO";              // FOLIO_ORIGEN 
                DGVmovimiento.Columns[2].HeaderText = "MUNICIPIO";          // MUNICIPIO 
                DGVmovimiento.Columns[3].HeaderText = "ZONA";               // ZONA 
                DGVmovimiento.Columns[4].HeaderText = "MANZANA";            // MANZANA 
                DGVmovimiento.Columns[5].HeaderText = "LOTE";               // LOTE 
                DGVmovimiento.Columns[6].HeaderText = "EDIFICIO";           // EDIFICIO 
                DGVmovimiento.Columns[7].HeaderText = "DEPTO";              // DEPTO 
                DGVmovimiento.Columns[8].HeaderText = "FECHA AUTORIZA";     // FECHA AUTORIZA 
                DGVmovimiento.Columns[9].HeaderText = "TRAMITE";            // TRAMITE 
                DGVmovimiento.Columns[10].HeaderText = "FECHA CREACION";     // FECHA CREACION 
                DGVmovimiento.Columns[11].HeaderText = "OBSERVACION CATASTRO";     // FECHA CREACION 

                DGVmovimiento.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft ;

                DGVmovimiento.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGVmovimiento.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                int CONTEO;
                CONTEO = DGVmovimiento.Rows.Count - 1;
                //lblNumRegistro.Text = CONTEO.ToString();
                DGVmovimiento.Enabled = true; // Habilitar la grilla de resultados
                con.cerrar_interno();
            }

            inabilitarPrediosYpropiedades();
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;

            cmdAlDia.Enabled = true;
            cmdDiasAnteriores.Enabled = true;
            cmdRefresh.Enabled = true;

        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            tipoDeMovimiento = 3;
            panelMuestra();
            invisibleSerieFolioVerdadero();
            abilitarSerieYfolio();
            generales();

            pnlOculta.Visible = false;

            btnConsulta.Enabled = true;   
            btnBuscar.Enabled = false;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;
            btnMinimizar.Enabled = true;


            txtSerie.Text = Program.SerieC.Trim();
            txtSerie.Enabled = true;
            txtFolio.Enabled = true;
            txtZona.Focus();
            return;

            //limpiarTodoNoClaveYfolio();
            //llenarCombosNormales();
            //txtZona.Focus();
            
        }
        
        private void txtFolio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir letras (mayúsculas y minúsculas) y teclas de control
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void DGVmovimiento_DoubleClick(object sender, EventArgs e)
        {
            //--------------------------------------------------------------------------------------------------------------------------------//
            //-------------------------  PROCEDEMOS A OBTENER EL FOLIO,  ---------------------------------------------------------------------//
            //--------------------------------------------------------------------------------------------------------------------------------//

            string serieVal = ""; string edifVar = ""; string deptoVar = "";
            string folioVar = ""; string munVar = ""; string zonVar = ""; string manVar = ""; string lotVar = "";

            int numRound = DGVmovimiento.CurrentRow.Index;
            if (numRound > -1)
            {
                serieVal = DGVmovimiento.CurrentRow.Cells[0].Value.ToString().Trim();
                if (serieVal == "") { return; }
                    txtSerie.Text = serieVal;
                      txtMun.Text = "041";
                    txtFolio.Text = DGVmovimiento.CurrentRow.Cells[1].Value.ToString().Trim();
                     txtZona.Text = DGVmovimiento.CurrentRow.Cells[3].Value.ToString().Trim();
                     txtMzna.Text = DGVmovimiento.CurrentRow.Cells[4].Value.ToString().Trim();
                     txtLote.Text = DGVmovimiento.CurrentRow.Cells[5].Value.ToString().Trim();
                 txtEdificio.Text = DGVmovimiento.CurrentRow.Cells[6].Value.ToString().Trim();
                    txtDepto.Text = DGVmovimiento.CurrentRow.Cells[7].Value.ToString().Trim();

                consultaGeneral();

                //consulta_inmueble_datos(folioBuscar);
                //txtInmueble.Text = Convert.ToString(folioBuscar);
            }
        }

        private void btnConstLote_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnConstLote, "CONTRUCCION PRIVADA");
        }

        private void btnConstComun_Click(object sender, EventArgs e)
        {
            Program.tipoDeMovimientoProgram = tipoDeMovimiento;  //1 ALTA, 2 BAJA, 3 CAMBIO
            double constP = 0;

            if (txtSupConstruccionPro.Text.Trim() == "") { txtSupConstruccionPro.Text = "0"; }
            if (txtSupConstruccionComunPro.Text.Trim() == "") { txtSupConstruccionComunPro.Text = "0"; }

            constP = Convert.ToDouble(txtSupConstruccionPro.Text.Trim());
            if (constP > 0)
            {
                MessageBox.Show("NO SE PUEDE INGRESAR CONSTRUCCION COMUN. PORQUE SE TIENE CONSTRUCCION PRIVADA", "ERROR", MessageBoxButtons.OK);
                return;
            }

            if (txtSupConstruccionPro.Text.Trim() == "0")
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

        private void btnConstLote_Click(object sender, EventArgs e)
        {
            Program.tipoDeMovimientoProgram = tipoDeMovimiento;  //1 ALTA, 2 BAJA, 3 CAMBIO, 4 CONSULTA, 5 CAMBIOS GENERALES

            double constC = 0;
            if (txtSupConstruccionPro.Text.Trim() == "") { txtSupConstruccionPro.Text = "0"; }
            if (txtSupConstruccionComunPro.Text.Trim() == "") { txtSupConstruccionComunPro.Text = "0"; }
            if (txtSupConstruccionComunPro.Text.Trim() != "0")
            {
                constC = Convert.ToDouble(txtSupConstruccionComunPro.Text.Trim());
                if (constC > 0)
                {
                    MessageBox.Show("NO SE PUEDE INGRESAR CONSTRUCCION PROPIA. PORQUE SE TIENE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK);
                    return;
                }
            }

            if (txtSupConstruccionComunPro.Text.Trim() == "") { txtSupConstruccionComunPro.Text = "0"; }
            if (Convert.ToDecimal(txtSupConstruccionComunPro.Text.Trim()) <= 0)
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
                txtSupConstruccionPro.Text = Program.constuccion.ToString();
                frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;

                //frmCatastro01UbicacionAlta.ActiveForm.Opacity = 0.50;
                //frmCatastro02UnidadesConstruccion fs = new frmCatastro02UnidadesConstruccion();
                //fs.ShowDialog();
                ////txtSupCont.Text = Program.construccion();
                ////fs.Show();
                //frmCatastro01UbicacionAlta.ActiveForm.Opacity = 1.0;
            }
        }

        private void btnConstComun_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnConstComun, "CONTRUCCION COMUN");
        }

        private void refreshh()
        {
            if (mtcInformacion.SelectedIndex == 0) // inicio
            {
                cajasBlanco();
                pnlOculta.Visible = true;
                pnlOculta.Size = new Size(1339, 531);
                pnlOculta.Left = 12;                    // Distancia desde el borde izquierdo
                pnlOculta.Top = 153;                    // Distancia desde el borde superior

                tipoDeMovimiento = 0;
                invisibleSerieFolioVerdadero();
                abilitarSerieYfolio();
                btnBuscar.Enabled = false;

                inicio();
            }// inicio

            if (mtcInformacion.SelectedIndex == 1) // altas
            {
                cajasBlanco();

                invisibleSerieFolioVerdadero();
                abilitarSerieYfolio();
                generales();
                panelMuestra();

                btnConsulta.Enabled = true;
                btnBuscar.Enabled = false;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtSerie.Text = Program.SerieC.Trim();
                txtSerie.Enabled = true;
                txtFolio.Enabled = true;
                txtZona.Focus();

            }// Altas

            if (mtcInformacion.SelectedIndex == 2) // bajas
            {
                cajasBlanco();

                panelOculta();
                invisibleSerieFoliofalse();
                inabilitarSerieYfolio();
                generales();

                txtPropietario.Enabled = false;
                btnConsulta.Enabled = true;
                btnBuscar.Enabled = true;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtZona.Focus();

            }// Bajas

            if (mtcInformacion.SelectedIndex == 3) // cambios
            {
                cajasBlanco();
                invisibleSerieFolioVerdadero();
                abilitarSerieYfolio();
                generales();
                panelMuestra();

                btnConsulta.Enabled = true;
                btnBuscar.Enabled = false;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtPropietario.Enabled = false;
                txtSerie.Text = Program.SerieC.Trim();
                txtSerie.Enabled = true;
                txtFolio.Enabled = true;
                txtZona.Focus();

            }// Cambios

            if (mtcInformacion.SelectedIndex == 4)   // consultas generales
            {
                cajasBlanco();
                panelOculta();
                invisibleSerieFoliofalse();
                inabilitarSerieYfolio();
                generales();

                txtPropietario.Enabled = false;
                btnConsulta.Enabled = true;
                btnBuscar.Enabled = true;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtZona.Focus();

            }// Consultas generales

            if (mtcInformacion.SelectedIndex == 5)   // Cambios generales
            {
                cajasBlanco();
                panelOculta();
                invisibleSerieFoliofalse();
                inabilitarSerieYfolio();
                generales();

                txtPropietario.Enabled = false;
                btnConsulta.Enabled = true;
                btnBuscar.Enabled = true;
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = true;
                btnMinimizar.Enabled = true;

                txtZona.Focus();
            }// Cambios Generales
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshh();
        }

        private int scriptAltass(int y)
        {
            //OBTENEMOS LA FECHA Y HORA ACTUAL DEL SISTEMA

            string fechaSistemas = "";
            DateTime fechaActual = DateTime.Now;
            string fechaSistema = fechaActual.ToString("yyyyMMdd");
            string horaSistema = fechaActual.ToString("HH:mm:ss");

            fechaSistemas = fechaSistema + " " + horaSistema;

                if (cboTipoPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL TIPO DE PREDIO", "ERROR", MessageBoxButtons.OK); cboTipoPredio.Focus(); return 0; }
                if (lblEdoPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL ESTADO DEL PREDIO", "ERROR", MessageBoxButtons.OK); lblEdoPredio.Focus(); return 0; }
                if (txtDomicilioPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO DEL PREDIO", "ERROR", MessageBoxButtons.OK); txtDomicilioPredio.Focus(); return 0; }
                if (txtZonaOrigen.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA DE ORIGEN", "ERROR", MessageBoxButtons.OK); txtZonaOrigen.Focus(); return 0; }
                if (txtCodigoCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL CODIGO DE CALLE", "ERROR", MessageBoxButtons.OK); txtCodigoCalle.Focus(); return 0; }
                if (cboCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA CALLE", "ERROR", MessageBoxButtons.OK); cboCalle.Focus(); return 0; }
                if (txtNoExterior.Text.Trim() == "") { MessageBox.Show("NO SE TIENE NUMERO EXTERIOR", "ERROR", MessageBoxButtons.OK); txtNoExterior.Focus(); return 0; }
                if (txtEnCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE ENTRE CALLE", "ERROR", MessageBoxButtons.OK); txtEnCalle.Focus(); return 0; }
                if (txtYcalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE ENTRE CALLE", "ERROR", MessageBoxButtons.OK); txtYcalle.Focus(); return 0; }
                if (txtCodigoPostal.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL CODIGO POSTAL", "ERROR", MessageBoxButtons.OK); txtCodigoPostal.Focus(); return 0; }
                if (txtColonia.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA COLONIA", "ERROR", MessageBoxButtons.OK); txtColonia.Focus(); return 0; }
                if (cboRegimenPropiedad.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL REGIMEN DE PROPIEDAD", "ERROR", MessageBoxButtons.OK); cboRegimenPropiedad.Focus(); return 0; }
                if (cboUbicacion.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA UBICACION", "ERROR", MessageBoxButtons.OK); cboUbicacion.Focus(); return 0; }
                if (txtSupTerreno.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE TERRENO", "ERROR", MessageBoxButtons.OK); txtSupTerreno.Focus(); return 0; }
                if (txtSupConstruccion.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtSupConstruccion.Focus(); return 0; }
                if (txtSupTerrenoComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtSupTerrenoComun.Focus(); return 0; }
                if (txtSupConstruccionComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtSupConstruccionComun.Focus(); return 0; }
                if (txtFrente.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FRENTE", "ERROR", MessageBoxButtons.OK); txtFrente.Focus(); return 0; }
                if (txtFondo.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FONDO", "ERROR", MessageBoxButtons.OK); txtFondo.Focus(); return 0; }
                if (txtDesnivel.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DESNIVEL", "ERROR", MessageBoxButtons.OK); txtDesnivel.Focus(); return 0; }
                if (txtArea.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA AREA", "ERROR", MessageBoxButtons.OK); txtArea.Focus(); return 0; }

                if (txtNoIntrior.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL NUMERO INTERIOR", "ERROR", MessageBoxButtons.OK); txtNoIntrior.Focus(); return 0; }
                if (txtPropietario.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL PROPIETARIO", "ERROR", MessageBoxButtons.OK); txtPropietario.Focus(); return 0; }
                if (txtDomicilioPropietario.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO DEL PROPIETARIO", "ERROR", MessageBoxButtons.OK); txtDomicilioPropietario.Focus(); return 0; }
                if (txtDomicilioFiscal.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO FISCAL", "ERROR", MessageBoxButtons.OK); txtDomicilioFiscal.Focus(); return 0; }
                if (cboUsoSuelo.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL SUELO", "ERROR", MessageBoxButtons.OK); cboUsoSuelo.Focus(); return 0; }
                if (cboDestino.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DESTINO", "ERROR", MessageBoxButtons.OK); cboDestino.Focus(); return 0; }
                if (txtSupTerrenoPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. TERRENO PROPIA", "ERROR", MessageBoxButtons.OK); txtSupTerrenoPro.Focus(); return 0; }
                if (txtSupTerrenoComunPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtSupTerrenoComunPro.Focus(); return 0; }
                if (txtSupConstruccionPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. CONSTRUCCION PROPIA", "ERROR", MessageBoxButtons.OK); txtSupConstruccionPro.Focus(); return 0; }
                if (txtSupConstruccionComunPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtSupConstruccionComunPro.Focus(); return 0; }
                if (txtIndiviso.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL INDIVISO", "ERROR", MessageBoxButtons.OK); txtIndiviso.Focus(); return 0; }
                if (txtValorTerrenoPropio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE TERRENO PROPIO", "ERROR", MessageBoxButtons.OK); txtValorTerrenoPropio.Focus(); return 0; }
                if (txtValorTerrenoComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtValorTerrenoComun.Focus(); return 0; }
                if (txtValorConstPropia.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE CONSTRUCCION PROPIA", "ERROR", MessageBoxButtons.OK); txtValorConstPropia.Focus(); return 0; }
                if (txtValorConstComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtValorConstComun.Focus(); return 0; }
                if (txtValorCatastral.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR CATASTRAL", "ERROR", MessageBoxButtons.OK); txtValorCatastral.Focus(); return 0; }
                if (txtObservacionPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA OBSERVACION DE LA ALTA", "ERROR", MessageBoxButtons.OK); txtObservacionPro.Focus(); return 0; }

            int propiedadSioNo = 0;
            int prediosSioNo = 0;
            int tempPrediosSioNo = 0;

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////  consulta para ver si la clave catastral se encuentra en la tabla de PROPIEDADES  /////////////////////////////////// 
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            propiedadSioNo = 0;
            prediosSioNo = 0;
            tempPrediosSioNo = 0;

                try
                {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT Estado";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE Estado = 15 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Municipio = " + txtMun.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Zona      = " + txtZona.Text.Trim();   ;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana   = " + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Lote      = " + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Edificio  = " + "'" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Depto     = " + "'" + txtDepto.Text.Trim()    + "')";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 0";
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
                        if (con.leer_interno[0].ToString().Trim() == "") { propiedadSioNo = 0; }
                        else { propiedadSioNo = Convert.ToInt32(con.leer_interno[0].ToString().Trim()); }
                    }
                    con.cerrar_interno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0; // Retornar false si ocurre un error
                }                           // SI EXIST PREDIOS

                if (propiedadSioNo == 0) { MessageBox.Show("CLAVE CATASTRAL, EXISTENTE EN LA TABLA DE PROPIEDADES, FAVOR DE VERIFICAR", "ERROR", MessageBoxButtons.OK); return 0; }

                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT Estado";
                    con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE Estado = 15 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND Municipio = " + txtMun.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND Zona      = " + txtZona.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana   = " + txtMzna.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND Lote      = " + txtLote.Text.Trim() + ")";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 0";
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
                        if (con.leer_interno[0].ToString().Trim() == "") { prediosSioNo = 0; }
                        else { prediosSioNo = Convert.ToInt32(con.leer_interno[0].ToString().Trim()); }
                    }
                    con.cerrar_interno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0; // Retornar false si ocurre un error
                }                           // SI EXIST PROPIEDADES

                if (prediosSioNo == 0) { MessageBox.Show("CLAVE CATASTRAL, EXISTENTE EN LA TABLA DE PREDIOS, FAVOR DE VERIFICAR", "ERROR", MessageBoxButtons.OK); return 0; }

                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT Estado";
                    con.cadena_sql_interno = con.cadena_sql_interno + "              FROM TEM_PREDIOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE    Estado = 15 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND Municipio =  " + txtMun.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND      Zona =  " + txtZona.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND   Manzana =  " + txtMzna.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND      Lote =  " + txtLote.Text.Trim() + ")";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 0";               //EXISTE
                    con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";               //NO EXISTE
                    con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        if (con.leer_interno[0].ToString().Trim() == "") { tempPrediosSioNo = 0; }
                        else { tempPrediosSioNo = Convert.ToInt32(con.leer_interno[0].ToString().Trim()); }
                    }
                    con.cerrar_interno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;                                     // Retornar false si ocurre un error
                }                           // SI EXISTE EN TEMP PREDIOS

                if (tempPrediosSioNo == 0)
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " DELETE FROM TEM_PROPIEDADES";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE    Estado = 15";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND Municipio = " + txtMun.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND      Zona = " + txtZona.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND   Manzana = " + txtMzna.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND      Lote = " + txtLote.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND  Edificio = '" + txtEdificio.Text.Trim() + "'";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND     Depto = '" + txtDepto.Text.Trim() + "'";

                    con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                    con.cadena_sql_interno = con.cadena_sql_interno + " DELETE FROM TEM_PREDIOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE    Estado = 15";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND Municipio = " + txtMun.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND      Zona = " + txtZona.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND   Manzana = " + txtMzna.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND      Lote = " + txtLote.Text.Trim();

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    con.cerrar_interno();
                }    // BORRAMOS LOS REGISTROS DE TEMPS PREDIOS Y PROPIEDADES

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////  OBTENEMOS EL CODIGO DE LA COLONIA                                                                  /////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            int codigoColoniaInsert = 0;

            con.conectar_base_interno();
            con.cadena_sql_interno = "";

            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT Colonia";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM MANZANAS";
            con.cadena_sql_interno = con.cadena_sql_interno + "  Where Zona     = " + txtZona.Text.Trim() ;
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana  = " + txtMzna.Text.Trim() ;

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                if (con.leer_interno[0].ToString().Trim() == "") { codigoColoniaInsert = 0; }
                else { codigoColoniaInsert = Convert.ToInt32(con.leer_interno[0].ToString().Trim()); }
            }

            con.cerrar_interno();

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////  INSERTAMOS EN TEM_PREDIOS Y TEM_PROPIEDADES                                                        /////////////////
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  INSERT INTO TEM_PREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             (";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Estado,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Municipio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Zona,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Manzana,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Lote,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             TipoPredio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             RegProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Domicilio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ZonaOrig,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             CodCalle,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NumExt,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Colonia,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             CodPost,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             EntCalle,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             YCalle,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SupTerrTot,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SupTerrCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SupCons,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SupConsCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Frente,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Fondo,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Desnivel,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             AreaInscr,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Ubicacion,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFrente,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFFondo,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFIrreg,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFArea,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFTopogr,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFUbic,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ValTerr,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ValCons,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             FCaptura,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Aclaracion,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             cEdoPred,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             cObsPred,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Baja";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";
                con.cadena_sql_interno = con.cadena_sql_interno + "         Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "             (";
                con.cadena_sql_interno = con.cadena_sql_interno + "             15";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtMun.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + cboTipoPredio.Text.Trim().Substring(0,1) + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + cboRegimenPropiedad.Text.Trim().Substring(0,1) + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtDomicilioPredio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtZonaOrigen.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtCodigoCalle.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtNoExterior.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + codigoColoniaInsert;

                if (txtCodigoPostal.Text.Trim() == "") { txtCodigoPostal.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtCodigoPostal.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtEnCalle.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtYcalle.Text.Trim() + "'";

                if (txtSupTerreno.Text.Trim() == "") { txtSupTerreno.Text = "0"; }
                if (txtSupTerrenoComun.Text.Trim() == "") { txtSupTerrenoComun.Text = "0"; }
                if (txtSupConstruccion.Text.Trim() == "") { txtSupConstruccion.Text = "0"; }
                if (txtSupConstruccionComun.Text.Trim() == "") { txtSupConstruccionComun.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupTerreno.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupTerrenoComun.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupConstruccion.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupConstruccionComun.Text.Trim());

                if (txtFrente.Text.Trim() == "") { txtFrente.Text = "0"; }
                if (txtFondo.Text.Trim() == "") { txtFondo.Text = "0"; }
                if (txtDesnivel.Text.Trim() == "") { txtDesnivel.Text = "0"; }
                if (txtArea.Text.Trim() == "") { txtArea.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtFrente.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtFondo.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtDesnivel.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtArea.Text.Trim());

                if (cboUbicacion.Text.Trim().Substring(0, 1) == "") { con.cadena_sql_interno = con.cadena_sql_interno + "         ,1"; }
                else { con.cadena_sql_interno = con.cadena_sql_interno + "         ," + cboUbicacion.Text.Trim().Substring(0, 1); }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facFrente;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facFondo;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facIrreg;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facArea;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facTopo;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facPosicion;

                if (txtValorTerrenoPropio.Text.Trim() == "") { txtValorTerrenoPropio.Text = "0"; }
                if (txtValorConstPropia.Text.Trim() == "") { txtValorConstPropia.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorTerrenoPropio.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorConstPropia.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + fechaSistemas + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + "0" + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + "1" + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtObservacionPro.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + "0" + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";

                con.cadena_sql_interno = con.cadena_sql_interno + "              ";
                con.cadena_sql_interno = con.cadena_sql_interno + "       SET NOCOUNT ON";
                con.cadena_sql_interno = con.cadena_sql_interno + "              ";

                con.cadena_sql_interno = con.cadena_sql_interno + "  INSERT INTO TEM_HPREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "             (";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Estado,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Municipio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Zona,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Manzana,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Lote,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             TipoPredio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             RegProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Domicilio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ZonaOrig,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             CodCalle,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NumExt,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Colonia,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             CodPost,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             EntCalle,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             YCalle,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SupTerrTot,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SupTerrCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SupCons,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SupConsCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Frente,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Fondo,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Desnivel,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             AreaInscr,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Ubicacion,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFrente,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFFondo,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFIrreg,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFArea,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFTopogr,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NFUbic,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ValTerr,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ValCons,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             FCaptura,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Aclaracion,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             cEdoPred,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             cObsPred,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Baja,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UsrMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             FecMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             HoraMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             OperaMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             EdoOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             MpioOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ZonaOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             MznaOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             LoteOD";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";
                con.cadena_sql_interno = con.cadena_sql_interno + "         Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "             (";
                con.cadena_sql_interno = con.cadena_sql_interno + "             15";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtMun.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + cboTipoPredio.Text.Trim().Substring(0, 1) + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + cboRegimenPropiedad.Text.Trim().Substring(0, 1);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtDomicilioPredio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtZonaOrigen.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtCodigoCalle.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtNoExterior.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + codigoColoniaInsert;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtCodigoPostal.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtEnCalle.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtYcalle.Text.Trim() + "'";

                if (txtSupTerreno.Text.Trim() == "") { txtSupTerreno.Text = "0"; }
                if (txtSupTerrenoComun.Text.Trim() == "") { txtSupTerrenoComun.Text = "0"; }
                if (txtSupConstruccion.Text.Trim() == "") { txtSupConstruccion.Text = "0"; }
                if (txtSupConstruccionComun.Text.Trim() == "") { txtSupConstruccionComun.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupTerreno.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupTerrenoComun.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupConstruccion.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupConstruccionComun.Text.Trim());

                if (txtFrente.Text.Trim() == "") { txtFrente.Text = "0"; }
                if (txtFondo.Text.Trim() == "") { txtFondo.Text = "0"; }
                if (txtDesnivel.Text.Trim() == "") { txtDesnivel.Text = "0"; }
                if (txtArea.Text.Trim() == "") { txtArea.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtFrente.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtFondo.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtDesnivel.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtArea.Text.Trim());

                if (cboUbicacion.Text.Trim().Substring(0, 1) == "") { con.cadena_sql_interno = con.cadena_sql_interno + "         ,1"; }
                else { con.cadena_sql_interno = con.cadena_sql_interno + "         ," + cboUbicacion.Text.Trim().Substring(0, 1); }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facFrente;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facFondo;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facIrreg;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facArea;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facTopo;
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + facPosicion;

                if (txtValorTerrenoPropio.Text.Trim() == "") { txtValorTerrenoPropio.Text = "0"; }
                if (txtValorConstPropia.Text.Trim() == "") { txtValorConstPropia.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorTerrenoPropio.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorConstPropia.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + fechaSistemas + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + "0" + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + "1" + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtObservacionPro.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + "0" + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + Program.acceso_usuario.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + fechaSistemas + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + fechaSistemas + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + "ALTA" + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";

                con.cadena_sql_interno = con.cadena_sql_interno + "              ";
                con.cadena_sql_interno = con.cadena_sql_interno + "       SET NOCOUNT ON";
                con.cadena_sql_interno = con.cadena_sql_interno + "              ";

                con.cadena_sql_interno = con.cadena_sql_interno + "  INSERT INTO TEM_PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "             (";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Estado,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Municipio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Zona,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Manzana,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Lote,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Edificio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Depto,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Folio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Serie,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Uso,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UsoEsp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             PmnProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             RFC,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NumIntP,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             TelProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             DomFis,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             STerrProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             STerrCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SConsProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SConsCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             VTerrProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             VTerrCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             VConsProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             VConsCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             PtjeCondom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UltAnioPag,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UltMesPag,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UltimPPag,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Impto95,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Aclaracion,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             cObsProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             nValorFisc,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             FCaptura,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Baja,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Bonific";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";
                con.cadena_sql_interno = con.cadena_sql_interno + "         Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "             (";
                con.cadena_sql_interno = con.cadena_sql_interno + "             15";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtMun.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtDepto.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToInt32(txtFolio.Text.Trim());
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'0'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + cboUsoSuelo.Text.Trim().Substring(0, 1) + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + cboDestino.Text.Trim().Substring(0, 2) + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtPropietario.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'XAX010101000' ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtNoIntrior.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             , '1'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtDomicilioFiscal.Text.Trim() + "'";

                if (txtSupTerrenoPro.Text.Trim() == "") { txtValorTerrenoPropio.Text = "0"; }
                if (txtSupTerrenoComunPro.Text.Trim() == "") { txtValorConstPropia.Text = "0"; }
                if (txtSupConstruccionPro.Text.Trim() == "") { txtValorTerrenoComun.Text = "0"; }
                if (txtSupConstruccionComunPro.Text.Trim() == "") { txtValorConstComun.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupTerrenoPro.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupTerrenoComunPro.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupConstruccionPro.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupConstruccionComunPro.Text);

                if (txtValorTerrenoPropio.Text.Trim() == "") { txtValorTerrenoPropio.Text = "0"; }
                if (txtValorConstPropia.Text.Trim() == "") { txtValorConstPropia.Text = "0"; }
                if (txtValorTerrenoComun.Text.Trim() == "") { txtValorTerrenoComun.Text = "0"; }
                if (txtValorConstComun.Text.Trim() == "") { txtValorConstComun.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorTerrenoPropio.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorTerrenoComun.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorConstPropia.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorConstComun.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtIndiviso.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,1990";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,12";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'0'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtObservacionPro.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + fechaSistemas + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'0'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";

                con.cadena_sql_interno = con.cadena_sql_interno + "              ";
                con.cadena_sql_interno = con.cadena_sql_interno + "       SET NOCOUNT ON";
                con.cadena_sql_interno = con.cadena_sql_interno + "              ";

                con.cadena_sql_interno = con.cadena_sql_interno + "  INSERT INTO TEM_HPROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "             (";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Estado,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Municipio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Zona,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Manzana,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Lote,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Edificio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Depto,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Folio,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Serie,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Uso,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UsoEsp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             PmnProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             RFC,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             NumIntP,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             TelProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             DomFis,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             STerrProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             STerrCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SConsProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             SConsCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             VTerrProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             VTerrCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             VConsProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             VConsCom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             PtjeCondom,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UltAnioPag,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UltMesPag,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UltimPPag,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Impto95,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Aclaracion,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             cObsProp,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             nValorFisc,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             FCaptura,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Baja,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             UsrMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             FecMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             HoraMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             OperaMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             EdoOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             MpioOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ZonaOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             MznaOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             LoteOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             EdifOD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "             DeptoOD";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";
                con.cadena_sql_interno = con.cadena_sql_interno + "         Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "             (";
                con.cadena_sql_interno = con.cadena_sql_interno + "             15";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtMun.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtDepto.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtFolio.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'0'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + cboUsoSuelo.Text.Trim().Substring(0, 1) + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + cboDestino.Text.Trim().Substring(0, 2) + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtPropietario.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'XAX010101000' ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtNoIntrior.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             , '1'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtDomicilioFiscal.Text.Trim() + "'";

                if (txtSupTerrenoPro.Text.Trim() == "") { txtValorTerrenoPropio.Text = "0"; }
                if (txtSupTerrenoComunPro.Text.Trim() == "") { txtValorConstPropia.Text = "0"; }
                if (txtSupConstruccionPro.Text.Trim() == "") { txtValorTerrenoComun.Text = "0"; }
                if (txtSupConstruccionComunPro.Text.Trim() == "") { txtValorConstComun.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupTerrenoPro.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupTerrenoComunPro.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupConstruccionPro.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtSupConstruccionComunPro.Text);

                if (txtValorTerrenoPropio.Text.Trim() == "") { txtValorTerrenoPropio.Text = "0"; }
                if (txtValorConstPropia.Text.Trim() == "") { txtValorConstPropia.Text = "0"; }
                if (txtValorTerrenoComun.Text.Trim() == "") { txtValorTerrenoComun.Text = "0"; }
                if (txtValorConstComun.Text.Trim() == "") { txtValorConstComun.Text = "0"; }

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorTerrenoPropio.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorTerrenoComun.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorConstPropia.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + Convert.ToDecimal(txtValorConstComun.Text);

                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + txtIndiviso.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,1990";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,12";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'0'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + txtObservacionPro.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + fechaSistemas + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'0'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + Program.acceso_usuario + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + fechaSistemas + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ," + "'" + fechaSistemas + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'ALTA'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,0";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'0'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             ,'0'";
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";
                con.cadena_sql_interno = con.cadena_sql_interno + "              ";

                con.cadena_sql_interno = con.cadena_sql_interno + "       SET NOCOUNT ON";

                con.cadena_sql_interno = con.cadena_sql_interno + "              ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  Update CAT_DONDE_VA_2025";
            con.cadena_sql_interno = con.cadena_sql_interno + "     Set SISTEMAS = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , FECHA_SIS = GETDATE()";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , HORA_SIS  = GETDATE()";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , USU_SISTEMAS  =" + util.scm(Program.acceso_nombre_usuario);
            con.cadena_sql_interno = con.cadena_sql_interno + "           , OBSERVA_SISTEMA  =" + util.scm("TRAMITE OK");
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE FOLIO_ORIGEN =" + Convert.ToInt32(txtFolio.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND        SERIE =" + "'" + txtSerie.Text.Trim() + "'";


            con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                con.cerrar_interno();

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////  insertar EN PREDIOS Y PROPIEDADES     ////////////////////////////////////////////////////////////////////////////// 
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                if (txtEdificio.Text == "00" || txtDepto.Text == "0000")
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = " ";
                    con.cadena_sql_interno = con.cadena_sql_interno + " INSERT INTO PREDIOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + " (";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Estado, Municipio, Zona, Manzana, Lote, TipoPredio, RegProp, Domicilio, ZonaOrig, CodCalle, NumExt, Colonia,";
                    con.cadena_sql_interno = con.cadena_sql_interno + " CodPost, EntCalle, YCalle, SupTerrTot, SupTerrCom, SupCons, SupConsCom, Frente, Fondo, Desnivel, AreaInscr,";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Ubicacion, NFrente, NFFondo, NFIrreg, NFArea, NFTopogr, NFUbic, ValTerr, ValCons, FCaptura, Aclaracion,";
                    con.cadena_sql_interno = con.cadena_sql_interno + " cEdoPred , cObsPred, Baja";
                    con.cadena_sql_interno = con.cadena_sql_interno + " )";
                    con.cadena_sql_interno = con.cadena_sql_interno + " SELECT Estado, Municipio, Zona, Manzana, Lote, TipoPredio, RegProp, Domicilio, ZonaOrig, CodCalle, NumExt, Colonia,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "        CodPost, EntCalle, YCalle, SupTerrTot, SupTerrCom, SupCons, SupConsCom, Frente, Fondo, Desnivel, AreaInscr,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "        Ubicacion, NFrente, NFFondo, NFIrreg, NFArea, NFTopogr, NFUbic, ValTerr, ValCons, FCaptura, Aclaracion,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "        cEdoPred , cObsPred, Baja";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   From TEM_PREDIOS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  Where Zona    = " + txtZona.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana = " + txtMzna.Text.Trim();
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote    = " + txtLote.Text.Trim();

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();
                    con.cerrar_interno();
                }

                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + " INSERT INTO PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + " (";
                con.cadena_sql_interno = con.cadena_sql_interno + " Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto, Folio, Serie, Uso, UsoEsp, PmnProp, RFC, NumIntP,";
                con.cadena_sql_interno = con.cadena_sql_interno + " TelProp, DomFis, STerrProp, STerrCom, SConsProp, SConsCom, VTerrProp, VTerrCom, VConsProp, VConsCom, PtjeCondom,";
                con.cadena_sql_interno = con.cadena_sql_interno + " UltAnioPag , UltMesPag, UltimPPag, Impto95, Aclaracion, cObsProp, nValorFisc, FCaptura, Baja, Bonific";
                con.cadena_sql_interno = con.cadena_sql_interno + " )";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto, Folio, Serie, Uso, UsoEsp, PmnProp, RFC, NumIntP,";
                con.cadena_sql_interno = con.cadena_sql_interno + " TelProp, DomFis, STerrProp, STerrCom, SConsProp, SConsCom, VTerrProp, VTerrCom, VConsProp, VConsCom, PtjeCondom,";
                con.cadena_sql_interno = con.cadena_sql_interno + " UltAnioPag , UltMesPag, UltimPPag, Impto95, Aclaracion, cObsProp, nValorFisc, FCaptura, Baja, Bonific";
                con.cadena_sql_interno = con.cadena_sql_interno + "   From TEM_PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where     Zona = " + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND  Manzana = " + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND     Lote = " + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio = " + "'" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND    Depto = " + "'" + txtDepto.Text.Trim() + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                con.cerrar_interno();

                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno = " INSERT INTO HPROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + " (";
                con.cadena_sql_interno = con.cadena_sql_interno + " Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto, Folio, Serie, Uso, UsoEsp, PmnProp, RFC, NumIntP,";
                con.cadena_sql_interno = con.cadena_sql_interno + " TelProp, DomFis, STerrProp, STerrCom, SConsProp, SConsCom, VTerrProp, VTerrCom, VConsProp, VConsCom, PtjeCondom,";
                con.cadena_sql_interno = con.cadena_sql_interno + " UltAnioPag, UltMesPag, UltimPPag, Impto95, Aclaracion, cObsProp, nValorFisc, FCaptura, Baja, UsrMod, FecMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + " HoraMod , OperaMod, EdoOD, MpioOD, ZonaOD, MznaOD, LoteOD, EdifOD, DeptoOD";
                con.cadena_sql_interno = con.cadena_sql_interno + " )";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto, Folio, Serie, Uso, UsoEsp, PmnProp, RFC, NumIntP,";
                con.cadena_sql_interno = con.cadena_sql_interno + " TelProp, DomFis, STerrProp, STerrCom, SConsProp, SConsCom, VTerrProp, VTerrCom, VConsProp, VConsCom, PtjeCondom,";
                con.cadena_sql_interno = con.cadena_sql_interno + " UltAnioPag, UltMesPag, UltimPPag, Impto95, Aclaracion, cObsProp, nValorFisc, FCaptura, Baja, UsrMod, FecMod,";
                con.cadena_sql_interno = con.cadena_sql_interno + " HoraMod , OperaMod, EdoOD, MpioOD, ZonaOD, MznaOD, LoteOD, EdifOD, DeptoOD";
                con.cadena_sql_interno = con.cadena_sql_interno + "  From TEM_HPROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where     Zona = " + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND  Manzana = " + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND     Lote = " + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio = " + "'" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND    Depto = " + "'" + txtDepto.Text.Trim() + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                con.cerrar_interno();

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ////////////////  PROCEDEMOS AL BORRADO DE TEMPORALES    /////////////////////////////////////////////////////////////////////////////
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                con.conectar_base_interno();
                con.cadena_sql_interno = " ";

                con.cadena_sql_interno = con.cadena_sql_interno = " Delete TEM_PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where ESTADO    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Municipio = 41";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Zona      = " + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana   = " + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote      = " + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio  = " + "'" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto     = " + "'" + txtDepto.Text.Trim() + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                con.cerrar_interno();

                con.conectar_base_interno();
                con.cadena_sql_interno = " ";

                con.cadena_sql_interno = con.cadena_sql_interno = " Delete TEM_HPROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where ESTADO    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Municipio = 41";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Zona      = " + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana   = " + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote      = " + txtLote.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio  = " + "'" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto     = " + "'" + txtDepto.Text.Trim() + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                con.cerrar_interno();

                con.conectar_base_interno();
                con.cadena_sql_interno = " ";

                con.cadena_sql_interno = con.cadena_sql_interno = " Delete TEM_PREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where ESTADO    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Municipio = 41";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Zona      = " + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana   = " + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote      = " + txtLote.Text.Trim();

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                con.cerrar_interno();

                con.conectar_base_interno();
                con.cadena_sql_interno = " ";

                con.cadena_sql_interno = con.cadena_sql_interno = " Delete TEM_HPREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where ESTADO    = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Municipio = 41";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Zona      = " + txtZona.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana   = " + txtMzna.Text.Trim();
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote      = " + txtLote.Text.Trim();

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                con.cerrar_interno();

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                con.conectar_base_interno();
                con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "  Update CAT_DONDE_VA_2025";
            con.cadena_sql_interno = con.cadena_sql_interno + "     Set SISTEMAS = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , FECHA_SIS = GETDATE()";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , HORA_SIS  = GETDATE()";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , USU_SISTEMAS  =" + util.scm(Program.acceso_nombre_usuario);
            con.cadena_sql_interno = con.cadena_sql_interno + "           , OBSERVA_SISTEMA  =" + util.scm("TRAMITE OK");
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE FOLIO_ORIGEN =" + Convert.ToInt32(txtFolio.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND        SERIE =" + "'" + txtSerie.Text.Trim() + "'";

            con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                con.cerrar_interno();

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                double valor_terreno_m;
                double valor_terreno_comun_m;
                double valor_construccion_m;
                double valor_COMUN_m;

                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("songCalculoValorCat", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = 15;
                cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 3).Value = Convert.ToInt32(txtMun.Text.Trim());
                cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = Convert.ToInt32(txtZona.Text.Trim());
                cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = Convert.ToInt32(txtMzna.Text.Trim());
                cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = Convert.ToInt32(txtLote.Text.Trim());
                cmd.Parameters.Add("@EDIFICIO", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
                cmd.Parameters.Add("@DEPTO", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();
                cmd.Parameters.Add("@AÑO", SqlDbType.Int, 4).Value = Program.añoActual;

                cmd.Parameters.Add("@valorTerrenoPropio", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@valorTerrenoComun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@valorConstruccion", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@valorComun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@valorCatastral", SqlDbType.Float, 9).Direction = ParameterDirection.Output;

                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                valor_terreno_m = Convert.ToDouble(cmd.Parameters["@valorTerrenoPropio"].Value);
                valor_terreno_comun_m = Convert.ToDouble(cmd.Parameters["@valorTerrenoComun"].Value);
                valor_construccion_m = Convert.ToDouble(cmd.Parameters["@valorConstruccion"].Value);
                valor_COMUN_m = Convert.ToDouble(cmd.Parameters["@valorComun"].Value);

                con.cerrar_interno();

                txtValorTerrenoPropio.Text = valor_terreno_m.ToString("N2");
                txtValorTerrenoComun.Text = valor_terreno_comun_m.ToString("N2");
                txtValorConstPropia.Text = valor_construccion_m.ToString("N2");
                txtValorConstComun.Text = valor_COMUN_m.ToString("N2");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////








            return 1;
        }

        private int scriptBajas(int y)
        {
            string fechaSistemas = "";
            DateTime fechaActual = DateTime.Now;
            string fechaSistema = fechaActual.ToString("yyyyMMdd");
            string horaSistema = fechaActual.ToString("HH:mm:ss");

            fechaSistemas = fechaSistema + " " + horaSistema;

            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO HPREDIOS (Estado, Municipio, Zona, Manzana, Lote,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     TipoPredio, RegProp, Domicilio, ZonaOrig, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     CodCalle, NumExt, Colonia, CodPost, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     EntCalle, YCalle, SupTerrTot, SupTerrCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     SupCons, SupConsCom, Frente, Fondo, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Desnivel, AreaInscr, Ubicacion, NFrente,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     NFFondo, NFIrreg, NFArea, NFTopogr, NFUbic, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     ValTerr, ValCons, FCaptura, Aclaracion, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     cEdoPred, cObsPred, Baja, UsrMod,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     FecMod, HoraMod, OperaMod, EdoOD,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     MpioOD, ZonaOD, MznaOD, LoteOD)";
            con.cadena_sql_interno = con.cadena_sql_interno + "              SELECT Estado, Municipio, Zona, Manzana, Lote,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     TipoPredio, RegProp, Domicilio, ZonaOrig,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     CodCalle, NumExt, Colonia, CodPost,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     EntCalle, YCalle, SupTerrTot, SupTerrCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     SupCons, SupConsCom, Frente, Fondo, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Desnivel, AreaInscr, Ubicacion, NFrente,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     NFFondo, NFIrreg, NFArea, NFTopogr, NFUbic,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     ValTerr, ValCons, FCaptura, Aclaracion,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     cEdoPred, cObsPred, Baja, '" + Program.acceso_usuario.Trim() + "',";
            con.cadena_sql_interno = con.cadena_sql_interno + "                 '" + fechaSistemas + "', '" + fechaSistemas + "', 'BORRAR', Estado,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Municipio, Zona, Manzana, Lote";
            con.cadena_sql_interno = con.cadena_sql_interno + "                FROM PREDIOS";
            con.cadena_sql_interno = con.cadena_sql_interno + "               WHERE    Estado =    15";
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND Municipio =" + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND      Zona =" + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND   Manzana =" + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND      Lote =" + txtLote.Text.Trim();

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO HPROPIEDADES(Estado, Municipio, Zona, Manzana,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Lote, Edificio, Depto, Folio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Serie, Uso, UsoEsp, PmnProp,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         RFC, NumIntP, TelProp, DomFis,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         STerrProp, STerrCom, SConsProp, SConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         VTerrProp, VTerrCom, VConsProp, VConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         PtjeCondom, UltAnioPag, UltMesPag, UltimPPag,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Impto95, Aclaracion, cObsProp, nValorFisc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         FCaptura, Baja, UsrMod, FecMod,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         HoraMod, OperaMod, EdoOD, MpioOD,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         ZonaOD, MznaOD, LoteOD, EdifOD, DeptoOD)";
            con.cadena_sql_interno = con.cadena_sql_interno + "                  SELECT Estado, Municipio, Zona, Manzana,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Lote, Edificio, Depto, Folio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Serie, Uso, UsoEsp, PmnProp,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         RFC, NumIntP, TelProp, DomFis,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         STerrProp, STerrCom, SConsProp, SConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         VTerrProp, VTerrCom, VConsProp, VConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         PtjeCondom, UltAnioPag, UltMesPag, UltimPPag,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Impto95, Aclaracion, cObsProp, nValorFisc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         FCaptura, Baja, '" + Program.acceso_usuario.Trim() + "', '" + fechaSistemas + "',";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     '" + fechaSistemas + "', " + "'BORRAR', Estado, Municipio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Zona, Manzana, Lote, Edificio, Depto";
            con.cadena_sql_interno = con.cadena_sql_interno + "                    FROM PROPIEDADES";
            con.cadena_sql_interno = con.cadena_sql_interno + "                   WHERE Estado = 15";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND Municipio = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND      Zona = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND   Manzana = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND      Lote = " + txtLote.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND  Edificio = " + "'" + txtEdificio.Text + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND     Depto = " + "'" + txtDepto.Text + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + " DELETE PROPIEDADES";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE ESTADO    =     15";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND MUNICIPIO = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND ZONA      = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana   = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote      = " + txtLote.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio  = " + "'" + txtEdificio.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto     = " + "'" + txtDepto.Text.Trim() + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + " DELETE PREDIOS";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE ESTADO    =    15";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND MUNICIPIO = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND ZONA      = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana   = " + txtMzna.Text.Trim();   ;
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote      = " + txtLote.Text.Trim();

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();

            return 1;
        }

        private int scriptCambios(int y)
        {
            string fechaSistemas = "";
            DateTime fechaActual = DateTime.Now;
            string fechaSistema = fechaActual.ToString("yyyyMMdd");
            string horaSistema = fechaActual.ToString("HH:mm:ss");

            fechaSistemas = fechaSistema + " " + horaSistema;

            if (cboTipoPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL TIPO DE PREDIO", "ERROR", MessageBoxButtons.OK); cboTipoPredio.Focus(); return 0; }
            if (lblEdoPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL ESTADO DEL PREDIO", "ERROR", MessageBoxButtons.OK); lblEdoPredio.Focus(); return 0; }
            if (txtDomicilioPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO DEL PREDIO", "ERROR", MessageBoxButtons.OK); txtDomicilioPredio.Focus(); return 0; }
            if (txtZonaOrigen.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA DE ORIGEN", "ERROR", MessageBoxButtons.OK); txtZonaOrigen.Focus(); return 0; }
            if (txtCodigoCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL CODIGO DE CALLE", "ERROR", MessageBoxButtons.OK); txtCodigoCalle.Focus(); return 0; }
            if (cboCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA CALLE", "ERROR", MessageBoxButtons.OK); cboCalle.Focus(); return 0; }
            if (txtNoExterior.Text.Trim() == "") { MessageBox.Show("NO SE TIENE NUMERO EXTERIOR", "ERROR", MessageBoxButtons.OK); txtNoExterior.Focus(); return 0; }
            if (txtEnCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE ENTRE CALLE", "ERROR", MessageBoxButtons.OK); txtEnCalle.Focus(); return 0; }
            if (txtYcalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE ENTRE CALLE", "ERROR", MessageBoxButtons.OK); txtYcalle.Focus(); return 0; }
            if (txtCodigoPostal.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL CODIGO POSTAL", "ERROR", MessageBoxButtons.OK); txtCodigoPostal.Focus(); return 0; }
            if (txtColonia.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA COLONIA", "ERROR", MessageBoxButtons.OK); txtColonia.Focus(); return 0; }
            if (cboRegimenPropiedad.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL REGIMEN DE PROPIEDAD", "ERROR", MessageBoxButtons.OK); cboRegimenPropiedad.Focus(); return 0; }
            if (cboUbicacion.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA UBICACION", "ERROR", MessageBoxButtons.OK); cboUbicacion.Focus(); return 0; }
            if (txtSupTerreno.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE TERRENO", "ERROR", MessageBoxButtons.OK); txtSupTerreno.Focus(); return 0; }
            if (txtSupConstruccion.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtSupConstruccion.Focus(); return 0; }
            if (txtSupTerrenoComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtSupTerrenoComun.Focus(); return 0; }
            if (txtSupConstruccionComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtSupConstruccionComun.Focus(); return 0; }
            if (txtFrente.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FRENTE", "ERROR", MessageBoxButtons.OK); txtFrente.Focus(); return 0; }
            if (txtFondo.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FONDO", "ERROR", MessageBoxButtons.OK); txtFondo.Focus(); return 0; }
            if (txtDesnivel.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DESNIVEL", "ERROR", MessageBoxButtons.OK); txtDesnivel.Focus(); return 0; }
            if (txtArea.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA AREA", "ERROR", MessageBoxButtons.OK); txtArea.Focus(); return 0; }
            if (txtObservaciones.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LAS OBSERVACIONES DEL PREDIO", "ERROR", MessageBoxButtons.OK); txtObservaciones.Focus(); return 0; }
            if (txtNoIntrior.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL NUMERO INTERIOR", "ERROR", MessageBoxButtons.OK); txtNoIntrior.Focus(); return 0; }
            if (txtPropietario.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL PROPIETARIO", "ERROR", MessageBoxButtons.OK); txtPropietario.Focus(); return 0; }
            if (txtDomicilioPropietario.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO DEL PROPIETARIO", "ERROR", MessageBoxButtons.OK); txtDomicilioPropietario.Focus(); return 0; }
            if (txtDomicilioFiscal.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO FISCAL", "ERROR", MessageBoxButtons.OK); txtDomicilioFiscal.Focus(); return 0; }
            if (cboUsoSuelo.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL SUELO", "ERROR", MessageBoxButtons.OK); cboUsoSuelo.Focus(); return 0; }
            if (cboDestino.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DESTINO", "ERROR", MessageBoxButtons.OK); cboDestino.Focus(); return 0; }
            if (txtSupTerrenoPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. TERRENO PROPIA", "ERROR", MessageBoxButtons.OK); txtSupTerrenoPro.Focus(); return 0; }
            if (txtSupTerrenoComunPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtSupTerrenoComunPro.Focus(); return 0; }
            if (txtSupConstruccionPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. CONSTRUCCION PROPIA", "ERROR", MessageBoxButtons.OK); txtSupConstruccionPro.Focus(); return 0; }
            if (txtSupConstruccionComunPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtSupConstruccionComunPro.Focus(); return 0; }
            if (txtIndiviso.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL INDIVISO", "ERROR", MessageBoxButtons.OK); txtIndiviso.Focus(); return 0; }
            if (txtValorTerrenoPropio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE TERRENO PROPIO", "ERROR", MessageBoxButtons.OK); txtValorTerrenoPropio.Focus(); return 0; }
            if (txtValorTerrenoComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtValorTerrenoComun.Focus(); return 0; }
            if (txtValorConstPropia.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE CONSTRUCCION PROPIA", "ERROR", MessageBoxButtons.OK); txtValorConstPropia.Focus(); return 0; }
            if (txtValorConstComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtValorConstComun.Focus(); return 0; }
            if (txtValorCatastral.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR CATASTRAL", "ERROR", MessageBoxButtons.OK); txtValorCatastral.Focus(); return 0; }
            if (txtObservacionPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA OBSERVACION DE LA ALTA", "ERROR", MessageBoxButtons.OK); txtObservacionPro.Focus(); return 0; }

            int propiedadSioNo = 0;
            int prediosSioNo = 0;
            int tempPrediosSioNo = 0;

            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO HPREDIOS (Estado, Municipio, Zona, Manzana, Lote,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     TipoPredio, RegProp, Domicilio, ZonaOrig, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     CodCalle, NumExt, Colonia, CodPost, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     EntCalle, YCalle, SupTerrTot, SupTerrCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     SupCons, SupConsCom, Frente, Fondo, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Desnivel, AreaInscr, Ubicacion, NFrente,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     NFFondo, NFIrreg, NFArea, NFTopogr, NFUbic, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     ValTerr, ValCons, FCaptura, Aclaracion, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     cEdoPred, cObsPred, Baja, UsrMod,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     FecMod, HoraMod, OperaMod, EdoOD,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     MpioOD, ZonaOD, MznaOD, LoteOD)";
            con.cadena_sql_interno = con.cadena_sql_interno + "              SELECT Estado, Municipio, Zona, Manzana, Lote,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     TipoPredio, RegProp, Domicilio, ZonaOrig,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     CodCalle, NumExt, Colonia, CodPost,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     EntCalle, YCalle, SupTerrTot, SupTerrCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     SupCons, SupConsCom, Frente, Fondo, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Desnivel, AreaInscr, Ubicacion, NFrente,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     NFFondo, NFIrreg, NFArea, NFTopogr, NFUbic,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     ValTerr, ValCons, FCaptura, Aclaracion,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     cEdoPred, cObsPred, Baja, '" + Program.acceso_usuario.Trim() + "',";
            con.cadena_sql_interno = con.cadena_sql_interno + "                 '" + fechaSistemas + "', '" + fechaSistemas + "', 'CAMBIOS', Estado,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Municipio, Zona, Manzana, Lote";
            con.cadena_sql_interno = con.cadena_sql_interno + "                FROM PREDIOS";
            con.cadena_sql_interno = con.cadena_sql_interno + "               WHERE    Estado =    15";
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND Municipio =" + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND      Zona =" + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND   Manzana =" + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND      Lote =" + txtLote.Text.Trim();

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO HPROPIEDADES(Estado, Municipio, Zona, Manzana,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Lote, Edificio, Depto, Folio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Serie, Uso, UsoEsp, PmnProp,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         RFC, NumIntP, TelProp, DomFis,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         STerrProp, STerrCom, SConsProp, SConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         VTerrProp, VTerrCom, VConsProp, VConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         PtjeCondom, UltAnioPag, UltMesPag, UltimPPag,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Impto95, Aclaracion, cObsProp, nValorFisc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         FCaptura, Baja, UsrMod, FecMod,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         HoraMod, OperaMod, EdoOD, MpioOD,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         ZonaOD, MznaOD, LoteOD, EdifOD, DeptoOD)";
            con.cadena_sql_interno = con.cadena_sql_interno + "                  SELECT Estado, Municipio, Zona, Manzana,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Lote, Edificio, Depto, Folio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Serie, Uso, UsoEsp, PmnProp,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         RFC, NumIntP, TelProp, DomFis,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         STerrProp, STerrCom, SConsProp, SConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         VTerrProp, VTerrCom, VConsProp, VConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         PtjeCondom, UltAnioPag, UltMesPag, UltimPPag,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Impto95, Aclaracion, cObsProp, nValorFisc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         FCaptura, Baja, '" + Program.acceso_usuario.Trim() + "', '" + fechaSistemas + "',";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     '" + fechaSistemas + "', " + "'CAMBIOS', Estado, Municipio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Zona, Manzana, Lote, Edificio, Depto";
            con.cadena_sql_interno = con.cadena_sql_interno + "                    FROM PROPIEDADES";
            con.cadena_sql_interno = con.cadena_sql_interno + "                   WHERE Estado = 15";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND Municipio = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND      Zona = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND   Manzana = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND      Lote = " + txtLote.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND  Edificio = " + "'" + txtEdificio.Text + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND     Depto = " + "'" + txtDepto.Text + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + " Update PREDIOS";
            con.cadena_sql_interno = con.cadena_sql_interno + "    SET TipoPredio = " + cboTipoPredio.Text.Trim().Substring(0, 1);
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,RegProp    = " + cboRegimenPropiedad.Text.Trim().Substring(0, 1);
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Domicilio  = " + "'" + txtDomicilioPredio.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,ZonaOrig   = " + txtZonaOrigen.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,CodCalle   = " + txtCodigoCalle.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NumExt     = " + "'" + txtNoExterior.Text.Trim() + "'";

            if (txtCodigoPostal.Text.Trim() == "") txtCodigoPostal.Text = "00000";

            con.cadena_sql_interno = con.cadena_sql_interno + "       ,CodPost    = " + txtCodigoPostal.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,EntCalle   = " + "'" + txtEnCalle.Text.Trim()  + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,YCalle     = " + "'" + txtYcalle.Text.Trim()   + "'";

            if (txtSupTerreno.Text.Trim()           == "") txtSupTerreno.Text = "0";
            if (txtSupTerrenoComun.Text.Trim()      == "") txtSupTerrenoComun.Text = "0";
            if (txtSupConstruccion.Text.Trim()      == "") txtSupConstruccion.Text = "0";
            if (txtSupConstruccionComun.Text.Trim() == "") txtSupConstruccionComun.Text = "0";

            con.cadena_sql_interno = con.cadena_sql_interno + "       ,SupTerrTot = " + Convert.ToDecimal (txtSupTerreno.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,SupTerrCom = " + Convert.ToDecimal(txtSupTerrenoComun.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,SupCons    = " + Convert.ToDecimal(txtSupConstruccion.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,SupConsCom = " + Convert.ToDecimal(txtSupConstruccionComun.Text.Trim());

            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Frente     = " + Convert.ToDecimal(txtFrente.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Fondo      = " + Convert.ToDecimal(txtFondo.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Desnivel   = " + Convert.ToDecimal(txtDesnivel.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,AreaInscr  = " + Convert.ToDecimal(txtArea.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Ubicacion  = " + cboUbicacion.Text.Trim().Substring(0, 1);
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFrente    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFFondo    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFIrreg    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFArea     = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFTopogr   = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFUbic     = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,ValTerr    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,ValCons    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,FCaptura   = " + "'" + fechaSistemas + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Aclaracion = " + "'" + "0" + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,cEdoPred   = " + "'" + "1" + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,cObsPred   = " + "'" + txtObservacionPro.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Baja       = " + "'" + "0" + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE Estado    = 15";
            con.cadena_sql_interno = con.cadena_sql_interno + "    And Municipio = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    And Zona      = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    And Manzana   = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    And Lote      = " + txtLote.Text.Trim();

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "   Update PROPIEDADES";
            con.cadena_sql_interno = con.cadena_sql_interno + "      SET Folio      = " + txtFolio.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,Serie      = '0'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,Uso        = " + "'" + cboUsoSuelo.Text.Trim().Substring(0,1) + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,UsoEsp     = " + "'" + cboDestino.Text.Trim().Substring(0,2)  + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,PmnProp    = " + "'" + txtPropietario.Text.Trim()  + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,RFC        = 'XAX010101000' ";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,NumIntP    = " + "'" + txtNoIntrior.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,TelProp    = '1'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,DomFis     = " + "'" + txtDomicilioPropietario.Text.Trim()  + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,STerrProp  = " + Convert.ToDecimal(txtSupTerrenoPro.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,STerrCom   = " + Convert.ToDecimal(txtSupTerrenoComunPro.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,SConsProp  = " + Convert.ToDecimal(txtSupConstruccionPro.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,SConsCom   = " + Convert.ToDecimal(txtSupConstruccionComunPro.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,VTerrProp  = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,VTerrCom   = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,VConsProp  = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,VConsCom   = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,PtjeCondom = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,cObsProp   = " + "'" + txtObservacionPro.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,FCaptura   = " + "'" + fechaSistemas + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE Estado = 15";
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Municipio = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Zona      = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Manzana   = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Lote      = " + txtLote.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Edificio  = " + "'" + txtEdificio.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Depto     = " + "'" + txtDepto.Text.Trim() + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            double valor_terreno_m;
            double valor_terreno_comun_m;
            double valor_construccion_m;
            double valor_COMUN_m;

            con.conectar_base_interno();
            con.open_c_interno();

            SqlCommand cmd = new SqlCommand("songCalculoValorCat", con.cnn_interno);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = 15;
            cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 3).Value = Convert.ToInt32(txtMun.Text.Trim());
            cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = Convert.ToInt32(txtZona.Text.Trim());
            cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = Convert.ToInt32(txtMzna.Text.Trim());
            cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = Convert.ToInt32(txtLote.Text.Trim());
            cmd.Parameters.Add("@EDIFICIO", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
            cmd.Parameters.Add("@DEPTO", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();
            cmd.Parameters.Add("@AÑO", SqlDbType.Int, 4).Value = Program.añoActual;

            cmd.Parameters.Add("@valorTerrenoPropio", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@valorTerrenoComun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@valorConstruccion", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@valorComun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@valorCatastral", SqlDbType.Float, 9).Direction = ParameterDirection.Output;

            cmd.Connection = con.cnn_interno;
            cmd.ExecuteNonQuery();

            valor_terreno_m = Convert.ToDouble(cmd.Parameters["@valorTerrenoPropio"].Value);
            valor_terreno_comun_m = Convert.ToDouble(cmd.Parameters["@valorTerrenoComun"].Value);
            valor_construccion_m = Convert.ToDouble(cmd.Parameters["@valorConstruccion"].Value);
            valor_COMUN_m = Convert.ToDouble(cmd.Parameters["@valorComun"].Value);

            con.cerrar_interno();

            txtValorTerrenoPropio.Text = valor_terreno_m.ToString("N2");
            txtValorTerrenoComun.Text = valor_terreno_comun_m.ToString("N2");
            txtValorConstPropia.Text = valor_construccion_m.ToString("N2");
            txtValorConstComun.Text = valor_COMUN_m.ToString("N2");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "  Update CAT_DONDE_VA_2025";
            con.cadena_sql_interno = con.cadena_sql_interno + "     Set SISTEMAS = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , FECHA_SIS = GETDATE()";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , HORA_SIS  = GETDATE()";
            con.cadena_sql_interno = con.cadena_sql_interno + "           , USU_SISTEMAS  =" + util.scm(Program.acceso_nombre_usuario);
            con.cadena_sql_interno = con.cadena_sql_interno + "           , OBSERVA_SISTEMA  =" + util.scm("TRAMITE OK");
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE FOLIO_ORIGEN =" + Convert.ToInt32(txtFolio.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND        SERIE =" + "'" + txtSerie.Text.Trim() + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();

            return 1;
        }

        private int scriptCambiosGenerales(int y)
        {
            string fechaSistemas = "";
            DateTime fechaActual = DateTime.Now;
            string fechaSistema = fechaActual.ToString("yyyyMMdd");
            string horaSistema = fechaActual.ToString("HH:mm:ss");

            fechaSistemas = fechaSistema + " " + horaSistema;

            if (cboTipoPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL TIPO DE PREDIO", "ERROR", MessageBoxButtons.OK); cboTipoPredio.Focus(); return 0; }
            if (lblEdoPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL ESTADO DEL PREDIO", "ERROR", MessageBoxButtons.OK); lblEdoPredio.Focus(); return 0; }
            if (txtDomicilioPredio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO DEL PREDIO", "ERROR", MessageBoxButtons.OK); txtDomicilioPredio.Focus(); return 0; }
            if (txtZonaOrigen.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA DE ORIGEN", "ERROR", MessageBoxButtons.OK); txtZonaOrigen.Focus(); return 0; }
            if (txtCodigoCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL CODIGO DE CALLE", "ERROR", MessageBoxButtons.OK); txtCodigoCalle.Focus(); return 0; }
            if (cboCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA CALLE", "ERROR", MessageBoxButtons.OK); cboCalle.Focus(); return 0; }
            if (txtNoExterior.Text.Trim() == "") { MessageBox.Show("NO SE TIENE NUMERO EXTERIOR", "ERROR", MessageBoxButtons.OK); txtNoExterior.Focus(); return 0; }
            if (txtEnCalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE ENTRE CALLE", "ERROR", MessageBoxButtons.OK); txtEnCalle.Focus(); return 0; }
            if (txtYcalle.Text.Trim() == "") { MessageBox.Show("NO SE TIENE ENTRE CALLE", "ERROR", MessageBoxButtons.OK); txtYcalle.Focus(); return 0; }
            if (txtCodigoPostal.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL CODIGO POSTAL", "ERROR", MessageBoxButtons.OK); txtCodigoPostal.Focus(); return 0; }
            if (txtColonia.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA COLONIA", "ERROR", MessageBoxButtons.OK); txtColonia.Focus(); return 0; }
            if (cboRegimenPropiedad.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL REGIMEN DE PROPIEDAD", "ERROR", MessageBoxButtons.OK); cboRegimenPropiedad.Focus(); return 0; }
            if (cboUbicacion.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA UBICACION", "ERROR", MessageBoxButtons.OK); cboUbicacion.Focus(); return 0; }
            if (txtSupTerreno.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE TERRENO", "ERROR", MessageBoxButtons.OK); txtSupTerreno.Focus(); return 0; }
            if (txtSupConstruccion.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtSupConstruccion.Focus(); return 0; }
            if (txtSupTerrenoComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtSupTerrenoComun.Focus(); return 0; }
            if (txtSupConstruccionComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. DE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtSupConstruccionComun.Focus(); return 0; }
            if (txtFrente.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FRENTE", "ERROR", MessageBoxButtons.OK); txtFrente.Focus(); return 0; }
            if (txtFondo.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL FONDO", "ERROR", MessageBoxButtons.OK); txtFondo.Focus(); return 0; }
            if (txtDesnivel.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DESNIVEL", "ERROR", MessageBoxButtons.OK); txtDesnivel.Focus(); return 0; }
            if (txtArea.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA AREA", "ERROR", MessageBoxButtons.OK); txtArea.Focus(); return 0; }
            if (txtObservaciones.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LAS OBSERVACIONES DEL PREDIO", "ERROR", MessageBoxButtons.OK); txtObservaciones.Focus(); return 0; }
            if (txtNoIntrior.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL NUMERO INTERIOR", "ERROR", MessageBoxButtons.OK); txtNoIntrior.Focus(); return 0; }
            if (txtPropietario.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL PROPIETARIO", "ERROR", MessageBoxButtons.OK); txtPropietario.Focus(); return 0; }
            if (txtDomicilioPropietario.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO DEL PROPIETARIO", "ERROR", MessageBoxButtons.OK); txtDomicilioPropietario.Focus(); return 0; }
            if (txtDomicilioFiscal.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DOMICILIO FISCAL", "ERROR", MessageBoxButtons.OK); txtDomicilioFiscal.Focus(); return 0; }
            if (cboUsoSuelo.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL SUELO", "ERROR", MessageBoxButtons.OK); cboUsoSuelo.Focus(); return 0; }
            if (cboDestino.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DESTINO", "ERROR", MessageBoxButtons.OK); cboDestino.Focus(); return 0; }
            if (txtSupTerrenoPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. TERRENO PROPIA", "ERROR", MessageBoxButtons.OK); txtSupTerrenoPro.Focus(); return 0; }
            if (txtSupTerrenoComunPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtSupTerrenoComunPro.Focus(); return 0; }
            if (txtSupConstruccionPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. CONSTRUCCION PROPIA", "ERROR", MessageBoxButtons.OK); txtSupConstruccionPro.Focus(); return 0; }
            if (txtSupConstruccionComunPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA SUP. CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtSupConstruccionComunPro.Focus(); return 0; }
            if (txtIndiviso.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL INDIVISO", "ERROR", MessageBoxButtons.OK); txtIndiviso.Focus(); return 0; }
            if (txtValorTerrenoPropio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE TERRENO PROPIO", "ERROR", MessageBoxButtons.OK); txtValorTerrenoPropio.Focus(); return 0; }
            if (txtValorTerrenoComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE TERRENO COMUN", "ERROR", MessageBoxButtons.OK); txtValorTerrenoComun.Focus(); return 0; }
            if (txtValorConstPropia.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE CONSTRUCCION PROPIA", "ERROR", MessageBoxButtons.OK); txtValorConstPropia.Focus(); return 0; }
            if (txtValorConstComun.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR DE CONSTRUCCION COMUN", "ERROR", MessageBoxButtons.OK); txtValorConstComun.Focus(); return 0; }
            if (txtValorCatastral.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL VALOR CATASTRAL", "ERROR", MessageBoxButtons.OK); txtValorCatastral.Focus(); return 0; }
            if (txtObservacionPro.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA OBSERVACION DE LA ALTA", "ERROR", MessageBoxButtons.OK); txtObservacionPro.Focus(); return 0; }

            int propiedadSioNo = 0;
            int prediosSioNo = 0;
            int tempPrediosSioNo = 0;

            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO HPREDIOS (Estado, Municipio, Zona, Manzana, Lote,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     TipoPredio, RegProp, Domicilio, ZonaOrig, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     CodCalle, NumExt, Colonia, CodPost, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     EntCalle, YCalle, SupTerrTot, SupTerrCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     SupCons, SupConsCom, Frente, Fondo, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Desnivel, AreaInscr, Ubicacion, NFrente,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     NFFondo, NFIrreg, NFArea, NFTopogr, NFUbic, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     ValTerr, ValCons, FCaptura, Aclaracion, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     cEdoPred, cObsPred, Baja, UsrMod,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     FecMod, HoraMod, OperaMod, EdoOD,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     MpioOD, ZonaOD, MznaOD, LoteOD)";
            con.cadena_sql_interno = con.cadena_sql_interno + "              SELECT Estado, Municipio, Zona, Manzana, Lote,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     TipoPredio, RegProp, Domicilio, ZonaOrig,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     CodCalle, NumExt, Colonia, CodPost,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     EntCalle, YCalle, SupTerrTot, SupTerrCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     SupCons, SupConsCom, Frente, Fondo, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Desnivel, AreaInscr, Ubicacion, NFrente,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     NFFondo, NFIrreg, NFArea, NFTopogr, NFUbic,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     ValTerr, ValCons, FCaptura, Aclaracion,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     cEdoPred, cObsPred, Baja, '" + Program.acceso_usuario.Trim() + "',";
            con.cadena_sql_interno = con.cadena_sql_interno + "                 '" + fechaSistemas + "', '" + fechaSistemas + "', 'CAMBIOS', Estado,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     Municipio, Zona, Manzana, Lote";
            con.cadena_sql_interno = con.cadena_sql_interno + "                FROM PREDIOS";
            con.cadena_sql_interno = con.cadena_sql_interno + "               WHERE    Estado =    15";
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND Municipio =" + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND      Zona =" + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND   Manzana =" + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                 AND      Lote =" + txtLote.Text.Trim();

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO HPROPIEDADES(Estado, Municipio, Zona, Manzana,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Lote, Edificio, Depto, Folio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Serie, Uso, UsoEsp, PmnProp,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         RFC, NumIntP, TelProp, DomFis,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         STerrProp, STerrCom, SConsProp, SConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         VTerrProp, VTerrCom, VConsProp, VConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         PtjeCondom, UltAnioPag, UltMesPag, UltimPPag,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Impto95, Aclaracion, cObsProp, nValorFisc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         FCaptura, Baja, UsrMod, FecMod,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         HoraMod, OperaMod, EdoOD, MpioOD,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         ZonaOD, MznaOD, LoteOD, EdifOD, DeptoOD)";
            con.cadena_sql_interno = con.cadena_sql_interno + "                  SELECT Estado, Municipio, Zona, Manzana,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Lote, Edificio, Depto, Folio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Serie, Uso, UsoEsp, PmnProp,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         RFC, NumIntP, TelProp, DomFis,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         STerrProp, STerrCom, SConsProp, SConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         VTerrProp, VTerrCom, VConsProp, VConsCom,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         PtjeCondom, UltAnioPag, UltMesPag, UltimPPag,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Impto95, Aclaracion, cObsProp, nValorFisc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         FCaptura, Baja, '" + Program.acceso_usuario.Trim() + "', '" + fechaSistemas + "',";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     '" + fechaSistemas + "', " + "'CAMBIOS', Estado, Municipio,";
            con.cadena_sql_interno = con.cadena_sql_interno + "                         Zona, Manzana, Lote, Edificio, Depto";
            con.cadena_sql_interno = con.cadena_sql_interno + "                    FROM PROPIEDADES";
            con.cadena_sql_interno = con.cadena_sql_interno + "                   WHERE Estado = 15";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND Municipio = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND      Zona = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND   Manzana = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND      Lote = " + txtLote.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND  Edificio = " + "'" + txtEdificio.Text + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "                     AND     Depto = " + "'" + txtDepto.Text + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + " Update PREDIOS";
            con.cadena_sql_interno = con.cadena_sql_interno + "    SET TipoPredio = " + cboTipoPredio.Text.Trim().Substring(0, 1);
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,RegProp    = " + cboRegimenPropiedad.Text.Trim().Substring(0, 1);
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Domicilio  = " + "'" + txtDomicilioPredio.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,ZonaOrig   = " + txtZonaOrigen.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,CodCalle   = " + txtCodigoCalle.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NumExt     = " + "'" + txtNoExterior.Text.Trim() + "'";

            if (txtCodigoPostal.Text.Trim() == "") txtCodigoPostal.Text = "00000";

            con.cadena_sql_interno = con.cadena_sql_interno + "       ,CodPost    = " + txtCodigoPostal.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,EntCalle   = " + "'" + txtEnCalle.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,YCalle     = " + "'" + txtYcalle.Text.Trim() + "'";

            if (txtSupTerreno.Text.Trim() == "") txtSupTerreno.Text = "0";
            if (txtSupTerrenoComun.Text.Trim() == "") txtSupTerrenoComun.Text = "0";
            if (txtSupConstruccion.Text.Trim() == "") txtSupConstruccion.Text = "0";
            if (txtSupConstruccionComun.Text.Trim() == "") txtSupConstruccionComun.Text = "0";

            con.cadena_sql_interno = con.cadena_sql_interno + "       ,SupTerrTot = " + Convert.ToDecimal(txtSupTerreno.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,SupTerrCom = " + Convert.ToDecimal(txtSupTerrenoComun.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,SupCons    = " + Convert.ToDecimal(txtSupConstruccion.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,SupConsCom = " + Convert.ToDecimal(txtSupConstruccionComun.Text.Trim());

            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Frente     = " + Convert.ToDecimal(txtFrente.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Fondo      = " + Convert.ToDecimal(txtFondo.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Desnivel   = " + Convert.ToDecimal(txtDesnivel.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,AreaInscr  = " + Convert.ToDecimal(txtArea.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Ubicacion  = " + cboUbicacion.Text.Trim().Substring(0, 1);
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFrente    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFFondo    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFIrreg    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFArea     = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFTopogr   = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,NFUbic     = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,ValTerr    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,ValCons    = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,FCaptura   = " + "'" + fechaSistemas + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Aclaracion = " + "'" + "0" + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,cEdoPred   = " + "'" + "1" + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,cObsPred   = " + "'" + txtObservacionPro.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ,Baja       = " + "'" + "0" + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE Estado    = 15";
            con.cadena_sql_interno = con.cadena_sql_interno + "    And Municipio = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    And Zona      = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    And Manzana   = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    And Lote      = " + txtLote.Text.Trim();

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();


            con.conectar_base_interno();
            con.cadena_sql_interno = " ";

            con.cadena_sql_interno = con.cadena_sql_interno + "   Update PROPIEDADES";
            con.cadena_sql_interno = con.cadena_sql_interno + "      SET Uso        = " + "'" + cboUsoSuelo.Text.Trim().Substring(0, 1) + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,UsoEsp     = " + "'" + cboDestino.Text.Trim().Substring(0, 2) + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,PmnProp    = " + "'" + txtPropietario.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,RFC        = 'XAX010101000' ";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,NumIntP    = " + "'" + txtNoIntrior.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,TelProp    = '1'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,DomFis     = " + "'" + txtDomicilioPropietario.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,STerrProp  = " + Convert.ToDecimal(txtSupTerrenoPro.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,STerrCom   = " + Convert.ToDecimal(txtSupTerrenoComunPro.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,SConsProp  = " + Convert.ToDecimal(txtSupConstruccionPro.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,SConsCom   = " + Convert.ToDecimal(txtSupConstruccionComunPro.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,VTerrProp  = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,VTerrCom   = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,VConsProp  = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,VConsCom   = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,PtjeCondom = " + "0";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,cObsProp   = " + "'" + txtObservacionPro.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "         ,FCaptura   = " + "'" + fechaSistemas + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE Estado = 15";
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Municipio = " + txtMun.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Zona      = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Manzana   = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Lote      = " + txtLote.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Edificio  = " + "'" + txtEdificio.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "     And Depto     = " + "'" + txtDepto.Text.Trim() + "'";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            double valor_terreno_m;
            double valor_terreno_comun_m;
            double valor_construccion_m;
            double valor_COMUN_m;

            con.conectar_base_interno();
            con.open_c_interno();

            SqlCommand cmd = new SqlCommand("songCalculoValorCat", con.cnn_interno);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = 15;
            cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 3).Value = Convert.ToInt32(txtMun.Text.Trim());
            cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = Convert.ToInt32(txtZona.Text.Trim());
            cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = Convert.ToInt32(txtMzna.Text.Trim());
            cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = Convert.ToInt32(txtLote.Text.Trim());
            cmd.Parameters.Add("@EDIFICIO", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
            cmd.Parameters.Add("@DEPTO", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();
            cmd.Parameters.Add("@AÑO", SqlDbType.Int, 4).Value = Program.añoActual;

            cmd.Parameters.Add("@valorTerrenoPropio", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@valorTerrenoComun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@valorConstruccion", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@valorComun", SqlDbType.Float, 9).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@valorCatastral", SqlDbType.Float, 9).Direction = ParameterDirection.Output;

            cmd.Connection = con.cnn_interno;
            cmd.ExecuteNonQuery();

            valor_terreno_m = Convert.ToDouble(cmd.Parameters["@valorTerrenoPropio"].Value);
            valor_terreno_comun_m = Convert.ToDouble(cmd.Parameters["@valorTerrenoComun"].Value);
            valor_construccion_m = Convert.ToDouble(cmd.Parameters["@valorConstruccion"].Value);
            valor_COMUN_m = Convert.ToDouble(cmd.Parameters["@valorComun"].Value);

            con.cerrar_interno();

            txtValorTerrenoPropio.Text = valor_terreno_m.ToString("N2");
            txtValorTerrenoComun.Text = valor_terreno_comun_m.ToString("N2");
            txtValorConstPropia.Text = valor_construccion_m.ToString("N2");
            txtValorConstComun.Text = valor_COMUN_m.ToString("N2");

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            return 1;

        }


        private int bloquearClaveSioNo(int y) 
        {
            int validacion = 0;    // valor catastral menor 1, valor catastral mayor = 0

            con.conectar_base_interno();
            con.open_c_interno();

            SqlCommand cmd = new SqlCommand("songValidaValorCat", con.cnn_interno);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = 15;
            cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 3).Value = Convert.ToInt32(txtMun.Text.Trim());
            cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = Convert.ToInt32(txtZona.Text.Trim());
            cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = Convert.ToInt32(txtMzna.Text.Trim());
            cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = Convert.ToInt32(txtLote.Text.Trim());
            cmd.Parameters.Add("@EDIFICIO", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
            cmd.Parameters.Add("@DEPTO", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();

            cmd.Parameters.Add("@SINO", SqlDbType.Float, 9).Direction = ParameterDirection.Output;

            cmd.Connection = con.cnn_interno;
            cmd.ExecuteNonQuery();

            validacion = Convert.ToInt32(cmd.Parameters["@SINO"].Value);

            con.cerrar_interno();

            if (validacion == 1) 
            { 
                return 1;
            }
            else
            {
                return 0;
            }  
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //OBTENEMOS LA FECHA Y HORA ACTUAL DEL SISTEMA

            string fechaSistemas = "";
            DateTime fechaActual = DateTime.Now;
            string fechaSistema = fechaActual.ToString("yyyyMMdd");
            string horaSistema = fechaActual.ToString("hh:mm:ss tt");

            fechaSistemas = fechaSistema + " " + horaSistema;
            String claveCatastral = txtMun.Text.Trim() + "-" + txtZona.Text.Trim() + "-" + txtMzna.Text.Trim() + "-" + txtLote.Text.Trim() + "-" + txtEdificio.Text.Trim() + "-" + txtDepto.Text.Trim();

            if (tipoDeMovimiento == 0)              // inicio
            {
                MessageBox.Show("NO SE TIENE NINGUN MOVIMIENTO SELECCIONADO", "ERROR", MessageBoxButtons.OK);
                return;
            }   // inicio

            if (tipoDeMovimiento == 1)              // Altas
            {
                int resultado = scriptAltass(0);
                if (resultado == 1) 
                { 
                    MessageBox.Show("CLAVE CATASTRAL GUARDADA CON EXITO", "INFORMATIVO", MessageBoxButtons.OK);
                    refreshh();
                    return;
                }
                else { return; }
            }   // Altas

            if (tipoDeMovimiento == 2)              // Bajas
            {
                DialogResult resultadoBaja = MessageBox.Show("¿ESTÁ SEGURO DE QUE DESEA ELIMINAR ESTA CLAVE CATASTRAL ?, " + claveCatastral, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultadoBaja == DialogResult.Yes)
                {
                    int resultadoScriptBaja = scriptBajas(0);
                    if (resultadoScriptBaja == 1) 
                        {
                        MessageBox.Show("CLAVE CATASTRAL BORRADA CON EXITO", "INFORMATIVO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refreshh();
                        return;
                        }
                    else { return; }
                }
                else
                {
                    MessageBox.Show("Operación cancelada", "Cancelada");
                    return;
                }
            }   // Bajas

            if (tipoDeMovimiento == 3)      // Cambios
            {
                DialogResult resultadoCambio = MessageBox.Show("¿ESTÁ SEGURO DE QUE DESEA MODIFICAR LA INFORMACION DE ESTA CLAVE CATASTRAL ?, " + claveCatastral, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultadoCambio == DialogResult.Yes)
                {
                    int resultadoScriptCambio = scriptCambios(0);
                    if (resultadoScriptCambio == 1)
                    {
                        int resultadoComparativo = bloquearClaveSioNo(0);
                        if (resultadoComparativo == 1)
                        {
                            MessageBox.Show("CLAVE CATASTRAL BLOQUEADA, POR MODIFICAR VALOR CATASTRAL", "INFORMATIVO", MessageBoxButtons.OK);
                            refreshh();
                            return;
                        }

                        DialogResult resultadoCambioo = MessageBox.Show("¿ DESEAS SEGUIR MODIFICANDO LOS DATOS DE LA CLAVE CATASTRAL ?, " + claveCatastral, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultadoCambioo == DialogResult.Yes)
                        {
                            txtPropietario.Focus();
                            return;
                        }
                        else
                        {
                            refreshh();
                            return;
                        }
                    }
                    else { return; }
                }
                else
                {
                    MessageBox.Show("Operación cancelada", "Cancelada");
                    return;
                }
            }   // Cambios

            if (tipoDeMovimiento == 4)              // Consultas generales
            {

            }   // Consultas generales

            if (tipoDeMovimiento == 5)              // cambios generales
            {
                DialogResult resultadoCambio = MessageBox.Show("¿ESTÁ SEGURO DE QUE DESEA MODIFICAR LA INFORMACION DE ESTA CLAVE CATASTRAL ?, " + claveCatastral, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultadoCambio == DialogResult.Yes)
                {
                    int resultadoScriptCambio = scriptCambiosGenerales(0);
                    if (resultadoScriptCambio == 1)
                    {
                        int resultadoComparativo = bloquearClaveSioNo(0);
                        if (resultadoComparativo == 1)
                        {
                            MessageBox.Show("CLAVE CATASTRAL BLOQUEADA, POR MODIFICAR VALOR CATASTRAL", "INFORMATIVO", MessageBoxButtons.OK);
                            refreshh();
                            return;
                        }

                        DialogResult resultadoCambioo = MessageBox.Show("¿ ¡¡CAMBIOS REALIZADOS!!,  ¿DESEAS SEGUIR MODIFICANDO LOS DATOS DE LA CLAVE CATASTRAL?, " + claveCatastral, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (resultadoCambioo == DialogResult.Yes)
                        {
                            txtPropietario.Focus();
                            return;
                        }
                        else
                        {
                            refreshh();
                            return;
                        }
                    }
                    else { return; }
                }
                else
                {
                    MessageBox.Show("Operación cancelada", "Cancelada");
                    return;
                }
            }   // Cambios Generales
        }

        private void cboUsoSuelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboDestino.Items.Clear();

            if (cboUsoSuelo.Text != "")
            {
                cboDestino.Items.Clear();
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT UsoEsp, Descrip ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM DESTINO ";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Uso  = '" + cboUsoSuelo.Text.Trim().Substring(0, 1) + "'";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    cboDestino.Items.Add(con.leer_interno[0].ToString().Trim() + " " + con.leer_interno[1].ToString().Trim());
                }
                con.cerrar_interno();

                //cboDestino.SelectedIndex = 0;

            }
            // LLENAMOS EL COMBOBOX DE DESTINO
        }

        private void txtDomicilioPropietario_TextChanged(object sender, EventArgs e)
        {
            txtDomicilioFiscal.Text = txtDomicilioPropietario.Text;
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
        }

        private void btnBuscar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnBuscar, "BUSCAR");
        }

        private void btnCancelar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnCancelar, "CANCELAR");
        }

        private void cmdSalida_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdSalida, "SALIR");
        }

        private void btnConsulta_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnConsulta, "CONSULTA");
        }

        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnRefresh, "REFRESCAR MOVIMIENTOS");
        }

        private void btnAceptar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnAceptar, "ACEPTAR");
        }

        private void cmdAlDia_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdAlDia, "AL DIA");
        }

        private void cmdDiasAnteriores_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdDiasAnteriores, "DIAS ANTERIORES");
        }

        private void cmdRefresh_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdRefresh, "REFRESCAR GRID");
        }

        private void btnMinimizar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnMinimizar, "MINIMISAR");
        }

        private void cmdSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboCalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCodigoCalle.Text = cboCalle.Text.Substring(0, 3);
        }
    }
}


////MEMO 