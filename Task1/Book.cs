namespace Task1
{
    public class Book //KATALAOG w instrukcji
    {
        private string title;
        private string author;
        private string isbn;

        public Book(string title, string author, string isbn)
        {
            this.Title = title;
            this.Author = author;
            this.Isbn = isbn;
        }

        public string Isbn { get => isbn; set => isbn = value; }
        public string Author { get => author; set => author = value; }
        public string Title { get => title; set => title = value; }

        public string All { get => Isbn + " " + Title + " " + Author; }

        public override bool Equals(object obj)
        {
            return obj is Book book &&
                   isbn == book.isbn;
        }
    }
}
