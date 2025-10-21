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
using Form = System.Windows.Forms.Form;

namespace SMACatastro.catastroRevision
{
    public partial class frmCatastroCambioNombre : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();
        int ZONA, MANZANA, LOTE, validacionCambio, desbloqueo, CRUD, OPERACIONBLOQUEODESBLOQUEO, AREAOPERACION, verificar = 0;
        string EDIFICIO, DEPTO, usoSuelo, OBSERVACION, USUARIO = "";
        double TERRENO1, TERRENO2, TERRENO3, TERRENO4, TERRENO5, latitud, longitud = 0.0;
        double CONSTRUCCION1, CONSTRUCCION2, CONSTRUCCION3, CONSTRUCCION4, CONSTRUCCION5 = 0.0;
        public frmCatastroCambioNombre()
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


        private void frmCatastroCambioNombre_Load(object sender, EventArgs e)
        {
            btnNuevo.Focus();
            lblUsuario.Text = "Usuario: " + Program.nombre_usuario.Trim();
            limpiartodo();
        }

        private void frmCatastroCambioNombre_Activated(object sender, EventArgs e)
        {
            btnNuevo.Focus();
        }

        //////////////////////////////////////////////CÓDIGO PARA PONER EL RELOJ EN EL FORMULARIO, SE NECESITA AGREGAR UN TIMER Y ASOCIAR ESTE MÉTODO EN SUS EVENTOS 
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
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
            txtInfoBloqueo.Text = "";
            txtInfoBloqueo.Enabled = false;
            txtNuevoNombre.Text = "";
            txtNuevoNombre.Enabled = false;
            txtInfoBloqueo.Visible = false;
            txtNuevoNombre.Visible = false;
            //deshabilitar cajas 
            btnNuevo.Enabled = true;
            btnConsulta.Enabled = false;
            btnSalida.Enabled = true;
            btnMaps.Enabled = false;
            btnBloquear.Visible = true;
            btnDesbloquear.Visible = true;
            btnBloquear.Enabled = false;
            btnDesbloquear.Enabled = false;
            btnBloquear.Visible = false;
            btnDesbloquear.Visible = false;
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
            lblDesbloqueo.Visible = false;

            //lblOperacion.Text = "MOTIVOS DE OPERACIÓN";
            //lblHistorial.Text = "HISTORIAL DE BLOQUEOS";

            //LIMPIAR Y DESHABILITAR ESTA COSA
            ////limpiar el datagrid view    
            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            ///Limpiar lo del controlador del map 
            gMapControl1.Visible = false;
            btnBuscarClave.Enabled = true;

