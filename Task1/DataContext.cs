using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Task1
{
    public class DataContext
    {
        private List<Client> readers = new List<Client>();
        private Dictionary<Guid, Book> books = new Dictionary<Guid, Book>();
        private ObservableCollection<Purchace> purchaces = new ObservableCollection<Purchace>();
        private ObservableCollection<BookExample> bookExamples = new ObservableCollection<BookExample>();
    }
}
