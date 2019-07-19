using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    /// <summary>
    /// Product information on chip
    /// </summary>
    public class Chip : Product
    {
        public const string ChipSound = "Crunch Crunch, Yum!";
        public Chip(string prodName, decimal prodPrice) : base(prodName, prodPrice)
        {
        }
        //return the sound when customer chooses to purchase chips
        public override string ProductSound()
        {
            return ChipSound;
        }
    }
}



