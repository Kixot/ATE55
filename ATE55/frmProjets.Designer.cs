namespace ATE55 {
    partial class frmProjets {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjets));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewProjets = new System.Windows.Forms.DataGridView();
            this.IdProjet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CollectiviteProjet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntituleProjet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeProjet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnneeDemarrageProjet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EtatProjet = new System.Windows.Forms.DataGridViewImageColumn();
            this.NbMarches = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripGridProjets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUnProjetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripGridRowProjets = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUnProjetRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerProjet = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIntituleProjet = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCollectiviteRefProjet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewCollectivitesProjet = new System.Windows.Forms.DataGridView();
            this.CodeCollectiviteImpactee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomCollectivite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PopulationDGFCollectivite = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripGridCollectivites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUneCollectivitéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripGridRowCollectivites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUneCollectivitéToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerLaCollectivitéToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewMarches = new System.Windows.Forms.DataGridView();
            this.idMarche = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EtatMarche = new System.Windows.Forms.DataGridViewImageColumn();
            this.NomPrestataireMarche = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IntituleMarche = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeMarche = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateSignatureMarche = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MontantMarche = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripGridMarches = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUnNouveauMarcheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripGridRowMarches = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterUnNouveauMarcheRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerLeMarcheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textRemarquesProjet = new System.Windows.Forms.TextBox();
            this.labelRemarquesProjet = new System.Windows.Forms.Label();
            this.labelEuros = new System.Windows.Forms.Label();
            this.labelMontantPrevisionnelProjet = new System.Windows.Forms.Label();
            this.tabProjet = new System.Windows.Forms.TabControl();
            this.tabPageProjet = new System.Windows.Forms.TabPage();
            this.infosModifProjet = new System.Windows.Forms.Label();
            this.numericAnneeDemarrage = new System.Windows.Forms.NumericUpDown();
            this.numericMontantProjet = new System.Windows.Forms.NumericUpDown();
            this.labelNbCollectivitesImpactees = new System.Windows.Forms.Label();
            this.dataGridViewHistoriqueEtatProjet = new System.Windows.Forms.DataGridView();
            this.DateEtatAvancementHistorique = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EtatAvancementHistorique = new System.Windows.Forms.DataGridViewImageColumn();
            this.AuteurHistorique = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxEtatProjet = new CustomComboBox.CustomComboBox();
            this.imageListProjets = new System.Windows.Forms.ImageList(this.components);
            this.annulerProjet = new System.Windows.Forms.Button();
            this.sauvegarderProjetBouton = new System.Windows.Forms.Button();
            this.comboBoxTypeProjet = new CustomComboBox.CustomComboBox();
            this.tabPageMarche = new System.Windows.Forms.TabPage();
            this.groupBoxMarche = new System.Windows.Forms.GroupBox();
            this.checkSuiviSATE = new System.Windows.Forms.CheckBox();
            this.infosModifMarche = new System.Windows.Forms.Label();
            this.numericMontantMarche = new System.Windows.Forms.NumericUpDown();
            this.dataGridViewHistoriqueEtatsMarche = new System.Windows.Forms.DataGridView();
            this.DateEtatMarcheHistorique = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EtatMarcheHistorique = new System.Windows.Forms.DataGridViewImageColumn();
            this.AuteurEtatMarcheHistorique = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label16 = new System.Windows.Forms.Label();
            this.annulerMarche = new System.Windows.Forms.Button();
            this.modifMarche = new System.Windows.Forms.Button();
            this.textBoxRemarquesMarche = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dateTimePickerSignatureMarche = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxTypeMarche = new CustomComboBox.CustomComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxIntituleMarche = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPrestataire = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxEtatMarche = new CustomComboBox.CustomComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStripProjets = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonFichier = new System.Windows.Forms.ToolStripSplitButton();
            this.quitterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonActualiser = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_Session = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textRecherche = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.checkAfficherTousLesProjets = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjets)).BeginInit();
            this.menuStripGridProjets.SuspendLayout();
            this.menuStripGridRowProjets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollectivitesProjet)).BeginInit();
            this.menuStripGridCollectivites.SuspendLayout();
            this.menuStripGridRowCollectivites.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMarches)).BeginInit();
            this.menuStripGridMarches.SuspendLayout();
            this.menuStripGridRowMarches.SuspendLayout();
            this.tabProjet.SuspendLayout();
            this.tabPageProjet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnneeDemarrage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantProjet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoriqueEtatProjet)).BeginInit();
            this.tabPageMarche.SuspendLayout();
            this.groupBoxMarche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantMarche)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoriqueEtatsMarche)).BeginInit();
            this.toolStripProjets.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewProjets
            // 
            this.dataGridViewProjets.AllowUserToAddRows = false;
            this.dataGridViewProjets.AllowUserToDeleteRows = false;
            this.dataGridViewProjets.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewProjets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProjets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdProjet,
            this.CollectiviteProjet,
            this.IntituleProjet,
            this.TypeProjet,
            this.AnneeDemarrageProjet,
            this.EtatProjet,
            this.NbMarches});
            this.dataGridViewProjets.ContextMenuStrip = this.menuStripGridProjets;
            this.dataGridViewProjets.Location = new System.Drawing.Point(0, 43);
            this.dataGridViewProjets.MultiSelect = false;
            this.dataGridViewProjets.Name = "dataGridViewProjets";
            this.dataGridViewProjets.ReadOnly = true;
            this.dataGridViewProjets.RowHeadersVisible = false;
            this.dataGridViewProjets.RowHeadersWidth = 35;
            this.dataGridViewProjets.RowTemplate.ContextMenuStrip = this.menuStripGridRowProjets;
            this.dataGridViewProjets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewProjets.Size = new System.Drawing.Size(436, 647);
            this.dataGridViewProjets.TabIndex = 0;
            this.dataGridViewProjets.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewProjets_RowStateChanged);
            this.dataGridViewProjets.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewProjets_MouseDown);
            // 
            // IdProjet
            // 
            this.IdProjet.HeaderText = "IdProjet";
            this.IdProjet.Name = "IdProjet";
            this.IdProjet.ReadOnly = true;
            this.IdProjet.Visible = false;
            // 
            // CollectiviteProjet
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CollectiviteProjet.DefaultCellStyle = dataGridViewCellStyle1;
            this.CollectiviteProjet.HeaderText = "Collectivité";
            this.CollectiviteProjet.Name = "CollectiviteProjet";
            this.CollectiviteProjet.ReadOnly = true;
            this.CollectiviteProjet.Width = 150;
            // 
            // IntituleProjet
            // 
            this.IntituleProjet.HeaderText = "Intitulé";
            this.IntituleProjet.Name = "IntituleProjet";
            this.IntituleProjet.ReadOnly = true;
            // 
            // TypeProjet
            // 
            this.TypeProjet.HeaderText = "Type";
            this.TypeProjet.Name = "TypeProjet";
            this.TypeProjet.ReadOnly = true;
            this.TypeProjet.Width = 50;
            // 
            // AnneeDemarrageProjet
            // 
            this.AnneeDemarrageProjet.HeaderText = "Année";
            this.AnneeDemarrageProjet.Name = "AnneeDemarrageProjet";
            this.AnneeDemarrageProjet.ReadOnly = true;
            this.AnneeDemarrageProjet.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AnneeDemarrageProjet.Width = 40;
            // 
            // EtatProjet
            // 
            this.EtatProjet.HeaderText = "Etat";
            this.EtatProjet.Name = "EtatProjet";
            this.EtatProjet.ReadOnly = true;
            this.EtatProjet.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EtatProjet.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.EtatProjet.Width = 35;
            // 
            // NbMarches
            // 
            this.NbMarches.HeaderText = "Marchés";
            this.NbMarches.Name = "NbMarches";
            this.NbMarches.ReadOnly = true;
            this.NbMarches.Width = 35;
            // 
            // menuStripGridProjets
            // 
            this.menuStripGridProjets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUnProjetToolStripMenuItem});
            this.menuStripGridProjets.Name = "menuStripGridProjets";
            this.menuStripGridProjets.Size = new System.Drawing.Size(203, 26);
            // 
            // ajouterUnProjetToolStripMenuItem
            // 
            this.ajouterUnProjetToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUnProjetToolStripMenuItem.Name = "ajouterUnProjetToolStripMenuItem";
            this.ajouterUnProjetToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.ajouterUnProjetToolStripMenuItem.Text = "Créer un nouveau projet";
            this.ajouterUnProjetToolStripMenuItem.Click += new System.EventHandler(this.ajouterUnProjetToolStripMenuItem_Click);
            // 
            // menuStripGridRowProjets
            // 
            this.menuStripGridRowProjets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUnProjetRowToolStripMenuItem,
            this.supprimerProjet});
            this.menuStripGridRowProjets.Name = "menuStripGridProjets";
            this.menuStripGridRowProjets.Size = new System.Drawing.Size(203, 48);
            // 
            // ajouterUnProjetRowToolStripMenuItem
            // 
            this.ajouterUnProjetRowToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUnProjetRowToolStripMenuItem.Name = "ajouterUnProjetRowToolStripMenuItem";
            this.ajouterUnProjetRowToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.ajouterUnProjetRowToolStripMenuItem.Text = "Créer un nouveau projet";
            this.ajouterUnProjetRowToolStripMenuItem.Click += new System.EventHandler(this.ajouterUnProjetToolStripMenuItem_Click);
            // 
            // supprimerProjet
            // 
            this.supprimerProjet.Image = global::ATE55.Properties.Resources.Valeur_Suppr;
            this.supprimerProjet.Name = "supprimerProjet";
            this.supprimerProjet.Size = new System.Drawing.Size(202, 22);
            this.supprimerProjet.Text = "Supprimer le projet";
            this.supprimerProjet.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(199, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Liste des collectivités impactées :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Etat du projet :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Type du projet :";
            // 
            // textBoxIntituleProjet
            // 
            this.textBoxIntituleProjet.Location = new System.Drawing.Point(66, 49);
            this.textBoxIntituleProjet.Name = "textBoxIntituleProjet";
            this.textBoxIntituleProjet.Size = new System.Drawing.Size(437, 20);
            this.textBoxIntituleProjet.TabIndex = 18;
            this.textBoxIntituleProjet.TextChanged += new System.EventHandler(this.Modification);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Intitulé :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Année de démarrage :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxCollectiviteRefProjet
            // 
            this.comboBoxCollectiviteRefProjet.DisplayMember = "CodeCollectivite";
            this.comboBoxCollectiviteRefProjet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCollectiviteRefProjet.FormattingEnabled = true;
            this.comboBoxCollectiviteRefProjet.Location = new System.Drawing.Point(166, 14);
            this.comboBoxCollectiviteRefProjet.Name = "comboBoxCollectiviteRefProjet";
            this.comboBoxCollectiviteRefProjet.Size = new System.Drawing.Size(327, 21);
            this.comboBoxCollectiviteRefProjet.TabIndex = 14;
            this.comboBoxCollectiviteRefProjet.ValueMember = "CodeCollectivite";
            this.comboBoxCollectiviteRefProjet.SelectedValueChanged += new System.EventHandler(this.Modification);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Collectivité de référence :";
            // 
            // dataGridViewCollectivitesProjet
            // 
            this.dataGridViewCollectivitesProjet.AllowUserToAddRows = false;
            this.dataGridViewCollectivitesProjet.AllowUserToDeleteRows = false;
            this.dataGridViewCollectivitesProjet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewCollectivitesProjet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCollectivitesProjet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodeCollectiviteImpactee,
            this.NomCollectivite,
            this.PopulationDGFCollectivite});
            this.dataGridViewCollectivitesProjet.ContextMenuStrip = this.menuStripGridCollectivites;
            this.dataGridViewCollectivitesProjet.Location = new System.Drawing.Point(10, 298);
            this.dataGridViewCollectivitesProjet.MultiSelect = false;
            this.dataGridViewCollectivitesProjet.Name = "dataGridViewCollectivitesProjet";
            this.dataGridViewCollectivitesProjet.ReadOnly = true;
            this.dataGridViewCollectivitesProjet.RowHeadersVisible = false;
            this.dataGridViewCollectivitesProjet.RowTemplate.ContextMenuStrip = this.menuStripGridRowCollectivites;
            this.dataGridViewCollectivitesProjet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCollectivitesProjet.Size = new System.Drawing.Size(305, 360);
            this.dataGridViewCollectivitesProjet.TabIndex = 11;
            this.dataGridViewCollectivitesProjet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewCollectivitesProjet_MouseDown);
            // 
            // CodeCollectiviteImpactee
            // 
            this.CodeCollectiviteImpactee.HeaderText = "Column1";
            this.CodeCollectiviteImpactee.Name = "CodeCollectiviteImpactee";
            this.CodeCollectiviteImpactee.ReadOnly = true;
            this.CodeCollectiviteImpactee.Visible = false;
            // 
            // NomCollectivite
            // 
            this.NomCollectivite.HeaderText = "Collectivité";
            this.NomCollectivite.Name = "NomCollectivite";
            this.NomCollectivite.ReadOnly = true;
            this.NomCollectivite.Width = 200;
            // 
            // PopulationDGFCollectivite
            // 
            this.PopulationDGFCollectivite.HeaderText = "Population DGF";
            this.PopulationDGFCollectivite.Name = "PopulationDGFCollectivite";
            this.PopulationDGFCollectivite.ReadOnly = true;
            this.PopulationDGFCollectivite.Width = 80;
            // 
            // menuStripGridCollectivites
            // 
            this.menuStripGridCollectivites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUneCollectivitéToolStripMenuItem});
            this.menuStripGridCollectivites.Name = "menuStripGridCollectivites";
            this.menuStripGridCollectivites.Size = new System.Drawing.Size(197, 26);
            // 
            // ajouterUneCollectivitéToolStripMenuItem
            // 
            this.ajouterUneCollectivitéToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUneCollectivitéToolStripMenuItem.Name = "ajouterUneCollectivitéToolStripMenuItem";
            this.ajouterUneCollectivitéToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ajouterUneCollectivitéToolStripMenuItem.Text = "Ajouter une collectivité";
            this.ajouterUneCollectivitéToolStripMenuItem.Click += new System.EventHandler(this.ajouterUneCollectivitéToolStripMenuItem_Click);
            // 
            // menuStripGridRowCollectivites
            // 
            this.menuStripGridRowCollectivites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUneCollectivitéToolStripMenuItem1,
            this.supprimerLaCollectivitéToolStripMenuItem});
            this.menuStripGridRowCollectivites.Name = "menuStripGridRowCollectivites";
            this.menuStripGridRowCollectivites.Size = new System.Drawing.Size(202, 48);
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
            // dataGridViewMarches
            // 
            this.dataGridViewMarches.AllowUserToAddRows = false;
            this.dataGridViewMarches.AllowUserToDeleteRows = false;
            this.dataGridViewMarches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewMarches.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMarches.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idMarche,
            this.EtatMarche,
            this.NomPrestataireMarche,
            this.IntituleMarche,
            this.TypeMarche,
            this.DateSignatureMarche,
            this.MontantMarche});
            this.dataGridViewMarches.ContextMenuStrip = this.menuStripGridMarches;
            this.dataGridViewMarches.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewMarches.MultiSelect = false;
            this.dataGridViewMarches.Name = "dataGridViewMarches";
            this.dataGridViewMarches.ReadOnly = true;
            this.dataGridViewMarches.RowHeadersVisible = false;
            this.dataGridViewMarches.RowTemplate.ContextMenuStrip = this.menuStripGridRowMarches;
            this.dataGridViewMarches.RowTemplate.Height = 25;
            this.dataGridViewMarches.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMarches.Size = new System.Drawing.Size(609, 192);
            this.dataGridViewMarches.TabIndex = 5;
            this.dataGridViewMarches.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dataGridViewMarches_RowStateChanged);
            this.dataGridViewMarches.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridViewMarches_MouseDown);
            // 
            // idMarche
            // 
            this.idMarche.HeaderText = "idMarche";
            this.idMarche.Name = "idMarche";
            this.idMarche.ReadOnly = true;
            this.idMarche.Visible = false;
            // 
            // EtatMarche
            // 
            this.EtatMarche.HeaderText = "Etat";
            this.EtatMarche.Name = "EtatMarche";
            this.EtatMarche.ReadOnly = true;
            this.EtatMarche.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EtatMarche.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.EtatMarche.Width = 35;
            // 
            // NomPrestataireMarche
            // 
            this.NomPrestataireMarche.HeaderText = "Prestataire";
            this.NomPrestataireMarche.Name = "NomPrestataireMarche";
            this.NomPrestataireMarche.ReadOnly = true;
            this.NomPrestataireMarche.Width = 125;
            // 
            // IntituleMarche
            // 
            this.IntituleMarche.HeaderText = "Intitulé";
            this.IntituleMarche.Name = "IntituleMarche";
            this.IntituleMarche.ReadOnly = true;
            this.IntituleMarche.Width = 180;
            // 
            // TypeMarche
            // 
            this.TypeMarche.HeaderText = "Type";
            this.TypeMarche.Name = "TypeMarche";
            this.TypeMarche.ReadOnly = true;
            this.TypeMarche.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TypeMarche.Width = 75;
            // 
            // DateSignatureMarche
            // 
            this.DateSignatureMarche.HeaderText = "Signature";
            this.DateSignatureMarche.Name = "DateSignatureMarche";
            this.DateSignatureMarche.ReadOnly = true;
            this.DateSignatureMarche.Width = 70;
            // 
            // MontantMarche
            // 
            this.MontantMarche.HeaderText = "Montant";
            this.MontantMarche.Name = "MontantMarche";
            this.MontantMarche.ReadOnly = true;
            // 
            // menuStripGridMarches
            // 
            this.menuStripGridMarches.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUnNouveauMarcheToolStripMenuItem});
            this.menuStripGridMarches.Name = "menuStripGridMarches";
            this.menuStripGridMarches.Size = new System.Drawing.Size(212, 26);
            // 
            // ajouterUnNouveauMarcheToolStripMenuItem
            // 
            this.ajouterUnNouveauMarcheToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUnNouveauMarcheToolStripMenuItem.Name = "ajouterUnNouveauMarcheToolStripMenuItem";
            this.ajouterUnNouveauMarcheToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.ajouterUnNouveauMarcheToolStripMenuItem.Text = "Créer un nouveau marché";
            this.ajouterUnNouveauMarcheToolStripMenuItem.Click += new System.EventHandler(this.ajouterUnNouveauMarcheToolStripMenuItem_Click);
            // 
            // menuStripGridRowMarches
            // 
            this.menuStripGridRowMarches.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUnNouveauMarcheRowToolStripMenuItem,
            this.supprimerLeMarcheToolStripMenuItem});
            this.menuStripGridRowMarches.Name = "contextMenuStrip1";
            this.menuStripGridRowMarches.Size = new System.Drawing.Size(212, 48);
            // 
            // ajouterUnNouveauMarcheRowToolStripMenuItem
            // 
            this.ajouterUnNouveauMarcheRowToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.ajouterUnNouveauMarcheRowToolStripMenuItem.Name = "ajouterUnNouveauMarcheRowToolStripMenuItem";
            this.ajouterUnNouveauMarcheRowToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.ajouterUnNouveauMarcheRowToolStripMenuItem.Text = "Créer un nouveau marché";
            this.ajouterUnNouveauMarcheRowToolStripMenuItem.Click += new System.EventHandler(this.ajouterUnNouveauMarcheToolStripMenuItem_Click);
            // 
            // supprimerLeMarcheToolStripMenuItem
            // 
            this.supprimerLeMarcheToolStripMenuItem.Image = global::ATE55.Properties.Resources.Valeur_Suppr;
            this.supprimerLeMarcheToolStripMenuItem.Name = "supprimerLeMarcheToolStripMenuItem";
            this.supprimerLeMarcheToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.supprimerLeMarcheToolStripMenuItem.Text = "Supprimer le marché";
            this.supprimerLeMarcheToolStripMenuItem.Click += new System.EventHandler(this.supprimerLeMarcheToolStripMenuItem_Click);
            // 
            // textRemarquesProjet
            // 
            this.textRemarquesProjet.Location = new System.Drawing.Point(90, 224);
            this.textRemarquesProjet.MinimumSize = new System.Drawing.Size(4, 40);
            this.textRemarquesProjet.Multiline = true;
            this.textRemarquesProjet.Name = "textRemarquesProjet";
            this.textRemarquesProjet.Size = new System.Drawing.Size(513, 40);
            this.textRemarquesProjet.TabIndex = 4;
            this.textRemarquesProjet.TextChanged += new System.EventHandler(this.Modification);
            // 
            // labelRemarquesProjet
            // 
            this.labelRemarquesProjet.AutoSize = true;
            this.labelRemarquesProjet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRemarquesProjet.Location = new System.Drawing.Point(6, 227);
            this.labelRemarquesProjet.Name = "labelRemarquesProjet";
            this.labelRemarquesProjet.Size = new System.Drawing.Size(78, 13);
            this.labelRemarquesProjet.TabIndex = 3;
            this.labelRemarquesProjet.Text = "Remarques :";
            // 
            // labelEuros
            // 
            this.labelEuros.AutoSize = true;
            this.labelEuros.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEuros.Location = new System.Drawing.Point(310, 192);
            this.labelEuros.Name = "labelEuros";
            this.labelEuros.Size = new System.Drawing.Size(14, 13);
            this.labelEuros.TabIndex = 2;
            this.labelEuros.Text = "€";
            // 
            // labelMontantPrevisionnelProjet
            // 
            this.labelMontantPrevisionnelProjet.AutoSize = true;
            this.labelMontantPrevisionnelProjet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMontantPrevisionnelProjet.Location = new System.Drawing.Point(6, 192);
            this.labelMontantPrevisionnelProjet.Name = "labelMontantPrevisionnelProjet";
            this.labelMontantPrevisionnelProjet.Size = new System.Drawing.Size(187, 13);
            this.labelMontantPrevisionnelProjet.TabIndex = 0;
            this.labelMontantPrevisionnelProjet.Text = "Montant prévisionnel du projet :";
            // 
            // tabProjet
            // 
            this.tabProjet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabProjet.Controls.Add(this.tabPageProjet);
            this.tabProjet.Controls.Add(this.tabPageMarche);
            this.tabProjet.Location = new System.Drawing.Point(-3, -1);
            this.tabProjet.Name = "tabProjet";
            this.tabProjet.SelectedIndex = 0;
            this.tabProjet.Size = new System.Drawing.Size(626, 692);
            this.tabProjet.TabIndex = 6;
            this.tabProjet.Visible = false;
            this.tabProjet.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabProjet_Selecting);
            // 
            // tabPageProjet
            // 
            this.tabPageProjet.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageProjet.Controls.Add(this.infosModifProjet);
            this.tabPageProjet.Controls.Add(this.numericAnneeDemarrage);
            this.tabPageProjet.Controls.Add(this.numericMontantProjet);
            this.tabPageProjet.Controls.Add(this.labelNbCollectivitesImpactees);
            this.tabPageProjet.Controls.Add(this.dataGridViewCollectivitesProjet);
            this.tabPageProjet.Controls.Add(this.dataGridViewHistoriqueEtatProjet);
            this.tabPageProjet.Controls.Add(this.label15);
            this.tabPageProjet.Controls.Add(this.comboBoxEtatProjet);
            this.tabPageProjet.Controls.Add(this.label6);
            this.tabPageProjet.Controls.Add(this.annulerProjet);
            this.tabPageProjet.Controls.Add(this.sauvegarderProjetBouton);
            this.tabPageProjet.Controls.Add(this.label5);
            this.tabPageProjet.Controls.Add(this.label1);
            this.tabPageProjet.Controls.Add(this.comboBoxTypeProjet);
            this.tabPageProjet.Controls.Add(this.textRemarquesProjet);
            this.tabPageProjet.Controls.Add(this.comboBoxCollectiviteRefProjet);
            this.tabPageProjet.Controls.Add(this.label3);
            this.tabPageProjet.Controls.Add(this.labelRemarquesProjet);
            this.tabPageProjet.Controls.Add(this.textBoxIntituleProjet);
            this.tabPageProjet.Controls.Add(this.labelEuros);
            this.tabPageProjet.Controls.Add(this.label4);
            this.tabPageProjet.Controls.Add(this.label2);
            this.tabPageProjet.Controls.Add(this.labelMontantPrevisionnelProjet);
            this.tabPageProjet.Location = new System.Drawing.Point(4, 22);
            this.tabPageProjet.Name = "tabPageProjet";
            this.tabPageProjet.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProjet.Size = new System.Drawing.Size(618, 666);
            this.tabPageProjet.TabIndex = 0;
            this.tabPageProjet.Text = "Fiche du projet";
            // 
            // infosModifProjet
            // 
            this.infosModifProjet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infosModifProjet.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.infosModifProjet.Location = new System.Drawing.Point(321, 625);
            this.infosModifProjet.Name = "infosModifProjet";
            this.infosModifProjet.Size = new System.Drawing.Size(294, 38);
            this.infosModifProjet.TabIndex = 127;
            this.infosModifProjet.Text = "...";
            this.infosModifProjet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericAnneeDemarrage
            // 
            this.numericAnneeDemarrage.Location = new System.Drawing.Point(145, 84);
            this.numericAnneeDemarrage.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericAnneeDemarrage.Name = "numericAnneeDemarrage";
            this.numericAnneeDemarrage.Size = new System.Drawing.Size(55, 20);
            this.numericAnneeDemarrage.TabIndex = 126;
            this.numericAnneeDemarrage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericAnneeDemarrage.ValueChanged += new System.EventHandler(this.Modification);
            this.numericAnneeDemarrage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericUpDown1_KeyPress);
            // 
            // numericMontantProjet
            // 
            this.numericMontantProjet.DecimalPlaces = 2;
            this.numericMontantProjet.Location = new System.Drawing.Point(199, 189);
            this.numericMontantProjet.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numericMontantProjet.Name = "numericMontantProjet";
            this.numericMontantProjet.Size = new System.Drawing.Size(105, 20);
            this.numericMontantProjet.TabIndex = 125;
            this.numericMontantProjet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericMontantProjet.ThousandsSeparator = true;
            this.numericMontantProjet.ValueChanged += new System.EventHandler(this.Modification);
            this.numericMontantProjet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericMontantProjet_KeyPress);
            // 
            // labelNbCollectivitesImpactees
            // 
            this.labelNbCollectivitesImpactees.AutoSize = true;
            this.labelNbCollectivitesImpactees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNbCollectivitesImpactees.ForeColor = System.Drawing.Color.Blue;
            this.labelNbCollectivitesImpactees.Location = new System.Drawing.Point(205, 282);
            this.labelNbCollectivitesImpactees.Name = "labelNbCollectivitesImpactees";
            this.labelNbCollectivitesImpactees.Size = new System.Drawing.Size(22, 13);
            this.labelNbCollectivitesImpactees.TabIndex = 124;
            this.labelNbCollectivitesImpactees.Text = "(0)";
            // 
            // dataGridViewHistoriqueEtatProjet
            // 
            this.dataGridViewHistoriqueEtatProjet.AllowUserToAddRows = false;
            this.dataGridViewHistoriqueEtatProjet.AllowUserToDeleteRows = false;
            this.dataGridViewHistoriqueEtatProjet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewHistoriqueEtatProjet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistoriqueEtatProjet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateEtatAvancementHistorique,
            this.EtatAvancementHistorique,
            this.AuteurHistorique});
            this.dataGridViewHistoriqueEtatProjet.Location = new System.Drawing.Point(343, 298);
            this.dataGridViewHistoriqueEtatProjet.MultiSelect = false;
            this.dataGridViewHistoriqueEtatProjet.Name = "dataGridViewHistoriqueEtatProjet";
            this.dataGridViewHistoriqueEtatProjet.ReadOnly = true;
            this.dataGridViewHistoriqueEtatProjet.RowHeadersVisible = false;
            this.dataGridViewHistoriqueEtatProjet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistoriqueEtatProjet.Size = new System.Drawing.Size(257, 324);
            this.dataGridViewHistoriqueEtatProjet.TabIndex = 123;
            // 
            // DateEtatAvancementHistorique
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateEtatAvancementHistorique.DefaultCellStyle = dataGridViewCellStyle2;
            this.DateEtatAvancementHistorique.HeaderText = "Date";
            this.DateEtatAvancementHistorique.Name = "DateEtatAvancementHistorique";
            this.DateEtatAvancementHistorique.ReadOnly = true;
            this.DateEtatAvancementHistorique.Width = 140;
            // 
            // EtatAvancementHistorique
            // 
            this.EtatAvancementHistorique.HeaderText = "Etat";
            this.EtatAvancementHistorique.Name = "EtatAvancementHistorique";
            this.EtatAvancementHistorique.ReadOnly = true;
            this.EtatAvancementHistorique.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.EtatAvancementHistorique.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.EtatAvancementHistorique.Width = 35;
            // 
            // AuteurHistorique
            // 
            this.AuteurHistorique.HeaderText = "Auteur";
            this.AuteurHistorique.Name = "AuteurHistorique";
            this.AuteurHistorique.ReadOnly = true;
            this.AuteurHistorique.Width = 75;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(343, 282);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(126, 13);
            this.label15.TabIndex = 122;
            this.label15.Text = "Historique du projet :";
            // 
            // comboBoxEtatProjet
            // 
            this.comboBoxEtatProjet.AlignText = false;
            this.comboBoxEtatProjet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEtatProjet.FormattingEnabled = true;
            this.comboBoxEtatProjet.ImageList = this.imageListProjets;
            this.comboBoxEtatProjet.Location = new System.Drawing.Point(100, 154);
            this.comboBoxEtatProjet.Name = "comboBoxEtatProjet";
            this.comboBoxEtatProjet.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboBoxEtatProjet.Size = new System.Drawing.Size(127, 21);
            this.comboBoxEtatProjet.TabIndex = 25;
            this.comboBoxEtatProjet.SelectedValueChanged += new System.EventHandler(this.Modification);
            // 
            // imageListProjets
            // 
            this.imageListProjets.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListProjets.ImageStream")));
            this.imageListProjets.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListProjets.Images.SetKeyName(0, "vide");
            this.imageListProjets.Images.SetKeyName(1, "en_cours");
            this.imageListProjets.Images.SetKeyName(2, "Recherche");
            this.imageListProjets.Images.SetKeyName(3, "abandonne");
            this.imageListProjets.Images.SetKeyName(4, "termine");
            // 
            // annulerProjet
            // 
            this.annulerProjet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.annulerProjet.Enabled = false;
            this.annulerProjet.Image = global::ATE55.Properties.Resources.Annuler;
            this.annulerProjet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.annulerProjet.Location = new System.Drawing.Point(521, 43);
            this.annulerProjet.Name = "annulerProjet";
            this.annulerProjet.Size = new System.Drawing.Size(89, 24);
            this.annulerProjet.TabIndex = 12;
            this.annulerProjet.Text = "Annuler";
            this.annulerProjet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.annulerProjet.UseVisualStyleBackColor = true;
            this.annulerProjet.Click += new System.EventHandler(this.annulerProjet_Click);
            // 
            // sauvegarderProjetBouton
            // 
            this.sauvegarderProjetBouton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sauvegarderProjetBouton.Enabled = false;
            this.sauvegarderProjetBouton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sauvegarderProjetBouton.Image = global::ATE55.Properties.Resources.saveHS;
            this.sauvegarderProjetBouton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sauvegarderProjetBouton.Location = new System.Drawing.Point(494, 6);
            this.sauvegarderProjetBouton.Name = "sauvegarderProjetBouton";
            this.sauvegarderProjetBouton.Size = new System.Drawing.Size(116, 31);
            this.sauvegarderProjetBouton.TabIndex = 8;
            this.sauvegarderProjetBouton.Text = "Enregistrer";
            this.sauvegarderProjetBouton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.sauvegarderProjetBouton.UseVisualStyleBackColor = true;
            this.sauvegarderProjetBouton.Click += new System.EventHandler(this.sauvegarderProjetBouton_Click);
            // 
            // comboBoxTypeProjet
            // 
            this.comboBoxTypeProjet.AlignText = false;
            this.comboBoxTypeProjet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeProjet.FormattingEnabled = true;
            this.comboBoxTypeProjet.Location = new System.Drawing.Point(109, 119);
            this.comboBoxTypeProjet.Name = "comboBoxTypeProjet";
            this.comboBoxTypeProjet.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboBoxTypeProjet.Size = new System.Drawing.Size(183, 21);
            this.comboBoxTypeProjet.TabIndex = 24;
            this.comboBoxTypeProjet.SelectedValueChanged += new System.EventHandler(this.Modification);
            // 
            // tabPageMarche
            // 
            this.tabPageMarche.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMarche.Controls.Add(this.groupBoxMarche);
            this.tabPageMarche.Controls.Add(this.dataGridViewMarches);
            this.tabPageMarche.Location = new System.Drawing.Point(4, 22);
            this.tabPageMarche.Name = "tabPageMarche";
            this.tabPageMarche.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMarche.Size = new System.Drawing.Size(618, 666);
            this.tabPageMarche.TabIndex = 1;
            this.tabPageMarche.Text = "Marchés";
            // 
            // groupBoxMarche
            // 
            this.groupBoxMarche.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMarche.Controls.Add(this.checkSuiviSATE);
            this.groupBoxMarche.Controls.Add(this.infosModifMarche);
            this.groupBoxMarche.Controls.Add(this.numericMontantMarche);
            this.groupBoxMarche.Controls.Add(this.dataGridViewHistoriqueEtatsMarche);
            this.groupBoxMarche.Controls.Add(this.label16);
            this.groupBoxMarche.Controls.Add(this.annulerMarche);
            this.groupBoxMarche.Controls.Add(this.modifMarche);
            this.groupBoxMarche.Controls.Add(this.textBoxRemarquesMarche);
            this.groupBoxMarche.Controls.Add(this.label14);
            this.groupBoxMarche.Controls.Add(this.label13);
            this.groupBoxMarche.Controls.Add(this.label12);
            this.groupBoxMarche.Controls.Add(this.dateTimePickerSignatureMarche);
            this.groupBoxMarche.Controls.Add(this.label11);
            this.groupBoxMarche.Controls.Add(this.comboBoxTypeMarche);
            this.groupBoxMarche.Controls.Add(this.label10);
            this.groupBoxMarche.Controls.Add(this.textBoxIntituleMarche);
            this.groupBoxMarche.Controls.Add(this.label9);
            this.groupBoxMarche.Controls.Add(this.textBoxPrestataire);
            this.groupBoxMarche.Controls.Add(this.label8);
            this.groupBoxMarche.Controls.Add(this.comboBoxEtatMarche);
            this.groupBoxMarche.Controls.Add(this.label7);
            this.groupBoxMarche.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxMarche.Location = new System.Drawing.Point(3, 204);
            this.groupBoxMarche.Name = "groupBoxMarche";
            this.groupBoxMarche.Size = new System.Drawing.Size(612, 459);
            this.groupBoxMarche.TabIndex = 6;
            this.groupBoxMarche.TabStop = false;
            this.groupBoxMarche.Text = "Marché";
            this.groupBoxMarche.Visible = false;
            // 
            // checkSuiviSATE
            // 
            this.checkSuiviSATE.AutoSize = true;
            this.checkSuiviSATE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkSuiviSATE.Location = new System.Drawing.Point(408, 28);
            this.checkSuiviSATE.Name = "checkSuiviSATE";
            this.checkSuiviSATE.Size = new System.Drawing.Size(141, 17);
            this.checkSuiviSATE.TabIndex = 129;
            this.checkSuiviSATE.Text = "Assistance du SATE";
            this.checkSuiviSATE.UseVisualStyleBackColor = true;
            this.checkSuiviSATE.CheckedChanged += new System.EventHandler(this.ModificationMarche);
            // 
            // infosModifMarche
            // 
            this.infosModifMarche.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infosModifMarche.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.infosModifMarche.Location = new System.Drawing.Point(282, 418);
            this.infosModifMarche.Name = "infosModifMarche";
            this.infosModifMarche.Size = new System.Drawing.Size(327, 38);
            this.infosModifMarche.TabIndex = 128;
            this.infosModifMarche.Text = "...";
            this.infosModifMarche.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numericMontantMarche
            // 
            this.numericMontantMarche.DecimalPlaces = 2;
            this.numericMontantMarche.Location = new System.Drawing.Point(417, 116);
            this.numericMontantMarche.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numericMontantMarche.Name = "numericMontantMarche";
            this.numericMontantMarche.Size = new System.Drawing.Size(100, 20);
            this.numericMontantMarche.TabIndex = 127;
            this.numericMontantMarche.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericMontantMarche.ThousandsSeparator = true;
            this.numericMontantMarche.ValueChanged += new System.EventHandler(this.ModificationMarche);
            this.numericMontantMarche.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numericMontantMarche_KeyPress);
            // 
            // dataGridViewHistoriqueEtatsMarche
            // 
            this.dataGridViewHistoriqueEtatsMarche.AllowUserToAddRows = false;
            this.dataGridViewHistoriqueEtatsMarche.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewHistoriqueEtatsMarche.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewHistoriqueEtatsMarche.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistoriqueEtatsMarche.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateEtatMarcheHistorique,
            this.EtatMarcheHistorique,
            this.AuteurEtatMarcheHistorique});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewHistoriqueEtatsMarche.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewHistoriqueEtatsMarche.Location = new System.Drawing.Point(10, 216);
            this.dataGridViewHistoriqueEtatsMarche.MultiSelect = false;
            this.dataGridViewHistoriqueEtatsMarche.Name = "dataGridViewHistoriqueEtatsMarche";
            this.dataGridViewHistoriqueEtatsMarche.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewHistoriqueEtatsMarche.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewHistoriqueEtatsMarche.RowHeadersVisible = false;
            this.dataGridViewHistoriqueEtatsMarche.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHistoriqueEtatsMarche.Size = new System.Drawing.Size(266, 240);
            this.dataGridViewHistoriqueEtatsMarche.TabIndex = 126;
            // 
            // DateEtatMarcheHistorique
            // 
            this.DateEtatMarcheHistorique.HeaderText = "Date";
            this.DateEtatMarcheHistorique.Name = "DateEtatMarcheHistorique";
            this.DateEtatMarcheHistorique.ReadOnly = true;
            this.DateEtatMarcheHistorique.Width = 140;
            // 
            // EtatMarcheHistorique
            // 
            this.EtatMarcheHistorique.HeaderText = "Etat";
            this.EtatMarcheHistorique.Name = "EtatMarcheHistorique";
            this.EtatMarcheHistorique.ReadOnly = true;
            this.EtatMarcheHistorique.Width = 35;
            // 
            // AuteurEtatMarcheHistorique
            // 
            this.AuteurEtatMarcheHistorique.HeaderText = "Auteur";
            this.AuteurEtatMarcheHistorique.Name = "AuteurEtatMarcheHistorique";
            this.AuteurEtatMarcheHistorique.ReadOnly = true;
            this.AuteurEtatMarcheHistorique.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AuteurEtatMarcheHistorique.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AuteurEtatMarcheHistorique.Width = 75;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(7, 200);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(135, 13);
            this.label16.TabIndex = 125;
            this.label16.Text = "Historique du marché :";
            // 
            // annulerMarche
            // 
            this.annulerMarche.Enabled = false;
            this.annulerMarche.Image = global::ATE55.Properties.Resources.Annuler;
            this.annulerMarche.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.annulerMarche.Location = new System.Drawing.Point(369, 200);
            this.annulerMarche.Name = "annulerMarche";
            this.annulerMarche.Size = new System.Drawing.Size(69, 23);
            this.annulerMarche.TabIndex = 124;
            this.annulerMarche.Text = "Annuler";
            this.annulerMarche.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.annulerMarche.UseVisualStyleBackColor = true;
            this.annulerMarche.Click += new System.EventHandler(this.annulerMarche_Click);
            // 
            // modifMarche
            // 
            this.modifMarche.Enabled = false;
            this.modifMarche.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.modifMarche.Image = global::ATE55.Properties.Resources.Valeur_Modif;
            this.modifMarche.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.modifMarche.Location = new System.Drawing.Point(444, 192);
            this.modifMarche.Name = "modifMarche";
            this.modifMarche.Size = new System.Drawing.Size(154, 39);
            this.modifMarche.TabIndex = 123;
            this.modifMarche.Text = "Enregistrer le marché";
            this.modifMarche.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.modifMarche.UseVisualStyleBackColor = true;
            this.modifMarche.Click += new System.EventHandler(this.modifProjet_Click);
            // 
            // textBoxRemarquesMarche
            // 
            this.textBoxRemarquesMarche.Location = new System.Drawing.Point(88, 146);
            this.textBoxRemarquesMarche.Multiline = true;
            this.textBoxRemarquesMarche.Name = "textBoxRemarquesMarche";
            this.textBoxRemarquesMarche.Size = new System.Drawing.Size(510, 40);
            this.textBoxRemarquesMarche.TabIndex = 14;
            this.textBoxRemarquesMarche.TextChanged += new System.EventHandler(this.ModificationMarche);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(7, 149);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Remarques :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(523, 119);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "€";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(328, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Montant (HT) :";
            // 
            // dateTimePickerSignatureMarche
            // 
            this.dateTimePickerSignatureMarche.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePickerSignatureMarche.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerSignatureMarche.Location = new System.Drawing.Point(129, 116);
            this.dateTimePickerSignatureMarche.MaxDate = new System.DateTime(2100, 12, 31, 0, 0, 0, 0);
            this.dateTimePickerSignatureMarche.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerSignatureMarche.Name = "dateTimePickerSignatureMarche";
            this.dateTimePickerSignatureMarche.Size = new System.Drawing.Size(116, 20);
            this.dateTimePickerSignatureMarche.TabIndex = 9;
            this.dateTimePickerSignatureMarche.ValueChanged += new System.EventHandler(this.ModificationMarche);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "Date de signature :";
            // 
            // comboBoxTypeMarche
            // 
            this.comboBoxTypeMarche.AlignText = false;
            this.comboBoxTypeMarche.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeMarche.FormattingEnabled = true;
            this.comboBoxTypeMarche.Location = new System.Drawing.Point(235, 26);
            this.comboBoxTypeMarche.Name = "comboBoxTypeMarche";
            this.comboBoxTypeMarche.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboBoxTypeMarche.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTypeMarche.TabIndex = 7;
            this.comboBoxTypeMarche.SelectedIndexChanged += new System.EventHandler(this.ModificationMarche);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(192, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Type :";
            // 
            // textBoxIntituleMarche
            // 
            this.textBoxIntituleMarche.Location = new System.Drawing.Point(58, 86);
            this.textBoxIntituleMarche.Name = "textBoxIntituleMarche";
            this.textBoxIntituleMarche.Size = new System.Drawing.Size(540, 20);
            this.textBoxIntituleMarche.TabIndex = 5;
            this.textBoxIntituleMarche.TextChanged += new System.EventHandler(this.ModificationMarche);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Intitulé :";
            // 
            // textBoxPrestataire
            // 
            this.textBoxPrestataire.Location = new System.Drawing.Point(76, 56);
            this.textBoxPrestataire.Name = "textBoxPrestataire";
            this.textBoxPrestataire.Size = new System.Drawing.Size(522, 20);
            this.textBoxPrestataire.TabIndex = 3;
            this.textBoxPrestataire.TextChanged += new System.EventHandler(this.ModificationMarche);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Prestataire :";
            // 
            // comboBoxEtatMarche
            // 
            this.comboBoxEtatMarche.AlignText = false;
            this.comboBoxEtatMarche.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEtatMarche.FormattingEnabled = true;
            this.comboBoxEtatMarche.ImageList = this.imageListProjets;
            this.comboBoxEtatMarche.Location = new System.Drawing.Point(46, 26);
            this.comboBoxEtatMarche.Name = "comboBoxEtatMarche";
            this.comboBoxEtatMarche.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(180)))), ((int)(((byte)(191)))));
            this.comboBoxEtatMarche.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEtatMarche.TabIndex = 1;
            this.comboBoxEtatMarche.SelectedIndexChanged += new System.EventHandler(this.ModificationMarche);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Etat :";
            // 
            // toolStripProjets
            // 
            this.toolStripProjets.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonFichier,
            this.toolStripButtonActualiser,
            this.toolStripLabel_Session});
            this.toolStripProjets.Location = new System.Drawing.Point(0, 0);
            this.toolStripProjets.Name = "toolStripProjets";
            this.toolStripProjets.Size = new System.Drawing.Size(1064, 25);
            this.toolStripProjets.TabIndex = 7;
            this.toolStripProjets.Text = "toolStrip1";
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
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textRecherche);
            this.splitContainer1.Panel1.Controls.Add(this.label17);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewProjets);
            this.splitContainer1.Panel1.Controls.Add(this.checkAfficherTousLesProjets);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabProjet);
            this.splitContainer1.Size = new System.Drawing.Size(1064, 692);
            this.splitContainer1.SplitterDistance = 438;
            this.splitContainer1.TabIndex = 8;
            // 
            // textRecherche
            // 
            this.textRecherche.Location = new System.Drawing.Point(147, 1);
            this.textRecherche.Name = "textRecherche";
            this.textRecherche.Size = new System.Drawing.Size(205, 20);
            this.textRecherche.TabIndex = 3;
            this.textRecherche.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textRecherche_KeyUp);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(63, 4);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(77, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "Recherche :";
            // 
            // checkAfficherTousLesProjets
            // 
            this.checkAfficherTousLesProjets.AutoSize = true;
            this.checkAfficherTousLesProjets.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAfficherTousLesProjets.Location = new System.Drawing.Point(132, 24);
            this.checkAfficherTousLesProjets.Name = "checkAfficherTousLesProjets";
            this.checkAfficherTousLesProjets.Size = new System.Drawing.Size(160, 17);
            this.checkAfficherTousLesProjets.TabIndex = 1;
            this.checkAfficherTousLesProjets.Text = "Afficher tous les projets";
            this.checkAfficherTousLesProjets.UseVisualStyleBackColor = true;
            this.checkAfficherTousLesProjets.CheckedChanged += new System.EventHandler(this.checkAfficherTousLesProjets_CheckedChanged);
            // 
            // frmProjets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 720);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripProjets);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1080, 758);
            this.Name = "frmProjets";
            this.Text = "FormProjets";
            this.Load += new System.EventHandler(this.frmProjets_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProjets)).EndInit();
            this.menuStripGridProjets.ResumeLayout(false);
            this.menuStripGridRowProjets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCollectivitesProjet)).EndInit();
            this.menuStripGridCollectivites.ResumeLayout(false);
            this.menuStripGridRowCollectivites.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMarches)).EndInit();
            this.menuStripGridMarches.ResumeLayout(false);
            this.menuStripGridRowMarches.ResumeLayout(false);
            this.tabProjet.ResumeLayout(false);
            this.tabPageProjet.ResumeLayout(false);
            this.tabPageProjet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAnneeDemarrage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantProjet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoriqueEtatProjet)).EndInit();
            this.tabPageMarche.ResumeLayout(false);
            this.groupBoxMarche.ResumeLayout(false);
            this.groupBoxMarche.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericMontantMarche)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistoriqueEtatsMarche)).EndInit();
            this.toolStripProjets.ResumeLayout(false);
            this.toolStripProjets.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewProjets;
        private System.Windows.Forms.Button sauvegarderProjetBouton;
        private System.Windows.Forms.Label labelMontantPrevisionnelProjet;
        private System.Windows.Forms.TextBox textRemarquesProjet;
        private System.Windows.Forms.Label labelRemarquesProjet;
        private System.Windows.Forms.Label labelEuros;
        private System.Windows.Forms.DataGridView dataGridViewMarches;
        private System.Windows.Forms.ContextMenuStrip menuStripGridProjets;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnProjetToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuStripGridMarches;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnNouveauMarcheToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuStripGridRowProjets;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnProjetRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerProjet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIntituleProjet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxCollectiviteRefProjet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button annulerProjet;
        private System.Windows.Forms.DataGridView dataGridViewCollectivitesProjet;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip menuStripGridRowMarches;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnNouveauMarcheRowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supprimerLeMarcheToolStripMenuItem;
        private CustomComboBox.CustomComboBox comboBoxTypeProjet;
        private CustomComboBox.CustomComboBox comboBoxEtatProjet;
        private System.Windows.Forms.TabControl tabProjet;
        private System.Windows.Forms.TabPage tabPageProjet;
        private System.Windows.Forms.TabPage tabPageMarche;
        private System.Windows.Forms.GroupBox groupBoxMarche;
        private System.Windows.Forms.TextBox textBoxRemarquesMarche;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dateTimePickerSignatureMarche;
        private System.Windows.Forms.Label label11;
        private CustomComboBox.CustomComboBox comboBoxTypeMarche;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxIntituleMarche;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPrestataire;
        private System.Windows.Forms.Label label8;
        private CustomComboBox.CustomComboBox comboBoxEtatMarche;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button modifMarche;
        private System.Windows.Forms.DataGridView dataGridViewHistoriqueEtatProjet;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button annulerMarche;
        private System.Windows.Forms.ImageList imageListProjets;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateEtatAvancementHistorique;
        private System.Windows.Forms.DataGridViewImageColumn EtatAvancementHistorique;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuteurHistorique;
        private System.Windows.Forms.ToolStrip toolStripProjets;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButtonFichier;
        private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Session;
        private System.Windows.Forms.ToolStripButton toolStripButtonActualiser;
        private System.Windows.Forms.Label labelNbCollectivitesImpactees;
        private System.Windows.Forms.DataGridView dataGridViewHistoriqueEtatsMarche;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateEtatMarcheHistorique;
        private System.Windows.Forms.DataGridViewImageColumn EtatMarcheHistorique;
        private System.Windows.Forms.DataGridViewTextBoxColumn AuteurEtatMarcheHistorique;
        private System.Windows.Forms.NumericUpDown numericMontantMarche;
        private System.Windows.Forms.NumericUpDown numericMontantProjet;
        private System.Windows.Forms.NumericUpDown numericAnneeDemarrage;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdProjet;
        private System.Windows.Forms.DataGridViewTextBoxColumn CollectiviteProjet;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntituleProjet;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeProjet;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnneeDemarrageProjet;
        private System.Windows.Forms.DataGridViewImageColumn EtatProjet;
        private System.Windows.Forms.DataGridViewTextBoxColumn NbMarches;
        private System.Windows.Forms.DataGridViewTextBoxColumn idMarche;
        private System.Windows.Forms.DataGridViewImageColumn EtatMarche;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomPrestataireMarche;
        private System.Windows.Forms.DataGridViewTextBoxColumn IntituleMarche;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeMarche;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateSignatureMarche;
        private System.Windows.Forms.DataGridViewTextBoxColumn MontantMarche;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label infosModifProjet;
        private System.Windows.Forms.Label infosModifMarche;
        private System.Windows.Forms.CheckBox checkAfficherTousLesProjets;
        private System.Windows.Forms.ContextMenuStrip menuStripGridCollectivites;
        private System.Windows.Forms.ToolStripMenuItem ajouterUneCollectivitéToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuStripGridRowCollectivites;
        private System.Windows.Forms.ToolStripMenuItem ajouterUneCollectivitéToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem supprimerLaCollectivitéToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodeCollectiviteImpactee;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomCollectivite;
        private System.Windows.Forms.DataGridViewTextBoxColumn PopulationDGFCollectivite;
        private System.Windows.Forms.TextBox textRecherche;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox checkSuiviSATE;
    }
}