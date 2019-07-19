using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    /// <summary>
    /// create a subclass for the user interface for parent vending machine
    /// </summary>
    public class VendingMachineCLI 
    {
        private VendingMachine _vm = null;
        public VendingMachineCLI(VendingMachine vendingMachine)
        {
            _vm = vendingMachine;
            //create variables for customer input on main menu
            _vm.StartingInventory();
            string clientSelection = "";
            while (clientSelection != "3")
            {
                //create main menu

                Console.Clear();
                KaurRillyASCII();
                Console.WriteLine(" Welcome to KauRilly Vending Machine  \n   -presented by Umbrella Corps-\n \n Please make your selection.");

                //provide options for customer to choose
                Console.WriteLine("  1: Display items \n  2: Purchase Items \n  3: Exit");
                clientSelection = Console.ReadLine();
                Console.WriteLine();
                Console.Clear();

                //display inventory
                if (clientSelection == "1")
                {
                    ItemsASCII();
                    DisplayInventory();
                    Console.WriteLine();
                    Console.WriteLine(" Please press enter to return to the main menu.");
                    Console.ReadLine();
                    Console.Clear();
                }

                //create a wallet to allow customer to make depoists and purchase
                if (clientSelection == "2")
                { 
                    try
                    {
                        string subMenu = "S";
                        while (subMenu == "S")
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Your current balance: " + _vm.Balance.ToString("C2"));
                            Console.ResetColor();
                            Console.WriteLine(" 1. Deposit Money \n 2. Make Purchase \n 3. Finish transaction \n Please make selection 1-3");
                            subMenu = Console.ReadLine();
                            while (subMenu == "1")
                            {
                                decimal amtDeposit = 0;
                                Console.Clear();
                                WalletASCII();
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(" Your current balance: " + _vm.Balance.ToString("C2"));
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine(" How much would you like to deposit in whole dollar amounts? (Example: Enter 5 for $5.00) \n press Q to return previous menu");
                                string strDepositAmt = Console.ReadLine().ToUpper();
                                try
                                {
                                    if (strDepositAmt == "Q")
                                    {
                                        subMenu = "S";
                                    }
                                    else
                                    {
                                        amtDeposit = decimal.Parse(strDepositAmt);
                                        _vm.MakeDeposit(amtDeposit);
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine(" Balance after deposit: " + _vm.Balance.ToString("C2") +
                                            "\n Press any key to return to previous menu");
                                        Console.ResetColor();
                                        Console.WriteLine();
                                        Console.ReadKey();
                                        subMenu = "S";
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine(" Invalid Deposit");
                                    Console.ReadKey();
                                }
                            }
                            Console.Clear();
                            while (subMenu == "2")
                            {
                                if (_vm.Balance < 1)
                                {
                                    Console.WriteLine("A Deposit must be made before you can make a purchase " +
                                        "\n press any key to return to previous menu");
                                    Console.ReadKey();
                                    subMenu = "S";
                                }
                                else
                                {
                                    Console.Clear();
                                    ItemsASCII();
                                    Console.WriteLine(" You may choose from any of the following:");
                                    Console.WriteLine();
                                    DisplayInventory();
                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(" Your available balance is currently: " + _vm.Balance.ToString("C2"));
                                    Console.ResetColor();
                                    Console.WriteLine();
                                    Console.WriteLine(" Please enter your selection (enter Slot) or Q to return to previous menu");
                                    try
                                    {
                                        string selectionMade = Console.ReadLine().ToUpper();
                                        if (selectionMade == "Q")
                                        {
                                            subMenu = "S";
                                        }
                                        _vm.MakePurchase(selectionMade);
                                        Console.WriteLine("Would you like to make another purchase? Y/N");
                                        string anotherPurchase = Console.ReadLine().ToUpper();
                                        if (anotherPurchase == "Y")
                                        {
                                            subMenu = "2";
                                        }
                                        else
                                        {
                                            subMenu = "S";
                                        }
                                    }
                                    catch (FundsException)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Insuffienct funds for that Selection, press any key to return");
                                        Console.ReadKey();    
                                    }
                                    catch (SelectionException)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Invalid Selection");
                                        Console.ReadKey();
                                    }
                                }
                            }
                            while (subMenu == "3")
                            {
                                ReturnChange();
                                Console.WriteLine("Press any key to return to main menu");
                                Console.ReadKey();
                                subMenu = "m";
                            }
                        }
                    }
                    catch (OutOfStockException)
                    {
                        Console.WriteLine("That item is sold out");
                    }
                    catch (SelectionException)
                    {
                        Console.WriteLine(" That was not a valid selection");
                    }
                    catch (DepositException)
                    {
                        Console.WriteLine(" That was not a valid deposit");
                    }
                    catch (FundsException)
                    {
                        Console.WriteLine(" You do not have enough funds to make that purchase");
                        Console.ReadLine();
                        Console.Clear();
                    }
                    catch (FileException)
                    {
                        Console.WriteLine(" File provided did not meet required criteria");
                    }
                }
                //provide customer an exit menu
                if (clientSelection == "3")
                {
                    KaurRillyASCII();                    
                    Console.WriteLine(" Thank you for choosing KauRilly Vending. \n Please come again! (Press enter to close)");
                }
            }
        }

        private static void WalletASCII()
        {
            Console.WriteLine("                 _ _      _   ");
            Console.WriteLine("                | | |    | |  ");
            Console.WriteLine("  __      ____ _| | | ___| |_ ");
            Console.WriteLine("  \\ \\ /\\ / / _` | | |/ _ \\ __|");
            Console.WriteLine("   \\ V  V / (_| | | |  __/ |_ ");
            Console.WriteLine("    \\_/\\_/ \\__,_|_|_|\\___|\\__|");
            Console.WriteLine();
        }

        private static void ItemsASCII()
        {
            Console.WriteLine("  _ _                     ");
            Console.WriteLine(" (_) |                    ");
            Console.WriteLine("  _| |_ ___ _ __ ___  ___ ");
            Console.WriteLine(" | | __/ _ \\ '_ ` _ \\/ __|");
            Console.WriteLine(" | | ||  __/ | | | | \\__ \\");
            Console.WriteLine(" |_|\\__\\___|_| |_| |_|___/");
            Console.WriteLine();
        }

        private static void KaurRillyASCII()
        {
            Console.WriteLine(" _  __           _____  _ _ _       ");
            Console.WriteLine("| |/ /          |  __ \\(_) | |      ");
            Console.WriteLine("| ' / __ _ _   _| |__) |_| | |_   _  ");
            Console.WriteLine("|  < / _` | | | |  _  /| | | | | | |    ");
            Console.WriteLine("| . \\ (_| | |_| | | \\ \\| | | | |_| | ");
            Console.WriteLine("|_|\\_\\__,_|\\__,_|_|  \\_\\_|_|_|\\__, | ");
            Console.WriteLine("   \\ \\    / /           | (_)  __/ |     ");
            Console.WriteLine("    \\ \\  / /__ _ __   __| |_ _|___/ __ _ ");
            Console.WriteLine("     \\ \\/ / _ \\ '_ \\ / _` | | '_ \\ / _` |");
            Console.WriteLine("      \\  /  __/ | | | (_| | | | | | (_| |");
            Console.WriteLine("       \\/ \\___|_| |_|\\__,_|_|_| |_|\\__, |");
            Console.WriteLine("                                    __/ |");
            Console.WriteLine("                                   |___/ ");
            Console.WriteLine();
        }

        public void ReturnChange()
        {
            Console.Clear();
            var change = _vm.MakeChange();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Your current balance: " + _vm.Balance.ToString("C2")+"\n");          
            Console.WriteLine(" Your change will be returned in the following denominations: \n Quarters: " + change.Quarters + "\n Dimes: " + change.Dimes.ToString() + "\n Nickles: " + change.Nickles.ToString());
            _vm.ResetBalance();
            Console.ResetColor();            
            Console.WriteLine();
        }

        /// <summary>
        /// create a constructor to display inventory
        /// </summary>
        private void DisplayInventory()
        {
            Console.WriteLine("--Slot-----Item-------------Price---Inventory");
            foreach (KeyValuePair<string, Product> kvp in _vm.Inventory)
            {
                Console.WriteLine("   " + kvp.Key + "  " + kvp.Value.ProductName.PadRight(20) +" "+ kvp.Value.ProductPrice.ToString("C2") + "      x"+kvp.Value.DisplayQty);
            }
            Console.WriteLine();
        }
    }
}

