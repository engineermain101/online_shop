using System;
using System.Drawing;
using System.Globalization;

namespace shop_online
{
    public class ProdusItem
    {
        public Image [] Image
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
        public int Id_produs
        {
            get; set;
        }
        public int Cantitate
        {
            get; set;
        }
        public string Descriere
        {
            get; set;
        }

        public ProdusItem( Image [] image, string title, decimal pret, int nota_rewiu, int nr_recenzii, int id_produs, int cantitate, string descriere )
        {
            Id_produs = id_produs;
            Image = image;
            Title = title;
            Pret = Convert.ToDecimal(pret.ToString("N2", new CultureInfo("ro-RO")));
            Nota_Review = nota_rewiu;
            Nr_recenzii = nr_recenzii;
            Cantitate = cantitate;
            Descriere = descriere;
        }

    }

}
