using ACMELibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ACMELibrary.Data
{
    public class ApplicationDbContext
    {
        private readonly List<TimeRates> DataList;

        public ApplicationDbContext()
        {
            //There's not any data source, so it's built here
            //Data structure is based in Model TimeRates
            DataList = new List<TimeRates>();

            //Monday Rates
            DataList.Add(new TimeRates()
            { Id = 1, Day= "MO", StartTime = DateTime.Parse("00:01"), EndTime=DateTime.Parse("09:00"), Amount=25 });
            DataList.Add(new TimeRates()
            { Id = 2, Day = "MO", StartTime = DateTime.Parse("09:01"), EndTime = DateTime.Parse("18:00"), Amount = 15 });
            DataList.Add(new TimeRates()
            { Id = 3, Day = "MO", StartTime = DateTime.Parse("18:01"), EndTime = DateTime.Parse("00:00").AddDays(1), Amount = 20 });

            //Tuesday Rates
            DataList.Add(new TimeRates()
            { Id = 4, Day = "TU", StartTime = DateTime.Parse("00:01"), EndTime = DateTime.Parse("09:00"), Amount = 25 });
            DataList.Add(new TimeRates()
            { Id = 5, Day = "TU", StartTime = DateTime.Parse("09:01"), EndTime = DateTime.Parse("18:00"), Amount = 15 });
            DataList.Add(new TimeRates()
            { Id = 6, Day = "TU", StartTime = DateTime.Parse("18:01"), EndTime = DateTime.Parse("00:00").AddDays(1), Amount = 20 });

            //Wednesday Rates
            DataList.Add(new TimeRates()
            { Id = 7, Day = "WE", StartTime = DateTime.Parse("00:01"), EndTime = DateTime.Parse("09:00"), Amount = 25 });
            DataList.Add(new TimeRates()
            { Id = 8, Day = "WE", StartTime = DateTime.Parse("09:01"), EndTime = DateTime.Parse("18:00"), Amount = 15 });
            DataList.Add(new TimeRates()
            { Id = 9, Day = "WE", StartTime = DateTime.Parse("18:01"), EndTime = DateTime.Parse("00:00").AddDays(1), Amount = 20 });

            //Thursday Rates
            DataList.Add(new TimeRates()
            { Id = 10, Day = "TH", StartTime = DateTime.Parse("00:01"), EndTime = DateTime.Parse("09:00"), Amount = 25 });
            DataList.Add(new TimeRates()
            { Id = 11, Day = "TH", StartTime = DateTime.Parse("09:01"), EndTime = DateTime.Parse("18:00"), Amount = 15 });
            DataList.Add(new TimeRates()
            { Id = 12, Day = "TH", StartTime = DateTime.Parse("18:01"), EndTime = DateTime.Parse("00:00").AddDays(1), Amount = 20 });

            //Friday Rates
            DataList.Add(new TimeRates()
            { Id = 13, Day = "FR", StartTime = DateTime.Parse("00:01"), EndTime = DateTime.Parse("09:00"), Amount = 25 });
            DataList.Add(new TimeRates()
            { Id = 14, Day = "FR", StartTime = DateTime.Parse("09:01"), EndTime = DateTime.Parse("18:00"), Amount = 15 });
            DataList.Add(new TimeRates()
            { Id = 15, Day = "FR", StartTime = DateTime.Parse("18:01"), EndTime = DateTime.Parse("00:00").AddDays(1), Amount = 20 });

            //Saturday Rates
            DataList.Add(new TimeRates()
            { Id = 16, Day = "SA", StartTime = DateTime.Parse("00:01"), EndTime = DateTime.Parse("09:00"), Amount = 30 });
            DataList.Add(new TimeRates()
            { Id = 17, Day = "SA", StartTime = DateTime.Parse("09:01"), EndTime = DateTime.Parse("18:00"), Amount = 20 });
            DataList.Add(new TimeRates()
            { Id = 18, Day = "SA", StartTime = DateTime.Parse("18:01"), EndTime = DateTime.Parse("00:00").AddDays(1), Amount = 25 });

            //Sunday Rates
            DataList.Add(new TimeRates()
            { Id = 19, Day = "SU", StartTime = DateTime.Parse("00:01"), EndTime = DateTime.Parse("09:00"), Amount = 30 });
            DataList.Add(new TimeRates()
            { Id = 20, Day = "SU", StartTime = DateTime.Parse("09:01"), EndTime = DateTime.Parse("18:00"), Amount = 20 });
            DataList.Add(new TimeRates()
            { Id = 21, Day = "SU", StartTime = DateTime.Parse("18:01"), EndTime = DateTime.Parse("00:00").AddDays(1), Amount = 25 });


        }

        public IEnumerable<TimeRates> GetData()
        {
            //This function will retreive data
            return DataList;
        }

        public string DayName(string dayCode)
        {
            switch (dayCode)
            {
                case "MO":
                    return "Monday";
                case "TU":
                    return "Tuesday";
                case "WE":
                    return "Wednesday";
                case "TH":
                    return "Thursday";
                case "FR":
                    return "Friday";
                case "SA":
                    return "Saturday";
                case "SU":
                    return "Sunday";

                default:
                    return "";
            };
        }
    }
}
