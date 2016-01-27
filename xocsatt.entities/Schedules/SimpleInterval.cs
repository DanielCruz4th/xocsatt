﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Web.Utilities;

namespace XOcsatt.Entities.Schedules
{
    [Serializable]
    public class SimpleInterval : IScheduledItem
    {
        private TimeSpan _interval = new TimeSpan(1, 0, 0);
        private DateTime _startTime = DateTime.MinValue.RoundToSqlDateTime();
        private DateTime _endTime = DateTime.MaxValue.RoundToSqlDateTime();

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

        public IEnumerable<DateTime> AddEventsInInterval(DateTime begin, DateTime end)
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
            var _time = time.RoundToSqlDateTime();
            var span = _time.RoundToSqlDateTime() - _startTime;

            if (span < TimeSpan.Zero)
                return _startTime;

            return _time == _startTime && includeStartTime ? 

        }
    }
}
