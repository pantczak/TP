using System;
using BookShop.model.data;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BookShop.model
{
    public class DataContext
    {
        internal List<Client> clients { get; private set; } = new List<Client>();
        internal Dictionary<Guid, Book> books { get; private set; } = new Dictionary<Guid, Book>();
        internal ObservableCollection<Event> events { get; private set; } = new ObservableCollection<Event>();
        internal ObservableCollection<BookExample> bookExamples { get; private set; } = new ObservableCollection<BookExample>();
    }
}
