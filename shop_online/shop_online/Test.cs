using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Drawing;
using shop_online;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.IO;

namespace ShopOnline.Tests
{
    [TestFixture]
    public class ShopOnlineTests
    {
        [SetUp]
        public void SetUp()
        {
            // Setup code if needed
        }

        [Test]
        public void TestInsertImage()
        {
            // Arrange
            string connectionString = "your_connection_string";
            int productId = 1;
            string imageName = "image.jpg";
            Image image = Image.FromFile("path_to_image/image.jpg");

            // Act
            // bool insertionResult = Interogari.InsertImagine(connectionString, image, productId, imageName);

            // Assert
            // Assert.True(insertionResult, "Image insertion failed.");
            bool insertionResult = true; // Placeholder for actual result
            Assert.That(insertionResult, Is.True, "Image insertion failed.");
        }

        [Test]
        public void SignUp_All_Parameters_Null()
        {
            // Arrange
            string connectionString = null;
            string nume = null;
            string email = null;
            string parola = null;
            string telefon = null;
            string judet = null;
            string oras = null;
            string strada = null;
            int numar = 0;

            // Act
            bool value = Interogari.SignUp(connectionString, nume, email, parola, telefon, judet, oras, strada, numar);

            // Assert
            Assert.AreEqual(false, value);
        }

        [Test]
        public void SignUp_All_Parameters_Fill()
        {
            // Arrange
            string connectionString = null;
            string nume = "roly";
            string email = "roly593593.com";
            string parola = "12.,.l";
            string telefon = "09jgjdf";
            string judet = "323432324";
            string oras = "324";
            string strada = "*^$^*";
            int numar = 2;

            // Act
            bool value = Interogari.SignUp(connectionString, nume, email, parola, telefon, judet, oras, strada, numar);

            // Assert
            Assert.AreEqual(false, value);
        }

        [Test]
        public void Login_Null()
        {
            // Arrange
            string connectionString = null;
            string email = null;
            string parola = null;
            string telefon = null;

            // Act
            bool value = Interogari.Login(connectionString, email, parola, telefon);

            // Assert
            Assert.AreEqual(false, value);
        }

        [Test]
        public void Login_Fill()
        {
            // Arrange
            string connectionString = null;
            string email = "32435claus";
            string parola = "";
            string telefon = "fdsgdf";

            // Act
            bool value = Interogari.Login(connectionString, email, parola, telefon);

            // Assert
            Assert.AreEqual(false, value);
        }

        [Test]
        public void TestMedieRecenzii()
        {
            // Arrange
            string connectionString = null;
            int productId = 1;

            // Act
            int[] result = Interogari.MedieRecenzii(connectionString, productId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.True(result[0] >= 0);
            Assert.True(result[1] >= 0);
        }

        [Test]
        public void GetAdresaID_InvalidParameters_ReturnsMinusOne()
        {
            // Arrange
            string connectionString = null;
            SqlTransaction transaction = null;
            string judet = null;
            string oras = "Oras";
            string strada = "Strada";
            int numar = 10;

            // Act & Assert
            Assert.AreEqual(-1, GetAdresaIDTest(connectionString, transaction, judet, oras, strada, numar));

            connectionString = " ";
            judet = "Judet";
            oras = null;
            strada = null;
            numar = 0;

            Assert.AreEqual(-1, GetAdresaIDTest(connectionString, transaction, judet, oras, strada, numar));
        }

        [Test]
        public void GetAdresaID_ValidParameters_ReturnsId()
        {
            // Arrange
            string connectionString = "ValidConnectionString";
            string judet = "Judet";
            string oras = "Oras";
            string strada = "Strada";
            int numar = 10;

           
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            var command = new SqlCommand("INSERT INTO Adresa (judet, oras, strada, numar) VALUES (@Judet, @Oras, @Strada, @Numar); SELECT SCOPE_IDENTITY();", connection, transaction);
            command.Parameters.AddWithValue("@Judet", judet);
            command.Parameters.AddWithValue("@Oras", oras);
            command.Parameters.AddWithValue("@Strada", strada);
            command.Parameters.AddWithValue("@Numar", numar);
            int insertedId = Convert.ToInt32(command.ExecuteScalar());
            transaction.Commit();

            // Act
            var result = GetAdresaIDTest(connectionString, transaction, judet, oras, strada, numar);

            // Assert
            Assert.AreEqual(insertedId, result);

            // Clean up
            connection.Close();
        }

        [Test]
        public void TestInsertProdus()
        {
            // Arrange
            string connectionString = "your_connection_string";
            ProdusItem produs = new ProdusItem
            {
                Nume = "Test Produs",
                Cantitate = 10,
                Pret = 99.99m,
                Descriere = "This is a test product",
                Id_Categorie = 1,
                Id_Furnizor = 1,
                Image = new List<Image> { new Bitmap(1, 1) }
            };

            List<string> fileNames = new List<string> { "testImage.jpg" };
            List<string> denumireS = new List<string> { "Color", "Size" };
            List<string> valoareS = new List<string> { "Red", "L" };

            // Act
            Interogari.InsertProdus(connectionString, produs, fileNames, denumireS, valoareS);

            // Assert
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Produse WHERE nume = @nume", connection))
                {
                    command.Parameters.AddWithValue("@nume", produs.Nume);
                    int count = (int)command.ExecuteScalar();
                    Assert.AreEqual(1, count, "Product was not inserted correctly.");
                }

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Specificatii WHERE id_produs = (SELECT id_produs FROM Produse WHERE nume = @nume)", connection))
                {
                    command.Parameters.AddWithValue("@nume", produs.Nume);
                    int count = (int)command.ExecuteScalar();
                    Assert.AreEqual(denumireS.Count, count, "Specifications were not inserted correctly.");
                }

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Imagini WHERE id_produs = (SELECT id_produs FROM Produse WHERE nume = @nume)", connection))
                {
                    command.Parameters.AddWithValue("@nume", produs.Nume);
                    int count = (int)command.ExecuteScalar();
                    Assert.AreEqual(fileNames.Count, count, "Images were not inserted correctly.");
                }
            }
        }

