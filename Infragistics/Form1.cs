using DataGenerator;
using Infragistics.Win.DataVisualization;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Windows;
using System.Windows.Forms;
namespace WinCharts
{
    public partial class Form1 : Form
    {
        ObservableCollection<DataItem> chartSource;
        long prevAvailable = 0;
        public Form1()
        {
            InitializeComponent();
        }
        void bindData()
        {
            ScatterLineSeries series = new ScatterLineSeries();
            Chart.Series.Add(series);
            var xAxis = new NumericXAxis();
            var yAxis = new NumericYAxis();
            series.XAxis = xAxis;
            series.YAxis = yAxis;
            series.XMemberPath = "Argument";
            series.YMemberPath = "Value";
            Chart.Axes.Add(xAxis);
            Chart.Axes.Add(yAxis);
            series.DataSource = chartSource;
        }
        void button2_Click(object sender, EventArgs e)
        {
            MeasureAll(null, null);
        }
        void ClearChart()
        {
            DoEvents();
            Series series = Chart.Series.FirstOrDefault();
            if (series != null) {
                Chart.Series.Clear();
                Chart.Axes.Clear();
                this.Chart.WindowScaleHorizontal = 1;
                this.Chart.WindowPositionHorizontal = 0;
                series.DataSource = null; }
        }
        void DoEvents()
        {
            System.Windows.Forms.Application.DoEvents();
        }
        void LoadData(int pointsCount)
        {
            ClearChart();
            chartSource = Generator.Generate(pointsCount);
            LogMemConsumption();
            bindData();
            DoEvents();
        }
        void LoadDataEx(int pointsCount)
        {
            chartSource = Generator.Generate(pointsCount);
            bindData();
            DoEvents();
        }
        long LogMemConsumption()
        {
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForFullGCComplete();
            long available = GC.GetTotalMemory(true);

            long delta = prevAvailable - available;
            prevAvailable = available;
            return delta;
        }
        void MeasureAll(object sender, RoutedEventArgs e)
        {
            MeasureLoading(null, null);
            Text = "Loading - done";
            MeasureMemConsumption(null, null);
            Text = "Memory - done";
            MeasureZoom(null, null);
            Text = "Zooming - done";
            MeasurePan(null, null);
            Text = "All - done";
        }
        void MeasureLoading(object sender, RoutedEventArgs e)
        {
            string result = string.Empty;
            Iterate("Loading", new Action<int>(count =>
            {
                ClearChart();
                Stopwatch resampledLoading = new Stopwatch();
                resampledLoading.Start();
                LoadDataEx(count);
                resampledLoading.Stop();

                result += string.Format("{0}, {1}, {2}{3}", count, resampledLoading.ElapsedMilliseconds, resampledLoading.ElapsedMilliseconds, Environment.NewLine);
            }));
            File.WriteAllText("result_loading.txt", result);


        }
        void MeasureMemConsumption(object sender, RoutedEventArgs e)
        {
            ClearChart();
            MeasureMemConsumptionCore();
        }
        void MeasureMemConsumptionCore()
        {
            string result = string.Empty;
            Iterate("MemConsumption", new Action<int>(count =>
            {
                LoadData(count);
                long chartSize = LogMemConsumption();
                result += string.Format("{0}, {1}{2}", count, -chartSize, Environment.NewLine);
            }));
            File.WriteAllText(string.Format("result_memconsumptoin_{0}.txt", false), result);
        }
        void MeasurePan(object sender, RoutedEventArgs e)
        {
            string result = string.Empty;
            Iterate("Pan", new Action<int>(count =>
            {
                Stopwatch nonResampled = new Stopwatch();
                ClearChart();
                LoadDataEx(count);
                nonResampled.Start();
                PerofrmScroll();
                nonResampled.Stop();
                result += string.Format("{0}, {1}, {2}{3}", count, nonResampled.ElapsedMilliseconds, nonResampled.ElapsedMilliseconds, Environment.NewLine);
            }));
            File.WriteAllText("result_scroll.txt", result);

        }
        void MeasureZoom(object sender, RoutedEventArgs e)
        {
            string result = string.Empty;
            Iterate("Zoom", new Action<int>(count =>
            {
                ClearChart();
                Stopwatch resampled = new Stopwatch();
                LoadDataEx(count);
                resampled.Start();
                PerformZoom();
                resampled.Stop();
                result += string.Format("{0}, {1}, {2}{3}", count, resampled.ElapsedMilliseconds, resampled.ElapsedMilliseconds, Environment.NewLine);
            }));
            File.WriteAllText("result_zoom.txt", result);
        }
        void PerformZoom()
        {
            Chart.IsHorizontalZoomEnabled = true;
            Chart.IsVerticalZoomEnabled = true;
            for (int i = 0; i < 5; i++)
            {
                this.Chart.WindowScaleHorizontal -= 0.05;
                DoEvents();
            }
        }
        void PerofrmScroll()
        {
            
            for (int i = 0; i < 25; i+=1)
            {
                Chart.WindowPositionHorizontal -= 0.05;
                DoEvents();
            }
        }
        protected void Iterate(string caption, Action<int> action)
        {
            int[] counts = new int[] { 1000, 10000, 20000, 50000, 100000, 300000, 500000, 750000, 1000000, 2000000 };
            int counter = 0;
            foreach (int count in counts)
            {
                Text = caption + " " + counter++.ToString() + " of " + counts.Length;
                action(count);
            }
        }
        public UltraDataChart Chart { get { return ultraDataChart1; } }
    }
}