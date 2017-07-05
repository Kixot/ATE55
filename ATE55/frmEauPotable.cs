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
    public partial class frmEauPotable : Form {

        private CSession Session;
        private SqlCommand command;
        private SqlDataReader dataReader;

        
        public frmEauPotable() {
            InitializeComponent();
            this.Text = "ATE55 - Eau Potable";
        }

        private void frmEauPotable_Load(object sender, EventArgs e) {

            Session = (CSession)this.Tag;

            foreach (KeyValuePair<string, CCollectivite> Collectivite in frmATE55.Collectivites) {

                string Texte = Collectivite.Value.NomCollectivite + " - " + Collectivite.Key;
                comboCollectiviteCaptage.Items.Add(Texte);
                comboCollectiviteUD.Items.Add(Texte);
                comboCollectivitePopulation.Items.Add(Texte);
                comboCollectiviteVendeuse.Items.Add(Texte);

                // Seulement les communes
                if (Collectivite.Value.NatureCollectivite.Equals("COMMUNE")) {
                    comboCommuneImplantation.Items.Add(Texte);
                    comboCommunesDesservies.Items.Add(Texte);
                }

            }

            toolStripLabel_Session.Visible = true;
            toolStripLabel_Session.Text = Session.Utilisateur.Utilisateur
                + " [" + Session.Utilisateur.CodeDomaine + "/" + Session.Utilisateur.DroitAccess + "]"
                + ", accès précédent le " + Session.Utilisateur.DerniereConnexion.ToString();

            frmATE55.AlimenterComboBox("TypeCaptage", comboTypeCaptage, null, Session, -1);
            frmATE55.AlimenterComboBox("EtatCaptage", comboEtatCaptage, null, Session, -1);
            frmATE55.AlimenterComboBox("EtatArreteDUP", comboEtatArreteDUP, imageListEauPotable, Session, -1);
            frmATE55.AlimenterComboBox("ModeExploitation", comboModeExploitation, null, Session, -1);
            frmATE55.AlimenterComboBox("Chloration", comboChloration, null, Session, -1);
            frmATE55.AlimenterComboBox("PrioriteCaptage", comboCaptagePrioritaire, null, Session, -1);
            frmATE55.AlimenterComboBox("StatutChampAAC", comboAnimationAAC, null, Session, -1);
            frmATE55.AlimenterComboBox("StatutChampAAC", comboProgrammeActionsAAC, null, Session, -1);
            frmATE55.AlimenterComboBox("StatutChampAAC", comboDelimitationAAC, null, Session, -1);
            frmATE55.AlimenterComboBox("StatutChampAAC", comboDiagnostiquePressionsAAC, null, Session, -1);


            //this.ImporterRessources();
            //this.ImporterProcedures();
            //this.ImporterSuivis();
            //this.ImporterPopulations();
            //this.ImporterVenteEau();

            this.AfficherRessources();
            this.AfficherProcedures();
            this.AfficherBilan();

        }

        private void AfficherRessources(int idRessource = -1, int Type = -1) {

            TreeView tv = treeRessources;
            TreeNode NodeCollectivite;
            TreeNode Child;
            TreeNode NodeCaptage;
            TreeNode NodeUD;

            TreeNode NodeSelection = null;

            // Pour afficher un noeud en gras
            Font BoldFont = new Font(tv.Font, FontStyle.Bold);

            tv.Nodes.Clear();
            tv.Refresh();
            
            try {

                tv.BeginUpdate();

                // Récupération des collectivités
                string req = "SELECT DISTINCT CodeCollectivite FROM Ressource_V ORDER BY CodeCollectivite ASC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On stocke les codes des collectivités dans une liste
                List<string> CodesCollectivites = new List<string>();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {
                        CodesCollectivites.Add(dataReader["CodeCollectivite"].ToString());
                    }
                }
                dataReader.Close();

                // Parcourt des collectivités
                foreach (string CodeCollectivite in CodesCollectivites) {
                    
                    try {
                        NodeCollectivite = new TreeNode(frmATE55.Collectivites[CodeCollectivite].NomCollectivite);
                    }
                    catch (KeyNotFoundException) {
                        NodeCollectivite = new TreeNode(CodeCollectivite);
                    }

                    // Collectivités en gras
                    NodeCollectivite.NodeFont = BoldFont;
                    NodeCollectivite.Tag = CodeCollectivite;

                    // Récupération des ressources
                    req = "SELECT idRessource,NomRessource,CodeRessource,Type FROM Ressource_V WHERE CodeCollectivite = " + CodeCollectivite;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {

                        // On crée les noeuds
                        NodeCaptage = new TreeNode("Captages");
                        NodeUD = new TreeNode("UD");

                        NodeCaptage.Tag = "Captage";
                        NodeUD.Tag = "UD";
                        
                        // On parcourt les captages pour les ajouter à l'arbre
                        while (dataReader.Read()) {

                            int id = Convert.ToInt32(dataReader["idRessource"]);
                            int idType = 0;

                            Child = new TreeNode(dataReader["NomRessource"].ToString() + " (" + dataReader["CodeRessource"].ToString() + ")");
                            Child.Tag = id;

                            if (dataReader["Type"].ToString().Equals("Captage")) {
                                Child.ForeColor = frmATE55.Couleurs["Captage"];
                                NodeCaptage.Nodes.Add(Child);
                                idType = (int)eStatut.CAP;
                            }
                            else {
                                Child.ForeColor = frmATE55.Couleurs["UD"];
                                NodeUD.Nodes.Add(Child);
                                idType = (int)eStatut.UD;
                            }

                            // Node à sélectionner
                            if (id == idRessource && Type == idType)
                                NodeSelection = Child;
                            
                        }

                        NodeCaptage.Text = "Captages (" + NodeCaptage.Nodes.Count + ")";
                        NodeUD.Text = "UD (" + NodeUD.Nodes.Count + ")";

                        // On ajoute les noeuds des ressources s'ils ont au moins un enfant
                        if (NodeCaptage.Nodes.Count > 0) NodeCollectivite.Nodes.Add(NodeCaptage);
                        if (NodeUD.Nodes.Count > 0) NodeCollectivite.Nodes.Add(NodeUD);

                    }
                    dataReader.Close();

                    tv.Nodes.Add(NodeCollectivite);
                    NodeCollectivite.Text = NodeCollectivite.Text;

                }

                tv.Sort();

                tv.EndUpdate();

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


            if (NodeSelection != null) {
                treeRessources.SelectedNode = NodeSelection;
                treeRessources.Focus();
            }

        }

        private void AfficherCaptagesProcedure(int idProcedure) {

            DataGridView dgv = dataGridViewCaptages;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                // On récupère les captages liés à la procédure
                string req = "SELECT idCaptage,BSS,CodeCollectivite,NomRessource FROM Captage WHERE idProcedureCaptage = " + idProcedure;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        int i = dgv.Rows.Add();
                        row = dgv.Rows[i];

                        row.Cells["idCaptage"].Value = Convert.ToInt32(dataReader["idCaptage"]);
                        row.Cells["CodeBSS"].Value = dataReader["BSS"].ToString();
                        row.Cells["NomRessourceCaptage"].Value = dataReader["NomRessource"].ToString();

                        string CodeCollectivite = dataReader["CodeCollectivite"].ToString();

                        try {
                            row.Cells["CollectiviteCaptage"].Value = frmATE55.Collectivites[CodeCollectivite].NomCollectivite;
                        }
                        catch (Exception) {
                            row.Cells["CollectiviteCaptage"].Value = CodeCollectivite;
                        }

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


        }

        private void AfficherCaptagesSuivi(int idSuivi) {

            DataGridView dgv = dataGridViewSuivi;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                // On récupère les captages liés au suivi
                string req = "SELECT idCaptage,BSS,CodeCollectivite,NomRessource FROM Captage WHERE idSuiviCaptage = " + idSuivi;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        int i = dgv.Rows.Add();
                        row = dgv.Rows[i];

                        row.Cells["idCaptageSuivi"].Value = Convert.ToInt32(dataReader["idCaptage"]);
                        row.Cells["CodeBSS_Suivi"].Value = dataReader["BSS"].ToString();
                        row.Cells["NomRessourceSuivi"].Value = dataReader["NomRessource"].ToString();

                        string CodeCollectivite = dataReader["CodeCollectivite"].ToString();

                        try {
                            row.Cells["CollectiviteSuivi"].Value = frmATE55.Collectivites[CodeCollectivite].NomCollectivite;
                        }
                        catch (Exception) {
                            row.Cells["CollectiviteSuivi"].Value = CodeCollectivite;
                        }

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

        }

        private void AfficherProcedures(int idCaptage = -1) {

            TreeView tv = treeProcedures;
            TreeNode NodeCaptage;
            TreeNode NodeSelection = null;

            tv.Nodes.Clear();
            tv.Refresh();

            try {

                tv.BeginUpdate();

                // On récupère les captages étant en cours de procédure
                string req = "SELECT idCaptage,BSS,CodeCollectivite,NomRessource FROM Captage WHERE idStatut_EtatArreteDUP = "+(int)eStatut.ArreteEnCours+" ORDER BY CodeCollectivite ASC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        string CodeCollectivite = dataReader["CodeCollectivite"].ToString();

                        string text = "";

                        try {
                            text += frmATE55.Collectivites[CodeCollectivite].NomCollectivite;
                        }
                        catch (Exception) {
                            text += CodeCollectivite;
                        }

                        text += " - " + dataReader["NomRessource"].ToString();
                        text += " (" + dataReader["BSS"].ToString() + ")";

                        NodeCaptage = new TreeNode(text);
                        NodeCaptage.Tag = dataReader["idCaptage"].ToString();
                        NodeCaptage.ForeColor = frmATE55.Couleurs["Captage"];

                        tv.Nodes.Add(NodeCaptage);

                        if (Convert.ToInt32(dataReader["idCaptage"]) == idCaptage)
                            NodeSelection = NodeCaptage;

                    }
                }
                dataReader.Close();

                tv.EndUpdate();

                this.RemplirComboCaptages();

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

            if (NodeSelection != null)
                tv.SelectedNode = NodeSelection;

        }

        private void AfficherPopulationsCaptage(int idCaptage) {

            DataGridView dgv = dataGridViewPopulationCaptage;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                int PopulationTotale = 0;


                // On récupère les populations du captage
                string req = "SELECT * FROM PopulationCaptage WHERE idCaptage = " + idCaptage;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();


                List<CPopulationCaptage> Populations = new List<CPopulationCaptage>();


                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        CPopulationCaptage PopulationCaptage = new CPopulationCaptage();

                        PopulationCaptage.idCaptage = idCaptage;
                        PopulationCaptage.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                        PopulationCaptage.PourcentagePopulation = Convert.ToDecimal(dataReader["PourcentagePopulation"]);

                        Populations.Add(PopulationCaptage);

                    }

                }
                dataReader.Close();


                // On parcourt la liste
                foreach (CPopulationCaptage Population in Populations) {

                    int i = dgv.Rows.Add();
                    row = dgv.Rows[i];

                    row.Cells["idCaptagePopulation"].Value = idCaptage;
                    row.Cells["CodeCollectivitePopulation"].Value = Population.CodeCollectivite;
                    row.Cells["PourcentagePopulation"].Value = Decimal.Round(Population.PourcentagePopulation, 2) + "%";

                    try {
                        row.Cells["CollectivitePopulation"].Value = frmATE55.Collectivites[Population.CodeCollectivite].NomCollectivite;
                    }
                    catch (KeyNotFoundException) {
                        row.Cells["CollectivitePopulation"].Value = Population.CodeCollectivite;
                    }


                    // On récupère la population dans la table éligibilité
                    req = "SELECT PopulationDGF FROM Eligibilite WHERE CodeCollectivite = '" + Population.CodeCollectivite + "' ORDER BY AnneeEligibilite DESC";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        int PopulationCollectivite = Convert.ToInt32((Convert.ToInt32(dataReader["PopulationDGF"]) * Population.PourcentagePopulation) / 100);

                        row.Cells["PopulationCaptage"].Value = PopulationCollectivite;

                        PopulationTotale += PopulationCollectivite;

                    }
                    dataReader.Close();

                }


                // Population totale
                row = dgv.Rows[dgv.Rows.Add()];
                row.Cells["CollectivitePopulation"].Value = "Total";
                row.Cells["PopulationCaptage"].Value = PopulationTotale;

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

        private void AfficherCommunesDesserviesUD(int idUD) {

            DataGridView dgv = dataGridViewCommunesDesservies;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                string req = "SELECT CommunesDesserviesUD.CodeCollectivite, Collectivite_V.NomCollectivite FROM CommunesDesserviesUD LEFT JOIN Collectivite_V ON CommunesDesserviesUD.CodeCollectivite = Collectivite_V.CodeCollectivite WHERE idUD = " + idUD;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        int i = dgv.Rows.Add();
                        row = dgv.Rows[i];

                        row.Cells["idUD"].Value = idUD;
                        row.Cells["CodeCollectiviteCommuneDesservie"].Value = dataReader["CodeCollectivite"].ToString();
                        row.Cells["CollectiviteCommuneDesservie"].Value = dataReader["NomCollectivite"].ToString();

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

        }

        private void AfficherAchatEau(string CodeCollectiviteAcheteur) {

            DataGridView dgv = dataGridViewAchatEau;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                string req = "SELECT CodeCollectiviteVendeur,NomCollectivite FROM VenteEau INNER JOIN Collectivite_V ON VenteEau.CodeCollectiviteVendeur = Collectivite_V.CodeCollectivite WHERE CodeCollectiviteAcheteur = " + CodeCollectiviteAcheteur;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        int i = dgv.Rows.Add();
                        row = dgv.Rows[i];
                        this.Text += "/";
                        row.Cells["CodeCollectiviteAcheteur"].Value = CodeCollectiviteAcheteur;
                        row.Cells["CodeCollectiviteVendeur"].Value = dataReader["CodeCollectiviteVendeur"].ToString();
                        row.Cells["CollectiviteVendeur"].Value = dataReader["NomCollectivite"].ToString();

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

        }

        private void AfficherCaptageSelectionne(int idCaptage) {

            this.ViderControlsProcedures();
            this.ViderControlsSuivi();
            this.ViderControlsAAC();

            try {

                // Récupération du captage
                string req = "SELECT * FROM Captage WHERE idCaptage = " + idCaptage;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                CCaptage Captage;

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    Captage = new CCaptage();

                    Captage.idCaptage = idCaptage;
                    Captage.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                    Captage.idStatut_TypeCaptage = Convert.ToInt32(dataReader["idStatut_TypeCaptage"]);
                    Captage.idStatut_EtatArreteDUP = Convert.ToInt32(dataReader["idStatut_EtatArreteDUP"]);
                    Captage.idStatut_EtatCaptage = Convert.ToInt32(dataReader["idStatut_EtatCaptage"]);
                    Captage.idSuiviCaptage = Convert.ToInt32(dataReader["idSuiviCaptage"]);
                    Captage.idProcedureCaptage = Convert.ToInt32(dataReader["idProcedureCaptage"]);
                    Captage.CodeCollectiviteImplantation = dataReader["CodeCollectiviteImplantation"].ToString();
                    Captage.BSS = dataReader["BSS"].ToString();
                    Captage.NomRessource = dataReader["NomRessource"].ToString();
                    Captage.DateArreteDUP = dataReader["DateArreteDUP"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateArreteDUP"]) : DateTime.MinValue;
                    Captage.DebitAnnuelAutorise = Convert.ToInt32(dataReader["DebitAnnuelAutorise"]);
                    Captage.Observations = dataReader["Observations"].ToString();
                    Captage.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : new DateTime(0);
                    Captage.CreePar = dataReader["CreePar"].ToString();
                    Captage.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : DateTime.MinValue;
                    Captage.ModifiePar = dataReader["ModifiePar"].ToString();

                    dataReader.Close();


                    // Affichage des données
                    comboTypeCaptage.Set_SelectedId(Captage.idStatut_TypeCaptage.ToString());
                    comboEtatArreteDUP.Set_SelectedId(Captage.idStatut_EtatArreteDUP.ToString());
                    comboEtatCaptage.Set_SelectedId(Captage.idStatut_EtatCaptage.ToString());
                    textBSS.Text = Captage.BSS;
                    textNomRessource.Text = Captage.NomRessource;
                    numericDebitAnnuelAutorise.Value = Captage.DebitAnnuelAutorise;
                    textObservationsCaptage.Text = Captage.Observations;
                    infosModifCaptage.Text = Captage.InfosModif();

                    // Date arrêté DUP
                    if (Captage.idStatut_EtatArreteDUP == (int)eStatut.ArreteAttribue) {
                        labelArreteDUP.Visible = dateArreteDUP.Visible = true;

                        if (Captage.DateArreteDUP != DateTime.MinValue) {
                            dateArreteDUP.Checked = true;
                            dateArreteDUP.Value = (DateTime)Captage.DateArreteDUP;
                        }

                    }
                    else {
                        labelArreteDUP.Visible = dateArreteDUP.Visible = false;

                        dateArreteDUP.Value = DateTime.Today;
                        dateArreteDUP.Checked = false;
                    }

                    // Combos collectivités
                    try {
                        int index = comboCollectiviteCaptage.Items.IndexOf(frmATE55.Collectivites[Captage.CodeCollectivite].NomCollectivite + " - " + Captage.CodeCollectivite);
                        comboCollectiviteCaptage.SelectedIndex = index;

                        int indexCommuneImplantation = comboCommuneImplantation.Items.IndexOf(frmATE55.Collectivites[Captage.CodeCollectiviteImplantation].NomCollectivite + " - " + Captage.CodeCollectiviteImplantation);
                        comboCommuneImplantation.SelectedIndex = indexCommuneImplantation;

                        labelAE_Captage.Text = frmATE55.Collectivites[Captage.CodeCollectivite].AgenceEau.Equals("") ? "" : "AE : " + frmATE55.Collectivites[Captage.CodeCollectivite].AgenceEau;
                    }
                    catch (Exception) {
                        comboCollectiviteCaptage.SelectedIndex = -1;
                    }


                    tabCaptage.Visible = true;

                    // Affichage procédure
                    if (Captage.idProcedureCaptage != 0) {
                        this.AfficherProcedure(Captage.idProcedureCaptage, Captage.CodeCollectivite);
                        buttonEAA.Enabled = true;
                    }
                    this.AfficherProcedureAAC(idCaptage);
                    if (Captage.idSuiviCaptage != -1) this.AfficherSuivi(Captage.idSuiviCaptage, Captage.DateArreteDUP);
                    this.AfficherPopulationsCaptage(idCaptage);

                    this.RemplirComboCaptagesSuivi(Captage.CodeCollectivite);

                    enregistrerCaptageBouton.Enabled = annulerCaptageBouton.Enabled = false;
                    enregistrerProcedureBouton.Enabled = annulerProcedureBouton.Enabled = false;
                    enregistrerSuiviBouton.Enabled = annulerSuiviBouton.Enabled = false;

                }
                else {
                    dataReader.Close();
                    tabCaptage.Visible = panelUD.Visible = false;
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

        private void AfficherUDSelectionne(int idUD) {

            this.ViderControlsRessources();

            try {

                // Récupération de l'UD
                string req = "SELECT * FROM UD WHERE idUD = " + idUD;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CUD UD = new CUD();

                    UD.idUD = idUD;
                    UD.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                    UD.UDI_CodeNat = Convert.ToInt32(dataReader["UDI_CodeNat"]);
                    UD.NomRessource = dataReader["NomRessource"].ToString();
                    UD.Population = Convert.ToInt32(dataReader["Population"]);
                    UD.LineaireReseau = Convert.ToDecimal(dataReader["LineaireReseau"]);
                    UD.LineaireReseauxRenouveles = Convert.ToDecimal(dataReader["LineaireReseauxRenouveles"]);
                    UD.Rendement = Convert.ToDecimal(dataReader["Rendement"]);
                    UD.ILP = Convert.ToDecimal(dataReader["ILP"]);
                    UD.ILC = Convert.ToDecimal(dataReader["ILC"]);
                    UD.PrixEauHT = Convert.ToDecimal(dataReader["PrixEauHT"]);
                    UD.PrixEauTTC = Convert.ToDecimal(dataReader["PrixEauTTC"]);
                    UD.VolumeProduit = Convert.ToInt32(dataReader["VolumeProduit"]);
                    UD.VolumeImporte = Convert.ToInt32(dataReader["VolumeImporte"]);
                    UD.VolumeExporte = Convert.ToInt32(dataReader["VolumeExporte"]);
                    UD.VolumeService = Convert.ToInt32(dataReader["VolumeService"]);
                    UD.VolumeConsomme = Convert.ToInt32(dataReader["VolumeConsomme"]);
                    UD.VolumesConsommesComptabilises = Convert.ToInt32(dataReader["VolumesConsommesComptabilises"]);
                    UD.TauxTVA_Facture = Convert.ToDecimal(dataReader["TauxTVA_Facture"]);
                    UD.VoiesNavigables = Convert.ToInt32(dataReader["VoiesNavigables"]);
                    UD.ProtectionRessourceAE = Convert.ToDecimal(dataReader["ProtectionRessourceAE"]);
                    UD.RedevancePollutionAE = Convert.ToDecimal(dataReader["RedevancePollutionAE"]);
                    UD.AutresTaxes = Convert.ToDecimal(dataReader["AutresTaxes"]);
                    UD.idStatut_Chloration = Convert.ToInt32(dataReader["idStatut_Chloration"]);
                    UD.idStatut_ModeExploitation = Convert.ToInt32(dataReader["idStatut_ModeExploitation"]);
                    UD.AutresTraitements = dataReader["AutresTraitements"].ToString();
                    UD.NombrePLV = Convert.ToInt32(dataReader["NombrePLV"]);
                    UD.RestrictionsEv = Convert.ToInt32(dataReader["RestrictionsEv"]);
                    UD.DerogationsEv = Convert.ToInt32(dataReader["DerogationsEv"]);
                    UD.AutresEv = dataReader["AutresEv"].ToString();
                    UD.C_Bacteriologique = Convert.ToDecimal(dataReader["C_Bacteriologique"]);
                    UD.Max_pH = Convert.ToDecimal(dataReader["Max_pH"]);
                    UD.Min_pH = Convert.ToDecimal(dataReader["Min_pH"]);
                    UD.Moy_pH = Convert.ToDecimal(dataReader["Moy_pH"]);
                    UD.Max_TitreAlcalimetrique = Convert.ToDecimal(dataReader["Max_TitreAlcalimetrique"]);
                    UD.Min_TitreAlcalimetrique = Convert.ToDecimal(dataReader["Min_TitreAlcalimetrique"]);
                    UD.Moy_TitreAlcalimetrique = Convert.ToDecimal(dataReader["Moy_TitreAlcalimetrique"]);
                    UD.Max_TitreHydrometrique = Convert.ToDecimal(dataReader["Max_TitreHydrometrique"]);
                    UD.Min_TitreHydrometrique = Convert.ToDecimal(dataReader["Min_TitreHydrometrique"]);
                    UD.Moy_TitreHydrometrique = Convert.ToDecimal(dataReader["Moy_TitreHydrometrique"]);
                    UD.Max_Turbidite = Convert.ToDecimal(dataReader["Max_Turbidite"]);
                    UD.Min_Turbidite = Convert.ToDecimal(dataReader["Min_Turbidite"]);
                    UD.Moy_Turbidite = Convert.ToDecimal(dataReader["Moy_Turbidite"]);
                    UD.Max_Nitrates = Convert.ToDecimal(dataReader["Max_Nitrates"]);
                    UD.Min_Nitrates = Convert.ToDecimal(dataReader["Min_Nitrates"]);
                    UD.Moy_Nitrates = Convert.ToDecimal(dataReader["Moy_Nitrates"]);
                    UD.Moy_PesticidesTotaux = Convert.ToDecimal(dataReader["Moy_PesticidesTotaux"]);
                    UD.Observations = dataReader["Observations"].ToString();
                    UD.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : new DateTime(0);
                    UD.CreePar = dataReader["CreePar"].ToString();
                    UD.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : DateTime.MinValue;
                    UD.ModifiePar = dataReader["ModifiePar"].ToString();


                    dataReader.Close();


                    // Affichage des données
                    // UD
                    numericUDI_CodeNat.Value = UD.UDI_CodeNat;
                    textNomRessourceUD.Text = UD.NomRessource;
                    numericRendement.Value = UD.Rendement;
                    numericILC.Value = UD.ILC;
                    numericILP.Value = UD.ILP;
                    numericPrixHT.Value = UD.PrixEauHT;
                    numericPrixTTC.Value = UD.PrixEauTTC;
                    numericLineaireReseau.Value = UD.LineaireReseau;
                    numericLineaireReseauxRenouveles.Value = UD.LineaireReseauxRenouveles;
                    numericPopulation.Value = UD.Population;
                    textObservationsUD.Text = UD.Observations;
                    infosModifUD.Text = UD.InfosModif();

                    // UD - SISPEA
                    numericVolumeProduit.Value = UD.VolumeProduit;
                    numericVolumeImporte.Value = UD.VolumeImporte;
                    numericVolumeExporte.Value = UD.VolumeExporte;
                    numericVolumeService.Value = UD.VolumeService;
                    numericVolumesConsommesCompta.Value = UD.VolumesConsommesComptabilises;
                    numericVolumeConsommeSansComptage.Value = UD.VolumeConsomme;
                    numericTVA_Applicable.Value = UD.TauxTVA_Facture;
                    numericVoiesNavigables.Value = UD.VoiesNavigables;
                    numericProtectionRessource.Value = UD.ProtectionRessourceAE;
                    numericRedevancePollution.Value = UD.RedevancePollutionAE;
                    numericAutresTaxes.Value = UD.AutresTaxes;

                    // UD - Bilan qualitatif
                    comboModeExploitation.Set_SelectedId(UD.idStatut_ModeExploitation.ToString());
                    comboChloration.Set_SelectedId(UD.idStatut_Chloration.ToString());
                    textAutresTraitements.Text = UD.AutresTraitements;
                    numericNombrePLV.Value = UD.NombrePLV;
                    numericEvRestrictions.Value = UD.RestrictionsEv;
                    numericEvDerogations.Value = UD.DerogationsEv;
                    textEvAutres.Text = UD.AutresEv;
                    numericC_Bacteriologique.Value = UD.C_Bacteriologique;
                    numericMaxpH.Value = UD.Max_pH;
                    numericMinpH.Value = UD.Min_pH;
                    numericMoypH.Value = UD.Moy_pH;
                    numericMaxAlcalimetrique.Value = UD.Max_TitreAlcalimetrique;
                    numericMinAlcalimetrique.Value = UD.Min_TitreAlcalimetrique;
                    numericMoyAlcalimetrique.Value = UD.Moy_TitreAlcalimetrique;
                    numericMaxHydrometrique.Value = UD.Max_TitreHydrometrique;
                    numericMinHydrometrique.Value = UD.Min_TitreHydrometrique;
                    numericMoyHydrometrique.Value = UD.Moy_TitreHydrometrique;
                    numericMaxTurbidite.Value = UD.Max_Turbidite;
                    numericMinTurbidite.Value = UD.Min_Turbidite;
                    numericMoyTurbidite.Value = UD.Moy_Turbidite;
                    numericMaxNitrates.Value = UD.Max_Nitrates;
                    numericMinNitrates.Value = UD.Min_Nitrates;
                    numericMoyNitrates.Value = UD.Moy_Nitrates;
                    numericMoyPesticidesTotaux.Value = UD.Moy_PesticidesTotaux;


                    // Combo collectivité
                    try {
                        int index = comboCollectiviteUD.Items.IndexOf(frmATE55.Collectivites[UD.CodeCollectivite].NomCollectivite + " - " + UD.CodeCollectivite);
                        comboCollectiviteUD.SelectedIndex = index;

                        labelAE_UD.Text = frmATE55.Collectivites[UD.CodeCollectivite].AgenceEau.Equals("") ? "" : "AE : " + frmATE55.Collectivites[UD.CodeCollectivite].AgenceEau;
                    }
                    catch (Exception) {
                        comboCollectiviteUD.SelectedIndex = -1;
                    }

                    this.AfficherCommunesDesserviesUD(idUD);
                    this.AfficherAchatEau(UD.CodeCollectivite);
                    enregistrerUDBouton.Enabled = annulerUDBouton.Enabled = false;
                    panelUD.Visible = true;

                }
                else {
                    dataReader.Close();
                    tabCaptage.Visible = panelUD.Visible = false;
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

        private void AfficherProcedure(int idProcedure, string CodeCollectivite) {

            try {

                // Récupération de la procédure
                string req = "SELECT * FROM ProcedureDUP WHERE idProcedure = " + idProcedure;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CProcedureDUP Procedure = new CProcedureDUP();
                    Procedure.idProcedure = Convert.ToInt32(dataReader["idProcedure"]);
                    Procedure.DateRencontreCollectivite = dataReader["DateRencontreCollectivite"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateRencontreCollectivite"]) : DateTime.MinValue;
                    Procedure.DateDeliberationPhaseTechnique = dataReader["DateDeliberationPhaseTechnique"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDeliberationPhaseTechnique"]) : DateTime.MinValue;
                    Procedure.SubventionCD_PhaseTechnique = dataReader["SubventionCD_PhaseTechnique"].ToString();
                    Procedure.SubventionAE_PhaseTechnique = dataReader["SubventionAE_PhaseTechnique"].ToString();
                    Procedure.DateConsultationBE = dataReader["DateConsultationBE"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateConsultationBE"]) : DateTime.MinValue;
                    Procedure.BE_Retenu = dataReader["BE_Retenu"].ToString();
                    Procedure.DateCommandeBE = dataReader["DateCommandeBE"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateCommandeBE"]) : DateTime.MinValue;
                    Procedure.DateReceptionEtudePrealable = dataReader["DateReceptionEtudePrealable"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReceptionEtudePrealable"]) : DateTime.MinValue;
                    Procedure.DateEnvoiRemarquesEP = dataReader["DateEnvoiRemarquesEP"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEnvoiRemarquesEP"]) : DateTime.MinValue;
                    Procedure.DateEnvoiRemarquesEP_ARS = dataReader["DateEnvoiRemarquesEP_ARS"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEnvoiRemarquesEP_ARS"]) : DateTime.MinValue;
                    Procedure.DateVersionDefinitive = dataReader["DateVersionDefinitive"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateVersionDefinitive"]) : DateTime.MinValue;
                    Procedure.NomHA = dataReader["NomHA"].ToString();
                    Procedure.DateNomination = dataReader["DateNomination"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateNomination"]) : DateTime.MinValue;
                    Procedure.DateReception = dataReader["DateReception"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReception"]) : DateTime.MinValue;
                    Procedure.ReceptionNoticeLoiEau = Convert.ToInt32(dataReader["ReceptionNoticeLoiEau"]);
                    Procedure.DateEstimationFrais = dataReader["DateEstimationFrais"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEstimationFrais"]) : DateTime.MinValue;
                    Procedure.EstimationFraisTransmission = Convert.ToInt32(dataReader["EstimationFraisTransmission"]);
                    Procedure.DateRecevabilite = dataReader["DateRecevabilite"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateRecevabilite"]) : DateTime.MinValue;
                    Procedure.DateDeliberationPhaseAdmin = dataReader["DateDeliberationPhaseAdmin"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDeliberationPhaseAdmin"]) : DateTime.MinValue;
                    Procedure.SubventionCD_PhaseAdmin = dataReader["SubventionCD_PhaseAdmin"].ToString();
                    Procedure.SubventionAE_PhaseAdmin = dataReader["SubventionAE_PhaseAdmin"].ToString();
                    Procedure.DateConsultationServices = dataReader["DateConsultationServices"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateConsultationServices"]) : DateTime.MinValue;
                    Procedure.DateReponseCS = dataReader["DateReponseCS"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReponseCS"]) : DateTime.MinValue;
                    Procedure.DateReunionPublique = dataReader["DateReunionPublique"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReunionPublique"]) : DateTime.MinValue;
                    Procedure.DateConsultationGeometre = dataReader["DateCommandeGeometre"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateCommandeGeometre"]) : DateTime.MinValue;
                    Procedure.GeometreRetenu = dataReader["GeometreRetenu"].ToString();
                    Procedure.DateCommandeGeometre = dataReader["DateCommandeGeometre"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateCommandeGeometre"]) : DateTime.MinValue;
                    Procedure.DateValidationGeometreARS = dataReader["DateValidationGeometreARS"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateValidationGeometreARS"]) : DateTime.MinValue;
                    Procedure.DateDepotPrefecture = dataReader["DateDepotPrefecture"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDepotPrefecture"]) : DateTime.MinValue;
                    Procedure.DateArretePrefectoralDebutEP = dataReader["DateArretePrefectoralDebutEP"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateArretePrefectoralDebutEP"]) : DateTime.MinValue;
                    Procedure.DateDesignationCommissaire = dataReader["DateDesignationCommissaire"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDesignationCommissaire"]) : DateTime.MinValue;
                    Procedure.DateDebutEnquete = dataReader["DateDebutEnquete"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDebutEnquete"]) : DateTime.MinValue;
                    Procedure.DateFinEnquete = dataReader["DateFinEnquete"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateFinEnquete"]) : DateTime.MinValue;
                    Procedure.DateRapportCommissaire = dataReader["DateRapportCommissaire"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateRapportCommissaire"]) : DateTime.MinValue;
                    Procedure.DateCODERST = dataReader["DateCODERST"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateCODERST"]) : DateTime.MinValue;
                    Procedure.DateRAA = dataReader["DateRAA"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateRAA"]) : DateTime.MinValue;
                    Procedure.CoutTotal = Convert.ToDecimal(dataReader["CoutTotal"]);
                    Procedure.CoutEtudePrealable = Convert.ToDecimal(dataReader["CoutEtudePrealable"]);
                    Procedure.CoutHA = Convert.ToDecimal(dataReader["CoutHA"]);
                    Procedure.CoutGeometre = Convert.ToDecimal(dataReader["CoutGeometre"]);
                    Procedure.Observations = dataReader["Observations"].ToString();
                    Procedure.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : new DateTime(0);
                    Procedure.CreePar = dataReader["CreePar"].ToString();
                    Procedure.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : DateTime.MinValue;
                    Procedure.ModifiePar = dataReader["ModifiePar"].ToString();

                    dataReader.Close();


                    // Affichage de la procédure
                    RemplirEtChecker(dateRencontreCollectivite, Procedure.DateRencontreCollectivite);
                    RemplirEtChecker(dateDelibPhaseTechnique, Procedure.DateDeliberationPhaseTechnique);
                    RemplirEtChecker(dateInfosDelibPhaseTechnique, Procedure.DateDeliberationPhaseTechnique);
                    RemplirEtChecker(dateConsultationBE, Procedure.DateConsultationBE);
                    RemplirEtChecker(dateCommande, Procedure.DateCommandeBE);
                    RemplirEtChecker(dateReceptionEtudePrealable, Procedure.DateReceptionEtudePrealable);
                    RemplirEtChecker(dateEnvoiRemarquesEP, Procedure.DateEnvoiRemarquesEP);
                    RemplirEtChecker(dateEnvoiRemarquesEP_ARS, Procedure.DateEnvoiRemarquesEP_ARS);
                    RemplirEtChecker(dateVersionDefinitive, Procedure.DateVersionDefinitive);
                    RemplirEtChecker(dateNomination, Procedure.DateNomination);
                    RemplirEtChecker(dateReception, Procedure.DateReception);
                    RemplirEtChecker(dateEstimationFrais, Procedure.DateEstimationFrais);
                    RemplirEtChecker(dateRecevabilite, Procedure.DateRecevabilite);
                    RemplirEtChecker(dateDelibPhaseAdmin, Procedure.DateDeliberationPhaseAdmin);
                    RemplirEtChecker(dateInfosDelibPhaseAdmin, Procedure.DateDeliberationPhaseAdmin);
                    RemplirEtChecker(dateConsultationServices, Procedure.DateConsultationServices);
                    RemplirEtChecker(dateReponseCS, Procedure.DateReponseCS);
                    RemplirEtChecker(dateConsultationGeometre, Procedure.DateConsultationGeometre);
                    RemplirEtChecker(dateCommandeGeometre, Procedure.DateCommandeGeometre);
                    RemplirEtChecker(dateValidationGeometre, Procedure.DateValidationGeometreARS);
                    RemplirEtChecker(dateDepotPrefecture, Procedure.DateDepotPrefecture);
                    RemplirEtChecker(dateArretePrefectoral, Procedure.DateArretePrefectoralDebutEP);
                    RemplirEtChecker(dateDesignationCommissaire, Procedure.DateDesignationCommissaire);
                    RemplirEtChecker(dateDebutEnquete, Procedure.DateDebutEnquete);
                    RemplirEtChecker(dateFinEnquete, Procedure.DateFinEnquete);
                    RemplirEtChecker(dateRapportCommissaire, Procedure.DateRapportCommissaire);
                    RemplirEtChecker(dateCODERST, Procedure.DateCODERST);
                    RemplirEtChecker(dateRAA, Procedure.DateRAA);


                    textSubventionCD_PhaseTech.Text = Procedure.SubventionCD_PhaseTechnique;
                    textSubventionAE_PhaseTech.Text = Procedure.SubventionAE_PhaseTechnique;
                    textBE_Retenu.Text = Procedure.BE_Retenu;
                    textNomHA.Text = Procedure.NomHA;
                    textSubvAE_Admin.Text = Procedure.SubventionAE_PhaseAdmin;
                    textSubvCD_Admin.Text = Procedure.SubventionCD_PhaseAdmin;
                    textGeometreRetenu.Text = Procedure.GeometreRetenu;
                    textObservationsProcedure.Text = Procedure.Observations;

                    numericCoutTotal.Value = Procedure.CoutTotal;
                    numericCoutHA.Value = Procedure.CoutHA;
                    numericCoutGeometre.Value = Procedure.CoutGeometre;
                    numericCoutEP.Value = Procedure.CoutEtudePrealable;

                    checkReceptionNoticeLoiEau.Checked = Procedure.ReceptionNoticeLoiEau == 1;
                    checkTransmissionFrais.Checked = Procedure.EstimationFraisTransmission == 1;

                    labelInfosModifProcedure.Text = Procedure.InfosModif();


                    // Récupération de l'éligibilité
                    req = "SELECT TOP 1 PotentielFinancier,PopulationDGF,CommunesUrbaines,AnneeEligibilite FROM Eligibilite WHERE CodeCollectivite = " + CodeCollectivite + " ORDER BY AnneeEligibilite DESC";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        CEligibilite Eligibilite = new CEligibilite(Session);

                        Eligibilite.PotentielFinancier = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                        Eligibilite.PopulationDGF = Convert.ToInt32(dataReader["PopulationDGF"]);
                        Eligibilite.CommunesUrbaines = Convert.ToInt32(dataReader["CommunesUrbaines"]);
                        Eligibilite.AnneeEligibilite = Convert.ToInt32(dataReader["AnneeEligibilite"]);
                        Eligibilite.CodeCollectivite = CodeCollectivite;

                        dataReader.Close();

                        // Si éligible
                        if (Eligibilite.Eligible()) {
                            labelEligibiliteProcedure.BackColor = frmATE55.Couleurs["Vert"];
                            labelEligibiliteProcedure.Text = "Eligible";
                        }
                        // Sinon si éligible année précédente
                        else if (Eligibilite.Eligible(DateTime.Today.Year - 1)) {
                            labelEligibiliteProcedure.BackColor = frmATE55.Couleurs["Orange"];
                            labelEligibiliteProcedure.Text = "En transition";
                        }
                        // Sinon
                        else {
                            labelEligibiliteProcedure.BackColor = frmATE55.Couleurs["Rouge"];
                            labelEligibiliteProcedure.Text = "Non éligible";
                        }

                    }
                    else
                        dataReader.Close();


                    // Convention présente
                    req = "SELECT idConvention,DateFinConvention FROM Convention WHERE CodeCollectivite = " + CodeCollectivite + " AND (idStatut_TypeConvention = " + (int)eStatut.Captage_DUP + " OR idStatut_TypeConvention = " + (int)eStatut.DUP_SDAGE + " OR idStatut_TypeConvention = " + (int)eStatut.SDAGE + ")";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        DateTime DateFinConvention = dataReader["DateFinConvention"].GetType().Name == "DNNull" ? DateTime.MinValue : Convert.ToDateTime(dataReader["DateFinConvention"]);

                        checkConventionSATE.Checked = DateFinConvention != DateTime.MinValue && DateFinConvention > DateTime.Today;
                    }
                    dataReader.Close();

                    this.AfficherCaptagesProcedure(idProcedure);

                    enregistrerProcedureBouton.Enabled = annulerProcedureBouton.Enabled = false;
                    buttonAjouterCaptage.Enabled = true;
                    enregistrerProcedureBouton.Text = "Enregistrer";
                
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

        private void AfficherProcedureAAC(int idCaptage) {

            this.ViderControlsAAC();

            try {

                // On récupère la procédure AAC
                string req = "SELECT * FROM ProcedureAAC WHERE idCaptage = " + idCaptage;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    // Texte du bouton
                    enregistrerProcedureAACBouton.Text = "Enregistrer";

                    dataReader.Read();

                    CProcedureAAC ProcedureAAC = new CProcedureAAC();

                    ProcedureAAC.idStatut_EtatDelimitation = Convert.ToInt32(dataReader["idStatut_EtatDelimitation"]);
                    ProcedureAAC.idStatut_EtatDiagnostiquePressions = Convert.ToInt32(dataReader["idStatut_EtatDiagnostiquePressions"]);
                    ProcedureAAC.idStatut_EtatProgrammeActions = Convert.ToInt32(dataReader["idStatut_EtatProgrammeActions"]);
                    ProcedureAAC.idStatut_EtatAnimation = Convert.ToInt32(dataReader["idStatut_EtatAnimation"]);
                    ProcedureAAC.DateRencontreCollectivite = dataReader["DateRencontreCollectivite"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateRencontreCollectivite"]) : DateTime.MinValue;
                    ProcedureAAC.DateDeliberationEngagement = dataReader["DateDeliberationEngagement"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDeliberationEngagement"]) : DateTime.MinValue;
                    ProcedureAAC.DateCommande = dataReader["DateCommande"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateCommande"]) : DateTime.MinValue;
                    ProcedureAAC.BE_Retenu = dataReader["BE_Retenu"].ToString();
                    ProcedureAAC.DateReunionLancement = dataReader["DateReunionLancement"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReunionLancement"]) : DateTime.MinValue;
                    ProcedureAAC.DateReceptionEtude = dataReader["DateReceptionEtude"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReceptionEtude"]) : DateTime.MinValue;
                    ProcedureAAC.DateReunionPublique = dataReader["DateReunionPublique"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateReunionPublique"]) : DateTime.MinValue;
                    ProcedureAAC.DateDeliberationValidation = dataReader["DateDeliberationValidation"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateDeliberationValidation"]) : DateTime.MinValue;
                    ProcedureAAC.DiagnostiquePressions = dataReader["DiagnostiquePressions"].ToString();
                    ProcedureAAC.ProgrammeActions = dataReader["ProgrammeActions"].ToString();
                    ProcedureAAC.Animation = dataReader["Animation"].ToString();
                    ProcedureAAC.Observations = dataReader["Observations"].ToString();

                    dataReader.Close();

               
                    // Affichage des données
                    comboDelimitationAAC.Set_SelectedId(ProcedureAAC.idStatut_EtatDelimitation.ToString());
                    comboDiagnostiquePressionsAAC.Set_SelectedId(ProcedureAAC.idStatut_EtatDiagnostiquePressions.ToString());
                    comboProgrammeActionsAAC.Set_SelectedId(ProcedureAAC.idStatut_EtatProgrammeActions.ToString());
                    comboAnimationAAC.Set_SelectedId(ProcedureAAC.idStatut_EtatAnimation.ToString());

                    RemplirEtChecker(dateRencontreCollectiviteAAC, ProcedureAAC.DateRencontreCollectivite);
                    RemplirEtChecker(dateDelibEngagementAAC, ProcedureAAC.DateDeliberationEngagement);
                    RemplirEtChecker(dateCommandeAAC, ProcedureAAC.DateCommande);
                    RemplirEtChecker(dateReunionLancementAAC, ProcedureAAC.DateReunionLancement);
                    RemplirEtChecker(dateReceptionEtudeAAC, ProcedureAAC.DateReceptionEtude);
                    RemplirEtChecker(dateReunionPubliqueAAC, ProcedureAAC.DateReunionPublique);
                    RemplirEtChecker(dateDelibValidationAAC, ProcedureAAC.DateDeliberationValidation);

                    textBE_RetenuAAC.Text = ProcedureAAC.BE_Retenu;
                    textDiagnostiquePressionsAAC.Text = ProcedureAAC.DiagnostiquePressions;
                    textProgrammeActionsAAC.Text = ProcedureAAC.ProgrammeActions;
                    textAnimationAAC.Text = ProcedureAAC.Animation;
                    textObservationsAAC.Text = ProcedureAAC.Observations;

                }
                else {
                    enregistrerProcedureAACBouton.Text = "Créer";
                    dataReader.Close();
                }

                enregistrerProcedureAACBouton.Enabled = annulerProcedureAACBouton.Enabled = false;

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

        private void AfficherSuivi(int idSuivi, Nullable<DateTime> DateArreteDUP) {

            try {

                // On récupère le suivi
                string req = "SELECT * FROM Suivi WHERE idSuivi = " + idSuivi;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CSuivi Suivi = new CSuivi();

                    Suivi.idSuivi = idSuivi;
                    Suivi.DateVisite1 = dataReader["DateVisite1"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateVisite1"]) : DateTime.MinValue;
                    Suivi.DateEnvoiRapport1 = dataReader["DateEnvoiRapport1"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEnvoiRapport1"]) : DateTime.MinValue;
                    Suivi.DateVisite2 = dataReader["DateVisite2"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateVisite2"]) : DateTime.MinValue;
                    Suivi.DateEnvoiRapport2 = dataReader["DateEnvoiRapport2"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEnvoiRapport2"]) : DateTime.MinValue;
                    Suivi.DateVisite3 = dataReader["DateVisite3"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateVisite3"]) : DateTime.MinValue;
                    Suivi.DateEnvoiRapport3 = dataReader["DateEnvoiRapport3"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["DateEnvoiRapport3"]) : DateTime.MinValue;
                    Suivi.Observations = dataReader["Observations"].ToString();
                    Suivi.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : new DateTime(0);
                    Suivi.CreePar = dataReader["CreePar"].ToString();
                    Suivi.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : DateTime.MinValue;
                    Suivi.ModifiePar = dataReader["ModifiePar"].ToString();

                    dataReader.Close();


                    // Affichage des infos
                    RemplirEtChecker(dateVisite1, Suivi.DateVisite1);
                    RemplirEtChecker(dateEnvoiRapport1, Suivi.DateEnvoiRapport1);
                    RemplirEtChecker(dateVisite2, Suivi.DateVisite2);
                    RemplirEtChecker(dateEnvoiRapport2, Suivi.DateEnvoiRapport2);
                    RemplirEtChecker(dateVisite3, Suivi.DateVisite3);
                    RemplirEtChecker(dateEnvoiRapport3, Suivi.DateEnvoiRapport3);
                    RemplirEtChecker(dateArretePrefectoralSuivi, DateArreteDUP);

                    textObservationsSuivi.Text = Suivi.Observations;

                    infosModifsSuivi.Text = Suivi.InfosModif();

                    enregistrerSuiviBouton.Text = "Enregistrer";
                    ajouterCaptageSuiviBouton.Enabled = true;
                }
                else {
                    dataReader.Close();
                }

                enregistrerSuiviBouton.Enabled = annulerSuiviBouton.Enabled = false;

                this.AfficherCaptagesSuivi(idSuivi);

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

        private void AfficherBilan() {

            int NombreCaptagesProteges = this.AfficherNombreDUP();
            this.AfficherStats(NombreCaptagesProteges);

        }

        private int AfficherNombreDUP() {

            DataGridView dgv = dataGridViewBilanDUP;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            int TotalCaptagesProteges = 0;

            try {


                // On récupère les années
                string req = "SELECT DISTINCT YEAR(DateArreteDUP) AS Annee FROM Captage WHERE DateArreteDUP IS NOT NULL"; // AND idStatut_EtatCaptage = "+(int)eStatut.Captage_AEP;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On stocke les années dans une liste
                List<int> Annees = new List<int>();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        Annees.Add(Convert.ToInt32(dataReader["Annee"]));

                    }

                    dataReader.Close();

                    dgv.Rows.Add(Annees.Count);
                    int i = 0;

                    // Parcourt des années
                    foreach (int Annee in Annees) {

                        row = dgv.Rows[i];
                        i++;

                        // On récupère le nombre d'arrêtés l'année
                        req = "SELECT Count(*) FROM Captage WHERE YEAR(DateArreteDUP) = " + Annee;
                        command = new SqlCommand(req, Session.oConn);

                        row.Cells["AnneeDUP"].Value = Annee;
                        row.Cells["TotalDUP"].Value = command.ExecuteScalar();

                        // On récupère le nombre d'arrêtés non abandonnés de l'année
                        req = "SELECT Count(*) FROM Captage WHERE YEAR(DateArreteDUP) = " + Annee + " AND idStatut_EtatCaptage = " + (int)eStatut.Captage_AEP;
                        command = new SqlCommand(req, Session.oConn);

                        row.Cells["TotalDUP_SansAbandonnes"].Value = command.ExecuteScalar();

                        TotalCaptagesProteges += Convert.ToInt32(command.ExecuteScalar());

                    }

                }
                else
                    dataReader.Close();

                labelNbCaptagesProteges.Text = "Nombre de captages protégés : " + TotalCaptagesProteges;

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

            return TotalCaptagesProteges;

        }

        private void AfficherStats(int NombreCaptagesProteges) {

            // On récupère le nombre de captages "AEP"
            string req = "SELECT Count(*) FROM Captage WHERE idStatut_EtatCaptage = " + (int)eStatut.Captage_AEP;
            command = new SqlCommand(req, Session.oConn);

            int TotalCapAEP = Convert.ToInt32(command.ExecuteScalar());
            int NombreCaptagesNonProteges = TotalCapAEP - NombreCaptagesProteges;
            decimal PourcentageCaptagesNonProteges = Decimal.Round((decimal)NombreCaptagesProteges / (decimal)TotalCapAEP * 100, 2);

            // Nombre de captages dont l'arrêté est en cours
            req = "SELECT Count(*) FROM Captage WHERE idStatut_EtatArreteDUP = " + (int)eStatut.ArreteEnCours;
            command = new SqlCommand(req, Session.oConn);

            int NombreProceduresEnCours = Convert.ToInt32(command.ExecuteScalar());
            int NombreProceduresNonEntamees = NombreCaptagesNonProteges - NombreProceduresEnCours;

            int PopulationTotaleProtegee = 0;
            int PopulationTotaleNonProtegee = 0;

            // Population des captages portégés par DUP (état AEP)
            req = "SELECT PourcentagePopulation,PopulationDGF,DateArreteDUP FROM PopulationCaptage INNER JOIN Captage ON PopulationCaptage.idCaptage = Captage.idCaptage INNER JOIN Eligibilite ON PopulationCaptage.CodeCollectivite = Eligibilite.CodeCollectivite WHERE AnneeEligibilite = (SELECT MAX(AnneeEligibilite) FROM Eligibilite) AND idStatut_EtatCaptage = " + (int)eStatut.Captage_AEP;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    int PopulationCollectivite = Convert.ToInt32((Convert.ToInt32(dataReader["PopulationDGF"]) * Convert.ToDecimal(dataReader["PourcentagePopulation"])) / 100);

                    if (dataReader["DateArreteDUP"].GetType().Name != "DBNull")
                        PopulationTotaleProtegee += PopulationCollectivite;
                    else
                        PopulationTotaleNonProtegee += PopulationCollectivite;

                }
            }
            dataReader.Close();


            decimal PrctPopAlimentee = Decimal.Round((decimal)PopulationTotaleProtegee / (decimal)(PopulationTotaleProtegee + PopulationTotaleNonProtegee) * (decimal)100);

            labelNombreCaptagesNonProteges.Text = "Nombre de captages non protégés : " + NombreCaptagesNonProteges;
            labelTotalCapAEP.Text = "Total cap AEP : " + TotalCapAEP;
            labelNombreProceduresEnCours.Text = "Nombre de procédures DUP en cours : " + NombreProceduresEnCours;
            labelNombreProceduresNonEntamees.Text = "Nombre de procédures DUP non entamées : " + NombreProceduresNonEntamees;
            labelPopProtegee.Text = "Population protégée : " + PopulationTotaleProtegee;
            labelPrctCaptagesNonProteges.Text = "Pourcentage de captages protégés : " + PourcentageCaptagesNonProteges + "%";
            labelPopAlimenteeCaptageProtegee.Text = "Pourcentage de population alimentée par un captage protégé : " + PrctPopAlimentee + "%";
            labelPopNonProtegee.Text = "Pourcentage de population non protégée : " + (100 - PrctPopAlimentee) + "%";

        }

        private void UpdateNodeRessource(int idRessource, TreeNode Node, string TypeRessource, Boolean nouvelle = false) {
            
            if (Node != null && idRessource != -1) {

                try {

                    // Récupération de la ressource
                    string req = "SELECT * FROM Ressource_V WHERE idRessource = " + idRessource+" AND Type = '"+TypeRessource+"'";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        CRessource Ressource = new CRessource();

                        Ressource.idRessource = Convert.ToInt32(dataReader["idRessource"]);
                        Ressource.CodeRessource = dataReader["CodeRessource"].ToString();
                        Ressource.NomRessource = dataReader["NomRessource"].ToString();

                        dataReader.Close();

                        // Affichage
                        Node.Tag = Ressource.idRessource;
                        Node.Text = Ressource.NomRessource + " (" + Ressource.CodeRessource + ")";


                        // Couleur du noeud
                        Node.ForeColor = TypeRessource.Equals("Captage") ? frmATE55.Couleurs["Captage"] : frmATE55.Couleurs["UD"];

                        // On sélectionne si c'est une nouvelle ressource
                        if (nouvelle) {
                            treeRessources.SelectedNode = Node;
                            treeRessources.Focus();
                        }

                        // On affiche en fonction de la ressource
                        if (TypeRessource.Equals("Captage")) {
                            this.AfficherCaptageSelectionne(idRessource);
                            tabCaptage.Visible = true;
                        }
                        else {
                            this.AfficherUDSelectionne(idRessource);
                            panelUD.Visible = true;
                        }

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

        }

        private void Rechercher() {

            string Recherche = textRecherche.Text.ToLower();

            foreach (TreeNode Node in treeRessources.Nodes) {

                TreeNodeCollection NodeEnfant = Node.Nodes[0].Nodes;

                foreach (TreeNode NodeCaptage in NodeEnfant) {

                    string NomRessource = NodeCaptage.Text.ToLower();

                    if (NomRessource.Contains(Recherche)) {
                        treeRessources.SelectedNode = NodeCaptage;
                        treeRessources.Focus();
                        textRecherche.Focus();
                        return;
                    }

                }

                if (Node.Nodes.Count == 2) {

                    NodeEnfant = Node.Nodes[1].Nodes;

                    foreach (TreeNode NodeCaptage in NodeEnfant) {

                        string NomRessource = NodeCaptage.Text.ToLower();

                        if (NomRessource.Contains(Recherche)) {
                            treeRessources.SelectedNode = NodeCaptage;
                            treeRessources.Focus();
                            textRecherche.Focus();
                            return;
                        }

                    }

                }

            }

        }

        private void EnregistrerCaptage(int idCaptage) {

            // Récupération du code collectivité
            string CodeCollectivite = "55000";

            if (treeRessources.SelectedNode.Parent == null)
                CodeCollectivite = treeRessources.SelectedNode.Tag.ToString();
            else if (treeRessources.SelectedNode.Tag.ToString().Equals("Captage"))
                CodeCollectivite = treeRessources.SelectedNode.Parent.Tag.ToString();
            else
                CodeCollectivite = treeRessources.SelectedNode.Parent.Parent.Tag.ToString();


            if (idCaptage == -1)
                this.ViderControlsRessources();

            CCaptage Captage = new CCaptage(Session);

            Captage.idCaptage = idCaptage;
            Captage.idStatut_TypeCaptage = Convert.ToInt32(comboTypeCaptage.Get_SelectedId());
            Captage.idStatut_EtatArreteDUP = Convert.ToInt32(comboEtatArreteDUP.Get_SelectedId());
            Captage.idStatut_EtatCaptage = Convert.ToInt32(comboEtatCaptage.Get_SelectedId());
            Captage.idSuiviCaptage = -1;
            Captage.BSS = textBSS.Text;
            Captage.NomRessource = textNomRessource.Text;
            Captage.DateArreteDUP = dateArreteDUP.Checked ? Convert.ToDateTime(dateArreteDUP.Value) : (DateTime?)null;
            Captage.DebitAnnuelAutorise = (int)numericDebitAnnuelAutorise.Value;
            Captage.Observations = textObservationsCaptage.Text;

            // Code collectivité
            if (comboCollectiviteCaptage.SelectedIndex != -1 && idCaptage != -1) {
                String[] s = comboCollectiviteCaptage.SelectedItem.ToString().Split(null);
                Captage.CodeCollectivite = s[s.Length - 1];
            }
            else
                Captage.CodeCollectivite = "55000";

            // Code commune d'implantation
            if (comboCommuneImplantation.SelectedIndex != -1 && idCaptage != -1) {
                String[] s = comboCommuneImplantation.SelectedItem.ToString().Split(null);
                Captage.CodeCollectiviteImplantation = s[s.Length - 1];
            }
            else
                Captage.CodeCollectiviteImplantation = "55000";


            if (idCaptage == -1)
                Captage.CodeCollectivite = CodeCollectivite;


            if (idCaptage != -1) {
                // Mise à jour
                if (Captage.Enregistrer()) {

                    // On récupère le noeud du captage
                    UpdateNodeRessource(idCaptage, treeRessources.SelectedNode, "Captage");

                }

            }
            else {
                // Création
                if (Captage.Creer())
                    this.AfficherRessources(Captage.idCaptage, (int)eStatut.CAP);

            }

        }

        private void EnregistrerUD(int idUD) {

            // Récupération du code collectivité
            string CodeCollectivite = "55000";


            if (treeRessources.SelectedNode.Parent == null)
                CodeCollectivite = treeRessources.SelectedNode.Tag.ToString();
            if (treeRessources.SelectedNode.Tag.ToString().Equals("UD"))
                CodeCollectivite = treeRessources.SelectedNode.Parent.Tag.ToString();
            else
                CodeCollectivite = treeRessources.SelectedNode.Parent.Parent.Tag.ToString();



            if (idUD == -1)
                this.ViderControlsRessources();

            CUD UD = new CUD(Session);

            UD.idUD = idUD;
            UD.idStatut_ModeExploitation = Convert.ToInt32(comboModeExploitation.Get_SelectedId());
            UD.idStatut_Chloration = Convert.ToInt32(comboChloration.Get_SelectedId());
            UD.UDI_CodeNat = (int)numericUDI_CodeNat.Value;
            UD.NomRessource = textNomRessourceUD.Text;
            UD.Population = (int)numericPopulation.Value;
            UD.VolumeProduit = (int)numericVolumeProduit.Value;
            UD.VolumeImporte = (int)numericVolumeImporte.Value;
            UD.VolumeExporte = (int)numericVolumeExporte.Value;
            UD.LineaireReseau = numericLineaireReseau.Value;
            UD.LineaireReseauxRenouveles = numericLineaireReseauxRenouveles.Value;
            UD.TauxTVA_Facture = numericTVA_Applicable.Value;
            UD.VoiesNavigables = (int)numericVoiesNavigables.Value;
            UD.ProtectionRessourceAE = numericProtectionRessource.Value;
            UD.RedevancePollutionAE = numericRedevancePollution.Value;
            UD.AutresTaxes = numericAutresTaxes.Value;
            UD.VolumeService = (int)numericVolumeService.Value;
            UD.VolumeConsomme = (int)numericVolumeConsommeSansComptage.Value;
            UD.VolumesConsommesComptabilises = (int)numericVolumesConsommesCompta.Value;
            UD.Rendement = numericRendement.Value;
            UD.ILP = numericILP.Value;
            UD.ILC = numericILC.Value;
            UD.PrixEauHT = numericPrixHT.Value;
            UD.PrixEauTTC = numericPrixTTC.Value;
            UD.AutresTraitements = textAutresTraitements.Text;
            UD.NombrePLV = (int)numericNombrePLV.Value;
            UD.RestrictionsEv = (int)numericEvRestrictions.Value;
            UD.DerogationsEv = (int)numericEvDerogations.Value;
            UD.AutresEv = textEvAutres.Text;
            UD.C_Bacteriologique = numericC_Bacteriologique.Value;
            UD.Max_pH = numericMaxpH.Value;
            UD.Min_pH = numericMinpH.Value;
            UD.Moy_pH = numericMoypH.Value;
            UD.Max_TitreAlcalimetrique = numericMaxAlcalimetrique.Value;
            UD.Min_TitreAlcalimetrique = numericMinAlcalimetrique.Value;
            UD.Moy_TitreAlcalimetrique = numericMoyAlcalimetrique.Value;
            UD.Max_TitreHydrometrique = numericMaxHydrometrique.Value;
            UD.Min_TitreHydrometrique = numericMinHydrometrique.Value;
            UD.Moy_TitreHydrometrique = numericMoyHydrometrique.Value;
            UD.Max_Turbidite = numericMaxTurbidite.Value;
            UD.Min_Turbidite = numericMinTurbidite.Value;
            UD.Moy_Turbidite = numericMoyTurbidite.Value;
            UD.Max_Nitrates = numericMaxNitrates.Value;
            UD.Min_Nitrates = numericMinNitrates.Value;
            UD.Moy_Nitrates = numericMoyNitrates.Value;
            UD.Moy_PesticidesTotaux = numericMoyPesticidesTotaux.Value;
            UD.Observations = textObservationsUD.Text;

            // Code collectivité
            if (comboCollectiviteUD.SelectedIndex != -1 && idUD != -1) {
                String[] s = comboCollectiviteUD.SelectedItem.ToString().Split(null);
                UD.CodeCollectivite = s[s.Length - 1];
            }
            else
                UD.CodeCollectivite = "55000";

            if (idUD == -1)
                UD.CodeCollectivite = CodeCollectivite;

            if (idUD != -1) {
                // Mise à jour
                if (UD.Enregistrer()) {

                    // On récupère l'index du captage
                    UpdateNodeRessource(idUD, treeRessources.SelectedNode, "UD");

                }

            }
            else {
                // Création
                if (UD.Creer())
                    this.AfficherRessources(UD.idUD, (int)eStatut.UD);

            }


        }

        private void EnregistrerProcedure(int idCaptage) {

            // On récupère l'id de la procédure
            string req = "SELECT idProcedureCaptage FROM Captage WHERE idCaptage = " + idCaptage;
            command = new SqlCommand(req, Session.oConn);

            int idProcedure = Convert.ToInt32(command.ExecuteScalar());

            CProcedureDUP Procedure = new CProcedureDUP(Session);

            Procedure.idProcedure = idProcedure;
            Procedure.DateRencontreCollectivite = dateRencontreCollectivite.Checked ? dateRencontreCollectivite.Value : (DateTime?)null;
            Procedure.DateDeliberationPhaseTechnique = dateDelibPhaseTechnique.Checked ? dateDelibPhaseTechnique.Value : (DateTime?)null;
            Procedure.SubventionCD_PhaseTechnique = textSubventionCD_PhaseTech.Text;
            Procedure.SubventionAE_PhaseTechnique = textSubventionAE_PhaseTech.Text;
            Procedure.DateConsultationBE = dateConsultationBE.Checked ? dateConsultationBE.Value : (DateTime?)null;
            Procedure.BE_Retenu = textBE_Retenu.Text;
            Procedure.DateCommandeBE = dateCommande.Checked ? dateCommande.Value : (DateTime?)null;
            Procedure.DateReceptionEtudePrealable = dateReceptionEtudePrealable.Checked ? dateReceptionEtudePrealable.Value : (DateTime?)null;
            Procedure.DateEnvoiRemarquesEP = dateEnvoiRemarquesEP.Checked ? dateEnvoiRemarquesEP.Value : (DateTime?)null;
            Procedure.DateEnvoiRemarquesEP_ARS = dateEnvoiRemarquesEP_ARS.Checked ? dateEnvoiRemarquesEP_ARS.Value : (DateTime?)null;
            Procedure.DateVersionDefinitive = dateVersionDefinitive.Checked ? dateVersionDefinitive.Value : (DateTime?)null;
            Procedure.NomHA = textNomHA.Text;
            Procedure.DateNomination = dateNomination.Checked ? dateNomination.Value : (DateTime?)null;
            Procedure.DateReception = dateReception.Checked ? dateReception.Value : (DateTime?)null;
            Procedure.ReceptionNoticeLoiEau = checkReceptionNoticeLoiEau.Checked ? 1 : 0;
            Procedure.DateEstimationFrais = dateEstimationFrais.Checked ? dateEstimationFrais.Value : (DateTime?)null;
            Procedure.EstimationFraisTransmission = checkTransmissionFrais.Checked ? 1 : 0;
            Procedure.DateRecevabilite = dateRecevabilite.Checked ? dateRecevabilite.Value : (DateTime?)null;
            Procedure.DateDeliberationPhaseAdmin = dateDelibPhaseAdmin.Checked ? dateDelibPhaseAdmin.Value : (DateTime?)null;
            Procedure.SubventionCD_PhaseAdmin = textSubvCD_Admin.Text;
            Procedure.SubventionAE_PhaseAdmin = textSubvAE_Admin.Text;
            Procedure.DateConsultationServices = dateConsultationServices.Checked ? dateConsultationServices.Value : (DateTime?)null;
            Procedure.DateReponseCS = dateReponseCS.Checked ? dateReponseCS.Value : (DateTime?)null;
            Procedure.DateReunionPublique = dateReunionPublique.Checked ? dateReunionPublique.Value : (DateTime?)null;
            Procedure.DateConsultationGeometre = dateConsultationGeometre.Checked ? dateConsultationGeometre.Value : (DateTime?)null;
            Procedure.GeometreRetenu = textGeometreRetenu.Text;
            Procedure.DateCommandeGeometre = dateCommandeGeometre.Checked ? dateCommandeGeometre.Value : (DateTime?)null;
            Procedure.DateValidationGeometreARS = dateValidationGeometre.Checked ? dateValidationGeometre.Value : (DateTime?)null;
            Procedure.DateDepotPrefecture = dateDepotPrefecture.Checked ? dateDepotPrefecture.Value : (DateTime?)null;
            Procedure.DateArretePrefectoralDebutEP = dateArretePrefectoral.Checked ? dateArretePrefectoral.Value : (DateTime?)null;
            Procedure.DateDesignationCommissaire = dateDesignationCommissaire.Checked ? dateDesignationCommissaire.Value : (DateTime?)null;
            Procedure.DateDebutEnquete = dateDebutEnquete.Checked ? dateDebutEnquete.Value : (DateTime?)null;
            Procedure.DateFinEnquete = dateFinEnquete.Checked ? dateFinEnquete.Value : (DateTime?)null;
            Procedure.DateRapportCommissaire = dateRapportCommissaire.Checked ? dateRapportCommissaire.Value : (DateTime?)null;
            Procedure.DateCODERST = dateCODERST.Checked ? dateCODERST.Value : (DateTime?)null;
            Procedure.DateRAA = dateRAA.Checked ? dateRAA.Value : (DateTime?)null;
            Procedure.CoutTotal = numericCoutTotal.Value;
            Procedure.CoutEtudePrealable = numericCoutEP.Value;
            Procedure.CoutHA = numericCoutHA.Value;
            Procedure.CoutGeometre = numericCoutGeometre.Value;
            Procedure.Observations = textObservationsProcedure.Text;

            // Mise à jour
            if (enregistrerProcedureBouton.Text.Equals("Enregistrer")) {
                if (Procedure.Enregistrer())
                    this.AfficherCaptageSelectionne(idCaptage);
            }
            // Création
            else {
                if (Procedure.Creer()) {
                    CCaptage.EnregistrerProcedure(idCaptage, Procedure.idProcedure, Session);
                    this.AfficherCaptageSelectionne(idCaptage);
                }
            }

        }

        private void EnregistrerProcedureAAC(int idCaptage) {

            // On récupère l'id de la procédure
            string req = "SELECT idProcedureAAC FROM ProcedureAAC WHERE idCaptage = " + idCaptage;
            command = new SqlCommand(req, Session.oConn);

            int idProcedure = Convert.ToInt32(command.ExecuteScalar());

            CProcedureAAC ProcedureAAC = new CProcedureAAC(Session);

            ProcedureAAC.idCaptage = idCaptage;
            ProcedureAAC.idProcedureAAC = idProcedure;
            ProcedureAAC.idStatut_EtatDelimitation = Convert.ToInt32(comboDelimitationAAC.Get_SelectedId());
            ProcedureAAC.idStatut_EtatDiagnostiquePressions = Convert.ToInt32(comboDiagnostiquePressionsAAC.Get_SelectedId());
            ProcedureAAC.idStatut_EtatProgrammeActions = Convert.ToInt32(comboProgrammeActionsAAC.Get_SelectedId());
            ProcedureAAC.idStatut_EtatAnimation = Convert.ToInt32(comboAnimationAAC.Get_SelectedId());
            ProcedureAAC.DateRencontreCollectivite = dateRencontreCollectiviteAAC.Checked ? dateRencontreCollectiviteAAC.Value : (DateTime?)null;
            ProcedureAAC.DateDeliberationEngagement = dateDelibEngagementAAC.Checked ? dateDelibEngagementAAC.Value : (DateTime?)null;
            ProcedureAAC.DateCommande = dateCommandeAAC.Checked ? dateCommandeAAC.Value : (DateTime?)null;
            ProcedureAAC.BE_Retenu = textBE_RetenuAAC.Text;
            ProcedureAAC.DateReunionLancement = dateReunionLancementAAC.Checked ? dateReunionLancementAAC.Value : (DateTime?)null;
            ProcedureAAC.DateReceptionEtude = dateReceptionEtudeAAC.Checked ? dateReceptionEtudeAAC.Value : (DateTime?)null;
            ProcedureAAC.DateReunionPublique = dateReunionPubliqueAAC.Checked ? dateReunionPubliqueAAC.Value : (DateTime?)null;
            ProcedureAAC.DateDeliberationValidation = dateDelibValidationAAC.Checked ? dateDelibValidationAAC.Value : (DateTime?)null;
            ProcedureAAC.DiagnostiquePressions = textDiagnostiquePressionsAAC.Text;
            ProcedureAAC.ProgrammeActions = textProgrammeActionsAAC.Text;
            ProcedureAAC.Animation = textAnimationAAC.Text;
            ProcedureAAC.Observations = textObservationsAAC.Text;

            // On enregistre
            if (enregistrerProcedureAACBouton.Text.Equals("Enregistrer")) {
                if (ProcedureAAC.Enregistrer())
                    this.AfficherCaptageSelectionne(idCaptage);
            }
            else {
                if (ProcedureAAC.Creer())
                    this.AfficherCaptageSelectionne(idCaptage);
            }
        
        }

        private void EnregistrerSuivi(int idCaptage) {

            // On récupère l'id du suivi
            string req = "SELECT idSuiviCaptage FROM Captage WHERE idCaptage = " + idCaptage;
            command = new SqlCommand(req, Session.oConn);

            int idSuivi = Convert.ToInt32(command.ExecuteScalar());

            CSuivi Suivi = new CSuivi(Session);

            Suivi.idSuivi = idSuivi;
            Suivi.DateVisite1 = dateVisite1.Checked ? dateVisite1.Value : (DateTime?)null;
            Suivi.DateEnvoiRapport1 = dateEnvoiRapport1.Checked ? dateEnvoiRapport1.Value : (DateTime?)null;
            Suivi.DateVisite2 = dateVisite2.Checked ? dateVisite2.Value : (DateTime?)null;
            Suivi.DateEnvoiRapport2 = dateEnvoiRapport2.Checked ? dateEnvoiRapport2.Value : (DateTime?)null;
            Suivi.DateVisite3 = dateVisite3.Checked ? dateVisite3.Value : (DateTime?)null;
            Suivi.DateEnvoiRapport3 = dateEnvoiRapport3.Checked ? dateEnvoiRapport3.Value : (DateTime?)null;
            Suivi.Observations = textObservationsSuivi.Text;

            if (enregistrerSuiviBouton.Text.Equals("Enregistrer")) {
                if (Suivi.Enregistrer())
                    this.AfficherCaptageSelectionne(idCaptage);
            }
            else {
                if (Suivi.Creer()) {
                    CCaptage.EnregistrerSuivi(idCaptage, Suivi.idSuivi, Session);
                    this.AfficherCaptageSelectionne(idCaptage);
                }
            }
        }

        private void RemplirComboCaptages() {

            comboCaptages.Items.Clear();

            // On récupère les captages n'ayant pas de procédure
            string req = "SELECT idCaptage,BSS,CodeCollectivite,NomRessource FROM Captage WHERE idProcedureCaptage = 0";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    string label = "";

                    string CodeCollectivite = dataReader["CodeCollectivite"].ToString();

                    try {
                        label += frmATE55.Collectivites[CodeCollectivite].NomCollectivite;
                    }
                    catch (Exception) {
                        label += CodeCollectivite;
                    }

                    label += " - " + dataReader["NomRessource"].ToString();
                    label += " (" + dataReader["BSS"].ToString() + ")";

                    CustomComboBox.CustomComboBoxItem Item = new CustomComboBox.CustomComboBoxItem(dataReader["idCaptage"].ToString(), label, Color.Black, true);
                    comboCaptages.Items.Add(Item);

                }
            }
            dataReader.Close();

        }

        private void RemplirComboCaptagesSuivi(string CodeCollectiviteCaptage) {

            comboCaptagesSuivi.Items.Clear();

            // On récupère les captages de la même collectivité que le suivi et n'ayant pas de suivi
            string req = "SELECT idCaptage,BSS,CodeCollectivite,NomRessource FROM Captage WHERE CodeCollectivite = '"+CodeCollectiviteCaptage+"' AND idSuiviCaptage = -1";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    string label = "";

                    string CodeCollectivite = dataReader["CodeCollectivite"].ToString();

                    try {
                        label += frmATE55.Collectivites[CodeCollectivite].NomCollectivite;
                    }
                    catch (Exception) {
                        label += CodeCollectivite;
                    }

                    label += " - " + dataReader["NomRessource"].ToString();
                    label += " (" + dataReader["BSS"].ToString() + ")";

                    CustomComboBox.CustomComboBoxItem Item = new CustomComboBox.CustomComboBoxItem(dataReader["idCaptage"].ToString(), label, Color.Black, true);
                    comboCaptagesSuivi.Items.Add(Item);

                }
            }
            dataReader.Close();

        }

        private void ViderControlsRessources() {

            // Captage
            comboCollectiviteCaptage.SelectedIndex = comboCommuneImplantation.SelectedIndex = comboCollectivitePopulation.SelectedIndex = comboCollectiviteVendeuse.SelectedIndex = -1;
            comboTypeCaptage.SelectedIndex = comboEtatArreteDUP.SelectedIndex = comboEtatCaptage.SelectedIndex = comboCollectiviteCaptage.SelectedIndex = 0;
            numericPopulationAjout.Value = numericDebitAnnuelAutorise.Value = 0;
            dateArreteDUP.Value = DateTime.Today;
            dateArreteDUP.Checked = dateArreteDUP.Visible = labelArreteDUP.Visible = ajouterPopulationButton.Enabled = buttonAjouterVendeur.Enabled = false;

            // UD
            comboCollectiviteUD.SelectedIndex = comboCommunesDesservies.SelectedIndex = -1;
            textNomRessourceUD.Text = textObservationsUD.Text = infosModifCaptage.Text = infosModifUD.Text = labelAE_UD.Text = "";
            numericUDI_CodeNat.Value = numericILP.Value = numericILC.Value = numericRendement.Value = numericPrixHT.Value = numericPrixTTC.Value = numericLineaireReseau.Value = numericLineaireReseauxRenouveles.Value = numericPopulation.Value = 0;
            buttonAjoutCommuneDesservie.Enabled = false;
            // UD - SISPEA
            numericVolumeConsommeSansComptage.Value = numericVolumeExporte.Value = numericVolumeImporte.Value = numericVolumeProduit.Value = numericVolumesConsommesCompta.Value = numericVolumeService.Value = numericTVA_Applicable.Value = numericVoiesNavigables.Value = numericProtectionRessource.Value = numericRedevancePollution.Value = numericAutresTaxes.Value = 0;
            // UD - Bilan qualitatif
            comboChloration.SelectedIndex = comboModeExploitation.SelectedIndex = 0;
            numericC_Bacteriologique.Value = numericEvDerogations.Value = numericEvRestrictions.Value = numericNombrePLV.Value = 0;
            textAutresTraitements.Text = textEvAutres.Text = "";
            numericMaxpH.Value = numericMaxAlcalimetrique.Value = numericMaxHydrometrique.Value = numericMaxNitrates.Value = numericMaxTurbidite.Value = numericMinpH.Value = numericMinAlcalimetrique.Value = numericMinHydrometrique.Value = numericMinNitrates.Value = numericMinTurbidite.Value = numericMoypH.Value = numericMoyAlcalimetrique.Value = numericMoyHydrometrique.Value = numericMoyNitrates.Value = numericMoyTurbidite.Value = numericMoyPesticidesTotaux.Value = 0;

        }

        private void ViderControlsProcedures() {
            textSubventionAE_PhaseTech.Text = textSubventionCD_PhaseTech.Text = textBE_Retenu.Text = textNomHA.Text = textSubvAE_Admin.Text = textSubvCD_Admin.Text = textGeometreRetenu.Text = textObservationsProcedure.Text = labelEligibiliteProcedure.Text = labelInfosModifProcedure.Text = "";
            dateInfosDelibPhaseAdmin.Checked = dateInfosDelibPhaseTechnique.Checked = dateRencontreCollectivite.Checked = dateDelibPhaseTechnique.Checked = dateConsultationBE.Checked = dateCommande.Checked = dateReceptionEtudePrealable.Checked = dateEnvoiRemarquesEP.Checked = dateEnvoiRemarquesEP_ARS.Checked = dateVersionDefinitive.Checked = dateNomination.Checked = dateReception.Checked = dateEstimationFrais.Checked = dateRecevabilite.Checked = dateDelibPhaseAdmin.Checked = dateConsultationServices.Checked = dateReponseCS.Checked = dateReunionPublique.Checked = dateConsultationGeometre.Checked = dateCommandeGeometre.Checked = dateValidationGeometre.Checked = dateDepotPrefecture.Checked = dateArretePrefectoral.Checked = dateDesignationCommissaire.Checked = dateDebutEnquete.Checked = dateFinEnquete.Checked = dateRapportCommissaire.Checked = dateCODERST.Checked = dateRAA.Checked = false;
            dateInfosDelibPhaseAdmin.Value = dateInfosDelibPhaseTechnique.Value = dateRencontreCollectivite.Value = dateDelibPhaseTechnique.Value = dateConsultationBE.Value = dateCommande.Value = dateReceptionEtudePrealable.Value = dateEnvoiRemarquesEP.Value = dateEnvoiRemarquesEP_ARS.Value = dateVersionDefinitive.Value = dateNomination.Value = dateReception.Value = dateEstimationFrais.Value = dateRecevabilite.Value = dateDelibPhaseAdmin.Value = dateConsultationServices.Value = dateReponseCS.Value = dateReunionPublique.Value = dateConsultationGeometre.Value = dateCommandeGeometre.Value = dateValidationGeometre.Value = dateDepotPrefecture.Value = dateArretePrefectoral.Value = dateDesignationCommissaire.Value = dateDebutEnquete.Value = dateFinEnquete.Value = dateRapportCommissaire.Value = dateCODERST.Value = dateRAA.Value = DateTime.Today;
            buttonEAA.Enabled = buttonAjouterCaptage.Enabled = checkReceptionNoticeLoiEau.Checked = checkTransmissionFrais.Checked = checkConventionSATE.Checked = false;
            numericCoutTotal.Value = numericCoutHA.Value = numericCoutGeometre.Value = numericCoutEP.Value = 0;

            enregistrerProcedureBouton.Text = "Créer";

            dataGridViewCaptages.Rows.Clear();
            dataGridViewCaptages.Refresh();
        }

        private void ViderControlsAAC() {
            comboDiagnostiquePressionsAAC.SelectedIndex = comboDelimitationAAC.SelectedIndex = comboProgrammeActionsAAC.SelectedIndex = comboAnimationAAC.SelectedIndex = 0;
            textObservationsAAC.Text = textBE_RetenuAAC.Text = textDiagnostiquePressionsAAC.Text = textProgrammeActionsAAC.Text = textAnimationAAC.Text = "";
            dateRencontreCollectiviteAAC.Value = dateDelibEngagementAAC.Value = dateCommandeAAC.Value = dateReunionLancementAAC.Value = dateReceptionEtudeAAC.Value = dateReunionPubliqueAAC.Value = dateDelibValidationAAC.Value = DateTime.Today;
            dateRencontreCollectiviteAAC.Checked = dateDelibEngagementAAC.Checked = dateCommandeAAC.Checked = dateReunionLancementAAC.Checked = dateReceptionEtudeAAC.Checked = dateReunionPubliqueAAC.Checked = dateDelibValidationAAC.Checked = false;
        }

        private void ViderControlsSuivi() {
            dateArretePrefectoralSuivi.Value = dateVisite1.Value = dateVisite2.Value = dateVisite3.Value = dateEnvoiRapport1.Value = dateEnvoiRapport2.Value = dateEnvoiRapport3.Value = DateTime.Today;
            dateArretePrefectoralSuivi.Checked = dateVisite1.Checked = dateVisite2.Checked = dateVisite3.Checked = dateEnvoiRapport1.Checked = dateEnvoiRapport2.Checked = dateEnvoiRapport3.Checked = false;
            textObservationsSuivi.Text = infosModifsSuivi.Text = "";
            ajouterCaptageSuiviBouton.Enabled = false;
            dataGridViewSuivi.Rows.Clear();
            dataGridViewSuivi.Refresh();
            enregistrerSuiviBouton.Text = "Créer";
        }

        private void RemplirEtChecker(DateTimePicker datePicker, DateTime? Date) {
            datePicker.Checked = Date != DateTime.MinValue;
            datePicker.Value = datePicker.Checked ? (DateTime)Date : DateTime.Today;

            if (!datePicker.Checked)
                datePicker.CalendarForeColor = Color.White;
            else
                datePicker.CalendarForeColor = Color.Black;
        }

        private void ModificationCaptage(object sender, EventArgs e) {
            enregistrerCaptageBouton.Enabled = annulerCaptageBouton.Enabled = true;
        }

        private void ModificationUD(object sender, EventArgs e) {
            enregistrerUDBouton.Enabled = annulerUDBouton.Enabled = true;
        }

        private void ModificationProcedure(object sender, EventArgs e) {
            enregistrerProcedureBouton.Enabled = annulerProcedureBouton.Enabled = true;
        }

        private void ModificationProcedureAAC(object sender, EventArgs e) {
            enregistrerProcedureAACBouton.Enabled = annulerProcedureAACBouton.Enabled = true;
        }

        private void ModificationSuivi(object sender, EventArgs e) {
            enregistrerSuiviBouton.Enabled = annulerSuiviBouton.Enabled = true;
        }

        private void ModificationPopulationAjout(object sender, EventArgs e) {

            ajouterPopulationButton.Enabled = comboCollectivitePopulation.SelectedIndex > -1 && numericPopulationAjout.Value > 0;

        }

        public static void GenererExtractionProceduresDUP(CSession Session) {

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

            FeuilXL.Name = "Procédures DUP";

            // Police
            ApplicationXL.StandardFont = "Century Gothic";


            // Headers
            FeuilXL.Cells[1, 1] = "Collectivité";
            FeuilXL.Cells[1, 2] = "Code BSS";
            FeuilXL.Cells[1, 3] = "Captage";
            FeuilXL.Cells[1, 4] = "AE";
            FeuilXL.Cells[1, 5] = "Eligibilité";
            FeuilXL.Cells[1, 6] = "Convention";
            FeuilXL.Cells[1, 7] = "Rencontre collectivité";
            FeuilXL.Cells[1, 8] = "Délib phase technique";
            FeuilXL.Cells[1, 9] = "Subv CD";
            FeuilXL.Cells[1, 10] = "Subv AE";
            FeuilXL.Cells[1, 11] = "Consultation BE (date envoi CC)";
            FeuilXL.Cells[1, 12] = "BE retenu";
            FeuilXL.Cells[1, 13] = "Commande";
            FeuilXL.Cells[1, 14] = "Réception EP";
            FeuilXL.Cells[1, 15] = "Envoi remarques EP";
            FeuilXL.Cells[1, 16] = "Envoi remarques EP ARS";
            FeuilXL.Cells[1, 17] = "Version définitive";
            FeuilXL.Cells[1, 18] = "Nom HA";
            FeuilXL.Cells[1, 19] = "Nomination";
            FeuilXL.Cells[1, 20] = "Réception";
            FeuilXL.Cells[1, 21] = "Réception notice Loi sur l'Eau";
            FeuilXL.Cells[1, 22] = "Estimation des frais";
            FeuilXL.Cells[1, 23] = "Transmission à l'ARS";
            FeuilXL.Cells[1, 24] = "Recevabilité";
            FeuilXL.Cells[1, 25] = "Délib phase admin";
            FeuilXL.Cells[1, 26] = "Subv CD";
            FeuilXL.Cells[1, 27] = "Subv AE";
            FeuilXL.Cells[1, 28] = "Consultation des services";
            FeuilXL.Cells[1, 29] = "Réponse CS";
            FeuilXL.Cells[1, 30] = "Réunion publique";
            FeuilXL.Cells[1, 31] = "Consultation géomètre";
            FeuilXL.Cells[1, 32] = "Géomètre retenu";
            FeuilXL.Cells[1, 33] = "Commande du géomètre";
            FeuilXL.Cells[1, 34] = "Validation géomètre ARS";
            FeuilXL.Cells[1, 35] = "Dépôt dossier en préf";
            FeuilXL.Cells[1, 36] = "Arrêté préfectoral enquête publique";
            FeuilXL.Cells[1, 37] = "Désignation commissaire enquêteur";
            FeuilXL.Cells[1, 38] = "Début enquête";
            FeuilXL.Cells[1, 39] = "Fin enquête";
            FeuilXL.Cells[1, 40] = "Rapport commissaire";
            FeuilXL.Cells[1, 41] = "CODERST";
            FeuilXL.Cells[1, 42] = "AP";
            FeuilXL.Cells[1, 43] = "RAA";
            FeuilXL.Cells[1, 44] = "Coût AAC + AMO + EP";
            FeuilXL.Cells[1, 45] = "Coût EP HT";
            FeuilXL.Cells[1, 46] = "Coût HA HT";
            FeuilXL.Cells[1, 47] = "Coût géomètre HT";
            FeuilXL.Cells[1, 48] = "Observations";


            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[1, 48]);
            RangeHeaders.Font.Bold = true;
            RangeHeaders.Font.Size = 9;
            RangeHeaders.Rows.RowHeight = 54;
            RangeHeaders.Interior.Color = CouleurEnTetes;
            RangeHeaders.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.White);

            // Fixes
            FeuilXL.Application.ActiveWindow.SplitColumn = 1;
            FeuilXL.Application.ActiveWindow.SplitRow = 1;
            FeuilXL.Application.ActiveWindow.FreezePanes = true;


            int NumLigne = 2;


            // Récupération des conventions
            string req = "SELECT Captage.CodeCollectivite AS CodeCollectivite,NomCollectivite,BSS,NomRessource,DateArreteDUP,AgenceEau,PotentielFinancier,PopulationDGF,CommunesUrbaines," +
                            "DateRencontreCollectivite,DateDeliberationPhaseTechnique,SubventionCD_PhaseTechnique,SubventionAE_PhaseTechnique,DateConsultationBE," +
                            "BE_Retenu,DateCommandeBE,DateReceptionEtudePrealable,DateEnvoiRemarquesEP,DateEnvoiRemarquesEP_ARS,DateVersionDefinitive,NomHA,DateNomination," +
                            "DateReception,ReceptionNoticeLoiEau,DateEstimationFrais,EstimationFraisTransmission,DateRecevabilite,DateDeliberationPhaseAdmin," +
                            "SubventionCD_PhaseAdmin,SubventionAE_PhaseAdmin,DateConsultationServices,DateReponseCS,DateReunionPublique,DateConsultationGeometre," +
                            "GeometreRetenu,DateCommandeGeometre,DateValidationGeometreARS,DateDepotPrefecture,DateArretePrefectoralDebutEP,DateDesignationCommissaire," +
                            "DateDebutEnquete,DateFinEnquete,DateRapportCommissaire,DateCODERST,DateRAA,CoutTotal,CoutEtudePrealable,CoutHA,CoutGeometre," +
                            "ProcedureDUP.Observations AS Observations FROM Captage LEFT JOIN ProcedureDUP ON ProcedureDUP.idProcedure = Captage.idProcedureCaptage " +
                            "LEFT JOIN Eligibilite ON Captage.CodeCollectivite = Eligibilite.CodeCollectivite LEFT JOIN Collectivite_V ON Captage.CodeCollectivite " +
                            "= Collectivite_V.CodeCollectivite WHERE ((YEAR(GETDATE()) - YEAR(DateArreteDUP)) <= 3 AND (AnneeEligibilite = (SELECT MAX(AnneeEligibilite) FROM Eligibilite) OR AnneeEligibilite IS NULL)) OR (idStatut_EtatArreteDUP = " + (int)eStatut.ArreteEnCours + " AND (AnneeEligibilite = (SELECT MAX(AnneeEligibilite) FROM Eligibilite) OR AnneeEligibilite IS NULL)) ORDER BY Collectivite_V.CodeCollectivite";

            SqlCommand command = new SqlCommand(req, Session.oConn);
            SqlDataReader dataReader = command.ExecuteReader();

            List<string> CodesCollectivites = new List<string>();

            int NumLigneDebut = NumLigne;

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    // Affichage des données
                    FeuilXL.Cells[NumLigne, 1] = dataReader["NomCollectivite"].ToString();
                    FeuilXL.Cells[NumLigne, 2] = dataReader["BSS"].ToString();
                    FeuilXL.Cells[NumLigne, 3] = dataReader["NomRessource"].ToString();
                    FeuilXL.Cells[NumLigne, 4] = dataReader["AgenceEau"].ToString().Equals("Rhin-Meuse") ? "RM" : "SN";
                    FeuilXL.Cells[NumLigne, 7] = ConvertirDateDepuisDataReader(dataReader["DateRencontreCollectivite"]);
                    FeuilXL.Cells[NumLigne, 8] = ConvertirDateDepuisDataReader(dataReader["DateDeliberationPhaseTechnique"]);
                    FeuilXL.Cells[NumLigne, 9] = dataReader["SubventionCD_PhaseTechnique"].ToString();
                    FeuilXL.Cells[NumLigne, 10] = dataReader["SubventionAE_PhaseTechnique"].ToString();
                    FeuilXL.Cells[NumLigne, 11] = ConvertirDateDepuisDataReader(dataReader["DateConsultationBE"]);
                    FeuilXL.Cells[NumLigne, 12] = dataReader["BE_Retenu"].ToString();
                    FeuilXL.Cells[NumLigne, 13] = ConvertirDateDepuisDataReader(dataReader["DateCommandeBE"]);
                    FeuilXL.Cells[NumLigne, 14] = ConvertirDateDepuisDataReader(dataReader["DateReceptionEtudePrealable"]);
                    FeuilXL.Cells[NumLigne, 15] = ConvertirDateDepuisDataReader(dataReader["DateEnvoiRemarquesEP"]);
                    FeuilXL.Cells[NumLigne, 16] = ConvertirDateDepuisDataReader(dataReader["DateEnvoiRemarquesEP_ARS"]);
                    FeuilXL.Cells[NumLigne, 17] = ConvertirDateDepuisDataReader(dataReader["DateVersionDefinitive"]);
                    FeuilXL.Cells[NumLigne, 18] = dataReader["NomHA"].ToString();
                    FeuilXL.Cells[NumLigne, 19] = ConvertirDateDepuisDataReader(dataReader["DateNomination"]);
                    FeuilXL.Cells[NumLigne, 20] = ConvertirDateDepuisDataReader(dataReader["DateReception"]);
                    FeuilXL.Cells[NumLigne, 21] = dataReader["ReceptionNoticeLoiEau"].GetType().Name == "DBNull" ? "" : Convert.ToInt32(dataReader["ReceptionNoticeLoiEau"]) == 1 ? "x" : "";
                    FeuilXL.Cells[NumLigne, 22] = ConvertirDateDepuisDataReader(dataReader["DateEstimationFrais"]);
                    FeuilXL.Cells[NumLigne, 23] = dataReader["EstimationFraisTransmission"].GetType().Name == "DBNull" ? "" : Convert.ToInt32(dataReader["EstimationFraisTransmission"]) == 1 ? "x" : "";
                    FeuilXL.Cells[NumLigne, 24] = ConvertirDateDepuisDataReader(dataReader["DateRecevabilite"]);
                    FeuilXL.Cells[NumLigne, 25] = ConvertirDateDepuisDataReader(dataReader["DateDeliberationPhaseAdmin"]);
                    FeuilXL.Cells[NumLigne, 26] = dataReader["SubventionCD_PhaseAdmin"].ToString();
                    FeuilXL.Cells[NumLigne, 27] = dataReader["SubventionAE_PhaseAdmin"].ToString();
                    FeuilXL.Cells[NumLigne, 28] = ConvertirDateDepuisDataReader(dataReader["DateConsultationServices"]);
                    FeuilXL.Cells[NumLigne, 29] = ConvertirDateDepuisDataReader(dataReader["DateReponseCS"]);
                    FeuilXL.Cells[NumLigne, 30] = ConvertirDateDepuisDataReader(dataReader["DateReunionPublique"]);
                    FeuilXL.Cells[NumLigne, 31] = ConvertirDateDepuisDataReader(dataReader["DateConsultationGeometre"]);
                    FeuilXL.Cells[NumLigne, 32] = dataReader["GeometreRetenu"].ToString();
                    FeuilXL.Cells[NumLigne, 33] = ConvertirDateDepuisDataReader(dataReader["DateCommandeGeometre"]);
                    FeuilXL.Cells[NumLigne, 34] = ConvertirDateDepuisDataReader(dataReader["DateValidationGeometreARS"]);
                    FeuilXL.Cells[NumLigne, 35] = ConvertirDateDepuisDataReader(dataReader["DateDepotPrefecture"]);
                    FeuilXL.Cells[NumLigne, 36] = ConvertirDateDepuisDataReader(dataReader["DateArretePrefectoralDebutEP"]);
                    FeuilXL.Cells[NumLigne, 37] = ConvertirDateDepuisDataReader(dataReader["DateDesignationCommissaire"]);
                    FeuilXL.Cells[NumLigne, 38] = ConvertirDateDepuisDataReader(dataReader["DateDebutEnquete"]);
                    FeuilXL.Cells[NumLigne, 39] = ConvertirDateDepuisDataReader(dataReader["DateFinEnquete"]);
                    FeuilXL.Cells[NumLigne, 40] = ConvertirDateDepuisDataReader(dataReader["DateRapportCommissaire"]);
                    FeuilXL.Cells[NumLigne, 41] = ConvertirDateDepuisDataReader(dataReader["DateCODERST"]);
                    FeuilXL.Cells[NumLigne, 42] = ConvertirDateDepuisDataReader(dataReader["DateArreteDUP"]);
                    FeuilXL.Cells[NumLigne, 43] = ConvertirDateDepuisDataReader(dataReader["DateRAA"]);
                    FeuilXL.Cells[NumLigne, 44] = dataReader["CoutTotal"].GetType().Name == "DBNull" ? "0€" : Decimal.Round(Convert.ToDecimal(dataReader["CoutTotal"]), 2) + "€";
                    FeuilXL.Cells[NumLigne, 45] = dataReader["CoutEtudePrealable"].GetType().Name == "DBNull" ? "0€" : Decimal.Round(Convert.ToDecimal(dataReader["CoutEtudePrealable"]), 2) + "€";
                    FeuilXL.Cells[NumLigne, 46] = dataReader["CoutHA"].GetType().Name == "DBNull" ? "0€" : Decimal.Round(Convert.ToDecimal(dataReader["CoutHA"]), 2) + "€";
                    FeuilXL.Cells[NumLigne, 47] = dataReader["CoutGeometre"].GetType().Name == "DBNull" ? "0€" : Decimal.Round(Convert.ToDecimal(dataReader["CoutGeometre"]), 2) + "€";
                    FeuilXL.Cells[NumLigne, 48] = dataReader["Observations"].ToString();


                    if (dataReader["PotentielFinancier"].GetType().Name != "DBNull") {

                        // Eligibilité
                        CEligibilite Eligibilite = new CEligibilite(Session);

                        Eligibilite.PotentielFinancier = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                        Eligibilite.PopulationDGF = Convert.ToInt32(dataReader["PopulationDGF"]);
                        Eligibilite.CommunesUrbaines = Convert.ToInt32(dataReader["CommunesUrbaines"]);
                        Eligibilite.AnneeEligibilite = DateTime.Today.Year;
                        Eligibilite.CodeCollectivite = dataReader["CodeCollectivite"].ToString();


                        // Si éligible
                        if (Eligibilite.Eligible())
                            FeuilXL.Cells[NumLigne, 5] = "OUI";
                        // Sinon
                        else
                            FeuilXL.Cells[NumLigne, 5] = "NON";

                    }

                    CodesCollectivites.Add(dataReader["CodeCollectivite"].ToString());



                    NumLigne++;

                }
            }
            dataReader.Close();


            foreach (string CodeCollectivite in CodesCollectivites) {

                req = "SELECT idConvention,DateFinConvention FROM Convention WHERE CodeCollectivite = " + CodeCollectivite + " AND (idStatut_TypeConvention = " + (int)eStatut.Captage_DUP + " OR idStatut_TypeConvention = " + (int)eStatut.DUP_SDAGE + " OR idStatut_TypeConvention = " + (int)eStatut.SDAGE + ")";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                DateTime DateFinConvention = dataReader["DateFinConvention"].GetType().Name == "DBNull" ? DateTime.MinValue : Convert.ToDateTime(dataReader["DateFinConvention"]);

                // Convention
                FeuilXL.Cells[NumLigneDebut, 6] = DateFinConvention != DateTime.MinValue && DateFinConvention > DateTime.Today ? "x" : "";

                dataReader.Close();

                NumLigneDebut++;

            }


            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne-1, 48]);
            Range.Cells.WrapText = true;
            Range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            ContoursCellules(Range.Cells.Borders);
            Range.Columns.ColumnWidth = 10;
            Range.Font.Size = 8;
            RangeHeaders.Font.Size = 9;


            // Largeurs
            Microsoft.Office.Interop.Excel.Range RangeCollectivites = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne-1, 1]);
            RangeCollectivites.Columns.ColumnWidth = 30;
            RangeCollectivites.Font.Bold = true;

            Microsoft.Office.Interop.Excel.Range RangeCaptages = FeuilXL.get_Range(FeuilXL.Cells[1, 3], FeuilXL.Cells[NumLigne - 1, 3]);
            RangeCaptages.Columns.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range RangeBassins = FeuilXL.get_Range(FeuilXL.Cells[1, 4], FeuilXL.Cells[NumLigne - 1, 6]);
            RangeBassins.Columns.ColumnWidth = 5;

            Microsoft.Office.Interop.Excel.Range RangePhaseTechnique = FeuilXL.get_Range(FeuilXL.Cells[1, 8], FeuilXL.Cells[NumLigne - 1, 8]);
            RangePhaseTechnique.Font.Bold = true;

            Microsoft.Office.Interop.Excel.Range RangePhaseAdmin = FeuilXL.get_Range(FeuilXL.Cells[1, 25], FeuilXL.Cells[NumLigne - 1,25]);
            RangePhaseAdmin.Font.Bold = true;

            Microsoft.Office.Interop.Excel.Range RangeObservations = FeuilXL.get_Range(FeuilXL.Cells[1, 48], FeuilXL.Cells[NumLigne - 1, 48]);
            RangeObservations.Columns.ColumnWidth = 100;

            Range.Rows.AutoFit();


            ApplicationXL.Visible = true;

        }

        public static void GenererExtractionCaptagesUD(CSession Session) {

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

            FeuilXL.Name = "Captages - UD";

            // Police
            ApplicationXL.StandardFont = "Century Gothic";

            // Headers
            FeuilXL.Cells[2, 1] = "Collectivité";
            FeuilXL.Cells[2, 2] = "UD/CAP";
            FeuilXL.Cells[2, 3] = "Nom";
            FeuilXL.Cells[2, 4] = "Code BSS";
            FeuilXL.Cells[2, 5] = "Source/Forage";
            FeuilXL.Cells[2, 6] = "Commune d'immplantation";
            FeuilXL.Cells[2, 7] = "Date DUP";
            FeuilXL.Cells[2, 8] = "Débit annuel autorisé";
            FeuilXL.Cells[2, 9] = "Communes alimentées";
            FeuilXL.Cells[2, 10] = "Population alimentée";
            FeuilXL.Cells[2, 11] = "Etat";
            FeuilXL.Cells[2, 12] = "UDI code national";
            FeuilXL.Cells[2, 13] = "Communes desservies";
            FeuilXL.Cells[2, 14] = "Nombre d'habitants";
            FeuilXL.Cells[2, 15] = "Achat d'eau";
            FeuilXL.Cells[2, 16] = "Rendement";
            FeuilXL.Cells[2, 17] = "Volume produit";
            FeuilXL.Cells[2, 18] = "Volume consommé comptabilisé";
            FeuilXL.Cells[2, 19] = "Prix de l'eau HT";
            FeuilXL.Cells[2, 20] = "Linéaire de réseau";

            FeuilXL.Cells[1, 4] = "Captage";
            FeuilXL.Cells[1, 12] = "UD";

            FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[1, 3]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 4], FeuilXL.Cells[1, 11]).Cells.Merge();
            FeuilXL.get_Range(FeuilXL.Cells[1, 12], FeuilXL.Cells[1, 20]).Cells.Merge();

            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[2, 20]);
            RangeHeaders.Font.Bold = true;
            RangeHeaders.Interior.Color = CouleurEnTetes;
            RangeHeaders.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.White);

            Microsoft.Office.Interop.Excel.Range RangeCaptages = FeuilXL.get_Range(FeuilXL.Cells[1, 4], FeuilXL.Cells[2, 11]);
            RangeCaptages.Interior.Color = System.Drawing.ColorTranslator.ToOle(frmATE55.Couleurs["Captage"]);

            Microsoft.Office.Interop.Excel.Range RangeUD = FeuilXL.get_Range(FeuilXL.Cells[1, 12], FeuilXL.Cells[2, 20]);
            RangeUD.Interior.Color = System.Drawing.ColorTranslator.ToOle(frmATE55.Couleurs["UD"]);

            // Fixes
            FeuilXL.Application.ActiveWindow.SplitColumn = 1;
            FeuilXL.Application.ActiveWindow.SplitRow = 2;
            FeuilXL.Application.ActiveWindow.FreezePanes = true;

            int NumLigne = 3;


            // Récupération de toutes les ressources
            // SELECT Ressource_V.CodeCollectivite,Collectivite_V.NomCollectivite,Ressource_V.NomRessource,Ressource_V.CodeRessource,Ressource_V.Type,Captage.idStatut_TypeCaptage,Captage.CodeCollectiviteImplantation,Captage.DateArreteDUP,Captage.idStatut_EtatCaptage,UD.Population,UD.Rendement,UD.VolumeProduit,UD.VolumesConsommesComptabilises,UD.PrixEauHT,UD.LineaireReseau FROM Ressource_V LEFT JOIN Captage ON Ressource_V.idRessource = Captage.idCaptage AND Ressource_V.Type = 'Captage' LEFT JOIN UD ON Ressource_V.idRessource = UD.idUD AND Ressource_V.Type = 'UD' LEFT JOIN Collectivite_V ON Captage.CodeCollectivite = Collectivite_V.CodeCollectivite OR UD.CodeCollectivite = Collectivite_V.CodeCollectivite ORDER BY Ressource_V.CodeCollectivite;
            string req = "SELECT Ressource_V.idRessource, Ressource_V.CodeCollectivite,Collectivite_V.NomCollectivite AS NomCollectivite,Ressource_V.NomRessource AS NomRessource,Ressource_V.CodeRessource AS CodeRessource,Ressource_V.Type AS Type,Captage.idStatut_TypeCaptage,Captage.CodeCollectiviteImplantation,Captage.DateArreteDUP,DebitAnnuelAutorise,Captage.idStatut_EtatCaptage,UD.Population,UD.Rendement,UD.VolumeProduit,UD.VolumesConsommesComptabilises,UD.PrixEauHT,UD.LineaireReseau " +
                            "FROM Ressource_V LEFT JOIN Captage ON Ressource_V.idRessource = Captage.idCaptage AND Ressource_V.Type = 'Captage' LEFT JOIN UD ON Ressource_V.idRessource = UD.idUD AND Ressource_V.Type = 'UD' LEFT JOIN Collectivite_V ON Captage.CodeCollectivite = Collectivite_V.CodeCollectivite OR UD.CodeCollectivite = Collectivite_V.CodeCollectivite " +
                            "ORDER BY Ressource_V.CodeCollectivite";
            SqlCommand command = new SqlCommand(req, Session.oConn);
            SqlDataReader dataReader = command.ExecuteReader();


            List<string> IdsRessources = new List<string>();


            if (dataReader != null && dataReader.HasRows) {

                while (dataReader.Read()) {

                    FeuilXL.Cells[NumLigne, 1] = dataReader["NomCollectivite"].ToString();
                    FeuilXL.Cells[NumLigne, 2] = dataReader["Type"].ToString();
                    FeuilXL.Cells[NumLigne, 3] = dataReader["NomRessource"].ToString();

                    // Captage
                    if (dataReader["Type"].ToString().Equals("Captage")) {

                        FeuilXL.Cells[NumLigne, 4] = dataReader["CodeRessource"].ToString();
                        FeuilXL.Cells[NumLigne, 5] = frmATE55.Statuts[Convert.ToInt32(dataReader["idStatut_TypeCaptage"])].LibelleStatut;
                        FeuilXL.Cells[NumLigne, 7] = dataReader["DateArreteDUP"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateArreteDUP"]).ToShortDateString();
                        FeuilXL.Cells[NumLigne, 8] = Convert.ToInt32(dataReader["DebitAnnuelAutorise"]);
                        FeuilXL.Cells[NumLigne, 11] = frmATE55.Statuts[Convert.ToInt32(dataReader["idStatut_EtatCaptage"])].LibelleStatut;

                        try {
                            FeuilXL.Cells[NumLigne, 6] = frmATE55.Collectivites[dataReader["CodeCollectiviteImplantation"].ToString()].NomCollectivite;
                        }
                        catch (KeyNotFoundException) {
                            FeuilXL.Cells[NumLigne, 6] = dataReader["CodeCollectiviteImplantation"].ToString();
                        }

                        IdsRessources.Add("C-" + dataReader["idRessource"].ToString());

                    }
                    // UD
                    else {

                        FeuilXL.Cells[NumLigne, 12] = dataReader["CodeRessource"].ToString();
                        FeuilXL.Cells[NumLigne, 14] = dataReader["Population"].ToString();
                        FeuilXL.Cells[NumLigne, 16] = dataReader["Rendement"].ToString();
                        FeuilXL.Cells[NumLigne, 17] = dataReader["VolumeProduit"].ToString();
                        FeuilXL.Cells[NumLigne, 18] = dataReader["VolumesConsommesComptabilises"].ToString();
                        FeuilXL.Cells[NumLigne, 19] = Decimal.Round(Convert.ToDecimal(dataReader["PrixEauHT"].ToString()), 2) + "€";
                        FeuilXL.Cells[NumLigne, 20] = dataReader["LineaireReseau"].ToString();

                        IdsRessources.Add("U-" + dataReader["idRessource"].ToString());

                    }


                    NumLigne++;

                }

                dataReader.Close();


                NumLigne = 3;

                foreach (string Id in IdsRessources) {

                    int id = Convert.ToInt32(Id.Split('-')[1]);

                    //Captage
                    if (Id.Split('-')[0].Equals("C")) {

                        // Communes alimentées
                        req = "SELECT idCaptage,PopulationCaptage.CodeCollectivite, NomCollectivite, PourcentagePopulation, PopulationDGF, AnneeEligibilite "+
                                "FROM PopulationCaptage LEFT JOIN Collectivite_V ON PopulationCaptage.CodeCollectivite = Collectivite_V.CodeCollectivite LEFT JOIN Eligibilite ON PopulationCaptage.CodeCollectivite = Eligibilite.CodeCollectivite "+
                                "WHERE idCaptage = "+id+" AND AnneeEligibilite = (SELECT MAX(AnneeEligibilite) FROM Eligibilite)";
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {

                            string CommunesAlimentees = "";
                            int Population = 0;

                            dataReader.Read();
                            CommunesAlimentees = dataReader["NomCollectivite"].ToString();
                            Population += (int)(Convert.ToInt32(dataReader["PopulationDGF"]) * Convert.ToDecimal(dataReader["PourcentagePopulation"]) / 100);

                            while (dataReader.Read()) {

                                CommunesAlimentees += ", " + dataReader["NomCollectivite"].ToString();
                                Population += (int)(Convert.ToInt32(dataReader["PopulationDGF"]) * Convert.ToDecimal(dataReader["PourcentagePopulation"]) / 100);
                            }

                            FeuilXL.Cells[NumLigne, 9] = CommunesAlimentees;
                            FeuilXL.Cells[NumLigne, 10] = Population;

                        }
                        dataReader.Close();

                    }
                    //UD
                    else {

                        // Communes desservies
                        req = "SELECT CommunesDesserviesUD.CodeCollectivite, Collectivite_V.NomCollectivite FROM CommunesDesserviesUD LEFT JOIN Collectivite_V ON CommunesDesserviesUD.CodeCollectivite = Collectivite_V.CodeCollectivite WHERE idUD = "+id;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {

                            string CommunesDesservies = "";

                            dataReader.Read();
                            CommunesDesservies = dataReader["NomCollectivite"].ToString();

                            while (dataReader.Read()) {
                                CommunesDesservies += ", " + dataReader["NomCollectivite"].ToString();
                            }

                            FeuilXL.Cells[NumLigne, 13] = CommunesDesservies;

                        }
                        dataReader.Close();


                        // Achat d'eau
                        req = "SELECT CodeCollectiviteVendeur,NomCollectivite FROM VenteEau INNER JOIN Collectivite_V ON VenteEau.CodeCollectiviteVendeur = Collectivite_V.CodeCollectivite INNER JOIN UD ON VenteEau.CodeCollectiviteAcheteur = UD.CodeCollectivite WHERE idUD = " + id;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {

                            string AchatEau = "";

                            dataReader.Read();
                            AchatEau = dataReader["NomCollectivite"].ToString();

                            while (dataReader.Read()) {
                                AchatEau += ", " + dataReader["NomCollectivite"].ToString();
                            }

                            FeuilXL.Cells[NumLigne, 15] = AchatEau;

                        }
                        dataReader.Close();


                    }

                    NumLigne++;

                }


            }
            else
                dataReader.Close();

            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 20]);
            Range.Cells.WrapText = true;
            Range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            ContoursCellules(Range.Cells.Borders);
            Range.Rows.AutoFit();
            Range.Columns.ColumnWidth = 10;



            // Largeurs
            Microsoft.Office.Interop.Excel.Range RangeCollectivites = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 1]);
            RangeCollectivites.Columns.ColumnWidth = 40;
            RangeCollectivites.Font.Bold = true;

            FeuilXL.get_Range(FeuilXL.Cells[1, 3], FeuilXL.Cells[NumLigne - 1, 3]).Columns.ColumnWidth = 30;

            Microsoft.Office.Interop.Excel.Range RangeBSS = FeuilXL.get_Range(FeuilXL.Cells[1, 4], FeuilXL.Cells[NumLigne - 1, 4]);
            RangeBSS.Columns.ColumnWidth = 12;
            RangeBSS.Font.Bold = true;

            FeuilXL.get_Range(FeuilXL.Cells[1, 6], FeuilXL.Cells[NumLigne - 1, 6]).Columns.ColumnWidth = 25;

            FeuilXL.get_Range(FeuilXL.Cells[1, 9], FeuilXL.Cells[NumLigne - 1, 9]).Columns.ColumnWidth = 50;

            FeuilXL.get_Range(FeuilXL.Cells[1, 11], FeuilXL.Cells[NumLigne - 1, 11]).Columns.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range RangeUDI = FeuilXL.get_Range(FeuilXL.Cells[1, 12], FeuilXL.Cells[NumLigne - 1, 12]);
            RangeUDI.Font.Bold = true;

            FeuilXL.get_Range(FeuilXL.Cells[1, 13], FeuilXL.Cells[NumLigne - 1, 13]).Columns.ColumnWidth = 50;

            FeuilXL.get_Range(FeuilXL.Cells[1, 14], FeuilXL.Cells[NumLigne - 1, 14]).Columns.ColumnWidth = 5;

            FeuilXL.get_Range(FeuilXL.Cells[1, 15], FeuilXL.Cells[NumLigne - 1, 15]).Columns.ColumnWidth = 30;

            FeuilXL.get_Range(FeuilXL.Cells[1, 16], FeuilXL.Cells[NumLigne - 1, 20]).Columns.ColumnWidth = 8;

            ApplicationXL.Visible = true;

        }

        private static void GenererEtatAnnuel(CSession Session, int idCaptage) {

            Microsoft.Office.Interop.Excel.Application ApplicationXL = new Microsoft.Office.Interop.Excel.Application();

            string path = Application.StartupPath + @"\TemplatesWord\ModèleEAA.xltx";

            ApplicationXL.Workbooks.Open(path);

            ApplicationXL.Visible = false;

            ApplicationXL.Cells[1, 1] = "ETAT D'AVANCEMENT DE LA PROCEDURE DE PROTECTION DE CAPTAGE PAR DUP \n ANNEE " + DateTime.Today.Year;

            // Récupération des données du captage et de la procédure
            string req = "SELECT Collectivite_V.NomCollectivite, DateArreteDUP, DateRencontreCollectivite, DateDeliberationPhaseTechnique, DateConsultationBE, DateCommandeBE, DateReceptionEtudePrealable, DateEnvoiRemarquesEP, DateEnvoiRemarquesEP_ARS, DateVersionDefinitive, DateNomination, DateReception, DateEstimationFrais, DateRecevabilite, DateDeliberationPhaseAdmin, DateConsultationServices, DateReunionPublique, DateConsultationGeometre, DateCommandeGeometre, DateValidationGeometreARS, DateDepotPrefecture, DateArretePrefectoralDebutEP, DateDesignationCommissaire, DateDebutEnquete, DateFinEnquete, DateRapportCommissaire, DateCODERST, DateRAA FROM Captage LEFT JOIN Collectivite_V ON Captage.CodeCollectivite = Collectivite_V.CodeCollectivite LEFT JOIN ProcedureDUP ON Captage.idProcedureCaptage = ProcedureDUP.idProcedure WHERE idCaptage = " + idCaptage + " AND idProcedureCaptage != 0";
            SqlCommand command = new SqlCommand(req, Session.oConn);
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();

                ApplicationXL.Cells[2, 1] = "Commune de " + dataReader["NomCollectivite"].ToString().ToUpper();
                ApplicationXL.Cells[4, 5] = dataReader["DateRencontreCollectivite"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateRencontreCollectivite"]).ToShortDateString();
                ApplicationXL.Cells[5, 5] = dataReader["DateDeliberationPhaseTechnique"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateDeliberationPhaseTechnique"]).ToShortDateString();
                ApplicationXL.Cells[6, 5] = dataReader["DateConsultationBE"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateConsultationBE"]).ToShortDateString();
                ApplicationXL.Cells[7, 5] = dataReader["DateCommandeBE"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateCommandeBE"]).ToShortDateString();
                ApplicationXL.Cells[8, 5] = dataReader["DateReceptionEtudePrealable"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateReceptionEtudePrealable"]).ToShortDateString();
                ApplicationXL.Cells[9, 5] = dataReader["DateEnvoiRemarquesEP"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateEnvoiRemarquesEP"]).ToShortDateString();
                ApplicationXL.Cells[10, 5] = dataReader["DateEnvoiRemarquesEP_ARS"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateEnvoiRemarquesEP_ARS"]).ToShortDateString();
                ApplicationXL.Cells[11, 5] = dataReader["DateVersionDefinitive"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateVersionDefinitive"]).ToShortDateString();
                ApplicationXL.Cells[12, 5] = dataReader["DateNomination"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateNomination"]).ToShortDateString();
                ApplicationXL.Cells[14, 5] = dataReader["DateReception"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateReception"]).ToShortDateString();
                ApplicationXL.Cells[15, 5] = dataReader["DateEstimationFrais"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateEstimationFrais"]).ToShortDateString();
                ApplicationXL.Cells[17, 5] = dataReader["DateRecevabilite"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateRecevabilite"]).ToShortDateString();
                ApplicationXL.Cells[19, 5] = dataReader["DateDeliberationPhaseAdmin"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateDeliberationPhaseAdmin"]).ToShortDateString();
                ApplicationXL.Cells[20, 5] = dataReader["DateConsultationServices"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateConsultationServices"]).ToShortDateString();
                ApplicationXL.Cells[21, 5] = dataReader["DateReunionPublique"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateReunionPublique"]).ToShortDateString();
                ApplicationXL.Cells[23, 5] = dataReader["DateConsultationGeometre"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateConsultationGeometre"]).ToShortDateString();
                ApplicationXL.Cells[24, 5] = dataReader["DateCommandeGeometre"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateCommandeGeometre"]).ToShortDateString();
                ApplicationXL.Cells[27, 5] = dataReader["DateValidationGeometreARS"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateValidationGeometreARS"]).ToShortDateString();
                ApplicationXL.Cells[28, 5] = dataReader["DateDepotPrefecture"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateDepotPrefecture"]).ToShortDateString();
                ApplicationXL.Cells[29, 5] = dataReader["DateArretePrefectoralDebutEP"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateArretePrefectoralDebutEP"]).ToShortDateString();
                ApplicationXL.Cells[30, 5] = dataReader["DateDesignationCommissaire"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateDesignationCommissaire"]).ToShortDateString();
                ApplicationXL.Cells[34, 5] = (dataReader["DateDebutEnquete"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateDebutEnquete"]).ToShortDateString()) + "\n" +(dataReader["DateFinEnquete"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateFinEnquete"]).ToShortDateString());
                ApplicationXL.Cells[35, 5] = dataReader["DateRapportCommissaire"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateRapportCommissaire"]).ToShortDateString();
                ApplicationXL.Cells[36, 5] = dataReader["DateCODERST"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateCODERST"]).ToShortDateString();
                ApplicationXL.Cells[39, 5] = dataReader["DateArreteDUP"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateArreteDUP"]).ToShortDateString();
                ApplicationXL.Cells[43, 5] = dataReader["DateRAA"].GetType().Name == "DBNull" ? "" : Convert.ToDateTime(dataReader["DateRAA"]).ToShortDateString();


            }
            dataReader.Close();

            ApplicationXL.Visible = true;
        
        }

        private static void ContoursCellules(Microsoft.Office.Interop.Excel.Borders borders) {
            foreach (Microsoft.Office.Interop.Excel.Border Border in borders)
                borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }

        private static string ConvertirDateDepuisDataReader(object data) {
            return data.GetType().Name == "DBNull" ? "" : Convert.ToDateTime(data).ToShortDateString();
        }

        // Décommenter dans frmEauPotable_Load pour importer
        private void ImporterRessources() {

            string path = Application.StartupPath + @"\Imports\Ressources.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();


                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    int TypeRessource = Convert.ToInt32(values[1]);

                    // On crée l'objet en fonction du type
                    if (TypeRessource == (int)eStatut.CAP) {
                        // Captage

                        CCaptage Captage = new CCaptage(Session);

                        Captage.CodeCollectivite = values[2];
                        Captage.idStatut_TypeCaptage = Convert.ToInt32(values[5]);
                        Captage.idStatut_EtatArreteDUP = Convert.ToInt32(values[8]);
                        Captage.idStatut_EtatCaptage = values[55].Equals("") ? 0 : Convert.ToInt32(values[55]);
                        Captage.idSuiviCaptage = -1;
                        Captage.CodeCollectiviteImplantation = values[7];
                        Captage.BSS = values[4];
                        Captage.NomRessource = values[3];
                        Captage.DateArreteDUP = values[9].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[9]);
                        Captage.Observations = "";

                        if (!Captage.Creer()) {
                            this.Text = "Captage : "+values[0] + " / " + values[3];
                            return;
                        }

                    }
                    else if (TypeRessource == (int)eStatut.UD) {
                        // UD

                        CUD UD = new CUD(Session);

                        try {
                            UD.CodeCollectivite = values[2];
                            UD.idStatut_ModeExploitation = values[31].Equals("") ? 0 : Convert.ToInt32(values[31]);
                            UD.idStatut_Chloration = values[32].Equals("") ? 0 :Convert.ToInt32(values[32]);
                            UD.UDI_CodeNat = values[11].Equals("") ? 0 :Convert.ToInt32(values[11]);
                            UD.NomRessource = values[3];
                            UD.Population = values[10].Equals("") ? 0 :Convert.ToInt32(values[10]);
                            UD.VolumeProduit = values[13].Equals("") ? 0 : Convert.ToInt32(values[13]);
                            UD.VolumeImporte = values[14].Equals("") ? 0 : Convert.ToInt32(values[14]);
                            UD.VolumeExporte = values[15].Equals("") ? 0 : Convert.ToInt32(values[15]);
                            UD.LineaireReseau = values[16].Equals("") ? 0 : Convert.ToDecimal(values[16]);
                            UD.LineaireReseauxRenouveles = values[17].Equals("") ? 0 : Convert.ToDecimal(values[17]);
                            UD.TauxTVA_Facture = values[18].Equals("") ? 0 : Convert.ToDecimal(values[18]);
                            UD.VoiesNavigables = values[19].Equals("") ? 0 : Convert.ToInt32(values[19]);
                            UD.ProtectionRessourceAE = values[20].Equals("") ? 0 : Convert.ToDecimal(values[20]);
                            UD.RedevancePollutionAE = values[21].Equals("") ? 0 : Convert.ToDecimal(values[21]);
                            UD.AutresTaxes = values[22].Equals("") ? 0 : Convert.ToDecimal(values[22]);
                            UD.VolumeService = values[23].Equals("") ? 0 : Convert.ToInt32(values[23]);
                            UD.VolumeConsomme = values[24].Equals("") ? 0 : Convert.ToInt32(values[24]);
                            UD.VolumesConsommesComptabilises = values[25].Equals("") ? 0 : Convert.ToInt32(values[25]);
                            UD.Rendement = values[26].Equals("") ? 0 : Convert.ToDecimal(values[26]);
                            UD.ILP = values[27].Equals("") ? 0 : Convert.ToDecimal(values[27]);
                            UD.ILC = values[28].Equals("") ? 0 : Convert.ToDecimal(values[28]);
                            UD.PrixEauHT = values[29].Equals("") ? 0 : Convert.ToDecimal(values[29]);
                            UD.PrixEauTTC = values[30].Equals("") ? 0 : Convert.ToDecimal(values[30]);
                            UD.AutresTraitements = values[33];
                            UD.NombrePLV = values[34].Equals("") ? 0 : Convert.ToInt32(values[34]);
                            UD.RestrictionsEv = values[35].Equals("") ? 0 : Convert.ToInt32(values[35]);
                            UD.DerogationsEv = values[36].Equals("") ? 0 : Convert.ToInt32(values[36]);
                            UD.AutresEv = values[37];
                            UD.C_Bacteriologique = values[38].Equals("") ? 0 : Convert.ToDecimal(values[38]);
                            UD.Max_pH = values[39].Equals("") ? 0 : Convert.ToDecimal(values[39]);
                            UD.Min_pH = values[40].Equals("") ? 0 : Convert.ToDecimal(values[40]);
                            UD.Moy_pH = values[41].Equals("") ? 0 : Convert.ToDecimal(values[41]);
                            UD.Max_TitreAlcalimetrique = values[42].Equals("") ? 0 : Convert.ToDecimal(values[42]);
                            UD.Min_TitreAlcalimetrique = values[43].Equals("") ? 0 : Convert.ToDecimal(values[43]);
                            UD.Moy_TitreAlcalimetrique = values[44].Equals("") ? 0 : Convert.ToDecimal(values[44]);
                            UD.Max_TitreHydrometrique = values[45].Equals("") ? 0 : Convert.ToDecimal(values[45]);
                            UD.Min_TitreHydrometrique = values[46].Equals("") ? 0 : Convert.ToDecimal(values[46]);
                            UD.Moy_TitreHydrometrique = values[47].Equals("") ? 0 : Convert.ToDecimal(values[47]);
                            UD.Max_Turbidite = values[48].Equals("") ? 0 : Convert.ToDecimal(values[48]);
                            UD.Min_Turbidite = values[49].Equals("") ? 0 : Convert.ToDecimal(values[49]);
                            UD.Moy_Turbidite = values[50].Equals("") ? 0 : Convert.ToDecimal(values[50]);
                            UD.Max_Nitrates = values[51].Equals("") ? 0 : Convert.ToDecimal(values[51]);
                            UD.Min_Nitrates = values[52].Equals("") ? 0 : Convert.ToDecimal(values[52]);
                            UD.Moy_Nitrates = values[53].Equals("") ? 0 : Convert.ToDecimal(values[53]);
                            UD.Moy_PesticidesTotaux = values[54].Equals("") ? 0 : Convert.ToDecimal(values[54]);
                            UD.Observations = "";

                            if (!UD.Creer()) {
                                this.Text = "UD : " + values[0] + " / " + values[3];
                                return;
                            }

                        }
                        catch (FormatException) {
                            this.Text = "FormatException - UD : " + values[0] + " / " + values[3];
                            return;
                        }

                    }

                }

            }

        }

        private void ImporterProcedures() {

            string path = Application.StartupPath + @"\Imports\Procedures.csv";

            // On stocke dans un dictionnaire les ids des captages en fonction des codes bss
            Dictionary<string, int> IdsCaptages = new Dictionary<string, int>();

            // On va stocker les ids de procédures qui sont identiques pour ne pas dupliquer les objets et pouvoir les lier à plusieurs captages
            Dictionary<int, int> IdsProcedures = new Dictionary<int, int>();

            // Récupération des captages
            string req = "SELECT idCaptage,BSS FROM Captage";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {
                    try {
                        IdsCaptages.Add(dataReader["BSS"].ToString(), Convert.ToInt32(dataReader["idCaptage"]));
                    }
                    catch (ArgumentException) { }
                }
            }
            dataReader.Close();


            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();


                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    int id = Convert.ToInt32(values[47]);

                    CProcedureDUP Procedure = new CProcedureDUP(Session);


                    // Si l'id n'est pas dans le dictionnaire on crée une nouvelle procédure
                    if (!IdsProcedures.ContainsKey(id)) {

                        // On crée une procédure
                        Procedure.DateRencontreCollectivite = values[2].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[2]);
                        Procedure.DateDeliberationPhaseTechnique = values[3].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[3]);
                        Procedure.SubventionCD_PhaseTechnique = values[4];
                        Procedure.SubventionAE_PhaseTechnique = values[5];
                        Procedure.DateConsultationBE = values[6].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[6]);
                        Procedure.BE_Retenu = values[7];
                        Procedure.DateCommandeBE = values[8].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[8]);
                        Procedure.DateReceptionEtudePrealable = values[9].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[9]);
                        Procedure.DateEnvoiRemarquesEP = values[10].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[10]);
                        Procedure.DateEnvoiRemarquesEP_ARS = values[11].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[11]);
                        Procedure.DateVersionDefinitive = values[12].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[12]);
                        Procedure.NomHA = values[13];
                        Procedure.DateNomination = values[14].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[14]);
                        Procedure.DateReception = values[15].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[15]);
                        Procedure.ReceptionNoticeLoiEau = Convert.ToInt32(values[16]);
                        Procedure.DateEstimationFrais = values[17].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[17]);
                        Procedure.EstimationFraisTransmission = Convert.ToInt32(values[18]);
                        Procedure.DateRecevabilite = values[19].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[19]);
                        Procedure.DateDeliberationPhaseAdmin = values[20].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[20]);
                        Procedure.SubventionCD_PhaseAdmin = values[21];
                        Procedure.SubventionAE_PhaseAdmin = values[22];
                        Procedure.DateConsultationServices = values[23].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[23]);
                        Procedure.DateReponseCS = values[24].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[24]);
                        Procedure.DateReunionPublique = values[25].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[25]);
                        Procedure.DateConsultationGeometre = values[26].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[26]);
                        Procedure.GeometreRetenu = values[27];
                        Procedure.DateCommandeGeometre = values[28].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[28]);
                        Procedure.DateValidationGeometreARS = values[29].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[29]);
                        Procedure.DateDepotPrefecture = values[30].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[30]);
                        Procedure.DateArretePrefectoralDebutEP = values[31].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[31]);
                        Procedure.DateDesignationCommissaire = values[32].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[32]);
                        Procedure.DateDebutEnquete = values[33].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[33]);
                        Procedure.DateFinEnquete = values[34].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[34]);
                        Procedure.DateRapportCommissaire = values[35].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[35]);
                        Procedure.DateCODERST = values[36].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[36]);
                        Procedure.DateRAA = values[38].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[38]);
                        Procedure.Observations = values[39];
                        Procedure.CoutTotal = Convert.ToDecimal(values[41]);
                        Procedure.CoutEtudePrealable = Convert.ToDecimal(values[42]);
                        Procedure.CoutHA = Convert.ToDecimal(values[43]);
                        Procedure.CoutGeometre = Convert.ToDecimal(values[44]);

                        if (!Procedure.Creer()) {
                            this.Text = "Procedure : " + values[0];
                            return;
                        }

                        // On ajoute son id dans le dictionnaire après sa création
                        IdsProcedures.Add(id, Procedure.idProcedure);

                    }
                    else {
                        // S'il contient l'id on l'affecte juste à la procédure
                        Procedure.idProcedure = IdsProcedures[id];
                    }

                    // On va mettre à jour les captages pour leur affecter l'id de la procédure
                    if (!values[45].Equals("")) {

                        try {
                            CCaptage.EnregistrerProcedure(IdsCaptages[values[1]], Procedure.idProcedure, Session); // on lui affecte l'id de la procédure
                        }
                        catch (Exception) {
                            // Si aucun captage n'est associé au code bss on en crée un nouveau
                            CCaptage Captage = new CCaptage(Session);
                            Captage.BSS = values[1];
                            Captage.CodeCollectivite = values[45];
                            Captage.NomRessource = values[46];
                            Captage.CodeCollectiviteImplantation = "55000";
                            Captage.DateArreteDUP = null;
                            Captage.idStatut_EtatArreteDUP = 0;
                            Captage.idStatut_EtatCaptage = 0;
                            Captage.idStatut_TypeCaptage = 0;
                            Captage.Observations = "Généré automatiquement par création d'une procédure";

                            if (!Captage.Creer()) {
                                this.Text = "Captage : " + values[1];
                                return;
                            }

                            // On lui affecte l'id de la procédure en le mettant à jour
                            CCaptage.EnregistrerProcedure(Captage.idCaptage, Procedure.idProcedure, Session);

                        }

                        

                    }

                }

            }

        }

        private void ImporterSuivis() {

            string path = Application.StartupPath + @"\Imports\Suivis.csv";

            // On stocke dans un dictionnaire les ids des captages en fonction des codes bss
            Dictionary<string, int> IdsCaptages = new Dictionary<string, int>();

            // On va stocker les ids de procédures qui sont identiques pour ne pas dupliquer les objets et pouvoir les lier à plusieurs captages
            Dictionary<int, int> IdsSuivis = new Dictionary<int, int>();

            // Récupération des captages
            string req = "SELECT idCaptage,BSS FROM Captage";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {
                    try {
                        IdsCaptages.Add(dataReader["BSS"].ToString(), Convert.ToInt32(dataReader["idCaptage"]));
                    }
                    catch (ArgumentException) { }
                }
            }
            dataReader.Close();


            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();


                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');


                    int id = Convert.ToInt32(values[16]);

                    CSuivi Suivi = new CSuivi(Session);

                    if (!IdsSuivis.ContainsKey(id)) {

                        
                        Suivi.DateVisite1 = values[9].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[9]);
                        Suivi.DateEnvoiRapport1 = values[10].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[10]);
                        Suivi.DateVisite2 = values[11].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[11]);
                        Suivi.DateEnvoiRapport2 = values[12].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[12]);
                        Suivi.DateVisite3 = values[13].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[13]);
                        Suivi.DateEnvoiRapport3 = values[14].Equals("") ? (DateTime?)null : Convert.ToDateTime(values[14]);
                        Suivi.Observations = values[15];

                        if (!Suivi.Creer()) {
                            this.Text = "Suivi : " + values[0] + "/" + values[1];
                            return;
                        }


                        // On ajoute son id dans le dictionnaire après sa création
                        IdsSuivis.Add(id, Suivi.idSuivi);

                    }
                    else {
                        // S'il contient l'id on l'affecte juste à la procédure
                        Suivi.idSuivi = IdsSuivis[id];
                    }

                    CCaptage.EnregistrerSuivi(IdsCaptages[values[1]], Suivi.idSuivi, Session);

                }
            }

        }

        private void ImporterPopulations() {

            string path = Application.StartupPath + @"\Imports\PopulationCaptage.csv";

            // On stocke dans un dictionnaire les ids des captages en fonction des codes bss
            Dictionary<string, int> IdsCaptages = new Dictionary<string, int>();

            // Récupération des captages
            string req = "SELECT idCaptage,BSS FROM Captage";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {
                    try {
                        IdsCaptages.Add(dataReader["BSS"].ToString(), Convert.ToInt32(dataReader["idCaptage"]));
                    }
                    catch (ArgumentException) { }
                }
            }
            dataReader.Close();


            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();


                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');


                    CPopulationCaptage PopulationCaptage = new CPopulationCaptage(Session);

                    try {

                        PopulationCaptage.idCaptage = IdsCaptages[values[0]];
                        PopulationCaptage.CodeCollectivite = values[2];
                        PopulationCaptage.PourcentagePopulation = Convert.ToDecimal(values[3]);

                        if (!PopulationCaptage.Creer()) {
                            this.Text = "Population : " + values[0];
                            return;
                        }

                    }
                    catch (KeyNotFoundException) {
                        this.Text += values[0] + "/";
                    }



                }
            }
        }

        private void ImporterVenteEau() {

            string path = Application.StartupPath + @"\Imports\VenteEau.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();


                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    CVenteEau VenteEau = new CVenteEau(Session);

                    VenteEau.CodeCollectiviteAcheteur = values[0];
                    VenteEau.CodeCollectiviteVendeur = values[1];

                    if (!VenteEau.Creer()) {
                        this.Text = "VenteEau : " + values[0] + " - " + values[1];
                        return;
                    }

                }
            }
        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void toolStripButtonActualiser_Click(object sender, EventArgs e) {
            this.AfficherRessources();
            this.AfficherBilan();
        }

        private void textRecherche_TextChanged(object sender, EventArgs e) {
            this.Rechercher();
        }

        private void tabCaptage_VisibleChanged(object sender, EventArgs e) {
            if (tabCaptage.Visible)
                panelUD.Visible = false;
        }

        private void panelUD_VisibleChanged(object sender, EventArgs e) {
            if (panelUD.Visible)
                tabCaptage.Visible = false;
        }

        private void comboEtatArreteDUP_SelectedIndexChanged(object sender, EventArgs e) {

            // On affiche les infos de la dateArreteDUP seulement si son état est "attribué"
            labelArreteDUP.Visible = dateArreteDUP.Visible = Convert.ToInt32(comboEtatArreteDUP.Get_SelectedId()) == (int)eStatut.ArreteAttribue;

            this.ModificationCaptage(sender, e);

        }

        private void annulerCaptageBouton_Click(object sender, EventArgs e) {

            int idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);

            this.AfficherCaptageSelectionne(idCaptage);
            enregistrerCaptageBouton.Enabled = annulerCaptageBouton.Enabled = false;

        }

        private void enregistrerCaptageBouton_Click(object sender, EventArgs e) {

            int idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);

            this.EnregistrerCaptage(idCaptage);

            enregistrerCaptageBouton.Enabled = annulerCaptageBouton.Enabled = false;
        }

        private void annulerUDBouton_Click(object sender, EventArgs e) {

            int idUD = Convert.ToInt32(treeRessources.SelectedNode.Tag);

            this.AfficherUDSelectionne(idUD);

            enregistrerUDBouton.Enabled = annulerUDBouton.Enabled = false;

        }

        private void enregistrerUDBouton_Click(object sender, EventArgs e) {

            int idUD = Convert.ToInt32(treeRessources.SelectedNode.Tag);

            this.EnregistrerUD(idUD);

            enregistrerUDBouton.Enabled = annulerUDBouton.Enabled = false;

        }

        private void creerUnNouveauCaptageToolStripMenuItem_Click(object sender, EventArgs e) {
            this.EnregistrerCaptage(-1);
        }

        private void creerUnNouvelUDToolStripMenuItem_Click(object sender, EventArgs e) {
            this.EnregistrerUD(-1);
        }

        private void creerUnNouveauCaptageToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.EnregistrerCaptage(-1);
        }

        private void creerUnNouvelUDToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.EnregistrerUD(-1);
        }

        private void supprimerLaRessourceToolStripMenuItem_Click(object sender, EventArgs e) {

            int idRessource = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            int Type = Convert.ToInt32(treeRessources.SelectedNode.Parent.Tag.ToString().Equals("Captage") ? (int)eStatut.CAP : (int)eStatut.UD);

            if (MessageBox.Show("Voulez-vous SUPPRIMER la Ressource [" + (Type == (int)eStatut.CAP ? "Captage" : "UD") + " - " + treeRessources.SelectedNode.Text + "] ?\n Attention, la suppression est irréversible !", "Suppression de la Ressource ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                int index = treeRessources.SelectedNode.Index;
                int indexCollectivite = treeRessources.SelectedNode.Parent.Parent.Index;
                int indexRessource = treeRessources.SelectedNode.Parent.Index;

                if (Type == (int)eStatut.UD) {
                    if (CUD.Supprimer(Session, idRessource)) {
                        treeRessources.Nodes[indexCollectivite].Nodes[indexRessource].Nodes[index].Remove();
                    }
                }
                else {
                    if (CCaptage.Supprimer(Session, idRessource)) {
                        treeRessources.Nodes[indexCollectivite].Nodes[indexRessource].Nodes[index].Remove();
                    }
                }

            }

        }

        private void annulerProcedureBouton_Click(object sender, EventArgs e) {
           
            this.ViderControlsProcedures();

            int idCaptage = 0;

            if (treeRessources.Visible)
                idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            this.AfficherCaptageSelectionne(idCaptage);

        }

        private void enregistrerProcedureBouton_Click(object sender, EventArgs e) {
            int idCaptage = 0;

            if (treeRessources.Visible)
                idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            this.EnregistrerProcedure(idCaptage);
        }

        private void treeRessources_AfterSelect(object sender, TreeViewEventArgs e) {

            TreeNode Node = e.Node;

            // On vérifie que le noeud n'a pas d'enfants
            if (Node.Nodes.Count == 0) {

                // On récupère l'id de la ressource
                int idRessource = Convert.ToInt32(Node.Tag);


                // On teste si c'est un captage ou un UD en récupérant le texte du noeud parent pour afficher la ressource en conséquence
                if (Node.Parent.Text.Split(' ')[0].Equals("Captages"))
                    this.AfficherCaptageSelectionne(idRessource);
                else if(Node.Parent.Text.Split(' ')[0].Equals("UD"))
                    this.AfficherUDSelectionne(idRessource);

            }

        }

        private void treeRessources_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {

            // Tester si clic droit
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {

                // Si le noeud est une collectivité
                if (e.Node.Parent == null) {

                    // On affiche le contextMenuStrip correspondant
                    creerUnNouvelUDToolStripMenuItem1.Enabled = true;
                    creerUnNouveauCaptageToolStripMenuItem1.Enabled = true;
                    menuStripNodeCollectivite.Show(treeRessources, e.X, e.Y);

                    tabCaptage.Visible = panelUD.Visible = false;

                }
                // Sinon si c'est un noeud captage
                else if (e.Node.Tag.ToString().Equals("Captage")) {

                    // On désactive l'option pour créer l'UD
                    creerUnNouvelUDToolStripMenuItem1.Enabled = false;
                    creerUnNouveauCaptageToolStripMenuItem1.Enabled = true;
                    menuStripNodeCollectivite.Show(treeRessources, e.X, e.Y);

                    tabCaptage.Visible = panelUD.Visible = false;

                }
                // Sinon si c'est un noeud UD
                else if (e.Node.Tag.ToString().Equals("UD")) {

                    // On désactive l'option pour créer un captage
                    creerUnNouvelUDToolStripMenuItem1.Enabled = true;
                    creerUnNouveauCaptageToolStripMenuItem1.Enabled = false;
                    menuStripNodeCollectivite.Show(treeRessources, e.X, e.Y);

                    tabCaptage.Visible = panelUD.Visible = false;

                }
                // Sinon on si c'est une ressource captage (testé avec la couleur)
                else if (e.Node.ForeColor.Equals(frmATE55.Couleurs["Captage"])) {

                    // On désactive
                    creerUnNouveauCaptageToolStripMenuItem.Enabled = true;
                    creerUnNouvelUDToolStripMenuItem.Enabled = false;
                    menuStripNodeRessource.Show(treeRessources, e.X, e.Y);

                }
                // Sinon si c'est un UD
                else if (e.Node.ForeColor.Equals(frmATE55.Couleurs["UD"])) {

                    creerUnNouveauCaptageToolStripMenuItem.Enabled = false;
                    creerUnNouvelUDToolStripMenuItem.Enabled = true;
                    menuStripNodeRessource.Show(treeRessources, e.X, e.Y);

                }

                treeRessources.SelectedNode = e.Node;

            }

        }

        private void treeRessources_BeforeSelect(object sender, TreeViewCancelEventArgs e) {
            if (e.Node.Nodes.Count > 0) {

                //e.Cancel = true;

                if (e.Node.IsExpanded)
                    e.Node.Collapse();
                else
                    e.Node.Expand();

            }
        }

        private void checkConventionSATE_CheckedChanged(object sender, EventArgs e) {
            checkConventionSATE.ForeColor = checkConventionSATE.Checked ? Color.Green : Color.Black;
        }

        private void buttonAjouterCaptage_Click(object sender, EventArgs e) {

            if (comboCaptages.SelectedIndex != -1) {

                int idCaptage = Convert.ToInt32(comboCaptages.Get_SelectedId());
                int idCaptageCourant = 0;
                
                if(treeRessources.Visible)
                    idCaptageCourant = Convert.ToInt32(treeRessources.SelectedNode.Tag);
                else
                    idCaptageCourant = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

                // On récupère l'id de la procédure
                string req = "SELECT idProcedureCaptage FROM Captage WHERE idCaptage = " + idCaptageCourant;
                command = new SqlCommand(req, Session.oConn);

                int idProcedure = Convert.ToInt32(command.ExecuteScalar());

                // Màj du captage
                CCaptage.EnregistrerProcedure(idCaptage, idProcedure, Session);

                this.AfficherCaptageSelectionne(idCaptage);

                this.AfficherProcedures(idCaptageCourant);

            }

        }

        private void supprimerLeCaptageDeLaProcedureToolStripMenuItem_Click(object sender, EventArgs e) {
            
            int idCaptage = Convert.ToInt32(dataGridViewCaptages.SelectedRows[0].Cells["idCaptage"].Value);
            int idCaptageCourant = 0;

            if (treeRessources.Visible)
                idCaptageCourant = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptageCourant = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            CCaptage.EnregistrerProcedure(idCaptage, 0, Session);

            this.AfficherCaptageSelectionne(idCaptageCourant);
            this.AfficherProcedures(idCaptageCourant);
        }

        private void dataGridViewCaptages_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewCaptages.HitTest(e.X, e.Y).RowIndex;

            if (pos > -1) {
                dataGridViewCaptages.ClearSelection();
                dataGridViewCaptages.Rows[pos].Selected = true;
            }
        }

        private void checkAfficherProcedures_CheckedChanged(object sender, EventArgs e) {

            if (checkAfficherProcedures.Checked) {
                treeProcedures.Visible = true;
                textRecherche.Enabled = treeRessources.Visible = false;
            }
            else {
                treeRessources.Visible = textRecherche.Enabled = true;
                treeProcedures.Visible = false;
            }


        }

        private void treeProcedures_AfterSelect(object sender, TreeViewEventArgs e) {

            int idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            this.AfficherCaptageSelectionne(idCaptage);
        }

        private void tabControlProcedures_SelectedIndexChanged(object sender, EventArgs e) {
            // On change la hauteur et la position en fonction de l'onglet
            if (tabControlProcedures.SelectedIndex == 4) {

                tabControlProcedures.Height = 631;
                tabControlProcedures.Location = new Point(0, 0);

            }
            else {

                tabControlProcedures.Height = 440;
                tabControlProcedures.Location = new Point(0, 64);

            }

        }

        private void annulerProcedureAACBouton_Click(object sender, EventArgs e) {
            int idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            this.AfficherCaptageSelectionne(idCaptage);
        }

        private void enregistrerProcedureAACBouton_Click(object sender, EventArgs e) {
            int idCaptage = 0;

            if (treeRessources.Visible)
                idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            this.EnregistrerProcedureAAC(idCaptage);
        }

        private void dataGridViewSuivi_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewSuivi.HitTest(e.X, e.Y).RowIndex;

            if (pos > -1) {
                dataGridViewSuivi.ClearSelection();
                dataGridViewSuivi.Rows[pos].Selected = true;
            }
        }

        private void annulerSuiviBouton_Click(object sender, EventArgs e) {

            int idCaptage = 0;

            if (treeRessources.Visible)
                idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            this.AfficherCaptageSelectionne(idCaptage);

        }

        private void enregistrerSuiviBouton_Click(object sender, EventArgs e) {

            int idCaptage = 0;

            if (treeRessources.Visible)
                idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            this.EnregistrerSuivi(idCaptage);
        }

        private void supprimerLeCaptageDuSuiviToolStripMenuItem_Click(object sender, EventArgs e) {

            int idCaptage = Convert.ToInt32(dataGridViewSuivi.SelectedRows[0].Cells["idCaptageSuivi"].Value);
            int idCaptageCourant = 0;

            if (treeRessources.Visible)
                idCaptageCourant = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptageCourant = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            CCaptage.EnregistrerSuivi(idCaptage, -1, Session);

            this.AfficherCaptageSelectionne(idCaptageCourant);

        }

        private void ajouterCaptageSuiviBouton_Click(object sender, EventArgs e) {

            if (comboCaptagesSuivi.SelectedIndex != -1) {

                int idCaptage = Convert.ToInt32(comboCaptagesSuivi.Get_SelectedId());
                int idCaptageCourant = 0;

                if (treeRessources.Visible)
                    idCaptageCourant = Convert.ToInt32(treeRessources.SelectedNode.Tag);
                else
                    idCaptageCourant = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

                // On récupère l'id du suivi
                string req = "SELECT idSuiviCaptage FROM Captage WHERE idCaptage = " + idCaptageCourant;
                command = new SqlCommand(req, Session.oConn);

                int idSuivi = Convert.ToInt32(command.ExecuteScalar());

                // Màj du captage
                CCaptage.EnregistrerSuivi(idCaptage, idSuivi, Session);

                this.AfficherCaptageSelectionne(idCaptage);

            }

        }

        private void dataGridViewPopulationCaptage_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewPopulationCaptage.HitTest(e.X, e.Y).RowIndex;

            if (pos > -1) {
                dataGridViewPopulationCaptage.ClearSelection();
                dataGridViewPopulationCaptage.Rows[pos].Selected = true;
            }

        }

        private void ajouterPopulationButton_Click(object sender, EventArgs e) {

            // id du captage
            int idCaptage = 0;

            if (treeRessources.Visible)
                idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            // Code de la collectivité
            string CodeCollectivite = "";

            if (comboCollectivitePopulation.SelectedIndex != -1 && idCaptage != -1) {
                String[] s = comboCollectivitePopulation.SelectedItem.ToString().Split(null);
                CodeCollectivite = s[s.Length - 1];
            }
            else
                return;

            // Pourcentage
            decimal Pourcentage = numericPopulationAjout.Value;

            // Création et ajout
            CPopulationCaptage PopulationCaptage = new CPopulationCaptage(Session);

            PopulationCaptage.idCaptage = idCaptage;
            PopulationCaptage.CodeCollectivite = CodeCollectivite;
            PopulationCaptage.PourcentagePopulation = Pourcentage;

            if (PopulationCaptage.Creer())
                this.AfficherCaptageSelectionne(idCaptage);

        }

        private void supprimerLaPopulationToolStripMenuItem_Click(object sender, EventArgs e) {

            int idCaptage = Convert.ToInt32(dataGridViewPopulationCaptage.SelectedRows[0].Cells["idCaptagePopulation"].Value);
            string CodeCollectivite = dataGridViewPopulationCaptage.SelectedRows[0].Cells["CodeCollectivitePopulation"].Value.ToString();

            CPopulationCaptage.Supprimer(Session, idCaptage, CodeCollectivite);

            this.AfficherCaptageSelectionne(idCaptage);

        }

        private void extraireLesProceduresDUPToolStripMenuItem_Click(object sender, EventArgs e) {
            GenererExtractionProceduresDUP(Session);
        }

        private void extraireLesCaptagesUDToolStripMenuItem_Click(object sender, EventArgs e) {
            GenererExtractionCaptagesUD(Session);
        }

        private void comboCommunesDesservies_SelectedIndexChanged(object sender, EventArgs e) {
            buttonAjoutCommuneDesservie.Enabled = true;
        }

        private void buttonAjoutCommuneDesservie_Click(object sender, EventArgs e) {

            int idUD = Convert.ToInt32(treeRessources.SelectedNode.Tag);

            // Code de la collectivité
            string CodeCollectivite = "";

            if (comboCommunesDesservies.SelectedIndex != -1 && idUD != -1) {
                String[] s = comboCommunesDesservies.SelectedItem.ToString().Split(null);
                CodeCollectivite = s[s.Length - 1];
            }
            else
                return;

            CCommunesDesserviesUD CommunesDesservies = new CCommunesDesserviesUD(Session);
            CommunesDesservies.idUD = idUD;
            CommunesDesservies.CodeCollectivite = CodeCollectivite;

            if (CommunesDesservies.Creer())
                this.AfficherUDSelectionne(idUD);

        }

        private void dataGridViewCommunesDesservies_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewCommunesDesservies.HitTest(e.X, e.Y).RowIndex;

            if (pos > -1) {
                dataGridViewCommunesDesservies.ClearSelection();
                dataGridViewCommunesDesservies.Rows[pos].Selected = true;
            }
        }

        private void supprimerLaCommuneDesservieToolStripMenuItem_Click(object sender, EventArgs e) {

            int idUD = Convert.ToInt32(dataGridViewCommunesDesservies.SelectedRows[0].Cells["idUD"].Value);
            string CodeCollectivite = dataGridViewCommunesDesservies.SelectedRows[0].Cells["CodeCollectiviteCommuneDesservie"].Value.ToString();

            CCommunesDesserviesUD.Supprimer(Session, idUD, CodeCollectivite);

            this.AfficherUDSelectionne(idUD);

        }

        private void buttonEAA_Click(object sender, EventArgs e) {

            // id du captage
            int idCaptage = 0;

            if (treeRessources.Visible)
                idCaptage = Convert.ToInt32(treeRessources.SelectedNode.Tag);
            else
                idCaptage = Convert.ToInt32(treeProcedures.SelectedNode.Tag);

            GenererEtatAnnuel(Session, idCaptage);
        }

        private void comboCollectiviteVendeuse_SelectedIndexChanged(object sender, EventArgs e) {
            buttonAjouterVendeur.Enabled = true;
        }

        private void buttonAjouterVendeur_Click(object sender, EventArgs e) {

            int idUD = Convert.ToInt32(treeRessources.SelectedNode.Tag);

            string CodeCollectiviteAcheteur = dataGridViewAchatEau.SelectedRows[0].Cells["CodeCollectiviteAcheteur"].Value.ToString();
            string CodeCollectiviteVendeur = "";

            if (comboCollectiviteVendeuse.SelectedIndex != -1 && idUD != -1) {
                String[] s = comboCollectiviteVendeuse.SelectedItem.ToString().Split(null);
                CodeCollectiviteVendeur = s[s.Length - 1];
            }
            else
                return;

            CVenteEau VenteEau = new CVenteEau(Session);

            VenteEau.CodeCollectiviteAcheteur = CodeCollectiviteAcheteur;
            VenteEau.CodeCollectiviteVendeur = CodeCollectiviteVendeur;

            if (VenteEau.Creer())
                this.AfficherUDSelectionne(idUD);

        }

        private void supprimerLeVendeurToolStripMenuItem_Click(object sender, EventArgs e) {

            int idUD = Convert.ToInt32(treeRessources.SelectedNode.Tag);

            string CodeCollectiviteAcheteur = dataGridViewAchatEau.SelectedRows[0].Cells["CodeCollectiviteAcheteur"].Value.ToString();
            string CodeCollectiviteVendeur = dataGridViewAchatEau.SelectedRows[0].Cells["CodeCollectiviteVendeur"].Value.ToString();

            CVenteEau.Supprimer(Session, CodeCollectiviteAcheteur, CodeCollectiviteVendeur);

            this.AfficherUDSelectionne(idUD);

        }

        private void dataGridViewAchatEau_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewAchatEau.HitTest(e.X, e.Y).RowIndex;

            if (pos > -1) {
                dataGridViewAchatEau.ClearSelection();
                dataGridViewAchatEau.Rows[pos].Selected = true;
            }
        }

    }
}
