using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ATE55
{
    public partial class frmAttente : Form
    {
        public frmAttente()
        {
            InitializeComponent();
        }

        private void frmAttente_Load(object sender, EventArgs e)
        
        {
            Form frm = (Form)sender; // frm.Owner : adresse de la fen�tre parent
            // Si fen�tre parente existe
            if (frm.Owner !=null)
            {
                // Centrer automatiquement la fen�tre d'attente
                this.StartPosition = FormStartPosition.Manual;
                this.Left = frm.Owner.Left + (frm.Owner.Width - this.Width) / 2;
                this.Top = frm.Owner.Top + (frm.Owner.Height - this.Height) / 2;
            }
            else
                this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}