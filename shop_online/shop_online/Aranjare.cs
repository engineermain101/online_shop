using System;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace shop_online
{/*putem folosi si dictionar. Asta mi-a spus chatul.
    
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
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(email))
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




        //Claudiu


        //Puia

        //Horia


    }
}
