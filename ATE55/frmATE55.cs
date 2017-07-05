using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;

namespace ATE55
{
    public partial class frmATE55 : Form {
        
        //this.boutonEauPotable.Image = ((Image)new Bitmap((Image)(resources.GetObject("boutonEauPotable.Image")), new Size(200, 200)));  
        
        /// <summary>session d'accès en cours</summary>
        CSession Session;
        public static Dictionary<string, CCollectivite> Collectivites;
        public static Dictionary<int, CStatut> Statuts;
        public static Dictionary<string, Color> Couleurs;
        public static Dictionary<int, Color> couleursTypes;

        SqlCommand command;
        SqlDataReader dataReader;

        private frmEauPotable frmEauPotable;
        private frmAssainissement frmAssainissement;
        private frmConventions frmConventions;
        private frmProjets frmProjets;
        private frmSubventions frmSubventions;
        private frmBilan frmBilan;


        public frmATE55() {
            Session = new CSession();   // Initialisation de la session
            InitializeComponent();
            this.CacherInterface();
        }

        private void ChargerCollectivites() {
            // On charge les collectivités dans un dictionnaire
            Collectivites = new Dictionary<string, CCollectivite>();
            string req = "SELECT * FROM Collectivite_V ORDER BY NomCollectivite";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            if (dataReader != null) {
                while (dataReader.Read()) {

                    CCollectivite collectivite = new CCollectivite();
                    collectivite.CodeCollectivite = dataReader["CodeCollectivite"].ToString();
                    collectivite.NomCollectivite = dataReader["NomCollectivite"].ToString();
                    collectivite.NatureCollectivite = dataReader["NatureCollectivite"].ToString();
                    collectivite.Inactif = dataReader["Inactif"].ToString();
                    collectivite.AgenceEau = dataReader["AgenceEau"].ToString();

                    Collectivites.Add(dataReader["CodeCollectivite"].ToString(), collectivite);
                }
            }
            dataReader.Close();
        }

        private void ChargerStatuts() {

            // On récupère les statuts pour les stocker dans un dictionnaire
            // Cela permet d'économiser et de réduire considérablement le nombre de requêtes sur la bdd
            Statuts = new Dictionary<int, CStatut>();

            string req = "SELECT * FROM Statut";
            command = new SqlCommand(req, Session.oConn);
            dataReader = command.ExecuteReader();

            CStatut Statut;

            if (dataReader != null && dataReader.HasRows) {
                while (dataReader.Read()) {

                    Statut = new CStatut();
                    Statut.idStatut = Convert.ToInt32(dataReader["idStatut"]);
                    Statut.CategorieStatut = dataReader["CategorieStatut"].ToString();
                    Statut.LibelleStatut = dataReader["LibelleStatut"].ToString();
                    Statut.IconeStatut = dataReader["IconeStatut"].ToString();
                    Statut.OrdreTriStatut = Convert.ToInt32(dataReader["OrdreTriStatut"]);
                    Statut.CouleurTexte = dataReader["CouleurTexte"].ToString();
                    Statut.idStatutMere = dataReader["idStatutMere"].GetType().Name != "DBNull" ? Convert.ToInt32(dataReader["idStatutMere"]) : -1;

                    Statuts.Add(Statut.idStatut, Statut);
                }
            }
            dataReader.Close();


        }

