using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;


namespace shop_online
{
    public partial class Stergere_Produs : KryptonForm
    {
        int furnizorId = -1;
        public Stergere_Produs(int furnizorId)
        {
            InitializeComponent();
            this.furnizorId = furnizorId;

        }

        private void Stergere_Produs_Load(object sender, EventArgs e)
        {
            LoadProduse();
        }
        private void LoadProduse()
        {
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch {
                MessageBox.Show("Nu s-a putut lua connection stringu"); return; 
            }

            furnizorId = Interogari.GetFurnizorId(connectionString, furnizorId);
            List<string> produse = Interogari.GetProduseByFurnizor(connectionString, furnizorId);
            foreach (string produs in produse)
            {
                listBox1.Items.Add(produs);
            }
            
           
        }

       

        private void Stergere_Produs_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms["Afisare_Produse"] != null)
                Application.OpenForms["Afisare_Produse"].Show();
        }

        

        private void buttonSterge_Click(object sender, EventArgs e)
        {
            string produs;
            try
            {
                 produs = listBox1.SelectedItem.ToString();
            }
            catch
            {
                MessageBox.Show("Nu a-ti selectat un produs din lista");
                return;
            }
            
            if (string.IsNullOrEmpty(produs))
            {
                MessageBox.Show("Va rog selectati un item din lista!");
                return;
            }
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch
            {
                MessageBox.Show("Nu s-a putut lua connection stringu"); return;
            }
            string[] parts = produs.Split(':');
            int numar;
            if (parts.Length > 0)
            {
                string numarString = parts[0].Trim();
                if (int.TryParse(numarString, out numar))
                {
                    DialogResult result = MessageBox.Show($"Doriti sa stergeti produsul?\n {produs}", "Confirmare", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {

                        Interogari.DeleteProductById(connectionString, numar);
                        listBox1.Items.Remove(listBox1.SelectedItem);
                    }
                }
                else
                {
                    MessageBox.Show("Numărul produsului nu este valid!");
                }
            }
            else
            {
                MessageBox.Show("Formatul produsului nu este valid!");
            }

        }
    }
}