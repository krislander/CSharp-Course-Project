using MaterialDesignThemes.Wpf;
using ProjectTemplate_v2.Models.SensorTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;


namespace ProjectTemplate_v2.ViewModels
{
    public class EditFormDialogViewModel : BaseViewModel, IDataErrorInfo
    {
        private string name;
        private string latitude;
        private string longitude;
        private string description;
        private string minValue;
        private string maxValue;

        public Sensor Selected { get; set; }
        public Visibility DoorWindow { get; set; }
        public Visibility OtherVis { get; set; }
        public ICommand EditCommand { get; private set; }
        public SnackbarMessageQueue Snackbar { get; set; }
        public bool Open { get; set; }
        private int Index { get; set; }
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        private string oldName;


        public EditFormDialogViewModel(Sensors sensors, Sensor selected, SnackbarMessageQueue snackbar)
        {
            this.sensors = sensors;
            Snackbar = snackbar;
            EditCommand = new DelegateCommand(SubmitEdit);
            Index = sensors.List
                    .IndexOf(sensors.List.Where(sensor => sensor == selected).FirstOrDefault());
            Selected = selected;

            oldName = Selected.Name;
            Name = Selected.Name;
            Latitude = selected.Latitude.ToString();
            Longitude = selected.Longitude.ToString();
            Description = selected.Description;

            if (Selected is WindowDoorSensor)
            {
                Open = ((WindowDoorSensor)selected).Opened;
                OtherVis = Visibility.Collapsed;
                DoorWindow = Visibility.Visible;
            }
            else
            {
                MinValue = ((IHasRangeValue)selected).MinValue.ToString();
                MaxValue = ((IHasRangeValue)selected).MaxValue.ToString();
                DoorWindow = Visibility.Collapsed;
                OtherVis = Visibility.Visible;
            }
        }

        private void SubmitEdit(object obj)
        {
            //Validate();

            if (ErrorCollection.Count == 0)
            {
                Selected.Name = Name;
                Selected.Description = Description;
                Selected.Latitude = Convert.ToDouble(Latitude);
                Selected.Longitude = Convert.ToDouble(Longitude);

                if (Selected is WindowDoorSensor windowDoorSensor)
                {
                    windowDoorSensor.Opened = Open;
                }
                else
                {
                    ((IHasRangeValue)Selected).MinValue = Convert.ToDecimal(MinValue);
                    ((IHasRangeValue)Selected).MaxValue = Convert.ToDecimal(MaxValue);
                }

                sensors.List[Index] = Selected;
                UpdateXml(sensors);
                Snackbar.Enqueue("Changes saved");
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }



        //IDataErrorInfo implementation
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
                    else if (value != oldName && sensors.List.FirstOrDefault(item => item.Name == value) != null)
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
    }
}
