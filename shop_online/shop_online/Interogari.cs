using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace shop_online
{
   class Interogari
    {
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

    }
}
