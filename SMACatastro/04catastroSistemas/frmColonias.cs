using AccesoBase;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Utilerias;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;

namespace SMACatastro
{
    public partial class frmColonias : Form
    {
        int MOVIMIENTO = 0;
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();

        ////////////////////////////////////////////////////////////
        ///////////////// -------PARA ARRASTRAR EL PANEL 
        ////////////////////////////////////////////////////////////
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public frmColonias()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RECUERDE QUE PUEDE GENERAR UNA NUEVA COLONIA BASANDOSE EN CUALQUIER REGISTRO DE LA TABLA, DANDOLE DOBLE CLIC", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PNLNEW.Enabled = true; // Habilitar el panel para crear un nuevo concept
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Select max(Colonia) +1 AS numcol";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM COLONIAS";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    txtNoColoniaN.Text = con.leer_interno[0].ToString();                     // colocar numero mayor de la colonia a la caja de texto

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
            //agregamos el las cuentas claves de COLONIAs al combo box

            MOVIMIENTO = 1; // Indica que se está creando un nuevo COLONIA
            txtNombreColoniaN.Enabled = true; // Deshabilitar el botón de nuevo para evitar duplicados
            txtNoColoniaN.Enabled = false; // Deshabilitar el campo de texto para el número de colonia, ya que se generará automáticamente
            txtCPN.Enabled = true; // Habilitar el campo de texto para ingresar el código postal
            txtNombreColoniaN.Focus(); // Enfocar el campo de texto para ingresar el COLONIA
            btnEditar.Enabled = false; // Deshabilitar el botón de editar
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar COLONIA
            btnGuardar.Enabled = true; // Habilitar el botón de guardar
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo para evitar duplicados
            
