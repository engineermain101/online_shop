using System.Text.RegularExpressions;

namespace shop_online
{

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
        /* public string UserButtonStergeText
         {
             get; set;
         }
         public bool IsISBNReadOnly
         {
             get; set;
         }
         public bool IsEdituraDropDown
         {
             get; set;
         }
         public bool IsNrCopiiVisible
         {
             get; set;
         }
         public string CautaButtonText
         {
             get; set;
         }
         public bool IsAdaugasiStergeVisible
         {
             get; set;
         }
         public string AdaugasiStergeButtonText
         {
             get; set;
         }*/

        public Aranjare()
        {//DE FIECARE DATA CAND FACETI O NOUA METODA SAU CEVA PUNETI 
            //SI IN FUNCTIA   SetPanelState( Aranjare config ) DIN FormLogin


            panelMenuVisible = true;
            panelSignUpVisible = false;
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









        //Claudiu


        //Puia

        //Horia


    }
}
