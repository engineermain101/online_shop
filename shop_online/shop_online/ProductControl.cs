using System;
using System.Drawing;
using System.Windows.Forms;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
namespace shop_online
{
    public class ProductControl : UserControl
    {
        readonly ProdusItem produs = null;
        private Button buttonAdaugaCos;
        private PictureBox pictureBoxImagine;
        private Label labelTitle;
        private Label labelPret;
        private Label labelStele;
        private Label labelNrRecenzii;
        private Label labelinStoc;
        private Label labelBucati;
        private NumericUpDown numericUpDownBucati_Cos;
        private Label labelBucati_cos;
        private DetaliiProdus detaliiProdus = null;// Form nou
        private int nr_bucati_in_cos = 0;
        private decimal pret_total_cos = -1;
        private readonly Color defaultColor = Color.OrangeRed;
        private readonly Color selectedColor = Color.LightGray;

        public ProductControl()
        {
            InitializeComponent();
            //SetProductInfo(product);
            labelBucati_cos.Hide();
            numericUpDownBucati_Cos.Hide();
        }
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
        public ProductControl( ProdusItem product, bool visible, int nr_bucati_in_cos, decimal pret_total_cos )
        {
            InitializeComponent();
            produs = product;
            buttonAdaugaCos.Visible = visible;
            labelBucati_cos.Visible = !visible;
            numericUpDownBucati_Cos.Visible = !visible;
            this.nr_bucati_in_cos = nr_bucati_in_cos;
            this.pret_total_cos = pret_total_cos;
            SetProductInfo(product);
        }
        private void SetProductInfo( ProdusItem product )
        {
            pictureBoxImagine.Image = product.Image [0];
            labelTitle.Text = product.Nume;
            labelPret.Text = product.Pret.ToString();
            labelStele.Text = "Nota recenzie: " + product.Nota_Review.ToString();
            labelNrRecenzii.Text = "(" + product.Nr_recenzii.ToString() + ")";
            labelinStoc.Text = "In stoc";
            labelBucati.Text = "(" + product.Cantitate + ")";
            if (nr_bucati_in_cos > product.Cantitate)
                nr_bucati_in_cos = product.Cantitate;
            numericUpDownBucati_Cos.Maximum = product.Cantitate;
            numericUpDownBucati_Cos.Value = nr_bucati_in_cos;
        }
        private void buttonAdaugaCos_Click( object sender, System.EventArgs e )
        {
            try
            {
                int id_user = Afisare_Produse.GetUtilizatorID();
                string connectionString = Aranjare.GetConnectionString();
                Interogari.AdaugainCos(connectionString, nr_bucati_in_cos + 1, produs.Pret, id_user, produs.Id_Produs);
            }
            catch (Exception) { MessageBox.Show("Nu s-a putut adauga in cos."); }
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
            numericUpDownBucati_Cos = new System.Windows.Forms.NumericUpDown();
            labelBucati_cos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxImagine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownBucati_Cos)).BeginInit();
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
            pictureBoxImagine.Click += new System.EventHandler(pictureBoxImagine_Click);
            pictureBoxImagine.DoubleClick += new System.EventHandler(pictureBoxImagine_DoubleClick);
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
            labelStele.Location = new System.Drawing.Point(16, 108);
            labelStele.Name = "labelStele";
            labelStele.Size = new System.Drawing.Size(65, 17);
            labelStele.TabIndex = 4;
            labelStele.Text = "Nr stele: ";
            // 
            // labelNrRecenzii
            // 
            labelNrRecenzii.AutoSize = true;
            labelNrRecenzii.Location = new System.Drawing.Point(87, 108);
            labelNrRecenzii.Name = "labelNrRecenzii";
            labelNrRecenzii.Size = new System.Drawing.Size(18, 17);
            labelNrRecenzii.TabIndex = 5;
            labelNrRecenzii.Text = "()";
            // 
            // labelinStoc
            // 
            labelinStoc.AutoSize = true;
            labelinStoc.Location = new System.Drawing.Point(16, 85);
            labelinStoc.Name = "labelinStoc";
            labelinStoc.Size = new System.Drawing.Size(51, 17);
            labelinStoc.TabIndex = 6;
            labelinStoc.Text = "In Stoc";
            // 
            // labelBucati
            // 
            labelBucati.AutoSize = true;
            labelBucati.Location = new System.Drawing.Point(87, 85);
            labelBucati.Name = "labelBucati";
            labelBucati.Size = new System.Drawing.Size(18, 17);
            labelBucati.TabIndex = 7;
            labelBucati.Text = "()";
            // 
            // numericUpDownBucati_Cos
            // 
            numericUpDownBucati_Cos.Location = new System.Drawing.Point(222, 68);
            numericUpDownBucati_Cos.Name = "numericUpDownBucati_Cos";
            numericUpDownBucati_Cos.Size = new System.Drawing.Size(88, 22);
            numericUpDownBucati_Cos.TabIndex = 8;
            numericUpDownBucati_Cos.Visible = false;
            numericUpDownBucati_Cos.ValueChanged += new System.EventHandler(numericUpDownBucati_Cos_ValueChanged);
            // 
            // labelBucati_cos
            // 
            labelBucati_cos.AutoSize = true;
            labelBucati_cos.Location = new System.Drawing.Point(222, 45);
            labelBucati_cos.Name = "labelBucati_cos";
            labelBucati_cos.Size = new System.Drawing.Size(88, 17);
            labelBucati_cos.TabIndex = 9;
            labelBucati_cos.Text = "Bucati in cos";
            labelBucati_cos.Visible = false;
            // 
            // ProductControl
            // 
            BackColor = System.Drawing.Color.OrangeRed;
            Controls.Add(labelBucati_cos);
            Controls.Add(numericUpDownBucati_Cos);
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
            ((System.ComponentModel.ISupportInitialize)(numericUpDownBucati_Cos)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }
        private void ProductControl_Load( object sender, System.EventArgs e )
        {
            //ProductControl selectedProductControl = sender as ProductControl;
            //selectedProductControl.BackColor = defaultColor;
            ResetBackColor();
        }
        private void ProductControl_Click( object sender, System.EventArgs e )
        {
            //ProductControl selectedProductControl = sender as ProductControl;
            // Evidențiază selecția schimbând aspectul ProductControl-ului, de exemplu schimbând culoarea fundalului sau grosimea marginilor
            // Exemplu de schimbare a culorii fundalului pentru evidențiere

            Aranjare.ResetFlowLayoutPanelProduse("Afisare_Produse");
            Aranjare.ResetFlowLayoutPanelProduse("Cos");
            SetBackColor(selectedColor);

        }
        private void ProductControl_DoubleClick( object sender, System.EventArgs e )
        {
            Size MinimumSize = new Size(920, 635);
            Aranjare.HideCurrentFormAndOpenNewForm(FindForm(), detaliiProdus, produs, MinimumSize);
        }
        private void ProductControl_Leave( object sender, System.EventArgs e )
        {
            // ResetBackColor();
        }
        private void pictureBoxImagine_Click( object sender, System.EventArgs e )
        {
            Aranjare.ResetFlowLayoutPanelProduse("Afisare_Produse");
            Aranjare.ResetFlowLayoutPanelProduse("Cos");
            SetBackColor(selectedColor);
        }
        private void pictureBoxImagine_DoubleClick( object sender, System.EventArgs e )
        {
            Size MinimumSize = new Size(920, 635);
            Aranjare.HideCurrentFormAndOpenNewForm(FindForm(), detaliiProdus, produs, MinimumSize);
        }
        private void numericUpDownBucati_Cos_ValueChanged( object sender, EventArgs e )
        {
            if (!(Application.OpenForms ["Cos"] is Cos cosForm))
                return;

            int nouaValoareBucati = Convert.ToInt32(numericUpDownBucati_Cos.Value);
            int diferentaBucati = nouaValoareBucati - nr_bucati_in_cos;
            nr_bucati_in_cos = nouaValoareBucati;
            pret_total_cos = nr_bucati_in_cos * produs.Pret;

            // Actualizează textul din labelPretTotal în formularul "Cos"
            if (cosForm.Controls ["labelPretTotal"] is Label labelPretTotal && labelPretTotal.Text != "Pret total: ")
            {
                decimal pretTotalDecimal = Convert.ToDecimal(labelPretTotal.Text.Replace("Pret total: ", "").Replace(" lei", ""));
                pretTotalDecimal += diferentaBucati * produs.Pret;
                labelPretTotal.Text = "Pret total: " + pretTotalDecimal + " lei";
            }
        }



