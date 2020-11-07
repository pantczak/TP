using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.model.filler
{
    internal interface DataFiller
    {
         void Fill(DataContext context);
    }
}
