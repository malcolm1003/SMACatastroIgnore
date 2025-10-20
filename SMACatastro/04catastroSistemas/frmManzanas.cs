using AccesoBase;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Utilerias;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;

namespace SMACatastro
{
    public partial class frmManzanas : Form
    {
        int MOVIMIENTO = 0;
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();
        //string zona_calle, cod_calle, nombre_calle, vialidad_calle;
        int validacion = 0, colonias = 0, localidades = 0, areas_homogeneas = 0;
        int numero_zona, NUMMAY = 0;
        string zona_crud, manzana_crud, colonia_crud, localidad_crud, area_homogenea_crud, uso_crud, clase_crud, cate_const_crud;
        ////////////////////////////////////////////////////////////
        ///////////////// -------PARA ARRASTRAR EL PANEL 
        ////////////////////////////////////////////////////////////
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public frmManzanas()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RECUERDE QUE PUEDE GENERAR UNA NUEVA MANZANA BASANDOSE EN CUALQUIER REGISTRO DE LA TABLA, DANDOLE DOBLE CLIC", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PNLNEW.Enabled = true; // Habilitar el panel para crear un nuevo concept
            MOVIMIENTO = 1; // Indica que se está creando un nuevo
            txtNoZonaN.Enabled = true; // Deshabilitar el botón de nuevo para evitar duplicados
            cboMzaN.Enabled = false; // Deshabilitar el campo de texto para el número de la manzana, ya que se generará automáticamente
            cboColoniaN.Enabled = false; // Habilitar el campo de texto
            txtNoZonaN.Focus(); // Enfocar el campo de texto para ingresar 
            btnEditar.Enabled = false; // Deshabilitar el botón de editar
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar 
            btnGuardar.Enabled = true; // Habilitar el botón de guardar
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo para evitar duplicados
            btnCancelar.Enabled = true; // Habilitar el botón de cancelar
            btnBuscar.Enabled = true; // Habilitar el botón de buscar
            DGV_MANZANAS.Enabled = true; // 
            lbl_titulo.Text = "PROCESO DE CREACION"; // Establecer el título del formulario
            cboColoniaN.Enabled = false;
            cboLocalidadN.Enabled = false;
            cboAreasHomN.Enabled = false;
            cboCateConstN.Enabled = false;
            cboClaseN.Enabled = false;
            btnValidarZona.Enabled = true;
            cboUsoN.Enabled = false;
            btnValidarMza.Enabled = false;
            cargar_datagrid_MANZANA(); // Cargar los datos en el DataGridView 
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MOVIMIENTO = 2; // Indica que se está EDITANDO 
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar
            MessageBox.Show("SELECCIONA LA MANZANA QUE DESEA EDITAR, DANDO DOBLE CLIC DENTRO DE LA TABLA", "EDICION DE MANZANAS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lbl_titulo.Text = "PROCESO DE EDICION"; // Establecer el título del formulario
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnBuscar.Enabled = true; // Habilitar el botón de búsqueda

            cargar_datagrid_MANZANA(); // Cargar los datos en el DataGridView 
            DGV_MANZANAS.Enabled = true; // Habilitar la grilla de resultados para mostrar los  existentes
            DGV_MANZANAS.Focus(); // Enfocar el DataGridView de resultados para que el usuario pueda seleccionar a editar
        }
        private void formaInicio()
        {
            // Inicializar el formulario y cargar los datos necesarios
            MOVIMIENTO = 0; // Indica que no se está realizando ninguna acción (nuevo, eliminar o editar)
            Cbo_ZonaB.Items.Clear();
            Cbo_ZonaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            rbnZona.Checked = false; // Desmarcar el radio button 
            rbColonia.Checked = false; // Desmarcar el radio button
            rbLocalidad.Checked = false; // Desmarcar el radio button
            cboMzaB.SelectedIndex = -1; // Desmarcar cualquier selección previa en el combo box de búsqueda
            txtNoZonaN.Text = ""; // Limpiar el campo de contable

            PNLNEW.Enabled = false; // Deshabilitar el panel 
            PNLFBUSCAR.Enabled = false; // Deshabilitar el panel de búsqueda 
            Cbo_ZonaB.Enabled = false; // Deshabilitar el combo box 
            btnConsulta_bus.BackColor = Color.FromArgb(55, 61, 69);
            btnConsulta_bus.ForeColor = Color.White;
            btnBorrar.Enabled = true; // Deshabilitar el botón de borrar 
            cboMzaB.Enabled = false; // Deshabilitar el campo de búsqueda
            btnNuevo.Enabled = true; // Habilitar el botón de nuevo
            btnBuscar.Enabled = false; // Habilitar el botón de búsqueda
            DGV_MANZANAS.Enabled = false; // Habilitar el DataGridView de resultados

            btnEditar.Enabled = true;
            lbl_titulo.Text = ""; // Establecer el título del formulario

            lblNumRegistro.Text = "0";
            DGV_MANZANAS.DataSource = null; // Limpiar el DataGridView de resultados
            cboMzaN.SelectedIndex = -1; // Limpiar el campo de clave de área
            cboColoniaN.SelectedIndex = -1; // Limpiar el campo de clave de área
            cboAreasHomB.Items.Clear();
            cboAreasHomB.SelectedIndex = -1;
            cboAreasHomN.Items.Clear();
            cboAreasHomN.SelectedIndex = -1;
            cboColoniaB.Items.Clear();
            cboColoniaB.SelectedIndex = -1;
            cboColoniaN.Items.Clear();
            cboColoniaN.SelectedIndex = -1;
            cboLocalidadB.Items.Clear();
            cboLocalidadB.SelectedIndex = -1;
            cboLocalidadN.Items.Clear();
            cboLocalidadN.SelectedIndex = -1;
            DGV_AREAS_HOM.DataSource = null;
            cboCateConstN.Items.Clear();
            cboCateConstN.SelectedIndex = -1;
            cboClaseN.Items.Clear();
            cboClaseN.SelectedIndex = -1;
            cboUsoN.Items.Clear();
            cboUsoN.SelectedIndex = -1;

        }
        private void cargar_datagrid_MANZANA()
        {
            // Cargar los datos en el DataGridView de MANZANAS
            try
            {
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT M.Zona ,M.Manzana, M.Colonia, C.NomCol, M.Localidad,L.NomLoc,";
                con.cadena_sql_interno = con.cadena_sql_interno + "          M.AreaHom, A.DescAreaHo,M.Uso, M.ClaseConst, M.CategConst, T.DescrClCat   ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM MANZANAS M, COLONIAS C, LOCALIDADES L, AREASH A, TIPO_CONST T";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Where M.Colonia   = C.Colonia";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.Localidad = L.Localidad ";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.AreaHom = A.AreaHom  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.Uso = T.Uso";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.ClaseConst = T.ClaseConst";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.CategConst = t.CategConst ";
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND T.AnioVigVUC =" + Program.añoActual;
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND A.AnioVigVUS =" + Program.añoActual;
                con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY M.Zona, M.Manzana  ";
                //llenamos la grilla con los resultados de la consulta
                DataTable LLENAR_GRID_1 = new DataTable();
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO
                {
                    MessageBox.Show("NO SE ENCONTRO DATOS DE LA BUSQUEDA", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DGV_MANZANAS.DataSource = LLENAR_GRID_1;
                    con.cerrar_interno();
                    DGV_MANZANAS.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    DGV_MANZANAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DGV_MANZANAS.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                    DGV_MANZANAS.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    foreach (DataGridViewColumn columna in DGV_MANZANAS.Columns)
                    {
                        columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    // Configuración de selección
                    DGV_MANZANAS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // Deshabilitar edición
                    DGV_MANZANAS.ReadOnly = true;
                    // Estilos visuales
                    DGV_MANZANAS.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    DGV_MANZANAS.DefaultCellStyle.SelectionForeColor = Color.Black;

                    // Configurar todas las columnas para que no se puedan redimensionar
                    DGV_MANZANAS.AllowUserToResizeColumns = false;

                    DGV_MANZANAS.Columns[0].Width = 45;                         // ZONA         
                    DGV_MANZANAS.Columns[1].Width = 45;                         // MANZANA
                    DGV_MANZANAS.Columns[2].Width = 30;                        // NUMERO COLONIA
                    DGV_MANZANAS.Columns[3].Width = 230;                        // NOMBRE COLONIA
                    DGV_MANZANAS.Columns[4].Width = 30;                        // NUMERO LOCALIDAD
                    DGV_MANZANAS.Columns[5].Width = 245;                        // NOMBRE LOCALIDAD
                    DGV_MANZANAS.Columns[6].Width = 42;                         // AREA HOMOGENEA
                    DGV_MANZANAS.Columns[7].Width = 180;                         // DESCRIPCION
                    DGV_MANZANAS.Columns[8].Width = 40;                         // USO
                    DGV_MANZANAS.Columns[9].Width = 50;                         // CLASE CONSTRUCCION
                    DGV_MANZANAS.Columns[10].Width = 50;                         // CATEGORIA CONSTRUCCION
                    DGV_MANZANAS.Columns[11].Width = 210;                         // DESCRIPCION


                    DGV_MANZANAS.Columns[0].Name = "ZONA";                      // ZONA          
                    DGV_MANZANAS.Columns[1].Name = "MANZANA";                   // MANZANA
                    DGV_MANZANAS.Columns[2].Name = "NUMERO COLONIA";                // NUMERO COLONIA
                    DGV_MANZANAS.Columns[3].Name = "COLONIA";                   // NOMBRE COLONIA
                    DGV_MANZANAS.Columns[4].Name = "NUMERO LOCALIDAD";              // NUMERO LOCALIDAD 
                    DGV_MANZANAS.Columns[5].Name = "LOCALIDAD";                 // NOMBRE LOCALIDAD
                    DGV_MANZANAS.Columns[6].Name = "AREA_HOMOGENEA";            // AREA HOMOGENEA
                    DGV_MANZANAS.Columns[7].Name = "DESCRIPCION";               //DESCRIPCION
                    DGV_MANZANAS.Columns[8].Name = "USO";                       // USO
                    DGV_MANZANAS.Columns[9].Name = "CLASE_CONSTRUCCION";        //CLASE CONSTRUCCION
                    DGV_MANZANAS.Columns[10].Name = "CATEGORIA_CONSTRUCCION";   // CATEGORIA CONSTRUCCION
                    DGV_MANZANAS.Columns[11].Name = "DESCRIPCION";                         // DESCRIPCION

                    DGV_MANZANAS.Columns[0].HeaderText = "ZONA";                // ZONA          
                    DGV_MANZANAS.Columns[1].HeaderText = "MANZ";             // MANZANA
                    DGV_MANZANAS.Columns[2].HeaderText = "No";                    // NUMERO COLONIA
                    DGV_MANZANAS.Columns[3].HeaderText = "COLONIA";             // NOMBRE COLONIA
                    DGV_MANZANAS.Columns[4].HeaderText = "No";                    // NUMERO LOCALIDAD 
                    DGV_MANZANAS.Columns[5].HeaderText = "LOCALIDAD";           // NOMBRE LOCALIDAD
                    DGV_MANZANAS.Columns[6].HeaderText = "No ";               // AREA HOMOGENEA
                    DGV_MANZANAS.Columns[7].HeaderText = "AREA HOMOGENEA";         //DESCRIPCION
                    DGV_MANZANAS.Columns[8].HeaderText = "USO";                 // USO
                    DGV_MANZANAS.Columns[9].HeaderText = "CLASE CONST";         //CLASE CONSTRUCCION
                    DGV_MANZANAS.Columns[10].HeaderText = "CATE CONST";          // CATEGORIA CONSTRUCCION
                    DGV_MANZANAS.Columns[11].HeaderText = "DESCRIPCION";          // CATEGORIA CONSTRUCCION



                    DGV_MANZANAS.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                    DGV_MANZANAS.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                    DGV_MANZANAS.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_MANZANAS.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;// 

                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
            }
        }

        private void btnConsulta_bus_Click(object sender, EventArgs e)
        {
            if (Cbo_ZonaB.Text == "")
            {
                if (cboMzaB.Text == "")
                {
                    if (cboAreasHomB.Text == "")
                    {
                        if (cboColoniaB.Text == "")
                        {
                            if (cboLocalidadB.Text == "")
                            {
                                MessageBox.Show("NO SE TIENE OPCIONES DE BUSQUEDA", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }
                }
            }

            if (rbnZona.Checked == true) { if (Cbo_ZonaB.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELCCIONAR UNA ZONA", "ERROR", MessageBoxButtons.OK); Cbo_ZonaB.Focus(); return; } }
            if (rbAreaHomogenea.Checked == true) { if (cboAreasHomB.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR UN AREA HOMOGENEA", "ERROR", MessageBoxButtons.OK); cboAreasHomB.Focus(); return; } }
            if (rbLocalidad.Checked == true) { if (cboLocalidadB.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR UNA LOCALIDAD CORRECTA", "ERROR", MessageBoxButtons.OK); cboLocalidadB.Focus(); return; } }
            if (rbColonia.Checked == true) { if (cboColoniaB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA COLONIA CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); cboColoniaB.Focus(); return; } }
            if (rbManzana.Checked == true) { if (cboMzaB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL CODIGO DE LA MANZANA CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); cboMzaB.Focus(); return; } }

            // SE ARMA EL query DE BUSQUEDA
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT M.Zona ,M.Manzana, M.Colonia, C.NomCol, M.Localidad,L.NomLoc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "          M.AreaHom, A.DescAreaHo,M.Uso, M.ClaseConst, M.CategConst, T.DescrClCat   ";
            con.cadena_sql_interno = con.cadena_sql_interno + "     FROM MANZANAS M, COLONIAS C, LOCALIDADES L, AREASH A, TIPO_CONST T";
            con.cadena_sql_interno = con.cadena_sql_interno + "    Where M.Colonia   = C.Colonia";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.Localidad = L.Localidad ";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.AreaHom = A.AreaHom  ";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.Uso = T.Uso";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.ClaseConst = T.ClaseConst";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND M.CategConst = t.CategConst ";
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND T.AnioVigVUC =" + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "      AND A.AnioVigVUS =" + Program.añoActual;

            //NUMERO DE ZONA
            if (rbnZona.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "         AND M.Zona =" + Cbo_ZonaB.Text.Trim(); }
            //NUMERO DE MANZANA
            if (rbManzana.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "       AND M.Manzana =" + cboMzaB.Text.Trim(); }
            //COLONIA
            if (rbColonia.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "       AND M.Colonia =" + cboColoniaB.Text.Trim().Substring(0, 3); }
            //LOCALIDADES
            if (rbLocalidad.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "     AND  M.Localidad = " + cboLocalidadB.Text.Trim().Substring(0, 3); }
            //AREA HOMOGENEA
            if (rbAreaHomogenea.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + " AND  M.AreaHom = " + cboAreasHomB.Text.Trim().Substring(0, 3); }
            con.cadena_sql_interno = con.cadena_sql_interno + "                                   ORDER BY M.Zona, M.Manzana";
            //llenamos la grilla con los resultados de la consulta
            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO
            {
                MessageBox.Show("NO SE ENCONTRO DATOS DE LA BUSQUEDA", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DGV_MANZANAS.DataSource = LLENAR_GRID_1;
                con.cerrar_interno();
                DGV_MANZANAS.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                DGV_MANZANAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                DGV_MANZANAS.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                DGV_MANZANAS.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                foreach (DataGridViewColumn columna in DGV_MANZANAS.Columns)
                {
                    columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                // Configuración de selección
                DGV_MANZANAS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Deshabilitar edición
                DGV_MANZANAS.ReadOnly = true;
                // Estilos visuales
                DGV_MANZANAS.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                DGV_MANZANAS.DefaultCellStyle.SelectionForeColor = Color.Black;

                // Configurar todas las columnas para que no se puedan redimensionar
                DGV_MANZANAS.AllowUserToResizeColumns = false;


                DGV_MANZANAS.Columns[0].Width = 45;                         // ZONA         
                DGV_MANZANAS.Columns[1].Width = 45;                         // MANZANA
                DGV_MANZANAS.Columns[2].Width = 30;                        // NUMERO COLONIA
                DGV_MANZANAS.Columns[3].Width = 230;                        // NOMBRE COLONIA
                DGV_MANZANAS.Columns[4].Width = 30;                        // NUMERO LOCALIDAD
                DGV_MANZANAS.Columns[5].Width = 245;                        // NOMBRE LOCALIDAD
                DGV_MANZANAS.Columns[6].Width = 42;                         // AREA HOMOGENEA
                DGV_MANZANAS.Columns[7].Width = 180;                         // DESCRIPCION
                DGV_MANZANAS.Columns[8].Width = 40;                         // USO
                DGV_MANZANAS.Columns[9].Width = 50;                         // CLASE CONSTRUCCION
                DGV_MANZANAS.Columns[10].Width = 50;                         // CATEGORIA CONSTRUCCION
                DGV_MANZANAS.Columns[11].Width = 210;                         // DESCRIPCION


                DGV_MANZANAS.Columns[0].Name = "ZONA";                      // ZONA          
                DGV_MANZANAS.Columns[1].Name = "MANZANA";                   // MANZANA
                DGV_MANZANAS.Columns[2].Name = "NUMERO COLONIA";                // NUMERO COLONIA
                DGV_MANZANAS.Columns[3].Name = "COLONIA";                   // NOMBRE COLONIA
                DGV_MANZANAS.Columns[4].Name = "NUMERO LOCALIDAD";              // NUMERO LOCALIDAD 
                DGV_MANZANAS.Columns[5].Name = "LOCALIDAD";                 // NOMBRE LOCALIDAD
                DGV_MANZANAS.Columns[6].Name = "AREA_HOMOGENEA";            // AREA HOMOGENEA
                DGV_MANZANAS.Columns[7].Name = "DESCRIPCION";               //DESCRIPCION
                DGV_MANZANAS.Columns[8].Name = "USO";                       // USO
                DGV_MANZANAS.Columns[9].Name = "CLASE_CONSTRUCCION";        //CLASE CONSTRUCCION
                DGV_MANZANAS.Columns[10].Name = "CATEGORIA_CONSTRUCCION";   // CATEGORIA CONSTRUCCION
                DGV_MANZANAS.Columns[11].Name = "DESCRIPCION";                         // DESCRIPCION

                DGV_MANZANAS.Columns[0].HeaderText = "ZONA";                // ZONA          
                DGV_MANZANAS.Columns[1].HeaderText = "MANZ";             // MANZANA
                DGV_MANZANAS.Columns[2].HeaderText = "No";                    // NUMERO COLONIA
                DGV_MANZANAS.Columns[3].HeaderText = "COLONIA";             // NOMBRE COLONIA
                DGV_MANZANAS.Columns[4].HeaderText = "No";                    // NUMERO LOCALIDAD 
                DGV_MANZANAS.Columns[5].HeaderText = "LOCALIDAD";           // NOMBRE LOCALIDAD
                DGV_MANZANAS.Columns[6].HeaderText = "No ";               // AREA HOMOGENEA
                DGV_MANZANAS.Columns[7].HeaderText = "AREA HOMOGENEA";         //DESCRIPCION
                DGV_MANZANAS.Columns[8].HeaderText = "USO";                 // USO
                DGV_MANZANAS.Columns[9].HeaderText = "CLASE CONST";         //CLASE CONSTRUCCION
                DGV_MANZANAS.Columns[10].HeaderText = "CATE CONST";          // CATEGORIA CONSTRUCCION
                DGV_MANZANAS.Columns[11].HeaderText = "DESCRIPCION";          // CATEGORIA CONSTRUCCION



                DGV_MANZANAS.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                DGV_MANZANAS.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                DGV_MANZANAS.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_MANZANAS.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;// 

                int CONTEO;
                CONTEO = DGV_MANZANAS.Rows.Count - 1;
                lblNumRegistro.Text = CONTEO.ToString();
                DGV_MANZANAS.Enabled = true; // Habilitar la grilla de resultados
                

            }
            con.cerrar_interno();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            MOVIMIENTO = 3; // Indica que se está ELIMINANDO
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo
            MessageBox.Show("SELECCIONA LA MANZANA QUE DESEA ELIMINAR DENTRO DE LA TABLA DANDO DOBLE CLIC EN LA MISMA", "ELIMINACION DE MANZANA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lbl_titulo.Text = "PROCESO DE ELIMINACION"; // Establecer el título del formulario
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnBuscar.Enabled = true; // 
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar
            DGV_MANZANAS.Enabled = true; // Habilitar la grilla de resultados
            cargar_datagrid_MANZANA(); // Cargar los datos en el DataGridView 
            DGV_MANZANAS.Focus(); // Enfocar el DataGridView de resultados para que el usuario pueda seleccionar a editar
        }

        private void DGV_COLONIAS_DoubleClick(object sender, EventArgs e)
        {
            if (MOVIMIENTO == 1)//ALTA DE UNA MANZANA
            {
                //DialogResult resp = MessageBox.Show("¿DESEA CREAR UNA NUEVA MANZANA BASANDOSE EN LA SELECCIONADA?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (resp == DialogResult.Yes)
                //{
                if (DGV_MANZANAS.CurrentRow.Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Sale del método o procedimiento
                }
                enviar_datos(); // Llamar al método para enviar los datos seleccionados
                PNLNEW.Enabled = true; // Habilitar el panel 
                txtNoZonaN.Enabled = false; // Deshabilitar el campo de texto para el número de zonas, ya que se generará automáticamente
                cboLocalidadN.Enabled = true;
                cboColoniaN.Enabled = true; // Habilitar el campo de texto
                cboMzaN.Enabled = true;
                cboAreasHomN.Enabled = true;
                btnValidarMza.Enabled = false;
                DGV_AREAS_HOM.Enabled = true;
                cboCateConstN.Enabled = true;
                cboClaseN.Enabled = true;
                cboUsoN.Enabled = true;
                btn_cancelar2.Enabled = true;
                btnGuardar.Enabled = true;
                // }
            }
            else if (MOVIMIENTO == 2)//EDITANDO MANZANA
            {
                //DialogResult resp = MessageBox.Show("¿DESEA EDITAR LA MANZANA SELECCIONADA?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (resp == DialogResult.Yes)
                //{
                if (DGV_MANZANAS.CurrentRow.Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Sale del método o procedimiento
                }
                enviar_datos(); // Llamar al método para enviar los datos seleccionados
                PNLNEW.Enabled = true; // Habilitar el panel 
                txtNoZonaN.Enabled = false; // Deshabilitar el campo de texto para el número de zona, ya que se generará automáticamente
                cboLocalidadN.Enabled = true; // Habilitar el campo
                cboColoniaN.Enabled = true; // Habilitar el campo
                cboMzaN.Enabled = false; // Deshabilitar el campo
                cboAreasHomN.Enabled = true;
                btnValidarMza.Enabled = false;
                DGV_AREAS_HOM.Enabled = true;
                cboCateConstN.Enabled = true;
                cboClaseN.Enabled = true;
                cboUsoN.Enabled = true;
                btn_cancelar2.Enabled = true;
                btnGuardar.Enabled = true;
                //}
            }
            else if (MOVIMIENTO == 3)// ELIMINACION DE CALLE
            {
                //DialogResult resp = MessageBox.Show("¿DESEA REALIZAR EL PROCESO DE ELIMINACION DE LA MANZANA SELECCIONADA?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (resp == DialogResult.Yes)
                //{
                if (DGV_MANZANAS.CurrentRow.Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Sale del método o procedimiento
                }
                enviar_datos(); // Llamar al método para enviar los datos seleccionados
                PNLNEW.Enabled = true; // Habilitar el panel
                txtNoZonaN.Enabled = false; // Deshabilitar el campo 
                cboLocalidadN.Enabled = false; // Deshabilitar el campo 
                cboColoniaN.Enabled = false; // Deshabilitar el campo de texto para ingresar el código postal
                cboMzaN.Enabled = false; // Deshabilitar 
                cboAreasHomN.Enabled = false;
                DGV_AREAS_HOM.Enabled = false;
                btnValidarMza.Enabled = false;
                cboCateConstN.Enabled = false;
                cboClaseN.Enabled = false;
                cboUsoN.Enabled = false;
                btn_cancelar2.Enabled = true;
                btnGuardar.Enabled = true;
                // }
            }
            else // Si MOVIMIENTO es 0, significa que NO SE SELECCIONÓ UN PROCESO VÁLIDO
            {
                MessageBox.Show("ERROR, DEBE DE SELECCIONAR UN PROCESO, NUEVO, EDITAR O ELIMINAR.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Sale del método o procedimiento si no se ha seleccionado un proceso válido
            }

        }
        private void validar_manzana()
        {
            int verificar = 0;
            try
            {

                con.conectar_base_interno();

                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "IF EXISTS (SELECT zona";
                con.cadena_sql_interno = con.cadena_sql_interno + "     From MANZANAS ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Where ZONA = " + numero_zona + " )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT existe = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "    End";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Else";
                con.cadena_sql_interno = con.cadena_sql_interno + "    BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT existe = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "    End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    verificar = Convert.ToInt32(con.leer_interno[0].ToString());
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
            if (verificar == 1)
            {
                cboMzaN.Items.Clear();
                try
                {
                    con.conectar_base_interno();

                    con.cadena_sql_interno = "Select max(MANZANA) +10 AS MAYOR";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  FROM MANZANAS";
                    con.cadena_sql_interno = con.cadena_sql_interno + " WHERE ZONA =" + numero_zona;

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        NUMMAY = Convert.ToInt32(con.leer_interno[0].ToString());                     // colocar numero mayor de la calle a la caja de texto
                    }

                    con.cerrar_interno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    return; // Retornar false si ocurre un error
                }
                try
                {
                    con.conectar_base_interno();

                    con.cadena_sql_interno = "SELECT CONSECUTIVO  FROM FALTANTES FA ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE NOT EXISTS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  ( SELECT MANZANA FROM MANZANAS MA WHERE MA.Manzana = FA.CONSECUTIVO";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   AND MA.Zona =    " + numero_zona + ")";
                    con.cadena_sql_interno = con.cadena_sql_interno + " AND FA.CONSECUTIVO <=  " + NUMMAY;
                    con.cadena_sql_interno = con.cadena_sql_interno + "ORDER BY FA.CONSECUTIVO ";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        cboMzaN.Items.Add(Convert.ToInt32(con.leer_interno[0].ToString()));                    // colocar numero mayor de la calle a la caja de texto

                    }
                    cboMzaN.SelectedIndex = cboMzaN.Items.Count - 10; // Seleccionar el último elemento (el mayor)
                    con.cerrar_interno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    return; // Retornar false si ocurre un error
                }
            }
        }
        private void enviar_datos()
        {

            string localidad_temp = "";
            string colonia_temp = "";
            string area_hom_temp = "";
            string uso_temp = "";
            string clase_temp = "";
            string cate_temp = "";
            cboAreasHomN.Items.Clear();
            cboColoniaN.Items.Clear();
            cboLocalidadN.Items.Clear();
            cboMzaN.Items.Clear();

            txtNoZonaN.Text = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[0].Value).Trim();
            numero_zona = Convert.ToInt32(txtNoZonaN.Text.Trim());
            if (MOVIMIENTO == 1)//ALTA DE MANZANA
            {
                validar_manzana();
            }
            else
            {
                cboMzaN.Items.Add(Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[1].Value).Trim());
                cboMzaN.SelectedIndex = 0;
            }
            //boUsoN.Text = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[8].Value).Trim();
            //cboClaseN.Text = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[9].Value).Trim();
            //cboCateConstN.Text = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[10].Value).Trim();
            colonias = 2;
            cboColoniaN.Items.Clear();
            cargar_colonias();
            localidades = 2;
            cboLocalidadN.Items.Clear();
            cargar_localidades();
            areas_homogeneas = 2;
            cboAreasHomN.Items.Clear();
            combo_uso();
            cargar_areas_hom();
            CARGAR_AREAS_HOM_MANZANAS_TABLA();



            localidad_temp = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[4].Value).Trim();
            foreach (var item in cboLocalidadN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(localidad_temp))
                {
                    // Mostrar el valor completo del ComboBox
                    cboLocalidadN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }

            colonia_temp = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[2].Value).Trim();
            foreach (var item in cboColoniaN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(colonia_temp))
                {
                    // Mostrar el valor completo del ComboBox
                    cboColoniaN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }

            area_hom_temp = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[6].Value).Trim();
            foreach (var item in cboAreasHomN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(area_hom_temp))
                {
                    // Mostrar el valor completo del ComboBox
                    cboAreasHomN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }

            uso_temp = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[8].Value).Trim();
            foreach (var item in cboUsoN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(uso_temp))
                {
                    // Mostrar el valor completo del ComboBox
                    cboUsoN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }

            clase_temp = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[9].Value).Trim();
            foreach (var item in cboClaseN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(clase_temp))
                {
                    // Mostrar el valor completo del ComboBox
                    cboClaseN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }

            cate_temp = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[10].Value).Trim();
            foreach (var item in cboCateConstN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(cate_temp))
                {
                    // Mostrar el valor completo del ComboBox
                    cboCateConstN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }

            if (!string.IsNullOrEmpty(area_hom_temp))
            {
                // Recorrer todas las filas del DataGridView
                foreach (DataGridViewRow fila in DGV_AREAS_HOM.Rows)
                {
                    // Verificar que no sea la fila nueva (si está en modo edición)
                    if (!fila.IsNewRow)
                    {
                        // Obtener el valor de la columna 0
                        object valorCelda = fila.Cells[0].Value;

                        if (valorCelda != null && valorCelda.ToString() == area_hom_temp)
                        {
                            // Limpiar selección anterior
                            DGV_AREAS_HOM.ClearSelection();

                            // Seleccionar la fila encontrada
                            fila.Selected = true;

                            // Hacer scroll hasta la fila seleccionada
                            DGV_AREAS_HOM.FirstDisplayedScrollingRowIndex = fila.Index;

                            // Opcional: Hacer focus en el DataGridView
                            DGV_AREAS_HOM.Focus();

                            break; // Salir del bucle al encontrar la primera coincidencia
                        }
                    }
                }
            }

            // txtCalleN.Text = Convert.ToString(DGV_MANZANAS.CurrentRow.Cells[4].Value).Trim();

            btnValidarZona.Enabled = false;
        }

        private void CARGAR_AREAS_HOM_MANZANAS_TABLA()
        {

            DataTable LLENAR_GRID_1 = new DataTable();

            con.conectar_base_interno();
            con.open_c_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT AreaHom, DescAreaHo, Uso, Clasif, ValM2Suelo, FrenteBase, FondoBase, AnioVigVUS ";
            con.cadena_sql_interno = con.cadena_sql_interno + "     FROM AREASH";
            con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE AnioVigVUS = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY AnioVigVUS";


            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DGV_AREAS_HOM.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 6, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
            DGV_AREAS_HOM.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 6);
            DGV_AREAS_HOM.RowTemplate.Height = 18; // Altura de cada fila
                                                   //DGV_AREAS_HOM.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DGV_AREAS_HOM.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
            DGV_AREAS_HOM.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            foreach (DataGridViewColumn columna in DGV_AREAS_HOM.Columns)
            {
                columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            // Configuración de selección
            DGV_AREAS_HOM.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Deshabilitar edición
            DGV_AREAS_HOM.ReadOnly = true;
            // Estilos visuales
            DGV_AREAS_HOM.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            DGV_AREAS_HOM.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Configurar todas las columnas para que no se puedan redimensionar
            DGV_AREAS_HOM.AllowUserToResizeColumns = false;
            da.Fill(LLENAR_GRID_1);
            DGV_AREAS_HOM.DataSource = LLENAR_GRID_1;

            con.cerrar_interno();

            //DGV_AREAS_HOM.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            DGV_AREAS_HOM.Columns[0].Width = 45;                         // AREA HOMOGENEA
            DGV_AREAS_HOM.Columns[1].Width = 150;                         // DESCRIPCION
            DGV_AREAS_HOM.Columns[2].Width = 45;                         //USO
            DGV_AREAS_HOM.Columns[3].Width = 45;                         //CLASIFICACION
            DGV_AREAS_HOM.Columns[4].Width = 85;                         //VALOR DEL METRO CUADRADO
            DGV_AREAS_HOM.Columns[5].Width = 45;                         //FRENTE
            DGV_AREAS_HOM.Columns[6].Width = 45;                         //FONDO
            DGV_AREAS_HOM.Columns[7].Width = 80;                         //AÑO


            DGV_AREAS_HOM.Columns[0].Name = "AREA";                       // AREA HOMOGENEA
            DGV_AREAS_HOM.Columns[1].Name = "DESCRIPCION";                // DESCRIPCION
            DGV_AREAS_HOM.Columns[2].Name = "USO";                        //USO
            DGV_AREAS_HOM.Columns[3].Name = "CLASIFICACION";              //CLASIFICACION
            DGV_AREAS_HOM.Columns[4].Name = "VALOR";                      //VALOR DEL METRO CUADRADO
            DGV_AREAS_HOM.Columns[5].Name = "FRENTE";                     //FRENTE
            DGV_AREAS_HOM.Columns[6].Name = "FONDO";                      //FONDO
            DGV_AREAS_HOM.Columns[7].Name = "AÑO";                      //AÑO


            DGV_AREAS_HOM.Columns[0].HeaderText = "AREA";                       // AREA HOMOGENEA
            DGV_AREAS_HOM.Columns[1].HeaderText = "DESCRIPCION";                // DESCRIPCION
            DGV_AREAS_HOM.Columns[2].HeaderText = "USO";                        //USO
            DGV_AREAS_HOM.Columns[3].HeaderText = "CLASIFICACION";              //CLASIFICACION
            DGV_AREAS_HOM.Columns[4].HeaderText = "VALOR";                      //VALOR DEL METRO CUADRADO
            DGV_AREAS_HOM.Columns[5].HeaderText = "FRENTE";                     //FRENTE
            DGV_AREAS_HOM.Columns[6].HeaderText = "FONDO";                      //FONDO
            DGV_AREAS_HOM.Columns[7].HeaderText = "AÑO";                        //AÑO

            DGV_AREAS_HOM.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_AREAS_HOM.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_AREAS_HOM.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_AREAS_HOM.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_AREAS_HOM.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_AREAS_HOM.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_AREAS_HOM.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGV_AREAS_HOM.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        private void crud() // METODO PARA EL CRUD DE LA MANZANA
        {
            try
            {

                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("SONG_CRUD_MANZANA", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 1).Value = Program.PEstado;
                cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 1).Value = Program.municipioN;
                cmd.Parameters.Add("@ZONA", SqlDbType.Int, 2).Value = zona_crud;
                cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 2).Value = manzana_crud;
                cmd.Parameters.Add("@COLONIA", SqlDbType.Int, 2).Value = colonia_crud;
                cmd.Parameters.Add("@LOCALIDAD", SqlDbType.Int, 2).Value = localidad_crud;
                cmd.Parameters.Add("@AREAHOM", SqlDbType.Int, 2).Value = area_homogenea_crud;
                cmd.Parameters.Add("@USO", SqlDbType.VarChar, 2).Value = uso_crud;
                cmd.Parameters.Add("@CLASECONST", SqlDbType.VarChar, 2).Value = clase_crud;
                cmd.Parameters.Add("@CATEGCONST", SqlDbType.VarChar, 2).Value = cate_const_crud;
                cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = Program.nombre_usuario;
                cmd.Parameters.Add("@MOVIMIENTO", SqlDbType.VarChar, 100).Value = MOVIMIENTO;
                cmd.Parameters.Add("@VALIDACION", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();
                validacion = Convert.ToInt32(cmd.Parameters["@VALIDACION"].Value);

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
            if (validacion == 1)
            {
                MessageBox.Show("SE REALIZO EL ALTA CORRECTAMENTE", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (validacion == 2)
            {
                MessageBox.Show("SE REALIZO LA EDICION CORRECTAMENTE", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (validacion == 3)
            {
                MessageBox.Show("SE REALIZO LA ELIMINACION CORRECTAMENTE", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("ERROR AL REALIZAR LA OPERACION", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //string zona, manzana, colonia, localidad, area_homogenea, uso, clase, cate_const;

            if (cboLocalidadN.Text == "")
            {
                MessageBox.Show("Ingrese La Localidad", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboLocalidadN.Focus();
                return;
            }
            if (cboAreasHomN.Text == "")
            {
                MessageBox.Show("Ingrese El Area Homogenea", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboAreasHomN.Focus();
                return;
            }
            if (cboColoniaN.Text == "")
            {
                MessageBox.Show("Ingrese La Colonia", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboColoniaN.Focus();
                return;
            }
            if (cboMzaN.Text == "")
            {
                MessageBox.Show("Ingrese El Uso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboMzaN.Focus();
                return;
            }
            if (cboClaseN.Text == "")
            {
                MessageBox.Show("Ingrese La Clase", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboClaseN.Focus();
                return;
            }
            if (cboCateConstN.Text == "")
            {
                MessageBox.Show("Ingrese La Categoria de Construccion", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboCateConstN.Focus();
                return;
            }

            zona_crud = txtNoZonaN.Text.Trim();
            manzana_crud = cboMzaN.Text.Trim();
            colonia_crud = cboColoniaN.Text.Substring(0, 3);
            localidad_crud = cboLocalidadN.Text.Substring(0, 3);
            area_homogenea_crud = cboAreasHomN.Text.Substring(0, 3);
            uso_crud = cboUsoN.Text;
            clase_crud = cboClaseN.Text;
            cate_const_crud = cboCateConstN.Text.Substring(0, 1);

            if (MOVIMIENTO == 1)//ALTA DE MANZANA
            {
                DialogResult resp = MessageBox.Show("¿ESTA SEGURO DESEA CREAR LA MANZANA?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    crud(); // Llamar al método para realizar el CRUD de la manzana
                    formaInicio(); // Llamar al método para reiniciar el formulario


                }
                if (resp == DialogResult.No)
                {
                    //no hacer nada
                    // txtCalleN.Focus();
                    return; // Sale del método o procedimiento si el usuario no confirma la edición

                }
            }
            else if (MOVIMIENTO == 2)//EDITANDO MANZANA
            {
                int totalCve = 0;
                try
                {

                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "SELECT count(Manzana)";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  From PREDIOS ";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Where Zona = " + zona_crud;
                    con.cadena_sql_interno = con.cadena_sql_interno + "   And Manzana =   " + manzana_crud;

                    con.open_c_interno();
                    con.cadena_sql_cmd_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        totalCve = Convert.ToInt32(con.leer_interno[0].ToString());
                    }
                    con.cerrar_interno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    return; // Retornar false si ocurre un error
                }
                DialogResult resp = MessageBox.Show("¿SEGURO QUE DESEA EDITAR LA MANZANA, SE AFECTARAN " + totalCve + " CLAVES CATASTRALES ?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {

                    crud(); // Llamar al método para realizar el CRUD de la manzana
                    formaInicio(); // Llamar al método para reiniciar el formulario

                }
                if (resp == DialogResult.No)
                {
                    //no hacer nada
                    return; // Sale del método o procedimiento si el usuario no confirma la edición
                }

            }
            else if (MOVIMIENTO == 3)// ELIMINACION DE manzana
            {
                DialogResult resp = MessageBox.Show(" ¿SEGURO QUE DESEA ELIMINAR LA MANZANA No. " + manzana_crud + " DE LA ZONA No. " + zona_crud + " ?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    int totalCve = 0;
                    try
                    {

                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "SELECT count(Manzana)";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  From PREDIOS ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Where Zona = " + zona_crud;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   And Manzana =   " + manzana_crud;

                        con.open_c_interno();
                        con.cadena_sql_cmd_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            totalCve = Convert.ToInt32(con.leer_interno[0].ToString());
                        }
                        con.cerrar_interno();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }

                    if (totalCve != 0)
                    {
                        MessageBox.Show("NO SE PUEDE ELIMINAR LA MANZANA DADO QUE SE ENCUENTRA UTILIZADO EN " + totalCve + " CLAVES CATASTRALES", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        crud(); // Llamar al método para realizar el CRUD de la manzana
                        formaInicio();
                    }
                }
                if (resp == DialogResult.No)
                {
                    //no hacer nada
                    return; // Sale del método o procedimiento si el usuario no confirma la edición
                }
            }

        }

        private void frmColonias_Load(object sender, EventArgs e)
        {
            formaInicio();
            cajasColor();
            lblUsuario.Text = "Usuario: " + Program.nombre_usuario.Trim();
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            formaInicio();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PNLFBUSCAR.Enabled = true; // Habilitar el panel de búsqueda 
            DGV_MANZANAS.DataSource = null;
            DGV_MANZANAS.Enabled = false; // Deshabilitar el DataGridView de resultados
            btnBuscar.Enabled = false; // Deshabilitar el botón de búsqueda para evitar múltiples clics
            btnConsulta_bus.BackColor = Color.Yellow;
            btnConsulta_bus.ForeColor = Color.Black;
            //btnNuevo.Enabled = false;
            //btnEditar.Enabled = false;
            //btnBorrar.Enabled = false;

            LIMPIARBUSQUEDA();



        }
        private void LIMPIARBUSQUEDA()
        {
            pnlManzanaB.Enabled = false;
            rbnZona.Checked = false; // Desmarcar el radio button de área emisora
            Cbo_ZonaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboMzaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            pnlManzanaB.Enabled = false; // Deshabilitar el panel de código de calle
            Cbo_ZonaB.Enabled = false; // Deshabilitar el combo box
            rbColonia.Checked = false; // Desmarcar el radio button

            rbColonia.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            cboColoniaB.SelectedIndex = -1; // Limpiar el campo
            cboColoniaB.Enabled = false; // Deshabilitar el campo

            rbLocalidad.Checked = false; // Desmarcar el radio button
            cboLocalidadB.SelectedIndex = -1; // Limpiar el campo
            cboLocalidadB.Enabled = false; // Deshabilitar el campo

            rbManzana.Checked = false; // Desmarcar el radio button 
            cboMzaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboMzaB.Enabled = false; // Deshabilitar el campo

            rbAreaHomogenea.Checked = false; // Desmarcar el radio button
            cboAreasHomB.SelectedIndex = -1; // Limpiar el campo
            cboAreasHomB.Enabled = false; // Deshabilitar el campo
            txtNoZonaN.Text = "";
            //cboMzaN.Text = "";
            cboColoniaN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            btnValidarMza.Enabled = false;
            btnValidarZona.Enabled = false;
            txtNoZonaN.Enabled = false;
            cboLocalidadN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboLocalidadN.Enabled = false;
            cboColoniaN.SelectedIndex = -1; // Desmarcar cualquier selección previ
            cboColoniaN.Enabled = false; // Deshabilitar el campo de texto 
            cboMzaN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboMzaN.Enabled = false; // Deshabilitar el campo de texto 
            cboAreasHomN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboAreasHomN.Enabled = false;
            cboUsoN.Items.Clear();
            cboUsoN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboClaseN.Items.Clear();
            cboClaseN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboCateConstN.Items.Clear();
            cboCateConstN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            DGV_AREAS_HOM.DataSource = null;
            cboUsoN.Enabled = false;
            cboClaseN.Enabled = false;
            cboCateConstN.Enabled = false;
            btnGuardar.Enabled = false;
            btn_cancelar2.Enabled = false;

        }

        private void cmd_cancelar2_Click(object sender, EventArgs e)
        {
            txtNoZonaN.Text = "";
            //cboMzaN.Text = "";
            cboColoniaN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            btnValidarMza.Enabled = false;
            btnValidarZona.Enabled = false;
            txtNoZonaN.Enabled = false;
            cboLocalidadN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboLocalidadN.Enabled = false;
            cboColoniaN.SelectedIndex = -1; // Desmarcar cualquier selección previ
            cboColoniaN.Enabled = false; // Deshabilitar el campo de texto 
            cboMzaN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboMzaN.Enabled = false; // Deshabilitar el campo de texto 
            cboAreasHomN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboAreasHomN.Enabled = false;
            cboUsoN.Items.Clear();
            cboUsoN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboClaseN.Items.Clear();
            cboClaseN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboCateConstN.Items.Clear();
            cboCateConstN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            DGV_AREAS_HOM.DataSource = null;
            cboUsoN.Enabled = false;
            cboClaseN.Enabled = false;
            cboCateConstN.Enabled = false;
            if (MOVIMIENTO == 1)
            {
                btnValidarZona.Enabled = true;
                txtNoZonaN.Enabled = true; // Habilitar el campo de texto para el número de zona
                txtNoZonaN.Focus();
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

        private void btnSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnSalida, "SALIR");
        }

        private void btnCancela_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnCancela, "CANCELAR");
        }

        private void btnBuscar_MouseHover(object sender, EventArgs e)
        {

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBuscar, "FILTRO DE BUSQUEDA");
        }

        private void btnBorrar_MouseHover(object sender, EventArgs e)
        {

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnBorrar, "ELIMINAR");
        }

        private void btnEditar_MouseHover(object sender, EventArgs e)
        {

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnEditar, "EDITAR");
        }

        private void btnNuevo_MouseHover(object sender, EventArgs e)
        {

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnNuevo, "NUEVO");
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

        private void btn_cancelar2_MouseHover(object sender, EventArgs e)
        {

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btn_cancelar2, "CANCELA PROCESO");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cbo_ZonaB.Items.Clear();
            Cbo_ZonaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboMzaB.Items.Clear();
            cboMzaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            Cbo_ZonaB.Enabled = false; // Deshabilitar el combo box 
            rbnZona.Checked = false; // Desmarcar el radio button 
            rbColonia.Checked = false; // Desmarcar el radio button

            rbLocalidad.Checked = false; // Desmarcar el radio button
            rbManzana.Checked = false; // Desmarcar el radio butt

            cboColoniaB.Items.Clear(); // Limpiar el campo 
            cboColoniaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboColoniaB.Enabled = false; // Deshabilitar el campo
            cboLocalidadB.Items.Clear(); // Limpiar el campo de 
            cboLocalidadB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboLocalidadB.Enabled = false; // Deshabilitar el campo 
            cboAreasHomB.Items.Clear(); // Limpiar el campo 
            cboAreasHomB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboAreasHomB.Enabled = false; // Deshabilitar el campo
            cboMzaB.Enabled = false; // Deshabilitar el campo 
            DGV_MANZANAS.DataSource = null; // Limpiar la fuente de datos del DataGridView
            lblNumRegistro.Text = "0";
            pnlManzanaB.Enabled = false; // Deshabilitar el panel 
            cboLocalidadB.SelectedIndex = -1; // Desmarcar cualquier selección previa
        }

        private void rbnNoCol_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnZona.Checked == true)//se llena el combo box de zonas
            {
                Cbo_ZonaB.Items.Clear();
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   Select DISTINCT (Zona)  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM MANZANAS";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY Zona";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();


                    while (con.leer_interno.Read())
                    {
                        Cbo_ZonaB.Items.Add(con.leer_interno[0].ToString().Trim());                // 

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
                Cbo_ZonaB.Enabled = true;
                pnlManzanaB.Enabled = true;
                Cbo_ZonaB.SelectedIndex = 0;
                Cbo_ZonaB.Focus();
            }
        }
        private void cargar_colonias()
        {
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT Colonia, NomCol";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM COLONIAS";
                con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY Colonia";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (colonias == 1) // Si se está cargando la colonia para búsqueda
                    {
                        cboColoniaB.Items.Add(con.leer_interno[0].ToString() + "  " + con.leer_interno[1].ToString());
                    }
                    else if (colonias == 2) // Si se está cargando la colonia para creación o edición
                    {
                        cboColoniaN.Items.Add(con.leer_interno[0].ToString() + "  " + con.leer_interno[1].ToString());
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
        }


        private void rbIdentiNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbColonia.Checked == true)
            {
                cboColoniaB.Items.Clear();
                colonias = 1; // Indica que se está cargando la colonia busqueda
                cargar_colonias();
                cboColoniaB.Enabled = true;
                cboColoniaB.SelectedIndex = 0;
                cboColoniaB.Focus();
            }
        }

        private void cargar_localidades()
        {
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT Localidad, NomLoc ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM LOCALIDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY Localidad";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (localidades == 1) // Si se está cargando la localidad para búsqueda
                    {
                        cboLocalidadB.Items.Add(con.leer_interno[0].ToString() + "  " + con.leer_interno[1].ToString());                     // colocar numero mayor de la calle a la caja de texto
                    }
                    else if (localidades == 2) // Si se está cargando la localidad para creación o edición
                    {
                        cboLocalidadN.Items.Add(con.leer_interno[0].ToString() + "  " + con.leer_interno[1].ToString());                     // colocar numero mayor de la calle a la caja de texto
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

        }

        private void rbCP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLocalidad.Checked == true)
            {
                cboLocalidadB.Items.Clear();
                localidades = 1; // Indica que se está cargando la localidad busqueda
                cargar_localidades();
                cboLocalidadB.Enabled = true;
                cboLocalidadB.SelectedIndex = 0;
                cboLocalidadB.Focus();

            }
        }
        private void cajasColor()
        {

            cboMzaB.Enter += util.Cbo_Box_Enter;

            cboMzaN.Enter += util.Cbo_Box_Enter;
            Cbo_ZonaB.Enter += util.Cbo_Box_Enter;

            cboLocalidadB.Enter += util.Cbo_Box_Enter;
            cboLocalidadN.Enter += util.Cbo_Box_Enter;
            txtNoZonaN.Enter += util.TextBox_Enter;
            cboColoniaN.Enter += util.Cbo_Box_Enter;
            cboColoniaB.Enter += util.Cbo_Box_Enter;
            cboAreasHomB.Enter += util.Cbo_Box_Enter;
            cboAreasHomN.Enter += util.Cbo_Box_Enter;
            cboCateConstN.Enter += util.Cbo_Box_Enter;
            cboClaseN.Enter += util.Cbo_Box_Enter;
            cboUsoN.Enter += util.Cbo_Box_Enter;


        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBorrar7_Click(object sender, EventArgs e)
        {
            rbnZona.Checked = false; // Desmarcar el radio button de área emisora
            Cbo_ZonaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboMzaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            pnlManzanaB.Enabled = false; // Deshabilitar el panel de código de calle
            Cbo_ZonaB.Enabled = false; // Deshabilitar el combo box
        }

        private void cmdLimpiaCiudadano_Click(object sender, EventArgs e)
        {
            rbColonia.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            cboColoniaB.SelectedIndex = -1; // Limpiar el campo
            cboColoniaB.Enabled = false; // Deshabilitar el campo


        }

        private void DGV_AREAS_HOM_DoubleClick(object sender, EventArgs e)
        {
            string area, uso_tmp;
            area = Convert.ToString(DGV_AREAS_HOM.CurrentRow.Cells[0].Value);

            foreach (var item in cboAreasHomN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(area))
                {
                    // Mostrar el valor completo del ComboBox
                    cboAreasHomN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }

            uso_tmp = Convert.ToString(DGV_AREAS_HOM.CurrentRow.Cells[2].Value);
            foreach (var item in cboUsoN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(uso_tmp))
                {
                    // Mostrar el valor completo del ComboBox
                    cboUsoN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }


            //cboUsoN.Enabled = false;
            cboCateConstN.Items.Clear();
            cboCateConstN.Enabled = false;
            cboClaseN.Focus();
        }

        private void txtCateConstN_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void txtNoZonaN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                validacion_zona();
            }
        }

        private void cmdLimpiarRFC_Click(object sender, EventArgs e)
        {
            rbLocalidad.Checked = false; // Desmarcar el radio button
            cboLocalidadB.SelectedIndex = -1; // Limpiar el campo
            cboLocalidadB.Enabled = false; // Deshabilitar el campo

        }

        private void txtClaseN_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloLetras(e);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMinimizar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnMinimizar, "MINIMIZAR PANTALLA");
        }

        private void btnGuardar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnGuardar, "GUARDAR");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rbAreaHomogenea.Checked = false; // Desmarcar el radio button
            cboAreasHomB.SelectedIndex = -1; // Limpiar el campo
            cboAreasHomB.Enabled = false; // Deshabilitar el campo
        }
        private void combo_uso()
        {
            cboUsoN.Items.Clear();
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "   Select Uso ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM TIPO_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE AnioVigVUC = " + Program.añoActual;
                con.cadena_sql_interno = con.cadena_sql_interno + " GROUP BY Uso";


                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();


                while (con.leer_interno.Read())
                {
                    cboUsoN.Items.Add(con.leer_interno[0].ToString().Trim());                // 

                }
                cboUsoN.Enabled = true;


                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
        }

        private void cboUsoN_SelectedValueChanged(object sender, EventArgs e)
        {
            cboClaseN.Items.Clear();
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "   Select ClaseConst ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM TIPO_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE AnioVigVUC = " + Program.añoActual;
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND Uso = " + util.scm(cboUsoN.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + " GROUP BY ClaseConst ";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();


                while (con.leer_interno.Read())
                {
                    cboClaseN.Items.Add(con.leer_interno[0].ToString().Trim());                // 

                }
                cboClaseN.Enabled = true;
                cboCateConstN.Items.Clear();
                cboCateConstN.SelectedIndex = -1;

                //cboClaseN.SelectedIndex = 0;
                cboClaseN.Focus();
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
        }

        private void cboClaseN_SelectedValueChanged(object sender, EventArgs e)
        {
            cboCateConstN.Items.Clear();
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "   Select CategConst, DescrClCat ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM TIPO_CONST";
                con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE AnioVigVUC = " + Program.añoActual;
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND Uso = " + util.scm(cboUsoN.Text);
                con.cadena_sql_interno = con.cadena_sql_interno + "      AND ClaseConst = " + util.scm(cboClaseN.Text);


                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();


                while (con.leer_interno.Read())
                {
                    cboCateConstN.Items.Add(con.leer_interno[0].ToString().Trim() + " " + con.leer_interno[1].ToString().Trim());                // 

                }
                cboCateConstN.Enabled = true;
                //cboCateConstN.SelectedIndex = 0;
                cboCateConstN.Focus();
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return; // Retornar false si ocurre un error
            }
        }

        private void cboMzaN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                validarManzana();
            }
        }

