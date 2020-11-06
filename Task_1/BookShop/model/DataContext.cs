using System;
using BookShop.model.data;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BookShop.model
{
    public class DataContext
    {
        private List<Client> readers;
        private Dictionary<Guid, Book> books;
        private ObservableCollection<Purchace> purchaces;
        private ObservableCollection<BookExample> bookExamples;
    }
}