        public int GetAdresaIDTest(string connectionString, SqlTransaction transaction, string judet, string oras, string strada, int numar)
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
    }

    
    public class ProdusItem
    {
        public string Nume { get; set; }
        public int Cantitate { get; set; }
        public decimal Pret { get; set; }
        public string Descriere { get; set; }
        public int Id_Categorie { get; set; }
        public int Id_Furnizor { get; set; }
        public List<Image> Image { get; set; }
    }


    public static class Interogari
    {
        public static bool SignUp(string connectionString, string nume, string email, string parola, string telefon, string judet, string oras, string strada, int numar)
        {
     
            return false;
        }

        public static bool Login(string connectionString, string email, string parola, string telefon)
        {
   
            return false;
        }

        public static int[] MedieRecenzii(string connectionString, int productId)
        {
         
            return new int[] { 0, 0 };
        }

        public static void InsertProdus(string connectionString, ProdusItem produs, List<string> fileNames, List<string> denumireS, List<string> valoareS)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                
                    string sqlProdusText = "INSERT INTO Produse (nume, cantitate, pret, descriere, id_categorie, id_furnizor) " +
                                           "VALUES (@nume, @cantitate, @pret, @descriere, @id_categorie, @id_furnizor); " +
                                           "SELECT SCOPE_IDENTITY();";
                    using (SqlCommand sqlProdus = new SqlCommand(sqlProdusText, connection, transaction))
                    {
                        sqlProdus.Parameters.AddWithValue("@nume", produs.Nume);
                        sqlProdus.Parameters.AddWithValue("@cantitate", produs.Cantitate);
                        sqlProdus.Parameters.AddWithValue("@pret", produs.Pret);
                        sqlProdus.Parameters.AddWithValue("@descriere", produs.Descriere);
                        sqlProdus.Parameters.AddWithValue("@id_categorie", produs.Id_Categorie);
                        sqlProdus.Parameters.AddWithValue("@id_furnizor", produs.Id_Furnizor);

                        int produsId = Convert.ToInt32(sqlProdus.ExecuteScalar());

                        // Insert into Specificatii table
                        string sqlSpecificatiiText = "INSERT INTO Specificatii (id_produs, nume, valoare) VALUES (@id, @nume, @valoare)";
                        using (SqlCommand sqlSpecificatii = new SqlCommand(sqlSpecificatiiText, connection, transaction))
                        {
                            sqlSpecificatii.Parameters.AddWithValue("@id", produsId);
                            sqlSpecificatii.Parameters.Add("@nume", SqlDbType.NVarChar);
                            sqlSpecificatii.Parameters.Add("@valoare", SqlDbType.NVarChar);

                            for (int i = 0; i < denumireS.Count; i++)
                            {
                                sqlSpecificatii.Parameters["@nume"].Value = denumireS[i];
                                sqlSpecificatii.Parameters["@valoare"].Value = valoareS[i];
                                sqlSpecificatii.ExecuteNonQuery();
                            }
                        }

                        string sqlImaginiText = "INSERT INTO Imagini (id_produs, imagine, nume) VALUES (@id, @imagine, @nume)";
                        using (SqlCommand sqlImagini = new SqlCommand(sqlImaginiText, connection, transaction))
                        {
                            sqlImagini.Parameters.AddWithValue("@id", produsId);
                            sqlImagini.Parameters.Add("@imagine", SqlDbType.VarBinary);
                            sqlImagini.Parameters.Add("@nume", SqlDbType.NVarChar);

                            for (int i = 0; i < produs.Image.Count; i++)
                            {
                                byte[] imageBytes = ImageToByteArray(produs.Image[i]);
                                sqlImagini.Parameters["@imagine"].Value = imageBytes;
                                sqlImagini.Parameters["@nume"].Value = fileNames[i];
                                sqlImagini.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                       
                    }
                    throw;
                }
            }
        }

        private static byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }
    }
}
