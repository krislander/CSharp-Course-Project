using System;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignColors;
using System.Windows.Threading;
using System.Windows;

namespace ProjectTemplate_v2.Models.Gauges
{
    /// <summary>
    /// Interaction logic for HumidityGaugeCtrl.xaml
    /// </summary>
    public partial class HumidityGaugeCtrl : UserControl
    {
        private HumiditySensor sensor;
        private string sensorId;

        public HumidityGaugeCtrl(HumiditySensor sensor,SensorModel model)
        {
            InitializeComponent();
            humidityGauge.ToolTip = sensor.Name;
            sensorId = model.SensorId;
            this.sensor = sensor;

            DispatcherTimer timer = new DispatcherTimer
            {               
                Interval = TimeSpan.FromSeconds(model.MinPollingIntervalInSeconds)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            Timer_Tick(new object(), new EventArgs());
        }

        void Timer_Tick(object sender,EventArgs e)
        {
            Needle.Value = HttpService.GetValueAsync(sensorId).Result;
            label.Text = string.Format($"{Needle.Value}");
            if (Needle.Value > (double)sensor.MaxValue || Needle.Value < (double)sensor.MinValue)
            {
               // humidityGauge.Background = new SolidColorBrush(Colors.IndianRed);
                Needle.Background = new SolidColorBrush(Colors.IndianRed);
            }
            else
            {
                humidityGauge.Background = new SolidColorBrush(Colors.Transparent);
                Needle.Background= (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
            }
        }
    }
}
