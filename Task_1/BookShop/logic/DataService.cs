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

        public void CreateClient(string firstName, string lastName, string pesel)
        {
            dataRepository.AddClient(new Client(firstName, lastName, pesel));
        }

        public IEnumerable<BookExample> GetAllBookExamples()
        {
            return dataRepository.GetAllBookExamples();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return dataRepository.GetAllBook();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return dataRepository.GetAllClient();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return dataRepository.GetAllEvent();
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            IEnumerable<Event> eventList= dataRepository.GetAllEvent();
            List<Purchase> purchaseList = new List<Purchase>();
            foreach (Event evnt in eventList)
            {
                if(evnt is Purchase)
                {
                    purchaseList.Add((Purchase)evnt);
                }
            }
            return purchaseList;
        }

        public Book GetBookByIsbn(Guid isbn)
        {
            return dataRepository.GetBook(isbn);

        }

        public IEnumerable<BookExample> GetBookExamplesByBook(Book book)
        {
            IEnumerable<BookExample> bookExamples = dataRepository.GetAllBookExamples();
            List<BookExample> foundBookExamples = new List<BookExample>();
            foreach (BookExample bookExample in bookExamples)
            {
                if (bookExample.Book == book)
                {
                    foundBookExamples.Add(bookExample);
                }
            }
            return foundBookExamples;
        }

        public IEnumerable<BookExample> GetBookExamplesInPriceRange(double priceMin, double priceMax)
        {
            IEnumerable<BookExample> bookExamples = dataRepository.GetAllBookExamples();
            List<BookExample> foundBookExamples = new List<BookExample>();
            foreach (BookExample bookExample in bookExamples)
            {
                if (bookExample.Price >=priceMax && bookExample.Price <= priceMax)
                {
                    foundBookExamples.Add(bookExample);
                }
            }
            return foundBookExamples;
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            IEnumerable<Book> books = dataRepository.GetAllBook();
            List<Book> foundBooks= new List<Book>();
            foreach (Book book in books)
            {
                if (book.Author == author)
                {
                    foundBooks.Add(book);
                }
            }
            return foundBooks;
        }

        public IEnumerable<Book> GetBooksByTitle(string title)
        {
            IEnumerable<Book> books = dataRepository.GetAllBook();
            List<Book> foundBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (book.Title == title)
                {
                    foundBooks.Add(book);
                }
            }
            return foundBooks;
        }

        public IEnumerable<Client> GetClientsByFirstLetterOfName(char letter)
        {
            IEnumerable<Client> clients = dataRepository.GetAllClient();
            List<Client> foundClients = new List<Client>();
            foreach (Client client in clients)
            {
                if (client.LastName[0]==letter)
                {
                    foundClients.Add(client);
                }
            }
            return foundClients;
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
