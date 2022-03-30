namespace CleanArchitecture.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public Customer(string name)
        {
            Name = name;
        }
        public string Name { get; private set; }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}