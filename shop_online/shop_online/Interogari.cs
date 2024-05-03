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
    static class Interogari
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
                            ) r ON p.id_produs = r.id_produs and p.cantitate>0
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

        public static void AdaugainCos( string connectionString, int nr_bucati, decimal pret, int id_user, int id_produs )
        {
            if (id_user <= 0 || id_produs <= 0)
            {
                MessageBox.Show("Eroare la adaugarea in cos.");
                return;
            }
            try
            {
                bool productExists = CheckIfProductExistsInCart(connectionString, id_user, id_produs);

                if (productExists)
                {
                    UpdateProductInCart(connectionString, id_user, id_produs, nr_bucati, pret * nr_bucati);
                }
                else
                {
                    AddProductToCart(connectionString, id_user, id_produs, nr_bucati, pret * nr_bucati);
                }
            }
            catch (Exception ex) { MessageBox.Show("Eroare la adaugare in cos. " + ex.ToString()); }
        }
        private static bool CheckIfProductExistsInCart( string connectionString, int id_user, int id_produs )
        {
            string query = "SELECT COUNT(*) FROM Cos WHERE id_user = @id_user AND id_produs = @id_produs";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_user", id_user);
                    command.Parameters.AddWithValue("@id_produs", id_produs);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private static void UpdateProductInCart( string connectionString, int id_user, int id_produs, int nr_bucati, decimal total_pret )
        {
            // string query = "UPDATE Cos SET nr_bucati = nr_bucati + @nr_bucati, total_pret = total_pret + @total_pret WHERE id_user = @id_user AND id_produs = @id_produs";
            string query = "UPDATE Cos SET nr_bucati = @nr_bucati, total_pret = @total_pret WHERE id_user = @id_user AND id_produs = @id_produs";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nr_bucati", nr_bucati);
                    command.Parameters.AddWithValue("@total_pret", total_pret);
                    command.Parameters.AddWithValue("@id_user", id_user);
                    command.Parameters.AddWithValue("@id_produs", id_produs);

                    command.ExecuteNonQuery();
                }
            }
        }
        private static void AddProductToCart( string connectionString, int id_user, int id_produs, int nr_bucati, decimal total_pret )
        {
            string query = "INSERT INTO Cos (id_user, id_produs, nr_bucati, total_pret) VALUES (@id_user, @id_produs, @nr_bucati, @total_pret)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_user", id_user);
                    command.Parameters.AddWithValue("@id_produs", id_produs);
                    command.Parameters.AddWithValue("@nr_bucati", nr_bucati);
                    command.Parameters.AddWithValue("@total_pret", total_pret);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable GetCos( string connectionString, int idUser )
        {
            DataTable dataTable = new DataTable();
            /* string query = @"
             SELECT p.*, c.*
             FROM Produse p
             INNER JOIN (
                 SELECT id_produs, id_user
                 FROM Cos
                 WHERE id_user = @idUser
                 GROUP BY id_produs, id_user
             ) c ON p.id_produs = c.id_produs
             ORDER BY p.id_produs DESC";*/


            string query = @"
            SELECT p.*, c.nr_bucati, c.total_pret
            FROM Produse p
            INNER JOIN (
                SELECT id_produs, nr_bucati, total_pret
                FROM Cos
                WHERE id_user = 3
                GROUP BY id_produs, nr_bucati, total_pret
            ) c ON p.id_produs = c.id_produs
            ORDER BY p.id_produs DESC;";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idUser", idUser);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Poți trata diferite tipuri de excepții SQL aici
                Console.WriteLine($"A apărut o excepție SQL: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                // Poți trata alte excepții aici
                Console.WriteLine($"A apărut o excepție: {ex.Message}");
                return null;
            }

            return dataTable;
        }

        public static void DeleteProdusdinCos( string connectionString, int id_user, int id_produs )
        {
            string query = "DELETE FROM Cos WHERE id_user = @id_user AND id_produs = @id_produs";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id_user", id_user);
                        command.Parameters.AddWithValue("@id_produs", id_produs);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la ștergerea produsului din coș: " + ex.Message);
            }
        }





        public static void TranzactieCumparareProdus( string connectionString, int id_user, List<ProductControl> produse, string metoda_plata )
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Connection string is null or empty.");
            }
            if (produse == null || produse.Count == 0)
            {
                throw new ArgumentException("Nu aveti produse in cos.");
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                if (connection.State != ConnectionState.Open)
                {
                    throw new Exception("Failed to open database connection.");
                }
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // VerificaCard(connection, transaction, card);
                        /*private static void VerificaCard( SqlConnection connection, SqlTransaction transaction, Card card )
                        {
                            throw new NotImplementedException("This is not implemented");
                        }*/
                        SaveTranzactie(connection, transaction, id_user, produse, metoda_plata);

                        UpdateProducts(connection, transaction, produse);
                        DeleteAllUserProductsFromCart(connection, transaction, id_user);

                        // Alte operații pe care doriți să le efectuați în cadrul tranzacției
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Transaction rolled back due to error: " + ex.Message);
                    }
                }
            }
        }
        private static void UpdateProducts( SqlConnection connection, SqlTransaction transaction, List<ProductControl> produse )
        {
            string query = "UPDATE Produse SET pret = @pret, cantitate = cantitate - @cantitate WHERE id_produs = @id_produs";

            foreach (ProductControl produs in produse)
            {
                int cantitateDisponibila = GetAvailableQuantity(connection, transaction, produs.GetProdus_ID());

                if (cantitateDisponibila < produs.GetBucatiProdusdinCos())
                {
                    // Dacă cantitatea din listă depășește cantitatea disponibilă în baza de date,
                    // poți gestiona situația aici, cum ar fi generarea unei excepții sau afișarea unui mesaj de avertizare.
                    throw new Exception($"Cantitatea pentru produsul: {produs.GetProdus().Nume} depășește cantitatea disponibilă în stoc.");
                }

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@pret", produs.GetProdus_Pret());
                    command.Parameters.AddWithValue("@cantitate", produs.GetBucatiProdusdinCos());
                    command.Parameters.AddWithValue("@id_produs", produs.GetProdus_ID());

                    command.ExecuteNonQuery();
                }
            }
        }
        private static int GetAvailableQuantity( SqlConnection connection, SqlTransaction transaction, int idProdus )
        {
            string query = "SELECT cantitate FROM Produse WHERE id_produs = @id_produs";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@id_produs", idProdus);
                return (int)command.ExecuteScalar();
            }
        }
        private static void DeleteAllUserProductsFromCart( SqlConnection connection, SqlTransaction transaction, int id_user )
        {
            string query = "DELETE FROM Cos WHERE id_user = @id_user";

            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@id_user", id_user);
                command.ExecuteNonQuery();
            }
        }
        private static void SaveTranzactie( SqlConnection connection, SqlTransaction transaction, int id_user, List<ProductControl> produse, string metoda_plata )
        {
            if (connection == null || transaction == null)
            {
                throw new ArgumentNullException("connection or transaction", "Connection or transaction is null.");
            }

            if (produse == null || produse.Count == 0)
            {
                throw new ArgumentException("Product list is null or empty.", nameof(produse));
            }

            string query = @"INSERT INTO Tranzactii (id_user, id_furnizor, id_produs, data, metoda_plata, suma, status) 
                     VALUES (@id_user, @id_furnizor, @id_produs, @data_tranzactie, @metoda_plata, @suma, @status);";

            DateTime dataTranzactie = DateTime.Now;
            using (SqlCommand insertCommand = connection.CreateCommand())
            {
                insertCommand.Transaction = transaction;
                insertCommand.CommandText = query;

                foreach (ProductControl produs in produse)
                {
                    insertCommand.Parameters.Clear(); 
                    insertCommand.Parameters.AddWithValue("@id_user", id_user);
                    insertCommand.Parameters.AddWithValue("@id_furnizor", produs.GetProdus().Id_Furnizor);
                    insertCommand.Parameters.AddWithValue("@id_produs", produs.GetProdus_ID());
                    insertCommand.Parameters.AddWithValue("@data_tranzactie", dataTranzactie);
                    insertCommand.Parameters.AddWithValue("@metoda_plata", metoda_plata);
                    insertCommand.Parameters.AddWithValue("@suma", produs.GetProdus_Pret() * produs.GetBucatiProdusdinCos());
                    insertCommand.Parameters.AddWithValue("@status", "In curs");

                    insertCommand.ExecuteNonQuery();
                }
            }
            /*foreach (ProductControl produs in produse)
            {
                using (SqlCommand insertCommand = new SqlCommand(query, connection, transaction))
                {
                    insertCommand.Parameters.AddWithValue("@id_user", id_user);
                    insertCommand.Parameters.AddWithValue("@id_furnizor", produs.GetProdus().Id_Furnizor);
                    insertCommand.Parameters.AddWithValue("@id_produs", produs.GetProdus_ID());
                    insertCommand.Parameters.AddWithValue("@data_tranzactie", dataTranzactie);
                    insertCommand.Parameters.AddWithValue("@metoda_plata", metoda_plata);
                    insertCommand.Parameters.AddWithValue("@suma", produs.GetProdus_Pret() * produs.GetBucatiProdusdinCos());
                    insertCommand.Parameters.AddWithValue("@status", "In curs");
                    insertCommand.ExecuteNonQuery();
                }
            }*/
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
