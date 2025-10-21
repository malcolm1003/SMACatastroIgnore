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
    public partial class frmAreasHomogeneas : Form
    {

        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        Util util = new Util();

        ////////////////////////////////////////////////////////////
        ///////////////// -------PARA ARRASTRAR EL PANEL 
        ////////////////////////////////////////////////////////////
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        public frmAreasHomogeneas()
        {
            InitializeComponent();
        }
        private void formaInicio()
        {

            cboAñoV.Items.Clear();
            cboAñoV.SelectedIndex = -1; // Desmarcar cualquier selección previa
            rbnAñoV.Checked = false; // Desmarcar el radio button

            cboNumero.SelectedIndex = -1;
            cboNumero.Enabled = false;
            txtNombre.Text = ""; // Limpiar el campo de búsqueda
            txtNombre.Enabled = false; // Deshabilitar el campo de búsqueda de
            rbIdentiNombre.Checked = false; // Desmarcar el radio button 
            rbSimilarNombre.Checked = false; // Desmarcar el radio button
            rbnNumero.Checked = false;

            PNLFBUSCAR.Enabled = true; // Deshabilitar el panel de búsqueda
            cboAñoV.Enabled = false; // Deshabilitar el combo box

            lblNumRegistro.Text = "0"; // Reiniciar el contador de registros a cero

            dgvAreasH.Enabled = true; // Habilitar el DataGridView de resultados
            cargar_datagrid_Areas(); // Cargar los datos en el DataGridView inicial

        }
        private void cargar_datagrid_Areas()
        {
            // Cargar los datos en el DataGridView
            try
            {
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "                SELECT  AnioVigVUS , AreaHom , DescAreaHo ";
                con.cadena_sql_interno = con.cadena_sql_interno + " , Uso , Clasif , ValM2Suelo , FrenteBase , FondoBase  FROM AREASH";
                con.cadena_sql_interno = con.cadena_sql_interno + "                     ORDER BY  AnioVigVUS DESC, AreaHom ASC";

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
                    dgvAreasH.DataSource = LLENAR_GRID_1;
                    con.cerrar_interno();
                    dgvAreasH.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    dgvAreasH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    dgvAreasH.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                    dgvAreasH.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    foreach (DataGridViewColumn columna in dgvAreasH.Columns)
                    {
                        columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    // Configuración de selección
                    dgvAreasH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // Deshabilitar edición
                    dgvAreasH.ReadOnly = true;
                    // Estilos visuales
                    dgvAreasH.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    dgvAreasH.DefaultCellStyle.SelectionForeColor = Color.Black;

                    // Configurar todas las columnas para que no se puedan redimensionar
                    dgvAreasH.AllowUserToResizeColumns = false;

                    dgvAreasH.Columns[0].Width = 75;                         // AÑO DE VIGENCIA        
                    dgvAreasH.Columns[1].Width = 55;                         // AREA HOMOGENEA
                    dgvAreasH.Columns[2].Width = 300;                         // DESCRIPCION
                    dgvAreasH.Columns[3].Width = 51;                         //USO
                    dgvAreasH.Columns[4].Width = 80;                         //CLASIFICACION
                    dgvAreasH.Columns[5].Width = 110;                         //VALOR DEL METRO CUADRADO
                    dgvAreasH.Columns[6].Width = 78;                         //FRENTE
                    dgvAreasH.Columns[7].Width = 78;                         //FONDO

                    dgvAreasH.Columns[0].Name = "VIGENCIA";                   // AÑO DE VIGENCIA        
                    dgvAreasH.Columns[1].Name = "AREA";                       // AREA HOMOGENEA
                    dgvAreasH.Columns[2].Name = "DESCRIPCION";                // DESCRIPCION
                    dgvAreasH.Columns[3].Name = "USO";                        //USO
                    dgvAreasH.Columns[4].Name = "CLASIFICACION";              //CLASIFICACION
                    dgvAreasH.Columns[5].Name = "VALOR x M2";                 //VALOR DEL METRO CUADRADO
                    dgvAreasH.Columns[6].Name = "FRENTE";                     //FRENTE
                    dgvAreasH.Columns[7].Name = "FONDO";                      //FONDO

                    dgvAreasH.Columns[0].HeaderText = "VIGENCIA";                   // AÑO DE VIGENCIA        
                    dgvAreasH.Columns[1].HeaderText = "AREA";                       // AREA HOMOGENEA
                    dgvAreasH.Columns[2].HeaderText = "DESCRIPCION";                // DESCRIPCION
                    dgvAreasH.Columns[3].HeaderText = "USO";                        //USO
                    dgvAreasH.Columns[4].HeaderText = "CLASIFI.";              //CLASIFICACION
                    dgvAreasH.Columns[5].HeaderText = "VALOR x M2";                      //VALOR DEL METRO CUADRADO
                    dgvAreasH.Columns[6].HeaderText = "FRENTE";                     //FRENTE
                    dgvAreasH.Columns[7].HeaderText = "FONDO";                      //FONDO

                    dgvAreasH.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                }

                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                return;
            }
        }

        private void btnConsulta_bus_Click(object sender, EventArgs e)
        {
            if (cboAñoV.Text == "")
            {
                if (cboNumero.Text == "")
                {
                    if (txtNombre.Text == "")
                    {
                        MessageBox.Show("NO SE TIENE OPCIONES DE BUSQUEDA", "ERROR", MessageBoxButtons.OK);
                        return;
                    }
                }
            }


            if (rbnAñoV.Checked == true) { if (cboAñoV.Text.Trim() == "") { MessageBox.Show("FAVOR DE SELCCIONAR UNA ZONA", "ERROR", MessageBoxButtons.OK); cboAñoV.Focus(); return; } }
            if (rbnNumero.Checked == true) { if (cboNumero.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NUMERO DEL AREA HOMOGENEA CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); cboNumero.Focus(); return; } }
            if (rbIdentiNombre.Checked == true) { if (txtNombre.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DEL AREA HOMOGENEA CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); txtNombre.Focus(); return; } }
            if (rbSimilarNombre.Checked == true) { if (txtNombre.Text.Trim() == "") { MessageBox.Show("FAVOR DE INGRESAR EL NOMBRE DEL AREA HOMOGENEA CORRECTAMENTE", "ERROR", MessageBoxButtons.OK); txtNombre.Focus(); return; } }


            // SE ARMA EL query DE BUSQUEDA
            try
            {
                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT AnioVigVUS , AreaHom , DescAreaHo, ";
                con.cadena_sql_interno = con.cadena_sql_interno + "         Uso , Clasif , ValM2Suelo , FrenteBase , FondoBase";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM AREASH ";
                con.cadena_sql_interno = con.cadena_sql_interno + "   Where Municipio = " + Program.municipioN;
                //AÑO DE VIGENCIA
                if (rbnAñoV.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "           AND AnioVigVUS =" + cboAñoV.Text.Trim(); }
                //numero del area homogenea
                if (rbnNumero.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "         AND AreaHom =" + cboNumero.Text.Trim(); }
                //nombre del area homogenea
                if (rbIdentiNombre.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "    AND DescAreaHo =" + txtNombre.Text.Trim(); }
                if (rbSimilarNombre.Checked == true) { con.cadena_sql_interno = con.cadena_sql_interno + "   AND DescAreaHo LIKE '%" + txtNombre.Text.Trim() + "%'"; }
                con.cadena_sql_interno = con.cadena_sql_interno + "                                     ORDER BY AnioVigVUS DESC, AreaHom ASC";
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
                    dgvAreasH.DataSource = LLENAR_GRID_1;
                    con.cerrar_interno();
                    dgvAreasH.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas 
                    dgvAreasH.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    dgvAreasH.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
                    dgvAreasH.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                    foreach (DataGridViewColumn columna in dgvAreasH.Columns)
                    {
                        columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    // Configuración de selección
                    dgvAreasH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    // Deshabilitar edición
                    dgvAreasH.ReadOnly = true;
                    // Estilos visuales
                    dgvAreasH.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    dgvAreasH.DefaultCellStyle.SelectionForeColor = Color.Black;

                    // Configurar todas las columnas para que no se puedan redimensionar
                    dgvAreasH.AllowUserToResizeColumns = false;

                    dgvAreasH.Columns[0].Width = 75;                         // AÑO DE VIGENCIA        
                    dgvAreasH.Columns[1].Width = 55;                         // AREA HOMOGENEA
                    dgvAreasH.Columns[2].Width = 300;                         // DESCRIPCION
                    dgvAreasH.Columns[3].Width = 51;                         //USO
                    dgvAreasH.Columns[4].Width = 80;                         //CLASIFICACION
                    dgvAreasH.Columns[5].Width = 110;                         //VALOR DEL METRO CUADRADO
                    dgvAreasH.Columns[6].Width = 78;                         //FRENTE
                    dgvAreasH.Columns[7].Width = 78;                         //FONDO

                    dgvAreasH.Columns[0].Name = "VIGENCIA";                   // AÑO DE VIGENCIA        
                    dgvAreasH.Columns[1].Name = "AREA";                       // AREA HOMOGENEA
                    dgvAreasH.Columns[2].Name = "DESCRIPCION";                // DESCRIPCION
                    dgvAreasH.Columns[3].Name = "USO";                        //USO
                    dgvAreasH.Columns[4].Name = "CLASIFICACION";              //CLASIFICACION
                    dgvAreasH.Columns[5].Name = "VALOR x M2";                 //VALOR DEL METRO CUADRADO
                    dgvAreasH.Columns[6].Name = "FRENTE";                     //FRENTE
                    dgvAreasH.Columns[7].Name = "FONDO";                      //FONDO

                    dgvAreasH.Columns[0].HeaderText = "VIGENCIA";                   // AÑO DE VIGENCIA        
                    dgvAreasH.Columns[1].HeaderText = "AREA";                       // AREA HOMOGENEA
                    dgvAreasH.Columns[2].HeaderText = "DESCRIPCION";                // DESCRIPCION
                    dgvAreasH.Columns[3].HeaderText = "USO";                        //USO
                    dgvAreasH.Columns[4].HeaderText = "CLASIFI.";              //CLASIFICACION
                    dgvAreasH.Columns[5].HeaderText = "VALOR x M2";                      //VALOR DEL METRO CUADRADO
                    dgvAreasH.Columns[6].HeaderText = "FRENTE";                     //FRENTE
                    dgvAreasH.Columns[7].HeaderText = "FONDO";                      //FONDO

                    dgvAreasH.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvAreasH.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    int CONTEO;
                    CONTEO = dgvAreasH.Rows.Count - 1;
                    lblNumRegistro.Text = CONTEO.ToString();
                    dgvAreasH.Enabled = true; // Habilitar la grilla de resultados

                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al executar el proceso N19_CALCULO_CATASTRO" + ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }
           
        }

        private void frmColonias_Load(object sender, EventArgs e)
        {
            formaInicio();
            cajasColor();
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            formaInicio();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            PNLFBUSCAR.Enabled = true; // Habilitar el panel de búsqueda
            dgvAreasH.DataSource = null;
            dgvAreasH.Enabled = false; // Deshabilitar el DataGridView de resultados
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
        private void btnConsulta_bus_MouseHover(object sender, EventArgs e)
        {

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnConsulta_bus, "CONSULTAR BUSQUEDA");
        }

        private void rbnNoCol_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnAñoV.Checked == true)
            {
                cboAñoV.Items.Clear();
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   Select DISTINCT AnioVigVUS  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM AREASH";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY AnioVigVUS DESC";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        cboAñoV.Items.Add(con.leer_interno[0].ToString().Trim());                // 
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
                cboAñoV.Enabled = true;

                cboAñoV.Focus();
            }
        }

        private void rbIdentiNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIdentiNombre.Checked == true)
            {
                txtNombre.Text = "";
                txtNombre.Enabled = true;
                txtNombre.Focus();
            }
        }

        private void rbSimilarNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSimilarNombre.Checked == true)
            {
                txtNombre.Text = "";
                txtNombre.Enabled = true;
                txtNombre.Focus();
            }
        }

        private void cajasColor()
        {
            txtNombre.Enter += util.TextBox_Enter;
            cboAñoV.Enter += util.Cbo_Box_Enter;
            cboNumero.Enter += util.Cbo_Box_Enter;

        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdBorrar7_Click(object sender, EventArgs e)
        {
            rbnAñoV.Checked = false; // Desmarcar el radio button
            cboAñoV.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboAñoV.Enabled = false;
        }

        private void rbnNumero_CheckedChanged(object sender, EventArgs e)
        {
            if (rbnNumero.Checked == true)
            {
                cboNumero.Items.Clear();
                try
                {
                    con.conectar_base_interno();
                    con.cadena_sql_interno = "";
                    con.cadena_sql_interno = con.cadena_sql_interno + "   Select DISTINCT AreaHom  ";
                    con.cadena_sql_interno = con.cadena_sql_interno + "     FROM AREASH";
                    con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY AreaHom";

                    con.cadena_sql_cmd_interno();
                    con.open_c_interno();
                    con.leer_interno = con.cmd_interno.ExecuteReader();

                    while (con.leer_interno.Read())
                    {
                        cboNumero.Items.Add(con.leer_interno[0].ToString().Trim());                // 
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
                cboNumero.Enabled = true;

                cboNumero.Focus();
            }
        }

        private void btnborrarNumero_Click(object sender, EventArgs e)
        {
            rbnNumero.Checked = false; // Desmarcar el radio button
            cboNumero.SelectedIndex = -1; // Desmarcar cualquier selección previa
            cboNumero.Enabled = false;
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
            rbIdentiNombre.Checked = false; // Desmarcar el radio button 
            rbSimilarNombre.Checked = false; // Desmarcar el radio button 
            txtNombre.Text = ""; // Limpiar el campo de búsqueda 
            txtNombre.Enabled = false; // Deshabilitar el campo de búsqueda 
        }









    }
}
