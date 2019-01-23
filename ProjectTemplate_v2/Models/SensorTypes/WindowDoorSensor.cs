using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTemplate_v2
{
    public class WindowDoorSensor:Sensor
    {
        private bool opened;

        public WindowDoorSensor(string name, string url, string description, double latitude, double longitude,bool followed,bool opened) 
            : base(name, url, description, latitude, longitude,followed)
        {
            Opened = opened;
        }

        public WindowDoorSensor():base()
        {
            Opened = false;
        }

        public WindowDoorSensor(WindowDoorSensor sensor) : base(sensor)
        {
            Opened = sensor.Opened;
        }

        public bool Opened
        {
            get { return opened; }
            set
            {
                if (opened != value)
                {
                    opened = value;
                    NotifyPropertyChanged("Opened");
                }
            }
        }

    }
}
