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

        public IEnumerable<BookExample> GetAllBookExamples()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            throw new NotImplementedException();
        }

        public Book GetBookByIsbn(Guid isbn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookExample> GetBookExamplesByBook(Book book)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookExample> GetBookExamplesInPriceRange(double priceMin, double priceMax)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooksByName(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetClientsByFirstLetterOfName(char letter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetClientsByPesel(string pesel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEventsByBookExample(BookExample bookExample)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetEventsByClient(Client client)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetPurchasesInDateRange(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public void ModifyBook(Book oldBook, Book newBook)
        {
            throw new NotImplementedException();
        }

        public void ModifyBookExample(BookExample oldBookExample, BookExample newBookExample)
        {
            throw new NotImplementedException();
        }

        public void ModifyClient(Client oldClient, Client newClient)
        {
            throw new NotImplementedException();
        }

        public void ModifyPurchase(Purchase oldPurchase, Purchase newPurchase)
        {
            throw new NotImplementedException();
        }

        public void PurchaceBook(Client client, BookExample bookExample)
        {
            dataRepository.AddEvent(new Purchase(client, bookExample,DateTime.Now));
        }

        public void RemoveBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void RemoveBookExample(BookExample bookExample)
        {
            throw new NotImplementedException();
        }

        public void RemoveClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void RemoveEvent(Event evnt)
        {
            throw new NotImplementedException();
        }
    }
}
