using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.model.filler
{
    public interface IDataFiller
    {
         void Fill(DataContext context);
    }
}
