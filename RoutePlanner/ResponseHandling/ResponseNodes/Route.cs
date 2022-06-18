using System;
using System.Globalization;

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class Route
    {
        public string distanceUnit;
        public string durationUnit;
        public double travelDistance;
        public TimeSpan travelDuration;
        public TimeSpan travelDurationTraffic;
        public string travelDurationTrafficStr;
        public string travelMode;
        public DateTime departureTime;
        public RouteLeg routeLeg;

        public override string ToString()
        {
            string travelDurationTemp = travelDuration.ToString("c");
            string departureTimeTemp = departureTime.ToString("G", CultureInfo.GetCultureInfo("es-ES"));
            return $"Route:" +
                $"\n\tdistanceUnit: {distanceUnit}" +
                $"\n\tdurationUnit: {durationUnit}" +
                $"\n\ttravelDistance: {travelDistance}" +
                $"\n\ttravelDuration: {travelDurationTemp}" +
                $"\n\ttravelDurationTraffic: {travelDurationTrafficStr}" +
                $"\n\ttravelMode: {travelMode}" +
                $"\n\tdepartureTime: {departureTimeTemp}" +
                $"{routeLeg}";
        }
    }
}
