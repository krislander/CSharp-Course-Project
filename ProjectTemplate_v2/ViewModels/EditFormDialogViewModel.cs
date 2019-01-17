using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace ProjectTemplate_v2.ViewModels
{
    public class EditFormDialogViewModel : BaseViewModel
    {
        //public List<string> Types { get; private set; } = 
        //    new List<string>() { "Temperature", "Humidity", "Electricity Consumption", "Noise", "Window/Door" };
        private Sensor selected;

        public Sensor Selected
        {
            get { return selected; }
            set { selected = value; }
        }

        public Visibility DoorWindow { get; set; }
        public Visibility OtherVis { get; set; }
        public ICommand EditCommand { get; private set; }
        public int Index { get; set; }

        public EditFormDialogViewModel(Sensors sensors, Sensor selected)
        {
            this.sensors = sensors;
            EditCommand = new DelegateCommand(SubmitEdit);
            Index = sensors.List
                    .IndexOf(sensors.List.Where(sensor => sensor == selected).FirstOrDefault());
            CreateCopy(selected, ref this.selected);

            if (Selected is WindowDoorSensor)
            {
                OtherVis = Visibility.Collapsed;
                DoorWindow = Visibility.Visible;
            }
            else
            {
                DoorWindow = Visibility.Collapsed;
                OtherVis = Visibility.Visible;
            }
        }

        private void SubmitEdit(object obj)
        {
            sensors.List[Index] = Selected;
            UpdateXml(sensors);

            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void CreateCopy(Sensor sensor1, ref Sensor sensor2)
        {
            if (sensor1 is HumiditySensor)
                sensor2 = new HumiditySensor((HumiditySensor)sensor1);
            else if (sensor1 is NoiseSensor)
                sensor2 = new NoiseSensor((NoiseSensor)sensor1);
            else if (sensor1 is PowerConsumptionSensor)
                sensor2 = new PowerConsumptionSensor((PowerConsumptionSensor)sensor1);
            else if (sensor1 is TemperatureSensor)
                sensor2 = new TemperatureSensor((TemperatureSensor)sensor1);
            else
                sensor2 = new WindowDoorSensor((WindowDoorSensor)sensor1);
        }
    }
}
