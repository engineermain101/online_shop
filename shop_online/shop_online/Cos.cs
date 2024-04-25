using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace shop_online
{
    public partial class Cos : Form
    {
        private int utilizatorId = -1;

        public Cos()
        {
            InitializeComponent();
        }

        public Cos( int utilizatorId )
        {
            InitializeComponent();
            this.utilizatorId = utilizatorId;
        }
        private void Cos_FormClosed( object sender, FormClosedEventArgs e )
        {
            Application.OpenForms ["Afisare_Produse"].Show();
        }
        private void Cos_Load( object sender, EventArgs e )
        {
            LoadUser(utilizatorId);
        }

        public void LoadUser( int userId )
        {
            if (userId < 0)
            {
                Application.Exit();
                return;
            }
            utilizatorId = userId;

            string connectionString = Aranjare.GetConnectionString();
            DataTable data = Interogari.GetCos(connectionString, utilizatorId);
            Aranjare.Adaugare_in_flowLayoutPanel(flowLayoutPanelProduse, data, false);

            decimal pret_total = flowLayoutPanelProduse.Controls.OfType<ProductControl>().Sum(pc => pc.GetProdus_Pret());
            labelPretTotal.Text ="Pret total: "+ pret_total;
        }

        private void Cos_Click( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }

        private void flowLayoutPanelProduse_Click( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }

        private void buttonElimina_Click( object sender, EventArgs e )
        {
            int id_produs = Aranjare.GetIdProdusSelectat(flowLayoutPanelProduse);
            string con = Aranjare.GetConnectionString();
            Interogari.DeleteProdusdinCos(con, utilizatorId, id_produs);
        }
    }
}
