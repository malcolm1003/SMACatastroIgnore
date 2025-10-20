
using TextBox = System.Windows.Forms.TextBox;
using GMap.NET.MapProviders;
using AccesoBase;
using System;
using SMAv2._0._01Cartografia;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilerias;
using Font = System.Drawing.Font;
using System.Data.SqlClient;
using GMap.NET;
using GMap.NET.WindowsForms;
using Microsoft.Office.Interop.Excel;
using System.Security.Policy;
using System.Reflection;


namespace SMAv2._0._01Cartografia
{
    public partial class frmCatastro02UbicacionCambios : Form
    {
      
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();      //conexion a la base de sapase
        Util util = new Util();
        String AAA="";

        //METODO PARA ARRASTRAR EL FORMULARIO-----------------------------------------------------------------------------------------------
        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //METODOS PARA CERRAR,MAXIMIZAR, MINIMIZAR FORMULARIO-------------------------------------------------------------------------------
        int lx, ly;
        int sw, sh;

        // Variables que utiizamos en el form
        public frmCatastro02UbicacionCambios()
        {
            InitializeComponent();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            if (Program.tipoUbicacionCartografia == 2)      //cambios en clave catastral
            {
                VALIDACION();
            }
        }
        private void VALIDACION()
        {
            if (txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();

            int EXISTE_PRO = 0;
            int verificar = 0;

            /////////////////////////////////////////////////////////////  VERIFICAR SI EXISTE EN POPIEDADES
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM PROPIEDADES";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(deptoVar) + ")";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 1";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";
                con.cadena_sql_interno = con.cadena_sql_interno + " ELSE";
                con.cadena_sql_interno = con.cadena_sql_interno + "     BEGIN";
                con.cadena_sql_interno = con.cadena_sql_interno + "         SELECT memo = 2";
                con.cadena_sql_interno = con.cadena_sql_interno + "     End";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() == "") { EXISTE_PRO = 2; }
                    else
                    {
                        EXISTE_PRO = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }
                con.cerrar_interno();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }
            if (EXISTE_PRO == 2) { MessageBox.Show("NO EXISTE ESTA CLAVE CATASTRAL ", "ERROR", MessageBoxButtons.OK);
                txtZona.Focus();
                terminado();
                return;  
            }

            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE_2";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm( deptoVar) + ")";
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

                if (verificar == 1)
                {
                    MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA ", "ERROR", MessageBoxButtons.OK);
                    txtZona.Focus();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }
            try
            {
                //////////////VERIRFICAMOS SI SE ENCUENTRA BLOQUEADA LA CLAVE, SEGUNDA CONSULTA

                con.conectar_base_interno();
                con.cadena_sql_interno = "";
                con.cadena_sql_interno = con.cadena_sql_interno + " IF EXISTS (SELECT *";
                con.cadena_sql_interno = con.cadena_sql_interno + "              FROM BLOQCVE";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE estado = 15";
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MUNICIPIO = " + muniVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + zonaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + mznaVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + loteVar;
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + util.scm(edificioVar);
                con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + util.scm(deptoVar) + ")";
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

                if (verificar == 1)
                {
                    MessageBox.Show(" ESTA CLAVE CATASTRAL ESTA BLOQUEADA ", "ERROR", MessageBoxButtons.OK);
                    txtZona.Focus();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////  INGRESAMOS EL GMAP


            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
            gMapControl1.Position = new GMap.NET.PointLatLng(19.262174, -99.5330638);
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 14;
            gMapControl1.AutoScroll = true;
            gMapControl1.Visible = true;

            /////////////////////////////////////////////////////////////  INGRESAMOS EL GMAP



            /////////////////////////////////////////////////////////////  SI EXISTE CLAVE CONTINUAMOS CON EL PROCESO
            pnlCambios.Visible = true;
            btnConsulta.Enabled = false;
            btnGuardar.Enabled = true;
            btnBUSCAR.Enabled = false;
            lblobser.Enabled = true;
            txtObservaciones.Enabled = true;
            btnCancelar.Enabled = true;

            txtZona.Enabled = false;
            txtMzna.Enabled = false;
            txtLote.Enabled = false;
            txtEdificio.Enabled = false;
            txtDepto.Enabled = false;
        }

        private void frmCatastro02UbicacionCambios_Load(object sender, EventArgs e)
        {
            terminado();
            Program.tipoUbicacionCartografia = 2;
            label27.Text = "Usuario: " + Program.nombre_usuario.Trim();
            txtZona.Enabled = true;
            txtZona.Text = "";
            txtZona.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtZona.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtZona.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN ZONA", "ERROR", MessageBoxButtons.OK); txtZona.Focus(); return; }
            if (txtMzna.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtMzna.Text.Length < 3) { MessageBox.Show("SE DEBEN DE TENER 3 DIGITOS EN MANZANA", "ERROR", MessageBoxButtons.OK); txtMzna.Focus(); return; }
            if (txtLote.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtLote.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL LOTE", "ERROR", MessageBoxButtons.OK); txtLote.Focus(); return; }
            if (txtEdificio.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtEdificio.Text.Length < 2) { MessageBox.Show("SE DEBEN DE TENER 2 DIGITOS EN EL EDIFICIO", "ERROR", MessageBoxButtons.OK); txtEdificio.Focus(); return; }
            if (txtDepto.Text.Trim() == "") { MessageBox.Show("NO SE TIENE EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtDepto.Text.Length < 4) { MessageBox.Show("SE DEBEN DE TENER 4 DIGITOS EN EL DEPARTAMENTO", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtObservaciones.Text.Trim() == "") { MessageBox.Show("NO SE TIENE LA OBSERVACION", "ERROR", MessageBoxButtons.OK); txtObservaciones.Focus(); return; }

            String muniVar = Program.municipioT;
            String zonaVar = txtZona.Text.Trim();
            String mznaVar = txtMzna.Text.Trim();
            String loteVar = txtLote.Text.Trim();
            String edificioVar = txtEdificio.Text.Trim();
            String deptoVar = txtDepto.Text.Trim();
            String serie = Program.SerieC;
            string fechaIngreso = "";
            AAA = "";

            fechaIngreso = DateTime.Now.ToString("O");
            string fechaIngresos = fechaIngreso.Trim().Substring(0, 10);
            string fechasHora = fechaIngreso.Trim().Substring(11, 8);
            string fechaSql = fechaIngreso.Trim().Substring(0, 4) + fechaIngreso.Trim().Substring(5, 2) + fechaIngreso.Trim().Substring(8, 2);
            string fechaHoraSql = fechaSql + " " + fechasHora;
            int maxFolio = 0;

            //revisar las fechas como obtenerlas

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// COMPROBACIONES //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (!CHK_NOMBRE.Checked && !CHK_CONSTRUC.Checked && !CHK_CONSTRC_FACTORES.Checked && !CHK_SUPERFICIE.Checked && !CHK_TERRENO_FACTORES.Checked)
            {
                MessageBox.Show("NO SE TIENE NINGUNA OPCION DE CAMBIOS", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Sale de la función
            }
            AAA += (CHK_NOMBRE.Checked) ? "1" : "0";
            AAA += (CHK_SUPERFICIE.Checked) ? "1" : "0";
            AAA += (CHK_CONSTRUC.Checked) ? "1" : "0";
            AAA += (CHK_CONSTRC_FACTORES.Checked) ? "1" : "0";
            AAA += (CHK_TERRENO_FACTORES.Checked) ? "1" : "0";


            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /////// OBTENEMOS EL MAXIMO FOLIO //////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                con.conectar_base_interno();
                con.cadena_sql_interno = " SELECT MAX(FOLIO_ORIGEN) FROM CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE serie = " + util.scm(serie);

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                

                while (con.leer_interno.Read())
                {
                    if (con.leer_interno[0].ToString().Trim() != "")
                    {
                        maxFolio = Convert.ToInt32(con.leer_interno[0].ToString().Trim());
                    }
                }

                if (maxFolio == 0) { maxFolio = 1; } else { maxFolio = maxFolio + 1; }

                con.cerrar_interno();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///////  INSERTAR EN CAT_NEW_CARTOGRAFIA  ///////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            try
            {
                con.conectar_base_interno();

                con.cadena_sql_interno = " ";
                con.cadena_sql_interno = con.cadena_sql_interno + "INSERT INTO CAT_NEW_CARTOGRAFIA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "     (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ESTADO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    MUNICIPIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ZONA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    MANZANA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    LOTE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    EDIFICIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    DEPTO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    UBICACION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    DESCRIPCION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FECHA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    HORA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    USUARIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    OBSERVACIONES,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TERR_PROPIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TERR_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    AÑO_CALCULO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    frente,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    fondo,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    IRREGULARIDAD,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    TOPOGRAFIA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    POSICION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    INDIVISO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SUP_CON_COM,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SUP_CON,";

                con.cadena_sql_interno = con.cadena_sql_interno + "    REGIMEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    COD_CALLE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ZON_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_TERRENO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_TERRENO_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_CONST,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VAL_CONST_COMUN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_FRENTE,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_FONDO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_IRREG,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_AREA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_TOPO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FAC_POSICION,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ) ";
                con.cadena_sql_interno = con.cadena_sql_interno + " Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ( ";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 15 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(muniVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(zonaVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(mznaVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Convert.ToInt32(loteVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(edificioVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(deptoVar) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.tipoUbicacionCartografia + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    '" + AAA + "CAMBIO DE CLAVE" + "',";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(fechaSql) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(fechaHoraSql) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(Program.nombre_usuario) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(txtObservaciones.Text.Trim()) + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + Program.añoActual + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";

                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + 0 + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";

                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_interno = con.cadena_sql_interno + "    INSERT INTO CAT_DONDE_VA_2025";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    FOLIO_ORIGEN,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    CARTOGRAFIA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    VENTANILLA,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    REVISO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SISTEMAS,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    ELIMINADO,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SERIE";
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    Values";
                con.cadena_sql_interno = con.cadena_sql_interno + "    (";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + maxFolio + " ,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    1,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    0,";
                con.cadena_sql_interno = con.cadena_sql_interno + "    " + util.scm(serie);
                con.cadena_sql_interno = con.cadena_sql_interno + "    )";
                con.cadena_sql_interno = con.cadena_sql_interno + "    SET NOCOUNT ON";

                con.cadena_sql_cmd_interno();
                con.open_c_interno();
                con.leer_interno = con.cmd_interno.ExecuteReader();

                con.cerrar_interno();


                MessageBox.Show("FUE INGRESADO EL TRAMITE ,CON EXITO", "INFORMACION", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Retornar false si ocurre un error
            }
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (s, ev) =>
            {
                // Configurar la fuente
                Font font = new Font("Arial", 14, FontStyle.Bold);

                // Crear el texto a imprimir
                string texto = $"FOLIO: {serie}-  {maxFolio} CAMBIO DE CLAVE  041-{zonaVar}-{mznaVar}-{loteVar}-{edificioVar}-{deptoVar}    {DateTime.Now}";

                // Dibujar el texto en la página
                ev.Graphics.DrawString(texto, font, Brushes.Black, new PointF(15, 20));
            };

            // Iniciar la impresió
            printDoc.Print();
            //terminado();     
        }
        private void terminado()
        {
            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";
            txtObservaciones.Text = "";

            //txtMun.Enabled = true;
            txtZona.Enabled = true;
            txtMzna.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;
            txtObservaciones.Enabled = false;

            CHK_CONSTRC_FACTORES.Checked = false;
            CHK_CONSTRUC.Checked = false;
            CHK_NOMBRE.Checked = false;
            CHK_SUPERFICIE.Checked = false;
            CHK_TERRENO_FACTORES.Checked = false;

            pnlCambios.Visible= false ;

            btnConsulta.Enabled = true;
            btnGuardar.Enabled = false;
            btnBUSCAR.Enabled = true;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;
            btnCerrar.Enabled = true;

            txtZona.Focus();
        }
        private void txtEdificio_TextChanged(object sender, EventArgs e)
        {
            if (txtEdificio.Text.Length == 2) { txtDepto.Focus(); }
        }
        private void txtZona_TextChanged(object sender, EventArgs e)
        {
            if (txtZona.Text.Length == 2) { txtMzna.Focus(); }
        }
        private void txtMzna_TextChanged(object sender, EventArgs e)
        {
            if (txtMzna.Text.Length == 3) { txtLote.Focus(); }
        }
        private void txtLote_TextChanged(object sender, EventArgs e)
        {
            if (txtLote.Text.Length == 2) { txtEdificio.Focus(); }
        }
        private void txtDepto_TextChanged(object sender, EventArgs e)
        {
            if (txtDepto.Text.Length == 4) { btnConsulta.Focus(); }
        }
        private void NUMEROS(object sender, KeyPressEventArgs e)
        {

            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
      if (Char.IsControl(e.KeyChar)) //permitir teclas de control como retroceso 
            {
                e.Handled = false;
            }
            else
            {
                //el resto de teclas pulsadas se desactivan 
                e.Handled = true;
            }
        }
        private void LOSTFOCUS(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = System.Drawing.Color.White;
            txt.Select(txt.Text.Length, 0);
        }

        private void GOTFOCUS(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.BackColor = System.Drawing.Color.Yellow;
            txt.Select(txt.Text.Length, 0);
        }
        private void MAYUSCULAS(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            txt.Text = txt.Text.ToUpper();
            txt.Select(txt.Text.Length, 0);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            terminado();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            this.Size = new Size(sw, sh);
            this.Location = new System.Drawing.Point(lx, ly);
            btnNormal.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnBUSCAR_Click(object sender, EventArgs e)
        {
           // frmCatastro02UbicacionCambios.ActiveForm.Opacity = 0.50;
            frmBuscadorInmueble fs = new frmBuscadorInmueble();
            fs.ShowDialog();
            fs.Show();
            //frmCatastro02UbicacionCambios.ActiveForm.Opacity = 1.0;
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            btnMaximizar.Visible = false;
            btnNormal.Visible = true;
        }

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmCatastro02UbicacionCambios_Activated(object sender, EventArgs e)
        {
            //Program.tipoUbicacionCartografia = 2;
            //terminado();
            //label27.Text = "Usuario: " + Program.nombre_usuario.Trim();
            //txtZona.Focus();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleHybridMap;
            gMapControl1.Position = new GMap.NET.PointLatLng(19.262174, -99.5330638);
            gMapControl1.MinZoom = 1;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 14;
            gMapControl1.AutoScroll = true;
            gMapControl1.Visible = true;

            txtLatitud.Text = "";
            txtLongitud.Text = "";
        }

        private void cmdSalida_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.menuBotonBloqueo = 1;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.menuBotonBloqueo = 1;
        }
    }


}



