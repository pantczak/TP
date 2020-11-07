using System;
using BookShop.model.data;
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

        }

    }
}
