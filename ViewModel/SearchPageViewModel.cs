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
        public bool IsSearchByTripNameAndPlace { get; set; }
        private ObservableCollection<TripModel> _trips;
        public ICommand Search { get; set; }

        public ICommand PreviousPage { get; set; }

        public ICommand NextPage { get; set; }

        public ICommand UpdateTripFromSearch { get; set; }
        public Paging PagingVar { get; set; }
        public int tripPerPage { get; set; }

        private MainViewModel viewModel;

        public ICommand UpdateSearchView { get; set; }
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
        public SearchPageViewModel(MainViewModel param){
            tripPerPage = 8; 
            Search = new RelayCommand(o => SearchFunction(o));
            TripsToShow = new List<TripModel>(); 
            PagingVar = new Paging { CurrentPage = 1, TotalPages = 1, TripPerPage = tripPerPage };
            CurrentSearchString = "";
            NextPage = new NextPageSearchCommand(this);
            PreviousPage = new PreviousPageSearchCommand(this);
            viewModel = param;
            UpdateSearchView = new RelayCommand(o => GotoDetailPage(o));
            UpdateTripFromSearch = new RelayCommand(o => GotoUpdatePage(o));
        }

        public void CalculateChanging()
        {
            int tripCount = DatabaseAccess.SearchResultCount(CurrentSearchString, IsSearchByTripNameAndPlace);
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

            TripsToShow = DatabaseAccess.GetSearchResultWithPage(CurrentSearchString, IsSearchByTripNameAndPlace, PagingVar.CurrentPage, PagingVar.TripPerPage);
            OnPropertyChanged("TripsToShow");

        }

       
        public void SearchFunction(object o)
        {
            var patameters = (object[])o;
            string searchText = (string)patameters[0];
            bool isByTripNameandPlace = (bool)patameters[1];

            IsSearchByTripNameAndPlace = isByTripNameandPlace;
            CurrentSearchString = searchText; 
            

            // Change source  

            CalculateChanging(); 

            Debug.WriteLine("OK");
            
        }

        public void GoToNextPage()
        {

            PagingVar.CurrentPage++;
            OnPropertyChanged("PagingVar");

            TripsToShow = DatabaseAccess.GetSearchResultWithPage(CurrentSearchString, IsSearchByTripNameAndPlace, PagingVar.CurrentPage, PagingVar.TripPerPage);
            OnPropertyChanged("TripsToShow");
        }
        public void GoToPreviousPage()
        {

            PagingVar.CurrentPage--;
            OnPropertyChanged("PagingVar");
            TripsToShow = DatabaseAccess.GetSearchResultWithPage(CurrentSearchString, IsSearchByTripNameAndPlace, PagingVar.CurrentPage, PagingVar.TripPerPage);
            OnPropertyChanged("TripsToShow");
        }
        public void GotoDetailPage(object o)
        {
            viewModel.SelectedViewModel = new DetailPageViewModel(int.Parse(o.ToString()), viewModel);
        }
        public void GotoUpdatePage(object o)
        {
            viewModel.SelectedViewModel = new UpdatePageViewModel(int.Parse(o.ToString()), viewModel);
        }
    }
}
