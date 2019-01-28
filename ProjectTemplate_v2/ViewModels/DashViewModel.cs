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
        //static int counter = 0;

        public DashViewModel(Sensors sensors)
        {
            this.sensors = sensors;
            GetFollowedList(sensors);          
            //AutoTileCommand = new DelegateCommand(AutoGenerateTile);
            //Interlocked.Increment(ref counter);
            //MessageBox.Show($"{counter}");
        }

        //~DashViewModel()
        //{
        //    Interlocked.Decrement(ref counter);
        //}

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
                var model = HttpService.SensorList.First(item => item.Tag == sensor.Link);
                ((AutoGeneratingTileEventArgs)e).Tile.Content = new HumidityGaugeCtrl((HumiditySensor)sensor, model);

            }
            else if (sensor is TemperatureSensor)
            {
                ((AutoGeneratingTileEventArgs)e).Tile.TileType = TileType.Double;

                if (((AutoGeneratingTileEventArgs)e).Tile.DisplayIndex % 2 == 1)
                {
                   // MessageBox.Show($"{((AutoGeneratingTileEventArgs)e).Tile.DisplayIndex}");
                    ((AutoGeneratingTileEventArgs)e).Tile.DisplayIndex += 2;
                }
                ((AutoGeneratingTileEventArgs)e).Tile.Content = new TempGaugeCtrl();
            }
        }
    }
}
