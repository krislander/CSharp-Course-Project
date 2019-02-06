using MaterialDesignThemes.Wpf;
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
        private Sensor selected;

        public Sensor Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public Visibility DoorWindow { get; set; }
        public Visibility OtherVis { get; set; }
        public ICommand EditCommand { get; private set; }
        public SnackbarMessageQueue Snackbar { get; set; }
        private int Index { get; set; }

        public EditFormDialogViewModel(Sensors sensors, Sensor selected, SnackbarMessageQueue snackbar)
        {
            this.sensors = sensors;
            Snackbar = snackbar;
            EditCommand = new DelegateCommand(SubmitEdit);
            Index = sensors.List
                    .IndexOf(sensors.List.Where(sensor => sensor == selected).FirstOrDefault());
            CreateCopy(selected, ref this.selected);

            if (Selected is WindowDoorSensor)
            {
                OtherVis = Visibility.Collapsed;
                DoorWindow = Visibility.Visible;
            }
            else
            {
                DoorWindow = Visibility.Collapsed;
                OtherVis = Visibility.Visible;
            }
        }

        private void SubmitEdit(object obj)
        {
            Validate();

            if (ErrorCollection.Count == 0)
            {
                sensors.List[Index] = Selected;
                UpdateXml(sensors);
                Snackbar.Enqueue("Changes saved");
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void CreateCopy(Sensor sensor1, ref Sensor sensor2)
        {
            if (sensor1 is HumiditySensor)
                sensor2 = new HumiditySensor((HumiditySensor)sensor1);
            else if (sensor1 is NoiseSensor)
                sensor2 = new NoiseSensor((NoiseSensor)sensor1);
            else if (sensor1 is PowerConsumptionSensor)
                sensor2 = new PowerConsumptionSensor((PowerConsumptionSensor)sensor1);
            else if (sensor1 is TemperatureSensor)
                sensor2 = new TemperatureSensor((TemperatureSensor)sensor1);
            else
                sensor2 = new WindowDoorSensor((WindowDoorSensor)sensor1);
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

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        void Validate()
        {
            ErrorCollection.Clear();
            
            if(Selected is TemperatureSensor)
            {
                TemperatureSensor temp = Selected as TemperatureSensor;

                if (string.IsNullOrEmpty(temp.MinValue.ToString()) || string.IsNullOrWhiteSpace(temp.MinValue.ToString()))
                    ErrorCollection.Add("MinValue", "MinValue is empty");
                else if (Convert.ToDecimal(temp.MinValue) >= Convert.ToDecimal(temp.MaxValue))
                    ErrorCollection.Add("MinValue", "MinValue >= MaxValue");
                if (string.IsNullOrEmpty(temp.MaxValue.ToString()) || string.IsNullOrWhiteSpace(temp.MaxValue.ToString()))
                    ErrorCollection.Add("MaxValue", "MaxValue is empty");
            }
            else if(Selected is NoiseSensor)
            {
                NoiseSensor temp = Selected as NoiseSensor;

                if (string.IsNullOrEmpty(temp.MinValue.ToString()) || string.IsNullOrWhiteSpace(temp.MinValue.ToString()))
                    ErrorCollection.Add("MinValue", "MinValue is empty");
                else if (Convert.ToDecimal(temp.MinValue) >= Convert.ToDecimal(temp.MaxValue))
                    ErrorCollection.Add("MinValue", "MinValue >= MaxValue");
                if (string.IsNullOrEmpty(temp.MaxValue.ToString()) || string.IsNullOrWhiteSpace(temp.MaxValue.ToString()))
                    ErrorCollection.Add("MaxValue", "MaxValue is empty");
            }
            else if(Selected is HumiditySensor)
            {
                HumiditySensor temp = Selected as HumiditySensor;

                if (string.IsNullOrEmpty(temp.MinValue.ToString()) || string.IsNullOrWhiteSpace(temp.MinValue.ToString()))
                    ErrorCollection.Add("MinValue", "MinValue is empty");
                else if (Convert.ToDecimal(temp.MinValue) >= Convert.ToDecimal(temp.MaxValue))
                    ErrorCollection.Add("MinValue", "MinValue >= MaxValue");
                if (string.IsNullOrEmpty(temp.MaxValue.ToString()) || string.IsNullOrWhiteSpace(temp.MaxValue.ToString()))
                    ErrorCollection.Add("MaxValue", "MaxValue is empty");
            }
            else if(Selected is PowerConsumptionSensor)
            {
                PowerConsumptionSensor temp = Selected as PowerConsumptionSensor;

                if (string.IsNullOrEmpty(temp.MinValue.ToString()) || string.IsNullOrWhiteSpace(temp.MinValue.ToString()))
                    ErrorCollection.Add("MinValue", "MinValue is empty");
                else if (Convert.ToDecimal(temp.MinValue) >= Convert.ToDecimal(temp.MaxValue))
                    ErrorCollection.Add("MinValue", "MinValue >= MaxValue");
                if (string.IsNullOrEmpty(temp.MaxValue.ToString()) || string.IsNullOrWhiteSpace(temp.MaxValue.ToString()))
                    ErrorCollection.Add("MaxValue", "MaxValue is empty");
            }

            //Name
            if (string.IsNullOrEmpty(Selected.Name))
                ErrorCollection.Add("Name", "Name is empty");
            else if (Selected.Name.Length < 4)
                ErrorCollection.Add("Name", "Name's length must be at least 4");

            //Latitude and Longtitude
            if (string.IsNullOrWhiteSpace(Selected.Latitude.ToString()) || string.IsNullOrEmpty(Selected.Latitude.ToString()))
                ErrorCollection.Add("Latitude", "Latitude is empty");
            else if (Convert.ToDouble(Selected.Latitude) < -90 || Convert.ToDouble(Selected.Latitude) > 90)
                ErrorCollection.Add("Latitude", "Invalid Latitude coordinates");

            if (string.IsNullOrWhiteSpace(Selected.Longitude.ToString()) || string.IsNullOrEmpty(Selected.Longitude.ToString()))
                ErrorCollection.Add("Longitude", "Longtitude is empty");
            else if (Convert.ToDouble(Selected.Longitude) < -180 || Convert.ToDouble(Selected.Longitude) > 180)
                ErrorCollection.Add("Longitude", "Invalid Longtitude coordinates");

            //Description
            if (string.IsNullOrEmpty(Selected.Description) || string.IsNullOrWhiteSpace(Selected.Description))
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
