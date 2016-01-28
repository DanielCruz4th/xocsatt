using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOcsatt.Entities.Schedules
{
    /// <summary>
	/// This class will be used to implement a filter that enables a window of activity.  For cases where you want to 
	/// run every 15 minutes between 6:00 AM and 5:00 PM.  Or just on weekdays or weekends.
	/// </summary>
    public class BlockWrapper : IScheduledItem
    {
        private IScheduledItem _item;
        private ScheduledTime _begin, _end;

        public IScheduledItem Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public ScheduledTime Begin
        {
            get { return _begin; }
            set { _begin = value; }
        }

        public ScheduledTime End
        {
            get { return _end; }
            set { _end = value; }
        }

        public BlockWrapper(IScheduledItem item, string timeBase, string beginOffset, string endOffset)
        {
            _item = item;
            _begin = new ScheduledTime(timeBase, beginOffset);
            _end = new ScheduledTime(timeBase, endOffset);
        }

        public IEnumerable<DateTime> AddEventsInInterval(DateTime begin, DateTime end)
        {
            List<DateTime> agenda = new List<DateTime>();

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
            return NextRunTime(time, 100, includeStartTime);
        }

        DateTime NextRunTime(DateTime time, int count, bool includeStartTime)
        {
            if (count == 0)
                throw new Exception("Invalid block wrapper combination.");

            DateTime
                temp = this.Item.NextRunTime(time, includeStartTime),
                begin = this.Begin.NextRunTime(time, true),
                end = this.End.NextRunTime(time, true);
            Debug.WriteLine(string.Format("{0} {1} {2} {3}", time, begin, end, temp));
            bool A = temp > end, B = temp < begin, C = end < begin;
            Debug.WriteLine(string.Format("{0} {1} {2}", A, B, C));
            if (C)
            {
                if (A && B)
                    return NextRunTime(begin, --count, false);
                else
                    return temp;
            }
            else
            {
                if (!A && !B)
                    return temp;
                if (!A)
                    return NextRunTime(begin, --count, false);
                else
                    return NextRunTime(end, --count, false);
            }
        }
    }
}
