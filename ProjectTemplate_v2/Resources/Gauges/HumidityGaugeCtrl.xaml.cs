using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Linq;

namespace ProjectTemplate_v2.Models.Gauges
{
    /// <summary>
    /// Interaction logic for HumidityGaugeCtrl.xaml
    /// </summary>
    public partial class HumidityGaugeCtrl : UserControl
    {
        private HumiditySensor sensor;
        private SensorModel model;
        private DispatcherTimer timer;

        public HumidityGaugeCtrl(HumiditySensor sensor)
        {
            InitializeComponent();
            ToolTip = sensor.Name;
            this.sensor = sensor;

            try
            {
                model = HttpService.SensorList.First(item => item.Tag == sensor.Link); ;

                timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(model.MinPollingIntervalInSeconds)
                };
                timer.Tick += Timer_Tick;
                timer.Start();
                FirstTick();
            }
            catch
            {
                label.Foreground = new SolidColorBrush(Colors.LightGray);
                unit.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void FirstTick()
        {
            //Dispatcher.BeginInvoke(DispatcherPriority.Loaded,(Action)(() => Timer_Tick(new object(), new EventArgs())));
            Timer_Tick(new object(), new EventArgs());
        }

        private void Timer_Tick(object sender,EventArgs e)
        {
            try
            {
                Needle.Value = Convert.ToDouble(HttpService.GetValueAsync(model.SensorId).Result.Value);
                label.Text = Needle.Value.ToString();
                if (Needle.Value > (double)sensor.MaxValue || Needle.Value < (double)sensor.MinValue)
                {
                    var converter = new BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#B00020");
                    Needle.Background = brush;
                    unit.Foreground = brush;
                    label.Foreground = brush;
                }
                else
                {
                    label.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
                    unit.Foreground = (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                    Needle.Background = (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                }
            }
            catch
            {
                label.Foreground = new SolidColorBrush(Colors.LightGray);
                Needle.Background = new SolidColorBrush(Colors.LightGray);
                unit.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
        }
    }
}
