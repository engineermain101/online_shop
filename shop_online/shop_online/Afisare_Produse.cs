using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;


namespace shop_online
{
    public partial class Afisare_Produse : KryptonForm
    {
        private static string emailUtilizator = "bbbb";
        private string parolaUtilizator = "0";
        private string telefonUtilizator = "01";
        private static int utilizatorCurentId = -1;
        private bool admin = false;
        private bool furnizor = false;
        private Adauga_Produse adauga_Produse = null;// Form nou
        private Cos cos = null;
        private Adauga_Stergere_Admin adauga_Stergere_Admin = null;
        private Adauga_Furnizor Adauga_Furnizor = null;
        private FormLogin formlogin = null;
        private bool userRequestedClose = false;

        public Afisare_Produse()
        {
            InitializeComponent();
            PopulateMenuStrip();
        }

        public Afisare_Produse( string email, string parola, string telefon )
        {
            InitializeComponent();
            PopulateMenuStrip();
            emailUtilizator = email;
            parolaUtilizator = parola;
            telefonUtilizator = telefon;
            utilizatorCurentId = -1;
        }
        //Roli
        private void Afisare_Produse_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !userRequestedClose)
            {
                Application.Exit();
            }
        }
        private void Afisare_Produse_Load( object sender, EventArgs e )
        {

            LoadUser(emailUtilizator, parolaUtilizator, telefonUtilizator);
            
       
        }
        public void LoadUser( string email, string parola, string telefon )
        {
            MinimumSize = new Size(520 * 2, 138 * 4);

            adaugaToolStripMenuItem.Visible = false;
            adaugaProdusToolStripMenuItem.Visible = false;
            adaugaFurnizorToolStripMenuItem.Visible = false;
            adaugaAdminToolStripMenuItem.Visible = false;

            stergereToolStripMenuItem.Visible = false;
            stergereProdusToolStripMenuItem.Visible = false;
            stergereFurnizorToolStripMenuItem.Visible = false;
            stergereAdminToolStripMenuItem.Visible = false;

            telefonUtilizator = telefon;
            parolaUtilizator = parola;
            emailUtilizator = email;

            // string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception) { return; }

            utilizatorCurentId = Interogari.GetUserID(connectionString, emailUtilizator, telefonUtilizator, parolaUtilizator);
            label1.Text = Interogari.GetNameById(connectionString, utilizatorCurentId);
            pictureBox1.Location = new Point(this.ClientSize.Width - pictureBox1.Width - 10, 10);

            label1.Location = new Point(this.ClientSize.Width - label1.Width - 10, pictureBox1.Bottom) ;
            // Setează ancorarea pentru PictureBox și Label
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            if (utilizatorCurentId < 1)
            {
                Application.Exit();
                return;
            }
            DataTable data = Interogari.SelectTopProduse(connectionString, 30);
            Aranjare.Adaugare_in_flowLayoutPanel(flowLayoutPanelProduse, data, true);

            if (Interogari.GetFurnizorId(connectionString, utilizatorCurentId) > 0)
            {
                adaugaToolStripMenuItem.Visible = true;
                adaugaProdusToolStripMenuItem.Visible = true;
                stergereToolStripMenuItem.Visible = true;
                stergereProdusToolStripMenuItem.Visible = true;
                furnizor = true;

                return;
            }

            if (Interogari.GetAdminId(connectionString, utilizatorCurentId) > 0)
            {
                admin = true;
                adaugaToolStripMenuItem.Visible = true;
                adaugaFurnizorToolStripMenuItem.Visible = true;
                adaugaAdminToolStripMenuItem.Visible = true;
                stergereToolStripMenuItem.Visible = true;
                stergereProdusToolStripMenuItem.Visible = true;
                stergereFurnizorToolStripMenuItem.Visible = true;
                stergereAdminToolStripMenuItem.Visible = true;
                stergereProdusToolStripMenuItem.Visible = false;

                return;
            }


        }
        private void flowLayoutPanelProduse_Click( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }
        private void adaugaProdusToolStripMenuItem_Click( object sender, EventArgs e )
        {
            //string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;
            string connectionString = null;
            try
            {
                connectionString = Aranjare.GetConnectionString();
            }
            catch (Exception) { return; }
            int id_furnizor = Interogari.GetFurnizorId(connectionString, utilizatorCurentId);

            if (id_furnizor > 0)
            {
                Size minimumSize = new Size(490, 535);
                Aranjare.HideCurrentFormAndOpenNewForm(this, adauga_Produse, (object)id_furnizor, minimumSize);
                //CloseCurrentFormAndOpenAdaugaProdus(id_furnizor);
            }

        }
        private void cosToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
            if (utilizatorCurentId > 0)
            {
                Size minimumSize = new Size(750, 560);
                Aranjare.HideCurrentFormAndOpenNewForm(this, cos, (object)utilizatorCurentId, minimumSize);
            }

        }

        private void flowLayoutPanelProduse_DoubleClick( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }

        private void adaugaFurnizorToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
            Size size = new Size(700, 500);
            Aranjare.HideCurrentFormAndOpenNewForm(this, Adauga_Furnizor, (object)true, size);
        }
        private void stergereFurnizorToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
            Size size = new Size(700, 500);
            Aranjare.HideCurrentFormAndOpenNewForm(this, Adauga_Furnizor, (object)false, size);
        }

        private void adaugaAdminToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
            Size size = new Size(700, 400);
            Aranjare.HideCurrentFormAndOpenNewForm(this, adauga_Stergere_Admin, (object)true, size);
        }
        private void stergereAdminToolStripMenuItem_Click( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
            Size size = new Size(700, 400);
            Aranjare.HideCurrentFormAndOpenNewForm(this, adauga_Stergere_Admin, (object)false, size);
        }

        private void delogheazateToolStripMenuItem_Click( object sender, EventArgs e )
        {
            /*Hide();
          FormLogin form2 = new FormLogin();
           form2.Closed += ( s, args ) => Close();
           form2.Show();*/
            string filePath = "logInfo.txt";
            File.WriteAllText(filePath, string.Empty);

            Size size = new Size(500, 300);
            Aranjare.HideCurrentFormAndOpenNewForm(FindForm(), formlogin, (object)-1, size);
            userRequestedClose = true;
            Close();

        }

        private void CategoryMenuItem_Click( object sender, EventArgs e )
        {
            string con = null;
            try
            {
                con = Aranjare.GetConnectionString();
            }
            catch (Exception) { return; }

            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            string category = clickedItem.Text.Trim();
            DataTable products = Interogari.GetProductsByCategory(con, category);
            Aranjare.Adaugare_in_flowLayoutPanel(flowLayoutPanelProduse, products, true);
        }


        public void ResetFlowLayoutPanelProduse()
        {
            if (flowLayoutPanelProduse != null)
                Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }
        public static int GetUtilizatorID()
        {
            return utilizatorCurentId;
        }
        public static string GetUtilizatorEmail()
        {
            return emailUtilizator;
        }
        private void PopulateMenuStrip()
        {
            string con = null;
            try
            {
                con = Aranjare.GetConnectionString();
            }
            catch (Exception) { return; }
            List<string> categories = Interogari.GetCategories(con);
            foreach (string category in categories)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(category);
                menuItem.Click += CategoryMenuItem_Click;
                categorieToolStripMenuItem.DropDownItems.Add(menuItem);
            }
        }
//Claudiu
        private void stergereProdusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Aranjare.HideCurrentFormAndOpenNewForm(this, new Stergere_Produs(GetUtilizatorID()),(object)true, MinimumSize);
        }
        public static int GetCurrentUserId()
        {
            return utilizatorCurentId;
        }

        private void threaduriToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Afisare_Produse_Resize(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.ClientSize.Width - pictureBox1.Width - 10, 10);
            label1.Location = new Point(this.ClientSize.Width - label1.Width - 10, pictureBox1.Bottom );

        }














        //Puia

        //Horia
    }
}
