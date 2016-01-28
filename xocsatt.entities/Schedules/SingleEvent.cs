using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Web.Utilities;

namespace XOcsatt.Entities.Schedules
{
    /// <summary>Single event represents an event which only fires once.</summary>
    public class SingleEvent : IScheduledItem
    {
        private DateTime _eventTime;

        public DateTime EventTime
        {
            get { return _eventTime; }
            set { _eventTime = value; }
        }

        public SingleEvent(DateTime eventTime)
        {
            _eventTime = eventTime;
        }
        #region IScheduledItem Members

        public IEnumerable<DateTime> GetEventsInInterval(DateTime begin, DateTime end)
        {
            List<DateTime> agenda = new List<DateTime>();

            if (begin <= this.EventTime && end > this.EventTime)
                agenda.Add(this.EventTime);

            return agenda;
        }

        public DateTime NextRunTime(DateTime time, bool includeStartTime)
        {
            if (includeStartTime)
                return (this.EventTime >= time) ? this.EventTime : DateTime.MaxValue.RoundToSqlDateTime();
            else
                return (this.EventTime > time) ? this.EventTime : DateTime.MaxValue.RoundToSqlDateTime();
        }

        #endregion
    }
}
