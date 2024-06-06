using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;


namespace shop_online
{
    public partial class Cos : KryptonForm
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
            try
            {
                string con = Aranjare.GetConnectionString();

                foreach (ProductControl control in flowLayoutPanelProduse.Controls.OfType<ProductControl>())
                {
                    Interogari.AdaugainCos(con, control.GetBucatiProdusdinCos(), control.GetProdus_Pret(), utilizatorId, control.GetProdus_ID());
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show("Eroare" + ee.ToString());
            }

            if (Application.OpenForms ["Afisare_Produse"] != null)
            {
                Application.OpenForms ["Afisare_Produse"].Show();
            }
                
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

        private void buttonAddRecenzie_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanelProduse.Controls.Count == 0)
                return;

            DialogResult result = MessageBox.Show("Doriți să cumpărați produsele selectate?", "Confirmare cumpărare", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
                if (connectionString == null)
                    throw new Exception();
            }
            catch (Exception ex) { MessageBox.Show("Eroare la cumpararea produsului." + ex.Message); return; }

            try
            {
                List<ProductControl> lista = new List<ProductControl>();
                foreach (ProductControl control in flowLayoutPanelProduse.Controls)
                {
                    lista.Add(control);
                    int a = control.GetBucatiProdusdinCos();
                    int b = control.GetNrBucatiCos();
                    if (a > b)
                    {
                        MessageBox.Show("Nu avem asa de multe produse in stoc.");
                        return;
                    }
                }
                Aranjare.Delete_from_flowLayoutPanel(flowLayoutPanelProduse);
                Interogari.TranzactieCumparareProdus(connectionString, utilizatorId, lista, "cash");
                MessageBox.Show("Tranzactia a fost realizata cu succes.");
                flowLayoutPanelProduse.Controls.Clear();
                Close();
            }
            catch (ArgumentException) { }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la efectuarea tranzactiei: " + ex.Message);
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanelProduse.Controls.Count == 0)
                return;

            int id_produs = Aranjare.GetIdProdusSelectat(flowLayoutPanelProduse);
            if (id_produs < 1)
                return;

            DialogResult result = MessageBox.Show("Doriți să stergeti produsul selectat?", "Confirmare stergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
                return;

            string con = null;
            try
            {
                con = Aranjare.GetConnectionString();
                if (con == null)
                    return;
            }
            catch (Exception) { return; }
            Aranjare.Delete_from_flowLayoutPanel(flowLayoutPanelProduse, id_produs);
            bool deleted = Interogari.DeleteProdusdinCos(con, utilizatorId, id_produs);
            if (!deleted)
                return;

            CalculatePretTotal();
        }
    }
}
