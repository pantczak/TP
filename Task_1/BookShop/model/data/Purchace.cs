using System;
using System.Collections.Generic;

namespace BookShop.model.data
{
    public class Purchace //ZDARZENIE
    {
        public Guid Guid { get;private set; }
        public Client Client { get; private set; }
        public BookExample BookExample { get; private set; }
        public DateTime DateOfPurchace { get; private set; }

        public Purchace(Guid id, Client client, BookExample bookExample, DateTime dateOfPurchace)
        {
            Guid = id;
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
                return (this.Guid.Equals(other.Guid)) && (this.Client.Equals(other.Client));
            }
        }
    }
}
