using BookShop.model.data;
using System;
using System.Collections.Generic;

namespace BookShop.model.filler
{
    class ConstDataFiller : IDataFiller
    {
        public void Fill(DataContext context)
        {
            context.clients.Add(new Client("Adam", "Kowalski", "99340540232"));
            context.clients.Add(new Client("Jan", "Nowak", "02958382493"));

            List<Book> books = new List<Book>
        {
            new Book("Hobbit", "J.R.R. Tolkien", Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")),
            new Book("Mistborn", "Brandon Sanderson", Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")),
            new Book("The Lord of the Rings", "J.R.R. Tolkien", Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")),
            new Book("Harry Potter", "J.K. Rowling", Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226")),
            new Book("Metro 2033", "Dmitrij Głuchowski", Guid.Parse("820EF5E7-641D-4D4C-8785-36B538AF4226"))
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

            context.purchaces.Add(new Purchace(context.clients[1], context.bookExamples[5], DateTime.Parse("21/10/2020 21:11:00")));
            context.purchaces.Add(new Purchace(context.clients[1], context.bookExamples[7], DateTime.Parse("16/05/2019 11:30:00")));
            context.purchaces.Add(new Purchace(context.clients[0], context.bookExamples[0], DateTime.Parse("01/01/2011 11:59:00")));
        }
    }
}
