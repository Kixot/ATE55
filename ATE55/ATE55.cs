using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using Microsoft.Win32;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Collections.Specialized;

namespace ATE55
{
    /// <summary>Enumération des droits d'accés pour l'utilisateur</summary>
    public enum eDroitAcces
    {
        Aucun = 0,
        Lecteur = 1,
        RedacteurPrincipal = 2,
        Gestionnaire = 3,
        Redacteur = 4
    }

    public enum eStatut {
        NonPrecise = 0,
        Projet_Assainissement = 1,
        Projet_EP = 2,
        Projet_MA = 3,
        Projet_DUP = 4,
        Marche_MOE = 5,
        Marche_AMO = 6,
        Marche_Etudes = 7,
        Marche_Travaux = 8,
        Marche_En_Preparation = 9,
        Marche_En_Cours = 10,
        Marche_Termine = 11,
        Marche_Abandonne = 12,
        Projet_En_Reflexion = 13,
        Projet_En_Cours = 14,
        Projet_Termine = 15,
        Projet_Abandonne = 16,
        SPAC = 17,
        SPAC_A = 18,
        SPAC_R = 19,
        SPANC = 20,
        Programme_d_Assainissement = 21,
        Captage_DUP = 22,
        SDAGE = 23,
        DUP_SDAGE = 24,
        Realisation_DUP = 25,
        Suivi_DUP = 26,
        Rivieres_ZH = 27,
        Diagnostic_territorial = 28,
        Audit_Gestion = 29,
        Assainissement = 30,
        DUP = 31,
        TypeSubvention_AEP = 32,
        TypeSubvention_Assainissement = 33,
        TypeSubvention_EAD = 34,
        TypeSubvention_DUP = 35,
        TypeSubvention_Rivieres = 36,
        Complet = 37,
        Pret_Programme = 38,
        Programme = 39,
        Solde = 111,
        ModeGestionStation_Regie = 40,
        ModeGestionStation_DSP = 41,
        Reseau_Separatif = 48,
        Reseau_Unitaire = 50,
        TypeStation_BA = 51,
        TypeStation_FPR = 52,
        TypeStation_LAG = 54,
        TypeStation_LB = 55,
        Parametre_MES = 71,
        Parametre_DCO = 74,
        Parametre_DBO5 = 75,
        Parametre_NNO3 = 76,
        Parametre_NH4 = 77,
        Parametre_Ptotal = 78,
        Parametre_NTK = 79,
        Parametre_NGL = 80,
        Parametre_Volume = 112,
        Mesure_Bilan24h = 81,
        Mesure_PP = 82,
        CAP = 83,
        UD = 84,
        ArreteEnCours = 88,
        ArreteAttribue = 89,
        Captage_AEP = 90,
        Captage_Abandonne = 97,
        MesureNonConforme = 106
    } 

    /// <summary>Structure pour stocker les infos des utilisateurs</summary>
    public class CUtilisateur
    {
        public int idUtilisateur;
        public string Utilisateur;
        public string NomUtilisateur;
        public string PrenomUtilisateur;
        public string PasswordUtilisateur;
        public string EmailUtilisateur;
        public string CodeDomaine;
        
        public eDroitAcces DroitAccess;
        public int NbConnexion = 0;
        public DateTime? DerniereConnexion; // DateTime? ; rendre la variable nullable

        public CUtilisateur()
        {
            Init();
        }

        public void Init()
        {
            idUtilisateur = -1;
            
            Utilisateur = NomUtilisateur = PrenomUtilisateur = PasswordUtilisateur = EmailUtilisateur = CodeDomaine = "";
            DroitAccess = eDroitAcces.Aucun;
            NbConnexion = 0;
            DerniereConnexion=null;
        }

        public override string ToString()
        {
            return Utilisateur + " (" + NomUtilisateur + " " + PrenomUtilisateur + ")";
        }
        
    }

   
    /// <summary>Structure des bases de données potentiellement accessibles</summary>
    public class CBaseData
    {
        public string Description = "";
        public string BaseAccessible = "";
        public string ConnexionSQL = "";

        public CBaseData(string _Description, string _BaseAccessible)
        {
            Description = _Description;
            BaseAccessible = _BaseAccessible;
        }
    }

    /// <summary>
    /// Classe permettant d'initialiser la session liée à un utilisateur
    /// (initialisation des variables ou des listes de références,droits, ...)
    /// </summary>
    public class CSession
    {
        /// <summary>Chaine de connexion à la base de données principale</summary>
        public string ConnexionChaine = "";
        /// <summary>Objet de Connexion à la base de données principale</summary>
        public SqlConnection oConn;
        /// <summary>Cache en mémoire des données de référence de la base de données principale</summary>
        public DataSet ds;
        public string NomApplication = Assembly.GetExecutingAssembly().GetName().Name;
        public CUtilisateur Utilisateur; // Nom de l'utilisateur
        private frmAttente dlgAttente = null; // Fenêtre d'attente pour information
        public List<CBaseData> ListeBaseData = new List<CBaseData>(); // Liste des chaines de connexion sur différentes bases extraits du fichier de configuration
        public CBaseData BaseDefaut = null;   // Base à connecter par défaut  ex: Base="FicheFonction_Prod"
        public string ServeurSMTP = "";
        public string EmailConventions = "";
        public string EmailSubventions = "";
        public StringCollection MaxPotentielFinancier;
        public int MaxPopulationDGF = 0;
        public int Max2PopulationDGF = 0;
        public Dictionary<int, decimal> MaxPotentielFinancierAnnee = new Dictionary<int, decimal>();
        public int DepollutionDBO5 = 0;
        public int DepollutionDCO = 0;
        public int DepollutionAzote = 0;

        public CSession()
        {
            ds = new DataSet(NomApplication); // création du cache mémoire des données
            Utilisateur = new CUtilisateur();
        }

        /// <summary>Initialisation de la session
        /// - Contrôler les paramétres internationaux</summary>
        public bool Init()
        {
            try
            {
                string Base_Defaut = "";
                string[] listeStr;   // Liste des couples de bases extraits du fichier de configuration
                CBaseData BaseData;

                // Contrôler les paramétres internationaux et les modifier si nécessaire
                // ATTENTION : au séparateur de décimales '.' surtout pas ','
                RegistryKey rkcu = Registry.CurrentUser;
                RegistryKey rk = rkcu.OpenSubKey(@"Control Panel\International", true); //Récupère la sous-clé spécifiée
                if (rk != null)
                {
                    if ((string)rk.GetValue("sDecimal") != ".") rk.SetValue("sDecimal", (string)".");
                    if ((string)rk.GetValue("sMonDecimalSep") != ".") rk.SetValue("sMonDecimalSep", (string)".");
                    if ((string)rk.GetValue("sCurrency") != "€") rk.SetValue("sCurrency", (string)"€");
                    if ((string)rk.GetValue("sThousand") != " ") rk.SetValue("sThousand", (string)" ");
                    if ((string)rk.GetValue("sMonThousandSep") != " ") rk.SetValue("sMonThousandSep", (string)" ");
                    rk.Flush();
                    rk.Close();
                }

                // Lecture dans le registre de la dernière base ouverte (Prod, Test ou Locale)
                rk = rkcu.OpenSubKey(@"Software\CG55\" + NomApplication, true); //Récupère la sous-clé spécifiée
                if (rk != null)
                {
                    Base_Defaut = (string)rk.GetValue("Base_" + NomApplication, "");
                    this.Utilisateur.Utilisateur = (string)rk.GetValue("Utilisateur"); // Nom de l'utilisateur
                    rk.Close();
                }

                // Lecture du fichier de App.Config (situé au même endroit que l'exécutable) pour extraire la chaine de connexion à la base de données
                // pour une connexion future
                // ATTENTION: le fichier de App.config ne peut pas faire référence à un lien UNC (\\serveur\alias)

                // Lecture du fichier de Config pour extraire les bases de données potentiellement accessibles
                // et celle à se connecter par défaut
                // Charger dans le fichier de config "NOMAPPLICATION.exe.config" et la section "Section_BaseActive"
                // les chaines de connexion pour la base de données
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AppSettingsSection appSet1 = (AppSettingsSection)config.GetSection("Section_BaseActive");
                ConnectionStringsSection appSet = (ConnectionStringsSection)config.GetSection("connectionStrings");

                for (int i = 1; i <= appSet1.Settings.Count; i++)
                {
                    // ex: key="BDD1" value="Base de Production/NOMAPPLICATION_Prod"
                    // Extraire la clé key = "BDD1" => appSet1.Settings.AllKeys.GetValue(i - 1).ToString()
                    // Extraire les sous-chaînes comprises entre '/'
                    listeStr = appSet1.Settings[appSet1.Settings.AllKeys.GetValue(i - 1).ToString()].Value.Split(new Char[] { '/' });

                    // Ajouter un nouvel élément dans la Liste des bases disponibles
                    BaseData = new CBaseData(listeStr[0], listeStr[1]);

                    // Extraire la chaine de connexion des bases de données
                    BaseData.ConnexionSQL = appSet.ConnectionStrings[BaseData.BaseAccessible].ConnectionString;
                    ListeBaseData.Add(BaseData);

                    // Si la base par défaut n'a pas encore été initialisée,
                    // elle prend la valeur de la 1ere base trouvée dans le fichier de Config
                    if (BaseDefaut == null) BaseDefaut = BaseData;

                    // Si la BaseData en cours de lecture correspond aux infos extraites du registre
                    // alors elle devient BaseDefaut
                    if (BaseData.BaseAccessible == Base_Defaut) BaseDefaut = BaseData;

                    this.ServeurSMTP = ATE55.Properties.Settings.Default.ServeurSMTP; // Récupérer le nom du serveur SMTP dans ATE55.exe.config (voir Propriétés/Paramètres)
                    this.EmailConventions = ATE55.Properties.Settings.Default.EmailConvention; // martine.finet-fort@meuse.fr
                    this.EmailSubventions = ATE55.Properties.Settings.Default.EmailSubvention; // pierre
                    this.MaxPotentielFinancier = ATE55.Properties.Settings.Default.EligibiliteMaxPotentielFinancier;
                    this.MaxPopulationDGF = ATE55.Properties.Settings.Default.EligibiliteMaxPopulation;
                    this.Max2PopulationDGF = ATE55.Properties.Settings.Default.EligibiliteMax2Population;
                    this.DepollutionDBO5 = ATE55.Properties.Settings.Default.DepollutionDBO5;
                    this.DepollutionDCO = ATE55.Properties.Settings.Default.DepollutionDCO;
                    this.DepollutionAzote = ATE55.Properties.Settings.Default.DepollutionAzote;

                }

                foreach (string s in MaxPotentielFinancier) {
                    string[] tab = s.Split('-');
                    int annee = Convert.ToInt32(tab[0]);
                    decimal PF = Convert.ToDecimal(tab[1]);
                    MaxPotentielFinancierAnnee.Add(annee, PF);
                }

                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
                return false;
            }
        }

        /// <summary>Renseigne le dataset ds à partir d'une requête de remplissage</summary>
        /// <param name="_Req">Requête de remplissage</param>
        /// <param name="_NomTable">Nom de table pour l'identifier dans le dataset</param>
        public bool RenseignerDataSet(String _Req, string _NomTable)
        {
            bool bResult = false;

            ds.Tables.Add(_NomTable);

            try
            {   // Se connecter à la base et exécuter la requête
                SqlDataAdapter adapter = new SqlDataAdapter(_Req, this.oConn);
                // Remplir le DataSet pour le Nomde la table spécifiée
                adapter.Fill(ds, _NomTable);
                bResult = true;
            }
            catch (Exception exc)
            {
                // The connection failed. Display an error message.
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
            }
            return bResult;
        }

        /// <summary>Extraire la chaine de connexion à la base de données principale et ouvrir la connexion</summary>
        /// <returns>True : si connexion est bien ouverte</returns>
        public bool Connexion()
        {
            try
            {
                // Lecture du fichier de App.Config (situé au même endroit que l'exécutable) pour extraire la chaine de connexion à la base de données
                // pour une connexion future
                // ATTENTION: le fichier de App.config ne peut pas faire référence à un lien UNC (\\serveur\alias)
                //Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //ConnectionStringsSection appSet = (ConnectionStringsSection)config.GetSection("connectionStrings");

                // Ouvrir une connexion à la base de données principale
                this.ConnexionChaine = this.BaseDefaut.ConnexionSQL;
                this.oConn = new SqlConnection(this.ConnexionChaine);
                this.oConn.Open();
                return true;
            }

            // Gestion des erreurs
            catch (SqlException exc)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++)
                {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                this.oConn.Close();
                return false;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
                return false;
            }
        }

