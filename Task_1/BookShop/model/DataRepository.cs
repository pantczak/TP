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
            if (dataContext.bookExamples.Contains(bookExample))
            {
                throw new Exception("Data already exists");
            }
            checkBookCopyIsbn(bookExample);
            dataContext.bookExamples.Add(bookExample);
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
            if (dataContext.purchaces.Contains(purchace))
            {
                throw new Exception("Data already exists");
            }
            if (!dataContext.bookExamples.Contains(purchace.BookExample) )
            {
                throw new Exception("No such BookExample in DataRepository");
            }
            if(!dataContext.clients.Contains(purchace.Client))
                {
                throw new Exception("No such Client in DataRepository");
            }
            else
            {
                
                dataContext.purchaces.Add(purchace);
            }
        }

        public void DeleteBook(Book book)
        {
            foreach (var purchace in dataContext.purchaces)
            {
                if (purchace.BookExample.Book == book)
                {
                    throw new Exception("Book has examples in use, can't be deleted");
                }
            }
            var result = dataContext.books.Remove(book.Isbn);

            if (!result)
            {
                throw new Exception("No such book");
            }
        }

        public void DeleteBookExample(BookExample bookExample)
        {
            foreach (var purchace in dataContext.purchaces)
            {
                if (purchace.BookExample == bookExample)
                {
                    throw new Exception("Book example is in use, can't be deleted");
                }
            }
           var result =  dataContext.bookExamples.Remove(bookExample);

            if (!result)
            {
                throw new Exception("No such book copy");
            }
        }

        public void DeleteClient(Client client)
        {
            foreach (var purchace in dataContext.purchaces)
            {
                if (purchace.Client == client)
                {
                    throw new Exception("Client has purchaces, can't be deleted");
                }
            }

            var result = dataContext.clients.Remove(client);

            if (!result)
            {
                throw new Exception("No such client");
            }
        }

        public void DeletePurchace(Purchace purchace)
        {
            var result = dataContext.purchaces.Remove(purchace);

            if (!result)
            {
                throw new Exception("No such purchace");
            }
        }

        public IEnumerable<Book> GetAllBook()
        {
            return dataContext.books.Values;
        }

        public IEnumerable<BookExample> GetAllBookExamples()
        {
            return dataContext.bookExamples;
        }

        public IEnumerable<Client> GetAllClient()
        {
            return dataContext.clients;
        }

        public IEnumerable<Purchace> GetAllPurchace()
        {
            return dataContext.purchaces;
        }

        public Book GetBook(Guid Isbn)
        {
            if (dataContext.books.ContainsKey(Isbn))
            {
                return dataContext.books[Isbn];
            }
            throw new Exception("No such book");
        }

        public BookExample GetBookExample(int id)
        {
            if (dataContext.bookExamples.Count > id) 
            {
                return dataContext.bookExamples[id];
            }
            throw new Exception("No such book copy");
        }

        public Client GetClient(int id)
        {
            if (dataContext.clients.Count > id)
            {
                return dataContext.clients[id];
            }
            throw new Exception("No such client");
        }

        public Purchace GetPurchace(int id)
        {
            if(dataContext.purchaces.Count>id)
            {
                return dataContext.purchaces[id];
            }
            throw new Exception("No such purchase");
        }

        public void UpdateBook(Guid Isbn, Book book)
        {
            if (!dataContext.books.ContainsKey(Isbn)) 
            {
                throw new Exception("No such book");
            }
            Book currentBook = GetBook(Isbn);
            foreach (var bookExample in dataContext.bookExamples)
            {
                if (bookExample.Book == currentBook)
                {
                    bookExample.Book = book;
                }
            }
            dataContext.books.Add(Isbn, book);
        }

        public void UpdateBookExample(int Id, BookExample bookExample)
        {
            if (!(Id<dataContext.bookExamples.Count))
            {
                throw new Exception("No such book index");
            }
            checkBookCopyIsbn(bookExample);
            BookExample currentBookExample = GetBookExample(Id);
            foreach (var purchase in dataContext.purchaces) 
            {
                if (purchase.BookExample == currentBookExample) {
                    purchase.BookExample = bookExample;
                }
            }
            dataContext.bookExamples.Remove(currentBookExample);
            dataContext.bookExamples.Insert(Id, bookExample);
        }

        public void UpdateClient(int id, Client client)
        {
            if (!(id < dataContext.clients.Count))
            {
                throw new Exception("No such client index");
            }
            Client currentClient = GetClient(id);
            foreach(var purchase in dataContext.purchaces)
            {
                if(purchase.Client==currentClient)
                {
                    purchase.Client = client;
                }
            }
            dataContext.clients.Remove(currentClient);
            dataContext.clients.Insert(id, client);
        }

        public void UpdatePurchace(int id, Purchace purchace)
        {
            if (!(id < dataContext.purchaces.Count))
            {
                throw new Exception("No such purchase index");
            }
            if (!dataContext.bookExamples.Contains(purchace.BookExample))
            {
                throw new Exception("No such BookExample in DataRepository");
            }
            dataContext.purchaces.RemoveAt(id);
            dataContext.purchaces.Insert(id, purchace);
        }



        //PRIVATE METHODS

        private void checkBookCopyIsbn(BookExample bookExample)
        {
            if (!dataContext.books.ContainsKey(bookExample.Book.Isbn))
            {
                throw new Exception("Wrong book example Isbn reference");
            }

        }
    }
}
