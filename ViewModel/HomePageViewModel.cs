using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using We_Split_WPF.Command;
using We_Split_WPF.Model;
using We_Split_WPF.Helper;
using System.Windows.Controls;

namespace We_Split_WPF.ViewModel
{
    public class HomePageViewModel:BaseViewModel
    {
        public MainViewModel MainViewModel;
        public List<TripModel> TripsToShow { get; set; }
        public ICommand UpdateHomeView { get; set; }

        public ICommand PreviousPage { get; set; }

        public ICommand NextPage { get; set; }

        public Paging PagingVar { get; set;  }
        public int tripPerPage { get; set;  }

        private int _currentFilter;
      
                     
        public int CurrentFilter
        {
            get => _currentFilter;
            set
            {
                _currentFilter = value;
                //MessageBox.Show(CurrentFilter.Content.ToString());

                 CalculatePaging(); 
                   
            }
        }
        
        public string filterString
        {
            get
            {
                switch(CurrentFilter)
                {
                    case 0:
                        return "all";
                    case 1:
                        return "going";
                    case 2:
                        return "finished";
                    default:
                        return "all";
                }
            }
        }
      


        public void CalculatePaging()
        {
            int tripCount = DatabaseAccess.GetTripCount(filterString);
            PagingVar = new Paging { 
                CurrentPage = 1,  
                TotalPages = tripCount / tripPerPage +
                    (((tripCount % tripPerPage) == 0) ? 0 : 1),
                TripPerPage = tripPerPage
            };
            OnPropertyChanged("PagingVar");
            TripsToShow = DatabaseAccess.GetTripWithPageInfo(PagingVar.CurrentPage, PagingVar.TripPerPage,filterString);
            OnPropertyChanged("TripsToShow");
        }

        public void GoToNextPage()
        {
            
            PagingVar.CurrentPage++;
            OnPropertyChanged("PagingVar");

            TripsToShow = DatabaseAccess.GetTripWithPageInfo(PagingVar.CurrentPage, PagingVar.TripPerPage,filterString);
            OnPropertyChanged("TripsToShow"); 
        }
        public void GoToPreviousPage()
        {
           
            PagingVar.CurrentPage--;
            OnPropertyChanged("PagingVar");
            TripsToShow = DatabaseAccess.GetTripWithPageInfo(PagingVar.CurrentPage, PagingVar.TripPerPage,filterString);
            OnPropertyChanged("TripsToShow");
        }

        public ICommand UpdateTripFromHome { get; set; }
        
            //TripsToShow = DatabaseAccess.LoadAllTrips();
            //Debug.WriteLine(TripsToShow[0].ImageLink);
        public ICommand addNewButtonCommand { get; set; }
        public HomePageViewModel(MainViewModel param)
        {
            addNewButtonCommand = new RelayCommand(o => addNewButtonClick());
            MainViewModel = param;
            UpdateHomeView = new UpdateHomeViewCommand(MainViewModel);
            tripPerPage = 8;

            NextPage = new NextPageHomeCommand(this);
            PreviousPage = new PreviousPageHomeCommand(this);
            CurrentFilter = 0;

            int tripCount = DatabaseAccess.GetTripCount();
            CalculatePaging(); 
            TripsToShow = DatabaseAccess.GetTripWithPageInfo(PagingVar.CurrentPage,PagingVar.TripPerPage);

            
            

            UpdateTripFromHome = new UpdateTripFromHomeCommand(MainViewModel);
        }
        public void addNewButtonClick()
        {
            MainViewModel.SelectedViewModel = new AddNewTripPageViewModel(MainViewModel);
        }
    }
}
