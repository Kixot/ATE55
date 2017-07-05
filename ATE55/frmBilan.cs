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
    public partial class frmBilan : Form {

        CSession Session;
        SqlCommand command;
        SqlDataReader dataReader;

        Dictionary<string, CEligibilite> Eligibilites;

        static int COLONNE_POTENTIELFINANCIER = 3;

        public frmBilan() {
            InitializeComponent();
            this.Text = "ATE55 - Bilan";
        }

        private void frmBilan_Load(object sender, EventArgs e) {

            this.Session = (CSession)this.Tag;

            foreach (KeyValuePair<string, CCollectivite> Collectivite in frmATE55.Collectivites) {
                comboCollectiviteEauPotable.Items.Add(Collectivite.Value.NomCollectivite);
                comboCollectiviteAssainissementCollectif.Items.Add(Collectivite.Value.NomCollectivite);
                comboCollectiviteAssainissementNonCollectif.Items.Add(Collectivite.Value.NomCollectivite);
                comboCollectiviteMilieuxAquatiques.Items.Add(Collectivite.Value.NomCollectivite);
            }

            toolStripLabel_Session.Visible = true;
            toolStripLabel_Session.Text = Session.Utilisateur.Utilisateur
                + " [" + Session.Utilisateur.CodeDomaine + "/" + Session.Utilisateur.DroitAccess + "]"
                + ", accès précédent le " + Session.Utilisateur.DerniereConnexion.ToString();


            // On alimente le comboBox pour les années ce qui déclence l'affichage des collectivités
            this.AlimenterComboAnnees();

            // Couleurs affichage nombre communes
            this.labelNbEligibles.ForeColor = frmATE55.Couleurs["Vert"];
            this.labelNbNonEligibles.ForeColor = frmATE55.Couleurs["Rouge"];
            this.labelNbTransition.ForeColor = frmATE55.Couleurs["Orange"];

            // Contour des cellules en noir
            dataGridViewEvolution.GridColor = Color.Black;

            // MouseWheel
            comboAnnees.MouseWheel += new MouseEventHandler(comboAnnees_MouseWheel);

        }

        private void AfficherDonnees(int annee) {

            // On récupère les collectivités
            string req = "SELECT idEligibilite,CodeCollectivite,PotentielFinancier,PopulationDGF,CommunesUrbaines FROM Eligibilite WHERE AnneeEligibilite = " + annee;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            Eligibilites = new Dictionary<string, CEligibilite>();
            CEligibilite eligibilite;

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    eligibilite = new CEligibilite(Session);
                    eligibilite.idEligibilite = Convert.ToInt32(dataReader["idEligibilite"]);
                    eligibilite.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                    eligibilite.PotentielFinancier = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                    eligibilite.PopulationDGF = Convert.ToInt32(dataReader["PopulationDGF"]);
                    eligibilite.CommunesUrbaines = Convert.ToInt32(dataReader["CommunesUrbaines"]);
                    eligibilite.AnneeEligibilite = annee;
                    eligibilite.EligibleAnneeCourante = eligibilite.Eligible();


                    Eligibilites.Add(eligibilite.CodeCollectivite, eligibilite);

                }
            }
            dataReader.Close();

            foreach (CEligibilite Eligibilite in Eligibilites.Values)
                Eligibilite.EligibleAnneePrecedente = Eligibilite.Eligible(annee - 1);



            //On affiche toutes les données
            this.AfficherListeGlobale(annee, true);
            this.AfficherEvolution(annee);
            this.AfficherCodecomsCA();
            this.AfficherStatistiques(annee);
        }

        private void AfficherListeGlobale(int annee, bool PremierLancement = false) {

            // Compteurs nombre communes
            int nbEligibles = 0, nbNonEligibles = 0, nbTransition = 0, popEligible = 0;

            DataGridView dgv = dataGridViewCollectivites;
            DataGridViewRow row;
            CEligibilite Eligibilite;

            dgv.Rows.Clear();
            dgv.Refresh();

            dgv.Rows.Add(Eligibilites.Count);
            int i = 0;


            // Parcourt de la liste
            foreach (KeyValuePair<string, CEligibilite> CleValeurEligibilite in Eligibilites) {

                Eligibilite = CleValeurEligibilite.Value;

                row = dgv.Rows[i];
                i++;

                row.Cells["idEligibilite"].Value = Eligibilite.idEligibilite;

                // Culey n'existe pas
                try {
                    row.Cells["CollectiviteEligibilite"].Value = frmATE55.Collectivites[Eligibilite.CodeCollectivite].NomCollectivite;
                }
                catch (KeyNotFoundException) {
                    row.Cells["CollectiviteEligibilite"].Value = Eligibilite.CodeCollectivite;
                }

                row.Cells["PotentielFinancierEligibilite"].Value = Decimal.Round(Eligibilite.PotentielFinancier, 2) + " €";
                row.Cells["PopulationEligibilite"].Value = Eligibilite.PopulationDGF;

                // Si éligible
                if (Eligibilite.EligibleAnneeCourante) {
                    popEligible += Eligibilite.PopulationDGF;
                    row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Vert"];
                    nbEligibles++;
                }
                // Sinon si éligible année précédente
                else if (Eligibilite.EligibleAnneePrecedente) {
                    row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Orange"];
                    nbTransition++;
                }
                // Sinon
                else {
                    row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Rouge"];
                    nbNonEligibles++;
                }

            }

            tabEligibilite.Visible = false;


            // Affichage des compteurs
            labelNbEligibles.Text = nbEligibles.ToString();
            labelNbNonEligibles.Text = nbNonEligibles.ToString();
            labelNbTransition.Text = nbTransition.ToString();
            labelPopulationEligible.Text = popEligible + "";

            // Le paramètre permet d'optimiser le temps de lancement de l'application
            // Quand la recherche masque la première ligne affichée elle déclenche la sélection (rowStateChanged) de la suivante plusieurs fois
            // Ce qui l'affiche et ralentit le lancement
            if (!PremierLancement)
                this.RechercherCollectivites();

        }

        private void AfficherEvolution(int annee) {

            DataGridView dgv = dataGridViewEvolution;
            DataGridViewRow row;
            CEligibilite Eligibilite;

            dgv.Rows.Clear();
            dgv.Refresh();

            // On stocke dans des listes les collectivités dont l'égibilité a évolué
            List<string> NonEligibleAEligible = new List<string>();
            List<string> PhaseTransitoireAEligible = new List<string>();
            List<string> EligibleANonEligible = new List<string>();
            List<string> PhaseTransitoire = new List<string>();

            // Si l'année sélectionnée est la plus ancienne on ne fait rien
            if (comboAnnees.SelectedIndex > 0) {

                // Parcourt de la liste
                foreach (KeyValuePair<string, CEligibilite> CleValeurEligibilite in Eligibilites) {

                    Eligibilite = CleValeurEligibilite.Value;

                    if (Eligibilite.EligibleAnneeCourante) {
                        // Si éligible mais non éligible avant
                        if (!Eligibilite.EligibleAnneePrecedente)
                            NonEligibleAEligible.Add(Eligibilite.CodeCollectivite);
                        // Si en phase transitoire avant et éligible maintenant
                        else if ((!Eligibilite.EligibleAnneePrecedente && Eligibilite.Eligible(annee - 2)))
                            PhaseTransitoireAEligible.Add(Eligibilite.CodeCollectivite);
                    }
                    else {
                        // Si non éligible mais éligible avant -> phase transitoire
                        if (Eligibilite.EligibleAnneePrecedente)
                            PhaseTransitoire.Add(Eligibilite.CodeCollectivite);
                    }

                }


                // On récupère la longueur de la liste la plus longue
                int count = NonEligibleAEligible.Count;
                count = PhaseTransitoire.Count > count ? PhaseTransitoire.Count : count;
                count = EligibleANonEligible.Count > count ? EligibleANonEligible.Count : count;
                count = PhaseTransitoireAEligible.Count > count ? PhaseTransitoireAEligible.Count : count;

                dgv.Rows.Add(count);
                int index = 0;

                // Parcourt et affichage des listes
                for (int i = 0; i < count; i++) {

                    row = dgv.Rows[index];
                    index++;

                    try {
                        row.Cells["NonEligibleAEligible"].Value = NonEligibleAEligible.Count > i ? frmATE55.Collectivites[NonEligibleAEligible.ElementAt<string>(i)].NomCollectivite : "";
                        row.Cells["TransitoireAEligible"].Value = PhaseTransitoireAEligible.Count > i ? frmATE55.Collectivites[PhaseTransitoireAEligible.ElementAt<string>(i)].NomCollectivite : "";
                        row.Cells["EligibleANonEligible"].Value = EligibleANonEligible.Count > i ? frmATE55.Collectivites[EligibleANonEligible.ElementAt<string>(i)].NomCollectivite : "";
                        row.Cells["Transitoire"].Value = PhaseTransitoire.Count > i ? frmATE55.Collectivites[PhaseTransitoire.ElementAt<string>(i)].NomCollectivite : "";
                    }
                    catch (KeyNotFoundException) {}

                }

            }

            this.labelEvolution.Text = "EVOLUTION " + (annee - 1) + "-" + annee;

            // Labels nombre
            this.labelEvolutionNbEligibleANonEligible.Text = EligibleANonEligible.Count.ToString();
            this.labelEvolutionNbTransitoireAEligible.Text = PhaseTransitoireAEligible.Count.ToString();
            this.labelEvolutionNbNonEligibleAEligible.Text = NonEligibleAEligible.Count.ToString();
            this.labelEvolutionNbTransitoire.Text = PhaseTransitoire.Count.ToString();

        }

        private void AfficherCodecomsCA() {
            
            DataGridView dgv = dataGridViewCodecoms;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();

            // On récupère les codecoms et les ca dans une liste
            List<string> CodecomsCA = new List<string>();

            string req = "SELECT CodeCollectivite FROM EPCI_V";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    CodecomsCA.Add(dataReader["CodeCollectivite"].ToString());

                }
            }
            dataReader.Close();


            // On parcourt la liste
            foreach (string CodeCollectivite in CodecomsCA) {

                // On récupère les collectivités rattachées à l'epci
                req = "SELECT CodeCollectiviteFille FROM Collectivite_Lien_V WHERE CodeCollectiviteMere = " + CodeCollectivite;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();

                // On les stocke dans une liste
                List<string> CollectivitesFilles = new List<string>();

                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read()) {

                        CollectivitesFilles.Add(dataReader["CodeCollectiviteFille"].ToString());

                    }
                }
                dataReader.Close();


                int PopulationTotale = 0;
                int PopulationNonEligible = 0;

                // On parcourt les collectivités
                foreach (string CodeCollectiviteFille in CollectivitesFilles) {

                    // On récupère les données d'éligibilité
                    try {
                        CEligibilite Eligibilite = Eligibilites[CodeCollectiviteFille];
                        PopulationTotale += Eligibilite.PopulationDGF;

                        if (!Eligibilite.EligibleAnneeCourante)
                            PopulationNonEligible += Eligibilite.PopulationDGF;
                    }
                    catch (KeyNotFoundException e) { e.ToString(); };

                }


                // Si on a aucune donnée on ne l'affiche pas
                if (PopulationTotale > 0 && PopulationNonEligible > 0) {

                    int i = dgv.Rows.Add();
                    row = dgv.Rows[i];

                    row.Cells["CodeCollectivite"].Value = CodeCollectivite;
                    row.Cells["NomCollectivite"].Value = frmATE55.Collectivites[CodeCollectivite].NomCollectivite;

                    // On affiche les populations et le prorata
                    row.Cells["PopulationDGFTotale"].Value = PopulationTotale;
                    row.Cells["PopulationCommunesNonEligibles"].Value = PopulationNonEligible;

                    // Calcul du prorata
                    decimal Prorata = Convert.ToDecimal(PopulationTotale-PopulationNonEligible)/Convert.ToDecimal(PopulationTotale)*100.0M;
                    row.Cells["Prorata"].Value = Decimal.Round(Prorata, 2) + " %";


                    // Non éligible
                    // SI population > 15 000
                    // OU SI Prorata < 0.5
                    // On colore la ligne
                    if (PopulationTotale > 15000 || Prorata < 0.5M)
                        row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Rouge"];
                    else
                        row.DefaultCellStyle.BackColor = frmATE55.Couleurs["Vert"];

                }
               
            }
            
        }

        private void AfficherEligibiliteSelectionnee(int idEligibilite) {

            tabEligibilite.Visible = true;
            this.ViderControlsEligibilite();

            string req = "SELECT * FROM Eligibilite WHERE idEligibilite = " + idEligibilite;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();

                // Récupération des données
                CEligibilite Eligibilite = new CEligibilite(Session);
                Eligibilite.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                Eligibilite.PotentielFinancier = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                Eligibilite.PopulationDGF = Convert.ToInt32(dataReader["PopulationDGF"]);
                Eligibilite.CommunesUrbaines = Convert.ToInt32(dataReader["CommunesUrbaines"]);
                Eligibilite.AnneeEligibilite = Convert.ToInt32(dataReader["AnneeEligibilite"]);

                // Affichage des données
                labelCodeCollectiviteEligibilite.Text = Eligibilite.CodeCollectivite;
                try {
                    labelNomCollectiviteEligibilite.Text = frmATE55.Collectivites[Eligibilite.CodeCollectivite].NomCollectivite;
                    // Agence de l'eau
                    labelAE.Text = frmATE55.Collectivites[Eligibilite.CodeCollectivite].AgenceEau;
                }
                catch (KeyNotFoundException e) {
                    labelNomCollectiviteEligibilite.Text = "Non renseigné";
                    labelAE.Text = "";
                    e.ToString();
                }
                labelPotentielFinancier.Text = Decimal.Round(Eligibilite.PotentielFinancier,2) + " €";
                labelPopulationDGF.Text = CMiseEnForme.SeparationMilliersInt(Eligibilite.PopulationDGF);
                labelEPCIFP.Text = dataReader["EPCI_FP"].ToString();
                comboCollectiviteEauPotable.Text = dataReader["EauPotable"].ToString();
                comboCollectiviteAssainissementCollectif.Text = dataReader["AssainissementCollectif"].ToString();
                comboCollectiviteAssainissementNonCollectif.Text = dataReader["AssainissementNonCollectif"].ToString();
                comboCollectiviteMilieuxAquatiques.Text = dataReader["MilieuxAquatiques"].ToString();
                labelSurface.Text = dataReader["Surface"].ToString() + " km²";
                labelZoneRouge.Visible = Convert.ToInt32(dataReader["ZoneRouge"]) == 1;

                dataReader.Close();


                // Eligibilite
                // Si éligible
                if (Eligibilite.Eligible()) {
                    labelEligible.ForeColor = tabPageCollectivite.BackColor = frmATE55.Couleurs["Vert"];
                    labelEligible.Text = "Eligible";
                }
                else {
                    // On teste si la collectivité est en phase de transition
                    if (Eligibilite.Eligible(Eligibilite.AnneeEligibilite - 1)) {
                        labelEligible.ForeColor = tabPageCollectivite.BackColor = frmATE55.Couleurs["Orange"];
                        labelEligible.Text = "En transition";
                    }
                    else {
                        labelEligible.ForeColor = tabPageCollectivite.BackColor = frmATE55.Couleurs["Rouge"];
                        labelEligible.Text = "Non éligible";
                    }
                }

                // On colore les labels pour l'éligibilité
                labelPotentielFinancier.ForeColor = Eligibilite.PotentielFinancier >= Session.MaxPotentielFinancierAnnee[Eligibilite.AnneeEligibilite] ? frmATE55.Couleurs["Rouge"] : Color.Black;
                labelPopulationDGF.ForeColor = Eligibilite.PopulationDGF >= Session.MaxPopulationDGF ? frmATE55.Couleurs["Rouge"] : Color.Black;

                enregistrerEligibiliteBouton.Enabled = annulerEligibiliteBouton.Enabled = false;
            }
            else
                dataReader.Close();

        }

        private void AfficherStatistiques(int annee) {

            // Nombre de stations suivies par le SATE
            string req = "SELECT Count(*) FROM StationAssainissement WHERE SuiviSATE = 1";
            command = new SqlCommand(req, Session.oConn);

            labelNbStations.Text = command.ExecuteScalar().ToString();


            // Nombre de points d'eau (codes BSS)
            req = "SELECT Count(DISTINCT BSS) FROM Captage WHERE idStatut_EtatCaptage != " + (int)eStatut.Captage_Abandonne;
            command = new SqlCommand(req, Session.oConn);

            labelNombrePointsDeau.Text = command.ExecuteScalar().ToString();


            // Nombre d'arrêtés DUP
            req = "SELECT Count(DISTINCT DateArreteDUP) FROM Captage WHERE idStatut_EtatCaptage != " + (int)eStatut.Captage_Abandonne;
            command = new SqlCommand(req, Session.oConn);

            labelNombreArretes.Text = command.ExecuteScalar().ToString();


            // Nombre points d'eau avec arrêtés
            req = "SELECT Count(DISTINCT BSS) FROM Captage WHERE idStatut_EtatCaptage != " + (int)eStatut.Captage_Abandonne + " AND DateArreteDUP IS NOT NULL";
            command = new SqlCommand(req, Session.oConn);

            labelNombrePointsDeauArrete.Text = command.ExecuteScalar().ToString();


            // Nombre de marchés et montant total
            req = "SELECT MontantMarche FROM Marche WHERE YEAR(DateSignatureMarche) = " + annee;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            int NbMarches = 0;
            decimal MontantMarches = 0;

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    NbMarches++;
                    MontantMarches += Convert.ToDecimal(dataReader["MontantMarche"]);

                }
            }
            dataReader.Close();

            labelNbMarches.Text = NbMarches+"";
            labelTotalMarches.Text = Decimal.Round(MontantMarches, 2) + " €";


            // Nombre de bilans 24h
            req = "SELECT Count(*) FROM Mesure WHERE idStatut_TypeMesure = " + (int)eStatut.Mesure_Bilan24h + " AND YEAR(DateMesure) = " + annee;
            command = new SqlCommand(req, Session.oConn);

            labelNbBilans24h.Text = command.ExecuteScalar().ToString();


            this.AfficherConventionsBilan(annee);

        }

        private void AfficherConventionsBilan(int annee) {

            // Conventions
            DataGridView dgv = dataGridViewConventionsBilan;
            DataGridViewRow row;

            dgv.Rows.Clear();
            dgv.Refresh();


            // On récupère d'abord les types
            string req = "SELECT idStatut FROM Statut WHERE CategorieStatut = 'ThèmeConvention' OR CategorieStatut = 'TypeConvention'";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            List<int> IdsTypes = new List<int>();

            // On les stocke dans une liste
            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    IdsTypes.Add(Convert.ToInt32(dataReader["idStatut"]));

                }
            }
            dataReader.Close();

            int NombreTotal = 0;
            decimal CoutTotal = 0;
            decimal CoutTotalAnnexes = 0;
            decimal TotalRemunerationTitree = 0;

            // Parcourt de la liste
            foreach (int IdType in IdsTypes) {

                int nombre = 0;
                decimal Cout = 0;
                decimal CoutAnnexes = 0;
                decimal RemunerationTitree = 0;

                // On récupère les LignesConvention de l'année et du type

                req = "SELECT MontantAnneeConvention,MontantAnnexe2Convention FROM LigneConvention INNER JOIN Convention ON LigneConvention.idConvention = Convention.idConvention WHERE AnneeLigneConvention = " + annee + " AND idStatut_TypeConvention = " + IdType;
                command = new SqlCommand(req, Session.oConn);
                dataReader = command.ExecuteReader();


                if (dataReader != null && dataReader.HasRows) {

                    while (dataReader.Read()) {

                        Cout += Convert.ToDecimal(dataReader["MontantAnneeConvention"]);
                        CoutAnnexes += Convert.ToDecimal(dataReader["MontantAnnexe2Convention"]);

                        nombre++;

                    }

                }
                dataReader.Close();


                // Affichage
                if (nombre > 0 && Cout > 0) {

                    int i = dgv.Rows.Add();
                    row = dgv.Rows[i];

                    req = "SELECT SUM(MontantTitreConvention) FROM Titre INNER JOIN LigneConvention ON Titre.idLigneConvention = LigneConvention.idLigneConvention INNER JOIN Convention ON LigneConvention.idConvention = Convention.idConvention WHERE AnneeLigneConvention = " + annee + " AND idStatut_TypeConvention = " + IdType;
                    command = new SqlCommand(req, Session.oConn);

                    RemunerationTitree =  command.ExecuteScalar().GetType().Name == "DBNull" ? 0 : Convert.ToDecimal(command.ExecuteScalar());

                    row.Cells["idTypeConvention"].Value = IdType;
                    row.Cells["TypeConvention"].Value = frmATE55.Statuts[IdType].LibelleStatut;
                    row.Cells["NombreConventions"].Value = nombre;
                    row.Cells["CoutConventions"].Value = CMiseEnForme.SeparationMilliersDecimal(Decimal.Round(Cout, 2)) + " €";
                    row.Cells["RemunerationTitree"].Value = CMiseEnForme.SeparationMilliersDecimal(Decimal.Round(RemunerationTitree, 2)) + " €";
                    row.Cells["CoutAnnexes"].Value = CMiseEnForme.SeparationMilliersDecimal(Decimal.Round(CoutAnnexes, 2)) + " €";
                    row.Cells["TotalConvention"].Value = CMiseEnForme.SeparationMilliersDecimal(Decimal.Round(Cout + CoutAnnexes, 2)) + " €";

                    row.DefaultCellStyle.BackColor = frmATE55.couleursTypes[IdType];

                    NombreTotal += nombre;
                    CoutTotal += Cout;
                    CoutTotalAnnexes += CoutAnnexes;
                    TotalRemunerationTitree += (RemunerationTitree - CoutAnnexes);

                }

            }

            // Affichage totaux
            int j = dgv.Rows.Add();
            row = dgv.Rows[j];

            row.Cells["TypeConvention"].Value = "Total";
            row.Cells["NombreConventions"].Value = NombreTotal;
            row.Cells["CoutConventions"].Value = CMiseEnForme.SeparationMilliersDecimal(Decimal.Round(CoutTotal, 2)) + " €";
            row.Cells["RemunerationTitree"].Value = CMiseEnForme.SeparationMilliersDecimal(Decimal.Round(TotalRemunerationTitree, 2)) + " €";
            row.Cells["CoutAnnexes"].Value = CMiseEnForme.SeparationMilliersDecimal(Decimal.Round(CoutTotalAnnexes, 2)) + " €";
            row.Cells["TotalConvention"].Value = CMiseEnForme.SeparationMilliersDecimal(Decimal.Round(CoutTotal + CoutTotalAnnexes, 2)) + " €";

        }
    
        private void RechercherCollectivites() {

            // On récupère le texte et on le met en minuscule
            string Recherche = textRecherche.Text.ToLower();

            // On parcourt le datagridview
            foreach (DataGridViewRow row in dataGridViewCollectivites.Rows) {

                // La recherche se fait sur le nom de la collectivité
                string NomCollectivite = row.Cells["CollectiviteEligibilite"].Value.ToString().ToLower();

                // On affiche si le nom contient la recherche
                row.Visible = NomCollectivite.Contains(Recherche);

                // Si la case est cochée on masque les collectivités éligibles (fond vert)
                if (checkMasquerCollectivitesEligibles.Checked && row.DefaultCellStyle.BackColor.Equals(frmATE55.Couleurs["Vert"]))
                    row.Visible = false;

            }
        }

        private void EnregistrerEligibilite(int idEligibilite) {

            if (idEligibilite != -1) {

                CEligibilite Eligibilite = new CEligibilite(Session);

                Eligibilite.idEligibilite = idEligibilite;
                Eligibilite.EauPotable = comboCollectiviteEauPotable.Text;
                Eligibilite.AssainissementCollectif = comboCollectiviteAssainissementCollectif.Text;
                Eligibilite.AssainissementNonCollectif = comboCollectiviteAssainissementNonCollectif.Text;
                Eligibilite.MilieuxAquatiques = comboCollectiviteMilieuxAquatiques.Text;

                if (Eligibilite.Enregistrer()) {
                    this.AfficherEligibiliteSelectionnee(idEligibilite);
                }

            }

        }

        private void AlimenterComboAnnees() {

            string req = "SELECT DISTINCT AnneeEligibilite FROM Eligibilite";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    comboAnnees.Items.Add(dataReader["AnneeEligibilite"].ToString());
                    
                }
            }
            dataReader.Close();

            // On sélectionne la dernière année
            comboAnnees.SelectedIndex = comboAnnees.Items.Count - 1;
        }

        private void GenererTableauEligibilite() {

            int Annee = Convert.ToInt32(comboAnnees.SelectedItem.ToString());

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

            FeuilXL.Name = Annee + " - Eligibilite";

            // Police
            ApplicationXL.StandardFont = "Century Gothic";


            // En-têtes
            FeuilXL.Cells[1, 1] = "Code INSEE";
            FeuilXL.Cells[1, 2] = "Nom commune";
            FeuilXL.Cells[1, 3] = "Inéligibilité " + (Annee - 1);
            FeuilXL.Cells[1, 4] = "Population DGF";
            FeuilXL.Cells[1, 5] = "Potentiel financier";
            FeuilXL.Cells[1, 6] = "Communes urbaines";
            FeuilXL.Cells[1, 7] = "Inéligibilité " + Annee;
            FeuilXL.Cells[1, 8] = "EPCI-FP";
            FeuilXL.Cells[1, 9] = "Eau potable";
            FeuilXL.Cells[1, 10] = "Assainissement collectif";
            FeuilXL.Cells[1, 11] = "Assainissement non collectif";
            FeuilXL.Cells[1, 12] = "Milieux aquatiques";
            FeuilXL.Cells[1, 13] = "AE";
            FeuilXL.Cells[1, 14] = "S (km²)";
            FeuilXL.Cells[1, 15] = "ZR";

            // Couleurs
            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[1, 15]);
            RangeHeaders.Interior.Color = CouleurEnTetes;
            RangeHeaders.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.White);
            RangeHeaders.Columns.ColumnWidth = 20;
            RangeHeaders.Font.Bold = true;


            int NumLigne = 2;


            // Récupération des éligibilités de l'année
            string req = "SELECT * FROM Eligibilite WHERE AnneeEligibilite = " + Annee;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            List<CEligibilite> Eligibilites = new List<CEligibilite>();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    CEligibilite Eligibilite = new CEligibilite(Session);

                    Eligibilite.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                    Eligibilite.PotentielFinancier = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                    Eligibilite.PopulationDGF = Convert.ToInt32(dataReader["PopulationDGF"]);
                    Eligibilite.CommunesUrbaines = Convert.ToInt32(dataReader["CommunesUrbaines"]);
                    Eligibilite.AnneeEligibilite = Annee;
                    Eligibilite.EPCI_FP = dataReader["EPCI_FP"].ToString();
                    Eligibilite.EauPotable = dataReader["EauPotable"].ToString();
                    Eligibilite.AssainissementCollectif = dataReader["AssainissementCollectif"].ToString();
                    Eligibilite.AssainissementNonCollectif = dataReader["AssainissementNonCollectif"].ToString();
                    Eligibilite.MilieuxAquatiques = dataReader["MilieuxAquatiques"].ToString();
                    Eligibilite.Surface = Convert.ToDecimal(dataReader["Surface"]);
                    Eligibilite.ZoneRouge = Convert.ToInt32(dataReader["ZoneRouge"]);

                    Eligibilites.Add(Eligibilite);

                }
            }
            dataReader.Close();


            // Parcourt et affichage des données
            foreach (CEligibilite Eligibilite in Eligibilites) {

                FeuilXL.Cells[NumLigne, 1] = Eligibilite.CodeCollectivite;
                FeuilXL.Cells[NumLigne, 4] = Eligibilite.PopulationDGF;
                FeuilXL.Cells[NumLigne, 5] = Decimal.Round(Eligibilite.PotentielFinancier,2) + "€";
                FeuilXL.Cells[NumLigne, 6] = Eligibilite.CommunesUrbaines == 1 ? "x" : "";
                FeuilXL.Cells[NumLigne, 8] = Eligibilite.EPCI_FP;
                FeuilXL.Cells[NumLigne, 9] = Eligibilite.EauPotable;
                FeuilXL.Cells[NumLigne, 10] = Eligibilite.AssainissementCollectif;
                FeuilXL.Cells[NumLigne, 11] = Eligibilite.AssainissementNonCollectif;
                FeuilXL.Cells[NumLigne, 12] = Eligibilite.MilieuxAquatiques;
                FeuilXL.Cells[NumLigne, 14] = Decimal.Round(Eligibilite.Surface, 2);
                FeuilXL.Cells[NumLigne, 15] = Eligibilite.ZoneRouge == 1 ? "x" : "";

                try {
                    FeuilXL.Cells[NumLigne, 2] = frmATE55.Collectivites[Eligibilite.CodeCollectivite].NomCollectivite;
                    FeuilXL.Cells[NumLigne, 13] = frmATE55.Collectivites[Eligibilite.CodeCollectivite].AgenceEau;
                }
                catch (KeyNotFoundException) { }


                // Si éligible
                if (Eligibilite.Eligible()) {
                    FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 7], FeuilXL.Cells[NumLigne, 7]).Interior.Color = System.Drawing.ColorTranslator.ToOle(frmATE55.Couleurs["Vert"]);
                }
                // Sinon si éligible année précédente
                else if (Eligibilite.Eligible(Annee - 1)) {
                    FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 7], FeuilXL.Cells[NumLigne, 7]).Interior.Color = System.Drawing.ColorTranslator.ToOle(frmATE55.Couleurs["Orange"]);
                    FeuilXL.Cells[NumLigne, 7] = "T";
                }
                // Sinon
                else {
                    FeuilXL.Cells[NumLigne, 7] = "x";
                    FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 7], FeuilXL.Cells[NumLigne, 7]).Interior.Color = System.Drawing.ColorTranslator.ToOle(frmATE55.Couleurs["Rouge"]);
                }

                if (Eligibilite.Eligible(Annee - 1)) {
                    FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 3], FeuilXL.Cells[NumLigne, 3]).Interior.Color = System.Drawing.ColorTranslator.ToOle(frmATE55.Couleurs["Vert"]);
                }
                else {
                    FeuilXL.Cells[NumLigne, 3] = "x";
                    FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 3], FeuilXL.Cells[NumLigne, 3]).Interior.Color = System.Drawing.ColorTranslator.ToOle(frmATE55.Couleurs["Rouge"]);
                }

                NumLigne++;

            }


            // Contours et taille
            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 15]);
            ContoursCellules(Range.Cells.Borders);
            Range.Rows.RowHeight = 30;
            AlignementCellules(Range);

            Microsoft.Office.Interop.Excel.Range RangeCodeCollectivite = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[NumLigne - 1, 1]);
            RangeCodeCollectivite.Columns.ColumnWidth = 10;

            Microsoft.Office.Interop.Excel.Range RangeCollectivite = FeuilXL.get_Range(FeuilXL.Cells[1, 2], FeuilXL.Cells[NumLigne - 1, 2]);
            RangeCollectivite.Columns.ColumnWidth = 30;
            RangeCollectivite.Font.Bold = true;

            Microsoft.Office.Interop.Excel.Range RangePopPF = FeuilXL.get_Range(FeuilXL.Cells[1, 3], FeuilXL.Cells[NumLigne - 1, 7]);
            RangePopPF.Columns.ColumnWidth = 10;


            Microsoft.Office.Interop.Excel.Range RangeCollectivites = FeuilXL.get_Range(FeuilXL.Cells[1, 8], FeuilXL.Cells[NumLigne - 1, 12]);
            RangeCollectivites.Columns.ColumnWidth = 30;

            Microsoft.Office.Interop.Excel.Range RangeZR = FeuilXL.get_Range(FeuilXL.Cells[1, 14], FeuilXL.Cells[NumLigne - 1, 15]);
            RangeZR.Columns.ColumnWidth = 5;

            ApplicationXL.Visible = true;

        }

        private void GenererRapportActivite() {

            int Annee = Convert.ToInt32(comboAnnees.SelectedItem.ToString());

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

            FeuilXL.Name = Annee + " - Rapport d'activité";

            // Police
            ApplicationXL.StandardFont = "Century Gothic";

            int NumLigne = 1;

            NumLigne = this.GenererTableauComptageCompetences(FeuilXL, NumLigne, CouleurEnTetes, Annee);
            NumLigne = this.GenererTableauMarches(FeuilXL, NumLigne, CouleurEnTetes, Annee);
            NumLigne = this.GenererTableauStations(FeuilXL, NumLigne, CouleurEnTetes, Annee);
            NumLigne = this.GenererTableauCaptagesProteges(FeuilXL, NumLigne, CouleurEnTetes, Annee);

            ApplicationXL.Visible = true;

        }

        private int GenererTableauComptageCompetences(Microsoft.Office.Interop.Excel._Worksheet FeuilXL, int NumLigne, int CouleurEnTetes, int Annee) {

            FeuilXL.Cells[NumLigne, 1] = "Nombre de conventions signées en " + Annee;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 1]).Cells.Font.Bold = true;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 1]).Cells.Font.Size = 12;

            NumLigne++;

            int NumLigneDebut = NumLigne;

            // En-têtes
            FeuilXL.Cells[NumLigne, 1] = "Thématiques";
            FeuilXL.Cells[NumLigne++, 3] = "Nombre de conventions signées en " + Annee;
            FeuilXL.Cells[NumLigne, 3] = "Bassin RM";
            FeuilXL.Cells[NumLigne, 4] = "Bassin SN";
            FeuilXL.Cells[NumLigne, 5] = "Sous-total";

            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[NumLigne - 1, 1], FeuilXL.Cells[NumLigne, 5]);
            this.CellulesEnTetes(RangeHeaders, CouleurEnTetes);

            // Fusion en-têtes
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne - 1, 1], FeuilXL.Cells[NumLigne, 2]).Cells.Merge(); // Thématiques
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne - 1, 3], FeuilXL.Cells[NumLigne - 1, 5]).Cells.Merge(); // Nb conventions
            
            NumLigne++;

            // Libellés des thématiques
            FeuilXL.Cells[NumLigne, 1] = "Assainissement";
            int NumLigneAssainissement = NumLigne++;
            FeuilXL.Cells[NumLigneAssainissement, 2] = "Programme assainissement";
            int NumLigneSPAC = NumLigne++;
            FeuilXL.Cells[NumLigneSPAC, 2] = "SPAC";
            int NumLigneSPANC = NumLigne++;
            FeuilXL.Cells[NumLigneSPANC, 2] = "SPANC";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigneSPANC - 2, 1], FeuilXL.Cells[NumLigneSPANC, 1]).Cells.Merge();
            FeuilXL.Cells[NumLigne, 1] = "Eau potable";
            int NumLigneDUP = NumLigne++;
            FeuilXL.Cells[NumLigneDUP, 2] = "Définition DUP/AAC";
            int NumLigneRealisationDUP = NumLigne++;
            FeuilXL.Cells[NumLigneRealisationDUP, 2] = "Réalisation DUP/AAC";
            int NumLigneSuiviDUP = NumLigne++;
            FeuilXL.Cells[NumLigneSuiviDUP, 2] = "Suivi DUP/AAC";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigneSuiviDUP - 2, 1], FeuilXL.Cells[NumLigneSuiviDUP, 1]).Cells.Merge();
            FeuilXL.Cells[NumLigne, 1] = "Rivières et milieux aquatiques";
            int NumLigneRivieres = NumLigne++;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigneRivieres, 1], FeuilXL.Cells[NumLigneRivieres, 2]).Cells.Merge();
            FeuilXL.Cells[NumLigne, 1] = "Diagnostic territorial";
            int NumLigneDiagnostic = NumLigne++;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigneDiagnostic, 1], FeuilXL.Cells[NumLigneDiagnostic, 2]).Cells.Merge();


            // Récupération des types de convention
            List<int> IdsTypesConventions = new List<int>();
            string req = "SELECT idStatut FROM Statut WHERE CategorieStatut = 'TypeConvention' OR CategorieStatut = 'ThèmeConvention'";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {
                    IdsTypesConventions.Add(Convert.ToInt32(dataReader["idStatut"]));
                }
            }
            dataReader.Close();

            int TotalRM = 0, TotalSN = 0;
            int RM_Assainissement = 0, SN_Assainissement = 0, RM_SPAC = 0, SN_SPAC = 0, RM_SPANC = 0, SN_SPANC = 0, RM_DUP = 0, SN_DUP = 0, RM_RealisationDUP = 0, SN_RealisationDUP = 0, RM_SuiviDUP = 0, SN_SuiviDUP = 0, RM_Rivieres = 0, SN_Rivieres = 0, RM_Diagnostic = 0, SN_Diagnostic = 0;


            // Parcourt des types
            foreach (int idType in IdsTypesConventions) {

                int RM = 0;
                int SN = 0;

                // Rhin-Meuse
                req = "SELECT Count(*) FROM LigneConvention INNER JOIN Convention ON LigneConvention.idConvention = Convention.idConvention INNER JOIN Collectivite_V ON Convention.CodeCollectivite = Collectivite_V.CodeCollectivite WHERE LigneConvention.AnneeLigneConvention = " + Annee + " AND Collectivite_V.AgenceEau = 'Rhin-Meuse' AND Convention.idStatut_TypeConvention = " + idType;
                command = new SqlCommand(req, Session.oConn);

                RM = Convert.ToInt32(command.ExecuteScalar());

                // Seine Normandie
                req = "SELECT Count(*) FROM LigneConvention INNER JOIN Convention ON LigneConvention.idConvention = Convention.idConvention INNER JOIN Collectivite_V ON Convention.CodeCollectivite = Collectivite_V.CodeCollectivite WHERE LigneConvention.AnneeLigneConvention = " + Annee + " AND Collectivite_V.AgenceEau = 'Seine Normandie' AND Convention.idStatut_TypeConvention = " + idType;
                command = new SqlCommand(req, Session.oConn);
                SN = Convert.ToInt32(command.ExecuteScalar());

                // Ajout en fonction du type
                if (idType == (int)eStatut.Assainissement || idType == (int)eStatut.Programme_d_Assainissement) {
                    RM_Assainissement += RM;
                    SN_Assainissement += SN;
                }
                else if (idType == (int)eStatut.SPAC || idType == (int)eStatut.SPAC_A || idType == (int)eStatut.SPAC_R) {
                    RM_SPAC += RM;
                    SN_SPAC += SN;
                }
                else if (idType == (int)eStatut.SPANC) {
                    RM_SPANC += RM;
                    SN_SPANC += SN;
                }
                else if (idType == (int)eStatut.Captage_DUP || idType == (int)eStatut.DUP || idType == (int)eStatut.Realisation_DUP) {
                    RM_DUP += RM;
                    SN_DUP += SN;
                }
                else if (idType == (int)eStatut.Realisation_DUP) {
                    RM_RealisationDUP += RM;
                    SN_RealisationDUP += SN;
                }
                else if (idType == (int)eStatut.Suivi_DUP) {
                    RM_SuiviDUP += RM;
                    SN_SuiviDUP += SN;
                }
                else if (idType == (int)eStatut.Rivieres_ZH) {
                    RM_Rivieres += RM;
                    SN_Rivieres += SN;
                }
                else if (idType == (int)eStatut.Diagnostic_territorial) {
                    RM_Diagnostic += RM;
                    SN_Diagnostic += SN;
                }

                TotalRM += RM;
                TotalSN += SN;

            }

            // Affichage
            FeuilXL.Cells[NumLigneAssainissement, 3] = RM_Assainissement;
            FeuilXL.Cells[NumLigneAssainissement, 4] = SN_Assainissement;
            FeuilXL.Cells[NumLigneAssainissement, 5] = RM_Assainissement+SN_Assainissement;
            FeuilXL.Cells[NumLigneSPAC, 3] = RM_SPAC;
            FeuilXL.Cells[NumLigneSPAC, 4] = SN_SPAC;
            FeuilXL.Cells[NumLigneSPAC, 5] = RM_SPAC + SN_SPAC;
            FeuilXL.Cells[NumLigneSPANC, 3] = RM_SPANC;
            FeuilXL.Cells[NumLigneSPANC, 4] = SN_SPANC;
            FeuilXL.Cells[NumLigneSPANC, 5] = RM_SPANC + SN_SPANC;
            FeuilXL.Cells[NumLigneDUP, 3] = RM_DUP;
            FeuilXL.Cells[NumLigneDUP, 4] = SN_DUP;
            FeuilXL.Cells[NumLigneDUP, 5] = RM_DUP + SN_DUP;
            FeuilXL.Cells[NumLigneRealisationDUP, 3] = RM_RealisationDUP;
            FeuilXL.Cells[NumLigneRealisationDUP, 4] = SN_RealisationDUP;
            FeuilXL.Cells[NumLigneRealisationDUP, 5] = RM_RealisationDUP + SN_RealisationDUP;
            FeuilXL.Cells[NumLigneSuiviDUP, 3] = RM_SuiviDUP;
            FeuilXL.Cells[NumLigneSuiviDUP, 4] = SN_SuiviDUP;
            FeuilXL.Cells[NumLigneSuiviDUP, 5] = RM_SuiviDUP + SN_SuiviDUP;
            FeuilXL.Cells[NumLigneRivieres, 3] = RM_Rivieres;
            FeuilXL.Cells[NumLigneRivieres, 4] = SN_Rivieres;
            FeuilXL.Cells[NumLigneRivieres, 5] = RM_Rivieres + SN_Rivieres;
            FeuilXL.Cells[NumLigneDiagnostic, 3] = RM_Diagnostic;
            FeuilXL.Cells[NumLigneDiagnostic, 4] = SN_Diagnostic;
            FeuilXL.Cells[NumLigneDiagnostic, 5] = RM_Diagnostic + SN_Diagnostic;
            FeuilXL.Cells[NumLigne, 3] = TotalRM;
            FeuilXL.Cells[NumLigne, 4] = TotalSN;
            FeuilXL.Cells[NumLigne, 5] = TotalRM + TotalSN;

            // Alignement et contours
            Microsoft.Office.Interop.Excel.Range RangeTableau = FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebut, 1], FeuilXL.Cells[NumLigne-1, 5]);
            Microsoft.Office.Interop.Excel.Range RangeTotaux = FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 3], FeuilXL.Cells[NumLigne, 5]);
            this.ContoursCellules(RangeTableau.Cells.Borders);
            this.ContoursCellules(RangeTotaux.Cells.Borders);
            this.AlignementCellules(RangeTableau);
            this.AlignementCellules(RangeTotaux);
            RangeTableau.Rows.RowHeight = 30;
            RangeTotaux.Rows.RowHeight = 30;


            NumLigne += 2;

            return NumLigne;

        }

        private int GenererTableauMarches(Microsoft.Office.Interop.Excel._Worksheet FeuilXL, int NumLigne, int CouleurEnTetes, int Annee) {

            FeuilXL.Cells[NumLigne, 1] = "Prestations d'assistance technique - " + Annee;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 1]).Cells.Font.Bold = true;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 1]).Cells.Font.Size = 12;

            NumLigne++;

            // Headers
            FeuilXL.Cells[NumLigne, 1] = "Bassin";
            FeuilXL.Cells[NumLigne, 2] = "Collectivité";
            FeuilXL.Cells[NumLigne, 3] = "Type de marché";
            FeuilXL.Cells[NumLigne, 4] = "Date d'analyse";
            FeuilXL.Cells[NumLigne, 5] = "Prestataire retenu";
            FeuilXL.Cells[NumLigne, 6] = "Montant des marchés (HT)";

            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 6]);
            this.CellulesEnTetes(RangeHeaders, CouleurEnTetes);

            int NumLigneDebut = NumLigne;

            NumLigne++;

            // Récupération des marchés de l'année suivis par le sate
            string req = "SELECT NomPrestataireMarche,IntituleMarche,MontantMarche,NomCollectivite,AgenceEau FROM Marche INNER JOIN Projet ON Marche.idProjet = Projet.idProjet INNER JOIN Collectivite_V ON Projet.CodeCollectivite = Collectivite_V.CodeCollectivite WHERE AssistanceSATE = 1 AND YEAR(DateSignatureMarche) = "+Annee+" ORDER BY Collectivite_V.AgenceEau ASC";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            int RM = 0;
            int SN = 0;
            int NumLigneDebutBassin = NumLigne;

            if (dataReader != null && dataReader.HasRows) {

                while (dataReader.Read()) {

                    string Bassin = dataReader["AgenceEau"].ToString();
                    if (Bassin.Equals("Rhin-Meuse"))
                        RM++;
                    else
                        SN++;

                    FeuilXL.Cells[NumLigne, 2] = dataReader["NomCollectivite"].ToString();
                    FeuilXL.Cells[NumLigne, 3] = dataReader["IntituleMarche"].ToString();
                    FeuilXL.Cells[NumLigne, 6] = Decimal.Round(Convert.ToDecimal(dataReader["MontantMarche"]), 2) + "€";
                    FeuilXL.Cells[NumLigne, 5] = dataReader["NomPrestataireMarche"];
                    FeuilXL.Cells[NumLigne, 4] = "-";

                    NumLigne++;

                }
            }
            dataReader.Close();


            // Fusion des noms de bassins
            if (SN == 0) {
                FeuilXL.Cells[NumLigneDebutBassin, 1] = "Rhin-Meuse";
                FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebutBassin, 1], FeuilXL.Cells[NumLigneDebutBassin + RM - 1, 1]).Cells.Merge();
            }
            else if (RM == 0) {
                FeuilXL.Cells[NumLigneDebutBassin, 1] = "Seine-Normandie";
                FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebutBassin, 1], FeuilXL.Cells[NumLigneDebutBassin + SN - 1, 1]).Cells.Merge();
            }
            else {
                FeuilXL.Cells[NumLigneDebutBassin, 1] = "Rhin-Meuse";
                FeuilXL.Cells[NumLigneDebutBassin + RM, 1] = "Seine-Normandie";
                FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebutBassin, 1], FeuilXL.Cells[NumLigneDebutBassin + RM - 1, 1]).Cells.Merge();
                FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebutBassin + RM, 1], FeuilXL.Cells[NumLigneDebutBassin + RM + SN - 1, 1]).Cells.Merge();
            }


            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebut, 1], FeuilXL.Cells[NumLigne - 1, 6]);
            ContoursCellules(Range.Borders);
            AlignementCellules(Range);
            Range.Rows.RowHeight = 40;

            Microsoft.Office.Interop.Excel.Range RangeBassin = FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebut, 1], FeuilXL.Cells[NumLigne - 1, 1]);
            RangeBassin.Font.Bold = true;

            NumLigne += 3;
            return NumLigne;
        }

        private int GenererTableauStations(Microsoft.Office.Interop.Excel._Worksheet FeuilXL, int NumLigne, int CouleurEnTetes, int Annee) {

            FeuilXL.Cells[NumLigne, 1] = "Stations de traitement des eaux usées éligibles au SATE en " + Annee;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 1]).Cells.Font.Bold = true;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 1]).Cells.Font.Size = 12;

            NumLigne++;

            // Headers
            FeuilXL.Cells[NumLigne, 1] = "Caractérisation";
            FeuilXL.Cells[NumLigne, 4] = "Eligible au SATE";
            FeuilXL.Cells[NumLigne, 5] = "Non-Eligible au SATE";
            FeuilXL.Cells[NumLigne, 6] = "Total";


            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 3]).Cells.Merge();
            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 6]);
            this.CellulesEnTetes(RangeHeaders, CouleurEnTetes);

            int NumLigneDebut = NumLigne;

            NumLigne++;


            // Headers à gauche
            FeuilXL.Cells[NumLigne, 1] = "Bassin Rhin-Meuse";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne + 3, 1]).Cells.Merge();
            FeuilXL.Cells[NumLigne + 4, 1] = "Bassin Seine-Normandie";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne + 4, 1], FeuilXL.Cells[NumLigne + 7, 1]).Cells.Merge();
            FeuilXL.Cells[NumLigne + 8, 1] = "Département";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne + 8, 1], FeuilXL.Cells[NumLigne + 11, 1]).Cells.Merge();
            FeuilXL.Cells[NumLigne, 2] = "Nombre";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 2], FeuilXL.Cells[NumLigne + 1, 2]).Cells.Merge();
            FeuilXL.Cells[NumLigne + 2, 2] = "Capacité de traitement";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne + 2, 2], FeuilXL.Cells[NumLigne + 3, 2]).Cells.Merge();
            FeuilXL.Cells[NumLigne + 4, 2] = "Nombre";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne + 4, 2], FeuilXL.Cells[NumLigne + 5, 2]).Cells.Merge();
            FeuilXL.Cells[NumLigne + 6, 2] = "Capacité de traitement";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne + 6, 2], FeuilXL.Cells[NumLigne + 7, 2]).Cells.Merge();
            FeuilXL.Cells[NumLigne + 8, 2] = "Nombre";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne + 8, 2], FeuilXL.Cells[NumLigne + 9, 2]).Cells.Merge();
            FeuilXL.Cells[NumLigne + 10, 2] = "Capacité de traitement";
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne + 10, 2], FeuilXL.Cells[NumLigne + 11, 2]).Cells.Merge();
            FeuilXL.Cells[NumLigne, 3] = "Valeur";
            FeuilXL.Cells[NumLigne+1, 3] = "Ratio";
            FeuilXL.Cells[NumLigne+2, 3] = "Valeur (EqH)";
            FeuilXL.Cells[NumLigne+3, 3] = "Ratio";
            FeuilXL.Cells[NumLigne+4, 3] = "Valeur";
            FeuilXL.Cells[NumLigne+5, 3] = "Ratio";
            FeuilXL.Cells[NumLigne+6, 3] = "Valeur (EqH)";
            FeuilXL.Cells[NumLigne+7, 3] = "Ratio";
            FeuilXL.Cells[NumLigne+8, 3] = "Valeur";
            FeuilXL.Cells[NumLigne+9, 3] = "Ratio";
            FeuilXL.Cells[NumLigne+10, 3] = "Valeur (EqH)";
            FeuilXL.Cells[NumLigne+11, 3] = "Ratio";


            int RM_NbEligibles = 0, RM_NbNonEligible = 0, RM_CapaciteEligible = 0, RM_CapaciteNonEligible = 0, SN_NbEligibles = 0, SN_NbNonEligible = 0, SN_CapaciteEligible = 0, SN_CapaciteNonEligible = 0;

            // Récupération des stations
            string req = "SELECT Capacite, PopulationDGF, PotentielFinancier, CommunesUrbaines, AgenceEau FROM StationAssainissement INNER JOIN Eligibilite ON StationAssainissement.CodeCollectiviteLocalisation = Eligibilite.CodeCollectivite INNER JOIN Collectivite_V ON StationAssainissement.CodeCollectiviteLocalisation = Collectivite_V.CodeCollectivite WHERE AnneeEligibilite = " + Annee;
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    // Test d'éligibilité
                    CEligibilite Eligibilite = new CEligibilite(Session);

                    Eligibilite.PopulationDGF = Convert.ToInt32(dataReader["PopulationDGF"]);
                    Eligibilite.PotentielFinancier = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                    Eligibilite.CommunesUrbaines = Convert.ToInt32(dataReader["CommunesUrbaines"]);
                    Eligibilite.AnneeEligibilite = Annee;

                    if (Eligibilite.Eligible()) {
                        if (dataReader["AgenceEau"].Equals("Rhin-Meuse")) {
                            RM_NbEligibles++;
                            RM_CapaciteEligible += Convert.ToInt32(dataReader["Capacite"]);
                        }
                        else {
                            SN_NbEligibles++;
                            SN_CapaciteEligible += Convert.ToInt32(dataReader["Capacite"]);
                        }
                    }
                    else {
                        if (dataReader["AgenceEau"].Equals("Rhin-Meuse")) {
                            RM_NbNonEligible++;
                            RM_CapaciteNonEligible += Convert.ToInt32(dataReader["Capacite"]);
                        }
                        else {
                            SN_NbNonEligible++;
                            SN_CapaciteNonEligible += Convert.ToInt32(dataReader["Capacite"]);
                        }
                    }

                }
            }
            dataReader.Close();


            // Affichage des données
            int TotalRM = RM_NbEligibles + RM_NbNonEligible;
            int TotalCapaciteRM = RM_CapaciteEligible + RM_CapaciteNonEligible;
            int TotalSN = SN_NbEligibles + SN_NbNonEligible;
            int TotalCapaciteSN = SN_CapaciteEligible + SN_CapaciteNonEligible;
            int Total = TotalRM + TotalSN;
            int TotalCapacite = TotalCapaciteRM + TotalCapaciteSN;

            FeuilXL.Cells[NumLigne, 4] = RM_NbEligibles;
            FeuilXL.Cells[NumLigne, 5] = RM_NbNonEligible;
            FeuilXL.Cells[NumLigne++, 6] = TotalRM;
            FeuilXL.Cells[NumLigne, 4] = Decimal.Round((decimal)RM_NbEligibles / (decimal)TotalRM * 100, 2) + " %";
            FeuilXL.Cells[NumLigne, 5] = Decimal.Round((decimal)RM_NbNonEligible / (decimal)TotalRM * 100, 2) + " %";
            FeuilXL.Cells[NumLigne++, 6] = "100 %";
            FeuilXL.Cells[NumLigne, 4] = RM_CapaciteEligible;
            FeuilXL.Cells[NumLigne, 5] = RM_CapaciteNonEligible;
            FeuilXL.Cells[NumLigne++, 6] = TotalCapaciteRM;
            FeuilXL.Cells[NumLigne, 4] = Decimal.Round((decimal)RM_CapaciteEligible / (decimal)TotalCapaciteRM * 100, 2) + " %";
            FeuilXL.Cells[NumLigne, 5] = Decimal.Round((decimal)RM_CapaciteNonEligible / (decimal)TotalCapaciteRM * 100, 2) + " %";
            FeuilXL.Cells[NumLigne++, 6] = "100 %";
            FeuilXL.Cells[NumLigne, 4] = SN_NbEligibles;
            FeuilXL.Cells[NumLigne, 5] = SN_NbNonEligible;
            FeuilXL.Cells[NumLigne++, 6] = TotalSN;
            FeuilXL.Cells[NumLigne, 4] = Decimal.Round((decimal)SN_NbEligibles / (decimal)TotalSN * 100, 2) + " %";
            FeuilXL.Cells[NumLigne, 5] = Decimal.Round((decimal)SN_NbNonEligible / (decimal)TotalSN * 100, 2) + " %";
            FeuilXL.Cells[NumLigne++, 6] = "100 %";
            FeuilXL.Cells[NumLigne, 4] = SN_CapaciteEligible;
            FeuilXL.Cells[NumLigne, 5] = SN_CapaciteNonEligible;
            FeuilXL.Cells[NumLigne++, 6] = TotalCapaciteSN;
            FeuilXL.Cells[NumLigne, 4] = Decimal.Round((decimal)SN_CapaciteEligible / (decimal)TotalCapaciteSN * 100, 2) + " %";
            FeuilXL.Cells[NumLigne, 5] = Decimal.Round((decimal)SN_CapaciteNonEligible / (decimal)TotalCapaciteSN * 100, 2) + " %";
            FeuilXL.Cells[NumLigne++, 6] = "100 %";
            FeuilXL.Cells[NumLigne, 4] = RM_NbEligibles + SN_NbEligibles;
            FeuilXL.Cells[NumLigne, 5] = RM_NbNonEligible + SN_NbNonEligible;
            FeuilXL.Cells[NumLigne++, 6] = Total;
            FeuilXL.Cells[NumLigne, 4] = Decimal.Round(((decimal)RM_NbEligibles + (decimal)SN_NbEligibles) / (decimal)Total * 100, 2) + " %";
            FeuilXL.Cells[NumLigne, 5] = Decimal.Round(((decimal)RM_NbNonEligible + (decimal)SN_NbNonEligible) / (decimal)Total * 100, 2) + " %";
            FeuilXL.Cells[NumLigne++, 6] = "100 %";
            FeuilXL.Cells[NumLigne, 4] = RM_CapaciteEligible + SN_CapaciteEligible;
            FeuilXL.Cells[NumLigne, 5] = RM_CapaciteNonEligible + SN_CapaciteNonEligible;
            FeuilXL.Cells[NumLigne++, 6] = TotalCapacite;
            FeuilXL.Cells[NumLigne, 4] = Decimal.Round(((decimal)RM_CapaciteEligible + (decimal)SN_CapaciteEligible) / (decimal)TotalCapacite * 100, 2) + " %";
            FeuilXL.Cells[NumLigne, 5] = Decimal.Round(((decimal)RM_CapaciteNonEligible + (decimal)SN_CapaciteNonEligible) / (decimal)TotalCapacite * 100, 2) + " %";
            FeuilXL.Cells[NumLigne, 6] = "100 %";

            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebut, 1], FeuilXL.Cells[NumLigne, 6]);
            ContoursCellules(Range.Borders);
            AlignementCellules(Range);
            Range.Rows.RowHeight = 30;

            Microsoft.Office.Interop.Excel.Range RangeDepartement = FeuilXL.get_Range(FeuilXL.Cells[NumLigne-3, 1], FeuilXL.Cells[NumLigne, 6]);
            RangeDepartement.Font.Bold = true;


            NumLigne += 3;
            return NumLigne;

        }

        private int GenererTableauCaptagesProteges(Microsoft.Office.Interop.Excel._Worksheet FeuilXL, int NumLigne, int CouleurEnTetes, int Annee) {

            FeuilXL.Cells[NumLigne, 1] = "Liste des points d'eau protégés par arrêté de DUP en " + Annee;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 1]).Cells.Font.Bold = true;
            FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 1]).Cells.Font.Size = 12;

            NumLigne++;

            // Headers
            FeuilXL.Cells[NumLigne, 1] = "Collectivité";
            FeuilXL.Cells[NumLigne, 2] = "Points d'eau";
            FeuilXL.Cells[NumLigne, 3] = "Date de l'arrêté de DUP";
            FeuilXL.Cells[NumLigne, 4] = "Bassin";

            Microsoft.Office.Interop.Excel.Range RangeHeaders = FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 4]);
            this.CellulesEnTetes(RangeHeaders, CouleurEnTetes);

            int NumLigneDebut = NumLigne;

            NumLigne++;

            int NumLigneDebutBassin = NumLigne+1;

            // Récupération des captages protégés par dup
            string req = "SELECT NomCollectivite, NomRessource, DateArreteDUP, AgenceEau FROM Captage INNER JOIN Collectivite_V ON Captage.CodeCollectivite = Collectivite_V.CodeCollectivite WHERE YEAR(DateArreteDUP) = " + Annee + " ORDER BY AgenceEau,NomCollectivite";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            int RM = 0;
            int SN = 0;

            if (dataReader != null && dataReader.HasRows) {

                string NomCollectivite = "";
                int NbRessources = 0;
                string Ressources = "";

                while (dataReader.Read()) {

                    if (NomCollectivite.Equals(dataReader["NomCollectivite"].ToString())) {
                        Ressources += "\n" + dataReader["NomRessource"].ToString();
                        NbRessources++;
                        FeuilXL.Cells[NumLigne, 2] = Ressources;
                    }
                    else {

                        FeuilXL.get_Range(FeuilXL.Cells[NumLigne, 1], FeuilXL.Cells[NumLigne, 4]).Rows.RowHeight = 30 * NbRessources;

                        NumLigne++;

                        NbRessources = 1;

                        NomCollectivite = dataReader["NomCollectivite"].ToString();
                        FeuilXL.Cells[NumLigne, 1] = dataReader["NomCollectivite"].ToString();
                        Ressources = dataReader["NomRessource"].ToString();
                        FeuilXL.Cells[NumLigne, 2] = Ressources;
                        FeuilXL.Cells[NumLigne, 3] = Convert.ToDateTime(dataReader["DateArreteDUP"]).ToShortDateString();

                        if (dataReader["AgenceEau"].ToString().Equals("Rhin-Meuse"))
                            RM++;
                        else
                            SN++;
                        
                    }

                }
            }
            dataReader.Close();


            // Fusion des noms de bassins
            if (SN == 0) {
                FeuilXL.Cells[NumLigneDebutBassin, 4] = "Rhin-Meuse";
                FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebutBassin, 4], FeuilXL.Cells[NumLigneDebutBassin + RM - 1, 4]).Cells.Merge();
            }
            else if (RM == 0) {
                FeuilXL.Cells[NumLigneDebutBassin, 1] = "Seine-Normandie";
                FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebutBassin, 4], FeuilXL.Cells[NumLigneDebutBassin + SN - 1, 4]).Cells.Merge();
            }
            else {
                FeuilXL.Cells[NumLigneDebutBassin, 4] = "Rhin-Meuse";
                FeuilXL.Cells[NumLigneDebutBassin + RM, 4] = "Seine-Normandie";
                FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebutBassin, 4], FeuilXL.Cells[NumLigneDebutBassin + RM - 1, 4]).Cells.Merge();
                FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebutBassin + RM, 4], FeuilXL.Cells[NumLigneDebutBassin + RM + SN - 1, 4]).Cells.Merge();
            }


            Microsoft.Office.Interop.Excel.Range Range = FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebut, 1], FeuilXL.Cells[NumLigne, 4]);
            ContoursCellules(Range.Borders);
            AlignementCellules(Range);

            Microsoft.Office.Interop.Excel.Range RangeCollectivites = FeuilXL.get_Range(FeuilXL.Cells[NumLigneDebut, 1], FeuilXL.Cells[NumLigne, 1]);
            RangeCollectivites.Font.Bold = true;


            NumLigne+=3;

            return NumLigne;
        }

        private void CellulesEnTetes(Microsoft.Office.Interop.Excel.Range Range, int CouleurEnTetes) {
            Range.Interior.Color = CouleurEnTetes;
            Range.Font.Color = System.Drawing.ColorTranslator.ToOle(Color.White);
            Range.Font.Bold = true;
            Range.Columns.ColumnWidth = 20;
            Range.Rows.RowHeight = 30;
        }

        private void AlignementCellules(Microsoft.Office.Interop.Excel.Range Range) {
            Range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Range.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
        }

        private void ContoursCellules(Microsoft.Office.Interop.Excel.Borders borders) {
            foreach (Microsoft.Office.Interop.Excel.Border Border in borders)
                borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }

        private void ViderControlsEligibilite() {
            labelCodeCollectiviteEligibilite.Text = labelNomCollectiviteEligibilite.Text = "";
            labelZoneRouge.Visible = false;
        }

        private void Modification(object sender, EventArgs e) {
            enregistrerEligibiliteBouton.Enabled = annulerEligibiliteBouton.Enabled = true;
        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void dataGridViewCollectivites_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e) {

            if (e.StateChanged == DataGridViewElementStates.Selected) {
                int idEligibilite = Convert.ToInt32(e.Row.Cells["idEligibilite"].Value);
                AfficherEligibiliteSelectionnee(idEligibilite);
            }
        }

        private void dataGridViewCollectivites_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewCollectivites.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewCollectivites.ClearSelection();
                dataGridViewCollectivites.Rows[pos].Selected = true;
            }
        }

        private void comboAnnees_SelectedIndexChanged(object sender, EventArgs e) {
            // On affiche les collectivités de l'année sélectionnée
            this.AfficherDonnees(Convert.ToInt32(comboAnnees.SelectedItem.ToString()));
        }

        private void checkMasquerCollectivitesEligibles_CheckedChanged(object sender, EventArgs e) {
            this.RechercherCollectivites();
        }

        private void comboAnnees_MouseWheel(object sender, MouseEventArgs e) {
            // Empêcher le scroll accidentel sur le combobox
            ((HandledMouseEventArgs)e).Handled = true;
        }

        private void toolStripButtonActualiser_Click(object sender, EventArgs e) {
            this.AfficherDonnees(Convert.ToInt32(comboAnnees.SelectedItem.ToString()));            
        }

        private void textRecherche_KeyUp(object sender, KeyEventArgs e) {
            this.RechercherCollectivites();
        }

        private void annulerEligibiliteBouton_Click(object sender, EventArgs e) {

            int idEligibilite = Convert.ToInt32(dataGridViewCollectivites.SelectedRows[0].Cells["idEligibilite"].Value);

            this.AfficherEligibiliteSelectionnee(idEligibilite);

        }

        private void enregistrerEligibiliteBouton_Click(object sender, EventArgs e) {

            int idEligibilite = Convert.ToInt32(dataGridViewCollectivites.SelectedRows[0].Cells["idEligibilite"].Value);

            this.EnregistrerEligibilite(idEligibilite);
        }

        private void dataGridViewConventionsBilan_MouseDown(object sender, MouseEventArgs e) {
            int pos = dataGridViewConventionsBilan.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1) {
                dataGridViewConventionsBilan.ClearSelection();
                dataGridViewConventionsBilan.Rows[pos].Selected = true;
            }
        }

        private void dataGridViewCollectivites_SortCompare(object sender, DataGridViewSortCompareEventArgs e) {
            // Tri par nombre

            // Colonne Potentiel Financier
            if (e.Column.Index == COLONNE_POTENTIELFINANCIER) {

                decimal PF1 = Convert.ToDecimal(e.CellValue1.ToString().Split(' ')[0]);
                decimal PF2 = Convert.ToDecimal(e.CellValue2.ToString().Split(' ')[0]);

                e.SortResult = PF1.CompareTo(PF2);
                e.Handled = true;

            }
        }

        private void buttonExtractionsTableauxRapport_Click(object sender, EventArgs e) {
            this.GenererRapportActivite();
        }

        private void buttonExtractionConventions_Click(object sender, EventArgs e) {
            frmConventions.GenererExtractionConventions(Session);
        }

        private void buttonExtractionStations_Click(object sender, EventArgs e) {
            frmAssainissement.GenererExtractionStations(Session);
        }

        private void buttonExtraireSubventionsAnnee_Click(object sender, EventArgs e) {
            frmSubventions.GenererExtractionStations(Session, Convert.ToInt32(comboAnnees.SelectedItem.ToString()));
        }

        private void buttonExtraireSubventionsSoldees_Click(object sender, EventArgs e) {
            frmSubventions.GenererExtractionStations(Session, -1, true, true);
        }

        private void buttonExtraireSubventionsNonSoldees_Click(object sender, EventArgs e) {
            frmSubventions.GenererExtractionStations(Session, -1, true, false);
        }

        private void buttonExtraireTableauEligibilite_Click(object sender, EventArgs e) {
            this.GenererTableauEligibilite();
        }

        private void buttonExtractionReseaux_Click(object sender, EventArgs e) {
            frmAssainissement.GenererExtractionReseaux(Session);
        }

    }
}
