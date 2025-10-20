using AccesoBase;
using GMap.NET.MapProviders;
//using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Utilerias;
using Form = System.Windows.Forms.Form;
namespace SMACatastro.catastroRevision
{
    public partial class frmVentanilla : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();
        int ZONA, MANZANA, LOTE, MUNICIPIO;
        int ubicacion = 0, historial = 0;
        string EDIFICIO, DEPTO, SERIE, FOLIO;
        double TERRENO1, TERRENO2, TERRENO3, TERRENO4, TERRENO5, terreno6, latitud, longitud = 0.0;
        double CONSTRUCCION1, CONSTRUCCION2, CONSTRUCCION3, CONSTRUCCION4, CONSTRUCCION5 = 0, CONSTRUCCION6, VALORCATASTRAL;
        ////////////////////////////////////////////////////////////
        ///////////////// -------INICIALIZA COMPONENTE
        ////////////////////////////////////////////////////////////
        public frmVentanilla()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////
        ///////////////// -------PARA ARRASTRAR EL PANEL 
        ////////////////////////////////////////////////////////////
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void frmBloqueoDesbloqueo_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "Usuario: " + Program.nombre_usuario.Trim();
            limpiartodo();
        }
        void llenarCboSerie()
        {
            //Llenamos el combo de serie con las series ocupadas en cat_new_cartografia_2025

            CBO_SERIE.Items.Clear();
            con.conectar_base_interno();
            con.open_c_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT DISTINCT SERIE ";
            con.cadena_sql_interno = con.cadena_sql_interno + "     FROM CAT_NEW_CARTOGRAFIA_2025 ";
            con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY SERIE DESC ";
            con.cadena_sql_cmd_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                CBO_SERIE.Items.Add(con.leer_interno[0].ToString().Trim());
            }

            CBO_SERIE.SelectedIndex = 0;
            con.cerrar_interno();
        }
        private bool focoEstablecido = false;
        private void frmBloqueoDesbloqueo_Activated(object sender, EventArgs e)
        {
            if (!focoEstablecido)
            {
                txtZona.Focus();
                focoEstablecido = true;
            }
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

        private void LIMPIAR_2()
        {
            //limpiamos los label de los paneles altaas y datos predio junto a complementos ( se ocupa al dar doble clic en el datagridview)
            //////////textboxis
            txtZona.Text = "";
            txtManzana.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";
            TXT_FOLIO.Text = "";
            //deshabilitar cajas 
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            TXT_FOLIO.Enabled = false;
            btnConsulta.Enabled = false;
            btnBuscarClave.Enabled = false;
            btnSalida.Enabled = false;
            btnNoAutorizar.Enabled = false;

            ////////label
            lblTitular.Text = "";
            lblDomicilio.Text = "";
            lblSupConsPriv.Text = "";
            lblValorConsPriv.Text = "";
            lblSupConsCom.Text = "";
            lblValConsCom.Text = "";
            lblSupTerrPriv.Text = "";
            lblValTerrPriv.Text = "";
            lblSupTerrComun.Text = "";
            lblValTerrCom.Text = "";
            lblValTotCons.Text = "";
            lblValTotTerr.Text = "";
            lblValor.Text = "";
            // llenarCboSerie();
            lblLatitud.Text = "";
            lblLongitud.Text = "";
            lblNoAutorizado.Text = "";
            btnMaps.Enabled = false;
            btnAutorizar.Enabled = false;
            gMapControl1.Visible = false;
            lblObsCar.Text = "";
            lblUbicacion.Text = "";
            pnlCambios.Visible = true;
            DesmarcarChecksRecursivo(pnlCambios);


            lblComentario.Visible = true;
            pnlDatosPredio.Visible = true;
            pnlAlta.Visible = false;
            //valores que ocupamos
            TERRENO1 = 0;
            TERRENO2 = 0;
            TERRENO3 = 0;
            TERRENO4 = 0;
            TERRENO5 = 0;
            CONSTRUCCION1 = 0;
            CONSTRUCCION2 = 0;
            CONSTRUCCION3 = 0;
            CONSTRUCCION4 = 0;
            CONSTRUCCION5 = 0;
        }

        void limpiartodo()
        {
            //limpieza en general, como al inicio
            //////////textboxis
            txtZona.Text = "";
            txtManzana.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";
            TXT_FOLIO.Text = "";
            //deshabilitar cajas 
            txtZona.Enabled = true;
            txtManzana.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;
            TXT_FOLIO.Enabled = true;
            btnConsulta.Enabled = true;
            CBO_SERIE.Enabled = true;
            btnSalida.Enabled = true;
            ////////label
            lblTitular.Text = "";
            lblDomicilio.Text = "";
            lblSupConsPriv.Text = "";
            lblValorConsPriv.Text = "";
            lblSupConsCom.Text = "";
            lblValConsCom.Text = "";
            lblSupTerrPriv.Text = "";
            lblValTerrPriv.Text = "";
            lblSupTerrComun.Text = "";
            lblValTerrCom.Text = "";
            lblValTotCons.Text = "";
            lblValTotTerr.Text = "";
            lblValor.Text = "";
            llenarCboSerie();
            lblLatitud.Text = "";
            lblLongitud.Text = "";
            lblNoAutorizado.Text = "";

            //LIMPIAR Y DESHABILITAR ESTA COSA
            btnMaps.Enabled = false;
            //btnBloquear.Visible = true;
            btnAutorizar.Enabled = false;
            //btnBloquear.Enabled = false;
            txtObservaciones.Text = "";
            //btnBloquear.Visible = false;
            pnlBusqueda.Enabled = false;
            DGVRESULTADO.DataSource = null; // Si estaba enlazado a un DataSource
            DGVRESULTADO.Rows.Clear();
            DGVRESULTADO.Columns.Clear();
            btnNoAutorizar.Enabled = false;
            gMapControl1.Visible = false;

            lblObsCar.Text = "";
            lblUbicacion.Text = "";
            pnlCambios.Visible = true;
            DesmarcarChecksRecursivo(pnlCambios);

            lblTerrenoTot.Text = "";
            lblConstTot.Text = "";
            btnBuscarClave.Enabled = true;
            lblComentario.Visible = true;
            pnlDatosPredio.Visible = true;
            pnlAlta.Visible = false;
            txtZona.Focus();
        }
        //se desmarcan los checks de los paneles de cambios
        private void DesmarcarChecksRecursivo(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (control is CheckBox)
                {
                    ((CheckBox)control).Checked = false;
                }
                else if (control.HasChildren)
                {
                    DesmarcarChecksRecursivo(control); // Llamada recursiva
                }
            }
        }


        //////////////////////////////////////////////////////////////////////////////////////////
        //// --- GENERAR UN MÉTODO CON UN SWITCH PARA CAMBIAR EL COLOR DE LAS CAJAS DE TEXTOS
        //////////////////////////////////////////////////////////////////////////////////////////
        void cajasamarillas(int ca)
        {
            switch (ca)
            {
                //cambiar a color amarillo las cajas de texto 
                case 0: txtZona.BackColor = Color.Yellow; break;
                case 1: txtManzana.BackColor = Color.Yellow; break;
                case 2: txtLote.BackColor = Color.Yellow; break;
                case 3: txtEdificio.BackColor = Color.Yellow; break;
                case 4: txtDepto.BackColor = Color.Yellow; break;
                case 5: TXT_FOLIO.BackColor = Color.Yellow; break;
                case 6: txtObservaciones.BackColor = Color.Yellow; break;

            }
        }
        void cajasblancas(int cb)
        {
            switch (cb)
            {
                //cambiar a color blanco las cajas de texto;
                case 0: txtZona.BackColor = Color.White; break;
                case 1: txtManzana.BackColor = Color.White; break;
                case 2: txtLote.BackColor = Color.White; break;
                case 3: txtEdificio.BackColor = Color.White; break;
                case 4: txtDepto.BackColor = Color.White; break;
                case 5: TXT_FOLIO.BackColor = Color.White; break;
                case 6: txtObservaciones.BackColor = Color.White; break;

            }
        }
        /////////////////////////////////////////////////////////////////////////////////
        //// ------- ASIGNAR A CADA UNA DE LAS CAJAS DE TEXTO SU COLOR
        /////////////////////////////////////////////////////////////////////////////////
        private void txtZona_Enter(object sender, EventArgs e)
        {
            cajasamarillas(0);
        }
        private void txtZona_Leave(object sender, EventArgs e)
        {
            cajasblancas(0);
        }
        private void txtManzana_Enter(object sender, EventArgs e)
        {
            cajasamarillas(1);
        }
        private void txtManzana_Leave(object sender, EventArgs e)
        {
            cajasblancas(1);
        }
        private void txtLote_Enter(object sender, EventArgs e)
        {
            cajasamarillas(2);
        }
        private void txtLote_Leave(object sender, EventArgs e)
        {
            cajasblancas(2);
        }
        private void txtEdificio_Enter(object sender, EventArgs e)
        {
            cajasamarillas(3);
        }
        private void txtEdificio_Leave(object sender, EventArgs e)
        {
            cajasblancas(3);
        }
        private void txtDepto_Enter(object sender, EventArgs e)
        {
            cajasamarillas(4);
        }
        private void txtDepto_Leave(object sender, EventArgs e)
        {
            cajasblancas(4);
        }
        private void txtInfoBloqueo_Enter(object sender, EventArgs e)
        {
            cajasamarillas(5);
        }
        private void txtInfoBloqueo_Leave(object sender, EventArgs e)
        {
            cajasblancas(5);
        }
        private void txtInfoDesbloqueo_Enter(object sender, EventArgs e)
        {
            cajasamarillas(6);
        }
        private void txtInfoDesbloqueo_Leave(object sender, EventArgs e)
        {
            cajasblancas(6);
        }
        ///////////////////////////////////////////////////////////////////
        //// --- PASAR DE UNA CAJA DE TEXTO A LA OTRA
        //////////////////////////////////////////////////////////////////
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
                TXT_FOLIO.Focus();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        ///// ----------- SOLO ACEPTAR NÚMEROS Y DARLE ENTER PARA GENERAR LA CONSULTA 
        ///////////////////////////////////////////////////////////////////////////////////////

        private void TXT_FOLIO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void TXT_FOLIO_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CONSULTA();
            }
        }

        private void TXT_FOLIO_Enter(object sender, EventArgs e)
        {
            cajasamarillas(5);
        }

        private void TXT_FOLIO_Leave(object sender, EventArgs e)
        {
            cajasblancas(5);
        }

        private void txtZona_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtManzana_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtLote_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtObservaciones_Leave(object sender, EventArgs e)
        {
            cajasblancas(6);
        }

        private void txtObservaciones_Enter(object sender, EventArgs e)
        {
            cajasamarillas(6);
        }

        private void PanelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int tramite = 0;
            string fecha_actual = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE AUTORIZAR ESTE FOLIO?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (resp == DialogResult.Yes)
            {
                if (txtObservaciones.Text == "")
                {
                    MessageBox.Show("FAVOR DE COLOCAR LAS OBSERVACIONES", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtObservaciones.Focus();
                    return;
                }

                try
                {
                    //dependiendo de la ubicacion se asigna el tramite ( como en el frankie)
                    if (ubicacion == 1)
                    {
                        tramite = 1;
                    }
                    else if (ubicacion == 2)
                    {
                        tramite = 3;
                    }
                    else if (ubicacion == 3)
                    {
                        tramite = 6;
                    }
                    else if (ubicacion == 5)
                    {
                        tramite = 7;
                    }
                    // se raliza el insert a la tabla CAT_NEW_VENTANILLA_2025
                    con.conectar_base_interno();
                    con.cadena_sql_interno = " ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO CAT_NEW_VENTANILLA_2025";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    UBICACION,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    TRAMITE,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    DESCRIPCION,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    FECHA,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    HORA,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    USUARIO,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    OBSERVACIONES,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + FOLIO + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.PEstado + " ," + MUNICIPIO + " ," + ZONA + " ," + MANZANA + " ," + LOTE + " ," + util.scm(EDIFICIO) + ",";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(DEPTO) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + ubicacion + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + tramite + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblUbicacion.Text.Trim()) + ",";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(fecha_actual) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(fecha_actual) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(Program.nombre_usuario) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(txtObservaciones.Text) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(SERIE);
                    con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                    con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";
                    //hacemos update a la tabla cat donde va 2025
                    con.cadena_sql_interno = con.cadena_sql_interno + "   UPDATE CAT_DONDE_VA_2025";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   SET VENTANILLA = 1 ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE FOLIO_ORIGEN = " + FOLIO;
                    con.cadena_sql_interno = con.cadena_sql_interno + "   AND SERIE =  " + util.scm(SERIE);

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
                //si tiene historial de autorizaciones se raliza el insert a la tabla SONG_CAT_NEW_VENTANILLA_AUTORIZA_H y se borra el registro de la tabla SONG_CAT_NEW_VENTANILLA_AUTORIZA
                //si no, el proceso no entra
                if (historial == 1)
                {
                    try
                    {
                        //realizamos el insert a la tabla SONG_CAT_NEW_VENTANILLA_AUTORIZA_H (historial de autorizaciones)
                        con.conectar_base_interno();
                        con.cadena_sql_interno = " ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO SONG_CAT_NEW_VENTANILLA_AUTORIZA_H";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    serie,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    UBICACION,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    DESCRIPCION,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    OBSERVACIONES,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    OBSERVACIONES_BORRADO,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    USUARIO";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(SERIE) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + FOLIO + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.PEstado + " ," + MUNICIPIO + " ," + ZONA + " ," + MANZANA + " ," + LOTE + " ," + util.scm(EDIFICIO) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(DEPTO) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + ubicacion + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblUbicacion.Text.Trim()) + ",";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblNoAutorizado.Text) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(txtObservaciones.Text) + " ,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(Program.nombre_usuario);
                        con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";

                        //hacemos delete a la tabla SONG_CAT_NEW_VENTANILLA_AUTORIZA
                        con.cadena_sql_interno = con.cadena_sql_interno + "   DELETE FROM SONG_CAT_NEW_VENTANILLA_AUTORIZA";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    Where SERIE =" + util.scm(SERIE);
                        con.cadena_sql_interno = con.cadena_sql_interno + "      AND FOLIO_ORIGEN = " + FOLIO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "      AND ESTADO = " + Program.PEstado;
                        con.cadena_sql_interno = con.cadena_sql_interno + "      AND MUNICIPIO = " + lblMun.Text;
                        con.cadena_sql_interno = con.cadena_sql_interno + "      AND ZONA = " + ZONA;
                        con.cadena_sql_interno = con.cadena_sql_interno + "      AND MANZANA = " + MANZANA;
                        con.cadena_sql_interno = con.cadena_sql_interno + "      AND LOTE = " + LOTE;
                        con.cadena_sql_interno = con.cadena_sql_interno + "      AND EDIFICIO = " + util.scm(EDIFICIO);
                        con.cadena_sql_interno = con.cadena_sql_interno + "      AND DEPTO = " + util.scm(DEPTO);

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
                MessageBox.Show("FOLIO AUTORIZADO CON ÉXITO", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiartodo();
            }
        }

        private void lblValor_Click(object sender, EventArgs e)
        {

        }
        private void panel53_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnNoAutorizar_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿ESTA SEGURO DE COLOCAR COMO PENDIENTE ESTE FOLIO?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (resp == DialogResult.Yes)
            {
                if (txtObservaciones.Text == "")
                {
                    MessageBox.Show("FAVOR DE COLOCAR LAS OBSERVACIONES DEL MOTIVO DE NO AUTORIZAR", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtObservaciones.Focus();
                    return;
                }
                try
                {
                    //se inserta en la tabla SONG_CAT_NEW_VENTANILLA_AUTORIZA el motivo de no autorizar el folio
                    con.conectar_base_interno();

                    con.cadena_sql_interno = " ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO SONG_CAT_NEW_VENTANILLA_AUTORIZA";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    serie,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Estado, Municipio, Zona, Manzana, Lote, Edificio, Depto,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    UBICACION,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    DESCRIPCION,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    OBSERVACIONES,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    USUARIO";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(SERIE) + ",";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + FOLIO + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.PEstado + " ," + MUNICIPIO + " ," + ZONA + " ," + MANZANA + " ," + LOTE + " ," + util.scm(EDIFICIO) + ",";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(DEPTO) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + ubicacion + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(lblUbicacion.Text.Trim()) + ",";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(txtObservaciones.Text) + " ,";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(Program.nombre_usuario);
                    con.cadena_sql_interno = con.cadena_sql_interno + "    )";

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

                MessageBox.Show("EL FOLIO NO FUE AUTORIZADO", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limpiartodo();
            }

        }

        private void btnBuscarClave_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBuscarClave, "FOLIOS SIN AUTORIZAR");
        }

        private void DGVRESULTADO_DoubleClick(object sender, EventArgs e)
        {
            // Al hacer doble clic en una fila del DataGridView, se llenan los campos correspondientes Y REALIZA LA CONSULTA
            LIMPIAR_2();
            string SERIED;
            //int FOLIO;
            if (DGVRESULTADO.CurrentRow.Cells[0].Value.ToString() == "")
            {
                MessageBox.Show("SELECCIONE UN DATO CORRECTO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // Sale del método o procedimiento
            }

            SERIED = Convert.ToString(DGVRESULTADO.CurrentRow.Cells[0].Value).Trim();
            // FOLIO = Convert.ToInt32(DGVRESULTADO.CurrentRow.Cells[1].Value);

            foreach (var item in CBO_SERIE.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(SERIED))
                {
                    // Mostrar el valor completo del ComboBox
                    //DGVRESULTADO.CurrentRow.Cells[0].Value = itemStr;
                    CBO_SERIE.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }
            // Asignar los valores de las celdas a los TextBox correspondientes
            txtZona.Text = DGVRESULTADO.CurrentRow.Cells[3].Value.ToString().Trim().PadLeft(2, '0');
            txtManzana.Text = DGVRESULTADO.CurrentRow.Cells[4].Value.ToString().Trim().PadLeft(3, '0');
            txtLote.Text = DGVRESULTADO.CurrentRow.Cells[5].Value.ToString().Trim().PadLeft(2, '0');
            txtEdificio.Text = DGVRESULTADO.CurrentRow.Cells[6].Value.ToString().Trim().PadLeft(2, '0');
            txtDepto.Text = DGVRESULTADO.CurrentRow.Cells[7].Value.ToString().Trim().PadLeft(4, '0');
            TXT_FOLIO.Text = DGVRESULTADO.CurrentRow.Cells[1].Value.ToString().Trim();
            //REALIZA LA CONSULTA CON LOS DATOS OBTENIDOS DEL DATAGRIDVIEW
            CONSULTA();
        }

        private void pnlDatosPredio_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMapsA_Click(object sender, EventArgs e)
        {
            // Abre Google Maps con la latitud y longitud ingresadas en los labels
            if (string.IsNullOrWhiteSpace(lblLatitud.Text) || string.IsNullOrWhiteSpace(lblLongitud.Text))
            {
                MessageBox.Show("POR FAVOR, INGRESE LA LATITUD Y LONGITUD ANTES DE ABRIR GOOGLE MAPS.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string latitud = lblLatitud.Text.Trim();
            string longitud = lblLongitud.Text.Trim();
            Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
        }

        private void btnConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnBuscarClave_Click(object sender, EventArgs e)
        {
            LIMPIAR_2();
            pnlBusqueda.Enabled = true;
            try
            {
                //RELIZA LA BUSQUEDA DE LOS FOLIOS SIN AUTORIZAR Y LOS MUESTRA EN EL DATAGRIDVIEW
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT RTRIM(CNC.SERIE), RTRIM(CNC.FOLIO_ORIGEN), RTRIM(CNC.MUNICIPIO), RTRIM(CNC.ZONA), RTRIM(CNC.MANZANA), RTRIM(CNC.LOTE),";
                con.cadena_sql_interno = con.cadena_sql_interno + "        RTRIM(CNC.EDIFICIO), RTRIM(CNC.DEPTO), RTRIM(CNC.DESCRIPCION), CNC.FECHA, RTRIM(CNC.OBSERVACIONES), RTRIM(CNC.USUARIO)  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_DONDE_VA_2025 CDV, CAT_NEW_CARTOGRAFIA_2025 CNC ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CDV.VENTANILLA = 0";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.SERIE = CNC.SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.FOLIO_ORIGEN = CNC.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY CNC.SERIE, CNC.FOLIO_ORIGEN DESC";

                DataTable LLENAR_GRID_1 = new DataTable();
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
                {
                    MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else //en caso de encontrar un dato, se realiza toda la acción de abajo 
                {
                    DGVRESULTADO.DataSource = LLENAR_GRID_1;
                    DGVRESULTADO.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    DGVRESULTADO.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
                    DGVRESULTADO.Columns[2].HeaderText = "MUN";       // 
                    DGVRESULTADO.Columns[3].HeaderText = "ZONA";                        // 
                    DGVRESULTADO.Columns[4].HeaderText = "MZA";               //
                    DGVRESULTADO.Columns[5].HeaderText = "LOTE";                  //
                    DGVRESULTADO.Columns[6].HeaderText = "EDIF";             //
                    DGVRESULTADO.Columns[7].HeaderText = "DEPTO";
                    DGVRESULTADO.Columns[8].HeaderText = "TRAMITE";
                    DGVRESULTADO.Columns[9].HeaderText = "FECHA";
                    DGVRESULTADO.Columns[10].HeaderText = "OBSERVACIONES";
                    DGVRESULTADO.Columns[11].HeaderText = "USUARIO";

                    DGVRESULTADO.Columns[0].Width = 50; // Ajusta el ancho de la columna SERIE
                    DGVRESULTADO.Columns[1].Width = 50; // Ajusta el ancho de la columna FOLIO
                    DGVRESULTADO.Columns[2].Width = 50; // Ajusta el ancho de la columna MUNICIPIO
                    DGVRESULTADO.Columns[3].Width = 50; // Ajusta el ancho de la columna ZONA
                    DGVRESULTADO.Columns[4].Width = 50; // Ajusta el ancho de la columna MANZANA
                    DGVRESULTADO.Columns[5].Width = 50; // Ajusta el ancho de la columna LOTE
                    DGVRESULTADO.Columns[6].Width = 50; // Ajusta el ancho de la columna EDIFICIO
                    DGVRESULTADO.Columns[7].Width = 50; // Ajusta el ancho de la columna DEPTO
                    DGVRESULTADO.Columns[8].Width = 150; // Ajusta el ancho de la columna UBICACION
                    DGVRESULTADO.Columns[9].Width = 150; // Ajusta el ancho de la columna FECHA
                    DGVRESULTADO.Columns[10].Width = 300; // Ajusta el ancho de la columna OBSERVACIONES
                    DGVRESULTADO.Columns[11].Width = 200; // Ajusta el ancho de la columna USUARIO

                    DGVRESULTADO.Enabled = true;
                    btnNoAutorizar.Enabled = false;
                    btnAutorizar.Enabled = false;

                    con.cerrar_interno(); //Cerramos la conexión después de llenar el DataTable
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
        }

        //////////////////////////////////////////////////////////////////////////
        //// ---------- MOSTRAR TEXTO AL PASAR EL BOTÓN (TOOLTIP)
        ///////////////////////////////////////////////////////////////////////////

        private void btnBuscarClave_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBuscarClave, "BÚSQUEDA DE CLAVE CATASTRAL");
        }
        private void btnCancela_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCancela, "CANCELAR");
        }
        private void btnSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnSalida, "SALIR");
        }

        private void btnMaps_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnMaps, "ABRIR GOOGLE MAPS PARA VER LAS COORDENADAS");
        }
        /////////////////////////////////////////////////////////////////////////
        //// ------------------------------- BOTONES 
        /////////////////////////////////////////////////////////////////////////
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            CONSULTA();
        }

        private void CONSULTA()
        {
            int verificar = 0;

            if (txtZona.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN LA ZONA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtZona.Focus(); return; }
            if (txtManzana.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN LA MANZANA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtManzana.Focus(); return; }
            if (txtLote.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN EL LOTE", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtLote.Focus(); return; }

            if (txtEdificio.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BÚSUQEDA SIN EL EDIFICIO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("NECESITAS COLOCAR DOS CARACTERES EN EL EDIFICIO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEdificio.Focus(); return; }

            if (txtDepto.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN EL DEPTO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("NECESITAS COLOCAR CUATRO CARACTERES EN EL DEPARTAMENTO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDepto.Focus(); return; ; }

            MUNICIPIO = Convert.ToInt32(lblMun.Text.Trim());
            ZONA = Convert.ToInt32(txtZona.Text.Trim());
            MANZANA = Convert.ToInt32(txtManzana.Text.Trim());
            LOTE = Convert.ToInt32(txtLote.Text.Trim());
            EDIFICIO = txtEdificio.Text.Trim();
            DEPTO = txtDepto.Text.Trim();
            SERIE = CBO_SERIE.Text.Trim();
            FOLIO = TXT_FOLIO.Text.Trim();

            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT ZONA";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO) + ")";
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
                    MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA POR CATASTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO) + ")";
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
                //VErifica que el folio exista en la tabla CAT_NEW_CARTOGRAFIA_2025 ( PROCESO DE CARTOGRAFIA )
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM CAT_NEW_CARTOGRAFIA_2025  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where SERIE =" + util.scm(SERIE);
                con.cadena_sql_interno = con.cadena_sql_interno + " AND FOLIO_ORIGEN = " + FOLIO;
                con.cadena_sql_interno = con.cadena_sql_interno + " AND ESTADO = " + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + " AND MUNICIPIO = " + lblMun.Text;
                con.cadena_sql_interno = con.cadena_sql_interno + " AND ZONA = " + ZONA;
                con.cadena_sql_interno = con.cadena_sql_interno + " AND MANZANA = " + MANZANA;
                con.cadena_sql_interno = con.cadena_sql_interno + " AND LOTE = " + LOTE;
                con.cadena_sql_interno = con.cadena_sql_interno + " AND EDIFICIO = " + util.scm(EDIFICIO);
                con.cadena_sql_interno = con.cadena_sql_interno + " AND DEPTO = " + util.scm(DEPTO);
                con.cadena_sql_interno = con.cadena_sql_interno + " )";
                con.cadena_sql_interno = con.cadena_sql_interno + " BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT existe = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + " End";
                con.cadena_sql_interno = con.cadena_sql_interno + " Else";
                con.cadena_sql_interno = con.cadena_sql_interno + " BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT existe = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + " End";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    var existe = con.leer_interno[0].ToString();
                    verificar = Convert.ToInt32(existe);
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
            if (verificar == 2)
            {
                MessageBox.Show("EL FOLIO NO EXISTENTE, VERIFIQUE LOS DATOS", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtZona.Focus();
                return;
            }
            try
            {
                //VERIFICA QUE EL FOLIO NO ESTE AUTORIZADO POR VENTANILLA EN LA TABLA CAT_DONDE_VA_2025
                verificar = 0;
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = "                          IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM CAT_DONDE_VA_2025  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where SERIE =" + util.scm(SERIE);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND FOLIO_ORIGEN = " + FOLIO;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND VENTANILLA = 1 ";
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
                    var existe = con.leer_interno[0].ToString();
                    verificar = Convert.ToInt32(existe);
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
            if (verificar == 1)
            {
                MessageBox.Show("NO SE PUEDE AUTORIZAR UN FOLIO YA AUTORIZADO", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                limpiartodo();
                //txtZona.Focus();
                return;
            }

            try
            {
                //VERIFICA SI EL FOLIO TIENE HISTORIAL DE NO AUTORIZACION EN LA TABLA SONG_CAT_NEW_VENTANILLA_AUTORIZA
                string resultado = "";
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = "                          IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM SONG_CAT_NEW_VENTANILLA_AUTORIZA  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where SERIE =" + util.scm(SERIE);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND FOLIO_ORIGEN = " + FOLIO;
                con.cadena_sql_interno = con.cadena_sql_interno + "             )";
                con.cadena_sql_interno = con.cadena_sql_interno + "                 BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "                     SELECT OBSERVACIONES";
                con.cadena_sql_interno = con.cadena_sql_interno + "                       FROM SONG_CAT_NEW_VENTANILLA_AUTORIZA  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "                      Where SERIE =" + util.scm(SERIE);
                con.cadena_sql_interno = con.cadena_sql_interno + "                        AND FOLIO_ORIGEN = " + FOLIO;
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
                    resultado = con.leer_interno[0].ToString();

                }

                con.cerrar_interno();

                if (resultado == "2")
                {
                    lblNoAutorizado.Text = "";
                    historial = 0; // No hay historial de autorizaciones
                }
                else
                {
                    lblNoAutorizado.Text = resultado;
                    historial = 1; // Hay historial de autorizaciones
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
                //REALIZA LA CONSULTA A LA TABLA CAT_NEW_CARTOGRAFIA_2025 PARA OBTENER LOS DATOS DEL FOLIO DEL PROCESO A REALIZAR
                con.conectar_base_interno();
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = "                           SELECT observaciones, ubicacion, descripcion";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM CAT_NEW_CARTOGRAFIA_2025  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where SERIE =" + util.scm(SERIE);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND FOLIO_ORIGEN = " + FOLIO;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ESTADO = " + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + lblMun.Text;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO);

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    lblObsCar.Text = con.leer_interno[0].ToString().Trim();
                    ubicacion = Convert.ToInt32(con.leer_interno[1].ToString().Trim());
                    lblUbicacion.Text = con.leer_interno[2].ToString().Trim();
                }
                con.cerrar_interno();

                //UBICACION 1 ALTA, MUESTRA LOS DATOS DEL ALTA
                if (ubicacion == 1)
                {
                    pnlAltaInfo.Visible = true;
                    pnlCambios.Visible = false;
                    pnlDatosPredio.Visible = false;
                    pnlCertificado.Visible = false;
                    pnlAlta.Visible = true;
                }
                //UBICACION 2 CAMBIOS, MUESTRA LOS DATOS DE CAMBIOS
                else if (ubicacion == 2)
                {
                    int longitud = lblUbicacion.Text.Length;
                    pnlCambios.Visible = true;
                    pnlCambios.Enabled = false;
                    pnlAltaInfo.Visible = false;
                    pnlCertificado.Visible = false;
                    pnlDatosPredio.Visible = true;
                    pnlAlta.Visible = false;
                    string cambios = "", nombre, superficie, construccion, factor_const, factor_terreno;
                    cambios = lblUbicacion.Text.Substring(0, 5);
                    nombre = cambios.Substring(0, 1);
                    superficie = cambios.Substring(1, 1);
                    construccion = cambios.Substring(2, 1);
                    factor_const = cambios.Substring(3, 1);
                    factor_terreno = cambios.Substring(4, 1);
                    lblUbicacion.Text = lblUbicacion.Text.Substring(5, (longitud - 5));

                    if (nombre == "1")
                    {
                        ckbCambioNombre.Checked = true;
                    }
                    if (superficie == "1")
                    {
                        ckbCambioSuperficie.Checked = true;
                    }
                    if (construccion == "1")
                    {
                        ckbCambioConstruccion.Checked = true;
                    }
                    if (factor_const == "1")
                    {
                        ckbCambioFactoresCons.Checked = true;
                    }
                    if (factor_terreno == "1")
                    {
                        ckbCambioFactoresTerr.Checked = true;
                    }

                }
                //UBICACION 3 Y 5 CERTIFICADO, MUESTRA LOS DATOS DEL CERTIFICADO
                else if (ubicacion == 3 || ubicacion == 5)
                {
                    pnlCertificado.Visible = true;
                    pnlAltaInfo.Visible = false;
                    pnlCambios.Visible = false;
                    pnlDatosPredio.Visible = true;
                    pnlAlta.Visible = false;
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
            if (ubicacion == 1)//operacion alta
            {
                string posicion = "";
                try
                {
                    //SE RALIZA LA BUSQUEDA DE LOS DATOS DEL PREDIO PARA LA UBICACION 1 (ALTA) 
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + " SELECT cn.ZON_ORIGEN, cn.COD_CALLE, cl.NomCalle, rg.Descr, cn.TERR_PROPIO, cn.TERR_COMUN";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     ,  cn.TOPOGRAFIA, cn.IRREGULARIDAD , cn.FRENTE, cn.FONDO , cn.posicion";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025 cn, CALLES cl , REGIMEN rg";
                    con.cadena_sql_interno = con.cadena_sql_interno + "             Where cn.SERIE =" + util.scm(SERIE);
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.FOLIO_ORIGEN = " + FOLIO;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.ESTADO = " + Program.PEstado;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.MUNICIPIO = " + lblMun.Text;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.ZONA = " + ZONA;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.MANZANA = " + MANZANA;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.LOTE = " + LOTE;
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.EDIFICIO = " + util.scm(EDIFICIO);
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.DEPTO = " + util.scm(DEPTO);
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.ZON_ORIGEN = cl.ZonaOrig  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.COD_CALLE = cl.CodCalle  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "               AND cn.REGIMEN = rg.RegProp  ";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        if (con.leer_interno[0].ToString().Trim() != "")
                        {
                            lblZonaA.Text = con.leer_interno[0].ToString().Trim();
                            lblCodCalle.Text = con.leer_interno[1].ToString().Trim();
                            lblCalleA.Text = con.leer_interno[2].ToString().Trim();
                            lblRegimenA.Text = con.leer_interno[3].ToString().Trim();
                            TERRENO1 = Convert.ToDouble(con.leer_interno[4].ToString().Trim()); //SUPERFICIE TERRENO PROPIO
                            TERRENO2 = Convert.ToDouble(con.leer_interno[5].ToString().Trim()); //SUPERFICIE TERRENO COMUN
                            TERRENO3 = TERRENO1 + TERRENO2; //SUPERFICIE TOTAL TERRENO   
                            lblSupTerrenoA.Text = TERRENO1.ToString("N2"); //SUPERFICIE TERRENO PROPIO
                            lblSupTerrCA.Text = TERRENO2.ToString("N2"); //SUPERFICIE TERRENO COMUN
                            lbldesA.Text = con.leer_interno[6].ToString().Trim(); //TOPOGRAFIA (desnivel)
                            lblAreaA.Text = con.leer_interno[7].ToString().Trim(); //IRREGULARIDAD (area inscripta)
                            lblFrenteA.Text = con.leer_interno[8].ToString().Trim(); //FRENTE
                            lblFondoA.Text = con.leer_interno[9].ToString().Trim(); //FONDO
                            posicion = con.leer_interno[10].ToString().Trim(); //POSICION (ubicación del predio)
                        }

                    }
                    foreach (var item in cboUbicacion.Items)
                    {
                        string itemStr = item.ToString();
                        if (itemStr.StartsWith(posicion))
                        {
                            // Mostrar el valor completo del ComboBox
                            cboUbicacion.SelectedItem = item;
                            break; // Salir del bucle al encontrar la primera coincidencia
                        }
                    }
                    con.cerrar_interno();
                    pnlDatosPredio.Visible = false;
                    pnlAlta.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error al sumar la construccion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    return; // Retornar false si ocurre un error
                }
            }
            else if (ubicacion == 2 || ubicacion == 3 || ubicacion == 5) // SE HACE LA CONSULTA EN LAS TABLAS DE PROPIEDADES Y PREDIOS PARA OBTENER LOS DATOS DEL PREDIO
            {
                try
                {
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    /////// OBTENEMOS DATOS DEL FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT  P.PmnProp, P.DomFis, P.STerrProp, P.STerrCom, P.VTerrProp, P.VTerrCom, C.NomCol";
                    con.cadena_sql_interno = con.cadena_sql_interno + "          ,CL.NomCalle, US.DescrUso";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    FROM PROPIEDADES P, PREDIOS PR, COLONIAS C, CALLES CL, USO_SUELO US";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE P.Municipio = " + MUNICIPIO;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Zona = " + ZONA;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Manzana = " + MANZANA;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Lote =" + LOTE;
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Edificio = " + util.scm(EDIFICIO);
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Depto = " + util.scm(DEPTO);
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Municipio = PR.Municipio";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Zona = PR.Zona";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Manzana = PR.Manzana";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND P.Lote = PR.Lote";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND C.Colonia = PR.Colonia";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND CL.ZonaOrig = PR.ZonaOrig";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND CL.CodCalle = PR.CodCalle";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     AND US.Uso = P.Uso";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        if (con.leer_interno[0].ToString().Trim() != "")
                        {
                            pnlDatosPredio.Visible = true;
                            lblTitular.Text = con.leer_interno[0].ToString().Trim();
                            lblDomicilio.Text = con.leer_interno[1].ToString().Trim();

                            TERRENO1 = Convert.ToDouble(con.leer_interno[2].ToString().Trim()); //SUPERFICIE TERRENO PROPIO
                            TERRENO2 = Convert.ToDouble(con.leer_interno[3].ToString().Trim()); //SUPERFICIE TERRENO COMUN
                            TERRENO3 = Convert.ToDouble(con.leer_interno[4].ToString().Trim()); //VALOR TERRENO PROPIO
                            TERRENO4 = Convert.ToDouble(con.leer_interno[5].ToString().Trim()); //VALOR TERRENO COMUN
                            TERRENO5 = TERRENO4 + TERRENO3; //TOTAL VALOR TERRENO PROPIO + COMUN
                            terreno6 = TERRENO1 + TERRENO2; //TOTAL SUPERFICIE TERRENO PROPIO + COMUN
                            lblTerrenoTot.Text = terreno6.ToString("N2");
                            lblSupTerrPriv.Text = TERRENO1.ToString("N2");
                            lblSupTerrComun.Text = TERRENO2.ToString("N2");
                            lblValTerrPriv.Text = TERRENO3.ToString("N2");
                            lblValTerrCom.Text = TERRENO4.ToString("N2");
                            lblValTotTerr.Text = TERRENO5.ToString("N2");
                            
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

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////  INGRESAMOS LA CONSTRUCCION SI ES QUE TIENE PRIVADA O COMUN
            /////////////////////////////////////////////////////////////  SOLO REVASAMOS SI HAY CONSTRUCCION PRIVADA
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //CONSTRUCCION1 = SUPERFICIE DE LA CONSTRUCCION PRIVADA
            //CONSTRUCCION2 = SUPERFICIE DE LA CONSTRUCCION COMUN
            //CONSTRUCCION3 = SUPERFICIE TOTAL DE LA CONSTRUCCION PRIVADA + COMUN
            //CONSTRUCCION4 = VALOR DE LA CONSTRUCCION PRIVADA (ETIQUETA)
            //CONSTRUCCION5 = VALOR DE LA CONSTRUCCION COMUN (ETIQUETA)
            //CONSTRUCCION6 = VALOR TOTAL DE LA CONSTRUCCION PRIVADA + COMUN (ETIQUETA)

            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons), SUM(ValorCons)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Where Zona     = " + ZONA;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana  = " + MANZANA;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote     = " + LOTE;
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio = '" + EDIFICIO + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto    = '" + DEPTO + "'";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        if (ubicacion == 1)//ALTA
                        {
                            lblSupConsA.Text = "0";

                        }
                        else //CAMBIOS Y CERTIFICADO
                        {
                            lblSupConsPriv.Text = "0";
                            lblValorConsPriv.Text = "0";
                            CONSTRUCCION1 = 0;
                        }

                    }
                    else
                    {
                        if (ubicacion == 1)//ALTA
                        {
                            lblSupConsA.Text = con.leer_interno[0].ToString().Trim();
                        }
                        else //CAMBIOS Y CERTIFICADO
                        {
                            lblSupConsPriv.Text = con.leer_interno[0].ToString().Trim();
                            CONSTRUCCION4 = Convert.ToDouble(con.leer_interno[1].ToString().Trim());
                            lblValorConsPriv.Text = CONSTRUCCION4.ToString("N2");
                            CONSTRUCCION1 = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
                        }
                    }
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

                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons), SUM(ValorCons)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "             Where Zona     = " + ZONA;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana  = " + MANZANA;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Lote     = " + LOTE;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Edificio = ''";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Depto    = ''";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "")
                    {
                        if (ubicacion == 1) //ALTA
                        {
                            lblSupConsCA.Text = "0";
                        }
                        else //CAMBIOS Y CERTIFICADO
                        {
                            lblSupConsCom.Text = "0";
                            lblValConsCom.Text = "0";
                        }

                    }
                    else
                    {
                        if (ubicacion == 1) //ALTA
                        {
                            lblSupConsCA.Text = con.leer_interno[0].ToString().Trim();
                        }
                        else //CAMBIOS Y CERTIFICADO
                        {
                            lblSupConsCom.Text = con.leer_interno[0].ToString().Trim();
                            CONSTRUCCION5 = Convert.ToDouble(con.leer_interno[1].ToString().Trim());
                            lblValConsCom.Text = CONSTRUCCION5.ToString("N2");
                            CONSTRUCCION2 = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
                        }
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
            if (ubicacion != 1) // SUMA LOS VALORES DE LA CONSTRUCCION PRIVADA Y COMUN PARA OBTENER EL VALOR CATASTRAL SI NO ES ALTA
            {
                VALORCATASTRAL = TERRENO5 + Convert.ToDouble(lblValorConsPriv.Text) + Convert.ToDouble(lblValConsCom.Text);
                lblValor.Text = VALORCATASTRAL.ToString("N2");
                CONSTRUCCION6 = CONSTRUCCION4 + CONSTRUCCION5;
                
                lblValTotCons.Text = CONSTRUCCION6.ToString("N2");
                CONSTRUCCION3 = CONSTRUCCION1 + CONSTRUCCION2;
                lblConstTot.Text = CONSTRUCCION3.ToString("N2");
            }
            try
            {
                ///OBTENER LA GEOLOCALIZACIÓN
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT TOP 1 LATITUD, LONGITUD";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_GEOLOCALIZACION";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana   = " + Convert.ToInt32(txtManzana.Text.Trim());  //Se cocatena la manzana que se mande 
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
                        lblLatitud.Text = con.leer_interno[0].ToString().Trim();
                        lblLongitud.Text = con.leer_interno[1].ToString().Trim();
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
            if (lblLatitud.Text != "" || lblLongitud.Text != "")
            {
                latitud = Convert.ToDouble(lblLatitud.Text.Trim());
                longitud = Convert.ToDouble(lblLongitud.Text.Trim());
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

            btnAutorizar.Enabled = false;
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            CBO_SERIE.Enabled = false;
            TXT_FOLIO.Enabled = false;
            btnConsulta.Enabled = false;
            btnAutorizar.Enabled = true;
            btnNoAutorizar.Enabled = true;
            txtObservaciones.Focus();
        }
        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaps_Click(object sender, EventArgs e)
        {
            //SE MANDA A ABRIR GOOGLE MAPS CON LA LATITUD Y LONGITUD DEL PREDIO
            if (string.IsNullOrWhiteSpace(lblLatitud.Text) || string.IsNullOrWhiteSpace(lblLongitud.Text))
            {
                MessageBox.Show("POR FAVOR, INGRESE LA LATITUD Y LONGITUD ANTES DE ABRIR GOOGLE MAPS.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string latitud = lblLatitud.Text.Trim();
            string longitud = lblLongitud.Text.Trim();
            Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
        }
        private void btnCancela_Click(object sender, EventArgs e)
        {
            limpiartodo();
        }
    }
}
