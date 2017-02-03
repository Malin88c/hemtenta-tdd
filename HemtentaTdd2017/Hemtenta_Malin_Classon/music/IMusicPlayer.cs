using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    // Ska testas och implementeras.
    public interface IMusicPlayer
    {
        // Antal sånger som finns i spellistan.
        // Returnerar alltid ett heltal >= 0.
        int NumSongsInQueue { get; }

        // Söker i databasen efter sångtitlar som
        // innehåller "search" och lägger till alla
        // sökträffar i spellistan.
        void LoadSongs(string search);

        // Om ingen låt spelas för tillfället ska
        // nästa sång i kön börja spelas. Om en låt
        // redan spelas har funktionen ingen effekt.
        void Play();

        // Om en sång spelas ska den sluta spelas.
        // Sången ligger kvar i spellistan. Om ingen
        // sång spelas har funktionen ingen effekt.
        void Stop();

        // Börjar spela nästa sång i kön. Om kön är tom
        // har funktionen samma effekt som Stop().
        void NextSong();

        // Returnerar strängen "Tystnad råder" om ingen
        // sång spelas, annars "Spelar <namnet på sången>".
        // Exempel: "Spelar Born to run".
        string NowPlaying();
    }
}
