using AccesoBase;
using Microsoft.Office.Interop.Excel;
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
using Telerik.SvgIcons;
using DataTable = System.Data.DataTable;

namespace SMACatastro.formaReporte
{
    public partial class MostrarPagoCatastro : System.Windows.Forms.Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        public DateTime FECHA1 { get; set; }

        public DateTime FECHA2 { get; set; }

        public int RALTA { get; set; }
        public int RCAMBIOS { get; set; }
        public int RCERTIFICADOS { get; set; }
        public int RMANIFIESTOS { get; set; }
        public string RUSER { get; set; }
        public string RUBICACION { get; set; }
        public string nombreReporte { get; set; }

        public MostrarPagoCatastro()
        {
            InitializeComponent();
        }

        private void MostrarPagoCatastro_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
        private DataTable ObtenerDatosOrdenSP(DateTime fechaInicio, DateTime fechaFinal, String USER, String Ubicacion)
        {
            //
            DataTable dt = new DataTable();

            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand("SP_SONGVENTANILLA_REPORTE", con.cnn_interno); //NOMBRE DEL PROCEDIMIENTO ALMACENADO 
            cmd.CommandType = CommandType.StoredProcedure;
            // Agregar parámetros al procedimiento almacenado
            cmd.Parameters.Add("@FECHAINICIO", SqlDbType.DateTime, 20).Value = fechaInicio; // parametros que se envian 
            cmd.Parameters.Add("@FECHAFIN", SqlDbType.DateTime, 20).Value = fechaFinal; //parametros que se envian 
            cmd.Parameters.Add("@USR", SqlDbType.VarChar, 100).Value = USER;
            cmd.Parameters.Add("@UBICACION", SqlDbType.Char, 10).Value = RUBICACION;
            cmd.Connection = con.cnn_interno;
            dt.Load(cmd.ExecuteReader());

            //CERRAR LA CONEXIÓN
            con.cerrar_interno();
            return dt;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

            ConfigurationManager.RefreshSection("connectionStrings");
            this.reportViewer1.LocalReport.EnableExternalImages = true; //Habilitar que el procedimiento almacenado acepte imagenes externas 
            reportViewer1.LocalReport.DisplayName = "PAGOS CATASTRALES";
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            //OBTENER DATOS DE LA BASE DE DATOS
            DataTable dtDetalle = ObtenerDatosOrdenSP(FECHA1, FECHA2, RUSER, RUBICACION); //datos para el procedimiento almacenado


            // OBTENER DATOS DE LA BASE DE DATOS
            DateTime FHoy = DateTime.Now;
            reportViewer1.LocalReport.ReportEmbeddedResource = "SMACatastro.formaReporte.rptVenta.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtDetalle));
            // reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtDetalle));
            ReportParameter[] parametros = new ReportParameter[]
            {
                new ReportParameter("Parameter1", FHoy.ToString()), //0 FECHA INICIAL
                new ReportParameter("Parameter1", RALTA.ToString()), //1 ALTA
                new ReportParameter("Parameter1", RCAMBIOS.ToString()), //2 CAMBIOS
                new ReportParameter("Parameter1", RCERTIFICADOS.ToString()), //3 CERTIFIADO
                new ReportParameter("Parameter1", RMANIFIESTOS.ToString()), //4 MANIFIESTOS
                new ReportParameter("Parameter1", RUSER.ToString()), //5 USUARIO
                new ReportParameter("Parameter1", nombreReporte.ToString()), //6 USUARIO         
               new ReportParameter("Parameter1", FECHA1.ToString()), //7
               new ReportParameter("Parameter1", FECHA2.ToString()), //8

            };
            //indicar que es parametros y se los vamos a pasar 
            reportViewer1.LocalReport.SetParameters(parametros);

            this.reportViewer1.RefreshReport();


            MostrarPagoCatastro mf1 = new MostrarPagoCatastro();
            mf1.Close();
            this.reportViewer1.RefreshReport();
        }
    }
}
