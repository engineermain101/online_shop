using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace shop_online
{
    public partial class DetaliiProdus : Form
    {
        private ProdusItem produs;
        public DetaliiProdus(ProdusItem produs)
        {
            InitializeComponent();
            this.produs = produs;
        }
        private void DetaliiProdus_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (Application.OpenForms["Afisare_Produse"] != null)
                Application.OpenForms["Afisare_Produse"].Show();
        }
        private void DetaliiProdus_Load(object sender, EventArgs e)
        {
            LoadUser(produs);
            LoadReviews(produs.Id_Produs);
        }

        public void LoadUser(ProdusItem produs)
        {
            this.produs = produs;
            listBoxImaginiProdus.Items.Clear();
            listViewImaginiProdus.Items.Clear();
            List<Image> imaginiProdus = produs.Image;
            if (imaginiProdus.Count > 0)
            {
                pictureBoxImagineProdus.Image = imaginiProdus[0];
            }

            foreach (Image imagine in imaginiProdus)
            {
                // Redimensionează imaginea la o dimensiune fixă pentru miniatură
                Image miniatura = new Bitmap(imagine, new Size(100, 100));

                // Adaugă miniatura în controlul ListBox sau ListView
                listBoxImaginiProdus.Items.Add(miniatura);
                //listViewImaginiProdus.Items.Add(imagine);
            }

            string connect = null;
            try
            {
                connect = Aranjare.GetConnectionString();
                DataTable specificatiiTable = Interogari.GetSpecificatii(connect, produs.Id_Produs);
                DataTable reviewTable = Interogari.GetRecenzii(connect, produs.Id_Produs);

                foreach (DataRow row in specificatiiTable.Rows)
                {
                    dataGridViewSpecificatii.Rows.Add(row.ItemArray);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return; };

        }
        public void LoadReviews(int produs_id)
        {
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception)
            {
                return;
            }
            List<Dictionary<string, object>> reviews;
            try
            {
                reviews=Interogari.GetReviews(connectionString, produs_id);
            }
            catch (Exception e)
            {
                MessageBox.Show(e + "Nu s-au putut afisa reviewsurile");
                return;
            }
            DisplayReviews(flowLayoutPanel1,reviews);

        }

        private void listBoxImaginiProdus_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (listBoxImaginiProdus.SelectedIndex >= 0)
            {
                pictureBoxImagineProdus.Image = (Image)listBoxImaginiProdus.SelectedItem;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string recenzie = textBoxRecenzie.Text;
            if (string.IsNullOrEmpty(recenzie))
            {
                MessageBox.Show("Va rog introduceti un comentariu!");
            }
            DateTime date = DateTime.Now;
            int nr_stele = trackBarStele.Value + 1;
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception)
            {
                return;
            }
            int id_user = Afisare_Produse.GetCurrentUserId();

            Interogari.AdaugaRecenzie(connectionString, id_user, produs.Id_Produs, recenzie, nr_stele, date);
            textBoxRecenzie.Clear();
            LoadReviews(produs.Id_Produs);
        }
        private static void DisplayReviews(FlowLayoutPanel flowLayoutPanel, List<Dictionary<string, object>> reviews)
        {
            flowLayoutPanel.Controls.Clear();

            foreach (var review in reviews)
            {
                Panel reviewPanel = new Panel
                {
                    Width = flowLayoutPanel.Width - 25, // Adjust width as needed
                    Height = 100, // Adjust height as needed
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(5)
                };

                Label lblUser = new Label
                {
                    Text = $"User ID: {review["UserId"]}",
                    AutoSize = true,
                    Location = new Point(10, 10)
                };

                Label lblComentariu = new Label
                {
                    Text = review["Comentariu"].ToString(),
                    AutoSize = true,
                    Location = new Point(10, 30)
                };

                PictureBox pbStars = new PictureBox
                {
                    Location = new Point(10, 60),
                    Size = new Size(100, 20), // Adjust size as needed
                    Image = GetStarRatingImage((int)review["NrStele"]) // Implement this method to return an image representing the star rating
                };

                Label lblDate = new Label
                {
                    Text = ((DateTime)review["Data"]).ToString("d MMM yyyy"),
                    AutoSize = true,
                    Location = new Point(120, 60)
                };

                reviewPanel.Controls.Add(lblUser);
                reviewPanel.Controls.Add(lblComentariu);
                reviewPanel.Controls.Add(pbStars);
                reviewPanel.Controls.Add(lblDate);

                flowLayoutPanel.Controls.Add(reviewPanel);
            }
        }

        private static Image GetStarRatingImage(int nrStele)
        {
            // Create and return an image based on the star rating (nrStele)
            // This is a placeholder implementation. Replace it with actual logic to generate or fetch star rating images.
            Bitmap starImage = new Bitmap(100, 20);
            using (Graphics g = Graphics.FromImage(starImage))
            {
                g.Clear(Color.White);
                for (int i = 0; i < nrStele; i++)
                {
                    g.FillRectangle(Brushes.Yellow, i * 20, 0, 18, 18); // Draw a simple rectangle to represent a star
                }
            }
            return starImage;
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
