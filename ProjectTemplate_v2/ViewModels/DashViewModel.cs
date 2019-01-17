using ProjectTemplate_v2.Models;
using ProjectTemplate_v2.Models.Gauges;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls;

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
            HttpService.InitializeClient();
            //AutoTileCommand = new DelegateCommand(AutoGenerateTile);
        }

        private void GetFollowedList(Sensors sensors)
        {
            FollowedList = new ObservableCollection<Sensor>(sensors.List);
            ICollectionView source = CollectionViewSource.GetDefaultView(FollowedList);
            source.Filter = item => ((Sensor)item).Followed;
        }

        public static void AutoGenerateTile(object e)
        {
            Sensor sensor = ((AutoGeneratingTileEventArgs)e).Tile.Content as Sensor;

            ((AutoGeneratingTileEventArgs)e).Tile.Background = new SolidColorBrush(Colors.Teal);
            ((AutoGeneratingTileEventArgs)e).Tile.TileType = TileType.Single;

            if (sensor is HumiditySensor)
            {
                var model = HttpService.SensorList.First(item => item.Tag == sensor.Name);
                ((AutoGeneratingTileEventArgs)e).Tile.Content = new HumidityGaugeCtrl((HumiditySensor)sensor,model);

            }
        }
    }
}
