using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATE55 {
    public partial class frmProjets : Form {

        CSession Session;
        SqlCommand command;
        SqlDataReader dataReader;
        int idProjetCourant = -1;
        int idMarcheCourant = -1;

        public frmProjets() {
            InitializeComponent();

            this.Text = "ATE55 - Projets";
        }

        private void frmProjets_Load(object sender, EventArgs args){

            Session = (CSession)this.Tag;

            // On alimente les comboBox
            frmATE55.AlimenterComboBox("TypeProjet", comboBoxTypeProjet, imageListProjets, Session, -1);
            frmATE55.AlimenterComboBox("EtatAvancementProjet", comboBoxEtatProjet, imageListProjets, Session, -1);
            frmATE55.AlimenterComboBox("TypeMarche", comboBoxTypeMarche, imageListProjets, Session, -1);
            frmATE55.AlimenterComboBox("EtatAvancementMarche", comboBoxEtatMarche, imageListProjets, Session, -1);

            // On renseigne la comboBox pour le choix de la collectivité de référence
            foreach (KeyValuePair<string, CCollectivite> Collectivite in frmATE55.Collectivites)
                comboBoxCollectiviteRefProjet.Items.Add(Collectivite.Value.NomCollectivite + " - " + Collectivite.Key);

            toolStripLabel_Session.Visible = true;
            toolStripLabel_Session.Text = Session.Utilisateur.Utilisateur
                + " [" + Session.Utilisateur.CodeDomaine + "/" + Session.Utilisateur.DroitAccess + "]"
                + ", accès précédent le " + Session.Utilisateur.DerniereConnexion.ToString();

            // On affiche les projets
            this.AfficherListeProjets(true);

        }

        private void AfficherListeProjets(bool PremierLancement = false) {

            DataGridView dgv = dataGridViewProjets;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                string req = "SELECT Count(idProjet) FROM Projet";
                command = new SqlCommand(req, Session.oConn);
                dgv.Rows.Add(Convert.ToInt32(command.ExecuteScalar()));

                req = "SELECT Projet.idProjet,idStatut_TypeProjet,NomCollectivite,IntituleProjet,idStatut_Etat,AnneeDemarrageProjet,Count(DISTINCT Marche.idProjet) AS NbMarches FROM Projet LEFT JOIN Collectivite_V ON Projet.CodeCollectivite = Collectivite_V.CodeCollectivite LEFT JOIN Projet_EtatAvancement ON Projet.idProjet = Projet_EtatAvancement.idProjet LEFT JOIN Marche ON Marche.idProjet = Projet.idProjet WHERE Projet_EtatAvancement.CreeLe = (SELECT MAX(CreeLe) FROM Projet_EtatAvancement WHERE idProjet = Projet.idProjet) GROUP BY Projet.idProjet,Projet.idStatut_TypeProjet,Collectivite_V.NomCollectivite,Projet.IntituleProjet,Projet_EtatAvancement.idStatut_Etat,Projet.AnneeDemarrageProjet ORDER BY AnneeDemarrageProjet DESC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                int i = 0;

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        row = dgv.Rows[i];
                        i++;

                        int idProjet = Convert.ToInt32(dataReader["idProjet"]);
                        int idStatut = Convert.ToInt32(dataReader["idStatut_Etat"]);

                        row.Cells["IdProjet"].Value = idProjet+"";
                        row.Cells["IntituleProjet"].Value = dataReader["IntituleProjet"].ToString();
                        row.Cells["AnneeDemarrageProjet"].Value = dataReader["AnneeDemarrageProjet"].ToString();

                        // Récupération de la collectivité du projet
                        row.Cells["CollectiviteProjet"].Value = dataReader["NomCollectivite"].ToString();

                        row.Cells["TypeProjet"].Value = frmATE55.Statuts[Convert.ToInt32(dataReader["idStatut_TypeProjet"])].LibelleStatut;

                        row.Cells["EtatProjet"].Value = imageListProjets.Images[frmATE55.Statuts[idStatut].IconeStatut];
                        // On met l'id de l'état en tag de l'image pour les filtres
                        ((Image)row.Cells["EtatProjet"].Value).Tag = idStatut;

                        // Si le projet est abandonné on affiche la ligne en gris
                        if (idStatut == (int)eStatut.Projet_Abandonne)
                            row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Gris"];

                        // Affichage du nombre de marchés correspondant au projet
                        row.Cells["NbMarches"].Value = dataReader["NbMarches"].ToString();

                        // Si le projet était sélectionné avant l'affichage, on le resélectionne
                        if (idProjetCourant == idProjet) {
                            dgv.ClearSelection();
                            row.Selected = true;
                            dgv.CurrentCell = row.Cells[1];
                        }

                        // Si le projet est abandonné ou terminé et que la case n'est pas cochée on masque
                        if ((idStatut == (int)eStatut.Projet_Abandonne || idStatut == (int)eStatut.Projet_Termine) && !checkAfficherTousLesProjets.Checked)
                            row.Visible = false;

                    }
                }
                dataReader.Close();

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
            }
            catch (Exception exc) {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
            }


            // Le paramètre permet d'optimiser le temps de lancement de l'application
            // Quand la recherche masque la première ligne affichée elle déclenche la sélection (rowStateChanged) de la suivante plusieurs fois
            // Ce qui l'affiche et ralentit le lancement
            if(!PremierLancement)
                this.Rechercher();
        }

        private void AfficherListeMarches() {

            dataGridViewMarches.Rows.Clear();
            dataGridViewMarches.Refresh();

            this.ViderControlsMarche();

            if (idProjetCourant != -1) {

                DataGridView dgv = dataGridViewMarches;

                string req = "SELECT idMarche, idStatut_TypeMarche, NomPrestataireMarche, IntituleMarche, DateSignatureMarche, MontantMarche, AssistanceSATE FROM Marche WHERE idProjet = " + idProjetCourant;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                List<CMarche> datasMarches = new List<CMarche>();
                // On stocke les marchés dans une liste
                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        CMarche marche = new CMarche();

                        marche.idMarche = Convert.ToInt32(dataReader["idMarche"]);
                        marche.idStatut_TypeMarche = Convert.ToInt32(dataReader["idStatut_TypeMarche"]);
                        marche.idProjet = idProjetCourant;
                        marche.NomPrestataireMarche = dataReader["NomPrestataireMarche"].ToString();
                        marche.IntituleMarche = dataReader["IntituleMarche"].ToString();
                        marche.DateSignatureMarche = dataReader["DateSignatureMarche"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateSignatureMarche"]) : new DateTime(0);
                        marche.MontantMarche = Decimal.Round(Convert.ToDecimal(dataReader["MontantMarche"]), 2);
                        marche.AssistanceSATE = Convert.ToInt32(dataReader["AssistanceSATE"]);

                        datasMarches.Add(marche);

                    }
                }
                dataReader.Close();

                // Parcourt de la liste
                foreach (CMarche marche in datasMarches) {

                    int i = dgv.Rows.Add();
                    DataGridViewRow row = dgv.Rows[i];

                    row.Cells["idMarche"].Value = marche.idMarche.ToString();
                    row.Cells["NomPrestataireMarche"].Value = marche.NomPrestataireMarche;
                    row.Cells["IntituleMarche"].Value = marche.IntituleMarche;
                    row.Cells["DateSignatureMarche"].Value = marche.DateSignatureMarche.ToShortDateString();
                    row.Cells["MontantMarche"].Value = marche.MontantMarche.ToString() + " €";

                    row.Cells["TypeMarche"].Value = frmATE55.Statuts[marche.idStatut_TypeMarche].LibelleStatut;

                    // Récupération de l'état du marché
                    req = "SELECT TOP 1 idStatut_Etat FROM Marche_EtatAvancement WHERE idMarche = " + marche.idMarche + " ORDER BY idMarcheEtatAvancement DESC";
                    command = new SqlCommand(req, Session.oConn);
                    int idStatut = Convert.ToInt32(command.ExecuteScalar());

                    row.Cells["EtatMarche"].Value = imageListProjets.Images[frmATE55.Statuts[idStatut].IconeStatut];

                    // Si le marché est suivi par le SATE on colore en bleu
                    if (marche.AssistanceSATE == 1)
                        row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Bleu"];

                    // Si le marché est abandonné on colore la ligne en gris
                    if (idStatut == (int)eStatut.Marche_Abandonne)
                        row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Gris"];

                }
            }
        }

        private void AfficherListeCollectivites(int idProjet) {

            dataGridViewCollectivitesProjet.Rows.Clear();
            dataGridViewCollectivitesProjet.Refresh();

            // Récupération et affichage des collectivités impactées
            string req = "SELECT CodeCollectivite FROM Projet_Collectivite WHERE idProjet = " + idProjet;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            List<string> CollectivitesImpactees = new List<string>();
            // On stocke les collectivités impactées dans une liste
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read())
                    CollectivitesImpactees.Add(dataReader["CodeCollectivite"].ToString());
            }
            dataReader.Close();

            // Affichage du nombre de collectivités
            labelNbCollectivitesImpactees.Text = "(" + CollectivitesImpactees.Count + ")";

            // Parcourt de la liste pour affichage et PopulationDGF
            foreach (string c in CollectivitesImpactees) {

                int i = dataGridViewCollectivitesProjet.Rows.Add();
                DataGridViewRow row = dataGridViewCollectivitesProjet.Rows[i];

                row.Cells["CodeCollectiviteImpactee"].Value = c;
                row.Cells["NomCollectivite"].Value = frmATE55.Collectivites[c].NomCollectivite;

                req = "SELECT PopulationDGF FROM Eligibilite WHERE CodeCollectivite = " + c + " ORDER BY AnneeEligibilite DESC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // Population
                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();
                    row.Cells["PopulationDGFCollectivite"].Value = dataReader["PopulationDGF"].ToString();
                    dataReader.Close();
                }
                else {
                    dataReader.Close();

                    // Si aucune population n'est retournée c'est un EPCI, il faut donc calculer la population manuellement
                    int population = 0;

                    List<string> CollectivitesLien = new List<string>();

                    req = "SELECT CodeCollectiviteFille FROM Collectivite_Lien_V WHERE CodeCollectiviteMere = " + c;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();
                    // On stocke les collectivités liées à l'EPCI dans une liste
                    if (dataReader != null && dataReader.HasRows) {
                        while (dataReader.Read())
                            CollectivitesLien.Add(dataReader["CodeCollectiviteFille"].ToString());
                    }
                    dataReader.Close();

                    // On parcourt les collectivités
                    foreach (string c2 in CollectivitesLien) {

                        req = "SELECT PopulationDGF FROM Eligibilite WHERE CodeCollectivite = " + c2 + " ORDER BY AnneeEligibilite DESC";
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();
                            // Calcul de la population
                            population += Convert.ToInt32(dataReader["PopulationDGF"]);
                        }
                        dataReader.Close();

                    }
                    // Affichage de la population
                    row.Cells["PopulationDGFCollectivite"].Value = population;

                }
            }

        }

        private void AfficherHistoriqueEtatProjet(int idProjet) {

            dataGridViewHistoriqueEtatProjet.Rows.Clear();
            dataGridViewHistoriqueEtatProjet.Refresh();

            DataGridView dgv = dataGridViewHistoriqueEtatProjet;

            // Récupération des états
            string req = "SELECT idStatut_Etat, CreeLe, CreePar FROM Projet_EtatAvancement WHERE idProjet = " + idProjet+ "ORDER BY CreeLe DESC";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            List<CProjet_EtatAvancement> etats = new List<CProjet_EtatAvancement>();
            // On stocke l'historique des états dans une liste
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    CProjet_EtatAvancement etat = new CProjet_EtatAvancement();

                    etat.CreeLe = Convert.ToDateTime(dataReader["CreeLe"]);
                    etat.idStatut_Etat = Convert.ToInt32(dataReader["idStatut_Etat"]);
                    etat.CreePar = dataReader["CreePar"].ToString();

                    etats.Add(etat);

                }
            }
            dataReader.Close();

            
            // Récupération du libellé des états et affichage
            foreach (CProjet_EtatAvancement etat in etats) {

                int i = dgv.Rows.Add();
                DataGridViewRow row = dgv.Rows[i];

                row.Cells["DateEtatAvancementHistorique"].Value = etat.CreeLe.ToString();
                row.Cells["AuteurHistorique"].Value = etat.CreePar;

                row.Cells["EtatAvancementHistorique"].Value = imageListProjets.Images[frmATE55.Statuts[etat.idStatut_Etat].IconeStatut];

            }

        }

        private void AfficherHistoriqueEtatMarche(int idMarche) {
            
            DataGridView dgv = dataGridViewHistoriqueEtatsMarche;

            dgv.Rows.Clear();
            dgv.Refresh();

            // Récupération des états
            string req = "SELECT idStatut_Etat, CreeLe, CreePar FROM Marche_EtatAvancement WHERE idMarche = " + idMarche + "ORDER BY CreeLe DESC";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            List<CMarche_EtatAvancement> etats = new List<CMarche_EtatAvancement>();
            // On stocke les états dans une liste
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    CMarche_EtatAvancement etat = new CMarche_EtatAvancement(Session);

                    etat.CreeLe = Convert.ToDateTime(dataReader["CreeLe"]);
                    etat.idStatut_Etat = Convert.ToInt32(dataReader["idStatut_Etat"]);
                    etat.CreePar = dataReader["CreePar"].ToString();

                    etats.Add(etat);

                }
            }
            dataReader.Close();


            // Récupération du libellé des états et affichage
            foreach (CMarche_EtatAvancement etat in etats) {

                int i = dgv.Rows.Add();
                DataGridViewRow row = dgv.Rows[i];

                row.Cells["DateEtatMarcheHistorique"].Value = etat.CreeLe.ToString();
                row.Cells["AuteurEtatMarcheHistorique"].Value = etat.CreePar;

                req = "SELECT IconeStatut FROM Statut WHERE idStatut = " + etat.idStatut_Etat;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                row.Cells["EtatMarcheHistorique"].Value = imageListProjets.Images[frmATE55.Statuts[etat.idStatut_Etat].IconeStatut];

                dataReader.Close();
            }

        }

        private void AfficherProjetSelectionne(int idProjet) {

            // On réinitialise le formulaire avant d'afficher les données du projet
            this.ViderControls();

            if (idProjet > 0) {

                idProjetCourant = idProjet;

                string req = "SELECT * FROM Projet WHERE idProjet = " + idProjet;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    // Affichage des données
                    textBoxIntituleProjet.Text = dataReader["IntituleProjet"].GetType().Name != "DBNull" ? dataReader["IntituleProjet"].ToString() : "";
                    numericAnneeDemarrage.Value = Convert.ToInt32(dataReader["AnneeDemarrageProjet"]);
                    textRemarquesProjet.Text = dataReader["RemarqueProjet"].GetType().Name != "DBNull" ? dataReader["RemarqueProjet"].ToString() : "";
                    numericMontantProjet.Value = Convert.ToDecimal(dataReader["MontantProjet"]);

                    string CodeCollectivite = dataReader["CodeCollectivite"].ToString();

                    int index = comboBoxCollectiviteRefProjet.Items.IndexOf(frmATE55.Collectivites[CodeCollectivite].NomCollectivite + " - " + CodeCollectivite);
                    comboBoxCollectiviteRefProjet.SelectedIndex = index;

                    comboBoxTypeProjet.Set_SelectedId(dataReader["idStatut_TypeProjet"].ToString());

                    // Affichage créé le/par modifié le/par
                    infosModifProjet.Text = "créé le " + dataReader["CreeLe"].ToString() + " par " + dataReader["CreePar"].ToString() + "\n" + "modifié le " + dataReader["ModifieLe"].ToString() + " par " + dataReader["ModifiePar"].ToString();

                    dataReader.Close();

                    // Récupération de l'état du projet
                    req = "SELECT TOP 1 idStatut_Etat FROM Projet_EtatAvancement WHERE idProjet = " + idProjet + " ORDER BY idProjetEtatAvancement DESC";
                    command = new SqlCommand(req, Session.oConn);

                    if (command.ExecuteScalar() != null)
                        comboBoxEtatProjet.Set_SelectedId(command.ExecuteScalar().ToString());

                    // On affiche la liste des marchés du projet et l'historique de ses états
                    AfficherListeMarches();
                    AfficherHistoriqueEtatProjet(idProjet);

                    AfficherListeCollectivites(idProjet);

                }
                else {
                    tabProjet.Visible = false;
                    dataReader.Close();
                }
            }
            else
                tabProjet.Visible = false;

            // On désactive l'onglet des marchés si c'est un nouveau projet et on force la sélection sur celui du projet
            tabPageMarche.Enabled = !(idProjetCourant == -1);
            if (idProjetCourant == -1)
                tabProjet.SelectedTab = tabPageProjet;

            // On désactive les boutons enregistrer et annuler
            sauvegarderProjetBouton.Enabled = annulerProjet.Enabled = false;
        }

        private void AfficherMarcheSelectionne(int idMarche) {

            // On réinitialise le formulaire des marchés
            this.ViderControlsMarche();

            if (idMarche > 0) {

                idMarcheCourant = idMarche;

                string req = "SELECT * FROM Marche WHERE idMarche = " + idMarche;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {

                    dataReader.Read();

                    CMarche marche = new CMarche();

                    marche.idMarche = Convert.ToInt32(dataReader["idMarche"]);
                    marche.idStatut_TypeMarche = Convert.ToInt32(dataReader["idStatut_TypeMarche"]);
                    marche.idProjet = idProjetCourant;
                    textBoxPrestataire.Text = dataReader["NomPrestataireMarche"].ToString();
                    textBoxIntituleMarche.Text = dataReader["IntituleMarche"].ToString();
                    dateTimePickerSignatureMarche.Value = dataReader["DateSignatureMarche"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateSignatureMarche"]) : new DateTime(0);
                    numericMontantMarche.Value = Convert.ToDecimal(dataReader["MontantMarche"]);
                    textBoxRemarquesMarche.Text = dataReader["RemarqueMarche"].ToString();
                    checkSuiviSATE.Checked = Convert.ToInt32(dataReader["AssistanceSATE"]) == 1;
                    marche.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : new DateTime(0);
                    marche.CreePar = dataReader["CreePar"].ToString();
                    marche.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : new DateTime(0);
                    marche.ModifiePar = dataReader["ModifiePar"].ToString();

                    dataReader.Close();

                    // Récupération du type
                    comboBoxTypeMarche.Set_SelectedId(marche.idStatut_TypeMarche.ToString());

                    // Récupération de l'état
                    req = "SELECT TOP 1 idStatut_Etat FROM Marche_EtatAvancement WHERE idMarche = " + marche.idMarche+ "ORDER BY idMarcheEtatAvancement DESC";
                    command = new SqlCommand(req, Session.oConn);

                    // Sélection de l'état du marché
                    if(command.ExecuteScalar() != null)
                        comboBoxEtatMarche.Set_SelectedId(command.ExecuteScalar().ToString());

                    AfficherHistoriqueEtatMarche(marche.idMarche);

                    // Créé le/par - Modifié le/par
                    infosModifMarche.Text = marche.InfosModif();

                    groupBoxMarche.Visible = true;
                }
                else {
                    dataReader.Close();
                    idMarcheCourant = -1;
                }

            }
            else
                idMarcheCourant = -1;

            
            modifMarche.Enabled = annulerMarche.Enabled = false;

        }

        private void UpdateRowProjet(int index, int idProjet) {

            DataGridViewRow row = dataGridViewProjets.Rows[index];
            CProjet projet = new CProjet();

            // Récupération du projet
            string req = "SELECT idStatut_TypeProjet, CodeCollectivite, IntituleProjet, AnneeDemarrageProjet FROM Projet WHERE idProjet = " + idProjet;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();

                projet.idProjet = idProjet;
                projet.idStatut_TypeProjet = Convert.ToInt32(dataReader["idStatut_TypeProjet"]);
                projet.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                projet.IntituleProjet = dataReader["IntituleProjet"].ToString();
                projet.AnneeDemarrageProjet = Convert.ToInt32(dataReader["AnneeDemarrageProjet"]);
            }
            else {
                // Si le projet n'existe pas on s'arrête ici
                dataReader.Close();
                return;
            }

            dataReader.Close();

            row.Cells["IdProjet"].Value = projet.idProjet;
            row.Cells["IntituleProjet"].Value = projet.IntituleProjet;
            row.Cells["AnneeDemarrageProjet"].Value = projet.AnneeDemarrageProjet;

            // Si la collectivité est celle par défaut (non renseigné) on affiche la ligne en rouge
            if (projet.CodeCollectivite.Equals("55000"))
                row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Rouge"];
            else
                row.DefaultCellStyle.BackColor = Color.White;
            // Récupération de la collectivité du projet
            row.Cells["CollectiviteProjet"].Value = frmATE55.Collectivites[projet.CodeCollectivite].NomCollectivite;

            row.Cells["TypeProjet"].Value = frmATE55.Statuts[projet.idStatut_TypeProjet].LibelleStatut;

            // Récupération de l'état du projet
            req = "SELECT TOP 1 idStatut_Etat FROM Projet_EtatAvancement WHERE idProjet = " + projet.idProjet + " ORDER BY idProjetEtatAvancement DESC";
            command = new SqlCommand(req, Session.oConn);
            int idStatut = Convert.ToInt32(command.ExecuteScalar());

            row.Cells["EtatProjet"].Value = imageListProjets.Images[frmATE55.Statuts[idStatut].IconeStatut];

            // Si le projet est abandonné on l'affiche en gris
            if (idStatut == (int)eStatut.Projet_Abandonne)
                row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Gris"];

            // Affichage du nombre de marchés correspondant au projet
            req = "SELECT Count(*) FROM Marche WHERE idProjet = " + projet.idProjet;
            command = new SqlCommand(req, Session.oConn);
            row.Cells["NbMarches"].Value = command.ExecuteScalar();

            // Si le projet était déjà sélectionné précédemment on le resélectionne
            if (idProjetCourant == projet.idProjet) {
                // On sélectionne la ligne et on force le scrolling jusqu'à cette ligne
                dataGridViewProjets.ClearSelection();
                row.Selected = true;
                dataGridViewProjets.FirstDisplayedScrollingRowIndex = dataGridViewProjets.SelectedRows[0].Index;
            }

        }

        private void UpdateRowMarche(int index, int idMarche) {

            DataGridViewRow row = dataGridViewMarches.Rows[index];

            string req = "SELECT idMarche, idStatut_TypeMarche, NomPrestataireMarche, IntituleMarche, DateSignatureMarche, MontantMarche, AssistanceSATE FROM Marche WHERE idMarche = " + idMarche;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();

                CMarche marche = new CMarche();

                marche.idMarche = Convert.ToInt32(dataReader["idMarche"]);
                marche.idStatut_TypeMarche = Convert.ToInt32(dataReader["idStatut_TypeMarche"]);
                marche.idProjet = idProjetCourant;
                marche.NomPrestataireMarche = dataReader["NomPrestataireMarche"].ToString();
                marche.IntituleMarche = dataReader["IntituleMarche"].ToString();
                marche.DateSignatureMarche = dataReader["DateSignatureMarche"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateSignatureMarche"]) : new DateTime(0);
                marche.MontantMarche = Decimal.Round(Convert.ToDecimal(dataReader["MontantMarche"]), 2);
                marche.AssistanceSATE = Convert.ToInt32(dataReader["AssistanceSATE"]);

                dataReader.Close();

                // Affichage
                row.Cells["idMarche"].Value = marche.idMarche.ToString();
                row.Cells["NomPrestataireMarche"].Value = marche.NomPrestataireMarche;
                row.Cells["IntituleMarche"].Value = marche.IntituleMarche;
                row.Cells["DateSignatureMarche"].Value = marche.DateSignatureMarche.ToShortDateString();
                row.Cells["MontantMarche"].Value = marche.MontantMarche.ToString() + " €";

                // Récupération et affichage du type de marché
                req = "SELECT LibelleStatut FROM Statut WHERE idStatut = " + marche.idStatut_TypeMarche;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();
                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();
                    row.Cells["TypeMarche"].Value = dataReader["LibelleStatut"].ToString();
                }
                dataReader.Close();

                // Récupération de l'état du marché
                req = "SELECT TOP 1 idStatut_Etat FROM Marche_EtatAvancement WHERE idMarche = " + marche.idMarche + " ORDER BY idMarcheEtatAvancement DESC";
                command = new SqlCommand(req, Session.oConn);
                int idStatut = Convert.ToInt32(command.ExecuteScalar());

                // Icone de l'état
                req = "SELECT IconeStatut FROM Statut WHERE idStatut = " + idStatut;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();
                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();
                    row.Cells["EtatMarche"].Value = imageListProjets.Images[dataReader["IconeStatut"].ToString()];
                }
                dataReader.Close();

                // Si le marché est abandonné on colore la ligne en gris
                if (idStatut == (int)eStatut.Marche_Abandonne)
                    row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Gris"];
                else if (marche.AssistanceSATE == 1)
                    // Si le marché est suivi par le SATE on colore en bleu
                    row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Bleu"];
                else
                    row.DefaultCellStyle.BackColor = Color.White;

                // On sélectionne la ligne
                row.Selected = true;

            }
            else
                dataReader.Close();

        }

        private void Rechercher() {

            // On récupère la recherche et on la met en minuscule
            string Recherche = textRecherche.Text.ToLower();

            // Parcourt du datagridview
            foreach (DataGridViewRow row in dataGridViewProjets.Rows) {

                // On recherche sur la collectivité, l'intitulé, le type et l'année
                string NomCollectivite = row.Cells["CollectiviteProjet"].Value.ToString().ToLower();
                string Intitule = row.Cells["IntituleProjet"].Value.ToString().ToLower();
                string Type = row.Cells["TypeProjet"].Value.ToString().ToLower();
                string Annee = row.Cells["AnneeDemarrageProjet"].Value.ToString();

                // On affiche la ligne si elle contient la recherche
                row.Visible = NomCollectivite.Contains(Recherche) || Intitule.Contains(Recherche) || Type.Contains(Recherche) || Annee.Contains(Recherche);

                // Si la case n'est pas cochée on masque les projets terminés ou abandonnés
                int idEtat = Convert.ToInt32(((Image)row.Cells["EtatProjet"].Value).Tag);
                if (!checkAfficherTousLesProjets.Checked && (idEtat == (int)eStatut.Projet_Abandonne || idEtat == (int)eStatut.Projet_Termine))
                    row.Visible = false;
            }

        }

        private void ViderControls() {
            // On vide les contrôles du projet pour les remettre aux valeurs par défaut
            comboBoxCollectiviteRefProjet.SelectedIndex = -1;
            textBoxIntituleProjet.Clear();
            numericAnneeDemarrage.Value = 0;
            comboBoxTypeProjet.SelectedIndex = -1;
            comboBoxEtatProjet.SelectedIndex = -1;
            numericMontantProjet.Value = 0;
            textRemarquesProjet.Clear();
            dataGridViewHistoriqueEtatProjet.Rows.Clear();
            dataGridViewHistoriqueEtatProjet.Refresh();
            dataGridViewMarches.Rows.Clear();
            dataGridViewMarches.Refresh();
            dataGridViewCollectivitesProjet.Rows.Clear();
            dataGridViewCollectivitesProjet.Refresh();
            labelNbCollectivitesImpactees.Text = "(0)";
        }

        private void ViderControlsMarche() {
            // On réinitialise le formulaire des marchés
            comboBoxEtatMarche.SelectedIndex = -1;
            comboBoxTypeMarche.SelectedIndex = -1;
            textBoxPrestataire.Clear();
            textBoxIntituleMarche.Clear();
            numericMontantMarche.Value = 0;
            textBoxRemarquesMarche.Clear();
            dateTimePickerSignatureMarche.Value = dateTimePickerSignatureMarche.MinDate;
            modifMarche.Enabled = annulerMarche.Enabled = checkSuiviSATE.Checked = false;
            dataGridViewHistoriqueEtatsMarche.Rows.Clear();
            dataGridViewHistoriqueEtatsMarche.Refresh();
            groupBoxMarche.Visible = false;
        }

        private void Modification(object sender, EventArgs e) {
            sauvegarderProjetBouton.Enabled = annulerProjet.Enabled = true;
        }

        private void ModificationMarche(object sender, EventArgs e) {
            modifMarche.Enabled = annulerMarche.Enabled = true;
        }

        private void EnregistrerProjet() {

            // Si le bouton enregistrer n'est pas activé on arrête la fonction
            if (!sauvegarderProjetBouton.Enabled) return;


            CProjet projet = new CProjet(Session);

            projet.idProjet = idProjetCourant;
            projet.idStatut_TypeProjet = (!comboBoxTypeProjet.Get_SelectedId().Equals("")) ? Convert.ToInt32(comboBoxTypeProjet.Get_SelectedId()) : 0;

            if (comboBoxCollectiviteRefProjet.SelectedIndex != -1) {
                String[] s = comboBoxCollectiviteRefProjet.SelectedItem.ToString().Split(null);
                projet.CodeCollectivite = s[s.Length - 1];
            }
            else
                projet.CodeCollectivite = "55000"; 

            projet.IntituleProjet = (!textBoxIntituleProjet.Text.Equals("")) ? textBoxIntituleProjet.Text : "A renseigner";
            projet.AnneeDemarrageProjet = Convert.ToInt32(numericAnneeDemarrage.Value);
            projet.MontantProjet = numericMontantProjet.Value;
            projet.RemarqueProjet = textRemarquesProjet.Text;

            // Si c'est un projet existant on le met à jour sinon on le crée
            if (idProjetCourant != -1) {
                if (projet.Enregistrer()) {

                    // Ajout de l'état du projet
                    CProjet_EtatAvancement etat = new CProjet_EtatAvancement(Session);

                    etat.idProjet = idProjetCourant;
                    etat.idStatut_Etat = Convert.ToInt32(comboBoxEtatProjet.Get_SelectedId());

                    if (etat.Creer()) {
                        sauvegarderProjetBouton.Enabled = annulerProjet.Enabled = false;

                        // On cherche l'index du projet pour mettre à jour
                        int index = -1;
                        foreach (DataGridViewRow row in dataGridViewProjets.Rows) {
                            if (idProjetCourant == Convert.ToInt32(row.Cells["idProjet"].Value))
                                index = row.Index;
                        }
                        UpdateRowProjet(index, idProjetCourant);
                    }

                }
            }
            else {

                if (projet.Creer()) {

                    // Ajout de l'état du projet
                    CProjet_EtatAvancement etat = new CProjet_EtatAvancement(Session);

                    etat.idProjet = projet.idProjet;
                    etat.idStatut_Etat = 0;

                    idProjetCourant = projet.idProjet;
                    
                    if (etat.Creer()) {
                        sauvegarderProjetBouton.Enabled = annulerProjet.Enabled = false;
                        UpdateRowProjet(dataGridViewProjets.Rows.Add(), projet.idProjet);
                    }

                }

            }


        }

        private void EnregistreMarche() {

            CMarche marche = new CMarche(Session);

            marche.idMarche = idMarcheCourant;
            marche.idStatut_TypeMarche = (!comboBoxTypeMarche.Get_SelectedId().Equals("")) ? Convert.ToInt32(comboBoxTypeMarche.Get_SelectedId()) : 0;
            marche.idProjet = idProjetCourant;
            marche.NomPrestataireMarche = (!textBoxPrestataire.Text.Equals("")) ? textBoxPrestataire.Text : "A renseigner";
            marche.IntituleMarche = (!textBoxIntituleMarche.Text.Equals("")) ? textBoxIntituleMarche.Text : "A renseigner";
            marche.DateSignatureMarche = Convert.ToDateTime(dateTimePickerSignatureMarche.Value);
            marche.MontantMarche = numericMontantMarche.Value;
            marche.RemarqueMarche = textBoxRemarquesMarche.Text;
            marche.AssistanceSATE = checkSuiviSATE.Checked ? 1 : 0;

            // Si c'est un marché existant on le met à jour sinon on le crée
            if (idMarcheCourant != -1) {
                if (marche.Enregistrer()) {

                    // Ajout de l'état du marché
                    CMarche_EtatAvancement etat = new CMarche_EtatAvancement(Session);

                    etat.idMarche = idMarcheCourant;
                    etat.idStatut_Etat = Convert.ToInt32(comboBoxEtatMarche.Get_SelectedId());

                    modifMarche.Enabled = annulerMarche.Enabled = false;
                    if (etat.Creer()) {
                        UpdateRowMarche(dataGridViewMarches.SelectedRows[0].Index, marche.idMarche);
                        AfficherHistoriqueEtatMarche(marche.idMarche);
                    }
                  
                }
            }
            else {
                if (marche.Creer()) {

                    // Ajout de l'état du marché
                    CMarche_EtatAvancement etat = new CMarche_EtatAvancement(Session);

                    etat.idMarche = marche.idMarche;
                    etat.idStatut_Etat = 0;
                    
                    idMarcheCourant = marche.idMarche;

                    modifMarche.Enabled = annulerMarche.Enabled = false;
                    if (etat.Creer()) {
                        UpdateRowMarche(dataGridViewMarches.Rows.Add(), marche.idMarche);
                        AfficherMarcheSelectionne(idMarcheCourant);
                    }
                  
                }
            }

        }

        private void AjouterCollectivite() {

            // On récupère et on stocke les collectivités déjà liées au projet pour ne pas les afficher
            string req = "SELECT CodeCollectivite FROM Projet_Collectivite WHERE idProjet = "+idProjetCourant;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            List<string> CollectivitesImpactees = new List<string>();
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read())
                    CollectivitesImpactees.Add(dataReader["CodeCollectivite"].ToString());
            }
            dataReader.Close();

            // On stocke les collectivités dans une liste
            List<object> collectivites = new List<object>();

            // On parcourt les collectivités
            foreach (KeyValuePair<string, CCollectivite> c in frmATE55.Collectivites) {

                    // Si la collectivité est déjà dans la liste ou si c'est la collectivité de référence ou si c'est 55000 on ne l'ajoute pas
                    if(CollectivitesImpactees.IndexOf(c.Key) == -1 && !c.Key.Equals("55000"))
                        collectivites.Add((object)c.Value);
            }
            
            
            // On crée une instance du formulaire d'affichage de liste
            frmListe frm = new frmListe("Ajouter des collectivités au projet", "collectivités", "CodeCollectivite", "NomCollectivite", collectivites);
            var result = frm.ShowDialog();

            // On récupère les collectivités sélectionnées
            if (result == DialogResult.OK) {
                foreach (string CodeCollectivite in frm.listeRetour) {

                    // On teste si la collectivité est déjà liée au projet
                    req = "SELECT Count(*) FROM Projet_Collectivite WHERE idProjet = " + idProjetCourant + " AND CodeCollectivite = " + CodeCollectivite;
                    command = new SqlCommand(req, Session.oConn);

                    // Si aucun lien n'existe entre le projet et la collectivité on l'ajoute
                    if ((int)command.ExecuteScalar() == 0) {
                        CProjet_Collectivite projet_collectivite = new CProjet_Collectivite(Session);
                        projet_collectivite.idProjet = idProjetCourant;
                        projet_collectivite.CodeCollectivite = CodeCollectivite;
                        projet_collectivite.Creer();
                    }

                }

                AfficherListeCollectivites(idProjetCourant);
            }

        }

        private void ajouterUnProjetToolStripMenuItem_Click(object sender, EventArgs e) {

            sauvegarderProjetBouton.Enabled = true;
            idProjetCourant = -1;
            this.ViderControls();
            EnregistrerProjet();

        }

        private void ajouterUnNouveauMarcheToolStripMenuItem_Click(object sender, EventArgs e) {

            idMarcheCourant = -1;

            EnregistreMarche();
        }

        private void dataGridViewProjets_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {
            
            if (e.StateChanged == DataGridViewElementStates.Selected) {
                
                int idProjetSelect = Convert.ToInt32(e.Row.Cells["IdProjet"].Value);

                if (idProjetCourant != idProjetSelect) {
                    if (sauvegarderProjetBouton.Enabled) {
                        
                        // On récupère les informations du projet à enregistrer pour les afficher dans le MessageBox
                        string req = "SELECT IntituleProjet FROM Projet WHERE idProjet = " + idProjetCourant;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        string IntituleProjet = "";

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();
                            IntituleProjet = dataReader["IntituleProjet"].ToString();
                        }
                        dataReader.Close();

                        if (MessageBox.Show("Voulez-vous SAUVEGARDER les données du projet n°"+idProjetCourant+" ["+IntituleProjet+"] non enregistrées ?\n Attention, les données seront perdues !", "Sauvegarder le projet courant ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            EnregistrerProjet();
                    }

                    else 
                        tabProjet.Visible = true;
                    AfficherProjetSelectionne(idProjetSelect);
                }

                idMarcheCourant = -1;

            }
            

        }

        private void dataGridViewMarches_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {
            if (e.StateChanged == DataGridViewElementStates.Selected) {

                int idMarcheSelect = Convert.ToInt32(e.Row.Cells["idMarche"].Value);

                if (idMarcheCourant != idMarcheSelect) {
                    if (modifMarche.Enabled) {
                        if (MessageBox.Show("Voulez-vous SAUVEGARDER les données non enregistrées ?\n Attention, les données seront perdues !", "Sauvegarder le marché courant ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            EnregistreMarche();
                    }
                }
               
                AfficherMarcheSelectionne(idMarcheSelect);           

            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) {
            // On récupère et on affiche les données du projet
            string req = "SELECT IntituleProjet FROM Projet WHERE idProjet = " + idProjetCourant;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            string IntituleProjet = "";

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();
                IntituleProjet = dataReader["IntituleProjet"].ToString();
            }
            dataReader.Close();

            if (MessageBox.Show("Voulez-vous SUPPRIMER le Projet N°" + idProjetCourant + " ["+IntituleProjet+"] ?\n Attention, la suppression est irréversible !", "Suppression du Projet ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                int index = dataGridViewProjets.SelectedRows[0].Index;
                if(CProjet.Supprimer(Session, Convert.ToInt32(dataGridViewProjets.SelectedRows[0].Cells["idProjet"].Value)))
                    dataGridViewProjets.Rows.RemoveAt(index);
            }
        }

        private void dataGridViewProjets_MouseDown(object sender, MouseEventArgs e) {

            int pos = dataGridViewProjets.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewProjets.ClearSelection();
                dataGridViewProjets.Rows[pos].Selected = true;
            }
        }

        private void dataGridViewMarches_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewMarches.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewMarches.ClearSelection();
                dataGridViewMarches.Rows[pos].Selected = true;
            }
        }

        private void supprimerLeMarcheToolStripMenuItem_Click(object sender, EventArgs e) {

            int idMarche = Convert.ToInt32(dataGridViewMarches.SelectedRows[0].Cells["idMarche"].Value);

            // On affiche les infos du marché
            string req = "SELECT IntituleMarche FROM Marche WHERE idMarche = "+idMarche;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            string IntituleMarche = "";

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();
                IntituleMarche = dataReader["IntituleMarche"].ToString();
            }
            dataReader.Close();

            if (MessageBox.Show("Voulez-vous SUPPRIMER le Marché n°"+idMarche+" ["+IntituleMarche+"] ?\n Attention, la suppression est irréversible !", "Suppression du Marché ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                int index = dataGridViewMarches.SelectedRows[0].Index;
                if (CMarche.Supprimer(Session, idMarche)) {
                    dataGridViewMarches.Rows.RemoveAt(index);
                    groupBoxMarche.Visible = false;
                }
            }
        }

        private void sauvegarderProjetBouton_Click(object sender, EventArgs e) {
            this.EnregistrerProjet();
        }

        private void annulerProjet_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Voulez-vous ANNULER toutes les modifications apportées au Projet n° [" + idProjetCourant + "] ?", "Annulation modification ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                sauvegarderProjetBouton.Enabled = annulerProjet.Enabled = false;
                AfficherProjetSelectionne(idProjetCourant);
            }
        }

        private void modifProjet_Click(object sender, EventArgs e) {
            this.EnregistreMarche();
        }

        private void tabProjet_Selecting(object sender, TabControlCancelEventArgs e) {
            if(e.TabPage.Name == "tabPageMarche")
                e.Cancel = !e.TabPage.Enabled;
        }

        private void annulerMarche_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Voulez-vous ANNULER toutes les modifications apportées au Marche ?", "Annulation modification ...", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                modifMarche.Enabled = annulerMarche.Enabled = false;
                AfficherMarcheSelectionne(idMarcheCourant);
            }
        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void toolStripButtonActualiser_Click(object sender, EventArgs e) {
            AfficherListeProjets();
            AfficherProjetSelectionne(idProjetCourant);
        }

        private void numericMontantProjet_KeyPress(object sender, KeyPressEventArgs e) {
            this.Modification(sender, e);
        }

        private void numericMontantMarche_KeyPress(object sender, KeyPressEventArgs e) {
            this.ModificationMarche(sender, e);
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e) {
            this.Modification(sender, e);
        }

        private void checkAfficherTousLesProjets_CheckedChanged(object sender, EventArgs e) {
            this.Rechercher();
        }

        private void ajouterUneCollectivitéToolStripMenuItem_Click(object sender, EventArgs e) {
            AjouterCollectivite();
        }

        private void ajouterUneCollectivitéToolStripMenuItem1_Click(object sender, EventArgs e) {
            AjouterCollectivite();
        }

        private void dataGridViewCollectivitesProjet_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewCollectivitesProjet.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewCollectivitesProjet.ClearSelection();
                dataGridViewCollectivitesProjet.Rows[pos].Selected = true;
            }
        }

        private void supprimerLaCollectivitéToolStripMenuItem_Click(object sender, EventArgs e) {
            CProjet_Collectivite.Supprimer(Session, idProjetCourant, dataGridViewCollectivitesProjet.SelectedRows[0].Cells["CodeCollectiviteImpactee"].Value.ToString());
            AfficherProjetSelectionne(idProjetCourant);
        }

        private void textRecherche_KeyUp(object sender, KeyEventArgs e) {
            this.Rechercher();
        }

    }
}
