using System;
using System.Collections.Generic;

namespace BookShop
{
    public class Purchace //ZDARZENIE
    {
        public Guid Id { get; set; }
        public Client Client;
        public BookExample BookExample;
        public DateTime DateOfPurchace { get; set; }

        public Purchace(Guid id, Client client, BookExample bookExample, DateTime dateOfPurchace)
        {
            Id = id;
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
                return (this.Id.Equals(other.Id)) && (this.Client.Equals(other.Client));
            }
        }
    }
}
