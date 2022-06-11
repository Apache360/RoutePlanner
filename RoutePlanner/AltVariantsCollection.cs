using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner
{
    public class AltVariantsCollection : List<AlternativeVariant>
    {
        public AltVariantsCollection()
        {

        }

        public AltVariantsCollection Normalize(int intervalMin,  double min=0, double max=100)
        {
            double evaluationDepartureTimeMin= double.MaxValue;
            double evaluationDelayTimeMin= double.MaxValue;
            double evaluationCoutryChangeMin = double.MaxValue;

            double evaluationDepartureTimeMax = double.MinValue;
            double evaluationDelayTimeMax = double.MinValue;
            double evaluationCoutryChangeMax = double.MinValue;

            foreach (AlternativeVariant altVar in this)
            {
                if (altVar.evaluationDeparuteTime < evaluationDepartureTimeMin) evaluationDepartureTimeMin = altVar.evaluationDeparuteTime;
                if (altVar.evaluationDelayTime < evaluationDelayTimeMin) evaluationDelayTimeMin = altVar.evaluationDelayTime;
                if (altVar.evaluationCoutryChange < evaluationCoutryChangeMin) evaluationCoutryChangeMin = altVar.evaluationCoutryChange;

                if (altVar.evaluationDeparuteTime > evaluationDepartureTimeMax) evaluationDepartureTimeMax = altVar.evaluationDeparuteTime;
                if (altVar.evaluationDelayTime > evaluationDelayTimeMax) evaluationDelayTimeMax = altVar.evaluationDelayTime;
                if (altVar.evaluationCoutryChange > evaluationCoutryChangeMax) evaluationCoutryChangeMax = altVar.evaluationCoutryChange;
            }

            Console.WriteLine(evaluationDelayTimeMax);
            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationDelayTimeMax != 0)
                {
                    altVar.evaluationDelayTime -= evaluationDelayTimeMin;
                }
            }
            evaluationDelayTimeMax = double.MinValue;
            foreach (AlternativeVariant altVar in this)
            {
                if (altVar.evaluationDelayTime > evaluationDelayTimeMax) evaluationDelayTimeMax = altVar.evaluationDelayTime;
            }
            Console.WriteLine(evaluationDelayTimeMax);

            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationDepartureTimeMax!=0)
                {
                    altVar.evaluationDeparuteTime *= (max / evaluationDepartureTimeMax);
                }
                if (evaluationDelayTimeMax!=0)
                {
                    altVar.evaluationDelayTime *= (max / evaluationDelayTimeMax);
                }
                if (evaluationCoutryChangeMax!=0)
                {
                    altVar.evaluationCoutryChange *= (max / evaluationCoutryChangeMax);
                }
            }

            return this;
        }

        public AltVariantsCollection EvaluateTotal()
        {
            foreach (AlternativeVariant altVar in this)
            {
                altVar.evaluationTotal = (altVar.evaluationDeparuteTime + altVar.evaluationDelayTime + altVar.evaluationCoutryChange)/3;
            }
            return this;
        }
    }
}
