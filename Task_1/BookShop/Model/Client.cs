namespace BookShop.Model
{
    public class Client //WYKAZ 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; private set; }

        public Client(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
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
                return this.Age.Equals(other.Age) && this.FirstName.Equals(other.FirstName) && this.LastName.Equals(other.LastName);
            }
        }
    }
}
