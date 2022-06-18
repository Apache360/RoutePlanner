using RoutePlanner.ResponseHandling.ResponseNodes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Xml;

namespace RoutePlanner.ResponseHandling
{
    class ResponseHandler
    {
        public static XmlElement GetResponse(string url)
        {
            XmlDocument response = null;

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
            XmlElement xRoot = response.DocumentElement;
            return xRoot;
        }

        public int GetCountryChangeCount(Response response)
        {
            int countryChangeCount = 0;
            foreach (ItineraryItem itineraryItem in response.ResourceSets.ResourseSet.Resources.Route.routeLeg.itineraryItems)
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
            NumberFormatInfo provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            if (xRoot != null)
            {
                // обход всех узлов в корневом элементе
                foreach (XmlElement nodeInRoot in xRoot)
                {
                    if (nodeInRoot.Name == "ResourceSets")
                    {
                        XmlNode resourceSets = nodeInRoot;
                        response.ResourceSets = new ResourceSets();
                        foreach (XmlNode nodeInResourceSets in resourceSets.ChildNodes)
                        {
                            if (nodeInResourceSets.Name == "ResourceSet")
                            {
                                XmlNode resourceSet = nodeInResourceSets;
                                response.ResourceSets.ResourseSet = new ResourseSet();
                                foreach (XmlNode nodeInResourceSet in resourceSet.ChildNodes)
                                {
                                    if (nodeInResourceSet.Name == "Resources")
                                    {
                                        XmlNode resources = nodeInResourceSet;
                                        response.ResourceSets.ResourseSet.Resources = new Resources();
                                        foreach (XmlNode nodeInResources in resources.ChildNodes)
                                        {
                                            if (nodeInResources.Name == "Route")
                                            {
                                                XmlNode route = nodeInResources;
                                                response.ResourceSets.ResourseSet.Resources.Route = new Route
                                                {
                                                    departureTime = departureTime
                                                };
                                                foreach (XmlNode nodeInRoute in route.ChildNodes)
                                                {
                                                    if (nodeInRoute.Name == "DistanceUnit")
                                                    {
                                                        response.ResourceSets.ResourseSet.Resources.Route.distanceUnit = nodeInRoute.InnerText;
                                                    }
                                                    if (nodeInRoute.Name == "DurationUnit")
                                                    {
                                                        response.ResourceSets.ResourseSet.Resources.Route.durationUnit = nodeInRoute.InnerText;
                                                    }
                                                    if (nodeInRoute.Name == "TravelDistance")
                                                    {
                                                        response.ResourceSets.ResourseSet.Resources.Route.travelDistance = Double.Parse( nodeInRoute.InnerText, provider);
                                                    }
                                                    if (nodeInRoute.Name == "TravelDuration")
                                                    {
                                                        TimeSpan travelDuration = TimeSpan.FromSeconds(Convert.ToInt32(nodeInRoute.InnerText));
                                                        response.ResourceSets.ResourseSet.Resources.Route.travelDuration = travelDuration;
                                                    }
                                                    if (nodeInRoute.Name == "TravelDurationTraffic")
                                                    {
                                                        XmlNode travelDurationTrafficNode = nodeInRoute;
                                                        TimeSpan travelDurationTraffic = TimeSpan.FromSeconds(Convert.ToInt32(travelDurationTrafficNode.InnerText));
                                                        response.ResourceSets.ResourseSet.Resources.Route.travelDurationTraffic = travelDurationTraffic;

                                                        string travelDurationTrafficStr = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                        travelDurationTraffic.Hours,
                                                                        travelDurationTraffic.Minutes,
                                                                        travelDurationTraffic.Seconds);
                                                        response.ResourceSets.ResourseSet.Resources.Route.travelDurationTrafficStr = travelDurationTrafficStr;
                                                    }
                                                    if (nodeInRoute.Name == "TravelMode")
                                                    {
                                                        response.ResourceSets.ResourseSet.Resources.Route.travelMode = nodeInRoute.InnerText;
                                                    }

                                                    if (nodeInRoute.Name == "RouteLeg")
                                                    {
                                                        XmlNode routeLeg = nodeInRoute;
                                                        response.ResourceSets.ResourseSet.Resources.Route.routeLeg = new RouteLeg
                                                        {
                                                            itineraryItems = new List<ItineraryItem>()
                                                        };

                                                        foreach (XmlNode nodeInRouteLeg in routeLeg.ChildNodes)
                                                        {
                                                            if (nodeInRouteLeg.Name == "TravelDistance")
                                                            {
                                                                response.ResourceSets.ResourseSet.Resources.Route.routeLeg.travelDistance = Double.Parse(nodeInRouteLeg.InnerText, provider);
                                                            }
                                                            if (nodeInRouteLeg.Name == "TravelDuration")
                                                            {
                                                                response.ResourceSets.ResourseSet.Resources.Route.routeLeg.travelDuration = Convert.ToInt32(nodeInRouteLeg.InnerText);
                                                            }
                                                            if (nodeInRouteLeg.Name == "TravelMode")
                                                            {
                                                                response.ResourceSets.ResourseSet.Resources.Route.routeLeg.travelMode = nodeInRouteLeg.InnerText;
                                                            }
                                                            if (nodeInRouteLeg.Name == "ActualStart")
                                                            {
                                                                response.ResourceSets.ResourseSet.Resources.Route.routeLeg.actualStart = 
                                                                    new BingMapsRESTToolkit.Coordinate(
                                                                        Double.Parse(nodeInRouteLeg.ChildNodes.Item(0).InnerText, provider),
                                                                        Double.Parse(nodeInRouteLeg.ChildNodes.Item(1).InnerText, provider)
                                                                        );
                                                            }
                                                            if (nodeInRouteLeg.Name == "ActualEnd")
                                                            {
                                                                response.ResourceSets.ResourseSet.Resources.Route.routeLeg.actualEnd = 
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
                                                                response.ResourceSets.ResourseSet.Resources.Route.routeLeg.itineraryItems.Add(itineraryItemTemp);
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
