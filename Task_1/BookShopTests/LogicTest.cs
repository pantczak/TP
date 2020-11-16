using BookShop.Logic;
using BookShop.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookShopTests
{
    [TestClass]
    public class LogicTest
    {
        class TestRepository : IDataRepository
        {
            public List<Client> clients = new List<Client>();
            public Dictionary<Guid, Book> books = new Dictionary<Guid, Book>();
            public ObservableCollection<Event> events = new ObservableCollection<Event>();
            public ObservableCollection<BookExample> bookExamples = new ObservableCollection<BookExample>();

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
        public void AddBookTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            dataService.AddBook("Pan Tadeusz", "Adam M", Guid.NewGuid());
            Assert.AreEqual(1, testRepository.books.Count);
        }

        [TestMethod()]
        public void AddBookExampleTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBookExample(book, 23, 60);
            Assert.AreEqual(1, testRepository.bookExamples.Count);
        }

        [TestMethod()]
        public void CreateClientTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            dataService.CreateClient("Adam", "Malysz", 66);
            Assert.AreEqual(1, testRepository.clients.Count);
        }

        [TestMethod()]
        public void GetAllBookExamplesTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            List<BookExample> books = new List<BookExample>
            {
                new BookExample(book, 23, 60),
                new BookExample(book, 22, 60)
            };
            dataService.AddBookExample(book, 23, 60);
            dataService.AddBookExample(book, 22, 60);
            CollectionAssert.AreEqual(books, (System.Collections.ICollection)dataService.GetAllBookExamples());
        }

        [TestMethod()]
        public void GetAllBooksTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            List<Book> books = new List<Book>
            {
                new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143")),
                new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"))
            };
            dataService.AddBook("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBook("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            CollectionAssert.AreEqual(books, (System.Collections.ICollection)dataService.GetAllBooks());
        }


        [TestMethod()]
        public void GetAllClientsTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            List<Client> clients = new List<Client>
            {
                new Client("Adam", "Malysz", 66),
                new Client("Robert", "Malysz", 66),
            };
            dataService.CreateClient("Adam", "Malysz", 66);
            dataService.CreateClient("Robert", "Malysz", 66);
            CollectionAssert.AreEqual(clients, (System.Collections.ICollection)dataService.GetAllClients());
        }

        [TestMethod()]
        public void GetAllEventsTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            List<Event> events = new List<Event>
            {
                new Purchase( new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60),DateTime.Now)
            };
            dataService.PurchaceBook(new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60));
            Assert.AreEqual(events.Count, new List<Event>(dataService.GetAllEvents()).Count);
        }

        [TestMethod()]
        public void GetAllPurchasesTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.PurchaceBook(new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60));
            dataService.ReturnBook(new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60), new Purchase(new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60), DateTime.Now));
            Assert.AreEqual(1, new List<Event>(dataService.GetAllPurchases()).Count);
        }

        [TestMethod()]
        public void GetBookByIsbnTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            List<Book> books = new List<Book>
            {
                new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143")),
                new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"))
            };
            dataService.AddBook("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBook("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Assert.AreEqual(books[0], dataService.GetBookByIsbn(Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143")));
        }

        [TestMethod()]
        public void GetBookExamplesByBookTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Book book1 = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBookExample(book, 23, 60);
            dataService.AddBookExample(book, 22, 60);
            dataService.AddBookExample(book1, 22, 60);
            Assert.AreEqual(2, new List<BookExample>(dataService.GetBookExamplesByBook(book)).Count);
        }

        [TestMethod()]
        public void GetBookExamplesInPriceRangeTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Book book1 = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBookExample(book, 10, 100);
            dataService.AddBookExample(book, 10, 200);
            dataService.AddBookExample(book1, 10, 300);
            Assert.AreEqual(2, new List<BookExample>(dataService.GetBookExamplesInPriceRange(110,300)).Count);
        }

        [TestMethod()]
        public void GetBooksByAuthorTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            dataService.AddBook("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBook("Pan Tadeusz1", "Adam K", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBook("Pan Tadeusz1", "Adam M", Guid.Parse("51D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Assert.AreEqual(2, new List<Book>(dataService.GetBooksByAuthor("Adam M")).Count);
        }

        [TestMethod()]
        public void GetBooksByTitleTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            dataService.AddBook("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBook("Pan Tadeusz1", "Adam K", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.AddBook("Pan Tadeusz1", "Adam M", Guid.Parse("51D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Assert.AreEqual(2, new List<Book>(dataService.GetBooksByTitle("Pan Tadeusz1")).Count);
        }

        [TestMethod()]
        public void GetClientsByAgeRangeTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            dataService.CreateClient("Adam", "Malysz", 65);
            dataService.CreateClient("Robert", "Malysz", 60);
            dataService.CreateClient("Jakub", "Malysz", 70);
            Assert.AreEqual(2, new List<Client>(dataService.GetClientsByAgeRange(63,71)).Count);
        }

        [TestMethod()]
        public void GetClientsByFirstLetterOfNameTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            dataService.CreateClient("Rubert", "Malysz", 65);
            dataService.CreateClient("Robert", "Kubica", 60);
            dataService.CreateClient("Jakub", "Malysz", 70);
            Assert.AreEqual(2, new List<Client>(dataService.GetClientsByFirstLetterOfName('M')).Count);
        }

        [TestMethod()]
        public void GetEventsByBookExampleTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Book book1 = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            BookExample bookExample = new BookExample(book, 23, 60);
            dataService.PurchaceBook(new Client("Adam", "Malysz", 66), bookExample);
            dataService.PurchaceBook(new Client("Adam", "Malysz", 66), new BookExample(book1, 23, 60));
            dataService.PurchaceBook(new Client("Pan", "Malysz", 66), bookExample);
            Assert.AreEqual(2, new List<Event>(dataService.GetEventsByBookExample(bookExample)).Count);
        }

        [TestMethod()]
        public void GetEventsByClientTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Book book1 = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            BookExample bookExample = new BookExample(book, 23, 60);
            Client client = new Client("Adam", "Malysz", 66);
            dataService.PurchaceBook(client, bookExample);
            dataService.PurchaceBook(client, new BookExample(book1, 23, 60));
            dataService.PurchaceBook(new Client("Pan", "Malysz", 66), bookExample);
            Assert.AreEqual(2, new List<Event>(dataService.GetEventsByClient(client)).Count);
        }

        [TestMethod()]
        public void GetPurchasesInDateRangeTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Book book1 = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            BookExample bookExample = new BookExample(book, 23, 60);
            Client client = new Client("Adam", "Malysz", 66);
            dataService.PurchaceBook(client, bookExample);
            dataService.PurchaceBook(client, new BookExample(book1, 23, 60));
            dataService.PurchaceBook(new Client("Pan", "Malysz", 66), bookExample);
            Assert.AreEqual(3, new List<Event>(dataService.GetPurchasesInDateRange(DateTime.Parse("1/1/2019"),DateTime.Now)).Count);
            Assert.AreEqual(0, new List<Event>(dataService.GetPurchasesInDateRange(DateTime.Parse("1/1/2019"), DateTime.Parse("1/1/2019"))).Count);
        }

        [TestMethod()]
        public void ModifyBookTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));

            dataService.AddBook("Pan Tadeusz", "Adam M", Guid.Parse("53D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.ModifyBook(book);
            Assert.AreEqual(book, dataService.GetBookByIsbn(book.Isbn));
        }

        [TestMethod()]
        public void ModifyBookExampleTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            BookExample bookExample = new BookExample(book, 23, 60);
            BookExample bookExampleOld = new BookExample(book, 20, 100);
            dataService.AddBookExample(book, 20, 100);
            dataService.ModifyBookExample(bookExampleOld,bookExample);
            Assert.AreEqual(bookExample, new List<BookExample>(dataService.GetBookExamplesByBook(book))[0]);
        }

        [TestMethod()]
        public void ModifyClientTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Client client = new Client("Adam", "Malysz", 66);
            Client clientOld = new Client("Robert", "K", 66);
            dataService.CreateClient("Robert", "K", 66);
            dataService.ModifyClient(clientOld, client);
            Assert.AreEqual(client, new List<Client>(dataService.GetClientsByFirstLetterOfName('M'))[0]);
        }

        [TestMethod()]
        public void ModifyEventTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Purchase purchase = new Purchase(new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60), DateTime.Parse("1/1/2001"));
            Purchase purchaseOld = new Purchase(new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60), DateTime.Parse("1/1/2001"));
            testRepository.events.Add(purchaseOld);
            dataService.ModifyEvent(purchaseOld,purchase);
            Assert.AreEqual(purchase, testRepository.events[0]);
        }

        [TestMethod()]
        public void PurchaceBookTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            dataService.PurchaceBook(new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60));
            Assert.AreEqual(1, testRepository.events.Count);
        }

        [TestMethod()]
        public void RemoveBookTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            testRepository.AddBook(book);
            dataService.RemoveBook(book);
            Assert.AreEqual(0, testRepository.books.Count);
        }

        [TestMethod()]
        public void RemoveBookExampleTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            BookExample bookExample = new BookExample(book, 23, 60);
            testRepository.AddBookExample(bookExample);
            dataService.RemoveBookExample(bookExample);
            Assert.AreEqual(0, testRepository.bookExamples.Count);
        }

        [TestMethod()]
        public void RemoveClientTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Client client = new Client("Adam", "Malysz", 66);
            testRepository.clients.Add(client);
            dataService.RemoveClient(client);
            Assert.AreEqual(0, testRepository.clients.Count);
        }

        [TestMethod()]
        public void RemoveEventTest()
        {
            TestRepository testRepository = new TestRepository();
            DataService dataService = new DataService(testRepository);
            Book book = new Book("Pan Tadeusz1", "Adam M", Guid.Parse("52D2DA0E-22C1-4A0E-BF60-96859EB5A143"));
            Purchase purchase = new Purchase(new Client("Adam", "Malysz", 66), new BookExample(book, 23, 60), DateTime.Parse("1/1/2001"));
            testRepository.events.Add(purchase);
            dataService.RemoveEvent(purchase);
            Assert.AreEqual(0, testRepository.events.Count);
        }
    }
}
