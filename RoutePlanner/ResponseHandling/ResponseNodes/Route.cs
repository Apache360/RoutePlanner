using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public RouteLeg routeLeg;

        public override string ToString()
        {
            string travelDurationTemp = travelDuration.ToString("c");
            return $"Route:" +
                $"\n\tdistanceUnit: {distanceUnit}" +
                $"\n\tdurationUnit: {durationUnit}" +
                $"\n\ttravelDistance: {travelDistance}" +
                $"\n\ttravelDuration: {travelDurationTemp}" +
                $"\n\ttravelDurationTraffic: {travelDurationTrafficStr}" +
                $"\n\ttravelMode: {travelMode}" +
                $"{routeLeg}";
        }
    }
}
