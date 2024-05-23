using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace shop_online
{
    public partial class Adauga_Produse : Form
    {
        private int user_id_furnizor = -1;

        public Adauga_Produse()
        {
            InitializeComponent();
        }

        public Adauga_Produse(int id_furnizor)
        {
            InitializeComponent();
            user_id_furnizor = id_furnizor;
        }

        private void Adauga_Produse_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Afisare_Produse"] != null)
                Application.OpenForms["Afisare_Produse"].Show();
        }
        private void Adauga_Produse_Load(object sender, System.EventArgs e)
        {
            this.AutoSize = true;
            Aranjare.CenteredPanel(this, panelAdaugaProdus);
            LoadUser(user_id_furnizor);

        }
        public void LoadUser(int id_furnizor)
        {

            user_id_furnizor = id_furnizor;
            //string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;
            try
            {
                string connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }




        //Claudiu
        private void buttonAdaugaProdus_Click(object sender, System.EventArgs e)
        {

            string connectionString = Aranjare.GetConnectionString();
            string denumire = textBoxDenumire.Text.ToString();
            if (denumire.Length == 0) { MessageBox.Show("Va rog introduceti o denumire valida!"); return; }

            if (comboBoxCategorie.SelectedIndex == -1) { MessageBox.Show("Va rog selectati o categorie"); return; }
            string categorie = comboBoxCategorie.Text.ToString();

            string descriere = textBoxDescriere.Text.ToString();
            if (descriere.Length == 0) { MessageBox.Show("Va rog introduceti o descriere"); return; }

            Image image = pictureBoxImagine.Image;
            string filename = pictureBoxImagine.ImageLocation;
            string extension = Path.GetExtension(filename);


            List<string> denumireS = new List<string>();
            List<string> valoareS = new List<string>();

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems.Count > 0)
                {
                    denumireS.Add(item.SubItems[0].Text);
                }
                else { MessageBox.Show("Adaugati o denumire de specificatie!"); }
            }
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems.Count > 0)
                {
                    valoareS.Add(item.SubItems[1].Text);
                }
                else { MessageBox.Show("Adaugati o valoare de specificatie!"); }
            }


            double pret = double.Parse(textBoxPret.Text);
            if (pret <= 0) { MessageBox.Show("Pret negativ sau 0"); return; }


            int cantitate = int.Parse(textBoxCantitate.Text);
            if (cantitate <= 0) { MessageBox.Show("Cantitate negativa sau 0"); return; }


            // Interogari.InsertProdus(connectionString, image, denumire, cantitate, pret, descriere, denumireS, valoareS, comboBoxCategorie.SelectedIndex, filename + extension, user_id_furnizor);


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string denumire = textBoxDenumireSpecificatie.Text;
            string specificatie = textBoxValoareSpecificatie.Text;
            if (string.IsNullOrWhiteSpace(denumire) || string.IsNullOrWhiteSpace(specificatie))
            {
                MessageBox.Show("Introduceti o denumire si o specificate!");
                return;
            }

            ListViewItem listViewItem = new ListViewItem(denumire);
            listViewItem.SubItems.Add(specificatie);

            listView1.Items.Add(listViewItem);
            textBoxDenumireSpecificatie.Clear();
            textBoxValoareSpecificatie.Clear();
            listView1.View = View.Details;

        }

        //Horia
    }
}
