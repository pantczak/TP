using BookShop.model.data;
using System;
using System.Collections.Generic;

namespace BookShop.model
{
    public interface IDataRepository
    {
        //BOOK
        void AddBook(Book book);
        Book GetBook(Guid Isbn);
        IEnumerable<Book> GetAllBook();
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        //CLIENT
        void AddClient(Client client);
        Client GetClient(int id);
        IEnumerable<Client> GetAllClient();
        void UpdateClient(int id, Client client);
        void DeleteClient(Client client);
        //BOOKEXAMPLE
        void AddBookExample(BookExample bookExample);
        BookExample GetBookExample(int id);
        IEnumerable<BookExample> GetAllBookExamples();
        void UpdateBookExample(int Id,BookExample bookExample);
        void DeleteBookExample(BookExample bookExample);
        //EVENT
        void AddEvent(Event evnt);
        Event GetEvent(int id);
        IEnumerable<Event> GetAllEvent();
        void UpdateEvent(int id, Event evnt);
        void DeleteEvent(Event @event);
    }
}
