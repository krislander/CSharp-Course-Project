using ProjectTemplate_v2.Models.Gauges;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls;
using ProjectTemplate_v2.Resources.Gauges;
using System.Windows;

namespace ProjectTemplate_v2.ViewModels
{
    public class DashViewModel : BaseViewModel
    {
        //public ICommand AutoTileCommand { get; private set; }
        public ObservableCollection<Sensor> FollowedList { get; set; }

        public DashViewModel(Sensors sensors)
        {
            this.sensors = sensors;
            GetFollowedList(sensors);          
            //AutoTileCommand = new DelegateCommand(AutoGenerateTile);
        }     

        private void GetFollowedList(Sensors sensors)
        {
            FollowedList = new ObservableCollection<Sensor>(sensors.List);
            ICollectionView source = CollectionViewSource.GetDefaultView(FollowedList);
            source.Filter = item => ((Sensor)item).Followed;
        }

        public static void AutoGenerateTile(AutoGeneratingTileEventArgs e)
        {
            Sensor sensor = e.Tile.Content as Sensor;

            e.Tile.Background = new SolidColorBrush(Colors.DarkCyan);
            e.Tile.TileType = TileType.Single;

            if (sensor is HumiditySensor)
            {
                try
                {
                    var model = HttpService.SensorList.First(item => item.Tag == sensor.Link);
                    e.Tile.Content = new HumidityGaugeCtrl((HumiditySensor)sensor, model);

                }
                catch
                {
                    e.Tile.Background = new SolidColorBrush(Colors.LightGray);
                    //TODO: sensor off view    
                }

            }
            else if (sensor is TemperatureSensor)
            {
                e.Tile.TileType = TileType.Double;

                var model = HttpService.SensorList.First(item => item.Tag == sensor.Link);

                if (e.Tile.DisplayIndex % 2 == 1)
                {
                    e.Tile.DisplayIndex += 2;
                }
                e.Tile.Content = new TempGaugeCtrl((TemperatureSensor)sensor, model);
            }
        }
    }
}
