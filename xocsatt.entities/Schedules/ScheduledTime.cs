using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOcsatt.Entities.Schedules
{
    public enum EventTimeBase
	{
		BySecond = 1,
		ByMinute = 2,
		Hourly = 3,
		Daily = 4,
		Weekly = 5,
		Monthly = 6,
	}

	/// <summary>
	/// The ScheduledTime class represents a repeating event that occurs anywhere from every
	/// second to once a month.  It consists of an enumeration to mark the interval and an offset from that interval.
	/// For example new ScheduledTime(Hourly, new TimeSpan(0, 15, 0)) would represent an event that fired 15 minutes
	/// after the hour every hour.
	/// </summary>
    [Serializable]
    public class ScheduledTime : IScheduledItem
    {
        private EventTimeBase _timeBase;
        private TimeSpan _offset;

        public EventTimeBase TimeBase
        {
            get { return _timeBase; }
            set { _timeBase = value; }
        }

        public TimeSpan Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public ScheduledTime()
        {
        }

        public ScheduledTime(EventTimeBase timeBase, TimeSpan offset)
        {
            _timeBase = timeBase;
            _offset = offset;
        }

        /// <summary>
        /// intializes a simple scheduled time element from a pair of strings.  
        /// Here are the supported formats
        /// 
        /// BySecond - single integer representing the offset in ms
        /// ByMinute - A comma seperate list of integers representing the number of seconds and ms
        /// Hourly - A comma seperated list of integers representing the number of minutes, seconds and ms
        /// Daily - A time in hh:mm:ss AM/PM format
        /// Weekly - n, time where n represents an integer and time is a time in the Daily format
        /// Monthly - the same format as weekly.
        /// 
        /// </summary>
        /// <param name="timeBase">A string representing the base enumeration for the scheduled time</param>
        /// <param name="offset">A string representing the offset for the time.</param>
        public ScheduledTime(string timeBase, string offset)
        {
            //TODO:Create an IScheduled time factory method.
            _timeBase = (EventTimeBase)Enum.Parse(typeof(EventTimeBase), timeBase, true);
            Init(offset);
        }

        public int ArrayAccess(string[] Arr, int i)
        {
            if (i >= Arr.Length)
                return 0;
            return int.Parse(Arr[i]);
        }

        public IEnumerable<DateTime> GetEventsInInterval(DateTime Begin, DateTime End)
        {
            List<DateTime> agenda = new List<DateTime>();

            var next = NextRunTime(Begin, true);
            while (next < End)
            {
                agenda.Add(next);
                next = IncrementInterval(next);
            }

            return agenda;
        }

        public DateTime NextRunTime(DateTime time, bool includeStartTime)
        {
            DateTime nextRun = LastSyncForTime(time) + this.Offset;

            if (nextRun == time && includeStartTime)
                return time;

            if (nextRun > time)
                return nextRun;

            return IncrementInterval(nextRun);
        }


        private DateTime LastSyncForTime(DateTime time)
        {
            switch (this.TimeBase)
            {
                case EventTimeBase.BySecond:
                    return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
                case EventTimeBase.ByMinute:
                    return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
                case EventTimeBase.Hourly:
                    return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
                case EventTimeBase.Daily:
                    return new DateTime(time.Year, time.Month, time.Day);
                case EventTimeBase.Weekly:
                    return (new DateTime(time.Year, time.Month, time.Day)).AddDays(-(int)time.DayOfWeek);
                case EventTimeBase.Monthly:
                    return new DateTime(time.Year, time.Month, 1);
            }
            throw new Exception("Invalid base specified for timer.");
        }

        private DateTime IncrementInterval(DateTime last)
        {
            switch (this.TimeBase)
            {
                case EventTimeBase.BySecond:
                    return last.AddSeconds(1);
                case EventTimeBase.ByMinute:
                    return last.AddMinutes(1);
                case EventTimeBase.Hourly:
                    return last.AddHours(1);
                case EventTimeBase.Daily:
                    return last.AddDays(1);
                case EventTimeBase.Weekly:
                    return last.AddDays(7);
                case EventTimeBase.Monthly:
                    return last.AddMonths(1);
            }
            throw new Exception("Invalid base specified for timer.");
        }

        private void Init(string offset)
        {
            switch (this.TimeBase)
            {
                case EventTimeBase.BySecond:
                    this.Offset = new TimeSpan(0, 0, 0, 0, int.Parse(offset));
                    break;
                case EventTimeBase.ByMinute:
                    string[] ArrMinute = offset.Split(',');
                    this.Offset = new TimeSpan(0, 0, 0, ArrayAccess(ArrMinute, 0), ArrayAccess(ArrMinute, 1));
                    break;
                case EventTimeBase.Hourly:
                    string[] ArrHour = offset.Split(',');
                    this.Offset = new TimeSpan(0, 0, ArrayAccess(ArrHour, 0), ArrayAccess(ArrHour, 1), ArrayAccess(ArrHour, 2));
                    break;
                case EventTimeBase.Daily:
                    DateTime Daytime = DateTime.Parse(offset);
                    this.Offset = new TimeSpan(0, Daytime.Hour, Daytime.Minute, Daytime.Second, Daytime.Millisecond);
                    break;
                case EventTimeBase.Weekly:
                    string[] ArrWeek = offset.Split(',');
                    if (ArrWeek.Length != 2)
                        throw new Exception("Weekly offset must be in the format n, time where n is the day of the week starting with 0 for sunday");
                    DateTime WeekTime = DateTime.Parse(ArrWeek[1]);
                    this.Offset = new TimeSpan(int.Parse(ArrWeek[0]), WeekTime.Hour, WeekTime.Minute, WeekTime.Second, WeekTime.Millisecond);
                    break;
                case EventTimeBase.Monthly:
                    string[] ArrMonth = offset.Split(',');
                    if (ArrMonth.Length != 2)
                        throw new Exception("Monthly offset must be in the format n, time where n is the day of the month starting with 1 for the first day of the month.");
                    DateTime MonthTime = DateTime.Parse(ArrMonth[1]);
                    this.Offset = new TimeSpan(int.Parse(ArrMonth[0]) - 1, MonthTime.Hour, MonthTime.Minute, MonthTime.Second, MonthTime.Millisecond);
                    break;
                default:
                    throw new Exception("Invalid base specified for timer.");
            }
        }
    }	 
}
