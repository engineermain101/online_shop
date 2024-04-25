using System;
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
            Application.OpenForms ["Afisare_Produse"].Show();
        }
        private void DetaliiProdus_Load( object sender, EventArgs e )
        {
            LoadUser(produs);
        }

        public void LoadUser( ProdusItem produs )
        {
            this.produs = produs;
        }


    }
}
