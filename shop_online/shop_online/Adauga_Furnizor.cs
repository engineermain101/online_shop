using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace shop_online
{
    public partial class Adauga_Furnizor : KryptonForm
    {
        private int id_user;
        private bool adauga;
        public Adauga_Furnizor()
        {
            InitializeComponent();
        }
        public Adauga_Furnizor( bool add )
        {
            InitializeComponent();
            adauga = add;
        }


        private void Adauga_Furnizor_FormClosed( object sender, FormClosedEventArgs e )
        {
            if (Application.OpenForms ["Afisare_Produse"] != null)
                Application.OpenForms ["Afisare_Produse"].Show();
        }
        private void Adauga_Furnizor_Load( object sender, EventArgs e )
        {
            if (adauga)
            {
                Aranjare.CenteredPanel(this, panelAdaugaFurnizor);
                panelAdaugaFurnizor.Show();
                panelStergeFurnizor.Hide();
                MinimumSize = panelAdaugaFurnizor.Size + new Size(50, 50);
                return;
            }

            Aranjare.CenteredPanel(this, panelStergeFurnizor);
            panelAdaugaFurnizor.Hide();
            panelStergeFurnizor.Show();
            MinimumSize = panelStergeFurnizor.Size + new Size(50, 50);

            try
            {
                string con = Aranjare.GetConnectionString();
                List<string> numeFirme = Interogari.GetAllNumeFurnizori(con);
                if (numeFirme != null)
                {
                    foreach (string numeFirma in numeFirme.Where(n => !string.IsNullOrWhiteSpace(n)))
                    {
                        comboBoxNumefirmaDelete.Items.Add(numeFirma);
                    }
                }
                else
                    MessageBox.Show("Eroare la selectarea furnizorilor.");
            }
            catch (Exception ex) { MessageBox.Show("Eroare la selectarea furnizorilor.\n" + ex.Message); return; }
        }

        private void buttonAdauga_Click( object sender, EventArgs e )
        {
            string iban = textBoxIBAN.Text;
            string nume = textBoxNumeFirma.Text;
            string judet = textBoxJudet.Text;
            string oras = textBoxOras.Text;
            string strada = textBoxStrada.Text;
            string email = textBoxEmailFirma.Text;
            string telefon = textBoxTelefon.Text;

            if (string.IsNullOrWhiteSpace(iban) || string.IsNullOrWhiteSpace(nume) ||
                  string.IsNullOrWhiteSpace(judet) || string.IsNullOrWhiteSpace(oras) ||
                  string.IsNullOrWhiteSpace(strada) || string.IsNullOrWhiteSpace(telefon))
            {
                MessageBox.Show("Toate câmpurile trebuie completate.");
                return;
            }

            if (!int.TryParse(textBoxNumar_Strada.Text, out int nr_strada) || nr_strada < 1)
            {
                MessageBox.Show("Numărul străzii trebuie să fie un număr valid și mai mare decat 0.");
                return;
            }

            try
            {
                string con = Aranjare.GetConnectionString();
                //int id_user1 = Interogari.GetUserIDbyEmail(con, email);
                bool success = Interogari.AdaugaFurnizor(con, nume, email, telefon, "ParolaFirmei", iban, judet, oras, strada, nr_strada);
                if (success)
                {
                    MessageBox.Show("Furnizorul a fost adăugat cu succes.");
                    Aranjare.ToateTextBoxurileledinPanelGoale(panelAdaugaFurnizor);
                }
               /* else
                {
                    MessageBox.Show("Nu s-a putut adăuga furnizorul.");
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare:\n" + ex.Message);
            }
        }

        private void buttonSterge_Click( object sender, EventArgs e )
        {
            string nume = comboBoxNumefirmaDelete.Text;
            string email = textBoxEmailDelete.Text;
            try
            {
                string con = Aranjare.GetConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        if (Interogari.StergeFurnizor(con, transaction, nume, email))
                        {
                            transaction.Commit();
                            comboBoxNumefirmaDelete.Items.Remove(nume);
                            MessageBox.Show("Furnizorul a fost șters cu succes.");
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show("Nu s-a putut șterge furnizorul.");
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Eroare. Nu se poate sterge furnizorul.\n" + ex.Message); }
        }

        private void buttonSignUpPanelMenu_Click(object sender, EventArgs e)
        {
            string iban = textBoxIBAN.Text;
            string nume = textBoxNumeFirma.Text;
            string judet = textBoxJudet.Text;
            string oras = textBoxOras.Text;
            string strada = textBoxStrada.Text;
            string email = textBoxEmailFirma.Text;
            string telefon = textBoxTelefon.Text;

            if (string.IsNullOrWhiteSpace(iban) || string.IsNullOrWhiteSpace(nume) ||
                  string.IsNullOrWhiteSpace(judet) || string.IsNullOrWhiteSpace(oras) ||
                  string.IsNullOrWhiteSpace(strada) || string.IsNullOrWhiteSpace(telefon))
            {
                MessageBox.Show("Toate câmpurile trebuie completate.");
                return;
            }

            if (!int.TryParse(textBoxNumar_Strada.Text, out int nr_strada) || nr_strada < 1)
            {
                MessageBox.Show("Numărul străzii trebuie să fie un număr valid și mai mare decat 0.");
                return;
            }

            try
            {
                string con = Aranjare.GetConnectionString();
                //int id_user1 = Interogari.GetUserIDbyEmail(con, email);
                bool success = Interogari.AdaugaFurnizor(con, nume, email, telefon, "ParolaFirmei", iban, judet, oras, strada, nr_strada);
                if (success)
                {
                    MessageBox.Show("Furnizorul a fost adăugat cu succes.");
                    Aranjare.ToateTextBoxurileledinPanelGoale(panelAdaugaFurnizor);
                }
                /* else
                 {
                     MessageBox.Show("Nu s-a putut adăuga furnizorul.");
                 }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare:\n" + ex.Message);
            }

        }

        private void buttonLoginPanelMenu_Click(object sender, EventArgs e)
        {
            string nume = comboBoxNumefirmaDelete.Text;
            string email = textBoxEmailDelete.Text;
            try
            {
                string con = Aranjare.GetConnectionString();
                using (SqlConnection connection = new SqlConnection(con))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        if (Interogari.StergeFurnizor(con, transaction, nume, email))
                        {
                            transaction.Commit();
                            comboBoxNumefirmaDelete.Items.Remove(nume);
                            MessageBox.Show("Furnizorul a fost șters cu succes.");
                        }
                        else
                        {
                            transaction.Rollback();
                            MessageBox.Show("Nu s-a putut șterge furnizorul.");
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Eroare. Nu se poate sterge furnizorul.\n" + ex.Message); }
        }
    }
}
