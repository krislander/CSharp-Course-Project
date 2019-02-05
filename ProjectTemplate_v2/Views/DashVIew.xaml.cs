using System.Windows.Controls;
using ProjectTemplate_v2.ViewModels;
using System;

namespace ProjectTemplate_v2.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void RadTileList_AutoGeneratingTile(object sender, Telerik.Windows.Controls.AutoGeneratingTileEventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
                 DashViewModel.AutoGenerateTile(e)
            ));

            //DashViewModel.AutoGenerateTile(e);
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Unloaded -= UserControl_Unloaded;
            DataContext = null;
            GC.Collect();
        }
    }
}
