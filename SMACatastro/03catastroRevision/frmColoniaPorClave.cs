using AccesoBase;
using GMap.NET.MapProviders;
using Microsoft.Office.Interop.Excel;
using SMACatastro.catastroCartografia;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Imaging;
using System.IO;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using Utilerias;
//using static Telerik.WinControls.NativeMethods;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace SMACatastro.catastroRevision
{
    public partial class frmColoniaPorClave : Form
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// DECLARACIÓN DE VARIABLES PARA EMPEZAR LA PANTALLA 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        double mlatitud, mlongitud = 0.0;
        int validacionCambio, ZONA, MANZANA, LOTE, COLONIA, TIPOCAMBIO = 0;
        Util util = new Util();
        public frmColoniaPorClave()
        {
            InitializeComponent();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// DECLARACIÓN DE VARIABLES PARA EMPEZAR LA PANTALLA 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// LO QUE HARÁ EL FORMULARIO AL CARGAR / ABRIR  
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void frmColoniaPorClave_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "USUARIO: " + Program.nombre_usuario.ToString();
            limpiartodo();
            btnNuevo.Focus();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// AL ARRASTRAR EL PANEL DEL TÍTULO PERMITE MOVER 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// TIMER PARA COLOCAR LA FECHA Y HORA EN EL FORMULARIO  
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// CÓDIGO PARA MINIMIZAR LA PANTALLA 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// BOTÓN DE LIMPIAR TODA LA PANTALLA Y HABILITAR EL BOTÓN DE NUEVO  
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancela_Click(object sender, EventArgs e)
        {
            limpiartodo();
            btnNuevo.Enabled = true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// CÓDIGO PARA CERRAR LA PANTALLA 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// MÉTODO PARA PONER DE COLOR AMARILLO LAS CAJAS / ELEMENTOS QUE SE SOLICITEN 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        void CajasAmarillas(int ca)
        {
            switch (ca)
            {
                case 0: txtZona.BackColor = Color.Yellow; break;
                case 1: txtManzana.BackColor = Color.Yellow; break;
                case 2: txtLote.BackColor = Color.Yellow; break;
                case 3: txtEdificio.BackColor = Color.Yellow; break;
                case 4: txtDepto.BackColor = Color.Yellow; break;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// MÉTODO PARA PONER DE COLOR BLANCO LAS CAJAS / ELEMENTOS QUE SE SOLICITEN 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        void CajasBlancas(int cb)
        {
            switch (cb)
            {
                case 0: txtZona.BackColor = Color.White; break;
                case 1: txtManzana.BackColor = Color.White; break;
                case 2: txtLote.BackColor = Color.White; break;
                case 3: txtEdificio.BackColor = Color.White; break;
                case 4: txtDepto.BackColor = Color.White; break;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// MÉTODO DE LIMPIAR TODOS LOS ELEMENTOS DE LA PANTALLA QUE CONTENGAN INFORMACIÓN
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        void limpiartodo()
        {
            txtZona.Text = "";
            txtManzana.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";
            btnNuevo.Enabled = true;

            lblColonia.Text = "";

            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            dgResultado.Enabled = false;
            deshabilitarClaveCatastral();

            btnMaps.Visible = false;
            gMapControl1.Visible = false;

            lblColoniaOrigen.Text = "";
            lblColoniaDestino.Text = "";

            lblConteoLotes.Text = "0";
            btnCancelarColoniasAbajo.Enabled = false;
            btnNuevo.Focus();
            btnCambioManzana.Enabled = false;
            btnCambioLote.Enabled = false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// AL DAR CLICK EN NUEVO, INVOCAR EL MÉTODO QUE SOLO HABILITA LAS CAJAS DE TEXTO 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        void habilitarClaveCatastral()
        {
            txtZona.Enabled = true;
            txtManzana.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;
            btnConsulta.Enabled = true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////  AL DAR CLICK EN NUEVO, INVOCAR EL MÉTODO QUE SOLO DESHABILITA LAS CAJAS DE TEXTO 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        void deshabilitarClaveCatastral()
        {
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            btnConsulta.Enabled = false;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////  BOTÓN NUEVO HABILITA CLAVE / HABILITA ZONA / BTN NUEVO 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Enabled = false;
            btnBuscarClave.Enabled = true;
            habilitarClaveCatastral();
            txtZona.Focus();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////  PROPIEDADES ENTER PARA CADA CAJA DE TEXTO (SE COLOCA EN AMARILLO)
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtZona_Enter(object sender, EventArgs e)
        {
            CajasAmarillas(0);
        }
        private void txtManzana_Enter(object sender, EventArgs e)
        {
            CajasAmarillas(1);
        }
        private void txtLote_Enter(object sender, EventArgs e)
        {
            CajasAmarillas(2);
        }
        private void txtEdificio_Enter(object sender, EventArgs e)
        {
            CajasAmarillas(3);
        }
        private void txtDepto_Enter(object sender, EventArgs e)
        {
            CajasAmarillas(4);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////  PROPIEDADES ENTER PARA CADA CAJA DE TEXTO (SE COLOCA EN BLANCO)
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtZona_Leave(object sender, EventArgs e)
        {
            CajasBlancas(0);
        }
        private void txtManzana_Leave(object sender, EventArgs e)
        {
            CajasBlancas(1);
        }
        private void txtLote_Leave(object sender, EventArgs e)
        {
            CajasBlancas(2);
        }
        private void txtEdificio_Leave(object sender, EventArgs e)
        {
            CajasBlancas(3);
        }
        private void txtDepto_Leave(object sender, EventArgs e)
        {
            CajasBlancas(4);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////REALIZAR VALIDACIONES , CONSULTA DE LA CLAVE CATASTRAL (BUSCAR)
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        void Consulta()
        {
            if (txtZona.Text == "") { MessageBox.Show("NECESITAS COLOCAR UNA ZONA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtZona.Focus(); return; }
            if (txtManzana.Text == "") { MessageBox.Show("NECESITAS COLOCAR UNA MANZANA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtManzana.Focus(); return; }
            if (txtManzana.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtManzana.Focus(); return; }
            if (txtLote.Text == "") { MessageBox.Show("NECESITAS COLOCAR UN LOTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtLote.Focus(); return; }
            if (txtEdificio.Text == "") { MessageBox.Show("NECESITAS COLOCAR UN EDIFICIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("EL EDIFICIO NO PUEDE TENER MENOS DE 2 CARACTERES", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information); txtEdificio.Focus(); return; }
            if (txtDepto.Text == "") { MessageBox.Show("NECESITAS COLOCAR UN DEPTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("EL DEPTO NO PUEDE TENER MENOS DE 4 CARACTERES", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information); txtDepto.Focus(); return; }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////// OBTENER LAS COORDENADAS / LATITUD / LONGITUD DE LA CLAVE CATASTRAL         
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT LATITUD, LONGITUD";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_GEOLOCALIZACION";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE ESTADO    = " + Program.PEstado;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND MUNICIPIO = " + Program.municipioN;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana   = " + Convert.ToInt32(txtManzana.Text.Trim());  //Se cocatena la manzana que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Lote      = " + Convert.ToInt32(txtLote.Text.Trim());  //Se cocatena el lote que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO     = '" + txtDepto.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO  = '" + txtEdificio.Text.Trim() + "'";
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            while (con.leer_interno.Read())
            {
                if (con.leer_interno[0].ToString().Trim() != "")
                {
                    mlatitud = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
                    mlongitud = Convert.ToDouble(con.leer_interno[1].ToString().Trim());
                }
            }
            ///CERRAR CONEXIÓN DE COORDENADAS
            con.cerrar_interno();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////LATITUD / LONGITUD DIFERENTE A 0 (ES DECIR, SI TIENE COORDENADAS)
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (mlatitud != 0 && mlongitud != 0)
            {
                gMapControl1.Visible = true;
                gMapControl1.DragButton = MouseButtons.Left;
                gMapControl1.CanDragMap = true;
                gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
                gMapControl1.Position = new GMap.NET.PointLatLng(mlatitud, mlongitud);
                gMapControl1.MinZoom = 1;
                gMapControl1.MaxZoom = 24;
                gMapControl1.Zoom = 19;
                gMapControl1.AutoScroll = true;
                gMapControl1.Enabled = true;
                btnMaps.Visible = true;
                btnMaps.Enabled = true;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////NO TIENE COORDENADAS; MANDAR MENSAJE 
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            else
            {
                btnMaps.Visible = false;
                gMapControl1.Visible = false;
                MessageBox.Show("CLAVE CATASTRAL NO CUENTA CON COORDENADAS; RECOMENDAMOS PASAR AL ÁREA DE CARTOGRAFÍA PARA QUE LA COLOQUEN", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////LLENAR INFORMACIÓN DE COLONIAS
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT C.COLONIA, C.NOMCOL";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM PREDIOS P, COLONIAS C";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE P.ESTADO    = " + Program.PEstado;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND P.MUNICIPIO = " + Program.municipioN;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND P.Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND P.Manzana   = " + Convert.ToInt32(txtManzana.Text.Trim());  //Se cocatena la manzana que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND P.Lote      = " + Convert.ToInt32(txtLote.Text.Trim());  //Se cocatena la manzana que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND P.ESTADO    = C.ESTADO";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND P.MUNICIPIO = C.MUNICIPIO";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND P.COLONIA   = C.COLONIA";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            while (con.leer_interno.Read())
            {
                if (con.leer_interno[0].ToString().Trim() != "")
                {
                    lblColonia.Text = con.leer_interno[0].ToString().Trim() + " - " + con.leer_interno[1].ToString().Trim(); //SOLO ESO PARA COLOCAR 
                }
            }
            //CERRAR LA CONEXIÓN 
            con.cerrar_interno();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////  LLENAR DATAGRIDVIEW CON LOS RESULTADOS DE LA COLUMNA 
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT C.COLONIA, C.NOMCOL ";
            con.cadena_sql_interno = con.cadena_sql_interno + "     FROM MANZANAS M, COLONIAS C ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE M.ZONA      = " + Convert.ToInt32(txtZona.Text.Trim());
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.ESTADO    = C.ESTADO ";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.MUNICIPIO = C.MUNICIPIO ";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.COLONIA   = C.COLONIA ";
            con.cadena_sql_interno = con.cadena_sql_interno + " GROUP BY C.COLONIA, C.NOMCOL  ";
            con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY C.COLONIA";

            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                con.cerrar_interno();
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN CON RESPECTO A LA BÚSQUEDA", "INFORMACIÓN");
            }
            else //en caso de encontrar un dato, se realiza toda la acción de abajo 
            {
                dgResultado.DataSource = LLENAR_GRID_1; //FORMA PARA LLENAR EL DATAGRIDVIEW CON LA CONSULTA 
                con.cerrar_interno();
                dgResultado.EnableHeadersVisualStyles = false; // Desactiva estilos predeterminados
                dgResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 54, 151); //COLOR DEL ENCABEZADO CON RGB (revisar si es catastro o teso)
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
                dgResultado.SelectionMode = DataGridViewSelectionMode.FullRowSelect; //SELECCIONAR TODA LA FILA 
                dgResultado.MultiSelect = false; // Solo permitir selección de una fila a la vez
                dgResultado.Columns[0].Width = 80;
                dgResultado.Columns[1].Width = 527;
                //
                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 
                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                //ME FALTA SACAR EL TOTAL , duda de cómo  ?
                dgResultado.Enabled = true;


                /////////////////////////////////////////lote ver si baja o aquí está bien 
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT COUNT (lote)";
                con.cadena_sql_interno = con.cadena_sql_interno + "  FROM predios ";
                con.cadena_sql_interno = con.cadena_sql_interno + " WHERE ESTADO    =    " + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND MUNICIPIO =    " + Program.municipioN;
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND ZONA      =    " + Convert.ToInt32(txtZona.Text.ToString());
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND MANZANA   =    " + Convert.ToInt32(txtManzana.Text.ToString());
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        lblConteoLotes.Text = "N° LOTES DENTRO DE LA MANZANA QUE SERÍAN AFECTADOS: " + con.leer_interno[0].ToString().Trim();
                    }
                }
                ///CERRAR LA CONEXIÓN
                con.cerrar_interno();
                //
                deshabilitarClaveCatastral();
                btnCambioLote.Enabled = true;
                btnCambioManzana.Enabled = true;
                btnBuscarClave.Enabled = false;
            }
        }
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta();
            ///////////////////////////////////////////////////////////////////////////////////
            /////////////////////REVISAR LO DEL LOTE, CONFIRMAR SI ES ASÍ BUENO 
            ///////////////////////////////////////////////////////////////////////////////////
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// AL DAR CLICK EN GOOGLE MAPS; ABRIR LA PÁGINA DE GOOGLE MAPS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnMaps_Click(object sender, EventArgs e)
        {
            string latitud = Convert.ToString(mlatitud).ToString();
            string longitud = Convert.ToString(mlongitud.ToString());
            Process.Start($"https://www.google.com/maps?q={latitud},{longitud}");
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// AL MOMENTO DE DAR DOUBLE CLICK EN EL DATAGRIDVIEW; VALIDAR Y COLOCAR LOS DATOS ABAJO 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void dgResultado_DoubleClick(object sender, EventArgs e)
        {
            var cellValue = dgResultado.CurrentRow.Cells[1].Value.ToString();
            if (string.IsNullOrWhiteSpace(cellValue?.ToString())) //ver esto 
            {
                MessageBox.Show("SE GENERÓ UN ERROR, NO HAY INFORMACIÓN", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgResultado.CurrentRow.Cells[0].Value.ToString().Trim() == "")
            {
                MessageBox.Show("NO SE PUEDE  TRABAJAR SIN UN FOLIO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///revisar esto 
            lblColoniaOrigen.Text = lblColonia.Text.ToString();
            lblColoniaDestino.Text = Convert.ToString(dgResultado.CurrentRow.Cells[0].Value.ToString() + " - " + dgResultado.CurrentRow.Cells[1].Value.ToString());
            dgResultado.Enabled = false;
            btnCancelarColoniasAbajo.Enabled = true;
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
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////MEDIANTE EL PROCEDIMIENTO ALMACENADO; SOLO VA A SER EL CAMBIO EN PREDIOS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCambioLote_Click(object sender, EventArgs e)
        {
            string Clave = "";
            Clave = " " + txtMun.Text.ToString() + " - " + txtZona.Text.ToString() + " - " +  txtManzana.Text.ToString() + " - " + txtLote.Text.ToString();
            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE REALIZAR EL CAMBIO DE COLONIA EN LA CLAVE?" + Clave , "INFORMACIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Information); //+// CLAVE, "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk); 
            if (resp == DialogResult.Yes)
            {
                try
                {
                    if (lblColoniaOrigen.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR EL CAMBIO SIN TENER LA COLONIA ORIGEN", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    if (lblColoniaDestino.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR EL CAMBIO SIN TENER LA COLONIA DESTINO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                    //ESTA VARIABLE SIRVE PARA INDICARLE QUÉ HACER AL PROCEDIMIENTO ALMACENADO; EN ESTE 
                    //CASO , SOLO VA A REALIZAR EL UPDATE EN PREDIOS 
                    TIPOCAMBIO = 1;
                    ZONA = Convert.ToInt32(txtZona.Text.ToString());
                    MANZANA = Convert.ToInt32(txtManzana.Text.ToString());
                    LOTE = Convert.ToInt32(txtLote.Text.ToString());
                    COLONIA = Convert.ToInt32(lblColoniaDestino.Text.ToString().Substring(0, 2));
                    con.conectar_base_interno();
                    con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                    con.open_c_interno();
                    SqlCommand cmd = new SqlCommand("SONGSP_CAMBIOCOLONIAPORCLAVE", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
                    cmd.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                    cmd.Parameters.Add("@TIPODECAMBIO", SqlDbType.Int, 2).Value = TIPOCAMBIO;
                    cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
                    cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 2).Value = Program.municipioN;
                    cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = ZONA;
                    cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = MANZANA;
                    cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = LOTE;
                    cmd.Parameters.Add("@COLONIA", SqlDbType.Int, 2).Value = COLONIA;
                    cmd.Parameters.Add("@USUARIO", SqlDbType.Char, 10).Value = Program.acceso_usuario;
                    cmd.Parameters.Add("@OPERACION", SqlDbType.Char, 15).Value = "CAMBIOLOTE";
                    cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                    cmd.Connection = con.cnn_interno;
                    cmd.ExecuteNonQuery();
                    validacionCambio = Convert.ToInt32(cmd.Parameters["@VALIDACION"].Value);
                    con.cerrar_interno();
                    if (validacionCambio == 1) //en caso de generarse de forma correcta, se realizan las acciones colocadas en el procedimiento  
                    {
                        MessageBox.Show("SE CAMBIÓ UN TOTAL DE: " + lblConteoLotes.Text.ToString() + " PREDIOS ", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiartodo();
                        return;
                    }
                    else ///AL ENTRAR AQUÍ ES PORQUE OCURRIÓ UN ERROR AL CAMBIAR EL NOMBRE
                    {
                        MessageBox.Show("OCURRIÓ UN ERROR AL REALIZAR EL CAMBIO; COMUNICARSE CON EL ADMINISTRADOR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        limpiartodo();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    con.cerrar_interno();
                    CapturarPantalla();
                    MessageBox.Show(ex.Message, "ERROR AL EJECUTAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CapturarPantalla();
                    return; // Retornar false si ocurre un error
                }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////MEDIANTE EL PROCEDIMIENTO ALMACENADO; VA A REALIZAR EL UPDATE A MANZANAS Y PREDIOS 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCambioManzana_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE REALIZAR EL CAMBIO DE COLONIA EN LA MANZANA  " + txtManzana.Text.ToString() + "?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (resp == DialogResult.Yes)
            {
                DialogResult resp2 = MessageBox.Show(lblConteoLotes.Text, "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (resp2 == DialogResult.Yes)
                    try
                    {
                        //ESTAS SEGURO DE AFECTAR LOS N REGISTROS?
                        // otro update     
                        if (lblColoniaOrigen.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR EL CAMBIO SIN TENER LA COLONIA ORIGEN", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                        if (lblColoniaDestino.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR EL CAMBIO SIN TENER LA COLONIA DESTINO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                        //ESTA VARIABLE SIRVE PARA INDICARLE QUÉ HACER AL PROCEDIMIENTO ALMACENADO; EN ESTE 
                        //CASO , VA A REALIZAR EL UPDATE EN MANZANAS,  Y DESPUÉS PREDIOS  
                        TIPOCAMBIO = 2;
                        ZONA = Convert.ToInt32(txtZona.Text.ToString());
                        MANZANA = Convert.ToInt32(txtManzana.Text.ToString());
                        LOTE = Convert.ToInt32(txtLote.Text.ToString());
                        COLONIA = Convert.ToInt32(lblColoniaDestino.Text.ToString().Substring(0, 2));
                        con.conectar_base_interno();
                        con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                        con.open_c_interno();
                        SqlCommand cmd = new SqlCommand("SONGSP_CAMBIOCOLONIAPORCLAVE", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
                        cmd.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                        cmd.Parameters.Add("@TIPODECAMBIO", SqlDbType.Int, 2).Value = TIPOCAMBIO;
                        cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
                        cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 2).Value = Program.municipioN;
                        cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = ZONA;
                        cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = MANZANA;
                        cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = LOTE;
                        cmd.Parameters.Add("@COLONIA", SqlDbType.Int, 2).Value = COLONIA;
                        cmd.Parameters.Add("@USUARIO", SqlDbType.Char, 10).Value = Program.acceso_usuario;
                        cmd.Parameters.Add("@OPERACION", SqlDbType.Char, 15).Value = "CAMBIOLOTE";
                        cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                        cmd.Connection = con.cnn_interno;
                        cmd.ExecuteNonQuery();
                        validacionCambio = Convert.ToInt32(cmd.Parameters["@VALIDACION"].Value);
                        con.cerrar_interno();
                        if (validacionCambio == 1) //en caso de generarse de forma correcta, se realizan las acciones colocadas en el procedimiento  
                        {
                            MessageBox.Show("SE CAMBIÓ UN TOTAL DE: " + lblConteoLotes.Text.ToString() + " PREDIOS", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            limpiartodo();
                            return;
                        }
                        else ///AL ENTRAR AQUÍ ES PORQUE OCURRIÓ UN ERROR AL CAMBIAR EL NOMBRE
                        {
                            MessageBox.Show("ERROR", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            limpiartodo();
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR AL EJECUTAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// BOTÓN SIRVE PARA LIMPIAR LO DE ABAJO (LBLSDECOLONIAS ORIGEN / DESTINO, EL CONTEO, ETC)
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnCancelarColoniasAbajo_Click(object sender, EventArgs e)
        {
            lblColoniaOrigen.Text = "";
            lblColoniaDestino.Text = "";
            lblConteoLotes.Text = "0";
            dgResultado.Enabled = true;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////MÉTODOS PARA QUE AL CUMPLIR UNA CONDICIÓN LA CAJA DE TEXTO; BRINQUE A LA OTRA DE FORMA AUTÓMATICA
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtZona_TextChanged(object sender, EventArgs e)
        {
            if (txtZona.Text.Length == 2) //SI CUMPLE CON ESTA CONDICIÓN DE LA LONGITUD DE TEXTO / CARACTERES
            {
                txtManzana.Focus(); //VA A PASAR CON ESTA CAJA DE TEXTO DE FORMA AUTOMÁTICA 
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

        private void btnBuscarClave_Click(object sender, EventArgs e)
        {
            frmCatastro03BusquedaCatastro bsuqueda = new frmCatastro03BusquedaCatastro();
            bsuqueda.ShowDialog(); // No modal, no bloquea
            txtZona.Text = Program.zonaV;
            txtManzana.Text = Program.manzanaV;
            txtLote.Text = Program.loteV;
            txtEdificio.Text = Program.edificioV;
            txtDepto.Text = Program.deptoV;
            // O mostrar sin crear variable explícita
            //new Form2().Show();
            if(txtManzana.Text != "")
            {
                Consulta();
                btnNuevo.Enabled = false;
            }
            
            btnConsulta.Enabled = false;
        }

        private void btnCambioLote_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip(); 
            tooltip.SetToolTip(btnCambioLote, "REALIZAR CAMBIO DE COLONIA EN EL LOTE");
        }

        private void btnCambioManzana_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnCambioManzana, "REALIZAR CAMBIO DE COLONIA EN LA MANZANA");
        }

        private void txtDepto_TextChanged(object sender, EventArgs e)
        {
            if (txtDepto.Text.Length == 4)
            {
                btnConsulta.Focus();
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////// MÉTODO PARA LOS TOOLTIP; ES DECIR QUE AL PASAR EL CURSOR POR LOS BOTONES; MUESTRA UN TEXTO 
        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnMinimizar_MouseHover(object sender, EventArgs e)
        {
            //PRIMERO DEBEMOS INSTANCIAR UN OBJETO DE TOOLTIP
            ToolTip tooltip = new ToolTip();
            //DESPUÉS, CON LA INSTANCIA; LE DECIMOS EL BOTÓN QUE DESEAMOS 
            //COLOCARLE EL TEXTO, Y LA , CON COMILLAS DOBLES INDICA EL TEXTO
            tooltip.SetToolTip(btnMinimizar, "MINIMIZAR LA PANTALLA");
        }
        private void btnNuevo_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnNuevo, "NUEVO PROCESO");
        }
        private void btnCancela_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnCancela, "LIMPIAR PANTALLA");
        }
        private void btnSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnSalida, "SALIR DE LA PANTALLA");
        }
        private void btnMaps_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnMaps, "ABRIR PÁGINA DE GOOGLE MAPS");
        }
    }
}
