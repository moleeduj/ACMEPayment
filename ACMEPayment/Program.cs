using ACMELibrary.Models;
using System;
using System.Collections.Generic;

namespace ACMEPayment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("WELCOME TO ACME PAYMENT CALCULATION!");
            Console.WriteLine("******************************************************************************************************");
            Console.WriteLine("Free evaluation copy. Developed By: Eduardo Molero");
            Console.WriteLine("");
            Console.WriteLine("This will help you to calculate payment amount, according current time rates.");
            Console.WriteLine("Use a string request with the employee name, and time interval for any weekday (comma separated).");
            Console.WriteLine("");
            Console.WriteLine("Example: RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00-21:00");
            Console.WriteLine("MO: Monday");
            Console.WriteLine("TU: Tuesday");
            Console.WriteLine("WE: Wednesday");
            Console.WriteLine("TH: Thursday");
            Console.WriteLine("FR: Friday");
            Console.WriteLine("SA: Saturday");
            Console.WriteLine("SU: Sunday");

            Console.WriteLine("");
            Console.WriteLine("APPLICATION SETTING:");
            Console.WriteLine("Do you want to show payment details in all calculations? (Y/N)");
            string showDetails = Console.ReadLine();
            bool detailed = false;
            if (showDetails == "Y")
                detailed = true;
            else detailed = false;

            Console.WriteLine("******************************************************************************************************");
            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Please, enter a valid string request. (Optional, enter 'E' to Exit, 'D' to see Time Reates)");
                string selectedValue = Console.ReadLine();
                if (selectedValue == "E")
                {
                    //Exit loop
                    break;
                }

                if (selectedValue == "D")
                {
                    //Show table of time rates
                    var dbContext = new ACMELibrary.Data.ApplicationDbContext();
                    var rateList = dbContext.GetData();
                    Console.WriteLine("");
                    Console.WriteLine("Time rate list (" + DateTime.Today.ToShortDateString() + ")");
                    Console.WriteLine("");

                    foreach (TimeRates item in rateList)
                    {
                        Console.WriteLine("Day: {0} - Start Time: {1} - End Time: {2} - Amount USD: {3:c}", dbContext.DayName(item.Day), item.StartTime.ToShortTimeString(), item.EndTime.ToShortTimeString(), item.Amount);
                    }
                }
                else
                {
                    //Evaluate string from the user
                    string errorMessage = "";
                    var payments = new ACMELibrary.Payments(selectedValue);
                    //Evaluate sintaxis errors using the ErrorMessage property
                    if (payments.ErrorMessage!="")
                    {
                        Console.WriteLine(payments.ErrorMessage);
                        errorMessage = payments.ErrorMessage;
                    }
                    else
                    {
                        //Evaluate if it's possible to obtain the amount
                        //Obtain total amount
                        float amount = payments.GetAmount(ref errorMessage);

                        if (errorMessage != "")
                        {
                            //In case of error, return the message
                            Console.WriteLine(errorMessage);
                        }
                        else
                        {
                            //Show the final message
                            Console.WriteLine("------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("The amount to pay " + payments.EmployeeName + " is: " + amount.ToString("c") + " USD");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------");

                        }
                    }



                    if (detailed && errorMessage == "")
                    {
                        //If detailed info was required, will iterate all payments and show them
                        var paymentsList = payments.GetPayments(ref errorMessage);
                        string returnString = "";

                        Console.WriteLine("Payment details:");
                        Console.WriteLine("");
                        foreach (Payment paymentItem in paymentsList)
                        {
                            returnString += paymentItem.Day + " (" + paymentItem.Schedule + ")  Hours: " + paymentItem.Hours.ToString() + "  Amount: " + paymentItem.Amount.ToString("c") + " USD" + System.Environment.NewLine;
                        };
                        Console.WriteLine(returnString);
                        Console.WriteLine("------------------------------------------------------------------------------------------------------");
                    }
                }



            }


        }

    }
}
