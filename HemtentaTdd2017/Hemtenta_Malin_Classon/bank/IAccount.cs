using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.bank
{
    // Representerar ett konto. Implementera den här!
    // Obs: i vanliga fall ska datatypen decimal användas
    // i stället för double när man hanterar pengar.
    public interface IAccount
    {
        // behöver inte testas
        double Amount { get; }

        // Sätter in ett belopp på kontot
        void Deposit(double amount);

        // Gör ett uttag från kontot
        void Withdraw(double amount);

        // Överför ett belopp från ett konto till ett annat
        void TransferFunds(IAccount destination, double amount);
    }

}
