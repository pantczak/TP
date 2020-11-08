using System;

namespace BookShop.model.data
{
    public class Book //KATALAOG w instrukcji
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public Guid Isbn { get; private set; }

        public Book(string title, string author, Guid isbn)
        {
            Title = title;
            Author = author;
            Isbn = isbn;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Book other = (Book)obj;
                return (this.Isbn.Equals(other.Isbn) && this.Title.Equals(other.Title) && this.Author.Equals(other.Author));
            }
        }
}
}
