using RoutePlanner.ResponseHandling.ResponseNodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RoutePlanner.ResponseHandling
{
    class RouteOptimization
    {
        public Response Optimize(Response response)
        {
            Route route = response.resourceSets.resourseSet.resources.route;
            double travelDistance = route.travelDistance;
            TimeSpan travelDuration = route.travelDuration;
            TimeSpan travelDurationTraffic = route.travelDurationTraffic;
            string travelDurationTrafficStr = route.travelDurationTrafficStr;

            //route leg rewrite

            for (int i = 0; i < route.routeLeg.itineraryItems.Count; i++)
            {

            }

            return response;
        }   
    }
}
