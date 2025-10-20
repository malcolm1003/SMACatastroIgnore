using AccesoBase;
using GMap.NET.MapProviders;
using SMACatastro.catastroCartografia;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Utilerias;
using Excel = Microsoft.Office.Interop.Excel;
using Form = System.Windows.Forms.Form;

namespace SMACatastro.catastroRevision
{
    public partial class frmBloqueoDesbloqueo : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();
        int ZONA, MANZANA, LOTE, validacionBloqueo, desbloqueo, CRUD, OPERACIONBLOQUEODESBLOQUEO, AREAOPERACION = 0;
        string EDIFICIO, DEPTO, usoSuelo = "";
        double TERRENO1, TERRENO2, TERRENO3, TERRENO4, TERRENO5, latitud, longitud = 0.0;
        double CONSTRUCCION1, CONSTRUCCION2, CONSTRUCCION3, CONSTRUCCION4, CONSTRUCCION5 = 0.0;
        ////////////////////////////////////////////////////////////
        ///////////////// -------INICIALIZA COMPONENTE
        ////////////////////////////////////////////////////////////
        public frmBloqueoDesbloqueo()
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
            btnNuevo.Focus();
            lblUsuario.Text = "Usuario: " + Program.nombre_usuario.Trim();
            limpiartodo();
        }
        private void frmBloqueoDesbloqueo_Activated(object sender, EventArgs e)
        {
            btnNuevo.Focus();
        }
        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ssss");
        }
        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        void inicio()
        {
            btnNuevo.Enabled = false;
            btnBuscar.Enabled = false;
            btnBuscarClave.Enabled = false;
            txtZona.Enabled = true;
            btnSalida.Enabled = false;
            txtZona.Focus();
            txtManzana.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;
            btnConsulta.Enabled = true;
        }
        void limpiartodo()
        {
            //////////textboxis
            txtZona.Text = "";
            txtManzana.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";
            //deshabilitar cajas 
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            btnNuevo.Enabled = true;
            btnConsulta.Enabled = false;
            btnBuscar.Enabled = false;
            btnSalida.Enabled = true;
            ////////label
            lblTitular.Text = "";
            lblDomicilio.Text = "";
            lblColonia.Text = "";
            lblCalle.Text = "";
            lblUsoDeSuelo.Text = "";
            lblEntCalle.Text = "";
            lblYCalle.Text = "";
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
            lblObservaciones.Text = "";
            lblLatitud.Text = "";
            lblLongitud.Text = "";
            lblBloqueo.Text = "";
            lblBloq.Text = "";
            lblInfoBloqueo.Text = "";
            lblDesbloqueo.Visible = false;
            lblInfoBloqueo.Visible = false;
            pbxBloqueo.Visible = false;
            pbxDesbloqueo.Visible = false;
            lblOperacion.Text = "MOTIVOS DE OPERACIÓN";
            lblHistorial.Text = "HISTORIAL DE BLOQUEOS";
            //LIMPIAR Y DESHABILITAR ESTA COSA
            btnMaps.Enabled = false;
            btnBloquear.Visible = true;
            btnDesbloquear.Visible = true;
            btnBloquear.Enabled = false;
            btnDesbloquear.Enabled = false;
            txtInfoBloqueo.Text = "";
            txtInfoBloqueo.Enabled = false;
            txtInfoDesbloqueo.Text = "";
            txtInfoDesbloqueo.Enabled = false;
            txtInfoBloqueo.Visible = false;
            txtInfoDesbloqueo.Visible = false;
            btnBloquear.Visible = false;
            btnDesbloquear.Visible = false;
            btnCandadoAbierto.Visible = false;
            btnCandadoCerrado.Visible = false;
            ////limpiar el datagrid view    
            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            //
            gMapControl1.Visible = false;

            btnExcel.Enabled = false;
            btnBuscarClave.Enabled = true;
            btnBuscar.Enabled = true;
            pbxBloqueo.Visible = false;
            pbxDesbloqueo.Visible = false;
            lblComentario.Visible = false;
        }
        ////////////////////////////////////////////////////
        //////////// -- BUSCAR INVOCAR A LA OTRA PANTALLA
        ////////////////////////////////////////////////////
        void BusquedaGeneral()
        {
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT MUNICIPIO, ZONA, MANZANA, LOTE, EDIFICIO, DEPTO ,";
            con.cadena_sql_interno = con.cadena_sql_interno + "         RTRIM(COMENTARIO) 'COMENTARIO', FechaModi, usrMod";
            con.cadena_sql_interno = con.cadena_sql_interno + "    FROM BLOQCVE_2";
            con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE ESTADO    = " + Program.PEstado; //variable  de estado con el program
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND MUNICIPIO = " + Program.municipioN; //variable de municipio con el program 
            //SOLO PARA EL DATAGRID VIEW 
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            SqlDataAdapter daa = new SqlDataAdapter(con.cmd_interno); //SQL adaptador y haces uno nuevo                                                                                                                                 
            DataTable grid_table = new DataTable(); //Crear nueva tabla
            daa.Fill(grid_table); //método para llenar el datagrid
            dgResultado.DataSource = grid_table; //de qué se va a alimentar la caja 
            con.leer_interno = con.cmd_interno.ExecuteReader();
            con.cerrar_interno();
            //cerrar

            //DAR FORMATO AL DATAGRID VIEW

            dgResultado.Columns[0].Width = 50;
            dgResultado.Columns[1].Width = 50;
            dgResultado.Columns[2].Width = 50;
            dgResultado.Columns[3].Width = 50;
            dgResultado.Columns[4].Width = 50;
            dgResultado.Columns[5].Width = 50;
            dgResultado.Columns[6].Width = 750;
            dgResultado.Columns[7].Width = 180;
            dgResultado.Columns[8].Width = 180;
            //dgResultado.Columns[4].Width = 200;


            dgResultado.EnableHeadersVisualStyles = false; // Desactiva estilos predeterminados

            dgResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 54, 151);
            dgResultado.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
            dgResultado.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8); //
            //FromArgb(159, 54, 151);

            dgResultado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            foreach (DataGridViewColumn columna in dgResultado.Columns)
            {
                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (DataGridViewColumn columna in dgResultado.Columns)
            {
                columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Configuración de selección
            dgResultado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgResultado.MultiSelect = false; // Solo permitir selección de una fila a la vez

            // Deshabilitar edición
            dgResultado.ReadOnly = true;
            // Estilos visuales
            dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgResultado.RowHeadersVisible = false;
            //dgResultado.Rows[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;


            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
            gMapControl1.Position = new GMap.NET.PointLatLng(19.262174, -99.5330638);
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 15;
            gMapControl1.AutoScroll = true;
            gMapControl1.Visible = true;

            //btnExcel.Enabled = true;
        }
        ////////////////////////////////////////////////
        //////////////// ------MÉTODO PARA LA CONSULTA 
        ////////////////////////////////////////////////
        void Consulta()
        {
            if (txtZona.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN LA ZONA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtZona.Focus(); return; }
            if (txtManzana.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN LA MANZANA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtManzana.Focus(); return; }
            if (txtLote.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN EL LOTE", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtLote.Focus(); return; }

            if (txtEdificio.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BÚSUQEDA SIN EL EDIFICIO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("NECESITAS COLOCAR DOS CARACTERES EN EL EDIFICIO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEdificio.Focus(); return; }

            if (txtDepto.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN EL DEPTO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("NECESITAS COLOCAR CUATRO CARACTERES EN EL DEPARTAMENTO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDepto.Focus(); return; ; }
            //EMPIEZA LA CONSULTA PARA LLENAR EL DATA GRID
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT MUNICIPIO, ZONA, MANZANA, LOTE, EDIFICIO, DEPTO ,";
            con.cadena_sql_interno = con.cadena_sql_interno + "         H.COMENTARIO 'COMENTARIO', H.FecMod, H.usrMod";
            con.cadena_sql_interno = con.cadena_sql_interno + "    FROM HBLOQCVE_2 H";
            con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE H.ESTADO    = " + Program.PEstado; //variable  de estado con el program
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.MUNICIPIO = " + Program.municipioN; //variable de municipio con el program 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.Manzana   = " + Convert.ToInt32(txtManzana.Text.Trim());  //Se cocatena la manzana que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.Lote      = " + Convert.ToInt32(txtLote.Text.Trim());  //Se cocatena el lote que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.DEPTO     = '" + txtDepto.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.EDIFICIO  = '" + txtEdificio.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "ORDER BY H.FecMod DESC";
            //SOLO PARA EL DATAGRID VIEW 
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            SqlDataAdapter daa = new SqlDataAdapter(con.cmd_interno); //SQL adaptador y haces uno nuevo                                                                                                                                 
            DataTable grid_table = new DataTable(); //Crear nueva tabla
            daa.Fill(grid_table); //método para llenar el datagrid
            dgResultado.DataSource = grid_table; //de qué se va a alimentar la caja 
            con.leer_interno = con.cmd_interno.ExecuteReader();
            //cerrar

            lblHistorial.Text = "HISTORIAL DE BLOQUEOS";
            //DAR FORMATO AL DATAGRID VIEW
            dgResultado.Columns[0].Width = 170;
            dgResultado.Columns[1].Width = 400;
            dgResultado.Columns[2].Width = 180;
            dgResultado.Columns[3].Width = 200;
            dgResultado.Columns[4].Width = 200;
            dgResultado.EnableHeadersVisualStyles = false; // Desactiva estilos predeterminados
            dgResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 54, 151);
            dgResultado.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
            dgResultado.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8); //
            dgResultado.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            foreach (DataGridViewColumn columna in dgResultado.Columns)
            {
                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (DataGridViewColumn columna in dgResultado.Columns)
            {
                columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Configuración de selección
            dgResultado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgResultado.MultiSelect = false; // Solo permitir selección de una fila a la vez

            // Deshabilitar edición
            dgResultado.ReadOnly = true;
            // Estilos visuales
            dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgResultado.RowHeadersVisible = false;
            //dgResultado.Rows[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;

            con.cerrar_interno();

            con.conectar_base_interno();
            con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
            con.open_c_interno();
            CRUD = 1; //OPERACIÓN DE CONSULTA DENTRO DE MI PROCEDIMIENTO ALMACENADO 
            SqlCommand cmd = new SqlCommand("SONGSP_CONSULTABLOQUEO", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
            cmd.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
            //cmd.Parameters.Add("@CRUD", SqlDbType.Int, 1).Value = CRUD;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
            cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 2).Value = Program.municipioN;
            cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = Convert.ToInt32(txtZona.Text.ToString());
            cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = Convert.ToInt32(txtManzana.Text.ToString());
            cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = Convert.ToInt32(txtLote.Text.ToString());
            cmd.Parameters.Add("@EDIFICIO", SqlDbType.Char, 2).Value = txtEdificio.Text.Trim();
            cmd.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = txtDepto.Text.Trim();
            cmd.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 100).Value = "";
            //cmd.Parameters.Add("@AREA", SqlDbType.Char, 2).Value = Program.cIdAreaBloqueoC; //PROGRAM.cIdArea;
            cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 30).Value = Program.nombre_usuario;
            cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
            cmd.Connection = con.cnn_interno;
            cmd.ExecuteNonQuery();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {   // Continúa leyendo mientras haya más filas{ {
                string valor = rdr[4].ToString();
                if (valor == string.Empty)
                {
                    MessageBox.Show("NO EXISTE INFORMACIÓN CON ESA CLAVE CATASTRAL", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.cerrar_interno();
                    limpiartodo();
                    gMapControl1.Visible = false;
                    btnMaps.Enabled = false;
                    btnNuevo.Focus();
                    return;
                }
                lblTitular.Text = rdr["PMNPROPT"].ToString().Trim();
                lblDomicilio.Text = rdr["DOMFIST"].ToString().Trim().ToUpper();
                lblCalle.Text = rdr["NOMCALLET"].ToString().Trim();
                lblColonia.Text = rdr["NOMCOLT"].ToString().Trim();
                lblUsoDeSuelo.Text = rdr["DESCRUSOT"].ToString().Trim();
                lblEntCalle.Text = rdr["ENTCALLET"].ToString().Trim();
                lblYCalle.Text = rdr["YCALLET"].ToString().Trim();
                if (lblValTerrPriv.Text.Trim() == "") { lblValTerrPriv.Text = "0.0"; }
                if (lblValorConsPriv.Text.Trim() == "") { lblValorConsPriv.Text = "0.0"; }
                if (lblValTerrCom.Text.Trim() == "") { lblValTerrCom.Text = "0.0"; }
                if (lblValConsCom.Text.Trim() == "") { lblValConsCom.Text = "0.0"; }
                if (lblValTotTerr.Text.Trim() == "") { lblValTotTerr.Text = "0.0"; }
                if (lblValTotCons.Text.Trim() == "") { lblValTotCons.Text = "0.0"; }
                if (lblValTotTerr.Text.Trim() == "") { lblValTotTerr.Text = "0.0"; }
                if (lblValTotCons.Text.Trim() == "") { lblValTotCons.Text = "0.0"; }
                if (lblSupConsCom.Text.Trim() == "") { lblSupConsCom.Text = "0.0"; }
                if (lblSupConsPriv.Text.Trim() == "") { lblSupConsCom.Text = "0.0"; }
                TERRENO1 = Convert.ToDouble(rdr["STERRPROPT"].ToString().Trim());
                TERRENO2 = Convert.ToDouble(rdr["STERRCOMT"].ToString().Trim());
                TERRENO3 = Convert.ToDouble(rdr["VTERRPROPT"].ToString().Trim());
                TERRENO4 = Convert.ToDouble(rdr["VTERRCOMT"].ToString().Trim());
                TERRENO5 = TERRENO3 + TERRENO4;
                //////
                CONSTRUCCION1 = Convert.ToDouble(rdr["SCONSPROPT"].ToString().Trim()); //SUP CONS 
                CONSTRUCCION2 = Convert.ToDouble(rdr["SCONSCOMT"].ToString().Trim()); //SUP CONS COM
                CONSTRUCCION3 = Convert.ToDouble(rdr["VTERRPROPT"].ToString().Trim()); //VALOR CONS P
                CONSTRUCCION4 = Convert.ToDouble(rdr["VCONSPROPT"].ToString().Trim()); //VALOR CONS C
                CONSTRUCCION5 = CONSTRUCCION3 + CONSTRUCCION4;
                //COMENTARIO 
                lblSupTerrPriv.Text = String.Format("{0:#,##0.00}", TERRENO1);
                lblSupTerrComun.Text = String.Format("{0:#,##0.00}", TERRENO2);
                lblValTerrPriv.Text = String.Format("{0:#,##0.00}", TERRENO3);
                lblValTerrCom.Text = String.Format("{0:#,##0.00}", TERRENO4);
                lblValTotTerr.Text = String.Format("{0:#,##0.00}", TERRENO5);
                lblValTotCons.Text = String.Format("{0:#,##0.00}", CONSTRUCCION5);
                lblSupConsPriv.Text = rdr["SCONSPROPT"].ToString().Trim();
                lblSupConsCom.Text = rdr["SCONSCOMT"].ToString().Trim();
                lblValorConsPriv.Text = rdr["VCONSPROPT"].ToString().Trim();
                lblValConsCom.Text = rdr["VCONSCOMT"].ToString().Trim();
                lblSupConsPriv.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblSupConsPriv.Text.Trim()));
                lblSupConsCom.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblSupConsCom.Text.Trim()));
                lblValorConsPriv.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValorConsPriv.Text.Trim()));
                lblValConsCom.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValConsCom.Text.Trim()));
                lblValor.Text = rdr["NVALORFISCT"].ToString().Trim();
                lblValor.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValor.Text.Trim()));
                lblObservaciones.Text = rdr["COBSPROP"].ToString().Trim().ToUpper();
            }
            con.cerrar_interno();
            ///OBTENER LA GEOLOCALIZACIÓN, ES DECIR LATITUD Y LONGITUD DE LA TABLA 
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT LATITUD, LONGITUD";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_GEOLOCALIZACION";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
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
                    lblLatitud.Text = con.leer_interno[0].ToString().Trim();
                    lblLongitud.Text = con.leer_interno[1].ToString().Trim();
                }
            }
            ///
            con.cerrar_interno();

            //VER SÍ ESTÁ BLOQUEADA, JALAMOS EL COMENTARIO DE LA TBALA DE BLOQUEO
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT COMENTARIO ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    FROM BLOQCVE_2";
            con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND Manzana   = " + Convert.ToInt32(txtManzana.Text.Trim());  //Se cocatena la manzana que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND Lote      = " + Convert.ToInt32(txtLote.Text.Trim());  //Se cocatena el lote que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND DEPTO     = '" + txtDepto.Text.Trim() + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND EDIFICIO  = '" + txtEdificio.Text.Trim() + "'";
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            while (con.leer_interno.Read())
            {
                //txtInfoBloqueo.Text = con.leer_interno[0].ToString();
                lblInfoBloqueo.Text = con.leer_interno[0].ToString().Trim().ToUpper();
                lblInfoBloqueo.Visible = true;
            }
            ///
            con.cerrar_interno();
            ////OBTENEEMOS LA LATITUD Y LONGITUD PARA MOSTRARLA EN EL MAPA 

            if (string.IsNullOrWhiteSpace(lblLatitud.Text) || string.IsNullOrWhiteSpace(lblLongitud.Text))
            {
                btnMaps.Enabled = false;
                latitud = 19.262174; //19.262174;
                longitud = -99.5330638; //99.5330638
                gMapControl1.Visible = false;
            }
            else
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
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            btnConsulta.Enabled = false;
            ///CÓDIGO REVISAR EL ESE PARA QIUE SALGA 
            if (lblInfoBloqueo.Text != "") //LO QUE REALIZA CUANDO ESTÁ DESBLOQUEADO O NO TIENE NADA 
            {
                txtInfoDesbloqueo.Visible = true;
                txtInfoDesbloqueo.Enabled = true;
                txtInfoDesbloqueo.Focus();
                btnBloquear.Visible = false;
                lblDesbloqueo.Visible = true;
                btnMaps.Enabled = true;
                lblOperacion.Text = "MOTIVO DEL DESBLOQUEO";
                lblDesbloqueo.Text = "Desbloqueo:";

                btnCandadoCerrado.Visible = true;
                btnCandadoCerrado.Enabled = true;

                //pbxBloqueo.Visible = true;
                pbxDesbloqueo.Visible = false;
                //lblBloqueo.Visible = false;
                lblDesbloqueo.Visible = true;
                lblComentario.Visible = true;
                MessageBox.Show("CLAVE CATASTRAL BLOQUEADA", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtInfoDesbloqueo.Focus(); return;

            }
            ///REVISAR PARA MANDAR EL ESE LABEL 
            else //AL MOMENTO DE PARA BLOQUEAR
            {
                lblBloq.Visible = true;
                lblBloq.Text = "Bloqueo:";
                lblDesbloqueo.Visible = false;

                btnDesbloquear.Visible = false;
                txtInfoDesbloqueo.Visible = true;
                txtInfoDesbloqueo.Enabled = true;
                lblOperacion.Text = "MOTIVO DEL BLOQUEO";
                pbxBloqueo.Visible = false;
                //pbxDesbloqueo.Visible = true;
                btnCandadoAbierto.Visible = true;
                btnCandadoAbierto.Enabled = true;

                MessageBox.Show("CLAVE CATASTRAL NO CUENTA CON BLOQUEO", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information); txtInfoDesbloqueo.Focus(); return;
            }
        }
        ///////////////////////////////////////////////////////
        //// ---- BLOQUEAR   CLAVE CATASTRAL
        ///////////////////////////////////////////////////////
        void Bloqueo()
        {
            try
            {
                if (txtInfoDesbloqueo.Text == "") { MessageBox.Show("NO SE PUEDE BLOQUEAR LA CLAVE CATASTRAL SIN UNA OBSERVACIÓN", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtInfoBloqueo.Focus(); return; }
                if (txtInfoDesbloqueo.Text.Length < 20) { MessageBox.Show("NECESITAS COLOCAR MÁS INFORMACIÓN PARA BLOQUEAR UNA CLAVE CATASTRAL", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtInfoBloqueo.Focus(); return; }
                //if (lblLatitud.Text == "") { MessageBox.Show("NO SE PUEDE BLOQUEAR SIN LATITUD, PASA A CARTOGRAFÍA PARA DARLA DE ALTA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); gMapControl1.Focus(); return; }
                //if (lblLongitud.Text == "") { MessageBox.Show("NO SE PUEDE BLOQUEAR SIN LONGITUD, PASA A CARTOGRAFÍA PARA DARLA DE ALTA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); gMapControl1.Focus(); return; 
                btnCandadoCerrado.Visible = true;
                btnCandadoAbierto.Visible = false;

                ZONA = Convert.ToInt32(txtZona.Text.ToString());
                MANZANA = Convert.ToInt32(txtManzana.Text.ToString());
                LOTE = Convert.ToInt32(txtLote.Text.ToString());
                EDIFICIO = txtEdificio.Text.Trim();
                DEPTO = txtDepto.Text.Trim();
                string OBSERVACION = txtInfoDesbloqueo.Text.Trim();
                string USUARIO = Program.nombre_usuario;

                AREAOPERACION = Program.cIdAreaBloqueoC;
                OPERACIONBLOQUEODESBLOQUEO = 1; //OPERACIÓN BLOQUEO EN MI PROCEDIMIENTO 
                validacionBloqueo = 0;
                //Variables que se mandan de forma automática
                //Usuario que va a aparecer en la base de datos
                con.conectar_base_interno();
                con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                con.open_c_interno();
                //PROCEDIMIENTO ALMACENADO PARA BLOQUEAR O DESBLOQUEAR LA CLAVE CATASTRAL SE PUEDE BLOQUEAR POR AMBAS AREAS
                SqlCommand cmd1 = new SqlCommand("SONGSP_OPERACIONESBLOQUEOS", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
                cmd1.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                cmd1.Parameters.Add("@AREAOPERACION", SqlDbType.Int, 2).Value = AREAOPERACION; //AREA OPERACIÓN QUE VA A REALIZAR EL AREA, EN ESTE CASO ES EL 9
                cmd1.Parameters.Add("@OPERACIONBLOQUEODESBLOQUEO", SqlDbType.Int, 1).Value = OPERACIONBLOQUEODESBLOQUEO; //OPREACION QUE VA A REALIZAR EL PROCEDIMIENTO ALMACENADO, SEA BLOQUEO O DESBLOQUEO. 
                cmd1.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
                cmd1.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 2).Value = Program.municipioN;
                cmd1.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = ZONA;
                cmd1.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = MANZANA;
                cmd1.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = LOTE;
                cmd1.Parameters.Add("@EDIFICIO", SqlDbType.Char, 2).Value = EDIFICIO;
                cmd1.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = DEPTO;
                cmd1.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 100).Value = OBSERVACION;
                cmd1.Parameters.Add("@AREA", SqlDbType.Int, 2).Value = Program.acceso_id_area;
                //cmd.Parameters.Add("@OBSERVACION", SqlDbType.NChar, 250).Value = OBSERVACION; //Debe de empatar el nombre de la variable con la longitud del campo 
                cmd1.Parameters.Add("@USUARIO", SqlDbType.VarChar, 30).Value = USUARIO;
                cmd1.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd1.Connection = con.cnn_interno;
                cmd1.ExecuteNonQuery();
                string clavecatastro = Program.municipioN + "-" + ZONA + "-" + MANZANA + "-" + LOTE + "-" + EDIFICIO + "-" + DEPTO;
                validacionBloqueo = Convert.ToInt32(cmd1.Parameters["@VALIDACION"].Value);

                con.cerrar_interno();
                if (validacionBloqueo == 1) //en caso de generarse de forma correcta, se realizan las acciones colocadas en el procedimiento  (2 = bien)
                {
                    MessageBox.Show("LA CLAVE CATASTRAL: " + " " + clavecatastro + " " + "SE BLOQUEÓ CON EXITO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiartodo();
                    return;
                }
                else
                {
                    MessageBox.Show("OCURRIÓ UN ERROR AL BLOQUEAR LA CLAVE CATASTRAL: " + " " + clavecatastro, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
        }
        ////////////////////////////////////////////////
        //// ------------ DESBLOQUEAR CLAVE CATASTRAL
        ////////////////////////////////////////////////
        void Desbloqueo()
        {
            try 
            {
                if (txtInfoDesbloqueo.Text == "") { MessageBox.Show("NO SE PUEDE GENERAR EL DESBLOQUEO DE LA CLAVE CATASTRAL SIN UNA OBSERVACIÓN", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtInfoDesbloqueo.Focus(); return; }
                if (txtInfoDesbloqueo.Text.Length < 20) { MessageBox.Show("NECESITAS COLOCAR MÁS INFORMACIÓN PARA DESBLOQUEAR UNA CLAVE CATASTRAL", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtInfoDesbloqueo.Focus(); return; }

                // if (lblLatitud.Text == "") { MessageBox.Show("NO SE PUEDE DESBLOQUEAR SIN LATITUD, PASA A CARTOGRAFÍA PARA DAR DE ALTA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); gMapControl1.Focus(); return; }
                // if (lblLongitud.Text == "") { MessageBox.Show("NO SE PUEDE DESBLOQUEAR SIN LONGITUD, PASA A CARTOGRAFÍA PARA DAR DE ALTA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); gMapControl1.Focus(); return; }
                btnCandadoCerrado.Visible = false;
                btnCandadoAbierto.Visible = true;

                ZONA = Convert.ToInt32(txtZona.Text.ToString());
                MANZANA = Convert.ToInt32(txtManzana.Text.ToString());
                LOTE = Convert.ToInt32(txtLote.Text.ToString());
                EDIFICIO = txtEdificio.Text.Trim();
                DEPTO = txtDepto.Text.Trim();
                //Tipo de validacion de bloqueo que se va a mandar
                string OBSERVACION = txtInfoDesbloqueo.Text.Trim();
                string USUARIO = Program.nombre_usuario;
                AREAOPERACION = Program.cIdAreaBloqueoC;
                OPERACIONBLOQUEODESBLOQUEO = 2; //OPERACIÓN BLOQUEO
                                                //Usuario que va a aparecer en la base de datos
                con.conectar_base_interno();
                con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                con.open_c_interno();
                ////PROCEDIMIENTO ALMACENADO PARA BLOQUEAR O DESBLOQUEAR LA CLAVE CATASTRAL SE PUEDE BLOQUEAR POR AMBAS AREAS CON UNA VARIABLE 
                SqlCommand cmd2 = new SqlCommand("SONGSP_OPERACIONESBLOQUEOS", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
                cmd2.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                cmd2.Parameters.Add("@AREAOPERACION", SqlDbType.Int, 2).Value = AREAOPERACION;
                cmd2.Parameters.Add("@OPERACIONBLOQUEODESBLOQUEO", SqlDbType.Int, 1).Value = OPERACIONBLOQUEODESBLOQUEO;
                cmd2.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
                cmd2.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 2).Value = Program.municipioN;
                cmd2.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = ZONA;
                cmd2.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = MANZANA;
                cmd2.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = LOTE;
                cmd2.Parameters.Add("@EDIFICIO", SqlDbType.Char, 2).Value = EDIFICIO;
                cmd2.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = DEPTO;
                cmd2.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 254).Value = OBSERVACION;
                cmd2.Parameters.Add("@AREA", SqlDbType.Int, 2).Value = Program.acceso_id_area;
                //c2md.Parameters.Add("@OBSERVACION", SqlDbType.NChar, 250).Value = OBSERVACION; //Debe de empatar el nombre de la variable con la longitud del campo 
                cmd2.Parameters.Add("@USUARIO", SqlDbType.VarChar, 30).Value = USUARIO;
                cmd2.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd2.Connection = con.cnn_interno;
                cmd2.ExecuteNonQuery();
                validacionBloqueo = Convert.ToInt32(cmd2.Parameters["@VALIDACION"].Value);
                con.cerrar_interno(); //AQUÍ VA ???
                string clavecatastro = Program.municipioN + "-" + ZONA + "-" + MANZANA + "-" + LOTE + "-" + EDIFICIO + "-" + DEPTO;
                if (validacionBloqueo == 1) //en caso de generarse de forma correcta, se realizan las acciones colocadas en el procedimiento  (2 = bien)
                {
                    MessageBox.Show("LA CLAVE CATASTRAL: " + " " + clavecatastro + " " + "SE DESBLOQUEÓ CON EXITO", "INFORMACIÓN", MessageBoxButtons.OK);
                    limpiartodo();
                    return;
                }
                else
                {
                    MessageBox.Show("OCURRIÓ UN ERROR AL BLOQUEAR LA CLAVE CATASTRAL: " + " " + clavecatastro, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
        }
        void exportarExcel()
        {
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Add();
                Excel.Worksheet worksheet = workbook.ActiveSheet;

                // Darle formato a los encabezados
                for (int i = 1; i <= dgResultado.Columns.Count; i++)
                {
                    Excel.Range headerCell = worksheet.Cells[1, i];
                    headerCell.Value = dgResultado.Columns[i - 1].HeaderText;
                    // Aplicar formato a todos los encabezados
                    headerCell.Font.Size = 11;
                    headerCell.Font.Bold = true;
                    headerCell.Font.Color = Color.White;
                    headerCell.Interior.Color = Excel.XlRgbColor.rgbDarkMagenta;
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                }

                // Datos (sin formato especial)
                for (int i = 0; i < dgResultado.Rows.Count; i++)
                {
                    if (dgResultado.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dgResultado.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dgResultado.Rows[i].Cells[j].Value?.ToString() ?? "";
                    }
                }
                worksheet.Columns.AutoFit();
                excelApp.Visible = true;

                // Liberar recursos
                System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL EXPORTAR A EXCEL:" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
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
                case 5: txtInfoBloqueo.BackColor = Color.Yellow; break;
                case 6: txtInfoDesbloqueo.BackColor = Color.Yellow; break;
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
                case 5: txtInfoBloqueo.BackColor = Color.White; break;
                case 6: txtInfoDesbloqueo.BackColor = Color.White; break;
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
                btnConsulta.Focus();
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        ///// ----------- SOLO ACEPTAR NÚMEROS Y DARLE ENTER PARA GENERAR LA CONSULTA 
        ///////////////////////////////////////////////////////////////////////////////////////
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
        private void txtDepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Consulta();
            }
        }
        private void btnConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Consulta();
            }
        }
        //////////////////////////////////////////////////////////////////////////
        //// ---------- MOSTRAR TEXTO AL PASAR EL BOTÓN (TOOLTIP)
        ///////////////////////////////////////////////////////////////////////////
        private void btnNuevo_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnNuevo, "NUEVO PROCESO");
        }
        private void btnBuscar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBuscar, "BÚSQUEDA GENERAL DE CLAVES CATASTRALES BLOQUEADAS");
        }
        private void btnBuscarClave_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBuscarClave, "BÚSQUEDA DE CLAVE CATASTRAL");
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
            Consulta();
            btnConsulta.Enabled = false;
        }

        private void dgResultado_DoubleClick(object sender, EventArgs e)
        {
            if (dgResultado.CurrentRow.Cells[0].Value.ToString() == "")
            {
                MessageBox.Show("SELECCIONE UN DATO CORRECTO", "¡ALERTA!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return; // Sale del método o procedimiento
            }
            int MUNICIPIO_M = Convert.ToInt32(dgResultado.CurrentRow.Cells[0].Value);
            int ZONA_M = Convert.ToInt32(dgResultado.CurrentRow.Cells[1].Value);
            int MANZANA_M = Convert.ToInt32(dgResultado.CurrentRow.Cells[2].Value);
            int LOTE_M = Convert.ToInt32(dgResultado.CurrentRow.Cells[3].Value);
            string EDIFICIO_M = dgResultado.CurrentRow.Cells[4].Value.ToString().Trim();
            string DEPTO_M = dgResultado.CurrentRow.Cells[5].Value.ToString().Trim();

            con.conectar_base_interno();
            con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
            con.open_c_interno();
            CRUD = 1; //OPERACIÓN DE CONSULTA DENTRO DE MI PROCEDIMIENTO ALMACENADO 
            SqlCommand cmd = new SqlCommand("SONGSP_CONSULTABLOQUEO", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
            cmd.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
            //cmd.Parameters.Add("@CRUD", SqlDbType.Int, 1).Value = CRUD;
            cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
            cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 2).Value = Program.municipioN;
            cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = ZONA_M;
            cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = MANZANA_M;
            cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = LOTE_M;
            cmd.Parameters.Add("@EDIFICIO", SqlDbType.Char, 2).Value = EDIFICIO_M;
            cmd.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = DEPTO_M;
            cmd.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 100).Value = "";
            //cmd.Parameters.Add("@AREA", SqlDbType.Char, 2).Value = Program.cIdAreaBloqueoC; //PROGRAM.cIdArea;
            cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 30).Value = Program.nombre_usuario;
            cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
            cmd.Connection = con.cnn_interno;
            cmd.ExecuteNonQuery();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {   // Continúa leyendo mientras haya más filas{ {
                string valor = rdr[4].ToString();
                if (valor == string.Empty)
                {
                    MessageBox.Show("NO EXISTE INFORMACIÓN CON ESA CLAVE CATASTRAL", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    con.cerrar_interno();
                    limpiartodo();
                    gMapControl1.Visible = false;
                    btnMaps.Enabled = false;
                    btnNuevo.Focus();
                    return;
                }
                lblTitular.Text = rdr["PMNPROPT"].ToString().Trim();
                lblDomicilio.Text = rdr["DOMFIST"].ToString().Trim().ToUpper();
                lblCalle.Text = rdr["NOMCALLET"].ToString().Trim();
                lblColonia.Text = rdr["NOMCOLT"].ToString().Trim();
                lblUsoDeSuelo.Text = rdr["DESCRUSOT"].ToString().Trim();
                lblEntCalle.Text = rdr["ENTCALLET"].ToString().Trim();
                lblYCalle.Text = rdr["YCALLET"].ToString().Trim();
                if (lblValTerrPriv.Text.Trim() == "") { lblValTerrPriv.Text = "0.0"; }
                if (lblValorConsPriv.Text.Trim() == "") { lblValorConsPriv.Text = "0.0"; }
                if (lblValTerrCom.Text.Trim() == "") { lblValTerrCom.Text = "0.0"; }
                if (lblValConsCom.Text.Trim() == "") { lblValConsCom.Text = "0.0"; }
                if (lblValTotTerr.Text.Trim() == "") { lblValTotTerr.Text = "0.0"; }
                if (lblValTotCons.Text.Trim() == "") { lblValTotCons.Text = "0.0"; }
                if (lblValTotTerr.Text.Trim() == "") { lblValTotTerr.Text = "0.0"; }
                if (lblValTotCons.Text.Trim() == "") { lblValTotCons.Text = "0.0"; }
                if (lblSupConsCom.Text.Trim() == "") { lblSupConsCom.Text = "0.0"; }
                if (lblSupConsPriv.Text.Trim() == "") { lblSupConsCom.Text = "0.0"; }
                TERRENO1 = Convert.ToDouble(rdr["STERRPROPT"].ToString().Trim());
                TERRENO2 = Convert.ToDouble(rdr["STERRCOMT"].ToString().Trim());
                TERRENO3 = Convert.ToDouble(rdr["VTERRPROPT"].ToString().Trim());
                TERRENO4 = Convert.ToDouble(rdr["VTERRCOMT"].ToString().Trim());
                TERRENO5 = TERRENO3 + TERRENO4;
                //////
                CONSTRUCCION1 = Convert.ToDouble(rdr["SCONSPROPT"].ToString().Trim()); //SUP CONS 
                CONSTRUCCION2 = Convert.ToDouble(rdr["SCONSCOMT"].ToString().Trim()); //SUP CONS COM
                CONSTRUCCION3 = Convert.ToDouble(rdr["VTERRPROPT"].ToString().Trim()); //VALOR CONS P
                CONSTRUCCION4 = Convert.ToDouble(rdr["VCONSPROPT"].ToString().Trim()); //VALOR CONS C
                CONSTRUCCION5 = CONSTRUCCION3 + CONSTRUCCION4;
                //COMENTARIO 
                lblSupTerrPriv.Text = String.Format("{0:#,##0.00}", TERRENO1);
                lblSupTerrComun.Text = String.Format("{0:#,##0.00}", TERRENO2);
                lblValTerrPriv.Text = String.Format("{0:#,##0.00}", TERRENO3);
                lblValTerrCom.Text = String.Format("{0:#,##0.00}", TERRENO4);
                lblValTotTerr.Text = String.Format("{0:#,##0.00}", TERRENO5);
                lblValTotCons.Text = String.Format("{0:#,##0.00}", CONSTRUCCION5);
                lblSupConsPriv.Text = rdr["SCONSPROPT"].ToString().Trim();
                lblSupConsCom.Text = rdr["SCONSCOMT"].ToString().Trim();
                lblValorConsPriv.Text = rdr["VCONSPROPT"].ToString().Trim();
                lblValConsCom.Text = rdr["VCONSCOMT"].ToString().Trim();
                lblSupConsPriv.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblSupConsPriv.Text.Trim()));
                lblSupConsCom.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblSupConsCom.Text.Trim()));
                lblValorConsPriv.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValorConsPriv.Text.Trim()));
                lblValConsCom.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValConsCom.Text.Trim()));
                lblValor.Text = rdr["NVALORFISCT"].ToString().Trim();
                lblValor.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(lblValor.Text.Trim()));
                lblObservaciones.Text = rdr["COBSPROP"].ToString().Trim().ToUpper();
            }
            con.cerrar_interno();
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT LATITUD, LONGITUD";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM SONG_GEOLOCALIZACION";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona      = " + ZONA_M;  //Se cocatena la zona que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana   = " + MANZANA_M;  //Se cocatena la manzana que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Lote      = " + LOTE_M;  //Se cocatena el lote que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND DEPTO     = '" +  DEPTO_M + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND EDIFICIO  = '" + EDIFICIO_M + "'";
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
            ///
            con.cerrar_interno();

            //VER SÍ ESTÁ BLOQUEADA, JALAMOS EL COMENTARIO DE LA TBALA DE BLOQUEO
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT COMENTARIO ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    FROM BLOQCVE_2";
            con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE Zona      = " + ZONA_M;  //Se cocatena la zona que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND Manzana   = " + MANZANA_M;  //Se cocatena la manzana que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND Lote      = " + LOTE_M;  //Se cocatena el lote que se mande 
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND DEPTO     = '" + DEPTO_M + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "     AND EDIFICIO  = '" + EDIFICIO_M + "'";
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();
            while (con.leer_interno.Read())
            {
                //txtInfoBloqueo.Text = con.leer_interno[0].ToString();
                lblInfoBloqueo.Text = con.leer_interno[0].ToString().Trim().ToUpper();
                lblInfoBloqueo.Visible = true;
            }
            ///
            con.cerrar_interno();
            ////OBTENEEMOS LA LATITUD Y LONGITUD PARA MOSTRARLA EN EL MAPA 

            if (string.IsNullOrWhiteSpace(lblLatitud.Text) || string.IsNullOrWhiteSpace(lblLongitud.Text))
            {
                btnMaps.Enabled = false;
                latitud = 19.262174; //19.262174;
                longitud = -99.5330638; //99.5330638
                gMapControl1.Visible = false;
            }
            else
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
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            btnConsulta.Enabled = false;
            ///CÓDIGO REVISAR EL ESE PARA QIUE SALGA 
            if (lblInfoBloqueo.Text != "") //LO QUE REALIZA CUANDO ESTÁ DESBLOQUEADO O NO TIENE NADA 
            {
                txtInfoDesbloqueo.Visible = true;
                txtInfoDesbloqueo.Enabled = true;
                txtInfoDesbloqueo.Focus();
                btnBloquear.Visible = false;
                lblDesbloqueo.Visible = true;
                btnMaps.Enabled = true;
                lblOperacion.Text = "MOTIVO DEL DESBLOQUEO";
                lblDesbloqueo.Text = "Desbloqueo:";

                btnCandadoCerrado.Visible = true;
                btnCandadoCerrado.Enabled = true;

                //pbxBloqueo.Visible = true;
                pbxDesbloqueo.Visible = false;
                //lblBloqueo.Visible = false;
                lblDesbloqueo.Visible = true;
                lblComentario.Visible = true;
                MessageBox.Show("CLAVE CATASTRAL BLOQUEADA", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtInfoDesbloqueo.Focus(); return;

            }
            ///REVISAR PARA MANDAR EL ESE LABEL 
            else //AL MOMENTO DE PARA BLOQUEAR
            {
                lblBloq.Visible = true;
                lblBloq.Text = "Bloqueo:";
                lblDesbloqueo.Visible = false;

                btnDesbloquear.Visible = false;
                txtInfoDesbloqueo.Visible = true;
                txtInfoDesbloqueo.Enabled = true;
                lblOperacion.Text = "MOTIVO DEL BLOQUEO";
                pbxBloqueo.Visible = false;
                //pbxDesbloqueo.Visible = true;
                btnCandadoAbierto.Visible = true;
                btnCandadoAbierto.Enabled = true;

                MessageBox.Show("CLAVE CATASTRAL NO CUENTA CON BLOQUEO", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Information); txtInfoDesbloqueo.Focus(); return;
            }
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
        private void btnExcel_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnExcel, "EXPORTAR A EXCEL TODAS LAS CLAVES CATASTRALES BLOQUEADAS");
        }
        private void btnMaps_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnMaps, "ABRIR GOOGLE MAPS PARA VER LAS COORDENADAS");
        }
        private void btnCandadoAbierto_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCandadoCerrado, "BLOQUEAR CLAVE CATASTRAL");
        }
        private void btnCandadoCerrado_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCandadoCerrado, "DESBLOQUEAR CLAVE CATASTRAL");
        }
        /////////////////////////////////////////////////////////////////////////
        //// ------------------------------- BOTONES 
        /////////////////////////////////////////////////////////////////////////
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BusquedaGeneral();
            lblHistorial.Text = "CLAVES CATASTRALES BLOQUEADAS";
            btnExcel.Enabled = true;
            //LO DEL MAPS 
            gMapControl1.Visible = false;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
            gMapControl1.Position = new GMap.NET.PointLatLng(19.262174, -99.5330638); //coordenadas de San Mateo Atenco
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 15;
            gMapControl1.AutoScroll = true;
            btnBuscar.Enabled = false;
            btnBuscarClave.Enabled = false;
            btnNuevo.Enabled = false;
            btnCancela.Enabled = true;
            btnSalida.Enabled = false;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            inicio();
        }
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta();
        }
        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            exportarExcel();
        }
        private void btnMaps_Click(object sender, EventArgs e)
        {
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
        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE DESBLOQUEAR LA CLAVE CATASTRAL?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (resp == DialogResult.Yes)
            {
                Desbloqueo();
            }
        }
        private void btnBloquear_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE BLOQUEAR LA CLAVE CATASTRAL?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (resp == DialogResult.Yes)
            {
                Bloqueo();
            }
        }
        private void btnCandadoCerrado_Click(object sender, EventArgs e)
        {
            btnCandadoCerrado.Visible = true;
            btnCandadoAbierto.Visible = false;

            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE DESBLOQUEAR LA CLAVE CATASTRAL?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (resp == DialogResult.Yes)
            {
                Desbloqueo();
            }
        }

        private void btnCandadoAbierto_Click(object sender, EventArgs e)
        {
            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE BLOQUEAR LA CLAVE CATASTRAL?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (resp == DialogResult.Yes)
            {
                Bloqueo();
            }
        }
        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
            //se posicionan en el txt de la latitud y longitud
            lblLatitud.Text = lat.ToString();
            lblLongitud.Text = lng.ToString();
        }
    }
}
