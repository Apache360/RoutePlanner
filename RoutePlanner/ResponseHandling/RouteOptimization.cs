using RoutePlanner.ResponseHandling.ResponseNodes;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            ResponseHandling.ResponseNodes.Response responseRaw;
            //route leg rewrite

            string wp0;
            string wp1;
            string key = "ApNf4cdMo33Rss3h5mOCtQYIYgEsonbD4PatMfaq8-9RPSQ-orq8vnk3lMuEcMx9";
            route.travelDurationTraffic = new TimeSpan();



            TimeSpan travelDurationTrafficTemp1 = new TimeSpan();
            TimeSpan travelDurationTrafficTemp2 = new TimeSpan();
            for (int i = 0; i < route.routeLeg.itineraryItems.Count-1; i++)
            {

                wp0 = $"{route.routeLeg.itineraryItems[i].maneuverPoint.Latitude}, {route.routeLeg.itineraryItems[i].maneuverPoint.Longitude}";
                wp1 = $"{route.routeLeg.itineraryItems[i + 1].maneuverPoint.Latitude}, {route.routeLeg.itineraryItems[i+1].maneuverPoint.Longitude}";
                DateTime dateTimeDepartureTemp=new DateTime();
                if (i == 0)
                {
                    dateTimeDepartureTemp = route.departureTime;
                    Console.WriteLine(dateTimeDepartureTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES")));
                }
                else
                {
                    dateTimeDepartureTemp = route.routeLeg.itineraryItems[i - 1].localDepartureTime
                    + TimeSpan.FromSeconds(route.routeLeg.itineraryItems[i - 1].travelDuration);
                    Console.WriteLine(dateTimeDepartureTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES")));

                    //dateTimeDepartureTemp = DateTime.ParseExact(route.routeLeg.startTime, "yyyy-MM-dd'T'HH:mm::ss", null);
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
                Console.WriteLine($"URL: {url}");


                XmlElement xRoot = ResponseHandler.GetResponse(url);
                responseRaw = ResponseHandler.ReadResponse(xRoot, dateTimeDepartureTemp);

                responseRaw.resourceSets.resourseSet.resources.route.routeLeg.itineraryItems[0].localDepartureTime = dateTimeDepartureTemp;

                route.travelDurationTraffic += responseRaw.resourceSets.resourseSet.resources.route.travelDurationTraffic;
                //route.travelDurationTraffic += TimeSpan.FromSeconds(route.routeLeg.itineraryItems[i].travelDuration);
                //travelDurationTrafficTemp1 += TimeSpan.FromSeconds(route.routeLeg.itineraryItems[i].travelDuration);

                string totalDurationTemp = route.travelDurationTraffic.ToString("c");
                //string totalDurationTemp = travelDurationTrafficTemp1.ToString("c");
                Console.WriteLine($"#{i}: totalDurationTemp {totalDurationTemp}");
                //if (i == route.routeLeg.itineraryItems.Count - 2)
                //{
                //    Console.WriteLine($"#{i}: totalDurationTemp {totalDurationTemp}");
                //}


                //if (i == route.routeLeg.itineraryItems.Count - 2)
                //{
                //    //for the total changes
                //    route.travelDurationTraffic = route.travelDurationTraffic + responseRaw.resourceSets.resourseSet.resources.route.travelDurationTraffic;

                //    string travelDurationTemp = route.travelDurationTraffic.ToString("c");
                //    Console.WriteLine("travelDurationTemp: "+travelDurationTemp);
                //}

                Console.WriteLine($"responseRaw #{i}: {responseRaw}");
                //Console.WriteLine($"responseRaw #{i}: {response}");



                //route.routeLeg.itineraryItems[i].localDepartureTime = route.departureTime;

                //dateTimeTempStr = route.routeLeg.itineraryItems[i].localDepartureTime.ToString("yyyy'/'MM'/'dd'%20'H':'mm':'ss");

                //url = $"https://dev.virtualearth.net/REST/V1/Routes/Driving" +
                //    $"?wp.0={wp0}" +
                //    $"&wp.1={wp1}" +
                //    $"&optmz=timeWithTraffic" +
                //    $"&timeType=Departure" +
                //    $"&dateTime={dateTimeTempStr}" +
                //    $"&output=xml" +
                //    $"&key={key}";
                //Console.WriteLine($"URL: {url}");


                //xRoot = ResponseHandler.GetResponse(url);
                //responseRaw = ResponseHandler.ReadResponse(xRoot, dateTimeDepartureTemp);

                //responseRaw.resourceSets.resourseSet.resources.route.routeLeg.itineraryItems[0].localDepartureTime = dateTimeDepartureTemp;
                ////route.travelDurationTraffic += TimeSpan.FromSeconds(route.routeLeg.itineraryItems[i].travelDuration);
                //travelDurationTrafficTemp1 += TimeSpan.FromSeconds(route.routeLeg.itineraryItems[i].travelDuration);
                ////totalDurationTemp = route.travelDurationTraffic.ToString("c");
                //totalDurationTemp = travelDurationTrafficTemp1.ToString("c");
                //Console.WriteLine($"#{i}: totalDurationTemp {totalDurationTemp}");
                //Console.WriteLine($"responseRaw #{i}: {responseRaw}");

                //route.routeLeg.itineraryItems[i].localDepartureTime = dateTimeDepartureTemp;

            }

            return response;
        }   
    }
}
