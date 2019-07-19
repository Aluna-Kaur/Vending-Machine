using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    /// <summary>
    /// Product information on Candy
    /// </summary> 
    public class Candy : Product
    {
        public const string CandySound = "Munch Munch, Yum!";
        //
        public Candy(string prodName, decimal prodPrice) : base(prodName, prodPrice)
        {
        }
        //return the sound when customer chooses to purchase candy
        public override string ProductSound()
        {
            return CandySound;
        }   
    }
}
