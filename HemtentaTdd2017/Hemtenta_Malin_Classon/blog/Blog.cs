using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    public class Blog : IBlog
    {
        private IAuthenticator _authenticator;

        private User loggedInUser;


        public Blog(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        // True om användaren är inloggad (behöver
        // inte testas separat)

        public bool UserIsLoggedIn
        {
            get
            {
                if (loggedInUser != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Försöker logga in en användare. Man kan
        // se om inloggningen lyckades på property
        // UserIsLoggedIn.
        // Kastar ett exception om User är null.
        public void LoginUser(User u)
        {
            if (u != null)
            {
                User user = _authenticator.GetUserFromDatabase(u.Name);

                if (user != null)
                {
                    if (u.Password == user.Password)
                    {
                        loggedInUser = user;

                    }

                    else
                    {
                        throw new IncorrectAuthenticationException();
                    }

                }
                else
                {
                    throw new NoUserFoundInDbException();
                }
            }
            else
            {
                throw new IncorrectUserException();
            }
        }

        // Försöker logga ut en användare. Kastar
        // exception om User är null.
        public void LogoutUser(User u)
        {
            if (u != null)
            {
                User user = _authenticator.GetUserFromDatabase(u.Name);

                if (user != null)
                {
                    if (UserIsLoggedIn != false)
                    {
                        if (u.Password == user.Password)
                        {
                            loggedInUser = null;
                        }

                        else
                        {
                            throw new IncorrectAuthenticationException();
                        }

                    }

                    else
                    {
                        throw new NoUserLoggedInException();
                    }
                }
                else
                {
                    throw new NoUserFoundInDbException();
                }
            }
            else
            {
                //TODO - Implement custom exception
                throw new IncorrectUserException();
            }

        }

        // För att publicera en sida måste Page vara
        // ett giltigt Page-objekt och användaren
        // måste vara inloggad.
        // Returnerar true om det gick att publicera,
        // false om publicering misslyckades och
        // exception om Page har ett ogiltigt värde.
        public bool PublishPage(Page p)
        {
            //Page kan vara ett object eller null

            if (p != null)
            {
                if (UserIsLoggedIn == true)
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }
            else
            {
                throw new IncorrectPageException();
            }


        }

        // För att skicka e-post måste användaren vara
        // inloggad och alla parametrar ha giltiga värden.
        // Returnerar 1 om det gick att skicka mailet,
        // 0 annars.
        public int SendEmail(string address, string caption, string body)
        {
            if (!String.IsNullOrEmpty(address) && !String.IsNullOrEmpty(caption) && !String.IsNullOrEmpty(body))
            {
                if (UserIsLoggedIn == true)
                {
                    return 1;
                }

                else
                {
                    return 0;
                }
            }

            //Throw exception. Inte i beskrivning.

            else
            {
                //OM någon av parametrarna har ett felaktigt värde.
                throw new IncorrectEmailException();
            }


        }
    }
}
