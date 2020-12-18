using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using We_Split_WPF.Command;
using We_Split_WPF.Model;
using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;

namespace We_Split_WPF.ViewModel
{
    public class DetailPageViewModel : BaseViewModel
    {
        public TripModel Trip { get; set; }
        private MainViewModel viewModel;
        public ICommand UpdateTrip { get; set; }
        public ICommand AddPlaceImage { get; set; }
        public ICommand PrevClick { get; set; }
        public ICommand NextClick { get; set; }
        public ICommand EndTrip { get; set; }
        public string Status { get; set; }
        public SeriesCollection RecieveMoneyChartData { get; set; }
        public SeriesCollection ExpenseMoneyChartData { get; set; }
        private Uri _placeImageDisplay;
        private string _currentStringDisplay = "0 OF 0";
        public int test = 3;
        public string CurrentStringDisplay
        {
            get
            {
                return _currentStringDisplay;
            }
            set
            {
                _currentStringDisplay = value;
                OnPropertyChanged(nameof(CurrentStringDisplay));
            }
        }



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
        public int CurrentPlaceImage { get; set; }
        public int TotalPlaceImage { get; set; }
        public List<Uri> PlaceImages { get; set; }
        public DetailPageViewModel(int ID, MainViewModel param)
        {

            Trip = DatabaseAccess.LoadSingleTrip(ID);
            Trip.Name = Trip.Name.ToUpper();
            this.viewModel = param;
            UpdateTrip = new UpdateTripCommand(viewModel, ID);
            AddPlaceImage = new RelayCommand(o => AddPlaceImageForTrip());
            PrevClick = new RelayCommand(o => PrevButtonClick());
            NextClick = new RelayCommand(o => NextButtonClick());
            EndTrip = new RelayCommand(o => EndTripClick());
            InitPlaceImage();
            CalcRemainMoney();
            InitChart();
            if (Trip.IsFinished)
            {
                Status = "ĐÃ KẾT THÚC";
            }
            else
            {
                Status = "ĐANG ĐI";
            }

        }
        public void AddPlaceImageForTrip()
        {

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn Ảnh";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files(*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            string[] allImageSource = new string[] { };
            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    allImageSource = openFileDialog.FileNames;
                }
                string file = AppDomain.CurrentDomain.BaseDirectory;

                foreach (string source in allImageSource)
                {
                    var tokens1 = source.Split(new string[] { "\\" }, StringSplitOptions.None);
                    string filename = tokens1[tokens1.Length - 1];
                    string dest = $"{file}Data\\Images\\TripsImage\\{Trip.ID}\\Location\\";
                    var tokens2 = filename.Split(new string[] { "." }, StringSplitOptions.None);
                    string filetype = tokens2[tokens2.Length - 1];
                    filename = $"image{PlaceImages.Count() + 1}.{filetype}";
                    dest += filename;
                    PlaceImages.Add(new Uri(dest));
                    System.IO.File.Copy(source, dest, true);
                }
                PlaceImageDisplay = PlaceImages[0];
                OnPropertyChanged(nameof(PlaceImageDisplay));
                TotalPlaceImage = PlaceImages.Count();
                CurrentPlaceImage = 1;
                CurrentStringDisplay = $"1 OF {TotalPlaceImage}";
            }
            catch
            {

            }
        }
        public void PrevButtonClick()
        {
            try
            {
                if (CurrentPlaceImage > 1)
                {
                    CurrentPlaceImage--;
                    PlaceImageDisplay = PlaceImages[CurrentPlaceImage - 1];
                    CurrentStringDisplay = $"{CurrentPlaceImage} OF {TotalPlaceImage}";
                    OnPropertyChanged(nameof(PlaceImageDisplay));
                }
            }
            catch
            {

            }
        }
        public void NextButtonClick()
        {
            try
            {
                if (CurrentPlaceImage < TotalPlaceImage)
                {
                    PlaceImageDisplay = PlaceImages[CurrentPlaceImage];
                    CurrentPlaceImage++;
                    CurrentStringDisplay = $"{CurrentPlaceImage} OF {TotalPlaceImage}";
                    OnPropertyChanged(nameof(PlaceImageDisplay));
                }
            }
            catch
            {

            }
        }
        public void EndTripClick()
        {
            if (Trip.IsFinished)
            {
                MessageBoxImage errorIcon = MessageBoxImage.Error;
                MessageBox.Show("Chuyến đi này đã kết thúc!!!", "Error", MessageBoxButton.OKCancel, errorIcon);
            }
            else
            {
                Trip.ToogleIsFinished();
                ICommand BackToHomePage = new UpdateMainViewCommand(viewModel);
                var a = new DialogHost();
                a.ShowDialog((object)"test");
                MessageBoxImage icon = MessageBoxImage.Question;
                MessageBoxResult dialogResult = MessageBox.Show("Kết thúc chuyến đi này?", "Confirmation", MessageBoxButton.YesNo, icon);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    BackToHomePage.Execute((object)"HomePage");
                }
                else
                {
                    //do something else
                }
            }

        }
        private void InitPlaceImage()
        {
            try
            {
                PlaceImages = Trip.PlaceImages;
                PlaceImageDisplay = PlaceImages[0];
                TotalPlaceImage = PlaceImages.Count();
                CurrentPlaceImage = 1;
                CurrentStringDisplay = $"{CurrentPlaceImage} OF {TotalPlaceImage}";
            }
            catch
            {

            }
        }
        private void CalcRemainMoney()
        {
            foreach (var member in Trip.memberList)
            {
                member.RemainMoney = (member.MoneyPaid - Trip.SumExpenses / Trip.memberList.Count());
            }

        }
        private void InitChart()
        {
            RecieveMoneyChartData = new SeriesCollection { };
            foreach (var member in Trip.memberList)
            {
                RecieveMoneyChartData.Add(
                new PieSeries
                {
                    Title = member.Name,
                    Values = new ChartValues<int> { member.MoneyPaid },
                    DataLabels = true
                }); ;
            }

            ExpenseMoneyChartData = new SeriesCollection { };
            foreach (var expense in Trip.expensesList)
            {
                ExpenseMoneyChartData.Add(
                new PieSeries
                {
                    Title = expense.Name,
                    Values = new ChartValues<int> { expense.AmountMoney },
                    DataLabels = true
                });
            }
        }
      
    }
}