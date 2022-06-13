﻿using BingMapsRESTToolkit;
using Microsoft.PowerBI.Api.Models;
using Newtonsoft.Json;
using RoutePlanner.ResponseHandling;
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
using System.Windows.Media.Imaging;
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
            numericUpDownDepStartHr.Value = 6;
            numericUpDownDepStartMin.Value =45;
            metroDateTimeDepartureEnd.Value = new DateTime(2022, 07, 05, 0, 0, 0);
            //metroTextBoxW0.Text = "41,780715, -83,560378";
            //metroTextBoxW0.Text = "42,339544, -83,090409";
            //metroTextBoxW1.Text = "42,912429, -78,885757";
            //metroTextBoxW0.Text = "3835 Luna Pier Rd, Erie, Мічиган 48133, Сполучені Штати";
            //metroTextBoxW1.Text = "19 18th St, Буффало, Нью-Йорк 14213, Сполучені Штати";
            metroTextBoxW0.Text = "39.951916,-75.150118";
            metroTextBoxW1.Text = "40.745702,-73.847184";

            SetUpDataGridView();
            UpdateEstimatedAltVariantsCount();
            
        }

        static readonly string key = "ApNf4cdMo33Rss3h5mOCtQYIYgEsonbD4PatMfaq8-9RPSQ-orq8vnk3lMuEcMx9";
        DateTime dateTimeStart = new DateTime();
        DateTime dateTimeEnd = new DateTime();
        TimeSpan dateTimeDelta = new TimeSpan();
        int alternativeVariantsCount = 0;
        readonly int recommendedAltVariantsCount = 1000;

        AltVariantsCollection altVariantsListRaw;
        AltVariantsCollection altVariantsListRawOptz;
        AltVariantsCollection altVariantsList;

        string wp0;
        string wp1;
        AlternativeVariant altVarBest;
        RouteOptimization routeOptimization;
        ResponseHandler responseHandler;

        List<ResponseHandling.ResponseNodes.Response> responseRawList;

        private async void ButtonSearch_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*************************Test***********************");
            richTextBox1.Clear();
            dataGridViewProfitMatrix.Rows.Clear();
            dataGridViewProfitMatrix.Refresh();

            buttonSearch.Enabled = false;
            buttonSearch.Text = "Wait...";
            altVariantsListRaw = new AltVariantsCollection();
            altVariantsListRawOptz = new AltVariantsCollection();
            altVariantsList = new AltVariantsCollection();
            responseHandler = new ResponseHandler();

            wp0 = metroTextBoxW0.Text;
            wp1 = metroTextBoxW1.Text;

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

            routeOptimization = new RouteOptimization();

            for (int i = 0; i < alternativeVariantsCount; i++)
            {
                DateTime dateTimeDepartureTemp = dateTimeStart.AddMinutes(i * Convert.ToDouble(numericUpDownInterval.Value));
                altVariantsList.Add(new AlternativeVariant(i,dateTimeDepartureTemp, i, 0, 0));
                altVariantsListRaw.Add(new AlternativeVariant(i,dateTimeDepartureTemp, i, 0, 0));
                altVariantsListRawOptz.Add(new AlternativeVariant(i,dateTimeDepartureTemp, i, 0, 0));

                string dateTimeTempStr = dateTimeDepartureTemp.ToString("yyyy'/'MM'/'dd'%20'H':'mm':'ss");

                var url = $"https://dev.virtualearth.net/REST/V1/Routes/Driving" +
                    $"?wp.0={wp0}" +
                    $"&wp.1={wp1}" +
                    $"&optmz=timeWithTraffic" +
                    $"&timeType=Departure" +
                    $"&dateTime={dateTimeTempStr}" +
                    $"&output=xml" +
                    $"&key={key}";
                Console.WriteLine($"URL: {url}");


                XmlElement xRoot =ResponseHandler.GetResponse(url);
                ResponseHandling.ResponseNodes.Response responseRaw;
                responseRaw = ResponseHandler.ReadResponse(xRoot, dateTimeDepartureTemp);
                //Console.WriteLine(responseRaw);

                //Console.WriteLine($"TravelDurationTraffic #{i}: " +
                //    $"{dateTimeDepartureTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES"))}: " +
                //    $"{responseRaw.resourceSets.resourseSet.resources.route.travelDurationTrafficStr}");

                richTextBox1.Text += $"TravelDurationTraffic #{i}: " +
                    $"{dateTimeDepartureTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES"))}: " +
                    $"{responseRaw.resourceSets.resourseSet.resources.route.travelDurationTrafficStr}" +
                    $"{Environment.NewLine}";
                richTextBox1.Refresh();

                altVariantsListRaw[i].evaluationTravelTime = responseRaw.resourceSets.resourseSet.resources.route.travelDurationTraffic.Ticks / 10000000;
                //altVariantsList[i].evaluationTravelTime = responseRaw.resourceSets.resourseSet.resources.route.travelDurationTraffic.Ticks / 10000000;

                altVariantsListRaw[i].evaluationCoutryChange = responseHandler.GetCountryChangeCount(responseRaw);
                //altVariantsList[i].evaluationCoutryChange = responseHandler.GetCountryChangeCount(responseRaw);

                ResponseHandling.ResponseNodes.Response responseOptz;
                responseOptz = ResponseHandler.ReadResponse(xRoot, dateTimeDepartureTemp);
                responseOptz = routeOptimization.Optimize(responseRaw);
                Console.WriteLine("STOP!");
            }

            

            buttonSearch.Enabled = true;
            buttonSearch.Text = "Search";
            UpdateDataGridView();
            altVarBest = altVariantsList.FindBest();
            metroTextBoxBestDeparture.Text = $"#{altVarBest.id}: {altVarBest.deparuteTime.ToLocalTime().ToString("U", CultureInfo.GetCultureInfo("en-US"))}";
            UpdateMapView(altVarBest, wp0, wp1); 





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


        public void SetUpDataGridView()
        {
            dataGridViewProfitMatrix.ColumnCount = 6;

            //dataGridViewCostMatrix.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            //dataGridViewCostMatrix.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewProfitMatrix.ColumnHeadersDefaultCellStyle.Font =new Font(dataGridViewProfitMatrix.Font, FontStyle.Bold);

            //dataGridViewCostMatrix.Name = "altVarDataGridView";
            //dataGridViewCostMatrix.Location = new System.Drawing.Point(8, 8);
            //dataGridViewCostMatrix.Size = new Size(400, 250);
            //dataGridViewCostMatrix.AutoSizeRowsMode =DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //dataGridViewCostMatrix.ColumnHeadersBorderStyle =DataGridViewHeaderBorderStyle.Single;
            //dataGridViewCostMatrix.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //dataGridViewCostMatrix.GridColor = Color.Black;
            //dataGridViewCostMatrix.RowHeadersVisible = false;

            dataGridViewProfitMatrix.Columns[0].Name = "Id";
            dataGridViewProfitMatrix.Columns[1].Name = "Departure Time";
            dataGridViewProfitMatrix.Columns[2].Name = "F1 Evaluation (Departure Time)";
            dataGridViewProfitMatrix.Columns[3].Name = "F2 Evaluation (Delay Time)";
            dataGridViewProfitMatrix.Columns[4].Name = "F3 Evaluation (Border Crossing)";
            dataGridViewProfitMatrix.Columns[5].Name = "Total Evaluation";

            dataGridViewProfitMatrix.Columns[5].DefaultCellStyle.Font =
                new Font(dataGridViewProfitMatrix.DefaultCellStyle.Font, FontStyle.Regular);

            dataGridViewProfitMatrix.Columns[0].Width = 40;
            dataGridViewProfitMatrix.Columns[1].Width = 130;
            for (int i = 2; i < dataGridViewProfitMatrix.Columns.Count; i++)
            {
                dataGridViewProfitMatrix.Columns[i].Width = 80;
            }


            //dataGridViewCostMatrix.Dock = DockStyle.Fill;

            //
            dataGridViewRawMatrix.ColumnCount = 5;

            //dataGridViewCostMatrix.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            //dataGridViewCostMatrix.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewRawMatrix.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridViewRawMatrix.Font, FontStyle.Bold);

            //dataGridViewCostMatrix.Name = "altVarDataGridView";
            //dataGridViewCostMatrix.Location = new System.Drawing.Point(8, 8);
            //dataGridViewCostMatrix.Size = new Size(400, 250);
            //dataGridViewCostMatrix.AutoSizeRowsMode =DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //dataGridViewCostMatrix.ColumnHeadersBorderStyle =DataGridViewHeaderBorderStyle.Single;
            //dataGridViewCostMatrix.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //dataGridViewCostMatrix.GridColor = Color.Black;
            //dataGridViewCostMatrix.RowHeadersVisible = false;

            dataGridViewRawMatrix.Columns[0].Name = "Id";
            dataGridViewRawMatrix.Columns[1].Name = "Departure Time";
            dataGridViewRawMatrix.Columns[2].Name = "Travel Time";
            dataGridViewRawMatrix.Columns[3].Name = "Delay Time";
            dataGridViewRawMatrix.Columns[4].Name = "Border Crossing";

            dataGridViewRawMatrix.Columns[0].Width = 40;
            dataGridViewRawMatrix.Columns[1].Width = 130;
            for (int i = 2; i < dataGridViewRawMatrix.Columns.Count; i++)
            {
                dataGridViewRawMatrix.Columns[i].Width = 100;
            }


            //dataGridViewCostMatrix.Dock = DockStyle.Fill;

        }

        public void UpdateDataGridView()
        {
            DataGridViewRow rowProfit;
            altVariantsList = altVariantsList.Normalize(Convert.ToInt32( numericUpDownInterval.Value), 0,100);
            altVariantsList = altVariantsList.EvaluateTotal(trackBarF1.Value,trackBarF2.Value,trackBarF3.Value);
            for (int i = 0; i < altVariantsList.Count; i++)
            {
                rowProfit = (DataGridViewRow)dataGridViewProfitMatrix.Rows[i].Clone();
                rowProfit.Cells[0].Value = altVariantsList[i].id;
                rowProfit.Cells[1].Value = altVariantsList[i].deparuteTime.ToString("yyyy'/'MM'/'dd' 'H':'mm':'ss");
                rowProfit.Cells[2].Value = altVariantsList[i].evaluationDeparuteTime;
                rowProfit.Cells[3].Value = altVariantsList[i].evaluationDelayTime;
                rowProfit.Cells[4].Value = altVariantsList[i].evaluationCoutryChange;
                rowProfit.Cells[5].Value = altVariantsList[i].evaluationTotal;
                dataGridViewProfitMatrix.Rows.Add(rowProfit);
            }
            
            DataGridViewRow rowRaw;
            altVariantsListRaw = altVariantsListRaw.GetDelayTime();
            for (int i = 0; i < altVariantsListRaw.Count; i++)
            {
                //rowRaw = new DataGridViewRow();
                rowRaw = (DataGridViewRow)dataGridViewRawMatrix.Rows[i].Clone();
                rowRaw.Cells[0].Value = altVariantsListRaw[i].id;
                rowRaw.Cells[1].Value = altVariantsListRaw[i].deparuteTime.ToString("yyyy'/'MM'/'dd' 'H':'mm':'ss");
                rowRaw.Cells[2].Value = TimeSpan.FromSeconds( altVariantsListRaw[i].evaluationTravelTime).ToString(@"hh\:mm\:ss");
                rowRaw.Cells[3].Value = TimeSpan.FromSeconds( altVariantsListRaw[i].evaluationDelayTime).ToString(@"hh\:mm\:ss");
                //rowRaw.Cells[3].Value = alternativeVariantsListRaw[i].evaluationDelayTime;
                rowRaw.Cells[4].Value = altVariantsListRaw[i].evaluationCoutryChange;
                dataGridViewRawMatrix.Rows.Add(rowRaw);
            }

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

        public void UpdateExtraPoints()
        {
            var extraPoints = 100 - trackBarF1.Value - trackBarF2.Value - trackBarF3.Value;
            metroLabelExtraPoints.Text = extraPoints.ToString();
            if (extraPoints!=0)
            {
                buttonSearch.Enabled = false;
                metroLabelExtraPoints.Text += " (Should be 0)";
            }
            else
            {
                buttonSearch.Enabled = true;
            }
        }

        public void UpdateMapView(AlternativeVariant altVarBest, string wp0, string wp1)
        {
            string dateTimeDeparture = altVarBest.deparuteTime.ToString("yyyy'/'MM'/'dd'%20'H':'mm':'ss");
            int mapWidth;
            int mapHeight;
            if (pictureBoxMapView.Width > 400) mapWidth = pictureBoxMapView.Width;
            else mapWidth = 400;
            if (pictureBoxMapView.Height>400) mapHeight = pictureBoxMapView.Height;
            else mapHeight = 400;
            pictureBoxMapView.Load($"https://dev.virtualearth.net/REST/V1/Imagery/Map/Road/Routes/Driving?" +
                $"wp.0={wp0}" +
                $"&wp.1={wp1}" +
                $"&mapsize={mapWidth},{mapHeight}" +
                $"&optmz=timeWithTraffic" +
                $"&timeType=Departure" +
                $"&dateTime={dateTimeDeparture}" +
                $"&key={key}");

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
            trackBarF1.Value = Convert.ToInt32( numericUpDownF1.Value);
            UpdateExtraPoints();
        }

        private void numericUpDownF2_ValueChanged(object sender, EventArgs e)
        {
            trackBarF2.Value = Convert.ToInt32(numericUpDownF2.Value);
            UpdateExtraPoints();
        }

        private void numericUpDownF3_ValueChanged(object sender, EventArgs e)
        {
            trackBarF3.Value = Convert.ToInt32(numericUpDownF3.Value);
            UpdateExtraPoints();
        }

        private void trackBarF1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownF1.Value = trackBarF1.Value;
            UpdateExtraPoints();
        }

        private void trackBarF2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownF2.Value = trackBarF2.Value;
            UpdateExtraPoints();
        }

        private void trackBarF3_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownF3.Value = trackBarF3.Value;
            UpdateExtraPoints();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Console.WriteLine(tabControlCentral.Size);
            Console.WriteLine(panelCentral.Size);
            Console.WriteLine(pictureBoxMapView.Size);
            tabControlCentral.Size = panelCentral.Size;
            tabControlCentral.Size = new Size(width: panelCentral.Size.Width-9, height: panelCentral.Size.Height - 67);
            pictureBoxMapView.Size = new Size(width: tabPageCentralMapView.Size.Width, height: tabPageCentralMapView.Size.Height);
            metroLabelBestTime.Location = new System.Drawing.Point(7, tabControlCentral.Height+15);
            metroTextBoxBestDeparture.Location = new System.Drawing.Point(7, tabControlCentral.Height+32);
            UpdateMapView(altVarBest, wp0, wp1);

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
