using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    public class Song : ISong
    {
        private string _title;
        public Song(string title)
        {
            _title = title;
        }
        public string Title
        {
            get
            {
                return _title;
            }
        }
    }
}
