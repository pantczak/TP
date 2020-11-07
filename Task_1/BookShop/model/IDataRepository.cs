﻿using BookShop.model.data;
using System;
using System.Collections.Generic;

namespace BookShop.model
{
    public interface IDataRepository
    {
        void AddBook(Book book);
        Book GetBook(Guid Id);
        IEnumerable<Book> GetAllBook();
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void AddClient(Client client);
        Client GetClient(string PESEL);
        IEnumerable<Client> GetAllClient();
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}