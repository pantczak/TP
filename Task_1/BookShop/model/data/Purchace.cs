using System;
using System.Collections.Generic;

namespace BookShop.model.data
{
    public class Purchace : Event //ZDARZENIE
    {
        public Client Client { get; set; }
        public BookExample BookExample { get;set; }

        public Purchace( Client client, BookExample bookExample, DateTime dateOfPurchace): base(dateOfPurchace)
        {
            Client = client;
            BookExample = bookExample;
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
                return (this.BookExample.Equals(other.BookExample)) && (this.EventTime.Equals(other.EventTime))
                    && (this.Client.Equals(other.Client));
            }
        }
    }
}
