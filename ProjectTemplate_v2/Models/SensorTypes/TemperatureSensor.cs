﻿using ProjectTemplate_v2.Models.SensorTypes;

namespace ProjectTemplate_v2
{
    public class TemperatureSensor : Sensor,IHasRangeValue
    {
        private decimal minValue;
        private decimal maxValue;

        public TemperatureSensor(string name, string url, string description, double latitude, double longitude,bool followed,decimal minValue,decimal maxValue) 
            : base(name, url, description, latitude, longitude,followed)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public TemperatureSensor() : base()
        {
            MinValue = 0;
            MaxValue = 0;
        }

        public TemperatureSensor(TemperatureSensor sensor) : base(sensor)
        {
            MinValue = sensor.MinValue;
            MaxValue = sensor.MaxValue;
        }

        public decimal MaxValue
        {
            get { return maxValue; }
            set
            {
                if (maxValue != value)
                {
                    maxValue = value;
                    NotifyPropertyChanged("MaxValue");
                }
            }
        }

        public decimal MinValue
        {
            get { return minValue; }
            set
            {
                if (minValue != value)
                {
                    minValue = value;
                    NotifyPropertyChanged("MinValue");
                }
            }
        }
    }
}
