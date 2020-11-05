using System;

namespace Task1
{
    public class Book //KATALAOG w instrukcji
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public Guid Isbn { get; set; }

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
                return (this.Isbn.Equals(other.Isbn));
            }
        }
}
}
