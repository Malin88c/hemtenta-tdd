using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    // Förbjudet att implementera!!
    public interface IAuthenticator
    {
        // Söker igenom databasen efter en användare med
        // namnet "username". Returnerar ett giltigt
        // User-objekt om den hittade en användare,
        // null annars.
        User GetUserFromDatabase(string username);
    }
}
