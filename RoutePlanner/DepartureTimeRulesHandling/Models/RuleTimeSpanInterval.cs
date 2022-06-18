using System;

namespace RoutePlanner.DepartureTimeRulesHandling
{
    public class RuleTimeSpanInterval
    {
        public TimeSpan startTime;
        public TimeSpan endTime;

        public RuleTimeSpanInterval(TimeSpan startTime, TimeSpan endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;
        }
    } 
}
