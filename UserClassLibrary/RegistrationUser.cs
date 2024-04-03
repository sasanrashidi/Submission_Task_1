namespace UserClassLibrary
{
    // Klassdefinition för registrerade användare
    public class RegistrationUser
    {
        // Egenskaper för användarnamn, e-post och lösenord
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Konstruktor för att skapa en ny registrerad användare
        public RegistrationUser(string user, string password, string email)
        {
            // Kontrollera om lösenordet är giltigt (inte null, minst 8 tecken långt) och e-postadressen är giltig (inte null och slutar med "@gmail.com")
            if (password is not null && password.Length >= 8 && email is not null && email.EndsWith("@gmail.com"))
            {
                // Validerar användarnamnets längt och tecken för att säkerställa att det är alfanumeriskt och inom önskad längd (minst 5 tecken långt och högst 20 tecken långt)
                if (user is not null && user.Length >= 5 && user.Length <= 20 && IsAlphanumeric(user))
                {
                    // Om alla valideringskrav är uppfyllda, tilldelas användarens egenskaper.
                    Username = user;
                    Password = password;
                    Email = email;
                }
                else if (!IsAlphanumeric(user))
                {
                    // Kasta undantag om användarnamnet inte uppfyller kraven på att vara alfanumeriskt.
                    throw new ArgumentException("The username must be alphanumeric");
                }
                else
                {
                    // Kasta undantag om användarnamnet inte uppfyller längdkraven
                    throw new ArgumentException("The username must be between 5 & 20 characters long.");
                }
            }
            else
            {
                // Kasta undantag om lösenordet eller e-postadressen inte uppfyller kraven.
                throw new ArgumentException("The email must end with '@gmail.com'");
            }
        }

        private bool IsAlphanumeric(string str) //metod för att kontrollera om en sträng är alfanumerisk.
        {
            //Går igenom varje tecken i strängen för att verifiera att det är en bokstav eller siffra
            foreach (char c in str)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public bool ContainsSpecialCharacter(string password)//Kontrollerar om lösenordet innehåller åtminståne ett specialtecken.
        {
            // Definiera en uppsättning specialtecken som ska finnas i lösenordet
            string specialCharacters = "!@#$%^&*()-_=+[]{}|;:'\",.<>/?";

            // Kontrollera om något tecken i lösenordet matchar något tecken i specialteckensuppsättningen
            // Returnerar true om minst ett specialtecken finns i lösenordet, annars returneras false
            return password.Any(c => specialCharacters.Contains(c));
        }
    }
}