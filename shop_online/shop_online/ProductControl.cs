using System;
using System.Drawing;
using System.Windows.Forms;
/*using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using ComponentFactory.Krypton.Toolkit;

namespace shop_online
{
    public class ProductControl : UserControl
    {
        readonly ProdusItem produs = null;
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
        private readonly Color defaultColor = Color.DarkSalmon;
        private Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton buttonAdaugaCos;
        private System.ComponentModel.IContainer components;
        private readonly Color selectedColor = System.Drawing.SystemColors.ControlDark;

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
        

        private void InitializeComponent()
        {
            this.pictureBoxImagine = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelPret = new System.Windows.Forms.Label();
            this.labelStele = new System.Windows.Forms.Label();
            this.labelNrRecenzii = new System.Windows.Forms.Label();
            this.labelinStoc = new System.Windows.Forms.Label();
            this.labelBucati = new System.Windows.Forms.Label();
            this.numericUpDownBucati_Cos = new System.Windows.Forms.NumericUpDown();
            this.labelBucati_cos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAdaugaCos = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBucati_Cos)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxImagine
            // 
            this.pictureBoxImagine.Location = new System.Drawing.Point(358, 3);
            this.pictureBoxImagine.Name = "pictureBoxImagine";
            this.pictureBoxImagine.Padding = new System.Windows.Forms.Padding(10);
            this.pictureBoxImagine.Size = new System.Drawing.Size(144, 132);
            this.pictureBoxImagine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImagine.TabIndex = 1;
            this.pictureBoxImagine.TabStop = false;
            this.pictureBoxImagine.Click += new System.EventHandler(this.pictureBoxImagine_Click);
            this.pictureBoxImagine.DoubleClick += new System.EventHandler(this.pictureBoxImagine_DoubleClick);
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.Color.Black;
            this.labelTitle.Location = new System.Drawing.Point(16, 13);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(49, 25);
            this.labelTitle.TabIndex = 2;
            this.labelTitle.Text = "Titlu";
            // 
            // labelPret
            // 
            this.labelPret.AutoSize = true;
            this.labelPret.ForeColor = System.Drawing.Color.Black;
            this.labelPret.Location = new System.Drawing.Point(18, 58);
            this.labelPret.Name = "labelPret";
            this.labelPret.Size = new System.Drawing.Size(31, 16);
            this.labelPret.TabIndex = 3;
            this.labelPret.Text = "Pret";
            // 
            // labelStele
            // 
            this.labelStele.AutoSize = true;
            this.labelStele.ForeColor = System.Drawing.Color.Black;
            this.labelStele.Location = new System.Drawing.Point(16, 108);
            this.labelStele.Name = "labelStele";
            this.labelStele.Size = new System.Drawing.Size(59, 16);
            this.labelStele.TabIndex = 4;
            this.labelStele.Text = "Nr stele: ";
            // 
            // labelNrRecenzii
            // 
            this.labelNrRecenzii.AutoSize = true;
            this.labelNrRecenzii.ForeColor = System.Drawing.Color.Black;
            this.labelNrRecenzii.Location = new System.Drawing.Point(87, 108);
            this.labelNrRecenzii.Name = "labelNrRecenzii";
            this.labelNrRecenzii.Size = new System.Drawing.Size(15, 16);
            this.labelNrRecenzii.TabIndex = 5;
            this.labelNrRecenzii.Text = "()";
            // 
            // labelinStoc
            // 
            this.labelinStoc.AutoSize = true;
            this.labelinStoc.ForeColor = System.Drawing.Color.Black;
            this.labelinStoc.Location = new System.Drawing.Point(16, 85);
            this.labelinStoc.Name = "labelinStoc";
            this.labelinStoc.Size = new System.Drawing.Size(47, 16);
            this.labelinStoc.TabIndex = 6;
            this.labelinStoc.Text = "In Stoc";
            // 
            // labelBucati
            // 
            this.labelBucati.AutoSize = true;
            this.labelBucati.ForeColor = System.Drawing.Color.Black;
            this.labelBucati.Location = new System.Drawing.Point(87, 85);
            this.labelBucati.Name = "labelBucati";
            this.labelBucati.Size = new System.Drawing.Size(15, 16);
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
            this.labelBucati_cos.Size = new System.Drawing.Size(82, 16);
            this.labelBucati_cos.TabIndex = 9;
            this.labelBucati_cos.Text = "Bucati in cos";
            this.labelBucati_cos.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(87, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "RON";
            // 
            // buttonAdaugaCos
            // 
            this.buttonAdaugaCos.Location = new System.Drawing.Point(204, 93);
            this.buttonAdaugaCos.Name = "buttonAdaugaCos";
            this.buttonAdaugaCos.OverrideDefault.Back.Color1 = System.Drawing.Color.White;
            this.buttonAdaugaCos.OverrideDefault.Back.Color2 = System.Drawing.Color.White;
            this.buttonAdaugaCos.OverrideDefault.Back.ColorAngle = 45F;
            this.buttonAdaugaCos.OverrideDefault.Border.Color1 = System.Drawing.Color.Firebrick;
            this.buttonAdaugaCos.OverrideDefault.Border.Color2 = System.Drawing.Color.Brown;
            this.buttonAdaugaCos.OverrideDefault.Border.ColorAngle = 45F;
            this.buttonAdaugaCos.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.buttonAdaugaCos.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonAdaugaCos.OverrideDefault.Border.Rounding = 20;
            this.buttonAdaugaCos.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.buttonAdaugaCos.Size = new System.Drawing.Size(122, 42);
            this.buttonAdaugaCos.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.buttonAdaugaCos.StateCommon.Back.Color2 = System.Drawing.Color.White;
            this.buttonAdaugaCos.StateCommon.Back.ColorAngle = 45F;
            this.buttonAdaugaCos.StateCommon.Back.ImageStyle = ComponentFactory.Krypton.Toolkit.PaletteImageStyle.TopLeft;
            this.buttonAdaugaCos.StateCommon.Border.Color1 = System.Drawing.Color.Firebrick;
            this.buttonAdaugaCos.StateCommon.Border.Color2 = System.Drawing.Color.Maroon;
            this.buttonAdaugaCos.StateCommon.Border.ColorAngle = 45F;
            this.buttonAdaugaCos.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.buttonAdaugaCos.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonAdaugaCos.StateCommon.Border.Rounding = 20;
            this.buttonAdaugaCos.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.buttonAdaugaCos.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.buttonAdaugaCos.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdaugaCos.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonAdaugaCos.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonAdaugaCos.StatePressed.Back.ColorAngle = 135F;
            this.buttonAdaugaCos.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonAdaugaCos.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonAdaugaCos.StatePressed.Border.ColorAngle = 135F;
            this.buttonAdaugaCos.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.buttonAdaugaCos.StatePressed.Border.Rounding = 20;
            this.buttonAdaugaCos.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.buttonAdaugaCos.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonAdaugaCos.StateTracking.Back.ColorAngle = 45F;
            this.buttonAdaugaCos.StateTracking.Border.Color1 = System.Drawing.Color.Maroon;
            this.buttonAdaugaCos.StateTracking.Border.Color2 = System.Drawing.Color.Firebrick;
            this.buttonAdaugaCos.StateTracking.Border.ColorAngle = 45F;
            this.buttonAdaugaCos.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.buttonAdaugaCos.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonAdaugaCos.StateTracking.Border.Rounding = 20;
            this.buttonAdaugaCos.TabIndex = 11;
            this.buttonAdaugaCos.Values.Text = "Adauga Cos";
            this.buttonAdaugaCos.Click += new System.EventHandler(this.buttonAdaugaCos_Click_1);
            // 
            // ProductControl
            // 
            this.BackColor = System.Drawing.Color.LightCoral;
            this.Controls.Add(this.buttonAdaugaCos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelBucati_cos);
            this.Controls.Add(this.numericUpDownBucati_Cos);
            this.Controls.Add(this.labelBucati);
            this.Controls.Add(this.labelinStoc);
            this.Controls.Add(this.labelNrRecenzii);
            this.Controls.Add(this.labelStele);
            this.Controls.Add(this.labelPret);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBoxImagine);
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

        private void buttonAdaugaCos_Click_1(object sender, EventArgs e)
        {

            try
            {
                int id_user = Afisare_Produse.GetUtilizatorID();
                string connectionString = Aranjare.GetConnectionString();
                if (!Interogari.AdaugainCos(connectionString, nr_bucati_in_cos + 1, produs.Pret, id_user, produs.Id_Produs))
                {
                    MessageBox.Show("Nu s-a putut adauga produsul!");
                    return;
                }
            }
            catch (Exception) { MessageBox.Show("Nu s-a putut adauga in cos."); }

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
