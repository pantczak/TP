namespace Task1
{
    public class Reader //WYKAZ w Instrukcji
    {
        private string firstName;
        private string lastName;
        private int id;

        public Reader(string firstName, string lastName, int id)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Id = id;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Id { get => id; set => id = value; }
        public string All { get => Id + " " + FirstName + " " + LastName; }

        public override bool Equals(object obj)
        {
            return obj is Reader reader &&
                   id == reader.id;
        }
    }
}
