using System;
using BookShop.model.data;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BookShop.model
{
    public class DataContext
    {
        public List<Client> Clients { get; private set; } = new List<Client>();
        public Dictionary<Guid, Book> Books { get; private set; } = new Dictionary<Guid, Book>();
        public ObservableCollection<Event> Events { get; private set; } = new ObservableCollection<Event>();
        public ObservableCollection<BookExample> BookExamples { get; private set; } = new ObservableCollection<BookExample>();
    }
}