        private void ChargerCouleurs() {

            Couleurs = new Dictionary<string, Color>();
            
            Couleurs.Add("Gris", Color.FromArgb(180, 180, 180));
            Couleurs.Add("Rouge", Color.FromArgb(255, 108, 108));
            Couleurs.Add("Vert", Color.FromArgb(56, 255, 158));
            Couleurs.Add("Orange", Color.FromArgb(255, 126, 24));
            Couleurs.Add("Bleu", Color.FromArgb(51, 153, 204));
            Couleurs.Add("Captage", Color.Blue);
            Couleurs.Add("UD", Color.Green);

            // Couleurs des cellules en fonction du type
            couleursTypes = new Dictionary<int, Color>();
            Color CouleurAssainissmenet = Color.FromArgb(252, 228, 214);
            Color CouleurDUP = Color.FromArgb(255, 242, 204);
            Color CouleurRivieres = Color.FromArgb(194, 231, 250);
            Color CouleurDiag = Color.FromArgb(217, 225, 242);
            Color Blanc = Color.White;
            couleursTypes.Add((int)eStatut.Assainissement, CouleurAssainissmenet);
            couleursTypes.Add((int)eStatut.SPAC, CouleurAssainissmenet);
            couleursTypes.Add((int)eStatut.SPAC_A, CouleurAssainissmenet);
            couleursTypes.Add((int)eStatut.SPAC_R, CouleurAssainissmenet);
            couleursTypes.Add((int)eStatut.SPANC, CouleurAssainissmenet);
            couleursTypes.Add((int)eStatut.Programme_d_Assainissement, CouleurAssainissmenet);
            couleursTypes.Add((int)eStatut.DUP, CouleurDUP);
            couleursTypes.Add((int)eStatut.Captage_DUP, CouleurDUP);
            couleursTypes.Add((int)eStatut.SDAGE, CouleurDUP);
            couleursTypes.Add((int)eStatut.DUP_SDAGE, CouleurDUP);
            couleursTypes.Add((int)eStatut.Realisation_DUP, CouleurDUP);
            couleursTypes.Add((int)eStatut.Suivi_DUP, CouleurDUP);
            couleursTypes.Add((int)eStatut.Rivieres_ZH, CouleurRivieres);
            couleursTypes.Add((int)eStatut.Diagnostic_territorial, CouleurDiag);
            couleursTypes.Add((int)eStatut.Audit_Gestion, Blanc);
            couleursTypes.Add(-1, Blanc);
            couleursTypes.Add(0, Blanc);

        }

        private void CacherInterface() {
            boutonEauPotable.Hide();
            boutonAssainissement.Hide();
            boutonConventions.Hide();
            boutonProjets.Hide();
            boutonSubventions.Hide();
            boutonBilan.Hide();
        }

        private void AfficherInterface() {
            boutonEauPotable.Show();
            boutonAssainissement.Show();
            boutonConventions.Show();
            boutonProjets.Show();
            boutonSubventions.Show();
            boutonBilan.Show();
        }

        private void OuvrirForm(String nom) {

            Form formOpen = Application.OpenForms[nom];

            if (formOpen == null) {
                switch (nom) {
                    case "frmEauPotable":
                        frmEauPotable = new frmEauPotable();
                        frmEauPotable.Tag = Session;
                        frmEauPotable.Show();
                        break;
                    case "frmAssainissement":
                        frmAssainissement = new frmAssainissement();
                        frmAssainissement.Tag = Session;
                        frmAssainissement.Show();
                        break;
                    case "frmConventions":
                        frmConventions = new frmConventions();
                        frmConventions.Tag = Session;
                        frmConventions.Show();
                        break;
                    case "frmProjets":
                        frmProjets = new frmProjets();
                        frmProjets.Tag = Session;
                        frmProjets.Show();
                        break;
                    case "frmSubventions":
                        frmSubventions = new frmSubventions();
                        frmSubventions.Tag = Session;
                        frmSubventions.Show();
                        break;
                    case "frmBilan":
                        frmBilan = new frmBilan();
                        frmBilan.Tag = Session;
                        frmBilan.Show();
                        break;
                }
            }
        }

