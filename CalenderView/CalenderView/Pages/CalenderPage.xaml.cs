using System;
using System.Collections.Generic;
using CalenderView.ViewModels;
using Xamarin.Forms;

namespace CalenderView.Pages
{
    public partial class CalenderPage : ContentPage
    {
        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        public CalenderViewModel ViewModel
        {
            get;
            set;
        }

        public CalenderPage()
        {
            BindingContext = ViewModel = new CalenderViewModel();
            InitializeComponent();
        }
    }
}
