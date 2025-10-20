using AccesoBase;
using Microsoft.Reporting.WinForms;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace SMACatastro.formaReporte
{
    public partial class frmCertificado3en1 : Form
    {
        public string FOLIO_CERTIFICADO { get; set; }
        public int FOLIO_3EN1 { get; set; }
        public string año_act { get; set; }
        public string nombre_contri { get; set; }
        public string calle { get; set; }
        public string manzana { get; set; }
        public string lote { get; set; }
        public string num_ext { get; set; }
        public string num_int { get; set; }
        public string colonia { get; set; }
        public string domicilio { get; set; }
        public string CP { get; set; }
        public string clave_catastral { get; set; }
        public string fecha_factura { get; set; }
        public string tp { get; set; }
        public string tc { get; set; }
        public string cp1 { get; set; }
        public string cc { get; set; }
        public string vtp { get; set; }
        public string vtc { get; set; }
        public string vcp { get; set; }
        public string vttp { get; set; }
        public string vttc { get; set; }
        public string vc { get; set; }
        public string vcc { get; set; }
        public string folio_orden { get; set; }
        public string serie_orden { get; set; }


        public frmCertificado3en1()
        {
            InitializeComponent();
        }
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        private DataTable ObtenerDatosFacturaSP(int FOLIO_CERTI)
        {
            DataTable dt = new DataTable();
            con.conectar_base_interno();
            con.open_c_interno();
            SqlCommand cmd = new SqlCommand("TRES_EN_UNO_IMPRESION", con.cnn_interno);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FOLIO_C", SqlDbType.Int, 2).Value = FOLIO_CERTI;
            cmd.Connection = con.cnn_interno;
            dt.Load(cmd.ExecuteReader());
            con.cerrar_interno();
            return dt;
        }

        private void frmCertificado3en1_Load(object sender, EventArgs e)
        {
            ConfigurationManager.RefreshSection("connectionStrings");
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            // LocalReport reporte = new LocalReport();
            // OBTENER DATOS DE LA BASE DE DATOS
            DataTable dtDetalle = ObtenerDatosFacturaSP(FOLIO_3EN1);

            reportViewer1.LocalReport.ReportEmbeddedResource = "SMACatastro.formaReporte.rptCertificado3en1.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtDetalle));
            this.reportViewer1.RefreshReport();

            // Crear un parámetro para el informe
            ReportParameter[] parametros = new ReportParameter[]
            {
                new ReportParameter("Parametro1", calle.ToString()),//0
                new ReportParameter("Parametro1", manzana.ToString()),//1
                new ReportParameter("Parametro1", lote.ToString()),//2
                new ReportParameter("Parametro1", num_ext.ToString()),//3
                new ReportParameter("Parametro1", num_int.ToString()),//4
                new ReportParameter("Parametro1", colonia.ToString()),//5
                new ReportParameter("Parametro1", domicilio.ToString()),//6
                new ReportParameter("Parametro1", CP.ToString()),//7
                new ReportParameter("Parametro1", clave_catastral.ToString()),//8
                new ReportParameter("Parametro1", nombre_contri.ToString()),//9
                new ReportParameter("Parametro1", año_act.ToString()),//10
                new ReportParameter("Parametro1", fecha_factura.ToString()),//11
                new ReportParameter("Parametro1", tp.ToString()),//12
                new ReportParameter("Parametro1", tc.ToString()),//13
                new ReportParameter("Parametro1", cp1.ToString()),//14
                new ReportParameter("Parametro1", cc.ToString()),//15
                new ReportParameter("Parametro1", vtp.ToString()),//16
                new ReportParameter("Parametro1", vtc.ToString()),//17
                new ReportParameter("Parametro1", vcp.ToString()),//18
                new ReportParameter("Parametro1", vcc.ToString()),//19
                new ReportParameter("Parametro1", vttp.ToString()),//20
                new ReportParameter("Parametro1", vttc.ToString()),//21
                new ReportParameter("Parametro1", vc.ToString()),//22
                new ReportParameter("Parametro1", FOLIO_CERTIFICADO.ToString()),//23 
                new ReportParameter("Parametro1", serie_orden.ToString()),//24 
                new ReportParameter("Parametro1", folio_orden.ToString())//25


             };
            // Asignar el parámetro al informe
            reportViewer1.LocalReport.SetParameters(parametros);


            // reportViewer1.PrinterSettings.DefaultPageSettings.Landscape = true;
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);  // Vista de impresión

            this.reportViewer1.RefreshReport();
        }
    }
}
