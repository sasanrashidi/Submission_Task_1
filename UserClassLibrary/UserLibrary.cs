// Klassdefinition för hantering av användare
using UserClassLibrary;

public class UserLibrary
{
    // Lista som innehåller alla registrerade användare. Används för att lagra användarinformation.
    List<RegistrationUser> Users = new List<RegistrationUser>();

    // Metod för att lägga till en ny användare
    public string AddUser(RegistrationUser user)
    {
        // Kontrollera först om det redan finns en användare med samma användarnamn i lista.
        // Detta för att säkerställa att varje användare är unik.
        if (Users.Exists(u => u.Username == user.Username))
        {
            // Om användarnamnet redan finns, Kastas ett undantag.
            // Detta säger att användaren redan existerar och därför är det inte tillgänglig. Det förhindrar registrering.
            throw new ArgumentException("The username is already in use");
        }

        // Om användarnamnet är unikt, läggs unvändaren till i listan.
        Users.Add(user);

        // Returnera ett meddelande som bekräftar att användaren har skapats. Positiv feedback om att operationen lyckades. 
        return $"Your user {user.Username} has been created!";
    }
}
