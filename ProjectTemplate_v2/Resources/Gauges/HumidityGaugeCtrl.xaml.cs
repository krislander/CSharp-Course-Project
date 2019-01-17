using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ProjectTemplate_v2.Models.Gauges
{
    /// <summary>
    /// Interaction logic for HumidityGaugeCtrl.xaml
    /// </summary>
    public partial class HumidityGaugeCtrl : UserControl
    {
        private static Random rand;
        private HumiditySensor sensor;
        private string sensorId;

        public HumidityGaugeCtrl(HumiditySensor sensor,SensorModel model)
        {
            InitializeComponent();
            minMarker.Value=(double)sensor.MinValue;
            maxMarker.Value = (double)sensor.MaxValue;
            humidityGauge.ToolTip = sensor.Name;
            sensorId = model.SensorId;
            this.sensor = sensor;

            rand = new Random();
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
            //Needle.Value = rand.Next(0, 101);
            Needle.Value = HttpService.GetValueAsync(sensorId).Result;
            if (Needle.Value >(double) sensor.MaxValue || Needle.Value < (double)sensor.MinValue)
            {
                humidityGauge.Background = new SolidColorBrush(Colors.IndianRed);
            }
            else
                humidityGauge.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
