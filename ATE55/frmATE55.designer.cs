using System.Drawing;
namespace ATE55 {
    partial class frmATE55 {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmATE55));
            this.boutonSubventions = new System.Windows.Forms.Button();
            this.boutonConventions = new System.Windows.Forms.Button();
            this.boutonAssainissement = new System.Windows.Forms.Button();
            this.boutonProjets = new System.Windows.Forms.Button();
            this.boutonEauPotable = new System.Windows.Forms.Button();
            this.toolStripPrincipal = new System.Windows.Forms.ToolStrip();
            this.mnu_Session = new System.Windows.Forms.ToolStripSplitButton();
            this.mnu_Connexion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Deconnexion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Param = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_DroitUtilisateur = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_aPropos = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel_Session = new System.Windows.Forms.ToolStripLabel();
            this.mnuContextuelBaseData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tiersDoublonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_Tiers_Orphelin = new System.Windows.Forms.ToolStripMenuItem();
            this.boutonBilan = new System.Windows.Forms.Button();
            this.toolStripPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // boutonSubventions
            // 
            this.boutonSubventions.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boutonSubventions.Image = ((System.Drawing.Image)(resources.GetObject("boutonSubventions.Image")));
            this.boutonSubventions.Location = new System.Drawing.Point(372, 305);
            this.boutonSubventions.Name = "boutonSubventions";
            this.boutonSubventions.Size = new System.Drawing.Size(250, 250);
            this.boutonSubventions.TabIndex = 4;
            this.boutonSubventions.Text = "Subventions";
            this.boutonSubventions.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.boutonSubventions.UseVisualStyleBackColor = true;
            this.boutonSubventions.Click += new System.EventHandler(this.boutonSubventions_Click);
            // 
            // boutonConventions
            // 
            this.boutonConventions.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boutonConventions.Image = ((System.Drawing.Image)(resources.GetObject("boutonConventions.Image")));
            this.boutonConventions.Location = new System.Drawing.Point(672, 32);
            this.boutonConventions.Name = "boutonConventions";
            this.boutonConventions.Size = new System.Drawing.Size(250, 250);
            this.boutonConventions.TabIndex = 3;
            this.boutonConventions.Text = "Conventions";
            this.boutonConventions.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.boutonConventions.UseVisualStyleBackColor = true;
            this.boutonConventions.Click += new System.EventHandler(this.boutonConventions_Click);
            // 
            // boutonAssainissement
            // 
            this.boutonAssainissement.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boutonAssainissement.Image = ((System.Drawing.Image)(resources.GetObject("boutonAssainissement.Image")));
            this.boutonAssainissement.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.boutonAssainissement.Location = new System.Drawing.Point(372, 32);
            this.boutonAssainissement.Name = "boutonAssainissement";
            this.boutonAssainissement.Size = new System.Drawing.Size(250, 250);
            this.boutonAssainissement.TabIndex = 2;
            this.boutonAssainissement.Text = "Assainissement";
            this.boutonAssainissement.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.boutonAssainissement.UseVisualStyleBackColor = true;
            this.boutonAssainissement.Click += new System.EventHandler(this.boutonAssainissement_Click);
            // 
            // boutonProjets
            // 
            this.boutonProjets.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boutonProjets.Image = ((System.Drawing.Image)(resources.GetObject("boutonProjets.Image")));
            this.boutonProjets.Location = new System.Drawing.Point(72, 305);
            this.boutonProjets.Name = "boutonProjets";
            this.boutonProjets.Size = new System.Drawing.Size(250, 250);
            this.boutonProjets.TabIndex = 1;
            this.boutonProjets.Text = "Projets";
            this.boutonProjets.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.boutonProjets.UseVisualStyleBackColor = true;
            this.boutonProjets.Click += new System.EventHandler(this.boutonProjets_Click);
            // 
            // boutonEauPotable
            // 
            this.boutonEauPotable.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boutonEauPotable.Image = ((System.Drawing.Image)(resources.GetObject("boutonEauPotable.Image")));
            this.boutonEauPotable.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.boutonEauPotable.Location = new System.Drawing.Point(72, 32);
            this.boutonEauPotable.Name = "boutonEauPotable";
            this.boutonEauPotable.Size = new System.Drawing.Size(250, 250);
            this.boutonEauPotable.TabIndex = 0;
            this.boutonEauPotable.Text = "Eau Potable";
            this.boutonEauPotable.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.boutonEauPotable.UseVisualStyleBackColor = true;
            this.boutonEauPotable.Click += new System.EventHandler(this.boutonEauPotable_Click);
            // 
            // toolStripPrincipal
            // 
            this.toolStripPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Session,
            this.mnu_Param,
            this.mnu_aPropos,
            this.toolStripLabel_Session});
            this.toolStripPrincipal.Location = new System.Drawing.Point(0, 0);
            this.toolStripPrincipal.Name = "toolStripPrincipal";
            this.toolStripPrincipal.Size = new System.Drawing.Size(976, 25);
            this.toolStripPrincipal.TabIndex = 5;
            this.toolStripPrincipal.Text = "toolStrip1";
            // 
            // mnu_Session
            // 
            this.mnu_Session.AutoToolTip = false;
            this.mnu_Session.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_Connexion,
            this.mnu_Deconnexion});
            this.mnu_Session.Image = global::ATE55.Properties.Resources.connexion;
            this.mnu_Session.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_Session.Name = "mnu_Session";
            this.mnu_Session.Size = new System.Drawing.Size(78, 22);
            this.mnu_Session.Text = "Session";
            this.mnu_Session.Click += new System.EventHandler(this.mnu_Session_Click);
            // 
            // mnu_Connexion
            // 
            this.mnu_Connexion.Image = global::ATE55.Properties.Resources.connexion;
            this.mnu_Connexion.Name = "mnu_Connexion";
            this.mnu_Connexion.Size = new System.Drawing.Size(155, 22);
            this.mnu_Connexion.Text = "se Connecter";
            this.mnu_Connexion.Click += new System.EventHandler(this.mnu_Connexion_Click);
            this.mnu_Connexion.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mnu_Connexion_MouseDown);
            // 
            // mnu_Deconnexion
            // 
            this.mnu_Deconnexion.Enabled = false;
            this.mnu_Deconnexion.Image = global::ATE55.Properties.Resources.deconnexion;
            this.mnu_Deconnexion.Name = "mnu_Deconnexion";
            this.mnu_Deconnexion.Size = new System.Drawing.Size(155, 22);
            this.mnu_Deconnexion.Text = "se Déconnecter";
            this.mnu_Deconnexion.Click += new System.EventHandler(this.mnu_Deconnexion_Click);
            // 
            // mnu_Param
            // 
            this.mnu_Param.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.mnu_DroitUtilisateur});
            this.mnu_Param.Enabled = false;
            this.mnu_Param.Image = global::ATE55.Properties.Resources.parametre;
            this.mnu_Param.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_Param.Name = "mnu_Param";
            this.mnu_Param.Size = new System.Drawing.Size(98, 22);
            this.mnu_Param.Text = "Paramètres";
            this.mnu_Param.Click += new System.EventHandler(this.mnu_Param_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // mnu_DroitUtilisateur
            // 
            this.mnu_DroitUtilisateur.Image = global::ATE55.Properties.Resources.Utilisateurs_Cle_16;
            this.mnu_DroitUtilisateur.Name = "mnu_DroitUtilisateur";
            this.mnu_DroitUtilisateur.ShowShortcutKeys = false;
            this.mnu_DroitUtilisateur.Size = new System.Drawing.Size(189, 22);
            this.mnu_DroitUtilisateur.Text = "Gestion des Utilisateurs";
            this.mnu_DroitUtilisateur.Click += new System.EventHandler(this.mnu_DroitUtilisateur_Click);
            // 
            // mnu_aPropos
            // 
            this.mnu_aPropos.Image = global::ATE55.Properties.Resources.Information;
            this.mnu_aPropos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnu_aPropos.Name = "mnu_aPropos";
            this.mnu_aPropos.Size = new System.Drawing.Size(75, 22);
            this.mnu_aPropos.Text = "A Propos";
            this.mnu_aPropos.Click += new System.EventHandler(this.mnu_aPropos_Click);
            // 
            // toolStripLabel_Session
            // 
            this.toolStripLabel_Session.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel_Session.Name = "toolStripLabel_Session";
            this.toolStripLabel_Session.Size = new System.Drawing.Size(16, 22);
            this.toolStripLabel_Session.Text = "...";
            this.toolStripLabel_Session.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mnuContextuelBaseData
            // 
            this.mnuContextuelBaseData.Name = "mnuContextuelBaseData";
            this.mnuContextuelBaseData.Size = new System.Drawing.Size(61, 4);
            // 
            // tiersDoublonsToolStripMenuItem
            // 
            this.tiersDoublonsToolStripMenuItem.Name = "tiersDoublonsToolStripMenuItem";
            this.tiersDoublonsToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // mnu_Tiers_Orphelin
            // 
            this.mnu_Tiers_Orphelin.Name = "mnu_Tiers_Orphelin";
            this.mnu_Tiers_Orphelin.Size = new System.Drawing.Size(32, 19);
            // 
            // boutonBilan
            // 
            this.boutonBilan.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boutonBilan.Image = ((System.Drawing.Image)(resources.GetObject("boutonBilan.Image")));
            this.boutonBilan.Location = new System.Drawing.Point(672, 305);
            this.boutonBilan.Name = "boutonBilan";
            this.boutonBilan.Size = new System.Drawing.Size(250, 250);
            this.boutonBilan.TabIndex = 6;
            this.boutonBilan.Text = "Bilan";
            this.boutonBilan.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.boutonBilan.UseVisualStyleBackColor = true;
            this.boutonBilan.Click += new System.EventHandler(this.boutonBilan_Click);
            // 
            // frmATE55
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 590);
            this.Controls.Add(this.boutonBilan);
            this.Controls.Add(this.toolStripPrincipal);
            this.Controls.Add(this.boutonSubventions);
            this.Controls.Add(this.boutonConventions);
            this.Controls.Add(this.boutonAssainissement);
            this.Controls.Add(this.boutonProjets);
            this.Controls.Add(this.boutonEauPotable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmATE55";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmATE55_Load);
            this.Shown += new System.EventHandler(this.frm_Shown);
            this.toolStripPrincipal.ResumeLayout(false);
            this.toolStripPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button boutonEauPotable;
        private System.Windows.Forms.Button boutonProjets;
        private System.Windows.Forms.Button boutonAssainissement;
        private System.Windows.Forms.Button boutonConventions;
        private System.Windows.Forms.Button boutonSubventions;
        private System.Windows.Forms.ToolStrip toolStripPrincipal;
        private System.Windows.Forms.ToolStripSplitButton mnu_Session;
        private System.Windows.Forms.ToolStripMenuItem mnu_Connexion;
        private System.Windows.Forms.ToolStripMenuItem mnu_Deconnexion;
        private System.Windows.Forms.ToolStripSplitButton mnu_Param;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnu_DroitUtilisateur;
        private System.Windows.Forms.ToolStripMenuItem tiersDoublonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_Tiers_Orphelin;
        private System.Windows.Forms.ToolStripButton mnu_aPropos;
        private System.Windows.Forms.ToolStripLabel toolStripLabel_Session;
        private System.Windows.Forms.ContextMenuStrip mnuContextuelBaseData;
        private System.Windows.Forms.Button boutonBilan;

    }
}

