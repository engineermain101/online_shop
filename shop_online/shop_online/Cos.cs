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
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception) { MessageBox.Show("Nu aveti autorizatie."); Application.Exit(); return; }
            DataTable data = Interogari.GetCos(connectionString, utilizatorId);
            Aranjare.Adaugare_in_flowLayoutPanel(flowLayoutPanelProduse, data, false);

            CalculatePretTotal();

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
            Aranjare.Delete_from_flowLayoutPanel(flowLayoutPanelProduse, id_produs);
            string con = null;
            try
            {
                con = Aranjare.GetConnectionString();
            }
            catch (Exception) { return; }
            Interogari.DeleteProdusdinCos(con, utilizatorId, id_produs);
            CalculatePretTotal();
        }


        private void buttonCumpara_Click( object sender, EventArgs e )
        {

        }

        private void CalculatePretTotal()
        {
            decimal pret_total = flowLayoutPanelProduse.Controls.OfType<ProductControl>().Sum(pc => pc.GetProdus_Pret() * pc.GetNrBucatiCos());
            labelPretTotal.Text = "Pret total: " + pret_total + " lei";
        }

        public void ResetFlowLayoutPanelProduse()
        {
            if (flowLayoutPanelProduse != null)
                Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }

    }
}
