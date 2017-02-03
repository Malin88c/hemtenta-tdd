using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HemtentaTdd2017.webshop;
using NUnit.Framework;

namespace Hemtenta_Test
{
    //Vilka metoder och properties behöver testas?

    //Metoder
    //- AddProduct()
    //- RemoveProduct()
    //- Checkout()

    //Properties
    //- TotalCost
    //- Balance


    //Ska några exceptions kastas?
    //Ja.Jag anser att exceptions bör kastas när:
    //- Användaren matar in en amount som är <= 0
    //- Användaren matar in en product med price <=0 (Default är 0.00)
    //- Användaren matar inte in en product vilket resulterar till att den blir null
    //- Användaren försöker ta bort fler produkter än vad som ligger i Basket.
    //- Användaren handlar för mer än vad den har i balance.
    //- Användaren skapar en webshop men basket är null
    //- Användaren checkar ut med IBilling är  null


    //Min tolkning av domän är de värden som är tillåtna för de olika attributen och parametrarna, returvärden.

    //Vilka är domänerna för IWebshop och IBasket?
    //IWebshop:
    //- Domänen IBasket som kan vara ett objekt. (Får ej vara null)
    //- Domänen IBilling som kan vara ett objekt. (Får ej vara null)

    //IBasket:
    //Decimal TotaltCost som kan ha värderna över 0.00 och mindre än Decimal.MaxValue (79,228,162,514,264,337,593,543,950,335)
    //Add + Remove Product metoderna har domänerna Product p och int amount.
    //Product kan vara ett objekt (Får ej vara null). Amount kan ha värdena över 0 till och med Int.MaxValue(2,147,483,647)



    [TestFixture]
    public class Webshop_Test
    {
        private IWebshop webshop;
        private IBasket basket;
        private IBilling billing;

        //Create Webshop
        [Test]
        public void CreateMyWebshopWhenBasketIsNullShouldThrowException()
        {
            Assert.Throws<IncorrectBasketException>(() => webshop = new MyWebshop(null), "Webshop should need a basket.");
        }


        //Add Product
        [TestCase(2)]
        public void AddProductShouldSucceedAddToList(int amount)
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = 20.00m };
            webshop.Basket.AddProduct(p, amount);

            Assert.AreEqual(p.Price * amount, webshop.Basket.TotalCost, "Does not match");
        }

        [Test]
        public void AddProductWithoutPriceShouldThrowException()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = -0.01M };
            Assert.Throws<IncorrectPriceException>(() => webshop.Basket.AddProduct(p, 2), "Price can not be empty");
        }

        [Test]
        public void AddProductWhenNullShouldThrowException()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = null;
            Assert.Throws<IncorrectProductException>(() => webshop.Basket.AddProduct(p, 2), "Product can not be null");
        }

        [Test]
        public void AddProductWithPriceZeroShouldThrowException()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { };
            Assert.Throws<IncorrectPriceException>(() => webshop.Basket.AddProduct(p, 2), "Price can not be 0");
        }

        [Test]
        public void AddProductWithNegativeAmountShouldThrowException()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = 20.00m };
            Assert.Throws<IncorrectAmountException>(() => webshop.Basket.AddProduct(p, -1), "Amount must be above 0");
        }

        //Remove Product
        [TestCase(2)]
        public void RemoveProductShouldSucceed(int amount)
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = 10.00m };
            webshop.Basket.AddProduct(p, amount);
            webshop.Basket.RemoveProduct(p, amount);

            Assert.That(webshop.Basket.TotalCost, Is.EqualTo(0), "Should be the same");

        }
        [Test]
        public void RemoveMoreProductsThanInBasketShouldThrowException()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = 10.00m };
            webshop.Basket.AddProduct(p, 2);

            Assert.Throws<IncorrectActionException>(() => webshop.Basket.RemoveProduct(p, 3), "Should work");
        }

        [Test]
        public void RemoveZeroProductShouldThrowException()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = 10.00m };
            webshop.Basket.AddProduct(p, 2);

            Assert.Throws<IncorrectActionException>(() => webshop.Basket.RemoveProduct(p, 0), "Should not be able to remove zero products");
        }

        //CheckOut

        [Test]
        public void CheckOutWhenIBillingIsNullIsNullShouldThrowException()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = 10.00m };
            webshop.Basket.AddProduct(p, 2);

            Assert.Throws<IncorrectBillingException>(() => webshop.Checkout(null), "Basket should need a billing.");
        }


        [Test]
        public void NotEnoughCashShouldThrowException()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = 10.00m };
            webshop.Basket.AddProduct(p, 2);

            Assert.Throws<NotEnoughCashException>(() => webshop.Checkout(billing = new MockBilling(19.00m)), "You should not have enough cash");
        }

        [Test]
        public void EnoughCashShouldSucceed()
        {
            webshop = new MyWebshop(basket = new Basket());
            Product p = new Product() { Price = 10.00m };
            webshop.Basket.AddProduct(p, 2);

            webshop.Checkout(billing = new MockBilling(21.00m));

            var currentBalance = billing.Balance;

            Assert.AreEqual(1.00m, currentBalance, "You should have enough cash");

        }
    }
}
