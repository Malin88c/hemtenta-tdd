using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    public class MyWebshop : IWebshop
    {
        private IBasket _basket;
        private IBilling _billing;
        public IBasket Basket
        {
            get
            {
                return _basket;
            }
        }

        public MyWebshop(IBasket basket)
        {
            if(basket != null)
            {
                _basket = basket;
            }

            else
            {
                throw new IncorrectBasketException();
            }
        }

        public void Checkout(IBilling billing)
        {
            if(billing != null)
            {
                _billing = billing;

                _billing.Pay(_basket.TotalCost);
            }

            else
            {
                throw new IncorrectBillingException();
            }

        }
    }
}
