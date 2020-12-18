using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using We_Split_WPF.Model;
using We_Split_WPF.Command;
using System.Collections.ObjectModel;
using System.Windows.Input;
using We_Split_WPF.Helper;
using System.Diagnostics;

namespace We_Split_WPF.ViewModel
{
    public class SearchPageViewModel : BaseViewModel
    {
        public string CurrentSearchString { get; set; }
        public bool IsSearchByTripName { get; set; }
        private ObservableCollection<TripModel> _trips;
        public ICommand Search { get; set; }

        public ICommand PreviousPage { get; set; }

        public ICommand NextPage { get; set; }

        public Paging PagingVar { get; set; }
        public int tripPerPage { get; set; }

        public List<TripModel> TripsToShow { get; set; }
        public ObservableCollection<TripModel> Trips
        {
            get
            {
                return _trips;
            }
            set
            {
                _trips = value;
                OnPropertyChanged(nameof(Trips));
            }
        }
        public SearchPageViewModel(){
            tripPerPage = 1; 
            Search = new RelayCommand(o => SearchFunction(o));
            TripsToShow = new List<TripModel>(); 
            PagingVar = new Paging { CurrentPage = 1, TotalPages = 1, TripPerPage = tripPerPage };
            CurrentSearchString = "";
            NextPage = new NextPageSearchCommand(this);
            PreviousPage = new PreviousPageSearchCommand(this);
        }

        public void CalculateChanging()
        {
            int tripCount = DatabaseAccess.SearchResultCount(CurrentSearchString, IsSearchByTripName);
            int newTotalPage = tripCount / tripPerPage +
                    (((tripCount % tripPerPage) == 0) ? 0 : 1);
            if (newTotalPage == 0) newTotalPage = 1; 
            PagingVar = new Paging
            {
                CurrentPage = 1,
                TotalPages = newTotalPage,
                TripPerPage = tripPerPage
            };
            OnPropertyChanged("PagingVar");

            TripsToShow = DatabaseAccess.GetSearchResultWithPage(CurrentSearchString, IsSearchByTripName, PagingVar.CurrentPage, PagingVar.TripPerPage);
            OnPropertyChanged("TripsToShow");

        }

       
        public void SearchFunction(object o)
        {
            var patameters = (object[])o;
            string searchText = (string)patameters[0];
            bool isByTripName = (bool)patameters[1];

            IsSearchByTripName = isByTripName;
            CurrentSearchString = searchText; 
            

            // Change source  

            CalculateChanging(); 

            Debug.WriteLine("OK");
            
        }

        public void GoToNextPage()
        {

            PagingVar.CurrentPage++;
            OnPropertyChanged("PagingVar");

            TripsToShow = DatabaseAccess.GetSearchResultWithPage(CurrentSearchString, IsSearchByTripName, PagingVar.CurrentPage, PagingVar.TripPerPage);
            OnPropertyChanged("TripsToShow");
        }
        public void GoToPreviousPage()
        {

            PagingVar.CurrentPage--;
            OnPropertyChanged("PagingVar");
            TripsToShow = DatabaseAccess.GetSearchResultWithPage(CurrentSearchString, IsSearchByTripName, PagingVar.CurrentPage, PagingVar.TripPerPage);
            OnPropertyChanged("TripsToShow");
        }
    }
}
