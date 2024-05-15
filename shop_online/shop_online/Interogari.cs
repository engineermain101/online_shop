﻿using System;
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
        public static int GetUserIDbyEmail( string connectionString, string email )
        {
            try
            {
                if (!Aranjare.IsValidEmail(email) || string.IsNullOrWhiteSpace(connectionString))
                {
                    MessageBox.Show("Adresa de email nu este validă.");
                    return -1;
                }

                string query = "SELECT id_user FROM Useri WHERE email = @Email";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int userId))
                    {
                        return userId;
                    }
                    else
                    {
                        MessageBox.Show("Nu există utilizator cu această adresă de email.");
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la obținerea ID-ului utilizatorului: " + ex.Message);
                return -1;
            }
        }
        public static List<string> GetAllUserEmails( string connectionString )
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return null;

            List<string> emails = new List<string>();
            try
            {
                string query = "SELECT email FROM Useri";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            emails.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la extragerea adreselor de email: " + ex.Message);
                return null;
            }

            return emails;
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
        public static List<string> GetAllAdminRoles( string connectionString )
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return null;

            List<string> roles = new List<string>();

            try
            {
                string query = "SELECT DISTINCT rol FROM Admini";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la obținerea rolurilor de administrator: " + ex.Message);
                return null;
            }

            return roles;
        }
        public static List<string> GetAdminUserEmails( string connectionString )
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                return null;

            List<string> adminUserEmails = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT U.email
                FROM Useri U
                INNER JOIN Admini A ON U.id_user = A.id_user";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                adminUserEmails.Add(reader ["email"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la obținerea emailurilor de utilizatori admin: " + ex.Message);
            }

            return adminUserEmails;
        }
        public static bool CheckIfAdminExists( string connectionString, int idUser )
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Admini WHERE id_user = @IdUser";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUser", idUser);
                    connection.Open();

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la verificarea existenței administratorului: " + ex.Message);
                return false;
            }
        }

        public static bool AdaugainCos( string connectionString, int nr_bucati, decimal pret, int id_user, int id_produs )
        {
            if (id_user <= 0 || id_produs <= 0)
            {
                MessageBox.Show("Eroare la adaugarea in cos. Incercati inca o data.");
                return false;
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
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la adaugare in cos. " + ex.ToString());
                return false;
            }
            return true;
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

        public static bool DeleteProdusdinCos( string connectionString, int id_user, int id_produs )
        {
            if (id_user <= 0 || id_produs <= 0)
            {
                MessageBox.Show("Eroare la adaugarea in cos.");
                return false;
            }
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
                return false;
            }
            return true;
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


        public static DataTable GetSpecificatiiSiRecenzii( string connectionString, int idProdus )
        {
            DataTable dataTable = new DataTable();

            try
            {
                DataTable specificatii = GetSpecificatii(connectionString, idProdus);
                DataTable recenzii = GetRecenzii(connectionString, idProdus);

                foreach (DataColumn column in specificatii.Columns)
                {
                    dataTable.Columns.Add(column.ColumnName, column.DataType);
                }

                foreach (DataColumn column in recenzii.Columns)
                {
                    dataTable.Columns.Add(column.ColumnName, column.DataType);
                }

                foreach (DataRow row in specificatii.Rows)
                {
                    dataTable.Rows.Add(row.ItemArray);
                }

                foreach (DataRow row in recenzii.Rows)
                {
                    dataTable.Rows.Add(row.ItemArray);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return dataTable;
        }
        public static DataTable GetSpecificatii( string connectionString, int idProdus )
        {
            if (idProdus <= 0)
                return null;

            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT nume, valoare FROM Specificatii WHERE id_produs = @idprodus";
                    command.Parameters.AddWithValue("@idprodus", idProdus);
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dataTable;
        }
        public static DataTable GetRecenzii( string connectionString, int idProdus )
        {
            if (idProdus <= 0)
            {
                return null;
            }

            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    SELECT R.*, U.username
                    FROM Review R
                    INNER JOIN Useri U ON R.id_user = U.id_user
                    WHERE R.id_produs = @idprodus";

                    command.Parameters.AddWithValue("@idprodus", idProdus);
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dataTable;
        }

        public static bool InsertAdmin( string connectionString, int idUser, string rol )
        {
            if (string.IsNullOrWhiteSpace(rol) || string.IsNullOrWhiteSpace(connectionString) || idUser <= 0)
                return false;

            try
            {
                rol = Aranjare.FormatName(rol);
                string query = @"INSERT INTO Admini (id_user, rol) VALUES (@IdUser, @Rol)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUser", idUser);
                    command.Parameters.AddWithValue("@Rol", rol);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la inserarea administratorului: " + ex.Message);
                return false;
            }
            return true;
        }
        public static bool DeleteAdmin( string connectionString, int idUser )
        {
            if (string.IsNullOrWhiteSpace(connectionString) || idUser <= 0)
                return false;

            try
            {
                string query = @"DELETE FROM Admini WHERE id_user = @IdUser";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUser", idUser);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la ștergerea administratorului: " + ex.Message);
                return false;
            }
            return true;
        }
        public static bool UpdateAdmin( string connectionString, int idUser, string newrol )
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(newrol) || idUser <= 0)
            {
                return false;
            }

            try
            {
                string query = @"UPDATE Admini SET rol = @Rol WHERE id_user = @IdUser";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUser", idUser);
                    command.Parameters.AddWithValue("@Rol", newrol);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("A apărut o eroare la actualizarea administratorului: " + ex.Message);
                return false;
            }

            return true;
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
        public static bool AdaugaFurnizor( string connectionString, int id_user, string iban, string nume_firma, string judet, string oras, string strada, int numar )
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(iban) ||
                string.IsNullOrWhiteSpace(nume_firma) || string.IsNullOrWhiteSpace(judet) ||
                string.IsNullOrWhiteSpace(oras) || string.IsNullOrWhiteSpace(strada) || numar < 1 || id_user < 1)
            {
                return false;
            }
            nume_firma = Aranjare.FormatName(nume_firma);
            judet = Aranjare.FormatName(judet);
            oras = Aranjare.FormatName(oras);
            strada = Aranjare.FormatName(strada);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO Furnizori (id_user, iban, nume_firma, id_adresa) VALUES (@IdUser, @Iban, @NumeFirma, @IdAdresa)";
                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@IdUser", id_user);
                            command.Parameters.AddWithValue("@Iban", iban);
                            command.Parameters.AddWithValue("@NumeFirma", nume_firma);

                            int id_adresa = GetAdresaID(connectionString, transaction, judet, oras, strada, numar);
                            if (id_adresa < 1)
                            {
                                bool adresaAdaugata = AdaugaAdresa(connectionString, transaction, judet, oras, strada, numar);
                                if (!adresaAdaugata)
                                {
                                    transaction.Rollback();
                                    return false;
                                }

                                id_adresa = GetAdresaID(connectionString, transaction, judet, oras, strada, numar);
                            }
                            command.Parameters.AddWithValue("@IdAdresa", id_adresa);

                            command.ExecuteNonQuery();
                            transaction.Commit();
                            return true;
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("A apărut o eroare la adăugarea furnizorului: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        public static bool StergeFurnizorDupaNume( string connectionString, SqlTransaction transaction, string numeFirma )
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(numeFirma) || transaction == null)
            {
                return false;
            }

            string query = "DELETE FROM Furnizori WHERE NumeFirma = @NumeFirma";
            try
            {
                using (SqlCommand command = new SqlCommand(query, transaction.Connection, transaction))
                {
                    command.Parameters.AddWithValue("@NumeFirma", numeFirma);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static int VerificaAdresaExistenta( string connectionString, SqlTransaction transaction, string judet, string oras, string strada, int numar )
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(judet) || transaction == null ||
                string.IsNullOrWhiteSpace(oras) || string.IsNullOrWhiteSpace(strada) || numar < 1)
            {
                return -1;
            }
            string query = "SELECT id_adresa FROM Adresa WHERE judet = @Judet AND oras = @Oras AND strada = @Strada AND numar = @Numar";
            try
            {
                using (SqlCommand command = new SqlCommand(query, transaction.Connection, transaction))
                {
                    command.Parameters.AddWithValue("@Judet", judet);
                    command.Parameters.AddWithValue("@Oras", oras);
                    command.Parameters.AddWithValue("@Strada", strada);
                    command.Parameters.AddWithValue("@Numar", numar);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return (int)result;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception) { return -1; }
        }
        private static bool AdaugaAdresa( string connectionString, SqlTransaction transaction, string judet, string oras, string strada, int numar )
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(judet) || transaction == null ||
                string.IsNullOrWhiteSpace(oras) || string.IsNullOrWhiteSpace(strada) || numar < 1)
            {
                return false;
            }

            string query = "INSERT INTO Adresa (judet, oras, strada, numar) VALUES (@Judet, @Oras, @Strada, @Numar)";
            try
            {
                using (SqlCommand command = new SqlCommand(query, transaction.Connection, transaction))
                {
                    command.Parameters.AddWithValue("@Judet", judet);
                    command.Parameters.AddWithValue("@Oras", oras);
                    command.Parameters.AddWithValue("@Strada", strada);
                    command.Parameters.AddWithValue("@Numar", numar);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static int GetAdresaID( string connectionString, SqlTransaction transaction, string judet, string oras, string strada, int numar )
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(judet) || transaction == null ||
                string.IsNullOrWhiteSpace(oras) || string.IsNullOrWhiteSpace(strada) || numar < 1)
            {
                return -1;
            }

            string query = "SELECT id_adresa FROM Adresa WHERE judet = @Judet AND oras = @Oras AND strada = @Strada AND numar = @Numar";
            try
            {
                using (SqlCommand command = new SqlCommand(query, transaction.Connection, transaction))
                {
                    command.Parameters.AddWithValue("@Judet", judet);
                    command.Parameters.AddWithValue("@Oras", oras);
                    command.Parameters.AddWithValue("@Strada", strada);
                    command.Parameters.AddWithValue("@Numar", numar);

                    object result = command.ExecuteScalar();
                    return (result != null && result != DBNull.Value) ? (int)result : -1;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        //Claudiu

        //Puia
        public static void InsertProdus( string connectionString, Image image, string NumeProdus, int Cantitate, double Pret, string Descriere, string NumeListaSpecificatii, string ValoareListaSpecificatii, int idCategorie, string numeImage, int id_furnizor )
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlCommand sqlProdus = new SqlCommand("INSERT INTO Produse (nume,cantitate,pret,descriere,id_categorie,id_furnizor) VALUES (@nume,@cantitate,@pret,@descriere,@id_categorie,@id_furnizor)" +
                                                            "SELECT SCOPE_IDENTITY();", connection, transaction);
                    sqlProdus.Parameters.AddWithValue("@nume", NumeProdus);
                    sqlProdus.Parameters.AddWithValue("@cantitate", Cantitate);
                    sqlProdus.Parameters.AddWithValue("@pret", Pret);
                    sqlProdus.Parameters.AddWithValue("@descriere", Descriere);
                    sqlProdus.Parameters.AddWithValue("@id_categorie", idCategorie);
                    sqlProdus.Parameters.AddWithValue("@id_furnizor", id_furnizor);
                    int produsId = Convert.ToInt32(sqlProdus.ExecuteScalar());

                    SqlCommand sqlSpecificatii = new SqlCommand("INSERT INTO Specificatii (id_produs,nume,valoare)" +
                                                    "VALUES (@id,@nume,@valoare)", connection, transaction);
                    sqlSpecificatii.Parameters.AddWithValue("@id", produsId);
                    sqlSpecificatii.Parameters.AddWithValue("@nume", NumeListaSpecificatii);
                    sqlSpecificatii.Parameters.AddWithValue("@valoare", ValoareListaSpecificatii);

                    SqlCommand sqlImagini = new SqlCommand("INSERT INTO Imagini (id_produs,imagine,nume)" +
                                                    "VALUES (@id,@imagine,@nume)", connection, transaction);
                    sqlSpecificatii.Parameters.AddWithValue("@id", produsId);
                    sqlSpecificatii.Parameters.AddWithValue("@imagine", image);
                    sqlSpecificatii.Parameters.AddWithValue("@nume", numeImage);

                }
                catch (Exception ex)
                {
                    // Rollback the transaction if any error occurs
                    Console.WriteLine("Error: " + ex.Message);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        Console.WriteLine("Rollback Error: " + exRollback.Message);
                    }
                }

                //Horia
            }
        }

        private static bool GestionareAdmin( string connectionString, string operatie, int idAdmin, int idUser = 0, string rol = null )
        {
            throw new NotImplementedException();
            try
            {
                string query = "";

                switch (operatie.ToLower())
                {
                    case "insert":
                        query = @"INSERT INTO Admini (id_admin, id_user, rol) VALUES (@IdAdmin, @IdUser, @Rol)";
                        break;
                    case "update":
                        query = @"UPDATE Admini SET id_user = @IdUser, rol = @Rol WHERE id_admin = @IdAdmin";
                        break;
                    case "delete":
                        query = @"DELETE FROM Admini WHERE id_admin = @IdAdmin";
                        break;
                    default:
                        MessageBox.Show("Operația specificată nu este validă.");
                        return false;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@IdAdmin", idAdmin);

                    if (operatie.ToLower() != "delete")
                    {
                        cmd.Parameters.AddWithValue("@IdUser", idUser);
                        cmd.Parameters.AddWithValue("@Rol", rol);
                    }

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    switch (operatie.ToLower())
                    {
                        case "insert":
                            MessageBox.Show("Adminul a fost adăugat cu succes în baza de date!");
                            break;
                        case "update":
                            if (rowsAffected > 0)
                                MessageBox.Show("Adminul a fost actualizat cu succes în baza de date!");
                            else
                                MessageBox.Show("Adminul cu id-ul specificat nu a fost găsit în baza de date!");
                            break;
                        case "delete":
                            if (rowsAffected > 0)
                                MessageBox.Show("Adminul a fost șters cu succes din baza de date!");
                            else
                                throw new Exception("Adminul cu id-ul specificat nu a fost găsit în baza de date!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare în gestionarea adminului în baza de date: " + ex.Message);
                return false;
            }
            return true;
        }

        //Horia
    }
}
