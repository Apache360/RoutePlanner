using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner
{
    public class AltVariantsCollection : List<AlternativeVariant>
    {
        public AltVariantsCollection GetLocalValues(AltVariantsCollection collection)
        {
            double evaluationDeparuteTimeMin= double.MaxValue;
            //double evaluationDeparuteTimeMax=0;
            double evaluationDelayTimeMin= double.MaxValue;
            //double evaluationDelayTimeMax=0;
            foreach (var altVar in collection)
            {
                if (altVar.evaluationDeparuteTime < evaluationDeparuteTimeMin) evaluationDeparuteTimeMin = altVar.evaluationDeparuteTime;
                //if (altVar.evaluationDeparuteTime > evaluationDeparuteTimeMax) evaluationDeparuteTimeMax = altVar.evaluationDeparuteTime;
                if (altVar.evaluationDelayTime < evaluationDelayTimeMin) evaluationDelayTimeMin = altVar.evaluationDelayTime;
                //if (altVar.evaluationDelayTime > evaluationDelayTimeMax) evaluationDelayTimeMax = altVar.evaluationDelayTime;
            }

            foreach (var altVar in collection)
            {
                altVar.evaluationDeparuteTime -= evaluationDeparuteTimeMin;
                altVar.evaluationDelayTime -= evaluationDelayTimeMin;
            }
            return collection;
        }
    }
}
