using AccesoBase;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USLibV4;
using Utilerias;

namespace SMACatastro.formaReporte
{
    public partial class mostrarReporteCartografia : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        ///////////////////////////////////////////////////////////////////
        //// PARAMETROS QUE SE VAN A RECIBIR DESDE LA OTRA PANTALLA
        ///////////////////////////////////////////////////////////////////
        public DateTime fechaInicio { get; set; }

        public DateTime fechaFin { get; set; }

        public string Usuario { get; set; }
        public string Descripcion { get; set; }
        public string nombreReporte { get; set; }
        public int totalAlta { get; set; }

        public int totalCambios { get; set; }
        
        public int totalCertificaciones { get; set; }

        public mostrarReporteCartografia()
        {
            InitializeComponent();
        }
        //////////////////////////////////////////////////////////////////////////////////////////
        ////////OBTENCION DE DATOS DESDE UN PROCEDIMIENTO ALMACENADO PERO CON UN DATATABLE 
        /////////////////////////////////////////////////////////////////////////////////////////
        private DataTable ObtenerDatosOrdenSP(DateTime fechaInicio, DateTime fechaFin, string Usuario, string Descripcion)
        {
            //
            DataTable dt = new DataTable();

            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand("SONGSP_REPORTE_CARTOGRAFÍA_2025", con.cnn_interno); //NOMBRE DEL PROCEDIMIENTO ALMACENADO 
            cmd.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros al procedimiento almacenado
            cmd.Parameters.Add("@FECHAINICIO", SqlDbType.DateTime, 20).Value = fechaInicio; // parametros que se envian 
            cmd.Parameters.Add("@FECHAFINAL", SqlDbType.DateTime, 20).Value = fechaFin; //parametros que se envian 
            cmd.Parameters.Add("@USUARIO", SqlDbType.Char, 50).Value = Usuario; //usuario que genera el proceso
            cmd.Parameters.Add("@UBICACION", SqlDbType.Char, 10).Value = Descripcion;
            cmd.Connection = con.cnn_interno;
            dt.Load(cmd.ExecuteReader());

            //CERRAR LA CONEXIÓN
            con.cerrar_interno();
            return dt;
        }
        private void mostrarReporteCartografia_Load(object sender, EventArgs e)
        {
            ConfigurationManager.RefreshSection("connectionStrings");
            this.reportViewer1.LocalReport.EnableExternalImages = true; //Habilitar que el procedimiento almacenado acepte imagenes externas 
            reportViewer1.LocalReport.DisplayName = "REPORTE DE CARTOGRAFÍA";
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            //OBTENER DATOS DE LA BASE DE DATOS
            DataTable dtDetalle = ObtenerDatosOrdenSP(fechaInicio, fechaFin, Usuario, Descripcion); //datos para el procedimiento almacenado 
            reportViewer1.LocalReport.ReportEmbeddedResource = "SMACatastro.formaReporte.ReporteCartografia.rdlc"; //indicar la ruta y que se le va a poner     
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtDetalle));

            //PARAMETROS a mandar al reporte 
            ReportParameter[] parametros = new ReportParameter[]
            {
               new ReportParameter("Parametro1", Usuario.ToString()), //0
               new ReportParameter("Parametro1", totalAlta.ToString()), //1
               new ReportParameter("Parametro1", totalCambios.ToString()), //2
               new ReportParameter("Parametro1", totalCertificaciones.ToString()), //3
               new ReportParameter("Parametro1", fechaInicio.ToString()), //4
               new ReportParameter("Parametro1", fechaFin.ToString()), //5
               new ReportParameter("Parametro1", nombreReporte.ToString()) //6
            };
            
            reportViewer1.LocalReport.SetParameters(parametros);
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
    }
}
