using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace shop_online
{
    /*putem folosi si dictionar. Asta mi-a spus chatul.
    
     public class Aranjare
{
    public Dictionary<string, bool> ElementeVizibile { get; set; }

    public Aranjare()
    {
        ElementeVizibile = new Dictionary<string, bool>
        {
            { "panelMenu", true },
            { "panelSignUp", false },
            { "labelParola", true },
            { "textBoxParola", true },
            { "labelTelefon", true },
            { "textBoxTelefon", true },
            { "buttonAcces", true },
            { "buttonBack", true }
            // Adaugă alte elemente după nevoie


     TexteElemente = new Dictionary<string, string>
    {
        { "labelTelefonText", "Telefon" }, // Textul pentru label
    }
        };
    }

     private void SetPanelState(Aranjare config)//asa ar arata functia de SetPanel. Este mai simplu dar nu stiu cum se foloseste mai exact
    {
        foreach (var element in config.ElementeVizibile)
        {
             Control control = Controls[element.Key]; // Accesarea controlului după cheie
        if (control != null)
        {
            control.Visible = element.Value;
        }
        }

     foreach (var element in config.TexteElemente)
    {
        Control control = Controls[element.Key]; // Accesarea controlului după cheie
        if (control is Label label)
        {
            label.Text = element.Value;
        }
    }
    }
}

     */

    public class Aranjare
    {
        //Roli
        public bool panelMenuVisible
        {
            get; set;
        }
        public bool panelSignUpVisible
        {
            get; set;
        }
        public bool labelParolaVisible
        {
            get; set;
        }
        public bool textBoxParolaVisible
        {
            get; set;
        }
        public bool labelTelefonVisible
        {
            get; set;
        }
        public bool textBoxTelefonVisible
        {
            get; set;
        }
        public string labelTelefonText
        {
            get; set;
        }
        public bool buttonAccesVisible
        {
            get; set;
        }
        public bool buttonBackVisible
        {
            get; set;
        }

        public Aranjare()
        {//DE FIECARE DATA CAND FACETI O NOUA METODA SAU CEVA PUNETI 
         //SI IN FUNCTIA   SetPanelState( Aranjare config ) DIN FormLogin


            panelMenuVisible = true;
            panelSignUpVisible = false;
            labelParolaVisible = true;
            textBoxParolaVisible = true;
            labelTelefonVisible = true;
            textBoxTelefonVisible = true;
            labelTelefonText = "";
            buttonAccesVisible = true;
            buttonBackVisible = true;






            /*Exemplu de la aplicatia cu Biblioteca
             * IsComboBoxRolAdminVisible = false;
            PanelName = "cartiPanelCartiApasat";
            UserButtonStergeText = "Sterge Utilizator";
            IsISBNReadOnly = false;
            IsEdituraDropDown = true;
            IsNrCopiiVisible = false;
            IsAdaugasiStergeVisible = true;
            CautaButtonText = "Cauta";
            AdaugasiStergeButtonText = "Imprumuta";*/
        }


        public static string FormatName( string input )
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }
            input.Trim();
            // Păstrează prima literă mare și restul caractere mici
            string formattedText = input.Substring(0, 1).ToUpper() + input.Substring(1).ToLowerInvariant();
            return formattedText;
        }
        public static bool IsValidEmail( string email )
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            email.Trim();

            // Expresie regulată pentru validarea formatului complet al emailului
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            // Verifică dacă emailul respectă formatul
            return Regex.IsMatch(email, pattern);
        }
        public static bool IsValidTelefon( string telefon )
        {
            if (string.IsNullOrEmpty(telefon))
                return false;
            telefon.Trim();

            // Expresie regulată pentru validarea formatului numărului de telefon
            string pattern = @"^\d{10}$";

            // Verifică dacă numărul de telefon respectă formatul
            return Regex.IsMatch(telefon, pattern);
        }
        public static void ToateObicteledinPanelVisible( Panel panel, bool vis )
        {
            foreach (Control control in panel.Controls)
            {
                control.Visible = vis;
            }
        }
        public static void ToateTextBoxurileledinPanelGoale( Panel panel )
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.Text = string.Empty;
                }
            }
        }
        public static ImageFormat GetImageFormat( string fileName )
        {
            // Obține extensia fișierului
            string extension = Path.GetExtension(fileName);

            // Verifică dacă extensia este validă și obține formatul corespunzător
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".gif":
                    return ImageFormat.Gif;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".tiff":
                    return ImageFormat.Tiff;
                case ".ico":
                    return ImageFormat.Icon;
                default:
                    throw new NotSupportedException("Extensia fișierului nu este suportată.");
            }
        }
        public static string GetConnectionString()
        {
            ConnectionStringSettingsCollection connectionStrings = ConfigurationManager.ConnectionStrings;
            string computerName = Environment.MachineName;

            foreach (ConnectionStringSettings connectionString in connectionStrings)
            {
                // Verifică dacă connection string-ul conține numele calculatorului
                if (connectionString.ConnectionString.Contains(computerName))
                {
                    // Aici ai găsit connection string-ul potrivit pentru calculatorul curent
                    //Console.WriteLine(connectionString.ConnectionString);
                    return connectionString.ConnectionString;

                    // Poți folosi connectionName și connectionValue aici în funcție de necesități
                    // De exemplu, poți utiliza connectionValue pentru a crea o conexiune la baza de date
                    // break; // Ieși din iterație după ce ai găsit connection string-ul potrivit
                }
            }

            //  return ConfigurationManager.ConnectionStrings [connectionName].ConnectionString;
            throw new Exception("Nu s-a găsit niciun connection string potrivit pentru acest calculator.");
        }
        public static void CenteredPanel( Form form, Panel panel, Size? offcentered = null )
        {
            panel.Anchor = AnchorStyles.None;
            panel.Dock = DockStyle.None;

            int x = (form.ClientSize.Width - panel.Width) / 2;
            int y = (form.ClientSize.Height - panel.Height) / 2;

            if (offcentered != null)
            {
                x += offcentered.Value.Width;
                y += offcentered.Value.Height;
            }

            panel.Location = new Point(x, y);
        }

        /// <summary>
        /// Dacă idProdus este specificat, șterge elementul corespunzător din FlowLayoutPanel.
        /// În caz contrar, șterge toate produsele cu cantitatea zero din coș.
        /// </summary>
        /// <param name="flowLayoutPanelProduse">FlowLayoutPanel-ul din care să fie șterse elementele.</param>
        /// <param name="idProdus">ID-ul produsului de șters (opțional).</param>
        public static void Delete_from_flowLayoutPanel( FlowLayoutPanel flowLayoutPanelProduse, int? idProdus = null )
        {
            if (flowLayoutPanelProduse == null)
                return;

            List<ProductControl> controlsToDelete;

            if (idProdus != null)
            {
                controlsToDelete = flowLayoutPanelProduse.Controls.OfType<ProductControl>()
                                                                  .Where(pc => pc.GetProdus_ID() == idProdus)
                                                                  .ToList();
            }
            else
            {
                controlsToDelete = flowLayoutPanelProduse.Controls.OfType<ProductControl>()
                                                                  .Where(pc => pc.GetBucatiProdusdinCos() == 0)
                                                                  .ToList();
            }

            if (controlsToDelete.Count == 0)
                return;

            foreach (ProductControl controlToDelete in controlsToDelete)
            {
                flowLayoutPanelProduse.Controls.Remove(controlToDelete);
            }
        }


        public static void ResetColorProductControl( FlowLayoutPanel flowLayoutPanel )
        {
            /*  bool clickedOutsideProductControl = true;
              Point cursorPosition = flowLayoutPanel.PointToClient(Cursor.Position);

              foreach (Control control in flowLayoutPanel.Controls)
              {
                  if (control.Bounds.Contains(cursorPosition))
                  {
                      clickedOutsideProductControl = false;
                      break;
                  }
              }

              // Dacă s-a făcut clic în afara unui ProductControl, dezactivează toate ProductControl-urile
              if (clickedOutsideProductControl)*/
            {
                foreach (Control control in flowLayoutPanel.Controls)
                {
                    if (control is ProductControl productControl)
                        productControl.ResetBackColor();
                }
            }
        }

        public static int GetIdProdusSelectat( FlowLayoutPanel panel )
        {//poate trebuie sa punem functia in clasa cu formul Cos
            if (panel == null)
                return -1;

            foreach (Control control in panel.Controls)
            {
                if (control is ProductControl productControl && productControl.BackColor == productControl.GetSelectedColor())
                    return productControl.GetProdus_ID();
            }
            return -1;
        }
        public static void ResetFlowLayoutPanelProduse( string formName )
        {
            if (Application.OpenForms [formName] is Form form)
            {
                if (form is Afisare_Produse || form is Cos)
                {
                    (form as dynamic).ResetFlowLayoutPanelProduse();
                }
            }
        }


        public static void Adaugare_in_flowLayoutPanel( FlowLayoutPanel flowLayoutPanelProduse, DataTable data, bool buttonVisible )
        {
            if (data == null || flowLayoutPanelProduse == null)
            {
                MessageBox.Show("Nu s-au gasit produse.");
                return;
            }

            flowLayoutPanelProduse.Controls.Clear();

            string connectionString = null;
            try
            {
                connectionString = GetConnectionString();
            }
            catch (Exception)
            {
                return;
            }

            foreach (DataRow row in data.Rows)
            {
                AdaugaProdusInFlowLayoutPanel(flowLayoutPanelProduse, row, connectionString, buttonVisible);
            }
        }
        private static void AdaugaProdusInFlowLayoutPanel( FlowLayoutPanel flowLayoutPanelProduse, DataRow row, string connectionString, bool buttonVisible )
        {
            int cantitate = (int)row ["cantitate"];
            if (cantitate <= 0)
            {
                return;
            }

            int id_produs = (int)row ["id_produs"];
            Dictionary<string, Image> imagedictionary = Interogari.SelectImagines(connectionString, id_produs);
            List<Image> images = GetProductImages(imagedictionary);

            string descriere = (string)row ["descriere"];
            string title = (string)row ["nume"];
            decimal pret = (decimal)row ["pret"];
            int[] medie=Interogari.ReviewNotExists(connectionString,id_produs);
            int medie_review =0;
            int nr_recenzii = 0;
            if (medie[0] != 0)
            {
                medie = null;
                medie = Interogari.MedieRecenzii(connectionString, id_produs);
                medie_review = medie [0];
                nr_recenzii = medie [1];
            }
 
            int nr_bucati = 0;
            decimal total_pret = -1;
            bool visible = false;

            if (row.Table.Columns.Contains("nr_bucati"))
            {
                nr_bucati = (int)row ["nr_bucati"];
                visible = true;
            }

            if (row.Table.Columns.Contains("total_pret"))
            {
                visible = true;
                total_pret = (decimal)row ["total_pret"];
            }

            int id_furnizor = (int)row ["id_furnizor"];
            int id_categorie = (int)row ["id_categorie"];

            ProdusItem produs = new ProdusItem(images, title, pret, medie_review, nr_recenzii, id_produs, cantitate, descriere, id_furnizor, id_categorie);
            flowLayoutPanelProduse.Controls.Add(new ProductControl(produs, buttonVisible, nr_bucati, total_pret));
        }
        private static List<Image> GetProductImages( Dictionary<string, Image> imagedictionary )
        {
            List<Image> images = new List<Image>();

            if (imagedictionary.Count == 0)
            {
                images.Add(SystemIcons.WinLogo.ToBitmap());
            }
            else
            {
                foreach (KeyValuePair<string, Image> kvp in imagedictionary)
                {
                    images.Add(kvp.Value);
                }
            }

            return images;
        }



        /// <summary>
        /// Face invizibil un form și deschide altul.  
        /// </summary>
        /// <example>
        /// Aranjare.CloseCurrentFormAndOpenForm(FindForm(), detaliiProdus, produs, MinimumSize);
        /// </example>
        public static void HideCurrentFormAndOpenNewForm<T, A>( Form oldForm, T newForm, A constructor_value, Size minimumSize )
where T : Form
where A : class
        {
            oldForm?.Hide();

            if (newForm == null)
            {
                newForm = (T)Activator.CreateInstance(typeof(T), constructor_value);

                newForm.MinimumSize = minimumSize;
                newForm.Size = newForm.MinimumSize;

                newForm.FormClosed += ( sender, e ) => { newForm = null; };
            }

            newForm.Visible = true;
            if (oldForm != null && oldForm.Visible)
            {
                oldForm.Hide();
            }

            /*  if (newForm is Adauga_Produse adaugaProduse)
              {
                  adaugaProduse.LoadUser(id);
              }
              else if (newForm is Cos cos)
              {
                  cos.LoadUser(id);
              }

              // if (newForm is ILoadUser loadUserForm)
              // {
              //     loadUserForm.LoadUser(id as int? ?? 0);
              // }*/

            newForm.Show();
            newForm.Focus();
        }





        //Claudiu


        //Puia

        //Horia


    }
}
