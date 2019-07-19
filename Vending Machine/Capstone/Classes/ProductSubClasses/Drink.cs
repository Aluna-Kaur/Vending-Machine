using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    /// <summary>
    /// Product information on drink
    /// </summary>
    public class Drink : Product
    {
        public const string drinkSound = "Glug Glug, Yum!";
        public Drink(string prodName, decimal prodPrice) : base(prodName, prodPrice)
        {
        }
        //return the sound when customer chooses to purchase a drink
        public override string ProductSound()
        {
            return drinkSound;
        }
    }
}
