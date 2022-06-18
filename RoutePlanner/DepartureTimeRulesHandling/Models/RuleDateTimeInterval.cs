using System;

namespace RoutePlanner.DepartureTimeRulesHandling
{
    public class RuleDateTimeInterval
    {
        public DateTime startDateTime;
        public DateTime endDateTime;

        public RuleDateTimeInterval(DateTime startDateTime, DateTime endDateTime)
        {
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
        }
    }
}
