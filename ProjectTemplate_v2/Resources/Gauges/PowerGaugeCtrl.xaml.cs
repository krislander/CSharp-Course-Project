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
        private DispatcherTimer timer;

        public PowerGaugeCtrl(PowerConsumptionSensor sensor)
        {
            InitializeComponent();
            ToolTip = sensor.Name;
            lbl_Name.Content = sensor.Name;
            this.sensor = sensor;
            scale.Min = (double)sensor.MinValue;
            scale.Max = (double)sensor.MaxValue;

            try
            {
                model = HttpService.SensorList.First(item => item.Tag == sensor.Link);

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
                needle.Background = new SolidColorBrush(Colors.LightGray);
                numIndicator.Foreground = new SolidColorBrush(Colors.LightGray);
                label.Foreground = new SolidColorBrush(Colors.LightGray);
                bar.Background = new SolidColorBrush(Colors.LightGray);

            }

        }

        private void FirstTick()
        {
            //Dispatcher.BeginInvoke(DispatcherPriority.Loaded, (Action)(() => Timer_Tick(new object(), new EventArgs())));
            Timer_Tick(new object(), new EventArgs());
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                bar.Value = Convert.ToDouble(HttpService.GetValueAsync(model.SensorId).Result.Value);
                numIndicator.Text = bar.Value.ToString();

                if (bar.Value < (double)sensor.MinValue)
                {
                    bar.Value = (double)sensor.MinValue;
                    
                    var converter = new BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#B00020");
                    needle.Background = brush;
                    numIndicator.Foreground = brush;
                    bar.Background = brush;
                    label.Foreground = brush;
                }
                else if (bar.Value > (double)sensor.MaxValue)
                {
                    bar.Value = (double)sensor.MaxValue;
                    
                    var converter = new BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#B00020");
                    needle.Background = brush;
                    numIndicator.Foreground = brush;
                    bar.Background = brush;
                    label.Foreground = brush;
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
            catch
            {
                needle.Background = new SolidColorBrush(Colors.LightGray);
                numIndicator.Foreground = new SolidColorBrush(Colors.LightGray);
                label.Foreground = new SolidColorBrush(Colors.LightGray);
                bar.Background = new SolidColorBrush(Colors.LightGray);
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
