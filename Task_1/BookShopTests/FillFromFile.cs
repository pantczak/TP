﻿using BookShop.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace BookShopTests
{
    public class FillFromFile : IDataFiller
    {
        private List<Client> clients = new List<Client>();
        private List<Book> books = new List<Book>();
        private List<Event> events = new List<Event>();
        private List<BookExample> bookExamples = new List<BookExample>();

        public FillFromFile(string filename)
            {
            string[] lines = File.ReadAllLines(filename);
            foreach(String line in lines)
            {
                string[] values = line.Split(';');

                try
                {
                    switch (values[0])
                    {
                        case "Client":
                            clients.Add(new Client(values[1], values[2], int.Parse(values[3])));
                            break;
                        case "Book":
                            books.Add(new Book(values[1], values[2], Guid.Parse(values[3])));
                            break;
                        case "BookExample":
                            bookExamples.Add(new BookExample(books[int.Parse(values[1])], int.Parse(values[2]), Double.Parse(values[3])));
                            break;
                        case "Purchase":
                            events.Add(new Purchase(clients[int.Parse(values[1])], bookExamples[int.Parse(values[2])], DateTime.Parse(values[3])));
                            break;
                        case "Return":
                            events.Add(new Return(DateTime.Parse(values[4]), clients[int.Parse(values[1])], bookExamples[int.Parse(values[2])], DateTime.Parse(values[3])));
                            break;

                    }
                }
                catch (IndexOutOfRangeException)
                {

                    throw new Exception("Wrong file content");
                }
            }


        }
        public void Fill(DataContext context)
        {
            foreach (Client client in clients)
            {
                context.Clients.Add(client);
            }
            foreach (Book book in books)
            {
                context.Books.Add(book.Isbn, book);
            }
            foreach (BookExample bookExample in bookExamples)
            {
                context.BookExamples.Add(bookExample);
            }
            foreach (Event evnt in events)
            {
                context.Events.Add(evnt);
            }
        }
    }
}
