using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using HemtentaTdd2017.bank;

namespace Hemtenta_Test
{
    [TestFixture]
    public class Bank_Test
    {
        private IAccount account;
        
        //Deposit
        [TestCase(-0.01)]
        [TestCase(Double.MinValue)]
        [TestCase(Double.NaN)]
        [TestCase(Double.NegativeInfinity)]
        [TestCase(Double.PositiveInfinity)]
        public void IfAmountIsNegativeOrToBigDepositShouldThrowIllegalAmmountException(double amount)
        {
            account = new Account();
            Assert.Throws<IllegalAmountException>(() => account.Deposit(amount), "Should throw exception");
        }

        [TestCase(20.00)]
        [TestCase(Double.MaxValue)]
        public void IfAmountIsPositiveShouldDepositAmount(double amount)
        {
            account = new Account();
            account.Deposit(amount);
            var result = account.Amount;

            Assert.AreEqual(amount, result, "Should be correct");
        }

        //Withdraw
        [TestCase(-0.01)]
        [TestCase(Double.MinValue)]
        [TestCase(Double.NegativeInfinity)]

        public void IfAmountIsNegativeWithdrawShoulddThrowIllegalAmmountException(double amount)
        {
            account = new Account();
            account.Deposit(100.00);
            Assert.Throws<IllegalAmountException>(() => account.Withdraw(amount), "Should throw exception");
        }

        [TestCase(20.00)]
        public void IfAmountIsPositiveShouldWithdrawAmount(double amount)
        {
            account = new Account();
            account.Deposit(100.00);

            var before = account.Amount;
            account.Withdraw(amount);

            var after = account.Amount;

            var result = after += amount;

            Assert.AreEqual(before, result, "Should be correct");
        }

        [TestCase(20.00)]
        [TestCase(Double.MaxValue)]
        public void IfAmountIsToLargeShouldWithdrawAmount(double amount)
        {
            account = new Account();
            account.Deposit(10.00);

            Assert.Throws<InsufficientFundsException>(() => account.Withdraw(amount));
        }

        //Transfer Funds
        [Test]
        public void IfDestinationIsSetAndAmountPositiveTransferSuccess()
        {
            IAccount account2 = new Account();
            double amount = 102.00;
            account.TransferFunds(account2, amount);
            var result = account2.Amount;

            Assert.AreEqual(amount, result, "These amounts should match");         
        }

        [TestCase(102.00)]
        public void IfDestinationIsNullShouldThrowException(double amount)
        {
            account = new Account();
            IAccount account2 = null;

            Assert.Throws<OperationNotPermittedException>(() => account.TransferFunds(account2, amount), "Should throw exception");
        }

        [TestCase(Double.NaN)]
        [TestCase(-0.01)]
        public void IfAmountIsNegativeShouldThrowException(double amount)
        {
            account = new Account();

            IAccount account2 = new Account();
            Assert.Throws<IllegalAmountException>(() => account.TransferFunds(account2, amount), "Should throw exception");
        }

    }
}
