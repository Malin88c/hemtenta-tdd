using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.bank
{
    // Kastas när beloppet på kontot inte tillåter
    // ett uttag eller en överföring
    public class InsufficientFundsException : Exception { }
}
