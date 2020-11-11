using System;
using System.Collections.Generic;
using System.Linq;
using BookShop.model;
using BookShop.model.data;
using BookShop.model.filler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookShopTests
{
    [TestClass]
    public class ModelTest
    {
        private List<Book> booksList;

        private List<Client> clientList;

        private List<BookExample> bookExampleList;

        private List<Event> purchaceList;
        

        [TestInitialize]
        public void FillRepository()
        {
           booksList = new List<Book>()
            {
            new Book("Hobbit", "J.R.R. Tolkien", Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")),
            new Book("Mistborn", "Brandon Sanderson", Guid.Parse("BD8A1680-65BD-44A6-92C0-88827A3B473A")),
            new Book("The Lord of the Rings", "J.R.R. Tolkien", Guid.Parse("10957692-E446-4577-B8C7-8B41FF7C17B8")),
            new Book("Harry Potter", "J.K. Rowling", Guid.Parse("A5CC4A82-7498-4A10-8FB8-973954335474")),
            new Book("Metro 2033", "Dmitrij Głuchowski", Guid.Parse("86167D69-FA4D-42CC-8D08-9972F2F04EF5"))
            };

             clientList = new List<Client>()
            {
                new Client("Adam", "Kowalski", 39),
                new Client("Jan", "Nowak",34)
            };

             bookExampleList = new List<BookExample>()
            {
            new BookExample(booksList[0], 23, 59.99),
            new BookExample(booksList[0], 23, 49.99),
            new BookExample(booksList[1], 8, 29.99),
            new BookExample(booksList[1], 8, 69.99),
            new BookExample(booksList[1], 23, 129.99),
            new BookExample(booksList[2], 3, 19.99),
            new BookExample(booksList[2], 23, 9.99),
            new BookExample(booksList[3], 8, 111.99),
            new BookExample(booksList[3], 23, 22.99),
            new BookExample(booksList[4], 23, 55.60),
            };

            purchaceList = new List<Event>()
            {
            new Purchase(clientList[0], bookExampleList[0], DateTime.Parse("5/10/2020 21:11:00")),
            new Purchase(clientList[1], bookExampleList[7], DateTime.Parse("3/5/2019 11:30:00")),
            new Purchase(clientList[0], bookExampleList[0], DateTime.Parse("11/1/2011 11:59:00")),
            };
        }



        [TestMethod]
        public void BookTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            Assert.AreEqual(guid, book.Isbn);
            Assert.AreEqual("Hobbit", book.Title);
            Assert.AreEqual("J. R. R. Tolkien", book.Author);
        }

        [TestMethod]
        public void ClientTest()
        {
            Client client = new Client("Adam", "Tomczak", 39);
            Assert.AreEqual(39, client.Age);
            Assert.AreEqual("Adam", client.FirstName);
            Assert.AreEqual("Tomczak", client.LastName);
        }


        [TestMethod]
        public void BookExampleTest()
        {
            Guid guid = Guid.NewGuid();
            Guid guid1 = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid1);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Assert.AreEqual(book, bookExample.Book);
            Assert.AreEqual(23, bookExample.Tax);
            Assert.AreEqual(69.99, bookExample.BasePrice);
            Assert.AreEqual(69.99 * 23 / 100.0, bookExample.Price, 0.00001);
        }

        [TestMethod]
        public void PurchaceTest()
        {
            Guid guid = Guid.NewGuid();
            DateTime dateTime = DateTime.Now;
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", 39);
            Purchase purchace = new Purchase(client, bookExample, dateTime);
            Assert.AreEqual(bookExample, purchace.BookExample);
            Assert.AreEqual(client, purchace.Client);
            Assert.AreEqual(dateTime, purchace.EventTime);
        }

        [TestMethod]
        public void DataRepositoryAddBookTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);


            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList,booksList,purchaceList,bookExampleList));
            int lastFilledBookIndex = dataRepository.GetAllBook().Count() - 1;

            dataRepository.AddBook(book);
            Assert.AreEqual(book, dataRepository.GetBook(book.Isbn));
        }

        [TestMethod]
        public void DataRepositoryAddBookExampleTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            int lastFilledBookExampleIndex = dataRepository.GetAllBookExamples().Count() - 1;
            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            Assert.AreEqual(bookExample, dataRepository.GetBookExample(lastFilledBookExampleIndex + 1));
        }

        [TestMethod]
        public void DataRepositoryAddClientTest()
        {
            Client client = new Client("Adam", "Tomczak", 39);
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            int lastFilledClientIndex = dataRepository.GetAllClient().Count() - 1;

            dataRepository.AddClient(client);
            Assert.AreEqual(client, dataRepository.GetClient(lastFilledClientIndex + 1));
        }

        [TestMethod]
        public void DataRepositoryAddEventTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", 39);
            Purchase purchace = new Purchase(client, bookExample, DateTime.Now);
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            int lastFilledPurchaseIndex = dataRepository.GetAllEvent().Count() - 1;
            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            dataRepository.AddClient(client);
            dataRepository.AddEvent(purchace);
            Assert.AreEqual(purchace, dataRepository.GetEvent(lastFilledPurchaseIndex + 1));
        }

        [TestMethod]
        public void DataRepositoryAddDuplicateBookTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);


            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            dataRepository.AddBook(book);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddBook(book));
            Assert.AreEqual("Data already exists", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddDuplicateBookExampleTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddBookExample(bookExample));
            Assert.AreEqual("Data already exists", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddDuplicateClientTest()
        {
            Client client = new Client("Adam", "Tomczak", 39);
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));


            dataRepository.AddClient(client);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddClient(client));
            Assert.AreEqual("Data already exists", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddDuplicateEventTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", 39);
            Purchase purchace = new Purchase(client, bookExample, DateTime.Now);
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            dataRepository.AddClient(client);
            dataRepository.AddEvent(purchace);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddEvent(purchace));
            Assert.AreEqual("Data already exists", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddBookExampleWithBadIsbnTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddBookExample(new BookExample(newBook, 13, 25.5)));
            Assert.AreEqual("Wrong book example Isbn reference", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddEventWithBadBookExampleRefTest()
        {
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            Client client = new Client("Adam", "Tomczak", 39);
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);

            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            int lastFilledClientIndex = dataRepository.GetAllClient().Count() - 1;
            dataRepository.AddBook(newBook);
            dataRepository.AddClient(client);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddEvent(new Purchase(client, newBookExample, DateTime.Now)));
            Assert.AreEqual("No such BookExample in DataRepository", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddEventWithBadClientTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", 39);
            Purchase purchace = new Purchase(client, bookExample, DateTime.Now);
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);

            Client newClient = new Client("Jan", "Kowalski", 39);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddEvent(new Purchase(newClient, bookExample, DateTime.Now)));
            Assert.AreEqual("No such Client in DataRepository", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryBadBookDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBook(newBook));
            Assert.AreEqual("No such book", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryBadBookExampleDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);
            dataRepository.AddBook(newBook);

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBookExample(newBookExample));
            Assert.AreEqual("No such book copy", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryBadClientDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Client newClient = new Client("Jan", "Kowalski", 39);

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteClient(newClient));
            Assert.AreEqual("No such client", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryBadPurchaseDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);
            dataRepository.AddBook(newBook);
            Client newClient = new Client("Jan", "Kowalski", 39);

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteEvent(new Purchase(newClient, newBookExample, DateTime.Now)));
            Assert.AreEqual("No such event", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryBookRefDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);
            Client newClient = new Client("Jan", "Kowalski", 39);
            dataRepository.AddBook(newBook);
            dataRepository.AddBookExample(newBookExample);
            dataRepository.AddClient(newClient);
            dataRepository.AddEvent(new Purchase(newClient, newBookExample, DateTime.Now));
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBook(newBook));
            Assert.AreEqual("Book has examples in use, can't be deleted", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryBookExampleRefDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);
            Client newClient = new Client("Jan", "Kowalski", 39);
            dataRepository.AddBook(newBook);
            dataRepository.AddBookExample(newBookExample);
            dataRepository.AddClient(newClient);
            dataRepository.AddEvent(new Purchase(newClient, newBookExample, DateTime.Now));

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBookExample(newBookExample));
            Assert.AreEqual("Book example is in use, can't be deleted", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryClientRefDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);
            Client newClient = new Client("Jan", "Kowalski", 39);
            dataRepository.AddBook(newBook);
            dataRepository.AddBookExample(newBookExample);
            dataRepository.AddClient(newClient);
            dataRepository.AddEvent(new Purchase(newClient, newBookExample, DateTime.Now));

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteClient(newClient));
            Assert.AreEqual("Client has purchaces, can't be deleted", exc.Message);
        }

        [TestMethod]
        public void DataRepositoryBookDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            dataRepository.AddBook(newBook);

            if (dataRepository.GetAllBook().Contains(newBook))
            {
                dataRepository.DeleteBook(newBook);
                Assert.IsFalse(dataRepository.GetAllBook().Contains(newBook));
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataRepositoryBookExampleDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);
            dataRepository.AddBook(newBook);
            dataRepository.AddBookExample(newBookExample);

            if (dataRepository.GetAllBookExamples().Contains(newBookExample))
            {
                dataRepository.DeleteBookExample(newBookExample);
                Assert.IsFalse(dataRepository.GetAllBookExamples().Contains(newBookExample));
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataRepositoryClientDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Client newClient = new Client("Jan", "Kowalski", 39);
            dataRepository.AddClient(newClient);
            if (dataRepository.GetAllClient().Contains(newClient))
            {
                dataRepository.DeleteClient(newClient);
                Assert.IsFalse(dataRepository.GetAllClient().Contains(newClient));
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataRepositoryPurchaseDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);
            Client newClient = new Client("Jan", "Kowalski", 39);
            Purchase purchase = new Purchase(newClient, newBookExample, DateTime.Now);
            dataRepository.AddBook(newBook);
            dataRepository.AddBookExample(newBookExample);
            dataRepository.AddClient(newClient);
            dataRepository.AddEvent(purchase);
            if (dataRepository.GetAllEvent().Contains(purchase))
            {
                dataRepository.DeleteEvent(purchase);
                Assert.IsFalse(dataRepository.GetAllEvent().Contains(purchase));
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataRepositoryPurchaseUpdateTest()
        {

            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            BookExample bookExample1 = dataRepository.GetBookExample(0);
            Client client1 = dataRepository.GetClient(0);
            Purchase purchase1 = (Purchase)dataRepository.GetEvent(0);
            Purchase purchase2 = new Purchase(client1, bookExample1, DateTime.Now);

            //Correct Update tests
            if (dataRepository.GetAllEvent().Contains(purchase1) && !dataRepository.GetAllEvent().Contains(purchase2))
            {
                dataRepository.UpdateEvent(0, purchase2);
                Assert.IsFalse(dataRepository.GetAllEvent().Contains(purchase1));
                Assert.IsTrue(dataRepository.GetAllEvent().Contains(purchase2));
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataRepositoryClientUpdateTest()
        {

            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Client client1 = dataRepository.GetClient(0);
            Client client2 = new Client("John", "Watson", 39);


            if (dataRepository.GetAllClient().Contains(client1) && !dataRepository.GetAllClient().Contains(client2))
            {
                dataRepository.UpdateClient(0, client2);
                Assert.IsFalse(dataRepository.GetAllClient().ToList().Contains(client1));
                Assert.IsTrue(dataRepository.GetAllClient().ToList().Contains(client2));
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataRepositoryClientForPurchaseUpdateTest()
        {

            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Client client1 = dataRepository.GetClient(0);
            Client client2 = new Client("John", "Watson", 39);
            Purchase purchase1 = (Purchase)dataRepository.GetEvent(0);
            BookExample bookExample1 = dataRepository.GetBookExample(0);
            Purchase purchase2 = new Purchase(client1, bookExample1, DateTime.Now);
            dataRepository.UpdateEvent(0, purchase2);

            if (dataRepository.GetAllClient().Contains(client1) && !dataRepository.GetAllClient().Contains(client2))
            {

                dataRepository.UpdateClient(0, client2);
                Assert.IsFalse(dataRepository.GetAllClient().ToList().Contains(client1));
                Assert.IsTrue(dataRepository.GetAllClient().ToList().Contains(client2));
                Assert.AreEqual(purchase2.Client, client2);
            }
            else
            {
                Assert.Fail();
            }


        }

        [TestMethod]
        public void DataRepositoryBookExampleUpdateTest()
        {

            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book book1 = dataRepository.GetAllBook().ToList()[0];
            Client client1 = dataRepository.GetClient(0);
            Client client2 = new Client("John", "Watson", 39);
            Purchase purchase1 = (Purchase)dataRepository.GetEvent(0);
            BookExample bookExample1 = dataRepository.GetBookExample(0);
            BookExample bookExample2 = new BookExample(book1, 10, 73.5);
            Purchase purchase2 = new Purchase(client1, bookExample1, DateTime.Now);
            dataRepository.UpdateEvent(0, purchase2);
            dataRepository.UpdateClient(0, client2);
            if (dataRepository.GetAllBookExamples().Contains(bookExample1) && !dataRepository.GetAllBookExamples().Contains(bookExample2))
            {

                dataRepository.UpdateBookExample(0, bookExample2);
                Assert.IsFalse(dataRepository.GetAllBookExamples().ToList().Contains(bookExample1));
                Assert.IsTrue(dataRepository.GetAllBookExamples().ToList().Contains(bookExample2));
                Assert.AreEqual(purchase2.BookExample, bookExample2);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataRepositoryBookUpdateTest()
        {

            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Book book1 = dataRepository.GetAllBook().ToList()[0];
            Book book2 = new Book("Igrzyska Smierci", "Suzanne Collins", book1.Isbn);
            Client client1 = dataRepository.GetClient(0);
            Client client2 = new Client("John", "Watson", 39);
            BookExample bookExample1 = dataRepository.GetBookExample(0);
            BookExample bookExample2 = new BookExample(book1, 10, 73.5);
            Purchase purchase2 = new Purchase(client1, bookExample1, DateTime.Now);
            dataRepository.UpdateEvent(0, purchase2);
            dataRepository.UpdateClient(0, client2);
            dataRepository.UpdateBookExample(0, bookExample2);
            if (dataRepository.GetAllBook().Contains(book1) && !dataRepository.GetAllBook().Contains(book2))
            {

                dataRepository.UpdateBook(book2);
                Assert.IsFalse(dataRepository.GetAllBook().ToList().Contains(book1));
                Assert.IsTrue(dataRepository.GetAllBook().ToList().Contains(book2));
                Assert.AreEqual(bookExample2.Book, book2);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataRepositoryBadBookUpdateTest()
        {
            {

                DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

                var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBook(new Book("Silmarillion", "J.R.R. Tolkien", Guid.NewGuid())));
                Assert.AreEqual("No such book", exc.Message);

            }
        }

        [TestMethod]
        public void DataRepositoryBadBookExampleUpdateTest()
        {
            {
                DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
                BookExample bookExample1 = dataRepository.GetBookExample(0);
                var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBookExample(dataRepository.GetAllBookExamples().Count(), bookExample1));
                Assert.AreEqual("No such book copy index", exc.Message);

            }
        }

        [TestMethod]
        public void DataRepositoryBadClientUpdateTest()
        {
            {
                DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
                Client client1 = dataRepository.GetClient(0);
                var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateClient(dataRepository.GetAllClient().Count(), client1));
                Assert.AreEqual("No such client index", exc.Message);

            }
        }

        [TestMethod]
        public void DataRepositoryBadEventUpdateTest()
        {
            {
                DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
                Purchase purchase1 = (Purchase)dataRepository.GetEvent(0);
                var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateEvent(dataRepository.GetAllEvent().Count(), purchase1));
                Assert.AreEqual(exc.Message, "No such event index");

            }
        }

        [TestMethod]
        public void DataRepositoryBadBookIndexUpdateTest()
        {
            {

                DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
                Book book1 = dataRepository.GetAllBook().ToList()[0];
                Book book2 = new Book("Hobbit", "J.R.R. Tolkien", book1.Isbn);
                var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBook(book2));
                Assert.AreEqual("Data already exists", exc.Message);

            }
        }

        [TestMethod]
        public void DataRepositoryBadClientIndexUpdateTest()
        {
            {
                DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
                Client client2 = dataRepository.GetClient(0);
                var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateClient(0, client2));
                Assert.AreEqual("Data already exists", exc.Message);

            }
        }

        [TestMethod]
        public void DataRepositoryBadBookExampleIndexUpdateTest()
        {
            {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));

            BookExample bookExample1 = dataRepository.GetBookExample(0);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBookExample(0, bookExample1));
            Assert.AreEqual("Data already exists", exc.Message);

        }
    }

    [TestMethod]
    public void DataRepositoryBadEventIndexUpdateTest()
    {
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller(clientList, booksList, purchaceList, bookExampleList));
            Purchase purchase2 = (Purchase)dataRepository.GetEvent(0);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateEvent(0, purchase2));
            Assert.AreEqual("Data already exists", exc.Message);

        }
    }

}
}
