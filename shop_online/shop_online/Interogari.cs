using System;
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    /* string checkQuery = @"SELECT COUNT(*) FROM Useri
                                   WHERE nume = @nume AND  parola = @parola
                                   AND ( telefon = @telefon OR email = @email) 
                                   AND judet = @judet AND oras = @oras 
                                   AND strada = @strada AND numar = @numar";*/

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


                    // Verifică dacă utilizatorul există deja în baza de date
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                    {
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
                    }
                    string insertAddressQuery = @"INSERT INTO Adresa 
                                          (judet, oras, strada, numar) 
                                          VALUES (@judet, @oras, @strada, @numar); 
                                          SELECT SCOPE_IDENTITY();";
                    int userId;
                    using (SqlCommand insertAddressCommand = new SqlCommand(insertAddressQuery, connection))
                    {

                        insertAddressCommand.Parameters.AddWithValue("@judet", judet);
                        insertAddressCommand.Parameters.AddWithValue("@oras", oras);
                        insertAddressCommand.Parameters.AddWithValue("@strada", strada);
                        insertAddressCommand.Parameters.AddWithValue("@numar", numar);

                        // Obținem ID-ul 
                        userId = Convert.ToInt32(insertAddressCommand.ExecuteScalar());
                    }

                    string insertUserQuery = @"INSERT INTO Useri
                                    (username, email, parola, telefon,id_adresa) 
                                    VALUES (@nume, @email, @parola, @telefon,@id_adresa);                                   "; // Obținem ID-ul utilizatorului inserat

                    using (SqlCommand insertUserCommand = new SqlCommand(insertUserQuery, connection))
                    {
                        insertUserCommand.Parameters.AddWithValue("@nume", nume);
                        insertUserCommand.Parameters.AddWithValue("@email", email);
                        insertUserCommand.Parameters.AddWithValue("@parola", parola);
                        insertUserCommand.Parameters.AddWithValue("@telefon", telefon);
                        insertUserCommand.Parameters.AddWithValue("@id_adresa", userId);

                        insertUserCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Utilizatorul a fost adăugat cu succes în baza de date.");
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("A apărut o eroare la inserarea în baza de date: " + ex.Message);

                }
            }
            return false;
        }

        public static bool Login( string connectionString, string email, string telefon, string parola )
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT COUNT(*) FROM Useri WHERE parola = @parola";

                    if (!string.IsNullOrEmpty(email))
                    {
                        query += " AND email = @email";
                    }
                    else if (!string.IsNullOrEmpty(telefon))
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

        public static void InsertImagine( string connectionString, Image imagine, int id_produs, ImageFormat format )
        {
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

                string query = "INSERT INTO Imagini (imagine,id_produs) VALUES (@ImageData,@id_produs)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ImageData", imageData);
                        command.Parameters.AddWithValue("@id_produs", id_produs);
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

        public static Image [] SelectImagine( string connectionString, int id_produs )
        {
            //throw new NotImplementedException("Trebuie sa aflam formatul din baza de date!!!!!");
            try
            {
                byte [] imageDataFromDatabase;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT imagine FROM Imagini WHERE id_produs = @ProductId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductId", id_produs);
                    connection.Open();
                    imageDataFromDatabase = (byte [])command.ExecuteScalar();
                }

                // Verificăm dacă am primit date de la baza de date
                if (imageDataFromDatabase == null || imageDataFromDatabase.Length == 0)
                {
                    //MessageBox.Show("Nu există imagini pentru produsul cu ID-ul specificat.");
                    Console.WriteLine("Nu există imagini pentru produsul cu ID-ul specificat.");
                    return null; // Nu există imagini disponibile
                }

                // Convertim array-ul de bytes într-o imagine
                using (MemoryStream ms = new MemoryStream(imageDataFromDatabase))
                {
                    Image imageFromDatabase = Image.FromStream(ms);
                    return new Image [] { imageFromDatabase };
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Eroare la inserare imagine: " + e.Message);
                return null;
            }
        }

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
                    // Fără criterii de căutare valide
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





        //Claudiu

        //Puia

        //Horia
    }
}
