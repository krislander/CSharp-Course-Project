using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace ProjectTemplate_v2.ViewModels
{
    public partial class PushpinModel : Pushpin
    {
        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string CurrentValue { get; set; }
    }

    public class MapViewModel : BaseViewModel
    {
        public Map MapWithMarkers { get; set; }
        public ICommand OpenPopupBox { get; private set; }
        private PushpinModel selectedPushpin;
        private ObservableCollection<PushpinModel> pushpins;
        private bool isChecked;

        public MapViewModel(ref Sensors sensors)
        {
            this.sensors = sensors;
            MapLayer dataLayer = new MapLayer();
            OpenPopupBox = new DelegateCommand(PushPinClicked);
            Pushpins = new ObservableCollection<PushpinModel>();

            InitMap();
        }

        private void PushPinClicked(object obj)
        {
            SelectedPushpin = (PushpinModel)obj;
            //tuk ima nqkvi neshta
        }

        private void InitMap()
        {
            foreach (var sensor in sensors.List)
            {
                PushpinModel pin = new PushpinModel
                {
                    //Location is the field of Pushpin class
                    Location = new Location(sensor.Latitude, sensor.Longitude),
                    //currentvalue??
                    Latitude = sensor.Latitude,
                    Longtitude = sensor.Longitude,
                    Title = sensor.Name.ToString(),
                    Type = sensor.GetType().ToString(),
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

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if(isChecked !=  value)
                {
                    isChecked = value;
                    RaisePropertyChanged("IsChecked");
                }
            }
        }
    }
}