        private void btnValidarZona_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnValidarZona, "VALIDAR ZONA");
        }
        private void validarManzana()
        {
            string numero_manzana, numero_zona;
            numero_zona = txtNoZonaN.Text;
            numero_manzana = cboMzaN.Text;
            int verificar = 0, verificar2 = 0;
            //CARGAR_COLONIAS_MANZANAS();

            if (numero_manzana == "")
            {
                MessageBox.Show("Porfavor De Seleccionar Una Manzana", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (numero_manzana == "0")
            {
                MessageBox.Show("El Numero De Manzana Es Incorrecta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //VERIFICAMOS SI EXISTE EN LA TABLA DE PREDIOS
                con.conectar_base_interno();

                con.cadena_sql_interno = "IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "  From PREDIOS ";
                con.cadena_sql_interno = con.cadena_sql_interno + " Where Zona = " + numero_zona;
                con.cadena_sql_interno = con.cadena_sql_interno + " And Manzana = " + numero_manzana + " )";
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
                if (verificar == 1)
                {
                    //aqui va verificar si existe en la tabla de mazanas y colocar mensaje que existe una clave catastral con esa zona y manzana pero puede continuar

                    con.conectar_base_interno();

                    con.cadena_sql_interno = "IF EXISTS (SELECT *";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  From MANZANAS ";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Where Zona = " + numero_zona;
                    con.cadena_sql_interno = con.cadena_sql_interno + " And Manzana = " + numero_manzana + " )";
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
                        verificar2 = Convert.ToInt32(con.leer_interno[0].ToString());
                    }
                    if (verificar2 == 1)
                    {
                        MessageBox.Show("Esta Zona Y Manzana Ya Existen", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (verificar2 == 2)
                    {
                        colonias = 2; // Indica que se está cargando la colonia para creación o edición
                        cargar_colonias(); // Cargar las colonias en el combo box
                        localidades = 2; // Indica que se está cargando la localidad para creación o edición
                        cargar_localidades(); // Cargar las localidades en el combo box
                        areas_homogeneas = 2; // Indica que se está cargando el area homogenea para creación o edición
                        cargar_areas_hom();
                        CARGAR_AREAS_HOM_MANZANAS_TABLA();
                        combo_uso();
                        cboMzaN.Enabled = false;
                        cboColoniaN.Enabled = true;
                        cboLocalidadN.Enabled = true;
                        cboAreasHomN.Enabled = true;
                        cboUsoN.Enabled = true;
                        cboClaseN.Enabled = true;
                        cboCateConstN.Enabled = true;
                        btnValidarMza.Enabled = false;
                        DGV_AREAS_HOM.Enabled = true;

                        MessageBox.Show("La Manzana " + numero_manzana + " De La Zona " + numero_zona + " Se Encuentra Asignada A Una Clave Catastral", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                if (verificar == 2)
                {
                    colonias = 2; // Indica que se está cargando la colonia para creación o edición
                    cargar_colonias(); // Cargar las colonias en el combo box
                    localidades = 2; // Indica que se está cargando la localidad para creación o edición
                    cargar_localidades(); // Cargar las localidades en el combo box
                    areas_homogeneas = 2; // Indica que se está cargando el area homogenea para creación o edición
                    cargar_areas_hom();
                    CARGAR_AREAS_HOM_MANZANAS_TABLA();
                    combo_uso();
                    cboMzaN.Enabled = false;
                    cboColoniaN.Enabled = true;
                    cboLocalidadN.Enabled = true;
                    cboAreasHomN.Enabled = true;
                    cboUsoN.Enabled = true;
                    cboClaseN.Enabled = true;
                    cboCateConstN.Enabled = true;
                    btnValidarMza.Enabled = false;
                    DGV_AREAS_HOM.Enabled = true;
                    MessageBox.Show("LA MANZANA NO EXISTE EN ESTA ZONA, PUEDE CONTINUAR CON EL PROCESO DE ALTA ", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }

            }
        }

        private void btnValidarMza_Click(object sender, EventArgs e)
        {
           
            validarManzana();
        }


        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void PNLNEW_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void cargar_areas_hom()
        {
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT AreaHom, DescAreaHo ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM AREASH";
                con.cadena_sql_interno = con.cadena_sql_interno + "     Where AnioVigVUS = " + Program.añoActual;
                con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY AreaHom";

                con.open_c_interno();
                con.cadena_sql_cmd_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (areas_homogeneas == 1) // Si se está cargando la colonia para búsqueda
                    {
                        cboAreasHomB.Items.Add(con.leer_interno[0].ToString() + "  " + con.leer_interno[1].ToString());                     // colocar numero mayor de la calle a la caja de texto
                    }
                    else if (areas_homogeneas == 2) // Si se está cargando la colonia para creación o edición
                    {
                        cboAreasHomN.Items.Add(con.leer_interno[0].ToString() + "  " + con.leer_interno[1].ToString());                     // colocar numero mayor de la calle a la caja de texto
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
        }

        private void rbAreaHomogenea_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAreaHomogenea.Checked == true)
            {
                cboAreasHomB.Items.Clear();
                areas_homogeneas = 1;
                cargar_areas_hom();
                cboAreasHomB.Enabled = true;
                cboAreasHomB.SelectedIndex = 0;
                cboAreasHomB.Focus();

            }
        }

        private void btnValidarZona_Click(object sender, EventArgs e)
        {
            validacion_zona();
        }
        private void validacion_zona()
        {
            string numero_zona;
            int verificar = 0, NUMMAY = 0;
            numero_zona = txtNoZonaN.Text.Trim();
            if (numero_zona == "")
            {
                MessageBox.Show("FAVOR DE COLOCAR UNA ZONA", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoZonaN.Focus();
            }
            else if (numero_zona == "0")
            {
                MessageBox.Show("FAVOR DE COLOCAR UNA ZONA CORRECTA", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoZonaN.Focus();
            }
            else
            {
                //Hacer consulta si existe y de colocar el mayor codigo de calle
                try
                {
                    con.conectar_base_interno();

                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "IF EXISTS (SELECT ZONA";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     From MANZANAS ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Where ZONA = " + numero_zona + " )";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    BEGIN";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT existe = 1";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    End";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Else";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    BEGIN";
                    con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT existe = 2";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    End";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        verificar = Convert.ToInt32(con.leer_interno[0].ToString());
                    }
                    con.cerrar_interno();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    util.CapturarPantallaConInformacion(ex);
                    System.Threading.Thread.Sleep(500);
                    con.cerrar_interno();
                    return; // Retornar false si ocurre un error
                }
                if (verificar == 1)
                {
                    try
                    {
                        con.conectar_base_interno();

                        con.cadena_sql_interno = "Select max(MANZANA) +10 AS MAYOR";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  FROM MANZANAS";
                        con.cadena_sql_interno = con.cadena_sql_interno + " WHERE ZONA =" + numero_zona;

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            NUMMAY = Convert.ToInt32(con.leer_interno[0].ToString());                     // colocar numero mayor de la calle a la caja de texto
                        }

                        con.cerrar_interno();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }
                    try
                    {
                        con.conectar_base_interno();

                        con.cadena_sql_interno = "SELECT CONSECUTIVO  FROM FALTANTES FA ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE NOT EXISTS";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  ( SELECT MANZANA FROM MANZANAS MA WHERE MA.Manzana = FA.CONSECUTIVO";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   AND MA.Zona =    " + numero_zona + ")";
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND FA.CONSECUTIVO <=  " + NUMMAY;
                        con.cadena_sql_interno = con.cadena_sql_interno + "ORDER BY FA.CONSECUTIVO ";

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            cboMzaN.Items.Add(Convert.ToInt32(con.leer_interno[0].ToString()));                    // colocar numero mayor de la calle a la caja de texto

                        }
                        cboMzaN.SelectedIndex = cboMzaN.Items.Count - 11; // Seleccionar el último elemento (el mayor)
                        con.cerrar_interno();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }

                    cboMzaN.Enabled = true;
                    cboColoniaN.Enabled = false;
                    txtNoZonaN.Enabled = false;
                    btnValidarZona.Enabled = false;
                    btnValidarMza.Enabled = true;

                    cboMzaN.Focus();


                }
                else if (verificar == 2)
                {
                    DialogResult resp = MessageBox.Show("NO EXISTE LA ZONA COLOCADA, SE AGREGARA DICHA ZONA, ¿DESEA CONTINUAR CON EL PROCESO?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resp == DialogResult.Yes)
                    {
                        cboMzaN.Items.Clear();
                        cboMzaN.Items.Add("1");
                        cboMzaN.SelectedIndex = 0;
                        colonias = 2; // Indica que se está cargando la colonia para creación o edición
                        cargar_colonias(); // Cargar las colonias en el combo box
                        localidades = 2; // Indica que se está cargando la localidad para creación o edición
                        cargar_localidades(); // Cargar las localidades en el combo box
                        areas_homogeneas = 2; // Indica que se está cargando el area homogenea para creación o edición
                        cargar_areas_hom();
                        CARGAR_AREAS_HOM_MANZANAS_TABLA();
                        combo_uso();
                        cboMzaN.Enabled = false;
                        cboColoniaN.Enabled = true;
                        cboLocalidadN.Enabled = true;
                        cboAreasHomN.Enabled = true;
                        cboUsoN.Enabled = true;
                        cboClaseN.Enabled = false;
                        cboCateConstN.Enabled = false;
                        btnValidarMza.Enabled = false;
                        DGV_AREAS_HOM.Enabled = true;
                        txtNoZonaN.Enabled = false;
                        btnValidarZona.Enabled = false;

                        cboColoniaN.Focus();

                    }
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            rbManzana.Checked = false; // Desmarcar el radio button 
            cboMzaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboMzaB.Enabled = false; // Deshabilitar el campo
        }

        private void rbCodCalle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbManzana.Checked == true)
            {
                if (Cbo_ZonaB.Text.Trim() == "")
                {
                    MessageBox.Show("FAVOR DE SELECCIONAR UNA ZONA PRIMERO", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cbo_ZonaB.Focus();
                    rbManzana.Checked = false;
                    return;
                }
                else
                {
                    cboMzaB.Items.Clear();
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   Select MANZANA ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     FROM MANZANAS";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE Zona = " + Cbo_ZonaB.Text.Trim();

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();


                        while (con.leer_interno.Read())
                        {
                            cboMzaB.Items.Add(con.leer_interno[0].ToString().Trim());                // 

                        }
                        cboMzaB.Enabled = true;
                        cboMzaB.SelectedIndex = 0;
                        cboMzaB.Focus();
                        con.cerrar_interno();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }
                }
            }
        }
    }
}
