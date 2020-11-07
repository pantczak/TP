using BookShop.model;
using BookShop.model.data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookShop.logic
{
    class DataService : IDataService
    {
        private IDataRepository dataRepository;

        public DataService(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public void AddBook(string title, string author, Guid isbn)
        {
            dataRepository.AddBook(new Book(title, author, isbn));
        }

        public void AddBookExample(Book book, int tax, double basePrice)
        {
            dataRepository.AddBookExample(new BookExample(book, tax, basePrice));
        }

        public void CreateClient(string firstName, string lastName, string pesel)
        {
            dataRepository.AddClient(new Client(firstName, lastName, pesel));
        }

        public IEnumerable<Client> GetAllClientsByName(string name = null)
        {
            if (string.IsNullOrEmpty(name)){
                return dataRepository.GetAllClient();
            }
            return dataRepository.GetAllClient().Where(client =>
            (client.FirstName + " " + client.LastName).Contains(name));
        }

        public IEnumerable<BookExample> GetBookExamples(Guid isbn)
        {
            return dataRepository.GetAllBookExamples().Where(bookExample => bookExample.Book.Isbn == isbn);
        }

        public Dictionary<Book, int> GetBooks()
        {
            throw new NotImplementedException(); //BOOKCOUNT ???
        }

        public IEnumerable<Purchace> GetClientPurchaces(Client client)
        {
            return dataRepository.GetAllPurchace().Where(purchace => purchace.Client.Equals(client));
        }

        public IEnumerable<Purchace> GetPurchacesFromDate(DateTime? start = null, DateTime? end = null)
        {
            var result = dataRepository.GetAllPurchace();
            if (start.HasValue)
            {
                result = result.Where(purchace => purchace.DateOfPurchace >= start);
            }

            if (end.HasValue)
            {
                result = result.Where(purchace => purchace.DateOfPurchace <= end);
            }
            return result;
        }

        public void PurchaceBook(Client client, BookExample bookExample)
        {
            dataRepository.AddPurchace(new Purchace(client, bookExample,DateTime.Now));
        }

    }
}
