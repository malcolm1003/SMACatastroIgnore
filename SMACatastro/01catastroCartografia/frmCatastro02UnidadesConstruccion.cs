using AccesoBase;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Utilerias;
using Font = System.Drawing.Font;

namespace SMACatastro.catastroCartografia
{
    public partial class frmCatastro02UnidadesConstruccion : Form
    {
        CSE_01_CONEXION_2 con = new CSE_01_CONEXION_2();      //conexion a la base de sapase
        Util util = new Util();
        public int maxUnidad = 0;
        public int tipoGuardado = 0;    // 1 = nuevo, 2 = modificar

        public frmCatastro02UnidadesConstruccion()
        {
            InitializeComponent();
        }
        private void limpiarCajasInicio()
        {
            cmdNuevo.Enabled = false;
            btnGuardar.Enabled = false;
            btnBorrar.Enabled = false;
            btnCancelar.Enabled = false;
            cmdSalida.Enabled = true;

            cmdBorrarUno.Enabled = false;
            cmdAplica.Enabled = false;
            cmdAutorisa.Enabled = false;

            txtNumero.Text = "";
            cboUnidades.Text = "";
            txtSupCont.Text = "";
            cboAñoConstruccion.SelectedIndex = -1;
            cboConservacion.SelectedIndex = -1;
            cboNiveles.SelectedIndex = -1;
            cboUnidades.SelectedIndex = -1;

            lblSupConstruccion.Text = "";
            lblValConstruccion.Text = "";
            lblConstTotal.Text = "";
            lblValorConstTotal.Text = "";
        }
        private void llenarCombosTipologia()
        {
            cboUnidades.Items.Clear();

            int acumulativoI = 1;
            string acumalativoS = "";
            Program.indexTipologiaC = 0;
            string tipologiasSS = "";

            con.conectar_base_interno();
            con.cadena_sql_interno = "";

            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT RTRIM(Uso), RTRIM(claseConst), RTRIM(categConst), RTRIM(DescrClCat), ValM2Const";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM TIPO_CONST";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE AnioVigVUC = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "  ORDER BY Uso";

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                acumalativoS = Convert.ToString(acumulativoI);
                tipologiasSS = con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "   " + con.leer_interno[3].ToString().Trim();

                if (Program.tipologiaC.Trim() == tipologiasSS.Trim()) { Program.indexTipologiaC = acumulativoI; }

                cboUnidades.Items.Add(con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  " + con.leer_interno[3].ToString().Trim());

                //if (acumalativoS.Trim().Length == 1) { cboUnidades.Items.Add(acumalativoS + "     " + con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  " + con.leer_interno[3].ToString().Trim()); }
                //if (acumalativoS.Trim().Length == 2) { cboUnidades.Items.Add(acumalativoS + "   " + con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  " + con.leer_interno[3].ToString().Trim()); }
                //if (acumalativoS.Trim().Length == 3) { cboUnidades.Items.Add(acumalativoS + " " + con.leer_interno[0].ToString().Trim() + con.leer_interno[1].ToString().Trim() + con.leer_interno[2].ToString().Trim() + "  " + con.leer_interno[3].ToString().Trim()); }

                acumulativoI = acumulativoI + 1;
            }
            con.cerrar_interno();
        }
        private void llenarGridUnidades()
        {
            //con.cadena_sql_interno = "";
            //con.cadena_sql_interno = con.cadena_sql_interno + " SELECT NumUnidad, Uso, ClaseConst, CategConst, SupCons, ValorCons, AniodeCons, EstadoCons, NivCons";
            //con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
            //con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE Zona = " + txtZona.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana =" + txtMzna.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote = " + txtLote.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND Edificio = " + txtEdificio.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND Depto =" + txtDepto.Text.Trim();

            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + "SELECT Unidad            = RTRIM(uc.NumUnidad),";
            con.cadena_sql_interno = con.cadena_sql_interno + "       Tipologia         = RTRIM(uc.Uso) + '' + RTRIM(uc.ClaseConst) + '' + RTRIM(uc.CategConst) + '   ' + RTRIM(tc.DescrClCat),";
            con.cadena_sql_interno = con.cadena_sql_interno + "       Construc          = CAST(RTRIM(uc.SupCons) as float),";
            con.cadena_sql_interno = con.cadena_sql_interno + "       Valor_M2          = CAST(RTRIM(tc.ValM2Const) as float),";
            con.cadena_sql_interno = con.cadena_sql_interno + "       Año               = RTRIM(uc.AniodeCons),";
            con.cadena_sql_interno = con.cadena_sql_interno + "       Conservacion      = RTRIM(f.DescFact),";
            con.cadena_sql_interno = con.cadena_sql_interno + "       Niveles           = RTRIM(uc.NivCons),";
            con.cadena_sql_interno = con.cadena_sql_interno + "       V_Unidad          = CAST(RTRIM(uc.ValorCons) as float),";
            con.cadena_sql_interno = con.cadena_sql_interno + "       Folio             = RTRIM(uc.FOLIO)";
            con.cadena_sql_interno = con.cadena_sql_interno + "  FROM UNID_CONST uc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       TIPO_CONST tc,";
            con.cadena_sql_interno = con.cadena_sql_interno + "       FACTORES f";
            con.cadena_sql_interno = con.cadena_sql_interno + " WHERE Zona              = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Manzana           = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND Lote              = " + txtLote.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND uc.Uso            = tc.Uso";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND uc.ClaseConst     = tc.ClaseConst";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND uc.CategConst     = tc.CategConst";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND tc.AnioVigVUC     = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND uc.EstadoCons     = f.RangoInf";
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND f.AnioVigMD       = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "   AND f.TipoMerDem      = 3";
            con.cadena_sql_interno = con.cadena_sql_interno + " ORDER BY uc.NumUnidad";

            con.conectar_base_interno();
            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            SqlDataAdapter da = new SqlDataAdapter(con.cmd_interno);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("159, 24, 151");

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold); //Microsoft sans serif para todas las celdas


            dataGridView1.Columns[2].HeaderText = "Construccion";

