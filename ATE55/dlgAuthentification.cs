using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.DirectoryServices;

namespace ATE55
{
    public partial class dlgAuthentification : Form
    {
        CSession Session;
        SqlDataReader rdr;
        SqlCommand oCmd;
        DirectoryEntry Ldap;

        /// <summary>Structure d'un Rôle utilisateur</summary>
        public class CRole
        {
            public int idRole = -1;
            public string LibRole = "";

            public CRole(int _idRole, string _LibRole)
            {
                idRole = _idRole;
                LibRole = _LibRole;
            }
            /// <summary>Retourne le Texte affiché quand la méthode ToString est appelée</summary>
            public override string ToString()
            {
                return this.LibRole;
            }
        }

        public dlgAuthentification()
        {
            InitializeComponent();
        }

        private void dlgAuthentification_Load(object sender, EventArgs e)
        {
            try
            {
                Session = (CSession) this.Tag;
                txtUtilisateur.Text = Environment.UserName; // Récupérer le nom de l'utilisateur de la session NT en cours

                lbInfos.Text = "Connexion à " + Session.oConn.DataSource + "\\" + Session.oConn.Database;
                txtPassword.Text = Session.Utilisateur.PasswordUtilisateur;
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
                rdr.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
            }
        }

        private void dlgAuthentification_Shown(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        /// <summary>Valider l'authentification selon le compte de connexion et éventuellement le mot de passe si _SansMotdePasse=False</summary>
        /// <param name="ControleMotdePasse"></param>
        private void ControleConnexion(bool _SansMotdePasse = false)
        {
            try
            {
                // Si le nom de l'utilisateur n'a pas encore été validé
                if (txtUtilisateur.Enabled)
                {
                    this.Cursor = Cursors.WaitCursor;
                    // Si Authentification en contrôlant le mot de passe
                    if (!_SansMotdePasse)
                    { // Authentification LDAP
                        // Se connecter à l'annuaire LDAP avec le Nom et mot de passe de l'utilisateur
                        Ldap = new DirectoryEntry("LDAP://" + Environment.UserDomainName, txtUtilisateur.Text, txtPassword.Text);
                        object connect = Ldap.NativeObject;// Force l'authentfication a avoir lieu, pour forcer une erreur si utilisateur ou mot de passe incorrect
                        Ldap.Close();
                    }
					else
                    {   // Pour passer outre le contrôle du mot de passe, vérifier que l'utilisateur de la session NT a des droits Gestionnaire
                        oCmd = new SqlCommand("SELECT COUNT(*) FROM Utilisateur WHERE Utilisateur='" + Environment.UserName + "' "
                            + "AND UtilisateurInactif <> 'O' AND RoleAdmin = 'O'", Session.oConn);
                        int NbEnr = Convert.ToInt16(oCmd.ExecuteScalar());
                        if (NbEnr == 0)
                            return;
                    }
					
                    // Rechercher si l'utilisateur a été déclaré dans l'application
                    oCmd = new SqlCommand("SELECT * FROM Utilisateur WHERE Utilisateur='" + txtUtilisateur.Text + "'", Session.oConn);
                    
                    rdr = oCmd.ExecuteReader();

                    // Si aucune erreur dans la requête de sélection
                    if (rdr != null)
                    {
                        if (rdr.Read()) // Lire le premier enregistrement suivant
                        {
                            if (rdr["UtilisateurInactif"].ToString() != "O")
                            {
                                Session.Utilisateur.idUtilisateur = Convert.ToInt16(rdr["idUtilisateur"]);
                                Session.Utilisateur.Utilisateur = rdr["Utilisateur"].ToString();
                                Session.Utilisateur.NomUtilisateur = rdr["NomUtilisateur"].ToString();
                                // Pour garder une trace du compte qui a forcé l'authentification
                                // on stocke dans le NomUtilisateur , le compte NT d'ouverture de session si celui-ci est différent du nom de session
                                if (_SansMotdePasse && Session.Utilisateur.Utilisateur.ToUpper() != Environment.UserName.ToUpper())
                                    Session.Utilisateur.Utilisateur = Session.Utilisateur.Utilisateur + "<-" + Environment.UserName; // Récupérer le nom de l'utilisateur de la session NT en cours

                                Session.Utilisateur.PrenomUtilisateur = rdr["PrenomUtilisateur"].ToString();
                                Session.Utilisateur.PasswordUtilisateur = txtPassword.Text;
                                Session.Utilisateur.EmailUtilisateur = rdr["EmailUtilisateur"].ToString();

                                if (rdr["RoleAdmin"].ToString() == "O")
                                { Session.Utilisateur.DroitAccess = eDroitAcces.Gestionnaire; listRole.Items.Add(Session.Utilisateur.DroitAccess); }

                                if (rdr["RoleConsultation"].ToString() == "O")
                                { Session.Utilisateur.DroitAccess = eDroitAcces.Lecteur; listRole.Items.Add(Session.Utilisateur.DroitAccess); }

                                if (rdr["RoleSaisie"].ToString() == "O")
                                { Session.Utilisateur.DroitAccess = eDroitAcces.Redacteur; listRole.Items.Add(Session.Utilisateur.DroitAccess); }

                                if (rdr["NbConnexion"].GetType().Name != "DBNull")
                                    Session.Utilisateur.NbConnexion = Convert.ToInt16(rdr["NbConnexion"]);
                                else Session.Utilisateur.NbConnexion = 0;

                                if (rdr["DerniereConnexion"].GetType().Name != "DBNull")
                                    Session.Utilisateur.DerniereConnexion = Convert.ToDateTime(rdr["DerniereConnexion"]);
                                else Session.Utilisateur.DerniereConnexion = null;

                                rdr.Close();
                            }
                            else
                            {   // Compte Utilisateur INACTIF dans l'application
                                Session.Utilisateur.idUtilisateur = -1;
                            }
                        }
                    }
                    rdr.Close();
                    this.Cursor = Cursors.Default;
                }

                if (Session.Utilisateur.idUtilisateur == 0 || Session.Utilisateur.idUtilisateur == -1)
                {
                    MessageBox.Show("L'utilisateur [" + txtUtilisateur.Text + "] n'est pas déclaré " + (Session.Utilisateur.idUtilisateur == -1 ? "'ACTIF' " : "") + "dans l'application\n\rdonc n'est pas autorisé à utiliser l'application !", "Erreur authentification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUtilisateur.Focus();
                }
                else
                {
                    txtUtilisateur.Enabled = txtPassword.Enabled = false; // Désactiver les champs Nom et mot de passe utilisateur pour éviter de modifier le compte
                    
                        if (!listRole.Visible)
                        {
                            listRole.Visible = lbRole.Visible = true;
                            listRole.SelectedIndex = 0;
                            listRole.Focus();
                        }
                        else
                        {
                            Session.Utilisateur.DroitAccess = (eDroitAcces)listRole.SelectedItem;
                            DialogResult = DialogResult.OK; // Pour fermer la boite de dialogue avec succès
                        }
                    
                }

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
                rdr.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException exc)
            {
                MessageBox.Show("Erreur : " + exc.Message, "Erreur LDAP n°" + exc.ErrorCode + " (" + Environment.UserDomainName + "\\" + txtUtilisateur.Text + ")", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
            }

            catch (System.SystemException exc)
            {   // Erreur d'accès au serveur LDAP
                MessageBox.Show("Erreur : " + exc.Message, "Erreur (" + Environment.UserDomainName + "\\" + txtUtilisateur.Text + ")", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Default;
                ControleConnexion();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
            }
        }

        private void btnConnecter_Click(object sender, EventArgs e)
        {
            ControleConnexion();
        }
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si la touche ENTREE est appuyée
            if (e.KeyChar == (char)Keys.Return)
            {
                ControleConnexion();
            }
        }

        private void listRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Si la touche ENTREE est appuyée
            if (e.KeyChar == (char)Keys.Return && listRole.SelectedIndex>=0)
            {
                ControleConnexion();
            }
        }

        private void listRole_DoubleClick(object sender, EventArgs e)
        {
            ControleConnexion();
        }


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            // Si Ctrl + ALt + Shift + Espace, alors effectuer une Validation d'authentification uniquement sur le compte de l'Utilisateur (SANS tenir compte du mot de passe)
            if (e.Alt & e.Control & e.Shift & e.KeyCode == Keys.Space)
            {
                ControleConnexion(true);
            }
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {

        }






    }
}