using System;
using System.Drawing;
using System.Windows.Forms;

namespace shop_online
{
    public partial class Adauga_Furnizor : Form
    {
        private int id_user;
        public Adauga_Furnizor()
        {
            InitializeComponent();
        }
        public Adauga_Furnizor( int Id_user )
        {
            InitializeComponent();
            id_user = Id_user;
        }


        private void Adauga_Furnizor_FormClosed( object sender, FormClosedEventArgs e )
        {
            if (Application.OpenForms ["Afisare_Produse"] != null)
                Application.OpenForms ["Afisare_Produse"].Show();
        }
        private void Adauga_Furnizor_Load( object sender, EventArgs e )
        {
            Aranjare.CenteredPanel(this, panelFurnizor);
            MinimumSize = panelFurnizor.Size + new Size(50, 50);
        }

        private void buttonAdauga_Click( object sender, EventArgs e )
        {
            string iban = textBoxIBAN.Text;
            string nume = textBoxNumeFirma.Text;
            string judet = textBoxJudet.Text;
            string oras = textBoxOras.Text;
            string strada = textBoxStrada.Text;

            if (!int.TryParse(labelNumar_Strada.Text, out int nr_strada) || nr_strada < 1)
            {
                MessageBox.Show("Numărul străzii trebuie să fie un număr valid și mai mare de 0.");
                return;
            }

            if (string.IsNullOrWhiteSpace(iban) || string.IsNullOrWhiteSpace(nume) ||
                string.IsNullOrWhiteSpace(judet) || string.IsNullOrWhiteSpace(oras) ||
                string.IsNullOrWhiteSpace(strada) || nr_strada < 1)
            {
                MessageBox.Show("Toate câmpurile trebuie completate.");
                return;
            }
            try
            {
                string con = Aranjare.GetConnectionString();
                bool success = Interogari.AdaugaFurnizor(con, id_user, iban, nume, judet, oras, strada, nr_strada);

                if (success)
                {
                    MessageBox.Show("Furnizorul a fost adăugat cu succes.");
                }
                else
                {
                    MessageBox.Show("Nu s-a putut adăuga furnizorul.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare:\n" + ex.Message);
            }
        }
    }
}
