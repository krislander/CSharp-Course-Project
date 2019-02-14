using ProjectTemplate_v2.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ProjectTemplate_v2.Resources.Gauges
{
    /// <summary>
    /// Interaction logic for DoorWindowGaugeCtrl.xaml
    /// </summary>
    public partial class DoorWindowGaugeCtrl : UserControl
    {
        private WindowDoorSensor sensor;
        private SensorModel model;
        DispatcherTimer timer;
        private bool state = false;

        public DoorWindowGaugeCtrl(WindowDoorSensor sensor)
        {
            InitializeComponent();
            ToolTip = sensor.Name;
            lbl_Name.Content = sensor.Name;
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
                Foreground = new SolidColorBrush(Colors.LightGray);
                rectangle.Fill = new SolidColorBrush(Colors.LightGray);
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
                bool value = Convert.ToBoolean(HttpService.GetValueAsync(model.SensorId).Result.Value);

                if (value == sensor.Opened)
                {
                    Foreground= (Brush)Application.Current.Resources["PrimaryHueMidBrush"];
                    rectangle.Fill= (Brush)Application.Current.Resources["PrimaryHueLightBrush"];
                }
                else
                {
                    var converter = new BrushConverter();
                    var brush = (Brush)converter.ConvertFromString("#B00020");
                    Foreground = brush;
                    rectangle.Fill = brush;
                }

                ChangeState(value);
            }
            catch
            {
                Foreground = new SolidColorBrush(Colors.LightGray);
                rectangle.Fill = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void ChangeState(bool value)
        {
            ThicknessAnimation animation = new ThicknessAnimation
            {
                Duration = TimeSpan.FromSeconds(.5)
            };

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTargetName(animation, "rectangle");
            Storyboard.SetTargetProperty(animation, new PropertyPath(Rectangle.MarginProperty));
            storyboard.Children.Add(animation);

            if (value && !state)
            {
                animation.From = new Thickness(0, 0, 0, 0);
                animation.To = new Thickness(-190, 30, 0, 0);
                state = true;
                storyboard.Begin(this);
            }
            else if(!value&& state)
            {
                animation.From = new Thickness(-160, 30, 0, 0);
                animation.To = new Thickness(0, 0, 0, 0);
                state = false;
                storyboard.Begin(this);
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
