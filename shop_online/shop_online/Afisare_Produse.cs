using System;
using System.Configuration;
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
        private int utilizatorCurentId = -1;
        private bool admin = false;

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

        private void Afisare_Produse_Load( object sender, EventArgs e )
        {
            LoadUser(emailUtilizator, parolaUtilizator, telefonUtilizator);
        }
        private void Afisare_Produse_FormClosed( object sender, FormClosedEventArgs e )
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        public void LoadUser( string email, string parola, string telefon )
        {
            InitializeComponent();
            telefonUtilizator = telefon;
            parolaUtilizator = parola;
            emailUtilizator = email;
            string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;


            utilizatorCurentId = Interogari.GetUserID(connectionString, emailUtilizator, telefonUtilizator, parolaUtilizator);

            if (utilizatorCurentId < 1)
                return;

            Adaugare_in_flowLayoutPanelProduse();


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


        private void Adaugare_in_flowLayoutPanelProduse()
        {
            flowLayoutPanelProduse.Controls.Clear();
            string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;
            DataTable data = Interogari.SelectTop30Produse(connectionString);
            foreach (DataRow row in data.Rows)
            {
                int cantitate = (int)row ["cantitate"];
                if (cantitate > 0)
                {
                    // Extrage datele din DataRow
                    int id_produs = (int)row ["id_produs"];
                    Image [] imagelist = Interogari.SelectImagine(connectionString, id_produs);
                   
                    Image image=Image.FromFile("C:/Users/Roland/Desktop/FACULTATE/AN 3/zpoze/Examen_EP_2024.jpg");
                                                          
                    if (imagelist != null && imagelist.Length > 0)
                    {
                        image = imagelist [0];
                    }

                    string title = (string)row ["nume"];
                    decimal pretMoney = (decimal)row ["pret"];
                    int pret = (int)pretMoney;
                    int [] medie = Interogari.MedieRecenzii(connectionString, id_produs);
                    int medie_review = medie [0];
                    int nr_recenzii = medie [1];

                    // Creează un nou ProdusItem și adaugă-l direct în flowLayoutPanelProduse
                    ProdusItem produs = new ProdusItem(image, title, pret, medie_review, nr_recenzii);
                    Panel pan = new Panel();
                    pan.BackColor = Color.Pink;
                    pan.Controls.Add(new ProductControl(produs));
                    flowLayoutPanelProduse.Controls.Add(pan);
                }
            }

            // MessageBox.Show("Adaugare in flow panel.");
        }







    }
}
