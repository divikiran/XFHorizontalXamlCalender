using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CalenderView.Models;
using Xamarin.Forms;

namespace CalenderView.ViewModels
{
    public class CalenderViewModel : BaseViewModel
    {
        ObservableCollection<DisplayModel> _dates = new ObservableCollection<DisplayModel>();
        public ObservableCollection<DisplayModel> Dates
        {
            get
            {
                return _dates;
            }

            set
            {
                _dates = value;
                RaisePropertyChanged();
            }
        }
        DateTime selectedDate = DateTime.Now;

        public DateTime SelectedDate
        {
            get
            {
                return selectedDate;
            }

            set
            {
                selectedDate = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(PageTitle));
                LoadMonth();
            }
        }

        public string PageTitle
        {
            get
            {
                return SelectedDate.ToMonthName();
            }
        }

        public ICommand DateTapped
        {
            get {

                return new Command(DateTappedAction);

            }
        }

        ObservableCollection<string> selectedDateHistory = new ObservableCollection<string>();
        public ObservableCollection<string> SelectedDateHistory
        {
            get
            {
                return selectedDateHistory;
            }

            set
            {
                selectedDateHistory = value;
                RaisePropertyChanged();
            }
        }

        private void DateTappedAction(object obj)
        {
            var tappedDate = (DateTime)obj;
            if (tappedDate != null)
            {
                SelectedDateHistory.Add(tappedDate.ToString("D"));
            }
        }

        public CalenderViewModel()
        {
            LoadMonth();
        }

        private void LoadMonth()
        {
            int days = DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month);
            Dates = new ObservableCollection<DisplayModel>();
            for (int i = 1; i <= days; i++)
            {
                Dates.Add(new DisplayModel(new DateTime(SelectedDate.Year,SelectedDate.Month,i)));
            }
        }

        public ICommand ScrolledNow
        {
            get{
                return new Command(ScrolledNowAction);
            }
        }

        private void ScrolledNowAction(object obj)
        {
            
        }
    }
}
