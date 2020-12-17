﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using We_Split_WPF.Command;
using We_Split_WPF.Model;
namespace We_Split_WPF.ViewModel
{
    public class DetailPageViewModel : BaseViewModel
    {
        public TripModel Trip { get; set; }
        private MainViewModel viewModel;
        public ICommand UpdateTrip { get; set; }
        private Uri _placeImageDisplay;
        private string _currentPlaceImage = "1";
        private string _totalPlaceImage;
        public Uri PlaceImageDisplay
        {
            get
            {
                return _placeImageDisplay;
            }
            set
            {
                _placeImageDisplay = value;
                OnPropertyChanged(nameof(PlaceImageDisplay));
            }
        }
        public string CurrentPlaceImage
        {
            get
            {
                return _currentPlaceImage;
            }
            set
            {
                _currentPlaceImage = value;
                OnPropertyChanged(nameof(CurrentPlaceImage));
            }
            }
        public string TotalPlaceImage
        {
            get
            {
                return _totalPlaceImage;
            }
            set
            { 
                _totalPlaceImage = value;
                OnPropertyChanged(nameof(TotalPlaceImage));
            }
        }
        public List<Uri> PlaceImages { get; set; }
        public DetailPageViewModel(int ID,MainViewModel param)
        {
            Trip = DatabaseAccess.LoadSingleTrip(ID);
            Trip.Name = Trip.Name.ToUpper();
            this.viewModel = param;
            UpdateTrip = new UpdateTripCommand(viewModel,ID);
            PlaceImages = Trip.PlaceImages;
            PlaceImageDisplay = PlaceImages[0];
   
        }
    }
}