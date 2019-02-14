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
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        
        public bool Open { get; set; }
        public bool Tracking { get; set; } = true;

        private string name;
        private string latitude;
        private string longitude;
        private string description;
        private string minValue;
        private string maxValue;
        private string selectedItem;
        private SensorModel toLinkWith;
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

            CheckForBlanks();
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
                        sensor = new NoiseSensor(Name, ToLinkWith.Tag, Description, Convert.ToDouble(Latitude),
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
                            MinValue = "";
                            MaxValue = "";
                            ErrorCollection.Remove("MinValue");
                            ErrorCollection.Remove("MaxValue");
                            Visibility2 = Visibility.Visible;
                            Visibility1 = Visibility.Collapsed;
                            break;
                    }

                    Filter();
                    ErrorCollection.Remove("SelectedItem");
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
                    ErrorCollection.Remove("ToLinkWith");
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

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    ErrorCollection.Remove("Name");

                    if (string.IsNullOrEmpty(value))
                        ErrorCollection.Add("Name", "Field is empty");
                    else if (value.Length < 3)
                        ErrorCollection.Add("Name", "Name must be at least 3 characters");
                    else if (sensors.List.FirstOrDefault(item => item.Name == value) != null)
                        ErrorCollection.Add("Name", "This name is already in use");

                    name = value;
                    RaisePropertyChanged("Name");

                }
            }
        }

        public string Latitude
        {
            get { return latitude; }
            set
            {
                if (latitude != value)
                {
                    ErrorCollection.Remove("Latitude");

                    if (string.IsNullOrEmpty(value))
                        ErrorCollection.Add("Latitude", "Field is empty");
                    else if (double.TryParse(value, out var d))
                    {
                        if (d > 90 || d < -90)
                            ErrorCollection.Add("Latitude", "Invalid coordinate");
                    }
                    else
                        ErrorCollection.Add("Latitude", "NaN");

                    latitude = value;
                    RaisePropertyChanged("Latitude");
                }
            }
        }

        public string Longitude
        {
            get { return longitude; }
            set
            {
                if (longitude != value)
                {
                    ErrorCollection.Remove("Longitude");

                    if (string.IsNullOrEmpty(value))
                        ErrorCollection.Add("Longitude", "Field is empty");
                    else if (double.TryParse(value, out var d))
                    {
                        if (d > 180 || d < -180)
                            ErrorCollection.Add("Longitude", "Invalid coordinate");
                    }
                    else
                        ErrorCollection.Add("Longitude", "NaN");

                    longitude = value;
                    RaisePropertyChanged("Longitude");
                }
            }
        }

        public string MinValue
        {
            get { return minValue; }
            set
            {
                if (minValue != value)
                {
                    ErrorCollection.Remove("MinValue");

                    if (string.IsNullOrEmpty(value))
                        ErrorCollection.Add("MinValue", "Field is empty");
                    else if (decimal.TryParse(value, out var d))
                    {
                        if (decimal.TryParse(MaxValue, out var d1))
                        {
                            if (d > d1)
                                ErrorCollection.Add("MinValue", "Greater than Max");
                        }
                    }
                    else
                        ErrorCollection.Add("MinValue", "NaN");

                    minValue = value;
                    RaisePropertyChanged("MinValue");
                }
            }
        }

        public string MaxValue
        {
            get { return maxValue; }
            set
            {
                if (maxValue != value)
                {
                    ErrorCollection.Remove("MaxValue");

                    if (string.IsNullOrEmpty(value))
                        ErrorCollection.Add("MaxValue", "Field is empty");
                    else if (decimal.TryParse(value, out var d))
                    {
                        if (decimal.TryParse(MinValue, out var d1))
                        {
                            if (d < d1)
                                ErrorCollection.Add("MaxValue", "Lesser than Min");
                        }
                    }

                    else
                        ErrorCollection.Add("MaxValue", "NaN");

                    maxValue = value;
                    RaisePropertyChanged("MaxValue");
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    ErrorCollection.Remove("Description");

                    if (string.IsNullOrEmpty(value))
                        ErrorCollection.Add("Description", "Field is empty");

                    description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        public SensorModel ToLinkWith
        {
            get { return toLinkWith; }
            set
            {
                if (toLinkWith != value)
                {
                    toLinkWith = value;
                    RaisePropertyChanged("ToLinkWith");
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


        private void CheckForBlanks()
        {
            //Name
            if (string.IsNullOrEmpty(Name) && !ErrorCollection.ContainsKey("Name"))
                ErrorCollection.Add("Name", "Field is empty");
            //Min and Max Value
            if (string.IsNullOrEmpty(MinValue) && !ErrorCollection.ContainsKey("MinValue") && Visibility2 != Visibility.Visible)
                ErrorCollection.Add("MinValue", "Field is empty");

            if (string.IsNullOrEmpty(MaxValue) && !ErrorCollection.ContainsKey("MaxValue") && Visibility2 != Visibility.Visible)
                ErrorCollection.Add("MaxValue", "Field is empty");

            //Latitude and Longtitude
            if (string.IsNullOrEmpty(Latitude) && !ErrorCollection.ContainsKey("Latitude"))
                ErrorCollection.Add("Latitude", "Field is empty");

            if (string.IsNullOrEmpty(Longitude) && !ErrorCollection.ContainsKey("Longitude"))
                ErrorCollection.Add("Longitude", "Field is empty");

            //Description
            if (string.IsNullOrEmpty(Description) && !ErrorCollection.ContainsKey("Description"))
                ErrorCollection.Add("Description", "Field is empty");

            //Sensor type
            if (SelectedItem == null && !ErrorCollection.ContainsKey("SelectedItem"))
                ErrorCollection.Add("SelectedItem", "Please choose sensor type");

            //Link
            if (ToLinkWith == null && !ErrorCollection.ContainsKey("ToLinkWith"))
                ErrorCollection.Add("ToLinkWith", "Please select a sensor");
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