            btncancelar2.Enabled = true; // Habilitar el botón de cancelar
            btnBuscar.Enabled = true; // Habilitar el botón de buscar
            DGV_COLONIAS.Enabled = true; // 
            cargar_datagrid_colonias(); // Cargar los datos en el DataGridView de COLONIAs inicial
            lbl_titulo.Text = "PROCESO DE CREACION "; // Establecer el título del formulario
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MOVIMIENTO = 2; // Indica que se está EDITANDO un colonia
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo colonia
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar colonia
            MessageBox.Show("SELECCIONA LA COLONIA QUE DESEA EDITAR, DANDO DOBLE CLIC DENTRO DE LA TABLA", "EDICION DE COLONIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cargar_datagrid_colonias(); // Cargar los datos en el DataGridView de COLONIAs inicial
            lbl_titulo.Text = "PROCESO DE EDICION"; // Establecer el título del formulario
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnBuscar.Enabled = true; // Habilitar el botón de búsqueda de COLONIAs
            DGV_COLONIAS.Enabled = true; // Habilitar la grilla de resultados para mostrar los COLONIAs existentes
            DGV_COLONIAS.Focus(); // Enfocar el DataGridView de resultados para que el usuario pueda seleccionar un COLONIA a editar
            btnGuardar.Enabled = true; // Habilitar el botón de guardar
            btncancelar2.Enabled = true; // Habilitar el botón de cancelar
        }
        private void formaInicio()
        {
            // Inicializar el formulario y cargar los datos necesarios
            MOVIMIENTO = 0; // Indica que se está creando un nuevo COLONIA
            // Agregar las áreas emisoras al combo box
            Cbo_NoColoniaB.Items.Clear();
            Cbo_NoColoniaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            rbnNoCol.Checked = false; // Desmarcar el radio button de área emisora
            rbIdentiNombre.Checked = false; // Desmarcar el radio button de cuenta clave
            rbSimilarNombre.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            rbCP.Checked = false; // Desmarcar el radio button de COLONIA similar
            btnConsulta_bus.BackColor = Color.FromArgb(55, 61, 69);
            btnConsulta_bus.ForeColor = Color.White;

            // Agregar las cuentas claves de COLONIAs al combo box

            txtColoniaB.Text = ""; // Limpiar el campo de búsqueda de COLONIAs
            txtCPB.Text = ""; // Limpiar el campo de COLONIA
            txtNoColoniaN.Text = ""; // Limpiar el campo de contable

            PNLNEW.Enabled = false; // Deshabilitar el panel de creación de nuevos COLONIAs
            PNLFBUSCAR.Enabled = false; // Deshabilitar el panel de búsqueda de COLONIAs
            Cbo_NoColoniaB.Enabled = false; // Deshabilitar el combo box de área emisora

            DGV_COLONIAS.DataSource = null; // Limpiar el DataGridView de resultados
            txtColoniaB.Enabled = false; // Deshabilitar el combo box de cuenta clave
            btnBorrar.Enabled = true; // Deshabilitar el botón de borrar COLONIA
            txtCPB.Enabled = false; // Deshabilitar el campo de búsqueda de COLONIAs
            btnNuevo.Enabled = true; // Habilitar el botón de nuevo COLONIA
            btnBuscar.Enabled = false; // Habilitar el botón de búsqueda de COLONIAs
            DGV_COLONIAS.Enabled = false; // Habilitar el DataGridView de resultados
            
            btnEditar.Enabled = true;
            lbl_titulo.Text = ""; // Establecer el título del formulario
            txtNombreColoniaN.Text = ""; // Limpiar el campo de clave de dirección
            txtCPN.Text = ""; // Limpiar el campo de clave de área


        }
        private void cargar_datagrid_colonias()
        {
            // Cargar los datos en el DataGridView de COLONIAs
            try
            {
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT Colonia , NomCol , cCPCol ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM COLONIAS";
                con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY Colonia";

                //llenamos la grilla con los resultados de la consulta
                DataTable LLENAR_GRID_1 = new DataTable();
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO
                {
                    MessageBox.Show("NO SE ENCONTRO DATOS DE LA BUSQUEDA", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DGV_COLONIAS.DataSource = LLENAR_GRID_1;
                    con.cerrar_interno();
                    DGV_COLONIAS.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    DGV_COLONIAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DGV_COLONIAS.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                    DGV_COLONIAS.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    foreach (DataGridViewColumn columna in DGV_COLONIAS.Columns)
                    {
                        columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    // Configuración de selección
                    DGV_COLONIAS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // Deshabilitar edición
                    DGV_COLONIAS.ReadOnly = true;
                    // Estilos visuales
                    DGV_COLONIAS.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    DGV_COLONIAS.DefaultCellStyle.SelectionForeColor = Color.Black;

                    // Configurar todas las columnas para que no se puedan redimensionar
                    DGV_COLONIAS.AllowUserToResizeColumns = false;

                    DGV_COLONIAS.Columns[0].Width = 170;                         // NUMERO DE COLONIA        
                    DGV_COLONIAS.Columns[1].Width = 488;                         // NOMBRE DE COLONIA
                    DGV_COLONIAS.Columns[2].Width = 170;                         // CODIGO POSTAL

                    DGV_COLONIAS.Columns[0].Name = "COLONIA";                    // NUMERO DE COLONIA        
                    DGV_COLONIAS.Columns[1].Name = "NUMERO DE COLONIA";          // NOMBRE DE COLONIA
                    DGV_COLONIAS.Columns[2].Name = "CODIGO POSTAL";              // CODIGO POSTAL

                    DGV_COLONIAS.Columns[0].HeaderText = "NO. COLONIA";         // NUMERO DE COLONIA         
                    DGV_COLONIAS.Columns[1].HeaderText = "NOMBRE COLONIA";      // NOMBDE DE COLONIA
                    DGV_COLONIAS.Columns[2].HeaderText = "CODIGO POSTAL";       // CODIGO POSTAL

                    DGV_COLONIAS.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_COLONIAS.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_COLONIAS.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;              // 

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

        private void btnConsulta_bus_Click(object sender, EventArgs e)
        {
            if (Cbo_NoColoniaB.Text == "")
            {
                if (txtColoniaB.Text == "")
                {
                    if (txtCPB.Text == "")
                    {
                        MessageBox.Show("NO SE TIENE OPCIONES DE BUSQUEDA", "ERROR", MessageBoxButtons.OK);
                        return;
                    }
                }
            }

            if (rbnNoCol.Checked == true) { if (Cbo_NoColoniaB.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR UNA COLONIA CORRECTA", "ERROR", MessageBoxButtons.OK); Cbo_NoColoniaB.Focus(); return; } }

            if (rbCP.Checked == true) { if (txtCPB.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELECCIONAR UNA CUENTA CLAVE", "ERROR", MessageBoxButtons.OK); txtCPB.Focus(); return; } }

            if (rbIdentiNombre.Checked == true) { if (txtColoniaB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA COLONIA CORRECTA", "ERROR", MessageBoxButtons.OK); txtColoniaB.Focus(); return; } }
            if (rbSimilarNombre.Checked == true) { if (txtColoniaB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA COLONIA CORRECTA", "ERROR", MessageBoxButtons.OK); txtColoniaB.Focus(); return; } }

            // SE ARMA EL query DE BUSQUEDA
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT Colonia , NomCol , cCPCol ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    FROM COLONIAS";
            con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE MUNICIPIO = " + Program.municipioN;
            //NUMERO DE COLONIA
            if (rbnNoCol.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "          AND Colonia =" + Cbo_NoColoniaB.Text.Trim(); }
            //nombre de colonia
            if (rbIdentiNombre.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "    AND NomCol ='" + txtColoniaB.Text.Trim() + "'"; }
            if (rbSimilarNombre.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND NomCol LIKE '%" + txtColoniaB.Text.Trim() + "%'"; }
            //CODIGO POSTAL
            if (rbCP.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "              AND cCPCol ='" + txtCPB.Text.Trim() + "'"; }
            con.cadena_sql_interno = con.cadena_sql_interno + "                                     ORDER BY Colonia";
            //llenamos la grilla con los resultados de la consulta
            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            if (da.Fill(LLENAR_GRID_1) == 0)     //COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO
            {
                MessageBox.Show("NO SE ENCONTRO DATOS DE LA BUSQUEDA", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DGV_COLONIAS.DataSource = LLENAR_GRID_1;
                con.cerrar_interno();
                DGV_COLONIAS.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                DGV_COLONIAS.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                DGV_COLONIAS.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                DGV_COLONIAS.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                foreach (DataGridViewColumn columna in DGV_COLONIAS.Columns)
                {
                    columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                // Configuración de selección
                DGV_COLONIAS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Deshabilitar edición
                DGV_COLONIAS.ReadOnly = true;
                // Estilos visuales
                DGV_COLONIAS.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                DGV_COLONIAS.DefaultCellStyle.SelectionForeColor = Color.Black;

                // Configurar todas las columnas para que no se puedan redimensionar
                DGV_COLONIAS.AllowUserToResizeColumns = false;

                DGV_COLONIAS.Columns[0].Width = 200;                         // NUMERO DE COLONIA        
                DGV_COLONIAS.Columns[1].Width = 488;                         // NOMBRE DE COLONIA
                DGV_COLONIAS.Columns[2].Width = 200;                         // CODIGO POSTAL

                DGV_COLONIAS.Columns[0].HeaderText = "NUMERO COLONIA";         // NUMERO DE COLONIA         
                DGV_COLONIAS.Columns[1].HeaderText = "NOMBRE COLONIA";      // NOMBDE DE COLONIA
                DGV_COLONIAS.Columns[2].HeaderText = "CODIGO POSTAL";       // CODIGO POSTAL

                DGV_COLONIAS.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_COLONIAS.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_COLONIAS.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;              // 

                int CONTEO;
                CONTEO = DGV_COLONIAS.Rows.Count - 1;
                lblNumRegistro.Text = CONTEO.ToString();
                DGV_COLONIAS.Enabled = true; // Habilitar la grilla de resultados
             

            }
            con.cerrar_interno();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            MOVIMIENTO = 3; // Indica que se está ELIMINANDO un concepto
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo COLONIA
            MessageBox.Show("SELECCIONA LA COLONIA QUE DESEA ELIMINAR DENTRO DE LA TABLA DANDO DOBLE CLIC EN LA MISMA", "ELIMINACION DE COLONIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lbl_titulo.Text = "PROCESO DE ELIMINACION "; // Establecer el título del formulario
            cargar_datagrid_colonias(); // Cargar los datos en el DataGridView de COLONIAs inicial
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnBuscar.Enabled = true; // 
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar
            DGV_COLONIAS.Enabled = true; // Habilitar la grilla de resultados para mostrar los COLONIAs existentes
            DGV_COLONIAS.Focus(); // Enfocar el DataGridView de resultados para que el usuario pueda seleccionar un COLONIA a editar
        }

        private void DGV_COLONIAS_DoubleClick(object sender, EventArgs e)
        {
            if (MOVIMIENTO == 1)//ALTA DE COLONIA
            {
              if (DGV_COLONIAS.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel de creación de nuevos COLONIAs
                    txtNoColoniaN.Enabled = false; // Deshabilitar el campo de texto para el número de colonia, ya que se generará automáticamente
                    txtNombreColoniaN.Enabled = true; // Habilitar el campo de texto para ingresar el nombre de la colonia
                    txtCPN.Enabled = true; // Habilitar el campo de texto para ingresar el código postal
                
            }
            else if (MOVIMIENTO == 2)//EDITANDO COLONIA
            {
              
                    if (DGV_COLONIAS.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel de creación de nuevos COLONIAs
                    txtNoColoniaN.Enabled = false; // Deshabilitar el campo de texto para el número de colonia, ya que se generará automáticamente
                    txtNombreColoniaN.Enabled = true; // Habilitar el campo de texto para ingresar el nombre de la colonia
                    txtCPN.Enabled = true; // Habilitar el campo de texto para ingresar el código postal
                
            }
            else if (MOVIMIENTO == 3)// ELIMINACION DE COLONIA
            {
                
                    if (DGV_COLONIAS.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel de creación de nuevos COLONIAs
                    txtNoColoniaN.Enabled = false; // Deshabilitar el campo de texto para el número de colonia, ya que se generará automáticamente
                    txtNombreColoniaN.Enabled = false; // Habilitar el campo de texto para ingresar el nombre de la colonia
                    txtCPN.Enabled = false; // Habilitar el campo de texto para ingresar el código postal
                
            }
            else // Si MOVIMIENTO es 0, significa que NO SE SELECCIONÓ UN PROCESO VÁLIDO
            {
                MessageBox.Show("ERROR, DEBE DE SELECCIONAR UN PROCESO, NUEVO, EDITAR O ELIMINAR.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Sale del método o procedimiento si no se ha seleccionado un proceso válido
            }

        }
        private void enviar_datos()
        {
            if (MOVIMIENTO != 1)//ALTA DE COLONIA
            {
                txtNoColoniaN.Text = Convert.ToString(DGV_COLONIAS.CurrentRow.Cells[0].Value).Trim();
            }
            txtNombreColoniaN.Text = Convert.ToString(DGV_COLONIAS.CurrentRow.Cells[1].Value).Trim();
            txtCPN.Text = Convert.ToString(DGV_COLONIAS.CurrentRow.Cells[2].Value).Trim();

        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre_colonia, codigo_postal, numero_colonia;
            nombre_colonia = txtNombreColoniaN.Text.Trim();
            codigo_postal = txtCPN.Text.Trim();
            numero_colonia = txtNoColoniaN.Text.Trim();
            string fecha_actual = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");

            int verificar = 0;
            if (nombre_colonia == "")
            {
                MessageBox.Show("INGRESE EL NOMBRE DE LA COLONIA", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombreColoniaN.Focus();
                return;


            }
            if (codigo_postal == "" || codigo_postal.Length < 5)
            {
                MessageBox.Show("INGRESE EL CODIGO POSTAL DE LA COLONIA CORRECTO", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCPN.Focus();
                return;
            }
            if (MOVIMIENTO == 1)//ALTA DE COLONIA
            {
                DialogResult resp = MessageBox.Show("¿ESTA SEGURO DE CREAR LA COLONIA?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (resp == DialogResult.Yes)
                {
                    try
                    {
                        con.conectar_base_interno();
                        con.open_c_interno();

                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  IF EXISTS (SELECT *";
                        con.cadena_sql_interno = con.cadena_sql_interno + "              From COLONIAS ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             Where NomCol = " + util.scm(nombre_colonia) + ")";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             BEGIN";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT existe = 1";
                        con.cadena_sql_interno = con.cadena_sql_interno + "              End";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             Else";
                        con.cadena_sql_interno = con.cadena_sql_interno + "            BEGIN";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                 SELECT existe = 2";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             End";

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
                        MessageBox.Show("NO SE PUEDE DAR DE ALTA UNA COLONIA YA CREADA", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (verificar == 2)
                    {
                        try
                        {

                            con.conectar_base_interno();
                            con.open_c_interno();
                            SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandType = System.Data.CommandType.Text;

                            con.cadena_sql_interno = "";
                            con.cadena_sql_interno = con.cadena_sql_interno + "            INSERT INTO COLONIAS ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       (";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Estado, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Municipio, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Colonia, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       NomCol,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       cCPCol";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       )";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        Values";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       (";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        15";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + Program.municipioN;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + numero_colonia;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(nombre_colonia);
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + codigo_postal;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        )";

                            con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";

                            con.cadena_sql_interno = con.cadena_sql_interno + "             INSERT INTO COLONIAS_H ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       (";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Estado, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Municipio, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Colonia, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       NomCol,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       cCPCol,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       USARIO,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       FECHA,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       MOVIMIENTO";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       )";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        Values";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       (";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        15";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + Program.municipioN;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + numero_colonia;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(nombre_colonia);
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + codigo_postal;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(Program.nombre_usuario);//colocar nombre de usuario
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(fecha_actual);
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(Program.ALTA);
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        )";

                            cmd.CommandText = con.cadena_sql_interno;
                            cmd.Connection = con.cnn_interno;
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                            con.cerrar_interno();

                            MessageBox.Show("SE REALIZO EL ALTA CORRECTAMENTE", "INFORMATIVO", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            formaInicio();
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
            }
            else if (MOVIMIENTO == 2)//EDITANDO COLONIA
            {
                int NUM_COLONIA = 0;
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "SELECT count(colonia)";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  From PREDIOS ";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Where Colonia = " + numero_colonia;

                    //int existe;
                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        NUM_COLONIA = Convert.ToInt32(con.leer_interno[0].ToString());

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
                DialogResult resp = MessageBox.Show("¿SEGURO QUE DESEA EDITAR LA COLONIA, SE AFECTARAN " + NUM_COLONIA + " CLAVES CATASTRALES?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (resp == DialogResult.Yes)
                {
                    try
                    {
                        con.conectar_base_interno();
                        con.open_c_interno();

                        SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                        cmd.CommandType = System.Data.CommandType.Text;

                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                    UPDATE COLONIAS ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       SET NomCol    =" + "'" + nombre_colonia + "'";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                          ,cCPcol = " + codigo_postal;
                        con.cadena_sql_interno = con.cadena_sql_interno + "                     WHERE Estado    =" + 15;
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       AND Municipio =" + Program.municipioN;
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       AND Colonia   =" + numero_colonia;

                        con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";

                        con.cadena_sql_interno = con.cadena_sql_interno + "             INSERT INTO COLONIAS_H ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       (";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       Estado, ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       Municipio, ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       Colonia, ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       NomCol,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       cCPCol,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       USARIO,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       FECHA,";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       MOVIMIENTO";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       )";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        Values";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                       (";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        15";
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + Program.municipioN;
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + numero_colonia;
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(nombre_colonia);
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + codigo_postal;
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(Program.nombre_usuario);//colocar nombre de usuario
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(fecha_actual);
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(Program.MODIFICACION);
                        con.cadena_sql_interno = con.cadena_sql_interno + "                        )";

                        cmd.CommandText = con.cadena_sql_interno;
                        cmd.Connection = con.cnn_interno;
                        cmd.CommandTimeout = 300;
                        cmd.ExecuteNonQuery();
                        con.cerrar_interno();

                        MessageBox.Show("SE REALIZO LA MODIFICACION CORRECTAMENTE", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        formaInicio();
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
                if (resp == DialogResult.No)
                {
                    //no hacer nada
                    txtNombreColoniaN.Focus();
                    return; // Sale del método o procedimiento si el usuario no confirma la edición

                }
            }
            else if (MOVIMIENTO == 3)// ELIMINACION DE COLONIA
            {
                DialogResult resp = MessageBox.Show("¿SEGURO QUE DESEA ELIMINAR LA COLONIA?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


                if (resp == DialogResult.Yes)
                {
                    //realizar la eliminacion
                    //se verifica si existe la colonia en la tabla de predios
                    int NUM_COLONIA = 0;
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "SELECT count(colonia)";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  From PREDIOS ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Where Colonia = " + numero_colonia;

                        //int existe;
                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();

                        while (con.leer_interno.Read())
                        {
                            NUM_COLONIA = Convert.ToInt32(con.leer_interno[0].ToString());

                        }
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
                    if (NUM_COLONIA != 0)
                    {
                        MessageBox.Show("NO SE PUEDE ELIMINAR LA COLONIA DADO QUE SE ENCUENTRA UTILIZADO EN " + NUM_COLONIA + " CLAVES CATASTRALES", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento si la colonia está en uso

                    }
                    else
                    {
                        try
                        {
                            con.conectar_base_interno();
                            con.open_c_interno();
                            SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                            cmd.CommandType = System.Data.CommandType.Text;

                            con.cadena_sql_interno = con.cadena_sql_interno + "             INSERT INTO COLONIAS_H ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       (";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Estado, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Municipio, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       Colonia, ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       NomCol,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       cCPCol,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       USARIO,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       FECHA,";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       MOVIMIENTO";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       )";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        Values";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                       (";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        15";
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + Program.municipioN;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + numero_colonia;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(nombre_colonia);
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + codigo_postal;
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(Program.nombre_usuario);//colocar nombre de usuario
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(fecha_actual);
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        ," + util.scm(Program.BAJA);
                            con.cadena_sql_interno = con.cadena_sql_interno + "                        )";

                            con.cadena_sql_interno = con.cadena_sql_interno + "   SET NOCOUNT ON ";

                            con.cadena_sql_interno = con.cadena_sql_interno + " DELETE ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM COLONIAS ";
                            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE Estado = " + 15;
                            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Municipio =" + Program.municipioN;
                            con.cadena_sql_interno = con.cadena_sql_interno + "    AND COLONIA  =" + numero_colonia;

                            cmd.CommandText = con.cadena_sql_interno;
                            cmd.Connection = con.cnn_interno;
                            cmd.CommandTimeout = 300;
                            cmd.ExecuteNonQuery();
                            con.cerrar_interno();

                            MessageBox.Show("SE REALIZO LA BAJA CORRECTAMENTE", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            formaInicio();
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

            PNLFBUSCAR.Enabled = true; // Habilitar el panel de búsqueda de COLONIAs
            DGV_COLONIAS.DataSource = null;
            DGV_COLONIAS.Enabled = false; // Deshabilitar el DataGridView de resultados
            btnBuscar.Enabled = false; // Deshabilitar el botón de búsqueda para evitar múltiples clics
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo COLONIA
            rbSimilarNombre.Checked = true;
            btnConsulta_bus.BackColor = Color.Yellow;
            btnConsulta_bus.ForeColor = Color.Black;

        }

        private void cmd_cancelar2_Click(object sender, EventArgs e)
        {
            if (MOVIMIENTO != 1)
            {
                txtNoColoniaN.Text = "";

            }
            txtNombreColoniaN.Text = "";
            txtCPN.Text = "";

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
            toolTip.SetToolTip(btncancelar2, "CANCELA PROCESO");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Cbo_NoColoniaB.Items.Clear();
            Cbo_NoColoniaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            Cbo_NoColoniaB.Enabled = false; // Deshabilitar el combo box de área emisora
            rbnNoCol.Checked = false; // Desmarcar el radio button de área emisora
            rbIdentiNombre.Checked = false; // Desmarcar el radio button de cuenta clave
            rbSimilarNombre.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            rbCP.Checked = false; // Desmarcar el radio button de COLONIA similar

            txtColoniaB.Text = ""; // Limpiar el campo de búsqueda de COLONIAs
            txtColoniaB.Enabled = false; // Deshabilitar el campo de búsqueda de COLONIAs

            txtCPB.Text = ""; // Limpiar el campo de COLONIA
            txtCPB.Enabled = false; // Deshabilitar el campo de COLONIA
            DGV_COLONIAS.DataSource = null; // Limpiar la fuente de datos del DataGridView
            lblNumRegistro.Text = "0";

        }

        private void rbnNoCol_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnNoCol.Checked == true)
            {
                Cbo_NoColoniaB.Items.Clear();
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   Select Colonia";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM COLONIAS";
                    con.cadena_sql_interno = con.cadena_sql_interno + "    WHERE Colonia <> 900";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY COLONIA";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();


                    while (con.leer_interno.Read())
                    {
                        Cbo_NoColoniaB.Items.Add(con.leer_interno[0].ToString().Trim());                // 

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
                Cbo_NoColoniaB.Enabled = true;
                Cbo_NoColoniaB.Focus();
            }
        }

        private void rbIdentiNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdentiNombre.Checked == true)
            {
                txtColoniaB.Text = "";
                txtColoniaB.Enabled = true;
                txtColoniaB.Focus();
            }
        }

        private void rbSimilarNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSimilarNombre.Checked == true)
            {
                txtColoniaB.Text = "";
                txtColoniaB.Enabled = true;
                txtColoniaB.Focus();
            }
        }

        private void rbCP_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCP.Checked == true)
            {
                txtCPB.Text = "";
                txtCPB.Enabled = true;
                txtCPB.Focus();
            }
        }
        private void cajasColor()
        {
            txtColoniaB.Enter += util.TextBox_Enter;
            txtCPB.Enter += util.TextBox_Enter;
            txtNombreColoniaN.Enter += util.TextBox_Enter;
            txtCPN.Enter += util.TextBox_Enter;
            Cbo_NoColoniaB.Enter += util.Cbo_Box_Enter;
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBorrar7_Click(object sender, EventArgs e)
        {
            rbnNoCol.Checked = false; // Desmarcar el radio button de área emisora
            Cbo_NoColoniaB.Items.Clear();
            Cbo_NoColoniaB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            Cbo_NoColoniaB.Enabled = false;
        }

        private void cmdLimpiaCiudadano_Click(object sender, EventArgs e)
        {
            rbIdentiNombre.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            rbSimilarNombre.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            txtColoniaB.Text = ""; // Limpiar el campo de búsqueda de COLONIAs
            txtColoniaB.Enabled = false;
        }

        private void cmdLimpiarRFC_Click(object sender, EventArgs e)
        {
            rbCP.Checked = false; // Desmarcar el radio button de COLONIA similar
            txtCPB.Text = ""; // Limpiar el campo de COLONIA
            txtCPB.Enabled = false;
        }

        private void txtCPB_KeyPress(object sender, KeyPressEventArgs e)
        {
            util.soloNumero(e);
        }

        private void btnGuardar_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnGuardar, "GUARDAR");
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
    }
}
