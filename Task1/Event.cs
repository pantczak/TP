using System;
using System.Collections.Generic;

namespace Task1
{
    public class Event //ZDARZENIE
    {
        private Reader reader;
        private Storage storage;
        private DateTime dateOfBorrow;
        private DateTime dateOfRetun;

        public Event(Reader reader, Storage storage, DateTime dateOfBorrow, DateTime dateOfRetun)
        {
            this.Reader = reader;
            this.Storage = storage;
            this.DateOfBorrow = dateOfBorrow;
            this.DateOfRetun = dateOfRetun;
        }

        public Reader Reader { get => reader; set => reader = value; }
        public Storage Storage { get => storage; set => storage = value; }
        public DateTime DateOfBorrow { get => dateOfBorrow; set => dateOfBorrow = value; }
        public DateTime DateOfRetun { get => dateOfRetun; set => dateOfRetun = value; }
        public string All { get => Reader.All + " " + Storage.All + " " + DateOfBorrow + " " + DateOfRetun; }

        public override bool Equals(object obj)
        {
            return obj is Event @event &&
                   EqualityComparer<Reader>.Default.Equals(reader, @event.reader) && // ?????????
                   EqualityComparer<Storage>.Default.Equals(storage, @event.storage) &&
                   dateOfBorrow == @event.dateOfBorrow;
        }
    }
}
