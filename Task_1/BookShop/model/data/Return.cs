using System;

namespace BookShop.model.data
{
    public class Return : Event
    {
        public DateTime ReturnDate { get; set; }

        public Return(DateTime returnDate, Client client, BookExample bookExample, DateTime eventTime) : base(eventTime,client,bookExample)
        {
            ReturnDate = returnDate;
        }

    }
}
