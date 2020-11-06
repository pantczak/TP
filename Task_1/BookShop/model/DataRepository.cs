using BookShop.model.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.model
{
    public class DataRepository : IDataRepository
    {
        private DataContext data;
        public void AddBook(Book book)
        {
            data.books.Add(book.Isbn,book);
        }

        public void AddClient(Client client)
        {
            data.readers.Add(client);
        }

        public void DeleteBook(Book book)
        {
            data.books.Remove(book.Isbn);
        }

        public void DeleteClient(Client client)
        {
            data.readers.Remove(client);
        }

        public IEnumerable<Book> GetAllBook()
        {
            return (IEnumerable<Book>)data.books;
        }

        public IEnumerable<Client> GetAllClient()
        {
            return data.readers;
        }

        public Book GetBook(Guid Id)
        {
            return data.books[Id];
        }

        public Client GetClient(string PESEL)
        {
            return data.readers.Find(reader => reader.Pesel == PESEL);
        }

        public void UpdateBook(Book book)
        {
            data.books.Add(book.Isbn, book);
        }

        public void UpdateClient( Client client)
        {
            DeleteClient(client);
            AddClient(client);
        }
    }
}
