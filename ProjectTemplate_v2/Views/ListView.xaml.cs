using MaterialDesignThemes.Wpf;
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

namespace ProjectTemplate_v2.Views
{
    /// <summary>
    /// Interaction logic for ListView.xaml
    /// </summary>
    public partial class ListView : UserControl
    {
        public ListView()
        {
            InitializeComponent();
        }

        private void ListBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((ListBoxItem)sender).IsSelected = true;
        }

        private void LbListBox_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (VisualTreeHelper.GetChild(lbListBox, 0) is Decorator border)
            {
                ScrollViewer scrollViewer = border.Child as ScrollViewer;
                if (scrollViewer.VerticalOffset == 0)
                {
                    lbListBox.BorderThickness = new Thickness(0, 1, 0, 0);
                }
                else if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {
                    lbListBox.BorderThickness = new Thickness(0, 0, 0, 1);
                }
                else
                {
                    lbListBox.BorderThickness = new Thickness(0, 0, 0, 0);
                }
            }
        }
    }
}
