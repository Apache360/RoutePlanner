using RoutePlanner.ResponseHandling.ResponseNodes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RoutePlanner.ResponseHandling
{
    class ResponseHandler
    {
        public static XmlElement GetResponse(string url)
        {
            XmlElement xRoot = null;
            XmlDocument response = null;

            //Console.WriteLine("Getting response...");
            int triesCount =10;
            for (int i = 0; i < 10; i++)
            {
                response = GetXmlResponse(url);
                if (response!=null)
                {
                    break;
                }
                Console.WriteLine($"Error. Getting response again... Try {i+1}/{triesCount}");
            }
            xRoot = response.DocumentElement;
            return xRoot;
        }

        public int GetCountryChangeCount(Response response)
        {
            int countryChangeCount = 0;
            foreach (ItineraryItem itineraryItem in response.resourceSets.resourseSet.resources.route.routeLeg.itineraryItems)
            {
                countryChangeCount += itineraryItem.countryChangeCount;
            }
            return countryChangeCount;
        }

        private static XmlDocument GetXmlResponse(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());
                return (xmlDoc);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.Read();
                return null;
            }
        }

        public static Response ReadResponse(XmlElement xRoot, DateTime departureTime)
        {
            Response response = new Response();
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement nodeInRoot in xRoot)
                {
                    if (nodeInRoot.Name == "ResourceSets")
                    {
                        XmlNode resourceSets = nodeInRoot;
                        response.resourceSets = new ResourceSets();
                        foreach (XmlNode nodeInResourceSets in resourceSets.ChildNodes)
                        {
                            if (nodeInResourceSets.Name == "ResourceSet")
                            {
                                XmlNode resourceSet = nodeInResourceSets;
                                response.resourceSets.resourseSet = new ResourseSet();
                                foreach (XmlNode nodeInResourceSet in resourceSet.ChildNodes)
                                {
                                    if (nodeInResourceSet.Name == "Resources")
                                    {
                                        XmlNode resources = nodeInResourceSet;
                                        response.resourceSets.resourseSet.resources = new Resources();
                                        foreach (XmlNode nodeInResources in resources.ChildNodes)
                                        {
                                            if (nodeInResources.Name == "Route")
                                            {
                                                XmlNode route = nodeInResources;
                                                response.resourceSets.resourseSet.resources.route = new Route();
                                                response.resourceSets.resourseSet.resources.route.departureTime = departureTime;
                                                foreach (XmlNode nodeInRoute in route.ChildNodes)
                                                {
                                                    if (nodeInRoute.Name == "DistanceUnit")
                                                    {
                                                        response.resourceSets.resourseSet.resources.route.distanceUnit = nodeInRoute.InnerText;
                                                    }
                                                    if (nodeInRoute.Name == "DurationUnit")
                                                    {
                                                        response.resourceSets.resourseSet.resources.route.durationUnit = nodeInRoute.InnerText;
                                                    }
                                                    if (nodeInRoute.Name == "TravelDistance")
                                                    {
                                                        response.resourceSets.resourseSet.resources.route.travelDistance = Double.Parse( nodeInRoute.InnerText, provider);
                                                    }
                                                    if (nodeInRoute.Name == "TravelDuration")
                                                    {
                                                        TimeSpan travelDuration = TimeSpan.FromSeconds(Convert.ToInt32(nodeInRoute.InnerText));
                                                        response.resourceSets.resourseSet.resources.route.travelDuration = travelDuration;
                                                    }
                                                    if (nodeInRoute.Name == "TravelDurationTraffic")
                                                    {
                                                        XmlNode travelDurationTrafficNode = nodeInRoute;
                                                        TimeSpan travelDurationTraffic = TimeSpan.FromSeconds(Convert.ToInt32(travelDurationTrafficNode.InnerText));
                                                        response.resourceSets.resourseSet.resources.route.travelDurationTraffic = travelDurationTraffic;

                                                        string travelDurationTrafficStr = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                        travelDurationTraffic.Hours,
                                                                        travelDurationTraffic.Minutes,
                                                                        travelDurationTraffic.Seconds);
                                                        response.resourceSets.resourseSet.resources.route.travelDurationTrafficStr = travelDurationTrafficStr;
                                                    }
                                                    if (nodeInRoute.Name == "TravelMode")
                                                    {
                                                        response.resourceSets.resourseSet.resources.route.travelMode = nodeInRoute.InnerText;
                                                    }

                                                    if (nodeInRoute.Name == "RouteLeg")
                                                    {
                                                        XmlNode routeLeg = nodeInRoute;
                                                        response.resourceSets.resourseSet.resources.route.routeLeg = new RouteLeg();
                                                        response.resourceSets.resourseSet.resources.route.routeLeg.itineraryItems = new List<ItineraryItem>();

                                                        foreach (XmlNode nodeInRouteLeg in routeLeg.ChildNodes)
                                                        {
                                                            if (nodeInRouteLeg.Name == "TravelDistance")
                                                            {
                                                                response.resourceSets.resourseSet.resources.route.routeLeg.travelDistance = Double.Parse(nodeInRouteLeg.InnerText, provider);
                                                            }
                                                            if (nodeInRouteLeg.Name == "TravelDuration")
                                                            {
                                                                response.resourceSets.resourseSet.resources.route.routeLeg.travelDuration = Convert.ToInt32(nodeInRouteLeg.InnerText);
                                                            }
                                                            if (nodeInRouteLeg.Name == "TravelMode")
                                                            {
                                                                response.resourceSets.resourseSet.resources.route.routeLeg.travelMode = nodeInRouteLeg.InnerText;
                                                            }
                                                            if (nodeInRouteLeg.Name == "ActualStart")
                                                            {
                                                                response.resourceSets.resourseSet.resources.route.routeLeg.actualStart = 
                                                                    new BingMapsRESTToolkit.Coordinate(
                                                                        Double.Parse(nodeInRouteLeg.ChildNodes.Item(0).InnerText, provider),
                                                                        Double.Parse(nodeInRouteLeg.ChildNodes.Item(1).InnerText, provider)
                                                                        );
                                                            }
                                                            if (nodeInRouteLeg.Name == "ActualEnd")
                                                            {
                                                                response.resourceSets.resourseSet.resources.route.routeLeg.actualEnd = 
                                                                    new BingMapsRESTToolkit.Coordinate(
                                                                        Double.Parse(nodeInRouteLeg.ChildNodes.Item(0).InnerText, provider),
                                                                        Double.Parse(nodeInRouteLeg.ChildNodes.Item(1).InnerText, provider)
                                                                        );
                                                            }
                                                            if (nodeInRouteLeg.Name == "ItineraryItem")
                                                            {
                                                                XmlNode itineraryItem = nodeInRouteLeg;
                                                                ItineraryItem itineraryItemTemp = new ItineraryItem();
                                                                foreach (XmlNode nodeInItineraryItem in itineraryItem.ChildNodes)
                                                                {
                                                                    if (nodeInItineraryItem.Name == "TravelMode")
                                                                    {
                                                                        itineraryItemTemp.travelMode = nodeInItineraryItem.InnerText;
                                                                    }
                                                                    if (nodeInItineraryItem.Name == "TravelDistance")
                                                                    {
                                                                        itineraryItemTemp.travelDistance = Double.Parse(nodeInItineraryItem.InnerText, provider);
                                                                    }
                                                                    if (nodeInItineraryItem.Name == "TravelDuration")
                                                                    {
                                                                        itineraryItemTemp.travelDuration = Convert.ToInt32(nodeInItineraryItem.InnerText);
                                                                    }
                                                                    if (nodeInItineraryItem.Name == "ManeuverPoint")
                                                                    {
                                                                        itineraryItemTemp.maneuverPoint =
                                                                            new BingMapsRESTToolkit.Coordinate(
                                                                                Double.Parse(nodeInItineraryItem.ChildNodes.Item(0).InnerText, provider),
                                                                                Double.Parse(nodeInItineraryItem.ChildNodes.Item(1).InnerText, provider)
                                                                                );
                                                                    }
                                                                    if (nodeInItineraryItem.Name == "CompassDirection")
                                                                    {
                                                                        itineraryItemTemp.compassDirection = nodeInItineraryItem.InnerText;
                                                                    }
                                                                    if (nodeInItineraryItem.Name == "Warning" && nodeInItineraryItem.Attributes["warningType"].Value == "CountryChange")
                                                                    {
                                                                        itineraryItemTemp.countryChangeCount += 1;
                                                                        //Console.WriteLine("CountryChange!");
                                                                    }
                                                                }
                                                                response.resourceSets.resourseSet.resources.route.routeLeg.itineraryItems.Add(itineraryItemTemp);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return response;
        }
    }
}
