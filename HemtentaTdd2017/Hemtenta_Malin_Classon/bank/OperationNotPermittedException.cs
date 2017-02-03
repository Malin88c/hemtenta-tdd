using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.bank
{
    // Kastas om en operation på kontot inte tillåts av någon
    // anledning som inte de andra exceptions täcker in
    public class OperationNotPermittedException : Exception { }
}
