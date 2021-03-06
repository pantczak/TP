﻿using BookShop.Model;
using System;
using System.Collections.Generic;

namespace BookShop.Logic
{
    public interface IDataService
    {
        void CreateClient(string firstName, string lastName, int age);
        void AddBook(string title, string author, Guid isbn);
        void AddBookExample(Book book, int tax, double basePrice);
        void PurchaceBook(Client client, BookExample bookExample);
        void ReturnBook(Client client, BookExample bookExample, Event evnt);
        void RemoveClient(Client client);
        void RemoveBook(Book book);
        void RemoveBookExample(BookExample bookExample);
        void RemoveEvent(Event evnt);
        void ModifyClient(Client oldClient,Client newClient);
        void ModifyBook( Book newBook);
        void ModifyBookExample(BookExample oldBookExample,BookExample newBookExample);
        void ModifyEvent(Event oldEvent, Event newEvent);
        IEnumerable<Book> GetAllBooks();
        IEnumerable<BookExample> GetAllBookExamples();
        IEnumerable<Client> GetAllClients();
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Purchase> GetAllPurchases();
        IEnumerable<Purchase> GetPurchasesInDateRange(DateTime from,DateTime to);
        IEnumerable<Event> GetEventsByClient(Client client);
        IEnumerable<Event> GetEventsByBookExample(BookExample bookExample);
        IEnumerable<Book> GetBooksByAuthor(string author);
        IEnumerable<Book> GetBooksByTitle(string title);
        Book GetBookByIsbn(Guid isbn);
        IEnumerable<BookExample> GetBookExamplesByBook(Book book);
        IEnumerable<BookExample> GetBookExamplesInPriceRange(double priceMin, double priceMax);
        IEnumerable<Client> GetClientsByFirstLetterOfName(char letter);
        IEnumerable<Client> GetClientsByAgeRange(int ageMin, int ageMax);






    }
}
