using BookShop.model;
using BookShop.model.data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.logic
{
    class DataService : IDataService
    {
        private IDataRepository dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public void AddBook(string title, string author, Guid isbn)
        {
            dataRepository.AddBook(new Book(title, author, isbn));
        }

        public void AddBookExample(Book book, int tax, double basePrice)
        {
            dataRepository.AddBookExample(new BookExample(book, tax, basePrice));
        }

        public void CreateClient(string firstName, string lastName, int age)
        {
            dataRepository.AddClient(new Client(firstName, lastName, age));
        }

        public void PurchaceBook(Client client, BookExample bookExample)
        {
            dataRepository.AddEvent(new Purchase(client, bookExample,DateTime.Now));
        }

    }
}