        // Décommenter dans mnu_Connexion_Click pour importer
        private void ImporterEligibilites() {
            // Eligibilite2017 dans .\Imports\

            string path2016 = Application.StartupPath + @"\Imports\Eligibilite2016.csv";
            string path2017 = Application.StartupPath + @"\Imports\Eligibilite2017.csv";

            Dictionary<int, string> paths = new Dictionary<int, string>();
            paths.Add(2016, path2016);
            paths.Add(2017, path2017);

            foreach (KeyValuePair<int, string> FichierEligibilite in paths) {

                int annee = FichierEligibilite.Key;
                string path = FichierEligibilite.Value;

                using(var fs = File.OpenRead(path))
                using (var reader = new StreamReader(fs)) {

                    reader.ReadLine();

                    while (!reader.EndOfStream) {

                        string line = reader.ReadLine();
                        string[] values = line.Split(';');

                        CEligibilite Eligibilite = new CEligibilite(Session);
                        Eligibilite.CodeCollectivite = values[0];
                        this.Text = Eligibilite.CodeCollectivite;
                        Eligibilite.PotentielFinancier = Convert.ToDecimal(values[7]);
                        Eligibilite.PopulationDGF = Convert.ToInt32(values[4]);
                        Eligibilite.AnneeEligibilite = annee;
                        Eligibilite.CommunesUrbaines = values[5].Equals("OUI") ? 1 : 0;
                        Eligibilite.EPCI_FP = values[10];
                        Eligibilite.EauPotable = values[11];
                        Eligibilite.AssainissementCollectif = values[12];
                        Eligibilite.AssainissementNonCollectif = values[13];
                        Eligibilite.MilieuxAquatiques = values[14];
                        Eligibilite.Surface = values[15].Equals("") ? 0.00M : Convert.ToDecimal(values[15]);
                        Eligibilite.ZoneRouge = values[16].Equals("") ? 0 : 1;

                        if(!Eligibilite.Creer())
                            return;

                    }

                }

            }

        }

        public static void AlimenterComboBox(string Categorie, CustomComboBox.CustomComboBox combo, ImageList imageList, CSession s, int Theme) {

            CustomComboBox.CustomComboBoxItem item;
            DataView view;

            if (Theme == -1)
                view = new DataView(s.ds.Tables["Statut"], "CategorieStatut='" + Categorie + "' OR CategorieStatut = 'TOUS'", "OrdreTriStatut,LibelleStatut", DataViewRowState.CurrentRows);
            else
                view = new DataView(s.ds.Tables["Statut"], "CategorieStatut='TOUS' OR idStatutMere="+Theme, "OrdreTriStatut,LibelleStatut", DataViewRowState.CurrentRows);
            
            for (int i = 0; i < view.Count; i++) {
                item = new CustomComboBox.CustomComboBoxItem(view[i]["idStatut"].ToString(), view[i]["LibelleStatut"].ToString(), combo.ForeColor, false);
                if(imageList != null)
                    item.IndexImageList = imageList.Images.IndexOfKey(view[i]["IconeStatut"].ToString());
                combo.Items.Add(item);
            }

        }

        private void boutonEauPotable_Click(object sender, EventArgs e) {
            OuvrirForm("frmEauPotable");
        }

        private void boutonAssainissement_Click(object sender, EventArgs e) {
            OuvrirForm("frmAssainissement");
        }

        private void boutonConventions_Click(object sender, EventArgs e) {
            OuvrirForm("frmConventions");
        }

        private void boutonProjets_Click(object sender, EventArgs e) {
            OuvrirForm("frmProjets");
        }

        private void boutonSubventions_Click(object sender, EventArgs e) {
            OuvrirForm("frmSubventions");
        }

        private void boutonBilan_Click(object sender, EventArgs e) {
            OuvrirForm("frmBilan");
        }

        private void frm_Shown(object sender, EventArgs e) {
            mnu_Connexion_Click(sender, e);
        }

