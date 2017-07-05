namespace ATE55
{
    partial class frmUtilisateur
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUtilisateur));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabListe = new System.Windows.Forms.TabControl();
            this.tabListePersonne = new System.Windows.Forms.TabPage();
            this.txtRechercheUtilisateur = new System.Windows.Forms.TextBox();
            this.btnExportUtilisateur = new System.Windows.Forms.Button();
            this.btnSupprimerUtilisateur = new System.Windows.Forms.Button();
            this.btnCreerUtilisateur = new System.Windows.Forms.Button();
            this.dgvUtilisateur = new System.Windows.Forms.DataGridView();
            this.Inactif = new System.Windows.Forms.DataGridViewImageColumn();
            this.Role_Lecteur = new System.Windows.Forms.DataGridViewImageColumn();
            this.Role_Redacteur = new System.Windows.Forms.DataGridViewImageColumn();
            this.Role_Gestionnaire = new System.Windows.Forms.DataGridViewImageColumn();
            this.Utilisateur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServiceAutorise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabFiche = new System.Windows.Forms.TabControl();
            this.tabFichePersonne = new System.Windows.Forms.TabPage();
            this.txtMatricule = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmailUtilisateur = new System.Windows.Forms.TextBox();
            this.lbDerniereConnexion = new System.Windows.Forms.Label();
            this.lbIdUtilisateur = new System.Windows.Forms.Label();
            this.cbRole_Lecteur = new System.Windows.Forms.CheckBox();
            this.ilNiveauAcces = new System.Windows.Forms.ImageList(this.components);
            this.txtUtilisateur = new System.Windows.Forms.TextBox();
            this.cbRole_Gestionnaire = new System.Windows.Forms.CheckBox();
            this.cbRole_Redacteur = new System.Windows.Forms.CheckBox();
            this.btnEnregistrerUtilisateur = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAnnulerUtilisateur = new System.Windows.Forms.Button();
            this.cbInactif = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrenomUtilisateur = new System.Windows.Forms.TextBox();
            this.txtNomUtilisateur = new System.Windows.Forms.TextBox();
            this.toolTipUtilisateur = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabListe.SuspendLayout();
            this.tabListePersonne.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtilisateur)).BeginInit();
            this.tabFiche.SuspendLayout();
            this.tabFichePersonne.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabListe);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabFiche);
            this.splitContainer1.Size = new System.Drawing.Size(800, 495);
            this.splitContainer1.SplitterDistance = 354;
            this.splitContainer1.TabIndex = 0;
            // 
            // tabListe
            // 
            this.tabListe.Controls.Add(this.tabListePersonne);
            this.tabListe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabListe.Location = new System.Drawing.Point(0, 0);
            this.tabListe.Name = "tabListe";
            this.tabListe.SelectedIndex = 0;
            this.tabListe.Size = new System.Drawing.Size(354, 495);
            this.tabListe.TabIndex = 0;
            // 
            // tabListePersonne
            // 
            this.tabListePersonne.BackColor = System.Drawing.SystemColors.Control;
            this.tabListePersonne.Controls.Add(this.txtRechercheUtilisateur);
            this.tabListePersonne.Controls.Add(this.btnExportUtilisateur);
            this.tabListePersonne.Controls.Add(this.btnSupprimerUtilisateur);
            this.tabListePersonne.Controls.Add(this.btnCreerUtilisateur);
            this.tabListePersonne.Controls.Add(this.dgvUtilisateur);
            this.tabListePersonne.Location = new System.Drawing.Point(4, 22);
            this.tabListePersonne.Name = "tabListePersonne";
            this.tabListePersonne.Padding = new System.Windows.Forms.Padding(3);
            this.tabListePersonne.Size = new System.Drawing.Size(346, 469);
            this.tabListePersonne.TabIndex = 0;
            this.tabListePersonne.Text = "Utilisateurs";
            // 
            // txtRechercheUtilisateur
            // 
            this.txtRechercheUtilisateur.Location = new System.Drawing.Point(175, 6);
            this.txtRechercheUtilisateur.Name = "txtRechercheUtilisateur";
            this.txtRechercheUtilisateur.Size = new System.Drawing.Size(74, 20);
            this.txtRechercheUtilisateur.TabIndex = 46;
            this.toolTipUtilisateur.SetToolTip(this.txtRechercheUtilisateur, "Utilisateur à rechercher");
            this.txtRechercheUtilisateur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRechercheUtilisateur_KeyPress);
            // 
            // btnExportUtilisateur
            // 
            this.btnExportUtilisateur.Image = ((System.Drawing.Image)(resources.GetObject("btnExportUtilisateur.Image")));
            this.btnExportUtilisateur.Location = new System.Drawing.Point(139, 4);
            this.btnExportUtilisateur.Name = "btnExportUtilisateur";
            this.btnExportUtilisateur.Size = new System.Drawing.Size(30, 26);
            this.btnExportUtilisateur.TabIndex = 44;
            this.toolTipUtilisateur.SetToolTip(this.btnExportUtilisateur, "Exportation de la liste des utilisateurs déclarés");
            this.btnExportUtilisateur.UseVisualStyleBackColor = true;
            this.btnExportUtilisateur.Click += new System.EventHandler(this.btnExportUtilisateur_Click);
            // 
            // btnSupprimerUtilisateur
            // 
            this.btnSupprimerUtilisateur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSupprimerUtilisateur.Enabled = false;
            this.btnSupprimerUtilisateur.Image = global::ATE55.Properties.Resources.Valeur_Suppr;
            this.btnSupprimerUtilisateur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSupprimerUtilisateur.Location = new System.Drawing.Point(269, 5);
            this.btnSupprimerUtilisateur.Name = "btnSupprimerUtilisateur";
            this.btnSupprimerUtilisateur.Size = new System.Drawing.Size(74, 23);
            this.btnSupprimerUtilisateur.TabIndex = 13;
            this.btnSupprimerUtilisateur.Text = "Supprimer";
            this.btnSupprimerUtilisateur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTipUtilisateur.SetToolTip(this.btnSupprimerUtilisateur, "Supprimer la fiche de cet utilisateur");
            this.btnSupprimerUtilisateur.UseVisualStyleBackColor = true;
            this.btnSupprimerUtilisateur.Click += new System.EventHandler(this.btnSupprimerPersonne_Click);
            // 
            // btnCreerUtilisateur
            // 
            this.btnCreerUtilisateur.Image = global::ATE55.Properties.Resources.Valeur_Nouv;
            this.btnCreerUtilisateur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreerUtilisateur.Location = new System.Drawing.Point(3, 5);
            this.btnCreerUtilisateur.Name = "btnCreerUtilisateur";
            this.btnCreerUtilisateur.Size = new System.Drawing.Size(130, 23);
            this.btnCreerUtilisateur.TabIndex = 6;
            this.btnCreerUtilisateur.Text = "Ajouter un utilisateur";
            this.btnCreerUtilisateur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTipUtilisateur.SetToolTip(this.btnCreerUtilisateur, "Ajouter d\'un nouvel Utilisateur");
            this.btnCreerUtilisateur.UseVisualStyleBackColor = true;
            this.btnCreerUtilisateur.Click += new System.EventHandler(this.btnCreerUtilisateur_Click);
            // 
            // dgvUtilisateur
            // 
            this.dgvUtilisateur.AllowUserToAddRows = false;
            this.dgvUtilisateur.AllowUserToDeleteRows = false;
            this.dgvUtilisateur.AllowUserToResizeRows = false;
            this.dgvUtilisateur.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUtilisateur.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUtilisateur.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUtilisateur.ColumnHeadersVisible = false;
            this.dgvUtilisateur.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Inactif,
            this.Role_Lecteur,
            this.Role_Redacteur,
            this.Role_Gestionnaire,
            this.Utilisateur,
            this.ServiceAutorise});
            this.dgvUtilisateur.Location = new System.Drawing.Point(3, 35);
            this.dgvUtilisateur.MultiSelect = false;
            this.dgvUtilisateur.Name = "dgvUtilisateur";
            this.dgvUtilisateur.ReadOnly = true;
            this.dgvUtilisateur.RowHeadersVisible = false;
            this.dgvUtilisateur.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvUtilisateur.RowTemplate.Height = 20;
            this.dgvUtilisateur.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUtilisateur.Size = new System.Drawing.Size(340, 431);
            this.dgvUtilisateur.TabIndex = 2;
            this.dgvUtilisateur.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvUtilisateur_RowStateChanged);
            // 
            // Inactif
            // 
            this.Inactif.Frozen = true;
            this.Inactif.HeaderText = "Inactif";
            this.Inactif.Image = global::ATE55.Properties.Resources.vide;
            this.Inactif.Name = "Inactif";
            this.Inactif.ReadOnly = true;
            this.Inactif.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Inactif.Width = 16;
            // 
            // Role_Lecteur
            // 
            this.Role_Lecteur.Frozen = true;
            this.Role_Lecteur.HeaderText = "Role_Lecteur";
            this.Role_Lecteur.Image = global::ATE55.Properties.Resources.vide;
            this.Role_Lecteur.Name = "Role_Lecteur";
            this.Role_Lecteur.ReadOnly = true;
            this.Role_Lecteur.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Role_Lecteur.Width = 16;
            // 
            // Role_Redacteur
            // 
            this.Role_Redacteur.Frozen = true;
            this.Role_Redacteur.HeaderText = "Role_Redacteur";
            this.Role_Redacteur.Image = global::ATE55.Properties.Resources.vide;
            this.Role_Redacteur.Name = "Role_Redacteur";
            this.Role_Redacteur.ReadOnly = true;
            this.Role_Redacteur.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Role_Redacteur.Width = 16;
            // 
            // Role_Gestionnaire
            // 
            this.Role_Gestionnaire.HeaderText = "Role_Gestionnaire";
            this.Role_Gestionnaire.Image = global::ATE55.Properties.Resources.vide;
            this.Role_Gestionnaire.Name = "Role_Gestionnaire";
            this.Role_Gestionnaire.ReadOnly = true;
            this.Role_Gestionnaire.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Role_Gestionnaire.Width = 16;
            // 
            // Utilisateur
            // 
            this.Utilisateur.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Utilisateur.HeaderText = "Nom, Prénom";
            this.Utilisateur.Name = "Utilisateur";
            this.Utilisateur.ReadOnly = true;
            // 
            // ServiceAutorise
            // 
            this.ServiceAutorise.HeaderText = "ServiceAutorise";
            this.ServiceAutorise.Name = "ServiceAutorise";
            this.ServiceAutorise.ReadOnly = true;
            this.ServiceAutorise.Width = 50;
            // 
            // tabFiche
            // 
            this.tabFiche.Controls.Add(this.tabFichePersonne);
            this.tabFiche.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFiche.Location = new System.Drawing.Point(0, 0);
            this.tabFiche.Name = "tabFiche";
            this.tabFiche.SelectedIndex = 0;
            this.tabFiche.Size = new System.Drawing.Size(442, 495);
            this.tabFiche.TabIndex = 0;
            // 
            // tabFichePersonne
            // 
            this.tabFichePersonne.BackColor = System.Drawing.SystemColors.Control;
            this.tabFichePersonne.Controls.Add(this.txtMatricule);
            this.tabFichePersonne.Controls.Add(this.label5);
            this.tabFichePersonne.Controls.Add(this.txtEmailUtilisateur);
            this.tabFichePersonne.Controls.Add(this.lbDerniereConnexion);
            this.tabFichePersonne.Controls.Add(this.lbIdUtilisateur);
            this.tabFichePersonne.Controls.Add(this.cbRole_Lecteur);
            this.tabFichePersonne.Controls.Add(this.txtUtilisateur);
            this.tabFichePersonne.Controls.Add(this.cbRole_Gestionnaire);
            this.tabFichePersonne.Controls.Add(this.cbRole_Redacteur);
            this.tabFichePersonne.Controls.Add(this.btnEnregistrerUtilisateur);
            this.tabFichePersonne.Controls.Add(this.label3);
            this.tabFichePersonne.Controls.Add(this.btnAnnulerUtilisateur);
            this.tabFichePersonne.Controls.Add(this.cbInactif);
            this.tabFichePersonne.Controls.Add(this.label2);
            this.tabFichePersonne.Controls.Add(this.label4);
            this.tabFichePersonne.Controls.Add(this.label1);
            this.tabFichePersonne.Controls.Add(this.txtPrenomUtilisateur);
            this.tabFichePersonne.Controls.Add(this.txtNomUtilisateur);
            this.tabFichePersonne.Location = new System.Drawing.Point(4, 22);
            this.tabFichePersonne.Name = "tabFichePersonne";
            this.tabFichePersonne.Padding = new System.Windows.Forms.Padding(3);
            this.tabFichePersonne.Size = new System.Drawing.Size(434, 469);
            this.tabFichePersonne.TabIndex = 0;
            this.tabFichePersonne.Text = "Fiche descriptive de l\'utilisateur";
            // 
            // txtMatricule
            // 
            this.txtMatricule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatricule.ForeColor = System.Drawing.Color.Blue;
            this.txtMatricule.Location = new System.Drawing.Point(332, 84);
            this.txtMatricule.MaxLength = 30;
            this.txtMatricule.Name = "txtMatricule";
            this.txtMatricule.Size = new System.Drawing.Size(73, 20);
            this.txtMatricule.TabIndex = 16;
            this.toolTipUtilisateur.SetToolTip(this.txtMatricule, "indispensable pour récupérer l\'affectation à l\'organigramme");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(270, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Matricule :";
            // 
            // txtEmailUtilisateur
            // 
            this.txtEmailUtilisateur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmailUtilisateur.Location = new System.Drawing.Point(50, 141);
            this.txtEmailUtilisateur.MaxLength = 30;
            this.txtEmailUtilisateur.Name = "txtEmailUtilisateur";
            this.txtEmailUtilisateur.Size = new System.Drawing.Size(218, 20);
            this.txtEmailUtilisateur.TabIndex = 3;
            this.txtEmailUtilisateur.TextChanged += new System.EventHandler(this.FicheUtilisateur_Modifier);
            // 
            // lbDerniereConnexion
            // 
            this.lbDerniereConnexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDerniereConnexion.ForeColor = System.Drawing.Color.Gray;
            this.lbDerniereConnexion.Location = new System.Drawing.Point(63, 3);
            this.lbDerniereConnexion.Name = "lbDerniereConnexion";
            this.lbDerniereConnexion.Size = new System.Drawing.Size(182, 32);
            this.lbDerniereConnexion.TabIndex = 14;
            this.lbDerniereConnexion.Text = "     ";
            // 
            // lbIdUtilisateur
            // 
            this.lbIdUtilisateur.AutoSize = true;
            this.lbIdUtilisateur.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIdUtilisateur.ForeColor = System.Drawing.Color.Gray;
            this.lbIdUtilisateur.Location = new System.Drawing.Point(9, 6);
            this.lbIdUtilisateur.Name = "lbIdUtilisateur";
            this.lbIdUtilisateur.Size = new System.Drawing.Size(33, 18);
            this.lbIdUtilisateur.TabIndex = 14;
            this.lbIdUtilisateur.Text = "     ";
            // 
            // cbRole_Lecteur
            // 
            this.cbRole_Lecteur.AutoSize = true;
            this.cbRole_Lecteur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRole_Lecteur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbRole_Lecteur.ImageKey = "Fonction_Gris";
            this.cbRole_Lecteur.ImageList = this.ilNiveauAcces;
            this.cbRole_Lecteur.Location = new System.Drawing.Point(31, 190);
            this.cbRole_Lecteur.Name = "cbRole_Lecteur";
            this.cbRole_Lecteur.Size = new System.Drawing.Size(77, 17);
            this.cbRole_Lecteur.TabIndex = 5;
            this.cbRole_Lecteur.Text = "      Lecteur";
            this.cbRole_Lecteur.UseVisualStyleBackColor = true;
            this.cbRole_Lecteur.CheckedChanged += new System.EventHandler(this.FicheUtilisateur_Modifier);
            // 
            // ilNiveauAcces
            // 
            this.ilNiveauAcces.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilNiveauAcces.ImageStream")));
            this.ilNiveauAcces.TransparentColor = System.Drawing.Color.Transparent;
            this.ilNiveauAcces.Images.SetKeyName(0, "INACTIF");
            this.ilNiveauAcces.Images.SetKeyName(1, "VIDE");
            this.ilNiveauAcces.Images.SetKeyName(2, "Fonction_Gris");
            this.ilNiveauAcces.Images.SetKeyName(3, "Fonction_Orange");
            this.ilNiveauAcces.Images.SetKeyName(4, "Fonction_Rouge");
            this.ilNiveauAcces.Images.SetKeyName(5, "mail");
            // 
            // txtUtilisateur
            // 
            this.txtUtilisateur.Enabled = false;
            this.txtUtilisateur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUtilisateur.Location = new System.Drawing.Point(132, 84);
            this.txtUtilisateur.MaxLength = 20;
            this.txtUtilisateur.Name = "txtUtilisateur";
            this.txtUtilisateur.Size = new System.Drawing.Size(125, 20);
            this.txtUtilisateur.TabIndex = 0;
            this.txtUtilisateur.TextChanged += new System.EventHandler(this.FicheUtilisateur_Modifier);
            // 
            // cbRole_Gestionnaire
            // 
            this.cbRole_Gestionnaire.AutoSize = true;
            this.cbRole_Gestionnaire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRole_Gestionnaire.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbRole_Gestionnaire.ImageKey = "Fonction_Rouge";
            this.cbRole_Gestionnaire.ImageList = this.ilNiveauAcces;
            this.cbRole_Gestionnaire.Location = new System.Drawing.Point(31, 264);
            this.cbRole_Gestionnaire.Name = "cbRole_Gestionnaire";
            this.cbRole_Gestionnaire.Size = new System.Drawing.Size(100, 17);
            this.cbRole_Gestionnaire.TabIndex = 7;
            this.cbRole_Gestionnaire.Text = "      Gestionnaire";
            this.cbRole_Gestionnaire.UseVisualStyleBackColor = true;
            this.cbRole_Gestionnaire.CheckedChanged += new System.EventHandler(this.FicheUtilisateur_Modifier);
            // 
            // cbRole_Redacteur
            // 
            this.cbRole_Redacteur.AutoSize = true;
            this.cbRole_Redacteur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbRole_Redacteur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbRole_Redacteur.ImageKey = "Fonction_Orange";
            this.cbRole_Redacteur.ImageList = this.ilNiveauAcces;
            this.cbRole_Redacteur.Location = new System.Drawing.Point(31, 224);
            this.cbRole_Redacteur.Name = "cbRole_Redacteur";
            this.cbRole_Redacteur.Size = new System.Drawing.Size(91, 17);
            this.cbRole_Redacteur.TabIndex = 6;
            this.cbRole_Redacteur.Text = "      Rédacteur";
            this.cbRole_Redacteur.UseVisualStyleBackColor = true;
            this.cbRole_Redacteur.CheckedChanged += new System.EventHandler(this.FicheUtilisateur_Modifier);
            // 
            // btnEnregistrerUtilisateur
            // 
            this.btnEnregistrerUtilisateur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnregistrerUtilisateur.Enabled = false;
            this.btnEnregistrerUtilisateur.Image = global::ATE55.Properties.Resources.saveHS;
            this.btnEnregistrerUtilisateur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEnregistrerUtilisateur.Location = new System.Drawing.Point(344, 6);
            this.btnEnregistrerUtilisateur.Name = "btnEnregistrerUtilisateur";
            this.btnEnregistrerUtilisateur.Size = new System.Drawing.Size(81, 23);
            this.btnEnregistrerUtilisateur.TabIndex = 8;
            this.btnEnregistrerUtilisateur.Text = "Enregistrer";
            this.btnEnregistrerUtilisateur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnregistrerUtilisateur.UseVisualStyleBackColor = true;
            this.btnEnregistrerUtilisateur.Click += new System.EventHandler(this.btnEnregistrerUtilisateur_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Compte de connexion :";
            // 
            // btnAnnulerUtilisateur
            // 
            this.btnAnnulerUtilisateur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnulerUtilisateur.Enabled = false;
            this.btnAnnulerUtilisateur.Image = global::ATE55.Properties.Resources.Annuler;
            this.btnAnnulerUtilisateur.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnnulerUtilisateur.Location = new System.Drawing.Point(345, 32);
            this.btnAnnulerUtilisateur.Name = "btnAnnulerUtilisateur";
            this.btnAnnulerUtilisateur.Size = new System.Drawing.Size(81, 23);
            this.btnAnnulerUtilisateur.TabIndex = 9;
            this.btnAnnulerUtilisateur.Text = "Annuler";
            this.btnAnnulerUtilisateur.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnnulerUtilisateur.UseVisualStyleBackColor = true;
            this.btnAnnulerUtilisateur.Click += new System.EventHandler(this.btnAnnulerUtilisateur_Click);
            // 
            // cbInactif
            // 
            this.cbInactif.AutoSize = true;
            this.cbInactif.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbInactif.Image = global::ATE55.Properties.Resources.Annuler;
            this.cbInactif.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbInactif.Location = new System.Drawing.Point(12, 38);
            this.cbInactif.Name = "cbInactif";
            this.cbInactif.Size = new System.Drawing.Size(126, 17);
            this.cbInactif.TabIndex = 0;
            this.cbInactif.TabStop = false;
            this.cbInactif.Text = "      Compte désactivé";
            this.cbInactif.UseVisualStyleBackColor = true;
            this.cbInactif.CheckedChanged += new System.EventHandler(this.FicheUtilisateur_Modifier);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Prénom :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "E-mail :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom :";
            // 
            // txtPrenomUtilisateur
            // 
            this.txtPrenomUtilisateur.Location = new System.Drawing.Point(251, 113);
            this.txtPrenomUtilisateur.MaxLength = 30;
            this.txtPrenomUtilisateur.Name = "txtPrenomUtilisateur";
            this.txtPrenomUtilisateur.Size = new System.Drawing.Size(139, 20);
            this.txtPrenomUtilisateur.TabIndex = 2;
            this.txtPrenomUtilisateur.TextChanged += new System.EventHandler(this.FicheUtilisateur_Modifier);
            // 
            // txtNomUtilisateur
            // 
            this.txtNomUtilisateur.Location = new System.Drawing.Point(50, 113);
            this.txtNomUtilisateur.MaxLength = 30;
            this.txtNomUtilisateur.Name = "txtNomUtilisateur";
            this.txtNomUtilisateur.Size = new System.Drawing.Size(141, 20);
            this.txtNomUtilisateur.TabIndex = 1;
            this.toolTipUtilisateur.SetToolTip(this.txtNomUtilisateur, "Nom complet de la personne (utilisé dans les éditions)");
            this.txtNomUtilisateur.TextChanged += new System.EventHandler(this.FicheUtilisateur_Modifier);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.Frozen = true;
            this.dataGridViewImageColumn1.HeaderText = "Inactif";
            this.dataGridViewImageColumn1.Image = global::ATE55.Properties.Resources.vide;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn1.Width = 20;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.Frozen = true;
            this.dataGridViewImageColumn2.HeaderText = "Accés";
            this.dataGridViewImageColumn2.Image = global::ATE55.Properties.Resources.vide;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.ReadOnly = true;
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn2.Width = 20;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.Frozen = true;
            this.dataGridViewImageColumn3.HeaderText = "Utilisateur";
            this.dataGridViewImageColumn3.Image = global::ATE55.Properties.Resources.vide;
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.ReadOnly = true;
            this.dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn3.Width = 20;
            // 
            // dataGridViewImageColumn4
            // 
            this.dataGridViewImageColumn4.HeaderText = "Signataire";
            this.dataGridViewImageColumn4.Image = global::ATE55.Properties.Resources.vide;
            this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            this.dataGridViewImageColumn4.ReadOnly = true;
            this.dataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn4.Width = 20;
            // 
            // dataGridViewImageColumn5
            // 
            this.dataGridViewImageColumn5.HeaderText = "AffaireSuivie";
            this.dataGridViewImageColumn5.Image = global::ATE55.Properties.Resources.vide;
            this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            this.dataGridViewImageColumn5.ReadOnly = true;
            this.dataGridViewImageColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn5.Width = 20;
            // 
            // dataGridViewImageColumn6
            // 
            this.dataGridViewImageColumn6.HeaderText = "Accés";
            this.dataGridViewImageColumn6.Image = global::ATE55.Properties.Resources.vide;
            this.dataGridViewImageColumn6.Name = "dataGridViewImageColumn6";
            this.dataGridViewImageColumn6.ReadOnly = true;
            this.dataGridViewImageColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewImageColumn6.Width = 16;
            // 
            // frmUtilisateur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 495);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUtilisateur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestion des Utilisateurs";
            this.Load += new System.EventHandler(this.frmPersonne_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tabListe.ResumeLayout(false);
            this.tabListePersonne.ResumeLayout(false);
            this.tabListePersonne.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUtilisateur)).EndInit();
            this.tabFiche.ResumeLayout(false);
            this.tabFichePersonne.ResumeLayout(false);
            this.tabFichePersonne.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabListe;
        private System.Windows.Forms.TabPage tabListePersonne;
        private System.Windows.Forms.TabControl tabFiche;
        private System.Windows.Forms.TabPage tabFichePersonne;
        private System.Windows.Forms.TextBox txtNomUtilisateur;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrenomUtilisateur;
        private System.Windows.Forms.ToolTip toolTipUtilisateur;
        private System.Windows.Forms.CheckBox cbRole_Lecteur;
        private System.Windows.Forms.TextBox txtUtilisateur;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbInactif;
        private System.Windows.Forms.DataGridView dgvUtilisateur;
        private System.Windows.Forms.Button btnEnregistrerUtilisateur;
        private System.Windows.Forms.Button btnAnnulerUtilisateur;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn6;
        private System.Windows.Forms.Button btnSupprimerUtilisateur;
        private System.Windows.Forms.Button btnCreerUtilisateur;
        private System.Windows.Forms.Label lbIdUtilisateur;
        private System.Windows.Forms.ImageList ilNiveauAcces;
        private System.Windows.Forms.Label lbDerniereConnexion;
        private System.Windows.Forms.CheckBox cbRole_Redacteur;
        private System.Windows.Forms.CheckBox cbRole_Gestionnaire;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmailUtilisateur;
        private System.Windows.Forms.Button btnExportUtilisateur;
        private System.Windows.Forms.TextBox txtRechercheUtilisateur;
        private System.Windows.Forms.DataGridViewImageColumn Inactif;
        private System.Windows.Forms.DataGridViewImageColumn Role_Lecteur;
        private System.Windows.Forms.DataGridViewImageColumn Role_Redacteur;
        private System.Windows.Forms.DataGridViewImageColumn Role_Gestionnaire;
        private System.Windows.Forms.DataGridViewTextBoxColumn Utilisateur;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServiceAutorise;
        private System.Windows.Forms.TextBox txtMatricule;
        private System.Windows.Forms.Label label5;
     
    }
}