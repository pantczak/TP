using BookShop.model.data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace BookShop.model.filler
{
     public class ConstFiller : IDataFiller
     {
        private List<Client> clients;
        private Dictionary<Guid, Book> books;
        private ObservableCollection<Event> events;
        private ObservableCollection<BookExample> bookExamples;

        public ConstFiller(List<Client> clients , List<Book> books , List<Event> events, List<BookExample> bookExamples)
        {
            this.clients = clients;
            this.books = books.ToDictionary(b => b.Isbn, b => b);
            this.events = new ObservableCollection<Event>(events);
            this.bookExamples = new ObservableCollection<BookExample>(bookExamples);
        }

        public void Fill(DataContext dataContext)
        {
            dataContext.clients.AddRange(clients);

            foreach (KeyValuePair<Guid,Book> book in books)
            {
                dataContext.books.Add(book.Key, book.Value);
            }

            foreach (Event evnt in events)
            {
                dataContext.events.Add(evnt);
            }

            foreach (BookExample bookExample in bookExamples)
            {
                dataContext.bookExamples.Add(bookExample);
            }
        }
     }
}
