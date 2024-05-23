using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace shop_online
{
    public partial class FormLogin : Form
    {
        private Afisare_Produse afisare_Produse = null;// Form nou

        public FormLogin()
        {
            // Shown += new EventHandler(autoLogin);
            InitializeComponent();
        }
        public FormLogin( int userid )
        {
            InitializeComponent();
        }

        //Roli   
        private void FormLogin_FormClosed( object sender, FormClosedEventArgs e )
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
        private void FormLogin_Load( object sender, EventArgs e )
        {
            
            Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            Aranjare.ToateTextBoxurileledinPanelGoale(panelMenu);
            panelSignUp.Hide();
            panelMenu.Show();
            buttonLoginPanelMenu.Visible = true;
            MinimumSize = new Size(panelMenu.Width + 50, panelMenu.Height + 50);
            Size = new Size(panelMenu.Width + 50, panelMenu.Height + 50);

            panelMenu.Anchor = AnchorStyles.None; // Debifează orice ancorare existentă pentru panel menu
            panelMenu.Dock = DockStyle.None; // Dezactivează orice ancorare existentă pentru panel menu
            Size = panelMenu.Size;
            panelMenu.Location = new Point(0, 0);
            MinimumSize = new Size(panelMenu.Width, panelMenu.Height);


            butonuldeBack();
        }
        private void butonuldeBack()
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            panelMenu.Show();
            panelSignUp.Hide();
            MinimumSize = new Size(panelMenu.Width + 50, panelMenu.Height + 50);
        }
        private void buttonSignUp_Click( object sender, EventArgs e )
        {
            Aranjare.ToateTextBoxurileledinPanelGoale(panelSignUp);
            SignUpButton();
        }
        private void SignUpButton()
        {
            panelMenu.Hide();
            panelSignUp.Show();
            panelSignUp.Anchor = AnchorStyles.None;
            panelSignUp.Dock = DockStyle.None;
            MinimumSize = new Size(panelSignUp.Width + 50, panelSignUp.Height + 50);
            Size = new Size(panelSignUp.Width + 50, panelSignUp.Height + 50);
            FormBorderStyle = FormBorderStyle.Sizable;
            panelSignUp.Location = new Point(0, 0);
            Aranjare.ToateObicteledinPanelVisible(panelSignUp, true);
        }
        private void buttonBack_Click( object sender, EventArgs e )
        {
            butonuldeBack();
            Size = new Size(panelMenu.Width + 50, panelMenu.Height + 50);
        }
        private void buttonAcces_Click( object sender, EventArgs e )
        {
            string parola = textBoxParola.Text;
            string telefon = textBoxTelefon.Text.Trim();
            string tel = string.Empty, email_sus = string.Empty;
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); return; }

            if (Aranjare.IsValidTelefon(telefon))
                tel = telefon;
            else if (Aranjare.IsValidEmail(telefon))
                email_sus = telefon;
            else
            {
                MessageBox.Show("Introduceți un email sau telefon valid.");
                return;
            }

            if (Interogari.Login(connectionString, email_sus, tel, parola))
            {
                stayLogged(telefon, parola);
                CloseCurrentFormAndOpenNewFormAsync("", email_sus, parola, tel, "", "", "", -1);
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
                    MinimumSize = new Size(520 * 2, 138 * 4)
                };
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
            //afisare_Produse.LoadUser(email, parola, telefon);
            afisare_Produse.Show();
            afisare_Produse.Focus();
         
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
            buttonLoginPanelMenu.Visible = config.buttonAccesVisible;
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
        private void stayLogged( string user, string parola )
        {
            string filePath = "log_info.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(user);
                    writer.WriteLine(parola);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing to file: " + ex.Message);
            }
        }
        private void autoLogin( object sender, EventArgs e )
        {
            string filePath = "log_info.txt";
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists || fileInfo.Length == 0)
                    return;

                string [] lines = File.ReadAllLines(filePath);
                if (lines.Length % 2 == 1)
                    return;

                string telefon = lines [0];
                string parola = lines [1];
                string tel = string.Empty;
                string email_sus = string.Empty;

                if (Aranjare.IsValidTelefon(telefon))
                    tel = telefon;
                else if (Aranjare.IsValidEmail(telefon))
                    email_sus = telefon;
                else
                    return;

                string connectionString = Aranjare.GetConnectionString();
                if (Interogari.GetUserID(connectionString, email_sus, tel, parola) <= 0)
                    return;

                CloseCurrentFormAndOpenNewFormAsync("", email_sus, parola, tel, "", "", "", -1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la autentificare automată: {ex.Message}");
            }
        }
        private void buttonSgnFurnizor_Click(object sender, EventArgs e)
        {
            this.Hide();
            Adauga_Furnizor adauga_Furnizor = new Adauga_Furnizor(true);
            adauga_Furnizor.Show();


        }
        //Puia


        //Horia
        private void buttonSignUp2_Click( object sender, EventArgs e )
        {
            string nume = Aranjare.FormatName(textBoxNume.Text);
            string email = Aranjare.FormatName(textBoxEmail.Text);
            string parola = textBoxParola2.Text.Trim();
            string telefon = textBoxTelefon2.Text.Trim();
            string judet = Aranjare.FormatName(textBoxJudet.Text);
            string oras = Aranjare.FormatName(textBoxOras.Text);
            string strada = Aranjare.FormatName(textBoxStrada.Text);

            // string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            if (!int.TryParse(textBoxNumar_Strada.Text, out int numar) || numar < 1)
            {
                MessageBox.Show("Introduceți un număr valid pentru stradă.");
                return;
            }

            if (Interogari.SignUp(connectionString, nume, email, parola, telefon, judet, oras, strada, numar))
            {
                stayLogged(telefon, parola);
                CloseCurrentFormAndOpenNewFormAsync(nume, email, parola, telefon, judet, oras, strada, numar);
            }
        }

        
    }
}
