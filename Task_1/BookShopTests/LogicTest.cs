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
            Assert.Fail();
        }

        [TestMethod()]
        public void AddBookTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddBookExampleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreateClientTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllBookExamplesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllBooksTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllClientsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllEventsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllPurchasesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBookByIsbnTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBookExamplesByBookTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBookExamplesInPriceRangeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBooksByAuthorTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetBooksByTitleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetClientsByAgeRangeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetClientsByFirstLetterOfNameTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetEventsByBookExampleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetEventsByClientTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetPurchasesInDateRangeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifyBookTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifyBookExampleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifyClientTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModifyEventTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PurchaceBookTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveBookTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveBookExampleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveClientTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveEventTest()
        {
            Assert.Fail();
        }
    }
}
