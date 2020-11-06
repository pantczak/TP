namespace BookShop.model.data
{
    public class Client //WYKAZ 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; private set; }

        public Client(string firstName, string lastName, string pesel)
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
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
                return this.Pesel.Equals(other.Pesel);
            }
        }
    }
}
