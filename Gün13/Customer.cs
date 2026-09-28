using System;
namespace Gun13
{
    public class Customer
    {
        public int Id { get; }
        public string Name { get; }
        public Customer(int id, string name)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Invalid customer details.");
            Id = id; Name = name;
        }
    }
}
