using System.ComponentModel;

namespace ProjectTemplate_v2
{
    public class Sensor:INotifyPropertyChanged
    {

        private string name;
        private string description;
        private string link;
        private double latitude;
        private double longitude;
        private bool followed;


        protected Sensor(string name,string url,string description,double latitude,double longitude,bool follow)
        {
            Name = name;
            Link = url;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            Followed = follow;
        }

        public Sensor() : this(null,null,null,0,0,false)
        {

        }

        public Sensor(Sensor sensor):this(sensor.Name,sensor.Link,sensor.Description,sensor.Latitude,sensor.Longitude,sensor.Followed)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propName));
        }

        public bool Followed
        {
            get { return followed; }
            set
            {
                if (followed != value)
                {
                    followed = value;
                    NotifyPropertyChanged("Followed");
                }
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (longitude != value)
                {
                    longitude = value;
                    NotifyPropertyChanged("Longitude");
                }
            }
        }


        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (latitude != value)
                {
                    latitude = value;
                    NotifyPropertyChanged("Latitude");
                }
            }
        }


        public string Link
        {
            get { return link; }
            set
            {
                if (link!= value)
                {
                    link = value;
                    NotifyPropertyChanged("Link");
                }
            }
        }


        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public override string ToString()
        {
            return string.Format("");
        }

    }
}
