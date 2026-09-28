using System;
using System.Collections.Generic;
using System.Linq;

namespace Gun13
{
    internal class Program
    {
        static void Main()
        {
            var products = new List<Product> { new Product(1, "Defter", 40m, 5) };
            var customers = new List<Customer> { new Customer(1, "Berkay") };
            var orders = new List<Order>();
            while (true)
            {
                Console.Write("1 Ürünler  2 Sipariş ver  3 Siparişler  0 Çıkış: ");
                string choice = Console.ReadLine();
                if (choice == "0") return;
                if (choice == "1")
                {
                    foreach (var p in products) Console.WriteLine($"{p.Id}: {p.Name}, {p.Price:C}, stok {p.Stock}");
                }
                else if (choice == "2")
                {
                    Console.Write("Ürün kodu: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Invalid id."); continue; }
                    Product product = products.FirstOrDefault(p => p.Id == id);
                    if (product == null) { Console.WriteLine("Product not found."); continue; }
                    Console.Write("Adet: ");
                    if (!int.TryParse(Console.ReadLine(), out int quantity)) { Console.WriteLine("Invalid quantity."); continue; }
                    try
                    {
                        var order = new Order(orders.Count + 1, customers[0], product, quantity);
                        orders.Add(order);
                        Console.WriteLine($"Sipariş #{order.Id}: {order.Total:C}; kalan stok {product.Stock}");
                    }
                    catch (ArgumentOutOfRangeException error) { Console.WriteLine(error.Message); }
                    catch (InvalidOperationException error) { Console.WriteLine(error.Message); }
                }
                else if (choice == "3")
                {
                    foreach (var order in orders)
                        Console.WriteLine($"#{order.Id} {order.Customer.Name}: {order.Product.Name} x{order.Quantity}, {order.Total:C}");
                }
            }
        }
    }
}
