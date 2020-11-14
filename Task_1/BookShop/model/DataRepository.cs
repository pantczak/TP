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

        public void AddEvent(Event evnt)
        {
            if (dataContext.events.Contains(evnt))
            {
                throw new Exception("Data already exists");
            }
            if (!dataContext.bookExamples.Contains(evnt.BookExample))
            {
                throw new Exception("No such BookExample in DataRepository");
            }
            if (!dataContext.clients.Contains(evnt.Client))
            {
                throw new Exception("No such Client in DataRepository");
            }
            else
            {

                dataContext.events.Add(evnt);
            }

        }

        public void DeleteBook(Book book)
        {
            foreach (Event evnt in dataContext.events)
            {


                if (evnt.BookExample.Book == book)
                {
                    throw new Exception("Book has examples in use, can't be deleted");
                }

            }
                Boolean result = dataContext.books.Remove(book.Isbn);

                if (!result)
                {
                    throw new Exception("No such book");
                }

            
        }

        public void DeleteBookExample(BookExample bookExample)
        {
            foreach (Event evnt in dataContext.events)
            {

                if (evnt.BookExample == bookExample)
               {
                    throw new Exception("Book example is in use, can't be deleted");
               }
            }
           Boolean result =  dataContext.bookExamples.Remove(bookExample);

            if (!result)
            {
                throw new Exception("No such book copy");
            }
        }

        public void DeleteClient(Client client)
        {
            foreach (Event evnt in dataContext.events)
            {
                if (evnt.Client == client)
                {
                    throw new Exception("Client has purchaces, can't be deleted");
                }
            }

            Boolean result = dataContext.clients.Remove(client);

            if (!result)
            {
                throw new Exception("No such client");
            }
        }

        public void DeleteEvent(Event evnt)
        {
            Boolean result = dataContext.events.Remove(evnt);

            if (!result)
            {
                throw new Exception("No such event");
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

        public IEnumerable<Event> GetAllEvent()
        {
            return dataContext.events;
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

        public Event GetEvent(int id)
        {
            if(dataContext.events.Count>id)
            {
                return dataContext.events[id];
            }
            throw new Exception("No such event");
        }

        public void UpdateBook(Book book)
        {
            if (!dataContext.books.ContainsKey(book.Isbn)) 
            {
                throw new Exception("No such book");
            }
            if (dataContext.books.ContainsValue(book))
            {
                throw new Exception("Data already exists");
            }
            Book currentBook = GetBook(book.Isbn);
            foreach (BookExample bookExample in dataContext.bookExamples)
            {
                if (bookExample.Book == currentBook)
                {
                    bookExample.Book = book;
                }
            }
            dataContext.books.Remove(book.Isbn);
            dataContext.books.Add(book.Isbn, book);
        }

        public void UpdateBookExample(int Id, BookExample bookExample)
        {
            if (!(Id<dataContext.bookExamples.Count))
            {
                throw new Exception("No such book copy index");
            }
            if (dataContext.bookExamples.Contains(bookExample))
            {
                throw new Exception("Data already exists");
            }
            checkBookCopyIsbn(bookExample);
            BookExample currentBookExample = GetBookExample(Id);
            foreach (Event evnt in dataContext.events) 
            {
                    if (evnt.BookExample == currentBookExample)
                    {
                        evnt.BookExample = bookExample;
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
            if (dataContext.clients.Contains(client))
            {
                throw new Exception("Data already exists");
            }
            Client currentClient = GetClient(id);
            foreach(Event evnt in dataContext.events)
            {
                    if (evnt.Client == currentClient)
                    {
                        evnt.Client = client;
                    }
            }
            dataContext.clients.Remove(currentClient);
            dataContext.clients.Insert(id, client);
        }

        public void UpdateEvent(int id, Event evnt)
        {
            if (!(id < dataContext.events.Count))
            {
                throw new Exception("No such event index");
            }
            if (!dataContext.bookExamples.Contains(evnt.BookExample))
            {
                throw new Exception("No such BookExample in DataRepository");
            }
            if (dataContext.events.Contains(evnt))
            {
                throw new Exception("Data already exists");
            }
            dataContext.events.RemoveAt(id);
            dataContext.events.Insert(id, evnt);
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
