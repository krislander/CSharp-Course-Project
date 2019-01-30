using ProjectTemplate_v2.Models;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ProjectTemplate_v2.Resources.Gauges
{
    /// <summary>
    /// Interaction logic for TempGaugeCtrl.xaml
    /// </summary>
    public partial class TempGaugeCtrl : UserControl
    {
        private TemperatureSensor sensor;
        private string sensorId;

        public TempGaugeCtrl(TemperatureSensor sensor,SensorModel model)
        {
            InitializeComponent();
            chillRange.Max = (double)sensor.MinValue;
            hotRange.Min = (double)sensor.MaxValue;
            midRange.Max = hotRange.Min;
            midRange.Min =chillRange.Max;
            tempGauge.ToolTip = sensor.Name;
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

         void Timer_Tick(object sender, EventArgs e)
        {
            bar.Value = HttpService.GetValueAsync(sensorId).Result;
            if (bar.Value > (double)sensor.MaxValue || bar.Value < (double)sensor.MinValue)
            {
                gradient.Color = (Color)ColorConverter.ConvertFromString("IndianRed");
            }
            else
                gradient.Color= (Color)ColorConverter.ConvertFromString("#FF009F98"); 
        }
    }
}
