using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    /// <summary>
    /// Product information on gum
    /// </summary>
    public class Gum : Product
    {
        public const string gumSound = "Chew Chew, Yum!";
        public Gum(string prodName, decimal prodPrice) : base(prodName, prodPrice)
        {
        }
        //return the sound when customer chooses to purchase gum
        public override string ProductSound()
        {
            return gumSound;
        }
    }
}
