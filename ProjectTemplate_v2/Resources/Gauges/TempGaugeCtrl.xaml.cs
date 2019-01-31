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
            this.ToolTip = sensor.Name;
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
            numValue.Text = bar.Value.ToString();
            if (bar.Value >= (double)sensor.MaxValue)
            {
                stateIndicator.Fill = new SolidColorBrush(Colors.IndianRed);
            }
            else if (bar.Value <= (double)sensor.MinValue)
                stateIndicator.Fill = new SolidColorBrush(Colors.DodgerBlue);
            else
                stateIndicator.Fill = new SolidColorBrush(Colors.Transparent);
        }
    }
}
