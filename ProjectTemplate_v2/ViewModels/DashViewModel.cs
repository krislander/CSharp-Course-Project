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
            if (!HttpService.IsInitialized)
                HttpService.InitializeClient();
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

            //e.Tile.Background = (Brush)Application.Current.Resources["PrimaryHueDarkBrush"];
            //e.Tile.Background = new SolidColorBrush(Colors.White);
            e.Tile.TileType = TileType.Single;

            if (sensor is HumiditySensor)
            {
                e.Tile.Content = new HumidityGaugeCtrl((HumiditySensor)sensor);

            }
            else if (sensor is TemperatureSensor)
            {
                e.Tile.Content = new TempGaugeCtrl((TemperatureSensor)sensor);
            }
            else if (sensor is PowerConsumptionSensor)
            {
                e.Tile.Content = new PowerGaugeCtrl((PowerConsumptionSensor)sensor);
            }
        }
    }
}
