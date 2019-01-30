﻿using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Linq;
using ProjectTemplate_v2.Views;
using System.Windows.Input;
using Telerik.Windows.Controls;
using System;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

namespace ProjectTemplate_v2.ViewModels
{
    public class ListViewModel : BaseViewModel
    {
        public ICommand RemoveCommand { get; private set; }
        public ICommand FollowCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand MapViewCommand { get; private set; }
        public ICommand AddCommand { get; set; }
        public ICommand AllCommand { get; private set; }
        public ICommand TempCommand { get; private set; }
        public ICommand HumCommand { get; private set; }
        public ICommand NoiseCommand { get; private set; }
        public ICommand EleCommand { get; private set; }
        public ICommand WDCommand { get; private set; }
        public SnackbarMessageQueue Snackbar { get; set; }

        private Sensor selected;
        private string followButtonContent;
        private PackIconKind iconKind;
        private ObservableCollection<Sensor> list;
        private ICollectionView collectionView;


        public ListViewModel(Sensors sensors,SnackbarMessageQueue snackbar)
        {
            this.sensors = sensors;
            Snackbar = snackbar;
            List = sensors.List;
            collectionView = CollectionViewSource.GetDefaultView(List);
            if (collectionView.Filter!=null)
                collectionView.Filter = null;

            RemoveCommand = new DelegateCommand(RemoveSensor);
            FollowCommand = new DelegateCommand(ChangeFollow);
            EditCommand = new DelegateCommand(ExecuteEditDialog);
            MapViewCommand = new DelegateCommand(ViewOnMap);
            AddCommand = new DelegateCommand(OpenAddForm);

            AllCommand = new DelegateCommand((object obj)=>Filter(typeof(object)));
            TempCommand = new DelegateCommand((object obj) => Filter(typeof(TemperatureSensor)));
            HumCommand = new DelegateCommand((object obj) => Filter(typeof(HumiditySensor)));
            EleCommand = new DelegateCommand((object obj) => Filter(typeof(PowerConsumptionSensor)));
            WDCommand = new DelegateCommand((object obj) => Filter(typeof(WindowDoorSensor)));
            NoiseCommand = new DelegateCommand((object obj) => Filter(typeof(NoiseSensor)));
        }

        private void Filter(Type type)
        {
            if (type != typeof(object))
            {
                collectionView.Filter = item => item.GetType() == type;
            }
            else
                collectionView.Filter = null;
        }

        private async void OpenAddForm(object obj)
        {
            var view = new AddFormDialog
            {
                DataContext = new AddFormDialogViewModel(sensors,Snackbar)
            };
            await DialogHost.Show(view);
        }

        private async void ViewOnMap(object obj)
        {
            var view = new ViewOnMap
            {
                DataContext = new ViewOnMapViewModel(sensors, Selected)
            };
            await DialogHost.Show(view);
        }

        private async void ExecuteEditDialog(object obj)
        {
            var view = new EditFormDialog
            {
                DataContext = new EditFormDialogViewModel(sensors, Selected,Snackbar)
            };
            await DialogHost.Show(view);
        }

        private void RemoveSensor(object param)
        {
            sensors.List
                .Where(item => Selected == item)
                .ToList().All(i => sensors.List.Remove(i));
            UpdateXml(sensors);
            Snackbar.Enqueue("Sensor removed");
        }

        private void ChangeFollow(object param)
        {
            sensors.List
                .Where(item => Selected == item)
                .Select(item => item.Followed = !item.Followed).ToList();

            FollowButtonContent = !Selected.Followed ? "Follow" : "Unfollow";
            UpdateXml(sensors);
        }

        public Sensor Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                    if (Selected != null)
                    {
                        FollowButtonContent = !Selected.Followed ? "Follow" : "Unfollow";

                        if (selected is HumiditySensor)
                            IconKind = PackIconKind.Humidity;
                        else if (selected is NoiseSensor)
                            IconKind = PackIconKind.VolumeHigh;
                        else if (selected is PowerConsumptionSensor)
                            IconKind = PackIconKind.Electricity;
                        else if (selected is TemperatureSensor)
                            IconKind = PackIconKind.ThermometerLines;
                        else
                            IconKind = PackIconKind.DoorOpen;
                    }
                    else
                    {
                        IconKind = PackIconKind.AccessPoint;
                    }
                    RaisePropertyChanged("Selected");
                }
            }
        }

        public ObservableCollection<Sensor> List
        {
            get { return list; }
            set
            {
                if (list != value)
                {
                    list = value;
                    RaisePropertyChanged("List");
                }
            }
        }

        public PackIconKind IconKind
        {
            get { return iconKind; }
            set
            {
                if (iconKind != value)
                {
                    iconKind = value;
                    RaisePropertyChanged("IconKind");
                }
            }
        }

        public string FollowButtonContent
        {
            get { return followButtonContent; }
            set
            {
                if (followButtonContent != value)
                {
                    followButtonContent = value;
                    RaisePropertyChanged("FollowButtonContent");
                }
            }
        }
    }
}
