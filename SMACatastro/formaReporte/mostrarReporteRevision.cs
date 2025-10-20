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
    public partial class mostrarReporteRevision : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        ///////////////////////////////////////////////////////////////////
        //// PARAMETROS QUE SE VAN A RECIBIR DESDE LA OTRA PANTALLA
        ///////////////////////////////////////////////////////////////////
        public DateTime fechaInicio { get; set; }

        public DateTime fechaFin { get; set; }
        public string Usuario { get; set; }
        public string nombreReporte { get; set; }
        public int totalAltasRevision { get; set; }
        public mostrarReporteRevision()
        {
            InitializeComponent();
        }
        private DataTable ObtenerDatosOrdenSP(DateTime fechaInicio, DateTime fechaFin)
        {
            //
            DataTable dt = new DataTable();

            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand("SONGSP_REPORTE_REVISION_2025", con.cnn_interno); //NOMBRE DEL PROCEDIMIENTO ALMACENADO 
            cmd.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros al procedimiento almacenado
            cmd.Parameters.Add("@FECHAINICIO", SqlDbType.DateTime, 20).Value = fechaInicio; // parametros que se envian 
            cmd.Parameters.Add("@FECHAFINAL", SqlDbType.DateTime, 20).Value = fechaFin; //parametros que se envian 
            cmd.Connection = con.cnn_interno;
            dt.Load(cmd.ExecuteReader());

            //CERRAR LA CONEXIÓN
            con.cerrar_interno();
            return dt;
        }

        private void mostrarReporteRevision_Load(object sender, EventArgs e)
        {
            ConfigurationManager.RefreshSection("connectionStrings");
            this.reportViewer1.LocalReport.EnableExternalImages = true; //Habilitar que el procedimiento almacenado acepte imagenes externas 
            reportViewer1.LocalReport.DisplayName = "REPORTE DE REVISIÓN/ALTAS";
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            //OBTENER DATOS DE LA BASE DE DATOS
            DataTable dtDetalle = ObtenerDatosOrdenSP(fechaInicio, fechaFin); //datos para el procedimiento almacenado 
            reportViewer1.LocalReport.ReportEmbeddedResource = "SMACatastro.formaReporte.rptRevision.rdlc"; //indicar la ruta y que se le va a poner     
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtDetalle));
            //PARAMETROS a mandar al reporte 
            ReportParameter[] parametros = new ReportParameter[]
            {
               new ReportParameter("Parametro1", Usuario.ToString()), //0
               new ReportParameter("Parametro1", fechaInicio.ToString()), //1
               new ReportParameter("Parametro1", fechaFin.ToString()), //2
               new ReportParameter("Parametro1", nombreReporte.ToString()), //3
               new ReportParameter("Parametro1", totalAltasRevision.ToString())
            };

            reportViewer1.LocalReport.SetParameters(parametros);

            this.reportViewer1.RefreshReport();
            
            mostrarReporteCartografia mf = new mostrarReporteCartografia();
            mf.Close();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
