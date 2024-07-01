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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBundler));
            BtnProcess = new Button();
            DgvIncludedScripts = new DataGridView();
            colStatus = new DataGridViewTextBoxColumn();
            colPathGroup = new DataGridViewTextBoxColumn();
            colFileName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            colObs = new DataGridViewTextBoxColumn();
            CklGroups = new CheckedListBox();
            DgvExcludedScripts = new DataGridView();
            colExcludedStatus = new DataGridViewTextBoxColumn();
            colExcludedGroup = new DataGridViewTextBoxColumn();
            colExcludedFilePath = new DataGridViewTextBoxColumn();
            colExcludedDescripcion = new DataGridViewTextBoxColumn();
            BtnMoveUp = new Button();
            BtnMoveDown = new Button();
            lblIncluded = new Label();
            lblExcluded = new Label();
            BtnExclude = new Button();
            BtnOrder = new Button();
            lblPath = new Label();
            TxtProjectPath = new TextBox();
            BtnSyncFolder = new Button();
            BtnInclude = new Button();
            BtnMerge = new Button();
            BtnExit = new Button();
            GbxProjectPath = new GroupBox();
            BtnBrowseFolder = new Button();
            StsMain = new StatusStrip();
            TslblSomeInfo = new ToolStripStatusLabel();
            TslblSpace = new ToolStripStatusLabel();
            TslblProcessing = new ToolStripStatusLabel();
            PnlControlButtons = new Panel();
            BtnCancel = new Button();
            CmbProjectBranches = new ComboBox();
            GbxBranchDiff = new GroupBox();
            TxtWorkingBranch = new TextBox();
            lblSource = new Label();
            lblWorkingBranch = new Label();
            GbxGroups = new GroupBox();
            mnsMain = new MenuStrip();
            ChkXmlComment = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)DgvIncludedScripts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DgvExcludedScripts).BeginInit();
            GbxProjectPath.SuspendLayout();
            StsMain.SuspendLayout();
            PnlControlButtons.SuspendLayout();
            GbxBranchDiff.SuspendLayout();
            GbxGroups.SuspendLayout();
            SuspendLayout();
            // 
            // BtnProcess
            // 
            BtnProcess.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            BtnProcess.BackColor = SystemColors.ControlLightLight;
            BtnProcess.FlatStyle = FlatStyle.Flat;
            BtnProcess.Font = new Font("Fira Sans Condensed", 9F);
            BtnProcess.Image = Properties.Resources.Processor;
            BtnProcess.Location = new Point(884, 33);
            BtnProcess.Name = "BtnProcess";
            BtnProcess.Size = new Size(80, 50);
            BtnProcess.TabIndex = 0;
            BtnProcess.Text = "Procesar";
            BtnProcess.TextAlign = ContentAlignment.BottomCenter;
            BtnProcess.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnProcess.UseVisualStyleBackColor = false;
            BtnProcess.Click += BtnProcess_Click;
            // 
            // DgvIncludedScripts
            // 
            DgvIncludedScripts.AllowUserToAddRows = false;
            DgvIncludedScripts.AllowUserToDeleteRows = false;
            DgvIncludedScripts.AllowUserToResizeRows = false;
            DgvIncludedScripts.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DgvIncludedScripts.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Fira Sans Condensed", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DgvIncludedScripts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DgvIncludedScripts.Columns.AddRange(new DataGridViewColumn[] { colStatus, colPathGroup, colFileName, colDescription, colObs });
            DgvIncludedScripts.EnableHeadersVisualStyles = false;
            DgvIncludedScripts.Location = new Point(12, 126);
            DgvIncludedScripts.Name = "DgvIncludedScripts";
            DgvIncludedScripts.ReadOnly = true;
            DgvIncludedScripts.RowHeadersVisible = false;
            DgvIncludedScripts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvIncludedScripts.Size = new Size(868, 269);
            DgvIncludedScripts.TabIndex = 1;
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
            // CklGroups
            // 
            CklGroups.CheckOnClick = true;
            CklGroups.FormattingEnabled = true;
            CklGroups.Location = new Point(6, 18);
            CklGroups.Name = "CklGroups";
            CklGroups.Size = new Size(205, 38);
            CklGroups.TabIndex = 2;
            // 
            // DgvExcludedScripts
            // 
            DgvExcludedScripts.AllowUserToAddRows = false;
            DgvExcludedScripts.AllowUserToDeleteRows = false;
            DgvExcludedScripts.AllowUserToResizeRows = false;
            DgvExcludedScripts.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DgvExcludedScripts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DgvExcludedScripts.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Fira Sans Condensed", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DgvExcludedScripts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DgvExcludedScripts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DgvExcludedScripts.Columns.AddRange(new DataGridViewColumn[] { colExcludedStatus, colExcludedGroup, colExcludedFilePath, colExcludedDescripcion });
            DgvExcludedScripts.EnableHeadersVisualStyles = false;
            DgvExcludedScripts.Location = new Point(12, 420);
            DgvExcludedScripts.Name = "DgvExcludedScripts";
            DgvExcludedScripts.ReadOnly = true;
            DgvExcludedScripts.RowHeadersVisible = false;
            DgvExcludedScripts.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            DgvExcludedScripts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvExcludedScripts.Size = new Size(868, 165);
            DgvExcludedScripts.TabIndex = 7;
            // 
            // colExcludedStatus
            // 
            colExcludedStatus.HeaderText = "Estado";
            colExcludedStatus.Name = "colExcludedStatus";
            colExcludedStatus.ReadOnly = true;
            // 
            // colExcludedGroup
            // 
            colExcludedGroup.HeaderText = "Grupo";
            colExcludedGroup.Name = "colExcludedGroup";
            colExcludedGroup.ReadOnly = true;
            // 
            // colExcludedFilePath
            // 
            colExcludedFilePath.HeaderText = "Archivo";
            colExcludedFilePath.Name = "colExcludedFilePath";
            colExcludedFilePath.ReadOnly = true;
            // 
            // colExcludedDescripcion
            // 
            colExcludedDescripcion.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colExcludedDescripcion.HeaderText = "Descripción";
            colExcludedDescripcion.Name = "colExcludedDescripcion";
            colExcludedDescripcion.ReadOnly = true;
            colExcludedDescripcion.Width = 250;
            // 
            // BtnMoveUp
            // 
            BtnMoveUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnMoveUp.Enabled = false;
            BtnMoveUp.FlatAppearance.BorderColor = SystemColors.ControlDark;
            BtnMoveUp.FlatStyle = FlatStyle.Flat;
            BtnMoveUp.Image = Properties.Resources.Upload;
            BtnMoveUp.Location = new Point(886, 217);
            BtnMoveUp.Name = "BtnMoveUp";
            BtnMoveUp.Padding = new Padding(2);
            BtnMoveUp.Size = new Size(80, 30);
            BtnMoveUp.TabIndex = 10;
            BtnMoveUp.Text = "Subir";
            BtnMoveUp.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnMoveUp.UseVisualStyleBackColor = true;
            BtnMoveUp.Visible = false;
            // 
            // BtnMoveDown
            // 
            BtnMoveDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnMoveDown.AutoSize = true;
            BtnMoveDown.Enabled = false;
            BtnMoveDown.FlatAppearance.BorderColor = SystemColors.ControlDark;
            BtnMoveDown.FlatStyle = FlatStyle.Flat;
            BtnMoveDown.Image = Properties.Resources.Download;
            BtnMoveDown.Location = new Point(886, 253);
            BtnMoveDown.Name = "BtnMoveDown";
            BtnMoveDown.Padding = new Padding(2);
            BtnMoveDown.Size = new Size(80, 30);
            BtnMoveDown.TabIndex = 11;
            BtnMoveDown.Text = "Bajar";
            BtnMoveDown.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnMoveDown.UseVisualStyleBackColor = true;
            BtnMoveDown.Visible = false;
            // 
            // lblIncluded
            // 
            lblIncluded.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblIncluded.AutoSize = true;
            lblIncluded.Location = new Point(12, 109);
            lblIncluded.Name = "lblIncluded";
            lblIncluded.Size = new Size(50, 14);
            lblIncluded.TabIndex = 14;
            lblIncluded.Text = "Incluídos";
            // 
            // lblExcluded
            // 
            lblExcluded.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblExcluded.AutoSize = true;
            lblExcluded.Location = new Point(12, 403);
            lblExcluded.Name = "lblExcluded";
            lblExcluded.Size = new Size(52, 14);
            lblExcluded.TabIndex = 15;
            lblExcluded.Text = "Excluídos";
            // 
            // BtnExclude
            // 
            BtnExclude.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnExclude.AutoSize = true;
            BtnExclude.FlatAppearance.BorderColor = SystemColors.ControlDark;
            BtnExclude.FlatStyle = FlatStyle.Flat;
            BtnExclude.Image = Properties.Resources.ExpandDown;
            BtnExclude.Location = new Point(886, 365);
            BtnExclude.Name = "BtnExclude";
            BtnExclude.Padding = new Padding(2);
            BtnExclude.Size = new Size(80, 30);
            BtnExclude.TabIndex = 18;
            BtnExclude.Text = "Excluir";
            BtnExclude.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnExclude.UseVisualStyleBackColor = true;
            BtnExclude.Click += BtnExclude_Click;
            // 
            // BtnOrder
            // 
            BtnOrder.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnOrder.Enabled = false;
            BtnOrder.FlatAppearance.BorderColor = SystemColors.ControlDark;
            BtnOrder.FlatStyle = FlatStyle.Flat;
            BtnOrder.Image = Properties.Resources.SortingByGrouping;
            BtnOrder.Location = new Point(886, 126);
            BtnOrder.Name = "BtnOrder";
            BtnOrder.Padding = new Padding(2);
            BtnOrder.Size = new Size(80, 30);
            BtnOrder.TabIndex = 19;
            BtnOrder.Text = "Ordenar";
            BtnOrder.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnOrder.UseVisualStyleBackColor = true;
            BtnOrder.Visible = false;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Font = new Font("Fira Sans Condensed", 8F);
            lblPath.Location = new Point(6, 18);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(30, 13);
            lblPath.TabIndex = 22;
            lblPath.Text = "Ruta:";
            // 
            // TxtProjectPath
            // 
            TxtProjectPath.BackColor = SystemColors.ControlLight;
            TxtProjectPath.Font = new Font("Fira Sans Condensed", 9F);
            TxtProjectPath.Location = new Point(6, 34);
            TxtProjectPath.Margin = new Padding(3, 3, 0, 3);
            TxtProjectPath.Name = "TxtProjectPath";
            TxtProjectPath.ReadOnly = true;
            TxtProjectPath.Size = new Size(279, 22);
            TxtProjectPath.TabIndex = 21;
            // 
            // BtnSyncFolder
            // 
            BtnSyncFolder.AutoSize = true;
            BtnSyncFolder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnSyncFolder.FlatStyle = FlatStyle.Flat;
            BtnSyncFolder.Image = Properties.Resources.Sync;
            BtnSyncFolder.Location = new Point(145, 33);
            BtnSyncFolder.Margin = new Padding(0, 3, 3, 3);
            BtnSyncFolder.Name = "BtnSyncFolder";
            BtnSyncFolder.Size = new Size(24, 24);
            BtnSyncFolder.TabIndex = 23;
            BtnSyncFolder.UseVisualStyleBackColor = false;
            BtnSyncFolder.Click += BtnSyncFolder_Click;
            // 
            // BtnInclude
            // 
            BtnInclude.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            BtnInclude.AutoSize = true;
            BtnInclude.FlatAppearance.BorderColor = SystemColors.ControlDark;
            BtnInclude.FlatStyle = FlatStyle.Flat;
            BtnInclude.Image = Properties.Resources.CollapseUp;
            BtnInclude.Location = new Point(886, 420);
            BtnInclude.Name = "BtnInclude";
            BtnInclude.Padding = new Padding(2);
            BtnInclude.Size = new Size(80, 30);
            BtnInclude.TabIndex = 27;
            BtnInclude.Text = "Incluir";
            BtnInclude.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnInclude.UseVisualStyleBackColor = true;
            BtnInclude.Click += BtnInclude_Click;
            // 
            // BtnMerge
            // 
            BtnMerge.BackColor = SystemColors.ControlLightLight;
            BtnMerge.FlatStyle = FlatStyle.Flat;
            BtnMerge.Font = new Font("Fira Sans Condensed", 9F);
            BtnMerge.Image = Properties.Resources.Compile;
            BtnMerge.Location = new Point(12, 8);
            BtnMerge.Name = "BtnMerge";
            BtnMerge.Size = new Size(80, 50);
            BtnMerge.TabIndex = 28;
            BtnMerge.Text = "Empacar";
            BtnMerge.TextAlign = ContentAlignment.BottomCenter;
            BtnMerge.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnMerge.UseVisualStyleBackColor = false;
            BtnMerge.Click += BtnMerge_Click;
            // 
            // BtnExit
            // 
            BtnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnExit.BackColor = SystemColors.ControlLightLight;
            BtnExit.FlatStyle = FlatStyle.Flat;
            BtnExit.Font = new Font("Fira Sans Condensed", 9F);
            BtnExit.Image = Properties.Resources.Output;
            BtnExit.Location = new Point(886, 8);
            BtnExit.Name = "BtnExit";
            BtnExit.Size = new Size(80, 50);
            BtnExit.TabIndex = 29;
            BtnExit.Text = "Salir";
            BtnExit.TextAlign = ContentAlignment.BottomCenter;
            BtnExit.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnExit.UseVisualStyleBackColor = false;
            BtnExit.Click += BtnExit_Click;
            // 
            // GbxProjectPath
            // 
            GbxProjectPath.AutoSize = true;
            GbxProjectPath.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            GbxProjectPath.Controls.Add(BtnBrowseFolder);
            GbxProjectPath.Controls.Add(lblPath);
            GbxProjectPath.Controls.Add(TxtProjectPath);
            GbxProjectPath.Font = new Font("Fira Sans Condensed", 9F, FontStyle.Bold);
            GbxProjectPath.Location = new Point(12, 27);
            GbxProjectPath.Name = "GbxProjectPath";
            GbxProjectPath.Size = new Size(321, 78);
            GbxProjectPath.TabIndex = 30;
            GbxProjectPath.TabStop = false;
            GbxProjectPath.Text = "Carpeta de Trabajo";
            // 
            // BtnBrowseFolder
            // 
            BtnBrowseFolder.AutoSize = true;
            BtnBrowseFolder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnBrowseFolder.FlatStyle = FlatStyle.Flat;
            BtnBrowseFolder.Image = Properties.Resources.FolderBrowserDialogControl;
            BtnBrowseFolder.Location = new Point(291, 33);
            BtnBrowseFolder.Margin = new Padding(0, 3, 3, 3);
            BtnBrowseFolder.Name = "BtnBrowseFolder";
            BtnBrowseFolder.Size = new Size(24, 24);
            BtnBrowseFolder.TabIndex = 24;
            BtnBrowseFolder.UseVisualStyleBackColor = false;
            BtnBrowseFolder.Click += BtnBrowseFolder_Click;
            // 
            // StsMain
            // 
            StsMain.Items.AddRange(new ToolStripItem[] { TslblSomeInfo, TslblSpace, TslblProcessing });
            StsMain.Location = new Point(0, 657);
            StsMain.Name = "StsMain";
            StsMain.Size = new Size(978, 22);
            StsMain.TabIndex = 31;
            StsMain.Text = "statusStrip1";
            // 
            // TslblSomeInfo
            // 
            TslblSomeInfo.Name = "TslblSomeInfo";
            TslblSomeInfo.Size = new Size(118, 17);
            TslblSomeInfo.Text = "toolStripStatusLabel1";
            TslblSomeInfo.Visible = false;
            // 
            // TslblSpace
            // 
            TslblSpace.Name = "TslblSpace";
            TslblSpace.Size = new Size(963, 17);
            TslblSpace.Spring = true;
            // 
            // TslblProcessing
            // 
            TslblProcessing.Image = (Image)resources.GetObject("TslblProcessing.Image");
            TslblProcessing.Name = "TslblProcessing";
            TslblProcessing.Size = new Size(94, 17);
            TslblProcessing.Text = "Procesando...";
            TslblProcessing.Visible = false;
            // 
            // PnlControlButtons
            // 
            PnlControlButtons.BackColor = SystemColors.ScrollBar;
            PnlControlButtons.Controls.Add(BtnCancel);
            PnlControlButtons.Controls.Add(BtnMerge);
            PnlControlButtons.Controls.Add(BtnExit);
            PnlControlButtons.Dock = DockStyle.Bottom;
            PnlControlButtons.Location = new Point(0, 591);
            PnlControlButtons.Name = "PnlControlButtons";
            PnlControlButtons.Size = new Size(978, 66);
            PnlControlButtons.TabIndex = 32;
            // 
            // BtnCancel
            // 
            BtnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            BtnCancel.BackColor = SystemColors.ControlLightLight;
            BtnCancel.FlatStyle = FlatStyle.Flat;
            BtnCancel.Font = new Font("Fira Sans Condensed", 9F);
            BtnCancel.Image = Properties.Resources.Cancel;
            BtnCancel.Location = new Point(800, 8);
            BtnCancel.Name = "BtnCancel";
            BtnCancel.Size = new Size(80, 50);
            BtnCancel.TabIndex = 30;
            BtnCancel.Text = "Cancelar";
            BtnCancel.TextAlign = ContentAlignment.BottomCenter;
            BtnCancel.TextImageRelation = TextImageRelation.ImageAboveText;
            BtnCancel.UseVisualStyleBackColor = false;
            BtnCancel.Visible = false;
            BtnCancel.Click += BtnCancel_Click;
            // 
            // CmbProjectBranches
            // 
            CmbProjectBranches.DropDownStyle = ComboBoxStyle.DropDownList;
            CmbProjectBranches.FormattingEnabled = true;
            CmbProjectBranches.Items.AddRange(new object[] { "develop" });
            CmbProjectBranches.Location = new Point(6, 34);
            CmbProjectBranches.Name = "CmbProjectBranches";
            CmbProjectBranches.Size = new Size(136, 22);
            CmbProjectBranches.TabIndex = 33;
            // 
            // GbxBranchDiff
            // 
            GbxBranchDiff.AutoSize = true;
            GbxBranchDiff.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            GbxBranchDiff.Controls.Add(TxtWorkingBranch);
            GbxBranchDiff.Controls.Add(lblSource);
            GbxBranchDiff.Controls.Add(lblWorkingBranch);
            GbxBranchDiff.Controls.Add(BtnSyncFolder);
            GbxBranchDiff.Controls.Add(CmbProjectBranches);
            GbxBranchDiff.Location = new Point(341, 27);
            GbxBranchDiff.Name = "GbxBranchDiff";
            GbxBranchDiff.Size = new Size(313, 78);
            GbxBranchDiff.TabIndex = 35;
            GbxBranchDiff.TabStop = false;
            GbxBranchDiff.Text = "Dif. Ramas";
            // 
            // TxtWorkingBranch
            // 
            TxtWorkingBranch.Location = new Point(174, 34);
            TxtWorkingBranch.Name = "TxtWorkingBranch";
            TxtWorkingBranch.ReadOnly = true;
            TxtWorkingBranch.Size = new Size(133, 22);
            TxtWorkingBranch.TabIndex = 38;
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Font = new Font("Fira Sans Condensed", 8F);
            lblSource.Location = new Point(6, 18);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(40, 13);
            lblSource.TabIndex = 36;
            lblSource.Text = "Source:";
            // 
            // lblWorkingBranch
            // 
            lblWorkingBranch.AutoSize = true;
            lblWorkingBranch.Font = new Font("Fira Sans Condensed", 8F);
            lblWorkingBranch.Location = new Point(174, 18);
            lblWorkingBranch.Name = "lblWorkingBranch";
            lblWorkingBranch.Size = new Size(38, 13);
            lblWorkingBranch.TabIndex = 37;
            lblWorkingBranch.Text = "Actual:";
            // 
            // GbxGroups
            // 
            GbxGroups.AutoSize = true;
            GbxGroups.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            GbxGroups.Controls.Add(CklGroups);
            GbxGroups.Location = new Point(661, 27);
            GbxGroups.Name = "GbxGroups";
            GbxGroups.Size = new Size(217, 77);
            GbxGroups.TabIndex = 36;
            GbxGroups.TabStop = false;
            GbxGroups.Text = "Agrupamiento";
            // 
            // mnsMain
            // 
            mnsMain.Location = new Point(0, 0);
            mnsMain.Name = "mnsMain";
            mnsMain.Size = new Size(978, 24);
            mnsMain.TabIndex = 38;
            mnsMain.Text = "menuStrip1";
            // 
            // ChkXmlComment
            // 
            ChkXmlComment.AutoSize = true;
            ChkXmlComment.Location = new Point(884, 89);
            ChkXmlComment.Name = "ChkXmlComment";
            ChkXmlComment.Size = new Size(73, 18);
            ChkXmlComment.TabIndex = 39;
            ChkXmlComment.Text = "XML Com.";
            ChkXmlComment.UseVisualStyleBackColor = true;
            // 
            // FrmBundler
            // 
            AutoScaleDimensions = new SizeF(6F, 14F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(978, 679);
            Controls.Add(ChkXmlComment);
            Controls.Add(PnlControlButtons);
            Controls.Add(GbxGroups);
            Controls.Add(GbxBranchDiff);
            Controls.Add(StsMain);
            Controls.Add(mnsMain);
            Controls.Add(GbxProjectPath);
            Controls.Add(BtnInclude);
            Controls.Add(BtnOrder);
            Controls.Add(BtnExclude);
            Controls.Add(lblExcluded);
            Controls.Add(lblIncluded);
            Controls.Add(BtnMoveDown);
            Controls.Add(BtnMoveUp);
            Controls.Add(DgvExcludedScripts);
            Controls.Add(DgvIncludedScripts);
            Controls.Add(BtnProcess);
            Font = new Font("Fira Sans Condensed", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = mnsMain;
            MaximizeBox = false;
            Name = "FrmBundler";
            Text = "SQL Script Bundler";
            FormClosing += FrmBundler_FormClosing;
            Load += FrmBundler_Load;
            ((System.ComponentModel.ISupportInitialize)DgvIncludedScripts).EndInit();
            ((System.ComponentModel.ISupportInitialize)DgvExcludedScripts).EndInit();
            GbxProjectPath.ResumeLayout(false);
            GbxProjectPath.PerformLayout();
            StsMain.ResumeLayout(false);
            StsMain.PerformLayout();
            PnlControlButtons.ResumeLayout(false);
            GbxBranchDiff.ResumeLayout(false);
            GbxBranchDiff.PerformLayout();
            GbxGroups.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnProcess;
        private DataGridView DgvIncludedScripts;
        private CheckedListBox CklGroups;
        private DataGridView DgvExcludedScripts;
        private Button BtnMoveUp;
        private Button BtnMoveDown;
        private Label lblIncluded;
        private Label lblExcluded;
        private Button BtnExclude;
        private Button BtnOrder;
        private Label lblPath;
        private TextBox TxtProjectPath;
        private Button BtnSyncFolder;
        private Button BtnInclude;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colPathGroup;
        private DataGridViewTextBoxColumn colFileName;
        private DataGridViewTextBoxColumn colDescription;
        private DataGridViewTextBoxColumn colObs;
        private DataGridViewTextBoxColumn colExcludedStatus;
        private DataGridViewTextBoxColumn colExcludedGroup;
        private DataGridViewTextBoxColumn colExcludedFilePath;
        private DataGridViewTextBoxColumn colExcludedDescripcion;
        private Button BtnMerge;
        private Button BtnExit;
        private GroupBox GbxProjectPath;
        private StatusStrip StsMain;
        private Panel PnlControlButtons;
        private ComboBox CmbProjectBranches;
        private GroupBox GbxBranchDiff;
        private Label lblSource;
        private Label lblWorkingBranch;
        private ToolStripStatusLabel TslblSomeInfo;
        private ToolStripStatusLabel TslblProcessing;
        private ToolStripStatusLabel TslblSpace;
        private GroupBox GbxGroups;
        private Button BtnBrowseFolder;
        private TextBox TxtWorkingBranch;
        private MenuStrip mnsMain;
        private Button BtnCancel;
        private CheckBox ChkXmlComment;
    }
}