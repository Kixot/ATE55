using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ATE55
{
    public partial class frmUserAD : Form
    {
        CSession Session; // Session en cours
        public List<string> ListeCompteDejaSelect = null;
        public List<string> ListeCompteNouvSelect = null;

        public frmUserAD()
        {
            InitializeComponent();
        }

        private void frmUserAD_Load(object sender, EventArgs e)
        {
            Session = (CSession)this.Tag; // Réaffectation de l'objet Session
        }

        private void frmUserAD_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Refresh();
                // Se connecter à la base Active Directory et afficher la liste des Comptes User
                UserAD.ConnectAD(Session.Utilisateur.Utilisateur, Session.Utilisateur.PasswordUtilisateur);
                // Pré-sélectionner les comptes spécificiées dans la liste
                UserAD.SelectCompteAD(ListeCompteDejaSelect);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
            }
        }

        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            ListeCompteNouvSelect=UserAD.RetourneListeCompteNouvSelect();
        }


    }
}
