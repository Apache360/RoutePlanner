using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class ItineraryItem
    {
        public string travelMode;
        public double travelDistance;
        public int travelDuration;
        public Coordinate maneuverPoint;
        public string compassDirection;
        public int countryChangeCount=0;

        public override string ToString()
        {
            return $"\n\t\tItineraryItem:" +
                $"\n\t\t\ttravelMode: {travelMode}" +
                $"\n\t\t\ttravelDistance: {travelDistance}" +
                $"\n\t\t\ttravelDuration: {travelDuration}" +
                $"\n\t\t\tmaneuverPoint: {maneuverPoint}" +
                $"\n\t\t\tcompassDirection: {compassDirection}" +
                $"\n\t\t\tcountryChangeCount: {countryChangeCount}";
        }
    }
}
