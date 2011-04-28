using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace MobiTest.TestRunner
{
    public class FilteredObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {

        public ObservableCollection<T> AllItems { get; private set; }

        public Func<T, bool> FilterFunc { get; private set; }

        public FilteredObservableCollection(Func<T, bool> filterFunc)
        {
            FilterFunc = filterFunc;
            AllItems = new ObservableCollection<T>();
            AllItems.CollectionChanged += CollectionChangedHandler;
        }

        public FilteredObservableCollection(ObservableCollection<T> allItems, Func<T, bool> filterFunc)
            : this(filterFunc)
        {
            foreach (var item in allItems)
            {
                if (filterFunc(item))
                {
                    Add(item);
                }
                item.PropertyChanged += PropertyChangedHandler;
            }
            AllItems = allItems;
            AllItems.CollectionChanged += CollectionChangedHandler;
        }


        private void AddItem(T item)
        {
            Add(item);
            item.PropertyChanged += PropertyChangedHandler;
        }

        private void RemoveItem(T item)
        {
            Remove(item);
            item.PropertyChanged -= PropertyChangedHandler;
        }

        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            var item = (T)sender;
            if (FilterFunc(item))
            {
                if (!Contains(item))
                {
                    Add(item);
                }
            }
            else
            {
                if (Contains(item))
                {
                    Remove(item);
                }
            }
        }

        private void CollectionChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems.Cast<T>())
                {
                    if (FilterFunc(item))
                    {
                        AddItem(item);
                    }
                    else
                    {
                        item.PropertyChanged += PropertyChangedHandler;
                    }
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems.Cast<T>())
                {
                    RemoveItem(item);
                }
            }
        }


    }
}
