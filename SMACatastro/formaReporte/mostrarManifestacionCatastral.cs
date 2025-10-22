using AccesoBase;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMACatastro.formaReporte
{
    public partial class mostrarManifestacionCatastral : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        public int ZONA { get; set; }
        public int MANZANA { get; set; }
        public int LOTE { get; set; }
        public string EDIFICIO { get; set; }
        public string DEPTO { get; set; }


        public mostrarManifestacionCatastral()
        {
            InitializeComponent();
        }

        private void mostrarManifestacionCatastral_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();



            this.reportViewer1.RefreshReport();
            ConfigurationManager.RefreshSection("connectionStrings");
            this.reportViewer1.LocalReport.EnableExternalImages = true; //Habilitar que el procedimiento almacenado acepte imagenes externas 
            reportViewer1.LocalReport.DisplayName = "MANI";
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            //OBTENER DATOS DE LA BASE DE DATOS
            DataTable dtDetalle = ObtenerDatosOrdenSP(ZONA, MANZANA, LOTE, EDIFICIO, DEPTO); //datos para el procedimiento almacenado 
            reportViewer1.LocalReport.ReportEmbeddedResource = "SMACatastro.formaReporte.rptMan3en1.rdlc"; //indicar la ruta y que se le va a poner     
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtDetalle));

            //PARAMETROS a mandar al reporte 

            //reportViewer1.LocalReport.SetParameters(parametros);
            ///reportViewer1.LocalReport.SetParameters(Parametro1);
            this.reportViewer1.RefreshReport();
            /*
            Microsoft.Reporting.WinForms.Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;
            string deviceInfo = @"<DeviceInfo>
                      <OutputFormat>EMF</OutputFormat>
                      <PageWidth>8.5in</PageWidth>
                      <PageHeight>11in</PageHeight>
                      <MarginTop>0.25in</MarginTop>
                      <MarginLeft>0.25in</MarginLeft>
                      <MarginRight>0.25in</MarginRight>
                      <MarginBottom>0.25in</MarginBottom>
                    </DeviceInfo>";

            byte[] bytes = reportViewer1.LocalReport.Render("PDF", deviceInfo, out _, out encoding, out extension, out streamIds, out _);

            //Validamos que la ruta de la carpeta exista, en caso que no; se genera la carpeta 
            string rutaCarpeta = @"C:SONGUI\SMA_ORDENES"; // Cambia por tu ruta
            try
            {
                // Verificar si la carpeta existe
                if (!Directory.Exists(rutaCarpeta))
                {
                    // Crear la carpeta si no existe
                    Directory.CreateDirectory(rutaCarpeta);
                    Console.WriteLine($"Carpeta creada en: {rutaCarpeta}");
                }
                else
                {
                    Console.WriteLine("La carpeta ya existe.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            */


            // FileStream fs = new FileStream(@"C:\SONGUI\SMA_ORDENES\REPORTE_CARTOGRAFÍA"+@".pdf", FileMode.Create);

            //fs.Write(bytes, 0, bytes.Length);
            // fs.Close();
            mostrarReporteCartografia mf = new mostrarReporteCartografia();
            mf.Close();
            this.reportViewer1.RefreshReport();

        }

        private DataTable ObtenerDatosOrdenSP(int ZONA, int MANZANA, int LOTE, string EDIFICIO, string DEPTO)
        {
            //
            DataTable dt = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand("Song_Sp_Consulta_Datos_Para_Manifestacion_2025 ", con.cnn_interno); //NOMBRE DEL PROCEDIMIENTO ALMACENADO 
            cmd.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros al procedimiento almacenado
            cmd.Parameters.Add("@ESTADO", SqlDbType.Int, 18).Value = Program.PEstado; // parametros que se envian 
            cmd.Parameters.Add("@MUNICIPIO", SqlDbType.Int, 18).Value = Program.municipioN; // parametros que se envian 
            cmd.Parameters.Add("@ZONA", SqlDbType.Int, 18).Value = ZONA; // parametros que se envian 
            cmd.Parameters.Add("@MANZANA", SqlDbType.Int, 18).Value = MANZANA; // parametros que se envian 
            cmd.Parameters.Add("@LOTE", SqlDbType.Int, 18).Value = LOTE; // parametros que se envian 
            cmd.Parameters.Add("@EDIFICIO", SqlDbType.Char, 2).Value = EDIFICIO; //parametros que se envian 
            cmd.Parameters.Add("@DEPTO", SqlDbType.Char, 4).Value = DEPTO; //parametros que se envian 
            //cmd.Parameters.Add("@TIPOREPORTE", SqlDbType.Int, 3).Value = tipoReporte; //usuario que genera el proceso

            cmd.Connection = con.cnn_interno;
            dt.Load(cmd.ExecuteReader());

            //CERRAR LA CONEXIÓN
            con.cerrar_interno();
            return dt;
        }
    }
}
