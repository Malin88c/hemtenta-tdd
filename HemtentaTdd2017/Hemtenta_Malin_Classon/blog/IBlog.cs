using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    // Implementera IBlog när du skrivit testerna
    public interface IBlog
    {
        // Försöker logga in en användare. Man kan
        // se om inloggningen lyckades på property
        // UserIsLoggedIn.
        // Kastar ett exception om User är null.
        void LoginUser(User u);

        // Försöker logga ut en användare. Kastar
        // exception om User är null.
        void LogoutUser(User u);

        // True om användaren är inloggad (behöver
        // inte testas separat)
        bool UserIsLoggedIn { get; }

        // För att publicera en sida måste Page vara
        // ett giltigt Page-objekt och användaren
        // måste vara inloggad.
        // Returnerar true om det gick att publicera,
        // false om publicering misslyckades och
        // exception om Page har ett ogiltigt värde.
        bool PublishPage(Page p);

        // För att skicka e-post måste användaren vara
        // inloggad och alla parametrar ha giltiga värden.
        // Returnerar 1 om det gick att skicka mailet,
        // 0 annars.
        int SendEmail(string address, string caption, string body);
    }
}
