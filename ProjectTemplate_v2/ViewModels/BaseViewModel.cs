using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace ProjectTemplate_v2.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected Sensors sensors;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propName));
        }

        protected void UpdateXml(Sensors sensors)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Sensors));
            TextWriter writer = new StreamWriter("sensors.xml");

            serializer.Serialize(writer, sensors);
            writer.Close();
        }
    }
}
