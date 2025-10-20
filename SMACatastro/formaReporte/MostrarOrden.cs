using AccesoBase;
using Microsoft.Reporting.WinForms;
using QRCoder;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
//using System.Web.Services.Description;

using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace SMAIngresos.formaReporte
{
    public partial class MostrarOrden : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();
        public MostrarOrden()
        {
            InitializeComponent();

        }
        public int folioOrden { get; set; }
        public string serieOrden { get; set; }
        //PARAMAETROS NUEVOS DE MANDAR  
        public string claveCat { get; set; }
        public int folioCat { get; set; }
        public string serieCatastro { get; set; }

        private DataTable ObtenerDatosOrdenSP(string serieOrden, int folioOrden)
        {
            DataTable dt = new DataTable();

            con.conectar_base_interno();
            con.open_c_interno();

            SqlCommand cmd = new SqlCommand("SONGspORDENpago", con.cnn_interno);
            cmd.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al procedimiento almacenado
            cmd.Parameters.Add("@serieOrd", SqlDbType.VarChar, 2).Value = serieOrden;
            cmd.Parameters.Add("@folioOrd", SqlDbType.Int, 2).Value = folioOrden;
            cmd.Connection = con.cnn_interno;
            dt.Load(cmd.ExecuteReader());

            //CERRAR LA CONEXIÓN
            con.cerrar_interno();
            return dt;
        }
        public byte[] GenerarQR(string texto)
        {
            // Crear un generador de QR
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // Generar la imagen del QR
            Bitmap qrCodeImage = qrCode.GetGraphic(20); // 20 es el tamaño de los píxeles

            // Convertir la imagen a un array de bytes
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        private void MostrarOrden_Load(object sender, System.EventArgs e)
        {
            ConfigurationManager.RefreshSection("connectionStrings");
            this.reportViewer1.LocalReport.EnableExternalImages = true; //Habilitar que el procedimiento almacenado acepte imagenes externas 
            reportViewer1.LocalReport.DisplayName = "ÓRDEN DE PAGO CATASTRO";
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);

            // OBTENER DATOS DE LA BASE DE DATOS
            DataTable dtDetalle = ObtenerDatosOrdenSP(serieOrden, folioOrden);

            string clave_qr;
            clave_qr = serieOrden + "-" + folioOrden;

            byte[] qrCodeBytes = GenerarQR(clave_qr);

            reportViewer1.LocalReport.ReportEmbeddedResource = "SMACatastro.formaReporte.repOrdenPago.rdlc";
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dtDetalle));


            ReportParameter[] parametros = new ReportParameter[]
            {
                new ReportParameter("Parametro1", claveCat.ToString()), //0
                new ReportParameter("Parametro1", folioCat.ToString()), //1 
                new ReportParameter("Parametro1", serieCatastro.ToString())//2
            };
            ReportParameter parametroQR = new ReportParameter("ParametroQR", Convert.ToBase64String(qrCodeBytes));
            //AGREGAR EL PARAMETRO AL INFORME 
            //indicar que es parametros y se los vamos a pasar 
            reportViewer1.LocalReport.SetParameters(parametros);

            reportViewer1.LocalReport.SetParameters(parametroQR);
            this.reportViewer1.RefreshReport();
            //Microsoft.Reporting.WinForms.Warning[] warnings;
            //string[] streamIds;
            //string contentType;
            //string encoding;
            //string extension;
            //string deviceInfo = @"<DeviceInfo>
            //          <OutputFormat>EMF</OutputFormat>
            //          <PageWidth>8.5in</PageWidth>
            //          <PageHeight>11in</PageHeight>
            //          <MarginTop>0.25in</MarginTop>
            //          <MarginLeft>0.25in</MarginLeft>
            //          <MarginRight>0.25in</MarginRight>
            //          <MarginBottom>0.25in</MarginBottom>
            //        </DeviceInfo>";

            //byte[] bytes = reportViewer1.LocalReport.Render("PDF", deviceInfo, out _, out encoding, out extension, out streamIds, out _);
            //FileStream fs = new FileStream(@"C:\SONGUI\SMA_ORDENES\" + serieOrden + "-" + folioOrden + @".pdf", FileMode.Create);
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();
            MostrarOrden mf = new MostrarOrden();
            mf.Close();
            this.reportViewer1.RefreshReport();

        }
    }
}
