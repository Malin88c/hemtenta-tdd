using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    // Mocka
    public interface IBilling
    {
        decimal Balance { get; set; }
        void Pay(decimal amount);
    }
}
