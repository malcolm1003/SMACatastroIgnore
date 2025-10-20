using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace SMACatastro.formaReporte
{
    public partial class frmRCertificaciones : Form
    {

        public string FOLIO2 { get; set; }
        public string FOLIO_CER { get; set; }
        public string CLAVE_CAT { get; set; }
        public string CLAVE_ANT { get; set; }
        public string NOMBRE { get; set; }
        public string DIRECCION { get; set; }
        public string DIRECCION_2 { get; set; }
        public string SUP_TERRENO_PRIV { get; set; }
        public string SUP_TERRENO_COM { get; set; }
        public string SUP_CONST_PRIV { get; set; }
        public string SUP_CONST_COM { get; set; }
        public string VAL_TERRENO { get; set; }
        public string VAL_CONSTRUCCION { get; set; }
        public string VAL_CATASTRAL { get; set; }
        public string CERTIFICACION_01 { get; set; }
        public string CERTIFICACION_02 { get; set; }
        public string CERTIFICACION_03 { get; set; }
        public string FECHA_DIA { get; set; }
        public string FECHA_MES { get; set; }
        public string FECHA_A { get; set; }

        public string PERSONA_FIRMA { get; set; }
        public frmRCertificaciones()
        {
            InitializeComponent();
        }

        private void frmCertificaciones_Load(object sender, EventArgs e)
        {

            // Configurar el ReportViewer
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
            if (Program.tipoReporte == 5) // Verifica si es el tipo de reporte 5
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "SMACatastro.formaReporte.rptCertificadosCveValor.rdlc";
            }
            else
            {
                reportViewer1.LocalReport.ReportEmbeddedResource = "SMACatastro.formaReporte.rptCertificados.rdlc";
            }
            reportViewer1.RefreshReport();
            // Obtener el valor del TextBox


            // Crear un parámetro para el informe
            ReportParameter[] parametros = new ReportParameter[]
            {
                new ReportParameter("Parametro1", FOLIO_CER.ToString()),
                new ReportParameter("Parametro1", CLAVE_CAT.ToString()),
                new ReportParameter("Parametro1", CLAVE_ANT.ToString()),
                new ReportParameter("Parametro1", FOLIO2.ToString()),
                new ReportParameter("Parametro1", NOMBRE.ToString()),
                new ReportParameter("Parametro1", DIRECCION.ToString()),
                new ReportParameter("Parametro1", DIRECCION_2.ToString()),
                new ReportParameter("Parametro1", SUP_TERRENO_PRIV.ToString()),
                new ReportParameter("Parametro1", SUP_TERRENO_COM.ToString()),
                new ReportParameter("Parametro1", SUP_CONST_PRIV.ToString()),
                new ReportParameter("Parametro1", SUP_CONST_COM.ToString()),

                new ReportParameter("Parametro1", VAL_TERRENO.ToString()),
                new ReportParameter("Parametro1", VAL_CONSTRUCCION.ToString()),
                new ReportParameter("Parametro1", VAL_CATASTRAL.ToString()),
                new ReportParameter("Parametro1", FECHA_DIA.ToString()),

                new ReportParameter("Parametro1", FECHA_MES.ToString()),
                new ReportParameter("Parametro1", FECHA_A.ToString()),
                new ReportParameter("Parametro1", PERSONA_FIRMA.ToString())


             };

            if (Program.tipoReporte == 5) // Verifica si es el tipo de reporte 5
            {

                ReportParameter[] parametros2 = new ReportParameter[]
            {

                new ReportParameter("Parametro2", CERTIFICACION_01.ToString()),

             };
                reportViewer1.LocalReport.SetParameters(parametros2);
            }
            else
            {
                ReportParameter[] parametros2 = new ReportParameter[]
            {
                new ReportParameter("Parametro2", CERTIFICACION_01.ToString()),
                new ReportParameter("Parametro2", CERTIFICACION_02.ToString()),
                new ReportParameter("Parametro2", CERTIFICACION_03.ToString())
             };
                reportViewer1.LocalReport.SetParameters(parametros2);
            }
            // Asignar el parámetro al informe
            reportViewer1.LocalReport.SetParameters(parametros);

            // Refrescar el informe

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
