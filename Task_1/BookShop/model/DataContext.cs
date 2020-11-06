using System;
using BookShop.model.data;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BookShop.model
{
    internal class DataContext
    {
        internal List<Client> readers { get; private set; }
        internal Dictionary<Guid, Book> books { get; private set; }
        internal ObservableCollection<Purchace> purchaces { get; private set; }
        internal ObservableCollection<BookExample> bookExamples { get; private set; }
    }
}
