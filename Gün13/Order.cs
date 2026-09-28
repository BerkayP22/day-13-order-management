namespace Gun13
{
    public class Order
    {
        public int Id { get; }
        public Customer Customer { get; }
        public Product Product { get; }
        public int Quantity { get; }
        public decimal Total { get; }
        public Order(int id, Customer customer, Product product, int quantity)
        {
            product.Reserve(quantity);
            Id = id; Customer = customer; Product = product; Quantity = quantity;
            Total = product.Price * quantity;
        }
    }
}
