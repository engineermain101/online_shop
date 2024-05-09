using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace shop_online
{
    public partial class DetaliiProdus : Form
    {
        ProdusItem produs;
        public DetaliiProdus( ProdusItem produs )
        {
            InitializeComponent();
            this.produs = produs;
        }
        private void DetaliiProdus_FormClosed( object sender, FormClosedEventArgs e )
        {
            /* foreach (Form form in Application.OpenForms)
             {
                 if (form != this) // Excludem forma curentă pentru a nu o închide accidental
                 {
                     form.Close();
                 }
             }*/
            if (Application.OpenForms ["Afisare_Produse"] != null)
                Application.OpenForms ["Afisare_Produse"].Show();
        }
        private void DetaliiProdus_Load( object sender, EventArgs e )
        {
            LoadUser(produs);
        }

        public void LoadUser( ProdusItem produs )
        {
            this.produs = produs;
            listBoxImaginiProdus.Items.Clear();
            listViewImaginiProdus.Items.Clear();
            List<Image> imaginiProdus = produs.Image;
            if (imaginiProdus.Count > 0)
            {
                pictureBoxImagineProdus.Image = imaginiProdus [0];
            }

            foreach (Image imagine in imaginiProdus)
            {
                // Redimensionează imaginea la o dimensiune fixă pentru miniatură
                Image miniatura = new Bitmap(imagine, new Size(100, 100));

                // Adaugă miniatura în controlul ListBox sau ListView
                listBoxImaginiProdus.Items.Add(miniatura);
                //listViewImaginiProdus.Items.Add(imagine);
            }
            /* foreach (Image imagine in imaginiProdus)
             {
                 // Redimensionează imaginea la o dimensiune fixă pentru miniatură
                 Image miniatura = new Bitmap(imagine, new Size(100, 100));
                 ListViewItem item = new ListViewItem();
                 item.Content = new Image() { Source = ConvertBitmapToBitmapImage(miniatura) };

                 // Adaugă elementul în ListView
                 listViewImaginiProdus.Items.Add(item);
                 listBoxImaginiProdus.Items.Add(miniatura);
             }*/
        }

        private void listBoxImaginiProdus_SelectedIndexChanged( object sender, EventArgs e )
        {
            if (listBoxImaginiProdus.SelectedIndex >= 0)
            {
                pictureBoxImagineProdus.Image = (Image)listBoxImaginiProdus.SelectedItem;
            }
        }




    }
}
