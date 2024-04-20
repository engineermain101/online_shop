using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace shop_online
{
    class Interogari
    {
        //Roli
        public static bool SignUp( string connectionString, string nume, string email, string parola,
        string telefon, string judet, string oras, string strada, int numar )
        {
            if (string.IsNullOrWhiteSpace(nume) || string.IsNullOrWhiteSpace(parola) ||
                  string.IsNullOrWhiteSpace(judet) || string.IsNullOrWhiteSpace(oras) ||
                  string.IsNullOrWhiteSpace(strada) || numar < 1)
            {
                MessageBox.Show("Trebuie completate câmpurile!");
                return false;
            }

            if (!Aranjare.IsValidTelefon(telefon))
            {
                MessageBox.Show("Introduceți un telefon valid.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(email) && !Aranjare.IsValidEmail(email))//optional
            {
                MessageBox.Show("Introduceți un email valid.");
                return false;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                using (SqlCommand checkCommand = connection.CreateCommand())
                using (SqlCommand insertAddressCommand = connection.CreateCommand())
                using (SqlCommand insertUserCommand = connection.CreateCommand())
                {
                    checkCommand.Transaction = transaction;
                    insertAddressCommand.Transaction = transaction;
                    insertUserCommand.Transaction = transaction;

                    try
                    {
                        string checkQuery = @" SELECT COUNT(*)
                                        FROM Useri AS u
                                        JOIN Adresa AS a ON u.id_adresa = a.id_adresa
                                        WHERE u.username = @nume
                                            AND (u.telefon = @telefon OR u.email = @email)
                                            AND u.parola = @parola 
                                            AND a.judet = @judet
                                            AND a.oras = @oras
                                            AND a.strada = @strada
                                            AND a.numar = @numar";

                        checkCommand.CommandText = checkQuery;
                        checkCommand.Parameters.AddWithValue("@nume", nume);
                        checkCommand.Parameters.AddWithValue("@telefon", telefon);
                        checkCommand.Parameters.AddWithValue("@parola", parola);
                        checkCommand.Parameters.AddWithValue("@email", email);
                        checkCommand.Parameters.AddWithValue("@judet", judet);
                        checkCommand.Parameters.AddWithValue("@oras", oras);
                        checkCommand.Parameters.AddWithValue("@strada", strada);
                        checkCommand.Parameters.AddWithValue("@numar", numar);

                        int existingUserCount = (int)checkCommand.ExecuteScalar();
                        if (existingUserCount > 0)
                        {
                            MessageBox.Show("Utilizatorul există deja în baza de date.");
                            return false;
                        }

                        string insertAddressQuery = @"INSERT INTO Adresa 
                                          (judet, oras, strada, numar) 
                                          VALUES (@judet, @oras, @strada, @numar); 
                                          SELECT SCOPE_IDENTITY();";

                        insertAddressCommand.CommandText = insertAddressQuery;
                        insertAddressCommand.Parameters.AddWithValue("@judet", judet);
                        insertAddressCommand.Parameters.AddWithValue("@oras", oras);
                        insertAddressCommand.Parameters.AddWithValue("@strada", strada);
                        insertAddressCommand.Parameters.AddWithValue("@numar", numar);
                        int userId = Convert.ToInt32(insertAddressCommand.ExecuteScalar());


                        string insertUserQuery = @"INSERT INTO Useri
                                    (username, email, parola, telefon,id_adresa) 
                                    VALUES (@nume, @email, @parola, @telefon,@id_adresa);                                   "; // Obținem ID-ul utilizatorului inserat

                        insertUserCommand.CommandText = insertUserQuery;
                        insertUserCommand.Parameters.AddWithValue("@nume", nume);
                        insertUserCommand.Parameters.AddWithValue("@email", email);
                        insertUserCommand.Parameters.AddWithValue("@parola", parola);
                        insertUserCommand.Parameters.AddWithValue("@telefon", telefon);
                        insertUserCommand.Parameters.AddWithValue("@id_adresa", userId);

                        insertUserCommand.ExecuteNonQuery();

                        transaction.Commit();
                        // MessageBox.Show("Utilizatorul a fost adăugat cu succes în baza de date.");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("A apărut o eroare la inserarea în baza de date: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public static bool Login( string connectionString, string email, string telefon, string parola )
        {
            if (string.IsNullOrWhiteSpace(parola))
            {
                MessageBox.Show("Trebuie completat câmpul parolă");
                return false;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT COUNT(*) FROM Useri WHERE parola = @parola";

                    if (Aranjare.IsValidEmail(email))
                    {
                        query += " AND email = @email";
                    }
                    else if (Aranjare.IsValidTelefon(telefon))
                    {
                        query += " AND telefon = @telefon";
                    }
                    else
                    {
                        MessageBox.Show("Trebuie să furnizați un email sau un număr de telefon pentru autentificare.");
                        return false;
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@parola", parola);
                        if (!string.IsNullOrEmpty(email))
                            command.Parameters.AddWithValue("@email", email);
                        else if (!string.IsNullOrEmpty(telefon))
                            command.Parameters.AddWithValue("@telefon", telefon);

                        int result = (int)command.ExecuteScalar();

                        if (result > 0)
                        {
                            // Utilizatorul există în baza de date
                            return true;
                        }
                        else
                            MessageBox.Show("Autentificare eșuată. Verificați datele de autentificare sau nu v-ati conectat inca.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la autentificare: " + ex.Message);
                }
            }
            return false;
        }

        /// <summary>
        /// Insereaza o imagine in tabela Imagine.
        /// </summary>
        public static void InsertImagine( string connectionString, Image imagine, int id_produs, string nume_imagine_cu_extensie )
        {
            ImageFormat format = Aranjare.GetImageFormat(nume_imagine_cu_extensie);
            try
            {
                byte [] imageData;// Convertirea imaginii într-un array de bytes
                /*  using (MemoryStream ms = new MemoryStream())
                {
                    imagine.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Salvează imaginea în format JPEG
                    imageData = ms.ToArray();
                }*/
                using (MemoryStream ms = new MemoryStream())
                {
                    // Salvează imaginea în formatul specificat
                    imagine.Save(ms, format);
                    imageData = ms.ToArray();
                }

                string query = "INSERT INTO Imagini (imagine,id_produs,nume) VALUES (@ImageData,@id_produs,@Nume)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ImageData", imageData);
                        command.Parameters.AddWithValue("@id_produs", id_produs);
                        command.Parameters.AddWithValue("@Nume", nume_imagine_cu_extensie);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Eroare la inserare imagine: " + e.Message);
            }
        }

        public static Dictionary<string, Image> SelectImagines( string connectionString, int id_produs )
        {
            try
            {
                Dictionary<string, Image> imagesDictionary = new Dictionary<string, Image>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT imagine, nume FROM Imagini WHERE id_produs = @ProductId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", id_produs);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte [] imageDataFromDatabase = (byte [])reader ["imagine"];
                                string imageName = reader ["nume"].ToString();
                                ImageFormat format = Aranjare.GetImageFormat(imageName);

                                using (MemoryStream ms = new MemoryStream(imageDataFromDatabase))
                                {
                                    Image imageFromDatabase = Image.FromStream(ms);
                                    using (MemoryStream convertedMs = new MemoryStream())
                                    {
                                        // Salvează imaginea în formatul corespunzător
                                        imageFromDatabase.Save(convertedMs, format);

                                        // Poziționează fluxul la început pentru a putea citi datele
                                        convertedMs.Seek(0, SeekOrigin.Begin);

                                        // Creează o nouă imagine din fluxul convertit
                                        Image convertedImage = Image.FromStream(convertedMs);

                                        // Adaugă imaginea convertită în dictionar
                                        imagesDictionary.Add(imageName, convertedImage);
                                    }
                                }
                            }
                        }
                    }
                }

                return imagesDictionary;
            }
            catch (Exception e)
            {
                MessageBox.Show("Eroare la selectarea imaginii: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Returneaza 2 parametri:1)AverageRating, 2)NumberOfReviews
        /// </summary>
        public static int [] MedieRecenzii( string connectionString, int id_produs )
        {
            try
            {
                string query = "SELECT AVG(nr_stele) AS AverageRating, COUNT(*) AS NumberOfReviews FROM Review WHERE id_produs = @ProductId";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", id_produs);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int averageRating = Convert.ToInt32(reader ["AverageRating"]);
                                int numberOfReviews = Convert.ToInt32(reader ["NumberOfReviews"]);
                                return new int [] { averageRating, numberOfReviews };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Eroare la Media de recenzii: " + e.Message);
            }
            return null;
        }

        public static DataTable SelectTop30Produse( string connectionString )
        {
            try
            {
                string query = @"
                            SELECT TOP 30 p.*
                            FROM Produse p
                            INNER JOIN (
                                SELECT id_produs, AVG(nr_stele) AS AvgRating
                                FROM Review
                                GROUP BY id_produs
                            ) r ON p.id_produs = r.id_produs
                            ORDER BY r.AvgRating DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Eroare la selectarea produselor: " + e.Message);
            }
            return null;
        }

        public static int GetUserID( string connectionString, string email, string telefon, string parola )
        {
            try
            {
                string query = "SELECT id_user FROM Useri WHERE parola = @parola";

                if (Aranjare.IsValidEmail(email))
                {
                    query += " AND email = @email";
                }
                else if (Aranjare.IsValidTelefon(telefon))
                {
                    query += " AND telefon = @telefon";
                }
                else
                {
                    MessageBox.Show("Autentificare eșuată. Verificați datele de autentificare sau nu v-ați conectat încă.");
                    return -1;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@parola", parola);
                        if (!string.IsNullOrEmpty(email))
                        {
                            command.Parameters.AddWithValue("@email", email);
                        }
                        else if (!string.IsNullOrEmpty(telefon))
                        {
                            command.Parameters.AddWithValue("@telefon", telefon);
                        }

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int idUtilizator))
                        {
                            return idUtilizator;
                        }
                        else
                        {
                            MessageBox.Show("Autentificare eșuată. Verificați datele de autentificare sau nu v-ați conectat încă.");
                            return -1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la autentificare: " + ex.Message);
                return -1;
            }
        }

        public static int GetFurnizorId( string connectionString, int user_Id )
        {
            try
            {
                string query = "SELECT id_furnizor FROM Furnizori WHERE id_user=@id_user";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_user", user_Id);
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return (int)result;
                    }
                    else
                    {
                        //MessageBox.Show("Autentificare eșuată. Verificați datele de autentificare sau nu v-ați conectat încă.");
                        return -1;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la autentificare: " + ex.Message);
                return -1;
            }
        }

        public static int GetAdminId( string connectionString, int user_Id )
        {
            try
            {
                string query = "SELECT id_admin FROM Admini WHERE id_user=@id_user";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_user", user_Id);
                    connection.Open();

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        return (int)result;
                    }
                    else
                    {
                        //MessageBox.Show("Autentificare eșuată. Verificați datele de autentificare sau nu v-ați conectat încă.");
                        return -1;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la autentificare: " + ex.Message);
                return -1;
            }
        }


        //Claudiu

        //Puia
        public static void InsertProdus( string connectionString, Image image, int id_Categorie, int id_Furnizor, string NumeProdus, int Cantitate, decimal Pret, string Descriere, List<string> NumeListaSpecificatii, List<string> ValoareListaSpecificatii )
        {
            try
            {
                NumeProdus = Aranjare.FormatName(NumeProdus);
                Descriere = Aranjare.FormatName(Descriere);
                foreach (string s in NumeListaSpecificatii)
                {
                    string specificatieFormatata = s.ToLower().Trim();
                    //pt fiecare numespecificatii se apelaeaza aranjare.FormatName
                }
                foreach (string s in ValoareListaSpecificatii)
                {
                    string valoarespecificatie = s.ToLower().Trim();
                    //pt fiecare numespecificatii se apelaeaza aranjare.FormatName
                }

                if (string.IsNullOrWhiteSpace(NumeProdus) ||

                    string.IsNullOrWhiteSpace(Descriere))
                {
                    MessageBox.Show("Toate câmpurile trebuie completate.");
                    return;
                }
                //apelare metoda adauga imagini. verifica daca toate sunt goale nu tr lasa sa adaugi produsul
                //implementatre intergari:-insert image apelare, -adauga specificatii (o alta functie pt adaugare specificatii) apelare foreach
                //

                string query = @"INSERT INTO Produse (nume, pret, id_categorie, cantitate, descriere, id_furnizor) " +
                                      "VALUES (@Nume, @Pret, @Categorie, @Cantitate, @Descriere, @Furnizor)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    cmd.CommandText = @"INSERT INTO Produse (nume, pret, id_categorie, cantitate, descriere, id_furnizor) " +
                                      "VALUES (@Nume, @Pret, @Categorie, @Cantitate, @Descriere, @Furnizor)";

                    cmd.Parameters.AddWithValue("@Nume", NumeProdus);
                    cmd.Parameters.AddWithValue("@Pret", (Pret));
                    cmd.Parameters.AddWithValue("@Categorie", id_Categorie);
                    cmd.Parameters.AddWithValue("@Furnizor", id_Furnizor);
                    cmd.Parameters.AddWithValue("@Cantitate", (Cantitate));
                    cmd.Parameters.AddWithValue("@Descriere", Descriere);


                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Produsul a fost adăugat cu succes în baza de date!");
                }

                throw new Exception("apelare metoda adauga imagini,implementatre intergari:-insert image apelare, -adauga specificatii ");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adăugarea produsului în baza de date: " + ex.Message);
            }
        }

        //Horia
    }
}
