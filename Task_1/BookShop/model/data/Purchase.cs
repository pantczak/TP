using System;
using System.Collections.Generic;

namespace BookShop.model.data
{
    public class Purchase : Event //ZDARZENIE
    {
        public Purchase( Client client, BookExample bookExample,DateTime eventTime) : base(eventTime, client, bookExample) 
        {

        }
    }
}
