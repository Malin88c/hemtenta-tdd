using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    public class MockBilling : IBilling
    {
        private decimal balance;

        public decimal Balance
        {
            get
            {
                return balance;
            }

            set
            {
                balance = Balance;
            }
        }

        //Jag la till enkonstruktor här för att kunna sätta balance
        public MockBilling(decimal cash)
        {
            balance = cash;
        }

        public void Pay(decimal amount)
        {
            
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
            }

            else
            {
                throw new NotEnoughCashException();
            }

            
        }
    }
}
