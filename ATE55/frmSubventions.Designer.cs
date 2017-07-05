using System.Drawing;
namespace ATE55 {
    partial class frmSubventions {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubventions));
            this.toolStripConventions = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonFichier = new System.Windows.Forms.ToolStripSplitButton();
            this.quitterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonActualiser = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_Session = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSplitExtraire = new System.Windows.Forms.ToolStripSplitButton();
            this.subventionsDeLannéeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subventionsSoldéesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subventionsNonSoldéesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainerSubvention = new System.Windows.Forms.SplitContainer();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.comboRechercheType = new CustomComboBox.CustomComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textRecherche = new System.Windows.Forms.TextBox();
            this.checkAfficherDossiersProgrammes = new System.Windows.Forms.CheckBox();
            this.dataGridViewSubventions = new System.Windows.Forms.DataGridView();
            this.idSubvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CollectiviteSubvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateReceptionDemandeSubvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationSubvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeSubvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EtatSubvention = new System.Windows.Forms.DataGridViewImageColumn();
            this.menuStripDataGridSubventions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUnDossierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripRowSubventions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUnDossierToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerLeDossierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSubvention = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl5 = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.numericSubventionAE = new System.Windows.Forms.NumericUpDown();
            this.calculerSubvAEBouton = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.numericTauxAE = new System.Windows.Forms.NumericUpDown();
            this.numericDSHTAE = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.labelDateAR = new System.Windows.Forms.Label();
            this.tabControl4 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.numericSubventionSUR = new System.Windows.Forms.NumericUpDown();
            this.calculerSubvSURBouton = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.numericTauxSUR = new System.Windows.Forms.NumericUpDown();
            this.numericDSHTSUR = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.dateARDossierComplet = new System.Windows.Forms.DateTimePicker();
            this.numericNbHeures = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.numericSubventionDpt = new System.Windows.Forms.NumericUpDown();
            this.calculerSubvDptBouton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericTauxDpt = new System.Windows.Forms.NumericUpDown();
            this.numericDSHTDpt = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelNbheures = new System.Windows.Forms.Label();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.numericSubventionGIP = new System.Windows.Forms.NumericUpDown();
            this.calculerSubvGIPBouton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numericTauxGIP = new System.Windows.Forms.NumericUpDown();
            this.numericDSHTGIP = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.checkClauses = new System.Windows.Forms.CheckBox();
            this.labelDateProg = new System.Windows.Forms.Label();
            this.dateProgrammation = new System.Windows.Forms.DateTimePicker();
            this.infosModifSubvention = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textCommentairesSubvention = new System.Windows.Forms.TextBox();
            this.numericPP = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.dataGridViewCollectivitesSubvention = new System.Windows.Forms.DataGridView();
            this.CodeCollectiviteSubvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomCollectiviteSubvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PopDGFSubvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripCollectiviteSubvention = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUneCollectivitéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripRowCollectiviteSubvention = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUneCollectivitéToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerLaCollectivitéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.enregistrerSubventionBouton = new System.Windows.Forms.Button();
            this.annulerSubventionBouton = new System.Windows.Forms.Button();
            this.comboTypeDossier = new CustomComboBox.CustomComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboCollectiviteSubvention = new System.Windows.Forms.ComboBox();
            this.dateReceptionDemande = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textOperation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboEtatSubvention = new CustomComboBox.CustomComboBox();
            this.imageListSubventions = new System.Windows.Forms.ImageList(this.components);
            this.toolStripConventions.SuspendLayout();
            this.splitContainerSubvention.Panel1.SuspendLayout();
            this.splitContainerSubvention.Panel2.SuspendLayout();
            this.splitContainerSubvention.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubventions)).BeginInit();
            this.menuStripDataGridSubventions.SuspendLayout();
            this.menuStripRowSubventions.SuspendLayout();
            this.tabSubvention.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSubventionAE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTauxAE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDSHTAE)).BeginInit();
            this.tabControl4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSubventionSUR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTauxSUR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDSHTSUR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNbHeures)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSubventionDpt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTauxDpt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDSHTDpt)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSubventionGIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTauxGIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDSHTGIP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPP)).BeginInit();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollectivitesSubvention)).BeginInit();
            this.menuStripCollectiviteSubvention.SuspendLayout();
            this.menuStripRowCollectiviteSubvention.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripConventions
            // 
            this.toolStripConventions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonFichier,
            this.toolStripButtonActualiser,
            this.toolStripLabel_Session,
            this.toolStripSplitExtraire});
            this.toolStripConventions.Location = new System.Drawing.Point(0, 0);
            this.toolStripConventions.Name = "toolStripConventions";
            this.toolStripConventions.Size = new System.Drawing.Size(1064, 25);
            this.toolStripConventions.TabIndex = 11;
            this.toolStripConventions.Text = "toolStrip1";
            // 
            // toolStripSplitButtonFichier
            // 
            this.toolStripSplitButtonFichier.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitterToolStripMenuItem1});
            this.toolStripSplitButtonFichier.Image = global::ATE55.Properties.Resources.Valeur_Modif1;
            this.toolStripSplitButtonFichier.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonFichier.Name = "toolStripSplitButtonFichier";
            this.toolStripSplitButtonFichier.Size = new System.Drawing.Size(74, 22);
            this.toolStripSplitButtonFichier.Text = "Fichier";
            // 
            // quitterToolStripMenuItem1
            // 
            this.quitterToolStripMenuItem1.Name = "quitterToolStripMenuItem1";
            this.quitterToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitterToolStripMenuItem1.Size = new System.Drawing.Size(154, 22);
            this.quitterToolStripMenuItem1.Text = "Quitter";
            this.quitterToolStripMenuItem1.Click += new System.EventHandler(this.quitterToolStripMenuItem1_Click);
            // 
            // toolStripButtonActualiser
            // 
            this.toolStripButtonActualiser.Image = global::ATE55.Properties.Resources.Refresh;
            this.toolStripButtonActualiser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonActualiser.Name = "toolStripButtonActualiser";
            this.toolStripButtonActualiser.Size = new System.Drawing.Size(79, 22);
            this.toolStripButtonActualiser.Text = "Actualiser";
            this.toolStripButtonActualiser.Click += new System.EventHandler(this.toolStripButtonActualiser_Click);
            // 
            // toolStripLabel_Session
            // 
            this.toolStripLabel_Session.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_Session.Name = "toolStripLabel_Session";
            this.toolStripLabel_Session.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel_Session.Text = "...";
            this.toolStripLabel_Session.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSplitExtraire
            // 
            this.toolStripSplitExtraire.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subventionsDeLannéeToolStripMenuItem,
            this.subventionsSoldéesToolStripMenuItem,
            this.subventionsNonSoldéesToolStripMenuItem});
            this.toolStripSplitExtraire.Image = global::ATE55.Properties.Resources.excel;
            this.toolStripSplitExtraire.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitExtraire.Name = "toolStripSplitExtraire";
            this.toolStripSplitExtraire.Size = new System.Drawing.Size(77, 22);
            this.toolStripSplitExtraire.Text = "Extraire";
            // 
            // subventionsDeLannéeToolStripMenuItem
            // 
            this.subventionsDeLannéeToolStripMenuItem.Image = global::ATE55.Properties.Resources.excel;
            this.subventionsDeLannéeToolStripMenuItem.Name = "subventionsDeLannéeToolStripMenuItem";
            this.subventionsDeLannéeToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.subventionsDeLannéeToolStripMenuItem.Text = "Subventions de l\'année";
            this.subventionsDeLannéeToolStripMenuItem.Click += new System.EventHandler(this.subventionsDeLannéeToolStripMenuItem_Click);
            // 
            // subventionsSoldéesToolStripMenuItem
            // 
            this.subventionsSoldéesToolStripMenuItem.Image = global::ATE55.Properties.Resources.excel;
            this.subventionsSoldéesToolStripMenuItem.Name = "subventionsSoldéesToolStripMenuItem";
            this.subventionsSoldéesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.subventionsSoldéesToolStripMenuItem.Text = "Subventions soldées";
            this.subventionsSoldéesToolStripMenuItem.Click += new System.EventHandler(this.subventionsSoldéesToolStripMenuItem_Click);
            // 
            // subventionsNonSoldéesToolStripMenuItem
            // 
            this.subventionsNonSoldéesToolStripMenuItem.Image = global::ATE55.Properties.Resources.excel;
            this.subventionsNonSoldéesToolStripMenuItem.Name = "subventionsNonSoldéesToolStripMenuItem";
            this.subventionsNonSoldéesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.subventionsNonSoldéesToolStripMenuItem.Text = "Subventions non soldées";
            this.subventionsNonSoldéesToolStripMenuItem.Click += new System.EventHandler(this.subventionsNonSoldéesToolStripMenuItem_Click);
            // 
            // splitContainerSubvention
            // 
            this.splitContainerSubvention.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerSubvention.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSubvention.Location = new System.Drawing.Point(0, 25);
            this.splitContainerSubvention.Name = "splitContainerSubvention";
            // 
            // splitContainerSubvention.Panel1
            // 
            this.splitContainerSubvention.Panel1.Controls.Add(this.panel7);
            this.splitContainerSubvention.Panel1.Controls.Add(this.dataGridViewSubventions);
            // 
            // splitContainerSubvention.Panel2
            // 
            this.splitContainerSubvention.Panel2.Controls.Add(this.tabSubvention);
            this.splitContainerSubvention.Size = new System.Drawing.Size(1064, 695);
            this.splitContainerSubvention.SplitterDistance = 478;
            this.splitContainerSubvention.TabIndex = 12;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.label33);
            this.panel7.Controls.Add(this.comboRechercheType);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.textRecherche);
            this.panel7.Controls.Add(this.checkAfficherDossiersProgrammes);
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(476, 72);
            this.panel7.TabIndex = 4;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(131, 45);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(43, 13);
            this.label33.TabIndex = 5;
            this.label33.Text = "Type :";
            // 
            // comboRechercheType
            // 
            this.comboRechercheType.AlignText = false;
            this.comboRechercheType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRechercheType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboRechercheType.FormattingEnabled = true;
            this.comboRechercheType.Location = new System.Drawing.Point(180, 40);
            this.comboRechercheType.Name = "comboRechercheType";
            this.comboRechercheType.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboRechercheType.Size = new System.Drawing.Size(128, 21);
            this.comboRechercheType.TabIndex = 4;
            this.comboRechercheType.SelectedIndexChanged += new System.EventHandler(this.comboRechercheType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Recherche :";
            // 
            // textRecherche
            // 
            this.textRecherche.Location = new System.Drawing.Point(87, 10);
            this.textRecherche.Name = "textRecherche";
            this.textRecherche.Size = new System.Drawing.Size(138, 20);
            this.textRecherche.TabIndex = 3;
            this.textRecherche.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textRecherche_KeyUp);
            // 
            // checkAfficherDossiersProgrammes
            // 
            this.checkAfficherDossiersProgrammes.AutoSize = true;
            this.checkAfficherDossiersProgrammes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAfficherDossiersProgrammes.Location = new System.Drawing.Point(249, 12);
            this.checkAfficherDossiersProgrammes.Name = "checkAfficherDossiersProgrammes";
            this.checkAfficherDossiersProgrammes.Size = new System.Drawing.Size(168, 17);
            this.checkAfficherDossiersProgrammes.TabIndex = 1;
            this.checkAfficherDossiersProgrammes.Text = "Afficher tous les dossiers";
            this.checkAfficherDossiersProgrammes.UseVisualStyleBackColor = true;
            this.checkAfficherDossiersProgrammes.CheckedChanged += new System.EventHandler(this.checkAfficherDossiersProgrammes_CheckedChanged);
            // 
            // dataGridViewSubventions
            // 
            this.dataGridViewSubventions.AllowUserToAddRows = false;
            this.dataGridViewSubventions.AllowUserToDeleteRows = false;
            this.dataGridViewSubventions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSubventions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSubventions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idSubvention,
            this.CollectiviteSubvention,
            this.DateReceptionDemandeSubvention,
            this.OperationSubvention,
            this.TypeSubvention,
            this.EtatSubvention});
            this.dataGridViewSubventions.ContextMenuStrip = this.menuStripDataGridSubventions;
            this.dataGridViewSubventions.Location = new System.Drawing.Point(0, 72);
            this.dataGridViewSubventions.MultiSelect = false;
            this.dataGridViewSubventions.Name = "dataGridViewSubventions";
            this.dataGridViewSubventions.ReadOnly = true;
            this.dataGridViewSubventions.RowHeadersVisible = false;
            this.dataGridViewSubventions.RowTemplate.ContextMenuStrip = this.menuStripRowSubventions;
            this.dataGridViewSubventions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSubventions.Size = new System.Drawing.Size(476, 621);
            this.dataGridViewSubventions.TabIndex = 0;
            this.dataGridViewSubventions.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewSubventions_RowStateChanged);
            this.dataGridViewSubventions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewSubventions_MouseDown);
            // 
            // idSubvention
            // 
            this.idSubvention.HeaderText = "idSubvention";
            this.idSubvention.Name = "idSubvention";
            this.idSubvention.ReadOnly = true;
            this.idSubvention.Visible = false;
            // 
            // CollectiviteSubvention
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CollectiviteSubvention.DefaultCellStyle = dataGridViewCellStyle1;
            this.CollectiviteSubvention.HeaderText = "Collectivité";
            this.CollectiviteSubvention.Name = "CollectiviteSubvention";
            this.CollectiviteSubvention.ReadOnly = true;
            this.CollectiviteSubvention.Width = 155;
            // 
            // DateReceptionDemandeSubvention
            // 
            this.DateReceptionDemandeSubvention.HeaderText = "Réception demande";
            this.DateReceptionDemandeSubvention.Name = "DateReceptionDemandeSubvention";
            this.DateReceptionDemandeSubvention.ReadOnly = true;
            this.DateReceptionDemandeSubvention.Width = 67;
            // 
            // OperationSubvention
            // 
            this.OperationSubvention.HeaderText = "Opération";
            this.OperationSubvention.Name = "OperationSubvention";
            this.OperationSubvention.ReadOnly = true;
            this.OperationSubvention.Width = 150;
            // 
            // TypeSubvention
            // 
            this.TypeSubvention.HeaderText = "Type";
            this.TypeSubvention.Name = "TypeSubvention";
            this.TypeSubvention.ReadOnly = true;
            this.TypeSubvention.Width = 50;
            // 
            // EtatSubvention
            // 
            this.EtatSubvention.HeaderText = "Etat";
            this.EtatSubvention.Name = "EtatSubvention";
            this.EtatSubvention.ReadOnly = true;
            this.EtatSubvention.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EtatSubvention.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.EtatSubvention.Width = 29;
            // 
            // menuStripDataGridSubventions
            // 
            this.menuStripDataGridSubventions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUnDossierToolStripMenuItem});
            this.menuStripDataGridSubventions.Name = "menuStripDataGridSubventions";
            this.menuStripDataGridSubventions.Size = new System.Drawing.Size(171, 26);
            // 
            // ajouterUnDossierToolStripMenuItem
            // 
            this.ajouterUnDossierToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUnDossierToolStripMenuItem.Name = "ajouterUnDossierToolStripMenuItem";
            this.ajouterUnDossierToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.ajouterUnDossierToolStripMenuItem.Text = "Ajouter un dossier";
            this.ajouterUnDossierToolStripMenuItem.Click += new System.EventHandler(this.ajouterUnDossierToolStripMenuItem_Click);
            // 
            // menuStripRowSubventions
            // 
            this.menuStripRowSubventions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUnDossierToolStripMenuItem1,
            this.supprimerLeDossierToolStripMenuItem});
            this.menuStripRowSubventions.Name = "menuStripRowSubventions";
            this.menuStripRowSubventions.Size = new System.Drawing.Size(182, 48);
            // 
            // ajouterUnDossierToolStripMenuItem1
            // 
            this.ajouterUnDossierToolStripMenuItem1.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUnDossierToolStripMenuItem1.Name = "ajouterUnDossierToolStripMenuItem1";
            this.ajouterUnDossierToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.ajouterUnDossierToolStripMenuItem1.Text = "Ajouter un dossier";
            this.ajouterUnDossierToolStripMenuItem1.Click += new System.EventHandler(this.ajouterUnDossierToolStripMenuItem_Click);
            // 
            // supprimerLeDossierToolStripMenuItem
            // 
            this.supprimerLeDossierToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Suppr;
            this.supprimerLeDossierToolStripMenuItem.Name = "supprimerLeDossierToolStripMenuItem";
            this.supprimerLeDossierToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.supprimerLeDossierToolStripMenuItem.Text = "Supprimer le dossier";
            this.supprimerLeDossierToolStripMenuItem.Click += new System.EventHandler(this.supprimerLeDossierToolStripMenuItem_Click);
            // 
            // tabSubvention
            // 
            this.tabSubvention.Controls.Add(this.tabPage1);
            this.tabSubvention.Location = new System.Drawing.Point(0, 0);
            this.tabSubvention.Name = "tabSubvention";
            this.tabSubvention.SelectedIndex = 0;
            this.tabSubvention.Size = new System.Drawing.Size(581, 694);
            this.tabSubvention.TabIndex = 0;
            this.tabSubvention.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(573, 668);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Demande de Subvention";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tabControl5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(613, 668);
            this.panel1.TabIndex = 132;
            // 
            // tabControl5
            // 
            this.tabControl5.Controls.Add(this.tabPage6);
            this.tabControl5.Controls.Add(this.tabPage7);
            this.tabControl5.Location = new System.Drawing.Point(0, 159);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(574, 508);
            this.tabControl5.TabIndex = 4;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.tabControl3);
            this.tabPage6.Controls.Add(this.labelDateAR);
            this.tabPage6.Controls.Add(this.tabControl4);
            this.tabPage6.Controls.Add(this.dateARDossierComplet);
            this.tabPage6.Controls.Add(this.numericNbHeures);
            this.tabPage6.Controls.Add(this.tabControl1);
            this.tabPage6.Controls.Add(this.labelNbheures);
            this.tabPage6.Controls.Add(this.tabControl2);
            this.tabPage6.Controls.Add(this.checkClauses);
            this.tabPage6.Controls.Add(this.labelDateProg);
            this.tabPage6.Controls.Add(this.dateProgrammation);
            this.tabPage6.Controls.Add(this.infosModifSubvention);
            this.tabPage6.Controls.Add(this.label18);
            this.tabPage6.Controls.Add(this.textCommentairesSubvention);
            this.tabPage6.Controls.Add(this.numericPP);
            this.tabPage6.Controls.Add(this.label19);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(566, 482);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "Informations";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Location = new System.Drawing.Point(289, 194);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(270, 159);
            this.tabControl3.TabIndex = 156;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.panel5);
            this.tabPage4.Controls.Add(this.label23);
            this.tabPage4.Controls.Add(this.label24);
            this.tabPage4.Controls.Add(this.numericTauxAE);
            this.tabPage4.Controls.Add(this.numericDSHTAE);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.label26);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(262, 133);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "AE";
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label16);
            this.panel5.Controls.Add(this.numericSubventionAE);
            this.panel5.Controls.Add(this.calculerSubvAEBouton);
            this.panel5.Controls.Add(this.label17);
            this.panel5.Location = new System.Drawing.Point(0, 71);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(262, 62);
            this.panel5.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(203, 38);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(14, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "€";
            // 
            // numericSubventionAE
            // 
            this.numericSubventionAE.DecimalPlaces = 2;
            this.numericSubventionAE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericSubventionAE.ForeColor = System.Drawing.Color.Blue;
            this.numericSubventionAE.Location = new System.Drawing.Point(90, 35);
            this.numericSubventionAE.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericSubventionAE.Name = "numericSubventionAE";
            this.numericSubventionAE.Size = new System.Drawing.Size(112, 20);
            this.numericSubventionAE.TabIndex = 4;
            this.numericSubventionAE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSubventionAE.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // calculerSubvAEBouton
            // 
            this.calculerSubvAEBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculerSubvAEBouton.Image = global::ATE55.Properties.Resources.entreprise_16;
            this.calculerSubvAEBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.calculerSubvAEBouton.Location = new System.Drawing.Point(97, 5);
            this.calculerSubvAEBouton.Name = "calculerSubvAEBouton";
            this.calculerSubvAEBouton.Size = new System.Drawing.Size(80, 23);
            this.calculerSubvAEBouton.TabIndex = 0;
            this.calculerSubvAEBouton.Text = "Calculer";
            this.calculerSubvAEBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.calculerSubvAEBouton.UseVisualStyleBackColor = true;
            this.calculerSubvAEBouton.Click += new System.EventHandler(this.calculerSubvAEBouton_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(5, 38);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Subvention :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(119, 48);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(16, 13);
            this.label23.TabIndex = 6;
            this.label23.Text = "%";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(174, 14);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(14, 13);
            this.label24.TabIndex = 5;
            this.label24.Text = "€";
            // 
            // numericTauxAE
            // 
            this.numericTauxAE.DecimalPlaces = 2;
            this.numericTauxAE.Location = new System.Drawing.Point(53, 45);
            this.numericTauxAE.Name = "numericTauxAE";
            this.numericTauxAE.Size = new System.Drawing.Size(64, 20);
            this.numericTauxAE.TabIndex = 4;
            this.numericTauxAE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTauxAE.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // numericDSHTAE
            // 
            this.numericDSHTAE.DecimalPlaces = 2;
            this.numericDSHTAE.Location = new System.Drawing.Point(60, 11);
            this.numericDSHTAE.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericDSHTAE.Name = "numericDSHTAE";
            this.numericDSHTAE.Size = new System.Drawing.Size(112, 20);
            this.numericDSHTAE.TabIndex = 3;
            this.numericDSHTAE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericDSHTAE.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(6, 48);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(43, 13);
            this.label25.TabIndex = 1;
            this.label25.Text = "Taux :";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(6, 14);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(49, 13);
            this.label26.TabIndex = 0;
            this.label26.Text = "DSHT :";
            // 
            // labelDateAR
            // 
            this.labelDateAR.AutoSize = true;
            this.labelDateAR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateAR.Location = new System.Drawing.Point(6, 10);
            this.labelDateAR.Name = "labelDateAR";
            this.labelDateAR.Size = new System.Drawing.Size(155, 13);
            this.labelDateAR.TabIndex = 138;
            this.labelDateAR.Text = "Date AR dossier complet :";
            this.labelDateAR.Visible = false;
            // 
            // tabControl4
            // 
            this.tabControl4.Controls.Add(this.tabPage5);
            this.tabControl4.Location = new System.Drawing.Point(9, 194);
            this.tabControl4.Name = "tabControl4";
            this.tabControl4.SelectedIndex = 0;
            this.tabControl4.Size = new System.Drawing.Size(270, 159);
            this.tabControl4.TabIndex = 155;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.panel6);
            this.tabPage5.Controls.Add(this.label29);
            this.tabPage5.Controls.Add(this.label30);
            this.tabPage5.Controls.Add(this.numericTauxSUR);
            this.tabPage5.Controls.Add(this.numericDSHTSUR);
            this.tabPage5.Controls.Add(this.label31);
            this.tabPage5.Controls.Add(this.label32);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(262, 133);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "SUR";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.label27);
            this.panel6.Controls.Add(this.numericSubventionSUR);
            this.panel6.Controls.Add(this.calculerSubvSURBouton);
            this.panel6.Controls.Add(this.label28);
            this.panel6.Location = new System.Drawing.Point(0, 71);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(262, 62);
            this.panel6.TabIndex = 7;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(205, 38);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(14, 13);
            this.label27.TabIndex = 6;
            this.label27.Text = "€";
            // 
            // numericSubventionSUR
            // 
            this.numericSubventionSUR.DecimalPlaces = 2;
            this.numericSubventionSUR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericSubventionSUR.ForeColor = System.Drawing.Color.Blue;
            this.numericSubventionSUR.Location = new System.Drawing.Point(90, 35);
            this.numericSubventionSUR.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericSubventionSUR.Name = "numericSubventionSUR";
            this.numericSubventionSUR.Size = new System.Drawing.Size(112, 20);
            this.numericSubventionSUR.TabIndex = 4;
            this.numericSubventionSUR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSubventionSUR.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // calculerSubvSURBouton
            // 
            this.calculerSubvSURBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculerSubvSURBouton.Image = global::ATE55.Properties.Resources.entreprise_16;
            this.calculerSubvSURBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.calculerSubvSURBouton.Location = new System.Drawing.Point(97, 5);
            this.calculerSubvSURBouton.Name = "calculerSubvSURBouton";
            this.calculerSubvSURBouton.Size = new System.Drawing.Size(80, 23);
            this.calculerSubvSURBouton.TabIndex = 0;
            this.calculerSubvSURBouton.Text = "Calculer";
            this.calculerSubvSURBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.calculerSubvSURBouton.UseVisualStyleBackColor = true;
            this.calculerSubvSURBouton.Click += new System.EventHandler(this.calculerSubvSURBouton_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(5, 38);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(79, 13);
            this.label28.TabIndex = 2;
            this.label28.Text = "Subvention :";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(119, 48);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(16, 13);
            this.label29.TabIndex = 6;
            this.label29.Text = "%";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(174, 14);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(14, 13);
            this.label30.TabIndex = 5;
            this.label30.Text = "€";
            // 
            // numericTauxSUR
            // 
            this.numericTauxSUR.DecimalPlaces = 2;
            this.numericTauxSUR.Location = new System.Drawing.Point(53, 45);
            this.numericTauxSUR.Name = "numericTauxSUR";
            this.numericTauxSUR.Size = new System.Drawing.Size(64, 20);
            this.numericTauxSUR.TabIndex = 4;
            this.numericTauxSUR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTauxSUR.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // numericDSHTSUR
            // 
            this.numericDSHTSUR.DecimalPlaces = 2;
            this.numericDSHTSUR.Location = new System.Drawing.Point(60, 11);
            this.numericDSHTSUR.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericDSHTSUR.Name = "numericDSHTSUR";
            this.numericDSHTSUR.Size = new System.Drawing.Size(112, 20);
            this.numericDSHTSUR.TabIndex = 3;
            this.numericDSHTSUR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericDSHTSUR.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(6, 48);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(43, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "Taux :";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(6, 14);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(49, 13);
            this.label32.TabIndex = 0;
            this.label32.Text = "DSHT :";
            // 
            // dateARDossierComplet
            // 
            this.dateARDossierComplet.Checked = false;
            this.dateARDossierComplet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateARDossierComplet.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateARDossierComplet.Location = new System.Drawing.Point(165, 7);
            this.dateARDossierComplet.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateARDossierComplet.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateARDossierComplet.Name = "dateARDossierComplet";
            this.dateARDossierComplet.ShowCheckBox = true;
            this.dateARDossierComplet.Size = new System.Drawing.Size(108, 20);
            this.dateARDossierComplet.TabIndex = 139;
            this.dateARDossierComplet.Visible = false;
            this.dateARDossierComplet.ValueChanged += new System.EventHandler(this.dateARDossierComplet_ValueChanged);
            // 
            // numericNbHeures
            // 
            this.numericNbHeures.Location = new System.Drawing.Point(372, 366);
            this.numericNbHeures.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericNbHeures.Name = "numericNbHeures";
            this.numericNbHeures.Size = new System.Drawing.Size(68, 20);
            this.numericNbHeures.TabIndex = 154;
            this.numericNbHeures.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericNbHeures.Visible = false;
            this.numericNbHeures.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(9, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(270, 159);
            this.tabControl1.TabIndex = 140;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.numericTauxDpt);
            this.tabPage2.Controls.Add(this.numericDSHTDpt);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(262, 133);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Département de la Meuse";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.numericSubventionDpt);
            this.panel2.Controls.Add(this.calculerSubvDptBouton);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(0, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(262, 62);
            this.panel2.TabIndex = 7;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(205, 38);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(14, 13);
            this.label20.TabIndex = 6;
            this.label20.Text = "€";
            // 
            // numericSubventionDpt
            // 
            this.numericSubventionDpt.DecimalPlaces = 2;
            this.numericSubventionDpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericSubventionDpt.ForeColor = System.Drawing.Color.Blue;
            this.numericSubventionDpt.Location = new System.Drawing.Point(90, 35);
            this.numericSubventionDpt.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericSubventionDpt.Name = "numericSubventionDpt";
            this.numericSubventionDpt.Size = new System.Drawing.Size(112, 20);
            this.numericSubventionDpt.TabIndex = 4;
            this.numericSubventionDpt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSubventionDpt.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // calculerSubvDptBouton
            // 
            this.calculerSubvDptBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculerSubvDptBouton.Image = global::ATE55.Properties.Resources.entreprise_16;
            this.calculerSubvDptBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.calculerSubvDptBouton.Location = new System.Drawing.Point(97, 5);
            this.calculerSubvDptBouton.Name = "calculerSubvDptBouton";
            this.calculerSubvDptBouton.Size = new System.Drawing.Size(80, 23);
            this.calculerSubvDptBouton.TabIndex = 0;
            this.calculerSubvDptBouton.Text = "Calculer";
            this.calculerSubvDptBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.calculerSubvDptBouton.UseVisualStyleBackColor = true;
            this.calculerSubvDptBouton.Click += new System.EventHandler(this.calculerSubvDptBouton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Subvention :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(119, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(174, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "€";
            // 
            // numericTauxDpt
            // 
            this.numericTauxDpt.DecimalPlaces = 2;
            this.numericTauxDpt.Location = new System.Drawing.Point(53, 45);
            this.numericTauxDpt.Name = "numericTauxDpt";
            this.numericTauxDpt.Size = new System.Drawing.Size(64, 20);
            this.numericTauxDpt.TabIndex = 4;
            this.numericTauxDpt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTauxDpt.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // numericDSHTDpt
            // 
            this.numericDSHTDpt.DecimalPlaces = 2;
            this.numericDSHTDpt.Location = new System.Drawing.Point(60, 11);
            this.numericDSHTDpt.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericDSHTDpt.Name = "numericDSHTDpt";
            this.numericDSHTDpt.Size = new System.Drawing.Size(112, 20);
            this.numericDSHTDpt.TabIndex = 3;
            this.numericDSHTDpt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericDSHTDpt.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Taux :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "DSHT :";
            // 
            // labelNbheures
            // 
            this.labelNbheures.AutoSize = true;
            this.labelNbheures.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNbheures.Location = new System.Drawing.Point(260, 369);
            this.labelNbheures.Name = "labelNbheures";
            this.labelNbheures.Size = new System.Drawing.Size(110, 13);
            this.labelNbheures.TabIndex = 153;
            this.labelNbheures.Text = "Nombre d\'heures :";
            this.labelNbheures.Visible = false;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Location = new System.Drawing.Point(289, 33);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(270, 159);
            this.tabControl2.TabIndex = 141;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.numericTauxGIP);
            this.tabPage3.Controls.Add(this.numericDSHTGIP);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.label15);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(262, 133);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "GIP";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label21);
            this.panel3.Controls.Add(this.numericSubventionGIP);
            this.panel3.Controls.Add(this.calculerSubvGIPBouton);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(0, 71);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(262, 62);
            this.panel3.TabIndex = 7;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(203, 38);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(14, 13);
            this.label21.TabIndex = 7;
            this.label21.Text = "€";
            // 
            // numericSubventionGIP
            // 
            this.numericSubventionGIP.DecimalPlaces = 2;
            this.numericSubventionGIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericSubventionGIP.ForeColor = System.Drawing.Color.Blue;
            this.numericSubventionGIP.Location = new System.Drawing.Point(90, 35);
            this.numericSubventionGIP.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericSubventionGIP.Name = "numericSubventionGIP";
            this.numericSubventionGIP.Size = new System.Drawing.Size(112, 20);
            this.numericSubventionGIP.TabIndex = 4;
            this.numericSubventionGIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericSubventionGIP.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // calculerSubvGIPBouton
            // 
            this.calculerSubvGIPBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calculerSubvGIPBouton.Image = global::ATE55.Properties.Resources.entreprise_16;
            this.calculerSubvGIPBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.calculerSubvGIPBouton.Location = new System.Drawing.Point(97, 5);
            this.calculerSubvGIPBouton.Name = "calculerSubvGIPBouton";
            this.calculerSubvGIPBouton.Size = new System.Drawing.Size(80, 23);
            this.calculerSubvGIPBouton.TabIndex = 0;
            this.calculerSubvGIPBouton.Text = "Calculer";
            this.calculerSubvGIPBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.calculerSubvGIPBouton.UseVisualStyleBackColor = true;
            this.calculerSubvGIPBouton.Click += new System.EventHandler(this.calculerSubvGIPBouton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(5, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "Subvention :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(119, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "%";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(174, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 5;
            this.label13.Text = "€";
            // 
            // numericTauxGIP
            // 
            this.numericTauxGIP.DecimalPlaces = 2;
            this.numericTauxGIP.Location = new System.Drawing.Point(53, 45);
            this.numericTauxGIP.Name = "numericTauxGIP";
            this.numericTauxGIP.Size = new System.Drawing.Size(64, 20);
            this.numericTauxGIP.TabIndex = 4;
            this.numericTauxGIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTauxGIP.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // numericDSHTGIP
            // 
            this.numericDSHTGIP.DecimalPlaces = 2;
            this.numericDSHTGIP.Location = new System.Drawing.Point(60, 11);
            this.numericDSHTGIP.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericDSHTGIP.Name = "numericDSHTGIP";
            this.numericDSHTGIP.Size = new System.Drawing.Size(112, 20);
            this.numericDSHTGIP.TabIndex = 3;
            this.numericDSHTGIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericDSHTGIP.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 48);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Taux :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(6, 14);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "DSHT :";
            // 
            // checkClauses
            // 
            this.checkClauses.AutoSize = true;
            this.checkClauses.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkClauses.Location = new System.Drawing.Point(125, 369);
            this.checkClauses.Name = "checkClauses";
            this.checkClauses.Size = new System.Drawing.Size(120, 17);
            this.checkClauses.TabIndex = 152;
            this.checkClauses.Text = "Clauses sociales";
            this.checkClauses.UseVisualStyleBackColor = true;
            this.checkClauses.CheckedChanged += new System.EventHandler(this.checkClauses_CheckedChanged);
            // 
            // labelDateProg
            // 
            this.labelDateProg.AutoSize = true;
            this.labelDateProg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDateProg.Location = new System.Drawing.Point(303, 10);
            this.labelDateProg.Name = "labelDateProg";
            this.labelDateProg.Size = new System.Drawing.Size(146, 13);
            this.labelDateProg.TabIndex = 144;
            this.labelDateProg.Text = "Date de programmation :";
            this.labelDateProg.UseMnemonic = false;
            this.labelDateProg.Visible = false;
            // 
            // dateProgrammation
            // 
            this.dateProgrammation.Checked = false;
            this.dateProgrammation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateProgrammation.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateProgrammation.Location = new System.Drawing.Point(451, 7);
            this.dateProgrammation.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateProgrammation.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateProgrammation.Name = "dateProgrammation";
            this.dateProgrammation.ShowCheckBox = true;
            this.dateProgrammation.Size = new System.Drawing.Size(108, 20);
            this.dateProgrammation.TabIndex = 145;
            this.dateProgrammation.Visible = false;
            this.dateProgrammation.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // infosModifSubvention
            // 
            this.infosModifSubvention.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.infosModifSubvention.Location = new System.Drawing.Point(36, 442);
            this.infosModifSubvention.Name = "infosModifSubvention";
            this.infosModifSubvention.Size = new System.Drawing.Size(526, 38);
            this.infosModifSubvention.TabIndex = 150;
            this.infosModifSubvention.Text = "...";
            this.infosModifSubvention.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(6, 369);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 13);
            this.label18.TabIndex = 146;
            this.label18.Text = "PP :";
            // 
            // textCommentairesSubvention
            // 
            this.textCommentairesSubvention.Location = new System.Drawing.Point(104, 399);
            this.textCommentairesSubvention.Multiline = true;
            this.textCommentairesSubvention.Name = "textCommentairesSubvention";
            this.textCommentairesSubvention.Size = new System.Drawing.Size(458, 40);
            this.textCommentairesSubvention.TabIndex = 149;
            this.textCommentairesSubvention.TextChanged += new System.EventHandler(this.Modification);
            // 
            // numericPP
            // 
            this.numericPP.Location = new System.Drawing.Point(42, 366);
            this.numericPP.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericPP.Name = "numericPP";
            this.numericPP.Size = new System.Drawing.Size(47, 20);
            this.numericPP.TabIndex = 147;
            this.numericPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericPP.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 402);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(93, 13);
            this.label19.TabIndex = 148;
            this.label19.Text = "Commentaires :";
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage7.Controls.Add(this.dataGridViewCollectivitesSubvention);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(566, 482);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Collectivités impactées";
            // 
            // dataGridViewCollectivitesSubvention
            // 
            this.dataGridViewCollectivitesSubvention.AllowUserToAddRows = false;
            this.dataGridViewCollectivitesSubvention.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCollectivitesSubvention.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCollectivitesSubvention.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCollectivitesSubvention.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodeCollectiviteSubvention,
            this.NomCollectiviteSubvention,
            this.PopDGFSubvention});
            this.dataGridViewCollectivitesSubvention.ContextMenuStrip = this.menuStripCollectiviteSubvention;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCollectivitesSubvention.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewCollectivitesSubvention.Location = new System.Drawing.Point(101, 6);
            this.dataGridViewCollectivitesSubvention.MultiSelect = false;
            this.dataGridViewCollectivitesSubvention.Name = "dataGridViewCollectivitesSubvention";
            this.dataGridViewCollectivitesSubvention.ReadOnly = true;
            this.dataGridViewCollectivitesSubvention.RowHeadersVisible = false;
            this.dataGridViewCollectivitesSubvention.RowTemplate.ContextMenuStrip = this.menuStripRowCollectiviteSubvention;
            this.dataGridViewCollectivitesSubvention.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCollectivitesSubvention.Size = new System.Drawing.Size(402, 470);
            this.dataGridViewCollectivitesSubvention.TabIndex = 1;
            // 
            // CodeCollectiviteSubvention
            // 
            this.CodeCollectiviteSubvention.HeaderText = "Code INSEE";
            this.CodeCollectiviteSubvention.Name = "CodeCollectiviteSubvention";
            this.CodeCollectiviteSubvention.ReadOnly = true;
            // 
            // NomCollectiviteSubvention
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NomCollectiviteSubvention.DefaultCellStyle = dataGridViewCellStyle3;
            this.NomCollectiviteSubvention.HeaderText = "Collectivité";
            this.NomCollectiviteSubvention.Name = "NomCollectiviteSubvention";
            this.NomCollectiviteSubvention.ReadOnly = true;
            this.NomCollectiviteSubvention.Width = 200;
            // 
            // PopDGFSubvention
            // 
            this.PopDGFSubvention.HeaderText = "Population DGF";
            this.PopDGFSubvention.Name = "PopDGFSubvention";
            this.PopDGFSubvention.ReadOnly = true;
            this.PopDGFSubvention.Width = 80;
            // 
            // menuStripCollectiviteSubvention
            // 
            this.menuStripCollectiviteSubvention.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUneCollectivitéToolStripMenuItem});
            this.menuStripCollectiviteSubvention.Name = "menuStripCollectiviteConvention";
            this.menuStripCollectiviteSubvention.Size = new System.Drawing.Size(197, 26);
            // 
            // ajouterUneCollectivitéToolStripMenuItem
            // 
            this.ajouterUneCollectivitéToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUneCollectivitéToolStripMenuItem.Name = "ajouterUneCollectivitéToolStripMenuItem";
            this.ajouterUneCollectivitéToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ajouterUneCollectivitéToolStripMenuItem.Text = "Ajouter une collectivité";
            this.ajouterUneCollectivitéToolStripMenuItem.Click += new System.EventHandler(this.ajouterUneCollectivitéToolStripMenuItem_Click);
            // 
            // menuStripRowCollectiviteSubvention
            // 
            this.menuStripRowCollectiviteSubvention.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUneCollectivitéToolStripMenuItem1,
            this.supprimerLaCollectivitéToolStripMenuItem});
            this.menuStripRowCollectiviteSubvention.Name = "menuStripRowCollectiviteConvention";
            this.menuStripRowCollectiviteSubvention.Size = new System.Drawing.Size(202, 48);
            // 
            // ajouterUneCollectivitéToolStripMenuItem1
            // 
            this.ajouterUneCollectivitéToolStripMenuItem1.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUneCollectivitéToolStripMenuItem1.Name = "ajouterUneCollectivitéToolStripMenuItem1";
            this.ajouterUneCollectivitéToolStripMenuItem1.Size = new System.Drawing.Size(201, 22);
            this.ajouterUneCollectivitéToolStripMenuItem1.Text = "Ajouter une collectivité";
            this.ajouterUneCollectivitéToolStripMenuItem1.Click += new System.EventHandler(this.ajouterUneCollectivitéToolStripMenuItem1_Click);
            // 
            // supprimerLaCollectivitéToolStripMenuItem
            // 
            this.supprimerLaCollectivitéToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Suppr;
            this.supprimerLaCollectivitéToolStripMenuItem.Name = "supprimerLaCollectivitéToolStripMenuItem";
            this.supprimerLaCollectivitéToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.supprimerLaCollectivitéToolStripMenuItem.Text = "Supprimer la collectivité";
            this.supprimerLaCollectivitéToolStripMenuItem.Click += new System.EventHandler(this.supprimerLaCollectivitéToolStripMenuItem_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.enregistrerSubventionBouton);
            this.panel4.Controls.Add(this.annulerSubventionBouton);
            this.panel4.Controls.Add(this.comboTypeDossier);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.comboCollectiviteSubvention);
            this.panel4.Controls.Add(this.dateReceptionDemande);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.textOperation);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.comboEtatSubvention);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(611, 159);
            this.panel4.TabIndex = 151;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(298, 132);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(105, 13);
            this.label22.TabIndex = 138;
            this.label22.Text = "Type du dossier :";
            // 
            // enregistrerSubventionBouton
            // 
            this.enregistrerSubventionBouton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enregistrerSubventionBouton.Enabled = false;
            this.enregistrerSubventionBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enregistrerSubventionBouton.Image = global::ATE55.Properties.Resources.saveHS;
            this.enregistrerSubventionBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.enregistrerSubventionBouton.Location = new System.Drawing.Point(452, 4);
            this.enregistrerSubventionBouton.Name = "enregistrerSubventionBouton";
            this.enregistrerSubventionBouton.Size = new System.Drawing.Size(116, 31);
            this.enregistrerSubventionBouton.TabIndex = 128;
            this.enregistrerSubventionBouton.Text = "Enregistrer";
            this.enregistrerSubventionBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.enregistrerSubventionBouton.UseVisualStyleBackColor = true;
            this.enregistrerSubventionBouton.Click += new System.EventHandler(this.enregistrerSubventionBouton_Click);
            // 
            // annulerSubventionBouton
            // 
            this.annulerSubventionBouton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.annulerSubventionBouton.Enabled = false;
            this.annulerSubventionBouton.Image = global::ATE55.Properties.Resources.Annuler;
            this.annulerSubventionBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.annulerSubventionBouton.Location = new System.Drawing.Point(479, 41);
            this.annulerSubventionBouton.Name = "annulerSubventionBouton";
            this.annulerSubventionBouton.Size = new System.Drawing.Size(89, 24);
            this.annulerSubventionBouton.TabIndex = 129;
            this.annulerSubventionBouton.Text = "Annuler";
            this.annulerSubventionBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.annulerSubventionBouton.UseVisualStyleBackColor = true;
            this.annulerSubventionBouton.Click += new System.EventHandler(this.annulerSubventionBouton_Click);
            // 
            // comboTypeDossier
            // 
            this.comboTypeDossier.AlignText = false;
            this.comboTypeDossier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTypeDossier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTypeDossier.FormattingEnabled = true;
            this.comboTypeDossier.Location = new System.Drawing.Point(404, 129);
            this.comboTypeDossier.Name = "comboTypeDossier";
            this.comboTypeDossier.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboTypeDossier.Size = new System.Drawing.Size(121, 21);
            this.comboTypeDossier.TabIndex = 139;
            this.comboTypeDossier.SelectedIndexChanged += new System.EventHandler(this.Modification);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 130;
            this.label2.Text = "Collectivité :";
            // 
            // comboCollectiviteSubvention
            // 
            this.comboCollectiviteSubvention.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboCollectiviteSubvention.DisplayMember = "CodeCollectivite";
            this.comboCollectiviteSubvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCollectiviteSubvention.FormattingEnabled = true;
            this.comboCollectiviteSubvention.Location = new System.Drawing.Point(85, 9);
            this.comboCollectiviteSubvention.Name = "comboCollectiviteSubvention";
            this.comboCollectiviteSubvention.Size = new System.Drawing.Size(358, 21);
            this.comboCollectiviteSubvention.TabIndex = 131;
            this.comboCollectiviteSubvention.ValueMember = "CodeCollectivite";
            this.comboCollectiviteSubvention.SelectedIndexChanged += new System.EventHandler(this.Modification);
            // 
            // dateReceptionDemande
            // 
            this.dateReceptionDemande.Checked = false;
            this.dateReceptionDemande.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateReceptionDemande.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateReceptionDemande.Location = new System.Drawing.Point(167, 49);
            this.dateReceptionDemande.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateReceptionDemande.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateReceptionDemande.Name = "dateReceptionDemande";
            this.dateReceptionDemande.ShowCheckBox = true;
            this.dateReceptionDemande.Size = new System.Drawing.Size(108, 20);
            this.dateReceptionDemande.TabIndex = 132;
            this.dateReceptionDemande.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 133;
            this.label1.Text = "Réception de la demande : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 134;
            this.label3.Text = "Opération :";
            // 
            // textOperation
            // 
            this.textOperation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textOperation.Location = new System.Drawing.Point(79, 79);
            this.textOperation.Multiline = true;
            this.textOperation.Name = "textOperation";
            this.textOperation.Size = new System.Drawing.Size(489, 40);
            this.textOperation.TabIndex = 135;
            this.textOperation.TextChanged += new System.EventHandler(this.Modification);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 136;
            this.label4.Text = "Etat du dossier :";
            // 
            // comboEtatSubvention
            // 
            this.comboEtatSubvention.AlignText = false;
            this.comboEtatSubvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEtatSubvention.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboEtatSubvention.FormattingEnabled = true;
            this.comboEtatSubvention.ImageList = this.imageListSubventions;
            this.comboEtatSubvention.Location = new System.Drawing.Point(107, 129);
            this.comboEtatSubvention.Name = "comboEtatSubvention";
            this.comboEtatSubvention.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboEtatSubvention.Size = new System.Drawing.Size(121, 21);
            this.comboEtatSubvention.TabIndex = 137;
            this.comboEtatSubvention.SelectedIndexChanged += new System.EventHandler(this.comboEtatSubvention_SelectedIndexChanged);
            // 
            // imageListSubventions
            // 
            this.imageListSubventions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSubventions.ImageStream")));
            this.imageListSubventions.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSubventions.Images.SetKeyName(0, "vide");
            this.imageListSubventions.Images.SetKeyName(1, "complet");
            this.imageListSubventions.Images.SetKeyName(2, "pretaetreprogramme");
            this.imageListSubventions.Images.SetKeyName(3, "programme");
            this.imageListSubventions.Images.SetKeyName(4, "solde");
            // 
            // frmSubventions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 720);
            this.Controls.Add(this.splitContainerSubvention);
            this.Controls.Add(this.toolStripConventions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1080, 758);
            this.Name = "frmSubventions";
            this.Text = "FormSubventions";
            this.Load += new System.EventHandler(this.frmSubventions_Load);
            this.toolStripConventions.ResumeLayout(false);
            this.toolStripConventions.PerformLayout();
            this.splitContainerSubvention.Panel1.ResumeLayout(false);
            this.splitContainerSubvention.Panel2.ResumeLayout(false);
            this.splitContainerSubvention.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubventions)).EndInit();
            this.menuStripDataGridSubventions.ResumeLayout(false);
            this.menuStripRowSubventions.ResumeLayout(false);
            this.tabSubvention.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabControl5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSubventionAE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTauxAE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDSHTAE)).EndInit();
            this.tabControl4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSubventionSUR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTauxSUR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDSHTSUR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericNbHeures)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSubventionDpt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTauxDpt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDSHTDpt)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSubventionGIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericTauxGIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDSHTGIP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPP)).EndInit();
            this.tabPage7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollectivitesSubvention)).EndInit();
            this.menuStripCollectiviteSubvention.ResumeLayout(false);
            this.menuStripRowCollectiviteSubvention.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripConventions;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonFichier;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualiser;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Session;
        private System.Windows.Forms.SplitContainer splitContainerSubvention;
        private System.Windows.Forms.DataGridView dataGridViewSubventions;
        private System.Windows.Forms.CheckBox checkAfficherDossiersProgrammes;
        private System.Windows.Forms.TabControl tabSubvention;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboCollectiviteSubvention;
        private System.Windows.Forms.Button enregistrerSubventionBouton;
        private System.Windows.Forms.Button annulerSubventionBouton;
        private System.Windows.Forms.TextBox textOperation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateReceptionDemande;
        private System.Windows.Forms.TextBox textCommentairesSubvention;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericPP;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DateTimePicker dateProgrammation;
        private System.Windows.Forms.Label labelDateProg;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown numericSubventionGIP;
        private System.Windows.Forms.Button calculerSubvGIPBouton;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericTauxGIP;
        private System.Windows.Forms.NumericUpDown numericDSHTGIP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown numericSubventionDpt;
        private System.Windows.Forms.Button calculerSubvDptBouton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericTauxDpt;
        private System.Windows.Forms.NumericUpDown numericDSHTDpt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateARDossierComplet;
        private System.Windows.Forms.Label labelDateAR;
        private CustomComboBox.CustomComboBox comboEtatSubvention;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label infosModifSubvention;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private CustomComboBox.CustomComboBox comboTypeDossier;
        private System.Windows.Forms.ImageList imageListSubventions;
        private System.Windows.Forms.ContextMenuStrip menuStripDataGridSubventions;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnDossierToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuStripRowSubventions;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnDossierToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem supprimerLeDossierToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numericNbHeures;
        private System.Windows.Forms.Label labelNbheures;
        private System.Windows.Forms.CheckBox checkClauses;
        private System.Windows.Forms.TextBox textRecherche;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numericSubventionAE;
        private System.Windows.Forms.Button calculerSubvAEBouton;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numericTauxAE;
        private System.Windows.Forms.NumericUpDown numericDSHTAE;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TabControl tabControl4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numericSubventionSUR;
        private System.Windows.Forms.Button calculerSubvSURBouton;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.NumericUpDown numericTauxSUR;
        private System.Windows.Forms.NumericUpDown numericDSHTSUR;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TabControl tabControl5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.DataGridView dataGridViewCollectivitesSubvention;
        private System.Windows.Forms.ContextMenuStrip menuStripCollectiviteSubvention;
        private System.Windows.Forms.ToolStripMenuItem ajouterUneCollectivitéToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuStripRowCollectiviteSubvention;
        private System.Windows.Forms.ToolStripMenuItem ajouterUneCollectivitéToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem supprimerLaCollectivitéToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodeCollectiviteSubvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomCollectiviteSubvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn PopDGFSubvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn idSubvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollectiviteSubvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateReceptionDemandeSubvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationSubvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeSubvention;
        private System.Windows.Forms.DataGridViewImageColumn EtatSubvention;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label33;
        private CustomComboBox.CustomComboBox comboRechercheType;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitExtraire;
        private System.Windows.Forms.ToolStripMenuItem subventionsDeLannéeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subventionsSoldéesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subventionsNonSoldéesToolStripMenuItem;
    }
}