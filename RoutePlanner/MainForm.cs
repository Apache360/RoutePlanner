using BingMapsRESTToolkit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RoutePlanner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //metroDateTimeDepartureStart.Value = DateTime.Now.AddDays(1);
            //metroDateTimeDepartureEnd.Value = DateTime.Now.AddDays(2);
            metroDateTimeDepartureStart.Value = new DateTime(2022, 07, 04, 0, 0, 0);
            metroDateTimeDepartureEnd.Value = new DateTime(2022, 07, 05, 0, 0, 0);
            //metroTextBoxW0.Text = "41,780715, -83,560378";
            metroTextBoxW0.Text = "42,339544, -83,090409";
            metroTextBoxW1.Text = "42,912429, -78,885757";
            
                
            UpdateEstimatedAltVariantsCount();
        }


        public List<XmlDocument> responseXmlDocList;
        static string key = "ApNf4cdMo33Rss3h5mOCtQYIYgEsonbD4PatMfaq8-9RPSQ-orq8vnk3lMuEcMx9";
        DateTime dateTimeStart = new DateTime();
        DateTime dateTimeEnd = new DateTime();
        TimeSpan dateTimeDelta = new TimeSpan();
        //string dateTimeStartStr;
        int alternativeVariantsCount = 0;
        int recommendedAltVariantsCount = 1000;

        private async void buttonSearch_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*************************Test***********************");
            richTextBox1.Clear();
            buttonSearch.Enabled = false;
            buttonSearch.Text = "Wait...";
            AltVariantsCollection alternativeVariants = new AltVariantsCollection();
            responseXmlDocList = new List<XmlDocument>();
            //string wp0 = "39.951916,-75.150118";
            //string wp0 = "50.485068, 30.457412";
            //string wp1 = "40.745702,-73.847184";
            //string wp1 = "49.832458, 23.978329";

            //string wp0 = "41,838011, -83,540048";
            //string wp0 = "41,929006, -83,386938";
            //string wp1 = "42,912397, -78,885736";

            string wp0 = metroTextBoxW0.Text;
            string wp1 = metroTextBoxW1.Text;

            string dateTime = "07/04/2022%200:00:00";

            UpdateEstimatedAltVariantsCount();

            dateTimeStart = metroDateTimeDepartureStart.Value.Date;

            dateTimeStart = dateTimeStart.AddHours(Convert.ToDouble(numericUpDownDepStartHr.Value));
            dateTimeStart = dateTimeStart.AddMinutes(Convert.ToDouble(numericUpDownDepStartMin.Value));
            dateTimeStart = dateTimeStart.AddSeconds(Convert.ToDouble(numericUpDownDepStartSec.Value));

            dateTimeEnd = metroDateTimeDepartureEnd.Value.Date;
            dateTimeEnd = dateTimeEnd.AddHours(Convert.ToDouble(numericUpDownDepEndHr.Value));
            dateTimeEnd = dateTimeEnd.AddMinutes(Convert.ToDouble(numericUpDownDepEndMin.Value));
            dateTimeEnd = dateTimeEnd.AddSeconds(Convert.ToDouble(numericUpDownDepEndSec.Value));

            dateTimeDelta = dateTimeEnd.Subtract(dateTimeStart);
            alternativeVariantsCount = Convert.ToInt32( Math.Round( (dateTimeDelta.Ticks/ 10000000/60) / numericUpDownInterval.Value,0));


            for (int i = 0; i < alternativeVariantsCount; i++)
            {
                DateTime dateTimeTemp = dateTimeStart.AddMinutes(i * Convert.ToDouble(numericUpDownInterval.Value));

                string dateTimeTempStr = dateTimeTemp.ToString("yyyy'/'MM'/'dd'%20'H':'mm':'ss");
                //Console.WriteLine($"dateTimeTempStr {dateTimeTempStr}");

                var url = $"https://dev.virtualearth.net/REST/V1/Routes/Driving" +
                    $"?wp.0={wp0}" +
                    $"&wp.1={wp1}" +
                    $"&optmz=timeWithTraffic" +
                    $"&timeType=Departure" +
                    $"&dateTime={dateTimeTempStr}" +
                    $"&output=xml" +
                    $"&key={key}";
                //Console.WriteLine($"URL: {url}");


                XmlElement xRoot =null;
                int numberOfTries = 5;

                for (int j = 1; j <= numberOfTries+1; j++)
                {
                    if (j>numberOfTries)
                    {
                        Console.WriteLine($"Error. Can't get the response.");
                    }
                    try
                    {
                        Console.WriteLine("Getting response...");
                        XmlDocument response = GetXmlResponse(url);
                        xRoot = response.DocumentElement;
                        responseXmlDocList.Add(response);

                        //Console.WriteLine("Response is get successfully");
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Error. Can't get the response. Try {j}/{numberOfTries}.");
                    }
                }

                //Console.WriteLine("Reading XML started");

                if (xRoot != null)
                {
                    // обход всех узлов в корневом элементе
                    foreach (XmlElement xnode in xRoot)
                    {
                        if (xnode.Name == "Copyright")
                        {
                            XmlNode copynode = xnode;
                            //Console.WriteLine("Copyright: " + copynode.InnerText);
                        }

                        if (xnode.Name == "ResourceSets")
                        {
                            XmlNode ResourceSets = xnode;
                            foreach (XmlNode item in ResourceSets.ChildNodes)
                            {
                                if (item.Name == "ResourceSet")
                                {
                                    XmlNode ResourceSet = item;
                                    foreach (XmlNode item2 in ResourceSet.ChildNodes)
                                    {
                                        if (item2.Name == "Resources")
                                        {
                                            XmlNode Resources = item2;

                                            foreach (XmlNode item3 in Resources.ChildNodes)
                                            {
                                                if (item3.Name == "Route")
                                                {
                                                    XmlNode Route = item3;
                                                    foreach (XmlNode item4 in Route.ChildNodes)
                                                    {
                                                        if (item4.Name == "TravelDistance")
                                                        {
                                                            XmlNode TravelDistance = item4;
                                                            //Console.WriteLine("TravelDistance: " + TravelDistance.InnerText);
                                                        }
                                                        if (item4.Name == "TravelDuration")
                                                        {
                                                            XmlNode TravelDuration = item4;
                                                            //Console.WriteLine("TravelDuration: " + TravelDuration.InnerText);
                                                        }
                                                        if (item4.Name == "TravelDurationTraffic")
                                                        {
                                                            XmlNode TravelDurationTraffic = item4;
                                                            TimeSpan timeTravel = TimeSpan.FromSeconds(Convert.ToInt32(TravelDurationTraffic.InnerText));

                                                            string timeTravelStr = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                                                            timeTravel.Hours,
                                                                            timeTravel.Minutes,
                                                                            timeTravel.Seconds);

                                                            Console.WriteLine($"TravelDurationTraffic #{i}: " +
                                                                $"{dateTimeTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES"))}: " + timeTravelStr);
                                                            richTextBox1.Text += $"TravelDurationTraffic #{i}: " +
                                                                $"{dateTimeTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES"))}: " + timeTravelStr + Environment.NewLine;



                                                        }
                                                        ///
                                                        if (item4.Name == "RouteLeg")
                                                        {
                                                            XmlNode RouteLeg = item4;
                                                            foreach (XmlNode item5 in RouteLeg.ChildNodes)
                                                            {
                                                                if (item5.Name == "ItineraryItem")
                                                                {
                                                                    XmlNode ItineraryItem = item5;
                                                                    foreach (XmlNode item6 in ItineraryItem.ChildNodes)
                                                                    {
                                                                        try
                                                                        {
                                                                            if (item6.Name == "Warning" && item6.Attributes["warningType"].Value == "CountryChange")
                                                                            {
                                                                                Console.WriteLine("CountryChange!");
                                                                            }
                                                                        }
                                                                        catch (Exception)
                                                                        {
                                                                            Console.WriteLine("No CountryChange!");
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
                    }
                }
                //Console.WriteLine("Reading XML finished");
                buttonSearch.Enabled = true;
                buttonSearch.Text = "Search";

            }






            //////responseList = new List<Location>();
            //responseList = new List<Route>();

            //var request = new RouteRequest()
            //{

            //    Waypoints = new List<SimpleWaypoint>() {
            //        new SimpleWaypoint(39.951916,-75.150118),
            //        new SimpleWaypoint(40.745702,-73.847184)
            //        //new SimpleWaypoint(50.485068, 30.457412),
            //        //new SimpleWaypoint(49.832458, 23.978329)
            //        //new SimpleWaypoint(41.915423, -83.406422),
            //        //new SimpleWaypoint(42.979566, -78.868626)
            //    },
            //    WaypointOptimization = BingMapsRESTToolkit.Extensions.TspOptimizationType.TravelDistance,
            //    RouteOptions = new RouteOptions()
            //    {
            //        TravelMode = TravelModeType.Driving,
            //        TimeType = RouteTimeType.Departure,
            //        //DateTime = new DateTime(2022, 07, 04, 8, 45, 0, DateTimeKind.Local)
            //        //DateTime = new DateTime(2022, 07, 04, 8, 45, 0)
            //        DateTime = new DateTime(637925211000000000)
            //    },
            //    BingMapsKey = key,
            //    //Culture = "en-US"
            //    Culture = "ua"
            //};


            //Console.WriteLine(request.GetRequestUrl());
            //Console.WriteLine(request.Culture);
            //Console.WriteLine(request.Domain);
            //Console.WriteLine(request.BatchSize);
            ////Console.WriteLine(request.RouteOptions.DateTime.Value.TimeOfDay.ToString());
            //Console.WriteLine(request.RouteOptions.TravelMode.ToString());
            //Console.WriteLine(request.RouteOptions.TimeType.ToString());
            //Console.WriteLine(request.RouteOptions.DistanceUnits);
            //Console.WriteLine(request.UserRegion);
            //Console.WriteLine(request.UserIp);
            //Console.WriteLine(request.UserLocation);
            //Console.WriteLine(request.Waypoints.Count);
            //Console.WriteLine(request.Waypoints[0].Address);
            //Console.WriteLine(request.Waypoints[1].Address);

            //var response = await request.Execute();

            //if (response != null &&
            //    response.ResourceSets != null &&
            //    response.ResourceSets.Length > 0 &&
            //    response.ResourceSets[0].Resources != null &&
            //    response.ResourceSets[0].Resources.Length > 0)
            //{
            //    responseList.Add(response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Route);

            //    Console.WriteLine(responseList[0].ToString());
            //    Console.WriteLine("*************************Test2***********************");

            //    richTextBox1.Text += "ActualStart.Coordinates[0]: " + responseList[0].RouteLegs[0].ActualStart.Coordinates[0]+Environment.NewLine;
            //    richTextBox1.Text += "ActualStart.Coordinates[1]: " + responseList[0].RouteLegs[0].ActualStart.Coordinates[1] + Environment.NewLine;

            //    richTextBox1.Text += "StartTime: " + responseList[0].RouteLegs[0].StartTime + Environment.NewLine;
            //    richTextBox1.Text += "StartTimeUtc.TimeOfDay.ToString(): " + responseList[0].RouteLegs[0].StartTimeUtc.TimeOfDay.ToString() + Environment.NewLine;
            //    //richTextBox1.Text += "StartLocation.Address.FormattedAddress: " + responseList[0].RouteLegs[0].StartLocation.Address.AddressLine;

            //    richTextBox1.Text += "EndTime: " + responseList[0].RouteLegs[0].EndTime + Environment.NewLine;
            //    richTextBox1.Text += "EndTimeUtc.TimeOfDay.ToString(): " + responseList[0].RouteLegs[0].EndTimeUtc.TimeOfDay.ToString() + Environment.NewLine;
            //    //richTextBox1.Text += "EndLocation.Address.FormattedAddress: " + responseList[0].RouteLegs[0].EndLocation.Address.AddressLine;

            //    richTextBox1.Text += "TravelDistance: " + responseList[0].RouteLegs[0].TravelDistance + Environment.NewLine;
            //    richTextBox1.Text += "TravelDuration: " + responseList[0].RouteLegs[0].TravelDuration + Environment.NewLine;

            //    //richTextBox1.Text += "AddressLine: " + responseList[0].Address.AddressLine +Environment.NewLine;
            //    //richTextBox1.Text += "AdminDistrict: " + responseList[0].Address.AdminDistrict + Environment.NewLine;
            //    //richTextBox1.Text += "AdminDistrict2: " + responseList[0].Address.AdminDistrict2 + Environment.NewLine;
            //    //richTextBox1.Text += "CountryRegion: " + responseList[0].Address.CountryRegion + Environment.NewLine;
            //    //richTextBox1.Text += "CountryRegionIso2: " + responseList[0].Address.CountryRegionIso2 + Environment.NewLine;
            //    //richTextBox1.Text += "FormattedAddress: " + responseList[0].Address.FormattedAddress + Environment.NewLine;
            //    //richTextBox1.Text += "Landmark: " + responseList[0].Address.Landmark + Environment.NewLine;
            //    //richTextBox1.Text += "Locality: " + responseList[0].Address.Locality + Environment.NewLine;
            //    //richTextBox1.Text += "Neighborhood: " + responseList[0].Address.Neighborhood + Environment.NewLine;
            //    //richTextBox1.Text += "PostalCode: " + responseList[0].Address.PostalCode + Environment.NewLine;
            //    //richTextBox1.Text += "Point.Coordinates[0]: " + responseList[0].Point.Coordinates[0] + Environment.NewLine;
            //    //richTextBox1.Text += "Point.Coordinates[1]: " + responseList[0].Point.Coordinates[1] + Environment.NewLine;
            //    //Do something with the result.
            //}




        }

        public void UpdateEstimatedAltVariantsCount()
        {
            dateTimeStart = metroDateTimeDepartureStart.Value.Date;

            dateTimeStart = dateTimeStart.AddHours(Convert.ToDouble(numericUpDownDepStartHr.Value));
            dateTimeStart = dateTimeStart.AddMinutes(Convert.ToDouble(numericUpDownDepStartMin.Value));
            dateTimeStart = dateTimeStart.AddSeconds(Convert.ToDouble(numericUpDownDepStartSec.Value));
            //dateTimeStartStr = dateTimeStart.ToString("G", CultureInfo.GetCultureInfo("es-ES"));

            dateTimeEnd = metroDateTimeDepartureEnd.Value.Date;
            dateTimeEnd = dateTimeEnd.AddHours(Convert.ToDouble(numericUpDownDepEndHr.Value));
            dateTimeEnd = dateTimeEnd.AddMinutes(Convert.ToDouble(numericUpDownDepEndMin.Value));
            dateTimeEnd = dateTimeEnd.AddSeconds(Convert.ToDouble(numericUpDownDepEndSec.Value));

            //Console.WriteLine($"{dateTimeStart.ToString("G", CultureInfo.GetCultureInfo("es-ES"))}");

            dateTimeDelta = dateTimeEnd.Subtract(dateTimeStart);
            alternativeVariantsCount = Convert.ToInt32(Math.Floor((dateTimeDelta.Ticks / 10000000 / 60) / numericUpDownInterval.Value));

            if (alternativeVariantsCount > 0)
            {
                if (alternativeVariantsCount > recommendedAltVariantsCount)
                {
                    metroLabelEstimatedAltVariantsCount.Text = $"{alternativeVariantsCount}\n(>{recommendedAltVariantsCount} is not recommended)";
                }
                else
                {
                    metroLabelEstimatedAltVariantsCount.Text = alternativeVariantsCount.ToString();
                }
                buttonSearch.Enabled = true ;
            }
            else
            {

                metroLabelEstimatedAltVariantsCount.Text = "Should be more than 0";
                buttonSearch.Enabled = false;
            }

        }

        public static XmlDocument GetXmlResponse(string requestUrl)
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

        /// <summary>  
        /// Display each "entry" in the Bing Spatial Data Services Atom (XML) response.  
        /// </summary>  
        /// <param name="entryElements"></param>  
        private void ProcessEntityElements(XmlDocument response)
        {
            XmlNodeList entryElements = response.GetElementsByTagName("entry");
            for (int i = 0; i <= entryElements.Count - 1; i++)
            {
                XmlElement element = (XmlElement)entryElements[i];
                XmlElement contentElement = (XmlElement)element.GetElementsByTagName(
                  "content")[0];
                XmlElement propElement = (XmlElement)
                  contentElement.GetElementsByTagName("m:properties")[0];
                XmlNode nameElement = propElement.GetElementsByTagName("d:Name")[0];
                if (nameElement == null)
                    throw new Exception("Name not found");
                XmlNode latElement = propElement.GetElementsByTagName("d:Latitude")[0];
                if (latElement == null)
                    throw new Exception("Latitude not found");
                XmlNode longElement = propElement.GetElementsByTagName("d:Longitude")
                  [0];
                if (longElement == null)
                    throw new Exception("Longitude not found");
                string name = nameElement.InnerText;
                double latitude = 0;
                Double.TryParse(latElement.InnerText, out latitude);
                double longitude = 0;
                Double.TryParse(longElement.InnerText, out longitude);
                Console.WriteLine("Coordinates of '{0}': {1}, {2}", name, latitude,
                  longitude);
            }
        }

        //Departure End
        private void numericUpDownDepEndHr_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        private void numericUpDownDepEndMin_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        private void numericUpDownDepEndSec_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        //Departure Start
        private void numericUpDownDepStartHr_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        private void numericUpDownDepStartMin_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        private void numericUpDownDepStartSec_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        //Date Time picker
        private void metroDateTimeDepartureEnd_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        private void metroDateTimeDepartureStart_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        //interval
        private void numericUpDownInterval_ValueChanged(object sender, EventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }

        //coefficients
        private void numericUpDownF1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownF2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDownF3_ValueChanged(object sender, EventArgs e)
        {

        }


        private void numericUpDownDepStartHr_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            UpdateEstimatedAltVariantsCount();
        }




        //private void Search(RouteRequest request)
        //{
        //    //Process the request by using the ServiceManager.

        //    var _response = request.Execute();

        //    if (_response != null &&
        //        _response.Result.ResourceSets != null &&
        //        _response.Result.ResourceSets.Length > 0 &&
        //        _response.Result.ResourceSets[0].Resources != null &&
        //        _response.Result.ResourceSets[0].Resources.Length > 0)
        //    {
        //        responseList.Add(_response.Result.ResourceSets[0].Resources[0] as Route);

        //        Console.WriteLine(responseList[0].ToString());
        //        Console.WriteLine("*************************Test2***********************");

        //        richTextBox1.Text += "ActualStart.Coordinates[0]: " + responseList[0].RouteLegs[0].ActualStart.Coordinates[0];
        //        richTextBox1.Text += "ActualStart.Coordinates[1]: " + responseList[0].RouteLegs[0].ActualStart.Coordinates[1];
        //        //Do something with the result.
        //    }
        //}

        //private async Task SearchAsync(RouteRequest request)
        //{
        //    //Process the request by using the ServiceManager.

        //    var _response = await request.Execute();

        //    if (_response != null &&
        //        _response.ResourceSets != null &&
        //        _response.ResourceSets.Length > 0 &&
        //        _response.ResourceSets[0].Resources != null &&
        //        _response.ResourceSets[0].Resources.Length > 0)
        //    {
        //        responseList.Add(_response.ResourceSets[0].Resources[0] as Route);

        //        Console.WriteLine(responseList[0].ToString());
        //        Console.WriteLine("*************************Test2***********************");

        //        richTextBox1.Text += "ActualStart.Coordinates[0]: " + responseList[0].RouteLegs[0].ActualStart.Coordinates[0];
        //        richTextBox1.Text += "ActualStart.Coordinates[1]: " + responseList[0].RouteLegs[0].ActualStart.Coordinates[1];
        //        //Do something with the result.
        //    }
        //}

    }
}
