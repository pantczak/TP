using System;
using Task1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Task1Tests
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
            Assert.AreEqual("98051234565", client.Id);
            Assert.AreEqual("Adam", client.FirstName);
            Assert.AreEqual("Tomczak", client.LastName);
        }


        [TestMethod]
        public void BookExampleTest()
        {
            Guid guid = Guid.NewGuid();
            Guid guid1 = Guid.NewGuid();
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid1);
            BookExample bookExample = new BookExample(guid,book, 23,5, 69.99);
            Assert.AreEqual(guid, bookExample.Id);
            Assert.AreEqual(book, bookExample.Book);
            Assert.AreEqual(23, bookExample.Tax);
            Assert.AreEqual(5, bookExample.Quantitiy);
            Assert.AreEqual(69.99, bookExample.Price);
        }

        [TestMethod]
        public void PurchaceTest()
        {
            Guid guid = Guid.NewGuid();
            DateTime dateTime = DateTime.Now;
            Book book = new Book("Hobbit", "J. R. R. Tolkien", guid);
            BookExample bookExample = new BookExample(guid, book, 23, 5, 69.99);
            Client client = new Client("Adam", "Tomczak", "98051234565");
            Purchace purchace = new Purchace(guid, client, bookExample, dateTime);
            Assert.AreEqual(guid, purchace.Id);
            Assert.AreEqual(bookExample, purchace.BookExample);
            Assert.AreEqual(client, purchace.Client);
            Assert.AreEqual(dateTime, purchace.DateOfPurchace);
        }

    }
}
