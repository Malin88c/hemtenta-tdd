using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class MusicPlayer : IMusicPlayer
    {
        private IMediaDatabase _mediaDb;
        private ISoundMaker _soundMaker;

        private const string defaultSong = "Tystnad råder";

        private List<ISong> queSongs;


        public List<ISong> QueSongs { get { return queSongs; } }

        // Antal sånger som finns i spellistan.
        // Returnerar alltid ett heltal >= 0.
        public int NumSongsInQueue
        {
            get
            {
                return queSongs.Count();
            }
        }

        public MusicPlayer(IMediaDatabase mediaDb, ISoundMaker soundMaker)
        {
            _mediaDb = mediaDb;
            _soundMaker = soundMaker;
            queSongs = new List<ISong>();
        }

        // Söker i databasen efter sångtitlar som
        // innehåller "search" och lägger till alla
        // sökträffar i spellistan.
        public void LoadSongs(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var songs = _mediaDb.FetchSongs(search);

                if (songs != null && songs.Count() > 0)
                {
                    foreach (var song in songs)
                    {
                        queSongs.Add(song);
                    }
                }

                else
                {
                    throw new IncorrectSearchException();
                }

            }
            else
            {
                throw new IncorrectSearchException();
            }

        }

        // Om ingen låt spelas för tillfället ska
        // nästa sång i kön börja spelas. Om en låt
        // redan spelas har funktionen ingen effekt.
        public void Play()
        {
            if (String.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                if (queSongs.Count() > 0)
                {
                    _soundMaker.Play(queSongs.First());
                    queSongs.Remove(queSongs.First());

                }
            }

        }

        // Om en sång spelas ska den sluta spelas.
        // Sången ligger kvar i spellistan. Om ingen
        // sång spelas har funktionen ingen effekt.
        public void Stop()
        {
            if (!String.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                _soundMaker.Stop();
            }
        }

        // Börjar spela nästa sång i kön. Om kön är tom
        // har funktionen samma effekt som Stop().
        public void NextSong()
        {
            if (queSongs.Count() > 0)
            {
                var nextSong = queSongs.First();

                _soundMaker.Play(nextSong);
                queSongs.Remove(nextSong);
            }

            else
            {
                _soundMaker.Stop();
            }

        }

        // Returnerar strängen "Tystnad råder" om ingen
        // sång spelas, annars "Spelar <namnet på sången>".
        // Exempel: "Spelar Born to run".
        public string NowPlaying()
        {
            if (String.IsNullOrEmpty(_soundMaker.NowPlaying))
            {
                return defaultSong;
            }
            else
            {
                return "Spelar " + _soundMaker.NowPlaying;
            }
        }


    }
}
