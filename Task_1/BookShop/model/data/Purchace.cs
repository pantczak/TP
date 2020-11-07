using System;
using System.Collections.Generic;

namespace BookShop.model.data
{
    public class Purchace //ZDARZENIE
    {
        public Client Client { get; private set; }
        public BookExample BookExample { get; private set; }
        public DateTime DateOfPurchace { get; private set; }

        public Purchace( Client client, BookExample bookExample, DateTime dateOfPurchace)
        {
            Client = client;
            BookExample = bookExample;
            DateOfPurchace = dateOfPurchace;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Purchace other = (Purchace)obj;
                return (this.BookExample.Equals(other.BookExample)) && (this.DateOfPurchace.Equals(other.DateOfPurchace))
                    && (this.Client.Equals(other.Client));
            }
        }
    }
}
