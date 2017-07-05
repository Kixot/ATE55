namespace TexteMasqueSaisi
{
    partial class TexteMasqueSaisi
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

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errTexteMasqueSaisi = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errTexteMasqueSaisi)).BeginInit();
            this.SuspendLayout();
            // 
            // errTexteMasqueSaisi
            // 
            this.errTexteMasqueSaisi.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            ((System.ComponentModel.ISupportInitialize)(this.errTexteMasqueSaisi)).EndInit();
            this.ResumeLayout(false);

            // 
            // TexteMasqueSaisi
            // 
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Leave += new System.EventHandler(this.OnLeave);
        }

        #endregion

        private System.Windows.Forms.ErrorProvider errTexteMasqueSaisi;
    }
}
