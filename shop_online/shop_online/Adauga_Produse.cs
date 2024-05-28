using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace shop_online
{
    public partial class Adauga_Produse : Form
    {
        private int user_id_furnizor = -1;
        private List<Image> imagesList = new List<Image>();
        private List<string> fileNames = new List<string>();

        public Adauga_Produse()
        {
            InitializeComponent();
        }

        public Adauga_Produse( int id_furnizor )
        {
            InitializeComponent();
            user_id_furnizor = id_furnizor;
        }

        private void Adauga_Produse_FormClosed( object sender, FormClosedEventArgs e )
        {
            if (Application.OpenForms ["Afisare_Produse"] != null)
                Application.OpenForms ["Afisare_Produse"].Show();
        }
        private void Adauga_Produse_Load( object sender, System.EventArgs e )
        {
            AutoSize = true;
            Aranjare.CenteredPanel(this, panelAdaugaProdus);
            LoadUser(user_id_furnizor);

        }
        public void LoadUser( int id_furnizor )
        {

            user_id_furnizor = id_furnizor;
            //string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;
            try
            {
                string connectionString = Aranjare.GetConnectionString();
                List<string> categorii = Interogari.GetCategories(connectionString);
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

        }


        //Claudiu
        private void buttonAdaugaProdus_Click( object sender, EventArgs e )
        {
            string connectionString = Aranjare.GetConnectionString();
            string denumire = textBoxDenumire.Text.Trim();
            string descriere = textBoxDescriere.Text.Trim();
            int categorie = comboBoxCategorie.SelectedIndex;

            if (string.IsNullOrWhiteSpace(denumire))
            {
                MessageBox.Show("Vă rog introduceți o denumire validă!");
                return;
            }

            if (categorie == -1)
            {
                MessageBox.Show("Vă rog selectați o categorie");
                return;
            }

            if (string.IsNullOrWhiteSpace(descriere))
            {
                MessageBox.Show("Vă rog introduceți o descriere");
                return;
            }

            if (!decimal.TryParse(textBoxPret.Text, out decimal pret) || pret <= 0)
            {
                MessageBox.Show("Introduceți un preț valid (pozitiv)!");
                return;
            }

            if (!int.TryParse(textBoxCantitate.Text, out int cantitate) || cantitate <= 0)
            {
                MessageBox.Show("Introduceți o cantitate validă (pozitivă)!");
                return;
            }

            Image image = pictureBoxImagine.Image;
            string filename = pictureBoxImagine.ImageLocation;
            string extension = Path.GetExtension(filename);

            List<string> denumireS = new List<string>();
            List<string> valoareS = new List<string>();

            foreach (ListViewItem item in listView1.Items)
            {
                if (item.SubItems.Count > 1)
                {
                    denumireS.Add(item.SubItems [0].Text);
                    valoareS.Add(item.SubItems [1].Text);
                }
                else
                {
                    MessageBox.Show("Fiecare specificație trebuie să aibă atât o denumire cât și o valoare!");
                    return;
                }
            }
            if (denumireS.Count == 0 || valoareS.Count == 0)
            {
                MessageBox.Show("Listele de denumiri și valori nu trebuie să fie goale!");
                return;
            }
            //id categorie nu este id-ul din combobox. Este id-ul din baza de date
            ProdusItem produs = new ProdusItem(imagesList, denumire, pret, 0, 0, -1, cantitate, descriere, user_id_furnizor, categorie + 1);//trebuie sa faci o functie care ia Id-categorie pe baza cuvantului din combobox.

            try
            {
                Interogari.InsertProdus(connectionString, produs, fileNames, denumireS, valoareS);
                MessageBox.Show("Produs adăugat cu succes!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea produsului: " + ex.Message);
            }
        }


        private void listView1_SelectedIndexChanged( object sender, EventArgs e )
        {

        }

        private void button2_Click( object sender, EventArgs e )
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

        /* private void buttonAddImage_Click(object sender, EventArgs e)
         {
             OpenFileDialog openFileDialog = new OpenFileDialog();
             openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
             openFileDialog.Title = "Select an Image File";

             if (openFileDialog.ShowDialog() == DialogResult.OK)
             {
                 string selectedFilePath = openFileDialog.FileName;
                 pictureBoxImagine.ImageLocation = selectedFilePath;

             }
             int nr_imagini = int.Parse(labelCounter.Text);nr_imagini++;
             labelCounter.Text = nr_imagini.ToString();
             string filename = pictureBoxImagine.ImageLocation;
             string extension = Path.GetExtension(filename);
             imagesList.Add(pictureBoxImagine.Image);
             fileNames.Add(filename + extension);


         }*/
        private void buttonAddImage_Click( object sender, EventArgs e )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*",
                Title = "Select an Image File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                try
                {
                    // Verificăm dacă fișierul selectat este o imagine validă
                    using (Image image = Image.FromFile(selectedFilePath))
                    {
                        // Adăugăm imaginea doar dacă nu este deja în listă
                        if (!fileNames.Contains(selectedFilePath))
                        {
                            pictureBoxImagine.ImageLocation = selectedFilePath;

                            // Incrementăm contorul de imagini
                            if (int.TryParse(labelCounter.Text, out int nr_imagini))
                            {
                                nr_imagini++;
                                labelCounter.Text = nr_imagini.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Eroare la parsarea contorului de imagini.");
                                return;
                            }

                            // Adăugăm imaginea și numele fișierului în listele respective
                            imagesList.Add((Image)image.Clone());
                            fileNames.Add(selectedFilePath);
                        }
                        else
                        {
                            MessageBox.Show("Această imagine a fost deja adăugată.");
                        }
                    }
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("Fișierul selectat nu este o imagine validă.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la adăugarea imaginii: " + ex.Message);
                }
            }
        }
        //Horia
    }
}
