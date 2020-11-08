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
            Client client = new Client("Adam","Tomczak","98051234565");
            Assert.AreEqual("98051234565", client.Pesel);
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
            Assert.AreEqual(69.99 * 23 / 100.0, bookExample.Price);
        }

        [TestMethod]
        public void PurchaceTest()
        {
            Guid guid = Guid.NewGuid();
            DateTime dateTime = DateTime.Now;
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23,69.99);
            Client client = new Client("Adam", "Tomczak", "98051234565");
            Purchace purchace = new Purchace( client, bookExample, dateTime);
            Assert.AreEqual(bookExample, purchace.BookExample);
            Assert.AreEqual(client, purchace.Client);
            Assert.AreEqual(dateTime, purchace.DateOfPurchace);
        }

        [TestMethod]
        public void DataRepositoryAddTest()
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", "98051234565");
            Purchace purchace = new Purchace(client, bookExample, DateTime.Now);

            DataRepository dataRepository = new DataRepository(new ConstDataFiller());
            int lastFilledBookIndex = dataRepository.GetAllBook().Count()-1;
            int lastFilledBookExampleIndex = dataRepository.GetAllBookExamples().Count() - 1;
            int lastFilledClientIndex = dataRepository.GetAllClient().Count() - 1;
            int lastFilledPurchaseIndex = dataRepository.GetAllPurchace().Count() - 1;

            //Correct Additions check
            dataRepository.AddBook(book);
            Assert.AreEqual(book, dataRepository.GetBook(book.Isbn));

            dataRepository.AddBookExample(bookExample);
            Assert.AreEqual(bookExample, dataRepository.GetBookExample(lastFilledBookExampleIndex+1));

            dataRepository.AddClient(client);
            Assert.AreEqual(client, dataRepository.GetClient(lastFilledClientIndex+1));

            dataRepository.AddPurchace(purchace);
            Assert.AreEqual(purchace, dataRepository.GetPurchace(lastFilledPurchaseIndex+1));

            //Already Existing Data Additions check
            var exc= Assert.ThrowsException<Exception>(() => dataRepository.AddBook(book));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException <Exception> (() => dataRepository.AddBookExample(bookExample));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddClient(client));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddPurchace(purchace));
            Assert.AreEqual(exc.Message, "Data already exists");
            //Data with incorrect data references Additions check
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddBookExample(new BookExample(newBook,13,25.5)));
            Assert.AreEqual(exc.Message, "Wrong book example Isbn reference");

            BookExample newBookExample = new BookExample(book, 10, 49.9);
            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddPurchace(new Purchace(client,newBookExample,DateTime.Now)));
            Assert.AreEqual(exc.Message, "No such BookExample in DataRepository");

            Client newClient = new Client("Jan", "Kowalski", "11234567890");
            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddPurchace(new Purchace(newClient, bookExample, DateTime.Now)));
            Assert.AreEqual(exc.Message, "No such Client in DataRepository");
        }

        [TestMethod]
        public void DataRepositoryDeleteTest() 
        {
            Guid guid = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(book, 23, 69.99);
            Client client = new Client("Adam", "Tomczak", "98051234565");
            Purchace purchace = new Purchace(client, bookExample, DateTime.Now);
            DataRepository dataRepository = new DataRepository(new ConstDataFiller());
            Book newBook = new Book("Ksiazka niebedaca w bazie", "Anonim", Guid.NewGuid());
            BookExample newBookExample = new BookExample(book, 10, 49.9);
            Client newClient = new Client("Jan", "Kowalski", "11234567890");
            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            dataRepository.AddClient(client);
            dataRepository.AddPurchace(purchace);

            var exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBook(newBook));
            Assert.AreEqual(exc.Message, "No such book");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBookExample(newBookExample));
            Assert.AreEqual(exc.Message, "No such book copy");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteClient(newClient));
            Assert.AreEqual(exc.Message, "No such client");
            Purchace newPurchase = new Purchace(newClient, newBookExample, DateTime.Now);
            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeletePurchace(newPurchase));
            Assert.AreEqual(exc.Message, "No such purchace");

            //Referenced objects Deletions tests
            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBook(book));
            Assert.AreEqual(exc.Message, "Book has examples in use, can't be deleted");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteBookExample(bookExample));
            Assert.AreEqual(exc.Message, "Book example is in use, can't be deleted");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.DeleteClient(client));
            Assert.AreEqual(exc.Message, "Client has purchaces, can't be deleted");

            //Correct Deletions tests
            Assert.IsTrue(dataRepository.GetAllPurchace().ToList().Contains(purchace));
            dataRepository.DeletePurchace(purchace);
            Assert.IsFalse(dataRepository.GetAllPurchace().ToList().Contains(purchace));

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
        public void DataRepositoryUpdateTest()
        {
            
            DataRepository dataRepository = new DataRepository(new ConstDataFiller());
            Book book1 = dataRepository.GetAllBook().ToList()[0];
            Book book2 = new Book("Igrzyska Smierci", "Suzanne Collins", book1.Isbn);
            BookExample bookExample1 = dataRepository.GetBookExample(0);
            BookExample bookExample2 = new BookExample(book1, 10, 73.5);
            Client client1 = dataRepository.GetClient(0);
            Client client2 = new Client("John", "Watson", "94707384534");
            Purchace purchase1 = dataRepository.GetPurchace(0);
            Purchace purchase2 = new Purchace(client1, bookExample1, DateTime.Now);

            //Correct Update tests
            Assert.IsTrue(dataRepository.GetAllPurchace().ToList().Contains(purchase1));
            Assert.IsFalse(dataRepository.GetAllPurchace().ToList().Contains(purchase2));
            dataRepository.UpdatePurchace(0, purchase2);
            Assert.IsFalse(dataRepository.GetAllPurchace().ToList().Contains(purchase1));
            Assert.IsTrue(dataRepository.GetAllPurchace().ToList().Contains(purchase2));


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

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdatePurchace(dataRepository.GetAllPurchace().Count(), purchase1));
            Assert.AreEqual(exc.Message, "No such purchase index");

            // Diffrent index objects Update tests

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBook(book2));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateBookExample(0, bookExample2));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdateClient(0, client2));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.UpdatePurchace(0, purchase2));
            Assert.AreEqual(exc.Message, "Data already exists");
        }

    }
}