            dataGridView1.Columns[0].Width = 75;        // unidad
            dataGridView1.Columns[1].Width = 540;       // tipologia
            dataGridView1.Columns[2].Width = 130;       // construccion
            dataGridView1.Columns[3].Width = 120;       // valor x metro cuadrado
            dataGridView1.Columns[4].Width = 60;        // Año
            dataGridView1.Columns[5].Width = 130;       // conservacion
            dataGridView1.Columns[6].Width = 80;        // niveles
            dataGridView1.Columns[7].Width = 130;       // valor por unidad
            dataGridView1.Columns[8].Width = 80;        // folio

            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // unidad
            dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;      // tipologia
            dataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // construccion
            dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // valor x metro cuadrado
            dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // Año
            dataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // conservacion
            dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // niveles
            dataGridView1.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // valor por unidad
            dataGridView1.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;    // folio

            dataGridView1.Columns[2].DefaultCellStyle.Format = "###,###,##0.##";

            dataGridView1.Columns[3].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "C2";


            //dataGridView1.Columns["Valor_M2"].DefaultCellStyle.Format = "##,##0.##";
            //dataGridView1.Columns["V_Unidad"].DefaultCellStyle.Format = "##,##0.#0";
            //dataGridView1.Columns["Construc"].DefaultCellStyle.Format = "##,##0.##";
            //dataGridView1.Columns["V_Unidad"].DefaultCellStyle.Format = "##,##0.##";

            //dataGridView1.Columns["V_Unidad"].DefaultCellStyle.Format = "C2";

            //dataGridView1.Columns[0].Width = 65;        // unidad
            //dataGridView1.Columns[1].Width = 450;       // tipologia
            //dataGridView1.Columns[2].Width = 120;        // construccion
            //dataGridView1.Columns[3].Width = 100;        // valor x metro cuadrado
            //dataGridView1.Columns[4].Width = 60;        // Año
            //dataGridView1.Columns[5].Width = 130;       // conservacion
            //dataGridView1.Columns[6].Width = 80;        // niveles
            //dataGridView1.Columns[7].Width = 130;       // valor por unidad
            //dataGridView1.Columns[8].Width = 150;       // privada o comun
            //dataGridView1.Columns[9].Width = 80;        // folio




            //dataGridView1.Columns["Construc"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Unidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Valor_M2"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Año"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Conservacion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Niveles"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["V_Unidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Pri=0_Com=1"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Folio"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dataGridView1.Columns["Unidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Tipologia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dataGridView1.Columns["Construc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Valor_M2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Año"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Conservacion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Niveles"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["V_Unidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dataGridView1.Columns["Pri=0_Com=1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.Columns["Folio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;







            con.cerrar_interno();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///

            con.conectar_base_interno();
            con.cadena_sql_interno = "";

            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT sum(SupCons), sum(ValorCons) ";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE Zona = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Manzana = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Lote = " + txtLote.Text.Trim();

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            double sumaTotal = 0.00;
            double cantidadTotal = 0.00;

