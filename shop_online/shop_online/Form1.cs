using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
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
            };
            SetPanelState(panelConfiguration);
            //FormBorderStyle = FormBorderStyle.FixedSingle;

            foreach (Control control in panelSignUp.Controls)
            {
                control.Visible = true;
            }
        }
        private void buttonBack_Click( object sender, EventArgs e )
        {
            butonuldeBack();
        }
        private void buttonAcces_Click( object sender, EventArgs e )
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string nume = Aranjare.FormatName(textBoxNume.Text);
            string email = Aranjare.FormatName(textBoxEmail.Text);
            string parola = textBoxParola.Text;
            string telefon = textBoxTelefon.Text.Trim();
            string judet = Aranjare.FormatName(textBoxJudet.Text);
            string oras = Aranjare.FormatName(textBoxOras.Text);
            string strada = Aranjare.FormatName(textBoxStrada.Text);
            int numar = -1;

            string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;

            if (!Aranjare.IsValidTelefon(telefon))
            {
                MessageBox.Show("Introduceți un telefon valid.");
                return;
            }

            if (!int.TryParse(textBoxNumar_Strada.Text, out numar) && numar < 1)
            {
                MessageBox.Show("Introduceți un număr valid pentru stradă.");
                return;
            }

            if (signupApasat)
            {//Logica butonului de signUp
                if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(parola) ||
                    string.IsNullOrWhiteSpace(judet) || string.IsNullOrWhiteSpace(oras) ||
                    string.IsNullOrWhiteSpace(strada) || numar < 1)
                {
                    MessageBox.Show("Trebuie completate câmpurile!");
                    return;
                }
                if (!Aranjare.IsValidEmail(email))//optional
                {
                    MessageBox.Show("Introduceți un email de telefon valid.");
                    return;
                }
                if (SignUp(connectionString, nume, email, parola, telefon, judet, oras, strada, numar))
                {
                    CloseCurrentFormAndOpenNewFormAsync(nume, email, parola, telefon, judet, oras, strada, numar);
                }
            }
            else
            {// Logica pentru Login
             /* if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(prenume) || string.IsNullOrWhiteSpace(parola))
              {
                  MessageBox.Show("Trebuie completate câmpurile: nume, prenume, parola");
                  return;
              }*/
                if (Login(connectionString, nume, email, parola, telefon, judet, oras, strada, numar))
                {
                    CloseCurrentFormAndOpenNewFormAsync(nume, email, parola, telefon, judet, oras, strada, numar);
                }
            }
        }


        private void SetPanelState( Aranjare config )
        {

            panelMenu.Visible = config.panelMenuVisible;
            panelSignUp.Visible = config.panelSignUpVisible;


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




        private bool SignUp( string connectionString, string nume, string email, string parola,
            string telefon, string judet, string oras, string strada, int numar )
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    /* string checkQuery = @"SELECT COUNT(*) FROM Useri
                                   WHERE nume = @nume AND  parola = @parola
                                   AND ( telefon = @telefon OR email = @email) 
                                   AND judet = @judet AND oras = @oras 
                                   AND strada = @strada AND numar = @numar";*/

                    string checkQuery = @" SELECT COUNT(*)
                                        FROM Useri AS u
                                        JOIN Adresa AS a ON u.id_adresa = a.id_adresa
                                        WHERE u.nume = @nume
                                            AND (u.telefon = @telefon OR u.email = @email)
                                            ANDu.parola = @parola 
                                            AND a.judet = @judet
                                            AND a.oras = @oras
                                            AND a.strada = @strada
                                            AND a.numar = @numar";


                    // Verifică dacă utilizatorul există deja în baza de date
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@nume", nume);
                        checkCommand.Parameters.AddWithValue("@telefon", telefon);
                        checkCommand.Parameters.AddWithValue("@parola", parola);
                        checkCommand.Parameters.AddWithValue("@email", email);
                        checkCommand.Parameters.AddWithValue("@judet", judet);
                        checkCommand.Parameters.AddWithValue("@oras", oras);
                        checkCommand.Parameters.AddWithValue("@strada", strada);
                        checkCommand.Parameters.AddWithValue("@numar", numar);

                        int existingUserCount = (int)checkCommand.ExecuteScalar();

                        if (existingUserCount > 0)
                        {
                            MessageBox.Show("Utilizatorul există deja în baza de date.");
                            return false;
                        }
                    }
                    string insertAddressQuery = @"INSERT INTO Adresa 
                                          (judet, oras, strada, numar) 
                                          VALUES (@judet, @oras, @strada, @numar); 
                                          SELECT SCOPE_IDENTITY();";
                    int userId;
                    using (SqlCommand insertAddressCommand = new SqlCommand(insertAddressQuery, connection))
                    {

                        insertAddressCommand.Parameters.AddWithValue("@judet", judet);
                        insertAddressCommand.Parameters.AddWithValue("@oras", oras);
                        insertAddressCommand.Parameters.AddWithValue("@strada", strada);
                        insertAddressCommand.Parameters.AddWithValue("@numar", numar);

                        // Obținem ID-ul 
                        userId = Convert.ToInt32(insertAddressCommand.ExecuteScalar());
                    }

                    string insertUserQuery = @"INSERT INTO Useri
                                    (nume, email, parola, telefon,id_adresa) 
                                    VALUES (@nume, @email, @parola, @telefon,@id_adresa);                                   "; // Obținem ID-ul utilizatorului inserat

                    using (SqlCommand insertUserCommand = new SqlCommand(insertUserQuery, connection))
                    {
                        insertUserCommand.Parameters.AddWithValue("@nume", nume);
                        insertUserCommand.Parameters.AddWithValue("@email", email);
                        insertUserCommand.Parameters.AddWithValue("@parola", parola);
                        insertUserCommand.Parameters.AddWithValue("@telefon", telefon);
                        insertUserCommand.Parameters.AddWithValue("@id_adresa", userId);

                        insertUserCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Utilizatorul a fost adăugat cu succes în baza de date.");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la inserarea în baza de date: " + ex.Message);

                }
            }
            return false;
        }

        private bool Login( string connectionString, string nume, string email, string parola,
            string telefon, string judet, string oras, string strada, int numar )
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    /*  string checkQuery = @"SELECT COUNT(*) FROM Useri
                                  WHERE nume = @nume AND  parola = @parola
                                  AND ( telefon = @telefon OR email = @email) 
                                  AND judet = @judet AND oras = @oras 
                                  AND strada = @strada AND numar = @numar";


                       string query = "SELECT COUNT(*) FROM Utilizatori WHERE nume = @nume AND prenume = @prenume AND parola = @parola AND email=@email";
                       using (SqlCommand command = new SqlCommand(query, connection))
                       {
                           command.Parameters.AddWithValue("@nume", nume);
                           command.Parameters.AddWithValue("@prenume", prenume);
                           command.Parameters.AddWithValue("@parola", parola);
                           command.Parameters.AddWithValue("@email", email);

                           int result = (int)command.ExecuteScalar();

                           if (result > 0)
                           {
                               // Utilizatorul există în baza de date
                               return true;
                           }
                           else
                               MessageBox.Show("Autentificare eșuată. Verificați datele de autentificare sau nu v-ati conectat inca.");
                       } */
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la autentificare: " + ex.Message);
                }
            }



            return false;
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






        //Claudiu


        //Puia


        //Horia







    }
}
