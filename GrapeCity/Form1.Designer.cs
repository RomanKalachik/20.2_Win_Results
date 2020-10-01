namespace WinCharts
{
    partial class Form1
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
            C1.Chart.ElementSize elementSize1 = new C1.Chart.ElementSize();
            C1.Win.Chart.Series series1 = new C1.Win.Chart.Series();
            this.button2 = new System.Windows.Forms.Button();
            this.flexChart1 = new C1.Win.Chart.FlexChart();
            ((System.ComponentModel.ISupportInitialize)(this.flexChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // flexChart1
            // 
            this.flexChart1.AnimationLoad.Direction = C1.Chart.AnimationDirection.Y;
            this.flexChart1.AnimationLoad.Duration = 400;
            this.flexChart1.AnimationLoad.Easing = C1.Chart.Easing.Linear;
            this.flexChart1.AnimationLoad.Type = C1.Chart.AnimationType.All;
            this.flexChart1.AnimationSettings = C1.Chart.AnimationSettings.None;
            this.flexChart1.AnimationUpdate.Duration = 400;
            this.flexChart1.AnimationUpdate.Easing = C1.Chart.Easing.Linear;
            this.flexChart1.AnimationUpdate.Type = C1.Chart.AnimationType.All;
            this.flexChart1.AxisX.Chart = this.flexChart1;
            this.flexChart1.AxisX.DataSource = null;
            this.flexChart1.AxisX.GroupProvider = null;
            this.flexChart1.AxisX.GroupSeparator = C1.Chart.AxisGroupSeparator.None;
            this.flexChart1.AxisX.GroupTitleAlignment = C1.Chart.AxisLabelAlignment.Center;
            this.flexChart1.AxisX.GroupVisibilityLevel = 0;
            this.flexChart1.AxisX.LabelMax = false;
            this.flexChart1.AxisX.LabelMin = false;
            this.flexChart1.AxisX.PlotAreaName = null;
            this.flexChart1.AxisX.Position = C1.Chart.Position.Bottom;
            this.flexChart1.AxisX.TimeUnit = C1.Chart.TimeUnits.Day;
            this.flexChart1.AxisY.AxisLine = false;
            this.flexChart1.AxisY.Chart = this.flexChart1;
            this.flexChart1.AxisY.DataSource = null;
            this.flexChart1.AxisY.GroupProvider = null;
            this.flexChart1.AxisY.GroupSeparator = C1.Chart.AxisGroupSeparator.None;
            this.flexChart1.AxisY.GroupTitleAlignment = C1.Chart.AxisLabelAlignment.Center;
            this.flexChart1.AxisY.GroupVisibilityLevel = 0;
            this.flexChart1.AxisY.LabelMax = false;
            this.flexChart1.AxisY.LabelMin = false;
            this.flexChart1.AxisY.MajorGrid = true;
            this.flexChart1.AxisY.MajorTickMarks = C1.Chart.TickMark.None;
            this.flexChart1.AxisY.PlotAreaName = null;
            this.flexChart1.AxisY.Position = C1.Chart.Position.Left;
            this.flexChart1.AxisY.TimeUnit = C1.Chart.TimeUnits.Day;
            this.flexChart1.DataLabel.Angle = 0;
            this.flexChart1.DataLabel.Border = false;
            this.flexChart1.DataLabel.ConnectingLine = false;
            this.flexChart1.DataLabel.Content = null;
            this.flexChart1.DataLabel.ContentOptions = C1.Chart.ContentOptions.WordWrap;
            this.flexChart1.DataLabel.MaxAutoLabels = 100;
            this.flexChart1.DataLabel.MaxLines = 0;
            this.flexChart1.DataLabel.MaxWidth = 0;
            this.flexChart1.DataLabel.Offset = 0;
            this.flexChart1.DataLabel.Overlapping = C1.Chart.LabelOverlapping.Hide;
            this.flexChart1.DataLabel.OverlappingOptions = C1.Chart.LabelOverlappingOptions.OutsidePlotArea;
            this.flexChart1.DataLabel.Position = C1.Chart.LabelPosition.None;
            this.flexChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flexChart1.Legend.ItemMaxWidth = 0;
            this.flexChart1.Legend.Orientation = C1.Chart.Orientation.Auto;
            this.flexChart1.Legend.Position = C1.Chart.Position.Right;
            this.flexChart1.Legend.Reversed = false;
            this.flexChart1.Legend.ScrollBars = C1.Chart.LegendScrollBars.None;
            this.flexChart1.Legend.TextWrapping = C1.Chart.TextWrapping.None;
            this.flexChart1.Legend.Title = null;
            this.flexChart1.Location = new System.Drawing.Point(0, 0);
            this.flexChart1.Margin = new System.Windows.Forms.Padding(10);
            this.flexChart1.Name = "flexChart1";
            elementSize1.SizeType = C1.Chart.ElementSizeType.Percentage;
            elementSize1.Value = 70D;
            this.flexChart1.Options.ClusterSize = elementSize1;
            this.flexChart1.PlotMargin = new System.Windows.Forms.Padding(0);
            this.flexChart1.SelectedSeries = null;
            this.flexChart1.SelectionStyle.StrokeColor = System.Drawing.Color.Red;
            series1.DataLabel = null;
            series1.Name = "Series 1";
            series1.StackingGroup = -1;
            series1.Style.StrokeWidth = 2F;
            series1.Tooltip = null;
            this.flexChart1.Series.Add(series1);
            this.flexChart1.Size = new System.Drawing.Size(800, 464);
            this.flexChart1.TabIndex = 2;
            this.flexChart1.Text = "flexChart1";
            // 
            // 
            // 
            this.flexChart1.ToolTip.Content = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 464);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.flexChart1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.flexChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private C1.Win.Chart.FlexChart flexChart1;
    }
}

