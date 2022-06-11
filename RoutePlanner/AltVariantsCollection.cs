﻿using System;
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
            double evaluationTravelTimeMin= double.MaxValue;
            double evaluationCoutryChangeMin = double.MaxValue;

            double evaluationDepartureTimeMax = double.MinValue;
            double evaluationTravelTimeMax = double.MinValue;
            double evaluationCoutryChangeMax = double.MinValue;

            foreach (AlternativeVariant altVar in this)
            {
                if (altVar.evaluationDeparuteTime < evaluationDepartureTimeMin) evaluationDepartureTimeMin = altVar.evaluationDeparuteTime;
                if (altVar.evaluationTravelTime < evaluationTravelTimeMin) evaluationTravelTimeMin = altVar.evaluationTravelTime;
                if (altVar.evaluationCoutryChange < evaluationCoutryChangeMin) evaluationCoutryChangeMin = altVar.evaluationCoutryChange;

                if (altVar.evaluationDeparuteTime > evaluationDepartureTimeMax) evaluationDepartureTimeMax = altVar.evaluationDeparuteTime;
                if (altVar.evaluationTravelTime > evaluationTravelTimeMax) evaluationTravelTimeMax = altVar.evaluationTravelTime;
                if (altVar.evaluationCoutryChange > evaluationCoutryChangeMax) evaluationCoutryChangeMax = altVar.evaluationCoutryChange;
            }

            //Console.WriteLine(evaluationDelayTimeMax);
            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationTravelTimeMax != 0)
                {

                    altVar.evaluationDelayTime = altVar.evaluationTravelTime - evaluationTravelTimeMin;
                    //altVar.evaluationDelayTime -= evaluationTravelTimeMin;
                }
            }
            //evaluationTravelTimeMax = double.MinValue;
            double evaluationDelayTimeMax = double.MinValue;

            foreach (AlternativeVariant altVar in this)
            {
                if (altVar.evaluationDelayTime > evaluationDelayTimeMax) evaluationDelayTimeMax = altVar.evaluationDelayTime;
            }
            //Console.WriteLine(evaluationDelayTimeMax);

            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationDepartureTimeMax!=0)
                {
                    altVar.evaluationDeparuteTime *= (max / evaluationDepartureTimeMax);
                    altVar.evaluationDeparuteTime = max-altVar.evaluationDeparuteTime;
                }
                if (evaluationDelayTimeMax != 0)
                {
                    altVar.evaluationDelayTime *= (max / evaluationDelayTimeMax);
                    altVar.evaluationDelayTime = max - altVar.evaluationDelayTime;
                }
                if (evaluationCoutryChangeMax!=0)
                {
                    altVar.evaluationCoutryChange *= (max / evaluationCoutryChangeMax);
                    altVar.evaluationCoutryChange = max - altVar.evaluationCoutryChange;
                }
            }

            return this;
        }

        public AltVariantsCollection GetDelayTime ()
        {
            //double evaluationTravelTimeMax = double.MinValue;
            double evaluationTravelTimeMin = double.MaxValue;
            foreach (AlternativeVariant altVar in this)
            {
                if (altVar.evaluationTravelTime < evaluationTravelTimeMin) evaluationTravelTimeMin = altVar.evaluationTravelTime;
            }

            //Console.WriteLine(evaluationDelayTimeMax);
            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationTravelTimeMin != 0)
                {
                    altVar.evaluationDelayTime = altVar.evaluationTravelTime- evaluationTravelTimeMin;
                }
            }
            return this;
        }

        public AltVariantsCollection EvaluateTotal(double coefF1, double coefF2, double coefF3)
        {

            //Console.WriteLine($"{coefF1} {coefF2} {coefF3}");
            coefF1 /= 100;
            coefF2 /= 100;
            coefF3 /= 100;
            //Console.WriteLine($"{coefF1} {coefF2} {coefF3}");
            foreach (AlternativeVariant altVar in this)
            {

                //Console.WriteLine($"{altVar.evaluationDeparuteTime} {altVar.evaluationDelayTime} {altVar.evaluationCoutryChange}");
                altVar.evaluationDeparuteTime *= coefF1;
                altVar.evaluationDelayTime *= coefF2;
                altVar.evaluationCoutryChange *= coefF3;
                altVar.evaluationTotal = (altVar.evaluationDeparuteTime + altVar.evaluationDelayTime + altVar.evaluationCoutryChange);
                altVar.evaluationTotal = Math.Round(altVar.evaluationTotal, 2);
            }
            return this;
        }

        public AlternativeVariant FindBest()
        {
            double evaluationTotalMax = Double.MinValue;
            AlternativeVariant altVarBest= new AlternativeVariant();
            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationTotalMax < altVar.evaluationTotal)
                {
                    //Console.WriteLine(altVar.evaluationTotal);
                    evaluationTotalMax = altVar.evaluationTotal;
                    altVarBest = altVar;
                }
            }
            return altVarBest;
        }
    }
}
