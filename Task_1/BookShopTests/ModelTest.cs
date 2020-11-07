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
        public void DataRepositoryTest()
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
            dataRepository.AddBook(book);
            dataRepository.AddBookExample(bookExample);
            dataRepository.AddClient(client);
            dataRepository.AddPurchace(purchace);
            

            Assert.AreEqual(book, dataRepository.GetBook(book.Isbn));
            Assert.AreEqual(bookExample, dataRepository.GetBookExample(lastFilledBookExampleIndex+1));
            Assert.AreEqual(client, dataRepository.GetClient(lastFilledClientIndex+1));
            Assert.AreEqual(purchace, dataRepository.GetPurchace(lastFilledPurchaseIndex+1));

            var exc= Assert.ThrowsException<Exception>(() => dataRepository.AddBook(book));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException <Exception> (() => dataRepository.AddBookExample(bookExample));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddClient(client));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddPurchace(purchace));
            Assert.AreEqual(exc.Message, "Data already exists");

            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddBookExample(new BookExample(new Book("Ksiazka niebedaca w bazie","Anonim",Guid.NewGuid()),13,25.5)));
            Assert.AreEqual(exc.Message, "Wrong book example Isbn reference");

            BookExample newBookExample = new BookExample(book, 10, 49.9);
            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddPurchace(new Purchace(client,newBookExample,DateTime.Now)));
            Assert.AreEqual(exc.Message, "No such BookExample in DataRepository");

            Client newClient = new Client("Jan", "Kowalski", "11234567890");
            exc = Assert.ThrowsException<Exception>(() => dataRepository.AddPurchace(new Purchace(newClient, bookExample, DateTime.Now)));
            Assert.AreEqual(exc.Message, "No such Client in DataRepository");



        }

    }
}
