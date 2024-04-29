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


            // Acum lista 'produse' conține obiecte ProdusItem populate cu datele din DataTable


            {


                /* {

                cartiPanelCartiDateTimePickerAnAparitie.Format = DateTimePickerFormat.Custom;
                cartiPanelCartiDateTimePickerAnAparitie.CustomFormat = "dd MMMM yyyy";
                cartiPanelCartiDateTimePickerAnAparitie.Value = DateTime.Today;

                List<ComboBox> comboBoxes = new List<ComboBox>
                {
                    cartiPanelComboBoxNume, cartiPanelComboBoxPrenume,
                    cartiPanelComboBoxGen, cartiPanelComboBoxEditura,
                    cartiPanelComboBoxRolAdmin
                };
                foreach (ComboBox comboBox in comboBoxes)
                {
                    comboBox.IntegralHeight = false;
                    comboBox.MaxDropDownItems = 5;
                }

                string connectionString = ConfigurationManager.ConnectionStrings ["AdaugareDate"].ConnectionString;
                InitializePanelStates();
                utilizatorCurentId = GetUserID(connectionString, numeUtilizator, prenumeUtilizator, emailUtilizator, parolaUtilizator, "parola");
                admin = VerificaAdmin(utilizatorCurentId, connectionString);

                cartiPanelCartiButtonAdaugasiSterge.Text = "Imprumuta";
                cartiPanelTextBoxNrCopii.Visible = false;
                cartiPanelLabelNrCopii.Visible = false;
                cartiPanelTextBoxISBN.ReadOnly = false;
                cartiPanelCartiListBox.HorizontalScrollbar = true;
                cartiPanelUserApasat.Visible = false;
                cartiPanelInformatiiUserApasat.Visible = false;
                cartiPanelCartiApasat.Visible = true;
                cartiPanelComboBoxRolAdmin.Visible = false;
                cartiPanelLabelRolAdmin.Visible = false;



                cartiPanelTextBoxISBN.Visible = admin;
                cartiPanelLabelISBN.Visible = admin;
                adaugaToolStripMenuItem1.Visible = admin;
                stergeToolStripMenuItem1.Visible = admin;
                modificaToolStripMenuItem1.Visible = admin;
                adminCartiToolStripMenuItem.Visible = admin;
                adaugaToolStripMenuItem2.Visible = admin;
                stergeToolStripMenuItem2.Visible = admin;

                ConfigurarePanelCentral(cartiPanelCartiApasat);
                ConfigurarePanelCentral(cartiPanelUserApasat);
                ConfigurarePanelCentral(cartiPanelInformatiiUserApasat);


                cartiPanelInformatiiUserApasat.BackColor = Color.Coral; // Setăm culoarea panelului la roz
                cartiPanelInformatiiLabelTitlu.Text = "Informații Utilizator"; // Textul titlului
                cartiPanelInformatiiLabelTitlu.Font = new Font("Arial", 16, FontStyle.Bold); // Stilul și dimensiunea textului
                cartiPanelInformatiiLabelTitlu.Dock = DockStyle.Top; // Plasarea titlului în partea de sus a panelului
                cartiPanelInformatiiLabelTitlu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                cartiPanelInformatiiLabelTitlu.TextAlign = ContentAlignment.MiddleCenter;


                utilizatorSters = IsAccountDeleted(utilizatorCurentId, connectionString);
                if (utilizatorSters)
                {
                    cartiCartiToolStripMenuItem.Visible = false;
                    adminCartiToolStripMenuItem.Visible = false;
                    adaugaToolStripMenuItem2.Visible = false;
                    stergeToolStripMenuItem2.Visible = false;
                    cartiPanelCartiApasat.Visible = false;
                    cartiPanelUserApasat.Visible = false;
                    cartiPanelInformatiiUserButtonModifica.Visible = false;
                    cartiPanelInformatiiUserButtonStergeCont.Text = "Recupereaza Contul";

                    cartiPanelInformatiiUserApasat.Visible = true;
                }*/
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
                CloseCurrentFormAndOpenAdaugaProdus(id_furnizor);
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
                CloseCurrentFormAndOpenCos(utilizatorCurentId);
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

        private void CloseCurrentFormAndOpenCos( int utilizatorId )
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

        private void flowLayoutPanelProduse_DoubleClick( object sender, EventArgs e )
        {
            Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }

        public void ResetFlowLayoutPanelProduse()
        {
            //FlowLayoutPanel fl = Afisare_Produse.flowLayoutPanelProduse;
            if (flowLayoutPanelProduse != null)
                Aranjare.ResetColorProductControl(flowLayoutPanelProduse);
        }



        //Claudiu

        //Puia

        //Horia
    }
}
