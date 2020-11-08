using BookShop.model.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.logic
{
    public interface IDataService
    {
        void CreateClient(string firstName, string lastName, int age);
        void AddBook(string title, string author, Guid isbn);
        void AddBookExample(Book book, int tax, double basePrice);
        void PurchaceBook(Client client, BookExample bookExample);
        void RemoveClient(Client client);
        void RemoveBook(Book book);
        void RemoveBookExample(BookExample bookExample);
        void RemoveEvent(Event evnt);
        void ModifyClient(Client oldClient,Client newClient);
        void ModifyBook(Book oldBook, Book newBook);
        void ModifyBookExample(BookExample oldBookExample,BookExample newBookExample);
        void ModifyPurchase(Purchase oldPurchase, Purchase newPurchase);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<BookExample> GetAllBookExamples();
        IEnumerable<Client> GetAllClients();
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Purchase> GetAllPurchases();
        IEnumerable<Purchase> GetPurchasesInDateRange(DateTime from,DateTime to);
        IEnumerable<Event> GetEventsByClient(Client client);
        IEnumerable<Event> GetEventsByBookExample(BookExample bookExample);
        IEnumerable<Book> GetBooksByAuthor(string author);
        IEnumerable<Book> GetBooksByTitle(string title);
        Book GetBookByIsbn(Guid isbn);
        IEnumerable<BookExample> GetBookExamplesByBook(Book book);
        IEnumerable<BookExample> GetBookExamplesInPriceRange(double priceMin, double priceMax);
        IEnumerable<Client> GetClientsByFirstLetterOfName(char letter);
        IEnumerable<Client> GetClientsByPesel(string pesel);






    }
}
