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
            labelTitle.Text = product.Title;
            labelPret.Text = product.Pret.ToString();
            labelStele.Text = "Nota recenzie: " + product.Nota_Review.ToString();
            labelNrRecenzii.Text = "(" + product.Nr_recenzii.ToString() + ")";
            labelinStoc.Text = "In stoc";
            labelBucati.Text = "(" + product.Cantitate + ")";
            numericUpDownBucati_Cos.Value = nr_bucati_in_cos;
        }
        private void buttonAdaugaCos_Click( object sender, System.EventArgs e )
        {
            try
            {
                int id_user = Afisare_Produse.GetUtilizatorID();
                string connectionString = Aranjare.GetConnectionString();
                Interogari.AdaugainCos(connectionString, 1, produs.Pret, id_user, produs.Id_produs);
            }
            catch (Exception) { MessageBox.Show("Nu s-a putut adauga in cos."); }
        }

        private void InitializeComponent()
        {
            this.buttonAdaugaCos = new System.Windows.Forms.Button();
            this.pictureBoxImagine = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelPret = new System.Windows.Forms.Label();
            this.labelStele = new System.Windows.Forms.Label();
            this.labelNrRecenzii = new System.Windows.Forms.Label();
            this.labelinStoc = new System.Windows.Forms.Label();
            this.labelBucati = new System.Windows.Forms.Label();
            this.numericUpDownBucati_Cos = new System.Windows.Forms.NumericUpDown();
            this.labelBucati_cos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBucati_Cos)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAdaugaCos
            // 
            this.buttonAdaugaCos.Location = new System.Drawing.Point(222, 96);
            this.buttonAdaugaCos.Name = "buttonAdaugaCos";
            this.buttonAdaugaCos.Size = new System.Drawing.Size(113, 40);
            this.buttonAdaugaCos.TabIndex = 0;
            this.buttonAdaugaCos.Text = "Adaugă in Coș";
            this.buttonAdaugaCos.UseVisualStyleBackColor = true;
            this.buttonAdaugaCos.Click += new System.EventHandler(this.buttonAdaugaCos_Click);
            // 
            // pictureBoxImagine
            // 
            this.pictureBoxImagine.Location = new System.Drawing.Point(358, 13);
            this.pictureBoxImagine.Name = "pictureBoxImagine";
            this.pictureBoxImagine.Padding = new System.Windows.Forms.Padding(10);
            this.pictureBoxImagine.Size = new System.Drawing.Size(144, 122);
            this.pictureBoxImagine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImagine.TabIndex = 1;
            this.pictureBoxImagine.TabStop = false;
            this.pictureBoxImagine.Click += new System.EventHandler(this.pictureBoxImagine_Click);
            this.pictureBoxImagine.DoubleClick += new System.EventHandler(this.pictureBoxImagine_DoubleClick);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(16, 13);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(35, 17);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Titlu";
            // 
            // labelPret
            // 
            this.labelPret.AutoSize = true;
            this.labelPret.Location = new System.Drawing.Point(16, 59);
            this.labelPret.Name = "labelPret";
            this.labelPret.Size = new System.Drawing.Size(34, 17);
            this.labelPret.TabIndex = 3;
            this.labelPret.Text = "Pret";
            // 
            // labelStele
            // 
            this.labelStele.AutoSize = true;
            this.labelStele.Location = new System.Drawing.Point(16, 108);
            this.labelStele.Name = "labelStele";
            this.labelStele.Size = new System.Drawing.Size(65, 17);
            this.labelStele.TabIndex = 4;
            this.labelStele.Text = "Nr stele: ";
            // 
            // labelNrRecenzii
            // 
            this.labelNrRecenzii.AutoSize = true;
            this.labelNrRecenzii.Location = new System.Drawing.Point(87, 108);
            this.labelNrRecenzii.Name = "labelNrRecenzii";
            this.labelNrRecenzii.Size = new System.Drawing.Size(18, 17);
            this.labelNrRecenzii.TabIndex = 5;
            this.labelNrRecenzii.Text = "()";
            // 
            // labelinStoc
            // 
            this.labelinStoc.AutoSize = true;
            this.labelinStoc.Location = new System.Drawing.Point(16, 85);
            this.labelinStoc.Name = "labelinStoc";
            this.labelinStoc.Size = new System.Drawing.Size(51, 17);
            this.labelinStoc.TabIndex = 6;
            this.labelinStoc.Text = "In Stoc";
            // 
            // labelBucati
            // 
            this.labelBucati.AutoSize = true;
            this.labelBucati.Location = new System.Drawing.Point(87, 85);
            this.labelBucati.Name = "labelBucati";
            this.labelBucati.Size = new System.Drawing.Size(18, 17);
            this.labelBucati.TabIndex = 7;
            this.labelBucati.Text = "()";
            // 
            // numericUpDownBucati_Cos
            // 
            this.numericUpDownBucati_Cos.Location = new System.Drawing.Point(222, 68);
            this.numericUpDownBucati_Cos.Name = "numericUpDownBucati_Cos";
            this.numericUpDownBucati_Cos.Size = new System.Drawing.Size(88, 22);
            this.numericUpDownBucati_Cos.TabIndex = 8;
            this.numericUpDownBucati_Cos.Visible = false;
            this.numericUpDownBucati_Cos.ValueChanged += new System.EventHandler(this.numericUpDownBucati_Cos_ValueChanged);
            // 
            // labelBucati_cos
            // 
            this.labelBucati_cos.AutoSize = true;
            this.labelBucati_cos.Location = new System.Drawing.Point(222, 45);
            this.labelBucati_cos.Name = "labelBucati_cos";
            this.labelBucati_cos.Size = new System.Drawing.Size(88, 17);
            this.labelBucati_cos.TabIndex = 9;
            this.labelBucati_cos.Text = "Bucati in cos";
            this.labelBucati_cos.Visible = false;
            // 
            // ProductControl
            // 
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.Controls.Add(this.labelBucati_cos);
            this.Controls.Add(this.numericUpDownBucati_Cos);
            this.Controls.Add(this.labelBucati);
            this.Controls.Add(this.labelinStoc);
            this.Controls.Add(this.labelNrRecenzii);
            this.Controls.Add(this.labelStele);
            this.Controls.Add(this.labelPret);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBoxImagine);
            this.Controls.Add(this.buttonAdaugaCos);
            this.Name = "ProductControl";
            this.Size = new System.Drawing.Size(505, 138);
            this.Load += new System.EventHandler(this.ProductControl_Load);
            this.Click += new System.EventHandler(this.ProductControl_Click);
            this.DoubleClick += new System.EventHandler(this.ProductControl_DoubleClick);
            this.Leave += new System.EventHandler(this.ProductControl_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBucati_Cos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
            CloseCurrentFormAndOpenNewFormAsync(produs);
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
            CloseCurrentFormAndOpenNewFormAsync(produs);
        }


        public int GetProdus_ID()
        {
            return produs.Id_produs;
        }
        public decimal GetProdus_Pret()
        {
            return produs.Pret;
        }
        public int GetCantitate()
        {
            return produs.Cantitate;
        }
        public int GetNrBucatiCos()
        {
            return nr_bucati_in_cos;
        }
        public Color GetSelectedColor()
        {
            return selectedColor;
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
