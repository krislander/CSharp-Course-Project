using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;

namespace ProjectTemplate_v2.Views
{
    public class ToIconConverter : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PackIconKind iconKind;
            if (value is Sensor sensor)
            {

                if (sensor is HumiditySensor)
                    iconKind = PackIconKind.Humidity;
                else if (sensor is NoiseSensor)
                    iconKind = PackIconKind.VolumeHigh;
                else if (sensor is PowerConsumptionSensor)
                    iconKind = PackIconKind.Electricity;
                else if (sensor is TemperatureSensor)
                    iconKind = PackIconKind.ThermometerLines;
                else
                    iconKind = PackIconKind.DoorOpen;
            }
            else
                iconKind = PackIconKind.AccessPoint;

            return iconKind;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
