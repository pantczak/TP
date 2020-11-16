using System;

namespace BookShop.Model
{
    public abstract class Event
    {
        public DateTime EventTime { get; set; }
        public Client Client { get; set; }
        public BookExample BookExample { get; set; }

        protected Event(DateTime eventTime, Client client, BookExample bookExample)
        {
            EventTime = eventTime;
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
                Event other = (Event)obj;
                return (this.EventTime.Equals(other.EventTime)) && (this.Client.Equals(other.Client)) && (this.BookExample.Equals(other.BookExample));
            }
        }
    }
}
