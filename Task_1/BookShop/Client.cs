namespace BookShop
{
    public class Client //WYKAZ 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }

        public Client(string firstName, string lastName, string id)
        {
            FirstName = firstName;
            LastName = lastName;
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Client other = (Client)obj;
                return this.Id.Equals(other.Id);
            }
        }
    }
}
