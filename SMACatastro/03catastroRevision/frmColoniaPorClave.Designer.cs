namespace SMACatastro.catastroRevision
{
    partial class frmColoniaPorClave
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmColoniaPorClave));
            this.PanelBarraTitulo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lbFecha = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.tmFechaHora = new System.Windows.Forms.Timer(this.components);
            this.btnSalida = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnCancela = new System.Windows.Forms.Button();
            this.txtMun = new System.Windows.Forms.Label();
            this.txtEdificio = new System.Windows.Forms.TextBox();
            this.txtDepto = new System.Windows.Forms.TextBox();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.txtManzana = new System.Windows.Forms.TextBox();
            this.txtZona = new System.Windows.Forms.TextBox();
            this.btnConsulta = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.btnMaps = new System.Windows.Forms.Button();
            this.dgResultado = new System.Windows.Forms.DataGridView();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lblColoniaDestino = new System.Windows.Forms.Label();
            this.lblColoniaOrigen = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblConteoLotes = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCambioLote = new System.Windows.Forms.Button();
            this.btnCancelarColoniasAbajo = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.lblColonia = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCambioManzana = new System.Windows.Forms.Button();
            this.btnBuscarClave = new System.Windows.Forms.Button();
            this.PanelBarraTitulo.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).BeginInit();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelBarraTitulo
            // 
            this.PanelBarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.PanelBarraTitulo.Controls.Add(this.label2);
            this.PanelBarraTitulo.Controls.Add(this.btnMinimizar);
            this.PanelBarraTitulo.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.PanelBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelBarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.PanelBarraTitulo.Name = "PanelBarraTitulo";
            this.PanelBarraTitulo.Size = new System.Drawing.Size(1366, 43);
            this.PanelBarraTitulo.TabIndex = 1510;
            this.PanelBarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelBarraTitulo_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("High Tower Text", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(797, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(497, 31);
            this.label2.TabIndex = 12;
            this.label2.Text = "COLONIA POR CLAVE - CATASTRO";
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnMinimizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Yellow;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Image = global::SMACatastro.Properties.Resources.Minimize;
            this.btnMinimizar.Location = new System.Drawing.Point(1311, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(43, 43);
            this.btnMinimizar.TabIndex = 8;
            this.btnMinimizar.UseVisualStyleBackColor = true;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            this.btnMinimizar.MouseHover += new System.EventHandler(this.btnMinimizar_MouseHover);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel1.Location = new System.Drawing.Point(0, -115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(7, 950);
            this.panel1.TabIndex = 1512;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.panel2.Location = new System.Drawing.Point(1359, -119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(7, 959);
            this.panel2.TabIndex = 1513;
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
            this.panel3.TabIndex = 1514;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(11, 6);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(61, 16);
            this.lblUsuario.TabIndex = 8;
            this.lblUsuario.Text = "Usuario";
            // 
            // lbFecha
            // 
            this.lbFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFecha.AutoSize = true;
            this.lbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFecha.ForeColor = System.Drawing.Color.White;
            this.lbFecha.Location = new System.Drawing.Point(1030, 7);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(202, 15);
            this.lbFecha.TabIndex = 7;
            this.lbFecha.Text = "Lunes, 26 de septiembre 2018";
            // 
            // lblHora
            // 
            this.lblHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.Color.White;
            this.lblHora.Location = new System.Drawing.Point(1291, 5);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(63, 15);
            this.lblHora.TabIndex = 6;
            this.lblHora.Text = "21:49:45";
            // 
            // tmFechaHora
            // 
            this.tmFechaHora.Enabled = true;
            this.tmFechaHora.Tick += new System.EventHandler(this.tmFechaHora_Tick);
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
            this.btnSalida.Location = new System.Drawing.Point(1281, 49);
            this.btnSalida.Name = "btnSalida";
            this.btnSalida.Size = new System.Drawing.Size(72, 64);
            this.btnSalida.TabIndex = 1522;
            this.btnSalida.UseVisualStyleBackColor = false;
            this.btnSalida.Click += new System.EventHandler(this.btnSalida_Click);
            this.btnSalida.MouseHover += new System.EventHandler(this.btnSalida_MouseHover);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Image = global::SMACatastro.Properties.Resources.nuevo;
            this.btnNuevo.Location = new System.Drawing.Point(1135, 49);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(72, 64);
            this.btnNuevo.TabIndex = 1524;
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            this.btnNuevo.MouseHover += new System.EventHandler(this.btnNuevo_MouseHover);
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
            this.btnCancela.Location = new System.Drawing.Point(1208, 49);
            this.btnCancela.Name = "btnCancela";
            this.btnCancela.Size = new System.Drawing.Size(72, 64);
            this.btnCancela.TabIndex = 1523;
            this.btnCancela.Text = "r";
            this.btnCancela.UseVisualStyleBackColor = false;
            this.btnCancela.Click += new System.EventHandler(this.btnCancela_Click);
            this.btnCancela.MouseHover += new System.EventHandler(this.btnCancela_MouseHover);
            // 
            // txtMun
            // 
            this.txtMun.AutoSize = true;
            this.txtMun.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMun.Location = new System.Drawing.Point(618, 75);
            this.txtMun.Name = "txtMun";
            this.txtMun.Size = new System.Drawing.Size(35, 18);
            this.txtMun.TabIndex = 1531;
            this.txtMun.Text = "041";
            this.txtMun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEdificio
            // 
            this.txtEdificio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEdificio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEdificio.Location = new System.Drawing.Point(780, 73);
            this.txtEdificio.MaxLength = 2;
            this.txtEdificio.Name = "txtEdificio";
            this.txtEdificio.Size = new System.Drawing.Size(36, 22);
            this.txtEdificio.TabIndex = 1528;
            this.txtEdificio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEdificio.TextChanged += new System.EventHandler(this.txtEdificio_TextChanged);
            this.txtEdificio.Enter += new System.EventHandler(this.txtEdificio_Enter);
            this.txtEdificio.Leave += new System.EventHandler(this.txtEdificio_Leave);
            // 
            // txtDepto
            // 
            this.txtDepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepto.Location = new System.Drawing.Point(828, 73);
            this.txtDepto.MaxLength = 4;
            this.txtDepto.Name = "txtDepto";
            this.txtDepto.Size = new System.Drawing.Size(65, 22);
            this.txtDepto.TabIndex = 1529;
            this.txtDepto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDepto.TextChanged += new System.EventHandler(this.txtDepto_TextChanged);
            this.txtDepto.Enter += new System.EventHandler(this.txtDepto_Enter);
            this.txtDepto.Leave += new System.EventHandler(this.txtDepto_Leave);
            // 
            // txtLote
            // 
            this.txtLote.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLote.Location = new System.Drawing.Point(742, 73);
            this.txtLote.MaxLength = 2;
            this.txtLote.Name = "txtLote";
            this.txtLote.Size = new System.Drawing.Size(26, 22);
            this.txtLote.TabIndex = 1527;
            this.txtLote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLote.TextChanged += new System.EventHandler(this.txtLote_TextChanged);
            this.txtLote.Enter += new System.EventHandler(this.txtLote_Enter);
            this.txtLote.Leave += new System.EventHandler(this.txtLote_Leave);
            // 
            // txtManzana
            // 
            this.txtManzana.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtManzana.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManzana.Location = new System.Drawing.Point(698, 73);
            this.txtManzana.MaxLength = 3;
            this.txtManzana.Name = "txtManzana";
            this.txtManzana.Size = new System.Drawing.Size(34, 22);
            this.txtManzana.TabIndex = 1526;
            this.txtManzana.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtManzana.TextChanged += new System.EventHandler(this.txtManzana_TextChanged);
            this.txtManzana.Enter += new System.EventHandler(this.txtManzana_Enter);
            this.txtManzana.Leave += new System.EventHandler(this.txtManzana_Leave);
            // 
            // txtZona
            // 
            this.txtZona.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtZona.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZona.Location = new System.Drawing.Point(662, 73);
            this.txtZona.MaxLength = 2;
            this.txtZona.Name = "txtZona";
            this.txtZona.Size = new System.Drawing.Size(26, 22);
            this.txtZona.TabIndex = 1525;
            this.txtZona.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtZona.TextChanged += new System.EventHandler(this.txtZona_TextChanged);
            this.txtZona.Enter += new System.EventHandler(this.txtZona_Enter);
            this.txtZona.Leave += new System.EventHandler(this.txtZona_Leave);
            // 
            // btnConsulta
            // 
            this.btnConsulta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnConsulta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsulta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(24)))), ((int)(((byte)(151)))));
            this.btnConsulta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(24)))), ((int)(((byte)(151)))));
            this.btnConsulta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsulta.ForeColor = System.Drawing.Color.White;
            this.btnConsulta.Location = new System.Drawing.Point(899, 73);
            this.btnConsulta.Name = "btnConsulta";
            this.btnConsulta.Size = new System.Drawing.Size(122, 23);
            this.btnConsulta.TabIndex = 1530;
            this.btnConsulta.Text = "CONSULTA";
            this.btnConsulta.UseVisualStyleBackColor = false;
            this.btnConsulta.Click += new System.EventHandler(this.btnConsulta_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(818, 79);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(8, 11);
            this.label17.TabIndex = 1543;
            this.label17.Text = "-";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(770, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(8, 11);
            this.label16.TabIndex = 1542;
            this.label16.Text = "-";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(733, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(8, 11);
            this.label15.TabIndex = 1541;
            this.label15.Text = "-";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(689, 79);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(8, 11);
            this.label14.TabIndex = 1540;
            this.label14.Text = "-";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(651, 79);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(8, 11);
            this.label13.TabIndex = 1539;
            this.label13.Text = "-";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(847, 96);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 10);
            this.label12.TabIndex = 1538;
            this.label12.Text = "Depto";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(782, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 10);
            this.label11.TabIndex = 1537;
            this.label11.Text = "Edificio";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(745, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 10);
            this.label10.TabIndex = 1536;
            this.label10.Text = "Lote";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(695, 96);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 10);
            this.label9.TabIndex = 1535;
            this.label9.Text = "Manzana";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(664, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 10);
            this.label8.TabIndex = 1534;
            this.label8.Text = "Zona";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Rounded MT Bold", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(617, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 10);
            this.label7.TabIndex = 1533;
            this.label7.Text = "Municipio";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(466, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 16);
            this.label1.TabIndex = 1532;
            this.label1.Text = "CLAVE CATASTRAL:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::SMACatastro.Properties.Resources.logo_2025;
            this.pictureBox1.Location = new System.Drawing.Point(12, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(399, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1544;
            this.pictureBox1.TabStop = false;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(16)))), ((int)(((byte)(44)))));
            this.panel8.Location = new System.Drawing.Point(348, 130);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(956, 3);
            this.panel8.TabIndex = 1548;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(192)))), ((int)(((byte)(20)))));
            this.panel9.Location = new System.Drawing.Point(347, 126);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(946, 3);
            this.panel9.TabIndex = 1547;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(110)))), ((int)(((byte)(191)))));
            this.panel7.Location = new System.Drawing.Point(347, 122);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(934, 3);
            this.panel7.TabIndex = 1546;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(14)))), ((int)(((byte)(108)))));
            this.panel6.Location = new System.Drawing.Point(348, 118);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(919, 3);
            this.panel6.TabIndex = 1545;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(181)))), ((int)(((byte)(17)))));
            this.panel5.Location = new System.Drawing.Point(348, 134);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(938, 3);
            this.panel5.TabIndex = 1549;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.panel4.Controls.Add(this.gMapControl1);
            this.panel4.Location = new System.Drawing.Point(12, 162);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(707, 520);
            this.panel4.TabIndex = 1550;
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
            this.gMapControl1.Location = new System.Drawing.Point(7, 3);
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
            this.gMapControl1.Size = new System.Drawing.Size(693, 514);
            this.gMapControl1.TabIndex = 121;
            this.gMapControl1.Zoom = 0D;
            // 
            // btnMaps
            // 
            this.btnMaps.AutoSize = true;
            this.btnMaps.BackColor = System.Drawing.Color.White;
            this.btnMaps.BackgroundImage = global::SMACatastro.Properties.Resources.mapa__1_;
            this.btnMaps.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMaps.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaps.Enabled = false;
            this.btnMaps.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnMaps.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaps.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMaps.Location = new System.Drawing.Point(598, 3);
            this.btnMaps.Name = "btnMaps";
            this.btnMaps.Size = new System.Drawing.Size(22, 22);
            this.btnMaps.TabIndex = 1611;
            this.btnMaps.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnMaps.UseVisualStyleBackColor = false;
            this.btnMaps.Click += new System.EventHandler(this.btnMaps_Click);
            this.btnMaps.MouseHover += new System.EventHandler(this.btnMaps_MouseHover);
            // 
            // dgResultado
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgResultado.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgResultado.BackgroundColor = System.Drawing.Color.Gray;
            this.dgResultado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResultado.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.dgResultado.Location = new System.Drawing.Point(723, 247);
            this.dgResultado.Name = "dgResultado";
            this.dgResultado.Size = new System.Drawing.Size(630, 185);
            this.dgResultado.TabIndex = 1551;
            this.dgResultado.DoubleClick += new System.EventHandler(this.dgResultado_DoubleClick);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.panel11.Controls.Add(this.lblColoniaDestino);
            this.panel11.Controls.Add(this.lblColoniaOrigen);
            this.panel11.Controls.Add(this.panel13);
            this.panel11.Controls.Add(this.panel12);
            this.panel11.Controls.Add(this.label19);
            this.panel11.Controls.Add(this.label18);
            this.panel11.Controls.Add(this.lblConteoLotes);
            this.panel11.Controls.Add(this.label5);
            this.panel11.Controls.Add(this.label4);
            this.panel11.Location = new System.Drawing.Point(723, 435);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(630, 218);
            this.panel11.TabIndex = 1553;
            // 
            // lblColoniaDestino
            // 
            this.lblColoniaDestino.BackColor = System.Drawing.Color.White;
            this.lblColoniaDestino.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblColoniaDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblColoniaDestino.Location = new System.Drawing.Point(6, 153);
            this.lblColoniaDestino.Name = "lblColoniaDestino";
            this.lblColoniaDestino.Size = new System.Drawing.Size(613, 39);
            this.lblColoniaDestino.TabIndex = 1598;
            this.lblColoniaDestino.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblColoniaOrigen
            // 
            this.lblColoniaOrigen.BackColor = System.Drawing.Color.White;
            this.lblColoniaOrigen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblColoniaOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblColoniaOrigen.Location = new System.Drawing.Point(7, 62);
            this.lblColoniaOrigen.Name = "lblColoniaOrigen";
            this.lblColoniaOrigen.Size = new System.Drawing.Size(613, 39);
            this.lblColoniaOrigen.TabIndex = 1597;
            this.lblColoniaOrigen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.Cyan;
            this.panel13.Location = new System.Drawing.Point(8, 116);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(197, 2);
            this.panel13.TabIndex = 1596;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.Cyan;
            this.panel12.Location = new System.Drawing.Point(6, 26);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(197, 2);
            this.panel12.TabIndex = 1595;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(7, 128);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 13);
            this.label19.TabIndex = 4;
            this.label19.Text = "Colonia Destino:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(7, 39);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Colonia Origen:";
            // 
            // lblConteoLotes
            // 
            this.lblConteoLotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConteoLotes.ForeColor = System.Drawing.Color.White;
            this.lblConteoLotes.Location = new System.Drawing.Point(133, 2);
            this.lblConteoLotes.Name = "lblConteoLotes";
            this.lblConteoLotes.Size = new System.Drawing.Size(486, 19);
            this.lblConteoLotes.TabIndex = 2;
            this.lblConteoLotes.Text = "0";
            this.lblConteoLotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(244, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(7, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "CAMBIOS COLONIA";
            // 
            // btnCambioLote
            // 
            this.btnCambioLote.BackColor = System.Drawing.Color.Yellow;
            this.btnCambioLote.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambioLote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambioLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambioLote.ForeColor = System.Drawing.Color.Black;
            this.btnCambioLote.Location = new System.Drawing.Point(1086, 659);
            this.btnCambioLote.Name = "btnCambioLote";
            this.btnCambioLote.Size = new System.Drawing.Size(116, 23);
            this.btnCambioLote.TabIndex = 1554;
            this.btnCambioLote.Text = "COLONIA - LOTE";
            this.btnCambioLote.UseVisualStyleBackColor = false;
            this.btnCambioLote.Click += new System.EventHandler(this.btnCambioLote_Click);
            this.btnCambioLote.MouseHover += new System.EventHandler(this.btnCambioLote_MouseHover);
            // 
            // btnCancelarColoniasAbajo
            // 
            this.btnCancelarColoniasAbajo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancelarColoniasAbajo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarColoniasAbajo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarColoniasAbajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarColoniasAbajo.ForeColor = System.Drawing.Color.White;
            this.btnCancelarColoniasAbajo.Location = new System.Drawing.Point(723, 659);
            this.btnCancelarColoniasAbajo.Name = "btnCancelarColoniasAbajo";
            this.btnCancelarColoniasAbajo.Size = new System.Drawing.Size(168, 23);
            this.btnCancelarColoniasAbajo.TabIndex = 1555;
            this.btnCancelarColoniasAbajo.Text = "REFRESH COLONIAS";
            this.btnCancelarColoniasAbajo.UseVisualStyleBackColor = false;
            this.btnCancelarColoniasAbajo.Click += new System.EventHandler(this.btnCancelarColoniasAbajo_Click);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.panel10.Controls.Add(this.btnMaps);
            this.panel10.Controls.Add(this.panel14);
            this.panel10.Controls.Add(this.lblColonia);
            this.panel10.Controls.Add(this.label3);
            this.panel10.Location = new System.Drawing.Point(723, 162);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(630, 82);
            this.panel10.TabIndex = 1556;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.Cyan;
            this.panel14.Location = new System.Drawing.Point(9, 19);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(217, 2);
            this.panel14.TabIndex = 1596;
            // 
            // lblColonia
            // 
            this.lblColonia.BackColor = System.Drawing.Color.White;
            this.lblColonia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblColonia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblColonia.Location = new System.Drawing.Point(7, 29);
            this.lblColonia.Name = "lblColonia";
            this.lblColonia.Size = new System.Drawing.Size(613, 39);
            this.lblColonia.TabIndex = 1545;
            this.lblColonia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "INFORMACIÓN COLONIA ACTUAL:";
            // 
            // btnCambioManzana
            // 
            this.btnCambioManzana.BackColor = System.Drawing.Color.Yellow;
            this.btnCambioManzana.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCambioManzana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCambioManzana.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCambioManzana.Location = new System.Drawing.Point(1207, 659);
            this.btnCambioManzana.Name = "btnCambioManzana";
            this.btnCambioManzana.Size = new System.Drawing.Size(144, 23);
            this.btnCambioManzana.TabIndex = 1557;
            this.btnCambioManzana.Text = "COLONIA - MANZANA";
            this.btnCambioManzana.UseVisualStyleBackColor = false;
            this.btnCambioManzana.Click += new System.EventHandler(this.btnCambioManzana_Click);
            this.btnCambioManzana.MouseHover += new System.EventHandler(this.btnCambioManzana_MouseHover);
            // 
            // btnBuscarClave
            // 
            this.btnBuscarClave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.btnBuscarClave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarClave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnBuscarClave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(54)))), ((int)(((byte)(151)))));
            this.btnBuscarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarClave.ForeColor = System.Drawing.Color.White;
            this.btnBuscarClave.Image = global::SMACatastro.Properties.Resources.buscar;
            this.btnBuscarClave.Location = new System.Drawing.Point(1063, 49);
            this.btnBuscarClave.Name = "btnBuscarClave";
            this.btnBuscarClave.Size = new System.Drawing.Size(72, 64);
            this.btnBuscarClave.TabIndex = 1707;
            this.btnBuscarClave.UseVisualStyleBackColor = false;
            this.btnBuscarClave.Click += new System.EventHandler(this.btnBuscarClave_Click);
            // 
            // frmColoniaPorClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1366, 720);
            this.Controls.Add(this.btnBuscarClave);
            this.Controls.Add(this.btnCambioManzana);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.btnCancelarColoniasAbajo);
            this.Controls.Add(this.btnCambioLote);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.dgResultado);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.txtMun);
            this.Controls.Add(this.txtEdificio);
            this.Controls.Add(this.txtDepto);
            this.Controls.Add(this.txtLote);
            this.Controls.Add(this.txtManzana);
            this.Controls.Add(this.txtZona);
            this.Controls.Add(this.btnConsulta);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalida);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnCancela);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PanelBarraTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmColoniaPorClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmColoniaPorClave";
            this.Load += new System.EventHandler(this.frmColoniaPorClave_Load);
            this.PanelBarraTitulo.ResumeLayout(false);
            this.PanelBarraTitulo.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgResultado)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelBarraTitulo;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Timer tmFechaHora;
        private System.Windows.Forms.Button btnSalida;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnCancela;
        private System.Windows.Forms.Label txtMun;
        private System.Windows.Forms.TextBox txtEdificio;
        private System.Windows.Forms.TextBox txtDepto;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.TextBox txtManzana;
        private System.Windows.Forms.TextBox txtZona;
        private System.Windows.Forms.Button btnConsulta;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private System.Windows.Forms.DataGridView dgResultado;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnCambioLote;
        private System.Windows.Forms.Button btnCancelarColoniasAbajo;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCambioManzana;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnMaps;
        private System.Windows.Forms.Label lblColonia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblConteoLotes;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label lblColoniaDestino;
        private System.Windows.Forms.Label lblColoniaOrigen;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button btnBuscarClave;
    }
}