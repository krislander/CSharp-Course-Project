﻿using ProjectTemplate_v2.Models.SensorTypes;

namespace ProjectTemplate_v2
{
    public class PowerConsumptionSensor:Sensor,IHasRangeValue
    {
        private decimal minValue;
        private decimal maxValue;

        public PowerConsumptionSensor(string name, string url, string description, double latitude, double longitude, bool followed,decimal minValue, decimal maxValue)
            : base(name, url, description, latitude, longitude,followed)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public PowerConsumptionSensor() : base()
        {
            MinValue = 0;
            MaxValue = 0;
        }

        public PowerConsumptionSensor(PowerConsumptionSensor sensor) : base(sensor)
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
