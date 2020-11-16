using System;

namespace BookShop.Model
{
    public class BookExample //OPIS STANU
    {
        public Book Book { get;set; }
        public int Tax { get; set; }
        public double BasePrice { get; set; }
        public double Price => (100+Tax) * BasePrice/100;

        public BookExample( Book book, int tax, double price)
        {
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
                return (this.Book.Equals(other.Book)) && (this.Tax.Equals(other.Tax)) && (this.BasePrice.Equals(other.BasePrice));
            }
        }
    }
}
