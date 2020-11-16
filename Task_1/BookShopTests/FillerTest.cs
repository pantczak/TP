using BookShop.model;
using BookShop.model.data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace BookShopTests
{
    [TestClass]
    public class FillerTest
    {
       

        [TestMethod]
        public void ConstFillerBookTest()
        {
            List<Book> booksList = new List<Book>()
            {
            new Book("Hobbit", "J.R.R. Tolkien", Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")),
            new Book("Mistborn", "Brandon Sanderson", Guid.Parse("BD8A1680-65BD-44A6-92C0-88827A3B473A")),
            new Book("The Lord of the Rings", "J.R.R. Tolkien", Guid.Parse("10957692-E446-4577-B8C7-8B41FF7C17B8")),
            new Book("Harry Potter", "J.K. Rowling", Guid.Parse("A5CC4A82-7498-4A10-8FB8-973954335474")),
            new Book("Metro 2033", "Dmitrij Głuchowski", Guid.Parse("86167D69-FA4D-42CC-8D08-9972F2F04EF5"))
            };

            List<Client> clientList = new List<Client>()
            {
                new Client("Adam", "Kowalski", 39),
                new Client("Jan", "Nowak",34)
            };

            List<BookExample> bookExampleList = new List<BookExample>()
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

            List<Event> purchaceList = new List<Event>()
            {
            new Purchase(clientList[0], bookExampleList[0], DateTime.Parse("5/10/2020 21:11:00")),
            new Purchase(clientList[1], bookExampleList[7], DateTime.Parse("3/5/2019 11:30:00")),
            new Purchase(clientList[0], bookExampleList[0], DateTime.Parse("11/1/2011 11:59:00")),
            };


            DataRepository dataRepository = new DataRepository(new ConstFiller());
            

            for (int i = 0; i < booksList.Count; i++) {
                if (!booksList[i].Equals(dataRepository.GetBook(booksList[i].Isbn)))
                {
                    Assert.Fail();
                }
            }

            for (int i = 0; i < clientList.Count; i++)
            {
                if (!clientList[i].Equals(dataRepository.GetClient(i)))
                {
                    Assert.Fail();
                }
            }

            for (int i = 0; i < bookExampleList.Count; i++)
            {
                if (!bookExampleList[i].Equals(dataRepository.GetBookExample(i)))
                {
                    Assert.Fail();
                }
            }
            
            for (int i = 0; i < purchaceList.Count; i++)
            {
                if (!purchaceList[i].Equals(dataRepository.GetEvent(i)))
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod]
        public void FillFromFileTest()
        {
            string text = "Client;Jan;Kowalski;22\n" +
                          "Book;W pustyni i w puszczy;Henryk Sienkiewicz;820EF5E7-641D-4D4C-8785-36B538AF4226\n" +
                          "BookExample;0;33;40\n" +
                          "Purchase;0;0;5/10/2020 21:11:00\n" +
                          "Return;0;0;5/10/2020 21:11:00;7/11/2020 23:11:00";
            File.WriteAllText("testfile.txt", text);
            IDataRepository dataRepository = new DataRepository(new FillFromFile("testfile.txt"));

            Assert.AreEqual("Jan", dataRepository.GetClient(0).FirstName);
            Assert.AreEqual("Kowalski", dataRepository.GetClient(0).LastName);
            Assert.AreEqual(22, dataRepository.GetClient(0).Age);

            Assert.AreEqual("W pustyni i w puszczy", dataRepository.GetBook(Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")).Title);
            Assert.AreEqual("Henryk Sienkiewicz", dataRepository.GetBook(Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")).Author);
            Assert.AreEqual(Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226"), dataRepository.GetBook(Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")).Isbn);

            Assert.AreEqual(dataRepository.GetBook(Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")), dataRepository.GetBookExample(0).Book);
            Assert.AreEqual(33, dataRepository.GetBookExample(0).Tax);
            Assert.AreEqual(40.0, dataRepository.GetBookExample(0).BasePrice);

            Assert.AreEqual(dataRepository.GetBookExample(0), dataRepository.GetEvent(0).BookExample);
            Assert.AreEqual(dataRepository.GetClient(0), dataRepository.GetEvent(0).Client);
            Assert.AreEqual(DateTime.Parse("5/10/2020 21:11:00"), dataRepository.GetEvent(0).EventTime);

            Assert.AreEqual(dataRepository.GetBookExample(0), dataRepository.GetEvent(1).BookExample);
            Assert.AreEqual(dataRepository.GetClient(0), dataRepository.GetEvent(1).Client);
            Assert.AreEqual(DateTime.Parse("5/10/2020 21:11:00"), dataRepository.GetEvent(1).EventTime);
            Return retrn =(Return) dataRepository.GetEvent(1);
            Assert.AreEqual(DateTime.Parse("7/11/2020 23:11:00"), retrn.ReturnDate);



        }
    }
}
