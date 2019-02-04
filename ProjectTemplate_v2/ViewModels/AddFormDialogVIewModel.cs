using MaterialDesignThemes.Wpf;
using ProjectTemplate_v2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ProjectTemplate_v2.ViewModels
{
    public class AddFormDialogViewModel : BaseViewModel, IDataErrorInfo
    {
        public List<string> Types { get; private set; } =
               new List<string>() { "Temperature", "Humidity", "Electricity Consumption", "Noise", "Window/Door" };
        public List<SensorModel> Models { get; set; }
        public ICommand SubmitCommand { get; private set; }
        public SnackbarMessageQueue Snackbar { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public SensorModel ToLinkWith { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public bool Open { get; set; }
        public bool Tracking { get; set; } = true;

        private string selectedItem;
        private string unit = " ";
        private Visibility visibility1 = Visibility.Visible;
        private Visibility visibility2 = Visibility.Collapsed;
        private bool isEnabled = false;

        public AddFormDialogViewModel(Sensors sensors, SnackbarMessageQueue snackbar)
        {
            this.sensors = sensors;
            Snackbar = snackbar;
            SubmitCommand = new DelegateCommand(Submit);
            Models = HttpService.SensorList;
        }

        private void Filter()
        {
            IsEnabled = true;
            ICollectionView source = CollectionViewSource.GetDefaultView(Models);
            source.Filter = item => Unit == ((SensorModel)item).MeasureType;
        }


        private void Submit(object param)
        {
            Sensor sensor;

            switch (SelectedItem)
            {
                case "Temperature":
                    sensor = new TemperatureSensor(Name, ToLinkWith.Tag, Description, Convert.ToDouble(Latitude),
                        Convert.ToDouble(Longitude), Tracking,
                        Convert.ToDecimal(MinValue), Convert.ToDecimal(MaxValue));
                    break;
                case "Humidity":
                    sensor = new HumiditySensor(Name, ToLinkWith.Tag, Description, Convert.ToDouble(Latitude),
                        Convert.ToDouble(Longitude), Tracking,
                        Convert.ToDecimal(MinValue), Convert.ToDecimal(MaxValue));
                    break;
                case "Electricity Consumption":
                    sensor = new PowerConsumptionSensor(Name, ToLinkWith.Tag, Description, Convert.ToDouble(Latitude),
                        Convert.ToDouble(Longitude), Tracking,
                        Convert.ToDecimal(MinValue), Convert.ToDecimal(MaxValue));
                    break;
                case "Noise":
                    sensor = new TemperatureSensor(Name, ToLinkWith.Tag, Description, Convert.ToDouble(Latitude),
                        Convert.ToDouble(Longitude), Tracking,
                        Convert.ToDecimal(MinValue), Convert.ToDecimal(MaxValue));
                    break;
                case "Window/Door":
                    sensor = new WindowDoorSensor(Name, ToLinkWith.Tag, Description, Convert.ToDouble(Latitude),
                        Convert.ToDouble(Longitude), Tracking,
                        Open);
                    break;
                default:
                    sensor = null;
                    break;
            }

            //proverka dali ima greshki
            //ako ima da ne pozvolqva submit
            sensors.List.Add(sensor);
            UpdateXml(sensors);
            Snackbar.Enqueue($"{sensor.Name} added");
            DialogHost.CloseDialogCommand.Execute(null, null);
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
                            Unit = "%";
                            Visibility1 = Visibility.Visible;
                            Visibility2 = Visibility.Collapsed;
                            break;
                        case "Electricity Consumption":
                            Unit = "W";
                            Visibility1 = Visibility.Visible;
                            Visibility2 = Visibility.Collapsed;
                            break;
                        case "Noise":
                            Unit = "dB";
                            Visibility1 = Visibility.Visible;
                            Visibility2 = Visibility.Collapsed;
                            break;
                        case "Window/Door":
                            Unit = "(true/false)";
                            Visibility2 = Visibility.Visible;
                            Visibility1 = Visibility.Collapsed;
                            break;
                    }
                    Filter();
                    selectedItem = value;
                    RaisePropertyChanged("SelectedItem");
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

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    RaisePropertyChanged("IsEnabled");
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

        public string Error { get { return null; } }


        //IDataErrorInfo interface implementation
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string Fieldname]
        {
            get
            {
                string result = null;

                switch (Fieldname)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                            result = "Name cannot be empty!";
                        else if (Name.Length < 4)
                            result = "Name's length must be at least 4";
                        break;
                    case "MinValue":
                        if (string.IsNullOrEmpty(MinValue) || string.IsNullOrWhiteSpace(MinValue))
                            result = "MinValue cannot be empty!";
                        else if (Convert.ToDecimal(MinValue) >= Convert.ToDecimal(MaxValue))
                            result = "MinValue is equal to or bigger than MaxValue";
                        break;
                    case "MaxValue":
                        if (string.IsNullOrEmpty(MaxValue) || string.IsNullOrWhiteSpace(MaxValue))
                            result = "MaxValue cannot be empty!";
                        break;
                    case "Latitude":
                        if (string.IsNullOrWhiteSpace(Latitude) || string.IsNullOrEmpty(Latitude))
                            result = "Latitude cannot be empty";
                        else if (Convert.ToDouble(Latitude) < -90 || Convert.ToDouble(Latitude) > 90)
                            result = "Invalid Latitude coordinates";
                        break;
                    case "Longitude":
                        if (string.IsNullOrWhiteSpace(Longitude) || string.IsNullOrEmpty(Longitude))
                            result = "Latitude cannot be empty";
                        else if (Convert.ToDouble(Longitude) < -180 || Convert.ToDouble(Longitude) > 180)
                            result = "Invalid Longtitude coordinates";
                        break;
                    case "Description":
                        if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                            result = "Description field is empty!";
                        break;
                }

                if (ErrorCollection.ContainsKey(Fieldname))
                    ErrorCollection[Fieldname] = result;
                else if (result != null)
                    ErrorCollection.Add(Fieldname, result);

                RaisePropertyChanged("ErrorCollection");
                return result;
            }
        }
    }
}
