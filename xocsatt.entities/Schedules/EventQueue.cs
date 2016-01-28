using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOcsatt.Entities.Schedules
{
    /// <summary>
	/// The event queue is a collection of scheduled items that represents the union of all child scheduled items.
	/// This is useful for events that occur every 10 minutes or at multiple intervals not covered by the simple
	/// scheduled items.
	/// </summary>
    public class EventQueue : IScheduledItem
    {
        private List<IScheduledItem> _agenda;

        public List<IScheduledItem> Agenda
        {
            get { return _agenda; }
            set { _agenda = value; }
        }

        public EventQueue()
        {
            _agenda = new List<IScheduledItem>();
        }
        /// <summary>
        /// Adds a ScheduledTime to the queue.
        /// </summary>
        /// <param name="time">The scheduled time to add</param>
        public void Add(IScheduledItem time)
        {
            _agenda.Add(time);
        }

        /// <summary>
        /// Clears the list of scheduled times.
        /// </summary>
        public void Clear()
        {
            _agenda.Clear();
        }

        /// <summary>
        /// Adds the running time for all events in the list.
        /// </summary>
        /// <param name="begin">The beginning time of the interval</param>
        /// <param name="end">The end time of the interval</param>
        /// <param name="List">The list to add times to.</param>
        public IEnumerable<DateTime> AddEventsInInterval(DateTime begin, DateTime end)
        {
            List<DateTime> agenda = new List<DateTime>();

            foreach (IScheduledItem st in this.Agenda)
                agenda.AddRange(st.AddEventsInInterval(begin, end));

            agenda.Sort();

            return agenda;
        }

        /// <summary>
        /// Returns the first time after the starting time for all events in the list.
        /// </summary>
        /// <param name="time">The starting time.</param>
        /// <param name="AllowExact">If this is true then it allows the return time to match the time parameter, false forces the return time to be greater then the time parameter</param>
        /// <returns>Either the next event after the input time or greater or equal to depending on the AllowExact parameter.</returns>
        public DateTime NextRunTime(DateTime time, bool includeStartTime)
        {
            DateTime next = DateTime.MaxValue;
            //Get minimum datetime from the list.
            foreach (IScheduledItem st in this.Agenda)
            {
                DateTime Proposed = st.NextRunTime(time, includeStartTime);
                next = (Proposed < next) ? Proposed : next;
            }

            return next;
        }
    }
}
