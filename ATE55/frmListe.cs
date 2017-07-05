using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATE55 {
    public partial class frmListe : Form {

        List<object> liste;
        string nomObjet;
        public List<string> listeRetour;

        public frmListe(string titre, string nom, string idListe, string nomListe, List<object> l) {
            InitializeComponent();

            this.Text = titre;
            labelRecherche.Text = "Rechercher les " + nom + " : ";
            boutonAjouter.Text = "Ajouter les " + nom + " sélectionnées";
            dataGridViewListe.Columns[0].HeaderText = idListe;
            dataGridViewListe.Columns[1].HeaderText = nomListe;

            liste = l;
            nomObjet = nom;

        }

        private void frmListe_Load(object sender, EventArgs e) {
            this.AfficherListe();
        }

        private void AfficherListe() {

            // On parcourt la liste
            foreach (object o in liste) {

                // On effectue les actions nécessaires en fonction du type d'objet
                if (nomObjet.Equals("collectivités")) {

                    CCollectivite collectivite = (CCollectivite)o;

                    int i = dataGridViewListe.Rows.Add();
                    DataGridViewRow row = dataGridViewListe.Rows[i];

                    row.Cells["IdListe"].Value = collectivite.CodeCollectivite;
                    row.Cells["NomListe"].Value = collectivite.NomCollectivite;

                }

            }

        }

        private void textRecherche_KeyUp(object sender, KeyEventArgs e) {

            string Recherche = textRecherche.Text.ToLower();

            // On masque les lignes ne contenant pas le texte recherché (par id et nom)
            foreach (DataGridViewRow row in dataGridViewListe.Rows)
                row.Visible = row.Cells[0].Value.ToString().ToLower().Contains(Recherche) || row.Cells[1].Value.ToString().ToLower().Contains(Recherche);
        }

        private void boutonAjouter_Click(object sender, EventArgs e) {

            listeRetour = new List<string>();

            foreach (DataGridViewRow row in dataGridViewListe.Rows) {

                if (Convert.ToBoolean(row.Cells["checkListe"].Value))
                    listeRetour.Add(row.Cells[0].Value.ToString());

            }

            this.DialogResult = DialogResult.OK;
            this.Close();

        }


    }
}