        /// <summary>Chargement du formulaire principal</summary>
        private void frmATE55_Load(object sender, EventArgs e) {

            ToolStripItem tsItem;

            try {
                Session.Init(); // Initialisation de la session
                this.Text = Application.ProductName + " [" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "]";
                // Initialisation le menu Connexion avec la base par défaut
                mnu_Connexion.Text = "Connexion (" + Session.BaseDefaut.Description + ")";
                mnu_Connexion.Tag = Session.BaseDefaut;

                // Création d'un item par base disponible dans le menu contextuel
                for (int i = 0; i < Session.ListeBaseData.Count; i++) {
                    tsItem = mnuContextuelBaseData.Items.Add(Session.ListeBaseData[i].Description);
                    tsItem.Tag = Session.ListeBaseData[i]; // Stocker dans le Tag les infos concernant l'accès à la base
                    tsItem.Click += new System.EventHandler(this.mnuContextuelBaseData_Click); // Gestion du clic
                }

            }

            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); }

        }

        private void mnu_Connexion_Click(object sender, EventArgs e) {

            try {
                if (Session.Connexion() == true) {
                    if (Session.Authentification() == true) {
                        mnu_Connexion.Enabled = false;
                        mnu_Deconnexion.Enabled = true;

                        this.AfficherInterface();

                        // Init des menus

                        // Gestion des droits
                        mnu_Param.Enabled = (Session.Utilisateur.DroitAccess != eDroitAcces.Aucun ? true : false);
                        mnu_DroitUtilisateur.Enabled = (Session.Utilisateur.DroitAccess == eDroitAcces.Gestionnaire ? true : false);

                        toolStripLabel_Session.Visible = true;
                        toolStripLabel_Session.Text = Session.Utilisateur.Utilisateur
                            + " [" + Session.Utilisateur.CodeDomaine + "/" + Session.Utilisateur.DroitAccess + "]"
                            + ", accès précédent le " + Session.Utilisateur.DerniereConnexion.ToString();

                        // Renseigner le dataset Session.ds avec la liste des Statuts autorisés pour ce domaine
                        // pour alimenter les listes déroulantes concernant les différents statuts possibles (Priorité, Avancement, ...)
                        string req = "SELECT idStatut,CategorieStatut,LibelleStatut,IconeStatut,OrdreTriStatut,CouleurTexte,idStatutMere FROM Statut ORDER BY CategorieStatut,OrdreTriStatut";
                        Session.RenseignerDataSet(req, "Statut");


                        ChargerCollectivites();
                        ChargerStatuts();
                        ChargerCouleurs();
                        //ImporterEligibilites();

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
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); }

        }

        /// <summary>Sur clic droit sur le menu "Connexion", afficher le menu contextuel des bases de données accessibles</summary>
        private void mnu_Connexion_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) { // Sur clic droit du menu Connexion
                // Afficher le menu contextuel à l'emplacement de la souris
                mnuContextuelBaseData.Show(this.Location.X + e.X, this.Location.Y + e.Y);
            }
        }

        private void mnu_Deconnexion_Click(object sender, EventArgs e) {
            if (Session.Deconnexion() == true) {
                mnu_Connexion.Enabled = true;
                mnu_Deconnexion.Enabled = false;
                this.CacherInterface();
                Session.ds.Clear();


                toolStripLabel_Session.Visible = false;
                toolStripLabel_Session.Text = "";
            }
        }

        /// <summary>Clic sur le menu contextuel des bases de données accessibles</summary>
        private void mnuContextuelBaseData_Click(object sender, EventArgs e) {
            ToolStripItem tsItem;

            // Sur le choix d'une base de connexion depuis le menu contextuel,
            // affecter les nouvelles infos sur le menu "Connexion" pour pointer sur la base choisie
            // et sur l'objet "Session.BaseDefaut"
            tsItem = (ToolStripItem)sender;
            mnu_Connexion.Text = "Connexion (" + tsItem.Text + ")";
            mnu_Connexion.Tag = tsItem.Tag;
            Session.BaseDefaut = (CBaseData)tsItem.Tag;

            // Lancer la connexion en passant directement par le menu Connexion
            mnu_Connexion_Click(sender, e);
        }

        private void mnu_Session_Click(object sender, EventArgs e) {
            mnu_Session.ShowDropDown();
        }

        private void mnu_DroitUtilisateur_Click(object sender, EventArgs e) {
            frmUtilisateur dlg = new frmUtilisateur();

            try {
                dlg.Tag = Session;
                DialogResult res = dlg.ShowDialog(this);
                dlg.Close();
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString()); }
        }

        private void mnu_Param_Click(object sender, EventArgs e) {
            mnu_Param.ShowDropDown();
        }

        private void mnu_aPropos_Click(object sender, EventArgs e) {
            AboutBox dlg_Apropos = new AboutBox();
            try {
                if (Session.oConn != null)
                    dlg_Apropos.textBoxInfos.Text = "Base ATE55: " + Session.oConn.DataSource + "\\" + Session.oConn.Database;
                else
                    dlg_Apropos.textBoxInfos.Text = "";
                DialogResult res = dlg_Apropos.ShowDialog(this);

                dlg_Apropos.Close();
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString()); }
        }


    }
 }

