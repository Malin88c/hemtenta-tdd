using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class MockSoundMaker : ISoundMaker
    {
        private string currentSong = "";

        // Titeln på sången som spelas just nu. Ska vara
        // tom sträng om ingen sång spelas.
        public string NowPlaying
        {
            get
            {
                return currentSong;
            }
        }

        public void Play(ISong song)
        {
            if(song != null)
            {
                currentSong = song.Title;
            }

            else
            {
                currentSong = "";
            }

         
        }

        public void Stop()
        {
            currentSong = "";
        }
    }
}
