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
    public partial class frmCalles : Form
    {
        int MOVIMIENTO = 0;
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();
        string zona_calle, cod_calle, nombre_calle, vialidad_calle;
        int validacion = 0;
        ////////////////////////////////////////////////////////////
        ///////////////// -------PARA ARRASTRAR EL PANEL 
        ////////////////////////////////////////////////////////////
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public frmCalles()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RECUERDE QUE PUEDE GENERAR UNA NUEVA CALLE BASANDOSE EN CUALQUIER REGISTRO DE LA TABLA, DANDOLE DOBLE CLIC", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PNLNEW.Enabled = true; // Habilitar el panel 
            MOVIMIENTO = 1; // Indica que se está creando uuna calle
            txtNoZonaN.Enabled = true; // Deshabilitar el botón de nuevo para evitar duplicados
            txtCodCalleN.Enabled = false; // Deshabilitar el campo de texto para el número de la calle, ya que se generará automáticamente
            cboVialidadN.Enabled = false; // Deshabilitar el campo de texto para ingresar el código postal
            txtNoZonaN.Focus(); // Enfocar el campo de texto para ingresar la calle
            btnEditar.Enabled = false; // Deshabilitar el botón de editar
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar
            btnGuardar.Enabled = true; // Habilitar el botón de guardar
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo para evitar duplicados
            btnCancelar.Enabled = true; // Habilitar el botón de cancelar
            btn_cancelar2.Enabled = true; // Habilitar el botón de cancelar
            btnBuscar.Enabled = true; // Habilitar el botón de buscar
            DGV_CALLE.Enabled = true; // 
            btnBuscar.Enabled = true;
            cargar_datagrid_calles(); // Cargar los datos en el DataGridView 
            lbl_titulo.Text = "PROCESO DE CREACION"; // Establecer el título del formulario
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MOVIMIENTO = 2; // Indica que se está EDITANDO un COLONIA
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo COLONIA
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar COLONIA
            MessageBox.Show("SELECCIONA LA CALLE QUE DESEA EDITAR, DANDO DOBLE CLIC DENTRO DE LA TABLA", "EDICION DE CALLES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lbl_titulo.Text = "PROCESO DE EDICION"; // Establecer el título del formulario
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnBuscar.Enabled = true; // Habilitar el botón de búsqueda de COLONIAs
            DGV_CALLE.Enabled = true; // Habilitar la grilla de resultados para mostrar los COLONIAs existentes
            cargar_datagrid_calles(); // Cargar los datos en el DataGridView 
            DGV_CALLE.Focus(); // Enfocar el DataGridView de resultados para que el usuario pueda seleccionar una calle a editar
        }
        private void formaInicio()
        {
            // Inicializar el formulario y cargar los datos necesarios
            MOVIMIENTO = 0; // movimiento en 0, no se ha seleccionado ninguna acción
            // Agregar las áreas emisoras al combo box
            Cbo_ZonaB.Items.Clear();
            Cbo_ZonaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            rbnZona.Checked = false; // Desmarcar el radio button de zona
            rbIdentiNombre.Checked = false; // Desmarcar el radio button de nombre identico
            rbSimilarNombre.Checked = false; // Desmarcar el radio button de nombre similar
            rbVialidad.Checked = false; // Desmarcar el radio button de vialidad

            txtCalleB.Text = ""; // Limpiar el campo de búsqueda de calles
            cboCodCalleB.SelectedIndex = -1; // Desmarcar cualquier selección previa en el combo box de búsqueda
            txtNoZonaN.Text = ""; // Limpiar la zona 

            PNLNEW.Enabled = false; // Deshabilitar el panel de creación de nuevos 
            PNLFBUSCAR.Enabled = false; // Deshabilitar el panel de búsqueda 
            Cbo_ZonaB.Enabled = false; // Deshabilitar el combo box

            txtCalleB.Enabled = false; // Deshabilitar el combo box
            btnBorrar.Enabled = true; // Deshabilitar el botón de borrar 
            cboCodCalleB.Enabled = false; // Deshabilitar el campo de búsqueda de 
            btnNuevo.Enabled = true; // Habilitar el botón de nuevo 
            btnBuscar.Enabled = false; // Habilitar el botón de búsqueda de 
            DGV_CALLE.Enabled = false; // Habilitar el DataGridView de resultados
            lbl_titulo.Text = "";
            btnEditar.Enabled = true;
            DGV_CALLE.DataSource = null; // Limpiar el DataGridView
            txtCalleN.Text = ""; // Limpiar el campo 
            txtCodCalleN.Text = ""; // Limpiar el campo
            cboVialidadN.SelectedIndex = -1; // Limpiar el campo
            lblNumRegistro.Text = "0";
            btnConsulta_bus.BackColor = Color.FromArgb(55, 61, 69);
            btnConsulta_bus.ForeColor = Color.White;
        }
        private void cargar_datagrid_calles()
        {
            // Cargar los datos en el DataGridView de calles
            try
            {
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "        SELECT C.ZonaOrig , C.CodCalle , C.TipoVialid, V.DescVialid, C.NomCalle  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "          FROM CALLES C,VIALIDAD V";
                con.cadena_sql_interno = con.cadena_sql_interno + "         Where C.TipoVialid = V.TipoVialid";
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
                    DGV_CALLE.DataSource = LLENAR_GRID_1;
                    con.cerrar_interno();
                    DGV_CALLE.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    DGV_CALLE.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DGV_CALLE.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                    DGV_CALLE.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    foreach (DataGridViewColumn columna in DGV_CALLE.Columns)
                    {
                        columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    // Configuración de selección
                    DGV_CALLE.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // Deshabilitar edición
                    DGV_CALLE.ReadOnly = true;
                    // Estilos visuales
                    DGV_CALLE.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    DGV_CALLE.DefaultCellStyle.SelectionForeColor = Color.Black;

                    // Configurar todas las columnas para que no se puedan redimensionar
                    DGV_CALLE.AllowUserToResizeColumns = false;

                    DGV_CALLE.Columns[0].Width = 100;                         // ZONA ORIGEN        
                    DGV_CALLE.Columns[1].Width = 100;                         // CODIGO CALLE
                    DGV_CALLE.Columns[2].Width = 100;                         // TIPO VIALIDAD
                    DGV_CALLE.Columns[3].Width = 200;                         // DESCRIPCION VIALIDAD
                    DGV_CALLE.Columns[4].Width = 329;                         // NOMBRE CALLE


                    DGV_CALLE.Columns[0].Name = "ZONAORIG";                          // ZONA ORIGEN           
                    DGV_CALLE.Columns[1].Name = "CODCALLE";                          // CODIGO CALLE
                    DGV_CALLE.Columns[2].Name = "TIPOVIALIDAD";                      // TIPO VIALIDAD
                    DGV_CALLE.Columns[3].Name = "DESCVIALID";                        // DESCRIPCION VIALIDAD
                    DGV_CALLE.Columns[4].Name = "NOMBRE CALLE";                      // NOMBRE CALLE

                    DGV_CALLE.Columns[0].HeaderText = "ZONA";                        // ZONA ORIGEN          
                    DGV_CALLE.Columns[1].HeaderText = "COD CALLE";                   // CODIGO CALLE
                    DGV_CALLE.Columns[2].HeaderText = "TIPO DE VIALIDAD";            //TIPO VIALIDAD
                    DGV_CALLE.Columns[3].HeaderText = "DESCRIPCION DE VIALIDAD";     // DESCRIPCION VIALIDAD
                    DGV_CALLE.Columns[4].HeaderText = "NOMBRE CALLE";                // NOMBRE CALLE

                    DGV_CALLE.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_CALLE.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_CALLE.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_CALLE.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_CALLE.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;      // 

                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return;
            }
        }
        private void consulta()
        {
            if (Cbo_ZonaB.Text == "")
            {
                if (txtCalleB.Text == "")
                {
                    if (cboCodCalleB.Text == "")
                    {
                        if (cboCodCalleB.Text == "")
                        {
                            MessageBox.Show("NO SE TIENE OPCIONES DE BUSQUEDA", "ERROR", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }

            if (rbnZona.Checked == true) { if (Cbo_ZonaB.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELCCIONAR UNA ZONA", "ERROR", MessageBoxButtons.OK); Cbo_ZonaB.Focus(); return; } }

            if (rbVialidad.Checked == true) { if (cboVialidadB.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR UNA VIALIDAD CORRECTA", "ERROR", MessageBoxButtons.OK); cboCodCalleB.Focus(); return; } }

            if (rbIdentiNombre.Checked == true) { if (txtCalleB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA CALLE CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); txtCalleB.Focus(); return; } }
            if (rbSimilarNombre.Checked == true) { if (txtCalleB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA CALLE CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); txtCalleB.Focus(); return; } }
            if (rbCodCalle.Checked == true) { if (cboCodCalleB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL CODIGO DE LA CALLE CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); txtCalleB.Focus(); return; } }


            // SE ARMA EL query DE BUSQUEDA
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT C.ZonaOrig , C.CodCalle , C.TipoVialid, V.DescVialid, C.NomCalle ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    FROM CALLES C,VIALIDAD V";
            con.cadena_sql_interno = con.cadena_sql_interno + "   Where C.TipoVialid = V.TipoVialid";
            //NUMERO DE ZONA
            if (rbnZona.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "           AND C.ZONAORIG =" + Cbo_ZonaB.Text.Trim(); }
            //CODIGO DE CALLE
            if (rbCodCalle.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "        AND C.CODCALLE =" + cboCodCalleB.Text.Trim(); }

            //nombre de LA CALLE
            if (rbIdentiNombre.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "    AND C.NOMCALLE =" + util.scm(txtCalleB.Text.Trim()); }
            if (rbSimilarNombre.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND C.NOMCALLE LIKE '%" + txtCalleB.Text.Trim() + "%'"; }
            //VIALIDADESL
            if (rbVialidad.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "        AND C.TipoVialid =" + util.scm(cboVialidadB.Text.Substring(0, 3)); }
            con.cadena_sql_interno = con.cadena_sql_interno + "                                     ORDER BY C.ZONAORIG, C.CODCALLE";
            //llenamos la grilla con los resultados de la consulta
            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO
            {
                MessageBox.Show("NO SE ENCONTRO DATOS DE LA BUSQUEDA", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.cerrar_interno();
                return;
            }
            else
            {
                DGV_CALLE.DataSource = LLENAR_GRID_1;
                con.cerrar_interno();
                DGV_CALLE.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                DGV_CALLE.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                DGV_CALLE.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                DGV_CALLE.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                foreach (DataGridViewColumn columna in DGV_CALLE.Columns)
                {
                    columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                // Configuración de selección
                DGV_CALLE.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Deshabilitar edición
                DGV_CALLE.ReadOnly = true;
                // Estilos visuales
                DGV_CALLE.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                DGV_CALLE.DefaultCellStyle.SelectionForeColor = Color.Black;

                // Configurar todas las columnas para que no se puedan redimensionar
                DGV_CALLE.AllowUserToResizeColumns = false;

                DGV_CALLE.Columns[0].Width = 100;                         // ZONA ORIGEN        
                DGV_CALLE.Columns[1].Width = 100;                         // CODIGO CALLE
                DGV_CALLE.Columns[2].Width = 100;                         // TIPO VIALIDAD
                DGV_CALLE.Columns[3].Width = 200;                         // DESCRIPCION VIALIDAD
                DGV_CALLE.Columns[4].Width = 329;                         // NOMBRE CALLE


                DGV_CALLE.Columns[0].Name = "ZONAORIG";                          // ZONA ORIGEN           
                DGV_CALLE.Columns[1].Name = "CODCALLE";                          // CODIGO CALLE
                DGV_CALLE.Columns[2].Name = "TIPOVIALIDAD";                      // TIPO VIALIDAD
                DGV_CALLE.Columns[3].Name = "DESCVIALID";                        // DESCRIPCION VIALIDAD
                DGV_CALLE.Columns[4].Name = "NOMBRE CALLE";                      // NOMBRE CALLE

                DGV_CALLE.Columns[0].HeaderText = "ZONA";                        // ZONA ORIGEN          
                DGV_CALLE.Columns[1].HeaderText = "COD CALLE";                   // CODIGO CALLE
                DGV_CALLE.Columns[2].HeaderText = "TIPO DE VIALIDAD";            //TIPO VIALIDAD
                DGV_CALLE.Columns[3].HeaderText = "DESCRIPCION DE VIALIDAD";     // DESCRIPCION VIALIDAD
                DGV_CALLE.Columns[4].HeaderText = "NOMBRE CALLE";                // NOMBRE CALLE

                DGV_CALLE.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_CALLE.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_CALLE.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_CALLE.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_CALLE.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                int CONTEO;
                CONTEO = DGV_CALLE.Rows.Count - 1;
                lblNumRegistro.Text = CONTEO.ToString();
                DGV_CALLE.Enabled = true; // Habilitar la grilla de resultados
                //btnNuevo.Enabled = true; // Habilitar el botón de nuevo COLONIA
                //btnEditar.Enabled = true; // Habilitar el botón de edición
                //btnBorrar.Enabled = true; // Habilitar el botón de borrar COLONIA

            }
           
        }

        private void btnConsulta_bus_Click(object sender, EventArgs e)
        {
            consulta();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            MOVIMIENTO = 3; // Indica que se está ELIMINANDO 
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo 
            MessageBox.Show("SELECCIONA LA CALLE QUE DESEA ELIMINAR DENTRO DE LA TABLA DANDO DOBLE CLIC EN LA MISMA", "ELIMINACION DE COLONIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lbl_titulo.Text = "PROCESO DE ELIMINACION"; // Establecer el título del formulario
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnBuscar.Enabled = true; // 
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar
            DGV_CALLE.Enabled = true; // Habilitar la grilla de resultados para mostrar los calles existentes
            cargar_datagrid_calles(); // Cargar los datos en el DataGridView 
            DGV_CALLE.Focus(); // Enfocar el DataGridView de resultados para que el usuario pueda seleccionar un calles a eliminar
        }

        private void DGV_COLONIAS_DoubleClick(object sender, EventArgs e)
        {
            if (MOVIMIENTO == 1)//ALTA DE UNA CALLE
            {
               
                    if (DGV_CALLE.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel de creación de nuevas calles
                    txtNoZonaN.Enabled = false; // Deshabilitar el campo de texto para el número de calle, ya que se generará automáticamente
                    txtCalleN.Enabled = true; // Habilitar el campo de texto para ingresar el nombre de la calle
                    cboVialidadN.Enabled = true; // Habilitar el campo de texto para ingresar la vialidad de la calle
                    txtCodCalleN.Enabled = false; // Deshabilitar el campo de texto para el código de la calle, ya que se generará automáticamente
                    btn_cancelar2.Enabled = true;
                    btnGuardar.Enabled = true;

                
            }
            else if (MOVIMIENTO == 2)//EDITANDO CALLE
            {
                
                    if (DGV_CALLE.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel de creación de nuevos COLONIAs
                    txtNoZonaN.Enabled = false; // Deshabilitar el campo de texto para el número de colonia, ya que se generará automáticamente
                    txtCalleN.Enabled = true; // Habilitar el campo de texto para ingresar el nombre de la colonia
                    cboVialidadN.Enabled = true; // Habilitar el campo de texto para ingresar el código postal
                    txtCodCalleN.Enabled = false; // Deshabilitar el campo de texto para el código de la calle, ya que se generará automáticamente
                btn_cancelar2.Enabled = true;
                btnGuardar.Enabled = true;

            }
            else if (MOVIMIENTO == 3)// ELIMINACION DE CALLE
            {
                
                    if (DGV_CALLE.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel de eliminacion de calles
                    txtNoZonaN.Enabled = false; // Deshabilitar el campo de texto para el número de calles, ya que se generará automáticamente
                    txtCodCalleN.Enabled = false; // deshabilitar el campo de texto para el código de la calle, ya que se generará automáticamente
                    cboVialidadN.Enabled = false; // Habilitar el campo de texto para ingresar la vialidad de la calle
                    txtCalleN.Enabled = false;
                btn_cancelar2.Enabled = true;
                btnGuardar.Enabled = true;
            }
            else // Si MOVIMIENTO es 0, significa que NO SE SELECCIONÓ UN PROCESO VÁLIDO
            {
                MessageBox.Show("ERROR, DEBE DE SELECCIONAR UN PROCESO, NUEVO, EDITAR O ELIMINAR.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Sale del método o procedimiento si no se ha seleccionado un proceso válido
            }

        }
        private void enviar_datos()
        {
            string VIALIDAD;
            txtNoZonaN.Text = Convert.ToString(DGV_CALLE.CurrentRow.Cells[0].Value).Trim();
            if (MOVIMIENTO == 1)//ALTA DE CALLE
            {
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "Select max(CodCalle) +1 AS numcalle";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  FROM CALLES";
                    con.cadena_sql_interno = con.cadena_sql_interno + " WHERE CodCalle <> 900";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   AND ZonaOrig =" + txtNoZonaN.Text;

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        txtCodCalleN.Text = con.leer_interno[0].ToString();  // colocar numero mayor de la calle a la caja de texto

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

            }
            else
            {
                txtCodCalleN.Text = Convert.ToString(DGV_CALLE.CurrentRow.Cells[1].Value).Trim();
            }
            VIALIDAD = Convert.ToString(DGV_CALLE.CurrentRow.Cells[2].Value).Trim();
            foreach (var item in cboVialidadN.Items)
            {
                string itemStr = item.ToString();
                if (itemStr.StartsWith(VIALIDAD))
                {
                    // Mostrar el valor completo del ComboBox
                    cboVialidadN.SelectedItem = item;
                    break; // Salir del bucle al encontrar la primera coincidencia
                }
            }

            txtCalleN.Text = Convert.ToString(DGV_CALLE.CurrentRow.Cells[4].Value).Trim();
            btnValidarZona.Enabled = false;
        }
        private void crud()//METODO PARA DAR DE ALTA, EDITAR O ELIMINAR UNA CALLE
        {
            try
            {

                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("SONG_CRUD_CALLES", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 1).Value = Program.PEstado;
                cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 1).Value = Program.municipioN;
                cmd.Parameters.Add("@ZONAORIG", SqlDbType.Int, 2).Value = zona_calle;
                cmd.Parameters.Add("@COD_CALLE", SqlDbType.Int, 2).Value = cod_calle;
                cmd.Parameters.Add("@TIPO_VIALIDAD", SqlDbType.VarChar, 2).Value = vialidad_calle;
                cmd.Parameters.Add("@NOMBRE_CALLE", SqlDbType.VarChar, 200).Value = nombre_calle;
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
            

            if (txtNoZonaN.Text == "")
            {
                MessageBox.Show("FAVOR DE INGRESAR LA ZONA", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoZonaN.Focus();
                return;
            }
            if (txtCodCalleN.Text == "")
            {
                MessageBox.Show("FAVOR DE INGRESAR EL NUMERO DE CODIGO DE LA CALLE", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCodCalleN.Focus();
                return;
            }
            if (txtCalleN.Text == "")
            {
                MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA CALLE", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCalleN.Focus();
                return;
            }
            if (cboVialidadN.Text == "")
            {
                MessageBox.Show("FAVOR DE SELECCIONAR LA VIALIDAD", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboVialidadN.Focus();
                return;
            }
            zona_calle = txtNoZonaN.Text.Trim();
            cod_calle = txtCodCalleN.Text.Trim();
            nombre_calle = txtCalleN.Text.Trim();
            vialidad_calle = cboVialidadN.Text.Trim().Substring(0, 3);
            int verificar = 0, verificar2 = 0;
            if (MOVIMIENTO == 1)//ALTA DE CALLE
            {
                DialogResult resp = MessageBox.Show("¿ESTA SEGURO DESEA CREAR LA CALLE?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    try
                    {
                        con.conectar_base_interno();
                        //se verifica si existe esa calle dada de alta
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT ZonaOrig";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             From CALLES ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "            Where ZonaOrig = " + zona_calle;
                        con.cadena_sql_interno = con.cadena_sql_interno + "              AND TipoVialid = " + util.scm(vialidad_calle);
                        con.cadena_sql_interno = con.cadena_sql_interno + "              AND NomCalle =" + util.scm(nombre_calle);
                        con.cadena_sql_interno = con.cadena_sql_interno + "            )";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             BEGIN";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT existe = 1";
                        con.cadena_sql_interno = con.cadena_sql_interno + "              End";
                        con.cadena_sql_interno = con.cadena_sql_interno + "         Else";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             BEGIN";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT existe = 2";
                        con.cadena_sql_interno = con.cadena_sql_interno + "         End";

                        con.open_c_interno();
                        con.cadena_sql_cmd_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            verificar = Convert.ToInt32(con.leer_interno[0].ToString());
                        }
                        con.cerrar_interno();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }
                    if (verificar == 1)
                    {
                        MessageBox.Show("NO SE PUEDE DAR DE ALTA LA CALLE DADO QUE YA SE ENCUENTRA CREADA", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento si la calle ya existe
                    }
                    else if (verificar == 2)
                    {
                        try
                        {
                            //verificamos si existe asignada el codigo de calle
                            con.conectar_base_interno();
                            con.cadena_sql_interno = "";
                            con.cadena_sql_interno = con.cadena_sql_interno + "IF EXISTS ( SELECT ZonaOrig";
                            con.cadena_sql_interno = con.cadena_sql_interno + "     From   CALLES ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "    Where   CodCalle = " + cod_calle;
                            con.cadena_sql_interno = con.cadena_sql_interno + "      AND   ZonaOrig = " + util.scm(zona_calle);
                            con.cadena_sql_interno = con.cadena_sql_interno + "           )";
                            con.cadena_sql_interno = con.cadena_sql_interno + "           BEGIN";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT existe = 1";
                            con.cadena_sql_interno = con.cadena_sql_interno + "           End";
                            con.cadena_sql_interno = con.cadena_sql_interno + "           Else";
                            con.cadena_sql_interno = con.cadena_sql_interno + "           BEGIN";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT existe = 2";
                            con.cadena_sql_interno = con.cadena_sql_interno + "           End";

                            con.open_c_interno();
                            con.cadena_sql_cmd_interno();
                            con.leer_interno = con.cmd_interno.ExecuteReader();

                            while (con.leer_interno.Read())
                            {
                                verificar2 = Convert.ToInt32(con.leer_interno[0].ToString());
                            }
                            con.cerrar_interno();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ERROR AL EJECUTAR LA CONSULTA: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            util.CapturarPantallaConInformacion(ex);
                            System.Threading.Thread.Sleep(500);
                            con.cerrar_interno();
                            return; // Retornar false si ocurre un error
                        }

                        if (verificar2 == 1)
                        {
                            MessageBox.Show("YA SE ENCUENTRA ASIGNADA EL CODIGO DE LA CALLE", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else if (verificar2 == 2)
                        {
                            crud(); // Llamar al método para realizar el CRUD de la calle
                            formaInicio(); // Llamar al método para reiniciar el formulario
                        }
                    }
                }
                if (resp == DialogResult.No)
                {
                    //no hacer nada
                    txtCalleN.Focus();
                    return; // Sale del método o procedimiento si el usuario no confirma la edición
                }
            }
            else if (MOVIMIENTO == 2)//EDITANDO CALLE
            {
                int NUM_CALLE = 0;
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "SELECT count(CODCALLE)";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  From PREDIOS ";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Where ZonaOrig = " + zona_calle;
                    con.cadena_sql_interno = con.cadena_sql_interno + "   And CodCalle =   " + cod_calle;

                    con.open_c_interno();
                    con.cadena_sql_cmd_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        NUM_CALLE = Convert.ToInt32(con.leer_interno[0].ToString());
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
                DialogResult resp = MessageBox.Show("¿SEGURO QUE DESEA EDITAR LA CALLE, SE AFECTARAN " + NUM_CALLE + " CLAVES CATASTRALES ?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    crud(); // Llamar al método para realizar el CRUD de la calle
                    formaInicio(); // Llamar al método para reiniciar el formulario
                }
                if (resp == DialogResult.No)
                {
                    //no hacer nada
                    return; // Sale del método o procedimiento si el usuario no confirma la edición
                }

            }
            else if (MOVIMIENTO == 3)// ELIMINACION DE CALLE
            {
                DialogResult resp = MessageBox.Show(" ¿SEGURO QUE DESEA ELIMINAR LA CALLE?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    int NUM_CALLE = 0;
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "SELECT count(CODCALLE)";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  From PREDIOS ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Where ZonaOrig = " + zona_calle;
                        con.cadena_sql_interno = con.cadena_sql_interno + "   And CodCalle =   " + cod_calle;

                        con.open_c_interno();
                        con.cadena_sql_cmd_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            NUM_CALLE = Convert.ToInt32(con.leer_interno[0].ToString());
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

                    if (NUM_CALLE != 0)
                    {
                        MessageBox.Show("NO SE PUEDE ELIMINAR LA CALLE DADO QUE SE ENCUENTRA UTILIZADO EN " + NUM_CALLE + " CLAVES CATASTRALES", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        crud(); // Llamar al método para realizar el CRUD de la calle
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
            btnGuardar.Enabled = false;
            btn_cancelar2.Enabled = false;
            txtCalleN.Text = ""; // Limpiar el campo 
            txtCodCalleN.Text = ""; // Limpiar el campo
            cboVialidadN.SelectedIndex = -1; // Limpiar el campo
            PNLFBUSCAR.Enabled = true; // Habilitar el panel de búsqueda 
            DGV_CALLE.DataSource = null;
            DGV_CALLE.Enabled = false; // Deshabilitar el DataGridView de resultados
            btnBuscar.Enabled = false; // Deshabilitar el botón de búsqueda para evitar múltiples clics
            //btnNuevo.Enabled = false;
            //btnEditar.Enabled = false;
            //btnBorrar.Enabled = false;
            txtCalleB.Text = "";
            txtCalleB.Enabled = true;
            txtCalleB.Focus();
            pnlCodCalle.Enabled = false;
            rbnZona.Checked = false; // Desmarcar el radio button de área emisora
            rbIdentiNombre.Checked = true; // Desmarcar el radio button de cuenta clave
            rbSimilarNombre.Checked = false; // Desmarcar el radio button 
            rbVialidad.Checked = false; // Desmarcar el radio button 
            rbCodCalle.Checked = false; // Desmarcar el radio button de código de calle
            txtCalleB.Text = ""; // Limpiar el campo de búsqueda 
            cboVialidadB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            lblNumRegistro.Text = "0";
            cboVialidadB.Enabled = false;
            btnConsulta_bus.BackColor = Color.Yellow;
            btnConsulta_bus.ForeColor = Color.Black;

        }

        private void cmd_cancelar2_Click(object sender, EventArgs e)
        {
            txtNoZonaN.Text = "";
            txtCodCalleN.Text = "";
            cboVialidadN.SelectedIndex = -1; // Desmarcar cualquier selección previa
            txtCalleN.Text = "";
            if (MOVIMIENTO == 1)
            {
                btnValidarZona.Enabled = true;
            }

            txtNoZonaN.Enabled = true; // Habilitar el campo de texto
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
            cboCodCalleB.Items.Clear();
            cboCodCalleB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            Cbo_ZonaB.Enabled = false; // Deshabilitar el combo box 
            rbnZona.Checked = false; // Desmarcar el radio button 
            rbIdentiNombre.Checked = false; // Desmarcar el radio button 
            rbSimilarNombre.Checked = false; // Desmarcar el radio button
            rbVialidad.Checked = false; // Desmarcar el radio button de
            rbCodCalle.Checked = false; // Desmarcar el radio button d
            txtCalleB.Text = ""; // Limpiar el campo de búsqueda 
            txtCalleB.Enabled = false; // Deshabilitar el campo 

            cboCodCalleB.Text = ""; // Limpiar el campo 
            cboCodCalleB.Enabled = false; // Deshabilitar el campo 
            DGV_CALLE.DataSource = null; // Limpiar la fuente de datos del DataGridView
            lblNumRegistro.Text = "0";
            pnlCodCalle.Enabled = false; // Deshabilitar el panel de código de calle
            cboVialidadB.SelectedIndex = -1; // Desmarcar cualquier selección previa
        }

        private void rbnNoCol_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnZona.Checked == true)
            {
                Cbo_ZonaB.Items.Clear();
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   Select DISTINCT (ZonaOrig)  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM Calles";


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
                pnlCodCalle.Enabled = true;
                Cbo_ZonaB.Focus();
            }
        }

        private void rbIdentiNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdentiNombre.Checked == true)
            {
                txtCalleB.Text = "";
                txtCalleB.Enabled = true;
                txtCalleB.Focus();
            }
        }

        private void rbSimilarNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSimilarNombre.Checked == true)
            {
                txtCalleB.Text = "";
                txtCalleB.Enabled = true;
                txtCalleB.Focus();
            }
        }

        private void rbCP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVialidad.Checked == true)
            {
                cboCodCalleB.Text = "";
                cboCodCalleB.Enabled = true;
                cboCodCalleB.Focus();
            }
        }
        private void cajasColor()
        {
            txtCalleB.Enter += util.TextBox_Enter;
            cboCodCalleB.Enter += util.Cbo_Box_Enter;
            txtCalleN.Enter += util.TextBox_Enter;
            txtCodCalleN.Enter += util.TextBox_Enter;
            Cbo_ZonaB.Enter += util.Cbo_Box_Enter;
            cboVialidadB.Enter += util.Cbo_Box_Enter;
            txtNoZonaN.Enter += util.TextBox_Enter;
            cboVialidadN.Enter += util.Cbo_Box_Enter;

        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBorrar7_Click(object sender, EventArgs e)
        {
            rbnZona.Checked = false; // Desmarcar el radio button 
            Cbo_ZonaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboCodCalleB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            pnlCodCalle.Enabled = false; // Deshabilitar el panel de código de calle

        }

        private void cmdLimpiaCiudadano_Click(object sender, EventArgs e)
        {
            rbIdentiNombre.Checked = false; // Desmarcar el radio button
            rbSimilarNombre.Checked = false; // Desmarcar el radio button d
            txtCalleB.Text = ""; // Limpiar el campo de búsqueda 
            txtCalleB.Enabled = false; // Deshabilitar el campo de búsqueda 
        }

        private void cmdLimpiarRFC_Click(object sender, EventArgs e)
        {
            rbVialidad.Checked = false; // Desmarcar el radio button
            cboCodCalleB.Text = ""; // Limpiar el campo 
        }

        private void btnValidarZona_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnValidarZona, "VALIDAR ZONA");
        }

        private void txtNoZonaN_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void txtCodCalleN_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void btnMinimizar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnMinimizar, "MINIMIZAR PANTALLA");
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtCalleB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consulta();
            }
        }

        private void Cbo_ZonaB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consulta();
            }
        }

        private void cboCodCalleB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consulta();
            }
        }

        private void cboVialidadB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                consulta();
            }
        }
        private void validarZona()
        {
            string numero_zona;
            int verificar = 0;
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
                    con.cadena_sql_interno = con.cadena_sql_interno + "IF EXISTS (SELECT ZonaOrig";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     From CALLES ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    Where ZonaOrig = " + numero_zona + " )";
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

                        con.cadena_sql_interno = "Select max(CodCalle) +1 AS numcalle";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  FROM CALLES";
                        con.cadena_sql_interno = con.cadena_sql_interno + " WHERE CodCalle <> 900";
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND ZonaOrig =" + numero_zona;

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            txtCodCalleN.Text = con.leer_interno[0].ToString();                     // colocar numero mayor de la calle a la caja de texto

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
                    txtCodCalleN.Enabled = false;
                    cboVialidadN.Enabled = true;
                    txtCalleN.Enabled = true;
                    txtNoZonaN.Enabled = false;
                    btnValidarZona.Enabled = false;
                    MessageBox.Show("SE SELECCIONO LA ZONA " + txtNoZonaN.Text + " Y EL CODIGO DE CALLE: " + txtCodCalleN.Text, "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCalleN.Focus();


                }
                else if (verificar == 2)
                {
                    DialogResult resp = MessageBox.Show("NO EXISTE LA ZONA COLOCADA, SE AGREGARA DICHA ZONA, ¿DESEA CONTINUAR CON EL PROCESO?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resp == DialogResult.Yes)
                    {
                        txtCodCalleN.Text = "1";
                        txtCodCalleN.Enabled = false;
                        cboVialidadN.Enabled = true;
                        txtCalleN.Enabled = true;
                        txtNoZonaN.Enabled = false;
                        btnValidarZona.Enabled = false;
                        txtCodCalleN.Focus();
                    }
                }
            }
        }

        private void txtNoZonaN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                validarZona();
            }
        }

        private void btnValidarZona_Click(object sender, EventArgs e)
        {
            validarZona();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rbCodCalle.Checked = false; // Desmarcar el radio button de código de calle
            cboCodCalleB.SelectedIndex = -1; // Desmarcar cualquier selección previa
        }

        private void rbCodCalle_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCodCalle.Checked == true)
            {
                if (Cbo_ZonaB.Text.Trim() == "")
                {
                    MessageBox.Show("FAVOR DE SELECCIONAR UNA ZONA PRIMERO", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cbo_ZonaB.Focus();
                    rbCodCalle.Checked = false;
                    return;
                }
                else
                {
                    cboCodCalleB.Items.Clear();
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   Select DISTINCT (CodCalle)  ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     FROM Calles";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE ZONAORIG = " + Cbo_ZonaB.Text.Trim();

                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();


                        while (con.leer_interno.Read())
                        {
                            cboCodCalleB.Items.Add(con.leer_interno[0].ToString().Trim());                // 

                        }
                        cboCodCalleB.Enabled = true;
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
