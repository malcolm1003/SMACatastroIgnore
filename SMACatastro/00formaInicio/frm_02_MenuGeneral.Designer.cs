namespace SMACatastro.formaInicio
{
    partial class frm_02_MenuGeneral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_02_MenuGeneral));
            this.tmFechaHora = new System.Windows.Forms.Timer(this.components);
            this.tmExpandirMenu = new System.Windows.Forms.Timer(this.components);
            this.tmContraerMenu = new System.Windows.Forms.Timer(this.components);
            this.panelContenedorPrincipal = new System.Windows.Forms.Panel();
            this.panelContenedorForm = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.PictureBox();
            this.lblCargoUsuarioMenu = new System.Windows.Forms.Label();
            this.lblNombreUsuarioMenu = new System.Windows.Forms.Label();
            this.lbFecha = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.lblHora = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.btnSoporte = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.btnGenerales = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.btnSistemas = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnRevision = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnVentanilla = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCartografia = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PanelBarraTitulo = new System.Windows.Forms.Panel();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVercionSudsos = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.panelContenedorPrincipal.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSalir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.PanelBarraTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // tmFechaHora
            // 
            this.tmFechaHora.Enabled = true;
            this.tmFechaHora.Tick += new System.EventHandler(this.tmFechaHora_Tick);
            // 
            // tmExpandirMenu
            // 
            this.tmExpandirMenu.Interval = 40;
            this.tmExpandirMenu.Tick += new System.EventHandler(this.tmExpandirMenu_Tick);
            // 
            // tmContraerMenu
            // 
            this.tmContraerMenu.Interval = 40;
            this.tmContraerMenu.Tick += new System.EventHandler(this.tmContraerMenu_Tick);
            // 
            // panelContenedorPrincipal
            // 
            this.panelContenedorPrincipal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.panelContenedorPrincipal.Controls.Add(this.panelContenedorForm);
            this.panelContenedorPrincipal.Controls.Add(this.panel1);
            this.panelContenedorPrincipal.Controls.Add(this.panelMenu);
            this.panelContenedorPrincipal.Controls.Add(this.PanelBarraTitulo);
            this.panelContenedorPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedorPrincipal.Location = new System.Drawing.Point(0, 0);
            this.panelContenedorPrincipal.Name = "panelContenedorPrincipal";
            this.panelContenedorPrincipal.Size = new System.Drawing.Size(1100, 600);
            this.panelContenedorPrincipal.TabIndex = 13;
            // 
            // panelContenedorForm
            // 
            this.panelContenedorForm.BackColor = System.Drawing.Color.White;
            this.panelContenedorForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedorForm.Location = new System.Drawing.Point(230, 43);
            this.panelContenedorForm.Name = "panelContenedorForm";
            this.panelContenedorForm.Size = new System.Drawing.Size(870, 494);
            this.panelContenedorForm.TabIndex = 12;
            this.panelContenedorForm.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContenedorForm_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.panel1.Controls.Add(this.btnSalir);
            this.panel1.Controls.Add(this.lblCargoUsuarioMenu);
            this.panel1.Controls.Add(this.lblNombreUsuarioMenu);
            this.panel1.Controls.Add(this.lbFecha);
            this.panel1.Controls.Add(this.pictureBox7);
            this.panel1.Controls.Add(this.lblHora);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(230, 537);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(870, 63);
            this.panel1.TabIndex = 11;
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(181)))), ((int)(((byte)(17)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Image = global::SMACatastro.Properties.Resources.apagado1;
            this.btnSalir.Location = new System.Drawing.Point(6, 6);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(51, 51);
            this.btnSalir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnSalir.TabIndex = 14;
            this.btnSalir.TabStop = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblCargoUsuarioMenu
            // 
            this.lblCargoUsuarioMenu.AutoSize = true;
            this.lblCargoUsuarioMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCargoUsuarioMenu.ForeColor = System.Drawing.Color.White;
            this.lblCargoUsuarioMenu.Location = new System.Drawing.Point(117, 38);
            this.lblCargoUsuarioMenu.Name = "lblCargoUsuarioMenu";
            this.lblCargoUsuarioMenu.Size = new System.Drawing.Size(49, 16);
            this.lblCargoUsuarioMenu.TabIndex = 7;
            this.lblCargoUsuarioMenu.Text = "Cargo";
            // 
            // lblNombreUsuarioMenu
            // 
            this.lblNombreUsuarioMenu.AutoSize = true;
            this.lblNombreUsuarioMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreUsuarioMenu.ForeColor = System.Drawing.Color.White;
            this.lblNombreUsuarioMenu.Location = new System.Drawing.Point(117, 13);
            this.lblNombreUsuarioMenu.Name = "lblNombreUsuarioMenu";
            this.lblNombreUsuarioMenu.Size = new System.Drawing.Size(120, 16);
            this.lblNombreUsuarioMenu.TabIndex = 5;
            this.lblNombreUsuarioMenu.Text = "Nombre Usuario";
            // 
            // lbFecha
            // 
            this.lbFecha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFecha.AutoSize = true;
            this.lbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFecha.ForeColor = System.Drawing.Color.White;
            this.lbFecha.Location = new System.Drawing.Point(576, 41);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(224, 20);
            this.lbFecha.TabIndex = 4;
            this.lbFecha.Text = "Lunes, 26 de septiembre 2018";
            this.lbFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.pictureBox7.Image = global::SMACatastro.Properties.Resources.administradorG;
            this.pictureBox7.Location = new System.Drawing.Point(63, 6);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(51, 51);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 3;
            this.pictureBox7.TabStop = false;
            // 
            // lblHora
            // 
            this.lblHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.Color.White;
            this.lblHora.Location = new System.Drawing.Point(637, -1);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(229, 42);
            this.lblHora.TabIndex = 1;
            this.lblHora.Text = "21:49:45";
            this.lblHora.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.panelMenu.Controls.Add(this.pictureBox5);
            this.panelMenu.Controls.Add(this.btnSoporte);
            this.panelMenu.Controls.Add(this.btnMenu);
            this.panelMenu.Controls.Add(this.pictureBox6);
            this.panelMenu.Controls.Add(this.btnGenerales);
            this.panelMenu.Controls.Add(this.pictureBox3);
            this.panelMenu.Controls.Add(this.btnSistemas);
            this.panelMenu.Controls.Add(this.pictureBox4);
            this.panelMenu.Controls.Add(this.btnRevision);
            this.panelMenu.Controls.Add(this.pictureBox2);
            this.panelMenu.Controls.Add(this.btnVentanilla);
            this.panelMenu.Controls.Add(this.pictureBox1);
            this.panelMenu.Controls.Add(this.btnCartografia);
            this.panelMenu.Controls.Add(this.label2);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 43);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(230, 557);
            this.panelMenu.TabIndex = 10;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.pictureBox5.Location = new System.Drawing.Point(0, 311);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(7, 40);
            this.pictureBox5.TabIndex = 20;
            this.pictureBox5.TabStop = false;
            // 
            // btnSoporte
            // 
            this.btnSoporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSoporte.FlatAppearance.BorderSize = 0;
            this.btnSoporte.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnSoporte.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnSoporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSoporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSoporte.ForeColor = System.Drawing.Color.White;
            this.btnSoporte.Image = global::SMACatastro.Properties.Resources.administrador2;
            this.btnSoporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSoporte.Location = new System.Drawing.Point(7, 311);
            this.btnSoporte.Name = "btnSoporte";
            this.btnSoporte.Size = new System.Drawing.Size(221, 40);
            this.btnSoporte.TabIndex = 19;
            this.btnSoporte.Text = "    CONFIGURACION";
            this.btnSoporte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSoporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSoporte.UseVisualStyleBackColor = true;
            this.btnSoporte.Click += new System.EventHandler(this.btnSoporte_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.Image = global::SMACatastro.Properties.Resources.menu2;
            this.btnMenu.Location = new System.Drawing.Point(172, 2);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(60, 37);
            this.btnMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnMenu.TabIndex = 12;
            this.btnMenu.TabStop = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.pictureBox6.Location = new System.Drawing.Point(0, 264);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(7, 40);
            this.pictureBox6.TabIndex = 9;
            this.pictureBox6.TabStop = false;
            // 
            // btnGenerales
            // 
            this.btnGenerales.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerales.FlatAppearance.BorderSize = 0;
            this.btnGenerales.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnGenerales.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnGenerales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerales.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerales.ForeColor = System.Drawing.Color.White;
            this.btnGenerales.Image = global::SMACatastro.Properties.Resources.napoleon;
            this.btnGenerales.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerales.Location = new System.Drawing.Point(7, 264);
            this.btnGenerales.Name = "btnGenerales";
            this.btnGenerales.Size = new System.Drawing.Size(221, 40);
            this.btnGenerales.TabIndex = 8;
            this.btnGenerales.Text = "    GENERALES";
            this.btnGenerales.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerales.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerales.UseVisualStyleBackColor = true;
            this.btnGenerales.Click += new System.EventHandler(this.btnConfiguracion_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.pictureBox3.Location = new System.Drawing.Point(0, 218);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(7, 40);
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // btnSistemas
            // 
            this.btnSistemas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSistemas.FlatAppearance.BorderSize = 0;
            this.btnSistemas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnSistemas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnSistemas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSistemas.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSistemas.ForeColor = System.Drawing.Color.White;
            this.btnSistemas.Image = global::SMACatastro.Properties.Resources.computadora_de_escritorio;
            this.btnSistemas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSistemas.Location = new System.Drawing.Point(7, 218);
            this.btnSistemas.Name = "btnSistemas";
            this.btnSistemas.Size = new System.Drawing.Size(221, 40);
            this.btnSistemas.TabIndex = 6;
            this.btnSistemas.Text = "    SISTEMAS";
            this.btnSistemas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSistemas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSistemas.UseVisualStyleBackColor = true;
            this.btnSistemas.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.pictureBox4.Location = new System.Drawing.Point(0, 172);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(7, 40);
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // btnRevision
            // 
            this.btnRevision.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRevision.FlatAppearance.BorderSize = 0;
            this.btnRevision.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnRevision.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnRevision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevision.ForeColor = System.Drawing.Color.White;
            this.btnRevision.Image = global::SMACatastro.Properties.Resources.revision;
            this.btnRevision.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRevision.Location = new System.Drawing.Point(7, 172);
            this.btnRevision.Name = "btnRevision";
            this.btnRevision.Size = new System.Drawing.Size(221, 40);
            this.btnRevision.TabIndex = 4;
            this.btnRevision.Text = "    REVISIÓN";
            this.btnRevision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRevision.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRevision.UseVisualStyleBackColor = true;
            this.btnRevision.Click += new System.EventHandler(this.btnCaja_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.pictureBox2.Location = new System.Drawing.Point(0, 126);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(7, 40);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // btnVentanilla
            // 
            this.btnVentanilla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentanilla.FlatAppearance.BorderSize = 0;
            this.btnVentanilla.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnVentanilla.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnVentanilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentanilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentanilla.ForeColor = System.Drawing.Color.White;
            this.btnVentanilla.Image = global::SMACatastro.Properties.Resources.ventanas__1_;
            this.btnVentanilla.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVentanilla.Location = new System.Drawing.Point(7, 126);
            this.btnVentanilla.Name = "btnVentanilla";
            this.btnVentanilla.Size = new System.Drawing.Size(221, 40);
            this.btnVentanilla.TabIndex = 2;
            this.btnVentanilla.Text = "    VENTANILLA";
            this.btnVentanilla.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVentanilla.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVentanilla.UseVisualStyleBackColor = true;
            this.btnVentanilla.Click += new System.EventHandler(this.btnCalculos_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.pictureBox1.Location = new System.Drawing.Point(0, 80);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(7, 40);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnCartografia
            // 
            this.btnCartografia.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCartografia.FlatAppearance.BorderSize = 0;
            this.btnCartografia.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.btnCartografia.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.btnCartografia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCartografia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCartografia.ForeColor = System.Drawing.Color.White;
            this.btnCartografia.Image = global::SMACatastro.Properties.Resources.mapa__1_;
            this.btnCartografia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCartografia.Location = new System.Drawing.Point(7, 80);
            this.btnCartografia.Name = "btnCartografia";
            this.btnCartografia.Size = new System.Drawing.Size(221, 40);
            this.btnCartografia.TabIndex = 0;
            this.btnCartografia.Text = "    CARTOGRAFIA";
            this.btnCartografia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCartografia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCartografia.UseVisualStyleBackColor = true;
            this.btnCartografia.Click += new System.EventHandler(this.btnContrato_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft PhagsPa", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 25);
            this.label2.TabIndex = 18;
            this.label2.Text = "MENU  GENERAL";
            // 
            // PanelBarraTitulo
            // 
            this.PanelBarraTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(17)))), ((int)(((byte)(146)))));
            this.PanelBarraTitulo.Controls.Add(this.pictureBox8);
            this.PanelBarraTitulo.Controls.Add(this.label1);
            this.PanelBarraTitulo.Controls.Add(this.lblVercionSudsos);
            this.PanelBarraTitulo.Controls.Add(this.btnMinimizar);
            this.PanelBarraTitulo.Cursor = System.Windows.Forms.Cursors.NoMove2D;
            this.PanelBarraTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelBarraTitulo.Location = new System.Drawing.Point(0, 0);
            this.PanelBarraTitulo.Name = "PanelBarraTitulo";
            this.PanelBarraTitulo.Size = new System.Drawing.Size(1100, 43);
            this.PanelBarraTitulo.TabIndex = 9;
            this.PanelBarraTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelBarraTitulo_MouseDown);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.White;
            this.pictureBox8.Image = global::SMACatastro.Properties.Resources.logo_2025;
            this.pictureBox8.Location = new System.Drawing.Point(0, -1);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(230, 44);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 16;
            this.pictureBox8.TabStop = false;
            this.pictureBox8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox8_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("High Tower Text", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(225, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 34);
            this.label1.TabIndex = 4;
            this.label1.Text = "  C A T A S T R O";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            // 
            // lblVercionSudsos
            // 
            this.lblVercionSudsos.AutoSize = true;
            this.lblVercionSudsos.Font = new System.Drawing.Font("High Tower Text", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblVercionSudsos.ForeColor = System.Drawing.Color.White;
            this.lblVercionSudsos.Location = new System.Drawing.Point(502, 5);
            this.lblVercionSudsos.Name = "lblVercionSudsos";
            this.lblVercionSudsos.Size = new System.Drawing.Size(82, 36);
            this.lblVercionSudsos.TabIndex = 15;
            this.lblVercionSudsos.Text = " 2.0.1";
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(24)))), ((int)(((byte)(151)))));
            this.btnMinimizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(24)))), ((int)(((byte)(151)))));
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Image = global::SMACatastro.Properties.Resources.Minimize;
            this.btnMinimizar.Location = new System.Drawing.Point(1043, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(43, 43);
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.UseVisualStyleBackColor = true;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click_1);
            // 
            // frm_02_MenuGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.panelContenedorPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1100, 600);
            this.MinimumSize = new System.Drawing.Size(1100, 600);
            this.Name = "frm_02_MenuGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_02_MenuGeneral";
            this.Load += new System.EventHandler(this.frm_02_MenuGeneral_Load);
            this.panelContenedorPrincipal.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSalir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.PanelBarraTitulo.ResumeLayout(false);
            this.PanelBarraTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmFechaHora;
        private System.Windows.Forms.Timer tmExpandirMenu;
        private System.Windows.Forms.Timer tmContraerMenu;
        private System.Windows.Forms.Panel panelContenedorPrincipal;
        private System.Windows.Forms.Panel panelContenedorForm;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblCargoUsuarioMenu;
        public System.Windows.Forms.Label lblNombreUsuarioMenu;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label lblHora;
        public System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.PictureBox pictureBox5;
        public System.Windows.Forms.Button btnSoporte;
        private System.Windows.Forms.PictureBox btnMenu;
        private System.Windows.Forms.PictureBox pictureBox6;
        public System.Windows.Forms.Button btnGenerales;
        private System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.Button btnSistemas;
        private System.Windows.Forms.PictureBox pictureBox4;
        public System.Windows.Forms.Button btnRevision;
        private System.Windows.Forms.PictureBox pictureBox2;
        public System.Windows.Forms.Button btnVentanilla;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button btnCartografia;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PanelBarraTitulo;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblVercionSudsos;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.PictureBox btnSalir;
        private System.Windows.Forms.PictureBox pictureBox8;
    }
}