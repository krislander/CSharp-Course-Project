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

            Validate();
            //check for errors
            if (ErrorCollection.Count == 0)
            {
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

                sensors.List.Add(sensor);
                UpdateXml(sensors);
                Snackbar.Enqueue($"{sensor.Name} added");
                DialogHost.CloseDialogCommand.Execute(null, null);
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

        public string Error
        {
            get
            {
                if (ErrorCollection.Count > 0)
                {
                    return "Errors found.";
                }
                return null;
            }
        }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        void Validate()
        {
            ErrorCollection.Clear();
            //Name
            if (string.IsNullOrEmpty(Name))
                ErrorCollection.Add("Name", "Name is empty");
            else if (Name.Length < 4)
                ErrorCollection.Add("Name", "Name's length must be at least 4");
            //Min and Max Value
            if (string.IsNullOrEmpty(MinValue) || string.IsNullOrWhiteSpace(MinValue))
                ErrorCollection.Add("MinValue", "MinValue is empty");
            else if (Convert.ToDecimal(MinValue) >= Convert.ToDecimal(MaxValue))
                ErrorCollection.Add("MinValue", "MinValue >= MaxValue");

            if (string.IsNullOrEmpty(MaxValue) || string.IsNullOrWhiteSpace(MaxValue))
                ErrorCollection.Add("MaxValue", "MaxValue is empty");

            //Latitude and Longtitude
            if (string.IsNullOrWhiteSpace(Latitude) || string.IsNullOrEmpty(Latitude))
                ErrorCollection.Add("Latitude", "Latitude is empty");
            else if (Convert.ToDouble(Latitude) < -90 || Convert.ToDouble(Latitude) > 90)
                ErrorCollection.Add("Latitude", "Invalid Latitude coordinates");

            if (string.IsNullOrWhiteSpace(Longitude) || string.IsNullOrEmpty(Longitude))
                ErrorCollection.Add("Longitude", "Longtitude is empty");
            else if (Convert.ToDouble(Longitude) < -180 || Convert.ToDouble(Longitude) > 180)
                ErrorCollection.Add("Longitude", "Invalid Longtitude coordinates");

            //Description
            if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
                ErrorCollection.Add("Description", "Description is empty");

            RaisePropertyChanged(null);
        }

        public string this[string columnName]
        {
            get
            {
                if (ErrorCollection.ContainsKey(columnName))
                {
                    return ErrorCollection[columnName];
                }
                return null;
            }
        }
    }
}
