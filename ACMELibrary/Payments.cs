using ACMELibrary.Data;
using ACMELibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ACMELibrary
{
    public class Payments
    {

        public string EmployeeName { get; }
        public string ScheduleList { get; }
        public string ErrorMessage { get; }

        public Payments(string stringRequest)
        {
            EmployeeName = "";
            ScheduleList = "";
            ErrorMessage = "";

            try
            {

                //Evaluate if there is no string
                if (stringRequest.Length == 0)
                {
                    ErrorMessage = "Request error: There is no any string to evaluate.";
                    return;
                };

                //Evaluate if there is no equal into request or if there is more than one
                int equalPosition = stringRequest.IndexOf("=");
                if (equalPosition == -1)
                {
                    ErrorMessage = "Request error: Missing '=' symbol.";
                    return;
                };
                if (stringRequest.IndexOf("=", equalPosition + 1) != -1)
                {
                    ErrorMessage = "Request error: More than one '=' symbol.";
                    return;
                }

                //Evaluate if there is a employee text before equal sign
                string employeeName = stringRequest.Substring(0, equalPosition);
                if (employeeName == "")
                {
                    ErrorMessage = "Request error: Missing employee name.";
                    return;
                }

                //Evaluate if there is text after equal sign
                string scheduleList = stringRequest.Substring(equalPosition + 1);
                if (scheduleList == "")
                {
                    ErrorMessage = "Request error: Missing schedule list.";
                    return;
                }

                if (ErrorMessage == "")
                {
                    EmployeeName = employeeName;
                    ScheduleList = scheduleList;
                }
                else
                {
                    EmployeeName = "";
                    ScheduleList = "";
                }

            }
            catch (Exception ex)
            {
                EmployeeName = "";
                ScheduleList = "";
                ErrorMessage = "Unexpected error: Please contact the software developer. " + ex.Message;
            }
        }

        //Local function to iterate all payments and return the total
        public float GetAmount(ref string errorMessage)
        {
            IEnumerable<Payment> paymentList = GetPayments(ref errorMessage);
            float amount = 0;
            foreach (Payment itemPayment in paymentList)
            {
                amount += itemPayment.Amount;
            }
            return amount;
        }

        //Local function to split the string on each schedule, make validations and return a list of payments
        public IEnumerable<Payment> GetPayments(ref string errorMessage)
        {
            string scheduleString = ScheduleList;
            errorMessage = "";

            try
            {

                //Read rates from database
                var dbContext = new ApplicationDbContext();
                List<TimeRates> dataRates = (List<TimeRates>)dbContext.GetData();

                //Split according comma character and put into an array
                string[] arraySchedule = scheduleString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                //Evaluate if there is no items
                if (arraySchedule.Length == 0)
                {
                    errorMessage = "Request error: No items to evaluate.";
                    return new List<Payment>();
                }

                //local variables to read each schedule
                string dayString;
                string startString;
                string separator;
                string endString;
                DateTime startTime;
                DateTime endTime;
                int counter = 1;
                var paymentList = new List<Payment>();

                foreach (string itemSchedule in arraySchedule)
                {
                    //Evaluate if each schedule has more than 13 characters
                    if (itemSchedule.Length != 13)
                    {
                        errorMessage = "Request error: Wrong length for item (" + counter.ToString() + ").";
                        break;
                    }

                    //Evaluate if day names are valid
                    dayString = itemSchedule.Substring(0, 2);
                    if (dbContext.DayName(dayString) == "")
                    {
                        errorMessage = "Request error: Wrong day code for item (" + counter.ToString() + ").";
                        break;
                    }

                    //Identifying start & end time in each string
                    startString = itemSchedule.Substring(2, 5);
                    separator = itemSchedule.Substring(7, 1);
                    endString = itemSchedule.Substring(8, 5);

                    //Evaluate the separator character
                    if (separator != "-")
                    {
                        errorMessage = "Request error: Wrong separator character for item (" + counter.ToString() + ").";
                        break;
                    }

                    //Try to parse the content for start time, if it's Ok, will keep it into a variable
                    if (!DateTime.TryParseExact(startString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime))
                    {
                        errorMessage = "Request error: Invalid start time for item (" + counter.ToString() + ").";
                        break;
                    }

                    //Try to parse the content for end time, if it's Ok, will keep it into a variable
                    if (!DateTime.TryParseExact(endString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
                    {
                        errorMessage = "Request error: Invalid end time for item (" + counter.ToString() + ").";
                        break;
                    }

                    //Evaluate if end time is less than start time
                    //Only for 12:00, increase one day
                    if (endString == "00:00") endTime = endTime.AddDays(1);
                    if (startTime > endTime)
                    {
                        errorMessage = "Request error: Start time is greater than end time in item (" + counter.ToString() + ").";
                        break;
                    }

                    counter += 1;

                    //Evaluate schedule according rates
                    List<TimeRates> timeMatchRates = dataRates.FindAll(item => item.Day == dayString && item.StartTime <= endTime && item.EndTime >= startTime);

                    foreach (TimeRates itemRate in timeMatchRates)
                    {
                        //Include in payment list according time range, calculating hours and amount
                        float totalHours = 0;
                        float totalAmount = 0;
                        DateTime startLimitTime;
                        DateTime endLimitTime;

                        //Set the start time according this schedule
                        if (startTime < itemRate.StartTime)
                            startLimitTime = itemRate.StartTime;
                        else startLimitTime = startTime;
                        //Set the end time according this schedule
                        if (endTime > itemRate.EndTime)
                            endLimitTime = itemRate.EndTime;
                        else endLimitTime = endTime;

                        //Calculate total hours and amount
                        totalHours = (float)Math.Round(endLimitTime.Subtract(startLimitTime).TotalHours, 0);
                        totalAmount = itemRate.Amount * totalHours;

                        //Add payment amount into the list to return
                        paymentList.Add(new Payment
                        {
                            Day = itemRate.Day,
                            Schedule = startLimitTime.ToString("HH:mm") + separator + endLimitTime.ToString("HH:mm"),
                            Amount = totalAmount,
                            Hours = totalHours
                        }); ;
                    };

                }


                if (errorMessage != "")
                {
                    return new List<Payment>();
                }
                else
                {
                    return paymentList;
                }

            }
            catch (Exception ex)
            {
                errorMessage = "Unexpected error: Please contact the software developer. " + ex.Message;
                return new List<Payment>();
            }
        }
    }
}
