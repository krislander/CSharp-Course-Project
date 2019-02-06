using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ProjectTemplate_v2.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ICommand CreatorsCommand { get; private set; }
        public ICommand DashboardCommand { get; set; }
        public ICommand SensorsCommand { get; private set; }
        public ICommand MapCommand { get; private set; }
        private string imagePath;
        private string headerText;
        private string mainText;
        public Card Card1 { get; set; }
        public Card Card2 { get; set; }
        public Card Card3 { get; set; }
        public Card Card4 { get; set; }
        public Visibility Visibility1 { get; set; }
        public Visibility Visibility2 { get; set; }
        public Visibility Visibility3 { get; set; }
        public Visibility Visibility4 { get; set; }

        public AboutViewModel()
        {
            CreatorsCommand = new DelegateCommand(BuildCreatorsPage);
            DashboardCommand = new DelegateCommand(BuildDashboardPage);
            SensorsCommand = new DelegateCommand(BuildSensorsPage);
            MapCommand = new DelegateCommand(BuildMapPage);
        }

        private void BuildMapPage(object obj)
        {
            Visibility2 = Visibility.Visible;
            Visibility3 = Visibility.Visible;
            
        }

        private void BuildSensorsPage(object obj)
        {
            Visibility1 = Visibility.Visible;
            Visibility2 = Visibility.Visible;
            Visibility3 = Visibility.Visible;
            Visibility4 = Visibility.Visible;
        }

        private void BuildDashboardPage(object obj)
        {
            Visibility1 = Visibility.Visible;
            Visibility2 = Visibility.Visible;
            Visibility3 = Visibility.Visible;
            Visibility4 = Visibility.Visible;
        }

        private void BuildCreatorsPage(object obj)
        {
            Visibility2 = Visibility.Visible;
            Visibility3 = Visibility.Visible;
        }

        //ImagePath = "Resources/Dashboard.jpg";
        ////UpperTextBlockText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
        //HeaderText = "Lorem ipsum";
        //MainText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    RaisePropertyChanged("ImagePath");
                }
            }
        }

        public string HeaderText
        {
            get { return headerText; }
            set
            {
                if (headerText != value)
                {
                    headerText = value;
                    RaisePropertyChanged("HeaderText");
                }
            }
        }

        public string MainText
        {
            get { return mainText; }
            set
            {
                if (mainText != value)
                {
                    mainText = value;
                    RaisePropertyChanged("MainText");
                }
            }
        }
    }
}
