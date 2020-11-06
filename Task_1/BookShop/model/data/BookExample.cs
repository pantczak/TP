using System;

namespace BookShop.model.data
{
    public class BookExample //OPIS STANU
    {
        public Guid Guid { get;private set; }
        public Book Book { get;private set; }
        public int Tax { get; set; }
        public double BasePrice { get; set; }
        public double Price => BasePrice * Tax / 100.0;

        public BookExample(Guid id, Book book, int tax, double price)
        {
            Guid = id;
            Book = book;
            Tax = tax;
            BasePrice = price;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                BookExample other = (BookExample)obj;
                return (this.Guid.Equals(other.Guid)) && (this.Book.Equals(other.Book));
            }
        }
    }
}
