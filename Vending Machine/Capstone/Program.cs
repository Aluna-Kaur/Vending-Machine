using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                VendingMachine vm = new VendingMachine();
                VendingMachineCLI cli = new VendingMachineCLI(vm);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
