using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner
{
    public class DepartureTimeRuleHandler
    {
        public static AltVariantsCollection CalculateDepartureTimeRules(AltVariantsCollection alternativeVariants, List<DepartureTimeRule> departureTimeRules)
        {
            foreach (DepartureTimeRule departureTimeRule in departureTimeRules)
            {
                switch (departureTimeRule.ruleDayType)
                {
                    case RuleDayType.DaysOfWeek:
                        switch (departureTimeRule.ruleTimeType)
                        {
                            case RuleTimeType.WholeDay:
                                foreach (AlternativeVariant alternativeVariant in alternativeVariants)
                                {
                                    if (departureTimeRule.daysOfWeek.Contains(alternativeVariant.deparuteTime.DayOfWeek))
                                    {
                                        alternativeVariant.evaluationTotal *= departureTimeRule.ruleCoefficient;
                                    }
                                }
                                break;
                            case RuleTimeType.SpecialTime:
                                foreach (AlternativeVariant alternativeVariant in alternativeVariants)
                                {
                                    if (departureTimeRule.daysOfWeek.Contains(alternativeVariant.deparuteTime.DayOfWeek)
                                        && alternativeVariant.deparuteTime.TimeOfDay> departureTimeRule.ruleTimeSpanInterval.startTime
                                        && alternativeVariant.deparuteTime.TimeOfDay < departureTimeRule.ruleTimeSpanInterval.endTime)
                                    {
                                        alternativeVariant.evaluationTotal *= departureTimeRule.ruleCoefficient;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    case RuleDayType.SpecialDays:
                        switch (departureTimeRule.ruleTimeType)
                        {
                            case RuleTimeType.WholeDay:
                                foreach (AlternativeVariant alternativeVariant in alternativeVariants)
                                {
                                    if (alternativeVariant.deparuteTime > departureTimeRule.ruleDateTimeInterval.startDateTime
                                        && alternativeVariant.deparuteTime < departureTimeRule.ruleDateTimeInterval.endDateTime)
                                    {
                                        alternativeVariant.evaluationTotal *= departureTimeRule.ruleCoefficient;
                                    }
                                }
                                break;
                            case RuleTimeType.SpecialTime:
                                foreach (AlternativeVariant alternativeVariant in alternativeVariants)
                                {
                                    if (alternativeVariant.deparuteTime > departureTimeRule.ruleDateTimeInterval.startDateTime
                                        && alternativeVariant.deparuteTime < departureTimeRule.ruleDateTimeInterval.endDateTime
                                        && alternativeVariant.deparuteTime.TimeOfDay > departureTimeRule.ruleTimeSpanInterval.startTime
                                        && alternativeVariant.deparuteTime.TimeOfDay < departureTimeRule.ruleTimeSpanInterval.endTime)
                                    {
                                        alternativeVariant.evaluationTotal *= departureTimeRule.ruleCoefficient;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
            return alternativeVariants;
        }
    }
}
