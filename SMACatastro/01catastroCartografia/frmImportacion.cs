using AccesoBase;
using Microsoft.Office.Interop.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Utilerias;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;

namespace SMACatastro.catastroCartografia
{
    public partial class frmImportacion : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();      //conexion 
        Util util = new Util();

        //METODO PARA ARRASTRAR EL FORMULARIO-----------------------------------------------------------------------------------------------
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //METODOS PARA CERRAR,MAXIMIZAR, MINIMIZAR FORMULARIO-------------------------------------------------------------------------------
        int lx, ly;
        int sw, sh;
        double SupTerrTotC = 0;
        double SupTerrComC = 0;
        double SupConsComC = 0;
        int error = 0, error2 = 0;
        string construccion;
        int construccion_si_no = 0;
        public frmImportacion()
        {
            InitializeComponent();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {


            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
                ofd.Title = "Seleccionar archivo Excel";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Configurar ProgressBar
                        progressBar1.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = 100;
                        progressBar1.Value = 0;
                        progressBar1.Style = ProgressBarStyle.Continuous;

                        // Opcional: agregar un label para mostrar el progreso
                        lblProgress.Visible = true;
                        lblProgress.Text = "Iniciando importación...";

                        dataGridView1.DataSource = null;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Columns.Clear();

                        error = 0;
                        Cursor = Cursors.WaitCursor;

                        // Actualizar progreso
                        progressBar1.Value = 10;
                        lblProgress.Text = "Abriendo archivo Excel...";
                        // Application.DoEvents();

                        // Opción 1: Importar hasta último número válido
                        ImportarExcelHastaUltimoNumero(ofd.FileName);//PROCESO DE IMPORTACION

                        if (error == 1) //SI ERROR ES  SIGNIFICA QUE NO ES EL ARCHIVO CORRECTO, COMPARACION DE ENCABEZADOS
                        {
                            lblProgress.Text = "ARCHIVO DE EXCEL ERRONEO";
                            Inicio();
                            return;
                        }
                        progressBar1.Value = 80;
                        lblProgress.Text = "Aplicando formato...";
                        // Application.DoEvents();
                        // Opción 2: Importar con validación de secuencia
                        // DataTable dt = ImportarExcelSoloNumerosConsecutivos(ofd.FileName);
                        // if (dt != null) dataGridView1.DataSource = dt;

                        // Personalizar encabezados
                        PersonalizarEncabezadosDataGridView();

                        progressBar1.Value = 100;
                        lblProgress.Text = "Importación completada";
                        //Application.DoEvents();

                        MessageBox.Show($"IMPORTACION COMPLETA, {dataGridView1.Rows.Count - 1} REGISTROS IMPORTADOS", "IMPORTACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnImportar.Enabled = false;
                        btnValidar.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                    }
                    finally
                    {
                        progressBar1.Visible = false;
                        lblProgress.Visible = false;
                        Cursor = Cursors.Default;
                    }
                }
            }

        }

        private void PersonalizarEncabezadosDataGridView()//SE PERSONALIZAN LOS ENCABEZADOS DEL DATAGRIDVIEW
        {
            if (dataGridView1 == null) return;

            // PERSONALIZAR ENCABEZADOS DE COLUMNAS
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(159, 24, 151);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;


            // Configuración de selección
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // dgResultado.MultiSelect = false; // Solo permitir selección de una fila a la vez

            // Deshabilitar edición
            dataGridView1.ReadOnly = true;
            // Estilos visuales
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            //Para los encabezados del datagridview
            dataGridView1.Columns[0].HeaderText = "No";                      //          
            dataGridView1.Columns[1].HeaderText = "MUN";                      // 
            dataGridView1.Columns[2].HeaderText = "ZONA";       // 
            dataGridView1.Columns[3].HeaderText = "MZA";                        // 
            dataGridView1.Columns[4].HeaderText = "LOTE";               //
            dataGridView1.Columns[5].HeaderText = "EDIF.";                  //
            dataGridView1.Columns[6].HeaderText = "DEPTO";             //
            dataGridView1.Columns[7].HeaderText = "MZA";                      //
            dataGridView1.Columns[8].HeaderText = "LT.";                    //
            dataGridView1.Columns[9].HeaderText = "EDIF";                    //
            dataGridView1.Columns[10].HeaderText = "VIV.";
            dataGridView1.Columns[11].HeaderText = "No. INT";          //
            dataGridView1.Columns[12].HeaderText = "No. EXT.";                   //
            dataGridView1.Columns[13].HeaderText = "CALLE";                   //
            dataGridView1.Columns[14].HeaderText = "NOMBRE";                 //
            dataGridView1.Columns[15].HeaderText = "S.T.P.";                 //
            dataGridView1.Columns[16].HeaderText = "S.T.C.";                 //
            dataGridView1.Columns[17].HeaderText = "S.C.P.";                 //
            dataGridView1.Columns[18].HeaderText = "INDIVISO %";               //
            dataGridView1.Columns[19].HeaderText = "%";                      //
            dataGridView1.Columns[20].HeaderText = "S.T.P.";              //
            dataGridView1.Columns[21].HeaderText = "S.T.C.";              //
            dataGridView1.Columns[22].HeaderText = "S.C.P.";              //
            dataGridView1.Columns[23].HeaderText = "S.C.C.";              //
            dataGridView1.Columns[24].HeaderText = "DIRECCION";          //
            dataGridView1.Columns[25].HeaderText = "AÑO CORRIENTE";       //
            dataGridView1.Columns[26].HeaderText = "USO CONST";                 //
            dataGridView1.Columns[27].HeaderText = "CLASE CONST";               //
            dataGridView1.Columns[28].HeaderText = "CATEGORIA CONST";           //
            dataGridView1.Columns[29].HeaderText = "AÑO CONST";                 //
            dataGridView1.Columns[30].HeaderText = "ESTADO CONST";              //
            dataGridView1.Columns[31].HeaderText = "NIVEL CONST";               //

            // Ajustar el tamaño de las columnas al contenido

            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 40;
            dataGridView1.Columns[2].Width = 40;
            dataGridView1.Columns[3].Width = 40;
            dataGridView1.Columns[4].Width = 40;
            dataGridView1.Columns[5].Width = 40;
            dataGridView1.Columns[6].Width = 40;
            dataGridView1.Columns[7].Width = 40;
            dataGridView1.Columns[8].Width = 40;
            dataGridView1.Columns[9].Width = 40;
            dataGridView1.Columns[10].Width = 40;
            dataGridView1.Columns[11].Width = 40;
            dataGridView1.Columns[12].Width = 40;
            dataGridView1.Columns[13].Width = 200;
            dataGridView1.Columns[14].Width = 250;
            dataGridView1.Columns[15].Width = 70;
            dataGridView1.Columns[16].Width = 70;
            dataGridView1.Columns[17].Width = 70;
            dataGridView1.Columns[18].Width = 70;
            dataGridView1.Columns[19].Width = 50;
            dataGridView1.Columns[20].Width = 70;
            dataGridView1.Columns[21].Width = 70;
            dataGridView1.Columns[22].Width = 70;
            dataGridView1.Columns[23].Width = 70;
            dataGridView1.Columns[24].Width = 250;
            dataGridView1.Columns[25].Width = 80;
            dataGridView1.Columns[26].Width = 60;
            dataGridView1.Columns[27].Width = 60;
            dataGridView1.Columns[28].Width = 60;
            dataGridView1.Columns[29].Width = 60;
            dataGridView1.Columns[30].Width = 60;
            dataGridView1.Columns[31].Width = 60;


            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                columna.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }


            // Borde de encabezados de columnas
            //dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            //dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            //dataGridView1.ColumnHeadersHeight = 35; // Altura personalizada

            //// PERSONALIZAR ENCABEZADOS DE FILAS
            //dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            //dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.DarkBlue;
            //dataGridView1.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //dataGridView1.RowHeadersWidth = 50; // Ancho personalizado

            //// ESTILO PARA CELDAS NORMALES
            //dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            //dataGridView1.DefaultCellStyle.BackColor = Color.White;
            //dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            // FILAS ALTERNADAS
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
        }
        private void LiberarRecursosInterop(Application excelApp, Workbook workbook, Worksheet worksheet)
        {
            try
            {
                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }

                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                if (worksheet != null)
                    Marshal.ReleaseComObject(worksheet);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error liberando recursos: {ex.Message}");
            }
        }


        private void ImportarExcelHastaUltimoNumero(string filePath)//METODO PARA IMPORTAR EL EXCEL HASTA EL ULTIMO NUMERO VALIDO
        {
            Application excelApp = null;
            Workbook workbook = null;
            Worksheet worksheet = null;
            // Encabezados esperados en la fila 4
            string[] encabezado = { "N°", "MPIO.", "ZONA", "MZN.", "LOTE", "EDIF.", "DEPTO.", "MZ.", "LT.", "EDIFICIO", "VIV.", "N° INT", "N° EXT.", "CALLE", "NOMBRE", "S.T.P", "S.T.C", "S.C.C" };


            try
            {
                excelApp = new Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                // Actualizar progreso
                progressBar1.Value = 20;
                lblProgress.Text = "Cargando workbook...";
                //Application.DoEvents();

                workbook = excelApp.Workbooks.Open(filePath);
                worksheet = workbook.Sheets[1];

                Range usedRange = worksheet.UsedRange;
                int totalRows = usedRange.Rows.Count;
                int totalCols = usedRange.Columns.Count;

                if (totalRows < 5)
                {
                    MessageBox.Show("El archivo no tiene suficientes filas", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Actualizar progreso
                progressBar1.Value = 30;
                lblProgress.Text = "Leyendo encabezados...";
                //Application.DoEvents();


                // 1. CREAR DATATABLE CON ENCABEZADOS DE FILA 4
                DataTable dt = new DataTable();

                for (int col = 1; col <= totalCols; col++) //RECORRE LOS ENCABEZADOS DE LAS COLUMNAS
                {
                    Range headerCell = usedRange.Cells[4, col];
                    string header = headerCell.Value2?.ToString()?.Trim();
                    if (col <= 18)
                    {
                        if (header != encabezado[col - 1]) //COMPARA LOS ENCABEZADOS
                        {
                            MessageBox.Show("EL ARCHIVO DE EXCEL NO CUENTA CON EL FORMATO CORRECTO", "ERROR DE COMPATIBILIDAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            error = 1;
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(header))
                        header = $"Columna {col}";

                    string uniqueHeader = header;
                    int counter = 1;
                    while (dt.Columns.Contains(uniqueHeader))
                    {
                        uniqueHeader = $"{header}_{counter}";
                        counter++;
                    }

                    dt.Columns.Add(uniqueHeader, typeof(string));
                }

                // Actualizar progreso
                progressBar1.Value = 40;
                lblProgress.Text = "Buscando datos...";
                // Application.DoEvents();

                // 2. ENCONTRAR EL ÚLTIMO NÚMERO VÁLIDO EN LA PRIMERA COLUMNA
                int ultimaFilaConNumero = 0;

                for (int row = 5; row <= totalRows; row++)
                {
                    Range celdaNumero = usedRange.Cells[row, 1]; // Primera columna (N°)
                    string valor = celdaNumero.Value2?.ToString()?.Trim();

                    // Verificar si es un número válido
                    if (int.TryParse(valor, out int numero))
                    {
                        ultimaFilaConNumero = row;
                    }
                    else if (!string.IsNullOrEmpty(valor))
                    {
                        // Si hay texto pero no es número, terminar
                        break;
                    }
                    else
                    {
                        // Si está vacío, terminar
                        break;
                    }
                }

                // 3. LEER SOLO HASTA EL ÚLTIMO NÚMERO VÁLIDO
                int filasImportadas = 0;

                if (ultimaFilaConNumero >= 5)
                {
                    int totalFilas = ultimaFilaConNumero - 4;
                    for (int row = 5; row <= ultimaFilaConNumero; row++)
                    {
                        DataRow dr = dt.NewRow();
                        bool filaTieneDatos = false;

                        for (int col = 1; col <= totalCols; col++)
                        {
                            Range cell = usedRange.Cells[row, col];
                            string valor = cell.Value2?.ToString()?.Trim();

                            if (!string.IsNullOrEmpty(valor))
                            {
                                filaTieneDatos = true;
                            }

                            dr[col - 1] = valor ?? string.Empty;
                        }

                        if (filaTieneDatos)
                        {
                            dt.Rows.Add(dr);
                            filasImportadas++;
                        }
                        // Actualizar progreso durante la importación
                        int progreso = 40 + (int)((row - 4) * 40.0 / totalFilas);
                        progressBar1.Value = progreso;
                        lblProgress.Text = $"Importando fila {row - 4} de {totalFilas}...";
                        //Application.DoEvents();
                    }
                }

                // 4. ASIGNAR AL DATAGRIDVIEW
                dataGridView1.DataSource = dt;
                //MessageBox.Show($"Datos importados: {filasImportadas} filas (hasta número {ultimaFilaConNumero - 4})");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                LiberarRecursosInterop(excelApp, workbook, worksheet);
            }
        }
        private decimal SumarColumnaPorIndice(int indiceColumna) //METODO PARA SUMAR UNA COLUMNA DEL DATAGRIDVIEW
        {
            decimal suma = 0;
            int tabla = dataGridView1.Rows.Count;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Index < tabla - 1)
                {

                    // Saltar filas vacías o nuevas
                    if (!row.IsNewRow && row.Cells[indiceColumna].Value != null)
                    {
                        if (decimal.TryParse(row.Cells[indiceColumna].Value.ToString(), out decimal valor))
                        {
                            suma += valor;
                        }
                        else
                        {
                            MessageBox.Show("EL VALOR NO ES UN NUMERO DE LA COLUMNA " + indiceColumna + " Y CELDA " + (row.Index + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            error2 = 1;
                            return suma;
                        }
                    }
                    else
                    {
                        MessageBox.Show("EL VALOR NO PUEDE SER VACIO DE LA COLUMNA " + indiceColumna + " Y CELDA " + (row.Index + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        error2 = 1;
                        return suma;
                    }
                }
            }
            return suma;

        }

        private void ValidarColumnaPorIndice(int indiceColumna) //METODO PARA SUMAR UNA COLUMNA DEL DATAGRIDVIEW
        {

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Saltar filas vacías o nuevas
                if (!row.IsNewRow && row.Cells[indiceColumna].Value == null)
                {
                    MessageBox.Show("EL VALOR NO PUEDE SER VACIO DE LA COLUMNA " + indiceColumna + " Y CELDA " + (row.Index + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    error2 = 1;
                    return;
                }
            }

        }
        private void calculartotales(int municipio, int zona, int manzana, int lote) //METODO PARA CALCULAR LOS TOTALES DE LA CLAVE MADRE
        {

            try
            {   ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT SupTerrTot, SupTerrCom, SupConsCom  ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM PREDIOS";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE  Municipio = " + municipio;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND Zona =  " + zona;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND Manzana = " + manzana;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND Lote = " + lote;


                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();


                // Verificar si el resultado está vacío
                if (!con.leer_interno.HasRows)
                {
                    MessageBox.Show("NO EXISTE CLAVE MADRE PARA SEGUIR CON LA IMPORTACION", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return; // Retornar si no hay resultados
                }


                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        SupTerrTotC = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
                        SupTerrComC = Convert.ToDouble(con.leer_interno[1].ToString().Trim());
                        SupConsComC = Convert.ToDouble(con.leer_interno[2].ToString().Trim());
                    }
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

        private void btnValidar_Click(object sender, EventArgs e)
        {
            construccion_si_no = 0;
            construccion = "";
            double totalIndiviso = (double)SumarColumnaPorIndice(18); // indiviso
            if (error2 == 1)
            {
                return;
            }
            double totalSTPropio = (double)SumarColumnaPorIndice(20); // superficie total propio
            if (error2 == 1)
            {
                return;
            }
            double totalSTComun = (double)SumarColumnaPorIndice(21); // superficie total comun
            if (error2 == 1)
            {
                return;
            }
            double totalSCComun = (double)SumarColumnaPorIndice(23); // construccion total comun
            if (error2 == 1)
            {
                return;
            }
            double sumamun = (double)SumarColumnaPorIndice(1);
            if (error2 == 1)
            {
                return;
            }
            double sumamza = (double)SumarColumnaPorIndice(3);
            if (error2 == 1)
            {
                return;
            }
            double sumzona = (double)SumarColumnaPorIndice(2);
            if (error2 == 1)
            {
                return;
            }
            double sumlote = (double)SumarColumnaPorIndice(4);
            if (error2 == 1)
            {
                return;
            }
            ValidarColumnaPorIndice(5);
            if (error2 == 1)
            {
                return;
            }
            ValidarColumnaPorIndice(6);
            if (error2 == 1)
            {
                return;
            }
            double suma_superficie_terreno = 0;
            double suma_superficie_terreno_comun = 0;
            double suma_superficie_construccion = 0;
            int MUNICIPIO = 0;
            int ZONA = 0;
            int MANZANA = 0;
            int LOTE = 0;
            string EDIFICIO = "";
            string DEPTO = "";

            if (error2 == 1)
            {
                return;
            }

            MUNICIPIO = Convert.ToInt32(dataGridView1.Rows[0].Cells[1].Value);//MUNICIPIO
            ZONA = Convert.ToInt32(dataGridView1.Rows[0].Cells[2].Value);//ZONA
            MANZANA = Convert.ToInt32(dataGridView1.Rows[0].Cells[3].Value);//MANZANA
            LOTE = Convert.ToInt32(dataGridView1.Rows[0].Cells[4].Value);//LOTE
            EDIFICIO = dataGridView1.Rows[0].Cells[5].Value.ToString();//EDIFICIO
            DEPTO = dataGridView1.Rows[0].Cells[6].Value.ToString();//DEPTO
            calculartotales(MUNICIPIO, ZONA, MANZANA, LOTE);//CALCULA LOS TOTALES DE LA CLAVE MADRE SUPERFICIES

            if (totalSTPropio != SupTerrTotC) //SE COMPARA EL TOTAL DE LA REGILLA (TOTALSTPPROPIO) Y TOTAL DE LA BASE PREDIOS (SUPTERRTOTC)
            {
                MessageBox.Show("NO SE PUEDE CONTINUAR, YA QUE NO COINCIDE EL TOTAL DE LA SUPERFICIE DE TERRENO PROPIO", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (totalSTComun != SupTerrComC)
            {
                MessageBox.Show("NO SE PUEDE CONTINUAR, YA QUE NO COINCIDE EL TOTAL DE LA SUPERFICIE DE TERRENO COMUN", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (totalSCComun != SupConsComC)
            {
                MessageBox.Show("NO SE PUEDE CONTINUAR, YA QUE NO COINCIDE EL TOTAL DE LA SUPERFICIE DE CONSTRUCCION COMUN", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }




            try
            {   ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT SUM(STerrProp), SUM(STerrCom), SUM(SConsCom) ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FROM PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE  Municipio = " + MUNICIPIO;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND Zona =  " + ZONA;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND Manzana = " + MANZANA;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND Lote = " + LOTE;
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND Edificio <> '00' ";
                con.cadena_sql_interno = con.cadena_sql_interno + "     AND Depto <> '0000' ";


                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();


                // Verificar si el resultado está vacío
                if (!con.leer_interno.HasRows)
                {
                    MessageBox.Show("NO EXISTE CLAVE MADRE PARA SEGUIR CON LA IMPORTACION", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblProgress.Visible = false;
                    progressBar1.Visible = false;
                    return; // Retornar si no hay resultados
                }


                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        suma_superficie_terreno = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
                        suma_superficie_terreno_comun = Convert.ToDouble(con.leer_interno[1].ToString().Trim());
                        suma_superficie_construccion = Convert.ToDouble(con.leer_interno[2].ToString().Trim());

                    }
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


            if ((suma_superficie_terreno + totalSTPropio) > SupTerrTotC)//SI LA SUMA DE LA SUPERFICIE DE TERRENO PROPIO ES MAYOR O IGUAL A LA CLAVE MADRE
            {
                MessageBox.Show("NO SE PUEDE CONTINUAR, YA QUE NO CUENTA CON SUPERFICIE PARA LA IMPORTACION", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblProgress.Visible = false;
                progressBar1.Visible = false;
                return;
            }
            if ((suma_superficie_terreno_comun + totalSTComun) > SupTerrComC)//SI LA SUMA DE LA SUPERFICIE DE TERRENO COMUN ES MAYOR O IGUAL A LA CLAVE MADRE
            {
                MessageBox.Show("NO SE PUEDE CONTINUAR, YA QUE NO CUENTA CON SUPERFICIE DE TERRENO COMUN PARA LA IMPORTACION", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblProgress.Visible = false;
                progressBar1.Visible = false;
                return;
            }
            if ((suma_superficie_construccion + totalSCComun) > SupConsComC)//SI LA SUMA DE LA SUPERFICIE DE CONSTRUCCION COMUN ES MAYOR O IGUAL A LA CLAVE MADRE
            {
                MessageBox.Show("NO SE PUEDE CONTINUAR, YA QUE NO CUENTA CON SUPERFICIE DE CONSTRUCCION PARA LA IMPORTACION", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblProgress.Visible = false;
                progressBar1.Visible = false;
                return;
            }


            double SupConstrProp = 0;
            int año_consulta = 0;
            string edo_const;
            string nivel_const;
            double STerrProp;
            double STerrCom;
            double SConstrProp;
            double SConstrCom;
            double INDIVISOP;
            

            if (totalIndiviso != 100) // Verificar si el total de indiviso es 100%
            {
                MessageBox.Show("EL TOTAL DE INDIVISO DEBE DE SER 100%");
                return;
            }


            int totalFilas = dataGridView1.Rows.Count; //SUMA DEL TOTAL DE FILAS PARA LA BARRA DE ESTADO

            progressBar1.Value = 0;
            progressBar1.Maximum = totalFilas;
            lblProgress.Text = "INICIANDO PROCESO DE VERIFICACION...";
            lblProgress.Visible = true;
            progressBar1.Visible = true;

            dataGridView1.Columns.Add("VALOR TERRENO PROP", "VALOR TERRENO PROP"); //AGREGAMOS LAS COLUMNAS QUE VAMOS A UTILIZAR PARA LLENAR EL GRID
            dataGridView1.Columns.Add("VALOR TERRENO COM", "VALOR TERRENO COM");
            dataGridView1.Columns.Add("VALOR CONSTRUCCION PROP", "VALOR CONSTRUCCION PROP");
            dataGridView1.Columns.Add("VALOR CONSTRUCCION COM", "VALOR CONSTRUCCION COM");
            dataGridView1.Columns.Add("VALOR CATASTRAL", "VALOR CATASTRAL");
            dataGridView1.Columns[32].Width = 90;
            dataGridView1.Columns[33].Width = 100;
            dataGridView1.Columns[34].Width = 90;

            int i = 0;
            while (i < dataGridView1.Rows.Count)
            {
                // Saltar la fila nueva (si está en modo edición)
                if (!dataGridView1.Rows[i].IsNewRow)
                {
                    // Acceder a las celdas
                    bool MUNICIPIOBOL = EsNumeroEntero(dataGridView1.Rows[i].Cells[1].Value.ToString(), "MUNICIPIO"); //VALIDA SI ES NUMERO EL MUNICIPIO
                    if (MUNICIPIOBOL == false)
                    {
                        MessageBox.Show("EL MUNICIPIO NO ES UN NUMERO ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    bool ZONABOL = EsNumeroEntero(dataGridView1.Rows[i].Cells[2].Value.ToString(), "ZONA"); //VALIDA SI ES NUMERO LA ZONA
                    if (ZONABOL == false)
                    {
                        MessageBox.Show("LA ZONA NO ES UN NUMERO ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    bool MANZANABOL = EsNumeroEntero(dataGridView1.Rows[i].Cells[3].Value.ToString(), "MANZANA"); //VALIDA SI ES NUMERO LA MANZANA
                    if (MANZANABOL == false)
                    {
                        MessageBox.Show("LA MANZANA NO ES UN NUMERO ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    bool LOTEBOL = EsNumeroEntero(dataGridView1.Rows[i].Cells[4].Value.ToString(), "LOTE"); //VALIDA SI ES NUMERO EL LOTE
                    if (LOTEBOL == false)
                    {
                        MessageBox.Show("EL LOTE NO ES UN NUMERO ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    if (dataGridView1.Rows[i].Cells[5].Value == null) //VALIDA SI EL EDIFICIO ES NULO
                    {
                        MessageBox.Show("EL EDIFICIO NO PUEDE IR VACIO ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    if (dataGridView1.Rows[i].Cells[5].Value.ToString().Length != 2)
                    {
                        MessageBox.Show("EL EDIFICIO TIENE QUE TENER DOS CARACTERES ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    if (dataGridView1.Rows[i].Cells[6].Value == null) //VALIDA SI EL DEPARTAMENTO ES NULO
                    {
                        MessageBox.Show("EL DEPARTAMENTO NO PUEDE IR VACIO ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    if (dataGridView1.Rows[i].Cells[6].Value.ToString().Length != 4)
                    {
                        MessageBox.Show("EL DEPARTAMENTO TIENE QUE TENER CUATRO CARACTERES ", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    MUNICIPIO = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);//MUNICIPIO
                    ZONA = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);//ZONA
                    MANZANA = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);//MANZANA
                    LOTE = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);//LOTE
                    EDIFICIO = dataGridView1.Rows[i].Cells[5].Value.ToString();//EDIFICIO
                    DEPTO = dataGridView1.Rows[i].Cells[6].Value.ToString();//DEPTO



                    bool superterrpro = EsNumeroDoble(dataGridView1.Rows[i].Cells[15].Value.ToString(), "STP"); //VALIDA SI ES NUMERO LA SUPERFICIE DE TERRENO PROPIO
                    if (superterrpro == false)
                    {
                        MessageBox.Show("LA SUPERFICIE DE TERRENO PROPIO NO ES UN NUMERO EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    bool superterrcom = EsNumeroDoble(dataGridView1.Rows[i].Cells[16].Value.ToString(), "STC");//VALIDA SI ES NUMERO LA SUPERFICIE DE TERRENO COMUN
                    if (superterrcom == false)
                    {
                        MessageBox.Show("LA SUPERFICIE DE TERRENO COMUN NO ES UN NUMERO DE EN COLUMNA " + 16 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    bool superconstrcom = EsNumeroDoble(dataGridView1.Rows[i].Cells[17].Value.ToString(), "SCC");//VALIDA SI ES NUMERO LA SUPERFICIE DE CONSTRUCCION COMUN
                    if (superconstrcom == false)
                    {
                        MessageBox.Show("LA SUPERFICIE DE CONSTRUCCION COMUN NO ES UN NUMERO EN LA COLUMNA " + 17 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    bool indiviso = EsNumeroDoble(dataGridView1.Rows[i].Cells[18].Value.ToString(), "indiviso");//VALIDA SI ES NUMERO EL INDIVISO
                    if (indiviso == false)
                    {
                        MessageBox.Show("EL INDIVISO NO ES UN NUMERO DE LA CLAVE EN LA COLUMNA " + 18 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    bool superfiterrtotalPROP = EsNumeroDoble(dataGridView1.Rows[i].Cells[20].Value.ToString(), "STPCOM");//TERRENO PROPIO
                    if (superfiterrtotalPROP == false)
                    {
                        MessageBox.Show("LA SUPERFICIE DE TERRENO PROPIO NO ES NUMERO EN LA COLUMNA " + 20 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    bool superfiterrtotalcom = EsNumeroDoble(dataGridView1.Rows[i].Cells[21].Value.ToString(), "STPCOM");//COMUN
                    if (superfiterrtotalcom == false)
                    {
                        MessageBox.Show("LA SUPERFICIE DE TERRENO COMUN NO ES NUMERO EN LA COLUMNA " + 21 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    bool superfitotalconstrPRO = EsNumeroDoble(dataGridView1.Rows[i].Cells[22].Value.ToString(), "SCPPRO");//pro
                    if (superfitotalconstrPRO == false)
                    {
                        MessageBox.Show("LA SUPERFICIE DE CONSTRUCCION PROPIA NO ES NUMERO EN LA COLUMNA " + 22 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    bool superfitotalconstrcom = EsNumeroDoble(dataGridView1.Rows[i].Cells[23].Value.ToString(), "SCPCOM");//com
                    if (superfitotalconstrcom == false)
                    {
                        MessageBox.Show("LA SUPERFICIE DE CONSTRUCCION COMUN NO ES NUMERO EN LA COLUMNA " + 23 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    if (dataGridView1.Rows[i].Cells[24].Value.ToString() == "")    //VALIDA SI LA DIRECCION ESTA VACIA
                    {
                        MessageBox.Show("LA DIRECCION NO PUEDE IR VACIA EN LA COLUMNA " + 24 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    bool AÑO_CORRIENTE_2 = EsNumeroEntero(dataGridView1.Rows[i].Cells[25].Value.ToString(), "AÑO_CORRIENTE");//VALIDA SI ES NUMERO EL AÑO CORRIENTE
                    if (AÑO_CORRIENTE_2 == false)
                    {
                        MessageBox.Show("EL AÑO CORRIENTE NO ES UN NUMERO EN LA COLUMNA " + 25 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    construccion = dataGridView1.Rows[i].Cells[22].Value.ToString();
                    if (construccion == "0" || construccion == "0.00"|| construccion == "0.0")
                    {
                        construccion_si_no = 1;
                    }
                    else
                    {
                        construccion_si_no = 0;
                    }
                    if (construccion_si_no != 1)
                    {

                    
                    bool año_const = EsNumeroEntero(dataGridView1.Rows[i].Cells[29].Value.ToString(), "año_const"); //VALIDA SI ES NUMERO EL AÑO DE CONSTRUCCION
                    if (año_const == false)
                    {
                        MessageBox.Show("EL AÑO DE CONSTRUCCION NO ES UN NUMERO EN LA COLUMNA " + 29 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    bool cateEsValido = EsNumeroEntero(dataGridView1.Rows[i].Cells[30].Value.ToString(), "cate_const"); //VALIDA SI ES NUMERO EL ESTADO DE CONSTRUCCION
                    if (cateEsValido == false)
                    {
                        MessageBox.Show("EL ESTADO DE LA CONSTRUCCION DEBE SER UN VALOR NUMERICO EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }

                    bool NIVEL_CONST = EsNumeroEntero(dataGridView1.Rows[i].Cells[31].Value.ToString(), "NIVEL_CONST");//VALIDA SI ES NUMERO EL NIVEL DE CONSTRUCCION
                    if (NIVEL_CONST == false)
                    {
                        MessageBox.Show("EL NIVEL DE CONSTRUCCION NO ES UN NUMERO DE LA CLAVE EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    }

                    //if (Convert.ToInt32(dataGridView1.Rows[i].Cells[31].Value) == 0) //VALIDA SI EL NIVEL DE CONSTRUCCION ES 0
                    //{
                    //    MessageBox.Show("EL NIVEL DE LA CONSTRUCCION NO PUEDE SER 0 EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    lblProgress.Visible = false;
                    //    progressBar1.Visible = false;
                    //    return;
                    //}
                    double STP = Convert.ToDouble(dataGridView1.Rows[i].Cells[15].Value);//SUPERFICIE DE TERRENO PROPIO
                    double STC = Convert.ToDouble(dataGridView1.Rows[i].Cells[16].Value);//SUPERFICIE DE TERRENO COMUN
                    double SCC = Convert.ToDouble(dataGridView1.Rows[i].Cells[17].Value);//SUPERFICIE DE CONSTRUCCION COMUN
                    int AÑO_CORRIENTE = Convert.ToInt32(dataGridView1.Rows[i].Cells[25].Value);//AÑO CORRIENTE
                    string uso = dataGridView1.Rows[i].Cells[26].Value.ToString();//USO 
                    string clase_construccion = "";
                    int cate_construccion = 0;
                    int año_construccion = 0;
                    int estado_construccion = 0;
                    int nivel_construccion = 0;
                    if (construccion_si_no != 1)
                    {
                         año_construccion = Convert.ToInt32(dataGridView1.Rows[i].Cells[29].Value);//AÑO DE CONSTRUCCION
                         estado_construccion = Convert.ToInt32(dataGridView1.Rows[i].Cells[30].Value.ToString());//ESTADO DE CONSTRUCCION
                         nivel_construccion = Convert.ToInt32(dataGridView1.Rows[i].Cells[31].Value);//NIVEL DE CONSTRUCCION
                         clase_construccion = dataGridView1.Rows[i].Cells[27].Value.ToString();//CLASE DE CONSTRUCCION
                         cate_construccion = Convert.ToInt32(dataGridView1.Rows[i].Cells[28].Value.ToString());//CATEGORIA DE CONSTRUCCION
                    }
                    else
                    {
                         año_construccion = 0;
                        estado_construccion = 0;
                        nivel_construccion = 0;
                        clase_construccion = "-";
                        cate_construccion = 0;
                    }

                    

                    SupConstrProp = Convert.ToDouble(dataGridView1.Rows[i].Cells[22].Value);
                    edo_const = dataGridView1.Rows[i].Cells[30].Value.ToString();//ESTADO DE CONSTRUCCION
                    nivel_const = dataGridView1.Rows[i].Cells[31].Value.ToString();//NIVEL DE CONSTRUCCION
                    STerrProp = Convert.ToDouble(dataGridView1.Rows[i].Cells[20].Value);//SUPERFICIE DE TERRENO PROPIO PROPIEDADES
                    STerrCom = Convert.ToDouble(dataGridView1.Rows[i].Cells[21].Value);//SUPERFICIE DE TERRENO COMUN PROPIEDADES
                    SConstrProp = Convert.ToDouble(dataGridView1.Rows[i].Cells[22].Value);//SUPERFICIE DE CONSTRUCCION PROPIA PROPIEDADES
                    SConstrCom = Convert.ToDouble(dataGridView1.Rows[i].Cells[23].Value);//SUPERFICIE DE CONSTRUCCION COMUN PROPIEDADES
                    INDIVISOP = Convert.ToDouble(dataGridView1.Rows[i].Cells[18].Value);//INDIVISO

                    // Validaciones




                    if (construccion_si_no != 1)
                    {
                        try
                        {
                            //////////////VERIRFICAMOS SI EXISTE EL TIPO DE CONSTRUCCION
                            int verificar = 0;
                            con.conectar_base_interno();
                            con.cadena_sql_interno = "";
                            con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT DescrClCat";
                            con.cadena_sql_interno = con.cadena_sql_interno + "              FROM TIPO_CONST";
                            con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE Uso = " + util.scm(uso);
                            con.cadena_sql_interno = con.cadena_sql_interno + "               AND ClaseConst = " + util.scm(clase_construccion);
                            con.cadena_sql_interno = con.cadena_sql_interno + "               AND CategConst = " + cate_construccion;
                            con.cadena_sql_interno = con.cadena_sql_interno + "               AND AnioVigVUC = " + Program.añoActual + ")";
                            con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                            con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT CHARLY = 1";
                            con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                            con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                            con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                            con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT CHARLY = 2";
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

                            if (verificar == 2)
                            {
                                MessageBox.Show("EL TIPO DE CONSTRUCCION NO ES VALIDA, FAVOR DE REVISAR LAS COLUMNA " + "25, 26 Y 27" + " Y CELDA " + (i + 1), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                lblProgress.Visible = false;
                                progressBar1.Visible = false;
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
                    }




                    if (SupTerrTotC != STP)//VERIFICA SI LA SUPERFICIE DE TERRENO PROPIO COINCIDE CON LA CLAVE MADRE
                    {
                        MessageBox.Show("LA SUPERFICIE DE TERRENO PROPIO NO COINCIDE, FAVOR DE REVISAR EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    if (SupTerrComC != STC)//VERIFICA SI LA SUPERFICIE DE TERRENO COMUN COINCIDE CON LA CLAVE MADRE
                    {
                        MessageBox.Show("LA SUPERFICIE DE TERRENO COMUN NO COINCIDE EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    if (SupConsComC != SCC)//VERIFICA SI LA SUPERFICIE DE CONSTRUCCION COMUN COINCIDE CON LA CLAVE MADRE
                    {
                        MessageBox.Show("LA SUPERFICIE DE CONSTRUCCION COMUN NO COINCIDE EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    //if (SupConstrProp <= 0) //VERIFICA SI LA SUPERFICIE DE CONSTRUCCION PROPIA ES 0 O MENOR
                    //{
                    //    MessageBox.Show("LA SUPERFICIE DE CONSTRUCCION PROPIA NO PUEDE SER 0 O MENOR EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    lblProgress.Visible = false;
                    //    progressBar1.Visible = false;
                    //    return;
                    //}
                    if (construccion_si_no != 1)
                    {

                    
                    año_consulta = (Program.añoActual) - 4; //OBTENEMOS EL AÑO DE CONSULTA AL ACTUAL MENOS 4 AÑOS
                    if (año_construccion < año_consulta)//VERIFICA SI EL AÑO DE CONSTRUCCION ES MENOR AL AÑO DE CONSULTA
                    {
                        MessageBox.Show("EL AÑO DE LA CONSTRUCCION NO PUEDE SER MENOR A 4 AÑOS AL ACTUAL EN LA COLUMNA " + 15 + " Y CELDA " + (i + 1), "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblProgress.Visible = false;
                        progressBar1.Visible = false;
                        return;
                    }
                    }


                    try
                    {//LLENAMOS EL GRID CON EL VALOR CATASTRAL SE EJECUTA EL PROCEDIMIENTO ALMACENADO
                        double valor_terreno_propio = 0, valor_terreno_comun = 0, valor_construccion_propia = 0, valor_construccion_comun = 0, VALORCATASTRAL = 0;
                        con.conectar_base_interno();
                        con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                        con.open_c_interno();

                        SqlCommand cmd = new SqlCommand("N19_CALCULO_CAT_VIRTUAL", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
                        cmd.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                        cmd.Parameters.Add("@ESTADO_GEN", SqlDbType.Int, 8).Value = Program.PEstado;
                        cmd.Parameters.Add("@MUNICIPIO_GEN", SqlDbType.Int, 8).Value = MUNICIPIO;
                        cmd.Parameters.Add("@ZONA_GEN", SqlDbType.Int, 8).Value = ZONA;
                        cmd.Parameters.Add("@MANZANA_GEN", SqlDbType.Int, 8).Value = MANZANA;
                        cmd.Parameters.Add("@LOTE_GEN", SqlDbType.Int, 4).Value = LOTE;
                        cmd.Parameters.Add("@EDIFICIO_GEN", SqlDbType.Char, 3).Value = EDIFICIO;
                        cmd.Parameters.Add("@DEPTO_GEN", SqlDbType.Char, 4).Value = DEPTO;
                        cmd.Parameters.Add("@TERRENO_PROPIO", SqlDbType.Float, 8).Value = STerrProp;
                        cmd.Parameters.Add("@AÑO_CORRIENTE", SqlDbType.Int, 8).Value = AÑO_CORRIENTE;
                        cmd.Parameters.Add("@INDIVISO_GENERAL", SqlDbType.Float, 10).Value = INDIVISOP;
                        cmd.Parameters.Add("@TERRENO_COM_GEN", SqlDbType.Float, 10).Value = STC; //SUPERFICIE DE TERRENO COMUN PREDIOS ( GENERAL)
                        cmd.Parameters.Add("@CONSTRU_PRP_GEN", SqlDbType.Float, 10).Value = SupConstrProp;
                        cmd.Parameters.Add("@CONSTRU_COM_GEN", SqlDbType.Float, 10).Value = SCC;
                        cmd.Parameters.Add("@USO", SqlDbType.Char, 1).Value = uso;
                        cmd.Parameters.Add("@CLASECONST_GEN", SqlDbType.Char, 1).Value = clase_construccion;
                        cmd.Parameters.Add("@CATEGCONST", SqlDbType.Int, 8).Value = cate_construccion;
                        cmd.Parameters.Add("@ANIODECONS", SqlDbType.Int, 8).Value = año_construccion;
                        cmd.Parameters.Add("@ESTADOCONS", SqlDbType.Int, 8).Value = estado_construccion;
                        cmd.Parameters.Add("@NIVCONS", SqlDbType.Int, 8).Value = nivel_construccion;
                        cmd.Parameters.Add("@valor_terreno1", SqlDbType.Float, 60).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@valor_terreno_comun1", SqlDbType.Float, 60).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@valor_construccion1", SqlDbType.Float, 60).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@valor_COMUN1", SqlDbType.Float, 60).Direction = ParameterDirection.Output;
                        cmd.Connection = con.cnn_interno;
                        cmd.ExecuteNonQuery();

                        valor_terreno_propio = Convert.ToDouble(cmd.Parameters["@valor_terreno1"].Value);
                        valor_terreno_comun = Convert.ToDouble(cmd.Parameters["@valor_terreno_comun1"].Value);
                        valor_construccion_propia = Convert.ToDouble(cmd.Parameters["@valor_construccion1"].Value);
                        valor_construccion_comun = Convert.ToDouble(cmd.Parameters["@valor_COMUN1"].Value);
                        con.cerrar_interno();

                        // dataGridView1.Rows.Add(1);
                        dataGridView1.Rows[i].Cells[32].Value = valor_terreno_propio;
                        dataGridView1.Rows[i].Cells[33].Value = valor_terreno_comun;
                        dataGridView1.Rows[i].Cells[34].Value = valor_construccion_propia;
                        dataGridView1.Rows[i].Cells[35].Value = valor_construccion_comun;
                        VALORCATASTRAL = valor_terreno_propio + valor_terreno_comun + valor_construccion_propia + valor_construccion_comun;
                        dataGridView1.Rows[i].Cells[36].Value = VALORCATASTRAL;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        util.CapturarPantallaConInformacion(ex);
                        System.Threading.Thread.Sleep(500);
                        con.cerrar_interno();
                        return; // Retornar false si ocurre un error
                    }
                    int progreso = i;
                    progressBar1.Value = progreso;
                    lblProgress.Text = $"VERIFICANDO LA CLAVE CATASTRAL {MUNICIPIO} - {ZONA} - {MANZANA} - {LOTE} - {EDIFICIO} - {DEPTO}, PROCESO {i + 1} de {totalFilas - 1}...";
                }
                i++;
            }
            lblProgress.Text = "VALIDACION FINALIZADA";
            MessageBox.Show("VALIDACION CORRECTA, PUEDE SEGUIR CON EL PROCESO", "VALIDACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnGuardar.Enabled = true;
            btnValidar.Enabled = false;
            progressBar1.Visible = false;
            lblProgress.Visible = false;

        }
        public bool EsNumeroEntero(string valor, string nombreVariable = "valor")//VALIDA SI ES NUMERO ENTERO
        {
            if (int.TryParse(valor, out _))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public bool EsNumeroDoble(string valor, string nombreVariable = "valor")//VALIDA SI ES NUMERO DECIMAL
        {
            if (double.TryParse(valor, out _))
            {
                return true;
            }
            else
            {

                return false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int ESTADO = Program.PEstado;
            int MUNICIPIO = 0;
            int ZONA = 0;
            int MANZANA = 0;
            int LOTE = 0;
            string EDIFICIO = "";
            string DEPTO = "";
            double SupTerrTotC = 0;
            double SupTerrComC = 0;
            double SupConsComC = 0;
            double SupConstrProp = 0;
            int año_consulta = 0;
            int edo_const;
            int nivel_const;
            int folio = 0;
            string serie = ".";
            string usoesp = "01";
            string propietario;
            string rfc = "S/RFC";
            string NumInterior;
            string Telefono = "S/TELEFONO";
            string domicilio_fiscal;
            double STerrProp;
            double STerrCom;
            double STerrConstrProp;
            double STerrConstrCom;
            double VALTERRPROP;
            double VALTERRCOM;
            double VALCONSPROP;
            double VALCONSCOM;
            double VALORCATASTRAL = 0;
            double PTJECONDOM;
            int ULTAÑOPAGO = 0;
            int ULTMESPAGO = 0;
            int ULTIMOPAGO = 0;
            int IMPTO95;
            string ACLARACION;
            string COBSPROP;
            int NVALORFISC;
            string FCAPTURA;
            string BAJA;
            int BONIFICACION;
            string usuario;
            string fecha_mod;
            string hora_mod;
            string operamod;
            int edood;
            int mpiood;
            int zonaod;
            int mznaod;
            int loteod;
            string edifod;
            string deptood;



            int i = 0;
            int totalFilas = dataGridView1.Rows.Count;

            progressBar1.Value = 0;
            progressBar1.Maximum = totalFilas;
            lblProgress.Text = "INICIANDO PROCESO DE GUARDADO...";
            lblProgress.Visible = true;
            progressBar1.Visible = true;

            while (i < dataGridView1.Rows.Count)
            {
                // Saltar la fila nueva (si está en modo edición)
                if (!dataGridView1.Rows[i].IsNewRow)
                {

                    int AÑO_CORRIENTE = Convert.ToInt32(dataGridView1.Rows[i].Cells[25].Value);//AÑO CORRIENTE
                    string uso = dataGridView1.Rows[i].Cells[26].Value.ToString();//USO 
                    string clase_construccion = "";
                    int cate_construccion = 0;
                    int año_construccion = 0;
                    int estado_construccion = 0;
                    int nivel_construccion = 0;
                    construccion = dataGridView1.Rows[i].Cells[22].Value.ToString();
                    if (construccion == "0" || construccion == "0.00" || construccion == "0.0")
                    {
                        construccion_si_no = 1;
                    }
                    else
                    {
                        construccion_si_no = 0;
                    }
                    if (construccion_si_no != 1)
                    {
                        año_construccion = Convert.ToInt32(dataGridView1.Rows[i].Cells[29].Value);//AÑO DE CONSTRUCCION
                        estado_construccion = Convert.ToInt32(dataGridView1.Rows[i].Cells[30].Value.ToString());//ESTADO DE CONSTRUCCION
                        nivel_construccion = Convert.ToInt32(dataGridView1.Rows[i].Cells[31].Value);//NIVEL DE CONSTRUCCION
                        clase_construccion = dataGridView1.Rows[i].Cells[27].Value.ToString();//CLASE DE CONSTRUCCION
                        cate_construccion = Convert.ToInt32(dataGridView1.Rows[i].Cells[28].Value.ToString());//CATEGORIA DE CONSTRUCCION
                    }
                    else
                    {
                        año_construccion = 0;
                        estado_construccion = 0;
                        nivel_construccion = 0;
                        clase_construccion = "-";
                        cate_construccion = 0;
                    }
                    MUNICIPIO = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);//MUNICIPIO
                    ZONA = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);//ZONA
                    MANZANA = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);//MANZANA
                    LOTE = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);//LOTE
                    EDIFICIO = dataGridView1.Rows[i].Cells[5].Value.ToString();//EDIFICIO
                    DEPTO = dataGridView1.Rows[i].Cells[6].Value.ToString();//DEPTO
                    SupConstrProp = Convert.ToDouble(dataGridView1.Rows[i].Cells[22].Value);
                    //edo_const = Convert.ToInt32(dataGridView1.Rows[i].Cells[30].Value.ToString());//ESTADO DE CONSTRUCCION
                    //nivel_const = Convert.ToInt32(dataGridView1.Rows[i].Cells[31].Value.ToString());//NIVEL DE CONSTRUCCION
                    propietario = dataGridView1.Rows[i].Cells[14].Value.ToString();//PROPIETARIO
                    NumInterior = dataGridView1.Rows[i].Cells[11].Value.ToString();//NUM INTERIOR
                    domicilio_fiscal = dataGridView1.Rows[i].Cells[24].Value.ToString();//DOMICILIO FISCAL
                    STerrProp = Convert.ToDouble(dataGridView1.Rows[i].Cells[20].Value);//SUPERFICIE DE TERRENO PROPIO PROPIEDADES
                    STerrCom = Convert.ToDouble(dataGridView1.Rows[i].Cells[21].Value);//SUPERFICIE DE TERRENO COMUN PROPIEDADES
                    STerrConstrProp = Convert.ToDouble(dataGridView1.Rows[i].Cells[22].Value);//SUPERFICIE DE CONSTRUCCION PROPIA PROPIEDADES
                    STerrConstrCom = Convert.ToDouble(dataGridView1.Rows[i].Cells[23].Value);//SUPERFICIE DE CONSTRUCCION COMUN PROPIEDADES
                    VALTERRPROP = Convert.ToDouble(dataGridView1.Rows[i].Cells[32].Value);//VALOR DE TERRENO PROPIO
                    VALTERRCOM = Convert.ToDouble(dataGridView1.Rows[i].Cells[33].Value);//VALOR DE TERRENO COMUN
                    VALCONSPROP = Convert.ToDouble(dataGridView1.Rows[i].Cells[34].Value);//VALOR DE CONSTRUCCION PROPIA
                    VALCONSCOM = Convert.ToDouble(dataGridView1.Rows[i].Cells[35].Value);//VALOR DE CONSTRUCCION COMUN
                    VALORCATASTRAL = Convert.ToDouble(dataGridView1.Rows[i].Cells[36].Value);//VALOR CATASTRAL
                    PTJECONDOM = Convert.ToDouble(dataGridView1.Rows[i].Cells[18].Value);//INDIVISO
                    //ULTAÑOPAGO = Program.añoActual;
                    //ULTMESPAGO = 4;
                    ULTIMOPAGO = 0;
                    IMPTO95 = 0;
                    ACLARACION = ".";
                    COBSPROP = "IMPORTACION DE ALTAS INMOBILIARIAS";
                    NVALORFISC = 0;
                    FCAPTURA = DateTime.Now.ToString("yyyyMMdd");
                    BAJA = ".";
                    BONIFICACION = 0;
                    usuario = Program.nombre_usuario;
                    fecha_mod = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                    hora_mod = DateTime.Now.ToString("HH:mm:ss");
                    operamod = "ALTA";
                    edood = 0;
                    mpiood = 0;
                    zonaod = 0;
                    mznaod = 0;
                    loteod = 0;
                    edifod = "00";
                    deptood = "0000";



                    // Validaciones
                    try
                    {
                        //////////////VERIRFICAMOS SI EXISTE LA CLAVE CATASTRAL
                        int verificar = 0;
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT ZONA";
                        con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PROPIEDADES";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE ESTADO = " + ESTADO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(EDIFICIO);
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(DEPTO) + ")";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                        con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT CHARLY = 1";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                        con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                        con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                        con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT CHARLY = 2";
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
                            MessageBox.Show("LA CLAVE CATASTRAL: " + MUNICIPIO + "-" + ZONA + "-" + MANZANA + "-" + LOTE + "-" + EDIFICIO + "-" + DEPTO + " YA EXISTE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            progressBar1.Visible = false;
                            lblProgress.Visible = false;
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
                        //////////////VERIRFICAMOS SI EXISTE LA CLAVE CATASTRAL MADRE
                        int verificar = 0;
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT ZONA";
                        con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PREDIOS";
                        con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE ESTADO = " + ESTADO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + MUNICIPIO;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + ZONA;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + MANZANA;
                        con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + LOTE + ")";
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

                        if (verificar == 2)
                        {
                            MessageBox.Show("LA CLAVE CATASTRAL: " + MUNICIPIO + "-" + ZONA + "-" + MANZANA + "-" + LOTE + "-" + " NO EXISTE, FALTA CLAVE MADRE", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            progressBar1.Visible = false;
                            lblProgress.Visible = false;
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
                    {   ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //OBTENEMOS EL ULTIMO AÑO Y MES DE PAGO DE LA CONFIGURACION
                        con.conectar_base_interno();
                        con.cadena_sql_interno = "";
                        con.cadena_sql_interno = con.cadena_sql_interno + "  SELECT ULTANIOPAGO, ULTMESPAGO  ";
                        con.cadena_sql_interno = con.cadena_sql_interno + "    FROM SONG_CONFIGURACION";
                        con.cadena_sql_interno = con.cadena_sql_interno + "   WHERE ID = 1";


                        con.cadena_sql_cmd_interno();
                        con.open_c_interno();
                        con.leer_interno = con.cmd_interno.ExecuteReader();


                        // Verificar si el resultado está vacío
                        if (!con.leer_interno.HasRows)
                        {
                            MessageBox.Show("NO EXISTE REGISTRO EN LA CONFIGURACION", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return; // Retornar si no hay resultados
                        }


                        while (con.leer_interno.Read())
                        {
                            if (con.leer_interno[0].ToString().Trim() != "")
                            {
                                ULTAÑOPAGO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                                ULTMESPAGO = Convert.ToInt32(con.leer_interno[1].ToString().Trim());

                            }
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
                        //SE REALIZA EL ALTA EN LA TABLA DE PROPIEDADES, HPROPIEDADES, UNIDADES DE CPMSTRUCCION Y HUNIDADES DE CONSTRUCCION
                        con.conectar_base_interno();
                        con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                        con.open_c_interno();

                        SqlCommand cmd = new SqlCommand("SONG_IMPORTACION", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
                        cmd.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                        cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 8).Value = Program.PEstado;
                        cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 8).Value = MUNICIPIO;
                        cmd.Parameters.Add("@ZONA", SqlDbType.Int, 8).Value = ZONA;
                        cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 8).Value = MANZANA;
                        cmd.Parameters.Add("@LOTE", SqlDbType.Int, 4).Value = LOTE;
                        cmd.Parameters.Add("@EDIFICIO", SqlDbType.Char, 3).Value = EDIFICIO;
                        cmd.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = DEPTO;
                        cmd.Parameters.Add("@FOLIO", SqlDbType.Int, 20).Value = folio;
                        cmd.Parameters.Add("@SERIE", SqlDbType.Char, 4).Value = serie;
                        cmd.Parameters.Add("@USO", SqlDbType.Char, 2).Value = uso;
                        cmd.Parameters.Add("@USOESP", SqlDbType.Char, 20).Value = usoesp;
                        cmd.Parameters.Add("@PROPIETARIO", SqlDbType.Char, 100).Value = propietario;
                        cmd.Parameters.Add("@RFC", SqlDbType.Char, 20).Value = rfc;
                        cmd.Parameters.Add("@NUM_INT", SqlDbType.Char, 20).Value = NumInterior;
                        cmd.Parameters.Add("@TELEFONO", SqlDbType.Char, 20).Value = Telefono;
                        cmd.Parameters.Add("@DOMFIS", SqlDbType.Char, 150).Value = domicilio_fiscal;
                        cmd.Parameters.Add("@STERRPROP", SqlDbType.Float, 8).Value = STerrProp;
                        cmd.Parameters.Add("@STERRCOM", SqlDbType.Float, 8).Value = STerrCom;
                        cmd.Parameters.Add("@SCONSPRO", SqlDbType.Float, 8).Value = STerrConstrProp;
                        cmd.Parameters.Add("@SCONSCOM", SqlDbType.Float, 8).Value = STerrConstrCom;
                        cmd.Parameters.Add("@VTERRPROP", SqlDbType.Float, 20).Value = VALTERRPROP;
                        cmd.Parameters.Add("@VTERRCOM", SqlDbType.Float, 20).Value = VALTERRCOM;
                        cmd.Parameters.Add("@VCONSPROP", SqlDbType.Float, 20).Value = VALCONSPROP;
                        cmd.Parameters.Add("@VCONSCOM", SqlDbType.Float, 20).Value = VALCONSCOM;
                        cmd.Parameters.Add("@PTJECONDOM", SqlDbType.Float, 10).Value = PTJECONDOM;
                        cmd.Parameters.Add("@ULTANIOPAG", SqlDbType.Int, 8).Value = ULTAÑOPAGO;
                        cmd.Parameters.Add("@ULTMESPAG", SqlDbType.Int, 8).Value = ULTMESPAGO;
                        cmd.Parameters.Add("@ULTIMPPAG", SqlDbType.Int, 8).Value = ULTIMOPAGO;
                        cmd.Parameters.Add("@IMPTO95", SqlDbType.Int, 8).Value = IMPTO95;
                        cmd.Parameters.Add("@ACLARACIONES", SqlDbType.Char, 200).Value = ACLARACION;
                        cmd.Parameters.Add("@COBSPROP", SqlDbType.Char, 100).Value = COBSPROP;
                        cmd.Parameters.Add("@NVALORFISC", SqlDbType.Int, 8).Value = NVALORFISC;
                        cmd.Parameters.Add("@FCAPTURA", SqlDbType.Char, 20).Value = FCAPTURA;
                        cmd.Parameters.Add("@BAJA", SqlDbType.Char, 20).Value = BAJA;
                        cmd.Parameters.Add("@BONIFIC", SqlDbType.Int, 8).Value = BONIFICACION;
                        cmd.Parameters.Add("@USER", SqlDbType.Char, 20).Value = usuario;
                        cmd.Parameters.Add("@FECMOD", SqlDbType.Char, 20).Value = fecha_mod;
                        cmd.Parameters.Add("@HORAMOD", SqlDbType.Char, 20).Value = hora_mod;
                        cmd.Parameters.Add("@OPERACION", SqlDbType.Char, 20).Value = operamod;
                        cmd.Parameters.Add("@CLASECONST", SqlDbType.Char, 1).Value = clase_construccion;
                        cmd.Parameters.Add("@CATEGCONST", SqlDbType.Int, 8).Value = cate_construccion;
                        cmd.Parameters.Add("@AÑOCONST", SqlDbType.Int, 8).Value = año_construccion;
                        cmd.Parameters.Add("@ESTADOCOSNT", SqlDbType.Int, 8).Value = estado_construccion;
                        cmd.Parameters.Add("@NIVCONS", SqlDbType.Int, 8).Value = nivel_construccion;
                        cmd.Connection = con.cnn_interno;
                        cmd.ExecuteNonQuery();
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

                    try
                    {
                        //SE REALIZA EL ALTA EN LA TABLA DE VALORES DE LA CLAVE CATASTRAL
                        con.conectar_base_interno();
                        con.cadena_sql_interno = ""; //Se limpia la cadena de texto para dejarla vacia
                        con.open_c_interno();

                        SqlCommand cmd = new SqlCommand("SongAltasValores", con.cnn_interno); //Nombre del procedimiento almacenado que va a utilizar 
                        cmd.CommandType = CommandType.StoredProcedure; //Se le indica al sistema que el comando a utilzar será un procedimiento almacenado 
                        cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 8).Value = Program.PEstado;
                        cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 8).Value = MUNICIPIO;
                        cmd.Parameters.Add("@ZONA", SqlDbType.Int, 8).Value = ZONA;
                        cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 8).Value = MANZANA;
                        cmd.Parameters.Add("@LOTE", SqlDbType.Int, 4).Value = LOTE;
                        cmd.Parameters.Add("@EDIFICIO", SqlDbType.Char, 3).Value = EDIFICIO;
                        cmd.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = DEPTO;
                        cmd.Parameters.Add("@valida", SqlDbType.Float, 1).Direction = ParameterDirection.Output;
                        cmd.Connection = con.cnn_interno;
                        cmd.ExecuteNonQuery();

                        int validacion = Convert.ToInt32(cmd.Parameters["@valida"].Value);
                        con.cerrar_interno();
                        if (validacion != 1)
                        {
                            MessageBox.Show("ERROR AL REALIZAR EL ALTA DE VALORES EN LA CLAVE CATASTRAL: " + MUNICIPIO + "-" + ZONA + "-" + MANZANA + "-" + LOTE + "-" + EDIFICIO + "-" + DEPTO, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            progressBar1.Visible = false;
                            lblProgress.Visible = false;
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
                    int progreso = i;
                    progressBar1.Value = progreso;
                    lblProgress.Text = $"IMPORTANDO LA CLAVE CATASTRAL {MUNICIPIO} - {ZONA} - {MANZANA} - {LOTE} - {EDIFICIO} - {DEPTO}, PROCESO {i + 1} de {totalFilas - 1}...";

                }
                i++;
            }
            MessageBox.Show("IMPORTACION EXITOSA", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnGuardar.Enabled = false;
            cmdSalida.Enabled = false;


        }

        private void frmImportacion_Load(object sender, EventArgs e)
        {
            label7.Text = "Usuario: " + Program.nombre_usuario;
            Inicio();
        }

        private void Inicio()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            btnGuardar.Enabled = false;
            btnValidar.Enabled = false;
            cmdNuevo.Enabled = true;
            cmdCancela.Enabled = true;
            lblProgress.Text = "";
            cmdSalida.Enabled = true;
            btnImportar.Enabled = false;
            progressBar1.Visible = false;
            error = 0;

        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            btnGuardar.Enabled = false;
            btnValidar.Enabled = false;
            btnImportar.Enabled = true;
            cmdNuevo.Enabled = false;
        }

        private void cmdCancela_Click(object sender, EventArgs e)
        {
            Inicio();
        }

        private void cmdSalida_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
        }
    }
}
