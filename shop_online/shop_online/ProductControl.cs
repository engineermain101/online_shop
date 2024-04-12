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
        private PictureBox pictureBox;
        private Label titleLabel;
        private Label pretLabel;
        private Label recenzieLabel;
        private Label nr_recenziiLabel;
        public Button ButonAdaugaCos;
        public Label LabelInStoc;

        public ProductControl( ProdusItem product )
        {
            InitializeComponents();
            SetProductInfo(product);
        }

        private void InitializeComponents()
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

        private void SetProductInfo( ProdusItem product )
        {
            pictureBox.Image = product.Image;
            titleLabel.Text = product.Title;
            pretLabel.Text = product.Pret.ToString();
            recenzieLabel.Text = "Nota recenzie: " + product.Nota_Review.ToString();
            nr_recenziiLabel.Text = "(" + product.Nr_recenzii.ToString() + ")";
            LabelInStoc.Text = "In stoc";
        }
    }

}
