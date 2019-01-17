using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;
using ProjectTemplate_v2.ViewModels;

namespace ProjectTemplate_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Sensors sensors;

        public MainWindow()
        {
            InitializeComponent();
            InitializeList();
            DataContext = new DashViewModel(sensors);
        }

        private void InitializeList()
        {
            sensors = new Sensors();

            XmlSerializer serializer = new XmlSerializer(typeof(Sensors));

            FileStream fs;

            if (!File.Exists("sensors.xml"))
            {
                using (StreamWriter sw = new StreamWriter("sensors.xml"))
                {
                    serializer.Serialize(sw, sensors);
                }

            }

            fs = new FileStream("sensors.xml", FileMode.Open);
            sensors = (Sensors)serializer.Deserialize(fs);
            fs.Close();
        }

        private void BtnToMain_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataContext = new DashViewModel(sensors);
            MenuToggleButton.IsChecked = false;
        }

        private void BtnToSensorList_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataContext = new ListViewModel(sensors);
            MenuToggleButton.IsChecked = false;
        }

        private void BtnToSensorAdd_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataContext = new AddSensorViewModel(sensors);
            MenuToggleButton.IsChecked = false;
        }

        private void BtnToMap_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataContext = new MapViewModel(ref sensors);
            MenuToggleButton.IsChecked = false;
        }
    }
}
