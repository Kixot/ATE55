using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TexteMasqueSaisi
{
    public enum TypeMasque { Aucun, DateUniquement, TelephoneAvecIndicatif, AdresseIP, SSN, Decimal, Entier };
    [ToolboxBitmap("app.bmp")]
    public partial class TexteMasqueSaisi : System.Windows.Forms.TextBox
    {
        private TypeMasque m_type_masque = TypeMasque.Aucun;
        
        // Cette propriété apparaît dans les propriétés de l'objet lors de la conception
        public TypeMasque Type_Masque   
        {
            get { return m_type_masque; }
            set
            {
                m_type_masque = value;
                this.Text = "";
            }
        }

        private int digitPos = 0;
        private int DelimitNumber = 0;
        private int CountDot = 0;

        public TexteMasqueSaisi()
        {
            InitializeComponent();
        }

        public TexteMasqueSaisi(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>Sur touche appuyée</summary>
        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            TexteMasqueSaisi sd = (TexteMasqueSaisi)sender;
            switch (m_type_masque)
            {
                case TypeMasque.DateUniquement:
                    sd.MaskDate(e);
                    break;
                case TypeMasque.TelephoneAvecIndicatif:
                    sd.MaskPhoneSSN(e, 3, 3);
                    break;
                case TypeMasque.AdresseIP:
                    sd.MaskIpAddr(e);
                    break;
                case TypeMasque.SSN:
                    sd.MaskPhoneSSN(e, 3, 2);
                    break;
                case TypeMasque.Decimal:
                    sd.Masque_Decimal(e);
                    break;
                case TypeMasque.Entier:
                    sd.MaskDigit(e);
                    break;
            }
        }
        private void OnLeave(object sender, EventArgs e)
		{
            TexteMasqueSaisi sd = (TexteMasqueSaisi)sender;
			Regex regStr;
            errTexteMasqueSaisi.SetError(this, "");

			switch(m_type_masque)
			{
				case TypeMasque.DateUniquement:
					regStr = new Regex(@"\d{2}/\d{2}/\d{4}");
					if(!regStr.IsMatch(sd.Text))
						errTexteMasqueSaisi.SetError(this, "Date non valide; mm/dd/yyyy");
					break;
				case TypeMasque.TelephoneAvecIndicatif:
					regStr = new Regex(@"\d{3}-\d{3}-\d{4}");
					if(!regStr.IsMatch(sd.Text))
						errTexteMasqueSaisi.SetError(this, "Phone number is not valid; xxx-xxx-xxxx");
					break;
				case TypeMasque.AdresseIP:
					short cnt=0;
					int len = sd.Text.Length;
					for(short i=0; i<len;i++)
						if(sd.Text[i] == '.')
						{
							cnt++;
							if(i+1 < len)
								if(sd.Text[i+1] == '.')
								{
									errTexteMasqueSaisi.SetError(this, "IP Address is not valid; x??.x??.x??.x??");
									break;
								}
						}
					if(cnt < 3 || sd.Text[len-1] == '.')
						errTexteMasqueSaisi.SetError(this, "IP Address is not valid; x??.x??.x??.x??");
					break;
				case TypeMasque.SSN:
					regStr = new Regex(@"\d{3}-\d{2}-\d{4}");
					if(!regStr.IsMatch(sd.Text))
						errTexteMasqueSaisi.SetError(this, "SSN is not valid; xxx-xx-xxxx");
					break;
				case TypeMasque.Decimal:
					break;
				case TypeMasque.Entier:
					break;
			}
		}
		private void MaskDigit(KeyPressEventArgs e)
		{
			//enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
			if (e.KeyChar == (char)3 || e.KeyChar == (char)22 || e.KeyChar == (char)24 || e.KeyChar ==(char)26)
			{
				e.Handled = false;
				return;
			}
			if(Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
			{
				errTexteMasqueSaisi.SetError(this, "");
				e.Handled = false;
			}
			else
			{
				e.Handled = true;
				errTexteMasqueSaisi.SetError(this, "Only valid for Digit");
			}
		}
		private void Masque_Decimal(KeyPressEventArgs e)
		{
			// Possibilité d'utiliser Keyboard Ctrl+C et Keyboard Ctrl+V
			if (e.KeyChar == (char)3 || e.KeyChar == (char)22 || e.KeyChar == (char)24 || e.KeyChar ==(char)26)
			{
				e.Handled = false;
				return;
			}

			if(Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8 || e.KeyChar == '-')
			{
				// if select all reset vars
				if(this.SelectionLength == this.Text.Length) 
				{
					if(e.KeyChar != (char)22)
						this.Text=null;
				}
				else 
				{
                    //ReplaceSelectionOrInsert(e, this.Text.Length);
                    if(ReplaceSelectionOrInsert(e,this.Text.Length))
                        return;
				}
				e.Handled = false;
				errTexteMasqueSaisi.SetError(this, "");
				string str = this.Text;
				if(e.KeyChar == '.')
				{
					int indx = str.IndexOf('.',0);
					if(indx > 0)
					{
                        e.Handled = true;
						errTexteMasqueSaisi.SetError(this, "Un décimal ne peut avoir plus d'un '.'");
					}
				}
				if(e.KeyChar == '-' && str.Length > 0)
				{
					e.Handled = true;
					errTexteMasqueSaisi.SetError(this, "'-' doit se trouver au début");
				}
			}
			else
			{
				e.Handled = true;
				errTexteMasqueSaisi.SetError(this, "seulement les chiffres ou un point");
			}
		}
		private bool ReplaceSelectionOrInsert(KeyPressEventArgs e, int len)
		{
			int selectStart = this.SelectionStart;
			int selectLen = this.SelectionLength;
			if(selectLen >0)
			{
				string str;
				str = this.Text.Remove(selectStart,selectLen);
				this.Text = str.Insert(selectStart,e.KeyChar.ToString());
				e.Handled = true;
				this.SelectionStart = selectStart+1;
				return true;
			}
			else if(selectLen == 0 && SelectionStart >0 && SelectionStart < len)
			{
				string str=this.Text;
				if(e.KeyChar == 8)
				{
					this.Text = str.Remove(selectStart-1,1);
					this.SelectionStart = selectStart-1;
				}
				else
				{
					this.Text = str.Insert(selectStart,e.KeyChar.ToString());
					this.SelectionStart = selectStart+1;
				}
				e.Handled = true;
				return true;
			}
			return false;
		}
		private void MaskDate(KeyPressEventArgs e)
		{
			int len = this.Text.Length;
			int indx = this.Text.LastIndexOf("/");

			//enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
			if (e.KeyChar == (char)3 || e.KeyChar == (char)22 || e.KeyChar == (char)24 || e.KeyChar ==(char)26)
			{
				e.Handled = false;
				return;
			}
			if(Char.IsDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == 8)
			{ 
				// if select all reset vars
				if(this.SelectionLength == len) 
				{
					indx=-1;
					digitPos=0;
					DelimitNumber=0;
					this.Text=null;
				}
				else 
				{
					if(ReplaceSelectionOrInsert(e,len))
						return;
				}
			
				string tmp = this.Text;
				if (e.KeyChar != 8)
				{
					if (e.KeyChar != '/' )
					{
						if(indx > 0)
							digitPos = len-indx;
						else
							digitPos++;

						if (digitPos == 3 && DelimitNumber < 2)
						{
							if (e.KeyChar != '/')
							{
								DelimitNumber++;
								this.AppendText("/");
							}
						}

						errTexteMasqueSaisi.SetError(this, "");
						if( (digitPos == 2 || (Int32.Parse(e.KeyChar.ToString())>1 && DelimitNumber ==0) ))
						{
							string tmp2;
							if(indx == -1)
								tmp2= e.KeyChar.ToString();
							else
								tmp2 = this.Text.Substring(indx+1)+e.KeyChar.ToString();
							
							if(DelimitNumber < 2)
							{
								if(digitPos==1) this.AppendText("0");
								this.AppendText(e.KeyChar.ToString());
								if(indx <0)
								{
									if(Int32.Parse(this.Text)> 12) // check validation
									{
										string str;
										str = this.Text.Insert(0, "0");
										if(Int32.Parse(this.Text)>13)
										{
											this.Text =str.Insert(2, "/0");
											DelimitNumber++;
											this.AppendText("/");
										}
										else
										{
											this.Text =str.Insert(2, "/");
											this.AppendText("");
										}
										DelimitNumber++;
									}
									else
									{
										this.AppendText("/");
										DelimitNumber++;
									}
									e.Handled=true;
								}
								else
								{
									if( DelimitNumber == 1)
									{
										int m = Int32.Parse(this.Text.Substring(0,indx));
										if(!CheckDayOfMonth(m, Int32.Parse(tmp2)))
										{
											errTexteMasqueSaisi.SetError(this, "Make sure this month have the day");
											e.Handled=true;
											return;
										}
										else
										{
											this.AppendText("/");
											DelimitNumber++;
											e.Handled=true;
										}
									}
								}
							}
						}
						else if(digitPos == 1 && Int32.Parse(e.KeyChar.ToString())>3 && DelimitNumber<2)
						{
							if(digitPos==1) this.AppendText("0");
							this.AppendText(e.KeyChar.ToString());
							this.AppendText("/");
							DelimitNumber++;
							e.Handled = true;
						}
						else 
						{
							if(digitPos == 1 && DelimitNumber==2 && e.KeyChar > '2')
								errTexteMasqueSaisi.SetError(this, "The year should start with 1 or 2");
						}
						if(	digitPos > 4)
							e.Handled = true;
					}
					else
					{
						if ( (this.Text.Length == 3) || (this.Text.Length == 6) || DelimitNumber > 1)
						{
							e.Handled = true;
						}
						else
						{
							DelimitNumber++;
							string tmp3;
							if(indx == -1)
								tmp3 = this.Text.Substring(indx+1);
							else
								tmp3 = this.Text;
							if(digitPos == 1)
							{
								this.Text = tmp3.Insert(indx+1,"0");;
								this.AppendText("/");
								e.Handled = true;
							}
						}
					}
				}
				else
				{
					e.Handled = false;
					if((len-indx) == 1)
					{
						DelimitNumber--;
						if (indx > -1 )
							digitPos = 2;
						else
							digitPos--;
					}
					else 
					{
						if(indx > -1)
							digitPos=len-indx-1;
						else
							digitPos=len-1;
					}
				}
			}
			else
			{
				e.Handled = true;
				errTexteMasqueSaisi.SetError(this, "Only valid for Digit and /");
			}
		}
		private void MaskPhoneSSN(KeyPressEventArgs e, int pos, int pos2)
		{
			int len = this.Text.Length;
			int indx = this.Text.LastIndexOf("-");
			//enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
			if (e.KeyChar == (char)3 || e.KeyChar == (char)22 || e.KeyChar == (char)24 || e.KeyChar ==(char)26)
			{
				e.Handled = false;
				return;
			}
			if(Char.IsDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == 8)
			{ // only digit, Backspace and - are accepted
				// if select all reset vars
				if(this.SelectionLength == len) 
				{
					indx=-1;
					digitPos=0;
					DelimitNumber=0;
					this.Text=null;
				}
				else 
				{
					if(ReplaceSelectionOrInsert(e,len))
						return;
				}
				string tmp = this.Text;
				if (e.KeyChar != 8)
				{
					errTexteMasqueSaisi.SetError(this, "");
					if (e.KeyChar != '-' )
					{
						if(indx > 0)
							digitPos = len-indx;
						else
							digitPos++;
					}
					if(indx > -1 && digitPos == pos2 && DelimitNumber == 1)
					{
						if (e.KeyChar != '-')
						{
							this.AppendText(e.KeyChar.ToString());
							this.AppendText("-");
							e.Handled = true;
							DelimitNumber++;
						}
					}
					if (digitPos == pos && DelimitNumber == 0)
					{
						if (e.KeyChar != '-')
						{
							this.AppendText(e.KeyChar.ToString());
							this.AppendText("-");
							e.Handled = true;
							DelimitNumber++;
						}
					}
					if(digitPos > 4)
						e.Handled = true;
				}
				else
				{
					e.Handled = false;
					if((len-indx) == 1)
					{
						DelimitNumber--;
						if ((indx) > -1 )
							digitPos = len-indx;
						else
							digitPos--;
					}
					else 
					{
						if(indx > -1)
							digitPos=len-indx-1;
						else
							digitPos=len-1;
					}
				}
			}
			else
			{
				e.Handled = true;
				errTexteMasqueSaisi.SetError(this, "Only valid for Digit and -");
			}
		}
		private void MaskIpAddr(KeyPressEventArgs e)
		{
			int len = this.Text.Length;
			int indx = this.Text.LastIndexOf(".");
			//enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
			if (e.KeyChar == (char)3 || e.KeyChar == (char)22 || e.KeyChar == (char)24 || e.KeyChar ==(char)26)
			{
				e.Handled = false;
				return;
			}
			if(Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
			{ // only digit, Backspace and dot are accepted
				// if select all reset vars
				if(this.SelectionLength == len) 
				{
					indx=-1;
					digitPos=0;
					DelimitNumber=0;
					this.Text=null;
				}
				else 
				{
					if(ReplaceSelectionOrInsert(e,len))
						return;
				}
				string tmp = this.Text;
				errTexteMasqueSaisi.SetError(this, "");
				if (e.KeyChar != 8)
				{
					if (e.KeyChar != '.' )
					{
						if(indx > 0)
							digitPos = len-indx;
						else
							digitPos++;	
						if(digitPos == 3 )
						{
							string tmp2 = this.Text.Substring(indx+1)+e.KeyChar;
							if(Int32.Parse(tmp2)> 255) // check validation
								errTexteMasqueSaisi.SetError(this,"The number can't be bigger than 255");
							else
							{
								if (DelimitNumber<3)
								{
									this.AppendText(e.KeyChar.ToString());
									this.AppendText(".");
									DelimitNumber++;
									e.Handled = true;
								}
							}
						}
						if (digitPos == 4)
						{
							if(DelimitNumber<3)
							{
								this.AppendText(".");
								DelimitNumber++;
							}
							else
								e.Handled = true;
						}
					}
					else
					{   // added - MAC
						// process the "."
						if (DelimitNumber + 1 > 3) // cant have more than 3 dots (at least for IPv4)
						{
							errTexteMasqueSaisi.SetError(this, "No more . please!");
							e.Handled = true; // dont add 4th dot
							this.Text.TrimEnd(e.KeyChar); 
						}
						else
						{	// got the right number, but don't allow two in a row
							if (this.Text.EndsWith("."))
							{
								errTexteMasqueSaisi.SetError(this, "Can't do two dots in a row");
								e.Handled = true;
							}
							else
							{	// ok, add the dot
								DelimitNumber++;
							}
						}
					}
				}
				else
				{
					e.Handled = false;
					if((len-indx) == 1)
					{
						DelimitNumber--;
						if (indx > -1 )
						{
							digitPos = len-indx;
						}
						else
							digitPos--;
					}
					else 
					{
						if(indx > -1)
							digitPos=len-indx-1;
						else
							digitPos=len-1;
					}
				}
			}
			else
			{
				e.Handled = true;
				errTexteMasqueSaisi.SetError(this, "Only valid for Digit abd dot");
			}
		}
		private bool CheckDayOfMonth(int mon, int day)
		{
			bool ret=true;
			if(day==0) ret=false;
			switch(mon)
			{
				case 1:
					if(day > 31 )
						ret=false;
					break;
				case 2:
					System.DateTime moment = DateTime.Now;
					int year = moment.Year;
					int d = ((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0) ) ) ? 29 : 28 ;
					if(day > d)
						ret=false;
					break;
				case 3:
					if(day > 31 )
						ret=false;
					break;
				case 4: 
					if(day > 30 )
						ret=false;
					break;
				case 5:
					if(day > 31 )
						ret=false;
					break;
				case 6:
					if(day > 30 )
						ret=false;
					break;
				case 7:
					if(day > 31 )
						ret=false;
					break;
				case 8:
					if(day > 31 )
						ret=false;
					break;
				case 9:
					if(day > 30 )
						ret=false;
					break;
				case 10:
					if(day > 31 )
						ret=false;
					break;
				case 11:
					if(day > 30 )
						ret=false;
					break;
				case 12:
					if(day > 31 )
						ret=false;
					break;
				default:
					ret=false;
					break;
			}
			return ret;
		}
    }

    [ToolboxBitmap(typeof(TextBox))] // Pour avoir un bel icone de Textbox dans la toolbox
    public class NumericBox : TextBox
    {
        public NumericBox()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errNumericBox = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errNumericBox)).BeginInit();
            this.SuspendLayout();
            // 
            // errTexteMasqueSaisi
            // 
            this.errNumericBox.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            ((System.ComponentModel.ISupportInitialize)(this.errNumericBox)).EndInit();
            this.ResumeLayout(false);

            // 
            // TexteMasqueSaisi
            // 
            //this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            //this.Leave += new System.EventHandler(this.OnLeave);
        }

        #endregion

        private System.Windows.Forms.ErrorProvider errNumericBox;
        //* Désactiver les 6 lignes suivantes pour permettre le copier / coller */
        private const int WM_PASTE = 0x0302;
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg != WM_PASTE)
                base.WndProc(ref m);
        }
        protected override void OnValidating(CancelEventArgs e)
        {
            //base.OnValidating(e);
            TextBox T = ((TextBox)this);
            try
            {
                if (T.Text == "") T.Text = "0";
                Decimal.Parse(T.Text);
                errNumericBox.SetError(T, "");
                T.Text = Convert.ToDouble(T.Text).ToString("N2");
            }
            catch (ArgumentNullException)
            {
                errNumericBox.SetError(T, "La case ne peut être vide !");
                T.SelectAll();
                e.Cancel = true;
            }
            catch (OverflowException)
            {
                errNumericBox.SetError(T, "Le nombre est trop grand !");
                T.SelectAll();
                e.Cancel = true;
            }
            catch (FormatException)
            {
                errNumericBox.SetError(T, "Le format n'est pas correct");
                T.SelectAll();
                e.Cancel = true;
            }

        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // stoque le séparateur décimal du système
            char Separateur = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            

            // Si la caractère tapé est numérique
            if (char.IsNumber(e.KeyChar) || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-'))
            {
                // dans le cas de l'ecriture d'un séparateur
                if ((e.KeyChar == '.') || (e.KeyChar == ','))
                {
                    // Force l'ecriture du bon séparateur
                    e.KeyChar = Separateur;
                }

                if (e.KeyChar == '²') e.Handled = true; // Si c'est un '²', on gère l'evenement.
                else e.Handled = false; // Sinon, on laisse passer le caractère (On peut omettre cette ligne)
            }
            // Si le caractère tapé est un caractère de "controle" (Enter, backspace, ...), on laisse passer
            else if (char.IsControl(e.KeyChar)) e.Handled = false;
            // Et sinon, on gère toutes les autres touches tapées, et on en fait rien
            else e.Handled = true;
        }
    }
}
