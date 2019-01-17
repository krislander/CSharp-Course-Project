using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProjectTemplate_v2.ViewModels
{
    public partial class PushpinModel : Pushpin
    {
        //public Location Location { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class MapLocation
    {
        public Location Location { get; set; }
        public string Name { get; set; }
    }

    public class MapViewModel : BaseViewModel
    {
        private ObservableCollection<PushpinModel> pushpins;
        public Map MapWithMarkers { get; set; }
        public Border Infobox = new Border();

        public MapViewModel(ref Sensors sensors)
        {
            this.sensors = sensors;
            MapLayer dataLayer = new MapLayer();

            ObservableCollection<PushpinModel> Pushpins = new ObservableCollection<PushpinModel>();

            InitMap();
            Map MapWithSensors = new Map();
            if (sensors.List.Count == 0)
                MapWithSensors.Center = new Location(42.698334, 23.319941);

        }

        private void InitMap()
        {
            MapWithMarkers = new Map
            {
                Center = new Location(42.698334, 23.319941),
                ZoomLevel = 10,
                Margin = new Thickness(0, 3, 0, 0),
                Mode = new AerialMode(true),
                CredentialsProvider = new ApplicationIdCredentialsProvider("Arlj7m-YopkSpqjw8gdI2PHqnd8tulYdY91G_h8qZ42jmUOPjjqFRnO7iMpk9TuS")
            };

            foreach (var sensor in sensors.List)
            {
                PushpinModel pin = new PushpinModel
                {
                    Location = new Location(sensor.Latitude, sensor.Longitude),
                    Title = sensor.Name.ToString(),
                    Description = sensor.Description
                };

                ToolTipService.SetToolTip(pin, new ToolTip()
                {
                    DataContext = pin,
                    Style = Application.Current.Resources["CustomInfoboxStyle"] as Style
                });
                //Infobox logic
                //pin.MouseLeftButtonDown += PinClicked;

                MapWithMarkers.Children.Add(pin);
            }
        }

        public ObservableCollection<PushpinModel> Pushpins
        {
            get { return pushpins; }
            set
            {
                if (pushpins != value)
                    pushpins = value;
                RaisePropertyChanged("Pushpins");
            }
        }

        private void PinClicked(object sender, MouseButtonEventArgs e)
        {
            Pushpin temp = sender as Pushpin;
            PushpinModel pushpins = (PushpinModel)temp.Tag;
            if (!String.IsNullOrEmpty(pushpins.Title) || !String.IsNullOrEmpty(pushpins.Description))
            {
                Infobox.DataContext = pushpins;

                Infobox.Visibility = Visibility.Visible;

                MapLayer.SetPosition(Infobox, MapLayer.GetPosition(temp));
            }
            else
                Infobox.Visibility = Visibility.Collapsed;
        }

        private void CloseInfobox_Clicked(object sender, RoutedEventArgs e)
        {
            Infobox.Visibility = Visibility.Collapsed;
        }
    }
}