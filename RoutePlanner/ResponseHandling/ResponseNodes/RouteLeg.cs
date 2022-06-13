using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutePlanner.ResponseHandling.ResponseNodes
{
    class RouteLeg
    {
        public double travelDistance;
        public int travelDuration;
        public string travelMode;
        public Coordinate actualStart;
        public Coordinate actualEnd;
        public List<ItineraryItem> itineraryItems;
        public string startTime;
        public string endTime;

        public override string ToString()
        {
            string itineraryItemsStr="";
            foreach (ItineraryItem itineraryItem in itineraryItems)
            {
                itineraryItemsStr += itineraryItem.ToString();
            }
            return $"\n\tRouteLeg:" +
                $"\n\t\ttravelDistance: {travelDistance}" +
                $"\n\t\ttravelDuration: {travelDuration}" +
                $"\n\t\ttravelMode: {travelMode}" +
                $"\n\t\tactualStart: {actualStart}" +
                $"\n\t\tactualEnd: {actualEnd}" +
                $"\n\t\tstartTime: {startTime}" +
                $"\n\t\tendTime: {endTime}" +
                $"\n\t\titineraryItems: {itineraryItemsStr}";
        }
    }
}
