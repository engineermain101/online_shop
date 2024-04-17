using System;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace shop_online
{
    public partial class FormLogin : Form
    {
        private int height;
        private int width;
        private bool signupApasat = false;
        //bool backlaCarti = false;
        private Afisare_Produse afisare_Produse = null;// Form nou

        public FormLogin()
        {
            InitializeComponent();
        }

        //Roli   
        private void FormLogin_Load( object sender, EventArgs e )
        {
            //Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            //panelSignUp.Hide();

            panelSignUp.Visible = false;
            //panelSignUp.Visible = true;
            labelParola.Visible = true;
            textBoxParola.Visible = true;
            labelTelefon.Text = "Telefon sau Email";
            labelTelefon.Visible = true;
            textBoxTelefon.Visible = true;
            buttonAcces.Visible = true;
            buttonBack.Visible = true;
            buttonLogin.Visible = false;
            MinimumSize = new Size(panelMenu.Width + 50, panelMenu.Height + 50);
            this.Size = new Size(panelMenu.Width + 50, panelMenu.Height + 50);


            //panelMenu.Anchor = AnchorStyles.None; // Debifează orice ancorare existentă pentru panel menu
            // panelMenu.Dock = DockStyle.None; // Dezactivează orice ancorare existentă pentru panel menu
            /*
            int xp = (ClientSize.Width - buttonLogin.Width) / 2;
            int yp = (ClientSize.Height - panelMenu.Height) / 2;
            //panelMenu.Location = new Point(xp - 125, yp);

            int x = (panelMenu.ClientSize.Width - buttonLogin.Width) / 2;
            int y = (panelMenu.ClientSize.Height - buttonLogin.Height) / 2;

            // Setează locația și ancorajul butoanelor în panel-ul Menu
            buttonLogin.Location = new Point(x - 125, y);
            buttonLogin.Anchor = AnchorStyles.None;

            buttonSignUp.Location = new Point(buttonLogin.Right + 100, buttonLogin.Top);
            buttonSignUp.Anchor = AnchorStyles.None;
            */
            butonuldeBack();
        }
        private void butonuldeBack()
        {
            //Width = 681;
            //Height = 423;
            //panelMenu.Height = 277;
            // panelMenu.Width = 505;

            FormBorderStyle = FormBorderStyle.Sizable;
            Aranjare panelConfiguration = new Aranjare
            {
                //panelMenuVisible = true,
                panelSignUpVisible = false,
                buttonAccesVisible = true,
                
            };
            buttonSignUp.Visible = true;
            SetPanelState(panelConfiguration);
            panelSignUp.Visible = false;
            labelTelefon.Text = "Telefon sau Email";
            signupApasat = false;
            MinimumSize = new Size(panelMenu.Width + 50, panelMenu.Height + 50);

        }
        private void buttonSignUp_Click( object sender, EventArgs e )
        {
            Console.WriteLine("Ati apasat SignUp!");
            Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            SignUpButton();
            ShiftUpSignUpButtons();
        }
        private void SignUpButton()
        {
            signupApasat = true;
            //labelTelefon.Location = new Point(labelTelefon.Location.X, labelTelefon.Location.Y + 20);
           /* panelMenu.Anchor = AnchorStyles.None;
            panelMenu.Dock = DockStyle.None;

            Size = panelMenu.Size;
            panelMenu.Location = new Point(0, 0);
            MinimumSize = new Size(panelSignUp.Width, panelSignUp.Height);*/

            Aranjare panelConfiguration = new Aranjare
            {
                panelMenuVisible = true,
                panelSignUpVisible = true,
                labelTelefonText = "Telefon",
                buttonAccesVisible = false,
                
            };

            labelNume.Visible = true;
            buttonSignUp.Visible = false;

            SetPanelState(panelConfiguration);
            //FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimumSize = new Size(panelSignUp.Width, panelMenu.Height + panelSignUp.Height - 50);
            Aranjare.ToateObicteledinPanelVisible(panelSignUp, true);
        }
        private void buttonBack_Click( object sender, EventArgs e )
        {
            butonuldeBack();
            ShiftDownSignUpButtons();
            this.Size = new Size(panelMenu.Width + 50, panelMenu.Height + 50);
        }
        private void buttonAcces_Click( object sender, EventArgs e )
        {
            string nume = Aranjare.FormatName(textBoxNume.Text);
            string email = Aranjare.FormatName(textBoxEmail.Text);
            string parola = textBoxParola.Text;
            string telefon = textBoxTelefon.Text.Trim();
            string judet = Aranjare.FormatName(textBoxJudet.Text);
            string oras = Aranjare.FormatName(textBoxOras.Text);
            string strada = Aranjare.FormatName(textBoxStrada.Text);
            int numar;

            string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;

            if (signupApasat)
            {//Logica butonului de signUp

                if (!Aranjare.IsValidTelefon(telefon))
                {
                    MessageBox.Show("Introduceți un telefon valid.");
                    return;
                }

                if (!int.TryParse(textBoxNumar_Strada.Text, out numar) || numar < 1)
                {
                    MessageBox.Show("Introduceți un număr valid pentru stradă.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(parola) ||
                    string.IsNullOrWhiteSpace(judet) || string.IsNullOrWhiteSpace(oras) ||
                    string.IsNullOrWhiteSpace(strada) || numar < 1)
                {
                    MessageBox.Show("Trebuie completate câmpurile!");
                    return;
                }
                if (!Aranjare.IsValidEmail(email))//optional
                {
                    MessageBox.Show("Introduceți un email de email valid.");
                    return;
                }
                if (Interogari.SignUp(connectionString, nume, email, parola, telefon, judet, oras, strada, numar))
                {
                    CloseCurrentFormAndOpenNewFormAsync(nume, email, parola, telefon, judet, oras, strada, numar);
                }
            }
            else
            {// Logica pentru Login
                string tel = string.Empty, email_sus = string.Empty;

                if (string.IsNullOrWhiteSpace(parola))
                {
                    MessageBox.Show("Trebuie completat câmpul parolă");
                    return;
                }

                if (Aranjare.IsValidTelefon(telefon))
                {
                    tel = telefon;
                }
                else if (Aranjare.IsValidEmail(telefon))
                {
                    email_sus = telefon;
                }
                else
                {
                    MessageBox.Show("Introduceți un email sau telefon valid.");
                    return;
                }

                if (Interogari.Login(connectionString, email_sus, tel, parola))
                {
                    numar = -1;
                    CloseCurrentFormAndOpenNewFormAsync(nume, email_sus, parola, tel, judet, oras, strada, numar);
                }
            }
        }
               

        private void CloseCurrentFormAndOpenNewFormAsync( string nume, string email, string parola,
        string telefon, string judet, string oras, string strada, int numar )
        {
            Hide();
            Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            butonuldeBack();

            if (afisare_Produse == null)
            {
                afisare_Produse = new Afisare_Produse(email, parola, telefon)
                {
                    MinimumSize = new Size(520*2, 138*4)
                };
                /*   afisare_Produse = new Afisare_Produse("a@a.a", "a", telefon)
                   {
                       MinimumSize = new Size(490, 535)
                   };*/
                afisare_Produse.Size = afisare_Produse.MinimumSize;
                afisare_Produse.FormClosed += ( sender, e ) => { afisare_Produse = null; }; // Resetare referință când formularul este închis
            }

            if (!afisare_Produse.Visible)
            {
                afisare_Produse.Visible = true;

                if (Application.OpenForms ["FormLogin"] != null)
                {
                    Application.OpenForms ["FormLogin"].Hide();
                }
            }
            afisare_Produse.LoadUser(email, parola, telefon);

            /*      if (afisare_Produse == null)
                  {
                      afisare_Produse = new Afisare_Produse(email, parola, telefon)
                      {
                          MinimumSize = new Size(490, 535),
                          Size = new Size(490, 535)
                      };
                      afisare_Produse.FormClosed += ( sender, e ) => { afisare_Produse = null; }; // Resetare referință când formularul este închis
                  }*/

            //afisare_Produse.LoadUser("a@a.a", "a", telefon);
            afisare_Produse.Show();
            afisare_Produse.Focus();
        }

        private void buttonLogin_Click( object sender, EventArgs e )
        {
            //Console.WriteLine("Ati apasat Login!");
            /*panelMenu.Anchor = AnchorStyles.None;
            panelMenu.Dock = DockStyle.None;
            Size = panelMenu.Size;
            panelMenu.Location = new Point(0, 0);
            MinimumSize = new Size(panelSignUp.Width, panelSignUp.Height);
            */

            Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            Aranjare.ToateObicteledinPanelVisible(panelSignUp, false);
            Aranjare panelConfiguration = new Aranjare
            {
                /*panelMenuVisible = false,
                panelSignUpVisible = true,
                labelParolaVisible = true,
                textBoxParolaVisible = true,
                labelTelefonVisible = true,
                textBoxTelefonVisible = true,
                buttonAccesVisible = true,
                buttonBackVisible = true,
                labelTelefonText = "Telefon sau Email",*/

            };
            SetPanelState(panelConfiguration);
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void SetPanelState( Aranjare config )
        {
            panelMenu.Visible = config.panelMenuVisible;
            panelSignUp.Visible = config.panelSignUpVisible;
            labelParola.Visible = config.labelParolaVisible;
            textBoxParola.Visible = config.textBoxParolaVisible;
            labelTelefon.Visible = config.labelTelefonVisible;
            textBoxTelefon.Visible = config.textBoxTelefonVisible;
            labelTelefon.Text = config.labelTelefonText;
            buttonAcces.Visible = config.buttonAccesVisible;
            buttonBack.Visible = config.buttonBackVisible;
            /*List<string> panelKeys = panelStates.Keys.ToList();

            // Ascunde toate panourile
            foreach (string panelKey in panelKeys)
            {
                panelStates [panelKey] = false;
                Control panel = Controls.Find(panelKey, true).FirstOrDefault() as Control;
                if (panel != null)
                {
                    panel.Visible = false;
                }
            }

            // Afisează panoul specificat și setează starea
            if (panelStates.ContainsKey(config.PanelName))
            {
                panelStates [config.PanelName] = true;
                Control panel = Controls.Find(config.PanelName, true).FirstOrDefault() as Control;
                if (panel != null)
                {
                    panel.Visible = true;
                }
            }

            // Setează vizibilitatea și proprietățile controalelor pentru panoul de carti                    
            cartiPanelTextBoxISBN.ReadOnly = config.IsISBNReadOnly;
            cartiPanelComboBoxEditura.DropDownStyle = config.IsEdituraDropDown ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
            cartiPanelTextBoxNrCopii.Visible = config.IsNrCopiiVisible;
            cartiPanelLabelNrCopii.Visible = config.IsNrCopiiVisible;
            cartiPanelCartiButtonAdaugasiSterge.Visible = config.IsAdaugasiStergeVisible;
            cartiPanelCartiButtonGoleste.Visible = true;
            cartiPanelCartiButtonCauta.Text = config.CautaButtonText;
            cartiPanelCartiButtonAdaugasiSterge.Text = config.AdaugasiStergeButtonText;

            // Setează vizibilitatea și textul pentru panoul de utilizatori
            cartiPanelComboBoxRolAdmin.Visible = config.IsComboBoxRolAdminVisible;
            cartiPanelLabelRolAdmin.Visible = config.IsComboBoxRolAdminVisible;
            cartiPanelUserButtonSterge.Text = config.UserButtonStergeText;
            cartiPanelLabelNumeUser.Text = "Nume Utilizator";
            cartiPanelLabelPrenumeUser.Text = "Prenume Utilizator";*/
        }

        //Claudiu


        //Puia


        //Horia
private void buttonSignUp2_Click(object sender, EventArgs e)
        {
            string nume = Aranjare.FormatName(textBoxNume.Text);
            string email = Aranjare.FormatName(textBoxEmail.Text);
            string parola = textBoxParola.Text;
            string telefon = textBoxTelefon.Text.Trim();
            string judet = Aranjare.FormatName(textBoxJudet.Text);
            string oras = Aranjare.FormatName(textBoxOras.Text);
            string strada = Aranjare.FormatName(textBoxStrada.Text);
            int numar;

            string connectionString = ConfigurationManager.ConnectionStrings["DatadeBaza"].ConnectionString;

            if (signupApasat)
            {//Logica butonului de signUp

                if (!Aranjare.IsValidTelefon(telefon))
                {
                    MessageBox.Show("Introduceți un telefon valid.");
                    return;
                }

                if (!int.TryParse(textBoxNumar_Strada.Text, out numar) || numar < 1)
                {
                    MessageBox.Show("Introduceți un număr valid pentru stradă.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(parola) ||
                    string.IsNullOrWhiteSpace(judet) || string.IsNullOrWhiteSpace(oras) ||
                    string.IsNullOrWhiteSpace(strada) || numar < 1)
                {
                    MessageBox.Show("Trebuie completate câmpurile!");
                    return;
                }
                if (!Aranjare.IsValidEmail(email))//optional
                {
                    MessageBox.Show("Introduceți un email de email valid.");
                    return;
                }
                if (Interogari.SignUp(connectionString, nume, email, parola, telefon, judet, oras, strada, numar))
                {
                    CloseCurrentFormAndOpenNewFormAsync(nume, email, parola, telefon, judet, oras, strada, numar);
                }
            }
        }

        private void ShiftUpSignUpButtons() //functia asta muta text-box-urile cu tot cu label-uri mai sus pt ca sa arate mai bine ecranu de sign up
        {
            panelSignUp.Location = new Point(0, this.Location.Y - 20);
            //panelMenu.Location = new Point(0, this.Location.Y + 65);

        }

        private void ShiftDownSignUpButtons() //functia asta muta text-box-urile cu tot cu label-uri mai jos pt ca sa arate mai bine ecranu de login
        {
            panelSignUp.Location = new Point(0, this.Location.Y + 20);
            //panelMenu.Location = new Point(0, this.Location.Y - 65);
        }





    }
}
