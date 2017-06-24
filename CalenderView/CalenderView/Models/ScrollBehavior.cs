using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalenderView.Models
{
    public class ScrollBehavior : Behavior<ScrollView>
    {
		//public static BindableProperty ScrolledCommandProperty =
		//BindableProperty.Create<ScrollBehavior, ICommand>(x => x.ItemScrolledCommand, null);

		public static BindableProperty ScrolledCommandProperty =
            BindableProperty.Create("ItemScrolledCommand", typeof(ICommand), typeof(ScrollBehavior));

		public ICommand ItemScrolledCommand
		{
			get { return (ICommand)this.GetValue(ScrolledCommandProperty); }
			set { this.SetValue(ScrolledCommandProperty, value); }
		}


		protected override void OnAttachedTo(BindableObject bindable)
        {
            base.OnAttachedTo(bindable);

            var scrollView = bindable as ScrollView;
            scrollView.Scrolled += ScrollView_Scrolled;
        }

        void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            ScrollView scrollView = sender as ScrollView;
			double scrollingSpace = scrollView.ContentSize.Width - scrollView.Width;

			if ((int)scrollingSpace == (int)e.ScrollX) // Touched bottom
			{
                //this.ItemScrolledCommand.Execute(null);
			}
        }
    }
}