        /// <summary>Fermer la connexion à la base de données principale</summary>
        /// <returns>True : si connexion est bien fermée</returns>
        public bool Deconnexion()
        {
            try
            {
                // Fermer la connexion à la base de données principale
                this.ConnexionChaine = "";
                this.oConn.Close();
                ds.Tables.Clear();
                return true;
            }

            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
                return false;
            }
        }

        /// <summary>Ouvrir la fenêtre d'authentification pour que l'utilisateur s'identifie</summary>
        /// <returns>True : authentification correcte</returns>
        public bool Authentification()
        {
            dlgAuthentification dlg = new dlgAuthentification();

            dlg.Tag = this; // Passer en paramètre l'objet Session au formulaire pour récupérer la connexion à la base

            // Ouvrir le formulaire d'authentification en mode "boite de dialogue"
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                // Ecrire les infos dans le registre
                // Enregistrer le nom du dernier utilisateur connecté et sur quelles bases
                RegistryKey rkcu = Registry.CurrentUser;
                RegistryKey rk = rkcu.CreateSubKey(@"Software\CG55\ATE55");
                rk.SetValue("Utilisateur", this.Utilisateur.Utilisateur);
                rk.SetValue("Base_ATE55", this.BaseDefaut.BaseAccessible);
                rk.Flush(); rk.Close();

                // Enregistrer la dernière connexion de l'utilisateur + Nb de connexion
                this.Utilisateur.NbConnexion++;

                string Req = "UPDATE Utilisateur "
                    + "SET NbConnexion =" + this.Utilisateur.NbConnexion + ","
                    + " DerniereConnexion = '" + DateTime.Now + "'"
                    + " WHERE idUtilisateur=" + this.Utilisateur.idUtilisateur;

                SqlCommand oCmd = new SqlCommand(Req, this.oConn);

                // Si enregistrement enregistré
                if (oCmd.ExecuteNonQuery() > 0)
                    return true;
                else return false;
            }
            else return false;
        }

        /// <summary>Afficher d'un message d'attente</summary>
        /// <param name="f">Formulaire parent</param>
        /// <param name="sMessage">Message à afficher</param>
        /// <param name="iPourcentage">si Pourcentage inférieur à 0, masquer</param>
        public void AfficherMsgAttente(Form f, string sMessage, int iPourcentage)
        {
            if (dlgAttente == null)
                dlgAttente = new frmAttente();

            this.dlgAttente.lbMessage.Text = sMessage;

            // Gérer la barre de pourcentage
            if (iPourcentage < 0)
                this.dlgAttente.progressBar.Visible = false;
            else
            {
                this.dlgAttente.progressBar.Visible = true;
                if (iPourcentage > 100) iPourcentage = 100;
                this.dlgAttente.progressBar.Value = iPourcentage;
            }

            if (!this.dlgAttente.Visible)
                if (f!=null)
                    this.dlgAttente.Show(f);
                else
                    this.dlgAttente.Show();

            this.dlgAttente.Refresh(); // Force l'affichage de la fenêtre
        }

        /// <summary>Fermer le message d'attente et vider le pointeur</summary>
        public void FermerMsgAttente()
        {
            if (this.dlgAttente != null)
            {
                this.dlgAttente.Close();
                this.dlgAttente = null;
            }
        }
    }

    /// <summary>Structure d'un Statut permet de stocker les infos</summary>
    public class CStatut
    {
        public eStatut Statut = eStatut.NonPrecise;

        public int idStatut;
        public string CategorieStatut;
        public string LibelleStatut;
        public string IconeStatut;
        public int OrdreTriStatut;
        public string CouleurTexte;
        public int idStatutMere;

        public CStatut() { }
    }

    public class CProjet {

        public CSession Session;
        public int idProjet;
        public int idStatut_TypeProjet = (int)eStatut.NonPrecise;
        public String CodeCollectivite;
        public String IntituleProjet;
        public int AnneeDemarrageProjet;
        public decimal MontantProjet;
        public String RemarqueProjet;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CProjet() {}

        public CProjet(CSession s) {
            this.Session = s;
        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {
                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE Projet "
                    + "SET idStatut_TypeProjet = " + this.idStatut_TypeProjet + ","
                    + " CodeCollectivite = '" + this.CodeCollectivite + "',"
                    + " IntituleProjet = '" + this.IntituleProjet.Replace("'", "''") + "',"
                    + " AnneeDemarrageProjet = " + this.AnneeDemarrageProjet + ","
                    + " MontantProjet = " + this.MontantProjet + ","
                    + " RemarqueProjet = '" + this.RemarqueProjet.Replace("'", "''") + "',"
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idProjet =" + this.idProjet;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                this.CreeLe = DateTime.Now;
                this.CreePar = Session.Utilisateur.Utilisateur;

                string req = "INSERT Projet (idStatut_TypeProjet, CodeCollectivite, IntituleProjet, AnneeDemarrageProjet, MontantProjet, RemarqueProjet, CreeLe, CreePar)"
                    + "VALUES("
                    + this.idStatut_TypeProjet + ","
                    + "'" + this.CodeCollectivite + "',"
                    + "'" + this.IntituleProjet.Replace("'", "''") + "',"
                    + this.AnneeDemarrageProjet + ","
                    + this.MontantProjet + ","
                    + "'" + this.RemarqueProjet.Replace("'", "''") + "',"
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idProjet = Convert.ToInt32(command.ExecuteScalar());
                if (this.idProjet != 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // SUPPRESSION Projet_Collectivite
                string req = "DELETE FROM Projet_Collectivite WHERE idProjet = " + id;
                command = new SqlCommand(req, session.oConn);
                command.ExecuteNonQuery();

                // SUPPRESSION Projet_EtatAvancement
                req = "DELETE FROM Projet_EtatAvancement WHERE idProjet = " + id;
                command = new SqlCommand(req, session.oConn);
                command.ExecuteNonQuery();

                // SUPPRESSION des marchés
                req = "SELECT idMarche FROM Marche WHERE idProjet = " + id;
                command = new SqlCommand(req, session.oConn);
                SqlDataReader dataReader = command.ExecuteReader();
                List<int> ids = new List<int>();
                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read())
                        ids.Add(Convert.ToInt32(dataReader["idMarche"]));
                }
                dataReader.Close();
                foreach(int idMarche in ids)
                    CMarche.Supprimer(session, idMarche);

                // SUPPRESSION Projet
                req = "DELETE FROM Projet WHERE idProjet = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;



            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }

    }

    public class CMarche {

        public CSession Session;
        public int idMarche;
        public int idStatut_TypeMarche;
        public int idProjet;
        public string NomPrestataireMarche;
        public string IntituleMarche;
        public DateTime DateSignatureMarche;
        public decimal MontantMarche;
        public string RemarqueMarche;
        public int AssistanceSATE;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;


        public CMarche() { }

        public CMarche(CSession s) {
            this.Session = s;
        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {
                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE Marche "
                    + "SET idStatut_TypeMarche = " + this.idStatut_TypeMarche + ","
                    + " idProjet = " + this.idProjet + ","
                    + " NomPrestataireMarche = '" + this.NomPrestataireMarche.Replace("'", "''") + "',"
                    + " IntituleMarche = '" + this.IntituleMarche.Replace("'", "''") + "',"
                    + " DateSignatureMarche = '" + this.DateSignatureMarche.ToString() + "',"
                    + " MontantMarche = " + this.MontantMarche + ","
                    + " RemarqueMarche = '" + this.RemarqueMarche.Replace("'", "''") + "',"
                    + " AssistanceSATE = " + this.AssistanceSATE + ","
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idMarche =" + this.idMarche;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                this.CreeLe = DateTime.Now;
                this.CreePar = Session.Utilisateur.Utilisateur;

                string req = "INSERT Marche (idStatut_TypeMarche, idProjet, NomPrestataireMarche, IntituleMarche, DateSignatureMarche, MontantMarche, RemarqueMarche, AssistanceSATE, CreeLe, CreePar)"
                    + "VALUES("
                    + this.idStatut_TypeMarche + ","
                    + this.idProjet + ","
                    + "'" + this.NomPrestataireMarche.Replace("'", "''") + "',"
                    + "'" + this.IntituleMarche.Replace("'", "''") + "',"
                    + "'" + this.DateSignatureMarche.ToString() + "',"
                    + this.MontantMarche + ","
                    + "'" + this.RemarqueMarche.Replace("'", "''") + "',"
                    + this.AssistanceSATE + ","
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idMarche = Convert.ToInt32(command.ExecuteScalar());
                if (this.idMarche != 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // SUPPRESSION Marche_EtatAvancement
                string req = "DELETE FROM Marche_EtatAvancement WHERE idMarche = " + id;
                command = new SqlCommand(req, session.oConn);
                command.ExecuteNonQuery();

                // SUPPRESSION Marche
                req = "DELETE FROM Marche WHERE idMarche = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }
    }

    public class CProjet_EtatAvancement {

        public CSession Session;
        public int idProjetEtatAvancement;
        public int idProjet;
        public int idStatut_Etat;
        public DateTime CreeLe;
        public string CreePar;

        public CProjet_EtatAvancement() { }

        public CProjet_EtatAvancement(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {
            SqlCommand command;

            try {

                // On récupère le dernier état pour savoir s'il a été modifié
                string req = "SELECT TOP 1 idStatut_Etat FROM Projet_EtatAvancement WHERE idProjet = " + this.idProjet + " ORDER BY CreeLe DESC";
                command = new SqlCommand(req, Session.oConn);

                int idAncienEtat = -1;
                if(command.ExecuteScalar() != null)
                    idAncienEtat = Convert.ToInt32(command.ExecuteScalar());

                // On enregistre le nouvel état seulement s'il est différent de l'ancien
                if (idAncienEtat == -1 || idAncienEtat != this.idStatut_Etat) {

                    this.CreeLe = DateTime.Now;
                    this.CreePar = Session.Utilisateur.Utilisateur;

                    req = "INSERT Projet_EtatAvancement (idProjet, idStatut_Etat, CreeLe, CreePar)"
                        + "VALUES("
                        + this.idProjet + ","
                        + this.idStatut_Etat + ","
                        + "'" + this.CreeLe.ToString() + "',"
                        + "'" + this.CreePar + "');";

                    command = new SqlCommand(req, Session.oConn);
                    if (command.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return true;

                
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CMarche_EtatAvancement {

        public CSession Session;
        public int idMarcheEtatAvancement;
        public int idMarche;
        public int idStatut_Etat;
        public DateTime CreeLe;
        public string CreePar;

        public CMarche_EtatAvancement(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {
            SqlCommand command;

            try {

                // On récupère le dernier état pour savoir s'il a été modifié
                string req = "SELECT TOP 1 idStatut_Etat FROM Marche_EtatAvancement WHERE idMarche = " + this.idMarche + " ORDER BY CreeLe DESC";
                command = new SqlCommand(req, Session.oConn);

                int idAncienEtat = -1;
                if (command.ExecuteScalar() != null)
                    idAncienEtat = Convert.ToInt32(command.ExecuteScalar());

                // On enregistre le nouvel état seulement s'il est différent de l'ancien
                if (idAncienEtat == -1 || idAncienEtat != this.idStatut_Etat) {
                    this.CreeLe = DateTime.Now;
                    this.CreePar = Session.Utilisateur.Utilisateur;

                    req = "INSERT Marche_EtatAvancement (idMarche, idStatut_Etat, CreeLe, CreePar)"
                        + "VALUES("
                        + this.idMarche + ","
                        + this.idStatut_Etat + ","
                        + "'" + this.CreeLe.ToString() + "',"
                        + "'" + this.CreePar + "');";

                    command = new SqlCommand(req, Session.oConn);
                    if (command.ExecuteNonQuery() > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return true;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CCollectivite {

        public String CodeCollectivite;
        public String NomCollectivite;
        public String NatureCollectivite;
        public String Inactif;
        public String AgenceEau;

       // public List<CEligibilite> Eligibilites;

        public CCollectivite() { }

    }

    public class CConvention{

        public CSession Session;
        public int idConvention;
        public int idStatut_TypeConvention;
        public string CodeCollectivite;
        public Nullable<DateTime> DateDebut;
        public Nullable<DateTime> DateFin;
        public Nullable<DateTime> DateDebutPrevision;
        public Nullable<DateTime> DateFinPrevision;
        public string ObservationsConvention;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CConvention() { }

        public CConvention(CSession session) {
            Session = session;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                this.CreeLe = DateTime.Now;
                this.CreePar = Session.Utilisateur.Utilisateur;
                
                string req = "INSERT Convention (idStatut_TypeConvention, CodeCollectivite, DateDebut, DateFin, DateDebutConvention, DateFinConvention, ObservationsConvention, CreeLe, CreePar)"
                    + "VALUES("
                    + this.idStatut_TypeConvention + ","
                    + "'" + this.CodeCollectivite + "',"
                    + (this.DateDebut == null ? "NULL" : "'" + this.DateDebut.ToString() + "'") + ","
                    + (this.DateFin == null ? "NULL" : "'" + this.DateFin.ToString() + "'") + ","
                    + (this.DateDebutPrevision == null ? "NULL" : "'" + this.DateDebutPrevision.ToString() + "'") + ","
                    + (this.DateFinPrevision == null ? "NULL" : "'" + this.DateFinPrevision.ToString() + "'") + ","
                    + "'" + this.ObservationsConvention.Replace("'", "''") + "',"
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idConvention = Convert.ToInt32(command.ExecuteScalar());
                if (this.idConvention != 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {
                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE Convention "
                    + "SET idStatut_TypeConvention = " + this.idStatut_TypeConvention + ","
                    + " CodeCollectivite = '" + this.CodeCollectivite + "',"
                    + " DateDebut = " + (this.DateDebut == null ? "NULL" : "'" + this.DateDebut.ToString() + "'") + ","
                    + " DateFin = " + (this.DateFin == null ? "NULL" : "'" + this.DateFin.ToString() + "'") + ","
                    + " DateDebutConvention = " + (this.DateDebutPrevision == null ? "NULL" : "'" + this.DateDebutPrevision.ToString() + "'") + ","
                    + " DateFinConvention = " + (this.DateFinPrevision == null ? "NULL" : "'" + this.DateFinPrevision.ToString() + "'") + ","
                    + " ObservationsConvention = '" + this.ObservationsConvention.Replace("'", "''") + "',"
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idConvention =" + this.idConvention;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // Suppression collectivités impactées
                string req = "DELETE FROM Convention_Collectivite WHERE idConvention = " + id;
                command = new SqlCommand(req, session.oConn);
                command.ExecuteNonQuery();

                // Suppression lignes convention
                // Récupération des ids
                req = "SELECT idLigneConvention FROM LigneConvention WHERE idConvention = " + id;
                command = new SqlCommand(req, session.oConn);
                SqlDataReader dataReader = command.ExecuteReader();
                List<int> ids = new List<int>();
                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read())
                        ids.Add(Convert.ToInt32(dataReader["idLigneConvention"]));
                }
                dataReader.Close();
                // On boucle sur les ids pour supprimer les lignes
                foreach (int idLigneConvention in ids)
                    CLigneConvention.Supprimer(session, idLigneConvention);

                // Suppression convention
                req = "DELETE FROM Convention WHERE idConvention = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }

    }

    public class CLigneConvention {

        public CSession Session;
        public int idLigneConvention;
        public int idConvention;
        public int AnneeLigneConvention;
        public decimal MontantAnneeConvention;
        public decimal MontantAnnexe2Convention;
        public Nullable<DateTime> DateEnvoiCollectiviteSignatureConvention;
        public Nullable<DateTime> DateRetourConvention;
        public Nullable<DateTime> DateSignatureConvention;
        public Nullable<DateTime> DateEnvoiCollectiviteSigneConvention;
        public Nullable<DateTime> RevisionEnvoiConvention;
        public Nullable<DateTime> RevisionRetourConvention;
        public Nullable<DateTime> DateEnvoiRPQS;
        public Nullable<DateTime> DateRetourRPQS;
        public string MandatRPQS;
        public Nullable<DateTime> EnvoiAnnexe2_Marche;
        public Nullable<DateTime> RetourAnnexe2_Marche;
        public string MandatSPAC;
        public string ObservationsConvention;
        public string NonRecouvreConvention;
        public Nullable<DateTime> DateArreteDUP;

        public CLigneConvention() { }

        public CLigneConvention(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT LigneConvention (idConvention, AnneeLigneConvention, MontantAnneeConvention, MontantAnnexe2Convention, DateEnvoiCollectiviteSignatureConvention, DateRetourConvention, DateSignatureConvention, DateEnvoiCollectiviteSigneConvention, RevisionEnvoiConvention, RevisionRetourConvention, DateEnvoiRPQS, DateRetourRPQS, MandatRPQS, EnvoiAnnexe2_Marche, RetourAnnexe2_Signee, MandatSPAC, ObservationsConvention, NonRecouvreConvention, DateArreteDUP)"
                    + "VALUES("
                    + this.idConvention + ","
                    + this.AnneeLigneConvention + ","
                    + this.MontantAnneeConvention + ","
                    + this.MontantAnnexe2Convention + ","
                    + (this.DateEnvoiCollectiviteSignatureConvention == null ? "NULL" : "'" + this.DateEnvoiCollectiviteSignatureConvention.ToString() + "'") + ","
                    + (this.DateRetourConvention == null ? "NULL" : "'" + this.DateRetourConvention.ToString() + "'") + ","
                    + (this.DateSignatureConvention == null ? "NULL" : "'" + this.DateSignatureConvention.ToString() + "'") + ","
                    + (this.DateEnvoiCollectiviteSigneConvention == null ? "NULL" : "'" + this.DateEnvoiCollectiviteSigneConvention.ToString() + "'") + ","
                    + (this.RevisionEnvoiConvention == null ? "NULL" : "'" + this.RevisionEnvoiConvention.ToString() + "'") + ","
                    + (this.RevisionRetourConvention == null ? "NULL" : "'" + this.RevisionRetourConvention.ToString() + "'") + ","
                    + (this.DateEnvoiRPQS == null ? "NULL" : "'" + this.DateEnvoiRPQS.ToString() + "'") + ","
                    + (this.DateRetourRPQS == null ? "NULL" : "'" + this.DateRetourRPQS.ToString() + "'") + ","
                    + "'" + this.MandatRPQS + "',"
                    + (this.EnvoiAnnexe2_Marche == null ? "NULL" : "'" + this.EnvoiAnnexe2_Marche.ToString() + "'") + ","
                    + (this.RetourAnnexe2_Marche == null ? "NULL" : "'" + this.RetourAnnexe2_Marche.ToString() + "'") + ","
                    + "'" + this.MandatSPAC + "',"
                    + "'" + this.ObservationsConvention.Replace("'", "''") + "',"
                    + "'" + this.NonRecouvreConvention.Replace("'", "''") + "',"
                    + (this.DateArreteDUP == null ? "NULL" : "'" + this.DateArreteDUP.ToString() + "'") + ");"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idLigneConvention = Convert.ToInt32(command.ExecuteScalar());
                if (this.idLigneConvention != 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                string req = "UPDATE LigneConvention "
                    + "SET MontantAnneeConvention = " + this.MontantAnneeConvention + ","
                    + " MontantAnnexe2Convention = " + this.MontantAnnexe2Convention + ","
                    + " DateEnvoiCollectiviteSignatureConvention = " + (this.DateEnvoiCollectiviteSignatureConvention == null ? "NULL" : "'" + this.DateEnvoiCollectiviteSignatureConvention.ToString() + "'") + ","
                    + " DateRetourConvention = " + (this.DateRetourConvention == null ? "NULL" : "'" + this.DateRetourConvention.ToString() + "'") + ","
                    + " DateSignatureConvention = " + (this.DateSignatureConvention == null ? "NULL" : "'" + this.DateSignatureConvention.ToString() + "'") + ","
                    + " DateEnvoiCollectiviteSigneConvention = " + (this.DateEnvoiCollectiviteSigneConvention == null ? "NULL" : "'" + this.DateEnvoiCollectiviteSigneConvention.ToString() + "'") + ","
                    + " RevisionEnvoiConvention = " + (this.RevisionEnvoiConvention == null ? "NULL" : "'" + this.RevisionEnvoiConvention.ToString() + "'") + ","
                    + " RevisionRetourConvention = " + (this.RevisionRetourConvention == null ? "NULL" : "'" + this.RevisionRetourConvention.ToString() + "'") + ","
                    + " DateEnvoiRPQS = " + (this.DateEnvoiRPQS == null ? "NULL" : "'" + this.DateEnvoiRPQS.ToString() + "'") + ","
                    + " DateRetourRPQS = " + (this.DateRetourRPQS == null ? "NULL" : "'" + this.DateRetourRPQS.ToString() + "'") + ","
                    + " MandatRPQS = '" + this.MandatRPQS + "',"
                    + " EnvoiAnnexe2_Marche = " + (this.EnvoiAnnexe2_Marche == null ? "NULL" : "'" + this.EnvoiAnnexe2_Marche.ToString() + "'") + ","
                    + " RetourAnnexe2_Signee = " + (this.RetourAnnexe2_Marche == null ? "NULL" : "'" + this.RetourAnnexe2_Marche.ToString() + "'") + ","
                    + " MandatSPAC = '" + this.MandatSPAC + "',"
                    + " ObservationsConvention = '" + this.ObservationsConvention.Replace("'", "''") + "',"
                    + " NonRecouvreConvention  = '" + this.NonRecouvreConvention.Replace("'", "''") + "',"
                    + " DateArreteDUP = " + (this.DateArreteDUP == null ? "NULL" : "'" + this.DateArreteDUP.ToString() + "'")
                    + " WHERE idLigneConvention = " + this.idLigneConvention;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int idLigneConvention) {

            SqlCommand command;

            try {

                // Suppression titres
                // Récupération des ids des titres
                string req = "SELECT idTitre FROM Titre WHERE idLigneConvention = " + idLigneConvention;
                command = new SqlCommand(req, session.oConn);
                SqlDataReader dataReader = command.ExecuteReader();
                List<int> ids = new List<int>();
                if (dataReader != null && dataReader.HasRows) {
                    while (dataReader.Read())
                        ids.Add(Convert.ToInt32(dataReader["idTitre"]));
                }
                dataReader.Close();
                foreach (int idTitre in ids)
                    CTitre.Supprimer(session, idTitre);

                // Suppression ligne convention
                req = "DELETE FROM LigneConvention WHERE idLigneConvention = " + idLigneConvention;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }
    }

    public class CTitre {

        public CSession Session;
        public int idTitre;
        public int idLigneConvention;
        public decimal MontantTitreConvention;
        public int NumTitreConvention;
        public Nullable<DateTime> DateEmissionTitreConvention;

        public CTitre() { }

        public CTitre(CSession session) {
            this.Session = session;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT Titre (idLigneConvention, NumTitreConvention, MontantTitreConvention, DateEmissionTitreConvention)"
                    + "VALUES("
                    + this.idLigneConvention + ","
                    + this.NumTitreConvention + ","
                    + this.MontantTitreConvention + ","
                    + (this.DateEmissionTitreConvention == null ? "NULL" : "'" + this.DateEmissionTitreConvention.ToString() + "'") + ");"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idTitre = Convert.ToInt32(command.ExecuteScalar());
                if (this.idTitre != 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                string req = "UPDATE Titre "
                    + "SET MontantTitreConvention = " + this.MontantTitreConvention + ","
                    + " NumTitreConvention = " + this.NumTitreConvention + ","
                    + " DateEmissionTitreConvention = " + (this.DateEmissionTitreConvention == null ? "NULL" : "'" + this.DateEmissionTitreConvention.ToString() + "'")
                    + " WHERE idTitre =" + this.idTitre;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int idTitre) {

            SqlCommand command;

            try {

                // Suppression titre
                string req = "DELETE FROM Titre WHERE idTitre = " + idTitre;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }
    }

    public class CProjet_Collectivite {

        public int idProjet;
        public string CodeCollectivite;
        public CSession session;

        public CProjet_Collectivite(CSession s) {
            session = s;
        }

        public Boolean Creer() {
            SqlCommand command;

            try {

                string req = "INSERT Projet_Collectivite (idProjet, CodeCollectivite) "
                    + "VALUES("
                    + this.idProjet + ","
                    + "'"+this.CodeCollectivite + "')";

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public static Boolean Supprimer(CSession Session, int idProjet, string CodeCollectivite) {

            SqlCommand command;

            try {

                string req = "DELETE FROM Projet_Collectivite WHERE idProjet = " + idProjet+" AND CodeCollectivite = "+CodeCollectivite;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }
    }

    public class CConvention_Collectivite {

        public int idConvention;
        public string CodeCollectivite;
        public CSession session;

        public CConvention_Collectivite(CSession s) {
            session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT Convention_Collectivite (idConvention, CodeCollectivite) "
                    + "VALUES("
                    + this.idConvention + ","
                    + "'"+this.CodeCollectivite + "')";

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public static Boolean Supprimer(CSession Session, int idConvention, string CodeCollectivite) {

            SqlCommand command;

            try {

                string req = "DELETE FROM Convention_Collectivite WHERE idConvention = " + idConvention+" AND CodeCollectivite = "+CodeCollectivite;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }
    }

    public class CSubvention_Collectivite {

        public int idSubvention;
        public string CodeCollectivite;
        public CSession session;

        public CSubvention_Collectivite(CSession s) {
            session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT Subvention_Collectivite (idSubvention, CodeCollectivite) "
                    + "VALUES("
                    + this.idSubvention + ","
                    + "'"+this.CodeCollectivite + "')";

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public static Boolean Supprimer(CSession Session, int idSubvention, string CodeCollectivite) {

            SqlCommand command;

            try {

                string req = "DELETE FROM Subvention_Collectivite WHERE idSubvention = " + idSubvention+" AND CodeCollectivite = "+CodeCollectivite;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CStation_Collectivite {

        public CSession Session;
        public int idStation;
        public string CodeCollectivite;

        public CStation_Collectivite(CSession s) {
            Session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT Station_Collectivite (idStation, CodeCollectivite) "
                    + "VALUES("
                    + this.idStation + ","
                    + "'"+this.CodeCollectivite + "')";

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public static Boolean Supprimer(CSession Session, int idStation, string CodeCollectivite) {

            SqlCommand command;

            try {

                string req = "DELETE FROM Station_Collectivite WHERE idStation = " + idStation+" AND CodeCollectivite = "+CodeCollectivite;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CEligibilite {

        CSession Session;
        public int idEligibilite;
        public string CodeCollectivite;
        public decimal PotentielFinancier;
        public int PopulationDGF;
        public int AnneeEligibilite;
        public int CommunesUrbaines;
        public string EPCI_FP;
        public string EauPotable;
        public string AssainissementCollectif;
        public string AssainissementNonCollectif;
        public string MilieuxAquatiques;
        public decimal Surface;
        public int ZoneRouge;

        public bool EligibleAnneeCourante;
        public bool EligibleAnneePrecedente;

        public CEligibilite() { }

        public CEligibilite(CSession session) {
            this.Session = session;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT Eligibilite (CodeCollectivite,PotentielFinancier,PopulationDGF,AnneeEligibilite,CommunesUrbaines,EPCI_FP,EauPotable,AssainissementCollectif,AssainissementNonCollectif,MilieuxAquatiques,Surface,ZoneRouge) "
                    + "VALUES("
                    + "'" + this.CodeCollectivite + "',"
                    + this.PotentielFinancier + ","
                    + this.PopulationDGF + ","
                    + this.AnneeEligibilite + ","
                    + this.CommunesUrbaines + ","
                    + "'" + this.EPCI_FP + "',"
                    + "'" + this.EauPotable + "',"
                    + "'" + this.AssainissementCollectif + "',"
                    + "'" + this.AssainissementNonCollectif + "',"
                    + "'" + this.MilieuxAquatiques + "',"
                    + this.Surface + ","
                    + this.ZoneRouge + ")";

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                string req = "UPDATE Eligibilite "
                    + "SET EauPotable = '" + this.EauPotable.Replace("'", "''") + "',"
                    + " AssainissementCollectif = '" + this.AssainissementCollectif.Replace("'", "''") + "',"
                    + " AssainissementNonCollectif = '" + this.AssainissementNonCollectif.Replace("'", "''") + "',"
                    + " MilieuxAquatiques = '" + this.MilieuxAquatiques.Replace("'", "''") + "'"
                    + " WHERE idEligibilite = " + this.idEligibilite;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Eligible() {
            // Non éligible si :
            //    - PopDGF >= 5000
            // Sinon si :
            //    - PF >= 1005
            // Sinon si :
            //    - PopDGF >= 2000 ET n'appartient pas à un EPCI

            Boolean CommunesUrbaines = this.CommunesUrbaines == 1;

            if (this.PopulationDGF >= Session.MaxPopulationDGF || this.PotentielFinancier >= Session.MaxPotentielFinancierAnnee[this.AnneeEligibilite])
                return false;
            else if (this.PopulationDGF >= Session.Max2PopulationDGF && CommunesUrbaines)
                return false;
            else
                return true;

        }

        public Boolean Eligible(int annee) {

            string req = "SELECT PopulationDGF,PotentielFinancier,CommunesUrbaines FROM Eligibilite WHERE CodeCollectivite = " + this.CodeCollectivite + " AND AnneeEligibilite = " + annee;
            SqlCommand command = new SqlCommand(req, Session.oConn);
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader != null && dataReader.HasRows) {
                dataReader.Read();

                int Population = Convert.ToInt32(dataReader["PopulationDGF"]);
                decimal PF = Convert.ToDecimal(dataReader["PotentielFinancier"]);
                bool Communes = Convert.ToInt32(dataReader["CommunesUrbaines"]) == 1;

                dataReader.Close();

                if (Population >= Session.MaxPopulationDGF || PF >= Session.MaxPotentielFinancierAnnee[annee])
                    return false;
                else if (Population >= Session.Max2PopulationDGF && Communes)
                    return false;
                else
                    return true;

            }
            else {
                dataReader.Close();
                return false;
            }

        }

    }

    public class CSubvention {

        public CSession Session;
        public int idSubvention;
        public string CodeCollectivite;
        public int idStatut_TypeDossier;
        public int idStatut_EtatAvancement;
        public Nullable<DateTime> DateReceptionDemande;
        public string OperationSubvention;
        public Nullable<DateTime> DateAR_DossierComplet;
        public Nullable<DateTime> DateProgrammation;
        public decimal DSHT_Dpt;
        public decimal TauxDpt;
        public decimal SubventionDpt;
        public decimal DSHT_GIP;
        public decimal TauxGIP;
        public decimal SubventionGIP;
        public decimal DSHT_SUR;
        public decimal TauxSUR;
        public decimal SubventionSUR;
        public decimal DSHT_AE;
        public decimal TauxAE;
        public decimal SubventionAE;
        public int PP_Subvention;
        public string CommentairesSubvention;
        public int NbHeuresClausesSociales;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CSubvention() { }

        public CSubvention(CSession session) {
            this.Session = session;
        }

        public Boolean Creer() {

            this.CreeLe = DateTime.Now;
            this.CreePar = Session.Utilisateur.Utilisateur;

            SqlCommand command;

            try {

                string req = "INSERT Subvention (CodeCollectivite,idStatut_TypeDossier,idStatut_EtatAvancement,DateReceptionDemande,OperationSubvention,DateAR_DossierComplet,DateProgrammation,DSHT_Dpt,TauxDpt,SubventionDpt,DSHT_GIP,TauxGIP,SubventionGIP,DSHT_SUR,TauxSUR,SubventionSUR,DSHT_AE,TauxAE,SubventionAE,PP_Subvention,CommentairesSubvention,NbHeuresClausesSociales,CreeLe,CreePar) "
                    + "VALUES("
                    + "'" + this.CodeCollectivite + "',"
                    + idStatut_TypeDossier + ","
                    + idStatut_EtatAvancement + ","
                    + (this.DateReceptionDemande == null ? "NULL" : "'" + this.DateReceptionDemande.ToString() + "'") + ","
                    + "'" + this.OperationSubvention.Replace("'", "''") + "',"
                    + (this.DateAR_DossierComplet == null ? "NULL" : "'" + this.DateAR_DossierComplet.ToString() + "'") + ","
                    + (this.DateProgrammation == null ? "NULL" : "'" + this.DateProgrammation.ToString() + "'") + ","
                    + this.DSHT_Dpt + ","
                    + this.TauxDpt + ","
                    + this.SubventionDpt + ","
                    + this.DSHT_GIP + ","
                    + this.TauxGIP + ","
                    + this.SubventionGIP + ","
                    + this.DSHT_SUR + ","
                    + this.TauxSUR + ","
                    + this.SubventionSUR + ","
                    + this.DSHT_AE + ","
                    + this.TauxAE + ","
                    + this.SubventionAE + ","
                    + this.PP_Subvention + ","
                    + "'" + this.CommentairesSubvention.Replace("'", "''") + "',"
                    + this.NbHeuresClausesSociales + ","
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idSubvention = Convert.ToInt32(command.ExecuteScalar());
                if (this.idSubvention != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {
                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE Subvention "
                    + "SET CodeCollectivite = '" + this.CodeCollectivite + "',"
                    + " idStatut_TypeDossier = " + this.idStatut_TypeDossier + ","
                    + " idStatut_EtatAvancement = " + this.idStatut_EtatAvancement + ","
                    + " DateReceptionDemande = " + (this.DateReceptionDemande == null ? "NULL" : "'" + this.DateReceptionDemande.ToString() + "'") + ","
                    + " OperationSubvention = '" + this.OperationSubvention.Replace("'", "''") + "',"
                    + " DateAR_DossierComplet = " + (this.DateAR_DossierComplet == null ? "NULL" : "'" + this.DateAR_DossierComplet.ToString() + "'") + ","
                    + " DateProgrammation = " + (this.DateProgrammation == null ? "NULL" : "'" + this.DateProgrammation.ToString() + "'") + ","
                    + " DSHT_Dpt = " + this.DSHT_Dpt + ","
                    + " TauxDpt = " + this.TauxDpt + ","
                    + " SubventionDpt = " + this.SubventionDpt + ","
                    + " DSHT_GIP = " + this.DSHT_GIP + ","
                    + " TauxGIP = " + this.TauxGIP + ","
                    + " SubventionGIP = " + this.SubventionGIP + ","
                    + " DSHT_SUR = " + this.DSHT_SUR + ","
                    + " TauxSUR = " + this.TauxSUR + ","
                    + " SubventionSUR = " + this.SubventionSUR + ","
                    + " DSHT_AE = " + this.DSHT_AE + ","
                    + " TauxAE = " + this.TauxAE + ","
                    + " SubventionAE = " + this.SubventionAE + ","
                    + " PP_Subvention = " + this.PP_Subvention + ","
                    + " CommentairesSubvention = '" + this.CommentairesSubvention.Replace("'", "''") + "',"
                    + " NbHeuresClausesSociales = " + this.NbHeuresClausesSociales + ","
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idSubvention = " + this.idSubvention;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // Suppression collectivités impactées
                string req = "DELETE FROM Subvention_Collectivite WHERE idSubvention = " + id;
                command = new SqlCommand(req, session.oConn);
                command.ExecuteNonQuery();

                // Suppression subvention
                req = "DELETE FROM Subvention WHERE idSubvention = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }
    }

    public class CStationAssainissement {

        public CSession Session;
        public int idStation;
        public string CodeSANDRE;
        public string NomStation;
        public int idStatut_TypeFiliereBoue;
        public int idStatut_ModeGestion;
        public int idStatut_TypeStation;
        public int idStatut_SousTypeStation;
        public int idStatut_EtatOuvrages;
        public int idStatut_EtatEntretien;
        public decimal PositionX;
        public decimal PositionY;
        public int Capacite;
        public int CapaciteDBO5;
        public decimal DebitReference;
        public decimal DebitReferenceRecalcule;
        public int AnneeConstruction;
        public string ComplementModeGestion;
        public string CodeCollectiviteLocalisation;
        public string ExutoireStation;
        public string MasseDeau;
        public int SuiviSATE;
        public int ZRV;
        public string Dysfonctionnements;
        public decimal CapaciteOrganiqueRecalculee;
        public int NombreVisites;
        public string Observations;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CStationAssainissement() { }

        public CStationAssainissement(CSession s) { this.Session = s; }
        
        public Boolean Creer() {

            this.CreeLe = DateTime.Now;
            this.CreePar = Session.Utilisateur.Utilisateur;

            SqlCommand command;

            try {

                string req = "INSERT StationAssainissement (idStatut_ModeGestion,idStatut_TypeStation,idStatut_TypeFiliereBoue,idStatut_SousTypeStation,idStatut_EtatOuvrages,idStatut_EtatEntretien,CodeSANDRE,NomStation,PositionX,PositionY,Capacite,CapaciteDBO5,DebitReference,DebitReferenceRecalcule,AnneeConstruction,ComplementModeGestion,CodeCollectiviteLocalisation,ExutoireStation,MasseDeau,SuiviSATE,ZRV,Dysfonctionnements,CapaciteOrganiqueRecalculee,NombreVisites,Observations,CreeLe,CreePar) "
                    + "VALUES("
                    + this.idStatut_ModeGestion + ","
                    + this.idStatut_TypeStation + ","
                    + this.idStatut_TypeFiliereBoue + ","
                    + this.idStatut_SousTypeStation + ","
                    + this.idStatut_EtatOuvrages + ","
                    + this.idStatut_EtatEntretien + ","
                    + "'" + this.CodeSANDRE.Replace("'", "''") + "',"
                    + "'" + this.NomStation.Replace("'", "''") + "',"
                    + this.PositionX + ","
                    + this.PositionY + ","
                    + this.Capacite + ","
                    + this.CapaciteDBO5 + ","
                    + this.DebitReference + ","
                    + this.DebitReferenceRecalcule + ","
                    + this.AnneeConstruction + ","
                    + "'" + this.ComplementModeGestion.Replace("'", "''") + "',"
                    + "'" + this.CodeCollectiviteLocalisation.Replace("'", "''") + "',"
                    + "'" + this.ExutoireStation.Replace("'", "''") + "',"
                    + "'" + this.MasseDeau.Replace("'", "''") + "',"
                    + this.SuiviSATE + ","
                    + this.ZRV + ","
                    + "'" + this.Dysfonctionnements.Replace("'", "''") + "',"
                    + this.CapaciteOrganiqueRecalculee + ","
                    + this.NombreVisites + ","
                    + "'" + this.Observations.Replace("'", "''") + "',"
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idStation = Convert.ToInt32(command.ExecuteScalar());
                if (this.idStation != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE StationAssainissement "
                    + "SET idStatut_ModeGestion = " + this.idStatut_ModeGestion + ","
                    + " idStatut_TypeStation = " + this.idStatut_TypeStation + ","
                    + " idStatut_TypeFiliereBoue = " + this.idStatut_TypeFiliereBoue + ","
                    + " idStatut_SousTypeStation = " + this.idStatut_SousTypeStation + ","
                    + " idStatut_EtatOuvrages = " + this.idStatut_EtatOuvrages + ","
                    + " idStatut_EtatEntretien = " + this.idStatut_EtatEntretien + ","
                    + " CodeSANDRE = '" + this.CodeSANDRE.Replace("'", "''") + "',"
                    + " NomStation = '" + this.NomStation.Replace("'", "''") + "',"
                    + " PositionX = " + this.PositionX + ","
                    + " PositionY = " + this.PositionY + ","
                    + " Capacite = " + this.Capacite + ","
                    + " CapaciteDBO5 = " + this.CapaciteDBO5 + ","
                    + " DebitReference = " + this.DebitReference + ","
                    + " DebitReferenceRecalcule = " + this.DebitReferenceRecalcule + ","
                    + " AnneeConstruction = " + this.AnneeConstruction + ","
                    + " ComplementModeGestion = '" + this.ComplementModeGestion.Replace("'", "''") + "',"
                    + " CodeCollectiviteLocalisation = '" + this.CodeCollectiviteLocalisation.Replace("'", "''") + "',"
                    + " ExutoireStation = '" + this.ExutoireStation.Replace("'", "''") + "',"
                    + " MasseDeau = '" + this.MasseDeau.Replace("'", "''") + "',"
                    + " SuiviSATE = " + this.SuiviSATE + ","
                    + " ZRV = " + this.ZRV + ","
                    + " Dysfonctionnements = '" + this.Dysfonctionnements.Replace("'", "''") + "',"
                    + " CapaciteOrganiqueRecalculee = " + this.CapaciteOrganiqueRecalculee + ","
                    + " NombreVisites = " + this.NombreVisites + ","
                    + " Observations = '" + this.Observations.Replace("'", "''") + "',"
                    + " ModifieLe = '" + this.ModifieLe.ToString().Replace("'", "''") + "',"
                    + " ModifiePar = '" + this.ModifiePar.Replace("'", "''") + "'"
                    + " WHERE idStation=" + this.idStation;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // Suppression mesures
                string req = "DELETE FROM Mesure WHERE idStationMesure = " + id;
                new SqlCommand(req, session.oConn).ExecuteNonQuery();

                // Suppression réseaux
                req = "DELETE FROM Reseau WHERE idStationReseau = " + id;
                new SqlCommand(req, session.oConn).ExecuteNonQuery();                

                // Suppression station
                req = "DELETE FROM StationAssainissement WHERE idStation = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }
    }

    public class CReseau {

        public CSession Session;
        public int idReseau;
        public int idStatut_TypeReseau;
        public int idStationReseau;
        public string CodeCollectiviteCT;
        public int NombreBranchements;
        public int NombreEqH;
        public int NombreDeversoirsOrage;
        public int NombreStationsPompage;
        public decimal Longueur;
        public int MilieuSensible;
        public int NombreRaccordes;
        public int NombreRaccordables;
        public int UsagersND;
        public int UsagersAssimilesDomestiques;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CReseau() { }

        public CReseau(CSession s) { this.Session = s; }

        public Boolean Creer() {

            this.CreeLe = DateTime.Now;
            this.CreePar = Session.Utilisateur.Utilisateur;

            SqlCommand command;

            try {

                string req = "INSERT Reseau (idStatut_TypeReseau,idStationReseau,CodeCollectiviteCT,NombreBranchements,NombreEqH,NombreDeversoirsOrage,NombreStationsPompage,Longueur,MilieuSensible,NombreRaccordes,NombreRaccordables,UsagersND,UsagersAssimilesDomestiques,CreeLe,CreePar) "
                    + "VALUES("
                    + this.idStatut_TypeReseau + ","
                    + this.idStationReseau + ","
                    + "'" + this.CodeCollectiviteCT + "',"
                    + this.NombreBranchements + ","
                    + this.NombreEqH + ","
                    + this.NombreDeversoirsOrage + ","
                    + this.NombreStationsPompage + ","
                    + this.Longueur + ","
                    + this.MilieuSensible + ","
                    + this.NombreRaccordes + ","
                    + this.NombreRaccordables + ","
                    + this.UsagersND + ","
                    + this.UsagersAssimilesDomestiques + ","
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idReseau = Convert.ToInt32(command.ExecuteScalar());
                if (this.idReseau != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE Reseau "
                    + "SET idStatut_TypeReseau = " + this.idStatut_TypeReseau + ","
                    + " idStationReseau = " + this.idStationReseau + ","
                    + " CodeCollectiviteCT = '" + this.CodeCollectiviteCT + "',"
                    + " NombreBranchements = " + this.NombreBranchements + ","
                    + " NombreEqH = " + this.NombreEqH + ","
                    + " NombreDeversoirsOrage = " + this.NombreDeversoirsOrage + ","
                    + " NombreStationsPompage = " + this.NombreStationsPompage + ","
                    + " Longueur = " + this.Longueur + ","
                    + " MilieuSensible = " + this.MilieuSensible + ","
                    + " NombreRaccordes = " + this.NombreRaccordes + ","
                    + " NombreRaccordables = " + this.NombreRaccordables + ","
                    + " UsagersND = " + this.UsagersND + ","
                    + " UsagersAssimilesDomestiques = " + this.UsagersAssimilesDomestiques + ","
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idReseau=" + this.idReseau;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // Suppression station
                string req = "DELETE FROM Reseau WHERE idReseau = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }
        
        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }

    }

    public class CMesure {

        public CSession Session;
        
        public int idMesure;
        public int idStatut_TypeMesure;
        public int idStatut_TypeParametre;
        public int idStatut_EtatConformite;
        public int idStationMesure;
        public decimal MesureConcentrationEntree;
        public decimal MesureConcentrationPointIntermediaire1;
        public decimal MesureConcentrationPointIntermediaire2;
        public decimal MesureConcentrationSortie;
        public decimal MesureFluxEntree;
        public decimal MesureFluxSortie;
        public decimal Rendement;
        public DateTime DateMesure;

        public CMesure() { }

        public CMesure(CSession Session) { this.Session = Session; }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT Mesure (idStatut_TypeMesure,idStatut_TypeParametre,idStatut_EtatConformite,idStationMesure,MesureConcentrationEntree,MesureConcentrationPointIntermediaire1,MesureConcentrationPointIntermediaire2,MesureConcentrationSortie,MesureFluxEntree,MesureFluxSortie,Rendement,DateMesure) "
                    + "VALUES("
                    + this.idStatut_TypeMesure + ","
                    + this.idStatut_TypeParametre + ","
                    + this.idStatut_EtatConformite + ","
                    + this.idStationMesure + ","
                    + this.MesureConcentrationEntree + ","
                    + this.MesureConcentrationPointIntermediaire1 + ","
                    + this.MesureConcentrationPointIntermediaire2 + ","
                    + this.MesureConcentrationSortie + ","
                    + this.MesureFluxEntree + ","
                    + this.MesureFluxSortie + ","
                    + this.Rendement + ","
                    + (this.DateMesure == null ? "NULL" : "'" + this.DateMesure.ToString() + "'") + ");"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idMesure = Convert.ToInt32(command.ExecuteScalar());
                if (this.idMesure != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                string req = "UPDATE Mesure "
                    + "SET idStatut_EtatConformite = " + this.idStatut_EtatConformite + ","
                    + " MesureConcentrationEntree = " + this.MesureConcentrationEntree + ","
                    + " MesureConcentrationSortie = " + this.MesureConcentrationSortie + ","
                    + " MesureConcentrationPointIntermediaire1 = " + this.MesureConcentrationPointIntermediaire1 + ","
                    + " MesureConcentrationPointIntermediaire2 = " + this.MesureConcentrationPointIntermediaire2 + ","
                    + " MesureFluxEntree = " + this.MesureFluxEntree + ","
                    + " MesureFluxSortie = " + this.MesureFluxSortie + ","
                    + " Rendement = " + this.Rendement
                    + " WHERE idMesure=" + this.idMesure;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // Suppression mesure
                string req = "DELETE FROM Mesure WHERE idMesure = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }
    }

    public class CNorme {

        public CSession Session;

        public int idNorme;
        public int idStationNorme;
        public int idStatut_TypeParametre;
        public int AnneValidite;
        public decimal ConcentrationMax;
        public decimal RendementMin;

        public CNorme() { }

        public CNorme(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT Norme (idStationNorme,idStatut_TypeParametre,AnneeValidite,ConcentrationMax,RendementMin) "
                    + "VALUES("
                    + this.idStationNorme + ","
                    + this.idStatut_TypeParametre + ","
                    + this.AnneValidite + ","
                    + this.ConcentrationMax + ","
                    + this.RendementMin + ");"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idNorme = Convert.ToInt32(command.ExecuteScalar());
                if (this.idNorme != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

    }

    public class CRessource {

        // Classe utilisée pour stocker les captages et les UD dans un même objet
        public int idRessource;
        public string CodeCollectivite;
        public string CodeRessource; // BSS pour captage et UDI_CodeNat pour UD
        public string NomRessource;
        public int TypeRessource;

        public CRessource() { }

    }

    public class CCaptage {

        public CSession Session;

        public int idCaptage;
        public string CodeCollectivite;
        public int idStatut_TypeCaptage;
        public int idStatut_EtatArreteDUP;
        public int idStatut_EtatCaptage;
        public int idSuiviCaptage;
        public int idProcedureCaptage;
        public string CodeCollectiviteImplantation;
        public string BSS;
        public string NomRessource;
        public Nullable<DateTime> DateArreteDUP;
        public int DebitAnnuelAutorise;
        public string Observations;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CCaptage() { }

        public CCaptage(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            this.CreeLe = DateTime.Now;
            this.CreePar = Session.Utilisateur.Utilisateur;

            SqlCommand command;

            try {

                string req = "INSERT Captage (CodeCollectivite,idStatut_TypeCaptage,idStatut_EtatArreteDUP,idStatut_EtatCaptage,idSuiviCaptage,idProcedureCaptage,CodeCollectiviteImplantation,NomRessource,BSS,DateArreteDUP,DebitAnnuelAutorise,Observations,CreeLe,CreePar) "
                    + "VALUES("
                    + "'" + this.CodeCollectivite.Replace("'", "''") + "',"
                    + this.idStatut_TypeCaptage + ","
                    + this.idStatut_EtatArreteDUP + ","
                    + this.idStatut_EtatCaptage + ","
                    + this.idSuiviCaptage + ","
                    + this.idProcedureCaptage + ","
                    + "'" + this.CodeCollectiviteImplantation.Replace("'", "''") + "',"
                    + "'" + this.NomRessource.Replace("'", "''") + "',"
                    + "'" + this.BSS + "',"
                    + (this.DateArreteDUP == null ? "NULL" : "'" + this.DateArreteDUP.ToString() + "'") + ","
                    + this.DebitAnnuelAutorise + ","
                    + "'" + this.Observations.Replace("'", "''") + "',"
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idCaptage = Convert.ToInt32(command.ExecuteScalar());
                if (this.idCaptage != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE Captage "
                    + "SET CodeCollectivite = '" + this.CodeCollectivite.Replace("'", "''") + "',"
                    + " idStatut_TypeCaptage = " + this.idStatut_TypeCaptage + ","
                    + " idStatut_EtatArreteDUP = " + this.idStatut_EtatArreteDUP + ","
                    + " idStatut_EtatCaptage = " + this.idStatut_EtatCaptage + ","
                    + " CodeCollectiviteImplantation = '" + this.CodeCollectiviteImplantation.Replace("'", "''") + "',"
                    + " NomRessource = '" + this.NomRessource.Replace("'", "''") + "',"
                    + " BSS = '" + this.BSS + "',"
                    + " DateArreteDUP = " + (this.DateArreteDUP == null ? "NULL" : "'" + this.DateArreteDUP.ToString() + "'") + ","
                    + " DebitAnnuelAutorise = " + this.DebitAnnuelAutorise + ","
                    + " Observations = '" + this.Observations.Replace("'", "''") + "',"
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idCaptage=" + this.idCaptage;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean EnregistrerProcedure(int idCaptage, int idProcedure, CSession Session) {

            SqlCommand command;

            try {

                string req = "UPDATE Captage "
                    + "SET idProcedureCaptage = " + idProcedure + " WHERE idCaptage=" + idCaptage;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean EnregistrerSuivi(int idCaptage, int idSuivi, CSession Session) {

            SqlCommand command;

            try {

                string req = "UPDATE Captage "
                    + "SET idSuiviCaptage = " + idSuivi + " WHERE idCaptage=" + idCaptage;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // Suppression mesure
                string req = "DELETE FROM Captage WHERE idCaptage = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CUD {

        public CSession Session;

        public int idUD;
        public string CodeCollectivite;
        public int idStatut_ModeExploitation;
        public int idStatut_Chloration;
        public int UDI_CodeNat;
        public string NomRessource;
        public int Population;
        public int VolumeProduit;
        public int VolumeImporte;
        public int VolumeExporte;
        public decimal LineaireReseau;
        public decimal LineaireReseauxRenouveles;
        public decimal TauxTVA_Facture;
        public int VoiesNavigables;
        public decimal ProtectionRessourceAE;
        public decimal RedevancePollutionAE;
        public decimal AutresTaxes;
        public int VolumeService;
        public int VolumeConsomme;
        public int VolumesConsommesComptabilises;
        public decimal Rendement;
        public decimal ILP;
        public decimal ILC;
        public decimal PrixEauHT;
        public decimal PrixEauTTC;
        public string AutresTraitements;
        public int NombrePLV;
        public int RestrictionsEv;
        public int DerogationsEv;
        public string AutresEv;
        public decimal C_Bacteriologique;
        public decimal Max_pH;
        public decimal Min_pH;
        public decimal Moy_pH;
        public decimal Max_TitreAlcalimetrique;
        public decimal Min_TitreAlcalimetrique;
        public decimal Moy_TitreAlcalimetrique;
        public decimal Max_TitreHydrometrique;
        public decimal Min_TitreHydrometrique;
        public decimal Moy_TitreHydrometrique;
        public decimal Max_Turbidite;
        public decimal Min_Turbidite;
        public decimal Moy_Turbidite;
        public decimal Max_Nitrates;
        public decimal Min_Nitrates;
        public decimal Moy_Nitrates;
        public decimal Moy_PesticidesTotaux;
        public string Observations;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CUD() { }

        public CUD(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            this.CreeLe = DateTime.Now;
            this.CreePar = Session.Utilisateur.Utilisateur;

            SqlCommand command;

            try {

                string req = "INSERT UD (CodeCollectivite, idStatut_ModeExploitation, idStatut_Chloration, UDI_CodeNat, NomRessource, Population, VolumeProduit," +
                                            "VolumeImporte, VolumeExporte, LineaireReseau, LineaireReseauxRenouveles, TauxTVA_Facture, VoiesNavigables, ProtectionRessourceAE," +
                                            "RedevancePollutionAE, AutresTaxes, VolumeService, VolumeConsomme, VolumesConsommesComptabilises, Rendement, ILP, ILC," +
                                            "PrixEauHT, PrixEauTTC, AutresTraitements, NombrePLV, RestrictionsEv, DerogationsEv, AutresEv, C_Bacteriologique," +
                                            "Max_pH, Min_pH, Moy_pH, Max_TitreAlcalimetrique, Min_TitreAlcalimetrique, Moy_TitreAlcalimetrique," +
                                            "Max_TitreHydrometrique, Min_TitreHydrometrique, Moy_TitreHydrometrique, Max_Turbidite, Min_Turbidite, Moy_Turbidite," +
                                            "Max_Nitrates, Min_Nitrates, Moy_Nitrates, Moy_PesticidesTotaux, Observations, CreeLe, CreePar) "
                    + "VALUES("
                    + "'" + this.CodeCollectivite.Replace("'", "''") + "',"
                    + this.idStatut_ModeExploitation + ","
                    + this.idStatut_Chloration + ","
                    + this.UDI_CodeNat + ","
                    + "'" + this.NomRessource.Replace("'", "''") + "',"
                    + this.Population + ","
                    + this.VolumeProduit + ","
                    + this.VolumeImporte + ","
                    + this.VolumeExporte + ","
                    + this.LineaireReseau + ","
                    + this.LineaireReseauxRenouveles + ","
                    + this.TauxTVA_Facture + ","
                    + this.VoiesNavigables + ","
                    + this.ProtectionRessourceAE + ","
                    + this.RedevancePollutionAE + ","
                    + this.AutresTaxes + ","
                    + this.VolumeService + ","
                    + this.VolumeConsomme + ","
                    + this.VolumesConsommesComptabilises + ","
                    + this.Rendement + ","
                    + this.ILP + ","
                    + this.ILC + ","
                    + this.PrixEauHT + ","
                    + this.PrixEauTTC + ","
                    + "'" + this.AutresTraitements.Replace("'", "''") + "',"
                    + this.NombrePLV + ","
                    + this.RestrictionsEv + ","
                    + this.DerogationsEv + ","
                    + "'" + this.AutresEv.Replace("'", "''") + "',"
                    + this.C_Bacteriologique + ","
                    + this.Max_pH + ","
                    + this.Min_pH + ","
                    + this.Moy_pH + ","
                    + this.Max_TitreAlcalimetrique + ","
                    + this.Min_TitreAlcalimetrique + ","
                    + this.Moy_TitreAlcalimetrique + ","
                    + this.Max_TitreHydrometrique + ","
                    + this.Min_TitreHydrometrique + ","
                    + this.Moy_TitreHydrometrique + ","
                    + this.Max_Turbidite + ","
                    + this.Min_Turbidite + ","
                    + this.Moy_Turbidite + ","
                    + this.Max_Nitrates + ","
                    + this.Min_Nitrates + ","
                    + this.Moy_Nitrates + ","
                    + this.Moy_PesticidesTotaux + ","
                    + "'" + this.Observations + "',"
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idUD = Convert.ToInt32(command.ExecuteScalar());
                if (this.idUD != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE UD "
                    + "SET CodeCollectivite = '" + this.CodeCollectivite.Replace("'", "''") + "',"
                    + " idStatut_ModeExploitation = " + this.idStatut_ModeExploitation + ","
                    + " idStatut_Chloration = " + this.idStatut_Chloration + ","
                    + " UDI_CodeNat = " + this.UDI_CodeNat + ","
                    + " NomRessource = '" + this.NomRessource.Replace("'", "''") + "',"
                    + " Population = " + this.Population + ","
                    + " VolumeProduit = " + this.VolumeProduit + ","
                    + " VolumeImporte = " + this.VolumeImporte + ","
                    + " VolumeExporte = " + this.VolumeExporte + ","
                    + " LineaireReseau = " + this.LineaireReseau + ","
                    + " LineaireReseauxRenouveles = " + this.LineaireReseauxRenouveles + ","
                    + " TauxTVA_Facture = " + this.TauxTVA_Facture + ","
                    + " VoiesNavigables = " + this.VoiesNavigables + ","
                    + " ProtectionRessourceAE = " + this.ProtectionRessourceAE + ","
                    + " RedevancePollutionAE = " + this.RedevancePollutionAE + ","
                    + " AutresTaxes = " + this.AutresTaxes + ","
                    + " VolumeService = " + this.VolumeService + ","
                    + " VolumeConsomme = " + this.VolumeConsomme + ","
                    + " VolumesConsommesComptabilises = " + this.VolumesConsommesComptabilises + ","
                    + " Rendement = " + this.Rendement + ","
                    + " ILP = " + this.ILP + ","
                    + " ILC = " + this.ILC + ","
                    + " PrixEauHT = " + this.PrixEauHT + ","
                    + " PrixEauTTC = " + this.PrixEauTTC + ","
                    + " AutresTraitements = '" + this.AutresTraitements.Replace("'", "''") + "',"
                    + " NombrePLV = " + this.NombrePLV + ","
                    + " RestrictionsEv = " + this.RestrictionsEv + ","
                    + " DerogationsEv = " + this.DerogationsEv + ","
                    + " AutresEv = '" + this.AutresEv.Replace("'", "''") + "',"
                    + " C_Bacteriologique = " + this.C_Bacteriologique + ","
                    + " Max_pH = " + this.Max_pH + ","
                    + " Min_pH = " + this.Min_pH + ","
                    + " Moy_pH = " + this.Moy_pH + ","
                    + " Max_TitreAlcalimetrique = " + this.Max_TitreAlcalimetrique + ","
                    + " Min_TitreAlcalimetrique = " + this.Min_TitreAlcalimetrique + ","
                    + " Moy_TitreAlcalimetrique = " + this.Moy_TitreAlcalimetrique + ","
                    + " Max_TitreHydrometrique = " + this.Max_TitreHydrometrique + ","
                    + " Min_TitreHydrometrique = " + this.Min_TitreHydrometrique + ","
                    + " Moy_TitreHydrometrique = " + this.Moy_TitreHydrometrique + ","
                    + " Max_Turbidite = " + this.Max_Turbidite + ","
                    + " Min_Turbidite = " + this.Min_Turbidite + ","
                    + " Moy_Turbidite = " + this.Moy_Turbidite + ","
                    + " Max_Nitrates = " + this.Max_Nitrates + ","
                    + " Min_Nitrates = " + this.Min_Nitrates + ","
                    + " Moy_Nitrates = " + this.Moy_Nitrates + ","
                    + " Moy_PesticidesTotaux = " + this.Moy_PesticidesTotaux + ","
                    + " Observations = '" + this.Observations.Replace("'", "''") + "',"
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idUD=" + this.idUD;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }

        public static Boolean Supprimer(CSession session, int id) {

            SqlCommand command;

            try {

                // Suppression mesure
                string req = "DELETE FROM UD WHERE idUD = " + id;

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CProcedureDUP {

        public CSession Session;

        public int idProcedure;
        public Nullable<DateTime> DateRencontreCollectivite;
        public Nullable<DateTime> DateDeliberationPhaseTechnique;
        public string SubventionCD_PhaseTechnique;
        public string SubventionAE_PhaseTechnique;
        public Nullable<DateTime> DateConsultationBE;
        public string BE_Retenu;
        public Nullable<DateTime> DateCommandeBE;
        public Nullable<DateTime> DateReceptionEtudePrealable;
        public Nullable<DateTime> DateEnvoiRemarquesEP;
        public Nullable<DateTime> DateEnvoiRemarquesEP_ARS;
        public Nullable<DateTime> DateVersionDefinitive;
        public string NomHA;
        public Nullable<DateTime> DateNomination;
        public Nullable<DateTime> DateReception;
        public int ReceptionNoticeLoiEau;
        public Nullable<DateTime> DateEstimationFrais;
        public int EstimationFraisTransmission;
        public Nullable<DateTime> DateRecevabilite;
        public Nullable<DateTime> DateDeliberationPhaseAdmin;
        public string SubventionCD_PhaseAdmin;
        public string SubventionAE_PhaseAdmin;
        public Nullable<DateTime> DateConsultationServices;
        public Nullable<DateTime> DateReponseCS;
        public Nullable<DateTime> DateReunionPublique;
        public Nullable<DateTime> DateConsultationGeometre;
        public string GeometreRetenu;
        public Nullable<DateTime> DateCommandeGeometre;
        public Nullable<DateTime> DateValidationGeometreARS;
        public Nullable<DateTime> DateDepotPrefecture;
        public Nullable<DateTime> DateArretePrefectoralDebutEP;
        public Nullable<DateTime> DateDesignationCommissaire;
        public Nullable<DateTime> DateDebutEnquete;
        public Nullable<DateTime> DateFinEnquete;
        public Nullable<DateTime> DateRapportCommissaire;
        public Nullable<DateTime> DateCODERST;
        public Nullable<DateTime> DateRAA;
        public decimal CoutTotal;
        public decimal CoutEtudePrealable;
        public decimal CoutHA;
        public decimal CoutGeometre;
        public string Observations;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CProcedureDUP() { }

        public CProcedureDUP(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            this.CreeLe = DateTime.Now;
            this.CreePar = Session.Utilisateur.Utilisateur;

            SqlCommand command;

            try {

                string req = "INSERT ProcedureDUP (DateRencontreCollectivite, DateDeliberationPhaseTechnique, SubventionCD_PhaseTechnique," +
                                                "SubventionAE_PhaseTechnique, DateConsultationBE, BE_Retenu, DateCommandeBE, DateReceptionEtudePrealable," +
                                                "DateEnvoiRemarquesEP, DateEnvoiRemarquesEP_ARS, DateVersionDefinitive, NomHA, DateNomination, DateReception, ReceptionNoticeLoiEau," +
                                                "DateEstimationFrais, EstimationFraisTransmission, DateRecevabilite, DateDeliberationPhaseAdmin," +
                                                "SubventionCD_PhaseAdmin, SubventionAE_PhaseAdmin, DateConsultationServices, DateReponseCS, DateReunionPublique," +
                                                "DateConsultationGeometre, GeometreRetenu, DateCommandeGeometre, DateValidationGeometreARS, DateDepotPrefecture," +
                                                "DateArretePrefectoralDebutEP, DateDesignationCommissaire, DateDebutEnquete, DateFinEnquete, DateRapportCommissaire, DateCODERST, DateRAA," +
                                                "CoutTotal, CoutEtudePrealable, CoutHA, CoutGeometre, Observations, CreeLe, CreePar) "
                    + "VALUES("
                    + (this.DateRencontreCollectivite == null ? "NULL" : "'" + this.DateRencontreCollectivite.ToString() + "'") + ","
                    + (this.DateDeliberationPhaseTechnique == null ? "NULL" : "'" + this.DateDeliberationPhaseTechnique.ToString() + "'") + ","
                    + "'" + this.SubventionCD_PhaseTechnique.Replace("'", "''") + "',"
                    + "'" + this.SubventionAE_PhaseTechnique.Replace("'", "''") + "',"
                    + (this.DateConsultationBE == null ? "NULL" : "'" + this.DateConsultationBE.ToString() + "'") + ","
                    + "'" + this.BE_Retenu.Replace("'", "''") + "',"
                    + (this.DateCommandeBE == null ? "NULL" : "'" + this.DateCommandeBE.ToString() + "'") + ","
                    + (this.DateReceptionEtudePrealable == null ? "NULL" : "'" + this.DateReceptionEtudePrealable.ToString() + "'") + ","
                    + (this.DateEnvoiRemarquesEP == null ? "NULL" : "'" + this.DateEnvoiRemarquesEP.ToString() + "'") + ","
                    + (this.DateEnvoiRemarquesEP_ARS == null ? "NULL" : "'" + this.DateEnvoiRemarquesEP_ARS.ToString() + "'") + ","
                    + (this.DateVersionDefinitive == null ? "NULL" : "'" + this.DateVersionDefinitive.ToString() + "'") + ","
                    + "'" + this.NomHA.Replace("'", "''") + "',"
                    + (this.DateNomination == null ? "NULL" : "'" + this.DateNomination.ToString() + "'") + ","
                    + (this.DateReception == null ? "NULL" : "'" + this.DateReception.ToString() + "'") + ","
                    + this.ReceptionNoticeLoiEau + ","
                    + (this.DateEstimationFrais == null ? "NULL" : "'" + this.DateEstimationFrais.ToString() + "'") + ","
                    + this.EstimationFraisTransmission + ","
                    + (this.DateRecevabilite == null ? "NULL" : "'" + this.DateRecevabilite.ToString() + "'") + ","
                    + (this.DateDeliberationPhaseAdmin == null ? "NULL" : "'" + this.DateDeliberationPhaseAdmin.ToString() + "'") + ","
                    + "'" + this.SubventionCD_PhaseAdmin.Replace("'", "''") + "',"
                    + "'" + this.SubventionAE_PhaseAdmin.Replace("'", "''") + "',"
                    + (this.DateConsultationServices == null ? "NULL" : "'" + this.DateConsultationServices.ToString() + "'") + ","
                    + (this.DateReponseCS == null ? "NULL" : "'" + this.DateReponseCS.ToString() + "'") + ","
                    + (this.DateReunionPublique == null ? "NULL" : "'" + this.DateReunionPublique.ToString() + "'") + ","
                    + (this.DateConsultationGeometre == null ? "NULL" : "'" + this.DateConsultationGeometre.ToString() + "'") + ","
                    + "'" + this.GeometreRetenu.Replace("'", "''") + "',"
                    + (this.DateCommandeGeometre == null ? "NULL" : "'" + this.DateCommandeGeometre.ToString() + "'") + ","
                    + (this.DateValidationGeometreARS == null ? "NULL" : "'" + this.DateValidationGeometreARS.ToString() + "'") + ","
                    + (this.DateDepotPrefecture == null ? "NULL" : "'" + this.DateDepotPrefecture.ToString() + "'") + ","
                    + (this.DateArretePrefectoralDebutEP == null ? "NULL" : "'" + this.DateArretePrefectoralDebutEP.ToString() + "'") + ","
                    + (this.DateDesignationCommissaire == null ? "NULL" : "'" + this.DateDesignationCommissaire.ToString() + "'") + ","
                    + (this.DateDebutEnquete == null ? "NULL" : "'" + this.DateDebutEnquete.ToString() + "'") + ","
                    + (this.DateFinEnquete == null ? "NULL" : "'" + this.DateFinEnquete.ToString() + "'") + ","
                    + (this.DateRapportCommissaire == null ? "NULL" : "'" + this.DateRapportCommissaire.ToString() + "'") + ","
                    + (this.DateCODERST == null ? "NULL" : "'" + this.DateCODERST.ToString() + "'") + ","
                    + (this.DateRAA == null ? "NULL" : "'" + this.DateRAA.ToString() + "'") + ","
                    + this.CoutTotal + ","
                    + this.CoutEtudePrealable + ","
                    + this.CoutHA + ","
                    + this.CoutGeometre + ","
                    + "'" + this.Observations.Replace("'", "''") + "',"
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idProcedure = Convert.ToInt32(command.ExecuteScalar());
                if (this.idProcedure != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE ProcedureDUP "
                    + "SET DateRencontreCollectivite = " + (this.DateRencontreCollectivite == null ? "NULL" : "'" + this.DateRencontreCollectivite.ToString() + "'") + ","
                    + " DateDeliberationPhaseTechnique = " + (this.DateDeliberationPhaseTechnique == null ? "NULL" : "'" + this.DateDeliberationPhaseTechnique.ToString() + "'") + ","
                    + " SubventionCD_PhaseTechnique = '" + this.SubventionCD_PhaseTechnique.Replace("'", "''") + "',"
                    + " SubventionAE_PhaseTechnique = '" + this.SubventionAE_PhaseTechnique.Replace("'", "''") + "',"
                    + " DateConsultationBE = " + (this.DateConsultationBE == null ? "NULL" : "'" + this.DateConsultationBE.ToString() + "'") + ","
                    + " BE_Retenu = '" + this.BE_Retenu.Replace("'", "''") + "',"
                    + " DateCommandeBE = " + (this.DateCommandeBE == null ? "NULL" : "'" + this.DateCommandeBE.ToString() + "'") + ","
                    + " DateReceptionEtudePrealable = " + (this.DateReceptionEtudePrealable == null ? "NULL" : "'" + this.DateReceptionEtudePrealable.ToString() + "'") + ","
                    + " DateEnvoiRemarquesEP = " + (this.DateEnvoiRemarquesEP == null ? "NULL" : "'" + this.DateEnvoiRemarquesEP.ToString() + "'") + ","
                    + " DateEnvoiRemarquesEP_ARS = " + (this.DateEnvoiRemarquesEP_ARS == null ? "NULL" : "'" + this.DateEnvoiRemarquesEP_ARS.ToString() + "'") + ","
                    + " DateVersionDefinitive = " + (this.DateVersionDefinitive == null ? "NULL" : "'" + this.DateVersionDefinitive.ToString() + "'") + ","
                    + " NomHA = '" + this.NomHA.Replace("'", "''") + "',"
                    + " DateNomination = " + (this.DateNomination == null ? "NULL" : "'" + this.DateNomination.ToString() + "'") + ","
                    + " DateReception = " + (this.DateReception == null ? "NULL" : "'" + this.DateReception.ToString() + "'") + ","
                    + " ReceptionNoticeLoiEau = " + this.ReceptionNoticeLoiEau + ","
                    + " DateEstimationFrais = " + (this.DateEstimationFrais == null ? "NULL" : "'" + this.DateEstimationFrais.ToString() + "'") + ","
                    + " EstimationFraisTransmission = " + this.EstimationFraisTransmission + ","
                    + " DateRecevabilite = " + (this.DateRecevabilite == null ? "NULL" : "'" + this.DateRecevabilite.ToString() + "'") + ","
                    + " DateDeliberationPhaseAdmin = " + (this.DateDeliberationPhaseAdmin == null ? "NULL" : "'" + this.DateDeliberationPhaseAdmin.ToString() + "'") + ","
                    + " SubventionCD_PhaseAdmin = '" + this.SubventionCD_PhaseAdmin.Replace("'", "''") + "',"
                    + " SubventionAE_PhaseAdmin = '" + this.SubventionAE_PhaseAdmin.Replace("'", "''") + "',"
                    + " DateConsultationServices = " + (this.DateConsultationServices == null ? "NULL" : "'" + this.DateConsultationServices.ToString() + "'") + ","
                    + " DateReponseCS = " + (this.DateReponseCS == null ? "NULL" : "'" + this.DateReponseCS.ToString() + "'") + ","
                    + " DateReunionPublique = " + (this.DateReunionPublique == null ? "NULL" : "'" + this.DateReunionPublique.ToString() + "'") + ","
                    + " DateConsultationGeometre = " + (this.DateConsultationGeometre == null ? "NULL" : "'" + this.DateConsultationGeometre.ToString() + "'") + ","
                    + " GeometreRetenu = '" + this.GeometreRetenu.Replace("'", "''") + "',"
                    + " DateCommandeGeometre = " + (this.DateCommandeGeometre == null ? "NULL" : "'" + this.DateCommandeGeometre.ToString() + "'") + ","
                    + " DateValidationGeometreARS = " + (this.DateValidationGeometreARS == null ? "NULL" : "'" + this.DateValidationGeometreARS.ToString() + "'") + ","
                    + " DateDepotPrefecture = " + (this.DateDepotPrefecture == null ? "NULL" : "'" + this.DateDepotPrefecture.ToString() + "'") + ","
                    + " DateArretePrefectoralDebutEP = " + (this.DateArretePrefectoralDebutEP == null ? "NULL" : "'" + this.DateArretePrefectoralDebutEP.ToString() + "'") + ","
                    + " DateDesignationCommissaire = " + (this.DateDesignationCommissaire == null ? "NULL" : "'" + this.DateDesignationCommissaire.ToString() + "'") + ","
                    + " DateDebutEnquete = " + (this.DateDebutEnquete == null ? "NULL" : "'" + this.DateDebutEnquete.ToString() + "'") + ","
                    + " DateFinEnquete = " + (this.DateFinEnquete == null ? "NULL" : "'" + this.DateFinEnquete.ToString() + "'") + ","
                    + " DateRapportCommissaire = " + (this.DateRapportCommissaire == null ? "NULL" : "'" + this.DateRapportCommissaire.ToString() + "'") + ","
                    + " DateCODERST = " + (this.DateCODERST == null ? "NULL" : "'" + this.DateCODERST.ToString() + "'") + ","
                    + " DateRAA = " + (this.DateRAA == null ? "NULL" : "'" + this.DateRAA.ToString() + "'") + ","
                    + " CoutTotal = " + this.CoutTotal + ","
                    + " CoutEtudePrealable = " + this.CoutEtudePrealable + ","
                    + " CoutHA = " + this.CoutHA + ","
                    + " CoutGeometre = " + this.CoutGeometre + ","
                    + " Observations = '" + this.Observations.Replace("'", "''") + "',"
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idProcedure=" + this.idProcedure;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }

    }

    public class CProcedureAAC {

        public CSession Session;

        public int idProcedureAAC;
        public int idCaptage;
        public int idStatut_EtatDelimitation;
        public int idStatut_EtatDiagnostiquePressions;
        public int idStatut_EtatProgrammeActions;
        public int idStatut_EtatAnimation;
        public Nullable<DateTime> DateRencontreCollectivite;
        public Nullable<DateTime> DateDeliberationEngagement;
        public Nullable<DateTime> DateCommande;
        public string BE_Retenu;
        public Nullable<DateTime> DateReunionLancement;
        public Nullable<DateTime> DateReceptionEtude;
        public Nullable<DateTime> DateReunionPublique;
        public Nullable<DateTime> DateDeliberationValidation;
        public string DiagnostiquePressions;
        public string ProgrammeActions;
        public string Animation;
        public string Observations;

        public CProcedureAAC() { }

        public CProcedureAAC(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT ProcedureAAC (idCaptage, idStatut_EtatDelimitation, idStatut_EtatDiagnostiquePressions, idStatut_EtatProgrammeActions," +
                                                "idStatut_EtatAnimation, DateRencontreCollectivite, DateDeliberationEngagement, DateCommande, BE_Retenu," +
                                                "DateReunionLancement, DateReceptionEtude, DateReunionPublique, DateDeliberationValidation, DiagnostiquePressions, ProgrammeActions, Animation, Observations) "
                    + "VALUES("
                    + this.idCaptage + ","
                    + this.idStatut_EtatDelimitation + ","
                    + this.idStatut_EtatDiagnostiquePressions + ","
                    + this.idStatut_EtatProgrammeActions + ","
                    + this.idStatut_EtatAnimation + ","
                    + (this.DateRencontreCollectivite == null ? "NULL" : "'" + this.DateRencontreCollectivite.ToString() + "'") + ","
                    + (this.DateDeliberationEngagement == null ? "NULL" : "'" + this.DateDeliberationEngagement.ToString() + "'") + ","
                    + (this.DateCommande == null ? "NULL" : "'" + this.DateCommande.ToString() + "'") + ","
                    + "'" + this.BE_Retenu.Replace("'", "''") + "',"
                    + (this.DateReunionLancement == null ? "NULL" : "'" + this.DateReunionLancement.ToString() + "'") + ","
                    + (this.DateReceptionEtude == null ? "NULL" : "'" + this.DateReceptionEtude.ToString() + "'") + ","
                    + (this.DateReunionPublique == null ? "NULL" : "'" + this.DateReunionPublique.ToString() + "'") + ","
                    + (this.DateDeliberationValidation == null ? "NULL" : "'" + this.DateDeliberationValidation.ToString() + "'") + ","
                    + "'" + this.DiagnostiquePressions.Replace("'", "''") + "',"
                    + "'" + this.ProgrammeActions.Replace("'", "''") + "',"
                    + "'" + this.Animation.Replace("'", "''") + "',"
                    + "'" + this.Observations.Replace("'", "''") + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idProcedureAAC = Convert.ToInt32(command.ExecuteScalar());
                if (this.idProcedureAAC != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                string req = "UPDATE ProcedureAAC "
                    + "SET idStatut_EtatDelimitation = " + this.idStatut_EtatDelimitation + ","
                    + " idStatut_EtatDiagnostiquePressions = " + this.idStatut_EtatDiagnostiquePressions + ","
                    + " idStatut_EtatProgrammeActions = " + this.idStatut_EtatProgrammeActions + ","
                    + " idStatut_EtatAnimation = " + this.idStatut_EtatAnimation + ","
                    + " DateRencontreCollectivite = " + (this.DateRencontreCollectivite == null ? "NULL" : "'" + this.DateRencontreCollectivite.ToString() + "'") + ","
                    + " DateDeliberationEngagement = " + (this.DateDeliberationEngagement == null ? "NULL" : "'" + this.DateDeliberationEngagement.ToString() + "'") + ","
                    + " DateCommande = " + (this.DateCommande == null ? "NULL" : "'" + this.DateCommande.ToString() + "'") + ","
                    + " BE_Retenu = '" + this.BE_Retenu.Replace("'", "''") + "',"
                    + " DateReunionLancement = " + (this.DateReunionLancement == null ? "NULL" : "'" + this.DateReunionLancement.ToString() + "'") + ","
                    + " DateReceptionEtude = " + (this.DateReceptionEtude == null ? "NULL" : "'" + this.DateReceptionEtude.ToString() + "'") + ","
                    + " DateReunionPublique = " + (this.DateReunionPublique == null ? "NULL" : "'" + this.DateReunionPublique.ToString() + "'") + ","
                    + " DateDeliberationValidation = " + (this.DateDeliberationValidation == null ? "NULL" : "'" + this.DateDeliberationValidation.ToString() + "'") + ","
                    + " DiagnostiquePressions = '" + this.DiagnostiquePressions.Replace("'", "''") + "',"
                    + " ProgrammeActions = '" + this.ProgrammeActions.Replace("'", "''") + "',"
                    + " Animation = '" + this.Animation.Replace("'", "''") + "',"
                    + " Observations = '" + this.Observations.Replace("'", "''") + "'"
                    + " WHERE idProcedureAAC=" + this.idProcedureAAC;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }


    }

    public class CSuivi {

        public CSession Session;

        public int idSuivi;
        public Nullable<DateTime> DateVisite1;
        public Nullable<DateTime> DateEnvoiRapport1;
        public Nullable<DateTime> DateVisite2;
        public Nullable<DateTime> DateEnvoiRapport2;
        public Nullable<DateTime> DateVisite3;
        public Nullable<DateTime> DateEnvoiRapport3;
        public string Observations;
        public DateTime CreeLe;
        public String CreePar;
        public DateTime ModifieLe;
        public String ModifiePar;

        public CSuivi() { }

        public CSuivi(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            this.CreeLe = DateTime.Now;
            this.CreePar = Session.Utilisateur.Utilisateur;

            SqlCommand command;

            try {

                string req = "INSERT Suivi (DateVisite1, DateEnvoiRapport1, DateVisite2, DateEnvoiRapport2, DateVisite3, DateEnvoiRapport3, Observations, CreeLe, CreePar) "
                    + "VALUES("
                    + (this.DateVisite1 == null ? "NULL" : "'" + this.DateVisite1.ToString() + "'") + ","
                    + (this.DateEnvoiRapport1 == null ? "NULL" : "'" + this.DateEnvoiRapport1.ToString() + "'") + ","
                    + (this.DateVisite2 == null ? "NULL" : "'" + this.DateVisite2.ToString() + "'") + ","
                    + (this.DateEnvoiRapport2 == null ? "NULL" : "'" + this.DateEnvoiRapport2.ToString() + "'") + ","
                    + (this.DateVisite3 == null ? "NULL" : "'" + this.DateVisite3.ToString() + "'") + ","
                    + (this.DateEnvoiRapport3 == null ? "NULL" : "'" + this.DateEnvoiRapport3.ToString() + "'") + ","
                    + "'" + this.Observations.Replace("'", "''") + "',"
                    + "'" + this.CreeLe.ToString() + "',"
                    + "'" + this.CreePar + "');"
                    + "SELECT CAST(@@IDENTITY AS int)";

                command = new SqlCommand(req, Session.oConn);
                this.idSuivi = Convert.ToInt32(command.ExecuteScalar());
                if (this.idSuivi != 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                this.ModifieLe = DateTime.Now;
                this.ModifiePar = Session.Utilisateur.Utilisateur;

                string req = "UPDATE Suivi "
                    + "SET DateVisite1 = " + (this.DateVisite1 == null ? "NULL" : "'" + this.DateVisite1.ToString() + "'") + ","
                    + " DateEnvoiRapport1 = " + (this.DateEnvoiRapport1 == null ? "NULL" : "'" + this.DateEnvoiRapport1.ToString() + "'") + ","
                    + " DateVisite2 = " + (this.DateVisite2 == null ? "NULL" : "'" + this.DateVisite2.ToString() + "'") + ","
                    + " DateEnvoiRapport2 = " + (this.DateEnvoiRapport2 == null ? "NULL" : "'" + this.DateEnvoiRapport2.ToString() + "'") + ","
                    + " DateVisite3 = " + (this.DateVisite3 == null ? "NULL" : "'" + this.DateVisite3.ToString() + "'") + ","
                    + " DateEnvoiRapport3 = " + (this.DateEnvoiRapport3 == null ? "NULL" : "'" + this.DateEnvoiRapport3.ToString() + "'") + ","
                    + " Observations = '" + this.Observations.Replace("'", "''") + "',"
                    + " ModifieLe = '" + this.ModifieLe.ToString() + "',"
                    + " ModifiePar = '" + this.ModifiePar + "'"
                    + " WHERE idSuivi=" + this.idSuivi;

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public string InfosModif() {
            return ("créé le " + CreeLe + " par " + CreePar + "\n" + "modifié le " + ModifieLe + " par " + ModifiePar);
        }

    }

    public class CPopulationCaptage {

        public CSession Session;

        public int idCaptage;
        public string CodeCollectivite;
        public decimal PourcentagePopulation;

        public CPopulationCaptage() { }

        public CPopulationCaptage(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT PopulationCaptage (idCaptage,CodeCollectivite,PourcentagePopulation) "
                    + "VALUES("
                    + this.idCaptage + ","
                    + "'" + this.CodeCollectivite + "',"
                    + this.PourcentagePopulation + ");";

                command = new SqlCommand(req, Session.oConn);

                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public Boolean Enregistrer() {

            SqlCommand command;

            try {

                string req = "UPDATE PopulationCaptage "
                    + "SET PourcentagePopulation = " + this.PourcentagePopulation+ " WHERE idCaptage =" + this.idCaptage + " AND CodeCollectivite = '"+this.CodeCollectivite+"'";

                command = new SqlCommand(req, Session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int id, string CodeCollectivite) {

            SqlCommand command;

            try {

                // Suppression mesure
                string req = "DELETE FROM PopulationCaptage WHERE idCaptage = " + id+" AND CodeCollectivite = '"+CodeCollectivite+"'";

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CCommunesDesserviesUD {

        public CSession Session;

        public int idUD;
        public string CodeCollectivite;

        public CCommunesDesserviesUD() { }

        public CCommunesDesserviesUD(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT CommunesDesserviesUD (idUD,CodeCollectivite) "
                    + "VALUES("
                    + this.idUD + ","
                    + "'" + this.CodeCollectivite + "');";

                command = new SqlCommand(req, Session.oConn);

                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, int id, string CodeCollectivite) {

            SqlCommand command;

            try {

                // Suppression mesure
                string req = "DELETE FROM CommunesDesserviesUD WHERE idUD = " + id + " AND CodeCollectivite = '" + CodeCollectivite + "'";

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CVenteEau {

        public CSession Session;

        public string CodeCollectiviteAcheteur;
        public string CodeCollectiviteVendeur;

        public CVenteEau(){}

        public CVenteEau(CSession s) {
            this.Session = s;
        }

        public Boolean Creer() {

            SqlCommand command;

            try {

                string req = "INSERT VenteEau (CodeCollectiviteAcheteur,CodeCollectiviteVendeur) "
                    + "VALUES("
                    + "'" + this.CodeCollectiviteAcheteur + "',"
                    + "'" + this.CodeCollectiviteVendeur + "');";

                command = new SqlCommand(req, Session.oConn);

                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }

        }

        public static Boolean Supprimer(CSession session, string CodeCollectiviteAcheteur, string CodeCollectiviteVendeur) {

            SqlCommand command;

            try {

                // Suppression mesure
                string req = "DELETE FROM VenteEau WHERE CodeCollectiviteAcheteur = '" + CodeCollectiviteAcheteur + "' AND CodeCollectiviteVendeur = '" + CodeCollectiviteVendeur + "'";

                command = new SqlCommand(req, session.oConn);
                if (command.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;

            }
            catch (SqlException exc) {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++) {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString()); return false; }
        }

    }

    public class CMiseEnForme {

        public static string SeparationMilliersInt(int Nb) {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            return Nb.ToString("#,0", nfi);
        }

        public static string SeparationMilliersDecimal(decimal Nb) {
            var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            nfi.NumberGroupSeparator = " ";
            return decimal.Parse(Nb.ToString(), CultureInfo.InvariantCulture).ToString("N", new CultureInfo("fr-FR"));
        }

    }
}
