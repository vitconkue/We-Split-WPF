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
      


        public void RecalculatePaging()
        {
            // recalcute with certain info: filter
        }

        public void GoToNextPage()
        {
            PagingVar.CurrentPage++;
            OnPropertyChanged("PagingVar");

            TripsToShow = DatabaseAccess.GetTripWithPageInfo(PagingVar.CurrentPage, PagingVar.TripPerPage);
            OnPropertyChanged("TripsToShow"); 
        }
        public void GoToPreviousPage()
        {
            PagingVar.CurrentPage--;
            OnPropertyChanged("PagingVar");
            TripsToShow = DatabaseAccess.GetTripWithPageInfo(PagingVar.CurrentPage, PagingVar.TripPerPage);
            OnPropertyChanged("TripsToShow");
        }

        public ICommand UpdateTripFromHome { get; set; }
        public HomePageViewModel(MainViewModel param)
        {
            //TripsToShow = DatabaseAccess.LoadAllTrips();
            //Debug.WriteLine(TripsToShow[0].ImageLink);
            MainViewModel = param;
            UpdateHomeView = new UpdateHomeViewCommand(MainViewModel);
            tripPerPage = 1;

            NextPage = new NextPageHomeCommand(this);
            PreviousPage = new PreviousPageHomeCommand(this); 

            int tripCount = DatabaseAccess.GetTotalTripCount();
            PagingVar = new Paging {
                CurrentPage = 1,
                TotalPages = tripCount / tripPerPage +
                    (((tripCount % tripPerPage) == 0) ? 0 : 1),
                TripPerPage = tripPerPage 
                
            };
            TripsToShow = DatabaseAccess.GetTripWithPageInfo(PagingVar.CurrentPage,PagingVar.TripPerPage);



            UpdateTripFromHome = new UpdateTripFromHomeCommand(MainViewModel);
        }

    }
}
