using System;
using System.Collections.Generic;

namespace Task1
{
    public class BookExample //OPIS STANU
    {
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public int Tax { get; set; }
        public int Quantitiy { get; set; }
        public double Price { get; set; }

        public BookExample(Guid id, Book book, int tax, int quantitiy, double price)
        {
            Id = id;
            Book = book;
            Tax = tax;
            Quantitiy = quantitiy;
            Price = price;
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
                return (this.Id.Equals(other.Id)) && (this.Book.Equals(other.Book));
            }
        }
    }
}
