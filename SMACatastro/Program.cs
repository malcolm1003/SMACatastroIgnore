using SMACatastro.catastroRevision;
using SMACatastro.catastroSistemas;
using System;
using System.Windows.Forms;

namespace SMACatastro
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SMACatastro.formaInicio.frm_00_Inicio());
            //Application.Run(new SMACatastro.catastroCartografia.frmCatastro03BusquedaCatastro());
            //Application.Run(new SMACatastro.catastroCartografia.frmCatastro03BusquedaCatastro());
            //Application.Run(new SMACatastro.catastroCartografia.frmCatastro03BusquedaCatastro());
            //Application.Run(new SMACatastro.catastroRevision.frmTresenUno());
            //Application.Run(new frmImportacion());
            //Application.Run(new frmCambiosClveCat());
            //Application.Run(new frmTresenUno());
            //Application.Run(new frmMovimientosSistemas());
        }

        internal static string VercionS = "1.1.0";
        internal static int menuBotonBloqueo = 0;
        internal static int formsAbiertosCount = 0;

        internal static int movimientoConstruccionPropia = 0;
        internal static int movimientoConstruccionComun = 0;
        internal static int tipoDeMovimientoProgram = 0;

        /////////////////////////////////////////////////////////////////////////////////////
        /// ingresar variable de usuario
        /////////////////////////////////////////////////////////////////////////////////////

        internal static int idUsuarioI = 0;
        internal static int noEmpleadoI = 0;

        internal static string acceso_usuario = "SONGUI22";
        internal static string acceso_contraseña = "";

        internal static string acceso_nombre_usuario = "";
        internal static int acceso_direccion = 0;
        internal static string acceso_area = "";
        internal static int acceso_areai = 0;
        internal static string acceso_cargo = "";
        internal static int acceso_nivel_acceso = 0;
        internal static int acceso_activo = 0;
        internal static int acceso_validacion = 0;
        internal static int acceso_id_direccion = 0;
        internal static int acceso_id_area = 0;
        internal static string acceso_sucursal = "";

        internal static int acceso_año = 0;
        internal static string acceso_serie = "";
        internal static int acceso_idSucursal = 0;
        internal static string acceso_sucDescripcion = "";

        internal static string nombre_usuario = "PROYECTO";
        internal static string id_cpu_cpu = "";
        internal static string sucursal = "";
        internal static int sucursales2 = 0;
        internal static string correo = "";
        internal static int caja_cobro = 0;

        internal static double subTotalDesglosado = 0;
        internal static double ivaDesglosado = 0;
        internal static double totalDesglosado = 0;
        internal static int derivadaHistorial = 0;

        /////////////////////////////////////////////////////////////////////////////////////
        /////////// variables para reporte  
        /////////////////////////////////////////////////////////////////////////////////////

        internal static int tipoReporte = 0;

        /////////////////////////////////////////////////////////////////////////////////////
        /// CONFIGURACIO_INICIO
        /////////////////////////////////////////////////////////////////////////////////////
        internal static int PEstado = 15;
        internal static int PlanC = 0;
        internal static int añoActual = 2025;
        internal static string serie = "Y";
        internal static string serieOrdenPago = "A";
        internal static int folioOrdenPago = 0;
        internal static int oficinaCobro = 1;
        internal static string rfcEmpresa = "MSM850101P79";
        internal static string Vmunicipio = "041";
        internal static string Vestado = "15";
        internal static string ALTA = "";
        internal static string MODIFICACION = "";
        internal static string BAJA = "";

        internal static string serieCan = "";
        internal static int folioCan = 0;

        internal static string nconexion = "Data Source = 25.51.96.16\\SAPATEST; Initial Catalog = DesarrolloSMA ; Persist Security Info = True; User ID = sa; Password = Songuisapa21";
        //internal static string nconexion = "Data Source = 25.3.127.73; Initial Catalog = DB_INTMIOD_SAPASE; Persist Security Info = True; User ID = sa; Password = $4GU4_54P4S32024";
        internal static string nconexion2 = "Data Source = 999.99.99.9; Initial Catalog = DB; Persist Security Info = True; User ID = ta; Password = NoFound";

        internal static int municipioN = 41;
        internal static string municipioT = "041";

        /////////////////////////////////////////////////////////////////////////////////////
        /// VARIABLES_DE_CLAVE_CATASTRAL
        /////////////////////////////////////////////////////////////////////////////////////

        internal static string municipioV = "";
        internal static string zonaV = "";
        internal static string manzanaV = "";
        internal static string loteV = "";
        internal static string edificioV = "";
        internal static string deptoV = "";
        internal static string SerieC = "Y";
        internal static string tipologiaC = "";
        internal static int indexTipologiaC = 0;

        internal static int tipoContruccion = 0;
        internal static int tipoUbicacionCartografia = 0;
        internal static string CLIENTE = "SMA";
        internal static string PERMISOS = "TEMAS";
        internal static double constuccion;
        internal static int FolioC;

        /////////////////////////////////////////////////////////////////////////////////////
        /// VARIABLES_DE_ORDENES_DE_PAGO
        /////////////////////////////////////////////////////////////////////////////////////

        internal static int tipoOrdenDePago = 0;

        internal static Boolean cerrado = true;


        internal static int cIdAreaBloqueoC = 3;
        /////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////


    }
}
