using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace shop_online
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            List<string> a = AutoLogin();

            if (a != null)
                Application.Run(new Afisare_Produse(a [0], a [1], a [2]));
            else
                Application.Run(new FormLogin());

        }

        private static List<string> AutoLogin()
        {
            string filePath = "logInfo.txt";
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (!fileInfo.Exists || fileInfo.Length == 0)
                    return null;

                string [] lines = File.ReadAllLines(filePath);
                if (lines.Length % 2 == 1)
                    return null;

                string telefon = lines [0];
                string parola = lines [1];
                string tel = string.Empty;
                string email_sus = string.Empty;

                if (Aranjare.IsValidTelefon(telefon))
                    tel = telefon;
                else if (Aranjare.IsValidEmail(telefon))
                    email_sus = telefon;
                else
                    return null;

                string connectionString = Aranjare.GetConnectionString();
                if (Interogari.GetUserID(connectionString, email_sus, tel, parola) <= 0)
                    return null;
                List<string> lista = new List<string>
                {
                    email_sus,
                    parola,
                    tel
                };
                return lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare la autentificare automată: {ex.Message}");
                return null;
            }

        }

    }
}
