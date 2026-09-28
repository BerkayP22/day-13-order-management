using System;
namespace Gun13
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Stock { get; private set; }
        public Product(int id, string name, decimal price, int stock)
        {
            if (id <= 0 || string.IsNullOrWhiteSpace(name) || price < 0 || stock < 0)
                throw new ArgumentException("Invalid product details.");
            Id = id; Name = name; Price = price; Stock = stock;
        }
        public void Reserve(int quantity)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (quantity > Stock) throw new InvalidOperationException("Not enough stock.");
            Stock -= quantity;
        }
    }
}
