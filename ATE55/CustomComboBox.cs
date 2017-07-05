using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomComboBox
{

    public partial class CustomComboBox : System.Windows.Forms.ComboBox
    {
        private SolidBrush _selectionColor = new SolidBrush(Color.FromArgb(178, 180, 191));
        private bool _alignText = false;

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// CustomComboBoxImg to add special items.
        /// <remarks> The property DrawMode must be set to OwnerDrawFixed because we
        /// only need one event : MeasureItem. </remarks>
        /// </summary>
        /// ----------------------------------------------------------------------------
        public CustomComboBox()
        {
            this.InitializeComponent();
        }

        #region Properties

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get the draw mode. This property is yet a constant.
        /// </summary>
        /// ----------------------------------------------------------------------------
        [Category("Behavior")]
        [DefaultValue(DrawMode.OwnerDrawFixed)]
        [Browsable(false)]
        public new DrawMode DrawMode
        {
            get { return DrawMode.OwnerDrawFixed; }
            private set { base.DrawMode = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or set the ImageList.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public ImageList ImageList
        {
            get { return this.imageList; }
            set { this.imageList = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or set the selectionColor.
        /// </summary>
        /// ----------------------------------------------------------------------------
        public Color SelectionColor
        {
            get { return this._selectionColor.Color; }
            set { this._selectionColor.Color = value; }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Get or set the text alignment
        /// </summary>
        /// ----------------------------------------------------------------------------
        public bool AlignText
        {
            get { return this._alignText; }
            set { this._alignText = value; }
        }

        // En essayant en mode propriété , j'ai une erreur avec l'analyseur d'objet
        //public string SelectedId
        //{
        //    get { return ((CustomComboBoxItem)this.SelectedItem).idItem; }
        //    set
        //    {
        //        this.SelectedItem = null;
        //        for (int i = 0; i < this.Items.Count; i++)
        //        {
        //            if (((CustomComboBoxItem)this.Items[i]).idItem == value)
        //            {
        //                this.SelectedItem = this.Items[i];
        //                i = this.Items.Count;
        //            }
        //        }
        //    }
        //}

        #endregion

        public string Get_SelectedId()
        {
            if ((CustomComboBoxItem)this.SelectedItem == null)
                return "";
            else
                return ((CustomComboBoxItem)this.SelectedItem).idItem;
        }

        public bool Set_SelectedId(string id)
        {
            bool bResult=false;
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (((CustomComboBoxItem)this.Items[i]).idItem == id)
                {
                    this.SelectedItem = this.Items[i];
                    i = this.Items.Count;
                    bResult = true;
                }
            }
            return bResult;
        }
        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Draw manually the items.
        /// </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="e"> Not used. </param>
        /// ----------------------------------------------------------------------------
        private void CustomComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index != -1)
            {
                CustomComboBoxItem cbItem = this.Items[e.Index] as CustomComboBoxItem; // Retrieves the item
                Rectangle txtLocation = new Rectangle(e.Bounds.Location, e.Bounds.Size);
                SolidBrush foreColor = new SolidBrush(this.ForeColor);
                Font font = this.Font;
                string displayName = this.Items[e.Index].ToString();
                // Draw the selection if needed
                // Check the documentation for value 785
                if (cbItem == null || (cbItem != null && cbItem.IsEnabled))
                {
                    e.DrawBackground();
                    if ((int)e.State == 785) 
                        e.Graphics.FillRectangle(this._selectionColor, e.Bounds);
                }

                if (cbItem != null) // Test if it's a special item
                {
                    if (cbItem.ForeColor != Color.Empty) foreColor.Color = cbItem.ForeColor;

                    if ((e.State & DrawItemState.Focus) == DrawItemState.Focus)
                        e.Graphics.FillRectangle(this._selectionColor, e.Bounds);
                    else
                    {
                        if ((int)e.State != 785) // No selection, draw the background
                        {
                            SolidBrush backColor = new SolidBrush(this.BackColor);
                            if (cbItem.BackColor != Color.Empty) backColor.Color = cbItem.BackColor;
                            e.Graphics.FillRectangle(backColor, e.Bounds);
                            // Release ressources
                            backColor.Dispose();
                            backColor = null;
                        }
                    }
                    // Set the font
                    if (cbItem.IsBold) font = new Font(font, FontStyle.Bold);
                    if (!cbItem.IsEnabled) font = new Font(font, (FontStyle)((int)font.Style + (int)FontStyle.Italic));
                    if (cbItem.IndexImageList >= 0 && cbItem.IndexImageList < this.imageList.Images.Count)
                    {
                        // Draw the image if needed
                        Image curImg = this.imageList.Images[cbItem.IndexImageList];
                        if (curImg != null)
                        {
                            e.Graphics.DrawImage(curImg, new Point(2, e.Bounds.Top));
                            txtLocation.Location = new Point(e.Bounds.X + curImg.Width + 2, e.Bounds.Y + 1);
                        }
                    }
                }
                // If there is some images to display and the textAlignment is set to true
                if (this.imageList.Images.Count > 0 && this._alignText) txtLocation.Location = new Point(e.Bounds.X + this.imageList.Images[0].Width + 2, e.Bounds.Y + 1);
                // And finally, draw the text
                e.Graphics.DrawString(displayName, font, foreColor, txtLocation);
                // Release ressources
                foreColor.Dispose();
                foreColor = null;
            }
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Select the element only if it is enabled.
        /// </summary>
        /// <param name="sender"> Not used. </param>
        /// <param name="e"> Not used. </param>
        /// ----------------------------------------------------------------------------
        private void CustomComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomComboBoxItem cbi = this.SelectedItem as CustomComboBoxItem; // Retrieves the item
            if (cbi != null && !cbi.IsEnabled)
            {
                int index = -1;
                if (this.Items.Count > 0 && this.SelectedIndex > 0) index = this.SelectedIndex - 1;
                this.SelectedIndex = index;
            }
        }
    }

    /// ----------------------------------------------------------------------------
    /// <summary>Informations sur la CustomComboBoxItem</summary>
    /// ----------------------------------------------------------------------------
    public class CustomComboBoxItem
    {
        private Image _img_a_afficher = null;
        private Color _foreColor = Color.Empty;
        private Color _backColor = Color.Empty;
        private string _Texte_a_Afficher = string.Empty;
        private string _idItem = string.Empty;
        private bool _isBold = false;
        private bool _isEnabled = true;
        private int _indexImageList = -1;
        private object _obj = null;

        /// ----------------------------------------------------------------------------
        /// <summary>Constructor.</summary>
        /// ----------------------------------------------------------------------------
        public CustomComboBoxItem()
        {
        }

        /// ----------------------------------------------------------------------------
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"> id spécifique de l'item affiché</param>
        /// <param name="texte"> Texte de l'item affiché</param>
        /// <param name="foreColor">Couleur du texte</param>
        /// <param name="isBold">True si item en Gras</param>
        /// ----------------------------------------------------------------------------
        public CustomComboBoxItem(string id,string texte, Color foreColor, bool isBold)
        {
            this._idItem = id;
            this._Texte_a_Afficher = texte;
            this._foreColor = foreColor;
            this._isBold = isBold;
        }

        #region Properties

        /// <summary>Obtenir ou Affecter le texte à afficher</summary>
        public string Texte
        {
            get { return this._Texte_a_Afficher; }
            set { this._Texte_a_Afficher = value; }
        }

        /// <summary>Obtenir ou Affecter l'identifiant spécifique de l'item</summary>
        public string idItem
        {
            get { return this._idItem; }
            set { this._idItem = value; }
        }

        /// <summary>Obtenir ou Affecter un objet spécifique à l'item</summary>
        public object obj
        {
            get { return this._obj; }
            set { this._obj = value; }
        }

        /// <summary>Obtenir ou Affficher la couleur du texte</summary>
        public Color ForeColor
        {
            get { return this._foreColor; }
            set { this._foreColor = value; }
        }

        /// <summary>Obtenir ou Affecter la couleur du fond</summary>
        public Color BackColor
        {
            get { return this._backColor; }
            set { this._backColor = value; }
        }

        /// <summary>Obtenir ou Affecter l'image à afficher
        /// <remarks> à utiliser seulement avec le contrôle CustomComboBox. </remarks>
        /// </summary>
        public Image DisplayImage
        {
            get { return this._img_a_afficher; }
            set { this._img_a_afficher = value; }
        }

        /// <summary>Obtenir ou Affecter l'index de ImageList de l'image à afficher
        /// <remarks> à utiliser seulement avec le contrôle CustomComboBox. </remarks>
        /// </summary>
        public int IndexImageList
        {
            get { return this._indexImageList; }
            set { this._indexImageList = value; }
        }

        /// <summary>Obtenir ou Affecter si l'item doit être en gras</summary>
        public bool IsBold
        {
            get { return this._isBold; }
            set { this._isBold = value; }
        }

        /// <summary>Obtenir ou Affecter si l'item doit être inactif</summary>
        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { this._isEnabled = value; }
        }

        #endregion

        /// <summary>Retourne le Texte affiché quand la méthode ToString est appelée</summary>
        public override string ToString()
        {
            return this._Texte_a_Afficher;
        }
    }
}