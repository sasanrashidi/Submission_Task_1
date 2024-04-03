using UserClassLibrary;

namespace UserRegistrationService
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Is_The_Created_User_Unique() // testar om den skapade anv�ndaren �r unik. Om det inte �r unikt och anv�ndaren redan existerar ska f�rs�ket blockeras.
        {
            //Arrange - Skapar tv� anv�ndare med identiska anvn�ndarnamn f�r att simulera ett f�rs�k till dubbelt anv�ndarnamn.
            var username1 = new RegistrationUser("Sasan", "473628@b3", "sasan@gmail.com");
            var username2 = new RegistrationUser("Sasan", "473628@b3", "sasan@gmail.com");
            var userLibrary = new UserLibrary(); // Skapar en anv�ndarbiblioteksinstant.

            //Act - F�rs�ker l�gga till b�da anv�ndarna till systemet
            userLibrary.AddUser(username1);

            //Assert - F�rv�ntar oss att ett undantag kastas vid f�rs�k att l�gga till den andra anv�ndaren, eftersom anv�ndarnamnet redan existerar.
            Assert.ThrowsException<ArgumentException>(() => userLibrary.AddUser(username2), "The username is already in use");
        }


        [TestMethod]
        public void Is_The_Created_User_Username_To_Long_And_ThrowExeption() // Testar om anv�ndarnamnet f�r den skapade anv�ndaren �r f�r l�ngt och kastar ett undantag.
        {
            //Arrange och Act - F�rs�ker skapa en anv�ndare med ett anv�ndarnamn som �verstiger 20 tecken och f�ngar det f�rv�ntade undantaget.
            var invalidexeption1 = Assert.ThrowsException<ArgumentException>(() => new RegistrationUser("TheUsernameismorethen20charactersLong", "secretpassword", "sasan@gmail.com")); // Skapar en anv�ndare med ett l�senord som �r l�ngre �n 20 tecken och f�ngar det f�rv�ntade undantaget.

            //Assert - Kontrollerar att det kastade undantaget inneh�ller det korrekta felmeddelandet.
            Assert.AreEqual("The username must be between 5 & 20 characters long.", invalidexeption1.Message);

        }

        [TestMethod]
        public void Is_The_Created_User_Username_Too_Short_And_ThrowExeption() // Testar om anv�ndarnamnet f�r den skapade anv�ndaren �r f�r kort och kastar ett undantag.
        {
            //Arrange and Act - F�rs�ker skapa en anv�ndare med ett anv�ndarnamn under 5 tecken och f�ngar det f�rv�ntade undantaget.
            var invalidexeption2 = Assert.ThrowsException<ArgumentException>(() => new RegistrationUser("User", "secretpassword", "sasan@gmail.com")); // Skapar en anv�ndare med ett kort anv�ndarnamn och f�ngar det f�rv�ntade undantaget.

            //Assert - Kontrollerar att det kastade undantaget inneh�ller det korrekta felmeddelandet.
            Assert.AreEqual("The username must be between 5 & 20 characters long.", invalidexeption2.Message);

        }

        [TestMethod]
        public void Is_The_Created_Email_Correctly_Format() // Testar om e-postadressen f�r den skapade anv�ndaren �r korrekt formaterad.
        {
            //Arrange och Act - Skapar en anv�ndare med en giltig e-postadress och testar sedan med en ogiltig.
            var useremail1 = new RegistrationUser("Sasan", "23145343@bb", "Sasan@gmail.com");

            //Assert - Kontrollerar att den skapade anv�ndarens e-postadress �r korrekt.
            Assert.AreEqual("Sasan@gmail.com", useremail1.Email);

            //F�rs�ker skapa en anv�ndare med en ogiltig e-postadress och kontrollerar att r�tt undantag kastas.
            var exeption = Assert.ThrowsException<ArgumentException>(() => new RegistrationUser("Sasan", "12344233b@fa", "BadEmail"));
            Assert.AreEqual("The email must end with '@gmail.com'", exeption.Message); // Kontrollerar att felmeddelandet f�r det f�rv�ntade undantaget �r korrekt.
        }

        [TestMethod]
        public void Is_The_Created_User_Working_Return_Confirmation_Message() // Testar om det returnerar korrekt bekr�fteselsemeddelande efter lyckad registrering av en anv�ndare.
        {
            // Arrange - Skar en anv�ndare och en instans av anv�ndarbibloteket.
            var userLibrary = new UserLibrary();
            var user = new RegistrationUser("UnitTest", "Sasan�rdenb�sta", "Sasan.Rashidi@gmail.com");

            //Act - Registrerar anv�ndaren och f�ngar bekr�ftelsemeddelandet.
            string success = userLibrary.AddUser(user);

            // Assert - Kontrollerar att bekr�ftelsemeddelandet inneh�ller anv�ndarens anv�ndarnamn.
            Assert.IsTrue(success.Contains($"Your user {user.Username} has been created!"));
        }

        [TestMethod]
        public void Does_Password_Contain_Special_Character() //Testar om l�senordet f�r anv�ndaren inneh�ller minst ett specialtecken.
        {
            // Arrange - Skapar en anv�ndarinstans f�r att testa l�senordets inneh�ll.
            var userLibrary = new UserLibrary();

            // Act & Assert
            // Kontrollerar om metoden ContainsSpecialCharacter i RegistrationUser-klassen returnerar true n�r l�senordet inneh�ller ett specialtecken
            Assert.IsTrue(new RegistrationUser("Sasan", "secretp@ssword", "sasan@gmail.com").ContainsSpecialCharacter("secretp@ssword"));
            // Kontrollerar om metoden ContainsSpecialCharacter i RegistrationUser-klassen returnerar false n�r l�senordet inte inneh�ller n�got specialtecken
            Assert.IsFalse(new RegistrationUser("Sasan", "password", "sasan@gmail.com").ContainsSpecialCharacter("password"));
        }

    }
}
