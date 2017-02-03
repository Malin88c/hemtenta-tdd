using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    public class MockAuthenticator : IAuthenticator
    {
        public static List<User> DbUsers = new List<User>() { new User("Malin") { Password = "malin" }, new User("David") { Password = "david" }, new User("Karin") { Password = "karin" } };
        public User GetUserFromDatabase(string username)
        {
            // Söker igenom databasen efter en användare med
            // namnet "username". Returnerar ett giltigt
            // User-objekt om den hittade en användare,
            // null annars.

            if (!String.IsNullOrEmpty(username))
            {
                var user = DbUsers.Where(u => u.Name == username).FirstOrDefault();

                if (user == null)
                {
                    return null;
                }

                else
                {
                    return user;
                }
            }
            else
            {
                //Jag tyckte att ett exception borde kastas om username är felaktig.
                throw new IncorrectUserException();
            }
        }
    }
}
