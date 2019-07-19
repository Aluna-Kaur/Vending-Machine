using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Capstone;

namespace CapstoneTests
{
    [TestClass]
    public class KataVendingMachine
    {
        VendingMachine kata;

        [TestInitialize]
        public void Initialize()
        {
            kata = new VendingMachine();
            kata.StartingInventory();
        }

        [TestMethod]
        public void Despost()
        {
            Assert.AreEqual(5, kata.MakeDeposit(5));
            Assert.AreEqual(5, kata.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(DepositException))]
        public void InvalidDepositNegative()
        {
            kata.MakeDeposit(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(DepositException))]
        public void InvalidDepositUnderOne()
        {
            kata.MakeDeposit(.5m);
        }

        [TestMethod]
        [ExpectedException(typeof(DepositException))]
        public void InvalidDepositDecimal()
        {
            kata.MakeDeposit(5.00m);
        }

        [TestMethod]
        public void Purchase()
        {
            kata.MakeDeposit(4);
            Assert.AreEqual(Chip.ChipSound, kata.MakePurchase("A2"));
            Assert.AreEqual(Gum.gumSound, kata.MakePurchase("D2"));
        }

        [TestMethod]
        [ExpectedException(typeof(SelectionException))]
        public void InvalidSelection()
        {
            kata.MakePurchase("DD");
        }

        [TestMethod]
        [ExpectedException(typeof(FundsException))]
        public void InsuffientFunds()
        {
            kata.MakePurchase("A1");
        }

        [TestMethod]
        public void Change()
        {
            kata.MakeDeposit(2);
            kata.MakePurchase("D1");
            var change = kata.MakeChange();
            Assert.AreEqual(4, change.Quarters);
            Assert.AreEqual(1, change.Nickles);
            Assert.AreEqual(1, change.Dimes);
        }

        [TestMethod]
        public void ChangeMultiPurchase()
        {
            kata.MakeDeposit(5);
            kata.MakePurchase("B2");
            kata.MakePurchase("C1");
            var change = kata.MakeChange();
            Assert.AreEqual(9, change.Quarters);
            Assert.AreEqual(0, change.Nickles);
            Assert.AreEqual(0, change.Dimes);
        }
        [TestMethod]
        [ExpectedException(typeof(OutOfStockException))]
        public void InsuffienctStock()
        {
            kata.MakeDeposit(20);
            kata.MakePurchase("D1");
            kata.MakePurchase("D1");
            kata.MakePurchase("D1");
            kata.MakePurchase("D1");
            kata.MakePurchase("D1");
            kata.MakePurchase("D1");
        }
        [TestMethod]
        public void resetBalance()
        {
            kata.ResetBalance();
            Assert.AreEqual(0, kata.Balance);
        }
    }
}
