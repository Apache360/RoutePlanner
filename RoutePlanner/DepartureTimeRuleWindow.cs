using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoutePlanner
{
    public partial class DepartureTimeRuleWindow : Form
    {
        public DepartureTimeRuleWindow()
        {
            InitializeComponent();
            //metroDateTimeSpecialDaysStart.Value=metroDateTimeSpecialDaysStart.Value.AddDays(1);
            //metroDateTimeSpecialDaysEnd.Value=metroDateTimeSpecialDaysEnd.Value.AddDays(2);
        }

        MainForm mainForm;

        public DepartureTimeRuleWindow(MainForm _mainForm)
        {
            InitializeComponent();
            metroDateTimeSpecialDaysStart.Value = metroDateTimeSpecialDaysStart.Value.AddDays(1);
            metroDateTimeSpecialDaysEnd.Value = metroDateTimeSpecialDaysEnd.Value.AddDays(2);
            metroLabelSpecialDaysState.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            metroLabelSpecialTimeState.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            mainForm = _mainForm;

        }

        private void buttonOk_Click(object sender, EventArgs e)
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void metroRadioButtonDaysOfWeek_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSpecialDays.Enabled = false;
            groupBoxDaysOfWeek.Enabled = true;
        }

        private void metroRadioButtonSpecialDays_CheckedChanged(object sender, EventArgs e)
        {
            groupBoxSpecialDays.Enabled = true;
            groupBoxDaysOfWeek.Enabled = false;
        }

        private void metroRadioButtonWholeDay_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownTimeHrEnd.Enabled = false;
            numericUpDownTimeHrStart.Enabled = false;
            numericUpDownTimeMinEnd.Enabled = false;
            numericUpDownTimeMinStart.Enabled = false;
            numericUpDownTimeSecEnd.Enabled = false;
            numericUpDownTimeSecStart.Enabled = false;
        }

        private void metroRadioButtonSpecialTime_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownTimeHrEnd.Enabled = true;
            numericUpDownTimeHrStart.Enabled = true;
            numericUpDownTimeMinEnd.Enabled = true;
            numericUpDownTimeMinStart.Enabled = true;
            numericUpDownTimeSecEnd.Enabled = true;
            numericUpDownTimeSecStart.Enabled = true;
        }

        private void trackBarCoef_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCoef.Value = trackBarCoef.Value;
        }

        private void numericUpDownCoef_ValueChanged(object sender, EventArgs e)
        {
            trackBarCoef.Value = Convert.ToInt32(numericUpDownCoef.Value);

        }

        private void numericUpDownTimeHrStart_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = isRightTimeInterval();
        }

        private void numericUpDownTimeMinStart_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = isRightTimeInterval();
        }

        private void numericUpDownTimeSecStart_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = isRightTimeInterval();
        }

        private void numericUpDownTimeHrEnd_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = isRightTimeInterval();
        }

        private void numericUpDownTimeMinEnd_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = isRightTimeInterval();
        }

        private void numericUpDownTimeSecEnd_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = isRightTimeInterval();
        }

        private bool isRightTimeInterval()
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

        private void metroDateTimeSpecialDaysStart_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = isRightSpecialDaysInterval();
        }

        private void metroDateTimeSpecialDaysEnd_ValueChanged(object sender, EventArgs e)
        {
            buttonOk.Enabled = isRightSpecialDaysInterval();
        }

        private bool isRightSpecialDaysInterval()
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
