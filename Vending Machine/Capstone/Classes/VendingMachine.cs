using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class VendingMachine 
    {
        public decimal Balance { get; set; }        
        public Dictionary<string, Product> Inventory { get; set; } = new Dictionary<string, Product>();    
        public void StartingInventory()
        {
            try
            { 
                string inventoryPath = @"..\..\..\..\etc\VendingMachine.txt";
                //reads through entire file one line at a time.
                using (StreamReader sr = new StreamReader(inventoryPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        //separates line by information
                        string[] inventoryDetails = line.Split("|");
                        if (inventoryDetails.Length != 4)
                        {
                            throw new FileException();
                        }
                        Product eachProduct = null;
                        if (inventoryDetails[3] == "Candy")
                        {
                            eachProduct = new Candy(inventoryDetails[1], decimal.Parse(inventoryDetails[2]));
                        }
                        else if (inventoryDetails[3] == "Gum")
                        {
                            eachProduct = new Gum(inventoryDetails[1], decimal.Parse(inventoryDetails[2]));
                        }
                        else if (inventoryDetails[3] == "Chip")
                        {
                            eachProduct = new Chip(inventoryDetails[1], decimal.Parse(inventoryDetails[2]));
                        }
                        else if (inventoryDetails[3] == "Drink")
                        {
                            eachProduct = new Drink(inventoryDetails[1], decimal.Parse(inventoryDetails[2]));
                        }
                        else
                        {
                            throw new FileException();
                        }
                        Inventory.Add(inventoryDetails[0], eachProduct);
                    }
                }            
            }
            catch (Exception a)
            {
                a.GetType();
            }
        }
        public decimal MakeDeposit(decimal amtToDeposit)
        {
            if (amtToDeposit < 0)
            {
                throw new DepositException();
            }
            else
            {
                if (amtToDeposit.ToString().Contains("."))
                {
                    throw new DepositException();
                }
                else
                {
                    Balance += amtToDeposit;
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(@"..\..\..\..\etc\Log.txt", true))
                        {
                            sw.WriteLine(DateTime.UtcNow + " FEED MONEY              " + amtToDeposit.ToString("C2") + " " + Balance.ToString("C2"));
                        }
                    }
                    catch (Exception e)
                    {
                        e.GetType();
                    }
                }
            }
            return Balance;
        }
        public string MakePurchase(string selectionMade)
        {
            //int saleCount = 0;
            //decimal totalSales = 0;
            /*string saleFile = @"..\..\..\..\etc\SalesReport.txt";*/
            if (Inventory.ContainsKey(selectionMade) && Balance >= Inventory[selectionMade].ProductPrice)
            {
                Balance -= Inventory[selectionMade].ProductPrice;
                Inventory[selectionMade].RemoveItem();
                try
                {
                    using (StreamWriter sw = new StreamWriter(@"..\..\..\..\etc\Log.txt", true))
                    {
                        sw.WriteLine(DateTime.UtcNow + " " + Inventory[selectionMade].ProductName.PadRight(20) + " " + selectionMade+ " " +
                                    (Balance + Inventory[selectionMade].ProductPrice).ToString("C2") + " " + Balance.ToString("C2"));
                    }               
                    //using (StreamReader saleReader = new StreamReader(saleFile))
                    //{
                    //    while (!saleReader.EndOfStream)
                    //    {
                    //        string line = saleReader.ReadLine();
                    //        string [] saleSplit = line.Split("|");
                    //        SalesReport.Add(saleSplit[0]);
                    //        SalesReport.Add(saleSplit[1]);
                    //    }
                    //}
                    //using (StreamWriter saleWriter = new StreamWriter(saleFile, false))
                    //{
                    //    int amtSold = int.Parse(SalesReport[1]);
                    //    saleCount = amtSold + 1;
                    //    saleWriter.WriteLine(Inventory[selectionMade].ProductName + "|" + saleCount);
                    //}
                    //using (StreamWriter saleWriter = new StreamWriter(saleFile, true))
                    //{
                    //    saleWriter.WriteLine(Inventory[selectionMade].ProductName + "|" + saleCount);
                    //}
                }
                catch (Exception e)
                {
                    e.GetType();
                }
                return Inventory[selectionMade].ProductSound();
            }
            else if (Inventory.ContainsKey(selectionMade))
            {
                throw new FundsException();
            }
            else
            {
                throw new SelectionException();
            }
        }
        public Change MakeChange()
        {     
            try
            {
                using (StreamWriter sw = new StreamWriter(@"..\..\..\..\etc\Log.txt", true))
                {

                    sw.WriteLine(DateTime.UtcNow + " GIVE CHANGE:            " + Balance.ToString("C2") +" $0.00");                 
                }
            }
            catch (Exception e)
            {
                e.GetType();
            }      
            return new Change(Balance);    
        }
        public decimal ResetBalance()
        {           
            Balance = 0;
            return Balance;
        }

       
    }
}