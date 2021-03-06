﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Web.Utilities;

namespace XOcsatt.Entities.Schedules
{
    /// <summary>
    /// The SimpleInterval class models a simple pulse timer. Its constructor takes two parameters, 
    /// an absolute start time and a TimeSpan for the interval between events. 
    /// It is more general than the ScheduledTime object because any interval can be scheduled.
    /// </summary>
    [Serializable]
    public class SimpleInterval : IScheduledItem
    {
        private TimeSpan _interval = new TimeSpan(1, 0, 0);
        private DateTime _startTime = DateTime.MinValue.RoundToSqlDateTime();
        private DateTime _endTime = DateTime.MaxValue.RoundToSqlDateTime();

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public TimeSpan Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        public SimpleInterval()
        {
        }

        public SimpleInterval(DateTime startTime, TimeSpan interval)
        {
            _startTime = startTime.RoundToSqlDateTime();
            _interval = interval;
        }

        public SimpleInterval(DateTime startTime, TimeSpan interval, int count)
        {
            _startTime = startTime.RoundToSqlDateTime();
            _interval = interval;
            _endTime = _startTime + TimeSpan.FromTicks(_interval.Ticks * count);
        }

        public SimpleInterval(DateTime startTime, TimeSpan interval, DateTime endTime)
        {
            _startTime = startTime.RoundToSqlDateTime();
            _interval = interval;
            _endTime = endTime.RoundToSqlDateTime();
        }

        public IEnumerable<DateTime> GetEventsInInterval(DateTime begin, DateTime end)
        {
            List<DateTime> agenda = new List<DateTime>();

            if (end < begin)
                return agenda;

            var next = NextRunTime(begin, true);

            while (next < end)
            {
                agenda.Add(next);
                next = NextRunTime(next, false);
            }

            return agenda;
        }

        public DateTime NextRunTime(DateTime time, bool includeStartTime)
        {
            var nextRun = NextRunTimeInternal(time, includeStartTime);

            return nextRun >= this.EndTime ?
                DateTime.MaxValue.RoundToSqlDateTime() 
                : nextRun;
        }

        private DateTime NextRunTimeInternal(DateTime time, bool includeStartTime)
        {
            var _time = time.RoundToSqlDateTime();
            var span = _time - this.StartTime;

            if (this.Interval <= TimeSpan.Zero || span < TimeSpan.Zero)
                return this.StartTime;

            if (span.Ticks % this.Interval.Ticks == 0)
                return includeStartTime ? _time : _time + this.Interval;

            return _time + (this.Interval - span);
        }
    }
}
