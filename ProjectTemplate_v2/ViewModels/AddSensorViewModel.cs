using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ProjectTemplate_v2.ViewModels
{
    public class AddSensorViewModel : BaseViewModel
    {
        private string selectedItem;
        private string unit;
        private bool opened;
        private bool tracking = true;
        private Visibility visibility1;
        private Visibility visibility2;

        public List<string> Types { get; private set; } =
            new List<string>() { "Temperature", "Humidity", "Electricity Consumption", "Noise", "Window/Door" };
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public ICommand SubmitCommand { get; private set; }
        public SnackbarMessageQueue SensorAdded { get; set; }


        public AddSensorViewModel(Sensors sensors)
        {
            Visibility1 = Visibility.Visible;
            Visibility2 = Visibility.Collapsed;
            this.sensors = sensors;
            SensorAdded = new SnackbarMessageQueue();
            SubmitCommand = new DelegateCommand(Submit); //TODO: CanExecuteCommand
        }

        private void Submit(object param)
        {
            Sensor sensor;

            switch (SelectedItem)
            {
                case "Temperature":
                    sensor = new TemperatureSensor(Name, Url, Description, Latitude, Longitude, Tracking, MinValue, MaxValue);
                    break;
                case "Humidity":
                    sensor = new HumiditySensor(Name, Url, Description, Latitude, Longitude, Tracking, MinValue, MaxValue);
                    break;
                case "Electricity Consumption":
                    sensor = new PowerConsumptionSensor(Name, Url, Description, Latitude, Longitude, Tracking, MinValue, MaxValue);
                    break;
                case "Noise":
                    sensor = new TemperatureSensor(Name, Url, Description, Latitude, Longitude, Tracking, MinValue, MaxValue);
                    break;
                case "Window/Door":
                    sensor = new WindowDoorSensor(Name, Url, Description, Latitude, Longitude, Tracking, Opened);
                    break;
                default:
                    sensor = null;
                    break;
            }

            sensors.List.Add(sensor);
            UpdateXml(sensors);
            SensorAdded.Enqueue("Sensor added successfully");
        }

        public bool Opened
        {
            get
            {
                return opened;
            }
            set
            {
                if (opened != value)
                {
                    opened = value;
                    RaisePropertyChanged("Opened");
                }
            }
        }

        public bool Tracking
        {
            get { return tracking; }
            set
            {
                if (tracking != value)
                {
                    tracking = value;
                    RaisePropertyChanged("Tracking");
                }
            }
        }

        public Visibility Visibility2
        {
            get { return visibility2; }
            set
            {
                if (visibility2 != value)
                {
                    visibility2 = value;
                    RaisePropertyChanged("Visibility2");
                }
            }
        }

        public Visibility Visibility1
        {
            get { return visibility1; }
            set
            {
                if (visibility1 != value)
                {
                    visibility1 = value;
                    RaisePropertyChanged("Visibility1");
                }
            }
        }

        public string Unit
        {
            get { return unit; }
            set
            {
                if (unit != value)
                {
                    unit = value;
                    RaisePropertyChanged("Unit");
                }
            }
        }

        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    switch (value)
                    {
                        case "Temperature":
                            Unit = "°C";
                            Visibility1 = Visibility.Visible;
                            Visibility2 = Visibility.Collapsed;
                            break;
                        case "Humidity":
                            Unit = "% ";
                            Visibility1 = Visibility.Visible;
                            Visibility2 = Visibility.Collapsed;
                            break;
                        case "Electricity Consumption":
                            Unit = "W ";
                            Visibility1 = Visibility.Visible;
                            Visibility2 = Visibility.Collapsed;
                            break;
                        case "Noise":
                            Unit = "dB";
                            Visibility1 = Visibility.Visible;
                            Visibility2 = Visibility.Collapsed;
                            break;
                        case "Window/Door":
                            Visibility2 = Visibility.Visible;
                            Visibility1 = Visibility.Collapsed;
                            break;
                    }
                    selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
                }
            }
        }

    }
}
