using ProjectTemplate_v2.Models;
using System;
using System.Linq;
using System.Windows;
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
        private SensorModel model;

        public TempGaugeCtrl(TemperatureSensor sensor)
        {
            InitializeComponent();
            ToolTip = sensor.Name;
            this.sensor = sensor;

            try
            {
                model = HttpService.SensorList.First(item => item.Tag == sensor.Link);

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
                unit.Foreground = new SolidColorBrush(Colors.LightGray);
                stateIndicator.Fill = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                bar.Value = Convert.ToDouble(HttpService.GetValueAsync(model.SensorId).Result.Value);
                numValue.Text = bar.Value.ToString();

                bar.Background= (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                numValue.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
                unit.Foreground = (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                if (bar.Value >= (double)sensor.MaxValue)
                {
                    stateIndicator.Fill = new SolidColorBrush(Colors.IndianRed);
                }
                else if (bar.Value <= (double)sensor.MinValue)
                    stateIndicator.Fill = new SolidColorBrush(Colors.DodgerBlue);
                else
                {
                    stateIndicator.Fill = new SolidColorBrush(Colors.Transparent);
                }
            }
            catch(Exception)
            {
                unit.Foreground = new SolidColorBrush(Colors.LightGray);
                bar.Background = new SolidColorBrush(Colors.LightGray);
                numValue.Foreground = new SolidColorBrush(Colors.LightGray);
                stateIndicator.Fill = new SolidColorBrush(Colors.LightGray);
            }

        }
    }
}
