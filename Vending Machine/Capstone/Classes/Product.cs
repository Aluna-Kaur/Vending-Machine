using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public abstract class Product
    {
        public string ProductName { get; }
        public decimal ProductPrice { get; }
        public int Qty { get; private set; } = 5;
        public string DisplayQty
        {
            get
            {
                return Qty < 1 ? "Sold Out!" : Qty.ToString();
            }
        }
        public Product(string prodName, decimal prodPrice)
        {            
            ProductName = prodName;
            ProductPrice = prodPrice;
        }
        public void RemoveItem ()
        {
            if (Qty > 0)
            {
                Qty--;
            }
            else
            {
                throw new OutOfStockException();
            }
        }
        public abstract string ProductSound();
    }
}
