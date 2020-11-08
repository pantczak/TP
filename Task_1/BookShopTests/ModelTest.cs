using System;
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
        [TestMethod]
        public void BookTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien",guid);
            Assert.AreEqual(guid, book.Isbn);
            Assert.AreEqual("Hobbit", book.Title);
            Assert.AreEqual("J. R. R. Tolkien", book.Author);
        }

        [TestMethod]
        public void ClientTest()
        {
            Client client = new Client("Adam","Tomczak",39);
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
            Assert.AreEqual(69.99 * 23 / 100.0, bookExample.Price,0.00001);
        }

        [TestMethod]
        public void PurchaceTest()
        {
            Guid guid = Guid.NewGuid();
            DateTime dateTime = DateTime.Now;
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23,69.99);
            Client client = new Client("Adam", "Tomczak", 39);
            Purchase purchace = new Purchase( client, bookExample, dateTime);
            Assert.AreEqual(bookExample, purchace.BookExample);
            Assert.AreEqual(client, purchace.Client);
            Assert.AreEqual(dateTime, purchace.EventTime);
        }

        [TestMethod]
        public void DataRepositoryAddBookTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            
            
            DataRepository dataRepository = new DataRepository(new ConstFiller());
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
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            
            int lastFilledBookExampleIndex = dataRepository.GetAllBookExamples().Count() - 1;
            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            Assert.AreEqual(bookExample, dataRepository.GetBookExample(lastFilledBookExampleIndex + 1));
        }

        [TestMethod]
        public void DataRepositoryAddClientTest()
        {
            Client client = new Client("Adam", "Tomczak", 39);
            DataRepository dataRepository = new DataRepository(new ConstFiller());

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
            DataRepository dataRepository = new DataRepository(new ConstFiller());

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


            DataRepository dataRepository = new DataRepository(new ConstFiller());

            dataRepository.AddBook(book);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddBook(book));
            Assert.AreEqual("Data already exists",exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddDuplicateBookExampleTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            DataRepository dataRepository = new DataRepository(new ConstFiller());

            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddBookExample(bookExample));
            Assert.AreEqual("Data already exists",exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddDuplicateClientTest()
        {
            Client client = new Client("Adam", "Tomczak", 39);
            DataRepository dataRepository = new DataRepository(new ConstFiller());


            dataRepository.AddClient(client);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddClient(client));
            Assert.AreEqual( "Data already exists",exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddDuplicateEventTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", 39);
            Purchase purchace = new Purchase(client, bookExample, DateTime.Now);
            DataRepository dataRepository = new DataRepository(new ConstFiller());

            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            dataRepository.AddClient(client);
            dataRepository.AddEvent(purchace);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddEvent(purchace));
            Assert.AreEqual( "Data already exists",exc.Message);
        }
      
        [TestMethod]
        public void DataRepositoryAddBookExampleWithBadIsbnTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());

            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddBookExample(new BookExample(newBook, 13, 25.5)));
            Assert.AreEqual( "Wrong book example Isbn reference",exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddEventWithBadBookExampleRefTest()
        {
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            Client client = new Client("Adam", "Tomczak", 39);
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);

            DataRepository dataRepository = new DataRepository(new ConstFiller());

            int lastFilledClientIndex = dataRepository.GetAllClient().Count() - 1;
            dataRepository.AddBook(newBook);
            dataRepository.AddClient(client);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddEvent(new Purchase(client, newBookExample, DateTime.Now)));
            Assert.AreEqual( "No such BookExample in DataRepository" ,exc.Message);
        }

        [TestMethod]
        public void DataRepositoryAddEventWithBadClientTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", 39);
            Purchase purchace = new Purchase(client, bookExample, DateTime.Now);
            DataRepository dataRepository = new DataRepository(new ConstFiller());

            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);

            Client newClient = new Client("Jan", "Kowalski", 39);
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.AddEvent(new Purchase(newClient, bookExample, DateTime.Now)));
            Assert.AreEqual( "No such Client in DataRepository",exc.Message);
        }


        [TestMethod]
        public void DataRepositoryDeleteTest() 
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", 39);
            Purchase purchace = new Purchase(client, bookExample, DateTime.Now);
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(book, 10, 49.9);
            Client newClient = new Client("Jan", "Kowalski", 39);
            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            dataRepository.AddClient(client);
            dataRepository.AddEvent(purchace);

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBook(newBook));
          //  Assert.AreEqual(exc.Message, "No such book");

            //exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBookExample(newBookExample));
           // Assert.AreEqual(exc.Message, "No such book copy");

           // exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteClient(newClient));
           // Assert.AreEqual(exc.Message, "No such client");
            Purchase newPurchase = new Purchase(newClient, newBookExample, DateTime.Now);
            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteEvent(newPurchase));
            Assert.AreEqual(exc.Message, "No such event");

            //Referenced objects Deletions tests
            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBook(book));
            Assert.AreEqual(exc.Message, "Book has examples in use, can't be deleted");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBookExample(bookExample));
            Assert.AreEqual(exc.Message, "Book example is in use, can't be deleted");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteClient(client));
            Assert.AreEqual(exc.Message, "Client has purchaces, can't be deleted");

            //Correct Deletions tests
            Assert.IsTrue(dataRepository.GetAllEvent().ToList().Contains(purchace));
            dataRepository.DeleteEvent(purchace);
            Assert.IsFalse(dataRepository.GetAllEvent().ToList().Contains(purchace));

            Assert.IsTrue(dataRepository.GetAllClient().ToList().Contains(client));
            dataRepository.DeleteClient(client);
            Assert.IsFalse(dataRepository.GetAllClient().ToList().Contains(client));

            Assert.IsTrue(dataRepository.GetAllBookExamples().ToList().Contains(bookExample));
            dataRepository.DeleteBookExample(bookExample);
            Assert.IsFalse(dataRepository.GetAllBookExamples().ToList().Contains(bookExample));

            Assert.IsTrue(dataRepository.GetAllBook().ToList().Contains(book));
            dataRepository.DeleteBook(book);
            Assert.IsFalse(dataRepository.GetAllBook().ToList().Contains(book));
        }

        [TestMethod]
        public void DataRepositoryBadBookDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBook(newBook));
            Assert.AreEqual(exc.Message, "No such book");
        }

        [TestMethod]
        public void DataRepositoryBadBookExampleDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(newBook, 10, 49.9);
            dataRepository.AddBook(newBook);
            
            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBookExample(newBookExample));
            Assert.AreEqual(exc.Message, "No such book copy");
        }

        [TestMethod]
        public void DataRepositoryBadClientDeleteTest()
        {
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Client newClient = new Client("Jan", "Kowalski", 39);

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteClient(newClient));
            Assert.AreEqual(exc.Message, "No such client");
        }



        [TestMethod]
        public void DataRepositoryUpdateTest()
        {
            
            DataRepository dataRepository = new DataRepository(new ConstFiller());
            Book book1 = dataRepository.GetAllBook().ToList()[0];
            Book book2 = new Book("Igrzyska Smierci", "Suzanne Collins", book1.Isbn);
            BookExample bookExample1 = dataRepository.GetBookExample(0);
            BookExample bookExample2 = new BookExample(book1, 10, 73.5);
            Client client1 = dataRepository.GetClient(0);
            Client client2 = new Client("John", "Watson", 39);
            Purchase purchase1 = (Purchase)dataRepository.GetEvent(0);
            Purchase purchase2 = new Purchase(client1, bookExample1, DateTime.Now);

            //Correct Update tests
            Assert.IsTrue(dataRepository.GetAllEvent().ToList().Contains(purchase1));
            Assert.IsFalse(dataRepository.GetAllEvent().ToList().Contains(purchase2));
            dataRepository.UpdateEvent(0, purchase2);
            Assert.IsFalse(dataRepository.GetAllEvent().ToList().Contains(purchase1));
            Assert.IsTrue(dataRepository.GetAllEvent().ToList().Contains(purchase2));


            Assert.IsTrue(dataRepository.GetAllClient().ToList().Contains(client1));
            Assert.IsFalse(dataRepository.GetAllClient().ToList().Contains(client2));
            Assert.AreNotEqual(purchase2.Client, client2);
            dataRepository.UpdateClient(0, client2);
            Assert.IsFalse(dataRepository.GetAllClient().ToList().Contains(client1));
            Assert.IsTrue(dataRepository.GetAllClient().ToList().Contains(client2));
            Assert.AreEqual(purchase2.Client, client2);

            Assert.IsTrue(dataRepository.GetAllBookExamples().ToList().Contains(bookExample1));
            Assert.IsFalse(dataRepository.GetAllBookExamples().ToList().Contains(bookExample2));
            Assert.AreNotEqual(purchase2.BookExample, bookExample2);
            dataRepository.UpdateBookExample(0, bookExample2);
            Assert.IsFalse(dataRepository.GetAllBookExamples().ToList().Contains(bookExample1));
            Assert.IsTrue(dataRepository.GetAllBookExamples().ToList().Contains(bookExample2));
            Assert.AreEqual(purchase2.BookExample, bookExample2);

            Assert.IsTrue(dataRepository.GetAllBook().ToList().Contains(book1));
            Assert.IsFalse(dataRepository.GetAllBook().ToList().Contains(book2));
            Assert.AreNotEqual(bookExample2.Book, book2);
            dataRepository.UpdateBook(book2);
            Assert.IsFalse(dataRepository.GetAllBook().ToList().Contains(book1));
            Assert.IsTrue(dataRepository.GetAllBook().ToList().Contains(book2));
            Assert.AreEqual(bookExample2.Book, book2);

            //Not existing objects Update tests

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBook(new Book("Silmarillion", "J.R.R. Tolkien", Guid.NewGuid())));
            Assert.AreEqual(exc.Message, "No such book");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBookExample(dataRepository.GetAllBookExamples().Count(), bookExample1));
            Assert.AreEqual(exc.Message, "No such book copy index");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateClient(dataRepository.GetAllClient().Count(), client1));
            Assert.AreEqual(exc.Message, "No such client index");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateEvent(dataRepository.GetAllEvent().Count(), purchase1));
            Assert.AreEqual(exc.Message, "No such purchase index");

            // Diffrent index objects Update tests

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBook(book2));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBookExample(0, bookExample2));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateClient(0, client2));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateEvent(0, purchase2));
            Assert.AreEqual(exc.Message, "Data already exists");
        }

    }
}
