using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;

namespace shop_online
{
    public class ProdusItem
    {
        public List<Image> Image
        {
            get; set;
        }
        public string Nume
        {
            get; set;
        }
        public decimal Pret
        {
            get;
        } = -1;
        public int Nota_Review
        {
            get; set;
        }
        public int Nr_recenzii
        {
            get; set;
        }
        public int Id_Produs
        {
            get;
        } = -1;
        public int Cantitate
        {
            get; set;
        }
        public string Descriere
        {
            get; set;
        }
        public int Id_Furnizor
        {
            get;
        } = -1;
        public int Id_Categorie
        {
            get;
        } = -1;


        public ProdusItem( List<Image> image, string title, decimal pret, int nota_rewiu, int nr_recenzii, int id_produs, int cantitate, string descriere, int id_furnizor, int id_categorie )
        {
            Id_Produs = id_produs;
            Image = image;
            Nume = title;
            Pret = pret;//Convert.ToDecimal(pret.ToString("N2", new CultureInfo("ro-RO")));
            Nota_Review = nota_rewiu;
            Nr_recenzii = nr_recenzii;
            Cantitate = cantitate;
            Descriere = descriere;
            Id_Furnizor = id_furnizor;
            Id_Categorie = id_categorie;
        }

    }

}
