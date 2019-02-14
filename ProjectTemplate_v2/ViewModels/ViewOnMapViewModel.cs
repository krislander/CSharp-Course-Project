using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ProjectTemplate_v2.ViewModels
{
    public class ViewOnMapViewModel : BaseViewModel
    {
        private Sensor selected;
        public Map MapForSensorList { get; set; }
        public ICommand MapViewCommand { get; private set; }

        public ViewOnMapViewModel(Sensors sensors, Sensor selected)
        {
            this.sensors = sensors;
            MapViewCommand = new DelegateCommand(ExitMapView);
            CreateCopy(selected, ref this.selected);

            //initialize a map
            InitMap();
            //set the center to the only pushpin on the map
            MapForSensorList.Center = new Location(selected.Latitude, selected.Longitude);
        }

        private void ExitMapView(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        public Sensor Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        private void InitMap()
        {
            MapForSensorList = new Map
            {
                ZoomLevel = 12,
                Mode = new AerialMode(true),
                CredentialsProvider = new ApplicationIdCredentialsProvider("Arlj7m-YopkSpqjw8gdI2PHqnd8tulYdY91G_h8qZ42jmUOPjjqFRnO7iMpk9TuS")
            };

            PushpinModel pin = new PushpinModel
            {
                //Location is the field of Pushpin class
                Location = new Location(Selected.Latitude, Selected.Longitude),
                Latitude = Selected.Latitude,
                Longtitude = Selected.Longitude,
                Title = Selected.Name.ToString(),
                Type = Selected.GetType().Name,
                Description = Selected.Description
            };

            ToolTipService.SetToolTip(pin, new ToolTip()
            {
                DataContext = pin,
                Style = Application.Current.Resources["CustomInfoboxStyle"] as Style
            });
            //Infobox logic
            //pin.MouseLeftButtonDown += PinClicked;

            MapForSensorList.Children.Add(pin);
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
    }
}
