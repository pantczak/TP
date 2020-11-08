using BookShop.model.data;
using System;
using System.Collections.Generic;

namespace BookShop.model.filler
{
     public class ConstFiller : IDataFiller
     {
        public void Fill(DataContext context)
        {
            context.clients.Add(new Client("Adam", "Kowalski", "99340540232"));
            context.clients.Add(new Client("Jan", "Nowak", "02958382493"));

            List<Book> books = new List<Book>
        {
            new Book("Hobbit", "J.R.R. Tolkien", Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")),
            new Book("Mistborn", "Brandon Sanderson", Guid.Parse("BD8A1680-65BD-44A6-92C0-88827A3B473A")),
            new Book("The Lord of the Rings", "J.R.R. Tolkien", Guid.Parse("10957692-E446-4577-B8C7-8B41FF7C17B8")),
            new Book("Harry Potter", "J.K. Rowling", Guid.Parse("A5CC4A82-7498-4A10-8FB8-973954335474")),
            new Book("Metro 2033", "Dmitrij Głuchowski", Guid.Parse("86167D69-FA4D-42CC-8D08-9972F2F04EF5"))
        };
            context.books.Add(books[0].Isbn, books[0]);
            context.books.Add(books[1].Isbn, books[1]);
            context.books.Add(books[2].Isbn, books[2]);
            context.books.Add(books[3].Isbn, books[3]);
            context.books.Add(books[4].Isbn, books[4]);

            context.bookExamples.Add(new BookExample(books[0], 23, 59.99));
            context.bookExamples.Add(new BookExample(books[0], 23, 49.99));
            context.bookExamples.Add(new BookExample(books[1], 8, 29.99));
            context.bookExamples.Add(new BookExample(books[1], 8, 69.99));
            context.bookExamples.Add(new BookExample(books[1], 23, 129.99));
            context.bookExamples.Add(new BookExample(books[2], 3, 19.99));
            context.bookExamples.Add(new BookExample(books[2], 23, 9.99));
            context.bookExamples.Add(new BookExample(books[3], 8, 111.99));
            context.bookExamples.Add(new BookExample(books[3], 23, 22.99));
            context.bookExamples.Add(new BookExample(books[4], 23, 55.60));

            context.events.Add(new Purchace(context.clients[0], context.bookExamples[0], DateTime.Parse("5/10/2020 21:11:00")));
            context.events.Add(new Purchace(context.clients[1], context.bookExamples[7], DateTime.Parse("3/5/2019 11:30:00")));
            context.events.Add(new Purchace(context.clients[0], context.bookExamples[0], DateTime.Parse("11/1/2011 11:59:00")));
        }
     }
}
