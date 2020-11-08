using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookShop.logic;
using BookShop.model.data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BookShop.model;

namespace BookShopTests
{
    [TestClass]
    public class LogicTest
    {

        class TestRepository : IDataRepository
        {
            internal List<Client> clients { get; private set; } = new List<Client>();
            internal Dictionary<Guid, Book> books { get; private set; } = new Dictionary<Guid, Book>();
            internal ObservableCollection<Event> events { get; private set; } = new ObservableCollection<Event>();
            internal ObservableCollection<BookExample> bookExamples { get; private set; } = new ObservableCollection<BookExample>();

            public void AddBook(Book book)
            {
                books.Add(book.Isbn, book);
            }

            public void AddBookExample(BookExample bookExample)
            {
                bookExamples.Add(bookExample);
            }

            public void AddClient(Client client)
            {
                clients.Add(client);
            }

            public void AddEvent(Event evnt)
            {
                events.Add(evnt);
            }

            public void DeleteBook(Book book)
            {
                books.Remove(book.Isbn);
            }

            public void DeleteBookExample(BookExample bookExample)
            {
                bookExamples.Remove(bookExample);
            }

            public void DeleteClient(Client client)
            {
                clients.Remove(client);
            }

            public void DeleteEvent(Event evnt)
            {
                events.Remove(evnt);
            }

            public IEnumerable<Book> GetAllBook()
            {
                return books.Values;
            }

            public IEnumerable<BookExample> GetAllBookExamples()
            {
                return bookExamples;
            }

            public IEnumerable<Client> GetAllClient()
            {
                return clients;
            }

            public IEnumerable<Event> GetAllEvent()
            {
                return events;
            }

            public Book GetBook(Guid Isbn)
            {
                return books[Isbn];
            }

            public BookExample GetBookExample(int id)
            {
                return bookExamples[id];
            }

            public Client GetClient(int id)
            {
                return clients[id];
            }

            public Event GetEvent(int id)
            {
                return events[id];
            }

            public void UpdateBook(Book book)
            {
                books.Remove(book.Isbn);
                books.Add(book.Isbn, book);
            }

            public void UpdateBookExample(int Id, BookExample bookExample)
            {
                bookExamples.Remove(GetBookExample(Id));
                bookExamples.Insert(Id, bookExample);

            }

            public void UpdateClient(int id, Client client)
            {
                clients.Remove(GetClient(id));
                clients.Insert(id, client);
            }

            public void UpdateEvent(int id, Event evnt)
            {
                events.Remove(GetEvent(id));
                events.Insert(id, evnt);
            }
        }
        [TestMethod()]
        public void DataServiceTest()
        {
        }

        [TestMethod()]
        public void AddBookTest()
        {
        }

        [TestMethod()]
        public void AddBookExampleTest()
        {
        }

        [TestMethod()]
        public void CreateClientTest()
        {
        }

        [TestMethod()]
        public void GetAllBookExamplesTest()
        {
        }

        [TestMethod()]
        public void GetAllBooksTest()
        {
        }

        [TestMethod()]
        public void GetAllClientsTest()
        {
        }

        [TestMethod()]
        public void GetAllEventsTest()
        {
        }

        [TestMethod()]
        public void GetAllPurchasesTest()
        {
        }

        [TestMethod()]
        public void GetBookByIsbnTest()
        {
        }

        [TestMethod()]
        public void GetBookExamplesByBookTest()
        {
        }

        [TestMethod()]
        public void GetBookExamplesInPriceRangeTest()
        {
        }

        [TestMethod()]
        public void GetBooksByAuthorTest()
        {
        }

        [TestMethod()]
        public void GetBooksByTitleTest()
        {
        }

        [TestMethod()]
        public void GetClientsByAgeRangeTest()
        {
        }

        [TestMethod()]
        public void GetClientsByFirstLetterOfNameTest()
        {
        }

        [TestMethod()]
        public void GetEventsByBookExampleTest()
        {
        }

        [TestMethod()]
        public void GetEventsByClientTest()
        {
        }

        [TestMethod()]
        public void GetPurchasesInDateRangeTest()
        {
        }

        [TestMethod()]
        public void ModifyBookTest()
        {
        }

        [TestMethod()]
        public void ModifyBookExampleTest()
        {
        }

        [TestMethod()]
        public void ModifyClientTest()
        {
        }

        [TestMethod()]
        public void ModifyEventTest()
        {
        }

        [TestMethod()]
        public void PurchaceBookTest()
        {
        }

        [TestMethod()]
        public void RemoveBookTest()
        {
        }

        [TestMethod()]
        public void RemoveBookExampleTest()
        {
        }

        [TestMethod()]
        public void RemoveClientTest()
        {
        }

        [TestMethod()]
        public void RemoveEventTest()
        {
        }
    }
}
