using RoutePlanner.DepartureTimeRulesHandling;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RoutePlanner
{
    public partial class DepartureTimeRuleWindow : Form
    {
        public DepartureTimeRuleWindow()
        {
            InitializeComponent();
        }

        readonly MainForm mainForm;

        public DepartureTimeRuleWindow(MainForm _mainForm)
        {
            InitializeComponent();
            metroDateTimeSpecialDaysStart.Value = metroDateTimeSpecialDaysStart.Value.AddDays(1);
            metroDateTimeSpecialDaysEnd.Value = metroDateTimeSpecialDaysEnd.Value.AddDays(2);
            metroLabelSpecialDaysState.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            metroLabelSpecialTimeState.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            mainForm = _mainForm;

        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            RuleDayType ruleDayType= RuleDayType.DaysOfWeek;
            if (metroRadioButtonDaysOfWeek.Checked) ruleDayType = RuleDayType.DaysOfWeek;
            if (metroRadioButtonSpecialDays.Checked) ruleDayType = RuleDayType.SpecialDays;

            List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();
            if (metroCheckBoxMonday.Checked) daysOfWeek.Add(DayOfWeek.Monday);
            if (metroCheckBoxTuesday.Checked) daysOfWeek.Add(DayOfWeek.Tuesday);
            if (metroCheckBoxWednesday.Checked) daysOfWeek.Add(DayOfWeek.Wednesday);
            if (metroCheckBoxThursday.Checked) daysOfWeek.Add(DayOfWeek.Thursday);
            if (metroCheckBoxFriday.Checked) daysOfWeek.Add(DayOfWeek.Friday);
            if (metroCheckBoxSaturday.Checked) daysOfWeek.Add(DayOfWeek.Saturday);
            if (metroCheckBoxSunday.Checked) daysOfWeek.Add(DayOfWeek.Sunday);

            RuleDateTimeInterval ruleDateTimeInterval = new RuleDateTimeInterval(
                new DateTime(metroDateTimeSpecialDaysStart.Value.Ticks),
                new DateTime(metroDateTimeSpecialDaysEnd.Value.Ticks));

            RuleTimeType ruleTimeType = RuleTimeType.WholeDay;
            if (metroRadioButtonWholeDay.Checked) ruleTimeType = RuleTimeType.WholeDay;
            if (metroRadioButtonSpecialTime.Checked) ruleTimeType = RuleTimeType.SpecialTime;


            TimeSpan timeSpanStart = new TimeSpan(
                Convert.ToInt32(numericUpDownTimeHrStart.Value),
                Convert.ToInt32(numericUpDownTimeMinStart.Value),
                Convert.ToInt32(numericUpDownTimeSecStart.Value));
            TimeSpan timeSpanEnd = new TimeSpan(
                Convert.ToInt32(numericUpDownTimeHrEnd.Value),
                Convert.ToInt32(numericUpDownTimeMinEnd.Value),
                Convert.ToInt32(numericUpDownTimeSecEnd.Value));
            RuleTimeSpanInterval ruleTimeSpanInterval = new RuleTimeSpanInterval(timeSpanStart, timeSpanEnd);
            
            DepartureTimeRule departureTimeRule =  new DepartureTimeRule(
                DepartureTimeRule.GetAvaliableId(mainForm.departureTimeRules),
                Convert.ToDouble(numericUpDownCoef.Value/100),
                ruleDayType,
                daysOfWeek,
                ruleDateTimeInterval,
                ruleTimeType,
                ruleTimeSpanInterval
                );
            mainForm.AddRule(departureTimeRule);
            Console.WriteLine(departureTimeRule);
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MetroRadioButtonDaysOfWeek_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSpecialDays.Enabled = false;
            groupBoxDaysOfWeek.Enabled = true;
        }

        private void MetroRadioButtonSpecialDays_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSpecialDays.Enabled = true;
            groupBoxDaysOfWeek.Enabled = false;
        }

        private void MetroRadioButtonWholeDay_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownTimeHrEnd.Enabled = false;
            numericUpDownTimeHrStart.Enabled = false;
            numericUpDownTimeMinEnd.Enabled = false;
            numericUpDownTimeMinStart.Enabled = false;
            numericUpDownTimeSecEnd.Enabled = false;
            numericUpDownTimeSecStart.Enabled = false;
        }

        private void MetroRadioButtonSpecialTime_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownTimeHrEnd.Enabled = true;
            numericUpDownTimeHrStart.Enabled = true;
            numericUpDownTimeMinEnd.Enabled = true;
            numericUpDownTimeMinStart.Enabled = true;
            numericUpDownTimeSecEnd.Enabled = true;
            numericUpDownTimeSecStart.Enabled = true;
        }

        private void TrackBarCoef_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCoef.Value = trackBarCoef.Value;
        }

        private void NumericUpDownCoef_ValueChanged(object sender, EventArgs e)
        {
            trackBarCoef.Value = Convert.ToInt32(numericUpDownCoef.Value);

        }

        private void NumericUpDownTimeHrStart_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = IsRightTimeInterval();
        }

        private void NumericUpDownTimeMinStart_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = IsRightTimeInterval();
        }

        private void NumericUpDownTimeSecStart_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = IsRightTimeInterval();
        }

        private void NumericUpDownTimeHrEnd_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = IsRightTimeInterval();
        }

        private void NumericUpDownTimeMinEnd_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = IsRightTimeInterval();
        }

        private void NumericUpDownTimeSecEnd_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = IsRightTimeInterval();
        }

        private bool IsRightTimeInterval()
        {
            TimeSpan timeSpanStart = new TimeSpan(
                Convert.ToInt32( numericUpDownTimeHrStart.Value),
                Convert.ToInt32(numericUpDownTimeMinStart.Value),
                Convert.ToInt32(numericUpDownTimeSecStart.Value));
            TimeSpan timeSpanEnd = new TimeSpan(
                Convert.ToInt32( numericUpDownTimeHrEnd.Value),
                Convert.ToInt32(numericUpDownTimeMinEnd.Value),
                Convert.ToInt32(numericUpDownTimeSecEnd.Value));
            bool result = timeSpanEnd <= timeSpanStart;
            if (result)
            {
                metroLabelSpecialTimeState.Text = "Wrong time interval";
            }
            else
            {
                metroLabelSpecialTimeState.Text = "";
            }
            return !result;
        }

        private void MetroDateTimeSpecialDaysStart_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = IsRightSpecialDaysInterval();
        }

        private void MetroDateTimeSpecialDaysEnd_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = IsRightSpecialDaysInterval();
        }

        private bool IsRightSpecialDaysInterval()
        {
            bool result = metroDateTimeSpecialDaysEnd.Value >= metroDateTimeSpecialDaysStart.Value;
            if (result)
            {
                metroLabelSpecialDaysState.Text = "";
            }
            else
            {
                metroLabelSpecialDaysState.Text = "Wrong date interval";
            }
            return result;
        }
    }
}
