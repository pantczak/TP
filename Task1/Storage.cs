using System;
using System.Collections.Generic;

namespace Task1
{
    public class Storage //OPIS STANU
    {
        private Book book;
        private DateTime dateOfPurchase;
        private int quantitiy;
        private double price;
        //TODO stan magazynu - produkt ???

        public Storage(Book book, DateTime dateOfPurchase, int quantitiy, double price)
        {
            this.Book = book;
            this.DateOfPurchase = dateOfPurchase;
            this.Quantitiy = quantitiy;
            this.Price = price;
        }

        public Book Book { get => book; set => book = value; }
        public DateTime DateOfPurchase { get => dateOfPurchase; set => dateOfPurchase = value; }
        public int Quantitiy { get => quantitiy; set => quantitiy = value; }
        public double Price { get => price; set => price = value; }
        public string All { get => Book.All + " " + DateOfPurchase + " " + Quantitiy + " " + Price; }

        public override bool Equals(object obj)
        {
            return obj is Storage storage &&
                   EqualityComparer<Book>.Default.Equals(book, storage.book) && // ?????
                   dateOfPurchase == storage.dateOfPurchase;
        }
    }
}
