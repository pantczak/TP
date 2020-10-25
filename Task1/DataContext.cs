using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

namespace Task1
{
    public class DataContext
    {
        private List<Reader> readers;
        private Dictionary<int, Book> books;
        private ObservableCollection<Event> events;
        private List<Storage> storages;
    }
}
