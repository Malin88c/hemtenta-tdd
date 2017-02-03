using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    // Databasen som har alla sånger.
    // Om man försöker använda databasen när den
    // är stängd, eller öppna den när den redan
    // är öppen, ska funktionen kasta motsvarande
    // exception. Ska inte implementeras.

    public class MockMediaDatabase : IMediaDatabase
    {
        private bool connected = false;

        private List<ISong> dbSongs = new List<ISong>()
        {
            new Song("Born to run") { },
            new Song("Stairway to heaven") { },
            new Song("Angie") { },
            new Song("Nothing else matters")
        };

        public bool IsConnected
        {
            get
            {
                return connected;
            }
        }
        // Ansluter till databasen
        public void OpenConnection()
        {
            if (connected == false)
            {
                connected = true;
            }
            else
            {
                throw new DatabaseAlreadyOpenException();
            }
        }

        public void CloseConnection()
        {
            if (connected == true)
            {
                connected = false;
            }

            else
            {
                throw new DatabaseClosedException();
            }
        }
        // Returnerar alla sånger i databasen som
        // matchar söksträngen.
        // Tips: använd string.Contains(string)
        public List<ISong> FetchSongs(string search)
        {
            if(IsConnected == true)
            {
                return dbSongs.Where(x => x.Title.Contains(search)).ToList();
            }
            else
            {         
                throw new DatabaseClosedException();
            }


        }

    }
}
