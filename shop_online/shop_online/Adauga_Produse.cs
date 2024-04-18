using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace shop_online
{
    public partial class Adauga_Produse : Form
    {
        private int user_id_furnizor = -1;

        public Adauga_Produse()
        {
            InitializeComponent();
        }

        public Adauga_Produse( int id_furnizor )
        {
            InitializeComponent();
            user_id_furnizor = id_furnizor;
        }

        private void Adauga_Produse_FormClosed( object sender, FormClosedEventArgs e )
        {
            Application.OpenForms ["Afisare_Produse"].Show();
        }
        private void Adauga_Produse_Load( object sender, System.EventArgs e )
        {
            LoadUser(user_id_furnizor);
        }
        public void LoadUser( int id_furnizor )
        {
            InitializeComponent();
            user_id_furnizor = id_furnizor;
            string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;



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




        //Puia
        private void buttonAdaugaProdus_Click( object sender, System.EventArgs e )
        {
            int Cantitate = int.Parse(textBoxCantitate.Text);

            decimal Pret = decimal.Parse(textBoxPret.Text);
            string Descriere = textBoxDescriere.Text;
            List<string> Numelistaspecificatii = textBoxDenumireSpecificatie.Text.Split(',').Select(s => s.Trim()).ToList();
            List<string> ValoareListaSpecificatii = textBoxValoareSpecificatie.Text.Split(',').Select(s => s.Trim()).ToList();
            int id_Categorie = int.Parse(comboBoxCategorie.Text);
            Image image = pictureBoxImagine.Image;
            string NumeProdus = textBoxDenumire.Text;
            string connectionString = ConfigurationManager.ConnectionStrings ["DatadeBaza"].ConnectionString;


            Interogari.InsertProdus(connectionString, image, id_Categorie, user_id_furnizor, NumeProdus, Cantitate, Pret, Descriere, Numelistaspecificatii, ValoareListaSpecificatii);
        }


        //Horia
    }
}
