using System.Drawing;
namespace ATE55 {
    partial class frmConventions {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConventions));
            this.dataGridViewConventions = new System.Windows.Forms.DataGridView();
            this.idConvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CollectiviteConvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnneesConvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeConvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripGridConventions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.creerUneConventionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripRowConventions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.creerUneNouvelleConventionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerLaConventionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchConventionBox = new System.Windows.Forms.TextBox();
            this.checkAfficherConventions = new System.Windows.Forms.CheckBox();
            this.splitContainerConventions = new System.Windows.Forms.SplitContainer();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.comboTypeConvention = new CustomComboBox.CustomComboBox();
            this.comboThemeConvention = new CustomComboBox.CustomComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabConvention = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.comboAnneeConvention = new CustomComboBox.CustomComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dateFinConvention = new System.Windows.Forms.DateTimePicker();
            this.label29 = new System.Windows.Forms.Label();
            this.dateDebutConvention = new System.Windows.Forms.DateTimePicker();
            this.label30 = new System.Windows.Forms.Label();
            this.textObservationsConvention = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.dateFinPrevision = new System.Windows.Forms.DateTimePicker();
            this.label44 = new System.Windows.Forms.Label();
            this.dateDebutPrevision = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.labelAgenceEau = new System.Windows.Forms.Label();
            this.comboChoixTypeConvention = new CustomComboBox.CustomComboBox();
            this.enregistrerConventionBouton = new System.Windows.Forms.Button();
            this.annulerConventionBouton = new System.Windows.Forms.Button();
            this.comboCollectiviteConvention = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.infosModifConvention = new System.Windows.Forms.Label();
            this.tabControl5 = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.enregistrerLigneConventionBouton = new System.Windows.Forms.Button();
            this.genererConvention = new System.Windows.Forms.Button();
            this.panelLigneConvention = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dateEnvoiSignature = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dateRetourSignature = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dateSignature = new System.Windows.Forms.DateTimePicker();
            this.dateEnvoiCollectivite = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.dateRetourRevision = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dateEnvoiRevision = new System.Windows.Forms.DateTimePicker();
            this.numericMontantConvention = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.tabSPAC = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.genererAnnexeBouton = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.numericMontantAnnexe = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.checkMandatSPAC = new System.Windows.Forms.CheckBox();
            this.dateRetourAnnexe = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dateEnvoiAnnexe = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textNonRecouvre = new System.Windows.Forms.TextBox();
            this.textObservationsLigne = new System.Windows.Forms.TextBox();
            this.tabInfosTitre = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelTitre = new System.Windows.Forms.Label();
            this.panelTitre = new System.Windows.Forms.Panel();
            this.labelEuros = new System.Windows.Forms.Label();
            this.dateEmissionTitre = new System.Windows.Forms.DateTimePicker();
            this.numericTitre = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.numericMontantTitre = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.comboTitre = new CustomComboBox.CustomComboBox();
            this.tabRPQS = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkMandatRPQS = new System.Windows.Forms.CheckBox();
            this.dateRetourRPQS = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.dateEnvoiRPQS = new System.Windows.Forms.DateTimePicker();
            this.panelArreteQ = new System.Windows.Forms.Panel();
            this.dateArreteDUP = new System.Windows.Forms.DateTimePicker();
            this.labelArreteDUP = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dataGridViewCollectivitesConvention = new System.Windows.Forms.DataGridView();
            this.CodeCollectiviteImpacConvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CollectiviteImpacConvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PopDGFConvention = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripCollectiviteConvention = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUneCollectivitéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripRowCollectiviteConvention = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUneCollectivitéToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerLaCollectivitéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStripSplitButtonFichier = new System.Windows.Forms.ToolStripSplitButton();
            this.quitterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonActualiser = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_Session = new System.Windows.Forms.ToolStripLabel();
            this.toolStripConventions = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonExcel = new System.Windows.Forms.ToolStripSplitButton();
            this.extraireLesConventionsActivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConventions)).BeginInit();
            this.menuStripGridConventions.SuspendLayout();
            this.menuStripRowConventions.SuspendLayout();
            this.splitContainerConventions.Panel1.SuspendLayout();
            this.splitContainerConventions.Panel2.SuspendLayout();
            this.splitContainerConventions.SuspendLayout();
            this.tabConvention.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl5.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.panelLigneConvention.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantConvention)).BeginInit();
            this.tabSPAC.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantAnnexe)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabInfosTitre.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelTitre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTitre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantTitre)).BeginInit();
            this.tabRPQS.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panelArreteQ.SuspendLayout();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollectivitesConvention)).BeginInit();
            this.menuStripCollectiviteConvention.SuspendLayout();
            this.menuStripRowCollectiviteConvention.SuspendLayout();
            this.toolStripConventions.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewConventions
            // 
            this.dataGridViewConventions.AllowUserToAddRows = false;
            this.dataGridViewConventions.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.NullValue = null;
            this.dataGridViewConventions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewConventions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewConventions.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewConventions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewConventions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idConvention,
            this.CollectiviteConvention,
            this.AnneesConvention,
            this.TypeConvention});
            this.dataGridViewConventions.ContextMenuStrip = this.menuStripGridConventions;
            this.dataGridViewConventions.Location = new System.Drawing.Point(3, 58);
            this.dataGridViewConventions.MultiSelect = false;
            this.dataGridViewConventions.Name = "dataGridViewConventions";
            this.dataGridViewConventions.ReadOnly = true;
            this.dataGridViewConventions.RowHeadersVisible = false;
            this.dataGridViewConventions.RowTemplate.ContextMenuStrip = this.menuStripRowConventions;
            this.dataGridViewConventions.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewConventions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewConventions.Size = new System.Drawing.Size(375, 627);
            this.dataGridViewConventions.TabIndex = 0;
            this.dataGridViewConventions.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewConventions_RowStateChanged);
            this.dataGridViewConventions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewConventions_MouseDown);
            // 
            // idConvention
            // 
            this.idConvention.HeaderText = "Id Convention";
            this.idConvention.Name = "idConvention";
            this.idConvention.ReadOnly = true;
            this.idConvention.Visible = false;
            this.idConvention.Width = 50;
            // 
            // CollectiviteConvention
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CollectiviteConvention.DefaultCellStyle = dataGridViewCellStyle2;
            this.CollectiviteConvention.HeaderText = "Collectivité";
            this.CollectiviteConvention.Name = "CollectiviteConvention";
            this.CollectiviteConvention.ReadOnly = true;
            this.CollectiviteConvention.Width = 200;
            // 
            // AnneesConvention
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Blue;
            this.AnneesConvention.DefaultCellStyle = dataGridViewCellStyle3;
            this.AnneesConvention.HeaderText = "Année(s)";
            this.AnneesConvention.Name = "AnneesConvention";
            this.AnneesConvention.ReadOnly = true;
            this.AnneesConvention.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AnneesConvention.Width = 45;
            // 
            // TypeConvention
            // 
            this.TypeConvention.HeaderText = "Type";
            this.TypeConvention.Name = "TypeConvention";
            this.TypeConvention.ReadOnly = true;
            this.TypeConvention.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TypeConvention.Width = 105;
            // 
            // menuStripGridConventions
            // 
            this.menuStripGridConventions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creerUneConventionToolStripMenuItem});
            this.menuStripGridConventions.Name = "menuStripGridConventions";
            this.menuStripGridConventions.Size = new System.Drawing.Size(237, 26);
            // 
            // creerUneConventionToolStripMenuItem
            // 
            this.creerUneConventionToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.creerUneConventionToolStripMenuItem.Name = "creerUneConventionToolStripMenuItem";
            this.creerUneConventionToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.creerUneConventionToolStripMenuItem.Text = "Créer une nouvelle convention";
            this.creerUneConventionToolStripMenuItem.Click += new System.EventHandler(this.creerUneConventionToolStripMenuItem_Click);
            // 
            // menuStripRowConventions
            // 
            this.menuStripRowConventions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.creerUneNouvelleConventionToolStripMenuItem,
            this.supprimerLaConventionToolStripMenuItem});
            this.menuStripRowConventions.Name = "menuStripRowConventions";
            this.menuStripRowConventions.Size = new System.Drawing.Size(237, 48);
            // 
            // creerUneNouvelleConventionToolStripMenuItem
            // 
            this.creerUneNouvelleConventionToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.creerUneNouvelleConventionToolStripMenuItem.Name = "creerUneNouvelleConventionToolStripMenuItem";
            this.creerUneNouvelleConventionToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.creerUneNouvelleConventionToolStripMenuItem.Text = "Créer une nouvelle convention";
            this.creerUneNouvelleConventionToolStripMenuItem.Click += new System.EventHandler(this.creerUneNouvelleConventionToolStripMenuItem_Click);
            // 
            // supprimerLaConventionToolStripMenuItem
            // 
            this.supprimerLaConventionToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Suppr;
            this.supprimerLaConventionToolStripMenuItem.Name = "supprimerLaConventionToolStripMenuItem";
            this.supprimerLaConventionToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.supprimerLaConventionToolStripMenuItem.Text = "Supprimer la convention";
            this.supprimerLaConventionToolStripMenuItem.Click += new System.EventHandler(this.supprimerLaConventionToolStripMenuItem_Click);
            // 
            // searchConventionBox
            // 
            this.searchConventionBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchConventionBox.Location = new System.Drawing.Point(81, 6);
            this.searchConventionBox.Name = "searchConventionBox";
            this.searchConventionBox.Size = new System.Drawing.Size(103, 20);
            this.searchConventionBox.TabIndex = 1;
            this.searchConventionBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchConventionBox_KeyUp);
            // 
            // checkAfficherConventions
            // 
            this.checkAfficherConventions.AutoSize = true;
            this.checkAfficherConventions.Location = new System.Drawing.Point(192, 8);
            this.checkAfficherConventions.Name = "checkAfficherConventions";
            this.checkAfficherConventions.Size = new System.Drawing.Size(191, 17);
            this.checkAfficherConventions.TabIndex = 9;
            this.checkAfficherConventions.Text = "Afficher les anciennes conventions";
            this.checkAfficherConventions.UseVisualStyleBackColor = true;
            this.checkAfficherConventions.CheckedChanged += new System.EventHandler(this.checkAfficherConventions_CheckedChanged);
            // 
            // splitContainerConventions
            // 
            this.splitContainerConventions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerConventions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerConventions.Location = new System.Drawing.Point(0, 28);
            this.splitContainerConventions.Name = "splitContainerConventions";
            // 
            // splitContainerConventions.Panel1
            // 
            this.splitContainerConventions.Panel1.Controls.Add(this.label25);
            this.splitContainerConventions.Panel1.Controls.Add(this.label24);
            this.splitContainerConventions.Panel1.Controls.Add(this.comboTypeConvention);
            this.splitContainerConventions.Panel1.Controls.Add(this.comboThemeConvention);
            this.splitContainerConventions.Panel1.Controls.Add(this.checkAfficherConventions);
            this.splitContainerConventions.Panel1.Controls.Add(this.label1);
            this.splitContainerConventions.Panel1.Controls.Add(this.dataGridViewConventions);
            this.splitContainerConventions.Panel1.Controls.Add(this.searchConventionBox);
            this.splitContainerConventions.Panel1MinSize = 383;
            // 
            // splitContainerConventions.Panel2
            // 
            this.splitContainerConventions.Panel2.Controls.Add(this.tabConvention);
            this.splitContainerConventions.Size = new System.Drawing.Size(1064, 690);
            this.splitContainerConventions.SplitterDistance = 383;
            this.splitContainerConventions.TabIndex = 11;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(188, 38);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 13);
            this.label25.TabIndex = 13;
            this.label25.Text = "Type :";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(8, 38);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(46, 13);
            this.label24.TabIndex = 12;
            this.label24.Text = "Thème :";
            // 
            // comboTypeConvention
            // 
            this.comboTypeConvention.AlignText = false;
            this.comboTypeConvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTypeConvention.Enabled = false;
            this.comboTypeConvention.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboTypeConvention.FormattingEnabled = true;
            this.comboTypeConvention.Location = new System.Drawing.Point(229, 35);
            this.comboTypeConvention.Name = "comboTypeConvention";
            this.comboTypeConvention.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboTypeConvention.Size = new System.Drawing.Size(150, 21);
            this.comboTypeConvention.TabIndex = 11;
            this.comboTypeConvention.SelectedIndexChanged += new System.EventHandler(this.comboTypeConvention_SelectedIndexChanged);
            // 
            // comboThemeConvention
            // 
            this.comboThemeConvention.AlignText = false;
            this.comboThemeConvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboThemeConvention.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboThemeConvention.FormattingEnabled = true;
            this.comboThemeConvention.Location = new System.Drawing.Point(55, 35);
            this.comboThemeConvention.Name = "comboThemeConvention";
            this.comboThemeConvention.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboThemeConvention.Size = new System.Drawing.Size(127, 21);
            this.comboThemeConvention.TabIndex = 10;
            this.comboThemeConvention.SelectedIndexChanged += new System.EventHandler(this.comboThemeConvention_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Recherche :";
            // 
            // tabConvention
            // 
            this.tabConvention.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabConvention.Controls.Add(this.tabPage7);
            this.tabConvention.Location = new System.Drawing.Point(0, 0);
            this.tabConvention.Name = "tabConvention";
            this.tabConvention.SelectedIndex = 0;
            this.tabConvention.Size = new System.Drawing.Size(676, 690);
            this.tabConvention.TabIndex = 14;
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage7.Controls.Add(this.comboAnneeConvention);
            this.tabPage7.Controls.Add(this.panel2);
            this.tabPage7.Controls.Add(this.infosModifConvention);
            this.tabPage7.Controls.Add(this.tabControl5);
            this.tabPage7.Controls.Add(this.label3);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(668, 664);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "Convention";
            // 
            // comboAnneeConvention
            // 
            this.comboAnneeConvention.AlignText = false;
            this.comboAnneeConvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAnneeConvention.FormattingEnabled = true;
            this.comboAnneeConvention.Location = new System.Drawing.Point(264, 132);
            this.comboAnneeConvention.Name = "comboAnneeConvention";
            this.comboAnneeConvention.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboAnneeConvention.Size = new System.Drawing.Size(57, 21);
            this.comboAnneeConvention.TabIndex = 131;
            this.comboAnneeConvention.SelectedIndexChanged += new System.EventHandler(this.comboAnneeConvention_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dateFinConvention);
            this.panel2.Controls.Add(this.label29);
            this.panel2.Controls.Add(this.dateDebutConvention);
            this.panel2.Controls.Add(this.label30);
            this.panel2.Controls.Add(this.textObservationsConvention);
            this.panel2.Controls.Add(this.label28);
            this.panel2.Controls.Add(this.dateFinPrevision);
            this.panel2.Controls.Add(this.label44);
            this.panel2.Controls.Add(this.dateDebutPrevision);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.labelAgenceEau);
            this.panel2.Controls.Add(this.comboChoixTypeConvention);
            this.panel2.Controls.Add(this.enregistrerConventionBouton);
            this.panel2.Controls.Add(this.annulerConventionBouton);
            this.panel2.Controls.Add(this.comboCollectiviteConvention);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(668, 130);
            this.panel2.TabIndex = 129;
            // 
            // dateFinConvention
            // 
            this.dateFinConvention.Checked = false;
            this.dateFinConvention.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateFinConvention.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFinConvention.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFinConvention.Location = new System.Drawing.Point(340, 58);
            this.dateFinConvention.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateFinConvention.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateFinConvention.Name = "dateFinConvention";
            this.dateFinConvention.ShowCheckBox = true;
            this.dateFinConvention.Size = new System.Drawing.Size(105, 20);
            this.dateFinConvention.TabIndex = 141;
            this.dateFinConvention.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(260, 61);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(78, 13);
            this.label29.TabIndex = 140;
            this.label29.Text = "Date de fin :";
            // 
            // dateDebutConvention
            // 
            this.dateDebutConvention.Checked = false;
            this.dateDebutConvention.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateDebutConvention.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDebutConvention.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateDebutConvention.Location = new System.Drawing.Point(112, 58);
            this.dateDebutConvention.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateDebutConvention.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateDebutConvention.Name = "dateDebutConvention";
            this.dateDebutConvention.ShowCheckBox = true;
            this.dateDebutConvention.Size = new System.Drawing.Size(105, 20);
            this.dateDebutConvention.TabIndex = 139;
            this.dateDebutConvention.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(10, 61);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(96, 13);
            this.label30.TabIndex = 138;
            this.label30.Text = "Date de début :";
            // 
            // textObservationsConvention
            // 
            this.textObservationsConvention.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textObservationsConvention.Location = new System.Drawing.Point(101, 106);
            this.textObservationsConvention.Name = "textObservationsConvention";
            this.textObservationsConvention.Size = new System.Drawing.Size(563, 20);
            this.textObservationsConvention.TabIndex = 137;
            this.textObservationsConvention.TextChanged += new System.EventHandler(this.Modification);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(10, 109);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(89, 13);
            this.label28.TabIndex = 136;
            this.label28.Text = "Observations :";
            // 
            // dateFinPrevision
            // 
            this.dateFinPrevision.Checked = false;
            this.dateFinPrevision.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateFinPrevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateFinPrevision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateFinPrevision.Location = new System.Drawing.Point(468, 82);
            this.dateFinPrevision.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateFinPrevision.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateFinPrevision.Name = "dateFinPrevision";
            this.dateFinPrevision.ShowCheckBox = true;
            this.dateFinPrevision.Size = new System.Drawing.Size(105, 20);
            this.dateFinPrevision.TabIndex = 134;
            this.dateFinPrevision.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(308, 85);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(160, 13);
            this.label44.TabIndex = 133;
            this.label44.Text = "Date de fin prévisionnelle :";
            // 
            // dateDebutPrevision
            // 
            this.dateDebutPrevision.Checked = false;
            this.dateDebutPrevision.Cursor = System.Windows.Forms.Cursors.Default;
            this.dateDebutPrevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateDebutPrevision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateDebutPrevision.Location = new System.Drawing.Point(181, 82);
            this.dateDebutPrevision.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateDebutPrevision.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateDebutPrevision.Name = "dateDebutPrevision";
            this.dateDebutPrevision.ShowCheckBox = true;
            this.dateDebutPrevision.Size = new System.Drawing.Size(105, 20);
            this.dateDebutPrevision.TabIndex = 132;
            this.dateDebutPrevision.ValueChanged += new System.EventHandler(this.Modification);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(10, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(168, 13);
            this.label21.TabIndex = 131;
            this.label21.Text = "Date de début prévisionnel :";
            // 
            // labelAgenceEau
            // 
            this.labelAgenceEau.AutoSize = true;
            this.labelAgenceEau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAgenceEau.Location = new System.Drawing.Point(258, 36);
            this.labelAgenceEau.Name = "labelAgenceEau";
            this.labelAgenceEau.Size = new System.Drawing.Size(31, 13);
            this.labelAgenceEau.TabIndex = 130;
            this.labelAgenceEau.Text = "AE :";
            // 
            // comboChoixTypeConvention
            // 
            this.comboChoixTypeConvention.AlignText = false;
            this.comboChoixTypeConvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChoixTypeConvention.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboChoixTypeConvention.FormattingEnabled = true;
            this.comboChoixTypeConvention.Location = new System.Drawing.Point(58, 33);
            this.comboChoixTypeConvention.Name = "comboChoixTypeConvention";
            this.comboChoixTypeConvention.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboChoixTypeConvention.Size = new System.Drawing.Size(150, 21);
            this.comboChoixTypeConvention.TabIndex = 128;
            this.comboChoixTypeConvention.SelectedIndexChanged += new System.EventHandler(this.comboChoixTypeConvention_SelectedIndexChanged);
            // 
            // enregistrerConventionBouton
            // 
            this.enregistrerConventionBouton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enregistrerConventionBouton.Enabled = false;
            this.enregistrerConventionBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enregistrerConventionBouton.Image = global::ATE55.Properties.Resources.saveHS;
            this.enregistrerConventionBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.enregistrerConventionBouton.Location = new System.Drawing.Point(541, 8);
            this.enregistrerConventionBouton.Name = "enregistrerConventionBouton";
            this.enregistrerConventionBouton.Size = new System.Drawing.Size(116, 31);
            this.enregistrerConventionBouton.TabIndex = 124;
            this.enregistrerConventionBouton.Text = "Enregistrer";
            this.enregistrerConventionBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.enregistrerConventionBouton.UseVisualStyleBackColor = true;
            this.enregistrerConventionBouton.Click += new System.EventHandler(this.enregistrerConventionBouton_Click);
            // 
            // annulerConventionBouton
            // 
            this.annulerConventionBouton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.annulerConventionBouton.Enabled = false;
            this.annulerConventionBouton.Image = global::ATE55.Properties.Resources.Annuler;
            this.annulerConventionBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.annulerConventionBouton.Location = new System.Drawing.Point(568, 42);
            this.annulerConventionBouton.Name = "annulerConventionBouton";
            this.annulerConventionBouton.Size = new System.Drawing.Size(89, 24);
            this.annulerConventionBouton.TabIndex = 125;
            this.annulerConventionBouton.Text = "Annuler";
            this.annulerConventionBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.annulerConventionBouton.UseVisualStyleBackColor = true;
            this.annulerConventionBouton.Click += new System.EventHandler(this.annulerConventionBouton_Click);
            // 
            // comboCollectiviteConvention
            // 
            this.comboCollectiviteConvention.DisplayMember = "CodeCollectivite";
            this.comboCollectiviteConvention.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCollectiviteConvention.FormattingEnabled = true;
            this.comboCollectiviteConvention.Location = new System.Drawing.Point(86, 7);
            this.comboCollectiviteConvention.Name = "comboCollectiviteConvention";
            this.comboCollectiviteConvention.Size = new System.Drawing.Size(451, 21);
            this.comboCollectiviteConvention.TabIndex = 127;
            this.comboCollectiviteConvention.ValueMember = "CodeCollectivite";
            this.comboCollectiviteConvention.SelectedIndexChanged += new System.EventHandler(this.comboCollectiviteConvention_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 126;
            this.label2.Text = "Collectivité :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 129;
            this.label4.Text = "Type :";
            // 
            // infosModifConvention
            // 
            this.infosModifConvention.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.infosModifConvention.Location = new System.Drawing.Point(317, 129);
            this.infosModifConvention.Name = "infosModifConvention";
            this.infosModifConvention.Size = new System.Drawing.Size(346, 38);
            this.infosModifConvention.TabIndex = 122;
            this.infosModifConvention.Text = "...";
            this.infosModifConvention.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl5
            // 
            this.tabControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl5.Controls.Add(this.tabPage5);
            this.tabControl5.Controls.Add(this.tabPage6);
            this.tabControl5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl5.Location = new System.Drawing.Point(3, 147);
            this.tabControl5.Name = "tabControl5";
            this.tabControl5.SelectedIndex = 0;
            this.tabControl5.Size = new System.Drawing.Size(661, 514);
            this.tabControl5.TabIndex = 127;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage5.Controls.Add(this.enregistrerLigneConventionBouton);
            this.tabPage5.Controls.Add(this.genererConvention);
            this.tabPage5.Controls.Add(this.panelLigneConvention);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(653, 488);
            this.tabPage5.TabIndex = 0;
            this.tabPage5.Text = "Informations";
            // 
            // enregistrerLigneConventionBouton
            // 
            this.enregistrerLigneConventionBouton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enregistrerLigneConventionBouton.Enabled = false;
            this.enregistrerLigneConventionBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.enregistrerLigneConventionBouton.Image = global::ATE55.Properties.Resources.saveHS;
            this.enregistrerLigneConventionBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.enregistrerLigneConventionBouton.Location = new System.Drawing.Point(523, 1);
            this.enregistrerLigneConventionBouton.Name = "enregistrerLigneConventionBouton";
            this.enregistrerLigneConventionBouton.Size = new System.Drawing.Size(116, 31);
            this.enregistrerLigneConventionBouton.TabIndex = 137;
            this.enregistrerLigneConventionBouton.Text = "Enregistrer";
            this.enregistrerLigneConventionBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.enregistrerLigneConventionBouton.UseVisualStyleBackColor = true;
            this.enregistrerLigneConventionBouton.Click += new System.EventHandler(this.enregistrerLigneConventionBouton_Click);
            // 
            // genererConvention
            // 
            this.genererConvention.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.genererConvention.Enabled = false;
            this.genererConvention.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genererConvention.Image = global::ATE55.Properties.Resources.word;
            this.genererConvention.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.genererConvention.Location = new System.Drawing.Point(354, 1);
            this.genererConvention.Name = "genererConvention";
            this.genererConvention.Size = new System.Drawing.Size(163, 31);
            this.genererConvention.TabIndex = 132;
            this.genererConvention.Text = "Générer la convention";
            this.genererConvention.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.genererConvention.UseVisualStyleBackColor = true;
            this.genererConvention.Click += new System.EventHandler(this.genererConvention_Click);
            // 
            // panelLigneConvention
            // 
            this.panelLigneConvention.Controls.Add(this.tabControl1);
            this.panelLigneConvention.Controls.Add(this.tabSPAC);
            this.panelLigneConvention.Controls.Add(this.panel3);
            this.panelLigneConvention.Controls.Add(this.tabRPQS);
            this.panelLigneConvention.Controls.Add(this.panelArreteQ);
            this.panelLigneConvention.Location = new System.Drawing.Point(0, 9);
            this.panelLigneConvention.Name = "panelLigneConvention";
            this.panelLigneConvention.Size = new System.Drawing.Size(658, 486);
            this.panelLigneConvention.TabIndex = 136;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(645, 158);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.label26);
            this.tabPage1.Controls.Add(this.dateRetourRevision);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.dateEnvoiRevision);
            this.tabPage1.Controls.Add(this.numericMontantConvention);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(637, 132);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Suivi Convention";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.dateEnvoiSignature);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dateRetourSignature);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dateSignature);
            this.panel1.Controls.Add(this.dateEnvoiCollectivite);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 64);
            this.panel1.TabIndex = 150;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Envoi le ";
            // 
            // dateEnvoiSignature
            // 
            this.dateEnvoiSignature.Checked = false;
            this.dateEnvoiSignature.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEnvoiSignature.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnvoiSignature.Location = new System.Drawing.Point(67, 7);
            this.dateEnvoiSignature.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateEnvoiSignature.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateEnvoiSignature.Name = "dateEnvoiSignature";
            this.dateEnvoiSignature.ShowCheckBox = true;
            this.dateEnvoiSignature.Size = new System.Drawing.Size(105, 20);
            this.dateEnvoiSignature.TabIndex = 0;
            this.dateEnvoiSignature.ValueChanged += new System.EventHandler(this.dateEnvoiSignature_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(177, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "et retour le ";
            // 
            // dateRetourSignature
            // 
            this.dateRetourSignature.Checked = false;
            this.dateRetourSignature.Enabled = false;
            this.dateRetourSignature.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRetourSignature.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateRetourSignature.Location = new System.Drawing.Point(251, 7);
            this.dateRetourSignature.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateRetourSignature.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateRetourSignature.Name = "dateRetourSignature";
            this.dateRetourSignature.ShowCheckBox = true;
            this.dateRetourSignature.Size = new System.Drawing.Size(105, 20);
            this.dateRetourSignature.TabIndex = 4;
            this.dateRetourSignature.Value = new System.DateTime(2017, 4, 26, 0, 0, 0, 0);
            this.dateRetourSignature.ValueChanged += new System.EventHandler(this.dateRetourSignature_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Convention signée le";
            // 
            // dateSignature
            // 
            this.dateSignature.Checked = false;
            this.dateSignature.Enabled = false;
            this.dateSignature.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateSignature.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateSignature.Location = new System.Drawing.Point(139, 37);
            this.dateSignature.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateSignature.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateSignature.Name = "dateSignature";
            this.dateSignature.ShowCheckBox = true;
            this.dateSignature.Size = new System.Drawing.Size(105, 20);
            this.dateSignature.TabIndex = 6;
            this.dateSignature.ValueChanged += new System.EventHandler(this.dateSignature_ValueChanged);
            // 
            // dateEnvoiCollectivite
            // 
            this.dateEnvoiCollectivite.Checked = false;
            this.dateEnvoiCollectivite.Enabled = false;
            this.dateEnvoiCollectivite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEnvoiCollectivite.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnvoiCollectivite.Location = new System.Drawing.Point(333, 37);
            this.dateEnvoiCollectivite.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateEnvoiCollectivite.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateEnvoiCollectivite.Name = "dateEnvoiCollectivite";
            this.dateEnvoiCollectivite.ShowCheckBox = true;
            this.dateEnvoiCollectivite.Size = new System.Drawing.Size(105, 20);
            this.dateEnvoiCollectivite.TabIndex = 8;
            this.dateEnvoiCollectivite.ValueChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(247, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "et envoyée le ";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(297, 103);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(14, 13);
            this.label26.TabIndex = 149;
            this.label26.Text = "€";
            // 
            // dateRetourRevision
            // 
            this.dateRetourRevision.Checked = false;
            this.dateRetourRevision.Enabled = false;
            this.dateRetourRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRetourRevision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateRetourRevision.Location = new System.Drawing.Point(325, 70);
            this.dateRetourRevision.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateRetourRevision.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateRetourRevision.Name = "dateRetourRevision";
            this.dateRetourRevision.ShowCheckBox = true;
            this.dateRetourRevision.Size = new System.Drawing.Size(105, 20);
            this.dateRetourRevision.TabIndex = 21;
            this.dateRetourRevision.ValueChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(255, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "et retour le ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(8, 73);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(137, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "Envoi de la révision le ";
            // 
            // dateEnvoiRevision
            // 
            this.dateEnvoiRevision.Checked = false;
            this.dateEnvoiRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEnvoiRevision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnvoiRevision.Location = new System.Drawing.Point(146, 70);
            this.dateEnvoiRevision.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateEnvoiRevision.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateEnvoiRevision.Name = "dateEnvoiRevision";
            this.dateEnvoiRevision.ShowCheckBox = true;
            this.dateEnvoiRevision.Size = new System.Drawing.Size(105, 20);
            this.dateEnvoiRevision.TabIndex = 18;
            this.dateEnvoiRevision.ValueChanged += new System.EventHandler(this.dateEnvoiRevision_ValueChanged);
            // 
            // numericMontantConvention
            // 
            this.numericMontantConvention.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericMontantConvention.DecimalPlaces = 2;
            this.numericMontantConvention.Location = new System.Drawing.Point(171, 100);
            this.numericMontantConvention.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericMontantConvention.Name = "numericMontantConvention";
            this.numericMontantConvention.Size = new System.Drawing.Size(120, 20);
            this.numericMontantConvention.TabIndex = 143;
            this.numericMontantConvention.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericMontantConvention.ThousandsSeparator = true;
            this.numericMontantConvention.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericMontantConvention_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(8, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(160, 13);
            this.label11.TabIndex = 142;
            this.label11.Text = "Montant de la convention :";
            // 
            // tabSPAC
            // 
            this.tabSPAC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSPAC.Controls.Add(this.tabPage4);
            this.tabSPAC.Location = new System.Drawing.Point(3, 167);
            this.tabSPAC.Name = "tabSPAC";
            this.tabSPAC.SelectedIndex = 0;
            this.tabSPAC.Size = new System.Drawing.Size(645, 92);
            this.tabSPAC.TabIndex = 26;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.genererAnnexeBouton);
            this.tabPage4.Controls.Add(this.label27);
            this.tabPage4.Controls.Add(this.numericMontantAnnexe);
            this.tabPage4.Controls.Add(this.label20);
            this.tabPage4.Controls.Add(this.checkMandatSPAC);
            this.tabPage4.Controls.Add(this.dateRetourAnnexe);
            this.tabPage4.Controls.Add(this.label18);
            this.tabPage4.Controls.Add(this.label19);
            this.tabPage4.Controls.Add(this.dateEnvoiAnnexe);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(637, 66);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Annexe 2";
            // 
            // genererAnnexeBouton
            // 
            this.genererAnnexeBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genererAnnexeBouton.Image = global::ATE55.Properties.Resources.word;
            this.genererAnnexeBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.genererAnnexeBouton.Location = new System.Drawing.Point(528, 38);
            this.genererAnnexeBouton.Name = "genererAnnexeBouton";
            this.genererAnnexeBouton.Size = new System.Drawing.Size(97, 25);
            this.genererAnnexeBouton.TabIndex = 130;
            this.genererAnnexeBouton.Text = "Annexe 2";
            this.genererAnnexeBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.genererAnnexeBouton.UseVisualStyleBackColor = true;
            this.genererAnnexeBouton.Click += new System.EventHandler(this.genererAnnexeBouton_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(565, 15);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(14, 13);
            this.label27.TabIndex = 129;
            this.label27.Text = "€";
            // 
            // numericMontantAnnexe
            // 
            this.numericMontantAnnexe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericMontantAnnexe.DecimalPlaces = 2;
            this.numericMontantAnnexe.Location = new System.Drawing.Point(440, 13);
            this.numericMontantAnnexe.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericMontantAnnexe.Name = "numericMontantAnnexe";
            this.numericMontantAnnexe.Size = new System.Drawing.Size(120, 20);
            this.numericMontantAnnexe.TabIndex = 11;
            this.numericMontantAnnexe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericMontantAnnexe.ThousandsSeparator = true;
            this.numericMontantAnnexe.ValueChanged += new System.EventHandler(this.ModificationLigneConvention);
            this.numericMontantAnnexe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericMontantAnnexe_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(380, 16);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 13);
            this.label20.TabIndex = 10;
            this.label20.Text = "Montant : ";
            // 
            // checkMandatSPAC
            // 
            this.checkMandatSPAC.AutoSize = true;
            this.checkMandatSPAC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMandatSPAC.Location = new System.Drawing.Point(10, 39);
            this.checkMandatSPAC.Name = "checkMandatSPAC";
            this.checkMandatSPAC.Size = new System.Drawing.Size(104, 17);
            this.checkMandatSPAC.TabIndex = 9;
            this.checkMandatSPAC.Text = "Mandat SPAC";
            this.checkMandatSPAC.UseVisualStyleBackColor = true;
            this.checkMandatSPAC.CheckedChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // dateRetourAnnexe
            // 
            this.dateRetourAnnexe.Checked = false;
            this.dateRetourAnnexe.Enabled = false;
            this.dateRetourAnnexe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRetourAnnexe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateRetourAnnexe.Location = new System.Drawing.Point(244, 13);
            this.dateRetourAnnexe.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateRetourAnnexe.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateRetourAnnexe.Name = "dateRetourAnnexe";
            this.dateRetourAnnexe.ShowCheckBox = true;
            this.dateRetourAnnexe.Size = new System.Drawing.Size(105, 20);
            this.dateRetourAnnexe.TabIndex = 8;
            this.dateRetourAnnexe.ValueChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(173, 16);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(73, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "et retour le ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(8, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(57, 13);
            this.label19.TabIndex = 6;
            this.label19.Text = "Envoi le ";
            // 
            // dateEnvoiAnnexe
            // 
            this.dateEnvoiAnnexe.Checked = false;
            this.dateEnvoiAnnexe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEnvoiAnnexe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnvoiAnnexe.Location = new System.Drawing.Point(65, 13);
            this.dateEnvoiAnnexe.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateEnvoiAnnexe.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateEnvoiAnnexe.Name = "dateEnvoiAnnexe";
            this.dateEnvoiAnnexe.ShowCheckBox = true;
            this.dateEnvoiAnnexe.Size = new System.Drawing.Size(105, 20);
            this.dateEnvoiAnnexe.TabIndex = 5;
            this.dateEnvoiAnnexe.ValueChanged += new System.EventHandler(this.dateEnvoiAnnexe_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label23);
            this.panel3.Controls.Add(this.textNonRecouvre);
            this.panel3.Controls.Add(this.textObservationsLigne);
            this.panel3.Controls.Add(this.tabInfosTitre);
            this.panel3.Location = new System.Drawing.Point(3, 265);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(645, 139);
            this.panel3.TabIndex = 129;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Observations :";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(7, 4);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(92, 13);
            this.label23.TabIndex = 127;
            this.label23.Text = "Non recouvré :";
            // 
            // textNonRecouvre
            // 
            this.textNonRecouvre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textNonRecouvre.Location = new System.Drawing.Point(102, 1);
            this.textNonRecouvre.Name = "textNonRecouvre";
            this.textNonRecouvre.Size = new System.Drawing.Size(540, 20);
            this.textNonRecouvre.TabIndex = 128;
            this.textNonRecouvre.TextChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // textObservationsLigne
            // 
            this.textObservationsLigne.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textObservationsLigne.Location = new System.Drawing.Point(102, 27);
            this.textObservationsLigne.Multiline = true;
            this.textObservationsLigne.Name = "textObservationsLigne";
            this.textObservationsLigne.Size = new System.Drawing.Size(540, 40);
            this.textObservationsLigne.TabIndex = 23;
            this.textObservationsLigne.TextChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // tabInfosTitre
            // 
            this.tabInfosTitre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabInfosTitre.Controls.Add(this.tabPage2);
            this.tabInfosTitre.Location = new System.Drawing.Point(0, 53);
            this.tabInfosTitre.Name = "tabInfosTitre";
            this.tabInfosTitre.SelectedIndex = 0;
            this.tabInfosTitre.Size = new System.Drawing.Size(645, 85);
            this.tabInfosTitre.TabIndex = 24;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.labelTitre);
            this.tabPage2.Controls.Add(this.panelTitre);
            this.tabPage2.Controls.Add(this.comboTitre);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(637, 59);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Informations Titre";
            // 
            // labelTitre
            // 
            this.labelTitre.AutoSize = true;
            this.labelTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitre.ForeColor = System.Drawing.Color.Red;
            this.labelTitre.Location = new System.Drawing.Point(145, 9);
            this.labelTitre.Name = "labelTitre";
            this.labelTitre.Size = new System.Drawing.Size(199, 13);
            this.labelTitre.TabIndex = 141;
            this.labelTitre.Text = "/!\\ Un nouveau titre va être créé.";
            this.labelTitre.Visible = false;
            // 
            // panelTitre
            // 
            this.panelTitre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTitre.Controls.Add(this.labelEuros);
            this.panelTitre.Controls.Add(this.dateEmissionTitre);
            this.panelTitre.Controls.Add(this.numericTitre);
            this.panelTitre.Controls.Add(this.label15);
            this.panelTitre.Controls.Add(this.label14);
            this.panelTitre.Controls.Add(this.numericMontantTitre);
            this.panelTitre.Controls.Add(this.label10);
            this.panelTitre.Location = new System.Drawing.Point(0, 31);
            this.panelTitre.Name = "panelTitre";
            this.panelTitre.Size = new System.Drawing.Size(637, 28);
            this.panelTitre.TabIndex = 140;
            // 
            // labelEuros
            // 
            this.labelEuros.AutoSize = true;
            this.labelEuros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEuros.Location = new System.Drawing.Point(585, 6);
            this.labelEuros.Name = "labelEuros";
            this.labelEuros.Size = new System.Drawing.Size(14, 13);
            this.labelEuros.TabIndex = 148;
            this.labelEuros.Text = "€";
            // 
            // dateEmissionTitre
            // 
            this.dateEmissionTitre.Checked = false;
            this.dateEmissionTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEmissionTitre.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEmissionTitre.Location = new System.Drawing.Point(224, 3);
            this.dateEmissionTitre.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateEmissionTitre.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.dateEmissionTitre.Name = "dateEmissionTitre";
            this.dateEmissionTitre.ShowCheckBox = true;
            this.dateEmissionTitre.Size = new System.Drawing.Size(105, 20);
            this.dateEmissionTitre.TabIndex = 145;
            this.dateEmissionTitre.ValueChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // numericTitre
            // 
            this.numericTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericTitre.ForeColor = System.Drawing.Color.Blue;
            this.numericTitre.Location = new System.Drawing.Point(80, 3);
            this.numericTitre.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericTitre.Name = "numericTitre";
            this.numericTitre.Size = new System.Drawing.Size(87, 20);
            this.numericTitre.TabIndex = 147;
            this.numericTitre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericTitre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericTitre_KeyPress);
            this.numericTitre.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericTitre_KeyUp);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(173, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 144;
            this.label15.Text = "émis le ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(19, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 13);
            this.label14.TabIndex = 146;
            this.label14.Text = "N° titre : ";
            // 
            // numericMontantTitre
            // 
            this.numericMontantTitre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericMontantTitre.DecimalPlaces = 2;
            this.numericMontantTitre.Location = new System.Drawing.Point(459, 3);
            this.numericMontantTitre.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericMontantTitre.Name = "numericMontantTitre";
            this.numericMontantTitre.Size = new System.Drawing.Size(120, 20);
            this.numericMontantTitre.TabIndex = 141;
            this.numericMontantTitre.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericMontantTitre.ThousandsSeparator = true;
            this.numericMontantTitre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericMontantTitre_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(353, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(105, 13);
            this.label10.TabIndex = 140;
            this.label10.Text = "Montant du titre :";
            // 
            // comboTitre
            // 
            this.comboTitre.AlignText = false;
            this.comboTitre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTitre.FormattingEnabled = true;
            this.comboTitre.Location = new System.Drawing.Point(17, 6);
            this.comboTitre.Name = "comboTitre";
            this.comboTitre.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboTitre.Size = new System.Drawing.Size(121, 21);
            this.comboTitre.TabIndex = 129;
            this.comboTitre.SelectedIndexChanged += new System.EventHandler(this.comboTitre_SelectedIndexChanged);
            // 
            // tabRPQS
            // 
            this.tabRPQS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabRPQS.Controls.Add(this.tabPage3);
            this.tabRPQS.Location = new System.Drawing.Point(3, 410);
            this.tabRPQS.Name = "tabRPQS";
            this.tabRPQS.SelectedIndex = 0;
            this.tabRPQS.Size = new System.Drawing.Size(645, 65);
            this.tabRPQS.TabIndex = 25;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.checkMandatRPQS);
            this.tabPage3.Controls.Add(this.dateRetourRPQS);
            this.tabPage3.Controls.Add(this.label16);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.dateEnvoiRPQS);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(637, 39);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "RPQS";
            // 
            // checkMandatRPQS
            // 
            this.checkMandatRPQS.AutoSize = true;
            this.checkMandatRPQS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMandatRPQS.Location = new System.Drawing.Point(383, 13);
            this.checkMandatRPQS.Name = "checkMandatRPQS";
            this.checkMandatRPQS.Size = new System.Drawing.Size(68, 17);
            this.checkMandatRPQS.TabIndex = 9;
            this.checkMandatRPQS.Text = "Mandat";
            this.checkMandatRPQS.UseVisualStyleBackColor = true;
            this.checkMandatRPQS.CheckedChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // dateRetourRPQS
            // 
            this.dateRetourRPQS.Checked = false;
            this.dateRetourRPQS.Enabled = false;
            this.dateRetourRPQS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateRetourRPQS.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateRetourRPQS.Location = new System.Drawing.Point(244, 13);
            this.dateRetourRPQS.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateRetourRPQS.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateRetourRPQS.Name = "dateRetourRPQS";
            this.dateRetourRPQS.ShowCheckBox = true;
            this.dateRetourRPQS.Size = new System.Drawing.Size(105, 20);
            this.dateRetourRPQS.TabIndex = 8;
            this.dateRetourRPQS.ValueChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(174, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(73, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "et retour le ";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(8, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "Envoi le ";
            // 
            // dateEnvoiRPQS
            // 
            this.dateEnvoiRPQS.Checked = false;
            this.dateEnvoiRPQS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateEnvoiRPQS.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateEnvoiRPQS.Location = new System.Drawing.Point(65, 13);
            this.dateEnvoiRPQS.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateEnvoiRPQS.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateEnvoiRPQS.Name = "dateEnvoiRPQS";
            this.dateEnvoiRPQS.ShowCheckBox = true;
            this.dateEnvoiRPQS.Size = new System.Drawing.Size(105, 20);
            this.dateEnvoiRPQS.TabIndex = 5;
            this.dateEnvoiRPQS.ValueChanged += new System.EventHandler(this.dateEnvoiRPQS_ValueChanged);
            // 
            // panelArreteQ
            // 
            this.panelArreteQ.Controls.Add(this.dateArreteDUP);
            this.panelArreteQ.Controls.Add(this.labelArreteDUP);
            this.panelArreteQ.Location = new System.Drawing.Point(3, 481);
            this.panelArreteQ.Name = "panelArreteQ";
            this.panelArreteQ.Size = new System.Drawing.Size(642, 25);
            this.panelArreteQ.TabIndex = 130;
            // 
            // dateArreteDUP
            // 
            this.dateArreteDUP.Checked = false;
            this.dateArreteDUP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateArreteDUP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateArreteDUP.Location = new System.Drawing.Point(109, 3);
            this.dateArreteDUP.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateArreteDUP.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateArreteDUP.Name = "dateArreteDUP";
            this.dateArreteDUP.ShowCheckBox = true;
            this.dateArreteDUP.Size = new System.Drawing.Size(105, 20);
            this.dateArreteDUP.TabIndex = 128;
            this.dateArreteDUP.ValueChanged += new System.EventHandler(this.ModificationLigneConvention);
            // 
            // labelArreteDUP
            // 
            this.labelArreteDUP.AutoSize = true;
            this.labelArreteDUP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelArreteDUP.Location = new System.Drawing.Point(24, 7);
            this.labelArreteDUP.Name = "labelArreteDUP";
            this.labelArreteDUP.Size = new System.Drawing.Size(79, 13);
            this.labelArreteDUP.TabIndex = 129;
            this.labelArreteDUP.Text = "Arrêté DUP :";
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage6.Controls.Add(this.dataGridViewCollectivitesConvention);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(653, 488);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Collectivités impactées";
            // 
            // dataGridViewCollectivitesConvention
            // 
            this.dataGridViewCollectivitesConvention.AllowUserToAddRows = false;
            this.dataGridViewCollectivitesConvention.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCollectivitesConvention.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewCollectivitesConvention.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCollectivitesConvention.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodeCollectiviteImpacConvention,
            this.CollectiviteImpacConvention,
            this.PopDGFConvention});
            this.dataGridViewCollectivitesConvention.ContextMenuStrip = this.menuStripCollectiviteConvention;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCollectivitesConvention.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewCollectivitesConvention.Location = new System.Drawing.Point(128, 19);
            this.dataGridViewCollectivitesConvention.MultiSelect = false;
            this.dataGridViewCollectivitesConvention.Name = "dataGridViewCollectivitesConvention";
            this.dataGridViewCollectivitesConvention.ReadOnly = true;
            this.dataGridViewCollectivitesConvention.RowHeadersVisible = false;
            this.dataGridViewCollectivitesConvention.RowTemplate.ContextMenuStrip = this.menuStripRowCollectiviteConvention;
            this.dataGridViewCollectivitesConvention.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCollectivitesConvention.Size = new System.Drawing.Size(402, 506);
            this.dataGridViewCollectivitesConvention.TabIndex = 0;
            this.dataGridViewCollectivitesConvention.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewCollectivitesConvention_MouseDown);
            // 
            // CodeCollectiviteImpacConvention
            // 
            this.CodeCollectiviteImpacConvention.HeaderText = "Code INSEE";
            this.CodeCollectiviteImpacConvention.Name = "CodeCollectiviteImpacConvention";
            this.CodeCollectiviteImpacConvention.ReadOnly = true;
            // 
            // CollectiviteImpacConvention
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CollectiviteImpacConvention.DefaultCellStyle = dataGridViewCellStyle5;
            this.CollectiviteImpacConvention.HeaderText = "Collectivité";
            this.CollectiviteImpacConvention.Name = "CollectiviteImpacConvention";
            this.CollectiviteImpacConvention.ReadOnly = true;
            this.CollectiviteImpacConvention.Width = 200;
            // 
            // PopDGFConvention
            // 
            this.PopDGFConvention.HeaderText = "Population DGF";
            this.PopDGFConvention.Name = "PopDGFConvention";
            this.PopDGFConvention.ReadOnly = true;
            this.PopDGFConvention.Width = 80;
            // 
            // menuStripCollectiviteConvention
            // 
            this.menuStripCollectiviteConvention.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUneCollectivitéToolStripMenuItem});
            this.menuStripCollectiviteConvention.Name = "menuStripCollectiviteConvention";
            this.menuStripCollectiviteConvention.Size = new System.Drawing.Size(197, 26);
            // 
            // ajouterUneCollectivitéToolStripMenuItem
            // 
            this.ajouterUneCollectivitéToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUneCollectivitéToolStripMenuItem.Name = "ajouterUneCollectivitéToolStripMenuItem";
            this.ajouterUneCollectivitéToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ajouterUneCollectivitéToolStripMenuItem.Text = "Ajouter une collectivité";
            this.ajouterUneCollectivitéToolStripMenuItem.Click += new System.EventHandler(this.ajouterUneCollectivitéToolStripMenuItem_Click);
            // 
            // menuStripRowCollectiviteConvention
            // 
            this.menuStripRowCollectiviteConvention.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUneCollectivitéToolStripMenuItem1,
            this.supprimerLaCollectivitéToolStripMenuItem});
            this.menuStripRowCollectiviteConvention.Name = "menuStripRowCollectiviteConvention";
            this.menuStripRowCollectiviteConvention.Size = new System.Drawing.Size(202, 48);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(208, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "ANNEE :";
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
            // toolStripConventions
            // 
            this.toolStripConventions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonFichier,
            this.toolStripButtonActualiser,
            this.toolStripSplitButtonExcel,
            this.toolStripLabel_Session});
            this.toolStripConventions.Location = new System.Drawing.Point(0, 0);
            this.toolStripConventions.Name = "toolStripConventions";
            this.toolStripConventions.Size = new System.Drawing.Size(1064, 25);
            this.toolStripConventions.TabIndex = 10;
            this.toolStripConventions.Text = "toolStrip1";
            // 
            // toolStripSplitButtonExcel
            // 
            this.toolStripSplitButtonExcel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extraireLesConventionsActivesToolStripMenuItem});
            this.toolStripSplitButtonExcel.Image = global::ATE55.Properties.Resources.excel;
            this.toolStripSplitButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonExcel.Name = "toolStripSplitButtonExcel";
            this.toolStripSplitButtonExcel.Size = new System.Drawing.Size(77, 22);
            this.toolStripSplitButtonExcel.Text = "Extraire";
            // 
            // extraireLesConventionsActivesToolStripMenuItem
            // 
            this.extraireLesConventionsActivesToolStripMenuItem.Image = global::ATE55.Properties.Resources.excel;
            this.extraireLesConventionsActivesToolStripMenuItem.Name = "extraireLesConventionsActivesToolStripMenuItem";
            this.extraireLesConventionsActivesToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.extraireLesConventionsActivesToolStripMenuItem.Text = "Extraire les conventions actives";
            this.extraireLesConventionsActivesToolStripMenuItem.Click += new System.EventHandler(this.extraireLesConventionsActivesToolStripMenuItem_Click);
            // 
            // frmConventions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 720);
            this.Controls.Add(this.splitContainerConventions);
            this.Controls.Add(this.toolStripConventions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1080, 758);
            this.Name = "frmConventions";
            this.Text = "FormConventions";
            this.Load += new System.EventHandler(this.frmConventions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewConventions)).EndInit();
            this.menuStripGridConventions.ResumeLayout(false);
            this.menuStripRowConventions.ResumeLayout(false);
            this.splitContainerConventions.Panel1.ResumeLayout(false);
            this.splitContainerConventions.Panel1.PerformLayout();
            this.splitContainerConventions.Panel2.ResumeLayout(false);
            this.splitContainerConventions.ResumeLayout(false);
            this.tabConvention.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl5.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.panelLigneConvention.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantConvention)).EndInit();
            this.tabSPAC.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantAnnexe)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabInfosTitre.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panelTitre.ResumeLayout(false);
            this.panelTitre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTitre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantTitre)).EndInit();
            this.tabRPQS.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panelArreteQ.ResumeLayout(false);
            this.panelArreteQ.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollectivitesConvention)).EndInit();
            this.menuStripCollectiviteConvention.ResumeLayout(false);
            this.menuStripRowCollectiviteConvention.ResumeLayout(false);
            this.toolStripConventions.ResumeLayout(false);
            this.toolStripConventions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewConventions;
        private System.Windows.Forms.TextBox searchConventionBox;
        private System.Windows.Forms.CheckBox checkAfficherConventions;
        private System.Windows.Forms.SplitContainer splitContainerConventions;
        private CustomComboBox.CustomComboBox comboTypeConvention;
        private CustomComboBox.CustomComboBox comboThemeConvention;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label infosModifConvention;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ContextMenuStrip menuStripGridConventions;
        private System.Windows.Forms.ToolStripMenuItem creerUneConventionToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuStripRowConventions;
        private System.Windows.Forms.ToolStripMenuItem creerUneNouvelleConventionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerLaConventionToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dataGridViewCollectivitesConvention;
        private System.Windows.Forms.TabControl tabConvention;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dateFinPrevision;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.DateTimePicker dateDebutPrevision;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label labelAgenceEau;
        private CustomComboBox.CustomComboBox comboChoixTypeConvention;
        private System.Windows.Forms.Button enregistrerConventionBouton;
        private System.Windows.Forms.Button annulerConventionBouton;
        private System.Windows.Forms.ComboBox comboCollectiviteConvention;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textObservationsConvention;
        private System.Windows.Forms.Label label28;
        private CustomComboBox.CustomComboBox comboAnneeConvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn idConvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollectiviteConvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnneesConvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeConvention;
        private System.Windows.Forms.ContextMenuStrip menuStripCollectiviteConvention;
        private System.Windows.Forms.ToolStripMenuItem ajouterUneCollectivitéToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuStripRowCollectiviteConvention;
        private System.Windows.Forms.ToolStripMenuItem ajouterUneCollectivitéToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem supprimerLaCollectivitéToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button enregistrerLigneConventionBouton;
        private System.Windows.Forms.Button genererConvention;
        private System.Windows.Forms.FlowLayoutPanel panelLigneConvention;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateEnvoiSignature;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateRetourSignature;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateSignature;
        private System.Windows.Forms.DateTimePicker dateEnvoiCollectivite;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DateTimePicker dateRetourRevision;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dateEnvoiRevision;
        private System.Windows.Forms.NumericUpDown numericMontantConvention;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabControl tabSPAC;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numericMontantAnnexe;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox checkMandatSPAC;
        private System.Windows.Forms.DateTimePicker dateRetourAnnexe;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dateEnvoiAnnexe;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textObservationsLigne;
        private System.Windows.Forms.TextBox textNonRecouvre;
        private System.Windows.Forms.TabControl tabInfosTitre;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelTitre;
        private System.Windows.Forms.Panel panelTitre;
        private System.Windows.Forms.Label labelEuros;
        private System.Windows.Forms.DateTimePicker dateEmissionTitre;
        private System.Windows.Forms.NumericUpDown numericTitre;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numericMontantTitre;
        private System.Windows.Forms.Label label10;
        private CustomComboBox.CustomComboBox comboTitre;
        private System.Windows.Forms.TabControl tabRPQS;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkMandatRPQS;
        private System.Windows.Forms.DateTimePicker dateRetourRPQS;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dateEnvoiRPQS;
        private System.Windows.Forms.Button genererAnnexeBouton;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelArreteQ;
        private System.Windows.Forms.DateTimePicker dateArreteDUP;
        private System.Windows.Forms.Label labelArreteDUP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodeCollectiviteImpacConvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollectiviteImpacConvention;
        private System.Windows.Forms.DataGridViewTextBoxColumn PopDGFConvention;
        private System.Windows.Forms.DateTimePicker dateFinConvention;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.DateTimePicker dateDebutConvention;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonFichier;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualiser;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Session;
        private System.Windows.Forms.ToolStrip toolStripConventions;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonExcel;
        private System.Windows.Forms.ToolStripMenuItem extraireLesConventionsActivesToolStripMenuItem;
    }
}