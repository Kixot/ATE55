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
    public partial class frmAssainissement : Form {

        public CSession Session;
        private SqlCommand command;
        private SqlDataReader dataReader;
        int idStationCourante = -1;
        
        public frmAssainissement() {
            InitializeComponent();
            this.Text = "ATE55 - Assainissement";
        }

        private void frmAssainissement_Load(object sender, EventArgs e) {

            Session = (CSession)this.Tag;

            foreach (KeyValuePair<string, CCollectivite> Collectivite in frmATE55.Collectivites) {
                comboCollectiviteLocalisation.Items.Add(Collectivite.Value.NomCollectivite + " - " + Collectivite.Key);
                comboCollectiviteReseau.Items.Add(Collectivite.Value.NomCollectivite + " - " + Collectivite.Key);
            }


            toolStripLabel_Session.Visible = true;
            toolStripLabel_Session.Text = Session.Utilisateur.Utilisateur
                + " [" + Session.Utilisateur.CodeDomaine + "/" + Session.Utilisateur.DroitAccess + "]"
                + ", accès précédent le " + Session.Utilisateur.DerniereConnexion.ToString();


            frmATE55.AlimenterComboBox("TypeStation", comboTypeStation, null, Session, -1);
            frmATE55.AlimenterComboBox("ModeGestionStation", comboModeGestion, null, Session, -1);
            frmATE55.AlimenterComboBox("TypeReseau", comboTypeReseau, null, Session, -1);
            frmATE55.AlimenterComboBox("FiliereBoue", comboFiliereBoue, null, Session, -1);
            frmATE55.AlimenterComboBox("ParametreMesure", comboParametresBilan, null, Session, -1);
            frmATE55.AlimenterComboBox("ParametreMesure", comboParametresPP, null, Session, -1);
            frmATE55.AlimenterComboBox("ConformiteStation", comboConformiteBilan, null, Session, -1);
            frmATE55.AlimenterComboBox("ConformiteStation", comboConformitePP, null, Session, -1);
            frmATE55.AlimenterComboBox("EtatOuvrageEntretien", comboEtatOuvrages, null, Session, -1);
            frmATE55.AlimenterComboBox("EtatOuvrageEntretien", comboEtatEntretien, null, Session, -1);

            // A décommenter pour importer les stations
            //this.ImporterStations();
            //this.ImporterReseaux();
            //this.ImporterMesures();
            //this.ImporterNormes();

            this.AfficherStations();

        }

        private void AfficherStations() {

            DataGridView dgv = dataGridViewStations;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                // On récupère les stations
                string req = "SELECT idStation,NomStation,idStatut_TypeStation,idStatut_SousTypeStation,AnneeConstruction,Capacite,SuiviSATE,CodeCollectiviteLocalisation FROM StationAssainissement ORDER BY NomStation ASC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();


                // On stocke dans une liste
                List<CStationAssainissement> Stations = new List<CStationAssainissement>();


                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        CStationAssainissement Station = new CStationAssainissement();

                        Station.idStation = Convert.ToInt32(dataReader["idStation"]);
                        Station.idStatut_TypeStation = Convert.ToInt32(dataReader["idStatut_TypeStation"]);
                        Station.idStatut_SousTypeStation = Convert.ToInt32(dataReader["idStatut_SousTypeStation"]);
                        Station.AnneeConstruction = Convert.ToInt32(dataReader["AnneeConstruction"]);
                        Station.NomStation = dataReader["NomStation"].ToString();
                        Station.Capacite = Convert.ToInt32(dataReader["Capacite"]);
                        Station.SuiviSATE = Convert.ToInt32(dataReader["SuiviSATE"]);
                        Station.CodeCollectiviteLocalisation = dataReader["CodeCollectiviteLocalisation"].ToString();

                        Stations.Add(Station);

                    }
                }
                dataReader.Close();

                dgv.Rows.Add(Stations.Count);

                int i = 0;


                // On parcourt la liste de stations
                foreach (CStationAssainissement Station in Stations) {

                    // On ajoute une ligne pour afficher la station
                    row = dgv.Rows[i];
                    i++;

                    row.Cells["idStation"].Value = Station.idStation;
                    row.Cells["CapaciteStation"].Value = Station.Capacite + " EqH";
                    row.Cells["TypeStation"].Value = Station.idStatut_SousTypeStation != 0 ? frmATE55.Statuts[Station.idStatut_SousTypeStation].LibelleStatut : (Station.idStatut_TypeStation != 0 ? frmATE55.Statuts[Station.idStatut_TypeStation].LibelleStatut : "");
                    row.Cells["AnneeConstruction"].Value = Station.AnneeConstruction;

                    row.Cells["CollectiviteStation"].Value = Station.NomStation;

                    // Si la station est suivie par le sate on l'affiche en bleu
                    if (Station.SuiviSATE == 1)
                        row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Bleu"];
                    
                    
                    
                    if (!Station.CodeCollectiviteLocalisation.Equals("") && !Station.CodeCollectiviteLocalisation.Equals("55000")) {

                        // Si la station est éligible on colore la dernière cellule
                        req = "SELECT TOP 1 PotentielFinancier,PopulationDGF,CommunesUrbaines,AnneeEligibilite FROM Eligibilite WHERE CodeCollectivite = " + Station.CodeCollectiviteLocalisation + " ORDER BY AnneeEligibilite DESC";
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();

                            CEligibilite Eligibilite = new CEligibilite(Session);

                            Eligibilite.PotentielFinancier = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                            Eligibilite.PopulationDGF = Convert.ToInt32(dataReader["PopulationDGF"]);
                            Eligibilite.CommunesUrbaines = Convert.ToInt32(dataReader["CommunesUrbaines"]);
                            Eligibilite.AnneeEligibilite = Convert.ToInt32(dataReader["AnneeEligibilite"]);
                            Eligibilite.CodeCollectivite = Station.CodeCollectiviteLocalisation;

                            dataReader.Close();


                            DataGridViewCell Cell = row.Cells["EligibiliteStation"];

                            // Si éligible
                            if (Eligibilite.Eligible())
                                Cell.Style.BackColor = frmATE55.Couleurs["Vert"];
                            // Sinon si éligible année précédente
                            else if (Eligibilite.Eligible(DateTime.Today.Year - 1))
                                Cell.Style.BackColor = frmATE55.Couleurs["Orange"];
                            // Sinon
                            else
                                Cell.Style.BackColor = frmATE55.Couleurs["Rouge"];

                        }
                        else
                            dataReader.Close();

                    }
                    

                }

                enregistrerStationBouton.Enabled = false;

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

        private void AfficherReseaux(int idStation) {

            DataGridView dgv = dataGridViewReseaux;
            DataGridViewRow row;

            try {
                dgv.Rows.Clear();
                dgv.Refresh();
            }
            catch (ArgumentException e) { e.ToString(); }


            try {

                string req = "SELECT idReseau,CodeCollectiviteCT,idStatut_TypeReseau,Longueur FROM Reseau WHERE idStationReseau = " + idStation;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On stocke les réseaux dans une liste
                List<CReseau> Reseaux = new List<CReseau>();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        CReseau Reseau = new CReseau();

                        Reseau.idReseau = Convert.ToInt32(dataReader["idReseau"]);
                        Reseau.CodeCollectiviteCT = dataReader["CodeCollectiviteCT"].ToString();
                        Reseau.idStatut_TypeReseau = Convert.ToInt32(dataReader["idStatut_TypeReseau"]);
                        Reseau.Longueur = Decimal.Round(Convert.ToDecimal(dataReader["Longueur"]), 2);

                        Reseaux.Add(Reseau);

                    }
                }
                dataReader.Close();


                // On parcourt la liste
                foreach (CReseau Reseau in Reseaux) {

                    int i = dgv.Rows.Add();
                    row = dgv.Rows[i];

                    row.Cells["idReseau"].Value = Reseau.idReseau;
                    row.Cells["TypeReseau"].Value = frmATE55.Statuts[Reseau.idStatut_TypeReseau].LibelleStatut;
                    row.Cells["LineaireReseau"].Value = Decimal.Round(Reseau.Longueur,2) + " km";

                    try {
                        row.Cells["NomCTReseau"].Value = frmATE55.Collectivites[Reseau.CodeCollectiviteCT].NomCollectivite;
                    }
                    catch (KeyNotFoundException e) {
                        e.ToString();
                        row.Cells["NomCTReseau"].Value = Reseau.CodeCollectiviteCT;
                    }

                    // On récupère le nom du maître d'ouvrage dans l'éligibilité
                    req = "SELECT AssainissementCollectif FROM Eligibilite WHERE CodeCollectivite = " + Reseau.CodeCollectiviteCT + " AND AnneeEligibilite = " + DateTime.Today.Year;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        row.Cells["NomMOReseau"].Value = dataReader["AssainissementCollectif"].ToString();
                    }
                    dataReader.Close();

                }

                tabReseau.Visible = false;

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

        private void AfficherMesuresBilan() {

            this.ViderControlsMesuresBilan();

            if (comboDatesBilans.SelectedIndex != -1) {

                int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

                DataGridView dgv = dataGridViewBilan;
                DataGridViewRow row;

                dgv.Rows.Clear();
                dgv.Refresh();


                // On récupère les mesures du paramètre pour la date sélectionnée
                string req = "SELECT * FROM Mesure WHERE idStationMesure = " + idStation + " AND DateMesure = '" + comboDatesBilans.Text + "' AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On stocke les mesures dans une liste
                List<CMesure> Mesures = new List<CMesure>();


                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        CMesure Mesure = new CMesure();

                        Mesure.idMesure = Convert.ToInt32(dataReader["idMesure"]);
                        Mesure.idStatut_TypeParametre = Convert.ToInt32(dataReader["idStatut_TypeParametre"]);
                        Mesure.MesureFluxEntree = Convert.ToInt32(dataReader["MesureFluxEntree"]);
                        Mesure.MesureFluxSortie = Convert.ToInt32(dataReader["MesureFluxSortie"]);
                        Mesure.MesureConcentrationEntree = Convert.ToInt32(dataReader["MesureConcentrationEntree"]);
                        Mesure.MesureConcentrationSortie = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                        Mesure.Rendement = Convert.ToInt32(dataReader["Rendement"]);

                        Mesures.Add(Mesure);

                    }

                    dataReader.Close();

                    // On parcourt la liste des mesures
                    foreach (CMesure Mesure in Mesures) {

                        int i = dgv.Rows.Add();
                        row = dgv.Rows[i];

                        row.Cells["idMesure"].Value = Mesure.idMesure;
                        row.Cells["BilanParametre"].Value = frmATE55.Statuts[Mesure.idStatut_TypeParametre].LibelleStatut;
                        row.Cells["BilanFluxEntree"].Value = Mesure.idStatut_TypeParametre == (int)eStatut.Parametre_Volume ? "" : Mesure.MesureFluxEntree + " kg/j";
                        row.Cells["BilanFluxSortie"].Value = Mesure.idStatut_TypeParametre == (int)eStatut.Parametre_Volume ? "" :  Mesure.MesureFluxSortie + " kg/j";
                        row.Cells["BilanConcentrationEntree"].Value = Mesure.MesureConcentrationEntree + (Mesure.idStatut_TypeParametre == (int)eStatut.Parametre_Volume ? " m³/j" : " mg/l");
                        row.Cells["BilanConcentrationSortie"].Value = Mesure.MesureConcentrationSortie + (Mesure.idStatut_TypeParametre == (int)eStatut.Parametre_Volume ? " m³/j" : " mg/l");
                        row.Cells["BilanRendement"].Value = Mesure.idStatut_TypeParametre == (int)eStatut.Parametre_Volume ? "" : Mesure.Rendement + " %";


                        // Récupération des normes
                        req = "SELECT ConcentrationMax, RendementMin FROM Norme WHERE idStationNorme = " + idStation + " AND idStatut_TypeParametre = " + Mesure.idStatut_TypeParametre + " AND AnneeValidite = " + Convert.ToDateTime(comboDatesBilans.Text).Year;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();

                            int ConcentrationMax = Convert.ToInt32(dataReader["ConcentrationMax"]);
                            int RendementMin = Convert.ToInt32(dataReader["RendementMin"]);

                            row.Cells["NormeConcentrationBilan"].Value = ConcentrationMax == 0 ? "" : ConcentrationMax + " mg/l";
                            row.Cells["NormeRendementBilan"].Value = RendementMin == 0 ? "" : RendementMin + " %";

                            // Couleurs
                            row.Cells["NormeConcentrationBilan"].Style.ForeColor = Mesure.MesureConcentrationSortie >= ConcentrationMax ? frmATE55.Couleurs["Rouge"] : frmATE55.Couleurs["Vert"];
                            row.Cells["NormeRendementBilan"].Style.ForeColor = Mesure.Rendement < RendementMin ? frmATE55.Couleurs["Rouge"] : frmATE55.Couleurs["Vert"];

                        }
                        dataReader.Close();

                    }

                }
                else
                    dataReader.Close();

            }

            panelMesuresBilan.Visible = false;

        }

        private void AfficherMesuresPP() {

            this.ViderControlsMesuresPP();

            if (comboDatesPP.SelectedIndex != -1) {

                int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

                DataGridView dgv = dataGridViewPP;
                DataGridViewRow row;

                dgv.Rows.Clear();
                dgv.Refresh();


                // On récupère les mesures du paramètre pour la date sélectionnée
                string req = "SELECT * FROM Mesure WHERE idStationMesure = " + idStation + " AND DateMesure = '" + comboDatesPP.Text + "' AND idStatut_TypeMesure = " + (int)eStatut.Mesure_PP;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On stocke les mesures dans une liste
                List<CMesure> Mesures = new List<CMesure>();


                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        CMesure Mesure = new CMesure();

                        Mesure.idMesure = Convert.ToInt32(dataReader["idMesure"]);
                        Mesure.idStatut_TypeParametre = Convert.ToInt32(dataReader["idStatut_TypeParametre"]);
                        Mesure.MesureConcentrationEntree = Convert.ToInt32(dataReader["MesureConcentrationEntree"]);
                        Mesure.MesureConcentrationSortie = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                        Mesure.MesureConcentrationPointIntermediaire1 = Convert.ToInt32(dataReader["MesureConcentrationPointIntermediaire1"]);
                        Mesure.MesureConcentrationPointIntermediaire2 = Convert.ToInt32(dataReader["MesureConcentrationPointIntermediaire2"]);
                        Mesure.Rendement = Convert.ToInt32(dataReader["Rendement"]);

                        Mesures.Add(Mesure);

                    }

                    dataReader.Close();

                    // On parcourt la liste des mesures
                    foreach (CMesure Mesure in Mesures) {

                        if (Mesure.idStatut_TypeParametre != (int)eStatut.Parametre_Volume) {

                            int i = dgv.Rows.Add();
                            row = dgv.Rows[i];

                            row.Cells["idMesurePP"].Value = Mesure.idMesure;
                            row.Cells["ParametrePP"].Value = frmATE55.Statuts[Mesure.idStatut_TypeParametre].LibelleStatut;
                            row.Cells["ConcentrationEntreePP"].Value = Mesure.MesureConcentrationEntree + " mg/l";
                            row.Cells["ConcentrationSortiePP"].Value = Mesure.MesureConcentrationSortie + " mg/l";
                            row.Cells["ConcentrationSortieFiltre1"].Value = Mesure.MesureConcentrationPointIntermediaire1 + " mg/l";
                            row.Cells["ConcentrationSortieFiltre2"].Value = Mesure.MesureConcentrationPointIntermediaire2 + " mg/l";
                            row.Cells["RendementPP"].Value = Mesure.Rendement + " %";


                            // Récupération des normes
                            req = "SELECT ConcentrationMax, RendementMin FROM Norme WHERE idStationNorme = " + idStation + " AND idStatut_TypeParametre = " + Mesure.idStatut_TypeParametre + " AND AnneeValidite = " + Convert.ToDateTime(comboDatesPP.Text).Year;
                            command = new SqlCommand(req, Session.oConn);
                            dataReader = command.ExecuteReader();

                            if (dataReader != null && dataReader.HasRows) {
                                dataReader.Read();

                                int ConcentrationMax = Convert.ToInt32(dataReader["ConcentrationMax"]);
                                int RendementMin = Convert.ToInt32(dataReader["RendementMin"]);

                                row.Cells["NormeConcentrationPP"].Value = ConcentrationMax == 0 ? "" : ConcentrationMax + " mg/l";
                                row.Cells["NormeRendementPP"].Value = RendementMin == 0 ? "" : RendementMin + " %";

                                // Couleurs
                                row.Cells["NormeConcentrationPP"].Style.ForeColor = Mesure.MesureConcentrationSortie >= ConcentrationMax ? frmATE55.Couleurs["Rouge"] : frmATE55.Couleurs["Vert"];
                                row.Cells["NormeRendementPP"].Style.ForeColor = Mesure.Rendement < RendementMin ? frmATE55.Couleurs["Rouge"] : frmATE55.Couleurs["Vert"];

                            }
                            dataReader.Close();
                        }

                    }

                }
                else
                    dataReader.Close();

            }

            panelMesuresPP.Visible = false;

        }

        private void AfficherStationSelectionnee(int idStation) {

            this.ViderControls();

            try {

                string req = "SELECT * FROM StationAssainissement WHERE idStation = " + idStation;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On affiche les infos de la station
                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CStationAssainissement Station = new CStationAssainissement();
                    Station.NomStation = dataReader["NomStation"].ToString();
                    Station.CodeSANDRE = dataReader["CodeSANDRE"].ToString();
                    Station.idStatut_TypeFiliereBoue = Convert.ToInt32(dataReader["idStatut_TypeFiliereBoue"]);
                    Station.idStatut_ModeGestion = Convert.ToInt32(dataReader["idStatut_ModeGestion"]);
                    Station.idStatut_TypeStation = Convert.ToInt32(dataReader["idStatut_TypeStation"]);
                    Station.idStatut_SousTypeStation = Convert.ToInt32(dataReader["idStatut_SousTypeStation"]);
                    Station.idStatut_EtatEntretien = Convert.ToInt32(dataReader["idStatut_EtatEntretien"]);
                    Station.idStatut_EtatOuvrages = Convert.ToInt32(dataReader["idStatut_EtatOuvrages"]);
                    Station.Capacite = Convert.ToInt32(dataReader["Capacite"]);
                    Station.CapaciteDBO5 = Convert.ToInt32(dataReader["CapaciteDBO5"]);
                    Station.DebitReference = Convert.ToDecimal(dataReader["DebitReference"]);
                    Station.DebitReferenceRecalcule = Convert.ToDecimal(dataReader["DebitReferenceRecalcule"]);
                    Station.AnneeConstruction = Convert.ToInt32(dataReader["AnneeConstruction"]);
                    Station.ComplementModeGestion = dataReader["ComplementModeGestion"].ToString();
                    Station.CodeCollectiviteLocalisation = dataReader["CodeCollectiviteLocalisation"].ToString();
                    Station.ExutoireStation = dataReader["ExutoireStation"].ToString();
                    Station.PositionX = Convert.ToDecimal(dataReader["PositionX"]);
                    Station.PositionY = Convert.ToDecimal(dataReader["PositionY"]);
                    Station.MasseDeau = dataReader["MasseDeau"].ToString();
                    Station.SuiviSATE = Convert.ToInt32(dataReader["SuiviSATE"]);
                    Station.ZRV = Convert.ToInt32(dataReader["ZRV"]);
                    Station.Dysfonctionnements = dataReader["Dysfonctionnements"].ToString();
                    Station.CapaciteOrganiqueRecalculee = Convert.ToDecimal(dataReader["CapaciteOrganiqueRecalculee"]);
                    Station.NombreVisites = Convert.ToInt32(dataReader["NombreVisites"]);
                    Station.Observations = dataReader["Observations"].ToString();
                    Station.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : new DateTime(0);
                    Station.CreePar = dataReader["CreePar"].ToString();
                    Station.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : DateTime.MinValue;
                    Station.ModifiePar = dataReader["ModifiePar"].ToString();


                    // Affichage des données
                    textNomStation.Text = Station.NomStation;

                    try {
                        int index = comboCollectiviteLocalisation.Items.IndexOf(frmATE55.Collectivites[Station.CodeCollectiviteLocalisation].NomCollectivite + " - " + Station.CodeCollectiviteLocalisation);
                        comboCollectiviteLocalisation.SelectedIndex = index;

                        labelAE.Text = "AE : " + frmATE55.Collectivites[Station.CodeCollectiviteLocalisation].AgenceEau;
                    }
                    catch (Exception) {}

                    textCodeSandre.Text = Station.CodeSANDRE;


                    // TabStation
                    comboTypeStation.Set_SelectedId(Station.idStatut_TypeStation.ToString());

                    if (Station.idStatut_TypeStation != (int)eStatut.NonPrecise) {
                        labelSousType.Visible = comboSousTypeStation.Visible;
                        comboSousTypeStation.Set_SelectedId(Station.idStatut_SousTypeStation.ToString());
                    }

                    checkSuiviSATE.Checked = Station.SuiviSATE == 1;
                    checkZRV.Checked = Station.ZRV == 1;
                    numericCapaciteOrganiqueRecalculee.Value = Station.CapaciteOrganiqueRecalculee;
                    numericDebitRefRecalcule.Value = Station.DebitReferenceRecalcule;
                    textDysfonctionnements.Text = Station.Dysfonctionnements;
                    numericCapaciteStation.Value = Station.Capacite;
                    numericCapaciteDBO.Value = Station.CapaciteDBO5;
                    numericDebitRef.Value = Station.DebitReference;
                    numericAnneeConstruction.Value = Station.AnneeConstruction;
                    textComplementModeGestion.Text = Station.ComplementModeGestion;
                    comboFiliereBoue.Set_SelectedId(Station.idStatut_TypeFiliereBoue.ToString());
                    comboModeGestion.Set_SelectedId(Station.idStatut_ModeGestion.ToString());
                    textExutoireStation.Text = Station.ExutoireStation;
                    numericPosX.Value = Station.PositionX;
                    numericPosY.Value = Station.PositionY;
                    textObservations.Text = Station.Observations;
                    textMasseDeau.Text = Station.MasseDeau;
                    infosModifStation.Text = Station.InfosModif();
                    comboEtatEntretien.Set_SelectedId(Station.idStatut_EtatEntretien.ToString());
                    comboEtatOuvrages.Set_SelectedId(Station.idStatut_EtatOuvrages.ToString());
                    numericNbVisites.Value = Station.NombreVisites;


                    dataReader.Close();


                    // On récupère le maître d'ouvrage contenu dans la table éligibilité
                    req = "SELECT AssainissementCollectif FROM Eligibilite WHERE CodeCollectivite = " + Station.CodeCollectiviteLocalisation + " AND AnneeEligibilite = " + DateTime.Today.Year;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        labelMO.Text = dataReader["AssainissementCollectif"].ToString();
                    }
                    dataReader.Close();

                    // Taux de dépollution (DBO5, DCO, Azote)
                    decimal TauxDepollutionDBO5 = CalculerTauxDepollution((int)eStatut.Parametre_DBO5, idStation);
                    decimal TauxDepollutionDCO = CalculerTauxDepollution((int)eStatut.Parametre_DCO, idStation);
                    decimal TauxDepollutionAzote = CalculerTauxDepollution((int)eStatut.Parametre_NGL, idStation);

                    labelTauxDepollutionDBO5.Text = TauxDepollutionDBO5 == -1 ? "" : "Taux de dépollution (DBO5) : " + TauxDepollutionDBO5 + " %";
                    labelTauxDepollutionDCO.Text = TauxDepollutionDCO == -1 ? "" : "Taux de dépollution (DCO) : " + TauxDepollutionDCO + " %";
                    labelTauxDepollutionAzote.Text = TauxDepollutionAzote == -1 ? "" : "Taux de dépollution (Azote) : " + TauxDepollutionAzote + " %";


                    // On affiche les réseaux associés à la station
                    this.AfficherReseaux(idStation);
                    this.RemplirCombosAnneesMesures(idStation);
                    this.AfficherCollectivites(idStation);


                    // Conformité
                    // On récupère les conformités de chaque mesure du dernier bilan
                    // Si une d'entre elle n'est pas conforme alors la station n'est pas conforme
                    // (on ignore le sans objet)
                    if (comboDatesBilans.Items.Count > 0) {
                        req = "SELECT idStatut_EtatConformite FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " AND DateMesure = '" + comboDatesBilans.Items[0].ToString() + "'";
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        bool Conforme = true;

                        if (dataReader != null && dataReader.HasRows) {
                            while (dataReader.Read()) {

                                Conforme = Conforme && Convert.ToInt32(dataReader["idStatut_EtatConformite"]) != (int)eStatut.MesureNonConforme;

                            }
                        }
                        dataReader.Close();

                        labelConformiteStation.Text = Conforme ? "Conforme" : "Non conforme";
                        labelConformiteStation.ForeColor = Conforme ? Color.Green : Color.Red;

                    }



                    panelStation.Visible = true;

                    enregistrerStationBouton.Enabled = annulerStationBouton.Enabled = false;

                }
                else {
                    dataReader.Close();
                    panelStation.Visible = false;
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

        private void AfficherReseauSelectionne(int idReseau) {

            this.ViderControlsReseau();

            try {

                string req = "SELECT * FROM Reseau WHERE idReseau = " + idReseau;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CReseau Reseau = new CReseau();

                    Reseau.idReseau = idReseau;
                    Reseau.idStationReseau = Convert.ToInt32(dataReader["idStationReseau"]);
                    Reseau.idStatut_TypeReseau = Convert.ToInt32(dataReader["idStatut_TypeReseau"]);
                    Reseau.CodeCollectiviteCT = dataReader["CodeCollectiviteCT"].ToString();
                    Reseau.NombreBranchements = Convert.ToInt32(dataReader["NombreBranchements"]);
                    Reseau.NombreEqH = Convert.ToInt32(dataReader["NombreEqH"]);
                    Reseau.NombreDeversoirsOrage = Convert.ToInt32(dataReader["NombreDeversoirsOrage"]);
                    Reseau.NombreStationsPompage = Convert.ToInt32(dataReader["NombreStationsPompage"]);
                    Reseau.Longueur = Convert.ToDecimal(dataReader["Longueur"]);
                    Reseau.MilieuSensible = Convert.ToInt32(dataReader["MilieuSensible"]);
                    Reseau.NombreRaccordes = Convert.ToInt32(dataReader["NombreRaccordes"]);
                    Reseau.NombreRaccordables = Convert.ToInt32(dataReader["NombreRaccordables"]);
                    Reseau.UsagersND = Convert.ToInt32(dataReader["UsagersND"]);
                    Reseau.UsagersAssimilesDomestiques = Convert.ToInt32(dataReader["UsagersAssimilesDomestiques"]);
                    Reseau.CreeLe = dataReader["CreeLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["CreeLe"]) : new DateTime(0);
                    Reseau.CreePar = dataReader["CreePar"].ToString();
                    Reseau.ModifieLe = dataReader["ModifieLe"].GetType().Name != "DBNull" ? Convert.ToDateTime(dataReader["ModifieLe"]) : DateTime.MinValue;
                    Reseau.ModifiePar = dataReader["ModifiePar"].ToString();

                    dataReader.Close();


                    // Affichage des données
                    comboTypeReseau.Set_SelectedId(Reseau.idStatut_TypeReseau.ToString());
                    numericNbBranchements.Value = Reseau.NombreBranchements;
                    numericNbEqH.Value = Reseau.NombreEqH;
                    numericNbStationsPompage.Value = Reseau.NombreStationsPompage;
                    numericNbDeversoirsOrage.Value = Reseau.NombreDeversoirsOrage;
                    numericLongueur.Value = Reseau.Longueur;
                    checkMilieuSensible.Checked = Reseau.MilieuSensible == 1;
                    numericNbRaccordes.Value = Reseau.NombreRaccordes;
                    numericNbRaccordables.Value = Reseau.NombreRaccordables;
                    numericUsagersND.Value = Reseau.UsagersND;
                    numericUsagersAD.Value = Reseau.UsagersAssimilesDomestiques;


                    // Collectivité
                    try {
                        int index = comboCollectiviteReseau.Items.IndexOf(frmATE55.Collectivites[Reseau.CodeCollectiviteCT].NomCollectivite + " - " + Reseau.CodeCollectiviteCT);
                        comboCollectiviteReseau.SelectedIndex = index;
                    }
                    catch (Exception e) { e.ToString(); }

                    // On récupère le nom du maître d'ouvrage
                    req = "SELECT AssainissementCollectif FROM Eligibilite WHERE CodeCollectivite = " + Reseau.CodeCollectiviteCT + " AND AnneeEligibilite = " + DateTime.Today.Year;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        labelMOReseau.Text = dataReader["AssainissementCollectif"].ToString();
                    }
                    dataReader.Close();

                    infosModifsReseau.Text = Reseau.InfosModif();


                    enregistrerReseauBouton.Enabled = annulerReseauBouton.Enabled = false;

                    tabReseau.Visible = true;

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

        private void AfficherMesureBilanSelectionnee(int idMesure) {

            this.ViderControlsMesuresBilan();


            if (idMesure != -1) {

                try {

                    // On récupère la mesure
                    string req = "SELECT idStatut_TypeParametre,MesureConcentrationEntree,MesureConcentrationSortie,MesureFluxEntree,MesureFluxSortie,Rendement,idStatut_EtatConformite FROM Mesure,Statut WHERE idMesure = " + idMesure+" ORDER BY OrdreTriStatut ASC";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        comboParametresBilan.Set_SelectedId(dataReader["idStatut_TypeParametre"].ToString());

                        numericBilanFluxEntree.Value = Convert.ToInt32(dataReader["MesureFluxEntree"]);
                        numericBilanFluxSortie.Value = Convert.ToInt32(dataReader["MesureFluxSortie"]);
                        numericBilanConcentrationEntree.Value = Convert.ToInt32(dataReader["MesureConcentrationEntree"]);
                        numericBilanConcentrationSortie.Value = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                        numericBilanRendement.Value = Convert.ToInt32(dataReader["Rendement"]);
                        comboConformiteBilan.Set_SelectedId(dataReader["idStatut_EtatConformite"].ToString());

                        panelMesuresBilan.Visible = true;

                    }
                    else
                        panelMesuresBilan.Visible = false;

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
            else
                panelMesuresBilan.Visible = false;

            enregistrerMesuresBilan.Enabled = annulerMesuresBilan.Enabled = false;

        }

        private void AfficherMesurePPSelectionnee(int idMesure) {

            this.ViderControlsMesuresPP();


            if (idMesure != -1) {

                try {

                    // On récupère la mesure
                    string req = "SELECT idStatut_TypeParametre,MesureConcentrationEntree,MesureConcentrationSortie,MesureConcentrationPointIntermediaire1,MesureConcentrationPointIntermediaire2,Rendement,idStatut_EtatConformite FROM Mesure,Statut WHERE idMesure = " + idMesure + " ORDER BY OrdreTriStatut ASC";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        comboParametresPP.Set_SelectedId(dataReader["idStatut_TypeParametre"].ToString());

                        numericPPEntree.Value = Convert.ToInt32(dataReader["MesureConcentrationEntree"]);
                        numericPPSortie.Value = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                        numericPPSortieFiltre1.Value = Convert.ToInt32(dataReader["MesureConcentrationPointIntermediaire1"]);
                        numericPPSortieFiltre2.Value = Convert.ToInt32(dataReader["MesureConcentrationPointIntermediaire2"]);
                        numericPPRendement.Value = Convert.ToInt32(dataReader["Rendement"]);
                        comboConformitePP.Set_SelectedId(dataReader["idStatut_EtatConformite"].ToString());

                        panelMesuresPP.Visible = true;

                    }
                    else
                        panelMesuresPP.Visible = false;

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
            else
                panelMesuresPP.Visible = false;

            enregistrerMesurePPbutton.Enabled = annulerMesurePP.Enabled = false;

        }

        private void AfficherCollectivites(int idStation) {

            DataGridView dgv = dataGridViewCollectivitesStation;
            dgv.Rows.Clear();
            dgv.Refresh();

            try {

                // Affichage des collectivités impactées
                string req = "SELECT CodeCollectivite FROM Station_Collectivite WHERE idStation = " + idStation;
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

                    int i = dgv.Rows.Add();
                    DataGridViewRow row = dgv.Rows[i];

                    row.Cells["CodeCollectiviteImpacStation"].Value = c;
                    row.Cells["CollectiviteImpacStation"].Value = frmATE55.Collectivites[c].NomCollectivite;

                    // Population
                    req = "SELECT PopulationDGF FROM Eligibilite WHERE CodeCollectivite = " + c + " ORDER BY AnneeEligibilite DESC";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();
                        row.Cells["PopDGFStation"].Value = dataReader["PopulationDGF"].ToString();
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

                        row.Cells["PopDGFStation"].Value = population;

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

        private void UpdateRowStation(int idStation, int index, bool nouvelle = false) {

            if (index != -1 && idStation != -1) {

                DataGridViewRow row = dataGridViewStations.Rows[index];

                try {

                    // On récupère la station
                    string req = "SELECT idStation,NomStation,idStatut_TypeStation,idStatut_SousTypeStation,AnneeConstruction,Capacite,SuiviSATE FROM StationAssainissement WHERE idStation = " + idStation;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();


                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        CStationAssainissement Station = new CStationAssainissement();

                        Station.idStation = Convert.ToInt32(dataReader["idStation"]);
                        Station.idStatut_TypeStation = Convert.ToInt32(dataReader["idStatut_TypeStation"]);
                        Station.idStatut_SousTypeStation = Convert.ToInt32(dataReader["idStatut_SousTypeStation"]);
                        Station.AnneeConstruction = Convert.ToInt32(dataReader["AnneeConstruction"]);
                        Station.NomStation = dataReader["NomStation"].ToString();
                        Station.Capacite = Convert.ToInt32(dataReader["Capacite"]);
                        Station.SuiviSATE = Convert.ToInt32(dataReader["SuiviSATE"]);

                        dataReader.Close();


                        // Affichage des données           
                        row.Cells["idStation"].Value = Station.idStation;
                        row.Cells["CapaciteStation"].Value = Station.Capacite + " EqH";
                        row.Cells["TypeStation"].Value = Station.idStatut_SousTypeStation != 0 ? frmATE55.Statuts[Station.idStatut_SousTypeStation].LibelleStatut : (Station.idStatut_TypeStation != 0 ? frmATE55.Statuts[Station.idStatut_TypeStation].LibelleStatut : "");
                        row.Cells["AnneeConstruction"].Value = Station.AnneeConstruction;
                        row.Cells["CollectiviteStation"].Value = Station.NomStation;


                        // Si la station est suivie par le sate on l'affiche en bleu
                        if (Station.SuiviSATE == 1)
                            row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Bleu"];
                        else
                            row.DefaultCellStyle.BackColor = Color.White;


                        if (nouvelle) {
                            dataGridViewStations.ClearSelection();
                            row.Selected = true;
                            dataGridViewStations.FirstDisplayedScrollingRowIndex = dataGridViewStations.SelectedRows[0].Index;
                        }
                        AfficherStationSelectionnee(idStation);

                    }
                    else
                        dataReader.Close();

                    enregistrerStationBouton.Enabled = annulerStationBouton.Enabled = false;

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

        private void UpdateRowReseau(int idReseau, int index, bool nouvelle = false) {

            DataGridViewRow row = dataGridViewReseaux.Rows[index];

            try {

                string req = "SELECT idReseau,CodeCollectiviteCT,idStatut_TypeReseau,Longueur FROM Reseau WHERE idReseau = " + idReseau;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();


                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CReseau Reseau = new CReseau();

                    Reseau.idReseau = Convert.ToInt32(dataReader["idReseau"]);
                    Reseau.CodeCollectiviteCT = dataReader["CodeCollectiviteCT"].ToString();
                    Reseau.idStatut_TypeReseau = Convert.ToInt32(dataReader["idStatut_TypeReseau"]);
                    Reseau.Longueur = Convert.ToDecimal(dataReader["Longueur"]);

                    dataReader.Close();

                    // Affichage
                    row.Cells["idReseau"].Value = Reseau.idReseau;
                    row.Cells["TypeReseau"].Value = frmATE55.Statuts[Reseau.idStatut_TypeReseau].LibelleStatut;
                    row.Cells["LineaireReseau"].Value = Decimal.Round(Reseau.Longueur, 2) + " km";

                    try {
                        row.Cells["NomCTReseau"].Value = frmATE55.Collectivites[Reseau.CodeCollectiviteCT].NomCollectivite;
                    }
                    catch (KeyNotFoundException e) { e.ToString(); }

                    // On récupère le nom du maître d'ouvrage dans l'éligibilité
                    req = "SELECT AssainissementCollectif FROM Eligibilite WHERE CodeCollectivite = " + Reseau.CodeCollectiviteCT + " AND AnneeEligibilite = " + DateTime.Today.Year;
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    if (dataReader != null && dataReader.HasRows) {
                        dataReader.Read();

                        row.Cells["NomMOReseau"].Value = dataReader["AssainissementCollectif"].ToString();
                    }
                    dataReader.Close();

                }
                else
                    dataReader.Close();

                tabReseau.Visible = false;

                if (nouvelle) {
                    dataGridViewReseaux.ClearSelection();
                    row.Selected = true;
                    dataGridViewReseaux.FirstDisplayedScrollingRowIndex = dataGridViewReseaux.SelectedRows[0].Index;
                }
                AfficherReseauSelectionne(idReseau);

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

        private void UpdateRowBilan(int idMesure, int index) {

            if (index != -1) {

                DataGridViewRow row = dataGridViewBilan.Rows[index];

                // On récupère les mesures du paramètre pour la date sélectionnée
                string req = "SELECT * FROM Mesure WHERE idMesure = " + idMesure;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();



                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CMesure Mesure = new CMesure();

                    Mesure.idMesure = Convert.ToInt32(dataReader["idMesure"]);
                    Mesure.idStatut_TypeParametre = Convert.ToInt32(dataReader["idStatut_TypeParametre"]);
                    Mesure.MesureFluxEntree = Convert.ToInt32(dataReader["MesureFluxEntree"]);
                    Mesure.MesureFluxSortie = Convert.ToInt32(dataReader["MesureFluxSortie"]);
                    Mesure.MesureConcentrationEntree = Convert.ToInt32(dataReader["MesureConcentrationEntree"]);
                    Mesure.MesureConcentrationSortie = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                    Mesure.Rendement = Convert.ToInt32(dataReader["Rendement"]);


                    dataReader.Close();


                    row.Cells["idMesure"].Value = Mesure.idMesure;
                    row.Cells["BilanParametre"].Value = frmATE55.Statuts[Mesure.idStatut_TypeParametre].LibelleStatut;
                    row.Cells["BilanFluxEntree"].Value = Mesure.MesureFluxEntree + " kg/j";
                    row.Cells["BilanFluxSortie"].Value = Mesure.MesureFluxSortie + " kg/j";
                    row.Cells["BilanConcentrationEntree"].Value = Mesure.MesureConcentrationEntree + " mg/l";
                    row.Cells["BilanConcentrationSortie"].Value = Mesure.MesureConcentrationSortie + " mg/l";
                    row.Cells["BilanRendement"].Value = Mesure.Rendement + " %";


                    panelMesuresBilan.Visible = true;

                }
                else
                    dataReader.Close();

            }
            else
                panelMesuresBilan.Visible = false;

        }

        private void UpdateRowPP(int idMesure, int index) {

            if (index != -1) {

                DataGridViewRow row = dataGridViewPP.Rows[index];

                // On récupère les mesures du paramètre pour la date sélectionnée
                string req = "SELECT * FROM Mesure WHERE idMesure = " + idMesure;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();



                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CMesure Mesure = new CMesure();

                    Mesure.idMesure = Convert.ToInt32(dataReader["idMesure"]);
                    Mesure.idStatut_TypeParametre = Convert.ToInt32(dataReader["idStatut_TypeParametre"]);
                    Mesure.MesureConcentrationEntree = Convert.ToInt32(dataReader["MesureConcentrationEntree"]);
                    Mesure.MesureConcentrationSortie = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                    Mesure.MesureConcentrationPointIntermediaire1 = Convert.ToInt32(dataReader["MesureConcentrationPointIntermediaire1"]);
                    Mesure.MesureConcentrationPointIntermediaire2 = Convert.ToInt32(dataReader["MesureConcentrationPointIntermediaire2"]);
                    Mesure.Rendement = Convert.ToInt32(dataReader["Rendement"]);


                    dataReader.Close();


                    row.Cells["idMesurePP"].Value = Mesure.idMesure;
                    row.Cells["ParametrePP"].Value = frmATE55.Statuts[Mesure.idStatut_TypeParametre].LibelleStatut;
                    row.Cells["ConcentrationEntreePP"].Value = Mesure.MesureConcentrationEntree + " mg/l";
                    row.Cells["ConcentrationSortiePP"].Value = Mesure.MesureConcentrationSortie + " mg/l";
                    row.Cells["ConcentrationSortieFiltre1"].Value = Mesure.MesureConcentrationPointIntermediaire1 + " mg/l";
                    row.Cells["ConcentrationSortieFiltre2"].Value = Mesure.MesureConcentrationPointIntermediaire2 + " mg/l";
                    row.Cells["RendementPP"].Value = Mesure.Rendement + " %";


                    panelMesuresPP.Visible = true;

                }
                else
                    dataReader.Close();

            }
            else
                panelMesuresPP.Visible = false;

        }

        private void RemplirCombosAnneesMesures(int idStation) {

            comboDatesBilans.Items.Clear();
            comboDatesPP.Items.Clear();


            // BILANS 24H
            string req = "SELECT DISTINCT DateMesure FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " ORDER BY DateMesure DESC";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            // Parcourt des dates
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    comboDatesBilans.Items.Add(Convert.ToDateTime(dataReader["DateMesure"]).ToShortDateString());

                }
                dataReader.Close();
            }
            else
                dataReader.Close();

            

            // PRELEVEMENTS PONCTUELS
            req = "SELECT DISTINCT DateMesure FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_PP + " ORDER BY DateMesure DESC";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            // Parcourt des dates
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    comboDatesPP.Items.Add(Convert.ToDateTime(dataReader["DateMesure"]).ToShortDateString());

                }
                dataReader.Close();
            }
            else
                dataReader.Close();

            dataGridViewBilan.Rows.Clear();
            dataGridViewBilan.Refresh();
            dataGridViewPP.Rows.Clear();
            dataGridViewPP.Refresh();

            try {
                comboDatesBilans.SelectedIndex = comboDatesBilans.Items.Count > 0 ? 0 : -1;
                comboDatesPP.SelectedIndex = comboDatesPP.Items.Count > 0 ? 0 : -1;
            }
            catch (ArgumentOutOfRangeException) { }

        }

        private decimal CalculerTauxDepollution(int Type, int idStation) {
            
            int TauxParametre = -1;

            switch (Type) {
                case (int)eStatut.Parametre_DBO5:
                    TauxParametre = Session.DepollutionDBO5;
                    break;
                case (int)eStatut.Parametre_DCO:
                    TauxParametre = Session.DepollutionDCO;
                    break;
                case (int)eStatut.Parametre_NGL:
                    TauxParametre = Session.DepollutionAzote;
                    break;
                default:
                    return -1;
            }

            // Calcul du nombre d'habitants
            // Habitants = usagers raccordables + AD + ND (somme des habitants de chaque réseau)
            int Habitants = 0;
            // Récupération des réseaux
            string req = "SELECT NombreRaccordables,UsagersND,UsagersAssimilesDomestiques FROM Reseau WHERE idStationReseau = " + idStation;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();
            // On parcourt les réseaux
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read())
                    Habitants += Convert.ToInt32(dataReader["NombreRaccordables"]) + Convert.ToInt32(dataReader["UsagersND"]) + Convert.ToInt32(dataReader["UsagersAssimilesDomestiques"]);
            }
            dataReader.Close();

            decimal FluxEntree = -1;
            decimal Rendement = -1;
            // Récupération du dernier rendement et du dernier flux entrée de la station du paramètre du bilan
            req = "SELECT TOP 1 MesureFluxEntree,Rendement FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeParametre = " + Type + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " ORDER BY DateMesure DESC";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();

                FluxEntree = Convert.ToDecimal(dataReader["MesureFluxEntree"]);
                Rendement = Convert.ToDecimal(dataReader["Rendement"]);

            }
            dataReader.Close();


            // Si une des valeurs n'a pas de données on s'arrête ici
            if (Habitants == 0 || FluxEntree == -1 || Rendement == -1)
                return -1;


            // Calcul du taux de collecte
            // Taux de collecte = (Flux entrée) / (habitants * taux du paramètre) * 100
            decimal TauxCollecte = (FluxEntree / (Habitants * TauxParametre)) * 1000;

            // Calcul du taux de dépollution
            // Taux de dépollution = (Taux de collecte * rendement) / 100
            decimal TauxDepollution = TauxCollecte * Rendement;

            return Decimal.Round(TauxDepollution, 2);
        }

        private void Rechercher() {

            // On récupère le texte recherché
            string Recherche = textRecherche.Text.ToLower();

            // On parcourt les lignes du datagridview
            foreach (DataGridViewRow row in dataGridViewStations.Rows) {

                string NomCollectivite = row.Cells["CollectiviteStation"].Value.ToString().ToLower();
                string Capacite = row.Cells["CapaciteStation"].Value.ToString();
                string Type = row.Cells["TypeStation"].Value.ToString().ToLower();
                string AnneeConstruction = row.Cells["AnneeConstruction"].Value.ToString();

                row.Visible = NomCollectivite.Contains(Recherche) || Capacite.Contains(Recherche) || Type.Contains(Recherche) || AnneeConstruction.Contains(Recherche);

                // Si la case est cochée et que la station n'est pas suivie par le SATE (fond blanc) on masque
                if (checkMasquerStations.Checked && row.DefaultCellStyle.BackColor != frmATE55.Couleurs["Bleu"])
                    row.Visible = false;

            }

        }

        private void EnregistrerStation(int idStation) {

            enregistrerStationBouton.Enabled = false;

            CStationAssainissement Station = new CStationAssainissement(Session);

            Station.idStation = idStation;

            // Récupération des données
            Station.NomStation = textNomStation.Text;
            Station.idStatut_TypeFiliereBoue = comboFiliereBoue.Get_SelectedId().Equals("") ? 0 : Convert.ToInt32(comboFiliereBoue.Get_SelectedId());
            Station.idStatut_TypeStation = comboTypeStation.Get_SelectedId().Equals("") ? 0 : Convert.ToInt32(comboTypeStation.Get_SelectedId());
            Station.idStatut_SousTypeStation = comboSousTypeStation.Get_SelectedId().Equals("") ? 0 : Convert.ToInt32(comboSousTypeStation.Get_SelectedId());
            Station.idStatut_EtatOuvrages = comboEtatOuvrages.Get_SelectedId().Equals("") ? 0 : Convert.ToInt32(comboEtatOuvrages.Get_SelectedId());
            Station.idStatut_EtatEntretien = comboEtatEntretien.Get_SelectedId().Equals("") ? 0 : Convert.ToInt32(comboEtatEntretien.Get_SelectedId());
            Station.CodeSANDRE = textCodeSandre.Text;
            Station.Capacite = (int)numericCapaciteStation.Value;
            Station.CapaciteDBO5 = (int)numericCapaciteDBO.Value;
            Station.DebitReference = numericDebitRef.Value;
            Station.AnneeConstruction = (int)numericAnneeConstruction.Value;
            Station.PositionX = numericPosX.Value;
            Station.PositionY = numericPosY.Value;
            Station.idStatut_ModeGestion = comboModeGestion.Get_SelectedId().Equals("") ? 0 : Convert.ToInt32(comboModeGestion.Get_SelectedId());
            Station.ComplementModeGestion = textComplementModeGestion.Text;
            Station.ExutoireStation = textExutoireStation.Text;
            Station.Observations = textObservations.Text;
            Station.MasseDeau = textMasseDeau.Text;
            Station.SuiviSATE = checkSuiviSATE.Checked ? 1 : 0;
            Station.ZRV = checkZRV.Checked ? 1 : 0;
            Station.Dysfonctionnements = textDysfonctionnements.Text;
            Station.DebitReferenceRecalcule = numericDebitRefRecalcule.Value;
            Station.CapaciteOrganiqueRecalculee = numericCapaciteOrganiqueRecalculee.Value;
            Station.NombreVisites = (int)numericNbVisites.Value;

            // Code collectivité
            if (comboCollectiviteLocalisation.SelectedIndex != -1) {
                String[] s = comboCollectiviteLocalisation.SelectedItem.ToString().Split(null);
                Station.CodeCollectiviteLocalisation = s[s.Length - 1];
            }
            else
                Station.CodeCollectiviteLocalisation = "55000";


            if (idStation != -1) {
                // On enregistre la station
                if (Station.Enregistrer()) {

                    // On récupère l'index de la station
                    int index = -1;
                    foreach (DataGridViewRow row in dataGridViewStations.Rows) {
                        if (Convert.ToInt32(row.Cells["idStation"].Value) == idStation)
                            index = row.Index;
                    }

                    UpdateRowStation(idStation, index);
                }
            }
            else {
                // Sinon on la crée
                if (Station.Creer())
                    UpdateRowStation(Station.idStation, dataGridViewStations.Rows.Add(), true);
            }


        }

        private void EnregistrerReseau(int idReseau) {

            enregistrerReseauBouton.Enabled = false;

            CReseau Reseau = new CReseau(Session);

            Reseau.idReseau = idReseau;
            Reseau.idStationReseau = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);
            Reseau.idStatut_TypeReseau = comboTypeReseau.Get_SelectedId().Equals("") ? 0 : Convert.ToInt32(comboTypeReseau.Get_SelectedId());
            Reseau.NombreBranchements = (int)numericNbBranchements.Value;
            Reseau.NombreEqH = (int)numericNbEqH.Value;
            Reseau.NombreDeversoirsOrage = (int)numericNbDeversoirsOrage.Value;
            Reseau.NombreStationsPompage = (int)numericNbStationsPompage.Value;
            Reseau.Longueur = numericLongueur.Value;
            Reseau.MilieuSensible = checkMilieuSensible.Checked ? 1 : 0;
            Reseau.NombreRaccordes = (int)numericNbRaccordes.Value;
            Reseau.NombreRaccordables = (int)numericNbRaccordables.Value;
            Reseau.UsagersND = (int)numericUsagersND.Value;
            Reseau.UsagersAssimilesDomestiques = (int)numericUsagersAD.Value;


            // Code collectivité
            if (comboCollectiviteReseau.SelectedIndex != -1) {
                String[] s = comboCollectiviteReseau.SelectedItem.ToString().Split(null);
                Reseau.CodeCollectiviteCT = s[s.Length - 1];
            }
            else
                Reseau.CodeCollectiviteCT = "55000";


            if (idReseau != -1) {
                // On enregistre
                if (Reseau.Enregistrer()) {

                    // On récupère l'index du réseau
                    int index = -1;
                    foreach (DataGridViewRow row in dataGridViewReseaux.Rows) {
                        if (Convert.ToInt32(row.Cells["idReseau"].Value) == idReseau)
                            index = row.Index;
                    }

                    UpdateRowReseau(idReseau, index);

                }
            }
            else {
                // On crée
                if (Reseau.Creer())
                    UpdateRowReseau(Reseau.idReseau, dataGridViewReseaux.Rows.Add(), true);
            }

        }

        private void EnregistrerMesureBilan(int idMesure) {

            if (idMesure != -1) {

                CMesure Mesure = new CMesure(Session);
                Mesure.idMesure = idMesure;

                Mesure.MesureConcentrationEntree = numericBilanConcentrationEntree.Value;
                Mesure.MesureConcentrationSortie = numericBilanConcentrationSortie.Value;
                Mesure.MesureFluxEntree = numericBilanFluxEntree.Value;
                Mesure.MesureFluxSortie = numericBilanFluxSortie.Value;
                Mesure.Rendement = numericBilanRendement.Value;
                Mesure.idStatut_EtatConformite = Convert.ToInt32(comboConformiteBilan.Get_SelectedId());

                Mesure.MesureConcentrationPointIntermediaire1 = Mesure.MesureConcentrationPointIntermediaire2 = 0;

                if (Mesure.Enregistrer()) {

                    int index = -1;
                    foreach (DataGridViewRow row in dataGridViewBilan.Rows) {
                        if (Convert.ToInt32(row.Cells["idMesure"].Value) == idMesure)
                            index = row.Index;
                    }

                    this.AfficherMesureBilanSelectionnee(idMesure);
                    this.UpdateRowBilan(idMesure, index);
                }

                enregistrerMesuresBilan.Enabled = annulerMesuresBilan.Enabled = false;

            }

        }

        private void EnregistrerMesurePP(int idMesure) {

            if (idMesure != -1) {

                CMesure Mesure = new CMesure(Session);
                Mesure.idMesure = idMesure;

                Mesure.MesureConcentrationEntree = numericPPEntree.Value;
                Mesure.MesureConcentrationSortie = numericPPSortie.Value;
                Mesure.MesureConcentrationPointIntermediaire1 = numericPPSortieFiltre1.Value;
                Mesure.MesureConcentrationPointIntermediaire2 = numericPPSortieFiltre2.Value;
                Mesure.Rendement = numericPPRendement.Value;
                Mesure.idStatut_EtatConformite = Convert.ToInt32(comboConformitePP.Get_SelectedId());

                Mesure.MesureFluxSortie = Mesure.MesureFluxEntree = 0;

                if (Mesure.Enregistrer()) {

                    int index = -1;
                    foreach (DataGridViewRow row in dataGridViewPP.Rows) {
                        if (Convert.ToInt32(row.Cells["idMesurePP"].Value) == idMesure)
                            index = row.Index;
                    }

                    this.AfficherMesurePPSelectionnee(idMesure);
                    this.UpdateRowPP(idMesure, index);
                }

                enregistrerMesurePPbutton.Enabled = annulerMesurePP.Enabled = false;

            }

        }

        private void AjouterCollectivite() {

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

            // On récupère et on stocke les collectivités déjà liées à la station pour ne pas les afficher
            string req = "SELECT CodeCollectivite FROM Station_Collectivite WHERE idStation = " + idStation;
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

                // Si la collectivité est déjà dans la liste ou si c'est 55000 on ne l'ajoute pas
                if (CollectivitesImpactees.IndexOf(c.Key) == -1 && !c.Key.Equals("55000"))
                    collectivites.Add((object)c.Value);
            }

            frmListe frm = new frmListe("Ajouter des collectivités à la station", "collectivités", "CodeCollectivite", "NomCollectivite", collectivites);
            var result = frm.ShowDialog();

            // On récupère les collectivités sélectionnées
            if (result == DialogResult.OK) {

                foreach (string CodeCollectivite in frm.listeRetour) {

                    // On teste si la collectivité est déjà liée à la convention
                    req = "SELECT Count(*) FROM Station_Collectivite WHERE idStation = " + idStation + " AND CodeCollectivite = " + CodeCollectivite;
                    command = new SqlCommand(req, Session.oConn);

                    if ((int)command.ExecuteScalar() == 0) {
                        CStation_Collectivite station_collectivite = new CStation_Collectivite(Session);
                        station_collectivite.idStation = idStation;
                        station_collectivite.CodeCollectivite = CodeCollectivite;
                        station_collectivite.Creer();
                    }

                }

                AfficherCollectivites(idStation);
            }

        }

        private void AjouterDateMesuresBilan(int idStation, string DateMesures) {

            if (idStation != -1) {

                // On stocke les paramètres dans une liste
                List<int> IdsParametres = new List<int>();

                foreach (KeyValuePair<int, CStatut> Statut in frmATE55.Statuts) {
                    if (Statut.Value.CategorieStatut.Equals("ParametreMesure"))
                        IdsParametres.Add(Statut.Value.idStatut);
                }

                // Parcourt de la liste
                foreach (int idParametre in IdsParametres) {

                    CMesure Mesure = new CMesure(Session);

                    Mesure.idStationMesure = idStation;
                    Mesure.idStatut_TypeMesure = (int)eStatut.Mesure_Bilan24h;
                    Mesure.idStatut_TypeParametre = idParametre;
                    Mesure.DateMesure = Convert.ToDateTime(DateMesures);

                    // Valeurs par défaut
                    Mesure.MesureConcentrationEntree = Mesure.MesureConcentrationPointIntermediaire1 = Mesure.MesureConcentrationPointIntermediaire2 = Mesure.MesureConcentrationSortie = Mesure.MesureFluxEntree = Mesure.MesureFluxSortie = Mesure.Rendement = 0;

                    if (!Mesure.Creer()) {
                        this.Text = DateMesures + "/" + Mesure.idStatut_TypeParametre;
                        return;
                    }

                }

                this.RemplirCombosAnneesMesures(idStation);

                dateNouvelleDateBilan.Value = DateTime.Today;
                dateNouvelleDateBilan.Checked = boutonAjouterBilan.Enabled = false;

            }

        }

        private void AjouterDateMesuresPP(int idStation, string DateMesures) {

            if (idStation != -1) {

                // On stocke les paramètres dans une liste
                List<int> IdsParametres = new List<int>();

                foreach (KeyValuePair<int, CStatut> Statut in frmATE55.Statuts) {
                    if (Statut.Value.CategorieStatut.Equals("ParametreMesure"))
                        IdsParametres.Add(Statut.Value.idStatut);
                }

                // Parcourt de la liste
                foreach (int idParametre in IdsParametres) {

                    CMesure Mesure = new CMesure(Session);

                    Mesure.idStationMesure = idStation;
                    Mesure.idStatut_TypeMesure = (int)eStatut.Mesure_PP;
                    Mesure.idStatut_TypeParametre = idParametre;
                    Mesure.DateMesure = Convert.ToDateTime(DateMesures);

                    // Valeurs par défaut
                    Mesure.MesureConcentrationEntree = Mesure.MesureConcentrationPointIntermediaire1 = Mesure.MesureConcentrationPointIntermediaire2 = Mesure.MesureConcentrationSortie = Mesure.MesureFluxEntree = Mesure.MesureFluxSortie = Mesure.Rendement = 0;

                    if (!Mesure.Creer()) {
                        this.Text = DateMesures + "/" + Mesure.idStatut_TypeParametre;
                        return;
                    }

                }

                this.RemplirCombosAnneesMesures(idStation);

                dateNouvelleDatePP.Value = DateTime.Today;
                dateNouvelleDatePP.Checked = ajoutDatePP.Enabled = false;

            }

        }

        private void SupprimerDateMesuresBilan(int idStation, string DateMesures) {

            if (idStation != -1) {

                // On récupère les mesures de la station à cette date
                string req = "SELECT idMesure FROM Mesure WHERE idStationMesure = " + idStation + " AND DateMesure = '" + DateMesures + "' AND idStatut_TypeMesure = "+(int)eStatut.Mesure_Bilan24h;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On stocke dans une liste
                List<int> IdsMesures = new List<int>();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read())
                        IdsMesures.Add(Convert.ToInt32(dataReader["idMesure"]));         
                }

                dataReader.Close();


                // On parcourt la liste pour supprimer
                foreach(int idMesure in IdsMesures)
                    CMesure.Supprimer(Session, idMesure);              

                this.RemplirCombosAnneesMesures(idStation);

                boutonSupprimerDateBilan.Enabled = false;
            }

        }

        private void SupprimerDateMesuresPP(int idStation, string DateMesures) {

            if (idStation != -1) {

                // On récupère les mesures de la station à cette date
                string req = "SELECT idMesure FROM Mesure WHERE idStationMesure = " + idStation + " AND DateMesure = '" + DateMesures + "' AND idStatut_TypeMesure = " + (int)eStatut.Mesure_PP;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On stocke dans une liste
                List<int> IdsMesures = new List<int>();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read())
                        IdsMesures.Add(Convert.ToInt32(dataReader["idMesure"]));
                }

                dataReader.Close();


                // On parcourt la liste pour supprimer
                foreach (int idMesure in IdsMesures)
                    CMesure.Supprimer(Session, idMesure);

                this.RemplirCombosAnneesMesures(idStation);

                supprimerDatePP.Enabled = false;
            }

        }

        private void ViderControls() {
            labelConformiteStation.Text = textDysfonctionnements.Text = labelAE.Text = textNomStation.Text = textCodeSandre.Text = textComplementModeGestion.Text = labelMO.Text = textExutoireStation.Text = textMasseDeau.Text = textCodeSandre.Text = textNomStation.Text = "";
            checkZRV.Checked = labelComplement.Visible = textComplementModeGestion.Visible = labelSousType.Visible = comboSousTypeStation.Visible = checkSuiviSATE.Checked = panelMesuresBilan.Visible = panelMesuresPP.Visible = dateNouvelleDateBilan.Checked = false;
            numericCapaciteOrganiqueRecalculee.Value = numericDebitRefRecalcule.Value = numericAnneeConstruction.Value = numericCapaciteStation.Value = numericPosX.Value = numericPosY.Value = numericDebitRef.Value = numericCapaciteDBO.Value = numericNbVisites.Value = 0;
            comboFiliereBoue.SelectedIndex = comboCollectiviteLocalisation.SelectedIndex = comboTypeStation.SelectedIndex = comboSousTypeStation.SelectedIndex = comboModeGestion.SelectedIndex = comboParametresPP.SelectedIndex = comboEtatEntretien.SelectedIndex = comboEtatOuvrages.SelectedIndex = -1;
            dateNouvelleDateBilan.Value = DateTime.Today;
        }
        
        private void ViderControlsReseau() {
            numericLongueur.Value = numericNbBranchements.Value = numericNbEqH.Value = numericNbDeversoirsOrage.Value = numericNbStationsPompage.Value = numericUsagersND.Value = numericUsagersAD.Value = numericNbRaccordables.Value = numericNbRaccordes.Value = 0;
            checkMilieuSensible.Checked = false;
            labelMOReseau.Text = "";
            comboTypeReseau.SelectedIndex = comboCollectiviteReseau.SelectedIndex = -1;
        }

        private void ViderControlsMesuresBilan() {
            numericBilanFluxEntree.Value = numericBilanFluxSortie.Value = numericBilanConcentrationEntree.Value = numericBilanConcentrationSortie.Value = numericBilanRendement.Value = 0;
            dateNouvelleDateBilan.Checked = false;
            dateNouvelleDateBilan.Value = DateTime.Today;
            comboParametresBilan.SelectedIndex = -1;
            comboConformiteBilan.Set_SelectedId("0");
        }

        private void ViderControlsMesuresPP() {
            numericPPEntree.Value = numericPPSortie.Value = numericPPSortieFiltre1.Value = numericPPSortieFiltre2.Value = numericPPRendement.Value = 0;
            dateNouvelleDatePP.Checked = false;
            dateNouvelleDatePP.Value = DateTime.Today;
            comboParametresPP.SelectedIndex = -1;
            comboConformitePP.Set_SelectedId("0");
        }

        private void GenererGraphiques(int idStation) {

            Color CouleurEnTetes = Color.LightBlue;

            Microsoft.Office.Interop.Excel.Application ApplicationXL;
            Microsoft.Office.Interop.Excel._Workbook ClasseurXL;
            Microsoft.Office.Interop.Excel._Worksheet FeuilXL_Station;
            Microsoft.Office.Interop.Excel._Worksheet FeuilXL_Reseau;
            Microsoft.Office.Interop.Excel._Worksheet FeuilXL_Mesures;
            Microsoft.Office.Interop.Excel._Worksheet FeuilXL_Graphiques;
            
            StringBuilder errorMessages = new StringBuilder();


            try {

                // On stocke les ids des paramètres
                List<int> IdsParams = new List<int>();

                string req = "SELECT idStatut FROM Statut WHERE CategorieStatut = 'ParametreMesure' ORDER BY OrdreTriStatut ASC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        IdsParams.Add(Convert.ToInt32(dataReader["idStatut"]));

                    }
                }
                dataReader.Close();


                // On stocke les dates dans des listes
                List<string> DatesBilans = new List<string>();
                List<string> DatesPP = new List<string>();


                // On récupère les dates des bilans
                req = "SELECT DISTINCT DateMesure FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h+" ORDER BY DateMesure ASC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        DatesBilans.Add(Convert.ToDateTime(dataReader["DateMesure"]).ToShortDateString());

                    }
                }
                dataReader.Close();

                // On récupère les dates des prélèvements ponctuels
                req = "SELECT DISTINCT DateMesure FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_PP + " ORDER BY DateMesure ASC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        DatesPP.Add(Convert.ToDateTime(dataReader["DateMesure"]).ToShortDateString());

                    }
                }
                dataReader.Close();


                // Excel
                ApplicationXL = new Microsoft.Office.Interop.Excel.Application();
                ApplicationXL.Visible = false;

                ClasseurXL = (Microsoft.Office.Interop.Excel._Workbook)ApplicationXL.Workbooks.Add(System.Reflection.Missing.Value);
                FeuilXL_Station = (Microsoft.Office.Interop.Excel._Worksheet)ClasseurXL.ActiveSheet;
                FeuilXL_Reseau = (Microsoft.Office.Interop.Excel._Worksheet)ClasseurXL.Sheets.Add(After: ClasseurXL.Sheets[ClasseurXL.Sheets.Count]);
                FeuilXL_Mesures = (Microsoft.Office.Interop.Excel._Worksheet)ClasseurXL.Sheets.Add(After: ClasseurXL.Sheets[ClasseurXL.Sheets.Count]);
                FeuilXL_Graphiques = (Microsoft.Office.Interop.Excel._Worksheet)ClasseurXL.Sheets.Add(After: ClasseurXL.Sheets[ClasseurXL.Sheets.Count]);

                ((Microsoft.Office.Interop.Excel.Worksheet)ClasseurXL.Sheets[1]).Select();

                FeuilXL_Station.Name = "Station";
                FeuilXL_Reseau.Name = "Réseau";
                FeuilXL_Mesures.Name = "Mesures";
                FeuilXL_Graphiques.Name = "Graphiques";

                // Largeurs cellules
                FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[3, 1], FeuilXL_Mesures.Cells[3, 1]).Columns.ColumnWidth = 30;
                FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[3, 3], FeuilXL_Mesures.Cells[3, 2]).Columns.ColumnWidth = 15;
                FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[3, 3], FeuilXL_Mesures.Cells[3, 3]).Columns.ColumnWidth = 20;

                // Numero de la ligne
                int NumLigneParam = 3;
                // Numéro de la colonne à afficher
                int NumColonne = 4;

                Boolean GraphiqueVolume = false;

                int NumLigneRejetsStations = 6;


                // ------ STATION - REJETS STATION -------
                if (DatesBilans.Count > 0 || DatesPP.Count > 0) {

                    FeuilXL_Station.Cells[1, 1] = "Rejets Station";

                    // Headers
                    FeuilXL_Station.Cells[3, 1] = "Paramètre";
                    FeuilXL_Station.Cells[3, 2] = "Norme de rejet";
                    FeuilXL_Station.Cells[3, 4] = "Résultats du dernier bilan 24h";
                    FeuilXL_Station.Cells[3, 6] = "Résultats du dernier prélèvement ponctuel";
                    FeuilXL_Station.Cells[4, 2] = "Concentration maximum";
                    FeuilXL_Station.Cells[4, 3] = "Rendement minimum";
                    FeuilXL_Station.Cells[4, 4] = comboDatesBilans.Items.Count > 0 ? comboDatesBilans.Items[0] : "Pas de données";
                    FeuilXL_Station.Cells[5, 4] = "Concentration (mg/l)";
                    FeuilXL_Station.Cells[5, 5] = "Rendement";
                    FeuilXL_Station.Cells[4, 6] = comboDatesPP.Items.Count > 0 ? comboDatesPP.Items[0] : "Pas de données";
                    FeuilXL_Station.Cells[5, 6] = "Concentration (mg/l)";
                    FeuilXL_Station.Cells[5, 7] = "Rendement";

                    // Fusion des headers
                    FeuilXL_Station.get_Range(FeuilXL_Station.Cells[3, 1], FeuilXL_Station.Cells[5, 1]).Cells.Merge();
                    FeuilXL_Station.get_Range(FeuilXL_Station.Cells[3, 2], FeuilXL_Station.Cells[3, 3]).Cells.Merge();
                    FeuilXL_Station.get_Range(FeuilXL_Station.Cells[3, 4], FeuilXL_Station.Cells[3, 5]).Cells.Merge();
                    FeuilXL_Station.get_Range(FeuilXL_Station.Cells[3, 6], FeuilXL_Station.Cells[3, 7]).Cells.Merge();
                    FeuilXL_Station.get_Range(FeuilXL_Station.Cells[4, 2], FeuilXL_Station.Cells[5, 2]).Cells.Merge();
                    FeuilXL_Station.get_Range(FeuilXL_Station.Cells[4, 3], FeuilXL_Station.Cells[5, 3]).Cells.Merge();
                    FeuilXL_Station.get_Range(FeuilXL_Station.Cells[4, 4], FeuilXL_Station.Cells[4, 5]).Cells.Merge();
                    FeuilXL_Station.get_Range(FeuilXL_Station.Cells[4, 6], FeuilXL_Station.Cells[4, 7]).Cells.Merge();

                    // Couleur des headers
                    Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL_Station.get_Range(FeuilXL_Station.Cells[3, 1], FeuilXL_Station.Cells[5, 7]);
                    RangeHeaders.Interior.Color = System.Drawing.ColorTranslator.ToOle(CouleurEnTetes);
                    RangeHeaders.Cells.Font.Bold = true;

                    // Parcourt des paramètres
                    foreach (int idParam in IdsParams) {

                        int Annee = comboDatesBilans.Items.Count > 0 ? Convert.ToDateTime(comboDatesBilans.Items[0]).Year : comboDatesPP.Items.Count > 0 ? Convert.ToDateTime(comboDatesPP.Items[0]).Year : DateTime.Today.Year;

                        // On ne s'occupe pas de certains paramètres (seulement MES, DCO, DBO5, NTK, Pt)
                        if (idParam == (int)eStatut.Parametre_MES || idParam == (int)eStatut.Parametre_DCO || idParam == (int)eStatut.Parametre_DBO5 || idParam == (int)eStatut.Parametre_NTK || idParam == (int)eStatut.Parametre_Ptotal) {

                            FeuilXL_Station.Cells[NumLigneRejetsStations, 1] = frmATE55.Statuts[idParam].LibelleStatut;

                            int ConcentrationMax = 0;
                            int RendementMin = 0;

                            // On récupère et on affiche les normes
                            req = "SELECT ConcentrationMax, RendementMin FROM Norme WHERE idStationNorme = " + idStation + " AND idStatut_TypeParametre = " + idParam + " AND AnneeValidite = " + Annee;
                            command = new SqlCommand(req, Session.oConn);
                            dataReader = command.ExecuteReader();

                            if (dataReader != null && dataReader.HasRows) {
                                dataReader.Read();

                                ConcentrationMax = Convert.ToInt32(dataReader["ConcentrationMax"]);
                                RendementMin = Convert.ToInt32(dataReader["RendementMin"]);

                                FeuilXL_Station.Cells[NumLigneRejetsStations, 2] = ConcentrationMax == 0 ? "/" : ConcentrationMax+"";
                                FeuilXL_Station.Cells[NumLigneRejetsStations, 3] = RendementMin == 0 ? "/" : RendementMin + "%";

                            }
                            else {
                                FeuilXL_Station.Cells[NumLigneRejetsStations, 2] = "/";
                                FeuilXL_Station.Cells[NumLigneRejetsStations, 3] = "/";
                            }
                            dataReader.Close();


                            // On récupère les mesures des bilans 24h
                            if(comboDatesBilans.Items.Count > 0){
                                req = "SELECT MesureConcentrationSortie,Rendement FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " AND idStatut_TypeParametre = " + idParam + " AND DateMesure = '" + comboDatesBilans.Items[0] + "'";
                                command = new SqlCommand(req, Session.oConn);
                                dataReader = command.ExecuteReader();

                                if (dataReader != null && dataReader.HasRows) {
                                    dataReader.Read();

                                    int Concentration = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                                    int Rendement = Convert.ToInt32(dataReader["Rendement"]);

                                    FeuilXL_Station.Cells[NumLigneRejetsStations, 4] = Concentration == 0 ? "/" : Concentration+"";
                                    FeuilXL_Station.Cells[NumLigneRejetsStations, 5] = Rendement == 0 ? "/" : Rendement+"%";

                                    // Couleurs en fonction de la norme
                                    if(ConcentrationMax > 0) CelluleNormeConforme(FeuilXL_Station.get_Range(FeuilXL_Station.Cells[NumLigneRejetsStations, 4], FeuilXL_Station.Cells[NumLigneRejetsStations, 4]), Concentration <= ConcentrationMax);
                                    if(RendementMin > 0) CelluleNormeConforme(FeuilXL_Station.get_Range(FeuilXL_Station.Cells[NumLigneRejetsStations, 5], FeuilXL_Station.Cells[NumLigneRejetsStations, 5]), Rendement >= RendementMin);

                                }
                                else {
                                    FeuilXL_Station.Cells[NumLigneRejetsStations, 4] = "/";
                                    FeuilXL_Station.Cells[NumLigneRejetsStations, 5] = "/";
                                }
                                dataReader.Close();

                            }

                            
                            // On récupère les PP
                            if (comboDatesPP.Items.Count > 0) {
                                req = "SELECT MesureConcentrationSortie,Rendement FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_PP + " AND idStatut_TypeParametre = " + idParam + " AND DateMesure = '" + comboDatesPP.Items[0] + "'";
                                command = new SqlCommand(req, Session.oConn);
                                dataReader = command.ExecuteReader();

                                if (dataReader != null && dataReader.HasRows) {
                                    dataReader.Read();

                                    int Concentration = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                                    int Rendement = Convert.ToInt32(dataReader["Rendement"]);

                                    FeuilXL_Station.Cells[NumLigneRejetsStations, 6] = Concentration == 0 ? "/" : Concentration + "";
                                    FeuilXL_Station.Cells[NumLigneRejetsStations, 7] = Rendement == 0 ? "/" : Rendement + "%";

                                    // Couleurs en fonction de la norme
                                    if (ConcentrationMax > 0) CelluleNormeConforme(FeuilXL_Station.get_Range(FeuilXL_Station.Cells[NumLigneRejetsStations, 6], FeuilXL_Station.Cells[NumLigneRejetsStations, 6]), Concentration <= ConcentrationMax);
                                    if (RendementMin > 0) CelluleNormeConforme(FeuilXL_Station.get_Range(FeuilXL_Station.Cells[NumLigneRejetsStations, 7], FeuilXL_Station.Cells[NumLigneRejetsStations, 7]), Rendement >= RendementMin);

                                }
                                else {
                                    FeuilXL_Station.Cells[NumLigneRejetsStations, 6] = "/";
                                    FeuilXL_Station.Cells[NumLigneRejetsStations, 7] = "/";
                                }
                                dataReader.Close();

                            }


                            // On incrémente le numéro de ligne
                            NumLigneRejetsStations++;

                        }

                    }


                    // Contours des cellules, tailles, alignements
                    Microsoft.Office.Interop.Excel.Range RangeRejetsStation = FeuilXL_Station.get_Range(FeuilXL_Station.Cells[3, 1], FeuilXL_Station.Cells[NumLigneRejetsStations-1, 7]);
                    RangeRejetsStation.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    RangeRejetsStation.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    ContoursCellules(RangeRejetsStation.Borders);
                    RangeRejetsStation.Columns.ColumnWidth = 20;
                    RangeRejetsStation.Rows.RowHeight = 30;

                    Microsoft.Office.Interop.Excel.Range RangeParams = FeuilXL_Station.get_Range(FeuilXL_Station.Cells[6, 1], FeuilXL_Station.Cells[NumLigneRejetsStations - 1, 1]);
                    RangeParams.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                }else
                    FeuilXL_Station.Cells[1, 1] = "PAS DE DONNEES";


                int NumLignePP = NumLigneRejetsStations + 2;


                // ------ STATION - PP -------
                if (comboDatesPP.Items.Count > 0) {

                    FeuilXL_Station.Cells[NumLignePP, 1] = "Prélèvement ponctuel";

                    NumLignePP += 2;

                    // Headers
                    FeuilXL_Station.Cells[NumLignePP, 1] = "Paramètre";
                    FeuilXL_Station.Cells[NumLignePP, 2] = "Unité";
                    FeuilXL_Station.Cells[NumLignePP, 3] = "Entrée";
                    FeuilXL_Station.Cells[NumLignePP, 4] = "Sortie filtre 1";
                    FeuilXL_Station.Cells[NumLignePP, 5] = "Sortie filtre 2";
                    FeuilXL_Station.Cells[NumLignePP, 6] = "Sortie";
                    FeuilXL_Station.Cells[NumLignePP, 7] = "Rendement";

                    // Couleurs headers
                    Microsoft.Office.Interop.Excel.Range RangeHeadersPP = FeuilXL_Station.get_Range(FeuilXL_Station.Cells[NumLignePP, 1], FeuilXL_Station.Cells[NumLignePP, 7]);
                    RangeHeadersPP.Interior.Color = System.Drawing.ColorTranslator.ToOle(CouleurEnTetes);
                    RangeHeadersPP.Cells.Font.Bold = true;

                    NumLignePP++;

                    // On parcourt les paramètres
                    foreach(int idParam in IdsParams){

                        // On récupère les mesures (seulement MES, DCO, DBO5, N-NO3, NH4, Ptotal)
                        if(idParam == (int)eStatut.Parametre_MES || idParam == (int)eStatut.Parametre_DCO || idParam == (int)eStatut.Parametre_DBO5 || idParam == (int)eStatut.Parametre_NNO3 || idParam == (int)eStatut.Parametre_NH4 || idParam == (int)eStatut.Parametre_Ptotal){

                            FeuilXL_Station.Cells[NumLignePP, 1] = frmATE55.Statuts[idParam].LibelleStatut;
                            FeuilXL_Station.Cells[NumLignePP, 2] = "mg/l";

                            // Requête
                            req = "SELECT * FROM Mesure WHERE idStatut_TypeMesure = " + (int)eStatut.Mesure_PP + " AND idStatut_TypeParametre = " + idParam + " AND idStationMesure = " + idStation + " AND DateMesure = '" + comboDatesPP.Items[0] + "'";
                            command = new SqlCommand(req, Session.oConn);
                            dataReader = command.ExecuteReader();

                            if (dataReader != null && dataReader.HasRows) {
                                dataReader.Read();

                                FeuilXL_Station.Cells[NumLignePP, 3] = Convert.ToInt32(dataReader["MesureConcentrationEntree"]);
                                FeuilXL_Station.Cells[NumLignePP, 4] = Convert.ToInt32(dataReader["MesureConcentrationPointIntermediaire1"]);
                                FeuilXL_Station.Cells[NumLignePP, 5] = Convert.ToInt32(dataReader["MesureConcentrationPointIntermediaire2"]);
                                FeuilXL_Station.Cells[NumLignePP, 6] = Convert.ToInt32(dataReader["MesureConcentrationSortie"]);
                                FeuilXL_Station.Cells[NumLignePP, 7] = Convert.ToInt32(dataReader["Rendement"])+"%";


                            }
                            else {
                                FeuilXL_Station.Cells[NumLignePP, 3] = "/";
                                FeuilXL_Station.Cells[NumLignePP, 4] = "/";
                                FeuilXL_Station.Cells[NumLignePP, 5] = "/";
                                FeuilXL_Station.Cells[NumLignePP, 6] = "/";
                                FeuilXL_Station.Cells[NumLignePP, 7] = "/";
                            }
                            dataReader.Close();

                            NumLignePP++;

                        }

                    }


                    // Contours, alignement
                    Microsoft.Office.Interop.Excel.Range RangePP = FeuilXL_Station.get_Range(FeuilXL_Station.Cells[NumLigneRejetsStations+4, 1], FeuilXL_Station.Cells[NumLignePP - 1, 7]);
                    RangePP.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    RangePP.Cells.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    ContoursCellules(RangePP.Borders);
                    RangePP.Columns.ColumnWidth = 20;
                    RangePP.Rows.RowHeight = 30;

                    Microsoft.Office.Interop.Excel.Range RangeParams = FeuilXL_Station.get_Range(FeuilXL_Station.Cells[NumLigneRejetsStations+5, 1], FeuilXL_Station.Cells[NumLignePP - 1, 1]);
                    RangeParams.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                }
                else
                    FeuilXL_Station.Cells[NumLignePP, 1] = "PAS DE PRELEVEMENT PONCTUEL";


                // ------ RESEAU - TAUX DE COLLECTE -------
                if (DatesBilans.Count > 0) {
                    int NumLigneTauxCollecte = 4;

                    FeuilXL_Reseau.Cells[1, 1] = "Taux de collecte";

                    // Headers
                    FeuilXL_Reseau.Cells[3, 1] = "Date du bilan";
                    FeuilXL_Reseau.Cells[3, 2] = "Taux de charge organique";
                    FeuilXL_Reseau.Cells[3, 3] = "Taux de charge hydraulique";
                    FeuilXL_Reseau.Cells[3, 4] = "Taux de collecte en DCO/j";
                    FeuilXL_Reseau.Cells[3, 5] = "Taux de collecte en DBO5/j";
                    FeuilXL_Reseau.Cells[3, 6] = "Usagers raccordés";

                    // Couleurs
                    Microsoft.Office.Interop.Excel.Range RangeHeadersReseau = FeuilXL_Reseau.get_Range(FeuilXL_Reseau.Cells[3, 1], FeuilXL_Reseau.Cells[3, 6]);
                    RangeHeadersReseau.Interior.Color = System.Drawing.ColorTranslator.ToOle(CouleurEnTetes);
                    RangeHeadersReseau.Font.Bold = true;

                    // Parcourt des dates de bilan
                    foreach (String DateBilan in DatesBilans) {

                        int CapaciteRecalculee = 0;
                        int DebitRecalcule = 0;
                        int DBO5Entree = 0;
                        int DCOEntree = 0;
                        int DebitEntree = 0;
                        int UsagersRaccordes = 0;


                        // On récupère des données dans la station
                        req = "SELECT CapaciteOrganiqueRecalculee,DebitReferenceRecalcule FROM StationAssainissement WHERE idStation = " + idStation;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();

                            CapaciteRecalculee = Convert.ToInt32(dataReader["CapaciteOrganiqueRecalculee"]);
                            DebitRecalcule = Convert.ToInt32(dataReader["DebitReferenceRecalcule"]);

                        }
                        dataReader.Close();


                        // On récupère les infos en DBO5
                        req = "SELECT MesureFluxEntree FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " AND idStatut_TypeParametre = " + (int)eStatut.Parametre_DBO5 + " AND DateMesure = '" + DateBilan + "'";
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();

                            DBO5Entree = Convert.ToInt32(dataReader["MesureFluxEntree"]);

                        }
                        dataReader.Close();

                        // On récupère les infos en DCO
                        req = "SELECT MesureFluxEntree FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " AND idStatut_TypeParametre = " + (int)eStatut.Parametre_DCO + " AND DateMesure = '" + DateBilan + "'";
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();

                            DCOEntree = Convert.ToInt32(dataReader["MesureFluxEntree"]);

                        }
                        dataReader.Close();

                        // On récupère les infos en DBO5
                        req = "SELECT MesureConcentrationEntree FROM Mesure WHERE idStationMesure = " + idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " AND idStatut_TypeParametre = " + (int)eStatut.Parametre_Volume + " AND DateMesure = '" + DateBilan + "'";
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            dataReader.Read();

                            DebitEntree = Convert.ToInt32(dataReader["MesureConcentrationEntree"]);

                        }
                        dataReader.Close();

                        // On récupère le nombre d'usagers raccordés en parcourant les réseaux
                        req = "SELECT NombreRaccordables,UsagersND,UsagersAssimilesDomestiques FROM Reseau WHERE idStationReseau = " + idStation;
                        command = new SqlCommand(req, Session.oConn);
                        dataReader = command.ExecuteReader();

                        if (dataReader != null && dataReader.HasRows) {
                            while (dataReader.Read()) {

                                UsagersRaccordes += Convert.ToInt32(dataReader["NombreRaccordables"]);
                                UsagersRaccordes += Convert.ToInt32(dataReader["UsagersND"]);
                                UsagersRaccordes += Convert.ToInt32(dataReader["UsagersAssimilesDomestiques"]);

                            }
                        }
                        dataReader.Close();


                        // Affichage des données
                        FeuilXL_Reseau.Cells[NumLigneTauxCollecte, 1] = DateBilan;
                        FeuilXL_Reseau.Cells[NumLigneTauxCollecte, 2] = DBO5Entree == 0 || CapaciteRecalculee == 0 ? "/" : Decimal.Round(((decimal)DBO5Entree / (decimal)CapaciteRecalculee * 100),0) + "%";
                        FeuilXL_Reseau.Cells[NumLigneTauxCollecte, 3] = DebitEntree == 0 || DebitRecalcule == 0 ? "/" : Decimal.Round(((decimal)DebitEntree / (decimal)DebitRecalcule * 100),0) + "%";
                        FeuilXL_Reseau.Cells[NumLigneTauxCollecte, 4] = DCOEntree == 0 || UsagersRaccordes == 0 ? "/" : Decimal.Round(((decimal)DCOEntree / (decimal)UsagersRaccordes * 1000),0) + "%";
                        FeuilXL_Reseau.Cells[NumLigneTauxCollecte, 5] = DBO5Entree == 0 || UsagersRaccordes == 0 ? "/" : Decimal.Round(((decimal)DBO5Entree / (decimal)UsagersRaccordes * 2000),0) + "%";
                        FeuilXL_Reseau.Cells[NumLigneTauxCollecte, 6] = UsagersRaccordes == 0 ? "/" : UsagersRaccordes + "";

                        NumLigneTauxCollecte++;
                    }

                    // Contours et alignement
                    Microsoft.Office.Interop.Excel.Range RangeReseau = FeuilXL_Reseau.get_Range(FeuilXL_Reseau.Cells[3, 1], FeuilXL_Reseau.Cells[NumLigneTauxCollecte-1, 6]);
                    ContoursCellules(RangeReseau.Borders);
                    RangeReseau.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    RangeReseau.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    RangeReseau.Columns.ColumnWidth = 20;
                    RangeReseau.Rows.RowHeight = 15;
                    
                }

                // ------ MESURES - BILAN 24h ------
                if (DatesBilans.Count > 0) {

                    FeuilXL_Mesures.Cells[1, 1] = "BILAN 24H";

                    // En-têtes (RowHeader)
                    FeuilXL_Mesures.Cells[3, 3] = "Date";
                    FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[3, 1], FeuilXL_Mesures.Cells[3, 3]).Cells.Merge();

                    // Parcourt des paramètres
                    foreach (int idParam in IdsParams) {

                        GraphiqueVolume = idParam == (int)eStatut.Parametre_Volume ? true : GraphiqueVolume;

                        NumLigneParam++;

                        // En-têtes pour chaque paramètre
                        FeuilXL_Mesures.Cells[NumLigneParam, 1] = frmATE55.Statuts[idParam].LibelleStatut;
                        FeuilXL_Mesures.Cells[NumLigneParam, 2] = "Entrée";
                        FeuilXL_Mesures.Cells[NumLigneParam++, 3] = idParam == (int)eStatut.Parametre_Volume ? "" : "Flux (kg/j)";
                        FeuilXL_Mesures.Cells[NumLigneParam++, 3] = idParam == (int)eStatut.Parametre_Volume ? "Débit (m³/j)" : "Concentration (mg/l)";
                        FeuilXL_Mesures.Cells[NumLigneParam, 2] = "Sortie";
                        FeuilXL_Mesures.Cells[NumLigneParam++, 3] = idParam == (int)eStatut.Parametre_Volume ? "" : "Flux (kg/j)";
                        FeuilXL_Mesures.Cells[NumLigneParam++, 3] = idParam == (int)eStatut.Parametre_Volume ? "Débit (m³/j)" : "Concentration (mg/l)";
                        FeuilXL_Mesures.Cells[NumLigneParam, 2] = idParam == (int)eStatut.Parametre_Volume ? "" : "Rendement";

                        // Fusion des cellules
                        FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneParam - 4, 1], FeuilXL_Mesures.Cells[NumLigneParam, 1]).Cells.Merge(); // Libellé sur 5 lignes
                        FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneParam - 4, 2], FeuilXL_Mesures.Cells[NumLigneParam - 3, 2]).Cells.Merge(); // Entrée sur 2 lignes
                        FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneParam - 2, 2], FeuilXL_Mesures.Cells[NumLigneParam - 1, 2]).Cells.Merge(); // Sortie sur 2 lignes
                        FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneParam, 2], FeuilXL_Mesures.Cells[NumLigneParam, 3]).Cells.Merge(); // Rendement sur 2 colonnes

                    }

                    // Parcourt des dates
                    foreach (string DateMesure in DatesBilans) {

                        FeuilXL_Mesures.Cells[3, NumColonne] = DateMesure;
                        NumColonne++;

                    }

                    // Couleur des cellules d'en-têtes
                    Microsoft.Office.Interop.Excel.Range RangeColonnes = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[3, 1], FeuilXL_Mesures.Cells[3, NumColonne - 1]);
                    RangeColonnes.Interior.Color = System.Drawing.ColorTranslator.ToOle(CouleurEnTetes);
                    Microsoft.Office.Interop.Excel.Range RangeLignes = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[3, 1], FeuilXL_Mesures.Cells[NumLigneParam, 3]);
                    RangeLignes.Interior.Color = System.Drawing.ColorTranslator.ToOle(CouleurEnTetes);
                    Microsoft.Office.Interop.Excel.Range RangeTout = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[3, 1], FeuilXL_Mesures.Cells[NumLigneParam, NumColonne - 1]);

                    ContoursCellules(RangeTout.Borders);                    

                    // Affichage des bilans
                    // On place nos curseurs
                    NumColonne = 3;

                    // On parcourt les dates
                    foreach (string Date in DatesBilans) {

                        NumLigneParam = 4;
                        NumColonne++;

                        // On parcourt chaque paramètre pour chaque date
                        foreach (int IdParam in IdsParams) {

                            // On récupère les mesures
                            req = "SELECT * FROM Mesure WHERE idStationMesure = "+idStation+" AND DateMesure = '" + Date + "' AND idStatut_TypeParametre = " + IdParam+ " AND idStatut_TypeMesure = "+(int)eStatut.Mesure_Bilan24h;
                            command = new SqlCommand(req, Session.oConn);
                            dataReader = command.ExecuteReader();

                            if (dataReader != null && dataReader.HasRows) {
                                dataReader.Read();

                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["MesureFluxEntree"].ToString();
                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["MesureConcentrationEntree"].ToString();
                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["MesureFluxSortie"].ToString();
                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["MesureConcentrationSortie"].ToString();
                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["Rendement"].ToString()+" %";

                            }
                            else
                                NumLigneParam += 5;

                            dataReader.Close();

                        }

                    }


                    // On convertit les cellules des données au format "nombre" pour pouvoir créer les graphiques
                    Microsoft.Office.Interop.Excel.Range ValeursRange = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[4, 4], FeuilXL_Mesures.Cells[NumLigneParam-1, 3+DatesBilans.Count]);
                    foreach(Microsoft.Office.Interop.Excel.Range Cell in ValeursRange){
                        try{
                            Cell.Value = Convert.ToDecimal(Cell.Value);
                        }
                        catch (Exception) { }
                    }

                    // GRAPHIQUE CHARGE HYDRAULIQUE
                    Microsoft.Office.Interop.Excel.Range DatesRange = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[3, 4], FeuilXL_Mesures.Cells[3, 3 + DatesBilans.Count]);
                    Microsoft.Office.Interop.Excel.ChartObjects Charts = (Microsoft.Office.Interop.Excel.ChartObjects)FeuilXL_Graphiques.ChartObjects(Type.Missing);

                    if (GraphiqueVolume) {
                        Microsoft.Office.Interop.Excel.ChartObject ChartObject = Charts.Add(10, 10, 500, 200); // Taille et position
                        Microsoft.Office.Interop.Excel.Chart ChartHydraulique = ChartObject.Chart;

                        Microsoft.Office.Interop.Excel.Range DebitRange = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[5, 4], FeuilXL_Mesures.Cells[5, 3 + DatesBilans.Count]);

                        ChartHydraulique.ChartWizard(Source: DebitRange,
                            Title: "Charge hydraulique",
                            CategoryTitle: "Dates",
                            ValueTitle: "Débit en m3/j",
                            Gallery: Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered,
                            HasLegend: false);

                        Microsoft.Office.Interop.Excel.Axis xAxis = (Microsoft.Office.Interop.Excel.Axis)ChartHydraulique.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary);
                        xAxis.CategoryNames = DatesRange;

                        xAxis.CategoryType = Microsoft.Office.Interop.Excel.XlCategoryType.xlCategoryScale;
                    }

                    // GRAPHIQUE CHARGE ORGANIQUE
                    Microsoft.Office.Interop.Excel.ChartObject ChartObject2 = Charts.Add(10, 250, 500, 200); // Taille et position
                    Microsoft.Office.Interop.Excel.Chart ChartOrganique = ChartObject2.Chart;

                    ChartOrganique.HasTitle = true;
                    ChartOrganique.ChartTitle.Text = "Charge organique";
                    ChartOrganique.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlColumnClustered;
                    ChartOrganique.HasDataTable = true;

                    Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = (Microsoft.Office.Interop.Excel.SeriesCollection)ChartOrganique.SeriesCollection();
                    Microsoft.Office.Interop.Excel.Series seriesDBO5 = seriesCollection.NewSeries();
                    Microsoft.Office.Interop.Excel.Series seriesDCO = seriesCollection.NewSeries();

                    seriesDBO5.Name = "DBO5";
                    seriesDCO.Name = "DCO";

                    seriesDBO5.Values = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[19, 4], FeuilXL_Mesures.Cells[19, 3 + DatesBilans.Count]);
                    seriesDCO.Values = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[14, 4], FeuilXL_Mesures.Cells[14, 3 + DatesBilans.Count]);

                    Microsoft.Office.Interop.Excel.Axis xAxis2 = (Microsoft.Office.Interop.Excel.Axis)ChartOrganique.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory, Microsoft.Office.Interop.Excel.XlAxisGroup.xlPrimary);
                    xAxis2.CategoryNames = DatesRange;

                    xAxis2.CategoryType = Microsoft.Office.Interop.Excel.XlCategoryType.xlCategoryScale;


                }else
                    FeuilXL_Mesures.Cells[1, 1] = "PAS DE BILAN 24H";


                NumLigneParam += 3;
                NumColonne = 4;


                // MESURES ------- PRELEVEMENTS PONCTUELS -------
                if (DatesPP.Count > 0) {

                    FeuilXL_Mesures.Cells[NumLigneParam, 1] = "Prélèvements ponctuels SATE";
                    NumLigneParam += 2;

                    int NumLigneDebut = NumLigneParam;

                    // En-têtes
                    FeuilXL_Mesures.Cells[NumLigneParam, 3] = "Date";
                    FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneParam, 1], FeuilXL_Mesures.Cells[NumLigneParam, 3]).Cells.Merge();

                    // Parcourt des dates
                    foreach (string DateMesure in DatesPP) {

                        FeuilXL_Mesures.Cells[NumLigneParam, NumColonne] = DateMesure;
                        NumColonne++;

                    }

                    // Parcourt des paramètres
                    foreach (int idParam in IdsParams) {

                        NumLigneParam++;

                        // En-têtes pour chaque paramètre
                        FeuilXL_Mesures.Cells[NumLigneParam, 1] = frmATE55.Statuts[idParam].LibelleStatut;
                        FeuilXL_Mesures.Cells[NumLigneParam, 2] = "Entrée";
                        FeuilXL_Mesures.Cells[NumLigneParam++, 3] = "Concentration (mg/l)";
                        FeuilXL_Mesures.Cells[NumLigneParam, 2] = "Sortie filtre 1";
                        FeuilXL_Mesures.Cells[NumLigneParam++, 3] = "Concentration (mg/l)";
                        FeuilXL_Mesures.Cells[NumLigneParam, 2] = "Sortie filtre 2";
                        FeuilXL_Mesures.Cells[NumLigneParam++, 3] = "Concentration (mg/l)";
                        FeuilXL_Mesures.Cells[NumLigneParam, 2] = "Sortie";
                        FeuilXL_Mesures.Cells[NumLigneParam++, 3] = "Concentration (mg/l)";
                        FeuilXL_Mesures.Cells[NumLigneParam, 2] = "Rendement";

                        // Fusion des cellules
                        FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneParam - 4, 1], FeuilXL_Mesures.Cells[NumLigneParam, 1]).Cells.Merge(); // Libellé sur 4 lignes
                        FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneParam, 2], FeuilXL_Mesures.Cells[NumLigneParam, 3]).Cells.Merge(); // Rendement sur 2 colonnes

                    }


                    // Couleurs et contours
                    Microsoft.Office.Interop.Excel.Range RangeColonnes = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneDebut, 1], FeuilXL_Mesures.Cells[NumLigneDebut, NumColonne - 1]);
                    RangeColonnes.Interior.Color = System.Drawing.ColorTranslator.ToOle(CouleurEnTetes);
                    Microsoft.Office.Interop.Excel.Range RangeLignes = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneDebut, 1], FeuilXL_Mesures.Cells[NumLigneParam, 3]);
                    RangeLignes.Interior.Color = System.Drawing.ColorTranslator.ToOle(CouleurEnTetes);
                    Microsoft.Office.Interop.Excel.Range RangeTout = FeuilXL_Mesures.get_Range(FeuilXL_Mesures.Cells[NumLigneDebut, 1], FeuilXL_Mesures.Cells[NumLigneParam, NumColonne - 1]);

                    ContoursCellules(RangeTout.Borders);   


                    // Affichage des mesures
                    NumColonne = 3;

                    // Parcourt des dates
                    foreach (string DateMesure in DatesPP) {

                        NumColonne++;
                        NumLigneParam = NumLigneDebut+1;

                        // Parcourt des paramètres
                        foreach (int IdParam in IdsParams) {

                            // On récupère la mesure correspondante
                            req = "SELECT * FROM Mesure WHERE idStationMesure = " + idStation + " AND DateMesure = '" + DateMesure + "' AND idStatut_TypeMesure = " + (int)eStatut.Mesure_PP + " AND idStatut_TypeParametre = " + IdParam;
                            command = new SqlCommand(req, Session.oConn);
                            dataReader = command.ExecuteReader();

                            if (dataReader != null && dataReader.HasRows) {
                                dataReader.Read();

                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["MesureConcentrationEntree"].ToString();
                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["MesureConcentrationPointIntermediaire1"].ToString();
                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["MesureConcentrationPointIntermediaire2"].ToString();
                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["MesureConcentrationSortie"].ToString();
                                FeuilXL_Mesures.Cells[NumLigneParam++, NumColonne] = dataReader["Rendement"].ToString()+"%";

                            }
                            else
                                NumLigneParam += 5;

                            dataReader.Close();

                        }

                    }

                }
                else
                    FeuilXL_Mesures.Cells[NumLigneParam, 1] = "PAS DE PRELEVEMENTS PONCTUELS";


                // On affiche le tableur une fois que toutes les opérations sont terminées
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

        public static void GenererExtractionStations(CSession Session) {

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

            FeuilXL.Name = "Liste des stations";

            // Police
            ApplicationXL.StandardFont = "Century Gothic";

            // Headers
            FeuilXL.Cells[1, 1] = "ID";
            FeuilXL.Cells[1, 2] = "Code INSEE";
            FeuilXL.Cells[1, 3] = "Code SANDRE";
            FeuilXL.Cells[1, 4] = "Nom";
            FeuilXL.Cells[1, 5] = "Localisation";
            FeuilXL.Cells[1, 6] = "AE";
            FeuilXL.Cells[1, 7] = "Capacité eqH";
            FeuilXL.Cells[1, 8] = "Capacité DBO5/j";
            FeuilXL.Cells[1, 9] = "Année de construction";
            FeuilXL.Cells[1, 10] = "Eligible";
            FeuilXL.Cells[1, 11] = "Conforme";
            FeuilXL.Cells[1, 12] = "Nombre de visites";
            FeuilXL.Cells[1, 13] = "Nombre de bilans restants";
            FeuilXL.Cells[1, 14] = "Nombre de bilans année prochaine";
            FeuilXL.Cells[1, 15] = "Etat de l'ouvrage";
            FeuilXL.Cells[1, 16] = "Etat de l'entretien";
            FeuilXL.Cells[1, 17] = "Suivi SATE";
            FeuilXL.Cells[1, 18] = "Observations";
            

            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[1, 18]);
            RangeHeaders.Interior.Color = CouleurEnTetes;
            RangeHeaders.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.White);
            RangeHeaders.Font.Bold = true;


            int NumLigne = 2;

            List<CStationAssainissement> Stations = new List<CStationAssainissement>();

            // Récupération et stockage des stations
            string req = "SELECT * FROM StationAssainissement";
            SqlCommand command = new SqlCommand(req, Session.oConn);
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    CStationAssainissement Station = new CStationAssainissement();
                    Station.idStation = Convert.ToInt32(dataReader["idStation"]);
                    Station.NomStation = dataReader["NomStation"].ToString();
                    Station.CodeSANDRE = dataReader["CodeSANDRE"].ToString();
                    Station.idStatut_TypeFiliereBoue = Convert.ToInt32(dataReader["idStatut_TypeFiliereBoue"]);
                    Station.idStatut_ModeGestion = Convert.ToInt32(dataReader["idStatut_ModeGestion"]);
                    Station.idStatut_TypeStation = Convert.ToInt32(dataReader["idStatut_TypeStation"]);
                    Station.idStatut_SousTypeStation = Convert.ToInt32(dataReader["idStatut_SousTypeStation"]);
                    Station.idStatut_EtatEntretien = Convert.ToInt32(dataReader["idStatut_EtatEntretien"]);
                    Station.idStatut_EtatOuvrages = Convert.ToInt32(dataReader["idStatut_EtatOuvrages"]);
                    Station.Capacite = Convert.ToInt32(dataReader["Capacite"]);
                    Station.CapaciteDBO5 = Convert.ToInt32(dataReader["CapaciteDBO5"]);
                    Station.DebitReference = Convert.ToDecimal(dataReader["DebitReference"]);
                    Station.DebitReferenceRecalcule = Convert.ToDecimal(dataReader["DebitReferenceRecalcule"]);
                    Station.AnneeConstruction = Convert.ToInt32(dataReader["AnneeConstruction"]);
                    Station.ComplementModeGestion = dataReader["ComplementModeGestion"].ToString();
                    Station.CodeCollectiviteLocalisation = dataReader["CodeCollectiviteLocalisation"].ToString();
                    Station.ExutoireStation = dataReader["ExutoireStation"].ToString();
                    Station.PositionX = Convert.ToDecimal(dataReader["PositionX"]);
                    Station.PositionY = Convert.ToDecimal(dataReader["PositionY"]);
                    Station.MasseDeau = dataReader["MasseDeau"].ToString();
                    Station.SuiviSATE = Convert.ToInt32(dataReader["SuiviSATE"]);
                    Station.ZRV = Convert.ToInt32(dataReader["ZRV"]);
                    Station.Dysfonctionnements = dataReader["Dysfonctionnements"].ToString();
                    Station.CapaciteOrganiqueRecalculee = Convert.ToDecimal(dataReader["CapaciteOrganiqueRecalculee"]);
                    Station.NombreVisites = Convert.ToInt32(dataReader["NombreVisites"]);
                    Station.Observations = dataReader["Observations"].ToString();

                    Stations.Add(Station);

                }
            }
            dataReader.Close();

            // Parcourt de la liste des stations
            foreach (CStationAssainissement Station in Stations) {

                FeuilXL.Cells[NumLigne, 1] = Station.idStation;
                FeuilXL.Cells[NumLigne, 2] = Station.CodeCollectiviteLocalisation;
                FeuilXL.Cells[NumLigne, 3] = Station.CodeSANDRE;
                FeuilXL.Cells[NumLigne, 4] = Station.NomStation;
                FeuilXL.Cells[NumLigne, 7] = Station.Capacite;
                FeuilXL.Cells[NumLigne, 8] = Station.CapaciteDBO5;
                FeuilXL.Cells[NumLigne, 9] = Station.AnneeConstruction;
                FeuilXL.Cells[NumLigne, 12] = Station.NombreVisites;
                FeuilXL.Cells[NumLigne, 15] = frmATE55.Statuts[Station.idStatut_EtatOuvrages].LibelleStatut;
                FeuilXL.Cells[NumLigne, 16] = frmATE55.Statuts[Station.idStatut_EtatEntretien].LibelleStatut;
                FeuilXL.Cells[NumLigne, 17] = Station.SuiviSATE == 1 ? "x" : "";
                FeuilXL.Cells[NumLigne, 18] = Station.Observations;

                try {
                    FeuilXL.Cells[NumLigne, 5] = frmATE55.Collectivites[Station.CodeCollectiviteLocalisation].NomCollectivite;
                    FeuilXL.Cells[NumLigne, 6] = frmATE55.Collectivites[Station.CodeCollectiviteLocalisation].AgenceEau;
                }
                catch (KeyNotFoundException) { }


                // Nombre de visites
                // DBO5 :
                // < 12 -> 0
                // >= 12 && < 30 -> 1 tous les 2 ans
                // >= 30 && < 60 -> 1 tous les ans
                // >= 60 && < 120 -> 2 par an
                // >= 120 -> 12 par an
                int NombreVisitesRestantes = 0;

                if (Station.CapaciteDBO5 >= 12 && Station.CapaciteDBO5 < 30)
                    NombreVisitesRestantes = -1;
                else if (Station.CapaciteDBO5 >= 30 && Station.CapaciteDBO5 < 60)
                    NombreVisitesRestantes = 1;
                else if(Station.CapaciteDBO5 >= 60 && Station.CapaciteDBO5 < 120)
                    NombreVisitesRestantes = 2;
                else if(Station.CapaciteDBO5 >= 120)
                    NombreVisitesRestantes = 12;

                req = "SELECT Count(DISTINCT DateMesure) FROM Mesure WHERE idStationMesure = " + Station.idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " AND YEAR(DateMesure) =" + DateTime.Today.Year;
                command = new SqlCommand(req, Session.oConn);

                FeuilXL.Cells[NumLigne, 14] = NombreVisitesRestantes;
                FeuilXL.Cells[NumLigne, 13] = NombreVisitesRestantes - Convert.ToInt32(command.ExecuteScalar());

                if (NombreVisitesRestantes == -1) {
                    if (Convert.ToInt32(command.ExecuteScalar()) == 1) {
                        FeuilXL.Cells[NumLigne, 13] = 0;
                        FeuilXL.Cells[NumLigne, 14] = 1;
                    }
                    else {
                        FeuilXL.Cells[NumLigne, 13] = 1;
                        FeuilXL.Cells[NumLigne, 14] = 0;
                    }
                }

                // Eligibilité
                req = "SELECT TOP 1 PotentielFinancier,PopulationDGF,CommunesUrbaines,AnneeEligibilite FROM Eligibilite WHERE CodeCollectivite = " + Station.CodeCollectiviteLocalisation + " ORDER BY AnneeEligibilite DESC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    CEligibilite Eligibilite = new CEligibilite(Session);

                    Eligibilite.PotentielFinancier = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                    Eligibilite.PopulationDGF = Convert.ToInt32(dataReader["PopulationDGF"]);
                    Eligibilite.CommunesUrbaines = Convert.ToInt32(dataReader["CommunesUrbaines"]);
                    Eligibilite.AnneeEligibilite = Convert.ToInt32(dataReader["AnneeEligibilite"]);
                    Eligibilite.CodeCollectivite = Station.CodeCollectiviteLocalisation;

                    dataReader.Close();

                    // Si éligible
                    if (Eligibilite.Eligible())
                        FeuilXL.Cells[NumLigne, 10] = "Eligible";
                    // Sinon si éligible année précédente
                    else if (Eligibilite.Eligible(DateTime.Today.Year - 1))
                        FeuilXL.Cells[NumLigne, 10] = "En transition";
                    // Sinon
                    else
                        FeuilXL.Cells[NumLigne, 10] = "Non éligible";

                }
                else
                    dataReader.Close();


                // Conformité
                // On récupère les conformités de chaque mesure du dernier bilan
                // Si une d'entre elle n'est pas conforme alors la station n'est pas conforme
                // (on ignore le sans objet)
                req = "SELECT DISTINCT DateMesure FROM Mesure WHERE idStationMesure = " + Station.idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " ORDER BY DateMesure DESC";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    dataReader.Read();

                    DateTime DateMesure = Convert.ToDateTime(dataReader["DateMesure"]);

                    dataReader.Close();

                    req = "SELECT idStatut_EtatConformite FROM Mesure WHERE idStationMesure = " + Station.idStation + " AND idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " AND DateMesure = '" + DateMesure + "'";
                    command = new SqlCommand(req, Session.oConn);
                    dataReader = command.ExecuteReader();

                    bool Conforme = true;

                    if (dataReader != null && dataReader.HasRows) {
                        while (dataReader.Read()) {

                            Conforme = Conforme && Convert.ToInt32(dataReader["idStatut_EtatConformite"]) != (int)eStatut.MesureNonConforme;

                        }
                    }
                    dataReader.Close();

                    FeuilXL.Cells[NumLigne, 11] = Conforme ? "Conforme" : "Non conforme";

                }
                else
                    dataReader.Close();


                NumLigne++;

            }

            // Contours et tailles
            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 18]);
            ContoursCellules(Range.Cells.Borders);
            Range.Columns.ColumnWidth = 20;
            Range.Rows.RowHeight = 20;
            Range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            // Largeurs colonnes
            Microsoft.Office.Interop.Excel.Range RangeIds = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 1]);
            RangeIds.Columns.ColumnWidth = 5;

            Microsoft.Office.Interop.Excel.Range RangeCodes = FeuilXL.get_Range(FeuilXL.Cells[1, 2], FeuilXL.Cells[NumLigne - 1, 2]);
            RangeCodes.Columns.ColumnWidth = 10;

            Microsoft.Office.Interop.Excel.Range RangeSANDRE = FeuilXL.get_Range(FeuilXL.Cells[1, 3], FeuilXL.Cells[NumLigne - 1, 3]);
            RangeSANDRE.Columns.ColumnWidth = 15;
            RangeSANDRE.Font.Bold = true;

            Microsoft.Office.Interop.Excel.Range RangeLocalisation = FeuilXL.get_Range(FeuilXL.Cells[1, 4], FeuilXL.Cells[NumLigne - 1, 5]);
            RangeLocalisation.Columns.ColumnWidth = 30;

            Microsoft.Office.Interop.Excel.Range RangeNombres = FeuilXL.get_Range(FeuilXL.Cells[1, 7], FeuilXL.Cells[NumLigne - 1, 9]);
            RangeNombres.Columns.ColumnWidth = 5;

            Microsoft.Office.Interop.Excel.Range RangeEligibleConforme = FeuilXL.get_Range(FeuilXL.Cells[1, 10], FeuilXL.Cells[NumLigne - 1, 11]);
            RangeEligibleConforme.Columns.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range RangeNombreVisites = FeuilXL.get_Range(FeuilXL.Cells[1, 12], FeuilXL.Cells[NumLigne - 1, 14]);
            RangeNombreVisites.Columns.ColumnWidth = 5;

            Microsoft.Office.Interop.Excel.Range RangeEtats = FeuilXL.get_Range(FeuilXL.Cells[1, 15], FeuilXL.Cells[NumLigne - 1, 16]);
            RangeEtats.Columns.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range RangeSuivi = FeuilXL.get_Range(FeuilXL.Cells[1, 17], FeuilXL.Cells[NumLigne - 1, 17]);
            RangeSuivi.Columns.ColumnWidth = 5;

            Microsoft.Office.Interop.Excel.Range RangeObservations = FeuilXL.get_Range(FeuilXL.Cells[1, 18], FeuilXL.Cells[NumLigne - 1, 18]);
            RangeObservations.Columns.ColumnWidth = 30;



            ApplicationXL.Visible = true;

        }

        public static void GenererExtractionReseaux(CSession Session) {

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

            FeuilXL.Name = "Liste des réseaux";

            // Police
            ApplicationXL.StandardFont = "Century Gothic";

            // Headers
            FeuilXL.Cells[1, 1] = "Code SANDRE";
            FeuilXL.Cells[1, 2] = "Nom station";
            FeuilXL.Cells[1, 3] = "Code INSEE";
            FeuilXL.Cells[1, 4] = "Localisation";
            FeuilXL.Cells[1, 5] = "Longueur";
            FeuilXL.Cells[1, 6] = "Type";

            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[1, 6]);
            RangeHeaders.Interior.Color = CouleurEnTetes;
            RangeHeaders.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.White);
            RangeHeaders.Font.Bold = true;


            int NumLigne = 2;

            // Récupération des réseaux
            string req = "SELECT CodeSANDRE,NomStation,CodeCollectiviteCT,NomCollectivite,Longueur,idStatut_TypeReseau FROM Reseau LEFT JOIN StationAssainissement ON Reseau.idStationReseau = StationAssainissement.idStation LEFT JOIN Collectivite_V ON Reseau.CodeCollectiviteCT = Collectivite_V.CodeCollectivite";
            SqlCommand command = new SqlCommand(req, Session.oConn);
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    FeuilXL.Cells[NumLigne, 1] = dataReader["CodeSANDRE"].ToString();
                    FeuilXL.Cells[NumLigne, 2] = dataReader["NomStation"].ToString();
                    FeuilXL.Cells[NumLigne, 3] = dataReader["CodeCollectiviteCT"].ToString();
                    FeuilXL.Cells[NumLigne, 4] = dataReader["NomCollectivite"].ToString();
                    FeuilXL.Cells[NumLigne, 5] = Decimal.Round(Convert.ToDecimal(dataReader["Longueur"]), 2) + " km";
                    FeuilXL.Cells[NumLigne, 6] = Convert.ToInt32(dataReader["idStatut_TypeReseau"]) != 0 ? frmATE55.Statuts[Convert.ToInt32(dataReader["idStatut_TypeReseau"])].LibelleStatut : "";

                    NumLigne++;

                }
            }
            dataReader.Close();


            FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 1]).Font.Bold = true;

            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 6]);
            Range.Rows.AutoFit();
            Range.Columns.AutoFit();
            Range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            ContoursCellules(Range.Borders);

            ApplicationXL.Visible = true;

        }

        private static void ContoursCellules(Microsoft.Office.Interop.Excel.Borders borders) {
            foreach (Microsoft.Office.Interop.Excel.Border Border in borders)
                borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }

        private void CelluleNormeConforme(Microsoft.Office.Interop.Excel.Range Cell, bool Conforme) {

            Color FondNonConforme = Color.FromArgb(255, 199, 206);
            Color TexteNonConforme = Color.FromArgb(156, 0, 6);
            Color FondConforme = Color.FromArgb(198, 239, 206);
            Color TexteConforme = Color.FromArgb(0, 97, 0);

            if (Conforme) {
                Cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(FondConforme);
                Cell.Font.Color = System.Drawing.ColorTranslator.ToOle(TexteConforme);
            }
            else {
                Cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(FondNonConforme);
                Cell.Font.Color = System.Drawing.ColorTranslator.ToOle(TexteNonConforme);
            }

        }

        private void ImporterStations() {

            string path = Application.StartupPath + @"\Imports\Stations.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();


                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    CStationAssainissement Station = new CStationAssainissement(Session);

                    Station.idStatut_TypeStation = Convert.ToInt32(values[13]);
                    Station.idStatut_SousTypeStation = Convert.ToInt32(values[14]);
                    Station.Capacite = Convert.ToInt32(values[1]);
                    Station.CapaciteDBO5 = Convert.ToInt32(values[2]);
                    Station.DebitReference = Convert.ToInt32(values[3]);
                    Station.AnneeConstruction = Convert.ToInt32(values[6]);
                    Station.idStatut_ModeGestion = Convert.ToInt32(values[4]);
                    Station.ComplementModeGestion = values[5];
                    Station.CodeSANDRE = values[8].Equals("") ? "" : "0"+values[8];
                    Station.CodeCollectiviteLocalisation = values[0];
                    Station.NomStation = values[7];
                    Station.ExutoireStation = values[12];
                    Station.idStatut_TypeFiliereBoue = Convert.ToInt32(values[11]);
                    Station.PositionX = Convert.ToInt32(values[9]);
                    Station.PositionY = Convert.ToInt32(values[10]);

                    // Valeurs par défaut
                    Station.Observations = "";
                    Station.MasseDeau = "";
                    Station.Dysfonctionnements = "";
                    Station.ZRV = 0;
                    Station.DebitReferenceRecalcule = 0;
                    Station.CapaciteOrganiqueRecalculee = 0;

                    if (!Station.Creer()) {
                        this.Text = values[0];
                        return;
                    }

                }
            }

        }

        private void ImporterReseaux() {

            string path = Application.StartupPath + @"\Imports\Reseaux.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On stocke les ids des stations dans un dictionnaire contenant les noms
                Dictionary<string, int> IdsStations = new Dictionary<string, int>();

                string req = "SELECT idStation,NomStation FROM StationAssainissement";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        try {
                            IdsStations.Add(dataReader["NomStation"].ToString(), Convert.ToInt32(dataReader["idStation"]));
                        }
                        catch (ArgumentException e) { e.ToString(); }

                    }
                }
                dataReader.Close();

                CReseau Reseau;

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();

                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    Reseau = new CReseau(Session);

                    try {
                        Reseau.idStationReseau = IdsStations[values[1]];
                        Reseau.idStatut_TypeReseau = 0;
                        Reseau.Longueur = 0;
                        Reseau.MilieuSensible = 0;
                        Reseau.CodeCollectiviteCT = values[4];
                        Reseau.NombreBranchements = 0;
                        Reseau.NombreDeversoirsOrage = 0;
                        Reseau.NombreStationsPompage = 0;
                        Reseau.NombreEqH = Convert.ToInt32(values[3]);

                        //Valeurs par défaut
                        Reseau.UsagersND = 0;
                        Reseau.UsagersAssimilesDomestiques = 0;
                        Reseau.NombreRaccordables = 0;
                        Reseau.NombreRaccordes = 0;

                        if (!Reseau.Creer()) {
                            this.Text = values[0];
                            return;
                        }

                    }
                    catch (KeyNotFoundException e) { e.ToString(); }

                }


                /**
                // On stocke les réseaux dans un dictionnaire pour récupérer ses infos si plusieurs lignes
                // du fichier y sont rattachées et ensuite le mettre à jour
                // Clé = Sandre+NomCT
                Dictionary<string, CReseau> SandreReseaux = new Dictionary<string, CReseau>();


                int i = 0;
                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');


                    // On teste si le code sandre est déjà rattaché à un réseau
                    // S'il y est on récupère le réseau associé 
                    // Si la collectivité est la même on met à jour ses infos
                    // Sinon on en crée un nouveau
                    Boolean CreerNouveau = !SandreReseaux.TryGetValue(values[2] + values[4], out Reseau);

                    if (!CreerNouveau)
                        CreerNouveau = !Reseau.NomCT.Equals(values[4]);


                    if (CreerNouveau) {

                        // Création
                        Reseau = new CReseau(Session);

                        try {
                            Reseau.idStationReseau = IdsStations[values[2]];

                            Reseau.NomMO = values[0];
                            Reseau.NomCT = values[4];

                            Reseau.idStatut_TypeReseau = Convert.ToInt32(values[5]);

                            Reseau.NombreBranchements = Convert.ToInt32(values[6]);
                            Reseau.NombreEqH = Convert.ToInt32(values[7]);
                            Reseau.Longueur = Convert.ToInt32(values[9]);

                            // Valeurs par défaut
                            Reseau.MilieuSensible = 0;

                            SandreReseaux.Add(values[2] + values[4], Reseau);


                            if (!Reseau.Creer()) {
                                this.Text = "Création : " + values[2];
                                return;
                            }
                            else
                                i++;

                        }
                        catch (KeyNotFoundException e) { e.ToString(); }


                    }
                    else {

                        // Màj

                        if (Reseau.idStatut_TypeReseau == (int)eStatut.NonPrecise || Reseau.idStatut_TypeReseau == (int)eStatut.Reseau_Unitaire)
                            Reseau.idStatut_TypeReseau = Convert.ToInt32(values[5]);

                        Reseau.NombreBranchements += Convert.ToInt32(values[6]);
                        Reseau.NombreEqH += Convert.ToInt32(values[7]);
                        Reseau.Longueur += Convert.ToInt32(values[9]);

                        if (!Reseau.Enregistrer()) {
                            this.Text = "Mise à jour : " + values[2];
                            return;
                        }
                        else
                            i++;

                    }

                }
                this.Text = i + "";*/
            }

        }

        private void ImporterMesures() {

            string path = Application.StartupPath + @"\Imports\Mesures.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On stocke les ids des stations dans un dictionnaire contenant les codes SANDRE
                Dictionary<string, int> IdsStations = new Dictionary<string, int>();

                string req = "SELECT idStation,CodeSANDRE FROM StationAssainissement";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        try {
                            IdsStations.Add(dataReader["CodeSANDRE"].ToString(), Convert.ToInt32(dataReader["idStation"]));
                        }
                        catch (ArgumentException) {}

                    }
                }
                dataReader.Close();

                CMesure Mesure;

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();

                DateTime DateMesure = DateTime.MinValue;
                int idStationMesure = -1;

                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');
                    if (!values[0].Equals("")) {

                        Mesure = new CMesure(Session);

                        try {

                            Mesure.idStationMesure = IdsStations["0" + values[0]];
                            Mesure.DateMesure = Convert.ToDateTime(values[1]);
                            Mesure.idStatut_TypeParametre = Convert.ToInt32(values[2]);
                            Mesure.Rendement = Convert.ToDecimal(values[4]);
                            Mesure.MesureFluxEntree = Convert.ToDecimal(values[6]);
                            Mesure.MesureFluxSortie = Convert.ToDecimal(values[7]);
                            Mesure.MesureConcentrationSortie = Convert.ToDecimal(values[3]);
                            Mesure.idStatut_EtatConformite = Convert.ToInt32(values[5]);

                            // Calcul concentration entrée
                            // Concentration Entrée = FluxEntree/DebitEntree*1000
                            Mesure.MesureConcentrationEntree = Mesure.MesureFluxEntree / Convert.ToDecimal(values[8]) * 1000;

                            // Valeurs par défaut
                            Mesure.MesureConcentrationPointIntermediaire1 = 0;
                            Mesure.MesureConcentrationPointIntermediaire2 = 0;
                            Mesure.idStatut_TypeMesure = (int)eStatut.Mesure_Bilan24h;

                            if (!Mesure.Creer()) {
                                this.Text = values[0] + " - " + values[1];
                                return;
                            }


                            // Si la station ne possède pas de mesure pour le volume à cette date on la crée
                            req = "SELECT * FROM Mesure WHERE idStationMesure = " + Mesure.idStationMesure + " AND DateMesure = '" + Mesure.DateMesure + "' AND idStatut_TypeParametre = " + (int)eStatut.Parametre_Volume;
                            command = new SqlCommand(req, Session.oConn);
                            dataReader = command.ExecuteReader();

                            if (dataReader == null || !dataReader.HasRows) {

                                dataReader.Close();

                                Mesure = new CMesure(Session);
                                Mesure.idStationMesure = IdsStations["0" + values[0]];
                                Mesure.DateMesure = Convert.ToDateTime(values[1]);
                                Mesure.MesureConcentrationEntree = Convert.ToDecimal(values[8]);
                                Mesure.MesureConcentrationSortie = Convert.ToDecimal(values[9]);
                                Mesure.idStatut_TypeParametre = (int)eStatut.Parametre_Volume;
                                Mesure.idStatut_TypeMesure = (int)eStatut.Mesure_Bilan24h;

                                if (!Mesure.Creer()) {
                                    this.Text = "Volume : " + values[0] + " - " + values[1];
                                    return;
                                }

                                idStationMesure = IdsStations["0" + values[0]];
                                DateMesure = Convert.ToDateTime(values[1]);

                            }
                            else
                                dataReader.Close();

                        }
                        catch (KeyNotFoundException) { this.Text += values[0] + "/"; }


                    }

                }
            }

        }

        private void ImporterNormes() {

            string path = Application.StartupPath + @"\Imports\Normes.csv";

            using (var fs = File.OpenRead(path))
            using (var reader = new StreamReader(fs)) {

                // On stocke les ids des stations dans un dictionnaire contenant les codes SANDRE
                Dictionary<string, int> IdsStations = new Dictionary<string, int>();

                string req = "SELECT idStation,CodeSANDRE FROM StationAssainissement";
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        try {
                            IdsStations.Add(dataReader["CodeSANDRE"].ToString(), Convert.ToInt32(dataReader["idStation"]));
                        }
                        catch (ArgumentException) {}

                    }
                }
                dataReader.Close();


                CNorme Norme = new CNorme(Session);

                // On saute la première ligne avec les en-têtes
                reader.ReadLine();

                while (!reader.EndOfStream) {

                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    try {

                        Norme.idStationNorme = IdsStations["0"+values[0]];
                        Norme.idStatut_TypeParametre = Convert.ToInt32(values[1]);
                        Norme.AnneValidite = Convert.ToInt32(values[2]);
                        Norme.RendementMin = Convert.ToDecimal(values[3]);
                        Norme.ConcentrationMax = Convert.ToDecimal(values[4]);

                        if (!Norme.Creer()) {
                            this.Text = "Norme : " + values[0];
                            return;
                        }

                    }
                    catch (KeyNotFoundException) {}

                }

            }

        }

        private void Modification(object sender, EventArgs e) {
            enregistrerStationBouton.Enabled = annulerStationBouton.Enabled = true;
        }

        private void ModificationReseau(object sender, EventArgs e) {
            enregistrerReseauBouton.Enabled = annulerReseauBouton.Enabled = true;
        }

        private void ModificationMesuresBilan(object sender, EventArgs e) {
            enregistrerMesuresBilan.Enabled = annulerMesuresBilan.Enabled = true;
        }

        private void ModificationMesuresPP(object sender, EventArgs e) {
            enregistrerMesurePPbutton.Enabled = annulerMesurePP.Enabled = true;
        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void dataGridViewStations_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {

            if (e.StateChanged == DataGridViewElementStates.Selected) {

                int idStationSelectionnee = Convert.ToInt32(e.Row.Cells["idStation"].Value);

                if (idStationCourante != idStationSelectionnee) {

                    if (enregistrerStationBouton.Enabled) {
                        if (MessageBox.Show("Voulez-vous SAUVEGARDER les données de la Station non enregistrées ?\n Attention, les données seront perdues !", "Sauvegarder la Station courante ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            EnregistrerStation(idStationCourante);
                    }

                    AfficherStationSelectionnee(idStationSelectionnee);

                }

            }

        }

        private void textRecherche_KeyUp(object sender, KeyEventArgs e){
            this.Rechercher();
        }

        private void dataGridViewStations_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewStations.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewStations.ClearSelection();
                dataGridViewStations.Rows[pos].Selected = true;
            }
        }

        private void comboModeGestion_SelectedIndexChanged(object sender, EventArgs e) {

            // On affiche les infos pour le complément si le mode de gestion sélectionné est DSP
            try {
                labelComplement.Visible = textComplementModeGestion.Visible = Convert.ToInt32(comboModeGestion.Get_SelectedId()) == (int)eStatut.ModeGestionStation_DSP;
            }
            catch (FormatException ex) { ex.ToString(); }

            this.Modification(sender, e);
        }

        private void checkMilieuSensible_CheckedChanged(object sender, EventArgs e) {
            checkMilieuSensible.ForeColor = checkMilieuSensible.Checked ? Color.Red : Color.Black;
            this.ModificationReseau(sender, e);
        }

        private void dataGridViewReseaux_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {

            if (e.StateChanged == DataGridViewElementStates.Selected) {
                this.AfficherReseauSelectionne(Convert.ToInt32(e.Row.Cells["idReseau"].Value));
            }
        }

        private void dataGridViewReseaux_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewReseaux.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewReseaux.ClearSelection();
                dataGridViewReseaux.Rows[pos].Selected = true;
            }
        }

        private void enregistrerStationBouton_Click(object sender, EventArgs e) {
            this.EnregistrerStation(Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value));
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            tabReseau.Visible = false;
        }

        private void toolStripButtonActualiser_Click(object sender, EventArgs e) {
            this.AfficherStations();
        }

        private void comboTypeStation_SelectedIndexChanged(object sender, EventArgs e) {

            comboSousTypeStation.Items.Clear();

            this.Modification(sender, e);

            int idType = 0;

            try {
                idType = Convert.ToInt32(comboTypeStation.Get_SelectedId());
            }
            catch (FormatException ex) { ex.ToString(); }

            if (idType != (int)eStatut.NonPrecise) {
                frmATE55.AlimenterComboBox("SousTypeStation", comboSousTypeStation, null, Session, idType);
                labelSousType.Visible = comboSousTypeStation.Visible = true;
            }

        }

        private void checkMasquerStations_CheckedChanged(object sender, EventArgs e) {
            this.Rechercher();
        }

        private void creerUneNouvelleStationToolStripMenuItem_Click(object sender, EventArgs e) {
            this.ViderControls();
            this.EnregistrerStation(-1);
        }

        private void NumericTextBox(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void annulerStationBouton_Click(object sender, EventArgs e) {

            enregistrerStationBouton.Enabled = annulerStationBouton.Enabled = false;

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

            AfficherStationSelectionnee(idStation);

        }

        private void supprimerLaStationToolStripMenuItem_Click(object sender, EventArgs e) {

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

            if (MessageBox.Show("Voulez-vous SUPPRIMER la Station [" + dataGridViewStations.SelectedRows[0].Cells["CollectiviteStation"].Value + "] ?\n Attention, la suppression est irréversible !", "Suppression de la Station ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                int index = dataGridViewStations.SelectedRows[0].Index;

                if (CStationAssainissement.Supprimer(Session, idStation)) {
                    dataGridViewStations.Rows.RemoveAt(index);
                }
            }

        }

        private void enregistrerReseauBouton_Click(object sender, EventArgs e) {

            int idReseau = Convert.ToInt32(dataGridViewReseaux.SelectedRows[0].Cells["idReseau"].Value);
            this.EnregistrerReseau(idReseau);

        }

        private void annulerReseauBouton_Click(object sender, EventArgs e) {

            int idReseau = Convert.ToInt32(dataGridViewReseaux.SelectedRows[0].Cells["idReseau"].Value);
            this.AfficherReseauSelectionne(idReseau);

        }

        private void creerUnNouveauReseauToolStripMenuItem_Click(object sender, EventArgs e) {
            this.ViderControlsReseau();
            this.EnregistrerReseau(-1);
        }

        private void supprimerLeReseauToolStripMenuItem_Click(object sender, EventArgs e) {

            int idReseau = Convert.ToInt32(dataGridViewReseaux.SelectedRows[0].Cells["idReseau"].Value);

            if (MessageBox.Show("Voulez-vous SUPPRIMER le Réseau ?\n Attention, la suppression est irréversible !", "Suppression du Réseau ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                int index = dataGridViewReseaux.SelectedRows[0].Index;

                if (CReseau.Supprimer(Session, idReseau)) {
                    dataGridViewReseaux.Rows.RemoveAt(index);
                }
            }

        }

        private void comboDatesBilans_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboDatesBilans.SelectedIndex != -1) {
                boutonSupprimerDateBilan.Enabled = true;
                this.AfficherMesuresBilan();
            }
        }

        private void dataGridViewBilan_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {

            if (e.StateChanged == DataGridViewElementStates.Selected) {

                int idMesure = Convert.ToInt32(e.Row.Cells["idMesure"].Value);

                this.AfficherMesureBilanSelectionnee(idMesure);

            }
        }

        private void dataGridViewBilan_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewBilan.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewBilan.ClearSelection();
                dataGridViewBilan.Rows[pos].Selected = true;
            }
        }

        private void dateNouveauBilan_ValueChanged(object sender, EventArgs e) {
            boutonAjouterBilan.Enabled = dateNouvelleDateBilan.Checked;
        }

        private void boutonAjouterBilan_Click(object sender, EventArgs e) {

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

            this.AjouterDateMesuresBilan(idStation, dateNouvelleDateBilan.Value.ToShortDateString());
        }

        private void boutonSupprimerDateBilan_Click(object sender, EventArgs e) {

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);
            
            if (MessageBox.Show("Voulez-vous SUPPRIMER les Mesures du [" + comboDatesBilans.Text + "] ?\n Attention, la suppression est irréversible !", "Suppression de Mesures ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.SupprimerDateMesuresBilan(idStation, comboDatesBilans.Text);
        }

        private void annulerMesuresBilan_Click(object sender, EventArgs e) {

            int idMesure = Convert.ToInt32(dataGridViewBilan.SelectedRows[0].Cells["idMesure"].Value);

            this.AfficherMesureBilanSelectionnee(idMesure);

        }

        private void enregistrerMesuresBilan_Click(object sender, EventArgs e) {
            
            int idMesure = Convert.ToInt32(dataGridViewBilan.SelectedRows[0].Cells["idMesure"].Value);
            this.EnregistrerMesureBilan(idMesure);
        }

        private void dateNouvelleDatePP_ValueChanged(object sender, EventArgs e) {
            ajoutDatePP.Enabled = dateNouvelleDatePP.Checked;
        }

        private void ajoutDatePP_Click(object sender, EventArgs e) {

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

            this.AjouterDateMesuresPP(idStation, dateNouvelleDatePP.Text);
        }

        private void comboDatesPP_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboDatesPP.SelectedIndex != -1) {
                supprimerDatePP.Enabled = true;
                this.AfficherMesuresPP();
            }
        }

        private void supprimerDatePP_Click(object sender, EventArgs e) {

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

            if (MessageBox.Show("Voulez-vous SUPPRIMER les Mesures du [" + comboDatesPP.Text + "] ?\n Attention, la suppression est irréversible !", "Suppression de Mesures ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.SupprimerDateMesuresPP(idStation, comboDatesPP.Text);

        }

        private void dataGridViewPP_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewPP.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewPP.ClearSelection();
                dataGridViewPP.Rows[pos].Selected = true;
            }
        }

        private void dataGridViewPP_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {

            if (e.StateChanged == DataGridViewElementStates.Selected) {

                int idMesure = Convert.ToInt32(e.Row.Cells["idMesurePP"].Value);

                this.AfficherMesurePPSelectionnee(idMesure);

            }

        }

        private void annulerMesurePP_Click(object sender, EventArgs e) {

            int idMesure = Convert.ToInt32(dataGridViewPP.SelectedRows[0].Cells["idMesurePP"].Value);

            this.AfficherMesurePPSelectionnee(idMesure);

        }

        private void enregistrerMesurePPbutton_Click(object sender, EventArgs e) {
            int idMesure = Convert.ToInt32(dataGridViewPP.SelectedRows[0].Cells["idMesurePP"].Value);
            this.EnregistrerMesurePP(idMesure);
        }

        private void boutonGenererGraphiques_Click(object sender, EventArgs e) {

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);

            this.GenererGraphiques(idStation);
        }

        private void ajouterUneCollectivitéToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.AjouterCollectivite();
        }

        private void ajouterUneCollectivitéToolStripMenuItem_Click(object sender, EventArgs e) {
            this.AjouterCollectivite();
        }

        private void dataGridViewCollectivitesStation_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewCollectivitesStation.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewCollectivitesStation.ClearSelection();
                dataGridViewCollectivitesStation.Rows[pos].Selected = true;
            }
        }

        private void supprimerLaCollectivitéToolStripMenuItem_Click(object sender, EventArgs e) {

            int idStation = Convert.ToInt32(dataGridViewStations.SelectedRows[0].Cells["idStation"].Value);
            CStation_Collectivite.Supprimer(Session, idStation, dataGridViewCollectivitesStation.SelectedRows[0].Cells["CodeCollectiviteImpacStation"].Value.ToString());
            AfficherCollectivites(idStation);

        }

        private void buttonCalculerRendement_Click(object sender, EventArgs e) {

            decimal ConcentrationEntree = numericPPEntree.Value;
            decimal ConcentrationSortie = numericPPSortie.Value;

            if (ConcentrationEntree > 0 && ConcentrationSortie > 0) {

                decimal Rendement = 100 - (ConcentrationSortie / ConcentrationEntree) * 100;

                numericPPRendement.Value = Rendement;

            }

        }

        private void listeDesStationsToolStripMenuItem_Click(object sender, EventArgs e) {
            GenererExtractionStations(Session);
        }

        private void listeDesRéseauxToolStripMenuItem_Click(object sender, EventArgs e) {
            GenererExtractionReseaux(Session);
        }

    }
}
