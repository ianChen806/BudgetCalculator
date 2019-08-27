using System;

namespace BudgetCalculator
{
    internal static class DateTimeExtension
    {
        public static string YearMonth(this DateTime datetime)
        {
            return datetime.ToString("yyyyMM");
        }

        public static DateTime LastDateTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month,
                DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
        }

        public static DateTime FirstDateTime(this DateTime currentTime)
        {
            return new DateTime(currentTime.Year, currentTime.Month, 1);
        }

        public static int Days(this DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }
    }
}