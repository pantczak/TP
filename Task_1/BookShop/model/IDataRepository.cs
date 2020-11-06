using BookShop.model.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.model
{
    internal interface IDataRepository
    {
        void AddBook(Book book);
        Book GetBook(int Id);
        IEnumerable<Book> GetAllBook();
        void UpdateBook(int Id, Book book);
        void DeleteBook(Book book);
        void AddClient(Client client);
        Client GetClient(Client client);
        IEnumerable<Client> GetAllClient();
        void UpdateClient(int Id, Client client);
        void DeleteClient(Client client);
    }
}
