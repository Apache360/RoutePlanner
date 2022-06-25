using System;

namespace RoutePlanner
{
    public class AlternativeVariant
    {
        public int id;
        public DateTime DeparuteTime { get; set; }
        public double EvaluationDeparuteTime { get; set; }
        public double EvaluationDelayTime { get; set; }
        public double EvaluationTravelTime { get; set; }
        public double EvaluationCoutryChange { get; set; }
        public double EvaluationTotal { get; set; }

        public AlternativeVariant()
        {
        }

        public AlternativeVariant(int id, DateTime deparuteTime, double evaluationDeparuteTime, double evaluationDelayTime, double evaluationCoutryChange)
        {
            this.id = id;
            this.DeparuteTime = deparuteTime;
            this.EvaluationDeparuteTime = evaluationDeparuteTime; // in raw: total seconds for departure
            this.EvaluationTravelTime = evaluationDelayTime; // in raw: seconds for delay time
            this.EvaluationCoutryChange = evaluationCoutryChange; // in raw: count of crossing the country border
        }

        public override string ToString()
        {
            return $"AlternativeVariant - id: {id}" +
                $"\nDeparuteTime: {DeparuteTime}" +
                $"\nEvaluationDeparuteTime: {EvaluationDeparuteTime}" +
                $"\nEvaluationDelayTime: {EvaluationDelayTime}" +
                $"\nEvaluationTravelTime: {EvaluationTravelTime}" +
                $"\nEvaluationCoutryChange: {EvaluationCoutryChange}" +
                $"\nEvaluationTotal: {EvaluationTotal}";
        }
    }
}
