/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace shop_online
{
    public class ProdusItem
    {
        public Image Image
        {
            get; set;
        }
        public string Title
        {
            get; set;
        }
        public decimal Pret
        {
            get; set;
        }
        public int Nota_Review
        {
            get; set;
        }
        public int Nr_recenzii
        {
            get; set;
        }
        public Button ButonAdaugaCos
        {
            get; 
        }
        public Label LabelInStoc
        {
            get; set;
        }

        public ProdusItem( Image image, string title, decimal pret, int nota_rewiu, int nr_recenzii )
        {
            Image = image;
            Title = title;
            Pret = pret;
            Nota_Review = nota_rewiu;
            Nr_recenzii = nr_recenzii;
            //Buton_Adauga_Cos= buton_adauga_cos
            //Label_in_Stoc= label_in_Stoc

            ButonAdaugaCos = new Button();
            ButonAdaugaCos.Text = "Adaugă în coș";
            ButonAdaugaCos.Click += ButonAdaugaCos_Click;


            LabelInStoc = new Label();
            LabelInStoc.Text = "În stoc";
        }
        private void ButonAdaugaCos_Click( object sender, EventArgs e )
        {
            // Adaugă logica pentru adăugarea în coș aici
            // Poți accesa informațiile despre produs folosind proprietățile clasei
        }
    }

}
