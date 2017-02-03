using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    public class Basket : IBasket
    {
        private decimal totalCost;
        private List<Product> products = new List<Product>();

        public decimal TotalCost
        {
            get
            {
                return totalCost;
            }
        }

        public void AddProduct(Product p, int amount)
        {
            if (p != null)
            {
                if (p.Price > 0 && p.Price <= decimal.MaxValue)
                {                   
                    if (amount > 0 && amount <= int.MaxValue)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            products.Add(p);
                            totalCost += p.Price;
                        }
                    }

                    else
                    {
                        throw new IncorrectAmountException();
                    }

                }

                else
                {
                    throw new IncorrectPriceException();
                }

            }

            else
            {
                throw new IncorrectProductException();
            }



        }

        //Since the product only has a Price property, I made the assumption that this
        //is a webshop with only one kind of products, but with different prices.
        //So, when a product is removed, the price of that product is removed from the totalcost.
        //I chose not to add a name property since the name is not unique.

        public void RemoveProduct(Product p, int amount)
        {
            if (amount <= products.Count() && amount > 0)
            {
                for (int i = 0; i < amount; i++)
                {
                    products.Remove(p);
                    totalCost -= p.Price;
                }
            }
            else
            {
                throw new IncorrectActionException();
            }


        }
    }
}
