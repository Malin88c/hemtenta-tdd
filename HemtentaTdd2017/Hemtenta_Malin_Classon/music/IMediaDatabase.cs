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
    public interface IMediaDatabase
    {
        bool IsConnected { get; }

        // Ansluter till databasen
        void OpenConnection();

        // Stänger anslutning till databasen
        void CloseConnection();

        // Returnerar alla sånger i databasen som
        // matchar söksträngen.
        // Tips: använd string.Contains(string)
        List<ISong> FetchSongs(string search);
    }
}
