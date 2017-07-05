using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ATE55 {

    public partial class frmConventions : Form {

        CSession Session;
        SqlCommand command;
        SqlDataReader dataReader;
        bool Loaded = false;

        int idConventionCourante = -1;

        public frmConventions() {
            InitializeComponent();
            this.Text = "ATE55 - Conventions";
        }

        private void frmConventions_Load(object sender, EventArgs e) {

            Session = (CSession)this.Tag;

            foreach (KeyValuePair<string, CCollectivite> Collectivite in frmATE55.Collectivites)
                comboCollectiviteConvention.Items.Add(Collectivite.Value.NomCollectivite + " - " + Collectivite.Key);

            // On renseigne les informations nécessaires dans chaque comboBox
            frmATE55.AlimenterComboBox("ThèmeConvention", comboThemeConvention, null, Session, -1);
            ChangerCouleursCombo(comboThemeConvention);
            frmATE55.AlimenterComboBox("ThèmeConvention' AND idStatut <> "+(int)eStatut.Assainissement+" AND idStatut <> "+(int)eStatut.DUP+" OR CategorieStatut = 'TypeConvention", comboChoixTypeConvention, null, Session, -1);
            ChangerCouleursCombo(comboChoixTypeConvention);
            // Sélection du thème "non renseigné" pour déclencher l'affichage de la liste des conventions
            comboThemeConvention.SelectedIndex = -1;

            toolStripLabel_Session.Visible = true;
            toolStripLabel_Session.Text = Session.Utilisateur.Utilisateur
                + " [" + Session.Utilisateur.CodeDomaine + "/" + Session.Utilisateur.DroitAccess + "]"
                + ", accès précédent le " + Session.Utilisateur.DerniereConnexion.ToString();

            comboThemeConvention.Set_SelectedId("0");

            // Contour des cellules en noir
            dataGridViewConventions.GridColor = Color.Black;

            //this.ImporterConventions();

            this.AfficherConventions(true);

        }

        private void AfficherConventions(bool PremierLancement = false) {

            idConventionCourante = -1;

            DataGridView dgv = dataGridViewConventions;
            try {
                dgv.Rows.Clear();
                dgv.Refresh();
            }
            catch (InvalidOperationException) {}

            DataGridViewRow row;

            try {

                string req = "SELECT Count(idConvention) FROM Convention";
                command = new SqlCommand(req, Session.oConn);
                dgv.Rows.Add(Convert.ToInt32(command.ExecuteScalar()));

                req = "SELECT idConvention,idStatut_TypeConvention,NomCollectivite,DateDebutConvention,DateFinConvention FROM Convention LEFT JOIN Collectivite_V ON Convention.CodeCollectivite = Collectivite_V.CodeCollectivite ORDER BY NomCollectivite";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                int i = 0;
                // Récupération des conventions
                if (dataReader != null && dataReader.HasRows) {
                    
                    while (dataReader.Read()) {
                        
                        int idConvention = Convert.ToInt32(dataReader["idConvention"]);
                        int idType = Convert.ToInt32(dataReader["idStatut_TypeConvention"]);
                        DateTime DateDebutPrevision = dataReader["DateDebutConvention"].GetType().Name == "DBNull" ? DateTime.MinValue : Convert.ToDateTime(dataReader["DateDebutConvention"]);
                        DateTime DateFinPrevision = dataReader["DateFinConvention"].GetType().Name == "DBNull" ? DateTime.MinValue : Convert.ToDateTime(dataReader["DateFinConvention"]);
                        

                        row = dgv.Rows[i];
                        i++;
                        row.Cells["idConvention"].Value = idConvention;

                        row.Cells["CollectiviteConvention"].Value = dataReader["NomCollectivite"].ToString();
                        
                        
                        // Années
                        if (DateFinPrevision != DateTime.MinValue && DateDebutPrevision != DateTime.MinValue) {

                            int dateDebut = DateDebutPrevision.Year;
                            int dateFin = DateFinPrevision.Year;
                            string annees = "";
                            for (int j = dateDebut; j < dateFin; j++)
                                annees += j + "\n";
                            annees += dateFin;
                            row.Cells["AnneesConvention"].Value = annees;

                            // On affiche les années en rouge si la convention est terminée
                            if (DateFinPrevision < DateTime.Today) {
                                row.Cells["AnneesConvention"].Style.ForeColor = Color.Red;
                                // Si la case n'est pas cochée on cache
                                row.Visible = checkAfficherConventions.Checked;
                            }
                        }

                        row.Cells["TypeConvention"].Value = frmATE55.Statuts[idType].LibelleStatut;

                        // Couleur
                        row.DefaultCellStyle.BackColor = frmATE55.couleursTypes[idType];
                                                
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

            tabConvention.Visible = false;

            // Le paramètre permet d'optimiser le temps de lancement de l'application
            // Quand la recherche masque la première ligne affichée elle déclenche la sélection (rowStateChanged) de la suivante plusieurs fois
            // Ce qui l'affiche et ralentit le lancement
            if (!PremierLancement)
                this.Rechercher();
            else
                Loaded = true;

        }

        private void AfficherCollectivites(int idConvention) {

            dataGridViewCollectivitesConvention.Rows.Clear();
            dataGridViewCollectivitesConvention.Refresh();

            // Affichage des collectivités impactées
            string req = "SELECT CodeCollectivite FROM Convention_Collectivite WHERE idConvention = " + idConvention;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            // On stocke les codes des collectivités impactées dans une liste pour pouvoir s'en resservir après
            List<string> CollectivitesImpactees = new List<string>();
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read())
                    CollectivitesImpactees.Add(dataReader["CodeCollectivite"].ToString());
            }
            dataReader.Close();

            // Parcourt de la liste des codes
            foreach (string c in CollectivitesImpactees) {

                int i = dataGridViewCollectivitesConvention.Rows.Add();
                DataGridViewRow row = dataGridViewCollectivitesConvention.Rows[i];

                row.Cells["CodeCollectiviteImpacConvention"].Value = c;
                row.Cells["CollectiviteImpacConvention"].Value = frmATE55.Collectivites[c].NomCollectivite;

                // Population
                req = "SELECT PopulationDGF FROM Eligibilite WHERE CodeCollectivite = " + c + " ORDER BY AnneeEligibilite DESC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();
                    row.Cells["PopDGFConvention"].Value = dataReader["PopulationDGF"].ToString();
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

                    row.Cells["PopDGFConvention"].Value = population;

                }

            }    
                    
        }

        private void AfficherConventionSelectionnee(int idConvention, int idLigneConvention = -1) {

            this.ViderControls();

            if (idConvention > 0) {

                try {

                    CConvention convention = new CConvention();

                    string req = "SELECT idStatut_TypeConvention,CodeCollectivite,DateDebut,DateFin,DateDebutConvention,DateFinConvention,ObservationsConvention,CreeLe,CreePar,ModifieLe,ModifiePar FROM Convention WHERE idConvention = " + idConvention;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        convention.idConvention = idConvention;
                        convention.idStatut_TypeConvention = Convert.ToInt32(dataReader["idStatut_TypeConvention"]);
                        convention.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                        convention.DateDebut = dataReader["DateDebut"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDebut"]) : DateTime.MinValue;
                        convention.DateFin = dataReader["DateFin"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateFin"]) : DateTime.MinValue;
                        convention.DateDebutPrevision = dataReader["DateDebutConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDebutConvention"]) : DateTime.MinValue;
                        convention.DateFinPrevision = dataReader["DateFinConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateFinConvention"]) : DateTime.MinValue;
                        convention.ObservationsConvention = dataReader["ObservationsConvention"].ToString();
                        convention.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : DateTime.MinValue;
                        convention.CreePar = dataReader["CreePar"].ToString();
                        convention.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : DateTime.MinValue;
                        convention.ModifiePar = dataReader["ModifiePar"].ToString();

                        // On masque et on affiche les champs de formulaires en fonction du type de la convention
                        MasquerControlsInutiles(convention.idStatut_TypeConvention);


                        // Affichage des données
                        comboChoixTypeConvention.Set_SelectedId(convention.idStatut_TypeConvention.ToString());

                        int index = -1;
                        try {
                            labelAgenceEau.Text = "AE : " + frmATE55.Collectivites[convention.CodeCollectivite].AgenceEau;
                            index = comboCollectiviteConvention.Items.IndexOf(frmATE55.Collectivites[convention.CodeCollectivite].NomCollectivite + " - " + convention.CodeCollectivite);
                            comboCollectiviteConvention.SelectedIndex = index;
                        }
                        catch (Exception e) { e.ToString(); }


                        dateDebutConvention.Value = (convention.DateDebut != DateTime.MinValue) ? Convert.ToDateTime(convention.DateDebut) : dateDebutConvention.MinDate;
                        dateDebutConvention.Checked = convention.DateDebut != DateTime.MinValue;
                        dateFinConvention.Value = (convention.DateFin != DateTime.MinValue) ? Convert.ToDateTime(convention.DateFin) : dateFinConvention.MinDate;
                        dateFinConvention.Checked = convention.DateFin != DateTime.MinValue;

                        dateDebutPrevision.Value = (convention.DateDebutPrevision != DateTime.MinValue) ? Convert.ToDateTime(convention.DateDebutPrevision) : dateDebutPrevision.MinDate;
                        dateDebutPrevision.Checked = (convention.DateDebutPrevision != DateTime.MinValue);
                        dateFinPrevision.Value = (convention.DateFinPrevision != DateTime.MinValue) ? Convert.ToDateTime(convention.DateFinPrevision) : dateFinPrevision.MinDate;
                        dateFinPrevision.Checked = (convention.DateFinPrevision != DateTime.MinValue);
                        textObservationsConvention.Text = convention.ObservationsConvention;

                        dataReader.Close();

                        // On affiche les collectivités impactées
                        AfficherCollectivites(convention.idConvention);

                        // Calcul du nombre d'années et remplissage du comboAnneeConvention
                        if (dateDebutPrevision.Checked && dateFinPrevision.Checked) {

                            int anneeDebut = Convert.ToDateTime(dateDebutPrevision.Value).Year;
                            int anneeFin = Convert.ToDateTime(dateFinPrevision.Value).Year;

                            for (int i = anneeDebut; i <= anneeFin; i++) {

                                CustomComboBox.CustomComboBoxItem item;
                                // On teste si une ligneConvention existe pour cette année
                                req = "SELECT idLigneConvention FROM LigneConvention WHERE idConvention = " + idConvention + " AND AnneeLigneConvention = " + i;
                                command = new SqlCommand(req, Session.oConn);
                                if (command.ExecuteScalar() != null)
                                    item = new CustomComboBox.CustomComboBoxItem(command.ExecuteScalar().ToString(), i.ToString(), Color.Blue, true);
                                else
                                    item = new CustomComboBox.CustomComboBoxItem("-1", i.ToString(), Color.Red, false);

                                comboAnneeConvention.Items.Add(item);
                            }

                            // On sélectionne la ligne
                            if (idLigneConvention != -1)
                                comboAnneeConvention.Set_SelectedId(idLigneConvention.ToString());
                            else {
                                try {
                                    comboAnneeConvention.SelectedIndex = 0;
                                }
                                catch (ArgumentOutOfRangeException e) { e.ToString(); }
                            }
                        }

                        // Affichage des informations de création et modification
                        infosModifConvention.Text = convention.InfosModif();

                        enregistrerConventionBouton.Enabled = annulerConventionBouton.Enabled = false;

                        idConventionCourante = idConvention;

                        tabConvention.Visible = true;
                    }
                    else {
                        dataReader.Close();
                        tabConvention.Visible = false;
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

        }

        private void AfficherLigneConvention(int idConvention, int annee) {

            ViderControlsLigneConvention();

            try {

                if (annee != -1) {

                    string req = "SELECT idLigneConvention,MontantAnneeConvention,MontantAnnexe2Convention,DateEnvoiCollectiviteSignatureConvention,DateRetourConvention,DateSignatureConvention,DateEnvoiCollectiviteSigneConvention,RevisionEnvoiConvention,RevisionRetourConvention,DateEnvoiRPQS,DateRetourRPQS,MandatRPQS,EnvoiAnnexe2_Marche,RetourAnnexe2_Signee,MandatSPAC,ObservationsConvention,NonRecouvreConvention,DateArreteDUP FROM LigneConvention WHERE idConvention = " + idConvention + " AND AnneeLigneConvention = " + annee;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    CLigneConvention ligneConvention = new CLigneConvention();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        ligneConvention.idLigneConvention = Convert.ToInt32(dataReader["idLigneConvention"]);
                        ligneConvention.idConvention = idConvention;
                        ligneConvention.AnneeLigneConvention = annee;
                        ligneConvention.MontantAnneeConvention = Convert.ToDecimal(dataReader["MontantAnneeConvention"]);
                        ligneConvention.MontantAnnexe2Convention = Convert.ToDecimal(dataReader["MontantAnnexe2Convention"]);
                        ligneConvention.DateEnvoiCollectiviteSignatureConvention = dataReader["DateEnvoiCollectiviteSignatureConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEnvoiCollectiviteSignatureConvention"]) : DateTime.MinValue;
                        ligneConvention.DateRetourConvention = dataReader["DateRetourConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateRetourConvention"]) : DateTime.MinValue;
                        ligneConvention.DateSignatureConvention = dataReader["DateSignatureConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateSignatureConvention"]) : DateTime.MinValue;
                        ligneConvention.DateEnvoiCollectiviteSigneConvention = dataReader["DateEnvoiCollectiviteSigneConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEnvoiCollectiviteSigneConvention"]) : DateTime.MinValue;
                        ligneConvention.RevisionEnvoiConvention = dataReader["RevisionEnvoiConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["RevisionEnvoiConvention"]) : DateTime.MinValue;
                        ligneConvention.RevisionRetourConvention = dataReader["RevisionRetourConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["RevisionRetourConvention"]) : DateTime.MinValue;
                        ligneConvention.DateEnvoiRPQS = dataReader["DateEnvoiRPQS"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEnvoiRPQS"]) : DateTime.MinValue;
                        ligneConvention.DateRetourRPQS = dataReader["DateRetourRPQS"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateRetourRPQS"]) : DateTime.MinValue;
                        ligneConvention.MandatRPQS = dataReader["MandatRPQS"].ToString();
                        ligneConvention.EnvoiAnnexe2_Marche = dataReader["EnvoiAnnexe2_Marche"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["EnvoiAnnexe2_Marche"]) : DateTime.MinValue;
                        ligneConvention.RetourAnnexe2_Marche = dataReader["RetourAnnexe2_Signee"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["RetourAnnexe2_Signee"]) : DateTime.MinValue;
                        ligneConvention.MandatSPAC = dataReader["MandatSPAC"].ToString();
                        ligneConvention.ObservationsConvention = dataReader["ObservationsConvention"].ToString();
                        ligneConvention.NonRecouvreConvention = dataReader["NonRecouvreConvention"].ToString();
                        ligneConvention.DateArreteDUP = dataReader["DateArreteDUP"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateArreteDUP"]) : DateTime.MinValue;

                        dataReader.Close();


                        // Affichage
                        numericMontantConvention.Value = ligneConvention.MontantAnneeConvention;
                        numericMontantAnnexe.Value = ligneConvention.MontantAnnexe2Convention;

                        try {
                            dateEnvoiCollectivite.Value = (ligneConvention.DateEnvoiCollectiviteSigneConvention != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.DateEnvoiCollectiviteSigneConvention) : dateEnvoiCollectivite.MinDate;
                        }
                        catch (ArgumentOutOfRangeException e) {
                            this.Text = "Min : " + dateEnvoiCollectivite.MinDate.ToShortDateString() + "/ Max : " + dateEnvoiCollectivite.MaxDate.ToShortDateString() + "/Date : " + ligneConvention.DateEnvoiCollectiviteSigneConvention;
                            e.ToString();
                            return;
                        }
                        dateSignature.Value = (ligneConvention.DateSignatureConvention != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.DateSignatureConvention) : dateSignature.MinDate;
                        dateRetourSignature.Value = (ligneConvention.DateRetourConvention != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.DateRetourConvention) : dateRetourSignature.MinDate;
                        dateEnvoiSignature.Value = (ligneConvention.DateEnvoiCollectiviteSignatureConvention != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.DateEnvoiCollectiviteSignatureConvention) : dateEnvoiSignature.MinDate;
                        dateEnvoiSignature.Checked = dateRetourSignature.Enabled = (ligneConvention.DateEnvoiCollectiviteSignatureConvention != DateTime.MinValue);
                        dateRetourSignature.Checked = dateSignature.Enabled = (ligneConvention.DateRetourConvention != DateTime.MinValue);
                        dateSignature.Checked = dateEnvoiCollectivite.Enabled = (ligneConvention.DateSignatureConvention != DateTime.MinValue);
                        dateEnvoiCollectivite.Checked = (ligneConvention.DateEnvoiCollectiviteSigneConvention != DateTime.MinValue);

                        dateRetourRevision.Value = (ligneConvention.RevisionRetourConvention != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.RevisionRetourConvention) : dateRetourRevision.MinDate;
                        dateEnvoiRevision.Value = (ligneConvention.RevisionEnvoiConvention != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.RevisionEnvoiConvention) : dateEnvoiRevision.MinDate;
                        dateEnvoiRevision.Checked = dateRetourRevision.Enabled = (ligneConvention.RevisionEnvoiConvention != DateTime.MinValue);
                        dateRetourRevision.Checked = (ligneConvention.RevisionRetourConvention != DateTime.MinValue);

                        dateRetourRPQS.Value = (ligneConvention.DateRetourRPQS != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.DateRetourRPQS) : dateRetourRPQS.MinDate;
                        dateEnvoiRPQS.Value = (ligneConvention.DateEnvoiRPQS != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.DateEnvoiRPQS) : dateEnvoiRPQS.MinDate;
                        dateEnvoiRPQS.Checked = dateRetourRPQS.Enabled = (ligneConvention.DateEnvoiRPQS != DateTime.MinValue);
                        dateRetourRPQS.Checked = (ligneConvention.DateRetourRPQS != DateTime.MinValue);

                        checkMandatRPQS.Checked = (ligneConvention.MandatRPQS.Equals("oui"));

                        dateRetourAnnexe.Value = (ligneConvention.RetourAnnexe2_Marche != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.RetourAnnexe2_Marche) : dateRetourAnnexe.MinDate;
                        dateEnvoiAnnexe.Value = (ligneConvention.EnvoiAnnexe2_Marche != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.EnvoiAnnexe2_Marche) : dateEnvoiAnnexe.MinDate;
                        dateEnvoiAnnexe.Checked = dateRetourAnnexe.Enabled = (ligneConvention.EnvoiAnnexe2_Marche != DateTime.MinValue);
                        dateRetourAnnexe.Checked = (ligneConvention.RetourAnnexe2_Marche != DateTime.MinValue);

                        checkMandatSPAC.Checked = (ligneConvention.MandatSPAC.Equals("oui"));
                        textObservationsLigne.Text = ligneConvention.ObservationsConvention;
                        textNonRecouvre.Text = ligneConvention.NonRecouvreConvention;
                        dateArreteDUP.Value = (ligneConvention.DateArreteDUP != DateTime.MinValue) ? Convert.ToDateTime(ligneConvention.DateArreteDUP) : dateArreteDUP.MinDate;
                        dateArreteDUP.Checked = (ligneConvention.DateArreteDUP != DateTime.MinValue);


                        // Récupération des titres
                        req = "SELECT idTitre,NumTitreConvention,DateEmissionTitreConvention FROM Titre WHERE idLigneConvention = " + ligneConvention.idLigneConvention + " ORDER BY DateEmissionTitreConvention,NumTitreConvention DESC";
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            while (dataReader.Read()) {

                                CustomComboBox.CustomComboBoxItem item = new CustomComboBox.CustomComboBoxItem(dataReader["idTitre"].ToString(), dataReader["NumTitreConvention"].ToString(), Color.Blue, true);
                                comboTitre.Items.Add(item);

                            }
                            dataReader.Close();
                            comboTitre.SelectedIndex = 0;
                        }
                        else
                            dataReader.Close();

                        genererConvention.Enabled = genererAnnexeBouton.Enabled = true;
                    }
                    else
                        dataReader.Close();

                    enregistrerLigneConventionBouton.Enabled = false;

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

        private void AfficherTitre(int idTitre) {

            string req = "SELECT TOP 1 * FROM Titre WHERE idTitre = " + idTitre;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();

                CTitre titre = new CTitre();
                titre.idLigneConvention = Convert.ToInt32(dataReader["idLigneConvention"]);
                titre.MontantTitreConvention = Convert.ToDecimal(dataReader["MontantTitreConvention"]);
                titre.NumTitreConvention = Convert.ToInt32(dataReader["NumTitreConvention"]);
                titre.DateEmissionTitreConvention = dataReader["DateEmissionTitreConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEmissionTitreConvention"]) : DateTime.MinValue;

                numericMontantTitre.Value = titre.MontantTitreConvention;
                numericTitre.Value = titre.NumTitreConvention;
                dateEmissionTitre.Value = (titre.DateEmissionTitreConvention != DateTime.MinValue) ? Convert.ToDateTime(titre.DateEmissionTitreConvention) : dateEmissionTitre.MinDate;
                dateEmissionTitre.Checked = (titre.DateEmissionTitreConvention != DateTime.MinValue);

            }
            dataReader.Close();

        }

        private void UpdateRowConvention(int index, int idConvention, bool nouvelle = false) {

            DataGridViewRow row = dataGridViewConventions.Rows[index];

            try {

                string req = "SELECT idConvention,idStatut_TypeConvention,CodeCollectivite,DateDebutConvention,DateFinConvention FROM Convention WHERE idConvention = " + idConvention;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CConvention convention = new CConvention();
                    convention.idConvention = idConvention;
                    convention.idStatut_TypeConvention = Convert.ToInt32(dataReader["idStatut_TypeConvention"]);
                    convention.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                    convention.DateDebutPrevision = dataReader["DateDebutConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDebutConvention"]) : DateTime.MinValue;
                    convention.DateFinPrevision = dataReader["DateFinConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateFinConvention"]) : DateTime.MinValue;

                    dataReader.Close();

                    // Affichage
                    row.DefaultCellStyle.BackColor = frmATE55.couleursTypes[convention.idStatut_TypeConvention];
                    row.Cells["idConvention"].Value = convention.idConvention;
                    row.Cells["CollectiviteConvention"].Value = frmATE55.Collectivites[convention.CodeCollectivite].NomCollectivite;

                    // Années
                    if (convention.DateFinPrevision.Value != DateTime.MinValue && convention.DateDebutPrevision.Value != DateTime.MinValue) {
                        int dateDebut = ((DateTime)(convention.DateDebutPrevision)).Year;
                        int dateFin = ((DateTime)(convention.DateFinPrevision)).Year;
                        string annees = "";
                        for (int j = dateDebut; j < dateFin; j++)
                            annees += j + "\n";
                        annees += dateFin;
                        row.Cells["AnneesConvention"].Value = annees;

                        // On affiche les années en rouge si la convention est terminée
                        if (convention.DateFinPrevision < DateTime.Today)
                            row.Cells["AnneesConvention"].Style.ForeColor = Color.Red;
                    }

                    row.Cells["TypeConvention"].Value = frmATE55.Statuts[convention.idStatut_TypeConvention].LibelleStatut;


                    // On sélectionne la ligne et on force le scrolling jusqu'à cette ligne
                    if (nouvelle) {
                        dataGridViewConventions.ClearSelection();
                        row.Selected = true;
                        dataGridViewConventions.FirstDisplayedScrollingRowIndex = dataGridViewConventions.SelectedRows[0].Index;
                    }
                    this.AfficherConventionSelectionnee(convention.idConvention);

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

        private void Rechercher() {

            // La recherche se base sur la textbox et les combobox
            // On récupère le texte ainsi que les éléments sélectionnés
            string TextRecherche = searchConventionBox.Text.ToLower();
            string ThemeRecherche = !comboThemeConvention.Get_SelectedId().Equals("0") ? comboThemeConvention.SelectedItem.ToString().ToLower() : "";
            string TypeRecherche = (comboTypeConvention.Items.Count > 0 && !comboTypeConvention.Get_SelectedId().Equals("0") && !comboThemeConvention.Get_SelectedId().Equals("0")) ? comboTypeConvention.SelectedItem.ToString().ToLower() : "";
            
            // On parcourt la datagridview
            foreach (DataGridViewRow row in dataGridViewConventions.Rows) {


                // Le filtre se fait sur la collectivité, les années et le type
                string NomCollectivite = row.Cells["CollectiviteConvention"].Value.ToString().ToLower();
                string Annees = row.Cells["AnneesConvention"].Value.ToString();
                string Type = row.Cells["TypeConvention"].Value.ToString().ToLower();

                // On affiche ou on cache la ligne en fonction si elle contient la recherche ou non
                // Si aucune filtre n'est renseigné on affiche
                if (TextRecherche.Equals("") && ThemeRecherche.Equals("") && TypeRecherche.Equals(""))
                    row.Visible = true;
                // Sinon si le thème/type est renseigné mais pas le texte on ne filtre que sur le type
                else if(TextRecherche.Equals(""))
                    row.Visible = (!ThemeRecherche.Equals("") && Type.Equals(ThemeRecherche)) || (!TypeRecherche.Equals("") && Type.Equals(TypeRecherche));
                // Sinon si seul le texte est renseigné
                else if(ThemeRecherche.Equals("") && TypeRecherche.Equals(""))
                    row.Visible = NomCollectivite.Contains(TextRecherche) || Annees.Contains(TextRecherche) || Type.Equals(TextRecherche);
                // Sinon on filtre tous les champs
                else
                    row.Visible = NomCollectivite.Contains(TextRecherche) || Annees.Contains(TextRecherche) || Type.Equals(TextRecherche) || (!ThemeRecherche.Equals("") && Type.Equals(ThemeRecherche)) || (!TypeRecherche.Equals("") && Type.Equals(TypeRecherche));


                // Si la case n'est pas cochée on cache les conventions terminées (texte rouge)
                if (!checkAfficherConventions.Checked)
                    row.Visible = row.Visible && !row.Cells["AnneesConvention"].Style.ForeColor.Equals(Color.Red);


            }

        }

        private void ChangerCouleursCombo(CustomComboBox.CustomComboBox combo) {

            foreach (CustomComboBox.CustomComboBoxItem item in combo.Items)
                item.BackColor = frmATE55.couleursTypes[Convert.ToInt32(item.idItem)];

        }

        private void ViderControls() {

            // On réinitialise tous les contrôles en rapport avec une convention pour les remettre aux valeurs par défaut
            comboCollectiviteConvention.SelectedIndex = -1;
            comboChoixTypeConvention.Set_SelectedId("0");
            labelAgenceEau.Text = "AE : ";
            dateDebutPrevision.Value = dateFinPrevision.Value = dateDebutConvention.Value = dateFinConvention.Value = DateTime.Today;
            dateDebutPrevision.Checked = dateFinPrevision.Checked = dateDebutConvention.Checked = dateFinConvention.Checked = false;
            textObservationsConvention.Text = "";
            panelLigneConvention.Visible = false;

            enregistrerConventionBouton.Enabled = annulerConventionBouton.Enabled = false;

            dataGridViewCollectivitesConvention.Rows.Clear();
            dataGridViewCollectivitesConvention.Refresh();

            comboAnneeConvention.Items.Clear();
            comboAnneeConvention.Refresh();
        }

        private void ViderControlsLigneConvention() {

            // On réinitialise tous les contrôles en rapport avec une ligne convention pour les remettre aux valeurs par défaut
            dateEnvoiSignature.MinDate = dateRetourSignature.MinDate = dateSignature.MinDate = dateEnvoiCollectivite.MinDate = dateEmissionTitre.MinDate = dateEnvoiRevision.MinDate = dateRetourRevision.MinDate = dateArreteDUP.MinDate = dateEnvoiRPQS.MinDate = dateRetourRPQS.MinDate = dateEnvoiAnnexe.MinDate = dateRetourAnnexe.MinDate = new DateTime(2000, 1, 1);

            dateEnvoiSignature.Value = dateRetourSignature.Value = dateSignature.Value = dateEnvoiCollectivite.Value = dateEmissionTitre.Value = dateEnvoiRevision.Value = dateRetourRevision.Value = dateArreteDUP.Value = dateEnvoiRPQS.Value = dateRetourRPQS.Value = dateEnvoiAnnexe.Value = dateRetourAnnexe.Value = new DateTime(2000, 1, 1);
            dateEnvoiSignature.Checked = dateRetourSignature.Checked = dateSignature.Checked = dateEnvoiCollectivite.Checked = dateEmissionTitre.Checked = dateEnvoiRevision.Checked = dateRetourRevision.Checked = dateArreteDUP.Checked = dateEnvoiRPQS.Checked = dateRetourRPQS.Checked = dateEnvoiAnnexe.Checked = dateRetourAnnexe.Checked = false;
            checkMandatRPQS.Checked = checkMandatSPAC.Checked = false;

            genererConvention.Enabled = genererAnnexeBouton.Enabled = dateRetourSignature.Enabled = dateSignature.Enabled = dateEnvoiCollectivite.Enabled = dateRetourRevision.Enabled = dateRetourRPQS.Enabled = dateRetourAnnexe.Enabled = false;

            comboTitre.Items.Clear();

            textNonRecouvre.Text = textObservationsLigne.Text = "";
            numericMontantAnnexe.Value = numericMontantConvention.Value = 0;

            ViderControlsTitre();
        }

        private void ViderControlsTitre() {
            dateEmissionTitre.MinDate = new DateTime(2000, 1, 1);
            dateEmissionTitre.Value = DateTime.Today;
            dateEmissionTitre.Checked = false;
            labelTitre.Visible = false;
            numericTitre.Value = numericMontantTitre.Value = 0;
            numericTitre.ForeColor = Color.Blue;
        }

        private void MasquerControlsInutiles(int idType) {

            // On affiche et on masque les controls en fonction du type de convention
            if (idType == (int)eStatut.Realisation_DUP || idType == (int)eStatut.Suivi_DUP || idType == (int)eStatut.Diagnostic_territorial || idType == (int)eStatut.Rivieres_ZH || idType == (int)eStatut.DUP || idType == (int)eStatut.Captage_DUP || idType == (int)eStatut.SDAGE || idType == (int)eStatut.DUP_SDAGE || idType == (int)eStatut.Assainissement || idType == (int)eStatut.Programme_d_Assainissement) {
                tabRPQS.Visible = tabSPAC.Visible = false;
                tabInfosTitre.Visible = true;
            }
            else if (idType == (int)eStatut.SPAC || idType == (int)eStatut.SPAC_A || idType == (int)eStatut.SPAC_R)
                tabRPQS.Visible = tabSPAC.Visible = tabInfosTitre.Visible = true;
            else if (idType == (int)eStatut.SPANC) {
                tabRPQS.Visible = tabInfosTitre.Visible = true;
                tabSPAC.Visible = false;
            }
            else if (idType == (int)eStatut.Audit_Gestion || idType == (int)eStatut.NonPrecise)
                tabInfosTitre.Visible = tabRPQS.Visible = tabSPAC.Visible = false;

            if (idType == (int)eStatut.DUP || idType == (int)eStatut.Realisation_DUP || idType == (int)eStatut.Suivi_DUP || idType == (int)eStatut.Diagnostic_territorial || idType == (int)eStatut.Captage_DUP || idType == (int)eStatut.SDAGE || idType == (int)eStatut.DUP_SDAGE) {
                if (idType != (int)eStatut.Diagnostic_territorial)
                    dateArreteDUP.Visible = labelArreteDUP.Visible = true;
                else
                    dateArreteDUP.Visible = labelArreteDUP.Visible = false;
            }
            else
                dateArreteDUP.Visible = labelArreteDUP.Visible = false;

            if (dateArreteDUP.Visible || labelArreteDUP.Visible)
                panelArreteQ.Visible = true;
                
        }

        private void EnregistrerConvention(int idConvention) {

            CConvention convention = new CConvention(Session);

            convention.idConvention = idConvention;
            convention.idStatut_TypeConvention = (!comboChoixTypeConvention.Get_SelectedId().Equals("")) ? Convert.ToInt32(comboChoixTypeConvention.Get_SelectedId()) : 0;
            
            if(comboCollectiviteConvention.SelectedIndex != -1){
                String[] s = comboCollectiviteConvention.SelectedItem.ToString().Split(null);
                convention.CodeCollectivite = s[s.Length-1];
            }else
                convention.CodeCollectivite = "55000";

            convention.DateDebut = dateDebutConvention.Checked ? Convert.ToDateTime(dateDebutConvention.Value) : (DateTime?)null;
            convention.DateFin = dateFinConvention.Checked ? Convert.ToDateTime(dateFinConvention.Value) : (DateTime?)null;
            convention.DateDebutPrevision = dateDebutPrevision.Checked ? Convert.ToDateTime(dateDebutPrevision.Value) : (DateTime?)null;
            convention.DateFinPrevision = dateFinPrevision.Checked ? Convert.ToDateTime(dateFinPrevision.Value) : (DateTime?)null;
            convention.ObservationsConvention = textObservationsConvention.Text;

   
            // Si la convention existe on la met à jour, sinon on la crée
            if (idConvention != -1) {
                if (convention.Enregistrer()) {
                    // On cherche l'index de la convention
                    int index = -1;
                    foreach (DataGridViewRow row in dataGridViewConventions.Rows) {
                        if (idConvention == Convert.ToInt32(row.Cells["idConvention"].Value))
                            index = row.Index;
                    }
                    // On met à jour les cellules de la convention
                    UpdateRowConvention(index, convention.idConvention);
                }
            }
            else {
                if (convention.Creer()) {
                    // On crée une nouvelle ligne
                    UpdateRowConvention(dataGridViewConventions.Rows.Add(), convention.idConvention, true);
                }
            }

        }

        private void EnregistrerLigneConvention(int idLigneConvention, int idConvention, int annee) {

            CLigneConvention ligneConvention = new CLigneConvention(Session);

            ligneConvention.idLigneConvention = idLigneConvention;
            ligneConvention.idConvention = idConvention;
            ligneConvention.AnneeLigneConvention = annee;
            ligneConvention.MontantAnneeConvention = numericMontantConvention.Value;
            ligneConvention.MontantAnnexe2Convention = numericMontantAnnexe.Value;
            ligneConvention.DateEnvoiCollectiviteSignatureConvention = dateEnvoiSignature.Checked ? Convert.ToDateTime(dateEnvoiSignature.Value) : (DateTime?)null;
            ligneConvention.DateRetourConvention = dateRetourSignature.Checked ? Convert.ToDateTime(dateRetourSignature.Value) : (DateTime?)null;
            ligneConvention.DateSignatureConvention = dateSignature.Checked ? Convert.ToDateTime(dateSignature.Value) : (DateTime?)null;
            ligneConvention.DateEnvoiCollectiviteSigneConvention = dateEnvoiCollectivite.Checked ? Convert.ToDateTime(dateEnvoiCollectivite.Value) : (DateTime?)null;
            ligneConvention.RevisionEnvoiConvention = dateEnvoiRevision.Checked ? Convert.ToDateTime(dateEnvoiRevision.Value) : (DateTime?)null;
            ligneConvention.RevisionRetourConvention = dateRetourRevision.Checked ? Convert.ToDateTime(dateRetourRevision.Value) : (DateTime?)null;
            ligneConvention.DateEnvoiRPQS = dateEnvoiRPQS.Checked ? Convert.ToDateTime(dateEnvoiRPQS.Value) : (DateTime?)null;
            ligneConvention.DateRetourRPQS = dateRetourRPQS.Checked ? Convert.ToDateTime(dateRetourRPQS.Value) : (DateTime?)null;
            ligneConvention.MandatRPQS = checkMandatRPQS.Checked ? "oui" : "non";
            ligneConvention.EnvoiAnnexe2_Marche = dateEnvoiAnnexe.Checked ? Convert.ToDateTime(dateEnvoiAnnexe.Value) : (DateTime?)null;
            ligneConvention.RetourAnnexe2_Marche = dateRetourAnnexe.Checked ? Convert.ToDateTime(dateRetourAnnexe.Value) : (DateTime?)null;
            ligneConvention.MandatSPAC = checkMandatSPAC.Checked ? "oui" : "non";
            ligneConvention.ObservationsConvention = textObservationsLigne.Text;
            ligneConvention.NonRecouvreConvention = textNonRecouvre.Text;
            ligneConvention.DateArreteDUP = dateArreteDUP.Checked ? Convert.ToDateTime(dateArreteDUP.Value) : (DateTime?)null;

            Boolean enregistre = false;
            if (idLigneConvention != -1) {
                if (ligneConvention.Enregistrer()) {
                    enregistrerLigneConventionBouton.Enabled = false;
                    enregistre = true;
                }
            }
            else {
                if (ligneConvention.Creer()) {
                    enregistrerLigneConventionBouton.Enabled = false;
                    enregistre = true;
                }
            }



            // On crée ou on met à jour le titre si l'insertion de la LigneConvention a bien été effectuée
            if (enregistre) {
                int numeroTitre = Convert.ToInt32(numericTitre.Value);


                if (numeroTitre > 0) {
                    // On regarde si le titre existe
                    string req = "SELECT idTitre FROM Titre WHERE NumTitreConvention = " + numeroTitre + " AND idLigneConvention = " + ligneConvention.idLigneConvention;
                    command = new SqlCommand(req, Session.oConn);

                    CTitre titre = new CTitre(Session);
                    titre.idLigneConvention = ligneConvention.idLigneConvention;
                    titre.MontantTitreConvention = numericMontantTitre.Value;
                    titre.NumTitreConvention = Convert.ToInt32(numericTitre.Value);
                    titre.DateEmissionTitreConvention = dateEmissionTitre.Checked ? Convert.ToDateTime(dateEmissionTitre.Value) : (DateTime?)null;

                    // S'il existe on le met à jour sinon on le crée
                    if (command.ExecuteScalar() != null) {

                        int idTitre = Convert.ToInt32(command.ExecuteScalar());
                        titre.idTitre = idTitre;

                        titre.Enregistrer();
                    }
                    else
                        titre.Creer();
                }

            }

            AfficherConventionSelectionnee(idConvention, ligneConvention.idLigneConvention);

        }

        private void AjouterCollectivite() {

            int idConvention = Convert.ToInt32(dataGridViewConventions.SelectedRows[0].Cells["idConvention"].Value);

            // On récupère et on stocke les collectivités déjà liées à la convention pour ne pas les afficher
            string req = "SELECT CodeCollectivite FROM Convention_Collectivite WHERE idConvention = " + idConvention;
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

            frmListe frm = new frmListe("Ajouter des collectivités à la convention", "collectivités", "CodeCollectivite", "NomCollectivite", collectivites);
            var result = frm.ShowDialog();

            // On récupère les collectivités sélectionnées
            if (result == DialogResult.OK) {

                foreach (string CodeCollectivite in frm.listeRetour) {

                    // On teste si la collectivité est déjà liée à la convention
                    req = "SELECT Count(*) FROM Convention_Collectivite WHERE idConvention = " + idConvention + " AND CodeCollectivite = " + CodeCollectivite;
                    command = new SqlCommand(req, Session.oConn);

                    if ((int)command.ExecuteScalar() == 0) {
                        CConvention_Collectivite convention_collectivite = new CConvention_Collectivite(Session);
                        convention_collectivite.idConvention = idConvention;
                        convention_collectivite.CodeCollectivite = CodeCollectivite;
                        convention_collectivite.Creer();
                    }

                }

                AfficherCollectivites(idConvention);
            }

        }

        private void GenererFichierWordConvention(int idConvention) {

            // On récupère le type de la convention
            string req = "SELECT idStatut_TypeConvention,CodeCollectivite FROM Convention WHERE idConvention = " + idConvention;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            int Type = 0;
            string CodeCollectivite = "";

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();
                Type = Convert.ToInt32(dataReader["idStatut_TypeConvention"]);
                CodeCollectivite = dataReader["CodeCollectivite"].ToString();
            }
            dataReader.Close();

            // Word
            Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
            msWord.Visible = true;
            object missing = System.Reflection.Missing.Value;
            object fileName = @"Convention.docx";
            Microsoft.Office.Interop.Word.Document nvDoc;
            object templateName;

            // On charge le template en fonction du type de la convention
            switch (Type) {
                case (int)eStatut.Assainissement:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_Assainissement.dotx";
                    break;
                case (int)eStatut.Programme_d_Assainissement:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_Assainissement.dotx";
                    break;
                case (int)eStatut.SPAC:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_Assainissement.dotx";
                    break;
                case (int)eStatut.Audit_Gestion:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_Audit.dotx";
                    break;
                case (int)eStatut.Diagnostic_territorial:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_Diagnostic.dotx";
                    break;
                case (int)eStatut.DUP:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_DUP.dotx";
                    break;
                case (int)eStatut.Captage_DUP:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_DUP.dotx";
                    break;
                case (int)eStatut.SDAGE:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_DUP.dotx";
                    break;
                case (int)eStatut.DUP_SDAGE:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_DUP.dotx";
                    break;
                case (int)eStatut.Realisation_DUP:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_RealisationDUP.dotx";
                    break;
                case (int)eStatut.Rivieres_ZH:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_Rivieres.dotx";
                    break;
                case (int)eStatut.SPAC_A:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_SPAC_A.dotx";
                    break;
                case (int)eStatut.SPAC_R:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_SPAC_R.dotx";
                    break;
                case (int)eStatut.SPANC:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_SPANC.dotx";
                    break;
                case (int)eStatut.Suivi_DUP:
                    templateName = Application.StartupPath + @"\TemplatesWord\Convention_SuiviDUP.dotx";
                    break;
                default:
                    return;
            }

            // Si le template existe
            if (File.Exists(templateName.ToString())) {
                nvDoc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);

                // Noms des signets
                object NatureCollectivite = "NatureCollectivite";
                object GenreCollectivite = "GenreCollectivite";
                object NatureResponsable = "NatureResponsable";

                if (nvDoc.Bookmarks.Exists(NatureCollectivite.ToString())) {

                    // Nature de la collectivité (Le/La + nom)
                    Microsoft.Office.Interop.Word.Bookmark bookmarkNature = nvDoc.Bookmarks[ref NatureCollectivite];

                    Microsoft.Office.Interop.Word.Bookmark bookmarkGenreCollectivite = nvDoc.Bookmarks.Exists(GenreCollectivite.ToString()) ? nvDoc.Bookmarks[ref GenreCollectivite] : null;
                    Microsoft.Office.Interop.Word.Bookmark bookmarkNatureResponsable = nvDoc.Bookmarks.Exists(NatureResponsable.ToString()) ? nvDoc.Bookmarks[ref NatureResponsable] : null;

                    try {

                        string nature = frmATE55.Collectivites[CodeCollectivite].NatureCollectivite;

                        // On renseigne les informations en fonction de la nature de la collectivité
                        if (nature.Equals("COMMUNE")) {
                            bookmarkNature.Range.Text = "La commune de " + frmATE55.Collectivites[CodeCollectivite].NomCollectivite;
                            if (bookmarkGenreCollectivite != null) bookmarkGenreCollectivite.Range.Text = "e";
                            if (bookmarkNatureResponsable != null) bookmarkNatureResponsable.Range.Text = "Maire";
                        }
                        else if (nature.Equals("SIVOM") || nature.Equals("SMF") || nature.Equals("SIVU")) {
                            bookmarkNature.Range.Text = "Le " + frmATE55.Collectivites[CodeCollectivite].NomCollectivite;
                            if (bookmarkGenreCollectivite != null) bookmarkGenreCollectivite.Range.Text = "";
                        }
                        else {
                            bookmarkNature.Range.Text = "La " + frmATE55.Collectivites[CodeCollectivite].NomCollectivite;
                            if (bookmarkGenreCollectivite != null) bookmarkGenreCollectivite.Range.Text = "e";
                        }

                    }
                    catch (KeyNotFoundException e) {e.ToString();}

                }
            }

        }

        private void GenererAnnexe() {

            Microsoft.Office.Interop.Word.Application msWord = new Microsoft.Office.Interop.Word.Application();
            msWord.Visible = true;
            object missing = System.Reflection.Missing.Value;
            object fileName = @"Annexe.docx";

            Microsoft.Office.Interop.Word.Document nvDoc;
            object templateName = Application.StartupPath + @"\TemplatesWord\Annexe2.dotx";

            if (File.Exists(templateName.ToString())) {
                nvDoc = msWord.Documents.Add(ref templateName, ref missing, ref missing,
                                            ref missing);
            }

        }

        public static void GenererExtractionConventions(CSession Session) {

            Microsoft.Office.Interop.Excel.Application ApplicationXL;
            Microsoft.Office.Interop.Excel._Workbook ClasseurXL;
            Microsoft.Office.Interop.Excel._Worksheet FeuilXL_Conventions;

            StringBuilder errorMessages = new StringBuilder();

            try {

                // Excel
                ApplicationXL = new Microsoft.Office.Interop.Excel.Application();
                ApplicationXL.Visible = false;

                ClasseurXL = (Microsoft.Office.Interop.Excel._Workbook)ApplicationXL.Workbooks.Add(System.Reflection.Missing.Value);
                FeuilXL_Conventions = (Microsoft.Office.Interop.Excel._Worksheet)ClasseurXL.ActiveSheet;

                ((Microsoft.Office.Interop.Excel.Worksheet)ClasseurXL.Sheets[1]).Select();

                FeuilXL_Conventions.Name = "Conventions";

                // Headers
                FeuilXL_Conventions.Cells[1, 1] = "ID";
                FeuilXL_Conventions.Cells[1, 2] = "Code INSEE";
                FeuilXL_Conventions.Cells[1, 3] = "Collectivité";
                FeuilXL_Conventions.Cells[1, 4] = "AE";
                FeuilXL_Conventions.Cells[1, 5] = "Type";
                FeuilXL_Conventions.Cells[1, 6] = "Observations";
                FeuilXL_Conventions.Cells[1, 7] = "Année";
                FeuilXL_Conventions.Cells[1, 8] = "Montant";
                FeuilXL_Conventions.Cells[1, 9] = "Montant annexe 2";
                FeuilXL_Conventions.Cells[1, 10] = "Envoi collectivité pour signature";
                FeuilXL_Conventions.Cells[1, 11] = "Retour convention";
                FeuilXL_Conventions.Cells[1, 12] = "Signature";
                FeuilXL_Conventions.Cells[1, 13] = "Envoi collectivité convention signée";
                FeuilXL_Conventions.Cells[1, 14] = "Révision envoi";
                FeuilXL_Conventions.Cells[1, 15] = "Révision retour";
                FeuilXL_Conventions.Cells[1, 16] = "Envoi RPQS";
                FeuilXL_Conventions.Cells[1, 17] = "Retour RPQS";
                FeuilXL_Conventions.Cells[1, 18] = "Mandat RPQS";
                FeuilXL_Conventions.Cells[1, 19] = "Envoi annexe 2 marché";
                FeuilXL_Conventions.Cells[1, 20] = "Retour annexe 2 signée";
                FeuilXL_Conventions.Cells[1, 21] = "Mandat SPAC";
                FeuilXL_Conventions.Cells[1, 22] = "Arrêté DUP";
                FeuilXL_Conventions.Cells[1, 23] = "Non recouvré";
                FeuilXL_Conventions.Cells[1, 24] = "Numéro titre";
                FeuilXL_Conventions.Cells[1, 25] = "Montant titre";
                FeuilXL_Conventions.Cells[1, 26] = "Emission titre";
                FeuilXL_Conventions.Cells[1, 27] = "Observations";

                
                // Largeurs
                FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 1], FeuilXL_Conventions.Cells[1, 1]).Columns.ColumnWidth = 5;
                FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 3], FeuilXL_Conventions.Cells[1, 3]).Columns.ColumnWidth = 50;
                FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 4], FeuilXL_Conventions.Cells[1, 4]).Columns.ColumnWidth = 5;
                FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 5], FeuilXL_Conventions.Cells[1, 5]).Columns.ColumnWidth = 15;
                FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 6], FeuilXL_Conventions.Cells[1, 6]).Columns.ColumnWidth = 30;
                FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 7], FeuilXL_Conventions.Cells[1, 7]).Columns.ColumnWidth = 5;
                FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 27], FeuilXL_Conventions.Cells[1, 27]).Columns.ColumnWidth = 30;


                // Range headers
                Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 1], FeuilXL_Conventions.Cells[1, 27]);
                ContoursCellules(RangeHeaders.Cells.Borders);
                RangeHeaders.Font.Bold = true;


                // Récupération et stockage dans une liste des conventions
                List<CConvention> Conventions = new List<CConvention>();
                string req = "SELECT * FROM Convention WHERE '" + DateTime.Today + "' BETWEEN DateDebutConvention AND DateFinConvention ORDER BY idConvention";
                SqlCommand command = new SqlCommand(req, Session.oConn);
                SqlDataReader dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        CConvention Convention = new CConvention();

                        Convention.idConvention = Convert.ToInt32(dataReader["idConvention"]);
                        Convention.idStatut_TypeConvention = Convert.ToInt32(dataReader["idStatut_TypeConvention"]);
                        Convention.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                        Convention.DateDebutPrevision = dataReader["DateDebutConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDebutConvention"]) : DateTime.MinValue;
                        Convention.DateFinPrevision = dataReader["DateFinConvention"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateFinConvention"]) : DateTime.MinValue;
                        Convention.ObservationsConvention = dataReader["ObservationsConvention"].ToString();

                        Conventions.Add(Convention);

                    }
                }
                dataReader.Close();


                int NumLigne = 2;

                // Parcourt des conventions
                foreach (CConvention Convention in Conventions) {

                    int DebutConvention = NumLigne;

                    // Affichage données convention
                    FeuilXL_Conventions.Cells[NumLigne, 1] = Convention.idConvention;
                    FeuilXL_Conventions.Cells[NumLigne, 2] = Convention.CodeCollectivite;
                    FeuilXL_Conventions.Cells[NumLigne, 5] = frmATE55.Statuts[Convention.idStatut_TypeConvention].LibelleStatut;
                    FeuilXL_Conventions.Cells[NumLigne, 6] = Convention.ObservationsConvention;

                    try {
                        FeuilXL_Conventions.Cells[NumLigne, 3] = frmATE55.Collectivites[Convention.CodeCollectivite].NomCollectivite;
                        FeuilXL_Conventions.Cells[NumLigne, 4] = frmATE55.Collectivites[Convention.CodeCollectivite].AgenceEau.Equals("Seine Normandie") ? "SN" : "RM";
                    }
                    catch (KeyNotFoundException) { }


                    // On parcourt les années
                    for (int i = Convention.DateDebutPrevision.Value.Year; i <= Convention.DateFinPrevision.Value.Year; i++) {


                        FeuilXL_Conventions.Cells[NumLigne, 7] = i;


                        // On récupère la LigneConvention correspondant à l'année
                        req = "SELECT * FROM LigneConvention WHERE idConvention = " + Convention.idConvention + " AND AnneeLigneConvention = " + i;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();

                            int idLigneConvention = Convert.ToInt32(dataReader["idLigneConvention"]);

                            FeuilXL_Conventions.Cells[NumLigne, 8] = dataReader["MontantAnneeConvention"].ToString() + "€";
                            FeuilXL_Conventions.Cells[NumLigne, 9] = dataReader["MontantAnnexe2Convention"].ToString() + "€";
                            FeuilXL_Conventions.Cells[NumLigne, 10] = dataReader["DateEnvoiCollectiviteSignatureConvention"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateEnvoiCollectiviteSignatureConvention"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 11] = dataReader["DateRetourConvention"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateRetourConvention"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 12] = dataReader["DateSignatureConvention"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateSignatureConvention"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 13] = dataReader["DateEnvoiCollectiviteSigneConvention"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateEnvoiCollectiviteSigneConvention"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 14] = dataReader["RevisionEnvoiConvention"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["RevisionEnvoiConvention"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 15] = dataReader["RevisionRetourConvention"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["RevisionRetourConvention"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 16] = dataReader["DateEnvoiRPQS"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateEnvoiRPQS"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 17] = dataReader["DateRetourRPQS"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateRetourRPQS"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 18] = dataReader["MandatRPQS"].ToString();
                            FeuilXL_Conventions.Cells[NumLigne, 19] = dataReader["EnvoiAnnexe2_Marche"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["EnvoiAnnexe2_Marche"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 20] = dataReader["RetourAnnexe2_Signee"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["RetourAnnexe2_Signee"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 21] = dataReader["MandatSPAC"].ToString();
                            FeuilXL_Conventions.Cells[NumLigne, 22] = dataReader["DateArreteDUP"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateArreteDUP"]).ToShortDateString();
                            FeuilXL_Conventions.Cells[NumLigne, 23] = dataReader["NonRecouvreConvention"].ToString();
                            FeuilXL_Conventions.Cells[NumLigne, 27] = dataReader["ObservationsConvention"].ToString();

                            dataReader.Close();


                            // Récupération du titre
                            req = "SELECT * FROM Titre WHERE idLigneConvention = " + idLigneConvention + " ORDER BY DateEmissionTitreConvention DESC";
                            command = new SqlCommand(req, Session.oConn);
                            dataReader = command.ExecuteReader();

                            if (dataReader != null && dataReader.HasRows) {
                                dataReader.Read();

                                FeuilXL_Conventions.Cells[NumLigne, 24] = dataReader["NumTitreConvention"].ToString();
                                FeuilXL_Conventions.Cells[NumLigne, 25] = dataReader["MontantTitreConvention"].ToString();
                                FeuilXL_Conventions.Cells[NumLigne, 26] = dataReader["DateEmissionTitreConvention"].GetType().Equals("DBNull") ? "" : Convert.ToDateTime(dataReader["DateEmissionTitreConvention"]).ToShortDateString();

                            }
                            dataReader.Close();

                        }
                        else
                            dataReader.Close();

                        // On incrémente l'index de la ligne
                        NumLigne++;

                    }


                    // Fusion des cellules
                    for (int index = 1; index <= 6; index++)
                        FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[DebutConvention, index], FeuilXL_Conventions.Cells[NumLigne - 1, index]).Cells.Merge();


                    // On colore toute la convention
                    Microsoft.Office.Interop.Excel.Range RangeConvention = FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[DebutConvention, 1], FeuilXL_Conventions.Cells[NumLigne-1, 27]);
                    RangeConvention.Interior.Color = System.Drawing.ColorTranslator.ToOle(frmATE55.couleursTypes[Convention.idStatut_TypeConvention]);
                    ContoursCellules(RangeConvention.Cells.Borders);

                }


                // Collectivités, années et types en gras
                Microsoft.Office.Interop.Excel.Range RangeCollectivites = FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[2, 3], FeuilXL_Conventions.Cells[NumLigne, 3]);
                Microsoft.Office.Interop.Excel.Range RangeAnnees = FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[2, 7], FeuilXL_Conventions.Cells[NumLigne, 7]);
                Microsoft.Office.Interop.Excel.Range RangeTypes = FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[2, 5], FeuilXL_Conventions.Cells[NumLigne, 5]);
                RangeCollectivites.Font.Bold = RangeTypes.Font.Bold = RangeAnnees.Font.Bold = true;

                // On aligne tout
                Microsoft.Office.Interop.Excel.Range RangeTout = FeuilXL_Conventions.get_Range(FeuilXL_Conventions.Cells[1, 1], FeuilXL_Conventions.Cells[NumLigne, 27]);
                RangeTout.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                RangeTout.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                ApplicationXL.Visible = true;

            }
            catch (SqlException exc) {
                for (int i = 0; i < exc.Errors.Count; i++) {
                    errorMessages.Append("Index #" + i + "\n" + "Message: " + exc.Errors[i].Message + "\n" + "LineNumber: " + exc.Errors[i].LineNumber + "\n" + "Source: " + exc.Errors[i].Source + "\n" + "Procedure: " + exc.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                Session.FermerMsgAttente();
            }
            catch (Exception exc) {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
                Session.FermerMsgAttente();
            }

        }

        private static void ContoursCellules(Microsoft.Office.Interop.Excel.Borders borders) {
            borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }

        // Import (décommenter dans frmConventions_Load)
        private void ImporterConventions() {

            string path = Application.StartupPath + @"\Imports\Conventions.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();

                string CodeInterne = "";
                int idConvention = -1;


                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    // Si le code interne est différent du précédent on crée une nouvelle convention
                    if (!CodeInterne.Equals(values[1])) {

                        // On stocke le code interne pour pouvoir lier les lignes ayant le même à la convention
                        CodeInterne = values[1];

                        CConvention Convention = new CConvention(Session);

                        Convention.idStatut_TypeConvention = Convert.ToInt32(values[7]);
                        Convention.CodeCollectivite = values[0];
                        Convention.DateDebut = null;
                        Convention.DateFin = null;
                        // Pour la date de début, on prend la première année de la convention (1er janvier)
                        Convention.DateDebutPrevision = new DateTime(Convert.ToInt32(values[3]), 1, 1);
                        Convention.DateFinPrevision = (DateTime?)null;
                        Convention.ObservationsConvention = "";

                        // Si la création se passe bien on stocke l'id pour s'en resservir
                        if (Convention.Creer())
                            idConvention = Convention.idConvention;
                        else {
                            this.Text = "Convention : " + CodeInterne + "-" + values[3];
                            return;
                        }

                    }


                    // On récupère les infos de la ligne convention
                    CLigneConvention LigneConvention = new CLigneConvention(Session);

                    try {
                        LigneConvention.idConvention = idConvention;
                        LigneConvention.AnneeLigneConvention = Convert.ToInt32(values[3]);
                        LigneConvention.MontantAnneeConvention = values[5].Equals("") ? 0.00M : Convert.ToDecimal(values[5]);
                        LigneConvention.MontantAnnexe2Convention = values[6].Equals("") ? 0.00M : Convert.ToDecimal(values[6]);
                        LigneConvention.DateEnvoiCollectiviteSignatureConvention = values[8].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[8]);
                        LigneConvention.DateRetourConvention = values[9].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[9]);
                        LigneConvention.DateSignatureConvention = values[10].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[10]);
                        LigneConvention.DateEnvoiCollectiviteSigneConvention = values[11].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[11]);
                        LigneConvention.RevisionEnvoiConvention = values[12].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[12]);
                        LigneConvention.RevisionRetourConvention = values[13].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[13]);
                        LigneConvention.DateEnvoiRPQS = values[14].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[14]);
                        LigneConvention.DateRetourRPQS = values[15].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[15]);
                        LigneConvention.MandatRPQS = values[16].Equals("1") ? "oui" : "non";
                        LigneConvention.EnvoiAnnexe2_Marche = values[19].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[19]);
                        LigneConvention.RetourAnnexe2_Marche = values[20].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[20]);
                        LigneConvention.MandatSPAC = values[21].Equals("1") ? "oui" : "non";
                        LigneConvention.ObservationsConvention = values[2];
                        LigneConvention.NonRecouvreConvention = "";
                        LigneConvention.DateArreteDUP = values[22].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[22]);
                    }
                    catch (FormatException e) {
                        this.Text = "LigneConventionException : " + CodeInterne + "-" + values[3];
                        e.ToString();
                        return;
                    }



                    // Si la ligne est créée on met à jour la date de fin de la convention (31 décembre de l'année) et on crée le titre
                    if (LigneConvention.Creer()) {

                        // On récupère les données de la convention
                        CConvention Convention = new CConvention(Session);

                        string req = "SELECT idStatut_TypeConvention,CodeCollectivite,DateDebutConvention FROM Convention WHERE idConvention = " + idConvention;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();

                            Convention.idConvention = idConvention;
                            Convention.idStatut_TypeConvention = Convert.ToInt32(dataReader["idStatut_TypeConvention"]);
                            Convention.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                            Convention.DateDebutPrevision = dataReader["DateDebutConvention"].GetType().Name == "DBNull" ? (DateTime?)null : Convert.ToDateTime(dataReader["DateDebutConvention"]);
                            Convention.DateFinPrevision = new DateTime(LigneConvention.AnneeLigneConvention, 12, 31);
                            Convention.ObservationsConvention = "";

                            dataReader.Close();

                            // On met à jour
                            if (!Convention.Enregistrer()) {
                                this.Text = "ConventionMàj : " + CodeInterne + " - " + LigneConvention.AnneeLigneConvention;
                                return;
                            }

                        }
                        else
                            dataReader.Close();


                        // Création du titre s'il y en a un
                        if (!values[17].Equals("")) {

                            CTitre Titre = new CTitre(Session);

                            Titre.idLigneConvention = LigneConvention.idLigneConvention;
                            Titre.NumTitreConvention = Convert.ToInt32(values[17]);
                            Titre.MontantTitreConvention = values[4].Equals("") ? 0 : Convert.ToDecimal(values[4]);
                            Titre.DateEmissionTitreConvention = values[18].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[18]);

                            // Création
                            if (!Titre.Creer()) {
                                this.Text = "Titre : " + CodeInterne + " - " + LigneConvention.AnneeLigneConvention;
                                return;
                            }

                        }

                    }
                    else {
                        this.Text = "LigneConvention : " + CodeInterne + "-" + values[3];
                        return;
                    }


                }
            }
        }

        private void Modification(object sender, EventArgs e) {
            enregistrerConventionBouton.Enabled = annulerConventionBouton.Enabled = true;
        }

        private void ModificationLigneConvention(object sender, EventArgs e) {
            enregistrerLigneConventionBouton.Enabled = true;
        }

        private void comboThemeConvention_SelectedIndexChanged(object sender, EventArgs e) {

            comboTypeConvention.Items.Clear();
            comboTypeConvention.Enabled = false;

            // On filtre les types en fonction du thème choisi
            int idTheme = Convert.ToInt32(comboThemeConvention.Get_SelectedId());
            if (idTheme == 0) idTheme = -1;

            if (idTheme == (int)eStatut.Assainissement || idTheme == (int)eStatut.DUP) {
                comboTypeConvention.Enabled = true;
                frmATE55.AlimenterComboBox("TypeConvention", comboTypeConvention, null, Session, idTheme);
                ChangerCouleursCombo(comboTypeConvention);
                comboTypeConvention.Set_SelectedId("0");
            }

            this.Rechercher();

        }

        private void comboTypeConvention_SelectedIndexChanged(object sender, EventArgs e) {

            int idType = Convert.ToInt32(comboTypeConvention.Get_SelectedId());
            if (idType == 0) idType = Convert.ToInt32(comboThemeConvention.Get_SelectedId());

            this.Rechercher();
        }

        private void comboChoixTypeConvention_SelectedIndexChanged(object sender, EventArgs e) {
            this.MasquerControlsInutiles(Convert.ToInt32(comboChoixTypeConvention.Get_SelectedId()));
            this.Modification(sender, e);
        }

        private void dataGridViewConventions_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewConventions.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewConventions.ClearSelection();
                dataGridViewConventions.Rows[pos].Selected = true;
            }
        }

        private void dataGridViewConventions_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {

            if (e.StateChanged == DataGridViewElementStates.Selected) {

                if (Loaded) {
                    int idConventionSelectionnee = Convert.ToInt32(e.Row.Cells["idConvention"].Value);

                    if (idConventionCourante != idConventionSelectionnee) {

                        if (enregistrerConventionBouton.Enabled) {
                            if (MessageBox.Show("Voulez-vous SAUVEGARDER les données de la Convention non enregistrées ?\n Attention, les données seront perdues !", "Sauvegarder la Convention courante ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                EnregistrerConvention(idConventionCourante);
                        }

                        AfficherConventionSelectionnee(idConventionSelectionnee);

                    }
                }

            }
        }

        private void numericTitre_KeyPress(object sender, KeyPressEventArgs e) {
            this.ModificationLigneConvention(sender, e);

            // On empêche l'écriture du point
            if (e.KeyChar == 46) {
                e.Handled = true;
                return;
            }

        }

        private void numericMontantTitre_KeyPress(object sender, KeyPressEventArgs e) {
            this.ModificationLigneConvention(sender, e);
        }

        private void numericMontantConvention_KeyPress(object sender, KeyPressEventArgs e) {
            this.ModificationLigneConvention(sender, e);
        }

        private void numericMontantAnnexe_KeyPress(object sender, KeyPressEventArgs e) {
            this.Modification(sender, e);
        }

        private void dateEnvoiSignature_ValueChanged(object sender, EventArgs e) {

            // On donne à la date suivante la date comme valeur minimum
            dateRetourSignature.MinDate = dateEnvoiSignature.Value;
            // On décoche le control car il s'active tout seul lors du changement de valeur
            dateRetourSignature.Checked = dateSignature.Enabled = dateSignature.Checked = dateEnvoiCollectivite.Enabled = dateEnvoiCollectivite.Checked = false;

            dateRetourSignature.Enabled = dateEnvoiSignature.Checked;
            this.ModificationLigneConvention(sender, e);

            if (!dateEnvoiSignature.Checked)
                // On désactive la date suivante
                dateRetourSignature.Checked = dateSignature.Enabled = dateSignature.Checked = dateEnvoiCollectivite.Enabled = dateEnvoiCollectivite.Checked = false;
        }

        private void dateRetourSignature_ValueChanged(object sender, EventArgs e) {

            dateSignature.MinDate = dateRetourSignature.Value;
            dateSignature.Checked = dateEnvoiCollectivite.Enabled = dateEnvoiCollectivite.Checked = false;

            this.ModificationLigneConvention(sender, e);
            dateSignature.Enabled = dateRetourSignature.Checked;

            if (!dateRetourSignature.Checked)
                dateSignature.Checked = dateEnvoiCollectivite.Enabled = dateEnvoiCollectivite.Checked = false;
        }

        private void dateSignature_ValueChanged(object sender, EventArgs e) {
            dateEnvoiCollectivite.MinDate = dateSignature.Value;
            dateEnvoiCollectivite.Checked = dateEnvoiCollectivite.Enabled = false;

            this.ModificationLigneConvention(sender, e);
            dateEnvoiCollectivite.Enabled = dateSignature.Checked;

            if (!dateSignature.Checked)
                dateEnvoiCollectivite.Checked = false;

        }

        private void dateEnvoiRevision_ValueChanged(object sender, EventArgs e) {
            dateRetourRevision.MinDate = dateEnvoiRevision.Value;
            dateRetourRevision.Checked = false;

            dateRetourRevision.Enabled = dateEnvoiRevision.Checked;
            this.ModificationLigneConvention(sender, e);

            if (!dateEnvoiRevision.Checked)
                dateRetourRevision.Checked = false;
        }

        private void dateEnvoiRPQS_ValueChanged(object sender, EventArgs e) {
            dateRetourRPQS.MinDate = dateEnvoiRPQS.Value;
            dateRetourRPQS.Checked = false;

            dateRetourRPQS.Enabled = dateEnvoiRPQS.Checked;
            this.ModificationLigneConvention(sender, e);

            if (!dateEnvoiRPQS.Checked)
                dateRetourRPQS.Checked = false;
        }

        private void dateEnvoiAnnexe_ValueChanged(object sender, EventArgs e) {
            dateRetourAnnexe.MinDate = dateEnvoiAnnexe.Value;
            dateRetourAnnexe.Checked = false;

            dateRetourAnnexe.Enabled = dateEnvoiAnnexe.Checked;
            this.ModificationLigneConvention(sender, e);

            if (!dateEnvoiAnnexe.Checked)
                dateRetourAnnexe.Checked = false;
        }

        private void creerUneConventionToolStripMenuItem_Click(object sender, EventArgs e) {
            this.ViderControls();
            this.EnregistrerConvention(-1);
        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void supprimerLaConventionToolStripMenuItem_Click(object sender, EventArgs e) {

            int idConvention = Convert.ToInt32(dataGridViewConventions.SelectedRows[0].Cells["idConvention"].Value);

            if (MessageBox.Show("Voulez-vous SUPPRIMER la Convention [" + dataGridViewConventions.SelectedRows[0].Cells["CollectiviteConvention"].Value + "] ?\n Attention, la suppression est irréversible !", "Suppression de la Convention ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                CConvention.Supprimer(Session, idConvention);
                
                // On supprime la ligne
                dataGridViewConventions.Rows.RemoveAt(dataGridViewConventions.SelectedRows[0].Index);
                tabConvention.Visible = false;
            }

        }

        private void enregistrerConventionBouton_Click(object sender, EventArgs e) {
            this.EnregistrerConvention(Convert.ToInt32(dataGridViewConventions.SelectedRows[0].Cells["idConvention"].Value));
        }

        private void creerUneNouvelleConventionToolStripMenuItem_Click(object sender, EventArgs e) {
            this.ViderControls();
            this.EnregistrerConvention(-1);
        }

        private void searchConventionBox_KeyUp(object sender, KeyEventArgs e) {
            this.Rechercher();
        }

        private void numericTitre_KeyUp(object sender, KeyEventArgs e) {
            this.ModificationLigneConvention(sender, e);

            if (!numericTitre.Text.Equals("")) {

                // On affiche labelTitre si le numéro de titre entré n'existe pas
                int numeroTitre = Convert.ToInt32(numericTitre.Text);

                string req = "SELECT idTitre FROM Titre WHERE NumTitreConvention = " + numeroTitre + " AND idLigneConvention = " + comboAnneeConvention.Get_SelectedId();
                command = new SqlCommand(req, Session.oConn);
                labelTitre.Visible = command.ExecuteScalar() == null;
                numericTitre.ForeColor = command.ExecuteScalar() == null ? Color.Red : Color.Blue;
            }
        }

        private void annulerConventionBouton_Click(object sender, EventArgs e) {
            AfficherConventionSelectionnee(Convert.ToInt32(dataGridViewConventions.SelectedRows[0].Cells["idConvention"].Value));
        }

        private void comboCollectiviteConvention_SelectedIndexChanged(object sender, EventArgs e) {
            this.Modification(sender, e);

            // On met à jour l'agence de l'eau
            int index = comboCollectiviteConvention.SelectedIndex;
            if (index != -1) {
                string[] s = comboCollectiviteConvention.Items[index].ToString().Split(null);
                string CodeCollectivite = s[s.Length-1];
                labelAgenceEau.Text = "AE : " + frmATE55.Collectivites[CodeCollectivite].AgenceEau;
            }
            else
                labelAgenceEau.Text = "AE : ";

        }

        private void toolStripButtonActualiser_Click(object sender, EventArgs e) {
            idConventionCourante = -1;
            this.AfficherConventions();
        }

        private void comboAnneeConvention_SelectedIndexChanged(object sender, EventArgs e) {
            panelLigneConvention.Visible = true;

            AfficherLigneConvention(Convert.ToInt32(dataGridViewConventions.SelectedRows[0].Cells["idConvention"].Value.ToString()), Convert.ToInt32(comboAnneeConvention.SelectedItem.ToString()));
        }

        private void enregistrerLigneConventionBouton_Click(object sender, EventArgs e) {
            // On récupère l'id de la ligne
            int idConvention = Convert.ToInt32(dataGridViewConventions.SelectedRows[0].Cells["idConvention"].Value.ToString());
            int annee = Convert.ToInt32(comboAnneeConvention.SelectedItem.ToString());

            string req = "SELECT idLigneConvention FROM LigneConvention WHERE idConvention = " + idConvention + " AND AnneeLigneConvention = " + annee;
            command = new SqlCommand(req, Session.oConn);
            if (command.ExecuteScalar() != null)
                EnregistrerLigneConvention(Convert.ToInt32(command.ExecuteScalar()), idConvention, annee);
            else
                EnregistrerLigneConvention(-1, idConvention, annee);
        }

        private void ajouterUneCollectivitéToolStripMenuItem1_Click(object sender, EventArgs e) {
            AjouterCollectivite();
        }

        private void ajouterUneCollectivitéToolStripMenuItem_Click(object sender, EventArgs e) {
            AjouterCollectivite();
        }

        private void supprimerLaCollectivitéToolStripMenuItem_Click(object sender, EventArgs e) {
                      
            int idConvention = Convert.ToInt32(dataGridViewConventions.SelectedRows[0].Cells["idConvention"].Value);
            CConvention_Collectivite.Supprimer(Session, idConvention, dataGridViewCollectivitesConvention.SelectedRows[0].Cells["CodeCollectiviteImpacConvention"].Value.ToString());
            AfficherCollectivites(idConvention);
        }

        private void dataGridViewCollectivitesConvention_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewCollectivitesConvention.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewCollectivitesConvention.ClearSelection();
                dataGridViewCollectivitesConvention.Rows[pos].Selected = true;
            }
        }

        private void comboTitre_SelectedIndexChanged(object sender, EventArgs e) {

            ViderControlsTitre();
            ModificationLigneConvention(sender, e);

            AfficherTitre(Convert.ToInt32(comboTitre.Get_SelectedId()));
        }

        private void checkAfficherConventions_CheckedChanged(object sender, EventArgs e) {
            this.Rechercher();
        }

        private void genererConvention_Click(object sender, EventArgs e) {
            // On récupère l'id de la convention et l'année
            int idConvention = Convert.ToInt32(dataGridViewConventions.SelectedRows[0].Cells["idConvention"].Value);

            this.GenererFichierWordConvention(idConvention);
        }

        private void genererAnnexeBouton_Click(object sender, EventArgs e) {
            this.GenererAnnexe();
        }

        private void extraireLesConventionsActivesToolStripMenuItem_Click(object sender, EventArgs e) {
            GenererExtractionConventions(Session);
        }


    }

}
