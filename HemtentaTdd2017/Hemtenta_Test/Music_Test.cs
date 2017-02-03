using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HemtentaTdd2017.music;
using NUnit.Framework;

namespace Hemtenta_Test
{
    [TestFixture]
    public class Music_Test
    {
        private IMediaDatabase mediaDb = new MockMediaDatabase();
        private ISoundMaker soundDb = new MockSoundMaker();
        private IMusicPlayer player;

        private const string defaultSong = "Tystnad råder";

        [TestCase("Nothing")]
        public void LoadSongsShouldReturnOneSong(string nothing)
        {
            IMusicPlayer player = new MusicPlayer(mediaDb, soundDb);

            int songsInQueBefore = player.NumSongsInQueue;

            mediaDb.OpenConnection();
            player.LoadSongs(nothing);
            mediaDb.CloseConnection();

            int songInQueAfter = player.NumSongsInQueue;;

            Assert.That(songsInQueBefore < songInQueAfter, "Inga låtar lades till");
           
        }

        [TestCase("Hej och hå i skogen")]
        [TestCase(null)]
        [TestCase("")]
        public void LoadIncorrectSongsShouldThrowException(string search)
        {
            IMusicPlayer player = new MusicPlayer(mediaDb, soundDb);

            mediaDb.OpenConnection();

            Assert.Throws<IncorrectSearchException>(() => player.LoadSongs(search), "Borde ha kastat en exception");
            mediaDb.CloseConnection();
        }

        [Test]
        public void NowPlayingShouldBeSilent()
        {
            player = new MusicPlayer(mediaDb, soundDb);

            string actual = player.NowPlaying();
            Assert.AreEqual(defaultSong, actual, "Tystnad råder tydligen inte");
        }

        [Test]
        public void NowPlayingShouldBeCurrentSong()
        {
            player = new MusicPlayer(mediaDb, soundDb);
            mediaDb.OpenConnection();

            player.LoadSongs("to");

            mediaDb.CloseConnection();
            player.Play();

            //Jag hade tänkt att hämta defaultSong från musicplayer, men det gick ej då det strider mot Interfacet.
            //Antog att det inte var okej att lägga till en property där, därav upprepningen.

            string actual = player.NowPlaying();
            player.Stop();
            Assert.AreNotEqual(defaultSong, actual, "Tystnad ska ej råda");
        }

        [TestCase("Stairway to heaven", "Angie")]
        public void NowPlayingShouldNotChangeIfSongIsAlreadyPlaying(string songTitle1, string songTitle2)
        {
            player = new MusicPlayer(mediaDb, soundDb);
            mediaDb.OpenConnection();
            player.LoadSongs(songTitle1);
            player.LoadSongs(songTitle2);
            mediaDb.CloseConnection();

            // Spelar Stairway to heaven
            player.Play();

            var playing = player.NowPlaying();

            StringAssert.Contains(songTitle1, player.NowPlaying(), "Den borde ju spelas");


            player.Play();

            playing = player.NowPlaying();

            StringAssert.Contains(songTitle1, player.NowPlaying(), "Den borde ju spelas");

            player.Stop();
        }


        //Jag hade velat kunna kolla vilka låtar som läggs till i kön, men att lägga till properties
        //strider mot interfacet.
        [Test]
        public void NumberOfSongsInQue()
        {
            player = new MusicPlayer(mediaDb, soundDb);
            int songs = player.NumSongsInQueue;

            Assert.AreEqual(0, songs, "Number of songs does not match");
            mediaDb.OpenConnection();

            player.LoadSongs("to");
            mediaDb.CloseConnection();

            songs = player.NumSongsInQueue;

            Assert.AreEqual(2, songs, "Number of songs does not match");

            player.NextSong();
            player.NextSong();
            songs = player.NumSongsInQueue;

            Assert.That(0, Is.EqualTo(songs), "Number of songs does not match");

        }

        [Test]
        public void DbNotOpenShouldThrowException()
        {
            player = new MusicPlayer(mediaDb, soundDb);
            Assert.Throws<DatabaseClosedException>(() => player.LoadSongs("to"));
        }

        [Test]
        public void DbAlreadyOpenShouldThrowException()
        {
            player = new MusicPlayer(mediaDb, soundDb);
            mediaDb.OpenConnection();
            Assert.Throws<DatabaseAlreadyOpenException>(() => mediaDb.OpenConnection());
            mediaDb.CloseConnection();
        }

    }
}
