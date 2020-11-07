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
        void UpdateBook(Guid Isbn, Book book);
        void DeleteBook(Guid Isbn);
        //CLIENT
        void AddClient(Client client);
        Client GetClient(string PESEL);
        IEnumerable<Client> GetAllClient();
        void UpdateClient(string PESEL, Client client);
        void DeleteClient(string Pesel);
        //BOOKEXAMPLE
        void AddBookExample(BookExample bookExample);
        Book GetBookExample(Guid Id);
        IEnumerable<BookExample> GetAllBookExamples();
        void UpdateBookExample(Guid Id,BookExample bookExample);
        void DeleteBookExample(Guid Id);
        //EVENT
        void AddPurchace(Purchace purchace);
        Purchace GetPurchace(Guid Id);
        IEnumerable<Purchace> GetAllPurchace();
        void UpdatePurchace(Guid id, Purchace purchace);
        void DeletePurchace(Purchace purchace);
    }
}
