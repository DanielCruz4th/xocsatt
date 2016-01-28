using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XOcsatt.Entities.Schedules;

namespace UnitTest
{
    [TestClass]
    public class ScheduleTest
    {
        [TestMethod]
        public void SimpleIntervalTest()
        {
            var startDate = new DateTime(2016, 1, 28);

            // Run every hour starting from 2016/1/28
            var simpleScheduler = new SimpleInterval(startDate, TimeSpan.FromHours(1));

            // Get events from the next two days
            var simpleAgenda = simpleScheduler.GetEventsInInterval(startDate, startDate.AddDays(2));

            System.Diagnostics.Debug.WriteLine("simpleAgenda events: {0}", simpleAgenda.Count());
            // Events from two days must be returned
            Assert.IsTrue(simpleAgenda.Count() == 48);

            // Run every hours twelve times starting from 2016/1/28
            var limitedScheduler = new SimpleInterval(startDate, TimeSpan.FromHours(1), 12);

            // Get events from next day.
            var limitedAgenda = limitedScheduler.GetEventsInInterval(startDate, startDate.AddDays(1));

            System.Diagnostics.Debug.WriteLine("limitedAgenda events: {0}", limitedAgenda.Count());
            // Only twelve events must be returned
            Assert.IsTrue(limitedAgenda.Count() == 12);

            // Run every hour starting from 2016/1/28 to 2016/1/29
            var dailyScheduler = new SimpleInterval(
                startDate,
                TimeSpan.FromHours(1),
                startDate.AddDays(1)
                );

            // Gets events from next two days
            var dailyAgenda = dailyScheduler.GetEventsInInterval(startDate, startDate.AddDays(2));

            System.Diagnostics.Debug.WriteLine("dailyAgenda events: {0}", dailyAgenda.Count());
            // Only events from one day are returned
            Assert.IsTrue(dailyAgenda.Count() == 24);

            // Run every hour starting from 2016/1/28 but with an end time in the past.
            var invalidScheduler = new SimpleInterval(startDate, TimeSpan.FromHours(1), startDate.AddDays(-1));

            // Get none events
            var invalidAgenda = invalidScheduler.GetEventsInInterval(simpleScheduler.StartTime, simpleScheduler.EndTime);

            System.Diagnostics.Debug.WriteLine("invalidAgenda events: {0}", invalidAgenda.Count());
            // It shouldn't return any events
            Assert.IsTrue(invalidAgenda.Count() == 0);

            // Run every hour starting from 2016/1/28
            var unlimitedScheduler = new SimpleInterval(startDate, TimeSpan.FromHours(1));

            // Get events from the following day but starting 6 hours after 2016/1/28 00:00:00
            var partialAgenda = unlimitedScheduler.GetEventsInInterval(startDate.AddHours(6), startDate.AddDays(1));

            System.Diagnostics.Debug.WriteLine("partialAgenda events: {0}", partialAgenda.Count());
            // Partial query should return only eighteen items.
            Assert.IsTrue(partialAgenda.Count() == 18);

            // Empty scheduler with no interval
            var anotherScheduler = new SimpleInterval(startDate, TimeSpan.Zero, startDate);

            // Get none events
            var anotherAgenda = anotherScheduler.GetEventsInInterval(startDate, startDate.AddDays(1));

            System.Diagnostics.Debug.WriteLine("anotherAgenda events: {0}", anotherAgenda.Count());
            // I shoudn't return any event.
            Assert.IsTrue(anotherAgenda.Count() == 0);

            anotherAgenda = anotherScheduler.GetEventsInInterval(startDate, startDate);

            System.Diagnostics.Debug.WriteLine("anotherAgenda events: {0}", anotherAgenda.Count());
            // I shoudn't return any event.
            Assert.IsTrue(anotherAgenda.Count() == 0);
        }

        [TestMethod]
        public void ScheduledTimeTest()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void SingleEvent()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void QueuedEvent()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void BlockWrapper()
        {
            Assert.IsTrue(false);
        }

        public SimpleInterval HrsScheduler { get; set; }
    }
}
