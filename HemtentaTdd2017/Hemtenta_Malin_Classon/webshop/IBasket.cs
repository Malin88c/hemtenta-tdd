using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HemtentaTdd2017.webshop
{
    // Testa och implementera
    public interface IBasket
    {
        void AddProduct(Product p, int amount);
        void RemoveProduct(Product p, int amount);
        decimal TotalCost { get; }
    }
}
