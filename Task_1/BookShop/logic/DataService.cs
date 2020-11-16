using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.Logic
{
    public class DataService : IDataService
    {
        private IDataRepository dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public void AddBook(string title, string author, Guid isbn)
        {
            dataRepository.AddBook(new Book(title, author, isbn));
        }

        public void AddBookExample(Book book, int tax, double basePrice)
        {
            dataRepository.AddBookExample(new BookExample(book, tax, basePrice));
        }

        public void CreateClient(string firstName, string lastName, int age)
        {
            dataRepository.AddClient(new Client(firstName, lastName, age));
        }

        public IEnumerable<BookExample> GetAllBookExamples()
        {
            return dataRepository.GetAllBookExamples();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return dataRepository.GetAllBook();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return dataRepository.GetAllClient();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return dataRepository.GetAllEvent();
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            IEnumerable<Event> eventList= dataRepository.GetAllEvent();
            List<Purchase> purchaseList = new List<Purchase>();
            foreach (Event evnt in eventList)
            {
                if(evnt is Purchase)
                {
                    purchaseList.Add((Purchase)evnt);
                }
            }
            return purchaseList;
        }

        public Book GetBookByIsbn(Guid isbn)
        {
            return dataRepository.GetBook(isbn);

        }

        public IEnumerable<BookExample> GetBookExamplesByBook(Book book)
        {
            IEnumerable<BookExample> bookExamples = dataRepository.GetAllBookExamples();
            List<BookExample> foundBookExamples = new List<BookExample>();
            foreach (BookExample bookExample in bookExamples)
            {
                if (bookExample.Book == book)
                {
                    foundBookExamples.Add(bookExample);
                }
            }
            return foundBookExamples;
        }

        public IEnumerable<BookExample> GetBookExamplesInPriceRange(double priceMin, double priceMax)
        {
            IEnumerable<BookExample> bookExamples = dataRepository.GetAllBookExamples();
            List<BookExample> foundBookExamples = new List<BookExample>();
            foreach (BookExample bookExample in bookExamples)
            {
                if (bookExample.Price >= priceMin && bookExample.Price <= priceMax)
                {
                    foundBookExamples.Add(bookExample);
                }
            }
            return foundBookExamples;
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            IEnumerable<Book> books = dataRepository.GetAllBook();
            List<Book> foundBooks= new List<Book>();
            foreach (Book book in books)
            {
                if (book.Author == author)
                {
                    foundBooks.Add(book);
                }
            }
            return foundBooks;
        }

        public IEnumerable<Book> GetBooksByTitle(string title)
        {
            IEnumerable<Book> books = dataRepository.GetAllBook();
            List<Book> foundBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (book.Title == title)
                {
                    foundBooks.Add(book);
                }
            }
            return foundBooks;
        }

        public IEnumerable<Client> GetClientsByAgeRange(int ageMin, int ageMax)
        {
            IEnumerable<Client> clients = dataRepository.GetAllClient();
            List<Client> foundClients = new List<Client>();
            foreach (Client client in clients)
            {
                if (client.Age >=ageMin && client.Age<=ageMax)
                {
                    foundClients.Add(client);
                }
            }
            return foundClients;
        }

        public IEnumerable<Client> GetClientsByFirstLetterOfName(char letter)
        {
            IEnumerable<Client> clients = dataRepository.GetAllClient();
            List<Client> foundClients = new List<Client>();
            foreach (Client client in clients)
            {
                if (client.LastName[0]==letter)
                {
                    foundClients.Add(client);
                }
            }
            return foundClients;
        }


        public IEnumerable<Event> GetEventsByBookExample(BookExample bookExample)
        {
            IEnumerable<Event> events= dataRepository.GetAllEvent();
            List<Event> foundEvents= new List<Event>();
            foreach (Event evnt in events)
            {
                if(evnt.BookExample==bookExample)
                {
                    foundEvents.Add(evnt);
                }
            }
            return foundEvents;
        }

        public IEnumerable<Event> GetEventsByClient(Client client)
        {
            IEnumerable<Event> events = dataRepository.GetAllEvent();
            List<Event> foundEvents = new List<Event>();
            foreach (Event evnt in events)
            {
                if (evnt.Client == client)
                {
                    foundEvents.Add(evnt);
                }
            }
            return foundEvents;
        }

        public IEnumerable<Purchase> GetPurchasesInDateRange(DateTime from, DateTime to)
        {
            IEnumerable<Purchase> purchases = GetAllPurchases();
            List<Purchase> foundPurchases = new List<Purchase>();
            foreach (Purchase purchase in purchases)
            {
                if (purchase.EventTime >=from && purchase.EventTime <=to)
                {
                    foundPurchases.Add(purchase);
                }
            }
            return foundPurchases;
        }

        public void ModifyBook(Book newBook)
        {
            dataRepository.UpdateBook(newBook);
            
        }

        public void ModifyBookExample(BookExample oldBookExample, BookExample newBookExample)
        {
        List<BookExample> books = GetAllBookExamples().ToList();
            for (int i = 0; i < books.Count(); i++)
            {
                if (books[i].Equals(oldBookExample))
                {
                    dataRepository.UpdateBookExample(i, newBookExample);
                    return;
                }
            }
            throw new Exception("No such book copy");
        }

        public void ModifyClient(Client oldClient, Client newClient)
        {
            List<Client> clients = GetAllClients().ToList();
            for (int i = 0; i < clients.Count(); i++)
            {
                if (clients[i].Equals(oldClient))
                {
                    dataRepository.UpdateClient(i, newClient);
                    return;
                }
            }
            throw new Exception("No such client");
        }

        public void ModifyEvent(Event oldEvent, Event newEvent)
        {
            List<Event> events = GetAllEvents().ToList();
            for (int i = 0; i < events.Count(); i++)
            {
                if (events[i].Equals(oldEvent))
                {
                    dataRepository.UpdateEvent(i, newEvent);
                    return;
                }
            }
            throw new Exception("No such event");
        }

        public void PurchaceBook(Client client, BookExample bookExample)
        {
            dataRepository.AddEvent(new Purchase(client, bookExample,DateTime.Now));
        }

        public void RemoveBook(Book book)
        {
            dataRepository.DeleteBook(book);
        }

        public void RemoveBookExample(BookExample bookExample)
        {
            dataRepository.DeleteBookExample(bookExample);
        }

        public void RemoveClient(Client client)
        {
            dataRepository.DeleteClient(client);
        }

        public void RemoveEvent(Event evnt)
        {
            dataRepository.DeleteEvent(evnt);
        }

        public void ReturnBook(Client client, BookExample bookExample, Event evnt)
        {
            dataRepository.AddEvent(new Return(DateTime.Now, client, bookExample, evnt.EventTime));
        }
    }
}
