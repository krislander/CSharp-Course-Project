using Microsoft.Maps.MapControl.WPF;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ProjectTemplate_v2.ViewModels
{
    public partial class PushpinModel : Pushpin
    {
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }       
    }

    public class MapViewModel : BaseViewModel
    {
        public Map MapWithMarkers { get; set; }
        private PushpinModel selectedPushpin;
        private ObservableCollection<PushpinModel> pushpins;

        public MapViewModel(ref Sensors sensors)
        {
            this.sensors = sensors;
            MapLayer dataLayer = new MapLayer();
            Pushpins = new ObservableCollection<PushpinModel>();

            InitMap();
        }

        private void InitMap()
        {
            foreach (var sensor in sensors.List)
            {
                PushpinModel pin = new PushpinModel
                {
                    //Location is the field of Pushpin class
                    Location = new Location(sensor.Latitude, sensor.Longitude),
                    Latitude = sensor.Latitude,
                    Longtitude = sensor.Longitude,
                    Title = sensor.Name.ToString(),
                    Type = sensor.GetType().Name,
                    Description = sensor.Description 
                };

                ToolTipService.SetToolTip(pin, new ToolTip()
                {
                    DataContext = pin,
                    Style = Application.Current.Resources["CustomInfoboxStyle"] as Style
                });

                Pushpins.Add(pin);
                //MapWithMarkers.Children.Add(pin);
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

        public PushpinModel SelectedPushpin
        {
            get { return selectedPushpin; }
            set
            {
                if (selectedPushpin != value)
                {
                    selectedPushpin = value;
                    RaisePropertyChanged("SelectedPushpin");
                }
            }
        }
    }
}