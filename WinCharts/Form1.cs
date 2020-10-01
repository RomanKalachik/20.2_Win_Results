using DataGenerator;
using DevExpress.XtraCharts;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        string log;
        long prevAvailable = 0;
        public Form1()
        {
            InitializeComponent();
            Load += (s, e) =>
            {
                BeginInvoke((Action)(() =>
                {
                    string[] args = Environment.GetCommandLineArgs();
                    if (args.Length > 1) Start(null, null);
                }));
            };
        }


        void bindData(bool allowResample)
        {
            Series series = new Series();
            chartControl1.Series.Add(series);
            series.View = new LineSeriesView();
            series.AllowResample = allowResample;
            series.BindToData(chartSource, "Argument", "Value");
            XYDiagram2D d2d = chartControl1.Diagram as XYDiagram2D;
            if (d2d != null)
            {
                d2d.ZoomingOptions.AxisXMaxZoomPercent = 100000000;
                d2d.ZoomingOptions.AxisYMaxZoomPercent = 100000000;
            }
        }
        public void Start(object sender, EventArgs e)
        {
            MeasureAll(null, null);
            System.Windows.Forms.Application.Exit();
        }
        void ClearChart()
        {
            DoEvents();
            Series series = chartControl1.Series.FirstOrDefault() as Series;
            if (series != null) series.DataSource = null;
            XYDiagram d2d = chartControl1.Diagram as XYDiagram;
            d2d?.ResetZoom();
            chartControl1.Series.Clear();

        }
        void DoEvents()
        {
            System.Windows.Forms.Application.DoEvents();
        }
        void LoadData(int pointsCount, bool allowResample)
        {
            ClearChart();
            chartSource = Generator.Generate(pointsCount);
            LogMemConsumption();
            bindData(allowResample);
            DoEvents();
        }
        void LoadDataEx(int pointsCount, bool allowResample)
        {
            chartSource = Generator.Generate(pointsCount);
            bindData(allowResample);
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
                Stopwatch nonResampledLoading = new Stopwatch();
                resampledLoading.Start();
                LoadDataEx(count, true);
                resampledLoading.Stop();
                ClearChart();
                nonResampledLoading.Start();
                LoadDataEx(count, false);
                nonResampledLoading.Stop();
                result += string.Format("{0}, {1}, {2}{3}", count, resampledLoading.ElapsedMilliseconds, nonResampledLoading.ElapsedMilliseconds, Environment.NewLine);
            }));
            File.WriteAllText("result_loading.txt", result);


        }
        void MeasureMemConsumption(object sender, RoutedEventArgs e)
        {
            ClearChart();
            MeasureMemConsumptionCore(true);
            ClearChart();
            MeasureMemConsumptionCore(false);
        }
        void MeasureMemConsumptionCore(bool allowResampling)
        {
            string result = string.Empty;
            Iterate("MemConsumption", new Action<int>(count =>
            {
                LoadData(count, allowResampling);
                long chartSize = LogMemConsumption();
                result += string.Format("{0}, {1}{2}", count, -chartSize, Environment.NewLine);
            }));
            File.WriteAllText(string.Format("result_memconsumptoin_{0}.txt", allowResampling), result);
        }
        void MeasurePan(object sender, RoutedEventArgs e)
        {
            string result = string.Empty;
            Iterate("Pan", new Action<int>(count =>
            {
                Stopwatch resampled = new Stopwatch();
                Stopwatch nonResampled = new Stopwatch();
                ClearChart();
                LoadDataEx(count, true);
                XYDiagram2D xyd2d = Chart.Diagram as XYDiagram2D;
                xyd2d.EnableAxisXZooming = true;
                xyd2d.EnableAxisYZooming = true;
                xyd2d.ZoomIn(null);
                resampled.Start();
                PerofrmScroll(xyd2d);
                resampled.Stop();
                ClearChart();
                LoadDataEx(count, false);
                xyd2d = Chart.Diagram as XYDiagram2D;
                nonResampled.Start();
                PerofrmScroll(xyd2d);
                nonResampled.Stop();
                result += string.Format("{0}, {1}, {2}{3}", count, resampled.ElapsedMilliseconds, nonResampled.ElapsedMilliseconds, Environment.NewLine);
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
                Stopwatch nonResampled = new Stopwatch();
                LoadDataEx(count, true);
                XYDiagram2D xyd2d = Chart.Diagram as XYDiagram2D;
                resampled.Start();
                PerformZoom(xyd2d);
                resampled.Stop();
                ClearChart();
                LoadDataEx(count, false);
                xyd2d = Chart.Diagram as XYDiagram2D;
                nonResampled.Start();
                PerformZoom(xyd2d);
                nonResampled.Stop();
                result += string.Format("{0}, {1}, {2}{3}", count, resampled.ElapsedMilliseconds, nonResampled.ElapsedMilliseconds, Environment.NewLine);
            }));
            File.WriteAllText("result_zoom.txt", result);
        }
        void PerformZoom(XYDiagram2D xyd2d)
        {
            xyd2d.EnableAxisXZooming = true;
            xyd2d.EnableAxisYZooming = true;
            for (int i = 0; i < 5; i++)
            {
                xyd2d.ZoomIn(null);
                DoEvents();
            }
        }
        void PerofrmScroll(XYDiagram2D xyd2d)
        {
            xyd2d.EnableAxisXScrolling = true;
            xyd2d.EnableAxisYScrolling = true;
            for (int i = 0; i < 25; i++)
            {
                xyd2d.Scroll(HorizontalScrollingDirection.Left, VerticalScrollingDirection.None);
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
        public ChartControl Chart { get { return chartControl1; } }
    }
}