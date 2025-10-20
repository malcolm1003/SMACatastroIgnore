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
    public partial class frmLocalidades : Form
    {
        int MOVIMIENTO = 0;
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();
        string nombre_localidad, numero_localidad;
        int validacion = 0;
        ////////////////////////////////////////////////////////////
        ///////////////// -------PARA ARRASTRAR EL PANEL 
        ////////////////////////////////////////////////////////////
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public frmLocalidades()
        {
            InitializeComponent();
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("RECUERDE QUE PUEDE GENERAR UNA NUEVA LOCALIDAD BASANDOSE EN CUALQUIER REGISTRO DE LA TABLA, DANDOLE DOBLE CLIC", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PNLNEW.Enabled = true; // Habilitar el panel para crear un nuevo LOCALIDAD
            MOVIMIENTO = 1; // Indica que se está creando un nuevo LOCALIDAD
            txtNoLocN.Enabled = false; // Deshabilitar el botón de nuevo para evitar duplicados
            cargar_datagrid_localidades(); // Cargar los datos en el DataGridView inicial
            btnEditar.Enabled = false; // Deshabilitar el botón de editar
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar LOCALIDAD
            btnGuardar.Enabled = true; // Habilitar el botón de guardar
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo para evitar duplicados
            btnCancelar.Enabled = true; // Habilitar el botón de cancelar
            btnBuscar.Enabled = true; // Habilitar el botón de buscar
            DGV_LOCALIDADES.Enabled = true; // 
            CARGAR_LOCALIDAD_MAYOR();
            txtLocN.Enabled = true; // Habilitar el campo de texto para ingresar la nueva localidad
            lbl_titulo.Text = "PROCESO DE CREACION"; // Establecer el título del formulario
            txtLocN.Focus(); // Enfocar el campo de texto para ingresar la localidad
        }
        private void CARGAR_LOCALIDAD_MAYOR()
        {
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  Select max(Localidad) +1 AS NumLoc";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM LOCALIDADES";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();


                while (con.leer_interno.Read())
                {
                    txtNoLocN.Text = con.leer_interno[0].ToString();                     // colocar numero mayor de la localidad a la caja de texto

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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            MOVIMIENTO = 2; // Indica que se está EDITANDO 
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo 
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar 
            MessageBox.Show("SELECCIONA LA LOCALIDAD QUE DESEA EDITAR, DANDO DOBLE CLIC DENTRO DE LA TABLA", "EDICION DE CALLES", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lbl_titulo.Text = "PROCESO DE EDITAR"; // Establecer el título del formulario
            cargar_datagrid_localidades(); // Cargar los datos en el DataGridView inicial
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnBuscar.Enabled = true; // Habilitar el botón de búsqueda  
            DGV_LOCALIDADES.Enabled = true; // Habilitar la grilla de resultados para mostrar los  existentes
            DGV_LOCALIDADES.Focus(); // Enfocar el DataGridView de resultados para que el usuario pueda seleccionar elemento a editar
            btnGuardar.Enabled = true;
            btncancelar2.Enabled = true;
        }
        private void formaInicio()
        {
            // Inicializar el formulario y cargar los datos necesarios
            MOVIMIENTO = 0; // se inicializa la variable MOVIMIENTO en 0, indicando que no se ha seleccionado ninguna acción (nuevo, editar, eliminar)
            // Agregar las áreas emisoras al combo box
            Cbo_LocB.Items.Clear();
            Cbo_LocB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            rbNoLoc.Checked = false; // Desmarcar el radio button
            rbIdentiNombre.Checked = false; // Desmarcar el radio button de nombre identico
            rbSimilarNombre.Checked = false; // Desmarcar el radio button de nombre similar
            txtLocB.Text = ""; // Limpiar el campo de búsqueda de localidades
            txtNoLocN.Text = ""; // Limpiar el campo de numero de localidad
            PNLNEW.Enabled = false; // Deshabilitar el panel de creación de localidades
            PNLFBUSCAR.Enabled = false; // Deshabilitar el panel de búsqueda 
            Cbo_LocB.Enabled = false; // Deshabilitar el combo box de localidades
           
            DGV_LOCALIDADES.Enabled = false; // Deshabilitar el DataGridView de resultados
            
            txtLocB.Enabled = false; // 
            btnBorrar.Enabled = true; // Deshabilitar el botón de borrar 
            lblNumRegistro.Text = "0"; // Reiniciar el contador de registros a cero
            btnNuevo.Enabled = true; // Habilitar el botón de nuevo 
            btnBuscar.Enabled = true; // Habilitar el botón de búsqueda
           
            DGV_LOCALIDADES.DataSource = null; // Limpiar el DataGridView de resultados
            btnBuscar.Enabled = false;
            btnEditar.Enabled = true;
            lbl_titulo.Text = ""; // Establecer el título del formulario
            txtLocN.Text = ""; // Limpiar el campo 
            btnConsulta_bus.BackColor = Color.FromArgb(55, 61, 69);
            btnConsulta_bus.ForeColor = Color.White;

        }
        private void cargar_datagrid_localidades()
        {
            // Cargar los datos en el DataGridView
            try
            {
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   SELECT Localidad , NomLoc  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     FROM LOCALIDADES";

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
                    DGV_LOCALIDADES.DataSource = LLENAR_GRID_1;
                    con.cerrar_interno();
                    DGV_LOCALIDADES.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    DGV_LOCALIDADES.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    DGV_LOCALIDADES.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                    DGV_LOCALIDADES.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    foreach (DataGridViewColumn columna in DGV_LOCALIDADES.Columns)
                    {
                        columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    // Configuración de selección
                    DGV_LOCALIDADES.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // Deshabilitar edición
                    DGV_LOCALIDADES.ReadOnly = true;
                    // Estilos visuales
                    DGV_LOCALIDADES.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    DGV_LOCALIDADES.DefaultCellStyle.SelectionForeColor = Color.Black;

                    // Configurar todas las columnas para que no se puedan redimensionar
                    DGV_LOCALIDADES.AllowUserToResizeColumns = false;

                    DGV_LOCALIDADES.Columns[0].Width = 190;                         // NUMERO DE LOCALIDAD        
                    DGV_LOCALIDADES.Columns[1].Width = 638;                         // NOMBRE DE LOCALIDAD

                    DGV_LOCALIDADES.Columns[0].Name = "LOCALIDAD";                   // NUMERO DE LOCALIDAD        
                    DGV_LOCALIDADES.Columns[1].Name = "NOMBRE LOCALIDAD";                    // NOMBRE DE LOCALIDAD

                    DGV_LOCALIDADES.Columns[0].HeaderText = "NUMERO LOCALIDAD";         // NUMERO DE LOCALIDAD         
                    DGV_LOCALIDADES.Columns[1].HeaderText = "NOMBRE LOCALIDAD";      // NOMBRE DE LOCALIDAD

                    DGV_LOCALIDADES.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DGV_LOCALIDADES.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
        }

        private void btnConsulta_bus_Click(object sender, EventArgs e)
        {
            if (Cbo_LocB.Text == "")
            {
                if (txtLocB.Text == "")
                {
                    MessageBox.Show("NO SE TIENE OPCIONES DE BUSQUEDA", "ERROR", MessageBoxButtons.OK);
                    return;
                }
            }

            if (rbNoLoc.Checked == true) { if (Cbo_LocB.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELCCIONAR UNA ZONA", "ERROR", MessageBoxButtons.OK); Cbo_LocB.Focus(); return; } }
            if (rbIdentiNombre.Checked == true) { if (txtLocB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA CALLE CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); txtLocB.Focus(); return; } }
            if (rbSimilarNombre.Checked == true) { if (txtLocB.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA CALLE CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); txtLocB.Focus(); return; } }


            // SE ARMA EL query DE BUSQUEDA
            con.cadena_sql_interno = " ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT Localidad , NomLoc ";
            con.cadena_sql_interno = con.cadena_sql_interno + "    FROM LOCALIDADES";
            con.cadena_sql_interno = con.cadena_sql_interno + "   Where Municipio = " + Program.municipioN;
            //NUMERO DE LOCALIDAD
            if (rbNoLoc.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "           AND Localidad =" + Cbo_LocB.Text.Trim(); }

            //nombre de LA CALLE
            if (rbIdentiNombre.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "    AND NomLoc =" + util.scm(txtLocB.Text.Trim()); }
            if (rbSimilarNombre.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND NomLoc LIKE '%" + txtLocB.Text.Trim() + "%'"; }
            con.cadena_sql_interno = con.cadena_sql_interno + "                                     ORDER BY Localidad";
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
                DGV_LOCALIDADES.DataSource = LLENAR_GRID_1;
                con.cerrar_interno();
                DGV_LOCALIDADES.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                DGV_LOCALIDADES.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                DGV_LOCALIDADES.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                DGV_LOCALIDADES.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                foreach (DataGridViewColumn columna in DGV_LOCALIDADES.Columns)
                {
                    columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                // Configuración de selección
                DGV_LOCALIDADES.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Deshabilitar edición
                DGV_LOCALIDADES.ReadOnly = true;
                // Estilos visuales
                DGV_LOCALIDADES.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                DGV_LOCALIDADES.DefaultCellStyle.SelectionForeColor = Color.Black;

                // Configurar todas las columnas para que no se puedan redimensionar
                DGV_LOCALIDADES.AllowUserToResizeColumns = false;

                DGV_LOCALIDADES.Columns[0].Width = 190;                         // NUMERO DE LOCALIDAD        
                DGV_LOCALIDADES.Columns[1].Width = 642;                         // NOMBRE DE LOCALIDAD

                DGV_LOCALIDADES.Columns[0].Name = "LOCALIDAD";                   // NUMERO DE LOCALIDAD        
                DGV_LOCALIDADES.Columns[1].Name = "NOMBRE LOCALIDAD";                    // NOMBRE DE LOCALIDAD

                DGV_LOCALIDADES.Columns[0].HeaderText = "NUMERO LOCALIDAD";         // NUMERO DE LOCALIDAD         
                DGV_LOCALIDADES.Columns[1].HeaderText = "NOMBRE LOCALIDAD";      // NOMBRE DE LOCALIDAD

                DGV_LOCALIDADES.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGV_LOCALIDADES.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                int CONTEO;
                CONTEO = DGV_LOCALIDADES.Rows.Count - 1;
                lblNumRegistro.Text = CONTEO.ToString();
                DGV_LOCALIDADES.Enabled = true; // Habilitar la grilla de resultados
                if(MOVIMIENTO != 1)
                {
                    btnGuardar.Enabled = false;
                    btncancelar2.Enabled = false;
                }

            }
            con.cerrar_interno();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            MOVIMIENTO = 3; // Indica que se está ELIMINANDO
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo 
            MessageBox.Show("SELECCIONA LA LOCALIDAD QUE DESEA ELIMINAR DENTRO DE LA TABLA DANDO DOBLE CLIC EN LA MISMA", "ELIMINACION DE COLONIA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            lbl_titulo.Text = "PROCESO DE ELIMINACION"; // Establecer el título del formulario
            cargar_datagrid_localidades(); // Cargar los datos en el DataGridView inicial
            btnGuardar.Enabled = true;
            btncancelar2.Enabled = true;
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnBuscar.Enabled = true; // 
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar
            DGV_LOCALIDADES.Enabled = true; // Habilitar la grilla de resultados 
            DGV_LOCALIDADES.Focus(); // Enfocar el DataGridView de resultados 
        }

        private void DGV_COLONIAS_DoubleClick(object sender, EventArgs e)
        {
            if (MOVIMIENTO == 1)//ALTA DE UNA LOCALIDAD
            {
               
                    if (DGV_LOCALIDADES.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel de creación de nueva localidad
                    txtNoLocN.Enabled = false; // Deshabilitar el campo de texto para el número de localidad, ya que se generará automáticamente
                    txtLocN.Enabled = true; // Habilitar el campo de texto para ingresar el nombre de la localidad
                btnGuardar.Enabled = true;
                btncancelar2.Enabled = true;
                txtLocN.Focus();
                
            }
            else if (MOVIMIENTO == 2)//EDITANDO LOCALIDAD
            {
                
                    if (DGV_LOCALIDADES.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel 
                    txtNoLocN.Enabled = false; // Deshabilitar el campo 
                txtLocN.Enabled = true; // Habilitar el campo de texto para ingresar el nombre de la localidad
                btnGuardar.Enabled = true;
                btncancelar2.Enabled = true;
                txtLocN.Focus();

            }
            else if (MOVIMIENTO == 3)// ELIMINACION DE LOCALIDAD
            {
                
                    if (DGV_LOCALIDADES.CurrentRow.Cells[0].Value.ToString() == "")
                    {
                        MessageBox.Show("¡SELECCIONE UN DATO CORRECTO!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento
                    }
                    enviar_datos(); // Llamar al método para enviar los datos seleccionados
                    PNLNEW.Enabled = true; // Habilitar el panel
                    txtNoLocN.Enabled = false; // Deshabilitar el campo de texto 
                    txtLocN.Enabled = false;
                btnGuardar.Enabled = true;
                btncancelar2.Enabled = true;
            }
            else // Si MOVIMIENTO es 0, significa que NO SE SELECCIONÓ UN PROCESO VÁLIDO
            {
                MessageBox.Show("ERROR, DEBE DE SELECCIONAR UN PROCESO, NUEVO, EDITAR O ELIMINAR.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Sale del método o procedimiento si no se ha seleccionado un proceso válido
            }

        }
        private void enviar_datos()
        {

            if (MOVIMIENTO != 1)//ALTA DE CALLE
            {
                txtNoLocN.Text = Convert.ToString(DGV_LOCALIDADES.CurrentRow.Cells[0].Value).Trim();
            }
            txtLocN.Text = Convert.ToString(DGV_LOCALIDADES.CurrentRow.Cells[1].Value).Trim();
        }
        private void crud() // METODO PARA DAR DE ALTA, EDITAR O ELIMINAR UNA LOCALIDAD
        {
            try
            {
                con.conectar_base_interno();
                con.open_c_interno();
                SqlCommand cmd = new SqlCommand("SONG_CRUD_LOCALIDAD", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 1).Value = Program.PEstado;
                cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 1).Value = Program.municipioN;
                cmd.Parameters.Add("@LOCALIDAD", SqlDbType.Int, 10).Value = numero_localidad;
                cmd.Parameters.Add("@NomLoc", SqlDbType.VarChar, 200).Value = nombre_localidad;
                cmd.Parameters.Add("@USUARIO", SqlDbType.VarChar, 100).Value = Program.nombre_usuario;
                cmd.Parameters.Add("@MOVIMIENTO", SqlDbType.Int, 1).Value = MOVIMIENTO;
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
            numero_localidad = txtNoLocN.Text.Trim();
            nombre_localidad = txtLocN.Text.Trim();
            int verificar = 0, verificar2 = 0;
            if (numero_localidad == "")
            {
                MessageBox.Show("FAVOR DE INGRESAR LA NUMERO DE LOCALIDAD", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoLocN.Focus();
                return;
            }
            if (nombre_localidad == "")
            {
                MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DE LA LOCALIDAD", "ALERTA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLocN.Focus();
                return;
            }
            if (MOVIMIENTO == 1)//ALTA DE CALLE
            {
                DialogResult resp = MessageBox.Show("¿ESTA SEGURO DESEA CREAR LA LOCALIDAD?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    try
                    {
                        con.conectar_base_interno();
                        //se verifica si existe esa calle dada de alta
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             From LOCALIDADES ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             Where NomLoc =" + util.scm(nombre_localidad);
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
                            var existe = con.leer_interno[0].ToString();
                            verificar = Convert.ToInt32(existe);
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
                        MessageBox.Show("NO SE PUEDE DAR DE ALTA LA LOCALIDAD DADO QUE YA SE ENCUENTRA CREADA", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Sale del método o procedimiento si la calle ya existe
                    }
                    else if (verificar == 2)
                    {
                        crud(); // Llamar al método para realizar el CRUD de la calle
                        formaInicio(); // Llamar al método para reiniciar el formulario

                    }
                }
                if (resp == DialogResult.No)
                {
                    //no hacer nada
                    txtLocN.Focus();
                    return; // Sale del método o procedimiento si el usuario no confirma la edición

                }
            }
            else if (MOVIMIENTO == 2)//EDITANDO CALLE
            {
                int NUM_LOCALIDAD = 0;
                string nombre_localidad_anterior = DGV_LOCALIDADES.CurrentRow.Cells[1].Value.ToString().Trim();
                if (nombre_localidad == nombre_localidad_anterior)
                {
                    MessageBox.Show("NO SE PUEDE EDITAR LA LOCALIDAD, YA QUE NO SE HA CAMBIADO EL NOMBRE", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Sale del método o procedimiento si no se ha cambiado el nombre de la localidad
                }
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "SELECT count(LOCALIDAD)";
                    con.cadena_sql_interno = con.cadena_sql_interno + "  From MANZANAS ";
                    con.cadena_sql_interno = con.cadena_sql_interno + " Where Localidad = " + numero_localidad;
                    con.open_c_interno();
                    con.cadena_sql_cmd_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();
                    while (con.leer_interno.Read())
                    {
                        NUM_LOCALIDAD = Convert.ToInt32(con.leer_interno[0].ToString());
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
                DialogResult resp = MessageBox.Show("¿SEGURO QUE DESEA EDITAR LA LOCALIDAD, SE AFECTARAN " + NUM_LOCALIDAD + " MANZANAS ?", "Informacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                DialogResult resp = MessageBox.Show(" ¿SEGURO QUE DESEA ELIMINAR LA LOCALIDAD?", "INFORMACION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resp == DialogResult.Yes)
                {
                    int NUM_LOCALIDAD = 0;
                    try
                    {
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "SELECT count(LOCALIDAD)";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  From MANZANAS ";
                        con.cadena_sql_interno = con.cadena_sql_interno + " Where Localidad = " + numero_localidad;
                        con.open_c_interno();
                        con.cadena_sql_cmd_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();
                        while (con.leer_interno.Read())
                        {
                            NUM_LOCALIDAD = Convert.ToInt32(con.leer_interno[0].ToString());
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
                    if (NUM_LOCALIDAD != 0)
                    {
                        MessageBox.Show("NO SE PUEDE ELIMINAR LA LOCALIDAD DADO QUE SE ENCUENTRA UTILIZADO EN " + NUM_LOCALIDAD + " MANZANAS", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            PNLFBUSCAR.Enabled = true; // Habilitar el panel de búsqueda de LOCALIDADES
            DGV_LOCALIDADES.DataSource = null;
            DGV_LOCALIDADES.Enabled = false; // Deshabilitar el DataGridView de resultados
            btnBuscar.Enabled = false; // Deshabilitar el botón de búsqueda para evitar múltiples clics
            btnBorrar.Enabled = false; // Deshabilitar el botón de borrar
            btnEditar.Enabled = false; // Deshabilitar el botón de edición para evitar múltiples clics
            btnNuevo.Enabled = false; // Deshabilitar el botón de nuevo LOCALIDADES
            rbIdentiNombre.Checked = false; // Desmarcar el radio button de cuenta clave
            txtLocB.Enabled = false; // Deshabilitar el campo de búsqueda de LOCALIDADES
            btnConsulta_bus.BackColor = Color.Yellow;
            btnConsulta_bus.ForeColor = Color.Black;
        }
        private void cmd_cancelar2_Click(object sender, EventArgs e)
        {
            txtLocN.Enabled = true;
            txtLocN.Text = "";
            //txtLocN.Enabled = false;
            txtNoLocN.Enabled = false; // Habilitar el campo de texto para el número de colonia
           
            if (MOVIMIENTO != 1)
            {
                txtNoLocN.Text = "";
                txtLocN.Enabled = false;
            }
            txtLocN.Focus();
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
            txtLocN.Enabled = true;
            txtLocN.Text = "";
            //txtLocN.Enabled = false;
            txtNoLocN.Enabled = false; // Habilitar el campo de texto para el número de colonia

            if (MOVIMIENTO != 1)
            {
                txtNoLocN.Text = "";
                txtLocN.Enabled = false;
            }
            txtLocN.Focus();

            Cbo_LocB.Items.Clear();
            Cbo_LocB.SelectedIndex = -1; // Desmarcar cualquier selección previa
            Cbo_LocB.Enabled = false; // Deshabilitar el combo box de área emisora
            rbNoLoc.Checked = false; // Desmarcar el radio button de área emisora
            rbIdentiNombre.Checked = false; // Desmarcar el radio button de cuenta clave
            rbSimilarNombre.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            txtLocB.Text = ""; // Limpiar el campo de búsqueda de COLONIAs
            txtLocB.Enabled = false; // Deshabilitar el campo de búsqueda 
            DGV_LOCALIDADES.DataSource = null; // Limpiar la fuente de datos del DataGridView
            lblNumRegistro.Text = "0";
            DGV_LOCALIDADES.Enabled = false; // Deshabilitar el DataGridView de resultados
        }

        private void rbnNoCol_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoLoc.Checked == true)
            {
                Cbo_LocB.Items.Clear();
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   Select LOCALIDAD ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM LOCALIDADES";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY LOCALIDAD";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        Cbo_LocB.Items.Add(con.leer_interno[0].ToString().Trim());                // 
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
                Cbo_LocB.Enabled = true;

                Cbo_LocB.Focus();
            }
        }

        private void rbIdentiNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdentiNombre.Checked == true)
            {
                txtLocB.Text = "";
                txtLocB.Enabled = true;
                txtLocB.Focus();
            }
        }

        private void rbSimilarNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSimilarNombre.Checked == true)
            {
                txtLocB.Text = "";
                txtLocB.Enabled = true;
                txtLocB.Focus();
            }
        }

        private void rbCP_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void cajasColor()
        {
            txtLocB.Enter += util.TextBox_Enter;

            txtLocN.Enter += util.TextBox_Enter;

            Cbo_LocB.Enter += util.Cbo_Box_Enter;

            txtNoLocN.Enter += util.TextBox_Enter;


        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBorrar7_Click(object sender, EventArgs e)
        {
            rbNoLoc.Checked = false; // Desmarcar el radio button de área emisora
            Cbo_LocB.SelectedIndex = -1; // Desmarcar cualquier selección previa


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

        private void cmdLimpiaCiudadano_Click(object sender, EventArgs e)
        {
            rbIdentiNombre.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            rbSimilarNombre.Checked = false; // Desmarcar el radio button de identificación de COLONIA
            txtLocB.Text = ""; // Limpiar el campo de búsqueda de COLONIAs
            txtLocB.Enabled = false; // Deshabilitar el campo de búsqueda de COLONIAs
        }









    }
}
