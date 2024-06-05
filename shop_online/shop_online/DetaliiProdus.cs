using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;


namespace shop_online
{
    public partial class DetaliiProdus : KryptonForm
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
            List<Image> imaginiProdus = produs.Image;
            label3.Text = produs.Nume;
            label4.Text = produs.Pret.ToString();
            textBoxDescriere.Text=produs.Descriere;
            label6.Text= produs.Nota_Review.ToString();
            label10.Text = produs.Nota_Review.ToString();

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
            // Create an image with appropriate width to hold all the stars
            int starWidth = 20; // Width of each star
            int starHeight = 20; // Height of each star
            int imageWidth = nrStele * starWidth;
            int imageHeight = starHeight;

            Bitmap starImage = new Bitmap(imageWidth, imageHeight);
            using (Graphics g = Graphics.FromImage(starImage))
            {
                g.Clear(Color.White);

                for (int i = 0; i < nrStele; i++)
                {
                    // Define the points of a star
                    PointF[] starPoints = new PointF[]
                    {
                new PointF(10, 0),
                new PointF(12, 7),
                new PointF(20, 7),
                new PointF(14, 11),
                new PointF(16, 18),
                new PointF(10, 14),
                new PointF(4, 18),
                new PointF(6, 11),
                new PointF(0, 7),
                new PointF(8, 7)
                    };

                    // Translate the star points to the correct position
                    for (int j = 0; j < starPoints.Length; j++)
                    {
                        starPoints[j] = new PointF(starPoints[j].X + i * starWidth, starPoints[j].Y);
                    }

                    // Draw the star
                    g.FillPolygon(Brushes.Yellow, starPoints);
                    g.DrawPolygon(Pens.Black, starPoints); // Optional: add border to the star
                }
            }

            return starImage;
        }


        private void buttonAdaugaProdus_Click(object sender, EventArgs e)
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
            label10.Text = produs.Nota_Review.ToString();

            LoadReviews(produs.Id_Produs);
        }

       
    }
}
