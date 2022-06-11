using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner
{
    public class AlternativeVariant
    {
        public int id;
        public DateTime deparuteTime { get; set; }
        public double evaluationDeparuteTime { get; set; }
        public double evaluationDelayTime { get; set; }
        public double evaluationCoutryChange { get; set; }
        public double evaluationTotal { get; set; }

        public AlternativeVariant(int id, DateTime deparuteTime, double evaluationDeparuteTime, double evaluationDelayTime, double evaluationCoutryChange)
        {
            this.id = id;
            this.deparuteTime = deparuteTime;
            //this.evaluationDeparuteTime = deparuteTime.Ticks/10000000; // in raw: total seconds for departure
            this.evaluationDeparuteTime = evaluationDeparuteTime; // in raw: total seconds for departure
            this.evaluationDelayTime = evaluationDelayTime; // in raw: seconds for delay time
            this.evaluationCoutryChange = evaluationCoutryChange; // in raw: count of crossing the country border
        }
    }
}
