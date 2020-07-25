using System;

namespace VipControle.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime NowToBrazil(this DateTime date)
        {
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
        }
    }
}