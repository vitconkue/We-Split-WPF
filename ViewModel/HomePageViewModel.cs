﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using We_Split_WPF.Command;
using We_Split_WPF.Model;


namespace We_Split_WPF.ViewModel
{
    public class HomePageViewModel:BaseViewModel
    {
        public MainViewModel MainViewModel;
        public List<TripModel> Trips { get; set; }
        public ICommand UpdateHomeView { get; set; }
        public ICommand addNewButtonCommand { get; set; }
        public HomePageViewModel(MainViewModel param)
        {
            addNewButtonCommand = new RelayCommand(o => addNewButtonClick());
            Trips = DatabaseAccess.LoadAllTrips();
            Debug.WriteLine(Trips[0].ImageLink);
            MainViewModel = param;
            UpdateHomeView = new UpdateHomeViewCommand(MainViewModel);
        }
        public void addNewButtonClick()
        {
            MainViewModel.SelectedViewModel = new AddNewTripPageViewModel();
        }
    }
}
