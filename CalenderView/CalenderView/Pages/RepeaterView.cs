using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Xamarin.Forms;

namespace CalenderView.Pages
{
	public class RepeaterView : StackLayout
	{
        public RepeaterView()
        {
            this.Orientation = StackOrientation.Horizontal;
        }
        /// <summary>
        /// Definition for <see cref="ItemTemplate"/>.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
			nameof(ItemTemplate),
			typeof(DataTemplate),
			typeof(RepeaterView),
			default(DataTemplate));

		/// <summary>
		/// Definition for <see cref="ItemsSource"/>.
		/// </summary>
		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
			nameof(ItemsSource),
			typeof(IEnumerable),
			typeof(RepeaterView),
			default(IEnumerable),
			BindingMode.OneWay,
			propertyChanged: ItemsChanged);

		/// <summary>
		/// The item source property.
		/// </summary>
		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		/// <summary>
		/// The item template property.
		/// </summary>
		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		/// <summary>
		/// Create new child views for each new item.
		/// </summary>
		/// <param name="bindable">The repeater.</param>
		/// <param name="oldValue">Previous bound object.</param>
		/// <param name="newValue">New bound object.</param>
		private static void ItemsChanged(BindableObject bindable, object oldValue, object newValue)
		{
			// Get the repeater control
			var repeater = bindable as RepeaterView;
			if (repeater == null)
				throw new Exception($"Invalid bindable object passed to RepeaterView::ItemsChanged expected a RepeaterView received a {bindable.GetType().Name}");

			// If the item source implements INotifyCollectionChanged, setup event handler
			var itemSource = repeater.ItemsSource;
			if (itemSource == null) return;
			if (itemSource is INotifyCollectionChanged)
			{
				var collection = itemSource as INotifyCollectionChanged;
				collection.CollectionChanged += repeater.CollectionOnCollectionChanged;
			}

			// Get the data template
			var dataTemplate = repeater.ItemTemplate;
			if (dataTemplate == null) return;

			// Make sure the IEnumerable is valid
			if (newValue == null) return;

			// Create the child views of the repeater
			repeater.Children.Clear();
			foreach (var viewModel in (IEnumerable)newValue)
			{
				var content = dataTemplate.CreateContent();
				if (!(content is View) && !(content is ViewCell))
				{
					throw new Exception($"Invalid visual object {nameof(content)}");
				}

				var view = (content is View) ? content as View : ((ViewCell)content).View;
				view.BindingContext = viewModel;

				repeater.Children.Add(view);
			}
		}

		/// <summary>
		/// Event handler for the INotifyCollectionChanged.CollectionChanged event.
		/// </summary>
		/// <param name="sender">The sender(this).</param>
		/// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
		private void CollectionOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			// Remove child view for the item removed from the collection
			if (e.OldItems != null)
			{
				Children.RemoveAt(e.OldStartingIndex);
				UpdateChildrenLayout();
				InvalidateLayout();
			}

			// Add new child view for the new item in the collection
			if (e.NewItems != null)
			{
				foreach (var viewModel in e.NewItems)
				{
					var content = ItemTemplate.CreateContent();
					if (!(content is View) && !(content is ViewCell))
					{
						throw new Exception($"Invalid visual object {nameof(content)}");
					}

					var view = (content is View) ? content as View : ((ViewCell)content).View;
					view.BindingContext = viewModel;

					Children.Add(view);
				}
				UpdateChildrenLayout();
				InvalidateLayout();
			}
		}
	}
}
