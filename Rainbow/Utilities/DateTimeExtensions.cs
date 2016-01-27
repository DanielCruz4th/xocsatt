using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Web.Utilities
{
    public static class DateTimeExtensions
    {
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
                return dateTime;

            return DateTime.SpecifyKind(dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks)), dateTime.Kind);
        }

        public static DateTime RoundToSqlDateTime(this DateTime dateTime)
        {
            var value = dateTime < SqlDateTime.MinValue.Value ? SqlDateTime.MinValue.Value
                : dateTime > SqlDateTime.MaxValue.Value ? SqlDateTime.MaxValue.Value : dateTime;

            return DateTime.SpecifyKind(new SqlDateTime(value).Value, dateTime.Kind);
        }

        //private static readonly int[] OFFSET = { 0, -1, +1, 0, -1, +2, +1, 0, -1, +1 };
        //private static readonly DateTime SQL_SERVER_DATETIME_MIN = new DateTime(1753, 01, 01, 00, 00, 00, 000);
        //private static readonly DateTime SQL_SERVER_DATETIME_MAX = new DateTime(9999, 12, 31, 23, 59, 59, 997);

        //public static DateTime RoundToSqlDateTime(this DateTime dateTime)
        //{
        //    var dt = new SqlDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour,
        //        dateTime.Minute, dateTime.Second, dateTime.Millisecond);
        //    int milliseconds = dateTime.Millisecond;
        //    int t = milliseconds % 10;
        //    int offset = OFFSET[t];
        //    DateTime rounded = dt.Value.AddMilliseconds(offset);

        //    if (rounded < SQL_SERVER_DATETIME_MIN) throw new ArgumentOutOfRangeException("value");
        //    if (rounded > SQL_SERVER_DATETIME_MAX) throw new ArgumentOutOfRangeException("value");

        //    return DateTime.SpecifyKind(rounded, dateTime.Kind);
        //}
    }
}
