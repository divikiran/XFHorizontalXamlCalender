using System;
namespace CalenderView.Models
{
    public class DisplayModel
    {
        public DateTime DateTimePassed
        {
            get;
            set;
        }

        public DisplayModel(DateTime dateTime)
        {
            DateTimePassed = dateTime;
            Date = dateTime.Day;
            Month = dateTime.Month;
            Year = dateTime.Year;

        }
        public int Date
        {
            get;
            set;
        }

        public int Month
        {
            get;
            set;
        }

        public int Year
        {
            get;
            set;
        }
    }
}
