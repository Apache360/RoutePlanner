
namespace RoutePlanner
{
    partial class DepartureTimeRuleWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxDaysOfWeek = new System.Windows.Forms.GroupBox();
            this.groupBoxSpecialDays = new System.Windows.Forms.GroupBox();
            this.metroDateTimeSpecialDaysStart = new MetroFramework.Controls.MetroDateTime();
            this.metroDateTimeSpecialDaysEnd = new MetroFramework.Controls.MetroDateTime();
            this.numericUpDownCoef = new System.Windows.Forms.NumericUpDown();
            this.trackBarCoef = new System.Windows.Forms.TrackBar();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroRadioButtonDaysOfWeek = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButtonSpecialDays = new MetroFramework.Controls.MetroRadioButton();
            this.metroRadioButtonWholeDay = new MetroFramework.Controls.MetroRadioButton();
            this.groupBoxTime = new System.Windows.Forms.GroupBox();
            this.metroRadioButtonSpecialTime = new MetroFramework.Controls.MetroRadioButton();
            this.metroLabelSpecialTimeState = new MetroFramework.Controls.MetroLabel();
            this.metroCheckBoxMonday = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBoxTuesday = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBoxWednesday = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBoxThursday = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBoxFriday = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBoxSaturday = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBoxSunday = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.numericUpDownTimeHrStart = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTimeMinStart = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTimeSecStart = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTimeHrEnd = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTimeMinEnd = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTimeSecEnd = new System.Windows.Forms.NumericUpDown();
            this.metroLabelSpecialDaysState = new MetroFramework.Controls.MetroLabel();
            this.groupBoxDaysOfWeek.SuspendLayout();
            this.groupBoxSpecialDays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCoef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCoef)).BeginInit();
            this.groupBoxTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeHrStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMinStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeSecStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeHrEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMinEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeSecEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(125, 545);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(87, 30);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(219, 545);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 30);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // groupBoxDaysOfWeek
            // 
            this.groupBoxDaysOfWeek.Controls.Add(this.metroCheckBoxSunday);
            this.groupBoxDaysOfWeek.Controls.Add(this.metroCheckBoxSaturday);
            this.groupBoxDaysOfWeek.Controls.Add(this.metroCheckBoxFriday);
            this.groupBoxDaysOfWeek.Controls.Add(this.metroCheckBoxThursday);
            this.groupBoxDaysOfWeek.Controls.Add(this.metroCheckBoxWednesday);
            this.groupBoxDaysOfWeek.Controls.Add(this.metroCheckBoxTuesday);
            this.groupBoxDaysOfWeek.Controls.Add(this.metroCheckBoxMonday);
            this.groupBoxDaysOfWeek.Location = new System.Drawing.Point(16, 51);
            this.groupBoxDaysOfWeek.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxDaysOfWeek.Name = "groupBoxDaysOfWeek";
            this.groupBoxDaysOfWeek.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxDaysOfWeek.Size = new System.Drawing.Size(174, 219);
            this.groupBoxDaysOfWeek.TabIndex = 2;
            this.groupBoxDaysOfWeek.TabStop = false;
            // 
            // groupBoxSpecialDays
            // 
            this.groupBoxSpecialDays.Controls.Add(this.metroLabelSpecialDaysState);
            this.groupBoxSpecialDays.Controls.Add(this.metroLabel3);
            this.groupBoxSpecialDays.Controls.Add(this.metroLabel2);
            this.groupBoxSpecialDays.Controls.Add(this.metroDateTimeSpecialDaysEnd);
            this.groupBoxSpecialDays.Controls.Add(this.metroDateTimeSpecialDaysStart);
            this.groupBoxSpecialDays.Enabled = false;
            this.groupBoxSpecialDays.Location = new System.Drawing.Point(196, 51);
            this.groupBoxSpecialDays.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxSpecialDays.Name = "groupBoxSpecialDays";
            this.groupBoxSpecialDays.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxSpecialDays.Size = new System.Drawing.Size(209, 219);
            this.groupBoxSpecialDays.TabIndex = 3;
            this.groupBoxSpecialDays.TabStop = false;
            // 
            // metroDateTimeSpecialDaysStart
            // 
            this.metroDateTimeSpecialDaysStart.Location = new System.Drawing.Point(6, 44);
            this.metroDateTimeSpecialDaysStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroDateTimeSpecialDaysStart.MinimumSize = new System.Drawing.Size(0, 29);
            this.metroDateTimeSpecialDaysStart.Name = "metroDateTimeSpecialDaysStart";
            this.metroDateTimeSpecialDaysStart.Size = new System.Drawing.Size(192, 29);
            this.metroDateTimeSpecialDaysStart.TabIndex = 0;
            this.metroDateTimeSpecialDaysStart.ValueChanged += new System.EventHandler(this.MetroDateTimeSpecialDaysStart_ValueChanged);
            // 
            // metroDateTimeSpecialDaysEnd
            // 
            this.metroDateTimeSpecialDaysEnd.Location = new System.Drawing.Point(6, 107);
            this.metroDateTimeSpecialDaysEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroDateTimeSpecialDaysEnd.MinimumSize = new System.Drawing.Size(0, 29);
            this.metroDateTimeSpecialDaysEnd.Name = "metroDateTimeSpecialDaysEnd";
            this.metroDateTimeSpecialDaysEnd.Size = new System.Drawing.Size(192, 29);
            this.metroDateTimeSpecialDaysEnd.TabIndex = 1;
            this.metroDateTimeSpecialDaysEnd.ValueChanged += new System.EventHandler(this.MetroDateTimeSpecialDaysEnd_ValueChanged);
            // 
            // numericUpDownCoef
            // 
            this.numericUpDownCoef.Location = new System.Drawing.Point(74, 502);
            this.numericUpDownCoef.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownCoef.Name = "numericUpDownCoef";
            this.numericUpDownCoef.Size = new System.Drawing.Size(65, 25);
            this.numericUpDownCoef.TabIndex = 6;
            this.numericUpDownCoef.ValueChanged += new System.EventHandler(this.NumericUpDownCoef_ValueChanged);
            // 
            // trackBarCoef
            // 
            this.trackBarCoef.Location = new System.Drawing.Point(145, 479);
            this.trackBarCoef.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackBarCoef.Maximum = 100;
            this.trackBarCoef.Name = "trackBarCoef";
            this.trackBarCoef.Size = new System.Drawing.Size(260, 45);
            this.trackBarCoef.TabIndex = 7;
            this.trackBarCoef.ValueChanged += new System.EventHandler(this.TrackBarCoef_ValueChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(12, 479);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(127, 19);
            this.metroLabel1.TabIndex = 8;
            this.metroLabel1.Text = "Select coefficient, %:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(8, 21);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(40, 19);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "Start:";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(6, 80);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(31, 19);
            this.metroLabel3.TabIndex = 3;
            this.metroLabel3.Text = "End";
            // 
            // metroRadioButtonDaysOfWeek
            // 
            this.metroRadioButtonDaysOfWeek.AutoSize = true;
            this.metroRadioButtonDaysOfWeek.Checked = true;
            this.metroRadioButtonDaysOfWeek.Location = new System.Drawing.Point(12, 28);
            this.metroRadioButtonDaysOfWeek.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroRadioButtonDaysOfWeek.Name = "metroRadioButtonDaysOfWeek";
            this.metroRadioButtonDaysOfWeek.Size = new System.Drawing.Size(117, 15);
            this.metroRadioButtonDaysOfWeek.TabIndex = 9;
            this.metroRadioButtonDaysOfWeek.TabStop = true;
            this.metroRadioButtonDaysOfWeek.Text = "Every day of week";
            this.metroRadioButtonDaysOfWeek.UseSelectable = true;
            this.metroRadioButtonDaysOfWeek.CheckedChanged += new System.EventHandler(this.MetroRadioButtonDaysOfWeek_CheckedChanged);
            // 
            // metroRadioButtonSpecialDays
            // 
            this.metroRadioButtonSpecialDays.AutoSize = true;
            this.metroRadioButtonSpecialDays.Location = new System.Drawing.Point(196, 28);
            this.metroRadioButtonSpecialDays.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroRadioButtonSpecialDays.Name = "metroRadioButtonSpecialDays";
            this.metroRadioButtonSpecialDays.Size = new System.Drawing.Size(87, 15);
            this.metroRadioButtonSpecialDays.TabIndex = 10;
            this.metroRadioButtonSpecialDays.Text = "Special days";
            this.metroRadioButtonSpecialDays.UseSelectable = true;
            this.metroRadioButtonSpecialDays.CheckedChanged += new System.EventHandler(this.MetroRadioButtonSpecialDays_CheckedChanged);
            // 
            // metroRadioButtonWholeDay
            // 
            this.metroRadioButtonWholeDay.AutoSize = true;
            this.metroRadioButtonWholeDay.Checked = true;
            this.metroRadioButtonWholeDay.Location = new System.Drawing.Point(7, 25);
            this.metroRadioButtonWholeDay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroRadioButtonWholeDay.Name = "metroRadioButtonWholeDay";
            this.metroRadioButtonWholeDay.Size = new System.Drawing.Size(105, 15);
            this.metroRadioButtonWholeDay.TabIndex = 11;
            this.metroRadioButtonWholeDay.TabStop = true;
            this.metroRadioButtonWholeDay.Text = "Whole day 0-24";
            this.metroRadioButtonWholeDay.UseSelectable = true;
            this.metroRadioButtonWholeDay.CheckedChanged += new System.EventHandler(this.MetroRadioButtonWholeDay_CheckedChanged);
            // 
            // groupBoxTime
            // 
            this.groupBoxTime.Controls.Add(this.numericUpDownTimeSecEnd);
            this.groupBoxTime.Controls.Add(this.numericUpDownTimeMinEnd);
            this.groupBoxTime.Controls.Add(this.numericUpDownTimeHrEnd);
            this.groupBoxTime.Controls.Add(this.numericUpDownTimeSecStart);
            this.groupBoxTime.Controls.Add(this.numericUpDownTimeMinStart);
            this.groupBoxTime.Controls.Add(this.numericUpDownTimeHrStart);
            this.groupBoxTime.Controls.Add(this.metroLabelSpecialTimeState);
            this.groupBoxTime.Controls.Add(this.metroRadioButtonSpecialTime);
            this.groupBoxTime.Controls.Add(this.metroRadioButtonWholeDay);
            this.groupBoxTime.Location = new System.Drawing.Point(16, 297);
            this.groupBoxTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxTime.Size = new System.Drawing.Size(389, 174);
            this.groupBoxTime.TabIndex = 12;
            this.groupBoxTime.TabStop = false;
            // 
            // metroRadioButtonSpecialTime
            // 
            this.metroRadioButtonSpecialTime.AutoSize = true;
            this.metroRadioButtonSpecialTime.Location = new System.Drawing.Point(7, 52);
            this.metroRadioButtonSpecialTime.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroRadioButtonSpecialTime.Name = "metroRadioButtonSpecialTime";
            this.metroRadioButtonSpecialTime.Size = new System.Drawing.Size(87, 15);
            this.metroRadioButtonSpecialTime.TabIndex = 12;
            this.metroRadioButtonSpecialTime.Text = "Special time";
            this.metroRadioButtonSpecialTime.UseSelectable = true;
            this.metroRadioButtonSpecialTime.CheckedChanged += new System.EventHandler(this.MetroRadioButtonSpecialTime_CheckedChanged);
            // 
            // metroLabelSpecialTimeState
            // 
            this.metroLabelSpecialTimeState.AutoSize = true;
            this.metroLabelSpecialTimeState.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabelSpecialTimeState.Location = new System.Drawing.Point(34, 138);
            this.metroLabelSpecialTimeState.Name = "metroLabelSpecialTimeState";
            this.metroLabelSpecialTimeState.Size = new System.Drawing.Size(0, 0);
            this.metroLabelSpecialTimeState.TabIndex = 13;
            // 
            // metroCheckBoxMonday
            // 
            this.metroCheckBoxMonday.AutoSize = true;
            this.metroCheckBoxMonday.Location = new System.Drawing.Point(7, 25);
            this.metroCheckBoxMonday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBoxMonday.Name = "metroCheckBoxMonday";
            this.metroCheckBoxMonday.Size = new System.Drawing.Size(67, 15);
            this.metroCheckBoxMonday.TabIndex = 0;
            this.metroCheckBoxMonday.Text = "Monday";
            this.metroCheckBoxMonday.UseSelectable = true;
            // 
            // metroCheckBoxTuesday
            // 
            this.metroCheckBoxTuesday.AutoSize = true;
            this.metroCheckBoxTuesday.Location = new System.Drawing.Point(7, 52);
            this.metroCheckBoxTuesday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBoxTuesday.Name = "metroCheckBoxTuesday";
            this.metroCheckBoxTuesday.Size = new System.Drawing.Size(66, 15);
            this.metroCheckBoxTuesday.TabIndex = 1;
            this.metroCheckBoxTuesday.Text = "Tuesday";
            this.metroCheckBoxTuesday.UseSelectable = true;
            // 
            // metroCheckBoxWednesday
            // 
            this.metroCheckBoxWednesday.AutoSize = true;
            this.metroCheckBoxWednesday.Location = new System.Drawing.Point(7, 80);
            this.metroCheckBoxWednesday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBoxWednesday.Name = "metroCheckBoxWednesday";
            this.metroCheckBoxWednesday.Size = new System.Drawing.Size(84, 15);
            this.metroCheckBoxWednesday.TabIndex = 2;
            this.metroCheckBoxWednesday.Text = "Wednesday";
            this.metroCheckBoxWednesday.UseSelectable = true;
            // 
            // metroCheckBoxThursday
            // 
            this.metroCheckBoxThursday.AutoSize = true;
            this.metroCheckBoxThursday.Location = new System.Drawing.Point(7, 107);
            this.metroCheckBoxThursday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBoxThursday.Name = "metroCheckBoxThursday";
            this.metroCheckBoxThursday.Size = new System.Drawing.Size(71, 15);
            this.metroCheckBoxThursday.TabIndex = 3;
            this.metroCheckBoxThursday.Text = "Thursday";
            this.metroCheckBoxThursday.UseSelectable = true;
            // 
            // metroCheckBoxFriday
            // 
            this.metroCheckBoxFriday.AutoSize = true;
            this.metroCheckBoxFriday.Location = new System.Drawing.Point(7, 135);
            this.metroCheckBoxFriday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBoxFriday.Name = "metroCheckBoxFriday";
            this.metroCheckBoxFriday.Size = new System.Drawing.Size(55, 15);
            this.metroCheckBoxFriday.TabIndex = 4;
            this.metroCheckBoxFriday.Text = "Friday";
            this.metroCheckBoxFriday.UseSelectable = true;
            // 
            // metroCheckBoxSaturday
            // 
            this.metroCheckBoxSaturday.AutoSize = true;
            this.metroCheckBoxSaturday.Location = new System.Drawing.Point(7, 163);
            this.metroCheckBoxSaturday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBoxSaturday.Name = "metroCheckBoxSaturday";
            this.metroCheckBoxSaturday.Size = new System.Drawing.Size(69, 15);
            this.metroCheckBoxSaturday.TabIndex = 5;
            this.metroCheckBoxSaturday.Text = "Saturday";
            this.metroCheckBoxSaturday.UseSelectable = true;
            // 
            // metroCheckBoxSunday
            // 
            this.metroCheckBoxSunday.AutoSize = true;
            this.metroCheckBoxSunday.Location = new System.Drawing.Point(7, 191);
            this.metroCheckBoxSunday.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroCheckBoxSunday.Name = "metroCheckBoxSunday";
            this.metroCheckBoxSunday.Size = new System.Drawing.Size(62, 15);
            this.metroCheckBoxSunday.TabIndex = 6;
            this.metroCheckBoxSunday.Text = "Sunday";
            this.metroCheckBoxSunday.UseSelectable = true;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(12, 5);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(76, 19);
            this.metroLabel6.TabIndex = 13;
            this.metroLabel6.Text = "Select days:";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(12, 274);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(76, 19);
            this.metroLabel7.TabIndex = 14;
            this.metroLabel7.Text = "Select time:";
            // 
            // numericUpDownTimeHrStart
            // 
            this.numericUpDownTimeHrStart.Enabled = false;
            this.numericUpDownTimeHrStart.Location = new System.Drawing.Point(34, 75);
            this.numericUpDownTimeHrStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownTimeHrStart.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownTimeHrStart.Name = "numericUpDownTimeHrStart";
            this.numericUpDownTimeHrStart.Size = new System.Drawing.Size(71, 25);
            this.numericUpDownTimeHrStart.TabIndex = 15;
            this.numericUpDownTimeHrStart.ValueChanged += new System.EventHandler(this.NumericUpDownTimeHrStart_ValueChanged);
            // 
            // numericUpDownTimeMinStart
            // 
            this.numericUpDownTimeMinStart.Enabled = false;
            this.numericUpDownTimeMinStart.Location = new System.Drawing.Point(112, 75);
            this.numericUpDownTimeMinStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownTimeMinStart.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownTimeMinStart.Name = "numericUpDownTimeMinStart";
            this.numericUpDownTimeMinStart.Size = new System.Drawing.Size(71, 25);
            this.numericUpDownTimeMinStart.TabIndex = 16;
            this.numericUpDownTimeMinStart.ValueChanged += new System.EventHandler(this.NumericUpDownTimeMinStart_ValueChanged);
            // 
            // numericUpDownTimeSecStart
            // 
            this.numericUpDownTimeSecStart.Enabled = false;
            this.numericUpDownTimeSecStart.Location = new System.Drawing.Point(190, 75);
            this.numericUpDownTimeSecStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownTimeSecStart.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownTimeSecStart.Name = "numericUpDownTimeSecStart";
            this.numericUpDownTimeSecStart.Size = new System.Drawing.Size(71, 25);
            this.numericUpDownTimeSecStart.TabIndex = 17;
            this.numericUpDownTimeSecStart.ValueChanged += new System.EventHandler(this.NumericUpDownTimeSecStart_ValueChanged);
            // 
            // numericUpDownTimeHrEnd
            // 
            this.numericUpDownTimeHrEnd.Enabled = false;
            this.numericUpDownTimeHrEnd.Location = new System.Drawing.Point(34, 109);
            this.numericUpDownTimeHrEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownTimeHrEnd.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownTimeHrEnd.Name = "numericUpDownTimeHrEnd";
            this.numericUpDownTimeHrEnd.Size = new System.Drawing.Size(71, 25);
            this.numericUpDownTimeHrEnd.TabIndex = 18;
            this.numericUpDownTimeHrEnd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTimeHrEnd.ValueChanged += new System.EventHandler(this.NumericUpDownTimeHrEnd_ValueChanged);
            // 
            // numericUpDownTimeMinEnd
            // 
            this.numericUpDownTimeMinEnd.Enabled = false;
            this.numericUpDownTimeMinEnd.Location = new System.Drawing.Point(112, 109);
            this.numericUpDownTimeMinEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownTimeMinEnd.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownTimeMinEnd.Name = "numericUpDownTimeMinEnd";
            this.numericUpDownTimeMinEnd.Size = new System.Drawing.Size(71, 25);
            this.numericUpDownTimeMinEnd.TabIndex = 19;
            this.numericUpDownTimeMinEnd.ValueChanged += new System.EventHandler(this.NumericUpDownTimeMinEnd_ValueChanged);
            // 
            // numericUpDownTimeSecEnd
            // 
            this.numericUpDownTimeSecEnd.Enabled = false;
            this.numericUpDownTimeSecEnd.Location = new System.Drawing.Point(190, 109);
            this.numericUpDownTimeSecEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownTimeSecEnd.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.numericUpDownTimeSecEnd.Name = "numericUpDownTimeSecEnd";
            this.numericUpDownTimeSecEnd.Size = new System.Drawing.Size(71, 25);
            this.numericUpDownTimeSecEnd.TabIndex = 20;
            this.numericUpDownTimeSecEnd.ValueChanged += new System.EventHandler(this.NumericUpDownTimeSecEnd_ValueChanged);
            // 
            // metroLabelSpecialDaysState
            // 
            this.metroLabelSpecialDaysState.AutoSize = true;
            this.metroLabelSpecialDaysState.Location = new System.Drawing.Point(6, 144);
            this.metroLabelSpecialDaysState.Name = "metroLabelSpecialDaysState";
            this.metroLabelSpecialDaysState.Size = new System.Drawing.Size(0, 0);
            this.metroLabelSpecialDaysState.TabIndex = 4;
            // 
            // DepartureTimeRuleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(417, 582);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.groupBoxTime);
            this.Controls.Add(this.metroRadioButtonSpecialDays);
            this.Controls.Add(this.metroRadioButtonDaysOfWeek);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.trackBarCoef);
            this.Controls.Add(this.numericUpDownCoef);
            this.Controls.Add(this.groupBoxSpecialDays);
            this.Controls.Add(this.groupBoxDaysOfWeek);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DepartureTimeRuleWindow";
            this.Text = "Add Departure Time Rule";
            this.groupBoxDaysOfWeek.ResumeLayout(false);
            this.groupBoxDaysOfWeek.PerformLayout();
            this.groupBoxSpecialDays.ResumeLayout(false);
            this.groupBoxSpecialDays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCoef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCoef)).EndInit();
            this.groupBoxTime.ResumeLayout(false);
            this.groupBoxTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeHrStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMinStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeSecStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeHrEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeMinEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeSecEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxDaysOfWeek;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxSunday;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxSaturday;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxFriday;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxThursday;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxWednesday;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxTuesday;
        private MetroFramework.Controls.MetroCheckBox metroCheckBoxMonday;
        private System.Windows.Forms.GroupBox groupBoxSpecialDays;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroDateTime metroDateTimeSpecialDaysEnd;
        private MetroFramework.Controls.MetroDateTime metroDateTimeSpecialDaysStart;
        private System.Windows.Forms.NumericUpDown numericUpDownCoef;
        private System.Windows.Forms.TrackBar trackBarCoef;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroRadioButton metroRadioButtonDaysOfWeek;
        private MetroFramework.Controls.MetroRadioButton metroRadioButtonSpecialDays;
        private MetroFramework.Controls.MetroRadioButton metroRadioButtonWholeDay;
        private System.Windows.Forms.GroupBox groupBoxTime;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeSecEnd;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeMinEnd;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeHrEnd;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeSecStart;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeMinStart;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeHrStart;
        private MetroFramework.Controls.MetroLabel metroLabelSpecialTimeState;
        private MetroFramework.Controls.MetroRadioButton metroRadioButtonSpecialTime;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabelSpecialDaysState;
    }
}