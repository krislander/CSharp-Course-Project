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

        public HumidityGaugeCtrl(HumiditySensor sensor)
        {
            InitializeComponent();
            ToolTip = sensor.Name;
            this.sensor = sensor;

            try
            {
                model = HttpService.SensorList.First(item => item.Tag == sensor.Link); ;

                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(model.MinPollingIntervalInSeconds)
                };
                timer.Tick += Timer_Tick;
                timer.Start();
                Timer_Tick(new object(), new EventArgs());
            }
            catch
            {
                label.Foreground = new SolidColorBrush(Colors.LightGray);
                unit.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        void Timer_Tick(object sender,EventArgs e)
        {
            try
            {
                Needle.Value = HttpService.GetValueAsync(model.SensorId).Result;
                label.Text = Needle.Value.ToString();
                if (Needle.Value > (double)sensor.MaxValue || Needle.Value < (double)sensor.MinValue)
                {
                    Needle.Background = new SolidColorBrush(Colors.IndianRed);
                    unit.Foreground = new SolidColorBrush(Colors.IndianRed);
                    label.Foreground = new SolidColorBrush(Colors.IndianRed);
                }
                else
                {
                    label.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
                    unit.Foreground = (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                    Needle.Background = (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                }
            }
            catch(Exception)
            {
                label.Foreground = new SolidColorBrush(Colors.LightGray);
                Needle.Background = new SolidColorBrush(Colors.LightGray);
                unit.Foreground = new SolidColorBrush(Colors.LightGray);
                return;
            }
        }
    }
}
