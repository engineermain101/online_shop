using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace shop_online
{
    public partial class Afisare_Produse : Form
    {
        private string emailUtilizator = "bbbb";
        private string parolaUtilizator = "0";
        private string telefonUtilizator = "01";
        private static int utilizatorCurentId = -1;
        private bool admin = false;
        private bool furnizor = false;
        private Adauga_Produse adauga_Produse = null;// Form nou
        private Cos cos = null;

        public Afisare_Produse()
        {
            InitializeComponent();
        }

        public Afisare_Produse( string email, string parola, string telefon )
        {
            InitializeComponent();
            emailUtilizator = email;
            parolaUtilizator = parola;
            telefonUtilizator = telefon;
        }
        //Roli
        private void Afisare_Produse_FormClosed( object sender, FormClosedEventArgs e )
        {
            if (e.CloseReason == CloseReason.UserClosing)
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

            if (utilizatorCurentId < 1)
            {
                Application.Exit();
                return;
            }
            DataTable data = Interogari.SelectTop30Produse(connectionString);
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
            foreach (Control control in flowLayoutPanelProduse.Controls)
            {
                if (control is ProductControl)
                {
                    (control as ProductControl).ResetBackColor();
                }
            }
            if (utilizatorCurentId > 0)
            {
                Size minimumSize = new Size(750, 560);
                Aranjare.HideCurrentFormAndOpenNewForm(this, cos, (object)utilizatorCurentId, minimumSize);
                //CloseCurrentFormAndOpenCos(utilizatorCurentId);
            }

        }

        public static int GetUtilizatorID()
        {
            return utilizatorCurentId;
        }



        private void CloseCurrentFormAndOpenAdaugaProdus( int id_furnizor )
        {
            Hide();

            if (adauga_Produse == null)
            {
                adauga_Produse = new Adauga_Produse(id_furnizor)
                {
                    MinimumSize = new Size(490, 535)
                };
                adauga_Produse.Size = adauga_Produse.MinimumSize;
                adauga_Produse.FormClosed += ( sender, e ) => { adauga_Produse = null; }; // Resetare referință când formularul este închis
            }

            if (!adauga_Produse.Visible)
            {
                adauga_Produse.Visible = true;

                if (Application.OpenForms ["Afisare_Produse"] != null)
                {
                    Application.OpenForms ["Afisare_Produse"].Hide();
                }
            }
            adauga_Produse.LoadUser(id_furnizor);
            adauga_Produse.Show();
            adauga_Produse.Focus();
        }

        /*private void CloseCurrentFormAndOpenCos( int utilizatorId )
        {
            Hide();

            if (cos == null)
            {
                cos = new Cos(utilizatorId)
                {
                    MinimumSize = new Size(750, 560)
                };
                cos.Size = cos.MinimumSize;
                cos.FormClosed += ( sender, e ) => { cos = null; }; // Resetare referință când formularul este închis
            }

            if (!cos.Visible)
            {
                cos.Visible = true;

                if (Application.OpenForms ["Afisare_Produse"] != null)
                {
                    Application.OpenForms ["Afisare_Produse"].Hide();
                }
            }
            cos.LoadUser(utilizatorId);
            cos.Show();
            cos.Focus();
        }
        */
        private void flowLayoutPanelProduse_DoubleClick( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }

        public void ResetFlowLayoutPanelProduse()
        {
            if (flowLayoutPanelProduse != null)
                Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }

        private void adaugaFurnizorToolStripMenuItem_Click( object sender, EventArgs e )
        {

        }



        //Claudiu

        //Puia

        //Horia
    }
}
