using System;
using System.Drawing;
using Xunit;
using shop_online;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ShopOnline.Tests
{
    public class ShopOnlineTests
    {
        private ShopOnlineTests shopOnline; // Presupunând că ShopOnline este clasa care conține metoda InsertImagine

        public ShopOnlineTests()
        {
            // Inițializare obiect ShopOnline în constructor
            shopOnline = new ShopOnlineTests();
        }

        [Fact]
        public void TestInsertImage()
        {
            // Definirea parametrilor necesari pentru testare
            string connectionString = "your_connection_string";
            int productId = 1;
            string imageName = "image.jpg"; // Numele imaginii cu extensie

            // Încărcarea imaginii din sistemul de fișiere (asigurați-vă că calea este corectă)
            Image image = Image.FromFile("path_to_image/image.jpg");

            // Apelarea metodei pentru a insera imaginea
            // bool insertionResult = Interogari.InsertImagine(connectionString, image, productId, imageName);

            // Verificarea dacă inserarea a fost efectuată cu succes
            // Assert.True(insertionResult, "Image insertion failed.");
        }

        [Fact]
        public void SignUp_All_Parameters_Null()
        {//pt singup
         //Arrange connection string 
            string connectionString = null;
            string nume = null;
            string email = null;
            string parola = null;
            string telefon = null;
            string judet = null;
            string oras = null;
            string strada = null;
            int numar = 0;
            //Act  
            bool value = Interogari.SignUp(connectionString, nume, email, parola, telefon, judet, oras, strada, numar);
            //Asert  
            Assert.Equal(false, value);
        }
        [Fact]
        public void SignUp_All_Parameters_fill()
        {//pt singup
         //Arrange connection string 
            string connectionString = null;
            string nume = "roly";
            string email = "roly593593.com";
            string parola = "12.,.l";
            string telefon ="09jgjdf";
            string judet = "323432324";
            string oras = "324";
            string strada = "*^$^*";
            int numar = 2;
            //Act  
            bool value = Interogari.SignUp(connectionString, nume, email, parola, telefon, judet, oras, strada, numar);
            //Asert  
            Assert.Equal(false, value);
        }
        [Fact]
        public void Login_Null()
        {
            string connectionString = null;
            string email = null;
            string parola = null;
            string telefon = null;
            bool value = Interogari.Login(connectionString, email, parola, telefon);
            Assert.Equal(false, value);

        }
        public void Login_fill()
        {
            string connectionString = null;
            string email = "32435claus";
            string parola = "";
            string telefon = "fdsgdf";
            bool value = Interogari.Login(connectionString, email, parola, telefon);
            Assert.Equal(false, value);

        }
    }
    
    
}