        public ProdusItem GetProdus()
        {
            return produs;
        }
        public int GetProdus_ID()
        {
            return produs.Id_Produs;
        }
        public decimal GetProdus_Pret()
        {
            return produs.Pret;
        }
        public int GetNrBucatiCos()
        {
            return nr_bucati_in_cos;
        }
        public Color GetSelectedColor()
        {
            return selectedColor;
        }
        public int GetBucatiProdusdinCos()
        {
            return Convert.ToInt32(numericUpDownBucati_Cos.Value);
        }
        public decimal GetPretTotalCos()
        {
            return pret_total_cos;
        }

        public void SetBackColor( Color culoare )
        {
            BackColor = culoare;
        }
        override public void ResetBackColor()
        {
            BackColor = defaultColor;
        }
        public void ButtonVisible( bool visible )
        {
            buttonAdaugaCos.Visible = visible;
        }
        public void SetLabel_and_NumericVisible( bool vis )
        {
            labelBucati_cos.Visible = vis;
            numericUpDownBucati_Cos.Visible = vis;
        }


        /*private void CloseCurrentFormAndOpenNewFormAsync( ProdusItem produss )
        {
            if (detaliiProdus == null)
            {
                detaliiProdus = new DetaliiProdus(produss)
                {
                    MinimumSize = new Size(920, 635)
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
        */



        /*public void SetBackColor( ProductControl selectedProductControl, Color culoare )
             {
                 selectedProductControl.BackColor = culoare;
             }

         private void ResetFlowLayoutPanelProduse( string formName )
         {
             if (Application.OpenForms [formName] is Form form)
             {
                 if (form is Afisare_Produse || form is Cos)
                 {
                     (form as dynamic).ResetFlowLayoutPanelProduse();
                 }
             }
         }*/
    }

}