            while (con.leer_interno.Read())
            {
                if (con.leer_interno[0].ToString().Trim() == "")
                {
                    lblConstTotal.Text = "0.00";
                    lblValorConstTotal.Text = "0.00";
                }
                else
                {
                    sumaTotal = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
                    cantidadTotal = Convert.ToDouble(con.leer_interno[1].ToString().Trim());

                    lblConstTotal.Text = Convert.ToString(sumaTotal.ToString("###,###,###,###.##"));
                    lblValorConstTotal.Text = Convert.ToString(cantidadTotal.ToString("###,###,###,###.##"));

                    maxUnidad = Convert.ToInt32(con.leer_interno[0].ToString().Trim()) + 1;
                }
            }
            con.cerrar_interno();
        }
        private void maximoValorUnidad()
        {
            maxUnidad = 0;

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT max(NumUnidad)";
            con.cadena_sql_interno = con.cadena_sql_interno + "              FROM UNID_CONST";
            con.cadena_sql_interno = con.cadena_sql_interno + "             WHERE MUNICIPIO = " + Program.municipioT;
            con.cadena_sql_interno = con.cadena_sql_interno + "               AND ZONA = " + txtZona.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "               AND MANZANA = " + txtMzna.Text.Trim();
            con.cadena_sql_interno = con.cadena_sql_interno + "               AND LOTE = " + txtLote.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "               AND EDIFICIO = " + txtEdificio.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "               AND DEPTO = " + txtDepto.Text.Trim();

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                if (con.leer_interno[0].ToString().Trim() == "") { maxUnidad = 1; }
                else
                {
                    maxUnidad = Convert.ToInt32(con.leer_interno[0].ToString().Trim()) + 1;
                }
            }
            con.cerrar_interno();
        }
        private void cajas_amarilla(int x)
        {
            switch (x)
            {
                case 1: cboUnidades.BackColor = System.Drawing.Color.Yellow; break;
                case 2: txtSupCont.BackColor = System.Drawing.Color.Yellow; break;
                case 3: cboAñoConstruccion.BackColor = System.Drawing.Color.Yellow; break;
                case 4: cboConservacion.BackColor = System.Drawing.Color.Yellow; break;
                case 5: cboNiveles.BackColor = System.Drawing.Color.Yellow; break;
            }
        }
        private void cajas_blancas(int x)
        {
            switch (x)
            {
                case 1: cboUnidades.BackColor = System.Drawing.Color.White; break;
                case 2: txtSupCont.BackColor = System.Drawing.Color.White; break;
                case 3: cboAñoConstruccion.BackColor = System.Drawing.Color.White; break;
                case 4: cboConservacion.BackColor = System.Drawing.Color.White; break;
                case 5: cboNiveles.BackColor = System.Drawing.Color.White; break;
            }
        }
        private void autorizaCalculo()
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

            if (txtNumero.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL NUMERO DE LA UNIDAD", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboUnidades.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LA TIPOLOGIA DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtSupCont.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LA SUPERFICIE DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboNiveles.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LOS NIVELES DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboConservacion.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL GRADO DE CONSERVACION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboAñoConstruccion.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL AÑO DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            string fechasTimbrado = "";
            double valorVmetroCuadrado = 0;
            double AA = 0.0;

            fechasTimbrado = DateTime.Now.ToString("O");
            string fechasTimbrados = fechasTimbrado.Trim().Substring(0, 10);
            string fechasHora = fechasTimbrado.Trim().Substring(11, 8);
            string fechaSql = fechasTimbrado.Trim().Substring(0, 4) + fechasTimbrado.Trim().Substring(5, 2) + fechasTimbrado.Trim().Substring(8, 2);
            string fechaHoraSql = fechaSql + " " + fechasHora;

            //con.conectar_base_interno();
            //con.cadena_sql_interno = "";
            //con.cadena_sql_interno = con.cadena_sql_interno + " SELECT max(NumUnidad)";
            //con.cadena_sql_interno = con.cadena_sql_interno + "   FROM UNID_CONST";
            //con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE MUNICIPIO = " + Program.municipioT;
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND ZONA = " + txtZona.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND MANZANA = " + txtMzna.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND LOTE = " + txtLote.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND EDIFICIO = " + txtEdificio.Text.Trim();
            //con.cadena_sql_interno = con.cadena_sql_interno + "    AND DEPTO = " + txtDepto.Text.Trim();

            //con.cadena_sql_cmd_interno();
            //con.open_c_interno();
            //con.leer_interno = con.cmd_interno.ExecuteReader();

            //while (con.leer_interno.Read())
            //{
            //    if (con.leer_interno[0].ToString().Trim() == "") { maxUnidad = 1; }
            //    else { maxUnidad = Convert.ToInt32(con.leer_interno[0].ToString().Trim()); }
            //}
            //con.cerrar_interno();

            string SupContT = String.Format("{0:###0.00}", Convert.ToDouble(txtSupCont.Text.Trim()));
            double SupContT2 = Convert.ToDouble(SupContT);

            int unidadV = Convert.ToInt32(txtNumero.Text.Trim());
            string bb1V = cboUnidades.Text.Substring(0, 1);
            string bb2V = cboUnidades.Text.Substring(1, 1);
            int bb3V = Convert.ToInt32(cboUnidades.Text.Substring(2, 1));
            int años_consV = Program.añoActual - Convert.ToInt32(cboAñoConstruccion.Text.Trim());
            int estadoV = Convert.ToInt32(cboConservacion.Text.Trim().Substring(0, 1));
            int niveles2 = Convert.ToInt32(cboNiveles.Text.Trim().Substring(0, 1));
            double sup_total = SupContT2;
            int años_cons_años = Convert.ToInt32(cboAñoConstruccion.Text.Trim());

            ///*********************************************************************************************************///
            /// obtenemos el facedcon
            ///*********************************************************************************************************///

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT CoefDemA";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM FACEDCON";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE AnioVigVUC =" + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Uso = '" + bb1V + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND ClaseConst = '" + bb2V + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND CategConst = " + bb3V;

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                AA = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
            }
            con.cerrar_interno();

            ///*********************************************************************************************************///
            /// Factores de Estado
            ///*********************************************************************************************************///

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT Factor";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM FACTORES";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CptoMerDem = 'C'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND TipoMerDem = 3";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND AnioVigMD  = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND NumFactor  = " + estadoV;

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            double estado2 = 1;
            while (con.leer_interno.Read())
            {
                estado2 = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
            }
            con.cerrar_interno();

            ///*********************************************************************************************************///
            /// Factores para la edad
            ///*********************************************************************************************************///

            double factor_e = 0.0;
            if (años_consV > 0)
            {
                factor_e = 1 - (años_consV * AA);
                if (factor_e < 0.60)
                {
                    factor_e = 0.60;
                }
            }
            else
            {
                factor_e = 1;
                años_consV = 0;
            }

            ///*********************************************************************************************************///
            /// Factores para el estado de la construccion
            ///*********************************************************************************************************///

            double factor_c = 0.0;
            factor_c = estado2;

            ///*********************************************************************************************************///
            /// Factores de niveles
            ///*********************************************************************************************************///

            double factor_n = 0.0;
            if (niveles2 <= 2)
            {
                factor_n = 1;
            }
            else
            {
                factor_n = 1 + ((niveles2 - 2) * 0.002);
            }

            ///*********************************************************************************************************///
            /// Cambiamos los factores a 0.00000
            ///*********************************************************************************************************///

            double factor_ee = Math.Round(factor_e, 5);
            double factor_cc = Math.Round(factor_c, 5);
            double factor_nn = Math.Round(factor_n, 5);

            double sum_factores = factor_ee * factor_cc * factor_nn;

            ///*********************************************************************************************************///
            /// el factor de nivel lo ponemos en 1 para que no baje mucho su valor
            ///*********************************************************************************************************///

            sum_factores = factor_ee * factor_cc * 1;
            if (sum_factores < 0.4) { sum_factores = 0.4; }

            ///*********************************************************************************************************///
            /// Obtenemos el valor por metro cuadrado de construccion 
            ///*********************************************************************************************************///

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT ValM2Const";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM TIPO_CONST";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE AnioVigVUC = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Uso        = '" + bb1V + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND ClaseConst = '" + bb2V + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND CategConst = " + bb3V;

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            double valorV = 1;
            while (con.leer_interno.Read())
            {
                valorV = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
            }
            con.cerrar_interno();

            double valor_const = valorV * sup_total * sum_factores;
            double valor_constt = Math.Round(valor_const, 5);
            int ResultadoV = 0;

            if (lblTipoConstr.Text == "Privada")
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("SongInsertarPrivadaConstr", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@estado", SqlDbType.Int, 8).Value = 15;
                cmd.Parameters.Add("@municipio", SqlDbType.Int, 8).Value = 041;
                cmd.Parameters.Add("@zona", SqlDbType.Int, 8).Value = Convert.ToInt32(txtZona.Text.Trim());
                cmd.Parameters.Add("@manzana", SqlDbType.Int, 8).Value = Convert.ToInt32(txtMzna.Text.Trim());
                cmd.Parameters.Add("@lote", SqlDbType.Int, 8).Value = Convert.ToInt32(txtLote.Text.Trim());
                cmd.Parameters.Add("@edificio", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
                cmd.Parameters.Add("@depto", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();
                cmd.Parameters.Add("@unidad", SqlDbType.Int, 8).Value = Convert.ToInt32(txtNumero.Text.Trim());
                cmd.Parameters.Add("@supTotal", SqlDbType.Float, 50).Value = sup_total;
                cmd.Parameters.Add("@bb1", SqlDbType.VarChar, 1).Value = bb1V;
                cmd.Parameters.Add("@bb2", SqlDbType.VarChar, 1).Value = bb2V;
                cmd.Parameters.Add("@bb3", SqlDbType.Int, 1).Value = bb3V;

                cmd.Parameters.Add("@añosConstr", SqlDbType.Int, 4).Value = años_cons_años;
                cmd.Parameters.Add("@estados", SqlDbType.Int, 8).Value = estadoV;
                cmd.Parameters.Add("@niveles", SqlDbType.Int, 8).Value = niveles2;
                cmd.Parameters.Add("@factor_e", SqlDbType.Float, 12).Value = factor_ee;
                cmd.Parameters.Add("@factor_c", SqlDbType.Float, 12).Value = factor_cc;
                cmd.Parameters.Add("@factor_n", SqlDbType.Float, 12).Value = factor_nn;
                cmd.Parameters.Add("@valorConst", SqlDbType.Float, 12).Value = valor_constt;

                cmd.Parameters.Add("@fechaCap", SqlDbType.VarChar, 10).Value = fechaSql;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 10).Value = Program.acceso_usuario;

                if (tipoGuardado == 1)
                {
                    cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 1;
                    lblFolio.Text = "0";
                }       /// alta
                if (tipoGuardado == 2) { cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 2; }       /// guardado
                if (tipoGuardado == 3) { cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 3; }       /// baja
                cmd.Parameters.Add("@folio", SqlDbType.Int, 8).Value = Convert.ToInt32(lblFolio.Text.Trim());

                cmd.Parameters.Add("@respuesta", SqlDbType.Int, 8).Direction = ParameterDirection.Output;
                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                ResultadoV = Convert.ToInt32(cmd.Parameters["@respuesta"].Value);

                con.cerrar_interno();
            }

            if (lblTipoConstr.Text == "Comun")
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("SongInsertarComunConstr", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@estado", SqlDbType.Int, 8).Value = 15;
                cmd.Parameters.Add("@municipio", SqlDbType.Int, 8).Value = 041;
                cmd.Parameters.Add("@zona", SqlDbType.Int, 8).Value = Convert.ToInt32(txtZona.Text.Trim());
                cmd.Parameters.Add("@manzana", SqlDbType.Int, 8).Value = Convert.ToInt32(txtMzna.Text.Trim());
                cmd.Parameters.Add("@lote", SqlDbType.Int, 8).Value = Convert.ToInt32(txtLote.Text.Trim());
                cmd.Parameters.Add("@edificio", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
                cmd.Parameters.Add("@depto", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();
                cmd.Parameters.Add("@unidad", SqlDbType.Int, 8).Value = Convert.ToInt32(txtNumero.Text.Trim());
                cmd.Parameters.Add("@supTotal", SqlDbType.Float, 50).Value = sup_total;
                cmd.Parameters.Add("@bb1", SqlDbType.VarChar, 1).Value = bb1V;
                cmd.Parameters.Add("@bb2", SqlDbType.VarChar, 1).Value = bb2V;
                cmd.Parameters.Add("@bb3", SqlDbType.Int, 1).Value = bb3V;

                cmd.Parameters.Add("@añosConstr", SqlDbType.Int, 4).Value = años_cons_años;
                cmd.Parameters.Add("@estados", SqlDbType.Int, 8).Value = estadoV;
                cmd.Parameters.Add("@niveles", SqlDbType.Int, 8).Value = niveles2;
                cmd.Parameters.Add("@factor_e", SqlDbType.Float, 12).Value = factor_ee;
                cmd.Parameters.Add("@factor_c", SqlDbType.Float, 12).Value = factor_cc;
                cmd.Parameters.Add("@factor_n", SqlDbType.Float, 12).Value = factor_nn;
                cmd.Parameters.Add("@valorConst", SqlDbType.Float, 12).Value = valor_constt;

                cmd.Parameters.Add("@fechaCap", SqlDbType.VarChar, 10).Value = fechaSql;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 10).Value = Program.acceso_usuario;

                if (tipoGuardado == 1)
                {
                    cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 1;
                    lblFolio.Text = "0";
                }       /// alta
                if (tipoGuardado == 2) { cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 2; }       /// guardado
                if (tipoGuardado == 3) { cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 3; }       /// baja
                cmd.Parameters.Add("@folio", SqlDbType.Int, 8).Value = Convert.ToInt32(lblFolio.Text.Trim());

                cmd.Parameters.Add("@respuesta", SqlDbType.Int, 8).Direction = ParameterDirection.Output;
                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                ResultadoV = Convert.ToInt32(cmd.Parameters["@respuesta"].Value);

                con.cerrar_interno();
            }

            lblFolio.Text = "";
            nuevoInicio();
            inabilitarBotonesYtexto();
            llenarGridUnidades();
            cmdNuevo.Focus();
        }
        private void nuevoInicio()
        {
            cmdNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnBorrar.Enabled = false;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;

            cmdBorrarUno.Enabled = false;
            cmdAplica.Enabled = false;
            cmdAutorisa.Enabled = false;

            txtNumero.Text = "";
            cboUnidades.SelectedIndex = -1;
            txtSupCont.Text = "";
            cboAñoConstruccion.SelectedIndex = -1;
            cboConservacion.SelectedIndex = -1;
            cboNiveles.SelectedIndex = -1;

            lblSupConstruccion.Text = "";
            lblValConstruccion.Text = "";
            lblConstTotal.Text = "";
            lblValorConstTotal.Text = "";
        }
        private void inabilitarBotonesYtexto()
        {
            cmdNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnBorrar.Enabled = false;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;

            cmdBorrarUno.Enabled = false;
            cmdAplica.Enabled = false;
            cmdAutorisa.Enabled = false;

            cboUnidades.Enabled = false;
            txtSupCont.Enabled = false;
            cboAñoConstruccion.Enabled = false;
            cboConservacion.Enabled = false;
            cboNiveles.Enabled = false;

            lblSupConstruccion.Enabled = false;
            lblValConstruccion.Enabled = false;
            lblConstTotal.Enabled = false;
            lblValorConstTotal.Enabled = false;
        }
        private void eliminaRegistro()
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

            if (txtNumero.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL NUMERO DE LA UNIDAD", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboUnidades.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LA TIPOLOGIA DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtSupCont.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LA SUPERFICIE DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboNiveles.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LOS NIVELES DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboConservacion.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL GRADO DE CONSERVACION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboAñoConstruccion.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL AÑO DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            string fechasTimbrado = "";
            double valorVmetroCuadrado = 0;
            double AA = 0.0;

            tipoGuardado = 3;

            fechasTimbrado = DateTime.Now.ToString("O");
            string fechasTimbrados = fechasTimbrado.Trim().Substring(0, 10);
            string fechasHora = fechasTimbrado.Trim().Substring(11, 8);
            string fechaSql = fechasTimbrado.Trim().Substring(0, 4) + fechasTimbrado.Trim().Substring(5, 2) + fechasTimbrado.Trim().Substring(8, 2);
            string fechaHoraSql = fechaSql + " " + fechasHora;

            string SupContT = String.Format("{0:###0.00}", Convert.ToDouble(txtSupCont.Text.Trim()));
            double SupContT2 = Convert.ToDouble(SupContT);

            int unidadV = Convert.ToInt32(txtNumero.Text.Trim());
            string bb1V = cboUnidades.Text.Substring(0, 1);
            string bb2V = cboUnidades.Text.Substring(1, 1);
            int bb3V = Convert.ToInt32(cboUnidades.Text.Substring(2, 1));
            int años_consV = Program.añoActual - Convert.ToInt32(cboAñoConstruccion.Text.Trim());
            int estadoV = Convert.ToInt32(cboConservacion.Text.Trim().Substring(0, 1));
            int niveles2 = Convert.ToInt32(cboNiveles.Text.Trim().Substring(0, 1));
            double sup_total = SupContT2;
            int años_cons_años = Convert.ToInt32(cboAñoConstruccion.Text.Trim());

            ///*********************************************************************************************************///
            /// obtenemos el facedcon
            ///*********************************************************************************************************///

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT CoefDemA";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM FACEDCON";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE AnioVigVUC =" + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Uso = '" + bb1V + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND ClaseConst = '" + bb2V + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND CategConst = " + bb3V;

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            while (con.leer_interno.Read())
            {
                AA = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
            }
            con.cerrar_interno();

            ///*********************************************************************************************************///
            /// Factores de Estado
            ///*********************************************************************************************************///

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT Factor";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM FACTORES";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE CptoMerDem = 'C'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND TipoMerDem = 3";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND AnioVigMD  = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND NumFactor  = " + estadoV;

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            double estado2 = 1;
            while (con.leer_interno.Read())
            {
                estado2 = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
            }
            con.cerrar_interno();

            ///*********************************************************************************************************///
            /// Factores para la edad
            ///*********************************************************************************************************///

            double factor_e = 0.0;
            if (años_consV > 0)
            {
                factor_e = 1 - (años_consV * AA);
                if (factor_e < 0.60)
                {
                    factor_e = 0.60;
                }
            }
            else
            {
                factor_e = 1;
                años_consV = 0;
            }

            ///*********************************************************************************************************///
            /// Factores para el estado de la construccion
            ///*********************************************************************************************************///

            double factor_c = 0.0;
            factor_c = estado2;

            ///*********************************************************************************************************///
            /// Factores de niveles
            ///*********************************************************************************************************///

            double factor_n = 0.0;
            if (niveles2 <= 2)
            {
                factor_n = 1;
            }
            else
            {
                factor_n = 1 + ((niveles2 - 2) * 0.002);
            }

            ///*********************************************************************************************************///
            /// Cambiamos los factores a 0.00000
            ///*********************************************************************************************************///

            double factor_ee = Math.Round(factor_e, 5);
            double factor_cc = Math.Round(factor_c, 5);
            double factor_nn = Math.Round(factor_n, 5);

            double sum_factores = factor_ee * factor_cc * factor_nn;

            ///*********************************************************************************************************///
            /// el factor de nivel lo ponemos en 1 para que no baje mucho su valor
            ///*********************************************************************************************************///

            sum_factores = factor_ee * factor_cc * 1;
            if (sum_factores < 0.4) { sum_factores = 0.4; }

            ///*********************************************************************************************************///
            /// Obtenemos el valor por metro cuadrado de construccion 
            ///*********************************************************************************************************///

            con.conectar_base_interno();
            con.cadena_sql_interno = "";
            con.cadena_sql_interno = con.cadena_sql_interno + " SELECT ValM2Const";
            con.cadena_sql_interno = con.cadena_sql_interno + "   FROM TIPO_CONST";
            con.cadena_sql_interno = con.cadena_sql_interno + "  WHERE AnioVigVUC = " + Program.añoActual;
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND Uso        = '" + bb1V + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND ClaseConst = '" + bb2V + "'";
            con.cadena_sql_interno = con.cadena_sql_interno + "    AND CategConst = " + bb3V;

            con.cadena_sql_cmd_interno();
            con.open_c_interno();
            con.leer_interno = con.cmd_interno.ExecuteReader();

            double valorV = 1;
            while (con.leer_interno.Read())
            {
                valorV = Convert.ToDouble(con.leer_interno[0].ToString().Trim());
            }
            con.cerrar_interno();

            double valor_const = valorV * sup_total * sum_factores;
            double valor_constt = Math.Round(valor_const, 5);
            int ResultadoV = 0;

            if (lblTipoConstr.Text == "Privada")
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("SongInsertarPrivadaConstr", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@estado", SqlDbType.Int, 8).Value = 15;
                cmd.Parameters.Add("@municipio", SqlDbType.Int, 8).Value = 041;
                cmd.Parameters.Add("@zona", SqlDbType.Int, 8).Value = Convert.ToInt32(txtZona.Text.Trim());
                cmd.Parameters.Add("@manzana", SqlDbType.Int, 8).Value = Convert.ToInt32(txtMzna.Text.Trim());
                cmd.Parameters.Add("@lote", SqlDbType.Int, 8).Value = Convert.ToInt32(txtLote.Text.Trim());
                cmd.Parameters.Add("@edificio", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
                cmd.Parameters.Add("@depto", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();
                cmd.Parameters.Add("@unidad", SqlDbType.Int, 8).Value = Convert.ToInt32(txtNumero.Text.Trim());
                cmd.Parameters.Add("@supTotal", SqlDbType.Float, 50).Value = sup_total;
                cmd.Parameters.Add("@bb1", SqlDbType.VarChar, 1).Value = bb1V;
                cmd.Parameters.Add("@bb2", SqlDbType.VarChar, 1).Value = bb2V;
                cmd.Parameters.Add("@bb3", SqlDbType.Int, 1).Value = bb3V;

                cmd.Parameters.Add("@añosConstr", SqlDbType.Int, 4).Value = años_cons_años;
                cmd.Parameters.Add("@estados", SqlDbType.Int, 8).Value = estadoV;
                cmd.Parameters.Add("@niveles", SqlDbType.Int, 8).Value = niveles2;
                cmd.Parameters.Add("@factor_e", SqlDbType.Float, 12).Value = factor_ee;
                cmd.Parameters.Add("@factor_c", SqlDbType.Float, 12).Value = factor_cc;
                cmd.Parameters.Add("@factor_n", SqlDbType.Float, 12).Value = factor_nn;
                cmd.Parameters.Add("@valorConst", SqlDbType.Float, 12).Value = valor_constt;

                cmd.Parameters.Add("@fechaCap", SqlDbType.VarChar, 10).Value = fechaSql;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 10).Value = Program.acceso_usuario;

                if (tipoGuardado == 1) { cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 1; }       /// alta
                if (tipoGuardado == 2) { cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 2; }       /// guardado
                if (tipoGuardado == 3) { cmd.Parameters.Add("@tipoMod", SqlDbType.Int, 8).Value = 3; }
                cmd.Parameters.Add("@folio", SqlDbType.Int, 8).Value = Convert.ToInt32(lblFolio.Text.Trim());

                cmd.Parameters.Add("@respuesta", SqlDbType.Int, 8).Direction = ParameterDirection.Output;
                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                ResultadoV = Convert.ToInt32(cmd.Parameters["@respuesta"].Value);

                con.cerrar_interno();
            }

            if (lblTipoConstr.Text == "Comun")
            {
                con.conectar_base_interno();
                con.open_c_interno();

                SqlCommand cmd = new SqlCommand("SongInsertarComunConstr.", con.cnn_interno);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@estado", SqlDbType.Int, 8).Value = "15";
                cmd.Parameters.Add("@municipio", SqlDbType.Int, 8).Value = "041";
                cmd.Parameters.Add("@zona", SqlDbType.Int, 8).Value = txtZona.Text.Trim();
                cmd.Parameters.Add("@manzana", SqlDbType.Int, 8).Value = txtMzna.Text.Trim();
                cmd.Parameters.Add("@lote", SqlDbType.Int, 8).Value = txtLote.Text.Trim();
                cmd.Parameters.Add("@edificio", SqlDbType.VarChar, 2).Value = txtEdificio.Text.Trim();
                cmd.Parameters.Add("@depto", SqlDbType.VarChar, 4).Value = txtDepto.Text.Trim();
                cmd.Parameters.Add("@unidad", SqlDbType.Int, 8).Value = Convert.ToInt32(txtNumero.Text.Trim());
                cmd.Parameters.Add("@supTotal", SqlDbType.Float, 50).Value = sup_total;
                cmd.Parameters.Add("@bb1", SqlDbType.VarChar, 1).Value = bb1V;
                cmd.Parameters.Add("@bb2", SqlDbType.VarChar, 1).Value = bb2V;
                cmd.Parameters.Add("@bb3", SqlDbType.Int, 1).Value = bb3V;

                cmd.Parameters.Add("@añosConstr", SqlDbType.Int, 4).Value = años_cons_años;
                cmd.Parameters.Add("@estados", SqlDbType.Int, 8).Value = estadoV;
                cmd.Parameters.Add("@niveles", SqlDbType.Int, 8).Value = niveles2;
                cmd.Parameters.Add("@factor_e", SqlDbType.Float, 12).Value = factor_ee;
                cmd.Parameters.Add("@factor_c", SqlDbType.Float, 12).Value = factor_cc;
                cmd.Parameters.Add("@factor_n", SqlDbType.Float, 12).Value = factor_nn;
                cmd.Parameters.Add("@valorConst", SqlDbType.Float, 12).Value = valorV;

                cmd.Parameters.Add("@fechaCap", SqlDbType.VarChar, 50).Value = fechasTimbrado;
                cmd.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = Program.acceso_usuario;
                cmd.Parameters.Add("@tipoMod", SqlDbType.VarChar, 50).Value = 1;

                cmd.Parameters.Add("@respuesta", SqlDbType.Int, 8).Direction = ParameterDirection.Output;
                cmd.Connection = con.cnn_interno;
                cmd.ExecuteNonQuery();

                ResultadoV = Convert.ToInt32(cmd.Parameters["@respuesta"].Value);
                con.cerrar_interno();
            }

            lblFolio.Text = "";
            nuevoInicio();
            inabilitarBotonesYtexto();
            llenarGridUnidades();
            cmdNuevo.Focus();
        }
        private void inicio()
        {
            txtMun.Text = "";
            txtZona.Text = "";
            txtMzna.Text = "";
            txtLote.Text = "";
            txtEdificio.Text = "";
            txtDepto.Text = "";

            txtMun.Text = Program.municipioV.Trim();
            txtZona.Text = Program.zonaV.Trim();
            txtMzna.Text = Program.manzanaV.Trim();
            txtLote.Text = Program.loteV.Trim();
            txtEdificio.Text = Program.edificioV.Trim();
            txtDepto.Text = Program.deptoV.Trim();

            limpiarCajasInicio();
            cmdNuevo.Enabled = true;
            btnCancelar.Enabled = true;
            cmdSalida.Enabled = true;

            txtMun.Enabled = true;
            txtZona.Enabled = true;
            txtMzna.Enabled = true;
            txtLote.Enabled = true;
            txtEdificio.Enabled = true;
            txtDepto.Enabled = true;

            txtNumero.Enabled = false;
            cboUnidades.Enabled = false;
            txtSupCont.Enabled = false;
            cboAñoConstruccion.Enabled = false;
            cboConservacion.Enabled = false;
            cboNiveles.Enabled = false;
            cboUnidades.Enabled = false;

            if (Program.tipoContruccion == 0) { lblTipoConstr.Text = "Privada"; }
            else { lblTipoConstr.Text = "Comun"; }

            llenarGridUnidades();
            cmdNuevo.Focus();
        }





        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frmCatastro02UnidadesConstruccion_Load(object sender, EventArgs e)
        {
            inicio();
            if (Program.tipoUbicacionCartografia != 1)      // Cambios en clave catastral
            {
                cmdNuevo.Enabled = false;
            }
            else
            {
                cmdNuevo.Focus();
            }
        }

        private void frmCatastro02UnidadesConstruccion_Activated(object sender, EventArgs e)
        {
            //inicio();
            cmdNuevo.Focus();
        }

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            llenarCombosTipologia();
            llenarGridUnidades();
            maximoValorUnidad();

            txtNumero.Enabled = true;
            cboUnidades.Enabled = true;
            txtSupCont.Enabled = true;
            cboAñoConstruccion.Enabled = true;
            cboConservacion.Enabled = true;
            cboNiveles.Enabled = true;

            txtNumero.Text = Convert.ToString(maxUnidad);
            cmdBorrarUno.Enabled = false;
            cmdAplica.Enabled = true;
            tipoGuardado = 1;           // 1 = nuevo, 2 = modificar
            cboUnidades.Focus();
        }

        private void txtSupCont_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("SOLO SE PERMITEN NUMEROS", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtSupCont_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboNiveles.Focus();
            }
        }

        private void cboUnidades_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(1);
        }

        private void cboUnidades_Leave(object sender, EventArgs e)
        {
            cajas_blancas(1);
        }

        private void txtSupCont_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(2);
        }

        private void txtSupCont_Leave(object sender, EventArgs e)
        {
            cajas_blancas(2);
        }

        private void cboConservacion_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(4);
        }

        private void cboNiveles_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(5);
        }

        private void cboNiveles_Leave(object sender, EventArgs e)
        {
            cajas_blancas(5);
        }

        private void cboConservacion_Leave(object sender, EventArgs e)
        {
            cajas_blancas(4);
        }

        private void cboAñoConstruccion_Enter(object sender, EventArgs e)
        {
            cajas_amarilla(3);
        }

        private void cboAñoConstruccion_Leave(object sender, EventArgs e)
        {
            cajas_blancas(3);
        }

        private void cmdAplica_Click(object sender, EventArgs e)
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

            if (txtNumero.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL NUMERO DE LA UNIDAD", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboUnidades.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LA TIPOLOGIA DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (txtSupCont.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LA SUPERFICIE DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboNiveles.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER LOS NIVELES DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboConservacion.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL GRADO DE CONSERVACION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }
            if (cboAñoConstruccion.Text.Trim() == "") { MessageBox.Show("SE DEBEN DE TENER EL AÑO DE CONSTRUCCION", "ERROR", MessageBoxButtons.OK); txtDepto.Focus(); return; }

            con.conectar_base_interno();
            con.open_c_interno();

            SqlCommand cmd = new SqlCommand("SongCalculoContrUnidad", con.cnn_interno);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@uso", SqlDbType.VarChar, 1).Value = cboUnidades.Text.Trim().Substring(0, 1);
            cmd.Parameters.Add("@claseConst", SqlDbType.VarChar, 1).Value = cboUnidades.Text.Trim().Substring(1, 1);
            cmd.Parameters.Add("@categConst", SqlDbType.Int, 12).Value = Convert.ToInt32(cboUnidades.Text.Trim().Substring(2, 1));
            cmd.Parameters.Add("@anioVigVUC", SqlDbType.Int, 12).Value = Program.añoActual;
            cmd.Parameters.Add("@estadoCons", SqlDbType.Int, 12).Value = Convert.ToInt32(cboConservacion.Text.Trim().Substring(0, 1));
            cmd.Parameters.Add("@aniodeCons", SqlDbType.Int, 12).Value = Convert.ToInt32(cboAñoConstruccion.Text.Trim().Substring(0, 4));
            cmd.Parameters.Add("@nivCons", SqlDbType.Int, 12).Value = Convert.ToInt32(cboNiveles.Text.Trim().Substring(0, 1));
            cmd.Parameters.Add("@total_sup_const", SqlDbType.Float, 16).Value = Convert.ToDouble(txtSupCont.Text.Trim());
            cmd.Parameters.Add("@valorConstruccion", SqlDbType.Float, 16).Direction = ParameterDirection.Output;

            cmd.Connection = con.cnn_interno;
            cmd.ExecuteNonQuery();
            double valorConstruccionUnidad = Convert.ToDouble(cmd.Parameters["@valorConstruccion"].Value);

            con.cerrar_interno();

            lblSupConstruccion.Text = "";
            lblValConstruccion.Text = "";

            lblSupConstruccion.Text = txtSupCont.Text.Trim();
            lblValConstruccion.Text = String.Format("{0:#,##0.00}", valorConstruccionUnidad);

            lblSupConstruccion.Text = Convert.ToString(Convert.ToDouble(txtSupCont.Text.Trim()).ToString("###,###,###,###.##"));
            //lblValConstruccion.Text = valorConstruccionUnidad.ToString("###,###,###,###.##");

            cmdAutorisa.Enabled = true;
            btnGuardar.Enabled = true;
            cmdBorrarUno.Enabled = true;
            cmdAplica.Enabled = true;
        }

        private void cmdAutorisa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Agregar la Construccion", "AGREGAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                autorizaCalculo();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            inabilitarBotonesYtexto();
            nuevoInicio();
            llenarGridUnidades();
            if (Program.tipoUbicacionCartografia != 1)      // Cambios en clave catastral
            {
                cmdNuevo.Enabled = false;
            }
            else
            {
                cmdNuevo.Focus();
            }
            //cmdNuevo.Focus();
        }

        private void cmdSalida_Click(object sender, EventArgs e)
        {
            if (lblConstTotal.Text.ToString() != "")
            {
                //frmCatastro01UbicacionAlta form2 = new frmCatastro01UbicacionAlta();

                // Obtener el texto del Label de Form1
                string textoDelLabel = lblConstTotal.Text.ToString();

                // Asignar el texto al TextBox de Form2
                //form2.SetTextBoxValue(textoDelLabel);
                Program.constuccion = Convert.ToDouble(lblConstTotal.Text.ToString());

                this.Close();

                // Mostrar Form2
                //form2.Show();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            //--------------------------------------------------------------------------------------------------------------------------------//
            //-------------------------  PROCEDEMOS A OBTENER LOS DATOS DEL GRID Y MANDARLOS A LA CAJA DE TEXTO ------------------------------//
            //--------------------------------------------------------------------------------------------------------------------------------//

            double totalT = 0;
            string estatusf = "";

            int numRound = dataGridView1.CurrentRow.Index;

            llenarCombosTipologia();
            if (numRound > -1)
            {
                int folioBuscar = Convert.ToInt32(dataGridView1.CurrentRow.Cells[8].Value.ToString().Trim());           //inmuebleDerivada

                txtNumero.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                lblFolio.Text = "";
                lblFolio.Text = Convert.ToString(folioBuscar);

                double txtSupContV = Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim());
                txtSupCont.Text = txtSupContV.ToString("###,###,###,###.##");



                string tipologiaS = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim().Substring(0, 3);
                string textoBusqueda = dataGridView1.CurrentRow.Cells[1].Value.ToString().Trim();

                int a = 0;

                while (a < cboUnidades.Items.Count)
                {
                    if (cboUnidades.Items[a].ToString().Trim().Substring(0, 3) == tipologiaS)
                    {
                        cboUnidades.SelectedIndex = a;
                        break;
                    }
                    a++;
                }

                cboNiveles.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString().Trim();
                cboAñoConstruccion.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString().Trim();



                if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "BUENO") { cboConservacion.SelectedIndex = 0; }
                if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "NORMAL") { cboConservacion.SelectedIndex = 1; }
                if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "REGULAR") { cboConservacion.SelectedIndex = 2; }
                if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "MALO") { cboConservacion.SelectedIndex = 3; }
                if (dataGridView1.CurrentRow.Cells[5].Value.ToString().Trim() == "RUINOSO") { cboConservacion.SelectedIndex = 4; }

                cboUnidades.Enabled = true;
                txtSupCont.Enabled = true;
                cboNiveles.Enabled = true;
                cboConservacion.Enabled = true;
                cboAñoConstruccion.Enabled = true;

                double constrSubTotal = Convert.ToDouble(dataGridView1.CurrentRow.Cells[2].Value.ToString().Trim());
                double valorSubTotal = Convert.ToDouble(dataGridView1.CurrentRow.Cells[7].Value.ToString().Trim());

                lblSupConstruccion.Text = constrSubTotal.ToString("###,###,###,###.##");
                lblValConstruccion.Text = valorSubTotal.ToString("###,###,###,###.##");

                cmdNuevo.Enabled = false;
                btnGuardar.Enabled = false;
                if(Program.tipoUbicacionCartografia != 1)      // Cambios en clave catastral
                {
                    btnBorrar.Enabled = false;
                    cmdBorrarUno.Enabled = false;
                    cmdAplica.Enabled = false;
                }
                else
                {
                    btnBorrar.Enabled = true;
                    cmdBorrarUno.Enabled = true;
                    cmdAplica.Enabled = true;
                }
                
                btnCancelar.Enabled = true;
                cmdSalida.Enabled = false;

                //cmdBorrarUno.Enabled = true;
                //cmdAplica.Enabled = true;
                cmdAutorisa.Enabled = false;
                tipoGuardado = 2;

                cboUnidades.Focus();
            }
        }

        private void cmdBorrarUno_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desean Eliminar la Unidad de Construcción", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                eliminaRegistro();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desean Eliminar la Unidad de Construcción", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                eliminaRegistro();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Guardar la Unidad de Construcción", "GUARDAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                autorizaCalculo();
            }
        }

        private void cmdAutorisa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MessageBox.Show("Desea Guardar la Unidad de Construcción", "GUARDAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    autorizaCalculo();
                }
            }
        }

        private void btnGuardar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MessageBox.Show("Desea Guardar la Unidad de Construcción", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    autorizaCalculo();
                }
            }
        }

        private void btnBorrar_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MessageBox.Show("Desean Eliminar la Unidad de Construcción", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    eliminaRegistro();
                }
            }
        }

        private void cmdBorrarUno_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (MessageBox.Show("Desean Eliminar la Unidad de Construcción", "ELIMINAR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    eliminaRegistro();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        // METODO PARA ARRASTRAR EL FORMULARIO
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {

        }

        private void tmFechaHora_Tick(object sender, EventArgs e)
        {
            lbFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ssss tt");
        }

        private void cmdNuevo_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdNuevo, "NUEVO");
        }

        private void btnGuardar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnGuardar, "GUARDAR");
        }

        private void btnBorrar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnBorrar, "BORRAR");
        }

        private void btnCancelar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnCancelar, "CANCELAR");
        }

        private void cmdSalida_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdSalida, "SALIDA");
        }

        private void cmdBorrarUno_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdBorrarUno, "BORRAR UNO");
        }

        private void cmdAplica_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdAplica, "CALCULO");
        }

        private void cmdAutorisa_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(cmdAutorisa, "INGRESAR EL REGISTRO");
        }

        private void btnMinimizar_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(btnMinimizar, "MINIMIZAR");
        }
    }
}
