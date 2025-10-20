using AccesoBase;
using GMap.NET.MapProviders;
using Marmat.Forms.Skin;
using Microsoft.Reporting.WinForms;
using SMACatastro.formaReporte;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using System.Web.Management;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.XPath;
using Telerik.SvgIcons;
using Telerik.WinControls;
using Utilerias;
using Excel = Microsoft.Office.Interop.Excel;
using Form = System.Windows.Forms.Form;



namespace SMACatastro.catastroCartografia
{
    public partial class frmCatastro04ReporteCartografia : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        int tipoReporte, validacionCajasFecha, validacionFechaFormato, totalAlta, totalCambios, totalCertificacion, totalManifestacion, totalAltasRevision, tipoAutorizado, totalAltasSis, totalCambiosSis, totalCertiSis, tipoReporteJefatura = 0;
        int banderaArea, UbicacionJefatura = 0;
        int variableOperacionAutorizaNoAutorizaCartografia, variableOperacionAutorizaNoAutorizaVentanillas= 0;
        string ubicacionCartografia, ubicacionVentanilla, Usuario, fecha_iniL, fecha_finL = "";
        DateTime spFECHA_INI, spFECHA_FIN;
        Util util = new Util();
        DataTable dt;
        public frmCatastro04ReporteCartografia()
        {
            InitializeComponent();
            llenadoCombos();
        }
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void frmCatastro04ReporteCartografia_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "USUARIO: " + Program.nombre_usuario.ToString();
            limpiarTodo();
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// PARA COLOCAR LA FECHA Y HORA EN LAS ETIQUETAS DE ABAJO 
        //////////////////////////////////////////////////////////////////////////////////////////
        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// PARA COLOCAR LA FECHA Y HORA EN LAS ETIQUETAS DE ABAJO 
        //////////////////////////////////////////////////////////////////////////////////////////
        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss tt");
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////TOOLTIP A LOS BOTONES, ES DECIR; AL PASAR EL CURSOR INDICA UNA LEYENDA 
        //////////////////////////////////////////////////////////////////////////////////////////
        private void btnMinimizar_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnMinimizar, "MINIMIZAR FORMULARIO");
        }
        private void cmdNuevo_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(cmdNuevo, "NUEVA BÚSQUEDA");
        }
        private void cmdCancela_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(cmdCancela, "LIMPIAR PANTALLA");
        }
        private void cmdSalida_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(cmdSalida, "SALIR DE LA PANTALLA");
        }
        private void btnRefreshCartografia_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnRefresh, "REFRESH");
        }
        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnRefresh, "REFRESH");
        }
        private void btnGenerarExcel_MouseHover(object sender, EventArgs e)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btnGenerarExcel, "EXPORTAR A EXCEL LISTADO");
        }
        private void btnGenerarPDF_MouseHover(object sender, EventArgs e)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(btnGenerarPDF, "GENERAR REPORTE EN PDF");
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// TODOS LOS DIFERENTES MÉTODOS 
        //////////////////////////////////////////////////////////////////////////////////////////
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
        //////////LLENAR COMBOS DE FECHAS
        void llenadoCombos()
        {
            cboDia1.Items.Clear();
            cboDia1.Items.Add("01");
            cboDia1.Items.Add("02");
            cboDia1.Items.Add("03");
            cboDia1.Items.Add("04");
            cboDia1.Items.Add("05");
            cboDia1.Items.Add("06");
            cboDia1.Items.Add("07");
            cboDia1.Items.Add("08");
            cboDia1.Items.Add("09");
            cboDia1.Items.Add("10");
            cboDia1.Items.Add("11");
            cboDia1.Items.Add("12");
            cboDia1.Items.Add("13");
            cboDia1.Items.Add("14");
            cboDia1.Items.Add("15");
            cboDia1.Items.Add("16");
            cboDia1.Items.Add("17");
            cboDia1.Items.Add("18");
            cboDia1.Items.Add("19");
            cboDia1.Items.Add("20");
            cboDia1.Items.Add("21");
            cboDia1.Items.Add("22");
            cboDia1.Items.Add("23");
            cboDia1.Items.Add("24");
            cboDia1.Items.Add("25");
            cboDia1.Items.Add("26");
            cboDia1.Items.Add("27");
            cboDia1.Items.Add("28");
            cboDia1.Items.Add("29");
            cboDia1.Items.Add("30");
            cboDia1.Items.Add("31");
            cboDia1.SelectedIndex = -1;

            cboMes1.Items.Clear();
            cboMes1.Items.Add("01");
            cboMes1.Items.Add("02");
            cboMes1.Items.Add("03");
            cboMes1.Items.Add("04");
            cboMes1.Items.Add("05");
            cboMes1.Items.Add("06");
            cboMes1.Items.Add("07");
            cboMes1.Items.Add("08");
            cboMes1.Items.Add("09");
            cboMes1.Items.Add("10");
            cboMes1.Items.Add("11");
            cboMes1.Items.Add("12");
            cboMes1.SelectedIndex = -1;

            cboAño1.Items.Clear();
            cboAño1.Items.Add("2023"); //revisar esto (borrar los años)
            cboAño1.Items.Add("2024");
            cboAño1.Items.Add("2025");
            cboAño1.Items.Add("2026");
            cboAño1.Items.Add("2027");
            cboAño1.Items.Add("2028");
            cboAño1.Items.Add("2029");
            cboAño1.Items.Add("2030");
            cboAño1.Items.Add("2031");
            cboAño1.Items.Add("2032");
            cboAño1.Items.Add("2033");
            cboAño1.Items.Add("2034");
            cboAño1.SelectedIndex = -1;

            cboDia2.Items.Clear();
            cboDia2.Items.Add("01");
            cboDia2.Items.Add("02");
            cboDia2.Items.Add("03");
            cboDia2.Items.Add("04");
            cboDia2.Items.Add("05");
            cboDia2.Items.Add("06");
            cboDia2.Items.Add("07");
            cboDia2.Items.Add("08");
            cboDia2.Items.Add("09");
            cboDia2.Items.Add("10");
            cboDia2.Items.Add("11");
            cboDia2.Items.Add("12");
            cboDia2.Items.Add("13");
            cboDia2.Items.Add("14");
            cboDia2.Items.Add("15");
            cboDia2.Items.Add("16");
            cboDia2.Items.Add("17");
            cboDia2.Items.Add("18");
            cboDia2.Items.Add("19");
            cboDia2.Items.Add("20");
            cboDia2.Items.Add("21");
            cboDia2.Items.Add("22");
            cboDia2.Items.Add("23");
            cboDia2.Items.Add("24");
            cboDia2.Items.Add("25");
            cboDia2.Items.Add("26");
            cboDia2.Items.Add("27");
            cboDia2.Items.Add("28");
            cboDia2.Items.Add("29");
            cboDia2.Items.Add("30");
            cboDia2.Items.Add("31");
            cboDia2.SelectedIndex = -1;

            cboMes2.Items.Clear();
            cboMes2.Items.Add("01");
            cboMes2.Items.Add("02");
            cboMes2.Items.Add("03");
            cboMes2.Items.Add("04");
            cboMes2.Items.Add("05");
            cboMes2.Items.Add("06");
            cboMes2.Items.Add("07");
            cboMes2.Items.Add("08");
            cboMes2.Items.Add("09");
            cboMes2.Items.Add("10");
            cboMes2.Items.Add("11");
            cboMes2.Items.Add("12");
            cboMes2.SelectedIndex = -1;

            cboAño2.Items.Clear();
            cboAño2.Items.Add("2023"); //revisar esto 
            cboAño2.Items.Add("2024");
            cboAño2.Items.Add("2025");
            cboAño2.Items.Add("2026");
            cboAño2.Items.Add("2027");
            cboAño2.Items.Add("2028");
            cboAño2.Items.Add("2029");
            cboAño2.Items.Add("2030");
            cboAño2.Items.Add("2031");
            cboAño2.Items.Add("2032");
            cboAño2.Items.Add("2033");
            cboAño2.Items.Add("2034");
            cboAño2.SelectedIndex = -1;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// CAJAS AMARILLAS EN LOS COMBOBOX 
        //////////////////////////////////////////////////////////////////////////////////////////
        void cajasamarillas(int ca) //COLOCAR COLOR AMARILLO A LAS CAJAS // COMBOBOXES 
        {
            switch (ca)
            {
                case 0: cboDia1.BackColor = Color.Yellow; break;
                case 1: cboMes1.BackColor = Color.Yellow; break;
                case 2: cboAño1.BackColor = Color.Yellow; break;
                case 3: cboDia2.BackColor = Color.Yellow; break;
                case 4: cboMes2.BackColor = Color.Yellow; break;
                case 5: cboAño2.BackColor = Color.Yellow; break;
                case 6: cbbUbicacionCartografia.BackColor = Color.Yellow; break;
                case 7: cbbUbicacionVentanilla.BackColor = Color.Yellow; break;
                case 8: cbbUsuarioCartografia.BackColor = Color.Yellow; break;
                case 9: cbbUsuarioVentanilla.BackColor = Color.Yellow; break;
                case 10: cboEstatusCartografiaComparativa.BackColor = Color.Yellow; break;
                case 11: cboEstatusComparativaVentanilla.BackColor = Color.Yellow; break;
                    //
            }
        }
        void OcultarPaneles()
        {
            pnlCartografia.Visible = false;
            pnlVentanilla.Visible = false;
            pnlGeneral.Visible = false;
            pnlComparativa.Visible = false;
            pnlSistemas.Visible = false;
            pnUbicacionComparativa.Visible = false;
            pnlGeneral.Visible = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// LIMPIAR TODA LA PANTALLA 
        //////////////////////////////////////////////////////////////////////////////////////////
        void limpiarTodo()
        {
            cmdNuevo.Enabled = true;
            pnlCartografia.Visible = false;
            pnlVentanilla.Visible = false;
            pnlGeneral.Visible = false;
            pnlComparativa.Visible = false;
            pnlSistemas.Visible = false;
            pnUbicacionComparativa.Visible = false;
            pnlGeneral.Visible = true;
            //pnlBusquedaRevision.Visible = false;
            tipoReporte = 0;
            limpiarDataGridyLabelTotal();
            deshabilitarFechas();
            deshabilitarBotones();
            deshabilitarExportar();
            cboDia1.SelectedIndex = -1;
            cboMes1.SelectedIndex = -1;
            cboAño1.SelectedIndex = -1;
            cboDia2.SelectedIndex = -1;
            cboMes2.SelectedIndex = -1;
            cboAño2.SelectedIndex = -1;
            cbbUbicacionCartografia.Items.Clear();
            tipoReporteJefatura = 0;
            banderaArea = 0;
            rboCartografiaComp.Checked = false;
            rboCompVentanilla.Checked = false;
            

        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// LIMPIAR DATAGRID SOLAMENTE Y CONTEO A 0
        //////////////////////////////////////////////////////////////////////////////////////////
        void limpiarDataGridyLabelTotal()
        {
            dgResultado.DataSource = null;
            dgResultado.Rows.Clear();
            dgResultado.Columns.Clear();
            lblConteo.Location = new Point(855, 151); //
            
            label10.Text = "Total De Registros: ";
            lblConteo.Text = "0";
            tipoReporte = 0;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// VALIDAR QUE NINGÚN COMBOBOX DE LAS FECHAS ESTÉ VACIO 
        //////////////////////////////////////////////////////////////////////////////////////////
        void validaCajas()
        {
            if (cboDia1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DIA DE INICIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionCajasFecha = 1; cboDia1.Focus(); return; }
            if (cboMes1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL MES DE INICIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionCajasFecha = 1; cboMes1.Focus(); return; }
            if (cboAño1.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL AÑO DE INICIO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionCajasFecha = 1; cboAño1.Focus(); return; }
            if (cboDia2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL DIA FINAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionCajasFecha = 1; cboDia2.Focus(); return; }
            if (cboMes2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL MES FINAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionCajasFecha = 1; cboMes2.Focus(); return; }
            if (cboAño2.Text.Trim() == "") { MessageBox.Show("SE DEBE DE INGRESAR EL AÑO FINAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionCajasFecha = 1; cboAño2.Focus(); return; }

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////// PARA VALIDAR EL FORMATO DE LAS FECHAS (29 DE FEBRERO) FECHA FINAL NO ES MAYOR A FECHA INICIAL
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void validaFecha()
        {
            int validado = 0;
            string dateString = cboDia1.Text + "/" + cboMes1.Text + "/" + cboAño1.Text + " 00:00:00";
            CultureInfo enUS = new CultureInfo("en-US");
            DateTime dateValue;
            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy hh:mm:ss", enUS, DateTimeStyles.None, out dateValue))
            {
                validado = 1;
            }
            else { MessageBox.Show("LA FECHA DE INICIO, NO TIENE EL FORMATO ADECUADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionFechaFormato = 1; return; }

            string dateStrings = cboDia2.Text + "/" + cboMes2.Text + "/" + cboAño2.Text + " 00:00:00";
            CultureInfo enUSs = new CultureInfo("en-US");
            DateTime dateValues;
            if (DateTime.TryParseExact(dateStrings, "dd/MM/yyyy hh:mm:ss", enUSs, DateTimeStyles.None, out dateValues))
            {
                validado = 1;
            }
            else { MessageBox.Show("LA FECHA FINAL, NO TIENE EL FORMATO ADECUADO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionFechaFormato = 1; return; }

            DateTime spFECHA_INIS = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text + "-" + cboDia1.Text + "T00:00:00");
            DateTime spFECHA_FINS = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text + "-" + cboDia2.Text + "T23:59:59");

            if (spFECHA_INIS > spFECHA_FINS) { MessageBox.Show("LA FECHA INICIAL NO PUEDE SER MAYOR A LA FECHA FINAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionFechaFormato = 1; return; }
            if (spFECHA_FINS < spFECHA_INIS) { MessageBox.Show("LA FECHA FINAL NO PUEDE SER MENOR A LA FECHA INICIAL", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); validacionFechaFormato = 1; return; }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////// SOLO HABILITAR LOS COMBOBOX DE LAS FECHAS Y COLOCAR EL FOCUS A LAS CAJAS DE TEXTO 
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        void habilitarFechas() //habilitar las fechas  y colocar el foco a la combobox 
        {
            cboDia1.Enabled = true;
            cboMes1.Enabled = true;
            cboAño1.Enabled = true;
            cboDia2.Enabled = true;
            cboMes2.Enabled = true;
            cboAño2.Enabled = true;
            cboDia1.Focus();
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// DESHABILITAR LOS COMBOBOX DE LAS FECHAS  
        //////////////////////////////////////////////////////////////////////////////////////////
        void deshabilitarFechas() //DESHABILITAMOS LAS FECHAS 
        {
            cboDia1.Enabled = false;
            cboMes1.Enabled = false;
            cboAño1.Enabled = false;
            cboDia2.Enabled = false;
            cboMes2.Enabled = false;
            cboAño2.Enabled = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// HABILITAR LOS BOTONES DE ARRIBA PARA GENERAR LAS BÚSQUEDAS 
        //////////////////////////////////////////////////////////////////////////////////////////
        void habilitarBotones()
        {
            btnReporteCartografia.Enabled = true;
            btnReporteVentanilla.Enabled = true;
            btnReporteRevision.Enabled = true;
            btnReporteSistemas.Enabled = true;
            btnComparativas.Enabled = true;
            btnFoliosPendientesAutorizados.Enabled = true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// DESHABILITAR LOS BOTONES DE ARRIBA PARA GENERAR LAS BÚSQUEDAS
        //////////////////////////////////////////////////////////////////////////////////////////
        void deshabilitarBotones()
        {
            btnReporteCartografia.Enabled = false;
            btnReporteVentanilla.Enabled = false;
            btnReporteRevision.Enabled = false;
            btnReporteSistemas.Enabled = false;
            btnComparativas.Enabled = false;
            btnFoliosPendientesAutorizados.Enabled = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// HABILITAR LOS BOTONES DE ABAJO PARA EXPORTAR A PDF Y A EXCEL 
        //////////////////////////////////////////////////////////////////////////////////////////
        void habilitarExportar()
        {
            btnGenerarExcel.Enabled = true;
            btnGenerarPDF.Enabled = true;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        /////////// DESHABILITAR LOS BOTONES DE ABAJO PARA EXPORTAR A PDF Y A EXCEL
        //////////////////////////////////////////////////////////////////////////////////////////
        void deshabilitarExportar()
        {
            btnGenerarExcel.Enabled = false;
            btnGenerarPDF.Enabled = false;
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////MÉTODO PARA EXPORTAR A EXCEL EL LISTADO DEL DATAGRIDVIEW CON CIERTOS FORMATOS
        //////////////////////////////////////////////////////////////////////////////////////////
        void ExportarExcel() //solo revisar el nombre que va a poner el reporte 
        {
            if (dgResultado.Rows.Count == 0) //VALIDAR SI NO HAY RESULTADOS EN EL DATAGRIDVIEW 
            {
                MessageBox.Show("NO SE PUEDE EXPORTAR A EXCEL SIN INFORMACIÓN", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ////////////// SE GENERA UNA CADENA DE TEXTO NUEVA CON EL NOMBRE DEL ARCHIVO 
            string nombreArea = "";
            switch (tipoReporte) //PARAMETRO QUE RECIBE DEPENDE DEL TIPO DE ÁREA QUE SE SELECCIONE 
            {
                case 1:
                    nombreArea = "CARTOGRAFÍA";
                    break;
                case 2:
                    nombreArea = "VENTANILLA";
                    break;
                case 3:
                    nombreArea = "REVISIÓN";
                    break;
                case 4:
                    nombreArea = "SISTEMAS";
                    break;
                default:
                    nombreArea = "GENERAL";
                    break;
            }
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Add();
                worksheet = workbook.Worksheets[1]; // La primera hoja tiene índice 1
                worksheet.Name = "REPORTE CATASTRO"; // NOMBRE PERSONALIZADO AQUÍ PERO SOLO PARA LA HOJA EN LA QUE SE ABRE 
                // Formatear TODOS los encabezados
                for (int i = 1; i <= dgResultado.Columns.Count; i++)
                {
                    Excel.Range headerCell = worksheet.Cells[1, i];
                    headerCell.Value = dgResultado.Columns[i - 1].HeaderText;
                    // Aplicar formato a todos los encabezados
                    headerCell.Font.Size = 10.5;
                    headerCell.Font.Bold = true;
                    headerCell.Font.Color = Color.White;
                    headerCell.Interior.Color = Excel.XlRgbColor.rgbDarkMagenta;
                    headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    headerCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    headerCell.WrapText = true;
                }
                // Ajustar altura de la fila de encabezados
                worksheet.Rows[1].RowHeight = 25;
                // Datos
                for (int i = 0; i < dgResultado.Rows.Count; i++)
                {
                    if (dgResultado.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dgResultado.Columns.Count; j++)
                    {
                        Excel.Range dataCell = worksheet.Cells[i + 2, j + 1];
                        dataCell.Value = dgResultado.Rows[i].Cells[j].Value?.ToString() ?? "";

                        // Habilitar ajuste de texto y centrado para datos
                        //dataCell.AutoFit();
                        //dataCell.WrapText = true;
                        dataCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        dataCell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    }
                }
                // Determinar el rango completo de datos
                int totalFilas = dgResultado.Rows.Count;
                if (dgResultado.AllowUserToAddRows) totalFilas--;

                Excel.Range tablaRange = worksheet.Range
                [
                    worksheet.Cells[1, 1],
                    worksheet.Cells[totalFilas + 1, dgResultado.Columns.Count]
                ];
                // Aplicar bordes
                tablaRange.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                tablaRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlMedium;

                tablaRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                tablaRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlMedium;

                tablaRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                tablaRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlMedium;

                tablaRange.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                tablaRange.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlMedium;

                tablaRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                tablaRange.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;

                tablaRange.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                tablaRange.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;
                // PRIMERO: Autoajustar columnas al contenido
                worksheet.Columns.AutoFit();
                // SEGUNDO: Establecer un ancho mínimo para las columnas
                for (int i = 1; i <= dgResultado.Columns.Count; i++)
                {
                    Excel.Range column = worksheet.Columns[i];
                    if (column.ColumnWidth < 15) // Si el ancho es menor a 15, establecer mínimo de 15
                    {
                        column.ColumnWidth = 10;
                    }
                    // O establecer un ancho fijo para todas las columnas (opcional)
                    // column.ColumnWidth = 20; // Ancho fijo de 20 para todas las columnas
                }
                // TERCERO: Autoajustar filas según el contenido
                worksheet.Rows.AutoFit();
                // Establecer altura mínima para evitar filas demasiado pequeñas
                for (int i = 1; i <= totalFilas + 1; i++)
                {
                    if (worksheet.Rows[i].RowHeight < 10)
                    {
                        worksheet.Rows[i].RowHeight = 10;
                    }
                }
                // Crear carpeta si no existe
                string rutaCarpeta = @"C:\SONGUI\SMA_EXCEL";
                if (!Directory.Exists(rutaCarpeta))
                {
                    Directory.CreateDirectory(rutaCarpeta);
                }
                // Generar nombre de archivo (en este caso con fecha y la clave catastral)
                string nombreArchivo = $"REPORTECATASTRO-{nombreArea}-{DateTime.Now:yyyyMMdd}.xlsx";
                string rutaFinal = Path.Combine(rutaCarpeta, nombreArchivo);
                // Guardar
                workbook.SaveAs(rutaFinal, Excel.XlFileFormat.xlOpenXMLWorkbook);
                excelApp.Visible = true;
                MessageBox.Show($"ARCHIVO GUARDADO EN: \n{rutaFinal}", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR AL EXPORTAR A EXCEL:" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
            }
            finally
            {
                // Liberar recursos COM de manera segura
                if (worksheet != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);
                if (workbook != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                if (excelApp != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                // Limpiar variables
                worksheet = null;
                workbook = null;
                excelApp = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        //
        //MÉTODO PARA LAS COMPARATIVAS DE CARTOGRAFÍA, DONDE SE VE CUANTAS OPERACIONES HA REALIZADO QUÉ USUARIO
        //
        void porUsuario()
        {
            try
            {
                if (rboCartografiaComp.Checked = false)
                {
                    if (rboCompVentanilla.Checked = false)
                    {
                        MessageBox.Show("SE DEBE INGRESAR UN PARAMETRO DE BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                limpiarDataGridyLabelTotal();
                validacionCajasFecha = 0;
                validaCajas();
                if (validacionCajasFecha == 1) { return; }
                ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
                validacionFechaFormato = 0;
                validaFecha();
                if (validacionFechaFormato == 1) { return; }

                DateTime spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text + "-" + cboDia1.Text + "T00:00:00");
                DateTime spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text + "-" + cboDia2.Text + "T23:59:59");
                // --- 1. Definir los valores de las fechas (asumiendo DateTimePicker) ---
                // NOTA: Asegúrate de que los tipos de datos coincidan con tu SP (DATE, DATETIME, etc.)


                // --- 2. Preparar la conexión y el comando ---
                con.conectar_base_interno();
                // No necesitamos limpiar 'cadena_sql_interno' aquí.

                // El nombre del procedimiento almacenado que SÍ devuelve datos (debes crear uno)
                string nombreProcedimiento = "SP_REPORTE_POR_USUARIO_COMPARATIVA";

                SqlCommand cmd = new SqlCommand(nombreProcedimiento, con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;

                // --- 3. Agregar los parámetros de fecha ---
                // Usamos SqlDbType.Date si tu campo en SQL es DATE.
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = spFECHA_INI;
                // Para la fecha final, a menudo se añade 1 día y se usa '<', o se incluye la hora final (23:59:59)
                // Usaremos el valor de la fecha final para incluir ese día completo.
                cmd.Parameters.Add("@FechaFin", SqlDbType.DateTime).Value = spFECHA_FIN;

                // --- (Opcional) Puedes añadir otros parámetros si tu SP los necesita ---
                // cmd.Parameters.Add("@ESTADO", SqlDbType.Int).Value = Program.PEstado;

                // --- 4. Usar SqlDataAdapter para ejecutar y llenar un DataTable ---
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                // Abre la conexión (ya lo haces con con.open_c_interno(), pero lo mantengo explícito)
                // con.open_c_interno(); 

                da.Fill(dt); // Ejecuta el SP y llena el DataTable

                // --- 5. Asignar al DataGridView ---
                dgResultado.DataSource = dt;

                // con.cerrar_interno(); // La conexión se cierra al salir del 'using' del DataAdapter,
                // pero como usas métodos externos, la cerramos manualmente.
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


                dgResultado.Columns[0].Width = 300; //NOMBRE
                dgResultado.Columns[1].Width = 200; //alta
                dgResultado.Columns[2].Width = 200; //CAMBIOS
                dgResultado.Columns[3].Width = 200; //CERTI
                dgResultado.Columns[4].Width = 200; //MANIFE
                dgResultado.Columns[5].Width = 200; //MANIFE
                //dgResultado.Columns[6].Width = 500; //MANIFE

                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 


                mostrarComparativaJefatura frmReporte = new mostrarComparativaJefatura();
                //MessageBox.Show($"Se cargaron {dt.Rows.Count} registros.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                habilitarExportar();
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString();
                tipoReporte = 11;
                banderaArea = 1;
               
            }
            catch (Exception ex)
            {
                // Asegurarse de cerrar la conexión en caso de error
                if (con.cnn_interno.State == ConnectionState.Open)
                {
                    con.cerrar_interno();
                }
                CapturarPantalla();
                MessageBox.Show("Ocurrió un error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno(); 
            }
        }
        //
        //MÉTODO PARA LAS COMPARATIVAS DE CARTOGRAFÍA, DONDE SE VE CUANTAS OPERACIONES HA REALIZADO QUÉ USUARIO
        //
        void porTramite()
        {
            try
            {
                if (rboCartografiaComp.Checked = false)
                {
                    if (rboCompVentanilla.Checked = false)
                    {
                        MessageBox.Show("SE DEBE INGRESAR UN PARAMETRO DE BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                limpiarDataGridyLabelTotal();
                validacionCajasFecha = 0;
                validaCajas();
                if (validacionCajasFecha == 1) { return; }
                ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
                validacionFechaFormato = 0;
                validaFecha();
                if (validacionFechaFormato == 1) { return; }


                DateTime spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text + "-" + cboDia1.Text + "T00:00:00");
                DateTime spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text + "-" + cboDia2.Text + "T23:59:59");
                
                con.conectar_base_interno();
                
                
                string nombreProcedimiento = "SP_REPORTE_TRAMITES";

                SqlCommand cmd = new SqlCommand(nombreProcedimiento, con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@FechaInicio", SqlDbType.DateTime).Value = spFECHA_INI;
                cmd.Parameters.Add("@FechaFinal", SqlDbType.DateTime).Value = spFECHA_FIN;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt); // Ejecuta el SP y llena el DataTable
                dgResultado.DataSource = dt;
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

                dgResultado.Columns[0].Width = 300; //NOMBRE
                dgResultado.Columns[1].Width = 200; //alta
                dgResultado.Columns[2].Width = 200; //CAMBIOS
                dgResultado.Columns[3].Width = 200; //CERTI
                dgResultado.Columns[4].Width = 200; //MANIFE
                dgResultado.Columns[5].Width = 200; //MANIFE
                dgResultado.Columns[6].Width = 500; //MANIFE

                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString();
                //mostrarComparativaJefatura frmReporte = new mostrarComparativaJefatura(dt);
                habilitarExportar();
                tipoReporte = 13;
                //MessageBox.Show($"Se cargaron {dt.Rows.Count} registros.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Asegurarse de cerrar la conexión en caso de error
                if (con.cnn_interno.State == ConnectionState.Open)
                {
                    con.cerrar_interno();
                }
                MessageBox.Show("Ocurrió un error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                
            }
        }
        //
        //MÉTODO PARA CONSULTAR LOS FOLIOS POR AUTORIZAR O AUTORIZADOS DEL ÁREA DE VENTANILLA
        //
        void foliosPorAutorizarVentanilla()
        {
            limpiarDataGridyLabelTotal();
            validacionCajasFecha = 0;
            validaCajas();
            if (validacionCajasFecha == 1) { return; }
            ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
            validacionFechaFormato = 0;
            validaFecha();
            if (validacionFechaFormato == 1) { return; }




            //////CONVERTIR A CADENA DE TEXO LOS COMBOBOX PARA LAS FECHAS 
            fecha_iniL = cboAño1.Text + cboMes1.Text + cboDia1.Text + " 00:00:00";
            fecha_finL = cboAño2.Text + cboMes2.Text + cboDia2.Text + " 23:59:59";

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "     SELECT CNV.MUNICIPIO, CNV.ZONA, CNV.MANZANA, CNV.LOTE,";
            con.cadena_sql_interno = con.cadena_sql_interno + "            CNV.EDIFICIO, CNV.DEPTO, CNV.DESCRIPCION, CNV.USUARIO ";
            con.cadena_sql_interno = con.cadena_sql_interno + "       FROM CAT_NEW_CARTOGRAFIA_2025 CNC, CAT_NEW_VENTANILLA_2025 CNV, CAT_DONDE_VA_2025 CND    ";
            con.cadena_sql_interno = con.cadena_sql_interno + "      WHERE CNC.SERIE = CNV.SERIE ";
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CNC.FOLIO_ORIGEN = CNV.FOLIO_ORIGEN ";
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CNV.FECHA >= " + util.scm(fecha_iniL);
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CNV.FECHA <= " + util.scm(fecha_finL);
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CND.CARTOGRAFIA =1";
            if (variableOperacionAutorizaNoAutorizaVentanillas == 0)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "        AND CND.VENTANILLA = 0";
                tipoReporteJefatura = 3;
                lblConteo.Location = new Point(1005, 151);
                label10.Text = "Total De Folios Sin Autorizar Por Ventanilla:";
            }
            if (variableOperacionAutorizaNoAutorizaVentanillas == 1)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "        AND CND.VENTANILLA = 1";
                tipoReporteJefatura = 4;
                lblConteo.Location = new Point(1005, 151);
                label10.Text = "Total De Folios Sin Autorizar Por Ventanilla:";
            }
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CND.ELIMINADO = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CND.SISTEMAS = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND CNV.SERIE = CND.SERIE";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND CNV.FOLIO_ORIGEN = CND.FOLIO_ORIGEN";

            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                limpiarDataGridyLabelTotal();
                con.cerrar_interno();
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN AL RESPECTO", "ERROR",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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

                dgResultado.Columns[0].Width = 300; //NOMBRE
                dgResultado.Columns[1].Width = 200; //alta
                dgResultado.Columns[2].Width = 200; //CAMBIOS
                dgResultado.Columns[3].Width = 200; //CERTI
                dgResultado.Columns[4].Width = 200; //MANIFE
                dgResultado.Columns[5].Width = 200; //MANIFE

                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                                                       //habilitarFechas();
                habilitarExportar();
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString();
                tipoReporte = 13;
            }
        }
        void sistemas()
        {
            validacionCajasFecha = 0;
            validaCajas();
            if (validacionCajasFecha == 1) { return; }
            ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
            validacionFechaFormato = 0;
            validaFecha();
            if (validacionFechaFormato == 1) { return; }


            if (rbnAutorizadosSistemas.Checked == false)
            {
                if (rbnNoAutorizadosSistemas.Checked == false)
                {
                    MessageBox.Show("DEBE SELECCIONAR UNA OPCIÓN DE SISTEMAS", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            DateTime spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text + "-" + cboDia1.Text + "T00:00:00");
            DateTime spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text + "-" + cboDia2.Text + "T23:59:59");

            //////CONVERTIR A CADENA DE TEXO LOS COMBOBOX PARA LAS FECHAS 
            string fecha_iniL = cboAño1.Text + cboMes1.Text + cboDia1.Text + " 00:00:00";
            string fecha_finL = cboAño2.Text + cboMes2.Text + cboDia2.Text + " 23:59:59";


            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "     SELECT CNC.DESCRIPCION, CNC.MUNICIPIO, CNC.ZONA,";
            con.cadena_sql_interno = con.cadena_sql_interno + "            CNC.MANZANA, CNC.LOTE, CNC.EDIFICIO, CNC.DEPTO,";
            con.cadena_sql_interno = con.cadena_sql_interno + "            CDV.FECHA_REV, CDV.FECHA_SIS, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "            CDV.HORA_SIS, CDV.USU_SISTEMAS";
            con.cadena_sql_interno = con.cadena_sql_interno + "       FROM CAT_DONDE_VA_2025 CDV, CAT_NEW_CARTOGRAFIA_2025 CNC";
            con.cadena_sql_interno = con.cadena_sql_interno + "      WHERE CDV.FECHA_REV    >=" + util.scm(fecha_iniL);
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.FECHA_REV    <=" + util.scm(fecha_finL);
            if (rbnAutorizadosSistemas.Checked == true)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.SISTEMAS    = 1 ";
                tipoAutorizado = 1;
            }
            if (rbnNoAutorizadosSistemas.Checked == true)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.SISTEMAS   = 0 ";
                tipoAutorizado = 0;
            }
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.REVISO        = 1 ";
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.ELIMINADO     = 0 ";
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CNC.SERIE         =  " + util.scm(Program.serie);
            con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.FOLIO_ORIGEN  = CNC.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "        ORDER BY CDV.FECHA_REV";



            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                con.cerrar_interno();
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN REFERENTE A LA BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                deshabilitarFechas();
                habilitarExportar();
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString();
                tipoReporte = 4;


                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT SUMA = COUNT (*)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_DONDE_VA_2025 CDV, CAT_NEW_CARTOGRAFIA_2025 CNC";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CDV.FOLIO_ORIGEN = CNC.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.FECHA_SIS >= '" + fecha_iniL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.FECHA_SIS <= '" + fecha_finL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.REVISO = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.ELIMINADO = 0 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.SERIE = CNC.SERIE  ";
                if (rbnAutorizadosSistemas.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.SISTEMAS    = 1 ";
                }
                if (rbnNoAutorizadosSistemas.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.SISTEMAS    = 0 ";
                }
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CNC.DESCRIPCION = 'ALTA DE CLAVE'  ";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        totalAltasSis = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///cerrar la conexión
                con.cerrar_interno();


                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT SUMA = COUNT (*)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_DONDE_VA_2025 CDV, CAT_NEW_CARTOGRAFIA_2025 CNC";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CDV.FOLIO_ORIGEN = CNC.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.FECHA_SIS >= '" + fecha_iniL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.FECHA_SIS <= '" + fecha_finL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.REVISO = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.ELIMINADO = 0 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.ELIMINADO = 0 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.SERIE = CNC.SERIE  ";
                if (rbnAutorizadosSistemas.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.SISTEMAS    = 1 ";
                }
                if (rbnNoAutorizadosSistemas.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.SISTEMAS    = 0 ";
                }
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CNC.DESCRIPCION = 'CERTIFICACIONES' ";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        totalCertiSis = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///cerrar la conexión
                con.cerrar_interno();

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT SUMA = COUNT (*)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_DONDE_VA_2025 CDV, CAT_NEW_CARTOGRAFIA_2025 CNC";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CDV.FOLIO_ORIGEN = CNC.FOLIO_ORIGEN";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.FECHA_SIS >= '" + fecha_iniL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.FECHA_SIS <= '" + fecha_finL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.REVISO = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.ELIMINADO = 0 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.ELIMINADO = 0 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CDV.SERIE = CNC.SERIE  ";
                if (rbnAutorizadosSistemas.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.SISTEMAS    = 1 ";
                }
                if (rbnNoAutorizadosSistemas.Checked == true)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "        AND CDV.SISTEMAS    = 0 ";
                }
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND CNC.DESCRIPCION = '00100CAMBIO DE CLAVE' ";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        totalCambiosSis = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///cerrar la conexión
                con.cerrar_interno();


                //TIPO DE REPORTE (VER ) 

                //alta , falta cambio y certi
                /*
                "
"
"
"

"    AND CDV.SISTEMAS = 1" //rbn1 
            AND CDV.SISTEMAS = 0" rbn2 
                                AND CNC.DESCRIPCION = 'ALTA DE CLAVE'"
             

                 */


            }
        }
        //
        //MÉTODO PARA CONSULTAR LOS FOLIOS POR AUTORIZAR O AUTORIZADOS DEL ÁREA DE VENTANILLA
        //
        void foliosPorAutorizarCartografia()
        {
            limpiarDataGridyLabelTotal();
            validacionCajasFecha = 0;
            validaCajas();
            if (validacionCajasFecha == 1) { return; }
            ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
            validacionFechaFormato = 0;
            validaFecha();
            if (validacionFechaFormato == 1) { return; }




            //////CONVERTIR A CADENA DE TEXO LOS COMBOBOX PARA LAS FECHAS 
            fecha_iniL = cboAño1.Text + cboMes1.Text + cboDia1.Text + " 00:00:00";
            fecha_finL = cboAño2.Text + cboMes2.Text + cboDia2.Text + " 23:59:59";
            //si el radiobutton es uno, cambiar las cosas al tipo de reporte 
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "          SELECT CNC.MUNICIPIO, CNC.ZONA, CNC.MANZANA, CNC.LOTE, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "                 CNC.EDIFICIO, CNC.DEPTO, CNC.DESCRIPCION, RTRIM(CNC.USUARIO) 'USUARIO'";
            con.cadena_sql_interno = con.cadena_sql_interno + "            FROM CAT_NEW_CARTOGRAFIA_2025 CNC, CAT_DONDE_VA_2025 CND";
            con.cadena_sql_interno = con.cadena_sql_interno + "           WHERE CNC.SERIE = CND.SERIE ";
            con.cadena_sql_interno = con.cadena_sql_interno + "             AND CNC.FOLIO_ORIGEN = CND.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "             AND CNC.FECHA >= " + util.scm(fecha_iniL);
            con.cadena_sql_interno = con.cadena_sql_interno + "             AND CNC.FECHA <= " + util.scm(fecha_finL);
            if (variableOperacionAutorizaNoAutorizaCartografia == 0)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "         AND CND.CARTOGRAFIA = 0";
                tipoReporteJefatura = 1; //VARIABLE QUE SE UTILIZA PARA SABER DENTRO DEL PROCEDIMIENTO ALMAECENADO, QUÉ CONSULTA REALIZAR
                lblConteo.Location = new Point(1015, 151);
                label10.Text = "Total De Folios Sin Autorizar Por Cartografía:";
            }
            if (variableOperacionAutorizaNoAutorizaCartografia == 1)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "             AND CND.CARTOGRAFIA = 1";
                tipoReporteJefatura = 2;  //VARIABLE QUE SE UTILIZA PARA SABER DENTRO DEL PROCEDIMIENTO ALMAECENADO, QUÉ CONSULTA 
                lblConteo.Location = new Point(1015, 151);
                label10.Text = "Total De Folios Autorizados Por Cartografía:";
            }

            con.cadena_sql_interno = con.cadena_sql_interno + "             AND CND.ELIMINADO = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "             AND CND.SISTEMAS = 0";

            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                con.cerrar_interno();
                limpiarDataGridyLabelTotal();
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN AL RESPECTO", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                //error 
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

                dgResultado.Columns[0].Width = 80; //MUNICIPIO
                dgResultado.Columns[1].Width = 80; //ZONA
                dgResultado.Columns[2].Width = 110; //MANZANA
                dgResultado.Columns[3].Width = 110; //LOTE
                dgResultado.Columns[4].Width = 80; //EDIFICIO
                dgResultado.Columns[5].Width = 110; //DEPTO
                dgResultado.Columns[6].Width = 300; //DESC
                dgResultado.Columns[7].Width = 350; //USUARIO
                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                deshabilitarFechas();
                deshabilitarBotones();
                habilitarExportar();
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString();

                tipoReporte = 13;
            }
        }
        ///////////////////////////////////////////////////////////////////////////
        ///////CLICK A LOS BOTONES DEL FORMULARIO, EJEM. SALIDA, CANCELA, MINIMIZAR
        ///////////////////////////////////////////////////////////////////////////
        private void cmdSalida_Click(object sender, EventArgs e)
        {
            this.Close(); //cerrar el formulario 
        }
        ///////////////////////////////////////////////////////////////////////////
        ///////BOTON DE TACHE, DONDE AL DAR CLICK CANCELA TODO 
        ///////////////////////////////////////////////////////////////////////////
        private void cmdCancela_Click(object sender, EventArgs e)
        {
            limpiarTodo();
            //comparativasCartografía();
            //comparativasVentanilla();
            //foliosPorAutorizarCartografia();
        }
        ///////////////////////////////////////////////////////////////////////////
        ///////BOTON DE MINIMIZAR PARA EL FORMULARIO 
        ///////////////////////////////////////////////////////////////////////////
        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /////////////////////////////////////////
        //llenar el combo de usuarios cartografía 
        /////////////////////////////////////////
        void llenarUsuarioCarto()
        {
            cbbUsuarioCartografia.Items.Clear();
            cbbUsuarioCartografia.Items.Add("GENERAL");
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT DISTINCT USUARIO";
                con.cadena_sql_interno = con.cadena_sql_interno + "            FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        cbbUsuarioCartografia.Items.Add(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }
        }
        /////////////////////////////////////////
        //llenar el combo de usuarios ventanilla
        /////////////////////////////////////////
        void llenarUsuarioVenta()
        {
            cbbUsuarioVentanilla.Items.Clear();
            cbbUsuarioVentanilla.Items.Add("GENERAL");
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT DISTINCT USUARIO";
                con.cadena_sql_interno = con.cadena_sql_interno + "            FROM CAT_NEW_VENTANILLA_2025";
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        cbbUsuarioVentanilla.Items.Add(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                util.CapturarPantallaConInformacion(ex);
                System.Threading.Thread.Sleep(500);
                con.cerrar_interno();
                // Retornar false si ocurre un error
            }
        }
        //////////////////////////////////////////////////////////////////////////////
        /////////CLICK PARA LA CONSULTA DE CARTOGRAFÍA 
        //////////////////////////////////////////////////////////////////////////////
        private void btnReporteCartografia_Click(object sender, EventArgs e)
        {
            if (Program.acceso_nivel_acceso == 1 || Program.acceso_nivel_acceso == 3)
            {
                //CARTO ES 1 
                ///////VALIDAR LOS COMBOBOX DE LAS FECHAS, QUE NINGUNO ESTÉ VACIO
                validacionCajasFecha = 0;
                validaCajas();
                if (validacionCajasFecha == 1) { return; }
                ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
                validacionFechaFormato = 0;
                validaFecha();
                if (validacionFechaFormato == 1) { return; }
                //mostrar el panel de cartografia para agregar parametros 
                OcultarPaneles();
                pnlGeneral.Visible = false;
                pnlCartografia.Visible = true;

                //LLENAR EL COMBOBOX CON LAS UBICACIONES 

                cbbUbicacionCartografia.Items.Clear();

                cbbUbicacionCartografia.Items.Add("00 - GENERAL");
                cbbUbicacionCartografia.Items.Add("01 - ALTA DE CLAVE");
                cbbUbicacionCartografia.Items.Add("02 - CAMBIO DE CLAVE ");
                cbbUbicacionCartografia.Items.Add("03 - CERTIFICACIONES ");
                cbbUbicacionCartografia.Items.Add("05 - MANIFESTACIÓN");

                cbbUbicacionCartografia.Enabled = true;

                if (Program.acceso_nivel_acceso == 3)
                {
                    cbbUsuarioCartografia.Visible = true;
                    lblUsuarioCartografia.Visible = true;
                    cbbUsuarioCartografia.Enabled = true;
                    llenarUsuarioCarto();
                }
                else
                {
                    lblUsuarioCartografia.Visible = false;
                    cbbUsuarioCartografia.Visible = false;
                }

                
                //btnReporteCartografia.Enabled = true;
                //////es ocultar el panel de los parametros para los reportes
            }
            else
            {
                MessageBox.Show("TU NIVEL ES DIFERENTE AL NECESARIO PARA ACCEDER A ESTE BOTÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        ////////////////////////////////////////////////////////////////////////////
        /////BOTÓN DE VENTANILLA CONSULTA 
        ////////////////////////////////////////////////////////////////////////////
        private void btnReporteVentanilla_Click(object sender, EventArgs e)
        {
            if (Program.acceso_nivel_acceso == 2 || Program.acceso_nivel_acceso == 3)
            {
                limpiarDataGridyLabelTotal();
                //2 NIVEL 
                ///////VALIDAR LOS COMBOBOX DE LAS FECHAS, QUE NINGUNO ESTÉ VACIO
                validacionCajasFecha = 0;
                validaCajas();
                if (validacionCajasFecha == 1) { return; }
                ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
                validacionFechaFormato = 0;
                validaFecha();
                if (validacionFechaFormato == 1) { return; }
                OcultarPaneles();
                pnlGeneral.Visible = false;
                pnlVentanilla.Visible = true;

                //LLENAR EL COMBOBOX CON LAS UBICACIONES 
                cbbUbicacionVentanilla.Items.Clear();
                cbbUbicacionVentanilla.Items.Add("00 - GENERAL");
                cbbUbicacionVentanilla.Items.Add("01 - ALTA DE CLAVE");
                cbbUbicacionVentanilla.Items.Add("02 - CAMBIO DE CLAVE ");
                cbbUbicacionVentanilla.Items.Add("03 - CERTIFICACIONES ");
                cbbUbicacionVentanilla.Items.Add("05 - MANIFESTACIÓN");

                cbbUbicacionVentanilla.Enabled = true;
                btnRefresh.Enabled = true;
                //btnReporteVentanilla.Enabled = true;
                //////es ocultar el panel de los parametros para los reportes   
                if (Program.acceso_nivel_acceso == 3)
                {
                    cbbUsuarioVentanilla.Visible = true;
                    cbbUsuarioVentanilla.Enabled = true;
                    lblUsuarioVentanilla.Visible = true;
                    llenarUsuarioVenta();
                }
                else
                {
                    lblUsuarioVentanilla.Visible = false;
                    cbbUsuarioVentanilla.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("TU NIVEL ES DIFERENTE AL NECESARIO PARA ACCEDER A ESTE BOTÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnReporteRevision_Click(object sender, EventArgs e)
        { ///esta parte de aquí hay que revisar si está correcto 
            
            if (Program.acceso_nivel_acceso == 3) //revisar el nivel 
            {
                OcultarPaneles();
                pnlGeneral.Visible = false;
                pnlComparativa.Visible = true;
                btnRefreshPendientesComparativa.Enabled = true;
            }
            else
            {
                //MENSAJE 
                MessageBox.Show("TU NIVEL ES DIFERENTE AL NECESARIO PARA ACCEDER A ESTE BOTÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnReporteSistemas_Click(object sender, EventArgs e) //NO 
        {
            if (Program.acceso_nivel_acceso == 3 || Program.acceso_nivel_acceso == 4)
            {
            pnlGeneral.Visible = false;
           
            limpiarDataGridyLabelTotal();
            ///////VALIDAR LOS COMBOBOX DE LAS FECHAS, QUE NINGUNO ESTÉ VACIO
            validacionCajasFecha = 0;
            validaCajas();
            if (validacionCajasFecha == 1) { return; }
            ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
            validacionFechaFormato = 0;
            validaFecha();
            if (validacionFechaFormato == 1) { return; }
            pnlSistemas.Visible = true;
            /////////VALIDAR QUE TENGA ALGO SELECCIONADO EN EL COMBOBOX
            //pnlRevision.Visible = true;
            //////FECHAS PARA MANDAR AL PROCEDIMIENTO ALMACENADO 
            btnRefreshSistemas.Enabled = true;
            
            /////CONSULTA MAL 
            CapturarPantalla();
            }
            else
             {
                 MessageBox.Show("TU NIVEL ES DIFERENTE AL NECESARIO PARA ACCEDER A ESTE BOTÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }
        ////////////////////////////////////////////////////////////
        ////////////EMPEZAR A COLOCAR LAS CAJAS AMARILLAS
        ////////////////////////////////////////////////////////////
        private void cboDia1_Enter(object sender, EventArgs e)
        {
            cajasamarillas(0);
        }
        private void cboMes1_Enter(object sender, EventArgs e)
        {
            cajasamarillas(1);
        }
        private void cboAño1_Enter(object sender, EventArgs e)
        {
            cajasamarillas(2);
        }
        private void cboDia2_Enter(object sender, EventArgs e)
        {
            cajasamarillas(3);
        }
        private void cboMes2_Enter(object sender, EventArgs e)
        {
            cajasamarillas(4);
        }
        private void cboAño2_Enter(object sender, EventArgs e)
        {
            cajasamarillas(5);
        }
        private void cbbUbicacionCartografia_Enter(object sender, EventArgs e)
        {
            cajasamarillas(6);
        }
        private void cbbUbicacionVentanilla_Enter(object sender, EventArgs e)
        {
            cajasamarillas(7);
        }
        private void cbbUsuarioCartografia_Enter(object sender, EventArgs e)
        {
            cajasamarillas(8);
        }
        //MÉTODO PARA LAS COMPARATIVAS ENTRE ÁREAS
        private void btnComparativas_Click(object sender, EventArgs e)
        {
            if (Program.acceso_nivel_acceso == 3) //revisar el nivel 
            {
                OcultarPaneles();
                pnlGeneral.Visible = false;
                pnUbicacionComparativa.Visible = true;
                btnRefreshFolios.Enabled = true;
            }
            else
            {
                //MENSAJE 
                MessageBox.Show("TU NIVEL ES DIFERENTE AL NECESARIO PARA ACCEDER A ESTE BOTÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //MÉTODO PARA ABRIR EL PANEL DE LOS FOLIOS 
        private void btnFoliosPendientesAutorizados_Click(object sender, EventArgs e)
        {
            if (Program.acceso_nivel_acceso == 3) //revisar el nivel 
            {
                OcultarPaneles();
                pnlGeneral.Visible = false;
                pnlComparativa.Visible = true;
                btnRefreshPendientesComparativa.Enabled = true;
            }
            else
            {
                //MENSAJE 
                MessageBox.Show("TU NIVEL ES DIFERENTE AL NECESARIO PARA ACCEDER A ESTE BOTÓN", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rboComparativaCartografia_CheckedChanged(object sender, EventArgs e)
        {
            cboEstatusCartografiaComparativa.Items.Clear();
            cboEstatusCartografiaComparativa.Items.Add("0 - SIN AUTORIZAR");
            cboEstatusCartografiaComparativa.Items.Add("1 - AUTORIZADOS");
            cboEstatusCartografiaComparativa.Enabled = true;

        }

        private void rboVentanillaComparativa_CheckedChanged(object sender, EventArgs e)
        {
            cboEstatusComparativaVentanilla.Items.Clear();
            cboEstatusComparativaVentanilla.Items.Add("0 - SIN AUTORIZAR");
            cboEstatusComparativaVentanilla.Items.Add("1 - AUTORIZADOS");
            cboEstatusComparativaVentanilla.Enabled = true;
        }

        private void btnCancelarPanelComparativa_Click(object sender, EventArgs e)
        {
            
            rboComparativaCartografia.Checked = false;
            cboEstatusCartografiaComparativa.SelectedIndex = -1;
            cboEstatusCartografiaComparativa.Enabled = false;

            rboVentanillaComparativa.Checked = false;
            cboEstatusComparativaVentanilla.SelectedIndex = -1;
            cboEstatusComparativaVentanilla.Enabled = false;

            limpiarDataGridyLabelTotal();
            habilitarBotones();
        }
        private void btnConsultaComparativa_Click(object sender, EventArgs e)
        {
            if (rboCartografiaComp.Checked == false)
            {
                if (rboCompVentanilla.Checked == false)
                {
                    MessageBox.Show("NECESITAS SELECCIONAR UN PARAMETRO DE BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            if (rboCartografiaComp.Checked == true)
            {
                porUsuario(); //ESTO DEBEMOS CAMBIAR (POR USUARIOS)
            }
            else if (rboCompVentanilla.Checked == true)
            {
                porTramite(); //ESTO DEBEMOS CAMBIAR (POR TIPO DE TRAMITES)
            } 
            
        }

        private void cboEstatusCartografiaComparativa_Enter(object sender, EventArgs e)
        {
            cajasamarillas(10);
        }

        private void cboEstatusComparativaVentanilla_Enter(object sender, EventArgs e)
        {
            cajasamarillas(11);
        }

        private void btnCancelarComparativa_Click(object sender, EventArgs e)
        {
            limpiarDataGridyLabelTotal();
            rboCartografiaComp.Checked = false;
            rboCompVentanilla.Checked = false;
            habilitarBotones();

        }
        //limpiar el data y ponerle fechas
        private void btnRefreshPendientesComparativa_Click(object sender, EventArgs e)
        {
            habilitarFechas();
            habilitarBotones();
            limpiarDataGridyLabelTotal();
            cboDia1.Focus();
            label1.BackColor = Color.Yellow;
        }

        private void btnRefreshFolios_Click(object sender, EventArgs e)
        {
            habilitarFechas();
            habilitarBotones();
            limpiarDataGridyLabelTotal();
            cboDia1.Focus();
            label1.BackColor = Color.Yellow;
        }

        ///////////////FOLIO PARA REVISAR LOS PENDIENTES O AUTORIZADOS POR LAS DISTINTAS ÁREAS 
        private void btnConsultaPendienteAutorizado_Click(object sender, EventArgs e)
        {   
            if (rboComparativaCartografia.Checked == false)
            {
                if (rboVentanillaComparativa.Checked == false)
                {
                    MessageBox.Show("NECESITAS SELECCIONAR UN PARAMETRO DE BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (rboVentanillaComparativa.Checked == true)
            {
                //vamos a declarar una variable de tipo entero para saber qué tipo de consulta va a realizar cartografía
                variableOperacionAutorizaNoAutorizaVentanillas = Convert.ToInt32(cboEstatusComparativaVentanilla.Text.Substring(0, 2));
                foliosPorAutorizarVentanilla();

        
            }
            else if (rboComparativaCartografia.Checked == true)
            {
                variableOperacionAutorizaNoAutorizaCartografia =  Convert.ToInt32(cboEstatusCartografiaComparativa.Text.Substring(0, 2));
                foliosPorAutorizarCartografia();
            }
        }

        private void cbbUsuarioVentanilla_Enter(object sender, EventArgs e)
        {
            cajasamarillas(9);
        }

        private void btnCancelarVentanilla_Click(object sender, EventArgs e)
        {
            cbbUbicacionVentanilla.SelectedIndex = -1;
            cbbUsuarioVentanilla.SelectedIndex = -1;
            limpiarDataGridyLabelTotal();  
            habilitarBotones();
        }

        private void btnRefreshVentanillaRevision_Click(object sender, EventArgs e)
        {
            limpiarDataGridyLabelTotal();
            habilitarBotones();
            habilitarFechas();
        }
        //////////////////////////////////////////////////////////////////////////////////
        ////////////CLICK DE LOS BOTONES 
        //////////////////////////////////////////////////////////////////////////////////
        private void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////
        ///////CLICK EN EL BOTÓN DE NUEVO PARA HABILITAR FECHAS Y BOTONES DE LAS CONSULTAS 
        /////////////////////////////////////////////////////////////////////////////////////////////////
        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FAVOR DE INGRESAR FECHA DE INICIO Y FINAL PARA PODER REALIZAR LA BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
            cmdNuevo.Enabled = false;
            habilitarFechas();
            habilitarBotones();
            //habilitar para el color amarillo e indicar fecha 
            cboDia1.SelectedIndex = 0;
            cboDia1.Focus();
        }
        ////////////////////////////////////////////////////////////////////////////////////////
        ////////////// CONSULTA SISTEMAS
        ////////////////////////////////////////////////////////////////////////////////////////
        private void btnConsultaSistemas_Click(object sender, EventArgs e)
        {
            sistemasConsulta();
        //sistemas consulta
        //SP MALSIST
        }
        void sistemasConsulta()
        {
            limpiarDataGridyLabelTotal();

            validacionCajasFecha = 0;
            validaCajas();
            if (validacionCajasFecha == 1) { return; }
            ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
            validacionFechaFormato = 0;
            validaFecha();
            if (validacionFechaFormato == 1) { return; }
            /////////VALIDAR QUE TENGA ALGO SELECCIONADO EN EL COMBOBOX
            deshabilitarFechas();
            pnlGeneral.Visible = false;
            //////FECHAS PARA MANDAR AL PROCEDIMIENTO ALMACENADO 
            DateTime spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text + "-" + cboDia1.Text + "T00:00:00");
            DateTime spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text + "-" + cboDia2.Text + "T23:59:59");

            //////CONVERTIR A CADENA DE TEXTO LOS COMBOBOX PARA LAS FECHAS (BUSCAR ENTRE ESTOS RAANGOS ) 
            string fecha_iniL = cboAño1.Text + cboMes1.Text + cboDia1.Text + " 00:00:00";
            string fecha_finL = cboAño2.Text + cboMes2.Text + cboDia2.Text + " 23:59:59";
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "    SELECT cnc.MUNICIPIO, cnc.ZONA, cnc.MANZANA, cnc.LOTE,";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.EDIFICIO, cnc.DEPTO, cnc.DESCRIPCION,";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.FECHA, cnc.HORA, cnc.USUARIO, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.OBSERVACIONES ";
            con.cadena_sql_interno = con.cadena_sql_interno + "      FROM CAT_NEW_CARTOGRAFIA_2025 cnc, CAT_DONDE_VA_2025 cdv";
            con.cadena_sql_interno = con.cadena_sql_interno + "     WHERE cnc.SERIE = cdv.SERIE  ";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.FOLIO_ORIGEN = cdv.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cdv.CARTOGRAFIA = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cdv.VENTANILLA = 1";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.FECHA >= " + util.scm(fecha_iniL);
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnc.FECHA <= " + util.scm(fecha_finL);
            if (rbnAutorizadosSistemas.Checked == true)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND CDV.SISTEMAS = 1";
                tipoReporteJefatura = 2;
            }
            if (rbnNoAutorizadosSistemas.Checked == true)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND CDV.SISTEMAS = 0";
                tipoReporteJefatura = 1;
            }
            con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY cnc.folio_origen DESC      ";


            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                //error 
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

                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                                                       //habilitarFechas();
                                                       //habilitarExportarExcelPDF();
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString();
                deshabilitarBotones();
                deshabilitarFechas();
                habilitarExportar();
                tipoReporte = 4;
            }
        }
        //refresh
        private void btnRefreshSistemas_Click(object sender, EventArgs e)
        {
            habilitarFechas();
            habilitarBotones();
            limpiarDataGridyLabelTotal();
            cboDia1.Focus();
            //FALTO LIMPIAR Y COMPLILAR
            //EL SP ES PAR SIST
            //SP_MALSIST
        }

        private void btnCancelarSistemas_Click(object sender, EventArgs e)
        {
            rbnAutorizadosSistemas.Checked = false;
            rbnNoAutorizadosSistemas.Checked = false;
            limpiarDataGridyLabelTotal();
            habilitarBotones();
            habilitarFechas();
        }
        ///////////////////////////////////////////////////////////////////////////////////
        //////// CLICK PARA CONSULTAR EN CARTOGRAFÍA DIFERENTES PARAMETROS
        ///////////////////////////////////////////////////////////////////////////////////
        private void btnConsultaCartografia_Click(object sender, EventArgs e)
        {
            //////limpiamos los datos del datagridview y el conteo en 0 siempre. 

            ///////VALIDAR LOS COMBOBOX DE LAS FECHAS, QUE NINGUNO ESTÉ VACIO
            validacionCajasFecha = 0;
            validaCajas();
            if (validacionCajasFecha == 1) { return; }
            ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
            validacionFechaFormato = 0;
            validaFecha();
            if (validacionFechaFormato == 1) { return; }
            /////////VALIDAR QUE TENGA ALGO SELECCIONADO EN EL COMBOBOX
            if (cbbUbicacionCartografia.Text == "")
            {
                MessageBox.Show("NECESITAS SELECCIONAR UN CONCEPTO PARA BUSCAR EN CARTOGRAFÍA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Program.acceso_nivel_acceso == 3)
            {
                if (cbbUsuarioCartografia.Text == "")
                {
                    MessageBox.Show("NECESITAS SELECCIONAR UN USUARIO PARA REALIZAR LA BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            deshabilitarFechas();
            limpiarDataGridyLabelTotal();
            //////FECHAS PARA MANDAR AL PROCEDIMIENTO ALMACENADO 
            ///
            //DateTime spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text + "-" + cboDia1.Text + "T00:00:00");
            //DateTime spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text + "-" + cboDia2.Text + "T23:59:59");

            //////CONVERTIR A CADENA DE TEXO LOS COMBOBOX PARA LAS FECHAS 
            fecha_iniL = cboAño1.Text + cboMes1.Text + cboDia1.Text + " 00:00:00";
            fecha_finL = cboAño2.Text + cboMes2.Text + cboDia2.Text + " 23:59:59";

            /////SACAR LA CONSULTA QUE VA A LLENAR EL DATAGRIDVIEW 
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "    SELECT SERIE, FOLIO_ORIGEN 'FOLIO ÓRIGEN', ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           MUNICIPIO, ZONA, MANZANA, LOTE, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           EDIFICIO, DEPTO, DESCRIPCION, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           FECHA, HORA, USUARIO,";
            con.cadena_sql_interno = con.cadena_sql_interno + "           OBSERVACIONES, TERR_PROPIO,";
            con.cadena_sql_interno = con.cadena_sql_interno + "           TERR_COMUN, AÑO_CALCULO";
            con.cadena_sql_interno = con.cadena_sql_interno + "      FROM CAT_NEW_CARTOGRAFIA_2025";
            con.cadena_sql_interno = con.cadena_sql_interno + "     WHERE ESTADO      =   " + Program.PEstado;
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND MUNICIPIO   =   " + Program.municipioN;
            //concatenar la ubicacion para saber qué tipo de trámite es 
            if (cbbUbicacionCartografia.SelectedIndex > 0) //TIPODETRAMITE
            {
                //cortamos el combobox para solo sacar los números 
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND UBICACION =   " + util.scm(cbbUbicacionCartografia.Text.Substring(0, 2));
            }
            if (Program.acceso_nivel_acceso == 3)
            {
                if (cbbUsuarioCartografia.SelectedIndex > 0)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "   AND USUARIO = " + util.scm(cbbUsuarioCartografia.Text);

                }
                Usuario = cbbUsuarioCartografia.Text;
                banderaArea = 1;
                //duda del usuario / debo cambiar todo del procedimiento por si es general, o es específico      
            }
            else
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND USUARIO     = " + util.scm(Program.nombre_usuario);
                Usuario = Program.nombre_usuario;
            }
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND FECHA      >= '" + fecha_iniL + "'"; //UTIL.SCM
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND FECHA      <= '" + fecha_finL + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY FOLIO_ORIGEN ASC ";

            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///EN CASO QUE NO HAYA NADA EN LA LISTA 
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN REFERENTE A LA BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.cerrar_interno();

            }
            else //SI SE ENCUENTRA UN DATO 
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

                dgResultado.Columns[0].Width = 120; //serie 
                dgResultado.Columns[1].Width = 120; //folio
                dgResultado.Columns[2].Width = 120; //municipio

                dgResultado.Columns[8].Width = 250; //descripción
                dgResultado.Columns[10].Width = 250; //hora 
                dgResultado.Columns[11].Width = 250; //usuario 
                dgResultado.Columns[12].Width = 1200; //observaciones
                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 

                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString(); //quitar al resultado uno menos 
                                                                          //es una variable global declarada al principio; se le indica qué reporte va a imprimir (cartografía)
                habilitarExportar(); //habilitamos los botones de exportar 
                /////
                //////procedimiento almacenado de aquí para abajo (solo uno) (cambiar)
                ////

                //////SACAR CUÁNTAS ALTAS SE HICIERON 
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT COUNT (ubicacion)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE UBICACION = 1 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FECHA >= '" + fecha_iniL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FECHA <= '" + fecha_finL + "'";
                if (Program.acceso_nivel_acceso == 3)
                {
                    if (cbbUsuarioCartografia.SelectedIndex > 0)
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND USUARIO = " +util.scm(Usuario);
                    }
                }
                else
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND USUARIO = " + util.scm(Program.nombre_usuario);
                }
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        totalAlta = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///cerrar la conexión
                con.cerrar_interno();

                ////// SACAR CUÁNTOS CAMBIOS DE CLAVES HAY 
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT COUNT (ubicacion)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE UBICACION = 2 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FECHA >= '" + fecha_iniL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FECHA <= '" + fecha_finL + "'";
                if (Program.acceso_nivel_acceso == 3)
                {
                    if (cbbUsuarioCartografia.SelectedIndex > 0)
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND USUARIO = " + util.scm(Usuario);
                    }
                }
                else
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND USUARIO = " + util.scm(Program.nombre_usuario);
                }
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        totalCambios = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///cerrar la conexión
                con.cerrar_interno();

                ////// SACAR CUÁNTOS CERTIFICACIONES DE CLAVES HAY 
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT COUNT (ubicacion)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE UBICACION = 3 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FECHA >= '" + fecha_iniL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FECHA <= '" + fecha_finL + "'";
                if (Program.acceso_nivel_acceso == 3)
                {
                    if (cbbUsuarioCartografia.SelectedIndex > 0)
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND USUARIO = " + util.scm(Usuario);
                    }
                }
                else
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND USUARIO = " + util.scm(Program.nombre_usuario);
                }
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        totalCertificacion = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///cerrar la conexión
                con.cerrar_interno();


                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " SELECT COUNT (ubicacion)";
                con.cadena_sql_interno = con.cadena_sql_interno + "   FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE UBICACION = 5 ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FECHA >= '" + fecha_iniL + "'";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AND FECHA <= '" + fecha_finL + "'";
                if (Program.acceso_nivel_acceso == 3)
                {
                    if (cbbUsuarioCartografia.SelectedIndex > 0)
                    {
                        con.cadena_sql_interno = con.cadena_sql_interno + " AND USUARIO = " + util.scm(Usuario);
                    }
                }
                else
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "    AND USUARIO = " + util.scm(Program.nombre_usuario);
                }
                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();
                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        totalManifestacion = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                ///cerrar la conexión
                con.cerrar_interno();



                btnRefreshCartografia.Enabled = true;

                tipoReporte = 1;
                deshabilitarBotones();
                deshabilitarFechas();

                if (Program.acceso_nivel_acceso == 3)
                {
                    tipoReporte = 10;
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////
        //////////// CANCELAR LO DE CARTOGRAFÍA (EQUIOVOQUÉ DE BOTÓN)
        ////////////////////////////////////////////////////////////////////////////
        private void btnCancelarOrdenesPago_Click(object sender, EventArgs e)
        {
            cbbUbicacionCartografia.SelectedIndex = -1;
            cbbUsuarioCartografia.SelectedIndex = -1;
            habilitarBotones();
        }
        /////////////////////////////////////////////////////////////////////////////
        //////////// REFRESH A FECHAS Y DATAGRIDVIEW 
        ////////////////////////////////////////////////////////////////////////////
        private void btnRefreshCartografia_Click(object sender, EventArgs e)
        {
            habilitarFechas();
            habilitarBotones();
            limpiarDataGridyLabelTotal();
            cboDia1.Focus();
        }
        /////////////////////////////////////////////////////////////////////////////
        //////////// CLICK PARA LA CONSULTA DE VENTANILLA DIFERENTES PARAMETROS
        ////////////////////////////////////////////////////////////////////////////
        private void btnConsultaVentanilla_Click(object sender, EventArgs e)
        {

            ///////VALIDAR LOS COMBOBOX DE LAS FECHAS, QUE NINGUNO ESTÉ VACIO
            validacionCajasFecha = 0;
            validaCajas();
            if (validacionCajasFecha == 1) { return; }
            ///////VALIDAMOS LOS FORMATOS DE FECHA (NO ACEPTAR 29 DE FEBRERO, NI QUE SEA LA FECHA FINAL MAYOR A LA INICIAL)
            validacionFechaFormato = 0;
            validaFecha();
            if (validacionFechaFormato == 1) { return; }

            if (cbbUbicacionVentanilla.Text == "")
            {
                MessageBox.Show("NECESITAS AGREGAR UN ELEMENTO DE BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Program.acceso_nivel_acceso == 3)
            {
                if (cbbUsuarioVentanilla.Text == "")
                {
                    MessageBox.Show("NECESITAS SELECCIONAR UN USUARIO PARA REALIZAR LA BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            limpiarDataGridyLabelTotal();
            deshabilitarFechas();
            //////FECHAS PARA MANDAR AL PROCEDIMIENTO ALMACENADO 
            DateTime spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text + "-" + cboDia1.Text + "T00:00:00");
            DateTime spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text + "-" + cboDia2.Text + "T23:59:59");

            //////CONVERTIR A CADENA DE TEXO LOS COMBOBOX PARA LAS FECHAS 
            fecha_iniL = cboAño1.Text + cboMes1.Text + cboDia1.Text + " 00:00:00";
            fecha_finL = cboAño2.Text + cboMes2.Text + cboDia2.Text + " 23:59:59";

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT SERIE , FOLIO_ORIGEN ,MUNICIPIO, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "       ZONA , MANZANA , LOTE,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       EDIFICIO , DEPTO , DESCRIPCION , FECHA ";
            con.cadena_sql_interno = con.cadena_sql_interno + "       HORA , OBSERVACIONES ";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM CAT_NEW_VENTANILLA_2025";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE FECHA  >= " + util.scm(fecha_iniL);
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND FECHA  <= " + util.scm(fecha_finL);
            //con.cadena_sql_interno = con.cadena_sql_interno + "   AND USUARIO = " + util.scm(Program.nombreUsuarioVentanilla);
            //concatenar la ubicacion para saber qué tipo de trámite es 
            if (cbbUbicacionVentanilla.SelectedIndex > 0)
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "   AND UBICACION =   " + util.scm(cbbUbicacionVentanilla.Text.Substring(0, 2));
            }
            if (Program.acceso_nivel_acceso == 3) //esta cosa duda 
            {
                if (cbbUsuarioVentanilla.SelectedIndex > 0)
                {
                    con.cadena_sql_interno = con.cadena_sql_interno + "   AND USUARIO = " + util.scm(cbbUsuarioVentanilla.Text);
                }
                Usuario = cbbUsuarioVentanilla.Text;
            }
            else
            {
                con.cadena_sql_interno = con.cadena_sql_interno + "       AND USUARIO     = " + util.scm(Program.nombre_usuario);
                Usuario = Program.nombre_usuario;
            }

            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.cerrar_interno();
                return;
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
                //PARA LAS COLUMNAS ANCHO (CAMBIAR SI  SON MÁS O MENOS)
                dgResultado.Columns[0].Width = 80; //serie 
                dgResultado.Columns[1].Width = 120; //folio
                dgResultado.Columns[2].Width = 120; //mpio
                dgResultado.Columns[8].Width = 250; //descripcion
                dgResultado.Columns[10].Width = 250; //hora 

                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                                                       //habilitarFechas();
                                                       //habilitarExportarExcelPDF();
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString();
                habilitarExportar();
                deshabilitarBotones();
                deshabilitarFechas();
                btnRefresh.Enabled = true;

                banderaArea = 2; //para saber si es cartografía o ventanilla; es una variable para que realice una consulta en el procedimiento almacenado 
                tipoReporte = 2; //tipo de reporte que va a mostrar en el método de la impresión
                if (Program.acceso_nivel_acceso == 3) //para jefatura cambia el reporte 
                {
                    tipoReporte = 10;
                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            habilitarFechas();
            habilitarBotones();
            limpiarDataGridyLabelTotal();
            cboDia1.Focus();
        }
        private void btnConsultaRevision_Click(object sender, EventArgs e)
        {
            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "    SELECT cnc.folio_origen, cnc.MUNICIPIO, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.zona, cnc.MANZANA, cnc.lote, cnc.edificio, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.depto, cnc.descripcion 'Descripcion Cartografía', ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnc.fecha, cnc.hora, cnc.USUARIO,";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnv.descripcion 'Descripcion Ventanilla', cnv.FECHA, cnv.HORA, ";
            con.cadena_sql_interno = con.cadena_sql_interno + "           cnv.USUARIO, cdv.CARTOGRAFIA, cdv.VENTANILLA, cdv.REVISO, cdv.SISTEMAS";
            con.cadena_sql_interno = con.cadena_sql_interno + "      FROM CAT_NEW_CARTOGRAFIA_2025 cnc, CAT_NEW_VENTANILLA_2025 cnv, CAT_DONDE_VA_2025 cdv";
            con.cadena_sql_interno = con.cadena_sql_interno + "     WHERE cnc.FOLIO_ORIGEN = cnv.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cnv.FOLIO_ORIGEN = cdv.FOLIO_ORIGEN";
            con.cadena_sql_interno = con.cadena_sql_interno + "       AND cdv.ELIMINADO = 0";
            con.cadena_sql_interno = con.cadena_sql_interno + "";
            con.cadena_sql_interno = con.cadena_sql_interno + "";
            con.cadena_sql_interno = con.cadena_sql_interno + "";
            con.cadena_sql_interno = con.cadena_sql_interno + "";
            con.cadena_sql_interno = con.cadena_sql_interno + "";
            con.cadena_sql_interno = con.cadena_sql_interno + "";
            con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY cnc.folio_origen ASC";

            DataTable LLENAR_GRID_1 = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand(con.cadena_sql_interno, con.cnn_interno);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            ///
            if (da.Fill(LLENAR_GRID_1) == 0)//COMPROBAR SI LA BUSQUEDA OBTUVO UN DATO, en caso de ser igual a 0; marca error 
            {
                MessageBox.Show("NO SE ENCONTRÓ INFORMACIÓN REFERENTE A LA BÚSQUEDA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                con.cerrar_interno();
                //error 
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

                // Deshabilitar edición
                dgResultado.ReadOnly = true;
                // Estilos visuales
                dgResultado.DefaultCellStyle.SelectionBackColor = Color.Yellow; //AL SELECCIONAR UNA CELDA SE PONE DE COLOR AMARILLO 
                dgResultado.DefaultCellStyle.SelectionForeColor = Color.Black; //COLOR NEGRO 

                dgResultado.RowHeadersVisible = false; //QUITARLE LA PRIMER FILA BLANCA QUE SALE EN EL DATAGRIDVIEW 
                //habilitarFechas();
                habilitarExportar();
                lblConteo.Text = (dgResultado.Rows.Count - 1).ToString();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        ////refresh
        ////////////////////////////////////////////////////////////////////////
        private void btnRefreshRevision_Click(object sender, EventArgs e)
        {
            habilitarFechas();
            limpiarDataGridyLabelTotal();
            cboDia1.Focus();
            //pnlBusquedaRevision.Visible = true;
            //pnlRevision.Visible = false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////CLICK PARA EL BOTÓN DEL PDF (GENERACIÓN) SE CARGA EL TIPO DE REPORTE DESDE LA CONSULTA
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {

            //////VALIDAMOS QUÉ TIPO DE REPORTE ES, PARA ABRIR EL REPORTE 
            if (tipoReporte == 1) //REPORTE CARTOGRAFÍA  
            {
                //
                ubicacionCartografia = cbbUbicacionCartografia.Text.Substring(0, 2);
                string nombreReporte = "";

                if (cbbUbicacionCartografia.SelectedIndex == 0)
                {
                    nombreReporte = "REPORTE GENERAL DE CARTOGRAFÍA";
                }
                else if (cbbUbicacionCartografia.SelectedIndex == 1)
                {
                    nombreReporte = "REPORTE ALTAS DE CARTOGRAFÍA";
                }
                else if (cbbUbicacionCartografia.SelectedIndex == 2)
                {
                    nombreReporte = "REPORTE CAMBIOS DE CARTOGRAFÍA";
                }
                else if (cbbUbicacionCartografia.SelectedIndex == 3)
                {
                    nombreReporte = "REPORTE CERTIFICACION DE CARTOGRAFÍA";
                }
                else if (cbbUbicacionCartografia.SelectedIndex == 4)
                {
                    nombreReporte = "REPORTE MANIFESTACIONES DE CARTOGRAFÍA";
                }
                else
                {
                    nombreReporte = "ERROR EN REPORTE";
                }
                ///////////DATETIME PARA MANDAR A LOS PROCEDIMIENTOS ALMACENADOS 
                ///

                //string fecha_iniL = cboAño1.Text + cboMes1.Text + cboDia1.Text + " 00:00:00";
                //string fecha_finL = cboAño2.Text + cboMes2.Text + cboDia2.Text + " 23:59:59";

                spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + "T00:00:00");
                spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + "T23:59:59");
                //ABRIMOS EL REPORTE 
                formaReporte.mostrarReporteCartografia mostrarReporteV = new formaReporte.mostrarReporteCartografia();
                ////////// LOS PARAMETROS QUE VOY A MANDAR DEL FORMULARIO DE AQUÍ 
                mostrarReporteV.fechaInicio = spFECHA_INI; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.fechaFin = spFECHA_FIN; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.Usuario = Usuario; //DATETIME PARA LA FECHA INICIAL //DATETIME PARA LA FECHA FINAL 
                mostrarReporteV.Descripcion = ubicacionCartografia; //Ubicación cortada del combobox para el procedimiento almacenado
                //parametros que obtengo en una consulta para colocarlo en el reporte
                mostrarReporteV.totalAlta = totalAlta;
                mostrarReporteV.totalCambios = totalCambios;
                mostrarReporteV.totalCertificaciones = totalCertificacion;
                mostrarReporteV.nombreReporte = nombreReporte;
                //PARAMETROS QUE VOY A MANDAR DESDE AQUÍ PARA LA PANTALLA 

                MessageBox.Show("PROCEDE A IMPRIMIR EL REPORTE DE CARTOGRAFÍA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarReporteV.ShowDialog();
                //limpiarTodo();
                //cmdNuevo.Ena bled = true;
            }
            //////////////////////////////////////////////////////////////////////////////
            else if (tipoReporte == 2) //REPORTE VENTANILLA 
            {

                ubicacionVentanilla = cbbUbicacionVentanilla.Text.Substring(0, 2);
                string nombreReporte = "";

                if (cbbUbicacionVentanilla.SelectedIndex == 0)
                {
                    nombreReporte = "REPORTE GENERAL DE VENTANILLA";
                }
                else if (cbbUbicacionVentanilla.SelectedIndex == 1)
                {
                    nombreReporte = "REPORTE ALTAS DE VENTANILLA";
                }
                else if (cbbUbicacionVentanilla.SelectedIndex == 2)
                {
                    nombreReporte = "REPORTE CAMBIOS DE VENTANILLA";
                }
                else if (cbbUbicacionVentanilla.SelectedIndex == 3)
                {
                    nombreReporte = "REPORTE CERTIFICACION DE VENTANILLA";
                }
                else if (cbbUbicacionVentanilla.SelectedIndex == 4)
                {
                    nombreReporte = "REPORTE MANIFESTACIONES DE VENTANILLA";
                }
                else
                {
                    nombreReporte = "ERROR EN REPORTE";
                }

                // Corrected the initialization of DateTime variables
                spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + "T00:00:00");
                spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + "T23:59:59");

                
                //para sacar el usuario y que haga el conteo del usuario registrado 
                //si es para jefatura; es otro usuario
                if (Program.acceso_nivel_acceso == 3)
                {
                    if (cbbUsuarioVentanilla.SelectedIndex > 0)
                    {
                        Usuario = cbbUsuarioVentanilla.Text.ToString().Trim();
                    }
                    Usuario = cbbUsuarioVentanilla.Text.ToString().Trim();
                }
                else
                {
                    Usuario = Program.nombre_usuario; //es para el usuario que está logueado
                }
                // ABRIMOS EL PROCEDIMEINTO QUE NOS DEVOLVERA  EL CONTEO 
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("SP_SONGVENTANILLA_REPORTEA", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                //** PARAMETROS DE ENTRADA **//
                cmd.Parameters.Add("@FECHAINICIO", SqlDbType.DateTime, 25).Value = spFECHA_INI;
                cmd.Parameters.Add("@FECHAFIN", SqlDbType.DateTime, 25).Value = spFECHA_FIN;
                cmd.Parameters.Add("@USR", SqlDbType.VarChar, 100).Value = Usuario;

                //** PARAMETROS DE SALIDA **//
                cmd.Parameters.Add("@CONTEOALTAS", SqlDbType.Int, 5).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CONTEOCAMBIOS", SqlDbType.Int, 5).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CONTEOCER", SqlDbType.Int, 5).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@CONTEOMANI", SqlDbType.Int, 5).Direction = ParameterDirection.Output;

                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();
                con.cerrar_interno();

                int ALTAS = Convert.ToInt32(cmd.Parameters["@CONTEOALTAS"].Value);
                int CAMBIOS = Convert.ToInt32(cmd.Parameters["@CONTEOCAMBIOS"].Value);
                int CERTIS = Convert.ToInt32(cmd.Parameters["@CONTEOCER"].Value);
                int MANIS = Convert.ToInt32(cmd.Parameters["@CONTEOMANI"].Value);

                //** MOSTRAMOS EL REPORTE **//
                formaReporte.MostrarPagoCatastro PG = new formaReporte.MostrarPagoCatastro();
                PG.RALTA = ALTAS;
                PG.RCAMBIOS = CAMBIOS;
                PG.RCERTIFICADOS = CERTIS;
                PG.RMANIFIESTOS = MANIS;
                PG.FECHA1 = spFECHA_INI;
                PG.FECHA2 = spFECHA_FIN;
                PG.RUSER = Usuario;   ////Program.Usuario;
                PG.RUBICACION = ubicacionVentanilla;
                PG.nombreReporte = nombreReporte;
                PG.ShowDialog();
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            ///
            //////////////////////////////////////////////////////////////////////////////////////////////
            else if (tipoReporte == 3) //altas cat
            {
                string nombreReporte = "REPORTE DE ALTAS CATASTRALES";
                spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + "T00:00:00");
                spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + "T23:59:59");
                //ABRIMOS EL REPORTE 
                formaReporte.mostrarReporteRevision mostrarReporteV = new formaReporte.mostrarReporteRevision();
                ////////// LOS PARAMETROS QUE VOY A MANDAR DEL FORMULARIO DE AQUÍ 
                mostrarReporteV.fechaInicio = spFECHA_INI; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.fechaFin = spFECHA_FIN; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.Usuario = Program.nombre_usuario; //DATETIME PARA LA FECHA INICIAL //DATETIME PARA LA FECHA FINAL 
                //parametros que obtengo en una consulta para colocarlo en el reporte
                mostrarReporteV.totalAltasRevision = totalAltasRevision;
                //PARAMETROS QUE VOY A MANDAR DESDE AQUÍ PARA LA PANTALLA 
                mostrarReporteV.nombreReporte = nombreReporte;

                MessageBox.Show("PROCEDE A IMPRIMIR EL REPORTE DE REVISIÓN/ALTAS", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarReporteV.ShowDialog();
            }
            //////////////////////////////////////////////////////////////////////////////////////////////
            /// SISTEMAS
            //////////////////////////////////////////////////////////////////////////////////////////////
            else if (tipoReporte == 4)
            {
                string nombreReporte = "";
                if (tipoAutorizado == 0)
                {
                    nombreReporte = "REPORTE DE SISTEMAS NO AUTORIZADOS";
                }
                else
                {
                    nombreReporte = "REPORTE DE SISTEMAS AUTORIZADOS";
                }

                spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + "T00:00:00");
                spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + "T23:59:59");

                formaReporte.mostrarReporteSistemas mostrarReporteV = new formaReporte.mostrarReporteSistemas();
                mostrarReporteV.fechaInicio = spFECHA_INI; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.fechaFin = spFECHA_FIN; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.Usuario = Program.nombre_usuario; //DATETIME PARA LA FECHA INICIAL //DATETIME PARA LA FECHA FINAL
                mostrarReporteV.nombreReporte = nombreReporte;
                mostrarReporteV.tipoAutorizado = tipoReporteJefatura;

                //mostrarReporteV.totalAlta = totalAltasSis;
                //mostrarReporteV.totalCambios = totalCambiosSis;
                //mostrarReporteV.totalCertificaciones = totalCertiSis;


                MessageBox.Show("PROCEDE A IMPRIMIR EL REPORTE DE SISTEMAS", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarReporteV.ShowDialog();
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //PARA LOS REPORTES DE JEFATURA, ES DEL NIVEL 10 PARA ADELANTE 
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (tipoReporte == 10) //este es el número para el tipo de  reporte de jefatura 
            { //(igual al de cart/vent; pero para jefatura donde se le agrega usuario)
                //que reporte muestra 
                tipoReporteJefatura = 0; //variable global para el tipo de reporte; solo jefatura
                UbicacionJefatura = 0;

                ////////////////////////////////////////
                if (banderaArea == 1) //cartografía 
                {
                    UbicacionJefatura = 0;
                    UbicacionJefatura = Convert.ToInt32(cbbUbicacionCartografia.Text.ToString().Trim().Substring(0, 2));
                    if (cbbUbicacionCartografia.SelectedIndex == 0)
                    {
                        if (cbbUsuarioCartografia.SelectedIndex == 0)
                        //cambiar la variable 
                        {//tr es el tipo de consulta que hará el procedimiento para llenar la tabla del procedimiento
                            tipoReporteJefatura = 0; //en este caso es 0; por lo que es un reporte general
                        }
                    }
                    if (cbbUbicacionCartografia.SelectedIndex > 0)
                    {
                        if (cbbUsuarioCartografia.SelectedIndex > 0)
                        {
                            tipoReporteJefatura = 3; //es el tipo de reporte, en este caso es para un reporte con usuario y tipo
                        }
                        else //ubicacion, y usuario general 
                        {
                            tipoReporteJefatura = 2;
                        }
                    }
                    else
                    {//ubicacion general, y usuario (funcionó bien)
                        if (cbbUsuarioCartografia.SelectedIndex > 0)
                        {
                            tipoReporteJefatura = 1; //usuario sin ubicacion 
                        }
                    }
                }
                ////////////////////////////////
                else if (banderaArea == 2) //ventanilla 
                {
                    UbicacionJefatura = 0;
                    UbicacionJefatura = Convert.ToInt32(cbbUbicacionVentanilla.Text.ToString().Trim().Substring(0, 2));

                    //UbicacionJefatura = Convert.ToInt32(cbbUbicacionVentanilla.Text.ToString().Trim().Substring(0,2));
                    if (cbbUbicacionVentanilla.SelectedIndex == 0)
                    {
                        if (cbbUbicacionVentanilla.SelectedIndex == 0)
                        //cambiar la variable 
                        {//tr es el tipo de consulta que hará el procedimiento para llenar la tabla del procedimiento
                            tipoReporteJefatura = 10; //en este caso es 0; por lo que es un reporte general
                        }
                    }
                    if (cbbUbicacionVentanilla.SelectedIndex > 0)
                    {
                        if (cbbUbicacionVentanilla.SelectedIndex > 0)
                        {
                            tipoReporteJefatura = 13; //es el tipo de reporte, en este caso es para un reporte con usuario y tipo
                        }
                        else //ubicacion, y usuario general 
                        {
                            tipoReporteJefatura = 12;
                        }
                    }
                    else
                    {//ubicacion general, y usuario (funcionó bien)
                        if (cbbUbicacionVentanilla.SelectedIndex > 0)
                        {
                            tipoReporteJefatura = 11; //usuario sin ubicacion 
                        }
                    }
                }
                else
                {
                    MessageBox.Show("SE GENERÓ UN ERROR, COMUNICATE CON SONGUISYTEMS", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

              

                spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + " 00:00:00");
                spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + " 23:59:59");

                //antes de mandar; cambiar los tipos reporte 
                formaReporte.mostrarReporteJefatura mostrarReporteV = new formaReporte.mostrarReporteJefatura();
                ///////////////
                mostrarReporteV.fechaInicio = spFECHA_INI; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.fechaFin = spFECHA_FIN; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.tipoReporte = tipoReporteJefatura;
                mostrarReporteV.Usuario = Usuario; //DATETIME PARA LA FECHA INICIAL //DATETIME PARA LA FECHA FINAL
                mostrarReporteV.UsuarioImpresion = Program.nombre_usuario.ToString();


                mostrarReporteV.totalAlta = totalAlta;
                mostrarReporteV.totalCambios = totalCambios;
                mostrarReporteV.totalCertificaciones = totalCertificacion;



                //ubicacion debo modificar

                mostrarReporteV.Ubicacion = UbicacionJefatura;
                //mostrarReporteV.Ubicacion = cbbUbicacionCartografia.Text.ToString().Substring(0,2);
                //solo falta mandar el tipo, no perder tiempo 



                MessageBox.Show("PROCEDE A IMPRIMIR EL REPORTE DE JEFATURA", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarReporteV.ShowDialog();

            }
            else if (tipoReporte == 11)
            {








                ///COMPARATIVA 


                spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + "T00:00:00");
                spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + "T23:59:59");


                //ABRIMOS EL REPORTE 
                formaReporte.mostrarComparativaJefatura mostrarReporteV = new formaReporte.mostrarComparativaJefatura();
                ////////// LOS PARAMETROS QUE VOY A MANDAR DEL FORMULARIO DE AQUÍ 
                    mostrarReporteV.fechaInicio = spFECHA_INI; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.fechaFin = spFECHA_FIN; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES

                mostrarReporteV.tipoReporte = banderaArea; //DATETIME PARA LA FECHA INICIAL //DATETIME PARA LA FECHA FINAL 
                mostrarReporteV.Usuario = Program.nombre_usuario;
                //parametros que obtengo en una consulta para colocarlo en el reporte
                //mostrarReporteV.totalAltasRevision = totalAltasRevision;
                //PARAMETROS QUE VOY A MANDAR DESDE AQUÍ PARA LA PANTALLA 
                //mostrarReporteV.nombreReporte = nombreReporte;
                
                MessageBox.Show("PROCEDE A IMPRIMIR EL REPORTE DE REVISIÓN/ALTAS", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarReporteV.ShowDialog();
            }
            else if (tipoReporte == 12)
            {
               
                spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + "T00:00:00");
                spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + "T23:59:59");
                //ABRIMOS EL REPORTE 
                formaReporte.mostrarComparativaJefatura mostrarReporteV = new formaReporte.mostrarComparativaJefatura();
                ////////// LOS PARAMETROS QUE VOY A MANDAR DEL FORMULARIO DE AQUÍ 
                mostrarReporteV.fechaInicio = spFECHA_INI; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.fechaFin = spFECHA_FIN; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES

                mostrarReporteV.tipoReporte = banderaArea; //DATETIME PARA LA FECHA INICIAL //DATETIME PARA LA FECHA FINAL 
                mostrarReporteV.Usuario = Program.nombre_usuario;
                //parametros que obtengo en una consulta para colocarlo en el reporte
                //mostrarReporteV.totalAltasRevision = totalAltasRevision;
                //PARAMETROS QUE VOY A MANDAR DESDE AQUÍ PARA LA PANTALLA 
                //mostrarReporteV.nombreReporte = nombreReporte;

                MessageBox.Show("PROCEDE A IMPRIMIR EL REPORTE DE REVISIÓN/ALTAS", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarReporteV.ShowDialog();
            }
            else if (tipoReporte == 13) //tipo de reporte para la comparativa entre areas y usuarios 
            {
                //string nombreReporte = "REPORTE DE ALTAS CATASTRALES";
                spFECHA_INI = DateTime.Parse(cboAño1.Text + "-" + cboMes1.Text.Substring(0, 2) + "-" + cboDia1.Text + "T00:00:00");
                spFECHA_FIN = DateTime.Parse(cboAño2.Text + "-" + cboMes2.Text.Substring(0, 2) + "-" + cboDia2.Text + "T23:59:59");
                //ABRIMOS EL REPORTE 
                formaReporte.mostraReporteFolios mostrarReporteV = new formaReporte.mostraReporteFolios();
                ////////// LOS PARAMETROS QUE VOY A MANDAR DEL FORMULARIO DE AQUÍ 
                mostrarReporteV.fechaInicio = spFECHA_INI; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES
                mostrarReporteV.fechaFin = spFECHA_FIN; //SACAMOS EL ÁREA EMISORA DEL COMBOBOX, CON SOLO LOS DOS PRIMEROS CARACTERES

                //mostrarReporteV.tipoReporte = tipoReporteJefatura; //DATETIME PARA LA FECHA INICIAL //DATETIME PARA LA FECHA FINAL 
                mostrarReporteV.Usuario = Program.nombre_usuario;
                //parametros que obtengo en una consulta para colocarlo en el reporte
                //mostrarReporteV.totalAltasRevision = totalAltasRevision;
                //PARAMETROS QUE VOY A MANDAR DESDE AQUÍ PARA LA PANTALLA 
                //mostrarReporteV.nombreReporte = nombreReporte;

                MessageBox.Show("PROCEDE A IMPRIMIR EL REPORTE DE REVISIÓN/ALTAS", "INFORMACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mostrarReporteV.ShowDialog();
            }
            else
            {
                MessageBox.Show("SE GENERÓ UN ERROR AL REALIZAR LA IMPRESIÓN, COMUNÍCATE CON SONGUI SYSTEMS.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}