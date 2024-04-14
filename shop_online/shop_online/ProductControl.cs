/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System.Windows.Forms;
namespace shop_online
{
    public class ProductControl : UserControl
    {
        private Button buttonAdaugaCos;
        private PictureBox pictureBoxImagine;
        private Label labelTitle;
        private Label labelPret;
        private Label labelStele;
        private Label labelNrRecenzii;
        private Label labelinStoc;

        public ProductControl( ProdusItem product )
        {
            InitializeComponent();
            SetProductInfo(product);
        }

        /* private void InitializeComponents()
         {
             pictureBox = new PictureBox
             {
                 SizeMode = PictureBoxSizeMode.StretchImage,
                 Dock = DockStyle.Top,
                 Size = new Size(20, 20)
             };

             titleLabel = new Label
             {
                 Dock = DockStyle.Bottom,
                 TextAlign = ContentAlignment.MiddleCenter
             };

             pretLabel = new Label
             {
                 Dock = DockStyle.Bottom,
                 TextAlign = ContentAlignment.MiddleCenter
             };

             recenzieLabel = new Label
             {
                 Dock = DockStyle.Bottom,
                 TextAlign = ContentAlignment.MiddleCenter
             };

             nr_recenziiLabel = new Label
             {
                 Dock = DockStyle.Bottom,
                 TextAlign = ContentAlignment.MiddleCenter
             };

             ButonAdaugaCos = new Button
             {
                 Text = "Adauga in cos",
                 Dock = DockStyle.Bottom
             };

             LabelInStoc = new Label
             {
                 Dock = DockStyle.Bottom,
                 TextAlign = ContentAlignment.MiddleCenter
             };

             Controls.Add(pictureBox);
             Controls.Add(titleLabel);
             Controls.Add(pretLabel);
             Controls.Add(recenzieLabel);
             Controls.Add(nr_recenziiLabel);
             Controls.Add(ButonAdaugaCos);
             Controls.Add(LabelInStoc);
         }
         */
        private void SetProductInfo( ProdusItem product )
        {
            pictureBoxImagine.Image = product.Image;
            labelTitle.Text = product.Title;
            labelPret.Text = product.Pret.ToString();
            labelStele.Text = "Nota recenzie: " + product.Nota_Review.ToString();
            labelNrRecenzii.Text = "(" + product.Nr_recenzii.ToString() + ")";
            labelinStoc.Text = "In stoc";
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagine)).BeginInit();
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
            this.labelStele.Location = new System.Drawing.Point(130, 59);
            this.labelStele.Name = "labelStele";
            this.labelStele.Size = new System.Drawing.Size(65, 17);
            this.labelStele.TabIndex = 4;
            this.labelStele.Text = "Nr stele: ";
            // 
            // labelNrRecenzii
            // 
            this.labelNrRecenzii.AutoSize = true;
            this.labelNrRecenzii.Location = new System.Drawing.Point(130, 85);
            this.labelNrRecenzii.Name = "labelNrRecenzii";
            this.labelNrRecenzii.Size = new System.Drawing.Size(18, 17);
            this.labelNrRecenzii.TabIndex = 5;
            this.labelNrRecenzii.Text = "()";
            // 
            // labelinStoc
            // 
            this.labelinStoc.AutoSize = true;
            this.labelinStoc.Location = new System.Drawing.Point(16, 102);
            this.labelinStoc.Name = "labelinStoc";
            this.labelinStoc.Size = new System.Drawing.Size(51, 17);
            this.labelinStoc.TabIndex = 6;
            this.labelinStoc.Text = "In Stoc";
            // 
            // ProductControl
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImagine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonAdaugaCos_Click( object sender, System.EventArgs e )
        {

        }

        private void ProductControl_Load( object sender, System.EventArgs e )
        {

        }
    }

}
