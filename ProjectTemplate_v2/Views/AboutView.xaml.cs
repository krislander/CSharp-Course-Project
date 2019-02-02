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
        public string ImagePath1 { get; private set; }
        public string ImagePath2 { get; private set; }
        public string ImagePath3 { get; private set; }
        public string ImagePath4 { get; private set; }
        

        public AboutView()
        {
            InitializeComponent();
        }

        private void CreatorsPage_Checked(object sender, RoutedEventArgs e)
        {
            //screenshot of creator1
            card2.Visibility = Visibility.Visible;
            ImagePath2 = @"/ProjectTemplate_v2;component/Resources/PicturesAboutPage/Dashboard.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";

            //screenshot of creator2
            card4.Visibility = Visibility.Visible;
            ImagePath2 = @"/ProjectTemplate_v2;component/Resources/PicturesAboutPage/Dashboard.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";
        }

        private void DashboardPage_Checked(object sender, RoutedEventArgs e)
        {
            //screenshot of sensor from dashboard
            card1.Visibility = Visibility.Visible;
            ImagePath1 = @"\ProjectTemplate_v2\Resources\PicturesAboutPage\Dashboard.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";

            //screenshot of burger menu
            card2.Visibility = Visibility.Visible;
            ImagePath2 = @"\ProjectTemplate_v2\Resources\PicturesAboutPage\Dashboard.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";

        }

        private void SensorsPage_Checked(object sender, RoutedEventArgs e)
        {
            //screenshot of sensor list
            card1.Visibility = Visibility.Visible;
            ImagePath1 = @"\ProjectTemplate_v2\Resources\PicturesAboutPage\Dashboard.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";

            //screenshot of edit form
            card2.Visibility = Visibility.Visible;
            ImagePath2 = @"\ProjectTemplate_v2\Resources\PicturesAboutPage\Dashboard.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";

            //screenshot of popupBox in sensors
            card2.Visibility = Visibility.Visible;
            ImagePath2 = @"\ProjectTemplate_v2\Resources\PicturesAboutPage\Dashboard.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";

            //screenshot of current value field
            card2.Visibility = Visibility.Visible;
            ImagePath2 = @"\ProjectTemplate_v2\Resources\PicturesAboutPage\Dashboard.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";
        }

        private void MapPage_Checked(object sender, RoutedEventArgs e)
        {
            //screenshot of sensor from dashboard
            card1.Visibility = Visibility.Visible;
            ImagePath1 = @"\ProjectTemplate_v2\Resources\PicturesAboutPage\MapPage.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";

            //screenshot of burger menu
            card2.Visibility = Visibility.Visible;
            ImagePath2 = @"\ProjectTemplate_v2\Resources\PicturesAboutPage\MapPage.jpg";
            txtBlock1Header.Text = "Lorem Ipsum";
            txtBlock1Text.Text = "Lorem Ipsum";
        }
    }
}
