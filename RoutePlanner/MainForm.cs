using RoutePlanner.ResponseHandling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;

namespace RoutePlanner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            metroDateTimeDepartureStart.Value = new DateTime(2022, 07, 04, 0, 0, 0);
            numericUpDownDepStartHr.Value = 0;
            numericUpDownDepStartMin.Value = 0;
            numericUpDownDepStartSec.Value = 0;
            metroDateTimeDepartureEnd.Value = new DateTime(2022, 07, 05, 0, 0, 0);
            numericUpDownDepEndHr.Value = 0;
            numericUpDownDepEndMin.Value = 0;
            numericUpDownDepEndSec.Value = 0;
            //metroTextBoxW0.Text = "3835 Luna Pier Rd, Erie, Мічиган 48133, Сполучені Штати";
            //metroTextBoxW1.Text = "19 18th St, Буффало, Нью-Йорк 14213, Сполучені Штати";
            metroTextBoxW0.Text = "39.951916,-75.150118";
            metroTextBoxW1.Text = "40.745702,-73.847184";

            SetUpDataGridView();
            UpdateEstimatedAltVariantsCount();
            SetUpRulesDataGridView();
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

        public Stopwatch stopwatch;
        TimeSpan estimatedTime;

        public List<DepartureTimeRule> departureTimeRules;

        private async void ButtonSearch_Click(object sender, EventArgs e)
        {
            richTextBoxDebug.Clear();
            dataGridViewProfitMatrix.Rows.Clear();
            dataGridViewProfitMatrix.Refresh();
            StartStopwatch();
            estimatedTime = new TimeSpan();

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
                altVariantsListRaw.Add(new AlternativeVariant(i, dateTimeDepartureTemp, i, 0, 0));
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
                //Console.WriteLine($"URL: {url}");

                XmlElement xRoot =ResponseHandler.GetResponse(url);
                ResponseHandling.ResponseNodes.Response responseRaw;
                responseRaw = ResponseHandler.ReadResponse(xRoot, dateTimeDepartureTemp);
                //Console.WriteLine(responseRaw);

                richTextBoxDebug.Text += $"#{i} Travel duration (raw): " +
                    $"{Environment.NewLine}   {dateTimeDepartureTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES"))}: " +
                    $"{Environment.NewLine}   {responseRaw.resourceSets.resourseSet.resources.route.travelDurationTrafficStr}" +
                    $"{Environment.NewLine}";
                richTextBoxDebug.Refresh();
                richTextBoxDebug.SelectionStart = richTextBoxDebug.Text.Length;
                richTextBoxDebug.ScrollToCaret();

                altVariantsListRaw[i].evaluationTravelTime = responseRaw.resourceSets.resourseSet.resources.route.travelDurationTraffic.Ticks / 10000000;
                altVariantsListRaw[i].evaluationCoutryChange = responseHandler.GetCountryChangeCount(responseRaw);

                ResponseHandling.ResponseNodes.Response responseOptz;
                responseOptz = ResponseHandler.ReadResponse(xRoot, dateTimeDepartureTemp);
                responseOptz = routeOptimization.Optimize(responseRaw, this);
                richTextBoxDebug.Text += $"#{i} Travel duration (optimized): " +
                    $"{Environment.NewLine}   {dateTimeDepartureTemp.ToString("G", CultureInfo.GetCultureInfo("es-ES"))}: " +
                    $"{Environment.NewLine}   {responseOptz.resourceSets.resourseSet.resources.route.travelDurationTrafficStr}" +
                    $"{Environment.NewLine}{Environment.NewLine}";
                richTextBoxDebug.Refresh();
                richTextBoxDebug.SelectionStart = richTextBoxDebug.Text.Length;
                richTextBoxDebug.ScrollToCaret();
                Console.WriteLine($"#{i} evaluationTravelTime (optimized): {responseOptz.resourceSets.resourseSet.resources.route.travelDurationTraffic.ToString("c")}");

                altVariantsList[i].evaluationTravelTime = responseOptz.resourceSets.resourseSet.resources.route.travelDurationTraffic.Ticks / 10000000;
                altVariantsList[i].evaluationCoutryChange = responseHandler.GetCountryChangeCount(responseOptz);
                altVariantsListRawOptz[i].evaluationTravelTime = responseOptz.resourceSets.resourseSet.resources.route.travelDurationTraffic.Ticks / 10000000;
                altVariantsListRawOptz[i].evaluationCoutryChange = responseHandler.GetCountryChangeCount(responseOptz);

                estimatedTime = TimeSpan.FromMilliseconds((stopwatch.Elapsed.TotalMilliseconds/(i+1))* (alternativeVariantsCount-i));
                metroLabelTimeLeft.Text = $"Time left: {estimatedTime.ToString(@"hh\:mm\:ss", CultureInfo.GetCultureInfo("en-US"))}";
                metroLabelTimeLeft.Refresh();
            }

            StopStopwatch();
            metroLabelTimeLeft.Text = $"Time left: 00:00:00";
            metroLabelTimeLeft.Refresh();
            UpdateAltVariantsList();
            altVariantsList = DepartureTimeRuleHandler.CalculateDepartureTimeRules(altVariantsList, departureTimeRules);
            UpdateRawMatrixDataGridView();
            altVarBest = altVariantsList.FindBest();
            metroTextBoxBestDeparture.Text = $"#{altVarBest.id}: {altVarBest.deparuteTime.ToLocalTime().ToString("U", CultureInfo.GetCultureInfo("en-US"))}";
            UpdateMapView(altVarBest, wp0, wp1);

            buttonSearch.Enabled = true;
            buttonSearch.Text = "Search";
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
            dataGridViewRawMatrix.ColumnCount = 9;

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
            dataGridViewRawMatrix.Columns[2].Name = "Travel Time (Raw)";
            dataGridViewRawMatrix.Columns[3].Name = "Travel Time (Optimized)";
            dataGridViewRawMatrix.Columns[4].Name = "Delay Time (Raw)";
            dataGridViewRawMatrix.Columns[5].Name = "Delay Time (Optimized)";
            dataGridViewRawMatrix.Columns[6].Name = "Travel Time Difference";
            dataGridViewRawMatrix.Columns[7].Name = "Travel Time Difference, %";
            dataGridViewRawMatrix.Columns[8].Name = "Border Crossing";

            dataGridViewRawMatrix.Columns[0].Width = 40;
            dataGridViewRawMatrix.Columns[1].Width = 130;
            for (int i = 2; i < dataGridViewRawMatrix.Columns.Count; i++)
            {
                dataGridViewRawMatrix.Columns[i].Width = 100;
            }


            //dataGridViewCostMatrix.Dock = DockStyle.Fill;

        }

        public void SetUpRulesDataGridView()
        {
            departureTimeRules = new List<DepartureTimeRule>();
            dataGridViewRules.CellClick += dataGridViewRules_CellClick;
        }

        public void UpdateRulesDataGridView()
        {
            dataGridViewRules.Rows.Clear();
            DataGridViewRow rowRaw;

            foreach (DepartureTimeRule rule in departureTimeRules)
            {
                rowRaw = (DataGridViewRow)dataGridViewRules.Rows[0].Clone();

                rowRaw.Cells[0].Value = rule.id;
                string daysOfWeekStr = "Days of week: ";
                foreach (DayOfWeek dayOfWeek in rule.daysOfWeek)
                {
                    daysOfWeekStr += $"{dayOfWeek.ToString().Substring(0,3)} ";
                }
                rowRaw.Cells[1].Value = $"Rule coefficient: {rule.ruleCoefficient}, {Environment.NewLine}" +
                    $"Day type: {rule.ruleDayType}, {Environment.NewLine}" +
                    $"{daysOfWeekStr}, {Environment.NewLine}" +
                    $"Date interval: {rule.ruleDateTimeInterval.startDateTime.ToString("d")} {rule.ruleDateTimeInterval.endDateTime.ToString("d")}, {Environment.NewLine}" +
                    $"Time type: {rule.ruleTimeType}, {Environment.NewLine}" +
                    $"Time interval: {rule.ruleTimeSpanInterval.startTime} {rule.ruleTimeSpanInterval.endTime}";
                dataGridViewRules.Rows.Add(rowRaw);
            }
        }

        public void UpdateAltVariantsList() 
        {
            altVariantsList = altVariantsList.Normalize(Convert.ToInt32(numericUpDownInterval.Value), 0, 100);
            altVariantsList = altVariantsList.EvaluateTotal(trackBarF1.Value, trackBarF2.Value, trackBarF3.Value);
        }

        public void UpdateRawMatrixDataGridView()
        {
            DataGridViewRow rowProfit;
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
            altVariantsListRawOptz = altVariantsListRawOptz.GetDelayTime();
            altVariantsListRaw = altVariantsListRaw.GetDelayTime();
            for (int i = 0; i < altVariantsListRawOptz.Count; i++)
            {
                double differenceTravelTimePercent = Math.Round(
                    ((altVariantsListRawOptz[i].evaluationTravelTime/altVariantsListRaw[i].evaluationTravelTime)-1)*100, 1);
                string differenceTravelTime = TimeSpan.FromSeconds(altVariantsListRawOptz[i].evaluationTravelTime
                    - altVariantsListRaw[i].evaluationTravelTime).ToString(@"hh\:mm\:ss");
                rowRaw = (DataGridViewRow)dataGridViewRawMatrix.Rows[i].Clone();
                rowRaw.Cells[0].Value = altVariantsListRawOptz[i].id;
                rowRaw.Cells[1].Value = altVariantsListRawOptz[i].deparuteTime.ToString("yyyy'/'MM'/'dd' 'H':'mm':'ss");
                rowRaw.Cells[2].Value = TimeSpan.FromSeconds( altVariantsListRaw[i].evaluationTravelTime).ToString(@"hh\:mm\:ss");
                rowRaw.Cells[3].Value = TimeSpan.FromSeconds( altVariantsListRawOptz[i].evaluationTravelTime).ToString(@"hh\:mm\:ss");
                rowRaw.Cells[4].Value = TimeSpan.FromSeconds( altVariantsListRaw[i].evaluationDelayTime).ToString(@"hh\:mm\:ss");
                rowRaw.Cells[5].Value = TimeSpan.FromSeconds( altVariantsListRawOptz[i].evaluationDelayTime).ToString(@"hh\:mm\:ss");
                rowRaw.Cells[6].Value = differenceTravelTime;
                rowRaw.Cells[7].Value = differenceTravelTimePercent;
                rowRaw.Cells[8].Value = altVariantsListRawOptz[i].evaluationCoutryChange;
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

        public void UpdateMapView(AlternativeVariant altVarBest, string wp0, string wp1)
        {
            int mapWidth;
            int mapHeight;
            if (pictureBoxMapView.Width > 400) mapWidth = pictureBoxMapView.Width;
            else mapWidth = 400;
            if (pictureBoxMapView.Height>400) mapHeight = pictureBoxMapView.Height;
            else mapHeight = 400;
            if (altVarBest != null)
            {
                string dateTimeDeparture = altVarBest.deparuteTime.ToString("yyyy'/'MM'/'dd'%20'H':'mm':'ss");
                pictureBoxMapView.Load($"https://dev.virtualearth.net/REST/V1/Imagery/Map/Road/Routes/Driving?" +
                $"wp.0={wp0}" +
                $"&wp.1={wp1}" +
                $"&mapsize={mapWidth},{mapHeight}" +
                $"&optmz=timeWithTraffic" +
                $"&timeType=Departure" +
                $"&dateTime={dateTimeDeparture}" +
                $"&key={key}");
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
        }

        private void numericUpDownF2_ValueChanged(object sender, EventArgs e)
        {
            trackBarF2.Value = Convert.ToInt32(numericUpDownF2.Value);
        }

        private void numericUpDownF3_ValueChanged(object sender, EventArgs e)
        {
            trackBarF3.Value = Convert.ToInt32(numericUpDownF3.Value);
        }

        private void trackBarF1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownF1.Value = trackBarF1.Value;
        }

        private void trackBarF2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownF2.Value = trackBarF2.Value;
        }

        private void trackBarF3_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownF3.Value = trackBarF3.Value;
        }

        private void dataGridViewRules_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Remove
            if (departureTimeRules.Count==0)
            {
                return;
            }
            if (e.ColumnIndex == dataGridViewRules.Columns[2].Index)
            {
                //Do something with your button. e.RowIndex
                Console.WriteLine($"rule removed {e.RowIndex}");
                departureTimeRules.RemoveAt(e.RowIndex);
                UpdateRulesDataGridView();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            tabControlCentral.Size = panelCentral.Size;
            tabControlCentral.Size = new Size(width: panelCentral.Size.Width-9, height: panelCentral.Size.Height - 67);
            pictureBoxMapView.Size = new Size(width: tabPageCentralMapView.Size.Width, height: tabPageCentralMapView.Size.Height);
            dataGridViewProfitMatrix.Size = new Size(width: tabPageCentralProfitMatrix.Size.Width, height: tabPageCentralProfitMatrix.Size.Height);
            dataGridViewRawMatrix.Size = new Size(width: tabPageCentralRawMatrix.Size.Width, height: tabPageCentralRawMatrix.Size.Height);
            //dataGridViewProfitMatrix.Size = new Size(width: tabPageCentralMapView.Size.Width, height: tabPageCentralMapView.Size.Height);
            //dataGridViewRawMatrix.Size = new Size(width: tabPageCentralMapView.Size.Width, height: tabPageCentralMapView.Size.Height);
            metroLabelBestTime.Location = new System.Drawing.Point(7, tabControlCentral.Height+15);
            metroTextBoxBestDeparture.Location = new System.Drawing.Point(7, tabControlCentral.Height+32);
            UpdateMapView(altVarBest, wp0, wp1);

        }

        private void buttonAddRule_Click(object sender, EventArgs e)
        {
            DepartureTimeRuleWindow departureTimeRuleWindow = new DepartureTimeRuleWindow(this);
            departureTimeRuleWindow.ShowDialog();
        }

        public void AddRule(DepartureTimeRule departureTimeRule)
        {
            departureTimeRules.Add(departureTimeRule);
            UpdateRulesDataGridView();
        }

        private void StartStopwatch()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private void StopStopwatch()
        {
            stopwatch.Stop();
        }

        public void UpdateElapsedTime()
        {

            metroLabelElapsedTime.Text = $"Elapsed time: {stopwatch.Elapsed.ToString(@"hh\:mm\:ss", CultureInfo.GetCultureInfo("en-US"))}";
            metroLabelElapsedTime.Refresh();
        }

        public void UpdateEstimatedTime()
        {

        }
    }
}
