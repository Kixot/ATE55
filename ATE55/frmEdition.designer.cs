﻿namespace ATE55
{
    partial class frmEdition
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
            this.reportDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            this.CRViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CRViewer
            // 
            this.CRViewer.ActiveViewIndex = -1;
            this.CRViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CRViewer.DisplayGroupTree = false;
            this.CRViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CRViewer.Location = new System.Drawing.Point(0, 0);
            this.CRViewer.Name = "CRViewer";
            this.CRViewer.SelectionFormula = "";
            this.CRViewer.Size = new System.Drawing.Size(668, 504);
            this.CRViewer.TabIndex = 1;
            this.CRViewer.ViewTimeSelectionFormula = "";
            // 
            // frmEdition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 504);
            this.Controls.Add(this.CRViewer);
            this.Name = "frmEdition";
            this.Text = "frmEdition";
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.CrystalReports.Engine.ReportDocument reportDoc;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer CRViewer;
    }
}