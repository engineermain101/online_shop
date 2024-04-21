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
