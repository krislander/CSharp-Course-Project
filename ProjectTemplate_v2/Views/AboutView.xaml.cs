using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;

namespace ProjectTemplate_v2.Views
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        public string ImagePath { get; private set; }
        

        public AboutView()
        {
            InitializeComponent();
        }

        private void CreatorsPage_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void DashboardPage_Checked(object sender, RoutedEventArgs e)
        {
            

        }

        private void SensorsPage_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void MapPage_Checked(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
