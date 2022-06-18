using BingMapsRESTToolkit;
using System;
using System.Globalization;

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class ItineraryItem
    {
        public string travelMode;
        public double travelDistance;
        public int travelDuration;
        public int travelDurationTraffic;
        public Coordinate maneuverPoint;
        public string compassDirection;
        public int countryChangeCount=0;
        public DateTime localDepartureTime;

        public override string ToString()
        {
            string localDepartureTemp = localDepartureTime.ToString("G", CultureInfo.GetCultureInfo("es-ES"));
            return $"\n\t\tItineraryItem:" +
                $"\n\t\t\ttravelMode: {travelMode}" +
                $"\n\t\t\ttravelDistance: {travelDistance}" +
                $"\n\t\t\ttravelDurationTraffic: {travelDurationTraffic}" +
                $"\n\t\t\ttravelDuration: {travelDuration}" +
                $"\n\t\t\tmaneuverPoint: {maneuverPoint}" +
                $"\n\t\t\tcompassDirection: {compassDirection}" +
                $"\n\t\t\tlocalDepartureTime: {localDepartureTemp}" +
                $"\n\t\t\tcountryChangeCount: {countryChangeCount}";
        }
    }
}
