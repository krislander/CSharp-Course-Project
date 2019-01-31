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
    /// Interaction logic for PowerGaugeCtrl.xaml
    /// </summary>
    public partial class PowerGaugeCtrl : UserControl
    {
        private PowerConsumptionSensor sensor;
        private SensorModel model;

        public PowerGaugeCtrl(PowerConsumptionSensor sensor)
        {
            InitializeComponent();
            ToolTip = sensor.Name;
            this.sensor = sensor;
            scale.Min = (double)sensor.MinValue;
            scale.Max = (double)sensor.MaxValue;

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
                needle.Background = new SolidColorBrush(Colors.LightGray);
                numIndicator.Foreground = new SolidColorBrush(Colors.LightGray);
                label.Foreground = new SolidColorBrush(Colors.LightGray);
                bar.Background = new SolidColorBrush(Colors.LightGray);

            }

        }

        private void Timer_Tick(object sender,EventArgs e)
        {
            try
            {
                bar.Value = HttpService.GetValueAsync(model.SensorId).Result;
                numIndicator.Text = bar.Value.ToString();

                if (bar.Value < (double)sensor.MinValue)
                {
                    bar.Value = (double)sensor.MinValue;

                    needle.Background = new SolidColorBrush(Colors.IndianRed);
                    numIndicator.Foreground = new SolidColorBrush(Colors.IndianRed);
                    label.Foreground = new SolidColorBrush(Colors.IndianRed);
                    bar.Background = new SolidColorBrush(Colors.IndianRed);
                }
                else if (bar.Value > (double)sensor.MaxValue)
                {
                    bar.Value = (double)sensor.MaxValue;

                    needle.Background = new SolidColorBrush(Colors.IndianRed);
                    numIndicator.Foreground = new SolidColorBrush(Colors.IndianRed);
                    label.Foreground = new SolidColorBrush(Colors.IndianRed);
                    bar.Background = new SolidColorBrush(Colors.IndianRed);
                }
                else
                {

                    needle.Background = (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                    numIndicator.Foreground = (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
                    label.Foreground = (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                    bar.Background = (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                }

                needle.Value = bar.Value;
            }
            catch (Exception)
            {
                needle.Background = new SolidColorBrush(Colors.LightGray);
                numIndicator.Foreground = new SolidColorBrush(Colors.LightGray);
                label.Foreground = new SolidColorBrush(Colors.LightGray);
                bar.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
    }
}
