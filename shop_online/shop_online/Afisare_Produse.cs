using System;
using System.Windows.Forms;

namespace shop_online
{
    public partial class Afisare_Produse : Form
    {
        private string emailUtilizator;
        private string parolaUtilizator;
        private string telefonUtilizator;

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
            /* {
            numeUtilizator = nume;
            prenumeUtilizator = prenume;
            parolaUtilizator = parola;
            emailUtilizator = email;
            telefonUtilizator = telefon;

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
}
