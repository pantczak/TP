using BookShop.model.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.logic
{
    public interface IDataService
    {
        void CreateClient(string firstName, string lastName, string pesel);
        void AddBook(string title, string author, Guid isbn);
        void AddBookExample(Book book, int tax, double basePrice);
        void PurchaceBook(Client client, BookExample bookExample);
        IEnumerable<Client> GetAllClientsByName(string name = null);
        IEnumerable<Purchace> GetClientPurchaces(Client client);
        IEnumerable<Purchace> GetPurchacesFromDate(DateTime? start = null, DateTime? end = null);
        Dictionary<Book, int> GetBooks();
        IEnumerable<BookExample> GetBookExamples(Guid isbn);
    }
}
