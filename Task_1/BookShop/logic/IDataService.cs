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

    }
}
