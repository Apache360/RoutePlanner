using RoutePlanner.ResponseHandling.ResponseNodes;
using System;
using System.Xml;

namespace RoutePlanner.ResponseHandling
{
    class RouteOptimization
    {
        public Response Optimize(Response response, MainForm mainForm)
        {
            Route route = response.ResourceSets.ResourseSet.Resources.Route;
            Response responseRaw;
            mainForm.UpdateElapsedTime();

            string wp0;
            string wp1;
            string key = "ApNf4cdMo33Rss3h5mOCtQYIYgEsonbD4PatMfaq8-9RPSQ-orq8vnk3lMuEcMx9";
            route.travelDurationTraffic = new TimeSpan();
            for (int i = 0; i < route.routeLeg.itineraryItems.Count-1; i++)
            {

                wp0 = $"{route.routeLeg.itineraryItems[i].maneuverPoint.Latitude}, {route.routeLeg.itineraryItems[i].maneuverPoint.Longitude}";
                wp1 = $"{route.routeLeg.itineraryItems[i + 1].maneuverPoint.Latitude}, {route.routeLeg.itineraryItems[i+1].maneuverPoint.Longitude}";
                DateTime dateTimeDepartureTemp;
                if (i == 0)
                {
                    dateTimeDepartureTemp = route.departureTime;
                    //Console.WriteLine(dateTimeDepartureTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES")));
                }
                else
                {
                    dateTimeDepartureTemp = route.routeLeg.itineraryItems[i - 1].localDepartureTime
                    + TimeSpan.FromSeconds(route.routeLeg.itineraryItems[i - 1].travelDuration);
                    //Console.WriteLine(dateTimeDepartureTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES")));
                }
                route.routeLeg.itineraryItems[i].localDepartureTime = dateTimeDepartureTemp;

                string dateTimeTempStr = route.routeLeg.itineraryItems[i].localDepartureTime.ToString("yyyy'/'MM'/'dd'%20'H':'mm':'ss");
                var url = $"https://dev.virtualearth.net/REST/V1/Routes/Driving" +
                    $"?wp.0={wp0}" +
                    $"&wp.1={wp1}" +
                    $"&optmz=timeWithTraffic" +
                    $"&timeType=Departure" +
                    $"&dateTime={dateTimeTempStr}" +
                    $"&output=xml" +
                    $"&key={key}";
                //Console.WriteLine(url);

                Console.WriteLine($"#{i} Getting response...") ;
                XmlElement xRoot = ResponseHandler.GetResponse(url);
                responseRaw = ResponseHandler.ReadResponse(xRoot, dateTimeDepartureTemp);

                responseRaw.ResourceSets.ResourseSet.Resources.Route.routeLeg.itineraryItems[0].localDepartureTime = dateTimeDepartureTemp;
                route.travelDurationTraffic += responseRaw.ResourceSets.ResourseSet.Resources.Route.travelDurationTraffic;

                string travelDurationTrafficStr = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                    route.travelDurationTraffic.Hours,
                    route.travelDurationTraffic.Minutes,
                    route.travelDurationTraffic.Seconds);
                route.travelDurationTrafficStr = travelDurationTrafficStr;

                route.routeLeg.itineraryItems[i].localDepartureTime = dateTimeDepartureTemp;
                route.routeLeg.itineraryItems[i].travelDurationTraffic = Convert.ToInt32(responseRaw.ResourceSets.ResourseSet.Resources.Route.travelDurationTraffic.TotalSeconds);
                if (i == route.routeLeg.itineraryItems.Count - 2)
                mainForm.UpdateElapsedTime();
            }
            return response;
        }   
    }
}
