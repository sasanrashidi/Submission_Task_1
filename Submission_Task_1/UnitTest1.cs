using UserClassLibrary;

namespace UserRegistrationService
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Is_The_Created_User_Unique() // testar om den skapade användaren är unik. Om det inte är unikt och användaren redan existerar ska försöket blockeras.
        {
            //Arrange - Skapar två användare med identiska anvnändarnamn för att simulera ett försök till dubbelt användarnamn.
            var username1 = new RegistrationUser("Sasan", "473628@b3", "sasan@gmail.com");
            var username2 = new RegistrationUser("Sasan", "473628@b3", "sasan@gmail.com");
            var userLibrary = new UserLibrary(); // Skapar en användarbiblioteksinstant.

            //Act - Försöker lägga till båda användarna till systemet
            userLibrary.AddUser(username1);

            //Assert - Förväntar oss att ett undantag kastas vid försök att lägga till den andra användaren, eftersom användarnamnet redan existerar.
            Assert.ThrowsException<ArgumentException>(() => userLibrary.AddUser(username2), "The username is already in use");
        }


        [TestMethod]
        public void Is_The_Created_User_Username_To_Long_And_ThrowExeption() // Testar om användarnamnet för den skapade användaren är för långt och kastar ett undantag.
        {
            //Arrange och Act - Försöker skapa en användare med ett användarnamn som överstiger 20 tecken och fångar det förväntade undantaget.
            var invalidexeption1 = Assert.ThrowsException<ArgumentException>(() => new RegistrationUser("TheUsernameismorethen20charactersLong", "secretpassword", "sasan@gmail.com")); // Skapar en användare med ett lösenord som är längre än 20 tecken och fångar det förväntade undantaget.

            //Assert - Kontrollerar att det kastade undantaget innehåller det korrekta felmeddelandet.
            Assert.AreEqual("The username must be between 5 & 20 characters long.", invalidexeption1.Message);

        }

        [TestMethod]
        public void Is_The_Created_User_Username_Too_Short_And_ThrowExeption() // Testar om användarnamnet för den skapade användaren är för kort och kastar ett undantag.
        {
            //Arrange and Act - Försöker skapa en användare med ett användarnamn under 5 tecken och fångar det förväntade undantaget.
            var invalidexeption2 = Assert.ThrowsException<ArgumentException>(() => new RegistrationUser("User", "secretpassword", "sasan@gmail.com")); // Skapar en användare med ett kort användarnamn och fångar det förväntade undantaget.

            //Assert - Kontrollerar att det kastade undantaget innehåller det korrekta felmeddelandet.
            Assert.AreEqual("The username must be between 5 & 20 characters long.", invalidexeption2.Message);

        }

        [TestMethod]
        public void Is_The_Created_Email_Correctly_Format() // Testar om e-postadressen för den skapade användaren är korrekt formaterad.
        {
            //Arrange och Act - Skapar en användare med en giltig e-postadress och testar sedan med en ogiltig.
            var useremail1 = new RegistrationUser("Sasan", "23145343@bb", "Sasan@gmail.com");

            //Assert - Kontrollerar att den skapade användarens e-postadress är korrekt.
            Assert.AreEqual("Sasan@gmail.com", useremail1.Email);

            //Försöker skapa en användare med en ogiltig e-postadress och kontrollerar att rätt undantag kastas.
            var exeption = Assert.ThrowsException<ArgumentException>(() => new RegistrationUser("Sasan", "12344233b@fa", "BadEmail"));
            Assert.AreEqual("The email must end with '@gmail.com'", exeption.Message); // Kontrollerar att felmeddelandet för det förväntade undantaget är korrekt.
        }

        [TestMethod]
        public void Is_The_Created_User_Working_Return_Confirmation_Message() // Testar om det returnerar korrekt bekräfteselsemeddelande efter lyckad registrering av en användare.
        {
            // Arrange - Skar en användare och en instans av användarbibloteket.
            var userLibrary = new UserLibrary();
            var user = new RegistrationUser("UnitTest", "Sasanärdenbästa", "Sasan.Rashidi@gmail.com");

            //Act - Registrerar användaren och fångar bekräftelsemeddelandet.
            string success = userLibrary.AddUser(user);

            // Assert - Kontrollerar att bekräftelsemeddelandet innehåller användarens användarnamn.
            Assert.IsTrue(success.Contains($"Your user {user.Username} has been created!"));
        }

        [TestMethod]
        public void Does_Password_Contain_Special_Character() //Testar om lösenordet för användaren innehåller minst ett specialtecken.
        {
            // Arrange - Skapar en användarinstans för att testa lösenordets innehåll.
            var userLibrary = new UserLibrary();

            // Act & Assert
            // Kontrollerar om metoden ContainsSpecialCharacter i RegistrationUser-klassen returnerar true när lösenordet innehåller ett specialtecken
            Assert.IsTrue(new RegistrationUser("Sasan", "secretp@ssword", "sasan@gmail.com").ContainsSpecialCharacter("secretp@ssword"));
            // Kontrollerar om metoden ContainsSpecialCharacter i RegistrationUser-klassen returnerar false när lösenordet inte innehåller något specialtecken
            Assert.IsFalse(new RegistrationUser("Sasan", "password", "sasan@gmail.com").ContainsSpecialCharacter("password"));
        }

    }
}
