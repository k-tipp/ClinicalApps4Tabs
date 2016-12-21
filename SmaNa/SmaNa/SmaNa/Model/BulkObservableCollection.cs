
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace SmaNa.Model
{
    /// <summary>
    /// This class allows to operate many items within the list without causing a Notify-Event every time you change an item.
    /// Copied and modified from https://forums.xamarin.com/discussion/29925/observablecollection-addrange
    /// @Created: Marwin Philips
    /// </summary>
    /// <typeparam name="T">Type handled in the list</typeparam>
    /// 
    public class BulkObservableCollection<T> : ObservableCollection<T>
    {
        public BulkObservableCollection()  : base()   { }

        public BulkObservableCollection(IEnumerable<T> collection)  : base(collection)   { }

        public BulkObservableCollection(List<T> list)   : base(list)  { }

        /// <summary>
        /// Adds all elements in range to the List and fires a PropertyChangedEvent after editing the list.
        /// </summary>
        /// <param name="range">Elements to add to the list</param>
        public void AddRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Items.Add(item);
            }

            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        /// <summary>
        /// Removes all elements in range to the List and fires a PropertyChangedEvent after editing the list.
        /// </summary>
        /// <param name="range"></param>
        public void RemoveRange(IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                Items.Remove(item);
            }

            this.OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
