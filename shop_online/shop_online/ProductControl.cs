/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System.Drawing;
using System.Windows.Forms;
namespace shop_online
{
    public class ProductControl : UserControl
    {
        readonly ProdusItem produs;
        private Button buttonAdaugaCos;
        private PictureBox pictureBoxImagine;
        private Label labelTitle;
        private Label labelPret;
        private Label labelStele;
        private Label labelNrRecenzii;
        private Label labelinStoc;
        private Label labelBucati;
        private DetaliiProdus detaliiProdus = null;// Form nou
        private int nr_bucati;
        private decimal pret_total;

        public ProductControl( ProdusItem product )
        {
            InitializeComponent();
            SetProductInfo(product);
            produs = product;
        }
        public ProductControl( ProdusItem product, bool visible )
        {
            InitializeComponent();
            produs = product;
            buttonAdaugaCos.Visible = visible;
            SetProductInfo(product);
        }
        public ProductControl( ProdusItem product, bool visible, bool bucatiVisible, int nr_bucati, decimal pret_total )
        {
            InitializeComponent();
            produs = product;
            buttonAdaugaCos.Visible = visible;
            labelBucati.Visible = bucatiVisible;
            this.nr_bucati = nr_bucati;
            this.pret_total = pret_total;
            SetProductInfo(product);
        }
        private void SetProductInfo( ProdusItem product )
        {
            pictureBoxImagine.Image = product.Image [0];
            labelTitle.Text = product.Title;
            labelPret.Text = product.Pret.ToString();
            labelStele.Text = "Nota recenzie: " + product.Nota_Review.ToString();
            labelNrRecenzii.Text = "(" + product.Nr_recenzii.ToString() + ")";
            labelinStoc.Text = "In stoc";
        }
        private void buttonAdaugaCos_Click( object sender, System.EventArgs e )
        {
            int id_user = Afisare_Produse.GetUtilizatorID();
            string connectionString = Aranjare.GetConnectionString();
            Interogari.AdaugainCos(connectionString, 1, produs.Pret, id_user, produs.Id_produs);
        }

