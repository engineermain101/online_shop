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
        //private Carti cartiForm = null;// Form nou

        public FormLogin()
        {
            InitializeComponent();
        }

        //Roli   
        private void FormLogin_Load( object sender, EventArgs e )
        {
            Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            panelSignUp.Hide();
            panelMenu.Show();

            panelMenu.Anchor = AnchorStyles.None; // Debifează orice ancorare existentă pentru panel menu
            panelMenu.Dock = DockStyle.None; // Dezactivează orice ancorare existentă pentru panel menu

            int xp = (ClientSize.Width - buttonLogin.Width) / 2;
            int yp = (ClientSize.Height - panelMenu.Height) / 2;
            panelMenu.Location = new Point(xp - 125, yp);

            int x = (panelMenu.ClientSize.Width - buttonLogin.Width) / 2;
            int y = (panelMenu.ClientSize.Height - buttonLogin.Height) / 2;

            // Setează locația și ancorajul butoanelor în panel-ul Menu
            buttonLogin.Location = new Point(x - 125, y);
            buttonLogin.Anchor = AnchorStyles.None;

            buttonSignUp.Location = new Point(buttonLogin.Right + 100, buttonLogin.Top);
            buttonSignUp.Anchor = AnchorStyles.None;

            butonuldeBack();
        }
        private void butonuldeBack()
        {
            Width = 681;
            Height = 423;
            //panelMenu.Height = 277;
            // panelMenu.Width = 505;

            FormBorderStyle = FormBorderStyle.Sizable;
            Aranjare panelConfiguration = new Aranjare
            {
                panelMenuVisible = true,
                panelSignUpVisible = false,
            };
            SetPanelState(panelConfiguration);
            signupApasat = false;
            MinimumSize = new Size(panelMenu.Width + 50, panelMenu.Height + 50);
        }
        private void buttonSignUp_Click( object sender, EventArgs e )
        {
            Console.WriteLine("Ati apasat SignUp!");
            Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            SignUpButton();
        }
        private void SignUpButton()
        {
            signupApasat = true;

            panelMenu.Anchor = AnchorStyles.None;
            panelMenu.Dock = DockStyle.None;

            Size = panelMenu.Size;
            panelMenu.Location = new Point(0, 0);
            MinimumSize = new Size(panelSignUp.Width, panelSignUp.Height);

            Aranjare panelConfiguration = new Aranjare
            {
                panelMenuVisible = false,
                panelSignUpVisible = true,
                labelTelefonText = "Telefon",
            };
            SetPanelState(panelConfiguration);
            //FormBorderStyle = FormBorderStyle.FixedSingle;

            Aranjare.ToateObicteledinPanelVisible(panelSignUp, true);
        }
        private void buttonBack_Click( object sender, EventArgs e )
        {
            butonuldeBack();
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
            // Creează o instanță a noului form      
            /*
            inaltime panel=451  latime panel=455
            inaltime menu=24  latime menu=476
            min inaltime=535  min latime=492
            inaltime form=294  latime form=375              
            */


            /*

            bibliotecaPanelTextBoxNume.Clear();
            bibliotecaPanelTextBoxPrenume.Clear();
            bibliotecaPanelTextBoxEmail.Clear();
            bibliotecaPanelTextBoxParola.Clear();
            bibliotecaPanelTextBoxTelefon.Clear();
            butonuldeBack();

            if (cartiForm == null)
            {
                /*cartiForm = new Carti(nume, prenume, email, parola, telefon)
                {
                    MinimumSize = new Size(490, 535)
                };
                cartiForm = new Carti("a", "a", "a@a.a", "a", telefon)
                {
                    MinimumSize = new Size(490, 535)
                };
                //cartiForm.LoadUser(nume, prenume, email, parola, telefon);
                cartiForm.Size = cartiForm.MinimumSize;
                cartiForm.FormClosed += ( sender, e ) => { cartiForm = null; }; // Resetare referință când formularul este închis
            }

            if (!cartiForm.Visible)
            {
                cartiForm.Visible = true;

                if (Application.OpenForms ["Biblioteca"] != null)
                {
                    Application.OpenForms ["Biblioteca"].Hide(); // Ascunde formularul Biblioteca
                }
            }
            //cartiForm.LoadUser(nume, prenume, email, parola, telefon);
            cartiForm.LoadUser("a", "a", "a@a.a", "a", telefon);*/
        }

        private void buttonLogin_Click( object sender, EventArgs e )
        {
            //Console.WriteLine("Ati apasat Login!");
            panelMenu.Anchor = AnchorStyles.None;
            panelMenu.Dock = DockStyle.None;
            Size = panelMenu.Size;
            panelMenu.Location = new Point(0, 0);
            MinimumSize = new Size(panelSignUp.Width, panelSignUp.Height);


            Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            Aranjare.ToateObicteledinPanelVisible(panelSignUp, false);
            Aranjare panelConfiguration = new Aranjare
            {
                panelMenuVisible = false,
                panelSignUpVisible = true,
                labelParolaVisible = true,
                textBoxParolaVisible = true,
                labelTelefonVisible = true,
                textBoxTelefonVisible = true,
                buttonAccesVisible = true,
                buttonBackVisible = true,
                labelTelefonText = "Telefon sau Email",

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







    }
}
