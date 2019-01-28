using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Xml.Serialization;
using MaterialDesignThemes.Wpf;
using ProjectTemplate_v2.ViewModels;

namespace ProjectTemplate_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Sensors sensors;
        public SnackbarMessageQueue Snackbar { get; set; } = new SnackbarMessageQueue();

        public MainWindow()
        {
            InitializeComponent();
            InitializeList();
            HttpService.InitializeClient();
            DataContext = new DashViewModel(sensors);
            //Binding binding = new Binding
            //{
            //    Path = new PropertyPath("Snackbar"),
            //    Source=this
            //};
            //BindingOperations.SetBinding(dialogHost, DialogHost.SnackbarMessageQueueProperty, binding);
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
            MenuToggleButton.IsChecked = false;
            DataContext = new DashViewModel(sensors);
        }

        private void BtnToSensorList_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataContext = new ListViewModel(sensors,Snackbar);
            MenuToggleButton.IsChecked = false;
        }

        private void BtnToMap_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DataContext = new MapViewModel(ref sensors);
            MenuToggleButton.IsChecked = false;
        }

        private void BtnAbout_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