        private void InitializeComponent()
        {
            buttonAdaugaCos = new System.Windows.Forms.Button();
            pictureBoxImagine = new System.Windows.Forms.PictureBox();
            labelTitle = new System.Windows.Forms.Label();
            labelPret = new System.Windows.Forms.Label();
            labelStele = new System.Windows.Forms.Label();
            labelNrRecenzii = new System.Windows.Forms.Label();
            labelinStoc = new System.Windows.Forms.Label();
            labelBucati = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxImagine)).BeginInit();
            SuspendLayout();
            // 
            // buttonAdaugaCos
            // 
            buttonAdaugaCos.Location = new System.Drawing.Point(222, 96);
            buttonAdaugaCos.Name = "buttonAdaugaCos";
            buttonAdaugaCos.Size = new System.Drawing.Size(113, 40);
            buttonAdaugaCos.TabIndex = 0;
            buttonAdaugaCos.Text = "Adaugă in Coș";
            buttonAdaugaCos.UseVisualStyleBackColor = true;
            buttonAdaugaCos.Click += new System.EventHandler(buttonAdaugaCos_Click);
            // 
            // pictureBoxImagine
            // 
            pictureBoxImagine.Location = new System.Drawing.Point(358, 13);
            pictureBoxImagine.Name = "pictureBoxImagine";
            pictureBoxImagine.Padding = new System.Windows.Forms.Padding(10);
            pictureBoxImagine.Size = new System.Drawing.Size(144, 122);
            pictureBoxImagine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxImagine.TabIndex = 1;
            pictureBoxImagine.TabStop = false;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Location = new System.Drawing.Point(16, 13);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new System.Drawing.Size(35, 17);
            labelTitle.TabIndex = 2;
            labelTitle.Text = "Titlu";
            // 
            // labelPret
            // 
            labelPret.AutoSize = true;
            labelPret.Location = new System.Drawing.Point(16, 59);
            labelPret.Name = "labelPret";
            labelPret.Size = new System.Drawing.Size(34, 17);
            labelPret.TabIndex = 3;
            labelPret.Text = "Pret";
            // 
            // labelStele
            // 
            labelStele.AutoSize = true;
            labelStele.Location = new System.Drawing.Point(130, 59);
            labelStele.Name = "labelStele";
            labelStele.Size = new System.Drawing.Size(65, 17);
            labelStele.TabIndex = 4;
            labelStele.Text = "Nr stele: ";
            // 
            // labelNrRecenzii
            // 
            labelNrRecenzii.AutoSize = true;
            labelNrRecenzii.Location = new System.Drawing.Point(130, 85);
            labelNrRecenzii.Name = "labelNrRecenzii";
            labelNrRecenzii.Size = new System.Drawing.Size(18, 17);
            labelNrRecenzii.TabIndex = 5;
            labelNrRecenzii.Text = "()";
            // 
            // labelinStoc
            // 
            labelinStoc.AutoSize = true;
            labelinStoc.Location = new System.Drawing.Point(16, 108);
            labelinStoc.Name = "labelinStoc";
            labelinStoc.Size = new System.Drawing.Size(55, 17);
            labelinStoc.TabIndex = 6;
            labelinStoc.Text = "In Stoc:";
            // 
            // labelBucati
            // 
            labelBucati.AutoSize = true;
            labelBucati.Location = new System.Drawing.Point(84, 108);
            labelBucati.Name = "labelBucati";
            labelBucati.Size = new System.Drawing.Size(60, 17);
            labelBucati.TabIndex = 7;
            labelBucati.Text = "() bucati";
            // 
            // ProductControl
            // 
            BackColor = System.Drawing.Color.OrangeRed;
            Controls.Add(labelBucati);
            Controls.Add(labelinStoc);
            Controls.Add(labelNrRecenzii);
            Controls.Add(labelStele);
            Controls.Add(labelPret);
            Controls.Add(labelTitle);
            Controls.Add(pictureBoxImagine);
            Controls.Add(buttonAdaugaCos);
            Name = "ProductControl";
            Size = new System.Drawing.Size(505, 138);
            Load += new System.EventHandler(ProductControl_Load);
            Click += new System.EventHandler(ProductControl_Click);
            DoubleClick += new System.EventHandler(ProductControl_DoubleClick);
            Leave += new System.EventHandler(ProductControl_Leave);
            ((System.ComponentModel.ISupportInitialize)(pictureBoxImagine)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        private void ProductControl_Load( object sender, System.EventArgs e )
        {
            ProductControl selectedProductControl = sender as ProductControl;
            selectedProductControl.BackColor = Color.OrangeRed;
        }
        private void ProductControl_Click( object sender, System.EventArgs e )
        {
            ProductControl selectedProductControl = sender as ProductControl;
            // Evidențiază selecția schimbând aspectul ProductControl-ului, de exemplu schimbând culoarea fundalului sau grosimea marginilor
            selectedProductControl.BackColor = Color.LightGray; // Exemplu de schimbare a culorii fundalului pentru evidențiere

        }
        private void ProductControl_DoubleClick( object sender, System.EventArgs e )
        {
            CloseCurrentFormAndOpenNewFormAsync(produs);
        }
        private void ProductControl_Leave( object sender, System.EventArgs e )
        {
            ProductControl selectedProductControl = sender as ProductControl;
            // Evidențiază selecția schimbând aspectul ProductControl-ului, de exemplu schimbând culoarea fundalului sau grosimea marginilor
            selectedProductControl.BackColor = Color.OrangeRed; // Exemplu de schimbare a culorii fundalului pentru evidențiere

        }
        public int GetProdus_ID()
        {
            return produs.Id_produs;
        }
        public decimal GetProdus_Pret()
        {
            return produs.Pret;
        }


        public void ButtonVisible( bool visible )
        {
            buttonAdaugaCos.Visible = visible;
        }

        private void CloseCurrentFormAndOpenNewFormAsync( ProdusItem produss )
        {
            if (detaliiProdus == null)
            {
                detaliiProdus = new DetaliiProdus(produss)
                {
                    MinimumSize = new Size(490, 535)
                };
                detaliiProdus.Size = detaliiProdus.MinimumSize;
                detaliiProdus.FormClosed += ( sender, e ) => { detaliiProdus = null; }; // Resetare referință când formularul este închis
            }

            if (!detaliiProdus.Visible)
            {
                detaliiProdus.Visible = true;
                if (Application.OpenForms ["Afisare_Produse"] != null)
                {
                    Application.OpenForms ["Afisare_Produse"].Hide();
                }
            }
            detaliiProdus.LoadUser(produss);
            detaliiProdus.Show();
            detaliiProdus.Focus();
        }

    }

}
