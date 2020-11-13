using BookShop.model.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace BookShop.model.filler
{
    public class FillFromFile : IDataFiller
    {
        private string filename;
        private List<Client> clients = new List<Client>();
        private List<Book> books = new List<Book>();
        private List<Event> events = new List<Event>();
        private List<BookExample> bookExamples = new List<BookExample>();

        public FillFromFile(string filename)
            {
            string[] lines = File.ReadAllLines(filename);
            foreach(var line in lines)
            {
                string[] values = line.Split(';');
                if (values.Length != 4)
                {
                    throw new Exception("Wrong file content");
                }
                switch(values[0])
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

                }
            }


        }
        public void Fill(DataContext context)
        {
            foreach (var client in clients)
            {
                context.clients.Add(client);
            }
            foreach (var book in books)
            {
                context.books.Add(book.Isbn, book);
            }
            foreach (var bookExample in bookExamples)
            {
                context.bookExamples.Add(bookExample);
            }
            foreach (var evnt in events)
            {
                context.events.Add(evnt);
            }
        }
    }
}
