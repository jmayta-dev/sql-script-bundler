namespace SSB.Presentation.WinForm
{
    partial class FrmBundler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBundler));
            BtnProcess = new Button();
            DgvScripts = new DataGridView();
            chlbGroup = new CheckedListBox();
            DgvExcluded = new DataGridView();
            BtnMoveUp = new Button();
            BtnMoveDown = new Button();
            BtnExclude = new Button();
            BtnInclude = new Button();
            lblIncluded = new Label();
            lblExcluded = new Label();
            BtnEmpaquetar = new Button();
            BtnSalir = new Button();
            button1 = new Button();
            BtnReorder = new Button();
            label2 = new Label();
            textBox2 = new TextBox();
            BtnBrowseInputFolder = new Button();
            label3 = new Label();
            BtnBrowseOutputFolder = new Button();
            textBox3 = new TextBox();
            button3 = new Button();
            colStatus = new DataGridViewTextBoxColumn();
            colPathGroup = new DataGridViewTextBoxColumn();
            colFileName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            colObs = new DataGridViewTextBoxColumn();
            colExcludedStatus = new DataGridViewTextBoxColumn();
            colExcludedGroup = new DataGridViewTextBoxColumn();
            colExcludedFilePath = new DataGridViewTextBoxColumn();
            colExcludedDescripcion = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)DgvScripts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DgvExcluded).BeginInit();
            SuspendLayout();
            // 
            // BtnProcess
            // 
            BtnProcess.Font = new Font("Fira Sans Condensed", 9F);
            BtnProcess.Location = new Point(569, 9);
            BtnProcess.Name = "BtnProcess";
            BtnProcess.Size = new Size(64, 21);
            BtnProcess.TabIndex = 0;
            BtnProcess.Text = "Procesar";
            BtnProcess.UseVisualStyleBackColor = true;
            BtnProcess.Click += BtnProcess_Click;
            // 
            // DgvScripts
            // 
            DgvScripts.AllowUserToAddRows = false;
            DgvScripts.AllowUserToDeleteRows = false;
            DgvScripts.AllowUserToResizeRows = false;
            DgvScripts.BackgroundColor = SystemColors.Window;
            DgvScripts.Columns.AddRange(new DataGridViewColumn[] { colStatus, colPathGroup, colFileName, colDescription, colObs });
            DgvScripts.Location = new Point(12, 174);
            DgvScripts.MultiSelect = false;
            DgvScripts.Name = "DgvScripts";
            DgvScripts.ReadOnly = true;
            DgvScripts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvScripts.Size = new Size(621, 287);
            DgvScripts.TabIndex = 1;
            // 
            // chlbGroup
            // 
            chlbGroup.CheckOnClick = true;
            chlbGroup.FormattingEnabled = true;
            chlbGroup.Items.AddRange(new object[] { "Agrupar 01_MD", "Agrupar 04_DT" });
            chlbGroup.Location = new Point(530, 70);
            chlbGroup.Name = "chlbGroup";
            chlbGroup.Size = new Size(103, 38);
            chlbGroup.TabIndex = 2;
            // 
            // DgvExcluded
            // 
            DgvExcluded.AllowUserToAddRows = false;
            DgvExcluded.AllowUserToDeleteRows = false;
            DgvExcluded.AllowUserToResizeRows = false;
            DgvExcluded.BackgroundColor = SystemColors.Window;
            DgvExcluded.Columns.AddRange(new DataGridViewColumn[] { colExcludedStatus, colExcludedGroup, colExcludedFilePath, colExcludedDescripcion });
            DgvExcluded.Location = new Point(12, 494);
            DgvExcluded.Name = "DgvExcluded";
            DgvExcluded.ReadOnly = true;
            DgvExcluded.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvExcluded.Size = new Size(621, 152);
            DgvExcluded.TabIndex = 7;
            // 
            // BtnMoveUp
            // 
            BtnMoveUp.AutoSize = true;
            BtnMoveUp.Image = (Image)resources.GetObject("BtnMoveUp.Image");
            BtnMoveUp.Location = new Point(639, 284);
            BtnMoveUp.Name = "BtnMoveUp";
            BtnMoveUp.Padding = new Padding(2);
            BtnMoveUp.Size = new Size(26, 26);
            BtnMoveUp.TabIndex = 10;
            BtnMoveUp.UseVisualStyleBackColor = true;
            // 
            // BtnMoveDown
            // 
            BtnMoveDown.AutoSize = true;
            BtnMoveDown.Image = (Image)resources.GetObject("BtnMoveDown.Image");
            BtnMoveDown.Location = new Point(639, 316);
            BtnMoveDown.Name = "BtnMoveDown";
            BtnMoveDown.Padding = new Padding(2);
            BtnMoveDown.Size = new Size(26, 26);
            BtnMoveDown.TabIndex = 11;
            BtnMoveDown.UseVisualStyleBackColor = true;
            // 
            // BtnExclude
            // 
            BtnExclude.AutoSize = true;
            BtnExclude.Image = (Image)resources.GetObject("BtnExclude.Image");
            BtnExclude.Location = new Point(639, 403);
            BtnExclude.Name = "BtnExclude";
            BtnExclude.Padding = new Padding(2);
            BtnExclude.Size = new Size(26, 26);
            BtnExclude.TabIndex = 12;
            BtnExclude.UseVisualStyleBackColor = true;
            // 
            // BtnInclude
            // 
            BtnInclude.AutoSize = true;
            BtnInclude.Image = (Image)resources.GetObject("BtnInclude.Image");
            BtnInclude.Location = new Point(639, 526);
            BtnInclude.Name = "BtnInclude";
            BtnInclude.Padding = new Padding(2);
            BtnInclude.Size = new Size(26, 26);
            BtnInclude.TabIndex = 13;
            BtnInclude.UseVisualStyleBackColor = true;
            // 
            // lblIncluded
            // 
            lblIncluded.AutoSize = true;
            lblIncluded.Location = new Point(8, 157);
            lblIncluded.Name = "lblIncluded";
            lblIncluded.Size = new Size(50, 14);
            lblIncluded.TabIndex = 14;
            lblIncluded.Text = "Incluídos";
            // 
            // lblExcluded
            // 
            lblExcluded.AutoSize = true;
            lblExcluded.Location = new Point(12, 477);
            lblExcluded.Name = "lblExcluded";
            lblExcluded.Size = new Size(52, 14);
            lblExcluded.TabIndex = 15;
            lblExcluded.Text = "Excluídos";
            // 
            // BtnEmpaquetar
            // 
            BtnEmpaquetar.AutoSize = true;
            BtnEmpaquetar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnEmpaquetar.Font = new Font("Fira Sans Condensed", 9F);
            BtnEmpaquetar.Location = new Point(549, 664);
            BtnEmpaquetar.Name = "BtnEmpaquetar";
            BtnEmpaquetar.Padding = new Padding(2);
            BtnEmpaquetar.Size = new Size(80, 28);
            BtnEmpaquetar.TabIndex = 16;
            BtnEmpaquetar.Text = "Empaquetar";
            BtnEmpaquetar.UseVisualStyleBackColor = true;
            BtnEmpaquetar.Click += BtnEmpaquetar_Click;
            // 
            // BtnSalir
            // 
            BtnSalir.AutoSize = true;
            BtnSalir.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnSalir.Font = new Font("Fira Sans Condensed", 9F);
            BtnSalir.Location = new Point(12, 664);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.Padding = new Padding(2);
            BtnSalir.Size = new Size(43, 28);
            BtnSalir.TabIndex = 17;
            BtnSalir.Text = "Salir";
            BtnSalir.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(639, 435);
            button1.Name = "button1";
            button1.Padding = new Padding(2);
            button1.Size = new Size(26, 26);
            button1.TabIndex = 18;
            button1.UseVisualStyleBackColor = true;
            // 
            // BtnReorder
            // 
            BtnReorder.AutoSize = true;
            BtnReorder.Image = (Image)resources.GetObject("BtnReorder.Image");
            BtnReorder.Location = new Point(639, 200);
            BtnReorder.Name = "BtnReorder";
            BtnReorder.Padding = new Padding(2);
            BtnReorder.Size = new Size(26, 26);
            BtnReorder.TabIndex = 19;
            BtnReorder.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(89, 14);
            label2.TabIndex = 22;
            label2.Text = "Ruta de Entrada:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(12, 26);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(258, 22);
            textBox2.TabIndex = 21;
            textBox2.Text = "/ruta/al/directorio";
            // 
            // BtnBrowseInputFolder
            // 
            BtnBrowseInputFolder.Location = new Point(270, 25);
            BtnBrowseInputFolder.Name = "BtnBrowseInputFolder";
            BtnBrowseInputFolder.Size = new Size(41, 23);
            BtnBrowseInputFolder.TabIndex = 23;
            BtnBrowseInputFolder.Text = "...";
            BtnBrowseInputFolder.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 51);
            label3.Name = "label3";
            label3.Size = new Size(81, 14);
            label3.TabIndex = 24;
            label3.Text = "Ruta de Salida:";
            // 
            // BtnBrowseOutputFolder
            // 
            BtnBrowseOutputFolder.Location = new Point(270, 67);
            BtnBrowseOutputFolder.Name = "BtnBrowseOutputFolder";
            BtnBrowseOutputFolder.Size = new Size(41, 23);
            BtnBrowseOutputFolder.TabIndex = 26;
            BtnBrowseOutputFolder.Text = "...";
            BtnBrowseOutputFolder.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 68);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(258, 22);
            textBox3.TabIndex = 25;
            textBox3.Text = "/ruta/al/directorio";
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.Location = new Point(639, 494);
            button3.Name = "button3";
            button3.Padding = new Padding(2);
            button3.Size = new Size(26, 26);
            button3.TabIndex = 27;
            button3.UseVisualStyleBackColor = true;
            // 
            // colStatus
            // 
            colStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colStatus.HeaderText = "Estado";
            colStatus.Name = "colStatus";
            colStatus.ReadOnly = true;
            colStatus.Width = 150;
            // 
            // colPathGroup
            // 
            colPathGroup.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPathGroup.HeaderText = "Grupo";
            colPathGroup.Name = "colPathGroup";
            colPathGroup.ReadOnly = true;
            colPathGroup.Width = 150;
            // 
            // colFileName
            // 
            colFileName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colFileName.HeaderText = "Archivo";
            colFileName.Name = "colFileName";
            colFileName.ReadOnly = true;
            colFileName.Width = 170;
            // 
            // colDescription
            // 
            colDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDescription.HeaderText = "Descripción";
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            colDescription.Width = 250;
            // 
            // colObs
            // 
            colObs.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colObs.HeaderText = "Observación";
            colObs.Name = "colObs";
            colObs.ReadOnly = true;
            colObs.Width = 250;
            // 
            // colExcludedStatus
            // 
            colExcludedStatus.HeaderText = "Estado";
            colExcludedStatus.Name = "colExcludedStatus";
            colExcludedStatus.ReadOnly = true;
            colExcludedStatus.Width = 156;
            // 
            // colExcludedGroup
            // 
            colExcludedGroup.HeaderText = "Grupo";
            colExcludedGroup.Name = "colExcludedGroup";
            colExcludedGroup.ReadOnly = true;
            colExcludedGroup.Width = 155;
            // 
            // colExcludedFilePath
            // 
            colExcludedFilePath.HeaderText = "Archivo";
            colExcludedFilePath.Name = "colExcludedFilePath";
            colExcludedFilePath.ReadOnly = true;
            colExcludedFilePath.Width = 156;
            // 
            // colExcludedDescripcion
            // 
            colExcludedDescripcion.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colExcludedDescripcion.HeaderText = "Descripción";
            colExcludedDescripcion.Name = "colExcludedDescripcion";
            colExcludedDescripcion.ReadOnly = true;
            colExcludedDescripcion.Width = 250;
            // 
            // FrmBundler
            // 
            AutoScaleDimensions = new SizeF(6F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(679, 704);
            Controls.Add(button3);
            Controls.Add(BtnBrowseOutputFolder);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(BtnBrowseInputFolder);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(BtnReorder);
            Controls.Add(button1);
            Controls.Add(BtnSalir);
            Controls.Add(BtnEmpaquetar);
            Controls.Add(lblExcluded);
            Controls.Add(lblIncluded);
            Controls.Add(BtnInclude);
            Controls.Add(BtnExclude);
            Controls.Add(BtnMoveDown);
            Controls.Add(BtnMoveUp);
            Controls.Add(DgvExcluded);
            Controls.Add(chlbGroup);
            Controls.Add(DgvScripts);
            Controls.Add(BtnProcess);
            Font = new Font("Fira Sans Condensed", 9F);
            Name = "FrmBundler";
            Text = "SQL Script Bundler";
            Load += FrmBundler_Load;
            ((System.ComponentModel.ISupportInitialize)DgvScripts).EndInit();
            ((System.ComponentModel.ISupportInitialize)DgvExcluded).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnProcess;
        private DataGridView DgvScripts;
        private CheckedListBox chlbGroup;
        private DataGridView DgvExcluded;
        private Button BtnMoveUp;
        private Button BtnMoveDown;
        private Button BtnExclude;
        private Button BtnInclude;
        private Label lblIncluded;
        private Label lblExcluded;
        private Button BtnEmpaquetar;
        private Button BtnSalir;
        private Button button1;
        private Button BtnReorder;
        private Label label2;
        private TextBox textBox2;
        private Button BtnBrowseInputFolder;
        private Label label3;
        private Button BtnBrowseOutputFolder;
        private TextBox textBox3;
        private Button button3;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colPathGroup;
        private DataGridViewTextBoxColumn colFileName;
        private DataGridViewTextBoxColumn colDescription;
        private DataGridViewTextBoxColumn colObs;
        private DataGridViewTextBoxColumn colExcludedStatus;
        private DataGridViewTextBoxColumn colExcludedGroup;
        private DataGridViewTextBoxColumn colExcludedFilePath;
        private DataGridViewTextBoxColumn colExcludedDescripcion;
    }
}