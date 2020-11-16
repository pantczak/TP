using BookShop.model.data;
using BookShop.model;
using System;
using System.Collections.Generic;

namespace BookShop.model
{
    public class DataRepository : IDataRepository
    {
        private DataContext dataContext = new DataContext();

        private IDataFiller dataFiller;

        public DataRepository(IDataFiller dataFiller)
        {
            this.dataFiller = dataFiller;
            this.dataFiller.Fill(dataContext);
        }

        public void AddBook(Book book)
        {
            if (dataContext.Books.ContainsKey(book.Isbn))
            {
                throw new Exception("Data already exists");
            }
            dataContext.Books.Add(book.Isbn, book);
        }

        public void AddBookExample(BookExample bookExample)
        {
            if (dataContext.BookExamples.Contains(bookExample))
            {
                throw new Exception("Data already exists");
            }
            checkBookCopyIsbn(bookExample);
            dataContext.BookExamples.Add(bookExample);
        }

        public void AddClient(Client client)
        {
            if (dataContext.Clients.Contains(client))
            {
                throw new Exception("Data already exists");
            }
            dataContext.Clients.Add(client);
        }

        public void AddEvent(Event evnt)
        {
            if (dataContext.Events.Contains(evnt))
            {
                throw new Exception("Data already exists");
            }
            if (!dataContext.BookExamples.Contains(evnt.BookExample))
            {
                throw new Exception("No such BookExample in DataRepository");
            }
            if (!dataContext.Clients.Contains(evnt.Client))
            {
                throw new Exception("No such Client in DataRepository");
            }
            else
            {

                dataContext.Events.Add(evnt);
            }

        }

        public void DeleteBook(Book book)
        {
            foreach (Event evnt in dataContext.Events)
            {


                if (evnt.BookExample.Book == book)
                {
                    throw new Exception("Book has examples in use, can't be deleted");
                }

            }
                Boolean result = dataContext.Books.Remove(book.Isbn);

                if (!result)
                {
                    throw new Exception("No such book");
                }

            
        }

        public void DeleteBookExample(BookExample bookExample)
        {
            foreach (Event evnt in dataContext.Events)
            {

                if (evnt.BookExample == bookExample)
               {
                    throw new Exception("Book example is in use, can't be deleted");
               }
            }
           Boolean result =  dataContext.BookExamples.Remove(bookExample);

            if (!result)
            {
                throw new Exception("No such book copy");
            }
        }

        public void DeleteClient(Client client)
        {
            foreach (Event evnt in dataContext.Events)
            {
                if (evnt.Client == client)
                {
                    throw new Exception("Client has purchaces, can't be deleted");
                }
            }

            Boolean result = dataContext.Clients.Remove(client);

            if (!result)
            {
                throw new Exception("No such client");
            }
        }

        public void DeleteEvent(Event evnt)
        {
            Boolean result = dataContext.Events.Remove(evnt);

            if (!result)
            {
                throw new Exception("No such event");
            }
        }

        public IEnumerable<Book> GetAllBook()
        {
            return dataContext.Books.Values;
        }

        public IEnumerable<BookExample> GetAllBookExamples()
        {
            return dataContext.BookExamples;
        }

        public IEnumerable<Client> GetAllClient()
        {
            return dataContext.Clients;
        }

        public IEnumerable<Event> GetAllEvent()
        {
            return dataContext.Events;
        }

        public Book GetBook(Guid Isbn)
        {
            if (dataContext.Books.ContainsKey(Isbn))
            {
                return dataContext.Books[Isbn];
            }
            throw new Exception("No such book");
        }

        public BookExample GetBookExample(int id)
        {
            if (dataContext.BookExamples.Count > id) 
            {
                return dataContext.BookExamples[id];
            }
            throw new Exception("No such book copy");
        }

        public Client GetClient(int id)
        {
            if (dataContext.Clients.Count > id)
            {
                return dataContext.Clients[id];
            }
            throw new Exception("No such client");
        }

        public Event GetEvent(int id)
        {
            if(dataContext.Events.Count>id)
            {
                return dataContext.Events[id];
            }
            throw new Exception("No such event");
        }

        public void UpdateBook(Book book)
        {
            if (!dataContext.Books.ContainsKey(book.Isbn)) 
            {
                throw new Exception("No such book");
            }
            if (dataContext.Books.ContainsValue(book))
            {
                throw new Exception("Data already exists");
            }
            Book currentBook = GetBook(book.Isbn);
            foreach (BookExample bookExample in dataContext.BookExamples)
            {
                if (bookExample.Book == currentBook)
                {
                    bookExample.Book = book;
                }
            }
            dataContext.Books.Remove(book.Isbn);
            dataContext.Books.Add(book.Isbn, book);
        }

        public void UpdateBookExample(int Id, BookExample bookExample)
        {
            if (!(Id<dataContext.BookExamples.Count))
            {
                throw new Exception("No such book copy index");
            }
            if (dataContext.BookExamples.Contains(bookExample))
            {
                throw new Exception("Data already exists");
            }
            checkBookCopyIsbn(bookExample);
            BookExample currentBookExample = GetBookExample(Id);
            foreach (Event evnt in dataContext.Events) 
            {
                    if (evnt.BookExample == currentBookExample)
                    {
                        evnt.BookExample = bookExample;
                    }
            }
            dataContext.BookExamples.Remove(currentBookExample);
            dataContext.BookExamples.Insert(Id, bookExample);
        }

        public void UpdateClient(int id, Client client)
        {
            if (!(id < dataContext.Clients.Count))
            {
                throw new Exception("No such client index");
            }
            if (dataContext.Clients.Contains(client))
            {
                throw new Exception("Data already exists");
            }
            Client currentClient = GetClient(id);
            foreach(Event evnt in dataContext.Events)
            {
                    if (evnt.Client == currentClient)
                    {
                        evnt.Client = client;
                    }
            }
            dataContext.Clients.Remove(currentClient);
            dataContext.Clients.Insert(id, client);
        }

        public void UpdateEvent(int id, Event evnt)
        {
            if (!(id < dataContext.Events.Count))
            {
                throw new Exception("No such event index");
            }
            if (!dataContext.BookExamples.Contains(evnt.BookExample))
            {
                throw new Exception("No such BookExample in DataRepository");
            }
            if (dataContext.Events.Contains(evnt))
            {
                throw new Exception("Data already exists");
            }
            dataContext.Events.RemoveAt(id);
            dataContext.Events.Insert(id, evnt);
        }



        //PRIVATE METHODS

        private void checkBookCopyIsbn(BookExample bookExample)
        {
            if (!dataContext.Books.ContainsKey(bookExample.Book.Isbn))
            {
                throw new Exception("Wrong book example Isbn reference");
            }

        }
    }
}