            btnCambiarNombre.Visible = false;
            btnNuevo.Focus();
            lbllNombreAnterior.Text = "";
            lbllNombreAnterior.Visible = false;
            ////estos labels son para las flechas que aparezcan 
            label5.Visible = false;
            label10.Visible = false;
            label28.Visible = false;
            lblNombreActual.Visible = false;
        }
        /////////////////////////////////////////////////////////
        /////MÉTODO PARA CAMBIAR DE COLORES LAS CAJAS DE TEXTO 
        ////////////////////////////////////////////////////////
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
                case 5: txtNuevoNombre.BackColor = Color.Yellow; break;
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
                case 5: txtNuevoNombre.BackColor = Color.White; break;
            }
        }
        ////////////////////////////////////////////////////
        //////////// -- BUSCAR INVOCAR A LA OTRA PANTALLA
        ////////////////////////////////////////////////////
        void Consulta() //Método de la consulta 
        {
            // VALIDAR BLOQUEO DE AMBAS 
            //CATASTRO TESORERIA , LISTO 
            //VALIDAR QUE LAS CAJAS DE TEXTO NO ESTÉN VACÍAS
            if (txtZona.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN LA ZONA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtZona.Focus(); return; }
            if (txtManzana.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN LA MANZANA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtManzana.Focus(); return; }
            if (txtLote.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN EL LOTE", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtLote.Focus(); return; }

            //VALIDAR QUE LAS CAJAS DE TEXTO NO ESTÉN VACÍAS Y 2 CARACTERES O 4, SEGÚN SEA EL CASO PARA EDIFICIO, Y DEPTO 
            if (txtEdificio.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BÚSUQEDA SIN EL EDIFICIO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("NECESITAS COLOCAR DOS CARACTERES EN EL EDIFICIO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtEdificio.Focus(); return; }
            if (txtDepto.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR UNA BUSQUEDA SIN EL DEPTO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("NECESITAS COLOCAR CUATRO CARACTERES EN EL DEPARTAMENTO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtDepto.Focus(); return; ; }

            //CONSULTAR SI ESTÁ BLOQUEADA LA CLAVE CATASTRAL 
            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE POR CATASTRO
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS";     //Se realiza esta consulta solo con un dato puesto que solo queremos ver un dato 
                con.cadena_sql_interno = con.cadena_sql_interno + "           (";
                con.cadena_sql_interno = con.cadena_sql_interno + "            SELECT ZONA";     //Se realiza esta consulta solo con un dato puesto que solo queremos ver un dato 
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE ESTADO    = " + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + Program.municipioN;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Manzana   = " + Convert.ToInt32(txtManzana.Text.Trim());  //Se cocatena la manzana que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND Lote      = " + Convert.ToInt32(txtLote.Text.Trim());  //Se cocatena el lote que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO  = '" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO     = '" + txtDepto.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "            )";
                con.cadena_sql_interno = con.cadena_sql_interno + "         BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "          SELECT Malcolm = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "         END";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT Malcolm = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     END";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    var existe = con.leer_interno[0].ToString();
                    verificar = Convert.ToInt32(existe);
                }
                con.cerrar_interno();
                if (verificar == 1) //en el caso de que sea 1 del resultado en la consulta == existe 
                {
                    MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA POR CATASTRO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZona.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA DE BLOQUEO POR CATASTRO " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE POR TESORERÍA 
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (";
                con.cadena_sql_interno = con.cadena_sql_interno + "SELECT ZONA"; //Se realiza esta consulta solo con un dato puesto que solo queremos ver un dato 
                con.cadena_sql_interno = con.cadena_sql_interno + "               FROM BLOQCVE";
                con.cadena_sql_interno = con.cadena_sql_interno + "              WHERE ESTADO    = " + Program.PEstado;
                con.cadena_sql_interno = con.cadena_sql_interno + "                AND MUNICIPIO = " + Program.municipioN;
                con.cadena_sql_interno = con.cadena_sql_interno + "                AND Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "                AND Manzana   = " + Convert.ToInt32(txtManzana.Text.Trim());  //Se cocatena la manzana que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "                AND Lote      = " + Convert.ToInt32(txtLote.Text.Trim());  //Se cocatena el lote que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "                AND EDIFICIO  = '" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "                AND DEPTO     = '" + txtDepto.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "           )";
                con.cadena_sql_interno = con.cadena_sql_interno + "         BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "          SELECT Malcolm = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "         END";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT Malcolm = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     END";
                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    var existe = con.leer_interno[0].ToString();
                    verificar = Convert.ToInt32(existe);
                }
                con.cerrar_interno();
                if (verificar == 1) //si es en 1, significa que está bloqueada por lo que lo indica  
                {
                    MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA POR TESORERIA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtZona.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA DE BLOQUEO POR TESORERÍA:" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
            //CONSULTA PARA SACAR EL HISTORIAL DE CAMBIO DE NOMBRE DE UNA CLAVE CATASTRAL, SE LLENA UN DATAGRIDVIEW CON LA TABLA HPROPIEDADES
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT H.ESTADO, H.MUNICIPIO, H.ZONA, H.MANZANA, H.LOTE, H.EDIFICIO, H.DEPTO, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "         H.PMNPROP 'NOMBRE DEL PROPIETARIO', H.HORAMOD 'HORA DE LA MODIFICACION',";
                con.cadena_sql_interno = con.cadena_sql_interno + "         H.FECMOD 'FECHA DE LA MODIFICACION', H.USRMOD 'USUARIO'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM HPROPIEDADES H"; //catastro
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE H.ESTADO    = " + Program.PEstado; //variable del program que toma estado 
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.MUNICIPIO = " + Program.municipioN; //variable del municipio que se toma el program
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.Zona      = " + Convert.ToInt32(txtZona.Text.Trim());  //Se cocatena la zona que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.Manzana   = " + Convert.ToInt32(txtManzana.Text.Trim());  //Se cocatena la manzana que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.Lote      = " + Convert.ToInt32(txtLote.Text.Trim());  //Se cocatena el lote que se mande 
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.EDIFICIO  = '" + txtEdificio.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.DEPTO     = '" + txtDepto.Text.Trim() + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND H.OPERAMOD  = 'CAMBIO NOMBRE'"; //Y SOLO VAMOS A AGREGAR A LA CONSULTA LOS CAMBIOS DE NOMBRE  
                con.cadena_sql_interno = con.cadena_sql_interno + "ORDER BY H.HORAMOD DESC"; //ESTA LÍNEA ES PARA QUE TODOS LOS REGISTROS SE ORDNEN DE FORMA DESC
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                SqlDataAdapter daa = new SqlDataAdapter(con.cmd_interno); //SQL adaptador y haces uno nuevo                                                                                                                                 
                DataTable grid_table = new DataTable(); //Crear nueva tabla
                daa.Fill(grid_table); //método para llenar el datagrid
                dgResultado.DataSource = grid_table; //de qué se va a alimentar la caja 
                con.leer_interno = con.cmd_interno.ExecuteReader();
                ////CERRAR LA CONEXIÓN 
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA DEL HISTORIAL DE CAMBIOS DE NOMBRE:" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                con.cerrar_interno();
                return;

            }
            //SOLO PARA EL DATAGRID VIEW 
            lblHistorial.Text = "HISTORIAL DE CAMBIOS DE NOMBRE";
            //DAR FORMATO AL DATAGRID VIEW CON ESPACIOS DE COLUMNAS, PARA LA CELDA, ETC
            dgResultado.Columns[0].Width = 70; //estado 
            dgResultado.Columns[1].Width = 80; // municipio
            dgResultado.Columns[2].Width = 60; //zona
            dgResultado.Columns[3].Width = 70; //manzana
            dgResultado.Columns[4].Width = 60; //lote 
            dgResultado.Columns[5].Width = 60; //edificio
            dgResultado.Columns[6].Width = 60; //depto
            dgResultado.Columns[7].Width = 260; //nombre del propietario 
            dgResultado.Columns[8].Width = 260; //hora
            dgResultado.Columns[9].Width = 260; //fecha
            dgResultado.Columns[10].Width = 90; //usuario 

            dgResultado.EnableHeadersVisualStyles = false; // Desactiva estilos predeterminados
            dgResultado.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 54, 151); //COLOR DEL ENCABEZADO DEL DATAGRID VIEW 
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

            //Sirve para quitar una fila que se hace al inicio del datagridview
            dgResultado.RowHeadersVisible = false;


            try
            {
                //////PROCEDIMIENTO ALMACENADO PARA CONSULTAR LA INFORMACIÓN DE LA CLAVE CATASTRAL 
                con.conectar_base_interno();
                con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                con.open_c_interno();
                SqlCommand cmd = new SqlCommand("SONGSP_CONSULTABLOQUEO", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar, cuya funcion solo es darnos datos 
                cmd.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
                cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 2).Value = Program.municipioN;
                cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = Convert.ToInt32(txtZona.Text.ToString());
                cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = Convert.ToInt32(txtManzana.Text.ToString());
                cmd.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = Convert.ToInt32(txtLote.Text.ToString());
                cmd.Parameters.Add("@EDIFICIO", SqlDbType.Char, 2).Value = txtEdificio.Text.Trim();
                cmd.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = txtDepto.Text.Trim();
                cmd.Parameters.Add("@COMENTARIO", SqlDbType.VarChar, 100).Value = "";
                cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 30).Value = Program.nombre_usuario;
                cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string valor = rdr[4].ToString(); //Convertimos la posición 4 del procedimiento almacenado a una cadena de texto 
                    if (valor == string.Empty) //en caso que sea vacio el valor o la cadena esté sin datos 
                    {
                        MessageBox.Show("NO EXISTE INFORMACIÓN CON ESA CLAVE CATASTRAL", "¡INFORMACIÓN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        con.cerrar_interno();
                        limpiartodo();
                        gMapControl1.Visible = false;
                        btnMaps.Enabled = false;
                        btnNuevo.Focus();
                        return;
                    }
                    //es lo que va a realizar si el valor no es vacío
                    lblTitular.Text = rdr["PMNPROPT"].ToString().Trim();
                    lblDomicilio.Text = rdr["DOMFIST"].ToString().Trim().ToUpper();
                    lblCalle.Text = rdr["NOMCALLET"].ToString().Trim();
                    lblColonia.Text = rdr["NOMCOLT"].ToString().Trim();
                    lblUsoDeSuelo.Text = rdr["DESCRUSOT"].ToString().Trim();
                    lblEntCalle.Text = rdr["ENTCALLET"].ToString().Trim();
                    lblYCalle.Text = rdr["YCALLET"].ToString().Trim();

                    if (lblValTerrPriv.Text.Trim() == "") { lblValTerrPriv.Text = "0.0"; } //en caso de que sea vacio, que lo ponga en 0 
                    if (lblValorConsPriv.Text.Trim() == "") { lblValorConsPriv.Text = "0.0"; } //en caso de que sea vacio, lo pone en 0 
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

                    lbllNombreAnterior.Visible = true;
                    lbllNombreAnterior.Text = rdr["PMNPROPT"].ToString().Trim().ToUpper();
                }

                //CERRAR LA CONEXIÓN 
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar el proceso de consulta/bloq" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }


            ///esos label son para las fechas que ponemos de nombre actual a viejo LOS COLORES 
            label5.Visible = true;
            label10.Visible = true;
            label28.Visible = true;
            lblDesbloqueo.Visible = true;
            txtNuevoNombre.Visible = true;
            txtNuevoNombre.Enabled = true;
            txtNuevoNombre.Focus();
            lblNombreActual.Visible = true;
            btnCambiarNombre.Visible = true;
            txtZona.Enabled = false;
            txtManzana.Enabled = false;
            txtLote.Enabled = false;
            txtDepto.Enabled = false;
            txtEdificio.Enabled = false;
            btnConsulta.Enabled = false;

            try
            {
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
                    if (con.leer_interno[0].ToString().Trim() != "") //SE VALIDA QUE NO ESTÉ VACIO 
                    {
                        lblLatitud.Text = con.leer_interno[0].ToString().Trim(); //SE COLOCA LA LATITUD EN SU PROPIO LABEL 
                        lblLongitud.Text = con.leer_interno[1].ToString().Trim(); //SE COLOCA LA LONGITUD EN SU PROPIO LABEL 
                    }
                }
                ///CERRAR LA CADENA DE LA CONEXIÓN 
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar consulta de coords " + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }

            ///OBTENER LA GEOLOCALIZACIÓN, ES DECIR LATITUD Y LONGITUD DE LA SONG_GEOLOCALIZACIÓN

            //EN CASO DE QUE SEA VACIO LA LATITUD Y LA LONGITUD, DESHABILITAMOS EL BOTÓN DE MAPAS Y NO SÉ VE EL MAPA. 
            if (string.IsNullOrWhiteSpace(lblLatitud.Text) || string.IsNullOrWhiteSpace(lblLongitud.Text))
            {
                btnMaps.Enabled = false;
                gMapControl1.Visible = false;
            }
            else //EN CASO DE QUE NO SEA VACÍO, SE HABILITA EL BOTÓN Y SE MUESTRA LA INFORMACIÓN DEL VISOR 
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
        }
        void cambiarnombre()
        {
            if (txtNuevoNombre.Text == "") { MessageBox.Show("NO SE PUEDE REALIZAR EL CAMBIO DE NOMBRE SIN TEXTO", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNuevoNombre.Focus(); return; }
            if (txtNuevoNombre.Text.Length < 5) { MessageBox.Show("NO PUEDE SER TAN CORTO EL NUEVO NOMBRE DE LA CLAVE CATASTRAL", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtNuevoNombre.Focus(); return; }
            try
            {
                ZONA = Convert.ToInt32(txtZona.Text.ToString());
                MANZANA = Convert.ToInt32(txtManzana.Text.ToString());
                LOTE = Convert.ToInt32(txtLote.Text.ToString());
                EDIFICIO = txtEdificio.Text.Trim();
                DEPTO = txtDepto.Text.Trim();
                USUARIO = Program.nombre_usuario;
                OBSERVACION = "CAMBIO NOMBRE"; //SIEMPRE SE MANDA ESTE  DATO , PARA QUE LA OBSERVACION SEA SIEMPRE LA MISMA 
                con.conectar_base_interno();
                con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                con.open_c_interno();
                SqlCommand cmd2 = new SqlCommand("SONGSP_CAMBIODENOMBREPROPIETARIO", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
                cmd2.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                cmd2.Parameters.Add("@ESTADO", SqlDbType.Int, 2).Value = Program.PEstado;
                cmd2.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 2).Value = Program.municipioN;
                cmd2.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = ZONA;
                cmd2.Parameters.Add("@MANZANA", SqlDbType.Int, 3).Value = MANZANA;
                cmd2.Parameters.Add("@LOTE", SqlDbType.Int, 2).Value = LOTE;
                cmd2.Parameters.Add("@EDIFICIO", SqlDbType.Char, 2).Value = EDIFICIO;
                cmd2.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = DEPTO;
                cmd2.Parameters.Add("@NOMPROP", SqlDbType.Char, 100).Value = txtNuevoNombre.Text.Trim(); //nombreprop
                cmd2.Parameters.Add("@OBSERVACION", SqlDbType.NChar, 250).Value = OBSERVACION;
                cmd2.Parameters.Add("@USUARIO", SqlDbType.VarChar, 30).Value = USUARIO;
                cmd2.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd2.Connection = con.cnn_interno;
                cmd2.ExecuteNonQuery();
                validacionCambio = Convert.ToInt32(cmd2.Parameters["@VALIDACION"].Value);
                con.cerrar_interno();
                string clavecatastro = Program.municipioN + "-" + ZONA + "-" + MANZANA + "-" + LOTE + "-" + EDIFICIO + "-" + DEPTO; //OBTENGO LA CLAVE CATASTRAL EN UNA SOLA VARIABLE PARA MOSTRARLA EN EL MESSAGEBOX
                if (validacionCambio == 1) //en caso de generarse de forma correcta, se realizan las acciones colocadas en el procedimiento  
                {
                    MessageBox.Show("SE CAMBIÓ CORRECTAMENTE EL NOMBRE A LA CLAVE CATASTRAL: " + " " + clavecatastro + "", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiartodo();
                    return;
                }
                else ///AL ENTRAR AQUÍ ES PORQUE OCURRIÓ UN ERROR AL CAMBIAR EL NOMBRE
                {
                    MessageBox.Show("OCURRIÓ UN ERROR AL CAMBIAR DE NOMBRE EN LA CLAVE CATASTRAL: " + " " + clavecatastro, "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    limpiartodo();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar el proceso de cambio de nombre" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }

            //SACAR LOS VALORES Y CONVERTIRLOS 
          
        }
        ///////////////////////////////////////////////////////
        //COLOCAR EN AMARILLO AL ENTRAR EN LA CAJA DE TEXTO
        ///////////////////////////////////////////////////////
        private void txtZona_Enter(object sender, EventArgs e)
        {
            cajasamarillas(0); //OCUPANDO EL MÉTODO GENERADO DEL MISMO NOMBRE, SE LE COLOCA SOLO LA POSICIÓN 
        }
        private void txtManzana_Enter(object sender, EventArgs e)
        {
            cajasamarillas(1);
        }
        private void txtLote_Enter(object sender, EventArgs e)
        {
            cajasamarillas(2);
        }
        private void txtEdificio_Enter(object sender, EventArgs e)
        {
            cajasamarillas(3);
        }
        private void txtDepto_Enter(object sender, EventArgs e)
        {
            cajasamarillas(4);
        }
        private void txtNuevoNombre_Enter(object sender, EventArgs e)
        {
            cajasamarillas(5);
        }
        /////////////////////////////////////////////////////
        //COLOCAR EN BLANCO AL SALIR EN LA CAJA DE TEXTO
        /////////////////////////////////////////////////////
        private void txtZona_Leave(object sender, EventArgs e)
        {
            cajasblancas(0);
        }
        private void txtManzana_Leave(object sender, EventArgs e)
        {
            cajasblancas(1);
        }
        private void txtLote_Leave(object sender, EventArgs e)
        {
            cajasblancas(2);
        }
        private void txtEdificio_Leave(object sender, EventArgs e)
        {
            cajasblancas(3);
        }
        private void txtDepto_Leave(object sender, EventArgs e)
        {
            cajasblancas(4);
        }
        private void txtNuevoNombre_Leave(object sender, EventArgs e)
        {
            cajasblancas(5);
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

        private void PanelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////
        //QUE SE CAMBIE DE CAJA DE TEXTO AL CUMPLIR UNA CONDICIÓN DE UNA LONGITUD DE CARACTERES 
        ////////////////////////////////////////////////////////////////////////////////////////
        private void txtZona_TextChanged(object sender, EventArgs e)
        {
            if (txtZona.Text.Length == 2) //validar que la caja de texto que se está ingresando datos, cumpla con la condicion deseada 
            {
                txtManzana.Focus(); //colocar el foco en la caja de texto que se desea 
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
        /////////////////////////////////////////////////////////////////////////////////////////
        //SOLO ACEPTAR NÚMEROS EN LAS CAJAS DE TEXTO QUE SE NECESITE 
        /////////////////////////////////////////////////////////////////////////////////////////
        private void txtZona_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e); //Hacemos uso de la clase utilerias 
        }
        private void txtManzana_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e); //uso de la clase utilerias, solo números para esa caja de texto 
        }
        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }
        private void btnConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Consulta();
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        ////////////////PONER UN TEXTO EN LOS BOTONES ES EL TOOLTIP  
        /////////////////////////////////////////////////////////////////////////////////////////
        private void btnBuscarClave_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip(); //INSTANCIA 
            toolTip.SetToolTip(btnBuscarClave, "BÚSQUEDA GENERAL DE CLAVES CATASTRALES"); //ES EL TEXTO QUE SE APARECE AL PONER EL MOUSE SOBRE EL BOTÓN
        }
        private void btnNuevo_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnNuevo, "EMPEZAR UN NUEVO PROCESO");
        }
        private void btnCancela_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCancela, "CANCELAR Y LIMPIAR PROCESO");
        }
        private void btnSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnSalida, "SALIR DE LA PANTALLA");
        }
        private void btnConsulta_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnConsulta, "CONSULTA");
        }
        private void btnMaps_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnMaps, "ABRIR GOOGLE MAPS");
        }
        /////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////BOTONES DE LA PANTALLA 
        /////////////////////////////////////////////////////////////////////////////////////////
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            inicio(); //método de inicio para habilitar cajas de texto y botones 
        }
        private void btnConsulta_Click(object sender, EventArgs e)
        {
            Consulta(); //método que se utiliza para consultar la información 
        }
        private void btnCancela_Click(object sender, EventArgs e)
        {
            limpiartodo(); //limpiar todos los datos / botones que se encuentran la pantalla 
        }
        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close(); //Cerrar el formulario actual 
        }
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Para minimizar el formulario 
        }
        private void btnMaps_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblLatitud.Text) || string.IsNullOrWhiteSpace(lblLongitud.Text)) //Validar que la clave catastral tenga datos de coordenadas para que abra GMaps
            {
                MessageBox.Show("POR FAVOR, INGRESE LA LATITUD Y LONGITUD ANTES DE ABRIR GOOGLE MAPS.", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string latitud = lblLatitud.Text.Trim(); //convertimos a cadena de texto la latitud 
            string longitud = lblLongitud.Text.Trim(); //convertimos a cadena de texto la longitud 
            Process.Start($"https://www.google.com/maps?q={latitud},{longitud}"); //Abrir la página de google maps con las coordenadas obtenidas en las etiquetas 
        }
        private void btnCambiarNombre_Click(object sender, EventArgs e) //el botón de cambiar el nombre 
        {
            //Solicitar que el usuario confirme el cambio de nombre 
            DialogResult resp = MessageBox.Show("¿ESTÁS SEGURO DE CAMBIAR EL NOMBRE A LA CLAVE CATASTRAL?", "¡INFORMACIÓN!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resp == DialogResult.Yes)
            {
                cambiarnombre();
            }
        }
    }
}
