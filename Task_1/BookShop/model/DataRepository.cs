using BookShop.model.data;
using BookShop.model.filler;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.model
{
    public class DataRepository : IDataRepository
    {
        private DataContext dataContext;

        public DataRepository(IDataFiller dataFiller)
        {
            dataContext = new DataContext();
            dataFiller.Fill(dataContext);
        }

        public void AddBook(Book book)
        {
            if (dataContext.books.ContainsKey(book.Isbn))
            {
                throw new Exception("Data already exists");
            }
            dataContext.books.Add(book.Isbn, book);
        }

        public void AddBookExample(BookExample bookExample)
        {
            throw new NotImplementedException();
        }

        public void AddClient(Client client)
        {
            if (dataContext.clients.Contains(client))
            {
                throw new Exception("Data already exists");
            }
            dataContext.clients.Add(client);
        }

        public void AddPurchace(Purchace purchace)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(Guid Isbn)
        {
            throw new NotImplementedException();
        }

        public void DeleteBookExample(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void DeleteClient(string Pesel)
        {
            throw new NotImplementedException();
        }

        public void DeletePurchace(Purchace purchace)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetAllBook()
        {
            return (IEnumerable<Book>)dataContext.books;
        }

        public IEnumerable<BookExample> GetAllBookExamples()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClient()
        {
            return dataContext.clients;
        }

        public IEnumerable<Purchace> GetAllPurchace()
        {
            throw new NotImplementedException();
        }

        public Book GetBook(Guid Id)
        {
            return dataContext.books[Id];
        }

        public Book GetBookExample(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Client GetClient(string PESEL)
        {
            return dataContext.clients.Find(reader => reader.Pesel == PESEL);
        }

        public Purchace GetPurchace(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Guid Isbn, Book book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBookExample(Guid Id, BookExample bookExample)
        {
            throw new NotImplementedException();
        }

        public void UpdateClient(string PESEL, Client client)
        {
            throw new NotImplementedException();
        }

        public void UpdatePurchace(Guid id, Purchace purchace)
        {
            throw new NotImplementedException();
        }
    }
}
