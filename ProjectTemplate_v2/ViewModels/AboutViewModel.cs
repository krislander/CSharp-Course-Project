using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ProjectTemplate_v2.ViewModels
{
    public class AboutViewModel:BaseViewModel
    {
        public ICommand CreatorsPage { get; private set; }
        public ICommand DashboardPage { get; set; }
        public ICommand SensorsPage { get; private set; }
        public ICommand MapPage { get; private set; }
        public string ImageLink;
        public string TextBlockHeader;
        public string DownTextBlockText;

        public AboutViewModel()
        {
            CreatorsPage = new DelegateCommand((object obj) => BuildCreatorsPage());
            DashboardPage = new DelegateCommand((object obj) => BuildDashboardPage());
            SensorsPage = new DelegateCommand((object obj) => BuildSensorsPage());
            MapPage = new DelegateCommand((object obj) => BuildMapPage());
        }

        private void BuildMapPage()
        {
            throw new NotImplementedException();
        }

        private void BuildSensorsPage()
        {
            throw new NotImplementedException();
        }

        private void BuildDashboardPage()
        {
            ImageLink = "Resources/Dashboard.jpg";
            //UpperTextBlockText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            TextBlockHeader = "Lorem ipsum";
            DownTextBlockText = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";

        }

        private void BuildCreatorsPage()
        {
            throw new NotImplementedException();
        }
    }
}
