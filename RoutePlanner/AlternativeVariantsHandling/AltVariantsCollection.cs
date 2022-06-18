using System;
using System.Collections.Generic;

namespace RoutePlanner
{
    public class AltVariantsCollection : List<AlternativeVariant>
    {
        public AltVariantsCollection()
        {

        }

        public AltVariantsCollection Normalize(int intervalMin, double min = 0, double max = 100)
        {
            double evaluationDepartureTimeMin= double.MaxValue;
            double evaluationTravelTimeMin= double.MaxValue;
            double evaluationCoutryChangeMin = double.MaxValue;

            double evaluationDepartureTimeMax = double.MinValue;
            double evaluationTravelTimeMax = double.MinValue;
            double evaluationCoutryChangeMax = double.MinValue;

            foreach (AlternativeVariant altVar in this)
            {
                if (altVar.EvaluationDeparuteTime < evaluationDepartureTimeMin) evaluationDepartureTimeMin = altVar.EvaluationDeparuteTime;
                if (altVar.EvaluationTravelTime < evaluationTravelTimeMin) evaluationTravelTimeMin = altVar.EvaluationTravelTime;
                if (altVar.EvaluationCoutryChange < evaluationCoutryChangeMin) evaluationCoutryChangeMin = altVar.EvaluationCoutryChange;

                if (altVar.EvaluationDeparuteTime > evaluationDepartureTimeMax) evaluationDepartureTimeMax = altVar.EvaluationDeparuteTime;
                if (altVar.EvaluationTravelTime > evaluationTravelTimeMax) evaluationTravelTimeMax = altVar.EvaluationTravelTime;
                if (altVar.EvaluationCoutryChange > evaluationCoutryChangeMax) evaluationCoutryChangeMax = altVar.EvaluationCoutryChange;
            }

            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationTravelTimeMax != 0)
                {

                    altVar.EvaluationDelayTime = altVar.EvaluationTravelTime - evaluationTravelTimeMin;
                }
            }
            double evaluationDelayTimeMax = double.MinValue;

            foreach (AlternativeVariant altVar in this)
            {
                if (altVar.EvaluationDelayTime > evaluationDelayTimeMax) evaluationDelayTimeMax = altVar.EvaluationDelayTime;
            }

            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationDepartureTimeMax!=0)
                {
                    altVar.EvaluationDeparuteTime *= (max / evaluationDepartureTimeMax);
                    altVar.EvaluationDeparuteTime = Math.Round( max-altVar.EvaluationDeparuteTime,2);
                }
                if (evaluationDelayTimeMax != 0)
                {
                    altVar.EvaluationDelayTime *= (max / evaluationDelayTimeMax);
                    altVar.EvaluationDelayTime = Math.Round(max - altVar.EvaluationDelayTime,2);
                }
                if (evaluationCoutryChangeMax!=0)
                {
                    altVar.EvaluationCoutryChange *= (max / evaluationCoutryChangeMax);
                    altVar.EvaluationCoutryChange = Math.Round(max - altVar.EvaluationCoutryChange,2);
                }
            }
            return this;
        }

        public AltVariantsCollection GetDelayTime ()
        {
            double evaluationTravelTimeMin = double.MaxValue;
            foreach (AlternativeVariant altVar in this)
            {
                if (altVar.EvaluationTravelTime < evaluationTravelTimeMin) evaluationTravelTimeMin = altVar.EvaluationTravelTime;
            }
            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationTravelTimeMin != 0)
                {
                    altVar.EvaluationDelayTime = altVar.EvaluationTravelTime- evaluationTravelTimeMin;
                }
            }
            return this;
        }

        public AltVariantsCollection EvaluateTotal(double coefF1, double coefF2, double coefF3)
        {
            coefF1 /= 100;
            coefF2 /= 100;
            coefF3 /= 100;
            foreach (AlternativeVariant altVar in this)
            {
                altVar.EvaluationDeparuteTime *= coefF1;
                altVar.EvaluationDelayTime *= coefF2;
                altVar.EvaluationCoutryChange *= coefF3;
                altVar.EvaluationTotal = (altVar.EvaluationDeparuteTime + altVar.EvaluationDelayTime + altVar.EvaluationCoutryChange)/3;
                altVar.EvaluationTotal = Math.Round(altVar.EvaluationTotal, 2);
            }
            return this;
        }

        public AlternativeVariant FindBest()
        {
            double evaluationTotalMax = Double.MinValue;
            AlternativeVariant altVarBest= new AlternativeVariant();
            foreach (AlternativeVariant altVar in this)
            {
                if (evaluationTotalMax < altVar.EvaluationTotal)
                {
                    evaluationTotalMax = altVar.EvaluationTotal;
                    altVarBest = altVar;
                }
            }
            return altVarBest;
        }

        public AltVariantsCollection Clone(AltVariantsCollection collectionOrigin)
        {
            AltVariantsCollection collection = new AltVariantsCollection();
            foreach (AlternativeVariant altVariantOrigin in collectionOrigin)
            {
                AlternativeVariant altVariant = new AlternativeVariant
                {
                    id = altVariantOrigin.id,
                    DeparuteTime = altVariantOrigin.DeparuteTime,
                    EvaluationDeparuteTime = altVariantOrigin.EvaluationDeparuteTime,
                    EvaluationDelayTime = altVariantOrigin.EvaluationDelayTime,
                    EvaluationTravelTime = altVariantOrigin.EvaluationTravelTime,
                    EvaluationCoutryChange = altVariantOrigin.EvaluationCoutryChange,
                    EvaluationTotal = altVariantOrigin.EvaluationTotal
                };
                collection.Add(altVariant);
            }
            return collection;
        }
    }
}
