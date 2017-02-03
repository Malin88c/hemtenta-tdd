using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.blog
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public User(string name)
        {
            this.Name = name;
            this.Password = "guest";
        }
    }
}
