using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.music
{
    // Se till att databasklassen kastar dessa exceptions
    // när den ska göra det enligt specen.
    public class DatabaseAlreadyOpenException : Exception { }
}
