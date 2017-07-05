namespace ATE55
{
    partial class dlgAuthentification
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAuthentification));
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.lbUtilisateur = new System.Windows.Forms.Label();
            this.lbTitre = new System.Windows.Forms.Label();
            this.lbInfos = new System.Windows.Forms.Label();
            this.txtUtilisateur = new System.Windows.Forms.TextBox();
            this.listRole = new System.Windows.Forms.ListBox();
            this.lbRole = new System.Windows.Forms.Label();
            this.btnConnecter = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(90, 60);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(151, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(10, 65);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(71, 13);
            this.lbPassword.TabIndex = 5;
            this.lbPassword.Text = "Mot de passe";
            // 
            // lbUtilisateur
            // 
            this.lbUtilisateur.AutoSize = true;
            this.lbUtilisateur.Location = new System.Drawing.Point(25, 40);
            this.lbUtilisateur.Name = "lbUtilisateur";
            this.lbUtilisateur.Size = new System.Drawing.Size(53, 13);
            this.lbUtilisateur.TabIndex = 4;
            this.lbUtilisateur.Text = "Utilisateur";
            // 
            // lbTitre
            // 
            this.lbTitre.Location = new System.Drawing.Point(47, 9);
            this.lbTitre.Name = "lbTitre";
            this.lbTitre.Size = new System.Drawing.Size(118, 24);
            this.lbTitre.TabIndex = 6;
            this.lbTitre.Text = "Veuillez vous identifier :";
            // 
            // lbInfos
            // 
            this.lbInfos.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lbInfos.Location = new System.Drawing.Point(90, 85);
            this.lbInfos.Name = "lbInfos";
            this.lbInfos.Size = new System.Drawing.Size(235, 28);
            this.lbInfos.TabIndex = 7;
            // 
            // txtUtilisateur
            // 
            this.txtUtilisateur.Location = new System.Drawing.Point(90, 36);
            this.txtUtilisateur.Name = "txtUtilisateur";
            this.txtUtilisateur.Size = new System.Drawing.Size(192, 20);
            this.txtUtilisateur.TabIndex = 0;
            // 
            // listRole
            // 
            this.listRole.FormattingEnabled = true;
            this.listRole.Location = new System.Drawing.Point(90, 161);
            this.listRole.MultiColumn = true;
            this.listRole.Name = "listRole";
            this.listRole.Size = new System.Drawing.Size(230, 43);
            this.listRole.TabIndex = 9;
            this.listRole.Visible = false;
            this.listRole.DoubleClick += new System.EventHandler(this.listRole_DoubleClick);
            this.listRole.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listRole_KeyPress);
            // 
            // lbRole
            // 
            this.lbRole.AutoSize = true;
            this.lbRole.Location = new System.Drawing.Point(12, 161);
            this.lbRole.Name = "lbRole";
            this.lbRole.Size = new System.Drawing.Size(73, 13);
            this.lbRole.TabIndex = 5;
            this.lbRole.Text = "Choisir un rôle";
            this.lbRole.Visible = false;
            // 
            // btnConnecter
            // 
            this.btnConnecter.Image = global::ATE55.Properties.Resources.connexion;
            this.btnConnecter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConnecter.Location = new System.Drawing.Point(90, 210);
            this.btnConnecter.Name = "btnConnecter";
            this.btnConnecter.Size = new System.Drawing.Size(105, 30);
            this.btnConnecter.TabIndex = 3;
            this.btnConnecter.Text = "Se connecter";
            this.btnConnecter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConnecter.UseVisualStyleBackColor = true;
            this.btnConnecter.Click += new System.EventHandler(this.btnConnecter_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Image = global::ATE55.Properties.Resources.Annuler;
            this.btnAnnuler.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnnuler.Location = new System.Drawing.Point(245, 210);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(75, 30);
            this.btnAnnuler.TabIndex = 4;
            this.btnAnnuler.Text = "Annuler";
            this.btnAnnuler.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAnnuler.UseVisualStyleBackColor = true;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // label1
            // 
            this.label1.Image = global::ATE55.Properties.Resources.Utilisateurs_Cle_32;
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 38);
            this.label1.TabIndex = 6;
            // 
            // dlgAuthentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 242);
            this.Controls.Add(this.listRole);
            this.Controls.Add(this.txtUtilisateur);
            this.Controls.Add(this.lbInfos);
            this.Controls.Add(this.lbTitre);
            this.Controls.Add(this.lbRole);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.lbUtilisateur);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnConnecter);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAuthentification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Authentification Active Directory";
            this.Load += new System.EventHandler(this.dlgAuthentification_Load);
            this.Shown += new System.EventHandler(this.dlgAuthentification_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Button btnConnecter;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label lbUtilisateur;
        private System.Windows.Forms.Label lbTitre;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lbInfos;
        private System.Windows.Forms.TextBox txtUtilisateur;
        private System.Windows.Forms.ListBox listRole;
        private System.Windows.Forms.Label lbRole;
        private System.Windows.Forms.Label label1;

    }
}