using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATE55 {
    public partial class frmSubventions : Form {

        CSession Session;
        Dictionary<string, CCollectivite> Collectivites;
        SqlCommand command;
        SqlDataReader dataReader;

        public frmSubventions() {
            InitializeComponent();
            this.Text = "ATE55 - Subventions";
        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void frmSubventions_Load(object sender, EventArgs e) {

            Session = (CSession)this.Tag;

            // On alimente le comboBox permettant le choix de la collectivité de référence
            Collectivites = frmATE55.Collectivites;
            foreach (KeyValuePair<string, CCollectivite> Collectivite in Collectivites)
                comboCollectiviteSubvention.Items.Add(Collectivite.Value.NomCollectivite + " - " + Collectivite.Key);

            toolStripLabel_Session.Visible = true;
            toolStripLabel_Session.Text = Session.Utilisateur.Utilisateur
                + " [" + Session.Utilisateur.CodeDomaine + "/" + Session.Utilisateur.DroitAccess + "]"
                + ", accès précédent le " + Session.Utilisateur.DerniereConnexion.ToString();


            // On remplit les combobox
            frmATE55.AlimenterComboBox("TypeSubvention", comboTypeDossier, null, Session, -1);
            frmATE55.AlimenterComboBox("EtatDossierSubvention", comboEtatSubvention, imageListSubventions, Session, -1);
            frmATE55.AlimenterComboBox("TypeSubvention", comboRechercheType, null, Session, -1);

            comboRechercheType.Set_SelectedId("0");

            // A décommenter pour importer les subventions de 2016 et 2017
            //this.ImporterSubventions();

            this.AfficherSubventions(-1, true);

        }

        private void AfficherSubventions(int idSubventionSelect, bool PremierLancement = false) {

            DataGridView dgv = dataGridViewSubventions;
            DataGridViewRow rowSelection = null;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                string req = "SELECT Count(idSubvention) FROM Subvention";
                command = new SqlCommand(req, Session.oConn);
                dgv.Rows.Add(Convert.ToInt32(command.ExecuteScalar()));

                req = "SELECT idSubvention,NomCollectivite,DateReceptionDemande,OperationSubvention,idStatut_EtatAvancement,idStatut_TypeDossier FROM Subvention LEFT JOIN Collectivite_V ON Subvention.CodeCollectivite = Collectivite_V.CodeCollectivite ORDER BY DateReceptionDemande DESC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                int i = 0;

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        int idSubvention = Convert.ToInt32(dataReader["idSubvention"]);
                        int idEtat = Convert.ToInt32(dataReader["idStatut_EtatAvancement"]);

                        row = dgv.Rows[i];
                        i++;

                        row.Cells["idSubvention"].Value = idSubvention;
                        row.Cells["CollectiviteSubvention"].Value = dataReader["NomCollectivite"].ToString();
                        row.Cells["DateReceptionDemandeSubvention"].Value = dataReader["DateReceptionDemande"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateReceptionDemande"]).ToShortDateString();
                        row.Cells["OperationSubvention"].Value = dataReader["OperationSubvention"].ToString();

                        row.Cells["TypeSubvention"].Value = frmATE55.Statuts[Convert.ToInt32(dataReader["idStatut_TypeDossier"])].LibelleStatut;

                        row.Cells["EtatSubvention"].Value = imageListSubventions.Images[frmATE55.Statuts[idEtat].IconeStatut];
                        // On ajoute l'id du statut comme tag à l'image pour masquer les subventions programmées dans la recherche
                        ((Image)row.Cells["EtatSubvention"].Value).Tag = idEtat;

                        if (idSubventionSelect == idSubvention)
                            rowSelection = row;

                        // Si la subvention est programée ou soldée et que la case n'est pas cochée on cache
                        if ((idEtat == (int)eStatut.Programme || idEtat == (int)eStatut.Solde) && !checkAfficherDossiersProgrammes.Checked)
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


            if (rowSelection != null){
                rowSelection.Selected = tabSubvention.Visible = true;
                AfficherSubventionSelectionnee(idSubventionSelect);
            }
            else
                tabSubvention.Visible = false;

            // Le paramètre permet d'optimiser le temps de lancement de l'application
            // Quand la recherche masque la première ligne affichée elle déclenche la sélection (rowStateChanged) de la suivante plusieurs fois
            // Ce qui l'affiche et ralentit le lancement
            if (!PremierLancement)
                this.Rechercher();

        }

        private void AfficherCollectivites(int idSubvention) {

            DataGridView dgv = dataGridViewCollectivitesSubvention;
            DataGridViewRow row;
            
            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                string req = "SELECT CodeCollectivite FROM Subvention_Collectivite WHERE idSubvention = " + idSubvention;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                List<string> CodesCollectivites = new List<string>();

                // On récupère toutes les collectivités liées à la subvention
                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read())
                        CodesCollectivites.Add(dataReader["CodeCollectivite"].ToString());
                }
                dataReader.Close();


                // On parcourt la liste des codes récupérés
                foreach (string CodeCollectivite in CodesCollectivites) {

                    int i = dgv.Rows.Add();
                    row = dgv.Rows[i];

                    row.Cells["CodeCollectiviteSubvention"].Value = CodeCollectivite;
                    row.Cells["NomCollectiviteSubvention"].Value = frmATE55.Collectivites[CodeCollectivite].NomCollectivite;


                    // Population
                    req = "SELECT PopulationDGF FROM Eligibilite WHERE CodeCollectivite = " + CodeCollectivite + " ORDER BY AnneeEligibilite DESC";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();
                        row.Cells["PopDGFSubvention"].Value = dataReader["PopulationDGF"].ToString();
                        dataReader.Close();
                    }
                    else {
                        dataReader.Close();

                        // Si aucune population n'est retournée c'est un EPCI, il faut donc calculer la population manuellement
                        int population = 0;

                        List<string> CollectivitesLien = new List<string>();

                        req = "SELECT CodeCollectiviteFille FROM Collectivite_Lien_V WHERE CodeCollectiviteMere = " + CodeCollectivite;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        // On stocke les collectivités rattachées à l'EPCI
                        if (dataReader != null && dataReader.HasRows) {
                            while (dataReader.Read())
                                CollectivitesLien.Add(dataReader["CodeCollectiviteFille"].ToString());
                        }
                        dataReader.Close();

                        // Parcourt de la liste des collectivités
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

                        row.Cells["PopDGFSubvention"].Value = population;
                    }

                }
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

        }

        private void AfficherSubventionSelectionnee(int idSubvention) {

            this.ViderControls();

            if (idSubvention > 0) {
                try {

                    string req = "SELECT * FROM Subvention WHERE idSubvention = " + idSubvention;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        CSubvention subvention = new CSubvention();
                        subvention.idSubvention = idSubvention;
                        subvention.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                        subvention.DateReceptionDemande = dataReader["DateReceptionDemande"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReceptionDemande"]) : DateTime.MinValue;
                        subvention.OperationSubvention = dataReader["OperationSubvention"].ToString();
                        subvention.idStatut_EtatAvancement = Convert.ToInt32(dataReader["idStatut_EtatAvancement"]);
                        subvention.idStatut_TypeDossier = Convert.ToInt32(dataReader["idStatut_TypeDossier"]);
                        subvention.DateAR_DossierComplet = dataReader["DateAR_DossierComplet"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateAR_DossierComplet"]) : DateTime.MinValue;
                        subvention.DateProgrammation = dataReader["DateProgrammation"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateProgrammation"]) : DateTime.MinValue;
                        subvention.DSHT_Dpt = Convert.ToDecimal(dataReader["DSHT_Dpt"]);
                        subvention.TauxDpt = Convert.ToDecimal(dataReader["TauxDpt"]);
                        subvention.SubventionDpt = Convert.ToDecimal(dataReader["SubventionDpt"]);
                        subvention.DSHT_GIP = Convert.ToDecimal(dataReader["DSHT_GIP"]);
                        subvention.TauxGIP = Convert.ToDecimal(dataReader["TauxGIP"]);
                        subvention.SubventionGIP = Convert.ToDecimal(dataReader["SubventionGIP"]);
                        subvention.DSHT_SUR = Convert.ToDecimal(dataReader["DSHT_SUR"]);
                        subvention.TauxSUR = Convert.ToDecimal(dataReader["TauxSUR"]);
                        subvention.SubventionSUR = Convert.ToDecimal(dataReader["SubventionSUR"]);
                        subvention.DSHT_AE = Convert.ToDecimal(dataReader["DSHT_AE"]);
                        subvention.TauxAE = Convert.ToDecimal(dataReader["TauxAE"]);
                        subvention.SubventionAE = Convert.ToDecimal(dataReader["SubventionAE"]);
                        subvention.PP_Subvention = Convert.ToInt32(dataReader["PP_Subvention"]);
                        subvention.CommentairesSubvention = dataReader["CommentairesSubvention"].ToString();
                        subvention.NbHeuresClausesSociales = Convert.ToInt32(dataReader["NbHeuresClausesSociales"]);
                        subvention.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : DateTime.MinValue;
                        subvention.CreePar = dataReader["CreePar"].ToString();
                        subvention.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : DateTime.MinValue;
                        subvention.ModifiePar = dataReader["ModifiePar"].ToString();

                        dataReader.Close();


                        // Affichage des données
                        int index = comboCollectiviteSubvention.Items.IndexOf(Collectivites[subvention.CodeCollectivite].NomCollectivite + " - " + subvention.CodeCollectivite);
                        comboCollectiviteSubvention.SelectedIndex = index;

                        dateReceptionDemande.Checked = subvention.DateReceptionDemande != DateTime.MinValue;
                        dateReceptionDemande.Value = dateReceptionDemande.Checked ? Convert.ToDateTime(subvention.DateReceptionDemande) : DateTime.Today;

                        dateARDossierComplet.Checked = subvention.DateAR_DossierComplet != DateTime.MinValue;
                        dateARDossierComplet.Value = dateARDossierComplet.Checked ? Convert.ToDateTime(subvention.DateAR_DossierComplet) : DateTime.Today;
                        dateProgrammation.Checked = subvention.DateProgrammation != DateTime.MinValue;
                        dateProgrammation.Value = dateProgrammation.Checked ? Convert.ToDateTime(subvention.DateProgrammation) : DateTime.Today;

                        textOperation.Text = subvention.OperationSubvention;
                        textCommentairesSubvention.Text = subvention.CommentairesSubvention;

                        comboEtatSubvention.Set_SelectedId(subvention.idStatut_EtatAvancement.ToString());
                        comboTypeDossier.Set_SelectedId(subvention.idStatut_TypeDossier.ToString());

                        numericDSHTDpt.Value = subvention.DSHT_Dpt;
                        numericTauxDpt.Value = subvention.TauxDpt;
                        numericSubventionDpt.Value = subvention.SubventionDpt;
                        numericDSHTGIP.Value = subvention.DSHT_GIP;
                        numericTauxGIP.Value = subvention.TauxGIP;
                        numericSubventionGIP.Value = subvention.SubventionGIP;
                        numericDSHTSUR.Value = subvention.DSHT_SUR;
                        numericTauxSUR.Value = subvention.TauxSUR;
                        numericSubventionSUR.Value = subvention.SubventionSUR;
                        numericDSHTAE.Value = subvention.DSHT_AE;
                        numericTauxAE.Value = subvention.TauxAE;
                        numericSubventionAE.Value = subvention.SubventionAE;

                        numericPP.Value = subvention.PP_Subvention;
                        numericNbHeures.Value = subvention.NbHeuresClausesSociales;

                        if (subvention.NbHeuresClausesSociales > 0)
                            checkClauses.Checked = true;

                        // créé le/par modifié le/par
                        infosModifSubvention.Text = subvention.InfosModif();


                        this.AfficherCollectivites(idSubvention);

                    }
                    else
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
            }

            enregistrerSubventionBouton.Enabled = annulerSubventionBouton.Enabled = false;

        }

        private void UpdateRowSubvention(int idSubvention, int index, bool nouvelle = false) {

            if (index != -1 && idSubvention != -1) {

                DataGridViewRow row = dataGridViewSubventions.Rows[index];

                try {

                    string req = "SELECT idSubvention,CodeCollectivite,DateReceptionDemande,OperationSubvention,idStatut_EtatAvancement,idStatut_TypeDossier FROM Subvention WHERE idSubvention = " + idSubvention;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();


                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        // On affiche les infos de la subvention
                        row.Cells["idSubvention"].Value = Convert.ToInt32(dataReader["idSubvention"]);
                        row.Cells["CollectiviteSubvention"].Value = Collectivites[dataReader["CodeCollectivite"].ToString()].NomCollectivite;
                        row.Cells["DateReceptionDemandeSubvention"].Value = Convert.ToDateTime(dataReader["DateReceptionDemande"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReceptionDemande"]) : DateTime.MinValue).ToShortDateString();
                        row.Cells["OperationSubvention"].Value = dataReader["OperationSubvention"].ToString();
                        row.Cells["TypeSubvention"].Value = frmATE55.Statuts[Convert.ToInt32(dataReader["idStatut_TypeDossier"])].LibelleStatut;

                        int idStatut_EtatAvancement = Convert.ToInt32(dataReader["idStatut_EtatAvancement"]);

                        // On colore la ligne si des informations importantes sont manquantes
                        if (dataReader["CodeCollectivite"].ToString().Equals("55000") || Convert.ToDateTime(row.Cells["DateReceptionDemandeSubvention"].Value) == DateTime.MinValue || dataReader["OperationSubvention"].ToString().Equals(""))
                            row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Rouge"];
                        else
                            row.DefaultCellStyle.BackColor = Color.White;

                        dataReader.Close();

                        row.Cells["EtatSubvention"].Value = imageListSubventions.Images[frmATE55.Statuts[idStatut_EtatAvancement].IconeStatut];

                    }
                    else
                        dataReader.Close();

                    // On sélectionne la ligne et on force le scrolling jusqu'à cette ligne
                    if (nouvelle) {
                        dataGridViewSubventions.ClearSelection();
                        row.Selected = true;
                        dataGridViewSubventions.FirstDisplayedScrollingRowIndex = dataGridViewSubventions.SelectedRows[0].Index;
                    }
                    this.AfficherSubventionSelectionnee(idSubvention);

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

            }

        }

        private void EnregistrerSubvention(int idSubvention) {

            CSubvention subvention = new CSubvention(Session);
            subvention.idSubvention = idSubvention;

            if (comboCollectiviteSubvention.SelectedIndex != -1) {
                String[] s = comboCollectiviteSubvention.SelectedItem.ToString().Split(null);
                subvention.CodeCollectivite = s[s.Length - 1];
            }
            else
                subvention.CodeCollectivite = "55000"; 

            subvention.idStatut_TypeDossier = (!comboTypeDossier.Get_SelectedId().Equals("")) ? Convert.ToInt32(comboTypeDossier.Get_SelectedId()) : 0;
            subvention.idStatut_EtatAvancement = (!comboEtatSubvention.Get_SelectedId().Equals("")) ? Convert.ToInt32(comboEtatSubvention.Get_SelectedId()) : 0;
            subvention.DateReceptionDemande = dateReceptionDemande.Checked ? Convert.ToDateTime(dateReceptionDemande.Value) : (DateTime?)null;
            subvention.OperationSubvention = textOperation.Text;
            subvention.DateAR_DossierComplet = dateARDossierComplet.Checked ? Convert.ToDateTime(dateARDossierComplet.Value) : (DateTime?)null;
            subvention.DateProgrammation = dateProgrammation.Checked ? Convert.ToDateTime(dateProgrammation.Value) : (DateTime?)null;
            subvention.DSHT_Dpt = numericDSHTDpt.Value;
            subvention.TauxDpt = numericTauxDpt.Value;
            subvention.SubventionDpt = numericSubventionDpt.Value;
            subvention.DSHT_GIP = numericDSHTGIP.Value;
            subvention.TauxGIP = numericTauxGIP.Value;
            subvention.SubventionGIP = numericSubventionGIP.Value;
            subvention.DSHT_SUR = numericDSHTSUR.Value;
            subvention.TauxSUR = numericTauxSUR.Value;
            subvention.SubventionSUR = numericSubventionSUR.Value;
            subvention.DSHT_AE = numericDSHTAE.Value;
            subvention.TauxAE = numericTauxAE.Value;
            subvention.SubventionAE = numericSubventionAE.Value;
            subvention.PP_Subvention = Convert.ToInt32(numericPP.Value);
            subvention.CommentairesSubvention = textCommentairesSubvention.Text;
            subvention.NbHeuresClausesSociales = checkClauses.Checked ? Convert.ToInt32(numericNbHeures.Value) : 0;

            if (idSubvention == -1) {
                if (subvention.Creer())
                    this.UpdateRowSubvention(subvention.idSubvention, dataGridViewSubventions.Rows.Add(), true);
            }
            else {
                if(subvention.Enregistrer()){
                    
                    // On récupère l'index de la subvention
                    int index = -1;
                    foreach(DataGridViewRow row in dataGridViewSubventions.Rows){
                        if(Convert.ToInt32(row.Cells["idSubvention"].Value) == idSubvention)
                            index = row.Index;
                    }

                    // On met la ligne à jour
                    this.UpdateRowSubvention(subvention.idSubvention, index);

                }
            }


        }

        private void Rechercher() {

            // On met la recherche en minuscule
            string Recherche = textRecherche.Text.ToLower();
            string TypeRecherche = !comboRechercheType.Get_SelectedId().Equals("0") ? comboRechercheType.SelectedItem.ToString().ToLower() : "";

            // On parcourt la datagridview pour tester si la ligne contient le texte recherché
            foreach (DataGridViewRow row in dataGridViewSubventions.Rows) {

                // On peut rechercher par nom de Collectivite par date ou par Operation
                string NomCollectivite = row.Cells["CollectiviteSubvention"].Value.ToString().ToLower();
                string Date = row.Cells["DateReceptionDemandeSubvention"].Value.ToString();
                string Operation = row.Cells["OperationSubvention"].Value.ToString().ToLower();
                string Type = row.Cells["TypeSubvention"].Value.ToString().ToLower();

                // On affiche la ligne si elle contient le texte recherché
                row.Visible = NomCollectivite.Contains(Recherche) || Operation.Contains(Recherche) || Date.Contains(Recherche) || Type.Contains(Recherche);

                if(comboRechercheType.SelectedIndex != -1)
                    row.Visible = row.Visible && Type.Contains(TypeRecherche);

                // On récupère l'id du statut contenu dans le tag
                int idStatut_EtatSubvention = Convert.ToInt32(((Image)(row.Cells["EtatSubvention"].Value)).Tag);
                // Si la subvention est programmée et que la case n'est pas cochée on masque
                if ((idStatut_EtatSubvention == (int)eStatut.Programme || idStatut_EtatSubvention == (int)eStatut.Solde) && !checkAfficherDossiersProgrammes.Checked)
                    row.Visible = false;

            }


        }

        private void ViderControls() {
            comboCollectiviteSubvention.SelectedIndex = -1;
            comboEtatSubvention.Set_SelectedId("0");
            comboTypeDossier.Set_SelectedId("0");

            dateProgrammation.MinDate = new DateTime(2000, 1, 1);

            dateReceptionDemande.Value = dateARDossierComplet.Value = dateProgrammation.Value = DateTime.Today;
            dateReceptionDemande.Checked = dateARDossierComplet.Checked = dateProgrammation.Checked = checkClauses.Checked = false;
            dateARDossierComplet.Visible = labelDateAR.Visible = labelDateProg.Visible = false;

            numericNbHeures.Value = numericDSHTDpt.Value = numericDSHTGIP.Value = numericDSHTAE.Value = numericDSHTSUR.Value = numericTauxAE.Value = numericTauxSUR.Value = numericTauxDpt.Value = numericTauxGIP.Value = numericSubventionDpt.Value = numericSubventionGIP.Value = numericSubventionAE.Value = numericSubventionSUR.Value = numericPP.Value = 0;

            textOperation.Text = textCommentairesSubvention.Text = infosModifSubvention.Text = "";

            enregistrerSubventionBouton.Enabled = annulerSubventionBouton.Enabled = false;
        }

        private void AjouterCollectivite() {

            int idSubvention = Convert.ToInt32(dataGridViewSubventions.SelectedRows[0].Cells["idSubvention"].Value);

            // On récupère et on stocke les collectivités déjà liées à la subvention pour ne pas les afficher
            string req = "SELECT CodeCollectivite FROM Subvention_Collectivite WHERE idSubvention = " + idSubvention;
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
                if (CollectivitesImpactees.IndexOf(c.Key) == -1 && !c.Key.Equals("55000"))
                    collectivites.Add((object)c.Value);
            }

            frmListe frm = new frmListe("Ajouter des collectivités à la subvention", "collectivités", "CodeCollectivite", "NomCollectivite", collectivites);
            var result = frm.ShowDialog();

            // On récupère les collectivités sélectionnées
            if (result == DialogResult.OK) {

                foreach (string CodeCollectivite in frm.listeRetour) {

                    // On teste si la collectivité est déjà liée à la convention
                    req = "SELECT Count(*) FROM Subvention_Collectivite WHERE idSubvention = " + idSubvention + " AND CodeCollectivite = " + CodeCollectivite;
                    command = new SqlCommand(req, Session.oConn);

                    if ((int)command.ExecuteScalar() == 0) {
                        CSubvention_Collectivite subvention_collectivite = new CSubvention_Collectivite(Session);
                        subvention_collectivite.idSubvention = idSubvention;
                        subvention_collectivite.CodeCollectivite = CodeCollectivite;
                        subvention_collectivite.Creer();
                    }

                }

                AfficherCollectivites(idSubvention);
            }

        }

        private decimal CalculerSubvention(decimal dsht, decimal taux) {
            return dsht * taux / 100;
        }

        private void Modification(object sender, EventArgs e) {
            enregistrerSubventionBouton.Enabled = annulerSubventionBouton.Enabled = true;
        }

        public static void GenererExtractionStations(CSession Session, int Annee, bool Toutes = false, bool Soldees = false) {

            int CouleurEnTetes = System.Drawing.ColorTranslator.ToOle(Color.FromArgb(0, 96, 142));
            
            Microsoft.Office.Interop.Excel.Application ApplicationXL;
            Microsoft.Office.Interop.Excel._Workbook ClasseurXL;
            Microsoft.Office.Interop.Excel._Worksheet FeuilXL;


            // Excel
            ApplicationXL = new Microsoft.Office.Interop.Excel.Application();
            ApplicationXL.Visible = false;

            ClasseurXL = (Microsoft.Office.Interop.Excel._Workbook)ApplicationXL.Workbooks.Add(System.Reflection.Missing.Value);
            FeuilXL = (Microsoft.Office.Interop.Excel._Worksheet)ClasseurXL.ActiveSheet;

            ((Microsoft.Office.Interop.Excel.Worksheet)ClasseurXL.Sheets[1]).Select();

            FeuilXL.Name = Annee + " - Subventions";

            // Police
            ApplicationXL.StandardFont = "Century Gothic";


            // Headers
            FeuilXL.Cells[1, 1] = "ID";
            FeuilXL.Cells[1, 2] = "Code INSEE";
            FeuilXL.Cells[1, 3] = "Collectivité";
            FeuilXL.Cells[1, 4] = "Type";
            FeuilXL.Cells[1, 5] = "Avancement";
            FeuilXL.Cells[1, 6] = "Réception demande";
            FeuilXL.Cells[1, 7] = "Opération";
            FeuilXL.Cells[1, 8] = "AR dossier complet";
            FeuilXL.Cells[1, 9] = "Programmation";
            FeuilXL.Cells[1, 10] = "Département";
            FeuilXL.Cells[2, 10] = "DSHT";
            FeuilXL.Cells[2, 11] = "Taux";
            FeuilXL.Cells[2, 12] = "Subvention";
            FeuilXL.Cells[1, 13] = "GIP";
            FeuilXL.Cells[2, 13] = "DSHT";
            FeuilXL.Cells[2, 14] = "Taux";
            FeuilXL.Cells[2, 15] = "Subvention";
            FeuilXL.Cells[1, 16] = "SUR";
            FeuilXL.Cells[2, 16] = "DSHT";
            FeuilXL.Cells[2, 17] = "Taux";
            FeuilXL.Cells[2, 18] = "Subvention";
            FeuilXL.Cells[1, 19] = "AE";
            FeuilXL.Cells[2, 19] = "DSHT";
            FeuilXL.Cells[2, 20] = "Taux";
            FeuilXL.Cells[2, 21] = "Subvention";
            FeuilXL.Cells[1, 22] = "AE";
            FeuilXL.Cells[1, 23] = "PP";
            FeuilXL.Cells[1, 24] = "Clauses sociales";
            FeuilXL.Cells[1, 25] = "Observations";

            // Fusion headers
            FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[2, 1]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 2], FeuilXL.Cells[2, 2]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 3], FeuilXL.Cells[2, 3]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 4], FeuilXL.Cells[2, 4]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 5], FeuilXL.Cells[2, 5]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 6], FeuilXL.Cells[2, 6]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 7], FeuilXL.Cells[2, 7]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 8], FeuilXL.Cells[2, 8]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 9], FeuilXL.Cells[2, 9]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 10], FeuilXL.Cells[1, 12]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 13], FeuilXL.Cells[1, 15]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 16], FeuilXL.Cells[1, 18]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 19], FeuilXL.Cells[1, 21]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 22], FeuilXL.Cells[2, 22]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 23], FeuilXL.Cells[2, 23]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 24], FeuilXL.Cells[2, 24]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 25], FeuilXL.Cells[2, 25]).Cells.Merge();

            // Couleurs et font
            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[2, 25]);
            RangeHeaders.Interior.Color = CouleurEnTetes;
            RangeHeaders.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.White);
            RangeHeaders.Font.Bold = true;
            RangeHeaders.Columns.ColumnWidth = 20;
            RangeHeaders.Rows.RowHeight = 30;

            // On récupère les subventions
            // La requête est générée en fonction des paramètres
            string req = "";
            if (!Toutes)
                req = "SELECT * FROM Subvention WHERE YEAR(DateReceptionDemande) = " + Annee;
            else if (Toutes && !Soldees)
                req = "SELECT * FROM Subvention WHERE idStatut_EtatAvancement != " + (int)eStatut.Solde;
            else if (Toutes && Soldees)
                req = "SELECT * FROM Subvention WHERE idStatut_EtatAvancement = " + (int)eStatut.Solde;

            SqlCommand command = new SqlCommand(req, Session.oConn);
            SqlDataReader dataReader = command.ExecuteReader();

            // On stocke dans une liste
            List<CSubvention> Subventions = new List<CSubvention>();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    CSubvention subvention = new CSubvention();
                    subvention.idSubvention = Convert.ToInt32(dataReader["idSubvention"]);
                    subvention.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                    subvention.DateReceptionDemande = dataReader["DateReceptionDemande"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReceptionDemande"]) : DateTime.MinValue;
                    subvention.OperationSubvention = dataReader["OperationSubvention"].ToString();
                    subvention.idStatut_EtatAvancement = Convert.ToInt32(dataReader["idStatut_EtatAvancement"]);
                    subvention.idStatut_TypeDossier = Convert.ToInt32(dataReader["idStatut_TypeDossier"]);
                    subvention.DateAR_DossierComplet = dataReader["DateAR_DossierComplet"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateAR_DossierComplet"]) : DateTime.MinValue;
                    subvention.DateProgrammation = dataReader["DateProgrammation"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateProgrammation"]) : DateTime.MinValue;
                    subvention.DSHT_Dpt = Convert.ToDecimal(dataReader["DSHT_Dpt"]);
                    subvention.TauxDpt = Convert.ToDecimal(dataReader["TauxDpt"]);
                    subvention.SubventionDpt = Convert.ToDecimal(dataReader["SubventionDpt"]);
                    subvention.DSHT_GIP = Convert.ToDecimal(dataReader["DSHT_GIP"]);
                    subvention.TauxGIP = Convert.ToDecimal(dataReader["TauxGIP"]);
                    subvention.SubventionGIP = Convert.ToDecimal(dataReader["SubventionGIP"]);
                    subvention.DSHT_SUR = Convert.ToDecimal(dataReader["DSHT_SUR"]);
                    subvention.TauxSUR = Convert.ToDecimal(dataReader["TauxSUR"]);
                    subvention.SubventionSUR = Convert.ToDecimal(dataReader["SubventionSUR"]);
                    subvention.DSHT_AE = Convert.ToDecimal(dataReader["DSHT_AE"]);
                    subvention.TauxAE = Convert.ToDecimal(dataReader["TauxAE"]);
                    subvention.SubventionAE = Convert.ToDecimal(dataReader["SubventionAE"]);
                    subvention.PP_Subvention = Convert.ToInt32(dataReader["PP_Subvention"]);
                    subvention.CommentairesSubvention = dataReader["CommentairesSubvention"].ToString();
                    subvention.NbHeuresClausesSociales = Convert.ToInt32(dataReader["NbHeuresClausesSociales"]);

                    Subventions.Add(subvention);

                }
            }
            dataReader.Close();


            int NumLigne = 3;
            // Parcourt des subventions
            foreach (CSubvention Subvention in Subventions) {

                // Affichage
                FeuilXL.Cells[NumLigne, 1] = Subvention.idSubvention;
                FeuilXL.Cells[NumLigne, 2] = Subvention.CodeCollectivite;
                FeuilXL.Cells[NumLigne, 4] = frmATE55.Statuts[Subvention.idStatut_TypeDossier].LibelleStatut;
                FeuilXL.Cells[NumLigne, 5] = frmATE55.Statuts[Subvention.idStatut_EtatAvancement].LibelleStatut;
                FeuilXL.Cells[NumLigne, 6] = Subvention.DateReceptionDemande == null ? "" : ((DateTime)Subvention.DateReceptionDemande).ToShortDateString();
                FeuilXL.Cells[NumLigne, 7] = Subvention.OperationSubvention;
                FeuilXL.Cells[NumLigne, 8] = Subvention.DateAR_DossierComplet == null ? "" : ((DateTime)Subvention.DateAR_DossierComplet).ToShortDateString();
                FeuilXL.Cells[NumLigne, 9] = Subvention.DateProgrammation == null ? "" : ((DateTime)Subvention.DateProgrammation).ToShortDateString();
                FeuilXL.Cells[NumLigne, 10] = Decimal.Round(Subvention.DSHT_Dpt, 2) + " €";
                FeuilXL.Cells[NumLigne, 11] = Subvention.TauxDpt + " %";
                FeuilXL.Cells[NumLigne, 12] = Decimal.Round(Subvention.SubventionDpt, 2) + " €";
                FeuilXL.Cells[NumLigne, 13] = Decimal.Round(Subvention.DSHT_GIP, 2) + " €";
                FeuilXL.Cells[NumLigne, 14] = Subvention.TauxGIP + " %";
                FeuilXL.Cells[NumLigne, 15] = Decimal.Round(Subvention.SubventionGIP, 2) + " €";
                FeuilXL.Cells[NumLigne, 16] = Decimal.Round(Subvention.DSHT_SUR, 2) + " €";
                FeuilXL.Cells[NumLigne, 17] = Subvention.TauxSUR + " %";
                FeuilXL.Cells[NumLigne, 18] = Decimal.Round(Subvention.SubventionSUR, 2) + " €";
                FeuilXL.Cells[NumLigne, 19] = Decimal.Round(Subvention.DSHT_AE, 2) + " €";
                FeuilXL.Cells[NumLigne, 20] = Subvention.TauxAE + " %";
                FeuilXL.Cells[NumLigne, 21] = Decimal.Round(Subvention.SubventionAE, 2) + " €";
                FeuilXL.Cells[NumLigne, 23] = Subvention.PP_Subvention;
                FeuilXL.Cells[NumLigne, 24] = Subvention.NbHeuresClausesSociales + "h";
                FeuilXL.Cells[NumLigne, 25] = Subvention.CommentairesSubvention;

                try {
                    FeuilXL.Cells[NumLigne, 3] = frmATE55.Collectivites[Subvention.CodeCollectivite].NomCollectivite;
                    FeuilXL.Cells[NumLigne, 22] = frmATE55.Collectivites[Subvention.CodeCollectivite].AgenceEau;
                }
                catch (KeyNotFoundException) { }

                NumLigne++;

            }

            // Largeurs
            Microsoft.Office.Interop.Excel.Range RangeIds = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 1]);
            RangeIds.Columns.ColumnWidth = 5;

            Microsoft.Office.Interop.Excel.Range RangeCodes = FeuilXL.get_Range(FeuilXL.Cells[1, 2], FeuilXL.Cells[NumLigne - 1, 2]);
            RangeCodes.Columns.ColumnWidth = 10;
            RangeCodes.Font.Bold = true;

            Microsoft.Office.Interop.Excel.Range RangeCollectivites = FeuilXL.get_Range(FeuilXL.Cells[1, 3], FeuilXL.Cells[NumLigne - 1, 3]);
            RangeCollectivites.Columns.ColumnWidth = 40;

            Microsoft.Office.Interop.Excel.Range RangeStatuts = FeuilXL.get_Range(FeuilXL.Cells[1, 4], FeuilXL.Cells[NumLigne - 1, 5]);
            RangeStatuts.Columns.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range RangeReception = FeuilXL.get_Range(FeuilXL.Cells[1, 6], FeuilXL.Cells[NumLigne - 1, 6]);
            RangeReception.Columns.ColumnWidth = 10;

            Microsoft.Office.Interop.Excel.Range RangeOperation = FeuilXL.get_Range(FeuilXL.Cells[1, 7], FeuilXL.Cells[NumLigne - 1, 7]);
            RangeOperation.Columns.ColumnWidth = 40;

            Microsoft.Office.Interop.Excel.Range RangeDates = FeuilXL.get_Range(FeuilXL.Cells[1, 8], FeuilXL.Cells[NumLigne - 1, 9]);
            RangeDates.Columns.ColumnWidth = 10;

            Microsoft.Office.Interop.Excel.Range RangeSubventions = FeuilXL.get_Range(FeuilXL.Cells[1, 10], FeuilXL.Cells[NumLigne - 1, 22]);
            RangeSubventions.Columns.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range RangePP = FeuilXL.get_Range(FeuilXL.Cells[1, 23], FeuilXL.Cells[NumLigne - 1, 24]);
            RangePP.Columns.ColumnWidth = 5;

            Microsoft.Office.Interop.Excel.Range RangeObservations = FeuilXL.get_Range(FeuilXL.Cells[1, 25], FeuilXL.Cells[NumLigne - 1, 25]);
            RangeObservations.Columns.ColumnWidth = 40;

            // Contours et alignements
            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 25]);
            ContoursCellules(Range.Borders);
            AlignementCellules(Range);
            Range.Rows.RowHeight = 25;

            ApplicationXL.Visible = true;


        }

        private static void AlignementCellules(Microsoft.Office.Interop.Excel.Range Range) {
            Range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
        }

        private static void ContoursCellules(Microsoft.Office.Interop.Excel.Borders borders) {
            foreach (Microsoft.Office.Interop.Excel.Border Border in borders)
                borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }


        // Décommenter dans frmSubventions_load pour importer les subventions
        private void ImporterSubventions() {
            this.ImporterSubventionsAProgrammer();
            this.ImporterSubventionsProgrammees();
        }

        private void ImporterSubventionsAProgrammer() {

            string path = Application.StartupPath + @"\Imports\Subventions_aProgrammer.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne
                reader.ReadLine();

                // Parcourt du fichier
                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    bool DossierComplet = values[3].ToLower().Equals("oui");
                    bool DossierPretProgrammation = values[5].ToLower().Equals("oui");

                    CSubvention subvention = new CSubvention(Session);
                    subvention.CodeCollectivite = values[0];
                    subvention.DateReceptionDemande = !values[2].Equals("") ? Convert.ToDateTime(values[2]) : (DateTime?)null;
                    subvention.OperationSubvention = values[4];
                    subvention.DateAR_DossierComplet = !values[6].Equals("") ? Convert.ToDateTime(values[6]) : (DateTime?)null;
                    subvention.DSHT_Dpt = !values[7].Equals("") ? Convert.ToDecimal(values[7]) : 0;
                    subvention.TauxDpt = !values[8].Equals("") ? Convert.ToDecimal(values[8]) : 0;
                    subvention.SubventionDpt = !values[9].Equals("") ? Convert.ToDecimal(values[9]) : 0;
                    subvention.DSHT_GIP = !values[10].Equals("") ? Convert.ToDecimal(values[10]) : 0;
                    subvention.TauxGIP = !values[11].Equals("") ? Convert.ToDecimal(values[11]) : 0;
                    subvention.SubventionGIP = !values[12].Equals("") ? Convert.ToDecimal(values[12]) : 0;
                    subvention.PP_Subvention = !values[13].Equals("") ? Convert.ToInt32(values[13]) : 0;
                    subvention.CommentairesSubvention = values[14];
                    subvention.idStatut_TypeDossier = Convert.ToInt32(values[15]);
                    subvention.DateProgrammation = null;
                    subvention.NbHeuresClausesSociales = 0;

                    // Etat de la subvention
                    if (DossierComplet && DossierPretProgrammation)
                        subvention.idStatut_EtatAvancement = (int)eStatut.Pret_Programme;
                    else if (DossierComplet)
                        subvention.idStatut_EtatAvancement = (int)eStatut.Complet;
                    else
                        subvention.idStatut_EtatAvancement = 0;

                    if (!subvention.Creer())
                        return;

                }

            }

        }

        private void ImporterSubventionsProgrammees() {

            string path = Application.StartupPath + @"\Imports\Subventions_Programmees.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne
                reader.ReadLine();

                // Parcourt du fichier
                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    CSubvention subvention = new CSubvention(Session);
                    subvention.CodeCollectivite = values[0];
                    subvention.DateReceptionDemande = !values[2].Equals("") ? Convert.ToDateTime(values[2]) : (DateTime?)null;
                    subvention.DateProgrammation = !values[3].Equals("") ? Convert.ToDateTime(values[3]) : (DateTime?)null;
                    subvention.OperationSubvention = values[4];
                    subvention.DateAR_DossierComplet = !values[6].Equals("") ? Convert.ToDateTime(values[6]) : (DateTime?)null;
                    subvention.DSHT_Dpt = !values[7].Equals("") ? Convert.ToDecimal(values[7]) : 0;
                    subvention.TauxDpt = !values[8].Equals("") ? Convert.ToDecimal(values[8]) : 0;
                    subvention.SubventionDpt = !values[9].Equals("") ? Convert.ToDecimal(values[9]) : 0;
                    subvention.DSHT_GIP = !values[10].Equals("") ? Convert.ToDecimal(values[10]) : 0;
                    subvention.TauxGIP = !values[11].Equals("") ? Convert.ToDecimal(values[11]) : 0;
                    subvention.SubventionGIP = !values[12].Equals("") ? Convert.ToDecimal(values[12]) : 0;
                    subvention.PP_Subvention = !values[13].Equals("") ? Convert.ToInt32(values[13]) : 0;
                    subvention.CommentairesSubvention = values[14];
                    subvention.idStatut_TypeDossier = Convert.ToInt32(values[15]);
                    subvention.NbHeuresClausesSociales = 0;
                    subvention.idStatut_EtatAvancement = (int)eStatut.Programme;

                    if (!subvention.Creer())
                        return;

                }

            }

        }

        private void toolStripButtonActualiser_Click(object sender, EventArgs e) {
            this.AfficherSubventions(-1);
        }

        private void dataGridViewSubventions_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {

            if (e.StateChanged == DataGridViewElementStates.Selected) {

                int idSubvention = Convert.ToInt32(e.Row.Cells["idSubvention"].Value);

                if (enregistrerSubventionBouton.Enabled) {
                    if (MessageBox.Show("Voulez-vous SAUVEGARDER les données de la Subvention non enregistrées ?\n Attention, les données seront perdues !", "Sauvegarder la Subvention courante ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        this.EnregistrerSubvention(idSubvention);

                }

                tabSubvention.Visible = true;
                this.AfficherSubventionSelectionnee(idSubvention);

            }
        }

        private void dataGridViewSubventions_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewSubventions.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewSubventions.ClearSelection();
                dataGridViewSubventions.Rows[pos].Selected = true;
            }
        }

        private void calculerSubvDptBouton_Click(object sender, EventArgs e) {
            numericSubventionDpt.Value = CalculerSubvention(numericDSHTDpt.Value, numericTauxDpt.Value);
        }

        private void calculerSubvGIPBouton_Click(object sender, EventArgs e) {
            numericSubventionGIP.Value = CalculerSubvention(numericDSHTGIP.Value, numericTauxGIP.Value);
        }

        private void calculerSubvSURBouton_Click(object sender, EventArgs e) {
            numericSubventionSUR.Value = CalculerSubvention(numericDSHTSUR.Value, numericTauxSUR.Value);
        }

        private void calculerSubvAEBouton_Click(object sender, EventArgs e) {
            numericSubventionAE.Value = CalculerSubvention(numericDSHTAE.Value, numericTauxAE.Value);
        }

        private void comboEtatSubvention_SelectedIndexChanged(object sender, EventArgs e) {

            int idStatut = Convert.ToInt32(comboEtatSubvention.Get_SelectedId());

            if (idStatut == (int)eStatut.Programme || idStatut == (int)eStatut.Solde)
                labelDateAR.Visible = labelDateProg.Visible = dateARDossierComplet.Visible = dateProgrammation.Visible = true;
            else if (idStatut == (int)eStatut.Complet || idStatut == (int)eStatut.Pret_Programme) {
                labelDateAR.Visible = dateARDossierComplet.Visible = true;
                labelDateProg.Visible = dateProgrammation.Visible = false;
            }
            else
                labelDateAR.Visible = labelDateProg.Visible = dateARDossierComplet.Visible = dateProgrammation.Visible = false;

            this.Modification(sender, e);

        }

        private void ajouterUnDossierToolStripMenuItem_Click(object sender, EventArgs e) {
            this.ViderControls();
            this.EnregistrerSubvention(-1);
        }

        private void enregistrerSubventionBouton_Click(object sender, EventArgs e) {
            this.EnregistrerSubvention(Convert.ToInt32(dataGridViewSubventions.SelectedRows[0].Cells["idSubvention"].Value));
        }

        private void dateARDossierComplet_ValueChanged(object sender, EventArgs e) {
            this.Modification(sender, e);
        }

        private void checkClauses_CheckedChanged(object sender, EventArgs e) {
            labelNbheures.Visible = numericNbHeures.Visible = checkClauses.Checked;
        }

        private void checkAfficherDossiersProgrammes_CheckedChanged(object sender, EventArgs e) {
            this.Rechercher();
        }

        private void annulerSubventionBouton_Click(object sender, EventArgs e) {
            AfficherSubventionSelectionnee(Convert.ToInt32(dataGridViewSubventions.SelectedRows[0].Cells["idSubvention"].Value));
        }

        private void supprimerLeDossierToolStripMenuItem_Click(object sender, EventArgs e) {
            int idSubvention = Convert.ToInt32(dataGridViewSubventions.SelectedRows[0].Cells["idSubvention"].Value);

            if (MessageBox.Show("Voulez-vous SUPPRIMER la Subvention [" + dataGridViewSubventions.SelectedRows[0].Cells["CollectiviteSubvention"].Value + "] ?\n Attention, la suppression est irréversible !", "Suppression de la Subvention ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                int index = dataGridViewSubventions.SelectedRows[0].Index;

                if (CSubvention.Supprimer(Session, idSubvention)) {
                    dataGridViewSubventions.Rows.RemoveAt(index);
                }
            }

            
        }

        private void textRecherche_KeyUp(object sender, KeyEventArgs e) {
            this.Rechercher();
        }

        private void ajouterUneCollectivitéToolStripMenuItem_Click(object sender, EventArgs e) {
            this.AjouterCollectivite();
        }

        private void ajouterUneCollectivitéToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.AjouterCollectivite();
        }

        private void supprimerLaCollectivitéToolStripMenuItem_Click(object sender, EventArgs e) {
            int idSubvention = Convert.ToInt32(dataGridViewSubventions.SelectedRows[0].Cells["idSubvention"].Value);
            CSubvention_Collectivite.Supprimer(Session, idSubvention, dataGridViewCollectivitesSubvention.SelectedRows[0].Cells["CodeCollectiviteSubvention"].Value.ToString());
            AfficherSubventionSelectionnee(idSubvention);
        }

        private void comboRechercheType_SelectedIndexChanged(object sender, EventArgs e) {
            this.Rechercher();
        }

        private void subventionsDeLannéeToolStripMenuItem_Click(object sender, EventArgs e) {
            GenererExtractionStations(Session, DateTime.Today.Year);
        }

        private void subventionsSoldéesToolStripMenuItem_Click(object sender, EventArgs e) {
            GenererExtractionStations(Session, -1, true, true);
        }

        private void subventionsNonSoldéesToolStripMenuItem_Click(object sender, EventArgs e) {
            GenererExtractionStations(Session, -1, true, false);
        }

    }
}
