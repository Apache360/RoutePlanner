using System.Collections.Generic;

namespace RoutePlanner.DepartureTimeRulesHandling
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
                                    if (departureTimeRule.daysOfWeek.Contains(alternativeVariant.DeparuteTime.DayOfWeek))
                                    {
                                        alternativeVariant.EvaluationTotal *= departureTimeRule.ruleCoefficient;
                                    }
                                }
                                break;
                            case RuleTimeType.SpecialTime:
                                foreach (AlternativeVariant alternativeVariant in alternativeVariants)
                                {
                                    if (departureTimeRule.daysOfWeek.Contains(alternativeVariant.DeparuteTime.DayOfWeek)
                                        && alternativeVariant.DeparuteTime.TimeOfDay>= departureTimeRule.ruleTimeSpanInterval.startTime
                                        && alternativeVariant.DeparuteTime.TimeOfDay <= departureTimeRule.ruleTimeSpanInterval.endTime)
                                    {
                                        alternativeVariant.EvaluationTotal *= departureTimeRule.ruleCoefficient;
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
                                    if (alternativeVariant.DeparuteTime >= departureTimeRule.ruleDateTimeInterval.startDateTime
                                        && alternativeVariant.DeparuteTime <= departureTimeRule.ruleDateTimeInterval.endDateTime)
                                    {
                                        alternativeVariant.EvaluationTotal *= departureTimeRule.ruleCoefficient;
                                    }
                                }
                                break;
                            case RuleTimeType.SpecialTime:
                                foreach (AlternativeVariant alternativeVariant in alternativeVariants)
                                {
                                    if (alternativeVariant.DeparuteTime >= departureTimeRule.ruleDateTimeInterval.startDateTime
                                        && alternativeVariant.DeparuteTime <= departureTimeRule.ruleDateTimeInterval.endDateTime
                                        && alternativeVariant.DeparuteTime.TimeOfDay >= departureTimeRule.ruleTimeSpanInterval.startTime
                                        && alternativeVariant.DeparuteTime.TimeOfDay <= departureTimeRule.ruleTimeSpanInterval.endTime)
                                    {
                                        alternativeVariant.EvaluationTotal *= departureTimeRule.ruleCoefficient;
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
