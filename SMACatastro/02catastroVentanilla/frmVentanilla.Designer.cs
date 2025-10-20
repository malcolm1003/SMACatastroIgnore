using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace SMACatastro.catastroRevision
{
    partial class frmVentanilla
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVentanilla));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel13 = new System.Windows.Forms.Panel();
            this.lblNoAutorizado = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNoAutorizar = new System.Windows.Forms.Button();
            this.btnAutorizar = new System.Windows.Forms.Button();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblComentario = new System.Windows.Forms.Label();
            this.lblOperacion = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.lblObsCar = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.lblHistorial = new System.Windows.Forms.Label();
            this.pnlCambios = new System.Windows.Forms.Panel();
            this.ckbCambioFactoresTerr = new System.Windows.Forms.CheckBox();
            this.ckbCambioFactoresCons = new System.Windows.Forms.CheckBox();
            this.ckbCambioSuperficie = new System.Windows.Forms.CheckBox();
            this.ckbCambioConstruccion = new System.Windows.Forms.CheckBox();
            this.ckbCambioNombre = new System.Windows.Forms.CheckBox();
            this.pnlAltaInfo = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlCertificado = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.pnlDatosPredio = new System.Windows.Forms.Panel();
            this.panel54 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.lblPesito = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblValTerrPriv = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.lblValTotCons = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lblValTotTerr = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblValorConsPriv = new System.Windows.Forms.Label();
            this.lblValTerrCom = new System.Windows.Forms.Label();
            this.lblValConsCom = new System.Windows.Forms.Label();
            this.panel53 = new System.Windows.Forms.Panel();
            this.lblConstTot = new System.Windows.Forms.Label();
            this.lblTerrenoTot = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.lblSupTerrPriv = new System.Windows.Forms.Label();
            this.lblSupConsPriv = new System.Windows.Forms.Label();
            this.lblSupTerrComun = new System.Windows.Forms.Label();
            this.lblSupConsCom = new System.Windows.Forms.Label();
            this.btnMaps = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.lblDomicilio = new System.Windows.Forms.Label();
            this.lblTitular = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblCiudadano = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.lblLongitud = new System.Windows.Forms.Label();
            this.lblLatitud = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lbFecha = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnConsulta = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pbxQR2 = new System.Windows.Forms.PictureBox();
            this.pbxQR = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnCancela = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PanelBarraTitulo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnMaximizar = new System.Windows.Forms.Button();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.tmFechaHora = new System.Windows.Forms.Timer(this.components);
            this.panel15 = new System.Windows.Forms.Panel();
            this.lblMun = new System.Windows.Forms.Label();
            this.txtEdificio = new System.Windows.Forms.TextBox();
            this.txtDepto = new System.Windows.Forms.TextBox();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.txtManzana = new System.Windows.Forms.TextBox();
            this.txtZona = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.CBO_SERIE = new System.Windows.Forms.ComboBox();
            this.label34 = new System.Windows.Forms.Label();
            this.TXT_FOLIO = new System.Windows.Forms.TextBox();
            this.pnlAlta = new System.Windows.Forms.Panel();
            this.btnMapsA = new System.Windows.Forms.Button();
            this.lblRegimenA = new System.Windows.Forms.Label();
            this.lblFondoA = new System.Windows.Forms.Label();
            this.lblAreaA = new System.Windows.Forms.Label();
            this.lblSupConsCA = new System.Windows.Forms.Label();
            this.lblSupTerrCA = new System.Windows.Forms.Label();
            this.lblFrenteA = new System.Windows.Forms.Label();
            this.lbldesA = new System.Windows.Forms.Label();
            this.lblSupConsA = new System.Windows.Forms.Label();
            this.lblSupTerrenoA = new System.Windows.Forms.Label();
            this.lblCalleA = new System.Windows.Forms.Label();
            this.lblCodCalle = new System.Windows.Forms.Label();
            this.lblZonaA = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.cboUbicacion = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label56 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.DGVRESULTADO = new System.Windows.Forms.DataGridView();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.btnBuscarClave = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSalida = new System.Windows.Forms.Button();
            this.label92 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.panel13.SuspendLayout();
            this.panel12.SuspendLayout();
            this.pnlCambios.SuspendLayout();
            this.pnlAltaInfo.SuspendLayout();
            this.pnlCertificado.SuspendLayout();
            this.pnlDatosPredio.SuspendLayout();
            this.panel54.SuspendLayout();
            this.panel53.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxQR2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxQR)).BeginInit();
            this.PanelBarraTitulo.SuspendLayout();
            this.panel15.SuspendLayout();
            this.pnlAlta.SuspendLayout();
            this.pnlBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVRESULTADO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.panel13.Controls.Add(this.lblNoAutorizado);
            this.panel13.Controls.Add(this.label1);
            this.panel13.Controls.Add(this.btnNoAutorizar);
            this.panel13.Controls.Add(this.btnAutorizar);
            this.panel13.Controls.Add(this.txtObservaciones);
            this.panel13.Controls.Add(this.lblComentario);
            this.panel13.Controls.Add(this.lblOperacion);
            this.panel13.Controls.Add(this.panel11);
            this.panel13.Location = new System.Drawing.Point(746, 528);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(607, 158);
            this.panel13.TabIndex = 1702;
            // 
            // lblNoAutorizado
            // 
            this.lblNoAutorizado.BackColor = System.Drawing.Color.White;
            this.lblNoAutorizado.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblNoAutorizado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNoAutorizado.Location = new System.Drawing.Point(131, 26);
            this.lblNoAutorizado.Name = "lblNoAutorizado";
            this.lblNoAutorizado.Size = new System.Drawing.Size(468, 48);
            this.lblNoAutorizado.TabIndex = 1684;
            this.lblNoAutorizado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 1683;
            this.label1.Text = "Pendiente Por:";
            // 
            // btnNoAutorizar
            // 
            this.btnNoAutorizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnNoAutorizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNoAutorizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnNoAutorizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnNoAutorizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoAutorizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNoAutorizar.ForeColor = System.Drawing.Color.White;
            this.btnNoAutorizar.Location = new System.Drawing.Point(349, 130);
            this.btnNoAutorizar.Name = "btnNoAutorizar";
            this.btnNoAutorizar.Size = new System.Drawing.Size(122, 23);
            this.btnNoAutorizar.TabIndex = 1682;
            this.btnNoAutorizar.Text = "PENDIENTE";
            this.btnNoAutorizar.UseVisualStyleBackColor = false;
            this.btnNoAutorizar.Click += new System.EventHandler(this.btnNoAutorizar_Click);
            // 
            // btnAutorizar
            // 
            this.btnAutorizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnAutorizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutorizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnAutorizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnAutorizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutorizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutorizar.ForeColor = System.Drawing.Color.White;
            this.btnAutorizar.Location = new System.Drawing.Point(477, 130);
            this.btnAutorizar.Name = "btnAutorizar";
            this.btnAutorizar.Size = new System.Drawing.Size(122, 23);
            this.btnAutorizar.TabIndex = 1681;
            this.btnAutorizar.Text = "AUTORIZAR";
            this.btnAutorizar.UseVisualStyleBackColor = false;
            this.btnAutorizar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.Location = new System.Drawing.Point(131, 76);
            this.txtObservaciones.MaxLength = 200;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(468, 48);
            this.txtObservaciones.TabIndex = 1605;
            this.txtObservaciones.Enter += new System.EventHandler(this.txtObservaciones_Enter);
            this.txtObservaciones.Leave += new System.EventHandler(this.txtObservaciones_Leave);
            // 
            // lblComentario
            // 
            this.lblComentario.AutoSize = true;
            this.lblComentario.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComentario.ForeColor = System.Drawing.Color.White;
            this.lblComentario.Location = new System.Drawing.Point(19, 77);
            this.lblComentario.Name = "lblComentario";
            this.lblComentario.Size = new System.Drawing.Size(111, 17);
            this.lblComentario.TabIndex = 1604;
            this.lblComentario.Text = "Observaciones:";
            // 
            // lblOperacion
            // 
            this.lblOperacion.AutoSize = true;
            this.lblOperacion.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperacion.ForeColor = System.Drawing.Color.White;
            this.lblOperacion.Location = new System.Drawing.Point(4, 2);
            this.lblOperacion.Name = "lblOperacion";
            this.lblOperacion.Size = new System.Drawing.Size(133, 17);
            this.lblOperacion.TabIndex = 1593;
            this.lblOperacion.Text = "COMPLEMENTO";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel11.Location = new System.Drawing.Point(4, 22);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(133, 2);
            this.panel11.TabIndex = 1594;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.panel12.Controls.Add(this.label47);
            this.panel12.Controls.Add(this.lblUbicacion);
            this.panel12.Controls.Add(this.label46);
            this.panel12.Controls.Add(this.lblObsCar);
            this.panel12.Controls.Add(this.label37);
            this.panel12.Controls.Add(this.panel14);
            this.panel12.Controls.Add(this.lblHistorial);
            this.panel12.Controls.Add(this.pnlCambios);
            this.panel12.Controls.Add(this.pnlAltaInfo);
            this.panel12.Controls.Add(this.pnlCertificado);
            this.panel12.Location = new System.Drawing.Point(13, 305);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(726, 141);
            this.panel12.TabIndex = 1701;
            // 
            // label47
            // 
            this.label47.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.ForeColor = System.Drawing.Color.White;
            this.label47.Location = new System.Drawing.Point(8, 69);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(140, 21);
            this.label47.TabIndex = 1548;
            this.label47.Text = "Operaciones:";
            this.label47.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.BackColor = System.Drawing.Color.White;
            this.lblUbicacion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblUbicacion.Location = new System.Drawing.Point(151, 48);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(568, 24);
            this.lblUbicacion.TabIndex = 1547;
            this.lblUbicacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label46
            // 
            this.label46.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.White;
            this.label46.Location = new System.Drawing.Point(-24, 48);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(172, 21);
            this.label46.TabIndex = 1546;
            this.label46.Text = "Tramite:";
            this.label46.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblObsCar
            // 
            this.lblObsCar.BackColor = System.Drawing.Color.White;
            this.lblObsCar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblObsCar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblObsCar.Location = new System.Drawing.Point(151, 23);
            this.lblObsCar.Name = "lblObsCar";
            this.lblObsCar.Size = new System.Drawing.Size(568, 24);
            this.lblObsCar.TabIndex = 1545;
            this.lblObsCar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.White;
            this.label37.Location = new System.Drawing.Point(4, 26);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(144, 18);
            this.label37.TabIndex = 1544;
            this.label37.Text = "Observaciones Cart:";
            this.label37.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel14.Location = new System.Drawing.Point(3, 18);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(177, 2);
            this.panel14.TabIndex = 1215;
            // 
            // lblHistorial
            // 
            this.lblHistorial.AutoSize = true;
            this.lblHistorial.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHistorial.ForeColor = System.Drawing.Color.White;
            this.lblHistorial.Location = new System.Drawing.Point(0, -1);
            this.lblHistorial.Name = "lblHistorial";
            this.lblHistorial.Size = new System.Drawing.Size(170, 17);
            this.lblHistorial.TabIndex = 1214;
            this.lblHistorial.Text = "DATOS DEL PROCESO";
            // 
            // pnlCambios
            // 
            this.pnlCambios.BackColor = System.Drawing.Color.White;
            this.pnlCambios.Controls.Add(this.ckbCambioFactoresTerr);
            this.pnlCambios.Controls.Add(this.ckbCambioFactoresCons);
            this.pnlCambios.Controls.Add(this.ckbCambioSuperficie);
            this.pnlCambios.Controls.Add(this.ckbCambioConstruccion);
            this.pnlCambios.Controls.Add(this.ckbCambioNombre);
            this.pnlCambios.Enabled = false;
            this.pnlCambios.Location = new System.Drawing.Point(151, 73);
            this.pnlCambios.Name = "pnlCambios";
            this.pnlCambios.Size = new System.Drawing.Size(568, 63);
            this.pnlCambios.TabIndex = 1549;
            // 
            // ckbCambioFactoresTerr
            // 
            this.ckbCambioFactoresTerr.AutoSize = true;
            this.ckbCambioFactoresTerr.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCambioFactoresTerr.Location = new System.Drawing.Point(256, 21);
            this.ckbCambioFactoresTerr.Name = "ckbCambioFactoresTerr";
            this.ckbCambioFactoresTerr.Size = new System.Drawing.Size(247, 19);
            this.ckbCambioFactoresTerr.TabIndex = 9;
            this.ckbCambioFactoresTerr.Text = "CAMBIO EN FACTORES DE TERRENO";
            this.ckbCambioFactoresTerr.UseVisualStyleBackColor = true;
            // 
            // ckbCambioFactoresCons
            // 
            this.ckbCambioFactoresCons.AutoSize = true;
            this.ckbCambioFactoresCons.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCambioFactoresCons.Location = new System.Drawing.Point(256, 2);
            this.ckbCambioFactoresCons.Name = "ckbCambioFactoresCons";
            this.ckbCambioFactoresCons.Size = new System.Drawing.Size(290, 19);
            this.ckbCambioFactoresCons.TabIndex = 8;
            this.ckbCambioFactoresCons.Text = "CAMBIO EN FACTORES DE CONSTRUCCIÓN";
            this.ckbCambioFactoresCons.UseVisualStyleBackColor = true;
            // 
            // ckbCambioSuperficie
            // 
            this.ckbCambioSuperficie.AutoSize = true;
            this.ckbCambioSuperficie.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCambioSuperficie.Location = new System.Drawing.Point(7, 40);
            this.ckbCambioSuperficie.Name = "ckbCambioSuperficie";
            this.ckbCambioSuperficie.Size = new System.Drawing.Size(252, 19);
            this.ckbCambioSuperficie.TabIndex = 7;
            this.ckbCambioSuperficie.Text = "CAMBIO DE SUPERFICIE DE TERRENO";
            this.ckbCambioSuperficie.UseVisualStyleBackColor = true;
            // 
            // ckbCambioConstruccion
            // 
            this.ckbCambioConstruccion.AutoSize = true;
            this.ckbCambioConstruccion.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCambioConstruccion.Location = new System.Drawing.Point(7, 21);
            this.ckbCambioConstruccion.Name = "ckbCambioConstruccion";
            this.ckbCambioConstruccion.Size = new System.Drawing.Size(221, 19);
            this.ckbCambioConstruccion.TabIndex = 6;
            this.ckbCambioConstruccion.Text = "CAMBIO EN LA CONSTRUCCIÓN";
            this.ckbCambioConstruccion.UseVisualStyleBackColor = true;
            // 
            // ckbCambioNombre
            // 
            this.ckbCambioNombre.AutoSize = true;
            this.ckbCambioNombre.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbCambioNombre.Location = new System.Drawing.Point(7, 2);
            this.ckbCambioNombre.Name = "ckbCambioNombre";
            this.ckbCambioNombre.Size = new System.Drawing.Size(155, 19);
            this.ckbCambioNombre.TabIndex = 5;
            this.ckbCambioNombre.Text = "CAMBIO DE NOMBRE";
            this.ckbCambioNombre.UseVisualStyleBackColor = true;
            // 
            // pnlAltaInfo
            // 
            this.pnlAltaInfo.BackColor = System.Drawing.Color.White;
            this.pnlAltaInfo.Controls.Add(this.label6);
            this.pnlAltaInfo.Location = new System.Drawing.Point(151, 73);
            this.pnlAltaInfo.Name = "pnlAltaInfo";
            this.pnlAltaInfo.Size = new System.Drawing.Size(568, 63);
            this.pnlAltaInfo.TabIndex = 1550;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, -10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(566, 76);
            this.label6.TabIndex = 1192;
            this.label6.Text = resources.GetString("label6.Text");
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCertificado
            // 
            this.pnlCertificado.BackColor = System.Drawing.Color.White;
            this.pnlCertificado.Controls.Add(this.label20);
            this.pnlCertificado.Location = new System.Drawing.Point(151, 73);
            this.pnlCertificado.Name = "pnlCertificado";
            this.pnlCertificado.Size = new System.Drawing.Size(568, 60);
            this.pnlCertificado.TabIndex = 1550;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(1, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(562, 51);
            this.label20.TabIndex = 1195;
            this.label20.Text = "Los certificados que se pueden realizar son:\r\nCertificado de clave y valor catast" +
    "ral (CCVC), Certificado Aportación a Mejoras (CAM), (CCVC) con (CAM), y certific" +
    "ado de TRES en UNO.    ";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDatosPredio
            // 
            this.pnlDatosPredio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.pnlDatosPredio.Controls.Add(this.panel54);
            this.pnlDatosPredio.Controls.Add(this.panel53);
            this.pnlDatosPredio.Controls.Add(this.btnMaps);
            this.pnlDatosPredio.Controls.Add(this.label17);
            this.pnlDatosPredio.Controls.Add(this.lblDomicilio);
            this.pnlDatosPredio.Controls.Add(this.lblTitular);
            this.pnlDatosPredio.Controls.Add(this.label15);
            this.pnlDatosPredio.Controls.Add(this.lblCiudadano);
            this.pnlDatosPredio.Controls.Add(this.label12);
            this.pnlDatosPredio.Controls.Add(this.panel4);
            this.pnlDatosPredio.Controls.Add(this.label13);
            this.pnlDatosPredio.Location = new System.Drawing.Point(13, 448);
            this.pnlDatosPredio.Name = "pnlDatosPredio";
            this.pnlDatosPredio.Size = new System.Drawing.Size(726, 238);
            this.pnlDatosPredio.TabIndex = 1698;
            this.pnlDatosPredio.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDatosPredio_Paint);
            // 
            // panel54
            // 
            this.panel54.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel54.Controls.Add(this.label27);
            this.panel54.Controls.Add(this.label35);
            this.panel54.Controls.Add(this.label41);
            this.panel54.Controls.Add(this.label45);
            this.panel54.Controls.Add(this.label42);
            this.panel54.Controls.Add(this.label44);
            this.panel54.Controls.Add(this.label43);
            this.panel54.Controls.Add(this.label4);
            this.panel54.Controls.Add(this.label70);
            this.panel54.Controls.Add(this.lblPesito);
            this.panel54.Controls.Add(this.label23);
            this.panel54.Controls.Add(this.lblValor);
            this.panel54.Controls.Add(this.lblValTerrPriv);
            this.panel54.Controls.Add(this.label51);
            this.panel54.Controls.Add(this.lblValTotCons);
            this.panel54.Controls.Add(this.label24);
            this.panel54.Controls.Add(this.label21);
            this.panel54.Controls.Add(this.lblValTotTerr);
            this.panel54.Controls.Add(this.label22);
            this.panel54.Controls.Add(this.lblValorConsPriv);
            this.panel54.Controls.Add(this.lblValTerrCom);
            this.panel54.Controls.Add(this.lblValConsCom);
            this.panel54.Location = new System.Drawing.Point(-6, 144);
            this.panel54.Name = "panel54";
            this.panel54.Size = new System.Drawing.Size(732, 96);
            this.panel54.TabIndex = 1614;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(616, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 19);
            this.label4.TabIndex = 1584;
            this.label4.Text = "$";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.UseWaitCursor = true;
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.ForeColor = System.Drawing.Color.White;
            this.label70.Location = new System.Drawing.Point(7, 2);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(120, 13);
            this.label70.TabIndex = 941;
            this.label70.Text = "Valores Catastrales:";
            // 
            // lblPesito
            // 
            this.lblPesito.BackColor = System.Drawing.Color.White;
            this.lblPesito.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPesito.ForeColor = System.Drawing.Color.Black;
            this.lblPesito.Location = new System.Drawing.Point(617, 67);
            this.lblPesito.Name = "lblPesito";
            this.lblPesito.Size = new System.Drawing.Size(20, 19);
            this.lblPesito.TabIndex = 1584;
            this.lblPesito.Text = "$";
            this.lblPesito.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPesito.UseWaitCursor = true;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.White;
            this.label23.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(157, 17);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(20, 19);
            this.label23.TabIndex = 1581;
            this.label23.Text = "$";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label23.UseWaitCursor = true;
            // 
            // lblValor
            // 
            this.lblValor.BackColor = System.Drawing.Color.White;
            this.lblValor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValor.Location = new System.Drawing.Point(616, 64);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(108, 24);
            this.lblValor.TabIndex = 1556;
            this.lblValor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblValor.Click += new System.EventHandler(this.lblValor_Click);
            // 
            // lblValTerrPriv
            // 
            this.lblValTerrPriv.BackColor = System.Drawing.Color.White;
            this.lblValTerrPriv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblValTerrPriv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValTerrPriv.Location = new System.Drawing.Point(157, 14);
            this.lblValTerrPriv.Name = "lblValTerrPriv";
            this.lblValTerrPriv.Size = new System.Drawing.Size(108, 24);
            this.lblValTerrPriv.TabIndex = 1548;
            this.lblValTerrPriv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label51
            // 
            this.label51.BackColor = System.Drawing.Color.White;
            this.label51.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.Black;
            this.label51.Location = new System.Drawing.Point(617, 17);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(20, 19);
            this.label51.TabIndex = 1583;
            this.label51.Text = "$";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label51.UseWaitCursor = true;
            // 
            // lblValTotCons
            // 
            this.lblValTotCons.BackColor = System.Drawing.Color.White;
            this.lblValTotCons.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblValTotCons.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValTotCons.Location = new System.Drawing.Point(616, 39);
            this.lblValTotCons.Name = "lblValTotCons";
            this.lblValTotCons.Size = new System.Drawing.Size(108, 24);
            this.lblValTotCons.TabIndex = 1555;
            this.lblValTotCons.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.White;
            this.label24.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Location = new System.Drawing.Point(372, 17);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(20, 19);
            this.label24.TabIndex = 1582;
            this.label24.Text = "$";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label24.UseWaitCursor = true;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.White;
            this.label21.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(157, 42);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(20, 19);
            this.label21.TabIndex = 1090;
            this.label21.Text = "$";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label21.UseWaitCursor = true;
            // 
            // lblValTotTerr
            // 
            this.lblValTotTerr.BackColor = System.Drawing.Color.White;
            this.lblValTotTerr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblValTotTerr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValTotTerr.Location = new System.Drawing.Point(616, 14);
            this.lblValTotTerr.Name = "lblValTotTerr";
            this.lblValTotTerr.Size = new System.Drawing.Size(108, 24);
            this.lblValTotTerr.TabIndex = 1550;
            this.lblValTotTerr.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.White;
            this.label22.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(372, 42);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(20, 19);
            this.label22.TabIndex = 1580;
            this.label22.Text = "$";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label22.UseWaitCursor = true;
            // 
            // lblValorConsPriv
            // 
            this.lblValorConsPriv.BackColor = System.Drawing.Color.White;
            this.lblValorConsPriv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblValorConsPriv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorConsPriv.Location = new System.Drawing.Point(157, 39);
            this.lblValorConsPriv.Name = "lblValorConsPriv";
            this.lblValorConsPriv.Size = new System.Drawing.Size(108, 24);
            this.lblValorConsPriv.TabIndex = 1549;
            this.lblValorConsPriv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblValTerrCom
            // 
            this.lblValTerrCom.BackColor = System.Drawing.Color.White;
            this.lblValTerrCom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblValTerrCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValTerrCom.Location = new System.Drawing.Point(371, 14);
            this.lblValTerrCom.Name = "lblValTerrCom";
            this.lblValTerrCom.Size = new System.Drawing.Size(108, 24);
            this.lblValTerrCom.TabIndex = 1553;
            this.lblValTerrCom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblValConsCom
            // 
            this.lblValConsCom.BackColor = System.Drawing.Color.White;
            this.lblValConsCom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblValConsCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValConsCom.Location = new System.Drawing.Point(371, 39);
            this.lblValConsCom.Name = "lblValConsCom";
            this.lblValConsCom.Size = new System.Drawing.Size(108, 24);
            this.lblValConsCom.TabIndex = 1554;
            this.lblValConsCom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel53
            // 
            this.panel53.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel53.Controls.Add(this.label92);
            this.panel53.Controls.Add(this.label89);
            this.panel53.Controls.Add(this.label7);
            this.panel53.Controls.Add(this.label11);
            this.panel53.Controls.Add(this.label19);
            this.panel53.Controls.Add(this.label40);
            this.panel53.Controls.Add(this.lblConstTot);
            this.panel53.Controls.Add(this.lblTerrenoTot);
            this.panel53.Controls.Add(this.label48);
            this.panel53.Controls.Add(this.lblSupTerrPriv);
            this.panel53.Controls.Add(this.lblSupConsPriv);
            this.panel53.Controls.Add(this.lblSupTerrComun);
            this.panel53.Controls.Add(this.lblSupConsCom);
            this.panel53.Location = new System.Drawing.Point(-6, 73);
            this.panel53.Name = "panel53";
            this.panel53.Size = new System.Drawing.Size(732, 72);
            this.panel53.TabIndex = 1613;
            this.panel53.Paint += new System.Windows.Forms.PaintEventHandler(this.panel53_Paint);
            // 
            // lblConstTot
            // 
            this.lblConstTot.BackColor = System.Drawing.Color.White;
            this.lblConstTot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblConstTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConstTot.Location = new System.Drawing.Point(616, 38);
            this.lblConstTot.Name = "lblConstTot";
            this.lblConstTot.Size = new System.Drawing.Size(108, 24);
            this.lblConstTot.TabIndex = 1568;
            this.lblConstTot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTerrenoTot
            // 
            this.lblTerrenoTot.BackColor = System.Drawing.Color.White;
            this.lblTerrenoTot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblTerrenoTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTerrenoTot.Location = new System.Drawing.Point(616, 11);
            this.lblTerrenoTot.Name = "lblTerrenoTot";
            this.lblTerrenoTot.Size = new System.Drawing.Size(108, 24);
            this.lblTerrenoTot.TabIndex = 1566;
            this.lblTerrenoTot.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.ForeColor = System.Drawing.Color.White;
            this.label48.Location = new System.Drawing.Point(7, 1);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(97, 13);
            this.label48.TabIndex = 941;
            this.label48.Text = "Superficie (M2):";
            // 
            // lblSupTerrPriv
            // 
            this.lblSupTerrPriv.BackColor = System.Drawing.Color.White;
            this.lblSupTerrPriv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSupTerrPriv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupTerrPriv.Location = new System.Drawing.Point(157, 11);
            this.lblSupTerrPriv.Name = "lblSupTerrPriv";
            this.lblSupTerrPriv.Size = new System.Drawing.Size(108, 24);
            this.lblSupTerrPriv.TabIndex = 1546;
            this.lblSupTerrPriv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupConsPriv
            // 
            this.lblSupConsPriv.BackColor = System.Drawing.Color.White;
            this.lblSupConsPriv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSupConsPriv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupConsPriv.Location = new System.Drawing.Point(157, 38);
            this.lblSupConsPriv.Name = "lblSupConsPriv";
            this.lblSupConsPriv.Size = new System.Drawing.Size(108, 24);
            this.lblSupConsPriv.TabIndex = 1547;
            this.lblSupConsPriv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupTerrComun
            // 
            this.lblSupTerrComun.BackColor = System.Drawing.Color.White;
            this.lblSupTerrComun.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSupTerrComun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupTerrComun.Location = new System.Drawing.Point(372, 11);
            this.lblSupTerrComun.Name = "lblSupTerrComun";
            this.lblSupTerrComun.Size = new System.Drawing.Size(108, 24);
            this.lblSupTerrComun.TabIndex = 1551;
            this.lblSupTerrComun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupConsCom
            // 
            this.lblSupConsCom.BackColor = System.Drawing.Color.White;
            this.lblSupConsCom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSupConsCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupConsCom.Location = new System.Drawing.Point(372, 38);
            this.lblSupConsCom.Name = "lblSupConsCom";
            this.lblSupConsCom.Size = new System.Drawing.Size(108, 24);
            this.lblSupConsCom.TabIndex = 1552;
            this.lblSupConsCom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnMaps
            // 
            this.btnMaps.AutoSize = true;
            this.btnMaps.BackColor = System.Drawing.Color.White;
            this.btnMaps.BackgroundImage = global::SMACatastro.Properties.Resources.mapa__1_;
            this.btnMaps.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMaps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaps.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnMaps.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaps.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMaps.Location = new System.Drawing.Point(696, -1);
            this.btnMaps.Name = "btnMaps";
            this.btnMaps.Size = new System.Drawing.Size(23, 22);
            this.btnMaps.TabIndex = 1610;
            this.btnMaps.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnMaps.UseVisualStyleBackColor = false;
            this.btnMaps.Click += new System.EventHandler(this.btnMaps_Click);
            this.btnMaps.MouseHover += new System.EventHandler(this.btnMaps_MouseHover);
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(-9, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(157, 21);
            this.label17.TabIndex = 1597;
            this.label17.Text = "Domicilio:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDomicilio
            // 
            this.lblDomicilio.BackColor = System.Drawing.Color.White;
            this.lblDomicilio.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblDomicilio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDomicilio.Location = new System.Drawing.Point(151, 49);
            this.lblDomicilio.Name = "lblDomicilio";
            this.lblDomicilio.Size = new System.Drawing.Size(568, 24);
            this.lblDomicilio.TabIndex = 1595;
            this.lblDomicilio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitular
            // 
            this.lblTitular.BackColor = System.Drawing.Color.White;
            this.lblTitular.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblTitular.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTitular.Location = new System.Drawing.Point(151, 24);
            this.lblTitular.Name = "lblTitular";
            this.lblTitular.Size = new System.Drawing.Size(568, 24);
            this.lblTitular.TabIndex = 1543;
            this.lblTitular.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.White;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(614, 190);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(0, 13);
            this.label15.TabIndex = 1542;
            // 
            // lblCiudadano
            // 
            this.lblCiudadano.AutoSize = true;
            this.lblCiudadano.BackColor = System.Drawing.Color.White;
            this.lblCiudadano.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblCiudadano.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCiudadano.Location = new System.Drawing.Point(152, 25);
            this.lblCiudadano.Name = "lblCiudadano";
            this.lblCiudadano.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblCiudadano.Size = new System.Drawing.Size(0, 13);
            this.lblCiudadano.TabIndex = 1541;
            this.lblCiudadano.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(51, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 21);
            this.label12.TabIndex = 1218;
            this.label12.Text = "Propietario:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel4.Location = new System.Drawing.Point(7, 18);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(212, 2);
            this.panel4.TabIndex = 1214;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(1, -1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(218, 17);
            this.label13.TabIndex = 1213;
            this.label13.Text = "INFORMACIÓN CATASTRAL";
            // 
            // lblLongitud
            // 
            this.lblLongitud.BackColor = System.Drawing.Color.White;
            this.lblLongitud.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblLongitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLongitud.Location = new System.Drawing.Point(397, 6);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(202, 23);
            this.lblLongitud.TabIndex = 1607;
            this.lblLongitud.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLatitud
            // 
            this.lblLatitud.BackColor = System.Drawing.Color.White;
            this.lblLatitud.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblLatitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLatitud.Location = new System.Drawing.Point(66, 7);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(202, 23);
            this.lblLatitud.TabIndex = 1606;
            this.lblLatitud.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(-5, 9);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 21);
            this.label14.TabIndex = 1608;
            this.label14.Text = "Latitud:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(309, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(86, 21);
            this.label16.TabIndex = 1609;
            this.label16.Text = "Longitud:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel3.Controls.Add(this.lblUsuario);
            this.panel3.Controls.Add(this.lbFecha);
            this.panel3.Controls.Add(this.lblHora);
            this.panel3.Location = new System.Drawing.Point(-2, 692);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1370, 28);
            this.panel3.TabIndex = 1696;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(15, 6);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(61, 16);
            this.lblUsuario.TabIndex = 8;
            this.lblUsuario.Text = "Usuario";
            // 
            // lbFecha
            // 
            this.lbFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFecha.AutoSize = true;
            this.lbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFecha.ForeColor = System.Drawing.Color.White;
            this.lbFecha.Location = new System.Drawing.Point(1030, 6);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(212, 16);
            this.lbFecha.TabIndex = 7;
            this.lbFecha.Text = "Lunes, 26 de septiembre 2018";
            // 
            // lblHora
            // 
            this.lblHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.Color.White;
            this.lblHora.Location = new System.Drawing.Point(1258, 5);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(95, 15);
            this.lblHora.TabIndex = 6;
            this.lblHora.Text = "21:49:45 p.m.";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel2.Location = new System.Drawing.Point(0, -115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(7, 950);
            this.panel2.TabIndex = 1695;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnConsulta
            // 
            this.btnConsulta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnConsulta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsulta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnConsulta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnConsulta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsulta.ForeColor = System.Drawing.Color.White;
            this.btnConsulta.Location = new System.Drawing.Point(982, 75);
            this.btnConsulta.Name = "btnConsulta";
            this.btnConsulta.Size = new System.Drawing.Size(122, 23);
            this.btnConsulta.TabIndex = 1680;
            this.btnConsulta.Text = "CONSULTA";
            this.btnConsulta.UseVisualStyleBackColor = false;
            this.btnConsulta.Click += new System.EventHandler(this.btnConsulta_Click);
            this.btnConsulta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnConsulta_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(902, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 18);
            this.label8.TabIndex = 1678;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(181)))), ((int)(((byte)(17)))));
            this.panel5.Controls.Add(this.pbxQR2);
            this.panel5.Controls.Add(this.pbxQR);
            this.panel5.Location = new System.Drawing.Point(393, 136);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(865, 3);
            this.panel5.TabIndex = 1675;
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel5_Paint);
            // 
            // pbxQR2
            // 
            this.pbxQR2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxQR2.Location = new System.Drawing.Point(549, -35);
            this.pbxQR2.Name = "pbxQR2";
            this.pbxQR2.Size = new System.Drawing.Size(82, 60);
            this.pbxQR2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxQR2.TabIndex = 1312;
            this.pbxQR2.TabStop = false;
            this.pbxQR2.Visible = false;
            // 
            // pbxQR
            // 
            this.pbxQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxQR.Location = new System.Drawing.Point(471, -35);
            this.pbxQR.Name = "pbxQR";
            this.pbxQR.Size = new System.Drawing.Size(82, 60);
            this.pbxQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxQR.TabIndex = 1311;
            this.pbxQR.TabStop = false;
            this.pbxQR.Visible = false;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(14)))), ((int)(((byte)(108)))));
            this.panel6.Location = new System.Drawing.Point(407, 120);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(846, 3);
            this.panel6.TabIndex = 1671;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(110)))), ((int)(((byte)(191)))));
            this.panel7.Location = new System.Drawing.Point(406, 124);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(861, 3);
            this.panel7.TabIndex = 1672;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(192)))), ((int)(((byte)(20)))));
            this.panel9.Location = new System.Drawing.Point(406, 128);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(873, 3);
            this.panel9.TabIndex = 1673;
            this.panel9.Paint += new System.Windows.Forms.PaintEventHandler(this.panel9_Paint);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(16)))), ((int)(((byte)(44)))));
            this.panel8.Location = new System.Drawing.Point(407, 132);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(883, 3);
            this.panel8.TabIndex = 1674;
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel8_Paint);
            // 
            // btnCancela
            // 
            this.btnCancela.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnCancela.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancela.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnCancela.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnCancela.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancela.Font = new System.Drawing.Font("Webdings", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnCancela.ForeColor = System.Drawing.Color.White;
            this.btnCancela.Location = new System.Drawing.Point(1209, 50);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(72, 64);
            this.btnCancela.TabIndex = 1669;
            this.btnCancela.Text = "r";
            this.btnCancela.UseVisualStyleBackColor = false;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            this.btnCancela.MouseHover += new System.EventHandler(this.btnCancela_MouseHover);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel1.Location = new System.Drawing.Point(1359, -115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(7, 950);
            this.panel1.TabIndex = 1703;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // PanelBarraTitulo
            // 
            this.PanelBarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.PanelBarraTitulo.Controls.Add(this.label2);
            this.PanelBarraTitulo.Controls.Add(this.btnMinimizar);
            this.PanelBarraTitulo.Controls.Add(this.btnMaximizar);
            this.PanelBarraTitulo.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.PanelBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelBarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.PanelBarraTitulo.Name = "PanelBarraTitulo";
            this.PanelBarraTitulo.Size = new System.Drawing.Size(1366, 43);
            this.PanelBarraTitulo.TabIndex = 1704;
            this.PanelBarraTitulo.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBarraTitulo_Paint);
            this.PanelBarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelBarraTitulo_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("High Tower Text", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(774, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(547, 31);
            this.label2.TabIndex = 12;
            this.label2.Text = "AUTORIZA VENTANILLA - CATASTRO";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Aqua;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Image = global::SMACatastro.Properties.Resources.Minimize;
            this.btnMinimizar.Location = new System.Drawing.Point(1321, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(43, 43);
            this.btnMinimizar.TabIndex = 8;
            this.btnMinimizar.UseVisualStyleBackColor = true;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnMaximizar
            // 
            this.btnMaximizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaximizar.FlatAppearance.BorderSize = 0;
            this.btnMaximizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(46)))), ((int)(((byte)(47)))));
            this.btnMaximizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximizar.Location = new System.Drawing.Point(1260, -7);
            this.btnMaximizar.Name = "btnMaximizar";
            this.btnMaximizar.Size = new System.Drawing.Size(43, 43);
            this.btnMaximizar.TabIndex = 9;
            this.btnMaximizar.UseVisualStyleBackColor = true;
            // 
            // gMapControl1
            // 
            this.gMapControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(7, 32);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(592, 344);
            this.gMapControl1.TabIndex = 1705;
            this.gMapControl1.Zoom = 0D;
            // 
            // tmFechaHora
            // 
            this.tmFechaHora.Enabled = true;
            this.tmFechaHora.Tick += new System.EventHandler(this.tmFechaHora_Tick);
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.panel15.Controls.Add(this.gMapControl1);
            this.panel15.Controls.Add(this.lblLongitud);
            this.panel15.Controls.Add(this.lblLatitud);
            this.panel15.Controls.Add(this.label14);
            this.panel15.Controls.Add(this.label16);
            this.panel15.Location = new System.Drawing.Point(746, 145);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(607, 381);
            this.panel15.TabIndex = 1705;
            // 
            // lblMun
            // 
            this.lblMun.AutoSize = true;
            this.lblMun.BackColor = System.Drawing.Color.White;
            this.lblMun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMun.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMun.Location = new System.Drawing.Point(571, 75);
            this.lblMun.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMun.Name = "lblMun";
            this.lblMun.Size = new System.Drawing.Size(37, 20);
            this.lblMun.TabIndex = 1728;
            this.lblMun.Text = "041";
            // 
            // txtEdificio
            // 
            this.txtEdificio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEdificio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEdificio.Location = new System.Drawing.Point(736, 74);
            this.txtEdificio.MaxLength = 2;
            this.txtEdificio.Name = "txtEdificio";
            this.txtEdificio.Size = new System.Drawing.Size(43, 22);
            this.txtEdificio.TabIndex = 1714;
            this.txtEdificio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEdificio.TextChanged += new System.EventHandler(this.txtEdificio_TextChanged);
            this.txtEdificio.Enter += new System.EventHandler(this.txtEdificio_Enter);
            this.txtEdificio.Leave += new System.EventHandler(this.txtEdificio_Leave);
            // 
            // txtDepto
            // 
            this.txtDepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepto.Location = new System.Drawing.Point(785, 74);
            this.txtDepto.MaxLength = 4;
            this.txtDepto.Name = "txtDepto";
            this.txtDepto.Size = new System.Drawing.Size(75, 22);
            this.txtDepto.TabIndex = 1715;
            this.txtDepto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDepto.TextChanged += new System.EventHandler(this.txtDepto_TextChanged);
            this.txtDepto.Enter += new System.EventHandler(this.txtDepto_Enter);
            this.txtDepto.Leave += new System.EventHandler(this.txtDepto_Leave);
            // 
            // txtLote
            // 
            this.txtLote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLote.Location = new System.Drawing.Point(698, 74);
            this.txtLote.MaxLength = 2;
            this.txtLote.Name = "txtLote";
            this.txtLote.Size = new System.Drawing.Size(32, 22);
            this.txtLote.TabIndex = 1713;
            this.txtLote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLote.TextChanged += new System.EventHandler(this.txtLote_TextChanged);
            this.txtLote.Enter += new System.EventHandler(this.txtLote_Enter);
            this.txtLote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLote_KeyPress_1);
            this.txtLote.Leave += new System.EventHandler(this.txtLote_Leave);
            // 
            // txtManzana
            // 
            this.txtManzana.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtManzana.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManzana.Location = new System.Drawing.Point(650, 74);
            this.txtManzana.MaxLength = 3;
            this.txtManzana.Name = "txtManzana";
            this.txtManzana.Size = new System.Drawing.Size(41, 22);
            this.txtManzana.TabIndex = 1712;
            this.txtManzana.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtManzana.TextChanged += new System.EventHandler(this.txtManzana_TextChanged);
            this.txtManzana.Enter += new System.EventHandler(this.txtManzana_Enter);
            this.txtManzana.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtManzana_KeyPress_1);
            this.txtManzana.Leave += new System.EventHandler(this.txtManzana_Leave);
            // 
            // txtZona
            // 
            this.txtZona.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZona.Location = new System.Drawing.Point(613, 74);
            this.txtZona.MaxLength = 2;
            this.txtZona.Name = "txtZona";
            this.txtZona.Size = new System.Drawing.Size(32, 22);
            this.txtZona.TabIndex = 1711;
            this.txtZona.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtZona.TextChanged += new System.EventHandler(this.txtZona_TextChanged);
            this.txtZona.Enter += new System.EventHandler(this.txtZona_Enter);
            this.txtZona.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtZona_KeyPress_1);
            this.txtZona.Leave += new System.EventHandler(this.txtZona_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(778, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(8, 11);
            this.label3.TabIndex = 1727;
            this.label3.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(730, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(8, 11);
            this.label5.TabIndex = 1726;
            this.label5.Text = "-";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(692, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(8, 11);
            this.label9.TabIndex = 1725;
            this.label9.Text = "-";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(644, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(8, 11);
            this.label10.TabIndex = 1724;
            this.label10.Text = "-";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(607, 80);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(8, 11);
            this.label18.TabIndex = 1723;
            this.label18.Text = "-";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(806, 99);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(35, 12);
            this.label25.TabIndex = 1722;
            this.label25.Text = "Depto";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(736, 99);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(43, 12);
            this.label26.TabIndex = 1721;
            this.label26.Text = "Edificio";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.label28.Location = new System.Drawing.Point(702, 99);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(27, 12);
            this.label28.TabIndex = 1720;
            this.label28.Text = "Lote";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.label29.Location = new System.Drawing.Point(648, 99);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(51, 12);
            this.label29.TabIndex = 1719;
            this.label29.Text = "Manzana";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.label30.Location = new System.Drawing.Point(616, 99);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(30, 12);
            this.label30.TabIndex = 1718;
            this.label30.Text = "Zona";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold);
            this.label31.Location = new System.Drawing.Point(562, 99);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(54, 12);
            this.label31.TabIndex = 1717;
            this.label31.Text = "Municipio";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(415, 77);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(152, 16);
            this.label32.TabIndex = 1716;
            this.label32.Text = "CLAVE CATASTRAL:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(866, 99);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(31, 12);
            this.label33.TabIndex = 1710;
            this.label33.Text = "Serie";
            // 
            // CBO_SERIE
            // 
            this.CBO_SERIE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBO_SERIE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBO_SERIE.FormattingEnabled = true;
            this.CBO_SERIE.Items.AddRange(new object[] {
            "A"});
            this.CBO_SERIE.Location = new System.Drawing.Point(862, 75);
            this.CBO_SERIE.Name = "CBO_SERIE";
            this.CBO_SERIE.Size = new System.Drawing.Size(45, 21);
            this.CBO_SERIE.TabIndex = 1709;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(920, 99);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(45, 12);
            this.label34.TabIndex = 1708;
            this.label34.Text = "Folio C.";
            // 
            // TXT_FOLIO
            // 
            this.TXT_FOLIO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TXT_FOLIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_FOLIO.Location = new System.Drawing.Point(909, 74);
            this.TXT_FOLIO.MaxLength = 8;
            this.TXT_FOLIO.Name = "TXT_FOLIO";
            this.TXT_FOLIO.Size = new System.Drawing.Size(65, 22);
            this.TXT_FOLIO.TabIndex = 1707;
            this.TXT_FOLIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TXT_FOLIO.Enter += new System.EventHandler(this.TXT_FOLIO_Enter);
            this.TXT_FOLIO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT_FOLIO_KeyPress);
            this.TXT_FOLIO.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TXT_FOLIO_KeyUp);
            this.TXT_FOLIO.Leave += new System.EventHandler(this.TXT_FOLIO_Leave);
            // 
            // pnlAlta
            // 
            this.pnlAlta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.pnlAlta.Controls.Add(this.btnMapsA);
            this.pnlAlta.Controls.Add(this.lblRegimenA);
            this.pnlAlta.Controls.Add(this.lblFondoA);
            this.pnlAlta.Controls.Add(this.lblAreaA);
            this.pnlAlta.Controls.Add(this.lblSupConsCA);
            this.pnlAlta.Controls.Add(this.lblSupTerrCA);
            this.pnlAlta.Controls.Add(this.lblFrenteA);
            this.pnlAlta.Controls.Add(this.lbldesA);
            this.pnlAlta.Controls.Add(this.lblSupConsA);
            this.pnlAlta.Controls.Add(this.lblSupTerrenoA);
            this.pnlAlta.Controls.Add(this.lblCalleA);
            this.pnlAlta.Controls.Add(this.lblCodCalle);
            this.pnlAlta.Controls.Add(this.lblZonaA);
            this.pnlAlta.Controls.Add(this.label49);
            this.pnlAlta.Controls.Add(this.label50);
            this.pnlAlta.Controls.Add(this.label52);
            this.pnlAlta.Controls.Add(this.label53);
            this.pnlAlta.Controls.Add(this.cboUbicacion);
            this.pnlAlta.Controls.Add(this.label54);
            this.pnlAlta.Controls.Add(this.label55);
            this.pnlAlta.Controls.Add(this.panel17);
            this.pnlAlta.Controls.Add(this.label56);
            this.pnlAlta.Controls.Add(this.label57);
            this.pnlAlta.Controls.Add(this.label58);
            this.pnlAlta.Controls.Add(this.label59);
            this.pnlAlta.Controls.Add(this.label60);
            this.pnlAlta.Controls.Add(this.label61);
            this.pnlAlta.Controls.Add(this.label62);
            this.pnlAlta.Location = new System.Drawing.Point(13, 448);
            this.pnlAlta.Name = "pnlAlta";
            this.pnlAlta.Size = new System.Drawing.Size(728, 236);
            this.pnlAlta.TabIndex = 1729;
            // 
            // btnMapsA
            // 
            this.btnMapsA.AutoSize = true;
            this.btnMapsA.BackColor = System.Drawing.Color.White;
            this.btnMapsA.BackgroundImage = global::SMACatastro.Properties.Resources.mapa__1_;
            this.btnMapsA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMapsA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMapsA.Enabled = false;
            this.btnMapsA.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnMapsA.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnMapsA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMapsA.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMapsA.Location = new System.Drawing.Point(688, 0);
            this.btnMapsA.Name = "btnMapsA";
            this.btnMapsA.Size = new System.Drawing.Size(23, 22);
            this.btnMapsA.TabIndex = 1611;
            this.btnMapsA.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnMapsA.UseVisualStyleBackColor = false;
            this.btnMapsA.Click += new System.EventHandler(this.btnMapsA_Click);
            // 
            // lblRegimenA
            // 
            this.lblRegimenA.BackColor = System.Drawing.Color.White;
            this.lblRegimenA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblRegimenA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRegimenA.Location = new System.Drawing.Point(151, 86);
            this.lblRegimenA.Name = "lblRegimenA";
            this.lblRegimenA.Size = new System.Drawing.Size(163, 22);
            this.lblRegimenA.TabIndex = 1598;
            this.lblRegimenA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFondoA
            // 
            this.lblFondoA.BackColor = System.Drawing.Color.White;
            this.lblFondoA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFondoA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFondoA.Location = new System.Drawing.Point(558, 179);
            this.lblFondoA.Name = "lblFondoA";
            this.lblFondoA.Size = new System.Drawing.Size(163, 22);
            this.lblFondoA.TabIndex = 1596;
            this.lblFondoA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAreaA
            // 
            this.lblAreaA.BackColor = System.Drawing.Color.White;
            this.lblAreaA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblAreaA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAreaA.Location = new System.Drawing.Point(558, 156);
            this.lblAreaA.Name = "lblAreaA";
            this.lblAreaA.Size = new System.Drawing.Size(163, 22);
            this.lblAreaA.TabIndex = 1595;
            this.lblAreaA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSupConsCA
            // 
            this.lblSupConsCA.BackColor = System.Drawing.Color.White;
            this.lblSupConsCA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupConsCA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupConsCA.Location = new System.Drawing.Point(558, 133);
            this.lblSupConsCA.Name = "lblSupConsCA";
            this.lblSupConsCA.Size = new System.Drawing.Size(163, 22);
            this.lblSupConsCA.TabIndex = 1594;
            this.lblSupConsCA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSupTerrCA
            // 
            this.lblSupTerrCA.BackColor = System.Drawing.Color.White;
            this.lblSupTerrCA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupTerrCA.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupTerrCA.Location = new System.Drawing.Point(558, 110);
            this.lblSupTerrCA.Name = "lblSupTerrCA";
            this.lblSupTerrCA.Size = new System.Drawing.Size(163, 22);
            this.lblSupTerrCA.TabIndex = 1593;
            this.lblSupTerrCA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFrenteA
            // 
            this.lblFrenteA.BackColor = System.Drawing.Color.White;
            this.lblFrenteA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblFrenteA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblFrenteA.Location = new System.Drawing.Point(151, 178);
            this.lblFrenteA.Name = "lblFrenteA";
            this.lblFrenteA.Size = new System.Drawing.Size(163, 22);
            this.lblFrenteA.TabIndex = 1592;
            this.lblFrenteA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbldesA
            // 
            this.lbldesA.BackColor = System.Drawing.Color.White;
            this.lbldesA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbldesA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbldesA.Location = new System.Drawing.Point(151, 155);
            this.lbldesA.Name = "lbldesA";
            this.lbldesA.Size = new System.Drawing.Size(163, 22);
            this.lbldesA.TabIndex = 1591;
            this.lbldesA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupConsA
            // 
            this.lblSupConsA.BackColor = System.Drawing.Color.White;
            this.lblSupConsA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSupConsA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSupConsA.Location = new System.Drawing.Point(151, 132);
            this.lblSupConsA.Name = "lblSupConsA";
            this.lblSupConsA.Size = new System.Drawing.Size(163, 22);
            this.lblSupConsA.TabIndex = 1590;
            this.lblSupConsA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupTerrenoA
            // 
            this.lblSupTerrenoA.BackColor = System.Drawing.Color.White;
            this.lblSupTerrenoA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblSupTerrenoA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSupTerrenoA.Location = new System.Drawing.Point(151, 109);
            this.lblSupTerrenoA.Name = "lblSupTerrenoA";
            this.lblSupTerrenoA.Size = new System.Drawing.Size(163, 22);
            this.lblSupTerrenoA.TabIndex = 1589;
            this.lblSupTerrenoA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCalleA
            // 
            this.lblCalleA.BackColor = System.Drawing.Color.White;
            this.lblCalleA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblCalleA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCalleA.Location = new System.Drawing.Point(185, 61);
            this.lblCalleA.Name = "lblCalleA";
            this.lblCalleA.Size = new System.Drawing.Size(536, 24);
            this.lblCalleA.TabIndex = 1588;
            this.lblCalleA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCodCalle
            // 
            this.lblCodCalle.BackColor = System.Drawing.Color.White;
            this.lblCodCalle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblCodCalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCodCalle.Location = new System.Drawing.Point(151, 61);
            this.lblCodCalle.Name = "lblCodCalle";
            this.lblCodCalle.Size = new System.Drawing.Size(32, 24);
            this.lblCodCalle.TabIndex = 1587;
            this.lblCodCalle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblZonaA
            // 
            this.lblZonaA.BackColor = System.Drawing.Color.White;
            this.lblZonaA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblZonaA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblZonaA.Location = new System.Drawing.Point(151, 35);
            this.lblZonaA.Name = "lblZonaA";
            this.lblZonaA.Size = new System.Drawing.Size(32, 24);
            this.lblZonaA.TabIndex = 1586;
            this.lblZonaA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.ForeColor = System.Drawing.Color.White;
            this.label49.Location = new System.Drawing.Point(482, 159);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(76, 17);
            this.label49.TabIndex = 1229;
            this.label49.Text = "Area Insc.";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.ForeColor = System.Drawing.Color.White;
            this.label50.Location = new System.Drawing.Point(76, 158);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(71, 17);
            this.label50.TabIndex = 1227;
            this.label50.Text = "Desnivel:";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.ForeColor = System.Drawing.Color.White;
            this.label52.Location = new System.Drawing.Point(398, 136);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(160, 17);
            this.label52.TabIndex = 1225;
            this.label52.Text = "Sup. Construc. Comun:";
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.ForeColor = System.Drawing.Color.White;
            this.label53.Location = new System.Drawing.Point(481, 89);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(77, 17);
            this.label53.TabIndex = 1221;
            this.label53.Text = "Ubicacion:";
            // 
            // cboUbicacion
            // 
            this.cboUbicacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUbicacion.FormattingEnabled = true;
            this.cboUbicacion.Items.AddRange(new object[] {
            "0 SIN DESCRIPCION",
            "1 INTERMEDIO",
            "2 ESQUINERO",
            "3 CABECERO",
            "4 MANZANERO",
            "5 FRENTES NO CONTIGUOS",
            "6 INTERIOR"});
            this.cboUbicacion.Location = new System.Drawing.Point(558, 86);
            this.cboUbicacion.Name = "cboUbicacion";
            this.cboUbicacion.Size = new System.Drawing.Size(163, 23);
            this.cboUbicacion.TabIndex = 105;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.ForeColor = System.Drawing.Color.White;
            this.label54.Location = new System.Drawing.Point(99, 65);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(47, 17);
            this.label54.TabIndex = 1218;
            this.label54.Text = "Calle:";
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.White;
            this.label55.Location = new System.Drawing.Point(504, 182);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(54, 17);
            this.label55.TabIndex = 1216;
            this.label55.Text = "Fondo:";
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.panel17.Location = new System.Drawing.Point(5, 20);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(127, 2);
            this.panel17.TabIndex = 1214;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.White;
            this.label56.Location = new System.Drawing.Point(5, 3);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(127, 17);
            this.label56.TabIndex = 1213;
            this.label56.Text = "DATOS DE ALTA";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ForeColor = System.Drawing.Color.White;
            this.label57.Location = new System.Drawing.Point(90, 183);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(57, 17);
            this.label57.TabIndex = 1212;
            this.label57.Text = "Frente:";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.White;
            this.label58.Location = new System.Drawing.Point(409, 113);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(149, 17);
            this.label58.TabIndex = 1211;
            this.label58.Text = "Sup. Terreno Comun:";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.ForeColor = System.Drawing.Color.White;
            this.label59.Location = new System.Drawing.Point(48, 112);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(98, 17);
            this.label59.TabIndex = 1210;
            this.label59.Text = "Sup. Terreno:";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label60.ForeColor = System.Drawing.Color.White;
            this.label60.Location = new System.Drawing.Point(5, 89);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(142, 17);
            this.label60.TabIndex = 1209;
            this.label60.Text = "Regimen Propiedad:";
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.ForeColor = System.Drawing.Color.White;
            this.label61.Location = new System.Drawing.Point(52, 40);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(95, 17);
            this.label61.TabIndex = 1208;
            this.label61.Text = "Zona Origen:";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.ForeColor = System.Drawing.Color.White;
            this.label62.Location = new System.Drawing.Point(53, 135);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(93, 17);
            this.label62.TabIndex = 1224;
            this.label62.Text = "Sup. Constr.:";
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.pnlBusqueda.Controls.Add(this.DGVRESULTADO);
            this.pnlBusqueda.Controls.Add(this.panel16);
            this.pnlBusqueda.Controls.Add(this.label36);
            this.pnlBusqueda.Location = new System.Drawing.Point(13, 145);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(726, 158);
            this.pnlBusqueda.TabIndex = 1730;
            // 
            // DGVRESULTADO
            // 
            this.DGVRESULTADO.AllowUserToAddRows = false;
            this.DGVRESULTADO.AllowUserToDeleteRows = false;
            this.DGVRESULTADO.AllowUserToOrderColumns = true;
            this.DGVRESULTADO.AllowUserToResizeColumns = false;
            this.DGVRESULTADO.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DGVRESULTADO.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DGVRESULTADO.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.DGVRESULTADO.BackgroundColor = System.Drawing.Color.Gray;
            this.DGVRESULTADO.ColumnHeadersHeight = 29;
            this.DGVRESULTADO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DGVRESULTADO.EnableHeadersVisualStyles = false;
            this.DGVRESULTADO.Location = new System.Drawing.Point(3, 29);
            this.DGVRESULTADO.Margin = new System.Windows.Forms.Padding(2);
            this.DGVRESULTADO.MultiSelect = false;
            this.DGVRESULTADO.Name = "DGVRESULTADO";
            this.DGVRESULTADO.RowHeadersVisible = false;
            this.DGVRESULTADO.RowHeadersWidth = 51;
            this.DGVRESULTADO.RowTemplate.Height = 24;
            this.DGVRESULTADO.Size = new System.Drawing.Size(715, 126);
            this.DGVRESULTADO.TabIndex = 1593;
            this.DGVRESULTADO.DoubleClick += new System.EventHandler(this.DGVRESULTADO_DoubleClick);
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel16.Location = new System.Drawing.Point(3, 22);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(210, 2);
            this.panel16.TabIndex = 1215;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(0, 3);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(189, 17);
            this.label36.TabIndex = 1214;
            this.label36.Text = "PROCESOS PENDIENTES";
            // 
            // btnBuscarClave
            // 
            this.btnBuscarClave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnBuscarClave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarClave.Enabled = false;
            this.btnBuscarClave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnBuscarClave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnBuscarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarClave.ForeColor = System.Drawing.Color.White;
            this.btnBuscarClave.Image = global::SMACatastro.Properties.Resources.lupa_de_busqueda_con_una_cruz_blanco;
            this.btnBuscarClave.Location = new System.Drawing.Point(1137, 50);
            this.btnBuscarClave.Name = "btnBuscarClave";
            this.btnBuscarClave.Size = new System.Drawing.Size(72, 64);
            this.btnBuscarClave.TabIndex = 1706;
            this.btnBuscarClave.UseVisualStyleBackColor = false;
            this.btnBuscarClave.Click += new System.EventHandler(this.btnBuscarClave_Click);
            this.btnBuscarClave.MouseHover += new System.EventHandler(this.btnBuscarClave_MouseHover);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::SMACatastro.Properties.Resources.logo_2025;
            this.pictureBox1.Location = new System.Drawing.Point(13, 50);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(399, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1667;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnSalida
            // 
            this.btnSalida.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnSalida.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSalida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalida.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(181)))), ((int)(((byte)(17)))));
            this.btnSalida.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(181)))), ((int)(((byte)(17)))));
            this.btnSalida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalida.Font = new System.Drawing.Font("Webdings", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnSalida.ForeColor = System.Drawing.Color.White;
            this.btnSalida.Image = global::SMACatastro.Properties.Resources.apagado;
            this.btnSalida.Location = new System.Drawing.Point(1281, 50);
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(72, 64);
            this.btnSalida.TabIndex = 1668;
            this.btnSalida.UseVisualStyleBackColor = false;
            this.btnSalida.Click += new System.EventHandler(this.btnSalida_Click);
            this.btnSalida.MouseHover += new System.EventHandler(this.btnSalida_MouseHover);
            // 
            // label92
            // 
            this.label92.BackColor = System.Drawing.Color.Transparent;
            this.label92.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.ForeColor = System.Drawing.Color.White;
            this.label92.Location = new System.Drawing.Point(512, 42);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(98, 17);
            this.label92.TabIndex = 1574;
            this.label92.Text = "Constr. Total:";
            this.label92.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label89
            // 
            this.label89.BackColor = System.Drawing.Color.Transparent;
            this.label89.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.ForeColor = System.Drawing.Color.White;
            this.label89.Location = new System.Drawing.Point(512, 15);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(98, 17);
            this.label89.TabIndex = 1573;
            this.label89.Text = "Terr. Total:";
            this.label89.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(59, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 17);
            this.label7.TabIndex = 1569;
            this.label7.Text = "Terr. Priv.:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(269, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 17);
            this.label11.TabIndex = 1570;
            this.label11.Text = "Terr. Com.:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(59, 42);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(93, 17);
            this.label19.TabIndex = 1571;
            this.label19.Text = "Const. Priv.:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            this.label40.BackColor = System.Drawing.Color.Transparent;
            this.label40.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.ForeColor = System.Drawing.Color.White;
            this.label40.Location = new System.Drawing.Point(269, 42);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(98, 17);
            this.label40.TabIndex = 1572;
            this.label40.Text = "Const. Com.:";
            this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(515, 43);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(98, 17);
            this.label27.TabIndex = 1591;
            this.label27.Text = "Const. Total:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.Transparent;
            this.label35.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(515, 18);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(98, 17);
            this.label35.TabIndex = 1590;
            this.label35.Text = "Terr. Total:";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label41
            // 
            this.label41.BackColor = System.Drawing.Color.Transparent;
            this.label41.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ForeColor = System.Drawing.Color.White;
            this.label41.Location = new System.Drawing.Point(60, 18);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(93, 17);
            this.label41.TabIndex = 1585;
            this.label41.Text = "Terr. Priv.:";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label45
            // 
            this.label45.BackColor = System.Drawing.Color.Transparent;
            this.label45.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.ForeColor = System.Drawing.Color.White;
            this.label45.Location = new System.Drawing.Point(487, 68);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(125, 17);
            this.label45.TabIndex = 1589;
            this.label45.Text = "Valor Catastral:";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label42
            // 
            this.label42.BackColor = System.Drawing.Color.Transparent;
            this.label42.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.ForeColor = System.Drawing.Color.White;
            this.label42.Location = new System.Drawing.Point(60, 43);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(93, 17);
            this.label42.TabIndex = 1586;
            this.label42.Text = "Const. Priv.:";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label44
            // 
            this.label44.BackColor = System.Drawing.Color.Transparent;
            this.label44.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.ForeColor = System.Drawing.Color.White;
            this.label44.Location = new System.Drawing.Point(271, 18);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(98, 17);
            this.label44.TabIndex = 1587;
            this.label44.Text = "Terr. Com.:";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label43
            // 
            this.label43.BackColor = System.Drawing.Color.Transparent;
            this.label43.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.ForeColor = System.Drawing.Color.White;
            this.label43.Location = new System.Drawing.Point(271, 43);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(98, 17);
            this.label43.TabIndex = 1588;
            this.label43.Text = "Const. Com.:";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmVentanilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 720);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.lblMun);
            this.Controls.Add(this.txtEdificio);
            this.Controls.Add(this.txtDepto);
            this.Controls.Add(this.txtLote);
            this.Controls.Add(this.txtManzana);
            this.Controls.Add(this.txtZona);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.CBO_SERIE);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.TXT_FOLIO);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.btnBuscarClave);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.PanelBarraTitulo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnConsulta);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.btnSalida);
            this.Controls.Add(this.btnCancela);
            this.Controls.Add(this.pnlDatosPredio);
            this.Controls.Add(this.pnlAlta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVentanilla";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "z";
            this.Activated += new System.EventHandler(this.frmBloqueoDesbloqueo_Activated);
            this.Load += new System.EventHandler(this.frmBloqueoDesbloqueo_Load);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.pnlCambios.ResumeLayout(false);
            this.pnlCambios.PerformLayout();
            this.pnlAltaInfo.ResumeLayout(false);
            this.pnlCertificado.ResumeLayout(false);
            this.pnlDatosPredio.ResumeLayout(false);
            this.pnlDatosPredio.PerformLayout();
            this.panel54.ResumeLayout(false);
            this.panel54.PerformLayout();
            this.panel53.ResumeLayout(false);
            this.panel53.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxQR2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxQR)).EndInit();
            this.PanelBarraTitulo.ResumeLayout(false);
            this.PanelBarraTitulo.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.pnlAlta.ResumeLayout(false);
            this.pnlAlta.PerformLayout();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVRESULTADO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label lblOperacion;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label lblHistorial;
        private System.Windows.Forms.Panel pnlDatosPredio;
        private System.Windows.Forms.Label lblLongitud;
        private System.Windows.Forms.Label lblLatitud;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblDomicilio;
        private System.Windows.Forms.Label lblPesito;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.Label lblValTotCons;
        private System.Windows.Forms.Label lblValConsCom;
        private System.Windows.Forms.Label lblValTerrCom;
        private System.Windows.Forms.Label lblSupConsCom;
        private System.Windows.Forms.Label lblSupTerrComun;
        private System.Windows.Forms.Label lblValTotTerr;
        private System.Windows.Forms.Label lblValorConsPriv;
        private System.Windows.Forms.Label lblValTerrPriv;
        private System.Windows.Forms.Label lblSupConsPriv;
        private System.Windows.Forms.Label lblSupTerrPriv;
        private System.Windows.Forms.Label lblTitular;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblCiudadano;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnConsulta;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pbxQR2;
        private System.Windows.Forms.PictureBox pbxQR;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnSalida;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PanelBarraTitulo;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnMaximizar;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.Timer tmFechaHora;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button btnMaps;
        private System.Windows.Forms.Button btnBuscarClave;
        private System.Windows.Forms.Label lblComentario;
        private System.Windows.Forms.Label lblMun;
        private System.Windows.Forms.TextBox txtEdificio;
        private System.Windows.Forms.TextBox txtDepto;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.TextBox txtManzana;
        private System.Windows.Forms.TextBox txtZona;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.ComboBox CBO_SERIE;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox TXT_FOLIO;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label lblObsCar;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Panel pnlCambios;
        private System.Windows.Forms.CheckBox ckbCambioFactoresTerr;
        private System.Windows.Forms.CheckBox ckbCambioFactoresCons;
        private System.Windows.Forms.CheckBox ckbCambioSuperficie;
        private System.Windows.Forms.CheckBox ckbCambioConstruccion;
        private System.Windows.Forms.CheckBox ckbCambioNombre;
        private System.Windows.Forms.Panel pnlAlta;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.ComboBox cboUbicacion;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label lblCodCalle;
        private System.Windows.Forms.Label lblZonaA;
        private System.Windows.Forms.Label lblCalleA;
        private System.Windows.Forms.Label lblSupTerrenoA;
        private System.Windows.Forms.Label lblFondoA;
        private System.Windows.Forms.Label lblAreaA;
        private System.Windows.Forms.Label lblSupConsCA;
        private System.Windows.Forms.Label lblSupTerrCA;
        private System.Windows.Forms.Label lblFrenteA;
        private System.Windows.Forms.Label lbldesA;
        private System.Windows.Forms.Label lblSupConsA;
        private System.Windows.Forms.Label lblRegimenA;
        private System.Windows.Forms.Button btnMapsA;
        private System.Windows.Forms.Panel pnlAltaInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlCertificado;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnAutorizar;
        private System.Windows.Forms.Panel panel53;
        private System.Windows.Forms.Label lblConstTot;
        private System.Windows.Forms.Label lblTerrenoTot;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Panel panel54;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.DataGridView DGVRESULTADO;
        private System.Windows.Forms.Label lblNoAutorizado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNoAutorizar;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
    }
}