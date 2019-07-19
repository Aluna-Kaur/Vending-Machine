using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    /// <summary>
    /// create a class to disperse and calculate the change
    /// </summary>
    public class Change
    {
        public decimal Quarters { get; private set; } = 0;
        public decimal Dimes { get; private set; } = 0;
        public decimal Nickles { get; private set; } = 0;

        //calculate which coins will be needed to return change
        public Change(decimal changeToBeMade)
        {
            Quarters = (int)(changeToBeMade / .25m);
            Dimes = (int)((changeToBeMade % .25m) / .10m);
            Nickles = (int)(((changeToBeMade % .25m) % .10m) / .05m);        
        }
    }

}
