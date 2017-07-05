using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ATE55
{
    public partial class frmUtilisateur : Form
    {
        CSession Session; // Session en cours
        SqlCommand oCmd;
        SqlDataReader rdr;

        public frmUtilisateur()
        {
            InitializeComponent();
        }

        private void frmPersonne_Load(object sender, EventArgs e)
        {
            Session = (CSession)this.Tag; // Réaffectation de l'objet Session

            Afficher_ListeUtilisateur(-1);
        }

        /// <summary>Afficher la liste des Utilisateurs dans la grille</summary>
        private void Afficher_ListeUtilisateur(int idUtilisateur)
        {
            int i, Position = 0;

            try
            {
                DataGridView dgv = dgvUtilisateur;
                dgv.Tag = "";
                dgv.Rows.Clear(); // Effacer la grille des imputations

                oCmd = new SqlCommand("SELECT * FROM Utilisateur ORDER BY NomUtilisateur,PrenomUtilisateur", Session.oConn);
                rdr = oCmd.ExecuteReader();

                if (rdr != null)
                {
                    while (rdr.Read()) // Lire l'enregistrement suivant
                    {
                        i = dgv.Rows.Add();
                        dgv.Rows[i].Cells["Utilisateur"].Value = rdr["NomUtilisateur"].ToString() + " " + rdr["PrenomUtilisateur"].ToString()
                                                                + " [" + rdr["Utilisateur"].ToString() + "]";
                        dgv.Rows[i].Cells["Utilisateur"].Tag = rdr["Utilisateur"].ToString();
                        
                        // Renseigner l'image pour un Utilisateur en activité ou non
                        if (rdr["UtilisateurInactif"].ToString() == "O")
                        {
                            ((DataGridViewImageCell)dgv.Rows[i].Cells["Inactif"]).Value = ilNiveauAcces.Images["INACTIF"];
                            dgv.Rows[i].Cells["Inactif"].ToolTipText = "Ce compte est désactivé";
                        }
                        else
                            ((DataGridViewImageCell)dgv.Rows[i].Cells["Inactif"]).Value = ilNiveauAcces.Images["VIDE"];

                        // Renseigner l'image pour un Utilisateur "RoleConsultation"
                        if (rdr["RoleConsultation"].ToString() == "O")
                        {
                            ((DataGridViewImageCell)dgv.Rows[i].Cells["Role_Lecteur"]).Value = ilNiveauAcces.Images["Fonction_Gris"];
                            dgv.Rows[i].Cells["Role_Lecteur"].ToolTipText = "Droit Lecteur";
                        }
                        else
                            ((DataGridViewImageCell)dgv.Rows[i].Cells["Role_Lecteur"]).Value = ilNiveauAcces.Images["VIDE"];


                        // Renseigner l'image pour un Utilisateur "RoleSaisie"
                        if (rdr["RoleSaisie"].ToString() == "O")
                        {
                            ((DataGridViewImageCell)dgv.Rows[i].Cells["Role_Redacteur"]).Value = ilNiveauAcces.Images["Fonction_Orange"];
                            dgv.Rows[i].Cells["Role_Redacteur"].ToolTipText = "Droit Redacteur";
                        }
                        else
                            ((DataGridViewImageCell)dgv.Rows[i].Cells["Role_Redacteur"]).Value = ilNiveauAcces.Images["VIDE"];

                        // Renseigner l'image pour un Utilisateur "RoleAdmin"
                        if (rdr["RoleAdmin"].ToString() == "O")
                        {
                            ((DataGridViewImageCell)dgv.Rows[i].Cells["Role_Gestionnaire"]).Value = ilNiveauAcces.Images["Fonction_Rouge"];
                            dgv.Rows[i].Cells["Role_Gestionnaire"].ToolTipText = "Droit Gestionnaire";
                        }
                        else
                            ((DataGridViewImageCell)dgv.Rows[i].Cells["Role_Gestionnaire"]).Value = ilNiveauAcces.Images["VIDE"];

                        dgv.Rows[i].HeaderCell.Tag = Convert.ToInt32(rdr["idUtilisateur"]); // stocker le n° d'identification de la personne
                        if (idUtilisateur != -1 && idUtilisateur == Convert.ToInt32(rdr["idUtilisateur"])) Position = i;
                    }
                }
                rdr.Close();


                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[Position].Selected = false;
                    dgv.Tag = "ACTIF"; // Pour permettre l'affichage des infos sur l'élément sélectionné
                    dgv.Rows[Position].Selected = true;
                    dgv.FirstDisplayedScrollingRowIndex=Position; //définit l'index de la ligne qui est la première ligne affichée 
                }

                btnEnregistrerUtilisateur.Enabled = btnAnnulerUtilisateur.Enabled = false;
            }

            catch (SqlException exc)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++)
                {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
            }

            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
            }
        }

        /// <summary>Afficher la fiche descriptive d'un utilisateur sélectionné</summary>
        private void Afficher_FicheUtilisateur(int idUtilisateur)
        {
            try
            {
                oCmd = new SqlCommand("SELECT * FROM UTILISATEUR WHERE idUtilisateur=" + idUtilisateur.ToString(), Session.oConn);
                rdr = oCmd.ExecuteReader();

                // Initialiser les champs de la fiche
                txtNomUtilisateur.Text = txtPrenomUtilisateur.Text = txtUtilisateur.Text = "";
                cbInactif.Checked = cbRole_Gestionnaire.Checked = cbRole_Redacteur.Checked = cbRole_Lecteur.Checked = false;
                
                btnSupprimerUtilisateur.Enabled = false;
                lbIdUtilisateur.Text = idUtilisateur.ToString();

                if (rdr != null)
                {
                    if (rdr.Read()) // Lire l'enregistrement
                    {
                        lbDerniereConnexion.Text = "dernière connexion le : " + rdr["DerniereConnexion"].ToString() + " (total : " + rdr["NbConnexion"].ToString() + " fois)";
                        txtNomUtilisateur.Text = rdr["NomUtilisateur"].ToString();
                        txtPrenomUtilisateur.Text = rdr["PrenomUtilisateur"].ToString();
                        txtUtilisateur.Text = rdr["Utilisateur"].ToString();
                        txtEmailUtilisateur.Text = rdr["EmailUtilisateur"].ToString();
                        txtMatricule.Text = rdr["MatriculeAgent"].ToString();
                        cbInactif.Checked = (rdr["UtilisateurInactif"].ToString() == "O" ? true : false);
                        cbRole_Lecteur.Checked = (rdr["RoleConsultation"].ToString() == "O" ? true : false);
                        cbRole_Gestionnaire.Checked = (rdr["RoleAdmin"].ToString() == "O" ? true : false);
                        cbRole_Redacteur.Checked = (rdr["RoleSaisie"].ToString() == "O" ? true : false);
                        
                        btnSupprimerUtilisateur.Enabled = true;
                    }
                }
                rdr.Close();

                btnEnregistrerUtilisateur.Enabled = btnAnnulerUtilisateur.Enabled = false;
                btnEnregistrerUtilisateur.Tag = (int)idUtilisateur;
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
                rdr.Close();
            }
        }

        /// <summary>Changement de sélection d'une ligne dans la liste des Utilisateurs</summary>
        private void dgvUtilisateur_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            int idUtilisateur;

            // SI une ligne a été sélectionnée par clavier ou souris
            // dans la grille de la liste des éléments d'arbitrage
            // dgvPersonne.Tag == "ACTIF" permet de ne passer dans la boucle une seule fois (voir Afficher_ListeElementArbitrage() )
            // sinon à chaque création d'un élément dans le dataGV , RowStateChanged est activé
            if (e.StateChanged == DataGridViewElementStates.Selected && (string)dgvUtilisateur.Tag == "ACTIF")
            {
                if (btnEnregistrerUtilisateur.Enabled == true && btnEnregistrerUtilisateur.Tag != null)
                {
                    Enregistrer_FicheUtilisateur((int)btnEnregistrerUtilisateur.Tag,"");

                    idUtilisateur = (int)e.Row.HeaderCell.Tag;
                    Afficher_ListeUtilisateur(idUtilisateur);
                }
                else
                {
                    idUtilisateur = (int)e.Row.HeaderCell.Tag;
                    Afficher_FicheUtilisateur(idUtilisateur);
                }
            }
        }

        /// <summary>Changement d'un élément au moins sur la fiche de la personne</summary>
        private void FicheUtilisateur_Modifier(object sender, EventArgs e)
        {
            if (!btnEnregistrerUtilisateur.Enabled)
                btnEnregistrerUtilisateur.Enabled = btnAnnulerUtilisateur.Enabled = true;
        }

        

        private void btnAnnulerUtilisateur_Click(object sender, EventArgs e)
        {
            btnEnregistrerUtilisateur.Enabled = btnAnnulerUtilisateur.Enabled = false;
            Afficher_FicheUtilisateur((int)btnEnregistrerUtilisateur.Tag);
        }

        private void btnEnregistrerUtilisateur_Click(object sender, EventArgs e)
        {
            Enregistrer_FicheUtilisateur((int)btnEnregistrerUtilisateur.Tag,"");
            Afficher_ListeUtilisateur((int)btnEnregistrerUtilisateur.Tag);
        }
        /// <summary>Enregistrer ou Créer un Utilisateur</summary>
        /// <param name="idUtilisateur">Si -1, alors Création</param>
        /// <returns>retourne idUtilisateur créée ou modifiée, sinon -1</returns>
        private int Enregistrer_FicheUtilisateur(int idUtilisateur, string _reqSpe)
        {
            string req = "";
            int NbEnr = 0;

            try
            {
                if (idUtilisateur < 0)
                {   // Création de l'enregistrement
                    if (_reqSpe == "")
                        req = "INSERT Utilisateur (Utilisateur,NomUtilisateur) "
                            + "VALUES('NOM.P','(nouvel Utilisateur)')";
                    else
                        req = _reqSpe;
                }
                else
                {   // Modification de l'enregistrement
                    req = "UPDATE Utilisateur SET NomUtilisateur='" + txtNomUtilisateur.Text.Replace("'", "''") +"'"
                        + ",PrenomUtilisateur='" + txtPrenomUtilisateur.Text.Replace("'", "''") + "'"
                        + ",Utilisateur='" + txtUtilisateur.Text.Replace("'", "''") + "'"
                        + ",EmailUtilisateur='" + txtEmailUtilisateur.Text.Replace("'", "''") + "'"
                        + ",MatriculeAgent='" + txtMatricule.Text.Replace("'", "''") + "'"
                        + ",RoleConsultation='" + (cbRole_Lecteur.Checked ? "O" : "N") + "'"
                        + ",RoleSaisie='" + (cbRole_Redacteur.Checked ? "O" : "N") + "'"
                        + ",RoleAdmin='" + (cbRole_Gestionnaire.Checked ? "O" : "N") + "'"
                        + ",UtilisateurInactif='" + (cbInactif.Checked ? "O" : "N") + "'"
                        + " WHERE idUtilisateur=" + idUtilisateur.ToString();
                }

                // Renseigner la liste des éléments d'arbitrage
                oCmd = new SqlCommand(req, Session.oConn);
                NbEnr = oCmd.ExecuteNonQuery();
                rdr.Close();
                if (NbEnr <= 0) // Aucun enregistrement 
                {
                    idUtilisateur = -1;
                }
                else
                {
                    // Si demande de Création, récupérer le n° d'enregistrement créé
                    if (idUtilisateur < 0)
                    {
                        oCmd = new SqlCommand("SELECT max(idUtilisateur) FROM Utilisateur", Session.oConn);
                        idUtilisateur = Convert.ToInt32(oCmd.ExecuteScalar());
                    }
                    btnEnregistrerUtilisateur.Enabled = btnAnnulerUtilisateur.Enabled = false;
                }
                return idUtilisateur;
            }

            catch (SqlException exc)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++)
                {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return NbEnr;
            }

            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
                return NbEnr;
            }
        }

        /// <summary>Enregistrer ou Créer une personne</summary>
        /// <param name="idPersonne">Si -1, alors Création</param>
        /// <returns>retourne idPersonne créée ou modifiée, sinon -1</returns>
        private bool Supprimer_FicheUtilisateur(int idUtilisateur)
        {
            int NbEnr = 0;

            try
            {
                oCmd = new SqlCommand("DELETE FROM Utilisateur WHERE idUtilisateur=" + idUtilisateur.ToString(), Session.oConn);
                if(oCmd.ExecuteNonQuery()>0)
                rdr.Close();
                if (NbEnr <= 0) // Aucun enregistrement 
                    return true; // enregistrement supprimé
                else return false;
            }

            catch (SqlException exc)
            {
                StringBuilder errorMessages = new StringBuilder();
                for (int k = 0; k < exc.Errors.Count; k++)
                {
                    errorMessages.Append("Index #" + k + "\n" + "Message: " + exc.Errors[k].Message + "\n" + "LineNumber: " + exc.Errors[k].LineNumber + "\n" + "Source: " + exc.Errors[k].Source + "\n" + "Procedure: " + exc.Errors[k].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                return false;
            }

            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
                return false;
            }
        }

        private void btnSupprimerPersonne_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-Vous SUPPRIMER cet Utilisateur [" + txtNomUtilisateur.Text + " " + txtPrenomUtilisateur.Text + "] ?", "Suppression ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                btnEnregistrerUtilisateur.Enabled = false; // Désactiver le bouton Enregistrer même si des modifications ont été apportées, ceci pour éviter d'enregistrer avant suppression
                if (Supprimer_FicheUtilisateur((int)btnEnregistrerUtilisateur.Tag))
                {
                    Afficher_ListeUtilisateur(-1);
                }
            }
        }

        private void btnCreerUtilisateur_Click(object sender, EventArgs e)
        {
            frmUserAD dlg = new frmUserAD();
            List<string> ListeCompteDejaSelect = new List<string>();

            try
            {
                dlg.Tag = Session;

                int idUtilisateur = -1;
                string req = "";
                // Parcourir la liste des Comptes déjà déclarés pour les pré-sélectionner dans la lsietd es Comptes AD
                DataGridView dgv = dgvUtilisateur;
                for (int i = 0; i < dgv.RowCount; i++)
                    ListeCompteDejaSelect.Add(dgv.Rows[i].Cells["Utilisateur"].Tag.ToString());

                dlg.ListeCompteDejaSelect = ListeCompteDejaSelect;
                DialogResult res = dlg.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    for (int i = 0; i < dlg.ListeCompteNouvSelect.Count; i++)
                    {
                        string[] result= dlg.ListeCompteNouvSelect[i].Split('|');
                        req = "INSERT Utilisateur (Utilisateur,NomUtilisateur,PrenomUtilisateur,EmailUtilisateur,MatriculeAgent) "
                            + "VALUES('" + result[0] + "','" + result[1] + "','" + result[2] + "','" + result[3] + "','" + result[4] + "')";
                        idUtilisateur = Enregistrer_FicheUtilisateur(-1, req);
                    }
                    Afficher_ListeUtilisateur(idUtilisateur);
                }
                dlg.Close();
            }
            catch (Exception exc) { MessageBox.Show("Erreur : " + exc.ToString()); }
        }

        private void btnImprimerListePersonne_Click(object sender, EventArgs e)
        {
            //frmEdition frm = new frmEdition();


            //frmUtilisateur.ActiveForm.Cursor = Cursors.WaitCursor;
            //frm.reportDoc.FileName = @"\\serveur\OrdrServ\RPT\ListePersonne.rpt";

            //frm.reportDoc.Load(frm.reportDoc.FileName); // Chargement du rapport sans l'afficher pour l'instant
            //frm.Text = "Liste des Personnes";
            ////frm.reportDoc.RecordSelectionFormula = "{EDITION_ORDRESERVICE_V.CODE} = " + Marche.CodeMarcheLIA;
            //frm.reportDoc.SetDatabaseLogon("ORDRSERV", "ordrserv");
            //frmUtilisateur.ActiveForm.Cursor = Cursors.Default;
            //frm.CRViewer.ReportSource = frm.reportDoc;
            //frm.Show();
        }

        private void cbRedacteur_CheckedChanged(object sender, EventArgs e)
        {
            //txtServiceAutorise.Enabled = cbRedacteur.Checked;
            FicheUtilisateur_Modifier(sender, e);
        }


        private void btnExportUtilisateur_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ApplicationXL;
            Microsoft.Office.Interop.Excel._Workbook ClasseurXL;
            Microsoft.Office.Interop.Excel._Worksheet FeuilXL;
            Microsoft.Office.Interop.Excel.Range rg;
            StringBuilder errorMessages = new StringBuilder();
            int i, numlig = 1;
            int NbEnr = 0;

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.Refresh();

                // Démarrer Excel et créer l'objet Application Excel
                ApplicationXL = new Microsoft.Office.Interop.Excel.Application();
                ApplicationXL.Visible = false; // Afficher l'application Excel

                ClasseurXL = (Microsoft.Office.Interop.Excel._Workbook)ApplicationXL.Workbooks.Add(System.Reflection.Missing.Value); // Ouvrir un nouveau classeur
                FeuilXL = (Microsoft.Office.Interop.Excel._Worksheet)ClasseurXL.ActiveSheet; // feuille active

                FeuilXL.Name = "Utilisateurs";
                numlig = 1;

                // Création de la ligne d'entête avec le nom des champs
                FeuilXL.Cells[numlig, 1] = "Compte";
                FeuilXL.Cells[numlig, 2] = "Nom";
                FeuilXL.Cells[numlig, 3] = "Prénom";
                FeuilXL.Cells[numlig, 4] = "Matricule";
                FeuilXL.Cells[numlig, 5] = "Email";
                FeuilXL.Cells[numlig, 6] = "Inactif";
                //FeuilXL.Cells[numlig, 7] = "Organigramme Agent";
                //FeuilXL.Cells[numlig, 8] = "Service autorisé";
                FeuilXL.Cells[numlig, 9] = "Consultation";
                FeuilXL.Cells[numlig,10] = "Rédacteur";
                FeuilXL.Cells[numlig,11] = "Gestionnaire";
                FeuilXL.Cells[numlig,12] = "Dernière connexion";

                FeuilXL.get_Range("A1", "A1").Columns.ColumnWidth = 14; // Largeur cellule
                FeuilXL.get_Range("B1", "B1").Columns.ColumnWidth = 20; // Largeur cellule
                FeuilXL.get_Range("C1", "C1").Columns.ColumnWidth = 15; // Largeur cellule
                FeuilXL.get_Range("D1", "D1").Columns.ColumnWidth = 7; // Largeur cellule
                FeuilXL.get_Range("E1", "E1").Columns.ColumnWidth = 15; // Largeur cellule
                FeuilXL.get_Range("F1", "F1").Columns.ColumnWidth = 7; // Largeur cellule
                FeuilXL.get_Range("G1", "H1").Columns.ColumnWidth = 25; // Largeur cellule
                FeuilXL.get_Range("I1", "K1").Columns.ColumnWidth = 4; // Largeur cellule
                FeuilXL.get_Range("L1", "L1").Columns.ColumnWidth = 25; // Largeur cellule

                // Entête en gris
                rg = FeuilXL.get_Range(FeuilXL.Cells[1, 1], FeuilXL.Cells[1, 12]);
                rg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                rg.Interior.Pattern = Microsoft.Office.Interop.Excel.XlPattern.xlPatternSolid;

                numlig++;

                // Attention : cette vue a été construite de manière à générer l'arborescence en un seul passage
                oCmd = new SqlCommand("SELECT COUNT(*) FROM Utilisateur", Session.oConn);

                NbEnr = Convert.ToInt16(oCmd.ExecuteScalar());
                Session.AfficherMsgAttente(this, "Export vers Excel en cours (" + NbEnr + " enr.)", 0);

                oCmd.CommandText = "SELECT * FROM Utilisateur ORDER BY NomUtilisateur,PrenomUtilisateur";
                rdr = oCmd.ExecuteReader();
                if (rdr != null)
                {
                    while (rdr.Read()) // Lire l'enregistrement suivant
                    {
                        FeuilXL.Cells[numlig, 1] = rdr["Utilisateur"].ToString();
                        FeuilXL.Cells[numlig, 2] = rdr["NomUtilisateur"].ToString();
                        FeuilXL.Cells[numlig, 3] = rdr["PrenomUtilisateur"].ToString();
                        FeuilXL.Cells[numlig, 4] = "'"+ rdr["MatriculeAgent"].ToString();
                        FeuilXL.Cells[numlig, 5] = rdr["EmailUtilisateur"].ToString();
                        if (rdr["UtilisateurInactif"].ToString() == "O")
                        {
                            rg = FeuilXL.get_Range(FeuilXL.Cells[numlig, 1], FeuilXL.Cells[numlig, 1]);
                            rg.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                            FeuilXL.Cells[numlig, 6] = "X";
                        }
                        //FeuilXL.Cells[numlig, 7] = rdr["CodeOrganigramme"].ToString() + '-' + rdr["LibOrganigramme"].ToString();
                        //FeuilXL.Cells[numlig, 8] = "'"+rdr["ServiceAutorise"].ToString();
                        if (rdr["RoleConsultation"].ToString() == "O")
                        {
                            rg = FeuilXL.get_Range(FeuilXL.Cells[numlig, 9], FeuilXL.Cells[numlig, 9]);
                            rg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);
                            rg.Interior.Pattern = Microsoft.Office.Interop.Excel.XlPattern.xlPatternSolid;               
                        }
                        if (rdr["RoleSaisie"].ToString() == "O")
                        {
                            rg = FeuilXL.get_Range(FeuilXL.Cells[numlig, 10], FeuilXL.Cells[numlig,10]);
                            rg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                            rg.Interior.Pattern = Microsoft.Office.Interop.Excel.XlPattern.xlPatternSolid;
                        }
                        if (rdr["RoleAdmin"].ToString() == "O")
                        {
                            rg = FeuilXL.get_Range(FeuilXL.Cells[numlig,11], FeuilXL.Cells[numlig,11]);
                            rg.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                            rg.Interior.Pattern = Microsoft.Office.Interop.Excel.XlPattern.xlPatternSolid;
                        }
                        FeuilXL.Cells[numlig,12] = "le " + rdr["DerniereConnexion"].ToString() + " (total : " + rdr["NbConnexion"].ToString() + " fois)";
                        
                        // Rafraîchir tous les 5 %
                        if ((numlig * 100 / NbEnr) % 5 == 0)
                            Session.AfficherMsgAttente(this, "Export vers Excel en cours (" + NbEnr + " enr.) 1 min.", numlig * 100 / NbEnr);

                        numlig++;
                    }
                }
                rdr.Close();

                //rg.Font.Bold = System.Drawing.FontStyle.Bold;

                Cursor.Current = Cursors.Default;

                Session.FermerMsgAttente();
                ApplicationXL.Visible = true; // Afficher l'application Excel
            }
            catch (SqlException exc)
            {
                for (i = 0; i < exc.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" + "Message: " + exc.Errors[i].Message + "\n" + "LineNumber: " + exc.Errors[i].LineNumber + "\n" + "Source: " + exc.Errors[i].Source + "\n" + "Procedure: " + exc.Errors[i].Procedure + "\n");
                }
                MessageBox.Show(errorMessages.ToString(), exc.TargetSite.ToString() + ": SqlException Err n° " + exc.ErrorCode);
                Session.FermerMsgAttente();
            }

            catch (Exception exc)
            {
                MessageBox.Show("Erreur : " + exc.ToString(), exc.TargetSite.ToString());
                Session.FermerMsgAttente();
            }
        }

        private void txtRechercheUtilisateur_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridView dgv = dgvUtilisateur;

            // Si la touche ENTREE est appuyée
            if (e.KeyChar == (char)Keys.Return)
            {
                // extraire que la partie numérique du texte saisi
                string partie_num = "";
                int i = 0;
                for (i = 0; i < txtRechercheUtilisateur.Text.Length; i++)
                {
                    if (txtRechercheUtilisateur.Text[i] >= '0' && txtRechercheUtilisateur.Text[i] <= '9')
                        partie_num = partie_num + txtRechercheUtilisateur.Text[i];
                }
                dgv.ClearSelection();

                // Recherche début du nom de l'Utilisateur
                txtRechercheUtilisateur.Text = txtRechercheUtilisateur.Text.ToUpper();
                int lg = txtRechercheUtilisateur.Text.Length;
                for (i = 0; i < dgv.RowCount; i++)
                {
                    if (dgv.Rows[i].Cells["Utilisateur"].Value.ToString().Length >= lg)
                    {
                        if (dgv.Rows[i].Cells["Utilisateur"].Value.ToString().Substring(0, lg).ToUpper() == txtRechercheUtilisateur.Text)
                        {
                            if (dgv.Rows[i].Displayed == false)
                                dgv.FirstDisplayedScrollingRowIndex = i; //définit l'index de la ligne qui est la première ligne affichée, rend visible la ligne sélectionnée 

                            dgv.Rows[i].Selected = true; // Sélectionner l'agent trouvé
                            i = dgv.RowCount; // pour sortir de la boucle
                        }
                    }
                }
            }
        }


    }
}
