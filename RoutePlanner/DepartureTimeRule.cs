using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner
{
    public class DepartureTimeRule
    {
        public int id;
        public double ruleCoefficient;

        public RuleDayType ruleDayType;

        //if RuleDayType DaysOfWeek
        public List<DayOfWeek> daysOfWeek;
        //if RuleDayType SpecialDays
        public RuleDateTimeInterval ruleDateTimeInterval;

        public RuleTimeType ruleTimeType;

        //if RuleTimeType SpecialTime
        public RuleTimeSpanInterval ruleTimeSpanInterval;

        public DepartureTimeRule(int id, 
            double ruleCoefficient, 
            RuleDayType ruleDayType, 
            List<DayOfWeek> daysOfWeek, 
            RuleDateTimeInterval ruleDateTimeInterval, 
            RuleTimeType ruleTimeType, 
            RuleTimeSpanInterval ruleTimeSpanInterval)
        {
            this.id = id;
            this.ruleCoefficient = ruleCoefficient;
            this.ruleDayType = ruleDayType;
            this.daysOfWeek = daysOfWeek;
            this.ruleDateTimeInterval = ruleDateTimeInterval;
            this.ruleTimeType = ruleTimeType;
            this.ruleTimeSpanInterval = ruleTimeSpanInterval;
        }

        public static int GetAvaliableId(List<DepartureTimeRule> departureTimeRules)
        {
            int id = 0;
            foreach (DepartureTimeRule rule in departureTimeRules)
            {
                if (rule.id>=id)
                {
                    id=rule.id+1;
                }
            }
            return id;
        }

        public override string ToString()
        {
            string daysOfWeekStr="days of week: ";
            foreach (DayOfWeek dayOfWeek in this.daysOfWeek)
            {
                daysOfWeekStr += $"{dayOfWeek} ";
            }

            return $"Departure Time Rule:" +
                $"\n\tid: {id}" +
                $"\n\truleCoefficient: {ruleCoefficient}" +
                $"\n\truleDayType: {ruleDayType}" +
                $"\n\t{daysOfWeekStr}" +
                $"\n\truleDateTimeInterval: {ruleDateTimeInterval.startDateTime} {ruleDateTimeInterval.endDateTime}" +
                $"\n\truleTimeType: {ruleTimeType}" +
                $"\n\truleTimeSpanInterval: {ruleTimeSpanInterval.startTime} {ruleTimeSpanInterval.endTime}";
        }
    }

    public enum RuleDayType
    {
        DaysOfWeek = 0,
        SpecialDays = 1
    }

    public enum RuleTimeType
    {
        WholeDay = 0,
        SpecialTime = 1
    }

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
