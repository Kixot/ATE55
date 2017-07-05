namespace ATE55 {
    partial class frmListe {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.boutonAjouter = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelRecherche = new System.Windows.Forms.Label();
            this.textRecherche = new System.Windows.Forms.TextBox();
            this.dataGridViewListe = new System.Windows.Forms.DataGridView();
            this.IdListe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NomListe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckListe = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListe)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.boutonAjouter);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dataGridViewListe);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 619);
            this.panel1.TabIndex = 0;
            // 
            // boutonAjouter
            // 
            this.boutonAjouter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boutonAjouter.Image = global::ATE55.Properties.Resources.Plus;
            this.boutonAjouter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.boutonAjouter.Location = new System.Drawing.Point(58, 589);
            this.boutonAjouter.Name = "boutonAjouter";
            this.boutonAjouter.Size = new System.Drawing.Size(274, 23);
            this.boutonAjouter.TabIndex = 2;
            this.boutonAjouter.Text = "Ajouter les éléments sélectionnés";
            this.boutonAjouter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.boutonAjouter.UseVisualStyleBackColor = true;
            this.boutonAjouter.Click += new System.EventHandler(this.boutonAjouter_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.labelRecherche);
            this.panel2.Controls.Add(this.textRecherche);
            this.panel2.Location = new System.Drawing.Point(3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(382, 61);
            this.panel2.TabIndex = 1;
            // 
            // labelRecherche
            // 
            this.labelRecherche.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecherche.Location = new System.Drawing.Point(6, 6);
            this.labelRecherche.Name = "labelRecherche";
            this.labelRecherche.Size = new System.Drawing.Size(366, 23);
            this.labelRecherche.TabIndex = 1;
            this.labelRecherche.Text = "Rechercher un élément :";
            this.labelRecherche.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textRecherche
            // 
            this.textRecherche.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textRecherche.Location = new System.Drawing.Point(6, 32);
            this.textRecherche.Name = "textRecherche";
            this.textRecherche.Size = new System.Drawing.Size(366, 20);
            this.textRecherche.TabIndex = 0;
            this.textRecherche.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textRecherche_KeyUp);
            // 
            // dataGridViewListe
            // 
            this.dataGridViewListe.AllowUserToAddRows = false;
            this.dataGridViewListe.AllowUserToDeleteRows = false;
            this.dataGridViewListe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewListe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdListe,
            this.NomListe,
            this.CheckListe});
            this.dataGridViewListe.Location = new System.Drawing.Point(3, 71);
            this.dataGridViewListe.MultiSelect = false;
            this.dataGridViewListe.Name = "dataGridViewListe";
            this.dataGridViewListe.RowHeadersVisible = false;
            this.dataGridViewListe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewListe.Size = new System.Drawing.Size(382, 512);
            this.dataGridViewListe.TabIndex = 0;
            // 
            // IdListe
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IdListe.DefaultCellStyle = dataGridViewCellStyle1;
            this.IdListe.HeaderText = "Id";
            this.IdListe.Name = "IdListe";
            this.IdListe.ReadOnly = true;
            this.IdListe.Width = 90;
            // 
            // NomListe
            // 
            this.NomListe.HeaderText = "Nom";
            this.NomListe.Name = "NomListe";
            this.NomListe.ReadOnly = true;
            this.NomListe.Width = 220;
            // 
            // CheckListe
            // 
            this.CheckListe.HeaderText = "";
            this.CheckListe.Name = "CheckListe";
            this.CheckListe.Width = 50;
            // 
            // frmListe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 620);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(406, 658);
            this.MinimumSize = new System.Drawing.Size(406, 658);
            this.Name = "frmListe";
            this.Text = "frmListe";
            this.Load += new System.EventHandler(this.frmListe_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewListe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button boutonAjouter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textRecherche;
        private System.Windows.Forms.DataGridView dataGridViewListe;
        private System.Windows.Forms.Label labelRecherche;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdListe;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomListe;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckListe;

    }
}